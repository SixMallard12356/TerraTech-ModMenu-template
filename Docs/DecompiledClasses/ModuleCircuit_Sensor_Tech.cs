using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(ModuleCircuitDispensor))]
public class ModuleCircuit_Sensor_Tech : ModuleCircuit_Sensor_Collider, ICircuitDispensor
{
	[SerializeField]
	protected Transform m_BeamGeometry;

	[SerializeField]
	protected Transform m_BeamOrigin;

	[FormerlySerializedAs("BeamLengthSliderControl")]
	[SerializeField]
	protected ModuleHUDSliderControl m_BeamLengthSliderControl;

	private const float k_BeamRadius = 0.25f;

	private static RaycastHit[] _s_Hits = new RaycastHit[16];

	private void SetBeamLength(float length)
	{
		m_BeamGeometry.localScale = new Vector3(1f, 1f, length);
	}

	protected override int GetSensedColliders(ref Collider[] sensedCollidersCache)
	{
		int num = Physics.SphereCastNonAlloc(m_BeamOrigin.transform.position, 0.25f, m_BeamOrigin.forward, _s_Hits, m_BeamGeometry.localScale.z - 0.25f, m_DetectionLayerMask);
		if (num > _s_Hits.Length || num > sensedCollidersCache.Length)
		{
			while (num >= _s_Hits.Length)
			{
				Array.Resize(ref _s_Hits, _s_Hits.Length * 2);
			}
			while (num >= sensedCollidersCache.Length)
			{
				Array.Resize(ref sensedCollidersCache, sensedCollidersCache.Length * 2);
			}
			return GetSensedColliders(ref sensedCollidersCache);
		}
		for (int i = 0; i < num; i++)
		{
			sensedCollidersCache[i] = _s_Hits[i].collider;
		}
		return num;
	}

	protected override void OnSetActive(bool state)
	{
		m_BeamGeometry.gameObject.SetActive(state);
	}

	private void OnBeamLengthSet()
	{
		SetBeamLength(m_BeamLengthSliderControl.Value);
	}

	private void OnPool()
	{
		m_BeamLengthSliderControl.OptionSetEvent.Subscribe(OnBeamLengthSet);
	}
}
