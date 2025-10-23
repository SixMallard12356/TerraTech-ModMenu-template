using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIBtnPromptButton : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	[SerializeField]
	private ManBtnPrompt.PromptType m_PromptType = ManBtnPrompt.PromptType.ContextSelect;

	[SerializeField]
	private bool m_HUDPrompt;

	public void OnSelect(BaseEventData data)
	{
		if (m_HUDPrompt)
		{
			Singleton.Manager<ManBtnPrompt>.inst.UpdateCurrentHUDPrompt(m_PromptType);
		}
		else
		{
			Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(m_PromptType);
		}
	}

	public void OnDeselect(BaseEventData data)
	{
		if (m_HUDPrompt)
		{
			Singleton.Manager<ManBtnPrompt>.inst.HidePromptType(m_PromptType);
		}
		else
		{
			Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, m_PromptType);
		}
	}
}
