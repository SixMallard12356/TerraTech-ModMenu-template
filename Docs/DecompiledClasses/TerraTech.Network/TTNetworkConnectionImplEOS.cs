#define UNITY_EDITOR
using System;
using Epic.OnlineServices;
using Epic.OnlineServices.P2P;
using PlayEveryWare.EpicOnlineServices;
using UnityEngine.Networking;

namespace TerraTech.Network;

public class TTNetworkConnectionImplEOS : TTNetworkConnectionImpl
{
	private ProductUserId m_LocalUserId;

	private ProductUserId m_RemoteUserId;

	private string m_SocketName;

	public TTNetworkConnectionImplEOS(TTNetworkID remoteNetworkID, HostTopology hostTopology)
		: base(remoteNetworkID, hostTopology)
	{
		m_LocalUserId = EOSManager.Instance.GetProductUserId();
		m_RemoteUserId = remoteNetworkID.ToEOSProductUserID();
		UpdateSocketName();
	}

	public TTNetworkConnectionImplEOS()
	{
	}

	public override bool HandleTransportSend(byte[] bytes, int numBytes, int channelId)
	{
		if (!base.IsInitialised)
		{
			d.LogError("[TTNetworkConnectionImplEOS] HandleTransportSend - Error not initialised - aborting");
			return false;
		}
		d.Assert(numBytes < 1170, $"EOSP2PTransport.Send: Unable to send payload - The payload size ({numBytes} bytes) exceeds the maxmimum packet size supported by EOS P2P ({1170} bytes).");
		PacketReliability p2PSendMethod = GetP2PSendMethod(channelId);
		SocketId value = new SocketId
		{
			SocketName = m_SocketName
		};
		SendPacketOptions options = new SendPacketOptions
		{
			LocalUserId = m_LocalUserId,
			RemoteUserId = m_RemoteUserId,
			SocketId = value,
			AllowDelayedDelivery = true,
			Channel = (byte)channelId,
			Reliability = p2PSendMethod,
			Data = new ArraySegment<byte>(bytes, 0, numBytes)
		};
		Result result = EOSManager.Instance.GetEOSP2PInterface().SendPacket(ref options);
		if (result != Result.Success)
		{
			d.LogError($"EOSTransportManager.SendPacket: Unable to send {options.Data.Count} byte packet to RemoteUserId '{options.RemoteUserId}' - Error result, {result}.");
		}
		return result == Result.Success;
	}

	public override void HandleConnectionDisposed()
	{
		m_LocalUserId = null;
		m_RemoteUserId = null;
		m_SocketName = null;
	}

	public override void ChangeRemoteNetworkID(TTNetworkID remoteNetworkID)
	{
		base.ChangeRemoteNetworkID(remoteNetworkID);
		m_RemoteUserId = remoteNetworkID.ToEOSProductUserID();
		UpdateSocketName();
	}

	private void UpdateSocketName()
	{
		m_SocketName = "TTNetwCon_" + m_RemoteUserId.ToString();
		if (m_SocketName.Length > 32)
		{
			m_SocketName = m_SocketName.Substring(0, 32);
		}
	}

	private PacketReliability GetP2PSendMethod(int channelId)
	{
		switch (GetChannelQos(channelId))
		{
		case QosType.Unreliable:
		case QosType.UnreliableFragmented:
			return PacketReliability.UnreliableUnordered;
		case QosType.Reliable:
		case QosType.ReliableFragmented:
			return PacketReliability.ReliableUnordered;
		default:
			return PacketReliability.ReliableOrdered;
		}
	}
}
