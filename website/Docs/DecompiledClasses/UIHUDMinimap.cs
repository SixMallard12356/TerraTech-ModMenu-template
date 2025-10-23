using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIHUDMinimap : UIHUDElement
{
	[SerializeField]
	[EnumArray(typeof(ManRadar.MiniMapType))]
	private UIMiniMapDisplay[] m_RadarDisplays;

	private int m_CurrentTypeIndex = -1;

	private List<ModuleRadar> m_Radars = new List<ModuleRadar>();

	public override void Show(object context)
	{
		base.Show(context);
		ModuleRadar item = context as ModuleRadar;
		m_Radars.Add(item);
		UpdateDisplayTypeIfNeeded();
	}

	public override void Hide(object context)
	{
		ModuleRadar item = context as ModuleRadar;
		m_Radars.Remove(item);
		UpdateDisplayTypeIfNeeded();
		if (m_Radars.Count == 0)
		{
			base.Hide(context);
		}
	}

	private void UpdateDisplayTypeIfNeeded()
	{
		int num = ((m_Radars.Count > 0) ? m_Radars.Max((ModuleRadar r) => (int)r.MiniMapType) : (-1));
		if (num == m_CurrentTypeIndex)
		{
			return;
		}
		if (m_CurrentTypeIndex != -1)
		{
			SetRadarDisplayEnabled(m_CurrentTypeIndex, enabled: false);
		}
		bool flag = num > m_CurrentTypeIndex;
		m_CurrentTypeIndex = num;
		if (m_CurrentTypeIndex == -1)
		{
			return;
		}
		SetRadarDisplayEnabled(m_CurrentTypeIndex, enabled: true);
		if (flag)
		{
			ModuleRadar moduleRadar = m_Radars[m_Radars.Count - 1];
			FMODEvent radarOnSFXEvent = moduleRadar.RadarOnSFXEvent;
			if (radarOnSFXEvent.IsValid())
			{
				radarOnSFXEvent.PlayOneShot(moduleRadar.block.trans.position);
			}
		}
	}

	private void SetRadarDisplayEnabled(int typeIndex, bool enabled)
	{
		if (typeIndex >= 0 && typeIndex < m_RadarDisplays.Length && (bool)m_RadarDisplays[typeIndex])
		{
			if (enabled)
			{
				m_RadarDisplays[typeIndex].Show();
			}
			else
			{
				m_RadarDisplays[typeIndex].Hide();
			}
		}
	}

	private void OnSpawn()
	{
		for (int i = 0; i < m_RadarDisplays.Length; i++)
		{
			m_RadarDisplays[i]?.Hide();
		}
	}

	private void OnRecycle()
	{
		m_Radars.Clear();
	}
}
