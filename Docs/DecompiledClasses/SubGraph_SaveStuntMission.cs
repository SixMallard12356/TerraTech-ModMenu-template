using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class SubGraph_SaveStuntMission : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool clearSceneryAlongTrack = true;

	[Multiline(3)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius = 50f;

	[Multiline(3)]
	public string EncounterCentrePosName = "";

	private bool local_AddedQuestLog_System_Boolean;

	private CheckpointChallenge.EndReason local_EndReason_CheckpointChallenge_EndReason;

	private bool local_GotRamp_System_Boolean;

	private bool local_OutOfBounds_System_Boolean;

	private int local_PassedCheckpointIdx_System_Int32;

	private bool local_SpawnedRamp_System_Boolean;

	private TrackSpline local_Spline_TrackSpline;

	private GameObject local_StartingRamp_UnityEngine_GameObject;

	private GameObject local_StartingRamp_UnityEngine_GameObject_previous;

	private Transform local_StartTransform_UnityEngine_Transform;

	private string local_targetChallengeID_System_String = "";

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public uScript_AddMessage.MessageData msgComplete;

	public uScript_AddMessage.MessageData msgOutOfBounds;

	public uScript_AddMessage.MessageData msgOutOfTime;

	public uScript_AddMessage.MessageData msgQuitFromMenu;

	[Multiline(3)]
	public string Spawned_Ramp_Name = "";

	public TerrainObject StuntRampPrefab;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_16;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_24;

	private GameObject owner_Connection_29;

	private GameObject owner_Connection_31;

	private GameObject owner_Connection_33;

	private GameObject owner_Connection_60;

	private GameObject owner_Connection_64;

	private GameObject owner_Connection_81;

	private GameObject owner_Connection_108;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_2;

	private bool logic_uScriptCon_CompareBool_True_2 = true;

	private bool logic_uScriptCon_CompareBool_False_2 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_4 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_4;

	private bool logic_uScriptAct_SetBool_Out_4 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_4 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_4 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_7;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_7 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_7 = "HasSpawnedRamp";

	private uScript_GetSpawnedTerrainObject logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_9 = new uScript_GetSpawnedTerrainObject();

	private GameObject logic_uScript_GetSpawnedTerrainObject_ownerNode_9;

	private string logic_uScript_GetSpawnedTerrainObject_uniqueName_9 = "";

	private GameObject logic_uScript_GetSpawnedTerrainObject_Return_9;

	private bool logic_uScript_GetSpawnedTerrainObject_Out_9 = true;

	private bool logic_uScript_GetSpawnedTerrainObject_ObjectRefInvalid_9 = true;

	private bool logic_uScript_GetSpawnedTerrainObject_CurrentlySpawned_9 = true;

	private bool logic_uScript_GetSpawnedTerrainObject_CurrentlyNotSpawned_9 = true;

	private uScript_SpawnTerrainObject logic_uScript_SpawnTerrainObject_uScript_SpawnTerrainObject_10 = new uScript_SpawnTerrainObject();

	private GameObject logic_uScript_SpawnTerrainObject_ownerNode_10;

	private TerrainObject logic_uScript_SpawnTerrainObject_terrainObjectPrefab_10;

	private string logic_uScript_SpawnTerrainObject_posName_10 = "";

	private string logic_uScript_SpawnTerrainObject_uniqueName_10 = "";

	private bool logic_uScript_SpawnTerrainObject_Out_10 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_20 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_20;

	private string logic_uScript_RemoveScenery_positionName_20 = "";

	private float logic_uScript_RemoveScenery_radius_20;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_20 = true;

	private bool logic_uScript_RemoveScenery_Out_20 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_26 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_26;

	private bool logic_uScript_FinishEncounter_Out_26 = true;

	private uScript_GetLastChallengeResult logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_27 = new uScript_GetLastChallengeResult();

	private bool logic_uScript_GetLastChallengeResult_Success_27 = true;

	private bool logic_uScript_GetLastChallengeResult_Failure_27 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_30 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_30;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_30 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_30 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_30 = true;

	private uScript_ShowQuestLog logic_uScript_ShowQuestLog_uScript_ShowQuestLog_32 = new uScript_ShowQuestLog();

	private GameObject logic_uScript_ShowQuestLog_owner_32;

	private bool logic_uScript_ShowQuestLog_Out_32 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_35 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_35;

	private bool logic_uScriptAct_SetBool_Out_35 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_35 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_35 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_36;

	private bool logic_uScriptCon_CompareBool_True_36 = true;

	private bool logic_uScriptCon_CompareBool_False_36 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_39;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_39 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_39 = "HasAddedQuestLog";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_40 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_40;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_40;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_40;

	private bool logic_uScript_AddMessage_Out_40 = true;

	private bool logic_uScript_AddMessage_Shown_40 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_44 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_44;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_44;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_44;

	private bool logic_uScript_AddMessage_Out_44 = true;

	private bool logic_uScript_AddMessage_Shown_44 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_46 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_46;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_46;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_46;

	private bool logic_uScript_AddMessage_Out_46 = true;

	private bool logic_uScript_AddMessage_Shown_46 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_50 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_50;

	private bool logic_uScriptAct_SetBool_Out_50 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_50 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_50 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_51 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_51;

	private bool logic_uScriptCon_CompareBool_True_51 = true;

	private bool logic_uScriptCon_CompareBool_False_51 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_54 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_54;

	private bool logic_uScriptAct_SetBool_Out_54 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_54 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_54 = true;

	private uScript_ClearSceneryAlongSpline logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_55 = new uScript_ClearSceneryAlongSpline();

	private Transform logic_uScript_ClearSceneryAlongSpline_splineStartTrans_55;

	private TrackSpline logic_uScript_ClearSceneryAlongSpline_spline_55;

	private float logic_uScript_ClearSceneryAlongSpline_delayBetweenAreaClears_55;

	private Transform logic_uScript_ClearSceneryAlongSpline_sceneryClearSFXPrefab_55;

	private float logic_uScript_ClearSceneryAlongSpline_stepSizeWidthPercentage_55 = 0.8f;

	private bool logic_uScript_ClearSceneryAlongSpline_clearUpToPenaltyWidth_55 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_Out_55 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_BusyClearing_55 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_DoneClearing_55 = true;

	private uScript_GetEncounterSpline logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_57 = new uScript_GetEncounterSpline();

	private GameObject logic_uScript_GetEncounterSpline_owner_57;

	private TrackSpline logic_uScript_GetEncounterSpline_Return_57;

	private bool logic_uScript_GetEncounterSpline_Out_57 = true;

	private uScript_InitChallengeStarterWithEncounterChallengeData logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_58 = new uScript_InitChallengeStarterWithEncounterChallengeData();

	private GameObject logic_uScript_InitChallengeStarterWithEncounterChallengeData_owner_58;

	private GameObject logic_uScript_InitChallengeStarterWithEncounterChallengeData_targetChallengeStarterObject_58;

	private bool logic_uScript_InitChallengeStarterWithEncounterChallengeData_Out_58 = true;

	private uScript_GetChallengeStartTransform logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_59 = new uScript_GetChallengeStartTransform();

	private GameObject logic_uScript_GetChallengeStartTransform_challengeStarterObject_59;

	private Transform logic_uScript_GetChallengeStartTransform_Return_59;

	private bool logic_uScript_GetChallengeStartTransform_Out_59 = true;

	private uScript_GetChallengeIDFromChallengeStarter logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_68 = new uScript_GetChallengeIDFromChallengeStarter();

	private GameObject logic_uScript_GetChallengeIDFromChallengeStarter_challengeStarterObject_68;

	private string logic_uScript_GetChallengeIDFromChallengeStarter_Return_68;

	private bool logic_uScript_GetChallengeIDFromChallengeStarter_Out_68 = true;

	private uScript_GetChallengeStateFromChallengeID logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_70 = new uScript_GetChallengeStateFromChallengeID();

	private string logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_70 = "";

	private bool logic_uScript_GetChallengeStateFromChallengeID_Out_70 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_NotRunning_70 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_JustStarted_70 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_InProgress_70 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_JustEnded_70 = true;

	private uScript_ClearChallengeStarterChallengeData logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_73 = new uScript_ClearChallengeStarterChallengeData();

	private GameObject logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_73;

	private bool logic_uScript_ClearChallengeStarterChallengeData_Out_73 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_74;

	private bool logic_uScriptCon_CompareBool_True_74 = true;

	private bool logic_uScriptCon_CompareBool_False_74 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_77 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_77;

	private bool logic_uScriptAct_SetBool_Out_77 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_77 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_77 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_83 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_83;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_83 = CheckpointChallenge.EndReason.FailedOutOfBounds;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_83 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_83 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_86 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_86;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_86;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_86;

	private bool logic_uScript_AddMessage_Out_86 = true;

	private bool logic_uScript_AddMessage_Shown_86 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_88 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_88;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_88 = CheckpointChallenge.EndReason.FailedTimeUp;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_88 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_88 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_92 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_92;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_92;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_92;

	private bool logic_uScript_AddMessage_Out_92 = true;

	private bool logic_uScript_AddMessage_Shown_92 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_94 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_94;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_94 = CheckpointChallenge.EndReason.FailedQuit;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_94 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_94 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_97 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_97;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_97;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_97;

	private bool logic_uScript_AddMessage_Out_97 = true;

	private bool logic_uScript_AddMessage_Shown_97 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_102 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_102;

	private bool logic_uScriptAct_SetBool_Out_102 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_102 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_102 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_104 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_104 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_104 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_105 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_105;

	private bool logic_uScriptCon_CompareBool_True_105 = true;

	private bool logic_uScriptCon_CompareBool_False_105 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_107 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_107;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_107 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_109 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_109;

	private bool logic_uScriptAct_SetBool_Out_109 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_109 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_109 = true;

	private int event_UnityEngine_GameObject_CheckpointIndex_25;

	private CheckpointChallenge.EndReason event_UnityEngine_GameObject_EndReason_80;

	private float event_UnityEngine_GameObject_EndTime_80;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
			if (null != owner_Connection_3)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_3.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_3.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_1;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_1;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_1;
				}
			}
		}
		if (null == owner_Connection_5 || !m_RegisteredForEvents)
		{
			owner_Connection_5 = parentGameObject;
			if (null != owner_Connection_5)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_5.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_5.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
				}
			}
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
		}
		if (null == owner_Connection_16 || !m_RegisteredForEvents)
		{
			owner_Connection_16 = parentGameObject;
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
		}
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
			if (null != owner_Connection_22)
			{
				uScript_BoundsWarningEvent uScript_BoundsWarningEvent2 = owner_Connection_22.GetComponent<uScript_BoundsWarningEvent>();
				if (null == uScript_BoundsWarningEvent2)
				{
					uScript_BoundsWarningEvent2 = owner_Connection_22.AddComponent<uScript_BoundsWarningEvent>();
				}
				if (null != uScript_BoundsWarningEvent2)
				{
					uScript_BoundsWarningEvent2.OnBoundsWarningCaution += Instance_OnBoundsWarningCaution_23;
					uScript_BoundsWarningEvent2.OnBoundsWarningIllegal += Instance_OnBoundsWarningIllegal_23;
				}
			}
		}
		if (null == owner_Connection_24 || !m_RegisteredForEvents)
		{
			owner_Connection_24 = parentGameObject;
			if (null != owner_Connection_24)
			{
				uScript_CheckPointPassedEvent uScript_CheckPointPassedEvent2 = owner_Connection_24.GetComponent<uScript_CheckPointPassedEvent>();
				if (null == uScript_CheckPointPassedEvent2)
				{
					uScript_CheckPointPassedEvent2 = owner_Connection_24.AddComponent<uScript_CheckPointPassedEvent>();
				}
				if (null != uScript_CheckPointPassedEvent2)
				{
					uScript_CheckPointPassedEvent2.OnCheckPointPassed += Instance_OnCheckPointPassed_25;
				}
			}
		}
		if (null == owner_Connection_29 || !m_RegisteredForEvents)
		{
			owner_Connection_29 = parentGameObject;
		}
		if (null == owner_Connection_31 || !m_RegisteredForEvents)
		{
			owner_Connection_31 = parentGameObject;
		}
		if (null == owner_Connection_33 || !m_RegisteredForEvents)
		{
			owner_Connection_33 = parentGameObject;
		}
		if (null == owner_Connection_60 || !m_RegisteredForEvents)
		{
			owner_Connection_60 = parentGameObject;
		}
		if (null == owner_Connection_64 || !m_RegisteredForEvents)
		{
			owner_Connection_64 = parentGameObject;
		}
		if (null == owner_Connection_81 || !m_RegisteredForEvents)
		{
			owner_Connection_81 = parentGameObject;
			if (null != owner_Connection_81)
			{
				uScript_CheckPointChallengeEndedEvent uScript_CheckPointChallengeEndedEvent2 = owner_Connection_81.GetComponent<uScript_CheckPointChallengeEndedEvent>();
				if (null == uScript_CheckPointChallengeEndedEvent2)
				{
					uScript_CheckPointChallengeEndedEvent2 = owner_Connection_81.AddComponent<uScript_CheckPointChallengeEndedEvent>();
				}
				if (null != uScript_CheckPointChallengeEndedEvent2)
				{
					uScript_CheckPointChallengeEndedEvent2.OnSuccess += Instance_OnSuccess_80;
					uScript_CheckPointChallengeEndedEvent2.OnFail += Instance_OnFail_80;
				}
			}
		}
		if (null == owner_Connection_108 || !m_RegisteredForEvents)
		{
			owner_Connection_108 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		if (!m_RegisteredForEvents && null != owner_Connection_3)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_3.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_3.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_1;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_1;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_1;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_5)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_5.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_5.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_22)
		{
			uScript_BoundsWarningEvent uScript_BoundsWarningEvent2 = owner_Connection_22.GetComponent<uScript_BoundsWarningEvent>();
			if (null == uScript_BoundsWarningEvent2)
			{
				uScript_BoundsWarningEvent2 = owner_Connection_22.AddComponent<uScript_BoundsWarningEvent>();
			}
			if (null != uScript_BoundsWarningEvent2)
			{
				uScript_BoundsWarningEvent2.OnBoundsWarningCaution += Instance_OnBoundsWarningCaution_23;
				uScript_BoundsWarningEvent2.OnBoundsWarningIllegal += Instance_OnBoundsWarningIllegal_23;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_24)
		{
			uScript_CheckPointPassedEvent uScript_CheckPointPassedEvent2 = owner_Connection_24.GetComponent<uScript_CheckPointPassedEvent>();
			if (null == uScript_CheckPointPassedEvent2)
			{
				uScript_CheckPointPassedEvent2 = owner_Connection_24.AddComponent<uScript_CheckPointPassedEvent>();
			}
			if (null != uScript_CheckPointPassedEvent2)
			{
				uScript_CheckPointPassedEvent2.OnCheckPointPassed += Instance_OnCheckPointPassed_25;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_81)
		{
			uScript_CheckPointChallengeEndedEvent uScript_CheckPointChallengeEndedEvent2 = owner_Connection_81.GetComponent<uScript_CheckPointChallengeEndedEvent>();
			if (null == uScript_CheckPointChallengeEndedEvent2)
			{
				uScript_CheckPointChallengeEndedEvent2 = owner_Connection_81.AddComponent<uScript_CheckPointChallengeEndedEvent>();
			}
			if (null != uScript_CheckPointChallengeEndedEvent2)
			{
				uScript_CheckPointChallengeEndedEvent2.OnSuccess += Instance_OnSuccess_80;
				uScript_CheckPointChallengeEndedEvent2.OnFail += Instance_OnFail_80;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_3)
		{
			uScript_SaveLoad component = owner_Connection_3.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_1;
				component.LoadEvent -= Instance_LoadEvent_1;
				component.RestartEvent -= Instance_RestartEvent_1;
			}
		}
		if (null != owner_Connection_5)
		{
			uScript_EncounterUpdate component2 = owner_Connection_5.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_0;
				component2.OnSuspend -= Instance_OnSuspend_0;
				component2.OnResume -= Instance_OnResume_0;
			}
		}
		if (null != owner_Connection_22)
		{
			uScript_BoundsWarningEvent component3 = owner_Connection_22.GetComponent<uScript_BoundsWarningEvent>();
			if (null != component3)
			{
				component3.OnBoundsWarningCaution -= Instance_OnBoundsWarningCaution_23;
				component3.OnBoundsWarningIllegal -= Instance_OnBoundsWarningIllegal_23;
			}
		}
		if (null != owner_Connection_24)
		{
			uScript_CheckPointPassedEvent component4 = owner_Connection_24.GetComponent<uScript_CheckPointPassedEvent>();
			if (null != component4)
			{
				component4.OnCheckPointPassed -= Instance_OnCheckPointPassed_25;
			}
		}
		if (null != owner_Connection_81)
		{
			uScript_CheckPointChallengeEndedEvent component5 = owner_Connection_81.GetComponent<uScript_CheckPointChallengeEndedEvent>();
			if (null != component5)
			{
				component5.OnSuccess -= Instance_OnSuccess_80;
				component5.OnFail -= Instance_OnFail_80;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.SetParent(g);
		logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_9.SetParent(g);
		logic_uScript_SpawnTerrainObject_uScript_SpawnTerrainObject_10.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_20.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_26.SetParent(g);
		logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_27.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_30.SetParent(g);
		logic_uScript_ShowQuestLog_uScript_ShowQuestLog_32.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_35.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_40.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_44.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_46.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_51.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_54.SetParent(g);
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_55.SetParent(g);
		logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_57.SetParent(g);
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_58.SetParent(g);
		logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_59.SetParent(g);
		logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_68.SetParent(g);
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_70.SetParent(g);
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_73.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_77.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_83.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_86.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_88.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_92.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_94.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_97.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_104.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_105.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_107.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.SetParent(g);
		owner_Connection_3 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_16 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_24 = parentGameObject;
		owner_Connection_29 = parentGameObject;
		owner_Connection_31 = parentGameObject;
		owner_Connection_33 = parentGameObject;
		owner_Connection_60 = parentGameObject;
		owner_Connection_64 = parentGameObject;
		owner_Connection_81 = parentGameObject;
		owner_Connection_108 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Save_Out += SubGraph_SaveLoadBool_Save_Out_39;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Load_Out += SubGraph_SaveLoadBool_Load_Out_39;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_39;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_107.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDisable();
		logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_9.OnDisable();
		logic_uScript_SpawnTerrainObject_uScript_SpawnTerrainObject_10.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_30.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_40.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_44.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_46.OnDisable();
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_55.OnDisable();
		logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_57.OnDisable();
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_58.OnDisable();
		logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_59.OnDisable();
		logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_68.OnDisable();
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_70.OnDisable();
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_73.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_86.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_92.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_97.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Save_Out -= SubGraph_SaveLoadBool_Save_Out_39;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Load_Out -= SubGraph_SaveLoadBool_Load_Out_39;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_39;
	}

	private void Instance_OnUpdate_0(object o, EventArgs e)
	{
		Relay_OnUpdate_0();
	}

	private void Instance_OnSuspend_0(object o, EventArgs e)
	{
		Relay_OnSuspend_0();
	}

	private void Instance_OnResume_0(object o, EventArgs e)
	{
		Relay_OnResume_0();
	}

	private void Instance_SaveEvent_1(object o, EventArgs e)
	{
		Relay_SaveEvent_1();
	}

	private void Instance_LoadEvent_1(object o, EventArgs e)
	{
		Relay_LoadEvent_1();
	}

	private void Instance_RestartEvent_1(object o, EventArgs e)
	{
		Relay_RestartEvent_1();
	}

	private void Instance_OnBoundsWarningCaution_23(object o, EventArgs e)
	{
		Relay_OnBoundsWarningCaution_23();
	}

	private void Instance_OnBoundsWarningIllegal_23(object o, EventArgs e)
	{
		Relay_OnBoundsWarningIllegal_23();
	}

	private void Instance_OnCheckPointPassed_25(object o, uScript_CheckPointPassedEvent.CheckpointPassedEventArgs e)
	{
		event_UnityEngine_GameObject_CheckpointIndex_25 = e.CheckpointIndex;
		Relay_OnCheckPointPassed_25();
	}

	private void Instance_OnSuccess_80(object o, uScript_CheckPointChallengeEndedEvent.CheckpointChallengeEndedEventArgs e)
	{
		event_UnityEngine_GameObject_EndReason_80 = e.EndReason;
		event_UnityEngine_GameObject_EndTime_80 = e.EndTime;
		Relay_OnSuccess_80();
	}

	private void Instance_OnFail_80(object o, uScript_CheckPointChallengeEndedEvent.CheckpointChallengeEndedEventArgs e)
	{
		event_UnityEngine_GameObject_EndReason_80 = e.EndReason;
		event_UnityEngine_GameObject_EndTime_80 = e.EndTime;
		Relay_OnFail_80();
	}

	private void SubGraph_SaveLoadBool_Save_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_SpawnedRamp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Save_Out_7();
	}

	private void SubGraph_SaveLoadBool_Load_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_SpawnedRamp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Load_Out_7();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_SpawnedRamp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Restart_Out_7();
	}

	private void SubGraph_SaveLoadBool_Save_Out_39(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_39 = e.boolean;
		local_AddedQuestLog_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_39;
		Relay_Save_Out_39();
	}

	private void SubGraph_SaveLoadBool_Load_Out_39(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_39 = e.boolean;
		local_AddedQuestLog_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_39;
		Relay_Load_Out_39();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_39(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_39 = e.boolean;
		local_AddedQuestLog_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_39;
		Relay_Restart_Out_39();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_2();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_SaveEvent_1()
	{
		Relay_Save_7();
	}

	private void Relay_LoadEvent_1()
	{
		Relay_Load_7();
	}

	private void Relay_RestartEvent_1()
	{
		Relay_Set_False_7();
	}

	private void Relay_In_2()
	{
		logic_uScriptCon_CompareBool_Bool_2 = local_SpawnedRamp_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.In(logic_uScriptCon_CompareBool_Bool_2);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.False;
		if (num)
		{
			Relay_In_20();
		}
		if (flag)
		{
			Relay_True_4();
		}
	}

	private void Relay_True_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.True(out logic_uScriptAct_SetBool_Target_4);
		local_SpawnedRamp_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_False_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.False(out logic_uScriptAct_SetBool_Target_4);
		local_SpawnedRamp_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_Save_Out_7()
	{
		Relay_Save_39();
	}

	private void Relay_Load_Out_7()
	{
		Relay_Load_39();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_Set_False_39();
	}

	private void Relay_Save_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_SpawnedRamp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_SpawnedRamp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Load_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_SpawnedRamp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_SpawnedRamp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Set_True_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_SpawnedRamp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_SpawnedRamp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Set_False_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_SpawnedRamp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_SpawnedRamp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_In_9()
	{
		logic_uScript_GetSpawnedTerrainObject_ownerNode_9 = owner_Connection_16;
		logic_uScript_GetSpawnedTerrainObject_uniqueName_9 = Spawned_Ramp_Name;
		logic_uScript_GetSpawnedTerrainObject_Return_9 = logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_9.In(logic_uScript_GetSpawnedTerrainObject_ownerNode_9, logic_uScript_GetSpawnedTerrainObject_uniqueName_9);
		local_StartingRamp_UnityEngine_GameObject = logic_uScript_GetSpawnedTerrainObject_Return_9;
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		bool objectRefInvalid = logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_9.ObjectRefInvalid;
		bool currentlySpawned = logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_9.CurrentlySpawned;
		if (objectRefInvalid)
		{
			Relay_False_109();
		}
		if (currentlySpawned)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_SpawnTerrainObject_ownerNode_10 = owner_Connection_11;
		logic_uScript_SpawnTerrainObject_terrainObjectPrefab_10 = StuntRampPrefab;
		logic_uScript_SpawnTerrainObject_posName_10 = EncounterCentrePosName;
		logic_uScript_SpawnTerrainObject_uniqueName_10 = Spawned_Ramp_Name;
		logic_uScript_SpawnTerrainObject_uScript_SpawnTerrainObject_10.In(logic_uScript_SpawnTerrainObject_ownerNode_10, logic_uScript_SpawnTerrainObject_terrainObjectPrefab_10, logic_uScript_SpawnTerrainObject_posName_10, logic_uScript_SpawnTerrainObject_uniqueName_10);
		if (logic_uScript_SpawnTerrainObject_uScript_SpawnTerrainObject_10.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_RemoveScenery_ownerNode_20 = owner_Connection_19;
		logic_uScript_RemoveScenery_positionName_20 = clearSceneryPos;
		logic_uScript_RemoveScenery_radius_20 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_20.In(logic_uScript_RemoveScenery_ownerNode_20, logic_uScript_RemoveScenery_positionName_20, logic_uScript_RemoveScenery_radius_20, logic_uScript_RemoveScenery_preventChunksSpawning_20);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_20.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_OnBoundsWarningCaution_23()
	{
	}

	private void Relay_OnBoundsWarningIllegal_23()
	{
	}

	private void Relay_OnCheckPointPassed_25()
	{
		local_PassedCheckpointIdx_System_Int32 = event_UnityEngine_GameObject_CheckpointIndex_25;
	}

	private void Relay_Succeed_26()
	{
		logic_uScript_FinishEncounter_owner_26 = owner_Connection_29;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_26.Succeed(logic_uScript_FinishEncounter_owner_26);
	}

	private void Relay_Fail_26()
	{
		logic_uScript_FinishEncounter_owner_26 = owner_Connection_29;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_26.Fail(logic_uScript_FinishEncounter_owner_26);
	}

	private void Relay_In_27()
	{
		logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_27.In();
		bool success = logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_27.Success;
		bool failure = logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_27.Failure;
		if (success)
		{
			Relay_In_44();
		}
		if (failure)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_30()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_30 = owner_Connection_31;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_30.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_30);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_30.True)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_32()
	{
		logic_uScript_ShowQuestLog_owner_32 = owner_Connection_33;
		logic_uScript_ShowQuestLog_uScript_ShowQuestLog_32.In(logic_uScript_ShowQuestLog_owner_32);
		if (logic_uScript_ShowQuestLog_uScript_ShowQuestLog_32.Out)
		{
			Relay_True_35();
		}
	}

	private void Relay_True_35()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_35.True(out logic_uScriptAct_SetBool_Target_35);
		local_AddedQuestLog_System_Boolean = logic_uScriptAct_SetBool_Target_35;
	}

	private void Relay_False_35()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_35.False(out logic_uScriptAct_SetBool_Target_35);
		local_AddedQuestLog_System_Boolean = logic_uScriptAct_SetBool_Target_35;
	}

	private void Relay_In_36()
	{
		logic_uScriptCon_CompareBool_Bool_36 = local_AddedQuestLog_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.In(logic_uScriptCon_CompareBool_Bool_36);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.False;
		if (num)
		{
			Relay_In_70();
		}
		if (flag)
		{
			Relay_In_30();
		}
	}

	private void Relay_Save_Out_39()
	{
	}

	private void Relay_Load_Out_39()
	{
		Relay_False_102();
	}

	private void Relay_Restart_Out_39()
	{
		Relay_False_102();
	}

	private void Relay_Save_39()
	{
		logic_SubGraph_SaveLoadBool_boolean_39 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_39 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Save(ref logic_SubGraph_SaveLoadBool_boolean_39, logic_SubGraph_SaveLoadBool_boolAsVariable_39, logic_SubGraph_SaveLoadBool_uniqueID_39);
	}

	private void Relay_Load_39()
	{
		logic_SubGraph_SaveLoadBool_boolean_39 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_39 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Load(ref logic_SubGraph_SaveLoadBool_boolean_39, logic_SubGraph_SaveLoadBool_boolAsVariable_39, logic_SubGraph_SaveLoadBool_uniqueID_39);
	}

	private void Relay_Set_True_39()
	{
		logic_SubGraph_SaveLoadBool_boolean_39 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_39 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_39, logic_SubGraph_SaveLoadBool_boolAsVariable_39, logic_SubGraph_SaveLoadBool_uniqueID_39);
	}

	private void Relay_Set_False_39()
	{
		logic_SubGraph_SaveLoadBool_boolean_39 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_39 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_39.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_39, logic_SubGraph_SaveLoadBool_boolAsVariable_39, logic_SubGraph_SaveLoadBool_uniqueID_39);
	}

	private void Relay_In_40()
	{
		logic_uScript_AddMessage_messageData_40 = msgOutOfBounds;
		logic_uScript_AddMessage_speaker_40 = messageSpeaker;
		logic_uScript_AddMessage_Return_40 = logic_uScript_AddMessage_uScript_AddMessage_40.In(logic_uScript_AddMessage_messageData_40, logic_uScript_AddMessage_speaker_40);
	}

	private void Relay_In_44()
	{
		logic_uScript_AddMessage_messageData_44 = msgComplete;
		logic_uScript_AddMessage_speaker_44 = messageSpeaker;
		logic_uScript_AddMessage_Return_44 = logic_uScript_AddMessage_uScript_AddMessage_44.In(logic_uScript_AddMessage_messageData_44, logic_uScript_AddMessage_speaker_44);
		if (logic_uScript_AddMessage_uScript_AddMessage_44.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_46()
	{
		logic_uScript_AddMessage_messageData_46 = msgOutOfTime;
		logic_uScript_AddMessage_speaker_46 = messageSpeaker;
		logic_uScript_AddMessage_Return_46 = logic_uScript_AddMessage_uScript_AddMessage_46.In(logic_uScript_AddMessage_messageData_46, logic_uScript_AddMessage_speaker_46);
	}

	private void Relay_True_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.True(out logic_uScriptAct_SetBool_Target_50);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_50;
	}

	private void Relay_False_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.False(out logic_uScriptAct_SetBool_Target_50);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_50;
	}

	private void Relay_In_51()
	{
		logic_uScriptCon_CompareBool_Bool_51 = local_OutOfBounds_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_51.In(logic_uScriptCon_CompareBool_Bool_51);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_51.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_51.False;
		if (num)
		{
			Relay_In_40();
		}
		if (flag)
		{
			Relay_In_46();
		}
	}

	private void Relay_True_54()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_54.True(out logic_uScriptAct_SetBool_Target_54);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_54;
	}

	private void Relay_False_54()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_54.False(out logic_uScriptAct_SetBool_Target_54);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_54;
	}

	private void Relay_In_55()
	{
		logic_uScript_ClearSceneryAlongSpline_splineStartTrans_55 = local_StartTransform_UnityEngine_Transform;
		logic_uScript_ClearSceneryAlongSpline_spline_55 = local_Spline_TrackSpline;
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_55.In(logic_uScript_ClearSceneryAlongSpline_splineStartTrans_55, logic_uScript_ClearSceneryAlongSpline_spline_55, logic_uScript_ClearSceneryAlongSpline_delayBetweenAreaClears_55, logic_uScript_ClearSceneryAlongSpline_sceneryClearSFXPrefab_55, logic_uScript_ClearSceneryAlongSpline_stepSizeWidthPercentage_55, logic_uScript_ClearSceneryAlongSpline_clearUpToPenaltyWidth_55);
	}

	private void Relay_In_57()
	{
		logic_uScript_GetEncounterSpline_owner_57 = owner_Connection_60;
		logic_uScript_GetEncounterSpline_Return_57 = logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_57.In(logic_uScript_GetEncounterSpline_owner_57);
		local_Spline_TrackSpline = logic_uScript_GetEncounterSpline_Return_57;
		if (logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_57.Out)
		{
			Relay_True_77();
		}
	}

	private void Relay_In_58()
	{
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_owner_58 = owner_Connection_64;
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_targetChallengeStarterObject_58 = local_StartingRamp_UnityEngine_GameObject;
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_58.In(logic_uScript_InitChallengeStarterWithEncounterChallengeData_owner_58, logic_uScript_InitChallengeStarterWithEncounterChallengeData_targetChallengeStarterObject_58);
		if (logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_58.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_In_59()
	{
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		logic_uScript_GetChallengeStartTransform_challengeStarterObject_59 = local_StartingRamp_UnityEngine_GameObject;
		logic_uScript_GetChallengeStartTransform_Return_59 = logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_59.In(logic_uScript_GetChallengeStartTransform_challengeStarterObject_59);
		local_StartTransform_UnityEngine_Transform = logic_uScript_GetChallengeStartTransform_Return_59;
		if (logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_59.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_68()
	{
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		logic_uScript_GetChallengeIDFromChallengeStarter_challengeStarterObject_68 = local_StartingRamp_UnityEngine_GameObject;
		logic_uScript_GetChallengeIDFromChallengeStarter_Return_68 = logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_68.In(logic_uScript_GetChallengeIDFromChallengeStarter_challengeStarterObject_68);
		local_targetChallengeID_System_String = logic_uScript_GetChallengeIDFromChallengeStarter_Return_68;
		if (logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_68.Out)
		{
			Relay_In_59();
		}
	}

	private void Relay_In_70()
	{
		logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_70 = local_targetChallengeID_System_String;
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_70.In(logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_70);
		bool notRunning = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_70.NotRunning;
		bool justStarted = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_70.JustStarted;
		bool inProgress = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_70.InProgress;
		bool justEnded = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_70.JustEnded;
		if (notRunning)
		{
			Relay_In_105();
		}
		if (justStarted)
		{
			Relay_False_54();
		}
		if (inProgress)
		{
			Relay_In_105();
		}
		if (justEnded)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_73()
	{
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_73 = local_StartingRamp_UnityEngine_GameObject;
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_73.In(logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_73);
		if (logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_73.Out)
		{
			Relay_Succeed_26();
		}
	}

	private void Relay_In_74()
	{
		logic_uScriptCon_CompareBool_Bool_74 = local_GotRamp_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.In(logic_uScriptCon_CompareBool_Bool_74);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.False;
		if (num)
		{
			Relay_In_36();
		}
		if (flag)
		{
			Relay_In_9();
		}
	}

	private void Relay_True_77()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_77.True(out logic_uScriptAct_SetBool_Target_77);
		local_GotRamp_System_Boolean = logic_uScriptAct_SetBool_Target_77;
	}

	private void Relay_False_77()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_77.False(out logic_uScriptAct_SetBool_Target_77);
		local_GotRamp_System_Boolean = logic_uScriptAct_SetBool_Target_77;
	}

	private void Relay_OnSuccess_80()
	{
		local_EndReason_CheckpointChallenge_EndReason = event_UnityEngine_GameObject_EndReason_80;
	}

	private void Relay_OnFail_80()
	{
		local_EndReason_CheckpointChallenge_EndReason = event_UnityEngine_GameObject_EndReason_80;
	}

	private void Relay_In_83()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_83 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_83.In(logic_uScript_CompareCheckpointChallengeEndReason_result_83, logic_uScript_CompareCheckpointChallengeEndReason_expected_83);
		bool equalTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_83.EqualTo;
		bool notEqualTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_83.NotEqualTo;
		if (equalTo)
		{
			Relay_In_86();
		}
		if (notEqualTo)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_86()
	{
		logic_uScript_AddMessage_messageData_86 = msgOutOfBounds;
		logic_uScript_AddMessage_speaker_86 = messageSpeaker;
		logic_uScript_AddMessage_Return_86 = logic_uScript_AddMessage_uScript_AddMessage_86.In(logic_uScript_AddMessage_messageData_86, logic_uScript_AddMessage_speaker_86);
	}

	private void Relay_In_88()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_88 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_88.In(logic_uScript_CompareCheckpointChallengeEndReason_result_88, logic_uScript_CompareCheckpointChallengeEndReason_expected_88);
		bool equalTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_88.EqualTo;
		bool notEqualTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_88.NotEqualTo;
		if (equalTo)
		{
			Relay_In_92();
		}
		if (notEqualTo)
		{
			Relay_In_94();
		}
	}

	private void Relay_In_92()
	{
		logic_uScript_AddMessage_messageData_92 = msgOutOfTime;
		logic_uScript_AddMessage_speaker_92 = messageSpeaker;
		logic_uScript_AddMessage_Return_92 = logic_uScript_AddMessage_uScript_AddMessage_92.In(logic_uScript_AddMessage_messageData_92, logic_uScript_AddMessage_speaker_92);
	}

	private void Relay_In_94()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_94 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_94.In(logic_uScript_CompareCheckpointChallengeEndReason_result_94, logic_uScript_CompareCheckpointChallengeEndReason_expected_94);
		if (logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_94.EqualTo)
		{
			Relay_In_97();
		}
	}

	private void Relay_In_97()
	{
		logic_uScript_AddMessage_messageData_97 = msgQuitFromMenu;
		logic_uScript_AddMessage_speaker_97 = messageSpeaker;
		logic_uScript_AddMessage_Return_97 = logic_uScript_AddMessage_uScript_AddMessage_97.In(logic_uScript_AddMessage_messageData_97, logic_uScript_AddMessage_speaker_97);
	}

	private void Relay_True_102()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.True(out logic_uScriptAct_SetBool_Target_102);
		local_GotRamp_System_Boolean = logic_uScriptAct_SetBool_Target_102;
	}

	private void Relay_False_102()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.False(out logic_uScriptAct_SetBool_Target_102);
		local_GotRamp_System_Boolean = logic_uScriptAct_SetBool_Target_102;
	}

	private void Relay_In_104()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_104 = EncounterCentrePosName;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_104.In(logic_uScript_SetEncounterTargetPosition_positionName_104);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_104.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_105()
	{
		logic_uScriptCon_CompareBool_Bool_105 = clearSceneryAlongTrack;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_105.In(logic_uScriptCon_CompareBool_Bool_105);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_105.True)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_107()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_107 = owner_Connection_108;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_107.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_107);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_107.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_True_109()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.True(out logic_uScriptAct_SetBool_Target_109);
		local_SpawnedRamp_System_Boolean = logic_uScriptAct_SetBool_Target_109;
	}

	private void Relay_False_109()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.False(out logic_uScriptAct_SetBool_Target_109);
		local_SpawnedRamp_System_Boolean = logic_uScriptAct_SetBool_Target_109;
	}
}
