using UnityEngine;
using UnityEngine.UI;

public class DebugMenuFolderUI : DebugMenuUI
{
	[SerializeField]
	private Text m_TextField;

	public override void SetMenuObject(DebugMenuObject menuObject)
	{
		m_MenuData = menuObject;
		m_TextField.text = m_MenuData.Name;
	}

	public void OnButtonClick()
	{
		if (m_MenuData != null)
		{
			m_MenuData.TriggerMenuOption();
		}
	}
}
