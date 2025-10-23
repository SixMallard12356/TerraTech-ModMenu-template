#define UNITY_EDITOR
using Newtonsoft.Json;
using UnityEngine;

public class EncounterObject
{
	public string m_PrefabName;

	public string m_UniqueName;

	public V3Serial m_Position;

	public WorldPosition m_WorldPosition;

	public QuatSerial m_Rotation;

	public bool m_Active;

	[JsonIgnore]
	public Transform m_Trans;

	[JsonConstructor]
	public EncounterObject()
	{
	}

	public EncounterObject(Transform trans, string uniqueName)
	{
		m_PrefabName = trans.name;
		m_UniqueName = uniqueName;
		m_WorldPosition = WorldPosition.FromScenePosition(trans.position);
		m_Rotation = trans.rotation;
		m_Active = trans.gameObject.activeInHierarchy;
		m_Trans = trans;
	}

	public void UpdateObjectData()
	{
		if ((bool)m_Trans)
		{
			m_Position = Vector3.zero;
			m_WorldPosition = WorldPosition.FromScenePosition(m_Trans.position);
			m_Rotation = m_Trans.rotation;
			m_Active = m_Trans.gameObject.activeInHierarchy;
			return;
		}
		string text = (m_UniqueName.NullOrEmpty() ? "UnknownName" : m_UniqueName);
		string text2 = (m_PrefabName.NullOrEmpty() ? "UnknownPrefabName" : m_PrefabName);
		d.LogError("EncounterObject.UpdateObjectData: " + text + " from prefab " + text2 + " has no valid transform");
	}

	public bool Equals(EncounterObject other)
	{
		if ((object)other == null)
		{
			return false;
		}
		if ((object)this == other)
		{
			return true;
		}
		if (string.Equals(m_PrefabName, other.m_PrefabName) && string.Equals(m_UniqueName, other.m_UniqueName) && m_WorldPosition.Equals(other.m_WorldPosition))
		{
			return m_Active.Equals(other.m_Active);
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (this == obj)
		{
			return true;
		}
		if (obj.GetType() != GetType())
		{
			return false;
		}
		return Equals((EncounterObject)obj);
	}

	public override int GetHashCode()
	{
		return (((((((m_PrefabName != null) ? m_PrefabName.GetHashCode() : 0) * 397) ^ ((m_UniqueName != null) ? m_UniqueName.GetHashCode() : 0)) * 397) ^ m_WorldPosition.GetHashCode()) * 397) ^ m_Active.GetHashCode();
	}

	public static bool operator ==(EncounterObject left, EncounterObject right)
	{
		return object.Equals(left, right);
	}

	public static bool operator !=(EncounterObject left, EncounterObject right)
	{
		return !object.Equals(left, right);
	}

	public static implicit operator bool(EncounterObject mo)
	{
		return mo != null;
	}
}
