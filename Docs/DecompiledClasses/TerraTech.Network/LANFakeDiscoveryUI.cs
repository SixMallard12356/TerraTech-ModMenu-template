using System.Collections.Generic;
using UnityEngine;

namespace TerraTech.Network;

public class LANFakeDiscoveryUI : MonoBehaviour
{
	private struct LANEntry
	{
		public string m_Address;

		public string m_Port;

		public MultiplayerModeType m_GameType;
	}

	private List<LANEntry> m_Entries = new List<LANEntry>();

	private string m_PortToAdd = "7778";

	private string m_AddressToAdd = "localhost";

	public void PopulateLobbies(Dictionary<TTNetworkID, PlatformLobbySystem_LAN.PlatformLobbyData> platformLobbies, LobbySystem.LobbyFilterOptions filterOptions)
	{
		for (int i = 0; i < m_Entries.Count; i++)
		{
			int hashCode = m_Entries[i].GetHashCode();
			TTNetworkID key = new TTNetworkID((ulong)hashCode + 1uL);
			if (!platformLobbies.TryGetValue(key, out var value))
			{
				PlatformLobbySystem_LAN.PlatformLobbyData obj = new PlatformLobbySystem_LAN.PlatformLobbyData
				{
					m_HostId = new TTNetworkID((ulong)hashCode + 2uL),
					m_Players = new List<TTNetworkID>()
				};
				value = (platformLobbies[key] = obj);
			}
			LobbyData lobbyData = new LobbyData();
			lobbyData.SetDefaultChoices();
			lobbyData.m_Choices[0] = (int)m_Entries[i].m_GameType;
			value.m_Data["name"] = m_Entries[i].m_Address + " " + m_Entries[i].m_GameType;
			value.m_Data["choices"] = Lobby.ChoicesToString(lobbyData.m_Choices);
			value.m_Data["lobbyPublic"] = Lobby.LobbyVisibility.Public.ToString();
			value.m_Data["protocolVersion"] = LobbySystem.PROTOCOL_VERSION.ToString();
			value.m_Data["language"] = "0";
			value.m_Data["ownerID"] = value.m_HostId.ToString();
			value.m_Data["gameInProgress"] = "yes";
			value.m_Data["gameModeIndex"] = ((int)m_Entries[i].m_GameType).ToString();
			value.m_Data["networkAddress"] = m_Entries[i].m_Address;
			value.m_Data["networkPort"] = m_Entries[i].m_Port;
			value.m_Data["lobbyID"] = key.ToString();
			value.m_Data["hostID"] = value.m_HostId.ToString();
		}
		foreach (KeyValuePair<TTNetworkID, PlatformLobbySystem_LAN.PlatformLobbyData> platformLobby in platformLobbies)
		{
			if (platformLobby.Value.m_Data.ContainsKey("gameModeIndex"))
			{
				MultiplayerModeType gameType = (MultiplayerModeType)int.Parse(platformLobby.Value.m_Data["gameModeIndex"]);
				if (!HasEntry(platformLobby.Value.m_Data["networkAddress"], platformLobby.Value.m_Data["networkPort"], gameType))
				{
					platformLobbies.Remove(platformLobby.Key);
					break;
				}
			}
		}
	}

	public void OnGUI()
	{
		GUILayout.BeginArea(new Rect(200f, 0f, 300f, 500f));
		GUILayout.BeginVertical();
		GUILayout.BeginVertical("Box");
		GUILayout.BeginHorizontal();
		GUILayout.Label("Address:", GUILayout.Width(100f));
		m_AddressToAdd = GUILayout.TextField(m_AddressToAdd);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Port:", GUILayout.Width(100f));
		m_PortToAdd = GUILayout.TextField(m_PortToAdd);
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
		GUI.enabled = !HasEntry(m_AddressToAdd, m_PortToAdd);
		if (GUILayout.Button("Add Deathmatch"))
		{
			AddLocalGame(MultiplayerModeType.Deathmatch);
		}
		if (GUILayout.Button("Add Creative Coop"))
		{
			AddLocalGame(MultiplayerModeType.CoOpCreative);
		}
		if (GUILayout.Button("Add Campaign Coop"))
		{
			AddLocalGame(MultiplayerModeType.CoOpCampaign);
		}
		GUI.enabled = true;
		for (int i = 0; i < m_Entries.Count; i++)
		{
			GUILayout.BeginHorizontal("Box");
			GUILayout.Label(m_Entries[i].m_Address + ":" + m_Entries[i].m_Port, GUILayout.Width(150f));
			if (GUILayout.Button("Remove"))
			{
				m_Entries.RemoveAt(i);
				i--;
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	private bool HasEntry(string address, string port)
	{
		for (int i = 0; i < m_Entries.Count; i++)
		{
			if (m_Entries[i].m_Address == address && m_Entries[i].m_Port == port)
			{
				return true;
			}
		}
		return false;
	}

	private bool HasEntry(string address, string port, MultiplayerModeType gameType)
	{
		for (int i = 0; i < m_Entries.Count; i++)
		{
			if (m_Entries[i].m_Address == address && m_Entries[i].m_Port == port && m_Entries[i].m_GameType == gameType)
			{
				return true;
			}
		}
		return false;
	}

	private void AddLocalGame(MultiplayerModeType gameType)
	{
		LANEntry item = new LANEntry
		{
			m_Address = m_AddressToAdd,
			m_Port = m_PortToAdd,
			m_GameType = gameType
		};
		m_Entries.Add(item);
	}
}
