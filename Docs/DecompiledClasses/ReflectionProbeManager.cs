#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ReflectionProbeManager
{
	private ReflectionProbe m_TileReflectionProbePrefab;

	private List<WorldTile> m_ReflectionProbeQueue = new List<WorldTile>();

	private ReflectionProbe m_ProbeCurrentlyRendering;

	private int m_ReflectionProbeRenderID;

	private int m_ReflectionProbeRenderPerDayCounter;

	private bool m_ReflectionProbeUpdateEnabled;

	public void Init(ReflectionProbe tileReflectionProbePrefab)
	{
		m_TileReflectionProbePrefab = tileReflectionProbePrefab;
		d.Assert(m_TileReflectionProbePrefab.timeSlicingMode != ReflectionProbeTimeSlicingMode.NoTimeSlicing, "Reflection probes only supported with time slicing");
		Singleton.Manager<ManWorld>.inst.TileManager.TilePopulatedEvent.Subscribe(OnTilePopulated);
		Singleton.Manager<ManWorld>.inst.TileManager.TileDepopulatedEvent.Subscribe(OnTileDepopulated);
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(EnableReflectionProbes);
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(DisableReflectionProbes);
	}

	public void Reset()
	{
		m_ProbeCurrentlyRendering = null;
		m_ReflectionProbeQueue.Clear();
	}

	public void Update()
	{
		if (!m_ReflectionProbeUpdateEnabled || Singleton.Manager<ManWorld>.inst.TileManager.IsClearing)
		{
			return;
		}
		if ((bool)TOD_Sky.Instance)
		{
			int num = (int)(TOD_Sky.Instance.Cycle.Hour / 24f * (float)QualitySettingsExtended.TileReflectionProbeRendersPerDay);
			if (m_ReflectionProbeRenderPerDayCounter != num)
			{
				m_ReflectionProbeRenderPerDayCounter = num;
				m_ReflectionProbeQueue.Clear();
				TileManager.TileIterator enumerator = Singleton.Manager<ManWorld>.inst.TileManager.IterateTiles(WorldTile.State.Populated).GetEnumerator();
				while (enumerator.MoveNext())
				{
					WorldTile current = enumerator.Current;
					m_ReflectionProbeQueue.Add(current);
				}
			}
		}
		if (Singleton.Manager<ManWorld>.inst.TileManager.IsGenerating || (!(m_ProbeCurrentlyRendering == null) && !m_ProbeCurrentlyRendering.IsFinishedRendering(m_ReflectionProbeRenderID)))
		{
			return;
		}
		if (m_ReflectionProbeQueue.Count > 0)
		{
			Vector3 scenePosition = Singleton.Manager<ManWorld>.inst.FocalPoint.ScenePosition;
			int num2 = -1;
			float num3 = float.MaxValue;
			for (int i = 0; i < m_ReflectionProbeQueue.Count; i++)
			{
				float sqrMagnitude = (scenePosition - m_ReflectionProbeQueue[i].reflectionProbe.transform.position).ToVector2XZ().sqrMagnitude;
				if (sqrMagnitude < num3)
				{
					num3 = sqrMagnitude;
					num2 = i;
				}
			}
			if (num2 >= 0 && num2 < m_ReflectionProbeQueue.Count)
			{
				m_ProbeCurrentlyRendering = m_ReflectionProbeQueue[num2].reflectionProbe;
				m_ReflectionProbeQueue.RemoveAt(num2);
				m_ReflectionProbeRenderID = m_ProbeCurrentlyRendering.RenderProbe();
			}
			else
			{
				d.LogErrorFormat("Failed to find nearest reflection probe. How is that possible? (Current probe count: {0}. Nearest probe dist sqr: {1}. Index: {2})", m_ReflectionProbeQueue.Count, num3, num2);
				m_ProbeCurrentlyRendering = null;
			}
		}
		else
		{
			m_ProbeCurrentlyRendering = null;
		}
	}

	public void AddProbeToTileTemplate(Transform template)
	{
		if (m_TileReflectionProbePrefab != null)
		{
			Object.Instantiate(m_TileReflectionProbePrefab).transform.parent = template;
		}
	}

	private void OnTilePopulated(WorldTile tile)
	{
		tile.reflectionProbe = tile.Terrain.GetComponentInChildren<ReflectionProbe>();
		Vector3 vector = Vector3.one * Singleton.Manager<ManWorld>.inst.TileSize;
		tile.reflectionProbe.size = vector.SetY(tile.reflectionProbe.size.y);
		tile.reflectionProbe.transform.localPosition = (vector * 0.5f).SetY(0f);
		if (!m_ReflectionProbeQueue.Contains(tile))
		{
			tile.reflectionProbe.transform.position = Singleton.Manager<ManWorld>.inst.ProjectToGround(tile.reflectionProbe.transform.position) + Vector3.up * 10f;
			m_ReflectionProbeQueue.Add(tile);
		}
	}

	private void OnTileDepopulated(WorldTile tile)
	{
		m_ReflectionProbeQueue.Remove(tile);
		if (tile.reflectionProbe == m_ProbeCurrentlyRendering)
		{
			m_ProbeCurrentlyRendering = null;
		}
	}

	private void EnableReflectionProbes(Mode obj)
	{
		m_ReflectionProbeUpdateEnabled = QualitySettingsExtended.EnableReflectionProbes;
	}

	private void DisableReflectionProbes(Mode obj)
	{
		m_ReflectionProbeUpdateEnabled = false;
	}
}
