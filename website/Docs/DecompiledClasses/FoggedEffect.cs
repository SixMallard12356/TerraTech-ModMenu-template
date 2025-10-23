using UnityEngine;

public class FoggedEffect : MonoBehaviour
{
	private void Start()
	{
		Color globalFogColor = Singleton.Manager<CameraManager>.inst.Fog.globalFogColor;
		Vector4 value = new Vector4(globalFogColor.r, globalFogColor.g, globalFogColor.b, globalFogColor.a * Singleton.Manager<CameraManager>.inst.Fog.globalDensity);
		GetComponent<Renderer>().material.SetVector("fogCol", value);
		GetComponent<Renderer>().material.SetFloat("fogDensity", 1f / Singleton.Manager<CameraManager>.inst.Fog.globalDensity);
	}
}
