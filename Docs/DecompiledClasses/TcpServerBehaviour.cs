#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class TcpServerBehaviour : MonoBehaviour
{
	private TcpListener m_Server;

	private List<TCPConnection> m_Clients = new List<TCPConnection>();

	private Action<TCPConnection, BinaryReader, int> m_MessageHandler;

	public void SendToAll(byte[] data, int pos, int len)
	{
		foreach (TCPConnection client in m_Clients)
		{
			client.Send(data, pos, len);
		}
	}

	public bool IsRunning()
	{
		return m_Server != null;
	}

	public bool StartServer(int port, Action<TCPConnection, BinaryReader, int> messageHandler)
	{
		d.Assert(m_Server == null);
		StopServer();
		try
		{
			m_MessageHandler = messageHandler;
			m_Server = new TcpListener(IPAddress.Any, port);
			m_Server.Start();
			return true;
		}
		catch (SocketException)
		{
			m_MessageHandler = null;
			m_Server = null;
			return false;
		}
	}

	public void StopServer()
	{
		foreach (TCPConnection client in m_Clients)
		{
			client.Dispose();
		}
		m_Clients.Clear();
		if (m_Server != null)
		{
			m_Server.Stop();
			m_Server = null;
		}
	}

	public void RemoveConnection(TCPConnection connection)
	{
		if (m_Clients.Contains(connection))
		{
			connection.Dispose();
			m_Clients.Remove(connection);
		}
	}

	private void OnDestroy()
	{
		StopServer();
	}

	private void Update()
	{
		if (m_Server == null)
		{
			return;
		}
		while (m_Server.Pending())
		{
			TCPConnection tCPConnection = new TCPConnection(m_Server.AcceptTcpClient(), m_MessageHandler);
			d.Log("Add connection to " + tCPConnection);
			m_Clients.Add(tCPConnection);
		}
		for (int num = m_Clients.Count - 1; num >= 0; num--)
		{
			TCPConnection tCPConnection2 = m_Clients[num];
			if (!tCPConnection2.Update())
			{
				d.Log("Remove connection from " + tCPConnection2);
				m_Clients.RemoveAt(num);
				tCPConnection2.Dispose();
			}
		}
	}
}
