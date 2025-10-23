#define UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Binding;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Snapshots;

public class UISnapshotPanel : MonoBehaviour, IMoveHandler, IEventSystemHandler, ISubmitHandler, IUIExtraButtonHandler2
{
	public interface ISelectable
	{
	}

	[SerializeField]
	private VMSnapshotPanel m_ViewModel;

	[SerializeField]
	private ScrollRect m_GridScrollRect;

	[SerializeField]
	private float m_FocusDisabledAlpha;

	[SerializeField]
	[EnumArray(typeof(VMSnapshotPanel.Platform))]
	private UISnapshotTabHelper[] m_Tabs;

	[SerializeField]
	private RectTransform m_ContentLayout;

	[SerializeField]
	private UISnapshotItem m_ItemPrefab;

	[SerializeField]
	private UISnapshotFolder m_FolderPrefab;

	[SerializeField]
	private GameObject m_WarningsPanel;

	[SerializeField]
	[FormerlySerializedAs("m_ActionsExtras")]
	private GameObject m_ManagementOptionsHighlight;

	[FormerlySerializedAs("m_OptionsExtras")]
	[SerializeField]
	private GameObject m_FilterOptionsHighlight;

	[FormerlySerializedAs("m_SnapshotsExtras")]
	[SerializeField]
	private GameObject m_SnapshotTechListHighlight;

	[SerializeField]
	[FormerlySerializedAs("m_SnapshotTechDataPanel")]
	private GameObject m_SnapshotInfoAndLoadButtonsPanel;

	[SerializeField]
	private GameObject m_SnapshotInfo_SpawnControlsContainer;

	[SerializeField]
	private UISnapshotsBlockInfo m_BlockInfo;

	[SerializeField]
	private TextMeshProUGUI m_LoadingStatus;

	[SerializeField]
	private GameObject m_SettingsNavEntry;

	[SerializeField]
	private GameObject m_ActionsNavEntry;

	[SerializeField]
	private GameObject m_Searchbar;

	[Header("Selected item Info")]
	[SerializeField]
	private Text m_SelectedTechName;

	[SerializeField]
	private TextMeshProUGUI m_SelectedTechCost;

	[SerializeField]
	private TextMeshProUGUI m_SelectedBlockCost;

	[SerializeField]
	private Text m_SelectedTechCreator;

	[SerializeField]
	private TextMeshProUGUI m_SelectedDateCreated;

	[SerializeField]
	[Header("General Management Options")]
	private Transform m_ManagementOptionsContainerVLG;

	[SerializeField]
	private Button m_SearchButton;

	[SerializeField]
	private Button m_ShowBlockInfoButton;

	[SerializeField]
	[Header("Snapshot Management Options")]
	private Selectable[] m_SnapshotSpecific_ManagementOptions;

	[SerializeField]
	private GameObject m_SteamButtonContainer;

	[SerializeField]
	private Button m_SteamButton;

	[SerializeField]
	private Text m_SteamBtnLabel;

	[SerializeField]
	private Toggle m_SelectedFavouriteButton;

	[SerializeField]
	private Button m_SetFolderButton;

	[SerializeField]
	private TooltipComponent m_SetFolderButtonTooltip;

	[SerializeField]
	private LocalisedString m_SetFolderButton_InFolder_LocalisedText;

	[SerializeField]
	private Sprite m_SetFolderButton_InFolder_Icon;

	[SerializeField]
	private LocalisedString m_SetFolderButton_Loose_LocalisedText;

	[SerializeField]
	private Sprite m_SetFolderButton_Loose_Icon;

	[SerializeField]
	private Button m_RenameButton;

	[SerializeField]
	private Button m_DeleteButton;

	[Header("Folder Management Options")]
	[SerializeField]
	private Selectable[] m_FolderSpecific_ManagementOptions;

	[SerializeField]
	private Button m_Folder_RenameButton;

	[SerializeField]
	private Button m_Folder_DeleteButton;

	[SerializeField]
	private Sprite m_NewFolder_Icon;

	[SerializeField]
	private Sprite m_MoveIntoFolder_Icon;

	[Header("Filter Options")]
	[SerializeField]
	private InputField m_SearchField;

	[SerializeField]
	private UIOptionsBehaviourDropdown m_SortTypeDropdown;

	[SerializeField]
	private Toggle m_SortAscendingToggle;

	[SerializeField]
	private Toggle m_SortDecendingToggle;

	[SerializeField]
	private Toggle m_ShowOnlyAvailableToggle;

	[SerializeField]
	private Toggle m_ShowOnlyFavouritesToggle;

	[Header("Action Options")]
	[SerializeField]
	private Button m_SwapButton;

	[SerializeField]
	private Transform m_SwapButtonEnabled;

	[SerializeField]
	private Transform m_SwapButtonDisabled;

	[SerializeField]
	private Transform m_SwapPromptGamepad;

	[SerializeField]
	private Transform m_SwapPromptGamepadDisabled;

	[SerializeField]
	private Button m_PlaceButton;

	[SerializeField]
	private Transform m_PlaceButtonEnabled;

	[SerializeField]
	private Transform m_PlaceButtonDisabled;

	[SerializeField]
	private Transform m_PlaceButtonWarningIcon;

	[SerializeField]
	private Transform m_PlacePromptGamepad;

	[SerializeField]
	private Transform m_PlacePromptGamepadDisabled;

	[SerializeField]
	[Header("Warnings")]
	private Transform m_SwapBlockedReasonContainer;

	[SerializeField]
	private TextMeshProUGUI m_SwapBlockedReasonText;

	[SerializeField]
	private TextMeshProUGUI m_WarningsLabel;

	[Header("HotSwapping")]
	[SerializeField]
	private Transform m_HotSwappingContainer;

	[Header("Extra Panel Toggle")]
	[SerializeField]
	private Selectable m_ToggleOptionsButton;

	[SerializeField]
	private TextMeshProUGUI m_OptionsButtonLabel;

	[SerializeField]
	private Image m_OptionsButtonImage;

	[SerializeField]
	private Sprite m_OptionsButtonAdvancedSprite;

	[SerializeField]
	private Sprite m_OptionsButtonWarningsSprite;

	[SerializeField]
	private LocalisedString m_ShowOptionsString;

	[SerializeField]
	private LocalisedString m_HideOptionsString;

	[SerializeField]
	private LocalisedString m_ShowWarningsString;

	[SerializeField]
	private LocalisedString m_ShowOptionsGamepadString;

	[SerializeField]
	private LocalisedString m_HideOptionsGamepadString;

	[SerializeField]
	private LocalisedString m_ShowWarningsGamepadString;

	private ToggleGroup m_ButtonToggleGroup;

	private HashSet<ISelectable> m_Selectables = new HashSet<ISelectable>();

