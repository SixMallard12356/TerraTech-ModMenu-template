#define UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Binding;
using UnityEngine;

namespace Snapshots;

public class VMSnapshotPanel : MonoBehaviour
{
	public enum Platform
	{
		Local,
		Twitter,
		Steam,
		Null
	}

	public enum FocusTarget
	{
		Snapshots,
		Settings,
		Actions,
		Blocks,
		Search
	}

	public enum SelectionTypes
	{
		SnapshotItems,
		SnapshotFolders
	}

	public struct WarningData
	{
		public PlacementSelection.InvalidType m_WarningType;

		public string m_Text;
	}

	public Bindable<bool> m_ViewVisible = new Bindable<bool>(value: false);

	public Bindable<Platform> m_Platform = new Bindable<Platform>(Platform.Null);

	public Bindable<bool> m_SwapOptionEnabled = new Bindable<bool>(value: false);

	public Bindable<bool> m_SwapOptionVisible = new Bindable<bool>(value: false);

	public Bindable<bool> m_PlaceOptionEnabled = new Bindable<bool>(value: false);

	public Bindable<bool> m_PlaceOptionVisible = new Bindable<bool>(value: false);

	public Bindable<bool> m_SteamBtnVisible = new Bindable<bool>(value: false);

	public Bindable<bool> m_WarningsVisible = new Bindable<bool>(value: false);

	public Bindable<bool> m_OptionsVisbile = new Bindable<bool>(value: false);

	public Bindable<bool> m_ActionsVisbile = new Bindable<bool>(value: false);

	public Bindable<bool> m_SnapshotsVisible = new Bindable<bool>(value: false);

	public Bindable<bool> m_SearchVisible = new Bindable<bool>(value: false);

	public Bindable<bool> m_PlacementUserActivated = new Bindable<bool>(value: false);

	public Bindable<bool> m_BlockInfoVisible = new Bindable<bool>(value: false);

	public Bindable<SelectionTypes> m_LastSelectionType = new Bindable<SelectionTypes>(SelectionTypes.SnapshotItems);

	public Bindable<bool> m_FavouriteBtnEnabled = new Bindable<bool>(value: false);

	public Bindable<bool> m_SetFolderBtnEnabled = new Bindable<bool>(value: false);

	public Bindable<bool> m_RenameBtnEnabled = new Bindable<bool>(value: false);

	public Bindable<bool> m_DeleteBtnEnabled = new Bindable<bool>(value: false);

	public Bindable<bool> m_FolderRenameBtnEnabled = new Bindable<bool>(value: false);

	public Bindable<bool> m_FolderDeleteBtnEnabled = new Bindable<bool>(value: false);

	public Bindable<string> m_LoadingStatus = new Bindable<string>(string.Empty);

	public Bindable<bool> m_LoadingStatusVisible = new Bindable<bool>(value: false);

	public Bindable<string> m_SelectedFolderName = new Bindable<string>(string.Empty);

	public Bindable<SnapshotLiveData> m_Selected = new Bindable<SnapshotLiveData>(default(SnapshotLiveData));

	public Bindable<string> m_SelectedTechName = new Bindable<string>(string.Empty);

	public Bindable<string> m_SelectedTechCost = new Bindable<string>(string.Empty);

	public Bindable<string> m_SelectedBlockCost = new Bindable<string>(string.Empty);

	public Bindable<string> m_SelectedDateCreated = new Bindable<string>(string.Empty);

	public Bindable<string> m_SelectedTechCreator = new Bindable<string>(string.Empty);

	public Bindable<Sprite> m_SelectedSprite = new Bindable<Sprite>(null);

	public Bindable<bool> m_SelectedTechFavourite = new Bindable<bool>(value: false);

	public Bindable<string> m_SelectedTechFolder = new Bindable<string>(string.Empty);

	public Bindable<TechDataAvailValidation> m_SelectedBlockInfo = new Bindable<TechDataAvailValidation>(null);

	public Bindable<bool> m_SelectedUnvailableSwap = new Bindable<bool>(value: false);

	public Bindable<bool> m_SelectedUnvailablePlace = new Bindable<bool>(value: false);

	public Bindable<bool> m_ShowOnlyAvailable = new Bindable<bool>(value: false);

	public Bindable<bool> m_ShowOnlyFavourites = new Bindable<bool>(value: false);

	public Bindable<string> m_SearchFilterString = new Bindable<string>(string.Empty);

	public Bindable<int> m_SnapshotSortType = new Bindable<int>();

	public BindableList<string> m_SnapshotSortTypeString = new BindableList<string>();

	public Bindable<bool> m_SortDescending = new Bindable<bool>(value: false);

	public BindableList<WarningData> m_Warnings = new BindableList<WarningData>();

	public Bindable<bool> m_BlockLimiterInUse = new Bindable<bool>(value: false);

	public Bindable<string> m_SteamBtnLabel = new Bindable<string>(string.Empty);

	public BindableList<SnapshotLiveData> m_Snapshots = new BindableList<SnapshotLiveData>();

	public List<SnapshotFolderLiveData> m_SnapshotFolderCache = new List<SnapshotFolderLiveData>();

	public List<SnapshotFolderLiveData> m_SnapshotFoldersVisible = new List<SnapshotFolderLiveData>();

	public Event<Snapshot> OnSwapTech;

	private bool m_SnapshotsDirty;

	private ManSnapshots m_DataModel;

	private ISnapshotInteractionModel m_InteractionModel;

	private string[] m_SearchFilterGroups;

	private Dictionary<int, int> m_SortTypeIndexToValueLookup = new Dictionary<int, int>();

	private Bindable<FocusTarget> m_FocusTarget = new Bindable<FocusTarget>(FocusTarget.Snapshots);

