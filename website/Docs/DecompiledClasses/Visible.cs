#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using cakeslice;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class Visible : MonoBehaviour, ManVisible.StateVisualiser.Provider
{
	public enum Type
	{
		Null,
		Tank,
		Block,
		Resource,
		Pickup
	}

	public struct ConeFiltered
	{
		public Visible visible;

		public float distSq;

		public float angleDot;
	}

	public enum StateFlags
	{
		hasBeenStored = 0,
		hasBeenCollected = 1,
		inBeam = 3,
		notManagedByTile = 4,
		hasMultipleDamageables = 5
	}

	public enum AutoExpireState
	{
		NotUsed,
		Inactive,
		WaitingForTimeout,
		ReadyToDestroy
	}

	public enum LockTimerTypes
	{
		Interactible,
		Grabbable,
		StackAccept,
		BlocksAttachable,
		ItemCollection,
		SendToSCU
	}

	public class WeakReference
	{
		private Visible m_Visible;

		private int m_Id;

		public void Set(Visible visible)
		{
			m_Visible = visible;
			m_Id = (m_Visible.IsNotNull() ? m_Visible.ID : (-1));
		}

		public Visible Get()
		{
			if (m_Visible != null && (m_Visible.ID != m_Id || !m_Visible.gameObject.activeInHierarchy))
			{
				m_Visible = null;
			}
			return m_Visible;
		}
	}

	public ItemTypeInfo m_ItemType;

	[NonSerialized]
	public TileManager.TileCache tileCache;

	public const float k_AutoExpireDelayInvalid = -1f;

	public Event<Visible> RecycledEvent;

	public Event<bool> PhysicsEnabledEvent;

	public Event<bool> HeldEvent;

	public EventNoParams MesheRenderersUpdatedEvent;

	public Event<LockTimerTypes> LockTimeoutSetEvent;

	private GameObject m_GameObject;

	[HideInInspector]
	[SerializeField]
	private ColliderSwapper m_ColliderSwapper;

	[HideInInspector]
	[SerializeField]
	private SwitchableUpdater m_ConditionalUpdater;

	[SerializeField]
	[HideInInspector]
	private WorldSpaceObject m_WorldSpaceComponent;

	[SerializeField]
	[HideInInspector]
	private Outline m_Outline;

	private AutoExpireState m_AutoExpireState;

	private ManTimedEvents.ManagedEvent m_AutoExpireEvent = new ManTimedEvents.ManagedEvent();

	private bool m_IgnoreOneKeepAwake;

	[NonSerialized]
	private ModuleItemHolder.Stack m_HolderStack;

	private bool m_HasReportedNaN;

	private int touchingHolderFrame;

	private float[] m_LockTimeouts = new float[EnumValuesIterator<LockTimerTypes>.Count];

	private static List<ObjectTypes> m_NotAlseepUpdateTypes = new List<ObjectTypes>
	{
		ObjectTypes.Block,
		ObjectTypes.Chunk
	};

	private static List<ObjectTypes> m_AutoExpireUpdateTypes = new List<ObjectTypes>
	{
		ObjectTypes.Block,
		ObjectTypes.Chunk
	};

	private List<Collider> m_ChildrenColliderCache = new List<Collider>();

	private const string kVisibleTag = "_V";

	private Func<bool> RemoveFromGameEvent;

	public static bool DisableAddToTileOnSpawn { get; set; }

	public ObjectTypes type => m_ItemType.ObjectType;

	public int ItemType => m_ItemType.ItemType;

	public int ID { get; private set; }

	public Bitfield<StateFlags> Flags { get; private set; }

	public bool ManagedByTile
	{
		get
		{
			return !Flags.Contains(4);
		}
		set
		{
			Flags.Set(4, !value);
		}
	}

	public bool InBeam
	{
		get
		{
			return Flags.Contains(3);
		}
		set
		{
			Flags.Set(3, value);
		}
	}

	public bool HasMultipleDamageables
	{
		get
		{
			return Flags.Contains(5);
		}
		set
		{
			Flags.Set(5, value);
		}
	}

	public ModuleItemHolder.Stack holderStack => m_HolderStack;

	public bool TouchingHolder
	{
		get
		{
			return Time.frameCount - touchingHolderFrame <= 1;
		}
		private set
		{
			if (value)
			{
				touchingHolderFrame = Time.frameCount;
			}
			else
			{
				touchingHolderFrame = 0;
			}
		}
	}

	public int StackTakeHeartbeat { get; private set; }

	public bool TakenThisHeartbeat
	{
		get
		{
			if ((bool)m_HolderStack.myHolder.block.tank)
			{
				return StackTakeHeartbeat == m_HolderStack.myHolder.block.tank.Holders.HeartbeatCount;
			}
			return false;
		}
	}

	public bool IsPrePickup
	{
		get
		{
			if (m_HolderStack != null && m_HolderStack.isPrePickupFn != null)
			{
				return m_HolderStack.isPrePickupFn(this);
			}
			return false;
		}
	}

	public bool UsePrevHeldPos { get; set; }

	public WorldPosition PrevHeldPos { get; set; }

	public float HeldItemLastCollisionContact { get; set; }

	public int state
	{
		get
		{
			return Flags.Field;
		}
		set
		{
			Flags.SetFlags(value);
		}
	}

	public MeshRenderer[] meshRenderers { get; private set; }

	public Tank tank { get; private set; }

	public Crate crate { get; private set; }

	public TankBlock block { get; private set; }

	public ResourceDispenser resdisp { get; private set; }

	public ResourcePickup pickup { get; private set; }

	public Waypoint Waypoint { get; private set; }

	public Rigidbody rbody
	{
		get
		{
			switch (type)
			{
			case ObjectTypes.Vehicle:
				return tank.rbody;
			case ObjectTypes.Block:
				return block.rbody;
			case ObjectTypes.Chunk:
				return pickup.rbody;
			case ObjectTypes.Scenery:
				d.Assert(GetComponent<Rigidbody>() == null, "Non-NULL rbody on Scenery '" + base.name + "'", base.gameObject);
				return null;
			case ObjectTypes.Waypoint:
				d.Assert(GetComponent<Rigidbody>() == null, "Non-NULL rbody on Waypoint '" + base.name + "'", base.gameObject);
				return null;
			case ObjectTypes.Crate:
				return crate.rbody;
			default:
				d.LogError("shouldn't use the default rigidbody accessor");
				return GetComponent<Rigidbody>();
			}
		}
	}

	public Transform trans { get; private set; }

	public Damageable damageable { get; private set; }

	public ColliderSwapper ColliderSwapper => m_ColliderSwapper;

	public SwitchableUpdater ConditionalUpdater => m_ConditionalUpdater;

	public WorldSpaceObject WorldSpaceComponent => m_WorldSpaceComponent;

	public Outline Outline => m_Outline;

	public Vector3 centrePosition
	{
		get
		{
			Vector3 zero = Vector3.zero;
			zero = type switch
			{
				ObjectTypes.Block => block.centreOfMassWorld, 
				ObjectTypes.Vehicle => tank.boundsCentreWorld, 
				_ => trans.position, 
			};
			if (zero.IsNaN() && !m_HasReportedNaN)
			{
				m_HasReportedNaN = true;
				string text = "Visible " + base.name + " centrePosition is returning NaN";
				d.LogError(type switch
				{
					ObjectTypes.Block => string.Concat(text, "\nBlock COM: ", block.centreOfMassWorld, "\nPos: ", block.trans.position, "\nRot: ", block.trans.rotation, "\nScale: ", block.trans.localScale), 
					ObjectTypes.Vehicle => string.Concat(text, "\nTank COM: ", tank.blockBounds.center, "\nPos: ", tank.trans.position, "\nRot: ", tank.trans.rotation, "\nScale: ", tank.trans.localScale), 
					_ => string.Concat(text, "\nDefault\nPos: ", trans.position, "\nRot: ", trans.rotation, "\nScale: ", trans.localScale), 
				});
				zero = Vector3.zero;
			}
			return zero;
		}
		set
		{
			if (value.IsNaN())
			{
				d.LogError("Visible " + base.name + " centrePosition is being set to NaN");
			}
			d.Assert(value.y > Globals.inst.m_VisibleEmergencyKillHeight);
			switch (type)
			{
			case ObjectTypes.Block:
				block.centreOfMassWorld = value;
				break;
			case ObjectTypes.Vehicle:
				tank.boundsCentreWorld = value;
				break;
			default:
				trans.position = value;
				break;
			}
		}
	}

	public float Radius
	{
		get
		{
			switch (type)
			{
			case ObjectTypes.Vehicle:
				return Mathf.Max(tank.blockBounds.extents.x, tank.blockBounds.extents.z) + 0.5f;
			case ObjectTypes.Block:
			{
				Vector3 extents = block.BlockCellBounds.extents;
				return 0.5f + (extents.x + extents.y + extents.z) * 0.33f;
			}
			case ObjectTypes.Chunk:
				if (!m_ColliderSwapper.IsNotNull())
				{
					return 0.5f;
				}
				return m_ColliderSwapper.SimpleCollider.radius;
			case ObjectTypes.Scenery:
				return resdisp.GroundRadius;
			case ObjectTypes.Waypoint:
				return 1f;
			case ObjectTypes.Crate:
				return crate.Radius;
			default:
				d.Assert(condition: false, "checking radius of object that has no radius");
				return 0.001f;
			}
		}
	}

	public bool isActive
	{
		get
		{
			if (m_GameObject.IsNotNull())
			{
				return m_GameObject.activeSelf;
			}
			return false;
		}
	}

	public bool IsStationary
	{
		get
		{
			switch (type)
			{
			case ObjectTypes.Block:
				d.Assert(m_ConditionalUpdater);
				if (!block.tank)
				{
					return !m_ConditionalUpdater.enabled;
				}
				return false;
			case ObjectTypes.Chunk:
				d.Assert(m_ConditionalUpdater);
				return !m_ConditionalUpdater.enabled;
			case ObjectTypes.Vehicle:
				if (rbody.IsNotNull())
				{
					return rbody.velocity.ApproxZero();
				}
				return false;
			default:
				return true;
			}
		}
	}

	public bool Killed { get; private set; }

	public bool IsInteractible => !IsLocked(LockTimerTypes.Interactible);

	public bool IsGrabbable => !IsLocked(LockTimerTypes.Grabbable);

	public bool CanBlockAttachToTech => !IsLocked(LockTimerTypes.BlocksAttachable);

	public bool CanHoldInStack => !IsLocked(LockTimerTypes.StackAccept);

	public bool CanBeCollected => !IsLocked(LockTimerTypes.ItemCollection);

	public bool CanBeSentToSCU => !IsLocked(LockTimerTypes.SendToSCU);

	public void ForceAsKilled()
	{
		Killed = true;
	}

	public static Type ParseType(string typeString)
	{
		try
		{
			return (Type)Enum.Parse(typeof(Type), typeString);
		}
		catch (ArgumentException)
		{
			return Type.Null;
		}
	}

	public Vector3 GetAimPoint(Vector3 origin)
	{
		if (type == ObjectTypes.Vehicle && tank.IsNotNull())
		{
			return tank.control.GetWeaponTargetLocation(origin);
		}
		return centrePosition;
	}

	public bool IsVisibleFrom(Vector3 fromPoint, Vector3 forward, float range, float angle, bool ignoreY, Func<Visible, bool> ignorePredicate)
	{
		float num = Mathf.Cos((float)Math.PI / 180f * angle * 0.5f);
		float num2 = range * range;
		if (ignorePredicate != null && ignorePredicate(this))
		{
			return false;
		}
		Vector3 vector = centrePosition - fromPoint;
		if (ignoreY)
		{
			vector.y = 0f;
		}
		float sqrMagnitude = vector.sqrMagnitude;
		if ((type == ObjectTypes.Vehicle) ? (!tank.BoundsIntersectSphere(fromPoint, range)) : (sqrMagnitude > num2))
		{
			return false;
		}
		float num3;
		if (ignoreY)
		{
			Vector3 normalized = new Vector3(forward.x, 0f, forward.z).normalized;
			Vector3 normalized2 = new Vector3(vector.x, 0f, vector.z).normalized;
			num3 = Vector3.Dot(normalized, normalized2);
		}
		else
		{
			num3 = Vector3.Dot(forward.normalized, vector.normalized);
		}
		if (num3 < num)
		{
			return false;
		}
		return true;
	}

	public static void ConeFilterOptim(ManVisible.CachedSearchIterator visibles, List<ConeFiltered> results, float range, float angle, Vector3 centre, Vector3 forward, bool ignoreY, Func<Visible, bool> ignorePredicate)
	{
		float num = Mathf.Cos((float)Math.PI / 180f * angle * 0.5f);
		float num2 = range * range;
		Vector3 normalized = forward.SetY(0f).normalized;
		foreach (Visible visible in visibles)
		{
			if (ignorePredicate != null && ignorePredicate(visible))
			{
				continue;
			}
			Vector3 input = visible.centrePosition - centre;
			if (ignoreY)
			{
				input.y = 0f;
			}
			float sqrMagnitude = input.sqrMagnitude;
			if (!((visible.type == ObjectTypes.Vehicle) ? (!visible.tank.BoundsIntersectSphere(centre, range)) : (sqrMagnitude > num2)))
			{
				float num3;
				if (ignoreY)
				{
					Vector3 normalized2 = input.SetY(0f).normalized;
					num3 = Vector3.Dot(normalized, normalized2);
				}
				else
				{
					num3 = Vector3.Dot(forward.normalized, input.normalized);
				}
				if (!(num3 < num))
				{
					results.Add(new ConeFiltered
					{
						visible = visible,
						distSq = sqrMagnitude,
						angleDot = num3
					});
				}
			}
		}
	}

	public void GetDamageablesInChildren(List<Damageable> result)
	{
		if (HasMultipleDamageables)
		{
			GetComponentsInChildren(result);
			return;
		}
		result.Clear();
		result.Add(damageable);
	}

	public bool HasCollider(Collider col)
	{
		for (int i = 0; i < m_ChildrenColliderCache.Count; i++)
		{
			if (m_ChildrenColliderCache[i] == col)
			{
				return true;
			}
		}
		return false;
	}

	public void SaveForStorage(ManSaveGame.StoredVisible sv)
	{
		sv.m_Position = Vector3.zero;
		sv.m_WorldPosition = WorldPosition.FromScenePosition(centrePosition);
		if (centrePosition.IsNaN())
		{
			d.LogError("Saving Visible " + base.name + " for storage - CentrePos is NaN. Using transform pos instead");
			sv.m_WorldPosition = WorldPosition.FromScenePosition(trans.position);
		}
		sv.m_Rotation = trans.rotation;
		sv.m_TrackStates = state;
		if (rbody != null)
		{
			sv.m_Velocity = rbody.velocity;
			sv.m_AngularVelocity = rbody.angularVelocity;
		}
		sv.m_InSafeArea = false;
		sv.m_ExpireState = m_AutoExpireState;
		if (m_HolderStack != null || m_AutoExpireState == AutoExpireState.NotUsed || m_AutoExpireState == AutoExpireState.Inactive)
		{
			sv.m_ExpireTimeout = -1f;
		}
		else
		{
			float num = 0f;
			if (m_AutoExpireState == AutoExpireState.WaitingForTimeout)
			{
				DebugUtil.AssertRelease(m_AutoExpireEvent != null, "Visible.GetExpireTimeRelative " + base.name + " has null m_AutoExpireEvent");
				if (m_AutoExpireEvent != null)
				{
					d.Assert(m_AutoExpireEvent.TimeRemaining >= 0f);
					num = m_AutoExpireEvent.TimeRemaining;
				}
				if (Singleton.Manager<ManFreeSpace>.inst.IsSafeArea(sv.m_WorldPosition.ScenePosition))
				{
					sv.m_InSafeArea = true;
				}
			}
			sv.m_ExpireTimeout = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() + num;
		}
		sv.m_Asleep = m_ConditionalUpdater.IsNotNull() && !m_ConditionalUpdater.enabled;
		sv.m_ID = ID;
		sv.m_HasBeenSpawned = true;
		sv.m_WorldSpaceComponentInactive = WorldSpaceComponent.IsNull() || !WorldSpaceComponent.IsEnabled;
		sv.m_TileOverlapDirection = tileCache.tileOverlapDirection;
	}

	public static int InitStored(ManSaveGame.StoredVisible sv, Vector3 position, Quaternion rotation, float radius = 0f)
	{
		sv.m_Position = Vector3.zero;
		sv.m_WorldPosition = WorldPosition.FromScenePosition(in position);
		sv.m_Rotation = rotation;
		sv.m_Velocity = Vector3.zero;
		sv.m_AngularVelocity = Vector3.zero;
		sv.m_ExpireTimeout = -1f;
		sv.m_ExpireState = AutoExpireState.NotUsed;
		sv.m_InSafeArea = false;
		sv.m_Asleep = false;
		sv.m_HasBeenSpawned = false;
		sv.m_WorldSpaceComponentInactive = false;
		if (radius > 0f)
		{
			sv.m_TileOverlapDirection = Singleton.Manager<ManWorld>.inst.TileManager.GetTileOverlapDirection(sv.m_WorldPosition, radius);
		}
		else
		{
			sv.m_TileOverlapDirection = IntVector2.zero;
		}
		return sv.m_ID;
	}

	public void RestoreSaved(ManSaveGame.StoredVisible sv, bool restoreTransform = true)
	{
		state = sv.m_TrackStates;
		if (restoreTransform)
		{
			trans.rotation = sv.m_Rotation;
			centrePosition = sv.GetBackwardsCompatiblePosition();
			if (centrePosition.IsNaN())
			{
				d.LogError("Restore Visible " + base.name + " from save - CentrePos is NaN");
			}
		}
		if (rbody.IsNotNull())
		{
			if (!rbody.isKinematic)
			{
				rbody.velocity = sv.m_Velocity;
				rbody.angularVelocity = sv.m_AngularVelocity;
			}
			else
			{
				d.Assert((type == ObjectTypes.Vehicle || type == ObjectTypes.Crate) && ((Vector3)sv.m_Velocity).sqrMagnitude <= 0.001f && ((Vector3)sv.m_AngularVelocity).sqrMagnitude <= 0.001f, $"restoring kinematic visible {base.name}: type {type} velocity {sv.m_Velocity} angularV {sv.m_AngularVelocity}");
			}
		}
		if ((bool)m_ConditionalUpdater && sv.m_Asleep)
		{
			m_ConditionalUpdater.enabled = false;
			m_IgnoreOneKeepAwake = true;
			if ((bool)m_ColliderSwapper)
			{
				m_ColliderSwapper.UseSimple(m_HolderStack != null && !m_HolderStack.myHolder.IsFlag(ModuleItemHolder.Flags.NoSimpleCollider));
			}
			if (sv.m_ExpireTimeout == -1f)
			{
				if (sv.m_ExpireState == AutoExpireState.NotUsed)
				{
					m_AutoExpireState = AutoExpireState.NotUsed;
				}
				else
				{
					m_AutoExpireState = AutoExpireState.Inactive;
				}
			}
			else
			{
				float num = 0f;
				float currentModeRunningTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
				num = (sv.m_InSafeArea ? Singleton.Manager<ManGameMode>.inst.GetAutoExpireDelay(type) : ((!(currentModeRunningTime > sv.m_ExpireTimeout)) ? (sv.m_ExpireTimeout - currentModeRunningTime) : 0f));
				m_AutoExpireState = AutoExpireState.WaitingForTimeout;
				m_AutoExpireEvent.Set(num, OnAutoExpireTimeout);
			}
		}
		else
		{
			if (sv.m_ExpireState == AutoExpireState.NotUsed)
			{
				m_AutoExpireState = AutoExpireState.NotUsed;
			}
			else
			{
				m_AutoExpireState = AutoExpireState.Inactive;
			}
			d.Assert(sv.m_ExpireTimeout == -1f || sv.m_ExpireTimeout == 0f, "Visible.RestoredSaved(): restored non-sleeping tank with non-zero timeout");
		}
		if (WorldSpaceComponent.IsNotNull() && sv.m_WorldSpaceComponentInactive)
		{
			WorldSpaceComponent.SetEnabled(enabled: false);
		}
		ID = sv.m_ID;
	}

	public void KeepAwake()
	{
		if (m_IgnoreOneKeepAwake)
		{
			m_IgnoreOneKeepAwake = false;
		}
		else if (m_HolderStack == null)
		{
			EnableConditionalUpdater();
		}
	}

	public void Teleport(Vector3 position, Quaternion rotation, bool tankGrounded = true, bool stop = true)
	{
		if (rbody.IsNotNull() && !rbody.isKinematic && stop)
		{
			rbody.velocity = Vector3.zero;
			rbody.angularVelocity = Vector3.zero;
		}
		trans.rotation = rotation;
		if (type == ObjectTypes.Vehicle)
		{
			bool isAnchored = tank.IsAnchored;
			if (isAnchored)
			{
				tank.Anchors.UnanchorAll(playAnim: false);
			}
			Vector3 worldPos = (tankGrounded ? (Singleton.Manager<ManWorld>.inst.ProjectToGround(position, hitScenery: true) + Vector3.up) : position);
			Vector3 position2 = trans.position;
			tank.PositionBaseCentred(worldPos);
			Vector3 vector = trans.position - position2;
			TechHolders.HolderIterator enumerator = tank.Holders.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ModuleItemHolder.Stack.ItemIterator enumerator2 = enumerator.Current.Contents.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Visible current = enumerator2.Current;
					if (!current.IsPrePickup && current.WorldSpaceComponent.IsEnabled)
					{
						current.Teleport(current.centrePosition + vector, current.trans.rotation);
					}
				}
			}
			if (!Physics.autoSyncTransforms)
			{
				Physics.SyncTransforms();
			}
			if (isAnchored)
			{
				tank.Anchors.TryAnchorAll(moveTech: true);
			}
			tank.ResetEvent.Send(0);
		}
		else
		{
			centrePosition = position;
		}
		Singleton.Manager<ManWorld>.inst.TileManager.UpdateTileCache(this, forceNow: true);
	}

	public void StopManagingVisible()
	{
		SetManagedByTile(managed: false);
		if (m_ConditionalUpdater.IsNotNull())
		{
			m_ConditionalUpdater.enabled = false;
		}
		m_AutoExpireEvent.Clear();
	}

	public void SetManagedByTile(bool managed)
	{
		if (managed != ManagedByTile)
		{
			ManagedByTile = managed;
			if (managed)
			{
				Singleton.Manager<ManWorld>.inst.TileManager.UpdateTileCache(this, forceNow: true);
				return;
			}
			Singleton.Manager<ManWorld>.inst.TileManager.RemoveTileCache(this);
			tileCache.Reset();
		}
	}

	public void EnableAutoExpire(bool enable)
	{
		if (!ManNetwork.IsHost)
		{
			return;
		}
		if (NotAlseepUpdateByDefault() && AutoExpireByDefault())
		{
			if (enable)
			{
				m_AutoExpireState = AutoExpireState.Inactive;
			}
			else
			{
				m_AutoExpireState = AutoExpireState.NotUsed;
				m_AutoExpireEvent.Clear();
			}
			m_ConditionalUpdater.enabled = enable;
		}
		else
		{
			d.LogError("Visible.EnableAutoExpire called on Visible " + base.name + " which doesn't AutoExpire");
		}
	}

	public static Visible FindVisibleUpwards(Component c)
	{
		Component component = c.MatchTagInThisOrParents("_V");
		if (!component.IsNotNull())
		{
			return null;
		}
		return (component as Visible) ?? component.GetComponent<Visible>();
	}

	public static T FindComponentOnVisibleUpwards<T>(Component c) where T : Component
	{
		Component component = c.MatchTagInThisOrParents("_V");
		if (!component.IsNotNull())
		{
			return null;
		}
		return component.GetComponent<T>();
	}

	public static T FindComponentOnVisibleOrChild<T>(Component c) where T : Component
	{
		Component component = c.MatchTagInThisOrParents("_V");
		if (!component.IsNotNull())
		{
			return null;
		}
		return component.GetComponentInChildren<T>();
	}

	public void EnablePhysics(bool enable, bool disableWithTrigger = false)
	{
		if (rbody.IsNotNull())
		{
			if (tank.IsNotNull())
			{
				tank.EnableGravity = enable;
			}
			else
			{
				rbody.useGravity = enable;
			}
			if (!enable)
			{
				rbody.velocity = Vector3.zero;
				rbody.angularVelocity = Vector3.zero;
			}
		}
		if (m_ColliderSwapper.IsNotNull())
		{
			m_ColliderSwapper.EnableCollision(enable, disableWithTrigger);
		}
		PhysicsEnabledEvent.Send(enable);
	}

	public NetBlockChunk GetNetBlockChunk()
	{
		if (!block.IsNotNull())
		{
			if (!pickup.IsNotNull())
			{
				return null;
			}
			return pickup.netChunk;
		}
		return block.netBlock;
	}

	public void SetHolder(ModuleItemHolder.Stack stack, bool notifyRelease = true, bool isBeingRecycled = false, bool netSend = true)
	{
		bool flag = m_HolderStack != null;
		if (stack != m_HolderStack)
		{
			Tank tank = (flag ? m_HolderStack.myHolder.block.tank : null);
			Tank tank2 = stack?.myHolder.block.tank;
			if (flag)
			{
				if (notifyRelease)
				{
					m_HolderStack.Release(this, stack);
				}
				if ((stack == null || stack.myHolder != m_HolderStack.myHolder) && (bool)tank)
				{
					tank.Holders.ClearItemPickupTime(ID);
				}
				if (tank.IsNotNull() && (object)tank != tank2)
				{
					tank.Holders.ItemReleaseEvent.Send(tank, this);
				}
			}
			InBeam = false;
			if (stack != null)
			{
				if ((m_HolderStack == null || stack.myHolder != m_HolderStack.myHolder) && (bool)tank2)
				{
					tank2.Holders.SetItemPickupTime(ID);
				}
				StackTakeHeartbeat = tank2.Holders.HeartbeatCount;
				if (flag)
				{
					UsePrevHeldPos = true;
					PrevHeldPos = WorldPosition.FromScenePosition(centrePosition);
				}
				if (tank2.IsNotNull() && (object)tank != tank2)
				{
					tank2.Holders.ItemPickupEvent.Send(tank2, this);
				}
				if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && netSend)
				{
					NetBlockChunk netBlockChunk = GetNetBlockChunk();
					if (netBlockChunk.IsNotNull())
					{
						netBlockChunk.SetHolderBlock(stack.myHolder.block, stack.GetStackIndex(), stack.items.IndexOf(this));
					}
				}
			}
			else
			{
				StackTakeHeartbeat = 0;
				UsePrevHeldPos = false;
				if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && netSend && !isBeingRecycled)
				{
					NetBlockChunk netBlockChunk2 = GetNetBlockChunk();
					if (netBlockChunk2.IsNotNull())
					{
						netBlockChunk2.ReleaseFromHolder();
					}
				}
			}
		}
		m_HolderStack = stack;
		TouchingHolder = false;
		if (m_ColliderSwapper.IsNotNull())
		{
			m_ColliderSwapper.UseSimple(m_HolderStack != null && !m_HolderStack.myHolder.IsFlag(ModuleItemHolder.Flags.NoSimpleCollider));
		}
		if (m_HolderStack != null)
		{
			StopManagingVisible();
			HeldEvent.Send(paramA: true);
		}
		else
		{
			if (!flag)
			{
				return;
			}
			HeldEvent.Send(paramA: false);
			if (!isBeingRecycled)
			{
				SetManagedByTile(managed: true);
				if (m_ConditionalUpdater.IsNotNull())
				{
					bool forceSetExpireInactiveIfUsed = true;
					EnableConditionalUpdater(forceSetExpireInactiveIfUsed);
				}
				if (pickup.IsNotNull() && pickup.rbody.IsNull())
				{
					pickup.InitRigidbody();
				}
				if (block.IsNotNull() && block.rbody.IsNull())
				{
					block.InitRigidbody();
				}
			}
		}
	}

	public bool GetNetInstanceID(out NetworkInstanceId netId)
	{
		bool result = false;
		netId = NetworkInstanceId.Invalid;
		switch (type)
		{
		case ObjectTypes.Vehicle:
			if (tank.netTech.IsNotNull())
			{
				netId = tank.netTech.netId;
				result = true;
			}
			break;
		case ObjectTypes.Block:
			if (block.netBlock.IsNotNull())
			{
				netId = block.netBlock.netId;
				result = true;
			}
			break;
		case ObjectTypes.Chunk:
			if (pickup.netChunk.IsNotNull())
			{
				netId = pickup.netChunk.netId;
				result = true;
			}
			break;
		default:
			d.LogError(string.Concat("Visible.GetNetInstanceID: Type ", type, " is not currently supported"));
			break;
		}
		return result;
	}

	public bool KillVolumeCheck()
	{
		if (trans.position.y < Globals.inst.m_VisibleEmergencyKillHeight)
		{
			if (type == ObjectTypes.Vehicle)
			{
				return true;
			}
			bool flag = true;
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				if (type == ObjectTypes.Block)
				{
					flag = block.netBlock.IsNull() || (block.netBlock.IsNotNull() && Singleton.Manager<ManNetwork>.inst.IsServer);
				}
				else if (type == ObjectTypes.Chunk)
				{
					flag = pickup.netChunk.IsNull() || (pickup.netChunk.IsNotNull() && Singleton.Manager<ManNetwork>.inst.IsServer);
				}
				else if (type == ObjectTypes.Crate)
				{
					flag = crate.netCrate.IsNull() || (crate.netCrate.IsNotNull() && Singleton.Manager<ManNetwork>.inst.IsServer);
				}
			}
			if (flag)
			{
				d.LogWarning("Emergency killvolume: " + base.name + " VisibleID=" + ID + " Type=" + type);
				d.Assert(type == ObjectTypes.Block || type == ObjectTypes.Chunk || type == ObjectTypes.Crate, $"Object of type {type} hit killvolume");
				return true;
			}
		}
		return false;
	}

	public void RegisterRemovalCallback(Func<bool> callback)
	{
		RemoveFromGameEvent = null;
		RemoveFromGameEvent = callback;
	}

	public void RemoveFromGame()
	{
		bool flag = true;
		if (RemoveFromGameEvent != null)
		{
			flag = RemoveFromGameEvent();
		}
		Killed = true;
		if (flag)
		{
			trans.Recycle();
		}
	}

	public void ServerDestroy()
	{
		switch (type)
		{
		case ObjectTypes.Block:
			Singleton.Manager<ManLooseBlocks>.inst.RequestDespawnBlock(block, DespawnReason.Host);
			break;
		case ObjectTypes.Chunk:
			Singleton.Manager<ManLooseBlocks>.inst.HostDestroyChunk(pickup);
			break;
		case ObjectTypes.Vehicle:
			if (tank.netTech.IsNotNull())
			{
				d.Assert(tank.netTech.RequestRemoveFromGame(tank.netTech.NetPlayer, wasKilled: false), "ServerRemoveFromUnloadedTile: Vehicle " + base.name + " not OK to remove from game");
				trans.Recycle();
			}
			break;
		default:
			if (GetComponent<NetworkIdentity>().IsNotNull())
			{
				NetworkServer.UnSpawn(base.gameObject);
			}
			trans.Recycle();
			break;
		}
	}

	public void SetLockTimout(LockTimerTypes type, float additionalTime = 1f)
	{
		float time = Time.time;
		bool num = m_LockTimeouts[(int)type] > time;
		m_LockTimeouts[(int)type] = time + additionalTime;
		if (!num)
		{
			LockTimeoutSetEvent.Send(type);
		}
	}

	public bool IsLocked(LockTimerTypes type)
	{
		return m_LockTimeouts[(int)type] > Time.time;
	}

	public float GetLockedRemainingDuration(LockTimerTypes type)
	{
		return Mathf.Max(m_LockTimeouts[(int)type] - Time.time, 0f);
	}

	public void CheckTouchingHolder(Collision collision)
	{
		Rigidbody rigidbody = m_HolderStack.myHolder.block.tank.rbody;
		ContactPoint[] contacts = collision.contacts;
		foreach (ContactPoint contactPoint in contacts)
		{
			if ((object)contactPoint.otherCollider.attachedRigidbody == rigidbody)
			{
				TouchingHolder = true;
				break;
			}
		}
	}

	public void MoveAboveGround()
	{
		Vector3 scenePos = centrePosition;
		Vector3 vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos);
		float num = ((type == ObjectTypes.Vehicle) ? (tank.blockBounds.extents.y + 0.5f) : Radius);
		float num2 = scenePos.y - vector.y;
		if (num2 < num)
		{
			scenePos.y += num - num2;
			centrePosition = scenePos;
		}
	}

	public void RefreshMeshRendererList()
	{
		meshRenderers = GetComponentsInChildren<MeshRenderer>(includeInactive: true);
		MesheRenderersUpdatedEvent.Send();
	}

	public void SetCollidersEnabled(bool enabled)
	{
		for (int i = 0; i < m_ChildrenColliderCache.Count; i++)
		{
			m_ChildrenColliderCache[i].enabled = enabled;
		}
	}

	public void EnableOutlineGlow(bool enable, Outline.OutlineEnableReason reason)
	{
		if (type == ObjectTypes.Vehicle)
		{
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tank.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.visible.Outline.EnableOutline(enable, reason);
			}
		}
		else
		{
			Outline.EnableOutline(enable, reason);
		}
	}

	private void BuildChildrenColliderCache()
	{
		d.Assert(m_ChildrenColliderCache.Count == 0, "RebuildChildrenColliderCache - Children collider cache was not empty! Make sure to call ClearChildrenColliderCache first!");
		GetComponentsInChildren(includeInactive: true, m_ChildrenColliderCache);
		if (m_ColliderSwapper.IsNotNull())
		{
			ColliderSwapper.ColliderSwapperEntry[] allColliders = m_ColliderSwapper.AllColliders;
			for (int i = 0; i < allColliders.Length; i++)
			{
				ColliderSwapper.ColliderSwapperEntry colliderSwapperEntry = allColliders[i];
				m_ChildrenColliderCache.Remove(colliderSwapperEntry.collider);
			}
			m_ChildrenColliderCache.Remove(m_ColliderSwapper.SimpleCollider);
		}
		foreach (Collider item in m_ChildrenColliderCache)
		{
			Singleton.Manager<ManVisible>.inst.RegisterColliderToVisibleLookup(this, item);
		}
	}

	private void ClearChildrenColliderCache()
	{
		foreach (Collider item in m_ChildrenColliderCache)
		{
			Singleton.Manager<ManVisible>.inst.UnregisterColliderToVisibleLookup(item);
		}
		m_ChildrenColliderCache.Clear();
	}

	void ManVisible.StateVisualiser.Provider.Draw(Vector2 screenPos, Bitfield<DebugSettings.VisibleDebugFlags> flags)
	{
		if (flags.Contains(0))
		{
			screenPos.y -= 10f;
			string text = null;
			switch (m_AutoExpireState)
			{
			case AutoExpireState.WaitingForTimeout:
				text = ((int)m_AutoExpireEvent.TimeRemaining).ToString();
				break;
			case AutoExpireState.ReadyToDestroy:
				text = "Expired";
				break;
			}
			if (TouchingHolder)
			{
				text = "Touching";
			}
			if (text != null)
			{
				DebugGui.LabelScreen(text, new Color(1f, 0.7f, 0f, 0.5f), screenPos);
			}
		}
		if (m_HolderStack != null)
		{
			if (flags.Contains(1))
			{
				DebugGui.LabelWorld($"{m_HolderStack.myHolder.name}:{m_HolderStack.GetStackIndex()}", Color.green, trans.position);
			}
			if (flags.Contains(2))
			{
				m_HolderStack.myHolder.block.tank.IsNotNull();
			}
		}
	}

	private void OnResourceDispDamageStageChanged(ResourceDispenser resourceDisp)
	{
		d.Assert((object)resdisp == resourceDisp, "Raised event from resourceDisp that doesn't belong to this visible. Something has gone horribly wrong");
		ClearChildrenColliderCache();
		BuildChildrenColliderCache();
		if (Outline.IsNotNull())
		{
			Outline.OnMeshesUpdated();
		}
	}

	private void OnUpdateNotAsleep()
	{
		bool flag = false;
		bool flag2 = false;
		if (block.IsNotNull() && block.tank.IsNotNull())
		{
			flag = true;
			m_AutoExpireState = AutoExpireState.Inactive;
			if (m_AutoExpireEvent.IsSet)
			{
				d.LogWarning(base.name + " was attached while m_AutoExpireEvent is set");
				m_AutoExpireEvent.Clear();
			}
		}
		else if (GetNetBlockChunk().IsNull() || ManNetwork.IsHost)
		{
			if ((!flag2 && m_AutoExpireState != AutoExpireState.NotUsed) || m_HolderStack != null)
			{
				flag2 = UpdateAutoExpire();
			}
			if (!flag2)
			{
				flag2 = KillVolumeCheck();
			}
			if (flag2)
			{
				if (block.IsNotNull())
				{
					d.Assert(ManNetwork.IsHost, "Can't remove NetBlock from client timeout");
					Singleton.Manager<ManLooseBlocks>.inst.HostDestroyBlock(block);
				}
				else if (pickup.IsNotNull())
				{
					d.Assert(ManNetwork.IsHost, "Can't remove NetChunk from client timeout");
					Singleton.Manager<ManLooseBlocks>.inst.HostDestroyChunk(pickup);
				}
				else
				{
					RemoveFromGame();
				}
			}
			else
			{
				Singleton.Manager<ManWorld>.inst.TileManager.UpdateTileCache(this);
			}
		}
		if (flag || IsRigidbodySleeping())
		{
			m_ConditionalUpdater.enabled = false;
		}
	}

	private bool IsRigidbodySleeping()
	{
		if (rbody.IsNotNull())
		{
			return rbody.IsSleeping();
		}
		return false;
	}

	private bool NotAlseepUpdateByDefault()
	{
		return m_NotAlseepUpdateTypes.Contains(type);
	}

	private bool AutoExpireByDefault()
	{
		return m_AutoExpireUpdateTypes.Contains(type);
	}

	private void EnableConditionalUpdater(bool forceSetExpireInactiveIfUsed = false)
	{
		if (ManNetwork.IsHost)
		{
			m_ConditionalUpdater.enabled = true;
			if (m_AutoExpireState != AutoExpireState.NotUsed && (m_AutoExpireState == AutoExpireState.WaitingForTimeout || forceSetExpireInactiveIfUsed))
			{
				m_AutoExpireEvent.Clear();
				m_AutoExpireState = AutoExpireState.Inactive;
			}
		}
	}

	private bool UpdateAutoExpire()
	{
		if (m_AutoExpireState == AutoExpireState.ReadyToDestroy)
		{
			Vector3 vector = centrePosition - Singleton.cameraTrans.position;
			bool num = Singleton.Manager<ManGameMode>.inst.AutoExpireIfOffScreen();
			float num2 = Singleton.Manager<ManGameMode>.inst.GetAutoExpireRange() * Singleton.Manager<ManGameMode>.inst.GetAutoExpireRange();
			if ((num && Vector3.Dot(Singleton.cameraTrans.forward, vector.normalized) < 0f) || vector.sqrMagnitude > num2)
			{
				return true;
			}
		}
		else
		{
			if (m_AutoExpireState == AutoExpireState.WaitingForTimeout || m_AutoExpireEvent.IsSet)
			{
				m_AutoExpireEvent.Clear();
				if (m_AutoExpireState == AutoExpireState.Inactive)
				{
					d.LogWarning(base.name + " has m_AutoExpireEvent set while AutoExpireState.Inactive");
				}
			}
			m_AutoExpireState = AutoExpireState.Inactive;
			if (IsRigidbodySleeping())
			{
				float autoExpireDelay = Singleton.Manager<ManGameMode>.inst.GetAutoExpireDelay(type);
				if (autoExpireDelay != -1f)
				{
					m_AutoExpireState = AutoExpireState.WaitingForTimeout;
					m_AutoExpireEvent.Set(autoExpireDelay, OnAutoExpireTimeout);
				}
			}
		}
		return false;
	}

	private void OnAutoExpireTimeout()
	{
		d.Assert(isActive, "OnAutoExpireTimeout called on '" + base.name + "' while object was not active!");
		d.Assert(m_AutoExpireState == AutoExpireState.WaitingForTimeout, "OnAutoExpireTimeout called on '" + base.name + "' while autoExpireState was not set to WaitingForTimeout! (Was '" + m_AutoExpireState.ToString() + "')");
		if (!Singleton.Manager<ManFreeSpace>.inst.IsSafeArea(trans.position))
		{
			m_AutoExpireState = AutoExpireState.ReadyToDestroy;
			m_ConditionalUpdater.enabled = true;
			return;
		}
		m_ConditionalUpdater.enabled = false;
		float autoExpireDelay = Singleton.Manager<ManGameMode>.inst.GetAutoExpireDelay(type);
		if (autoExpireDelay != -1f)
		{
			m_AutoExpireState = AutoExpireState.WaitingForTimeout;
			m_AutoExpireEvent.Reset(autoExpireDelay);
		}
	}

	public Visible()
	{
		Flags = new Bitfield<StateFlags>();
	}

	private void PrePool()
	{
		if (type == ObjectTypes.Block || type == ObjectTypes.Chunk)
		{
			m_ColliderSwapper = GetComponent<ColliderSwapper>();
			if (m_ColliderSwapper.IsNull())
			{
				m_ColliderSwapper = base.gameObject.AddComponent<ColliderSwapper>();
			}
		}
		if (m_NotAlseepUpdateTypes.Contains(type))
		{
			m_ConditionalUpdater = base.gameObject.AddComponent<SwitchableUpdater>();
		}
		if (type != ObjectTypes.Scenery)
		{
			m_WorldSpaceComponent = base.gameObject.AddComponent<WorldSpaceObject>();
		}
		if (type != ObjectTypes.Vehicle && type != ObjectTypes.Waypoint)
		{
			m_Outline = base.gameObject.AddComponent<Outline>();
		}
	}

	private void OnPool()
	{
		base.tag = "_V";
		trans = base.transform;
		m_GameObject = base.gameObject;
		damageable = GetComponent<Damageable>();
		if (m_ConditionalUpdater.IsNotNull())
		{
			m_ConditionalUpdater.UpdateEvent.Subscribe(OnUpdateNotAsleep);
		}
		switch (type)
		{
		case ObjectTypes.Vehicle:
			tank = GetComponent<Tank>();
			break;
		case ObjectTypes.Crate:
			crate = GetComponent<Crate>();
			break;
		case ObjectTypes.Block:
			block = GetComponent<TankBlock>();
			break;
		case ObjectTypes.Chunk:
			pickup = GetComponent<ResourcePickup>();
			break;
		case ObjectTypes.Scenery:
			resdisp = GetComponent<ResourceDispenser>();
			break;
		case ObjectTypes.Waypoint:
			Waypoint = GetComponent<Waypoint>();
			break;
		case ObjectTypes.Null:
			Debug.LogError("Null type for Visible object: " + base.name);
			break;
		default:
			d.LogError($"Not implemented type {type} for Visible object: {base.name}");
			break;
		}
		if (m_ColliderSwapper.IsNotNull())
		{
			m_ColliderSwapper.RegisterVisibleColliders();
		}
		BuildChildrenColliderCache();
		if (resdisp.IsNotNull())
		{
			resdisp.ResourceGiverDamageStageChangedEvent.Subscribe(OnResourceDispDamageStageChanged);
		}
	}

	private void OnSpawn()
	{
		SetHolder(null, notifyRelease: true, isBeingRecycled: false, netSend: false);
		TouchingHolder = false;
		m_IgnoreOneKeepAwake = false;
		Flags.Clear();
		tileCache.Reset();
		EnablePhysics(enable: true);
		if (type == ObjectTypes.Scenery)
		{
			IntVector2 tileCoord = Singleton.Manager<ManWorld>.inst.TileManager.CurrentPopulatingPatchTile?.Coord ?? Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(trans.position);
			ID = Singleton.Manager<ManSaveGame>.inst.CurrentState.GetNextSceneryVisibleID(tileCoord);
		}
		else
		{
			ID = Singleton.Manager<ManSaveGame>.inst.CurrentState.GetNextVisibleID(type);
		}
		m_HasReportedNaN = false;
		if (rbody.IsNotNull())
		{
			if (rbody.velocity != Vector3.zero)
			{
				rbody.velocity = Vector3.zero;
			}
			if (rbody.angularVelocity != Vector3.zero)
			{
				rbody.angularVelocity = Vector3.zero;
			}
		}
		if (!DisableAddToTileOnSpawn && tank.IsNull())
		{
			Singleton.Manager<ManWorld>.inst.TileManager.UpdateTileCache(this, forceNow: true);
			if (type == ObjectTypes.Scenery)
			{
				ManagedByTile = false;
				if (m_ConditionalUpdater.IsNotNull())
				{
					m_ConditionalUpdater.enabled = false;
				}
			}
		}
		if (NotAlseepUpdateByDefault())
		{
			if (ManNetwork.IsHost)
			{
				m_ConditionalUpdater.enabled = true;
				if (AutoExpireByDefault())
				{
					m_AutoExpireState = AutoExpireState.Inactive;
				}
				else
				{
					m_AutoExpireState = AutoExpireState.NotUsed;
				}
			}
			else
			{
				m_AutoExpireState = AutoExpireState.NotUsed;
			}
		}
		else
		{
			m_AutoExpireState = AutoExpireState.NotUsed;
		}
		RefreshMeshRendererList();
		Killed = false;
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManStatusEffects>.inst.ClearAllEffectsOnVisible(this);
		d.Assert(GetNetBlockChunk() == null, $"Recycling block/chunk {base.name} ID={ID} while it still has a NetBlockChunk associated");
		SetHolder(null, notifyRelease: true, isBeingRecycled: true);
		if (m_ConditionalUpdater.IsNotNull())
		{
			m_ConditionalUpdater.enabled = false;
		}
		m_AutoExpireEvent.Clear();
		m_AutoExpireState = AutoExpireState.NotUsed;
		Flags.Clear();
		Singleton.Manager<ManWorld>.inst.TileManager.RemoveTileCache(this);
		Singleton.Manager<ManVisible>.inst.ClearParticles(this);
		RecycledEvent.Send(this);
		RecycledEvent.EnsureNoSubscribers();
	}

	private void OnDepool()
	{
		if (m_ColliderSwapper.IsNotNull())
		{
			m_ColliderSwapper.UnregisterVisibleColliders();
		}
		ClearChildrenColliderCache();
		m_GameObject = null;
		tank = null;
		crate = null;
		block = null;
		pickup = null;
		resdisp = null;
		Waypoint = null;
	}

	private void OnDrawGizmos()
	{
		if (base.gameObject.EditorSelectedSingle() && Application.isPlaying)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(centrePosition, Radius);
		}
	}
}
