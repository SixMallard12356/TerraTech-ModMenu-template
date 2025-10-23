using UnityEngine;

public class BiomeCloudParams : ScriptableObject
{
	[SerializeField]
	private TOD_CloudParameters m_Clouds;

	public TOD_CloudParameters Params => m_Clouds;

	public void EditorInit(TOD_CloudParameters clouds)
	{
		m_Clouds.Size = clouds.Size;
		m_Clouds.Opacity = clouds.Opacity;
		m_Clouds.Coverage = clouds.Coverage;
		m_Clouds.Sharpness = clouds.Sharpness;
		m_Clouds.Attenuation = clouds.Attenuation;
		m_Clouds.Saturation = clouds.Saturation;
		m_Clouds.Scattering = clouds.Scattering;
		m_Clouds.Brightness = clouds.Brightness;
	}
}
