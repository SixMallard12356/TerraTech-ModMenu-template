using System.Collections.Generic;
using UnityEngine.Networking;

public class ItemConsumeProgressMessage : MessageBase, IBlockMessage
{
	public uint m_BlockPoolID;

	public bool m_HasRecipe;

	public RecipeManager.RecipeDefinition m_RecipeDef;

	public byte m_RecipeRequester;

	public List<ItemTypeInfo> m_InputsRemaining;

	public int m_WantedInputItemWarningsFlags;

	public Stack<ItemTypeInfo> m_OutputQueue;

	public List<ItemTypeInfo> m_WantedItems;

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
		writer.Write(m_HasRecipe);
		if (m_HasRecipe)
		{
			writer.Serialize(m_RecipeDef);
			writer.Write(m_RecipeRequester);
			writer.WritePackedInt32(m_InputsRemaining.Count);
			foreach (ItemTypeInfo item in m_InputsRemaining)
			{
				writer.Serialize(item);
			}
			writer.WritePackedInt32(m_WantedInputItemWarningsFlags);
			writer.WritePackedInt32(m_OutputQueue.Count);
			foreach (ItemTypeInfo item2 in m_OutputQueue)
			{
				writer.Serialize(item2);
			}
		}
		int v = ((m_WantedItems != null) ? m_WantedItems.Count : 0);
		writer.WritePackedInt32(v);
		if (m_WantedItems == null)
		{
			return;
		}
		foreach (ItemTypeInfo wantedItem in m_WantedItems)
		{
			writer.Serialize(wantedItem);
		}
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_InputsRemaining = null;
		m_OutputQueue = null;
		m_WantedInputItemWarningsFlags = 0;
		m_WantedItems = null;
		m_BlockPoolID = reader.ReadPackedUInt32();
		m_HasRecipe = reader.ReadBoolean();
		if (m_HasRecipe)
		{
			reader.Deserialize(out m_RecipeDef);
			m_RecipeRequester = reader.ReadByte();
			int num = reader.ReadPackedInt32();
			m_InputsRemaining = new List<ItemTypeInfo>(num);
			for (int i = 0; i < num; i++)
			{
				reader.Deserialize(out ItemTypeInfo itemInfo);
				m_InputsRemaining.Add(itemInfo);
			}
			m_WantedInputItemWarningsFlags = reader.ReadPackedInt32();
			num = reader.ReadPackedInt32();
			m_OutputQueue = new Stack<ItemTypeInfo>(num);
			for (int j = 0; j < num; j++)
			{
				reader.Deserialize(out ItemTypeInfo itemInfo2);
				m_OutputQueue.Push(itemInfo2);
			}
		}
		int num2 = reader.ReadPackedInt32();
		if (num2 > 0)
		{
			m_WantedItems = new List<ItemTypeInfo>(num2);
			for (int k = 0; k < num2; k++)
			{
				reader.Deserialize(out ItemTypeInfo itemInfo3);
				m_WantedItems.Add(itemInfo3);
			}
		}
	}
}
