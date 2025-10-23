using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Gauge
{
	public struct SegmentState
	{
		public Color colour;

		public float blink;
	}

	[SerializeField]
	public GaugeProperties m_GaugeProperties;

	private SegmentState m_BlackSegmentState = new SegmentState
	{
		colour = Color.black,
		blink = 0f
	};

	public void CalculateSegmentStates(float value, ref List<SegmentState> segmentStates)
	{
		int num = m_GaugeProperties.m_Thresholds.Length;
		for (int i = 0; i < num; i++)
		{
			segmentStates.Add(m_BlackSegmentState);
		}
		int num2 = 0;
		for (int j = 0; j < m_GaugeProperties.m_Thresholds.Length; j++)
		{
			GaugeProperties.Thresholds thresholds = m_GaugeProperties.m_Thresholds[j];
			if (value > thresholds.threshold)
			{
				Color colour = thresholds.colour;
				num2 = segmentStates.Count - j - 1;
				SegmentState value2 = new SegmentState
				{
					colour = colour,
					blink = 0f
				};
				for (int k = 0; k < num2; k++)
				{
					segmentStates[k] = value2;
				}
				float num3 = 0.5f + Mathf.Sin(Time.time * m_GaugeProperties.m_BlinkRate) / 2f;
				segmentStates[num2] = new SegmentState
				{
					colour = colour,
					blink = ((value < thresholds.blinkThreshold) ? num3 : 0f)
				};
				num2++;
				break;
			}
		}
		for (int l = num2; l < segmentStates.Count; l++)
		{
			segmentStates[l] = m_BlackSegmentState;
		}
	}
}
