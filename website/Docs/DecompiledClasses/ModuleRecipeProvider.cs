#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleRecipeProvider : Module
{
	[SerializeField]
	private RecipeManager.RecipeNameWrapper[] m_RecipeListNames;

	[SerializeField]
	[HideInInspector]
	private RecipeListWrapper[] m_RecipeLists;

	public IEnumerator<RecipeTable.RecipeList> GetEnumerator()
	{
		RecipeListWrapper[] recipeLists = m_RecipeLists;
		foreach (RecipeListWrapper recipeListWrapper in recipeLists)
		{
			yield return recipeListWrapper.target;
		}
		foreach (RecipeTable.RecipeList moddedRecipeList in Singleton.Manager<RecipeManager>.inst.GetModdedRecipeLists(m_RecipeListNames))
		{
			yield return moddedRecipeList;
		}
	}

	private void ValidateRecipes()
	{
		Dictionary<uint, RecipeTable.Recipe> dictionary = new Dictionary<uint, RecipeTable.Recipe>();
		RecipeListWrapper[] recipeLists = m_RecipeLists;
		for (int i = 0; i < recipeLists.Length; i++)
		{
			foreach (RecipeTable.Recipe item in recipeLists[i].target)
			{
				uint inputHash = item.GetInputHash();
				if (dictionary.TryGetValue(inputHash, out var value))
				{
					d.LogError($"ModuleRecipeProvider - Duplicate recipe inputs? {item}\n {value}\n (input hash clash: {inputHash})");
				}
				else
				{
					dictionary.Add(inputHash, item);
				}
			}
		}
	}

	private void PrePool()
	{
		m_RecipeLists = Singleton.Manager<RecipeManager>.inst.GetWrappedRecipeLists(m_RecipeListNames);
	}

	private void OnSpawn()
	{
		m_RecipeLists = Singleton.Manager<RecipeManager>.inst.GetWrappedRecipeLists(m_RecipeListNames);
	}
}
