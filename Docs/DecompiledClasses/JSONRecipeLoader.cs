using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class JSONRecipeLoader : JSONModuleLoader
{
	public override string GetModuleKey()
	{
		return "Recipe";
	}

	public override bool CreateModuleForBlock(int blockID, ModdedBlockDefinition def, TankBlock block, JToken data)
	{
		return true;
	}

	public override bool InjectBlock(int blockID, ModdedBlockDefinition def, JToken jToken)
	{
		if (jToken.Type == JTokenType.String)
		{
			string[] array = jToken.ToObject<string>().Split(',');
			RecipeTable.Recipe recipe = new RecipeTable.Recipe();
			Dictionary<ChunkTypes, int> dictionary = new Dictionary<ChunkTypes, int>();
			for (int i = 0; i < array.Length; i++)
			{
				Enum.TryParse<ChunkTypes>(array[i].Trim(' '), ignoreCase: true, out var result);
				if (!dictionary.TryGetValue(result, out var value))
				{
					value = 0;
				}
				value = (dictionary[result] = value + 1);
			}
			recipe.m_InputItems = new RecipeTable.Recipe.ItemSpec[dictionary.Count];
			int num2 = 0;
			foreach (KeyValuePair<ChunkTypes, int> item2 in dictionary)
			{
				ChunkTypes key = item2.Key;
				int value2 = item2.Value;
				ItemTypeInfo item = new ItemTypeInfo(ObjectTypes.Chunk, (int)key);
				recipe.m_InputItems[num2++] = new RecipeTable.Recipe.ItemSpec(item, value2);
			}
			recipe.m_OutputItems[0] = new RecipeTable.Recipe.ItemSpec(new ItemTypeInfo(ObjectTypes.Block, blockID), 1);
			Singleton.Manager<RecipeManager>.inst.RegisterCustomBlockFabricatorRecipe(blockID, def.m_Corporation, recipe);
			return true;
		}
		return false;
	}
}
