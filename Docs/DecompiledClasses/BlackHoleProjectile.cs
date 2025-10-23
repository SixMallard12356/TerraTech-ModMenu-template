using System.Collections.Generic;
using UnityEngine;

public class BlackHoleProjectile : ZoneProjectile
{
	[SerializeField]
	private float m_MinStrength = 5f;

	[SerializeField]
	private float m_MaxStrength = 10f;

	[SerializeField]
	private bool m_StrengthDependsOnMass = true;

	[SerializeField]
	private bool m_EffectAttachedRigidBody;

	private HashSet<Rigidbody> m_AffectedRigidBodies;

	private List<Rigidbody> m_ParentRBodys;

	private void OnPool()
	{
		m_AffectedRigidBodies = new HashSet<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (!base.Deployed)
		{
			return;
		}
		m_AffectedRigidBodies.Clear();
		if (!m_EffectAttachedRigidBody)
		{
			m_ParentRBodys.Clear();
			GetComponentsInParent(includeInactive: true, m_ParentRBodys);
		}
		Vector3 position = base.trans.position;
		foreach (Collider item in PhysicsUtils.OverlapSphereAllNonAlloc(position, base.ZoneRadius, m_LayersToAffect.value))
		{
			Rigidbody attachedRigidbody = item.attachedRigidbody;
			if (attachedRigidbody != null && (m_EffectAttachedRigidBody || !m_ParentRBodys.Contains(attachedRigidbody)))
			{
				m_AffectedRigidBodies.Add(attachedRigidbody);
			}
		}
		foreach (Rigidbody affectedRigidBody in m_AffectedRigidBodies)
		{
			Vector3 vector = position - affectedRigidBody.worldCenterOfMass;
			float num = Mathf.Lerp(m_MaxStrength, m_MinStrength, vector.magnitude / base.ZoneRadius);
			affectedRigidBody.AddForce(vector.normalized * num * (m_StrengthDependsOnMass ? affectedRigidBody.mass : 1f));
		}
	}
}
