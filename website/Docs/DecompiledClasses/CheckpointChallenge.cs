#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CheckpointChallenge : Challenge
{
	[Serializable]
	public class Track
	{
		public bool isFlying;

		public float checkpointHeight = 100f;

		[FormerlySerializedAs("m_CheckpointGatePrefab")]
		public Checkpoint m_CheckpointPrefab;

		public Checkpoint m_LastCheckpointPrefabOverride;

		public Transform trackEdgePrefab;

		public Transform boundsEdgePrefab;

		public float trackEdgeHeight = 0.1f;

		public float trackEdgeWidth = 2f;

		public float edgeMaxRenderError = 0.01f;

		public int splineResolution = 200;

		public float passedCheckpointThreshold = 20f;

		public float goingWrongWayDistThreshold = 30f;

		public float goingWrongWayAngleThreshold = 130f;

		public float m_MaximumHeightVariation = 1000f;

		[Tooltip("Maximum incline between sample points on the track in degrees")]
		public float m_MaxInclineDegrees = float.MaxValue;

		[FormerlySerializedAs("m_MinInclineDegrees")]
		[Tooltip("Maximum decline between sample points on the track in degrees")]
		public float m_MaxDeclineDegrees = float.MinValue;

		public int m_NumFutureGatesToShow = 1;

		public Vector3[] checkpoints = new Vector3[0];

		public TrackSpline spline;

		[SerializeField]
		public ManSFX.MiscSfxType startSound = ManSFX.MiscSfxType.StuntRingStart;

		[SerializeField]
		public ManSFX.MiscSfxType checkPointSound = ManSFX.MiscSfxType.StuntRing;

		[SerializeField]
		public ManSFX.MiscSfxType completeSound = ManSFX.MiscSfxType.StuntComplete;

		[SerializeField]
		public ManSFX.MiscSfxType failSound = ManSFX.MiscSfxType.StuntFailed;

		public WaypointOverlayData waypointOverlayData;
	}

	public enum EndReason
	{
		Success,
		FailedQuit,
		FailedTouchedGround,
		FailedTimeUp,
		FailedOutOfBounds,
		FailedPlayerDestroyed
	}

	public enum BoundsArea
	{
		Track,
		Caution,
		Illegal
	}

	public class CheckpointChallengeEndData : ChallengeEndData
	{
		public EndReason endReason;

		public bool hasNewRecord;

		public float latestTime;

		public CheckpointChallengeEndData(EndReason endReason, bool hasNewRecord, float latestTime)
			: base(endReason == EndReason.Success)
		{
			this.endReason = endReason;
			this.hasNewRecord = hasNewRecord;
			this.latestTime = latestTime;
		}
	}

	private class CheckpointChallengeSavedData : ChallengeSaveData
	{
		public float fastestTime;
	}

	public static Event<int, Vector3, Quaternion> OnCheckpointPassed;

	public static Event<BoundsArea> OnBoundsWarning;

	private Track m_Track;

	private CheckpointChallengeData m_ChallengeData;

	private PlacementInfo m_PlacementInfo;

	private Checkpoint[] m_Checkpoints;

	private int m_NextGateToHitIndex;

	private TrackedVisible m_Waypoint;

	private ChallengeTimer m_ChallengeTimer = new ChallengeTimer();

	private float m_CurrentBest;

	private int m_TopSpeed;

	private bool m_OwnsControlSchemaHUD;

	private float m_ShowResultsTimeRemaining;

	private UICheckpointChallengeHUD m_MyHUD;

	private TrackRepresentation m_TrackRepresentation = new TrackRepresentation();

	private BoundsArea m_BoundsArea;

	private float m_FurthestDistanceReached;

	private float m_NextGateDistance;

	private CheckpointChallengeEndData m_CompletionData;

	public bool InProgress => m_ChallengeTimer.IsRunningSet;

	private Checkpoint NextGateToHit
	{
		get
		{
			if (m_Checkpoints == null || m_NextGateToHitIndex < 0 || m_NextGateToHitIndex >= m_Checkpoints.Length)
			{
				return null;
			}
			return m_Checkpoints[m_NextGateToHitIndex];
		}
	}

	public override bool IsShowingResults()
	{
		return m_ShowResultsTimeRemaining > 0f;
	}

	public static string ConvertTimeToScoreString(float timeSecs)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds(timeSecs);
		return timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00") + ":" + timeSpan.Milliseconds.ToString("000");
	}

	public CheckpointChallenge(Track track, CheckpointChallengeData challengeData, PlacementInfo placeInfo)
	{
		m_Track = track;
		m_ChallengeData = challengeData;
		m_PlacementInfo = placeInfo;
	}

	public override void Setup(object savedContextData)
	{
		CheckpointChallengeSavedData checkpointChallengeSavedData = ((savedContextData != null) ? ((CheckpointChallengeSavedData)savedContextData) : null);
		m_MyHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.CheckpointChallenge) as UICheckpointChallengeHUD;
		m_ChallengeTimer.Reset();
		m_CurrentBest = float.MaxValue;
		m_OwnsControlSchemaHUD = Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeGauntlet>();
		if (m_PlacementInfo.ShowHUD)
		{
			m_MyHUD.SetChallengeTimer(m_ChallengeTimer);
			if (m_OwnsControlSchemaHUD)
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ControlSchema);
			}
		}
		else
		{
			m_MyHUD.SetChallengeTimer(null);
			if (m_OwnsControlSchemaHUD)
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ControlSchema);
			}
		}
		m_MyHUD.Init();
		if (savedContextData != null)
		{
			m_CurrentBest = checkpointChallengeSavedData.fastestTime;
			m_MyHUD.SetBestTimeText(ConvertTimeToScoreString(m_CurrentBest));
		}
		m_TrackRepresentation.Setup(m_Track, m_PlacementInfo);
		m_BoundsArea = BoundsArea.Track;
		SpawnGates();
		ResetRaceProgress();
		m_FurthestDistanceReached = 0f;
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankChanged);
	}

	public override void Begin()
	{
		if (!InProgress)
		{
			StartChallenge();
			if (m_Track.spline.IsGateAtNodeIndex(0))
			{
				TriggerNextCheckPointGate();
			}
		}
	}

	public override void End(ManChallenge.ChallengeEndReason endReason)
	{
		EndReason reason;
		switch (endReason)
		{
		case ManChallenge.ChallengeEndReason.OutOfBounds:
			reason = EndReason.FailedOutOfBounds;
			break;
		case ManChallenge.ChallengeEndReason.PlayerDestroyed:
			reason = EndReason.FailedPlayerDestroyed;
			break;
		case ManChallenge.ChallengeEndReason.Unspecified:
		case ManChallenge.ChallengeEndReason.PlayerExit:
			reason = EndReason.FailedQuit;
			break;
		default:
			d.LogError(string.Concat("CheckpointChallenge.End - Unhandled mapping from ChallengeEndReason '", endReason, "' to CheckPointChallenge end reason."));
			reason = EndReason.FailedQuit;
			break;
		}
		EndChallenge(reason);
	}

	public override void TearDown()
	{
		TearDownNonTimer();
		TearDownTimer();
	}

	public void TearDownTimer()
	{
		m_ChallengeTimer.ResetTimeElapsed();
		m_MyHUD.SetChallengeTimer(null);
		if (m_OwnsControlSchemaHUD)
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ControlSchema);
		}
	}

	public void TearDownNonTimer()
	{
		m_ChallengeTimer.Stop();
		m_TopSpeed = 0;
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankChanged);
		m_TrackRepresentation.TearDown();
		RecycleGates();
		DespawnAndUnregisterWaypoint();
	}

	public void Reset()
	{
		EndChallenge(EndReason.FailedQuit);
	}

	public override void Update()
	{
		if (InProgress)
		{
			m_ChallengeTimer.Update();
			bool flag = false;
			if (m_ChallengeData.TimeTicksDown && m_ChallengeTimer.DisplayTime <= 0f)
			{
				EndChallenge(EndReason.FailedTimeUp);
				flag = true;
			}
			if (m_Track.isFlying && (bool)base.Protagonist && base.Protagonist.grounded)
			{
				EndChallenge(EndReason.FailedTouchedGround);
				flag = true;
			}
			if (flag)
			{
				return;
			}
			if (m_ChallengeData.PlayDangerMusic)
			{
				Singleton.Manager<ManMusic>.inst.SetDanger(ManMusic.DangerContext.Circumstance.SetPiece, FactionSubTypes.VEN);
			}
			if ((bool)base.Protagonist)
			{
				int num = (int)(base.Protagonist.rbody.velocity.magnitude * 2.2369363f);
				m_MyHUD.SetSpeed(num);
				if (num > m_TopSpeed)
				{
					m_TopSpeed = num;
					m_MyHUD.SetTopSpeed(num);
				}
			}
			if (m_Waypoint == null && (bool)NextGateToHit)
			{
				SpawnAndRegisterWaypoint(NextGateToHit.transform.position);
			}
			m_TrackRepresentation.Update();
			float num2 = 0f;
			Vector3 trackTangent = Vector3.forward;
			if ((bool)base.Protagonist)
			{
				TrackRepresentation.RelativeTrackInfo relativeTrackInformation = m_TrackRepresentation.GetRelativeTrackInformation(base.Protagonist.boundsCentreWorld);
				SendBoundsEventIfAreaChanged(relativeTrackInformation.distanceOutsideWarningArea, relativeTrackInformation.distanceOutsidePenaltyArea);
				num2 = relativeTrackInformation.distanceFromStart;
				m_FurthestDistanceReached = Mathf.Max(num2, m_FurthestDistanceReached);
				m_FurthestDistanceReached = Mathf.Min(m_NextGateDistance, m_FurthestDistanceReached);
				trackTangent = relativeTrackInformation.trackTangent;
			}
			if (!m_PlacementInfo.ReplayMode)
			{
				UpdateProgressWarningsVisibility(num2, trackTangent);
			}
			else if (IsPlayerPastGateWarningDist(num2))
			{
				TriggerNextCheckPointGate();
			}
		}
		else if (IsShowingResults())
		{
			m_ShowResultsTimeRemaining -= Time.deltaTime;
			if (m_ShowResultsTimeRemaining <= 0f)
			{
				FinaliseEndChallenge();
			}
		}
	}

	private void EndChallenge(EndReason reason, bool hasNewRecord = false, float latestTime = 0f)
	{
		DespawnAndUnregisterWaypoint();
		m_ChallengeTimer.Stop();
		m_CompletionData = new CheckpointChallengeEndData(reason, hasNewRecord, latestTime);
		if (hasNewRecord)
		{
			CheckpointChallengeSavedData saveData = new CheckpointChallengeSavedData
			{
				fastestTime = latestTime
			};
			m_CompletionData.SetSaveData(saveData);
		}
		if (reason != EndReason.Success && reason != EndReason.FailedQuit)
		{
			Singleton.Manager<ManSFX>.inst.PlayMiscSFX(m_Track.failSound, Singleton.cameraTrans.position);
		}
		if (reason == EndReason.Success && m_ChallengeData.ShowResultsTime > 0f)
		{
			m_ShowResultsTimeRemaining = m_ChallengeData.ShowResultsTime;
			TearDownNonTimer();
		}
		else
		{
			FinaliseEndChallenge();
		}
	}

	private void FinaliseEndChallenge()
	{
		m_ChallengeTimer.ResetTimeElapsed();
		OnChallengeEnded.Send(m_CompletionData);
	}

	private void SpawnAndRegisterWaypoint(Vector3 position)
	{
		if (m_Waypoint == null)
		{
			m_Waypoint = Singleton.Manager<ManSpawn>.inst.SpawnItemRef(new ItemTypeInfo(ObjectTypes.Waypoint, 0), position, Quaternion.identity, addToObjectManager: false, forceSpawn: true);
			m_Waypoint.RadarType = RadarTypes.Checkpoint;
			if (m_Track.spline.IsFreeform)
			{
				m_Waypoint.visible.trans.position = position;
			}
			Singleton.Manager<ManOverlay>.inst.AddWaypointOverlay(m_Waypoint, m_Track.waypointOverlayData);
		}
	}

	private void DespawnAndUnregisterWaypoint()
	{
		if (m_Waypoint != null)
		{
			bool allowMissing = true;
			Singleton.Manager<ManOverlay>.inst.RemoveWaypointOverlay(m_Waypoint, allowMissing);
			if ((bool)m_Waypoint.visible && (bool)m_Waypoint.visible.Waypoint)
			{
				m_Waypoint.visible.RemoveFromGame();
			}
			m_Waypoint = null;
		}
	}

	private void ResetRaceProgress()
	{
		m_NextGateToHitIndex = 0;
		m_NextGateDistance = CalculateProgressToNextGate();
	}

	private float CalculateProgressToNextGate()
	{
		Checkpoint nextGateToHit = NextGateToHit;
		d.Assert(nextGateToHit, "CalculateProgressToNextGate: we dont' have a next gate");
		if ((bool)nextGateToHit)
		{
			return m_TrackRepresentation.GetDistanceFromStart(nextGateToHit.transform.position);
		}
		return 0f;
	}

	private void SpawnGates()
	{
		if (m_Track.m_CheckpointPrefab != null)
		{
			List<Checkpoint> list = new List<Checkpoint>();
			int num = -1;
			if (m_Track.m_LastCheckpointPrefabOverride != null)
			{
				for (int num2 = m_Track.spline.NumNodes - 1; num2 >= 0; num2--)
				{
					if (m_Track.spline.IsGateAtNodeIndex(num2))
					{
						num = num2;
						break;
					}
				}
			}
			for (int i = 0; i < m_Track.spline.NumNodes; i++)
			{
				if (m_Track.spline.IsGateAtNodeIndex(i))
				{
					m_Track.spline.CalcNodeValuesSlow(TrackSpline.CurveType.TrackCentre, i, out var position, out var forward, out var up);
					Matrix4x4 spawnTransform = m_PlacementInfo.SpawnTransform;
					Vector3 vector = spawnTransform.MultiplyPoint(position);
					Vector3 position2;
					if (m_PlacementInfo.yPositionsRelativeToGround)
					{
						position2 = Singleton.Manager<ManWorld>.inst.ProjectToGround(vector);
						position2.y += position.y;
						d.Assert(!m_PlacementInfo.smoothGateUpDir, "Gate up dir smoothing is not yet supported in combination with Relative To Ground placement.");
					}
					else
					{
						position2 = vector;
					}
					Vector3 fwdDir = spawnTransform.MultiplyVector(forward);
					Vector3 upDir = spawnTransform.MultiplyVector(up);
					float trackWidthAtNodeIndex = m_Track.spline.GetTrackWidthAtNodeIndex(i);
					float num3 = m_Track.spline.GetHeightAtNodeIndex(i);
					d.LogFormat("Setup gate: height: {0}, < Epsilon: {1}, epsilon {2}", num3, num3 < Mathf.Epsilon, Mathf.Epsilon);
					if (num3 < 1f)
					{
						num3 = m_Track.checkpointHeight;
					}
					Checkpoint checkpoint = ((i == num && m_Track.m_LastCheckpointPrefabOverride != null) ? m_Track.m_LastCheckpointPrefabOverride : m_Track.m_CheckpointPrefab).Spawn();
					int count = list.Count;
					checkpoint.Setup(position2, fwdDir, upDir, trackWidthAtNodeIndex, num3, count, m_ChallengeData.GetTimeLimit(), m_Track.m_NumFutureGatesToShow);
					checkpoint.OnCheckpointPassed.Subscribe(OnTankGatePassed);
					checkpoint.gameObject.name = "Checkpoint " + list.Count;
					list.Add(checkpoint);
				}
			}
			m_Checkpoints = list.ToArray();
		}
		else
		{
			d.LogError("CheckpointChallenge Setup: doesn't have a checkpoint gate prefab -- cannot create gates");
		}
	}

	private void RecycleGates()
	{
		if (m_Checkpoints != null)
		{
			for (int i = 0; i < m_Checkpoints.Length; i++)
			{
				m_Checkpoints[i].OnCheckpointPassed.Unsubscribe(OnTankGatePassed);
				m_Checkpoints[i].Cleanup();
			}
			m_Checkpoints = null;
		}
	}

	private void UpdateProgressWarningsVisibility(float curDistanceFromStart, Vector3 trackTangent)
	{
		bool flag = false;
		bool goingWrongWayMessageVisible = false;
		if ((bool)base.Protagonist)
		{
			if (IsPlayerPastGateWarningDist(curDistanceFromStart))
			{
				flag = true;
			}
			Vector3 normalized = base.Protagonist.trans.forward.SetY(0f).normalized;
			float num = 57.29578f * Mathf.Acos(normalized.Dot(trackTangent));
			if (InProgress && !flag && curDistanceFromStart < m_FurthestDistanceReached - m_Track.goingWrongWayDistThreshold && num > m_Track.goingWrongWayAngleThreshold)
			{
				goingWrongWayMessageVisible = true;
			}
		}
		m_MyHUD.GoingWrongWayMessageVisible = goingWrongWayMessageVisible;
		m_MyHUD.PassedCheckPointMessageVisible = flag;
	}

	private bool IsPlayerPastGateWarningDist(float curDistanceFromStart)
	{
		bool result = false;
		if (InProgress && NextGateToHit != null)
		{
			result = !NextGateToHit.IsTechInContact(base.Protagonist) && curDistanceFromStart > m_NextGateDistance + m_Track.passedCheckpointThreshold;
		}
		return result;
	}

	private void OnTankGatePassed(Checkpoint checkpoint, Tank tank)
	{
		if (tank != base.Protagonist || (m_Track.isFlying && tank.grounded))
		{
			return;
		}
		Checkpoint nextGateToHit = NextGateToHit;
		if ((bool)nextGateToHit && nextGateToHit == checkpoint)
		{
			if (!InProgress)
			{
				StartChallenge();
			}
			TriggerNextCheckPointGate();
		}
	}

	private void StartChallenge()
	{
		m_ChallengeTimer.ResetTimeElapsed();
		float startTimeRemaining = 0f;
		if (m_ChallengeData.TimeTicksDown)
		{
			startTimeRemaining = m_ChallengeData.GetTimeForCheckpoint(0);
		}
		m_ChallengeTimer.Start(startTimeRemaining);
		m_TrackRepresentation.ShowTrackBoundary();
		Singleton.Manager<ManSFX>.inst.PlayMiscSFX(m_Track.startSound, Singleton.cameraTrans.position);
	}

	private void TriggerNextCheckPointGate()
	{
		if ((bool)NextGateToHit)
		{
			Transform transform = m_Checkpoints[m_NextGateToHitIndex].transform;
			OnCheckpointPassed.Send(m_NextGateToHitIndex, transform.position, transform.rotation);
			m_NextGateToHitIndex++;
			bool flag = m_NextGateToHitIndex >= m_Checkpoints.Length;
			if (!flag && m_ChallengeData.TimeTicksDown)
			{
				float timeForCheckpoint = m_ChallengeData.GetTimeForCheckpoint(m_NextGateToHitIndex);
				if (m_ChallengeData.CarryOverTime)
				{
					m_ChallengeTimer.AddToCountdownTime(timeForCheckpoint);
				}
				else
				{
					m_ChallengeTimer.ResetTimeElapsed();
					m_ChallengeTimer.Start(timeForCheckpoint);
				}
			}
			for (int i = 0; i < m_Checkpoints.Length; i++)
			{
				int num = i - m_NextGateToHitIndex;
				m_Checkpoints[i].UpdateRelativeCheckpointIndex(num, m_Track.m_NumFutureGatesToShow);
			}
			DespawnAndUnregisterWaypoint();
			if (flag)
			{
				HandleFinalCheckpointReached();
			}
			else
			{
				m_NextGateDistance = CalculateProgressToNextGate();
			}
			ManSFX.MiscSfxType sfxType = (flag ? m_Track.completeSound : m_Track.checkPointSound);
			Singleton.Manager<ManSFX>.inst.PlayMiscSFX(sfxType, Singleton.cameraTrans.position);
		}
		else
		{
			d.LogError("TriggerNextCheckPointGate(): no next gate to hit");
		}
	}

	private void HandleFinalCheckpointReached()
	{
		float timeElapsed = m_ChallengeTimer.TimeElapsed;
		bool flag = timeElapsed < m_CurrentBest;
		if (flag)
		{
			m_CurrentBest = timeElapsed;
			m_MyHUD.SetBestTimeText(ConvertTimeToScoreString(timeElapsed));
		}
		EndChallenge(EndReason.Success, flag, timeElapsed);
	}

	private void SendBoundsEventIfAreaChanged(float distOutsideTrack, float distOutsideBounds)
	{
		BoundsArea boundsArea = ((distOutsideBounds > 0f) ? BoundsArea.Illegal : ((distOutsideTrack > 0f) ? BoundsArea.Caution : BoundsArea.Track));
		if (boundsArea != m_BoundsArea)
		{
			if (boundsArea == BoundsArea.Illegal)
			{
				d.LogFormat("CheckpointChallenge: off track at position={0}, distOutsideBounds={1}", base.Protagonist.boundsCentreWorld, distOutsideBounds);
			}
			if (boundsArea == BoundsArea.Illegal || (boundsArea == BoundsArea.Caution && m_BoundsArea == BoundsArea.Track))
			{
				OnBoundsWarning.Send(boundsArea);
			}
			m_BoundsArea = boundsArea;
		}
	}

	private void OnPlayerTankChanged(Tank tank, bool set)
	{
		if ((bool)tank && set)
		{
			m_FurthestDistanceReached = 0f;
		}
	}
}
