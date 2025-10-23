#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class UndoBlockDetach : ManUndo.IUndoCommand
{
	[Serializable]
	public class Config
	{
		public AnimationCurve m_UndoInterpPositionProfile;

		public float m_UndoInterpTime = 0.2f;

		public float m_ClearUndoBufferAfterTime = 15f;

		public float m_MaxUndoBlockDistance = 1E+11f;

		public float m_ReleaseBeamThresholdCos = 0.9f;
	}

	private class Buffer
	{
		public List<TankBlock> blocks = new List<TankBlock>(10);

		public Tank tech;

		public WorldPosition techPos;

		public Bounds techBounds;

		public Quaternion techRot;

		public string techName;

		public bool wasFocus;

		public bool wasGrounded;

		public bool wasAnchored;

		public Dictionary<int, TechComponent.SerialData> techSaveState;
	}

	private Buffer m_Buffer = new Buffer();

	private bool m_WaitingToReleaseBeam;

	private bool m_AutoBuildRunning;

	private bool m_WaitingForOrientationReset;

	private float m_Timestamp;

	private bool m_HasExpiredBlocks;

	private Config m_Config;

	private UIUndoButton.Context m_UIContext = new UIUndoButton.Context();

	private Dictionary<TankBlock, TankPreset.BlockSpec> m_BlockSpecLookup = new Dictionary<TankBlock, TankPreset.BlockSpec>();

	public UndoTypes UndoType => UndoTypes.BlockDetach;

	public UIUndoButton.Context UIContext => m_UIContext;

	public UndoBlockDetach(Config config)
	{
		m_Config = config;
		m_UIContext.m_UndoTypes = UndoType;
	}

	public void OnBeforeDetachBlock(TankBlock block)
	{
		Tank tank = block.tank;
		bool flag = true;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			flag = !Singleton.Manager<ManNetwork>.inst.NetController.AllowCollaboration;
		}
		if (flag)
		{
			d.Assert(block.tank.Team == Singleton.Manager<ManPlayer>.inst.PlayerTeam, "Recording block dettachment that belongs to other team");
		}
		d.Assert(m_Buffer.tech == null, "UndoBlockDetachCommand starting to poll for new block events before being recyled properly");
		Reset();
		Singleton.Manager<ManNetwork>.inst.BlockReplacedWithNetBlockEvent.Subscribe(OnNetBlockReplacement);
		SetBufferTech(tank);
		m_Buffer.techBounds = tank.blockBounds;
		m_Buffer.techPos = WorldPosition.FromScenePosition(tank.trans.position);
		m_Buffer.techRot = tank.trans.rotation;
		m_Buffer.techName = tank.name;
		m_Buffer.wasFocus = tank == Singleton.playerTank;
		m_Buffer.wasGrounded = tank.grounded;
		m_Buffer.wasAnchored = tank.IsAnchored;
		m_Buffer.blocks.Clear();
		m_Buffer.techSaveState = new Dictionary<int, TechComponent.SerialData>();
		tank.SerializeEvent.Send(paramA: true, m_Buffer.techSaveState);
		m_BlockSpecLookup.Clear();
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tank.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			TankPreset.BlockSpec value = default(TankPreset.BlockSpec);
			value.InitFromBlockState(current, saveRuntimeState: true);
			m_BlockSpecLookup.Add(current, value);
		}
		m_UIContext.m_TechName = tank.name;
	}

	public void OnDetachBlock(TankBlock block, Tank fromTech)
	{
		if (fromTech == m_Buffer.tech && block.visible.isActive)
		{
			m_Buffer.blocks.Add(block);
		}
	}

	public bool OnAfterDetachBlock()
	{
		if (m_Buffer.blocks.Count > 1)
		{
			return true;
		}
		return false;
	}

	public void ArmedStart()
	{
		m_Timestamp = Time.time;
		m_HasExpiredBlocks = false;
	}

	public void ArmedRefresh()
	{
		m_Timestamp = Time.time;
	}

	public bool ArmedUpdateValid()
	{
		if (Singleton.playerTank == null)
		{
			return false;
		}
		if (TooFarFromTech(Singleton.playerTank.trans))
		{
			return false;
		}
		for (int num = m_Buffer.blocks.Count - 1; num >= 0; num--)
		{
			TankBlock tankBlock = m_Buffer.blocks[num];
			bool flag = Singleton.Manager<ManPointer>.inst.DraggingItem == tankBlock.visible;
			if (!tankBlock || !tankBlock.visible.isActive || ((bool)tankBlock.tank && !ManSpawn.IsPlayerTeam(tankBlock.tank.Team)) || (!flag && TooFarFromTech(tankBlock.trans)))
			{
				m_Buffer.blocks.RemoveAt(num);
				m_HasExpiredBlocks = true;
			}
		}
		if (m_Buffer.blocks.Count < 1)
		{
			return false;
		}
		if (Time.time - m_Timestamp > m_Config.m_ClearUndoBufferAfterTime)
		{
			return false;
		}
		return true;
	}

	public void ExecuteStart()
	{
		if ((bool)m_Buffer.tech && !m_Buffer.tech.visible.isActive)
		{
			SetBufferTech(null);
		}
		for (int i = 0; i < m_Buffer.blocks.Count; i++)
		{
			m_Buffer.blocks[i].visible.SetLockTimout(Visible.LockTimerTypes.StackAccept, m_Config.m_UndoInterpTime + 0.1f);
			m_Buffer.blocks[i].visible.SetHolder(null);
		}
		Singleton.Manager<ManNetwork>.inst.BlockUndoAuthorityDenied.Subscribe(OnUndoAuthorityDenied);
		RequestAuthorityForBlocks();
		m_AutoBuildRunning = true;
		if (m_Buffer.tech == null)
		{
			Vector3 scenePosition = m_Buffer.techPos.ScenePosition;
			TrackedVisible trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnEmptyTechRef(0, scenePosition, m_Buffer.techRot, grounded: false, addToManager: true, m_Buffer.techName);
			m_Buffer.tech = trackedVisible.visible.tank;
		}
		if (m_Buffer.wasFocus)
		{
			Singleton.Manager<ManTechs>.inst.SetPlayerTankLocally(m_Buffer.tech);
		}
		m_Buffer.tech.grounded = m_Buffer.wasGrounded;
		Vector3 centreWorld = (m_Buffer.wasAnchored ? m_Buffer.techPos.ScenePosition : m_Buffer.tech.boundsCentreWorld);
		Quaternion sceneRot = (m_Buffer.wasAnchored ? m_Buffer.techRot : m_Buffer.tech.trans.rotation);
		ManTechSwapper.Operation newOperation = Singleton.Manager<ManTechSwapper>.inst.GetNewOperation();
		newOperation.InitAttachToTech(m_Buffer.tech, centreWorld, sceneRot, m_Buffer.wasAnchored, m_Buffer.blocks, m_BlockSpecLookup, m_Buffer.techSaveState);
		newOperation.SubscribeToCompletionCallback(OnAutoBuildComplete);
	}

	private void OnAutoBuildComplete()
	{
		m_AutoBuildRunning = false;
	}

	public bool ExecuteUpdateValid()
	{
		return m_AutoBuildRunning;
	}

	public void Reset()
	{
		Singleton.Manager<ManNetwork>.inst.BlockReplacedWithNetBlockEvent.Unsubscribe(OnNetBlockReplacement);
		Singleton.Manager<ManNetwork>.inst.BlockUndoAuthorityDenied.Unsubscribe(OnUndoAuthorityDenied);
		SetBufferTech(null);
		m_Buffer.blocks.Clear();
	}

	public Tank GetBufferTech()
	{
		d.Assert(m_Buffer != null);
		return m_Buffer.tech;
	}

	private bool TooFarFromTech(Transform trans)
	{
		if (m_Config.m_MaxUndoBlockDistance <= 0f)
		{
			return false;
		}
		Vector3 position = trans.position;
		Vector3 vector = (m_Buffer.tech ? m_Buffer.tech.boundsCentreWorld : m_Buffer.techPos.ScenePosition);
		Vector3 point = position - vector;
		Vector3 vector2 = m_Buffer.techBounds.ClosestPoint(point);
		vector2 += vector;
		if ((position - vector2).SetY(0f).sqrMagnitude > m_Config.m_MaxUndoBlockDistance * m_Config.m_MaxUndoBlockDistance)
		{
			return true;
		}
		return false;
	}

	private void SetBufferTech(Tank tech)
	{
		if (m_Buffer.tech != null)
		{
			m_Buffer.tech.visible.RecycledEvent.Unsubscribe(OnTechRecycled);
		}
		m_Buffer.tech = tech;
		if (m_Buffer.tech != null)
		{
			m_Buffer.tech.visible.RecycledEvent.Subscribe(OnTechRecycled);
		}
	}

	private void RequestAuthorityForBlocks()
	{
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			return;
		}
		for (int num = m_Buffer.blocks.Count - 1; num >= 0; num--)
		{
			TankBlock tankBlock = m_Buffer.blocks[num];
			if (tankBlock.netBlock != null)
			{
				bool flag = true;
				if (tankBlock.netBlock.NetIdentity.clientAuthorityOwner != null && Singleton.Manager<ManNetwork>.inst.FindPlayerByConnection(tankBlock.netBlock.NetIdentity.clientAuthorityOwner) != Singleton.Manager<ManNetwork>.inst.MyPlayer && tankBlock.netBlock.GetAuthorityReason > ManNetwork.AuthorityReason.Collision)
				{
					flag = false;
				}
				if (flag)
				{
					Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.BlockRequestUndoAuthority, new BlockRequestUndoAuthority(), tankBlock.netBlock.netId);
				}
				else
				{
					m_Buffer.blocks.RemoveAt(num);
				}
			}
		}
	}

	private void OnTechRecycled(Visible visible)
	{
		d.Assert(m_Buffer.tech.visible.ID == visible.ID, "Getting tech recycle event for a different visible. How is this possible?");
		SetBufferTech(null);
	}

	private void OnNetBlockReplacement(TankBlock stdBlock, TankBlock netBlock)
	{
		bool flag = false;
		for (int i = 0; i < m_Buffer.blocks.Count; i++)
		{
			if (m_Buffer.blocks[i] == stdBlock)
			{
				m_Buffer.blocks[i] = netBlock;
				flag = true;
				break;
			}
		}
		if (flag && m_AutoBuildRunning)
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.BlockRequestUndoAuthority, new BlockRequestUndoAuthority(), netBlock.netBlock.netId);
		}
	}

	private void OnUndoAuthorityDenied(NetBlock netBlock)
	{
		TankBlock block = netBlock.block;
		for (int i = 0; i < m_Buffer.blocks.Count; i++)
		{
			if (m_Buffer.blocks[i] == block)
			{
				m_Buffer.blocks.RemoveAt(i);
				break;
			}
		}
	}
}
