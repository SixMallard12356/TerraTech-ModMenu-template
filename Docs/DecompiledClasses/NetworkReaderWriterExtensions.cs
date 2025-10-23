using UnityEngine.Networking;

public static class NetworkReaderWriterExtensions
{
	public static byte[] ReadBytesWithSize32(this NetworkReader reader)
	{
		uint num = reader.ReadPackedUInt32();
		if (num <= 65535)
		{
			return reader.ReadBytes((int)num);
		}
		byte[] array = new byte[num];
		for (uint num2 = 0u; num2 != num; num2++)
		{
			array[num2] = reader.ReadByte();
		}
		return array;
	}

	public static void WriteBytesWithSize32(this NetworkWriter writer, byte[] buffer)
	{
		writer.WriteBytesWithSize32(buffer, buffer.Length);
	}

	public static void WriteBytesWithSize32(this NetworkWriter writer, byte[] buffer, int length)
	{
		writer.WritePackedUInt32((uint)length);
		if (length <= 65535)
		{
			writer.Write(buffer, length);
			return;
		}
		writer.Write(buffer, 65535);
		for (uint num = 65535u; num != length; num++)
		{
			writer.Write(buffer[num]);
		}
	}

	public static void Write(this NetworkWriter writer, LocalisedString loc)
	{
		if (loc == null)
		{
			writer.Write("NULL");
			writer.Write("");
		}
		else
		{
			writer.Write(loc.m_Bank);
			writer.Write(loc.m_Id);
		}
	}

	public static LocalisedString ReadLocalisedString(this NetworkReader reader)
	{
		string text = reader.ReadString();
		string id = reader.ReadString();
		if (text == "NULL")
		{
			return null;
		}
		return new LocalisedString
		{
			m_Bank = text,
			m_Id = id
		};
	}
}
