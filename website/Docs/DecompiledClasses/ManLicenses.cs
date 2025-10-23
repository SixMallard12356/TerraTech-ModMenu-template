#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DevCommands;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class ManLicenses : Singleton.Manager<ManLicenses>, Mode.IManagerModeEvents
{
	public class LicenseProgression
	{
		public class SaveData
		{
			public int m_CurrentXP;

			public int m_CurrentLevel;

			public bool m_Discovered;
		}
	}

	public class LicenseBackgroundTaskProgression
	{
	}

	private class SaveData
	{
		public Dictionary<FactionSubTypes, FactionLicense.Progress> m_FactionLicenseProgress = new Dictionary<FactionSubTypes, FactionLicense.Progress>(default(FactionSubTypesComparer));

		public Dictionary<int, BlockState> m_BlockStates = new Dictionary<int, BlockState>();

		public RewardSpawner.SaveData m_RewardSpawnerSaveData;
	}

	public enum BlockState
	{
		Unknown,
		Available,
		Discovered
	}

	[Serializable]
	public struct ThresholdsTableEntry
	{
		public FactionLicense.Thresholds thresholds;

		public FactionSubTypes faction;
	}

	[SerializeField]
	public BlockUnlockTable m_UnlockTable;

	[SerializeField]
	private BlockRewardPoolTable m_RewardPoolTable;

	[SerializeField]
	private BlockTypes[] m_AutoDiscoveredBlocks;

	[SerializeField]
	private int m_MaxOverallGrade = 5;

	[SerializeField]
	[FormerlySerializedAs("m_LicenseProgressionData")]
	public List<ThresholdsTableEntry> m_ThresholdData = new List<ThresholdsTableEntry>();

	public bool XpDisabled;

	public Event<FactionSubTypes> AddedXPEvent;

	public Event<FactionSubTypes, int> LevelUpEvent;

	public Event<FactionSubTypes> LicenceMaxedEvent;

	public Event<bool> ShowAllXpBarsEvent;

	public Event<BlockTypes> BlockDiscoveredEvent;

	public Event<BlockTypes> BlockAvailableEvent;

	private Dictionary<FactionSubTypes, FactionLicense> m_FactionLicenses = new Dictionary<FactionSubTypes, FactionLicense>(default(FactionSubTypesComparer));

	private RewardSpawner m_RewardSpawner = new RewardSpawner();

	private SaveData m_Save = new SaveData();

	private bool m_HasOptionalLoadState;

	public List<FactionSubTypes> GetSupportedCorporationsEditorOnly()
	{
		List<FactionSubTypes> list = new List<FactionSubTypes>();
		for (int i = 0; i < m_ThresholdData.Count; i++)
		{
			list.Add(m_ThresholdData[i].faction);
		}
		return list;
	}

	public int GetMaxSupportedGradeEditorOnly(FactionSubTypes corporation)
	{
		int result = 0;
		for (int i = 0; i < m_ThresholdData.Count; i++)
		{
			if (m_ThresholdData[i].faction == corporation)
			{
				result = m_ThresholdData[i].thresholds.m_MaxSupportedLevel;
				break;
			}
		}
		return result;
	}

	public int GetMaxGradeEditorOnly()
	{
		return m_MaxOverallGrade;
	}

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		m_HasOptionalLoadState = optionalLoadState != null;
		optionalLoadState?.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManLicenses, out m_Save);
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(OnModeStart);
	}

	private void OnModeStart(Mode mode)
	{
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Unsubscribe(OnModeStart);
		bool flag = true;
		if (m_HasOptionalLoadState)
		{
			m_HasOptionalLoadState = false;
			if (m_Save != null)
			{
				SetupLicenses();
				flag = false;
				m_RewardSpawner.Load(m_Save.m_RewardSpawnerSaveData);
			}
		}
		if (flag)
		{
			Init();
		}
		AutoDiscoverBlocks();
		foreach (ThresholdsTableEntry thresholdDatum in m_ThresholdData)
		{
			FactionSubTypes faction = thresholdDatum.faction;
			if (!m_Save.m_FactionLicenseProgress.TryGetValue(faction, out var value))
			{
				continue;
			}
			GetLicense(faction);
			int currentLevel = value.m_CurrentLevel;
			if (currentLevel < MaxSupportedTier(faction))
			{
				int maxXPAtLevel = thresholdDatum.thresholds.GetMaxXPAtLevel(currentLevel);
				if (value.m_CurrentXP >= maxXPAtLevel)
				{
					int num = value.m_CurrentXP - (maxXPAtLevel - 1);
					d.Log($"Applying fixup to XP for {faction}. {num} XP removed then re-added");
					value.m_CurrentXP -= num;
					AddXP(faction, num);
				}
				else if (value.m_CurrentXP == maxXPAtLevel - 1 && faction == FactionSubTypes.EXP)
				{
					d.Log($"Applying fixup to XP for {faction}. 1 XP added.");
					AddXP(faction, 1);
				}
			}
		}
	}

	public void Save(ManSaveGame.State saveState)
	{
		m_RewardSpawner.Save(ref m_Save.m_RewardSpawnerSaveData);
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManLicenses, m_Save);
	}

	public void ModeExit()
	{
	}

	public int GetNumCorps()
	{
		return EnumValuesIterator<FactionSubTypes>.Count + Singleton.Manager<ManMods>.inst.GetNumCustomCorps();
	}

	public IEnumerable<int> GetAllCorpIDs()
	{
		int numVanillaCorps = EnumValuesIterator<FactionSubTypes>.Count;
		for (int id = 0; id < numVanillaCorps; id++)
		{
			yield return id;
		}
		foreach (int customCorpID in Singleton.Manager<ManMods>.inst.GetCustomCorpIDs())
		{
			yield return customCorpID;
		}
	}

	public void Clear()
	{
		if (!Globals.inst.m_DisableLicenses)
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.FactionLicences);
		}
	}

	public void SetHUDVisible(bool visible)
	{
		if (visible)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.FactionLicences);
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.FactionLicences);
		}
	}

	public bool IsLicenseSupported(FactionSubTypes corporation)
	{
		return GetLicense(corporation)?.IsSupported ?? false;
	}

	public bool IsLicenseDiscovered(FactionSubTypes corporation)
	{
		return GetLicense(corporation)?.IsDiscovered ?? false;
	}

	public int MaxSupportedTier(FactionSubTypes corporation)
	{
		int result = int.MaxValue;
		FactionLicense license = GetLicense(corporation);
		if (license != null)
		{
			result = license.NumXpLevels - 1;
		}
		return result;
	}

	public int GetBlockTier(BlockTypes blockType, bool supressWarnings = true)
	{
		return m_UnlockTable.GetBlockTier(blockType, supressWarnings);
	}

	public BlockState GetBlockState(BlockTypes blockType)
	{
		if (m_Save.m_BlockStates.TryGetValue((int)blockType, out var value))
		{
			return value;
		}
		return BlockState.Unknown;
	}

	public BlockUnlockTable GetBlockUnlockTable()
	{
		return m_UnlockTable;
	}

	public BlockRewardPoolTable GetRewardPoolTable()
	{
		return m_RewardPoolTable;
	}

	public int GetCurrentLevel(FactionSubTypes corporation)
	{
		return GetLicense(corporation)?.CurrentLevel ?? (-1);
	}

	public bool HasPercentageXpInGrade(FactionSubTypes corporation, int grade, float xpPercentage)
	{
		bool result = false;
		int currentLevel = GetCurrentLevel(corporation);
		if (currentLevel != -1 || currentLevel >= grade)
		{
			if (currentLevel > grade)
			{
				result = true;
			}
			else
			{
				FactionLicense license = GetLicense(corporation);
				if ((float)(license.CurrentAbsoluteXP - license.BaseLevelAbsoluteXP) / (float)(license.NextLevelAbsoluteXP - license.BaseLevelAbsoluteXP) >= xpPercentage)
				{
					result = true;
				}
			}
		}
		return result;
	}

	[DevCommand(Name = "AddLicense", Access = Access.DevCheat, Users = User.Host)]
	public void UnlockCorp(FactionSubTypes corporation, bool showLevelUp)
	{
		if (ManNetwork.IsHost)
		{
			UnlockCorpInternal(corporation, showLevelUp);
			Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.XpChanged, CreateXpChangedMessage(GetLicense(corporation), showLevelUp));
		}
	}

	private void UnlockCorpInternal(FactionSubTypes corporation, bool showLevelUp)
	{
		FactionLicense license = GetLicense(corporation);
		if (license != null)
		{
			license.Discover();
			LevelUpBlocks(license, 0);
			UILicenses uILicenses = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.FactionLicences) as UILicenses;
			if ((bool)uILicenses)
			{
				uILicenses.ShowCorpLicense(corporation);
			}
			if (showLevelUp)
			{
				Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.LicenseUpgrade);
				ShowLevelUpScreen(license);
			}
			LevelUpEvent.Send(corporation, 0);
			_ = ManNetwork.IsHost;
		}
	}

	private void OnClientXpUpdate(NetworkMessage netMsg)
	{
		XpChangedMessage xpChangedMessage = netMsg.ReadMessage<XpChangedMessage>();
		FactionLicense license = GetLicense(xpChangedMessage.m_Corporation);
		if (license == null)
		{
			d.LogError($"Failed to find license for corp {xpChangedMessage.m_Corporation} in ManLicense.OnClientXpUpdate");
			return;
		}
		if (xpChangedMessage.m_Discovered && !license.IsDiscovered)
		{
			UnlockCorpInternal(xpChangedMessage.m_Corporation, xpChangedMessage.m_ShowUI);
		}
		if (license.IsDiscovered && xpChangedMessage.m_AbsoluteXP > license.CurrentAbsoluteXP)
		{
			AddXPInternal(license, xpChangedMessage.m_AbsoluteXP - license.CurrentAbsoluteXP, xpChangedMessage.m_ShowUI);
		}
	}

	public void AddXP(FactionSubTypes corporation, int xp, bool showUI = true)
	{
		if (!XpDisabled && Singleton.Manager<ManGameMode>.inst.CanEarnXp() && ManNetwork.IsHost)
		{
			FactionLicense license = GetLicense(corporation);
			AddXPInternal(license, xp, showUI);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.XpChanged, CreateXpChangedMessage(license, showUI));
			}
		}
	}

	private void AddXPInternal(FactionLicense license, int xp, bool showUI)
	{
		if (license == null || !license.IsDiscovered || license.CurrentLevel >= license.NumXpLevels)
		{
			return;
		}
		int remainingXP = 0;
		bool leveledUP = false;
		bool flag = license.AddXP(xp, out remainingXP, out leveledUP);
		FactionSubTypes corporation = license.Corporation;
		if (flag)
		{
			AddedXPEvent.Send(corporation);
		}
		if (leveledUP)
		{
			int num = license.CurrentLevel + 1;
			if (num < license.NumXpLevels)
			{
				LevelUp(corporation, num, showUI);
			}
			else
			{
				if (showUI)
				{
					Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.LicenceMaxedNotification, corporation);
				}
				int maxGrade = m_UnlockTable.GetMaxGrade(corporation);
				for (int i = 0; i < maxGrade; i++)
				{
					DiscoverEntireTier(corporation, i);
				}
				LicenceMaxedEvent.Send(corporation);
				if (ManNetwork.IsHost)
				{
					HasMaxedAllLicences();
				}
			}
		}
		if (flag)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.EarnXP);
		}
		if (remainingXP != 0)
		{
			AddXPInternal(license, remainingXP, showUI);
		}
	}

	private XpChangedMessage CreateXpChangedMessage(FactionLicense license, bool showUI)
	{
		return new XpChangedMessage
		{
			m_ShowUI = showUI,
			m_Corporation = license.Corporation,
			m_Discovered = license.IsDiscovered,
			m_AbsoluteXP = license.CurrentAbsoluteXP
		};
	}

	public void DiscoverBlock(BlockTypes blockType)
	{
		if (GetBlockState(blockType) != BlockState.Discovered)
		{
			SetBlockState(blockType, BlockState.Discovered);
		}
	}

	public bool IsBlockDiscovered(BlockTypes blockType)
	{
		return GetBlockState(blockType) == BlockState.Discovered;
	}

	public int NumDiscoveredBlocks()
	{
		int num = 0;
		foreach (KeyValuePair<int, BlockState> blockState in m_Save.m_BlockStates)
		{
			if (blockState.Value == BlockState.Discovered)
			{
				num++;
			}
		}
		return num;
	}

	private void CheckCoopDiscoveryBroadcast(BlockTypes blockType)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer && Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeCoOpCampaign>())
		{
			SystemChatMessage systemChatMessage = new SystemChatMessage();
			systemChatMessage.m_Bank = LocalisationEnums.StringBanks.SystemChatMessage;
			systemChatMessage.m_StringID = 4;
			systemChatMessage.m_Params = new string[1] { blockType.ToString() };
			SystemChatMessage message = systemChatMessage;
			Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.AddSystemChatMessage, message);
		}
	}

	public List<BlockTypes> GetInitialTierBlocks(FactionSubTypes corp, int tier)
	{
		return m_UnlockTable.GetInitialBlocksInTier(tier, corp);
	}

	public List<BlockTypes> GetLevelUpScreenBlocksInTier(FactionSubTypes corp, int tier)
	{
		return m_UnlockTable.GetLevelUpScreenBlocksInTier(tier, corp);
	}

	public bool HasMaxedAllLicences()
	{
		bool result = true;
		foreach (FactionLicense value in m_FactionLicenses.Values)
		{
			if (!value.HasReachedMaxLevel)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	public FactionLicense GetLicense(FactionSubTypes faction)
	{
		m_FactionLicenses.TryGetValue(faction, out var value);
		return value;
	}

	public void SendXpLevelsToClient(NetPlayer targetPlayer)
	{
		foreach (KeyValuePair<FactionSubTypes, FactionLicense> factionLicense in m_FactionLicenses)
		{
			XpChangedMessage message = CreateXpChangedMessage(factionLicense.Value, showUI: false);
			Singleton.Manager<ManNetwork>.inst.SendToClient(targetPlayer.connectionToClient.connectionId, TTMsgType.XpChanged, message);
		}
	}

	public int GetTotalLicenseGrades()
	{
		int num = 0;
		foreach (int allCorpID in GetAllCorpIDs())
		{
			if (IsLicenseSupported((FactionSubTypes)allCorpID) && IsLicenseDiscovered((FactionSubTypes)allCorpID))
			{
				num += GetCurrentLevel((FactionSubTypes)allCorpID) + 1;
			}
		}
		return num;
	}

	private void Init()
	{
		if (!Globals.inst.m_DisableLicenses)
		{
			m_Save = new SaveData();
			SetupLicenses();
			int count = EnumValuesIterator<BlockTypes>.Count;
			for (int i = 0; i < count; i++)
			{
				m_Save.m_BlockStates.Add(i, BlockState.Unknown);
			}
			UnlockCorp(FactionSubTypes.GSO, showLevelUp: false);
		}
	}

	private void LevelUp(FactionSubTypes corporation, int level, bool showUI)
	{
		FactionLicense license = GetLicense(corporation);
		if (license == null || license.HasReachedMaxLevel)
		{
			return;
		}
		license.LevelUp(level);
		if (showUI)
		{
			string text = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Licensing, 10), level + 1);
			string[] messages = new string[1] { text };
			ManOnScreenMessages.Speaker speaker;
			switch (license.Corporation)
			{
			case FactionSubTypes.GSO:
				speaker = ManOnScreenMessages.Speaker.GSOGeneric;
				break;
			case FactionSubTypes.GC:
				speaker = ManOnScreenMessages.Speaker.GCGeneric;
				break;
			case FactionSubTypes.VEN:
				speaker = ManOnScreenMessages.Speaker.VENGeneric;
				break;
			case FactionSubTypes.HE:
				speaker = ManOnScreenMessages.Speaker.HEGeneric;
				break;
			case FactionSubTypes.BF:
				speaker = ManOnScreenMessages.Speaker.BFGeneric;
				break;
			case FactionSubTypes.SJ:
				speaker = ManOnScreenMessages.Speaker.SJGeneric;
				break;
			case FactionSubTypes.EXP:
				speaker = ManOnScreenMessages.Speaker.RRGeneric;
				break;
			default:
				d.LogError("ManLicenses.LevelUp - Failed to find speaker data for " + license.Corporation);
				speaker = ManOnScreenMessages.Speaker.None;
				break;
			}
			Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(new ManOnScreenMessages.OnScreenMessage(messages, ManOnScreenMessages.MessagePriority.High, hold: false, null, speaker));
			ShowLevelUpScreen(license);
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.LevelUp);
		}
		DiscoverEntireTier(corporation, level - 1);
		LevelUpBlocks(license, level);
		LevelUpEvent.Send(corporation, level);
		_ = ManNetwork.IsHost;
	}

	private void ShowLevelUpScreen(FactionLicense license)
	{
		if (!license.HasReachedMaxLevel)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.LicenceLevelUp, license);
		}
	}

	private void LevelUpBlocks(FactionLicense license, int level)
	{
		FactionSubTypes corporation = license.Corporation;
		SetInitialTierBlocks(corporation, level, BlockState.Discovered);
		int num = level + 1;
		if (num < m_MaxOverallGrade)
		{
			SetInitialTierBlocks(corporation, num, BlockState.Available);
		}
		if (ManNetwork.IsHost)
		{
			AwardLevelUpBlocks(corporation, level);
		}
	}

	private void SetInitialTierBlocks(FactionSubTypes corp, int tier, BlockState state)
	{
		List<BlockTypes> initialBlocksInTier = m_UnlockTable.GetInitialBlocksInTier(tier, corp);
		for (int i = 0; i < initialBlocksInTier.Count; i++)
		{
			SetBlockState(initialBlocksInTier[i], state);
		}
	}

	private void DiscoverEntireTier(FactionSubTypes corp, int tier)
	{
		List<BlockTypes> allBlocksInTier = m_UnlockTable.GetAllBlocksInTier(tier, corp, includeBonusBlocks: true);
		for (int i = 0; i < allBlocksInTier.Count; i++)
		{
			SetBlockState(allBlocksInTier[i], BlockState.Discovered);
		}
	}

	private void SetBlockState(BlockTypes blockType, BlockState blockState)
	{
		m_Save.m_BlockStates.TryGetValue((int)blockType, out var value);
		if (value != blockState)
		{
			switch (blockState)
			{
			case BlockState.Available:
				BlockAvailableEvent.Send(blockType);
				break;
			case BlockState.Discovered:
				BlockDiscoveredEvent.Send(blockType);
				CheckCoopDiscoveryBroadcast(blockType);
				break;
			case BlockState.Unknown:
				d.LogError("ManLicenses.SetBlockState - Block State should NOT be set to Unknown");
				break;
			}
		}
		m_Save.m_BlockStates[(int)blockType] = blockState;
	}

	private void AwardLevelUpBlocks(FactionSubTypes corporation, int grade)
	{
		if (corporation != FactionSubTypes.GSO || grade > 0)
		{
			List<BlockTypes> initialRewardBlocksInTier = Singleton.Manager<ManLicenses>.inst.GetBlockUnlockTable().GetInitialRewardBlocksInTier(grade, corporation);
			initialRewardBlocksInTier = Globals.inst.m_BlockPairsList.FillPairs(initialRewardBlocksInTier, onlyEnsurePairedOnce: true).ToList();
			Vector3 playerPos = Singleton.playerPos;
			m_RewardSpawner.RewardBlocksByCrate(initialRewardBlocksInTier, playerPos, corporation);
		}
	}

	private void AutoDiscoverBlocks()
	{
		for (int i = 0; i < m_AutoDiscoveredBlocks.Length; i++)
		{
			SetBlockState(m_AutoDiscoveredBlocks[i], BlockState.Discovered);
		}
	}

	private void SetupLicenses()
	{
		m_FactionLicenses.Clear();
		foreach (ThresholdsTableEntry thresholdDatum in m_ThresholdData)
		{
			FactionSubTypes faction = thresholdDatum.faction;
			m_Save.m_FactionLicenseProgress.TryGetValue(faction, out var value);
			if (value == null)
			{
				value = new FactionLicense.Progress();
				m_Save.m_FactionLicenseProgress.Add(faction, value);
			}
			FactionLicense factionLicense = new FactionLicense(faction, thresholdDatum.thresholds, value);
			m_FactionLicenses.Add(faction, factionLicense);
			int currentLevel = value.m_CurrentLevel;
			int maxXPAtLevel = thresholdDatum.thresholds.GetMaxXPAtLevel(currentLevel);
			int num = ((currentLevel > 0) ? thresholdDatum.thresholds.GetMaxXPAtLevel(currentLevel - 1) : 0);
			int num2 = factionLicense.NumXpLevels - 1;
			if ((value.m_CurrentXP < maxXPAtLevel || value.m_CurrentLevel >= num2) && value.m_CurrentXP < num)
			{
				value.m_CurrentXP = num;
			}
			if (value.m_CurrentLevel > num2)
			{
				d.Log(string.Concat("ManLicense.SetupLicenses - Above supported grade for ", faction, " setting back to current max."));
				value.m_CurrentLevel = num2;
				value.m_CurrentXP = factionLicense.GetMaxXP();
			}
			else if (value.m_CurrentLevel < 0)
			{
				d.Log(string.Concat("ManLicense.SetupLicenses - License ", faction, " below minimum value. Setting back to 0."));
				value.m_CurrentLevel = 0;
			}
		}
		Singleton.Manager<ManHUD>.inst.InitialiseHudElement(ManHUD.HUDElementType.FactionLicences, m_FactionLicenses);
	}

	[Conditional("USE_ANALYTICS")]
	private void SendLevelUpAnalyticEvent(FactionLicense license)
	{
		new Dictionary<string, object>
		{
			{ "Corp", license.Corporation },
			{
				"Level",
				license.CurrentLevel + 1
			},
			{
				"total_levels",
				GetTotalLicenseGrades()
			}
		};
	}

	private void Start()
	{
		m_UnlockTable.Init();
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.XpChanged, OnClientXpUpdate);
	}
}
