#define UNITY_EDITOR
using System;
using System.Collections;
using System.Net;
using UnityEngine;

namespace TerraTech.Network;

public class ManNetworkLobby : Singleton.Manager<ManNetworkLobby>, LobbySystem.GameStateQuerier
{
	[SerializeField]
	[HideInInspector]
	private LobbySystem m_System;

	[SerializeField]
	private LobbyConstants m_Constants;

	private bool m_CreatingLobbyForInviteHosting;

	private bool m_JoiningLobbyByInvite;

	private LobbySystem.LobbyErrorCode m_LastErrorShown = LobbySystem.LobbyErrorCode.None;

	public Event<bool> GameStartedEvent;

	public bool Inited
	{
		get
		{
			if (m_System != null)
			{
				return m_System.Inited;
			}
			return false;
		}
	}

	public LobbySystem LobbySystem => m_System;

	public LobbyConstants LobbyConstants => m_Constants;

	public NetOptionsAsset[] AvailableGameTypes => LobbySystem.AvailableGameTypes;

	public NetOptionsAsset TeamDeathMatchGameType => LobbySystem.TeamDeathMatchGameType;

	public bool IsJoinOrCreateLobbyRequestActive()
	{
		return m_System.IsJoinOrCreateLobbyRequestActive();
	}

	public IEnumerator InitialisePlatform()
	{
		if ((bool)m_System && m_System.Init(m_Constants, this))
		{
			Init();
		}
		yield break;
	}

	public void TriggerPreGameStart(Lobby lobby)
	{
		Singleton.Manager<ManNetwork>.inst.SetChosenLevelId(lobby.Data.LevelDataChoice);
		InitMapSizeFromPlayerCount(lobby);
	}

	public void TriggerGameStart(Lobby lobby)
	{
		Singleton.Manager<ManNetwork>.inst.StartAsHost();
		if (lobby.HasNoPendingClientConfigs())
		{
			Singleton.Manager<ManNetwork>.inst.SetAllClientsConnected();
		}
		StartGame(lobby, JIP: false);
	}

	public void PreJIP(Lobby lobby)
	{
		if (Singleton.Manager<ManGameMode>.inst.IsCurrentModeMultiplayer())
		{
			Singleton.Manager<ManNetwork>.inst.SetupWaitsForModeSwitch = false;
		}
	}

	public void DoJIP(Lobby lobby)
	{
		StartGame(lobby, JIP: true);
		if (lobby != null)
		{
			switch (lobby.Data.GameTypeChoice)
			{
			case MultiplayerModeType.CoOpCreative:
				Mode<ModeCoOpCreative>.inst.JoinInProgress();
				break;
			case MultiplayerModeType.CoOpCampaign:
				Mode<ModeCoOpCampaign>.inst.JoinInProgress();
				break;
			case MultiplayerModeType.Deathmatch:
				Mode<ModeDeathmatch>.inst.JoinInProgress();
				break;
			default:
				d.LogError("JoinInProgress unimplemented for the current gamemode");
				break;
			}
		}
	}

	private void StartGame(Lobby lobby, bool JIP)
	{
		GameStartedEvent.Send(JIP);
	}

	private void InitMapSizeFromPlayerCount(Lobby lobby)
	{
		ManNetwork.MapSizeOption mapSizeChoice = (ManNetwork.MapSizeOption)lobby.Data.MapSizeChoice;
		if (mapSizeChoice == ManNetwork.MapSizeOption.Auto)
		{
			int count = lobby.GetPlayerList().Count;
			if (count < 5)
			{
				Singleton.Manager<ManNetwork>.inst.SetMapSize(ManNetwork.MapSizeOption.Small);
			}
			else if (count < 9)
			{
				Singleton.Manager<ManNetwork>.inst.SetMapSize(ManNetwork.MapSizeOption.Medium);
			}
			else
			{
				Singleton.Manager<ManNetwork>.inst.SetMapSize(ManNetwork.MapSizeOption.Large);
			}
		}
		else
		{
			Singleton.Manager<ManNetwork>.inst.SetMapSize(mapSizeChoice);
		}
	}

