using UnityEngine;

namespace TerraTech.Network;

public struct LobbyPlayerData
{
	public TTNetworkID m_PlayerID;

	public string m_Name;

	public Sprite m_Sprite;

	public Color32 m_Colour;

	public int m_TeamID;
}
