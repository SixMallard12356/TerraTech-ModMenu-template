using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public static class WorldPositionExtensions
{
	public static void Write(this BinaryWriter stream, WorldPosition worldPos)
	{
		stream.WritePackedInt32(worldPos.TileCoord.x);
		stream.WritePackedInt32(worldPos.TileCoord.y);
		stream.Write(worldPos.TileRelativePos.x);
		stream.Write(worldPos.TileRelativePos.y);
		stream.Write(worldPos.TileRelativePos.z);
	}

	public static WorldPosition ReadWorldPosition(this BinaryReader stream)
	{
		IntVector2 tileCoord = default(IntVector2);
		tileCoord.x = stream.ReadPackedInt32();
		tileCoord.y = stream.ReadPackedInt32();
		Vector3 posInTile = default(Vector3);
		posInTile.x = stream.ReadSingle();
		posInTile.y = stream.ReadSingle();
		posInTile.z = stream.ReadSingle();
		return new WorldPosition(in tileCoord, in posInTile);
	}

	public static void WritePackedUInt32(this BinaryWriter stream, uint value)
	{
		if (value <= 240)
		{
			stream.Write((byte)value);
		}
		else if (value <= 2287)
		{
			stream.Write((byte)((value - 240) / 256 + 241));
			stream.Write((byte)((value - 240) % 256));
		}
		else if (value <= 67823)
		{
			stream.Write((byte)249);
			stream.Write((byte)((value - 2288) / 256));
			stream.Write((byte)((value - 2288) % 256));
		}
		else if (value <= 16777215)
		{
			stream.Write((byte)250);
			stream.Write((byte)(value & 0xFF));
			stream.Write((byte)((value >> 8) & 0xFF));
			stream.Write((byte)((value >> 16) & 0xFF));
		}
		else
		{
			stream.Write((byte)251);
			stream.Write((byte)(value & 0xFF));
			stream.Write((byte)((value >> 8) & 0xFF));
			stream.Write((byte)((value >> 16) & 0xFF));
			stream.Write((byte)((value >> 24) & 0xFF));
		}
	}

	public static uint ReadPackedUInt32(this BinaryReader stream)
	{
		byte b = stream.ReadByte();
		if (b < 241)
		{
			return b;
		}
		byte b2 = stream.ReadByte();
		if (b >= 241 && b <= 248)
		{
			return (uint)(240 + 256 * (b - 241) + b2);
		}
		byte b3 = stream.ReadByte();
		if (b == 249)
		{
			return (uint)(2288 + 256 * b2 + b3);
		}
		byte b4 = stream.ReadByte();
		if (b == 250)
		{
			return (uint)(b2 + (b3 << 8) + (b4 << 16));
		}
		byte b5 = stream.ReadByte();
		if (b >= 251)
		{
			return (uint)(b2 + (b3 << 8) + (b4 << 16) + (b5 << 24));
		}
		throw new IndexOutOfRangeException("ReadPackedUInt32() failure: " + b);
	}

	public static void WritePackedInt32(this BinaryWriter stream, int v)
	{
		if (v < 0)
		{
			stream.WritePackedUInt32((uint)((1 - v << 1) | 1));
		}
		else
		{
			stream.WritePackedUInt32((uint)(v << 1));
		}
	}

	public static int ReadPackedInt32(this BinaryReader stream)
	{
		uint num = stream.ReadPackedUInt32();
		if ((num & 1) == 0)
		{
			return (int)(num >> 1);
		}
		return (int)(1 - (num >> 1));
	}

	public static void WritePackedInt32(this NetworkWriter stream, int v)
	{
		if (v < 0)
		{
			stream.WritePackedUInt32((uint)((1 - v << 1) | 1));
		}
		else
		{
			stream.WritePackedUInt32((uint)(v << 1));
		}
	}

	public static int ReadPackedInt32(this NetworkReader stream)
	{
		uint num = stream.ReadPackedUInt32();
		if ((num & 1) == 0)
		{
			return (int)(num >> 1);
		}
		return (int)(1 - (num >> 1));
	}

	public static void Write(this NetworkWriter stream, WorldPosition worldPos)
	{
		stream.WritePackedInt32(worldPos.TileCoord.x);
		stream.WritePackedInt32(worldPos.TileCoord.y);
		stream.Write(worldPos.TileRelativePos);
	}

	public static WorldPosition ReadWorldPosition(this NetworkReader stream)
	{
		IntVector2 tileCoord = default(IntVector2);
		tileCoord.x = stream.ReadPackedInt32();
		tileCoord.y = stream.ReadPackedInt32();
		return new WorldPosition(in tileCoord, stream.ReadVector3());
	}
}
