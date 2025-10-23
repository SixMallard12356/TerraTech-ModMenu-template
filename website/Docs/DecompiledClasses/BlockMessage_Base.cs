using UnityEngine.Networking;

public abstract class BlockMessage_Base : MessageBase, IBlockMessage
{
	public uint m_BlockPoolID;

	public static uint s_ModuleBlockPoolID = uint.MaxValue;

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

	public static bool s_IsSerialisingModuleData => s_ModuleBlockPoolID != uint.MaxValue;

	public override void Serialize(NetworkWriter writer)
	{
		if (!s_IsSerialisingModuleData)
		{
			writer.WritePackedUInt32(m_BlockPoolID);
		}
	}

	public override void Deserialize(NetworkReader reader)
	{
		if (!s_IsSerialisingModuleData)
		{
			m_BlockPoolID = reader.ReadPackedUInt32();
		}
		else
		{
			m_BlockPoolID = s_ModuleBlockPoolID;
		}
	}
}
