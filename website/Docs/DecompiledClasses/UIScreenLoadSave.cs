#define UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using Payload.UI.Commands;
using Payload.UI.Commands.Steam;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScreenLoadSave : UIScreen, IMoveHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler, IUIExtraButtonHandler2, ITabChangePrevHandler, ITabChangeNextHandler
{
	[SerializeField]
	private RectTransform m_ContentHolder;

	[SerializeField]
	private ScrollRect m_ScrollRect;

	[SerializeField]
	private UISave m_SavePrefab;

	[SerializeField]
	private RectTransform m_LoadingGraphic;

	[SerializeField]
	private RectTransform m_NoSavesGraphic;

	[SerializeField]
	private UnityEvent m_SaveDoubleClickAction;

	[SerializeField]
	private Text m_SubTitle;

	[SerializeField]
	private ToggleGroup m_Categories;

	[SerializeField]
	[EnumArray(typeof(ManGameMode.GameType))]
	private Toggle[] m_CatergoryTypeButtons = new Toggle[1];

	[SerializeField]
	private ManGameMode.GameType[] m_CategoryTypeOrder;

	[SerializeField]
	private Button m_PlayButton;

	[SerializeField]
	private Button m_DeleteButton;

	[SerializeField]
	private UIStartMode m_UIStartMode;

	[SerializeField]
	private GameObject[] m_HiddenUIOnConsole;

	[SerializeField]
	private GameObject m_UploadButton;

	[SerializeField]
	private GameObject m_WorkshopButton;

	private List<UISave> m_SaveSlotUIElements = new List<UISave>();

	private List<ManSaveGame.SaveFileSlot> m_SavesToLoad;

	private ManGameMode.GameType m_GameTypeOfSaves;

	private int m_SavesToLoadProcessIndex;

	private int m_SavesLoadedProcessIndex;

	private bool m_BusyLoadingSaves;

	private ToggleGroup m_ToggleGroup;

	private UISave m_ActiveSave;

	private int m_CurrentSelectedIndex;

	private bool m_AttemptingToCreateLobby;

	private DoubleClickListener m_DoubleClickListener;

	private UIAutoScrollItemController m_AutoScrollController;

	private CommandOperation<SteamUploadData> m_SteamUploadOp;

	private CommandOperation<SteamDownloadData> m_SteamDownloadOp;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if (Singleton.Manager<ManGameMode>.inst.GetIsInPlayableMode())
		{
			Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, new Localisation.GlyphInfo[1]
			{
				new Localisation.GlyphInfo(41)
			});
			Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, new Localisation.GlyphInfo[1]
			{
				new Localisation.GlyphInfo(42)
			});
			ManGameMode.GameType currentGameType = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType();
			ShowSavesForType(currentGameType);
			m_SubTitle.gameObject.SetActive(value: true);
			m_Categories.gameObject.SetActive(value: false);
			m_SubTitle.text = GetGameTypeName(currentGameType);
		}
		else
		{
			if (m_GameTypeOfSaves == ManGameMode.GameType.Attract)
			{
				ManGameMode.GameType lastUsedSaveType = Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_LastUsedSaveType;
				m_GameTypeOfSaves = ((lastUsedSaveType == ManGameMode.GameType.Attract) ? ManGameMode.GameType.MainGame : lastUsedSaveType);
			}
			ShowSavesForType(m_GameTypeOfSaves);
			m_SubTitle.gameObject.SetActive(value: false);
			m_Categories.gameObject.SetActive(value: true);
			m_SubTitle.text = string.Empty;
		}
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			ToggleJoypadUI(on: true);
		}
		else
		{
			ToggleJoypadUI(on: false);
		}
		if (SKU.ConsoleUI)
		{
			m_ExitButton.gameObject.SetActive(value: false);
		}
		if (!SKU.SupportsMultiplayer || !Singleton.Manager<ManNetwork>.inst.IsMultiplayerAvailable())
		{
			m_CatergoryTypeButtons[11]?.gameObject?.SetActive(value: false);
			m_CatergoryTypeButtons[12]?.gameObject?.SetActive(value: false);
		}
		Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(base.gameObject);
	}

	private void ToggleJoypadUI(bool on)
	{
		GameObject[] hiddenUIOnConsole = m_HiddenUIOnConsole;
		for (int i = 0; i < hiddenUIOnConsole.Length; i++)
		{
			hiddenUIOnConsole[i].gameObject.SetActive(!on);
		}
	}

	public override void Hide()
	{
		Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(base.gameObject);
		ClearDisplayedSaves();
		base.Hide();
	}

	private void Awake()
	{
		m_ToggleGroup = m_ContentHolder.GetComponent<ToggleGroup>();
		m_DoubleClickListener = new DoubleClickListener();
		m_AutoScrollController = m_ContentHolder.GetComponentInParents<UIAutoScrollItemController>();
		for (int i = 0; i < m_CatergoryTypeButtons.Length; i++)
		{
			if (!(m_CatergoryTypeButtons[i] != null))
			{
				continue;
			}
			ManGameMode.GameType gameType = (ManGameMode.GameType)i;
			if (gameType == ManGameMode.GameType.RaD && !Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(ManDLC.DLCType.RandD))
			{
				m_CatergoryTypeButtons[i].interactable = false;
				continue;
			}
			m_CatergoryTypeButtons[i].onValueChanged.AddListener(delegate(bool toggledOn)
			{
				if (toggledOn && gameType != m_GameTypeOfSaves)
				{
					ShowSavesForType(gameType);
				}
			});
		}
		Singleton.Manager<ManDLC>.inst.OnDLCChanged.Subscribe(OnDLCChanged);
	}

	private void OnSpawn()
	{
		if (Singleton.Manager<ManSteamworks>.inst.Workshop.Enabled)
		{
			if (m_SteamUploadOp == null)
			{
				m_SteamUploadOp = new CommandOperation<SteamUploadData>();
				SteamOptionsCommand command = new SteamOptionsCommand();
				SteamCreateRenderCommand command2 = new SteamCreateRenderCommand();
				PackageCommand command3 = new PackageCommand();
				SteamCreateItemCommand command4 = new SteamCreateItemCommand();
				SteamSubmitItemCommand command5 = new SteamSubmitItemCommand();
				SteamUploadFeedbackCommand command6 = new SteamUploadFeedbackCommand();
				SteamGoToItemCommand steamGoToItemCommand = new SteamGoToItemCommand();
				m_SteamUploadOp.Add(command);
				m_SteamUploadOp.Add(command2);
				m_SteamUploadOp.Add(command3);
				m_SteamUploadOp.Add(command4);
				m_SteamUploadOp.Add(command5);
				m_SteamUploadOp.Add(command6);
				m_SteamUploadOp.AddConditional(SteamConditions.CheckGoToItem, steamGoToItemCommand);
				m_SteamUploadOp.Completed.Subscribe(OnUploadCompleted);
				m_SteamUploadOp.Cancelled.Subscribe(OnUploadCancelled);
			}
			if (m_SteamDownloadOp == null)
			{
				m_SteamDownloadOp = new CommandOperation<SteamDownloadData>();
				SteamCreateQueryCommand command7 = new SteamCreateQueryCommand();
				m_SteamDownloadOp.Add(command7);
				m_SteamDownloadOp.Completed.Subscribe(OnSteamSavesFetchComplete);
			}
		}
	}

	public void OnSteamUpload()
	{
		if (!Singleton.Manager<ManSteamworks>.inst.Inited)
		{
			d.LogError("UIScreenLoadSave.OnSteamUpload - Steam is not initialised");
		}
		else if (!m_SteamUploadOp.IsRunning && !m_SaveSlotUIElements[m_CurrentSelectedIndex].SaveInfo.IsWorkshopSave)
		{
			string saveName = m_SaveSlotUIElements[m_CurrentSelectedIndex].SaveInfo.m_SaveName;
			SteamUploadData data = SteamUploadData.Create(SteamItemCategory.Saves, saveName);
			data.m_SaveInfo = m_SaveSlotUIElements[m_CurrentSelectedIndex].SaveInfo;
			data.m_FileInfoTemp = new FileInfo(m_SaveSlotUIElements[m_CurrentSelectedIndex].SaveInfo.FullFilePath);
			m_SteamUploadOp.Execute(data);
		}
	}

	public void GetSteamWorkshopSaves()
	{
		if (Singleton.Manager<ManSteamworks>.inst.Workshop.Enabled)
		{
			SteamDownloadData data = SteamDownloadData.Create(SteamItemCategory.Saves);
			m_SteamDownloadOp.Execute(data);
		}
	}

	private void OnSteamSavesFetchComplete(SteamDownloadData data)
	{
		if (!data.HasAnyItems)
		{
			return;
		}
		for (int i = 0; i < data.m_Items.Count; i++)
		{
			SteamDownloadItemData data2 = data.m_Items[i];
			CommandOperation<SteamDownloadItemData> commandOperation = new CommandOperation<SteamDownloadItemData>();
			commandOperation.AddConditional(SteamConditions.CheckItemNeedsDownload, new SteamItemDownloadCommand());
			commandOperation.AddConditional(SteamConditions.CheckWaitingForDownload, new SteamItemWaitForDownloadCommand());
			commandOperation.Add(new SteamItemGetDataFile());
			commandOperation.Add(new SteamLoadMetaDataCommand());
			commandOperation.Add(new SteamLoadPreviewImageCommand());
			commandOperation.Completed.Subscribe(delegate(SteamDownloadItemData steamDownloadItemData)
			{
				ManSaveGame.SaveInfo saveInfo = ManSaveGame.LoadWorkshopSaveDataInfo(steamDownloadItemData.m_FileInfo, m_GameTypeOfSaves);
				if (saveInfo != null)
				{
					SetupSaveDataComponent(saveInfo.m_SaveName, isEmptySlot: false, saveInfo);
					ShowNoSavesGraphic(show: false);
				}
			});
			commandOperation.Execute(data2);
		}
	}

	public void GoToSteamWorkshop()
	{
		Singleton.Manager<ManSteamworks>.inst.OpenOverlayURL("http://steamcommunity.com/app/285920/workshop/");
	}

	private void OnUploadCompleted(SteamUploadData data)
	{
	}

	private void OnUploadCancelled(SteamUploadData data)
	{
		if (!data.m_CancelledByUser)
		{
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 42);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			uIScreenNotifications.Set(localisedString, delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			}, localisedString2);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
		}
	}

	private void Update()
	{
		if (m_BusyLoadingSaves)
		{
			if (m_SavesToLoad != null && m_SavesToLoad.Count > 0 && m_SavesToLoadProcessIndex < m_SavesToLoad.Count)
			{
				ShowNoSavesGraphic(show: false);
				ManSaveGame.SaveFileSlot save = m_SavesToLoad[m_SavesToLoadProcessIndex];
				if (save.isEmptySlot)
				{
					SetupSaveDataComponent(save.name, save.isEmptySlot, null);
				}
				else
				{
					ManSaveGame.LoadSaveDataInfoAsync(m_GameTypeOfSaves, save.name, delegate(ManSaveGame.SaveInfo saveInfo)
					{
						SetupSaveDataComponent(save.name, save.isEmptySlot, saveInfo);
					});
				}
				m_SavesToLoadProcessIndex++;
			}
			else if (m_SavesLoadedProcessIndex >= m_SavesToLoadProcessIndex)
			{
				if (m_SavesToLoadProcessIndex != m_SavesLoadedProcessIndex)
				{
					string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 22);
					string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 17);
					string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 18);
					localisedString2 = UIHyperlink.ConvertLinkToTMProLinkCode(localisedString2, localisedString3);
					localisedString = string.Format(localisedString, localisedString2);
					d.LogError("Failed to load one or more save infos in UIScreenLoadSave");
					Singleton.Manager<ManUI>.inst.ShowErrorPopup(localisedString);
				}
				ShowNoSavesGraphic(m_SaveSlotUIElements.Count == 0);
				m_BusyLoadingSaves = false;
				m_SavesToLoad = null;
				ShowLoadingGraphic(show: false);
				SetSavesInViewportActive();
				GetSteamWorkshopSaves();
			}
		}
		else
		{
			ShowNoSavesGraphic(m_SaveSlotUIElements.Count == 0);
		}
		if (!(m_ActiveSave == null) || m_SaveSlotUIElements.Count <= 0 || m_BusyLoadingSaves)
		{
			return;
		}
		for (int num = 0; num < m_SaveSlotUIElements.Count; num++)
		{
			if (m_SaveSlotUIElements[num].SaveInfo != null && m_SaveSlotUIElements[num].SaveInfo.CanLoadSave())
			{
				m_SaveSlotUIElements[num].SetSelected();
				break;
			}
		}
	}

	private void SetSavesInViewportActive()
	{
		foreach (UISave saveSlotUIElement in m_SaveSlotUIElements)
		{
			saveSlotUIElement.SetActiveIfInViewportRect(m_ScrollRect.viewport.WorldRect());
		}
	}

	private void SetupSaveDataComponent(string saveName, bool isEmptySlot, ManSaveGame.SaveInfo saveInfo)
	{
		if ((saveInfo != null && saveInfo.IsFormatSupported() && saveInfo.m_GameType == m_GameTypeOfSaves) || isEmptySlot)
		{
			UISave uISave = m_SavePrefab.Spawn(Vector3.zero);
			uISave.GetComponent<RectTransform>().SetParent(m_ContentHolder, worldPositionStays: false);
			if (m_ToggleGroup != null)
			{
				AddToToggleGroup(uISave);
			}
			uISave.Setup(saveName, saveInfo, isEmptySlot);
			if (isEmptySlot)
			{
				uISave.SetSelectable(selectable: false);
			}
			m_SaveSlotUIElements.Add(uISave);
		}
		else
		{
			d.LogErrorFormat("Failed to Setup SaveData Component for save: {0}, save data null? {1}, save format supported? {2}, save game type matches? {3}", saveName, saveInfo == null, (saveInfo != null) ? saveInfo.IsFormatSupported().ToString() : "N/A", (saveInfo != null) ? (saveInfo.m_GameType == m_GameTypeOfSaves).ToString() : "N/A");
		}
		m_SavesLoadedProcessIndex++;
	}

	public void UI_OnBarScrolled(Vector2 _)
	{
		SetSavesInViewportActive();
	}

	private void OnChangeSavesList()
	{
		if (m_ToggleGroup != null)
		{
			m_ToggleGroup.SetAllTogglesOff();
		}
		m_ActiveSave = null;
		m_CurrentSelectedIndex = -1;
		UpdatePlayButtons();
	}

	private void AddToToggleGroup(UISave save)
	{
		Toggle toggleComponent = save.ToggleComponent;
		d.Assert(toggleComponent != null, "UIScreenLoadSave " + base.name + " has toggle group but UISave elements do not contain a Toggle ccomponent!");
		if (!(toggleComponent != null))
		{
			return;
		}
		toggleComponent.isOn = false;
		toggleComponent.group = m_ToggleGroup;
		toggleComponent.onValueChanged.AddListener(delegate(bool selected)
		{
			if (selected)
			{
				OnSaveSelected(save);
			}
		});
	}

	private void RemoveFromToggleGroup(UISave save)
	{
		if (m_ToggleGroup != null)
		{
			Toggle toggleComponent = save.ToggleComponent;
			toggleComponent.group = null;
			toggleComponent.onValueChanged.RemoveAllListeners();
		}
	}

	private void OnSaveSelected(UISave selectedSave)
	{
		bool flag = false;
		if (m_SaveDoubleClickAction != null)
		{
			bool doubleClickConditionPassed = m_ActiveSave != null && m_ActiveSave == selectedSave;
			flag = m_DoubleClickListener.WasClickEventDoubleClick(doubleClickConditionPassed);
		}
		if (flag && !SKU.ConsoleUI)
		{
			m_SaveDoubleClickAction.Invoke();
			return;
		}
		m_ActiveSave = selectedSave;
		UpdateCurrentSelectedIndex();
		UpdatePlayButtons();
		if (!EventSystem.current.alreadySelecting && !Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.NotificationScreen))
		{
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}
		if (m_AutoScrollController != null && selectedSave != null)
		{
			m_AutoScrollController.ScrollToItem(selectedSave.GetComponent<RectTransform>());
		}
	}

	public void SetSavedGameToLoad()
	{
		d.Assert(m_ToggleGroup != null && m_ToggleGroup.AnyTogglesOn() && m_ActiveSave != null, "UIScreenLoadSave.SetSavedGameToLoad() - Trying to load a save while none is selected!");
		if (m_ActiveSave != null)
		{
			m_ActiveSave.SetupSaveGameToLoad();
		}
	}

	public void PromptDeleteSavedGame()
	{
		d.Assert(m_ToggleGroup != null && m_ToggleGroup.AnyTogglesOn() && m_ActiveSave != null, "UIScreenLoadSave.PromptDeleteSavedGame() - Trying to delete a save while none is selected!");
		if (m_ActiveSave != null)
		{
			m_ActiveSave.AskDelete();
		}
	}

	public void ShowSavesForType(ManGameMode.GameType gameType)
	{
		ClearDisplayedSaves();
		LoadUserSaveGames(gameType);
		for (int i = 0; i < m_CatergoryTypeButtons.Length; i++)
		{
			Toggle toggle = m_CatergoryTypeButtons[i];
			if (toggle != null)
			{
				toggle.isOn = i == (int)gameType;
			}
		}
	}

	private void ClearDisplayedSaves()
	{
		for (int num = m_SaveSlotUIElements.Count - 1; num >= 0; num--)
		{
			if (m_ToggleGroup != null)
			{
				m_ToggleGroup.SetAllTogglesOff();
			}
			RemoveFromToggleGroup(m_SaveSlotUIElements[num]);
			m_SaveSlotUIElements[num].transform.SetParent(null, worldPositionStays: false);
			m_SaveSlotUIElements[num].Recycle();
			m_SaveSlotUIElements.RemoveAt(num);
		}
	}

	private void LoadUserSaveGames(ManGameMode.GameType gameType)
	{
		m_GameTypeOfSaves = gameType;
		m_SavesToLoad = ManSaveGame.GetSavesInFolder(gameType, includeUnusedSlots: true);
		if (m_SavesToLoad != null)
		{
			m_SavesToLoad.Sort(SaveSortComparer);
		}
		m_SavesToLoadProcessIndex = 0;
		m_SavesLoadedProcessIndex = 0;
		m_BusyLoadingSaves = m_SavesToLoad != null && m_SavesToLoad.Count > 0;
		if (!m_BusyLoadingSaves)
		{
			GetSteamWorkshopSaves();
		}
		UpdateWorkshopButtons();
		ShowLoadingGraphic(m_BusyLoadingSaves);
		ShowNoSavesGraphic(!m_BusyLoadingSaves);
		OnChangeSavesList();
	}

	public static int SaveSortComparer(ManSaveGame.SaveFileSlot fileA, ManSaveGame.SaveFileSlot fileB)
	{
		int num = fileB.lastWriteTime.CompareTo(fileA.lastWriteTime);
		if (num == 0)
		{
			num = fileA.name.CompareTo(fileB.name);
		}
		return num;
	}

	private void ShowLoadingGraphic(bool show)
	{
		if (m_LoadingGraphic != null)
		{
			m_LoadingGraphic.gameObject.SetActive(show);
		}
	}

	private void ShowNoSavesGraphic(bool show)
	{
		if (m_NoSavesGraphic != null)
		{
			m_NoSavesGraphic.gameObject.SetActive(show);
		}
	}

	private string GetGameTypeName(ManGameMode.GameType gameType)
	{
		string result = string.Empty;
		for (int i = 0; i < m_CatergoryTypeButtons.Length; i++)
		{
			if (i == (int)gameType && m_CatergoryTypeButtons[i] != null)
			{
				UILocalisedText componentInChildren = m_CatergoryTypeButtons[i].GetComponentInChildren<UILocalisedText>();
				if (componentInChildren != null)
				{
					result = componentInChildren.m_String.Value;
				}
			}
		}
		return result;
	}

	private void UpdateCurrentSelectedIndex()
	{
		m_CurrentSelectedIndex = -1;
		if (m_ActiveSave != null)
		{
			m_CurrentSelectedIndex = m_SaveSlotUIElements.IndexOf(m_ActiveSave);
			d.Assert(m_CurrentSelectedIndex != -1, "UIScreenLoadSave.GetCurrentSelectedIndex - Failed to find m_ActiveSave in m_Saves !?");
		}
	}

	private void UpdatePlayButtons()
	{
		bool flag = m_ActiveSave != null;
		if (m_PlayButton != null)
		{
			m_PlayButton.interactable = flag;
		}
		if (m_DeleteButton != null)
		{
			m_DeleteButton.interactable = flag && m_ActiveSave.SaveInfo != null && !m_ActiveSave.SaveInfo.IsWorkshopSave;
		}
		UpdateWorkshopButtons();
	}

	private void UpdateWorkshopButtons()
	{
		bool flag = SKU.IsSteam && Singleton.Manager<ManSteamworks>.inst.Inited && Singleton.Manager<ManSteamworks>.inst.Workshop.Enabled;
		if (m_UploadButton != null)
		{
			bool flag2 = m_GameTypeOfSaves.IsCampaign();
			bool flag3 = m_CurrentSelectedIndex >= 0 && m_CurrentSelectedIndex < m_SaveSlotUIElements.Count && m_SaveSlotUIElements[m_CurrentSelectedIndex].SaveInfo != null && !m_SaveSlotUIElements[m_CurrentSelectedIndex].SaveInfo.IsWorkshopSave;
			m_UploadButton.SetActive(flag && !flag2 && flag3 && !m_BusyLoadingSaves);
		}
		if (m_WorkshopButton != null)
		{
			m_WorkshopButton.SetActive(flag);
		}
	}

	public void OnMove(AxisEventData eventData)
	{
		if (m_SaveSlotUIElements.Count == 0)
		{
			return;
		}
		int num = m_CurrentSelectedIndex;
		if (eventData.moveDir == MoveDirection.Up)
		{
			for (int num2 = m_CurrentSelectedIndex - 1; num2 >= 0; num2--)
			{
				if (m_SaveSlotUIElements[num2].SaveInfo != null)
				{
					num = num2;
					break;
				}
			}
		}
		else if (eventData.moveDir == MoveDirection.Down)
		{
			for (int i = m_CurrentSelectedIndex + 1; i < m_SaveSlotUIElements.Count; i++)
			{
				if (m_SaveSlotUIElements[i].SaveInfo != null)
				{
					num = i;
					break;
				}
			}
		}
		if (num >= 0 && num != m_CurrentSelectedIndex && m_SaveSlotUIElements[num].SaveInfo != null)
		{
			m_SaveSlotUIElements[num].SetSelected();
		}
		eventData.Use();
	}

	private void LoadAction()
	{
		SetSavedGameToLoad();
		bool flag = false;
		MultiplayerModeType gameType = MultiplayerModeType.Deathmatch;
		switch (m_ActiveSave.GetSaveGameType())
		{
		case ManGameMode.GameType.CoOpCreative:
			flag = true;
			gameType = MultiplayerModeType.CoOpCreative;
			break;
		case ManGameMode.GameType.CoOpCampaign:
			flag = true;
			gameType = MultiplayerModeType.CoOpCampaign;
			break;
		}
		if (flag)
		{
			if (!m_AttemptingToCreateLobby)
			{
				m_AttemptingToCreateLobby = true;
				Singleton.Manager<ManNetwork>.inst.WorldSeed = m_ActiveSave.SaveInfo.m_WorldSeed;
				Singleton.Manager<ManNetwork>.inst.BiomeChoice = m_ActiveSave.SaveInfo.m_BiomeChoice;
				Singleton.Manager<ManNetwork>.inst.SetPiecePlacements = null;
				Singleton.Manager<ManNetwork>.inst.WorldGenVersionID = m_ActiveSave.SaveInfo.m_WorldGenVersionID;
				Singleton.Manager<ManNetwork>.inst.WorldGenVersionType = m_ActiveSave.SaveInfo.m_WorldGenVersioningType;
				Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinedEvent.Subscribe(OnLobbyJoined);
				Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyCreateFailedEvent.Subscribe(OnLobbyCreationFailed);
				Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CreateLobby(gameType, m_ActiveSave.SaveInfo.m_LobbyVisibility);
			}
			Mode<ModeAttract>.inst.SetCanStartNewAttractModeSequence(enabled: false);
			Singleton.Manager<ManUI>.inst.FadeToBlack(3f, forceFront: true);
		}
		else
		{
			m_UIStartMode.StartMode();
		}
	}

	private void OnLobbyJoined(Lobby newLobby)
	{
		m_AttemptingToCreateLobby = false;
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.TriggerGameStart();
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinedEvent.Unsubscribe(OnLobbyJoined);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyCreateFailedEvent.Unsubscribe(OnLobbyCreationFailed);
	}

	private void OnLobbyCreationFailed(LobbySystem.LobbyErrorCode error)
	{
		m_AttemptingToCreateLobby = false;
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinedEvent.Unsubscribe(OnLobbyJoined);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyCreateFailedEvent.Unsubscribe(OnLobbyCreationFailed);
		d.LogError("Failed to create lobby with error: " + error);
		Singleton.Manager<ManUI>.inst.ClearFade(3f);
		Mode<ModeAttract>.inst.SetCanStartNewAttractModeSequence(enabled: true);
	}

	public void DisplayLoadConfirmMenu()
	{
		if (SKU.SwitchUI && m_ActiveSave.GetSaveGameType().IsCoOp())
		{
			d.Log($"Loading game type {m_ActiveSave.GetSaveGameType()} checking free communication persmission");
			if (!ManNintendoSwitch.CheckFreeCommunicationPermission(showUi: true))
			{
				d.Log($"Loading game type {m_ActiveSave.GetSaveGameType()} blocked by free communication persmission");
				return;
			}
		}
		if (m_ActiveSave.SaveInfo.HasMods && !Singleton.Manager<ManMods>.inst.CanLoadSaveGame(m_ActiveSave.SaveInfo.m_ModNames))
		{
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			if ((bool)uIScreenNotifications)
			{
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Save, 10);
				UIButtonData accept = new UIButtonData
				{
					m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
					m_Callback = delegate
					{
						LoadAction();
					},
					m_RewiredAction = 21
				};
				UIButtonData decline = new UIButtonData
				{
					m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30),
					m_Callback = delegate
					{
						Singleton.Manager<ManUI>.inst.PopScreen();
					},
					m_RewiredAction = 22
				};
				uIScreenNotifications.Set(localisedString, accept, decline);
				Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications.Type);
			}
		}
		else if (Singleton.Manager<ManGameMode>.inst.CurrentModeCanSave())
		{
			UIScreenNotifications uIScreenNotifications2 = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			if ((bool)uIScreenNotifications2)
			{
				string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Save, 9);
				UIButtonData accept2 = new UIButtonData
				{
					m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
					m_Callback = delegate
					{
						LoadAction();
					},
					m_RewiredAction = 21
				};
				UIButtonData decline2 = new UIButtonData
				{
					m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30),
					m_Callback = delegate
					{
						Singleton.Manager<ManUI>.inst.PopScreen();
					},
					m_RewiredAction = 22
				};
				uIScreenNotifications2.Set(localisedString2, accept2, decline2);
				Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications2.Type);
			}
		}
		else
		{
			LoadAction();
		}
	}

	public void OnSubmit(BaseEventData eventData)
	{
		if (m_ActiveSave != null)
		{
			DisplayLoadConfirmMenu();
		}
		eventData.Use();
	}

	public void OnCancel(BaseEventData eventData)
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
		eventData.Use();
	}

	public void OnUIExtraButton2(BaseEventData eventData)
	{
		if (m_ActiveSave != null && m_ActiveSave.SaveInfo != null)
		{
			PromptDeleteSavedGame();
		}
		eventData.Use();
	}

	public void OnTabChangePrev(BaseEventData eventData)
	{
		if (m_Categories.gameObject.activeSelf)
		{
			TabChangePrev();
		}
		eventData.Use();
	}

	public void OnTabChangeNext(BaseEventData eventData)
	{
		if (m_Categories.gameObject.activeSelf)
		{
			TabChangeNext();
		}
		eventData.Use();
	}

	private int GetCurrentCategoryIndex()
	{
		int result = 0;
		for (int i = 0; i < m_CatergoryTypeButtons.Length; i++)
		{
			Toggle toggle = m_CatergoryTypeButtons[i];
			if ((bool)toggle && toggle.isOn)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	private int GetNextInOrder(bool forwards)
	{
		int currentCategoryIndex = GetCurrentCategoryIndex();
		int result = currentCategoryIndex;
		int num = -1;
		for (int i = 0; i < m_CategoryTypeOrder.Length; i++)
		{
			if (m_CategoryTypeOrder[i] == (ManGameMode.GameType)currentCategoryIndex)
			{
				num = i;
				break;
			}
		}
		if (num >= 0)
		{
			for (int j = 1; j < m_CategoryTypeOrder.Length; j++)
			{
				int num2 = (forwards ? j : (m_CategoryTypeOrder.Length - j));
				int num3 = (num + num2) % m_CategoryTypeOrder.Length;
				int num4 = (int)m_CategoryTypeOrder[num3];
				if (m_CatergoryTypeButtons[num4] != null && m_CatergoryTypeButtons[num4].gameObject.activeInHierarchy && m_CatergoryTypeButtons[num4].interactable)
				{
					result = num4;
					break;
				}
			}
		}
		else
		{
			d.LogError($"GetNextInOrder did not find current category {currentCategoryIndex} in order array");
		}
		return result;
	}

	private int GetPreviousCategoryIndex()
	{
		return GetNextInOrder(forwards: false);
	}

	private int GetNextCategoryIndex()
	{
		return GetNextInOrder(forwards: true);
	}

	private void TabChangeNext()
	{
		int currentCategoryIndex = GetCurrentCategoryIndex();
		int nextCategoryIndex = GetNextCategoryIndex();
		if (nextCategoryIndex != currentCategoryIndex)
		{
			m_CatergoryTypeButtons[nextCategoryIndex].isOn = true;
		}
	}

	private void TabChangePrev()
	{
		int currentCategoryIndex = GetCurrentCategoryIndex();
		int previousCategoryIndex = GetPreviousCategoryIndex();
		if (previousCategoryIndex != currentCategoryIndex)
		{
			m_CatergoryTypeButtons[previousCategoryIndex].isOn = true;
		}
	}

	public void SelectTab(int index)
	{
		int currentCategoryIndex = GetCurrentCategoryIndex();
		if (index != currentCategoryIndex)
		{
			m_CatergoryTypeButtons[index].isOn = true;
		}
	}

	private void StopPreloadingGames()
	{
	}

	private void OnDLCChanged()
	{
		int num = 5;
		Toggle toggle = null;
		if (num < m_CatergoryTypeButtons.Length)
		{
			toggle = m_CatergoryTypeButtons[num];
		}
		bool flag = Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(ManDLC.DLCType.RandD);
		if (!(toggle != null) || toggle.interactable == flag)
		{
			return;
		}
		m_CatergoryTypeButtons[num].interactable = flag;
		if (flag)
		{
			m_CatergoryTypeButtons[num].onValueChanged.AddListener(delegate(bool toggledOn)
			{
				if (toggledOn && ManGameMode.GameType.RaD != m_GameTypeOfSaves)
				{
					ShowSavesForType(ManGameMode.GameType.RaD);
				}
			});
		}
		else
		{
			m_CatergoryTypeButtons[num].onValueChanged.RemoveAllListeners();
		}
	}
}
