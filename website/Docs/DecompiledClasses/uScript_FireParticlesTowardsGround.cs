using UnityEngine;

public class uScript_FireParticlesTowardsGround : uScriptLogic
{
	private bool m_Fired;

	private bool m_Delivered;

	private float m_DeliveryTime;

	private Transform m_Particles;

	private float m_Timer;

	public bool Delivered => m_Delivered;

	public bool Out => true;

	public Transform In(Vector3 groundPos, Transform particleEffect, GameObject owner, string uniqueName)
	{
		if (m_Fired && !m_Delivered)
		{
			m_Timer += Time.deltaTime;
			if (m_Timer >= m_DeliveryTime)
			{
				m_Delivered = true;
			}
		}
		if (!m_Fired && Singleton.Manager<ManUI>.inst.FadeFinished())
		{
			Encounter component = owner.GetComponent<Encounter>();
			Vector3 vector = Mode<ModeMain>.inst.StartPositionScene + Mode<ModeMain>.inst.m_GameStartPosOffset;
			if (component.TrySpawnUniqueParticlesOnce(particleEffect, uniqueName, vector, Quaternion.identity, out m_Particles))
			{
				m_DeliveryTime = Random.Range(3f, 5f);
				m_Particles.GetComponent<Rigidbody>().AddForce(CalculateBestThrowSpeed(vector, groundPos, m_DeliveryTime), ForceMode.VelocityChange);
			}
			else
			{
				m_Delivered = true;
			}
			m_Fired = true;
		}
		return m_Particles;
	}

	public void OnDisable()
	{
		m_Fired = false;
		m_Particles = null;
		m_Timer = 0f;
		m_Delivered = false;
	}

	private Vector3 CalculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
	{
		Vector3 vector2;
		Vector3 vector = (vector2 = target - origin);
		vector2.y = 0f;
		float y = vector.y;
		float magnitude = vector2.magnitude;
		float y2 = y / timeToTarget + 0.5f * Physics.gravity.magnitude * timeToTarget;
		float num = magnitude / timeToTarget;
		Vector3 normalized = vector2.normalized;
		normalized *= num;
		normalized.y = y2;
		return normalized;
	}
}
