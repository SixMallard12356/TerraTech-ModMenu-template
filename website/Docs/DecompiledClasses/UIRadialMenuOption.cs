#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRadialMenuOption : Selectable
{
	[SerializeField]
	private bool m_AddTooltip;

	private Vector2 m_SegmentCentreVector;

	private TooltipComponent m_TooltipComponent;

	private float m_SegmentArc;

	protected bool m_IsInside;

	private bool m_IsHighlighted;

	private bool m_TooltipEnabled = true;

	public new bool IsHighlighted => m_IsHighlighted;

	public TooltipComponent TooltipComponent
	{
		get
		{
			if (m_TooltipComponent == null)
			{
				m_TooltipComponent = GetComponent<TooltipComponent>();
			}
			return m_TooltipComponent;
		}
	}

	public bool TooltipEnabled
	{
		get
		{
			return m_TooltipEnabled;
		}
		set
		{
			m_TooltipEnabled = value;
			UpdateTooltip(m_IsInside);
		}
	}

	public virtual void Deselect()
	{
		m_IsInside = (m_IsHighlighted = false);
		DoStateTransition(SelectionState.Normal, instant: false);
	}

	public void Init(float centerAngle, float segmentArc)
	{
		m_SegmentCentreVector = new Vector2(Mathf.Sin(centerAngle), Mathf.Cos(centerAngle)).normalized;
		m_SegmentArc = Mathf.Cos(segmentArc * 0.5f);
	}

	public bool IsInside(Vector2 mousePos)
	{
		return Vector2.Dot(m_SegmentCentreVector, mousePos.normalized) > m_SegmentArc;
	}

	public virtual void SetIsInside(bool isInside)
	{
		if (isInside != m_IsInside)
		{
			m_IsInside = isInside;
			UpdateTooltip(isInside);
		}
		if (base.interactable && isInside != m_IsHighlighted)
		{
			m_IsHighlighted = isInside;
			if (m_IsHighlighted)
			{
				DoStateTransition(SelectionState.Highlighted, instant: false);
			}
			else
			{
				DoStateTransition(SelectionState.Normal, instant: false);
			}
		}
	}

	public void ResetState()
	{
		SetIsInside(isInside: false);
		m_IsHighlighted = false;
		UpdateTooltip(showTooltip: false);
		DoStateTransition(SelectionState.Normal, instant: false);
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
	}

	protected void UpdateTooltip(bool showTooltip)
	{
		if (m_AddTooltip)
		{
			if (m_TooltipComponent == null)
			{
				m_TooltipComponent = GetComponent<TooltipComponent>();
			}
			d.Assert(m_TooltipComponent != null, "Tried to show a tooltip without a valid TooltipComponent");
			if (m_TooltipComponent != null)
			{
				m_TooltipComponent.SetActive(showTooltip & m_TooltipEnabled);
			}
		}
	}
}
