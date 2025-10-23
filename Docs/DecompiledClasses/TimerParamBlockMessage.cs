using UnityEngine.Networking;

public class TimerParamBlockMessage : MessageBase, IBlockMessage
{
	public uint m_BlockPoolID;

	public Timer.Data value;

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
		writer.Write(m_BlockPoolID);
		value.Serialize(writer);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadUInt32();
		value.Deserialize(reader);
	}
}
