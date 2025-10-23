#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMiniMapDisplay : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IScrollHandler
{
	public interface IBoundsProvider
	{
		Rect GetBounds();
	}

	[SerializeField]
	private float m_WorldUnitsPerUIUnit = 6f;

	[SerializeField]
	[Tooltip("Size of the UI display, in world Units. Used to cull visible elements outside the bounds")]
	private float m_MaxDisplayRange;

	[SerializeField]
	[Header("Configuration")]
	private bool m_CentredOnPlayer;

	[SerializeField]
	private bool m_RotateWithCamera;

	[InspectorVisibilityControl("m_RotateWithCamera", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	[SerializeField]
	[Tooltip("This rotates with the camera when enabled. Can be the same as WorldContainer")]
	private RectTransform m_RotatingContainer;

	[SerializeField]
	private bool m_AllowZooming;

	[SerializeField]
	[InspectorVisibilityControl("m_AllowZooming", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	private float m_ZoomLevel = 1f;

	[SerializeField]
	[InspectorVisibilityControl("m_AllowZooming", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	private MinMaxFloat m_ZoomLimits = new MinMaxFloat(0.5f, 5f);

	[SerializeField]
	[Tooltip("Percent change of current size, per increment")]
	[InspectorVisibilityControl("m_AllowZooming", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	private float m_ZoomSpeed = 10f;

	[InspectorVisibilityControl("m_AllowZooming", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	[SerializeField]
	private bool m_ResetZoomOnShow;

	[SerializeField]
	private bool m_AllowPanning;

	[SerializeField]
	[InspectorVisibilityControl("m_AllowPanning", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	[Tooltip("Top level map container, contains everything from player to tech icons to map tiles etc. This is what moves when the player pans the map; <everything>")]
	private RectTransform m_PanningContainer;

	[SerializeField]
	[InspectorVisibilityControl("m_AllowPanning", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	private UIMiniMapLayer m_BoundsProviderLayer;

	public EventNoParams ShowEvent;

	public EventNoParams HideEvent;

	public EventNoParams ZoomChangedEvent;

	public EventNoParams PanChangedEvent;

	public Event<PointerEventData> PointerDownEvent;

	public Event<PointerEventData> PointerUpEvent;

	private float m_WorldUnitToUIRatio;

	private bool m_IsCurrentlyPanningMap;

	private Camera m_PanCamera;

	private Vector2 m_PanStartingOffset;

	private UIMiniMapLayer[] m_ContentLayers;

	private IBoundsProvider m_BoundsProvider;

	private bool m_LayersNeedUpdating;

	public WorldPosition FocalPoint { get; private set; }

	public Quaternion MapRotation { get; private set; } = Quaternion.identity;

	public float CurrentZoomLevel { get; private set; } = 1f;

	public bool CentredOnPlayer => m_CentredOnPlayer;

	public bool RotatesWithCamera => m_RotateWithCamera;

	public bool AllowsPanning => m_AllowPanning;

	public bool AllowsZooming => m_AllowZooming;

	public float WorldToUIUnitRatio => m_WorldUnitToUIRatio;

	public bool LimitDisplayRange => m_MaxDisplayRange > 0f;

	public float MaxDisplayRange
	{
		get
		{
			if (!m_AllowZooming)
			{
				return m_MaxDisplayRange;
			}
			return m_MaxDisplayRange / CurrentZoomLevel;
		}
	}

	public float MaxDisplayRangeSqr => MaxDisplayRange * MaxDisplayRange;

	public Vector2 MapPosition
	{
		get
		{
			Vector2 zero = Vector2.zero;
			if (m_CentredOnPlayer)
			{
				zero -= WorldToMap(FocalPoint) * CurrentZoomLevel;
			}
			if (m_AllowPanning)
			{
				zero += m_PanningContainer.anchoredPosition;
			}
			return zero;
		}
	}

	public RectTransform Viewport => ((m_PanningContainer != null) ? m_PanningContainer.parent : base.transform) as RectTransform;

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		ShowEvent.Send();
		if (m_AllowZooming && m_ResetZoomOnShow)
		{
			SetZoomLevel(m_ZoomLevel);
		}
		if (m_AllowPanning)
		{
			UpdateFocalPoint();
			CentreOnPosition(FocalPoint);
		}
		UpdateAllLayers();
	}

	public void Hide()
	{
		TankCamera.inst.SetMouseControlEnabled(enabled: true);
		HideEvent.Send();
		base.gameObject.SetActive(value: false);
	}

	public Vector2 SceneToMap(Vector3 scenePosition)
	{
		return (scenePosition + Singleton.Manager<ManWorld>.inst.SceneToGameWorld).ToVector2XZ() * m_WorldUnitToUIRatio;
	}

	public Vector2 WorldToMap(WorldPosition worldPosition)
	{
		return worldPosition.GameWorldPosition.ToVector2XZ() * m_WorldUnitToUIRatio;
	}

	public Rect GetMapBounds()
	{
		if (m_BoundsProvider == null)
		{
			return new Rect(Vector2.zero, Vector2.one * float.MaxValue);
		}
		return m_BoundsProvider.GetBounds();
	}

	public Vector3Int GetCursorPosWorld()
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(Viewport, Singleton.Manager<ManPointer>.inst.DragPositionOnScreen.ToVector2XY(), Singleton.Manager<ManUI>.inst.m_UICamera, out var localPoint);
		if (m_AllowPanning)
		{
			localPoint -= m_PanningContainer.anchoredPosition;
		}
		localPoint /= CurrentZoomLevel;
		return Vector3Int.RoundToInt((localPoint * m_WorldUnitsPerUIUnit).ToVector3XZ());
	}

	public void ApplyPanOffset(Vector2 panOffset)
	{
		if (m_AllowPanning && panOffset.sqrMagnitude > 0.001f)
		{
			Vector2 anchoredPosition = m_PanningContainer.anchoredPosition;
			Vector2 anchoredPos = anchoredPosition + panOffset;
			anchoredPos = ClampToView(anchoredPos);
			if (anchoredPos != anchoredPosition)
			{
				m_PanningContainer.anchoredPosition = anchoredPos;
				PanChangedEvent.Send();
			}
		}
	}

	public void ApplyZoomIncrement(float amount)
	{
		ApplyZoomIncrement(amount, Vector2.zero);
	}

	public void ApplyZoomIncrement(float amount, Vector2 relativeZoomCentre)
	{
		if (m_AllowZooming)
		{
			float num = amount * m_ZoomSpeed;
			float num2 = (100f + num) * 0.01f;
			float zoomLevel = CurrentZoomLevel * num2;
			SetZoomLevel(zoomLevel, relativeZoomCentre);
		}
	}

	public void Recentre()
	{
		if (m_AllowPanning)
		{
			CentreOnPosition(FocalPoint);
		}
	}

	private void CentreOnPosition(WorldPosition worldPosition)
	{
		Vector2 anchoredPos = -WorldToMap(worldPosition) * CurrentZoomLevel;
		anchoredPos = ClampToView(anchoredPos);
		if (anchoredPos != m_PanningContainer.anchoredPosition)
		{
			m_PanningContainer.anchoredPosition = anchoredPos;
			PanChangedEvent.Send();
		}
	}

	private void SetZoomLevel(float zoomLevel)
	{
		SetZoomLevel(zoomLevel, Vector2.zero);
	}

	private void SetZoomLevel(float zoomLevel, Vector2 relativeZoomCentre)
	{
		float currentZoomLevel = CurrentZoomLevel;
		zoomLevel = Mathf.Clamp(zoomLevel, m_ZoomLimits.Min, m_ZoomLimits.Max);
		if (zoomLevel == CurrentZoomLevel)
		{
			return;
		}
		CurrentZoomLevel = zoomLevel;
		if (m_AllowPanning)
		{
			float num = CurrentZoomLevel / currentZoomLevel;
			Vector2 anchoredPosition = m_PanningContainer.anchoredPosition;
			Vector2 anchoredPos = anchoredPosition * num;
			anchoredPos += relativeZoomCentre * (1f - num);
			anchoredPos = ClampToView(anchoredPos);
			if (anchoredPos != anchoredPosition)
			{
				m_PanningContainer.anchoredPosition = anchoredPos;
				PanChangedEvent.Send();
			}
		}
		ZoomChangedEvent.Send();
		m_LayersNeedUpdating = true;
	}

	private Vector2 ClampToView(Vector2 anchoredPos)
	{
		Vector2 result = anchoredPos;
		if (m_AllowPanning)
		{
			Rect mapBounds = GetMapBounds();
			Vector2 vector = m_PanningContainer.rect.size * 0.5f;
			mapBounds.min -= vector;
			mapBounds.max += vector;
			result = mapBounds.Clamp(-anchoredPos) * -1f;
		}
		return result;
	}

	private void UpdateContainerTransforms()
	{
		if (m_AllowPanning && m_IsCurrentlyPanningMap)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(m_PanningContainer, Input.mousePosition.ToVector2XY(), m_PanCamera, out var localPoint);
			ApplyPanOffset(localPoint - m_PanStartingOffset);
		}
		if (m_RotateWithCamera)
		{
			float num = Vector3.SignedAngle(Singleton.cameraTrans.forward.SetY(0f), Vector3.forward, Vector3.up);
			MapRotation = Quaternion.Euler(0f, 0f, 0f - num);
			m_RotatingContainer.rotation = MapRotation;
		}
	}

	private void UpdateAllLayers()
	{
		UIMiniMapLayer[] contentLayers = m_ContentLayers;
		for (int i = 0; i < contentLayers.Length; i++)
		{
			contentLayers[i].UpdateLayer();
		}
	}

	private void UpdateFocalPoint()
	{
		Transform transform = ((Singleton.playerTank != null) ? Singleton.playerTank.trans : Singleton.cameraTrans);
		FocalPoint = WorldPosition.FromScenePosition(transform.position);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		PointerDownEvent.Send(eventData);
		if (m_AllowPanning && !eventData.used && eventData.button == PointerEventData.InputButton.Left)
		{
			m_IsCurrentlyPanningMap = true;
			m_PanCamera = eventData.pressEventCamera;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(m_PanningContainer, Input.mousePosition.ToVector2XY(), m_PanCamera, out m_PanStartingOffset);
		}
		else if (!eventData.used && eventData.button == PointerEventData.InputButton.Right)
		{
			TankCamera.inst.SetMouseControlEnabled(enabled: false);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		PointerUpEvent.Send(eventData);
		if (!eventData.used && eventData.button == PointerEventData.InputButton.Left)
		{
			m_IsCurrentlyPanningMap = false;
			m_PanCamera = null;
		}
		else if (eventData.button == PointerEventData.InputButton.Right)
		{
			TankCamera.inst.SetMouseControlEnabled(enabled: true);
		}
	}

	public void OnScroll(PointerEventData eventData)
	{
		if (m_AllowZooming)
		{
			Vector2 localPoint = Vector2.zero;
			if (m_AllowPanning)
			{
				RectTransformUtility.ScreenPointToLocalPointInRectangle(m_PanningContainer.parent as RectTransform, eventData.position, eventData.enterEventCamera, out localPoint);
			}
			ApplyZoomIncrement(eventData.scrollDelta.y, localPoint);
		}
	}

	private void OnPool()
	{
		d.Assert(!m_AllowPanning || (bool)m_PanningContainer, "If m_AllowPanning is enabled, the panning container must be supplied!");
		m_ContentLayers = GetComponentsInChildren<UIMiniMapLayer>();
		m_BoundsProvider = m_BoundsProviderLayer as IBoundsProvider;
	}

	private void OnSpawn()
	{
		m_WorldUnitToUIRatio = 1f / m_WorldUnitsPerUIUnit;
		UIMiniMapLayer[] contentLayers = m_ContentLayers;
		for (int i = 0; i < contentLayers.Length; i++)
		{
			contentLayers[i].Init(this);
		}
		if (m_AllowZooming)
		{
			SetZoomLevel(m_ZoomLevel);
		}
	}

	private void Update()
	{
		UpdateFocalPoint();
		UpdateContainerTransforms();
		if (Time.frameCount % 2 == 0)
		{
			m_LayersNeedUpdating = true;
		}
		if (m_LayersNeedUpdating)
		{
			UpdateAllLayers();
			m_LayersNeedUpdating = false;
		}
	}

	private void OnValidate()
	{
		d.Assert(m_BoundsProviderLayer == null || m_BoundsProviderLayer is IBoundsProvider, $"Invalid object {m_BoundsProviderLayer} supplied as BoundsProvider; needs to implement type IBoundsProvider");
		m_WorldUnitToUIRatio = 1f / m_WorldUnitsPerUIUnit;
	}
}
