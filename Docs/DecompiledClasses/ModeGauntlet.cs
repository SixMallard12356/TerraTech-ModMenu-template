#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using Snapshots;
using UnityEngine;
using UnityEngine.Events;

public class ModeGauntlet : Mode<ModeGauntlet>
{
	private enum eSaveVersionNumberHistory
	{
		SingleTrack,
		StartSupportMultipleTracks
	}

	private class ScoreFileContents
	{
		public int version;

		public string trackName;

		public string playerName;

		public string playerIdent;

		public float score;

		public byte[] replayData;
	}

	public class ReplayData
	{
		public GhostData m_PlayerGhost;

		public List<ManReplay.ReplayTimingData> m_OtherGhosts;

		public ReplayData(GhostData playerGhost, List<ManReplay.ReplayTimingData> otherGhosts)
		{
			m_PlayerGhost = playerGhost;
			m_OtherGhosts = otherGhosts;
		}
	}

	public enum GauntletModes
	{
		Normal,
		Replay,
		Attract
	}

	private enum EnterModeState
	{
		PrePlayerSpawn,
		WaitPlayerGrounded,
		PostPlayerGrounded
	}

	private enum ReplayState
	{
		Start,
		WaitingForCountdown,
		PlayingBack,
		Done
	}

	private enum RespawnStates
	{
		WaitBeforeFade,
		FadeToBlack,
		ClearFade
	}

	private enum AttractState
	{
		Start,
		FadeToBlack,
		SetupNextCamera,
		ClearFade,
		ShowCamera
	}

	[SerializeField]
	private BiomeMapStack m_BiomeMaps;

	[SerializeField]
	private GauntletLevelData m_GauntletLevelData;

	[SerializeField]
	private float m_HealPercentage = 0.75f;

	[SerializeField]
	private bool m_HealUpToPercentage = true;

	[Tooltip("Change this from -1 if overriding")]
	[SerializeField]
	private float m_OverrideGrabDistance = -1f;

	[SerializeField]
	private Transform m_uScriptToStart;

	[SerializeField]
	private Transform m_uScriptToStartTutorial;

	[SerializeField]
	private TankPreset m_TutorialPlayerPreset;

	[SerializeField]
	private int m_MaxHighScoreTableSizeDefault = 50;

	[SerializeField]
	private int m_MaxHighScoreTableSizeSwitch = 25;

	[SerializeField]
	private LocalisedString m_MsgScoreFirstPlace;

	[SerializeField]
	private LocalisedString m_MsgScoreNewRecord;

	[SerializeField]
	private LocalisedString m_MsgScoreImproved;

	[SerializeField]
	private LocalisedString m_MsgScoreNoPlace;

	[SerializeField]
	private TankDescriptionData m_GhostOverlayData;

	[SerializeField]
	private float m_TechSavedDisplayTime;

	[SerializeField]
	private float m_BouncingArrowShowDuration;

	[SerializeField]
	private PlayerStuckChecker m_PlayerStuckChecker;

	[SerializeField]
	private Transform m_AttractModeCameraPositions;

	[SerializeField]
	private float m_AttractFollowTechTime = 10f;

	[SerializeField]
	private float m_AttractStaticCamTime = 5f;

	[SerializeField]
	private float m_AttractSpawnInterval = 5f;

	[SerializeField]
	private int m_AttractMaxGhosts = 10;

	public const string kSettingsMode = "Mode";

	public const string kSettingsTrack = "TrackData";

	public const string kSaveFileExtension = ".scr";

	public const int kCurSaveVersionNumber = 1;

	public EventNoParams GauntletStartedEvent;

	public Event<bool> GauntletFinishedEvent;

	private const int kAttractMaxScoresToShow = 10;

	private const string kSaveDirectory = "Gauntlet";

	private int m_MaxHighScoreTableSize;

	private EnterModeState m_EnterModeState;

	private ReplayState m_ReplayState;

	private GauntletModes m_GauntletMode;

	private RespawnStates m_RespawnState;

	private TechData m_StartingTech;

	private TechData m_RebuiltTech;

	private Vector3 m_LastCheckpointPos;

	private int m_LastCheckpointControlSchemeID;

	private Quaternion m_LastCheckpointRot;

	private float m_RunningTime;

	private bool m_ChallengeStarted;

	private Vector3 m_PlayerLastPos;

	private bool m_FirstCheckpointTriggered;

	private bool m_TutorialActive;

	private bool m_Respawning;

	private Transform m_uScript;

	private Transform m_uScriptTutorial;

	private string m_CurrentPlayerName;

	private string m_FilePath;

	private string m_SessionStartTime;

	private float m_Timer;

	private float m_CountdownTimer = 1.5f;

	private List<Visible.WeakReference> m_BlocksToRecycle = new List<Visible.WeakReference>();

	private bool m_StopReplay;

	private ReplayData m_ReplayData;

	private string m_SelectedGhostIdent;

	private List<ManReplay.ReplayTimingData> m_ReplayList = new List<ManReplay.ReplayTimingData>();

	private string m_LoadedScoresTrack;

	private List<ScoreFileContents> m_LoadedScores;

	private float m_TechSavedTimer;

	private AttractState m_AttractState;

	private bool m_AttractShowingGhost;

	private int m_NextAttractGhostIndex;

	private int m_NextAttractCameraIndex;

	private float m_AttractModeTimer;

	private float m_AttractSpawnTimer;

	private GhostData[] m_AttractGhosts;

	private int m_AttractNumSpawnedGhosts;

	private bool m_PlayerTechStateDirty;

	private IInventory<BlockTypes> m_Inventory;

	public GauntletModes CurrentMode => m_GauntletMode;

	private string LocalIdent => m_CurrentPlayerName + "_" + m_SessionStartTime;

