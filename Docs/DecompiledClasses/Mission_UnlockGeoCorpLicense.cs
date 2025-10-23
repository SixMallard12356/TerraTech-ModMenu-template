using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_UnlockGeoCorpLicense : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool _DEBUGEmulateMultiplayer;

	public BlockTypes blockTypeConsumer;

	public float clearSceneryRadius;

	public float consumeTime;

	public float distNPCFound;

	private float local_126_System_Single;

	private Tank[] local_92_TankArray = new Tank[0];

	private TankBlock local_ConsumerBlock_TankBlock;

	private bool local_ConsumeTimeReached_System_Boolean;

	private int local_CurrentAmount_System_Int32;

	private int local_CurrentAmountTotal_System_Int32;

	private float local_CurrentConsumeTime_System_Single;

	private bool local_Init_System_Boolean;

	private int local_InitialAmount_System_Int32;

	private bool local_MiningPhaseInit_MP_System_Boolean;

	private bool local_MsgConsumingHalfway_System_Boolean;

	private bool local_MsgConsumingStarted_System_Boolean;

	private bool local_msgIntro_System_Boolean;

	private bool local_NearNPC_System_Boolean;

	private Tank local_NPCTech_Tank;

	private bool local_ResourcesMined_MP_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private bool local_TimerReset_System_Boolean;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02NPCFound;

	public uScript_AddMessage.MessageData msg03aConsumingStarted;

	public uScript_AddMessage.MessageData msg03bConsumingHalfway;

	public uScript_AddMessage.MessageData msg04Complete;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	[Multiline(3)]
	public string NPCPosition = "";

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	private GameObject owner_Connection_13;

	private GameObject owner_Connection_15;

	private GameObject owner_Connection_42;

	private GameObject owner_Connection_45;

	private GameObject owner_Connection_47;

	private GameObject owner_Connection_56;

	private GameObject owner_Connection_57;

	private GameObject owner_Connection_68;

	private GameObject owner_Connection_164;

	private GameObject owner_Connection_165;

	private GameObject owner_Connection_193;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_2 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_2;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_2 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_2 = "Stage";

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_5 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_5 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_5;

	private bool logic_uScript_SetTankInvulnerable_Out_5 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_10 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_10;

	private BlockTypes logic_uScript_GetTankBlock_blockType_10;

	private TankBlock logic_uScript_GetTankBlock_Return_10;

	private bool logic_uScript_GetTankBlock_Out_10 = true;

	private bool logic_uScript_GetTankBlock_Returned_10 = true;

	private bool logic_uScript_GetTankBlock_NotFound_10 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_11 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_11;

	private object logic_uScript_SetEncounterTarget_visibleObject_11 = "";

	private bool logic_uScript_SetEncounterTarget_Out_11 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_12 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_12 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_16 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_16;

	private int logic_uScript_SetTankTeam_team_16 = -2;

	private bool logic_uScript_SetTankTeam_Out_16 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_17 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_17;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_17;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_22;

	private bool logic_uScriptCon_CompareBool_True_22 = true;

	private bool logic_uScriptCon_CompareBool_False_22 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_24 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_24;

	private float logic_uScript_IsPlayerInRangeOfTech_range_24 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_24 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_24 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_24 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_24 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_25 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_25;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_25;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_25;

	private bool logic_uScript_AddMessage_Out_25 = true;

	private bool logic_uScript_AddMessage_Shown_25 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_27 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_27 = new Tank[0];

	private int logic_uScript_AccessListTech_index_27;

	private Tank logic_uScript_AccessListTech_value_27;

	private bool logic_uScript_AccessListTech_Out_27 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_28 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_28;

	private bool logic_uScriptAct_SetBool_Out_28 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_28 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_28 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_29 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_29;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_29;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_29;

	private bool logic_uScript_AddMessage_Out_29 = true;

	private bool logic_uScript_AddMessage_Shown_29 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_31 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_31;

	private bool logic_uScriptAct_SetBool_Out_31 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_31 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_31 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_37;

	private bool logic_uScriptCon_CompareBool_True_37 = true;

	private bool logic_uScriptCon_CompareBool_False_37 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_38 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_38;

	private string logic_uScript_RemoveScenery_positionName_38 = "";

	private float logic_uScript_RemoveScenery_radius_38;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_38 = true;

	private bool logic_uScript_RemoveScenery_Out_38 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_39 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_39;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_39 = new BlockTypes[0];

	private bool logic_uScript_LockTechInteraction_Out_39 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_41;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_43 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_43;

	private bool logic_uScriptAct_SetBool_Out_43 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_43 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_43 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_53 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_53;

	private int logic_uScriptCon_CompareInt_B_53 = 1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_53 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_53 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_53 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_53 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_53 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_53 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_54 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_54 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_58 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_58;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_58 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_58;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_58 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_58 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_58 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_58 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_59 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_59;

	private bool logic_uScript_FinishEncounter_Out_59 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_61 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_61;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_61 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_61 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_62 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_62;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_62 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_63 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_63 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_63;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_63 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_63 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_65 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_65 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_65 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_65 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_69 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_69;

	private float logic_uScript_IsPlayerInRangeOfTech_range_69;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_69 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_69 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_69 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_69 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_70 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_70;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_70;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_70;

	private bool logic_uScript_AddMessage_Out_70 = true;

	private bool logic_uScript_AddMessage_Shown_70 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_72 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_72;

	private bool logic_uScriptAct_SetBool_Out_72 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_72 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_72 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_73 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_73;

	private bool logic_uScriptAct_SetBool_Out_73 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_73 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_73 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_76 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_76;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_79 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_79;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_79 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_79 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_80;

	private bool logic_uScriptCon_CompareBool_True_80 = true;

	private bool logic_uScriptCon_CompareBool_False_80 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_81;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_81 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_81 = "Init";

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_82 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_82;

	private int logic_uScript_SetTankTeam_team_82;

	private bool logic_uScript_SetTankTeam_Out_82 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_84 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_84;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_84;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_84;

	private bool logic_uScript_AddMessage_Out_84 = true;

	private bool logic_uScript_AddMessage_Shown_84 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_85 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_85;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_85 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_85 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_90 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_93 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_93 = true;

	private uScript_IsCraftingBlockInOperation logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_94 = new uScript_IsCraftingBlockInOperation();

	private TankBlock logic_uScript_IsCraftingBlockInOperation_craftingBlock_94;

	private bool logic_uScript_IsCraftingBlockInOperation_True_94 = true;

	private bool logic_uScript_IsCraftingBlockInOperation_False_94 = true;

	private uScriptAct_Stopwatch logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_96 = new uScriptAct_Stopwatch();

	private float logic_uScriptAct_Stopwatch_Seconds_96;

	private bool logic_uScriptAct_Stopwatch_Started_96 = true;

	private bool logic_uScriptAct_Stopwatch_Stopped_96 = true;

	private bool logic_uScriptAct_Stopwatch_Reset_96 = true;

	private bool logic_uScriptAct_Stopwatch_CheckedTime_96 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_98 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_98;

	private float logic_uScriptCon_CompareFloat_B_98;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_98 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_98 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_98 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_98 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_98 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_98 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_103 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_103;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_103;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_103;

	private bool logic_uScript_AddMessage_Out_103 = true;

	private bool logic_uScript_AddMessage_Shown_103 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_104 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_104;

	private bool logic_uScriptCon_CompareBool_True_104 = true;

	private bool logic_uScriptCon_CompareBool_False_104 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_106 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_106;

	private bool logic_uScriptAct_SetBool_Out_106 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_106 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_106 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_107 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_108 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_108 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_109;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_109 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_109 = "ConsumeTimeReached";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_114 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_114;

	private bool logic_uScriptAct_SetBool_Out_114 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_114 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_114 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_115;

	private bool logic_uScriptCon_CompareBool_True_115 = true;

	private bool logic_uScriptCon_CompareBool_False_115 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_117 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_117;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_117;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_117;

	private bool logic_uScript_AddMessage_Out_117 = true;

	private bool logic_uScript_AddMessage_Shown_117 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_121;

	private bool logic_uScriptCon_CompareBool_True_121 = true;

	private bool logic_uScriptCon_CompareBool_False_121 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_123 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_123;

	private bool logic_uScriptAct_SetBool_Out_123 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_123 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_123 = true;

	private uScriptAct_DivideFloat logic_uScriptAct_DivideFloat_uScriptAct_DivideFloat_124 = new uScriptAct_DivideFloat();

	private float logic_uScriptAct_DivideFloat_A_124;

	private float logic_uScriptAct_DivideFloat_B_124 = 2f;

	private float logic_uScriptAct_DivideFloat_FloatResult_124;

	private int logic_uScriptAct_DivideFloat_IntResult_124;

	private bool logic_uScriptAct_DivideFloat_Out_124 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_127 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_127;

	private float logic_uScriptCon_CompareFloat_B_127;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_127 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_127 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_127 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_127 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_127 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_127 = true;

	private uScript_RestrictItemPickup logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_129 = new uScript_RestrictItemPickup();

	private Tank logic_uScript_RestrictItemPickup_tech_129;

	private ChunkTypes[] logic_uScript_RestrictItemPickup_typesToAccept_129 = new ChunkTypes[0];

	private bool logic_uScript_RestrictItemPickup_Out_129 = true;

	private uScript_ClearItemPickup logic_uScript_ClearItemPickup_uScript_ClearItemPickup_132 = new uScript_ClearItemPickup();

	private Tank logic_uScript_ClearItemPickup_tech_132;

	private bool logic_uScript_ClearItemPickup_Out_132 = true;

	private uScript_RestrictItemPickup logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_134 = new uScript_RestrictItemPickup();

	private Tank logic_uScript_RestrictItemPickup_tech_134;

	private ChunkTypes[] logic_uScript_RestrictItemPickup_typesToAccept_134 = new ChunkTypes[0];

	private bool logic_uScript_RestrictItemPickup_Out_134 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_140;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_140 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_140 = "msgIntro";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_141 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_141 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_143 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_143;

	private bool logic_uScriptAct_SetBool_Out_143 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_143 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_143 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_144 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_144;

	private bool logic_uScriptCon_CompareBool_True_144 = true;

	private bool logic_uScriptCon_CompareBool_False_144 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_148 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_148;

	private bool logic_uScriptAct_SetBool_Out_148 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_148 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_148 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_149;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_149 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_149 = "MsgConsumingStarted";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_151;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_151 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_151 = "MsgConsumingHalfway";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_153 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_154 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_154 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_155 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_155;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_155 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_155 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_155;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_155;

	private bool logic_uScript_FlyTechUpAndAway_Out_155 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_159 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_159 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_159;

	private bool logic_uScript_SetTankHideBlockLimit_Out_159 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_160 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_160 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_160 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_166 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_166;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_166;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_166;

	private bool logic_uScript_SetQuestObjectiveCount_Out_166 = true;

	private uScript_GetNumResourcesHarvested logic_uScript_GetNumResourcesHarvested_uScript_GetNumResourcesHarvested_167 = new uScript_GetNumResourcesHarvested();

	private ChunkTypes logic_uScript_GetNumResourcesHarvested_resourceType_167;

	private int logic_uScript_GetNumResourcesHarvested_Return_167;

	private bool logic_uScript_GetNumResourcesHarvested_Out_167 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_169 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_169;

	private bool logic_uScriptAct_SetBool_Out_169 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_169 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_169 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_171;

	private bool logic_uScriptCon_CompareBool_True_171 = true;

	private bool logic_uScriptCon_CompareBool_False_171 = true;

	private SubGraph_CheckStatsTarget logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173 = new SubGraph_CheckStatsTarget();

	private int logic_SubGraph_CheckStatsTarget_objectiveID_173;

	private int logic_SubGraph_CheckStatsTarget_totalAmount_173;

	private int logic_SubGraph_CheckStatsTarget_initialAmount_173;

	private int logic_SubGraph_CheckStatsTarget_currentAmount_173;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_175 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_175;

	private bool logic_uScriptAct_SetBool_Out_175 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_175 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_175 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_177 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_177;

	private bool logic_uScriptCon_CompareBool_True_177 = true;

	private bool logic_uScriptCon_CompareBool_False_177 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_179;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_179 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_179 = "ResourcesMined_MP";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_182;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_182 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_182 = "MiningPhaseInit_MP";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_184;

	private bool logic_uScriptCon_CompareBool_True_184 = true;

	private bool logic_uScriptCon_CompareBool_False_184 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_188;

	private int logic_SubGraph_SaveLoadInt_integer_188;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_188 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_188 = "CurrentAmount";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_190;

	private int logic_SubGraph_SaveLoadInt_integer_190;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_190 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_190 = "InitialAmount";

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_192 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_192;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_192 = 2;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_192;

	private bool logic_uScript_SetQuestObjectiveCount_Out_192 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_194;

	private bool logic_uScriptCon_CompareBool_True_194 = true;

	private bool logic_uScriptCon_CompareBool_False_194 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_195 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_195 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_195 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_198 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_198;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_198;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_200 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_201 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_201;

	private int logic_uScriptCon_CompareInt_B_201 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_201 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_201 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_201 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_201 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_201 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_201 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_205;

	private bool logic_uScriptCon_CompareBool_True_205 = true;

	private bool logic_uScriptCon_CompareBool_False_205 = true;

	private ChunkTypes event_UnityEngine_GameObject_ResourceType_162;

	private int event_UnityEngine_GameObject_ResourceTypeTotal_162;

	private int event_UnityEngine_GameObject_HarvestedTotal_162;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_13 || !m_RegisteredForEvents)
		{
			owner_Connection_13 = parentGameObject;
			if (null != owner_Connection_13)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_13.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_13.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_48;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_48;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_48;
				}
			}
		}
		if (null == owner_Connection_15 || !m_RegisteredForEvents)
		{
			owner_Connection_15 = parentGameObject;
		}
		if (null == owner_Connection_42 || !m_RegisteredForEvents)
		{
			owner_Connection_42 = parentGameObject;
		}
		if (null == owner_Connection_45 || !m_RegisteredForEvents)
		{
			owner_Connection_45 = parentGameObject;
		}
		if (null == owner_Connection_47 || !m_RegisteredForEvents)
		{
			owner_Connection_47 = parentGameObject;
		}
		if (null == owner_Connection_56 || !m_RegisteredForEvents)
		{
			owner_Connection_56 = parentGameObject;
			if (null != owner_Connection_56)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_56.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_56.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_77;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_77;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_77;
				}
			}
		}
		if (null == owner_Connection_57 || !m_RegisteredForEvents)
		{
			owner_Connection_57 = parentGameObject;
		}
		if (null == owner_Connection_68 || !m_RegisteredForEvents)
		{
			owner_Connection_68 = parentGameObject;
		}
		if (null == owner_Connection_164 || !m_RegisteredForEvents)
		{
			owner_Connection_164 = parentGameObject;
			if (null != owner_Connection_164)
			{
				uScript_ResourceHarvestedEvent uScript_ResourceHarvestedEvent2 = owner_Connection_164.GetComponent<uScript_ResourceHarvestedEvent>();
				if (null == uScript_ResourceHarvestedEvent2)
				{
					uScript_ResourceHarvestedEvent2 = owner_Connection_164.AddComponent<uScript_ResourceHarvestedEvent>();
				}
				if (null != uScript_ResourceHarvestedEvent2)
				{
					uScript_ResourceHarvestedEvent2.ResourceHarvestedEvent += Instance_ResourceHarvestedEvent_162;
				}
			}
		}
		if (null == owner_Connection_165 || !m_RegisteredForEvents)
		{
			owner_Connection_165 = parentGameObject;
		}
		if (null == owner_Connection_193 || !m_RegisteredForEvents)
		{
			owner_Connection_193 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_13)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_13.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_13.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_48;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_48;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_48;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_56)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_56.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_56.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_77;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_77;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_77;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_164)
		{
			uScript_ResourceHarvestedEvent uScript_ResourceHarvestedEvent2 = owner_Connection_164.GetComponent<uScript_ResourceHarvestedEvent>();
			if (null == uScript_ResourceHarvestedEvent2)
			{
				uScript_ResourceHarvestedEvent2 = owner_Connection_164.AddComponent<uScript_ResourceHarvestedEvent>();
			}
			if (null != uScript_ResourceHarvestedEvent2)
			{
				uScript_ResourceHarvestedEvent2.ResourceHarvestedEvent += Instance_ResourceHarvestedEvent_162;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_13)
		{
			uScript_EncounterUpdate component = owner_Connection_13.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_48;
				component.OnSuspend -= Instance_OnSuspend_48;
				component.OnResume -= Instance_OnResume_48;
			}
		}
		if (null != owner_Connection_56)
		{
			uScript_SaveLoad component2 = owner_Connection_56.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_77;
				component2.LoadEvent -= Instance_LoadEvent_77;
				component2.RestartEvent -= Instance_RestartEvent_77;
			}
		}
		if (null != owner_Connection_164)
		{
			uScript_ResourceHarvestedEvent component3 = owner_Connection_164.GetComponent<uScript_ResourceHarvestedEvent>();
			if (null != component3)
			{
				component3.ResourceHarvestedEvent -= Instance_ResourceHarvestedEvent_162;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_5.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_10.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_11.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_12.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_16.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_17.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_24.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_25.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_27.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_29.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_38.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_39.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_43.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_53.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_54.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_59.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_61.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_62.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_63.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_65.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_69.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_70.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_72.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_76.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_79.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_82.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_84.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_85.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_93.SetParent(g);
		logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_94.SetParent(g);
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_96.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_98.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_103.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_104.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_106.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_108.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_114.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_117.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.SetParent(g);
		logic_uScriptAct_DivideFloat_uScriptAct_DivideFloat_124.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_127.SetParent(g);
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_129.SetParent(g);
		logic_uScript_ClearItemPickup_uScript_ClearItemPickup_132.SetParent(g);
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_134.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_141.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_143.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_144.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_154.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_155.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_159.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_160.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_166.SetParent(g);
		logic_uScript_GetNumResourcesHarvested_uScript_GetNumResourcesHarvested_167.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_169.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171.SetParent(g);
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_175.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_177.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_192.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_195.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_198.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_201.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.SetParent(g);
		owner_Connection_13 = parentGameObject;
		owner_Connection_15 = parentGameObject;
		owner_Connection_42 = parentGameObject;
		owner_Connection_45 = parentGameObject;
		owner_Connection_47 = parentGameObject;
		owner_Connection_56 = parentGameObject;
		owner_Connection_57 = parentGameObject;
		owner_Connection_68 = parentGameObject;
		owner_Connection_164 = parentGameObject;
		owner_Connection_165 = parentGameObject;
		owner_Connection_193 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_17.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_76.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Awake();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_198.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.Save_Out += SubGraph_SaveLoadInt_Save_Out_2;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.Load_Out += SubGraph_SaveLoadInt_Load_Out_2;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_2;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_17.Out += SubGraph_CompleteObjectiveStage_Out_17;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output1 += uScriptCon_ManualSwitch_Output1_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output2 += uScriptCon_ManualSwitch_Output2_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output3 += uScriptCon_ManualSwitch_Output3_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output4 += uScriptCon_ManualSwitch_Output4_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output5 += uScriptCon_ManualSwitch_Output5_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output6 += uScriptCon_ManualSwitch_Output6_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output7 += uScriptCon_ManualSwitch_Output7_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output8 += uScriptCon_ManualSwitch_Output8_41;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_76.Out += SubGraph_LoadObjectiveStates_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Save_Out += SubGraph_SaveLoadBool_Save_Out_81;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Load_Out += SubGraph_SaveLoadBool_Load_Out_81;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_81;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Save_Out += SubGraph_SaveLoadBool_Save_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Load_Out += SubGraph_SaveLoadBool_Load_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Save_Out += SubGraph_SaveLoadBool_Save_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Load_Out += SubGraph_SaveLoadBool_Load_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Save_Out += SubGraph_SaveLoadBool_Save_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Load_Out += SubGraph_SaveLoadBool_Load_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Save_Out += SubGraph_SaveLoadBool_Save_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Load_Out += SubGraph_SaveLoadBool_Load_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_151;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173.Reached += SubGraph_CheckStatsTarget_Reached_173;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173.Not_Reached += SubGraph_CheckStatsTarget_Not_Reached_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Save_Out += SubGraph_SaveLoadBool_Save_Out_179;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Load_Out += SubGraph_SaveLoadBool_Load_Out_179;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_179;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Save_Out += SubGraph_SaveLoadBool_Save_Out_182;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Load_Out += SubGraph_SaveLoadBool_Load_Out_182;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_182;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Save_Out += SubGraph_SaveLoadInt_Save_Out_188;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Load_Out += SubGraph_SaveLoadInt_Load_Out_188;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_188;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.Save_Out += SubGraph_SaveLoadInt_Save_Out_190;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.Load_Out += SubGraph_SaveLoadInt_Load_Out_190;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_190;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_198.Out += SubGraph_CompleteObjectiveStage_Out_198;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_17.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_76.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Start();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_198.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_17.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_62.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_76.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.OnEnable();
		logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_94.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.OnEnable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_198.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_5.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_17.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_24.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_25.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_29.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_69.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_70.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_76.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_84.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_103.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_117.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_160.OnDisable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_195.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_198.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_17.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_76.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Update();
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_96.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Update();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_198.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_17.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_76.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.OnDestroy();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_198.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.Save_Out -= SubGraph_SaveLoadInt_Save_Out_2;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.Load_Out -= SubGraph_SaveLoadInt_Load_Out_2;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_2;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_17.Out -= SubGraph_CompleteObjectiveStage_Out_17;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output1 -= uScriptCon_ManualSwitch_Output1_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output2 -= uScriptCon_ManualSwitch_Output2_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output3 -= uScriptCon_ManualSwitch_Output3_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output4 -= uScriptCon_ManualSwitch_Output4_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output5 -= uScriptCon_ManualSwitch_Output5_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output6 -= uScriptCon_ManualSwitch_Output6_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output7 -= uScriptCon_ManualSwitch_Output7_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output8 -= uScriptCon_ManualSwitch_Output8_41;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_76.Out -= SubGraph_LoadObjectiveStates_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Save_Out -= SubGraph_SaveLoadBool_Save_Out_81;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Load_Out -= SubGraph_SaveLoadBool_Load_Out_81;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_81;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Save_Out -= SubGraph_SaveLoadBool_Save_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Load_Out -= SubGraph_SaveLoadBool_Load_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Save_Out -= SubGraph_SaveLoadBool_Save_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Load_Out -= SubGraph_SaveLoadBool_Load_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Save_Out -= SubGraph_SaveLoadBool_Save_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Load_Out -= SubGraph_SaveLoadBool_Load_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Save_Out -= SubGraph_SaveLoadBool_Save_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Load_Out -= SubGraph_SaveLoadBool_Load_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_151;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173.Reached -= SubGraph_CheckStatsTarget_Reached_173;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173.Not_Reached -= SubGraph_CheckStatsTarget_Not_Reached_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Save_Out -= SubGraph_SaveLoadBool_Save_Out_179;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Load_Out -= SubGraph_SaveLoadBool_Load_Out_179;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_179;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Save_Out -= SubGraph_SaveLoadBool_Save_Out_182;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Load_Out -= SubGraph_SaveLoadBool_Load_Out_182;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_182;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Save_Out -= SubGraph_SaveLoadInt_Save_Out_188;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Load_Out -= SubGraph_SaveLoadInt_Load_Out_188;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_188;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.Save_Out -= SubGraph_SaveLoadInt_Save_Out_190;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.Load_Out -= SubGraph_SaveLoadInt_Load_Out_190;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_190;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_198.Out -= SubGraph_CompleteObjectiveStage_Out_198;
	}

	private void Instance_OnUpdate_48(object o, EventArgs e)
	{
		Relay_OnUpdate_48();
	}

	private void Instance_OnSuspend_48(object o, EventArgs e)
	{
		Relay_OnSuspend_48();
	}

	private void Instance_OnResume_48(object o, EventArgs e)
	{
		Relay_OnResume_48();
	}

	private void Instance_SaveEvent_77(object o, EventArgs e)
	{
		Relay_SaveEvent_77();
	}

	private void Instance_LoadEvent_77(object o, EventArgs e)
	{
		Relay_LoadEvent_77();
	}

	private void Instance_RestartEvent_77(object o, EventArgs e)
	{
		Relay_RestartEvent_77();
	}

	private void Instance_ResourceHarvestedEvent_162(object o, uScript_ResourceHarvestedEvent.ResourceHarvestedEventArgs e)
	{
		event_UnityEngine_GameObject_ResourceType_162 = e.ResourceType;
		event_UnityEngine_GameObject_ResourceTypeTotal_162 = e.ResourceTypeTotal;
		event_UnityEngine_GameObject_HarvestedTotal_162 = e.HarvestedTotal;
		Relay_ResourceHarvestedEvent_162();
	}

	private void SubGraph_SaveLoadInt_Save_Out_2(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_2 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_2;
		Relay_Save_Out_2();
	}

	private void SubGraph_SaveLoadInt_Load_Out_2(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_2 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_2;
		Relay_Load_Out_2();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_2(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_2 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_2;
		Relay_Restart_Out_2();
	}

	private void SubGraph_CompleteObjectiveStage_Out_17(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_17 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_17;
		Relay_Out_17();
	}

	private void uScriptCon_ManualSwitch_Output1_41(object o, EventArgs e)
	{
		Relay_Output1_41();
	}

	private void uScriptCon_ManualSwitch_Output2_41(object o, EventArgs e)
	{
		Relay_Output2_41();
	}

	private void uScriptCon_ManualSwitch_Output3_41(object o, EventArgs e)
	{
		Relay_Output3_41();
	}

	private void uScriptCon_ManualSwitch_Output4_41(object o, EventArgs e)
	{
		Relay_Output4_41();
	}

	private void uScriptCon_ManualSwitch_Output5_41(object o, EventArgs e)
	{
		Relay_Output5_41();
	}

	private void uScriptCon_ManualSwitch_Output6_41(object o, EventArgs e)
	{
		Relay_Output6_41();
	}

	private void uScriptCon_ManualSwitch_Output7_41(object o, EventArgs e)
	{
		Relay_Output7_41();
	}

	private void uScriptCon_ManualSwitch_Output8_41(object o, EventArgs e)
	{
		Relay_Output8_41();
	}

	private void SubGraph_LoadObjectiveStates_Out_76(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_76();
	}

	private void SubGraph_SaveLoadBool_Save_Out_81(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_81;
		Relay_Save_Out_81();
	}

	private void SubGraph_SaveLoadBool_Load_Out_81(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_81;
		Relay_Load_Out_81();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_81(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_81;
		Relay_Restart_Out_81();
	}

	private void SubGraph_SaveLoadBool_Save_Out_109(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = e.boolean;
		local_ConsumeTimeReached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_109;
		Relay_Save_Out_109();
	}

	private void SubGraph_SaveLoadBool_Load_Out_109(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = e.boolean;
		local_ConsumeTimeReached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_109;
		Relay_Load_Out_109();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_109(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = e.boolean;
		local_ConsumeTimeReached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_109;
		Relay_Restart_Out_109();
	}

	private void SubGraph_SaveLoadBool_Save_Out_140(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = e.boolean;
		local_msgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_140;
		Relay_Save_Out_140();
	}

	private void SubGraph_SaveLoadBool_Load_Out_140(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = e.boolean;
		local_msgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_140;
		Relay_Load_Out_140();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_140(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = e.boolean;
		local_msgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_140;
		Relay_Restart_Out_140();
	}

	private void SubGraph_SaveLoadBool_Save_Out_149(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = e.boolean;
		local_MsgConsumingStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_149;
		Relay_Save_Out_149();
	}

	private void SubGraph_SaveLoadBool_Load_Out_149(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = e.boolean;
		local_MsgConsumingStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_149;
		Relay_Load_Out_149();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_149(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = e.boolean;
		local_MsgConsumingStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_149;
		Relay_Restart_Out_149();
	}

	private void SubGraph_SaveLoadBool_Save_Out_151(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = e.boolean;
		local_MsgConsumingHalfway_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_151;
		Relay_Save_Out_151();
	}

	private void SubGraph_SaveLoadBool_Load_Out_151(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = e.boolean;
		local_MsgConsumingHalfway_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_151;
		Relay_Load_Out_151();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_151(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = e.boolean;
		local_MsgConsumingHalfway_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_151;
		Relay_Restart_Out_151();
	}

	private void SubGraph_CheckStatsTarget_Reached_173(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_173 = e.currentAmount;
		local_CurrentAmount_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_173;
		Relay_Reached_173();
	}

	private void SubGraph_CheckStatsTarget_Not_Reached_173(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_173 = e.currentAmount;
		local_CurrentAmount_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_173;
		Relay_Not_Reached_173();
	}

	private void SubGraph_SaveLoadBool_Save_Out_179(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_179 = e.boolean;
		local_ResourcesMined_MP_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_179;
		Relay_Save_Out_179();
	}

	private void SubGraph_SaveLoadBool_Load_Out_179(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_179 = e.boolean;
		local_ResourcesMined_MP_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_179;
		Relay_Load_Out_179();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_179(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_179 = e.boolean;
		local_ResourcesMined_MP_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_179;
		Relay_Restart_Out_179();
	}

	private void SubGraph_SaveLoadBool_Save_Out_182(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_182 = e.boolean;
		local_MiningPhaseInit_MP_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_182;
		Relay_Save_Out_182();
	}

	private void SubGraph_SaveLoadBool_Load_Out_182(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_182 = e.boolean;
		local_MiningPhaseInit_MP_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_182;
		Relay_Load_Out_182();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_182(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_182 = e.boolean;
		local_MiningPhaseInit_MP_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_182;
		Relay_Restart_Out_182();
	}

	private void SubGraph_SaveLoadInt_Save_Out_188(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_188 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_188;
		Relay_Save_Out_188();
	}

	private void SubGraph_SaveLoadInt_Load_Out_188(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_188 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_188;
		Relay_Load_Out_188();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_188(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_188 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_188;
		Relay_Restart_Out_188();
	}

	private void SubGraph_SaveLoadInt_Save_Out_190(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_190 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_190;
		Relay_Save_Out_190();
	}

	private void SubGraph_SaveLoadInt_Load_Out_190(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_190 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_190;
		Relay_Load_Out_190();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_190(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_190 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_190;
		Relay_Restart_Out_190();
	}

	private void SubGraph_CompleteObjectiveStage_Out_198(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_198 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_198;
		Relay_Out_198();
	}

	private void Relay_Save_Out_2()
	{
		Relay_Save_190();
	}

	private void Relay_Load_Out_2()
	{
		Relay_Load_190();
	}

	private void Relay_Restart_Out_2()
	{
		Relay_Restart_190();
	}

	private void Relay_Save_2()
	{
		logic_SubGraph_SaveLoadInt_integer_2 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_2 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.Save(logic_SubGraph_SaveLoadInt_restartValue_2, ref logic_SubGraph_SaveLoadInt_integer_2, logic_SubGraph_SaveLoadInt_intAsVariable_2, logic_SubGraph_SaveLoadInt_uniqueID_2);
	}

	private void Relay_Load_2()
	{
		logic_SubGraph_SaveLoadInt_integer_2 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_2 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.Load(logic_SubGraph_SaveLoadInt_restartValue_2, ref logic_SubGraph_SaveLoadInt_integer_2, logic_SubGraph_SaveLoadInt_intAsVariable_2, logic_SubGraph_SaveLoadInt_uniqueID_2);
	}

	private void Relay_Restart_2()
	{
		logic_SubGraph_SaveLoadInt_integer_2 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_2 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_2.Restart(logic_SubGraph_SaveLoadInt_restartValue_2, ref logic_SubGraph_SaveLoadInt_integer_2, logic_SubGraph_SaveLoadInt_intAsVariable_2, logic_SubGraph_SaveLoadInt_uniqueID_2);
	}

	private void Relay_In_5()
	{
		logic_uScript_SetTankInvulnerable_tank_5 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_5.In(logic_uScript_SetTankInvulnerable_invulnerable_5, logic_uScript_SetTankInvulnerable_tank_5);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_5.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_GetTankBlock_tank_10 = local_NPCTech_Tank;
		logic_uScript_GetTankBlock_blockType_10 = blockTypeConsumer;
		logic_uScript_GetTankBlock_Return_10 = logic_uScript_GetTankBlock_uScript_GetTankBlock_10.In(logic_uScript_GetTankBlock_tank_10, logic_uScript_GetTankBlock_blockType_10);
		local_ConsumerBlock_TankBlock = logic_uScript_GetTankBlock_Return_10;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_10.Returned)
		{
			Relay_In_94();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_SetEncounterTarget_owner_11 = owner_Connection_15;
		logic_uScript_SetEncounterTarget_visibleObject_11 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_11.In(logic_uScript_SetEncounterTarget_owner_11, logic_uScript_SetEncounterTarget_visibleObject_11);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_11.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_Pause_12()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_12.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_12.Out)
		{
			Relay_True_73();
		}
	}

	private void Relay_UnPause_12()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_12.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_12.Out)
		{
			Relay_True_73();
		}
	}

	private void Relay_In_16()
	{
		logic_uScript_SetTankTeam_tank_16 = local_NPCTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_16.In(logic_uScript_SetTankTeam_tank_16, logic_uScript_SetTankTeam_team_16);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_16.Out)
		{
			Relay_In_134();
		}
	}

	private void Relay_Out_17()
	{
	}

	private void Relay_In_17()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_17 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_17.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_17, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_17);
	}

	private void Relay_In_22()
	{
		logic_uScriptCon_CompareBool_Bool_22 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.In(logic_uScriptCon_CompareBool_Bool_22);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.False;
		if (num)
		{
			Relay_In_38();
		}
		if (flag)
		{
			Relay_True_31();
		}
	}

	private void Relay_In_24()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_24 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_24.In(logic_uScript_IsPlayerInRangeOfTech_tech_24, logic_uScript_IsPlayerInRangeOfTech_range_24, logic_uScript_IsPlayerInRangeOfTech_techs_24);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_24.InRange)
		{
			Relay_In_70();
		}
	}

	private void Relay_In_25()
	{
		logic_uScript_AddMessage_messageData_25 = msg02NPCFound;
		logic_uScript_AddMessage_speaker_25 = messageSpeaker;
		logic_uScript_AddMessage_Return_25 = logic_uScript_AddMessage_uScript_AddMessage_25.In(logic_uScript_AddMessage_messageData_25, logic_uScript_AddMessage_speaker_25);
		if (logic_uScript_AddMessage_uScript_AddMessage_25.Shown)
		{
			Relay_In_132();
		}
	}

	private void Relay_AtIndex_27()
	{
		int num = 0;
		Array array = local_92_TankArray;
		if (logic_uScript_AccessListTech_techList_27.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_27, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_27, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_27.AtIndex(ref logic_uScript_AccessListTech_techList_27, logic_uScript_AccessListTech_index_27, out logic_uScript_AccessListTech_value_27);
		local_92_TankArray = logic_uScript_AccessListTech_techList_27;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_27;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_27.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_True_28()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.True(out logic_uScriptAct_SetBool_Target_28);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_28;
	}

	private void Relay_False_28()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.False(out logic_uScriptAct_SetBool_Target_28);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_28;
	}

	private void Relay_In_29()
	{
		logic_uScript_AddMessage_messageData_29 = msg04Complete;
		logic_uScript_AddMessage_speaker_29 = messageSpeaker;
		logic_uScript_AddMessage_Return_29 = logic_uScript_AddMessage_uScript_AddMessage_29.In(logic_uScript_AddMessage_messageData_29, logic_uScript_AddMessage_speaker_29);
		if (logic_uScript_AddMessage_uScript_AddMessage_29.Shown)
		{
			Relay_In_155();
		}
	}

	private void Relay_True_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.True(out logic_uScriptAct_SetBool_Target_31);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_31;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_31.Out)
		{
			Relay_InitialSpawn_63();
		}
	}

	private void Relay_False_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.False(out logic_uScriptAct_SetBool_Target_31);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_31;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_31.Out)
		{
			Relay_InitialSpawn_63();
		}
	}

	private void Relay_In_37()
	{
		logic_uScriptCon_CompareBool_Bool_37 = local_NearNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.In(logic_uScriptCon_CompareBool_Bool_37);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.True)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_38()
	{
		logic_uScript_RemoveScenery_ownerNode_38 = owner_Connection_47;
		logic_uScript_RemoveScenery_positionName_38 = NPCPosition;
		logic_uScript_RemoveScenery_radius_38 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_38.In(logic_uScript_RemoveScenery_ownerNode_38, logic_uScript_RemoveScenery_positionName_38, logic_uScript_RemoveScenery_radius_38, logic_uScript_RemoveScenery_preventChunksSpawning_38);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_38.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_39()
	{
		logic_uScript_LockTechInteraction_tech_39 = local_NPCTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_39.In(logic_uScript_LockTechInteraction_tech_39, logic_uScript_LockTechInteraction_excludedBlocks_39);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_39.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_Output1_41()
	{
		Relay_In_129();
	}

	private void Relay_Output2_41()
	{
		Relay_In_160();
	}

	private void Relay_Output3_41()
	{
		Relay_In_200();
	}

	private void Relay_Output4_41()
	{
	}

	private void Relay_Output5_41()
	{
	}

	private void Relay_Output6_41()
	{
	}

	private void Relay_Output7_41()
	{
	}

	private void Relay_Output8_41()
	{
	}

	private void Relay_In_41()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_41 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.In(logic_uScriptCon_ManualSwitch_CurrentOutput_41);
	}

	private void Relay_True_43()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_43.True(out logic_uScriptAct_SetBool_Target_43);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_43;
	}

	private void Relay_False_43()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_43.False(out logic_uScriptAct_SetBool_Target_43);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_43;
	}

	private void Relay_OnUpdate_48()
	{
		Relay_In_22();
	}

	private void Relay_OnSuspend_48()
	{
	}

	private void Relay_OnResume_48()
	{
	}

	private void Relay_In_53()
	{
		logic_uScriptCon_CompareInt_A_53 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_53.In(logic_uScriptCon_CompareInt_A_53, logic_uScriptCon_CompareInt_B_53);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_53.EqualTo)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_54()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_54.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_54.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_58()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_58.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_58, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_58, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_58 = owner_Connection_42;
		int num2 = 0;
		Array array = local_92_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_58.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_58, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_58, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_58 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.In(logic_uScript_GetAndCheckTechs_techData_58, logic_uScript_GetAndCheckTechs_ownerNode_58, ref logic_uScript_GetAndCheckTechs_techs_58);
		local_92_TankArray = logic_uScript_GetAndCheckTechs_techs_58;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_27();
		}
		if (someAlive)
		{
			Relay_AtIndex_27();
		}
		if (allDead)
		{
			Relay_In_54();
		}
	}

	private void Relay_Succeed_59()
	{
		logic_uScript_FinishEncounter_owner_59 = owner_Connection_68;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_59.Succeed(logic_uScript_FinishEncounter_owner_59);
	}

	private void Relay_Fail_59()
	{
		logic_uScript_FinishEncounter_owner_59 = owner_Connection_68;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_59.Fail(logic_uScript_FinishEncounter_owner_59);
	}

	private void Relay_In_61()
	{
		logic_uScript_SetCustomRadarTeamID_tech_61 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_61.In(logic_uScript_SetCustomRadarTeamID_tech_61, logic_uScript_SetCustomRadarTeamID_radarTeamID_61);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_61.Out)
		{
			Relay_In_62();
		}
	}

	private void Relay_In_62()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_62 = owner_Connection_57;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_62.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_62);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_62.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_InitialSpawn_63()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_63.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_63, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_63, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_63 = owner_Connection_45;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_63.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_63, logic_uScript_SpawnTechsFromData_ownerNode_63, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_63);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_63.Out)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_65()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_65 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_65.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_65, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_65);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_65.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_In_69()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_69 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_69 = distNPCFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_69.In(logic_uScript_IsPlayerInRangeOfTech_tech_69, logic_uScript_IsPlayerInRangeOfTech_range_69, logic_uScript_IsPlayerInRangeOfTech_techs_69);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_69.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_69.OutOfRange;
		if (inRange)
		{
			Relay_Pause_12();
		}
		if (outOfRange)
		{
			Relay_UnPause_93();
		}
	}

	private void Relay_In_70()
	{
		logic_uScript_AddMessage_messageData_70 = msg01Intro;
		logic_uScript_AddMessage_speaker_70 = messageSpeaker;
		logic_uScript_AddMessage_Return_70 = logic_uScript_AddMessage_uScript_AddMessage_70.In(logic_uScript_AddMessage_messageData_70, logic_uScript_AddMessage_speaker_70);
		if (logic_uScript_AddMessage_uScript_AddMessage_70.Out)
		{
			Relay_True_72();
		}
	}

	private void Relay_True_72()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_72.True(out logic_uScriptAct_SetBool_Target_72);
		local_msgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_72;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_72.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_False_72()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_72.False(out logic_uScriptAct_SetBool_Target_72);
		local_msgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_72;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_72.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_True_73()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.True(out logic_uScriptAct_SetBool_Target_73);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_73;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_73.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_False_73()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.False(out logic_uScriptAct_SetBool_Target_73);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_73;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_73.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_Out_76()
	{
		Relay_In_195();
	}

	private void Relay_In_76()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_76 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_76.In(logic_SubGraph_LoadObjectiveStates_currentObjective_76);
	}

	private void Relay_SaveEvent_77()
	{
		Relay_Save_2();
	}

	private void Relay_LoadEvent_77()
	{
		Relay_Load_2();
	}

	private void Relay_RestartEvent_77()
	{
		Relay_Restart_2();
	}

	private void Relay_In_79()
	{
		logic_uScript_SetCustomRadarTeamID_tech_79 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_79.In(logic_uScript_SetCustomRadarTeamID_tech_79, logic_uScript_SetCustomRadarTeamID_radarTeamID_79);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_79.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_80()
	{
		logic_uScriptCon_CompareBool_Bool_80 = local_msgIntro_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.In(logic_uScriptCon_CompareBool_Bool_80);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.False;
		if (num)
		{
			Relay_In_69();
		}
		if (flag)
		{
			Relay_In_24();
		}
	}

	private void Relay_Save_Out_81()
	{
		Relay_Save_140();
	}

	private void Relay_Load_Out_81()
	{
		Relay_Load_140();
	}

	private void Relay_Restart_Out_81()
	{
		Relay_Set_False_140();
	}

	private void Relay_Save_81()
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_81 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Save(ref logic_SubGraph_SaveLoadBool_boolean_81, logic_SubGraph_SaveLoadBool_boolAsVariable_81, logic_SubGraph_SaveLoadBool_uniqueID_81);
	}

	private void Relay_Load_81()
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_81 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Load(ref logic_SubGraph_SaveLoadBool_boolean_81, logic_SubGraph_SaveLoadBool_boolAsVariable_81, logic_SubGraph_SaveLoadBool_uniqueID_81);
	}

	private void Relay_Set_True_81()
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_81 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_81, logic_SubGraph_SaveLoadBool_boolAsVariable_81, logic_SubGraph_SaveLoadBool_uniqueID_81);
	}

	private void Relay_Set_False_81()
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_81 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_81, logic_SubGraph_SaveLoadBool_boolAsVariable_81, logic_SubGraph_SaveLoadBool_uniqueID_81);
	}

	private void Relay_In_82()
	{
		logic_uScript_SetTankTeam_tank_82 = local_NPCTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_82.In(logic_uScript_SetTankTeam_tank_82, logic_uScript_SetTankTeam_team_82);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_82.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_84()
	{
		logic_uScript_AddMessage_messageData_84 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_84 = messageSpeaker;
		logic_uScript_AddMessage_Return_84 = logic_uScript_AddMessage_uScript_AddMessage_84.In(logic_uScript_AddMessage_messageData_84, logic_uScript_AddMessage_speaker_84);
		if (logic_uScript_AddMessage_uScript_AddMessage_84.Out)
		{
			Relay_False_28();
		}
	}

	private void Relay_In_85()
	{
		logic_uScript_LockTech_tech_85 = local_NPCTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_85.In(logic_uScript_LockTech_tech_85, logic_uScript_LockTech_lockType_85);
		if (logic_uScript_LockTech_uScript_LockTech_85.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_90()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.Out)
		{
			Relay_In_62();
		}
	}

	private void Relay_Pause_93()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_93.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_93.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_UnPause_93()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_93.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_93.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_In_94()
	{
		logic_uScript_IsCraftingBlockInOperation_craftingBlock_94 = local_ConsumerBlock_TankBlock;
		logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_94.In(logic_uScript_IsCraftingBlockInOperation_craftingBlock_94);
		bool num = logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_94.True;
		bool flag = logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_94.False;
		if (num)
		{
			Relay_In_104();
		}
		if (flag)
		{
			Relay_In_107();
		}
	}

	private void Relay_StartTimer_96()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_96.StartTimer(out logic_uScriptAct_Stopwatch_Seconds_96);
		local_CurrentConsumeTime_System_Single = logic_uScriptAct_Stopwatch_Seconds_96;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_96.Started)
		{
			Relay_In_124();
		}
	}

	private void Relay_Stop_96()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_96.Stop(out logic_uScriptAct_Stopwatch_Seconds_96);
		local_CurrentConsumeTime_System_Single = logic_uScriptAct_Stopwatch_Seconds_96;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_96.Started)
		{
			Relay_In_124();
		}
	}

	private void Relay_ResetTimer_96()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_96.ResetTimer(out logic_uScriptAct_Stopwatch_Seconds_96);
		local_CurrentConsumeTime_System_Single = logic_uScriptAct_Stopwatch_Seconds_96;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_96.Started)
		{
			Relay_In_124();
		}
	}

	private void Relay_CheckTime_96()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_96.CheckTime(out logic_uScriptAct_Stopwatch_Seconds_96);
		local_CurrentConsumeTime_System_Single = logic_uScriptAct_Stopwatch_Seconds_96;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_96.Started)
		{
			Relay_In_124();
		}
	}

	private void Relay_In_98()
	{
		logic_uScriptCon_CompareFloat_A_98 = local_CurrentConsumeTime_System_Single;
		logic_uScriptCon_CompareFloat_B_98 = consumeTime;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_98.In(logic_uScriptCon_CompareFloat_A_98, logic_uScriptCon_CompareFloat_B_98);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_98.GreaterThanOrEqualTo)
		{
			Relay_True_123();
		}
	}

	private void Relay_In_103()
	{
		logic_uScript_AddMessage_messageData_103 = msg03aConsumingStarted;
		logic_uScript_AddMessage_speaker_103 = messageSpeaker;
		logic_uScript_AddMessage_Return_103 = logic_uScript_AddMessage_uScript_AddMessage_103.In(logic_uScript_AddMessage_messageData_103, logic_uScript_AddMessage_speaker_103);
		if (logic_uScript_AddMessage_uScript_AddMessage_103.Out)
		{
			Relay_StartTimer_96();
		}
	}

	private void Relay_In_104()
	{
		logic_uScriptCon_CompareBool_Bool_104 = local_MsgConsumingStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_104.In(logic_uScriptCon_CompareBool_Bool_104);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_104.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_104.False;
		if (num)
		{
			Relay_In_141();
		}
		if (flag)
		{
			Relay_True_106();
		}
	}

	private void Relay_True_106()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_106.True(out logic_uScriptAct_SetBool_Target_106);
		local_MsgConsumingStarted_System_Boolean = logic_uScriptAct_SetBool_Target_106;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_106.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_False_106()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_106.False(out logic_uScriptAct_SetBool_Target_106);
		local_MsgConsumingStarted_System_Boolean = logic_uScriptAct_SetBool_Target_106;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_106.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_107()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.Out)
		{
			Relay_In_108();
		}
	}

	private void Relay_In_108()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_108.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_108.Out)
		{
			Relay_Stop_96();
		}
	}

	private void Relay_Save_Out_109()
	{
		Relay_Save_179();
	}

	private void Relay_Load_Out_109()
	{
		Relay_Load_179();
	}

	private void Relay_Restart_Out_109()
	{
		Relay_Set_False_179();
	}

	private void Relay_Save_109()
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = local_ConsumeTimeReached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_109 = local_ConsumeTimeReached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Save(ref logic_SubGraph_SaveLoadBool_boolean_109, logic_SubGraph_SaveLoadBool_boolAsVariable_109, logic_SubGraph_SaveLoadBool_uniqueID_109);
	}

	private void Relay_Load_109()
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = local_ConsumeTimeReached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_109 = local_ConsumeTimeReached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Load(ref logic_SubGraph_SaveLoadBool_boolean_109, logic_SubGraph_SaveLoadBool_boolAsVariable_109, logic_SubGraph_SaveLoadBool_uniqueID_109);
	}

	private void Relay_Set_True_109()
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = local_ConsumeTimeReached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_109 = local_ConsumeTimeReached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_109, logic_SubGraph_SaveLoadBool_boolAsVariable_109, logic_SubGraph_SaveLoadBool_uniqueID_109);
	}

	private void Relay_Set_False_109()
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = local_ConsumeTimeReached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_109 = local_ConsumeTimeReached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_109, logic_SubGraph_SaveLoadBool_boolAsVariable_109, logic_SubGraph_SaveLoadBool_uniqueID_109);
	}

	private void Relay_True_114()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_114.True(out logic_uScriptAct_SetBool_Target_114);
		local_MsgConsumingHalfway_System_Boolean = logic_uScriptAct_SetBool_Target_114;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_114.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_False_114()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_114.False(out logic_uScriptAct_SetBool_Target_114);
		local_MsgConsumingHalfway_System_Boolean = logic_uScriptAct_SetBool_Target_114;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_114.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_115()
	{
		logic_uScriptCon_CompareBool_Bool_115 = local_MsgConsumingHalfway_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.In(logic_uScriptCon_CompareBool_Bool_115);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.False;
		if (num)
		{
			Relay_In_98();
		}
		if (flag)
		{
			Relay_True_114();
		}
	}

	private void Relay_In_117()
	{
		logic_uScript_AddMessage_messageData_117 = msg03bConsumingHalfway;
		logic_uScript_AddMessage_speaker_117 = messageSpeaker;
		logic_uScript_AddMessage_Return_117 = logic_uScript_AddMessage_uScript_AddMessage_117.In(logic_uScript_AddMessage_messageData_117, logic_uScript_AddMessage_speaker_117);
		if (logic_uScript_AddMessage_uScript_AddMessage_117.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_In_121()
	{
		logic_uScriptCon_CompareBool_Bool_121 = local_ConsumeTimeReached_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.In(logic_uScriptCon_CompareBool_Bool_121);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.False;
		if (num)
		{
			Relay_In_16();
		}
		if (flag)
		{
			Relay_In_144();
		}
	}

	private void Relay_True_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.True(out logic_uScriptAct_SetBool_Target_123);
		local_ConsumeTimeReached_System_Boolean = logic_uScriptAct_SetBool_Target_123;
	}

	private void Relay_False_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.False(out logic_uScriptAct_SetBool_Target_123);
		local_ConsumeTimeReached_System_Boolean = logic_uScriptAct_SetBool_Target_123;
	}

	private void Relay_In_124()
	{
		logic_uScriptAct_DivideFloat_A_124 = consumeTime;
		logic_uScriptAct_DivideFloat_uScriptAct_DivideFloat_124.In(logic_uScriptAct_DivideFloat_A_124, logic_uScriptAct_DivideFloat_B_124, out logic_uScriptAct_DivideFloat_FloatResult_124, out logic_uScriptAct_DivideFloat_IntResult_124);
		local_126_System_Single = logic_uScriptAct_DivideFloat_FloatResult_124;
		if (logic_uScriptAct_DivideFloat_uScriptAct_DivideFloat_124.Out)
		{
			Relay_In_127();
		}
	}

	private void Relay_In_127()
	{
		logic_uScriptCon_CompareFloat_A_127 = local_126_System_Single;
		logic_uScriptCon_CompareFloat_B_127 = local_CurrentConsumeTime_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_127.In(logic_uScriptCon_CompareFloat_A_127, logic_uScriptCon_CompareFloat_B_127);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_127.LessThanOrEqualTo)
		{
			Relay_In_115();
		}
	}

	private void Relay_In_129()
	{
		logic_uScript_RestrictItemPickup_tech_129 = local_NPCTech_Tank;
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_129.In(logic_uScript_RestrictItemPickup_tech_129, logic_uScript_RestrictItemPickup_typesToAccept_129);
		if (logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_129.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_In_132()
	{
		logic_uScript_ClearItemPickup_tech_132 = local_NPCTech_Tank;
		logic_uScript_ClearItemPickup_uScript_ClearItemPickup_132.In(logic_uScript_ClearItemPickup_tech_132);
		if (logic_uScript_ClearItemPickup_uScript_ClearItemPickup_132.Out)
		{
			Relay_False_148();
		}
	}

	private void Relay_In_134()
	{
		logic_uScript_RestrictItemPickup_tech_134 = local_NPCTech_Tank;
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_134.In(logic_uScript_RestrictItemPickup_tech_134, logic_uScript_RestrictItemPickup_typesToAccept_134);
		if (logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_134.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_Save_Out_140()
	{
		Relay_Save_149();
	}

	private void Relay_Load_Out_140()
	{
		Relay_Load_149();
	}

	private void Relay_Restart_Out_140()
	{
		Relay_Set_False_149();
	}

	private void Relay_Save_140()
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_140 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Save(ref logic_SubGraph_SaveLoadBool_boolean_140, logic_SubGraph_SaveLoadBool_boolAsVariable_140, logic_SubGraph_SaveLoadBool_uniqueID_140);
	}

	private void Relay_Load_140()
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_140 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Load(ref logic_SubGraph_SaveLoadBool_boolean_140, logic_SubGraph_SaveLoadBool_boolAsVariable_140, logic_SubGraph_SaveLoadBool_uniqueID_140);
	}

	private void Relay_Set_True_140()
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_140 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_140, logic_SubGraph_SaveLoadBool_boolAsVariable_140, logic_SubGraph_SaveLoadBool_uniqueID_140);
	}

	private void Relay_Set_False_140()
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_140 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_140, logic_SubGraph_SaveLoadBool_boolAsVariable_140, logic_SubGraph_SaveLoadBool_uniqueID_140);
	}

	private void Relay_In_141()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_141.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_141.Out)
		{
			Relay_StartTimer_96();
		}
	}

	private void Relay_True_143()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_143.True(out logic_uScriptAct_SetBool_Target_143);
		local_TimerReset_System_Boolean = logic_uScriptAct_SetBool_Target_143;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_143.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_False_143()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_143.False(out logic_uScriptAct_SetBool_Target_143);
		local_TimerReset_System_Boolean = logic_uScriptAct_SetBool_Target_143;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_143.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_144()
	{
		logic_uScriptCon_CompareBool_Bool_144 = local_TimerReset_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_144.In(logic_uScriptCon_CompareBool_Bool_144);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_144.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_144.False;
		if (num)
		{
			Relay_In_159();
		}
		if (flag)
		{
			Relay_True_143();
		}
	}

	private void Relay_True_148()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.True(out logic_uScriptAct_SetBool_Target_148);
		local_TimerReset_System_Boolean = logic_uScriptAct_SetBool_Target_148;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_148.Out)
		{
			Relay_In_17();
		}
	}

	private void Relay_False_148()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.False(out logic_uScriptAct_SetBool_Target_148);
		local_TimerReset_System_Boolean = logic_uScriptAct_SetBool_Target_148;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_148.Out)
		{
			Relay_In_17();
		}
	}

	private void Relay_Save_Out_149()
	{
		Relay_Save_151();
	}

	private void Relay_Load_Out_149()
	{
		Relay_Load_151();
	}

	private void Relay_Restart_Out_149()
	{
		Relay_Set_False_151();
	}

	private void Relay_Save_149()
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = local_MsgConsumingStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_149 = local_MsgConsumingStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Save(ref logic_SubGraph_SaveLoadBool_boolean_149, logic_SubGraph_SaveLoadBool_boolAsVariable_149, logic_SubGraph_SaveLoadBool_uniqueID_149);
	}

	private void Relay_Load_149()
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = local_MsgConsumingStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_149 = local_MsgConsumingStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Load(ref logic_SubGraph_SaveLoadBool_boolean_149, logic_SubGraph_SaveLoadBool_boolAsVariable_149, logic_SubGraph_SaveLoadBool_uniqueID_149);
	}

	private void Relay_Set_True_149()
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = local_MsgConsumingStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_149 = local_MsgConsumingStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_149, logic_SubGraph_SaveLoadBool_boolAsVariable_149, logic_SubGraph_SaveLoadBool_uniqueID_149);
	}

	private void Relay_Set_False_149()
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = local_MsgConsumingStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_149 = local_MsgConsumingStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_149, logic_SubGraph_SaveLoadBool_boolAsVariable_149, logic_SubGraph_SaveLoadBool_uniqueID_149);
	}

	private void Relay_Save_Out_151()
	{
		Relay_Save_109();
	}

	private void Relay_Load_Out_151()
	{
		Relay_Load_109();
	}

	private void Relay_Restart_Out_151()
	{
		Relay_Set_False_109();
	}

	private void Relay_Save_151()
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = local_MsgConsumingHalfway_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_151 = local_MsgConsumingHalfway_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Save(ref logic_SubGraph_SaveLoadBool_boolean_151, logic_SubGraph_SaveLoadBool_boolAsVariable_151, logic_SubGraph_SaveLoadBool_uniqueID_151);
	}

	private void Relay_Load_151()
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = local_MsgConsumingHalfway_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_151 = local_MsgConsumingHalfway_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Load(ref logic_SubGraph_SaveLoadBool_boolean_151, logic_SubGraph_SaveLoadBool_boolAsVariable_151, logic_SubGraph_SaveLoadBool_uniqueID_151);
	}

	private void Relay_Set_True_151()
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = local_MsgConsumingHalfway_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_151 = local_MsgConsumingHalfway_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_151, logic_SubGraph_SaveLoadBool_boolAsVariable_151, logic_SubGraph_SaveLoadBool_uniqueID_151);
	}

	private void Relay_Set_False_151()
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = local_MsgConsumingHalfway_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_151 = local_MsgConsumingHalfway_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_151, logic_SubGraph_SaveLoadBool_boolAsVariable_151, logic_SubGraph_SaveLoadBool_uniqueID_151);
	}

	private void Relay_In_153()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153.Out)
		{
			Relay_In_154();
		}
	}

	private void Relay_In_154()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_154.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_154.Out)
		{
			Relay_ResetTimer_96();
		}
	}

	private void Relay_In_155()
	{
		logic_uScript_FlyTechUpAndAway_tech_155 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_155 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_155 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_155.In(logic_uScript_FlyTechUpAndAway_tech_155, logic_uScript_FlyTechUpAndAway_maxLifetime_155, logic_uScript_FlyTechUpAndAway_targetHeight_155, logic_uScript_FlyTechUpAndAway_aiTree_155, logic_uScript_FlyTechUpAndAway_removalParticles_155);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_155.Out)
		{
			Relay_Succeed_59();
		}
	}

	private void Relay_In_159()
	{
		logic_uScript_SetTankHideBlockLimit_tech_159 = local_NPCTech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_159.In(logic_uScript_SetTankHideBlockLimit_hidden_159, logic_uScript_SetTankHideBlockLimit_tech_159);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_159.Out)
		{
			Relay_In_82();
		}
	}

	private void Relay_In_160()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_160.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_160.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_160.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_184();
		}
		if (multiplayer)
		{
			Relay_In_171();
		}
	}

	private void Relay_ResourceHarvestedEvent_162()
	{
		local_CurrentAmountTotal_System_Int32 = event_UnityEngine_GameObject_HarvestedTotal_162;
		Relay_In_177();
	}

	private void Relay_SetCount_166()
	{
		logic_uScript_SetQuestObjectiveCount_owner_166 = owner_Connection_165;
		logic_uScript_SetQuestObjectiveCount_objectiveId_166 = local_Stage_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_166.SetCount(logic_uScript_SetQuestObjectiveCount_owner_166, logic_uScript_SetQuestObjectiveCount_objectiveId_166, logic_uScript_SetQuestObjectiveCount_currentCount_166);
	}

	private void Relay_AllResources_167()
	{
		logic_uScript_GetNumResourcesHarvested_Return_167 = logic_uScript_GetNumResourcesHarvested_uScript_GetNumResourcesHarvested_167.AllResources(logic_uScript_GetNumResourcesHarvested_resourceType_167);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumResourcesHarvested_Return_167;
		if (logic_uScript_GetNumResourcesHarvested_uScript_GetNumResourcesHarvested_167.Out)
		{
			Relay_SetCount_166();
		}
	}

	private void Relay_ResourcesOfType_167()
	{
		logic_uScript_GetNumResourcesHarvested_Return_167 = logic_uScript_GetNumResourcesHarvested_uScript_GetNumResourcesHarvested_167.ResourcesOfType(logic_uScript_GetNumResourcesHarvested_resourceType_167);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumResourcesHarvested_Return_167;
		if (logic_uScript_GetNumResourcesHarvested_uScript_GetNumResourcesHarvested_167.Out)
		{
			Relay_SetCount_166();
		}
	}

	private void Relay_True_169()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_169.True(out logic_uScriptAct_SetBool_Target_169);
		local_MiningPhaseInit_MP_System_Boolean = logic_uScriptAct_SetBool_Target_169;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_169.Out)
		{
			Relay_AllResources_167();
		}
	}

	private void Relay_False_169()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_169.False(out logic_uScriptAct_SetBool_Target_169);
		local_MiningPhaseInit_MP_System_Boolean = logic_uScriptAct_SetBool_Target_169;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_169.Out)
		{
			Relay_AllResources_167();
		}
	}

	private void Relay_In_171()
	{
		logic_uScriptCon_CompareBool_Bool_171 = local_MiningPhaseInit_MP_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171.In(logic_uScriptCon_CompareBool_Bool_171);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171.False)
		{
			Relay_True_169();
		}
	}

	private void Relay_Reached_173()
	{
		Relay_True_175();
	}

	private void Relay_Not_Reached_173()
	{
	}

	private void Relay_In_173()
	{
		logic_SubGraph_CheckStatsTarget_objectiveID_173 = local_Stage_System_Int32;
		logic_SubGraph_CheckStatsTarget_totalAmount_173 = local_CurrentAmountTotal_System_Int32;
		logic_SubGraph_CheckStatsTarget_initialAmount_173 = local_InitialAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_currentAmount_173 = local_CurrentAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_173.In(logic_SubGraph_CheckStatsTarget_objectiveID_173, logic_SubGraph_CheckStatsTarget_totalAmount_173, logic_SubGraph_CheckStatsTarget_initialAmount_173, ref logic_SubGraph_CheckStatsTarget_currentAmount_173);
	}

	private void Relay_True_175()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_175.True(out logic_uScriptAct_SetBool_Target_175);
		local_ResourcesMined_MP_System_Boolean = logic_uScriptAct_SetBool_Target_175;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_175.Out)
		{
			Relay_In_198();
		}
	}

	private void Relay_False_175()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_175.False(out logic_uScriptAct_SetBool_Target_175);
		local_ResourcesMined_MP_System_Boolean = logic_uScriptAct_SetBool_Target_175;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_175.Out)
		{
			Relay_In_198();
		}
	}

	private void Relay_In_177()
	{
		logic_uScriptCon_CompareBool_Bool_177 = local_MiningPhaseInit_MP_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_177.In(logic_uScriptCon_CompareBool_Bool_177);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_177.True)
		{
			Relay_In_205();
		}
	}

	private void Relay_Save_Out_179()
	{
		Relay_Save_182();
	}

	private void Relay_Load_Out_179()
	{
		Relay_Load_182();
	}

	private void Relay_Restart_Out_179()
	{
		Relay_Set_False_182();
	}

	private void Relay_Save_179()
	{
		logic_SubGraph_SaveLoadBool_boolean_179 = local_ResourcesMined_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_179 = local_ResourcesMined_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Save(ref logic_SubGraph_SaveLoadBool_boolean_179, logic_SubGraph_SaveLoadBool_boolAsVariable_179, logic_SubGraph_SaveLoadBool_uniqueID_179);
	}

	private void Relay_Load_179()
	{
		logic_SubGraph_SaveLoadBool_boolean_179 = local_ResourcesMined_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_179 = local_ResourcesMined_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Load(ref logic_SubGraph_SaveLoadBool_boolean_179, logic_SubGraph_SaveLoadBool_boolAsVariable_179, logic_SubGraph_SaveLoadBool_uniqueID_179);
	}

	private void Relay_Set_True_179()
	{
		logic_SubGraph_SaveLoadBool_boolean_179 = local_ResourcesMined_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_179 = local_ResourcesMined_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_179, logic_SubGraph_SaveLoadBool_boolAsVariable_179, logic_SubGraph_SaveLoadBool_uniqueID_179);
	}

	private void Relay_Set_False_179()
	{
		logic_SubGraph_SaveLoadBool_boolean_179 = local_ResourcesMined_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_179 = local_ResourcesMined_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_179.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_179, logic_SubGraph_SaveLoadBool_boolAsVariable_179, logic_SubGraph_SaveLoadBool_uniqueID_179);
	}

	private void Relay_Save_Out_182()
	{
	}

	private void Relay_Load_Out_182()
	{
		Relay_In_76();
	}

	private void Relay_Restart_Out_182()
	{
		Relay_False_43();
	}

	private void Relay_Save_182()
	{
		logic_SubGraph_SaveLoadBool_boolean_182 = local_MiningPhaseInit_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_182 = local_MiningPhaseInit_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Save(ref logic_SubGraph_SaveLoadBool_boolean_182, logic_SubGraph_SaveLoadBool_boolAsVariable_182, logic_SubGraph_SaveLoadBool_uniqueID_182);
	}

	private void Relay_Load_182()
	{
		logic_SubGraph_SaveLoadBool_boolean_182 = local_MiningPhaseInit_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_182 = local_MiningPhaseInit_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Load(ref logic_SubGraph_SaveLoadBool_boolean_182, logic_SubGraph_SaveLoadBool_boolAsVariable_182, logic_SubGraph_SaveLoadBool_uniqueID_182);
	}

	private void Relay_Set_True_182()
	{
		logic_SubGraph_SaveLoadBool_boolean_182 = local_MiningPhaseInit_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_182 = local_MiningPhaseInit_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_182, logic_SubGraph_SaveLoadBool_boolAsVariable_182, logic_SubGraph_SaveLoadBool_uniqueID_182);
	}

	private void Relay_Set_False_182()
	{
		logic_SubGraph_SaveLoadBool_boolean_182 = local_MiningPhaseInit_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_182 = local_MiningPhaseInit_MP_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_182.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_182, logic_SubGraph_SaveLoadBool_boolAsVariable_182, logic_SubGraph_SaveLoadBool_uniqueID_182);
	}

	private void Relay_In_184()
	{
		logic_uScriptCon_CompareBool_Bool_184 = _DEBUGEmulateMultiplayer;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184.In(logic_uScriptCon_CompareBool_Bool_184);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184.False;
		if (num)
		{
			Relay_In_171();
		}
		if (flag)
		{
			Relay_In_121();
		}
	}

	private void Relay_Save_Out_188()
	{
		Relay_Save_81();
	}

	private void Relay_Load_Out_188()
	{
		Relay_Load_81();
	}

	private void Relay_Restart_Out_188()
	{
		Relay_Set_False_81();
	}

	private void Relay_Save_188()
	{
		logic_SubGraph_SaveLoadInt_integer_188 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_188 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Save(logic_SubGraph_SaveLoadInt_restartValue_188, ref logic_SubGraph_SaveLoadInt_integer_188, logic_SubGraph_SaveLoadInt_intAsVariable_188, logic_SubGraph_SaveLoadInt_uniqueID_188);
	}

	private void Relay_Load_188()
	{
		logic_SubGraph_SaveLoadInt_integer_188 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_188 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Load(logic_SubGraph_SaveLoadInt_restartValue_188, ref logic_SubGraph_SaveLoadInt_integer_188, logic_SubGraph_SaveLoadInt_intAsVariable_188, logic_SubGraph_SaveLoadInt_uniqueID_188);
	}

	private void Relay_Restart_188()
	{
		logic_SubGraph_SaveLoadInt_integer_188 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_188 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Restart(logic_SubGraph_SaveLoadInt_restartValue_188, ref logic_SubGraph_SaveLoadInt_integer_188, logic_SubGraph_SaveLoadInt_intAsVariable_188, logic_SubGraph_SaveLoadInt_uniqueID_188);
	}

	private void Relay_Save_Out_190()
	{
		Relay_Save_188();
	}

	private void Relay_Load_Out_190()
	{
		Relay_Load_188();
	}

	private void Relay_Restart_Out_190()
	{
		Relay_Restart_188();
	}

	private void Relay_Save_190()
	{
		logic_SubGraph_SaveLoadInt_integer_190 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_190 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.Save(logic_SubGraph_SaveLoadInt_restartValue_190, ref logic_SubGraph_SaveLoadInt_integer_190, logic_SubGraph_SaveLoadInt_intAsVariable_190, logic_SubGraph_SaveLoadInt_uniqueID_190);
	}

	private void Relay_Load_190()
	{
		logic_SubGraph_SaveLoadInt_integer_190 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_190 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.Load(logic_SubGraph_SaveLoadInt_restartValue_190, ref logic_SubGraph_SaveLoadInt_integer_190, logic_SubGraph_SaveLoadInt_intAsVariable_190, logic_SubGraph_SaveLoadInt_uniqueID_190);
	}

	private void Relay_Restart_190()
	{
		logic_SubGraph_SaveLoadInt_integer_190 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_190 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_190.Restart(logic_SubGraph_SaveLoadInt_restartValue_190, ref logic_SubGraph_SaveLoadInt_integer_190, logic_SubGraph_SaveLoadInt_intAsVariable_190, logic_SubGraph_SaveLoadInt_uniqueID_190);
	}

	private void Relay_SetCount_192()
	{
		logic_uScript_SetQuestObjectiveCount_owner_192 = owner_Connection_193;
		logic_uScript_SetQuestObjectiveCount_currentCount_192 = local_CurrentAmount_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_192.SetCount(logic_uScript_SetQuestObjectiveCount_owner_192, logic_uScript_SetQuestObjectiveCount_objectiveId_192, logic_uScript_SetQuestObjectiveCount_currentCount_192);
	}

	private void Relay_In_194()
	{
		logic_uScriptCon_CompareBool_Bool_194 = _DEBUGEmulateMultiplayer;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.In(logic_uScriptCon_CompareBool_Bool_194);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.True)
		{
			Relay_In_201();
		}
	}

	private void Relay_In_195()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_195.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_195.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_195.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_194();
		}
		if (multiplayer)
		{
			Relay_In_201();
		}
	}

	private void Relay_Out_198()
	{
	}

	private void Relay_In_198()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_198 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_198.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_198, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_198);
	}

	private void Relay_In_200()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_201()
	{
		logic_uScriptCon_CompareInt_A_201 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_201.In(logic_uScriptCon_CompareInt_A_201, logic_uScriptCon_CompareInt_B_201);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_201.EqualTo)
		{
			Relay_SetCount_192();
		}
	}

	private void Relay_In_205()
	{
		logic_uScriptCon_CompareBool_Bool_205 = local_ResourcesMined_MP_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.In(logic_uScriptCon_CompareBool_Bool_205);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.False)
		{
			Relay_In_173();
		}
	}
}
