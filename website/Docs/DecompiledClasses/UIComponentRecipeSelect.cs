#define UNITY_EDITOR
public class UIComponentRecipeSelect : UIItemRecipeSelect
{
	protected override bool CanMakeRecipe(RecipeTable.Recipe recipe)
	{
		return recipe.m_OutputItems[0].m_Item.ObjectType == ObjectTypes.Chunk;
	}

	protected override void SetupGridFilterCallbacks()
	{
		UIChunkSelectGrid obj = base.ItemGrid as UIChunkSelectGrid;
		d.Assert(obj != null, "UIComponentRecipeSelect.OnPool - UIItemSelectGrid Grid assigned to UIComponentRecipeSelect was not of type UIChunkSelectGrid!");
		obj.ChunkFilter = ChunkFilterFunction;
	}

	protected override int GetItemCategory(ItemTypeInfo itemInfo)
	{
		return Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ComponentTier>(itemInfo.GetHashCode());
	}

	protected override void UpdateAvailableCategories()
	{
		int count = EnumValuesIterator<ComponentTier>.Count;
		bool[] array = new bool[count];
		foreach (RecipeTable.Recipe item2 in base.RecipesWeCanMake)
		{
			ItemTypeInfo item = item2.m_OutputItems[0].m_Item;
			ComponentTier descriptorFlags = (ComponentTier)Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ComponentTier>(item.GetHashCode());
			array[(int)descriptorFlags] = true;
		}
		bool flag = false;
		int num = -1;
		for (int i = 0; i < count; i++)
		{
			if (array[i] && num == -1)
			{
				num = i;
			}
			base.CategoryToggles.SetToggleInteractable(i, array[i]);
			ToggleWrapper toggle = base.CategoryToggles.GetToggle(i);
			if (toggle != null && toggle.isOn)
			{
				flag = true;
			}
		}
		if (!flag && num != -1)
		{
			base.CategoryToggles.SetToggleSelected(num, selected: true);
		}
	}

	protected override void SetupCategoryToggles()
	{
		base.CategoryToggles.SetupAsComponentTierToggles();
		base.CategoryToggles.SetToggleSelected(1, selected: true);
	}

	protected override string GetCategoryName(int categoryIdx)
	{
		return StringLookup.GetComponentTierName((ComponentTier)categoryIdx);
	}

	private bool ChunkFilterFunction(ChunkTypes chunkType)
	{
		bool result = false;
		ItemTypeInfo itemTypeInfo = new ItemTypeInfo(ObjectTypes.Chunk, (int)chunkType);
		if (HaveRecipeForItem(itemTypeInfo))
		{
			ComponentTier descriptorFlags = (ComponentTier)Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ComponentTier>(itemTypeInfo.GetHashCode());
			if (base.CategoryToggles.Selection.Contains((int)descriptorFlags))
			{
				result = true;
			}
		}
		return result;
	}

	private void OnSpawn()
	{
		d.Assert(base.CategoryToggles.NumToggles == EnumValuesIterator<ComponentTier>.Count - 1, "UIComponentChunkRecipeSelect - The number of category toggles did not match the number of component tiers!");
	}
}
