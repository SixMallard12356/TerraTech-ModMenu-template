#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
[DisallowMultipleComponent]
public class ColliderSwapper : MonoBehaviour
{
	[Serializable]
	public struct ColliderSwapperEntry
	{
		public Collider collider;

		public bool collisionWhenLoose;

		public bool collisionWhenAttached;
	}

	[SerializeField]
	[HideInInspector]
	public ColliderSwapperEntry[] m_Colliders;

	[SerializeField]
	private bool m_AttachedCollidersAreImportant;

	private Visible m_Visible;

	private bool m_CollisionEnabled;

	private bool m_UsingSimple;

	private bool m_UsingAttached;

	private bool m_MainWasEnabled;

	private bool m_AttachedWasEnabled;

	private bool m_SimpleWasEnabled;

	private bool m_DisableWithTrigger;

	private bool m_Reset;

	private ColliderSwapperEntry[] m_WheelColliders;

	private static List<Collider> s_CollidersInChildren = new List<Collider>(4);

	private static List<ColliderSwapperEntry> s_ColliderSwapperEntryListBuilder = new List<ColliderSwapperEntry>();

	public SphereCollider SimpleCollider { get; private set; }

	public ColliderSwapperEntry[] AllColliders => m_Colliders;

	public bool CollisionEnabled => m_CollisionEnabled;

	public void EnableCollision(bool enable, bool disableWithTrigger = false)
	{
		if (!enable && m_CollisionEnabled)
		{
			m_DisableWithTrigger = disableWithTrigger;
		}
		bool collisionEnabled = m_CollisionEnabled;
		m_CollisionEnabled = enable;
		UpdateColliderStatus();
		if (enable && !collisionEnabled)
		{
			m_DisableWithTrigger = false;
		}
	}

	public void UseSimple(bool simple)
	{
		if ((bool)SimpleCollider && (!m_UsingSimple || !SimpleCollider.isTrigger))
		{
			m_UsingSimple = simple;
			UpdateColliderStatus();
		}
	}

	public void UseAttached(bool attached)
	{
		bool flag = attached && (m_AttachedCollidersAreImportant || QualitySettingsExtended.UseSimplerCollidersForAttachedBlocks);
		if (m_UsingAttached != flag)
		{
			m_UsingAttached = flag;
			if (!m_UsingSimple)
			{
				UpdateColliderStatus();
			}
		}
	}

	public void RegisterVisibleColliders()
	{
		m_Visible = GetComponent<Visible>();
		SimpleCollider = GetComponent<SphereCollider>();
		ColliderSwapperEntry[] colliders = m_Colliders;
		for (int i = 0; i < colliders.Length; i++)
		{
			ColliderSwapperEntry colliderSwapperEntry = colliders[i];
			Singleton.Manager<ManVisible>.inst.RegisterColliderToVisibleLookup(m_Visible, colliderSwapperEntry.collider);
		}
		if ((bool)SimpleCollider)
		{
			Singleton.Manager<ManVisible>.inst.RegisterColliderToVisibleLookup(m_Visible, SimpleCollider);
		}
		if (m_WheelColliders != null)
		{
			colliders = m_WheelColliders;
			for (int i = 0; i < colliders.Length; i++)
			{
				ColliderSwapperEntry colliderSwapperEntry2 = colliders[i];
				Singleton.Manager<ManVisible>.inst.RegisterColliderToVisibleLookup(m_Visible, colliderSwapperEntry2.collider);
			}
		}
	}

	public void UnregisterVisibleColliders()
	{
		ColliderSwapperEntry[] colliders = m_Colliders;
		for (int i = 0; i < colliders.Length; i++)
		{
			ColliderSwapperEntry colliderSwapperEntry = colliders[i];
			Singleton.Manager<ManVisible>.inst.UnregisterColliderToVisibleLookup(colliderSwapperEntry.collider);
		}
		if ((bool)SimpleCollider)
		{
			Singleton.Manager<ManVisible>.inst.UnregisterColliderToVisibleLookup(SimpleCollider);
		}
		if (m_WheelColliders != null)
		{
			colliders = m_WheelColliders;
			for (int i = 0; i < colliders.Length; i++)
			{
				ColliderSwapperEntry colliderSwapperEntry2 = colliders[i];
				Singleton.Manager<ManVisible>.inst.UnregisterColliderToVisibleLookup(colliderSwapperEntry2.collider);
			}
		}
	}

