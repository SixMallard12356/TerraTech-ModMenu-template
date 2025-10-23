using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_BetterFutureRaceTrack : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool _DEBUGIgnoreMoneyCheck;

	public bool BlockLimitCritical;

	[Multiline(1)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius;

	public BlockTypes[] discoverableBlockTypesOnVehicle = new BlockTypes[0];

	public float distNearNPC;

	public BlockTypes interactableBlockType;

	private int local_144_System_Int32;

	private bool local_240_System_Boolean;

	private TankBlock local_242_TankBlock;

	private Tank[] local_39_TankArray = new Tank[0];

	private Tank[] local_80_TankArray = new Tank[0];

	private bool local_BeenInRangeOfEncounter_System_Boolean;

	private string local_ChallengeID_System_String = "";

	private bool local_ChallengeInProgress_System_Boolean;

	private int local_CurrentMoney_System_Int32;

	private CheckpointChallenge.EndReason local_EndReason_CheckpointChallenge_EndReason;

	private bool local_HasEnoughMoney_System_Boolean;

	private bool local_Init_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgPurchaseVehicle_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgPurchaseVehicle_Pad_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgVehicleControls_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgVehicleControls_Pad_ManOnScreenMessages_OnScreenMessage;

	private Tank local_NPCTech_Tank;

	private bool local_OutOfBounds_System_Boolean;

	private int local_PassedCheckpointIdx_System_Int32;

	private bool local_RaceAttempted_System_Boolean;

	private bool local_RampSetup_System_Boolean;

	private TrackSpline local_Spline_TrackSpline;

	private int local_Stage_System_Int32 = 1;

	private GameObject local_StarterObject_UnityEngine_GameObject;

	private GameObject local_StarterObject_UnityEngine_GameObject_previous;

	private Transform local_StartTransform_UnityEngine_Transform;

	private bool local_SwitchedVehicle_System_Boolean;

	private TankBlock local_TerminalBlock_TankBlock;

	private Tank local_Vehicle_Tank;

	private bool local_VehiclePurchased_System_Boolean;

	private bool local_WaitingOnPrompt_System_Boolean;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(1)]
	public string messageTagControls = "";

	[Multiline(1)]
	public string messageTagPurchase = "";

	public uScript_AddMessage.MessageData msgOutOfBounds;

	public LocalisedString msgPromptAccept;

	public LocalisedString msgPromptDecline;

	public LocalisedString msgPromptNoMoney;

	public LocalisedString msgPromptText;

	public uScript_AddMessage.MessageData msgPurchaseVehicle;

	public uScript_AddMessage.MessageData msgPurchaseVehicle_Pad;

	public uScript_AddMessage.MessageData msgQuitFromMenu;

	public uScript_AddMessage.MessageData msgRaceComplete;

	public uScript_AddMessage.MessageData msgTouchedGround;

	public uScript_AddMessage.MessageData msgVehicleControls;

	public uScript_AddMessage.MessageData msgVehicleControls_Pad;

	public LocalisedString msgWorldCapacityFull;

	public uScript_AddMessage.MessageData msgWorldCapacityFullReply;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	[Multiline(1)]
	public string raceStartPosition = "";

	[Multiline(1)]
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

	private GameObject owner_Connection_66;

	private GameObject owner_Connection_68;

	private GameObject owner_Connection_74;

	private GameObject owner_Connection_76;

	private GameObject owner_Connection_111;

	private GameObject owner_Connection_200;

	private GameObject owner_Connection_235;

	private GameObject owner_Connection_244;

	private GameObject owner_Connection_291;

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

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_65 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_65;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_65 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_67 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_67;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_67 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_67 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_67 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_69 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_69 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_70;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_73 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_73 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_73;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_73 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_73;

	private bool logic_uScript_SpawnTechsFromData_Out_73 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_79 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_79;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_79 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_79 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_81 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_81 = new Tank[0];

	private int logic_uScript_AccessListTech_index_81;

	private Tank logic_uScript_AccessListTech_value_81;

	private bool logic_uScript_AccessListTech_Out_81 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_82 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_82;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_82 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_82;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_82 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_82 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_82 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_82 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_83 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_83;

	private bool logic_uScriptAct_SetBool_Out_83 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_83 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_83 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_85 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_85;

	private bool logic_uScriptAct_SetBool_Out_85 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_85 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_85 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_87 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_87;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_87;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_87;

	private bool logic_uScript_AddMessage_Out_87 = true;

	private bool logic_uScript_AddMessage_Shown_87 = true;

	private uScript_GetLastChallengeResult logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_88 = new uScript_GetLastChallengeResult();

	private bool logic_uScript_GetLastChallengeResult_Success_88 = true;

	private bool logic_uScript_GetLastChallengeResult_Failure_88 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_91 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_91;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_91;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_91;

	private bool logic_uScript_AddMessage_Out_91 = true;

	private bool logic_uScript_AddMessage_Shown_91 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_92 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_92;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_92 = CheckpointChallenge.EndReason.FailedQuit;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_92 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_92 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_100 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_100;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_100 = CheckpointChallenge.EndReason.FailedOutOfBounds;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_100 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_100 = true;

	private uScript_ClearChallengeStarterChallengeData logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_101 = new uScript_ClearChallengeStarterChallengeData();

	private GameObject logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_101;

	private bool logic_uScript_ClearChallengeStarterChallengeData_Out_101 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_102 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_102;

	private bool logic_uScript_FinishEncounter_Out_102 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_103 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_103;

	private bool logic_uScriptAct_SetBool_Out_103 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_103 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_103 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_104 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_104;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_104;

	private bool logic_uScript_LockTechSendToSCU_Out_104 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_107 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_107;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_107;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_107;

	private bool logic_uScript_AddMessage_Out_107 = true;

	private bool logic_uScript_AddMessage_Shown_107 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_108 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_108;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_108;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_108;

	private bool logic_uScript_AddMessage_Out_108 = true;

	private bool logic_uScript_AddMessage_Shown_108 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_112 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_112;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_112 = CheckpointChallenge.EndReason.FailedTouchedGround;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_112 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_112 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_115 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_115;

	private bool logic_uScript_RemoveTech_Out_115 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_118 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_118 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_119 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_119 = true;

	private uScript_GetChallengeStateFromChallengeID logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_121 = new uScript_GetChallengeStateFromChallengeID();

	private string logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_121 = "";

	private bool logic_uScript_GetChallengeStateFromChallengeID_Out_121 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_NotRunning_121 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_JustStarted_121 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_InProgress_121 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_JustEnded_121 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_127 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_127;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_127;

	private bool logic_uScript_LockTech_Out_127 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_128 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_128 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_128;

	private bool logic_uScript_SetTankInvulnerable_Out_128 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_130 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_130;

	private Tank logic_uScript_SetTankInvulnerable_tank_130;

	private bool logic_uScript_SetTankInvulnerable_Out_130 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_132 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_132;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_132 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_132 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_135;

	private bool logic_uScriptCon_CompareBool_True_135 = true;

	private bool logic_uScriptCon_CompareBool_False_135 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_137 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_137 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_139 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_139 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_139 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_139 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_139 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_143;

	private bool logic_uScriptCon_CompareBool_True_143 = true;

	private bool logic_uScriptCon_CompareBool_False_143 = true;

	private uScriptAct_MultiplyInt_v2 logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_145 = new uScriptAct_MultiplyInt_v2();

	private int logic_uScriptAct_MultiplyInt_v2_A_145;

	private int logic_uScriptAct_MultiplyInt_v2_B_145 = -1;

	private int logic_uScriptAct_MultiplyInt_v2_IntResult_145;

	private float logic_uScriptAct_MultiplyInt_v2_FloatResult_145;

	private bool logic_uScriptAct_MultiplyInt_v2_Out_145 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_146 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_146;

	private bool logic_uScriptAct_SetBool_Out_146 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_146 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_146 = true;

	private uScript_AddMoney logic_uScript_AddMoney_uScript_AddMoney_147 = new uScript_AddMoney();

	private int logic_uScript_AddMoney_amount_147;

	private bool logic_uScript_AddMoney_Out_147 = true;

	private uScript_DiscoverBlocks logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_150 = new uScript_DiscoverBlocks();

	private BlockTypes[] logic_uScript_DiscoverBlocks_blockTypes_150 = new BlockTypes[0];

	private bool logic_uScript_DiscoverBlocks_Out_150 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_152 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_152;

	private bool logic_uScriptCon_CompareBool_True_152 = true;

	private bool logic_uScriptCon_CompareBool_False_152 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_154 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_154 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_156 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_156 = "";

	private bool logic_uScript_EnableGlow_enable_156;

	private bool logic_uScript_EnableGlow_Out_156 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_157 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_157 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_158 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_161 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_161;

	private float logic_uScript_IsPlayerInRangeOfTech_range_161;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_161 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_161 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_161 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_161 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_162 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_162 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_165 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_165;

	private BlockTypes logic_uScript_GetTankBlock_blockType_165;

	private TankBlock logic_uScript_GetTankBlock_Return_165;

	private bool logic_uScript_GetTankBlock_Out_165 = true;

	private bool logic_uScript_GetTankBlock_Returned_165 = true;

	private bool logic_uScript_GetTankBlock_NotFound_165 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_167 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_167;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_167 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_167 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_168;

	private bool logic_uScriptCon_CompareBool_True_168 = true;

	private bool logic_uScriptCon_CompareBool_False_168 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_171 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_171;

	private bool logic_uScriptAct_SetBool_Out_171 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_171 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_171 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_173;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_173 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_173 = "VehiclePurchased";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_174;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_174 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_174 = "SwitchedVehicle";

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_178 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_178;

	private bool logic_uScript_IsTechPlayer_Out_178 = true;

	private bool logic_uScript_IsTechPlayer_True_178 = true;

	private bool logic_uScript_IsTechPlayer_False_178 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_179 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_179;

	private bool logic_uScriptCon_CompareBool_True_179 = true;

	private bool logic_uScriptCon_CompareBool_False_179 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_182 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_182;

	private bool logic_uScriptCon_CompareBool_True_182 = true;

	private bool logic_uScriptCon_CompareBool_False_182 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_183;

	private bool logic_uScriptCon_CompareBool_True_183 = true;

	private bool logic_uScriptCon_CompareBool_False_183 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_185 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_185 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_187 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_187;

	private bool logic_uScriptAct_SetBool_Out_187 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_187 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_187 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_189 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_189;

	private bool logic_uScriptAct_SetBool_Out_189 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_189 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_189 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_190 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_190;

	private bool logic_uScriptAct_SetBool_Out_190 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_190 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_190 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_193 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_193;

	private bool logic_uScriptAct_SetBool_Out_193 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_193 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_193 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_195 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_195 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_196 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_196 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_201 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_201 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_203 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_203;

	private bool logic_uScriptAct_SetBool_Out_203 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_203 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_203 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_205;

	private bool logic_uScriptCon_CompareBool_True_205 = true;

	private bool logic_uScriptCon_CompareBool_False_205 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_206 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_206 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_207;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_207 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_207 = "RaceAttempted";

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_210 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_210;

	private float logic_uScript_IsPlayerInRangeOfTech_range_210;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_210 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_210 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_210 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_210 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_212;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_212;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_212;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_212;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_212;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_217;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_217;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_217;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_217;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_217;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_224 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_224 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_224 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_224 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_226 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_226 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_226 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_226 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_228 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_228 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_228 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_228 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_231 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_231 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_231 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_231 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_232 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_232 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_232 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_232 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_234 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_234 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_234;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_234 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_234;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_234 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_234 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_234 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_234 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_237 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_237 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_238 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_238 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_243 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_243;

	private TankBlock logic_uScript_CompareBlock_B_243;

	private bool logic_uScript_CompareBlock_EqualTo_243 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_243 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_245;

	private bool logic_uScriptCon_CompareBool_True_245 = true;

	private bool logic_uScriptCon_CompareBool_False_245 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_247 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_247;

	private bool logic_uScriptCon_CompareBool_True_247 = true;

	private bool logic_uScriptCon_CompareBool_False_247 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_248 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_248;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_248;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_248;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_248;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_248 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_248;

	private bool logic_uScript_MissionPromptBlock_Show_Out_248 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_250 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_250;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_250 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_251 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_251;

	private int logic_uScriptCon_CompareInt_B_251;

	private bool logic_uScriptCon_CompareInt_GreaterThan_251 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_251 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_251 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_251 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_251 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_251 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_253 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_253 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_256 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_256;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_256;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_256;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_256;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_256 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_256;

	private bool logic_uScript_MissionPromptBlock_Show_Out_256 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_262 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_262;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_262 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_264 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_264 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_264 = true;

	private uScript_CanSpawnPlayerTechsWithinBlockLimit logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_266 = new uScript_CanSpawnPlayerTechsWithinBlockLimit();

	private SpawnTechData[] logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_266 = new SpawnTechData[0];

	private int logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_266 = 1;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_Out_266 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_True_266 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_False_266 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_268 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_268;

	private bool logic_uScriptAct_SetBool_Out_268 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_268 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_268 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_271 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_271;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_271;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_271;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_271;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_271;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_271;

	private bool logic_uScript_MissionPromptBlock_Show_Out_271 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_274 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_274;

	private bool logic_uScriptAct_SetBool_Out_274 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_274 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_274 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_277 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_277;

	private bool logic_uScriptCon_CompareBool_True_277 = true;

	private bool logic_uScriptCon_CompareBool_False_277 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_278 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_278;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_278;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_278;

	private bool logic_uScript_AddMessage_Out_278 = true;

	private bool logic_uScript_AddMessage_Shown_278 = true;

	private uScript_ClearSceneryAlongSpline logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_282 = new uScript_ClearSceneryAlongSpline();

	private Transform logic_uScript_ClearSceneryAlongSpline_splineStartTrans_282;

	private TrackSpline logic_uScript_ClearSceneryAlongSpline_spline_282;

	private float logic_uScript_ClearSceneryAlongSpline_delayBetweenAreaClears_282;

	private Transform logic_uScript_ClearSceneryAlongSpline_sceneryClearSFXPrefab_282;

	private float logic_uScript_ClearSceneryAlongSpline_stepSizeWidthPercentage_282 = 0.8f;

	private bool logic_uScript_ClearSceneryAlongSpline_clearUpToPenaltyWidth_282 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_Out_282 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_BusyClearing_282 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_DoneClearing_282 = true;

	private uScript_ClearSceneryAlongSpline logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_285 = new uScript_ClearSceneryAlongSpline();

	private Transform logic_uScript_ClearSceneryAlongSpline_splineStartTrans_285;

	private TrackSpline logic_uScript_ClearSceneryAlongSpline_spline_285;

	private float logic_uScript_ClearSceneryAlongSpline_delayBetweenAreaClears_285;

	private Transform logic_uScript_ClearSceneryAlongSpline_sceneryClearSFXPrefab_285;

	private float logic_uScript_ClearSceneryAlongSpline_stepSizeWidthPercentage_285 = 0.8f;

	private bool logic_uScript_ClearSceneryAlongSpline_clearUpToPenaltyWidth_285 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_Out_285 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_BusyClearing_285 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_DoneClearing_285 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_287 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_287;

	private bool logic_uScriptAct_SetBool_Out_287 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_287 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_287 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_288 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_288;

	private bool logic_uScriptCon_CompareBool_True_288 = true;

	private bool logic_uScriptCon_CompareBool_False_288 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_290 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_290;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_290 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_290 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_290 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_293 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_293;

	private bool logic_uScriptAct_SetBool_Out_293 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_293 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_293 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_294 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_294;

	private bool logic_uScriptAct_SetBool_Out_294 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_294 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_294 = true;

	private int event_UnityEngine_GameObject_CheckpointIndex_25;

	private CheckpointChallenge.EndReason event_UnityEngine_GameObject_EndReason_199;

	private float event_UnityEngine_GameObject_EndTime_199;

	private TankBlock event_UnityEngine_GameObject_TankBlock_241;

	private bool event_UnityEngine_GameObject_Accepted_241;

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
		if (null == owner_Connection_66 || !m_RegisteredForEvents)
		{
			owner_Connection_66 = parentGameObject;
		}
		if (null == owner_Connection_68 || !m_RegisteredForEvents)
		{
			owner_Connection_68 = parentGameObject;
		}
		if (null == owner_Connection_74 || !m_RegisteredForEvents)
		{
			owner_Connection_74 = parentGameObject;
		}
		if (null == owner_Connection_76 || !m_RegisteredForEvents)
		{
			owner_Connection_76 = parentGameObject;
		}
		if (null == owner_Connection_111 || !m_RegisteredForEvents)
		{
			owner_Connection_111 = parentGameObject;
		}
		if (null == owner_Connection_200 || !m_RegisteredForEvents)
		{
			owner_Connection_200 = parentGameObject;
			if (null != owner_Connection_200)
			{
				uScript_CheckPointChallengeEndedEvent uScript_CheckPointChallengeEndedEvent2 = owner_Connection_200.GetComponent<uScript_CheckPointChallengeEndedEvent>();
				if (null == uScript_CheckPointChallengeEndedEvent2)
				{
					uScript_CheckPointChallengeEndedEvent2 = owner_Connection_200.AddComponent<uScript_CheckPointChallengeEndedEvent>();
				}
				if (null != uScript_CheckPointChallengeEndedEvent2)
				{
					uScript_CheckPointChallengeEndedEvent2.OnSuccess += Instance_OnSuccess_199;
					uScript_CheckPointChallengeEndedEvent2.OnFail += Instance_OnFail_199;
				}
			}
		}
		if (null == owner_Connection_235 || !m_RegisteredForEvents)
		{
			owner_Connection_235 = parentGameObject;
		}
		if (null == owner_Connection_244 || !m_RegisteredForEvents)
		{
			owner_Connection_244 = parentGameObject;
			if (null != owner_Connection_244)
			{
				uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_244.GetComponent<uScript_MissionPromptBlock_OnResult>();
				if (null == uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2 = owner_Connection_244.AddComponent<uScript_MissionPromptBlock_OnResult>();
				}
				if (null != uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_241;
				}
			}
		}
		if (null == owner_Connection_291 || !m_RegisteredForEvents)
		{
			owner_Connection_291 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_200)
		{
			uScript_CheckPointChallengeEndedEvent uScript_CheckPointChallengeEndedEvent2 = owner_Connection_200.GetComponent<uScript_CheckPointChallengeEndedEvent>();
			if (null == uScript_CheckPointChallengeEndedEvent2)
			{
				uScript_CheckPointChallengeEndedEvent2 = owner_Connection_200.AddComponent<uScript_CheckPointChallengeEndedEvent>();
			}
			if (null != uScript_CheckPointChallengeEndedEvent2)
			{
				uScript_CheckPointChallengeEndedEvent2.OnSuccess += Instance_OnSuccess_199;
				uScript_CheckPointChallengeEndedEvent2.OnFail += Instance_OnFail_199;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_244)
		{
			uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_244.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null == uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2 = owner_Connection_244.AddComponent<uScript_MissionPromptBlock_OnResult>();
			}
			if (null != uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_241;
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
		if (null != owner_Connection_200)
		{
			uScript_CheckPointChallengeEndedEvent component5 = owner_Connection_200.GetComponent<uScript_CheckPointChallengeEndedEvent>();
			if (null != component5)
			{
				component5.OnSuccess -= Instance_OnSuccess_199;
				component5.OnFail -= Instance_OnFail_199;
			}
		}
		if (null != owner_Connection_244)
		{
			uScript_MissionPromptBlock_OnResult component6 = owner_Connection_244.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null != component6)
			{
				component6.ResponseEvent -= Instance_ResponseEvent_241;
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
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_65.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_67.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_69.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_73.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_79.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_81.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_83.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_85.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_87.SetParent(g);
		logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_88.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_91.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_92.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_100.SetParent(g);
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_101.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_102.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_103.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_104.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_107.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_108.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_112.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_115.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_118.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_119.SetParent(g);
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_121.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_127.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_128.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_130.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_132.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_137.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_139.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.SetParent(g);
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_145.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_146.SetParent(g);
		logic_uScript_AddMoney_uScript_AddMoney_147.SetParent(g);
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_150.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_152.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_154.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_156.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_157.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_161.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_162.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_165.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_167.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_178.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_179.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_182.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_185.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_187.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_189.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_193.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_195.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_196.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_201.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_203.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_206.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_210.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_224.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_226.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_228.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_231.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_232.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_234.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_237.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_238.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_243.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_247.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_248.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_250.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_251.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_253.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_256.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_262.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_264.SetParent(g);
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_266.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_268.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_271.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_274.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_277.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_278.SetParent(g);
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_282.SetParent(g);
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_285.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_287.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_288.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_290.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_294.SetParent(g);
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
		owner_Connection_66 = parentGameObject;
		owner_Connection_68 = parentGameObject;
		owner_Connection_74 = parentGameObject;
		owner_Connection_76 = parentGameObject;
		owner_Connection_111 = parentGameObject;
		owner_Connection_200 = parentGameObject;
		owner_Connection_235 = parentGameObject;
		owner_Connection_244 = parentGameObject;
		owner_Connection_291 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Save_Out += SubGraph_SaveLoadInt_Save_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Load_Out += SubGraph_SaveLoadInt_Load_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_55;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.Out += SubGraph_LoadObjectiveStates_Out_56;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output1 += uScriptCon_ManualSwitch_Output1_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output2 += uScriptCon_ManualSwitch_Output2_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output3 += uScriptCon_ManualSwitch_Output3_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output4 += uScriptCon_ManualSwitch_Output4_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output5 += uScriptCon_ManualSwitch_Output5_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output6 += uScriptCon_ManualSwitch_Output6_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output7 += uScriptCon_ManualSwitch_Output7_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output8 += uScriptCon_ManualSwitch_Output8_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Save_Out += SubGraph_SaveLoadBool_Save_Out_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Load_Out += SubGraph_SaveLoadBool_Load_Out_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Save_Out += SubGraph_SaveLoadBool_Save_Out_174;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Load_Out += SubGraph_SaveLoadBool_Load_Out_174;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_174;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Save_Out += SubGraph_SaveLoadBool_Save_Out_207;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Load_Out += SubGraph_SaveLoadBool_Load_Out_207;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_207;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212.Out += SubGraph_AddMessageWithPadSupport_Out_212;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212.Shown += SubGraph_AddMessageWithPadSupport_Shown_212;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217.Out += SubGraph_AddMessageWithPadSupport_Out_217;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217.Shown += SubGraph_AddMessageWithPadSupport_Shown_217;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_65.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217.OnEnable();
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
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_67.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_87.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_91.OnDisable();
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_101.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_107.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_108.OnDisable();
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_121.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_128.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_130.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_161.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_165.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_210.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217.OnDisable();
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_266.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_278.OnDisable();
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_282.OnDisable();
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_285.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_290.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Save_Out -= SubGraph_SaveLoadInt_Save_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Load_Out -= SubGraph_SaveLoadInt_Load_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_55;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_56.Out -= SubGraph_LoadObjectiveStates_Out_56;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output1 -= uScriptCon_ManualSwitch_Output1_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output2 -= uScriptCon_ManualSwitch_Output2_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output3 -= uScriptCon_ManualSwitch_Output3_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output4 -= uScriptCon_ManualSwitch_Output4_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output5 -= uScriptCon_ManualSwitch_Output5_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output6 -= uScriptCon_ManualSwitch_Output6_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output7 -= uScriptCon_ManualSwitch_Output7_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.Output8 -= uScriptCon_ManualSwitch_Output8_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Save_Out -= SubGraph_SaveLoadBool_Save_Out_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Load_Out -= SubGraph_SaveLoadBool_Load_Out_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Save_Out -= SubGraph_SaveLoadBool_Save_Out_174;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Load_Out -= SubGraph_SaveLoadBool_Load_Out_174;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_174;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Save_Out -= SubGraph_SaveLoadBool_Save_Out_207;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Load_Out -= SubGraph_SaveLoadBool_Load_Out_207;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_207;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212.Out -= SubGraph_AddMessageWithPadSupport_Out_212;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212.Shown -= SubGraph_AddMessageWithPadSupport_Shown_212;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217.Out -= SubGraph_AddMessageWithPadSupport_Out_217;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217.Shown -= SubGraph_AddMessageWithPadSupport_Shown_217;
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

	private void Instance_OnSuccess_199(object o, uScript_CheckPointChallengeEndedEvent.CheckpointChallengeEndedEventArgs e)
	{
		event_UnityEngine_GameObject_EndReason_199 = e.EndReason;
		event_UnityEngine_GameObject_EndTime_199 = e.EndTime;
		Relay_OnSuccess_199();
	}

	private void Instance_OnFail_199(object o, uScript_CheckPointChallengeEndedEvent.CheckpointChallengeEndedEventArgs e)
	{
		event_UnityEngine_GameObject_EndReason_199 = e.EndReason;
		event_UnityEngine_GameObject_EndTime_199 = e.EndTime;
		Relay_OnFail_199();
	}

	private void Instance_ResponseEvent_241(object o, uScript_MissionPromptBlock_OnResult.PromptResultEventArgs e)
	{
		event_UnityEngine_GameObject_TankBlock_241 = e.TankBlock;
		event_UnityEngine_GameObject_Accepted_241 = e.Accepted;
		Relay_ResponseEvent_241();
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

	private void uScriptCon_ManualSwitch_Output1_70(object o, EventArgs e)
	{
		Relay_Output1_70();
	}

	private void uScriptCon_ManualSwitch_Output2_70(object o, EventArgs e)
	{
		Relay_Output2_70();
	}

	private void uScriptCon_ManualSwitch_Output3_70(object o, EventArgs e)
	{
		Relay_Output3_70();
	}

	private void uScriptCon_ManualSwitch_Output4_70(object o, EventArgs e)
	{
		Relay_Output4_70();
	}

	private void uScriptCon_ManualSwitch_Output5_70(object o, EventArgs e)
	{
		Relay_Output5_70();
	}

	private void uScriptCon_ManualSwitch_Output6_70(object o, EventArgs e)
	{
		Relay_Output6_70();
	}

	private void uScriptCon_ManualSwitch_Output7_70(object o, EventArgs e)
	{
		Relay_Output7_70();
	}

	private void uScriptCon_ManualSwitch_Output8_70(object o, EventArgs e)
	{
		Relay_Output8_70();
	}

	private void SubGraph_SaveLoadBool_Save_Out_173(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_173;
		Relay_Save_Out_173();
	}

	private void SubGraph_SaveLoadBool_Load_Out_173(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_173;
		Relay_Load_Out_173();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_173(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_173;
		Relay_Restart_Out_173();
	}

	private void SubGraph_SaveLoadBool_Save_Out_174(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_174 = e.boolean;
		local_SwitchedVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_174;
		Relay_Save_Out_174();
	}

	private void SubGraph_SaveLoadBool_Load_Out_174(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_174 = e.boolean;
		local_SwitchedVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_174;
		Relay_Load_Out_174();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_174(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_174 = e.boolean;
		local_SwitchedVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_174;
		Relay_Restart_Out_174();
	}

	private void SubGraph_SaveLoadBool_Save_Out_207(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = e.boolean;
		local_RaceAttempted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_207;
		Relay_Save_Out_207();
	}

	private void SubGraph_SaveLoadBool_Load_Out_207(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = e.boolean;
		local_RaceAttempted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_207;
		Relay_Load_Out_207();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_207(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = e.boolean;
		local_RaceAttempted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_207;
		Relay_Restart_Out_207();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_212(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_212 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_212 = e.messageControlPadReturn;
		local_MsgVehicleControls_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_212;
		local_MsgVehicleControls_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_212;
		Relay_Out_212();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_212(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_212 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_212 = e.messageControlPadReturn;
		local_MsgVehicleControls_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_212;
		local_MsgVehicleControls_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_212;
		Relay_Shown_212();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_217(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_217 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_217 = e.messageControlPadReturn;
		local_MsgPurchaseVehicle_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_217;
		local_MsgPurchaseVehicle_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_217;
		Relay_Out_217();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_217(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_217 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_217 = e.messageControlPadReturn;
		local_MsgPurchaseVehicle_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_217;
		local_MsgPurchaseVehicle_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_217;
		Relay_Shown_217();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_288();
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
			Relay_In_40();
		}
		if (flag)
		{
			Relay_True_4();
		}
	}

	private void Relay_True_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.True(out logic_uScriptAct_SetBool_Target_4);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_False_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.False(out logic_uScriptAct_SetBool_Target_4);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_Save_Out_7()
	{
		Relay_Save_173();
	}

	private void Relay_Load_Out_7()
	{
		Relay_Load_173();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_Set_False_173();
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
			Relay_In_282();
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
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_40.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_40, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_40, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
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
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_47();
		}
		if (someAlive)
		{
			Relay_AtIndex_47();
		}
		if (allDead)
		{
			Relay_In_118();
		}
		if (waitingToSpawn)
		{
			Relay_In_201();
		}
	}

	private void Relay_InitialSpawn_43()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_43.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_43, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_43, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_43 = owner_Connection_41;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_43.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_43, logic_uScript_SpawnTechsFromData_ownerNode_43, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_43, logic_uScript_SpawnTechsFromData_allowResurrection_43);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_43.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_46()
	{
		logic_uScript_SetTankInvulnerable_tank_46 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_46.In(logic_uScript_SetTankInvulnerable_invulnerable_46, logic_uScript_SetTankInvulnerable_tank_46);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_46.Out)
		{
			Relay_In_165();
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
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_47;
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
			Relay_In_234();
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
			Relay_False_189();
		}
	}

	private void Relay_False_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.False(out logic_uScriptAct_SetBool_Target_53);
		local_RampSetup_System_Boolean = logic_uScriptAct_SetBool_Target_53;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_53.Out)
		{
			Relay_False_189();
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
		logic_uScript_IsPlayerInRangeOfTech_tech_60 = local_NPCTech_Tank;
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
			Relay_In_264();
		}
	}

	private void Relay_In_62()
	{
		logic_uScript_SetEncounterTarget_owner_62 = owner_Connection_61;
		logic_uScript_SetEncounterTarget_visibleObject_62 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_62.In(logic_uScript_SetEncounterTarget_owner_62, logic_uScript_SetEncounterTarget_visibleObject_62);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_62.Out)
		{
			Relay_In_179();
		}
	}

	private void Relay_In_65()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_65 = owner_Connection_66;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_65.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_65);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_65.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_67()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_67 = owner_Connection_68;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_67.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_67);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_67.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_67.False;
		if (num)
		{
			Relay_Pause_69();
		}
		if (flag)
		{
			Relay_UnPause_69();
		}
	}

	private void Relay_Pause_69()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_69.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_69.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_UnPause_69()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_69.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_69.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_Output1_70()
	{
		Relay_In_121();
	}

	private void Relay_Output2_70()
	{
	}

	private void Relay_Output3_70()
	{
	}

	private void Relay_Output4_70()
	{
	}

	private void Relay_Output5_70()
	{
	}

	private void Relay_Output6_70()
	{
	}

	private void Relay_Output7_70()
	{
	}

	private void Relay_Output8_70()
	{
	}

	private void Relay_In_70()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_70 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_70.In(logic_uScriptCon_ManualSwitch_CurrentOutput_70);
	}

	private void Relay_InitialSpawn_73()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_73.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_73, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_73, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_73 = owner_Connection_74;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_73.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_73, logic_uScript_SpawnTechsFromData_ownerNode_73, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_73, logic_uScript_SpawnTechsFromData_allowResurrection_73);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_73.Out)
		{
			Relay_In_150();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_LockTechSendToSCU_tech_79 = local_Vehicle_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_79.In(logic_uScript_LockTechSendToSCU_tech_79, logic_uScript_LockTechSendToSCU_lockSendToSCU_79);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_79.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_AtIndex_81()
	{
		int num = 0;
		Array array = local_80_TankArray;
		if (logic_uScript_AccessListTech_techList_81.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_81, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_81, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_81.AtIndex(ref logic_uScript_AccessListTech_techList_81, logic_uScript_AccessListTech_index_81, out logic_uScript_AccessListTech_value_81);
		local_80_TankArray = logic_uScript_AccessListTech_techList_81;
		local_Vehicle_Tank = logic_uScript_AccessListTech_value_81;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_81.Out)
		{
			Relay_In_178();
		}
	}

	private void Relay_In_82()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_82.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_82, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_82, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_82 = owner_Connection_76;
		int num2 = 0;
		Array array2 = local_80_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_82.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_82, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_82, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_82 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.In(logic_uScript_GetAndCheckTechs_techData_82, logic_uScript_GetAndCheckTechs_ownerNode_82, ref logic_uScript_GetAndCheckTechs_techs_82);
		local_80_TankArray = logic_uScript_GetAndCheckTechs_techs_82;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_81();
		}
		if (someAlive)
		{
			Relay_AtIndex_81();
		}
		if (allDead)
		{
			Relay_In_119();
		}
		if (waitingToSpawn)
		{
			Relay_In_157();
		}
	}

	private void Relay_True_83()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_83.True(out logic_uScriptAct_SetBool_Target_83);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_83;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_83.Out)
		{
			Relay_False_85();
		}
	}

	private void Relay_False_83()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_83.False(out logic_uScriptAct_SetBool_Target_83);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_83;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_83.Out)
		{
			Relay_False_85();
		}
	}

	private void Relay_True_85()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_85.True(out logic_uScriptAct_SetBool_Target_85);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_85;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_85.Out)
		{
			Relay_In_285();
		}
	}

	private void Relay_False_85()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_85.False(out logic_uScriptAct_SetBool_Target_85);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_85;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_85.Out)
		{
			Relay_In_285();
		}
	}

	private void Relay_In_87()
	{
		logic_uScript_AddMessage_messageData_87 = msgRaceComplete;
		logic_uScript_AddMessage_speaker_87 = messageSpeaker;
		logic_uScript_AddMessage_Return_87 = logic_uScript_AddMessage_uScript_AddMessage_87.In(logic_uScript_AddMessage_messageData_87, logic_uScript_AddMessage_speaker_87);
		if (logic_uScript_AddMessage_uScript_AddMessage_87.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_In_88()
	{
		logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_88.In();
		bool success = logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_88.Success;
		bool failure = logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_88.Failure;
		if (success)
		{
			Relay_In_87();
		}
		if (failure)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_91()
	{
		logic_uScript_AddMessage_messageData_91 = msgQuitFromMenu;
		logic_uScript_AddMessage_speaker_91 = messageSpeaker;
		logic_uScript_AddMessage_Return_91 = logic_uScript_AddMessage_uScript_AddMessage_91.In(logic_uScript_AddMessage_messageData_91, logic_uScript_AddMessage_speaker_91);
	}

	private void Relay_In_92()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_92 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_92.In(logic_uScript_CompareCheckpointChallengeEndReason_result_92, logic_uScript_CompareCheckpointChallengeEndReason_expected_92);
		if (logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_92.EqualTo)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_100()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_100 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_100.In(logic_uScript_CompareCheckpointChallengeEndReason_result_100, logic_uScript_CompareCheckpointChallengeEndReason_expected_100);
		bool equalTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_100.EqualTo;
		bool notEqualTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_100.NotEqualTo;
		if (equalTo)
		{
			Relay_In_107();
		}
		if (notEqualTo)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_101()
	{
		if (local_StarterObject_UnityEngine_GameObject_previous != local_StarterObject_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StarterObject_UnityEngine_GameObject_previous = local_StarterObject_UnityEngine_GameObject;
		}
		logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_101 = local_StarterObject_UnityEngine_GameObject;
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_101.In(logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_101);
		if (logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_101.Out)
		{
			Relay_Succeed_102();
		}
	}

	private void Relay_Succeed_102()
	{
		logic_uScript_FinishEncounter_owner_102 = owner_Connection_111;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_102.Succeed(logic_uScript_FinishEncounter_owner_102);
	}

	private void Relay_Fail_102()
	{
		logic_uScript_FinishEncounter_owner_102 = owner_Connection_111;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_102.Fail(logic_uScript_FinishEncounter_owner_102);
	}

	private void Relay_True_103()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_103.True(out logic_uScriptAct_SetBool_Target_103);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_103;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_103.Out)
		{
			Relay_UnPause_196();
		}
	}

	private void Relay_False_103()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_103.False(out logic_uScriptAct_SetBool_Target_103);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_103;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_103.Out)
		{
			Relay_UnPause_196();
		}
	}

	private void Relay_In_104()
	{
		logic_uScript_LockTechSendToSCU_tech_104 = local_Vehicle_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_104.In(logic_uScript_LockTechSendToSCU_tech_104, logic_uScript_LockTechSendToSCU_lockSendToSCU_104);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_104.Out)
		{
			Relay_In_130();
		}
	}

	private void Relay_In_107()
	{
		logic_uScript_AddMessage_messageData_107 = msgOutOfBounds;
		logic_uScript_AddMessage_speaker_107 = messageSpeaker;
		logic_uScript_AddMessage_Return_107 = logic_uScript_AddMessage_uScript_AddMessage_107.In(logic_uScript_AddMessage_messageData_107, logic_uScript_AddMessage_speaker_107);
	}

	private void Relay_In_108()
	{
		logic_uScript_AddMessage_messageData_108 = msgTouchedGround;
		logic_uScript_AddMessage_speaker_108 = messageSpeaker;
		logic_uScript_AddMessage_Return_108 = logic_uScript_AddMessage_uScript_AddMessage_108.In(logic_uScript_AddMessage_messageData_108, logic_uScript_AddMessage_speaker_108);
	}

	private void Relay_In_112()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_112 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_112.In(logic_uScript_CompareCheckpointChallengeEndReason_result_112, logic_uScript_CompareCheckpointChallengeEndReason_expected_112);
		bool equalTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_112.EqualTo;
		bool notEqualTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_112.NotEqualTo;
		if (equalTo)
		{
			Relay_In_108();
		}
		if (notEqualTo)
		{
			Relay_In_92();
		}
	}

	private void Relay_In_115()
	{
		logic_uScript_RemoveTech_tech_115 = local_NPCTech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_115.In(logic_uScript_RemoveTech_tech_115);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_115.Out)
		{
			Relay_In_101();
		}
	}

	private void Relay_In_118()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_118.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_118.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_119()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_119.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_119.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_121()
	{
		logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_121 = local_ChallengeID_System_String;
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_121.In(logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_121);
		bool notRunning = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_121.NotRunning;
		bool justStarted = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_121.JustStarted;
		bool inProgress = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_121.InProgress;
		bool justEnded = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_121.JustEnded;
		if (notRunning)
		{
			Relay_In_152();
		}
		if (justStarted)
		{
			Relay_In_154();
		}
		if (inProgress)
		{
			Relay_Pause_195();
		}
		if (justEnded)
		{
			Relay_False_103();
		}
	}

	private void Relay_In_127()
	{
		logic_uScript_LockTech_tech_127 = local_Vehicle_Tank;
		logic_uScript_LockTech_uScript_LockTech_127.In(logic_uScript_LockTech_tech_127, logic_uScript_LockTech_lockType_127);
		if (logic_uScript_LockTech_uScript_LockTech_127.Out)
		{
			Relay_In_132();
		}
	}

	private void Relay_In_128()
	{
		logic_uScript_SetTankInvulnerable_tank_128 = local_Vehicle_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_128.In(logic_uScript_SetTankInvulnerable_invulnerable_128, logic_uScript_SetTankInvulnerable_tank_128);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_128.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_130()
	{
		logic_uScript_SetTankInvulnerable_tank_130 = local_Vehicle_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_130.In(logic_uScript_SetTankInvulnerable_invulnerable_130, logic_uScript_SetTankInvulnerable_tank_130);
	}

	private void Relay_In_132()
	{
		logic_uScript_LockTech_tech_132 = local_Vehicle_Tank;
		logic_uScript_LockTech_uScript_LockTech_132.In(logic_uScript_LockTech_tech_132, logic_uScript_LockTech_lockType_132);
		if (logic_uScript_LockTech_uScript_LockTech_132.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_135()
	{
		logic_uScriptCon_CompareBool_Bool_135 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.In(logic_uScriptCon_CompareBool_Bool_135);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.False;
		if (num)
		{
			Relay_In_82();
		}
		if (flag)
		{
			Relay_In_137();
		}
	}

	private void Relay_In_137()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_137.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_137.Out)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_139()
	{
		logic_uScript_PointArrowAtVisible_targetObject_139 = local_TerminalBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_139.In(logic_uScript_PointArrowAtVisible_targetObject_139, logic_uScript_PointArrowAtVisible_timeToShowFor_139, logic_uScript_PointArrowAtVisible_offset_139);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_139.Out)
		{
			Relay_In_266();
		}
	}

	private void Relay_In_143()
	{
		logic_uScriptCon_CompareBool_Bool_143 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.In(logic_uScriptCon_CompareBool_Bool_143);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.True)
		{
			Relay_In_145();
		}
	}

	private void Relay_In_145()
	{
		logic_uScriptAct_MultiplyInt_v2_A_145 = vehicleCost;
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_145.In(logic_uScriptAct_MultiplyInt_v2_A_145, logic_uScriptAct_MultiplyInt_v2_B_145, out logic_uScriptAct_MultiplyInt_v2_IntResult_145, out logic_uScriptAct_MultiplyInt_v2_FloatResult_145);
		local_144_System_Int32 = logic_uScriptAct_MultiplyInt_v2_IntResult_145;
		if (logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_145.Out)
		{
			Relay_In_147();
		}
	}

	private void Relay_True_146()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_146.True(out logic_uScriptAct_SetBool_Target_146);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_146;
	}

	private void Relay_False_146()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_146.False(out logic_uScriptAct_SetBool_Target_146);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_146;
	}

	private void Relay_In_147()
	{
		logic_uScript_AddMoney_amount_147 = local_144_System_Int32;
		logic_uScript_AddMoney_uScript_AddMoney_147.In(logic_uScript_AddMoney_amount_147);
		if (logic_uScript_AddMoney_uScript_AddMoney_147.Out)
		{
			Relay_InitialSpawn_73();
		}
	}

	private void Relay_In_150()
	{
		int num = 0;
		Array array = discoverableBlockTypesOnVehicle;
		if (logic_uScript_DiscoverBlocks_blockTypes_150.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DiscoverBlocks_blockTypes_150, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DiscoverBlocks_blockTypes_150, num, array.Length);
		num += array.Length;
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_150.In(logic_uScript_DiscoverBlocks_blockTypes_150);
		if (logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_150.Out)
		{
			Relay_True_146();
		}
	}

	private void Relay_In_152()
	{
		logic_uScriptCon_CompareBool_Bool_152 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_152.In(logic_uScriptCon_CompareBool_Bool_152);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_152.False)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_154()
	{
		logic_uScript_HideArrow_uScript_HideArrow_154.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_154.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_EnableGlow_targetObject_156 = local_TerminalBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_156.In(logic_uScript_EnableGlow_targetObject_156, logic_uScript_EnableGlow_enable_156);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_156.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_In_157()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_157.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_157.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_158()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_161()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_161 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_161 = distNearNPC;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_161.In(logic_uScript_IsPlayerInRangeOfTech_tech_161, logic_uScript_IsPlayerInRangeOfTech_range_161, logic_uScript_IsPlayerInRangeOfTech_techs_161);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_161.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_161.OutOfRange;
		if (inRange)
		{
			Relay_In_139();
		}
		if (outOfRange)
		{
			Relay_In_162();
		}
	}

	private void Relay_In_162()
	{
		logic_uScript_HideArrow_uScript_HideArrow_162.In();
	}

	private void Relay_In_165()
	{
		logic_uScript_GetTankBlock_tank_165 = local_NPCTech_Tank;
		logic_uScript_GetTankBlock_blockType_165 = interactableBlockType;
		logic_uScript_GetTankBlock_Return_165 = logic_uScript_GetTankBlock_uScript_GetTankBlock_165.In(logic_uScript_GetTankBlock_tank_165, logic_uScript_GetTankBlock_blockType_165);
		local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_165;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_165.Returned)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_167()
	{
		logic_uScript_SetBatteryChargeAmount_tech_167 = local_Vehicle_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_167.In(logic_uScript_SetBatteryChargeAmount_tech_167, logic_uScript_SetBatteryChargeAmount_chargeAmount_167);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_167.Out)
		{
			Relay_In_205();
		}
	}

	private void Relay_In_168()
	{
		logic_uScriptCon_CompareBool_Bool_168 = local_SwitchedVehicle_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.In(logic_uScriptCon_CompareBool_Bool_168);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.False;
		if (num)
		{
			Relay_In_205();
		}
		if (flag)
		{
			Relay_True_171();
		}
	}

	private void Relay_True_171()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.True(out logic_uScriptAct_SetBool_Target_171);
		local_SwitchedVehicle_System_Boolean = logic_uScriptAct_SetBool_Target_171;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_171.Out)
		{
			Relay_In_167();
		}
	}

	private void Relay_False_171()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.False(out logic_uScriptAct_SetBool_Target_171);
		local_SwitchedVehicle_System_Boolean = logic_uScriptAct_SetBool_Target_171;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_171.Out)
		{
			Relay_In_167();
		}
	}

	private void Relay_Save_Out_173()
	{
		Relay_Save_174();
	}

	private void Relay_Load_Out_173()
	{
		Relay_Load_174();
	}

	private void Relay_Restart_Out_173()
	{
		Relay_Set_False_174();
	}

	private void Relay_Save_173()
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_173 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Save(ref logic_SubGraph_SaveLoadBool_boolean_173, logic_SubGraph_SaveLoadBool_boolAsVariable_173, logic_SubGraph_SaveLoadBool_uniqueID_173);
	}

	private void Relay_Load_173()
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_173 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Load(ref logic_SubGraph_SaveLoadBool_boolean_173, logic_SubGraph_SaveLoadBool_boolAsVariable_173, logic_SubGraph_SaveLoadBool_uniqueID_173);
	}

	private void Relay_Set_True_173()
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_173 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_173, logic_SubGraph_SaveLoadBool_boolAsVariable_173, logic_SubGraph_SaveLoadBool_uniqueID_173);
	}

	private void Relay_Set_False_173()
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_173 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_173, logic_SubGraph_SaveLoadBool_boolAsVariable_173, logic_SubGraph_SaveLoadBool_uniqueID_173);
	}

	private void Relay_Save_Out_174()
	{
		Relay_Save_207();
	}

	private void Relay_Load_Out_174()
	{
		Relay_Load_207();
	}

	private void Relay_Restart_Out_174()
	{
		Relay_Set_False_207();
	}

	private void Relay_Save_174()
	{
		logic_SubGraph_SaveLoadBool_boolean_174 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_174 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Save(ref logic_SubGraph_SaveLoadBool_boolean_174, logic_SubGraph_SaveLoadBool_boolAsVariable_174, logic_SubGraph_SaveLoadBool_uniqueID_174);
	}

	private void Relay_Load_174()
	{
		logic_SubGraph_SaveLoadBool_boolean_174 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_174 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Load(ref logic_SubGraph_SaveLoadBool_boolean_174, logic_SubGraph_SaveLoadBool_boolAsVariable_174, logic_SubGraph_SaveLoadBool_uniqueID_174);
	}

	private void Relay_Set_True_174()
	{
		logic_SubGraph_SaveLoadBool_boolean_174 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_174 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_174, logic_SubGraph_SaveLoadBool_boolAsVariable_174, logic_SubGraph_SaveLoadBool_uniqueID_174);
	}

	private void Relay_Set_False_174()
	{
		logic_SubGraph_SaveLoadBool_boolean_174 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_174 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_174.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_174, logic_SubGraph_SaveLoadBool_boolAsVariable_174, logic_SubGraph_SaveLoadBool_uniqueID_174);
	}

	private void Relay_In_178()
	{
		logic_uScript_IsTechPlayer_tech_178 = local_Vehicle_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_178.In(logic_uScript_IsTechPlayer_tech_178);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_178.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_178.False;
		if (num)
		{
			Relay_In_168();
		}
		if (flag)
		{
			Relay_In_224();
		}
	}

	private void Relay_In_179()
	{
		logic_uScriptCon_CompareBool_Bool_179 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_179.In(logic_uScriptCon_CompareBool_Bool_179);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_179.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_179.False;
		if (num)
		{
			Relay_In_262();
		}
		if (flag)
		{
			Relay_In_182();
		}
	}

	private void Relay_In_182()
	{
		logic_uScriptCon_CompareBool_Bool_182 = local_ChallengeInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_182.In(logic_uScriptCon_CompareBool_Bool_182);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_182.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_182.False;
		if (num)
		{
			Relay_In_185();
		}
		if (flag)
		{
			Relay_In_183();
		}
	}

	private void Relay_In_183()
	{
		logic_uScriptCon_CompareBool_Bool_183 = local_WaitingOnPrompt_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.In(logic_uScriptCon_CompareBool_Bool_183);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.False;
		if (num)
		{
			Relay_In_185();
		}
		if (flag)
		{
			Relay_In_217();
		}
	}

	private void Relay_In_185()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_185.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_185.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_True_187()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_187.True(out logic_uScriptAct_SetBool_Target_187);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_187;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_187.Out)
		{
			Relay_False_193();
		}
	}

	private void Relay_False_187()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_187.False(out logic_uScriptAct_SetBool_Target_187);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_187;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_187.Out)
		{
			Relay_False_193();
		}
	}

	private void Relay_True_189()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_189.True(out logic_uScriptAct_SetBool_Target_189);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_189;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_189.Out)
		{
			Relay_False_187();
		}
	}

	private void Relay_False_189()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_189.False(out logic_uScriptAct_SetBool_Target_189);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_189;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_189.Out)
		{
			Relay_False_187();
		}
	}

	private void Relay_True_190()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.True(out logic_uScriptAct_SetBool_Target_190);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_190;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_190.Out)
		{
			Relay_False_293();
		}
	}

	private void Relay_False_190()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.False(out logic_uScriptAct_SetBool_Target_190);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_190;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_190.Out)
		{
			Relay_False_293();
		}
	}

	private void Relay_True_193()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_193.True(out logic_uScriptAct_SetBool_Target_193);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_193;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_193.Out)
		{
			Relay_False_190();
		}
	}

	private void Relay_False_193()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_193.False(out logic_uScriptAct_SetBool_Target_193);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_193;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_193.Out)
		{
			Relay_False_190();
		}
	}

	private void Relay_Pause_195()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_195.Pause();
	}

	private void Relay_UnPause_195()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_195.UnPause();
	}

	private void Relay_Pause_196()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_196.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_196.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_UnPause_196()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_196.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_196.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_OnSuccess_199()
	{
		local_EndReason_CheckpointChallenge_EndReason = event_UnityEngine_GameObject_EndReason_199;
	}

	private void Relay_OnFail_199()
	{
		local_EndReason_CheckpointChallenge_EndReason = event_UnityEngine_GameObject_EndReason_199;
	}

	private void Relay_In_201()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_201.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_201.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_True_203()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_203.True(out logic_uScriptAct_SetBool_Target_203);
		local_RaceAttempted_System_Boolean = logic_uScriptAct_SetBool_Target_203;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_203.Out)
		{
			Relay_True_83();
		}
	}

	private void Relay_False_203()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_203.False(out logic_uScriptAct_SetBool_Target_203);
		local_RaceAttempted_System_Boolean = logic_uScriptAct_SetBool_Target_203;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_203.Out)
		{
			Relay_True_83();
		}
	}

	private void Relay_In_205()
	{
		logic_uScriptCon_CompareBool_Bool_205 = local_RaceAttempted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.In(logic_uScriptCon_CompareBool_Bool_205);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.False;
		if (num)
		{
			Relay_In_206();
		}
		if (flag)
		{
			Relay_In_210();
		}
	}

	private void Relay_In_206()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_206.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_206.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_Save_Out_207()
	{
	}

	private void Relay_Load_Out_207()
	{
		Relay_False_53();
		Relay_In_56();
	}

	private void Relay_Restart_Out_207()
	{
		Relay_False_53();
	}

	private void Relay_Save_207()
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_207 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Save(ref logic_SubGraph_SaveLoadBool_boolean_207, logic_SubGraph_SaveLoadBool_boolAsVariable_207, logic_SubGraph_SaveLoadBool_uniqueID_207);
	}

	private void Relay_Load_207()
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_207 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Load(ref logic_SubGraph_SaveLoadBool_boolean_207, logic_SubGraph_SaveLoadBool_boolAsVariable_207, logic_SubGraph_SaveLoadBool_uniqueID_207);
	}

	private void Relay_Set_True_207()
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_207 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_207, logic_SubGraph_SaveLoadBool_boolAsVariable_207, logic_SubGraph_SaveLoadBool_uniqueID_207);
	}

	private void Relay_Set_False_207()
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_207 = local_RaceAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_207, logic_SubGraph_SaveLoadBool_boolAsVariable_207, logic_SubGraph_SaveLoadBool_uniqueID_207);
	}

	private void Relay_In_210()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_210 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_210 = distNearNPC;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_210.In(logic_uScript_IsPlayerInRangeOfTech_tech_210, logic_uScript_IsPlayerInRangeOfTech_range_210, logic_uScript_IsPlayerInRangeOfTech_techs_210);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_210.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_210.OutOfRange;
		if (inRange)
		{
			Relay_In_212();
		}
		if (outOfRange)
		{
			Relay_In_224();
		}
	}

	private void Relay_Out_212()
	{
		Relay_In_50();
	}

	private void Relay_Shown_212()
	{
	}

	private void Relay_In_212()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_212 = msgVehicleControls;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_212 = msgVehicleControls_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_212 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_212.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_212, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_212, logic_SubGraph_AddMessageWithPadSupport_speaker_212);
	}

	private void Relay_Out_217()
	{
		Relay_In_67();
	}

	private void Relay_Shown_217()
	{
	}

	private void Relay_In_217()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_217 = msgPurchaseVehicle;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_217 = msgPurchaseVehicle_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_217 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_217.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_217, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_217, logic_SubGraph_AddMessageWithPadSupport_speaker_217);
	}

	private void Relay_In_224()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_224 = messageTagControls;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_224.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_224, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_224);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_224.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_226()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_226 = messageTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_226.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_226, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_226);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_226.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_In_228()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_228 = messageTagControls;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_228.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_228, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_228);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_228.Out)
		{
			Relay_In_232();
		}
	}

	private void Relay_In_231()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_231 = messageTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_231.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_231, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_231);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_231.Out)
		{
			Relay_In_245();
		}
	}

	private void Relay_In_232()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_232 = messageTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_232.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_232, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_232);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_232.Out)
		{
			Relay_True_203();
		}
	}

	private void Relay_In_234()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_234.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_234, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_234, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_234 = owner_Connection_235;
		logic_uScript_GetAndCheckTechs_Return_234 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_234.In(logic_uScript_GetAndCheckTechs_techData_234, logic_uScript_GetAndCheckTechs_ownerNode_234, ref logic_uScript_GetAndCheckTechs_techs_234);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_234.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_234.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_234.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_234.WaitingToSpawn;
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
			Relay_In_237();
		}
		if (waitingToSpawn)
		{
			Relay_In_237();
		}
	}

	private void Relay_In_237()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_237.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_237.Out)
		{
			Relay_In_238();
		}
	}

	private void Relay_In_238()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_238.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_238.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_ResponseEvent_241()
	{
		local_242_TankBlock = event_UnityEngine_GameObject_TankBlock_241;
		local_240_System_Boolean = event_UnityEngine_GameObject_Accepted_241;
		Relay_In_243();
	}

	private void Relay_In_243()
	{
		logic_uScript_CompareBlock_A_243 = local_242_TankBlock;
		logic_uScript_CompareBlock_B_243 = local_TerminalBlock_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_243.In(logic_uScript_CompareBlock_A_243, logic_uScript_CompareBlock_B_243);
		if (logic_uScript_CompareBlock_uScript_CompareBlock_243.EqualTo)
		{
			Relay_In_231();
		}
	}

	private void Relay_In_245()
	{
		logic_uScriptCon_CompareBool_Bool_245 = local_240_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245.In(logic_uScriptCon_CompareBool_Bool_245);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245.True)
		{
			Relay_In_277();
		}
	}

	private void Relay_In_247()
	{
		logic_uScriptCon_CompareBool_Bool_247 = _DEBUGIgnoreMoneyCheck;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_247.In(logic_uScriptCon_CompareBool_Bool_247);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_247.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_247.False;
		if (num)
		{
			Relay_In_253();
		}
		if (flag)
		{
			Relay_In_250();
		}
	}

	private void Relay_In_248()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_248 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_248 = msgPromptNoMoney;
		logic_uScript_MissionPromptBlock_Show_targetBlock_248 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_248.In(logic_uScript_MissionPromptBlock_Show_bodyText_248, logic_uScript_MissionPromptBlock_Show_acceptButtonText_248, logic_uScript_MissionPromptBlock_Show_rejectButtonText_248, logic_uScript_MissionPromptBlock_Show_targetBlock_248, logic_uScript_MissionPromptBlock_Show_highlightBlock_248, logic_uScript_MissionPromptBlock_Show_singleUse_248);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_248.Out)
		{
			Relay_False_294();
		}
	}

	private void Relay_In_250()
	{
		logic_uScript_GetCurrentMoneyEarned_Return_250 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_250.In();
		local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_250;
		if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_250.Out)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_251()
	{
		logic_uScriptCon_CompareInt_A_251 = local_CurrentMoney_System_Int32;
		logic_uScriptCon_CompareInt_B_251 = vehicleCost;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_251.In(logic_uScriptCon_CompareInt_A_251, logic_uScriptCon_CompareInt_B_251);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_251.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_251.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_256();
		}
		if (lessThan)
		{
			Relay_In_248();
		}
	}

	private void Relay_In_253()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_253.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_253.Out)
		{
			Relay_In_256();
		}
	}

	private void Relay_In_256()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_256 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_256 = msgPromptAccept;
		logic_uScript_MissionPromptBlock_Show_rejectButtonText_256 = msgPromptDecline;
		logic_uScript_MissionPromptBlock_Show_targetBlock_256 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_256.In(logic_uScript_MissionPromptBlock_Show_bodyText_256, logic_uScript_MissionPromptBlock_Show_acceptButtonText_256, logic_uScript_MissionPromptBlock_Show_rejectButtonText_256, logic_uScript_MissionPromptBlock_Show_targetBlock_256, logic_uScript_MissionPromptBlock_Show_highlightBlock_256, logic_uScript_MissionPromptBlock_Show_singleUse_256);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_256.Out)
		{
			Relay_True_294();
		}
	}

	private void Relay_In_262()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_262 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_262.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_262);
		if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_262.Out)
		{
			Relay_In_185();
		}
	}

	private void Relay_In_264()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_264 = raceStartPosition;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_264.In(logic_uScript_SetEncounterTargetPosition_positionName_264);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_264.Out)
		{
			Relay_In_226();
		}
	}

	private void Relay_In_266()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_266.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_266, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_266, num, array.Length);
		num += array.Length;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_266.In(logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_266, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_266);
		bool num2 = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_266.True;
		bool flag = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_266.False;
		if (num2)
		{
			Relay_False_268();
		}
		if (flag)
		{
			Relay_True_274();
		}
	}

	private void Relay_True_268()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_268.True(out logic_uScriptAct_SetBool_Target_268);
		BlockLimitCritical = logic_uScriptAct_SetBool_Target_268;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_268.Out)
		{
			Relay_In_247();
		}
	}

	private void Relay_False_268()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_268.False(out logic_uScriptAct_SetBool_Target_268);
		BlockLimitCritical = logic_uScriptAct_SetBool_Target_268;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_268.Out)
		{
			Relay_In_247();
		}
	}

	private void Relay_In_271()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_271 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_271 = msgWorldCapacityFull;
		logic_uScript_MissionPromptBlock_Show_targetBlock_271 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_271.In(logic_uScript_MissionPromptBlock_Show_bodyText_271, logic_uScript_MissionPromptBlock_Show_acceptButtonText_271, logic_uScript_MissionPromptBlock_Show_rejectButtonText_271, logic_uScript_MissionPromptBlock_Show_targetBlock_271, logic_uScript_MissionPromptBlock_Show_highlightBlock_271, logic_uScript_MissionPromptBlock_Show_singleUse_271);
	}

	private void Relay_True_274()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_274.True(out logic_uScriptAct_SetBool_Target_274);
		BlockLimitCritical = logic_uScriptAct_SetBool_Target_274;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_274.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_False_274()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_274.False(out logic_uScriptAct_SetBool_Target_274);
		BlockLimitCritical = logic_uScriptAct_SetBool_Target_274;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_274.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_277()
	{
		logic_uScriptCon_CompareBool_Bool_277 = BlockLimitCritical;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_277.In(logic_uScriptCon_CompareBool_Bool_277);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_277.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_277.False;
		if (num)
		{
			Relay_In_278();
		}
		if (flag)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_278()
	{
		logic_uScript_AddMessage_messageData_278 = msgWorldCapacityFullReply;
		logic_uScript_AddMessage_speaker_278 = messageSpeaker;
		logic_uScript_AddMessage_Return_278 = logic_uScript_AddMessage_uScript_AddMessage_278.In(logic_uScript_AddMessage_messageData_278, logic_uScript_AddMessage_speaker_278);
	}

	private void Relay_In_282()
	{
		logic_uScript_ClearSceneryAlongSpline_splineStartTrans_282 = local_StartTransform_UnityEngine_Transform;
		logic_uScript_ClearSceneryAlongSpline_spline_282 = local_Spline_TrackSpline;
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_282.In(logic_uScript_ClearSceneryAlongSpline_splineStartTrans_282, logic_uScript_ClearSceneryAlongSpline_spline_282, logic_uScript_ClearSceneryAlongSpline_delayBetweenAreaClears_282, logic_uScript_ClearSceneryAlongSpline_sceneryClearSFXPrefab_282, logic_uScript_ClearSceneryAlongSpline_stepSizeWidthPercentage_282, logic_uScript_ClearSceneryAlongSpline_clearUpToPenaltyWidth_282);
		if (logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_282.Out)
		{
			Relay_In_234();
		}
	}

	private void Relay_In_285()
	{
		logic_uScript_ClearSceneryAlongSpline_splineStartTrans_285 = local_StartTransform_UnityEngine_Transform;
		logic_uScript_ClearSceneryAlongSpline_spline_285 = local_Spline_TrackSpline;
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_285.In(logic_uScript_ClearSceneryAlongSpline_splineStartTrans_285, logic_uScript_ClearSceneryAlongSpline_spline_285, logic_uScript_ClearSceneryAlongSpline_delayBetweenAreaClears_285, logic_uScript_ClearSceneryAlongSpline_sceneryClearSFXPrefab_285, logic_uScript_ClearSceneryAlongSpline_stepSizeWidthPercentage_285, logic_uScript_ClearSceneryAlongSpline_clearUpToPenaltyWidth_285);
	}

	private void Relay_True_287()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_287.True(out logic_uScriptAct_SetBool_Target_287);
		local_BeenInRangeOfEncounter_System_Boolean = logic_uScriptAct_SetBool_Target_287;
	}

	private void Relay_False_287()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_287.False(out logic_uScriptAct_SetBool_Target_287);
		local_BeenInRangeOfEncounter_System_Boolean = logic_uScriptAct_SetBool_Target_287;
	}

	private void Relay_In_288()
	{
		logic_uScriptCon_CompareBool_Bool_288 = local_BeenInRangeOfEncounter_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_288.In(logic_uScriptCon_CompareBool_Bool_288);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_288.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_288.False;
		if (num)
		{
			Relay_In_65();
		}
		if (flag)
		{
			Relay_In_290();
		}
	}

	private void Relay_In_290()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_290 = owner_Connection_291;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_290.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_290);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_290.True)
		{
			Relay_True_287();
		}
	}

	private void Relay_True_293()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.True(out logic_uScriptAct_SetBool_Target_293);
		local_BeenInRangeOfEncounter_System_Boolean = logic_uScriptAct_SetBool_Target_293;
	}

	private void Relay_False_293()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.False(out logic_uScriptAct_SetBool_Target_293);
		local_BeenInRangeOfEncounter_System_Boolean = logic_uScriptAct_SetBool_Target_293;
	}

	private void Relay_True_294()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_294.True(out logic_uScriptAct_SetBool_Target_294);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_294;
	}

	private void Relay_False_294()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_294.False(out logic_uScriptAct_SetBool_Target_294);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_294;
	}
}
