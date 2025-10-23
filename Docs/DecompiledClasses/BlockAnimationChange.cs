using UnityEngine;
using UnityEngine.Networking;

public class BlockAnimationChange : MessageBase
{
	public uint m_BlockPoolID;

	public AnimatorControllerParameterType m_ParameterType;

	public string m_ParameterName;

	public byte[] m_ParameterData = new byte[4];

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockPoolID);
		writer.Write((byte)m_ParameterType);
		writer.Write(m_ParameterName);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(m_ParameterData[i]);
		}
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadPackedUInt32();
		m_ParameterType = (AnimatorControllerParameterType)reader.ReadByte();
		m_ParameterName = reader.ReadString();
		for (int i = 0; i < 4; i++)
		{
			m_ParameterData[i] = reader.ReadByte();
		}
	}
}