	public void InjectInteractionModel(ISnapshotInteractionModel interactionModel)
	{
		m_InteractionModel = interactionModel;
		m_SelectedUnvailableSwap.Bind(delegate
		{
			UpdateWarnings();
		});
		m_SelectedUnvailablePlace.Bind(delegate
		{
			UpdateWarnings();
		});
		m_InteractionModel.SwapValidation.Bind(delegate
		{
			UpdateWarnings();
		});
		m_InteractionModel.PlaceValidation.Bind(delegate
		{
			UpdateWarnings();
		});
		m_Warnings.Bind(delegate
		{
			OnWarningsVisibleAdaptor();
		});
		m_FocusTarget.Bind(delegate
		{
			OnWarningsVisibleAdaptor();
		});
		m_Selected.Bind(delegate
		{
			OnWarningsVisibleAdaptor();
		});
		m_ViewVisible.Bind(delegate
		{
			OnValidateSwapAdaptor();
		});
		m_SwapOptionVisible.Bind(delegate
		{
			OnValidateSwapAdaptor();
		});
		m_SelectedUnvailableSwap.Bind(delegate
		{
			OnValidateSwapAdaptor();
		});
		m_ViewVisible.Bind(delegate
		{
			OnValidatePlaceAdaptor();
		});
		m_PlaceOptionVisible.Bind(delegate
		{
			OnValidatePlaceAdaptor();
		});
		m_PlacementUserActivated.Bind(delegate
		{
			OnValidatePlaceAdaptor();
		});
		m_SelectedUnvailablePlace.Bind(delegate
		{
			OnValidatePlaceAdaptor();
		});
		m_SelectedUnvailableSwap.Bind(delegate
		{
			OnSwapEnabledAdaptor();
		});
		m_InteractionModel.SwapValidation.Bind(delegate
		{
			OnSwapEnabledAdaptor();
		});
		m_SelectedUnvailablePlace.Bind(delegate
		{
			OnPlaceEnabledAdaptor();
		});
	}

	public void Show()
	{
		d.AssertFormat(m_InteractionModel != null, "ViewModel needs reference to interaction model to be filfulled before showing");
		m_ViewVisible.Value = true;
		m_DataModel.SetCollectionUpdateEnabled(enabled: true);
		SetFocusTarget(FocusTarget.Snapshots);
	}

	public void Hide()
	{
		m_ViewVisible.Value = false;
		m_DataModel.SetCollectionUpdateEnabled(enabled: false);
	}

	public void SetSelectedPlatform(Platform platform)
	{
		if (m_Platform.Value != platform)
		{
			ClearSnapshotSelection();
		}
		m_Platform.Value = platform;
		m_SteamBtnVisible.Value = platform == Platform.Steam;
		m_PlacementUserActivated.Value = false;
		m_DataModel.LoadPlatform(platform);
	}

	public void SelectFirstOrDefaultItemOrFolder()
	{
		if (m_Snapshots.Count > 0)
		{
			SetSelectedSnapshot(0);
			return;
		}
		if (m_SnapshotFoldersVisible.Count > 0)
		{
			SetSelectedSnapshotFolder(0);
			return;
		}
		ClearSnapshotSelection();
		ClearFolderSelection();
	}

	public void SetSelectedSnapshot(int index)
	{
		if (m_Snapshots.Count <= 0)
		{
			ClearSnapshotSelection();
			return;
		}
		if (index < 0)
		{
			index = 0;
		}
		else if (index >= m_Snapshots.Count)
		{
			index = m_Snapshots.Count - 1;
		}
		SnapshotLiveData selectedSnapshot = m_Snapshots[index];
		SetSelectedSnapshot(selectedSnapshot);
	}

	public void SetHotSwapID(int index)
	{
		if (m_Selected.Value.m_Snapshot is SnapshotDisk snapshot)
		{
			Singleton.Manager<ManPlayer>.inst.SetHotswapBinding(snapshot, index);
			return;
		}
		d.LogErrorFormat("Cannot set HotSwap with snapshot '{0}' - not a SnapshotDisk", m_Selected.Value.m_Snapshot.m_Name.Value);
	}

	public void SetSelectedSnapshotFolder(int folderIndex)
	{
		SetSelectedSnapshotFolder(m_SnapshotFoldersVisible[folderIndex].Name);
	}

	public void SetSelectedSnapshotFolder(string snapFolderName)
	{
		m_SelectedFolderName.Value = snapFolderName;
		bool flag = !snapFolderName.NullOrEmpty();
		if (flag)
		{
			ClearSnapshotSelection();
			m_LastSelectionType.Value = SelectionTypes.SnapshotFolders;
		}
		m_FolderRenameBtnEnabled.Value = flag;
		m_FolderDeleteBtnEnabled.Value = flag;
	}

