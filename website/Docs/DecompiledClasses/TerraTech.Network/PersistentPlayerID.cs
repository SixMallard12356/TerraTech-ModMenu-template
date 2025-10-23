using System;

namespace TerraTech.Network;

public struct PersistentPlayerID : IEquatable<PersistentPlayerID>
{
	private readonly string m_Id;

	public PersistentPlayerID(string idStr)
	{
		m_Id = idStr;
	}

	public PersistentPlayerID(TTNetworkID playerId)
	{
		m_Id = playerId.m_NetworkID.ToString();
	}

	public override string ToString()
	{
		return m_Id.ToString();
	}

	public static explicit operator TTNetworkID(in PersistentPlayerID playerId)
	{
		return new TTNetworkID(playerId.m_Id);
	}

	public override bool Equals(object other)
	{
		if (other is PersistentPlayerID)
		{
			return this == (PersistentPlayerID)other;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return m_Id.GetHashCode();
	}

	public static bool operator ==(PersistentPlayerID x, PersistentPlayerID y)
	{
		return x.m_Id == y.m_Id;
	}

	public static bool operator !=(PersistentPlayerID x, PersistentPlayerID y)
	{
		return !(x == y);
	}

	public bool Equals(PersistentPlayerID other)
	{
		return m_Id == other.m_Id;
	}
}
