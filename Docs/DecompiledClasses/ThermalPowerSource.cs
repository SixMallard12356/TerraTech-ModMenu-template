#define UNITY_EDITOR
using UnityEngine;

public class ThermalPowerSource : MonoBehaviour
{
	[SerializeField]
	private float m_PowerMultiplier = 1f;

	[SerializeField]
	private SceneryTypes m_SceneryType;

	[SerializeField]
	[HideInInspector]
	private ParticleSystem[] m_SteamParticles;

	private Visible m_ParentVisible;

	public float PowerMultiplier => m_PowerMultiplier;

	public void Cap(bool capped)
	{
		ParticleSystem[] steamParticles = m_SteamParticles;
		foreach (ParticleSystem particleSystem in steamParticles)
		{
			if (capped)
			{
				particleSystem.Stop();
			}
			else
			{
				particleSystem.Play();
			}
		}
	}

	public SceneryTypes GetSceneryType()
	{
		return m_SceneryType;
	}

	public void SetAwake(bool awake)
	{
	}

	private void PrePool()
	{
		m_SteamParticles = GetComponentsInChildren<ParticleSystem>(includeInactive: true);
	}

	private void OnSpawn()
	{
		m_ParentVisible = Visible.FindVisibleUpwards(this);
		d.Assert((bool)m_ParentVisible && (bool)m_ParentVisible.resdisp, "Thermal source spawned not as child of ResourceDispenser");
		m_ParentVisible.resdisp.RegisterThermalSource(this, register: true);
	}

	private void OnRecycle()
	{
		m_ParentVisible.resdisp.RegisterThermalSource(this, register: false);
		m_ParentVisible = null;
	}
}
