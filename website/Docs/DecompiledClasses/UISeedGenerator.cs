using System.Text;
using Netease.Oddish.Ingame.Sdk.Entity.ContentFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISeedGenerator : MonoBehaviour, UIGameModeSettings.ModeInitSettingProvider, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	[SerializeField]
	private InputField m_InputField;

	private static char[] s_SeedCharacterArray = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

	public void Generate()
	{
		m_InputField.text = GetUniqueKey(15);
	}

	public void AddSeed()
	{
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.AddModeInitSetting("WorldSeed", m_InputField.text);
	}

	public static string GetUniqueKey(int maxSize)
	{
		StringBuilder stringBuilder = new StringBuilder(maxSize);
		int max = s_SeedCharacterArray.Length;
		for (int i = 0; i < maxSize; i++)
		{
			int num = Random.Range(0, max);
			stringBuilder.Append(s_SeedCharacterArray[num]);
		}
		return stringBuilder.ToString();
	}

	public void InitComponent()
	{
		Generate();
	}

	public void AddSettings(ManGameMode.ModeSettings modeSettings)
	{
		modeSettings.AddModeInitSetting("WorldSeed", m_InputField.text);
	}

	public void OnSelect(BaseEventData data)
	{
		ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextRandomise);
		Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(promptDataByType);
		ManBtnPrompt.PromptData promptDataByType2 = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextSetManually);
		Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(promptDataByType2);
	}

	public void OnDeselect(BaseEventData data)
	{
		ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextRandomise);
		Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, promptDataByType.prompts[0].m_InlineGlyphs);
		ManBtnPrompt.PromptData promptDataByType2 = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextSetManually);
		Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, promptDataByType2.prompts[0].m_InlineGlyphs);
	}

	public void Start()
	{
		if ((bool)m_InputField)
		{
			if (SKU.AllowTextInput)
			{
				InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
				submitEvent.AddListener(OnTextEndEdit);
				m_InputField.onEndEdit = submitEvent;
			}
			else
			{
				m_InputField.interactable = false;
			}
		}
	}

	private void OnTextEndEdit(string unused)
	{
		if (SKU.IsNetEase)
		{
			Singleton.Manager<ManNetEase>.inst.CheckEnteredText(m_InputField.text, BannedWordCheckType.Substitute, delegate(BannedWordCheck response)
			{
				if (response.Status == BannedWordCheckStatus.Approved || response.CheckType == BannedWordCheckType.Substitute)
				{
					m_InputField.text = response.Content;
				}
				else
				{
					Generate();
				}
			});
		}
		if (!CheckValidSeed())
		{
			Generate();
		}
	}

	private bool CheckValidSeed()
	{
		m_InputField.text = m_InputField.text.TrimEnd();
		if (m_InputField.text.Length == 0)
		{
			return false;
		}
		if (m_InputField.text.Length > 15)
		{
			m_InputField.text = m_InputField.text.Remove(15, m_InputField.text.Length - 15);
		}
		string text = m_InputField.text;
		for (int i = 0; i < text.Length; i++)
		{
			if (!char.IsLetterOrDigit(text[i]))
			{
				return false;
			}
		}
		return true;
	}
}
