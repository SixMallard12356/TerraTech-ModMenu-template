using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_LearnToFly : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(1)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius;

	public float distAtWhichNPCInRange;

	public float distAtWhichTechCloseRange;

	public float distAtWhichTechFound;

	[Multiline(1)]
	public string EncounterCentrePosName = "";

	private BlockTypes[] local_116_BlockTypesArray = new BlockTypes[8]
	{
		BlockTypes.VENWingExpanderRight_413,
		BlockTypes.VENWingExpanderLeft_413,
		BlockTypes.VENLandingGear_111,
		BlockTypes.VENNoseProp_331,
		BlockTypes.VENWheel_111,
		BlockTypes.VENBracketUShape_111,
		BlockTypes.VENRadar_111,
		BlockTypes.VENWingTail_122
	};

	private Tank[] local_46_TankArray = new Tank[0];

	private bool local_ChallengeInProgress_System_Boolean;

	private CheckpointChallenge.EndReason local_EndReason_CheckpointChallenge_EndReason;

	private string local_MsgTagAccessMenu_System_String = "MsgTagAccessMenu";

	private string local_MsgTagControlTechComplete_System_String = "MsgTagControlTechComplete";

	private string local_MsgTagStuntStarted_System_String = "MsgTagStuntStarted";

	private Tank local_NPCTech_Tank;

	private bool local_OutOfBounds_System_Boolean;

	private int local_PassedCheckpointIdx_System_Int32;

	private bool local_RampSetup_System_Boolean;

	private bool local_SpawnedRamp_System_Boolean;

	private TrackSpline local_Spline_TrackSpline;

	private int local_Stage_System_Int32 = 1;

	private GameObject local_StartingRamp_UnityEngine_GameObject;

	private GameObject local_StartingRamp_UnityEngine_GameObject_previous;

	private Transform local_StartTransform_UnityEngine_Transform;

	private Tank local_Tech_Tank;

	private Tank[] local_Techs_TankArray = new Tank[0];

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public uScript_AddMessage.MessageData msg01FindTech;

	public uScript_AddMessage.MessageData msg02TechFound;

	public uScript_AddMessage.MessageData msg03AccessMenu;

	public uScript_AddMessage.MessageData msg03AccessMenu_Pad;

	public uScript_AddMessage.MessageData msg04ControlTechComplete;

	public uScript_AddMessage.MessageData msg04ControlTechComplete_Pad;

	public uScript_AddMessage.MessageData msgOutOfBounds;

	public uScript_AddMessage.MessageData msgQuitFromMenu;

	public uScript_AddMessage.MessageData msgStuntComplete;

	public uScript_AddMessage.MessageData msgStuntStarted;

	public uScript_AddMessage.MessageData msgTouchedGround;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	[Multiline(1)]
	public string Spawned_Ramp_Name = "";

	public TerrainObject StuntRampPrefab;

	[Multiline(1)]
	public string targetChallengeID = "";

	public SpawnTechData[] techData = new SpawnTechData[0];

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_16;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_24;

	private GameObject owner_Connection_36;

	private GameObject owner_Connection_40;

	private GameObject owner_Connection_48;

	private GameObject owner_Connection_51;

	private GameObject owner_Connection_68;

	private GameObject owner_Connection_73;

	private GameObject owner_Connection_75;

	private GameObject owner_Connection_77;

	private GameObject owner_Connection_80;

	private GameObject owner_Connection_89;

	private GameObject owner_Connection_91;

	private GameObject owner_Connection_167;

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

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_26;

	private bool logic_uScriptCon_CompareBool_True_26 = true;

	private bool logic_uScriptCon_CompareBool_False_26 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_30 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_30;

	private bool logic_uScriptAct_SetBool_Out_30 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_30 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_30 = true;

	private uScript_ClearSceneryAlongSpline logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_31 = new uScript_ClearSceneryAlongSpline();

	private Transform logic_uScript_ClearSceneryAlongSpline_splineStartTrans_31;

	private TrackSpline logic_uScript_ClearSceneryAlongSpline_spline_31;

	private float logic_uScript_ClearSceneryAlongSpline_delayBetweenAreaClears_31;

	private Transform logic_uScript_ClearSceneryAlongSpline_sceneryClearSFXPrefab_31;

	private float logic_uScript_ClearSceneryAlongSpline_stepSizeWidthPercentage_31 = 0.8f;

	private bool logic_uScript_ClearSceneryAlongSpline_clearUpToPenaltyWidth_31;

	private bool logic_uScript_ClearSceneryAlongSpline_Out_31 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_BusyClearing_31 = true;

	private bool logic_uScript_ClearSceneryAlongSpline_DoneClearing_31 = true;

	private uScript_GetEncounterSpline logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_33 = new uScript_GetEncounterSpline();

	private GameObject logic_uScript_GetEncounterSpline_owner_33;

	private TrackSpline logic_uScript_GetEncounterSpline_Return_33;

	private bool logic_uScript_GetEncounterSpline_Out_33 = true;

	private uScript_InitChallengeStarterWithEncounterChallengeData logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_34 = new uScript_InitChallengeStarterWithEncounterChallengeData();

	private GameObject logic_uScript_InitChallengeStarterWithEncounterChallengeData_owner_34;

	private GameObject logic_uScript_InitChallengeStarterWithEncounterChallengeData_targetChallengeStarterObject_34;

	private bool logic_uScript_InitChallengeStarterWithEncounterChallengeData_Out_34 = true;

	private uScript_GetChallengeStartTransform logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_35 = new uScript_GetChallengeStartTransform();

	private GameObject logic_uScript_GetChallengeStartTransform_challengeStarterObject_35;

	private Transform logic_uScript_GetChallengeStartTransform_Return_35;

	private bool logic_uScript_GetChallengeStartTransform_Out_35 = true;

	private uScript_GetChallengeIDFromChallengeStarter logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_44 = new uScript_GetChallengeIDFromChallengeStarter();

	private GameObject logic_uScript_GetChallengeIDFromChallengeStarter_challengeStarterObject_44;

	private string logic_uScript_GetChallengeIDFromChallengeStarter_Return_44;

	private bool logic_uScript_GetChallengeIDFromChallengeStarter_Out_44 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_47 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_47 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_47;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_47 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_47;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_47 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_47 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_47 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_47 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_50 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_50 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_50;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_50;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_50;

	private bool logic_uScript_SpawnTechsFromData_Out_50 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_53 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_53 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_53;

	private bool logic_uScript_SetTankInvulnerable_Out_53 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_54 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_54 = new Tank[0];

	private int logic_uScript_AccessListTech_index_54;

	private Tank logic_uScript_AccessListTech_value_54;

	private bool logic_uScript_AccessListTech_Out_54 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_57;

	private bool logic_uScriptCon_CompareBool_True_57 = true;

	private bool logic_uScriptCon_CompareBool_False_57 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_59 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_59;

	private bool logic_uScriptAct_SetBool_Out_59 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_59 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_59 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_60 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_60;

	private bool logic_uScriptAct_SetBool_Out_60 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_60 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_60 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_62 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_62;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_62 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_62 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_63 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_63;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_67 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_67;

	private float logic_uScript_IsPlayerInRangeOfTech_range_67;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_67 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_67 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_67 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_67 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_69 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_69;

	private object logic_uScript_SetEncounterTarget_visibleObject_69 = "";

	private bool logic_uScript_SetEncounterTarget_Out_69 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_72 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_72;

	private bool logic_uScript_ClearEncounterTarget_Out_72 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_74 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_74;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_74 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_76 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_76;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_76 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_76 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_76 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_78 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_78 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_82;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_85 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_85;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_85;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_85;

	private bool logic_uScript_AddMessage_Out_85 = true;

	private bool logic_uScript_AddMessage_Shown_85 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_87 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_87 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_87;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_87 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_87;

	private bool logic_uScript_SpawnTechsFromData_Out_87 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_94 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_94;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_94 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_94 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_96 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_96 = new Tank[0];

	private int logic_uScript_AccessListTech_index_96;

	private Tank logic_uScript_AccessListTech_value_96;

	private bool logic_uScript_AccessListTech_Out_96 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_97 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_97 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_97;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_97 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_97;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_97 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_97 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_97 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_97 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_98 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_98;

	private int logic_uScript_SetTankTeam_team_98;

	private bool logic_uScript_SetTankTeam_Out_98 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_100 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_100;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_100;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_100;

	private bool logic_uScript_AddMessage_Out_100 = true;

	private bool logic_uScript_AddMessage_Shown_100 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_104 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_104;

	private float logic_uScript_IsPlayerInRangeOfTech_range_104;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_104 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_104 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_104 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_104 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_106 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_106;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_106;

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_109 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_109;

	private bool logic_uScript_IsTechPlayer_Out_109 = true;

	private bool logic_uScript_IsTechPlayer_True_109 = true;

	private bool logic_uScript_IsTechPlayer_False_109 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_111;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_111;

	private uScript_DiscoverBlocks logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_113 = new uScript_DiscoverBlocks();

	private BlockTypes[] logic_uScript_DiscoverBlocks_blockTypes_113 = new BlockTypes[0];

	private bool logic_uScript_DiscoverBlocks_Out_113 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_117 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_117;

	private float logic_uScript_IsPlayerInRangeOfTech_range_117;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_117 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_117 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_117 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_117 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_118 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_118;

	private float logic_uScript_IsPlayerInRangeOfTech_range_118;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_118 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_118 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_118 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_118 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_121 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_121 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_121 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_121 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_124;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_124;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_124;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_124;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_124;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_128;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_128;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_128;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_128;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_128;

	private uScript_GetChallengeStateFromChallengeID logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_131 = new uScript_GetChallengeStateFromChallengeID();

	private string logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_131 = "";

	private bool logic_uScript_GetChallengeStateFromChallengeID_Out_131 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_NotRunning_131 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_JustStarted_131 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_InProgress_131 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_JustEnded_131 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_132 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_132;

	private bool logic_uScriptAct_SetBool_Out_132 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_132 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_132 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_135 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_135 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_135 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_135 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_137 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_137;

	private bool logic_uScriptAct_SetBool_Out_137 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_137 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_137 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_138 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_138;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_138;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_138;

	private bool logic_uScript_AddMessage_Out_138 = true;

	private bool logic_uScript_AddMessage_Shown_138 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_141 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_141;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_141;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_141;

	private bool logic_uScript_AddMessage_Out_141 = true;

	private bool logic_uScript_AddMessage_Shown_141 = true;

	private uScript_GetLastChallengeResult logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_142 = new uScript_GetLastChallengeResult();

	private bool logic_uScript_GetLastChallengeResult_Success_142 = true;

	private bool logic_uScript_GetLastChallengeResult_Failure_142 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_145 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_145;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_145;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_145;

	private bool logic_uScript_AddMessage_Out_145 = true;

	private bool logic_uScript_AddMessage_Shown_145 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_146 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_146 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_146 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_146 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_147 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_147;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_147 = CheckpointChallenge.EndReason.FailedQuit;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_147 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_147 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_156 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_156;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_156 = CheckpointChallenge.EndReason.FailedOutOfBounds;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_156 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_156 = true;

	private uScript_ClearChallengeStarterChallengeData logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_157 = new uScript_ClearChallengeStarterChallengeData();

	private GameObject logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_157;

	private bool logic_uScript_ClearChallengeStarterChallengeData_Out_157 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_158 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_158;

	private bool logic_uScript_FinishEncounter_Out_158 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_159 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_159;

	private bool logic_uScriptAct_SetBool_Out_159 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_159 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_159 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_160 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_160;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_160;

	private bool logic_uScript_LockTechSendToSCU_Out_160 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_163 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_163;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_163;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_163;

	private bool logic_uScript_AddMessage_Out_163 = true;

	private bool logic_uScript_AddMessage_Shown_163 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_164 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_164;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_164;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_164;

	private bool logic_uScript_AddMessage_Out_164 = true;

	private bool logic_uScript_AddMessage_Shown_164 = true;

	private uScript_CompareCheckpointChallengeEndReason logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_168 = new uScript_CompareCheckpointChallengeEndReason();

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_result_168;

	private CheckpointChallenge.EndReason logic_uScript_CompareCheckpointChallengeEndReason_expected_168 = CheckpointChallenge.EndReason.FailedTouchedGround;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_EqualTo_168 = true;

	private bool logic_uScript_CompareCheckpointChallengeEndReason_NotEqualTo_168 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_170 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_170 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_172 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_172;

	private bool logic_uScript_RemoveTech_Out_172 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_177 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_177 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_178 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_178 = true;

	private uScript_GetChallengeStateFromChallengeID logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_180 = new uScript_GetChallengeStateFromChallengeID();

	private string logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_180 = "";

	private bool logic_uScript_GetChallengeStateFromChallengeID_Out_180 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_NotRunning_180 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_JustStarted_180 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_InProgress_180 = true;

	private bool logic_uScript_GetChallengeStateFromChallengeID_JustEnded_180 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_181 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_181;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_181;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_189 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_189 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_189 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_189 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_190 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_190;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_190;

	private bool logic_uScript_LockTech_Out_190 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_191 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_191 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_191;

	private bool logic_uScript_SetTankInvulnerable_Out_191 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_193 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_193;

	private Tank logic_uScript_SetTankInvulnerable_tank_193;

	private bool logic_uScript_SetTankInvulnerable_Out_193 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_195 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_195;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_195 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_195 = true;

	private int event_UnityEngine_GameObject_CheckpointIndex_25;

	private CheckpointChallenge.EndReason event_UnityEngine_GameObject_EndReason_79;

	private float event_UnityEngine_GameObject_EndTime_79;

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
		if (null == owner_Connection_36 || !m_RegisteredForEvents)
		{
			owner_Connection_36 = parentGameObject;
		}
		if (null == owner_Connection_40 || !m_RegisteredForEvents)
		{
			owner_Connection_40 = parentGameObject;
		}
		if (null == owner_Connection_48 || !m_RegisteredForEvents)
		{
			owner_Connection_48 = parentGameObject;
		}
		if (null == owner_Connection_51 || !m_RegisteredForEvents)
		{
			owner_Connection_51 = parentGameObject;
		}
		if (null == owner_Connection_68 || !m_RegisteredForEvents)
		{
			owner_Connection_68 = parentGameObject;
		}
		if (null == owner_Connection_73 || !m_RegisteredForEvents)
		{
			owner_Connection_73 = parentGameObject;
		}
		if (null == owner_Connection_75 || !m_RegisteredForEvents)
		{
			owner_Connection_75 = parentGameObject;
		}
		if (null == owner_Connection_77 || !m_RegisteredForEvents)
		{
			owner_Connection_77 = parentGameObject;
		}
		if (null == owner_Connection_80 || !m_RegisteredForEvents)
		{
			owner_Connection_80 = parentGameObject;
			if (null != owner_Connection_80)
			{
				uScript_CheckPointChallengeEndedEvent uScript_CheckPointChallengeEndedEvent2 = owner_Connection_80.GetComponent<uScript_CheckPointChallengeEndedEvent>();
				if (null == uScript_CheckPointChallengeEndedEvent2)
				{
					uScript_CheckPointChallengeEndedEvent2 = owner_Connection_80.AddComponent<uScript_CheckPointChallengeEndedEvent>();
				}
				if (null != uScript_CheckPointChallengeEndedEvent2)
				{
					uScript_CheckPointChallengeEndedEvent2.OnSuccess += Instance_OnSuccess_79;
					uScript_CheckPointChallengeEndedEvent2.OnFail += Instance_OnFail_79;
				}
			}
		}
		if (null == owner_Connection_89 || !m_RegisteredForEvents)
		{
			owner_Connection_89 = parentGameObject;
		}
		if (null == owner_Connection_91 || !m_RegisteredForEvents)
		{
			owner_Connection_91 = parentGameObject;
		}
		if (null == owner_Connection_167 || !m_RegisteredForEvents)
		{
			owner_Connection_167 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_80)
		{
			uScript_CheckPointChallengeEndedEvent uScript_CheckPointChallengeEndedEvent2 = owner_Connection_80.GetComponent<uScript_CheckPointChallengeEndedEvent>();
			if (null == uScript_CheckPointChallengeEndedEvent2)
			{
				uScript_CheckPointChallengeEndedEvent2 = owner_Connection_80.AddComponent<uScript_CheckPointChallengeEndedEvent>();
			}
			if (null != uScript_CheckPointChallengeEndedEvent2)
			{
				uScript_CheckPointChallengeEndedEvent2.OnSuccess += Instance_OnSuccess_79;
				uScript_CheckPointChallengeEndedEvent2.OnFail += Instance_OnFail_79;
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
		if (null != owner_Connection_80)
		{
			uScript_CheckPointChallengeEndedEvent component5 = owner_Connection_80.GetComponent<uScript_CheckPointChallengeEndedEvent>();
			if (null != component5)
			{
				component5.OnSuccess -= Instance_OnSuccess_79;
				component5.OnFail -= Instance_OnFail_79;
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
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_30.SetParent(g);
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_31.SetParent(g);
		logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_33.SetParent(g);
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_34.SetParent(g);
		logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_35.SetParent(g);
		logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_44.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_47.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_50.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_53.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_54.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_59.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_63.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_67.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_69.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_72.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_74.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_76.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_78.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_85.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_87.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_94.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_96.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_97.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_98.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_100.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_104.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_106.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_109.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.SetParent(g);
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_113.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_117.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_118.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_121.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128.SetParent(g);
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_131.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_135.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_137.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_138.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_141.SetParent(g);
		logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_142.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_145.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_146.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_147.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_156.SetParent(g);
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_157.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_158.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_160.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_163.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_164.SetParent(g);
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_168.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_170.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_172.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_177.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_178.SetParent(g);
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_180.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_181.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_189.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_190.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_191.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_193.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_195.SetParent(g);
		owner_Connection_3 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_16 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_24 = parentGameObject;
		owner_Connection_36 = parentGameObject;
		owner_Connection_40 = parentGameObject;
		owner_Connection_48 = parentGameObject;
		owner_Connection_51 = parentGameObject;
		owner_Connection_68 = parentGameObject;
		owner_Connection_73 = parentGameObject;
		owner_Connection_75 = parentGameObject;
		owner_Connection_77 = parentGameObject;
		owner_Connection_80 = parentGameObject;
		owner_Connection_89 = parentGameObject;
		owner_Connection_91 = parentGameObject;
		owner_Connection_167 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_63.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_106.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_181.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Save_Out += SubGraph_SaveLoadInt_Save_Out_62;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Load_Out += SubGraph_SaveLoadInt_Load_Out_62;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_62;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_63.Out += SubGraph_LoadObjectiveStates_Out_63;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output1 += uScriptCon_ManualSwitch_Output1_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output2 += uScriptCon_ManualSwitch_Output2_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output3 += uScriptCon_ManualSwitch_Output3_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output4 += uScriptCon_ManualSwitch_Output4_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output5 += uScriptCon_ManualSwitch_Output5_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output6 += uScriptCon_ManualSwitch_Output6_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output7 += uScriptCon_ManualSwitch_Output7_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output8 += uScriptCon_ManualSwitch_Output8_82;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_106.Out += SubGraph_CompleteObjectiveStage_Out_106;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Out += SubGraph_CompleteObjectiveStage_Out_111;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124.Out += SubGraph_AddMessageWithPadSupport_Out_124;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124.Shown += SubGraph_AddMessageWithPadSupport_Shown_124;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128.Out += SubGraph_AddMessageWithPadSupport_Out_128;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128.Shown += SubGraph_AddMessageWithPadSupport_Shown_128;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_181.Out += SubGraph_CompleteObjectiveStage_Out_181;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_63.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_106.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_181.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_63.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_74.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_106.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_181.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDisable();
		logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_9.OnDisable();
		logic_uScript_SpawnTerrainObject_uScript_SpawnTerrainObject_10.OnDisable();
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_31.OnDisable();
		logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_33.OnDisable();
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_34.OnDisable();
		logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_35.OnDisable();
		logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_44.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_53.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_63.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_67.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_76.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_85.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_100.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_104.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_106.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_117.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_118.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128.OnDisable();
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_131.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_138.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_141.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_145.OnDisable();
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_157.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_163.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_164.OnDisable();
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_180.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_181.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_191.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_193.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_63.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_106.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_181.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_63.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_106.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_181.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Save_Out -= SubGraph_SaveLoadInt_Save_Out_62;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Load_Out -= SubGraph_SaveLoadInt_Load_Out_62;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_62;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_63.Out -= SubGraph_LoadObjectiveStates_Out_63;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output1 -= uScriptCon_ManualSwitch_Output1_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output2 -= uScriptCon_ManualSwitch_Output2_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output3 -= uScriptCon_ManualSwitch_Output3_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output4 -= uScriptCon_ManualSwitch_Output4_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output5 -= uScriptCon_ManualSwitch_Output5_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output6 -= uScriptCon_ManualSwitch_Output6_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output7 -= uScriptCon_ManualSwitch_Output7_82;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.Output8 -= uScriptCon_ManualSwitch_Output8_82;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_106.Out -= SubGraph_CompleteObjectiveStage_Out_106;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Out -= SubGraph_CompleteObjectiveStage_Out_111;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124.Out -= SubGraph_AddMessageWithPadSupport_Out_124;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124.Shown -= SubGraph_AddMessageWithPadSupport_Shown_124;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128.Out -= SubGraph_AddMessageWithPadSupport_Out_128;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128.Shown -= SubGraph_AddMessageWithPadSupport_Shown_128;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_181.Out -= SubGraph_CompleteObjectiveStage_Out_181;
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

	private void Instance_OnSuccess_79(object o, uScript_CheckPointChallengeEndedEvent.CheckpointChallengeEndedEventArgs e)
	{
		event_UnityEngine_GameObject_EndReason_79 = e.EndReason;
		event_UnityEngine_GameObject_EndTime_79 = e.EndTime;
		Relay_OnSuccess_79();
	}

	private void Instance_OnFail_79(object o, uScript_CheckPointChallengeEndedEvent.CheckpointChallengeEndedEventArgs e)
	{
		event_UnityEngine_GameObject_EndReason_79 = e.EndReason;
		event_UnityEngine_GameObject_EndTime_79 = e.EndTime;
		Relay_OnFail_79();
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

	private void SubGraph_SaveLoadInt_Save_Out_62(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_62 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_62;
		Relay_Save_Out_62();
	}

	private void SubGraph_SaveLoadInt_Load_Out_62(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_62 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_62;
		Relay_Load_Out_62();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_62(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_62 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_62;
		Relay_Restart_Out_62();
	}

	private void SubGraph_LoadObjectiveStates_Out_63(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_63();
	}

	private void uScriptCon_ManualSwitch_Output1_82(object o, EventArgs e)
	{
		Relay_Output1_82();
	}

	private void uScriptCon_ManualSwitch_Output2_82(object o, EventArgs e)
	{
		Relay_Output2_82();
	}

	private void uScriptCon_ManualSwitch_Output3_82(object o, EventArgs e)
	{
		Relay_Output3_82();
	}

	private void uScriptCon_ManualSwitch_Output4_82(object o, EventArgs e)
	{
		Relay_Output4_82();
	}

	private void uScriptCon_ManualSwitch_Output5_82(object o, EventArgs e)
	{
		Relay_Output5_82();
	}

	private void uScriptCon_ManualSwitch_Output6_82(object o, EventArgs e)
	{
		Relay_Output6_82();
	}

	private void uScriptCon_ManualSwitch_Output7_82(object o, EventArgs e)
	{
		Relay_Output7_82();
	}

	private void uScriptCon_ManualSwitch_Output8_82(object o, EventArgs e)
	{
		Relay_Output8_82();
	}

	private void SubGraph_CompleteObjectiveStage_Out_106(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_106 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_106;
		Relay_Out_106();
	}

	private void SubGraph_CompleteObjectiveStage_Out_111(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_111 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_111;
		Relay_Out_111();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_124(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_124 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_124 = e.messageControlPadReturn;
		Relay_Out_124();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_124(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_124 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_124 = e.messageControlPadReturn;
		Relay_Shown_124();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_128(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_128 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_128 = e.messageControlPadReturn;
		Relay_Out_128();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_128(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_128 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_128 = e.messageControlPadReturn;
		Relay_Shown_128();
	}

	private void SubGraph_CompleteObjectiveStage_Out_181(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_181 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_181;
		Relay_Out_181();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_74();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_SaveEvent_1()
	{
		Relay_Save_62();
	}

	private void Relay_LoadEvent_1()
	{
		Relay_Load_62();
	}

	private void Relay_RestartEvent_1()
	{
		Relay_Restart_62();
	}

	private void Relay_In_2()
	{
		logic_uScriptCon_CompareBool_Bool_2 = local_SpawnedRamp_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.In(logic_uScriptCon_CompareBool_Bool_2);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.False;
		if (num)
		{
			Relay_In_47();
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
	}

	private void Relay_Load_Out_7()
	{
		Relay_In_63();
		Relay_False_60();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_False_60();
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
		if (logic_uScript_GetSpawnedTerrainObject_uScript_GetSpawnedTerrainObject_9.CurrentlySpawned)
		{
			Relay_In_34();
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
			Relay_InitialSpawn_50();
		}
	}

	private void Relay_OnBoundsWarningCaution_23()
	{
	}

	private void Relay_OnBoundsWarningIllegal_23()
	{
		Relay_True_30();
	}

	private void Relay_OnCheckPointPassed_25()
	{
		local_PassedCheckpointIdx_System_Int32 = event_UnityEngine_GameObject_CheckpointIndex_25;
	}

	private void Relay_In_26()
	{
		logic_uScriptCon_CompareBool_Bool_26 = local_ChallengeInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.In(logic_uScriptCon_CompareBool_Bool_26);
	}

	private void Relay_True_30()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_30.True(out logic_uScriptAct_SetBool_Target_30);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_30;
	}

	private void Relay_False_30()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_30.False(out logic_uScriptAct_SetBool_Target_30);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_30;
	}

	private void Relay_In_31()
	{
		logic_uScript_ClearSceneryAlongSpline_splineStartTrans_31 = local_StartTransform_UnityEngine_Transform;
		logic_uScript_ClearSceneryAlongSpline_spline_31 = local_Spline_TrackSpline;
		logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_31.In(logic_uScript_ClearSceneryAlongSpline_splineStartTrans_31, logic_uScript_ClearSceneryAlongSpline_spline_31, logic_uScript_ClearSceneryAlongSpline_delayBetweenAreaClears_31, logic_uScript_ClearSceneryAlongSpline_sceneryClearSFXPrefab_31, logic_uScript_ClearSceneryAlongSpline_stepSizeWidthPercentage_31, logic_uScript_ClearSceneryAlongSpline_clearUpToPenaltyWidth_31);
		if (logic_uScript_ClearSceneryAlongSpline_uScript_ClearSceneryAlongSpline_31.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_In_33()
	{
		logic_uScript_GetEncounterSpline_owner_33 = owner_Connection_36;
		logic_uScript_GetEncounterSpline_Return_33 = logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_33.In(logic_uScript_GetEncounterSpline_owner_33);
		local_Spline_TrackSpline = logic_uScript_GetEncounterSpline_Return_33;
		if (logic_uScript_GetEncounterSpline_uScript_GetEncounterSpline_33.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_In_34()
	{
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_owner_34 = owner_Connection_40;
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_targetChallengeStarterObject_34 = local_StartingRamp_UnityEngine_GameObject;
		logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_34.In(logic_uScript_InitChallengeStarterWithEncounterChallengeData_owner_34, logic_uScript_InitChallengeStarterWithEncounterChallengeData_targetChallengeStarterObject_34);
		if (logic_uScript_InitChallengeStarterWithEncounterChallengeData_uScript_InitChallengeStarterWithEncounterChallengeData_34.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_35()
	{
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		logic_uScript_GetChallengeStartTransform_challengeStarterObject_35 = local_StartingRamp_UnityEngine_GameObject;
		logic_uScript_GetChallengeStartTransform_Return_35 = logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_35.In(logic_uScript_GetChallengeStartTransform_challengeStarterObject_35);
		local_StartTransform_UnityEngine_Transform = logic_uScript_GetChallengeStartTransform_Return_35;
		if (logic_uScript_GetChallengeStartTransform_uScript_GetChallengeStartTransform_35.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_In_44()
	{
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		logic_uScript_GetChallengeIDFromChallengeStarter_challengeStarterObject_44 = local_StartingRamp_UnityEngine_GameObject;
		logic_uScript_GetChallengeIDFromChallengeStarter_Return_44 = logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_44.In(logic_uScript_GetChallengeIDFromChallengeStarter_challengeStarterObject_44);
		targetChallengeID = logic_uScript_GetChallengeIDFromChallengeStarter_Return_44;
		if (logic_uScript_GetChallengeIDFromChallengeStarter_uScript_GetChallengeIDFromChallengeStarter_44.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_In_47()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_47.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_47, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_47, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_47 = owner_Connection_51;
		int num2 = 0;
		Array array = local_46_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_47.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_47, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_47, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_47 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_47.In(logic_uScript_GetAndCheckTechs_techData_47, logic_uScript_GetAndCheckTechs_ownerNode_47, ref logic_uScript_GetAndCheckTechs_techs_47);
		local_46_TankArray = logic_uScript_GetAndCheckTechs_techs_47;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_47.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_47.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_47.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_54();
		}
		if (someAlive)
		{
			Relay_AtIndex_54();
		}
		if (allDead)
		{
			Relay_In_177();
		}
	}

	private void Relay_InitialSpawn_50()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_50.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_50, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_50, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_50 = owner_Connection_48;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_50.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_50, logic_uScript_SpawnTechsFromData_ownerNode_50, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_50, logic_uScript_SpawnTechsFromData_allowResurrection_50);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_50.Out)
		{
			Relay_InitialSpawn_87();
		}
	}

	private void Relay_In_53()
	{
		logic_uScript_SetTankInvulnerable_tank_53 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_53.In(logic_uScript_SetTankInvulnerable_invulnerable_53, logic_uScript_SetTankInvulnerable_tank_53);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_53.Out)
		{
			Relay_In_97();
		}
	}

	private void Relay_AtIndex_54()
	{
		int num = 0;
		Array array = local_46_TankArray;
		if (logic_uScript_AccessListTech_techList_54.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_54, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_54, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_54.AtIndex(ref logic_uScript_AccessListTech_techList_54, logic_uScript_AccessListTech_index_54, out logic_uScript_AccessListTech_value_54);
		local_46_TankArray = logic_uScript_AccessListTech_techList_54;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_54;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_54.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_In_57()
	{
		logic_uScriptCon_CompareBool_Bool_57 = local_RampSetup_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.In(logic_uScriptCon_CompareBool_Bool_57);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.False;
		if (num)
		{
			Relay_In_67();
		}
		if (flag)
		{
			Relay_True_59();
		}
	}

	private void Relay_True_59()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_59.True(out logic_uScriptAct_SetBool_Target_59);
		local_RampSetup_System_Boolean = logic_uScriptAct_SetBool_Target_59;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_59.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_False_59()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_59.False(out logic_uScriptAct_SetBool_Target_59);
		local_RampSetup_System_Boolean = logic_uScriptAct_SetBool_Target_59;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_59.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_True_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.True(out logic_uScriptAct_SetBool_Target_60);
		local_RampSetup_System_Boolean = logic_uScriptAct_SetBool_Target_60;
	}

	private void Relay_False_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.False(out logic_uScriptAct_SetBool_Target_60);
		local_RampSetup_System_Boolean = logic_uScriptAct_SetBool_Target_60;
	}

	private void Relay_Save_Out_62()
	{
		Relay_Save_7();
	}

	private void Relay_Load_Out_62()
	{
		Relay_Load_7();
	}

	private void Relay_Restart_Out_62()
	{
		Relay_Set_False_7();
	}

	private void Relay_Save_62()
	{
		logic_SubGraph_SaveLoadInt_integer_62 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_62 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Save(logic_SubGraph_SaveLoadInt_restartValue_62, ref logic_SubGraph_SaveLoadInt_integer_62, logic_SubGraph_SaveLoadInt_intAsVariable_62, logic_SubGraph_SaveLoadInt_uniqueID_62);
	}

	private void Relay_Load_62()
	{
		logic_SubGraph_SaveLoadInt_integer_62 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_62 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Load(logic_SubGraph_SaveLoadInt_restartValue_62, ref logic_SubGraph_SaveLoadInt_integer_62, logic_SubGraph_SaveLoadInt_intAsVariable_62, logic_SubGraph_SaveLoadInt_uniqueID_62);
	}

	private void Relay_Restart_62()
	{
		logic_SubGraph_SaveLoadInt_integer_62 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_62 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Restart(logic_SubGraph_SaveLoadInt_restartValue_62, ref logic_SubGraph_SaveLoadInt_integer_62, logic_SubGraph_SaveLoadInt_intAsVariable_62, logic_SubGraph_SaveLoadInt_uniqueID_62);
	}

	private void Relay_Out_63()
	{
	}

	private void Relay_In_63()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_63 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_63.In(logic_SubGraph_LoadObjectiveStates_currentObjective_63);
	}

	private void Relay_In_67()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_67 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_67 = distAtWhichNPCInRange;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_67.In(logic_uScript_IsPlayerInRangeOfTech_tech_67, logic_uScript_IsPlayerInRangeOfTech_range_67, logic_uScript_IsPlayerInRangeOfTech_techs_67);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_67.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_67.OutOfRange;
		if (inRange)
		{
			Relay_In_69();
		}
		if (outOfRange)
		{
			Relay_In_72();
		}
	}

	private void Relay_In_69()
	{
		logic_uScript_SetEncounterTarget_owner_69 = owner_Connection_68;
		logic_uScript_SetEncounterTarget_visibleObject_69 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_69.In(logic_uScript_SetEncounterTarget_owner_69, logic_uScript_SetEncounterTarget_visibleObject_69);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_69.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_In_72()
	{
		logic_uScript_ClearEncounterTarget_owner_72 = owner_Connection_73;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_72.In(logic_uScript_ClearEncounterTarget_owner_72);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_72.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_In_74()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_74 = owner_Connection_75;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_74.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_74);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_74.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_76()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_76 = owner_Connection_77;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_76.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_76);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_76.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_76.False;
		if (num)
		{
			Relay_Pause_78();
		}
		if (flag)
		{
			Relay_UnPause_78();
		}
	}

	private void Relay_Pause_78()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_78.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_78.Out)
		{
			Relay_In_82();
		}
	}

	private void Relay_UnPause_78()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_78.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_78.Out)
		{
			Relay_In_82();
		}
	}

	private void Relay_OnSuccess_79()
	{
		local_EndReason_CheckpointChallenge_EndReason = event_UnityEngine_GameObject_EndReason_79;
	}

	private void Relay_OnFail_79()
	{
		local_EndReason_CheckpointChallenge_EndReason = event_UnityEngine_GameObject_EndReason_79;
	}

	private void Relay_Output1_82()
	{
		Relay_In_104();
	}

	private void Relay_Output2_82()
	{
		Relay_In_180();
	}

	private void Relay_Output3_82()
	{
		Relay_In_131();
	}

	private void Relay_Output4_82()
	{
	}

	private void Relay_Output5_82()
	{
	}

	private void Relay_Output6_82()
	{
	}

	private void Relay_Output7_82()
	{
	}

	private void Relay_Output8_82()
	{
	}

	private void Relay_In_82()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_82 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_82.In(logic_uScriptCon_ManualSwitch_CurrentOutput_82);
	}

	private void Relay_In_85()
	{
		logic_uScript_AddMessage_messageData_85 = msg01FindTech;
		logic_uScript_AddMessage_speaker_85 = messageSpeaker;
		logic_uScript_AddMessage_Return_85 = logic_uScript_AddMessage_uScript_AddMessage_85.In(logic_uScript_AddMessage_messageData_85, logic_uScript_AddMessage_speaker_85);
		if (logic_uScript_AddMessage_uScript_AddMessage_85.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_InitialSpawn_87()
	{
		int num = 0;
		Array array = techData;
		if (logic_uScript_SpawnTechsFromData_spawnData_87.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_87, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_87, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_87 = owner_Connection_89;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_87.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_87, logic_uScript_SpawnTechsFromData_ownerNode_87, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_87, logic_uScript_SpawnTechsFromData_allowResurrection_87);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_87.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_94()
	{
		logic_uScript_LockTechSendToSCU_tech_94 = local_Tech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_94.In(logic_uScript_LockTechSendToSCU_tech_94, logic_uScript_LockTechSendToSCU_lockSendToSCU_94);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_94.Out)
		{
			Relay_In_191();
		}
	}

	private void Relay_AtIndex_96()
	{
		int num = 0;
		Array array = local_Techs_TankArray;
		if (logic_uScript_AccessListTech_techList_96.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_96, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_96, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_96.AtIndex(ref logic_uScript_AccessListTech_techList_96, logic_uScript_AccessListTech_index_96, out logic_uScript_AccessListTech_value_96);
		local_Techs_TankArray = logic_uScript_AccessListTech_techList_96;
		local_Tech_Tank = logic_uScript_AccessListTech_value_96;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_96.Out)
		{
			Relay_In_190();
		}
	}

	private void Relay_In_97()
	{
		int num = 0;
		Array array = techData;
		if (logic_uScript_GetAndCheckTechs_techData_97.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_97, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_97, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_97 = owner_Connection_91;
		int num2 = 0;
		Array array2 = local_Techs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_97.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_97, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_97, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_97 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_97.In(logic_uScript_GetAndCheckTechs_techData_97, logic_uScript_GetAndCheckTechs_ownerNode_97, ref logic_uScript_GetAndCheckTechs_techs_97);
		local_Techs_TankArray = logic_uScript_GetAndCheckTechs_techs_97;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_97.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_97.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_97.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_96();
		}
		if (someAlive)
		{
			Relay_AtIndex_96();
		}
		if (allDead)
		{
			Relay_In_178();
		}
	}

	private void Relay_In_98()
	{
		logic_uScript_SetTankTeam_tank_98 = local_Tech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_98.In(logic_uScript_SetTankTeam_tank_98, logic_uScript_SetTankTeam_team_98);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_98.Out)
		{
			Relay_In_106();
		}
	}

	private void Relay_In_100()
	{
		logic_uScript_AddMessage_messageData_100 = msg02TechFound;
		logic_uScript_AddMessage_speaker_100 = messageSpeaker;
		logic_uScript_AddMessage_Return_100 = logic_uScript_AddMessage_uScript_AddMessage_100.In(logic_uScript_AddMessage_messageData_100, logic_uScript_AddMessage_speaker_100);
		if (logic_uScript_AddMessage_uScript_AddMessage_100.Shown)
		{
			Relay_In_98();
		}
	}

	private void Relay_In_104()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_104 = local_Tech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_104 = distAtWhichTechFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_104.In(logic_uScript_IsPlayerInRangeOfTech_tech_104, logic_uScript_IsPlayerInRangeOfTech_range_104, logic_uScript_IsPlayerInRangeOfTech_techs_104);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_104.InRange)
		{
			Relay_In_100();
		}
	}

	private void Relay_Out_106()
	{
	}

	private void Relay_In_106()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_106 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_106.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_106, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_106);
	}

	private void Relay_In_109()
	{
		logic_uScript_IsTechPlayer_tech_109 = local_Tech_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_109.In(logic_uScript_IsTechPlayer_tech_109);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_109.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_109.False;
		if (num)
		{
			Relay_In_118();
		}
		if (flag)
		{
			Relay_In_117();
		}
	}

	private void Relay_Out_111()
	{
	}

	private void Relay_In_111()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_111 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_111, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_111);
	}

	private void Relay_In_113()
	{
		int num = 0;
		Array array = local_116_BlockTypesArray;
		if (logic_uScript_DiscoverBlocks_blockTypes_113.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DiscoverBlocks_blockTypes_113, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DiscoverBlocks_blockTypes_113, num, array.Length);
		num += array.Length;
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_113.In(logic_uScript_DiscoverBlocks_blockTypes_113);
		if (logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_113.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_117()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_117 = local_Tech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_117 = distAtWhichTechFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_117.In(logic_uScript_IsPlayerInRangeOfTech_tech_117, logic_uScript_IsPlayerInRangeOfTech_range_117, logic_uScript_IsPlayerInRangeOfTech_techs_117);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_117.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_117.OutOfRange;
		if (inRange)
		{
			Relay_In_124();
		}
		if (outOfRange)
		{
			Relay_In_189();
		}
	}

	private void Relay_In_118()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_118 = local_Tech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_118 = distAtWhichTechCloseRange;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_118.In(logic_uScript_IsPlayerInRangeOfTech_tech_118, logic_uScript_IsPlayerInRangeOfTech_range_118, logic_uScript_IsPlayerInRangeOfTech_techs_118);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_118.InRange)
		{
			Relay_In_121();
		}
	}

	private void Relay_In_121()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_121 = local_MsgTagAccessMenu_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_121.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_121, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_121);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_121.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_Out_124()
	{
	}

	private void Relay_Shown_124()
	{
	}

	private void Relay_In_124()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_124 = msg03AccessMenu;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_124 = msg03AccessMenu_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_124 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_124.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_124, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_124, logic_SubGraph_AddMessageWithPadSupport_speaker_124);
	}

	private void Relay_Out_128()
	{
		Relay_In_113();
	}

	private void Relay_Shown_128()
	{
	}

	private void Relay_In_128()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_128 = msg04ControlTechComplete;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_128 = msg04ControlTechComplete_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_128 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_128.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_128, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_128, logic_SubGraph_AddMessageWithPadSupport_speaker_128);
	}

	private void Relay_In_131()
	{
		logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_131 = targetChallengeID;
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_131.In(logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_131);
		bool justStarted = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_131.JustStarted;
		bool justEnded = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_131.JustEnded;
		if (justStarted)
		{
			Relay_True_132();
		}
		if (justEnded)
		{
			Relay_False_159();
		}
	}

	private void Relay_True_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.True(out logic_uScriptAct_SetBool_Target_132);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_132;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_132.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_False_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.False(out logic_uScriptAct_SetBool_Target_132);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_132;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_132.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_135()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_135 = local_MsgTagControlTechComplete_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_135.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_135, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_135);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_135.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_True_137()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_137.True(out logic_uScriptAct_SetBool_Target_137);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_137;
	}

	private void Relay_False_137()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_137.False(out logic_uScriptAct_SetBool_Target_137);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_137;
	}

	private void Relay_In_138()
	{
		logic_uScript_AddMessage_messageData_138 = msgStuntStarted;
		logic_uScript_AddMessage_speaker_138 = messageSpeaker;
		logic_uScript_AddMessage_Return_138 = logic_uScript_AddMessage_uScript_AddMessage_138.In(logic_uScript_AddMessage_messageData_138, logic_uScript_AddMessage_speaker_138);
		if (logic_uScript_AddMessage_uScript_AddMessage_138.Out)
		{
			Relay_False_137();
		}
	}

	private void Relay_In_141()
	{
		logic_uScript_AddMessage_messageData_141 = msgStuntComplete;
		logic_uScript_AddMessage_speaker_141 = messageSpeaker;
		logic_uScript_AddMessage_Return_141 = logic_uScript_AddMessage_uScript_AddMessage_141.In(logic_uScript_AddMessage_messageData_141, logic_uScript_AddMessage_speaker_141);
		if (logic_uScript_AddMessage_uScript_AddMessage_141.Out)
		{
			Relay_In_160();
		}
	}

	private void Relay_In_142()
	{
		logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_142.In();
		bool success = logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_142.Success;
		bool failure = logic_uScript_GetLastChallengeResult_uScript_GetLastChallengeResult_142.Failure;
		if (success)
		{
			Relay_In_141();
		}
		if (failure)
		{
			Relay_In_146();
		}
	}

	private void Relay_In_145()
	{
		logic_uScript_AddMessage_messageData_145 = msgQuitFromMenu;
		logic_uScript_AddMessage_speaker_145 = messageSpeaker;
		logic_uScript_AddMessage_Return_145 = logic_uScript_AddMessage_uScript_AddMessage_145.In(logic_uScript_AddMessage_messageData_145, logic_uScript_AddMessage_speaker_145);
	}

	private void Relay_In_146()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_146 = local_MsgTagStuntStarted_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_146.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_146, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_146);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_146.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_147()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_147 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_147.In(logic_uScript_CompareCheckpointChallengeEndReason_result_147, logic_uScript_CompareCheckpointChallengeEndReason_expected_147);
		if (logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_147.EqualTo)
		{
			Relay_In_145();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_156 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_156.In(logic_uScript_CompareCheckpointChallengeEndReason_result_156, logic_uScript_CompareCheckpointChallengeEndReason_expected_156);
		bool equalTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_156.EqualTo;
		bool notEqualTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_156.NotEqualTo;
		if (equalTo)
		{
			Relay_In_163();
		}
		if (notEqualTo)
		{
			Relay_In_168();
		}
	}

	private void Relay_In_157()
	{
		if (local_StartingRamp_UnityEngine_GameObject_previous != local_StartingRamp_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_StartingRamp_UnityEngine_GameObject_previous = local_StartingRamp_UnityEngine_GameObject;
		}
		logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_157 = local_StartingRamp_UnityEngine_GameObject;
		logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_157.In(logic_uScript_ClearChallengeStarterChallengeData_targetChallengeStarterObject_157);
		if (logic_uScript_ClearChallengeStarterChallengeData_uScript_ClearChallengeStarterChallengeData_157.Out)
		{
			Relay_Succeed_158();
		}
	}

	private void Relay_Succeed_158()
	{
		logic_uScript_FinishEncounter_owner_158 = owner_Connection_167;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_158.Succeed(logic_uScript_FinishEncounter_owner_158);
	}

	private void Relay_Fail_158()
	{
		logic_uScript_FinishEncounter_owner_158 = owner_Connection_167;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_158.Fail(logic_uScript_FinishEncounter_owner_158);
	}

	private void Relay_True_159()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.True(out logic_uScriptAct_SetBool_Target_159);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_159;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_159.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_False_159()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.False(out logic_uScriptAct_SetBool_Target_159);
		local_ChallengeInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_159;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_159.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_In_160()
	{
		logic_uScript_LockTechSendToSCU_tech_160 = local_Tech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_160.In(logic_uScript_LockTechSendToSCU_tech_160, logic_uScript_LockTechSendToSCU_lockSendToSCU_160);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_160.Out)
		{
			Relay_In_193();
		}
	}

	private void Relay_In_163()
	{
		logic_uScript_AddMessage_messageData_163 = msgOutOfBounds;
		logic_uScript_AddMessage_speaker_163 = messageSpeaker;
		logic_uScript_AddMessage_Return_163 = logic_uScript_AddMessage_uScript_AddMessage_163.In(logic_uScript_AddMessage_messageData_163, logic_uScript_AddMessage_speaker_163);
	}

	private void Relay_In_164()
	{
		logic_uScript_AddMessage_messageData_164 = msgTouchedGround;
		logic_uScript_AddMessage_speaker_164 = messageSpeaker;
		logic_uScript_AddMessage_Return_164 = logic_uScript_AddMessage_uScript_AddMessage_164.In(logic_uScript_AddMessage_messageData_164, logic_uScript_AddMessage_speaker_164);
	}

	private void Relay_In_168()
	{
		logic_uScript_CompareCheckpointChallengeEndReason_result_168 = local_EndReason_CheckpointChallenge_EndReason;
		logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_168.In(logic_uScript_CompareCheckpointChallengeEndReason_result_168, logic_uScript_CompareCheckpointChallengeEndReason_expected_168);
		bool equalTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_168.EqualTo;
		bool notEqualTo = logic_uScript_CompareCheckpointChallengeEndReason_uScript_CompareCheckpointChallengeEndReason_168.NotEqualTo;
		if (equalTo)
		{
			Relay_In_164();
		}
		if (notEqualTo)
		{
			Relay_In_147();
		}
	}

	private void Relay_Pause_170()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_170.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_170.Out)
		{
			Relay_In_172();
		}
	}

	private void Relay_UnPause_170()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_170.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_170.Out)
		{
			Relay_In_172();
		}
	}

	private void Relay_In_172()
	{
		logic_uScript_RemoveTech_tech_172 = local_NPCTech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_172.In(logic_uScript_RemoveTech_tech_172);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_172.Out)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_177()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_177.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_177.Out)
		{
			Relay_In_97();
		}
	}

	private void Relay_In_178()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_178.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_178.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_180()
	{
		logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_180 = targetChallengeID;
		logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_180.In(logic_uScript_GetChallengeStateFromChallengeID_uniqueChallengeID_180);
		bool notRunning = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_180.NotRunning;
		bool justStarted = logic_uScript_GetChallengeStateFromChallengeID_uScript_GetChallengeStateFromChallengeID_180.JustStarted;
		if (notRunning)
		{
			Relay_In_109();
		}
		if (justStarted)
		{
			Relay_In_181();
		}
	}

	private void Relay_Out_181()
	{
		Relay_True_132();
	}

	private void Relay_In_181()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_181 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_181.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_181, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_181);
	}

	private void Relay_In_189()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_189 = local_MsgTagAccessMenu_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_189.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_189, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_189);
	}

	private void Relay_In_190()
	{
		logic_uScript_LockTech_tech_190 = local_Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_190.In(logic_uScript_LockTech_tech_190, logic_uScript_LockTech_lockType_190);
		if (logic_uScript_LockTech_uScript_LockTech_190.Out)
		{
			Relay_In_195();
		}
	}

	private void Relay_In_191()
	{
		logic_uScript_SetTankInvulnerable_tank_191 = local_Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_191.In(logic_uScript_SetTankInvulnerable_invulnerable_191, logic_uScript_SetTankInvulnerable_tank_191);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_191.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_193()
	{
		logic_uScript_SetTankInvulnerable_tank_193 = local_Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_193.In(logic_uScript_SetTankInvulnerable_invulnerable_193, logic_uScript_SetTankInvulnerable_tank_193);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_193.Out)
		{
			Relay_UnPause_170();
		}
	}

	private void Relay_In_195()
	{
		logic_uScript_LockTech_tech_195 = local_Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_195.In(logic_uScript_LockTech_tech_195, logic_uScript_LockTech_lockType_195);
		if (logic_uScript_LockTech_uScript_LockTech_195.Out)
		{
			Relay_In_94();
		}
	}
}
