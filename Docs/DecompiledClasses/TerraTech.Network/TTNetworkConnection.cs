#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

namespace TerraTech.Network;

public class TTNetworkConnection : NetworkConnection
{
	public struct NetworkStats
	{
		public ulong nReliableBytesSent;

		public ulong nReliablePacketsSent;

		public ulong nUnreliableBytesSent;

		public ulong nUnreliablePacketsSent;

		public ulong nReliableBytesReceived;

		public ulong nReliablePacketsReceived;

		public ulong nUnreliableBytesReceived;

		public ulong nUnreliablePacketsReceived;

		public void accummulate(NetworkStats rhs)
		{
			nReliableBytesSent += rhs.nReliableBytesSent;
			nReliablePacketsSent += rhs.nReliablePacketsSent;
			nUnreliableBytesSent += rhs.nUnreliableBytesSent;
			nUnreliablePacketsSent += rhs.nUnreliablePacketsSent;
			nReliableBytesReceived += rhs.nReliableBytesReceived;
			nReliablePacketsReceived += rhs.nReliablePacketsReceived;
			nUnreliableBytesReceived += rhs.nUnreliableBytesReceived;
			nUnreliablePacketsReceived += rhs.nUnreliablePacketsReceived;
		}
	}

	private TTNetworkConnectionImpl m_pImpl;

	private NetworkStats m_networkStats;

	public Event<TTNetworkConnection> OnDisposed;

	public string NetworkAddress { get; set; }

	public int NetworkPort { get; set; }

	public TTNetworkID RemoteClientID => m_pImpl.RemoteNetworkID;

	public HostTopology HostTopology => m_pImpl.HostTopology;

	public TTNetworkConnection(HostTopology hostTopology, TTNetworkID remoteClientId)
	{
		d.Log(string.Concat("[TTNetworkConnection] Created.  RemoteClientID=", remoteClientId, " HostToplogyNumChannels=", hostTopology.DefaultConfig.ChannelCount, " HostTopologyNumSpecialConfigs=", hostTopology.SpecialConnectionConfigsCount));
		if (SKU.IsLAN_MP)
		{
			m_pImpl = new TTNetworkConnectionImplLAN(remoteClientId, hostTopology);
		}
		else if (SKU.UsesEOS)
		{
			m_pImpl = new TTNetworkConnectionImplEOS(remoteClientId, hostTopology);
		}
		else if (SKU.IsSteam)
		{
			m_pImpl = new TTNetworkConnectionImplSteam(remoteClientId, hostTopology);
		}
		else if (SKU.IsNetEase)
		{
			m_pImpl = new TTNetworkConnectionImplNetEase(remoteClientId, hostTopology);
		}
		else
		{
			d.LogError("Unsupported platform for TTNetworkConnection!");
		}
		d.Assert(m_pImpl != null);
	}

	public TTNetworkConnection()
	{
		d.Log("Using parameterless constructor of TTNetworkConnection- why?");
		if (SKU.IsLAN_MP)
		{
			m_pImpl = new TTNetworkConnectionImplLAN();
		}
		else if (SKU.UsesEOS)
		{
			m_pImpl = new TTNetworkConnectionImplEOS();
		}
		else if (SKU.IsSteam)
		{
			m_pImpl = new TTNetworkConnectionImplSteam();
		}
		else if (SKU.IsNetEase)
		{
			m_pImpl = new TTNetworkConnectionImplNetEase();
		}
		else
		{
			d.LogError("Unsupported platform for TTNetworkConnection!");
		}
		d.Assert(m_pImpl != null);
	}

	public void ChangeRemoteNetworkID(TTNetworkID remoteClientID)
	{
		m_pImpl.ChangeRemoteNetworkID(remoteClientID);
	}

	public override void Initialize(string address, int hostId, int connectionId, HostTopology hostTopology)
	{
		d.Assert(m_pImpl != null);
		d.Log(string.Concat("[TTNetworkConnection] Initialised.  ConnectedID=", connectionId, " RemoteClientID=", RemoteClientID, " HostID=", hostId, " HostTopologyNumChannels=", hostTopology.DefaultConfig.ChannelCount, " HostTopologyNumSpecialConfigs=", hostTopology.SpecialConnectionConfigsCount));
		m_pImpl.Initialise(hostId, connectionId);
		m_pImpl.ChangeTopology(hostTopology);
		base.Initialize(address, hostId, connectionId, hostTopology);
	}

