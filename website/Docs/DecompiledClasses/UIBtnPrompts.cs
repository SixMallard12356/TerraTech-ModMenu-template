#define UNITY_EDITOR
using UnityEngine;

public class UIBtnPrompts : UIHUDElement
{
	[SerializeField]
	private RectTransform m_Panel;

	private ButtonPrompts m_PromptControl;

	public override void Show(object context)
	{
		if (!base.IsVisible)
		{
			base.Show(context);
		}
		if (context is ManBtnPrompt.PromptData data)
		{
			if (m_PromptControl == null)
			{
				m_PromptControl = new ButtonPrompts(m_Panel);
			}
			m_PromptControl.SpawnPrompts(data);
		}
		else
		{
			d.LogError("UIBtnPrompts.Show - Calling Show on UIBtnPrompts with null context.");
		}
	}

	public void UpdateCurrentPrompt(LocalisedString replacementString)
	{
		m_PromptControl.UpdateCurrentPrompt(replacementString, replacementString.m_InlineGlyphs);
	}

	public void ToggleBtnPrompt(bool active, Localisation.GlyphInfo[] rewiredActionIds)
	{
		m_PromptControl.ToggleBtnPrompt(active, rewiredActionIds);
	}

	public override void Hide(object context)
	{
		if (m_PromptControl != null)
		{
			m_PromptControl.Cleanup();
		}
		base.Hide(context);
	}
}
