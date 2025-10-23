#define UNITY_EDITOR
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class NetSpawnPoint : NetworkBehaviour
{
	private class Contact
	{
		public uint netId;

		public NetBlock netBlock;

		public NetTech netTech;

		public int numVolumes;

		public bool withinShield;

		public bool withinBarrier;

		private Rigidbody _rigidBody;

		public bool IsValid
		{
			get
			{
				bool result = false;
				if (netBlock != null)
				{
					result = netBlock.block != null && netBlock.block.trans != null;
				}
				else if (netTech != null)
				{
					result = netTech.tech != null && netTech.tech.trans != null;
				}
				return result;
			}
		}

		public Contact(uint id, NetBlock blk, NetTech nt)
		{
			d.Assert(blk != null || nt != null);
			netId = id;
			netBlock = blk;
			netTech = nt;
			numVolumes = 0;
			withinShield = false;
			withinBarrier = false;
			_rigidBody = null;
		}

		public Vector3 position()
		{
			if (!(netBlock != null))
			{
				return netTech.tech.trans.position;
			}
			return netBlock.block.trans.position;
		}

		public Rigidbody rigidBody()
		{
			if (_rigidBody == null)
			{
				_rigidBody = ((netTech != null) ? netTech.tech.rbody : netBlock.block.rbody);
				d.Assert(_rigidBody != null);
			}
			return _rigidBody;
		}

		public GameObject getGameObject()
		{
			if (!(netBlock != null))
			{
				return netTech.tech.gameObject;
			}
			return netBlock.block.gameObject;
		}

		public uint InitialSpawnShieldID()
		{
			if (!(netTech != null))
			{
				return netBlock.InitialSpawnShieldID;
			}
			return netTech.InitialSpawnShieldID;
		}

		public float encompassingRadius()
		{
			if (netTech != null)
			{
				return netTech.EncompassingRadius;
			}
			return 1f;
		}
	}

	[SerializeField]
	private float m_GrowDuration = 1f;

	[SerializeField]
	private float m_Radius = 50f;

	[SerializeField]
	private float m_ExtraTriggerSize = 5f;

	[SerializeField]
	private float m_BarrierForce = 150f;

	[SerializeField]
	private float m_BarrierForceBuildBeam = 300f;

	[SerializeField]
	[Range(1f, 100f)]
	private float m_BarrierForcePercentageForBlock = 10f;

	[SerializeField]
	[Range(0f, 10f)]
	private float m_BarrierShieldCountdownTimer = 3f;

	[SerializeField]
	private Transform m_ShieldGeom;

	[SerializeField]
	private GameObject m_TriggerVolume;

	[SerializeField]
	private bool m_RemoveScenery = true;

	[SyncVar]
	private bool m_ShieldRequested;

	[SyncVar]
	private bool m_BarrierRequested;

	private float m_ShieldScaleParam;

	private float m_CurShieldRadius;

	private float m_GrowSpeed;

	private bool m_BarrierEnabled;

	private float m_CountdownTimer;

	private GameObject m_ShieldObj;

	private Dictionary<int, Contact> m_Contacts = new Dictionary<int, Contact>(4);

	private List<int> s_InvalidElementsToRemove;

	public Transform trans { get; private set; }

	public float Radius => m_Radius;

	public bool Networkm_ShieldRequested
	{
		get
		{
			return m_ShieldRequested;
		}
		[param: In]
		set
		{
			SetSyncVar(value, ref m_ShieldRequested, 1u);
		}
	}

	public bool Networkm_BarrierRequested
	{
		get
		{
			return m_BarrierRequested;
		}
		[param: In]
		set
		{
			SetSyncVar(value, ref m_BarrierRequested, 2u);
		}
	}

	public bool IsBarrierEnabled()
	{
		if (!m_BarrierRequested)
		{
			return m_BarrierEnabled;
		}
		return true;
	}

	[Server]
	public void ServerSetShieldEnabled(bool enabled)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetSpawnPoint::ServerSetShieldEnabled(System.Boolean)' called on client");
		}
		else
		{
			Networkm_ShieldRequested = enabled;
		}
	}

	[Server]
	public void ServerSetBarrierEnabled(bool barrierEnabled)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetSpawnPoint::ServerSetBarrierEnabled(System.Boolean)' called on client");
			return;
		}
		Networkm_BarrierRequested = barrierEnabled;
		m_CountdownTimer = 0f;
	}

	[Server]
	public void ServerCommenceProtectionBubbleCountdown()
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetSpawnPoint::ServerCommenceProtectionBubbleCountdown()' called on client");
		}
		else if (m_BarrierEnabled && m_CountdownTimer <= 0f)
		{
			m_CountdownTimer = m_BarrierShieldCountdownTimer;
		}
	}

	public override void OnStartServer()
	{
		base.OnStartServer();
		if (m_RemoveScenery)
		{
			ManSpawn.RemoveAllSceneryAroundPosition(trans.position, m_Radius, ManSpawn.SceneryRemovalFlags.SpawnNoChunks | ManSpawn.SceneryRemovalFlags.PreventRegrow | ManSpawn.SceneryRemovalFlags.RemoveInstant | ManSpawn.SceneryRemovalFlags.RemovePersistentDamageStage);
		}
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		if (m_RemoveScenery && !base.isServer)
		{
			ManSpawn.RemoveAllSceneryAroundPosition(trans.position, m_Radius, ManSpawn.SceneryRemovalFlags.SpawnNoChunks | ManSpawn.SceneryRemovalFlags.PreventRegrow | ManSpawn.SceneryRemovalFlags.RemoveInstant | ManSpawn.SceneryRemovalFlags.RemovePersistentDamageStage);
		}
	}

	private bool TestPositionWithinRadius(Vector3 objectPos, float radius)
	{
		return (objectPos - trans.position).sqrMagnitude < radius * radius;
	}

	private void UpdateShieldScale()
	{
		float num = (m_ShieldRequested ? 1f : 0f);
		if (num != m_ShieldScaleParam)
		{
			m_ShieldScaleParam = Mathf.MoveTowards(m_ShieldScaleParam, num, m_GrowSpeed * Time.deltaTime);
			float num2 = Maths.SinEaseOut(m_ShieldScaleParam);
			m_CurShieldRadius = Radius * num2;
			float num3 = m_CurShieldRadius * 2f;
			m_ShieldGeom.localScale = new Vector3(num3, num3, num3);
		}
		bool flag = m_ShieldScaleParam > 0f;
		if (flag != m_ShieldObj.activeSelf)
		{
			m_ShieldObj.SetActive(flag);
		}
	}

	private void HandleTriggerEvent(TriggerCatcher.Interaction trigType, Collider otherCol)
	{
		Visible visible = null;
		if (otherCol.gameObject.layer == (int)Globals.inst.layerTank)
		{
			visible = Singleton.Manager<ManVisible>.inst.FindVisible(otherCol);
		}
		if (!(visible != null))
		{
			return;
		}
		TankBlock tankBlock = (visible ? visible.block : null);
		Tank tank = ((tankBlock != null) ? tankBlock.tank : visible.tank);
		if (!(tankBlock != null) && !(tank != null))
		{
			return;
		}
		Visible obj = (tank ? tank.visible : tankBlock.visible);
		int iD = obj.ID;
		if (!obj.GetNetInstanceID(out var networkInstanceId))
		{
			return;
		}
		uint value = networkInstanceId.Value;
		NetBlock netBlock = (tankBlock ? tankBlock.netBlock : null);
		NetTech netTech = (tank ? tank.netTech : null);
		if ((bool)netBlock || (bool)netTech)
		{
			switch (trigType)
			{
			case TriggerCatcher.Interaction.Enter:
			{
				if (!m_Contacts.TryGetValue(iD, out var value3))
				{
					value3 = new Contact(value, netBlock, netTech);
					m_Contacts.Add(iD, value3);
					(netBlock ? netBlock.block.visible : netTech.tech.visible).RecycledEvent.Subscribe(OnVisibleContactRecycled);
					if (TestPositionWithinRadius(value3.position(), Radius) && m_BarrierEnabled)
					{
						value3.withinBarrier = true;
					}
				}
				value3.numVolumes++;
				break;
			}
			case TriggerCatcher.Interaction.Exit:
			{
				if (!m_Contacts.TryGetValue(iD, out var value2))
				{
					break;
				}
				value2.numVolumes--;
				if (value2.numVolumes <= 0)
				{
					if (value2.withinShield && value2.netTech != null)
					{
						NetTech netTech2 = value2.netTech;
						int spawnShieldCount = netTech2.SpawnShieldCount - 1;
						netTech2.SpawnShieldCount = spawnShieldCount;
					}
					(value2.netBlock ? value2.netBlock.block.visible : value2.netTech.tech.visible).RecycledEvent.Unsubscribe(OnVisibleContactRecycled);
					m_Contacts.Remove(iD);
				}
				break;
			}
			}
		}
		else
		{
			d.LogErrorFormat("Unexpected Contact found in NetSpawnPoint.HandleTriggerEvent! Collided with object {0} (vis {1})", otherCol.name, Visible.FindVisibleUpwards(otherCol));
		}
	}

	private void OnVisibleContactRecycled(Visible vis)
	{
		vis.RecycledEvent.Unsubscribe(OnVisibleContactRecycled);
		if (m_Contacts.TryGetValue(vis.ID, out var value))
		{
			if (value.withinShield && value.netTech != null)
			{
				NetTech netTech = value.netTech;
				int spawnShieldCount = netTech.SpawnShieldCount - 1;
				netTech.SpawnShieldCount = spawnShieldCount;
			}
			m_Contacts.Remove(vis.ID);
		}
	}

	private void OnPool()
	{
		trans = base.transform;
		m_ShieldObj = m_ShieldGeom.gameObject;
		m_GrowSpeed = 1f / Mathf.Max(m_GrowDuration, 0.0001f);
		float num = (Radius + m_ExtraTriggerSize) * 2f;
		m_TriggerVolume.transform.localScale = new Vector3(num, num, num);
	}

	private void OnSpawn()
	{
		m_ShieldScaleParam = 0f;
		m_CurShieldRadius = 0f;
		Networkm_ShieldRequested = false;
		m_ShieldGeom.gameObject.SetActive(value: false);
		Networkm_BarrierRequested = false;
		m_BarrierEnabled = false;
		m_CountdownTimer = 0f;
		TriggerCatcher.Subscribe(m_TriggerVolume, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Exit, HandleTriggerEvent);
	}

	private void OnRecycle()
	{
		TriggerCatcher.Unsubscribe(m_TriggerVolume, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Exit, HandleTriggerEvent);
		foreach (KeyValuePair<int, Contact> contact in m_Contacts)
		{
			Contact value = contact.Value;
			if (value.withinShield && value.netTech != null)
			{
				NetTech netTech = value.netTech;
				int spawnShieldCount = netTech.SpawnShieldCount - 1;
				netTech.SpawnShieldCount = spawnShieldCount;
			}
			(value.netBlock ? value.netBlock.block.visible : value.netTech.tech.visible).RecycledEvent.Unsubscribe(OnVisibleContactRecycled);
		}
		m_Contacts.Clear();
	}

	private void Update()
	{
		UpdateShieldScale();
		if (!Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			return;
		}
		if (m_BarrierEnabled && m_CountdownTimer > 0f)
		{
			m_CountdownTimer -= Time.deltaTime;
			if (m_CountdownTimer <= 0f)
			{
				ServerSetShieldEnabled(enabled: false);
				ServerSetBarrierEnabled(barrierEnabled: false);
			}
		}
		else if ((!m_BarrierRequested || !m_BarrierEnabled) && m_ShieldScaleParam <= 0f)
		{
			if (Singleton.Manager<ManNetwork>.inst.ServerSpawnBank != null)
			{
				Singleton.Manager<ManNetwork>.inst.ServerSpawnBank.RemoveRecycledShieldBubble(this);
			}
			NetworkServer.UnSpawn(base.gameObject);
			trans.Recycle();
		}
	}

	private void FixedUpdate()
	{
		bool flag = false;
		if (m_BarrierEnabled != m_BarrierRequested)
		{
			m_BarrierEnabled = m_BarrierRequested;
			flag = m_BarrierEnabled;
			m_CountdownTimer = 0f;
		}
		float num = Radius * Radius;
		float num2 = m_CurShieldRadius * m_CurShieldRadius;
		foreach (KeyValuePair<int, Contact> contact in m_Contacts)
		{
			int key = contact.Key;
			Contact value = contact.Value;
			if (!value.IsValid)
			{
				d.LogError("Found contact in list that was not valid!");
				if (s_InvalidElementsToRemove == null)
				{
					s_InvalidElementsToRemove = new List<int>();
				}
				s_InvalidElementsToRemove.Add(key);
				continue;
			}
			Vector3 input = value.position() - trans.position;
			float sqrMagnitude = input.sqrMagnitude;
			bool flag2 = sqrMagnitude < num;
			if (!flag2 && value.InitialSpawnShieldID() != base.netId.Value)
			{
				float num3 = value.encompassingRadius() * 2f;
				float num4 = Radius + num3;
				if (sqrMagnitude <= num4 * num4)
				{
					Collider[] componentsInChildren = value.getGameObject().GetComponentsInChildren<Collider>(includeInactive: false);
					if (componentsInChildren != null)
					{
						for (int i = 0; i < componentsInChildren.Length; i++)
						{
							if ((componentsInChildren[i].transform.position - trans.position).sqrMagnitude < num)
							{
								flag2 = true;
								break;
							}
						}
					}
				}
			}
			if (flag)
			{
				value.withinBarrier = flag2;
			}
			bool flag3 = m_CurShieldRadius > 0f && sqrMagnitude < num2;
			if (value.withinShield != flag3)
			{
				value.withinShield = flag3;
				if (value.netTech != null)
				{
					value.netTech.SpawnShieldCount += (flag3 ? 1 : (-1));
				}
			}
			bool flag4 = flag2 && value.netTech != null && value.InitialSpawnShieldID() != base.netId.Value;
			bool flag5 = !flag2 && value.InitialSpawnShieldID() == base.netId.Value;
			if (flag5 && value.netTech != null && value.netTech.InitialSpawnShieldID == base.netId.Value)
			{
				flag5 = false;
			}
			if (m_BarrierEnabled && (flag5 || flag4))
			{
				float num5 = (flag4 ? m_BarrierForce : (0f - m_BarrierForce));
				if (flag4)
				{
					num5 *= 3f - Mathf.Pow(Mathf.Clamp01(sqrMagnitude / num), 3f) * 2f;
				}
				if (value.netBlock != null)
				{
					num5 = num5 * m_BarrierForcePercentageForBlock * 0.01f;
				}
				if (flag4 && value.netTech != null)
				{
					Tank tech = value.netTech.tech;
					if (tech != null && tech.beam.IsActive)
					{
						input = input.SetY(0f);
						num5 = m_BarrierForceBuildBeam;
					}
				}
				value.rigidBody().AddForce(input.normalized * num5, ForceMode.Acceleration);
			}
			if (m_BarrierEnabled && value.netTech != null && !flag2 && value.netTech.InitialSpawnShieldID == base.netId.Value && Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				ServerCommenceProtectionBubbleCountdown();
			}
		}
		if (s_InvalidElementsToRemove != null)
		{
			for (int j = 0; j < s_InvalidElementsToRemove.Count; j++)
			{
				m_Contacts.Remove(s_InvalidElementsToRemove[j]);
			}
			s_InvalidElementsToRemove.Clear();
		}
	}

	private void UNetVersion()
	{
	}

	public override bool OnSerialize(NetworkWriter writer, bool forceAll)
	{
		if (forceAll)
		{
			writer.Write(m_ShieldRequested);
			writer.Write(m_BarrierRequested);
			return true;
		}
		bool flag = false;
		if ((base.syncVarDirtyBits & 1) != 0)
		{
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
				flag = true;
			}
			writer.Write(m_ShieldRequested);
		}
		if ((base.syncVarDirtyBits & 2) != 0)
		{
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
				flag = true;
			}
			writer.Write(m_BarrierRequested);
		}
		if (!flag)
		{
			writer.WritePackedUInt32(base.syncVarDirtyBits);
		}
		return flag;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		if (initialState)
		{
			m_ShieldRequested = reader.ReadBoolean();
			m_BarrierRequested = reader.ReadBoolean();
			return;
		}
		int num = (int)reader.ReadPackedUInt32();
		if ((num & 1) != 0)
		{
			m_ShieldRequested = reader.ReadBoolean();
		}
		if ((num & 2) != 0)
		{
			m_BarrierRequested = reader.ReadBoolean();
		}
	}
}
