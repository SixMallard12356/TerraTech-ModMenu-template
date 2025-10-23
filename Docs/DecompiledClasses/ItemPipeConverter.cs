#define UNITY_EDITOR
using System.Collections.Generic;

internal class ItemPipeConverter : ItemPipe
{
	private ItemSearchConverter m_Converter;

	private HashSet<ItemTypeInfo> m_ChildBuildables;

	public void Setup(ItemSearchConverter converter, ItemPipe parent)
	{
		m_Converter = converter;
		SetParent(parent);
	}

	public override void OfferItem(OfferedItem item)
	{
		if (m_Converter.ConvertsFromType(item.GetTypeInfo()))
		{
			base.OfferItem(item);
		}
	}

	public override bool TryBuildItem(ItemTypeInfo item, ItemBuildContext context, Pass pass, out bool hasMissingInputs)
	{
		bool result = false;
		hasMissingInputs = false;
		if (m_Converter.ConvertsToType(item))
		{
			RecipeTable.Recipe recipeProducing = m_Converter.GetRecipeProducing(item);
			d.AssertFormat(recipeProducing != null, "ItemPipeConverter.BuildPassableItemTypes null recipe although we convert to this type: {0}", item);
			bool flag = false;
			if (recipeProducing != null)
			{
				flag = m_Converter.AllowsMultipleRecipes() || (m_Converter.CanAcceptRecipeRequest(recipeProducing) && !context.IsConverterUsed(m_Converter));
			}
			if (flag && TryBuildItemFromRecipe(item, recipeProducing, context, pass, out hasMissingInputs))
			{
				result = true;
			}
		}
		return result;
	}

	protected override int EvaluatePriority()
	{
		if (!m_Converter.IsHandlingRecipeRequest())
		{
			return 0;
		}
		return 1;
	}

	private bool TryBuildItemFromRecipe(ItemTypeInfo item, RecipeTable.Recipe recipe, ItemBuildContext context, Pass pass, out bool hasMissingInputs)
	{
		ItemBuildContext.Marker m = context.CreateMarker();
		Bitfield<int> bitfield = ((pass == Pass.BuildWhatPossible) ? new Bitfield<int>() : null);
		context.AddBuildRec(new ItemBuildContext.ConversionRec
		{
			recipe = recipe,
			converter = m_Converter,
			inputWarnings = bitfield
		});
		bool flag = true;
		hasMissingInputs = false;
		for (int i = 0; i < recipe.m_InputItems.Length; i++)
		{
			RecipeTable.Recipe.ItemSpec itemSpec = recipe.m_InputItems[i];
			for (int j = 0; j < itemSpec.m_Quantity; j++)
			{
				bool hasMissingInputs2 = false;
				bool flag2 = TrySourceItemLocally(itemSpec.m_Item, context);
				if (!flag2)
				{
					foreach (ItemPipe child in m_Children)
					{
						if (child.TryBuildItem(itemSpec.m_Item, context, pass, out hasMissingInputs2))
						{
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2 && pass == Pass.BuildWhatPossible)
				{
					if (itemSpec.m_Item.ObjectType == ObjectTypes.Chunk && Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.IsDescriptorFlag(itemSpec.m_Item.GetHashCode(), typeof(ChunkCategory), 1))
					{
						flag2 = true;
						hasMissingInputs2 = true;
					}
					if (!flag2)
					{
						foreach (ItemPipe child2 in m_Children)
						{
							if (child2.CanBuildItemFromScratch(itemSpec.m_Item))
							{
								flag2 = true;
								hasMissingInputs2 = true;
								break;
							}
						}
					}
				}
				if (hasMissingInputs2)
				{
					if (bitfield != null)
					{
						bitfield.Set(i, enabled: true);
					}
					hasMissingInputs = true;
				}
				if (!flag2)
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				break;
			}
		}
		if (!flag)
		{
			context.RestoreMarker(m);
		}
		return flag;
	}

	protected override void Destroy()
	{
		base.Destroy();
		m_Converter = null;
	}

	protected override ItemSearchConverter GetConverter()
	{
		return m_Converter;
	}

	public override void DetermineBuildableTypes()
	{
		if (m_ChildBuildables == null)
		{
			m_ChildBuildables = new HashSet<ItemTypeInfo>();
		}
		foreach (ItemPipe child in m_Children)
		{
			child.DetermineBuildableTypes();
			HashSet<ItemTypeInfo>.Enumerator buildableTypes = child.BuildableTypes;
			while (buildableTypes.MoveNext())
			{
				m_ChildBuildables.Add(buildableTypes.Current);
			}
		}
		foreach (RecipeTable.Recipe allRecipe in m_Converter.GetAllRecipes())
		{
			bool flag = true;
			for (int i = 0; i < allRecipe.m_InputItems.Length; i++)
			{
				ItemTypeInfo item = allRecipe.m_InputItems[i].m_Item;
				if (!m_ChildBuildables.Contains(item) && !Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.IsDescriptorFlag(item.GetHashCode(), typeof(ChunkCategory), 1))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				for (int j = 0; j < allRecipe.m_OutputItems.Length; j++)
				{
					AddBuildableType(allRecipe.m_OutputItems[j].m_Item);
				}
			}
		}
		m_ChildBuildables.Clear();
	}
}
