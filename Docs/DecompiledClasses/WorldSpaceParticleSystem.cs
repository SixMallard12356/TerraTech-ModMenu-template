#define UNITY_EDITOR
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class WorldSpaceParticleSystem : WorldSpaceObjectBase
{
	private ParticleSystem[] m_CachedParticleSystemList;

	private const int kDefaultParticleListSize = 100;

	private static ParticleSystem.Particle[] s_ParticleList;

	private bool m_Enabled;

	public void SetEnabled(bool enabled)
	{
		if (enabled != m_Enabled)
		{
			m_Enabled = enabled;
			if (m_Enabled)
			{
				Singleton.Manager<ManWorldTreadmill>.inst.AddWorldSpaceObject(this);
			}
			else
			{
				Singleton.Manager<ManWorldTreadmill>.inst.RemoveWorldSpaceObject(this);
			}
		}
	}

	public override void OnMoveWorldOrigin(IntVector3 amountToMove)
	{
		ParticleSystem[] cachedParticleSystemList = m_CachedParticleSystemList;
		foreach (ParticleSystem particleSystem in cachedParticleSystemList)
		{
			if (particleSystem.main.simulationSpace != ParticleSystemSimulationSpace.World || particleSystem.particleCount <= 0)
			{
				continue;
			}
			int maxParticles = particleSystem.main.maxParticles;
			if (s_ParticleList == null || s_ParticleList.Length < maxParticles)
			{
				if (s_ParticleList != null)
				{
					d.LogWarningFormat("WorldSpaceParticleSystem - New maximum particle count ({0}) encountered; list will be resized! (Consider increasing kDefaultParticleListSize to avoid this in future)", maxParticles);
				}
				s_ParticleList = new ParticleSystem.Particle[Mathf.Max(100, maxParticles)];
			}
			int particles = particleSystem.GetParticles(s_ParticleList);
			for (int j = 0; j < particles; j++)
			{
				s_ParticleList[j].position += amountToMove;
			}
			particleSystem.SetParticles(s_ParticleList, particles);
		}
	}

	private void OnPool()
	{
		m_CachedParticleSystemList = GetComponentsInChildren<ParticleSystem>(includeInactive: true);
	}

	private void OnSpawn()
	{
		SetEnabled(enabled: true);
	}

	private void OnRecycle()
	{
		SetEnabled(enabled: false);
	}
}
