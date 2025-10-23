#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.Networking;

public class SpawnTechMessage : MessageBase
{
	public TechData m_TechData;

	public NetworkInstanceId m_PlayerNetID;

	public NetworkInstanceId m_PlayerWhoCalledSpawn;

	public int m_Team;

	public bool m_IsPopulation;

	public bool m_IsSpawnedByPlayer;

	public WorldPosition m_Position;

	public Quaternion m_Rotation;

	public bool m_SpawnTechWithUnavailableBlocksMissing;

	public bool m_CheatBypassInventory;

	public override void Deserialize(NetworkReader reader)
	{
		m_TechData = new TechData();
		m_TechData.NetDeserialize(reader);
		m_PlayerNetID = new NetworkInstanceId(reader.ReadUInt32());
		m_Team = reader.ReadInt32();
		m_IsPopulation = reader.ReadBoolean();
		m_Position = reader.ReadWorldPosition();
		m_Rotation = reader.ReadQuaternion();
		m_SpawnTechWithUnavailableBlocksMissing = reader.ReadBoolean();
		m_CheatBypassInventory = reader.ReadBoolean();
		m_PlayerWhoCalledSpawn = new NetworkInstanceId(reader.ReadUInt32());
		m_IsSpawnedByPlayer = reader.ReadBoolean();
	}

	public override void Serialize(NetworkWriter writer)
	{
		d.Assert(m_TechData != null, "SpawnTechMessage.Serialize has a null tech data");
		m_TechData.NetSerialize(writer);
		writer.Write(m_PlayerNetID.Value);
		writer.Write(m_Team);
		writer.Write(m_IsPopulation);
		writer.Write(m_Position);
		writer.Write(m_Rotation);
		writer.Write(m_SpawnTechWithUnavailableBlocksMissing);
		writer.Write(m_CheatBypassInventory);
		writer.Write(m_PlayerWhoCalledSpawn.Value);
		writer.Write(m_IsSpawnedByPlayer);
	}
}
