using System.Runtime.InteropServices;
using UnityEngine.Networking;

namespace Unity;

[StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
public class GeneratedNetworkCode
{
	public static void _WriteIntVector3_None(NetworkWriter writer, IntVector3 value)
	{
		writer.WritePackedUInt32((uint)value.x);
		writer.WritePackedUInt32((uint)value.y);
		writer.WritePackedUInt32((uint)value.z);
	}

	public static IntVector3 _ReadIntVector3_None(NetworkReader reader)
	{
		return new IntVector3
		{
			x = (int)reader.ReadPackedUInt32(),
			y = (int)reader.ReadPackedUInt32(),
			z = (int)reader.ReadPackedUInt32()
		};
	}

	public static void _WriteIntVector2_None(NetworkWriter writer, IntVector2 value)
	{
		writer.WritePackedUInt32((uint)value.x);
		writer.WritePackedUInt32((uint)value.y);
	}

	public static IntVector2 _ReadIntVector2_None(NetworkReader reader)
	{
		return new IntVector2
		{
			x = (int)reader.ReadPackedUInt32(),
			y = (int)reader.ReadPackedUInt32()
		};
	}

	public static void _WriteArrayString_None(NetworkWriter writer, string[] value)
	{
		if (value == null)
		{
			writer.Write((ushort)0);
			return;
		}
		ushort value2 = (ushort)value.Length;
		writer.Write(value2);
		for (ushort num = 0; num < value.Length; num++)
		{
			writer.Write(value[num]);
		}
	}

	public static string[] _ReadArrayString_None(NetworkReader reader)
	{
		int num = reader.ReadUInt16();
		if (num == 0)
		{
			return new string[0];
		}
		string[] array = new string[num];
		for (int i = 0; i < num; i++)
		{
			ref string reference = ref array[i];
			reference = reader.ReadString();
		}
		return array;
	}
}
