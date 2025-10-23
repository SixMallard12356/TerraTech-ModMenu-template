using UnityEngine.Networking;

public class ConfigureItemStackMessage : MessageBase, IBlockMessage
{
	public uint m_BlockPoolID;

	public int _StackIndex;

	public byte _MoveType;

	public bool _BeamEnabled;

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
		writer.WritePackedInt32(_StackIndex);
		writer.Write(_MoveType);
		writer.Write(_BeamEnabled);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadPackedUInt32();
		_StackIndex = reader.ReadPackedInt32();
		_MoveType = reader.ReadByte();
		_BeamEnabled = reader.ReadBoolean();
	}
}
