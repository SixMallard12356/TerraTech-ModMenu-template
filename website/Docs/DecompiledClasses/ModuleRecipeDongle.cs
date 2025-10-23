using UnityEngine;

[RequireComponent(typeof(ModuleRecipeProvider))]
public class ModuleRecipeDongle : Module
{
	[HideInInspector]
	[SerializeField]
	public ModuleRecipeProvider recipeProvider;

	private void PrePool()
	{
		recipeProvider = GetComponent<ModuleRecipeProvider>();
	}
}
