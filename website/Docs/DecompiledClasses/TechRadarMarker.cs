#define UNITY_EDITOR
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(BehaviorTree))]
public class TechRadarMarker : TechComponent
{
	private List<ModuleRadarMarker> m_RadarMarkerModules = new List<ModuleRadarMarker>();

	private RadarMarker m_RadarMarkerConfig;

	public RadarMarker RadarMarkerConfig
	{
		get
		{
			return m_RadarMarkerConfig;
		}
		set
		{
			m_RadarMarkerConfig = value;
			UpdateModuleVisuals();
		}
	}

	public void AddRadarMarkerModule(ModuleRadarMarker radarMarkerModule)
	{
		d.Assert(!m_RadarMarkerModules.Contains(radarMarkerModule), $"Trying to add already added module.. {radarMarkerModule}");
		bool num = m_RadarMarkerModules.Count == 0 && m_RadarMarkerConfig.Equals(RadarMarker.DefaultMarker_Disabled);
		m_RadarMarkerModules.Add(radarMarkerModule);
		if (num)
		{
			m_RadarMarkerConfig = radarMarkerModule.DisplayedMarker;
		}
		else
		{
			radarMarkerModule.SetRadarMarkerToDisplay(m_RadarMarkerConfig);
		}
		UpdateIsUsed();
	}

	public void RemoveRadarMarkerModule(ModuleRadarMarker radarMarkerModule)
	{
		d.Assert(m_RadarMarkerModules.Remove(radarMarkerModule), $"Failed to remove module.. {radarMarkerModule}");
		if (!ManSaveGame.Storing)
		{
			UpdateIsUsed();
		}
	}

	private void UpdateIsUsed()
	{
		bool isUsed = m_RadarMarkerConfig.IsUsed;
		m_RadarMarkerConfig.IsUsed = m_RadarMarkerModules.Count != 0 && base.Tech.Anchors.NumAnchored != 0;
		if (m_RadarMarkerConfig.IsUsed != isUsed)
		{
			UpdateModuleVisuals();
		}
	}

	private void UpdateModuleVisuals()
	{
		for (int i = 0; i < m_RadarMarkerModules.Count; i++)
		{
			m_RadarMarkerModules[i].SetRadarMarkerToDisplay(m_RadarMarkerConfig);
		}
	}

	private void OnAnchor(bool anchored, bool skyAnchored)
	{
		UpdateIsUsed();
	}

	private void OnSpawn()
	{
		m_RadarMarkerConfig = RadarMarker.DefaultMarker_Disabled;
		base.Tech.Anchors.AnchorEvent.Subscribe(OnAnchor);
	}

	private void OnRecycle()
	{
		base.Tech.Anchors.AnchorEvent.Unsubscribe(OnAnchor);
	}
}
