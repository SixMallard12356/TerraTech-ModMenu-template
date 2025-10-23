#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIMiniMapElement : MonoBehaviour
{
	private Image m_Icon;

	private RectTransform m_RectTransform;

	private TooltipComponent m_TooltipComponent;

	public Image Icon => m_Icon;

	public RectTransform RectTrans => m_RectTransform;

	public TrackedVisible TrackedVis { get; set; }

	public void EnableTooltip(string text, UITooltipOptions mode = UITooltipOptions.Default)
	{
		d.AssertFormat(m_TooltipComponent != null, base.gameObject, "Map element {0} does not have a tooltip component!?", base.name);
		if (m_TooltipComponent != null)
		{
			m_TooltipComponent.enabled = true;
			m_TooltipComponent.SetPointerEnabled(enabled: true);
			m_TooltipComponent.Mode = mode;
			m_TooltipComponent.SetText(text);
		}
	}

	public void DisableTooltip()
	{
		if (m_TooltipComponent != null)
		{
			m_TooltipComponent.SetPointerEnabled(enabled: false);
			m_TooltipComponent.enabled = false;
		}
	}

	private void OnPool()
	{
		m_Icon = GetComponent<Image>();
		m_RectTransform = m_Icon.rectTransform;
		m_TooltipComponent = GetComponent<TooltipComponent>();
		if (m_TooltipComponent != null)
		{
			m_TooltipComponent.SetPointerEnabled(enabled: false);
		}
	}

	private void OnRecycle()
	{
		DisableTooltip();
	}
}
