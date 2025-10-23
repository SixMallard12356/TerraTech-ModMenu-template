using System.Linq;
using UnityEngine;

public class DespawnAfterTime : MonoBehaviour
{
	[SerializeField]
	private float m_DespawnTime;

	[SerializeField]
	private bool m_DespawnOutOfView;

	private bool m_Despawn;

	private float m_Timer;

	private Transform m_Trans;

	private Renderer[] m_Renderers;

	private void OnEncountersReset()
	{
		m_Trans.Recycle();
	}

	public void Despawn()
	{
		if (!m_Despawn)
		{
			Singleton.Manager<ManEncounter>.inst.ResetEvent.Subscribe(OnEncountersReset);
			m_Despawn = true;
		}
	}

	private void Update()
	{
		if (m_Despawn)
		{
			m_Timer += Time.deltaTime;
			if ((!m_DespawnOutOfView || m_Renderers == null || !m_Renderers.Any((Renderer x) => x.isVisible)) && m_Timer >= m_DespawnTime)
			{
				base.transform.Recycle();
			}
		}
	}

	private void OnPool()
	{
		m_Trans = base.transform;
		m_Renderers = GetComponentsInChildren<Renderer>(includeInactive: true);
	}

	private void OnRecycle()
	{
		if (m_Despawn)
		{
			Singleton.Manager<ManEncounter>.inst.ResetEvent.Unsubscribe(OnEncountersReset);
		}
		m_Despawn = false;
		m_Timer = 0f;
	}
}
