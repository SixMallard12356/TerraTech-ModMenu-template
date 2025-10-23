using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionCallback : MonoBehaviour
{
	private struct HitData
	{
		public int m_Count;

		public Vector3 m_HitPos;

		public Vector3 m_HitDir;

		public void AddHitData(Vector3 hitPos, Vector3 hitDir)
		{
			m_HitPos += hitPos;
			m_HitDir += hitDir;
			m_Count++;
		}
	}

	private Event<Visible, Vector3, Vector3> m_CollisionCallback;

	private ParticleSystem m_ParticleSystetm;

	private List<ParticleCollisionEvent> m_CollisionEvents;

	private static readonly int k_InitialListSize = 16;

	private Dictionary<Visible, HitData> m_VisibleData = new Dictionary<Visible, HitData>();

	public void SubscribeCallback(Action<Visible, Vector3, Vector3> callback)
	{
		m_CollisionCallback.Subscribe(callback);
	}

	public void UnsubscribeCallback(Action<Visible, Vector3, Vector3> callback)
	{
		m_CollisionCallback.Unsubscribe(callback);
	}

	private void OnParticleCollision(GameObject other)
	{
		int safeCollisionEventSize = m_ParticleSystetm.GetSafeCollisionEventSize();
		if (m_CollisionEvents.Count < safeCollisionEventSize)
		{
			m_CollisionEvents.Resize(safeCollisionEventSize);
		}
		int collisionEvents = m_ParticleSystetm.GetCollisionEvents(other, m_CollisionEvents);
		m_VisibleData.Clear();
		for (int i = 0; i < collisionEvents; i++)
		{
			ParticleCollisionEvent particleCollisionEvent = m_CollisionEvents[i];
			Visible componentInParents = particleCollisionEvent.colliderComponent.GetComponentInParents<Visible>(thisObjectFirst: true);
			if (componentInParents != null)
			{
				if (!m_VisibleData.TryGetValue(componentInParents, out var value))
				{
					value = default(HitData);
				}
				value.AddHitData(particleCollisionEvent.intersection, particleCollisionEvent.velocity.normalized);
				m_VisibleData[componentInParents] = value;
			}
		}
		foreach (KeyValuePair<Visible, HitData> visibleDatum in m_VisibleData)
		{
			HitData value2 = visibleDatum.Value;
			Vector3 paramB = value2.m_HitPos / value2.m_Count;
			Vector3 paramC = value2.m_HitDir / value2.m_Count;
			m_CollisionCallback.Send(visibleDatum.Key, paramB, paramC);
		}
	}

	private void OnPool()
	{
		m_ParticleSystetm = GetComponent<ParticleSystem>();
		m_CollisionEvents = new List<ParticleCollisionEvent>(k_InitialListSize);
	}
}
