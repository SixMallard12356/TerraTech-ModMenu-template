using Netease.Oddish.Ingame.Sdk.Entity.ContentFilter;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(UIButtonHostLobby))]
[RequireComponent(typeof(Button))]
public class UICoopPlayButton : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	private UIButtonHostLobby m_HostButton;

	private bool m_ModifyOthersTechs;

	private bool m_SkipSeedCheck;

	public void OnPlayClicked()
	{
		UIScreenNewMultiplayerGame uIScreenNewMultiplayerGame = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NewMultiplayerScreen) as UIScreenNewMultiplayerGame;
		if (!(uIScreenNewMultiplayerGame != null))
		{
			return;
		}
		uIScreenNewMultiplayerGame.ApplyCurrentModeSettings();
		if (SKU.IsNetEase && !m_SkipSeedCheck)
		{
			Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("WorldSeed", out var settingData);
			Singleton.Manager<ManNetEase>.inst.CheckEnteredText((string)settingData, BannedWordCheckType.Substitute, delegate(BannedWordCheck response)
			{
				if (response.Status == BannedWordCheckStatus.Approved || response.CheckType == BannedWordCheckType.Substitute)
				{
					Singleton.Manager<ManGameMode>.inst.NextModeSetting.AddModeInitSetting("WorldSeed", response.Content);
					m_SkipSeedCheck = true;
					OnPlayClicked();
				}
			});
			return;
		}
		m_SkipSeedCheck = false;
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("WorldSeed", out var settingData2);
		Singleton.Manager<ManNetwork>.inst.WorldSeed = settingData2 as string;
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("WorldBiome", out settingData2);
		Singleton.Manager<ManNetwork>.inst.BiomeChoice = settingData2 as string;
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("WorldGenVersionID", out settingData2);
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("WorldGenVersioningType", out var settingData3);
		if (settingData2 != null && settingData3 != null && int.TryParse(settingData2 as string, out var result) && int.TryParse(settingData3 as string, out var result2) && (result != 0 || result2 != 0))
		{
			Singleton.Manager<ManNetwork>.inst.WorldGenVersionID = result;
			Singleton.Manager<ManNetwork>.inst.WorldGenVersionType = result;
		}
		else
		{
			Singleton.Manager<ManGameMode>.inst.NextModeSetting.AddModeInitSetting("WorldGenVersionID", Singleton.Manager<ManSaveGame>.inst.CurrentState.WorldGenVersionData.m_VersionID);
			Singleton.Manager<ManGameMode>.inst.NextModeSetting.AddModeInitSetting("WorldGenVersioningType", (int)Singleton.Manager<ManSaveGame>.inst.CurrentState.WorldGenVersionData.m_VersioningType);
			Singleton.Manager<ManNetwork>.inst.WorldGenVersionID = Singleton.Manager<ManSaveGame>.inst.CurrentState.WorldGenVersionData.m_VersionID;
			Singleton.Manager<ManNetwork>.inst.WorldGenVersionType = (int)Singleton.Manager<ManSaveGame>.inst.CurrentState.WorldGenVersionData.m_VersioningType;
		}
		Singleton.Manager<ManNetwork>.inst.SetPiecePlacements = null;
		m_HostButton.OnLobbyCreatedEvent.Subscribe(OnLobbyHosted);
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("LobbyVisibility", out settingData2);
		m_HostButton.SetLobbyVisibility((Lobby.LobbyVisibility)settingData2);
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("ModifyOthersTechs", out settingData2);
		m_ModifyOthersTechs = (bool)settingData2;
		m_HostButton.OnHostLobby();
	}

	private void OnLobbyHosted(bool success)
	{
		if (success && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.SetLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CO_OP_ALLOW_PLAYER_TECH_MODS, m_ModifyOthersTechs ? 1 : 0);
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

	private void Awake()
	{
		m_HostButton = GetComponent<UIButtonHostLobby>();
	}
}
