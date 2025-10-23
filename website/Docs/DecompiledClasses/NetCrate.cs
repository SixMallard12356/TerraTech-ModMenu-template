#define UNITY_EDITOR
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetCrate : NetworkBehaviour, ManNetwork.IDumpableBehaviour
{
	private const float OPEN_CHECK_TIME_INTERVAL = 0.25f;

	private bool m_Opened;

	private bool m_WaitingForSpawning = true;

	private bool m_WaitingForFadeOut = true;

	private float m_OpenCheckTimer = 0.25f;

	private List<int> m_TestTeams = new List<int>(8);

	private Collider[] m_CachedColliders;

	private bool m_ObserversInitialised;

	private bool m_PhysicsEnabled;

	private bool m_CollisionEnabled;

	private int m_HostID;

	public static Event<NetCrate> OnNetCrateOpened;

	private const uint kSer_PhysicsEnabled_F = 1u;

	private const uint kSer_CollisionEnabled_F = 2u;

	private const uint kSer_HostID_F = 4u;

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
				SetDirtyBit(4u);
			}
		}
	}

	public NetworkIdentity NetIdentity { get; private set; }

	public Crate crate { get; private set; }

	public bool ObserversInitialised => m_ObserversInitialised;

	public void Dump(StringBuilder builder)
	{
		if ((bool)crate)
		{
			if ((bool)crate.visible)
			{
				builder.AppendFormat("Position={0}\n", crate.visible.centrePosition);
			}
			builder.AppendFormat("crate state={0}\n", crate.Save.m_State);
		}
	}

	public override bool OnSerialize(NetworkWriter writer, bool initialState)
	{
		uint num = (initialState ? uint.MaxValue : base.syncVarDirtyBits);
		writer.WritePackedUInt32(num);
		if ((num & 1) != 0)
		{
			writer.Write(crate.rbody.useGravity);
		}
		if ((num & 2) != 0)
		{
			writer.Write(m_CollisionEnabled);
		}
		if ((num & 4) != 0)
		{
			writer.Write(m_HostID);
		}
		return num != 0;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		uint num = reader.ReadPackedUInt32();
		if ((num & 1) != 0)
		{
			m_PhysicsEnabled = reader.ReadBoolean();
			crate.visible.EnablePhysics(m_PhysicsEnabled);
		}
		if ((num & 2) != 0)
		{
			m_CollisionEnabled = reader.ReadBoolean();
			_fixupCollision();
		}
		if ((num & 4) != 0)
		{
			m_HostID = reader.ReadInt32();
			Singleton.Manager<ManVisible>.inst.TryLinkVisibleToTrackedVisible(crate.visible, m_HostID);
		}
	}

	public override void OnStartAuthority()
	{
		base.OnStartAuthority();
	}

	public override void OnStopAuthority()
	{
		base.OnStopAuthority();
	}

	public override void OnStartServer()
	{
		base.OnStartServer();
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.ClientCrateStateUpdate, OnClientCrateStateUpdate);
	}

	public override void OnNetworkDestroy()
	{
		base.OnNetworkDestroy();
	}

	[Server]
	public void OnServerEnablePhysics(bool enable)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetCrate::OnServerEnablePhysics(System.Boolean)' called on client");
			return;
		}
		SetDirtyBit(1u);
		crate.visible.EnablePhysics(enable);
		m_PhysicsEnabled = enable;
	}

	[Server]
	private void ServerEnableCollision(bool enable)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetCrate::ServerEnableCollision(System.Boolean)' called on client");
			return;
		}
		SetDirtyBit(2u);
		m_CollisionEnabled = enable;
		_fixupCollision();
	}

	private void _fixupCollision()
	{
		if (m_CachedColliders != null)
		{
			for (int i = 0; i < m_CachedColliders.Length; i++)
			{
				m_CachedColliders[i].enabled = m_CollisionEnabled;
			}
		}
	}

	public void RevertPhysicsState()
	{
		crate.visible.EnablePhysics(m_PhysicsEnabled);
	}

	private void OnClientCrateStateUpdate(NetworkMessage netMsg)
	{
		if (base.isServer)
		{
			return;
		}
		CrateStateUpdateMessage crateStateUpdateMessage = netMsg.ReadMessage<CrateStateUpdateMessage>();
		if (crateStateUpdateMessage != null)
		{
			switch (crateStateUpdateMessage.m_State)
			{
			case Crate.State.Unlocking:
				crate.PlayUnlockAnimation();
				return;
			case Crate.State.Spawning:
				crate.PlayOpeningAnimation();
				return;
			case Crate.State.FadingOut:
				crate.PlayFadeOutAnimation();
				return;
			}
			d.LogWarning("NetCrate.OnClientCrateStateUpdate - Unexpected state=" + crateStateUpdateMessage.m_State.ToString() + " for Name=" + base.gameObject.name + " NetId=" + base.netId);
		}
	}

	private void _handleCollision(Collision collision)
	{
	}

	private void OnCollisionEnter(Collision collision)
	{
		_handleCollision(collision);
	}

	private void OnCollisionStay(Collision collision)
	{
		_handleCollision(collision);
	}

	private void OnPool()
	{
		crate = GetComponent<Crate>();
		NetIdentity = GetComponent<NetworkIdentity>();
		m_CachedColliders = base.gameObject.GetComponentsInChildren<Collider>();
	}

	private void OnSpawn()
	{
		m_HostID = 0;
		m_PhysicsEnabled = true;
		m_CollisionEnabled = true;
		m_Opened = false;
		m_WaitingForSpawning = true;
		m_WaitingForFadeOut = true;
		m_OpenCheckTimer = 0.25f;
		_fixupCollision();
	}

	private void OnRecycle()
	{
		m_CollisionEnabled = true;
		_fixupCollision();
		m_ObserversInitialised = false;
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromMessages(base.netId);
	}

	public override bool OnCheckObserver(NetworkConnection conn)
	{
		return Singleton.Manager<ManNetwork>.inst.CheckObserver(crate.visible, conn);
	}

	public override bool OnRebuildObservers(HashSet<NetworkConnection> observers, bool initialize)
	{
		if (initialize)
		{
			m_ObserversInitialised = true;
		}
		return Singleton.Manager<ManNetwork>.inst.RebuildObservers(crate.visible, observers);
	}

	private void Update()
	{
		if (base.isServer)
		{
			_updateServer();
		}
	}

	[Server]
	private void _updateServer()
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetCrate::_updateServer()' called on client");
			return;
		}
		if (!m_Opened)
		{
			if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeDeathmatch>())
			{
				CheckForCrateOpenInDeathmatch();
			}
			else if (crate.Save.m_State >= Crate.State.Unlocking)
			{
				ServerCrateUnlock();
			}
		}
		if (m_WaitingForSpawning)
		{
			if (crate.Save.m_State == Crate.State.Spawning)
			{
				m_WaitingForSpawning = false;
				CrateStateUpdateMessage message = new CrateStateUpdateMessage
				{
					m_State = Crate.State.Spawning
				};
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ClientCrateStateUpdate, message, base.netId);
			}
		}
		else if (m_WaitingForFadeOut && crate.Save.m_State == Crate.State.FadingOut)
		{
			m_WaitingForFadeOut = false;
			ServerEnableCollision(enable: false);
			CrateStateUpdateMessage message2 = new CrateStateUpdateMessage
			{
				m_State = Crate.State.FadingOut
			};
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ClientCrateStateUpdate, message2, base.netId);
		}
	}

	[Server]
	private void CheckForCrateOpenInDeathmatch()
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetCrate::CheckForCrateOpenInDeathmatch()' called on client");
			return;
		}
		m_OpenCheckTimer -= Time.deltaTime;
		if (!(m_OpenCheckTimer <= 0f))
		{
			return;
		}
		m_OpenCheckTimer = 0.25f;
		float num = ManNetwork.CrateDropPickupRanges[Singleton.Manager<ManNetwork>.inst.CrateDropPickupRangeIndex];
		float num2 = num * num;
		m_TestTeams.Clear();
		d.Assert(m_TestTeams.Capacity > 0);
		foreach (Tank currentTech in Singleton.Manager<ManTechs>.inst.CurrentTechs)
		{
			if (currentTech.netTech != null && currentTech.netTech.NetPlayer != null && (base.transform.position - currentTech.trans.position).sqrMagnitude <= num2)
			{
				int team = currentTech.Team;
				if (!m_TestTeams.Contains(team))
				{
					m_TestTeams.Add(team);
				}
			}
		}
		if (m_TestTeams.Count == 1)
		{
			crate.Unlock(autoSpawnContents: true);
			ServerCrateUnlock();
		}
	}

	[Server]
	private void ServerCrateUnlock()
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetCrate::ServerCrateUnlock()' called on client");
		}
		else if (!m_Opened)
		{
			m_Opened = true;
			m_WaitingForSpawning = true;
			m_WaitingForFadeOut = true;
			CrateStateUpdateMessage message = new CrateStateUpdateMessage
			{
				m_State = Crate.State.Unlocking
			};
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ClientCrateStateUpdate, message, base.netId);
			OnNetCrateOpened.Send(this);
		}
	}

	private void UNetVersion()
	{
	}
}
