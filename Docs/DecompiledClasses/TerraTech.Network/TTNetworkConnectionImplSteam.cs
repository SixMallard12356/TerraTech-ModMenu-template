#define UNITY_EDITOR
using Steamworks;
using UnityEngine.Networking;

namespace TerraTech.Network;

public class TTNetworkConnectionImplSteam : TTNetworkConnectionImpl
{
	private CSteamID m_RemoteSteamID;

	public TTNetworkConnectionImplSteam(TTNetworkID remoteNetworkID, HostTopology hostTopology)
		: base(remoteNetworkID, hostTopology)
	{
		m_RemoteSteamID = remoteNetworkID.ToSteamID();
	}

	public TTNetworkConnectionImplSteam()
	{
	}

	public override bool HandleTransportSend(byte[] bytes, int numBytes, int channelId)
	{
		EP2PSend eP2PSendType = _getEP2PSendType(channelId);
		if (SteamNetworking.SendP2PPacket(m_RemoteSteamID, bytes, (uint)numBytes, eP2PSendType, channelId))
		{
			return true;
		}
		string friendPersonaName = SteamFriends.GetFriendPersonaName(m_RemoteSteamID);
		string text = ((friendPersonaName != null) ? friendPersonaName : m_RemoteSteamID.m_SteamID.ToString());
		d.LogError("TTNetworkConnection.TransportSend: Failing to SendP2P Packet of " + numBytes + " bytes to SteamID=" + m_RemoteSteamID.ToString() + " Name=" + text);
		P2PSessionState_t pConnectionState = default(P2PSessionState_t);
		if (SteamNetworking.GetP2PSessionState(m_RemoteSteamID, out pConnectionState))
		{
			d.LogError("P2PSession with User:" + m_RemoteSteamID.ToString() + " Name:" + text + " is VALID.  ConnectionActive=" + pConnectionState.m_bConnectionActive + " Connecting=" + pConnectionState.m_bConnecting + " SessionError=" + (uint)pConnectionState.m_eP2PSessionError + " UsingRelay=" + pConnectionState.m_bUsingRelay + "  BytesQueuedForSend=" + pConnectionState.m_nBytesQueuedForSend + " PacketsQueuedForSend=" + pConnectionState.m_nPacketsQueuedForSend + " RemoteIP=" + pConnectionState.m_nRemoteIP.ToString("8X"));
		}
		else
		{
			d.LogError("P2PSession with User:" + m_RemoteSteamID.ToString() + " Name:" + text + " is INVALID!");
		}
		return false;
	}

	public override void HandleConnectionDisposed()
	{
		m_RemoteSteamID.Clear();
	}

	private EP2PSend _getEP2PSendType(int channelId)
	{
		EP2PSend result = EP2PSend.k_EP2PSendUnreliable;
		if (IsChannelReliable(channelId))
		{
			result = EP2PSend.k_EP2PSendReliable;
		}
		return result;
	}
}
