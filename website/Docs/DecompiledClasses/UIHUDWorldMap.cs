#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHUDWorldMap : UIHUDElement
{
	[Serializable]
	public struct GridLineVisibility
	{
		public float gridSeparation;

		public float lineWeight;

		public AnimationCurve alphaAtZoom;

		public LocalisedString scaleFormatString;
	}

	private struct LineElement
	{
		public RectTransform rectTrans;

		public Image image;
	}

	[SerializeField]
	private TextMeshProUGUI m_CursorCoordText;

	[SerializeField]
	private GridLineVisibility[] m_GridlineConfig;

	[SerializeField]
	private RectTransform m_GridlinePrefab;

	[SerializeField]
	private RectTransform m_GridLinesContainer;

	[SerializeField]
	private RectTransform m_MapScaleGraphic;

	[SerializeField]
	private TextMeshProUGUI m_MapScaleText;

	[SerializeField]
	private float m_MapScaleGridAlphaThreshold = 0.5f;

	[SerializeField]
	private float m_GamepadPanSpeed = 1f;

	[SerializeField]
	private float m_GamepadZoomSpeed = 1f;

	private UIMiniMapDisplay m_MapDisplay;

	private bool m_PointerInside;

	private Vector3Int m_LastCursorWorldPos = IntVector3.invalid;

	private const int kMaxMoveDistanceSqr = 225;

	private int m_CurBiomeTypeInfoID = -1;

	private const int kBiomeTypeInfoIDInvalid = -1;

	private Color m_GridLineBaseCol;

	private List<LineElement> m_SpawnedGridlines = new List<LineElement>();

	private Vector2 m_GamepadPanAmount = Vector2.zero;

	private PointerEventData m_GamepadPointerEvtData;

	private List<RaycastResult> m_GamepadPointerHitResults = new List<RaycastResult>();

	private readonly int kHighlightedBiomeTypeID = Shader.PropertyToID("_HighlightedBiomeID");

	private readonly int kHighlightedBiomeStartTime = Shader.PropertyToID("_HighlightedBiomeTime");

	private Vector3[] m_WorldCornerCache = new Vector3[4];

	public void CloseMenu()
	{
		HideSelf();
	}

	public override void Show(object context)
	{
		base.Show(context);
		m_MapDisplay.Show();
		UpdateGridLines();
		UpdateMapUnits();
		Singleton.Manager<ManPointer>.inst.EnableCursorEmulation(enable: true, ManPointer.CursorEmulationEnabledReason.WorldMap, resetPosition: true);
		Rect viewportScreenRect = GetViewportScreenRect();
		Singleton.Manager<ManPointer>.inst.SetEmulatedCursorBounds(viewportScreenRect);
		Singleton.Manager<ManInput>.inst.RegisterInputEventDelegate(104, InputActionEventType.AxisActive, OnGamepadZoom);
		Singleton.Manager<ManInput>.inst.RegisterInputEventDelegate(100, InputActionEventType.AxisActive, OnGamepadPanH);
		Singleton.Manager<ManInput>.inst.RegisterInputEventDelegate(47, InputActionEventType.AxisActive, OnGamepadPanV);
		Singleton.Manager<ManInput>.inst.RegisterInputEventDelegate(21, InputActionEventType.ButtonJustPressed, OnGamepadToggleWaypoint);
		Singleton.Manager<ManInput>.inst.RegisterInputEventDelegate(102, InputActionEventType.ButtonJustPressed, OnGamepadRecentreMap);
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_MapDisplay.Hide();
		m_PointerInside = false;
		Shader.SetGlobalFloat(kHighlightedBiomeTypeID, -1f);
		foreach (LineElement spawnedGridline in m_SpawnedGridlines)
		{
			spawnedGridline.rectTrans.SetParent(null, worldPositionStays: false);
			spawnedGridline.rectTrans.Recycle(worldPosStays: false);
		}
		m_SpawnedGridlines.Clear();
		Singleton.Manager<ManPointer>.inst.EnableCursorEmulation(enable: false, ManPointer.CursorEmulationEnabledReason.WorldMap);
		Singleton.Manager<ManPointer>.inst.ResetEmulatedCursorBounds();
		Singleton.Manager<ManInput>.inst.UnregisterInputEventDelegate(104, InputActionEventType.AxisActive, OnGamepadZoom);
		Singleton.Manager<ManInput>.inst.UnregisterInputEventDelegate(100, InputActionEventType.AxisActive, OnGamepadPanH);
		Singleton.Manager<ManInput>.inst.UnregisterInputEventDelegate(47, InputActionEventType.AxisActive, OnGamepadPanV);
		Singleton.Manager<ManInput>.inst.UnregisterInputEventDelegate(21, InputActionEventType.ButtonJustPressed, OnGamepadToggleWaypoint);
		Singleton.Manager<ManInput>.inst.UnregisterInputEventDelegate(102, InputActionEventType.ButtonJustPressed, OnGamepadRecentreMap);
	}

	public GameObject TryGetCursorTarget()
	{
		GameObject result = null;
		if (m_GamepadPointerEvtData == null)
		{
			m_GamepadPointerEvtData = new PointerEventData(EventSystem.current);
		}
		m_GamepadPointerEvtData.position = Singleton.Manager<ManPointer>.inst.DragPositionOnScreen;
		EventSystem.current.RaycastAll(m_GamepadPointerEvtData, m_GamepadPointerHitResults);
		if (m_GamepadPointerHitResults.Count > 0 && m_GamepadPointerHitResults[0].isValid)
		{
			result = m_GamepadPointerHitResults[0].gameObject;
		}
		return result;
	}

	public UIMiniMapElement TryGetWaypoint(GameObject cursorGO)
	{
		UIMiniMapElement uIMiniMapElement = ((cursorGO != null) ? cursorGO.GetComponent<UIMiniMapElement>() : null);
		if (!(uIMiniMapElement != null) || uIMiniMapElement.TrackedVis == null || uIMiniMapElement.TrackedVis.ObjectType != ObjectTypes.Waypoint || uIMiniMapElement.TrackedVis.RadarType != RadarTypes.MapNavTarget)
		{
			return null;
		}
		return uIMiniMapElement;
	}

	private void SetCoordText(Vector3Int pos)
	{
		if (m_CursorCoordText != null && pos != m_LastCursorWorldPos)
		{
			m_LastCursorWorldPos = pos;
			m_CursorCoordText.text = $"{pos.x} , {pos.z}";
		}
	}

	private void DisplayBiomeInfo(int biomeIdx)
	{
		if (m_CurBiomeTypeInfoID != biomeIdx)
		{
			m_CurBiomeTypeInfoID = biomeIdx;
			Shader.SetGlobalFloat(kHighlightedBiomeTypeID, m_CurBiomeTypeInfoID);
			Shader.SetGlobalFloat(kHighlightedBiomeStartTime, Time.time);
		}
	}

	private void UpdateMapUnits()
	{
		float num = float.MaxValue;
		LocalisedString localisedString = null;
		GridLineVisibility[] gridlineConfig = m_GridlineConfig;
		for (int i = 0; i < gridlineConfig.Length; i++)
		{
			GridLineVisibility gridLineVisibility = gridlineConfig[i];
			if (Mathf.Clamp01(gridLineVisibility.alphaAtZoom.Evaluate(m_MapDisplay.CurrentZoomLevel)) > m_MapScaleGridAlphaThreshold && gridLineVisibility.gridSeparation < num)
			{
				num = gridLineVisibility.gridSeparation;
				localisedString = gridLineVisibility.scaleFormatString;
			}
		}
		float num2 = num;
		if (m_MapScaleGraphic != null)
		{
			Vector2 sizeDelta = m_MapScaleGraphic.sizeDelta;
			sizeDelta.x = num2 * m_MapDisplay.WorldToUIUnitRatio * m_MapDisplay.CurrentZoomLevel;
			m_MapScaleGraphic.sizeDelta = sizeDelta;
		}
		if (m_MapScaleText != null)
		{
			m_MapScaleText.text = ((localisedString != null) ? string.Format(localisedString.Value, num2) : num2.ToString());
		}
	}

	private void UpdateGridLines()
	{
		if (!(m_GridLinesContainer != null))
		{
			return;
		}
		float currentZoomLevel = m_MapDisplay.CurrentZoomLevel;
		int num = 0;
		GridLineVisibility[] gridlineConfig = m_GridlineConfig;
		for (int i = 0; i < gridlineConfig.Length; i++)
		{
			GridLineVisibility gridLineVisibility = gridlineConfig[i];
			float num2 = Mathf.Clamp01(gridLineVisibility.alphaAtZoom.Evaluate(currentZoomLevel));
			if (!(num2 > 0f))
			{
				continue;
			}
			foreach (Rect item2 in VisibleLines(gridLineVisibility.gridSeparation, gridLineVisibility.lineWeight))
			{
				LineElement item;
				if (num < m_SpawnedGridlines.Count)
				{
					item = m_SpawnedGridlines[num];
				}
				else
				{
					RectTransform rectTransform = m_GridlinePrefab.SpawnWithLocalTransform(m_GridLinesContainer);
					item = new LineElement
					{
						rectTrans = rectTransform,
						image = rectTransform.GetComponent<Image>()
					};
					m_SpawnedGridlines.Add(item);
				}
				item.rectTrans.anchoredPosition = Vector2Int.RoundToInt(item2.center);
				item.rectTrans.sizeDelta = item2.size;
				item.image.color = m_GridLineBaseCol.SetAlpha(m_GridLineBaseCol.a * num2);
				num++;
			}
		}
		if (num < m_SpawnedGridlines.Count)
		{
			for (int j = num; j < m_SpawnedGridlines.Count; j++)
			{
				LineElement lineElement = m_SpawnedGridlines[j];
				lineElement.rectTrans.SetParent(null, worldPositionStays: false);
				lineElement.rectTrans.Recycle(worldPosStays: false);
			}
			m_SpawnedGridlines.RemoveRange(num, m_SpawnedGridlines.Count - num);
		}
	}

	private IEnumerable<Rect> VisibleLines(float separation, float lineWeight)
	{
		Rect viewportRect = m_MapDisplay.Viewport.rect;
		Vector2 mapPos = m_MapDisplay.MapPosition;
		float separationUI = separation * m_MapDisplay.WorldToUIUnitRatio * m_MapDisplay.CurrentZoomLevel;
		int numHor = Mathf.CeilToInt(viewportRect.width / separationUI);
		float firstPos = viewportRect.xMin + (mapPos.x - viewportRect.xMin) % separationUI - lineWeight * 0.5f;
		float y = Mathf.Floor(viewportRect.yMin);
		float w = lineWeight;
		float h = Mathf.Ceil(viewportRect.height);
		float x;
		for (int i = 0; i < numHor; i++)
		{
			x = Mathf.Round(firstPos + separationUI * (float)i);
			yield return new Rect(x, y, w, h);
		}
		int numVert = Mathf.CeilToInt(viewportRect.height / separationUI);
		firstPos = viewportRect.yMin + (mapPos.y - viewportRect.yMin) % separationUI - lineWeight * 0.5f;
		x = Mathf.Floor(viewportRect.xMin);
		w = Mathf.Ceil(viewportRect.width);
		h = lineWeight;
		for (int i = 0; i < numVert; i++)
		{
			y = Mathf.Round(firstPos + separationUI * (float)i);
			yield return new Rect(x, y, w, h);
		}
	}

	private void TryToggleWaypoint(GameObject cursorGO)
	{
		UIMiniMapElement uIMiniMapElement = TryGetWaypoint(cursorGO);
		if (uIMiniMapElement != null)
		{
			if (ManNetwork.IsHost)
			{
				Singleton.Manager<ManMap>.inst.RemoveNavigationWaypoint(uIMiniMapElement.TrackedVis);
				return;
			}
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.RequestRemoveNavWaypoint, new RequestRemoveNavWaypointMessage
			{
				hostID = uIMiniMapElement.TrackedVis.HostID
			});
			return;
		}
		Vector3Int cursorPosWorld = m_MapDisplay.GetCursorPosWorld();
		if (ManNetwork.IsHost)
		{
			Singleton.Manager<ManMap>.inst.AddNavigationWaypointAtPos(cursorPosWorld);
			return;
		}
		Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.RequestAddNavWaypoint, new RequestAddNavWaypointMessage
		{
			worldPos = cursorPosWorld
		});
	}

	private Rect GetViewportScreenRect()
	{
		m_MapDisplay.Viewport.GetWorldCorners(m_WorldCornerCache);
		Vector3 vector = Singleton.Manager<ManUI>.inst.m_UICamera.WorldToScreenPoint(m_WorldCornerCache[0]);
		Vector3 vector2 = Singleton.Manager<ManUI>.inst.m_UICamera.WorldToScreenPoint(m_WorldCornerCache[1]);
		Singleton.Manager<ManUI>.inst.m_UICamera.WorldToScreenPoint(m_WorldCornerCache[2]);
		Vector3 vector3 = Singleton.Manager<ManUI>.inst.m_UICamera.WorldToScreenPoint(m_WorldCornerCache[3]);
		return new Rect(vector.x, vector.y, vector3.x - vector.x, vector2.y - vector.y);
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		m_PointerInside = true;
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		m_PointerInside = false;
	}

	public void OnPointerUpHandler(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right && (eventData.position - eventData.pressPosition).sqrMagnitude < 225f)
		{
			TryToggleWaypoint(eventData.rawPointerPress);
			eventData.Use();
		}
		else if (eventData.button == PointerEventData.InputButton.Middle)
		{
			m_MapDisplay.Recentre();
			eventData.Use();
		}
	}

	private void OnGamepadZoom(InputActionEventData evtData)
	{
		Vector2 localPoint = Vector2.zero;
		if (m_MapDisplay.AllowsPanning)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(m_MapDisplay.Viewport, Singleton.Manager<ManPointer>.inst.DragPositionOnScreen, Singleton.Manager<ManUI>.inst.m_UICamera, out localPoint);
		}
		m_MapDisplay.ApplyZoomIncrement(evtData.GetAxis() * m_GamepadZoomSpeed, localPoint);
	}

	private void OnGamepadPanH(InputActionEventData evtData)
	{
		m_GamepadPanAmount.x = evtData.GetAxis() * m_GamepadPanSpeed;
	}

	private void OnGamepadPanV(InputActionEventData evtData)
	{
		m_GamepadPanAmount.y = evtData.GetAxis() * m_GamepadPanSpeed;
	}

	private void OnGamepadRecentreMap(InputActionEventData evtData)
	{
		m_MapDisplay.Recentre();
	}

	private void OnGamepadToggleWaypoint(InputActionEventData evtData)
	{
		GameObject cursorGO = TryGetCursorTarget();
		TryToggleWaypoint(cursorGO);
	}

	public void OnZoomChanged()
	{
		UpdateMapUnits();
	}

	public void OnPanChanged()
	{
		UpdateGridLines();
	}

	private void OnPool()
	{
		m_MapDisplay = GetComponentInChildren<UIMiniMapDisplay>();
		d.Assert(m_MapDisplay, "HUD WorldMap does not have radar display child!?");
		if (m_GridlinePrefab != null)
		{
			m_GridLineBaseCol = m_GridlinePrefab.GetComponent<Image>().color;
		}
		m_MapDisplay.PointerUpEvent.Subscribe(OnPointerUpHandler);
		m_MapDisplay.ZoomChangedEvent.Subscribe(OnZoomChanged);
		m_MapDisplay.PanChangedEvent.Subscribe(OnPanChanged);
	}

	private void OnSpawn()
	{
		SetCoordText(Vector3Int.zero);
		AddElementToGroup(ManHUD.HUDGroup.Main);
		AddElementToGroup(ManHUD.HUDGroup.GamepadQuickMenuHUDElements);
		AddElementToGroup(ManHUD.HUDGroup.PreventCursorTargetSelection);
	}

	private void Update()
	{
		if (m_GamepadPanAmount != Vector2.zero)
		{
			m_MapDisplay.ApplyPanOffset(m_GamepadPanAmount);
			m_GamepadPanAmount = Vector2.zero;
		}
		int biomeIdx = -1;
		if (m_PointerInside || Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			Vector3Int cursorPosWorld = m_MapDisplay.GetCursorPosWorld();
			SetCoordText(cursorPosWorld);
			WorldPosition worldPosition = WorldPosition.FromGameWorldPosition((Vector3)cursorPosWorld);
			IntVector2 tileCoord = worldPosition.TileCoord;
			Singleton.Manager<ManMap>.inst.m_MaskImages.TryGetValue(tileCoord, out var value);
			if (value != null)
			{
				Vector2 tileFrac = worldPosition.TileRelativePos.ToVector2XZ() / Singleton.Manager<ManWorld>.inst.TileSize;
				if (GetPixel(value, tileFrac).a > 0.1f)
				{
					biomeIdx = ((Color32)GetPixel(Singleton.Manager<ManMap>.inst.m_TileImages[tileCoord], tileFrac)).r;
				}
			}
			Shader.SetGlobalVector(Shader.PropertyToID("_CursorPos"), new Vector2(cursorPosWorld.x, cursorPosWorld.z) / Singleton.Manager<ManWorld>.inst.TileSize + Vector2.one * 0.5f);
		}
		DisplayBiomeInfo(biomeIdx);
		static Color GetPixel(Texture2D tex, Vector2 vector)
		{
			int x = Mathf.RoundToInt(vector.x * (float)tex.width);
			int y = Mathf.RoundToInt(vector.y * (float)tex.height);
			return tex.GetPixel(x, y);
		}
	}
}
