using UnityEngine;

public class ScreenBtnPrompts : MonoBehaviour
{
	[SerializeField]
	private RectTransform m_Panel;

	private ButtonPrompts m_PromptControl;

	public void ShowPrompt(ManBtnPrompt.PromptData context)
	{
		if (m_PromptControl == null)
		{
			m_PromptControl = new ButtonPrompts(m_Panel);
		}
		m_PromptControl.SpawnPrompts(context);
	}

	public void UpdateCurrentPrompt(LocalisedString replacementString)
	{
		if (m_PromptControl != null)
		{
			m_PromptControl.UpdateCurrentPrompt(replacementString, replacementString.m_InlineGlyphs);
		}
	}

	public void ToggleBtnPrompt(bool active, Localisation.GlyphInfo[] rewiredActionIds)
	{
		if (m_PromptControl != null)
		{
			m_PromptControl.ToggleBtnPrompt(active, rewiredActionIds);
		}
	}

	public void HidePrompt()
	{
		if (m_PromptControl != null)
		{
			m_PromptControl.Cleanup();
		}
	}
}
