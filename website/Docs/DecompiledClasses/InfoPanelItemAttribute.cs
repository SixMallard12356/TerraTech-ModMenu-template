using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelItemAttribute : MonoBehaviour
{
	[SerializeField]
	private Image m_Icon;

	[SerializeField]
	private TextMeshProUGUI m_Title;

	[SerializeField]
	private TooltipComponent m_Tooltip;

	private Color m_DefaultColor;

	public void Setup(InfoOverlayDataValues.ItemAttribute data)
	{
		if (m_Icon != null)
		{
			m_Icon.sprite = data.m_Icon;
			m_Icon.color = ((data.m_Color == default(Color)) ? m_DefaultColor : data.m_Color);
		}
		if (m_Title != null)
		{
			m_Title.text = data.m_Title;
		}
		if (m_Tooltip != null)
		{
			m_Tooltip.SetText(data.m_Title);
		}
	}

	private void OnPool()
	{
		m_DefaultColor = m_Icon.color;
	}

	private void OnRecycle()
	{
		if (m_Icon != null)
		{
			m_Icon.sprite = null;
			m_Icon.color = m_DefaultColor;
		}
		if (m_Title != null)
		{
			m_Title.text = string.Empty;
		}
		if (m_Tooltip != null)
		{
			m_Tooltip.SetText(string.Empty);
		}
	}
}
