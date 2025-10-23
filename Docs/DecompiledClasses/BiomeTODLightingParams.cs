using UnityEngine;

public class BiomeTODLightingParams : ScriptableObject
{
	[SerializeField]
	private Gradient m_SunOrMoonColour;

	[SerializeField]
	private Gradient m_LightColour;

	[SerializeField]
	private Gradient m_RayColour;

	[SerializeField]
	private Gradient m_SkyColour;

	[SerializeField]
	private Gradient m_CloudColour;

	[SerializeField]
	private Gradient m_FogColour;

	[SerializeField]
	private Gradient m_AmbientColour;

	[SerializeField]
	private Gradient m_DustVFXColour;

	public Gradient SunOrMoonColour => m_SunOrMoonColour;

	public Gradient LightColour => m_LightColour;

	public Gradient RayColour => m_RayColour;

	public Gradient SkyColour => m_SkyColour;

	public Gradient CloudColour => m_CloudColour;

	public Gradient FogColour => m_FogColour;

	public Gradient AmbientColour => m_AmbientColour;

	public Gradient DustVFXColour => m_DustVFXColour;

	public void EditorInit(Gradient sunMoon, Gradient light, Gradient ray, Gradient sky, Gradient cloud, Gradient fog, Gradient ambient, Gradient dust)
	{
		m_SunOrMoonColour = sunMoon;
		m_LightColour = light;
		m_RayColour = ray;
		m_SkyColour = sky;
		m_CloudColour = cloud;
		m_FogColour = fog;
		m_AmbientColour = ambient;
		m_DustVFXColour = dust;
	}
}
