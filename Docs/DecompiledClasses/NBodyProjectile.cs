#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class NBodyProjectile : ZoneProjectile
{
	[SerializeField]
	private float m_Strength = 5f;

	private List<Rigidbody> m_AffectedRigidBodies;

	private void OnPool()
	{
		m_AffectedRigidBodies = new List<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (!base.Deployed)
		{
			return;
		}
		m_AffectedRigidBodies.Clear();
		foreach (Collider item in PhysicsUtils.OverlapSphereAllNonAlloc(base.trans.position, base.ZoneRadius, m_LayersToAffect.value))
		{
			Rigidbody attachedRigidbody = item.attachedRigidbody;
			if ((bool)attachedRigidbody)
			{
				d.Assert(!m_AffectedRigidBodies.Contains(attachedRigidbody), "m_AffectedRigidBodies already contains " + attachedRigidbody.name);
				m_AffectedRigidBodies.Add(attachedRigidbody);
			}
		}
		int count = m_AffectedRigidBodies.Count;
		for (int i = 0; i < count; i++)
		{
			Rigidbody rigidbody = m_AffectedRigidBodies[i];
			for (int j = i + 1; j < count; j++)
			{
				Rigidbody rigidbody2 = m_AffectedRigidBodies[j];
				Vector3 worldCenterOfMass = rigidbody.worldCenterOfMass;
				Vector3 worldCenterOfMass2 = rigidbody2.worldCenterOfMass;
				rigidbody.AddForce((worldCenterOfMass2 - worldCenterOfMass).normalized * m_Strength * rigidbody2.mass);
				rigidbody2.AddForce((worldCenterOfMass - worldCenterOfMass2).normalized * m_Strength * rigidbody.mass);
			}
		}
	}
}
