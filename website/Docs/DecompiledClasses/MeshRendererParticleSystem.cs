#define UNITY_EDITOR
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class MeshRendererParticleSystem : MonoBehaviour
{
	[HideInInspector]
	[SerializeField]
	private ParticleSystem m_ParticleSystem;

	private ParticleSystem.ShapeModule m_ShapeModule;

	private void PrePool()
	{
		m_ParticleSystem = GetComponent<ParticleSystem>();
		m_ShapeModule = m_ParticleSystem.shape;
		m_ShapeModule.enabled = true;
		m_ShapeModule.shapeType = ParticleSystemShapeType.MeshRenderer;
		m_ShapeModule.meshShapeType = ParticleSystemMeshShapeType.Triangle;
		m_ShapeModule.meshRenderer = null;
	}

	private void OnPool()
	{
		m_ParticleSystem = GetComponent<ParticleSystem>();
		m_ShapeModule = m_ParticleSystem.shape;
	}

	private void OnSpawn()
	{
		d.Assert(base.transform.parent != null, "MeshRendererParticleSystem spawned without a parent! This system relies on having a parent transform with a valid mesh renderer on it to function! Call code!");
		MeshRenderer component = base.transform.parent.GetComponent<MeshRenderer>();
		d.Assert(component != null, "MeshRendererParticleSystem spawned without a valid parent! This system relies on having a parent transform with a valid mesh renderer on it to function! Call code!");
		m_ShapeModule.meshRenderer = component;
		if (m_ParticleSystem.isStopped)
		{
			m_ParticleSystem.Play();
		}
	}

	private void OnRecycle()
	{
		d.Assert(base.transform.parent != null, "MeshRendererParticleSystem spawned without a parent! This system relies on having a parent transform with a valid mesh renderer on it to function! Call code!");
		if (m_ParticleSystem.isPlaying)
		{
			m_ParticleSystem.Stop();
		}
		m_ShapeModule.meshRenderer = null;
	}
}
