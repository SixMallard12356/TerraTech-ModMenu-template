#define UNITY_EDITOR
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITechManagerHUD : UIHUDElement
{
	[SerializeField]
	private UITechManagerEntry m_TechEntryPrefab;

	[SerializeField]
	private Transform m_EntriesParent;

	[SerializeField]
	private Button m_ExitButton;

	[SerializeField]
	private RectTransform m_TechsTitle;

	[SerializeField]
	private Text m_TechsCountText;

	[SerializeField]
	private LocalisedString m_TechsCountFormatStr;

	[SerializeField]
	private GameObject m_TabsContainer;

	[SerializeField]
	private Button m_ChangeTabButton;

	[SerializeField]
	private ScrollRect m_ScrollRect;

	private List<UITechManagerEntry> m_Entries = new List<UITechManagerEntry>();

	private List<TrackedVisible> m_TrackedVisiblesToAdd = new List<TrackedVisible>();

	private List<TrackedVisible> m_TrackedVisiblesToRemove = new List<TrackedVisible>();

	private List<TrackedVisible> m_TrackedVisiblesPendingSpawn = new List<TrackedVisible>();

	private List<TrackedVisible> m_TrackedVisiblesBeingOperatedOn = new List<TrackedVisible>();

	private UIElementCache<UITechManagerEntry> m_EntryPool;

	private int m_TechUniqueIDSelected;

	private bool m_TechEntryHasFocus;

	private UITechManagerEntry m_CurrentSelection;

	private bool m_TemporarilyHidden;

	private bool m_DisableTabsPrompt;

	public static bool ShowOnlyPlayerTeam { get; set; }

	public static bool AllowTeamSelection => !ShowOnlyPlayerTeam;

	public void SelectTech(int uniqueID)
	{
		m_TechUniqueIDSelected = uniqueID;
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			UITechManagerEntry selectedEntry = GetSelectedEntry();
			if (selectedEntry.IsNotNull())
			{
				m_TechEntryHasFocus = true;
				selectedEntry.SetChildNavigationEntryPoint(set: true);
			}
		}
	}

	public void TemporarilyHide()
	{
		if (base.IsVisible)
		{
			m_TemporarilyHidden = true;
			HideSelf();
		}
	}

	public void ShowIfTemporarilyHidden()
	{
		if (m_TemporarilyHidden)
		{
			ShowSelf();
		}
	}

	public override void Show(object context)
	{
		d.Log("UITechManagerHUD.Show");
		m_TechEntryHasFocus = false;
		m_TemporarilyHidden = false;
		base.Show(context);
		m_DisableTabsPrompt = !CanSwapToPlayerList();
		m_TechUniqueIDSelected = 0;
		FullyRebuildTechList();
		if (m_TabsContainer.IsNotNull())
		{
			m_TabsContainer.SetActive(Singleton.Manager<ManNetwork>.inst.IsMultiplayer());
		}
		Singleton.Manager<ManTechs>.inst.TankDriverChangedEvent.Subscribe(OnTechDriverChanged);
		Singleton.Manager<ManTechs>.inst.TankTeamChangedEvent.Subscribe(OnTechTeamChanged);
		Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Subscribe(OnTechNameChanged);
		Singleton.Manager<ManVisible>.inst.OnStartedTrackingVisible.Subscribe(OnTrackedVisibleAdded);
		Singleton.Manager<ManVisible>.inst.OnStoppedTrackingVisible.Subscribe(OnTrackedVisibleRemoved);
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			m_ExitButton.gameObject.SetActive(value: false);
		}
		UIBlockLimit.ShowUI(UIBlockLimit.ShowReason.TechManagerOpen);
	}

	public override void Hide(object context)
	{
		d.Log("UITechManagerHUD.Hide");
		UIBlockLimit.HideUI(UIBlockLimit.ShowReason.TechManagerOpen);
		base.Hide(context);
		Cleanup();
		m_EntryPool.FreeAll();
		UpdateSelectionBorder(null);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.FullscreenUI);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.UIMissionLog);
		Singleton.Manager<ManUI>.inst.HideScreenPrompt();
		Singleton.Manager<ManTechs>.inst.TankDriverChangedEvent.Unsubscribe(OnTechDriverChanged);
		Singleton.Manager<ManTechs>.inst.TankTeamChangedEvent.Unsubscribe(OnTechTeamChanged);
		Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Unsubscribe(OnTechNameChanged);
		Singleton.Manager<ManVisible>.inst.OnStartedTrackingVisible.Unsubscribe(OnTrackedVisibleAdded);
		Singleton.Manager<ManVisible>.inst.OnStoppedTrackingVisible.Unsubscribe(OnTrackedVisibleRemoved);
	}

	public void CloseMenu()
	{
		Cleanup();
		HideSelf();
	}

	public bool CanSwapToPlayerList()
	{
		return Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
	}

	public void SwapToPlayerList()
	{
		if (CanSwapToPlayerList())
		{
			HideSelf();
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.PlayerInfo);
		}
	}

	private bool IsPotentiallyInteresting(TrackedVisible tv)
	{
		if (tv == null)
		{
			return false;
		}
		if (tv.ObjectType != ObjectTypes.Vehicle)
		{
			return false;
		}
		if (tv.IsVendor)
		{
			return false;
		}
		return true;
	}

	private bool ShouldShowInTechManager(TrackedVisible tv)
	{
		if (!IsPotentiallyInteresting(tv))
		{
			return false;
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
			{
				NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
				if (player.CurTech != null && tv.HostID == player.CurTech.HostID)
				{
					return false;
				}
			}
		}
		else if (Singleton.playerTank.IsNotNull() && Singleton.playerTank.visible.ID == tv.ID)
		{
			return false;
		}
		if (ShowOnlyPlayerTeam)
		{
			if (tv.TeamID != Singleton.Manager<ManPlayer>.inst.PlayerTeam)
			{
				return false;
			}
			if (tv.RadarTeamID != Singleton.Manager<ManPlayer>.inst.PlayerTeam)
			{
				return false;
			}
		}
		else if (!ManSpawn.IsPlayerTeam(tv.TeamID))
		{
			if (tv.visible == null)
			{
				return false;
			}
			if (tv.visible.tank.IsNotNull())
			{
				BlockUnlockTable blockUnlockTable = Singleton.Manager<ManLicenses>.inst.GetBlockUnlockTable();
				BlockManager blockman = tv.visible.tank.blockman;
				for (int num = blockman.blockCount - 1; num >= 0; num--)
				{
					BlockTypes blockType = blockman.GetBlockWithIndex(num).BlockType;
					if (!blockUnlockTable.ContainsBlock(blockType))
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	private void FullyRebuildTechList()
	{
		m_EntryPool.SetNoneUsed();
		Cleanup();
		foreach (TrackedVisible allTrackedVisible in Singleton.Manager<ManVisible>.inst.AllTrackedVisibles)
		{
			if (ShouldShowInTechManager(allTrackedVisible))
			{
				AddEntry(allTrackedVisible, initNav: false);
			}
		}
		SortEntriesList();
		UITechManagerEntry prevNav = null;
		foreach (UITechManagerEntry entry in m_Entries)
		{
			entry.InitNavigation(prevNav);
			prevNav = entry;
		}
		if (m_Entries.Count > 0)
		{
			UITechManagerEntry selectedEntry = GetSelectedEntry();
			if (selectedEntry.IsNotNull())
			{
				selectedEntry.EnsureSelection();
			}
		}
		OnListCountModified();
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.UIMissionLog);
	}

	private void SortEntriesList()
	{
		if (m_Entries.Count > 0)
		{
			m_Entries[0].SetAsGamepadEntryPoint(set: false);
		}
		m_Entries.Sort(EntryComparer);
		int num = 0;
		if (m_TechsTitle.IsNotNull())
		{
			m_TechsTitle.SetSiblingIndex(0);
			num = 1;
		}
		for (int i = 0; i < m_Entries.Count; i++)
		{
			m_Entries[i].transform.SetSiblingIndex(i + num);
		}
		if (m_Entries.Count > 0)
		{
			m_Entries[0].SetAsGamepadEntryPoint(set: true);
		}
	}

	private static int EntryComparer(UITechManagerEntry entryA, UITechManagerEntry entryB)
	{
		int num = entryA.TeamID.CompareTo(entryB.TeamID);
		if (num != 0)
		{
			return num;
		}
		return entryA.UniqueID.CompareTo(entryB.UniqueID);
	}

	private void AddEntry(TrackedVisible tv, bool initNav = true)
	{
		if (tv.visible.IsNotNull() && Singleton.Manager<ManTechSwapper>.inst.TryGetInProgressTechOperation(tv.visible.tank, out var outOp))
		{
			m_TrackedVisiblesBeingOperatedOn.Add(tv);
			outOp.SubscribeToCompletionCallback(delegate
			{
				OnTechOperationCompleted(tv);
			});
			return;
		}
		int uniqueID = (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() ? tv.HostID : tv.ID);
		if (TryGetEntry(uniqueID, out var entry))
		{
			entry.Init(tv, AllowTeamSelection);
		}
		else
		{
			UITechManagerEntry uITechManagerEntry = m_EntryPool.Alloc(m_EntriesParent);
			uITechManagerEntry.Init(tv, AllowTeamSelection);
			uITechManagerEntry.HoverEvent.Subscribe(OnElementHovered);
			if (initNav)
			{
				UITechManagerEntry prevNav = ((m_Entries.Count > 0) ? m_Entries[m_Entries.Count - 1] : null);
				uITechManagerEntry.InitNavigation(prevNav);
			}
			m_Entries.Add(uITechManagerEntry);
		}
		OnListCountModified();
	}

	private void RemoveEntry(int uniqueID)
	{
		for (int i = 0; i < m_Entries.Count; i++)
		{
			UITechManagerEntry uITechManagerEntry = m_Entries[i];
			if (uITechManagerEntry.UniqueID != uniqueID)
			{
				continue;
			}
			bool flag = i > 0;
			bool flag2 = i < m_Entries.Count - 1;
			UITechManagerEntry uITechManagerEntry2 = (flag ? m_Entries[i - 1] : null);
			UITechManagerEntry uITechManagerEntry3 = (flag2 ? m_Entries[i + 1] : null);
			if (flag)
			{
				uITechManagerEntry2.SetupNavigationToNextNeighbour(uITechManagerEntry3);
			}
			if (flag2)
			{
				uITechManagerEntry3.SetupNavigationToPrevNeighbour(uITechManagerEntry2);
			}
			if (i == 0)
			{
				uITechManagerEntry.SetAsGamepadEntryPoint(set: false);
				if (m_Entries.Count > 1)
				{
					m_Entries[1].SetAsGamepadEntryPoint(set: true);
				}
			}
			if (uniqueID == m_TechUniqueIDSelected)
			{
				if (flag2)
				{
					uITechManagerEntry3.EnsureSelection();
				}
				else if (flag)
				{
					uITechManagerEntry2.EnsureSelection();
				}
				else
				{
					EventSystem.current.SetSelectedGameObject(null);
				}
			}
			uITechManagerEntry.Cleanup();
			m_Entries.RemoveAt(i);
			m_EntryPool.Free(uITechManagerEntry);
			break;
		}
		OnListCountModified();
	}

	private UITechManagerEntry GetSelectedEntry()
	{
		UITechManagerEntry result = null;
		if (m_TechUniqueIDSelected != -1)
		{
			result = m_Entries.Find((UITechManagerEntry e) => e.UniqueID == m_TechUniqueIDSelected);
		}
		return result;
	}

	private bool TryGetEntry(int uniqueID, out UITechManagerEntry entry)
	{
		for (int i = 0; i < m_Entries.Count; i++)
		{
			if (m_Entries[i].UniqueID == uniqueID)
			{
				entry = m_Entries[i];
				return true;
			}
		}
		entry = null;
		return false;
	}

	private void UpdateSelectionBorder(UITechManagerEntry selectedEntry)
	{
		if (selectedEntry != m_CurrentSelection)
		{
			if (m_CurrentSelection.IsNotNull())
			{
				m_CurrentSelection.SetSelectionBorderVisible(visible: false);
				m_CurrentSelection.UpdateEnemyLimitMessageForSelection(forceHide: true);
			}
			m_CurrentSelection = selectedEntry;
			if (m_CurrentSelection.IsNotNull())
			{
				m_CurrentSelection.EnsureSelection();
				m_CurrentSelection.SetSelectionBorderVisible(visible: true);
			}
			m_TechUniqueIDSelected = (m_CurrentSelection.IsNotNull() ? m_CurrentSelection.UniqueID : 0);
		}
	}

	private void OnListCountModified()
	{
		if (m_TechsCountText.IsNotNull())
		{
			m_TechsCountText.text = string.Format(m_TechsCountFormatStr.Value.ToUpper(), m_Entries.Count.ToString());
		}
		m_EntryPool.FreeUnused();
	}

	private void Cleanup()
	{
		foreach (UITechManagerEntry entry in m_Entries)
		{
			entry.Cleanup();
		}
		m_Entries.Clear();
		m_TrackedVisiblesToAdd.Clear();
		m_TrackedVisiblesToRemove.Clear();
		m_TrackedVisiblesPendingSpawn.Clear();
		m_TrackedVisiblesBeingOperatedOn.Clear();
	}

	public override bool HandleCustomEscapeKeyAction()
	{
		bool result = false;
		if (base.IsVisible)
		{
			if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
			{
				if (!(EventSystem.current.currentSelectedGameObject != null) || !(EventSystem.current.currentSelectedGameObject.GetComponent<Dropdown>() != null))
				{
					if (m_TechEntryHasFocus)
					{
						m_TechEntryHasFocus = false;
						UITechManagerEntry selectedEntry = GetSelectedEntry();
						if (selectedEntry.IsNotNull())
						{
							selectedEntry.EnsureSelection();
							selectedEntry.SetChildNavigationEntryPoint(set: false);
						}
					}
					else
					{
						EventSystem.current.SetSelectedGameObject(null);
						HideSelf();
					}
				}
			}
			else
			{
				EventSystem.current.SetSelectedGameObject(null);
				HideSelf();
			}
			result = true;
		}
		return result;
	}

	private void OnTrackedVisibleAdded(TrackedVisible tv)
	{
		if (!IsPotentiallyInteresting(tv))
		{
			return;
		}
		if (tv.visible.IsNotNull())
		{
			if (ShouldShowInTechManager(tv))
			{
				m_TrackedVisiblesToAdd.Add(tv);
			}
		}
		else if (Singleton.Manager<ManWorld>.inst.CheckIsTileAtPositionLoaded(tv.Position))
		{
			m_TrackedVisiblesPendingSpawn.Add(tv);
			tv.OnRespawnEvent.Subscribe(OnPendingTrackedVisibleSpawned);
		}
		else if (ShouldShowInTechManager(tv))
		{
			m_TrackedVisiblesToAdd.Add(tv);
		}
	}

	private void OnPendingTrackedVisibleSpawned(Visible vis)
	{
		TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(vis.ID);
		trackedVisible.OnRespawnEvent.Unsubscribe(OnPendingTrackedVisibleSpawned);
		if (m_TrackedVisiblesPendingSpawn.Remove(trackedVisible) && ShouldShowInTechManager(trackedVisible))
		{
			m_TrackedVisiblesToAdd.Add(trackedVisible);
		}
	}

	private void OnTrackedVisibleRemoved(TrackedVisible tv)
	{
		m_TrackedVisiblesToRemove.Add(tv);
	}

	private void OnTechOperationCompleted(TrackedVisible tv)
	{
		if (m_TrackedVisiblesBeingOperatedOn.Remove(tv))
		{
			AddEntry(tv);
		}
	}

	private void OnTechDriverChanged(Tank tech)
	{
		bool num = tech.netTech != null;
		int num2 = (num ? tech.netTech.HostID : tech.visible.ID);
		TrackedVisible tv = (num ? Singleton.Manager<ManVisible>.inst.GetTrackedVisibleByHostID(num2) : Singleton.Manager<ManVisible>.inst.GetTrackedVisible(num2));
		bool num3 = ShouldShowInTechManager(tv);
		UITechManagerEntry entry;
		bool flag = TryGetEntry(num2, out entry);
		if (num3)
		{
			if (flag)
			{
				entry.OnTechDriverChanged();
			}
			else
			{
				AddEntry(tv);
			}
			return;
		}
		if (flag)
		{
			RemoveEntry(entry.UniqueID);
		}
		RemovePendingAdd(tv);
	}

	private void OnTechTeamChanged(Tank tech, ManTechs.TeamChangeInfo info)
	{
		bool num = tech.netTech != null;
		int num2 = (num ? tech.netTech.HostID : tech.visible.ID);
		TrackedVisible tv = (num ? Singleton.Manager<ManVisible>.inst.GetTrackedVisibleByHostID(num2) : Singleton.Manager<ManVisible>.inst.GetTrackedVisible(num2));
		bool num3 = ShouldShowInTechManager(tv);
		UITechManagerEntry entry;
		bool flag = TryGetEntry(num2, out entry);
		if (num3)
		{
			if (flag)
			{
				entry.OnTeamUpdated(info.m_NewTeam);
			}
			else
			{
				AddEntry(tv);
			}
			return;
		}
		if (flag)
		{
			RemoveEntry(entry.UniqueID);
		}
		RemovePendingAdd(tv);
	}

	private void RemovePendingAdd(TrackedVisible tv)
	{
		if (m_TrackedVisiblesPendingSpawn.Remove(tv))
		{
			tv.OnRespawnEvent.Unsubscribe(OnPendingTrackedVisibleSpawned);
		}
		else
		{
			m_TrackedVisiblesToAdd.Remove(tv);
		}
	}

	private void OnTechNameChanged(Tank tech, TrackedVisible trackedVisible)
	{
		int uniqueID;
		if (tech.IsNotNull())
		{
			uniqueID = ((tech.netTech != null) ? tech.netTech.HostID : tech.visible.ID);
		}
		else
		{
			d.Assert(trackedVisible != null, "Tank namechange event without valid tech or tracked visible param!");
			uniqueID = trackedVisible.ID;
		}
		if (TryGetEntry(uniqueID, out var entry))
		{
			entry.OnTechNameChanged();
		}
	}

	private void OnElementHovered(UITechManagerEntry entry, bool gainedFocus)
	{
		if (gainedFocus)
		{
			UpdateSelectionBorder(entry);
		}
		else if (entry == m_CurrentSelection)
		{
			UpdateSelectionBorder(null);
		}
	}

	private void OnPool()
	{
		m_EntryPool = new UIElementCache<UITechManagerEntry>(m_TechEntryPrefab);
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.GamepadQuickMenuHUDElements);
	}

	private void OnRecycle()
	{
		Cleanup();
	}

	private void Update()
	{
		if ((bool)m_CurrentSelection)
		{
			m_CurrentSelection.UpdateEnemyLimitMessageForSelection();
		}
		if (m_DisableTabsPrompt)
		{
			Singleton.Manager<ManBtnPrompt>.inst.ToggleBtnPrompt(active: false, new Localisation.GlyphInfo[1]
			{
				new Localisation.GlyphInfo(41)
			});
			Singleton.Manager<ManBtnPrompt>.inst.ToggleBtnPrompt(active: false, new Localisation.GlyphInfo[1]
			{
				new Localisation.GlyphInfo(42)
			});
			m_DisableTabsPrompt = false;
		}
		foreach (TrackedVisible item in m_TrackedVisiblesToAdd)
		{
			AddEntry(item);
		}
		m_TrackedVisiblesToAdd.Clear();
		foreach (TrackedVisible item2 in m_TrackedVisiblesToRemove)
		{
			m_TrackedVisiblesBeingOperatedOn.Remove(item2);
			if (!m_TrackedVisiblesPendingSpawn.Remove(item2))
			{
				RemoveEntry(Singleton.Manager<ManNetwork>.inst.IsMultiplayer() ? item2.HostID : item2.ID);
			}
		}
		m_TrackedVisiblesToRemove.Clear();
		if (m_Entries.Count == 0)
		{
			m_TechEntryHasFocus = false;
		}
		int num = 0;
		while (num < m_Entries.Count)
		{
			UITechManagerEntry uITechManagerEntry = m_Entries[num];
			if (uITechManagerEntry.Expired)
			{
				m_Entries[num].transform.SetParent(null, worldPositionStays: false);
				m_Entries.RemoveAt(num);
			}
			else
			{
				uITechManagerEntry.Refresh();
				num++;
			}
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(29, ControllerType.Joystick))
		{
			EventSystem.current.SetSelectedGameObject(null);
			HideSelf();
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(41, ControllerType.Joystick) || Singleton.Manager<ManInput>.inst.GetButtonDown(42, ControllerType.Joystick))
		{
			SwapToPlayerList();
		}
	}
}
