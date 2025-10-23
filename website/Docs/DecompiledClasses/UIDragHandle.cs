using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandle : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler
{
	[SerializeField]
	private RectTransform m_DragTransform;

	private Canvas m_Canvas;

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		m_DragTransform.SetAsLastSibling();
	}

	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		Vector2 anchoredPosition = m_DragTransform.anchoredPosition;
		anchoredPosition += eventData.delta / m_Canvas.scaleFactor;
		anchoredPosition = anchoredPosition.Clamp(Vector2.one * 64f, m_Canvas.pixelRect.size * (1f / m_Canvas.scaleFactor) - Vector2.one * 64f);
		m_DragTransform.anchoredPosition = anchoredPosition;
	}

	private void Awake()
	{
		if (m_DragTransform == null)
		{
			m_DragTransform = base.transform.parent as RectTransform;
		}
		m_Canvas = Singleton.Manager<ManHUD>.inst.Canvas;
	}
}
