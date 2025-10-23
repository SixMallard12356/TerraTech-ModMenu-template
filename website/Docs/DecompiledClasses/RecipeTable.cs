#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class RecipeTable : ScriptableObject
{
	[Serializable]
	public class RecipeList : IEnumerable<Recipe>, IEnumerable
	{
		public string m_Name;

		public bool m_Root;

		public float m_ValueAddFactor = 1.5f;

		public bool m_UseForMoneyRecipeCalculation = true;

		public bool m_UseForChunkCategoryCalculation = true;

		public List<Recipe> m_Recipes = new List<Recipe>();

		public IEnumerator<Recipe> GetEnumerator()
		{
			return m_Recipes.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	[Serializable]
	public class Recipe : IEquatable<Recipe>
	{
		[Serializable]
		public class ItemSpec : IEquatable<ItemSpec>, IComparable<ItemSpec>
		{
			public ItemTypeInfo m_Item;

			public int m_Quantity;

			public ItemSpec()
			{
			}

			public ItemSpec(ItemTypeInfo item, int quantity)
			{
				m_Item = item;
				m_Quantity = quantity;
			}

			public ItemSpec(ItemSpec copy)
			{
				m_Item = copy.m_Item;
				m_Quantity = copy.m_Quantity;
			}

			public static bool operator ==(ItemSpec a, ItemSpec b)
			{
				if ((object)a == b)
				{
					return true;
				}
				if ((object)a == null || (object)b == null)
				{
					return false;
				}
				return a.Equals(b);
			}

			public static bool operator !=(ItemSpec a, ItemSpec b)
			{
				return !(a == b);
			}

			public override bool Equals(object obj)
			{
				if (!(obj is ItemSpec))
				{
					return false;
				}
				return this == (ItemSpec)obj;
			}

			public bool Equals(ItemSpec other)
			{
				if (m_Item == other.m_Item)
				{
					return m_Quantity == other.m_Quantity;
				}
				return false;
			}

			public int CompareTo(ItemSpec other)
			{
				int num = m_Item.CompareTo(other.m_Item);
				if (num != 0)
				{
					return num;
				}
				return m_Quantity.CompareTo(other.m_Quantity);
			}

			public override int GetHashCode()
			{
				return m_Item.GetHashCode() | (m_Quantity << 16);
			}

			public override string ToString()
			{
				return m_Item.name + ((m_Quantity == 1) ? "" : (" x" + m_Quantity));
			}
		}

		public enum OutputType
		{
			Items,
			Energy,
			Money
		}

		public enum CompletionState
		{
			Open,
			CompleteExact,
			CompleteOver,
			Invalid
		}

		public enum CalcState
		{
			NeedUpdate,
			Root,
			Checked,
			Modified
		}

		public ItemSpec[] m_InputItems = new ItemSpec[1];

		public OutputType m_OutputType;

		public ItemSpec[] m_OutputItems = new ItemSpec[1];

		public float m_BuildTimeSeconds;

		public float m_EnergyOutput;

		public TechEnergy.EnergyType m_EnergyType;

		public int m_MoneyOutput;

		public CalcState m_CalcState;

		private static List<uint> s_HashCodeSorter = new List<uint>();

		[JsonIgnore]
		public ItemTypeInfo Output_Deprecated
		{
			get
			{
				d.Assert(m_OutputType == OutputType.Items && m_OutputItems.Length == 1 && m_OutputItems[0].m_Quantity == 1, "non-single-output-item recipes not supported");
				return m_OutputItems[0].m_Item;
			}
		}

		public bool InputsContain(ItemTypeInfo item)
		{
			for (int i = 0; i < m_InputItems.Length; i++)
			{
				if (m_InputItems[i].m_Item == item)
				{
					return true;
				}
			}
			return false;
		}

		public CompletionState CheckCompletion(List<ItemTypeInfo> items)
		{
			bool flag = false;
			int num = 0;
			int[] array = new int[m_InputItems.Length];
			for (int i = 0; i < m_InputItems.Length; i++)
			{
				array[i] = m_InputItems[i].m_Quantity;
			}
			foreach (ItemTypeInfo item in items)
			{
				int num2 = 0;
				for (num2 = 0; num2 < m_InputItems.Length; num2++)
				{
					if (array[num2] != 0 && m_InputItems[num2].m_Item == item)
					{
						array[num2]--;
						if (array[num2] == 0)
						{
							num++;
						}
						break;
					}
				}
				if (num2 == m_InputItems.Length)
				{
					flag = true;
				}
			}
			if (flag)
			{
				if (num == m_InputItems.Length)
				{
					return CompletionState.CompleteOver;
				}
				return CompletionState.Invalid;
			}
			if (num == m_InputItems.Length)
			{
				return CompletionState.CompleteExact;
			}
			return CompletionState.Open;
		}

		public IEnumerable<ItemTypeInfo> EnumerateInputs()
		{
			ItemSpec[] inputItems = m_InputItems;
			foreach (ItemSpec input in inputItems)
			{
				for (int count = 0; count < input.m_Quantity; count++)
				{
					yield return input.m_Item;
				}
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is Recipe recipe)
			{
				return this == recipe;
			}
			return false;
		}

		internal static bool ArraysEqual(ItemSpec[] a, ItemSpec[] b)
		{
			int num = ((a != null) ? a.Length : 0);
			int num2 = ((b != null) ? b.Length : 0);
			if (num == num2)
			{
				if (num != 0)
				{
					return a.All((ItemSpec s) => b.Contains(s));
				}
				return true;
			}
			return false;
		}

		public bool Equals(Recipe other)
		{
			if (m_OutputType == other.m_OutputType && m_EnergyType == other.m_EnergyType && m_MoneyOutput == other.m_MoneyOutput && m_BuildTimeSeconds == other.m_BuildTimeSeconds && m_EnergyOutput == other.m_EnergyOutput && ArraysEqual(m_InputItems, other.m_InputItems))
			{
				return ArraysEqual(m_OutputItems, other.m_OutputItems);
			}
			return false;
		}

		internal static uint ItemSpecListHash(ItemSpec[] itemSpecs)
		{
			uint num = 0u;
			if (itemSpecs != null)
			{
				lock (s_HashCodeSorter)
				{
					s_HashCodeSorter.Clear();
					for (int i = 0; i < itemSpecs.Length; i++)
					{
						s_HashCodeSorter.Add((uint)itemSpecs[i].GetHashCode());
					}
					s_HashCodeSorter.Sort();
					for (int j = 0; j < s_HashCodeSorter.Count; j++)
					{
						num = HashCodeUtility.FNVHashCombine(num, s_HashCodeSorter[j]);
					}
				}
			}
			return num;
		}

		public uint GetInputHash()
		{
			return ItemSpecListHash(m_InputItems);
		}

		public uint GetOutputHash()
		{
			return ItemSpecListHash(m_OutputItems);
		}

		public override int GetHashCode()
		{
			return (int)HashCodeUtility.FNVHashCombine(HashCodeUtility.FNVHashCombine(HashCodeUtility.FNVHashCombine(HashCodeUtility.FNVHashCombine(HashCodeUtility.FNVHashCombine(HashCodeUtility.FNVHashCombine(ItemSpecListHash(m_InputItems), (int)m_OutputType), ItemSpecListHash(m_OutputItems)), (int)m_EnergyType), m_BuildTimeSeconds), m_MoneyOutput), m_EnergyOutput);
		}

		public override string ToString()
		{
			string text = null;
			switch (m_OutputType)
			{
			case OutputType.Items:
				text = "null";
				if (m_OutputItems != null && m_OutputItems.Length != 0 && m_OutputItems[0].m_Item.ObjectType != ObjectTypes.Null)
				{
					text = ", ".JoinStrings(m_OutputItems);
				}
				break;
			case OutputType.Energy:
				text = $"Energy({m_EnergyOutput} {m_EnergyType})";
				break;
			case OutputType.Money:
				text = $"Money({m_MoneyOutput})";
				break;
			}
			return text + " <= " + ", ".JoinStrings(m_InputItems);
		}
	}

	public List<RecipeList> m_RecipeLists = new List<RecipeList>();

	public bool HasRuntimeChanges { get; set; }

	public RecipeList CreateOrUpdateInvertedItemList(RecipeList sourceList)
	{
		if (sourceList == null)
		{
			return null;
		}
		string invListName = sourceList.m_Name + "_inverse";
		RecipeList recipeList = m_RecipeLists.Find((RecipeList l) => l.m_Name == invListName);
		if (recipeList == null)
		{
			recipeList = new RecipeList
			{
				m_Name = invListName,
				m_Root = false,
				m_ValueAddFactor = 0f,
				m_UseForMoneyRecipeCalculation = false,
				m_UseForChunkCategoryCalculation = false
			};
			m_RecipeLists.Add(recipeList);
		}
		recipeList.m_Recipes.Clear();
		foreach (Recipe recipe in sourceList.m_Recipes)
		{
			if (recipe.m_OutputType != Recipe.OutputType.Items)
			{
				d.LogWarning($"Ignoring non-item recipe while creating inverted list from {sourceList.m_Name}: {recipe}");
				continue;
			}
			Recipe item = new Recipe
			{
				m_InputItems = recipe.m_OutputItems.Select((Recipe.ItemSpec i) => new Recipe.ItemSpec(i)).ToArray(),
				m_OutputType = Recipe.OutputType.Items,
				m_OutputItems = recipe.m_InputItems.Select((Recipe.ItemSpec i) => new Recipe.ItemSpec(i)).ToArray(),
				m_BuildTimeSeconds = 5f
			};
			recipeList.m_Recipes.Add(item);
		}
		return recipeList;
	}
}