	private void Init()
	{
		d.Assert(m_System != null, "[ManNetworkLobby] Lobby system not found! Did we call PrePool?");
		LobbySystem.EventTriggerPreGameStart.Subscribe(TriggerPreGameStart);
		LobbySystem.EventTriggerGameStart.Subscribe(TriggerGameStart);
		LobbySystem.PreJIPEvent.Subscribe(PreJIP);
		LobbySystem.JIPEvent.Subscribe(DoJIP);
		LobbySystem.PreJoinEvent.Subscribe(ForceLoadOfTerrainBeforeJoin);
		LobbySystem.SendPlayerDataToGameEvent.Subscribe(SendPlayerDataToGame);
		LobbySystem.SendLobbyDataToGameEvent.Subscribe(SetGameOptionsFromLobby);
		LobbySystem.LobbyJoinedEvent.Subscribe(OnLobbyJoined);
		LobbySystem.LobbyErrorEvent.Subscribe(OnLobbyError);
		LobbySystem.HostInvitationsEvent.Subscribe(OnHostInvitations);
		LobbySystem.ClientInvitationEvent.Subscribe(OnClientInvitation);
		LobbySystem.AllClientsConnectedEvent.Subscribe(OnAllClientsConnected);
		LobbySystem.StartAsClientEvent.Subscribe(StartAsClient);
		LobbySystem.StorePlayerConfigEvent.Subscribe(StorePlayerConfig);
		LobbySystem.ForceClientDisconnectionEvent.Subscribe(ForceClientDisconnection);
		LobbySystem.SetSecureTunnelEndpointEvent.Subscribe(SetSecureTunnelEndpoint);
	}