	public void SetPhysicMaterial(PhysicMaterial mat)
	{
		ColliderSwapperEntry[] colliders = m_Colliders;
		for (int i = 0; i < colliders.Length; i++)
		{
			colliders[i].collider.sharedMaterial = mat;
		}
	}

	public void AddWheels(List<ManWheels.Wheel> wheels)
	{
		d.Assert(m_WheelColliders == null, "AddWheels has been called on this ColliderSwapper before! Only expect to add one set of wheels!");
		if (m_WheelColliders == null)
		{
			d.Assert(s_ColliderSwapperEntryListBuilder.Count == 0, "s_ColliderSwapperEntryListBuilder was not emptied out after last use!");
			foreach (ManWheels.Wheel wheel in wheels)
			{
				SphereCollider[] suspensionColliders = wheel.suspensionColliders;
				foreach (Collider collider in suspensionColliders)
				{
					if (collider != null)
					{
						s_ColliderSwapperEntryListBuilder.Add(new ColliderSwapperEntry
						{
							collider = collider,
							collisionWhenAttached = true,
							collisionWhenLoose = false
						});
					}
				}
			}
			m_WheelColliders = s_ColliderSwapperEntryListBuilder.ToArray();
			s_ColliderSwapperEntryListBuilder.Clear();
			if (m_Visible.IsNotNull())
			{
				ColliderSwapperEntry[] wheelColliders = m_WheelColliders;
				for (int i = 0; i < wheelColliders.Length; i++)
				{
					ColliderSwapperEntry colliderSwapperEntry = wheelColliders[i];
					Singleton.Manager<ManVisible>.inst.RegisterColliderToVisibleLookup(m_Visible, colliderSwapperEntry.collider);
				}
			}
		}
		m_AttachedCollidersAreImportant = true;
	}

	public void CopyColliderShapes(ColliderSwapper source)
	{
		if (source.m_Colliders.Length != m_Colliders.Length)
		{
			return;
		}
		for (int i = 0; i < source.m_Colliders.Length; i++)
		{
			if (m_Colliders[i].collider is MeshCollider meshCollider && source.m_Colliders[i].collider is MeshCollider meshCollider2)
			{
				meshCollider.sharedMesh = meshCollider2.sharedMesh;
			}
			else if (m_Colliders[i].collider is BoxCollider boxCollider && source.m_Colliders[i].collider is BoxCollider boxCollider2)
			{
				boxCollider.size = boxCollider2.size;
				boxCollider.center = boxCollider2.center;
			}
			else if (m_Colliders[i].collider is CapsuleCollider capsuleCollider && source.m_Colliders[i].collider is CapsuleCollider capsuleCollider2)
			{
				capsuleCollider.radius = capsuleCollider2.radius;
				capsuleCollider.height = capsuleCollider2.height;
				capsuleCollider.center = capsuleCollider2.center;
				capsuleCollider.direction = capsuleCollider2.direction;
			}
		}
	}

	private void UpdateColliderStatus()
	{
		bool flag = m_CollisionEnabled && !m_UsingSimple;
		bool flag2 = m_CollisionEnabled && m_UsingSimple;
		bool flag3 = false;
		if (m_Reset || flag != m_MainWasEnabled || m_UsingAttached != m_AttachedWasEnabled)
		{
			if (m_Colliders != null)
			{
				UpdateColliders(m_Colliders, flag);
			}
			if (m_WheelColliders != null)
			{
				UpdateColliders(m_WheelColliders, flag);
			}
			m_MainWasEnabled = flag;
			m_AttachedWasEnabled = m_UsingAttached;
			flag3 = true;
		}
		if ((m_Reset || flag2 != m_SimpleWasEnabled) && SimpleCollider != null)
		{
			SimpleCollider.enabled = flag2;
			SimpleCollider.isTrigger = false;
			m_SimpleWasEnabled = flag2;
			flag3 = true;
		}
		if (flag3)
		{
			m_Reset = false;
			if (flag && (m_Visible.type != ObjectTypes.Block || !m_Visible.block.IsAttached) && !Singleton.Manager<ManSpawn>.inst.IsTechSpawning)
			{
				m_Visible.MoveAboveGround();
			}
		}
	}

