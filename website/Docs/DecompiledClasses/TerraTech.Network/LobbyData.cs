#define UNITY_EDITOR
using Steamworks;

namespace TerraTech.Network;

public sealed class LobbyData
{
	public enum EnumLobbyChoiceIndex
	{
		LCI_GAME_TYPE,
		LCI_DURATION_TYPE,
		LCI_INVENTORY_ENABLED,
		LCI_SCAVENGER_ENABLED,
		LCI_KILL_STREAK_REWARDS_ENABLED,
		LCI_KILL_STREAK_RESET_WHEN_DESTROYED_ENABLED,
		LCI_KILL_STREAK_REWARD_STACK_ENABLED,
		LCI_KILL_STREAK_MAXED_REWARD_AUTO_CLAIM_ENABLED,
		LCI_KILL_STREAK_PREVENT_CLAIM_NEAR_DANGER_ENABLED,
		LCI_KILL_STREAK_DANGER_RANGE_TYPE,
		LCI_KILL_STREAK_THRESHOLD_MULT_TYPE,
		LCI_CAB_SELF_DESTRUCT_ENABLED,
		LCI_CAB_SELF_DESTRUCT_TIME,
		LCI_MAP_SIZE,
		LCI_TEAM_MATCH,
		LCI_COLLIDE_TO_SCAVENGE,
		LCI_KEEP_LOOTED_BLOCKS_ON_RESPAWN,
		LCI_CLEAR_UNUSED_INVENTORY_AFTER_SPAWN_BUBBLE_ENABLED,
		LCI_CRATE_DROPS_ENABLED,
		LCI_CRATE_DROP_MIN_DISTANCE_TYPE,
		LCI_CRATE_DROP_FREQUENCY_TYPE,
		LCI_CRATE_DROP_BLOCK_QUANTITY_TYPE,
		LCI_CRATE_DROP_PICKUP_RANGE_TYPE,
		LCI_CRATE_DROP_DELAY_MINS_TYPE,
		LCI_HEAL_BUILD_ENABLED,
		LCI_HEAL_WARM_UP_TIME,
		LCI_HEAL_RATE,
		LCI_INTERRUPT_COOLDOWN,
		LCI_DEATH_STREAK_REWARDS_ENABLED,
		LCI_DEATH_STREAK_REWARDS_MIN_DEATHS_REQD,
		LCI_DEATH_STREAK_REWARDS_SUBS_DEATHS_REQD,
		LCI_UNUSED_,
		LCI_CO_OP_ALLOW_PLAYER_TECH_MODS,
		LCI_LEVEL_DATA,
		LCI_NUM_OPTIONS
	}

	public TTNetworkID m_IDLobby;

	public string m_LobbyName;

	public string m_Language;

	public Lobby.LobbyVisibility m_Visibility;

	public int m_ProtocolVersion;

	public int m_NumUsers;

	public int m_MaxUserLimit;

	public int m_NumFriends;

	public bool m_HostIsFriend;

	public int m_PingTimeMS;

	public bool m_GameInProgress;

	public int[] m_Choices;

	public BannedPlayers m_BannedPlayers = new BannedPlayers();

	public PublishedFileId_t[] m_WorkshopIds;

	public int NumMods
	{
		get
		{
			if (m_WorkshopIds != null)
			{
				return m_WorkshopIds.Length;
			}
			return 0;
		}
	}

	public MultiplayerModeType GameTypeChoice => (MultiplayerModeType)ChoiceForIndex(EnumLobbyChoiceIndex.LCI_GAME_TYPE);

