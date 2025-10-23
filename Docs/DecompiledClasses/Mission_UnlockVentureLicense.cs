using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_UnlockVentureLicense : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public float clearSceneryRadius = 50f;

	public float distLeavingMission;

	public float distNearNPC;

	public int halfwayCheckpointIndex;

	private GameHints.HintID local_169_GameHints_HintID = GameHints.HintID.RaceFailedOutOfTime;

	private Tank[] local_73_TankArray = new Tank[0];

	private bool local_ChallengeEnded_System_Boolean;

	private bool local_ChallengeInProgess_System_Boolean;

	private CheckpointChallenge.EndReason local_EndReason_CheckpointChallenge_EndReason;

	private bool local_Init_System_Boolean;

	private bool local_MissionComplete_System_Boolean;

	private bool local_MsgHalfwayRound_System_Boolean;

	private bool local_MsgIntro_System_Boolean;

	private bool local_MsgNPCFound_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgStartRace_ManOnScreenMessages_OnScreenMessage;

	private Tank local_NPCTech_Tank;

	private bool local_OutOfBounds_System_Boolean;

	private int local_PassedCheckpointIdx_System_Int32;

	private TrackSpline local_Spline_TrackSpline;

	private int local_Stage_System_Int32 = 1;

	private GameObject local_StartingRamp_UnityEngine_GameObject;

	private GameObject local_StartingRamp_UnityEngine_GameObject_previous;

	private Transform local_StartTransform_UnityEngine_Transform;

	private string local_TargetChallengeID_System_String = "";

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msgComplete;

	public uScript_AddMessage.MessageData msgHalfwayRound;

	public uScript_AddMessage.MessageData msgIntro;

	public uScript_AddMessage.MessageData msgNPCFound;

	public uScript_AddMessage.MessageData msgOutOfBounds;

	public uScript_AddMessage.MessageData msgOutOfTime;

	public uScript_AddMessage.MessageData msgQuitFromMenu;

	public uScript_AddMessage.MessageData msgRaceStarted;

	public uScript_AddMessage.MessageData msgRaceStartedEarly;

	public uScript_AddMessage.MessageData msgStartRace;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	[Multiline(3)]
	public string raceStartPosition = "";

	[Multiline(3)]
	public string terrainObjectName = "";

	public TerrainObject terrainObjectPrefab;

	private GameObject owner_Connection_4;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_23;

	private GameObject owner_Connection_25;

	private GameObject owner_Connection_30;

	private GameObject owner_Connection_51;

	private GameObject owner_Connection_55;

	private GameObject owner_Connection_67;

	private GameObject owner_Connection_71;

	private GameObject owner_Connection_163;

	private GameObject owner_Connection_166;

	private GameObject owner_Connection_176;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_2 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_2 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_3;

	private bool logic_uScriptCon_CompareBool_True_3 = true;

	private bool logic_uScriptCon_CompareBool_False_3 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_5 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_5;

	private bool logic_uScriptAct_SetBool_Out_5 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_5 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_5 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_8;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_8 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_8 = "Init";

	private uScript_GetSpawnedTerrainObject logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_10 = new uScript_GetSpawnedTerrainObject();

	private GameObject logic_uScript_GetSpawnedTerrainObject_ownerNode_10;

	private string logic_uScript_GetSpawnedTerrainObject_uniqueName_10 = "";

	private GameObject logic_uScript_GetSpawnedTerrainObject_Return_10;

	private bool logic_uScript_GetSpawnedTerrainObject_Out_10 = true;

	private bool logic_uScript_GetSpawnedTerrainObject_ObjectRefInvalid_10 = true;

	private bool logic_uScript_GetSpawnedTerrainObject_CurrentlySpawned_10 = true;

	private bool logic_uScript_GetSpawnedTerrainObject_CurrentlyNotSpawned_10 = true;

	private uScript_SpawnTerrainObject logic_uScript_SpawnTerrainObject_uScript_SpawnTerrainObject_11 = new uScript_SpawnTerrainObject();

	private GameObject logic_uScript_SpawnTerrainObject_ownerNode_11;

	private TerrainObject logic_uScript_SpawnTerrainObject_terrainObjectPrefab_11;

	private string logic_uScript_SpawnTerrainObject_posName_11 = "";

	private string logic_uScript_SpawnTerrainObject_uniqueName_11 = "";

	private bool logic_uScript_SpawnTerrainObject_Out_11 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_21 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_21;

	private string logic_uScript_RemoveScenery_positionName_21 = "";

	private float logic_uScript_RemoveScenery_radius_21;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_21 = true;

	private bool logic_uScript_RemoveScenery_Out_21 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_27 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_27;

	private bool logic_uScript_FinishEncounter_Out_27 = true;

	private uScript_GetLastChallengeResult logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_28 = new uScript_GetLastChallengeResult();

	private bool logic_uScript_GetLastChallengeResult_Success_28 = true;

	private bool logic_uScript_GetLastChallengeResult_Failure_28 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_31 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_31;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_31;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_31;

	private bool logic_uScript_AddMessage_Out_31 = true;

	private bool logic_uScript_AddMessage_Shown_31 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_35 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_35;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_35;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_35;

	private bool logic_uScript_AddMessage_Out_35 = true;

	private bool logic_uScript_AddMessage_Shown_35 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_37 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_37;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_37;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_37;

	private bool logic_uScript_AddMessage_Out_37 = true;

	private bool logic_uScript_AddMessage_Shown_37 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_41 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_41;

	private bool logic_uScriptAct_SetBool_Out_41 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_41 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_41 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_42;

	private bool logic_uScriptCon_CompareBool_True_42 = true;

	private bool logic_uScriptCon_CompareBool_False_42 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_45 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_45;

	private bool logic_uScriptAct_SetBool_Out_45 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_45 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_45 = true;

	private uScript_ClearSceneryAlongSpline logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_46 = new uScript_ClearSceneryAlongSpline();

	private Transform logic_uScript_ClearSceneryAlongSpline_splineStartTrans_46;

	private TrackSpline logic_uScript_ClearSceneryAlongSpline_spline_46;

	private float logic_uScript_ClearSceneryAlongSpline_delayBetweenAreaClears_46;

	private Transform logic_uScript_ClearSceneryAlongSpline_sceneryClearSFXPrefab_46;

	private float logic_uScript_ClearSceneryAlongSpline_stepSizeWidthPercentage_46 = 0.8f;

	private bool logic_uScript_ClearSceneryAlongSpline_clearUpToPenaltyWidth_46;

	private bool logic_uScript_ClearSceneryAlongSpline_Out_46 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_BusyClearing_46 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_DoneClearing_46 = true;

	private uScript_GetEncounterSpline logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_48 = new uScript_GetEncounterSpline();

	private GameObject logic_uScript_GetEncounterSpline_owner_48;

	private TrackSpline logic_uScript_GetEncounterSpline_Return_48;

	private bool logic_uScript_GetEncounterSpline_Out_48 = true;

	private uScript_InitChallengeStarterWithEncounterChallengeData logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_49 = new uScript_InitChallengeStarterWithEncounterChallengeData();

	private GameObject logic_uScript_InitChallengeStarterWithEncounterChallengeData_owner_49;

	private GameObject logic_uScript_InitChallengeStarterWithEncounterChallengeData_targetChallengeStarterObject_49;

	private bool logic_uScript_InitChallengeStarterWithEncounterChallengeData_Out_49 = true;

	private uScript_GetChallengeStartTransform logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_50 = new uScript_GetChallengeStartTransform();

	private GameObject logic_uScript_GetChallengeStartTransform_challengeStarterObject_50;

	private Transform logic_uScript_GetChallengeStartTransform_Return_50;

	private bool logic_uScript_GetChallengeStartTransform_Out_50 = true;

	private uScript_ClearChallengeStarterChallengeData logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_56 = new uScript_ClearChallengeStarterChallengeData();

	private GameObject logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_56;

	private bool logic_uScript_ClearChallengeStarterChallengeData_Out_56 = true;

	private uScript_GetChallengeIDFromChallengeStarter logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_61 = new uScript_GetChallengeIDFromChallengeStarter();

	private GameObject logic_uScript_GetChallengeIDFromChallengeStarter_challengeStarterObject_61;

	private string logic_uScript_GetChallengeIDFromChallengeStarter_Return_61;

	private bool logic_uScript_GetChallengeIDFromChallengeStarter_Out_61 = true;

	private uScript_GetChallengeStateFromChallengeID logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_63 = new uScript_GetChallengeStateFromChallengeID();

	private string logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_63 = "";

	private bool logic_uScript_GetChallengeStateFromChallengeID_Out_63 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_NotRunning_63 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_JustStarted_63 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_InProgress_63 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_JustEnded_63 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_65 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_65 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_65;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_65;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_65;

	private bool logic_uScript_SpawnTechsFromData_Out_65 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_68;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_72 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_72 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_72;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_72 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_72;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_72 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_72 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_72 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_72 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_75 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_75 = new Tank[0];

	private int logic_uScript_AccessListTech_index_75;

	private Tank logic_uScript_AccessListTech_value_75;

	private bool logic_uScript_AccessListTech_Out_75 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_78 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_78;

	private float logic_uScript_IsPlayerInRangeOfTech_range_78;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_78 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_78 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_78 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_78 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_79 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_79;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_79;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_79;

	private bool logic_uScript_AddMessage_Out_79 = true;

	private bool logic_uScript_AddMessage_Shown_79 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_80 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_80;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_80;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_84 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_84;

	private int logic_uScriptCon_CompareInt_B_84;

	private bool logic_uScriptCon_CompareInt_GreaterThan_84 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_84 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_84 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_84 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_84 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_84 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_88 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_88;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_88;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_88;

	private bool logic_uScript_AddMessage_Out_88 = true;

	private bool logic_uScript_AddMessage_Shown_88 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_91 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_91;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_91;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_91;

	private bool logic_uScript_AddMessage_Out_91 = true;

	private bool logic_uScript_AddMessage_Shown_91 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_93 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_93;

	private bool logic_uScriptAct_SetBool_Out_93 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_93 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_93 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_95;

	private bool logic_uScriptCon_CompareBool_True_95 = true;

	private bool logic_uScriptCon_CompareBool_False_95 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_97 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_97;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_97;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_97;

	private bool logic_uScript_AddMessage_Out_97 = true;

	private bool logic_uScript_AddMessage_Shown_97 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_100 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_100;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_100;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_100;

	private bool logic_uScript_AddMessage_Out_100 = true;

	private bool logic_uScript_AddMessage_Shown_100 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_105;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_105 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_105 = "MsgIntro";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_106 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_106;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_106 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_106 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_108;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_110 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_110;

	private bool logic_uScriptAct_SetBool_Out_110 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_110 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_110 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_112;

	private bool logic_uScriptCon_CompareBool_True_112 = true;

	private bool logic_uScriptCon_CompareBool_False_112 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_114;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_114 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_114 = "MsgHalfwayRound";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_115;

	private bool logic_uScriptCon_CompareBool_True_115 = true;

	private bool logic_uScriptCon_CompareBool_False_115 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_118 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_118;

	private bool logic_uScriptAct_SetBool_Out_118 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_118 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_118 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_123 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_123;

	private bool logic_uScriptAct_SetBool_Out_123 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_123 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_123 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_125 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_125;

	private bool logic_uScriptAct_SetBool_Out_125 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_125 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_125 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_128;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_128 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_128 = "ChallengeEnded";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_129;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_129 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_129 = "MissionComplete";

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_132 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_132;

	private float logic_uScript_IsPlayerInRangeOfTech_range_132;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_132 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_132 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_132 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_132 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_134 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_134;

	private bool logic_uScriptAct_SetBool_Out_134 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_134 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_134 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_136 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_136;

	private bool logic_uScriptAct_SetBool_Out_136 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_136 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_136 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_138 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_138;

	private bool logic_uScriptCon_CompareBool_True_138 = true;

	private bool logic_uScriptCon_CompareBool_False_138 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_140 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_140 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_140 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_140 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_142 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_142 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_142 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_142 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_144 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_144;

	private bool logic_uScriptAct_SetBool_Out_144 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_144 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_144 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_146;

	private bool logic_uScriptCon_CompareBool_True_146 = true;

	private bool logic_uScriptCon_CompareBool_False_146 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_148;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_148 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_148 = "MsgNPCFound";

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_149 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_149 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_149;

	private bool logic_uScript_SetTankInvulnerable_Out_149 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_151 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_151;

	private bool logic_uScriptCon_CompareBool_True_151 = true;

	private bool logic_uScriptCon_CompareBool_False_151 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_154 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_154;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_154;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_154;

	private bool logic_uScript_AddMessage_Out_154 = true;

	private bool logic_uScript_AddMessage_Shown_154 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_156;

	private bool logic_uScriptCon_CompareBool_True_156 = true;

	private bool logic_uScriptCon_CompareBool_False_156 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_159 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_159;

	private bool logic_uScriptAct_SetBool_Out_159 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_159 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_159 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_161 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_161;

	private float logic_uScript_IsPlayerInRangeOfTech_range_161;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_161 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_161 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_161 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_161 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_164 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_164;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_164 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_165 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_165;

	private object logic_uScript_SetEncounterTarget_visibleObject_165 = "";

	private bool logic_uScript_SetEncounterTarget_Out_165 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_168 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_168;

	private bool logic_uScript_ShowHint_Out_168 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_170 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_170;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_170 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_170 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_172 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_172;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_172;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_172;

	private bool logic_uScript_AddMessage_Out_172 = true;

	private bool logic_uScript_AddMessage_Shown_172 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_175 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_175;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_175 = CheckpointChallenge.EndReason.FailedOutOfBounds;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_175 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_175 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_180 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_180;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_180 = CheckpointChallenge.EndReason.FailedQuit;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_180 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_180 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_184 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_184;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_184;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_184;

	private bool logic_uScript_AddMessage_Out_184 = true;

	private bool logic_uScript_AddMessage_Shown_184 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_186 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_186;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_186 = CheckpointChallenge.EndReason.FailedTimeUp;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_186 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_186 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_188 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_188;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_188;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_188;

	private bool logic_uScript_AddMessage_Out_188 = true;

	private bool logic_uScript_AddMessage_Shown_188 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_190 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_190;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_190 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_190 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_190;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_190;

	private bool logic_uScript_FlyTechUpAndAway_Out_190 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_194;

	private bool logic_uScriptCon_CompareBool_True_194 = true;

	private bool logic_uScriptCon_CompareBool_False_194 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_195 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_195;

	private bool logic_uScriptAct_SetBool_Out_195 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_195 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_195 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_197;

	private bool logic_uScriptCon_CompareBool_True_197 = true;

	private bool logic_uScriptCon_CompareBool_False_197 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_199 = true;

	private int event_UnityEngine_GameObject_CheckpointIndex_26;

	private CheckpointChallenge.EndReason event_UnityEngine_GameObject_EndReason_181;

	private float event_UnityEngine_GameObject_EndTime_181;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		if (null == owner_Connection_4 || !m_RegisteredForEvents)
		{
			owner_Connection_4 = parentGameObject;
			if (null != owner_Connection_4)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_4.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_4.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_1;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_1;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_1;
				}
			}
		}
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
			if (null != owner_Connection_6)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_6.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_6.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
				}
			}
		}
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
		}
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (null == owner_Connection_23 || !m_RegisteredForEvents)
		{
			owner_Connection_23 = parentGameObject;
			if (null != owner_Connection_23)
			{
				uScript_BoundsWarningEvent uScript_BoundsWarningEvent2 = owner_Connection_23.GetComponent<uScript_BoundsWarningEvent>();
				if (null == uScript_BoundsWarningEvent2)
				{
					uScript_BoundsWarningEvent2 = owner_Connection_23.AddComponent<uScript_BoundsWarningEvent>();
				}
				if (null != uScript_BoundsWarningEvent2)
				{
					uScript_BoundsWarningEvent2.OnBoundsWarningCaution += Instance_OnBoundsWarningCaution_24;
					uScript_BoundsWarningEvent2.OnBoundsWarningIllegal += Instance_OnBoundsWarningIllegal_24;
				}
			}
		}
		if (null == owner_Connection_25 || !m_RegisteredForEvents)
		{
			owner_Connection_25 = parentGameObject;
			if (null != owner_Connection_25)
			{
				uScript_CheckPointPassedEvent uScript_CheckPointPassedEvent2 = owner_Connection_25.GetComponent<uScript_CheckPointPassedEvent>();
				if (null == uScript_CheckPointPassedEvent2)
				{
					uScript_CheckPointPassedEvent2 = owner_Connection_25.AddComponent<uScript_CheckPointPassedEvent>();
				}
				if (null != uScript_CheckPointPassedEvent2)
				{
					uScript_CheckPointPassedEvent2.OnCheckPointPassed += Instance_OnCheckPointPassed_26;
				}
			}
		}
		if (null == owner_Connection_30 || !m_RegisteredForEvents)
		{
			owner_Connection_30 = parentGameObject;
		}
		if (null == owner_Connection_51 || !m_RegisteredForEvents)
		{
			owner_Connection_51 = parentGameObject;
		}
		if (null == owner_Connection_55 || !m_RegisteredForEvents)
		{
			owner_Connection_55 = parentGameObject;
		}
		if (null == owner_Connection_67 || !m_RegisteredForEvents)
		{
			owner_Connection_67 = parentGameObject;
		}
		if (null == owner_Connection_71 || !m_RegisteredForEvents)
		{
			owner_Connection_71 = parentGameObject;
		}
		if (null == owner_Connection_163 || !m_RegisteredForEvents)
		{
			owner_Connection_163 = parentGameObject;
		}
		if (null == owner_Connection_166 || !m_RegisteredForEvents)
		{
			owner_Connection_166 = parentGameObject;
		}
		if (!(null == owner_Connection_176) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_176 = parentGameObject;
		if (null != owner_Connection_176)
		{
			uScript_CheckPointChallengeEndedEvent uScript_CheckPointChallengeEndedEvent2 = owner_Connection_176.GetComponent<uScript_CheckPointChallengeEndedEvent>();
			if (null == uScript_CheckPointChallengeEndedEvent2)
			{
				uScript_CheckPointChallengeEndedEvent2 = owner_Connection_176.AddComponent<uScript_CheckPointChallengeEndedEvent>();
			}
			if (null != uScript_CheckPointChallengeEndedEvent2)
			{
				uScript_CheckPointChallengeEndedEvent2.OnSuccess += Instance_OnSuccess_181;
				uScript_CheckPointChallengeEndedEvent2.OnFail += Instance_OnFail_181;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		if (!m_RegisteredForEvents && null != owner_Connection_4)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_4.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_4.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_1;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_1;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_1;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_6)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_6.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_6.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_23)
		{
			uScript_BoundsWarningEvent uScript_BoundsWarningEvent2 = owner_Connection_23.GetComponent<uScript_BoundsWarningEvent>();
			if (null == uScript_BoundsWarningEvent2)
			{
				uScript_BoundsWarningEvent2 = owner_Connection_23.AddComponent<uScript_BoundsWarningEvent>();
			}
			if (null != uScript_BoundsWarningEvent2)
			{
				uScript_BoundsWarningEvent2.OnBoundsWarningCaution += Instance_OnBoundsWarningCaution_24;
				uScript_BoundsWarningEvent2.OnBoundsWarningIllegal += Instance_OnBoundsWarningIllegal_24;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_25)
		{
			uScript_CheckPointPassedEvent uScript_CheckPointPassedEvent2 = owner_Connection_25.GetComponent<uScript_CheckPointPassedEvent>();
			if (null == uScript_CheckPointPassedEvent2)
			{
				uScript_CheckPointPassedEvent2 = owner_Connection_25.AddComponent<uScript_CheckPointPassedEvent>();
			}
			if (null != uScript_CheckPointPassedEvent2)
			{
				uScript_CheckPointPassedEvent2.OnCheckPointPassed += Instance_OnCheckPointPassed_26;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_176)
		{
			uScript_CheckPointChallengeEndedEvent uScript_CheckPointChallengeEndedEvent2 = owner_Connection_176.GetComponent<uScript_CheckPointChallengeEndedEvent>();
			if (null == uScript_CheckPointChallengeEndedEvent2)
			{
				uScript_CheckPointChallengeEndedEvent2 = owner_Connection_176.AddComponent<uScript_CheckPointChallengeEndedEvent>();
			}
			if (null != uScript_CheckPointChallengeEndedEvent2)
			{
				uScript_CheckPointChallengeEndedEvent2.OnSuccess += Instance_OnSuccess_181;
				uScript_CheckPointChallengeEndedEvent2.OnFail += Instance_OnFail_181;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_4)
		{
			uScript_SaveLoad component = owner_Connection_4.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_1;
				component.LoadEvent -= Instance_LoadEvent_1;
				component.RestartEvent -= Instance_RestartEvent_1;
			}
		}
		if (null != owner_Connection_6)
		{
			uScript_EncounterUpdate component2 = owner_Connection_6.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_0;
				component2.OnSuspend -= Instance_OnSuspend_0;
				component2.OnResume -= Instance_OnResume_0;
			}
		}
		if (null != owner_Connection_23)
		{
			uScript_BoundsWarningEvent component3 = owner_Connection_23.GetComponent<uScript_BoundsWarningEvent>();
			if (null != component3)
			{
				component3.OnBoundsWarningCaution -= Instance_OnBoundsWarningCaution_24;
				component3.OnBoundsWarningIllegal -= Instance_OnBoundsWarningIllegal_24;
			}
		}
		if (null != owner_Connection_25)
		{
			uScript_CheckPointPassedEvent component4 = owner_Connection_25.GetComponent<uScript_CheckPointPassedEvent>();
			if (null != component4)
			{
				component4.OnCheckPointPassed -= Instance_OnCheckPointPassed_26;
			}
		}
		if (null != owner_Connection_176)
		{
			uScript_CheckPointChallengeEndedEvent component5 = owner_Connection_176.GetComponent<uScript_CheckPointChallengeEndedEvent>();
			if (null != component5)
			{
				component5.OnSuccess -= Instance_OnSuccess_181;
				component5.OnFail -= Instance_OnFail_181;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_2.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_5.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.SetParent(g);
		logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_10.SetParent(g);
		logic_uScript_SpawnTerrainObject_uScript_SpawnTerrainObject_11.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_21.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_27.SetParent(g);
		logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_28.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_31.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_35.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_37.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.SetParent(g);
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_46.SetParent(g);
		logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_48.SetParent(g);
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_49.SetParent(g);
		logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_50.SetParent(g);
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_56.SetParent(g);
		logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_61.SetParent(g);
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_63.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_65.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_72.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_75.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_78.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_79.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_80.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_84.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_88.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_91.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_97.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_100.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_110.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_118.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_132.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_134.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_138.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_140.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_142.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_144.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_149.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_151.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_154.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_161.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_164.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_165.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_168.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_170.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_172.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_175.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_180.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_184.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_186.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_188.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_190.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_195.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199.SetParent(g);
		owner_Connection_4 = parentGameObject;
		owner_Connection_6 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_23 = parentGameObject;
		owner_Connection_25 = parentGameObject;
		owner_Connection_30 = parentGameObject;
		owner_Connection_51 = parentGameObject;
		owner_Connection_55 = parentGameObject;
		owner_Connection_67 = parentGameObject;
		owner_Connection_71 = parentGameObject;
		owner_Connection_163 = parentGameObject;
		owner_Connection_166 = parentGameObject;
		owner_Connection_176 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_80.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Save_Out += SubGraph_SaveLoadBool_Save_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Load_Out += SubGraph_SaveLoadBool_Load_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output1 += uScriptCon_ManualSwitch_Output1_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output2 += uScriptCon_ManualSwitch_Output2_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output3 += uScriptCon_ManualSwitch_Output3_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output4 += uScriptCon_ManualSwitch_Output4_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output5 += uScriptCon_ManualSwitch_Output5_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output6 += uScriptCon_ManualSwitch_Output6_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output7 += uScriptCon_ManualSwitch_Output7_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output8 += uScriptCon_ManualSwitch_Output8_68;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_80.Out += SubGraph_CompleteObjectiveStage_Out_80;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Save_Out += SubGraph_SaveLoadBool_Save_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Load_Out += SubGraph_SaveLoadBool_Load_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_105;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.Save_Out += SubGraph_SaveLoadInt_Save_Out_106;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.Load_Out += SubGraph_SaveLoadInt_Load_Out_106;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_106;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.Out += SubGraph_LoadObjectiveStates_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Save_Out += SubGraph_SaveLoadBool_Save_Out_114;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Load_Out += SubGraph_SaveLoadBool_Load_Out_114;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_114;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Save_Out += SubGraph_SaveLoadBool_Save_Out_128;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Load_Out += SubGraph_SaveLoadBool_Load_Out_128;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_128;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Save_Out += SubGraph_SaveLoadBool_Save_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Load_Out += SubGraph_SaveLoadBool_Load_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Save_Out += SubGraph_SaveLoadBool_Save_Out_148;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Load_Out += SubGraph_SaveLoadBool_Load_Out_148;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_148;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_80.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_80.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_164.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_170.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_190.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.OnDisable();
		logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_10.OnDisable();
		logic_uScript_SpawnTerrainObject_uScript_SpawnTerrainObject_11.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_31.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_35.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_37.OnDisable();
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_46.OnDisable();
		logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_48.OnDisable();
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_49.OnDisable();
		logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_50.OnDisable();
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_56.OnDisable();
		logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_61.OnDisable();
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_63.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_78.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_79.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_80.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_88.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_91.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_97.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_100.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_132.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_149.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_154.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_161.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_172.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_184.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_188.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_80.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_80.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Save_Out -= SubGraph_SaveLoadBool_Save_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Load_Out -= SubGraph_SaveLoadBool_Load_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output1 -= uScriptCon_ManualSwitch_Output1_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output2 -= uScriptCon_ManualSwitch_Output2_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output3 -= uScriptCon_ManualSwitch_Output3_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output4 -= uScriptCon_ManualSwitch_Output4_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output5 -= uScriptCon_ManualSwitch_Output5_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output6 -= uScriptCon_ManualSwitch_Output6_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output7 -= uScriptCon_ManualSwitch_Output7_68;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.Output8 -= uScriptCon_ManualSwitch_Output8_68;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_80.Out -= SubGraph_CompleteObjectiveStage_Out_80;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Save_Out -= SubGraph_SaveLoadBool_Save_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Load_Out -= SubGraph_SaveLoadBool_Load_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_105;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.Save_Out -= SubGraph_SaveLoadInt_Save_Out_106;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.Load_Out -= SubGraph_SaveLoadInt_Load_Out_106;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_106;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.Out -= SubGraph_LoadObjectiveStates_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Save_Out -= SubGraph_SaveLoadBool_Save_Out_114;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Load_Out -= SubGraph_SaveLoadBool_Load_Out_114;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_114;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Save_Out -= SubGraph_SaveLoadBool_Save_Out_128;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Load_Out -= SubGraph_SaveLoadBool_Load_Out_128;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_128;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Save_Out -= SubGraph_SaveLoadBool_Save_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Load_Out -= SubGraph_SaveLoadBool_Load_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Save_Out -= SubGraph_SaveLoadBool_Save_Out_148;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Load_Out -= SubGraph_SaveLoadBool_Load_Out_148;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_148;
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

	private void Instance_OnBoundsWarningCaution_24(object o, EventArgs e)
	{
		Relay_OnBoundsWarningCaution_24();
	}

	private void Instance_OnBoundsWarningIllegal_24(object o, EventArgs e)
	{
		Relay_OnBoundsWarningIllegal_24();
	}

	private void Instance_OnCheckPointPassed_26(object o, uScript_CheckPointPassedEvent.CheckpointPassedEventArgs e)
	{
		event_UnityEngine_GameObject_CheckpointIndex_26 = e.CheckpointIndex;
		Relay_OnCheckPointPassed_26();
	}

	private void Instance_OnSuccess_181(object o, uScript_CheckPointChallengeEndedEvent.CheckpointChallengeEndedEventArgs e)
	{
		event_UnityEngine_GameObject_EndReason_181 = e.EndReason;
		event_UnityEngine_GameObject_EndTime_181 = e.EndTime;
		Relay_OnSuccess_181();
	}

	private void Instance_OnFail_181(object o, uScript_CheckPointChallengeEndedEvent.CheckpointChallengeEndedEventArgs e)
	{
		event_UnityEngine_GameObject_EndReason_181 = e.EndReason;
		event_UnityEngine_GameObject_EndTime_181 = e.EndTime;
		Relay_OnFail_181();
	}

	private void SubGraph_SaveLoadBool_Save_Out_8(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_8;
		Relay_Save_Out_8();
	}

	private void SubGraph_SaveLoadBool_Load_Out_8(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_8;
		Relay_Load_Out_8();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_8(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_8;
		Relay_Restart_Out_8();
	}

	private void uScriptCon_ManualSwitch_Output1_68(object o, EventArgs e)
	{
		Relay_Output1_68();
	}

	private void uScriptCon_ManualSwitch_Output2_68(object o, EventArgs e)
	{
		Relay_Output2_68();
	}

	private void uScriptCon_ManualSwitch_Output3_68(object o, EventArgs e)
	{
		Relay_Output3_68();
	}

	private void uScriptCon_ManualSwitch_Output4_68(object o, EventArgs e)
	{
		Relay_Output4_68();
	}

	private void uScriptCon_ManualSwitch_Output5_68(object o, EventArgs e)
	{
		Relay_Output5_68();
	}

	private void uScriptCon_ManualSwitch_Output6_68(object o, EventArgs e)
	{
		Relay_Output6_68();
	}

	private void uScriptCon_ManualSwitch_Output7_68(object o, EventArgs e)
	{
		Relay_Output7_68();
	}

	private void uScriptCon_ManualSwitch_Output8_68(object o, EventArgs e)
	{
		Relay_Output8_68();
	}

	private void SubGraph_CompleteObjectiveStage_Out_80(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_80 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_80;
		Relay_Out_80();
	}

	private void SubGraph_SaveLoadBool_Save_Out_105(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = e.boolean;
		local_MsgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_105;
		Relay_Save_Out_105();
	}

	private void SubGraph_SaveLoadBool_Load_Out_105(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = e.boolean;
		local_MsgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_105;
		Relay_Load_Out_105();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_105(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = e.boolean;
		local_MsgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_105;
		Relay_Restart_Out_105();
	}

	private void SubGraph_SaveLoadInt_Save_Out_106(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_106 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_106;
		Relay_Save_Out_106();
	}

	private void SubGraph_SaveLoadInt_Load_Out_106(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_106 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_106;
		Relay_Load_Out_106();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_106(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_106 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_106;
		Relay_Restart_Out_106();
	}

	private void SubGraph_LoadObjectiveStates_Out_108(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_108();
	}

	private void SubGraph_SaveLoadBool_Save_Out_114(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_114 = e.boolean;
		local_MsgHalfwayRound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_114;
		Relay_Save_Out_114();
	}

	private void SubGraph_SaveLoadBool_Load_Out_114(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_114 = e.boolean;
		local_MsgHalfwayRound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_114;
		Relay_Load_Out_114();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_114(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_114 = e.boolean;
		local_MsgHalfwayRound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_114;
		Relay_Restart_Out_114();
	}

	private void SubGraph_SaveLoadBool_Save_Out_128(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = e.boolean;
		local_ChallengeEnded_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_128;
		Relay_Save_Out_128();
	}

	private void SubGraph_SaveLoadBool_Load_Out_128(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = e.boolean;
		local_ChallengeEnded_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_128;
		Relay_Load_Out_128();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_128(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = e.boolean;
		local_ChallengeEnded_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_128;
		Relay_Restart_Out_128();
	}

	private void SubGraph_SaveLoadBool_Save_Out_129(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = e.boolean;
		local_MissionComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_129;
		Relay_Save_Out_129();
	}

	private void SubGraph_SaveLoadBool_Load_Out_129(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = e.boolean;
		local_MissionComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_129;
		Relay_Load_Out_129();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_129(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = e.boolean;
		local_MissionComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_129;
		Relay_Restart_Out_129();
	}

	private void SubGraph_SaveLoadBool_Save_Out_148(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = e.boolean;
		local_MsgNPCFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_148;
		Relay_Save_Out_148();
	}

	private void SubGraph_SaveLoadBool_Load_Out_148(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = e.boolean;
		local_MsgNPCFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_148;
		Relay_Load_Out_148();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_148(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = e.boolean;
		local_MsgNPCFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_148;
		Relay_Restart_Out_148();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_3();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_SaveEvent_1()
	{
		Relay_Save_8();
	}

	private void Relay_LoadEvent_1()
	{
		Relay_Load_8();
	}

	private void Relay_RestartEvent_1()
	{
		Relay_Set_False_8();
	}

	private void Relay_In_2()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_2.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_2.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_3()
	{
		logic_uScriptCon_CompareBool_Bool_3 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3.In(logic_uScriptCon_CompareBool_Bool_3);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3.False;
		if (num)
		{
			Relay_In_2();
		}
		if (flag)
		{
			Relay_True_5();
		}
	}

	private void Relay_True_5()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_5.True(out logic_uScriptAct_SetBool_Target_5);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_5;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_5.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_False_5()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_5.False(out logic_uScriptAct_SetBool_Target_5);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_5;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_5.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_Save_Out_8()
	{
		Relay_Save_105();
	}

	private void Relay_Load_Out_8()
	{
		Relay_Load_105();
	}

	private void Relay_Restart_Out_8()
	{
		Relay_Set_False_105();
	}

	private void Relay_Save_8()
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_8 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Save(ref logic_SubGraph_SaveLoadBool_boolean_8, logic_SubGraph_SaveLoadBool_boolAsVariable_8, logic_SubGraph_SaveLoadBool_uniqueID_8);
	}

	private void Relay_Load_8()
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_8 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Load(ref logic_SubGraph_SaveLoadBool_boolean_8, logic_SubGraph_SaveLoadBool_boolAsVariable_8, logic_SubGraph_SaveLoadBool_uniqueID_8);
	}

	private void Relay_Set_True_8()
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_8 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_8, logic_SubGraph_SaveLoadBool_boolAsVariable_8, logic_SubGraph_SaveLoadBool_uniqueID_8);
	}

	private void Relay_Set_False_8()
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_8 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_8, logic_SubGraph_SaveLoadBool_boolAsVariable_8, logic_SubGraph_SaveLoadBool_uniqueID_8);
	}

	private void Relay_In_10()
	{
		logic_uScript_GetSpawnedTerrainObject_ownerNode_10 = owner_Connection_17;
		logic_uScript_GetSpawnedTerrainObject_uniqueName_10 = terrainObjectName;
		logic_uScript_GetSpawnedTerrainObject_Return_10 = logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_10.In(logic_uScript_GetSpawnedTerrainObject_ownerNode_10, logic_uScript_GetSpawnedTerrainObject_uniqueName_10);
		local_StartingRamp_UnityEngine_GameObject = logic_uScript_GetSpawnedTerrainObject_Return_10;
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		if (logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_10.CurrentlySpawned)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_SpawnTerrainObject_ownerNode_11 = owner_Connection_12;
		logic_uScript_SpawnTerrainObject_terrainObjectPrefab_11 = terrainObjectPrefab;
		logic_uScript_SpawnTerrainObject_posName_11 = raceStartPosition;
		logic_uScript_SpawnTerrainObject_uniqueName_11 = terrainObjectName;
		logic_uScript_SpawnTerrainObject_uScript_SpawnTerrainObject_11.In(logic_uScript_SpawnTerrainObject_ownerNode_11, logic_uScript_SpawnTerrainObject_terrainObjectPrefab_11, logic_uScript_SpawnTerrainObject_posName_11, logic_uScript_SpawnTerrainObject_uniqueName_11);
		if (logic_uScript_SpawnTerrainObject_uScript_SpawnTerrainObject_11.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_21()
	{
		logic_uScript_RemoveScenery_ownerNode_21 = owner_Connection_20;
		logic_uScript_RemoveScenery_positionName_21 = raceStartPosition;
		logic_uScript_RemoveScenery_radius_21 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_21.In(logic_uScript_RemoveScenery_ownerNode_21, logic_uScript_RemoveScenery_positionName_21, logic_uScript_RemoveScenery_radius_21, logic_uScript_RemoveScenery_preventChunksSpawning_21);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_21.Out)
		{
			Relay_InitialSpawn_65();
		}
	}

	private void Relay_OnBoundsWarningCaution_24()
	{
	}

	private void Relay_OnBoundsWarningIllegal_24()
	{
	}

	private void Relay_OnCheckPointPassed_26()
	{
		local_PassedCheckpointIdx_System_Int32 = event_UnityEngine_GameObject_CheckpointIndex_26;
		Relay_In_84();
	}

	private void Relay_Succeed_27()
	{
		logic_uScript_FinishEncounter_owner_27 = owner_Connection_30;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_27.Succeed(logic_uScript_FinishEncounter_owner_27);
	}

	private void Relay_Fail_27()
	{
		logic_uScript_FinishEncounter_owner_27 = owner_Connection_30;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_27.Fail(logic_uScript_FinishEncounter_owner_27);
	}

	private void Relay_In_28()
	{
		logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_28.In();
		bool success = logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_28.Success;
		bool failure = logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_28.Failure;
		if (success)
		{
			Relay_In_56();
		}
		if (failure)
		{
			Relay_In_175();
		}
	}

	private void Relay_In_31()
	{
		logic_uScript_AddMessage_messageData_31 = msgOutOfBounds;
		logic_uScript_AddMessage_speaker_31 = messageSpeaker;
		logic_uScript_AddMessage_Return_31 = logic_uScript_AddMessage_uScript_AddMessage_31.In(logic_uScript_AddMessage_messageData_31, logic_uScript_AddMessage_speaker_31);
	}

	private void Relay_In_35()
	{
		logic_uScript_AddMessage_messageData_35 = msgComplete;
		logic_uScript_AddMessage_speaker_35 = messageSpeaker;
		logic_uScript_AddMessage_Return_35 = logic_uScript_AddMessage_uScript_AddMessage_35.In(logic_uScript_AddMessage_messageData_35, logic_uScript_AddMessage_speaker_35);
		if (logic_uScript_AddMessage_uScript_AddMessage_35.Shown)
		{
			Relay_In_190();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_AddMessage_messageData_37 = msgOutOfTime;
		logic_uScript_AddMessage_speaker_37 = messageSpeaker;
		logic_uScript_AddMessage_Return_37 = logic_uScript_AddMessage_uScript_AddMessage_37.In(logic_uScript_AddMessage_messageData_37, logic_uScript_AddMessage_speaker_37);
		if (logic_uScript_AddMessage_uScript_AddMessage_37.Out)
		{
			Relay_In_170();
		}
	}

	private void Relay_True_41()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.True(out logic_uScriptAct_SetBool_Target_41);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_41;
	}

	private void Relay_False_41()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.False(out logic_uScriptAct_SetBool_Target_41);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_41;
	}

	private void Relay_In_42()
	{
		logic_uScriptCon_CompareBool_Bool_42 = local_OutOfBounds_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.In(logic_uScriptCon_CompareBool_Bool_42);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.False;
		if (num)
		{
			Relay_In_31();
		}
		if (flag)
		{
			Relay_In_37();
		}
	}

	private void Relay_True_45()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.True(out logic_uScriptAct_SetBool_Target_45);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_45;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_45.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_False_45()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.False(out logic_uScriptAct_SetBool_Target_45);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_45;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_45.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_In_46()
	{
		logic_uScript_ClearSceneryAlongSpline_splineStartTrans_46 = local_StartTransform_UnityEngine_Transform;
		logic_uScript_ClearSceneryAlongSpline_spline_46 = local_Spline_TrackSpline;
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_46.In(logic_uScript_ClearSceneryAlongSpline_splineStartTrans_46, logic_uScript_ClearSceneryAlongSpline_spline_46, logic_uScript_ClearSceneryAlongSpline_delayBetweenAreaClears_46, logic_uScript_ClearSceneryAlongSpline_sceneryClearSFXPrefab_46, logic_uScript_ClearSceneryAlongSpline_stepSizeWidthPercentage_46, logic_uScript_ClearSceneryAlongSpline_clearUpToPenaltyWidth_46);
		if (logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_46.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_In_48()
	{
		logic_uScript_GetEncounterSpline_owner_48 = owner_Connection_51;
		logic_uScript_GetEncounterSpline_Return_48 = logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_48.In(logic_uScript_GetEncounterSpline_owner_48);
		local_Spline_TrackSpline = logic_uScript_GetEncounterSpline_Return_48;
		if (logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_48.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_49()
	{
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_owner_49 = owner_Connection_55;
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_targetChallengeStarterObject_49 = local_StartingRamp_UnityEngine_GameObject;
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_49.In(logic_uScript_InitChallengeStarterWithEncounterChallengeData_owner_49, logic_uScript_InitChallengeStarterWithEncounterChallengeData_targetChallengeStarterObject_49);
		if (logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_49.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_50()
	{
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		logic_uScript_GetChallengeStartTransform_challengeStarterObject_50 = local_StartingRamp_UnityEngine_GameObject;
		logic_uScript_GetChallengeStartTransform_Return_50 = logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_50.In(logic_uScript_GetChallengeStartTransform_challengeStarterObject_50);
		local_StartTransform_UnityEngine_Transform = logic_uScript_GetChallengeStartTransform_Return_50;
		if (logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_50.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_In_56()
	{
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_56 = local_StartingRamp_UnityEngine_GameObject;
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_56.In(logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_56);
		if (logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_56.Out)
		{
			Relay_True_118();
		}
	}

	private void Relay_In_61()
	{
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		logic_uScript_GetChallengeIDFromChallengeStarter_challengeStarterObject_61 = local_StartingRamp_UnityEngine_GameObject;
		logic_uScript_GetChallengeIDFromChallengeStarter_Return_61 = logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_61.In(logic_uScript_GetChallengeIDFromChallengeStarter_challengeStarterObject_61);
		local_TargetChallengeID_System_String = logic_uScript_GetChallengeIDFromChallengeStarter_Return_61;
		if (logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_61.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_63()
	{
		logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_63 = local_TargetChallengeID_System_String;
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_63.In(logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_63);
		bool notRunning = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_63.NotRunning;
		bool justStarted = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_63.JustStarted;
		bool justEnded = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_63.JustEnded;
		if (notRunning)
		{
			Relay_In_151();
		}
		if (justStarted)
		{
			Relay_True_134();
		}
		if (justEnded)
		{
			Relay_False_136();
		}
	}

	private void Relay_InitialSpawn_65()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_65.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_65, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_65, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_65 = owner_Connection_67;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_65.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_65, logic_uScript_SpawnTechsFromData_ownerNode_65, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_65, logic_uScript_SpawnTechsFromData_allowResurrection_65);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_65.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_Output1_68()
	{
		Relay_In_78();
	}

	private void Relay_Output2_68()
	{
		Relay_In_115();
	}

	private void Relay_Output3_68()
	{
	}

	private void Relay_Output4_68()
	{
	}

	private void Relay_Output5_68()
	{
	}

	private void Relay_Output6_68()
	{
	}

	private void Relay_Output7_68()
	{
	}

	private void Relay_Output8_68()
	{
	}

	private void Relay_In_68()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_68 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_68.In(logic_uScriptCon_ManualSwitch_CurrentOutput_68);
	}

	private void Relay_In_72()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_72.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_72, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_72, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_72 = owner_Connection_71;
		int num2 = 0;
		Array array = local_73_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_72.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_72, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_72, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_72 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_72.In(logic_uScript_GetAndCheckTechs_techData_72, logic_uScript_GetAndCheckTechs_ownerNode_72, ref logic_uScript_GetAndCheckTechs_techs_72);
		local_73_TankArray = logic_uScript_GetAndCheckTechs_techs_72;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_72.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_72.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_75();
		}
		if (someAlive)
		{
			Relay_AtIndex_75();
		}
	}

	private void Relay_AtIndex_75()
	{
		int num = 0;
		Array array = local_73_TankArray;
		if (logic_uScript_AccessListTech_techList_75.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_75, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_75, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_75.AtIndex(ref logic_uScript_AccessListTech_techList_75, logic_uScript_AccessListTech_index_75, out logic_uScript_AccessListTech_value_75);
		local_73_TankArray = logic_uScript_AccessListTech_techList_75;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_75;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_75.Out)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_78()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_78 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_78 = distNearNPC;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_78.In(logic_uScript_IsPlayerInRangeOfTech_tech_78, logic_uScript_IsPlayerInRangeOfTech_range_78, logic_uScript_IsPlayerInRangeOfTech_techs_78);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_78.InRange)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_AddMessage_messageData_79 = msgIntro;
		logic_uScript_AddMessage_speaker_79 = messageSpeaker;
		logic_uScript_AddMessage_Return_79 = logic_uScript_AddMessage_uScript_AddMessage_79.In(logic_uScript_AddMessage_messageData_79, logic_uScript_AddMessage_speaker_79);
		if (logic_uScript_AddMessage_uScript_AddMessage_79.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_Out_80()
	{
	}

	private void Relay_In_80()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_80 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_80.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_80, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_80);
	}

	private void Relay_In_84()
	{
		logic_uScriptCon_CompareInt_A_84 = local_PassedCheckpointIdx_System_Int32;
		logic_uScriptCon_CompareInt_B_84 = halfwayCheckpointIndex;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_84.In(logic_uScriptCon_CompareInt_A_84, logic_uScriptCon_CompareInt_B_84);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_84.EqualTo)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_88()
	{
		logic_uScript_AddMessage_messageData_88 = msgHalfwayRound;
		logic_uScript_AddMessage_speaker_88 = messageSpeaker;
		logic_uScript_AddMessage_Return_88 = logic_uScript_AddMessage_uScript_AddMessage_88.In(logic_uScript_AddMessage_messageData_88, logic_uScript_AddMessage_speaker_88);
	}

	private void Relay_In_91()
	{
		logic_uScript_AddMessage_messageData_91 = msgRaceStarted;
		logic_uScript_AddMessage_speaker_91 = messageSpeaker;
		logic_uScript_AddMessage_Return_91 = logic_uScript_AddMessage_uScript_AddMessage_91.In(logic_uScript_AddMessage_messageData_91, logic_uScript_AddMessage_speaker_91);
	}

	private void Relay_True_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.True(out logic_uScriptAct_SetBool_Target_93);
		local_MsgNPCFound_System_Boolean = logic_uScriptAct_SetBool_Target_93;
	}

	private void Relay_False_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.False(out logic_uScriptAct_SetBool_Target_93);
		local_MsgNPCFound_System_Boolean = logic_uScriptAct_SetBool_Target_93;
	}

	private void Relay_In_95()
	{
		logic_uScriptCon_CompareBool_Bool_95 = local_MsgNPCFound_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.In(logic_uScriptCon_CompareBool_Bool_95);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.False;
		if (num)
		{
			Relay_In_100();
		}
		if (flag)
		{
			Relay_In_97();
		}
	}

	private void Relay_In_97()
	{
		logic_uScript_AddMessage_messageData_97 = msgNPCFound;
		logic_uScript_AddMessage_speaker_97 = messageSpeaker;
		logic_uScript_AddMessage_Return_97 = logic_uScript_AddMessage_uScript_AddMessage_97.In(logic_uScript_AddMessage_messageData_97, logic_uScript_AddMessage_speaker_97);
		if (logic_uScript_AddMessage_uScript_AddMessage_97.Shown)
		{
			Relay_True_93();
		}
	}

	private void Relay_In_100()
	{
		logic_uScript_AddMessage_messageData_100 = msgStartRace;
		logic_uScript_AddMessage_speaker_100 = messageSpeaker;
		logic_uScript_AddMessage_Return_100 = logic_uScript_AddMessage_uScript_AddMessage_100.In(logic_uScript_AddMessage_messageData_100, logic_uScript_AddMessage_speaker_100);
		local_MsgStartRace_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_100;
	}

	private void Relay_Save_Out_105()
	{
		Relay_Save_148();
	}

	private void Relay_Load_Out_105()
	{
		Relay_Load_148();
	}

	private void Relay_Restart_Out_105()
	{
		Relay_Set_False_148();
	}

	private void Relay_Save_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Save(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Load_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Load(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Set_True_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Set_False_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Save_Out_106()
	{
	}

	private void Relay_Load_Out_106()
	{
		Relay_In_108();
	}

	private void Relay_Restart_Out_106()
	{
		Relay_False_195();
	}

	private void Relay_Save_106()
	{
		logic_SubGraph_SaveLoadInt_integer_106 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_106 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.Save(logic_SubGraph_SaveLoadInt_restartValue_106, ref logic_SubGraph_SaveLoadInt_integer_106, logic_SubGraph_SaveLoadInt_intAsVariable_106, logic_SubGraph_SaveLoadInt_uniqueID_106);
	}

	private void Relay_Load_106()
	{
		logic_SubGraph_SaveLoadInt_integer_106 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_106 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.Load(logic_SubGraph_SaveLoadInt_restartValue_106, ref logic_SubGraph_SaveLoadInt_integer_106, logic_SubGraph_SaveLoadInt_intAsVariable_106, logic_SubGraph_SaveLoadInt_uniqueID_106);
	}

	private void Relay_Restart_106()
	{
		logic_SubGraph_SaveLoadInt_integer_106 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_106 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_106.Restart(logic_SubGraph_SaveLoadInt_restartValue_106, ref logic_SubGraph_SaveLoadInt_integer_106, logic_SubGraph_SaveLoadInt_intAsVariable_106, logic_SubGraph_SaveLoadInt_uniqueID_106);
	}

	private void Relay_Out_108()
	{
	}

	private void Relay_In_108()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_108 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.In(logic_SubGraph_LoadObjectiveStates_currentObjective_108);
	}

	private void Relay_True_110()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_110.True(out logic_uScriptAct_SetBool_Target_110);
		local_MsgHalfwayRound_System_Boolean = logic_uScriptAct_SetBool_Target_110;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_110.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_False_110()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_110.False(out logic_uScriptAct_SetBool_Target_110);
		local_MsgHalfwayRound_System_Boolean = logic_uScriptAct_SetBool_Target_110;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_110.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_112()
	{
		logic_uScriptCon_CompareBool_Bool_112 = local_MsgHalfwayRound_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.In(logic_uScriptCon_CompareBool_Bool_112);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.False)
		{
			Relay_True_110();
		}
	}

	private void Relay_Save_Out_114()
	{
		Relay_Save_128();
	}

	private void Relay_Load_Out_114()
	{
		Relay_Load_128();
	}

	private void Relay_Restart_Out_114()
	{
		Relay_Set_False_128();
	}

	private void Relay_Save_114()
	{
		logic_SubGraph_SaveLoadBool_boolean_114 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_114 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Save(ref logic_SubGraph_SaveLoadBool_boolean_114, logic_SubGraph_SaveLoadBool_boolAsVariable_114, logic_SubGraph_SaveLoadBool_uniqueID_114);
	}

	private void Relay_Load_114()
	{
		logic_SubGraph_SaveLoadBool_boolean_114 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_114 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Load(ref logic_SubGraph_SaveLoadBool_boolean_114, logic_SubGraph_SaveLoadBool_boolAsVariable_114, logic_SubGraph_SaveLoadBool_uniqueID_114);
	}

	private void Relay_Set_True_114()
	{
		logic_SubGraph_SaveLoadBool_boolean_114 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_114 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_114, logic_SubGraph_SaveLoadBool_boolAsVariable_114, logic_SubGraph_SaveLoadBool_uniqueID_114);
	}

	private void Relay_Set_False_114()
	{
		logic_SubGraph_SaveLoadBool_boolean_114 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_114 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_114.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_114, logic_SubGraph_SaveLoadBool_boolAsVariable_114, logic_SubGraph_SaveLoadBool_uniqueID_114);
	}

	private void Relay_In_115()
	{
		logic_uScriptCon_CompareBool_Bool_115 = local_MissionComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.In(logic_uScriptCon_CompareBool_Bool_115);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.False;
		if (num)
		{
			Relay_In_35();
		}
		if (flag)
		{
			Relay_In_63();
		}
	}

	private void Relay_True_118()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_118.True(out logic_uScriptAct_SetBool_Target_118);
		local_MissionComplete_System_Boolean = logic_uScriptAct_SetBool_Target_118;
	}

	private void Relay_False_118()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_118.False(out logic_uScriptAct_SetBool_Target_118);
		local_MissionComplete_System_Boolean = logic_uScriptAct_SetBool_Target_118;
	}

	private void Relay_True_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.True(out logic_uScriptAct_SetBool_Target_123);
		local_MsgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_123;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_123.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_False_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.False(out logic_uScriptAct_SetBool_Target_123);
		local_MsgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_123;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_123.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_True_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.True(out logic_uScriptAct_SetBool_Target_125);
		local_ChallengeEnded_System_Boolean = logic_uScriptAct_SetBool_Target_125;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_125.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_False_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.False(out logic_uScriptAct_SetBool_Target_125);
		local_ChallengeEnded_System_Boolean = logic_uScriptAct_SetBool_Target_125;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_125.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_Save_Out_128()
	{
		Relay_Save_129();
	}

	private void Relay_Load_Out_128()
	{
		Relay_Load_129();
	}

	private void Relay_Restart_Out_128()
	{
		Relay_Set_False_129();
	}

	private void Relay_Save_128()
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_128 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Save(ref logic_SubGraph_SaveLoadBool_boolean_128, logic_SubGraph_SaveLoadBool_boolAsVariable_128, logic_SubGraph_SaveLoadBool_uniqueID_128);
	}

	private void Relay_Load_128()
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_128 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Load(ref logic_SubGraph_SaveLoadBool_boolean_128, logic_SubGraph_SaveLoadBool_boolAsVariable_128, logic_SubGraph_SaveLoadBool_uniqueID_128);
	}

	private void Relay_Set_True_128()
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_128 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_128, logic_SubGraph_SaveLoadBool_boolAsVariable_128, logic_SubGraph_SaveLoadBool_uniqueID_128);
	}

	private void Relay_Set_False_128()
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_128 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_128, logic_SubGraph_SaveLoadBool_boolAsVariable_128, logic_SubGraph_SaveLoadBool_uniqueID_128);
	}

	private void Relay_Save_Out_129()
	{
		Relay_Save_106();
	}

	private void Relay_Load_Out_129()
	{
		Relay_Load_106();
	}

	private void Relay_Restart_Out_129()
	{
		Relay_Restart_106();
	}

	private void Relay_Save_129()
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_129 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Save(ref logic_SubGraph_SaveLoadBool_boolean_129, logic_SubGraph_SaveLoadBool_boolAsVariable_129, logic_SubGraph_SaveLoadBool_uniqueID_129);
	}

	private void Relay_Load_129()
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_129 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Load(ref logic_SubGraph_SaveLoadBool_boolean_129, logic_SubGraph_SaveLoadBool_boolAsVariable_129, logic_SubGraph_SaveLoadBool_uniqueID_129);
	}

	private void Relay_Set_True_129()
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_129 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_129, logic_SubGraph_SaveLoadBool_boolAsVariable_129, logic_SubGraph_SaveLoadBool_uniqueID_129);
	}

	private void Relay_Set_False_129()
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_129 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_129, logic_SubGraph_SaveLoadBool_boolAsVariable_129, logic_SubGraph_SaveLoadBool_uniqueID_129);
	}

	private void Relay_In_132()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_132 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_132 = distLeavingMission;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_132.In(logic_uScript_IsPlayerInRangeOfTech_tech_132, logic_uScript_IsPlayerInRangeOfTech_range_132, logic_uScript_IsPlayerInRangeOfTech_techs_132);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_132.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_132.OutOfRange;
		if (inRange)
		{
			Relay_In_146();
		}
		if (outOfRange)
		{
			Relay_In_197();
		}
	}

	private void Relay_True_134()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_134.True(out logic_uScriptAct_SetBool_Target_134);
		local_ChallengeInProgess_System_Boolean = logic_uScriptAct_SetBool_Target_134;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_134.Out)
		{
			Relay_False_45();
		}
	}

	private void Relay_False_134()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_134.False(out logic_uScriptAct_SetBool_Target_134);
		local_ChallengeInProgess_System_Boolean = logic_uScriptAct_SetBool_Target_134;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_134.Out)
		{
			Relay_False_45();
		}
	}

	private void Relay_True_136()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.True(out logic_uScriptAct_SetBool_Target_136);
		local_ChallengeInProgess_System_Boolean = logic_uScriptAct_SetBool_Target_136;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_136.Out)
		{
			Relay_True_125();
		}
	}

	private void Relay_False_136()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.False(out logic_uScriptAct_SetBool_Target_136);
		local_ChallengeInProgess_System_Boolean = logic_uScriptAct_SetBool_Target_136;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_136.Out)
		{
			Relay_True_125();
		}
	}

	private void Relay_In_138()
	{
		logic_uScriptCon_CompareBool_Bool_138 = local_ChallengeInProgess_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_138.In(logic_uScriptCon_CompareBool_Bool_138);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_138.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_138.False;
		if (num)
		{
			Relay_In_199();
		}
		if (flag)
		{
			Relay_In_132();
		}
	}

	private void Relay_In_140()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_140 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_140.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_140, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_140);
	}

	private void Relay_In_142()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_142 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_142.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_142, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_142);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_142.Out)
		{
			Relay_True_123();
		}
	}

	private void Relay_True_144()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_144.True(out logic_uScriptAct_SetBool_Target_144);
		local_MsgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_144;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_144.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_False_144()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_144.False(out logic_uScriptAct_SetBool_Target_144);
		local_MsgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_144;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_144.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_146()
	{
		logic_uScriptCon_CompareBool_Bool_146 = local_MsgIntro_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.In(logic_uScriptCon_CompareBool_Bool_146);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.False;
		if (num)
		{
			Relay_In_68();
		}
		if (flag)
		{
			Relay_True_144();
		}
	}

	private void Relay_Save_Out_148()
	{
		Relay_Save_114();
	}

	private void Relay_Load_Out_148()
	{
		Relay_Load_114();
	}

	private void Relay_Restart_Out_148()
	{
		Relay_Set_False_114();
	}

	private void Relay_Save_148()
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_148 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Save(ref logic_SubGraph_SaveLoadBool_boolean_148, logic_SubGraph_SaveLoadBool_boolAsVariable_148, logic_SubGraph_SaveLoadBool_uniqueID_148);
	}

	private void Relay_Load_148()
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_148 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Load(ref logic_SubGraph_SaveLoadBool_boolean_148, logic_SubGraph_SaveLoadBool_boolAsVariable_148, logic_SubGraph_SaveLoadBool_uniqueID_148);
	}

	private void Relay_Set_True_148()
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_148 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_148, logic_SubGraph_SaveLoadBool_boolAsVariable_148, logic_SubGraph_SaveLoadBool_uniqueID_148);
	}

	private void Relay_Set_False_148()
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_148 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_148, logic_SubGraph_SaveLoadBool_boolAsVariable_148, logic_SubGraph_SaveLoadBool_uniqueID_148);
	}

	private void Relay_In_149()
	{
		logic_uScript_SetTankInvulnerable_tank_149 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_149.In(logic_uScript_SetTankInvulnerable_invulnerable_149, logic_uScript_SetTankInvulnerable_tank_149);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_149.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_151()
	{
		logic_uScriptCon_CompareBool_Bool_151 = local_ChallengeEnded_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_151.In(logic_uScriptCon_CompareBool_Bool_151);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_151.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_151.False;
		if (num)
		{
			Relay_In_161();
		}
		if (flag)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_154()
	{
		logic_uScript_AddMessage_messageData_154 = msgRaceStartedEarly;
		logic_uScript_AddMessage_speaker_154 = messageSpeaker;
		logic_uScript_AddMessage_Return_154 = logic_uScript_AddMessage_uScript_AddMessage_154.In(logic_uScript_AddMessage_messageData_154, logic_uScript_AddMessage_speaker_154);
		if (logic_uScript_AddMessage_uScript_AddMessage_154.Out)
		{
			Relay_True_159();
		}
	}

	private void Relay_In_156()
	{
		logic_uScriptCon_CompareBool_Bool_156 = local_MsgNPCFound_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156.In(logic_uScriptCon_CompareBool_Bool_156);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156.False;
		if (num)
		{
			Relay_In_91();
		}
		if (flag)
		{
			Relay_In_154();
		}
	}

	private void Relay_True_159()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.True(out logic_uScriptAct_SetBool_Target_159);
		local_MsgNPCFound_System_Boolean = logic_uScriptAct_SetBool_Target_159;
	}

	private void Relay_False_159()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.False(out logic_uScriptAct_SetBool_Target_159);
		local_MsgNPCFound_System_Boolean = logic_uScriptAct_SetBool_Target_159;
	}

	private void Relay_In_161()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_161 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_161 = distNearNPC;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_161.In(logic_uScript_IsPlayerInRangeOfTech_tech_161, logic_uScript_IsPlayerInRangeOfTech_range_161, logic_uScript_IsPlayerInRangeOfTech_techs_161);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_161.InRange)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_164()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_164 = owner_Connection_163;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_164.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_164);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_164.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_165()
	{
		logic_uScript_SetEncounterTarget_owner_165 = owner_Connection_166;
		logic_uScript_SetEncounterTarget_visibleObject_165 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_165.In(logic_uScript_SetEncounterTarget_owner_165, logic_uScript_SetEncounterTarget_visibleObject_165);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_165.Out)
		{
			Relay_In_164();
		}
	}

	private void Relay_In_168()
	{
		logic_uScript_ShowHint_hintId_168 = local_169_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_168.In(logic_uScript_ShowHint_hintId_168);
	}

	private void Relay_In_170()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_170 = local_169_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_170.In(logic_uScript_HasHintBeenShownBefore_hintID_170);
		if (logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_170.NotShown)
		{
			Relay_In_168();
		}
	}

	private void Relay_In_172()
	{
		logic_uScript_AddMessage_messageData_172 = msgQuitFromMenu;
		logic_uScript_AddMessage_speaker_172 = messageSpeaker;
		logic_uScript_AddMessage_Return_172 = logic_uScript_AddMessage_uScript_AddMessage_172.In(logic_uScript_AddMessage_messageData_172, logic_uScript_AddMessage_speaker_172);
	}

	private void Relay_In_175()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_175 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_175.In(logic_uScript_CompareCheckpointChallengeEndReason_result_175, logic_uScript_CompareCheckpointChallengeEndReason_expected_175);
		bool equalTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_175.EqualTo;
		bool notEqualTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_175.NotEqualTo;
		if (equalTo)
		{
			Relay_In_188();
		}
		if (notEqualTo)
		{
			Relay_In_186();
		}
	}

	private void Relay_In_180()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_180 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_180.In(logic_uScript_CompareCheckpointChallengeEndReason_result_180, logic_uScript_CompareCheckpointChallengeEndReason_expected_180);
		if (logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_180.EqualTo)
		{
			Relay_In_172();
		}
	}

	private void Relay_OnSuccess_181()
	{
		local_EndReason_CheckpointChallenge_EndReason = event_UnityEngine_GameObject_EndReason_181;
	}

	private void Relay_OnFail_181()
	{
		local_EndReason_CheckpointChallenge_EndReason = event_UnityEngine_GameObject_EndReason_181;
	}

	private void Relay_In_184()
	{
		logic_uScript_AddMessage_messageData_184 = msgOutOfTime;
		logic_uScript_AddMessage_speaker_184 = messageSpeaker;
		logic_uScript_AddMessage_Return_184 = logic_uScript_AddMessage_uScript_AddMessage_184.In(logic_uScript_AddMessage_messageData_184, logic_uScript_AddMessage_speaker_184);
	}

	private void Relay_In_186()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_186 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_186.In(logic_uScript_CompareCheckpointChallengeEndReason_result_186, logic_uScript_CompareCheckpointChallengeEndReason_expected_186);
		bool equalTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_186.EqualTo;
		bool notEqualTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_186.NotEqualTo;
		if (equalTo)
		{
			Relay_In_184();
		}
		if (notEqualTo)
		{
			Relay_In_180();
		}
	}

	private void Relay_In_188()
	{
		logic_uScript_AddMessage_messageData_188 = msgOutOfBounds;
		logic_uScript_AddMessage_speaker_188 = messageSpeaker;
		logic_uScript_AddMessage_Return_188 = logic_uScript_AddMessage_uScript_AddMessage_188.In(logic_uScript_AddMessage_messageData_188, logic_uScript_AddMessage_speaker_188);
	}

	private void Relay_In_190()
	{
		logic_uScript_FlyTechUpAndAway_tech_190 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_190 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_190 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_190.In(logic_uScript_FlyTechUpAndAway_tech_190, logic_uScript_FlyTechUpAndAway_maxLifetime_190, logic_uScript_FlyTechUpAndAway_targetHeight_190, logic_uScript_FlyTechUpAndAway_aiTree_190, logic_uScript_FlyTechUpAndAway_removalParticles_190);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_190.Out)
		{
			Relay_Succeed_27();
		}
	}

	private void Relay_In_194()
	{
		logic_uScriptCon_CompareBool_Bool_194 = local_ChallengeInProgess_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.In(logic_uScriptCon_CompareBool_Bool_194);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.True)
		{
			Relay_In_112();
		}
	}

	private void Relay_True_195()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_195.True(out logic_uScriptAct_SetBool_Target_195);
		local_ChallengeInProgess_System_Boolean = logic_uScriptAct_SetBool_Target_195;
	}

	private void Relay_False_195()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_195.False(out logic_uScriptAct_SetBool_Target_195);
		local_ChallengeInProgess_System_Boolean = logic_uScriptAct_SetBool_Target_195;
	}

	private void Relay_In_197()
	{
		logic_uScriptCon_CompareBool_Bool_197 = local_MissionComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.In(logic_uScriptCon_CompareBool_Bool_197);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.False;
		if (num)
		{
			Relay_In_35();
		}
		if (flag)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_199()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199.Out)
		{
			Relay_In_68();
		}
	}
}
