#define UNITY_EDITOR
using UnityEngine;

public class TrackedVisible
{
	public Event<Visible> OnDespawnEvent;

	public Event<Visible> OnRespawnEvent;

	private int m_ID;

	private Visible m_Visible;

	private RadarTypes m_RadarType;

	private ObjectTypes m_Object;

	private WorldPosition m_Position;

	private bool m_Dead;

	private int m_TeamID = int.MaxValue;

	private int m_RadarTeamID = int.MaxValue;

	private bool m_IsQuestObject;

	private RadarMarker m_RadarMarkerConfig = RadarMarker.DefaultMarker_Disabled;

	private bool m_OverlayEnabled = true;

	private RadarTypes m_DefaultRadarType;

	public int ID => m_ID;

	public Visible visible
	{
		get
		{
			if (!m_Visible || !m_Visible.isActive)
			{
				return null;
			}
			return m_Visible;
		}
	}

	public ObjectTypes ObjectType
	{
		get
		{
			return m_Object;
		}
		set
		{
			m_Object = value;
		}
	}

	public Vector3 Position
	{
		get
		{
			if (!visible)
			{
				return m_Position.ScenePosition;
			}
			return visible.trans.position;
		}
	}

	public bool wasDestroyed => m_Dead;

	public int HostID { get; set; }

	public bool WaypointOverlayEnabled
	{
		get
		{
			return m_OverlayEnabled;
		}
		set
		{
			if (m_OverlayEnabled != value)
			{
				m_OverlayEnabled = value;
				SendMPTrackedVisibleUpdatedMsg();
			}
		}
	}

	public bool IsVendor
	{
		get
		{
			if (ObjectType == ObjectTypes.Vehicle)
			{
				return RadarType == RadarTypes.Vendor;
			}
			return false;
		}
	}

	public bool IsQuestObject
	{
		get
		{
			return m_IsQuestObject;
		}
		set
		{
			if (m_IsQuestObject != value)
			{
				m_IsQuestObject = value;
				SendMPTrackedVisibleUpdatedMsg();
			}
		}
	}

	public RadarTypes RadarType
	{
		get
		{
			TryUpdateRadarType();
			return m_RadarType;
		}
		set
		{
			if (m_RadarType != value)
			{
				m_RadarType = value;
				SendMPTrackedVisibleUpdatedMsg();
			}
		}
	}

	public int TeamID
	{
		get
		{
			TryUpdateTeamID();
			return m_TeamID;
		}
		set
		{
			d.Assert(!ManNetwork.IsHost || !visible || !visible.tank, "Setting team on TrackedVisible when it has a valid visible");
			if (m_TeamID != value)
			{
				m_TeamID = value;
				SendMPTrackedVisibleUpdatedMsg();
			}
		}
	}

	public RadarMarker RadarMarkerConfig
	{
		get
		{
			TryUpdateRadarMarkerConfig();
			return m_RadarMarkerConfig;
		}
		set
		{
			m_RadarMarkerConfig = value;
			SendMPTrackedVisibleUpdatedMsg();
		}
	}

	public int RadarTeamID
	{
		get
		{
			if (m_RadarTeamID == int.MaxValue)
			{
				return TeamID;
			}
			return m_RadarTeamID;
		}
		set
		{
			if (m_RadarTeamID != value)
			{
				m_RadarTeamID = value;
				SendMPTrackedVisibleUpdatedMsg();
			}
		}
	}

	public int RawRadarTeamID => m_RadarTeamID;

	public RadarTypes DefaultRadarType => m_DefaultRadarType;

	public TrackedVisible(int id, Visible v, ObjectTypes objType, RadarTypes radarType)
	{
		d.AssertFormat(objType != ObjectTypes.Scenery, "TrackedVisible being created for Scenery Object {0}. This is not supported! Call a coder.", (v != null) ? v.name : "");
		m_ID = id;
		HostID = (ManNetwork.IsHost ? id : 0);
		m_Visible = v;
		m_Object = objType;
		m_RadarType = radarType;
		m_DefaultRadarType = m_RadarType;
		m_Dead = false;
		m_RadarMarkerConfig = ((v.IsNotNull() && v.tank.IsNotNull()) ? v.tank.RadarMarker.RadarMarkerConfig : RadarMarker.DefaultMarker_Disabled);
		m_TeamID = int.MaxValue;
		m_RadarTeamID = int.MaxValue;
		if ((bool)m_Visible && m_Visible.isActive)
		{
			m_Visible.RecycledEvent.Subscribe(OnVisibleRecycled);
		}
	}

