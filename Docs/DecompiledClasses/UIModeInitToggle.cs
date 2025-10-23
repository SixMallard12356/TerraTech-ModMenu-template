#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIModeInitToggle : MonoBehaviour, UIGameModeSettings.ModeInitSettingProvider, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	[SerializeField]
	private string m_InitSettingName;

	[SerializeField]
	private bool m_ToggledByDefault;

	[SerializeField]
	private bool m_InvertSetting;

	private Toggle m_SettingToggle;

	public virtual void InitComponent()
	{
		if (m_SettingToggle == null)
		{
			m_SettingToggle = GetComponentInChildren<Toggle>();
			d.Assert(m_SettingToggle != null, "UIModeInitToggle Could not find Toggle button on this component or its children!");
		}
		m_SettingToggle.isOn = IsToggledByDefault();
	}

	public void AddSettings(ManGameMode.ModeSettings modeSettings)
	{
		bool flag = m_SettingToggle.isOn != m_InvertSetting;
		modeSettings.AddModeInitSetting(m_InitSettingName, flag);
	}

	protected virtual bool IsToggledByDefault()
	{
		return m_ToggledByDefault;
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
