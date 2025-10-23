using System;
using System.Collections;
using System.Collections.Generic;

namespace TerraTech.Network;

public class BannedPlayers : IEnumerable<PersistentPlayerID>, IEnumerable
{
	private readonly HashSet<PersistentPlayerID> m_BannedPlayers = new HashSet<PersistentPlayerID>();

	private const string kSeparator = ",";

	private static readonly string[] kSeparatorArr = new string[1] { "," };

	public bool Contains(PersistentPlayerID playerID)
	{
		return m_BannedPlayers.Contains(playerID);
	}

	internal bool Set(PersistentPlayerID playerID, bool banned)
	{
		if (banned)
		{
			return m_BannedPlayers.Add(playerID);
		}
		return m_BannedPlayers.Remove(playerID);
	}

	internal void Reset()
	{
		m_BannedPlayers.Clear();
	}

	public IEnumerator<PersistentPlayerID> GetEnumerator()
	{
		return m_BannedPlayers.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return m_BannedPlayers.GetEnumerator();
	}

	public void TryParse(string bannedPlayersStr)
	{
		m_BannedPlayers.Clear();
		if (!bannedPlayersStr.NullOrEmpty())
		{
			string[] array = bannedPlayersStr.Split(kSeparatorArr, StringSplitOptions.RemoveEmptyEntries);
			foreach (string idStr in array)
			{
				Set(new PersistentPlayerID(idStr), banned: true);
			}
		}
	}

	public override string ToString()
	{
		if (m_BannedPlayers.Count <= 0)
		{
			return string.Empty;
		}
		return string.Join(",", m_BannedPlayers);
	}

	public static bool IsPlayerBanned(string bannedPlayersStr, PersistentPlayerID playerID)
	{
		if (!bannedPlayersStr.NullOrEmpty())
		{
			string[] array = bannedPlayersStr.Split(kSeparatorArr, StringSplitOptions.RemoveEmptyEntries);
			foreach (string idStr in array)
			{
				PersistentPlayerID persistentPlayerID = new PersistentPlayerID(idStr);
				if (playerID == persistentPlayerID)
				{
					return true;
				}
			}
		}
		return false;
	}
}