	private void ForceLoadOfTerrainBeforeJoin(Lobby lobby)
	{
		UIScreenNetworkLobby uIScreenNetworkLobby = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.MatchmakingLobbyScreen) as UIScreenNetworkLobby;
		if (uIScreenNetworkLobby != null)
		{
			uIScreenNetworkLobby.ShowConnectingMessage(b: true);
		}
		Singleton.Manager<ManUI>.inst.FadeToBlack();
		Singleton.Manager<ManNetwork>.inst.OnGenerateTerrainForced.Send(paramA: true);
	}

	private void SendPlayerDataToGame(LobbyPlayerData playerData)
	{
		Singleton.Manager<ManNetwork>.inst.StoreLocalPlayerConfig(playerData);
	}

	private void OnAllClientsConnected()
	{
		Singleton.Manager<ManNetwork>.inst.SetAllClientsConnected();
	}

	private void StartAsClient(TTNetworkConnection conn)
	{
		Singleton.Manager<ManNetwork>.inst.StartAsClient(conn);
	}

	private void ForceClientDisconnection(TTNetworkConnection conn)
	{
		Singleton.Manager<ManNetwork>.inst.ForceClientDisconnection(conn);
	}

	private void Awake()
	{
		m_System = LobbySystem.CreateLobbySystem(base.gameObject);
	}

	private void OnDestroy()
	{
		if (LobbySystem != null)
		{
			m_System.Shutdown();
			LobbySystem.EventTriggerPreGameStart.Unsubscribe(TriggerPreGameStart);
			LobbySystem.EventTriggerGameStart.Unsubscribe(TriggerGameStart);
			LobbySystem.PreJIPEvent.Unsubscribe(PreJIP);
			LobbySystem.JIPEvent.Unsubscribe(DoJIP);
			LobbySystem.PreJoinEvent.Unsubscribe(ForceLoadOfTerrainBeforeJoin);
			LobbySystem.SendPlayerDataToGameEvent.Unsubscribe(SendPlayerDataToGame);
			LobbySystem.SendLobbyDataToGameEvent.Unsubscribe(SetGameOptionsFromLobby);
			LobbySystem.LobbyJoinedEvent.Unsubscribe(OnLobbyJoined);
			LobbySystem.LobbyErrorEvent.Unsubscribe(OnLobbyError);
			LobbySystem.HostInvitationsEvent.Unsubscribe(OnHostInvitations);
			LobbySystem.ClientInvitationEvent.Unsubscribe(OnClientInvitation);
			LobbySystem.AllClientsConnectedEvent.Unsubscribe(OnAllClientsConnected);
			LobbySystem.StartAsClientEvent.Unsubscribe(StartAsClient);
			LobbySystem.StorePlayerConfigEvent.Unsubscribe(StorePlayerConfig);
			LobbySystem.ForceClientDisconnectionEvent.Unsubscribe(ForceClientDisconnection);
			LobbySystem.LobbyCreateFailedEvent.Unsubscribe(OnLobbyCreateForInviteHostingFailed);
			LobbySystem.LobbyJoinFailedEvent.Unsubscribe(OnLobbyJoinFromInviteFailed);
			LobbySystem.SetSecureTunnelEndpointEvent.Unsubscribe(SetSecureTunnelEndpoint);
		}
	}

	public void SetGameOptionsFromLobby(Lobby lobby)
	{
		LobbyData data = lobby.Data;
		if (data.TeamMatchChoice == 1)
		{
			Singleton.Manager<ManNetwork>.inst.Options = LobbySystem.TeamDeathMatchGameType.m_Options;
		}
		else
		{
			Singleton.Manager<ManNetwork>.inst.Options = LobbySystem.AvailableGameTypes[(int)data.GameTypeChoice].m_Options;
		}
		Singleton.Manager<ManNetwork>.inst.InventoryAvailable = data.InventoryChoice == 1;
		Singleton.Manager<ManNetwork>.inst.ScavengeItems = data.ScavengerChoice == 1;
		Singleton.Manager<ManNetwork>.inst.KillStreakRewardsEnabled = data.KillStreakRewardsChoice == 1;
		Singleton.Manager<ManNetwork>.inst.KillStreakResetsWhenDestroyedEnabled = data.KillStreakResetWhenDestroyedChoice == 1;
		Singleton.Manager<ManNetwork>.inst.KillStreakRewardStackEnabled = data.KillStreakRewardStackChoice == 1;
		Singleton.Manager<ManNetwork>.inst.KillStreakMaxedAutoClaimRewardEnabled = data.KillStreakMaxedRewardAutoClaimChoice == 1;
		Singleton.Manager<ManNetwork>.inst.KillStreakPreventClaimNearDangerEnabled = data.KillStreakPreventClaimNearDangerChoice == 1;
		Singleton.Manager<ManNetwork>.inst.KillStreakClaimDangerRangeIndex = data.KillStreakClaimDangerRangeChoice;
		Singleton.Manager<ManNetwork>.inst.KillStreakKillThresholdMultiplierIndex = data.KillStreakKillThresholdMultiplierChoice;
		Singleton.Manager<ManNetwork>.inst.KeepLootedBlocksOnRespawnEnabled = data.KeepLootedBlocksOnRespawnChoice == 1;
		Singleton.Manager<ManNetwork>.inst.ClearUnusedInventoryAfterSpawnBubbleEnabled = data.ClearUnusedInventoryAfterSpawnBubbleChoice == 1;
		Singleton.Manager<ManNetwork>.inst.CrateDropsEnabled = data.CrateDropsEnabledChoice == 1;
		Singleton.Manager<ManNetwork>.inst.CrateDropMinDistanceIndex = data.CrateDropMinDistanceChoice;
		Singleton.Manager<ManNetwork>.inst.CrateDropFrequencyIndex = data.CrateDropFrequencyChoice;
		Singleton.Manager<ManNetwork>.inst.CrateDropBlockQuantityIndex = data.CrateDropBlockQuantityChoice;
		Singleton.Manager<ManNetwork>.inst.CrateDropPickupRangeIndex = data.CrateDropPickupRangeChoice;
		Singleton.Manager<ManNetwork>.inst.CrateDropDelayMinsIndex = data.CrateDropDelayMinsChoice;
		Singleton.Manager<ManNetwork>.inst.CabSelfDestruct = data.CabSelfDestructChoice == 1;
		Singleton.Manager<ManNetwork>.inst.CabSelfDestructTimeIndex = data.CabSelfDestructTimeRangeChoice;
		Singleton.Manager<ManNetwork>.inst.CollideToScavengeBlocks = data.CollideToScavengeChoice == 1;
		Singleton.Manager<ManNetwork>.inst.GameDurationTimeInSecs = ManNetwork.MatchDurationOptions[data.DurationChoice].m_durationSecs;
		Singleton.Manager<ManNetwork>.inst.HealInBuildBeam = data.HealBuildChoice == 1;
		Singleton.Manager<ManNetwork>.inst.HealWarmUpTimerInSecs = ManNetwork.HealWarmUpTimeRanges[data.HealWarmUpChoice];
		Singleton.Manager<ManNetwork>.inst.HealRate = ManNetwork.HealRateRanges[data.HealRateChoice];
		Singleton.Manager<ManNetwork>.inst.HealInterruptCooldownInSecs = ManNetwork.HealInterruptCooldownRanges[data.HealInterruptCooldownChoice];
		Singleton.Manager<ManNetwork>.inst.DeathStreakEnabled = data.DeathStreakEnabledChoice == 1;
		Singleton.Manager<ManNetwork>.inst.DeathStreakInitialDeathsRequired = ManNetwork.DeathStreakMinDeathsReqdRanges[data.DeathStreakMinDeathsChoice];
		Singleton.Manager<ManNetwork>.inst.DeathStreakSubsequentDeathsRequired = ManNetwork.DeathStreakSubsDeathsReqdRanges[data.DeathStreakSubsDeathsChoice];
		Singleton.Manager<ManNetwork>.inst.CoOpAllowPlayerTechMods = data.CoOpAllowPlayerTechModsChoice == 1;
	}

	public void LeaveLobby()
	{
		LobbySystem.LeaveLobby();
	}

	public void JoinLobby(TTNetworkID lobbyID, bool fromInvite)
	{
		LobbySystem.JoinLobby(lobbyID, fromInvite);
	}

	public void RequestLobbyJoin(TTNetworkID lobbyID)
	{
		LobbySystem.JoinLobbyAfterUpdate(lobbyID);
	}

	private string GetLocalisedString(LocalisationEnums.SteamLobbyEnterFailReason reason)
	{
		return Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamLobbyEnterFailReason, (int)reason);
	}

	public string ConvertErrorCode(LobbySystem.LobbyErrorCode errorCode)
	{
		switch (errorCode)
		{
		case LobbySystem.LobbyErrorCode.Banned:
			return GetLocalisedString(LocalisationEnums.SteamLobbyEnterFailReason.Banned);
		case LobbySystem.LobbyErrorCode.DoesntExist:
			return GetLocalisedString(LocalisationEnums.SteamLobbyEnterFailReason.DoesntExist);
		case LobbySystem.LobbyErrorCode.Error:
			return GetLocalisedString(LocalisationEnums.SteamLobbyEnterFailReason.Error);
		case LobbySystem.LobbyErrorCode.Limited:
			return GetLocalisedString(LocalisationEnums.SteamLobbyEnterFailReason.Limited);
		case LobbySystem.LobbyErrorCode.LobbyFull:
			return GetLocalisedString(LocalisationEnums.SteamLobbyEnterFailReason.Full);
		case LobbySystem.LobbyErrorCode.MemberBlockedYou:
			return GetLocalisedString(LocalisationEnums.SteamLobbyEnterFailReason.MemberBlockedYou);
		case LobbySystem.LobbyErrorCode.NotAllowed:
			return GetLocalisedString(LocalisationEnums.SteamLobbyEnterFailReason.NotAllowed);
		case LobbySystem.LobbyErrorCode.YouBlockedMember:
			return GetLocalisedString(LocalisationEnums.SteamLobbyEnterFailReason.YouBlockedMember);
		case LobbySystem.LobbyErrorCode.FailedToConnect:
		case LobbySystem.LobbyErrorCode.LostConnection:
			return Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Networking, 0);
		case LobbySystem.LobbyErrorCode.HostDisconnected:
			return Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 47);
		case LobbySystem.LobbyErrorCode.ModsNotSupported:
			return Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 125);
		default:
			return GetLocalisedString(LocalisationEnums.SteamLobbyEnterFailReason.Error);
		}
	}

	public void ReshowLobbyError()
	{
		if (m_LastErrorShown != LobbySystem.LobbyErrorCode.None)
		{
			ShowLobbyError(m_LastErrorShown, returnToAttract: true);
		}
		else
		{
			ShowLobbyError(LobbySystem.LobbyErrorCode.FailedToConnect, returnToAttract: true);
		}
	}

	public void ShowLobbyError(LobbySystem.LobbyErrorCode errorCode, bool returnToAttract)
	{
		if (errorCode != LobbySystem.LobbyErrorCode.Cancelled)
		{
			Action accept;
			if (returnToAttract)
			{
				m_LastErrorShown = errorCode;
				accept = delegate
				{
					m_LastErrorShown = LobbySystem.LobbyErrorCode.None;
					Singleton.Manager<ManUI>.inst.ExitAllScreens();
					Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
				};
			}
			else
			{
				accept = delegate
				{
					Singleton.Manager<ManUI>.inst.RemovePopup();
				};
			}
			string notification = ConvertErrorCode(errorCode);
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			uIScreenNotifications.Set(notification, accept, localisedString);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
		}
		else if (returnToAttract)
		{
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
			Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
		}
	}

	private bool HasInitialLoadFinished()
	{
		if (Singleton.Manager<ManSplashScreen>.inst.HasExited)
		{
			return Singleton.Manager<ManStartup>.inst.GameStarted;
		}
		return false;
	}

	private void OnLobbyJoined(Lobby lobby)
	{
		d.Log("[ManNetworkLobby] OnLobbyJoined: LobbyName=" + lobby.Name + " ID=" + lobby.ID.ToString());
		if (m_CreatingLobbyForInviteHosting)
		{
			Singleton.Manager<ManUI>.inst.RemovePopup();
			m_CreatingLobbyForInviteHosting = false;
			LobbySystem.LobbyCreateFailedEvent.Unsubscribe(OnLobbyCreateForInviteHostingFailed);
		}
		if (m_JoiningLobbyByInvite)
		{
			Singleton.Manager<ManUI>.inst.RemovePopup();
			m_JoiningLobbyByInvite = false;
			LobbySystem.LobbyJoinFailedEvent.Unsubscribe(OnLobbyJoinFromInviteFailed);
		}
		UIScreenNetworkLobby uIScreenNetworkLobby = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.MatchmakingLobbyScreen) as UIScreenNetworkLobby;
		uIScreenNetworkLobby.SetCurrentLobby(lobby);
		Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNetworkLobby);
	}

	private void OnLobbyCreateForInviteHostingFailed(LobbySystem.LobbyErrorCode errorCode)
	{
		Singleton.Manager<ManUI>.inst.RemovePopup();
		m_CreatingLobbyForInviteHosting = false;
		LobbySystem.LobbyCreateFailedEvent.Unsubscribe(OnLobbyCreateForInviteHostingFailed);
		ShowLobbyError(errorCode, returnToAttract: true);
	}

	private void OnLobbyJoinFromInviteFailed(LobbySystem.LobbyErrorCode errorCode)
	{
		Singleton.Manager<ManUI>.inst.RemovePopup();
		m_CreatingLobbyForInviteHosting = false;
		LobbySystem.LobbyJoinFailedEvent.Unsubscribe(OnLobbyJoinFromInviteFailed);
		ShowLobbyError(errorCode, returnToAttract: true);
	}

	private void OnLobbyError(LobbySystem.LobbyErrorCode errorCode)
	{
		d.Log("[ManNetworkLobby] OnLobbyError: errorCode=" + errorCode);
		LobbySystem.LeaveLobby();
		ShowLobbyError(errorCode, returnToAttract: true);
	}

	private void OnClientInvitation(LobbySystem.ClientInviteStatus status)
	{
		if (HasInitialLoadFinished())
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 50);
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			uIScreenNotifications.Set(localisedString, null, null);
			if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Attract && Singleton.Manager<ManGameMode>.inst.GetModePhase() == ManGameMode.GameState.InGame)
			{
				Singleton.Manager<ManUI>.inst.ExitAllScreens();
				LobbySystem.LeaveLobby();
				Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
				m_JoiningLobbyByInvite = true;
				LobbySystem.LobbyJoinFailedEvent.Subscribe(OnLobbyJoinFromInviteFailed);
				status.readyToJoin = true;
			}
			else if (!Singleton.Manager<ManGameMode>.inst.IsSwitchingMode)
			{
				d.Log("[ManNetworkLobby] - Can't handle invite in this game mode, returning to attract screen");
				Singleton.Manager<ManUI>.inst.ExitAllScreens();
				LobbySystem.LeaveLobby();
				Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
				Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
			}
			else
			{
				d.Log("[ManNetworkLobby] Waiting for mode switch to complete before returning to attract screen, to (eventually) handle invite");
			}
		}
	}

	private void OnHostInvitations(LobbySystem.HostInviteStatus status)
	{
		if (HasInitialLoadFinished())
		{
			if (LobbySystem.CurrentLobby != null && !LobbySystem.CurrentLobby.IsLobbyOwner())
			{
				d.Log("[ManNetworkLobby] Currently in someone else's lobby, leaving...");
				LeaveLobby();
			}
			if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.Attract)
			{
				d.Log("[ManNetworkLobby] - Can't handle invite (play together) in this game mode, returning to attract screen");
				Singleton.Manager<ManUI>.inst.ExitAllScreens();
				LeaveLobby();
				Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
			}
			else if (LobbySystem.CurrentLobby != null && LobbySystem.CurrentLobby.IsLobbyOwner() && Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MatchmakingLobbyScreen))
			{
				status.readyToSendInvites = true;
			}
			else if (!LobbySystem.IsJoinOrCreateLobbyRequestActive())
			{
				UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 107);
				uIScreenNotifications.Set(localisedString, null, null);
				Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
				m_CreatingLobbyForInviteHosting = true;
				LobbySystem.LobbyCreateFailedEvent.Subscribe(OnLobbyCreateForInviteHostingFailed);
				LobbySystem.ClearPingRequests();
				LobbySystem.CreateLobby(MultiplayerModeType.Deathmatch, Lobby.LobbyVisibility.Public);
			}
		}
	}

	private void StorePlayerConfig(TTNetworkConnection connection, LobbyPlayerData config)
	{
		Singleton.Manager<ManNetwork>.inst.StorePlayerConfig(connection, config);
	}

	private void SetSecureTunnelEndpoint(EndPoint endPoint)
	{
		Singleton.Manager<ManNetwork>.inst.SetEndPoint(endPoint);
	}

	public bool IsInProgress()
	{
		NetController netController = Singleton.Manager<ManNetwork>.inst.NetController;
		if (netController != null)
		{
			return netController.CurrentPhase == NetController.Phase.Playing;
		}
		return false;
	}

	public bool AlreadyRunning()
	{
		return Singleton.Manager<ManNetwork>.inst.IsActive;
	}

	public bool IsNetControllerNull()
	{
		return Singleton.Manager<ManNetwork>.inst.NetController == null;
	}

	public bool IsTerrainGenerated()
	{
		TileManager tileManager = Singleton.Manager<ManWorld>.inst.TileManager;
		if (Singleton.Manager<ManWorld>.inst.CurrentBiomeMap != null)
		{
			return !tileManager.IsGenerating;
		}
		return false;
	}

	public bool IsInactive()
	{
		return Singleton.Manager<ManNetwork>.inst.CurState == ManNetwork.State.Inactive;
	}

	public bool IsClientWaitingToJoin()
	{
		return Singleton.Manager<ManNetwork>.inst.IsClientWaitingToJoin();
	}

	public bool AllowedToTalk()
	{
		bool result = false;
		if (Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MatchmakingLobbyScreen))
		{
			result = true;
		}
		else
		{
			ManNetwork manNetwork = Singleton.Manager<ManNetwork>.inst;
			if (manNetwork.IsMultiplayer() && manNetwork.MyPlayer != null && (bool)manNetwork.MyPlayer.CurTech && manNetwork.MyPlayer.CurTech.InitialSpawnShieldID == 0)
			{
				result = true;
			}
		}
		return result;
	}

	public bool AllowedToListen()
	{
		bool flag = false;
		bool num = Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MatchmakingLobbyScreen);
		bool flag2 = Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MultiplayerTechSelect);
		if (num)
		{
			return true;
		}
		return !flag2;
	}
}
