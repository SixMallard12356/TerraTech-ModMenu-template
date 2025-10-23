using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class UIMiniMapLayerResources : UIMiniMapLayer
{
	private struct MappedRes
	{
		public Vector3 position;

		public ChunkTypes chunkType;

		public Vector3 worldPos;
	}

	private class IconCache
	{
		public int numUsed;

		public List<UIMiniMapElement> icons;
	}

	[SerializeField]
	private bool m_DisplayDirectionOnly;

	[SerializeField]
	private bool m_DisplayRelativeToPlayer;

	[SerializeField]
	private float m_ScanFrequency = 0.5f;

	[SerializeField]
	private float m_DisplayFallOffDist = 0.7f;

	private IconCache[] m_IconCaches;

	private float m_RectRadius;

	private float m_LastScanTime;

	private List<MappedRes> m_MappedResources = new List<MappedRes>();

	private List<MappedRes> m_ResIconPositions = new List<MappedRes>();

	public override void UpdateLayer()
	{
		if ((bool)Singleton.playerTank)
		{
			float time = Time.time;
			if (time > m_LastScanTime + m_ScanFrequency)
			{
				RefreshCachedResourceLocations();
				m_LastScanTime = time;
			}
			ZeroIconCount();
			UpdateResourcesVisibleOnUI();
			RecycleUnusedIcons();
		}
	}

	private void RefreshCachedResourceLocations()
	{
		m_MappedResources.Clear();
		if (!(Singleton.playerTank != null) || !Singleton.playerTank.Radar.IsMappingResources)
		{
			return;
		}
		float range = Singleton.playerTank.Radar.GetRange(ModuleRadar.RadarScanType.Resources);
		if (!(range > 0f))
		{
			return;
		}
		Bounds sceneBounds = new Bounds(Singleton.playerPos, Vector3.one * range * 2f);
		Singleton.Manager<ManWorld>.inst.TileManager.GetTileCoordRange(sceneBounds, out var min, out var max);
		TileManager.TileIterator enumerator = Singleton.Manager<ManWorld>.inst.TileManager.IterateTiles(min, max, WorldTile.State.Loaded).GetEnumerator();
		while (enumerator.MoveNext())
		{
			WorldTile current = enumerator.Current;
			foreach (ChunkTypes mappedChunkType in Singleton.playerTank.Radar.MappedChunkTypes)
			{
				if (!current.m_ResourcesOnTile.TryGetValue(mappedChunkType, out var value))
				{
					continue;
				}
				foreach (Visible item in value)
				{
					m_MappedResources.Add(new MappedRes
					{
						position = item.centrePosition + Singleton.Manager<ManWorld>.inst.SceneToGameWorld,
						chunkType = mappedChunkType
					});
				}
			}
		}
	}

	private void UpdateResourcesVisibleOnUI()
	{
		if (m_MappedResources.Count <= 0 || !(Singleton.playerTank != null) || !Singleton.playerTank.Radar.IsMappingResources)
		{
			return;
		}
		Vector3 scenePosition = m_MapDisplay.FocalPoint.ScenePosition;
		float range = Singleton.playerTank.Radar.GetRange(ModuleRadar.RadarScanType.Resources);
		if (!(range > 0f))
		{
			return;
		}
		float maxRangeSqr = range * range;
		foreach (MappedRes mappedResource in m_MappedResources)
		{
			if (m_DisplayDirectionOnly ? CalculateIconDirectionFromWorld(mappedResource.position, limitRange: true, maxRangeSqr, m_RectRadius, out var iconPosition2D) : CalculateIconPositionFromWorld(mappedResource.position, limitRange: true, maxRangeSqr, 0f, out iconPosition2D))
			{
				Vector3 iconPos3D = GetIconPos3D(iconPosition2D, ManRadar.IconType.GenericResource);
				m_ResIconPositions.Add(new MappedRes
				{
					position = iconPos3D,
					chunkType = mappedResource.chunkType,
					worldPos = mappedResource.position
				});
			}
		}
		for (int i = 0; i < m_ResIconPositions.Count; i++)
		{
			MappedRes mappedRes = m_ResIconPositions[i];
			Vector2 vector = mappedRes.position.ToVector2XY();
			ChunkTypes chunkType = m_ResIconPositions[i].chunkType;
			Vector3 worldPos = m_ResIconPositions[i].worldPos;
			bool flag = false;
			for (int j = i + 1; j < m_ResIconPositions.Count; j++)
			{
				mappedRes = m_ResIconPositions[j];
				Vector2 vector2 = mappedRes.position.ToVector2XY();
				if ((vector - vector2).sqrMagnitude < 25f)
				{
					flag = true;
					m_ResIconPositions.RemoveAt(j);
					j--;
				}
				if (flag)
				{
					m_ResIconPositions[i] = new MappedRes
					{
						position = vector,
						chunkType = chunkType,
						worldPos = worldPos
					};
				}
			}
		}
		foreach (MappedRes resIconPosition in m_ResIconPositions)
		{
			float value = (resIconPosition.worldPos + Singleton.Manager<ManWorld>.inst.GameWorldToScene - scenePosition).ToVector2XZ().magnitude / range;
			float num = Mathf.InverseLerp(m_DisplayFallOffDist, 1f, value);
			Color white = Color.white;
			white.a = Mathf.Lerp(1f, 0f, num * num);
			PlaceIcon(ManRadar.IconType.GenericResource, white, resIconPosition.position);
		}
		m_ResIconPositions.Clear();
	}

	private void PlaceIcon(ManRadar.IconType iconType, Color iconColour, Vector3 iconPos)
	{
		UIMiniMapElement icon = GetIcon(iconType);
		icon.RectTrans.localPosition = iconPos;
		icon.Icon.color = iconColour;
	}

	private UIMiniMapElement GetIcon(ManRadar.IconType iconType)
	{
		UIMiniMapElement result = null;
		IconCache iconCache = m_IconCaches[(int)iconType];
		UIMiniMapElement iconElementPrefab = Singleton.Manager<ManRadar>.inst.GetIconElementPrefab(iconType);
		if (iconElementPrefab != null)
		{
			if (iconCache.numUsed >= iconCache.icons.Count)
			{
				UIMiniMapElement uIMiniMapElement = iconElementPrefab.Spawn();
				uIMiniMapElement.RectTrans.SetParent(m_RectTrans, worldPositionStays: false);
				iconCache.icons.Add(uIMiniMapElement);
			}
			result = iconCache.icons[iconCache.numUsed];
			iconCache.numUsed++;
		}
		return result;
	}

	private void ZeroIconCount()
	{
		for (int i = 0; i < m_IconCaches.Length; i++)
		{
			m_IconCaches[i].numUsed = 0;
		}
	}

	private void RecycleUnusedIcons()
	{
		for (int i = 0; i < m_IconCaches.Length; i++)
		{
			IconCache iconCache = m_IconCaches[i];
			while (iconCache.icons.Count > iconCache.numUsed)
			{
				iconCache.icons[iconCache.icons.Count - 1].RectTrans.SetParent(null, worldPositionStays: false);
				iconCache.icons[iconCache.icons.Count - 1].Recycle(worldPosStays: false);
				iconCache.icons.RemoveAt(iconCache.icons.Count - 1);
			}
		}
	}

	private void ClearMiniMap()
	{
		ZeroIconCount();
		RecycleUnusedIcons();
	}

	private void OnShow()
	{
		m_LastScanTime = -2.1474836E+09f;
	}

	private void OnHide()
	{
		ClearMiniMap();
	}

	private void OnPool()
	{
		m_RectRadius = m_RectTrans.rect.width / 2f;
	}

	private void OnSpawn()
	{
		m_MapDisplay.ShowEvent.Subscribe(OnShow);
		m_MapDisplay.HideEvent.Subscribe(OnHide);
		m_IconCaches = new IconCache[ManRadar.IconTypeCount];
		for (int i = 0; i < m_IconCaches.Length; i++)
		{
			m_IconCaches[i] = new IconCache
			{
				numUsed = 0,
				icons = new List<UIMiniMapElement>()
			};
		}
	}

	private void OnRecycle()
	{
		ClearMiniMap();
		m_MapDisplay.ShowEvent.Unsubscribe(OnShow);
		m_MapDisplay.HideEvent.Unsubscribe(OnHide);
	}
}