	private void UpdateColliders(ColliderSwapperEntry[] colliderList, bool mainEnabled)
	{
		for (int i = 0; i < colliderList.Length; i++)
		{
			ColliderSwapperEntry colliderSwapperEntry = colliderList[i];
			bool flag = (!m_UsingAttached && colliderSwapperEntry.collisionWhenLoose) || (m_UsingAttached && colliderSwapperEntry.collisionWhenAttached);
			bool flag2 = mainEnabled || m_DisableWithTrigger;
			colliderSwapperEntry.collider.enabled = flag && flag2;
			colliderSwapperEntry.collider.isTrigger = !m_CollisionEnabled && m_DisableWithTrigger;
		}
	}

	private void PrePool()
	{
		m_Visible = GetComponent<Visible>();
		SimpleCollider = GetComponent<SphereCollider>();
		if (m_Colliders == null || m_Colliders.Length == 0)
		{
			d.Assert(s_ColliderSwapperEntryListBuilder.Count == 0, "s_ColliderSwapperEntryListBuilder was not emptied out after last use!");
			GetComponentsInChildren(s_CollidersInChildren);
			foreach (Collider s_CollidersInChild in s_CollidersInChildren)
			{
				if (s_CollidersInChild != SimpleCollider && !s_CollidersInChild.isTrigger && s_CollidersInChild.enabled)
				{
					s_ColliderSwapperEntryListBuilder.Add(new ColliderSwapperEntry
					{
						collider = s_CollidersInChild,
						collisionWhenAttached = true,
						collisionWhenLoose = true
					});
				}
			}
			m_Colliders = s_ColliderSwapperEntryListBuilder.ToArray();
			s_CollidersInChildren.Clear();
			s_ColliderSwapperEntryListBuilder.Clear();
		}
		else
		{
			ColliderSwapperEntry[] colliders = m_Colliders;
			for (int i = 0; i < colliders.Length; i++)
			{
				ColliderSwapperEntry colliderSwapperEntry = colliders[i];
				if (colliderSwapperEntry.collider == null)
				{
					d.LogErrorFormat("Null collider found in ColliderSwapper on {0}", base.name);
				}
				else
				{
					colliderSwapperEntry.collider.enabled = colliderSwapperEntry.collisionWhenLoose;
				}
			}
		}
		if ((bool)SimpleCollider)
		{
			Bounds bounds = default(Bounds);
			if (m_Colliders != null && m_Colliders.Length != 0)
			{
				bounds = m_Colliders[0].collider.bounds;
			}
			for (int j = 1; j < m_Colliders.Length; j++)
			{
				bounds.min = Vector3.Min(bounds.min, m_Colliders[j].collider.bounds.min);
				bounds.max = Vector3.Max(bounds.max, m_Colliders[j].collider.bounds.max);
			}
			SimpleCollider.center = base.transform.InverseTransformPoint(bounds.center);
			SimpleCollider.radius = (bounds.extents.x + bounds.extents.y + bounds.extents.z) / 3f;
			SimpleCollider.enabled = false;
			SimpleCollider.isTrigger = false;
		}
	}

	private void OnPool()
	{
		m_Reset = true;
		m_CollisionEnabled = true;
		m_UsingSimple = false;
		m_DisableWithTrigger = false;
		m_UsingAttached = false;
	}

	private void OnRecycle()
	{
		m_Reset = true;
		m_CollisionEnabled = true;
		m_UsingSimple = false;
		m_UsingAttached = false;
	}
}
