using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AdvertisingPanelIndexButton : MonoBehaviour
{
	protected Color m_SelectedColor = Color.white;

	protected Color m_DeselectedColor = new Color(1f, 1f, 1f, 0.6f);

	protected int m_Index = -1;

	protected AdvertisingPanel m_AdvertisingPanel;

	protected Button m_Button;

	public void Init(AdvertisingPanel advertisingPanel, int index)
	{
		m_Index = index;
		m_AdvertisingPanel = advertisingPanel;
		if (m_Button == null)
		{
			m_Button = GetComponent<Button>();
			m_SelectedColor = m_Button.colors.highlightedColor;
			m_DeselectedColor = m_Button.colors.normalColor;
		}
	}

	public void SetHighlighted(bool state)
	{
		ColorBlock colors = m_Button.colors;
		colors.normalColor = (state ? m_SelectedColor : m_DeselectedColor);
		m_Button.colors = colors;
	}

	public void UI_OnButtonClick()
	{
		m_AdvertisingPanel.SelectBanner(m_Index);
	}

	private void OnRecycle()
	{
		m_Index = -1;
		m_AdvertisingPanel = null;
	}
}
