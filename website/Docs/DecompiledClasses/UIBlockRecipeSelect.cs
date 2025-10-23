#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIBlockRecipeSelect : UIItemRecipeSelect
{
	[SerializeField]
	private Text m_SelectedBlockTier;

	[SerializeField]
	private CategoryOrder m_CategoryOrder;

	[SerializeField]
	private BlockSortOrder m_IndividualSortOrder;

	private bool AccessAllBlocks => !Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>();

	protected override bool CanMakeRecipe(RecipeTable.Recipe recipe)
	{
		return recipe.m_OutputItems[0].m_Item.ObjectType == ObjectTypes.Block;
	}

	protected override void SetupGridFilterCallbacks()
	{
		UIBlockSelectGrid obj = base.ItemGrid as UIBlockSelectGrid;
		d.Assert(obj != null, "UIBlockRecipeSelect.SetupGridFilterCallbacks - UIItemSelectGrid Grid assigned to UIBlockRecipeSelect was not of type UIBlockSelectGrid!");
		obj.CorpFilter = CorpFilterFunction;
		obj.BlockFilter = BlockFilterFunction;
		obj.CategoryOrder = m_CategoryOrder;
		obj.IndividualSortOrder = m_IndividualSortOrder;
		base.ItemGrid.ItemDisplayCallback = BlockDisplayFunction;
		base.ItemGrid.SelectionChanged.Subscribe(OnSelectionChanged);
	}

	protected override int GetItemCategory(ItemTypeInfo itemInfo)
	{
		return (int)Singleton.Manager<ManSpawn>.inst.GetCategory((BlockTypes)itemInfo.ItemType);
	}

	protected override void SetupCategoryToggles()
	{
		base.CategoryToggles.SetupAsBlockCategoryToggles(m_CategoryOrder);
		base.CategoryToggles.ToggleAllOn();
	}

	protected override string GetItemDescriptionString(ItemTypeInfo itemInfo)
	{
		BlockTypes itemType = (BlockTypes)itemInfo.ItemType;
		if (AccessAllBlocks || Singleton.Manager<ManLicenses>.inst.GetBlockState(itemType) == ManLicenses.BlockState.Discovered)
		{
			return base.GetItemDescriptionString(itemInfo);
		}
		return Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Crafting, 3);
	}

	protected override string GetCategoryName(int categoryIdx)
	{
		return StringLookup.GetBlockCategoryName((BlockCategories)categoryIdx);
	}

	private bool CorpFilterFunction(FactionSubTypes corp)
	{
		if (base.Consume == null)
		{
			return false;
		}
		if (base.Consume.CraftingFaction == FactionSubTypes.SPE)
		{
			return Singleton.Manager<ManMods>.inst.IsModdedCorp(corp);
		}
		return corp == base.Consume.CraftingFaction;
	}

	private bool BlockFilterFunction(BlockTypes blockType)
	{
		bool result = false;
		if (Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(blockType))
		{
			return false;
		}
		BlockCategories category = Singleton.Manager<ManSpawn>.inst.GetCategory(blockType);
		ItemTypeInfo itemInfo = new ItemTypeInfo(ObjectTypes.Block, (int)blockType);
		if (base.CategoryToggles.Selection.Contains((int)category) && HaveRecipeForItem(itemInfo))
		{
			int blockTier = Singleton.Manager<ManLicenses>.inst.GetBlockTier(blockType);
			FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(blockType);
			int num = Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(corporation) + 1;
			result = blockTier <= num || Singleton.Manager<ManLicenses>.inst.GetBlockState(blockType) == ManLicenses.BlockState.Discovered || AccessAllBlocks;
		}
		return result;
	}

	private UIItemGridButton.DisplayParams BlockDisplayFunction(ItemTypeInfo itemTypeInfo)
	{
		return new UIItemGridButton.DisplayParams
		{
			m_ItemColour = Color.white,
			m_ItemBackgroundColour = new Color(1f, 1f, 1f, 0f),
			m_DisplayQuantity = false,
			m_Quantity = -1,
			m_ShowAsUndiscovered = (!AccessAllBlocks && Singleton.Manager<ManLicenses>.inst.GetBlockState((BlockTypes)itemTypeInfo.ItemType) != ManLicenses.BlockState.Discovered),
			m_ShowDeathStreakReward = false
		};
	}

	private void OnSelectionChanged()
	{
		ItemTypeInfo selection = base.ItemGrid.GetSelection();
		if (selection != null && HaveRecipeForItem(selection))
		{
			BlockTypes itemType = (BlockTypes)selection.ItemType;
			m_SelectedBlockTier.text = StringLookup.GetBlockTierName(itemType);
		}
		else
		{
			m_SelectedBlockTier.text = string.Empty;
		}
	}
}
