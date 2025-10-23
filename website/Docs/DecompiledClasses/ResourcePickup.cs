#define UNITY_EDITOR
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(Visible))]
public class ResourcePickup : MonoBehaviour, IGravityAdjustmentTarget, IGravityApplicationTarget
{
	private enum CollisionCatcherTypes
	{
		None,
		Held,
		Loose
	}

	[FormerlySerializedAs("maxNonSolidTime")]
	[SerializeField]
	private float m_MaxNonSolidTime = 1f;

	[SerializeField]
	private ChunkRarity m_ChunkRarity;

	private float spawnTime;

	private Vector3 m_TriggerOverlap;

	private Renderer m_MyRenderer;

	private bool m_TriggerCatcherSubscribed;

	private CollisionCatcher m_CollisionCatcher;

	private float m_GravityScale = 1f;

	private float m_Mass;

	private static readonly Vector3 s_DefaultOverlapValue = new Vector3(0f, 1f, 0f);

	private static readonly Vector3 s_OnSpawnValue = new Vector3(0f, -1f, 0f);

	private bool m_GravityAdjustmentTouched;

	private bool m_GravityApplicationTouched;

	private uint m_BlockPoolID = uint.MaxValue;

	private CollisionCatcherTypes m_CurrentCollisionType;

	private static bool m_EnablePhysicsOnCollision = false;

	public uint blockPoolID => m_BlockPoolID;

	public Rigidbody rbody { get; set; }

	public Visible visible { get; private set; }

	public Transform trans { get; private set; }

	public ChunkTypes ChunkType => (ChunkTypes)visible.ItemType;

	public ChunkRarity ChunkRarity => m_ChunkRarity;

	public NetChunk netChunk { get; set; }

	public void setBlockPoolID(uint n)
	{
		m_BlockPoolID = n;
	}

	public bool HasValidBlockPoolID()
	{
		return TankBlock.IsBlockPoolIDValid(blockPoolID);
	}

	public void InitNew(Vector3 velocity, Vector3 angularVelocity)
	{
		visible.ColliderSwapper.UseSimple(simple: true);
		visible.ColliderSwapper.SimpleCollider.isTrigger = true;
		m_TriggerOverlap = s_OnSpawnValue;
		rbody.velocity = velocity;
		rbody.angularVelocity = angularVelocity;
		rbody.drag = Globals.inst.airSpeedDrag;
		visible.damageable.InitHealth(visible.damageable.MaxHealth);
		AddTriggerCatcher();
	}

	public void RestoreSaved(float health)
	{
		visible.damageable.InitHealth(health);
	}

	public void SetMaterial(Material mat)
	{
		if ((bool)m_MyRenderer)
		{
			m_MyRenderer.sharedMaterial = mat;
		}
	}

	public void InitRigidbody()
	{
		Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
		if (!rigidbody)
		{
			d.LogError("InitRigidbody on Chunk " + base.name + " failed to add new body: Rigidbody already exists?");
			rigidbody = GetComponent<Rigidbody>();
		}
		rbody = rigidbody;
		rbody.mass = m_Mass;
		rbody.useGravity = true;
		rbody.drag = Globals.inst.airSpeedDrag;
	}

	public void ClearRigidBody(bool immediate)
	{
		if (rbody.IsNotNull())
		{
			if (immediate)
			{
				UnityEngine.Object.DestroyImmediate(rbody);
			}
			else
			{
				UnityEngine.Object.Destroy(rbody);
			}
			rbody = null;
		}
	}

	private void AddTriggerCatcher()
	{
		if (!m_TriggerCatcherSubscribed)
		{
			TriggerCatcher.Subscribe(base.gameObject, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Stay, OnTriggerEvent);
			m_TriggerCatcherSubscribed = true;
			visible.ConditionalUpdater.FixedUpdateEvent.Subscribe(OnConditionalUpdate);
		}
	}

	private void RemoveTriggerCatcher()
	{
		if (m_TriggerCatcherSubscribed)
		{
			TriggerCatcher.Unsubscribe(base.gameObject, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Stay, OnTriggerEvent);
			m_TriggerCatcherSubscribed = false;
			visible.ConditionalUpdater.FixedUpdateEvent.Unsubscribe(OnConditionalUpdate);
		}
	}

