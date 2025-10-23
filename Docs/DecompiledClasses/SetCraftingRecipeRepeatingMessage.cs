using UnityEngine.Networking;

public class SetCraftingRecipeRepeatingMessage : MessageBase, IBlockMessage
{
	public uint m_BlockPoolID;

	public bool m_RecipeRepeating;

	uint IBlockMessage.BlockPoolID
	{
		get
		{
			return m_BlockPoolID;
		}
		set
		{
			m_BlockPoolID = value;
		}
	}

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockPoolID);
		writer.Write(m_RecipeRepeating);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadPackedUInt32();
		m_RecipeRepeating = reader.ReadBoolean();
	}
}
