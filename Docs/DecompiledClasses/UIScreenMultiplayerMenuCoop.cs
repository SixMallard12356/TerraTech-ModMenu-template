#define UNITY_EDITOR
using TerraTech.Network;
using UnityEngine;

public class UIScreenMultiplayerMenuCoop : UIScreenMultiplayerMenu
{
	[SerializeField]
	private UIGameMode m_CreateAWorld;

	[SerializeField]
	private UIGameMode m_QuickMatch;

	[SerializeField]
	private UIGameMode m_FindAGame;

	[SerializeField]
	private RectTransform m_PlaySettingsParent;

	public override void OnButtonClickedHost()
	{
		m_CreateAWorld.ShowPlaySettings(m_PlaySettingsParent);
	}

	public void OnButtonClickedLoad()
	{
		UIScreenLoadSave uIScreenLoadSave = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.LoadSave) as UIScreenLoadSave;
		if (uIScreenLoadSave.IsNotNull())
		{
			Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenLoadSave);
			uIScreenLoadSave.SelectTab(11);
		}
		else
		{
			d.LogError("Could not find load game screen");
		}
	}

	public override void ScreenInitialize(ManUI.ScreenType type)
	{
		base.ScreenInitialize(type);
		m_CreateAWorld.Initialise(hide: false);
		m_QuickMatch.Initialise(hide: false);
		m_FindAGame.Initialise(hide: false);
	}

	public override void Hide()
	{
		m_CreateAWorld.HidePlaySettings();
		base.Hide();
	}

	public void ApplyModeSettings()
	{
		Singleton.Manager<ManGameMode>.inst.NextModeSetting = m_CreateAWorld.GetModeSettings();
	}

	public void StartGameMode()
	{
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.SwitchToMode();
	}

	public void HostGame()
	{
		if (m_HasRequestPending)
		{
			d.Log("[UIScreenMultiplayerMenuCoop] - Trying to Create Game - Aborted as m_HasRequestPending == true");
			return;
		}
		d.Log("[UIScreenMultiplayerMenuCoop] - Trying to Create Game");
		if (!Singleton.Manager<ManNetworkLobby>.inst.Inited)
		{
			_showSteamNotInitialisedError();
			return;
		}
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.ClearPingRequests();
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinedEvent.Subscribe(OnLobbyFinishedJoining);
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("WorldSeed", out var settingData);
		Singleton.Manager<ManNetwork>.inst.WorldSeed = settingData as string;
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("WorldBiome", out settingData);
		Singleton.Manager<ManNetwork>.inst.BiomeChoice = settingData as string;
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("WorldGenVersionID", out settingData);
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("WorldGenVersioningType", out var settingData2);
		if (settingData != null && settingData2 != null && int.TryParse(settingData as string, out var result) && int.TryParse(settingData2 as string, out var result2) && (result != 0 || result2 != 0))
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
		d.Log("[UIScreenMultiplayerMenuCoop] - Trying to Create Host Lobby");
		m_HasRequestPending = true;
		object settingData3 = null;
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("LobbyVisibility", out settingData3);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CreateLobby(MultiplayerModeType.CoOpCreative, (Lobby.LobbyVisibility)settingData3);
		d.Log("[UIScreenMultiplayerMenuCoop] - Waiting to join Host Lobby");
	}

	private void OnLobbyFinishedJoining(Lobby newLobby)
	{
		d.Log("[UIScreenMultiplayerMenuCoop] OnLobbyJoined: LobbyName=" + newLobby.Name);
		m_HasRequestPending = false;
		object settingData = null;
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("ModifyOthersTechs", out settingData);
		UpdateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CO_OP_ALLOW_PLAYER_TECH_MODS, ((bool)settingData) ? 1 : 0, newLobby);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.TriggerGameStart();
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinedEvent.Unsubscribe(OnLobbyFinishedJoining);
	}

	private void UpdateLobbyChoice(LobbyData.EnumLobbyChoiceIndex lci, int choice, Lobby lobby)
	{
		d.Log("[UIScreenMultiplayerMenuCoop] UpdateLobbyChoice: " + lci.ToString() + " , choice: " + choice);
		lobby.Data.m_Choices[(int)lci] = choice;
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.SetLobbyChoices(lobby.Data.m_Choices);
		}
	}
}