	public void SetSelectedSnapshot(SnapshotLiveData snapshotData)
	{
		Snapshot snapshot = m_Selected.Value.m_Snapshot;
		TechDataAvailValidation validData = m_Selected.Value.m_ValidData;
		if (snapshot != null)
		{
			snapshot.m_Name.Unbind(m_SelectedTechName);
			snapshot.m_Meta.Unbind(OnSelectedTechMetadataChanged);
		}
		if (validData != null)
		{
			validData.m_UnavailableSwap.Unbind(m_SelectedUnvailableSwap);
			validData.m_UnavailablePlace.Unbind(m_SelectedUnvailablePlace);
		}
		m_Selected.Value = snapshotData;
		Snapshot snapshot2 = snapshotData.m_Snapshot;
		if (snapshot2 != null)
		{
			ClearFolderSelection();
			m_LastSelectionType.Value = SelectionTypes.SnapshotItems;
			snapshot2.m_Name.Bind(m_SelectedTechName);
			snapshot2.m_Meta.Bind(OnSelectedTechMetadataChanged);
			RefreshLocalisedStrings();
			snapshot2.ResolveThumbnail(delegate(Sprite s)
			{
				m_SelectedSprite.Value = s;
			});
			m_SelectedBlockInfo.Value = snapshotData.m_ValidData;
			snapshotData.m_ValidData.m_UnavailableSwap.Bind(m_SelectedUnvailableSwap);
			snapshotData.m_ValidData.m_UnavailablePlace.Bind(m_SelectedUnvailablePlace);
			m_FavouriteBtnEnabled.Value = m_DataModel.SupportsFavourites();
			m_RenameBtnEnabled.Value = m_DataModel.SupportsRenameAndDelete();
			m_DeleteBtnEnabled.Value = m_DataModel.SupportsRenameAndDelete();
			m_SetFolderBtnEnabled.Value = m_DataModel.SupportsFolders();
		}
		else
		{
			m_SelectedTechName.Value = string.Empty;
			RefreshLocalisedStrings();
			m_SelectedSprite.Value = null;
			m_SelectedTechFavourite.Value = false;
			m_SelectedTechFolder.Value = string.Empty;
			m_SelectedBlockInfo.Value = null;
			m_SelectedUnvailableSwap.Value = true;
			m_SelectedUnvailablePlace.Value = true;
			m_FavouriteBtnEnabled.Value = false;
			m_RenameBtnEnabled.Value = false;
			m_DeleteBtnEnabled.Value = false;
			m_SetFolderBtnEnabled.Value = false;
		}
		UpdateWarnings();
	}

	public void SetSelectedSnapshotAsFavourite(bool isFavourite)
	{
		SnapshotLiveData value = m_Selected.Value;
		m_DataModel.SetSnapshotFavourite(value, isFavourite);
	}

	public void Swap()
	{
		if (m_ViewVisible.Value)
		{
			SnapshotLiveData value = m_Selected.Value;
			if (value.m_Snapshot != null && m_SwapOptionEnabled.Value)
			{
				OnSwapTech.Send(value.m_Snapshot);
			}
		}
	}

	public bool Place()
	{
		if (!m_ViewVisible.Value)
		{
			return false;
		}
		if (m_Selected.Value.m_Snapshot != null && m_PlaceOptionEnabled.Value && !m_PlacementUserActivated.Value)
		{
			m_PlacementUserActivated.Value = true;
			return true;
		}
		return false;
	}

	public void ConfirmPlace()
	{
		m_PlacementUserActivated.Value = false;
	}

	public void CancelPlace()
	{
		m_PlacementUserActivated.Value = false;
	}

	public void ToggleBlockInfo()
	{
		if (m_BlockInfoVisible.Value)
		{
			HideBlockInfo();
		}
		else
		{
			ShowBlockInfo();
		}
	}

	public void ShowBlockInfo()
	{
		if (m_ViewVisible.Value)
		{
			SetFocusTarget(FocusTarget.Blocks);
		}
	}

	public void HideBlockInfo()
	{
		if (m_ViewVisible.Value && m_FocusTarget.Value == FocusTarget.Blocks)
		{
			SetFocusTarget(FocusTarget.Actions);
		}
	}

	public void ToggleFocus()
	{
		if (m_ViewVisible.Value)
		{
			switch (m_FocusTarget.Value)
			{
			case FocusTarget.Snapshots:
				SetFocusTarget(FocusTarget.Actions);
				break;
			case FocusTarget.Settings:
				SetFocusTarget(FocusTarget.Snapshots);
				break;
			case FocusTarget.Actions:
				SetFocusTarget(FocusTarget.Snapshots);
				break;
			default:
				d.LogErrorFormat("VMSnapshotPanel - no mapping found for focus target: {0}", m_FocusTarget.Value);
				break;
			case FocusTarget.Blocks:
			case FocusTarget.Search:
				break;
			}
		}
	}

	public void IncrementFocusPanel()
	{
		switch (m_FocusTarget.Value)
		{
		case FocusTarget.Snapshots:
			SetFocusTarget(FocusTarget.Actions);
			break;
		case FocusTarget.Settings:
			SetFocusTarget(FocusTarget.Snapshots);
			break;
		default:
			d.LogErrorFormat("VMSnapshotPanel - no mapping found for focus target: {0}", m_FocusTarget.Value);
			break;
		case FocusTarget.Actions:
		case FocusTarget.Blocks:
		case FocusTarget.Search:
			break;
		}
	}

	public void DecrementFocusPanel()
	{
		switch (m_FocusTarget.Value)
		{
		case FocusTarget.Snapshots:
			SetFocusTarget(FocusTarget.Settings);
			break;
		case FocusTarget.Actions:
			SetFocusTarget(FocusTarget.Snapshots);
			break;
		case FocusTarget.Search:
			SetFocusTarget(FocusTarget.Actions);
			break;
		default:
			d.LogErrorFormat("VMSnapshotPanel - no mapping found for focus target: {0}", m_FocusTarget.Value);
			break;
		case FocusTarget.Settings:
		case FocusTarget.Blocks:
			break;
		}
	}

	public bool CloseFocused()
	{
		if (!m_ViewVisible.Value)
		{
			return false;
		}
		if (m_PlacementUserActivated.Value)
		{
			m_PlacementUserActivated.Value = false;
			return true;
		}
		_ = m_FocusTarget.Value;
		switch (m_FocusTarget.Value)
		{
		case FocusTarget.Snapshots:
			return false;
		case FocusTarget.Settings:
			return false;
		case FocusTarget.Actions:
			return false;
		case FocusTarget.Blocks:
			SetFocusTarget(FocusTarget.Actions);
			return true;
		case FocusTarget.Search:
			SetFocusTarget(FocusTarget.Actions);
			return true;
		default:
			d.LogErrorFormat("VMSnapshotPanel - no mapping found for focus target: {0}", m_FocusTarget.Value);
			return false;
		}
	}

