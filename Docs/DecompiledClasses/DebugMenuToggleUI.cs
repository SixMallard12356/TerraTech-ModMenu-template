using UnityEngine;
using UnityEngine.UI;

public class DebugMenuToggleUI : DebugMenuUI
{
	[SerializeField]
	private Text m_TextField;

	[SerializeField]
	private Text m_OnOffTextField;

	public override void SetMenuObject(DebugMenuObject menuObject)
	{
		m_MenuData = menuObject;
		m_TextField.text = m_MenuData.Name;
	}

	public override void Show()
	{
		UpdateToggledState();
	}

	private void UpdateToggledState()
	{
		if (m_MenuData is DebugMenuToggle debugMenuToggle)
		{
			m_OnOffTextField.text = (debugMenuToggle.IsToggled() ? "On" : "Off");
		}
	}

	public void OnButtonClick()
	{
		if (m_MenuData != null)
		{
			m_MenuData.TriggerMenuOption();
			UpdateToggledState();
		}
	}
}
