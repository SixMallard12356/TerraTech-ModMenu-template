using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayVfxAnimEvent : MonoBehaviour
{
	[Serializable]
	public class NamedVFX
	{
		public string m_Name;

		public ParticleSystem m_ParticleSystem;
	}

	[SerializeField]
	[FormerlySerializedAs("m_VFX")]
	private NamedVFX[] m_ChildVFXToPlay;

	[SerializeField]
	private NamedVFX[] m_VFXToSpawn;

	private Dictionary<string, ParticleSystem> m_ChildVFXLookup;

	private Dictionary<string, ParticleSystem> m_VFXToSpawnLookup;

	public void PlayVFX(AnimationEvent animationEvent)
	{
		if (m_ChildVFXLookup.TryGetValue(animationEvent.stringParameter, out var value))
		{
			value.Play(withChildren: true);
		}
	}

	public void SpawnVFXAtTransform(AnimationEvent animationEvent)
	{
		if (m_VFXToSpawnLookup.TryGetValue(animationEvent.stringParameter, out var value))
		{
			value.Spawn(base.transform.position).Play(withChildren: true);
		}
	}

	private void Awake()
	{
		m_ChildVFXLookup = new Dictionary<string, ParticleSystem>();
		for (int i = 0; i < m_ChildVFXToPlay.Length; i++)
		{
			m_ChildVFXLookup.Add(m_ChildVFXToPlay[i].m_Name, m_ChildVFXToPlay[i].m_ParticleSystem);
		}
		m_VFXToSpawnLookup = new Dictionary<string, ParticleSystem>();
		for (int j = 0; j < m_VFXToSpawn.Length; j++)
		{
			m_VFXToSpawnLookup.Add(m_VFXToSpawn[j].m_Name, m_VFXToSpawn[j].m_ParticleSystem);
		}
	}
}
