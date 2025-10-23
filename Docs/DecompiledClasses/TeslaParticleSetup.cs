using UnityEngine;

public class TeslaParticleSetup : MonoBehaviour
{
	[SerializeField]
	private Transform[] m_CoilStages;

	[SerializeField]
	private ParticleSystem m_StageOneLoop;

	private float m_CurrentDuration;

	public void SetOffsetAndDuration(float targetDuration)
	{
		m_CurrentDuration = m_StageOneLoop.main.duration;
		ParticleSystem component = GetComponent<ParticleSystem>();
		if (!targetDuration.Approximately(m_CurrentDuration))
		{
			component.Stop(withChildren: true);
			for (int i = 0; i < m_CoilStages.Length; i++)
			{
				AdjustTiming(m_CoilStages[i], targetDuration / m_CurrentDuration);
			}
			m_CurrentDuration = targetDuration;
		}
		component.time = 0f;
		component.Play(withChildren: true);
	}

	public void AdjustTiming(Transform sysRoot, float newScale)
	{
		ParticleSystem[] componentsInChildren = sysRoot.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			TrySetTimingAndDelay(ref componentsInChildren[i], newScale);
		}
	}

	public void DisableAllStages()
	{
		for (int i = 0; i < m_CoilStages.Length; i++)
		{
			m_CoilStages[i].gameObject.SetActive(value: false);
		}
	}

	private void TrySetTimingAndDelay(ref ParticleSystem system, float newScale)
	{
		if (!system.Equals(null))
		{
			ParticleSystem.MainModule main = system.main;
			main.startDelay = main.startDelay.constant * newScale;
			main.duration *= newScale;
			if (!system.name.Contains("Loop"))
			{
				main.startLifetime = main.startLifetime.constant * newScale;
			}
		}
	}
}
