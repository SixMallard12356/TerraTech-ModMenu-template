using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonWithTooltip : SelectableButton
{
	[SerializeField]
	private TooltipComponent m_Tooltip;

	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);
		if (m_Tooltip != null)
		{
			m_Tooltip.SetActive(active: true);
		}
	}

	public override void OnDeselect(BaseEventData eventData)
	{
		base.OnDeselect(eventData);
		if (m_Tooltip != null)
		{
			m_Tooltip.SetActive(active: false);
		}
	}
}
