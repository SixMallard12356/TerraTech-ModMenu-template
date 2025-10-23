#define UNITY_EDITOR
using UnityEngine.Networking;

public class SetTutorialTechMessage : MessageBase
{
	public NetworkInstanceId m_TechNetId;

	public TechData m_TechData;

	public bool HasData => m_TechNetId != NetworkInstanceId.Invalid;

	public override void Serialize(NetworkWriter writer)
	{
		d.Assert(m_TechNetId == NetworkInstanceId.Invalid || m_TechData != null, "Invalid data config! Either needs to be all empty, or both set and valid");
		writer.Write(m_TechNetId);
		if (HasData)
		{
			m_TechData.NetSerialize(writer);
		}
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TechNetId = reader.ReadNetworkId();
		if (HasData)
		{
			m_TechData = new TechData();
			m_TechData.NetDeserialize(reader);
		}
		else
		{
			m_TechData = null;
		}
	}
}
