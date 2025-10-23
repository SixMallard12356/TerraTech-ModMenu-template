using UnityEngine;
using UnityEngine.Networking;

public class FlyTechAwayRequest : MessageBase
{
	public NetworkInstanceId m_TargetTechId;

	public bool m_UseParticles;

	public float m_TargetHeightWorld;

	public float m_MaxLifetime;

	public Vector3 m_ExpectedPositionOfSmoke;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_TargetTechId);
		writer.Write(m_UseParticles);
		writer.Write(m_TargetHeightWorld);
		writer.Write(m_MaxLifetime);
		writer.Write(m_ExpectedPositionOfSmoke);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TargetTechId = reader.ReadNetworkId();
		m_UseParticles = reader.ReadBoolean();
		m_TargetHeightWorld = reader.ReadSingle();
		m_MaxLifetime = reader.ReadSingle();
		m_ExpectedPositionOfSmoke = reader.ReadVector3();
	}
}
