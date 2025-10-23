#define UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ManWorldTreadmill : Singleton.Manager<ManWorldTreadmill>
{
	[SerializeField]
	private bool m_EnableFloatingOrigin;

	[SerializeField]
	private float m_DistanceFromOriginBeforeMove = 384f;

	[Header("Debug")]
	[SerializeField]
	private bool m_PrintTimeTaken = true;

	public EventNoParams OnBeforeWorldOriginMove;

	public Event<IntVector3> OnAfterWorldOriginMoved;

	private List<IWorldTreadmill> m_Listeners = new List<IWorldTreadmill>(1024);

	private List<WorldSpaceObjectBase> m_WorldSpaceObjects = new List<WorldSpaceObjectBase>(1024);

	private Coroutine m_MoveWorldCoroutine;

	private Coroutine m_PostWorldMovePhysicsDetectionCoroutine;

	private Coroutine m_PostTileLoadPhysicsDetectionCoroutine;

	private bool m_Locked;

	private IntVector3 m_LockedPos;

	private bool m_TileLoadedWaitingForPhysicsSync;

	private bool m_DebugOscillate;

	private const int kMultiboxBroadphaseRegionAxisExtent = 2112;

	public bool HasPendingNetworkMove
	{
		get
		{
			if (m_Locked)
			{
				return m_LockedPos != Singleton.Manager<ManWorld>.inst.FloatingOrigin;
			}
			return false;
		}
	}

	public IntVector3 PendingNetworkOrigin => m_LockedPos;

	public void AddListener(IWorldTreadmill listener)
	{
		d.Assert(listener != null);
		d.Assert(!m_Listeners.Contains(listener));
		m_Listeners.Add(listener);
	}

	public void RemoveListener(IWorldTreadmill listener)
	{
		int num = m_Listeners.IndexOf(listener);
		if (num >= 0)
		{
			m_Listeners.RemoveAt(num);
		}
	}

	public void AddWorldSpaceObject(WorldSpaceObjectBase wobj)
	{
		wobj.m_WorldTreadmillIndex = m_WorldSpaceObjects.Count;
		m_WorldSpaceObjects.Add(wobj);
	}

	public void RemoveWorldSpaceObject(WorldSpaceObjectBase wobj)
	{
		int worldTreadmillIndex = wobj.m_WorldTreadmillIndex;
		wobj.m_WorldTreadmillIndex = -1;
		m_WorldSpaceObjects[worldTreadmillIndex] = m_WorldSpaceObjects[m_WorldSpaceObjects.Count - 1];
		m_WorldSpaceObjects[worldTreadmillIndex].m_WorldTreadmillIndex = worldTreadmillIndex;
		m_WorldSpaceObjects.RemoveAt(m_WorldSpaceObjects.Count - 1);
	}

	public void QueueMoveWorld(IntVector3 amountToMove)
	{
		if (m_MoveWorldCoroutine != null)
		{
			d.LogWarning("MoveWorld called while there was already a world move scheduled! Cancelling queued event and replacing with new request!");
			StopCoroutine(m_MoveWorldCoroutine);
		}
		if (amountToMove.x != 0 || amountToMove.y != 0 || amountToMove.z != 0)
		{
			m_MoveWorldCoroutine = StartCoroutine(MoveWorldAtEndOfFrame(amountToMove));
		}
	}

	private IEnumerator MoveWorldAtEndOfFrame(IntVector3 amountToMove)
	{
		yield return new WaitForEndOfFrame();
		DoWorldMove(amountToMove);
		m_MoveWorldCoroutine = null;
		m_PostWorldMovePhysicsDetectionCoroutine = StartCoroutine(DetectPhysicsUpdate());
	}

	private void DoWorldMove(IntVector3 amountToMove)
	{
		OnBeforeWorldOriginMove.Send();
		Singleton.cameraTrans.position += amountToMove;
		foreach (WorldSpaceObjectBase worldSpaceObject in m_WorldSpaceObjects)
		{
			worldSpaceObject.OnMoveWorldOrigin(amountToMove);
		}
		foreach (IWorldTreadmill listener in m_Listeners)
		{
			listener.OnMoveWorldOrigin(amountToMove);
		}
		if (!Physics.autoSyncTransforms)
		{
			Physics.SyncTransforms();
		}
		OnAfterWorldOriginMoved.Send(amountToMove);
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.LockTreadmill, new LockTreadmillMessage
			{
				m_FloatingOrigin = Singleton.Manager<ManWorld>.inst.FloatingOrigin
			});
		}
	}

	private IEnumerator DetectPhysicsUpdate()
	{
		yield return new WaitForFixedUpdate();
		m_PostWorldMovePhysicsDetectionCoroutine = null;
	}

	private IEnumerator DetectPhysicsUpdateAfterTileLoad()
	{
		yield return new WaitForFixedUpdate();
		m_PostTileLoadPhysicsDetectionCoroutine = null;
		m_TileLoadedWaitingForPhysicsSync = false;
	}

	private void OnManagersCreated()
	{
		Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Subscribe(OnModeCleanup);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.LockTreadmill, OnLockTreadmill);
	}

	private void OnTileManagerInitialised()
	{
		Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Subscribe(OnTileLoaded);
	}

	private void OnModeCleanup(Mode mode)
	{
		if (m_MoveWorldCoroutine != null)
		{
			StopCoroutine(m_MoveWorldCoroutine);
			m_MoveWorldCoroutine = null;
		}
		if (m_PostWorldMovePhysicsDetectionCoroutine != null)
		{
			StopCoroutine(m_PostWorldMovePhysicsDetectionCoroutine);
			m_PostWorldMovePhysicsDetectionCoroutine = null;
		}
		if (m_PostTileLoadPhysicsDetectionCoroutine != null)
		{
			StopCoroutine(m_PostTileLoadPhysicsDetectionCoroutine);
			m_PostTileLoadPhysicsDetectionCoroutine = null;
		}
		m_TileLoadedWaitingForPhysicsSync = false;
		m_Locked = false;
		m_LockedPos = IntVector3.zero;
	}

	private void OnTileLoaded(WorldTile tile)
	{
		m_TileLoadedWaitingForPhysicsSync = true;
		if (m_PostTileLoadPhysicsDetectionCoroutine == null)
		{
			m_PostTileLoadPhysicsDetectionCoroutine = StartCoroutine(DetectPhysicsUpdateAfterTileLoad());
		}
	}

	private void OnLockTreadmill(NetworkMessage msg)
	{
		LockTreadmillMessage lockTreadmillMessage = msg.ReadMessage<LockTreadmillMessage>();
		m_Locked = true;
		m_LockedPos = lockTreadmillMessage.m_FloatingOrigin;
		d.Log($"[ManWorldTreadmill] Received treadmill lock message with pos {m_LockedPos}. We are at {Singleton.Manager<ManWorld>.inst.FloatingOrigin}");
	}

	private void Awake()
	{
		Singleton.DoOnceAfterStart(OnManagersCreated);
		Singleton.Manager<ManWorld>.inst.TileManagerInitialisedEvent.Subscribe(OnTileManagerInitialised);
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
		if (m_PostWorldMovePhysicsDetectionCoroutine == null)
		{
			return;
		}
		StopCoroutine(m_PostWorldMovePhysicsDetectionCoroutine);
		m_PostWorldMovePhysicsDetectionCoroutine = null;
		foreach (WorldSpaceObjectBase worldSpaceObject in m_WorldSpaceObjects)
		{
			worldSpaceObject.OnForceSyncToRigidBody();
		}
	}

	private void LateUpdate()
	{
		if (!m_EnableFloatingOrigin || !Singleton.Manager<ManGameMode>.inst.CurrentModeFloatingOriginEnabled())
		{
			return;
		}
		bool flag = false;
		IntVector3 intVector = IntVector3.zero;
		float distanceFromOriginBeforeMove = m_DistanceFromOriginBeforeMove;
		if (!m_Locked)
		{
			Vector3 posWorld = Singleton.Manager<ManWorld>.inst.FocalPoint.ScenePosition.SetY(0f);
			if (posWorld.sqrMagnitude > distanceFromOriginBeforeMove * distanceFromOriginBeforeMove)
			{
				intVector = -(Singleton.Manager<ManWorld>.inst.TileManager.WorldToTileCoord(in posWorld).ToVector3XZ() * (int)Singleton.Manager<ManWorld>.inst.TileSize);
				flag = true;
			}
		}
		else if (Singleton.Manager<ManWorld>.inst.FloatingOrigin != m_LockedPos)
		{
			flag = true;
			intVector = Singleton.Manager<ManWorld>.inst.FloatingOrigin - m_LockedPos;
			d.Log($"[ManWorldTreadmill] Pending network move of {intVector} from {Singleton.Manager<ManWorld>.inst.FloatingOrigin} detected");
		}
		if (!flag)
		{
			return;
		}
		if (!m_TileLoadedWaitingForPhysicsSync)
		{
			d.Log($"[ManWorldTreadmill] Queued a world treadmill move of {intVector} from {Singleton.Manager<ManWorld>.inst.FloatingOrigin}");
			QueueMoveWorld(intVector);
			return;
		}
		int num = 2112 - (int)(2f * Singleton.Manager<ManWorld>.inst.TileSize);
		d.Assert((float)num >= distanceFromOriginBeforeMove, "ManWorldTreadmill - Max distance before a world move is forced must be AT LEAST the expected normal treadmill distance or greater!");
		if (Mathf.Abs(intVector.x) >= num || Mathf.Abs(intVector.z) >= num)
		{
			d.Log("[ManWorldTreadmill] Pausing Generation of TileManager for ONE FRAME!");
			Singleton.Manager<ManWorld>.inst.TileManager.PauseGenerationOneFrame();
		}
	}
}
