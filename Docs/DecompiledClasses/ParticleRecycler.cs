using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ParticleRecycler : MonoBehaviour
{
	public bool m_PreventLooping;

	public Event<ParticleRecycler> ParticleSystemRecycledEvent;

	private Dictionary<ParticleSystemRenderer, Material> m_DefaultRenderMaterials = new Dictionary<ParticleSystemRenderer, Material>();

	[SerializeField]
	[HideInInspector]
	private ParticleSystem[] m_ParticlesLooping;

	[HideInInspector]
	[SerializeField]
	private float m_MaxParticleLifetime;

	private static List<ParticleRecycler> s_AliveList = new List<ParticleRecycler>();

	private float m_LifetimeRemaining;

	private bool m_WaitingForLoopingParticles;

	public bool IsPotentiallyLooping => m_WaitingForLoopingParticles;

	public void StopEmitting(bool stopImmediately = true)
	{
		for (int i = 0; i < m_ParticlesLooping.Length; i++)
		{
			if (stopImmediately)
			{
				m_ParticlesLooping[i].Stop();
				continue;
			}
			ParticleSystem.MainModule main = m_ParticlesLooping[i].main;
			main.loop = false;
		}
		m_WaitingForLoopingParticles = false;
	}

	public static void StaticUpdate()
	{
		int count = s_AliveList.Count;
		if (count > 0)
		{
			float deltaTime = Time.deltaTime;
			for (int num = count - 1; num >= 0; num--)
			{
				s_AliveList[num].RecycleIfFinished(deltaTime);
			}
		}
	}

	private bool RecycleIfFinished(float deltaTime)
	{
		if (m_WaitingForLoopingParticles)
		{
			return false;
		}
		m_LifetimeRemaining -= deltaTime;
		bool flag = m_LifetimeRemaining < 0f;
		if (flag)
		{
			base.transform.Recycle();
			for (int i = 0; i < m_ParticlesLooping.Length; i++)
			{
				ParticleSystem.MainModule main = m_ParticlesLooping[i].main;
				main.loop = true;
			}
		}
		return flag;
	}

	private void PrePool()
	{
		ParticleSystem[] componentsInChildren = GetComponentsInChildren<ParticleSystem>(includeInactive: true);
		if (m_PreventLooping)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				ParticleSystem.MainModule main = componentsInChildren[i].main;
				main.loop = false;
			}
		}
		m_MaxParticleLifetime = 0f;
		List<ParticleSystem> list = new List<ParticleSystem>(componentsInChildren.Length);
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			ParticleSystem.MainModule main2 = componentsInChildren[j].main;
			ParticleSystem.EmissionModule emission = componentsInChildren[j].emission;
			ParticleSystemRenderer component = componentsInChildren[j].GetComponent<ParticleSystemRenderer>();
			if ((bool)component && component.enabled && component.sharedMaterial != null)
			{
				if (main2.loop)
				{
					list.Add(componentsInChildren[j]);
				}
				if (emission.enabled && (emission.rateOverTime.constantMax > 0f || emission.rateOverDistance.constantMax > 0f))
				{
					float b = main2.duration + main2.startDelay.constantMax + main2.startLifetime.constantMax;
					m_MaxParticleLifetime = Mathf.Max(m_MaxParticleLifetime, b);
				}
				if (!emission.enabled || emission.burstCount <= 0)
				{
					continue;
				}
				for (int k = 0; k < emission.burstCount; k++)
				{
					ParticleSystem.Burst burst = emission.GetBurst(k);
					if (burst.maxCount > 0)
					{
						float b2 = burst.time + main2.startDelay.constantMax + main2.startLifetime.constantMax;
						m_MaxParticleLifetime = Mathf.Max(m_MaxParticleLifetime, b2);
					}
				}
			}
			else
			{
				Object.Destroy(componentsInChildren[j]);
				if ((bool)component)
				{
					Object.Destroy(component);
				}
			}
		}
		m_ParticlesLooping = list.ToArray();
	}

	private void OnSpawn()
	{
		s_AliveList.Add(this);
		m_WaitingForLoopingParticles = m_ParticlesLooping.Length != 0;
		m_LifetimeRemaining = m_MaxParticleLifetime;
		ParticleSystem[] componentsInChildren = GetComponentsInChildren<ParticleSystem>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			ParticleSystemRenderer component = componentsInChildren[i].GetComponent<ParticleSystemRenderer>();
			m_DefaultRenderMaterials.Add(component, component.sharedMaterial);
		}
	}

	private void OnRecycle()
	{
		s_AliveList.Remove(this);
		foreach (KeyValuePair<ParticleSystemRenderer, Material> defaultRenderMaterial in m_DefaultRenderMaterials)
		{
			if (defaultRenderMaterial.Key != null && defaultRenderMaterial.Key.sharedMaterial != defaultRenderMaterial.Value)
			{
				defaultRenderMaterial.Key.sharedMaterial = defaultRenderMaterial.Value;
			}
		}
		m_DefaultRenderMaterials.Clear();
		ParticleSystemRecycledEvent.Send(this);
	}
}
