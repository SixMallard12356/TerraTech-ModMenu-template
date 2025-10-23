#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIFilterMenu : UIHUDElement
{
	public enum FilterAcceptMode
	{
		Any,
		SpecificItem,
		None,
		SpecificCategory
	}

	[EnumArray(typeof(ChunkRarity))]
	[SerializeField]
	private RectTransform[] m_SpecificChunkPrefabs = new RectTransform[1];

	[FormerlySerializedAs("m_ExcludedTypes")]
	[SerializeField]
	private ChunkTypes[] m_ExcludedChunkTypes = new ChunkTypes[0];

	[SerializeField]
	private RadialMenu m_RadialMenu;

	private ModuleItemFilter m_TargetFilter;

	private IRadialInputController m_RadialController;

	private UIFilterChunkSubmenu m_ChunkSubmenu;

	private UIFilterCategorySubmenu m_CategorySubmenu;

	public bool SpecificGroupMenuActive
	{
		get
		{
			if (m_CategorySubmenu != null)
			{
				return m_CategorySubmenu.IsOpen;
			}
			return false;
		}
	}

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		ModuleItemFilter component = openMenuEventData.m_TargetTankBlock.GetComponent<ModuleItemFilter>();
		if (IsMenuAvailableForFilter(component) && openMenuEventData.m_AllowRadialMenu)
		{
			m_RadialController = Singleton.Manager<ManInput>.inst.GetRadialInputController(openMenuEventData.m_RadialInputController);
			m_RadialController.Activate();
			if (!m_RadialController.IsGamePad())
			{
				m_RadialMenu.SetDisableInputOnSubmenuOpen(disable: false);
			}
			m_TargetFilter = component;
			base.Show(context);
			m_RadialMenu.Show(openMenuEventData.m_RadialInputController, m_TargetFilter.block.tank != Singleton.playerTank);
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_RadialMenu.Hide();
		m_RadialController.Deactivate();
		m_RadialController = null;
		m_TargetFilter = null;
	}

	private void OnOptionSelected(int option)
	{
		bool flag = true;
		bool flag2 = option == -1;
		bool flag3 = true;
		ModuleItemFilter.AcceptMode filterAcceptMode;
		switch ((FilterAcceptMode)option)
		{
		case FilterAcceptMode.Any:
			filterAcceptMode = ModuleItemFilter.AcceptMode.Any;
			break;
		case FilterAcceptMode.SpecificItem:
			filterAcceptMode = ModuleItemFilter.AcceptMode.None;
			if (m_RadialController.IsGamePad())
			{
				m_RadialMenu.OpenSubmenu(0, null);
				flag = false;
			}
			break;
		case FilterAcceptMode.None:
			filterAcceptMode = ModuleItemFilter.AcceptMode.None;
			break;
		case FilterAcceptMode.SpecificCategory:
			filterAcceptMode = ModuleItemFilter.AcceptMode.None;
			if (m_RadialController.IsGamePad())
			{
				m_RadialMenu.OpenSubmenu(1, null);
				flag = false;
			}
			break;
		default:
			flag3 = !flag2;
			d.AssertFormat(flag3, "UIFilterMenu OnOptionSelected - Unhandled option passed in! {0}", option);
			filterAcceptMode = ModuleItemFilter.AcceptMode.None;
			break;
		}
		if (flag3 && !flag2)
		{
			m_TargetFilter.RequestSetAcceptMode(filterAcceptMode);
		}
		if (flag)
		{
			HideSelf();
		}
	}

	private void OnOptionHovered(int option)
	{
		if (!m_RadialController.IsGamePad())
		{
			if (option == 1)
			{
				m_RadialMenu.OpenSubmenu(0, null);
			}
			else
			{
				m_RadialMenu.CloseSubmenu(0);
			}
			if (option == 3)
			{
				m_RadialMenu.OpenSubmenu(1, null);
			}
			else
			{
				m_RadialMenu.CloseSubmenu(1);
			}
		}
	}

	public void OnSpecificItemSelected(ItemTypeInfo selectedType)
	{
		m_TargetFilter.RequestSetAcceptMode(ModuleItemFilter.AcceptMode.SpecificItem, selectedType);
		HideSelf();
	}

	public void OnSpecificCategorySelected(ChunkCategory selectedCategory)
	{
		ModuleItemFilter.AcceptMode filterAcceptMode;
		switch (selectedCategory)
		{
		case ChunkCategory.Raw:
			filterAcceptMode = ModuleItemFilter.AcceptMode.RawResource;
			break;
		case ChunkCategory.Refined:
			filterAcceptMode = ModuleItemFilter.AcceptMode.RefinedResource;
			break;
		case ChunkCategory.Component:
			filterAcceptMode = ModuleItemFilter.AcceptMode.Component;
			break;
		case ChunkCategory.Fuel:
			filterAcceptMode = ModuleItemFilter.AcceptMode.Fuel;
			break;
		default:
			d.LogError("UIFilterMenu OnSpecificCategorySelected - Unhandled category passed in! " + selectedCategory);
			filterAcceptMode = ModuleItemFilter.AcceptMode.None;
			break;
		}
		m_TargetFilter.RequestSetAcceptMode(filterAcceptMode);
		HideSelf();
	}

	public bool IsMenuAvailableForFilter(ModuleItemFilter targetFilter)
	{
		bool num = !Singleton.Manager<ManGameMode>.inst.LockPlayerControls && Singleton.playerTank != null;
		bool flag = targetFilter != null && targetFilter.block != null && targetFilter.block.tank != null && ManSpawn.IsPlayerTeam(targetFilter.block.tank.Team);
		bool flag2 = !base.IsVisible && !targetFilter.FilterIsFixed;
		bool flag3 = Singleton.Manager<ManPointer>.inst.DraggingItem != null;
		bool flag4 = Singleton.Manager<ManPointer>.inst.IsInteractionBlocked || Singleton.Manager<ManHUD>.inst.IsAnyElementInGroupVisible(ManHUD.HUDGroup.ContextMenuBlocking);
		if (num && flag && flag2 && !flag4)
		{
			return !flag3;
		}
		return false;
	}

	public UIRadialMenuOption GetFilterModeButton(FilterAcceptMode acceptMode)
	{
		return m_RadialMenu.GetOption((int)acceptMode);
	}

	public QueryableSelectable GetChunkCategoryButton(ChunkCategory category)
	{
		return m_CategorySubmenu.GetChunkCategoryButton(category);
	}

	private void InitResourceMenu()
	{
		Dictionary<ChunkRarity, List<ItemTypeInfo>> dictionary = new Dictionary<ChunkRarity, List<ItemTypeInfo>>();
		EnumValuesIterator<ChunkTypes> enumerator = EnumIterator<ChunkTypes>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ChunkTypes current = enumerator.Current;
			if (IsChunkTypeSelectable(current))
			{
				ItemTypeInfo itemTypeInfo = new ItemTypeInfo(ObjectTypes.Chunk, (int)current);
				ChunkRarity descriptorFlags = (ChunkRarity)Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ChunkRarity>(itemTypeInfo.GetHashCode());
				if (!dictionary.TryGetValue(descriptorFlags, out var value))
				{
					value = new List<ItemTypeInfo>();
					dictionary.Add(descriptorFlags, value);
				}
				value.Add(itemTypeInfo);
			}
		}
		EnumValuesIterator<ChunkRarity> enumerator2 = EnumIterator<ChunkRarity>.Values().GetEnumerator();
		while (enumerator2.MoveNext())
		{
			ChunkRarity current2 = enumerator2.Current;
			if (!dictionary.TryGetValue(current2, out var value2))
			{
				continue;
			}
			RectTransform rectTransform = m_SpecificChunkPrefabs[(int)current2];
			d.Assert(rectTransform != null, "UIFilterMenu.InitResourceMenu - Failed to find specific Chunk prefab for items of rarity " + current2);
			if (!(rectTransform != null))
			{
				continue;
			}
			foreach (ItemTypeInfo item in value2)
			{
				m_ChunkSubmenu.AddItem(rectTransform.transform, item);
			}
		}
	}

	private bool IsChunkTypeSelectable(ChunkTypes chunkType)
	{
		if (chunkType == ChunkTypes.Null)
		{
			return false;
		}
		bool result = true;
		ChunkTypes[] excludedChunkTypes = m_ExcludedChunkTypes;
		foreach (ChunkTypes chunkTypes in excludedChunkTypes)
		{
			if (chunkType == chunkTypes)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private void OnSpawn()
	{
		InitResourceMenu();
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
		AddElementToGroup(ManHUD.HUDGroup.ContextMenuBlocking);
	}

	private void Awake()
	{
		d.Assert(m_RadialMenu != null);
		m_RadialMenu.OnOptionHovered.Subscribe(OnOptionHovered);
		m_RadialMenu.OnOptionSelected.Subscribe(OnOptionSelected);
		m_RadialMenu.OnClose.Subscribe(base.HideSelf);
		m_ChunkSubmenu = (UIFilterChunkSubmenu)m_RadialMenu.GetSubmenu(0);
		m_ChunkSubmenu.OnItemSelected.Subscribe(OnSpecificItemSelected);
		m_CategorySubmenu = (UIFilterCategorySubmenu)m_RadialMenu.GetSubmenu(1);
		m_CategorySubmenu.OnCategorySelected.Subscribe(OnSpecificCategorySelected);
	}
}
