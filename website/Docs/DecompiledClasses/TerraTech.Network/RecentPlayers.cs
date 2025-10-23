using System.Collections.Generic;

namespace TerraTech.Network;

public class RecentPlayers
{
	public struct RecentPlayer
	{
		public TTNetworkID ID;

		public string Name;
	}

	private List<RecentPlayer> m_RecentPlayers;

	private LobbySystem m_LobbySystem;

	private int m_MaxNumEntries;

	public IEnumerable<RecentPlayer> Iterator => m_RecentPlayers;

	public RecentPlayers(LobbySystem lobbySystem, int maxEntries = 16)
	{
		m_LobbySystem = lobbySystem;
		m_RecentPlayers = new List<RecentPlayer>(maxEntries);
		m_MaxNumEntries = maxEntries;
	}

	public void Add(TTNetworkID playerID)
	{
		m_LobbySystem.Platform_GetUserName(playerID, AddEntry);
	}

	private void AddEntry(TTNetworkID playerID, string playerName = null)
	{
		int num = m_RecentPlayers.FindIndex((RecentPlayer pi) => pi.ID == playerID);
		RecentPlayer recentPlayer = new RecentPlayer
		{
			ID = playerID,
			Name = playerName
		};
		if (num >= 0)
		{
			m_RecentPlayers[num] = recentPlayer;
			return;
		}
		if (m_RecentPlayers.Count + 1 >= m_MaxNumEntries)
		{
			m_RecentPlayers.RemoveAt(0);
		}
		m_RecentPlayers.Add(recentPlayer);
	}
}
