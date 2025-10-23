#define UNITY_EDITOR
using System.Text;
using UnityEngine.Networking;

public class NetWaypoint : NetworkBehaviour, ManNetwork.IDumpableBehaviour
{
	private Visible m_Visible;

	private int m_HostID;

	private const uint kSer_HostID_F = 1u;

	private const uint kSer_Position_F = 2u;

	private const uint kSer_AllFlagMask = uint.MaxValue;

	public int HostID
	{
		get
		{
			return m_HostID;
		}
		set
		{
			if (m_HostID != value)
			{
				m_HostID = value;
				SetDirtyBit(1u);
			}
		}
	}

	public void Dump(StringBuilder builder)
	{
		Waypoint component = GetComponent<Waypoint>();
		if ((bool)component && (bool)component.visible)
		{
			builder.AppendFormat("Position={0}\n", component.visible.centrePosition);
		}
	}

	public void SetMoved()
	{
		SetDirtyBit(2u);
	}

	public override int GetNetworkChannel()
	{
		return 0;
	}

	public override bool OnSerialize(NetworkWriter writer, bool initialState)
	{
		uint num = (initialState ? uint.MaxValue : base.syncVarDirtyBits);
		writer.WritePackedUInt32(num);
		if ((num & 1) != 0)
		{
			writer.Write(m_HostID);
		}
		if ((num & 2) != 0)
		{
			d.Log($"NetWaypoint send position for {HostID} {base.transform.position}");
			WorldPosition worldPos = WorldPosition.FromScenePosition(base.transform.position);
			writer.Write(worldPos);
		}
		return num != 0;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		uint num = reader.ReadPackedUInt32();
		if ((num & 1) != 0)
		{
			m_HostID = reader.ReadInt32();
			if (!initialState || m_HostID != 0)
			{
				Singleton.Manager<ManVisible>.inst.TryLinkVisibleToTrackedVisible(m_Visible, m_HostID);
			}
		}
		if ((num & 2) != 0)
		{
			WorldPosition worldPosition = reader.ReadWorldPosition();
			base.transform.position = worldPosition.ScenePosition;
			d.Log($"NetWaypoint receive position for {HostID} {worldPosition.ScenePosition}");
		}
	}

	private void PrePool()
	{
		d.Assert(GetComponent<NetworkIdentity>().IsNotNull(), "Trying to spawn 'networked' waypoint but it does not have a network identity component!");
	}

	private void OnPool()
	{
		m_Visible = GetComponent<Visible>();
	}

	private void OnSpawn()
	{
		m_HostID = 0;
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			NetworkServer.Spawn(base.gameObject);
		}
	}

	private void OnRecycle()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			NetworkServer.UnSpawn(base.gameObject);
		}
	}

	private void UNetVersion()
	{
	}
}
