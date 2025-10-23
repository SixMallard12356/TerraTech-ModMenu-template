using UnityEngine.Networking;

public static class BlockCountListExtensions
{
	public static void Write(this NetworkWriter writer, ref BlockCountList list)
	{
		list.WriteTo(writer);
	}

	public static void Read(this NetworkReader reader, ref BlockCountList list)
	{
		list.Clear();
		list.ReadFrom(reader);
	}
}