	public void DeleteSelected()
	{
		if (!m_ViewVisible.Value)
		{
			return;
		}
		if (m_LastSelectionType.Value == SelectionTypes.SnapshotItems)
		{
			SnapshotLiveData value = m_Selected.Value;
			if (value.m_Snapshot != null)
			{
				int num = m_Snapshots.IndexOf(value);
				m_DataModel.Delete(value);
				SetSelectedSnapshot(num + 1);
			}
		}
		else
		{
			if (m_LastSelectionType.Value != SelectionTypes.SnapshotFolders)
			{
				return;
			}
			string value2 = m_SelectedFolderName.Value;
			if (value2.NullOrEmpty())
			{
				return;
			}
			foreach (SnapshotLiveData snapshot in m_Snapshots)
			{
				if (snapshot.m_Snapshot.m_Meta.Value.FolderName == value2)
				{
					m_DataModel.SetFolder(snapshot, string.Empty);
				}
			}
			RemoveFolder(value2);
			SelectFirstOrDefaultItemOrFolder();
		}
	}

	public void SearchClicked()
	{
		if (m_FocusTarget.Value == FocusTarget.Search)
		{
			SetFocusTarget(FocusTarget.Actions);
		}
		else
		{
			SetFocusTarget(FocusTarget.Search);
		}
	}

	public void ReturnToTechList()
	{
		if (m_ViewVisible.Value)
		{
			SetFocusTarget(FocusTarget.Snapshots);
		}
	}

	public void SetFolder(string folderName)
	{
		SnapshotLiveData value = m_Selected.Value;
		d.Log($"VMSnapshotPanel.SetFolder newFolder='{folderName}' m_Selected.Value={value}");
		if (!m_ViewVisible.Value)
		{
			d.Log("VMSnapshotPanel.SetFolder ignored - view is hidden");
		}
		else if (value.m_Snapshot != null)
		{
			m_DataModel.SetFolder(value, folderName);
		}
	}

	public IEnumerable<string> GetAllFolderNames(bool searchEntireCache)
	{
		HashSet<string> hashSet = new HashSet<string>();
		if (searchEntireCache)
		{
			for (int i = 0; i < m_SnapshotFolderCache.Count; i++)
			{
				hashSet.Add(m_SnapshotFolderCache[i].Name);
			}
		}
		else
		{
			for (int j = 0; j < m_SnapshotFoldersVisible.Count; j++)
			{
				hashSet.Add(m_SnapshotFoldersVisible[j].Name);
			}
		}
		return hashSet;
	}

	public void AddFolder(string folderName)
	{
		if (!m_SnapshotFolderCache.Any((SnapshotFolderLiveData r) => r.Name == folderName))
		{
			m_SnapshotFolderCache.Add(new SnapshotFolderLiveData(folderName, isExpanded: true));
			RefreshList();
		}
	}

	public void RemoveFolder(string folderName)
	{
		int num = -1;
		for (int i = 0; i < m_SnapshotFolderCache.Count; i++)
		{
			if (m_SnapshotFolderCache[i].Name == folderName)
			{
				num = i;
				break;
			}
		}
		if (num != -1)
		{
			m_SnapshotFolderCache.RemoveAt(num);
			RefreshList();
		}
	}

	public bool GetIsFolderExpanded(string folderName)
	{
		d.AssertFormat(m_SnapshotFolderCache.Any((SnapshotFolderLiveData x) => x.Name == folderName), "Attempting to access folder with name {0} but no such folder exists in this context", folderName);
		return m_SnapshotFolderCache.First((SnapshotFolderLiveData r) => r.Name == folderName).IsExpanded;
	}

	public void SetFolderExpanded(string folderName, bool state)
	{
		d.AssertFormat(m_SnapshotFolderCache.Any((SnapshotFolderLiveData x) => x.Name == folderName), "Attempting to access folder with name {0} but no such folder exists in this context", folderName);
		for (int num = 0; num < m_SnapshotFolderCache.Count; num++)
		{
			if (m_SnapshotFolderCache[num].Name == folderName)
			{
				SnapshotFolderLiveData value = m_SnapshotFolderCache[num];
				value.IsExpanded = state;
				m_SnapshotFolderCache[num] = value;
				break;
			}
		}
		RefreshList();
	}

	public void RenameSelected(string newName)
	{
		if (!m_ViewVisible.Value)
		{
			d.Log("VMSnapshotPanel.Rename ignored - view is hidden");
		}
		else if (m_LastSelectionType.Value == SelectionTypes.SnapshotItems)
		{
			d.Log($"VMSnapshotPanel.Rename newName='{newName}' m_Selected.Value={m_Selected.Value}");
			SnapshotLiveData value = m_Selected.Value;
			if (value.m_Snapshot != null)
			{
				m_DataModel.Rename(value, newName);
			}
		}
		else
		{
			if (m_LastSelectionType.Value != SelectionTypes.SnapshotFolders)
			{
				return;
			}
			string value2 = m_SelectedFolderName.Value;
			if (newName.NullOrEmpty())
			{
				return;
			}
			bool isFolderExpanded = GetIsFolderExpanded(value2);
			foreach (SnapshotLiveData snapshot in m_DataModel.m_Snapshots)
			{
				if (snapshot.m_Snapshot.m_Meta.Value.FolderName == value2)
				{
					m_DataModel.SetFolder(snapshot, newName);
				}
			}
			RemoveFolder(value2);
			AddFolder(newName);
			SetSelectedSnapshotFolder(newName);
			SetFolderExpanded(newName, isFolderExpanded);
		}
	}

