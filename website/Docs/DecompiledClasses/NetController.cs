#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetController : NetworkBehaviour, ManNetwork.IDumpableBehaviour
{
	private struct KillStreakTracker
	{
		public uint m_netId;

		public int m_killStreak;

		public void reset()
		{
			m_netId = 0u;
			m_killStreak = 0;
		}
	}

	public enum Phase
	{
		GatherPlayers,
		Intro,
		TechSelection,
		Playing,
		Outro,
		Restarting,
		Exiting
	}

	public enum Restriction
	{
		ForceBuildBeamOn,
		NoBuilding,
		NoDamage
	}

	public enum SpawnPolicy
	{
		AtSpawnPoint,
		CloseToAllies,
		AtEncounter
	}

	public enum SpawnOrientation
	{
		Random,
		FacingOutwards,
		FacingInwards
	}

	public enum ScorePolicy
	{
		GameTime,
		Kills,
		SetTime,
		KillMinusDeath,
		NumWaves
	}

	public enum VictoryConditions
	{
		ScoreExceeds,
		FinishWaves,
		None
	}

	public enum WaveCompletion
	{
		AllKilled,
		TimeExpired
	}

	[Serializable]
	public class WaveData
	{
		public List<Wave> m_Waves;

		public WaveCompletion m_WaveEndCondition;

		public int m_MaxEnemies;

		public bool m_Endless;

		public bool m_RepeatLastWave;
	}

	[Serializable]
	public class Wave
	{
		public List<TankPreset> m_WaveEnemies;

		public float m_WaveTime;

		public float m_NextWaveTime;

		public BlockTypes[] m_BlockRewards;

		public bool m_RewardsIntoInventory;

		public bool m_RewardsPerPlayer;
	}

	[Serializable]
	public class GameData
	{
		public GatherPlayers m_GatherPlayersData;

		public PreGame m_PreGameData;

		public InGame m_InGameData;

		public PostGame m_PostGameData;

		public float GetPhaseTime(Phase phase)
		{
			float result = 0f;
			switch (phase)
			{
			case Phase.GatherPlayers:
				result = m_GatherPlayersData.m_WaitTime;
				break;
			case Phase.Intro:
				result = m_PreGameData.m_PreBuildTime;
				break;
			case Phase.TechSelection:
				result = kFirstTechSelectionDurationInSecs;
				break;
			case Phase.Playing:
				result = Singleton.Manager<ManNetwork>.inst.GameDurationTimeInSecs;
				break;
			case Phase.Outro:
				result = m_PostGameData.m_PostGameTime;
				break;
			}
			return result;
		}
	}

	[Serializable]
	public class GatherPlayers
	{
		public float m_WaitTime;

		public LocalisedString m_Message;

		public SpawnPolicy m_SpawnPolicy;

		public SpawnOrientation m_SpawnOrientation;
	}

	[Serializable]
	public class PreGame
	{
		public float m_PreBuildTime;

		public bool m_ForceBuildBeamOn;

		public bool m_InsideShield;

		public LocalisedString m_Message;

		public Restriction[] restrictions;
	}

	[Serializable]
	public class InGame
	{
		public ScorePolicy m_ScorePolicy;

		public float m_GameLength;

		public VictoryConditions m_WinConditions;

		public float m_MaxScore;

		public bool m_UseWaves;

		public WaveData m_WaveData;

		public bool m_PlayerRespawn = true;

		public SpawnPolicy m_RespawnPolicy;

		public SpawnOrientation m_SpawnOrientation;

		public float m_RespawnInvulnerabilityTime;

		public bool m_BuildBeamProtects;

		public Restriction[] restrictions;
	}

	[Serializable]
	public class PostGame
	{
		public float m_PostGameTime;

		public LocalisedString m_Message;
	}

	public Event<Phase> ServerPhaseEnterEvent;

	public Event<Phase> ClientPhaseEnterEvent;

	private static int s_NumRestrictionTypes = -1;

	private static float kFirstTechSelectionDurationInSecs = 10f;

	private NetOptions m_Options;

	private UIMultiplayerHUD m_MultiplayerHud;

	private SpawnPolicy m_CurrentSpawnPolicy;

	private SpawnOrientation m_CurrentSpawnOrientation;

	private int m_nKillStreakTrackers;

	private KillStreakTracker[] m_killStreakTrackers = new KillStreakTracker[16];

	private NetTech m_CachedNetTech;

	private NetBlock m_CachedNetBlock;

	private const int kCachedNetBlockIDInvalid = -1;

	private int m_CachedNetBlockID = -1;

	private bool m_UpdateScores;

	private Dictionary<int, float> m_TeamScore = new Dictionary<int, float>();

	private Dictionary<int, Color> m_TeamColour = new Dictionary<int, Color>();

	private Phase m_CurrentPhase;

	private float m_PhaseTimer;

	private bool m_CurrentPhaseTimerPaused;

	private Bitfield<Restriction> m_Restrictions = new Bitfield<Restriction>();

	private string m_MsgBank;

	private string m_MsgId;

	private bool m_MsgUsesTime;

	private float m_ModeTimer;

	private bool m_ShowTimer;

	private NetworkInstanceId m_NotableTech;

	private NetworkInstanceId m_NotableBlock;

	private Bitfield<Restriction> s_NewRestrictions = new Bitfield<Restriction>();

	private const int kSer_Message = 1;

	private const int kSer_TimeRemaining = 2;

	private const int kSer_ShowTimer = 4;

	private const int kSer_Restrictions = 8;

	private const int kSer_NotableTech = 16;

	private const int kSer_NotableBlock = 32;

	private const int kSer_ServerCurPhase = 64;

	private const int kSer_AllFlagMask = 127;

	public MultiplayerModeType GameModeType => m_Options.m_GameModeType;

	public bool IsMultiTeamGame => m_Options.m_NumTeams > 1;

	public SpawnPolicy ServerSpawnPolicy => m_CurrentSpawnPolicy;

	public SpawnOrientation CurrentSpawnOrientation => m_CurrentSpawnOrientation;

	public bool BuildBeamProtects => m_Options.m_GameData.m_InGameData.m_BuildBeamProtects;

	public float RespawnInvulnerabilityTime => m_Options.m_GameData.m_InGameData.m_RespawnInvulnerabilityTime;

	public bool UseWaves => m_Options.m_GameData.m_InGameData.m_UseWaves;

	public WaveData CurrentWaveData => m_Options.m_GameData.m_InGameData.m_WaveData;

	public Phase CurrentPhase => m_CurrentPhase;

	public float CurrentPhaseTimer => m_PhaseTimer;

	public bool CurrentPhaseTimerPaused
	{
		get
		{
			return m_CurrentPhaseTimerPaused;
		}
		set
		{
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				m_CurrentPhaseTimerPaused = value;
			}
		}
	}

	public ScorePolicy CurrentScorePolicy => m_Options.m_GameData.m_InGameData.m_ScorePolicy;

	public VictoryConditions CurrentVictoryConditions => m_Options.m_GameData.m_InGameData.m_WinConditions;

	public float ScoreToWin => m_Options.m_GameData.m_InGameData.m_MaxScore;

	public bool AllowCollaboration => GameModeType.IsCoOp();

	public float IdealEnemySpawnDistance => m_Options.m_IdealSpawnDistanceEnemies;

	public float IdealAllySpawnDistance => m_Options.m_IdealSpawnDistanceAllies;

	private static int NumRestrictionTypes
	{
		get
		{
			if (s_NumRestrictionTypes < 0)
			{
				s_NumRestrictionTypes = Enum.GetNames(typeof(Restriction)).Length;
			}
			return s_NumRestrictionTypes;
		}
	}

	public void Dump(StringBuilder builder)
	{
		builder.AppendFormat("phase={0} phaseTimer={1}\n", m_CurrentPhase, m_PhaseTimer);
		builder.AppendFormat("modeType={0} modeTimer={1}\n", m_Options.m_GameModeType, m_ModeTimer);
	}

	public override void OnStartServer()
	{
		base.OnStartServer();
		ServerEnterPhase(Phase.GatherPlayers);
	}

	public override void OnNetworkDestroy()
	{
		base.OnNetworkDestroy();
		for (int i = 0; i < NumRestrictionTypes; i++)
		{
			SetRestrictionEnabled((Restriction)i, enabled: false);
		}
		m_MultiplayerHud.Hide(null);
		m_MultiplayerHud = null;
	}

	public override bool OnSerialize(NetworkWriter writer, bool initialState)
	{
		uint num = (initialState ? 127u : base.syncVarDirtyBits);
		writer.WritePackedUInt32(num);
		if ((num & 1) != 0)
		{
			writer.Write(m_MsgBank);
			writer.Write(m_MsgId);
			writer.Write(m_MsgUsesTime);
		}
		if ((num & 2) != 0)
		{
			writer.Write(m_PhaseTimer);
		}
		if ((num & 4) != 0)
		{
			writer.Write(m_ShowTimer);
			writer.Write(m_ModeTimer);
		}
		if ((num & 8) != 0)
		{
			writer.Write(m_Restrictions.Field);
		}
		if ((num & 0x10) != 0)
		{
			writer.Write(m_NotableTech);
		}
		if ((num & 0x20) != 0)
		{
			writer.Write(m_NotableBlock);
		}
		if ((num & 0x40) != 0)
		{
			writer.Write((int)m_CurrentPhase);
		}
		return num != 0;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		uint num = reader.ReadPackedUInt32();
		if ((num & 1) != 0)
		{
			string bankId = reader.ReadString();
			string stringId = reader.ReadString();
			bool usesTime = reader.ReadBoolean();
			SetMessage(bankId, stringId, usesTime);
		}
		if ((num & 2) != 0)
		{
			SetPhaseTimer(reader.ReadSingle());
		}
		if ((num & 4) != 0)
		{
			bool timerPanelVisible = reader.ReadBoolean();
			m_ModeTimer = reader.ReadSingle();
			m_MultiplayerHud.TimerPanelVisible = timerPanelVisible;
		}
		if ((num & 8) != 0)
		{
			s_NewRestrictions.SetFlags(reader.ReadInt32());
			for (int i = 0; i < NumRestrictionTypes; i++)
			{
				SetRestrictionEnabled((Restriction)i, s_NewRestrictions.Contains(i));
			}
		}
		if ((num & 0x10) != 0)
		{
			m_NotableTech = reader.ReadNetworkId();
			UpdateCachedNetTech(m_NotableTech);
		}
		if ((num & 0x20) != 0)
		{
			m_NotableBlock = reader.ReadNetworkId();
			UpdateCachedNetBlock(m_NotableBlock);
		}
		if ((num & 0x40) != 0 && !Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Phase phase = (Phase)reader.ReadInt32();
			if (m_CurrentPhase != phase)
			{
				ClientEnterPhase(phase);
			}
		}
	}

	public void SetNotableTech(NetTech netTech)
	{
		m_NotableTech = ((netTech != null) ? netTech.netId : NetworkInstanceId.Invalid);
		UpdateCachedNetTech(m_NotableTech);
		SetDirtyBit(16u);
	}

	public void SetNotableBlock(NetBlock netBlock)
	{
		m_NotableBlock = ((netBlock != null) ? netBlock.netId : NetworkInstanceId.Invalid);
		UpdateCachedNetBlock(m_NotableBlock);
		SetDirtyBit(32u);
	}

	public NetTech GetNotableTech()
	{
		return m_CachedNetTech;
	}

	public NetBlock GetNotableBlock()
	{
		return m_CachedNetBlock;
	}

	public float GetTeamScore(int teamID)
	{
		float value = 0f;
		m_TeamScore.TryGetValue(teamID, out value);
		return value;
	}

	public Color GetTeamColour(int teamID)
	{
		Color value = Color.white;
		m_TeamColour.TryGetValue(teamID, out value);
		return value;
	}

	[Server]
	public void ServerChangePhase(Phase phase)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetController::ServerChangePhase(NetController/Phase)' called on client");
			return;
		}
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer);
		if (m_CurrentPhase != Phase.Restarting)
		{
			if (phase == m_CurrentPhase + 1 || phase == Phase.Exiting)
			{
				ServerEnterPhase(phase);
			}
			else
			{
				d.LogError("NetController.ServerChangePhase - not next phase...");
			}
		}
		else
		{
			d.Assert(phase == Phase.Intro);
			ServerEnterPhase(phase);
		}
	}

	public void SetGameTimer(float timer)
	{
		m_ModeTimer = timer;
		m_ShowTimer = true;
		SetDirtyBit(4u);
		m_MultiplayerHud.TimerPanelVisible = true;
	}

	public void ClearGameTimer()
	{
		m_ShowTimer = false;
		SetDirtyBit(4u);
		m_MultiplayerHud.TimerPanelVisible = false;
	}

	public void CheatSkipTimerToEnd()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer && Singleton.Manager<ManNetwork>.inst.IsInPhase(Phase.Playing))
		{
			SetPhaseTimer(Mathf.Min(m_PhaseTimer, 5f));
		}
		else
		{
			d.LogWarningFormat("NetController unable to skip to end of session as we are not the server or not in the playing phase");
		}
	}

	private void UpdateCachedNetTech(NetworkInstanceId netID)
	{
		bool flag = false;
		if (netID != NetworkInstanceId.Invalid)
		{
			if (m_CachedNetTech == null || m_CachedNetTech.netId != netID)
			{
				GameObject gameObject = ClientScene.FindLocalObject(netID);
				if (gameObject != null)
				{
					m_CachedNetTech = gameObject.GetComponent<NetTech>();
				}
				else
				{
					d.LogError("NetController.UpdateCachedNetTech - no local game object found with netId=" + netID);
					flag = true;
				}
			}
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			m_CachedNetTech = null;
		}
	}

	private void UpdateCachedNetBlock(NetworkInstanceId netID)
	{
		bool flag = false;
		if (netID != NetworkInstanceId.Invalid)
		{
			if (m_CachedNetBlock == null || m_CachedNetBlock.netId != netID)
			{
				GameObject gameObject = ClientScene.FindLocalObject(netID);
				if (gameObject != null)
				{
					m_CachedNetBlock = gameObject.GetComponent<NetBlock>();
					Visible visible = m_CachedNetBlock.block.visible;
					if (m_CachedNetBlockID != -1)
					{
						Singleton.Manager<ManVisible>.inst.StopTrackingVisible(m_CachedNetBlockID);
					}
					m_CachedNetBlockID = visible.ID;
					TrackedVisible refToTrack = new TrackedVisible(m_CachedNetBlock.block.visible.ID, visible, ObjectTypes.Block, RadarTypes.MultiplayerObject);
					Singleton.Manager<ManVisible>.inst.TrackVisible(refToTrack);
				}
				else
				{
					d.LogError("NetController.UpdateCachedNetBlock - no local game object found with netId=" + netID);
					flag = true;
				}
			}
		}
		else
		{
			flag = true;
		}
		if (flag && m_CachedNetBlockID != -1)
		{
			Singleton.Manager<ManVisible>.inst.StopTrackingVisible(m_CachedNetBlockID);
			m_CachedNetBlock = null;
			m_CachedNetBlockID = -1;
		}
	}

	[Server]
	private void ServerEnterPhase(Phase phase)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetController::ServerEnterPhase(NetController/Phase)' called on client");
			return;
		}
		m_CurrentPhase = phase;
		SetDirtyBit(64u);
		m_ShowTimer = false;
		LocalisedString localisedString = null;
		bool usesTime = false;
		switch (phase)
		{
		case Phase.GatherPlayers:
		{
			GatherPlayers gatherPlayersData = m_Options.m_GameData.m_GatherPlayersData;
			localisedString = gatherPlayersData.m_Message;
			m_CurrentSpawnPolicy = gatherPlayersData.m_SpawnPolicy;
			m_CurrentSpawnOrientation = gatherPlayersData.m_SpawnOrientation;
			PreGame preGameData = m_Options.m_GameData.m_PreGameData;
			if (Singleton.Manager<ManNetwork>.inst.ServerSpawnBank != null)
			{
				Singleton.Manager<ManNetwork>.inst.ServerSpawnBank.SetAllSpawnShieldsEnabled(preGameData.m_InsideShield);
			}
			for (int k = 0; k < NumRestrictionTypes; k++)
			{
				SetRestrictionEnabled((Restriction)k, enabled: false);
			}
			for (int l = 0; l < preGameData.restrictions.Length; l++)
			{
				SetRestrictionEnabled(preGameData.restrictions[l], enabled: true);
			}
			SetDirtyBit(8u);
			break;
		}
		case Phase.Intro:
			localisedString = m_Options.m_GameData.m_PreGameData.m_Message;
			usesTime = true;
			SubscribeToScoreChanges();
			break;
		case Phase.Playing:
		{
			InGame inGameData = m_Options.m_GameData.m_InGameData;
			m_CurrentSpawnPolicy = inGameData.m_RespawnPolicy;
			for (int i = 0; i < NumRestrictionTypes; i++)
			{
				SetRestrictionEnabled((Restriction)i, enabled: false);
			}
			for (int j = 0; j < inGameData.restrictions.Length; j++)
			{
				SetRestrictionEnabled(inGameData.restrictions[j], enabled: true);
			}
			SetDirtyBit(8u);
			break;
		}
		case Phase.Outro:
			localisedString = m_Options.m_GameData.m_PostGameData.m_Message;
			break;
		case Phase.Exiting:
			Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
			break;
		}
		SetPhaseTimer(m_Options.m_GameData.GetPhaseTime(phase));
		if (localisedString != null)
		{
			SetMessage(localisedString.m_Bank, localisedString.m_Id, usesTime);
		}
		else
		{
			SetMessage("", "", usesTime);
		}
		SetDirtyBit(1u);
		if (m_PhaseTimer > 0f)
		{
			m_CurrentPhaseTimerPaused = false;
			m_MultiplayerHud.TimerPanelVisible = m_ShowTimer;
			m_MultiplayerHud.Timer.Reset(m_PhaseTimer);
			SetDirtyBit(2u);
			SetDirtyBit(4u);
		}
		else
		{
			ClearGameTimer();
		}
		ServerPhaseEnterEvent.Send(m_CurrentPhase);
	}

	private void ClientEnterPhase(Phase phase)
	{
		d.Log("NetController.ClientEnterPhase Phase=" + phase);
		m_CurrentPhase = phase;
		if (phase == Phase.Intro)
		{
			SubscribeToScoreChanges();
		}
		ClientPhaseEnterEvent.Send(m_CurrentPhase);
	}

	private void UpdatePhase()
	{
		bool flag = true;
		if (m_CurrentPhase == Phase.Playing && Singleton.Manager<ManNetwork>.inst.NetController.GameModeType.IsCoOp())
		{
			flag = false;
		}
		if (CurrentPhaseTimerPaused)
		{
			flag = false;
		}
		bool flag2 = false;
		if (flag && m_PhaseTimer > 0f)
		{
			float phaseTimer = Mathf.Max(0f, m_PhaseTimer - Time.deltaTime);
			SetPhaseTimer(phaseTimer);
			if (m_PhaseTimer <= 0f)
			{
				flag2 = true;
			}
		}
		if (m_MsgUsesTime)
		{
			SetMessage(m_MsgBank, m_MsgId, m_MsgUsesTime);
		}
		if (!base.isServer || !flag2 || m_CurrentPhase == Phase.Exiting)
		{
			return;
		}
		if (Singleton.Manager<ManNetwork>.inst.NetController.GameModeType == MultiplayerModeType.Deathmatch)
		{
			ServerEnterPhase(m_CurrentPhase + 1);
			return;
		}
		Phase phase = m_CurrentPhase + 1;
		if (phase == Phase.TechSelection)
		{
			phase = Phase.Playing;
		}
		ServerEnterPhase(phase);
	}

	private void SetRestrictionEnabled(Restriction restriction, bool enabled)
	{
		if (m_Restrictions.Contains((int)restriction) != enabled)
		{
			m_Restrictions.Set((int)restriction, enabled);
			switch (restriction)
			{
			case Restriction.ForceBuildBeamOn:
				NetTech.ForceBuildBeamEnabled = enabled;
				break;
			case Restriction.NoBuilding:
				Singleton.Manager<ManPointer>.inst.PreventInteraction(ManPointer.PreventChannel.Multiplayer, enabled);
				Singleton.Manager<ManPointer>.inst.PreventPainting(ManPointer.PreventChannel.Multiplayer, enabled);
				break;
			case Restriction.NoDamage:
				NetTech.ForceInvulnerable = enabled;
				break;
			default:
				d.AssertFormat(false, "NetController.SetRestrictionEnabled has invalid restriction {0}", restriction);
				break;
			}
		}
	}

	private void SetMessage(string bankId, string stringId, bool usesTime)
	{
		m_MsgBank = bankId;
		m_MsgId = stringId;
		m_MsgUsesTime = usesTime;
		if (!string.IsNullOrEmpty(bankId) && !string.IsNullOrEmpty(stringId))
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(bankId, stringId);
			if (usesTime)
			{
				int num = (int)Mathf.Ceil(m_PhaseTimer);
				string text = string.Format(localisedString, num);
				m_MultiplayerHud.Message1.SetText(text);
			}
			else
			{
				m_MultiplayerHud.Message1.SetText(localisedString);
			}
		}
		else
		{
			m_MultiplayerHud.Message1.Clear();
		}
	}

	private void SubscribeToScoreChanges()
	{
		int numPlayers = Singleton.Manager<ManNetwork>.inst.GetNumPlayers();
		for (int i = 0; i < numPlayers; i++)
		{
			Singleton.Manager<ManNetwork>.inst.GetPlayer(i).Score.OnChanged.Subscribe(OnScoreChanged);
		}
	}

	private void UnsubscribeToScoreChange()
	{
		int numPlayers = Singleton.Manager<ManNetwork>.inst.GetNumPlayers();
		for (int i = 0; i < numPlayers; i++)
		{
			Singleton.Manager<ManNetwork>.inst.GetPlayer(i).Score.OnChanged.Unsubscribe(OnScoreChanged);
		}
	}

	private void UpdateScores()
	{
		if (IsMultiTeamGame)
		{
			m_TeamScore.Clear();
		}
		int numPlayers = Singleton.Manager<ManNetwork>.inst.GetNumPlayers();
		for (int i = 0; i < numPlayers; i++)
		{
			NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
			_processKillStreakChanges(player);
			if (IsMultiTeamGame)
			{
				if (!m_TeamScore.ContainsKey(player.LobbyTeamID))
				{
					m_TeamScore.Add(player.LobbyTeamID, 0f);
				}
				m_TeamScore[player.LobbyTeamID] += player.Score.Evaluate(CurrentScorePolicy);
			}
		}
		m_nKillStreakTrackers = numPlayers;
		for (int j = 0; j < numPlayers; j++)
		{
			NetPlayer player2 = Singleton.Manager<ManNetwork>.inst.GetPlayer(j);
			int killStreak = player2.Score.KillStreak;
			uint value = player2.netId.Value;
			m_killStreakTrackers[j].m_netId = value;
			m_killStreakTrackers[j].m_killStreak = killStreak;
		}
	}

	private void _processKillStreakChanges(NetPlayer netPlayer)
	{
		int killStreak = netPlayer.Score.KillStreak;
		if (killStreak > 0)
		{
			int num = _findLastKillStreak(netPlayer);
			if (killStreak > num)
			{
				d.Log("Player: " + netPlayer.name + " has " + killStreak + " consecutive kills!  Killing Spree!");
				netPlayer.ResetDeathStreakRewards();
			}
		}
	}

	private int _findLastKillStreak(NetPlayer netPlayer)
	{
		uint value = netPlayer.netId.Value;
		for (int i = 0; i < m_nKillStreakTrackers; i++)
		{
			if (m_killStreakTrackers[i].m_netId == value)
			{
				return m_killStreakTrackers[i].m_killStreak;
			}
		}
		return 0;
	}

	private void SetPhaseTimer(float time)
	{
		if (m_PhaseTimer != time)
		{
			m_PhaseTimer = time;
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				SetDirtyBit(2u);
			}
		}
	}

	private void OnAllPlayersJoined()
	{
		if (base.isServer && m_CurrentPhase == Phase.GatherPlayers)
		{
			ServerEnterPhase(Phase.Intro);
		}
	}

	private void OnScoreChanged()
	{
		m_UpdateScores = true;
	}

	private void OnPlayerAdded(NetPlayer player)
	{
		if (!m_TeamColour.ContainsKey(player.LobbyTeamID))
		{
			m_TeamColour[player.LobbyTeamID] = player.Colour;
		}
	}

	private void OnSpawn()
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst != null);
		Singleton.Manager<ManNetwork>.inst.NetController = this;
		m_TeamColour.Clear();
		for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
		{
			NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
			d.Assert(player != null);
			OnPlayerAdded(player);
		}
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Subscribe(OnPlayerAdded);
		m_Options = Singleton.Manager<ManNetwork>.inst.Options;
		if (m_Options == null)
		{
			d.Log("Creating default net options");
			m_Options = new NetOptions();
			Singleton.Manager<ManNetwork>.inst.Options = m_Options;
		}
		m_MultiplayerHud = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.Multiplayer) as UIMultiplayerHUD;
		d.Assert(m_MultiplayerHud != null);
		m_MultiplayerHud.Show(null);
		m_NotableTech = NetworkInstanceId.Invalid;
		m_NotableBlock = NetworkInstanceId.Invalid;
		m_CachedNetTech = null;
		m_CachedNetBlock = null;
		m_CachedNetBlockID = -1;
		d.Assert(m_killStreakTrackers != null);
		m_nKillStreakTrackers = 0;
		d.Assert(m_killStreakTrackers.Length == 16);
		for (int j = 0; j < m_killStreakTrackers.Length; j++)
		{
			m_killStreakTrackers[j].reset();
		}
		d.Assert(m_TeamScore != null);
		m_TeamScore.Clear();
		Singleton.Manager<ManNetwork>.inst.OnAllExpectedPlayersJoined.Subscribe(OnAllPlayersJoined);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManNetwork>.inst.OnAllExpectedPlayersJoined.Unsubscribe(OnAllPlayersJoined);
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Unsubscribe(OnPlayerAdded);
		UnsubscribeToScoreChange();
		d.Assert(this == Singleton.Manager<ManNetwork>.inst.NetController);
		Singleton.Manager<ManNetwork>.inst.NetController = null;
		m_Options = null;
		m_CurrentPhase = Phase.GatherPlayers;
	}

	private void Update()
	{
		m_MultiplayerHud.Timer.Reset(m_ModeTimer);
		UpdatePhase();
		if (m_UpdateScores)
		{
			UpdateScores();
			m_UpdateScores = false;
		}
	}

	private void UNetVersion()
	{
	}
}