	public void SetPos(Vector3 scenePos)
	{
		if (float.IsNaN(scenePos.x) || float.IsNaN(scenePos.y) || float.IsNaN(scenePos.z))
		{
			scenePos = Vector3.zero;
			d.LogError("TrackedVisible - Somehow pos is NAN");
		}
		SetPos(WorldPosition.FromScenePosition(in scenePos));
	}

	public void SetPos(WorldPosition pos)
	{
		if (m_Position != pos)
		{
			m_Position = pos;
			SendMPTrackedVisibleUpdatedMsg();
		}
	}

	public WorldPosition GetWorldPosition()
	{
		if (visible != null)
		{
			return WorldPosition.FromScenePosition(visible.trans.position);
		}
		return m_Position;
	}

	public void StopTracking()
	{
		ClearVisible();
		m_Dead = false;
	}

	public void ClientUpdateID(int newID)
	{
		d.Assert(!ManNetwork.IsHost, "ClientUpdateID was called in Singleplayer or on the Server - this is invalid!");
		m_ID = newID;
	}

	private void ClearVisible()
	{
		if (m_Visible != null)
		{
			m_Visible.RecycledEvent.Unsubscribe(OnVisibleRecycled);
		}
		m_Visible = null;
	}

	private void OnVisibleRecycled(Visible visible)
	{
		if (visible.Killed)
		{
			m_Dead = true;
		}
		else if (ManSaveGame.Storing)
		{
			m_Position = WorldPosition.FromScenePosition(visible.trans.position);
			TryUpdateRadarType();
			TryUpdateTeamID();
			TryUpdateRadarMarkerConfig();
			SendMPTrackedVisibleUpdatedMsg();
			OnDespawnEvent.Send(visible);
		}
		ClearVisible();
	}

	public void OnRespawn()
	{
		m_Visible = Singleton.Manager<ManSaveGame>.inst.LookupSerializedVisible(m_ID);
		if (m_Visible != null)
		{
			m_Visible.RecycledEvent.Subscribe(OnVisibleRecycled);
			OnRespawnEvent.Send(m_Visible);
		}
		else
		{
			d.LogError("ERROR: Unable to find visible on respawn with id " + m_ID);
		}
	}

	public void OnRespawn(Visible visible)
	{
		Visible visible2 = m_Visible;
		m_Visible = visible;
		if (m_Visible != null)
		{
			if (visible != visible2)
			{
				m_Visible.RecycledEvent.Subscribe(OnVisibleRecycled);
				OnRespawnEvent.Send(m_Visible);
			}
		}
		else
		{
			d.LogError("ERROR: Unable to find visible on respawn with id " + m_ID);
		}
	}

	private void TryUpdateTeamID()
	{
		Visible visible = this.visible;
		if ((bool)visible)
		{
			Tank tank = visible.tank;
			if ((bool)tank)
			{
				m_TeamID = tank.Team;
			}
		}
	}

	private void TryUpdateRadarMarkerConfig()
	{
		Visible visible = this.visible;
		if (!visible.IsNull() && !visible.tank.IsNull())
		{
			m_RadarMarkerConfig = visible.tank.RadarMarker.RadarMarkerConfig;
		}
	}

	private void TryUpdateRadarType()
	{
		Visible visible = this.visible;
		if ((bool)visible)
		{
			Tank tank = visible.tank;
			if ((bool)tank && (m_RadarType == RadarTypes.Vehicle || m_RadarType == RadarTypes.Base) && tank.blockman.GetRootBlock().IsNotNull())
			{
				m_RadarType = ((!tank.IsBase) ? RadarTypes.Vehicle : RadarTypes.Base);
			}
		}
	}

	private void SendMPTrackedVisibleUpdatedMsg()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer && Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_ID) == this)
		{
			UpdateTrackedVisibleMessage message = new UpdateTrackedVisibleMessage(this);
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.UpdateTrackedVisible, message);
		}
	}

	public void ToggleWaypointOverlayEnabled(Encounter encounter, bool visible)
	{
		m_OverlayEnabled = visible;
		SendMPTrackedVisibleUpdatedMsg();
	}
}
