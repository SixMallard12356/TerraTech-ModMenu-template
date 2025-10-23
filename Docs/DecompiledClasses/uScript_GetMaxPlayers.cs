using TerraTech.Network;
using UnityEngine;

public class uScript_GetMaxPlayers : uScriptLogic
{
	public bool Out => true;

	public int In()
	{
		Lobby lobby = Singleton.Manager<ManNetworkLobby>.inst?.LobbySystem.CurrentLobby;
		int num;
		if (lobby != null)
		{
			num = lobby.Data.m_MaxUserLimit;
			if (SKU.IsLAN_MP)
			{
				num = Mathf.Min(4, num);
			}
		}
		else
		{
			num = 1;
		}
		return num;
	}
}
