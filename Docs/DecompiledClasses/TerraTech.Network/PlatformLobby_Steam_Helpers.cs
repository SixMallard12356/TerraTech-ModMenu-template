#define UNITY_EDITOR
using Steamworks;

namespace TerraTech.Network;

public static class PlatformLobby_Steam_Helpers
{
	public static CSteamID ToSteamID(this TTNetworkID TTnetworkID)
	{
		if (ulong.TryParse(TTnetworkID.m_NetworkID, out var result))
		{
			return new CSteamID(result);
		}
		d.LogErrorFormat("Invalid parse of TTNetworkID to CSteamID {0}", TTnetworkID);
		return CSteamID.Nil;
	}

	public static TTNetworkID ToTTID(this CSteamID steamID)
	{
		return new TTNetworkID(steamID.m_SteamID);
	}
}
