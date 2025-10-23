#define UNITY_EDITOR
using System.Collections.Generic;
using Binding;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIShopTechSelect : UIHUDElement, ISubmitHandler, IEventSystemHandler, ICancelHandler, IMoveHandler, IUIExtraButtonHandler2
{
	public enum TooltipReason
	{
		Default,
		MissingBlocks,
		EnemiesNearby,
		PositionBlocked,
		CurrentlyLoading,
		TooFarAway,
		BlockLimiter
	}

	[SerializeField]
	private RectTransform m_TechList;

	[SerializeField]
	private Text m_SelectedTechName;

	[SerializeField]
	private Text m_SelectedTechLimitCost;

	[SerializeField]
	private Text m_SelectedTechCreator;

	[SerializeField]
	private Image m_SelectedTechImage;

	[SerializeField]
	private RectTransform m_TechAvailablePanel;

	[SerializeField]
	private RectTransform m_TechUnavailablePanel;

	[SerializeField]
	private RectTransform m_TechBlockLimitedPanel;

	[SerializeField]
	private UIShopTechItem m_TechItemPrefab;

	[SerializeField]
	private string m_ExpandAnimation;

	[SerializeField]
	private string m_CollapseAnimation;

	[SerializeField]
	private LocalisedString m_DefaultTooltip;

	[SerializeField]
	private LocalisedString m_MissingBlocksTooltip;

	[SerializeField]
	private LocalisedString m_EnemiesNearbyTooltip;

	[SerializeField]
	private LocalisedString m_PositionBlockedTooltip;

	[SerializeField]
	private LocalisedString m_CurrentlyLoadingTooltip;

	[SerializeField]
	private LocalisedString m_TooFarAwayTooltip;

	[SerializeField]
	private LocalisedString m_BlockLimiterTooltip;

	[SerializeField]
	private ScrollRect m_GridScrollRect;

	[SerializeField]
	private GameObject m_DefaultTextThumbnail;

	[SerializeField]
	private GameObject m_DynamicTextThumbnail;

	[SerializeField]
	private UIUndiscoveredBlocks m_UndiscoveredBlocks;

	private Snapshot m_SelectedCapture;

	private TechDataAvailValidation m_SelectedTechBlockCache = new TechDataAvailValidation();

	private bool m_UpdateTech;

	private bool m_UpdateTechList;

	private bool m_BlockLimiterOK;

	private ToggleGroup m_TechListToggleGroup;

	private Animator m_Animator;

	private SnapshotCollectionDisk m_LoadedCaptures;

	private List<UIShopTechItem> m_TechButtons = new List<UIShopTechItem>();

	private Dictionary<Snapshot, UIShopTechItem> m_TechButtonLookup = new Dictionary<Snapshot, UIShopTechItem>();

	private Toggle m_PreviousTechButton;

	public override void Show(object context)
	{
		base.Show(context);
	}

	public override void Hide(object context)
	{
		CollapseSelf();
		base.Hide(context);
	}

	public override bool Expand(object context)
	{
		bool result = false;
		if (base.IsVisible)
		{
			m_UpdateTechList = true;
			if ((bool)m_Animator)
			{
				m_Animator.Play(m_ExpandAnimation, 0, 0f);
			}
			result = base.Expand(context);
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.UIDeployTechPanel);
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(base.gameObject);
			m_PreviousTechButton = null;
		}
		return result;
	}

	public override bool Collapse(object context)
	{
		bool result = false;
		if (base.IsVisible)
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.UIDeployTechPanel);
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(base.gameObject);
			m_PreviousTechButton = null;
			Singleton.Manager<ManPointer>.inst.DisablePlacementMode();
			Singleton.Manager<ManOverlay>.inst.RemoveToolTip();
			if ((bool)m_Animator)
			{
				m_Animator.Play(m_CollapseAnimation, 0, 0f);
			}
			result = base.Collapse(context);
			TankCamera.inst.SetMouseControlEnabled(enabled: true);
			m_UndiscoveredBlocks.SetActive(isActive: false);
		}
		return result;
	}

	private void ChooseTechPosUpdate(Vector3 worldPos, bool visible, bool blocked)
	{
	}

	public void PlaceTech(Vector3 position)
	{
		Vector3 position2 = position + Vector3.up * 5f;
		Singleton.Manager<ManPurchases>.inst.LoadTechFromInventoryAtPosition(m_SelectedTechBlockCache.m_Tech, position2, Quaternion.identity);
		m_UpdateTech = true;
	}

	private void UpdateList()
	{
		for (int i = 0; i < m_TechButtons.Count; i++)
		{
			m_TechButtons[i].Used = false;
		}
		BindableList<SnapshotDisk> snapshots = m_LoadedCaptures.Snapshots;
		int num = 0;
		for (int j = 0; j < snapshots.Count; j++)
		{
			SnapshotDisk snapshotDisk = snapshots[j];
			if (snapshotDisk != null && Singleton.Manager<ManSpawn>.inst.CanLoadTechInCurrentMode(snapshotDisk.techData))
			{
				if (!m_TechButtonLookup.TryGetValue(snapshotDisk, out var value))
				{
					value = SpawnTechItem(snapshotDisk);
				}
				value.Used = true;
				value.RectTransform.SetSiblingIndex(num);
				int hotswapSlot = Singleton.Manager<ManPlayer>.inst.GetHotswapSlot(snapshotDisk);
				value.SetHotswapDropdown(hotswapSlot);
				num++;
			}
		}
		bool flag = false;
		for (int num2 = m_TechButtons.Count - 1; num2 >= 0; num2--)
		{
			if (!m_TechButtons[num2].Used)
			{
				if (m_SelectedCapture == m_TechButtons[num2].Snapshot)
				{
					flag = true;
				}
				RecycleTechItem(m_TechButtons[num2]);
			}
		}
		if (flag && m_TechButtons.Count > 0)
		{
			SetSelectedTech(m_TechButtons[0].Snapshot);
			if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
			{
				HighLightButton(m_TechButtons[0].ToggleButton);
			}
		}
		bool flag2 = m_LoadedCaptures.Snapshots.Count < 1;
		m_DefaultTextThumbnail.gameObject.SetActive(flag2);
		m_DynamicTextThumbnail.gameObject.SetActive(!flag2);
	}

	private UIShopTechItem SpawnTechItem(Snapshot snapshot)
	{
		UIShopTechItem uIShopTechItem = m_TechItemPrefab.Spawn();
		uIShopTechItem.SetupTech(snapshot);
		uIShopTechItem.RectTransform.SetParent(m_TechList, worldPositionStays: false);
		if ((bool)uIShopTechItem.ToggleButton)
		{
			uIShopTechItem.ToggleButton.group = m_TechListToggleGroup;
		}
		uIShopTechItem.Selected.Subscribe(SetSelectedTech);
		uIShopTechItem.HotswapChanged.Subscribe(SetHotswapBinding);
		m_TechButtons.Add(uIShopTechItem);
		m_TechButtonLookup.Add(snapshot, uIShopTechItem);
		if (m_SelectedCapture == null && m_TechButtons.Count > 0)
		{
			SetSelectedTech(snapshot);
			if (uIShopTechItem.ToggleButton != null)
			{
				uIShopTechItem.ToggleButton.isOn = true;
			}
		}
		return uIShopTechItem;
	}

	private void RecycleTechItem(UIShopTechItem techButton)
	{
		techButton.Selected.Unsubscribe(SetSelectedTech);
		techButton.HotswapChanged.Unsubscribe(SetHotswapBinding);
		m_TechButtons.Remove(techButton);
		m_TechButtonLookup.Remove(techButton.Snapshot);
		techButton.RectTransform.SetParent(null, worldPositionStays: false);
		techButton.Recycle();
	}

	private void UpdateTech()
	{
		if (m_SelectedCapture == null)
		{
			return;
		}
		TechData techData = m_SelectedCapture.techData;
		if (techData == null)
		{
			SnapshotDisk snapshotDisk = m_SelectedCapture as SnapshotDisk;
			d.LogError("UIShopTechSelect.UpdateTech - Snapshot contains invalid tech data. Path: " + snapshotDisk.snapName);
			return;
		}
		Vector2 emulatedCursorPos = Singleton.Manager<ManPointer>.inst.GetEmulatedCursorPos();
		Singleton.Manager<ManPointer>.inst.DisablePlacementMode();
		m_SelectedTechName.text = techData.Name;
		m_SelectedTechCreator.text = m_SelectedCapture.creator;
		IInventory<BlockTypes> inventory = Singleton.Manager<ManPurchases>.inst.GetInventory();
		m_SelectedTechBlockCache.RecordBlockDataAndValidate(techData, new InventoryMetaData(inventory));
		bool flag = !m_SelectedTechBlockCache.m_ExceedsBlockLimitPlace;
		bool flag2 = !m_SelectedTechBlockCache.UnavailablePlace;
		bool flag3 = !m_SelectedTechBlockCache.HasMissingBlocksPlace && !flag;
		m_BlockLimiterOK = flag;
		if (Singleton.Manager<ManBlockLimiter>.inst.LimiterActive)
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Purchasing, 10);
			m_SelectedTechLimitCost.text = string.Format(localisedString, m_SelectedTechBlockCache.m_LimiterCost);
			m_SelectedTechLimitCost.color = (flag ? Color.white : Color.red);
		}
		else
		{
			m_SelectedTechLimitCost.text = "";
		}
		m_TechAvailablePanel.gameObject.SetActive(flag2);
		m_TechUnavailablePanel.gameObject.SetActive(!flag2 && !flag3);
		m_TechBlockLimitedPanel.gameObject.SetActive(flag3);
		m_UndiscoveredBlocks.SetActive(m_SelectedTechBlockCache.UnavailablePlace);
		m_UndiscoveredBlocks.PopulateItems(m_SelectedTechBlockCache);
		m_SelectedCapture.ResolveThumbnail(delegate(Sprite sprite)
		{
			m_SelectedTechImage.sprite = sprite;
		});
		Singleton.Manager<ManPointer>.inst.SetEmulatedCursorPos(emulatedCursorPos);
	}

	private void SetSelectedTech(Snapshot selectedCapture)
	{
		m_SelectedCapture = selectedCapture;
		m_UpdateTech = true;
	}

	private void SetHotswapBinding(Snapshot snapshot, int index)
	{
		if (snapshot is SnapshotDisk snapshot2)
		{
			Singleton.Manager<ManPlayer>.inst.SetHotswapBinding(snapshot2, index);
		}
		else
		{
			d.LogError("UIShopTechSelect.SetHotswapBinding: Found a non disk snapshot, this is incompatible with the current implentation of Hotswapping");
		}
	}

	private void MoveSelection(Vector2 moveVector)
	{
		UIShopTechItem fromUI = m_TechButtonLookup[m_SelectedCapture];
		UIShopTechItem nextImageInDirection = GetNextImageInDirection(fromUI, moveVector);
		HighLightButton(nextImageInDirection.ToggleButton);
		SetSelectedTech(nextImageInDirection.Snapshot);
	}

	private void HighLightButton(Toggle btn)
	{
		if (!(m_PreviousTechButton == btn))
		{
			UIHelpers.VertScrollToItem(m_GridScrollRect.content, btn.GetComponent<RectTransform>(), m_GridScrollRect.viewport.rect.height);
			btn.isOn = true;
			m_PreviousTechButton = btn;
		}
	}

	private UIShopTechItem GetNextImageInDirection(UIShopTechItem fromUI, Vector2 dir)
	{
		UIShopTechItem result = fromUI;
		float num = float.MaxValue;
		Vector2 rhs = new Vector2(dir.y, 0f - dir.x);
		foreach (UIShopTechItem techButton in m_TechButtons)
		{
			Vector3 position = fromUI.GetComponent<RectTransform>().position;
			Vector3 vector = techButton.GetComponent<RectTransform>().position - position;
			float num2 = Vector2.Dot(vector, dir);
			float num3 = Mathf.Abs(Vector2.Dot(vector, rhs));
			if (num2 > 0f)
			{
				float num4 = num2 + num3 * 2f;
				if (num4 < num)
				{
					result = techButton;
					num = num4;
				}
			}
		}
		return result;
	}

	private void OnDelete()
	{
		m_SelectedCapture = null;
		EventSystem.current.SetSelectedGameObject(base.gameObject);
		if (m_LoadedCaptures.Snapshots.Count < 1)
		{
			Singleton.Manager<ManPointer>.inst.DisablePlacementMode();
			Singleton.Manager<ManOverlay>.inst.RemoveToolTip();
			m_PreviousTechButton = null;
		}
		else
		{
			SetSelectedTech(m_TechButtons[0].Snapshot);
		}
	}

	public void OnSubmit(BaseEventData eventData)
	{
		if (base.gameObject.activeSelf && base.IsVisible)
		{
			eventData.Use();
		}
	}

	public void OnCancel(BaseEventData eventData)
	{
		if (base.gameObject.activeSelf && base.IsVisible)
		{
			CollapseSelf();
			eventData.Use();
		}
	}

	public void OnMove(AxisEventData eventData)
	{
		if (base.gameObject.activeSelf && base.IsVisible && m_LoadedCaptures.Snapshots.Count > 0)
		{
			MoveSelection(eventData.moveVector);
			eventData.Use();
		}
	}

	public void OnUIExtraButton2(BaseEventData eventData)
	{
		if (base.gameObject.activeSelf && m_LoadedCaptures.Snapshots.Count > 0)
		{
			Singleton.Manager<ManSnapshots>.inst.DeleteSnapshotRender(m_SelectedCapture, OnDelete);
			eventData.Use();
		}
	}

	private void OnLoadedCapturesChanged(SnapshotCollection<SnapshotDisk> collection)
	{
		if (Singleton.Manager<ManGameMode>.inst.GetModePhase() == ManGameMode.GameState.InGame)
		{
			m_UpdateTechList = true;
		}
	}

	private void OnBlockDiscovered(BlockTypes blockDiscovered)
	{
		if (Singleton.Manager<ManGameMode>.inst.GetModePhase() == ManGameMode.GameState.InGame)
		{
			m_UpdateTech = true;
		}
	}

	private void OnMoneyChanged(int money)
	{
		m_UpdateTech = true;
	}

	private void OnHotswapBindingChanged(ManPlayer.HotswapMap hotswapBindings)
	{
		m_UpdateTechList = true;
	}

	private void OnHotswapMaxSlotsChanged(int maxSlots)
	{
		for (int i = 0; i < m_TechButtons.Count; i++)
		{
			m_TechButtons[i].UpdateDropdownOptions(maxSlots);
		}
	}

	private void OnSpawn()
	{
		m_TechListToggleGroup = m_TechList.GetComponent<ToggleGroup>();
		m_Animator = GetComponent<Animator>();
		Singleton.Manager<ManLicenses>.inst.BlockDiscoveredEvent.Subscribe(OnBlockDiscovered);
		Singleton.Manager<ManPlayer>.inst.MoneyAmountChanged.Subscribe(OnMoneyChanged);
		Singleton.Manager<ManPlayer>.inst.HotswapBindingChanged.Subscribe(OnHotswapBindingChanged);
		Singleton.Manager<ManPlayer>.inst.HotswapMaxSlotsChanged.Subscribe(OnHotswapMaxSlotsChanged);
		AddElementToGroup(ManHUD.HUDGroup.Main, UIHUD.ShowAction.Expand);
		m_LoadedCaptures = Singleton.Manager<ManSnapshots>.inst.ServiceDisk.GetSnapshotCollectionDisk();
		m_LoadedCaptures.Changed.Subscribe(OnLoadedCapturesChanged);
		UpdateList();
	}

	private void OnRecycle()
	{
		for (int num = m_TechButtons.Count - 1; num >= 0; num--)
		{
			RecycleTechItem(m_TechButtons[num]);
		}
		m_TechButtons.Clear();
		m_TechButtonLookup.Clear();
		Singleton.Manager<ManLicenses>.inst.BlockDiscoveredEvent.Unsubscribe(OnBlockDiscovered);
		Singleton.Manager<ManPlayer>.inst.MoneyAmountChanged.Unsubscribe(OnMoneyChanged);
		Singleton.Manager<ManPlayer>.inst.HotswapBindingChanged.Unsubscribe(OnHotswapBindingChanged);
		Singleton.Manager<ManPlayer>.inst.HotswapMaxSlotsChanged.Unsubscribe(OnHotswapMaxSlotsChanged);
		m_LoadedCaptures.Changed.Unsubscribe(OnLoadedCapturesChanged);
		m_LoadedCaptures = null;
	}

	private void Update()
	{
		if (base.IsExpanded)
		{
			if (Singleton.Manager<ManBlockLimiter>.inst.AllowCreatePlayerTech(m_SelectedTechBlockCache.m_LimiterCost) != m_BlockLimiterOK)
			{
				m_UpdateTech = true;
			}
			if (m_UpdateTechList)
			{
				UpdateList();
				m_UpdateTechList = false;
				m_UpdateTech = true;
			}
			if (m_UpdateTech)
			{
				UpdateTech();
				m_UpdateTech = false;
			}
		}
	}
}
