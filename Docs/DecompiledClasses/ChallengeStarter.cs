#define UNITY_EDITOR
using UnityEngine;

public class ChallengeStarter : MonoBehaviour
{
	[SerializeField]
	[Header("Challenge params")]
	private ChallengeData m_ChallengeData;

	[SerializeField]
	[Tooltip("Whether the positions of waypoints are placed relative to the ground (i.e. project to ground)")]
	private bool m_CheckpointsYPositionsRelativeToGround;

	[SerializeField]
	[Tooltip("Whether to rotate the Up direction of a gate to naturally follow the curve of the track. World Up if false")]
	private bool m_SmoothGateUpDir;

	[SerializeField]
	private Transform m_ChallengeStartTransform;

	[SerializeField]
	private bool m_ShowCheckpointChallengeHUD;

	[SerializeField]
	private ManChallenge.BuildModeType m_BuildModeType;

	[SerializeField]
	private ManChallenge.StartModeType m_StartModeType = ManChallenge.StartModeType.Immediate;

	[SerializeField]
	private bool m_EndChallengeWhenPlayerDies = true;

	[SerializeField]
	private bool m_ExitOnOutOfBounds = true;

	[SerializeField]
	private bool m_DisplaysCustomPauseMenu = true;

	private Checkpoint m_StartingCheckpoint;

	private ChallengeData m_ActiveChallengeData;

	private string m_UniqueChallengeID;

	public Transform ChallengeStartTransform
	{
		get
		{
			if (!(m_ChallengeStartTransform != null))
			{
				return base.transform;
			}
			return m_ChallengeStartTransform;
		}
	}

	private ChallengeData ChallengeData => m_ActiveChallengeData ?? m_ChallengeData;

	public void SetChallengeData(ChallengeData challengeData)
	{
		m_ActiveChallengeData = challengeData;
		m_UniqueChallengeID = null;
	}

	public void ClearChallengeData()
	{
		SetChallengeData(null);
	}

	public string GetUniqueChallengeID()
	{
		if (m_UniqueChallengeID.NullOrEmpty())
		{
			ManChallenge.InitParams challengeInitParams = GetChallengeInitParams();
			m_UniqueChallengeID = ManChallenge.GetUniqueChallengeID(challengeInitParams);
			d.Assert(!m_UniqueChallengeID.NullOrEmpty(), "ChallengeStarter.GetUniqueChallengeID - Failed to get valid Challenge ID for " + base.gameObject.name);
		}
		return m_UniqueChallengeID;
	}

	private void StartChallenge()
	{
		if (ChallengeData != null)
		{
			ManChallenge.InitParams challengeInitParams = GetChallengeInitParams();
			Singleton.Manager<ManChallenge>.inst.SetupChallenge(challengeInitParams);
			if (!Singleton.Manager<ManChallenge>.inst.IsChallengeRunning)
			{
				d.LogError("ChallengeStarter.StartChallenge - Tried to start challenge from ChallengeStarter '" + base.gameObject.name + "', but after starting no challenge was found to be running!?");
			}
		}
	}

	private ManChallenge.InitParams GetChallengeInitParams()
	{
		ManChallenge.InitParams initParams = new ManChallenge.InitParams();
		initParams.data = ChallengeData;
		initParams.placementInfo = default(Challenge.PlacementInfo);
		initParams.placementInfo.yPositionsRelativeToGround = m_CheckpointsYPositionsRelativeToGround;
		initParams.placementInfo.smoothGateUpDir = m_SmoothGateUpDir;
		initParams.placementInfo.SetSpawnLocation(WorldPosition.FromScenePosition(ChallengeStartTransform.position), ChallengeStartTransform.rotation);
		initParams.placementInfo.ShowHUD = m_ShowCheckpointChallengeHUD;
		initParams.placementInfo.ReplayMode = false;
		initParams.buildModeType = m_BuildModeType;
		initParams.startModeType = m_StartModeType;
		initParams.endChallengeWhenPlayerDies = m_EndChallengeWhenPlayerDies;
		initParams.exitOnOutOfBounds = m_ExitOnOutOfBounds;
		initParams.displaysPauseMenu = m_DisplaysCustomPauseMenu;
		return initParams;
	}

	private void OnStartingCheckpointTriggered(Checkpoint checkpoint, Tank tank)
	{
		if (!(tank == Singleton.playerTank))
		{
			return;
		}
		if (Singleton.Manager<ManChallenge>.inst.IsChallengeRunning)
		{
			if (!Singleton.Manager<ManChallenge>.inst.IsShowingResults())
			{
				return;
			}
			Singleton.Manager<ManChallenge>.inst.KillChallenge();
		}
		StartChallenge();
	}

	private void OnSpawn()
	{
		Checkpoint[] componentsInChildren = GetComponentsInChildren<Checkpoint>();
		d.Assert(componentsInChildren.Length == 1, "ChallengeStarter.OnSpawn - Expected exactly 1 Checkpoint in children to function as challenge starting gate. Found " + componentsInChildren.Length);
		m_StartingCheckpoint = componentsInChildren[0];
		m_StartingCheckpoint.OnCheckpointPassed.Subscribe(OnStartingCheckpointTriggered);
	}

	private void OnRecycle()
	{
		m_StartingCheckpoint.OnCheckpointPassed.Unsubscribe(OnStartingCheckpointTriggered);
		m_StartingCheckpoint = null;
		m_UniqueChallengeID = null;
		m_ActiveChallengeData = null;
	}
}
