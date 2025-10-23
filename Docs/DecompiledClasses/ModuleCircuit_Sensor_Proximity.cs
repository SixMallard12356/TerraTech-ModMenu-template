using System;
using UnityEngine;

public class ModuleCircuit_Sensor_Proximity : ModuleCircuit_Sensor_Collider, ICircuitDispensor
{
	[SerializeField]
	protected ModuleHUDSliderControl m_SenseDistanceSlider;

	[SerializeField]
	protected Transform m_SenseEpicentre;

	[SerializeField]
	protected Transform m_SensorUnitGeometry;

	protected float m_Radius;

	protected override int GetSensedColliders(ref Collider[] sensedCollidersCache)
	{
		int num = Physics.OverlapSphereNonAlloc(m_SenseEpicentre.position, m_Radius, sensedCollidersCache, m_DetectionLayerMask);
		if (num >= sensedCollidersCache.Length)
		{
			while (num >= sensedCollidersCache.Length)
			{
				Array.Resize(ref sensedCollidersCache, sensedCollidersCache.Length * 2);
			}
			return GetSensedColliders(ref sensedCollidersCache);
		}
		return num;
	}

	private void SetSenseRadius(float value)
	{
		m_Radius = value;
		if (m_SensorUnitGeometry != null)
		{
			m_SensorUnitGeometry.localScale = Vector3.one * (m_Radius * 2f);
		}
	}

	protected override void OnSetActive(bool state)
	{
		m_SensorUnitGeometry.gameObject.SetActive(state);
	}

	private void OnSenseOptionsConfigured()
	{
		SetSenseRadius(m_SenseDistanceSlider.Value);
	}

	private void OnPool()
	{
		m_SenseDistanceSlider.OptionSetEvent.Subscribe(OnSenseOptionsConfigured);
	}

	private void OnDepool()
	{
		m_SenseDistanceSlider.OptionSetEvent.Unsubscribe(OnSenseOptionsConfigured);
	}
}
