using System;
using UnityEngine;

[Serializable]
public class RecipeListWrapper : ScriptableObject
{
	[SerializeField]
	public RecipeTable.RecipeList target;
}
