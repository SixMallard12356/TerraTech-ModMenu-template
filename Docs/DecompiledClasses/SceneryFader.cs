using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class SceneryFader : MonoBehaviour
{
	[SerializeField]
	public bool m_IgnoreSceneryFade;

	private List<Renderer> m_Renderers = new List<Renderer>();

	private bool m_IsLive;

	public bool IgnoreSceneryFade => m_IgnoreSceneryFade;

	public List<Renderer> Renderers => m_Renderers;

	private void UpdateRenderers()
	{
		GetComponentsInChildren(m_Renderers);
		for (int num = m_Renderers.Count - 1; num >= 0; num--)
		{
			if (m_Renderers[num] is ParticleSystemRenderer)
			{
				m_Renderers.RemoveAt(num);
			}
		}
		Singleton.Manager<ManSceneryFade>.inst.UpdateFadedScenery(this);
	}

	public void OnResourceDispDamaged(ResourceDispenser respDisp)
	{
		UpdateRenderers();
	}

	private void OnSpawn()
	{
		UpdateRenderers();
	}
}
