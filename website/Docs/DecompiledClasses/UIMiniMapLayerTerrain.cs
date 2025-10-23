using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class UIMiniMapLayerTerrain : UIMiniMapLayer, UIMiniMapDisplay.IBoundsProvider
{
	[SerializeField]
	private UIMiniMapTerrainTile m_TileImagePrefab;

	private Dictionary<IntVector2, UIMiniMapTerrainTile> m_TileUIElements = new Dictionary<IntVector2, UIMiniMapTerrainTile>();

	private Dictionary<IntVector2, UIMiniMapTerrainTile> m_TileUIElementsDoubleBuffer = new Dictionary<IntVector2, UIMiniMapTerrainTile>();

	private List<UIMiniMapTerrainTile> m_UIElementPool = new List<UIMiniMapTerrainTile>(32);

	private bool m_BoundsInvalid;

	private int m_PrevTileCount;

	private Rect m_UIMapBounds;

	private IntVector2 m_VisibleTileCoordCurMin = IntVector2.invalid;

	private IntVector2 m_VisibleTileCoordCurMax = IntVector2.invalid;

	private readonly int kMaskTexParamID = Shader.PropertyToID("_MaskTex");

	private readonly int kTileCoordParamID = Shader.PropertyToID("_TileCoord");

	public override void Init(UIMiniMapDisplay mapDisplay)
	{
		base.Init(mapDisplay);
	}

	public override void UpdateLayer()
	{
		if (!m_MapDisplay.AllowsPanning)
		{
			Vector2 vector = m_MapDisplay.WorldToMap(m_MapDisplay.FocalPoint);
			m_RectTrans.pivot = Vector2.one * 0.5f + vector / m_RectTrans.sizeDelta;
			UpdateTileImagesInView();
		}
	}

	public Rect GetBounds()
	{
		if (m_BoundsInvalid)
		{
			m_UIMapBounds = default(Rect);
			foreach (KeyValuePair<Vector2Int, Texture2D> tileImage in Singleton.Manager<ManMap>.inst.m_TileImages)
			{
				m_UIMapBounds.Encapsulate(tileImage.Key);
			}
			float num = Singleton.Manager<ManWorld>.inst.TileSize * m_MapDisplay.WorldToUIUnitRatio;
			m_UIMapBounds.min = m_UIMapBounds.min * num * m_MapDisplay.CurrentZoomLevel;
			m_UIMapBounds.max = m_UIMapBounds.max * num * m_MapDisplay.CurrentZoomLevel;
			m_BoundsInvalid = false;
		}
		return m_UIMapBounds;
	}

	private void AddTileImage(IntVector2 tileCoord, Texture2D tileImage)
	{
		if (!m_TileUIElements.ContainsKey(tileCoord))
		{
			UIMiniMapTerrainTile newUIElement = GetNewUIElement();
			newUIElement.name = $"Tile ({tileCoord.x,2}, {tileCoord.y,2})";
			RawImage tileImage2 = newUIElement.TileImage;
			tileImage2.texture = tileImage;
			Material material = new Material(tileImage2.material);
			material.SetTexture(kMaskTexParamID, Singleton.Manager<ManMap>.inst.m_MaskImages[tileCoord]);
			material.SetVector(kTileCoordParamID, new Vector4(tileCoord.x, tileCoord.y, 0f, 0f));
			tileImage2.material = material;
			float num = Singleton.Manager<ManWorld>.inst.TileSize * m_MapDisplay.WorldToUIUnitRatio;
			newUIElement.RectTrans.sizeDelta = Vector2.one * num;
			newUIElement.RectTrans.anchoredPosition = tileCoord * newUIElement.RectTrans.sizeDelta;
			bool flag = Singleton.Manager<ManMap>.inst.IsTileImageCreated(tileCoord);
			newUIElement.SetIsLoading(!flag);
			m_TileUIElements.Add(tileCoord, newUIElement);
			m_BoundsInvalid = true;
		}
	}

	private UIMiniMapTerrainTile GetNewUIElement()
	{
		if (m_UIElementPool.Count > 0)
		{
			UIMiniMapTerrainTile uIMiniMapTerrainTile = m_UIElementPool[m_UIElementPool.Count - 1];
			m_UIElementPool.RemoveAt(m_UIElementPool.Count - 1);
			uIMiniMapTerrainTile.gameObject.SetActive(value: true);
			return uIMiniMapTerrainTile;
		}
		return m_TileImagePrefab.SpawnWithLocalTransform(m_RectTrans);
	}

	private void ReturnUIElement(UIMiniMapTerrainTile uiTile)
	{
		uiTile.gameObject.SetActive(value: false);
		m_UIElementPool.Add(uiTile);
	}

	private void GetVisibleCoordRange(out IntVector2 minCoord, out IntVector2 maxCoordInclusive)
	{
		Rect rect = m_MapDisplay.Viewport.rect;
		Vector2 mapPosition = m_MapDisplay.MapPosition;
		float num = Singleton.Manager<ManWorld>.inst.TileSize * m_MapDisplay.WorldToUIUnitRatio * m_MapDisplay.CurrentZoomLevel;
		minCoord = new IntVector2(Mathf.FloorToInt((rect.xMin - mapPosition.x) / num + 0.5f), Mathf.CeilToInt((rect.yMin - mapPosition.y) / num - 0.5f));
		maxCoordInclusive = new IntVector2(Mathf.FloorToInt((rect.xMax - mapPosition.x) / num + 0.5f), Mathf.CeilToInt((rect.yMax - mapPosition.y) / num - 0.5f));
	}

	private bool IsVisibleInViewport(IntVector2 tileCoord)
	{
		GetVisibleCoordRange(out var minCoord, out var maxCoordInclusive);
		if (tileCoord.x >= minCoord.x && tileCoord.y >= minCoord.y && tileCoord.x <= maxCoordInclusive.x)
		{
			return tileCoord.y <= maxCoordInclusive.y;
		}
		return false;
	}

	private void UpdateTileImagesInView(bool forceRefresh = false)
	{
		GetVisibleCoordRange(out var minCoord, out var maxCoordInclusive);
		if (!forceRefresh && !(m_VisibleTileCoordCurMin != minCoord) && !(m_VisibleTileCoordCurMax != maxCoordInclusive))
		{
			return;
		}
		Dictionary<IntVector2, UIMiniMapTerrainTile> tileUIElements = m_TileUIElements;
		m_TileUIElements = m_TileUIElementsDoubleBuffer;
		m_TileUIElementsDoubleBuffer = tileUIElements;
		int num = 0;
		Util.CoordIterator enumerator = new Util.CoordIterator(minCoord, maxCoordInclusive).GetEnumerator();
		while (enumerator.MoveNext())
		{
			IntVector2 current = enumerator.Current;
			if (Singleton.Manager<ManMap>.inst.m_TileImages.TryGetValue(current, out var value))
			{
				if (tileUIElements.TryGetValue(current, out var value2))
				{
					tileUIElements.Remove(current);
					m_TileUIElements.Add(current, value2);
				}
				else
				{
					AddTileImage(current, value);
				}
				num++;
			}
		}
		foreach (KeyValuePair<IntVector2, UIMiniMapTerrainTile> item in tileUIElements)
		{
			IntVector2 key = item.Key;
			UIMiniMapTerrainTile value3 = item.Value;
			ReturnUIElement(value3);
			m_TileUIElements.Remove(key);
		}
		tileUIElements.Clear();
		m_BoundsInvalid = m_BoundsInvalid || num != m_PrevTileCount;
		m_PrevTileCount = num;
		m_VisibleTileCoordCurMin = minCoord;
		m_VisibleTileCoordCurMax = maxCoordInclusive;
	}

	private void UpdateScaleFromZoom()
	{
		m_RectTrans.localScale = Vector3.one * m_MapDisplay.CurrentZoomLevel;
	}

	private void OnShow()
	{
		UpdateScaleFromZoom();
		UpdateTileImagesInView(forceRefresh: true);
	}

	private void OnZoomChanged()
	{
		UpdateScaleFromZoom();
		UpdateTileImagesInView();
	}

	private void OnPanChanged()
	{
		UpdateTileImagesInView();
	}

	public void OnTileImagesChanged()
	{
		foreach (KeyValuePair<IntVector2, UIMiniMapTerrainTile> tileUIElement in m_TileUIElements)
		{
			IntVector2 key = tileUIElement.Key;
			UIMiniMapTerrainTile value = tileUIElement.Value;
			if (Singleton.Manager<ManMap>.inst.m_TileImages.TryGetValue(key, out var value2))
			{
				RawImage tileImage = value.TileImage;
				tileImage.texture = value2;
				tileImage.materialForRendering.SetTexture(kMaskTexParamID, Singleton.Manager<ManMap>.inst.m_MaskImages[key]);
				tileImage.materialForRendering.SetVector(kTileCoordParamID, new Vector4(key.x, key.y, 0f, 0f));
				bool flag = Singleton.Manager<ManMap>.inst.IsTileImageCreated(key);
				value.SetIsLoading(!flag);
			}
		}
	}

	public void OnSingleTileImageChanged(Vector2Int tileCoord)
	{
		Singleton.Manager<ManMap>.inst.m_TileImages.TryGetValue(tileCoord, out var value);
		if (IsVisibleInViewport(tileCoord))
		{
			if (m_TileUIElements.TryGetValue(tileCoord, out var value2))
			{
				value2.TileImage.texture = value;
				bool flag = Singleton.Manager<ManMap>.inst.IsTileImageCreated(tileCoord);
				value2.SetIsLoading(!flag);
			}
			else
			{
				AddTileImage(tileCoord, value);
			}
		}
	}

	private void OnSpawn()
	{
		m_MapDisplay.ShowEvent.Subscribe(OnShow);
		m_MapDisplay.ZoomChangedEvent.Subscribe(OnZoomChanged);
		m_MapDisplay.PanChangedEvent.Subscribe(OnPanChanged);
		Singleton.Manager<ManMap>.inst.TileImagesChangedEvent.Subscribe(OnTileImagesChanged);
		Singleton.Manager<ManMap>.inst.TileImageChangedEvent.Subscribe(OnSingleTileImageChanged);
		m_BoundsInvalid = true;
		m_PrevTileCount = -1;
		m_VisibleTileCoordCurMin = IntVector2.invalid;
		m_VisibleTileCoordCurMax = IntVector2.invalid;
	}

	private void OnRecycle()
	{
		foreach (UIMiniMapTerrainTile value in m_TileUIElements.Values)
		{
			value.RectTrans.SetParent(null, worldPositionStays: false);
			value.Recycle(worldPosStays: false);
		}
		m_TileUIElements.Clear();
		m_MapDisplay.ShowEvent.Unsubscribe(OnShow);
		m_MapDisplay.ZoomChangedEvent.Unsubscribe(OnZoomChanged);
		m_MapDisplay.PanChangedEvent.Unsubscribe(OnPanChanged);
		Singleton.Manager<ManMap>.inst.TileImagesChangedEvent.Unsubscribe(OnTileImagesChanged);
		Singleton.Manager<ManMap>.inst.TileImageChangedEvent.Unsubscribe(OnSingleTileImageChanged);
	}
}
