using UnityEngine.Networking;

public class SetCraftingRecipeMessage : MessageBase
{
	public uint m_BlockPoolID;

	public bool m_HasRecipe;

	public RecipeManager.RecipeDefinition m_RecipeDef;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockPoolID);
		writer.Write(m_HasRecipe);
		if (m_HasRecipe)
		{
			writer.Serialize(m_RecipeDef);
		}
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadPackedUInt32();
		m_HasRecipe = reader.ReadBoolean();
		if (m_HasRecipe)
		{
			reader.Deserialize(out m_RecipeDef);
		}
	}
}
