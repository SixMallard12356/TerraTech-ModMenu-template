using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

[Serializable]
public struct EncounterIdentifier : IEquatable<EncounterIdentifier>
{
	public static readonly EncounterIdentifier Invalid = new EncounterIdentifier(FactionSubTypes.NULL, 0, "?", "?");

	public const string kCoreEncounterCategoryName = "core";

	public const int kCoreEncounterCategoryIndex = 0;

	[JsonIgnore]
	private int m_HashID;

	[JsonIgnore]
	public bool IsCoreEncounter => m_Category.ToLower() == "core";

	[JsonProperty]
	public FactionSubTypes m_Corp { get; private set; }

	[JsonProperty]
	public int m_Grade { get; private set; }

	[JsonProperty]
	public string m_Category { get; private set; }

	[JsonProperty]
	public string m_Name { get; private set; }

	public EncounterIdentifier(FactionSubTypes corp, int grade, string category, string encounterName)
	{
		m_Corp = corp;
		m_Grade = grade;
		m_Category = category;
		m_Name = encounterName;
		m_HashID = 0;
		GenerateHash();
	}

	[OnDeserialized]
	private void OnDeserialized(StreamingContext context)
	{
		GenerateHash();
	}

	private void GenerateHash()
	{
		m_Category = m_Category.ToLower();
		m_HashID = (m_Corp.GetHashCode() << 7) ^ (m_Grade.GetHashCode() << 19) ^ (m_Category.GetHashCode() << 3) ^ m_Name.GetHashCode();
	}

	public static bool operator ==(EncounterIdentifier a, EncounterIdentifier b)
	{
		return a.Equals(b);
	}

	public static bool operator !=(EncounterIdentifier a, EncounterIdentifier b)
	{
		return !(a == b);
	}

	public bool Equals(EncounterIdentifier otherItem)
	{
		return m_HashID == otherItem.m_HashID;
	}

	public override int GetHashCode()
	{
		return m_HashID;
	}

	public override string ToString()
	{
		if (m_Corp == FactionSubTypes.NULL)
		{
			return $"EncounterID = {m_HashID}";
		}
		return $"{m_Corp} Grade {m_Grade}, {m_Category}: {m_Name}. EncounterID = {m_HashID}";
	}

	public override bool Equals(object obj)
	{
		if (obj is EncounterIdentifier otherItem)
		{
			return Equals(otherItem);
		}
		return false;
	}
}