	public int DurationChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_DURATION_TYPE);

	public int InventoryChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_INVENTORY_ENABLED);

	public int ScavengerChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_SCAVENGER_ENABLED);

	public int KillStreakRewardsChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_KILL_STREAK_REWARDS_ENABLED);

	public int KillStreakResetWhenDestroyedChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_KILL_STREAK_RESET_WHEN_DESTROYED_ENABLED);

	public int KillStreakRewardStackChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_KILL_STREAK_REWARD_STACK_ENABLED);

	public int KillStreakMaxedRewardAutoClaimChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_KILL_STREAK_MAXED_REWARD_AUTO_CLAIM_ENABLED);

	public int KillStreakPreventClaimNearDangerChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_KILL_STREAK_PREVENT_CLAIM_NEAR_DANGER_ENABLED);

	public int KillStreakClaimDangerRangeChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_KILL_STREAK_DANGER_RANGE_TYPE);

	public int KillStreakKillThresholdMultiplierChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_KILL_STREAK_THRESHOLD_MULT_TYPE);

	public int CabSelfDestructChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_CAB_SELF_DESTRUCT_ENABLED);

	public int CabSelfDestructTimeRangeChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_CAB_SELF_DESTRUCT_TIME);

	public int MapSizeChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_MAP_SIZE);

	public int TeamMatchChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_TEAM_MATCH);

	public int CollideToScavengeChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_COLLIDE_TO_SCAVENGE);

	public int KeepLootedBlocksOnRespawnChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_KEEP_LOOTED_BLOCKS_ON_RESPAWN);

	public int ClearUnusedInventoryAfterSpawnBubbleChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_CLEAR_UNUSED_INVENTORY_AFTER_SPAWN_BUBBLE_ENABLED);

	public int CrateDropsEnabledChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_CRATE_DROPS_ENABLED);

	public int CrateDropMinDistanceChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_CRATE_DROP_MIN_DISTANCE_TYPE);

	public int CrateDropFrequencyChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_CRATE_DROP_FREQUENCY_TYPE);

	public int CrateDropBlockQuantityChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_CRATE_DROP_BLOCK_QUANTITY_TYPE);

	public int CrateDropPickupRangeChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_CRATE_DROP_PICKUP_RANGE_TYPE);

	public int CrateDropDelayMinsChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_CRATE_DROP_DELAY_MINS_TYPE);

	public int HealBuildChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_HEAL_BUILD_ENABLED);

	public int HealWarmUpChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_HEAL_WARM_UP_TIME);

	public int HealRateChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_HEAL_RATE);

	public int HealInterruptCooldownChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_INTERRUPT_COOLDOWN);

	public int DeathStreakEnabledChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_DEATH_STREAK_REWARDS_ENABLED);

	public int DeathStreakMinDeathsChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_DEATH_STREAK_REWARDS_MIN_DEATHS_REQD);

	public int DeathStreakSubsDeathsChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_DEATH_STREAK_REWARDS_SUBS_DEATHS_REQD);

	public int CoOpAllowPlayerTechModsChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_CO_OP_ALLOW_PLAYER_TECH_MODS);

	public int LevelDataChoice => ChoiceForIndex(EnumLobbyChoiceIndex.LCI_LEVEL_DATA);

	public TTNetworkID ID => m_IDLobby;

	public int NumFriendsEstimate
	{
		get
		{
			if (m_NumFriends <= 0)
			{
				if (!m_HostIsFriend)
				{
					return 0;
				}
				return 1;
			}
			return m_NumFriends;
		}
	}

	public void SetDefaultChoices()
	{
		m_Choices = new int[34];
		m_Choices[0] = 0;
		m_Choices[1] = 1;
		m_Choices[2] = 1;
		m_Choices[3] = 1;
		m_Choices[4] = 1;
		m_Choices[5] = 1;
		m_Choices[6] = 0;
		m_Choices[7] = 0;
		m_Choices[8] = 1;
		m_Choices[9] = 3;
		m_Choices[10] = 0;
		m_Choices[11] = 1;
		m_Choices[12] = 0;
		m_Choices[13] = 0;
		m_Choices[14] = 0;
		m_Choices[15] = 1;
		m_Choices[16] = 1;
		m_Choices[17] = 0;
		m_Choices[18] = 1;
		m_Choices[19] = 5;
		m_Choices[20] = 1;
		m_Choices[21] = 4;
		m_Choices[22] = 2;
		m_Choices[23] = 1;
		m_Choices[24] = 1;
		m_Choices[25] = 0;
		m_Choices[26] = 1;
		m_Choices[27] = 2;
		m_Choices[28] = 1;
		m_Choices[29] = 1;
		m_Choices[30] = 0;
		m_Choices[32] = 1;
		m_Choices[33] = 0;
	}

	private int ChoiceForIndex(EnumLobbyChoiceIndex lci)
	{
		d.Assert(lci >= EnumLobbyChoiceIndex.LCI_GAME_TYPE && lci < EnumLobbyChoiceIndex.LCI_NUM_OPTIONS);
		int num = ((m_Choices != null) ? m_Choices.Length : 0);
		if ((int)lci >= num)
		{
			return 0;
		}
		return m_Choices[(int)lci];
	}
}
