#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeManager : Singleton.Manager<RecipeManager>
{
	[Serializable]
	public struct RecipeNameWrapper
	{
		public string name;

		public bool inverted;
	}

	public struct RecipeDefinition : IEquatable<RecipeDefinition>
	{
		public RecipeTable.Recipe.ItemSpec[] m_InputItems;

		public RecipeTable.Recipe.OutputType m_OutputType;

		public RecipeTable.Recipe.ItemSpec[] m_OutputItems;

		public bool Equals(RecipeDefinition other)
		{
			if (m_OutputType == other.m_OutputType && RecipeTable.Recipe.ArraysEqual(m_InputItems, other.m_InputItems))
			{
				return RecipeTable.Recipe.ArraysEqual(m_OutputItems, other.m_OutputItems);
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is RecipeDefinition other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (int)HashCodeUtility.FNVHashCombine(HashCodeUtility.FNVHashCombine(RecipeTable.Recipe.ItemSpecListHash(m_InputItems), (int)m_OutputType), RecipeTable.Recipe.ItemSpecListHash(m_OutputItems));
		}

		public override string ToString()
		{
			string text = m_OutputType.ToString();
			if (m_OutputType == RecipeTable.Recipe.OutputType.Items)
			{
				text = ((m_OutputItems == null || m_OutputItems.Length == 0 || m_OutputItems[0].m_Item.ObjectType == ObjectTypes.Null) ? "null" : ", ".JoinStrings(m_OutputItems));
			}
			return text + " <= " + ", ".JoinStrings(m_InputItems);
		}

		public static explicit operator RecipeDefinition(RecipeTable.Recipe recipe)
		{
			RecipeDefinition result = default(RecipeDefinition);
			if (recipe != null)
			{
				result.m_InputItems = recipe.m_InputItems;
				result.m_OutputType = recipe.m_OutputType;
				result.m_OutputItems = recipe.m_OutputItems;
			}
			return result;
		}
	}

	public RecipeTable recipeTable;

	public ItemTypeInfo[] m_DontAutoGenerateRecipes;

	private Dictionary<int, int> m_BlockPriceLookup = new Dictionary<int, int>();

	private Dictionary<int, int> m_ChunkPriceLookup = new Dictionary<int, int>();

	private Dictionary<ItemTypeInfo, RecipeTable.Recipe> m_RecipeByOutputLookup = new Dictionary<ItemTypeInfo, RecipeTable.Recipe>();

	private Dictionary<string, RecipeTable.RecipeList> m_ModdedRecipes = new Dictionary<string, RecipeTable.RecipeList>();

	private Dictionary<int, int> m_ModdedBlockPriceLookup = new Dictionary<int, int>();

	private Dictionary<ItemTypeInfo, RecipeTable.Recipe> m_ModdedRecipeByOutputLookup = new Dictionary<ItemTypeInfo, RecipeTable.Recipe>();

	public RecipeListWrapper[] GetWrappedRecipeLists(RecipeNameWrapper[] wrappedNames)
	{
		List<RecipeListWrapper> list = new List<RecipeListWrapper>();
		for (int i = 0; i < wrappedNames.Length; i++)
		{
			RecipeNameWrapper wrapped = wrappedNames[i];
			RecipeTable.RecipeList recipeList = recipeTable.m_RecipeLists.Find((RecipeTable.RecipeList l) => l.m_Name == wrapped.name);
			if (recipeList == null)
			{
				d.LogWarning($"unknown recipe list '{wrapped.name}'");
				continue;
			}
			if (wrapped.inverted)
			{
				recipeList = recipeTable.CreateOrUpdateInvertedItemList(recipeList);
			}
			RecipeListWrapper recipeListWrapper = ScriptableObject.CreateInstance<RecipeListWrapper>();
			recipeListWrapper.name = recipeList.m_Name;
			recipeListWrapper.target = recipeList;
			list.Add(recipeListWrapper);
		}
		return list.ToArray();
	}

	public RecipeTable.Recipe GetRecipeByOutputType(ItemTypeInfo outputType)
	{
		RecipeTable.Recipe value = null;
		if (!m_RecipeByOutputLookup.TryGetValue(outputType, out value))
		{
			m_ModdedRecipeByOutputLookup.TryGetValue(outputType, out value);
		}
		return value;
	}

	public IEnumerable<RecipeTable.RecipeList> GetModdedRecipeLists(RecipeNameWrapper[] wrappedNames)
	{
		for (int i = 0; i < wrappedNames.Length; i++)
		{
			RecipeNameWrapper recipeNameWrapper = wrappedNames[i];
			string key = (recipeNameWrapper.inverted ? (recipeNameWrapper.name + "_inv") : recipeNameWrapper.name);
			if (m_ModdedRecipes.TryGetValue(key, out var value))
			{
				yield return value;
			}
		}
	}

	public void RegisterCustomBlockFabricatorRecipe(int blockID, string corp, RecipeTable.Recipe recipe)
	{
		string text = corp.ToLower() + "fab";
		if (text == "expfab")
		{
			text = "rrfab";
		}
		bool flag = false;
		foreach (RecipeTable.RecipeList recipeList in recipeTable.m_RecipeLists)
		{
			if (recipeList.m_Name == text)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			text = "modfab";
		}
		if (!m_ModdedRecipes.ContainsKey(text))
		{
			m_ModdedRecipes.Add(text, new RecipeTable.RecipeList());
		}
		m_ModdedRecipes[text].m_Recipes.Add(recipe);
		m_ModdedRecipeByOutputLookup.Add(new ItemTypeInfo(ObjectTypes.Block, blockID), recipe);
		string key = text + "_inv";
		if (!m_ModdedRecipes.ContainsKey(key))
		{
			m_ModdedRecipes.Add(key, new RecipeTable.RecipeList());
		}
		RecipeTable.Recipe item = new RecipeTable.Recipe
		{
			m_InputItems = recipe.m_OutputItems.Select((RecipeTable.Recipe.ItemSpec i) => new RecipeTable.Recipe.ItemSpec(i)).ToArray(),
			m_OutputType = RecipeTable.Recipe.OutputType.Items,
			m_OutputItems = recipe.m_InputItems.Select((RecipeTable.Recipe.ItemSpec i) => new RecipeTable.Recipe.ItemSpec(i)).ToArray(),
			m_BuildTimeSeconds = 5f
		};
		m_ModdedRecipes[key].m_Recipes.Add(item);
		d.Log($"[Mods] Registered recipe for blockID {blockID} with fabricator {text}");
	}

	public void RegisterCustomBlockRecipe(int blockID, int price)
	{
		m_ModdedBlockPriceLookup[blockID] = price;
	}

	public void RemoveCustomBlockRecipes()
	{
		m_ModdedRecipes.Clear();
		m_ModdedBlockPriceLookup.Clear();
		m_ModdedRecipeByOutputLookup.Clear();
	}

	private void InitBlockPrices()
	{
		foreach (RecipeTable.Recipe item in recipeTable.m_RecipeLists.SelectMany((RecipeTable.RecipeList x) => x.m_Recipes))
		{
			if (item.m_OutputType == RecipeTable.Recipe.OutputType.Money)
			{
				d.AssertFormat(item.m_InputItems.Length == 1 && item.m_InputItems[0].m_Quantity == 1, "money recipe must have 1 input only: {0}", item);
				if (item.m_InputItems[0].m_Item.ObjectType == ObjectTypes.Block)
				{
					m_BlockPriceLookup[item.m_InputItems[0].m_Item.ItemType] = item.m_MoneyOutput;
				}
			}
		}
	}

	private void InitChunkCategories()
	{
		foreach (RecipeTable.RecipeList recipeList in recipeTable.m_RecipeLists)
		{
			if (!recipeList.m_UseForChunkCategoryCalculation)
			{
				continue;
			}
			foreach (RecipeTable.Recipe recipe in recipeList.m_Recipes)
			{
				if (recipe.m_OutputType == RecipeTable.Recipe.OutputType.Items && recipe.m_OutputItems.Length == 1 && recipe.m_OutputItems[0].m_Item.ObjectType == ObjectTypes.Chunk && recipe.m_OutputItems[0].m_Quantity == 1)
				{
					int hashCode = recipe.m_OutputItems[0].m_Item.GetHashCode();
					ChunkCategory descriptorFlags = (ChunkCategory)Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ChunkCategory>(hashCode);
					if (recipe.m_InputItems.Length == 1 && recipe.m_InputItems[0].m_Quantity == 1 && recipe.m_InputItems[0].m_Item.ObjectType == ObjectTypes.Chunk)
					{
						Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.SetDescriptor(hashCode, descriptorFlags | ChunkCategory.Refined);
					}
					else if (recipe.m_InputItems.Length != 0)
					{
						Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.SetDescriptor(hashCode, descriptorFlags | ChunkCategory.Component);
						int num = 1;
						RecipeTable.Recipe.ItemSpec[] inputItems = recipe.m_InputItems;
						foreach (RecipeTable.Recipe.ItemSpec itemSpec in inputItems)
						{
							int hashCode2 = itemSpec.m_Item.GetHashCode();
							if ((Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ChunkCategory>(hashCode2) & 4) != 0)
							{
								ComponentTier descriptorFlags2 = (ComponentTier)Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ComponentTier>(hashCode2);
								d.AssertFormat(descriptorFlags2 != ComponentTier.Null, "RecipeManager.InitChunkCategories - Component tier for item {0} was not set! Invalid setup order in Recipe table!?", itemSpec.m_Item);
								if ((int)descriptorFlags2 >= num)
								{
									num = (int)(descriptorFlags2 + 1);
								}
							}
						}
						d.AssertFormat(Enum.IsDefined(typeof(ComponentTier), num), "RecipeManager.InitChunkCategories - Component tier {0} was not found!", num);
						Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.SetDescriptor(hashCode, (ComponentTier)num);
					}
					else
					{
						d.LogError("Found Recipe that produces output items, but there are no input items to produce it from! " + recipe);
					}
				}
				else if (recipe.m_OutputType == RecipeTable.Recipe.OutputType.Energy && recipe.m_EnergyType == TechEnergy.EnergyType.Electric && recipe.m_InputItems.Length == 1)
				{
					int hashCode3 = recipe.m_InputItems[0].m_Item.GetHashCode();
					ChunkCategory descriptorFlags3 = (ChunkCategory)Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ChunkCategory>(hashCode3);
					Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.SetDescriptor(hashCode3, descriptorFlags3 | ChunkCategory.Fuel);
				}
				else if (recipe.m_OutputType == RecipeTable.Recipe.OutputType.Money && recipe.m_InputItems.Length == 1 && recipe.m_InputItems[0].m_Quantity == 1 && recipe.m_InputItems[0].m_Item.ObjectType == ObjectTypes.Chunk)
				{
					m_ChunkPriceLookup[recipe.m_InputItems[0].m_Item.ItemType] = recipe.m_MoneyOutput;
				}
			}
		}
		foreach (object value in Enum.GetValues(typeof(ChunkTypes)))
		{
			int hashCode4 = ItemTypeInfo.GetHashCode(ObjectTypes.Chunk, (int)value);
			ChunkCategory descriptorFlags4 = (ChunkCategory)Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ChunkCategory>(hashCode4);
			if ((descriptorFlags4 & (ChunkCategory.Refined | ChunkCategory.Component)) == 0)
			{
				Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.SetDescriptor(hashCode4, descriptorFlags4 | ChunkCategory.Raw);
			}
		}
	}

	public int GetBlockBuyPrice(BlockTypes blockType, bool silentFail = false)
	{
		int value = -1;
		if (!m_BlockPriceLookup.TryGetValue((int)blockType, out value))
		{
			m_ModdedBlockPriceLookup.TryGetValue((int)blockType, out value);
		}
		return value;
	}

	public int GetBlockSellPrice(BlockTypes blockType)
	{
		return ConvertBlockBuyToSellPrice(GetBlockBuyPrice(blockType, silentFail: true));
	}

	public int ConvertBlockBuyToSellPrice(int blockBuyPrice)
	{
		return (int)((float)blockBuyPrice * Globals.inst.m_BlockSellingModifier);
	}

	public int GetChunkPrice(ChunkTypes chunkType)
	{
		int value = -1;
		m_ChunkPriceLookup.TryGetValue((int)chunkType, out value);
		return value;
	}

	public int GetTechPrice(TechData techData, bool silentFail = false)
	{
		int num = 0;
		for (int i = 0; i < techData.m_BlockSpecs.Count; i++)
		{
			num += GetBlockBuyPrice(techData.m_BlockSpecs[i].GetBlockType(), silentFail);
		}
		return num;
	}

	public ChunkCategory GetChunkDominantCategory(ChunkCategory chunkCat)
	{
		if ((chunkCat & ChunkCategory.Component) == ChunkCategory.Component)
		{
			return ChunkCategory.Component;
		}
		if ((chunkCat & ChunkCategory.Refined) == ChunkCategory.Refined)
		{
			return ChunkCategory.Refined;
		}
		if ((chunkCat & ChunkCategory.Raw) == ChunkCategory.Raw)
		{
			return ChunkCategory.Raw;
		}
		if ((chunkCat & ChunkCategory.Fuel) == ChunkCategory.Fuel)
		{
			return ChunkCategory.Fuel;
		}
		d.LogError(string.Concat("*Error* - RecipeManager.GetDominantChunkCat: Could not find suitable selection for ", chunkCat, ". Is this defined in the code that called this error?"));
		return ChunkCategory.Null;
	}

	private void CreateRecipeLookup()
	{
		foreach (RecipeTable.RecipeList recipeList in recipeTable.m_RecipeLists)
		{
			foreach (RecipeTable.Recipe recipe in recipeList.m_Recipes)
			{
				if (recipe.m_OutputType == RecipeTable.Recipe.OutputType.Items && recipe.m_OutputItems.Length == 1 && recipe.m_OutputItems[0].m_Quantity == 1)
				{
					ItemTypeInfo item = recipe.m_OutputItems[0].m_Item;
					if (!m_RecipeByOutputLookup.TryGetValue(item, out var value))
					{
						m_RecipeByOutputLookup.Add(item, recipe);
					}
					else if (recipe != value)
					{
						d.LogErrorFormat("RecipeTable contains more than one recipe that outputs {0} : [{1}] and [{2}]", item, recipe, value);
					}
				}
			}
		}
	}

	private void Awake()
	{
		InitBlockPrices();
		InitChunkCategories();
		CreateRecipeLookup();
	}
}
