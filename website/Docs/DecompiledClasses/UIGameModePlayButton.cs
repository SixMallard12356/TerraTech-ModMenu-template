using Netease.Oddish.Ingame.Sdk.Entity.ContentFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIGameModePlayButton : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	[SerializeField]
	private bool m_SetModeSettingsFromGameMode = true;

	[SerializeField]
	private bool m_StartGameMode = true;

	private UIGameMode m_GameMode;

	private bool m_SkipSeedCheck;

	private void Awake()
	{
	}

	public void Initialise(UIGameMode mode)
	{
		m_GameMode = mode;
	}

	public void OnPlayClicked()
	{
		if (SKU.IsNetEase && !m_SkipSeedCheck)
		{
			m_GameMode.GetModeSettings().GetModeInitSetting("WorldSeed", out var settingData);
			string text = (string)settingData;
			Singleton.Manager<ManNetEase>.inst.CheckEnteredText(text, BannedWordCheckType.Substitute, delegate(BannedWordCheck response)
			{
				if (response.Status == BannedWordCheckStatus.Approved || response.CheckType == BannedWordCheckType.Substitute)
				{
					m_GameMode.GetModeSettings().AddModeInitSetting("WorldSeed", response.Content);
					m_SkipSeedCheck = true;
					OnPlayClicked();
				}
			});
			return;
		}
		m_SkipSeedCheck = false;
		UIScreenNewGame uIScreenNewGame = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NewGame) as UIScreenNewGame;
		if (uIScreenNewGame != null)
		{
			if (m_SetModeSettingsFromGameMode)
			{
				uIScreenNewGame.ApplyModeSettings(m_GameMode);
			}
			if (m_StartGameMode)
			{
				uIScreenNewGame.StartGameMode();
			}
		}
	}

	public void OnSelect(BaseEventData data)
	{
		ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextPlay);
		Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(promptDataByType);
	}

	public void OnDeselect(BaseEventData data)
	{
		ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextPlay);
		Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, promptDataByType.prompts[0].m_InlineGlyphs);
	}
}
