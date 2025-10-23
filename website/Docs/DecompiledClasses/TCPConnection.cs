#define UNITY_EDITOR
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class TCPConnection : IDisposable
{
	private static byte[] s_DataBuffer = new byte[10240];

	private TcpClient m_TcpClient;

	private NetworkStream m_Stream;

	private byte[] m_DataBuffer = new byte[1024];

	private int m_DataBufferUsed;

	private Action<TCPConnection, BinaryReader, int> m_MessageAction;

	public TCPConnection(string server, int port, Action<TCPConnection, BinaryReader, int> action)
	{
		d.Log("TCPConnection construct server=" + server + " port=" + port);
		try
		{
			m_TcpClient = new TcpClient(server, port);
			m_Stream = m_TcpClient.GetStream();
			m_MessageAction = action;
		}
		catch (SocketException ex)
		{
			d.LogError("Failed to create TcpClient for " + server + ":" + port + "\n" + ex.GetType().Name + "\n" + ex.Message);
			m_TcpClient = null;
		}
	}

	public TCPConnection(TcpClient c, Action<TCPConnection, BinaryReader, int> action)
	{
		m_TcpClient = c;
		m_Stream = c.GetStream();
		m_MessageAction = action;
	}

	public bool IsConnected()
	{
		if (m_TcpClient != null && m_TcpClient.Client.RemoteEndPoint != null)
		{
			return m_TcpClient.Connected;
		}
		return false;
	}

	public override string ToString()
	{
		if (m_TcpClient == null || m_TcpClient.Client.RemoteEndPoint == null)
		{
			return "[disconnected]";
		}
		return m_TcpClient.Client.RemoteEndPoint.ToString();
	}

	public void Dispose()
	{
		if (m_Stream != null)
		{
			m_Stream.Dispose();
			m_Stream = null;
		}
		if (m_TcpClient != null)
		{
			m_TcpClient.Dispose();
			m_TcpClient = null;
		}
	}

	public void Send(byte[] data, int offset, int len)
	{
		if (m_Stream != null && m_Stream.CanWrite)
		{
			try
			{
				m_Stream.WriteByte((byte)(len & 0xFF));
				m_Stream.WriteByte((byte)((len >> 8) & 0xFF));
				m_Stream.WriteByte((byte)((len >> 16) & 0xFF));
				m_Stream.WriteByte((byte)((len >> 24) & 0xFF));
				m_Stream.Write(data, offset, len);
			}
			catch (IOException)
			{
				d.Log("Dropped connection from " + ToString());
				Dispose();
			}
		}
	}

	public void Send(string msg)
	{
		if (m_Stream == null || !m_Stream.CanWrite)
		{
			return;
		}
		using MemoryStream memoryStream = new MemoryStream(s_DataBuffer);
		using BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8);
		binaryWriter.Write(msg);
		Send(s_DataBuffer, 0, (int)memoryStream.Position);
	}

	public bool Update()
	{
		if (m_TcpClient == null)
		{
			return false;
		}
		if (!m_TcpClient.Connected)
		{
			return false;
		}
		if (!m_Stream.CanRead || !m_Stream.CanWrite)
		{
			return false;
		}
		while (m_Stream.DataAvailable)
		{
			m_DataBufferUsed += m_Stream.Read(m_DataBuffer, m_DataBufferUsed, m_DataBuffer.Length - m_DataBufferUsed);
			if (m_DataBufferUsed == m_DataBuffer.Length)
			{
				Array.Resize(ref m_DataBuffer, m_DataBuffer.Length * 2);
			}
		}
		if (m_DataBufferUsed >= 4)
		{
			int num = m_DataBuffer[0] | (m_DataBuffer[1] << 8) | (m_DataBuffer[2] << 16) | (m_DataBuffer[3] << 24);
			if (m_DataBufferUsed >= num + 4)
			{
				using MemoryStream input = new MemoryStream(m_DataBuffer, 4, m_DataBufferUsed);
				using BinaryReader arg = new BinaryReader(input, Encoding.UTF8);
				m_MessageAction?.Invoke(this, arg, num);
				int num2 = 4 + num;
				Array.Copy(m_DataBuffer, num2, m_DataBuffer, 0, m_DataBufferUsed - num2);
				m_DataBufferUsed -= num2;
			}
		}
		return true;
	}
}
