#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITechManagerEntry : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler
{
	[SerializeField]
	private Text m_NameText;

	[SerializeField]
	private GameObject m_TeamsContainer;

	[SerializeField]
	private Dropdown m_TeamsDropDown;

	[SerializeField]
	private Button m_TeamDropDownContainer;

	[SerializeField]
	private Text m_TeamDropDownText;

	[SerializeField]
	private Text m_TeamDropDownDisabledText;

	[SerializeField]
	private Text m_TeamDropDownEnemyLimitReachedText;

	[SerializeField]
	private Image m_Thumbnail;

	[SerializeField]
	private Button m_SendToSCUButton;

	[SerializeField]
	private Button m_SnapshotButton;

	[SerializeField]
	private Button m_RenameButton;

	[SerializeField]
	private GameObject m_CapContainer;

	[SerializeField]
	private RectTransform m_CapFillBg;

	[SerializeField]
	private RectTransform m_CapFillBar;

	[SerializeField]
	private Text m_CapCurrent;

	[SerializeField]
	private Text m_CapLimit;

	[SerializeField]
	private Text m_DistanceText;

	[SerializeField]
	private Button m_GamepadOnlyWrapperButton;

	[SerializeField]
	private Image m_SelectionBorder;

	[SerializeField]
	private IntVector2 m_TechSnapshotSize = new IntVector2(96, 96);

	[SerializeField]
	private Text m_AuthorshipLabelText;

	[SerializeField]
	public LocalisedString m_AuthorshipLabelPrefix;

	public Event<UITechManagerEntry, bool> HoverEvent;

	private TrackedVisible m_TrackedVis;

	private TooltipComponent m_SendToSCUTooltip;

	private int m_CachedDist;

	public int TeamID => m_TrackedVis.TeamID;

	public int UniqueID
	{
		get
		{
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				return m_TrackedVis.ID;
			}
			return m_TrackedVis.HostID;
		}
	}

	public bool Expired => Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_TrackedVis.ID) == null;

	public TrackedVisible TrackedVisible => m_TrackedVis;

	public void Init(TrackedVisible tv, bool showTeams)
	{
		m_TrackedVis = tv;
		m_CachedDist = int.MinValue;
		int teamID = GetTeamID();
		if (showTeams && m_TeamsDropDown.IsNotNull())
		{
			int num = 0;
			List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				for (int i = 1073741824; i < 1073741829; i++)
				{
					string text = ModeCoOp<ModeCoOpCreative>.CreateTeamNameFromID(i);
					list.Add(new Dropdown.OptionData(text));
				}
			}
			else
			{
				list.Add(new Dropdown.OptionData(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUD, 44)));
			}
			list.Add(new Dropdown.OptionData(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUD, 32)));
			num = ((!ManSpawn.IsEnemyTeam(m_TrackedVis.TeamID)) ? teamID : (list.Count - 1));
			m_TeamsDropDown.ClearOptions();
			m_TeamsDropDown.AddOptions(list);
			m_TeamsDropDown.SetValue(num);
			m_TeamsDropDown.interactable = m_TrackedVis.visible.IsNotNull();
			TooltipComponent component = m_TeamsDropDown.GetComponent<TooltipComponent>();
			if ((bool)component)
			{
				component.enabled = m_TrackedVis.visible.IsNull();
			}
			if (m_TeamDropDownText.IsNotNull() && m_TeamDropDownDisabledText.IsNotNull())
			{
				m_TeamDropDownText.enabled = m_TrackedVis.visible.IsNotNull();
				m_TeamDropDownDisabledText.enabled = m_TrackedVis.visible.IsNull();
			}
		}
		m_TeamDropDownEnemyLimitReachedText.IsNotNull();
		if (m_TeamsContainer.IsNotNull())
		{
			m_TeamsContainer.SetActive(showTeams);
		}
		UpdateTechName();
		SetAuthor();
		if (m_CapContainer.IsNotNull())
		{
			m_CapContainer.SetActive(Singleton.Manager<ManBlockLimiter>.inst.LimiterActive);
		}
		if (Singleton.Manager<ManBlockLimiter>.inst.LimiterActive)
		{
			int trackedTechCost = Singleton.Manager<ManBlockLimiter>.inst.GetTrackedTechCost(m_TrackedVis.ID, includeHeldItems: true);
			int maximumUsage = Singleton.Manager<ManBlockLimiter>.inst.MaximumUsage;
			float num2 = Math.Min((float)trackedTechCost / Math.Max(maximumUsage, 1f), 1f);
			if (m_CapCurrent.IsNotNull())
			{
				m_CapCurrent.text = trackedTechCost.ToString();
			}
			if (m_CapLimit.IsNotNull())
			{
				m_CapLimit.text = "/" + maximumUsage;
			}
			if (m_CapFillBar.IsNotNull() && m_CapFillBg.IsNotNull())
			{
				float size = num2 * m_CapFillBg.rect.width;
				m_CapFillBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
			}
		}
		if (m_Thumbnail.IsNotNull())
		{
			ClearThumbnail();
			Singleton.Manager<ManScreenshot>.inst.RenderTechImage(m_TrackedVis, m_TechSnapshotSize, encodeTechData: false, delegate(TechData techData, Texture2D techImage)
			{
				if (techImage.IsNotNull())
				{
					m_Thumbnail.sprite = Sprite.Create(techImage, new Rect(Vector2.zero, new Vector2(techImage.width, techImage.height)), Vector2.zero);
					m_Thumbnail.preserveAspect = true;
				}
			});
		}
		Refresh();
	}

	private Transform FindDropDownList(Dropdown dropdown)
	{
		foreach (Transform item in dropdown.transform)
		{
			if (item.name == "Dropdown List")
			{
				return item;
			}
		}
		return null;
	}

	private Transform GetAncestor(Transform t, int generation)
	{
		for (int i = 0; i < generation; i++)
		{
			if (!t.IsNotNull())
			{
				break;
			}
			t = t.parent;
		}
		return t;
	}

	public void UpdateEnemyLimitMessageForSelection(bool forceHide = false)
	{
		if (!m_TeamDropDownEnemyLimitReachedText || !m_TeamsDropDown)
		{
			return;
		}
		bool active = false;
		if (!forceHide)
		{
			Transform transform = FindDropDownList(m_TeamsDropDown);
			if ((bool)transform)
			{
				GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
				d.Log($"UpdateEnemyLimitMessageForSelection dropdownList = {transform} selected = {currentSelectedGameObject} index = {(currentSelectedGameObject ? currentSelectedGameObject.transform.GetSiblingIndex() : (-999))} ancestor = {GetAncestor((currentSelectedGameObject != null) ? currentSelectedGameObject.transform : null, 3)}");
				if ((bool)transform && (bool)currentSelectedGameObject && GetAncestor(currentSelectedGameObject.transform, 3) == transform.transform)
				{
					int index = currentSelectedGameObject.transform.GetSiblingIndex() - 1;
					active = IsOptionDisabledForEnemySizeLimitation(index);
				}
			}
		}
		m_TeamDropDownEnemyLimitReachedText.gameObject.SetActive(active);
	}

	public void OnTeamUpdated(int newTeamID)
	{
		int num = 0;
		UIEventSyncExtensions.SetValue(value: (!ManSpawn.IsEnemyTeam(newTeamID)) ? (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() ? ManSpawn.LobbyTeamIDFromTechTeamID(newTeamID) : newTeamID) : (m_TeamsDropDown.options.Count - 1), instance: m_TeamsDropDown);
	}

	public void OnTechNameChanged()
	{
		UpdateTechName();
	}

	public void OnTechDriverChanged()
	{
		SetAuthor();
	}

	private void ClearThumbnail()
	{
		if (m_Thumbnail != null && m_Thumbnail.sprite != null)
		{
			UnityEngine.Object.Destroy(m_Thumbnail.sprite.texture);
			UnityEngine.Object.Destroy(m_Thumbnail.sprite);
		}
		m_Thumbnail.sprite = null;
	}

	public void Cleanup()
	{
		ClearThumbnail();
		HoverEvent.Clear();
	}

	public void SetChildNavigationEntryPoint(bool set)
	{
		GameObject target = (m_TeamsDropDown.gameObject.activeInHierarchy ? m_TeamDropDownContainer.gameObject : m_SnapshotButton.gameObject);
		if (set)
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(target);
		}
		else
		{
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(target);
		}
	}

	public void OnEntrySelectedGamepad()
	{
		UITechManagerHUD uITechManagerHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechManager) as UITechManagerHUD;
		if ((bool)uITechManagerHUD)
		{
			uITechManagerHUD.SelectTech(UniqueID);
		}
		UpdateSendToSCUTooltip(CanSendToSCU());
	}

	public void SetAsGamepadEntryPoint(bool set)
	{
		if (set)
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_GamepadOnlyWrapperButton.gameObject);
		}
		else
		{
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_GamepadOnlyWrapperButton.gameObject);
		}
	}

	public void EnsureSelection()
	{
		Singleton.Manager<ManNavUI>.inst.DeferredSetSelected(m_GamepadOnlyWrapperButton.gameObject);
	}

	public void SetupNavigationToNextNeighbour(UITechManagerEntry nextNav)
	{
		Selectable navigationDown = m_GamepadOnlyWrapperButton.GetNavigationDown();
		if (navigationDown != null)
		{
			if (navigationDown.GetNavigationUp() == m_GamepadOnlyWrapperButton)
			{
				navigationDown.SetNavigationUp(null);
			}
			m_GamepadOnlyWrapperButton.SetNavigationDown(null);
		}
		if (nextNav.IsNotNull())
		{
			m_GamepadOnlyWrapperButton.SetNavigationDown(nextNav.m_GamepadOnlyWrapperButton);
			nextNav.m_GamepadOnlyWrapperButton.SetNavigationUp(m_GamepadOnlyWrapperButton);
		}
	}

	public void SetupNavigationToPrevNeighbour(UITechManagerEntry nextNav)
	{
		Selectable navigationUp = m_GamepadOnlyWrapperButton.GetNavigationUp();
		if (navigationUp != null)
		{
			if (navigationUp.GetNavigationDown() == m_GamepadOnlyWrapperButton)
			{
				navigationUp.SetNavigationDown(null);
			}
			m_GamepadOnlyWrapperButton.SetNavigationUp(null);
		}
		if (nextNav.IsNotNull())
		{
			m_GamepadOnlyWrapperButton.SetNavigationUp(nextNav.m_GamepadOnlyWrapperButton);
			nextNav.m_GamepadOnlyWrapperButton.SetNavigationDown(m_GamepadOnlyWrapperButton);
		}
	}

	public void InitNavigation(UITechManagerEntry prevNav)
	{
		m_GamepadOnlyWrapperButton.SetNavigationMode(Navigation.Mode.Explicit);
		SetupNavigationToPrevNeighbour(prevNav);
	}

	private bool CanSendToSCU()
	{
		if (Singleton.Manager<ManTechs>.inst.CanEnemyProximitySensitiveActionBeExecuted(m_TrackedVis.Position, Globals.inst.m_TechStoreThreatDistance))
		{
			return Singleton.Manager<ManPlayer>.inst.PaletteUnlocked;
		}
		return false;
	}

	private int GetTeamID()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			return ManSpawn.LobbyTeamIDFromTechTeamID(m_TrackedVis.TeamID);
		}
		return m_TrackedVis.TeamID;
	}

	private void UpdateSendToSCUTooltip(bool canSend)
	{
		if ((bool)m_SendToSCUTooltip)
		{
			if (canSend)
			{
				m_SendToSCUTooltip.SetText(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.HUD.ToolTipStoreTech));
				m_SendToSCUTooltip.SetMode(UITooltipOptions.Default);
			}
			else if (!Singleton.Manager<ManPlayer>.inst.PaletteUnlocked)
			{
				m_SendToSCUTooltip.SetText(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.HUD.ToolTipInventoryRequired));
				m_SendToSCUTooltip.SetMode(UITooltipOptions.Warning);
			}
			else
			{
				m_SendToSCUTooltip.SetText(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.HUD.ToolTipStoreTechEnemies));
				m_SendToSCUTooltip.SetMode(UITooltipOptions.Warning);
			}
		}
	}

	public void SetSelectionBorderVisible(bool visible)
	{
		m_SelectionBorder.gameObject.SetActive(visible);
	}

	private bool IsOptionDisabledForEnemySizeLimitation(int index)
	{
		if (!Singleton.Manager<ManBlockLimiter>.inst.LimiterActive)
		{
			return false;
		}
		if (ManSpawn.IsEnemyTeam(m_TrackedVis.TeamID))
		{
			return false;
		}
		int num = ((!Singleton.Manager<ManNetwork>.inst.IsMultiplayer()) ? 1 : 5);
		if (index == num)
		{
			return Singleton.Manager<ManBlockLimiter>.inst.GetTrackedTechCost(m_TrackedVis.ID, includeHeldItems: false) > Singleton.Manager<ManBlockLimiter>.inst.GetRemainingEnemyLimit();
		}
		return false;
	}

	private void UpdateTechName()
	{
		if (!m_NameText.IsNotNull())
		{
			return;
		}
		string empty;
		if (m_TrackedVis.visible.IsNotNull())
		{
			empty = m_TrackedVis.visible.name;
		}
		else
		{
			TechData storedTechData = Singleton.Manager<ManVisible>.inst.GetStoredTechData(m_TrackedVis);
			if (storedTechData != null)
			{
				empty = storedTechData.Name;
			}
			else
			{
				d.LogError("No TechData for tracked visible with uniqueID " + UniqueID);
				empty = string.Empty;
			}
		}
		m_NameText.text = empty.ToUpper();
	}

	private void SetAuthor()
	{
		TechData techData = null;
		if (!m_AuthorshipLabelText.IsNotNull())
		{
			return;
		}
		string text = "";
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			if (m_TrackedVis.visible.IsNotNull())
			{
				if (m_TrackedVis.visible.tank.IsNotNull())
				{
					text = m_TrackedVis.visible.tank.Author;
				}
			}
			else
			{
				techData = Singleton.Manager<ManVisible>.inst.GetStoredTechData(m_TrackedVis);
				if (techData != null)
				{
					text = ((!(techData.Author != "")) ? "" : techData.Author);
				}
				else
				{
					d.LogError("No TechData for tracked visible with uniqueID " + UniqueID);
				}
			}
			text = ((!text.NullOrEmpty() && text != "") ? (m_AuthorshipLabelPrefix.Value + " " + text) : "");
		}
		m_AuthorshipLabelText.text = text;
	}

	private void OnDropdownSelectTeam(int index)
	{
		if (IsOptionDisabledForEnemySizeLimitation(index))
		{
			int value = (ManSpawn.IsEnemyTeam(m_TrackedVis.TeamID) ? (m_TeamsDropDown.options.Count - 1) : GetTeamID());
			m_TeamsDropDown.SetValue(value);
		}
		else
		{
			if (!m_TrackedVis.visible.IsNotNull())
			{
				return;
			}
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				int techTeamID = ManSpawn.TechTeamIDFromLobbyTeamID(index);
				if (index == 5)
				{
					techTeamID = -1;
				}
				if (m_TrackedVis.visible.tank.IsNotNull() && m_TrackedVis.visible.tank.netTech.IsNotNull())
				{
					m_TrackedVis.visible.tank.netTech.RequestChangeTeam(techTeamID);
				}
				return;
			}
			int num = ((index != 0) ? 1 : 0);
			if (m_TrackedVis.visible.tank.IsNotNull())
			{
				m_TrackedVis.visible.tank.SetTeam(num, num == 1);
				Tank tank = m_TrackedVis.visible.tank;
				if (ManSpawn.IsPlayerTeam(tank.Team))
				{
					tank.AI.SetBehaviorType(AITreeType.AITypes.Idle);
				}
				else
				{
					tank.AI.SetOldBehaviour();
				}
			}
		}
	}

	private void OnSendToSCU()
	{
		bool flag = CanSendToSCU();
		UpdateSendToSCUTooltip(flag);
		if (flag)
		{
			bool flag2 = !Singleton.Manager<ManUI>.inst.IsUILocked(ManUI.LockTimerTypes.SendToSCU);
			if (flag2 && m_TrackedVis.visible.IsNotNull())
			{
				flag2 = m_TrackedVis.visible.CanBeSentToSCU && !Singleton.Manager<ManTechSwapper>.inst.CheckOperatingOnTech(m_TrackedVis.visible.tank);
			}
			if (flag2)
			{
				Singleton.Manager<ManPurchases>.inst.StoreTechToInventory(m_TrackedVis, supportUndo: true);
			}
		}
	}

	private void OnTakeSnapshot()
	{
		Singleton.Manager<ManScreenshot>.inst.TakeSnapshotAndShowUI(m_TrackedVis);
		UITechManagerHUD uITechManagerHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechManager) as UITechManagerHUD;
		if ((bool)uITechManagerHUD)
		{
			uITechManagerHUD.TemporarilyHide();
		}
	}

	private void OnRename()
	{
		UIScreenRenameTech uIScreenRenameTech = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.RenameTech) as UIScreenRenameTech;
		d.Assert(uIScreenRenameTech != null, "Cannot find Rename Tech screen");
		if ((bool)uIScreenRenameTech)
		{
			uIScreenRenameTech.SetSelectedTech(m_TrackedVis);
			Singleton.Manager<ManUI>.inst.PushScreen(uIScreenRenameTech);
		}
		UITechManagerHUD uITechManagerHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechManager) as UITechManagerHUD;
		if ((bool)uITechManagerHUD)
		{
			uITechManagerHUD.TemporarilyHide();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		UpdateSendToSCUTooltip(CanSendToSCU());
		HoverEvent.Send(this, paramB: true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		HoverEvent.Send(this, paramB: false);
	}

	public void OnSelect(BaseEventData eventData)
	{
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			HoverEvent.Send(this, paramB: true);
		}
	}

	private void OnPool()
	{
		m_TeamsDropDown.onValueChanged.AddListener(OnDropdownSelectTeam);
		if (m_SendToSCUButton.IsNotNull())
		{
			m_SendToSCUTooltip = m_SendToSCUButton.GetComponentInChildren<TooltipComponent>();
			m_SendToSCUButton.onClick.AddListener(OnSendToSCU);
		}
		if (m_SnapshotButton.IsNotNull())
		{
			m_SnapshotButton.onClick.AddListener(OnTakeSnapshot);
		}
		if (m_RenameButton.IsNotNull())
		{
			m_RenameButton.onClick.AddListener(OnRename);
		}
		UpdateEnemyLimitMessageForSelection(forceHide: true);
	}

	private void OnRecycle()
	{
		m_TrackedVis = null;
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
		UpdateEnemyLimitMessageForSelection(forceHide: true);
		Cleanup();
	}

	public void Refresh()
	{
		bool flag = m_TrackedVis.visible.IsNotNull() && Singleton.Manager<ManTechSwapper>.inst.CheckOperatingOnTech(m_TrackedVis.visible.tank);
		if (m_TeamsDropDown.IsNotNull())
		{
			m_TeamsDropDown.interactable = m_TrackedVis.visible.IsNotNull();
		}
		if (m_SnapshotButton.IsNotNull())
		{
			m_SnapshotButton.interactable = !flag;
		}
		if (m_SendToSCUButton.IsNotNull())
		{
			m_SendToSCUButton.interactable = ManSpawn.IsPlayerTeam(m_TrackedVis.TeamID) && !flag;
		}
		if (m_RenameButton.IsNotNull())
		{
			m_RenameButton.interactable = ManSpawn.IsPlayerTeam(m_TrackedVis.TeamID);
		}
		if (m_DistanceText.IsNotNull())
		{
			int num = Mathf.RoundToInt((Singleton.cameraTrans.position - m_TrackedVis.Position).magnitude / 100f);
			if (num != m_CachedDist)
			{
				float gameUnits = (float)num * 100f;
				m_DistanceText.text = GameUnits.GetDistanceText(gameUnits);
				m_CachedDist = num;
			}
		}
		if (m_NameText.IsNotNull())
		{
			int teamID = GetTeamID();
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				if (teamID == int.MaxValue)
				{
					m_NameText.color = Color.red;
				}
				else
				{
					m_NameText.color = Singleton.Manager<ManNetworkLobby>.inst.LobbyConstants.m_UnpilotedTechColours[teamID];
				}
			}
			else
			{
				m_NameText.color = ((teamID == 0) ? ((Color)Singleton.Manager<ManNetworkLobby>.inst.LobbyConstants.m_UnpilotedTechColours[0]) : Color.red);
			}
		}
		ManNavUI.RecalculateLeftRightNavigation(new Selectable[4] { m_TeamDropDownContainer, m_SnapshotButton, m_SendToSCUButton, m_RenameButton });
	}
}
