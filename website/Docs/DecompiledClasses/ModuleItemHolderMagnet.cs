#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(ModuleItemHolder))]
[RequireComponent(typeof(ModuleItemPickup))]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleItemHolderMagnet : Module
{
	public delegate void GluedObjectsListUpdated();

	private struct MagnetisedObject
	{
		public Visible item;

		public Vector3 com;

		public float radius;

		public float potential;
	}

	private struct Gluable
	{
		public float firstCollisionTime;

		public int lastCollisionFrame;
	}

	[Tooltip("How powerful the magnet is")]
	[SerializeField]
	private float m_Strength = 500f;

	[Tooltip("The magnet starts to struggle with objects heavier than this mass")]
	[SerializeField]
	private float m_PowerDecayMassThreshold = 2f;

	[Tooltip("Multiplier for drop range, relative to ModuleItemPickup's pickup range")]
	[SerializeField]
	private float m_DropRadiusVsPickup = 1.2f;

	[Tooltip("Don't drop an out-of-range item until at least this long after first being attracted")]
	[SerializeField]
	private float m_DropAfterMinTime = 1f;

	[Tooltip("Drop any item if it hasn't been picked up, this long after first being attracted")]
	[SerializeField]
	private float m_DropUnstuckAfterTime = 2f;

	[Tooltip("Drop all items if taken this much damage in one hit")]
	[SerializeField]
	private float m_DropOnDamageThreshold = 10f;

	[Tooltip("Reference to the collider that attracted objects will stick to")]
	[SerializeField]
	private Collider m_ActiveCollider;

	[Tooltip("Objects in contact for this long become stuck. Don't touch unless you know what you're doing")]
	[SerializeField]
	private float m_GluePeriod = 0.2f;

	[Tooltip("Objects have to move slower than this in order to become stuck. Don't touch unless you know what you're doing")]
	[SerializeField]
	private float m_SettlingSpeedThreshold = 0.1f;

	[Tooltip("Particle effect while waiting to lift items")]
	[SerializeField]
	private ParticleSystem m_WaitingParticlesPrefab;

	[Tooltip("Particle effect while drawing in items")]
	[SerializeField]
	private ParticleSystem m_PullingParticlesPrefab;

	[SerializeField]
	[Tooltip("Particle effect for items stuck in place")]
	private ParticleSystem m_GluedParticlesPrefab;

	[SerializeField]
	private TechAudio.SFXType m_OnlineSFXType;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private GluedObjectsListUpdated GluedObjectsListUpdated_Event;

	private ModuleItemHolder m_Holder;

	private ModuleItemPickup m_Pickup;

	private ModuleAudioProvider m_AudioProvider;

	private float m_GluedMass;

	private float m_SettleThresholdSqr;

	private float m_PickupDelayTimeout;

	private const float k_PrePickupDelayNormal = 0.001f;

	[HideInInspector]
	[SerializeField]
	private Transform m_GluedObjectsContainer;

	private List<Visible> m_GluedObjects = new List<Visible>(5);

	private Dictionary<int, Gluable> m_ObjectsToGlue = new Dictionary<int, Gluable>();

	private HashSet<Visible> m_TriggeredAudioItems = new HashSet<Visible>();

	private static List<Visible> s_ItemsToDrop = new List<Visible>(10);

	private static List<MagnetisedObject> s_MagnetisedObjects = new List<MagnetisedObject>(20);

	private bool HummPlaying;

	private bool TankBeamActive
	{
		get
		{
			if (base.block.IsAttached)
			{
				return base.block.tank.beam.IsActive;
			}
			return false;
		}
	}

	public bool IsOperating
	{
		get
		{
			if (!TankBeamActive)
			{
				if (!CircuitControlled)
				{
					return IsCategoryEnabled();
				}
				if (base.block.CircuitNode.Receiver.CurrentChargeData > 0)
				{
					return base.block.CircuitReceiver.ShouldProcessInput;
				}
				return false;
			}
			return false;
		}
	}

	private bool CircuitControlled
	{
		get
		{
			if (m_IsUsedOnCircuit)
			{
				return base.block.CircuitNode.Receiver.IsConnectedToOtherNodes;
			}
			return false;
		}
	}

	private bool ShouldDropItem(Visible item, float radius)
	{
		if (!item.rbody || (m_ObjectsToGlue.TryGetValue(item.ID, out var value) && Time.frameCount - value.lastCollisionFrame < (int)(m_GluePeriod * 2f / Time.deltaTime)))
		{
			return false;
		}
		float itemPickupTime = base.block.tank.Holders.GetItemPickupTime(item.ID);
		d.Assert(itemPickupTime != -1f, "failed to find pickup time for item " + item.name);
		float num = Mathf.Max(itemPickupTime, m_PickupDelayTimeout) + m_DropAfterMinTime;
		if (!(Time.time > itemPickupTime + m_DropUnstuckAfterTime))
		{
			if (Time.time > num)
			{
				return radius > m_Pickup.PickupRange * m_DropRadiusVsPickup;
			}
			return false;
		}
		return true;
	}

	private void UpdateItemMovement()
	{
		if (!base.block.IsAttached)
		{
			return;
		}
		if (!IsOperating)
		{
			if (!m_Holder.IsEmpty)
			{
				UnglueAllObjects(prepareReGlue: false);
				m_Holder.DropAll();
			}
			m_PickupDelayTimeout = Time.time + m_Holder.PickupContentionPeriod;
			return;
		}
		Vector3 zero = Vector3.zero;
		Vector3 centreOfMassWorld = base.block.centreOfMassWorld;
		float num = 1f;
		s_ItemsToDrop.Clear();
		s_MagnetisedObjects.Clear();
		s_MagnetisedObjects.Add(new MagnetisedObject
		{
			item = null,
			com = centreOfMassWorld,
			radius = 0f,
			potential = 1f
		});
		ModuleItemHolder.Stack.ItemIterator enumerator = m_Holder.Contents.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Visible current = enumerator.Current;
			d.Assert(current.type == ObjectTypes.Block);
			ParticleSystem particleSystem = Singleton.Manager<ManVisible>.inst.GetParticles(current);
			ParticleSystem particleSystem2 = (particleSystem ? particleSystem.GetOriginalPrefab() : null);
			bool flag = Time.time < m_PickupDelayTimeout;
			ParticleSystem particleSystem3 = ((!current.rbody) ? m_GluedParticlesPrefab : (flag ? m_WaitingParticlesPrefab : m_PullingParticlesPrefab));
			if (particleSystem3 != particleSystem2)
			{
				if ((bool)particleSystem3)
				{
					particleSystem = Singleton.Manager<ManVisible>.inst.SpawnAttachParticles(current, particleSystem3);
				}
				else
				{
					Singleton.Manager<ManVisible>.inst.ClearParticles(current);
				}
			}
			if ((bool)particleSystem)
			{
				particleSystem.transform.LookAt(centreOfMassWorld);
			}
			if (flag)
			{
				if ((current.centrePosition - centreOfMassWorld).magnitude - m_Holder.HorizontalBoundsRadius > m_Pickup.PickupRange || Singleton.Manager<ManPointer>.inst.DraggingItem == current)
				{
					s_ItemsToDrop.Add(current);
				}
				continue;
			}
			Vector3 centreOfMassWorld2 = current.block.centreOfMassWorld;
			float magnitude = (centreOfMassWorld - centreOfMassWorld2).magnitude;
			if (current == Singleton.Manager<ManPointer>.inst.DraggingItem || ShouldDropItem(current, magnitude))
			{
				s_ItemsToDrop.Add(current);
			}
			else if (!(Singleton.Manager<ManPointer>.inst.DraggingItem == current))
			{
				float num2 = 1f / magnitude;
				num += num2;
				s_MagnetisedObjects.Add(new MagnetisedObject
				{
					item = current,
					com = centreOfMassWorld2,
					radius = magnitude,
					potential = num2
				});
				if (m_TriggeredAudioItems.Add(current))
				{
					TechAudio.AudioTickData data = TechAudio.AudioTickData.ConfigureOneshot(this, TechAudio.SFXType.BlockMagnetPickup);
					base.block.tank.TechAudio.PlayOneshot(data);
				}
			}
		}
		foreach (Visible item in s_ItemsToDrop)
		{
			m_TriggeredAudioItems.Remove(item);
			item.SetHolder(null);
		}
		float num3 = m_Strength / num;
		for (int i = 1; i < s_MagnetisedObjects.Count; i++)
		{
			MagnetisedObject magnetisedObject = s_MagnetisedObjects[i];
			if (!magnetisedObject.item.rbody)
			{
				continue;
			}
			Vector3 zero2 = Vector3.zero;
			for (int j = 0; j < s_MagnetisedObjects.Count; j++)
			{
				MagnetisedObject magnetisedObject2 = s_MagnetisedObjects[j];
				if (!(magnetisedObject2.item == magnetisedObject.item))
				{
					Vector3 vector = magnetisedObject2.com - magnetisedObject.com;
					float magnitude2 = vector.magnitude;
					Vector3 vector2 = vector * (magnetisedObject2.potential * num3 / (magnitude2 * magnitude2));
					zero2 += vector2;
				}
			}
			float num4 = ((magnetisedObject.item.rbody.mass < m_PowerDecayMassThreshold) ? magnetisedObject.item.rbody.mass : (m_PowerDecayMassThreshold * m_PowerDecayMassThreshold / magnetisedObject.item.rbody.mass));
			Vector3 vector3 = zero2 * num4;
			magnetisedObject.item.rbody.velocity *= Mathf.Lerp(1f, Globals.inst.m_MagnetVelocityDamping, num4 / magnetisedObject.item.rbody.mass);
			magnetisedObject.item.rbody.AddForce(vector3 - Physics.gravity * num4, ForceMode.Force);
			zero -= vector3;
		}
		base.block.tank.rbody.AddForceAtPosition(zero, centreOfMassWorld, ForceMode.Force);
	}

	private void GlueTankBlock(Visible visible, bool glue)
	{
		d.Assert(visible.type == ObjectTypes.Block);
		TankBlock tankBlock = visible.block;
		tankBlock.SetGluedToMagnet(glue, this, m_GluedObjectsContainer);
		m_GluedMass += (glue ? tankBlock.CurrentMass : (0f - tankBlock.CurrentMass));
		if (glue)
		{
			m_GluedObjects.Add(visible);
			m_TriggeredAudioItems.Remove(visible);
		}
		else
		{
			m_GluedObjects.Remove(visible);
		}
		GluedObjectsListUpdated_Event?.Invoke();
		tankBlock.SetAdditionalMassCategory(TankBlock.MassCategoryType.Magnet, m_GluedMass);
	}

	private void UnglueAllObjects(bool prepareReGlue)
	{
		m_ObjectsToGlue.Clear();
		if (m_GluedObjects.Count == 0)
		{
			return;
		}
		foreach (Visible gluedObject in m_GluedObjects)
		{
			if (gluedObject.isActive)
			{
				gluedObject.trans.parent = null;
				d.Assert(gluedObject.type == ObjectTypes.Block);
				if (gluedObject.block.rbody.IsNull())
				{
					gluedObject.block.InitRigidbody();
				}
				gluedObject.WorldSpaceComponent.SetEnabled(enabled: true);
				if (prepareReGlue)
				{
					m_ObjectsToGlue[gluedObject.ID] = new Gluable
					{
						firstCollisionTime = Time.time,
						lastCollisionFrame = Time.frameCount
					};
				}
			}
		}
		m_GluedObjects.Clear();
		GluedObjectsListUpdated_Event?.Invoke();
		m_GluedMass = 0f;
		base.block.SetAdditionalMassCategory(TankBlock.MassCategoryType.Magnet, 0.0);
	}

	private void OnAttached()
	{
		base.block.tank.CollisionEvent.Subscribe(OnTankCollision);
	}

	private void OnDetaching()
	{
		UnglueAllObjects(prepareReGlue: false);
		base.block.tank.CollisionEvent.Unsubscribe(OnTankCollision);
	}

	private void OnTankCollision(Tank.CollisionInfo collision, Tank.CollisionInfo.Event e)
	{
		if (!base.block.IsAttached || base.block.tank.beam.IsActive || !collision.b.visible || collision.b.visible.holderStack == null || !(collision.b.visible.holderStack.myHolder == m_Holder) || !collision.b.visible.isActive)
		{
			return;
		}
		bool flag = e == Tank.CollisionInfo.Event.Stay && collision.a.collider == m_ActiveCollider;
		if (!flag && e == Tank.CollisionInfo.Event.NonAttached)
		{
			d.AssertFormat(collision.a.visible != null, "ModuleItemHolderMagnet - Collision between {0} and {1} - Expected both objects to be Visibles", collision.a.visible ? collision.a.visible.name : collision.a.collider.name, collision.b.visible.name);
			if ((bool)collision.a.visible && (bool)collision.a.visible.rbody)
			{
				collision.Flip();
			}
			d.AssertFormat(!collision.a.visible.rbody, "ModuleItemHolderMagnet - Collision between {0} and {1} - Expected one of the objects to already be glued to the tech", collision.a.visible, collision.b.visible);
			flag = m_GluedObjects.Contains(collision.a.visible);
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			flag = false;
		}
		if (!flag)
		{
			return;
		}
		if (m_ObjectsToGlue.TryGetValue(collision.b.visible.ID, out var value))
		{
			if (Time.frameCount - value.lastCollisionFrame > 2)
			{
				m_ObjectsToGlue[collision.b.visible.ID] = new Gluable
				{
					firstCollisionTime = Time.time,
					lastCollisionFrame = Time.frameCount
				};
			}
			else if (Time.time - value.firstCollisionTime > m_GluePeriod && (collision.b.visible.rbody.velocity - base.block.tank.rbody.velocity).sqrMagnitude < m_SettleThresholdSqr)
			{
				GlueTankBlock(collision.b.visible, glue: true);
				m_ObjectsToGlue.Remove(collision.b.visible.ID);
			}
			else
			{
				m_ObjectsToGlue[collision.b.visible.ID] = new Gluable
				{
					firstCollisionTime = value.firstCollisionTime,
					lastCollisionFrame = Time.frameCount
				};
			}
		}
		else
		{
			m_ObjectsToGlue.Add(collision.b.visible.ID, new Gluable
			{
				firstCollisionTime = Time.time,
				lastCollisionFrame = Time.frameCount
			});
		}
	}

	private void OnReleaseItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack)
	{
		if (ManNetwork.IsHost && item.rbody.IsNull())
		{
			UnglueAllObjects(prepareReGlue: true);
		}
		Singleton.Manager<ManVisible>.inst.ClearParticles(item);
	}

	private void OnDamaged(ManDamage.DamageInfo damageInfo)
	{
		if (ManNetwork.IsHost && damageInfo.Damage > m_DropOnDamageThreshold)
		{
			UnglueAllObjects(prepareReGlue: false);
			m_Holder.DropAll();
		}
	}

	private bool HandlePickupFilterCallback(Visible theVisible)
	{
		return IsOperating;
	}

	private void PrePool()
	{
		m_GluedObjectsContainer = new GameObject("_glued objects").transform;
		m_GluedObjectsContainer.parent = base.transform;
		m_GluedObjectsContainer.localPosition = Vector3.zero;
		m_GluedObjectsContainer.localRotation = Quaternion.identity;
		d.Assert(m_ActiveCollider, "Magnet" + base.name + "has no active collider set");
		d.Assert(GetComponent<ModuleItemHolder>().IsFlag(ModuleItemHolder.Flags.NoAcceptDrops), "Magnet" + base.name + "should have Flags.NoAcceptDrops set");
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
	}

	private void OnPool()
	{
		m_ControlCategoryType = ModuleControlCategory.Magnet;
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
		m_Holder = GetComponent<ModuleItemHolder>();
		m_Holder.ReleaseItemEvent.Subscribe(OnReleaseItem);
		m_Pickup = GetComponent<ModuleItemPickup>();
		m_Pickup.SetPickupFilterCallback(HandlePickupFilterCallback);
		GetComponent<Damageable>().damageEvent.Subscribe(OnDamaged);
		m_AudioProvider = GetComponent<ModuleAudioProvider>();
	}

	private void OnSpawn()
	{
		GluedObjectsListUpdated_Event = OnGluedObjectsListUpdated;
		m_GluedMass = 0f;
		m_PickupDelayTimeout = 0f;
		m_Holder.PickupContentionPeriod = 0.001f;
		d.Assert(m_DropUnstuckAfterTime > m_DropAfterMinTime, "DropUnstuckAfterTime should be grater than DropAfterMinTime: " + base.name);
	}

	private void OnRecycle()
	{
		UnglueAllObjects(prepareReGlue: false);
		GluedObjectsListUpdated_Event = null;
	}

	private void OnFixedUpdate()
	{
		UpdateItemMovement();
		m_Holder.PickupContentionPeriod = Mathf.Max(m_PickupDelayTimeout - Time.time, 0.001f);
		m_SettleThresholdSqr = m_SettlingSpeedThreshold * m_SettlingSpeedThreshold;
	}

	private void OnGluedObjectsListUpdated()
	{
		if (!(m_AudioProvider == null) && HummPlaying != m_GluedObjects.Count > 0)
		{
			HummPlaying = m_GluedObjects.Count > 0;
			m_AudioProvider.SetNoteOn(m_OnlineSFXType, m_GluedObjects.Count > 0);
		}
	}
}
