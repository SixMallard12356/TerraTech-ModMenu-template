#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIScreenNetworkLobby : UIScreen
{
	private struct ChatMessage
	{
		public string m_Format;

		public string m_UserMsg;

		public TTNetworkID m_UserId;

		public string m_UserName;
	}

	[SerializeField]
	private UINetworkLobbyPlayerEntry m_PlayerEntryPrefab;

	[SerializeField]
	private UINetworkLobbyTeam m_TeamEntryPrefab;

	[SerializeField]
	private Transform m_PlayerEntryPanel;

	[SerializeField]
	private Transform m_PlayerEntryViewport;

	[SerializeField]
	private Transform m_TeamEntryPanel;

	[SerializeField]
	private Transform m_TeamEntryViewport;

	[SerializeField]
	private Button m_ServerStartButton;

	[SerializeField]
	private Text m_ChatWindow;

	[SerializeField]
	private InputField m_ChatInput;

	[SerializeField]
	private ScrollRect m_ScrollRect;

	[SerializeField]
	private GameObject m_InternalOptionsPanel;

	[SerializeField]
	private Transform m_GameTypeOption;

	[SerializeField]
	private Dropdown m_GameTypeDropDown;

	[SerializeField]
	private UIOptionsBehaviourDropdown m_DurationDropDown;

	[SerializeField]
	private UIOptionsBehaviourDropdown m_VisibilityDropdown;

	[SerializeField]
	private Toggle m_InventoryToggle;

	[SerializeField]
	private Toggle m_ScavengerToggle;

	[SerializeField]
	private UIOptionsBehaviourToggle m_KillStreakToggle;

	[SerializeField]
	private Toggle m_KillStreakResetWhenDestroyedToggle;

	[SerializeField]
	private Toggle m_KillStreakRewardStackToggle;

	[SerializeField]
	private Toggle m_KillStreakMaxRewardAutoClaimToggle;

	[SerializeField]
	private Toggle m_KillStreakPreventClaimNearDangerToggle;

	[SerializeField]
	private Dropdown m_KillStreakClaimDangerRangeDropDown;

	[SerializeField]
	private Dropdown m_KillStreakKillThresholdMuptiplierDropDown;

	[SerializeField]
	private Toggle m_CabSelfDestructToggle;

	[SerializeField]
	private Dropdown m_CabSelfDestructTimeRangeDropDown;

	[SerializeField]
	private UIOptionsBehaviourDropdown m_MapSizeDropDown;

	[SerializeField]
	private UIOptionsBehaviourToggle m_TeamToggle;

	[SerializeField]
	private Toggle m_CollideToScavenge;

	[SerializeField]
	private Toggle m_ReplenishAwardedBlocksToggle;

	[SerializeField]
	private Toggle m_ClearUnusedInventoryAfterSpawnBubbleToggle;

	[SerializeField]
	private Toggle m_HealToggle;

	[SerializeField]
	private Dropdown m_HealWarmUpDropDown;

	[SerializeField]
	private Dropdown m_HealRateDropDown;

	[SerializeField]
	private Dropdown m_HealInterruptDropDown;

	[SerializeField]
	private UIOptionsBehaviourToggle m_CrateDropsToggle;

	[SerializeField]
	private Dropdown m_CrateDropMinDistanceDropDown;

	[SerializeField]
	private UIOptionsBehaviourDropdown m_CrateDropFrequencyDropDown;

	[SerializeField]
	private Dropdown m_CrateDropBlockQuantityDropDown;

	[SerializeField]
	private Dropdown m_CrateDropPickupRangeDropDown;

	[SerializeField]
	private Dropdown m_CrateDropDelayMinsDropDown;

	[SerializeField]
	private UIOptionsBehaviourToggle m_DeathStreakToggle;

	[SerializeField]
	private Dropdown m_DeathStreakNumDeathsRewardDropDown;

	[SerializeField]
	private Dropdown m_DeathStreakSubsNumDeathsRewardDropDown;

	[SerializeField]
	private Toggle m_CoOpAllowPlayerModsToggle;

	[SerializeField]
	private GameObject m_ConnectingMessageContainer;

	[SerializeField]
	private UIOptionsBehaviourDropdown m_LevelDataDropDown;

	[SerializeField]
	private Button m_ServerInviteButton;

	private LobbyData m_LobbyData;

	private Lobby m_Lobby;

	protected string m_LobbyStatus;

	private bool m_IsHost;

	private int m_MaxPlayerCount;

	private int m_TeamCount;

	private List<UINetworkLobbyTeam> m_Teams = new List<UINetworkLobbyTeam>();

	private List<ChatMessage> m_ChatMessages = new List<ChatMessage>();

	private Dictionary<TTNetworkID, Color32> m_ColLookup = new Dictionary<TTNetworkID, Color32>();

	private StringBuilder m_Builder = new StringBuilder();

	private bool m_RepeatingHost;

	private bool m_HiddenByGamePadDisconnect;

	private bool m_RepeatingClient;

	public bool RepeatingHost
	{
		get
		{
			return m_RepeatingHost;
		}
		set
		{
			m_RepeatingHost = value;
		}
	}

	public bool RepeatingClient
	{
		get
		{
			return m_RepeatingClient;
		}
		set
		{
			m_RepeatingClient = value;
		}
	}

	private Lobby.LobbyVisibility LobbyVisibility => m_LobbyData.m_Visibility;

	private MultiplayerModeType GameTypeChoice => m_LobbyData.GameTypeChoice;

	private int DurationChoice => m_LobbyData.DurationChoice;

	private int InventoryChoice => m_LobbyData.InventoryChoice;

	private int ScavengerChoice => m_LobbyData.ScavengerChoice;

	private int KillStreakRewardsChoice => m_LobbyData.KillStreakRewardsChoice;

	private int KillStreakResetWhenDestroyedChoice => m_LobbyData.KillStreakResetWhenDestroyedChoice;

	private int KillStreakRewardStackChoice => m_LobbyData.KillStreakRewardStackChoice;

	private int KillStreakMaxedRewardAutoClaimChoice => m_LobbyData.KillStreakMaxedRewardAutoClaimChoice;

	private int KillStreakPreventClaimNearDangerChoice => m_LobbyData.KillStreakPreventClaimNearDangerChoice;

	private int KillStreakClaimDangerRangeChoice => m_LobbyData.KillStreakClaimDangerRangeChoice;

	private int KillStreakKillThresholdMultiplierChoice => m_LobbyData.KillStreakKillThresholdMultiplierChoice;

	private int CabSelfDestructChoice => m_LobbyData.CabSelfDestructChoice;

	private int CabSelfDestructTimeRangeChoice => m_LobbyData.CabSelfDestructTimeRangeChoice;

	private int MapSizeChoice => m_LobbyData.MapSizeChoice;

	private int TeamMatchChoice => m_LobbyData.TeamMatchChoice;

	private int CollideToScavengeChoice => m_LobbyData.CollideToScavengeChoice;

	private int KeepLootedBlocksOnRespawnChoice => m_LobbyData.KeepLootedBlocksOnRespawnChoice;

	private int ClearUnusedInventoryAfterSpawnBubbleChoice => m_LobbyData.ClearUnusedInventoryAfterSpawnBubbleChoice;

	private int CrateDropsEnabledChoice => m_LobbyData.CrateDropsEnabledChoice;

	private int CrateDropMinDistanceChoice => m_LobbyData.CrateDropMinDistanceChoice;

	private int CrateDropFrequencyChoice => m_LobbyData.CrateDropFrequencyChoice;

	private int CrateDropBlockQuantityChoice => m_LobbyData.CrateDropBlockQuantityChoice;

	private int CrateDropPickupRangeChoice => m_LobbyData.CrateDropPickupRangeChoice;

	private int CrateDropDelayMinsChoice => m_LobbyData.CrateDropDelayMinsChoice;

	private int HealBuildChoice => m_LobbyData.HealBuildChoice;

	private int HealWarmUpChoice => m_LobbyData.HealWarmUpChoice;

	private int HealRateChoice => m_LobbyData.HealRateChoice;

	private int HealInterruptCooldownChoice => m_LobbyData.HealInterruptCooldownChoice;

	private int DeathStreakEnabledChoice => m_LobbyData.DeathStreakEnabledChoice;

	private int DeathStreakMinDeathsChoice => m_LobbyData.DeathStreakMinDeathsChoice;

	private int DeathStreakSubsDeathsChoice => m_LobbyData.DeathStreakSubsDeathsChoice;

	private int CoOpAllowPlayerTechModsChoice => m_LobbyData.CoOpAllowPlayerTechModsChoice;

	private int LevelDataChoice => m_LobbyData.LevelDataChoice;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		BlockScreenExit(exitBlocked: true);
		d.Assert(m_InternalOptionsPanel != null);
		m_InternalOptionsPanel.SetActive(value: false);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.BecameOwnerEvent.Subscribe(OnBecameOwner);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.ChatMessageEvent.Subscribe(OnChatMessage);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyKickedEvent.Subscribe(OnPlayerKicked);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobbyUpdatedEvent.Subscribe(OnLobbyUpdated);
		Singleton.Manager<ManNetworkLobby>.inst.GameStartedEvent.Subscribe(OnGameStarted);
		if (!m_HiddenByGamePadDisconnect)
		{
			SetHost(RepeatingHost);
		}
		else
		{
			SetHost(m_IsHost);
		}
		m_HiddenByGamePadDisconnect = false;
		if (TeamMatchChoice == 1)
		{
			Singleton.Manager<ManNetwork>.inst.Options = Singleton.Manager<ManNetworkLobby>.inst.TeamDeathMatchGameType.m_Options;
		}
		else
		{
			Singleton.Manager<ManNetwork>.inst.Options = Singleton.Manager<ManNetworkLobby>.inst.AvailableGameTypes[(int)GameTypeChoice].m_Options;
		}
		UpdateFieldsFromLobbyData();
		m_GameTypeDropDown.onValueChanged.AddListener(OnGameTypeChosen);
		m_DurationDropDown.onValueChanged.AddListener(OnDurationChosen);
		m_InventoryToggle.onValueChanged.AddListener(OnInventoryToggle);
		m_ScavengerToggle.onValueChanged.AddListener(OnScavengerToggle);
		m_KillStreakToggle.onValueChanged.AddListener(OnKillStreakRewardsToggle);
		m_VisibilityDropdown.onValueChanged.AddListener(OnVisibilityChanged);
		m_KillStreakResetWhenDestroyedToggle.onValueChanged.AddListener(OnKillStreakResetWhenDestroyedToggle);
		m_KillStreakRewardStackToggle.onValueChanged.AddListener(OnKillStreakRewardStackToggle);
		m_KillStreakMaxRewardAutoClaimToggle.onValueChanged.AddListener(OnKillStreakMaxedRewardAutoClaimToggle);
		m_KillStreakPreventClaimNearDangerToggle.onValueChanged.AddListener(OnKillStreakPreventClaimNearDangerToggle);
		m_KillStreakClaimDangerRangeDropDown.onValueChanged.AddListener(OnKillStreakClaimDangerRangeChosen);
		m_KillStreakKillThresholdMuptiplierDropDown.onValueChanged.AddListener(OnKillStreakKillThresholdMultiplierChosen);
		m_CabSelfDestructToggle.onValueChanged.AddListener(OnCabSelfDestructToggle);
		m_CabSelfDestructTimeRangeDropDown.onValueChanged.AddListener(OnCabSelfDestructTimeRangeChosen);
		m_MapSizeDropDown.onValueChanged.AddListener(OnMapSizeChosen);
		m_TeamToggle.onValueChanged.AddListener(OnTeamToggle);
		m_CollideToScavenge.onValueChanged.AddListener(OnCollideToScavenge);
		m_ReplenishAwardedBlocksToggle.onValueChanged.AddListener(OnReplenishAwardedBlocksToggle);
		m_ClearUnusedInventoryAfterSpawnBubbleToggle.onValueChanged.AddListener(OnClearUnusedInventoryAfterSpawnBubbleToggle);
		m_CrateDropsToggle.onValueChanged.AddListener(OnCrateDropsEnabledToggle);
		m_CrateDropMinDistanceDropDown.onValueChanged.AddListener(OnCrateDropMinDistanceChosen);
		m_CrateDropFrequencyDropDown.onValueChanged.AddListener(OnCrateDropFrequencyChosen);
		m_CrateDropBlockQuantityDropDown.onValueChanged.AddListener(OnCrateDropBlockQuantityChosen);
		m_CrateDropPickupRangeDropDown.onValueChanged.AddListener(OnCrateDropPickupRangeChosen);
		m_CrateDropDelayMinsDropDown.onValueChanged.AddListener(OnCrateDropDelayMinsChosen);
		m_HealToggle.onValueChanged.AddListener(OnHealToggle);
		m_HealWarmUpDropDown.onValueChanged.AddListener(OnHealWarmUpChosen);
		m_HealRateDropDown.onValueChanged.AddListener(OnHealRateChosen);
		m_HealInterruptDropDown.onValueChanged.AddListener(OnHealInterruptChosen);
		m_DeathStreakToggle.onValueChanged.AddListener(OnDeathStreakToggle);
		m_DeathStreakNumDeathsRewardDropDown.onValueChanged.AddListener(OnDeathStreakNumDeathsRewardChosen);
		m_DeathStreakSubsNumDeathsRewardDropDown.onValueChanged.AddListener(OnDeathStreakSubsNumDeathsChosen);
		m_CoOpAllowPlayerModsToggle.onValueChanged.AddListener(OnCoOpAllowPlayerModsToggle);
		m_LevelDataDropDown.onValueChanged.AddListener(OnLevelDataChosen);
		bool flag = Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
		m_ExitButton.gameObject.SetActive(!flag);
		ShowConnectingMessage(b: false);
		if (SKU.ConsoleUI)
		{
			m_ChatInput.gameObject.SetActive(value: false);
			m_ChatWindow.gameObject.SetActive(value: false);
		}
		RepeatingClient = false;
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(22, OnCancelPressed);
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(24, OnCancelPressed);
		if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.ScoreBoard))
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ScoreBoard);
			if (SKU.ConsoleUI)
			{
				Singleton.Manager<ManUI>.inst.ShowScreenPrompt(ManUI.ScreenType.MatchmakingLobbyScreen);
			}
		}
		if (!Debug.isDebugBuild)
		{
			m_GameTypeOption.gameObject.SetActive(value: false);
		}
	}

	public override void Hide()
	{
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null && !Singleton.Manager<ManGameMode>.inst.IsSwitchingMode && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Attract && Singleton.Manager<ManUI>.inst.IsStackEmpty())
		{
			d.Log("[UIScreenNetworkLobby] Switching to main menu, as the UI stack is empty");
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.MainMenu);
		}
		if (!m_HiddenByGamePadDisconnect && !RepeatingClient && Singleton.Manager<ManNetworkLobby>.inst.Inited && Singleton.Manager<ManNetwork>.inst.CurState == ManNetwork.State.Inactive && Singleton.Manager<ManNetwork>.inst.NetController == null)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LeaveLobby();
		}
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.BecameOwnerEvent.Unsubscribe(OnBecameOwner);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.ChatMessageEvent.Unsubscribe(OnChatMessage);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyKickedEvent.Unsubscribe(OnPlayerKicked);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobbyUpdatedEvent.Unsubscribe(OnLobbyUpdated);
		Singleton.Manager<ManNetworkLobby>.inst.GameStartedEvent.Unsubscribe(OnGameStarted);
		m_GameTypeDropDown.onValueChanged.RemoveListener(OnGameTypeChosen);
		m_DurationDropDown.onValueChanged.RemoveListener(OnDurationChosen);
		m_InventoryToggle.onValueChanged.RemoveListener(OnInventoryToggle);
		m_ScavengerToggle.onValueChanged.RemoveListener(OnScavengerToggle);
		m_KillStreakToggle.onValueChanged.RemoveListener(OnKillStreakRewardsToggle);
		m_VisibilityDropdown.onValueChanged.RemoveListener(OnVisibilityChanged);
		m_KillStreakResetWhenDestroyedToggle.onValueChanged.RemoveListener(OnKillStreakResetWhenDestroyedToggle);
		m_KillStreakRewardStackToggle.onValueChanged.RemoveListener(OnKillStreakRewardStackToggle);
		m_KillStreakMaxRewardAutoClaimToggle.onValueChanged.RemoveListener(OnKillStreakMaxedRewardAutoClaimToggle);
		m_KillStreakPreventClaimNearDangerToggle.onValueChanged.RemoveListener(OnKillStreakPreventClaimNearDangerToggle);
		m_KillStreakClaimDangerRangeDropDown.onValueChanged.RemoveListener(OnKillStreakClaimDangerRangeChosen);
		m_KillStreakKillThresholdMuptiplierDropDown.onValueChanged.RemoveListener(OnKillStreakKillThresholdMultiplierChosen);
		m_CabSelfDestructToggle.onValueChanged.RemoveListener(OnCabSelfDestructToggle);
		m_CabSelfDestructTimeRangeDropDown.onValueChanged.RemoveListener(OnCabSelfDestructTimeRangeChosen);
		m_MapSizeDropDown.onValueChanged.RemoveListener(OnMapSizeChosen);
		m_TeamToggle.onValueChanged.RemoveListener(OnTeamToggle);
		m_CollideToScavenge.onValueChanged.RemoveListener(OnCollideToScavenge);
		m_ReplenishAwardedBlocksToggle.onValueChanged.RemoveListener(OnReplenishAwardedBlocksToggle);
		m_ClearUnusedInventoryAfterSpawnBubbleToggle.onValueChanged.RemoveListener(OnClearUnusedInventoryAfterSpawnBubbleToggle);
		m_CrateDropsToggle.onValueChanged.RemoveListener(OnCrateDropsEnabledToggle);
		m_CrateDropMinDistanceDropDown.onValueChanged.RemoveListener(OnCrateDropMinDistanceChosen);
		m_CrateDropFrequencyDropDown.onValueChanged.RemoveListener(OnCrateDropFrequencyChosen);
		m_CrateDropBlockQuantityDropDown.onValueChanged.RemoveListener(OnCrateDropBlockQuantityChosen);
		m_CrateDropPickupRangeDropDown.onValueChanged.RemoveListener(OnCrateDropPickupRangeChosen);
		m_CrateDropDelayMinsDropDown.onValueChanged.RemoveListener(OnCrateDropDelayMinsChosen);
		m_HealToggle.onValueChanged.RemoveListener(OnHealToggle);
		m_HealWarmUpDropDown.onValueChanged.RemoveListener(OnHealWarmUpChosen);
		m_HealRateDropDown.onValueChanged.RemoveListener(OnHealRateChosen);
		m_HealInterruptDropDown.onValueChanged.RemoveListener(OnHealInterruptChosen);
		m_DeathStreakToggle.onValueChanged.RemoveListener(OnDeathStreakToggle);
		m_DeathStreakNumDeathsRewardDropDown.onValueChanged.RemoveListener(OnDeathStreakNumDeathsRewardChosen);
		m_DeathStreakSubsNumDeathsRewardDropDown.onValueChanged.RemoveListener(OnDeathStreakSubsNumDeathsChosen);
		m_CoOpAllowPlayerModsToggle.onValueChanged.RemoveListener(OnCoOpAllowPlayerModsToggle);
		m_LevelDataDropDown.onValueChanged.RemoveListener(OnLevelDataChosen);
		base.Hide();
		for (int num = m_PlayerEntryPanel.childCount - 1; num >= 0; num--)
		{
			Transform child = m_PlayerEntryPanel.GetChild(num);
			child.SetParent(null, worldPositionStays: false);
			child.GetComponent<UINetworkLobbyPlayerEntry>().OnColourChosen.Unsubscribe(OnColourChosen);
			child.Recycle();
		}
		m_ChatMessages.Clear();
		m_ServerStartButton.interactable = true;
		m_ServerInviteButton.interactable = true;
		RepeatingHost = false;
		RepeatingClient = false;
		if (Singleton.Manager<ManNetwork>.inst.IsServer && Singleton.Manager<ManNetwork>.inst.NetController != null)
		{
			Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhaseTimerPaused = false;
		}
		if (Singleton.Manager<ManNetwork>.inst.IsServer && Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.Outro && Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.ScoreBoard))
		{
			(Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.ScoreBoard) as UIScoreBoardHUD).SelectStartButton();
		}
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(22, OnCancelPressed);
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(24, OnCancelPressed);
	}

	public void ShowConnectingMessage(bool b)
	{
		if (m_ConnectingMessageContainer != null)
		{
			m_ConnectingMessageContainer.SetActive(b);
		}
	}

	public void SetCurrentLobby(Lobby lobby)
	{
		m_Lobby = lobby;
		m_LobbyData = lobby.Data;
		SetupLobby();
	}

	private void SetHost(bool isHost)
	{
		m_IsHost = isHost;
		bool flag = Singleton.Manager<ManNetwork>.inst.NetController == null;
		m_ServerStartButton.gameObject.SetActive(m_IsHost && flag);
		m_ServerInviteButton.gameObject.SetActive(SKU.ConsoleUI && m_IsHost);
		m_ServerStartButton.interactable = m_IsHost && Singleton.Manager<ManNetwork>.inst.Options != null;
		m_GameTypeDropDown.interactable = m_IsHost;
		m_DurationDropDown.interactable = m_IsHost;
		m_VisibilityDropdown.interactable = m_IsHost;
		m_InventoryToggle.interactable = m_IsHost;
		m_ScavengerToggle.interactable = m_IsHost;
		m_KillStreakToggle.interactable = m_IsHost;
		m_KillStreakResetWhenDestroyedToggle.interactable = m_IsHost;
		m_KillStreakRewardStackToggle.interactable = m_IsHost;
		m_KillStreakMaxRewardAutoClaimToggle.interactable = m_IsHost;
		m_KillStreakPreventClaimNearDangerToggle.interactable = m_IsHost;
		m_KillStreakClaimDangerRangeDropDown.interactable = m_IsHost;
		m_KillStreakKillThresholdMuptiplierDropDown.interactable = m_IsHost;
		m_CabSelfDestructToggle.interactable = m_IsHost;
		m_CabSelfDestructTimeRangeDropDown.interactable = m_IsHost;
		m_MapSizeDropDown.interactable = m_IsHost;
		m_TeamToggle.interactable = m_IsHost;
		m_CollideToScavenge.interactable = m_IsHost;
		m_ReplenishAwardedBlocksToggle.interactable = m_IsHost;
		m_ClearUnusedInventoryAfterSpawnBubbleToggle.interactable = m_IsHost;
		m_CrateDropsToggle.interactable = m_IsHost;
		m_CrateDropMinDistanceDropDown.interactable = m_IsHost;
		m_CrateDropFrequencyDropDown.interactable = m_IsHost;
		m_CrateDropBlockQuantityDropDown.interactable = m_IsHost;
		m_CrateDropPickupRangeDropDown.interactable = m_IsHost;
		m_CrateDropDelayMinsDropDown.interactable = m_IsHost;
		m_HealToggle.interactable = m_IsHost;
		m_HealWarmUpDropDown.interactable = m_IsHost;
		m_HealRateDropDown.interactable = m_IsHost;
		m_HealInterruptDropDown.interactable = m_IsHost;
		m_DeathStreakToggle.interactable = m_IsHost;
		m_DeathStreakNumDeathsRewardDropDown.interactable = m_IsHost;
		m_DeathStreakSubsNumDeathsRewardDropDown.interactable = m_IsHost;
		m_CoOpAllowPlayerModsToggle.interactable = m_IsHost;
		m_LevelDataDropDown.interactable = m_IsHost;
		UpdatePlayersList();
		if (isHost && !flag && SKU.ConsoleUI && m_ServerInviteButton != null)
		{
			SelectButton(m_ServerInviteButton.gameObject);
		}
	}

	private void SetTeamGame(bool team, int teamCount)
	{
		bool flag = m_TeamCount > 0;
		if (flag == team && (!flag || m_TeamCount == teamCount))
		{
			return;
		}
		bool flag2 = team && teamCount > 1;
		m_TeamEntryViewport.gameObject.SetActive(flag2);
		m_PlayerEntryViewport.gameObject.SetActive(!flag2);
		if (flag2)
		{
			while (m_PlayerEntryPanel.childCount > 0)
			{
				Transform child = m_PlayerEntryPanel.GetChild(m_PlayerEntryPanel.childCount - 1);
				child.SetParent(null, worldPositionStays: false);
				child.GetComponent<UINetworkLobbyPlayerEntry>().OnColourChosen.Unsubscribe(OnColourChosen);
				child.Recycle();
			}
			CreateTeams(teamCount);
		}
		else if (flag && m_TeamCount > 1)
		{
			for (int num = m_TeamCount - 1; num >= 0; num--)
			{
				ClearTeam(m_Teams[num]);
			}
			m_Teams.Clear();
		}
		m_TeamCount = (team ? teamCount : 0);
		if (m_IsHost)
		{
			m_Lobby.ReassignPlayerTeams(m_TeamCount);
		}
	}

	private void CreateTeams(int teamCount)
	{
		for (int i = 0; i < teamCount; i++)
		{
			UINetworkLobbyTeam uINetworkLobbyTeam = null;
			if (i < m_Teams.Count)
			{
				uINetworkLobbyTeam = m_Teams[i];
			}
			else
			{
				uINetworkLobbyTeam = m_TeamEntryPrefab.Spawn();
				bool worldPositionStays = false;
				uINetworkLobbyTeam.transform.SetParent(m_TeamEntryPanel, worldPositionStays);
				m_Teams.Add(uINetworkLobbyTeam);
			}
			string text = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 115), i + 1);
			uINetworkLobbyTeam.SetName(text);
		}
		for (int num = m_Teams.Count - 1; num >= teamCount; num--)
		{
			ClearTeam(m_Teams[num]);
			m_Teams.RemoveAt(num);
		}
	}

	private void ClearTeam(UINetworkLobbyTeam team)
	{
		team.ClearUseCount();
		team.RecycleUnused();
		team.transform.SetParent(null, worldPositionStays: false);
		team.Recycle();
	}

	private Transform SpawnPlayerEntry(Transform parent)
	{
		Transform obj = m_PlayerEntryPrefab.transform.Spawn();
		bool worldPositionStays = false;
		obj.SetParent(parent, worldPositionStays);
		return obj;
	}

	private void UpdatePlayersList()
	{
		int num = 0;
		bool flag = m_TeamCount > 1;
		List<Color32> availableColoursList = m_Lobby.GetAvailableColoursList();
		List<LobbyPlayerData> playerList = m_Lobby.GetPlayerList();
		num = playerList.Count;
		if (flag)
		{
			for (int i = 0; i < m_TeamCount; i++)
			{
				m_Teams[i].ClearUseCount();
			}
		}
		for (int j = 0; j < num; j++)
		{
			UINetworkLobbyPlayerEntry uINetworkLobbyPlayerEntry = null;
			if (flag)
			{
				int teamID = playerList[j].m_TeamID;
				if (teamID == -1)
				{
					continue;
				}
				uINetworkLobbyPlayerEntry = m_Teams[teamID].GetUnsuedEntry();
				if (uINetworkLobbyPlayerEntry == null)
				{
					uINetworkLobbyPlayerEntry = SpawnPlayerEntry(m_Teams[teamID].transform).GetComponent<UINetworkLobbyPlayerEntry>();
					m_Teams[teamID].AddPlayer(uINetworkLobbyPlayerEntry);
				}
			}
			else if (j < m_PlayerEntryPanel.childCount)
			{
				uINetworkLobbyPlayerEntry = m_PlayerEntryPanel.GetChild(j).GetComponent<UINetworkLobbyPlayerEntry>();
			}
			else
			{
				uINetworkLobbyPlayerEntry = SpawnPlayerEntry(m_PlayerEntryPanel).GetComponent<UINetworkLobbyPlayerEntry>();
				uINetworkLobbyPlayerEntry.OnColourChosen.Subscribe(OnColourChosen);
			}
			bool flag2 = playerList[j].m_PlayerID == Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LocalPlayerNetworkID;
			bool showAdminButtons = !flag2 && m_IsHost;
			bool allowColourChoice = flag2 && m_TeamCount == 0;
			bool canShowProfileCard = !flag2 && SKU.XboxOneUI;
			uINetworkLobbyPlayerEntry.SetPlayerData(playerList[j], canShowProfileCard, showAdminButtons, allowColourChoice, availableColoursList);
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			if ((currentSelectedGameObject == null || !currentSelectedGameObject.activeInHierarchy) && Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled() && flag2)
			{
				SelectButton(uINetworkLobbyPlayerEntry.gameObject);
			}
		}
		if (flag)
		{
			for (int k = 0; k < m_TeamCount; k++)
			{
				m_Teams[k].RecycleUnused();
			}
			return;
		}
		while (m_PlayerEntryPanel.childCount > num)
		{
			Transform child = m_PlayerEntryPanel.GetChild(m_PlayerEntryPanel.childCount - 1);
			child.SetParent(null, worldPositionStays: false);
			child.GetComponent<UINetworkLobbyPlayerEntry>().OnColourChosen.Unsubscribe(OnColourChosen);
			child.Recycle();
		}
	}

	private void AddChatMessage(LobbyPlayerData playerData, string formatMsg, string userMsg)
	{
		m_ChatMessages.Add(new ChatMessage
		{
			m_Format = formatMsg,
			m_UserMsg = userMsg,
			m_UserId = playerData.m_PlayerID,
			m_UserName = playerData.m_Name
		});
		UpdateChat();
	}

	private void UpdateChat()
	{
		m_ColLookup.Clear();
		List<LobbyPlayerData> playerList = m_Lobby.GetPlayerList();
		if (playerList != null)
		{
			for (int i = 0; i < playerList.Count; i++)
			{
				LobbyPlayerData lobbyPlayerData = playerList[i];
				if (m_ColLookup.ContainsKey(lobbyPlayerData.m_PlayerID))
				{
					d.LogWarning("Duplicate PlayerID for for player: " + lobbyPlayerData.m_PlayerID.ToString() + " index=" + i);
					for (int j = 0; j < playerList.Count; j++)
					{
						d.LogWarning("Lobby Member Index=" + j + " PlayerID=" + playerList[j].m_PlayerID.ToString() + " Name=" + playerList[j].m_Name);
					}
					m_ColLookup[lobbyPlayerData.m_PlayerID] = lobbyPlayerData.m_Colour;
				}
				else
				{
					m_ColLookup.Add(lobbyPlayerData.m_PlayerID, lobbyPlayerData.m_Colour);
				}
			}
		}
		if (m_ChatMessages.Count > 0)
		{
			bool flag = true;
			for (int k = 0; k < m_ChatMessages.Count; k++)
			{
				ChatMessage chatMessage = m_ChatMessages[k];
				if (!m_ColLookup.TryGetValue(chatMessage.m_UserId, out var value))
				{
					value = Color.black;
				}
				if (!flag)
				{
					m_Builder.Append("\n");
				}
				flag = false;
				string arg = ColourConverter.ColourToString(value);
				string arg2 = $"<color=#{arg}>{chatMessage.m_UserName}</color>";
				m_Builder.AppendFormat(chatMessage.m_Format, arg2, chatMessage.m_UserMsg);
			}
			m_ChatWindow.text = m_Builder.ToString();
		}
		else
		{
			m_ChatWindow.text = "";
		}
		m_ScrollRect.verticalNormalizedPosition = 0f;
		m_ColLookup.Clear();
		m_Builder.Remove(0, m_Builder.Length);
	}

	private void SendChatMessage(string message)
	{
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.SendChat(message, -1, NetworkInstanceId.Invalid.Value);
		}
		m_ChatInput.text = "";
		m_ChatInput.ActivateInputField();
		m_ChatInput.Select();
	}

	private void Exit()
	{
		Singleton.Manager<ManUI>.inst.GoBack();
		if (Singleton.Manager<ManNetwork>.inst.IsInPhase(NetController.Phase.Outro))
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ScoreBoard);
		}
	}

	public void OnEndEdit(string value)
	{
		if ((Input.GetKey(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && !value.NullOrEmpty())
		{
			SendChatMessage(value);
		}
	}

	public void OnChatSend()
	{
		if (!m_ChatInput.text.NullOrEmpty())
		{
			SendChatMessage(m_ChatInput.text);
		}
	}

	public void OnStartServerClicked()
	{
		m_ServerStartButton.interactable = false;
		m_ServerInviteButton.interactable = false;
		if (RepeatingHost && Singleton.Manager<ManNetwork>.inst.NetController != null)
		{
			Singleton.Manager<ManNetwork>.inst.NetController.ServerChangePhase(NetController.Phase.Restarting);
			RepeatingHost = false;
		}
		else
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.TriggerGameStart();
		}
	}

	public void OnSendInvitesClicked()
	{
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.SendInvites();
	}

	private void OnGameStarted(bool isJIP)
	{
		if (!Singleton.Manager<ManGameMode>.inst.IsCurrentModeMultiplayer())
		{
			Singleton.Manager<ManNetwork>.inst.SetupWaitsForModeSwitch = true;
			ManGameMode.GameType modeToSet = ManGameMode.GameType.Deathmatch;
			switch (GameTypeChoice)
			{
			case MultiplayerModeType.Deathmatch:
				modeToSet = ManGameMode.GameType.Deathmatch;
				break;
			case MultiplayerModeType.CoOpCampaign:
				modeToSet = ManGameMode.GameType.CoOpCampaign;
				break;
			case MultiplayerModeType.CoOpCreative:
				modeToSet = ManGameMode.GameType.CoOpCreative;
				break;
			default:
				d.LogError("OnGameStarted with an unsupported gametype");
				break;
			}
			Singleton.Manager<ManGameMode>.inst.SetupModeSwitchAction(Singleton.Manager<ManGameMode>.inst.NextModeSetting, modeToSet);
			Singleton.Manager<ManGameMode>.inst.NextModeSetting.SwitchToMode();
		}
		else if (!isJIP)
		{
			Singleton.Manager<ManUI>.inst.FadeToBlack();
			Singleton.Manager<ManNetwork>.inst.OnGenerateTerrainForced.Send(paramA: true);
		}
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		if (SKU.ConsoleUI)
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 109);
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			uIScreenNotifications.Set(localisedString, null, null);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
		}
	}

	private void SetupLobby()
	{
		d.Assert(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null, "Lobby should not be null at this point!");
		Lobby currentLobby = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby;
		if (currentLobby != null)
		{
			m_LobbyStatus = $"Joined lobby {currentLobby.ID}";
			SetHost(isHost: false);
			OnLobbyUpdated(currentLobby);
		}
	}

	private void OnBecameOwner(Lobby lobby)
	{
		d.Log("[UIScreenNetworkLobby] OnBecameOwner: " + lobby.Name);
		m_LobbyStatus = $"Owner of lobby {lobby.ID}";
		SetHost(isHost: true);
		if (UILobbyVisibilityDropdown.VisibilityToDropdownIndex(Lobby.LobbyVisibility.Public, out var index))
		{
			m_VisibilityDropdown.SetValue(index);
			m_VisibilityDropdown.RefreshShownValue();
		}
	}

	private void OnChatMessage(LobbyPlayerData playerData, uint netId, int teamChannel, string message)
	{
		if (teamChannel == -1)
		{
			AddChatMessage(playerData, "{0}: {1}", message);
		}
	}

	private void OnPlayerKicked(TTNetworkID playerID, string playerName, bool isLocalUser)
	{
		if (isLocalUser)
		{
			Singleton.Manager<ManUI>.inst.GoBack();
			Action accept = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			};
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 119);
			(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(localisedString2, accept, localisedString);
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
		}
		else
		{
			string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamLobby, 4);
			string userMsg = "";
			m_ChatMessages.Add(new ChatMessage
			{
				m_Format = localisedString3,
				m_UserMsg = userMsg,
				m_UserId = playerID,
				m_UserName = playerName
			});
			UpdateChat();
		}
	}

	private void UpdateFieldsFromLobbyData()
	{
		d.Assert(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null, "[UIScreenNetworkLobby] Current lobby is null, but we are trying to show the lobby GUI");
		_ = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.Data;
		UILobbyVisibilityDropdown.VisibilityToDropdownIndex(LobbyVisibility, out var index);
		m_VisibilityDropdown.SetValue((index >= 0) ? index : 0);
		m_VisibilityDropdown.RefreshShownValue();
		int gameTypeChoice = (int)GameTypeChoice;
		m_GameTypeDropDown.value = gameTypeChoice;
		m_GameTypeDropDown.RefreshShownValue();
		int durationChoice = DurationChoice;
		m_DurationDropDown.value = durationChoice;
		m_DurationDropDown.RefreshShownValue();
		int inventoryChoice = InventoryChoice;
		m_InventoryToggle.isOn = inventoryChoice == 1;
		int scavengerChoice = ScavengerChoice;
		m_ScavengerToggle.isOn = scavengerChoice == 1;
		m_KillStreakToggle.isOn = KillStreakRewardsChoice == 1;
		m_KillStreakResetWhenDestroyedToggle.isOn = KillStreakResetWhenDestroyedChoice == 1;
		m_KillStreakRewardStackToggle.isOn = KillStreakRewardStackChoice == 1;
		m_KillStreakMaxRewardAutoClaimToggle.isOn = KillStreakMaxedRewardAutoClaimChoice == 1;
		m_KillStreakPreventClaimNearDangerToggle.isOn = KillStreakPreventClaimNearDangerChoice == 1;
		m_KillStreakClaimDangerRangeDropDown.value = KillStreakClaimDangerRangeChoice;
		m_KillStreakClaimDangerRangeDropDown.RefreshShownValue();
		m_KillStreakKillThresholdMuptiplierDropDown.value = KillStreakKillThresholdMultiplierChoice;
		m_KillStreakKillThresholdMuptiplierDropDown.RefreshShownValue();
		m_ReplenishAwardedBlocksToggle.isOn = KeepLootedBlocksOnRespawnChoice == 1;
		m_ClearUnusedInventoryAfterSpawnBubbleToggle.isOn = ClearUnusedInventoryAfterSpawnBubbleChoice == 1;
		m_CrateDropsToggle.isOn = CrateDropsEnabledChoice == 1;
		m_CrateDropMinDistanceDropDown.value = CrateDropMinDistanceChoice;
		m_CrateDropFrequencyDropDown.value = CrateDropFrequencyChoice;
		m_CrateDropBlockQuantityDropDown.value = CrateDropBlockQuantityChoice;
		m_CrateDropPickupRangeDropDown.value = CrateDropPickupRangeChoice;
		m_CrateDropDelayMinsDropDown.value = CrateDropDelayMinsChoice;
		NetOptions options = Singleton.Manager<ManNetwork>.inst.Options;
		bool flag = options.m_GameModeType == MultiplayerModeType.CoOpCampaign || options.m_GameModeType == MultiplayerModeType.CoOpCreative;
		if (flag)
		{
			m_CabSelfDestructToggle.isOn = false;
			m_CabSelfDestructToggle.interactable = false;
		}
		else
		{
			m_CabSelfDestructToggle.isOn = CabSelfDestructChoice == 1;
			if (m_IsHost)
			{
				m_CabSelfDestructToggle.interactable = true;
			}
		}
		m_CabSelfDestructTimeRangeDropDown.value = CabSelfDestructTimeRangeChoice;
		m_CabSelfDestructTimeRangeDropDown.RefreshShownValue();
		m_HealToggle.isOn = HealBuildChoice == 1;
		int healWarmUpChoice = HealWarmUpChoice;
		m_HealWarmUpDropDown.value = healWarmUpChoice;
		m_HealWarmUpDropDown.RefreshShownValue();
		int healRateChoice = HealRateChoice;
		m_HealRateDropDown.value = healRateChoice;
		m_HealRateDropDown.RefreshShownValue();
		int healInterruptCooldownChoice = HealInterruptCooldownChoice;
		m_HealInterruptDropDown.value = healInterruptCooldownChoice;
		m_HealInterruptDropDown.RefreshShownValue();
		m_DeathStreakToggle.isOn = DeathStreakEnabledChoice == 1;
		m_DeathStreakNumDeathsRewardDropDown.value = DeathStreakMinDeathsChoice;
		m_DeathStreakNumDeathsRewardDropDown.RefreshShownValue();
		m_DeathStreakSubsNumDeathsRewardDropDown.value = DeathStreakSubsDeathsChoice;
		m_DeathStreakSubsNumDeathsRewardDropDown.RefreshShownValue();
		m_CoOpAllowPlayerModsToggle.isOn = CoOpAllowPlayerTechModsChoice == 1;
		int mapSizeChoice = MapSizeChoice;
		m_MapSizeDropDown.value = mapSizeChoice;
		m_MapSizeDropDown.RefreshShownValue();
		if (options.m_GameModeType != MultiplayerModeType.Deathmatch)
		{
			m_TeamToggle.interactable = false;
			m_TeamToggle.isOn = false;
		}
		else
		{
			if (m_IsHost)
			{
				m_TeamToggle.interactable = true;
			}
			int teamMatchChoice = TeamMatchChoice;
			m_TeamToggle.isOn = teamMatchChoice == 1;
		}
		if (m_IsHost)
		{
			m_DurationDropDown.interactable = !flag;
			m_CrateDropsToggle.interactable = !flag;
			m_CrateDropMinDistanceDropDown.interactable = !flag;
			m_CrateDropFrequencyDropDown.interactable = !flag;
			m_CrateDropBlockQuantityDropDown.interactable = !flag;
			m_CrateDropPickupRangeDropDown.interactable = !flag;
			m_CrateDropDelayMinsDropDown.interactable = !flag;
			m_CoOpAllowPlayerModsToggle.interactable = flag;
		}
		int collideToScavengeChoice = CollideToScavengeChoice;
		m_CollideToScavenge.isOn = collideToScavengeChoice == 1;
		int value = LevelDataChoice;
		NetOptions options2 = Singleton.Manager<ManNetworkLobby>.inst.AvailableGameTypes[gameTypeChoice].m_Options;
		int count = options2.m_LevelData.Count;
		if (count != m_LevelDataDropDown.options.Count)
		{
			m_LevelDataDropDown.ClearOptions();
			List<string> list = new List<string>(count);
			for (int i = 0; i < count; i++)
			{
				list.Add(options2.m_LevelData[i].m_levelName.Value);
			}
			m_LevelDataDropDown.AddOptions(list);
			value = Mathf.Clamp(value, 0, count);
		}
		m_LevelDataDropDown.value = value;
		m_LevelDataDropDown.RefreshShownValue();
		SetTeamGame(options.m_NumTeams > 0, options.m_NumTeams);
		UpdatePlayersList();
		UpdateChat();
	}

	private void OnLobbyUpdated(Lobby lobby)
	{
		d.Assert(lobby != null && lobby.Name != null, "ASSERT: lobby is NULL!");
		m_Lobby = lobby;
		m_LobbyData = lobby.Data;
		if (m_LobbyData.m_Choices.Length != 34)
		{
			d.LogError("UIScreenNetworkLobby: OnLobbyUpdated has an invalid number of lobby option choices.  Expected:" + 34 + " Had:" + m_LobbyData.m_Choices.Length);
		}
		else
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.SendEventLobbyDataToGame(lobby);
			UpdateFieldsFromLobbyData();
			if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null && !m_IsHost && Singleton.Manager<ManNetwork>.inst.CurState == ManNetwork.State.Inactive)
			{
				Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.CheckForStartGame();
			}
		}
	}

	private void OnColourChosen(LobbyPlayerData playerData, Color32 choice)
	{
		if (!playerData.m_Colour.Equals(choice))
		{
			bool flag = playerData.m_PlayerID == Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LocalPlayerNetworkID;
			d.AssertFormat(flag, "UIScreenNetworkLobby.OnColourChosen - trying to choose colour for non-local player with id {0}, name {1}", playerData.m_PlayerID, playerData.m_Name);
			if (flag && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
			{
				Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.RequestSetColour(choice);
			}
		}
	}

	private void _updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex lci, int choice)
	{
		m_LobbyData.m_Choices[(int)lci] = choice;
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.SetLobbyChoices(m_LobbyData.m_Choices);
		}
	}

	private void OnGameTypeChosen(int newInd)
	{
		if (m_IsHost && newInd != (int)GameTypeChoice)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_GAME_TYPE, newInd);
			if (Singleton.Manager<ManNetworkLobby>.inst.AvailableGameTypes[(int)GameTypeChoice].m_Options.m_GameModeType == MultiplayerModeType.Deathmatch)
			{
				m_CabSelfDestructToggle.isOn = true;
			}
		}
	}

	private void OnDurationChosen(int newInd)
	{
		if (m_IsHost && newInd != DurationChoice)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_DURATION_TYPE, newInd);
		}
	}

	private void OnInventoryToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_INVENTORY_ENABLED, isOn ? 1 : 0);
		}
	}

	private void OnScavengerToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_SCAVENGER_ENABLED, isOn ? 1 : 0);
		}
	}

	private void OnKillStreakKillThresholdMultiplierChosen(int newInd)
	{
		if (m_IsHost && newInd != KillStreakKillThresholdMultiplierChoice)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_KILL_STREAK_THRESHOLD_MULT_TYPE, newInd);
		}
	}

	private void OnKillStreakClaimDangerRangeChosen(int newInd)
	{
		if (m_IsHost && newInd != KillStreakClaimDangerRangeChoice)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_KILL_STREAK_DANGER_RANGE_TYPE, newInd);
		}
	}

	private void OnKillStreakRewardsToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_KILL_STREAK_REWARDS_ENABLED, isOn ? 1 : 0);
		}
	}

	private void OnVisibilityChanged(int visibility)
	{
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			Lobby.LobbyVisibility visibility2 = UILobbyVisibilityDropdown.DropdownIndexToVisibility(visibility);
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.SetVisibility(visibility2);
		}
	}

	private void OnKillStreakResetWhenDestroyedToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_KILL_STREAK_RESET_WHEN_DESTROYED_ENABLED, isOn ? 1 : 0);
		}
	}

	private void OnKillStreakRewardStackToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_KILL_STREAK_REWARD_STACK_ENABLED, isOn ? 1 : 0);
		}
	}

	private void OnKillStreakMaxedRewardAutoClaimToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_KILL_STREAK_MAXED_REWARD_AUTO_CLAIM_ENABLED, isOn ? 1 : 0);
		}
	}

	private void OnKillStreakPreventClaimNearDangerToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_KILL_STREAK_PREVENT_CLAIM_NEAR_DANGER_ENABLED, isOn ? 1 : 0);
		}
	}

	private void OnCrateDropsEnabledToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CRATE_DROPS_ENABLED, isOn ? 1 : 0);
		}
	}

	private void OnCrateDropMinDistanceChosen(int newInd)
	{
		if (m_IsHost && newInd != CrateDropMinDistanceChoice)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CRATE_DROP_MIN_DISTANCE_TYPE, newInd);
		}
	}

	private void OnCrateDropFrequencyChosen(int newInd)
	{
		if (m_IsHost && newInd != CrateDropFrequencyChoice)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CRATE_DROP_FREQUENCY_TYPE, newInd);
		}
	}

	private void OnCrateDropBlockQuantityChosen(int newInd)
	{
		if (m_IsHost && newInd != CrateDropBlockQuantityChoice)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CRATE_DROP_BLOCK_QUANTITY_TYPE, newInd);
		}
	}

	private void OnCrateDropPickupRangeChosen(int newInd)
	{
		if (m_IsHost && newInd != CrateDropPickupRangeChoice)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CRATE_DROP_PICKUP_RANGE_TYPE, newInd);
		}
	}

	private void OnCrateDropDelayMinsChosen(int newInd)
	{
		if (m_IsHost && newInd != CrateDropPickupRangeChoice)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CRATE_DROP_DELAY_MINS_TYPE, newInd);
		}
	}

	private void OnCabSelfDestructToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CAB_SELF_DESTRUCT_ENABLED, isOn ? 1 : 0);
		}
	}

	private void OnCabSelfDestructTimeRangeChosen(int newInd)
	{
		if (m_IsHost && newInd != CabSelfDestructTimeRangeChoice)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CAB_SELF_DESTRUCT_TIME, newInd);
		}
	}

	private void OnMapSizeChosen(int newInd)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_MAP_SIZE, newInd);
		}
	}

	private void OnTeamToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_TEAM_MATCH, isOn ? 1 : 0);
		}
	}

	private void OnCollideToScavenge(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_COLLIDE_TO_SCAVENGE, isOn ? 1 : 0);
		}
	}

	private void OnReplenishAwardedBlocksToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_KEEP_LOOTED_BLOCKS_ON_RESPAWN, isOn ? 1 : 0);
		}
	}

	private void OnClearUnusedInventoryAfterSpawnBubbleToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CLEAR_UNUSED_INVENTORY_AFTER_SPAWN_BUBBLE_ENABLED, isOn ? 1 : 0);
		}
	}

	private void OnHealToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_HEAL_BUILD_ENABLED, isOn ? 1 : 0);
		}
	}

	private void OnHealWarmUpChosen(int newInd)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_HEAL_WARM_UP_TIME, newInd);
		}
	}

	private void OnHealRateChosen(int newInd)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_HEAL_RATE, newInd);
		}
	}

	private void OnHealInterruptChosen(int newInd)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_INTERRUPT_COOLDOWN, newInd);
		}
	}

	private void OnDeathStreakToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_DEATH_STREAK_REWARDS_ENABLED, isOn ? 1 : 0);
		}
	}

	private void OnDeathStreakNumDeathsRewardChosen(int newInd)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_DEATH_STREAK_REWARDS_MIN_DEATHS_REQD, newInd);
		}
	}

	private void OnDeathStreakSubsNumDeathsChosen(int newInd)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_DEATH_STREAK_REWARDS_SUBS_DEATHS_REQD, newInd);
		}
	}

	private void OnCoOpAllowPlayerModsToggle(bool isOn)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CO_OP_ALLOW_PLAYER_TECH_MODS, isOn ? 1 : 0);
		}
	}

	private void OnLevelDataChosen(int newInd)
	{
		if (m_IsHost)
		{
			_updateLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_LEVEL_DATA, newInd);
		}
	}

	public void OnExitClicked()
	{
		Exit();
	}

	private static void _setupIntegerFieldDropDown(Dropdown pDD, int[] pValues)
	{
		List<string> list = new List<string>(pValues.Length);
		for (int i = 0; i < pValues.Length; i++)
		{
			list.Add(pValues[i].ToString());
		}
		pDD.AddOptions(list);
	}

	private void _setupCrateDropFields()
	{
		_setupIntegerFieldDropDown(m_CrateDropMinDistanceDropDown, ManNetwork.CrateDropMinDistances);
		List<string> list = new List<string>(ManNetwork.CrateDropFrequencies.Length);
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 73);
		for (int i = 0; i < ManNetwork.CrateDropFrequencies.Length; i++)
		{
			list.Add(string.Format(localisedString, ManNetwork.CrateDropFrequencies[i]));
		}
		m_CrateDropFrequencyDropDown.AddOptions(list);
		List<string> list2 = new List<string>(ManNetwork.CrateDropBlockCounts.Length);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 74);
		for (int j = 0; j < ManNetwork.CrateDropBlockCounts.Length; j++)
		{
			list2.Add(string.Format(localisedString2, ManNetwork.CrateDropBlockCounts[j]));
		}
		m_CrateDropBlockQuantityDropDown.AddOptions(list2);
		_setupIntegerFieldDropDown(m_CrateDropPickupRangeDropDown, ManNetwork.CrateDropPickupRanges);
		_setupIntegerFieldDropDown(m_CrateDropDelayMinsDropDown, ManNetwork.CrateDropDelayMinsChoices);
	}

	private void Awake()
	{
		List<string> list = new List<string>(Singleton.Manager<ManNetworkLobby>.inst.AvailableGameTypes.Length);
		for (int i = 0; i < Singleton.Manager<ManNetworkLobby>.inst.AvailableGameTypes.Length; i++)
		{
			NetOptions options = Singleton.Manager<ManNetworkLobby>.inst.AvailableGameTypes[i].m_Options;
			bool flag = options.m_GameModeType == MultiplayerModeType.CoOpCreative;
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NetworkedGameTypes, 5);
			string item = (flag ? localisedString : options.m_LocName.Value);
			if (options.m_GameModeType != MultiplayerModeType.KingAnton && !flag)
			{
				list.Add(item);
			}
		}
		m_GameTypeDropDown.AddOptions(list);
		List<string> list2 = new List<string>(ManNetwork.MatchDurationOptions.Length);
		for (int j = 0; j < ManNetwork.MatchDurationOptions.Length; j++)
		{
			ManNetwork.MatchDurationOption matchDurationOption = ManNetwork.MatchDurationOptions[j];
			list2.Add(matchDurationOption.localisedString());
		}
		m_DurationDropDown.AddOptions(list2);
		UILobbyVisibilityDropdown.AddDropdownOptions(m_VisibilityDropdown.Target);
		List<string> list3 = new List<string>(ManNetwork.KillStreakClaimDangerRanges.Length);
		for (int k = 0; k < ManNetwork.KillStreakClaimDangerRanges.Length; k++)
		{
			list3.Add(ManNetwork.KillStreakClaimDangerRanges[k].ToString());
		}
		m_KillStreakClaimDangerRangeDropDown.AddOptions(list3);
		List<string> list4 = new List<string>(ManNetwork.KillStreakKillThresholdMultiplierOptions.Length);
		for (int l = 0; l < ManNetwork.KillStreakKillThresholdMultiplierOptions.Length; l++)
		{
			list4.Add(ManNetwork.KillStreakKillThresholdMultiplierOptions[l].ToString());
		}
		m_KillStreakKillThresholdMuptiplierDropDown.AddOptions(list4);
		List<string> list5 = new List<string>(ManNetwork.CabSelfDestructTimeRanges.Length);
		for (int m = 0; m < ManNetwork.CabSelfDestructTimeRanges.Length; m++)
		{
			list5.Add(ManNetwork.CabSelfDestructTimeRanges[m].ToString());
		}
		m_CabSelfDestructTimeRangeDropDown.AddOptions(list5);
		List<string> options2 = new List<string>(4)
		{
			Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 76),
			Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 79),
			Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 78),
			Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 77)
		};
		m_MapSizeDropDown.AddOptions(options2);
		_setupCrateDropFields();
		_setupIntegerFieldDropDown(m_HealWarmUpDropDown, ManNetwork.HealWarmUpTimeRanges);
		_setupIntegerFieldDropDown(m_HealRateDropDown, ManNetwork.HealRateRanges);
		_setupIntegerFieldDropDown(m_HealInterruptDropDown, ManNetwork.HealInterruptCooldownRanges);
		_setupIntegerFieldDropDown(m_DeathStreakNumDeathsRewardDropDown, ManNetwork.DeathStreakMinDeathsReqdRanges);
		_setupIntegerFieldDropDown(m_DeathStreakSubsNumDeathsRewardDropDown, ManNetwork.DeathStreakSubsDeathsReqdRanges);
		int gameTypeChoice = (int)GameTypeChoice;
		NetOptions options3 = Singleton.Manager<ManNetworkLobby>.inst.AvailableGameTypes[gameTypeChoice].m_Options;
		int count = options3.m_LevelData.Count;
		List<string> list6 = new List<string>(count);
		for (int n = 0; n < count; n++)
		{
			list6.Add(options3.m_LevelData[n].m_levelName.Value);
		}
		m_LevelDataDropDown.AddOptions(list6);
	}

	private void Update()
	{
		if (m_ConnectingMessageContainer.activeSelf)
		{
			bool num = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null;
			bool flag = num && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.IsLobbyOwner();
			if (!num || flag)
			{
				ShowConnectingMessage(b: false);
				Singleton.Manager<ManNetworkLobby>.inst.LeaveLobby();
				Singleton.Manager<ManUI>.inst.ClearFade(3f);
				Action accept = delegate
				{
					Singleton.Manager<ManUI>.inst.PopScreen();
					Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
				};
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 47);
				string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 46);
				(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(localisedString, accept, localisedString2);
				Singleton.Manager<ManUI>.inst.ExitAllScreens();
				Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
			}
		}
		if (m_IsHost)
		{
			m_ServerStartButton.interactable = Singleton.Manager<ManNetwork>.inst.Options != null;
		}
	}

	private void OnCancelPressed(PayloadUIEventData evt)
	{
		evt.Use();
		Exit();
	}

	public void HiddenByGamePadDisconnect(bool hide)
	{
		m_HiddenByGamePadDisconnect = hide;
	}
}
