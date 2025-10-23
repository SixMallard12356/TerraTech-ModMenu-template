using System.Collections.Generic;

public interface ItemSearchConverter
{
	IEnumerable<RecipeTable.Recipe> GetAllRecipes();

	RecipeTable.Recipe GetRecipeProducing(ItemTypeInfo outputType);

	bool ConvertsFromType(ItemTypeInfo inputType);

	bool ConvertsToType(ItemTypeInfo outputType);

	bool CanAcceptRecipeRequest(RecipeTable.Recipe recipe);

	void MakeRecipeRequest(RecipeTable.Recipe recipe, Bitfield<int> inputItemWarnings);

	bool IsHandlingRecipeRequest();

	bool AllowsMultipleRecipes();
}