	private List<UISnapshotItem> m_SnapshotItems;

	private bool m_ItemsDirty;

	private SnapshotLiveData m_ItemToSelect;

	private Dictionary<string, UISnapshotFolder> m_SnapshotFoldersLookup = new Dictionary<string, UISnapshotFolder>();

	private bool m_FoldersDirty;

	private sbyte m_DeferredCreateNewFolderRequest = -1;

	private string m_FolderToSelectName;

	private bool m_WarningsDirty;

	private UITogglesController m_TabToggleController;

	private bool m_SwapBtnVisible;

	private bool m_PlaceBtnVisible;

	private Selectable[] m_AllManagementOptions;

	private string _m_PreSetFolder_FolderName;

	private string m_PreRenameName;

	public void ToggleAdvancedOptions()
	{
		m_ViewModel.ToggleFocus();
	}

	public void SetHotSwapID(int index)
	{
		m_ViewModel.SetHotSwapID(index);
	}

	private void RefreshLayout()
	{
		foreach (UISnapshotFolder value in m_SnapshotFoldersLookup.Values)
		{
			value.RefreshLayoutGroup();
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(m_ContentLayout);
	}

	private UISnapshotFolder UnpackAndRecycleAllFolders(string exceptionName = "")
	{
		List<UISnapshotFolder> list = m_SnapshotFoldersLookup.Values.ToList();
		UISnapshotFolder result = null;
		for (int i = 0; i < list.Count; i++)
		{
			UISnapshotItem[] array = list[i].RetrieveAndClearAllItems();
			for (int j = 0; j < array.Length; j++)
			{
				array[j].transform.SetParent(m_ContentLayout, worldPositionStays: false);
			}
			if (!exceptionName.NullOrEmpty() && list[i].GetData().Name == exceptionName)
			{
				result = list[i];
				continue;
			}
			list[i].OnFolderExpandedEvent.Unsubscribe(OnFolderExpanded);
			list[i].OnToggledTrue.Unsubscribe(OnFolderButtonClicked);
			list[i].Recycle();
			m_Selectables.Remove(list[i]);
		}
		for (int k = 0; k < m_SnapshotItems.Count; k++)
		{
			m_SnapshotItems[k].transform.SetAsLastSibling();
		}
		m_SnapshotFoldersLookup.Clear();
		return result;
	}

	private void UpdateItems()
	{
		BindableList<SnapshotLiveData> snapshots = m_ViewModel.m_Snapshots;
		List<SnapshotFolderLiveData> snapshotFoldersVisible = m_ViewModel.m_SnapshotFoldersVisible;
		if ((bool)m_HotSwappingContainer)
		{
			m_HotSwappingContainer.gameObject.SetActive(Singleton.Manager<ManTechs>.inst.HotswapEnbled);
		}
		UISnapshotFolder uISnapshotFolder = UnpackAndRecycleAllFolders(m_ViewModel.m_SelectedFolderName.Value);
		for (int num = snapshotFoldersVisible.Count - 1; num >= 0; num--)
		{
			if (uISnapshotFolder != null && m_ViewModel.m_SelectedFolderName.Value == snapshotFoldersVisible[num].Name)
			{
				UpdateFolder(snapshotFoldersVisible[num], uISnapshotFolder);
			}
			else
			{
				AddFolder(snapshotFoldersVisible[num]);
			}
		}
		for (int num2 = m_SnapshotItems.Count - 1; num2 >= snapshots.Count; num2--)
		{
			RemoveItem(num2);
		}
		for (int i = 0; i < m_SnapshotItems.Count && i < snapshots.Count; i++)
		{
			UpdateItem(i, snapshots[i]);
		}
		bool flag = false;
		for (int j = m_SnapshotItems.Count; j < snapshots.Count; j++)
		{
			AddItem(snapshots[j]);
			flag = true;
		}
		RefreshLayout();
		if (!flag)
		{
			m_ItemsDirty = false;
		}
	}

	private void AddFolder(SnapshotFolderLiveData folderData)
	{
		UISnapshotFolder uISnapshotFolder = m_FolderPrefab.Spawn(m_ContentLayout);
		UpdateFolder(folderData, uISnapshotFolder);
		uISnapshotFolder.SetSelected(isSelected: false);
		uISnapshotFolder.OnToggledTrue.Subscribe(OnFolderButtonClicked);
		uISnapshotFolder.OnFolderExpandedEvent.Subscribe(OnFolderExpanded);
	}

	private void UpdateFolder(SnapshotFolderLiveData folderData, UISnapshotFolder folderElement)
	{
		folderElement.transform.localPosition = Vector3.zero;
		folderElement.transform.localScale = Vector3.one;
		folderElement.transform.SetAsFirstSibling();
		folderElement.SetData(folderData);
		folderElement.SetButtonGroup(m_ButtonToggleGroup);
		m_SnapshotFoldersLookup.Add(folderData.Name, folderElement);
		m_Selectables.Add(folderElement);
	}

	private void AddItemToFolderIfNecessary(UISnapshotItem snapItem, SnapshotLiveData snapData)
	{
		string folderName = snapData.m_Snapshot.m_Meta.Value.FolderName;
		if (!folderName.NullOrEmpty() && m_SnapshotFoldersLookup.ContainsKey(folderName))
		{
			m_SnapshotFoldersLookup[folderName].AddItem(snapItem);
		}
	}

	private void AddItem(SnapshotLiveData snapshotData)
	{
		UISnapshotItem uISnapshotItem = m_ItemPrefab.Spawn();
		uISnapshotItem.SetData(snapshotData);
		uISnapshotItem.transform.SetParent(m_ContentLayout, worldPositionStays: false);
		uISnapshotItem.transform.localScale = Vector3.one;
		uISnapshotItem.SetSelected(isSelected: false);
		uISnapshotItem.SetButtonGroup(m_ButtonToggleGroup);
		uISnapshotItem.OnToggledTrue.Subscribe(OnTechPresetButtonClicked);
		m_SnapshotItems.Add(uISnapshotItem);
		m_Selectables.Add(uISnapshotItem);
		AddItemToFolderIfNecessary(uISnapshotItem, snapshotData);
	}

	private void UpdateItem(int index, SnapshotLiveData snapshotData)
	{
		if (m_SnapshotItems.Count < 1 || index > m_SnapshotItems.Count - 1)
		{
			d.LogErrorFormat("UISnapshotPanel index {0} out of bounds {1}", index, m_SnapshotItems.Count);
		}
		else
		{
			UISnapshotItem uISnapshotItem = m_SnapshotItems[index];
			uISnapshotItem.SetData(snapshotData);
			AddItemToFolderIfNecessary(uISnapshotItem, snapshotData);
		}
	}

	private void RemoveItem(int index)
	{
		if (m_SnapshotItems.Count < 1 || index > m_SnapshotItems.Count - 1)
		{
			d.LogErrorFormat("UISnapshotPanel index {0} out of bounds {1}", index, m_SnapshotItems.Count);
			return;
		}
		UISnapshotItem uISnapshotItem = m_SnapshotItems[index];
		uISnapshotItem.SetSelected(isSelected: false);
		uISnapshotItem.OnToggledTrue.Unsubscribe(OnTechPresetButtonClicked);
		uISnapshotItem.Clear();
		m_SnapshotItems.RemoveAt(index);
		m_Selectables.Remove(uISnapshotItem);
		uISnapshotItem.Recycle();
	}

	private void UpdateWarnings()
	{
		string text = string.Empty;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		string text2 = $"<uppercase>{Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechPlacement, 7)}:</uppercase>";
		string text3 = $"<uppercase>{Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechPlacement, 8)}:</uppercase>";
		string text4 = $"<uppercase>{Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechPlacement, 9)}:</uppercase>";
		string text5 = "\n\t";
		BindableList<VMSnapshotPanel.WarningData> warnings = m_ViewModel.m_Warnings;
		for (int i = 0; i < warnings.Count; i++)
		{
			VMSnapshotPanel.WarningData warningData = warnings[i];
			if (warningData.m_WarningType == PlacementSelection.InvalidType.General)
			{
				text2 = text2 + text5 + warningData.m_Text;
				num++;
			}
			else if (warningData.m_WarningType == PlacementSelection.InvalidType.Place)
			{
				text3 = text3 + text5 + warningData.m_Text;
				num3++;
			}
			else if (warningData.m_WarningType == PlacementSelection.InvalidType.Swap)
			{
				text4 = text4 + text5 + warningData.m_Text;
				num2++;
			}
		}
		if (num > 0)
		{
			text += text2;
		}
		if (num3 > 0)
		{
			if (text.Length > 0)
			{
				text += "\n";
			}
			text += text3;
		}
		if (num2 > 0)
		{
			if (text.Length > 0)
			{
				text += "\n";
			}
			text += text4;
		}
		m_WarningsLabel.text = text;
		UpdateOptionsButtonLabel();
	}

	private void MoveSelection(Vector2 dir)
	{
		ISelectable selectable = null;
		if (m_ViewModel.m_Selected.Value.m_Snapshot != null)
		{
			selectable = m_SnapshotItems.FirstOrDefault((UISnapshotItem r) => m_ViewModel.m_Selected.Value == r.GetData());
		}
		else if (!m_ViewModel.m_SelectedFolderName.Value.NullOrEmpty())
		{
			selectable = m_SnapshotFoldersLookup.Values.FirstOrDefault((UISnapshotFolder r) => m_ViewModel.m_SelectedFolderName.Value == r.GetData().Name);
		}
		if (selectable == null)
		{
			return;
		}
		ISelectable selectable2 = selectable;
		Vector3[] array = new Vector3[4];
		((Component)selectable).GetComponent<RectTransform>().GetWorldCorners(array);
		Vector2 vector = array[1];
		float num = float.MaxValue;
		Vector2 rhs = new Vector2(dir.y, 0f - dir.x);
		foreach (ISelectable selectable3 in m_Selectables)
		{
			((Component)selectable3).GetComponent<RectTransform>().GetWorldCorners(array);
			Vector2 lhs = (Vector2)array[1] - vector;
			float num2 = Vector2.Dot(lhs, dir);
			float num3 = Mathf.Abs(Vector2.Dot(lhs, rhs));
			if (num2 > 0f)
			{
				float num4 = num2 + num3 * 2f;
				if (num4 < num)
				{
					selectable2 = selectable3;
					num = num4;
				}
			}
		}
		if (selectable2 is UISnapshotItem uISnapshotItem)
		{
			m_ViewModel.SetSelectedSnapshot(uISnapshotItem.GetData());
		}
		else if (selectable2 is UISnapshotFolder uISnapshotFolder)
		{
			m_ViewModel.SetSelectedSnapshotFolder(uISnapshotFolder.GetData().Name);
		}
	}

	private void UpdateOptionsButtonLabel()
	{
		if (m_ViewModel.m_BlockInfoVisible.Value || m_ViewModel.m_PlacementUserActivated.Value)
		{
			if ((bool)m_ToggleOptionsButton)
			{
				m_ToggleOptionsButton.interactable = false;
			}
			if ((bool)m_OptionsButtonLabel)
			{
				m_OptionsButtonLabel.text = string.Empty;
			}
			if ((bool)m_OptionsButtonImage)
			{
				m_OptionsButtonImage.enabled = false;
			}
			return;
		}
		bool value = m_ViewModel.m_OptionsVisbile.Value;
		bool flag = m_ViewModel.m_Warnings.Count > 0;
		bool flag2 = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
		LocalisedString localisedString = ((!value) ? (flag2 ? m_ShowOptionsGamepadString : m_ShowOptionsString) : ((!flag) ? (flag2 ? m_HideOptionsGamepadString : m_HideOptionsString) : (flag2 ? m_ShowWarningsGamepadString : m_ShowWarningsString)));
		if ((bool)m_ToggleOptionsButton)
		{
			m_ToggleOptionsButton.interactable = true;
		}
		if ((bool)m_OptionsButtonLabel)
		{
			m_OptionsButtonLabel.text = localisedString.Value;
		}
		if ((bool)m_OptionsButtonImage)
		{
			m_OptionsButtonImage.enabled = true;
			m_OptionsButtonImage.sprite = (value ? m_OptionsButtonAdvancedSprite : m_OptionsButtonWarningsSprite);
		}
	}

	private void SetManagementOptions(VMSnapshotPanel.SelectionTypes selectionType)
	{
		_internal_SetSpecificManagementOptionsActive(m_FolderSpecific_ManagementOptions, selectionType == VMSnapshotPanel.SelectionTypes.SnapshotFolders);
		_internal_SetSpecificManagementOptionsActive(m_SnapshotSpecific_ManagementOptions, selectionType == VMSnapshotPanel.SelectionTypes.SnapshotItems);
		Util.RebuildExplicitVerticalUINavigationBetweenElements(m_AllManagementOptions);
		static void _internal_SetSpecificManagementOptionsActive(Selectable[] options, bool state)
		{
			for (int i = 0; i < options.Length; i++)
			{
				options[i].gameObject.SetActive(state);
			}
		}
	}

	private void OnViewVisibilityChanged(bool visible)
	{
		if (visible)
		{
			if (!m_ViewModel.m_PlacementUserActivated.Value)
			{
				Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.UITechLoaderPanel);
			}
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(base.gameObject);
			Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(22, OnGlobalUICancel);
			Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(41, OnGlobalUITabNext);
			Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(42, OnGlobalUITabPrev);
		}
		else
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.UITechLoaderPanel);
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(base.gameObject);
			Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(22, OnGlobalUICancel);
			Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(41, OnGlobalUITabNext);
			Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(42, OnGlobalUITabPrev);
			m_SortTypeDropdown.Hide();
		}
	}

	private void OnSnapshotsUpdated(BindableList<SnapshotLiveData> list, int index, BindableChange changedType)
	{
		m_ItemsDirty = true;
	}

	private void OnBlockLimiterActiveChanged(bool blockLimInUse)
	{
		m_SelectedBlockCost.gameObject.SetActive(blockLimInUse);
	}

	private void OnSwapButtonEnabled(bool isEnabled)
	{
		m_SwapButton.interactable = isEnabled;
		bool gamepadUI = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
		UpdateSwapButtonState(m_SwapBtnVisible, m_SwapButton.interactable, gamepadUI);
	}

	private void OnSwapButtonVisibleChanged(bool isVisible)
	{
		m_SwapBtnVisible = isVisible;
		bool gamepadUI = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
		UpdateSwapButtonState(m_SwapBtnVisible, m_SwapButton.interactable, gamepadUI);
	}

	private void OnPlaceButtonEnabled(bool isEnabled)
	{
		m_PlaceButton.interactable = isEnabled;
		bool gamepadUI = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
		UpdatePlaceButtonState(m_PlaceBtnVisible, m_PlaceButton.interactable, gamepadUI);
	}

	private void OnPlaceButtonVisibleChanged(bool isVisible)
	{
		m_PlaceBtnVisible = isVisible;
		bool gamepadUI = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
		UpdatePlaceButtonState(m_PlaceBtnVisible, m_PlaceButton.interactable, gamepadUI);
	}

	private void UpdateSwapButtonState(bool visible, bool enabled, bool gamepadUI)
	{
		m_SwapButton.gameObject.SetActive(visible && !gamepadUI);
		m_SwapButtonEnabled.gameObject.SetActive(visible && !gamepadUI && enabled);
		m_SwapButtonDisabled.gameObject.SetActive(visible && !gamepadUI && !enabled);
		m_SwapPromptGamepad.gameObject.SetActive(visible && gamepadUI && enabled);
		m_SwapPromptGamepadDisabled.gameObject.SetActive(visible && gamepadUI && !enabled);
	}

	private void UpdatePlaceButtonState(bool visible, bool enabled, bool gamepadUI)
	{
		m_PlaceButton.gameObject.SetActive(visible && !gamepadUI);
		m_PlaceButtonEnabled.gameObject.SetActive(visible && !gamepadUI && enabled);
		m_PlaceButtonDisabled.gameObject.SetActive(visible && !gamepadUI && !enabled);
		int num;
		if (enabled)
		{
			_ = m_ViewModel.m_Selected.Value;
			if (m_ViewModel.m_Selected.Value.m_ValidData.HasMissingBlocksPlace)
			{
				num = ((!Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) || (!Singleton.Manager<DebugUtil>.inst.RemoveTechLoaderRestrictions && !Singleton.Manager<DebugUtil>.inst.AllBlocksInInventory)) ? 1 : 0);
				goto IL_00b3;
			}
		}
		num = 0;
		goto IL_00b3;
		IL_00b3:
		bool flag = (byte)num != 0;
		m_PlaceButtonWarningIcon.gameObject.SetActive(visible && !gamepadUI && flag);
		m_PlacePromptGamepad.gameObject.SetActive(visible && gamepadUI && enabled);
		m_PlacePromptGamepadDisabled.gameObject.SetActive(visible && gamepadUI && !enabled);
	}

	private void OnLastSelectionTypeChanged(VMSnapshotPanel.SelectionTypes selectionType)
	{
		SetManagementOptions(selectionType);
	}

	private void OnFavouriteBtnEnabled(bool isEnabled)
	{
		m_SelectedFavouriteButton.interactable = isEnabled;
	}

	private void OnSetFolderButtonEnabled(bool isEnabled)
	{
		m_SetFolderButton.interactable = isEnabled;
	}

	private void OnRenameBtnEnabled(bool isEnabled)
	{
		m_RenameButton.interactable = isEnabled;
	}

	private void OnDeleteBtnEnabled(bool isEnabled)
	{
		m_DeleteButton.interactable = isEnabled;
	}

	private void OnFolderRenameBtnEnabled(bool isEnabled)
	{
		if (m_Folder_RenameButton != null)
		{
			m_Folder_RenameButton.interactable = isEnabled;
		}
	}

	private void OnFolderDeleteBtnEnabled(bool isEnabled)
	{
		if (m_Folder_DeleteButton != null)
		{
			m_Folder_DeleteButton.interactable = isEnabled;
		}
	}

	private void OnWarningsVisibleChanged(bool isVisible)
	{
		m_WarningsPanel.SetActive(isVisible);
		UpdateOptionsButtonLabel();
	}

	private void OnSnapshotsVisisbleChanged(bool isVisible)
	{
		bool active = isVisible || !Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
		if ((bool)m_SnapshotTechListHighlight)
		{
			m_SnapshotTechListHighlight.SetActive(active);
		}
		if ((bool)m_SnapshotInfo_SpawnControlsContainer)
		{
			m_SnapshotInfo_SpawnControlsContainer.SetActive(active);
		}
		if (isVisible && base.gameObject.activeSelf)
		{
			Singleton.Manager<ManNavUI>.inst.ReselectTopmostEntryTarget();
		}
	}

	private void OnSearchVisibleChanged(bool shouldBeVisible)
	{
		bool activeSelf = m_Searchbar.activeSelf;
		bool flag = shouldBeVisible || ((bool)m_SearchField && !m_SearchField.text.NullOrEmpty());
		if (activeSelf == flag)
		{
			return;
		}
		m_Searchbar?.SetActive(flag);
		if (flag)
		{
			if (!SKU.ConsoleUI && !activeSelf)
			{
				Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_Searchbar, delegate
				{
					m_SearchField.ActivateInputField();
				});
			}
			else
			{
				Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_Searchbar);
			}
		}
		else
		{
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_Searchbar);
		}
	}

	private void OnFilterOptionsVisibleChanged(bool isVisible)
	{
		if ((bool)m_FilterOptionsHighlight)
		{
			m_FilterOptionsHighlight.SetActive(isVisible);
		}
		if (isVisible)
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_SettingsNavEntry);
		}
		else
		{
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_SettingsNavEntry);
		}
		UpdateOptionsButtonLabel();
	}

	private void OnManagementOptionsVisibleChanged(bool isVisible)
	{
		if ((bool)m_ManagementOptionsHighlight)
		{
			m_ManagementOptionsHighlight.SetActive(isVisible);
		}
		if (isVisible)
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_ActionsNavEntry);
		}
		else
		{
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_ActionsNavEntry);
		}
	}

	private void OnLoadingStatusChanged(string label)
	{
		m_LoadingStatus.text = label;
	}

	private void OnLoadingStatusVisibleChanged(bool isVisible)
	{
		m_LoadingStatus.gameObject.SetActive(isVisible);
	}

	private void OnWarningsChagned(BindableList<VMSnapshotPanel.WarningData> list, int index, BindableChange changeType)
	{
		m_WarningsDirty = true;
	}

	private void OnSelectedFolderChanged(string selectedFolderName)
	{
		m_FolderToSelectName = selectedFolderName;
	}

	private void OnSelectedChanged(SnapshotLiveData selectedData)
	{
		m_ItemToSelect = selectedData;
		m_SnapshotInfoAndLoadButtonsPanel.SetActive(selectedData != default(SnapshotLiveData));
	}

	private void OnSelectedTechNameChanged(string techName)
	{
		m_SelectedTechName.text = techName;
	}

	private void OnSelectedTechCostChanged(string techCost)
	{
		m_SelectedTechCost.text = techCost;
	}

	private void OnSelectedBlockCostChanged(string blockCost)
	{
		m_SelectedBlockCost.text = blockCost;
	}

	private void OnSelectedTechCreatorChanged(string techCreator)
	{
		m_SelectedTechCreator.text = techCreator;
	}

	private void OnSelectedTechFavouriteChanged(bool isFavourite)
	{
		m_SelectedFavouriteButton.SetValue(isFavourite);
	}

	private void OnSelectedTechFolderChanged(string folderName)
	{
		m_SetFolderButton.image.sprite = (folderName.NullOrEmpty() ? m_SetFolderButton_Loose_Icon : m_SetFolderButton_InFolder_Icon);
		m_SetFolderButtonTooltip.SetText(folderName.NullOrEmpty() ? m_SetFolderButton_Loose_LocalisedText.Value : m_SetFolderButton_InFolder_LocalisedText.Value);
	}

	private void OnSelectedDateChanged(string dateCreated)
	{
		m_SelectedDateCreated.text = dateCreated;
	}

	private void OnSteamBtnVisibleChanged(bool isVisible)
	{
		m_SteamButtonContainer.SetActive(isVisible);
	}

	private void OnSteamBtnLabelChanged(string label)
	{
		m_SteamBtnLabel.text = label;
	}

	private void OnShowOnlyAvailableChanged(bool showOnlyAvail)
	{
		m_ShowOnlyAvailableToggle.SetValue(showOnlyAvail);
	}

	private void OnShowOnlyFavChanged(bool showOnlyFav)
	{
		m_ShowOnlyFavouritesToggle.SetValue(showOnlyFav);
	}

	private void OnSearchFilterStringChanged(string searchStr)
	{
		m_SearchField.SetValue(searchStr);
	}

	private void OnSortTypeListChanged(BindableList<string> listValues, int changedIndex, BindableChange changeType)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < listValues.Count; i++)
		{
			list.Add(listValues[i]);
		}
		m_SortTypeDropdown.Hide();
		m_SortTypeDropdown.ClearOptions();
		m_SortTypeDropdown.AddOptions(list);
		m_SortTypeDropdown.RefreshShownValue();
	}

	private void OnSortTypeChanged(int selectedIndex)
	{
		m_SortTypeDropdown.SetValue(selectedIndex);
	}

	private void OnSortDecendingChanged(bool sortDecending)
	{
		m_SortAscendingToggle.SetValue(!sortDecending);
		m_SortDecendingToggle.SetValue(sortDecending);
	}

	private void OnTechPresetButtonClicked(SnapshotLiveData snapshotData)
	{
		m_ViewModel.SetSelectedSnapshot(snapshotData);
	}

	private void OnFolderButtonClicked(SnapshotFolderLiveData snapFolderData)
	{
		m_ViewModel.SetSelectedSnapshotFolder(snapFolderData.Name);
	}

	private void OnFolderExpanded(string folderName, bool state)
	{
		m_ViewModel.SetFolderExpanded(folderName, state);
	}

	private void OnFavouriteButtonClicked(bool isFavourite)
	{
		m_ViewModel.SetSelectedSnapshotAsFavourite(isFavourite);
	}

	private void OnSwapButtonClicked()
	{
		m_ViewModel.Swap();
	}

	private void OnPlaceButtonClicked()
	{
		m_ViewModel.Place();
	}

	private void OnDeleteButtonClicked()
	{
		string value = m_ViewModel.m_SelectedTechName.Value;
		UIScreenNotifications uIScreenNotifications = (UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen);
		string notification = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.Notifications.InfoSnapshotDelete), value);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MenuMain.menuYes),
			m_Callback = delegate
			{
				Singleton.Manager<ManUI>.inst.RemovePopup();
				m_ViewModel.DeleteSelected();
			},
			m_RewiredAction = 21
		};
		UIButtonData decline = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MenuMain.menuNo),
			m_Callback = delegate
			{
				Singleton.Manager<ManUI>.inst.RemovePopup();
			},
			m_RewiredAction = 22
		};
		uIScreenNotifications.Set(notification, accept, decline, m_ViewModel.m_Selected.Value.m_Snapshot.image);
		uIScreenNotifications.SetUseNewInputHandler(useNewInputHandler: true);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	private void OnSetFolderButtonClicked()
	{
		string folderName = m_ViewModel.m_Selected.Value.m_Snapshot.m_Meta.Value.FolderName;
		if (!folderName.NullOrEmpty())
		{
			m_ViewModel.SetFolder(string.Empty);
			return;
		}
		UIScreenNotificationMultiselect uIScreenNotificationMultiselect = (UIScreenNotificationMultiselect)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationMultiselect);
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.TechLoader.Folders_ChooseFolderNotificationTitle);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MenuMain.menuSave);
		List<string> list = m_ViewModel.GetAllFolderNames(searchEntireCache: true).ToList();
		list.Sort();
		UIScreenNotificationMultiselect.ItemConfig[] array = new UIScreenNotificationMultiselect.ItemConfig[list.Count + 1];
		for (int i = 0; i < array.Length; i++)
		{
			if (i == 0)
			{
				array[i] = new UIScreenNotificationMultiselect.ItemConfig
				{
					UniqueID = -1,
					Title = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.TechLoader.Folders_NewFolderItem),
					Icon = m_NewFolder_Icon
				};
			}
			else
			{
				array[i] = new UIScreenNotificationMultiselect.ItemConfig
				{
					Title = list[i - 1],
					Icon = m_MoveIntoFolder_Icon
				};
			}
		}
		_m_PreSetFolder_FolderName = folderName;
		uIScreenNotificationMultiselect.Configure(localisedString, array, allowMultiselect: false, allowNoneResult: false, localisedString2, OnSetFolderConfirmed, OnSetFolderCancelled);
		uIScreenNotificationMultiselect.SetUseNewInputHandler(useNewInputHandler: true);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotificationMultiselect);
	}

	private void OnSetFolderConfirmed(UIScreenNotificationMultiselect.ItemConfig[] returnedConfig)
	{
		UIScreenNotificationMultiselect.ItemConfig itemConfig = returnedConfig[0];
		bool num = itemConfig.UniqueID == -1;
		Singleton.Manager<ManUI>.inst.RemovePopup();
		if (num)
		{
			m_DeferredCreateNewFolderRequest = 0;
		}
		else if (itemConfig.Title != _m_PreSetFolder_FolderName)
		{
			m_ViewModel.SetFolder(itemConfig.Title);
		}
	}

	private void OnCreateNewFolderButtonClicked()
	{
		UIScreenNotificationInput uIScreenNotificationInput = (UIScreenNotificationInput)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationInput);
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.TechLoader.Folders_CreateFolderNotificationTitle);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MenuMain.menuSave);
		_m_PreSetFolder_FolderName = m_ViewModel.m_Selected.Value.m_Snapshot.m_Meta.Value.FolderName;
		uIScreenNotificationInput.Configure(string.Empty, localisedString, localisedString2, OnCreateFolderConfirmed, OnSetFolderCancelled);
		uIScreenNotificationInput.SetUseNewInputHandler(useNewInputHandler: true);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotificationInput);
	}

	private void OnCreateFolderConfirmed(string folderName)
	{
		Singleton.Manager<ManUI>.inst.RemovePopup();
		if (folderName != _m_PreSetFolder_FolderName)
		{
			m_ViewModel.SetFolder(folderName);
		}
		m_ViewModel.ReturnToTechList();
	}

	private void OnSetFolderCancelled()
	{
		Singleton.Manager<ManUI>.inst.RemovePopup();
		m_ViewModel.ReturnToTechList();
	}

	private void OnRenameButtonClicked()
	{
		UIScreenNotificationInput uIScreenNotificationInput = (UIScreenNotificationInput)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationInput);
		string value = m_ViewModel.m_SelectedTechName.Value;
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.TechLoader.RenameSnapshot);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MenuMain.menuSave);
		d.Log("UISnapshotPanel.OnRenameButtonClicked currentName=" + value);
		m_PreRenameName = value;
		uIScreenNotificationInput.Configure(value, localisedString, localisedString2, OnRenameSelectedConfirmed, OnRenameSelectedCancelled);
		uIScreenNotificationInput.SetUseNewInputHandler(useNewInputHandler: true);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotificationInput);
	}

	private void OnRenameSelectedConfirmed(string newName)
	{
		d.Log("UISnapshotPanel.OnRenameConfirmed '" + m_PreRenameName + "' rename to '" + newName + "'");
		if (newName.NullOrEmpty())
		{
			d.LogWarning("Trying to rename selected to empty name! Don't allow this (but maybe we should provide user feedback..)");
			return;
		}
		Singleton.Manager<ManUI>.inst.RemovePopup();
		if (newName != m_PreRenameName)
		{
			m_ViewModel.RenameSelected(newName);
		}
	}

	private void OnRenameSelectedCancelled()
	{
		d.Log("UISnapshotPanel.OnRenameCancelled leaving old name '" + m_PreRenameName + "'");
		Singleton.Manager<ManUI>.inst.RemovePopup();
	}

	private void OnFolderRenameButtonClicked()
	{
		UIScreenNotificationInput uIScreenNotificationInput = (UIScreenNotificationInput)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationInput);
		string value = m_ViewModel.m_SelectedFolderName.Value;
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.TechLoader.Folders_RenameFolderNotificationTitle);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MenuMain.menuSave);
		d.Log("UISnapshotPanel.OnRenameButtonClicked currentName=" + value);
		m_PreRenameName = value;
		uIScreenNotificationInput.Configure(value, localisedString, localisedString2, OnRenameSelectedConfirmed, OnRenameSelectedCancelled);
		uIScreenNotificationInput.SetUseNewInputHandler(useNewInputHandler: true);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotificationInput);
	}

	private void OnFolderDeleteButtonClicked()
	{
		UIScreenNotifications uIScreenNotifications = (UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen);
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.TechLoader.Folders_DeleteFolderNotificationText);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MenuMain.menuYes),
			m_Callback = delegate
			{
				Singleton.Manager<ManUI>.inst.RemovePopup();
				m_ViewModel.DeleteSelected();
			},
			m_RewiredAction = 21
		};
		UIButtonData decline = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MenuMain.menuNo),
			m_Callback = delegate
			{
				Singleton.Manager<ManUI>.inst.RemovePopup();
			},
			m_RewiredAction = 22
		};
		uIScreenNotifications.Set(localisedString, accept, decline);
		uIScreenNotifications.SetUseNewInputHandler(useNewInputHandler: true);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	private void OnSteamButtonClicked()
	{
		m_ViewModel.GoToWorkshop();
	}

	private void OnShowBlockInfoButtonsClicked()
	{
		m_ViewModel.ToggleBlockInfo();
	}

	public void OnSubmit(BaseEventData eventData)
	{
		if (m_ViewModel.m_PlaceOptionVisible.Value && m_ViewModel.m_PlaceOptionEnabled.Value)
		{
			if (m_ViewModel.Place())
			{
				eventData.Use();
			}
		}
		else if (!m_ViewModel.m_SelectedFolderName.Value.NullOrEmpty())
		{
			if (m_SnapshotFoldersLookup.TryGetValue(m_ViewModel.m_SelectedFolderName.Value, out var value))
			{
				value.ToggleSelected();
				return;
			}
			d.LogErrorFormat("Failed to find folder name {0} in snapshot folder lookup?!", m_ViewModel.m_SelectedFolderName.Value);
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
		if (m_ViewModel.m_SwapOptionVisible.Value && m_ViewModel.m_SwapOptionEnabled.Value)
		{
			OnSwapButtonClicked();
			eventData.Use();
		}
	}

	public void OnGlobalUIExtraButton1(PayloadUIEventData eventData)
	{
		m_ViewModel.ToggleFocus();
	}

	public void OnGlobalUITabNext(PayloadUIEventData eventData)
	{
		if (m_ViewModel.GetFocusTarget() == VMSnapshotPanel.FocusTarget.Blocks)
		{
			m_BlockInfo.MoveGrid(new Vector2(1f, 0f));
		}
		else
		{
			m_ViewModel.IncrementFocusPanel();
		}
	}

	public void OnGlobalUITabPrev(PayloadUIEventData eventData)
	{
		if (m_ViewModel.GetFocusTarget() == VMSnapshotPanel.FocusTarget.Blocks)
		{
			m_BlockInfo.MoveGrid(new Vector2(-1f, 0f));
		}
		else
		{
			m_ViewModel.DecrementFocusPanel();
		}
	}

	public void OnGlobalUICancel(PayloadUIEventData eventData)
	{
		if (m_ViewModel.CloseFocused())
		{
			eventData.Use();
		}
	}

	private void OnShowOnlyAvailableToggleClicked(bool showAvailOnly)
	{
		m_ViewModel.SetShowOnlyAvailable(showAvailOnly);
	}

	private void OnShowOnlyFavouritesToggleClicked(bool showFavOnly)
	{
		m_ViewModel.SetShowOnlyFavourites(showFavOnly);
	}

	private void OnSearchFieldValueChanged(string searchStr)
	{
		m_ViewModel.SetSearchFilterString(searchStr);
	}

	private void OnSortTypeValueChanged(int chosenIndex)
	{
		m_ViewModel.SetSortType(chosenIndex);
	}

	private void OnAscendingToggleChanged(bool toggledOn)
	{
		if (toggledOn)
		{
			m_ViewModel.SetSortDecending(decending: false);
		}
	}

	private void OnDecendingToggleChanged(bool toggledOn)
	{
		if (toggledOn)
		{
			m_ViewModel.SetSortDecending(decending: true);
		}
	}

	private void OnSearchClicked()
	{
		m_ViewModel.SearchClicked();
	}

	private void OnPool()
	{
		m_SnapshotItems = new List<UISnapshotItem>(10);
		m_ItemPrefab.CreatePool(10);
		m_AllManagementOptions = m_ManagementOptionsContainerVLG.GetComponentsInChildren<Selectable>(includeInactive: true);
		m_FolderPrefab.CreatePool(5);
		m_TabToggleController = new UITogglesController();
		for (int i = 0; i < m_Tabs.Length; i++)
		{
			if (m_Tabs[i] != null)
			{
				m_TabToggleController.AddToggle(m_Tabs[i].Toggle, m_Tabs[i].transform.GetSiblingIndex());
			}
		}
		m_ButtonToggleGroup = m_ContentLayout.GetComponent<ToggleGroup>();
		m_SelectedFavouriteButton.onValueChanged.AddListener(OnFavouriteButtonClicked);
		m_ShowOnlyAvailableToggle.onValueChanged.AddListener(OnShowOnlyAvailableToggleClicked);
		m_ShowOnlyFavouritesToggle.onValueChanged.AddListener(OnShowOnlyFavouritesToggleClicked);
		m_SearchField.onValueChanged.AddListener(OnSearchFieldValueChanged);
		m_SortTypeDropdown.onValueChanged.AddListener(OnSortTypeValueChanged);
		m_SortAscendingToggle.onValueChanged.AddListener(OnAscendingToggleChanged);
		m_SortDecendingToggle.onValueChanged.AddListener(OnDecendingToggleChanged);
		m_SwapButton.onClick.AddListener(OnSwapButtonClicked);
		m_PlaceButton.onClick.AddListener(OnPlaceButtonClicked);
		m_ShowBlockInfoButton.onClick.AddListener(OnShowBlockInfoButtonsClicked);
		m_SearchButton.onClick.AddListener(OnSearchClicked);
		m_SetFolderButton.onClick.AddListener(OnSetFolderButtonClicked);
		m_RenameButton.onClick.AddListener(OnRenameButtonClicked);
		m_DeleteButton.onClick.AddListener(OnDeleteButtonClicked);
		if (m_Folder_RenameButton != null)
		{
			m_Folder_RenameButton.onClick.AddListener(OnFolderRenameButtonClicked);
		}
		if (m_Folder_DeleteButton != null)
		{
			m_Folder_DeleteButton.onClick.AddListener(OnFolderDeleteButtonClicked);
		}
		m_SteamButton.onClick.AddListener(OnSteamButtonClicked);
	}

	private void OnSpawn()
	{
		d.Assert(m_ViewModel != null, "View Model can not be null");
		m_ViewModel.m_ViewVisible.Bind(OnViewVisibilityChanged);
		m_ViewModel.m_Snapshots.Bind(OnSnapshotsUpdated);
		m_ViewModel.m_BlockLimiterInUse.Bind(OnBlockLimiterActiveChanged);
		m_ViewModel.m_SwapOptionEnabled.Bind(OnSwapButtonEnabled);
		m_ViewModel.m_SwapOptionVisible.Bind(OnSwapButtonVisibleChanged);
		m_ViewModel.m_PlaceOptionEnabled.Bind(OnPlaceButtonEnabled);
		m_ViewModel.m_PlaceOptionVisible.Bind(OnPlaceButtonVisibleChanged);
		m_ViewModel.m_LastSelectionType.Bind(OnLastSelectionTypeChanged);
		m_ViewModel.m_FavouriteBtnEnabled.Bind(OnFavouriteBtnEnabled);
		m_ViewModel.m_SetFolderBtnEnabled.Bind(OnSetFolderButtonEnabled);
		m_ViewModel.m_RenameBtnEnabled.Bind(OnRenameBtnEnabled);
		m_ViewModel.m_DeleteBtnEnabled.Bind(OnDeleteBtnEnabled);
		m_ViewModel.m_FolderRenameBtnEnabled.Bind(OnFolderRenameBtnEnabled);
		m_ViewModel.m_FolderDeleteBtnEnabled.Bind(OnFolderDeleteBtnEnabled);
		m_ViewModel.m_WarningsVisible.Bind(OnWarningsVisibleChanged);
		m_ViewModel.m_OptionsVisbile.Bind(OnFilterOptionsVisibleChanged);
		m_ViewModel.m_ActionsVisbile.Bind(OnManagementOptionsVisibleChanged);
		m_ViewModel.m_SnapshotsVisible.Bind(OnSnapshotsVisisbleChanged);
		m_ViewModel.m_SearchVisible.Bind(OnSearchVisibleChanged);
		m_ViewModel.m_LoadingStatus.Bind(OnLoadingStatusChanged);
		m_ViewModel.m_LoadingStatusVisible.Bind(OnLoadingStatusVisibleChanged);
		m_ViewModel.m_Warnings.Bind(OnWarningsChagned);
		m_ViewModel.m_SelectedFolderName.Bind(OnSelectedFolderChanged);
		m_ViewModel.m_Selected.Bind(OnSelectedChanged);
		m_ViewModel.m_SelectedTechName.Bind(OnSelectedTechNameChanged);
		m_ViewModel.m_SelectedTechCost.Bind(OnSelectedTechCostChanged);
		m_ViewModel.m_SelectedBlockCost.Bind(OnSelectedBlockCostChanged);
		m_ViewModel.m_SelectedDateCreated.Bind(OnSelectedDateChanged);
		m_ViewModel.m_SelectedTechCreator.Bind(OnSelectedTechCreatorChanged);
		m_ViewModel.m_SelectedTechFavourite.Bind(OnSelectedTechFavouriteChanged);
		m_ViewModel.m_SelectedTechFolder.Bind(OnSelectedTechFolderChanged);
		m_ViewModel.m_SearchFilterString.Bind(OnSearchFilterStringChanged);
		m_ViewModel.m_ShowOnlyAvailable.Bind(OnShowOnlyAvailableChanged);
		m_ViewModel.m_ShowOnlyFavourites.Bind(OnShowOnlyFavChanged);
		m_ViewModel.m_SnapshotSortTypeString.Bind(OnSortTypeListChanged);
		m_ViewModel.m_SnapshotSortType.Bind(OnSortTypeChanged);
		m_ViewModel.m_SortDescending.Bind(OnSortDecendingChanged);
		m_ViewModel.m_SteamBtnVisible.Bind(OnSteamBtnVisibleChanged);
		m_ViewModel.m_SteamBtnLabel.Bind(OnSteamBtnLabelChanged);
	}

	private void OnRecycle()
	{
		m_ViewModel.m_ViewVisible.Unbind(OnViewVisibilityChanged);
		m_ViewModel.m_Snapshots.Unbind(OnSnapshotsUpdated);
		m_ViewModel.m_SwapOptionEnabled.Unbind(OnSwapButtonEnabled);
		m_ViewModel.m_SwapOptionVisible.Unbind(OnSwapButtonVisibleChanged);
		m_ViewModel.m_PlaceOptionEnabled.Unbind(OnPlaceButtonEnabled);
		m_ViewModel.m_PlaceOptionVisible.Unbind(OnPlaceButtonVisibleChanged);
		m_ViewModel.m_FavouriteBtnEnabled.Unbind(OnFavouriteBtnEnabled);
		m_ViewModel.m_SetFolderBtnEnabled.Unbind(OnSetFolderButtonEnabled);
		m_ViewModel.m_RenameBtnEnabled.Unbind(OnRenameBtnEnabled);
		m_ViewModel.m_DeleteBtnEnabled.Unbind(OnDeleteBtnEnabled);
		m_ViewModel.m_FolderRenameBtnEnabled.Unbind(OnRenameBtnEnabled);
		m_ViewModel.m_FolderDeleteBtnEnabled.Unbind(OnDeleteBtnEnabled);
		m_ViewModel.m_WarningsVisible.Unbind(OnWarningsVisibleChanged);
		m_ViewModel.m_OptionsVisbile.Unbind(OnFilterOptionsVisibleChanged);
		m_ViewModel.m_SearchVisible.Unbind(OnSearchVisibleChanged);
		m_ViewModel.m_ActionsVisbile.Unbind(OnManagementOptionsVisibleChanged);
		m_ViewModel.m_LoadingStatus.Unbind(OnLoadingStatusChanged);
		m_ViewModel.m_LoadingStatusVisible.Unbind(OnLoadingStatusVisibleChanged);
		m_ViewModel.m_Warnings.Unbind(OnWarningsChagned);
		m_ViewModel.m_SelectedFolderName.Unbind(OnSelectedFolderChanged);
		m_ViewModel.m_Selected.Unbind(OnSelectedChanged);
		m_ViewModel.m_SelectedTechName.Unbind(OnSelectedTechNameChanged);
		m_ViewModel.m_SelectedTechCost.Unbind(OnSelectedTechCostChanged);
		m_ViewModel.m_SelectedBlockCost.Unbind(OnSelectedBlockCostChanged);
		m_ViewModel.m_SelectedDateCreated.Unbind(OnSelectedDateChanged);
		m_ViewModel.m_SelectedTechCreator.Unbind(OnSelectedTechCreatorChanged);
		m_ViewModel.m_SelectedTechFavourite.Unbind(OnSelectedTechFavouriteChanged);
		m_ViewModel.m_SelectedTechFolder.Unbind(OnSelectedTechFolderChanged);
		m_ViewModel.m_SearchFilterString.Unbind(OnSearchFilterStringChanged);
		m_ViewModel.m_ShowOnlyAvailable.Unbind(OnShowOnlyAvailableChanged);
		m_ViewModel.m_ShowOnlyFavourites.Unbind(OnShowOnlyFavChanged);
		m_ViewModel.m_SnapshotSortTypeString.Unbind(OnSortTypeListChanged);
		m_ViewModel.m_SnapshotSortType.Unbind(OnSortTypeChanged);
		m_ViewModel.m_SortDescending.Unbind(OnSortDecendingChanged);
		m_ViewModel.m_SteamBtnVisible.Unbind(OnSteamBtnVisibleChanged);
		m_ViewModel.m_SteamBtnLabel.Unbind(OnSteamBtnLabelChanged);
		UnpackAndRecycleAllFolders();
		for (int num = m_SnapshotItems.Count - 1; num >= 0; num--)
		{
			RemoveItem(num);
		}
		m_ItemsDirty = false;
		m_DeferredCreateNewFolderRequest = -1;
		m_SortTypeDropdown.Hide();
	}

	private void Update()
	{
		if (m_ItemsDirty)
		{
			UpdateItems();
		}
		if (m_WarningsDirty)
		{
			UpdateWarnings();
		}
		if (m_DeferredCreateNewFolderRequest != -1)
		{
			if (m_DeferredCreateNewFolderRequest == 1)
			{
				OnCreateNewFolderButtonClicked();
				m_DeferredCreateNewFolderRequest = -1;
			}
			else
			{
				m_DeferredCreateNewFolderRequest++;
			}
		}
		if (!m_FolderToSelectName.NullOrEmpty())
		{
			foreach (UISnapshotFolder value in m_SnapshotFoldersLookup.Values)
			{
				if (value.GetData().Name == m_FolderToSelectName)
				{
					value.SetSelected(isSelected: true);
					UIHelpers.VertScrollToItem(m_GridScrollRect.content, value.GetComponent<RectTransform>(), m_GridScrollRect.viewport.rect.height);
					break;
				}
			}
			m_FolderToSelectName = string.Empty;
		}
		if (!(m_ItemToSelect != default(SnapshotLiveData)))
		{
			return;
		}
		for (int i = 0; i < m_SnapshotItems.Count; i++)
		{
			UISnapshotItem uISnapshotItem = m_SnapshotItems[i];
			if (uISnapshotItem.GetData() == m_ItemToSelect)
			{
				uISnapshotItem.SetSelected(isSelected: true);
				UIHelpers.VertScrollToEmbeddedItem(m_GridScrollRect.content, uISnapshotItem.GetComponent<RectTransform>(), m_GridScrollRect.viewport);
				break;
			}
		}
		m_ItemToSelect = default(SnapshotLiveData);
	}
}