	private void AddCollisionCatcher(CollisionCatcherTypes collisionType)
	{
		if (m_CurrentCollisionType != collisionType)
		{
			if (m_CurrentCollisionType == CollisionCatcherTypes.None)
			{
				m_CollisionCatcher.AddListener(CollisionCatcher.Type.Enter);
			}
			if (collisionType == CollisionCatcherTypes.Held)
			{
				m_CollisionCatcher.AddListener(CollisionCatcher.Type.Stay);
			}
			else
			{
				m_CollisionCatcher.RemoveListener(CollisionCatcher.Type.Stay);
			}
			m_CollisionCatcher.CollisionEvent.Unsubscribe(GetCollisionFunction(m_CurrentCollisionType));
			if (collisionType != CollisionCatcherTypes.None)
			{
				m_CollisionCatcher.CollisionEvent.Subscribe(GetCollisionFunction(collisionType));
			}
			m_CurrentCollisionType = collisionType;
		}
	}

	private Action<CollisionCatcher.Type, Collision> GetCollisionFunction(CollisionCatcherTypes collisionType)
	{
		Action<CollisionCatcher.Type, Collision> result = null;
		switch (collisionType)
		{
		case CollisionCatcherTypes.Held:
			result = OnCollisionHeld;
			break;
		case CollisionCatcherTypes.Loose:
			result = OnCollisionLoose;
			break;
		}
		return result;
	}

	private void RemoveCollisionCatchers()
	{
		m_CollisionCatcher.CollisionEvent.Unsubscribe(GetCollisionFunction(m_CurrentCollisionType));
		m_CollisionCatcher.Clear();
		m_CurrentCollisionType = CollisionCatcherTypes.None;
	}

	private void OnConditionalUpdate()
	{
		if (!visible.ColliderSwapper.SimpleCollider.isTrigger)
		{
			return;
		}
		if (m_TriggerOverlap == s_DefaultOverlapValue || Time.time >= spawnTime + m_MaxNonSolidTime)
		{
			visible.ColliderSwapper.SimpleCollider.isTrigger = false;
			visible.ColliderSwapper.UseSimple(simple: false);
			RemoveTriggerCatcher();
			return;
		}
		Vector3 vector = m_TriggerOverlap.SetY(0f);
		if (vector == Vector3.zero && m_TriggerOverlap != s_OnSpawnValue)
		{
			vector = new Vector3(UnityEngine.Random.value * 0.2f - 0.1f, 0f, UnityEngine.Random.value * 0.2f - 0.1f);
		}
		Vector3 force = vector * Globals.inst.m_ChunkOverlapRepelMax * Globals.inst.m_ChunkOverlapRepulsionCurve.Evaluate(vector.sqrMagnitude);
		rbody.AddForce(force, ForceMode.Impulse);
		m_TriggerOverlap = s_DefaultOverlapValue;
	}

	private void OnItemHeld(bool held)
	{
		if (held)
		{
			visible.ColliderSwapper.SimpleCollider.isTrigger = false;
			AddCollisionCatcher(CollisionCatcherTypes.Held);
		}
		else
		{
			AddCollisionCatcher(CollisionCatcherTypes.Loose);
		}
	}

	private void OnTriggerEvent(TriggerCatcher.Interaction t, Collider other)
	{
		if (t == TriggerCatcher.Interaction.Enter || t == TriggerCatcher.Interaction.Stay)
		{
			m_TriggerOverlap = visible.trans.position - other.transform.position;
		}
	}

	private void OnCollisionHeld(CollisionCatcher.Type t, Collision collision)
	{
		if (m_CurrentCollisionType == CollisionCatcherTypes.Held)
		{
			if ((t != CollisionCatcher.Type.Enter && t != CollisionCatcher.Type.Stay) || visible.holderStack == null || !visible.holderStack.myHolder.block.tank)
			{
				return;
			}
			visible.CheckTouchingHolder(collision);
			if (m_EnablePhysicsOnCollision)
			{
				if (rbody == null)
				{
					InitRigidbody();
					visible.holderStack.myHolder.GetComponent<ModuleItemHolderBeam>().ItemNowHasPhysics(visible);
				}
				visible.HeldItemLastCollisionContact = Time.time;
			}
		}
		else
		{
			d.LogError("ResourcePickup.OnCollisionHeld called on " + base.name + " but CurrentCollisionType is " + m_CurrentCollisionType);
		}
	}