	public void SetShowOnlyAvailable(bool showOnlyAvail)
	{
		m_ShowOnlyAvailable.Value = showOnlyAvail;
		RefreshList();
	}

	public void SetShowOnlyFavourites(bool showOnlyFav)
	{
		m_ShowOnlyFavourites.Value = showOnlyFav;
		RefreshList();
	}

	public void SetSearchFilterString(string searchStr)
	{
		m_SearchFilterString.Value = searchStr;
		if (!m_SearchFilterString.Value.NullOrEmpty())
		{
			m_SearchFilterGroups = Util.GetSearchFilters(m_SearchFilterString.Value);
		}
		else
		{
			m_SearchFilterGroups = null;
		}
		RefreshList();
	}

	public void SetSortType(int chosenIndex)
	{
		if (!m_SortTypeIndexToValueLookup.TryGetValue(chosenIndex, out var value))
		{
			d.LogErrorFormat("VMSnapshotPanel.SetSortType - Could not map chosen index {0} to valid sort type. Falling back to sort by Name!", chosenIndex);
			value = 0;
		}
		m_SnapshotSortType.Value = value;
		RefreshList();
	}

	public void SetSortDecending(bool decending)
	{
		m_SortDescending.Value = decending;
		RefreshList();
	}

	public void GoToWorkshop()
	{
		if (m_Selected.Value.m_Snapshot is SnapshotSteam snapshotSteam)
		{
			string workshopItemURL = Singleton.Manager<ManSteamworks>.inst.GetWorkshopItemURL(snapshotSteam.UniqueID);
			Singleton.Manager<ManSteamworks>.inst.OpenOverlayURL(workshopItemURL);
		}
		else
		{
			Singleton.Manager<ManSteamworks>.inst.OpenOverlayURL("http://steamcommunity.com/app/285920/workshop/");
		}
	}

	private void SetFocusTarget(FocusTarget focusTarget)
	{
		m_FocusTarget.Value = focusTarget;
		switch (focusTarget)
		{
		case FocusTarget.Snapshots:
			m_OptionsVisbile.Value = false;
			m_BlockInfoVisible.Value = false;
			m_PlacementUserActivated.Value = false;
			m_ActionsVisbile.Value = false;
			m_SnapshotsVisible.Value = true;
			m_SearchVisible.Value = false;
			break;
		case FocusTarget.Settings:
			m_OptionsVisbile.Value = true;
			m_BlockInfoVisible.Value = false;
			m_PlacementUserActivated.Value = false;
			m_ActionsVisbile.Value = false;
			m_SnapshotsVisible.Value = false;
			m_SearchVisible.Value = false;
			break;
		case FocusTarget.Blocks:
			m_OptionsVisbile.Value = false;
			m_BlockInfoVisible.Value = true;
			m_PlacementUserActivated.Value = false;
			m_ActionsVisbile.Value = false;
			m_SnapshotsVisible.Value = false;
			m_SearchVisible.Value = false;
			break;
		case FocusTarget.Actions:
			m_OptionsVisbile.Value = false;
			m_BlockInfoVisible.Value = false;
			m_PlacementUserActivated.Value = false;
			m_ActionsVisbile.Value = true;
			m_SnapshotsVisible.Value = false;
			m_SearchVisible.Value = false;
			break;
		case FocusTarget.Search:
			m_OptionsVisbile.Value = false;
			m_BlockInfoVisible.Value = false;
			m_PlacementUserActivated.Value = false;
			m_ActionsVisbile.Value = false;
			m_SnapshotsVisible.Value = false;
			m_SearchVisible.Value = true;
			break;
		default:
			d.LogErrorFormat("VMSnapshotPanel.SetFocusTarget - no mapping found for FocusTarget {0}", focusTarget);
			break;
		}
	}

