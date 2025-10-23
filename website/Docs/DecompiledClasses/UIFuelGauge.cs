using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFuelGauge : UIHUDElement
{
	private Tank m_Tank;

	private float m_PrevFuelLevel;

	private List<ModuleFuelGauge> m_Modules = new List<ModuleFuelGauge>();

	private bool m_ShowingFromUI;

	[SerializeField]
	private Image[] m_Segments;

	[SerializeField]
	private Gauge m_Gauge = new Gauge();

	private List<Gauge.SegmentState> m_segmentColours = new List<Gauge.SegmentState>();

	public override void Show(object moduleObject)
	{
		base.Show(moduleObject);
		ModuleFuelGauge moduleFuelGauge = moduleObject as ModuleFuelGauge;
		if ((bool)moduleFuelGauge)
		{
			m_Modules.Add(moduleFuelGauge);
			m_Tank = moduleFuelGauge.block.tank;
		}
		else
		{
			m_ShowingFromUI = true;
			m_Tank = Singleton.playerTank;
		}
	}

	public override void Hide(object moduleObject)
	{
		ModuleFuelGauge moduleFuelGauge = moduleObject as ModuleFuelGauge;
		if ((bool)moduleFuelGauge)
		{
			m_Modules.Remove(moduleFuelGauge);
		}
		if (!m_ShowingFromUI && m_Modules.Count == 0)
		{
			base.Hide(moduleObject);
		}
	}

	private void Update()
	{
		if (m_ShowingFromUI)
		{
			m_Tank = Singleton.playerTank;
		}
		if (!m_Tank)
		{
			return;
		}
		float fuelLevel = m_Tank.Boosters.FuelLevel;
		if (fuelLevel == m_PrevFuelLevel)
		{
			return;
		}
		m_PrevFuelLevel = fuelLevel;
		m_segmentColours.Clear();
		m_Gauge.CalculateSegmentStates(fuelLevel, ref m_segmentColours);
		for (int i = 0; i < m_Segments.Length; i++)
		{
			if (m_segmentColours[i].blink != 0f)
			{
				m_Segments[i].color = m_segmentColours[i].colour * m_segmentColours[i].blink;
			}
			else
			{
				m_Segments[i].color = m_segmentColours[i].colour;
			}
		}
	}

	private void OnRecycle()
	{
		m_Modules.Clear();
		m_ShowingFromUI = false;
	}
}
