using UnityEngine.Networking;

namespace TerraTech.Network;

public abstract class TTNetworkConnectionImpl
{
	private TTNetworkID m_RemoteNetworkID;

	private HostTopology m_HostTopology;

	private int m_HostId;

	private int m_ConnectionId;

	private bool m_Initialised;

	public TTNetworkID RemoteNetworkID => m_RemoteNetworkID;

	public HostTopology HostTopology => m_HostTopology;

	public int HostID => m_HostId;

	public int ConnectionID => m_ConnectionId;

	public bool IsInitialised => m_Initialised;

	protected TTNetworkConnectionImpl(TTNetworkID remoteNetworkID, HostTopology hostTopology)
	{
		m_RemoteNetworkID = remoteNetworkID;
		m_HostTopology = hostTopology;
		m_Initialised = false;
		m_HostId = 0;
		m_ConnectionId = 0;
	}

	protected TTNetworkConnectionImpl()
	{
		m_RemoteNetworkID = TTNetworkID.Invalid;
		m_Initialised = false;
		m_HostId = 0;
		m_ConnectionId = 0;
	}

	public virtual void ChangeRemoteNetworkID(TTNetworkID remoteNetworkID)
	{
		m_RemoteNetworkID = remoteNetworkID;
	}

	public virtual void Initialise(int hostId, int connectionId)
	{
		m_HostId = hostId;
		m_ConnectionId = connectionId;
		m_Initialised = true;
	}

	public virtual void ChangeTopology(HostTopology hostTopology)
	{
		m_HostTopology = hostTopology;
	}

	public QosType GetChannelQos(int channelId)
	{
		return m_HostTopology.DefaultConfig.Channels[channelId].QOS;
	}

	public bool IsChannelReliable(int channelId)
	{
		bool result = false;
		switch (m_HostTopology.DefaultConfig.Channels[channelId].QOS)
		{
		case QosType.Reliable:
		case QosType.ReliableFragmented:
		case QosType.ReliableSequenced:
		case QosType.ReliableStateUpdate:
		case QosType.AllCostDelivery:
		case QosType.ReliableFragmentedSequenced:
			result = true;
			break;
		}
		return result;
	}

	public abstract bool HandleTransportSend(byte[] bytes, int numBytes, int channelId);

	public virtual bool HandleTransportReceive(byte[] bytes, int numBytes, int channelId)
	{
		return true;
	}

	public abstract void HandleConnectionDisposed();
}
