using System;
using UnityEngine;

public class GaugeProperties : ScriptableObject
{
	[Serializable]
	public struct Thresholds
	{
		public float threshold;

		public float blinkThreshold;

		public Color colour;
	}

	public float m_BlinkRate = 10f;

	public Thresholds[] m_Thresholds;
}
