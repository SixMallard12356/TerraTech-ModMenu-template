using System;
using System.IO;

namespace TerraTech.Network;

public struct TTNetworkID : IEquatable<TTNetworkID>, IComparable<TTNetworkID>
{
	public string m_NetworkID;

	public static TTNetworkID Invalid = new TTNetworkID(null);

	public TTNetworkID(string nid)
	{
		m_NetworkID = nid;
	}

	public TTNetworkID(ulong nid)
	{
		m_NetworkID = nid.ToString();
	}

	public void Clear()
	{
		m_NetworkID = null;
	}

	public bool IsNull()
	{
		return !IsValid();
	}

	public bool IsValid()
	{
		return !m_NetworkID.NullOrEmpty();
	}

	public override string ToString()
	{
		if (IsValid())
		{
			return m_NetworkID;
		}
		return "invalid";
	}

	public override bool Equals(object other)
	{
		if (other is TTNetworkID)
		{
			return this == (TTNetworkID)other;
		}
		return false;
	}

	public override int GetHashCode()
	{
		if (m_NetworkID == null)
		{
			return 0;
		}
		return m_NetworkID.GetHashCode();
	}

	public static bool operator ==(TTNetworkID x, TTNetworkID y)
	{
		return x.m_NetworkID == y.m_NetworkID;
	}

	public static bool operator !=(TTNetworkID x, TTNetworkID y)
	{
		return !(x == y);
	}

	public static explicit operator TTNetworkID(string value)
	{
		return new TTNetworkID(value);
	}

	public static explicit operator TTNetworkID(ulong value)
	{
		return new TTNetworkID(value.ToString());
	}

	public static explicit operator string(TTNetworkID that)
	{
		return that.m_NetworkID;
	}

	public bool Equals(TTNetworkID other)
	{
		return this == other;
	}

	public int CompareTo(TTNetworkID other)
	{
		return m_NetworkID.CompareTo(other.m_NetworkID);
	}

	public void WriteTo(BinaryWriter writer)
	{
		writer.Write(m_NetworkID);
	}

	public static TTNetworkID ReadFrom(BinaryReader reader)
	{
		return new TTNetworkID(reader.ReadString());
	}
}