	public NetworkStats GetNetworkStats()
	{
		d.Assert(connectionId == m_pImpl.ConnectionID, "ASSERT: ConnectionID Mismatch! Base=" + connectionId + " Impl=" + m_pImpl.ConnectionID + " RemoteClientID=" + RemoteClientID);
		return m_networkStats;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			d.Log("[TTNetworkConnection] Disposing NetworkConnectionID=" + connectionId + " Disposing=" + disposing.ToString() + " RemoteClientID=" + RemoteClientID);
			d.Log("[TTNetworkConnection] Send MsgType.Disconnect");
			SetMaxDelay(0f);
			Send(33, new UnityEngine.Networking.NetworkSystem.EmptyMessage());
			Flush();
			m_pImpl.HandleConnectionDisposed();
			OnDisposed.Send(this);
			m_pImpl = null;
		}
		base.Dispose(disposing);
	}

	public void Flush()
	{
		FlushChannels();
	}

	public override bool TransportSend(byte[] bytes, int numBytes, int channelId, out byte error)
	{
		if (m_pImpl.HandleTransportSend(bytes, numBytes, channelId))
		{
			if (m_pImpl.IsChannelReliable(channelId))
			{
				m_networkStats.nReliablePacketsSent++;
				m_networkStats.nReliableBytesSent += (ulong)numBytes;
			}
			else
			{
				m_networkStats.nUnreliablePacketsSent++;
				m_networkStats.nUnreliableBytesSent += (ulong)numBytes;
			}
			error = 0;
			return true;
		}
		error = 1;
		return false;
	}

	public override string ToString()
	{
		return string.Format("[TTNetCon | HostID={0}, ConnID={1}, RemoteID={2}, {3}]", m_pImpl.HostID, m_pImpl.ConnectionID, m_pImpl.RemoteNetworkID, isReady ? "READY" : "NOT READY");
	}

	public override void TransportReceive(byte[] bytes, int numBytes, int channelId)
	{
		bool num = m_pImpl.IsChannelReliable(channelId);
		if (m_pImpl.HandleTransportReceive(bytes, numBytes, channelId))
		{
			DoTransportReceive(bytes, numBytes, channelId);
		}
		if (num)
		{
			m_networkStats.nReliablePacketsReceived++;
			m_networkStats.nReliableBytesReceived += (ulong)numBytes;
		}
		else
		{
			m_networkStats.nUnreliablePacketsReceived++;
			m_networkStats.nUnreliableBytesReceived += (ulong)numBytes;
		}
	}

	private void DoTransportReceive(byte[] bytes, int numBytes, int channelId)
	{
		try
		{
			HandleBytes(bytes, numBytes, channelId);
		}
		catch (Exception ex)
		{
			string text = ex.Message;
			if (string.IsNullOrEmpty(text))
			{
				text = ex.GetType().Name;
			}
			d.LogErrorFormat("TransportReceive: Exception Occured Message = '{0}' BufferLength={1} ChannelID={2}\nStack Trace:\n{3}", text, numBytes, channelId, ex.StackTrace);
			NetworkReader networkReader = new NetworkReader(bytes);
			int num = numBytes;
			if (num < 2)
			{
				d.LogError("TransportReceive - Unable to read length of message (less than 2 bytes)");
				return;
			}
			ushort num2 = networkReader.ReadUInt16();
			num -= 2;
			if (num < 2)
			{
				d.LogError("TransportReceive - Unable to read type of message");
				return;
			}
			short num3 = networkReader.ReadInt16();
			num -= 2;
			uint value = networkReader.ReadPackedUInt32();
			NetworkInstanceId networkInstanceId = new NetworkInstanceId(value);
			GameObject gameObject = ClientScene.FindLocalObject(networkInstanceId);
			d.LogErrorFormat("TransportReceive - Buffer error for NetId={0} MsgLength={1} MsgType={2} ObjectName={3} Active={4}", networkInstanceId, num2, num3, gameObject ? gameObject.name : "NULL", (!gameObject) ? "N/A" : (gameObject.activeInHierarchy ? "YES" : "NO"));
		}
	}
}
