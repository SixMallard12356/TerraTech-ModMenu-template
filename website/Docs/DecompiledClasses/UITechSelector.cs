#define UNITY_EDITOR
using System.Collections.Generic;
using MonsterLove.StateMachine;
using Payload.UI.Commands;
using Payload.UI.Commands.Steam;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITechSelector : MonoBehaviour, IMoveHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler, IUIExtraButtonHandler2
{
	public delegate void PlaceTechHandler(TechData techData, Vector3 position, Quaternion rotation);

	public delegate void SelectionEvent(Snapshot capture, int techCost);

	public delegate bool CanAcceptTechCallback(TechData techData, int techCost);

	public enum TechLocations
	{
		Local,
		Twitter,
		Steam,
		Null
	}

	public enum TwitterStates
	{
		TwitterIdle,
		TwitterAuth,
		TwitterLoading,
		TwitterComplete
	}

	public enum SteamStates
	{
		SteamIdle,
		SteamLoading,
		SteamPopulate,
		SteamComplete
	}

	public enum CommunityTagGroup
	{
		Default,
		Sumo
	}

	private struct TechUIElement
	{
		public UIPreset uiPreset;

		public Toggle toggleButton;

		public UnityAction<bool> clickHandler;
	}

	[SerializeField]
	private RectTransform m_ContentLayout;

	[SerializeField]
	private UIPreset m_ImagePrefab;

	[SerializeField]
	private Text m_LoadingInfo;

	[SerializeField]
	private Text m_SelectedTechName;

	[SerializeField]
	private Text m_SelectedTechCreator;

	[SerializeField]
	private Text m_SelectedTechPrice;

	[SerializeField]
	private Button m_SelectButton;

	[SerializeField]
	private bool m_DoubleClickSelectsItem = true;

	[SerializeField]
	private RectTransform m_UITabsPanel;

	[SerializeField]
	private RectTransform m_BottomPanel;

	[SerializeField]
	private Button m_SteamButton;

	[SerializeField]
	private TechLocations m_DefaultSelectedTab;

	[SerializeField]
	private bool m_ResetDefaultTabOnHide;

	[SerializeField]
	private ScrollRect m_GridScrollRect;

	[SerializeField]
	private UITechLoaderHUD m_UITechLoaderHUD;

	[SerializeField]
	private GameObject[] m_HiddenUIOnConsole;

	[SerializeField]
	private UIUndiscoveredBlocks m_UndiscoveredBlocks;

	public SelectionEvent OnListSelectEvent;

	public Event<Snapshot> OnSelectionAcceptedEvent;

	public CanAcceptTechCallback CanSelectTechCallback;

	private List<TechUIElement> m_MyImages;

	private TwitterAPI.TweetWithMediaDataThreaded m_FetchedTweets;

	private SnapshotCollectionDisk m_SnapshotsDisk;

	private SnapshotCollectionTwitter m_SnapshotsTwitter;

	private SnapshotCollectionSteam m_SnapshotsSteam;

	private IInventory<BlockTypes> m_BlockInventory;

	private bool m_DisplayOnlyAvailable;

	private bool m_AllowPartials;

	private bool m_FinishedFetchingVehicleData;

	private bool m_FinishedLoadingCaptures;

	private bool m_TwitterLoginFailed;

	private bool m_TwitterAuthPending;

	private CommunityTagGroup m_CommunityTagGroup;

	private CommandOperation<SteamDownloadData> m_SteamGetTechList;

	private SteamDownloadData m_SteamItems;

	private int m_NumTweetsDecoded;

	private int m_NumCapturesAdded;

	private StateMachine<TechLocations> m_FSM;

	private TechLocations m_CurrentTab;

	private TechLocations m_PreviousTab = TechLocations.Null;

	private Snapshot m_SelectedTech;

	private int m_SelectedTechCost;

	private StateMachine<TwitterStates> m_TwitterFSM;

	private StateMachine<SteamStates> m_SteamFSM;

	private DoubleClickListener m_DoubleClickListener;

	private ToggleGroup m_ButtonToggleGroup;

	private List<UITechSelectorTabHelper> m_UITabHelpers = new List<UITechSelectorTabHelper>();

	private Toggle m_PreviousSnapShotButton;

	private TechDataAvailValidation m_SelectedTechBlockCache = new TechDataAvailValidation();

	private static string[] m_CommunityHashtag = new string[2] { "#myTerraTech", "#myTerraTechSumo" };

	public bool IsVisible => base.gameObject.activeSelf;

	public TechLocations CurrentTab => m_FSM.State;

	public void Show()
	{
		if (base.gameObject.activeSelf)
		{
			return;
		}
		base.gameObject.SetActive(value: true);
		TechLocations targetTab = m_PreviousTab;
		if (m_PreviousTab == TechLocations.Null)
		{
			targetTab = m_DefaultSelectedTab;
		}
		ShowTab(targetTab);
		for (int i = 0; i < m_UITabHelpers.Count; i++)
		{
			UITechSelectorTabHelper uITechSelectorTabHelper = m_UITabHelpers[i];
			bool active;
			switch (uITechSelectorTabHelper.TargetTab)
			{
			case TechLocations.Local:
				active = true;
				break;
			case TechLocations.Steam:
				active = Singleton.Manager<ManSteamworks>.inst.Workshop.Enabled;
				break;
			case TechLocations.Twitter:
				active = false;
				break;
			default:
				d.AssertFormat(false, "Unhandled tech location type: {0}", uITechSelectorTabHelper.TargetTab);
				active = false;
				break;
			}
			uITechSelectorTabHelper.gameObject.SetActive(active);
		}
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.FullscreenUI);
		Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(base.gameObject);
		UpdateJoypadUI();
	}

	public void Hide()
	{
		if (base.gameObject.activeSelf)
		{
			ShowTab(TechLocations.Null);
			if (m_ResetDefaultTabOnHide)
			{
				m_PreviousTab = TechLocations.Null;
			}
			base.gameObject.SetActive(value: false);
			m_PreviousSnapShotButton = null;
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.FullscreenUI);
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(base.gameObject);
		}
	}

	public void SetInventory(IInventory<BlockTypes> availableInventory)
	{
		m_BlockInventory = availableInventory;
	}

	public void SetCommunitySource(CommunityTagGroup tagGroup)
	{
		m_CommunityTagGroup = tagGroup;
	}

	public void SetDisplayAvailableOnly(bool displayAvailableOnly)
	{
		m_DisplayOnlyAvailable = displayAvailableOnly;
	}

	public void ShowTab(TechLocations targetTab)
	{
		if (m_FSM.State != targetTab)
		{
			HighlightTech(null);
			ClearTab();
			m_NumCapturesAdded = 0;
			m_FSM.ChangeState(targetTab);
		}
	}

	private void Local_Enter()
	{
		m_SnapshotsDisk = Singleton.Manager<ManSnapshots>.inst.ServiceDisk.GetSnapshotCollectionDisk();
	}

	private void Local_Update()
	{
		LoadOneTechPerFrameIntoSprites(m_SnapshotsDisk);
	}

	private void Twitter_Enter()
	{
		m_FinishedFetchingVehicleData = false;
		m_FinishedLoadingCaptures = false;
		m_TwitterLoginFailed = false;
		m_TwitterFSM.ChangeState(TwitterStates.TwitterAuth);
	}

	private void TwitterAuth_Enter()
	{
		m_TwitterAuthPending = true;
		Singleton.Manager<TwitterAuthenticationUIHandler>.inst.TryAuthenticateAsync(TwitterAuth_OnComplete);
	}

	private void TwitterAuth_OnComplete(bool loggedIn)
	{
		m_TwitterAuthPending = false;
		m_TwitterLoginFailed = !loggedIn;
	}

	private void TwitterAuth_Update()
	{
		if (!m_TwitterAuthPending)
		{
			if (!m_TwitterLoginFailed)
			{
				m_TwitterFSM.ChangeState(TwitterStates.TwitterLoading);
			}
			else
			{
				m_TwitterFSM.ChangeState(TwitterStates.TwitterComplete);
			}
		}
	}

	private void TwitterAuth_Exit()
	{
		if (m_TwitterAuthPending)
		{
			m_TwitterAuthPending = false;
			Singleton.Manager<TwitterAuthenticationUIHandler>.inst.CancelAsync();
		}
	}

	private void TwitterLoading_Enter()
	{
		string loadVehicleModeRestriction = Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleModeRestriction;
		string loadVehicleSubModeRestriction = Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleSubModeRestriction;
		string loadVehicleUserDataRestriction = Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleUserDataRestriction;
		m_SnapshotsTwitter = new SnapshotCollectionTwitter(loadVehicleModeRestriction, loadVehicleSubModeRestriction, loadVehicleUserDataRestriction);
		m_FinishedFetchingVehicleData = false;
		m_FetchedTweets = new TwitterAPI.TweetWithMediaDataThreaded();
		string text = m_CommunityHashtag[(int)m_CommunityTagGroup];
		Singleton.Manager<TwitterAPI>.inst.RetrieveTaggedTweetsAsync(text, myTweets: false, m_FetchedTweets, TwitterLoading_RequestCompleted);
		m_NumTweetsDecoded = 0;
	}

	private void TwitterLoading_Update()
	{
		if (!LoadOneTechPerFrameIntoSprites(m_SnapshotsTwitter) && !DecodeTweet(m_SnapshotsTwitter) && m_FinishedFetchingVehicleData)
		{
			m_TwitterFSM.ChangeState(TwitterStates.TwitterComplete);
		}
	}

	private void TwitterLoading_RequestCompleted()
	{
		m_FinishedFetchingVehicleData = true;
	}

	private void TwitterLoading_Exit()
	{
		if (!m_FinishedFetchingVehicleData)
		{
			Singleton.Manager<TwitterAPI>.inst.Cancel();
		}
	}

	private void TwitterComplete_Enter()
	{
		m_FinishedLoadingCaptures = true;
	}

	private void Twitter_Exit()
	{
		m_TwitterFSM.ChangeState(TwitterStates.TwitterIdle);
	}

	private void Steam_Enter()
	{
		m_FinishedLoadingCaptures = false;
		if (m_SteamButton != null)
		{
			m_SteamButton.gameObject.SetActive(value: false);
		}
		m_SteamFSM.ChangeState(SteamStates.SteamLoading);
	}

	private void SteamLoading_Enter()
	{
		if (m_SteamGetTechList == null)
		{
			m_SteamGetTechList = new CommandOperation<SteamDownloadData>();
			SteamCreateQueryCommand command = new SteamCreateQueryCommand();
			m_SteamGetTechList.Add(command);
		}
		if (m_SnapshotsSteam == null)
		{
			m_SnapshotsSteam = new SnapshotCollectionSteam();
		}
		m_SnapshotsSteam.Clear();
		m_SteamGetTechList.Completed.Subscribe(SteamLoading_OnComplete);
		m_SteamGetTechList.Cancelled.Subscribe(SteamLoading_OnComplete);
		SteamDownloadData data = SteamDownloadData.Create(SteamItemCategory.Techs);
		m_SteamGetTechList.Execute(data);
	}

	private void SteamLoading_OnComplete(SteamDownloadData data)
	{
		m_SteamItems = data;
		if (data.HasAnyItems)
		{
			m_SteamFSM.ChangeState(SteamStates.SteamPopulate);
		}
		else
		{
			m_SteamFSM.ChangeState(SteamStates.SteamComplete);
		}
	}

	private void SteamLoading_Exit()
	{
		m_SteamGetTechList.Completed.Unsubscribe(SteamLoading_OnComplete);
		m_SteamGetTechList.Cancelled.Unsubscribe(SteamLoading_OnComplete);
	}

	private void SteamPopulate_Enter()
	{
		if (!m_SteamItems.HasAnyItems)
		{
			return;
		}
		for (int i = 0; i < m_SteamItems.m_Items.Count; i++)
		{
			SteamDownloadItemData data = m_SteamItems.m_Items[i];
			CommandOperation<SteamDownloadItemData> commandOperation = new CommandOperation<SteamDownloadItemData>();
			commandOperation.AddConditional(SteamConditions.CheckItemNeedsDownload, new SteamItemDownloadCommand());
			commandOperation.AddConditional(SteamConditions.CheckWaitingForDownload, new SteamItemWaitForDownloadCommand());
			commandOperation.Add(new SteamItemGetDataFile());
			commandOperation.Add(new SteamLoadMetaDataCommand());
			commandOperation.Add(new SteamLoadPreviewImageCommand());
			commandOperation.Add(new SteamItemParseSnapshot());
			commandOperation.Completed.Subscribe(delegate(SteamDownloadItemData steamDownloadItemData)
			{
				m_SnapshotsSteam.AddSnapshot(steamDownloadItemData.m_Snaphsot);
			});
			commandOperation.Execute(data);
		}
	}

	private void SteamPopulate_Update()
	{
		LoadOneTechPerFrameIntoSprites(m_SnapshotsSteam);
	}

	private void SteamComplete_Enter()
	{
		m_FinishedLoadingCaptures = true;
		if (!m_SteamItems.HasAnyItems)
		{
			if (m_SteamButton != null)
			{
				m_SteamButton.gameObject.SetActive(value: true);
			}
			if (m_BottomPanel != null)
			{
				m_BottomPanel.gameObject.SetActive(value: false);
			}
		}
	}

	private void Steam_Exit()
	{
		if (m_SteamButton != null)
		{
			m_SteamButton.gameObject.SetActive(value: false);
		}
		if (m_BottomPanel != null)
		{
			m_BottomPanel.gameObject.SetActive(value: true);
		}
		m_SteamFSM.ChangeState(SteamStates.SteamIdle);
	}

	private bool LoadOneTechPerFrameIntoSprites<TSnap>(SnapshotCollection<TSnap> capCollection) where TSnap : Snapshot, new()
	{
		while (m_NumCapturesAdded < capCollection.Snapshots.Count)
		{
			Snapshot snapshot = capCollection.Snapshots[m_NumCapturesAdded];
			m_NumCapturesAdded++;
			string loadVehicleModeRestriction = Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleModeRestriction;
			string loadVehicleSubModeRestriction = Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleSubModeRestriction;
			string loadVehicleUserDataRestriction = Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleUserDataRestriction;
			if (capCollection.CheckConverted(snapshot, loadVehicleModeRestriction, loadVehicleSubModeRestriction, loadVehicleUserDataRestriction))
			{
				AddListElementForLoadedTech(snapshot);
				return true;
			}
		}
		return false;
	}

	private bool DecodeTweet(SnapshotCollectionTwitter snapshotCollection)
	{
		d.Assert(m_FetchedTweets != null, "m_FetchedTweets is null, has the Twitter API been called correctly?");
		lock (m_FetchedTweets.m_Lock)
		{
			if (m_NumTweetsDecoded < m_FetchedTweets.m_Links.Count)
			{
				TwitterAPI.TweetWithMedia tweetData = m_FetchedTweets.m_Links[m_NumTweetsDecoded];
				Texture2D texture2D = new Texture2D(4, 4);
				texture2D.LoadImage(tweetData.ImageArray);
				snapshotCollection.TryAddFromImage(texture2D, tweetData, out var _);
				m_NumTweetsDecoded++;
				return true;
			}
		}
		return false;
	}

	private static bool CanBeMadeWithInventory(TechData techData, IInventory<BlockTypes> inventory)
	{
		bool result = true;
		int count = techData.m_BlockSpecs.Count;
		Dictionary<int, int> dictionary = new Dictionary<int, int>(count);
		for (int i = 0; i < count; i++)
		{
			TankPreset.BlockSpec blockSpec = techData.m_BlockSpecs[i];
			int blockType = (int)blockSpec.GetBlockType();
			if (!dictionary.TryGetValue(blockType, out var value))
			{
				value = inventory.GetQuantity(blockSpec.GetBlockType());
				dictionary.Add(blockType, value);
			}
			switch (value)
			{
			case 0:
				break;
			default:
				dictionary[blockType] = value - 1;
				continue;
			case -1:
				continue;
			}
			result = false;
			break;
		}
		return result;
	}

	private void AddListElementForLoadedTech(Snapshot capture)
	{
		m_SelectedTechBlockCache.RecordBlockDataAndValidate(capture.techData, new InventoryMetaData(m_BlockInventory));
		if ((m_DisplayOnlyAvailable && m_SelectedTechBlockCache.UnavailableSwap) || (!m_AllowPartials && m_SelectedTechBlockCache.HasMissingBlocksSwap))
		{
			return;
		}
		UIPreset uIPreset = m_ImagePrefab.Spawn();
		uIPreset.SetData(capture, m_SelectedTechBlockCache.UnavailableSwap);
		uIPreset.transform.SetParent(m_ContentLayout, worldPositionStays: false);
		uIPreset.transform.localScale = Vector3.one;
		Toggle componentInChildren = uIPreset.gameObject.GetComponentInChildren<Toggle>();
		componentInChildren.isOn = false;
		componentInChildren.group = m_ButtonToggleGroup;
		TechUIElement item = new TechUIElement
		{
			uiPreset = uIPreset,
			toggleButton = componentInChildren,
			clickHandler = delegate(bool toggledOn)
			{
				if (toggledOn)
				{
					OnTechPresetButtonClicked(capture);
				}
			}
		};
		componentInChildren.onValueChanged.AddListener(item.clickHandler);
		m_MyImages.Add(item);
	}

	private void RefreshListAfterDelete()
	{
		ClearTab();
		Local_Enter();
		EventSystem.current.SetSelectedGameObject(base.gameObject);
	}

	private void UpdateSelectButtonState()
	{
		if (m_SelectButton != null)
		{
			m_SelectButton.interactable = CanAcceptCurrentSelection();
		}
	}

	private void UpdateLoadingInfoText()
	{
		if (!(m_LoadingInfo != null))
		{
			return;
		}
		if (m_MyImages == null || m_MyImages.Count == 0)
		{
			if (!m_LoadingInfo.gameObject.activeSelf)
			{
				m_LoadingInfo.gameObject.SetActive(value: true);
			}
			if (CurrentTab == TechLocations.Twitter)
			{
				if (m_FinishedLoadingCaptures || m_TwitterLoginFailed)
				{
					m_LoadingInfo.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 41);
				}
				else
				{
					m_LoadingInfo.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 40);
				}
			}
			else if (CurrentTab == TechLocations.Steam)
			{
				if (m_FinishedLoadingCaptures)
				{
					m_LoadingInfo.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 5);
				}
				else
				{
					m_LoadingInfo.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 39);
				}
			}
			else
			{
				m_LoadingInfo.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 41);
			}
		}
		else if (m_LoadingInfo.gameObject.activeSelf)
		{
			m_LoadingInfo.gameObject.SetActive(value: false);
		}
	}

	private void ClearTab()
	{
		if (m_MyImages != null)
		{
			for (int num = m_MyImages.Count - 1; num >= 0; num--)
			{
				m_MyImages[num].toggleButton.group = null;
				m_MyImages[num].toggleButton.onValueChanged.RemoveListener(m_MyImages[num].clickHandler);
				m_MyImages[num].uiPreset.transform.SetParent(null, worldPositionStays: false);
				m_MyImages[num].uiPreset.Recycle();
				m_MyImages.RemoveAt(num);
			}
			m_MyImages.Clear();
		}
		UpdateLoadingInfoText();
	}

	private bool CanAcceptCurrentSelection()
	{
		TechData techData = ((m_SelectedTech != null) ? m_SelectedTech.techData : null);
		if (CanSelectTechCallback != null && !CanSelectTechCallback(techData, m_SelectedTechCost))
		{
			return false;
		}
		if (techData == null)
		{
			return false;
		}
		if (m_SelectedTechBlockCache.m_Tech != techData)
		{
			m_SelectedTechBlockCache.RecordBlockDataAndValidate(m_SelectedTech.techData, new InventoryMetaData(m_BlockInventory));
		}
		if (m_SelectedTechBlockCache.UnavailableSwap)
		{
			return false;
		}
		return true;
	}

	private void Update()
	{
		UpdateLoadingInfoText();
	}

	private void OnTechPresetButtonClicked(Snapshot capture)
	{
		bool flag = false;
		if (m_DoubleClickSelectsItem)
		{
			bool doubleClickConditionPassed = capture == m_SelectedTech;
			flag = m_DoubleClickListener.WasClickEventDoubleClick(doubleClickConditionPassed);
		}
		if (flag)
		{
			if (CanAcceptCurrentSelection())
			{
				SelectTech();
			}
		}
		else
		{
			HighlightTech(capture);
		}
	}

	private void HighlightTech(Snapshot capture)
	{
		m_SelectedTech = capture;
		if (m_SelectedTech != null)
		{
			TechData techData = capture.techData;
			if (m_SelectedTechName != null)
			{
				m_SelectedTechName.text = techData.Name;
			}
			if (m_SelectedTechCreator != null)
			{
				m_SelectedTechCreator.text = capture.creator;
			}
			m_SelectedTechCost = Singleton.Manager<RecipeManager>.inst.GetTechPrice(techData, silentFail: true);
			if (m_SelectedTechPrice != null)
			{
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Purchasing, 2);
				m_SelectedTechPrice.text = string.Format(localisedString, m_SelectedTechCost);
			}
			if (OnListSelectEvent != null)
			{
				OnListSelectEvent(m_SelectedTech, m_SelectedTechCost);
			}
			m_SelectedTechBlockCache.RecordBlockDataAndValidate(techData, new InventoryMetaData(m_BlockInventory));
			if (m_UndiscoveredBlocks != null)
			{
				m_UndiscoveredBlocks.SetActive(m_SelectedTechBlockCache.UnavailableSwap);
				if (m_SelectedTechBlockCache.UnavailableSwap)
				{
					m_UndiscoveredBlocks.PopulateItems(m_SelectedTechBlockCache);
				}
			}
		}
		else
		{
			if (m_SelectedTechName != null)
			{
				m_SelectedTechName.text = string.Empty;
			}
			if (m_SelectedTechCreator != null)
			{
				m_SelectedTechCreator.text = string.Empty;
			}
			m_SelectedTechCost = 0;
			if (m_SelectedTechPrice != null)
			{
				m_SelectedTechPrice.text = string.Empty;
			}
			if (m_UndiscoveredBlocks != null)
			{
				m_UndiscoveredBlocks.SetActive(isActive: false);
			}
		}
		UpdateSelectButtonState();
	}

	private void SelectTech()
	{
		OnSelectionAcceptedEvent.Send(m_SelectedTech);
	}

	private void OnDiskCollectionChanged(SnapshotCollection<SnapshotDisk> collection)
	{
	}

	private void OnTabChanged(TechLocations currentTab)
	{
		m_PreviousTab = m_FSM.LastState;
	}

	public void OnSubmit(BaseEventData eventData)
	{
		if (base.gameObject.activeSelf)
		{
			if (CanAcceptCurrentSelection())
			{
				SelectTech();
			}
			eventData.Use();
		}
	}

	public void OnCancel(BaseEventData eventData)
	{
		if (base.gameObject.activeSelf)
		{
			if (m_UITechLoaderHUD != null)
			{
				m_UITechLoaderHUD.CloseTechLoader();
			}
			eventData.Use();
		}
	}

	public void OnMove(AxisEventData eventData)
	{
		if (base.gameObject.activeSelf)
		{
			MoveSelection(eventData.moveVector);
			eventData.Use();
		}
	}

	public void OnUIExtraButton2(BaseEventData eventData)
	{
		if (base.gameObject.activeSelf && m_SelectedTech != null)
		{
			Singleton.Manager<ManSnapshots>.inst.DeleteSnapshotRender(m_SelectedTech, RefreshListAfterDelete);
			eventData.Use();
		}
	}

	private void UpdateJoypadUI()
	{
		bool flag = Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
		GameObject[] hiddenUIOnConsole = m_HiddenUIOnConsole;
		for (int i = 0; i < hiddenUIOnConsole.Length; i++)
		{
			hiddenUIOnConsole[i].gameObject.SetActive(!flag);
		}
		if (m_UITabsPanel != null)
		{
			m_UITabsPanel.gameObject.SetActive(!flag);
		}
	}

	private void MoveSelection(Vector2 dir)
	{
		TechUIElement fromUI = FindSelectedImage(m_SelectedTech);
		if (fromUI.Equals(default(TechUIElement)))
		{
			TrySelectFirstItem();
		}
		else
		{
			HighLightButton(GetNextImageInDirection(fromUI, dir).toggleButton);
		}
	}

	private void HighLightButton(Toggle btn)
	{
		if (!(m_PreviousSnapShotButton == btn))
		{
			UIHelpers.VertScrollToItem(m_GridScrollRect.content, btn.GetComponent<RectTransform>(), m_GridScrollRect.viewport.rect.height);
			btn.isOn = true;
			m_PreviousSnapShotButton = btn;
		}
	}

	private void TrySelectFirstItem()
	{
		if (m_MyImages.Count > 0)
		{
			HighLightButton(m_MyImages[0].toggleButton);
		}
	}

	private TechUIElement FindSelectedImage(Snapshot m_Tech)
	{
		foreach (TechUIElement myImage in m_MyImages)
		{
			if (myImage.uiPreset.GetData() == m_Tech)
			{
				return myImage;
			}
		}
		return default(TechUIElement);
	}

	private TechUIElement GetNextImageInDirection(TechUIElement fromUI, Vector2 dir)
	{
		TechUIElement result = fromUI;
		float num = float.MaxValue;
		Vector2 rhs = new Vector2(dir.y, 0f - dir.x);
		foreach (TechUIElement myImage in m_MyImages)
		{
			Vector3 position = fromUI.uiPreset.GetComponent<RectTransform>().position;
			Vector3 vector = myImage.uiPreset.GetComponent<RectTransform>().position - position;
			float num2 = Vector2.Dot(vector, dir);
			float num3 = Mathf.Abs(Vector2.Dot(vector, rhs));
			if (num2 > 0f)
			{
				float num4 = num2 + num3 * 2f;
				if (num4 < num)
				{
					result = myImage;
					num = num4;
				}
			}
		}
		return result;
	}

	private void OnPool()
	{
		m_MyImages = new List<TechUIElement>(10);
		m_ImagePrefab.CreatePool(10);
		if (m_SelectButton != null)
		{
			m_SelectButton.onClick.AddListener(SelectTech);
		}
		m_DoubleClickListener = new DoubleClickListener();
		m_ButtonToggleGroup = m_ContentLayout.GetComponent<ToggleGroup>();
		m_FSM = StateMachine<TechLocations>.Initialize(this, TechLocations.Null);
		m_FSM.Changed += OnTabChanged;
		m_TwitterFSM = StateMachine<TwitterStates>.Initialize(this, TwitterStates.TwitterIdle);
		m_SteamFSM = StateMachine<SteamStates>.Initialize(this, SteamStates.SteamIdle);
		if (m_UITabsPanel != null)
		{
			m_UITabsPanel.GetComponentsInChildren(includeInactive: true, m_UITabHelpers);
		}
	}

	private void OnSpawn()
	{
		base.gameObject.SetActive(value: false);
		if (m_SteamButton != null)
		{
			m_SteamButton.gameObject.SetActive(value: false);
		}
		if (m_BottomPanel != null)
		{
			m_BottomPanel.gameObject.SetActive(value: true);
		}
	}

	private void OnRecycle()
	{
		OnListSelectEvent = null;
		OnSelectionAcceptedEvent.Clear();
		CanSelectTechCallback = null;
		m_PreviousTab = TechLocations.Null;
		m_BlockInventory = null;
		m_SelectedTech = null;
	}
}
