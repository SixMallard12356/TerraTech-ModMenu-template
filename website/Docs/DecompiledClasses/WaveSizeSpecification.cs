#define UNITY_EDITOR
using System;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public struct WaveSizeSpecification
{
	[SerializeField]
	private int m_WaveSize;

	[SerializeField]
	private bool m_OverrideOnSwitch;

	[SerializeField]
	[InspectorVisibilityControl("m_OverrideOnSwitch", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	private int m_WaveSizeSwitch;

	public int WaveSize
	{
		get
		{
			if (!m_OverrideOnSwitch || !SKU.SwitchUI)
			{
				return m_WaveSize;
			}
			return m_WaveSizeSwitch;
		}
	}

	[Conditional("UNITY_EDITOR")]
	public void Validate(string errorContext)
	{
		d.Assert(m_WaveSize > 0, "Zero size wave specified for " + errorContext);
		if (m_OverrideOnSwitch)
		{
			d.Assert(m_WaveSizeSwitch > 0, $"Zero size wave specified (on Switch) for {errorContext} WaveSize={m_WaveSize} WaveSizeSwitch={m_WaveSizeSwitch}");
			d.Assert(m_WaveSizeSwitch <= m_WaveSize, $"Unexpected Switch size override specified for {errorContext} WaveSize={m_WaveSize} WaveSizeSwitch={m_WaveSizeSwitch}");
		}
	}
}
