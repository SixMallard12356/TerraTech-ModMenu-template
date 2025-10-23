using UnityEngine.Networking;

internal static class NetworkSerialisationExtensions
{
	public static void Serialize(this NetworkWriter writer, RecipeManager.RecipeDefinition recipeDef)
	{
		int num = ((recipeDef.m_InputItems != null) ? recipeDef.m_InputItems.Length : 0);
		writer.WritePackedInt32(num);
		if (num > 0)
		{
			RecipeTable.Recipe.ItemSpec[] inputItems = recipeDef.m_InputItems;
			foreach (RecipeTable.Recipe.ItemSpec itemSpec in inputItems)
			{
				writer.Serialize(itemSpec.m_Item);
				writer.WritePackedInt32(itemSpec.m_Quantity);
			}
		}
		num = ((recipeDef.m_OutputItems != null) ? recipeDef.m_OutputItems.Length : 0);
		writer.WritePackedInt32(num);
		if (num > 0)
		{
			RecipeTable.Recipe.ItemSpec[] inputItems = recipeDef.m_OutputItems;
			foreach (RecipeTable.Recipe.ItemSpec itemSpec2 in inputItems)
			{
				writer.Serialize(itemSpec2.m_Item);
				writer.WritePackedInt32(itemSpec2.m_Quantity);
			}
		}
		writer.WritePackedInt32((int)recipeDef.m_OutputType);
	}

	public static void Deserialize(this NetworkReader reader, out RecipeManager.RecipeDefinition recipeDef)
	{
		int num = reader.ReadPackedInt32();
		recipeDef.m_InputItems = new RecipeTable.Recipe.ItemSpec[num];
		for (int i = 0; i < num; i++)
		{
			reader.Deserialize(out ItemTypeInfo itemInfo);
			int quantity = reader.ReadPackedInt32();
			recipeDef.m_InputItems[i] = new RecipeTable.Recipe.ItemSpec(itemInfo, quantity);
		}
		int num2 = reader.ReadPackedInt32();
		recipeDef.m_OutputItems = new RecipeTable.Recipe.ItemSpec[num2];
		for (int j = 0; j < num2; j++)
		{
			reader.Deserialize(out ItemTypeInfo itemInfo2);
			int quantity2 = reader.ReadPackedInt32();
			recipeDef.m_OutputItems[j] = new RecipeTable.Recipe.ItemSpec(itemInfo2, quantity2);
		}
		recipeDef.m_OutputType = (RecipeTable.Recipe.OutputType)reader.ReadPackedInt32();
	}

	public static void Serialize(this NetworkWriter writer, ItemTypeInfo itemInfo)
	{
		writer.WritePackedInt32((int)itemInfo.ObjectType);
		writer.WritePackedInt32(itemInfo.ItemType);
	}

	public static void Deserialize(this NetworkReader reader, out ItemTypeInfo itemInfo)
	{
		int objectType = reader.ReadPackedInt32();
		int itemType = reader.ReadPackedInt32();
		itemInfo = new ItemTypeInfo((ObjectTypes)objectType, itemType);
	}
}
