using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_BetterFutureRaceTrackNew : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool _DEBUGIgnoreMoneyCheck;

	[Multiline(3)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius;

	public BlockTypes[] discoverableBlockTypesOnVehicle = new BlockTypes[0];

	public float distNearNPC;

	public SpawnTechData[] HoverNPCSpawnData = new SpawnTechData[0];

	public BlockTypes interactableBlockType;

	private int local_123_System_Int32;

	private bool local_204_System_Boolean;

	private TankBlock local_206_TankBlock;

	private Tank[] local_272_TankArray = new Tank[0];

	private Tank[] local_283_TankArray = new Tank[0];

	private Tank[] local_39_TankArray = new Tank[0];

	private Tank[] local_73_TankArray = new Tank[0];

	private string local_ChallengeID_System_String = "";

	private bool local_ChallengeInProgress_System_Boolean;

	private int local_CurrentMoney_System_Int32;

	private CheckpointChallenge.EndReason local_EndReason_CheckpointChallenge_EndReason;

	private bool local_HasEnoughMoney_System_Boolean;

	private Tank local_HoverNPCTech_Tank;

	private TankBlock local_HoverTerminalBlock_TankBlock;

	private bool local_Init_System_Boolean;

	private bool local_IntroMsgPlayed_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgReadyToBegin_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgReadyToBegin_Pad_ManOnScreenMessages_OnScreenMessage;

	private bool local_OutOfBounds_System_Boolean;

	private int local_PassedCheckpointIdx_System_Int32;

	private bool local_RaceAttempted_System_Boolean;

	private bool local_RaceStartedMsgShown_System_Boolean;

	private bool local_RampSetup_System_Boolean;

	private TrackSpline local_Spline_TrackSpline;

	private int local_Stage_System_Int32 = 1;

	private GameObject local_StarterObject_UnityEngine_GameObject;

	private GameObject local_StarterObject_UnityEngine_GameObject_previous;

	private Transform local_StartTransform_UnityEngine_Transform;

	private bool local_SwitchedVehicle_System_Boolean;

	private Tank local_Vehicle_Tank;

	private bool local_VehiclePurchased_System_Boolean;

	private bool local_WaitingOnPrompt_System_Boolean;

	private bool local_WorldCapacityFull_System_Boolean;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTagControls = "";

	[Multiline(3)]
	public string messageTagIntro = "";

	[Multiline(3)]
	public string messageTagPurchase = "";

	public uScript_AddMessage.MessageData msgIntro;

	public uScript_AddMessage.MessageData msgOutOfBounds;

	public uScript_AddMessage.MessageData msgOutOfTime;

	public LocalisedString msgPromptAccept;

	public LocalisedString msgPromptDecline;

	public LocalisedString msgPromptNoMoney;

	public LocalisedString msgPromptText;

	public uScript_AddMessage.MessageData msgQuitFromMenu;

	public uScript_AddMessage.MessageData msgRaceComplete;

	public uScript_AddMessage.MessageData msgRaceStarted;

	public uScript_AddMessage.MessageData msgReadyToBegin;

	public uScript_AddMessage.MessageData msgReadyToBegin_Pad;

	public LocalisedString msgWorldCapacityFull;

	public uScript_AddMessage.MessageData msgWorldCapacityFullReply;

	[Multiline(3)]
	public string raceStartPosition = "";

	[Multiline(3)]
	public string terrainObjectName = "";

	public TerrainObject terrainObjectPrefab;

	public int vehicleCost;

	public SpawnTechData[] vehicleSpawnData = new SpawnTechData[0];

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_16;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_24;

	private GameObject owner_Connection_31;

	private GameObject owner_Connection_35;

	private GameObject owner_Connection_41;

	private GameObject owner_Connection_44;

	private GameObject owner_Connection_61;

	private GameObject owner_Connection_69;

	private GameObject owner_Connection_70;

	private GameObject owner_Connection_99;

	private GameObject owner_Connection_199;

	private GameObject owner_Connection_208;

	private GameObject owner_Connection_262;

	private GameObject owner_Connection_267;

	private GameObject owner_Connection_276;

	private GameObject owner_Connection_282;

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

	private string logic_SubGraph_SaveLoadBool_uniqueID_7 = "Init";

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

	private uScript_GetEncounterSpline logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_28 = new uScript_GetEncounterSpline();

	private GameObject logic_uScript_GetEncounterSpline_owner_28;

	private TrackSpline logic_uScript_GetEncounterSpline_Return_28;

	private bool logic_uScript_GetEncounterSpline_Out_28 = true;

	private uScript_InitChallengeStarterWithEncounterChallengeData logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_29 = new uScript_InitChallengeStarterWithEncounterChallengeData();

	private GameObject logic_uScript_InitChallengeStarterWithEncounterChallengeData_owner_29;

	private GameObject logic_uScript_InitChallengeStarterWithEncounterChallengeData_targetChallengeStarterObject_29;

	private bool logic_uScript_InitChallengeStarterWithEncounterChallengeData_Out_29 = true;

	private uScript_GetChallengeStartTransform logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_30 = new uScript_GetChallengeStartTransform();

	private GameObject logic_uScript_GetChallengeStartTransform_challengeStarterObject_30;

	private Transform logic_uScript_GetChallengeStartTransform_Return_30;

	private bool logic_uScript_GetChallengeStartTransform_Out_30 = true;

	private uScript_GetChallengeIDFromChallengeStarter logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_37 = new uScript_GetChallengeIDFromChallengeStarter();

	private GameObject logic_uScript_GetChallengeIDFromChallengeStarter_challengeStarterObject_37;

	private string logic_uScript_GetChallengeIDFromChallengeStarter_Return_37;

	private bool logic_uScript_GetChallengeIDFromChallengeStarter_Out_37 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_40 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_40;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_40 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_40;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_40 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_40 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_40 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_40 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_43 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_43 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_43;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_43;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_43;

	private bool logic_uScript_SpawnTechsFromData_Out_43 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_46 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_46 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_46;

	private bool logic_uScript_SetTankInvulnerable_Out_46 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_47 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_47 = new Tank[0];

	private int logic_uScript_AccessListTech_index_47;

	private Tank logic_uScript_AccessListTech_value_47;

	private bool logic_uScript_AccessListTech_Out_47 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_50;

	private bool logic_uScriptCon_CompareBool_True_50 = true;

	private bool logic_uScriptCon_CompareBool_False_50 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_52 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_52;

	private bool logic_uScriptAct_SetBool_Out_52 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_52 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_52 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_53 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_53;

	private bool logic_uScriptAct_SetBool_Out_53 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_53 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_53 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_55 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_55;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_55 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_55 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_56;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_60 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_60;

	private float logic_uScript_IsPlayerInRangeOfTech_range_60;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_60 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_60 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_60 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_60 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_62 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_62;

	private object logic_uScript_SetEncounterTarget_visibleObject_62 = "";

	private bool logic_uScript_SetEncounterTarget_Out_62 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_65;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_68 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_68 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_68;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_68 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_68;

	private bool logic_uScript_SpawnTechsFromData_Out_68 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_74 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_74 = new Tank[0];

	private int logic_uScript_AccessListTech_index_74;

	private Tank logic_uScript_AccessListTech_value_74;

	private bool logic_uScript_AccessListTech_Out_74 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_75 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_75;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_75 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_75;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_75 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_75 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_75 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_75 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_76 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_76;

	private bool logic_uScriptAct_SetBool_Out_76 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_76 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_76 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_78 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_78;

	private bool logic_uScriptAct_SetBool_Out_78 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_78 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_78 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_80 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_80;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_80;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_80;

	private bool logic_uScript_AddMessage_Out_80 = true;

	private bool logic_uScript_AddMessage_Shown_80 = true;

	private uScript_GetLastChallengeResult logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_81 = new uScript_GetLastChallengeResult();

	private bool logic_uScript_GetLastChallengeResult_Success_81 = true;

	private bool logic_uScript_GetLastChallengeResult_Failure_81 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_84 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_84;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_84;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_84;

	private bool logic_uScript_AddMessage_Out_84 = true;

	private bool logic_uScript_AddMessage_Shown_84 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_85 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_85;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_85 = CheckpointChallenge.EndReason.FailedQuit;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_85 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_85 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_90 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_90;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_90 = CheckpointChallenge.EndReason.FailedOutOfBounds;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_90 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_90 = true;

	private uScript_ClearChallengeStarterChallengeData logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_91 = new uScript_ClearChallengeStarterChallengeData();

	private GameObject logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_91;

	private bool logic_uScript_ClearChallengeStarterChallengeData_Out_91 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_92 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_92;

	private bool logic_uScript_FinishEncounter_Out_92 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_93 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_93;

	private bool logic_uScriptAct_SetBool_Out_93 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_93 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_93 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_96 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_96;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_96;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_96;

	private bool logic_uScript_AddMessage_Out_96 = true;

	private bool logic_uScript_AddMessage_Shown_96 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_101 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_101;

	private bool logic_uScript_RemoveTech_Out_101 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_104 = true;

	private uScript_GetChallengeStateFromChallengeID logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_106 = new uScript_GetChallengeStateFromChallengeID();

	private string logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_106 = "";

	private bool logic_uScript_GetChallengeStateFromChallengeID_Out_106 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_NotRunning_106 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_JustStarted_106 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_InProgress_106 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_JustEnded_106 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_112;

	private bool logic_uScriptCon_CompareBool_True_112 = true;

	private bool logic_uScriptCon_CompareBool_False_112 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_114 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_114 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_117 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_117 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_117 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_117 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_117 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_118 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_118 = "";

	private bool logic_uScript_EnableGlow_enable_118 = true;

	private bool logic_uScript_EnableGlow_Out_118 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_122 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_122;

	private bool logic_uScriptCon_CompareBool_True_122 = true;

	private bool logic_uScriptCon_CompareBool_False_122 = true;

	private uScriptAct_MultiplyInt_v2 logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_124 = new uScriptAct_MultiplyInt_v2();

	private int logic_uScriptAct_MultiplyInt_v2_A_124;

	private int logic_uScriptAct_MultiplyInt_v2_B_124 = -1;

	private int logic_uScriptAct_MultiplyInt_v2_IntResult_124;

	private float logic_uScriptAct_MultiplyInt_v2_FloatResult_124;

	private bool logic_uScriptAct_MultiplyInt_v2_Out_124 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_125 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_125;

	private bool logic_uScriptAct_SetBool_Out_125 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_125 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_125 = true;

	private uScript_AddMoney logic_uScript_AddMoney_uScript_AddMoney_126 = new uScript_AddMoney();

	private int logic_uScript_AddMoney_amount_126;

	private bool logic_uScript_AddMoney_Out_126 = true;

	private uScript_DiscoverBlocks logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_129 = new uScript_DiscoverBlocks();

	private BlockTypes[] logic_uScript_DiscoverBlocks_blockTypes_129 = new BlockTypes[0];

	private bool logic_uScript_DiscoverBlocks_Out_129 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_131;

	private bool logic_uScriptCon_CompareBool_True_131 = true;

	private bool logic_uScriptCon_CompareBool_False_131 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_133 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_133 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_135 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_135 = "";

	private bool logic_uScript_EnableGlow_enable_135;

	private bool logic_uScript_EnableGlow_Out_135 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_136 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_136 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_137 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_137 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_140 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_140;

	private float logic_uScript_IsPlayerInRangeOfTech_range_140;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_140 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_140 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_140 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_140 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_142 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_142 = "";

	private bool logic_uScript_EnableGlow_enable_142;

	private bool logic_uScript_EnableGlow_Out_142 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_143 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_143 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_146 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_146;

	private BlockTypes logic_uScript_GetTankBlock_blockType_146;

	private TankBlock logic_uScript_GetTankBlock_Return_146;

	private bool logic_uScript_GetTankBlock_Out_146 = true;

	private bool logic_uScript_GetTankBlock_Returned_146 = true;

	private bool logic_uScript_GetTankBlock_NotFound_146 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_148 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_148;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_148 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_148 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_149;

	private bool logic_uScriptCon_CompareBool_True_149 = true;

	private bool logic_uScriptCon_CompareBool_False_149 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_152 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_152;

	private bool logic_uScriptAct_SetBool_Out_152 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_152 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_152 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_154;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_154 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_154 = "VehiclePurchased";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_155;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_155 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_155 = "SwitchedVehicle";

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_158 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_158;

	private bool logic_uScript_IsTechPlayer_Out_158 = true;

	private bool logic_uScript_IsTechPlayer_True_158 = true;

	private bool logic_uScript_IsTechPlayer_False_158 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_159;

	private bool logic_uScriptCon_CompareBool_True_159 = true;

	private bool logic_uScriptCon_CompareBool_False_159 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_162;

	private bool logic_uScriptCon_CompareBool_True_162 = true;

	private bool logic_uScriptCon_CompareBool_False_162 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_163 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_165 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_165;

	private bool logic_uScriptAct_SetBool_Out_165 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_165 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_165 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_167 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_167;

	private bool logic_uScriptAct_SetBool_Out_167 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_167 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_167 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_168 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_168;

	private bool logic_uScriptAct_SetBool_Out_168 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_168 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_168 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_171 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_171;

	private bool logic_uScriptAct_SetBool_Out_171 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_171 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_171 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_173 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_173;

	private bool logic_uScriptAct_SetBool_Out_173 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_173 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_173 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_175;

	private bool logic_uScriptCon_CompareBool_True_175 = true;

	private bool logic_uScriptCon_CompareBool_False_175 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_176 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_176 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_177;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_177 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_177 = "RaceAttempted";

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_180 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_180;

	private float logic_uScript_IsPlayerInRangeOfTech_range_180;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_180 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_180 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_180 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_180 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_182;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_182;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_182;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_182;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_182;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_188 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_188 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_188 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_188 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_190 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_190 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_190 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_190 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_192 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_192 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_192 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_192 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_195 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_195 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_195 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_195 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_196 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_196 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_196 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_196 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_198 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_198;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_198 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_198;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_198 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_198 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_198 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_198 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_201 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_201 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_202 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_202 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_207 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_207;

	private TankBlock logic_uScript_CompareBlock_B_207;

	private bool logic_uScript_CompareBlock_EqualTo_207 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_207 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_209 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_209;

	private bool logic_uScriptCon_CompareBool_True_209 = true;

	private bool logic_uScriptCon_CompareBool_False_209 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_211;

	private bool logic_uScriptCon_CompareBool_True_211 = true;

	private bool logic_uScriptCon_CompareBool_False_211 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_212 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_212;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_212;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_212;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_212;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_212 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_212;

	private bool logic_uScript_MissionPromptBlock_Show_Out_212 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_214 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_214;

	private bool logic_uScriptAct_SetBool_Out_214 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_214 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_214 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_215 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_215;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_215 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_216 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_216;

	private int logic_uScriptCon_CompareInt_B_216;

	private bool logic_uScriptCon_CompareInt_GreaterThan_216 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_216 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_216 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_216 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_216 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_216 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_219 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_219 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_223 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_223;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_223;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_223;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_223;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_223 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_223;

	private bool logic_uScript_MissionPromptBlock_Show_Out_223 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_229 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_229;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_229 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_231 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_231 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_231 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_234 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_234;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_234;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_234;

	private bool logic_uScript_AddMessage_Out_234 = true;

	private bool logic_uScript_AddMessage_Shown_234 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_237;

	private bool logic_uScriptCon_CompareBool_True_237 = true;

	private bool logic_uScriptCon_CompareBool_False_237 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_239 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_239;

	private bool logic_uScriptAct_SetBool_Out_239 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_239 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_239 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_242 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_242 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_244;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_244 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_244 = "IntroMsgPlayed";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_246;

	private bool logic_uScriptCon_CompareBool_True_246 = true;

	private bool logic_uScriptCon_CompareBool_False_246 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_250 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_250;

	private bool logic_uScriptAct_SetBool_Out_250 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_250 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_250 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_252 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_252;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_252;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_253 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_253;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_253;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_253;

	private bool logic_uScript_AddMessage_Out_253 = true;

	private bool logic_uScript_AddMessage_Shown_253 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_255;

	private bool logic_uScriptCon_CompareBool_True_255 = true;

	private bool logic_uScriptCon_CompareBool_False_255 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_256 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_256 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_257 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_257;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_257;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_257;

	private bool logic_uScript_AddMessage_Out_257 = true;

	private bool logic_uScript_AddMessage_Shown_257 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_261 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_261;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_261 = CheckpointChallenge.EndReason.FailedTimeUp;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_261 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_261 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_263 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_263 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_264 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_264;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_264 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_264 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_264 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_265;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_265 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_265 = "RaceStartedMsgShown";

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_268 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_268;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_268 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_269 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_269 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_270 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_270 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_271 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_273 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_273 = new Tank[0];

	private int logic_uScript_AccessListTech_index_273;

	private Tank logic_uScript_AccessListTech_value_273;

	private bool logic_uScript_AccessListTech_Out_273 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_275 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_275 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_275;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_275 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_275;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_275 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_275 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_275 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_275 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_279 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_279 = new Tank[0];

	private int logic_uScript_AccessListTech_index_279;

	private Tank logic_uScript_AccessListTech_value_279;

	private bool logic_uScript_AccessListTech_Out_279 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_280 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_280 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_280;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_280 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_280;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_280 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_280 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_280 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_280 = true;

	private uScript_CanSpawnPlayerTechsWithinBlockLimit logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_284 = new uScript_CanSpawnPlayerTechsWithinBlockLimit();

	private SpawnTechData[] logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_284 = new SpawnTechData[0];

	private int logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_284 = 1;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_Out_284 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_True_284 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_False_284 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_286 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_286;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_286;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_286;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_286;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_286;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_286;

	private bool logic_uScript_MissionPromptBlock_Show_Out_286 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_290 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_290;

	private bool logic_uScriptAct_SetBool_Out_290 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_290 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_290 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_293 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_293;

	private bool logic_uScriptAct_SetBool_Out_293 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_293 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_293 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_296 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_296;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_296;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_296;

	private bool logic_uScript_AddMessage_Out_296 = true;

	private bool logic_uScript_AddMessage_Shown_296 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_298 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_298;

	private bool logic_uScriptCon_CompareBool_True_298 = true;

	private bool logic_uScriptCon_CompareBool_False_298 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_299 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_299 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_301;

	private bool logic_uScriptCon_CompareBool_True_301 = true;

	private bool logic_uScriptCon_CompareBool_False_301 = true;

	private int event_UnityEngine_GameObject_CheckpointIndex_25;

	private TankBlock event_UnityEngine_GameObject_TankBlock_205;

	private bool event_UnityEngine_GameObject_Accepted_205;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (local_StarterObject_UnityEngine_GameObject_previous != local_StarterObject_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StarterObject_UnityEngine_GameObject_previous = local_StarterObject_UnityEngine_GameObject;
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
		if (null == owner_Connection_31 || !m_RegisteredForEvents)
		{
			owner_Connection_31 = parentGameObject;
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
		if (null == owner_Connection_41 || !m_RegisteredForEvents)
		{
			owner_Connection_41 = parentGameObject;
		}
		if (null == owner_Connection_44 || !m_RegisteredForEvents)
		{
			owner_Connection_44 = parentGameObject;
		}
		if (null == owner_Connection_61 || !m_RegisteredForEvents)
		{
			owner_Connection_61 = parentGameObject;
		}
		if (null == owner_Connection_69 || !m_RegisteredForEvents)
		{
			owner_Connection_69 = parentGameObject;
		}
		if (null == owner_Connection_70 || !m_RegisteredForEvents)
		{
			owner_Connection_70 = parentGameObject;
		}
		if (null == owner_Connection_99 || !m_RegisteredForEvents)
		{
			owner_Connection_99 = parentGameObject;
		}
		if (null == owner_Connection_199 || !m_RegisteredForEvents)
		{
			owner_Connection_199 = parentGameObject;
		}
		if (null == owner_Connection_208 || !m_RegisteredForEvents)
		{
			owner_Connection_208 = parentGameObject;
			if (null != owner_Connection_208)
			{
				uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_208.GetComponent<uScript_MissionPromptBlock_OnResult>();
				if (null == uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2 = owner_Connection_208.AddComponent<uScript_MissionPromptBlock_OnResult>();
				}
				if (null != uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_205;
				}
			}
		}
		if (null == owner_Connection_262 || !m_RegisteredForEvents)
		{
			owner_Connection_262 = parentGameObject;
		}
		if (null == owner_Connection_267 || !m_RegisteredForEvents)
		{
			owner_Connection_267 = parentGameObject;
		}
		if (null == owner_Connection_276 || !m_RegisteredForEvents)
		{
			owner_Connection_276 = parentGameObject;
		}
		if (null == owner_Connection_282 || !m_RegisteredForEvents)
		{
			owner_Connection_282 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (local_StarterObject_UnityEngine_GameObject_previous != local_StarterObject_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StarterObject_UnityEngine_GameObject_previous = local_StarterObject_UnityEngine_GameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_208)
		{
			uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_208.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null == uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2 = owner_Connection_208.AddComponent<uScript_MissionPromptBlock_OnResult>();
			}
			if (null != uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_205;
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
		if (null != owner_Connection_208)
		{
			uScript_MissionPromptBlock_OnResult component5 = owner_Connection_208.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null != component5)
			{
				component5.ResponseEvent -= Instance_ResponseEvent_205;
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
		logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_28.SetParent(g);
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_29.SetParent(g);
		logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_30.SetParent(g);
		logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_37.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_43.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_46.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_47.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_52.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_60.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_62.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_68.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_74.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_76.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_80.SetParent(g);
		logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_81.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_84.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_85.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_90.SetParent(g);
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_91.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_92.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_96.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_101.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104.SetParent(g);
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_106.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_114.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_117.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_118.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_122.SetParent(g);
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_124.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.SetParent(g);
		logic_uScript_AddMoney_uScript_AddMoney_126.SetParent(g);
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_129.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_133.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_135.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_136.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_137.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_140.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_142.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_143.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_146.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_148.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_152.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_158.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_165.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_167.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_168.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_173.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_176.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_180.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_188.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_190.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_192.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_195.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_196.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_201.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_202.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_207.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_209.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_212.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_215.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_216.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_219.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_223.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_229.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_231.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_234.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_239.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_242.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_250.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_252.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_253.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_256.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_257.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_261.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_263.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_264.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_268.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_269.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_270.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_273.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_275.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_279.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_280.SetParent(g);
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_284.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_286.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_290.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_296.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_298.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_299.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301.SetParent(g);
		owner_Connection_3 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_16 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_24 = parentGameObject;
		owner_Connection_31 = parentGameObject;
		owner_Connection_35 = parentGameObject;
		owner_Connection_41 = parentGameObject;
		owner_Connection_44 = parentGameObject;
		owner_Connection_61 = parentGameObject;
		owner_Connection_69 = parentGameObject;
		owner_Connection_70 = parentGameObject;
		owner_Connection_99 = parentGameObject;
		owner_Connection_199 = parentGameObject;
		owner_Connection_208 = parentGameObject;
		owner_Connection_262 = parentGameObject;
		owner_Connection_267 = parentGameObject;
		owner_Connection_276 = parentGameObject;
		owner_Connection_282 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_252.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Save_Out += SubGraph_SaveLoadInt_Save_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Load_Out += SubGraph_SaveLoadInt_Load_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_55;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.Out += SubGraph_LoadObjectiveStates_Out_56;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output1 += uScriptCon_ManualSwitch_Output1_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output2 += uScriptCon_ManualSwitch_Output2_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output3 += uScriptCon_ManualSwitch_Output3_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output4 += uScriptCon_ManualSwitch_Output4_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output5 += uScriptCon_ManualSwitch_Output5_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output6 += uScriptCon_ManualSwitch_Output6_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output7 += uScriptCon_ManualSwitch_Output7_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output8 += uScriptCon_ManualSwitch_Output8_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Save_Out += SubGraph_SaveLoadBool_Save_Out_154;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Load_Out += SubGraph_SaveLoadBool_Load_Out_154;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_154;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Save_Out += SubGraph_SaveLoadBool_Save_Out_155;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Load_Out += SubGraph_SaveLoadBool_Load_Out_155;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_155;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Save_Out += SubGraph_SaveLoadBool_Save_Out_177;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Load_Out += SubGraph_SaveLoadBool_Load_Out_177;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_177;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182.Out += SubGraph_AddMessageWithPadSupport_Out_182;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182.Shown += SubGraph_AddMessageWithPadSupport_Shown_182;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Save_Out += SubGraph_SaveLoadBool_Save_Out_244;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Load_Out += SubGraph_SaveLoadBool_Load_Out_244;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_244;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_252.Out += SubGraph_CompleteObjectiveStage_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Save_Out += SubGraph_SaveLoadBool_Save_Out_265;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Load_Out += SubGraph_SaveLoadBool_Load_Out_265;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_265;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_252.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_252.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_268.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDisable();
		logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_9.OnDisable();
		logic_uScript_SpawnTerrainObject_uScript_SpawnTerrainObject_10.OnDisable();
		logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_28.OnDisable();
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_29.OnDisable();
		logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_30.OnDisable();
		logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_37.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_46.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_60.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_80.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_84.OnDisable();
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_91.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_96.OnDisable();
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_106.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_140.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_146.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_180.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_234.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_252.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_253.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_257.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_264.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.OnDisable();
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_284.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_296.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_252.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_252.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Save_Out -= SubGraph_SaveLoadInt_Save_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Load_Out -= SubGraph_SaveLoadInt_Load_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_55;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.Out -= SubGraph_LoadObjectiveStates_Out_56;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output1 -= uScriptCon_ManualSwitch_Output1_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output2 -= uScriptCon_ManualSwitch_Output2_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output3 -= uScriptCon_ManualSwitch_Output3_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output4 -= uScriptCon_ManualSwitch_Output4_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output5 -= uScriptCon_ManualSwitch_Output5_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output6 -= uScriptCon_ManualSwitch_Output6_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output7 -= uScriptCon_ManualSwitch_Output7_65;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.Output8 -= uScriptCon_ManualSwitch_Output8_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Save_Out -= SubGraph_SaveLoadBool_Save_Out_154;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Load_Out -= SubGraph_SaveLoadBool_Load_Out_154;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_154;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Save_Out -= SubGraph_SaveLoadBool_Save_Out_155;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Load_Out -= SubGraph_SaveLoadBool_Load_Out_155;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_155;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Save_Out -= SubGraph_SaveLoadBool_Save_Out_177;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Load_Out -= SubGraph_SaveLoadBool_Load_Out_177;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_177;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182.Out -= SubGraph_AddMessageWithPadSupport_Out_182;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182.Shown -= SubGraph_AddMessageWithPadSupport_Shown_182;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Save_Out -= SubGraph_SaveLoadBool_Save_Out_244;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Load_Out -= SubGraph_SaveLoadBool_Load_Out_244;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_244;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_252.Out -= SubGraph_CompleteObjectiveStage_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Save_Out -= SubGraph_SaveLoadBool_Save_Out_265;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Load_Out -= SubGraph_SaveLoadBool_Load_Out_265;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_265;
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

	private void Instance_ResponseEvent_205(object o, uScript_MissionPromptBlock_OnResult.PromptResultEventArgs e)
	{
		event_UnityEngine_GameObject_TankBlock_205 = e.TankBlock;
		event_UnityEngine_GameObject_Accepted_205 = e.Accepted;
		Relay_ResponseEvent_205();
	}

	private void SubGraph_SaveLoadBool_Save_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Save_Out_7();
	}

	private void SubGraph_SaveLoadBool_Load_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Load_Out_7();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Restart_Out_7();
	}

	private void SubGraph_SaveLoadInt_Save_Out_55(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_55 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_55;
		Relay_Save_Out_55();
	}

	private void SubGraph_SaveLoadInt_Load_Out_55(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_55 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_55;
		Relay_Load_Out_55();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_55(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_55 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_55;
		Relay_Restart_Out_55();
	}

	private void SubGraph_LoadObjectiveStates_Out_56(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_56();
	}

	private void uScriptCon_ManualSwitch_Output1_65(object o, EventArgs e)
	{
		Relay_Output1_65();
	}

	private void uScriptCon_ManualSwitch_Output2_65(object o, EventArgs e)
	{
		Relay_Output2_65();
	}

	private void uScriptCon_ManualSwitch_Output3_65(object o, EventArgs e)
	{
		Relay_Output3_65();
	}

	private void uScriptCon_ManualSwitch_Output4_65(object o, EventArgs e)
	{
		Relay_Output4_65();
	}

	private void uScriptCon_ManualSwitch_Output5_65(object o, EventArgs e)
	{
		Relay_Output5_65();
	}

	private void uScriptCon_ManualSwitch_Output6_65(object o, EventArgs e)
	{
		Relay_Output6_65();
	}

	private void uScriptCon_ManualSwitch_Output7_65(object o, EventArgs e)
	{
		Relay_Output7_65();
	}

	private void uScriptCon_ManualSwitch_Output8_65(object o, EventArgs e)
	{
		Relay_Output8_65();
	}

	private void SubGraph_SaveLoadBool_Save_Out_154(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_154 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_154;
		Relay_Save_Out_154();
	}

	private void SubGraph_SaveLoadBool_Load_Out_154(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_154 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_154;
		Relay_Load_Out_154();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_154(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_154 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_154;
		Relay_Restart_Out_154();
	}

	private void SubGraph_SaveLoadBool_Save_Out_155(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_155 = e.boolean;
		local_SwitchedVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_155;
		Relay_Save_Out_155();
	}

	private void SubGraph_SaveLoadBool_Load_Out_155(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_155 = e.boolean;
		local_SwitchedVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_155;
		Relay_Load_Out_155();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_155(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_155 = e.boolean;
		local_SwitchedVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_155;
		Relay_Restart_Out_155();
	}

	private void SubGraph_SaveLoadBool_Save_Out_177(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = e.boolean;
		local_RaceAttempted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_177;
		Relay_Save_Out_177();
	}

	private void SubGraph_SaveLoadBool_Load_Out_177(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = e.boolean;
		local_RaceAttempted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_177;
		Relay_Load_Out_177();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_177(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = e.boolean;
		local_RaceAttempted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_177;
		Relay_Restart_Out_177();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_182(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_182 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_182 = e.messageControlPadReturn;
		local_MsgReadyToBegin_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_182;
		local_MsgReadyToBegin_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_182;
		Relay_Out_182();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_182(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_182 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_182 = e.messageControlPadReturn;
		local_MsgReadyToBegin_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_182;
		local_MsgReadyToBegin_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_182;
		Relay_Shown_182();
	}

	private void SubGraph_SaveLoadBool_Save_Out_244(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = e.boolean;
		local_IntroMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_244;
		Relay_Save_Out_244();
	}

	private void SubGraph_SaveLoadBool_Load_Out_244(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = e.boolean;
		local_IntroMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_244;
		Relay_Load_Out_244();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_244(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = e.boolean;
		local_IntroMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_244;
		Relay_Restart_Out_244();
	}

	private void SubGraph_CompleteObjectiveStage_Out_252(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_252 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_252;
		Relay_Out_252();
	}

	private void SubGraph_SaveLoadBool_Save_Out_265(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_265 = e.boolean;
		local_RaceStartedMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_265;
		Relay_Save_Out_265();
	}

	private void SubGraph_SaveLoadBool_Load_Out_265(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_265 = e.boolean;
		local_RaceStartedMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_265;
		Relay_Load_Out_265();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_265(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_265 = e.boolean;
		local_RaceStartedMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_265;
		Relay_Restart_Out_265();
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
		Relay_Save_55();
	}

	private void Relay_LoadEvent_1()
	{
		Relay_Load_55();
	}

	private void Relay_RestartEvent_1()
	{
		Relay_Restart_55();
	}

	private void Relay_In_2()
	{
		logic_uScriptCon_CompareBool_Bool_2 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.In(logic_uScriptCon_CompareBool_Bool_2);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.False;
		if (num)
		{
			Relay_In_264();
		}
		if (flag)
		{
			Relay_In_10();
		}
	}

	private void Relay_True_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.True(out logic_uScriptAct_SetBool_Target_4);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_264();
		}
	}

	private void Relay_False_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.False(out logic_uScriptAct_SetBool_Target_4);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_264();
		}
	}

	private void Relay_Save_Out_7()
	{
		Relay_Save_265();
	}

	private void Relay_Load_Out_7()
	{
		Relay_Load_265();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_Set_False_265();
	}

	private void Relay_Save_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Load_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Set_True_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Set_False_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_In_9()
	{
		logic_uScript_GetSpawnedTerrainObject_ownerNode_9 = owner_Connection_16;
		logic_uScript_GetSpawnedTerrainObject_uniqueName_9 = terrainObjectName;
		logic_uScript_GetSpawnedTerrainObject_Return_9 = logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_9.In(logic_uScript_GetSpawnedTerrainObject_ownerNode_9, logic_uScript_GetSpawnedTerrainObject_uniqueName_9);
		local_StarterObject_UnityEngine_GameObject = logic_uScript_GetSpawnedTerrainObject_Return_9;
		if (local_StarterObject_UnityEngine_GameObject_previous != local_StarterObject_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StarterObject_UnityEngine_GameObject_previous = local_StarterObject_UnityEngine_GameObject;
		}
		if (logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_9.CurrentlySpawned)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_SpawnTerrainObject_ownerNode_10 = owner_Connection_11;
		logic_uScript_SpawnTerrainObject_terrainObjectPrefab_10 = terrainObjectPrefab;
		logic_uScript_SpawnTerrainObject_posName_10 = raceStartPosition;
		logic_uScript_SpawnTerrainObject_uniqueName_10 = terrainObjectName;
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
			Relay_InitialSpawn_43();
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

	private void Relay_In_28()
	{
		logic_uScript_GetEncounterSpline_owner_28 = owner_Connection_31;
		logic_uScript_GetEncounterSpline_Return_28 = logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_28.In(logic_uScript_GetEncounterSpline_owner_28);
		local_Spline_TrackSpline = logic_uScript_GetEncounterSpline_Return_28;
		if (logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_28.Out)
		{
			Relay_In_198();
		}
	}

	private void Relay_In_29()
	{
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_owner_29 = owner_Connection_35;
		if (local_StarterObject_UnityEngine_GameObject_previous != local_StarterObject_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StarterObject_UnityEngine_GameObject_previous = local_StarterObject_UnityEngine_GameObject;
		}
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_targetChallengeStarterObject_29 = local_StarterObject_UnityEngine_GameObject;
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_29.In(logic_uScript_InitChallengeStarterWithEncounterChallengeData_owner_29, logic_uScript_InitChallengeStarterWithEncounterChallengeData_targetChallengeStarterObject_29);
		if (logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_29.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_30()
	{
		if (local_StarterObject_UnityEngine_GameObject_previous != local_StarterObject_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StarterObject_UnityEngine_GameObject_previous = local_StarterObject_UnityEngine_GameObject;
		}
		logic_uScript_GetChallengeStartTransform_challengeStarterObject_30 = local_StarterObject_UnityEngine_GameObject;
		logic_uScript_GetChallengeStartTransform_Return_30 = logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_30.In(logic_uScript_GetChallengeStartTransform_challengeStarterObject_30);
		local_StartTransform_UnityEngine_Transform = logic_uScript_GetChallengeStartTransform_Return_30;
		if (logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_30.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_37()
	{
		if (local_StarterObject_UnityEngine_GameObject_previous != local_StarterObject_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StarterObject_UnityEngine_GameObject_previous = local_StarterObject_UnityEngine_GameObject;
		}
		logic_uScript_GetChallengeIDFromChallengeStarter_challengeStarterObject_37 = local_StarterObject_UnityEngine_GameObject;
		logic_uScript_GetChallengeIDFromChallengeStarter_Return_37 = logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_37.In(logic_uScript_GetChallengeIDFromChallengeStarter_challengeStarterObject_37);
		local_ChallengeID_System_String = logic_uScript_GetChallengeIDFromChallengeStarter_Return_37;
		if (logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_37.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_40()
	{
		int num = 0;
		Array hoverNPCSpawnData = HoverNPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_40.Length != num + hoverNPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_40, num + hoverNPCSpawnData.Length);
		}
		Array.Copy(hoverNPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_40, num, hoverNPCSpawnData.Length);
		num += hoverNPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_40 = owner_Connection_44;
		int num2 = 0;
		Array array = local_39_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_40.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_40, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_40, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_40 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.In(logic_uScript_GetAndCheckTechs_techData_40, logic_uScript_GetAndCheckTechs_ownerNode_40, ref logic_uScript_GetAndCheckTechs_techs_40);
		local_39_TankArray = logic_uScript_GetAndCheckTechs_techs_40;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_47();
		}
		if (someAlive)
		{
			Relay_AtIndex_47();
		}
	}

	private void Relay_InitialSpawn_43()
	{
		int num = 0;
		Array hoverNPCSpawnData = HoverNPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_43.Length != num + hoverNPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_43, num + hoverNPCSpawnData.Length);
		}
		Array.Copy(hoverNPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_43, num, hoverNPCSpawnData.Length);
		num += hoverNPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_43 = owner_Connection_41;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_43.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_43, logic_uScript_SpawnTechsFromData_ownerNode_43, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_43, logic_uScript_SpawnTechsFromData_allowResurrection_43);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_43.Out)
		{
			Relay_True_4();
		}
	}

	private void Relay_In_46()
	{
		logic_uScript_SetTankInvulnerable_tank_46 = local_HoverNPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_46.In(logic_uScript_SetTankInvulnerable_invulnerable_46, logic_uScript_SetTankInvulnerable_tank_46);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_46.Out)
		{
			Relay_In_146();
		}
	}

	private void Relay_AtIndex_47()
	{
		int num = 0;
		Array array = local_39_TankArray;
		if (logic_uScript_AccessListTech_techList_47.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_47, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_47, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_47.AtIndex(ref logic_uScript_AccessListTech_techList_47, logic_uScript_AccessListTech_index_47, out logic_uScript_AccessListTech_value_47);
		local_39_TankArray = logic_uScript_AccessListTech_techList_47;
		local_HoverNPCTech_Tank = logic_uScript_AccessListTech_value_47;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_47.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_50()
	{
		logic_uScriptCon_CompareBool_Bool_50 = local_RampSetup_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.In(logic_uScriptCon_CompareBool_Bool_50);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.False;
		if (num)
		{
			Relay_In_198();
		}
		if (flag)
		{
			Relay_True_52();
		}
	}

	private void Relay_True_52()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_52.True(out logic_uScriptAct_SetBool_Target_52);
		local_RampSetup_System_Boolean = logic_uScriptAct_SetBool_Target_52;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_52.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_False_52()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_52.False(out logic_uScriptAct_SetBool_Target_52);
		local_RampSetup_System_Boolean = logic_uScriptAct_SetBool_Target_52;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_52.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_True_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.True(out logic_uScriptAct_SetBool_Target_53);
		local_RampSetup_System_Boolean = logic_uScriptAct_SetBool_Target_53;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_53.Out)
		{
			Relay_False_167();
		}
	}

	private void Relay_False_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.False(out logic_uScriptAct_SetBool_Target_53);
		local_RampSetup_System_Boolean = logic_uScriptAct_SetBool_Target_53;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_53.Out)
		{
			Relay_False_167();
		}
	}

	private void Relay_Save_Out_55()
	{
		Relay_Save_7();
	}

	private void Relay_Load_Out_55()
	{
		Relay_Load_7();
	}

	private void Relay_Restart_Out_55()
	{
		Relay_Set_False_7();
	}

	private void Relay_Save_55()
	{
		logic_SubGraph_SaveLoadInt_integer_55 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_55 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Save(logic_SubGraph_SaveLoadInt_restartValue_55, ref logic_SubGraph_SaveLoadInt_integer_55, logic_SubGraph_SaveLoadInt_intAsVariable_55, logic_SubGraph_SaveLoadInt_uniqueID_55);
	}

	private void Relay_Load_55()
	{
		logic_SubGraph_SaveLoadInt_integer_55 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_55 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Load(logic_SubGraph_SaveLoadInt_restartValue_55, ref logic_SubGraph_SaveLoadInt_integer_55, logic_SubGraph_SaveLoadInt_intAsVariable_55, logic_SubGraph_SaveLoadInt_uniqueID_55);
	}

	private void Relay_Restart_55()
	{
		logic_SubGraph_SaveLoadInt_integer_55 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_55 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Restart(logic_SubGraph_SaveLoadInt_restartValue_55, ref logic_SubGraph_SaveLoadInt_integer_55, logic_SubGraph_SaveLoadInt_intAsVariable_55, logic_SubGraph_SaveLoadInt_uniqueID_55);
	}

	private void Relay_Out_56()
	{
	}

	private void Relay_In_56()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_56 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.In(logic_SubGraph_LoadObjectiveStates_currentObjective_56);
	}

	private void Relay_In_60()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_60 = local_HoverNPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_60 = distNearNPC;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_60.In(logic_uScript_IsPlayerInRangeOfTech_tech_60, logic_uScript_IsPlayerInRangeOfTech_range_60, logic_uScript_IsPlayerInRangeOfTech_techs_60);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_60.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_60.OutOfRange;
		if (inRange)
		{
			Relay_In_62();
		}
		if (outOfRange)
		{
			Relay_In_231();
		}
	}

	private void Relay_In_62()
	{
		logic_uScript_SetEncounterTarget_owner_62 = owner_Connection_61;
		logic_uScript_SetEncounterTarget_visibleObject_62 = local_HoverNPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_62.In(logic_uScript_SetEncounterTarget_owner_62, logic_uScript_SetEncounterTarget_visibleObject_62);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_62.Out)
		{
			Relay_In_159();
		}
	}

	private void Relay_Output1_65()
	{
		Relay_In_106();
	}

	private void Relay_Output2_65()
	{
		Relay_In_106();
	}

	private void Relay_Output3_65()
	{
	}

	private void Relay_Output4_65()
	{
	}

	private void Relay_Output5_65()
	{
	}

	private void Relay_Output6_65()
	{
	}

	private void Relay_Output7_65()
	{
	}

	private void Relay_Output8_65()
	{
	}

	private void Relay_In_65()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_65 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_65.In(logic_uScriptCon_ManualSwitch_CurrentOutput_65);
	}

	private void Relay_InitialSpawn_68()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_68.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_68, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_68, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_68 = owner_Connection_69;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_68.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_68, logic_uScript_SpawnTechsFromData_ownerNode_68, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_68, logic_uScript_SpawnTechsFromData_allowResurrection_68);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_68.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_AtIndex_74()
	{
		int num = 0;
		Array array = local_73_TankArray;
		if (logic_uScript_AccessListTech_techList_74.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_74, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_74, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_74.AtIndex(ref logic_uScript_AccessListTech_techList_74, logic_uScript_AccessListTech_index_74, out logic_uScript_AccessListTech_value_74);
		local_73_TankArray = logic_uScript_AccessListTech_techList_74;
		local_Vehicle_Tank = logic_uScript_AccessListTech_value_74;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_74.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_75()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_75.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_75, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_75, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_75 = owner_Connection_70;
		int num2 = 0;
		Array array2 = local_73_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_75.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_75, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_75, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_75 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75.In(logic_uScript_GetAndCheckTechs_techData_75, logic_uScript_GetAndCheckTechs_ownerNode_75, ref logic_uScript_GetAndCheckTechs_techs_75);
		local_73_TankArray = logic_uScript_GetAndCheckTechs_techs_75;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_74();
		}
		if (someAlive)
		{
			Relay_AtIndex_74();
		}
		if (allDead)
		{
			Relay_In_104();
		}
		if (waitingToSpawn)
		{
			Relay_In_136();
		}
	}

	private void Relay_True_76()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_76.True(out logic_uScriptAct_SetBool_Target_76);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_76;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_76.Out)
		{
			Relay_False_78();
		}
	}

	private void Relay_False_76()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_76.False(out logic_uScriptAct_SetBool_Target_76);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_76;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_76.Out)
		{
			Relay_False_78();
		}
	}

	private void Relay_True_78()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.True(out logic_uScriptAct_SetBool_Target_78);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_78;
	}

	private void Relay_False_78()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.False(out logic_uScriptAct_SetBool_Target_78);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_78;
	}

	private void Relay_In_80()
	{
		logic_uScript_AddMessage_messageData_80 = msgRaceComplete;
		logic_uScript_AddMessage_speaker_80 = messageSpeaker;
		logic_uScript_AddMessage_Return_80 = logic_uScript_AddMessage_uScript_AddMessage_80.In(logic_uScript_AddMessage_messageData_80, logic_uScript_AddMessage_speaker_80);
		if (logic_uScript_AddMessage_uScript_AddMessage_80.Out)
		{
			Relay_In_101();
		}
	}

	private void Relay_In_81()
	{
		logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_81.In();
		bool success = logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_81.Success;
		bool failure = logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_81.Failure;
		if (success)
		{
			Relay_In_80();
		}
		if (failure)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_84()
	{
		logic_uScript_AddMessage_messageData_84 = msgQuitFromMenu;
		logic_uScript_AddMessage_speaker_84 = messageSpeaker;
		logic_uScript_AddMessage_Return_84 = logic_uScript_AddMessage_uScript_AddMessage_84.In(logic_uScript_AddMessage_messageData_84, logic_uScript_AddMessage_speaker_84);
	}

	private void Relay_In_85()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_85 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_85.In(logic_uScript_CompareCheckpointChallengeEndReason_result_85, logic_uScript_CompareCheckpointChallengeEndReason_expected_85);
		bool equalTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_85.EqualTo;
		bool notEqualTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_85.NotEqualTo;
		if (equalTo)
		{
			Relay_In_84();
		}
		if (notEqualTo)
		{
			Relay_In_261();
		}
	}

	private void Relay_In_90()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_90 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_90.In(logic_uScript_CompareCheckpointChallengeEndReason_result_90, logic_uScript_CompareCheckpointChallengeEndReason_expected_90);
		bool equalTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_90.EqualTo;
		bool notEqualTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_90.NotEqualTo;
		if (equalTo)
		{
			Relay_In_96();
		}
		if (notEqualTo)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_91()
	{
		if (local_StarterObject_UnityEngine_GameObject_previous != local_StarterObject_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StarterObject_UnityEngine_GameObject_previous = local_StarterObject_UnityEngine_GameObject;
		}
		logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_91 = local_StarterObject_UnityEngine_GameObject;
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_91.In(logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_91);
		if (logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_91.Out)
		{
			Relay_UnPause_270();
		}
	}

	private void Relay_Succeed_92()
	{
		logic_uScript_FinishEncounter_owner_92 = owner_Connection_99;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_92.Succeed(logic_uScript_FinishEncounter_owner_92);
	}

	private void Relay_Fail_92()
	{
		logic_uScript_FinishEncounter_owner_92 = owner_Connection_99;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_92.Fail(logic_uScript_FinishEncounter_owner_92);
	}

	private void Relay_True_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.True(out logic_uScriptAct_SetBool_Target_93);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_False_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.False(out logic_uScriptAct_SetBool_Target_93);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_In_96()
	{
		logic_uScript_AddMessage_messageData_96 = msgOutOfBounds;
		logic_uScript_AddMessage_speaker_96 = messageSpeaker;
		logic_uScript_AddMessage_Return_96 = logic_uScript_AddMessage_uScript_AddMessage_96.In(logic_uScript_AddMessage_messageData_96, logic_uScript_AddMessage_speaker_96);
	}

	private void Relay_In_101()
	{
		logic_uScript_RemoveTech_tech_101 = local_HoverNPCTech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_101.In(logic_uScript_RemoveTech_tech_101);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_101.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_104()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104.Out)
		{
			Relay_In_137();
		}
	}

	private void Relay_In_106()
	{
		logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_106 = local_ChallengeID_System_String;
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_106.In(logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_106);
		bool notRunning = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_106.NotRunning;
		bool justStarted = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_106.JustStarted;
		bool justEnded = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_106.JustEnded;
		if (notRunning)
		{
			Relay_In_131();
		}
		if (justStarted)
		{
			Relay_In_133();
		}
		if (justEnded)
		{
			Relay_False_93();
		}
	}

	private void Relay_In_112()
	{
		logic_uScriptCon_CompareBool_Bool_112 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.In(logic_uScriptCon_CompareBool_Bool_112);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.False;
		if (num)
		{
			Relay_In_75();
		}
		if (flag)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_114()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_114.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_114.Out)
		{
			Relay_In_136();
		}
	}

	private void Relay_In_117()
	{
		logic_uScript_PointArrowAtVisible_targetObject_117 = local_HoverTerminalBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_117.In(logic_uScript_PointArrowAtVisible_targetObject_117, logic_uScript_PointArrowAtVisible_timeToShowFor_117, logic_uScript_PointArrowAtVisible_offset_117);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_117.Out)
		{
			Relay_In_118();
		}
	}

	private void Relay_In_118()
	{
		logic_uScript_EnableGlow_targetObject_118 = local_HoverTerminalBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_118.In(logic_uScript_EnableGlow_targetObject_118, logic_uScript_EnableGlow_enable_118);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_118.Out)
		{
			Relay_In_284();
		}
	}

	private void Relay_In_122()
	{
		logic_uScriptCon_CompareBool_Bool_122 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_122.In(logic_uScriptCon_CompareBool_Bool_122);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_122.True)
		{
			Relay_In_124();
		}
	}

	private void Relay_In_124()
	{
		logic_uScriptAct_MultiplyInt_v2_A_124 = vehicleCost;
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_124.In(logic_uScriptAct_MultiplyInt_v2_A_124, logic_uScriptAct_MultiplyInt_v2_B_124, out logic_uScriptAct_MultiplyInt_v2_IntResult_124, out logic_uScriptAct_MultiplyInt_v2_FloatResult_124);
		local_123_System_Int32 = logic_uScriptAct_MultiplyInt_v2_IntResult_124;
		if (logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_124.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_True_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.True(out logic_uScriptAct_SetBool_Target_125);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_125;
	}

	private void Relay_False_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.False(out logic_uScriptAct_SetBool_Target_125);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_125;
	}

	private void Relay_In_126()
	{
		logic_uScript_AddMoney_amount_126 = local_123_System_Int32;
		logic_uScript_AddMoney_uScript_AddMoney_126.In(logic_uScript_AddMoney_amount_126);
		if (logic_uScript_AddMoney_uScript_AddMoney_126.Out)
		{
			Relay_InitialSpawn_68();
		}
	}

	private void Relay_In_129()
	{
		int num = 0;
		Array array = discoverableBlockTypesOnVehicle;
		if (logic_uScript_DiscoverBlocks_blockTypes_129.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DiscoverBlocks_blockTypes_129, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DiscoverBlocks_blockTypes_129, num, array.Length);
		num += array.Length;
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_129.In(logic_uScript_DiscoverBlocks_blockTypes_129);
		if (logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_129.Out)
		{
			Relay_True_125();
		}
	}

	private void Relay_In_131()
	{
		logic_uScriptCon_CompareBool_Bool_131 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.In(logic_uScriptCon_CompareBool_Bool_131);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.False)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_133()
	{
		logic_uScript_HideArrow_uScript_HideArrow_133.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_133.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_135()
	{
		logic_uScript_EnableGlow_targetObject_135 = local_HoverTerminalBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_135.In(logic_uScript_EnableGlow_targetObject_135, logic_uScript_EnableGlow_enable_135);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_135.Out)
		{
			Relay_In_192();
		}
	}

	private void Relay_In_136()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_136.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_136.Out)
		{
			Relay_In_137();
		}
	}

	private void Relay_In_137()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_137.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_137.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_140()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_140 = local_HoverNPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_140 = distNearNPC;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_140.In(logic_uScript_IsPlayerInRangeOfTech_tech_140, logic_uScript_IsPlayerInRangeOfTech_range_140, logic_uScript_IsPlayerInRangeOfTech_techs_140);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_140.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_140.OutOfRange;
		if (inRange)
		{
			Relay_In_275();
		}
		if (outOfRange)
		{
			Relay_In_280();
		}
	}

	private void Relay_In_142()
	{
		logic_uScript_EnableGlow_targetObject_142 = local_HoverTerminalBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_142.In(logic_uScript_EnableGlow_targetObject_142, logic_uScript_EnableGlow_enable_142);
	}

	private void Relay_In_143()
	{
		logic_uScript_HideArrow_uScript_HideArrow_143.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_143.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_In_146()
	{
		logic_uScript_GetTankBlock_tank_146 = local_HoverNPCTech_Tank;
		logic_uScript_GetTankBlock_blockType_146 = interactableBlockType;
		logic_uScript_GetTankBlock_Return_146 = logic_uScript_GetTankBlock_uScript_GetTankBlock_146.In(logic_uScript_GetTankBlock_tank_146, logic_uScript_GetTankBlock_blockType_146);
		local_HoverTerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_146;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_146.Returned)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_148()
	{
		logic_uScript_SetBatteryChargeAmount_tech_148 = local_Vehicle_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_148.In(logic_uScript_SetBatteryChargeAmount_tech_148, logic_uScript_SetBatteryChargeAmount_chargeAmount_148);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_148.Out)
		{
			Relay_In_175();
		}
	}

	private void Relay_In_149()
	{
		logic_uScriptCon_CompareBool_Bool_149 = local_SwitchedVehicle_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.In(logic_uScriptCon_CompareBool_Bool_149);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.False;
		if (num)
		{
			Relay_In_256();
		}
		if (flag)
		{
			Relay_True_152();
		}
	}

	private void Relay_True_152()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_152.True(out logic_uScriptAct_SetBool_Target_152);
		local_SwitchedVehicle_System_Boolean = logic_uScriptAct_SetBool_Target_152;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_152.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_False_152()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_152.False(out logic_uScriptAct_SetBool_Target_152);
		local_SwitchedVehicle_System_Boolean = logic_uScriptAct_SetBool_Target_152;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_152.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_Save_Out_154()
	{
		Relay_Save_155();
	}

	private void Relay_Load_Out_154()
	{
		Relay_Load_155();
	}

	private void Relay_Restart_Out_154()
	{
		Relay_Set_False_155();
	}

	private void Relay_Save_154()
	{
		logic_SubGraph_SaveLoadBool_boolean_154 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_154 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Save(ref logic_SubGraph_SaveLoadBool_boolean_154, logic_SubGraph_SaveLoadBool_boolAsVariable_154, logic_SubGraph_SaveLoadBool_uniqueID_154);
	}

	private void Relay_Load_154()
	{
		logic_SubGraph_SaveLoadBool_boolean_154 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_154 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Load(ref logic_SubGraph_SaveLoadBool_boolean_154, logic_SubGraph_SaveLoadBool_boolAsVariable_154, logic_SubGraph_SaveLoadBool_uniqueID_154);
	}

	private void Relay_Set_True_154()
	{
		logic_SubGraph_SaveLoadBool_boolean_154 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_154 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_154, logic_SubGraph_SaveLoadBool_boolAsVariable_154, logic_SubGraph_SaveLoadBool_uniqueID_154);
	}

	private void Relay_Set_False_154()
	{
		logic_SubGraph_SaveLoadBool_boolean_154 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_154 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_154.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_154, logic_SubGraph_SaveLoadBool_boolAsVariable_154, logic_SubGraph_SaveLoadBool_uniqueID_154);
	}

	private void Relay_Save_Out_155()
	{
		Relay_Save_177();
	}

	private void Relay_Load_Out_155()
	{
		Relay_Load_177();
	}

	private void Relay_Restart_Out_155()
	{
		Relay_Set_False_177();
	}

	private void Relay_Save_155()
	{
		logic_SubGraph_SaveLoadBool_boolean_155 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_155 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Save(ref logic_SubGraph_SaveLoadBool_boolean_155, logic_SubGraph_SaveLoadBool_boolAsVariable_155, logic_SubGraph_SaveLoadBool_uniqueID_155);
	}

	private void Relay_Load_155()
	{
		logic_SubGraph_SaveLoadBool_boolean_155 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_155 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Load(ref logic_SubGraph_SaveLoadBool_boolean_155, logic_SubGraph_SaveLoadBool_boolAsVariable_155, logic_SubGraph_SaveLoadBool_uniqueID_155);
	}

	private void Relay_Set_True_155()
	{
		logic_SubGraph_SaveLoadBool_boolean_155 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_155 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_155, logic_SubGraph_SaveLoadBool_boolAsVariable_155, logic_SubGraph_SaveLoadBool_uniqueID_155);
	}

	private void Relay_Set_False_155()
	{
		logic_SubGraph_SaveLoadBool_boolean_155 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_155 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_155.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_155, logic_SubGraph_SaveLoadBool_boolAsVariable_155, logic_SubGraph_SaveLoadBool_uniqueID_155);
	}

	private void Relay_In_158()
	{
		logic_uScript_IsTechPlayer_tech_158 = local_Vehicle_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_158.In(logic_uScript_IsTechPlayer_tech_158);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_158.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_158.False;
		if (num)
		{
			Relay_In_149();
		}
		if (flag)
		{
			Relay_In_188();
		}
	}

	private void Relay_In_159()
	{
		logic_uScriptCon_CompareBool_Bool_159 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.In(logic_uScriptCon_CompareBool_Bool_159);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.False;
		if (num)
		{
			Relay_In_229();
		}
		if (flag)
		{
			Relay_In_162();
		}
	}

	private void Relay_In_162()
	{
		logic_uScriptCon_CompareBool_Bool_162 = local_ChallengeInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.In(logic_uScriptCon_CompareBool_Bool_162);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.False;
		if (num)
		{
			Relay_In_163();
		}
		if (flag)
		{
			Relay_In_255();
		}
	}

	private void Relay_In_163()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_True_165()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_165.True(out logic_uScriptAct_SetBool_Target_165);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_165;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_165.Out)
		{
			Relay_False_171();
		}
	}

	private void Relay_False_165()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_165.False(out logic_uScriptAct_SetBool_Target_165);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_165;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_165.Out)
		{
			Relay_False_171();
		}
	}

	private void Relay_True_167()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_167.True(out logic_uScriptAct_SetBool_Target_167);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_167;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_167.Out)
		{
			Relay_False_165();
		}
	}

	private void Relay_False_167()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_167.False(out logic_uScriptAct_SetBool_Target_167);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_167;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_167.Out)
		{
			Relay_False_165();
		}
	}

	private void Relay_True_168()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_168.True(out logic_uScriptAct_SetBool_Target_168);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_168;
	}

	private void Relay_False_168()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_168.False(out logic_uScriptAct_SetBool_Target_168);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_168;
	}

	private void Relay_True_171()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.True(out logic_uScriptAct_SetBool_Target_171);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_171;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_171.Out)
		{
			Relay_False_168();
		}
	}

	private void Relay_False_171()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.False(out logic_uScriptAct_SetBool_Target_171);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_171;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_171.Out)
		{
			Relay_False_168();
		}
	}

	private void Relay_True_173()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_173.True(out logic_uScriptAct_SetBool_Target_173);
		local_RaceAttempted_System_Boolean = logic_uScriptAct_SetBool_Target_173;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_173.Out)
		{
			Relay_True_76();
		}
	}

	private void Relay_False_173()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_173.False(out logic_uScriptAct_SetBool_Target_173);
		local_RaceAttempted_System_Boolean = logic_uScriptAct_SetBool_Target_173;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_173.Out)
		{
			Relay_True_76();
		}
	}

	private void Relay_In_175()
	{
		logic_uScriptCon_CompareBool_Bool_175 = local_RaceAttempted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175.In(logic_uScriptCon_CompareBool_Bool_175);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175.False;
		if (num)
		{
			Relay_In_176();
		}
		if (flag)
		{
			Relay_In_180();
		}
	}

	private void Relay_In_176()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_176.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_176.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_Save_Out_177()
	{
		Relay_Save_244();
	}

	private void Relay_Load_Out_177()
	{
		Relay_Load_244();
	}

	private void Relay_Restart_Out_177()
	{
		Relay_Set_False_244();
	}

	private void Relay_Save_177()
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_177 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Save(ref logic_SubGraph_SaveLoadBool_boolean_177, logic_SubGraph_SaveLoadBool_boolAsVariable_177, logic_SubGraph_SaveLoadBool_uniqueID_177);
	}

	private void Relay_Load_177()
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_177 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Load(ref logic_SubGraph_SaveLoadBool_boolean_177, logic_SubGraph_SaveLoadBool_boolAsVariable_177, logic_SubGraph_SaveLoadBool_uniqueID_177);
	}

	private void Relay_Set_True_177()
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_177 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_177, logic_SubGraph_SaveLoadBool_boolAsVariable_177, logic_SubGraph_SaveLoadBool_uniqueID_177);
	}

	private void Relay_Set_False_177()
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_177 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_177, logic_SubGraph_SaveLoadBool_boolAsVariable_177, logic_SubGraph_SaveLoadBool_uniqueID_177);
	}

	private void Relay_In_180()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_180 = local_HoverNPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_180 = distNearNPC;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_180.In(logic_uScript_IsPlayerInRangeOfTech_tech_180, logic_uScript_IsPlayerInRangeOfTech_range_180, logic_uScript_IsPlayerInRangeOfTech_techs_180);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_180.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_180.OutOfRange;
		if (inRange)
		{
			Relay_In_182();
		}
		if (outOfRange)
		{
			Relay_In_188();
		}
	}

	private void Relay_Out_182()
	{
		Relay_In_50();
	}

	private void Relay_Shown_182()
	{
	}

	private void Relay_In_182()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_182 = msgReadyToBegin;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_182 = msgReadyToBegin_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_182 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_182.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_182, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_182, logic_SubGraph_AddMessageWithPadSupport_speaker_182);
	}

	private void Relay_In_188()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_188 = messageTagControls;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_188.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_188, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_188);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_188.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_190()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_190 = messageTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_190.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_190, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_190);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_190.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_192()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_192 = messageTagIntro;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_192.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_192, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_192);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_192.Out)
		{
			Relay_In_196();
		}
	}

	private void Relay_In_195()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_195 = messageTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_195.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_195, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_195);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_195.Out)
		{
			Relay_In_209();
		}
	}

	private void Relay_In_196()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_196 = messageTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_196.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_196, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_196);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_196.Out)
		{
			Relay_In_237();
		}
	}

	private void Relay_In_198()
	{
		int num = 0;
		Array hoverNPCSpawnData = HoverNPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_198.Length != num + hoverNPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_198, num + hoverNPCSpawnData.Length);
		}
		Array.Copy(hoverNPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_198, num, hoverNPCSpawnData.Length);
		num += hoverNPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_198 = owner_Connection_199;
		logic_uScript_GetAndCheckTechs_Return_198 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198.In(logic_uScript_GetAndCheckTechs_techData_198, logic_uScript_GetAndCheckTechs_ownerNode_198, ref logic_uScript_GetAndCheckTechs_techs_198);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_60();
		}
		if (someAlive)
		{
			Relay_In_60();
		}
		if (allDead)
		{
			Relay_In_201();
		}
		if (waitingToSpawn)
		{
			Relay_In_201();
		}
	}

	private void Relay_In_201()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_201.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_201.Out)
		{
			Relay_In_202();
		}
	}

	private void Relay_In_202()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_202.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_202.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_ResponseEvent_205()
	{
		local_206_TankBlock = event_UnityEngine_GameObject_TankBlock_205;
		local_204_System_Boolean = event_UnityEngine_GameObject_Accepted_205;
		Relay_In_207();
	}

	private void Relay_In_207()
	{
		logic_uScript_CompareBlock_A_207 = local_206_TankBlock;
		logic_uScript_CompareBlock_B_207 = local_HoverTerminalBlock_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_207.In(logic_uScript_CompareBlock_A_207, logic_uScript_CompareBlock_B_207);
		if (logic_uScript_CompareBlock_uScript_CompareBlock_207.EqualTo)
		{
			Relay_In_195();
		}
	}

	private void Relay_In_209()
	{
		logic_uScriptCon_CompareBool_Bool_209 = local_204_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_209.In(logic_uScriptCon_CompareBool_Bool_209);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_209.True)
		{
			Relay_In_301();
		}
	}

	private void Relay_In_211()
	{
		logic_uScriptCon_CompareBool_Bool_211 = _DEBUGIgnoreMoneyCheck;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.In(logic_uScriptCon_CompareBool_Bool_211);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.False;
		if (num)
		{
			Relay_In_219();
		}
		if (flag)
		{
			Relay_In_215();
		}
	}

	private void Relay_In_212()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_212 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_212 = msgPromptNoMoney;
		logic_uScript_MissionPromptBlock_Show_targetBlock_212 = local_HoverTerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_212.In(logic_uScript_MissionPromptBlock_Show_bodyText_212, logic_uScript_MissionPromptBlock_Show_acceptButtonText_212, logic_uScript_MissionPromptBlock_Show_rejectButtonText_212, logic_uScript_MissionPromptBlock_Show_targetBlock_212, logic_uScript_MissionPromptBlock_Show_highlightBlock_212, logic_uScript_MissionPromptBlock_Show_singleUse_212);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_212.Out)
		{
			Relay_False_214();
		}
	}

	private void Relay_True_214()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.True(out logic_uScriptAct_SetBool_Target_214);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_214;
	}

	private void Relay_False_214()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.False(out logic_uScriptAct_SetBool_Target_214);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_214;
	}

	private void Relay_In_215()
	{
		logic_uScript_GetCurrentMoneyEarned_Return_215 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_215.In();
		local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_215;
		if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_215.Out)
		{
			Relay_In_216();
		}
	}

	private void Relay_In_216()
	{
		logic_uScriptCon_CompareInt_A_216 = local_CurrentMoney_System_Int32;
		logic_uScriptCon_CompareInt_B_216 = vehicleCost;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_216.In(logic_uScriptCon_CompareInt_A_216, logic_uScriptCon_CompareInt_B_216);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_216.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_216.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_223();
		}
		if (lessThan)
		{
			Relay_In_212();
		}
	}

	private void Relay_In_219()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_219.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_219.Out)
		{
			Relay_In_223();
		}
	}

	private void Relay_In_223()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_223 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_223 = msgPromptAccept;
		logic_uScript_MissionPromptBlock_Show_rejectButtonText_223 = msgPromptDecline;
		logic_uScript_MissionPromptBlock_Show_targetBlock_223 = local_HoverTerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_223.In(logic_uScript_MissionPromptBlock_Show_bodyText_223, logic_uScript_MissionPromptBlock_Show_acceptButtonText_223, logic_uScript_MissionPromptBlock_Show_rejectButtonText_223, logic_uScript_MissionPromptBlock_Show_targetBlock_223, logic_uScript_MissionPromptBlock_Show_highlightBlock_223, logic_uScript_MissionPromptBlock_Show_singleUse_223);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_223.Out)
		{
			Relay_True_214();
		}
	}

	private void Relay_In_229()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_229 = local_HoverTerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_229.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_229);
		if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_229.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_In_231()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_231 = raceStartPosition;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_231.In(logic_uScript_SetEncounterTargetPosition_positionName_231);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_231.Out)
		{
			Relay_In_190();
		}
	}

	private void Relay_In_234()
	{
		logic_uScript_AddMessage_messageData_234 = msgRaceStarted;
		logic_uScript_AddMessage_speaker_234 = messageSpeaker;
		logic_uScript_AddMessage_Return_234 = logic_uScript_AddMessage_uScript_AddMessage_234.In(logic_uScript_AddMessage_messageData_234, logic_uScript_AddMessage_speaker_234);
		if (logic_uScript_AddMessage_uScript_AddMessage_234.Out)
		{
			Relay_True_239();
		}
	}

	private void Relay_In_237()
	{
		logic_uScriptCon_CompareBool_Bool_237 = local_RaceStartedMsgShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237.In(logic_uScriptCon_CompareBool_Bool_237);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237.False;
		if (num)
		{
			Relay_True_173();
		}
		if (flag)
		{
			Relay_In_234();
		}
	}

	private void Relay_True_239()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_239.True(out logic_uScriptAct_SetBool_Target_239);
		local_RaceStartedMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_239;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_239.Out)
		{
			Relay_True_173();
		}
	}

	private void Relay_False_239()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_239.False(out logic_uScriptAct_SetBool_Target_239);
		local_RaceStartedMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_239;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_239.Out)
		{
			Relay_True_173();
		}
	}

	private void Relay_In_242()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_242.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_242.Out)
		{
			Relay_In_176();
		}
	}

	private void Relay_Save_Out_244()
	{
	}

	private void Relay_Load_Out_244()
	{
		Relay_False_53();
		Relay_In_56();
	}

	private void Relay_Restart_Out_244()
	{
		Relay_False_53();
	}

	private void Relay_Save_244()
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_244 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Save(ref logic_SubGraph_SaveLoadBool_boolean_244, logic_SubGraph_SaveLoadBool_boolAsVariable_244, logic_SubGraph_SaveLoadBool_uniqueID_244);
	}

	private void Relay_Load_244()
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_244 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Load(ref logic_SubGraph_SaveLoadBool_boolean_244, logic_SubGraph_SaveLoadBool_boolAsVariable_244, logic_SubGraph_SaveLoadBool_uniqueID_244);
	}

	private void Relay_Set_True_244()
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_244 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_244, logic_SubGraph_SaveLoadBool_boolAsVariable_244, logic_SubGraph_SaveLoadBool_uniqueID_244);
	}

	private void Relay_Set_False_244()
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_244 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_244, logic_SubGraph_SaveLoadBool_boolAsVariable_244, logic_SubGraph_SaveLoadBool_uniqueID_244);
	}

	private void Relay_In_246()
	{
		logic_uScriptCon_CompareBool_Bool_246 = local_IntroMsgPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246.In(logic_uScriptCon_CompareBool_Bool_246);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246.False;
		if (num)
		{
			Relay_In_163();
		}
		if (flag)
		{
			Relay_In_253();
		}
	}

	private void Relay_True_250()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_250.True(out logic_uScriptAct_SetBool_Target_250);
		local_IntroMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_250;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_250.Out)
		{
			Relay_In_252();
		}
	}

	private void Relay_False_250()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_250.False(out logic_uScriptAct_SetBool_Target_250);
		local_IntroMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_250;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_250.Out)
		{
			Relay_In_252();
		}
	}

	private void Relay_Out_252()
	{
		Relay_In_271();
	}

	private void Relay_In_252()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_252 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_252.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_252, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_252);
	}

	private void Relay_In_253()
	{
		logic_uScript_AddMessage_messageData_253 = msgIntro;
		logic_uScript_AddMessage_speaker_253 = messageSpeaker;
		logic_uScript_AddMessage_Return_253 = logic_uScript_AddMessage_uScript_AddMessage_253.In(logic_uScript_AddMessage_messageData_253, logic_uScript_AddMessage_speaker_253);
		if (logic_uScript_AddMessage_uScript_AddMessage_253.Shown)
		{
			Relay_True_250();
		}
	}

	private void Relay_In_255()
	{
		logic_uScriptCon_CompareBool_Bool_255 = local_WaitingOnPrompt_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255.In(logic_uScriptCon_CompareBool_Bool_255);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255.False;
		if (num)
		{
			Relay_In_163();
		}
		if (flag)
		{
			Relay_In_246();
		}
	}

	private void Relay_In_256()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_256.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_256.Out)
		{
			Relay_In_242();
		}
	}

	private void Relay_In_257()
	{
		logic_uScript_AddMessage_messageData_257 = msgOutOfTime;
		logic_uScript_AddMessage_speaker_257 = messageSpeaker;
		logic_uScript_AddMessage_Return_257 = logic_uScript_AddMessage_uScript_AddMessage_257.In(logic_uScript_AddMessage_messageData_257, logic_uScript_AddMessage_speaker_257);
	}

	private void Relay_In_261()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_261 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_261.In(logic_uScript_CompareCheckpointChallengeEndReason_result_261, logic_uScript_CompareCheckpointChallengeEndReason_expected_261);
		if (logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_261.EqualTo)
		{
			Relay_In_257();
		}
	}

	private void Relay_Pause_263()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_263.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_263.Out)
		{
			Relay_In_268();
		}
	}

	private void Relay_UnPause_263()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_263.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_263.Out)
		{
			Relay_In_268();
		}
	}

	private void Relay_In_264()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_264 = owner_Connection_262;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_264.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_264);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_264.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_264.False;
		if (num)
		{
			Relay_Pause_263();
		}
		if (flag)
		{
			Relay_UnPause_269();
		}
	}

	private void Relay_Save_Out_265()
	{
		Relay_Save_154();
	}

	private void Relay_Load_Out_265()
	{
		Relay_Load_154();
	}

	private void Relay_Restart_Out_265()
	{
		Relay_Set_False_154();
	}

	private void Relay_Save_265()
	{
		logic_SubGraph_SaveLoadBool_boolean_265 = local_RaceStartedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_265 = local_RaceStartedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Save(ref logic_SubGraph_SaveLoadBool_boolean_265, logic_SubGraph_SaveLoadBool_boolAsVariable_265, logic_SubGraph_SaveLoadBool_uniqueID_265);
	}

	private void Relay_Load_265()
	{
		logic_SubGraph_SaveLoadBool_boolean_265 = local_RaceStartedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_265 = local_RaceStartedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Load(ref logic_SubGraph_SaveLoadBool_boolean_265, logic_SubGraph_SaveLoadBool_boolAsVariable_265, logic_SubGraph_SaveLoadBool_uniqueID_265);
	}

	private void Relay_Set_True_265()
	{
		logic_SubGraph_SaveLoadBool_boolean_265 = local_RaceStartedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_265 = local_RaceStartedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_265, logic_SubGraph_SaveLoadBool_boolAsVariable_265, logic_SubGraph_SaveLoadBool_uniqueID_265);
	}

	private void Relay_Set_False_265()
	{
		logic_SubGraph_SaveLoadBool_boolean_265 = local_RaceStartedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_265 = local_RaceStartedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_265.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_265, logic_SubGraph_SaveLoadBool_boolAsVariable_265, logic_SubGraph_SaveLoadBool_uniqueID_265);
	}

	private void Relay_In_268()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_268 = owner_Connection_267;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_268.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_268);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_268.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_Pause_269()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_269.Pause();
	}

	private void Relay_UnPause_269()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_269.UnPause();
	}

	private void Relay_Pause_270()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_270.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_270.Out)
		{
			Relay_Succeed_92();
		}
	}

	private void Relay_UnPause_270()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_270.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_270.Out)
		{
			Relay_Succeed_92();
		}
	}

	private void Relay_In_271()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_AtIndex_273()
	{
		int num = 0;
		Array array = local_272_TankArray;
		if (logic_uScript_AccessListTech_techList_273.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_273, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_273, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_273.AtIndex(ref logic_uScript_AccessListTech_techList_273, logic_uScript_AccessListTech_index_273, out logic_uScript_AccessListTech_value_273);
		local_272_TankArray = logic_uScript_AccessListTech_techList_273;
		local_HoverNPCTech_Tank = logic_uScript_AccessListTech_value_273;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_273.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_275()
	{
		int num = 0;
		Array hoverNPCSpawnData = HoverNPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_275.Length != num + hoverNPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_275, num + hoverNPCSpawnData.Length);
		}
		Array.Copy(hoverNPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_275, num, hoverNPCSpawnData.Length);
		num += hoverNPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_275 = owner_Connection_276;
		int num2 = 0;
		Array array = local_272_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_275.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_275, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_275, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_275 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_275.In(logic_uScript_GetAndCheckTechs_techData_275, logic_uScript_GetAndCheckTechs_ownerNode_275, ref logic_uScript_GetAndCheckTechs_techs_275);
		local_272_TankArray = logic_uScript_GetAndCheckTechs_techs_275;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_275.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_275.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_273();
		}
		if (someAlive)
		{
			Relay_AtIndex_273();
		}
	}

	private void Relay_AtIndex_279()
	{
		int num = 0;
		Array array = local_283_TankArray;
		if (logic_uScript_AccessListTech_techList_279.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_279, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_279, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_279.AtIndex(ref logic_uScript_AccessListTech_techList_279, logic_uScript_AccessListTech_index_279, out logic_uScript_AccessListTech_value_279);
		local_283_TankArray = logic_uScript_AccessListTech_techList_279;
		local_HoverNPCTech_Tank = logic_uScript_AccessListTech_value_279;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_279.Out)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_280()
	{
		int num = 0;
		Array hoverNPCSpawnData = HoverNPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_280.Length != num + hoverNPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_280, num + hoverNPCSpawnData.Length);
		}
		Array.Copy(hoverNPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_280, num, hoverNPCSpawnData.Length);
		num += hoverNPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_280 = owner_Connection_282;
		int num2 = 0;
		Array array = local_283_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_280.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_280, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_280, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_280 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_280.In(logic_uScript_GetAndCheckTechs_techData_280, logic_uScript_GetAndCheckTechs_ownerNode_280, ref logic_uScript_GetAndCheckTechs_techs_280);
		local_283_TankArray = logic_uScript_GetAndCheckTechs_techs_280;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_280.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_280.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_279();
		}
		if (someAlive)
		{
			Relay_AtIndex_279();
		}
	}

	private void Relay_In_284()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_284.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_284, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_284, num, array.Length);
		num += array.Length;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_284.In(logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_284, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_284);
		bool num2 = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_284.True;
		bool flag = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_284.False;
		if (num2)
		{
			Relay_False_293();
		}
		if (flag)
		{
			Relay_True_290();
		}
	}

	private void Relay_In_286()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_286 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_286 = msgWorldCapacityFull;
		logic_uScript_MissionPromptBlock_Show_targetBlock_286 = local_HoverTerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_286.In(logic_uScript_MissionPromptBlock_Show_bodyText_286, logic_uScript_MissionPromptBlock_Show_acceptButtonText_286, logic_uScript_MissionPromptBlock_Show_rejectButtonText_286, logic_uScript_MissionPromptBlock_Show_targetBlock_286, logic_uScript_MissionPromptBlock_Show_highlightBlock_286, logic_uScript_MissionPromptBlock_Show_singleUse_286);
	}

	private void Relay_True_290()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_290.True(out logic_uScriptAct_SetBool_Target_290);
		local_WorldCapacityFull_System_Boolean = logic_uScriptAct_SetBool_Target_290;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_290.Out)
		{
			Relay_In_286();
		}
	}

	private void Relay_False_290()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_290.False(out logic_uScriptAct_SetBool_Target_290);
		local_WorldCapacityFull_System_Boolean = logic_uScriptAct_SetBool_Target_290;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_290.Out)
		{
			Relay_In_286();
		}
	}

	private void Relay_True_293()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.True(out logic_uScriptAct_SetBool_Target_293);
		local_WorldCapacityFull_System_Boolean = logic_uScriptAct_SetBool_Target_293;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_293.Out)
		{
			Relay_In_211();
		}
	}

	private void Relay_False_293()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.False(out logic_uScriptAct_SetBool_Target_293);
		local_WorldCapacityFull_System_Boolean = logic_uScriptAct_SetBool_Target_293;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_293.Out)
		{
			Relay_In_211();
		}
	}

	private void Relay_In_296()
	{
		logic_uScript_AddMessage_messageData_296 = msgWorldCapacityFullReply;
		logic_uScript_AddMessage_speaker_296 = messageSpeaker;
		logic_uScript_AddMessage_Return_296 = logic_uScript_AddMessage_uScript_AddMessage_296.In(logic_uScript_AddMessage_messageData_296, logic_uScript_AddMessage_speaker_296);
	}

	private void Relay_In_298()
	{
		logic_uScriptCon_CompareBool_Bool_298 = _DEBUGIgnoreMoneyCheck;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_298.In(logic_uScriptCon_CompareBool_Bool_298);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_298.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_298.False;
		if (num)
		{
			Relay_In_299();
		}
		if (flag)
		{
			Relay_In_122();
		}
	}

	private void Relay_In_299()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_299.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_299.Out)
		{
			Relay_InitialSpawn_68();
		}
	}

	private void Relay_In_301()
	{
		logic_uScriptCon_CompareBool_Bool_301 = local_WorldCapacityFull_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301.In(logic_uScriptCon_CompareBool_Bool_301);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301.False;
		if (num)
		{
			Relay_In_296();
		}
		if (flag)
		{
			Relay_In_298();
		}
	}
}