	public void StartTutorial()
	{
		if (m_GauntletMode == GauntletModes.Normal)
		{
			m_TutorialActive = true;
			RespawnTech(removeLastTech: true);
			Singleton.playerTank.beam.EnableBeam(enable: true, force: true);
			Mode<ModeMain>.inst.TutorialLockBeam = true;
			if ((bool)m_uScriptToStartTutorial)
			{
				m_uScriptTutorial = m_uScriptToStartTutorial.Spawn();
			}
			m_GauntletLevelData.TutorialSpawnList.SpawnAll();
		}
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.LaunchTutorialButton);
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.Snapshot);
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechLoaderButton);
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechLoader);
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ResetPosition);
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
	}

	public void ShowStartTutorial(bool enabled)
	{
		if (enabled)
		{
			UnityAction context = StartTutorial;
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.LaunchTutorialButton, context);
			ShowBuildPhaseButtons();
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.LaunchTutorialButton);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.Snapshot);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechLoaderButton);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.Speedo);
		}
	}

	public bool IsTutorialActive()
	{
		return m_TutorialActive;
	}

	public void EndTutorial()
	{
		ClearTutorial();
		ShowBuildPhaseButtons();
	}

	private void ShowBuildPhaseButtons()
	{
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Snapshot);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechLoaderButton);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Speedo);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
	}

	public void StartPlaying()
	{
		Singleton.Manager<ManGameMode>.inst.AddModeInitSetting("Mode", GauntletModes.Normal.ToString());
		Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeGauntlet>();
		m_ReplayList.Clear();
	}

	public void StartReplay()
	{
		Singleton.Manager<ManGameMode>.inst.AddModeInitSetting("Mode", GauntletModes.Replay.ToString());
		Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeGauntlet>();
	}

	public void StopReplay()
	{
		m_StopReplay = true;
	}

	public void StartAttract()
	{
		Singleton.Manager<ManGameMode>.inst.AddModeInitSetting("Mode", GauntletModes.Attract.ToString());
		Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeGauntlet>();
		ClearReplayData();
	}

	private void ClearReplayData()
	{
		m_ReplayData = null;
		m_CurrentPlayerName = null;
		m_StartingTech = null;
		m_ReplayList.Clear();
	}

	private void SaveTechState(Tank tech, ref TechData techDataToSaveInto)
	{
		if (tech == null)
		{
			d.LogError("ModeGauntlet.SaveTechState was passed a Null tech !");
			return;
		}
		if (techDataToSaveInto == null)
		{
			techDataToSaveInto = new TechData();
		}
		if (techDataToSaveInto.SaveTechIfChanged(tech, saveRuntimeState: true) && !m_TutorialActive)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechSavedMessage);
			m_TechSavedTimer = m_TechSavedDisplayTime;
		}
	}

	private void SetRespawnState(float waitTime)
	{
		m_RespawnState = RespawnStates.WaitBeforeFade;
		m_Respawning = true;
		m_Timer = waitTime;
	}

	private void RespawnTech(bool removeLastTech)
	{
		if (removeLastTech && (bool)Singleton.playerTank)
		{
			RemoveTech();
		}
		if (m_TutorialActive)
		{
			ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
			{
				techData = m_TutorialPlayerPreset.GetTechDataFormatted(),
				blockIDs = null,
				teamID = 0,
				position = m_GauntletLevelData.PlayerSpawn.position,
				rotation = m_GauntletLevelData.PlayerSpawn.orientation,
				grounded = true
			};
			Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: true);
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank);
		}
		else
		{
			ManSpawn.TankSpawnParams param2 = new ManSpawn.TankSpawnParams
			{
				techData = ((m_RebuiltTech != null) ? m_RebuiltTech : m_GauntletLevelData.PlayerPreset.GetTechDataFormatted()),
				blockIDs = null,
				teamID = 0,
				position = m_LastCheckpointPos,
				rotation = m_LastCheckpointRot,
				grounded = true
			};
			TrackedVisible trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnTankRef(param2, addToObjectManager: true);
			Tank tank2 = null;
			if (trackedVisible != null && trackedVisible.visible != null)
			{
				tank2 = trackedVisible.visible.tank;
			}
			else
			{
				d.LogError("ModeGauntlet - Failed to Respawn Player! How did this happen?");
			}
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank2);
			tank2.control.SetActiveSchemeFromID(m_LastCheckpointControlSchemeID, addIfMissing: true);
		}
		Vector3 vector = m_LastCheckpointRot * Vector3.forward * 15f;
		Vector3 vector2 = Vector3.up * 15f;
		Vector3 vector3 = m_LastCheckpointPos - vector + vector2;
		Quaternion rotation = Quaternion.LookRotation(m_LastCheckpointPos - vector3);
		Singleton.Manager<CameraManager>.inst.ResetCamera(vector3, rotation);
		if (!m_TutorialActive)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ResetPosition);
		}
	}

	private void LoadTechPreset(Snapshot capture)
	{
		TechData techData = capture.techData;
		if ((bool)Singleton.playerTank)
		{
			Vector3 position = m_GauntletLevelData.PlayerSpawn.position;
			Quaternion orientation = m_GauntletLevelData.PlayerSpawn.orientation;
			RemoveTech();
			ManSpawn.RemoveAllBlocksAndTechAroundPosition(m_GauntletLevelData.PlayerSpawn.position, 50f, 0);
			ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
			{
				techData = techData,
				blockIDs = null,
				teamID = 0,
				position = position,
				rotation = orientation,
				grounded = true
			};
			Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: true);
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank);
		}
		else
		{
			if (m_RebuiltTech == null)
			{
				m_RebuiltTech = new TechData();
			}
			m_RebuiltTech.CopyWithoutSaveData(techData);
		}
	}

	private void RemoveTech()
	{
		if ((bool)Singleton.playerTank)
		{
			Tank playerTank = Singleton.playerTank;
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(null);
			playerTank.visible.RemoveFromGame();
		}
	}

	private void RecycleBlocks()
	{
		for (int i = 0; i < m_BlocksToRecycle.Count; i++)
		{
			Visible visible = m_BlocksToRecycle[i].Get();
			if ((bool)visible)
			{
				visible.trans.Recycle();
			}
		}
		m_BlocksToRecycle.Clear();
	}

	private void ClearTutorial()
	{
		if ((bool)m_uScriptTutorial)
		{
			m_uScriptTutorial.Recycle();
			m_uScriptTutorial = null;
		}
		m_GauntletLevelData.TutorialSpawnList.RecycleAllPrefabs();
		m_TutorialActive = false;
	}

	private void LoadHighScoreTable(string trackName, Action callback)
	{
		if (trackName == m_LoadedScoresTrack && m_LoadedScores != null)
		{
			callback();
		}
		else if (SKU.ConsoleUI)
		{
			LoadHighScoreTableConsoles(trackName, callback);
		}
		else
		{
			LoadHighScoreTablePC(trackName, callback);
		}
	}

	private void LoadHighScoreTablePC(string trackName, Action callback)
	{
		List<ScoreFileContents> list = new List<ScoreFileContents>();
		string highscoresFolderPC = GetHighscoresFolderPC(trackName);
		if (Directory.Exists(highscoresFolderPC))
		{
			string[] files = Directory.GetFiles(highscoresFolderPC, "*.scr");
			for (int i = 0; i < files.Length; i++)
			{
				ScoreFileContents scoreFileContents = LoadScoreFilePC(files[i]);
				if (scoreFileContents != null && scoreFileContents.trackName == trackName)
				{
					list.Add(scoreFileContents);
				}
			}
			list.Sort((ScoreFileContents e1, ScoreFileContents e2) => e1.score.CompareTo(e2.score));
			if (list.Count > m_MaxHighScoreTableSize)
			{
				list.RemoveRange(m_MaxHighScoreTableSize, list.Count - m_MaxHighScoreTableSize);
			}
		}
		else
		{
			d.LogError("ModeGauntlet - Unable to read scores: Can't find path: " + highscoresFolderPC);
		}
		m_LoadedScoresTrack = trackName;
		m_LoadedScores = list;
		callback();
	}

	private void LoadHighScoreTableConsoles(string trackName, Action callback)
	{
		d.Log("[ModeGauntlet] Load score table for the track " + trackName);
		if (SaveDataConsoles.DataExists(trackName, "Gauntlet"))
		{
			SaveDataConsoles.LoadData(trackName, "Gauntlet", delegate(bool success, byte[] result)
			{
				Dictionary<string, ScoreFileContents> objectToLoad = new Dictionary<string, ScoreFileContents>();
				if (result != null)
				{
					ManSaveGame.LoadObjectFromBytes(ref objectToLoad, result);
				}
				d.Log("[ModeGauntlet] scrore table loaded, number of entries: " + objectToLoad.Count);
				BuildScoreListConsoles(trackName, objectToLoad);
				callback();
			});
		}
		else
		{
			d.Log("[ModeGauntlet] score table failed to load");
			BuildScoreListConsoles(trackName, new Dictionary<string, ScoreFileContents>());
			callback();
		}
	}

	private void BuildScoreListConsoles(string trackName, Dictionary<string, ScoreFileContents> scoreTable)
	{
		List<ScoreFileContents> list = new List<ScoreFileContents>();
		foreach (KeyValuePair<string, ScoreFileContents> item in scoreTable)
		{
			list.Add(item.Value);
		}
		list.Sort((ScoreFileContents e1, ScoreFileContents e2) => e1.score.CompareTo(e2.score));
		if (list.Count > m_MaxHighScoreTableSize)
		{
			list.RemoveRange(m_MaxHighScoreTableSize, list.Count - m_MaxHighScoreTableSize);
		}
		m_LoadedScoresTrack = trackName;
		m_LoadedScores = list;
	}

	private void ConfigureAttractModeLeaderboard(UILeaderboard leaderboard)
	{
		if (!leaderboard)
		{
			return;
		}
		LoadHighScoreTable(m_GauntletLevelData.TrackName, delegate
		{
			int num = Math.Min((m_LoadedScores != null) ? m_LoadedScores.Count : 0, 10);
			if (num > 0)
			{
				List<UILeaderboard.ScoreEntry> list = new List<UILeaderboard.ScoreEntry>();
				for (int i = 0; i < num; i++)
				{
					list.Add(new UILeaderboard.ScoreEntry
					{
						m_PlayerName = m_LoadedScores[i].playerName,
						m_Score = m_LoadedScores[i].score
					});
				}
				leaderboard.Setup(list, "Time");
				leaderboard.gameObject.SetActive(value: true);
			}
			else
			{
				leaderboard.gameObject.SetActive(value: false);
			}
		});
	}

	private void AddToLeaderboard(string trackName, ReplayData replayData, float score, Action callback)
	{
		d.Log("AddToLeaderboard trackName: " + trackName + ", score: " + score);
		int initialCapacitySizeHint = 262144;
		ScoreFileContents scoreFileContents = new ScoreFileContents
		{
			version = 0,
			trackName = m_GauntletLevelData.TrackName,
			playerName = m_CurrentPlayerName,
			playerIdent = LocalIdent,
			score = score
		};
		if (replayData != null)
		{
			scoreFileContents.replayData = ManSaveGame.SaveObjectToBytes(replayData, initialCapacitySizeHint);
		}
		if (SKU.ConsoleUI)
		{
			SaveScoreFileConsoles(LocalIdent + "_" + score, trackName, scoreFileContents, callback);
			return;
		}
		SaveScoreFilePC(trackName, scoreFileContents);
		RemoveScoreFilesNotInLeaderboardPC(trackName);
		callback();
	}

	private void RemoveScoreFilesNotInLeaderboardPC(string trackName)
	{
		LoadHighScoreTablePC(trackName, delegate
		{
			if (m_LoadedScores != null && m_LoadedScores.Count > 0)
			{
				float score = m_LoadedScores[m_LoadedScores.Count - 1].score;
				string highscoresFolderPC = GetHighscoresFolderPC(trackName);
				if (Directory.Exists(highscoresFolderPC))
				{
					string[] files = Directory.GetFiles(highscoresFolderPC, "*.scr");
					for (int i = 0; i < files.Length; i++)
					{
						ScoreFileContents scoreFileContents = LoadScoreFilePC(files[i]);
						if (scoreFileContents != null && scoreFileContents.score > score)
						{
							File.Delete(files[i]);
						}
					}
				}
			}
		});
	}

	private void ShowLeaderboard(bool hasNewlyAddedEntry, float newlyAddedScore)
	{
		string localIdent = LocalIdent;
		bool flag = false;
		int num = 0;
		bool flag2 = false;
		int num2 = 0;
		for (int i = 0; i < m_LoadedScores.Count; i++)
		{
			if (localIdent == m_LoadedScores[i].playerIdent)
			{
				flag = true;
				num = i;
			}
			if (!m_SelectedGhostIdent.NullOrEmpty() && m_SelectedGhostIdent == m_LoadedScores[i].playerIdent)
			{
				flag2 = true;
				num2 = i;
			}
		}
		if (flag2)
		{
			ReplayData replayData = LoadReplayFromLeaderboardEntry(m_LoadedScores[num2]);
			if (replayData != null)
			{
				m_ReplayData = replayData;
			}
		}
		UIScreenLeaderboard uIScreenLeaderboard = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.LeaderBoard) as UIScreenLeaderboard;
		List<UILeaderboard.ScoreEntry> list = new List<UILeaderboard.ScoreEntry>();
		for (int j = 0; j < m_LoadedScores.Count; j++)
		{
			list.Add(new UILeaderboard.ScoreEntry
			{
				m_PlayerName = m_LoadedScores[j].playerName,
				m_Score = m_LoadedScores[j].score
			});
		}
		uIScreenLeaderboard.Setup(list, "Time");
		if (flag)
		{
			uIScreenLeaderboard.SetHighlightedIndex(num);
		}
		else
		{
			uIScreenLeaderboard.SetHighlightedIndex(-1);
		}
		if (flag2)
		{
			uIScreenLeaderboard.SetGhostedIndex(num2);
		}
		if (hasNewlyAddedEntry)
		{
			string message = "";
			if (flag && m_LoadedScores[num].score == newlyAddedScore)
			{
				message = ((num != 0) ? m_MsgScoreNewRecord.Value : m_MsgScoreFirstPlace.Value);
			}
			else if (m_MsgScoreNoPlace.Value != null)
			{
				TimeSpan timeSpan = TimeSpan.FromSeconds(newlyAddedScore);
				string arg = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00") + ":" + timeSpan.Milliseconds.ToString("000");
				message = string.Format(m_MsgScoreNoPlace.Value, arg);
			}
			uIScreenLeaderboard.SetMessage(message);
		}
		uIScreenLeaderboard.SetButtonActions(delegate
		{
			StartPlaying();
		}, delegate
		{
			ExitPlaying();
		});
		Singleton.Manager<ManUI>.inst.PushScreen(uIScreenLeaderboard);
	}

	private void RegisterCallbacks()
	{
		if (m_GauntletMode == GauntletModes.Normal)
		{
			CheckpointChallenge.OnBoundsWarning.Subscribe(OnBoundsWarning);
			CheckpointChallenge.OnCheckpointPassed.Subscribe(OnCheckpointPassed);
			Singleton.Manager<ManChallenge>.inst.OnChallengeEnded.Subscribe(OnChallengeEnded);
			Singleton.Manager<ManPointer>.inst.DragEvent.Subscribe(OnDragEvent);
			Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Subscribe(OnBlockAttached);
			Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Subscribe(OnBlockDetached);
			Singleton.Manager<ManCustomSkins>.inst.TankBlockPaintedEvent.Subscribe(OnBlockPainted);
			UISchemaMenu.ControlSchemesEditedEvent.Subscribe(OnTechControlSchemesEdited);
			Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankSwitched);
		}
	}

	private void StartGauntlet()
	{
		if (m_GauntletMode == GauntletModes.Normal)
		{
			if ((bool)m_uScriptToStart)
			{
				m_uScript = m_uScriptToStart.Spawn();
			}
			m_PlayerStuckChecker.Reset();
			m_PlayerTechStateDirty = false;
		}
	}

	private void ResetMode()
	{
		Tank playerTank = Singleton.playerTank;
		Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(null);
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = playerTank.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			Visible.WeakReference weakReference = new Visible.WeakReference();
			weakReference.Set(current.visible);
			m_BlocksToRecycle.Add(weakReference);
		}
		playerTank.blockman.Disintegrate();
		GamepadVibration.VibratePad(GamepadVibration.Type.PlayerTankDestroyed, hasPriority: true);
		SetRespawnState(2f);
	}

	private void OnCheckpointPassed(int checkpoint, Vector3 position, Quaternion rotation)
	{
		if ((bool)Singleton.playerTank)
		{
			BlockManager.BlockIterator<ModuleDamage>.Enumerator enumerator = Singleton.playerTank.blockman.IterateBlockComponents<ModuleDamage>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				ModuleDamage current = enumerator.Current;
				Damageable damageable = current.block.visible.damageable;
				float num = damageable.MaxHealth * m_HealPercentage;
				if (m_HealUpToPercentage)
				{
					if (damageable.Health < num)
					{
						damageable.Repair(num - damageable.Health);
					}
				}
				else
				{
					damageable.Repair(num);
				}
				current.ResetDetachMeter();
			}
			if (checkpoint == 0)
			{
				SaveTechState(Singleton.playerTank, ref m_StartingTech);
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.LaunchTutorialButton);
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.Snapshot);
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechLoaderButton);
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechLoader);
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
				ClearTutorial();
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ResetPosition);
				m_FirstCheckpointTriggered = true;
				GauntletStartedEvent.Send();
			}
			m_LastCheckpointControlSchemeID = Singleton.playerTank.control.GetActiveSchemeID();
		}
		else
		{
			d.LogError("ModeGauntlet - Checkpoint with null Singleton.playerTank");
		}
		m_LastCheckpointPos = position;
		m_LastCheckpointRot = rotation;
	}

	private void OnChallengeEnded(Challenge.ChallengeEndData challengeEndData)
	{
		CheckpointChallenge.CheckpointChallengeEndData checkpointChallengeEndData = challengeEndData as CheckpointChallenge.CheckpointChallengeEndData;
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ResetPosition);
		GhostData playerGhost = Singleton.Manager<ManReplay>.inst.StopAndGetGhostData();
		bool flag = Singleton.Manager<ManReplay>.inst.HasValidRun();
		Singleton.Manager<ManReplay>.inst.StopAllRecording();
		ReplayData replayData = (flag ? new ReplayData(playerGhost, m_ReplayList) : null);
		if (checkpointChallengeEndData != null)
		{
			d.Log($"[ModeGauntlet] OnChallengeEnded endReason = {checkpointChallengeEndData.endReason}");
			if (checkpointChallengeEndData.endReason == CheckpointChallenge.EndReason.Success)
			{
				bool hasNewRecord = checkpointChallengeEndData.hasNewRecord;
				float score = checkpointChallengeEndData.latestTime;
				if ((m_ReplayData == null || (hasNewRecord && !m_SelectedGhostIdent.NullOrEmpty())) && replayData != null)
				{
					m_ReplayData = replayData;
				}
				bool flag2 = false;
				if (m_LoadedScores == null)
				{
					d.LogError("Unexpected null m_LoadedScores on finishing a Gauntlet track");
					flag2 = true;
				}
				else if (m_LoadedScores.Count < m_MaxHighScoreTableSize)
				{
					flag2 = true;
				}
				else
				{
					float score2 = m_LoadedScores[m_LoadedScores.Count - 1].score;
					flag2 = score < score2;
				}
				if (flag2)
				{
					Action afterSaveComplete = delegate
					{
						d.Log("[ModeGauntlet] show leaderboard");
						ShowLeaderboard(hasNewlyAddedEntry: true, score);
					};
					if (VirtualKeyboard.IsRequired())
					{
						string keyboardDesc = CheckpointChallenge.ConvertTimeToScoreString(score);
						string defaultName = (m_CurrentPlayerName.NullOrEmpty() ? Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_Name : m_CurrentPlayerName);
						VirtualKeyboard.EntryCompleteDelegate onCompleteHandler = delegate(bool accepted, string result)
						{
							d.Log("[ModeGauntlet] OpenVirtualKeyboard - accepted = " + accepted + " result = " + ((result == null) ? "NULL" : result));
							if (accepted && !string.IsNullOrEmpty(result))
							{
								m_CurrentPlayerName = result;
							}
							else
							{
								m_CurrentPlayerName = defaultName;
							}
							AddToLeaderboard(m_GauntletLevelData.TrackName, replayData, score, afterSaveComplete);
						};
						VirtualKeyboard.PromptInput(string.Empty, keyboardDesc, defaultName, onCompleteHandler);
					}
					else
					{
						UIScreenEnterName uIScreenEnterName = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.EnterName) as UIScreenEnterName;
						uIScreenEnterName.ScoreText = CheckpointChallenge.ConvertTimeToScoreString(score);
						uIScreenEnterName.SetConfirmAction(delegate(string x)
						{
							Singleton.Manager<ManUI>.inst.PopScreen(showPrev: false);
							m_CurrentPlayerName = x;
							AddToLeaderboard(m_GauntletLevelData.TrackName, replayData, score, afterSaveComplete);
						});
						Singleton.Manager<ManUI>.inst.PushScreen(uIScreenEnterName);
					}
				}
				else
				{
					ShowLeaderboard(hasNewlyAddedEntry: true, score);
				}
			}
		}
		GauntletFinishedEvent.Send(checkpointChallengeEndData.endReason == CheckpointChallenge.EndReason.Success);
	}

	private void OnBoundsWarning(CheckpointChallenge.BoundsArea area)
	{
		if (area == CheckpointChallenge.BoundsArea.Illegal && !m_Respawning && (bool)Singleton.playerTank)
		{
			ResetMode();
		}
	}

	private void OnBlockPainted(Tank tank, TankBlock block)
	{
		if ((bool)Singleton.playerTank && tank == Singleton.playerTank)
		{
			m_PlayerTechStateDirty = true;
		}
	}

	private void OnBlockAttached(Tank tank, TankBlock block)
	{
		if ((bool)Singleton.playerTank && tank == Singleton.playerTank)
		{
			m_PlayerTechStateDirty = true;
		}
	}

	private void OnBlockDetached(Tank tank, TankBlock block)
	{
		if ((bool)Singleton.playerTank && tank == Singleton.playerTank && tank.blockman.PlayerInitiatedRemoveBlock)
		{
			m_PlayerTechStateDirty = true;
		}
	}

	private void OnTechControlSchemesEdited(Tank tank)
	{
		if (tank == Singleton.playerTank)
		{
			m_LastCheckpointControlSchemeID = tank.control.GetActiveSchemeID();
			m_PlayerTechStateDirty = true;
		}
	}

	private void OnPlayerTankSwitched(Tank theTank, bool set)
	{
		if (set && (bool)theTank)
		{
			if (theTank.blockman.blockCount > 0)
			{
				SaveTechState(theTank, ref m_RebuiltTech);
			}
			m_PlayerStuckChecker.Reset();
			m_PlayerTechStateDirty = false;
		}
	}

	private void OnLeaderboardReplayClicked(int leaderboardIndex)
	{
		d.Assert(m_LoadedScores != null, "No leaderboard scores loaded - cannot begin replay");
		if (m_LoadedScores != null && m_LoadedScores.Count > leaderboardIndex)
		{
			ReplayData replayData = LoadReplayFromLeaderboardEntry(m_LoadedScores[leaderboardIndex]);
			if (replayData != null && !DebugUtil.ModelPrintingEnabled)
			{
				m_SelectedGhostIdent = m_LoadedScores[leaderboardIndex].playerIdent;
				m_ReplayData = replayData;
				StartReplay();
			}
		}
	}

	private void OnTechPostSpawned(Tank tech)
	{
		if (!(tech != Singleton.playerTank))
		{
			return;
		}
		bool flag = false;
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (enumerator.Current.visible.ItemType == 10)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			return;
		}
		float currentTime = m_RunningTime;
		Action<GhostData> recordingFinishedCallback = delegate(GhostData x)
		{
			if (Singleton.Manager<ManGameMode>.inst.GetModePhase() == ManGameMode.GameState.InGame)
			{
				ManReplay.ReplayTimingData item = new ManReplay.ReplayTimingData
				{
					m_GhostData = x,
					m_StartTime = currentTime
				};
				m_ReplayList.Add(item);
			}
		};
		Singleton.Manager<ManReplay>.inst.RecordGhost(tech, recordingFinishedCallback);
	}

	private void OnDragEvent(Visible visible, ManPointer.DragAction dragAction, Vector3 screenPos)
	{
		if (!m_ChallengeStarted && dragAction == ManPointer.DragAction.ReleaseAllowPlace && Singleton.Manager<ManTechBuilder>.inst.DraggingPlayerCab)
		{
			Vector3 vector = Singleton.playerTank.trans.position - m_GauntletLevelData.PlayerSpawn.position;
			if (Mathf.Pow(vector.x, 2f) + Mathf.Pow(vector.z, 2f) > Mathf.Pow(m_GauntletLevelData.MaxDragFromSpawnDistance, 2f))
			{
				Singleton.Manager<ManDamage>.inst.DealDamage(visible.damageable, visible.damageable.Health, ManDamage.DamageType.Standard, this);
				ResetMode();
			}
		}
	}

	protected override void SetupModeLoadSaveListeners()
	{
		base.SetupModeLoadSaveListeners();
		SubscribeToEvents(Singleton.Manager<ManChallenge>.inst);
	}

	protected override void CleanupModeLoadSaveListeners()
	{
		base.CleanupModeLoadSaveListeners();
		UnsubscribeFromEvents(Singleton.Manager<ManChallenge>.inst);
	}

	protected override void ConfigureExitConfirmMenu(UIScreenNotifications exitScreen)
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuPause, 0);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
			m_Callback = delegate
			{
				ManSaveGame.ShouldStore = false;
				Singleton.Manager<ManUI>.inst.ExitAllScreens();
				ExitPlaying();
			},
			m_RewiredAction = 21
		};
		UIButtonData decline = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30),
			m_Callback = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			},
			m_RewiredAction = 22
		};
		exitScreen.Set(localisedString, accept, decline);
	}

	protected override void EnterGenerateTerrain(InitSettings initSettings)
	{
		if (initSettings.TryGetValue("Mode", out var value))
		{
			string text = (string)value;
			if (text != null && Enum.IsDefined(typeof(GauntletModes), text))
			{
				m_GauntletMode = (GauntletModes)Enum.Parse(typeof(GauntletModes), text);
			}
			else
			{
				if (text != null)
				{
					d.LogErrorFormat("Gauntlet mode init setting - Value of setting {0} ('{1}') does not correspond to a valid GauntletMode type!", "Mode", text);
				}
				else
				{
					d.LogErrorFormat("Gauntlet mode init setting - Setting '{0}' could not be converted to a valid string..", "Mode");
				}
				m_GauntletMode = GauntletModes.Attract;
			}
		}
		else
		{
			m_GauntletMode = GauntletModes.Attract;
		}
		if (SKU.SwitchUI)
		{
			m_MaxHighScoreTableSize = m_MaxHighScoreTableSizeSwitch;
		}
		else
		{
			m_MaxHighScoreTableSize = m_MaxHighScoreTableSizeDefault;
		}
		if (initSettings.TryGetValue("TrackData", out var value2))
		{
			GameObject obj = ((UnityEngine.Object)value2) as GameObject;
			d.Assert(obj != null, "ModeGauntlet - Found 'TrackData' Setting, but it is not set to a valid GameObject!");
			GauntletLevelData component = obj.GetComponent<GauntletLevelData>();
			d.Assert(component != null, "ModeGauntlet - Found 'TrackData' Setting with a GameObject, but could not find GauntletLevelData component!");
			if (component != null)
			{
				m_GauntletLevelData = component;
				m_Inventory = new SingleplayerInventory();
				m_GauntletLevelData.CheckpointChallengeData.BuildInventory(m_Inventory);
			}
		}
		d.Assert(m_GauntletLevelData != null, "Attempting to start gauntlet without GauntletLevelData set! (Did you forget to add 'TrackData' to Mode Init settings?");
		if (m_FilePath.NullOrEmpty() && !SKU.ConsoleUI)
		{
			m_FilePath = ManSaveGame.GetSaveDataFolder() + "/Saves/Gauntlet/";
			Directory.CreateDirectory(m_FilePath);
		}
		switch (m_GauntletMode)
		{
		case GauntletModes.Attract:
		{
			UIScreenGauntletAttract uIScreenGauntletAttract = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.GauntletAttract) as UIScreenGauntletAttract;
			d.Assert(uIScreenGauntletAttract != null, "ModeGauntlet:EnterGenerateTerrain - don't have a valid attract menu");
			if ((bool)uIScreenGauntletAttract)
			{
				uIScreenGauntletAttract.BlockScreenExit(exitBlocked: true);
				ConfigureAttractModeLeaderboard(uIScreenGauntletAttract.Leaderboard);
				Singleton.Manager<ManUI>.inst.PushScreen(uIScreenGauntletAttract);
			}
			break;
		}
		case GauntletModes.Replay:
			Singleton.Manager<ManPauseGame>.inst.PushPauseMenu(ManUI.ScreenType.GauntletReplay, pauseGameplay: false, null);
			break;
		}
		m_SessionStartTime = DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00");
		PrototypeEnemySpawner[] array = UnityEngine.Object.FindObjectsOfType<PrototypeEnemySpawner>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Reset();
		}
		Vector3 position;
		Quaternion cameraRot;
		if (m_GauntletMode != GauntletModes.Attract || m_AttractModeCameraPositions == null || m_AttractModeCameraPositions.childCount == 0)
		{
			position = m_GauntletLevelData.CameraSpawn.position;
			cameraRot = m_GauntletLevelData.CameraSpawn.orientation;
		}
		else
		{
			Transform child = m_AttractModeCameraPositions.GetChild(0);
			position = child.position;
			cameraRot = child.rotation;
		}
		Singleton.Manager<ManWorld>.inst.SeedString = m_GauntletLevelData.SeedString;
		if (m_GauntletLevelData.OverrideTerrainGenVersion != -1)
		{
			Singleton.Manager<ManSaveGame>.inst.CurrentState.m_WorldGenVersion = m_GauntletLevelData.OverrideTerrainGenVersion;
			Singleton.Manager<ManSaveGame>.inst.CurrentState.m_WorldGenVersioningType = m_GauntletLevelData.OverrideTerrainGenVersioningType;
		}
		BiomeMap biomeMap = m_BiomeMaps.SelectCompatibleBiomeMap();
		Singleton.Manager<ManGameMode>.inst.RegenerateWorld(biomeMap, position, cameraRot);
		Singleton.Manager<ManWorld>.inst.TileManager.SetFixedTilesLoaded(m_GauntletLevelData.FixedTilesLoaded);
		Singleton.Manager<ManWorld>.inst.TileManager.SetFixedTilesUnpopulated(m_GauntletLevelData.FixedTilesUnpopulated);
	}

	protected override void EnterPreModeImpl(InitSettings initSettings)
	{
		m_EnterModeState = EnterModeState.PrePlayerSpawn;
	}

	protected override FunctionStatus UpdatePreModeImpl(InitSettings initSettings)
	{
		FunctionStatus result = FunctionStatus.Running;
		Singleton.Manager<ManSFX>.inst.SuppressUISFX();
		switch (m_EnterModeState)
		{
		case EnterModeState.PrePlayerSpawn:
		{
			Singleton.Manager<ManTimeOfDay>.inst.EnableSkyDome(enable: true);
			Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(enable: false);
			Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(11, 0, 0);
			EnterDefaultCameraMode();
			m_GauntletLevelData.SpawnList.SpawnAll();
			Singleton.Manager<ManSpawn>.inst.BlockLimit = BlockManager.DefaultBlockLimit;
			UIScreenLeaderboard uIScreenLeaderboard = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.LeaderBoard) as UIScreenLeaderboard;
			if ((bool)uIScreenLeaderboard)
			{
				uIScreenLeaderboard.Leaderboard.ReplayButtonClicked.Subscribe(OnLeaderboardReplayClicked);
			}
			switch (m_GauntletMode)
			{
			case GauntletModes.Normal:
			{
				RegisterCallbacks();
				TechData techData = ((m_StartingTech != null) ? m_StartingTech : m_GauntletLevelData.PlayerPreset.GetTechDataFormatted());
				if (techData != null)
				{
					ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
					{
						techData = techData,
						blockIDs = null,
						teamID = 0,
						position = m_GauntletLevelData.PlayerSpawn.position,
						rotation = m_GauntletLevelData.PlayerSpawn.orientation,
						grounded = true
					};
					Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: true);
					Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank);
				}
				m_EnterModeState = EnterModeState.WaitPlayerGrounded;
				(Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechLoader) as ITechLoader).SetupScreenHandlers(LoadTechPreset);
				IInventory<BlockTypes> inventory = new SingleplayerInventory();
				m_GauntletLevelData.CheckpointChallengeData.BuildInventory(inventory);
				Singleton.Manager<ManPurchases>.inst.SetInventory(inventory);
				break;
			}
			case GauntletModes.Replay:
			case GauntletModes.Attract:
				m_EnterModeState = EnterModeState.PostPlayerGrounded;
				break;
			}
			if (m_GauntletMode != GauntletModes.Attract)
			{
				break;
			}
			LoadHighScoreTable(m_GauntletLevelData.TrackName, delegate
			{
				int num = Math.Min((m_LoadedScores != null) ? m_LoadedScores.Count : 0, m_AttractMaxGhosts);
				m_AttractGhosts = new GhostData[num];
				for (int i = 0; i < num; i++)
				{
					ReplayData replayData = LoadReplayFromLeaderboardEntry(m_LoadedScores[i]);
					if (replayData != null)
					{
						m_AttractGhosts[i] = replayData.m_PlayerGhost;
					}
				}
			});
			break;
		}
		case EnterModeState.WaitPlayerGrounded:
			if (!m_GauntletLevelData.PlayerPreset || ((bool)Singleton.playerTank && Singleton.playerTank.grounded))
			{
				m_EnterModeState = EnterModeState.PostPlayerGrounded;
			}
			break;
		case EnterModeState.PostPlayerGrounded:
		{
			Vector3 position = m_GauntletLevelData.PlayerSpawn.position;
			Vector3 position2 = m_GauntletLevelData.CameraSpawn.position;
			Quaternion rotation = Quaternion.LookRotation(position - position2);
			Singleton.Manager<CameraManager>.inst.ResetCamera(position2, rotation);
			if (m_OverrideGrabDistance != -1f)
			{
				Singleton.Manager<ManPointer>.inst.SetPickupRange(m_OverrideGrabDistance);
			}
			CheckpointChallengeData checkpointChallengeData = m_GauntletLevelData.CheckpointChallengeData;
			if ((bool)checkpointChallengeData)
			{
				m_LastCheckpointPos = position;
				m_LastCheckpointControlSchemeID = 0;
				TrackSpline spline = checkpointChallengeData.Track.spline;
				Vector3 scenePos;
				Quaternion spawnRotation;
				if ((bool)spline)
				{
					Vector3 vector = spline.CalcNodePosSlow(TrackSpline.CurveType.Reference, 0);
					Vector3 forward = spline.CalcNodeTangentSlow(0);
					Matrix4x4 matrix4x = Matrix4x4.TRS(m_GauntletLevelData.ChallengeSpawn.position, Quaternion.identity, Vector3.one) * Matrix4x4.TRS(Vector3.zero, m_GauntletLevelData.ChallengeSpawn.orientation, Vector3.one) * Matrix4x4.TRS(Vector3.zero, Quaternion.Inverse(Quaternion.LookRotation(forward)), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
					scenePos = matrix4x.MultiplyPoint(Vector3.zero);
					spawnRotation = Quaternion.LookRotation(matrix4x.MultiplyVector(Vector3.forward), Vector3.up);
				}
				else
				{
					d.LogWarning("ModeGauntlet expected the track to have a spline - is not setting up starting transform properly");
					scenePos = Vector3.zero;
					spawnRotation = Quaternion.identity;
				}
				ManChallenge.InitParams initParams = new ManChallenge.InitParams();
				initParams.data = checkpointChallengeData;
				initParams.placementInfo = new Challenge.PlacementInfo
				{
					spawnPosition = WorldPosition.FromScenePosition(in scenePos),
					spawnRotation = spawnRotation,
					yPositionsRelativeToGround = false,
					ShowHUD = (m_GauntletMode != GauntletModes.Attract),
					ReplayMode = (m_GauntletMode == GauntletModes.Replay)
				};
				initParams.endChallengeWhenPlayerDies = false;
				initParams.exitOnOutOfBounds = false;
				initParams.buildModeType = ((m_GauntletMode == GauntletModes.Normal) ? ManChallenge.BuildModeType.Simultaneous : ManChallenge.BuildModeType.None);
				initParams.displaysPauseMenu = false;
				Singleton.Manager<ManChallenge>.inst.SetupChallenge(initParams);
				if (m_LoadedScores == null)
				{
					LoadHighScoreTable(m_GauntletLevelData.TrackName, SetBestScoreOnHUD);
				}
				else
				{
					SetBestScoreOnHUD();
				}
			}
			result = FunctionStatus.Done;
			break;
		}
		default:
			d.Assert(condition: false, "Invalid enter mode state " + m_EnterModeState);
			break;
		}
		return result;
	}

	private void SetBestScoreOnHUD()
	{
		if (m_LoadedScores != null && m_LoadedScores.Count > 0)
		{
			UICheckpointChallengeHUD uICheckpointChallengeHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.CheckpointChallenge) as UICheckpointChallengeHUD;
			if (uICheckpointChallengeHUD != null)
			{
				float score = m_LoadedScores[0].score;
				uICheckpointChallengeHUD.SetBestTimeText(CheckpointChallenge.ConvertTimeToScoreString(score));
			}
		}
	}

	protected override void EnterModeUpdateImpl()
	{
		switch (m_GauntletMode)
		{
		case GauntletModes.Normal:
			m_ReplayList.Clear();
			StartGauntlet();
			Singleton.Manager<ManTechs>.inst.TankPostSpawnEvent.Subscribe(OnTechPostSpawned);
			m_ChallengeStarted = false;
			break;
		case GauntletModes.Replay:
			m_ReplayState = ReplayState.Start;
			break;
		case GauntletModes.Attract:
			m_AttractState = AttractState.Start;
			break;
		}
		m_AttractState = AttractState.Start;
	}

	protected override FunctionStatus UpdateModeImpl()
	{
		switch (m_GauntletMode)
		{
		case GauntletModes.Normal:
			if ((bool)Singleton.playerTank)
			{
				m_PlayerLastPos = Singleton.playerPos;
			}
			if (m_FirstCheckpointTriggered)
			{
				Singleton.Manager<ManReplay>.inst.RecordTech();
				if (m_ReplayData != null)
				{
					bool ghostEffect = true;
					Singleton.Manager<ManReplay>.inst.ReplayGhost(m_ReplayData.m_PlayerGhost, ghostEffect);
				}
				m_ChallengeStarted = true;
				m_FirstCheckpointTriggered = false;
			}
			else
			{
				if (m_ChallengeStarted)
				{
					m_RunningTime += Time.deltaTime;
				}
				if (!m_Respawning && !Singleton.Manager<ManUndo>.inst.UndoInProgress && Singleton.playerTank == null && !Singleton.Manager<ManPointer>.inst.IsDraggingController)
				{
					SetRespawnState(2f);
				}
				if (m_Respawning)
				{
					switch (m_RespawnState)
					{
					case RespawnStates.WaitBeforeFade:
						if (m_Timer <= 0f)
						{
							bool forceFront = false;
							Singleton.Manager<ManUI>.inst.FadeToBlack(1f, forceFront);
							m_RespawnState = RespawnStates.FadeToBlack;
						}
						else
						{
							m_Timer -= Time.deltaTime;
						}
						break;
					case RespawnStates.FadeToBlack:
						if (Singleton.Manager<ManUI>.inst.FadeFinished())
						{
							RecycleBlocks();
							ManSpawn.RemoveAllBlocksAndTechAroundPosition(m_PlayerLastPos, 50f, 0);
							RespawnTech(removeLastTech: false);
							PrototypeEnemySpawner[] array = UnityEngine.Object.FindObjectsOfType<PrototypeEnemySpawner>();
							for (int num2 = 0; num2 < array.Length; num2++)
							{
								array[num2].RespawnEnemyIfActive();
							}
							Singleton.Manager<ManUI>.inst.ClearFade(1f);
							m_RespawnState = RespawnStates.ClearFade;
						}
						break;
					case RespawnStates.ClearFade:
						if (Singleton.Manager<ManUI>.inst.FadeFinished())
						{
							m_Respawning = false;
						}
						break;
					}
				}
				m_PlayerStuckChecker.Update();
				if (m_PlayerStuckChecker.Stuck && Singleton.Manager<ManChallenge>.inst.IsChallengeRunning)
				{
					UIBouncingArrow.BouncingArrowContext bouncingArrowContext = new UIBouncingArrow.BouncingArrowContext
					{
						targetTransform = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.ResetPosition).transform,
						forTime = m_BouncingArrowShowDuration
					};
					Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.BouncingArrow, bouncingArrowContext);
				}
			}
			UpdateMissionScoreboardButtonPress();
			break;
		case GauntletModes.Replay:
		{
			bool num = m_ReplayState == ReplayState.Done;
			UpdateReplay();
			if (!num && m_ReplayState == ReplayState.Done)
			{
				LoadHighScoreTable(m_GauntletLevelData.TrackName, delegate
				{
					ShowLeaderboard(hasNewlyAddedEntry: false, 0f);
				});
			}
			break;
		}
		case GauntletModes.Attract:
			UpdateAttractMode();
			break;
		}
		if (m_PlayerTechStateDirty)
		{
			SaveTechState(Singleton.playerTank, ref m_RebuiltTech);
			m_PlayerStuckChecker.Reset();
			m_PlayerTechStateDirty = false;
		}
		if (m_TechSavedTimer > 0f)
		{
			m_TechSavedTimer -= Time.deltaTime;
			if (m_TechSavedTimer <= 0f)
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechSavedMessage);
			}
		}
		return FunctionStatus.Running;
	}

	protected override void ExitModeImpl()
	{
		RemoveTech();
		CheckpointChallenge.OnBoundsWarning.Unsubscribe(OnBoundsWarning);
		CheckpointChallenge.OnCheckpointPassed.Unsubscribe(OnCheckpointPassed);
		Singleton.Manager<ManChallenge>.inst.OnChallengeEnded.Unsubscribe(OnChallengeEnded);
		Singleton.Manager<ManPointer>.inst.DragEvent.Unsubscribe(OnDragEvent);
		UIScreenLeaderboard uIScreenLeaderboard = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.LeaderBoard) as UIScreenLeaderboard;
		if ((bool)uIScreenLeaderboard)
		{
			uIScreenLeaderboard.Leaderboard.ReplayButtonClicked.Unsubscribe(OnLeaderboardReplayClicked);
		}
		(Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechLoader) as ITechLoader).RemoveScreenHandlers(LoadTechPreset);
		Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Unsubscribe(OnBlockAttached);
		Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Unsubscribe(OnBlockDetached);
		Singleton.Manager<ManCustomSkins>.inst.TankBlockPaintedEvent.Unsubscribe(OnBlockPainted);
		UISchemaMenu.ControlSchemesEditedEvent.Unsubscribe(OnTechControlSchemesEdited);
		Singleton.Manager<ManTechs>.inst.TankPostSpawnEvent.Unsubscribe(OnTechPostSpawned);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankSwitched);
		Singleton.Manager<ManReplay>.inst.StopAllReplays();
		Singleton.Manager<ManReplay>.inst.StopAndGetGhostData();
		Singleton.Manager<ManReplay>.inst.StopAllRecording();
		if ((bool)m_uScript)
		{
			m_uScript.Recycle();
			m_uScript = null;
		}
		ClearTutorial();
		m_FirstCheckpointTriggered = false;
		m_StopReplay = false;
		m_Respawning = false;
		TankCamera.inst.SetFollowTech(null);
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		Singleton.Manager<ManPurchases>.inst.ShowPalette(show: false);
		Singleton.Manager<ManPurchases>.inst.SetInventory(null);
		m_GauntletLevelData.SpawnList.RecycleAllPrefabs();
		if (m_GauntletMode == GauntletModes.Replay)
		{
			Singleton.Manager<ManPauseGame>.inst.PopPauseMenu();
		}
	}

	public override ManGameMode.GameType GetGameType()
	{
		return ManGameMode.GameType.Gauntlet;
	}

	public override string GetGameMode()
	{
		return "ModeGauntlet";
	}

	public override string GetGameSubmode()
	{
		return "";
	}

	public override ManHUD.HUDType GetDefaultHUDType()
	{
		return ManHUD.HUDType.MainGame;
	}

	public override WorldGenVersionData GetLatestMapVersion()
	{
		return m_BiomeMaps.LatestMap.WorldGenVersionData;
	}

	public override bool CanResetPosition()
	{
		if (Singleton.playerTank != null)
		{
			return !m_TutorialActive;
		}
		return false;
	}

	public override bool CanPlayerPlaceTech()
	{
		return false;
	}

	public override InventoryMetaData GetReferenceInventory()
	{
		if (m_Inventory == null)
		{
			d.LogError("ModeGauntlet - inventory has not been set yet, are you calling before EnterGenerateTerrain? Returning nil inventory");
			return new InventoryMetaData(null, locked: true);
		}
		return new InventoryMetaData(m_Inventory);
	}

	public override float GetSceneryRegrowTime()
	{
		return Globals.inst.m_DefaultSceneryRegrowTime;
	}

	public override void ResetPlayerPosition()
	{
		if ((bool)Singleton.playerTank)
		{
			if (m_TutorialActive)
			{
				Vector3 vector = m_GauntletLevelData.PlayerSpawn.position + Vector3.up * 2f;
				Singleton.playerTank.visible.Teleport(vector, m_GauntletLevelData.PlayerSpawn.orientation);
				Vector3 position = m_GauntletLevelData.CameraSpawn.position;
				Quaternion rotation = Quaternion.LookRotation(vector - position);
				Singleton.Manager<CameraManager>.inst.ResetCamera(position, rotation);
			}
			else
			{
				RemoveTech();
				float respawnState = 0f;
				SetRespawnState(respawnState);
			}
		}
	}

	private void UpdateAttractMode()
	{
		switch (m_AttractState)
		{
		case AttractState.Start:
			m_AttractNumSpawnedGhosts = 0;
			m_AttractState = AttractState.ShowCamera;
			m_AttractModeTimer = m_AttractStaticCamTime;
			m_AttractSpawnTimer = 0f;
			m_AttractShowingGhost = false;
			m_NextAttractCameraIndex = 0;
			m_NextAttractGhostIndex = 0;
			ShowCurAttractStaticCamera();
			break;
		case AttractState.FadeToBlack:
			if (Singleton.Manager<ManUI>.inst.FadeFinished())
			{
				if (m_AttractShowingGhost)
				{
					m_NextAttractGhostIndex %= m_AttractNumSpawnedGhosts;
					TankCamera.inst.SetFollowTech(m_AttractGhosts[m_NextAttractGhostIndex].ReplayTech());
					TankCamera.inst.ClearCaches();
					Singleton.Manager<ManPauseGame>.inst.Pause();
					m_AttractModeTimer = m_AttractFollowTechTime;
				}
				else
				{
					ShowCurAttractStaticCamera();
					m_AttractModeTimer = m_AttractStaticCamTime;
				}
				m_AttractState = AttractState.SetupNextCamera;
			}
			break;
		case AttractState.SetupNextCamera:
			if (!Singleton.Manager<ManWorld>.inst.TileManager.IsGenerating)
			{
				Singleton.Manager<ManUI>.inst.ClearFade(1f);
				Singleton.Manager<ManPauseGame>.inst.Resume();
				m_AttractState = AttractState.ClearFade;
			}
			break;
		case AttractState.ClearFade:
			if (Singleton.Manager<ManUI>.inst.FadeFinished())
			{
				m_AttractState = AttractState.ShowCamera;
			}
			break;
		case AttractState.ShowCamera:
			m_AttractModeTimer -= Time.deltaTime;
			if (m_AttractModeTimer <= 0f)
			{
				if (m_AttractShowingGhost)
				{
					m_NextAttractGhostIndex++;
				}
				else
				{
					m_NextAttractCameraIndex++;
				}
				m_AttractShowingGhost = m_AttractNumSpawnedGhosts > 0 && !m_AttractShowingGhost;
				bool forceFront = false;
				Singleton.Manager<ManUI>.inst.FadeToBlack(1f, forceFront);
				m_AttractState = AttractState.FadeToBlack;
			}
			break;
		default:
			d.LogError("No such Attract state " + m_AttractState);
			break;
		}
		if (m_AttractSpawnTimer <= 0f && m_AttractNumSpawnedGhosts < m_AttractGhosts.Length)
		{
			if (m_AttractGhosts[m_AttractNumSpawnedGhosts] != null)
			{
				bool ghostEffect = false;
				Singleton.Manager<ManReplay>.inst.ReplayGhost(m_AttractGhosts[m_AttractNumSpawnedGhosts], ghostEffect);
				m_AttractNumSpawnedGhosts++;
			}
			m_AttractSpawnTimer = m_AttractSpawnInterval;
		}
		m_AttractSpawnTimer -= Time.deltaTime;
		if (m_AttractGhosts.Length != 0 && m_AttractGhosts[0] != null && !Singleton.Manager<ManReplay>.inst.IsReplayRunning(m_AttractGhosts[0]))
		{
			StartAttract();
		}
	}

	private void ShowCurAttractStaticCamera()
	{
		if ((bool)m_AttractModeCameraPositions && m_AttractModeCameraPositions.childCount > 0)
		{
			TankCamera.inst.SetFollowTech(null);
			m_NextAttractCameraIndex %= m_AttractModeCameraPositions.childCount;
			Transform child = m_AttractModeCameraPositions.GetChild(m_NextAttractCameraIndex);
			Singleton.Manager<CameraManager>.inst.ResetCamera(child.position, child.localRotation);
		}
		else
		{
			d.LogError("ModeGauntlet: no attract mode cameras defined");
		}
	}

	private void UpdateReplay()
	{
		switch (m_ReplayState)
		{
		case ReplayState.Start:
		{
			m_ReplayState = ReplayState.WaitingForCountdown;
			Vector3 position = m_GauntletLevelData.ChallengeSpawn.position;
			Vector3 position2 = m_GauntletLevelData.CameraSpawn.position;
			Quaternion rotation = Quaternion.LookRotation(position - position2);
			Singleton.Manager<CameraManager>.inst.ResetCamera(position2, rotation);
			m_Timer = m_CountdownTimer;
			break;
		}
		case ReplayState.WaitingForCountdown:
			if (m_Timer <= 0f)
			{
				bool ghostEffect2 = false;
				Singleton.Manager<ManReplay>.inst.ReplayGhost(m_ReplayData.m_PlayerGhost, ghostEffect2);
				Singleton.Manager<ManChallenge>.inst.Protagonist = m_ReplayData.m_PlayerGhost.ReplayTech();
				Singleton.Manager<ManChallenge>.inst.BeginChallenge();
				TankCamera.inst.SetFollowTech(m_ReplayData.m_PlayerGhost.ReplayTech());
				m_RunningTime = 0f;
				m_ReplayState = ReplayState.PlayingBack;
			}
			else
			{
				m_Timer -= Time.deltaTime;
			}
			break;
		case ReplayState.PlayingBack:
		{
			for (int num = m_ReplayData.m_OtherGhosts.Count - 1; num >= 0; num--)
			{
				ManReplay.ReplayTimingData replayTimingData = m_ReplayData.m_OtherGhosts[num];
				if (m_RunningTime >= replayTimingData.m_StartTime)
				{
					bool ghostEffect = false;
					float startTime = m_RunningTime - replayTimingData.m_StartTime;
					Singleton.Manager<ManReplay>.inst.ReplayGhost(replayTimingData.m_GhostData, ghostEffect, startTime);
					m_ReplayData.m_OtherGhosts.RemoveAt(num);
				}
			}
			m_RunningTime += Time.deltaTime;
			if (!Singleton.Manager<ManReplay>.inst.IsReplayRunning(m_ReplayData.m_PlayerGhost) || m_StopReplay)
			{
				Singleton.Manager<ManChallenge>.inst.KillChallenge();
				TankCamera.inst.SetFollowTech(null);
				m_ReplayState = ReplayState.Done;
			}
			else if (Singleton.Manager<ManPauseGame>.inst.IsPauseMenuShowing())
			{
				float num2 = 10f;
				if (m_Timer >= num2)
				{
					Singleton.Manager<ManPauseGame>.inst.PauseGame(pauseState: false);
				}
				else
				{
					m_Timer += Time.deltaTime;
				}
			}
			else
			{
				m_Timer = 0f;
			}
			break;
		}
		case ReplayState.Done:
			break;
		}
	}

	private ScoreFileContents LoadScoreFilePC(string path)
	{
		ScoreFileContents objectToLoad = null;
		try
		{
			FileStream fileStream = File.Open(path, FileMode.Open);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			int count = binaryReader.ReadInt32();
			byte[] bytes = binaryReader.ReadBytes(count);
			ManSaveGame.LoadObjectFromBytes(ref objectToLoad, bytes);
			if (objectToLoad != null && objectToLoad.version != 1)
			{
				objectToLoad = null;
			}
			if (objectToLoad != null)
			{
				int num = binaryReader.ReadInt32();
				objectToLoad.replayData = ((num > 0) ? binaryReader.ReadBytes(num) : null);
			}
			binaryReader.Close();
			fileStream.Close();
		}
		catch (Exception ex)
		{
			d.LogError("ModeGauntlet - Reading score: " + path + " isn't valid " + ex.ToString());
		}
		return objectToLoad;
	}

	private void SaveScoreFilePC(string trackName, ScoreFileContents scoreEntry)
	{
		string highscoresFolderPC = GetHighscoresFolderPC(trackName);
		Directory.CreateDirectory(highscoresFolderPC);
		if (!Directory.Exists(highscoresFolderPC))
		{
			d.LogError("ModeGauntlet - Unable to write score: Can't find path: " + highscoresFolderPC);
			return;
		}
		string text = highscoresFolderPC + "/" + LocalIdent + "_" + scoreEntry.score + ".scr";
		byte[] replayData = scoreEntry.replayData;
		scoreEntry.replayData = null;
		scoreEntry.version = 1;
		try
		{
			FileStream fileStream = File.Create(text);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			int initialCapacitySizeHint = 262144;
			byte[] array = ManSaveGame.SaveObjectToBytes(scoreEntry, initialCapacitySizeHint);
			binaryWriter.Write(array.Length);
			binaryWriter.Write(array);
			if (replayData != null)
			{
				binaryWriter.Write(replayData.Length);
				binaryWriter.Write(replayData);
			}
			else
			{
				binaryWriter.Write(0);
			}
			binaryWriter.Close();
			fileStream.Close();
		}
		catch (Exception ex)
		{
			d.LogError("Failed to write score file " + text + ": " + ex.ToString());
		}
		scoreEntry.replayData = replayData;
	}

	private KeyValuePair<string, ScoreFileContents> GetWorstScore(Dictionary<string, ScoreFileContents> scoreTable)
	{
		KeyValuePair<string, ScoreFileContents> result = default(KeyValuePair<string, ScoreFileContents>);
		foreach (KeyValuePair<string, ScoreFileContents> item in scoreTable)
		{
			if (result.Key == null || item.Value.score > result.Value.score)
			{
				result = item;
			}
		}
		return result;
	}

	private void SaveScoreFileConsoles(string entryKey, string trackName, ScoreFileContents scoreEntry, Action callback)
	{
		d.Log("[ModeGauntlet] save score file");
		scoreEntry.version = 1;
		SaveDataConsoles.LoadData(trackName, "Gauntlet", delegate(bool success, byte[] result)
		{
			bool flag = true;
			Dictionary<string, ScoreFileContents> objectToLoad = new Dictionary<string, ScoreFileContents>();
			if (result != null)
			{
				ManSaveGame.LoadObjectFromBytes(ref objectToLoad, result);
			}
			d.Log("[ModeGauntlet] leaderboard data loaded and has " + objectToLoad.Count + " entries");
			if (objectToLoad.Count >= m_MaxHighScoreTableSize)
			{
				KeyValuePair<string, ScoreFileContents> worstScore = GetWorstScore(objectToLoad);
				if (scoreEntry.score > worstScore.Value.score)
				{
					d.Log("[ModeGauntlet] leaderboard data not adding score to full table score=" + scoreEntry.score + " is worse than last place " + worstScore.Value.score);
					flag = false;
				}
			}
			if (flag)
			{
				while (objectToLoad.Count >= m_MaxHighScoreTableSize)
				{
					KeyValuePair<string, ScoreFileContents> worstScore2 = GetWorstScore(objectToLoad);
					d.Log("[ModeGauntlet] leaderboard data removing old worst entry with key " + worstScore2.Key + " was score = " + worstScore2.Value.score + "  playerIdent =" + worstScore2.Value.playerIdent);
					objectToLoad.Remove(worstScore2.Key);
				}
				objectToLoad[entryKey] = scoreEntry;
				d.Log("[ModeGauntlet] leaderboard data saving");
				int initialCapacitySizeHint = 262144;
				int maxExpectedCompressedSize = 3145728;
				byte[] array = ManSaveGame.SaveObjectToBytes(objectToLoad, initialCapacitySizeHint);
				SaveDataConsoles.SaveData(trackName, "Gauntlet", array, array.Length, maxExpectedCompressedSize, null);
				BuildScoreListConsoles(trackName, objectToLoad);
			}
			callback();
		});
	}

	private string GetHighscoresFolderPC(string trackName)
	{
		d.Assert(!trackName.NullOrEmpty(), "GetHighscoreFolder - Invalid track name (null or empty) passed in!");
		return m_FilePath + "/" + trackName;
	}

	private ReplayData LoadReplayFromLeaderboardEntry(ScoreFileContents tableEntry)
	{
		ReplayData objectToLoad = null;
		if (tableEntry != null)
		{
			try
			{
				ManSaveGame.LoadObjectFromBytes(ref objectToLoad, tableEntry.replayData);
			}
			catch (Exception ex)
			{
				d.LogError("ModeGauntlet - unable to unpack replay data:" + ex.ToString());
			}
			if (objectToLoad != null)
			{
				objectToLoad.m_PlayerGhost.SetTechName(tableEntry.playerName);
				objectToLoad.m_PlayerGhost.SetTechOverlayType(m_GhostOverlayData);
			}
		}
		return objectToLoad;
	}

	private void ExitPlaying()
	{
		ClearReplayData();
		m_LoadedScores = null;
		m_LoadedScoresTrack = null;
		Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
	}
}
