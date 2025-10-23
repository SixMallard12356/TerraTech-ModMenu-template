using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TechRadar : TechComponent
{
	public EventNoParams MappedChunkTypesDirtiedEvent;

	private List<ModuleRadar> m_Radars = new List<ModuleRadar>();

	private ModuleRadar.RadarScanType m_ActiveScanTypes;

	private Dictionary<ModuleRadar.RadarScanType, float> m_Ranges = new Dictionary<ModuleRadar.RadarScanType, float>(new ModuleRadar.RadarScanTypeComparer());

	private bool m_RangeDirty;

	private List<ChunkTypes> m_MappedChunkTypes = new List<ChunkTypes>();

	private bool m_MappedChunkTypesDirty;

	public bool IsMappingResources
	{
		get
		{
			UpdateMappedChunkTypes();
			return m_MappedChunkTypes.Count > 0;
		}
	}

	public IEnumerable<ChunkTypes> MappedChunkTypes
	{
		get
		{
			UpdateMappedChunkTypes();
			return m_MappedChunkTypes;
		}
	}

	public bool IsMappingTerrain
	{
		get
		{
			UpdateRanges();
			return (m_ActiveScanTypes & ModuleRadar.RadarScanType.Terrain) != 0;
		}
	}

	public void AddRadar(ModuleRadar radar)
	{
		m_Radars.Add(radar);
		m_RangeDirty = true;
		if (radar.ScansResources && radar.ResourceType != ChunkTypes.Null)
		{
			MarkMappedChunkTypesDirty();
		}
	}

	public void RemoveRadar(ModuleRadar radar)
	{
		m_Radars.Remove(radar);
		m_RangeDirty = true;
		if (radar.ScansResources && radar.ResourceType != ChunkTypes.Null)
		{
			MarkMappedChunkTypesDirty();
		}
	}

	public float GetRange(ModuleRadar.RadarScanType scanType)
	{
		UpdateRanges();
		m_Ranges.TryGetValue(scanType, out var value);
		return value;
	}

	public void MarkMappedChunkTypesDirty()
	{
		m_MappedChunkTypesDirty = true;
		MappedChunkTypesDirtiedEvent.Send();
	}

	private void UpdateRanges()
	{
		if (!m_RangeDirty)
		{
			return;
		}
		m_Ranges.TryGetValue(ModuleRadar.RadarScanType.Terrain, out var value);
		m_ActiveScanTypes = (ModuleRadar.RadarScanType)0;
		EnumValuesIterator<ModuleRadar.RadarScanType> enumerator = EnumIterator<ModuleRadar.RadarScanType>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleRadar.RadarScanType current = enumerator.Current;
			m_Ranges[current] = 0f;
		}
		for (int i = 0; i < m_Radars.Count; i++)
		{
			enumerator = EnumIterator<ModuleRadar.RadarScanType>.Values().GetEnumerator();
			while (enumerator.MoveNext())
			{
				ModuleRadar.RadarScanType current2 = enumerator.Current;
				if ((m_Radars[i].ScanType & current2) != 0)
				{
					m_ActiveScanTypes |= current2;
					m_Ranges.TryGetValue(current2, out var value2);
					m_Ranges[current2] = Mathf.Max(value2, m_Radars[i].GetRange(current2));
				}
			}
		}
		m_RangeDirty = false;
		if (m_Ranges.TryGetValue(ModuleRadar.RadarScanType.Terrain, out var value3) && value3 > value)
		{
			Singleton.Manager<ManMap>.inst.TerrainScanDirty = true;
		}
	}

	private void UpdateMappedChunkTypes()
	{
		if (!m_MappedChunkTypesDirty)
		{
			return;
		}
		m_MappedChunkTypesDirty = false;
		m_MappedChunkTypes.Clear();
		foreach (ModuleRadar radar in m_Radars)
		{
			if (radar.ScansResources && radar.ResourceType != ChunkTypes.Null && !m_MappedChunkTypes.Contains(radar.ResourceType))
			{
				m_MappedChunkTypes.Add(radar.ResourceType);
			}
		}
	}

	private void OnSpawn()
	{
		m_ActiveScanTypes = (ModuleRadar.RadarScanType)0;
		EnumValuesIterator<ModuleRadar.RadarScanType> enumerator = EnumIterator<ModuleRadar.RadarScanType>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleRadar.RadarScanType current = enumerator.Current;
			m_Ranges[current] = 0f;
		}
		m_RangeDirty = false;
		m_MappedChunkTypes.Clear();
		m_MappedChunkTypesDirty = false;
	}

	private void OnRecycle()
	{
		m_Radars.Clear();
	}
}
