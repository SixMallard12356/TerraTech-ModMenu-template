#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.Networking;

public class TechDataMessage : MessageBase
{
	public int m_HostID;

	public ManVisible.ClientTechData m_TechInfo;

	public override void Deserialize(NetworkReader reader)
	{
		m_HostID = reader.ReadInt32();
		Quaternion rotation = reader.ReadQuaternion();
		TechData techData = new TechData();
		techData.NetDeserialize(reader);
		m_TechInfo = new ManVisible.ClientTechData
		{
			rotation = rotation,
			techData = techData
		};
	}

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_HostID);
		writer.Write(m_TechInfo.rotation);
		d.Assert(m_TechInfo.techData != null, "TechDataMessage.Serialize has a null tech data");
		m_TechInfo.techData.NetSerialize(writer);
	}
}