	private void OnCollisionLoose(CollisionCatcher.Type t, Collision collision)
	{
		if (m_CurrentCollisionType == CollisionCatcherTypes.Loose)
		{
			if (t == CollisionCatcher.Type.Enter || t == CollisionCatcher.Type.Exit)
			{
				visible.KeepAwake();
			}
			if (t == CollisionCatcher.Type.Enter)
			{
				Singleton.Manager<ManSFX>.inst.PlayChunkImpactSFX(this, collision.relativeVelocity.magnitude);
			}
		}
		else
		{
			d.LogError(string.Concat("ResourcePickup.OnCollisionLoose (", t, ") called on ", base.name, " but CurrentCollisionType is ", m_CurrentCollisionType));
		}
	}

	private void OnFatalDamage(Damageable damageable, ManDamage.DamageInfo info)
	{
		Singleton.Manager<ManLooseBlocks>.inst.HostDestroyChunk(this);
	}

	private void OnPool()
	{
		trans = base.transform;
		rbody = GetComponent<Rigidbody>();
		if (rbody == null)
		{
			rbody = null;
		}
		visible = GetComponent<Visible>();
		Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>(includeInactive: true);
		m_MyRenderer = ((componentsInChildren != null && componentsInChildren.Length != 0) ? componentsInChildren[0] : null);
		visible.m_ItemType = new ItemTypeInfo(ObjectTypes.Chunk, visible.ItemType);
		visible.HeldEvent.Subscribe(OnItemHeld);
		visible.damageable.destroyOnDeath = false;
		visible.damageable.deathEvent.Subscribe(OnFatalDamage);
		m_Mass = rbody.mass;
		d.Assert(GetComponent<ColliderSwapper>(), "resource pickup " + base.name + "doesn't have a ColliderSwapper");
		m_CollisionCatcher = new CollisionCatcher(base.gameObject);
		m_BlockPoolID = 4294967294u;
	}

	private void OnSpawn()
	{
		spawnTime = Time.time;
		visible.damageable.InitHealth(visible.damageable.MaxHealth);
		AddCollisionCatcher(CollisionCatcherTypes.Loose);
		m_BlockPoolID = 4294967293u;
	}

	private void OnRecycle()
	{
		if ((bool)netChunk)
		{
			d.LogError($"Recycling chunk {base.name} ID={visible.ID} while it still has a NetChunk associated");
			netChunk.Disconnect();
		}
		RemoveTriggerCatcher();
		RemoveCollisionCatchers();
		if (rbody == null)
		{
			InitRigidbody();
		}
		Singleton.Manager<ManLooseBlocks>.inst.OnChunkRecycle(this);
		m_BlockPoolID = 4294967292u;
	}

	public void ResetGravityAdjustment()
	{
		m_GravityScale = 1f;
	}

	public void PrimeForGravityAdjustment()
	{
		m_GravityScale = 1f;
	}

	public float AdjustGravity(GravityManipulationZone zone)
	{
		float gravityScale = m_GravityScale;
		m_GravityScale += zone.m_ManipulationAmount;
		m_GravityScale = Mathf.Max(m_GravityScale, 0f);
		return gravityScale - m_GravityScale;
	}

	public void FinaliseGravityAdjustment()
	{
	}

	public IGravityApplicationTarget GetGravityApplicationTarget()
	{
		return this;
	}

	public void SetAdjustmentTouched(bool touched)
	{
		m_GravityAdjustmentTouched = touched;
	}

	public bool HasAdjustmentBeenTouched()
	{
		return m_GravityAdjustmentTouched;
	}

	public float GetGravityScale()
	{
		return m_GravityScale;
	}

	public Rigidbody GetApplicationRigidbody()
	{
		return rbody;
	}

	public Vector3 GetWorldCentreOfGravity()
	{
		return rbody.worldCenterOfMass;
	}

	public bool CanApplyGravity()
	{
		return true;
	}

	public void SetApplicationTouched(bool touched)
	{
		m_GravityApplicationTouched = touched;
	}

	public bool HasApplicationBeenTouched()
	{
		return m_GravityApplicationTouched;
	}
}
