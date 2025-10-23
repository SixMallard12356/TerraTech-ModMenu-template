#define UNITY_EDITOR
using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class EncounterToSpawn
{
	[JsonIgnore]
	public EncounterData m_EncounterData;

	public EncounterIdentifier m_EncounterDef;

	public bool m_UsePosForPlacement;

	public WorldPosition m_Position;

	public QuatSerial m_Rotation = Quaternion.identity;

	public int m_EncounterStringBankIdx;

	public EncounterToSpawn()
	{
	}

	public EncounterToSpawn(EncounterData encounterData, EncounterIdentifier encounterDef)
	{
		m_EncounterData = encounterData;
		m_EncounterDef = encounterDef;
		m_EncounterStringBankIdx = m_EncounterData.EncounterDetails.GetRandomStringBankIdx();
	}

	public EncounterToSpawn(EncounterIdentifier encounterID)
	{
		m_EncounterDef = encounterID;
		m_EncounterData = Singleton.Manager<ManEncounter>.inst.GetEncounterData(encounterID);
		if (m_EncounterData != null)
		{
			m_EncounterStringBankIdx = m_EncounterData.EncounterDetails.GetRandomStringBankIdx();
		}
		else
		{
			d.LogError("Failed to find encounter data for ID " + encounterID);
		}
	}

	public override string ToString()
	{
		return $"{m_EncounterDef.ToString()} at {m_Position}";
	}

	public int GetRotationForSetPiece()
	{
		int num = Mathf.RoundToInt(((Quaternion)m_Rotation).eulerAngles.y / 90f) * 90;
		if (num < 0)
		{
			num += 360;
		}
		return num;
	}

	public bool OnSerialize(NetworkWriter writer, bool initialState)
	{
		writer.Write(m_EncounterDef);
		writer.Write(m_UsePosForPlacement);
		writer.Write(m_Position);
		writer.Write(m_Rotation);
		writer.Write((byte)m_EncounterStringBankIdx);
		return true;
	}

	public bool OnDeserialize(NetworkReader reader)
	{
		m_EncounterDef = reader.ReadEncounterID();
		m_UsePosForPlacement = reader.ReadBoolean();
		m_Position = reader.ReadWorldPosition();
		m_Rotation = reader.ReadQuaternion();
		m_EncounterStringBankIdx = reader.ReadByte();
		return true;
	}
}
