#define UNITY_EDITOR
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ModeCheckpointChallenge : Mode<ModeCheckpointChallenge>
{
	[Serializable]
	public class ScoreRecord
	{
		public string name;

		public float time;
	}

	[Serializable]
	public class RaceTrack
	{
		public CheckpointChallenge.Track track;

		public string name;

		public ManGameMode.GameType m_MyType;

		public bool inMenu = true;

		public bool inDemo;

		public BiomeMap biomeMap;

		public string seed;

		public SpawnList spawnList = new SpawnList();

		public ScoreRecord[] records = new ScoreRecord[0];

		public TankPreset playerPreset;

		public PositionWithFacing playerSpawn = PositionWithFacing.identity;

		public PositionWithFacing cameraSpawn = PositionWithFacing.identity;

		public bool m_ShowInfoScreen;

		public LocalisedString m_Info;

		public LocalisedString m_Continue;
	}

	public float sceneryRegrowTime = 5f;

	[HideInInspector]
	public RaceTrack[] tracks;

	[SerializeField]
	[Tooltip("Change this from -1 if overriding")]
	private float m_OverrideGrabDistance = -1f;

	private CheckpointChallenge m_Challenge;

	private bool m_LoadedTank;

	private ManOnScreenMessages.OnScreenMessage msgBuild;

	private ManOnScreenMessages.OnScreenMessage msgBuildFly;

	private ManOnScreenMessages.OnScreenMessage msgStart;

	private ManOnScreenMessages.OnScreenMessage msgFollow;

	private ManOnScreenMessages.OnScreenMessage msgFollowFly;

	private ManOnScreenMessages.OnScreenMessage msgTryAgain;

	private ManOnScreenMessages.OnScreenMessage msgNewRecord;

	private ManOnScreenMessages.OnScreenMessage msgFlyFail;

	private ManOnScreenMessages.OnScreenMessage msgNotBad;

	private ManOnScreenMessages.OnScreenMessage msgOutOfBounds;

	public bool InProgress => m_Challenge.InProgress;

	private RaceTrack CurrentTrack { get; set; }

	private IEnumerator TutorialMain()
	{
		if (!m_LoadedTank)
		{
			ManOnScreenMessages.OnScreenMessage buildMsg = (CurrentTrack.track.isFlying ? msgBuildFly : msgBuild);
			Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(buildMsg, boolVal: true);
			while (Singleton.Manager<ManOnScreenMessages>.inst.IsInQueue(buildMsg) && !m_Challenge.InProgress)
			{
				yield return null;
			}
		}
		if (!m_Challenge.InProgress)
		{
			Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(msgStart, boolVal: true);
		}
		while (Singleton.Manager<ManOnScreenMessages>.inst.IsInQueue(msgStart) && !m_Challenge.InProgress)
		{
			yield return null;
		}
		while (!m_Challenge.InProgress)
		{
			yield return null;
		}
		Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(CurrentTrack.track.isFlying ? msgFollowFly : msgFollow, boolVal: true);
		while (m_Challenge.InProgress)
		{
			yield return null;
		}
		yield return new WaitForSeconds(5f);
		Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
		Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(msgTryAgain, boolVal: true);
	}

	private TechData GetPlayerData(InitSettings initSettings)
	{
		m_LoadedTank = false;
		TechData result = ((CurrentTrack.playerPreset != null) ? CurrentTrack.playerPreset.GetTechDataFormatted() : null);
		object value = null;
		if (initSettings.TryGetValue("LoadPlayerPreset", out value))
		{
			result = value as TechData;
			m_LoadedTank = true;
		}
		return result;
	}

	protected override void EnterGenerateTerrain(InitSettings initSettings)
	{
		object name = null;
		if (initSettings.TryGetValue("TrackName", out name))
		{
			CurrentTrack = tracks.SingleOrDefault((RaceTrack mode) => mode.name.EqualsNoCase(name as string));
		}
		if (CurrentTrack == null)
		{
			d.LogError("Failed to initialise mode");
			return;
		}
		Singleton.Manager<ManTimeOfDay>.inst.EnableSkyDome(enable: true);
		Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(enable: false);
		Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(11, 0, 0);
		Singleton.Manager<ManWorld>.inst.SeedString = CurrentTrack.seed;
		Singleton.Manager<ManGameMode>.inst.RegenerateWorld(CurrentTrack.biomeMap, CurrentTrack.cameraSpawn.position, CurrentTrack.cameraSpawn.orientation);
	}

	protected override void EnterPreModeImpl(InitSettings initSettings)
	{
		EnterDefaultCameraMode();
		m_Challenge = new CheckpointChallenge(placeInfo: new Challenge.PlacementInfo
		{
			yPositionsRelativeToGround = false
		}, track: CurrentTrack.track, challengeData: null);
		CheckpointChallenge.OnBoundsWarning.Subscribe(OnBoundsWarning);
		Singleton.Manager<ManChallenge>.inst.OnChallengeEnded.Subscribe(OnChallengeEnded);
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(TankKilled);
		m_Challenge.Setup(null);
		msgBuild = new ManOnScreenMessages.OnScreenMessage(new string[1] { Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.CheckpointChallenge, 0) }, ManOnScreenMessages.MessagePriority.Medium);
		msgBuildFly = new ManOnScreenMessages.OnScreenMessage(new string[1] { Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.CheckpointChallenge, 1) }, ManOnScreenMessages.MessagePriority.Medium);
		msgStart = new ManOnScreenMessages.OnScreenMessage(new string[1] { Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.CheckpointChallenge, 2) }, ManOnScreenMessages.MessagePriority.Medium);
		msgFollow = new ManOnScreenMessages.OnScreenMessage(new string[1] { Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.CheckpointChallenge, 3) }, ManOnScreenMessages.MessagePriority.Medium);
		msgFollowFly = new ManOnScreenMessages.OnScreenMessage(new string[1] { Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.CheckpointChallenge, 4) }, ManOnScreenMessages.MessagePriority.Medium);
		msgTryAgain = new ManOnScreenMessages.OnScreenMessage(new string[1] { Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.CheckpointChallenge, 5) }, ManOnScreenMessages.MessagePriority.Medium);
		msgNewRecord = new ManOnScreenMessages.OnScreenMessage(new string[1] { Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.CheckpointChallenge, 6) }, ManOnScreenMessages.MessagePriority.Medium);
		msgFlyFail = new ManOnScreenMessages.OnScreenMessage(new string[1] { Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.CheckpointChallenge, 7) }, ManOnScreenMessages.MessagePriority.Medium);
		msgNotBad = new ManOnScreenMessages.OnScreenMessage(new string[1] { Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.CheckpointChallenge, 8) }, ManOnScreenMessages.MessagePriority.Medium);
		msgOutOfBounds = new ManOnScreenMessages.OnScreenMessage(new string[1] { Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.CheckpointChallenge, 9) }, ManOnScreenMessages.MessagePriority.Medium);
		Singleton.Manager<FTUE>.inst.Execute(TutorialMain());
		CurrentTrack.spawnList.SpawnAll();
		TechData playerData = GetPlayerData(initSettings);
		if (playerData != null)
		{
			ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
			{
				techData = playerData,
				blockIDs = null,
				teamID = 0,
				position = CurrentTrack.playerSpawn.position,
				rotation = CurrentTrack.playerSpawn.orientation,
				grounded = true
			};
			Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: true);
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank);
		}
	}

	protected override FunctionStatus UpdatePreModeImpl(InitSettings initSettings)
	{
		FunctionStatus result = FunctionStatus.Running;
		if (Singleton.playerTank.grounded)
		{
			if (!m_LoadedTank)
			{
				Singleton.Manager<ManSFX>.inst.SuppressUISFX();
				Singleton.playerTank.beam.EnableBeam(enable: true);
			}
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Snapshot);
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ResetPosition);
			if (CurrentTrack.m_ShowInfoScreen)
			{
				(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(CurrentTrack.m_Info.Value, delegate
				{
					Singleton.Manager<ManUI>.inst.PopScreen();
				}, CurrentTrack.m_Continue.Value);
				Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen, ManUI.PauseType.Pause);
			}
			if (m_OverrideGrabDistance != -1f)
			{
				Singleton.Manager<ManPointer>.inst.SetPickupRange(m_OverrideGrabDistance);
			}
			result = FunctionStatus.Done;
		}
		return result;
	}

	protected override void EnterModeUpdateImpl()
	{
	}

	protected override FunctionStatus UpdateModeImpl()
	{
		m_Challenge.Update();
		return FunctionStatus.Running;
	}

	protected override void ExitModeImpl()
	{
		Singleton.Manager<FTUE>.inst.Stop();
		m_Challenge.TearDown();
		CheckpointChallenge.OnBoundsWarning.Unsubscribe(OnBoundsWarning);
		Singleton.Manager<ManChallenge>.inst.OnChallengeEnded.Unsubscribe(OnChallengeEnded);
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Unsubscribe(TankKilled);
		m_Challenge = null;
		msgBuild = null;
		msgBuildFly = null;
		msgStart = null;
		msgFollow = null;
		msgFollowFly = null;
		msgTryAgain = null;
		msgNewRecord = null;
		msgFlyFail = null;
		msgNotBad = null;
		msgOutOfBounds = null;
	}

	public override ManGameMode.GameType GetGameType()
	{
		return CurrentTrack?.m_MyType ?? ManGameMode.GameType.RacingChallenge;
	}

	public override string GetGameMode()
	{
		return "ModeCheckpoint";
	}

	public override string GetGameSubmode()
	{
		if (CurrentTrack == null)
		{
			return "";
		}
		return CurrentTrack.name;
	}

	public override float GetSceneryRegrowTime()
	{
		return sceneryRegrowTime;
	}

	public override WorldGenVersionData GetLatestMapVersion()
	{
		return CurrentTrack.biomeMap.WorldGenVersionData;
	}

	public override void ResetPlayerPosition()
	{
		if ((bool)Singleton.playerTank)
		{
			Singleton.playerTank.visible.Teleport(CurrentTrack.playerSpawn.position, CurrentTrack.playerSpawn.orientation);
			m_Challenge.Reset();
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

	private void OnChallengeEnded(Challenge.ChallengeEndData challengeEndData)
	{
		if (challengeEndData is CheckpointChallenge.CheckpointChallengeEndData checkpointChallengeEndData)
		{
			ManOnScreenMessages.OnScreenMessage onScreenMessage;
			switch (checkpointChallengeEndData.endReason)
			{
			case CheckpointChallenge.EndReason.Success:
				onScreenMessage = (checkpointChallengeEndData.hasNewRecord ? msgNewRecord : msgNotBad);
				break;
			case CheckpointChallenge.EndReason.FailedQuit:
				onScreenMessage = null;
				break;
			case CheckpointChallenge.EndReason.FailedTouchedGround:
				onScreenMessage = msgFlyFail;
				break;
			default:
				d.Assert(condition: false, "OnChallengeEnded: invalid end reason: " + checkpointChallengeEndData.endReason);
				onScreenMessage = null;
				break;
			}
			if (onScreenMessage != null)
			{
				Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(onScreenMessage);
			}
		}
	}

	private void OnBoundsWarning(CheckpointChallenge.BoundsArea area)
	{
		if (area == CheckpointChallenge.BoundsArea.Illegal)
		{
			m_Challenge.TearDown();
			Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(msgOutOfBounds);
		}
	}

	private void TankKilled(Tank tank, ManDamage.DamageInfo info)
	{
		if (tank == Singleton.playerTank)
		{
			m_Challenge.Reset();
		}
	}
}
