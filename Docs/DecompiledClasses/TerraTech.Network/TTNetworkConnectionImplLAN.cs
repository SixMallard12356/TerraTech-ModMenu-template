#define UNITY_EDITOR
using UnityEngine.Networking;

namespace TerraTech.Network;

public class TTNetworkConnectionImplLAN : TTNetworkConnectionImpl
{
	public TTNetworkConnectionImplLAN(TTNetworkID remoteNetworkID, HostTopology hostTopology)
		: base(remoteNetworkID, hostTopology)
	{
	}

	public TTNetworkConnectionImplLAN()
	{
	}

	public override bool HandleTransportSend(byte[] bytes, int numBytes, int channelId)
	{
		if (!base.IsInitialised)
		{
			d.LogError("[TTNetworkConnectionImplLAN] HandleTransportSend - Error not initialised - aborting");
			return false;
		}
		byte error;
		bool flag = NetworkTransport.Send(base.HostID, base.ConnectionID, channelId, bytes, numBytes, out error);
		if (!flag)
		{
			NetworkError networkError = (NetworkError)error;
			d.LogError("[TTNetworkConnectionImplLAN] unable to send data Error=" + networkError.ToString() + " ChannelID=" + channelId + " NumBytes=" + numBytes + " HostID=" + base.HostID + " RemoteHostID=" + base.RemoteNetworkID.m_NetworkID);
		}
		return flag;
	}

	public override void HandleConnectionDisposed()
	{
	}
}
