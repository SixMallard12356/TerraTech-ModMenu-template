#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;

public class QueryableSelectableWithTooltip : QueryableSelectable
{
	[SerializeField]
	private TooltipComponent m_TooltipComponent;

	protected void UpdateTooltip(bool showTooltip)
	{
		if (m_TooltipComponent == null)
		{
			m_TooltipComponent = GetComponent<TooltipComponent>();
		}
		d.Assert(m_TooltipComponent != null, "Tried to show a tooltip without a valid TooltipComponent");
		if (m_TooltipComponent != null)
		{
			m_TooltipComponent.SetActive(showTooltip);
		}
	}

	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);
		UpdateTooltip(showTooltip: true);
	}

	public override void OnDeselect(BaseEventData eventData)
	{
		base.OnDeselect(eventData);
		UpdateTooltip(showTooltip: false);
	}

	public TooltipComponent GetTooltip()
	{
		return m_TooltipComponent;
	}
}
