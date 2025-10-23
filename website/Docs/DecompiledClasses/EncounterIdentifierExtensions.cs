using UnityEngine.Networking;

public static class EncounterIdentifierExtensions
{
	public static void Write(this NetworkWriter writer, EncounterIdentifier id)
	{
		writer.Write(id.GetHashCode());
	}

	public static EncounterIdentifier ReadEncounterID(this NetworkReader reader)
	{
		return Singleton.Manager<ManEncounter>.inst.GetEncounterIdentifier(reader.ReadInt32());
	}
}