	public bool HandleCustomEscapeKey()
	{
		bool result = false;
		if (m_PlacementUserActivated.Value)
		{
			m_PlacementUserActivated.Value = false;
			return true;
		}
		switch (m_FocusTarget.Value)
		{
		case FocusTarget.Snapshots:
		case FocusTarget.Settings:
		case FocusTarget.Actions:
			result = false;
			break;
		case FocusTarget.Blocks:
			if (!Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
			{
				CloseFocused();
				result = true;
			}
			else
			{
				result = true;
			}
			break;
		default:
			d.LogErrorFormat("VMSnapshotPanel.HandleCustomeEscapeKey - no mapping found for FocusTarget {0}", m_FocusTarget.Value);
			break;
		}
		return result;
	}

	public FocusTarget GetFocusTarget()
	{
		return m_FocusTarget.Value;
	}

	private void RefreshLocalisedStrings()
	{
		if (m_Selected.Value.m_Snapshot == null)
		{
			m_SteamBtnLabel.Value = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 4);
			m_SelectedTechCost.Value = string.Empty;
			m_SelectedBlockCost.Value = string.Empty;
			m_SelectedTechCreator.Value = string.Empty;
			m_SelectedDateCreated.Value = string.Empty;
		}
		else
		{
			m_SteamBtnLabel.Value = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 3);
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechLoader, 13);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechLoader, 14);
			string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechLoader, 16);
			string localisedString4 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechLoader, 15);
			m_SelectedTechCost.Value = string.Format(localisedString, Singleton.Manager<Localisation>.inst.GetMoneyString(m_Selected.Value.m_ValidData.m_BlockBBCost));
			m_SelectedBlockCost.Value = string.Format(localisedString2, Singleton.Manager<Localisation>.inst.GetMoneyString(m_Selected.Value.m_ValidData.m_LimiterCost));
			m_SelectedTechCreator.Value = string.Format(localisedString3, m_Selected.Value.m_Snapshot.creator);
			m_SelectedDateCreated.Value = string.Format(localisedString4, Singleton.Manager<Localisation>.inst.GetDateString(m_Selected.Value.m_Snapshot.DateCreated));
		}
	}

	private void UpdateSnapshots()
	{
		BindableList<SnapshotLiveData> snapshotCollection = m_DataModel.SnapshotCollection;
		ManSnapshots.QueryStatus value = m_DataModel.m_QueryStatus.Value;
		string folderToReselect = m_SelectedFolderName.Value;
		RebuildFolders();
		SnapshotLiveData snapshotLiveData = m_Selected.Value;
		for (int num = m_Snapshots.Count - 1; num >= 0; num--)
		{
			if (m_Snapshots[num] != snapshotLiveData)
			{
				RemoveSnapshot(num);
			}
			else
			{
				if (!IsItemVisible(snapshotLiveData) || !snapshotCollection.Contains(snapshotLiveData))
				{
					if (!snapshotLiveData.m_Snapshot.m_Meta.Value.FolderName.NullOrEmpty())
					{
						d.Assert(folderToReselect.NullOrEmpty(), "Attempting to switch selected snapshot folder based on previous snapshot selection while we already have a folder to reselect!");
						folderToReselect = snapshotLiveData.m_Snapshot.m_Meta.Value.FolderName;
					}
					snapshotLiveData = default(SnapshotLiveData);
				}
				if (snapshotLiveData == default(SnapshotLiveData))
				{
					RemoveSnapshot(num);
				}
			}
		}
		if (!folderToReselect.NullOrEmpty() && !m_SnapshotFoldersVisible.Any((SnapshotFolderLiveData r) => r.Name == folderToReselect))
		{
			folderToReselect = string.Empty;
		}
		bool flag = false;
		for (int num2 = 0; num2 < snapshotCollection.Count; num2++)
		{
			if (!IsItemVisibleWithCurrentFilters(snapshotCollection[num2]))
			{
				continue;
			}
			if (IsItemVisibleInFolder(snapshotCollection[num2]))
			{
				if (snapshotCollection[num2] != snapshotLiveData)
				{
					AddSnapshot(snapshotCollection[num2]);
				}
				else
				{
					UpdateSnapshot(snapshotLiveData, 0);
				}
			}
			else
			{
				flag = true;
			}
		}
		m_Snapshots.Sort(SnapshotLiveData.GetSortComparer(m_SnapshotSortType.Value, m_SnapshotFoldersVisible, m_SortDescending.Value));
		if (snapshotLiveData != default(SnapshotLiveData))
		{
			SetSelectedSnapshot(snapshotLiveData);
		}
		else if (!folderToReselect.NullOrEmpty())
		{
			SetSelectedSnapshotFolder(folderToReselect);
		}
		else
		{
			SelectFirstOrDefaultItemOrFolder();
		}
		RefreshLoadingText(m_Snapshots.Count > 0 || flag, value, m_Platform.Value);
		m_SnapshotsDirty = false;
	}

	private void AddSnapshot(SnapshotLiveData snapshotData)
	{
		m_Snapshots.Add(snapshotData);
		snapshotData.m_Snapshot.m_Meta.Bind(OnMetadataChanged);
	}

	private void RemoveSnapshot(int index)
	{
		if (index < 0 || index >= m_Snapshots.Count)
		{
			d.LogErrorFormat("VMSnapshotPanel - index {0} out of bounds {0}", index, m_Snapshots.Count);
			return;
		}
		SnapshotLiveData snapshotLiveData = m_Snapshots[index];
		if (snapshotLiveData == m_Selected.Value)
		{
			ClearSnapshotSelection();
		}
		snapshotLiveData.m_Snapshot.m_Meta.Unbind(OnMetadataChanged);
		m_Snapshots.RemoveAt(index);
	}

	private void UpdateSnapshot(SnapshotLiveData snapshotData, int index)
	{
		m_Snapshots[index] = snapshotData;
	}

	private void RebuildFolders()
	{
		BindableList<SnapshotLiveData> snapshotCollection = m_DataModel.SnapshotCollection;
		foreach (SnapshotFolderLiveData item in m_SnapshotFolderCache)
		{
			item.Snapshots.Clear();
		}
		foreach (SnapshotLiveData item2 in snapshotCollection)
		{
			string snapFolderName = item2.m_Snapshot.m_Meta.Value.FolderName;
			if (snapFolderName.NullOrEmpty())
			{
				continue;
			}
			if (!m_SnapshotFolderCache.Any((SnapshotFolderLiveData r) => r.Name == snapFolderName))
			{
				m_SnapshotFolderCache.Add(new SnapshotFolderLiveData(snapFolderName, isExpanded: false));
			}
			for (int num = 0; num < m_SnapshotFolderCache.Count; num++)
			{
				if (!(m_SnapshotFolderCache[num].Name != snapFolderName))
				{
					m_SnapshotFolderCache[num].Snapshots.Add(item2);
					break;
				}
			}
		}
		m_SnapshotFolderCache.Sort(SnapshotFolderLiveData.GetSortComparer());
		m_SnapshotFoldersVisible.Clear();
		if (m_DataModel.SupportsFolders())
		{
			for (int num2 = 0; num2 < m_SnapshotFolderCache.Count; num2++)
			{
				m_SnapshotFoldersVisible.Add(m_SnapshotFolderCache[num2]);
			}
		}
	}

	private void ClearSnapshotSelection()
	{
		SetSelectedSnapshot(default(SnapshotLiveData));
	}

	private void ClearFolderSelection()
	{
		SetSelectedSnapshotFolder(string.Empty);
	}

	private void RefreshLoadingText(bool hasAnyItems, ManSnapshots.QueryStatus queryStatus, Platform platform)
	{
		if (hasAnyItems)
		{
			m_LoadingStatusVisible.Value = false;
			return;
		}
		m_LoadingStatusVisible.Value = true;
		switch (queryStatus)
		{
		case ManSnapshots.QueryStatus.Requesting:
			switch (platform)
			{
			case Platform.Local:
			case Platform.Steam:
				m_LoadingStatus.Value = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 39);
				break;
			case Platform.Twitter:
				m_LoadingStatus.Value = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 40);
				break;
			case Platform.Null:
				m_LoadingStatus.Value = string.Empty;
				break;
			default:
				d.LogErrorFormat("QueryStatus.Requesting - platform {0} not implemented", platform);
				break;
			}
			break;
		case ManSnapshots.QueryStatus.Done:
		case ManSnapshots.QueryStatus.AuthFailed:
			switch (platform)
			{
			case Platform.Local:
			case Platform.Twitter:
				m_LoadingStatus.Value = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 41);
				break;
			case Platform.Steam:
				m_LoadingStatus.Value = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 5);
				break;
			default:
				d.LogErrorFormat("QueryStatus.Done - platform {0} not implemented", platform);
				break;
			}
			break;
		}
	}

	private bool IsItemVisible(SnapshotLiveData snapshotData)
	{
		if (IsItemVisibleInFolder(snapshotData))
		{
			return IsItemVisibleWithCurrentFilters(snapshotData);
		}
		return false;
	}

	private bool IsItemVisibleInFolder(SnapshotLiveData snapshotData)
	{
		if (IsItemInActiveFolder(snapshotData))
		{
			return GetFolderForItem(snapshotData).IsExpanded;
		}
		return true;
	}

	private SnapshotFolderLiveData GetFolderForItem(SnapshotLiveData snapshotData)
	{
		return m_SnapshotFolderCache.Where((SnapshotFolderLiveData r) => r.Name == snapshotData.m_Snapshot.m_Meta.Value.FolderName).First();
	}

	private bool IsItemInActiveFolder(SnapshotLiveData snapshotData)
	{
		bool num = !snapshotData.m_Snapshot.m_Meta.Value.FolderName.NullOrEmpty();
		bool flag = m_SnapshotFoldersVisible.Any((SnapshotFolderLiveData r) => r.Name == snapshotData.m_Snapshot.m_Meta.Value.FolderName);
		return num && flag;
	}

	private bool IsItemVisibleWithCurrentFilters(SnapshotLiveData snapshotData)
	{
		if (snapshotData.m_Snapshot == null)
		{
			return false;
		}
		if (m_ShowOnlyAvailable.Value && snapshotData.m_ValidData.UnavailableSwap)
		{
			return false;
		}
		if (m_ShowOnlyFavourites.Value && !snapshotData.m_Snapshot.m_Meta.Value.IsFavourite)
		{
			return false;
		}
		if (!m_SearchFilterString.Value.NullOrEmpty() && m_SearchFilterGroups != null && m_SearchFilterGroups.Length != 0 && !Util.StringPassesSearchFilter(snapshotData.m_Snapshot.techData.Name, m_SearchFilterGroups))
		{
			return false;
		}
		return true;
	}

	private void RefreshList()
	{
		m_SnapshotsDirty = true;
	}

	private void UpdateSortingTypeDropdownChoices()
	{
		m_SnapshotSortTypeString.Clear();
		int num = 0;
		m_SortTypeIndexToValueLookup.Clear();
		SnapshotLiveData.SnapshotSortType value = (SnapshotLiveData.SnapshotSortType)m_SnapshotSortType.Value;
		int value2 = 0;
		EnumValuesIterator<SnapshotLiveData.SnapshotSortType> enumerator = EnumIterator<SnapshotLiveData.SnapshotSortType>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			SnapshotLiveData.SnapshotSortType current = enumerator.Current;
			if (current != SnapshotLiveData.SnapshotSortType.TechBlockLimitCost || Singleton.Manager<ManBlockLimiter>.inst.LimiterActive)
			{
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechLoaderSortTypes, (int)current);
				m_SnapshotSortTypeString.Add(localisedString);
				m_SortTypeIndexToValueLookup.Add(num, (int)current);
				if (current == value)
				{
					value2 = num;
				}
				num++;
			}
		}
		m_SnapshotSortType.Value = value2;
	}

	private void UpdateWarnings()
	{
		if (m_Selected.Value.m_Snapshot == null)
		{
			m_Warnings.Clear();
			return;
		}
		SnapshotLiveData value = m_Selected.Value;
		PlacementSelection.InvalidResult value2 = m_InteractionModel.SwapValidation.Value;
		PlacementSelection.InvalidResult value3 = m_InteractionModel.PlaceValidation.Value;
		m_Warnings.Clear();
		if (value.m_ValidData != null && value.m_ValidData.HasMissingBlocksPlace)
		{
			WarningData item = new WarningData
			{
				m_WarningType = PlacementSelection.InvalidType.Place,
				m_Text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechPlacement, 1)
			};
			m_Warnings.Add(item);
		}
		if (m_SwapOptionVisible.Value)
		{
			if (!value.m_ValidData.m_HasPlayerCab)
			{
				WarningData item2 = new WarningData
				{
					m_WarningType = PlacementSelection.InvalidType.Swap,
					m_Text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechPlacement, 15)
				};
				m_Warnings.Add(item2);
			}
			if (value.m_ValidData.m_ExceedsBlockLimitSwap)
			{
				WarningData item3 = new WarningData
				{
					m_WarningType = PlacementSelection.InvalidType.Swap,
					m_Text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechPlacement, 13)
				};
				m_Warnings.Add(item3);
			}
			if (!value2.IsValid)
			{
				PlacementSelection.InvalidResult invalidResult = value2;
				WarningData item4 = new WarningData
				{
					m_WarningType = invalidResult.m_Type,
					m_Text = TechPlacementHelper.GetPlacementBlockedReasonText(invalidResult.m_Flags)
				};
				m_Warnings.Add(item4);
			}
		}
		if (m_PlaceOptionVisible.Value)
		{
			if (value.m_ValidData.m_ExceedsBlockLimitPlace)
			{
				WarningData item5 = new WarningData
				{
					m_WarningType = PlacementSelection.InvalidType.Place,
					m_Text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechPlacement, 13)
				};
				m_Warnings.Add(item5);
			}
			if (!value3.IsValid)
			{
				PlacementSelection.InvalidResult invalidResult2 = value3;
				WarningData item6 = new WarningData
				{
					m_WarningType = invalidResult2.m_Type,
					m_Text = TechPlacementHelper.GetPlacementBlockedReasonText(invalidResult2.m_Flags)
				};
				m_Warnings.Add(item6);
			}
		}
	}

	private void OnSelectedTechMetadataChanged(Snapshot.MetaData metadata)
	{
		m_SelectedTechFavourite.Value = metadata.IsFavourite;
		m_SelectedTechFolder.Value = metadata.FolderName;
	}

	private void OnProfileChanged(ManProfile.Profile newProfile)
	{
		m_SnapshotFolderCache.Clear();
		m_SnapshotsDirty = true;
	}

	private void OnSnapshotCollectionChanged(BindableList<SnapshotLiveData> list, int index, BindableChange changedType)
	{
		m_SnapshotsDirty = true;
	}

	private void OnQueryStatusChanged(ManSnapshots.QueryStatus queryStatus)
	{
		m_SnapshotsDirty = true;
	}

	private void OnMetadataChanged(Snapshot.MetaData metadata)
	{
		RefreshList();
	}

	private void OnWarningsVisibleAdaptor()
	{
		bool flag = m_FocusTarget.Value != FocusTarget.Blocks;
		bool flag2 = m_Warnings.Count > 0;
		bool flag3 = m_Selected.Value.m_Snapshot != null;
		m_WarningsVisible.Value = flag && flag2 && flag3;
	}

	private void OnValidateSwapAdaptor()
	{
		bool flag = m_ViewVisible.Value && m_SwapOptionVisible.Value;
		bool num = !m_SelectedUnvailableSwap.Value && flag;
		d.AssertFormat(m_InteractionModel != null, "m_InteractionModel must be provided before calling internal state");
		if (num)
		{
			m_InteractionModel.StartSwapValidation(m_Selected.Value.m_Snapshot.techData);
		}
		else
		{
			m_InteractionModel.StopSwapValidation();
		}
	}

	private void OnSwapEnabledAdaptor()
	{
		PlacementSelection.InvalidResult value = m_InteractionModel.SwapValidation.Value;
		bool flag = !m_SelectedUnvailableSwap.Value;
		bool isValid = value.IsValid;
		m_SwapOptionEnabled.Value = flag && isValid;
	}

	private void OnValidatePlaceAdaptor()
	{
		bool value = m_PlacementUserActivated.Value;
		bool flag = m_ViewVisible.Value && m_PlaceOptionVisible.Value;
		bool flag2 = !m_SelectedUnvailablePlace.Value;
		bool num = value && flag2 && flag;
		d.AssertFormat(m_InteractionModel != null, "m_InteractionModel must be provided before calling internal state");
		if (num)
		{
			m_InteractionModel.StartPlaceValidation(m_Selected.Value.m_Snapshot.techData);
		}
		else
		{
			m_InteractionModel.StopPlaceValidation();
		}
	}

	private void OnPlaceEnabledAdaptor()
	{
		m_PlaceOptionEnabled.Value = !m_SelectedUnvailablePlace.Value;
	}

	private void OnLanguageChanged()
	{
		RefreshLocalisedStrings();
		UpdateSortingTypeDropdownChoices();
	}

	private void OnPool()
	{
		OnLanguageChanged();
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(OnLanguageChanged);
		m_SnapshotSortType.Value = 3;
		m_SortDescending.Value = true;
	}

	private void OnDepool()
	{
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Unsubscribe(OnLanguageChanged);
	}

	private void OnSpawn()
	{
		m_DataModel = Singleton.Manager<ManSnapshots>.inst;
		m_DataModel.SnapshotCollection.Bind(OnSnapshotCollectionChanged);
		Singleton.Manager<ManProfile>.inst.OnUserChanged.Subscribe(OnProfileChanged);
		m_DataModel.m_QueryStatus.Bind(OnQueryStatusChanged);
		m_DataModel.m_PlacementAvailable.Bind(m_PlaceOptionVisible);
		m_DataModel.m_SwapAvailableInMode.Bind(m_SwapOptionVisible);
		SetSelectedPlatform(Platform.Local);
		m_BlockLimiterInUse.Value = Singleton.Manager<ManBlockLimiter>.inst.LimiterActive;
	}

	private void OnRecycle()
	{
		m_DataModel.SnapshotCollection.Unbind(OnSnapshotCollectionChanged);
		Singleton.Manager<ManProfile>.inst.OnUserChanged.Unsubscribe(OnProfileChanged);
		m_DataModel.m_QueryStatus.Unbind(OnQueryStatusChanged);
		m_DataModel.m_PlacementAvailable.Unbind(m_PlaceOptionVisible);
		m_DataModel.m_SwapAvailableInMode.Unbind(m_SwapOptionVisible);
		m_SearchFilterString.Value = "";
		m_SnapshotFolderCache.Clear();
		m_SnapshotFoldersVisible.Clear();
		m_SearchFilterGroups = null;
		ClearSnapshotSelection();
		ClearFolderSelection();
	}

	private void Update()
	{
		if (m_SnapshotsDirty)
		{
			UpdateSnapshots();
		}
	}
}
