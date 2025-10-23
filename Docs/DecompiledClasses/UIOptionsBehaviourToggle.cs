#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIOptionsBehaviourToggle : MonoBehaviour, ISubmitHandler, IEventSystemHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
	[SerializeField]
	private Toggle m_Target;

	private Button m_Button;

	public bool interactable
	{
		set
		{
			m_Button.interactable = value;
			m_Target.interactable = value;
		}
	}

	public bool isOn
	{
		get
		{
			return m_Target.isOn;
		}
		set
		{
			m_Target.isOn = value;
		}
	}

	public Toggle.ToggleEvent onValueChanged => m_Target.onValueChanged;

	public void SetValue(bool value)
	{
		m_Target.SetValue(value);
	}

	private void OnPool()
	{
		d.Assert(m_Target != null, "Missing Target reference on UIOptionsBehaviourToggle", this);
		m_Button = GetComponent<Button>();
		d.Assert(m_Button != null, "Missing Button component on UIOptionsBehaviourToggle", this);
	}

	public void OnSubmit(BaseEventData eventData)
	{
		m_Target.OnSubmit(eventData);
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.CheckBox);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		m_Target.OnPointerClick(eventData);
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.CheckBox);
	}

	public void OnSelect(BaseEventData data)
	{
		ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextToggle);
		Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(promptDataByType);
	}

	public void OnDeselect(BaseEventData data)
	{
		ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextToggle);
		Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, promptDataByType.prompts[0].m_InlineGlyphs);
	}
}
