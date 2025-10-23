using UnityEngine;
using UnityEngine.UI;

public class UIScreenAbout : UIScreen
{
	[SerializeField]
	private Toggle[] m_TabToggles;

	[SerializeField]
	private UIScrollRectSetter[] m_ScrollRectSetter;

	[SerializeField]
	private RectTransform m_TabPanel;

	private int m_Index;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		m_Index = 0;
		m_TabToggles[m_Index].isOn = true;
		if (SKU.ConsoleUI)
		{
			m_TabPanel.gameObject.SetActive(value: false);
			m_ExitButton.gameObject.SetActive(value: false);
		}
	}

	private void Update()
	{
		UIScrollRectSetter uIScrollRectSetter = m_ScrollRectSetter[m_Index % m_ScrollRectSetter.Length];
		if ((bool)uIScrollRectSetter)
		{
			float axis = Singleton.Manager<ManInput>.inst.GetAxis(47);
			uIScrollRectSetter.Scroll((0f - axis) * Time.deltaTime * Globals.inst.m_StickScrollSensitivity);
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(20))
			{
				uIScrollRectSetter.PageUpOrDown(up: false);
			}
			if (Singleton.Manager<ManInput>.inst.GetNegativeButtonDown(20))
			{
				uIScrollRectSetter.PageUpOrDown(up: true);
			}
		}
		if (SKU.ConsoleUI)
		{
			return;
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonUp(41))
		{
			m_TabToggles[m_Index % m_TabToggles.Length].isOn = false;
			m_Index++;
			m_TabToggles[m_Index % m_TabToggles.Length].isOn = true;
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonUp(42))
		{
			m_TabToggles[m_Index % m_TabToggles.Length].isOn = false;
			m_Index--;
			if (m_Index < 0)
			{
				m_Index += m_TabToggles.Length;
			}
			m_TabToggles[m_Index % m_TabToggles.Length].isOn = true;
		}
	}
}
