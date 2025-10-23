#define UNITY_EDITOR
using System.Collections.Generic;
using Rewired.Integration.UnityUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInteractionHUD : UIHUDElement
{
	[SerializeField]
	private Image m_CursorGraphic;

	private RectTransform m_RectTransform;

	private GameCursor.CursorState m_LastCursorState;

	private PointerEventData m_PointerEventData;

	private List<RaycastResult> m_RaycastResultCache = new List<RaycastResult>();

	private EventSystem m_CachedEventSystem;

	private PayloadStandaloneInputModule m_InputModule;

	private static ExecuteEvents.EventFunction<IInteractionCursorEnterHandler> m_CustomPointerEnterHandler = CustomPointerEnter;

	private static ExecuteEvents.EventFunction<IInteractionCursorExitHandler> m_CustomPointerExitHandler = CustomPointerExit;

	public override void Show(object context)
	{
		UpdateFromDragCursorPosition();
		UpdateCursorState();
		base.Show(context);
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		SendInteractionCursorEnterAndExitEvents(null);
	}

	public void PointToPos(Vector3 pos)
	{
		Vector2 screenPos = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(Singleton.camera, pos);
		PointToScreenPos(screenPos);
	}

	private void PointToScreenPos(Vector2 screenPos)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(Singleton.Manager<ManHUD>.inst.Canvas.transform as RectTransform, screenPos, Singleton.Manager<ManHUD>.inst.Canvas.worldCamera, out var localPoint);
		m_RectTransform.anchoredPosition = localPoint;
	}

	private void UpdateFromDragCursorPosition()
	{
		PointToScreenPos(Singleton.Manager<ManPointer>.inst.DragPositionOnScreen.ToVector2XY());
	}

	private void UpdateCursorState()
	{
		GameCursor.CursorState cursorState = GameCursor.GetCursorState();
		if (m_LastCursorState != cursorState)
		{
			TrySetCursorState(cursorState);
			m_LastCursorState = cursorState;
		}
	}

	private void TrySetCursorState(GameCursor.CursorState cursorState)
	{
		CursorMode mode;
		CursorDataTable.CursorData currentCursorData = Singleton.Manager<ManUI>.inst.CursorDataTable.GetCurrentCursorData(CursorDataTable.PlatformSetTypes.Console, out mode, (int)cursorState);
		if (currentCursorData.m_Sprite != null)
		{
			m_CursorGraphic.sprite = currentCursorData.m_Sprite;
			m_CursorGraphic.SetNativeSize();
			m_CursorGraphic.rectTransform.anchoredPosition = new Vector2(currentCursorData.m_Hotspot.x, 0f - currentCursorData.m_Hotspot.y);
		}
	}

	private void SendInteractionCursorEnterAndExitEvents(GameObject newSelection)
	{
		if ((bool)m_InputModule)
		{
			m_InputModule.HandleCustomPointerExitAndEnter(m_PointerEventData, newSelection, m_CustomPointerEnterHandler, m_CustomPointerExitHandler);
		}
	}

	private void OnPool()
	{
		m_RectTransform = base.transform as RectTransform;
	}

	private void OnRecycle()
	{
		TrySetCursorState(GameCursor.CursorState.Default);
		m_LastCursorState = GameCursor.CursorState.Default;
	}

	private void Update()
	{
		if (m_CachedEventSystem == null && EventSystem.current != null)
		{
			m_CachedEventSystem = EventSystem.current;
			m_InputModule = m_CachedEventSystem.currentInputModule as PayloadStandaloneInputModule;
			d.AssertFormat(m_InputModule != null, "Was expecting to find PayloadStandaloneInputModule as inputmodule on EventSystem, but got {0}", (m_CachedEventSystem.currentInputModule != null) ? m_CachedEventSystem.currentInputModule.GetType().ToString() : "Null");
		}
		if (!(m_CachedEventSystem != null) || !(m_InputModule != null))
		{
			return;
		}
		if (m_PointerEventData == null)
		{
			m_PointerEventData = new PointerEventData(m_CachedEventSystem);
		}
		m_PointerEventData.Reset();
		m_PointerEventData.position = Singleton.Manager<ManPointer>.inst.DragPositionOnScreen.ToVector2XY();
		m_CachedEventSystem.RaycastAll(m_PointerEventData, m_RaycastResultCache);
		GameObject newSelection = null;
		for (int i = 0; i < m_RaycastResultCache.Count; i++)
		{
			if (m_RaycastResultCache[i].gameObject != null)
			{
				newSelection = m_RaycastResultCache[i].gameObject;
				break;
			}
		}
		m_RaycastResultCache.Clear();
		SendInteractionCursorEnterAndExitEvents(newSelection);
	}

	private void LateUpdate()
	{
		if (base.IsVisible)
		{
			UpdateFromDragCursorPosition();
			UpdateCursorState();
		}
	}

	private static void CustomPointerEnter(IInteractionCursorEnterHandler handler, BaseEventData eventData)
	{
		handler.OnInteractionCursorEnter(eventData as PointerEventData);
	}

	private static void CustomPointerExit(IInteractionCursorExitHandler handler, BaseEventData eventData)
	{
		handler.OnInteractionCursorExit(eventData as PointerEventData);
	}
}
