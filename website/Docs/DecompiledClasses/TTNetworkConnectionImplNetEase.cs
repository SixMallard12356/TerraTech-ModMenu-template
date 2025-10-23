#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Netease.Oddish.Ingame.Sdk;
using Netease.Oddish.Ingame.Sdk.Entity.Networking;
using Netease.Oddish.Ingame.Sdk.Task;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Networking;

public class TTNetworkConnectionImplNetEase : TTNetworkConnectionImpl
{
	private struct PendingPacketData
	{
		public byte[] Bytes;

		public int NumBytes;

		public int Channel;

		public P2pPacketSendTypeEnum packetSendType;

		public string remoteId;
	}

	private string m_RemoteId;

	private TTNetworkID m_RemoteTTId;

	private static Queue<PendingPacketData> s_PendingPacketQueue = new Queue<PendingPacketData>();

	private static volatile bool s_SendingPacket;

	private byte[] m_LastReceived;

	public TTNetworkConnectionImplNetEase(TTNetworkID remoteNetworkId, HostTopology topology)
		: base(remoteNetworkId, topology)
	{
		Debug.Log("[TTNetworkConnectionImplNetEase] Creating Network Implementation for ID=" + remoteNetworkId);
		m_RemoteId = remoteNetworkId.ToString();
		m_RemoteTTId = remoteNetworkId;
	}

	public TTNetworkConnectionImplNetEase()
	{
	}

	public static void DispatchPacketQueued(byte[] bytes, int numBytes, int channelId, P2pPacketSendTypeEnum packetSendType, string remoteId)
	{
		byte[] array = new byte[numBytes];
		Array.Copy(bytes, 0, array, 0, numBytes);
		FinalSendPacket(array, numBytes, channelId, packetSendType, remoteId);
	}

	public static void StaticUpdate()
	{
		if (!s_SendingPacket && s_PendingPacketQueue.Count > 0)
		{
			PendingPacketData pendingPacketData = s_PendingPacketQueue.Dequeue();
			FinalSendPacket(pendingPacketData.Bytes, pendingPacketData.NumBytes, pendingPacketData.Channel, pendingPacketData.packetSendType, pendingPacketData.remoteId);
		}
	}

	private static void FinalSendPacket(byte[] bytes, int numBytes, int channelId, P2pPacketSendTypeEnum packetSendType, string remoteId)
	{
		string text = Convert.ToBase64String(bytes);
		s_SendingPacket = true;
		OddishSdk.Networking.CreateSendP2pPacket().ExecAsync(remoteId, text, text.Length, packetSendType, channelId, delegate
		{
			s_SendingPacket = false;
		}, delegate(SendP2pPacketTask.FailResult result)
		{
			d.LogError($"TTNetworkConnectionImplNetEase.HandleTransportSend - Failed send p2p packet, reason {result}");
			s_SendingPacket = false;
		});
	}

	public override bool HandleTransportSend(byte[] bytes, int numBytes, int channelId)
	{
		if (m_RemoteTTId.IsValid())
		{
			P2pPacketSendTypeEnum p2PSendMethod = GetP2PSendMethod(channelId);
			DispatchPacketQueued(bytes, numBytes, channelId, p2PSendMethod, m_RemoteId);
			return true;
		}
		return false;
	}

	public override bool HandleTransportReceive(byte[] bytes, int numBytes, int channelId)
	{
		if (m_LastReceived != null && m_LastReceived.Length == numBytes)
		{
			bool flag = false;
			for (int i = 0; i < m_LastReceived.Length; i++)
			{
				if (m_LastReceived[i] != bytes[i])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		m_LastReceived = bytes;
		return true;
	}

	private P2pPacketSendTypeEnum GetP2PSendMethod(int channelId)
	{
		if (!IsChannelReliable(channelId))
		{
			return P2pPacketSendTypeEnum.SendUnreliable;
		}
		return P2pPacketSendTypeEnum.SendReliable;
	}

	public override void HandleConnectionDisposed()
	{
		m_RemoteTTId.Clear();
		m_RemoteId = string.Empty;
	}
}
