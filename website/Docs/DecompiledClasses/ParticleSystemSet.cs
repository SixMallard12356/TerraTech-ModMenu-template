using UnityEngine;

public class ParticleSystemSet : MonoBehaviour
{
	private ParticleSystem[] m_Systems;

	public void SetEmissionEnabled(bool enabled)
	{
		for (int i = 0; i < m_Systems.Length; i++)
		{
			if (enabled)
			{
				m_Systems[i].Play();
			}
			else
			{
				m_Systems[i].Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
			}
		}
	}

	private void OnPool()
	{
		bool includeInactive = true;
		m_Systems = GetComponentsInChildren<ParticleSystem>(includeInactive);
	}

	private void OnSpawn()
	{
		SetEmissionEnabled(enabled: true);
	}
}
