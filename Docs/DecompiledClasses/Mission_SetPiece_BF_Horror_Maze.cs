using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Stage1Init", "")]
public class Mission_SetPiece_BF_Horror_Maze : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string FlightHeightTrig = "";

	public SpawnTechData[] ForcefieldEntranceGroupSpawnData = new SpawnTechData[0];

	public SpawnTechData[] ForcefieldStage1GroupSpawnData = new SpawnTechData[0];

	public SpawnTechData[] ForcefieldStage2GroupSpawnData = new SpawnTechData[0];

	public SpawnTechData[] ForcefieldStage3GroupSpawnData = new SpawnTechData[0];

	public SpawnTechData[] ForcefieldStage4GroupSpawnData = new SpawnTechData[0];

	private Tank[] local_11_TankArray = new Tank[0];

	private Tank[] local_33_TankArray = new Tank[0];

	private Tank local_578_Tank;

	private bool local_BaseOnline_System_Boolean;

	private int local_CurrentObjective_System_Int32 = 1;

	private Tank local_curTank_Tank;

	private bool local_DialogueComplete_System_Boolean;

	private int local_DialogueProgress_System_Int32;

	private bool local_EndInit_System_Boolean;

	private Tank[] local_EntranceForcefields_TankArray = new Tank[0];

	private Tank local_FlyingTech_Tank;

	private Tank[] local_FlyingTechs_TankArray = new Tank[0];

	private bool local_MsgFlyingWarningShown_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgIntro_ManOnScreenMessages_OnScreenMessage;

	private bool local_MsgIntroShown_System_Boolean;

	private bool local_MsgIntroStarted_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgIntroWaiting_ManOnScreenMessages_OnScreenMessage;

	private bool local_MsgIntroWaitingShown_System_Boolean;

	private Tank[] local_NewlyEnteredTechs_TankArray = new Tank[0];

	private Tank[] local_NewlyExitedTechs_TankArray = new Tank[0];

	private Tank local_NPCTech_Tank;

	private Tank local_NPCTech02_Tank;

	private int local_Objective04SubStage_System_Int32 = 1;

	private bool local_Satege2Init_System_Boolean;

	private bool local_Satege3Init_System_Boolean;

	private Tank[] local_Stage1Forcefields_TankArray = new Tank[0];

	private Tank[] local_Stage2Forcefields_TankArray = new Tank[0];

	private Tank[] local_Stage3Forcefields_TankArray = new Tank[0];

	private int local_StartingHour_System_Int32;

	private bool local_TechsInit_System_Boolean;

	private Tank[] local_TechsInside_TankArray = new Tank[0];

	private Tank local_Terminal1Tech_Tank;

	private Tank local_Terminal2Tech_Tank;

	private Tank local_Terminal3Tech_Tank;

	private TankBlock local_TerminalBlock1_TankBlock;

	private TankBlock local_TerminalBlock2_TankBlock;

	private TankBlock local_TerminalBlock3_TankBlock;

	private Tank[] local_Terminals1_TankArray = new Tank[0];

	private Tank[] local_Terminals2_TankArray = new Tank[0];

	private Tank[] local_Terminals3_TankArray = new Tank[0];

	[Multiline(3)]
	public string MissionRangeTrig = "";

	public uScript_PlayDialogue.Dialogue msgEpilogue;

	public uScript_AddMessage.MessageData msgFlyingWarning;

	public uScript_AddMessage.MessageData msgIntro;

	public uScript_AddMessage.MessageData MsgIntroWaitingForPlayers;

	public uScript_AddMessage.MessageData msgOutro;

	public uScript_AddMessage.MessageData msgOverheadKillHitBubl;

	public uScript_AddMessage.MessageData msgOverheadKillHitHubl;

	public uScript_PlayDialogue.Dialogue msgStage1Complete;

	public uScript_PlayDialogue.Dialogue msgStage2Complete;

	public uScript_PlayDialogue.Dialogue msgStage3Complete;

	[Multiline(3)]
	public string NotMissionRangeTrig1 = "";

	[Multiline(3)]
	public string NotMissionRangeTrig2 = "";

	[Multiline(3)]
	public string NotMissionRangeTrig3 = "";

	public Transform NPCDespawnEffect;

	public SpawnTechData[] NPCSpawnData01 = new SpawnTechData[0];

	public SpawnTechData[] NPCSpawnData02 = new SpawnTechData[0];

	[Multiline(3)]
	public string NPCTriggerVolume01 = "";

	[Multiline(3)]
	public string OverheadKillVolume1 = "";

	public uScript_AddMessage.MessageSpeaker SpeakerBubl;

	public uScript_AddMessage.MessageSpeaker SpeakerHubl;

	public SpawnTechData[] Stage1_Enemy01_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage1_Enemy01_Kill_Trig = "";

	[Multiline(3)]
	public string Stage1_Enemy01_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage1_Enemy01_Spawn_Trig = "";

	public SpawnTechData[] Stage1_Enemy02_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage1_Enemy02_Kill_Trig = "";

	[Multiline(3)]
	public string Stage1_Enemy02_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage1_Enemy02_Spawn_Trig = "";

	public SpawnTechData[] Stage1_Enemy03_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage1_Enemy03_Kill_Trig = "";

	[Multiline(3)]
	public string Stage1_Enemy03_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage1_Enemy03_Spawn_Trig = "";

	public SpawnTechData[] Stage1_Enemy04_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage1_Enemy04_Kill_Trig = "";

	[Multiline(3)]
	public string Stage1_Enemy04_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage1_Enemy04_Spawn_Trig = "";

	[Multiline(3)]
	public string Stage1GoalTriggerVolume = "";

	public SpawnTechData[] Stage2_Enemy01_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage2_Enemy01_Kill_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy01_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy01_Spawn_Trig = "";

	public SpawnTechData[] Stage2_Enemy02_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage2_Enemy02_Kill_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy02_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy02_Spawn_Trig = "";

	public SpawnTechData[] Stage2_Enemy03_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage2_Enemy03_Kill_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy03_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy03_Spawn_Trig = "";

	public SpawnTechData[] Stage2_Enemy04_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage2_Enemy04_Kill_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy04_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy04_Spawn_Trig = "";

	public SpawnTechData[] Stage2_Enemy05_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage2_Enemy05_Kill_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy05_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy05_Spawn_Trig = "";

	public SpawnTechData[] Stage2_Enemy06_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage2_Enemy06_Kill_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy06_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy06_Spawn_Trig = "";

	public SpawnTechData[] Stage2_Enemy07_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage2_Enemy07_Kill_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy07_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy07_Spawn_Trig = "";

	public SpawnTechData[] Stage2_Enemy08_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage2_Enemy08_Kill_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy08_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy08_Spawn_Trig = "";

	public SpawnTechData[] Stage2_Enemy09_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage2_Enemy09_Kill_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy09_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy09_Spawn_Trig = "";

	public SpawnTechData[] Stage2_Enemy10_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage2_Enemy10_Kill_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy10_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage2_Enemy10_Spawn_Trig = "";

	public SpawnTechData[] Stage2_Enemy11_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage2GoalTriggerVolume = "";

	public SpawnTechData[] Stage3_Enemy01_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy01_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy01_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy01_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy02_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy02_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy02_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy02_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy03_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy03_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy03_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy03_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy04_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy04_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy04_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy04_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy05_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy05_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy05_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy05_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy06_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy06_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy06_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy06_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy07_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy07_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy07_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy07_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy08_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy08_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy08_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy08_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy09_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy09_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy09_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy09_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy10_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy10_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy10_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy10_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy11_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy11_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy11_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy11_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy12_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy12_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy12_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy12_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy13_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy13_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy13_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy13_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy14_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy14_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy14_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy14_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy15_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy15_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy15_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy15_Spawn_Trig = "";

	public SpawnTechData[] Stage3_Enemy16_Data = new SpawnTechData[0];

	[Multiline(3)]
	public string Stage3_Enemy16_Kill_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy16_PreSpawn_Trig = "";

	[Multiline(3)]
	public string Stage3_Enemy16_Spawn_Trig = "";

	[Multiline(3)]
	public string Stage3GoalTriggerVolume = "";

	public BlockTypes Terminal_Block;

	public SpawnTechData[] Terminal1SpawnData = new SpawnTechData[0];

	public SpawnTechData[] Terminal2SpawnData = new SpawnTechData[0];

	public SpawnTechData[] Terminal3SpawnData = new SpawnTechData[0];

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_26;

	private GameObject owner_Connection_35;

	private GameObject owner_Connection_36;

	private GameObject owner_Connection_57;

	private GameObject owner_Connection_61;

	private GameObject owner_Connection_63;

	private GameObject owner_Connection_66;

	private GameObject owner_Connection_108;

	private GameObject owner_Connection_112;

	private GameObject owner_Connection_115;

	private GameObject owner_Connection_118;

	private GameObject owner_Connection_119;

	private GameObject owner_Connection_120;

	private GameObject owner_Connection_302;

	private GameObject owner_Connection_304;

	private GameObject owner_Connection_309;

	private GameObject owner_Connection_315;

	private GameObject owner_Connection_332;

	private GameObject owner_Connection_342;

	private GameObject owner_Connection_358;

	private GameObject owner_Connection_361;

	private GameObject owner_Connection_363;

	private GameObject owner_Connection_380;

	private GameObject owner_Connection_387;

	private GameObject owner_Connection_390;

	private GameObject owner_Connection_398;

	private GameObject owner_Connection_400;

	private GameObject owner_Connection_458;

	private GameObject owner_Connection_509;

	private GameObject owner_Connection_563;

	private GameObject owner_Connection_577;

	private GameObject owner_Connection_643;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_4;

	private bool logic_uScriptCon_CompareBool_True_4 = true;

	private bool logic_uScriptCon_CompareBool_False_4 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_5 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_5;

	private bool logic_uScriptAct_SetBool_Out_5 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_5 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_5 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_7;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_7 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_7 = "TechsInit";

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_9;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_13 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_13 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_13;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_13 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_13;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_13 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_13 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_13 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_13 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_15 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_15;

	private object logic_uScript_SetEncounterTarget_visibleObject_15 = "";

	private bool logic_uScript_SetEncounterTarget_Out_15 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_19 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_19 = new Tank[0];

	private int logic_uScript_AccessListTech_index_19;

	private Tank logic_uScript_AccessListTech_value_19;

	private bool logic_uScript_AccessListTech_Out_19 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_21 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_22 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_22;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_22 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_22 = "CurrentObjective";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_24;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_27 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_27;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_27 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_27 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_27 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_28 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_28 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_30 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_30;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_30;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_38 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_38;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_38;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_38;

	private bool logic_uScript_AddMessage_Out_38 = true;

	private bool logic_uScript_AddMessage_Shown_38 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_40 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_40;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_40 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_40;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_40 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_40 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_40 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_40 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_41 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_41;

	private bool logic_uScript_FinishEncounter_Out_41 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_43 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_43 = new Tank[0];

	private int logic_uScript_AccessListTech_index_43;

	private Tank logic_uScript_AccessListTech_value_43;

	private bool logic_uScript_AccessListTech_Out_43 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_44 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_44;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_44;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_44;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_44;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_44;

	private bool logic_uScript_FlyTechUpAndAway_Out_44 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_45 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_45 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_45 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_45 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_45 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_45 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_45 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_50 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_50 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_50 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_50 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_50 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_50 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_50 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_53;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_56 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_56;

	private int logic_uScriptAct_AddInt_v2_B_56 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_56;

	private float logic_uScriptAct_AddInt_v2_FloatResult_56;

	private bool logic_uScriptAct_AddInt_v2_Out_56 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_58 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_58;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_58 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_60 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_60 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_60;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_60;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_60;

	private bool logic_uScript_SpawnTechsFromData_Out_60 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_62 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_62 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_62;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_62;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_62;

	private bool logic_uScript_SpawnTechsFromData_Out_62 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_65 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_65 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_65;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_65;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_65;

	private bool logic_uScript_SpawnTechsFromData_Out_65 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_70 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_70 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_70 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_70 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_70 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_70 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_70 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_71 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_71;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_71;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_74 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_74;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_74;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_76 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_76 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_76 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_76 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_76 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_76 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_76 = true;

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_81 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_81 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_81 = new Tank[0];

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_83 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_83 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_83 = new Tank[0];

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_86 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_86 = true;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_90 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_90 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_90 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_90 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_90 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_90 = true;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_95 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_95 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_95 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_95 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_95 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_95;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_97 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_97 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_97 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_97 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_97 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_97;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_104 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_104 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_104 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_104 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_104 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_104 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_109 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_109 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_109;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_109;

	private bool logic_uScript_DestroyTechsFromData_Out_109 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_110 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_110 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_110;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_110;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_110;

	private bool logic_uScript_SpawnTechsFromData_Out_110 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_113 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_113 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_113;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_113 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_113;

	private bool logic_uScript_SpawnTechsFromData_Out_113 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_114 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_114 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_114;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_114;

	private bool logic_uScript_DestroyTechsFromData_Out_114 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_122 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_122 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_122;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_122;

	private bool logic_uScript_DestroyTechsFromData_Out_122 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_123 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_123 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_123;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_123;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_123;

	private bool logic_uScript_SpawnTechsFromData_Out_123 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_124 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_124;

	private bool logic_uScriptAct_SetBool_Out_124 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_124 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_124 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_126;

	private bool logic_uScriptCon_CompareBool_True_126 = true;

	private bool logic_uScriptCon_CompareBool_False_126 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_129;

	private bool logic_uScriptCon_CompareBool_True_129 = true;

	private bool logic_uScriptCon_CompareBool_False_129 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_132 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_132;

	private bool logic_uScriptAct_SetBool_Out_132 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_132 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_132 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_133 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_133;

	private bool logic_uScriptCon_CompareBool_True_133 = true;

	private bool logic_uScriptCon_CompareBool_False_133 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_135 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_135;

	private bool logic_uScriptAct_SetBool_Out_135 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_135 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_135 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_137 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_137 = true;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_138 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_138 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_138 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_138 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_138 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_138 = true;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_147 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_147 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_147 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_147 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_147 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_147 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_151;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_151 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_151 = "Satege2Init";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_152;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_152 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_152 = "Satege3Init";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_153;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_153 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_153 = "EndInit";

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_157 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_157 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_157 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_157 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_157 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_157;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_161 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_161 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_161 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_161 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_161 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_161 = true;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_166 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_166 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_166 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_166 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_166 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_166;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_171 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_171 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_171 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_171 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_171 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_171;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_178 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_178 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_178 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_178 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_178 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_178 = true;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_182 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_182 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_182 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_182 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_182 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_182 = true;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_187 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_187 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_187 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_187 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_187 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_187;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_191 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_191 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_191 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_191 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_191 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_191;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_197 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_197 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_197 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_197 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_197 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_197;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_199 = true;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_203 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_203 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_203 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_203 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_203 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_203;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_205 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_205 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_205 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_205 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_205 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_205;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_214 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_214 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_214 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_214 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_214 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_214;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_216 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_216 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_216 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_216 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_216 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_216;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_223 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_223 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_223 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_223 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_223 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_223;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_225 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_225 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_225 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_225 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_225 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_225;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_234 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_234 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_234 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_234 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_234 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_234;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_236 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_236 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_236 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_236 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_236 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_236;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_243 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_243 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_243 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_243 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_243 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_243;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_249 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_249 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_249 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_249 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_249 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_249;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_254 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_254 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_254 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_254 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_254 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_254;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_255 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_255 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_255 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_255 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_255 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_255;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_261 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_261 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_261 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_261 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_261 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_261;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_266 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_266 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_266 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_266 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_266 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_266;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_271 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_271 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_271 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_271 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_271 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_271;

	private SubGraph_GhostTechController logic_SubGraph_GhostTechController_SubGraph_GhostTechController_275 = new SubGraph_GhostTechController();

	private string logic_SubGraph_GhostTechController_preSpawnTrigger_275 = "";

	private string logic_SubGraph_GhostTechController_ghostSpawnTrigger_275 = "";

	private string logic_SubGraph_GhostTechController_ghostKillTrigger_275 = "";

	private SpawnTechData[] logic_SubGraph_GhostTechController_ghostTechSpawnData_275 = new SpawnTechData[0];

	private bool logic_SubGraph_GhostTechController_UsePreSpawnTrigger_275;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_282 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_282 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_282 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_282;

	private float logic_uScript_DamageTechs_leaveBlksPercent_282 = 100f;

	private bool logic_uScript_DamageTechs_makeVulnerable_282 = true;

	private bool logic_uScript_DamageTechs_Out_282 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_283 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_283 = true;

	private uScript_SetDangerMusicMisc logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_284 = new uScript_SetDangerMusicMisc();

	private ManMusic.MiscDangerMusicType logic_uScript_SetDangerMusicMisc_miscDangerMusicType_284;

	private Tank logic_uScript_SetDangerMusicMisc_tech_284;

	private bool logic_uScript_SetDangerMusicMisc_Out_284 = true;

	private uScript_SetDangerMusicMisc logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_285 = new uScript_SetDangerMusicMisc();

	private ManMusic.MiscDangerMusicType logic_uScript_SetDangerMusicMisc_miscDangerMusicType_285 = ManMusic.MiscDangerMusicType.Halloween;

	private Tank logic_uScript_SetDangerMusicMisc_tech_285;

	private bool logic_uScript_SetDangerMusicMisc_Out_285 = true;

	private uScript_GetTechsInTrigger logic_uScript_GetTechsInTrigger_uScript_GetTechsInTrigger_286 = new uScript_GetTechsInTrigger();

	private string logic_uScript_GetTechsInTrigger_triggerAreaName_286 = "";

	private Tank[] logic_uScript_GetTechsInTrigger_Entered_286;

	private Tank[] logic_uScript_GetTechsInTrigger_Inside_286;

	private Tank[] logic_uScript_GetTechsInTrigger_Exited_286;

	private bool logic_uScript_GetTechsInTrigger_Out_286 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_288 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_288;

	private Tank logic_uScript_SetTimeOfDay_tech_288;

	private bool logic_uScript_SetTimeOfDay_Out_288 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_289 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_289 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_289;

	private int logic_uScript_ForEachListTech_currentIndex_289;

	private bool logic_uScript_ForEachListTech_Immediate_289 = true;

	private bool logic_uScript_ForEachListTech_Done_289 = true;

	private bool logic_uScript_ForEachListTech_Iteration_289 = true;

	private uScript_OverrideTankCameraDistanceMax logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_290 = new uScript_OverrideTankCameraDistanceMax();

	private bool logic_uScript_OverrideTankCameraDistanceMax_enable_290 = true;

	private float logic_uScript_OverrideTankCameraDistanceMax_newDistanceMax_290 = 7f;

	private Tank logic_uScript_OverrideTankCameraDistanceMax_tech_290;

	private bool logic_uScript_OverrideTankCameraDistanceMax_Out_290 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_295 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_295 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_295;

	private int logic_uScript_ForEachListTech_currentIndex_295;

	private bool logic_uScript_ForEachListTech_Immediate_295 = true;

	private bool logic_uScript_ForEachListTech_Done_295 = true;

	private bool logic_uScript_ForEachListTech_Iteration_295 = true;

	private uScript_OverrideTankCameraDistanceMax logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_297 = new uScript_OverrideTankCameraDistanceMax();

	private bool logic_uScript_OverrideTankCameraDistanceMax_enable_297;

	private float logic_uScript_OverrideTankCameraDistanceMax_newDistanceMax_297 = 60f;

	private Tank logic_uScript_OverrideTankCameraDistanceMax_tech_297;

	private bool logic_uScript_OverrideTankCameraDistanceMax_Out_297 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_298 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_298 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_303 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_303 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_303;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_303;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_303;

	private bool logic_uScript_SpawnTechsFromData_Out_303 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_305 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_305 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_305;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_305;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_305;

	private bool logic_uScript_SpawnTechsFromData_Out_305 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_307 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_307 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_307;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_307;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_307;

	private bool logic_uScript_SpawnTechsFromData_Out_307 = true;

	private uScript_SetBlockAnimationTrigger logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_313 = new uScript_SetBlockAnimationTrigger();

	private TankBlock logic_uScript_SetBlockAnimationTrigger_block_313;

	private string logic_uScript_SetBlockAnimationTrigger_name_313 = "Operating";

	private bool logic_uScript_SetBlockAnimationTrigger_Out_313 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_314 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_314 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_314;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_314 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_314;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_314 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_314 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_314 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_314 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_317 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_317 = new Tank[0];

	private int logic_uScript_AccessListTech_index_317;

	private Tank logic_uScript_AccessListTech_value_317;

	private bool logic_uScript_AccessListTech_Out_317 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_319 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_319;

	private BlockTypes logic_uScript_GetTankBlock_blockType_319;

	private TankBlock logic_uScript_GetTankBlock_Return_319;

	private bool logic_uScript_GetTankBlock_Out_319 = true;

	private bool logic_uScript_GetTankBlock_Returned_319 = true;

	private bool logic_uScript_GetTankBlock_NotFound_319 = true;

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_325 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_325 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_325 = new Tank[0];

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_326 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_326 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_326 = new Tank[0];

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_327 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_327 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_327 = new Tank[0];

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_331 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_331 = new Tank[0];

	private int logic_uScript_AccessListTech_index_331;

	private Tank logic_uScript_AccessListTech_value_331;

	private bool logic_uScript_AccessListTech_Out_331 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_334 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_334;

	private BlockTypes logic_uScript_GetTankBlock_blockType_334;

	private TankBlock logic_uScript_GetTankBlock_Return_334;

	private bool logic_uScript_GetTankBlock_Out_334 = true;

	private bool logic_uScript_GetTankBlock_Returned_334 = true;

	private bool logic_uScript_GetTankBlock_NotFound_334 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_337 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_337 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_337;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_337 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_337;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_337 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_337 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_337 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_337 = true;

	private uScript_SetBlockAnimationTrigger logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_339 = new uScript_SetBlockAnimationTrigger();

	private TankBlock logic_uScript_SetBlockAnimationTrigger_block_339;

	private string logic_uScript_SetBlockAnimationTrigger_name_339 = "Operating";

	private bool logic_uScript_SetBlockAnimationTrigger_Out_339 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_344 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_344 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_344;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_344 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_344;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_344 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_344 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_344 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_344 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_345 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_345;

	private BlockTypes logic_uScript_GetTankBlock_blockType_345;

	private TankBlock logic_uScript_GetTankBlock_Return_345;

	private bool logic_uScript_GetTankBlock_Out_345 = true;

	private bool logic_uScript_GetTankBlock_Returned_345 = true;

	private bool logic_uScript_GetTankBlock_NotFound_345 = true;

	private uScript_SetBlockAnimationTrigger logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_348 = new uScript_SetBlockAnimationTrigger();

	private TankBlock logic_uScript_SetBlockAnimationTrigger_block_348;

	private string logic_uScript_SetBlockAnimationTrigger_name_348 = "Operating";

	private bool logic_uScript_SetBlockAnimationTrigger_Out_348 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_349 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_349 = new Tank[0];

	private int logic_uScript_AccessListTech_index_349;

	private Tank logic_uScript_AccessListTech_value_349;

	private bool logic_uScript_AccessListTech_Out_349 = true;

	private uScript_SetDangerMusicMisc logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_350 = new uScript_SetDangerMusicMisc();

	private ManMusic.MiscDangerMusicType logic_uScript_SetDangerMusicMisc_miscDangerMusicType_350;

	private Tank logic_uScript_SetDangerMusicMisc_tech_350;

	private bool logic_uScript_SetDangerMusicMisc_Out_350 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_352 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_352 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_352;

	private int logic_uScript_ForEachListTech_currentIndex_352;

	private bool logic_uScript_ForEachListTech_Immediate_352 = true;

	private bool logic_uScript_ForEachListTech_Done_352 = true;

	private bool logic_uScript_ForEachListTech_Iteration_352 = true;

	private uScript_OverrideTankCameraDistanceMax logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_354 = new uScript_OverrideTankCameraDistanceMax();

	private bool logic_uScript_OverrideTankCameraDistanceMax_enable_354;

	private float logic_uScript_OverrideTankCameraDistanceMax_newDistanceMax_354 = 60f;

	private Tank logic_uScript_OverrideTankCameraDistanceMax_tech_354;

	private bool logic_uScript_OverrideTankCameraDistanceMax_Out_354 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_359 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_359 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_359;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_359;

	private bool logic_uScript_DestroyTechsFromData_Out_359 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_360 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_360 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_360;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_360;

	private bool logic_uScript_DestroyTechsFromData_Out_360 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_362 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_362 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_362;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_362;

	private bool logic_uScript_DestroyTechsFromData_Out_362 = true;

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_366 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_366 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_366 = new Tank[0];

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_368 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_368 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_368 = new Tank[0];

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_370 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_370 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_370 = new Tank[0];

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_372 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_372 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_372 = new Tank[0];

	private uScript_GetTimeOfDay logic_uScript_GetTimeOfDay_uScript_GetTimeOfDay_377 = new uScript_GetTimeOfDay();

	private int logic_uScript_GetTimeOfDay_Return_377;

	private bool logic_uScript_GetTimeOfDay_Out_377 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_379;

	private int logic_SubGraph_SaveLoadInt_integer_379;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_379 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_379 = "StartingHour";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_381 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_381 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_381;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_381;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_381;

	private bool logic_uScript_SpawnTechsFromData_Out_381 = true;

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_385 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_385 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_385 = new Tank[0];

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_386 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_386 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_386;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_386;

	private bool logic_uScript_DestroyTechsFromData_Out_386 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_389 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_389;

	private bool logic_uScript_ClearEncounterTarget_Out_389 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_392 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_392 = 4f;

	private bool logic_uScript_Wait_repeat_392;

	private bool logic_uScript_Wait_Waited_392 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_393 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_393 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_393 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_395 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_395 = 4f;

	private bool logic_uScript_Wait_repeat_395;

	private bool logic_uScript_Wait_Waited_395 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_396 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_396 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_396 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_397 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_397;

	private bool logic_uScript_ClearEncounterTarget_Out_397 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_399 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_399;

	private bool logic_uScript_ClearEncounterTarget_Out_399 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_403;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_403 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_403 = "DialogueComplete";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_404;

	private int logic_SubGraph_SaveLoadInt_integer_404;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_404 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_404 = "StartingHour";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_407 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_407;

	private bool logic_uScriptAct_SetBool_Out_407 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_407 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_407 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_409 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_409;

	private int logic_uScript_PlayDialogue_progress_409;

	private bool logic_uScript_PlayDialogue_Out_409 = true;

	private bool logic_uScript_PlayDialogue_Shown_409 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_409 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_410 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_410 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_411 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_411;

	private bool logic_uScriptCon_CompareBool_True_411 = true;

	private bool logic_uScriptCon_CompareBool_False_411 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_413 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_413;

	private bool logic_uScriptAct_SetBool_Out_413 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_413 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_413 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_415 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_415;

	private bool logic_uScriptCon_CompareBool_True_415 = true;

	private bool logic_uScriptCon_CompareBool_False_415 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_416 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_416 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_419 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_419;

	private int logic_uScript_PlayDialogue_progress_419;

	private bool logic_uScript_PlayDialogue_Out_419 = true;

	private bool logic_uScript_PlayDialogue_Shown_419 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_419 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_421 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_421;

	private int logic_uScriptAct_AddInt_v2_B_421 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_421;

	private float logic_uScriptAct_AddInt_v2_FloatResult_421;

	private bool logic_uScriptAct_AddInt_v2_Out_421 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_423 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_423;

	private bool logic_uScriptAct_SetBool_Out_423 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_423 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_423 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_425 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_425;

	private bool logic_uScriptAct_SetBool_Out_425 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_425 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_425 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_427 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_427;

	private bool logic_uScriptAct_SetBool_Out_427 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_427 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_427 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_429 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_429 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_430 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_430;

	private int logic_uScript_PlayDialogue_progress_430;

	private bool logic_uScript_PlayDialogue_Out_430 = true;

	private bool logic_uScript_PlayDialogue_Shown_430 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_430 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_434 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_434;

	private bool logic_uScriptAct_SetBool_Out_434 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_434 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_434 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_436 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_436;

	private bool logic_uScriptCon_CompareBool_True_436 = true;

	private bool logic_uScriptCon_CompareBool_False_436 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_437 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_437;

	private int logic_uScript_SetTankTeam_team_437 = 1;

	private bool logic_uScript_SetTankTeam_Out_437 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_439 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_439;

	private bool logic_uScriptAct_SetBool_Out_439 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_439 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_439 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_441 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_441 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_442;

	private bool logic_uScriptCon_CompareBool_True_442 = true;

	private bool logic_uScriptCon_CompareBool_False_442 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_444 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_444;

	private bool logic_uScriptAct_SetBool_Out_444 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_444 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_444 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_448 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_448;

	private int logic_uScript_PlayDialogue_progress_448;

	private bool logic_uScript_PlayDialogue_Out_448 = true;

	private bool logic_uScript_PlayDialogue_Shown_448 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_448 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_449 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_449 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_450 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_450;

	private int logic_uScriptAct_SetInt_Target_450;

	private bool logic_uScriptAct_SetInt_Out_450 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_452 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_452;

	private int logic_uScriptAct_SetInt_Target_452;

	private bool logic_uScriptAct_SetInt_Out_452 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_454 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_454;

	private int logic_uScriptAct_SetInt_Target_454;

	private bool logic_uScriptAct_SetInt_Out_454 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_457 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_457;

	private int logic_uScriptAct_SetInt_Target_457;

	private bool logic_uScriptAct_SetInt_Out_457 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_459 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_459;

	private object logic_uScript_SetEncounterTarget_visibleObject_459 = "";

	private bool logic_uScript_SetEncounterTarget_Out_459 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_461 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_461 = 4f;

	private bool logic_uScript_Wait_repeat_461;

	private bool logic_uScript_Wait_Waited_461 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_465 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_465 = true;

	private uScript_IsTechWheelGrounded logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_466 = new uScript_IsTechWheelGrounded();

	private Tank logic_uScript_IsTechWheelGrounded_tech_466;

	private bool logic_uScript_IsTechWheelGrounded_Out_466 = true;

	private bool logic_uScript_IsTechWheelGrounded_True_466 = true;

	private bool logic_uScript_IsTechWheelGrounded_False_466 = true;

	private uScript_IsTechTouchingTerrain logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_467 = new uScript_IsTechTouchingTerrain();

	private Tank logic_uScript_IsTechTouchingTerrain_tech_467;

	private bool logic_uScript_IsTechTouchingTerrain_Out_467 = true;

	private bool logic_uScript_IsTechTouchingTerrain_True_467 = true;

	private bool logic_uScript_IsTechTouchingTerrain_False_467 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_468 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_468 = new Tank[0];

	private int logic_uScript_AccessListTech_index_468;

	private Tank logic_uScript_AccessListTech_value_468;

	private bool logic_uScript_AccessListTech_Out_468 = true;

	private uScript_IsTechInTrigger logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_469 = new uScript_IsTechInTrigger();

	private string logic_uScript_IsTechInTrigger_triggerAreaName_469 = "";

	private Tank[] logic_uScript_IsTechInTrigger_techs_469 = new Tank[0];

	private bool logic_uScript_IsTechInTrigger_Out_469 = true;

	private bool logic_uScript_IsTechInTrigger_InRange_469 = true;

	private bool logic_uScript_IsTechInTrigger_OutOfRange_469 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_470 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_470 = true;

	private uScript_IsTechAnchored logic_uScript_IsTechAnchored_uScript_IsTechAnchored_471 = new uScript_IsTechAnchored();

	private Tank logic_uScript_IsTechAnchored_tech_471;

	private bool logic_uScript_IsTechAnchored_Out_471 = true;

	private bool logic_uScript_IsTechAnchored_True_471 = true;

	private bool logic_uScript_IsTechAnchored_False_471 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_472 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_472 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_475 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_475;

	private bool logic_uScriptCon_CompareBool_True_475 = true;

	private bool logic_uScriptCon_CompareBool_False_475 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_476 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_476;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_476;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_476;

	private bool logic_uScript_AddMessage_Out_476 = true;

	private bool logic_uScript_AddMessage_Shown_476 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_478 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_478;

	private bool logic_uScriptAct_SetBool_Out_478 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_478 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_478 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_480 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_480 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_481 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_481 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_482 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_482 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_483 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_483 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_485;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_485 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_485 = "MsgFlyingWarningShown";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_486 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_486 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_487 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_487;

	private bool logic_uScriptAct_SetBool_Out_487 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_487 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_487 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_490 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_490;

	private bool logic_uScriptCon_CompareBool_True_490 = true;

	private bool logic_uScriptCon_CompareBool_False_490 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_491 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_492 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_492 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_494;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_494 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_494 = "BaseOnline";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_496 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_496;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_496 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_496 = "Objective04SubStage";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_498 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_498;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_498;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_498;

	private bool logic_uScript_AddMessage_Out_498 = true;

	private bool logic_uScript_AddMessage_Shown_498 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_500 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_500;

	private int logic_uScriptCon_CompareInt_B_500 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_500 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_500 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_500 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_500 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_500 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_500 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_504 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_504;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_504;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_504;

	private bool logic_uScript_AddMessage_Out_504 = true;

	private bool logic_uScript_AddMessage_Shown_504 = true;

	private uScript_GetTechsInTrigger logic_uScript_GetTechsInTrigger_uScript_GetTechsInTrigger_506 = new uScript_GetTechsInTrigger();

	private string logic_uScript_GetTechsInTrigger_triggerAreaName_506 = "";

	private Tank[] logic_uScript_GetTechsInTrigger_Entered_506;

	private Tank[] logic_uScript_GetTechsInTrigger_Inside_506;

	private Tank[] logic_uScript_GetTechsInTrigger_Exited_506;

	private bool logic_uScript_GetTechsInTrigger_Out_506 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_507 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_507 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_507 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_507 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_507 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_507 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_507 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_508 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_508;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_508 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_510 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_510 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_510 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_511 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_511 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_511;

	private bool logic_uScript_SetTankInvulnerable_Out_511 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_512 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_512;

	private Tank logic_uScript_SetTankInvulnerable_tank_512;

	private bool logic_uScript_SetTankInvulnerable_Out_512 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_514 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_514 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_514 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_514;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_514 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_514 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_514 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_514 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_514 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_514 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_514 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_516 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_516 = 4;

	private Tank logic_uScript_SetTimeOfDay_tech_516;

	private bool logic_uScript_SetTimeOfDay_Out_516 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_518 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_518 = 12;

	private Tank logic_uScript_SetTimeOfDay_tech_518;

	private bool logic_uScript_SetTimeOfDay_Out_518 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_519 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_519 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_519 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_519;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_519 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_519 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_519 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_519 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_519 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_519 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_519 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_521 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_521 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_521 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_521;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_521 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_521 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_521 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_521 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_521 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_521 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_521 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_525 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_525 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_525 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_525 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_525 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_525 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_525 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_526 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_526 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_527 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_527 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_528 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_528 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_528 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_528 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_528 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_528 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_528 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_529 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_529 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_529 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_529 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_529 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_529 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_529 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_530 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_530 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_531 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_531 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_532 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_532 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_533 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_533 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_534 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_534 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_534 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_534 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_534 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_534 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_534 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_535 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_535 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_535 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_535 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_535 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_535 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_535 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_536 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_536 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_536 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_536 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_536 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_536 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_536 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_537 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_537 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_541 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_541 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_545 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_545;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_545;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_546 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_546 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_546 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_546 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_546 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_546 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_546 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_549 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_549;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_549;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_549;

	private bool logic_uScript_AddMessage_Out_549 = true;

	private bool logic_uScript_AddMessage_Shown_549 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_550 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_550;

	private bool logic_uScriptCon_CompareBool_True_550 = true;

	private bool logic_uScriptCon_CompareBool_False_550 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_551 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_551;

	private bool logic_uScriptCon_CompareBool_True_551 = true;

	private bool logic_uScriptCon_CompareBool_False_551 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_552 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_552;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_552;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_552;

	private bool logic_uScript_AddMessage_Out_552 = true;

	private bool logic_uScript_AddMessage_Shown_552 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_553 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_553 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_556 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_556 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_556 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_559 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_559;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_559;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_559;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_559;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_559;

	private bool logic_uScript_FlyTechUpAndAway_Out_559 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_560 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_560;

	private bool logic_uScriptAct_SetBool_Out_560 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_560 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_560 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_561 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_561;

	private bool logic_uScriptAct_SetBool_Out_561 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_561 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_561 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_565 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_565 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_567 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_567;

	private bool logic_uScript_RemoveOnScreenMessage_instant_567;

	private bool logic_uScript_RemoveOnScreenMessage_Out_567 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_570 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_570 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_570;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_570;

	private bool logic_uScript_DestroyTechsFromData_Out_570 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_574 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_574 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_574 = true;

	private uScript_OverrideTankCameraDistanceMax logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_579 = new uScript_OverrideTankCameraDistanceMax();

	private bool logic_uScript_OverrideTankCameraDistanceMax_enable_579;

	private float logic_uScript_OverrideTankCameraDistanceMax_newDistanceMax_579 = 60f;

	private Tank logic_uScript_OverrideTankCameraDistanceMax_tech_579;

	private bool logic_uScript_OverrideTankCameraDistanceMax_Out_579 = true;

	private uScript_SetDangerMusicMisc logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_580 = new uScript_SetDangerMusicMisc();

	private ManMusic.MiscDangerMusicType logic_uScript_SetDangerMusicMisc_miscDangerMusicType_580;

	private Tank logic_uScript_SetDangerMusicMisc_tech_580;

	private bool logic_uScript_SetDangerMusicMisc_Out_580 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_581 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_581;

	private int logic_uScriptCon_CompareInt_B_581 = 1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_581 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_581 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_581 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_581 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_581 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_581 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_584 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_584;

	private Tank logic_uScript_SetTimeOfDay_tech_584;

	private bool logic_uScript_SetTimeOfDay_Out_584 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_586 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_586;

	private int logic_uScriptCon_CompareInt_B_586 = 4;

	private bool logic_uScriptCon_CompareInt_GreaterThan_586 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_586 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_586 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_586 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_586 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_586 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_587 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_587 = 4;

	private Tank logic_uScript_SetTimeOfDay_tech_587;

	private bool logic_uScript_SetTimeOfDay_Out_587 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_588 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_588 = 12;

	private Tank logic_uScript_SetTimeOfDay_tech_588;

	private bool logic_uScript_SetTimeOfDay_Out_588 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_590 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_590;

	private bool logic_uScriptCon_CompareBool_True_590 = true;

	private bool logic_uScriptCon_CompareBool_False_590 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_592 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_592 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_592 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_592 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_592 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_592 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_592 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_593 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_593;

	private bool logic_uScriptCon_CompareBool_True_593 = true;

	private bool logic_uScriptCon_CompareBool_False_593 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_595 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_595;

	private bool logic_uScriptAct_SetBool_Out_595 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_595 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_595 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_597 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_597;

	private bool logic_uScriptAct_SetBool_Out_597 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_597 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_597 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_602 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_602;

	private bool logic_uScriptAct_SetBool_Out_602 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_602 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_602 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_603 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_603;

	private bool logic_uScriptAct_SetBool_Out_603 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_603 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_603 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_606 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_606;

	private bool logic_uScriptAct_SetBool_Out_606 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_606 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_606 = true;

	private SubGraph_KillEmAll_11 logic_SubGraph_KillEmAll_11_SubGraph_KillEmAll_11_638 = new SubGraph_KillEmAll_11();

	private SpawnTechData[] logic_SubGraph_KillEmAll_11_ghostTechSpawnData01_638 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_11_ghostTechSpawnData02_638 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_11_ghostTechSpawnData03_638 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_11_ghostTechSpawnData04_638 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_11_ghostTechSpawnData05_638 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_11_ghostTechSpawnData06_638 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_11_ghostTechSpawnData07_638 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_11_ghostTechSpawnData08_638 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_11_ghostTechSpawnData09_638 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_11_ghostTechSpawnData10_638 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_11_ghostTechSpawnData11_638 = new SpawnTechData[0];

	private SubGraph_KillEmAll_04 logic_SubGraph_KillEmAll_04_SubGraph_KillEmAll_04_639 = new SubGraph_KillEmAll_04();

	private SpawnTechData[] logic_SubGraph_KillEmAll_04_ghostTechSpawnData01_639 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_04_ghostTechSpawnData02_639 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_04_ghostTechSpawnData03_639 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_04_ghostTechSpawnData04_639 = new SpawnTechData[0];

	private SubGraph_KillEmAll_16 logic_SubGraph_KillEmAll_16_SubGraph_KillEmAll_16_640 = new SubGraph_KillEmAll_16();

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData01_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData02_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData03_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData04_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData05_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData06_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData07_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData08_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData09_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData10_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData11_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData12_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData13_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData14_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData15_640 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_KillEmAll_16_ghostTechSpawnData16_640 = new SpawnTechData[0];

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_641 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_641 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_642 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_642 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_642;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_642;

	private bool logic_uScript_DestroyTechsFromData_Out_642 = true;

	private Tank event_UnityEngine_GameObject_Tech_576;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_1.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
				}
			}
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
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_2;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_2;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_2;
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
		if (null == owner_Connection_26 || !m_RegisteredForEvents)
		{
			owner_Connection_26 = parentGameObject;
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
		if (null == owner_Connection_36 || !m_RegisteredForEvents)
		{
			owner_Connection_36 = parentGameObject;
		}
		if (null == owner_Connection_57 || !m_RegisteredForEvents)
		{
			owner_Connection_57 = parentGameObject;
		}
		if (null == owner_Connection_61 || !m_RegisteredForEvents)
		{
			owner_Connection_61 = parentGameObject;
		}
		if (null == owner_Connection_63 || !m_RegisteredForEvents)
		{
			owner_Connection_63 = parentGameObject;
		}
		if (null == owner_Connection_66 || !m_RegisteredForEvents)
		{
			owner_Connection_66 = parentGameObject;
		}
		if (null == owner_Connection_108 || !m_RegisteredForEvents)
		{
			owner_Connection_108 = parentGameObject;
		}
		if (null == owner_Connection_112 || !m_RegisteredForEvents)
		{
			owner_Connection_112 = parentGameObject;
		}
		if (null == owner_Connection_115 || !m_RegisteredForEvents)
		{
			owner_Connection_115 = parentGameObject;
		}
		if (null == owner_Connection_118 || !m_RegisteredForEvents)
		{
			owner_Connection_118 = parentGameObject;
		}
		if (null == owner_Connection_119 || !m_RegisteredForEvents)
		{
			owner_Connection_119 = parentGameObject;
		}
		if (null == owner_Connection_120 || !m_RegisteredForEvents)
		{
			owner_Connection_120 = parentGameObject;
		}
		if (null == owner_Connection_302 || !m_RegisteredForEvents)
		{
			owner_Connection_302 = parentGameObject;
		}
		if (null == owner_Connection_304 || !m_RegisteredForEvents)
		{
			owner_Connection_304 = parentGameObject;
		}
		if (null == owner_Connection_309 || !m_RegisteredForEvents)
		{
			owner_Connection_309 = parentGameObject;
		}
		if (null == owner_Connection_315 || !m_RegisteredForEvents)
		{
			owner_Connection_315 = parentGameObject;
		}
		if (null == owner_Connection_332 || !m_RegisteredForEvents)
		{
			owner_Connection_332 = parentGameObject;
		}
		if (null == owner_Connection_342 || !m_RegisteredForEvents)
		{
			owner_Connection_342 = parentGameObject;
		}
		if (null == owner_Connection_358 || !m_RegisteredForEvents)
		{
			owner_Connection_358 = parentGameObject;
		}
		if (null == owner_Connection_361 || !m_RegisteredForEvents)
		{
			owner_Connection_361 = parentGameObject;
		}
		if (null == owner_Connection_363 || !m_RegisteredForEvents)
		{
			owner_Connection_363 = parentGameObject;
		}
		if (null == owner_Connection_380 || !m_RegisteredForEvents)
		{
			owner_Connection_380 = parentGameObject;
		}
		if (null == owner_Connection_387 || !m_RegisteredForEvents)
		{
			owner_Connection_387 = parentGameObject;
		}
		if (null == owner_Connection_390 || !m_RegisteredForEvents)
		{
			owner_Connection_390 = parentGameObject;
		}
		if (null == owner_Connection_398 || !m_RegisteredForEvents)
		{
			owner_Connection_398 = parentGameObject;
		}
		if (null == owner_Connection_400 || !m_RegisteredForEvents)
		{
			owner_Connection_400 = parentGameObject;
		}
		if (null == owner_Connection_458 || !m_RegisteredForEvents)
		{
			owner_Connection_458 = parentGameObject;
		}
		if (null == owner_Connection_509 || !m_RegisteredForEvents)
		{
			owner_Connection_509 = parentGameObject;
		}
		if (null == owner_Connection_563 || !m_RegisteredForEvents)
		{
			owner_Connection_563 = parentGameObject;
		}
		if (null == owner_Connection_577 || !m_RegisteredForEvents)
		{
			owner_Connection_577 = parentGameObject;
			if (null != owner_Connection_577)
			{
				uScript_PlayerTechDestroyedEvent uScript_PlayerTechDestroyedEvent2 = owner_Connection_577.GetComponent<uScript_PlayerTechDestroyedEvent>();
				if (null == uScript_PlayerTechDestroyedEvent2)
				{
					uScript_PlayerTechDestroyedEvent2 = owner_Connection_577.AddComponent<uScript_PlayerTechDestroyedEvent>();
				}
				if (null != uScript_PlayerTechDestroyedEvent2)
				{
					uScript_PlayerTechDestroyedEvent2.TechDestroyedEvent += Instance_TechDestroyedEvent_576;
				}
			}
		}
		if (null == owner_Connection_643 || !m_RegisteredForEvents)
		{
			owner_Connection_643 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_1.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
			}
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
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_2;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_2;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_2;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_577)
		{
			uScript_PlayerTechDestroyedEvent uScript_PlayerTechDestroyedEvent2 = owner_Connection_577.GetComponent<uScript_PlayerTechDestroyedEvent>();
			if (null == uScript_PlayerTechDestroyedEvent2)
			{
				uScript_PlayerTechDestroyedEvent2 = owner_Connection_577.AddComponent<uScript_PlayerTechDestroyedEvent>();
			}
			if (null != uScript_PlayerTechDestroyedEvent2)
			{
				uScript_PlayerTechDestroyedEvent2.TechDestroyedEvent += Instance_TechDestroyedEvent_576;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_1)
		{
			uScript_EncounterUpdate component = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_0;
				component.OnSuspend -= Instance_OnSuspend_0;
				component.OnResume -= Instance_OnResume_0;
			}
		}
		if (null != owner_Connection_3)
		{
			uScript_SaveLoad component2 = owner_Connection_3.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_2;
				component2.LoadEvent -= Instance_LoadEvent_2;
				component2.RestartEvent -= Instance_RestartEvent_2;
			}
		}
		if (null != owner_Connection_577)
		{
			uScript_PlayerTechDestroyedEvent component3 = owner_Connection_577.GetComponent<uScript_PlayerTechDestroyedEvent>();
			if (null != component3)
			{
				component3.TechDestroyedEvent -= Instance_TechDestroyedEvent_576;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_5.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_13.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_15.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_19.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_27.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_28.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_30.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_38.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_41.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_43.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_44.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_45.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_50.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_56.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_58.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_60.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_62.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_65.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_70.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_71.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_74.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_76.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_81.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_83.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_86.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_90.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_95.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_97.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_104.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_109.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_110.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_113.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_114.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_122.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_123.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_133.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_135.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_137.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_138.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_147.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_157.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_161.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_166.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_171.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_178.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_182.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_187.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_191.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_197.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_203.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_205.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_214.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_216.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_223.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_225.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_234.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_236.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_243.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_249.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_254.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_255.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_261.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_266.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_271.SetParent(g);
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_275.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_282.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_283.SetParent(g);
		logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_284.SetParent(g);
		logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_285.SetParent(g);
		logic_uScript_GetTechsInTrigger_uScript_GetTechsInTrigger_286.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_288.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_289.SetParent(g);
		logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_290.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_295.SetParent(g);
		logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_297.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_298.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_303.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_305.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_307.SetParent(g);
		logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_313.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_314.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_317.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_319.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_325.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_326.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_327.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_331.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_334.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_337.SetParent(g);
		logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_339.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_344.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_345.SetParent(g);
		logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_348.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_349.SetParent(g);
		logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_350.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_352.SetParent(g);
		logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_354.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_359.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_360.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_362.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_366.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_368.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_370.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_372.SetParent(g);
		logic_uScript_GetTimeOfDay_uScript_GetTimeOfDay_377.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_381.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_385.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_386.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_389.SetParent(g);
		logic_uScript_Wait_uScript_Wait_392.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_393.SetParent(g);
		logic_uScript_Wait_uScript_Wait_395.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_396.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_397.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_399.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_407.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_409.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_410.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_411.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_413.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_415.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_416.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_419.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_421.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_423.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_425.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_427.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_429.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_430.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_434.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_436.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_437.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_439.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_441.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_444.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_448.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_449.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_450.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_452.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_454.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_457.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_459.SetParent(g);
		logic_uScript_Wait_uScript_Wait_461.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_465.SetParent(g);
		logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_466.SetParent(g);
		logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_467.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_468.SetParent(g);
		logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_469.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_470.SetParent(g);
		logic_uScript_IsTechAnchored_uScript_IsTechAnchored_471.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_472.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_475.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_476.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_478.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_480.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_481.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_482.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_483.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_486.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_487.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_490.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_492.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_498.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_500.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_504.SetParent(g);
		logic_uScript_GetTechsInTrigger_uScript_GetTechsInTrigger_506.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_507.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_508.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_510.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_511.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_512.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_514.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_516.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_518.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_519.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_521.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_525.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_526.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_527.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_528.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_529.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_530.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_531.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_532.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_533.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_534.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_535.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_536.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_537.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_541.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_545.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_546.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_549.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_550.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_551.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_552.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_553.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_556.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_559.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_560.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_561.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_565.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_567.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_570.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_574.SetParent(g);
		logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_579.SetParent(g);
		logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_580.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_581.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_584.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_586.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_587.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_588.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_590.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_592.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_593.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_595.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_597.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_602.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_603.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_606.SetParent(g);
		logic_SubGraph_KillEmAll_11_SubGraph_KillEmAll_11_638.SetParent(g);
		logic_SubGraph_KillEmAll_04_SubGraph_KillEmAll_04_639.SetParent(g);
		logic_SubGraph_KillEmAll_16_SubGraph_KillEmAll_16_640.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_641.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_642.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_26 = parentGameObject;
		owner_Connection_35 = parentGameObject;
		owner_Connection_36 = parentGameObject;
		owner_Connection_57 = parentGameObject;
		owner_Connection_61 = parentGameObject;
		owner_Connection_63 = parentGameObject;
		owner_Connection_66 = parentGameObject;
		owner_Connection_108 = parentGameObject;
		owner_Connection_112 = parentGameObject;
		owner_Connection_115 = parentGameObject;
		owner_Connection_118 = parentGameObject;
		owner_Connection_119 = parentGameObject;
		owner_Connection_120 = parentGameObject;
		owner_Connection_302 = parentGameObject;
		owner_Connection_304 = parentGameObject;
		owner_Connection_309 = parentGameObject;
		owner_Connection_315 = parentGameObject;
		owner_Connection_332 = parentGameObject;
		owner_Connection_342 = parentGameObject;
		owner_Connection_358 = parentGameObject;
		owner_Connection_361 = parentGameObject;
		owner_Connection_363 = parentGameObject;
		owner_Connection_380 = parentGameObject;
		owner_Connection_387 = parentGameObject;
		owner_Connection_390 = parentGameObject;
		owner_Connection_398 = parentGameObject;
		owner_Connection_400 = parentGameObject;
		owner_Connection_458 = parentGameObject;
		owner_Connection_509 = parentGameObject;
		owner_Connection_563 = parentGameObject;
		owner_Connection_577 = parentGameObject;
		owner_Connection_643 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_30.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_71.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_74.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_81.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_83.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_90.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_95.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_97.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_104.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_138.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_147.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_157.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_161.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_166.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_171.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_178.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_182.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_187.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_191.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_197.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_203.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_205.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_214.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_216.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_223.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_225.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_234.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_236.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_243.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_249.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_254.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_255.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_261.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_266.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_271.Awake();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_275.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_325.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_326.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_327.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_366.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_368.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_370.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_372.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_385.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_545.Awake();
		logic_SubGraph_KillEmAll_11_SubGraph_KillEmAll_11_638.Awake();
		logic_SubGraph_KillEmAll_04_SubGraph_KillEmAll_04_639.Awake();
		logic_SubGraph_KillEmAll_16_SubGraph_KillEmAll_16_640.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output1 += uScriptCon_ManualSwitch_Output1_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output2 += uScriptCon_ManualSwitch_Output2_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output3 += uScriptCon_ManualSwitch_Output3_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output4 += uScriptCon_ManualSwitch_Output4_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output5 += uScriptCon_ManualSwitch_Output5_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output6 += uScriptCon_ManualSwitch_Output6_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output7 += uScriptCon_ManualSwitch_Output7_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output8 += uScriptCon_ManualSwitch_Output8_9;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Save_Out += SubGraph_SaveLoadInt_Save_Out_22;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Load_Out += SubGraph_SaveLoadInt_Load_Out_22;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_22;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Out += SubGraph_LoadObjectiveStates_Out_24;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_30.Out += SubGraph_CompleteObjectiveStage_Out_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output1 += uScriptCon_ManualSwitch_Output1_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output2 += uScriptCon_ManualSwitch_Output2_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output3 += uScriptCon_ManualSwitch_Output3_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output4 += uScriptCon_ManualSwitch_Output4_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output5 += uScriptCon_ManualSwitch_Output5_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output6 += uScriptCon_ManualSwitch_Output6_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output7 += uScriptCon_ManualSwitch_Output7_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output8 += uScriptCon_ManualSwitch_Output8_53;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_71.Out += SubGraph_CompleteObjectiveStage_Out_71;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_74.Out += SubGraph_CompleteObjectiveStage_Out_74;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_81.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_81;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_83.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_83;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_90.Out += SubGraph_GhostTechController_Out_90;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_95.Out += SubGraph_GhostTechController_Out_95;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_97.Out += SubGraph_GhostTechController_Out_97;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_104.Out += SubGraph_GhostTechController_Out_104;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_138.Out += SubGraph_GhostTechController_Out_138;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_147.Out += SubGraph_GhostTechController_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Save_Out += SubGraph_SaveLoadBool_Save_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Load_Out += SubGraph_SaveLoadBool_Load_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Save_Out += SubGraph_SaveLoadBool_Save_Out_152;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Load_Out += SubGraph_SaveLoadBool_Load_Out_152;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_152;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Save_Out += SubGraph_SaveLoadBool_Save_Out_153;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Load_Out += SubGraph_SaveLoadBool_Load_Out_153;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_153;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_157.Out += SubGraph_GhostTechController_Out_157;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_161.Out += SubGraph_GhostTechController_Out_161;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_166.Out += SubGraph_GhostTechController_Out_166;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_171.Out += SubGraph_GhostTechController_Out_171;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_178.Out += SubGraph_GhostTechController_Out_178;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_182.Out += SubGraph_GhostTechController_Out_182;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_187.Out += SubGraph_GhostTechController_Out_187;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_191.Out += SubGraph_GhostTechController_Out_191;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_197.Out += SubGraph_GhostTechController_Out_197;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_203.Out += SubGraph_GhostTechController_Out_203;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_205.Out += SubGraph_GhostTechController_Out_205;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_214.Out += SubGraph_GhostTechController_Out_214;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_216.Out += SubGraph_GhostTechController_Out_216;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_223.Out += SubGraph_GhostTechController_Out_223;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_225.Out += SubGraph_GhostTechController_Out_225;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_234.Out += SubGraph_GhostTechController_Out_234;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_236.Out += SubGraph_GhostTechController_Out_236;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_243.Out += SubGraph_GhostTechController_Out_243;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_249.Out += SubGraph_GhostTechController_Out_249;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_254.Out += SubGraph_GhostTechController_Out_254;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_255.Out += SubGraph_GhostTechController_Out_255;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_261.Out += SubGraph_GhostTechController_Out_261;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_266.Out += SubGraph_GhostTechController_Out_266;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_271.Out += SubGraph_GhostTechController_Out_271;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_275.Out += SubGraph_GhostTechController_Out_275;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_325.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_325;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_326.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_326;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_327.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_327;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_366.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_366;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_368.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_368;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_370.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_370;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_372.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_372;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.Save_Out += SubGraph_SaveLoadInt_Save_Out_379;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.Load_Out += SubGraph_SaveLoadInt_Load_Out_379;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_379;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_385.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_385;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Save_Out += SubGraph_SaveLoadBool_Save_Out_403;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Load_Out += SubGraph_SaveLoadBool_Load_Out_403;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_403;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.Save_Out += SubGraph_SaveLoadInt_Save_Out_404;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.Load_Out += SubGraph_SaveLoadInt_Load_Out_404;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_404;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Save_Out += SubGraph_SaveLoadBool_Save_Out_485;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Load_Out += SubGraph_SaveLoadBool_Load_Out_485;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_485;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Save_Out += SubGraph_SaveLoadBool_Save_Out_494;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Load_Out += SubGraph_SaveLoadBool_Load_Out_494;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_494;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.Save_Out += SubGraph_SaveLoadInt_Save_Out_496;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.Load_Out += SubGraph_SaveLoadInt_Load_Out_496;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_496;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_545.Out += SubGraph_CompleteObjectiveStage_Out_545;
		logic_SubGraph_KillEmAll_11_SubGraph_KillEmAll_11_638.Out += SubGraph_KillEmAll_11_Out_638;
		logic_SubGraph_KillEmAll_04_SubGraph_KillEmAll_04_639.Out += SubGraph_KillEmAll_04_Out_639;
		logic_SubGraph_KillEmAll_16_SubGraph_KillEmAll_16_640.Out += SubGraph_KillEmAll_16_Out_640;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_30.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_71.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_74.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_81.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_83.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_90.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_95.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_97.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_104.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_138.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_147.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_157.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_161.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_166.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_171.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_178.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_182.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_187.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_191.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_197.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_203.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_205.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_214.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_216.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_223.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_225.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_234.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_236.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_243.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_249.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_254.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_255.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_261.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_266.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_271.Start();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_275.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_325.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_326.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_327.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_366.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_368.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_370.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_372.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_385.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_545.Start();
		logic_SubGraph_KillEmAll_11_SubGraph_KillEmAll_11_638.Start();
		logic_SubGraph_KillEmAll_04_SubGraph_KillEmAll_04_639.Start();
		logic_SubGraph_KillEmAll_16_SubGraph_KillEmAll_16_640.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_30.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_44.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_58.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_71.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_74.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_81.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_83.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_90.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_95.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_97.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_104.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_138.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_147.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_157.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_161.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_166.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_171.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_178.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_182.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_187.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_191.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_197.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_203.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_205.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_214.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_216.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_223.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_225.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_234.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_236.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_243.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_249.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_254.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_255.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_261.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_266.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_271.OnEnable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_275.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_325.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_326.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_327.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_366.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_368.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_370.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_372.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_385.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_409.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_419.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_430.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_448.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_508.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_545.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_559.OnEnable();
		logic_SubGraph_KillEmAll_11_SubGraph_KillEmAll_11_638.OnEnable();
		logic_SubGraph_KillEmAll_04_SubGraph_KillEmAll_04_639.OnEnable();
		logic_SubGraph_KillEmAll_16_SubGraph_KillEmAll_16_640.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_27.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_30.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_38.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_71.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_74.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_81.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_83.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_90.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_95.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_97.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_104.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_138.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_147.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_157.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_161.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_166.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_171.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_178.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_182.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_187.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_191.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_197.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_203.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_205.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_214.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_216.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_223.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_225.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_234.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_236.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_243.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_249.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_254.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_255.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_261.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_266.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_271.OnDisable();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_275.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_319.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_325.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_326.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_327.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_334.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_345.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_366.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_368.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_370.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_372.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_385.OnDisable();
		logic_uScript_Wait_uScript_Wait_392.OnDisable();
		logic_uScript_Wait_uScript_Wait_395.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_409.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_419.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_430.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_448.OnDisable();
		logic_uScript_Wait_uScript_Wait_461.OnDisable();
		logic_uScript_IsTechAnchored_uScript_IsTechAnchored_471.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_476.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_498.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_504.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_510.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_511.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_512.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_545.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_549.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_552.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_556.OnDisable();
		logic_SubGraph_KillEmAll_11_SubGraph_KillEmAll_11_638.OnDisable();
		logic_SubGraph_KillEmAll_04_SubGraph_KillEmAll_04_639.OnDisable();
		logic_SubGraph_KillEmAll_16_SubGraph_KillEmAll_16_640.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_30.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_71.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_74.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_81.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_83.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_90.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_95.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_97.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_104.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_138.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_147.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_157.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_161.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_166.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_171.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_178.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_182.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_187.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_191.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_197.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_203.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_205.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_214.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_216.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_223.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_225.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_234.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_236.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_243.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_249.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_254.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_255.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_261.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_266.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_271.Update();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_275.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_325.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_326.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_327.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_366.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_368.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_370.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_372.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_385.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_545.Update();
		logic_SubGraph_KillEmAll_11_SubGraph_KillEmAll_11_638.Update();
		logic_SubGraph_KillEmAll_04_SubGraph_KillEmAll_04_639.Update();
		logic_SubGraph_KillEmAll_16_SubGraph_KillEmAll_16_640.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_30.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_71.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_74.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_81.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_83.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_90.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_95.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_97.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_104.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_138.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_147.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_157.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_161.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_166.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_171.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_178.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_182.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_187.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_191.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_197.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_203.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_205.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_214.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_216.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_223.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_225.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_234.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_236.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_243.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_249.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_254.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_255.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_261.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_266.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_271.OnDestroy();
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_275.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_325.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_326.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_327.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_366.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_368.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_370.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_372.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_385.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_545.OnDestroy();
		logic_SubGraph_KillEmAll_11_SubGraph_KillEmAll_11_638.OnDestroy();
		logic_SubGraph_KillEmAll_04_SubGraph_KillEmAll_04_639.OnDestroy();
		logic_SubGraph_KillEmAll_16_SubGraph_KillEmAll_16_640.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output1 -= uScriptCon_ManualSwitch_Output1_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output2 -= uScriptCon_ManualSwitch_Output2_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output3 -= uScriptCon_ManualSwitch_Output3_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output4 -= uScriptCon_ManualSwitch_Output4_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output5 -= uScriptCon_ManualSwitch_Output5_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output6 -= uScriptCon_ManualSwitch_Output6_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output7 -= uScriptCon_ManualSwitch_Output7_9;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.Output8 -= uScriptCon_ManualSwitch_Output8_9;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Save_Out -= SubGraph_SaveLoadInt_Save_Out_22;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Load_Out -= SubGraph_SaveLoadInt_Load_Out_22;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_22;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Out -= SubGraph_LoadObjectiveStates_Out_24;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_30.Out -= SubGraph_CompleteObjectiveStage_Out_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output1 -= uScriptCon_ManualSwitch_Output1_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output2 -= uScriptCon_ManualSwitch_Output2_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output3 -= uScriptCon_ManualSwitch_Output3_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output4 -= uScriptCon_ManualSwitch_Output4_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output5 -= uScriptCon_ManualSwitch_Output5_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output6 -= uScriptCon_ManualSwitch_Output6_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output7 -= uScriptCon_ManualSwitch_Output7_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.Output8 -= uScriptCon_ManualSwitch_Output8_53;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_71.Out -= SubGraph_CompleteObjectiveStage_Out_71;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_74.Out -= SubGraph_CompleteObjectiveStage_Out_74;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_81.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_81;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_83.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_83;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_90.Out -= SubGraph_GhostTechController_Out_90;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_95.Out -= SubGraph_GhostTechController_Out_95;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_97.Out -= SubGraph_GhostTechController_Out_97;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_104.Out -= SubGraph_GhostTechController_Out_104;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_138.Out -= SubGraph_GhostTechController_Out_138;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_147.Out -= SubGraph_GhostTechController_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Save_Out -= SubGraph_SaveLoadBool_Save_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Load_Out -= SubGraph_SaveLoadBool_Load_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Save_Out -= SubGraph_SaveLoadBool_Save_Out_152;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Load_Out -= SubGraph_SaveLoadBool_Load_Out_152;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_152;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Save_Out -= SubGraph_SaveLoadBool_Save_Out_153;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Load_Out -= SubGraph_SaveLoadBool_Load_Out_153;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_153;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_157.Out -= SubGraph_GhostTechController_Out_157;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_161.Out -= SubGraph_GhostTechController_Out_161;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_166.Out -= SubGraph_GhostTechController_Out_166;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_171.Out -= SubGraph_GhostTechController_Out_171;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_178.Out -= SubGraph_GhostTechController_Out_178;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_182.Out -= SubGraph_GhostTechController_Out_182;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_187.Out -= SubGraph_GhostTechController_Out_187;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_191.Out -= SubGraph_GhostTechController_Out_191;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_197.Out -= SubGraph_GhostTechController_Out_197;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_203.Out -= SubGraph_GhostTechController_Out_203;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_205.Out -= SubGraph_GhostTechController_Out_205;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_214.Out -= SubGraph_GhostTechController_Out_214;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_216.Out -= SubGraph_GhostTechController_Out_216;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_223.Out -= SubGraph_GhostTechController_Out_223;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_225.Out -= SubGraph_GhostTechController_Out_225;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_234.Out -= SubGraph_GhostTechController_Out_234;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_236.Out -= SubGraph_GhostTechController_Out_236;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_243.Out -= SubGraph_GhostTechController_Out_243;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_249.Out -= SubGraph_GhostTechController_Out_249;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_254.Out -= SubGraph_GhostTechController_Out_254;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_255.Out -= SubGraph_GhostTechController_Out_255;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_261.Out -= SubGraph_GhostTechController_Out_261;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_266.Out -= SubGraph_GhostTechController_Out_266;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_271.Out -= SubGraph_GhostTechController_Out_271;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_275.Out -= SubGraph_GhostTechController_Out_275;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_325.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_325;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_326.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_326;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_327.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_327;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_366.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_366;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_368.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_368;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_370.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_370;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_372.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_372;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.Save_Out -= SubGraph_SaveLoadInt_Save_Out_379;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.Load_Out -= SubGraph_SaveLoadInt_Load_Out_379;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_379;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_385.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_385;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Save_Out -= SubGraph_SaveLoadBool_Save_Out_403;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Load_Out -= SubGraph_SaveLoadBool_Load_Out_403;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_403;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.Save_Out -= SubGraph_SaveLoadInt_Save_Out_404;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.Load_Out -= SubGraph_SaveLoadInt_Load_Out_404;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_404;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Save_Out -= SubGraph_SaveLoadBool_Save_Out_485;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Load_Out -= SubGraph_SaveLoadBool_Load_Out_485;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_485;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Save_Out -= SubGraph_SaveLoadBool_Save_Out_494;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Load_Out -= SubGraph_SaveLoadBool_Load_Out_494;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_494;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.Save_Out -= SubGraph_SaveLoadInt_Save_Out_496;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.Load_Out -= SubGraph_SaveLoadInt_Load_Out_496;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_496;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_545.Out -= SubGraph_CompleteObjectiveStage_Out_545;
		logic_SubGraph_KillEmAll_11_SubGraph_KillEmAll_11_638.Out -= SubGraph_KillEmAll_11_Out_638;
		logic_SubGraph_KillEmAll_04_SubGraph_KillEmAll_04_639.Out -= SubGraph_KillEmAll_04_Out_639;
		logic_SubGraph_KillEmAll_16_SubGraph_KillEmAll_16_640.Out -= SubGraph_KillEmAll_16_Out_640;
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

	private void Instance_SaveEvent_2(object o, EventArgs e)
	{
		Relay_SaveEvent_2();
	}

	private void Instance_LoadEvent_2(object o, EventArgs e)
	{
		Relay_LoadEvent_2();
	}

	private void Instance_RestartEvent_2(object o, EventArgs e)
	{
		Relay_RestartEvent_2();
	}

	private void Instance_TechDestroyedEvent_576(object o, uScript_PlayerTechDestroyedEvent.TechDestroyedEventArgs e)
	{
		event_UnityEngine_GameObject_Tech_576 = e.Tech;
		Relay_TechDestroyedEvent_576();
	}

	private void SubGraph_SaveLoadBool_Save_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_TechsInit_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Save_Out_7();
	}

	private void SubGraph_SaveLoadBool_Load_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_TechsInit_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Load_Out_7();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_TechsInit_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Restart_Out_7();
	}

	private void uScriptCon_ManualSwitch_Output1_9(object o, EventArgs e)
	{
		Relay_Output1_9();
	}

	private void uScriptCon_ManualSwitch_Output2_9(object o, EventArgs e)
	{
		Relay_Output2_9();
	}

	private void uScriptCon_ManualSwitch_Output3_9(object o, EventArgs e)
	{
		Relay_Output3_9();
	}

	private void uScriptCon_ManualSwitch_Output4_9(object o, EventArgs e)
	{
		Relay_Output4_9();
	}

	private void uScriptCon_ManualSwitch_Output5_9(object o, EventArgs e)
	{
		Relay_Output5_9();
	}

	private void uScriptCon_ManualSwitch_Output6_9(object o, EventArgs e)
	{
		Relay_Output6_9();
	}

	private void uScriptCon_ManualSwitch_Output7_9(object o, EventArgs e)
	{
		Relay_Output7_9();
	}

	private void uScriptCon_ManualSwitch_Output8_9(object o, EventArgs e)
	{
		Relay_Output8_9();
	}

	private void SubGraph_SaveLoadInt_Save_Out_22(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_22 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_22;
		Relay_Save_Out_22();
	}

	private void SubGraph_SaveLoadInt_Load_Out_22(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_22 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_22;
		Relay_Load_Out_22();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_22(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_22 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_22;
		Relay_Restart_Out_22();
	}

	private void SubGraph_LoadObjectiveStates_Out_24(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_24();
	}

	private void SubGraph_CompleteObjectiveStage_Out_30(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_30 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_30;
		Relay_Out_30();
	}

	private void uScriptCon_ManualSwitch_Output1_53(object o, EventArgs e)
	{
		Relay_Output1_53();
	}

	private void uScriptCon_ManualSwitch_Output2_53(object o, EventArgs e)
	{
		Relay_Output2_53();
	}

	private void uScriptCon_ManualSwitch_Output3_53(object o, EventArgs e)
	{
		Relay_Output3_53();
	}

	private void uScriptCon_ManualSwitch_Output4_53(object o, EventArgs e)
	{
		Relay_Output4_53();
	}

	private void uScriptCon_ManualSwitch_Output5_53(object o, EventArgs e)
	{
		Relay_Output5_53();
	}

	private void uScriptCon_ManualSwitch_Output6_53(object o, EventArgs e)
	{
		Relay_Output6_53();
	}

	private void uScriptCon_ManualSwitch_Output7_53(object o, EventArgs e)
	{
		Relay_Output7_53();
	}

	private void uScriptCon_ManualSwitch_Output8_53(object o, EventArgs e)
	{
		Relay_Output8_53();
	}

	private void SubGraph_CompleteObjectiveStage_Out_71(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_71 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_71;
		Relay_Out_71();
	}

	private void SubGraph_CompleteObjectiveStage_Out_74(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_74 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_74;
		Relay_Out_74();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_81(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_81 = e.Forcefields;
		local_Stage2Forcefields_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_81;
		Relay_Out_81();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_83(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_83 = e.Forcefields;
		local_Stage3Forcefields_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_83;
		Relay_Out_83();
	}

	private void SubGraph_GhostTechController_Out_90(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_90();
	}

	private void SubGraph_GhostTechController_Out_95(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_95();
	}

	private void SubGraph_GhostTechController_Out_97(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_97();
	}

	private void SubGraph_GhostTechController_Out_104(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_104();
	}

	private void SubGraph_GhostTechController_Out_138(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_138();
	}

	private void SubGraph_GhostTechController_Out_147(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_147();
	}

	private void SubGraph_SaveLoadBool_Save_Out_151(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = e.boolean;
		local_Satege2Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_151;
		Relay_Save_Out_151();
	}

	private void SubGraph_SaveLoadBool_Load_Out_151(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = e.boolean;
		local_Satege2Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_151;
		Relay_Load_Out_151();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_151(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = e.boolean;
		local_Satege2Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_151;
		Relay_Restart_Out_151();
	}

	private void SubGraph_SaveLoadBool_Save_Out_152(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = e.boolean;
		local_Satege3Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_152;
		Relay_Save_Out_152();
	}

	private void SubGraph_SaveLoadBool_Load_Out_152(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = e.boolean;
		local_Satege3Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_152;
		Relay_Load_Out_152();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_152(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = e.boolean;
		local_Satege3Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_152;
		Relay_Restart_Out_152();
	}

	private void SubGraph_SaveLoadBool_Save_Out_153(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = e.boolean;
		local_EndInit_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_153;
		Relay_Save_Out_153();
	}

	private void SubGraph_SaveLoadBool_Load_Out_153(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = e.boolean;
		local_EndInit_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_153;
		Relay_Load_Out_153();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_153(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = e.boolean;
		local_EndInit_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_153;
		Relay_Restart_Out_153();
	}

	private void SubGraph_GhostTechController_Out_157(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_157();
	}

	private void SubGraph_GhostTechController_Out_161(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_161();
	}

	private void SubGraph_GhostTechController_Out_166(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_166();
	}

	private void SubGraph_GhostTechController_Out_171(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_171();
	}

	private void SubGraph_GhostTechController_Out_178(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_178();
	}

	private void SubGraph_GhostTechController_Out_182(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_182();
	}

	private void SubGraph_GhostTechController_Out_187(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_187();
	}

	private void SubGraph_GhostTechController_Out_191(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_191();
	}

	private void SubGraph_GhostTechController_Out_197(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_197();
	}

	private void SubGraph_GhostTechController_Out_203(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_203();
	}

	private void SubGraph_GhostTechController_Out_205(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_205();
	}

	private void SubGraph_GhostTechController_Out_214(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_214();
	}

	private void SubGraph_GhostTechController_Out_216(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_216();
	}

	private void SubGraph_GhostTechController_Out_223(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_223();
	}

	private void SubGraph_GhostTechController_Out_225(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_225();
	}

	private void SubGraph_GhostTechController_Out_234(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_234();
	}

	private void SubGraph_GhostTechController_Out_236(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_236();
	}

	private void SubGraph_GhostTechController_Out_243(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_243();
	}

	private void SubGraph_GhostTechController_Out_249(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_249();
	}

	private void SubGraph_GhostTechController_Out_254(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_254();
	}

	private void SubGraph_GhostTechController_Out_255(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_255();
	}

	private void SubGraph_GhostTechController_Out_261(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_261();
	}

	private void SubGraph_GhostTechController_Out_266(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_266();
	}

	private void SubGraph_GhostTechController_Out_271(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_271();
	}

	private void SubGraph_GhostTechController_Out_275(object o, SubGraph_GhostTechController.LogicEventArgs e)
	{
		Relay_Out_275();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_325(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_325 = e.Forcefields;
		local_Terminals1_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_325;
		Relay_Out_325();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_326(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_326 = e.Forcefields;
		local_Terminals2_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_326;
		Relay_Out_326();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_327(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_327 = e.Forcefields;
		local_Terminals3_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_327;
		Relay_Out_327();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_366(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_366 = e.Forcefields;
		local_EntranceForcefields_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_366;
		Relay_Out_366();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_368(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_368 = e.Forcefields;
		local_Stage1Forcefields_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_368;
		Relay_Out_368();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_370(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_370 = e.Forcefields;
		local_Stage1Forcefields_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_370;
		Relay_Out_370();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_372(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_372 = e.Forcefields;
		local_EntranceForcefields_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_372;
		Relay_Out_372();
	}

	private void SubGraph_SaveLoadInt_Save_Out_379(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_379 = e.integer;
		local_StartingHour_System_Int32 = logic_SubGraph_SaveLoadInt_integer_379;
		Relay_Save_Out_379();
	}

	private void SubGraph_SaveLoadInt_Load_Out_379(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_379 = e.integer;
		local_StartingHour_System_Int32 = logic_SubGraph_SaveLoadInt_integer_379;
		Relay_Load_Out_379();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_379(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_379 = e.integer;
		local_StartingHour_System_Int32 = logic_SubGraph_SaveLoadInt_integer_379;
		Relay_Restart_Out_379();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_385(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_385 = e.Forcefields;
		local_Stage3Forcefields_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_385;
		Relay_Out_385();
	}

	private void SubGraph_SaveLoadBool_Save_Out_403(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = e.boolean;
		local_DialogueComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_403;
		Relay_Save_Out_403();
	}

	private void SubGraph_SaveLoadBool_Load_Out_403(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = e.boolean;
		local_DialogueComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_403;
		Relay_Load_Out_403();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_403(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = e.boolean;
		local_DialogueComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_403;
		Relay_Restart_Out_403();
	}

	private void SubGraph_SaveLoadInt_Save_Out_404(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_404 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_404;
		Relay_Save_Out_404();
	}

	private void SubGraph_SaveLoadInt_Load_Out_404(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_404 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_404;
		Relay_Load_Out_404();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_404(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_404 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_404;
		Relay_Restart_Out_404();
	}

	private void SubGraph_SaveLoadBool_Save_Out_485(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_485 = e.boolean;
		local_MsgFlyingWarningShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_485;
		Relay_Save_Out_485();
	}

	private void SubGraph_SaveLoadBool_Load_Out_485(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_485 = e.boolean;
		local_MsgFlyingWarningShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_485;
		Relay_Load_Out_485();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_485(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_485 = e.boolean;
		local_MsgFlyingWarningShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_485;
		Relay_Restart_Out_485();
	}

	private void SubGraph_SaveLoadBool_Save_Out_494(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_494 = e.boolean;
		local_BaseOnline_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_494;
		Relay_Save_Out_494();
	}

	private void SubGraph_SaveLoadBool_Load_Out_494(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_494 = e.boolean;
		local_BaseOnline_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_494;
		Relay_Load_Out_494();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_494(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_494 = e.boolean;
		local_BaseOnline_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_494;
		Relay_Restart_Out_494();
	}

	private void SubGraph_SaveLoadInt_Save_Out_496(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_496 = e.integer;
		local_Objective04SubStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_496;
		Relay_Save_Out_496();
	}

	private void SubGraph_SaveLoadInt_Load_Out_496(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_496 = e.integer;
		local_Objective04SubStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_496;
		Relay_Load_Out_496();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_496(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_496 = e.integer;
		local_Objective04SubStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_496;
		Relay_Restart_Out_496();
	}

	private void SubGraph_CompleteObjectiveStage_Out_545(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_545 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_545;
		Relay_Out_545();
	}

	private void SubGraph_KillEmAll_11_Out_638(object o, SubGraph_KillEmAll_11.LogicEventArgs e)
	{
		Relay_Out_638();
	}

	private void SubGraph_KillEmAll_04_Out_639(object o, SubGraph_KillEmAll_04.LogicEventArgs e)
	{
		Relay_Out_639();
	}

	private void SubGraph_KillEmAll_16_Out_640(object o, SubGraph_KillEmAll_16.LogicEventArgs e)
	{
		Relay_Out_640();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_4();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_SaveEvent_2()
	{
		Relay_Save_494();
	}

	private void Relay_LoadEvent_2()
	{
		Relay_Load_494();
	}

	private void Relay_RestartEvent_2()
	{
		Relay_Set_False_494();
	}

	private void Relay_In_4()
	{
		logic_uScriptCon_CompareBool_Bool_4 = local_TechsInit_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.In(logic_uScriptCon_CompareBool_Bool_4);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.False;
		if (num)
		{
			Relay_In_27();
		}
		if (flag)
		{
			Relay_True_5();
		}
	}

	private void Relay_True_5()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_5.True(out logic_uScriptAct_SetBool_Target_5);
		local_TechsInit_System_Boolean = logic_uScriptAct_SetBool_Target_5;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_5.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_False_5()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_5.False(out logic_uScriptAct_SetBool_Target_5);
		local_TechsInit_System_Boolean = logic_uScriptAct_SetBool_Target_5;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_5.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_Save_Out_7()
	{
		Relay_Save_151();
	}

	private void Relay_Load_Out_7()
	{
		Relay_Load_151();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_Set_False_151();
	}

	private void Relay_Save_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Load_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Set_True_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Set_False_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Output1_9()
	{
		Relay_In_366();
	}

	private void Relay_Output2_9()
	{
		Relay_False_597();
	}

	private void Relay_Output3_9()
	{
		Relay_In_81();
	}

	private void Relay_Output4_9()
	{
		Relay_In_83();
	}

	private void Relay_Output5_9()
	{
		Relay_True_487();
	}

	private void Relay_Output6_9()
	{
	}

	private void Relay_Output7_9()
	{
	}

	private void Relay_Output8_9()
	{
	}

	private void Relay_In_9()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_9 = local_CurrentObjective_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_9.In(logic_uScriptCon_ManualSwitch_CurrentOutput_9);
	}

	private void Relay_In_13()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData01;
		if (logic_uScript_GetAndCheckTechs_techData_13.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_13, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_13, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_13 = owner_Connection_12;
		int num2 = 0;
		Array array = local_11_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_13.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_13, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_13, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_13 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_13.In(logic_uScript_GetAndCheckTechs_techData_13, logic_uScript_GetAndCheckTechs_ownerNode_13, ref logic_uScript_GetAndCheckTechs_techs_13);
		local_11_TankArray = logic_uScript_GetAndCheckTechs_techs_13;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_13.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_13.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_13.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_13.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_19();
		}
		if (someAlive)
		{
			Relay_AtIndex_19();
		}
		if (allDead)
		{
			Relay_In_21();
		}
		if (waitingToSpawn)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_15()
	{
		logic_uScript_SetEncounterTarget_owner_15 = owner_Connection_17;
		logic_uScript_SetEncounterTarget_visibleObject_15 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_15.In(logic_uScript_SetEncounterTarget_owner_15, logic_uScript_SetEncounterTarget_visibleObject_15);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_15.Out)
		{
			Relay_In_546();
		}
	}

	private void Relay_AtIndex_19()
	{
		int num = 0;
		Array array = local_11_TankArray;
		if (logic_uScript_AccessListTech_techList_19.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_19, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_19, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_19.AtIndex(ref logic_uScript_AccessListTech_techList_19, logic_uScript_AccessListTech_index_19, out logic_uScript_AccessListTech_value_19);
		local_11_TankArray = logic_uScript_AccessListTech_techList_19;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_19;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_19.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_21()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21.Out)
		{
			Relay_In_546();
		}
	}

	private void Relay_Save_Out_22()
	{
		Relay_Save_379();
	}

	private void Relay_Load_Out_22()
	{
		Relay_Load_379();
	}

	private void Relay_Restart_Out_22()
	{
		Relay_Restart_379();
	}

	private void Relay_Save_22()
	{
		logic_SubGraph_SaveLoadInt_integer_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Save(logic_SubGraph_SaveLoadInt_restartValue_22, ref logic_SubGraph_SaveLoadInt_integer_22, logic_SubGraph_SaveLoadInt_intAsVariable_22, logic_SubGraph_SaveLoadInt_uniqueID_22);
	}

	private void Relay_Load_22()
	{
		logic_SubGraph_SaveLoadInt_integer_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Load(logic_SubGraph_SaveLoadInt_restartValue_22, ref logic_SubGraph_SaveLoadInt_integer_22, logic_SubGraph_SaveLoadInt_intAsVariable_22, logic_SubGraph_SaveLoadInt_uniqueID_22);
	}

	private void Relay_Restart_22()
	{
		logic_SubGraph_SaveLoadInt_integer_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Restart(logic_SubGraph_SaveLoadInt_restartValue_22, ref logic_SubGraph_SaveLoadInt_integer_22, logic_SubGraph_SaveLoadInt_intAsVariable_22, logic_SubGraph_SaveLoadInt_uniqueID_22);
	}

	private void Relay_Out_24()
	{
		Relay_In_590();
	}

	private void Relay_In_24()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_24 = local_CurrentObjective_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.In(logic_SubGraph_LoadObjectiveStates_currentObjective_24);
	}

	private void Relay_In_27()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_27 = owner_Connection_26;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_27.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_27);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_27.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_27.False;
		if (num)
		{
			Relay_Pause_28();
		}
		if (flag)
		{
			Relay_UnPause_28();
		}
	}

	private void Relay_Pause_28()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_28.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_28.Out)
		{
			Relay_In_514();
		}
	}

	private void Relay_UnPause_28()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_28.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_28.Out)
		{
			Relay_In_514();
		}
	}

	private void Relay_Out_30()
	{
	}

	private void Relay_In_30()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_30 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_30.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_30, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_30);
	}

	private void Relay_In_38()
	{
		logic_uScript_AddMessage_messageData_38 = msgOutro;
		logic_uScript_AddMessage_speaker_38 = SpeakerHubl;
		logic_uScript_AddMessage_Return_38 = logic_uScript_AddMessage_uScript_AddMessage_38.In(logic_uScript_AddMessage_messageData_38, logic_uScript_AddMessage_speaker_38);
		if (logic_uScript_AddMessage_uScript_AddMessage_38.Shown)
		{
			Relay_In_437();
		}
	}

	private void Relay_In_40()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData02;
		if (logic_uScript_GetAndCheckTechs_techData_40.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_40, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_40, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_40 = owner_Connection_35;
		int num2 = 0;
		Array array = local_33_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_40.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_40, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_40, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_40 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.In(logic_uScript_GetAndCheckTechs_techData_40, logic_uScript_GetAndCheckTechs_ownerNode_40, ref logic_uScript_GetAndCheckTechs_techs_40);
		local_33_TankArray = logic_uScript_GetAndCheckTechs_techs_40;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.SomeAlive;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_43();
		}
		if (someAlive)
		{
			Relay_AtIndex_43();
		}
		if (waitingToSpawn)
		{
			Relay_In_53();
		}
	}

	private void Relay_Succeed_41()
	{
		logic_uScript_FinishEncounter_owner_41 = owner_Connection_36;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_41.Succeed(logic_uScript_FinishEncounter_owner_41);
	}

	private void Relay_Fail_41()
	{
		logic_uScript_FinishEncounter_owner_41 = owner_Connection_36;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_41.Fail(logic_uScript_FinishEncounter_owner_41);
	}

	private void Relay_AtIndex_43()
	{
		int num = 0;
		Array array = local_33_TankArray;
		if (logic_uScript_AccessListTech_techList_43.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_43, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_43, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_43.AtIndex(ref logic_uScript_AccessListTech_techList_43, logic_uScript_AccessListTech_index_43, out logic_uScript_AccessListTech_value_43);
		local_33_TankArray = logic_uScript_AccessListTech_techList_43;
		local_NPCTech02_Tank = logic_uScript_AccessListTech_value_43;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_43.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_In_44()
	{
		logic_uScript_FlyTechUpAndAway_tech_44 = local_NPCTech02_Tank;
		logic_uScript_FlyTechUpAndAway_removalParticles_44 = NPCDespawnEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_44.In(logic_uScript_FlyTechUpAndAway_tech_44, logic_uScript_FlyTechUpAndAway_maxLifetime_44, logic_uScript_FlyTechUpAndAway_targetHeight_44, logic_uScript_FlyTechUpAndAway_aiTree_44, logic_uScript_FlyTechUpAndAway_removalParticles_44);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_44.Out)
		{
			Relay_In_359();
		}
	}

	private void Relay_In_45()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_45 = NPCTriggerVolume01;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_45.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_45);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_45.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_45.InRange;
		if (num)
		{
			Relay_In_442();
		}
		if (inRange)
		{
			Relay_False_427();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_50 = Stage1GoalTriggerVolume;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_50.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_50);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_50.Out;
		bool allInRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_50.AllInRange;
		if (num)
		{
			Relay_In_86();
		}
		if (allInRange)
		{
			Relay_In_314();
		}
	}

	private void Relay_Output1_53()
	{
		Relay_In_45();
	}

	private void Relay_Output2_53()
	{
		Relay_In_38();
	}

	private void Relay_Output3_53()
	{
		Relay_In_436();
	}

	private void Relay_Output4_53()
	{
	}

	private void Relay_Output5_53()
	{
	}

	private void Relay_Output6_53()
	{
	}

	private void Relay_Output7_53()
	{
	}

	private void Relay_Output8_53()
	{
	}

	private void Relay_In_53()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_53 = local_Objective04SubStage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_53.In(logic_uScriptCon_ManualSwitch_CurrentOutput_53);
	}

	private void Relay_In_56()
	{
		logic_uScriptAct_AddInt_v2_A_56 = local_Objective04SubStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_56.In(logic_uScriptAct_AddInt_v2_A_56, logic_uScriptAct_AddInt_v2_B_56, out logic_uScriptAct_AddInt_v2_IntResult_56, out logic_uScriptAct_AddInt_v2_FloatResult_56);
		local_Objective04SubStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_56;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_56.Out)
		{
			Relay_In_641();
		}
	}

	private void Relay_In_58()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_58 = owner_Connection_57;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_58.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_58);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_58.Out)
		{
			Relay_InitialSpawn_62();
		}
	}

	private void Relay_InitialSpawn_60()
	{
		int num = 0;
		Array forcefieldStage1GroupSpawnData = ForcefieldStage1GroupSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_60.Length != num + forcefieldStage1GroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_60, num + forcefieldStage1GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage1GroupSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_60, num, forcefieldStage1GroupSpawnData.Length);
		num += forcefieldStage1GroupSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_60 = owner_Connection_61;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_60.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_60, logic_uScript_SpawnTechsFromData_ownerNode_60, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_60, logic_uScript_SpawnTechsFromData_allowResurrection_60);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_60.Out)
		{
			Relay_InitialSpawn_303();
		}
	}

	private void Relay_InitialSpawn_62()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData01;
		if (logic_uScript_SpawnTechsFromData_spawnData_62.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_62, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_62, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_62 = owner_Connection_63;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_62.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_62, logic_uScript_SpawnTechsFromData_ownerNode_62, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_62, logic_uScript_SpawnTechsFromData_allowResurrection_62);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_62.Out)
		{
			Relay_InitialSpawn_65();
		}
	}

	private void Relay_InitialSpawn_65()
	{
		int num = 0;
		Array forcefieldEntranceGroupSpawnData = ForcefieldEntranceGroupSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_65.Length != num + forcefieldEntranceGroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_65, num + forcefieldEntranceGroupSpawnData.Length);
		}
		Array.Copy(forcefieldEntranceGroupSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_65, num, forcefieldEntranceGroupSpawnData.Length);
		num += forcefieldEntranceGroupSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_65 = owner_Connection_66;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_65.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_65, logic_uScript_SpawnTechsFromData_ownerNode_65, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_65, logic_uScript_SpawnTechsFromData_allowResurrection_65);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_65.Out)
		{
			Relay_InitialSpawn_60();
		}
	}

	private void Relay_In_70()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_70 = Stage2GoalTriggerVolume;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_70.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_70);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_70.Out;
		bool allInRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_70.AllInRange;
		if (num)
		{
			Relay_In_411();
		}
		if (allInRange)
		{
			Relay_In_337();
		}
	}

	private void Relay_Out_71()
	{
	}

	private void Relay_In_71()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_71 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_71.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_71, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_71);
	}

	private void Relay_Out_74()
	{
	}

	private void Relay_In_74()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_74 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_74.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_74, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_74);
	}

	private void Relay_In_76()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_76 = Stage3GoalTriggerVolume;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_76.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_76);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_76.Out;
		bool allInRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_76.AllInRange;
		if (num)
		{
			Relay_In_415();
		}
		if (allInRange)
		{
			Relay_In_344();
		}
	}

	private void Relay_Out_81()
	{
		Relay_In_70();
	}

	private void Relay_In_81()
	{
		int num = 0;
		Array forcefieldStage2GroupSpawnData = ForcefieldStage2GroupSpawnData;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_81.Length != num + forcefieldStage2GroupSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_81, num + forcefieldStage2GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage2GroupSpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_81, num, forcefieldStage2GroupSpawnData.Length);
		num += forcefieldStage2GroupSpawnData.Length;
		int num2 = 0;
		Array array = local_Stage2Forcefields_TankArray;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_81.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_81, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_81, num2, array.Length);
		num2 += array.Length;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_81.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_81, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_81);
	}

	private void Relay_Out_83()
	{
		Relay_In_76();
	}

	private void Relay_In_83()
	{
		int num = 0;
		Array forcefieldStage3GroupSpawnData = ForcefieldStage3GroupSpawnData;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_83.Length != num + forcefieldStage3GroupSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_83, num + forcefieldStage3GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage3GroupSpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_83, num, forcefieldStage3GroupSpawnData.Length);
		num += forcefieldStage3GroupSpawnData.Length;
		int num2 = 0;
		Array array = local_Stage3Forcefields_TankArray;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_83.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_83, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_83, num2, array.Length);
		num2 += array.Length;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_83.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_83, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_83);
	}

	private void Relay_In_86()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_86.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_86.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_Out_90()
	{
		Relay_In_95();
	}

	private void Relay_In_90()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_90 = Stage1_Enemy01_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_90 = Stage1_Enemy01_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_90 = Stage1_Enemy01_Kill_Trig;
		int num = 0;
		Array stage1_Enemy01_Data = Stage1_Enemy01_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_90.Length != num + stage1_Enemy01_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_90, num + stage1_Enemy01_Data.Length);
		}
		Array.Copy(stage1_Enemy01_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_90, num, stage1_Enemy01_Data.Length);
		num += stage1_Enemy01_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_90.In(logic_SubGraph_GhostTechController_preSpawnTrigger_90, logic_SubGraph_GhostTechController_ghostSpawnTrigger_90, logic_SubGraph_GhostTechController_ghostKillTrigger_90, logic_SubGraph_GhostTechController_ghostTechSpawnData_90, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_90);
	}

	private void Relay_Out_95()
	{
		Relay_In_97();
	}

	private void Relay_In_95()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_95 = Stage1_Enemy02_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_95 = Stage1_Enemy02_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_95 = Stage1_Enemy02_Kill_Trig;
		int num = 0;
		Array stage1_Enemy02_Data = Stage1_Enemy02_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_95.Length != num + stage1_Enemy02_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_95, num + stage1_Enemy02_Data.Length);
		}
		Array.Copy(stage1_Enemy02_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_95, num, stage1_Enemy02_Data.Length);
		num += stage1_Enemy02_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_95.In(logic_SubGraph_GhostTechController_preSpawnTrigger_95, logic_SubGraph_GhostTechController_ghostSpawnTrigger_95, logic_SubGraph_GhostTechController_ghostKillTrigger_95, logic_SubGraph_GhostTechController_ghostTechSpawnData_95, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_95);
	}

	private void Relay_Out_97()
	{
		Relay_In_104();
	}

	private void Relay_In_97()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_97 = Stage1_Enemy03_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_97 = Stage1_Enemy03_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_97 = Stage1_Enemy03_Kill_Trig;
		int num = 0;
		Array stage1_Enemy03_Data = Stage1_Enemy03_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_97.Length != num + stage1_Enemy03_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_97, num + stage1_Enemy03_Data.Length);
		}
		Array.Copy(stage1_Enemy03_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_97, num, stage1_Enemy03_Data.Length);
		num += stage1_Enemy03_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_97.In(logic_SubGraph_GhostTechController_preSpawnTrigger_97, logic_SubGraph_GhostTechController_ghostSpawnTrigger_97, logic_SubGraph_GhostTechController_ghostKillTrigger_97, logic_SubGraph_GhostTechController_ghostTechSpawnData_97, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_97);
	}

	private void Relay_Out_104()
	{
	}

	private void Relay_In_104()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_104 = Stage1_Enemy04_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_104 = Stage1_Enemy04_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_104 = Stage1_Enemy04_Kill_Trig;
		int num = 0;
		Array stage1_Enemy04_Data = Stage1_Enemy04_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_104.Length != num + stage1_Enemy04_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_104, num + stage1_Enemy04_Data.Length);
		}
		Array.Copy(stage1_Enemy04_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_104, num, stage1_Enemy04_Data.Length);
		num += stage1_Enemy04_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_104.In(logic_SubGraph_GhostTechController_preSpawnTrigger_104, logic_SubGraph_GhostTechController_ghostSpawnTrigger_104, logic_SubGraph_GhostTechController_ghostKillTrigger_104, logic_SubGraph_GhostTechController_ghostTechSpawnData_104, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_104);
	}

	private void Relay_In_109()
	{
		int num = 0;
		Array forcefieldStage2GroupSpawnData = ForcefieldStage2GroupSpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_109.Length != num + forcefieldStage2GroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_109, num + forcefieldStage2GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage2GroupSpawnData, 0, logic_uScript_DestroyTechsFromData_techData_109, num, forcefieldStage2GroupSpawnData.Length);
		num += forcefieldStage2GroupSpawnData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_109 = owner_Connection_112;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_109.In(logic_uScript_DestroyTechsFromData_techData_109, logic_uScript_DestroyTechsFromData_shouldExplode_109, logic_uScript_DestroyTechsFromData_ownerNode_109);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_109.Out)
		{
			Relay_InitialSpawn_110();
		}
	}

	private void Relay_InitialSpawn_110()
	{
		int num = 0;
		Array forcefieldStage3GroupSpawnData = ForcefieldStage3GroupSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_110.Length != num + forcefieldStage3GroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_110, num + forcefieldStage3GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage3GroupSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_110, num, forcefieldStage3GroupSpawnData.Length);
		num += forcefieldStage3GroupSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_110 = owner_Connection_108;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_110.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_110, logic_uScript_SpawnTechsFromData_ownerNode_110, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_110, logic_uScript_SpawnTechsFromData_allowResurrection_110);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_110.Out)
		{
			Relay_True_132();
		}
	}

	private void Relay_InitialSpawn_113()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData02;
		if (logic_uScript_SpawnTechsFromData_spawnData_113.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_113, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_113, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_113 = owner_Connection_115;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_113.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_113, logic_uScript_SpawnTechsFromData_ownerNode_113, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_113, logic_uScript_SpawnTechsFromData_allowResurrection_113);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_113.Out)
		{
			Relay_True_135();
		}
	}

	private void Relay_In_114()
	{
		int num = 0;
		Array forcefieldStage3GroupSpawnData = ForcefieldStage3GroupSpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_114.Length != num + forcefieldStage3GroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_114, num + forcefieldStage3GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage3GroupSpawnData, 0, logic_uScript_DestroyTechsFromData_techData_114, num, forcefieldStage3GroupSpawnData.Length);
		num += forcefieldStage3GroupSpawnData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_114 = owner_Connection_118;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_114.In(logic_uScript_DestroyTechsFromData_techData_114, logic_uScript_DestroyTechsFromData_shouldExplode_114, logic_uScript_DestroyTechsFromData_ownerNode_114);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_114.Out)
		{
			Relay_InitialSpawn_381();
		}
	}

	private void Relay_In_122()
	{
		int num = 0;
		Array forcefieldStage1GroupSpawnData = ForcefieldStage1GroupSpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_122.Length != num + forcefieldStage1GroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_122, num + forcefieldStage1GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage1GroupSpawnData, 0, logic_uScript_DestroyTechsFromData_techData_122, num, forcefieldStage1GroupSpawnData.Length);
		num += forcefieldStage1GroupSpawnData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_122 = owner_Connection_120;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_122.In(logic_uScript_DestroyTechsFromData_techData_122, logic_uScript_DestroyTechsFromData_shouldExplode_122, logic_uScript_DestroyTechsFromData_ownerNode_122);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_122.Out)
		{
			Relay_InitialSpawn_123();
		}
	}

	private void Relay_InitialSpawn_123()
	{
		int num = 0;
		Array forcefieldStage2GroupSpawnData = ForcefieldStage2GroupSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_123.Length != num + forcefieldStage2GroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_123, num + forcefieldStage2GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage2GroupSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_123, num, forcefieldStage2GroupSpawnData.Length);
		num += forcefieldStage2GroupSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_123 = owner_Connection_119;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_123.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_123, logic_uScript_SpawnTechsFromData_ownerNode_123, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_123, logic_uScript_SpawnTechsFromData_allowResurrection_123);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_123.Out)
		{
			Relay_True_124();
		}
	}

	private void Relay_True_124()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.True(out logic_uScriptAct_SetBool_Target_124);
		local_Satege2Init_System_Boolean = logic_uScriptAct_SetBool_Target_124;
	}

	private void Relay_False_124()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.False(out logic_uScriptAct_SetBool_Target_124);
		local_Satege2Init_System_Boolean = logic_uScriptAct_SetBool_Target_124;
	}

	private void Relay_In_126()
	{
		logic_uScriptCon_CompareBool_Bool_126 = local_Satege2Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.In(logic_uScriptCon_CompareBool_Bool_126);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.False;
		if (num)
		{
			Relay_In_137();
		}
		if (flag)
		{
			Relay_In_122();
		}
	}

	private void Relay_In_129()
	{
		logic_uScriptCon_CompareBool_Bool_129 = local_Satege3Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.In(logic_uScriptCon_CompareBool_Bool_129);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.False;
		if (num)
		{
			Relay_In_199();
		}
		if (flag)
		{
			Relay_In_109();
		}
	}

	private void Relay_True_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.True(out logic_uScriptAct_SetBool_Target_132);
		local_Satege3Init_System_Boolean = logic_uScriptAct_SetBool_Target_132;
	}

	private void Relay_False_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.False(out logic_uScriptAct_SetBool_Target_132);
		local_Satege3Init_System_Boolean = logic_uScriptAct_SetBool_Target_132;
	}

	private void Relay_In_133()
	{
		logic_uScriptCon_CompareBool_Bool_133 = local_EndInit_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_133.In(logic_uScriptCon_CompareBool_Bool_133);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_133.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_133.False;
		if (num)
		{
			Relay_In_385();
		}
		if (flag)
		{
			Relay_In_114();
		}
	}

	private void Relay_True_135()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_135.True(out logic_uScriptAct_SetBool_Target_135);
		local_EndInit_System_Boolean = logic_uScriptAct_SetBool_Target_135;
	}

	private void Relay_False_135()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_135.False(out logic_uScriptAct_SetBool_Target_135);
		local_EndInit_System_Boolean = logic_uScriptAct_SetBool_Target_135;
	}

	private void Relay_In_137()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_137.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_137.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_Out_138()
	{
		Relay_In_147();
	}

	private void Relay_In_138()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_138 = Stage2_Enemy01_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_138 = Stage2_Enemy01_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_138 = Stage2_Enemy01_Kill_Trig;
		int num = 0;
		Array stage2_Enemy01_Data = Stage2_Enemy01_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_138.Length != num + stage2_Enemy01_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_138, num + stage2_Enemy01_Data.Length);
		}
		Array.Copy(stage2_Enemy01_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_138, num, stage2_Enemy01_Data.Length);
		num += stage2_Enemy01_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_138.In(logic_SubGraph_GhostTechController_preSpawnTrigger_138, logic_SubGraph_GhostTechController_ghostSpawnTrigger_138, logic_SubGraph_GhostTechController_ghostKillTrigger_138, logic_SubGraph_GhostTechController_ghostTechSpawnData_138, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_138);
	}

	private void Relay_Out_147()
	{
		Relay_In_157();
	}

	private void Relay_In_147()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_147 = Stage2_Enemy02_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_147 = Stage2_Enemy02_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_147 = Stage2_Enemy02_Kill_Trig;
		int num = 0;
		Array stage2_Enemy02_Data = Stage2_Enemy02_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_147.Length != num + stage2_Enemy02_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_147, num + stage2_Enemy02_Data.Length);
		}
		Array.Copy(stage2_Enemy02_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_147, num, stage2_Enemy02_Data.Length);
		num += stage2_Enemy02_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_147.In(logic_SubGraph_GhostTechController_preSpawnTrigger_147, logic_SubGraph_GhostTechController_ghostSpawnTrigger_147, logic_SubGraph_GhostTechController_ghostKillTrigger_147, logic_SubGraph_GhostTechController_ghostTechSpawnData_147, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_147);
	}

	private void Relay_Save_Out_151()
	{
		Relay_Save_152();
	}

	private void Relay_Load_Out_151()
	{
		Relay_Load_152();
	}

	private void Relay_Restart_Out_151()
	{
		Relay_Set_False_152();
	}

	private void Relay_Save_151()
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = local_Satege2Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_151 = local_Satege2Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Save(ref logic_SubGraph_SaveLoadBool_boolean_151, logic_SubGraph_SaveLoadBool_boolAsVariable_151, logic_SubGraph_SaveLoadBool_uniqueID_151);
	}

	private void Relay_Load_151()
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = local_Satege2Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_151 = local_Satege2Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Load(ref logic_SubGraph_SaveLoadBool_boolean_151, logic_SubGraph_SaveLoadBool_boolAsVariable_151, logic_SubGraph_SaveLoadBool_uniqueID_151);
	}

	private void Relay_Set_True_151()
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = local_Satege2Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_151 = local_Satege2Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_151, logic_SubGraph_SaveLoadBool_boolAsVariable_151, logic_SubGraph_SaveLoadBool_uniqueID_151);
	}

	private void Relay_Set_False_151()
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = local_Satege2Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_151 = local_Satege2Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_151, logic_SubGraph_SaveLoadBool_boolAsVariable_151, logic_SubGraph_SaveLoadBool_uniqueID_151);
	}

	private void Relay_Save_Out_152()
	{
		Relay_Save_153();
	}

	private void Relay_Load_Out_152()
	{
		Relay_Load_153();
	}

	private void Relay_Restart_Out_152()
	{
		Relay_Set_False_153();
	}

	private void Relay_Save_152()
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = local_Satege3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_152 = local_Satege3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Save(ref logic_SubGraph_SaveLoadBool_boolean_152, logic_SubGraph_SaveLoadBool_boolAsVariable_152, logic_SubGraph_SaveLoadBool_uniqueID_152);
	}

	private void Relay_Load_152()
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = local_Satege3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_152 = local_Satege3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Load(ref logic_SubGraph_SaveLoadBool_boolean_152, logic_SubGraph_SaveLoadBool_boolAsVariable_152, logic_SubGraph_SaveLoadBool_uniqueID_152);
	}

	private void Relay_Set_True_152()
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = local_Satege3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_152 = local_Satege3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_152, logic_SubGraph_SaveLoadBool_boolAsVariable_152, logic_SubGraph_SaveLoadBool_uniqueID_152);
	}

	private void Relay_Set_False_152()
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = local_Satege3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_152 = local_Satege3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_152, logic_SubGraph_SaveLoadBool_boolAsVariable_152, logic_SubGraph_SaveLoadBool_uniqueID_152);
	}

	private void Relay_Save_Out_153()
	{
		Relay_Save_403();
	}

	private void Relay_Load_Out_153()
	{
		Relay_Load_403();
	}

	private void Relay_Restart_Out_153()
	{
		Relay_Set_False_403();
	}

	private void Relay_Save_153()
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = local_EndInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_153 = local_EndInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Save(ref logic_SubGraph_SaveLoadBool_boolean_153, logic_SubGraph_SaveLoadBool_boolAsVariable_153, logic_SubGraph_SaveLoadBool_uniqueID_153);
	}

	private void Relay_Load_153()
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = local_EndInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_153 = local_EndInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Load(ref logic_SubGraph_SaveLoadBool_boolean_153, logic_SubGraph_SaveLoadBool_boolAsVariable_153, logic_SubGraph_SaveLoadBool_uniqueID_153);
	}

	private void Relay_Set_True_153()
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = local_EndInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_153 = local_EndInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_153, logic_SubGraph_SaveLoadBool_boolAsVariable_153, logic_SubGraph_SaveLoadBool_uniqueID_153);
	}

	private void Relay_Set_False_153()
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = local_EndInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_153 = local_EndInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_153, logic_SubGraph_SaveLoadBool_boolAsVariable_153, logic_SubGraph_SaveLoadBool_uniqueID_153);
	}

	private void Relay_Out_157()
	{
		Relay_In_161();
	}

	private void Relay_In_157()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_157 = Stage2_Enemy03_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_157 = Stage2_Enemy03_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_157 = Stage2_Enemy03_Kill_Trig;
		int num = 0;
		Array stage2_Enemy03_Data = Stage2_Enemy03_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_157.Length != num + stage2_Enemy03_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_157, num + stage2_Enemy03_Data.Length);
		}
		Array.Copy(stage2_Enemy03_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_157, num, stage2_Enemy03_Data.Length);
		num += stage2_Enemy03_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_157.In(logic_SubGraph_GhostTechController_preSpawnTrigger_157, logic_SubGraph_GhostTechController_ghostSpawnTrigger_157, logic_SubGraph_GhostTechController_ghostKillTrigger_157, logic_SubGraph_GhostTechController_ghostTechSpawnData_157, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_157);
	}

	private void Relay_Out_161()
	{
		Relay_In_166();
	}

	private void Relay_In_161()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_161 = Stage2_Enemy04_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_161 = Stage2_Enemy04_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_161 = Stage2_Enemy04_Kill_Trig;
		int num = 0;
		Array stage2_Enemy04_Data = Stage2_Enemy04_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_161.Length != num + stage2_Enemy04_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_161, num + stage2_Enemy04_Data.Length);
		}
		Array.Copy(stage2_Enemy04_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_161, num, stage2_Enemy04_Data.Length);
		num += stage2_Enemy04_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_161.In(logic_SubGraph_GhostTechController_preSpawnTrigger_161, logic_SubGraph_GhostTechController_ghostSpawnTrigger_161, logic_SubGraph_GhostTechController_ghostKillTrigger_161, logic_SubGraph_GhostTechController_ghostTechSpawnData_161, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_161);
	}

	private void Relay_Out_166()
	{
		Relay_In_171();
	}

	private void Relay_In_166()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_166 = Stage2_Enemy05_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_166 = Stage2_Enemy05_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_166 = Stage2_Enemy05_Kill_Trig;
		int num = 0;
		Array stage2_Enemy05_Data = Stage2_Enemy05_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_166.Length != num + stage2_Enemy05_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_166, num + stage2_Enemy05_Data.Length);
		}
		Array.Copy(stage2_Enemy05_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_166, num, stage2_Enemy05_Data.Length);
		num += stage2_Enemy05_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_166.In(logic_SubGraph_GhostTechController_preSpawnTrigger_166, logic_SubGraph_GhostTechController_ghostSpawnTrigger_166, logic_SubGraph_GhostTechController_ghostKillTrigger_166, logic_SubGraph_GhostTechController_ghostTechSpawnData_166, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_166);
	}

	private void Relay_Out_171()
	{
		Relay_In_178();
	}

	private void Relay_In_171()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_171 = Stage2_Enemy06_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_171 = Stage2_Enemy06_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_171 = Stage2_Enemy06_Kill_Trig;
		int num = 0;
		Array stage2_Enemy06_Data = Stage2_Enemy06_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_171.Length != num + stage2_Enemy06_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_171, num + stage2_Enemy06_Data.Length);
		}
		Array.Copy(stage2_Enemy06_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_171, num, stage2_Enemy06_Data.Length);
		num += stage2_Enemy06_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_171.In(logic_SubGraph_GhostTechController_preSpawnTrigger_171, logic_SubGraph_GhostTechController_ghostSpawnTrigger_171, logic_SubGraph_GhostTechController_ghostKillTrigger_171, logic_SubGraph_GhostTechController_ghostTechSpawnData_171, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_171);
	}

	private void Relay_Out_178()
	{
		Relay_In_182();
	}

	private void Relay_In_178()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_178 = Stage2_Enemy07_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_178 = Stage2_Enemy07_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_178 = Stage2_Enemy07_Kill_Trig;
		int num = 0;
		Array stage2_Enemy07_Data = Stage2_Enemy07_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_178.Length != num + stage2_Enemy07_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_178, num + stage2_Enemy07_Data.Length);
		}
		Array.Copy(stage2_Enemy07_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_178, num, stage2_Enemy07_Data.Length);
		num += stage2_Enemy07_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_178.In(logic_SubGraph_GhostTechController_preSpawnTrigger_178, logic_SubGraph_GhostTechController_ghostSpawnTrigger_178, logic_SubGraph_GhostTechController_ghostKillTrigger_178, logic_SubGraph_GhostTechController_ghostTechSpawnData_178, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_178);
	}

	private void Relay_Out_182()
	{
		Relay_In_187();
	}

	private void Relay_In_182()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_182 = Stage2_Enemy08_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_182 = Stage2_Enemy08_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_182 = Stage2_Enemy08_Kill_Trig;
		int num = 0;
		Array stage2_Enemy08_Data = Stage2_Enemy08_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_182.Length != num + stage2_Enemy08_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_182, num + stage2_Enemy08_Data.Length);
		}
		Array.Copy(stage2_Enemy08_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_182, num, stage2_Enemy08_Data.Length);
		num += stage2_Enemy08_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_182.In(logic_SubGraph_GhostTechController_preSpawnTrigger_182, logic_SubGraph_GhostTechController_ghostSpawnTrigger_182, logic_SubGraph_GhostTechController_ghostKillTrigger_182, logic_SubGraph_GhostTechController_ghostTechSpawnData_182, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_182);
	}

	private void Relay_Out_187()
	{
		Relay_In_191();
	}

	private void Relay_In_187()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_187 = Stage2_Enemy09_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_187 = Stage2_Enemy09_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_187 = Stage2_Enemy09_Kill_Trig;
		int num = 0;
		Array stage2_Enemy09_Data = Stage2_Enemy09_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_187.Length != num + stage2_Enemy09_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_187, num + stage2_Enemy09_Data.Length);
		}
		Array.Copy(stage2_Enemy09_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_187, num, stage2_Enemy09_Data.Length);
		num += stage2_Enemy09_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_187.In(logic_SubGraph_GhostTechController_preSpawnTrigger_187, logic_SubGraph_GhostTechController_ghostSpawnTrigger_187, logic_SubGraph_GhostTechController_ghostKillTrigger_187, logic_SubGraph_GhostTechController_ghostTechSpawnData_187, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_187);
	}

	private void Relay_Out_191()
	{
		Relay_In_392();
	}

	private void Relay_In_191()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_191 = Stage2_Enemy10_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_191 = Stage2_Enemy10_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_191 = Stage2_Enemy10_Kill_Trig;
		int num = 0;
		Array stage2_Enemy10_Data = Stage2_Enemy10_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_191.Length != num + stage2_Enemy10_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_191, num + stage2_Enemy10_Data.Length);
		}
		Array.Copy(stage2_Enemy10_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_191, num, stage2_Enemy10_Data.Length);
		num += stage2_Enemy10_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_191.In(logic_SubGraph_GhostTechController_preSpawnTrigger_191, logic_SubGraph_GhostTechController_ghostSpawnTrigger_191, logic_SubGraph_GhostTechController_ghostKillTrigger_191, logic_SubGraph_GhostTechController_ghostTechSpawnData_191, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_191);
	}

	private void Relay_Out_197()
	{
	}

	private void Relay_In_197()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_197 = Stage2_Enemy10_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_197 = Stage2_Enemy10_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_197 = Stage2_Enemy10_Kill_Trig;
		int num = 0;
		Array stage2_Enemy11_Data = Stage2_Enemy11_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_197.Length != num + stage2_Enemy11_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_197, num + stage2_Enemy11_Data.Length);
		}
		Array.Copy(stage2_Enemy11_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_197, num, stage2_Enemy11_Data.Length);
		num += stage2_Enemy11_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_197.In(logic_SubGraph_GhostTechController_preSpawnTrigger_197, logic_SubGraph_GhostTechController_ghostSpawnTrigger_197, logic_SubGraph_GhostTechController_ghostKillTrigger_197, logic_SubGraph_GhostTechController_ghostTechSpawnData_197, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_197);
	}

	private void Relay_In_199()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199.Out)
		{
			Relay_In_203();
		}
	}

	private void Relay_Out_203()
	{
		Relay_In_205();
	}

	private void Relay_In_203()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_203 = Stage3_Enemy01_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_203 = Stage3_Enemy01_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_203 = Stage3_Enemy01_Kill_Trig;
		int num = 0;
		Array stage3_Enemy01_Data = Stage3_Enemy01_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_203.Length != num + stage3_Enemy01_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_203, num + stage3_Enemy01_Data.Length);
		}
		Array.Copy(stage3_Enemy01_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_203, num, stage3_Enemy01_Data.Length);
		num += stage3_Enemy01_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_203.In(logic_SubGraph_GhostTechController_preSpawnTrigger_203, logic_SubGraph_GhostTechController_ghostSpawnTrigger_203, logic_SubGraph_GhostTechController_ghostKillTrigger_203, logic_SubGraph_GhostTechController_ghostTechSpawnData_203, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_203);
	}

	private void Relay_Out_205()
	{
		Relay_In_216();
	}

	private void Relay_In_205()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_205 = Stage3_Enemy02_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_205 = Stage3_Enemy02_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_205 = Stage3_Enemy02_Kill_Trig;
		int num = 0;
		Array stage3_Enemy02_Data = Stage3_Enemy02_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_205.Length != num + stage3_Enemy02_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_205, num + stage3_Enemy02_Data.Length);
		}
		Array.Copy(stage3_Enemy02_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_205, num, stage3_Enemy02_Data.Length);
		num += stage3_Enemy02_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_205.In(logic_SubGraph_GhostTechController_preSpawnTrigger_205, logic_SubGraph_GhostTechController_ghostSpawnTrigger_205, logic_SubGraph_GhostTechController_ghostKillTrigger_205, logic_SubGraph_GhostTechController_ghostTechSpawnData_205, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_205);
	}

	private void Relay_Out_214()
	{
	}

	private void Relay_In_214()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_214 = Stage3_Enemy03_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_214 = Stage3_Enemy03_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_214 = Stage3_Enemy03_Kill_Trig;
		int num = 0;
		Array stage3_Enemy03_Data = Stage3_Enemy03_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_214.Length != num + stage3_Enemy03_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_214, num + stage3_Enemy03_Data.Length);
		}
		Array.Copy(stage3_Enemy03_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_214, num, stage3_Enemy03_Data.Length);
		num += stage3_Enemy03_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_214.In(logic_SubGraph_GhostTechController_preSpawnTrigger_214, logic_SubGraph_GhostTechController_ghostSpawnTrigger_214, logic_SubGraph_GhostTechController_ghostKillTrigger_214, logic_SubGraph_GhostTechController_ghostTechSpawnData_214, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_214);
	}

	private void Relay_Out_216()
	{
		Relay_In_223();
	}

	private void Relay_In_216()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_216 = Stage3_Enemy04_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_216 = Stage3_Enemy04_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_216 = Stage3_Enemy04_Kill_Trig;
		int num = 0;
		Array stage3_Enemy04_Data = Stage3_Enemy04_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_216.Length != num + stage3_Enemy04_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_216, num + stage3_Enemy04_Data.Length);
		}
		Array.Copy(stage3_Enemy04_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_216, num, stage3_Enemy04_Data.Length);
		num += stage3_Enemy04_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_216.In(logic_SubGraph_GhostTechController_preSpawnTrigger_216, logic_SubGraph_GhostTechController_ghostSpawnTrigger_216, logic_SubGraph_GhostTechController_ghostKillTrigger_216, logic_SubGraph_GhostTechController_ghostTechSpawnData_216, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_216);
	}

	private void Relay_Out_223()
	{
		Relay_In_234();
	}

	private void Relay_In_223()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_223 = Stage3_Enemy05_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_223 = Stage3_Enemy05_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_223 = Stage3_Enemy05_Kill_Trig;
		int num = 0;
		Array stage3_Enemy05_Data = Stage3_Enemy05_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_223.Length != num + stage3_Enemy05_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_223, num + stage3_Enemy05_Data.Length);
		}
		Array.Copy(stage3_Enemy05_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_223, num, stage3_Enemy05_Data.Length);
		num += stage3_Enemy05_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_223.In(logic_SubGraph_GhostTechController_preSpawnTrigger_223, logic_SubGraph_GhostTechController_ghostSpawnTrigger_223, logic_SubGraph_GhostTechController_ghostKillTrigger_223, logic_SubGraph_GhostTechController_ghostTechSpawnData_223, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_223);
	}

	private void Relay_Out_225()
	{
	}

	private void Relay_In_225()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_225 = Stage3_Enemy06_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_225 = Stage3_Enemy06_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_225 = Stage3_Enemy06_Kill_Trig;
		int num = 0;
		Array stage3_Enemy06_Data = Stage3_Enemy06_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_225.Length != num + stage3_Enemy06_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_225, num + stage3_Enemy06_Data.Length);
		}
		Array.Copy(stage3_Enemy06_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_225, num, stage3_Enemy06_Data.Length);
		num += stage3_Enemy06_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_225.In(logic_SubGraph_GhostTechController_preSpawnTrigger_225, logic_SubGraph_GhostTechController_ghostSpawnTrigger_225, logic_SubGraph_GhostTechController_ghostKillTrigger_225, logic_SubGraph_GhostTechController_ghostTechSpawnData_225, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_225);
	}

	private void Relay_Out_234()
	{
		Relay_In_236();
	}

	private void Relay_In_234()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_234 = Stage3_Enemy07_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_234 = Stage3_Enemy07_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_234 = Stage3_Enemy07_Kill_Trig;
		int num = 0;
		Array stage3_Enemy07_Data = Stage3_Enemy07_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_234.Length != num + stage3_Enemy07_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_234, num + stage3_Enemy07_Data.Length);
		}
		Array.Copy(stage3_Enemy07_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_234, num, stage3_Enemy07_Data.Length);
		num += stage3_Enemy07_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_234.In(logic_SubGraph_GhostTechController_preSpawnTrigger_234, logic_SubGraph_GhostTechController_ghostSpawnTrigger_234, logic_SubGraph_GhostTechController_ghostKillTrigger_234, logic_SubGraph_GhostTechController_ghostTechSpawnData_234, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_234);
	}

	private void Relay_Out_236()
	{
		Relay_In_243();
	}

	private void Relay_In_236()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_236 = Stage3_Enemy08_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_236 = Stage3_Enemy08_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_236 = Stage3_Enemy08_Kill_Trig;
		int num = 0;
		Array stage3_Enemy08_Data = Stage3_Enemy08_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_236.Length != num + stage3_Enemy08_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_236, num + stage3_Enemy08_Data.Length);
		}
		Array.Copy(stage3_Enemy08_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_236, num, stage3_Enemy08_Data.Length);
		num += stage3_Enemy08_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_236.In(logic_SubGraph_GhostTechController_preSpawnTrigger_236, logic_SubGraph_GhostTechController_ghostSpawnTrigger_236, logic_SubGraph_GhostTechController_ghostKillTrigger_236, logic_SubGraph_GhostTechController_ghostTechSpawnData_236, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_236);
	}

	private void Relay_Out_243()
	{
		Relay_In_254();
	}

	private void Relay_In_243()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_243 = Stage3_Enemy09_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_243 = Stage3_Enemy09_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_243 = Stage3_Enemy09_Kill_Trig;
		int num = 0;
		Array stage3_Enemy09_Data = Stage3_Enemy09_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_243.Length != num + stage3_Enemy09_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_243, num + stage3_Enemy09_Data.Length);
		}
		Array.Copy(stage3_Enemy09_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_243, num, stage3_Enemy09_Data.Length);
		num += stage3_Enemy09_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_243.In(logic_SubGraph_GhostTechController_preSpawnTrigger_243, logic_SubGraph_GhostTechController_ghostSpawnTrigger_243, logic_SubGraph_GhostTechController_ghostKillTrigger_243, logic_SubGraph_GhostTechController_ghostTechSpawnData_243, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_243);
	}

	private void Relay_Out_249()
	{
	}

	private void Relay_In_249()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_249 = Stage3_Enemy10_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_249 = Stage3_Enemy10_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_249 = Stage3_Enemy10_Kill_Trig;
		int num = 0;
		Array stage3_Enemy10_Data = Stage3_Enemy10_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_249.Length != num + stage3_Enemy10_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_249, num + stage3_Enemy10_Data.Length);
		}
		Array.Copy(stage3_Enemy10_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_249, num, stage3_Enemy10_Data.Length);
		num += stage3_Enemy10_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_249.In(logic_SubGraph_GhostTechController_preSpawnTrigger_249, logic_SubGraph_GhostTechController_ghostSpawnTrigger_249, logic_SubGraph_GhostTechController_ghostKillTrigger_249, logic_SubGraph_GhostTechController_ghostTechSpawnData_249, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_249);
	}

	private void Relay_Out_254()
	{
		Relay_In_261();
	}

	private void Relay_In_254()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_254 = Stage3_Enemy11_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_254 = Stage3_Enemy11_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_254 = Stage3_Enemy11_Kill_Trig;
		int num = 0;
		Array stage3_Enemy11_Data = Stage3_Enemy11_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_254.Length != num + stage3_Enemy11_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_254, num + stage3_Enemy11_Data.Length);
		}
		Array.Copy(stage3_Enemy11_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_254, num, stage3_Enemy11_Data.Length);
		num += stage3_Enemy11_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_254.In(logic_SubGraph_GhostTechController_preSpawnTrigger_254, logic_SubGraph_GhostTechController_ghostSpawnTrigger_254, logic_SubGraph_GhostTechController_ghostKillTrigger_254, logic_SubGraph_GhostTechController_ghostTechSpawnData_254, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_254);
	}

	private void Relay_Out_255()
	{
	}

	private void Relay_In_255()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_255 = Stage3_Enemy12_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_255 = Stage3_Enemy12_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_255 = Stage3_Enemy12_Kill_Trig;
		int num = 0;
		Array stage3_Enemy12_Data = Stage3_Enemy12_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_255.Length != num + stage3_Enemy12_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_255, num + stage3_Enemy12_Data.Length);
		}
		Array.Copy(stage3_Enemy12_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_255, num, stage3_Enemy12_Data.Length);
		num += stage3_Enemy12_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_255.In(logic_SubGraph_GhostTechController_preSpawnTrigger_255, logic_SubGraph_GhostTechController_ghostSpawnTrigger_255, logic_SubGraph_GhostTechController_ghostKillTrigger_255, logic_SubGraph_GhostTechController_ghostTechSpawnData_255, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_255);
	}

	private void Relay_Out_261()
	{
		Relay_In_266();
	}

	private void Relay_In_261()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_261 = Stage3_Enemy13_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_261 = Stage3_Enemy13_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_261 = Stage3_Enemy13_Kill_Trig;
		int num = 0;
		Array stage3_Enemy13_Data = Stage3_Enemy13_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_261.Length != num + stage3_Enemy13_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_261, num + stage3_Enemy13_Data.Length);
		}
		Array.Copy(stage3_Enemy13_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_261, num, stage3_Enemy13_Data.Length);
		num += stage3_Enemy13_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_261.In(logic_SubGraph_GhostTechController_preSpawnTrigger_261, logic_SubGraph_GhostTechController_ghostSpawnTrigger_261, logic_SubGraph_GhostTechController_ghostKillTrigger_261, logic_SubGraph_GhostTechController_ghostTechSpawnData_261, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_261);
	}

	private void Relay_Out_266()
	{
		Relay_In_271();
	}

	private void Relay_In_266()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_266 = Stage3_Enemy14_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_266 = Stage3_Enemy14_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_266 = Stage3_Enemy14_Kill_Trig;
		int num = 0;
		Array stage3_Enemy14_Data = Stage3_Enemy14_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_266.Length != num + stage3_Enemy14_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_266, num + stage3_Enemy14_Data.Length);
		}
		Array.Copy(stage3_Enemy14_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_266, num, stage3_Enemy14_Data.Length);
		num += stage3_Enemy14_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_266.In(logic_SubGraph_GhostTechController_preSpawnTrigger_266, logic_SubGraph_GhostTechController_ghostSpawnTrigger_266, logic_SubGraph_GhostTechController_ghostKillTrigger_266, logic_SubGraph_GhostTechController_ghostTechSpawnData_266, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_266);
	}

	private void Relay_Out_271()
	{
		Relay_In_275();
	}

	private void Relay_In_271()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_271 = Stage3_Enemy15_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_271 = Stage3_Enemy15_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_271 = Stage3_Enemy15_Kill_Trig;
		int num = 0;
		Array stage3_Enemy15_Data = Stage3_Enemy15_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_271.Length != num + stage3_Enemy15_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_271, num + stage3_Enemy15_Data.Length);
		}
		Array.Copy(stage3_Enemy15_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_271, num, stage3_Enemy15_Data.Length);
		num += stage3_Enemy15_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_271.In(logic_SubGraph_GhostTechController_preSpawnTrigger_271, logic_SubGraph_GhostTechController_ghostSpawnTrigger_271, logic_SubGraph_GhostTechController_ghostKillTrigger_271, logic_SubGraph_GhostTechController_ghostTechSpawnData_271, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_271);
	}

	private void Relay_Out_275()
	{
		Relay_In_395();
	}

	private void Relay_In_275()
	{
		logic_SubGraph_GhostTechController_preSpawnTrigger_275 = Stage3_Enemy16_PreSpawn_Trig;
		logic_SubGraph_GhostTechController_ghostSpawnTrigger_275 = Stage3_Enemy16_Spawn_Trig;
		logic_SubGraph_GhostTechController_ghostKillTrigger_275 = Stage3_Enemy16_Kill_Trig;
		int num = 0;
		Array stage3_Enemy16_Data = Stage3_Enemy16_Data;
		if (logic_SubGraph_GhostTechController_ghostTechSpawnData_275.Length != num + stage3_Enemy16_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_GhostTechController_ghostTechSpawnData_275, num + stage3_Enemy16_Data.Length);
		}
		Array.Copy(stage3_Enemy16_Data, 0, logic_SubGraph_GhostTechController_ghostTechSpawnData_275, num, stage3_Enemy16_Data.Length);
		num += stage3_Enemy16_Data.Length;
		logic_SubGraph_GhostTechController_SubGraph_GhostTechController_275.In(logic_SubGraph_GhostTechController_preSpawnTrigger_275, logic_SubGraph_GhostTechController_ghostSpawnTrigger_275, logic_SubGraph_GhostTechController_ghostKillTrigger_275, logic_SubGraph_GhostTechController_ghostTechSpawnData_275, logic_SubGraph_GhostTechController_UsePreSpawnTrigger_275);
	}

	private void Relay_In_282()
	{
		int num = 0;
		Array array = local_FlyingTechs_TankArray;
		if (logic_uScript_DamageTechs_techs_282.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_282, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_282, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_282.In(logic_uScript_DamageTechs_techs_282, logic_uScript_DamageTechs_dmgPercent_282, logic_uScript_DamageTechs_givePlyrCredit_282, logic_uScript_DamageTechs_leaveBlksPercent_282, logic_uScript_DamageTechs_makeVulnerable_282);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_282.Out)
		{
			Relay_In_500();
		}
	}

	private void Relay_In_283()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_283.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_283.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_284()
	{
		logic_uScript_SetDangerMusicMisc_tech_284 = local_curTank_Tank;
		logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_284.In(logic_uScript_SetDangerMusicMisc_miscDangerMusicType_284, logic_uScript_SetDangerMusicMisc_tech_284);
		if (logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_284.Out)
		{
			Relay_In_297();
		}
	}

	private void Relay_In_285()
	{
		logic_uScript_SetDangerMusicMisc_tech_285 = local_curTank_Tank;
		logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_285.In(logic_uScript_SetDangerMusicMisc_miscDangerMusicType_285, logic_uScript_SetDangerMusicMisc_tech_285);
		if (logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_285.Out)
		{
			Relay_In_290();
		}
	}

	private void Relay_In_286()
	{
		logic_uScript_GetTechsInTrigger_triggerAreaName_286 = MissionRangeTrig;
		logic_uScript_GetTechsInTrigger_uScript_GetTechsInTrigger_286.In(logic_uScript_GetTechsInTrigger_triggerAreaName_286, out logic_uScript_GetTechsInTrigger_Entered_286, out logic_uScript_GetTechsInTrigger_Inside_286, out logic_uScript_GetTechsInTrigger_Exited_286);
		local_NewlyEnteredTechs_TankArray = logic_uScript_GetTechsInTrigger_Entered_286;
		local_TechsInside_TankArray = logic_uScript_GetTechsInTrigger_Inside_286;
		local_NewlyExitedTechs_TankArray = logic_uScript_GetTechsInTrigger_Exited_286;
		if (logic_uScript_GetTechsInTrigger_uScript_GetTechsInTrigger_286.Out)
		{
			Relay_In_535();
		}
	}

	private void Relay_In_288()
	{
		logic_uScript_SetTimeOfDay_hour_288 = local_StartingHour_System_Int32;
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_288.In(logic_uScript_SetTimeOfDay_hour_288, logic_uScript_SetTimeOfDay_tech_288);
	}

	private void Relay_Reset_289()
	{
		int num = 0;
		Array array = local_NewlyEnteredTechs_TankArray;
		if (logic_uScript_ForEachListTech_List_289.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_289, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_289, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_289.Reset(logic_uScript_ForEachListTech_List_289, out logic_uScript_ForEachListTech_Value_289, out logic_uScript_ForEachListTech_currentIndex_289);
		local_curTank_Tank = logic_uScript_ForEachListTech_Value_289;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_289.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_289.Iteration;
		if (done)
		{
			Relay_In_298();
		}
		if (iteration)
		{
			Relay_In_285();
		}
	}

	private void Relay_In_289()
	{
		int num = 0;
		Array array = local_NewlyEnteredTechs_TankArray;
		if (logic_uScript_ForEachListTech_List_289.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_289, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_289, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_289.In(logic_uScript_ForEachListTech_List_289, out logic_uScript_ForEachListTech_Value_289, out logic_uScript_ForEachListTech_currentIndex_289);
		local_curTank_Tank = logic_uScript_ForEachListTech_Value_289;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_289.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_289.Iteration;
		if (done)
		{
			Relay_In_298();
		}
		if (iteration)
		{
			Relay_In_285();
		}
	}

	private void Relay_In_290()
	{
		logic_uScript_OverrideTankCameraDistanceMax_tech_290 = local_curTank_Tank;
		logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_290.In(logic_uScript_OverrideTankCameraDistanceMax_enable_290, logic_uScript_OverrideTankCameraDistanceMax_newDistanceMax_290, logic_uScript_OverrideTankCameraDistanceMax_tech_290);
		if (logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_290.Out)
		{
			Relay_In_510();
		}
	}

	private void Relay_Reset_295()
	{
		int num = 0;
		Array array = local_NewlyExitedTechs_TankArray;
		if (logic_uScript_ForEachListTech_List_295.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_295, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_295, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_295.Reset(logic_uScript_ForEachListTech_List_295, out logic_uScript_ForEachListTech_Value_295, out logic_uScript_ForEachListTech_currentIndex_295);
		local_curTank_Tank = logic_uScript_ForEachListTech_Value_295;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_295.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_295.Iteration;
		if (done)
		{
			Relay_In_289();
		}
		if (iteration)
		{
			Relay_In_284();
		}
	}

	private void Relay_In_295()
	{
		int num = 0;
		Array array = local_NewlyExitedTechs_TankArray;
		if (logic_uScript_ForEachListTech_List_295.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_295, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_295, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_295.In(logic_uScript_ForEachListTech_List_295, out logic_uScript_ForEachListTech_Value_295, out logic_uScript_ForEachListTech_currentIndex_295);
		local_curTank_Tank = logic_uScript_ForEachListTech_Value_295;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_295.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_295.Iteration;
		if (done)
		{
			Relay_In_289();
		}
		if (iteration)
		{
			Relay_In_284();
		}
	}

	private void Relay_In_297()
	{
		logic_uScript_OverrideTankCameraDistanceMax_tech_297 = local_curTank_Tank;
		logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_297.In(logic_uScript_OverrideTankCameraDistanceMax_enable_297, logic_uScript_OverrideTankCameraDistanceMax_newDistanceMax_297, logic_uScript_OverrideTankCameraDistanceMax_tech_297);
	}

	private void Relay_In_298()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_298.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_298.Out)
		{
			Relay_In_325();
		}
	}

	private void Relay_InitialSpawn_303()
	{
		int num = 0;
		Array terminal1SpawnData = Terminal1SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_303.Length != num + terminal1SpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_303, num + terminal1SpawnData.Length);
		}
		Array.Copy(terminal1SpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_303, num, terminal1SpawnData.Length);
		num += terminal1SpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_303 = owner_Connection_302;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_303.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_303, logic_uScript_SpawnTechsFromData_ownerNode_303, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_303, logic_uScript_SpawnTechsFromData_allowResurrection_303);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_303.Out)
		{
			Relay_InitialSpawn_305();
		}
	}

	private void Relay_InitialSpawn_305()
	{
		int num = 0;
		Array terminal2SpawnData = Terminal2SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_305.Length != num + terminal2SpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_305, num + terminal2SpawnData.Length);
		}
		Array.Copy(terminal2SpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_305, num, terminal2SpawnData.Length);
		num += terminal2SpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_305 = owner_Connection_304;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_305.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_305, logic_uScript_SpawnTechsFromData_ownerNode_305, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_305, logic_uScript_SpawnTechsFromData_allowResurrection_305);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_305.Out)
		{
			Relay_InitialSpawn_307();
		}
	}

	private void Relay_InitialSpawn_307()
	{
		int num = 0;
		Array terminal3SpawnData = Terminal3SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_307.Length != num + terminal3SpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_307, num + terminal3SpawnData.Length);
		}
		Array.Copy(terminal3SpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_307, num, terminal3SpawnData.Length);
		num += terminal3SpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_307 = owner_Connection_309;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_307.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_307, logic_uScript_SpawnTechsFromData_ownerNode_307, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_307, logic_uScript_SpawnTechsFromData_allowResurrection_307);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_307.Out)
		{
			Relay_In_377();
		}
	}

	private void Relay_In_313()
	{
		logic_uScript_SetBlockAnimationTrigger_block_313 = local_TerminalBlock1_TankBlock;
		logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_313.In(logic_uScript_SetBlockAnimationTrigger_block_313, logic_uScript_SetBlockAnimationTrigger_name_313);
		if (logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_313.Out)
		{
			Relay_In_389();
		}
	}

	private void Relay_In_314()
	{
		int num = 0;
		Array terminal1SpawnData = Terminal1SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_314.Length != num + terminal1SpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_314, num + terminal1SpawnData.Length);
		}
		Array.Copy(terminal1SpawnData, 0, logic_uScript_GetAndCheckTechs_techData_314, num, terminal1SpawnData.Length);
		num += terminal1SpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_314 = owner_Connection_315;
		int num2 = 0;
		Array array = local_Terminals1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_314.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_314, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_314, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_314 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_314.In(logic_uScript_GetAndCheckTechs_techData_314, logic_uScript_GetAndCheckTechs_ownerNode_314, ref logic_uScript_GetAndCheckTechs_techs_314);
		local_Terminals1_TankArray = logic_uScript_GetAndCheckTechs_techs_314;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_314.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_314.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_317();
		}
		if (someAlive)
		{
			Relay_AtIndex_317();
		}
	}

	private void Relay_AtIndex_317()
	{
		int num = 0;
		Array array = local_Terminals1_TankArray;
		if (logic_uScript_AccessListTech_techList_317.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_317, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_317, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_317.AtIndex(ref logic_uScript_AccessListTech_techList_317, logic_uScript_AccessListTech_index_317, out logic_uScript_AccessListTech_value_317);
		local_Terminals1_TankArray = logic_uScript_AccessListTech_techList_317;
		local_Terminal1Tech_Tank = logic_uScript_AccessListTech_value_317;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_317.Out)
		{
			Relay_In_319();
		}
	}

	private void Relay_In_319()
	{
		logic_uScript_GetTankBlock_tank_319 = local_Terminal1Tech_Tank;
		logic_uScript_GetTankBlock_blockType_319 = Terminal_Block;
		logic_uScript_GetTankBlock_Return_319 = logic_uScript_GetTankBlock_uScript_GetTankBlock_319.In(logic_uScript_GetTankBlock_tank_319, logic_uScript_GetTankBlock_blockType_319);
		local_TerminalBlock1_TankBlock = logic_uScript_GetTankBlock_Return_319;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_319.Returned)
		{
			Relay_In_313();
		}
	}

	private void Relay_Out_325()
	{
		Relay_In_326();
	}

	private void Relay_In_325()
	{
		int num = 0;
		Array terminal1SpawnData = Terminal1SpawnData;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_325.Length != num + terminal1SpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_325, num + terminal1SpawnData.Length);
		}
		Array.Copy(terminal1SpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_325, num, terminal1SpawnData.Length);
		num += terminal1SpawnData.Length;
		int num2 = 0;
		Array array = local_Terminals1_TankArray;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_325.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_325, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_325, num2, array.Length);
		num2 += array.Length;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_325.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_325, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_325);
	}

	private void Relay_Out_326()
	{
		Relay_In_327();
	}

	private void Relay_In_326()
	{
		int num = 0;
		Array terminal2SpawnData = Terminal2SpawnData;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_326.Length != num + terminal2SpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_326, num + terminal2SpawnData.Length);
		}
		Array.Copy(terminal2SpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_326, num, terminal2SpawnData.Length);
		num += terminal2SpawnData.Length;
		int num2 = 0;
		Array array = local_Terminals2_TankArray;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_326.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_326, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_326, num2, array.Length);
		num2 += array.Length;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_326.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_326, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_326);
	}

	private void Relay_Out_327()
	{
		Relay_In_486();
	}

	private void Relay_In_327()
	{
		int num = 0;
		Array terminal3SpawnData = Terminal3SpawnData;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_327.Length != num + terminal3SpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_327, num + terminal3SpawnData.Length);
		}
		Array.Copy(terminal3SpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_327, num, terminal3SpawnData.Length);
		num += terminal3SpawnData.Length;
		int num2 = 0;
		Array array = local_Terminals3_TankArray;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_327.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_327, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_327, num2, array.Length);
		num2 += array.Length;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_327.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_327, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_327);
	}

	private void Relay_AtIndex_331()
	{
		int num = 0;
		Array array = local_Terminals2_TankArray;
		if (logic_uScript_AccessListTech_techList_331.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_331, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_331, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_331.AtIndex(ref logic_uScript_AccessListTech_techList_331, logic_uScript_AccessListTech_index_331, out logic_uScript_AccessListTech_value_331);
		local_Terminals2_TankArray = logic_uScript_AccessListTech_techList_331;
		local_Terminal2Tech_Tank = logic_uScript_AccessListTech_value_331;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_331.Out)
		{
			Relay_In_334();
		}
	}

	private void Relay_In_334()
	{
		logic_uScript_GetTankBlock_tank_334 = local_Terminal2Tech_Tank;
		logic_uScript_GetTankBlock_blockType_334 = Terminal_Block;
		logic_uScript_GetTankBlock_Return_334 = logic_uScript_GetTankBlock_uScript_GetTankBlock_334.In(logic_uScript_GetTankBlock_tank_334, logic_uScript_GetTankBlock_blockType_334);
		local_TerminalBlock2_TankBlock = logic_uScript_GetTankBlock_Return_334;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_334.Returned)
		{
			Relay_In_339();
		}
	}

	private void Relay_In_337()
	{
		int num = 0;
		Array terminal2SpawnData = Terminal2SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_337.Length != num + terminal2SpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_337, num + terminal2SpawnData.Length);
		}
		Array.Copy(terminal2SpawnData, 0, logic_uScript_GetAndCheckTechs_techData_337, num, terminal2SpawnData.Length);
		num += terminal2SpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_337 = owner_Connection_332;
		int num2 = 0;
		Array array = local_Terminals2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_337.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_337, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_337, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_337 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_337.In(logic_uScript_GetAndCheckTechs_techData_337, logic_uScript_GetAndCheckTechs_ownerNode_337, ref logic_uScript_GetAndCheckTechs_techs_337);
		local_Terminals2_TankArray = logic_uScript_GetAndCheckTechs_techs_337;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_337.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_337.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_331();
		}
		if (someAlive)
		{
			Relay_AtIndex_331();
		}
	}

	private void Relay_In_339()
	{
		logic_uScript_SetBlockAnimationTrigger_block_339 = local_TerminalBlock2_TankBlock;
		logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_339.In(logic_uScript_SetBlockAnimationTrigger_block_339, logic_uScript_SetBlockAnimationTrigger_name_339);
		if (logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_339.Out)
		{
			Relay_In_397();
		}
	}

	private void Relay_In_344()
	{
		int num = 0;
		Array terminal3SpawnData = Terminal3SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_344.Length != num + terminal3SpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_344, num + terminal3SpawnData.Length);
		}
		Array.Copy(terminal3SpawnData, 0, logic_uScript_GetAndCheckTechs_techData_344, num, terminal3SpawnData.Length);
		num += terminal3SpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_344 = owner_Connection_342;
		int num2 = 0;
		Array array = local_Terminals3_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_344.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_344, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_344, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_344 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_344.In(logic_uScript_GetAndCheckTechs_techData_344, logic_uScript_GetAndCheckTechs_ownerNode_344, ref logic_uScript_GetAndCheckTechs_techs_344);
		local_Terminals3_TankArray = logic_uScript_GetAndCheckTechs_techs_344;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_344.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_344.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_349();
		}
		if (someAlive)
		{
			Relay_AtIndex_349();
		}
	}

	private void Relay_In_345()
	{
		logic_uScript_GetTankBlock_tank_345 = local_Terminal3Tech_Tank;
		logic_uScript_GetTankBlock_blockType_345 = Terminal_Block;
		logic_uScript_GetTankBlock_Return_345 = logic_uScript_GetTankBlock_uScript_GetTankBlock_345.In(logic_uScript_GetTankBlock_tank_345, logic_uScript_GetTankBlock_blockType_345);
		local_TerminalBlock3_TankBlock = logic_uScript_GetTankBlock_Return_345;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_345.Returned)
		{
			Relay_In_348();
		}
	}

	private void Relay_In_348()
	{
		logic_uScript_SetBlockAnimationTrigger_block_348 = local_TerminalBlock3_TankBlock;
		logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_348.In(logic_uScript_SetBlockAnimationTrigger_block_348, logic_uScript_SetBlockAnimationTrigger_name_348);
		if (logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_348.Out)
		{
			Relay_In_399();
		}
	}

	private void Relay_AtIndex_349()
	{
		int num = 0;
		Array array = local_Terminals3_TankArray;
		if (logic_uScript_AccessListTech_techList_349.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_349, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_349, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_349.AtIndex(ref logic_uScript_AccessListTech_techList_349, logic_uScript_AccessListTech_index_349, out logic_uScript_AccessListTech_value_349);
		local_Terminals3_TankArray = logic_uScript_AccessListTech_techList_349;
		local_Terminal3Tech_Tank = logic_uScript_AccessListTech_value_349;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_349.Out)
		{
			Relay_In_345();
		}
	}

	private void Relay_In_350()
	{
		logic_uScript_SetDangerMusicMisc_tech_350 = local_curTank_Tank;
		logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_350.In(logic_uScript_SetDangerMusicMisc_miscDangerMusicType_350, logic_uScript_SetDangerMusicMisc_tech_350);
		if (logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_350.Out)
		{
			Relay_In_354();
		}
	}

	private void Relay_Reset_352()
	{
		int num = 0;
		Array array = local_TechsInside_TankArray;
		if (logic_uScript_ForEachListTech_List_352.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_352, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_352, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_352.Reset(logic_uScript_ForEachListTech_List_352, out logic_uScript_ForEachListTech_Value_352, out logic_uScript_ForEachListTech_currentIndex_352);
		local_curTank_Tank = logic_uScript_ForEachListTech_Value_352;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_352.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_352.Iteration;
		if (done)
		{
			Relay_In_519();
		}
		if (iteration)
		{
			Relay_In_350();
		}
	}

	private void Relay_In_352()
	{
		int num = 0;
		Array array = local_TechsInside_TankArray;
		if (logic_uScript_ForEachListTech_List_352.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_352, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_352, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_352.In(logic_uScript_ForEachListTech_List_352, out logic_uScript_ForEachListTech_Value_352, out logic_uScript_ForEachListTech_currentIndex_352);
		local_curTank_Tank = logic_uScript_ForEachListTech_Value_352;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_352.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_352.Iteration;
		if (done)
		{
			Relay_In_519();
		}
		if (iteration)
		{
			Relay_In_350();
		}
	}

	private void Relay_In_354()
	{
		logic_uScript_OverrideTankCameraDistanceMax_tech_354 = local_curTank_Tank;
		logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_354.In(logic_uScript_OverrideTankCameraDistanceMax_enable_354, logic_uScript_OverrideTankCameraDistanceMax_newDistanceMax_354, logic_uScript_OverrideTankCameraDistanceMax_tech_354);
	}

	private void Relay_In_359()
	{
		int num = 0;
		Array terminal1SpawnData = Terminal1SpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_359.Length != num + terminal1SpawnData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_359, num + terminal1SpawnData.Length);
		}
		Array.Copy(terminal1SpawnData, 0, logic_uScript_DestroyTechsFromData_techData_359, num, terminal1SpawnData.Length);
		num += terminal1SpawnData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_359 = owner_Connection_358;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_359.In(logic_uScript_DestroyTechsFromData_techData_359, logic_uScript_DestroyTechsFromData_shouldExplode_359, logic_uScript_DestroyTechsFromData_ownerNode_359);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_359.Out)
		{
			Relay_In_360();
		}
	}

	private void Relay_In_360()
	{
		int num = 0;
		Array terminal2SpawnData = Terminal2SpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_360.Length != num + terminal2SpawnData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_360, num + terminal2SpawnData.Length);
		}
		Array.Copy(terminal2SpawnData, 0, logic_uScript_DestroyTechsFromData_techData_360, num, terminal2SpawnData.Length);
		num += terminal2SpawnData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_360 = owner_Connection_361;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_360.In(logic_uScript_DestroyTechsFromData_techData_360, logic_uScript_DestroyTechsFromData_shouldExplode_360, logic_uScript_DestroyTechsFromData_ownerNode_360);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_360.Out)
		{
			Relay_In_362();
		}
	}

	private void Relay_In_362()
	{
		int num = 0;
		Array terminal3SpawnData = Terminal3SpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_362.Length != num + terminal3SpawnData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_362, num + terminal3SpawnData.Length);
		}
		Array.Copy(terminal3SpawnData, 0, logic_uScript_DestroyTechsFromData_techData_362, num, terminal3SpawnData.Length);
		num += terminal3SpawnData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_362 = owner_Connection_363;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_362.In(logic_uScript_DestroyTechsFromData_techData_362, logic_uScript_DestroyTechsFromData_shouldExplode_362, logic_uScript_DestroyTechsFromData_ownerNode_362);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_362.Out)
		{
			Relay_In_386();
		}
	}

	private void Relay_Out_366()
	{
		Relay_In_368();
	}

	private void Relay_In_366()
	{
		int num = 0;
		Array forcefieldEntranceGroupSpawnData = ForcefieldEntranceGroupSpawnData;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_366.Length != num + forcefieldEntranceGroupSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_366, num + forcefieldEntranceGroupSpawnData.Length);
		}
		Array.Copy(forcefieldEntranceGroupSpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_366, num, forcefieldEntranceGroupSpawnData.Length);
		num += forcefieldEntranceGroupSpawnData.Length;
		int num2 = 0;
		Array array = local_EntranceForcefields_TankArray;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_366.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_366, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_366, num2, array.Length);
		num2 += array.Length;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_366.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_366, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_366);
	}

	private void Relay_Out_368()
	{
		Relay_In_13();
	}

	private void Relay_In_368()
	{
		int num = 0;
		Array forcefieldStage1GroupSpawnData = ForcefieldStage1GroupSpawnData;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_368.Length != num + forcefieldStage1GroupSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_368, num + forcefieldStage1GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage1GroupSpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_368, num, forcefieldStage1GroupSpawnData.Length);
		num += forcefieldStage1GroupSpawnData.Length;
		int num2 = 0;
		Array array = local_Stage1Forcefields_TankArray;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_368.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_368, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_368, num2, array.Length);
		num2 += array.Length;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_368.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_368, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_368);
	}

	private void Relay_Out_370()
	{
		Relay_In_50();
	}

	private void Relay_In_370()
	{
		int num = 0;
		Array forcefieldStage1GroupSpawnData = ForcefieldStage1GroupSpawnData;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_370.Length != num + forcefieldStage1GroupSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_370, num + forcefieldStage1GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage1GroupSpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_370, num, forcefieldStage1GroupSpawnData.Length);
		num += forcefieldStage1GroupSpawnData.Length;
		int num2 = 0;
		Array array = local_Stage1Forcefields_TankArray;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_370.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_370, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_370, num2, array.Length);
		num2 += array.Length;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_370.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_370, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_370);
	}

	private void Relay_Out_372()
	{
		Relay_In_370();
	}

	private void Relay_In_372()
	{
		int num = 0;
		Array forcefieldEntranceGroupSpawnData = ForcefieldEntranceGroupSpawnData;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_372.Length != num + forcefieldEntranceGroupSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_372, num + forcefieldEntranceGroupSpawnData.Length);
		}
		Array.Copy(forcefieldEntranceGroupSpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_372, num, forcefieldEntranceGroupSpawnData.Length);
		num += forcefieldEntranceGroupSpawnData.Length;
		int num2 = 0;
		Array array = local_EntranceForcefields_TankArray;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_372.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_372, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_372, num2, array.Length);
		num2 += array.Length;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_372.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_372, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_372);
	}

	private void Relay_In_377()
	{
		logic_uScript_GetTimeOfDay_Return_377 = logic_uScript_GetTimeOfDay_uScript_GetTimeOfDay_377.In();
		local_StartingHour_System_Int32 = logic_uScript_GetTimeOfDay_Return_377;
		if (logic_uScript_GetTimeOfDay_uScript_GetTimeOfDay_377.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_Save_Out_379()
	{
		Relay_Save_404();
	}

	private void Relay_Load_Out_379()
	{
		Relay_Load_404();
	}

	private void Relay_Restart_Out_379()
	{
		Relay_Restart_404();
	}

	private void Relay_Save_379()
	{
		logic_SubGraph_SaveLoadInt_restartValue_379 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_379 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_379 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.Save(logic_SubGraph_SaveLoadInt_restartValue_379, ref logic_SubGraph_SaveLoadInt_integer_379, logic_SubGraph_SaveLoadInt_intAsVariable_379, logic_SubGraph_SaveLoadInt_uniqueID_379);
	}

	private void Relay_Load_379()
	{
		logic_SubGraph_SaveLoadInt_restartValue_379 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_379 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_379 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.Load(logic_SubGraph_SaveLoadInt_restartValue_379, ref logic_SubGraph_SaveLoadInt_integer_379, logic_SubGraph_SaveLoadInt_intAsVariable_379, logic_SubGraph_SaveLoadInt_uniqueID_379);
	}

	private void Relay_Restart_379()
	{
		logic_SubGraph_SaveLoadInt_restartValue_379 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_379 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_379 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_379.Restart(logic_SubGraph_SaveLoadInt_restartValue_379, ref logic_SubGraph_SaveLoadInt_integer_379, logic_SubGraph_SaveLoadInt_intAsVariable_379, logic_SubGraph_SaveLoadInt_uniqueID_379);
	}

	private void Relay_InitialSpawn_381()
	{
		int num = 0;
		Array forcefieldStage4GroupSpawnData = ForcefieldStage4GroupSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_381.Length != num + forcefieldStage4GroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_381, num + forcefieldStage4GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage4GroupSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_381, num, forcefieldStage4GroupSpawnData.Length);
		num += forcefieldStage4GroupSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_381 = owner_Connection_380;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_381.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_381, logic_uScript_SpawnTechsFromData_ownerNode_381, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_381, logic_uScript_SpawnTechsFromData_allowResurrection_381);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_381.Out)
		{
			Relay_InitialSpawn_113();
		}
	}

	private void Relay_Out_385()
	{
		Relay_In_40();
	}

	private void Relay_In_385()
	{
		int num = 0;
		Array forcefieldStage4GroupSpawnData = ForcefieldStage4GroupSpawnData;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_385.Length != num + forcefieldStage4GroupSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_385, num + forcefieldStage4GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage4GroupSpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_385, num, forcefieldStage4GroupSpawnData.Length);
		num += forcefieldStage4GroupSpawnData.Length;
		int num2 = 0;
		Array array = local_Stage3Forcefields_TankArray;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_385.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_385, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_385, num2, array.Length);
		num2 += array.Length;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_385.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_385, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_385);
	}

	private void Relay_In_386()
	{
		int num = 0;
		Array forcefieldStage4GroupSpawnData = ForcefieldStage4GroupSpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_386.Length != num + forcefieldStage4GroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_386, num + forcefieldStage4GroupSpawnData.Length);
		}
		Array.Copy(forcefieldStage4GroupSpawnData, 0, logic_uScript_DestroyTechsFromData_techData_386, num, forcefieldStage4GroupSpawnData.Length);
		num += forcefieldStage4GroupSpawnData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_386 = owner_Connection_387;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_386.In(logic_uScript_DestroyTechsFromData_techData_386, logic_uScript_DestroyTechsFromData_shouldExplode_386, logic_uScript_DestroyTechsFromData_ownerNode_386);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_386.Out)
		{
			Relay_False_439();
		}
	}

	private void Relay_In_389()
	{
		logic_uScript_ClearEncounterTarget_owner_389 = owner_Connection_390;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_389.In(logic_uScript_ClearEncounterTarget_owner_389);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_389.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_392()
	{
		logic_uScript_Wait_uScript_Wait_392.In(logic_uScript_Wait_seconds_392, logic_uScript_Wait_repeat_392);
		if (logic_uScript_Wait_uScript_Wait_392.Waited)
		{
			Relay_In_393();
		}
	}

	private void Relay_In_393()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_393 = Stage2GoalTriggerVolume;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_393.In(logic_uScript_SetEncounterTargetPosition_positionName_393);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_393.Out)
		{
			Relay_In_639();
		}
	}

	private void Relay_In_395()
	{
		logic_uScript_Wait_uScript_Wait_395.In(logic_uScript_Wait_seconds_395, logic_uScript_Wait_repeat_395);
		if (logic_uScript_Wait_uScript_Wait_395.Waited)
		{
			Relay_In_396();
		}
	}

	private void Relay_In_396()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_396 = Stage3GoalTriggerVolume;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_396.In(logic_uScript_SetEncounterTargetPosition_positionName_396);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_396.Out)
		{
			Relay_In_638();
		}
	}

	private void Relay_In_397()
	{
		logic_uScript_ClearEncounterTarget_owner_397 = owner_Connection_398;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_397.In(logic_uScript_ClearEncounterTarget_owner_397);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_397.Out)
		{
			Relay_False_423();
		}
	}

	private void Relay_In_399()
	{
		logic_uScript_ClearEncounterTarget_owner_399 = owner_Connection_400;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_399.In(logic_uScript_ClearEncounterTarget_owner_399);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_399.Out)
		{
			Relay_In_452();
		}
	}

	private void Relay_Save_Out_403()
	{
		Relay_Save_22();
	}

	private void Relay_Load_Out_403()
	{
		Relay_Load_22();
	}

	private void Relay_Restart_Out_403()
	{
		Relay_Restart_22();
	}

	private void Relay_Save_403()
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = local_DialogueComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_403 = local_DialogueComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Save(ref logic_SubGraph_SaveLoadBool_boolean_403, logic_SubGraph_SaveLoadBool_boolAsVariable_403, logic_SubGraph_SaveLoadBool_uniqueID_403);
	}

	private void Relay_Load_403()
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = local_DialogueComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_403 = local_DialogueComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Load(ref logic_SubGraph_SaveLoadBool_boolean_403, logic_SubGraph_SaveLoadBool_boolAsVariable_403, logic_SubGraph_SaveLoadBool_uniqueID_403);
	}

	private void Relay_Set_True_403()
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = local_DialogueComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_403 = local_DialogueComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_403, logic_SubGraph_SaveLoadBool_boolAsVariable_403, logic_SubGraph_SaveLoadBool_uniqueID_403);
	}

	private void Relay_Set_False_403()
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = local_DialogueComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_403 = local_DialogueComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_403, logic_SubGraph_SaveLoadBool_boolAsVariable_403, logic_SubGraph_SaveLoadBool_uniqueID_403);
	}

	private void Relay_Save_Out_404()
	{
		Relay_Save_496();
	}

	private void Relay_Load_Out_404()
	{
		Relay_Load_496();
	}

	private void Relay_Restart_Out_404()
	{
		Relay_Restart_496();
	}

	private void Relay_Save_404()
	{
		logic_SubGraph_SaveLoadInt_integer_404 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_404 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.Save(logic_SubGraph_SaveLoadInt_restartValue_404, ref logic_SubGraph_SaveLoadInt_integer_404, logic_SubGraph_SaveLoadInt_intAsVariable_404, logic_SubGraph_SaveLoadInt_uniqueID_404);
	}

	private void Relay_Load_404()
	{
		logic_SubGraph_SaveLoadInt_integer_404 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_404 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.Load(logic_SubGraph_SaveLoadInt_restartValue_404, ref logic_SubGraph_SaveLoadInt_integer_404, logic_SubGraph_SaveLoadInt_intAsVariable_404, logic_SubGraph_SaveLoadInt_uniqueID_404);
	}

	private void Relay_Restart_404()
	{
		logic_SubGraph_SaveLoadInt_integer_404 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_404 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_404.Restart(logic_SubGraph_SaveLoadInt_restartValue_404, ref logic_SubGraph_SaveLoadInt_integer_404, logic_SubGraph_SaveLoadInt_intAsVariable_404, logic_SubGraph_SaveLoadInt_uniqueID_404);
	}

	private void Relay_True_407()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_407.True(out logic_uScriptAct_SetBool_Target_407);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_407;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_407.Out)
		{
			Relay_In_410();
		}
	}

	private void Relay_False_407()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_407.False(out logic_uScriptAct_SetBool_Target_407);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_407;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_407.Out)
		{
			Relay_In_410();
		}
	}

	private void Relay_In_409()
	{
		logic_uScript_PlayDialogue_dialogue_409 = msgStage1Complete;
		logic_uScript_PlayDialogue_progress_409 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_409.In(logic_uScript_PlayDialogue_dialogue_409, ref logic_uScript_PlayDialogue_progress_409);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_409;
		bool num = logic_uScript_PlayDialogue_uScript_PlayDialogue_409.Out;
		bool shown = logic_uScript_PlayDialogue_uScript_PlayDialogue_409.Shown;
		if (num)
		{
			Relay_In_410();
		}
		if (shown)
		{
			Relay_True_407();
		}
	}

	private void Relay_In_410()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_410.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_410.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_In_411()
	{
		logic_uScriptCon_CompareBool_Bool_411 = local_DialogueComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_411.In(logic_uScriptCon_CompareBool_Bool_411);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_411.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_411.False;
		if (num)
		{
			Relay_In_410();
		}
		if (flag)
		{
			Relay_In_409();
		}
	}

	private void Relay_True_413()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_413.True(out logic_uScriptAct_SetBool_Target_413);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_413;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_413.Out)
		{
			Relay_In_416();
		}
	}

	private void Relay_False_413()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_413.False(out logic_uScriptAct_SetBool_Target_413);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_413;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_413.Out)
		{
			Relay_In_416();
		}
	}

	private void Relay_In_415()
	{
		logic_uScriptCon_CompareBool_Bool_415 = local_DialogueComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_415.In(logic_uScriptCon_CompareBool_Bool_415);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_415.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_415.False;
		if (num)
		{
			Relay_In_416();
		}
		if (flag)
		{
			Relay_In_419();
		}
	}

	private void Relay_In_416()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_416.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_416.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_419()
	{
		logic_uScript_PlayDialogue_dialogue_419 = msgStage2Complete;
		logic_uScript_PlayDialogue_progress_419 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_419.In(logic_uScript_PlayDialogue_dialogue_419, ref logic_uScript_PlayDialogue_progress_419);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_419;
		bool num = logic_uScript_PlayDialogue_uScript_PlayDialogue_419.Out;
		bool shown = logic_uScript_PlayDialogue_uScript_PlayDialogue_419.Shown;
		if (num)
		{
			Relay_In_416();
		}
		if (shown)
		{
			Relay_True_413();
		}
	}

	private void Relay_In_421()
	{
		logic_uScriptAct_AddInt_v2_A_421 = local_Objective04SubStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_421.In(logic_uScriptAct_AddInt_v2_A_421, logic_uScriptAct_AddInt_v2_B_421, out logic_uScriptAct_AddInt_v2_IntResult_421, out logic_uScriptAct_AddInt_v2_FloatResult_421);
		local_Objective04SubStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_421;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_421.Out)
		{
			Relay_In_641();
		}
	}

	private void Relay_True_423()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_423.True(out logic_uScriptAct_SetBool_Target_423);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_423;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_423.Out)
		{
			Relay_In_450();
		}
	}

	private void Relay_False_423()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_423.False(out logic_uScriptAct_SetBool_Target_423);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_423;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_423.Out)
		{
			Relay_In_450();
		}
	}

	private void Relay_True_425()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_425.True(out logic_uScriptAct_SetBool_Target_425);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_425;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_425.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_False_425()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_425.False(out logic_uScriptAct_SetBool_Target_425);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_425;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_425.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_True_427()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_427.True(out logic_uScriptAct_SetBool_Target_427);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_427;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_427.Out)
		{
			Relay_In_454();
		}
	}

	private void Relay_False_427()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_427.False(out logic_uScriptAct_SetBool_Target_427);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_427;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_427.Out)
		{
			Relay_In_454();
		}
	}

	private void Relay_In_429()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_429.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_429.Out)
		{
			Relay_In_512();
		}
	}

	private void Relay_In_430()
	{
		logic_uScript_PlayDialogue_dialogue_430 = msgEpilogue;
		logic_uScript_PlayDialogue_progress_430 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_430.In(logic_uScript_PlayDialogue_dialogue_430, ref logic_uScript_PlayDialogue_progress_430);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_430;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_430.Shown)
		{
			Relay_True_434();
		}
	}

	private void Relay_True_434()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_434.True(out logic_uScriptAct_SetBool_Target_434);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_434;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_434.Out)
		{
			Relay_In_429();
		}
	}

	private void Relay_False_434()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_434.False(out logic_uScriptAct_SetBool_Target_434);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_434;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_434.Out)
		{
			Relay_In_429();
		}
	}

	private void Relay_In_436()
	{
		logic_uScriptCon_CompareBool_Bool_436 = local_DialogueComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_436.In(logic_uScriptCon_CompareBool_Bool_436);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_436.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_436.False;
		if (num)
		{
			Relay_In_429();
		}
		if (flag)
		{
			Relay_In_430();
		}
	}

	private void Relay_In_437()
	{
		logic_uScript_SetTankTeam_tank_437 = local_NPCTech02_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_437.In(logic_uScript_SetTankTeam_tank_437, logic_uScript_SetTankTeam_team_437);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_437.Out)
		{
			Relay_In_511();
		}
	}

	private void Relay_True_439()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_439.True(out logic_uScriptAct_SetBool_Target_439);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_439;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_439.Out)
		{
			Relay_In_457();
		}
	}

	private void Relay_False_439()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_439.False(out logic_uScriptAct_SetBool_Target_439);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_439;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_439.Out)
		{
			Relay_In_457();
		}
	}

	private void Relay_In_441()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_441.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_441.Out)
		{
			Relay_In_352();
		}
	}

	private void Relay_In_442()
	{
		logic_uScriptCon_CompareBool_Bool_442 = local_DialogueComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442.In(logic_uScriptCon_CompareBool_Bool_442);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442.False;
		if (num)
		{
			Relay_In_449();
		}
		if (flag)
		{
			Relay_In_448();
		}
	}

	private void Relay_True_444()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_444.True(out logic_uScriptAct_SetBool_Target_444);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_444;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_444.Out)
		{
			Relay_In_449();
		}
	}

	private void Relay_False_444()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_444.False(out logic_uScriptAct_SetBool_Target_444);
		local_DialogueComplete_System_Boolean = logic_uScriptAct_SetBool_Target_444;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_444.Out)
		{
			Relay_In_449();
		}
	}

	private void Relay_In_448()
	{
		logic_uScript_PlayDialogue_dialogue_448 = msgStage3Complete;
		logic_uScript_PlayDialogue_progress_448 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_448.In(logic_uScript_PlayDialogue_dialogue_448, ref logic_uScript_PlayDialogue_progress_448);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_448;
		bool num = logic_uScript_PlayDialogue_uScript_PlayDialogue_448.Out;
		bool shown = logic_uScript_PlayDialogue_uScript_PlayDialogue_448.Shown;
		if (num)
		{
			Relay_In_449();
		}
		if (shown)
		{
			Relay_True_444();
		}
	}

	private void Relay_In_449()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_449.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_449.Out)
		{
			Relay_In_461();
		}
	}

	private void Relay_In_450()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_450.In(logic_uScriptAct_SetInt_Value_450, out logic_uScriptAct_SetInt_Target_450);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_450;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_450.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_452()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_452.In(logic_uScriptAct_SetInt_Value_452, out logic_uScriptAct_SetInt_Target_452);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_452;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_452.Out)
		{
			Relay_False_425();
		}
	}

	private void Relay_In_454()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_454.In(logic_uScriptAct_SetInt_Value_454, out logic_uScriptAct_SetInt_Target_454);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_454;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_454.Out)
		{
			Relay_In_56();
		}
	}

	private void Relay_In_457()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_457.In(logic_uScriptAct_SetInt_Value_457, out logic_uScriptAct_SetInt_Target_457);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_457;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_457.Out)
		{
			Relay_In_642();
		}
	}

	private void Relay_In_459()
	{
		logic_uScript_SetEncounterTarget_owner_459 = owner_Connection_458;
		logic_uScript_SetEncounterTarget_visibleObject_459 = local_NPCTech02_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_459.In(logic_uScript_SetEncounterTarget_owner_459, logic_uScript_SetEncounterTarget_visibleObject_459);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_459.Out)
		{
			Relay_In_441();
		}
	}

	private void Relay_In_461()
	{
		logic_uScript_Wait_uScript_Wait_461.In(logic_uScript_Wait_seconds_461, logic_uScript_Wait_repeat_461);
		if (logic_uScript_Wait_uScript_Wait_461.Waited)
		{
			Relay_In_459();
		}
	}

	private void Relay_In_465()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_465.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_465.Out)
		{
			Relay_In_480();
		}
	}

	private void Relay_In_466()
	{
		logic_uScript_IsTechWheelGrounded_tech_466 = local_FlyingTech_Tank;
		logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_466.In(logic_uScript_IsTechWheelGrounded_tech_466);
		bool num = logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_466.True;
		bool flag = logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_466.False;
		if (num)
		{
			Relay_In_472();
		}
		if (flag)
		{
			Relay_In_467();
		}
	}

	private void Relay_In_467()
	{
		logic_uScript_IsTechTouchingTerrain_tech_467 = local_FlyingTech_Tank;
		logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_467.In(logic_uScript_IsTechTouchingTerrain_tech_467);
		bool num = logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_467.True;
		bool flag = logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_467.False;
		if (num)
		{
			Relay_In_465();
		}
		if (flag)
		{
			Relay_In_471();
		}
	}

	private void Relay_AtIndex_468()
	{
		int num = 0;
		Array array = local_FlyingTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_468.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_468, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_468, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_468.AtIndex(ref logic_uScript_AccessListTech_techList_468, logic_uScript_AccessListTech_index_468, out logic_uScript_AccessListTech_value_468);
		local_FlyingTechs_TankArray = logic_uScript_AccessListTech_techList_468;
		local_FlyingTech_Tank = logic_uScript_AccessListTech_value_468;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_468.Out)
		{
			Relay_In_466();
		}
	}

	private void Relay_In_469()
	{
		logic_uScript_IsTechInTrigger_triggerAreaName_469 = FlightHeightTrig;
		int num = 0;
		Array array = local_FlyingTechs_TankArray;
		if (logic_uScript_IsTechInTrigger_techs_469.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_IsTechInTrigger_techs_469, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_IsTechInTrigger_techs_469, num, array.Length);
		num += array.Length;
		logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_469.In(logic_uScript_IsTechInTrigger_triggerAreaName_469, ref logic_uScript_IsTechInTrigger_techs_469);
		local_FlyingTechs_TankArray = logic_uScript_IsTechInTrigger_techs_469;
		bool inRange = logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_469.InRange;
		bool outOfRange = logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_469.OutOfRange;
		if (inRange)
		{
			Relay_AtIndex_468();
		}
		if (outOfRange)
		{
			Relay_In_470();
		}
	}

	private void Relay_In_470()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_470.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_470.Out)
		{
			Relay_In_472();
		}
	}

	private void Relay_In_471()
	{
		logic_uScript_IsTechAnchored_tech_471 = local_FlyingTech_Tank;
		logic_uScript_IsTechAnchored_uScript_IsTechAnchored_471.In(logic_uScript_IsTechAnchored_tech_471);
		bool num = logic_uScript_IsTechAnchored_uScript_IsTechAnchored_471.True;
		bool flag = logic_uScript_IsTechAnchored_uScript_IsTechAnchored_471.False;
		if (num)
		{
			Relay_In_480();
		}
		if (flag)
		{
			Relay_In_475();
		}
	}

	private void Relay_In_472()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_472.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_472.Out)
		{
			Relay_In_465();
		}
	}

	private void Relay_In_475()
	{
		logic_uScriptCon_CompareBool_Bool_475 = local_MsgFlyingWarningShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_475.In(logic_uScriptCon_CompareBool_Bool_475);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_475.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_475.False;
		if (num)
		{
			Relay_In_481();
		}
		if (flag)
		{
			Relay_In_476();
		}
	}

	private void Relay_In_476()
	{
		logic_uScript_AddMessage_messageData_476 = msgFlyingWarning;
		logic_uScript_AddMessage_speaker_476 = SpeakerHubl;
		logic_uScript_AddMessage_Return_476 = logic_uScript_AddMessage_uScript_AddMessage_476.In(logic_uScript_AddMessage_messageData_476, logic_uScript_AddMessage_speaker_476);
		bool num = logic_uScript_AddMessage_uScript_AddMessage_476.Out;
		bool shown = logic_uScript_AddMessage_uScript_AddMessage_476.Shown;
		if (num)
		{
			Relay_In_482();
		}
		if (shown)
		{
			Relay_True_478();
		}
	}

	private void Relay_True_478()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_478.True(out logic_uScriptAct_SetBool_Target_478);
		local_MsgFlyingWarningShown_System_Boolean = logic_uScriptAct_SetBool_Target_478;
	}

	private void Relay_False_478()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_478.False(out logic_uScriptAct_SetBool_Target_478);
		local_MsgFlyingWarningShown_System_Boolean = logic_uScriptAct_SetBool_Target_478;
	}

	private void Relay_In_480()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_480.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_480.Out)
		{
			Relay_In_481();
		}
	}

	private void Relay_In_481()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_481.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_481.Out)
		{
			Relay_In_482();
		}
	}

	private void Relay_In_482()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_482.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_482.Out)
		{
			Relay_In_483();
		}
	}

	private void Relay_In_483()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_483.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_483.Out)
		{
			Relay_In_507();
		}
	}

	private void Relay_Save_Out_485()
	{
		Relay_Save_7();
	}

	private void Relay_Load_Out_485()
	{
		Relay_Load_7();
	}

	private void Relay_Restart_Out_485()
	{
		Relay_Set_False_7();
	}

	private void Relay_Save_485()
	{
		logic_SubGraph_SaveLoadBool_boolean_485 = local_MsgFlyingWarningShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_485 = local_MsgFlyingWarningShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Save(ref logic_SubGraph_SaveLoadBool_boolean_485, logic_SubGraph_SaveLoadBool_boolAsVariable_485, logic_SubGraph_SaveLoadBool_uniqueID_485);
	}

	private void Relay_Load_485()
	{
		logic_SubGraph_SaveLoadBool_boolean_485 = local_MsgFlyingWarningShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_485 = local_MsgFlyingWarningShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Load(ref logic_SubGraph_SaveLoadBool_boolean_485, logic_SubGraph_SaveLoadBool_boolAsVariable_485, logic_SubGraph_SaveLoadBool_uniqueID_485);
	}

	private void Relay_Set_True_485()
	{
		logic_SubGraph_SaveLoadBool_boolean_485 = local_MsgFlyingWarningShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_485 = local_MsgFlyingWarningShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_485, logic_SubGraph_SaveLoadBool_boolAsVariable_485, logic_SubGraph_SaveLoadBool_uniqueID_485);
	}

	private void Relay_Set_False_485()
	{
		logic_SubGraph_SaveLoadBool_boolean_485 = local_MsgFlyingWarningShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_485 = local_MsgFlyingWarningShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_485.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_485, logic_SubGraph_SaveLoadBool_boolAsVariable_485, logic_SubGraph_SaveLoadBool_uniqueID_485);
	}

	private void Relay_In_486()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_486.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_486.Out)
		{
			Relay_In_490();
		}
	}

	private void Relay_True_487()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_487.True(out logic_uScriptAct_SetBool_Target_487);
		local_BaseOnline_System_Boolean = logic_uScriptAct_SetBool_Target_487;
	}

	private void Relay_False_487()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_487.False(out logic_uScriptAct_SetBool_Target_487);
		local_BaseOnline_System_Boolean = logic_uScriptAct_SetBool_Target_487;
	}

	private void Relay_In_490()
	{
		logic_uScriptCon_CompareBool_Bool_490 = local_BaseOnline_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_490.In(logic_uScriptCon_CompareBool_Bool_490);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_490.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_490.False;
		if (num)
		{
			Relay_In_492();
		}
		if (flag)
		{
			Relay_In_521();
		}
	}

	private void Relay_In_491()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491.Out)
		{
			Relay_In_133();
		}
	}

	private void Relay_In_492()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_492.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_492.Out)
		{
			Relay_In_491();
		}
	}

	private void Relay_Save_Out_494()
	{
		Relay_Save_485();
	}

	private void Relay_Load_Out_494()
	{
		Relay_Load_485();
	}

	private void Relay_Restart_Out_494()
	{
		Relay_Set_False_485();
	}

	private void Relay_Save_494()
	{
		logic_SubGraph_SaveLoadBool_boolean_494 = local_BaseOnline_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_494 = local_BaseOnline_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Save(ref logic_SubGraph_SaveLoadBool_boolean_494, logic_SubGraph_SaveLoadBool_boolAsVariable_494, logic_SubGraph_SaveLoadBool_uniqueID_494);
	}

	private void Relay_Load_494()
	{
		logic_SubGraph_SaveLoadBool_boolean_494 = local_BaseOnline_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_494 = local_BaseOnline_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Load(ref logic_SubGraph_SaveLoadBool_boolean_494, logic_SubGraph_SaveLoadBool_boolAsVariable_494, logic_SubGraph_SaveLoadBool_uniqueID_494);
	}

	private void Relay_Set_True_494()
	{
		logic_SubGraph_SaveLoadBool_boolean_494 = local_BaseOnline_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_494 = local_BaseOnline_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_494, logic_SubGraph_SaveLoadBool_boolAsVariable_494, logic_SubGraph_SaveLoadBool_uniqueID_494);
	}

	private void Relay_Set_False_494()
	{
		logic_SubGraph_SaveLoadBool_boolean_494 = local_BaseOnline_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_494 = local_BaseOnline_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_494.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_494, logic_SubGraph_SaveLoadBool_boolAsVariable_494, logic_SubGraph_SaveLoadBool_uniqueID_494);
	}

	private void Relay_Save_Out_496()
	{
	}

	private void Relay_Load_Out_496()
	{
		Relay_In_24();
	}

	private void Relay_Restart_Out_496()
	{
	}

	private void Relay_Save_496()
	{
		logic_SubGraph_SaveLoadInt_integer_496 = local_Objective04SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_496 = local_Objective04SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.Save(logic_SubGraph_SaveLoadInt_restartValue_496, ref logic_SubGraph_SaveLoadInt_integer_496, logic_SubGraph_SaveLoadInt_intAsVariable_496, logic_SubGraph_SaveLoadInt_uniqueID_496);
	}

	private void Relay_Load_496()
	{
		logic_SubGraph_SaveLoadInt_integer_496 = local_Objective04SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_496 = local_Objective04SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.Load(logic_SubGraph_SaveLoadInt_restartValue_496, ref logic_SubGraph_SaveLoadInt_integer_496, logic_SubGraph_SaveLoadInt_intAsVariable_496, logic_SubGraph_SaveLoadInt_uniqueID_496);
	}

	private void Relay_Restart_496()
	{
		logic_SubGraph_SaveLoadInt_integer_496 = local_Objective04SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_496 = local_Objective04SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_496.Restart(logic_SubGraph_SaveLoadInt_restartValue_496, ref logic_SubGraph_SaveLoadInt_integer_496, logic_SubGraph_SaveLoadInt_intAsVariable_496, logic_SubGraph_SaveLoadInt_uniqueID_496);
	}

	private void Relay_In_498()
	{
		logic_uScript_AddMessage_messageData_498 = msgOverheadKillHitBubl;
		logic_uScript_AddMessage_speaker_498 = SpeakerBubl;
		logic_uScript_AddMessage_Return_498 = logic_uScript_AddMessage_uScript_AddMessage_498.In(logic_uScript_AddMessage_messageData_498, logic_uScript_AddMessage_speaker_498);
		if (logic_uScript_AddMessage_uScript_AddMessage_498.Out)
		{
			Relay_In_283();
		}
	}

	private void Relay_In_500()
	{
		logic_uScriptCon_CompareInt_A_500 = local_CurrentObjective_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_500.In(logic_uScriptCon_CompareInt_A_500, logic_uScriptCon_CompareInt_B_500);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_500.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_500.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_498();
		}
		if (lessThan)
		{
			Relay_In_504();
		}
	}

	private void Relay_In_504()
	{
		logic_uScript_AddMessage_messageData_504 = msgOverheadKillHitHubl;
		logic_uScript_AddMessage_speaker_504 = SpeakerHubl;
		logic_uScript_AddMessage_Return_504 = logic_uScript_AddMessage_uScript_AddMessage_504.In(logic_uScript_AddMessage_messageData_504, logic_uScript_AddMessage_speaker_504);
		if (logic_uScript_AddMessage_uScript_AddMessage_504.Out)
		{
			Relay_In_283();
		}
	}

	private void Relay_In_506()
	{
		logic_uScript_GetTechsInTrigger_triggerAreaName_506 = OverheadKillVolume1;
		logic_uScript_GetTechsInTrigger_uScript_GetTechsInTrigger_506.In(logic_uScript_GetTechsInTrigger_triggerAreaName_506, out logic_uScript_GetTechsInTrigger_Entered_506, out logic_uScript_GetTechsInTrigger_Inside_506, out logic_uScript_GetTechsInTrigger_Exited_506);
		local_FlyingTechs_TankArray = logic_uScript_GetTechsInTrigger_Inside_506;
		if (logic_uScript_GetTechsInTrigger_uScript_GetTechsInTrigger_506.Out)
		{
			Relay_In_282();
		}
	}

	private void Relay_In_507()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_507 = OverheadKillVolume1;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_507.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_507);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_507.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_507.InRange;
		if (num)
		{
			Relay_In_9();
		}
		if (inRange)
		{
			Relay_In_506();
		}
	}

	private void Relay_In_508()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_508 = owner_Connection_509;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_508.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_508);
	}

	private void Relay_In_510()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_510.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_510.SinglePlayer)
		{
			Relay_In_508();
		}
	}

	private void Relay_In_511()
	{
		logic_uScript_SetTankInvulnerable_tank_511 = local_NPCTech02_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_511.In(logic_uScript_SetTankInvulnerable_invulnerable_511, logic_uScript_SetTankInvulnerable_tank_511);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_511.Out)
		{
			Relay_In_421();
		}
	}

	private void Relay_In_512()
	{
		logic_uScript_SetTankInvulnerable_tank_512 = local_NPCTech02_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_512.In(logic_uScript_SetTankInvulnerable_invulnerable_512, logic_uScript_SetTankInvulnerable_tank_512);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_512.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_514()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_514 = MissionRangeTrig;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_514 = MissionRangeTrig;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_514.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_514, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_514, ref logic_uScript_IsPlayerInTriggerSmart_inside_514);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_514.Out;
		bool lastExited = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_514.LastExited;
		if (num)
		{
			Relay_In_286();
		}
		if (lastExited)
		{
			Relay_In_288();
		}
	}

	private void Relay_In_516()
	{
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_516.In(logic_uScript_SetTimeOfDay_hour_516, logic_uScript_SetTimeOfDay_tech_516);
		if (logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_516.Out)
		{
			Relay_In_469();
		}
	}

	private void Relay_In_518()
	{
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_518.In(logic_uScript_SetTimeOfDay_hour_518, logic_uScript_SetTimeOfDay_tech_518);
	}

	private void Relay_In_519()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_519 = MissionRangeTrig;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_519 = MissionRangeTrig;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_519.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_519, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_519, ref logic_uScript_IsPlayerInTriggerSmart_inside_519);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_519.Out;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_519.AllInside;
		bool someInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_519.SomeInside;
		if (num)
		{
			Relay_In_640();
		}
		if (allInside)
		{
			Relay_In_518();
		}
		if (someInside)
		{
			Relay_In_518();
		}
	}

	private void Relay_In_521()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_521 = MissionRangeTrig;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_521 = MissionRangeTrig;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_521.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_521, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_521, ref logic_uScript_IsPlayerInTriggerSmart_inside_521);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_521.Out;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_521.AllInside;
		if (num)
		{
			Relay_In_526();
		}
		if (allInside)
		{
			Relay_In_529();
		}
	}

	private void Relay_In_525()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_525 = NotMissionRangeTrig2;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_525.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_525);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_525.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_525.OutOfRange;
		if (inRange)
		{
			Relay_In_527();
		}
		if (outOfRange)
		{
			Relay_In_528();
		}
	}

	private void Relay_In_526()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_526.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_526.Out)
		{
			Relay_In_527();
		}
	}

	private void Relay_In_527()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_527.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_527.Out)
		{
			Relay_In_530();
		}
	}

	private void Relay_In_528()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_528 = NotMissionRangeTrig3;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_528.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_528);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_528.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_528.OutOfRange;
		if (inRange)
		{
			Relay_In_530();
		}
		if (outOfRange)
		{
			Relay_In_516();
		}
	}

	private void Relay_In_529()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_529 = NotMissionRangeTrig1;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_529.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_529);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_529.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_529.OutOfRange;
		if (inRange)
		{
			Relay_In_526();
		}
		if (outOfRange)
		{
			Relay_In_525();
		}
	}

	private void Relay_In_530()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_530.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_530.Out)
		{
			Relay_In_469();
		}
	}

	private void Relay_In_531()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_531.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_531.Out)
		{
			Relay_In_537();
		}
	}

	private void Relay_In_532()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_532.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_532.Out)
		{
			Relay_In_533();
		}
	}

	private void Relay_In_533()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_533.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_533.Out)
		{
			Relay_In_531();
		}
	}

	private void Relay_In_534()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_534 = NotMissionRangeTrig3;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_534.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_534);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_534.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_534.OutOfRange;
		if (inRange)
		{
			Relay_In_533();
		}
		if (outOfRange)
		{
			Relay_In_295();
		}
	}

	private void Relay_In_535()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_535 = NotMissionRangeTrig1;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_535.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_535);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_535.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_535.OutOfRange;
		if (inRange)
		{
			Relay_In_541();
		}
		if (outOfRange)
		{
			Relay_In_536();
		}
	}

	private void Relay_In_536()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_536 = NotMissionRangeTrig2;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_536.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_536);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_536.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_536.OutOfRange;
		if (inRange)
		{
			Relay_In_532();
		}
		if (outOfRange)
		{
			Relay_In_534();
		}
	}

	private void Relay_In_537()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_537.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_537.Out)
		{
			Relay_In_298();
		}
	}

	private void Relay_In_541()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_541.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_541.Out)
		{
			Relay_In_532();
		}
	}

	private void Relay_Out_545()
	{
	}

	private void Relay_In_545()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_545 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_545.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_545, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_545);
	}

	private void Relay_In_546()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_546 = NPCTriggerVolume01;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_546.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_546);
		bool allInRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_546.AllInRange;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_546.InRange;
		if (allInRange)
		{
			Relay_In_567();
		}
		if (inRange)
		{
			Relay_In_592();
		}
	}

	private void Relay_In_549()
	{
		logic_uScript_AddMessage_messageData_549 = MsgIntroWaitingForPlayers;
		logic_uScript_AddMessage_speaker_549 = SpeakerHubl;
		logic_uScript_AddMessage_Return_549 = logic_uScript_AddMessage_uScript_AddMessage_549.In(logic_uScript_AddMessage_messageData_549, logic_uScript_AddMessage_speaker_549);
		local_MsgIntroWaiting_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_549;
		if (logic_uScript_AddMessage_uScript_AddMessage_549.Shown)
		{
			Relay_True_561();
		}
	}

	private void Relay_In_550()
	{
		logic_uScriptCon_CompareBool_Bool_550 = local_MsgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_550.In(logic_uScriptCon_CompareBool_Bool_550);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_550.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_550.False;
		if (num)
		{
			Relay_In_553();
		}
		if (flag)
		{
			Relay_True_603();
		}
	}

	private void Relay_In_551()
	{
		logic_uScriptCon_CompareBool_Bool_551 = local_MsgIntroWaitingShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_551.In(logic_uScriptCon_CompareBool_Bool_551);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_551.False)
		{
			Relay_In_549();
		}
	}

	private void Relay_In_552()
	{
		logic_uScript_AddMessage_messageData_552 = msgIntro;
		logic_uScript_AddMessage_speaker_552 = SpeakerHubl;
		logic_uScript_AddMessage_Return_552 = logic_uScript_AddMessage_uScript_AddMessage_552.In(logic_uScript_AddMessage_messageData_552, logic_uScript_AddMessage_speaker_552);
		local_MsgIntro_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_552;
		if (logic_uScript_AddMessage_uScript_AddMessage_552.Shown)
		{
			Relay_True_560();
		}
	}

	private void Relay_In_553()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_553.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_553.Out)
		{
			Relay_In_565();
		}
	}

	private void Relay_In_556()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_556.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_556.Multiplayer)
		{
			Relay_In_593();
		}
	}

	private void Relay_In_559()
	{
		logic_uScript_FlyTechUpAndAway_tech_559 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_removalParticles_559 = NPCDespawnEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_559.In(logic_uScript_FlyTechUpAndAway_tech_559, logic_uScript_FlyTechUpAndAway_maxLifetime_559, logic_uScript_FlyTechUpAndAway_targetHeight_559, logic_uScript_FlyTechUpAndAway_aiTree_559, logic_uScript_FlyTechUpAndAway_removalParticles_559);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_559.Out)
		{
			Relay_In_570();
		}
	}

	private void Relay_True_560()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_560.True(out logic_uScriptAct_SetBool_Target_560);
		local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_560;
	}

	private void Relay_False_560()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_560.False(out logic_uScriptAct_SetBool_Target_560);
		local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_560;
	}

	private void Relay_True_561()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_561.True(out logic_uScriptAct_SetBool_Target_561);
		local_MsgIntroWaitingShown_System_Boolean = logic_uScriptAct_SetBool_Target_561;
	}

	private void Relay_False_561()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_561.False(out logic_uScriptAct_SetBool_Target_561);
		local_MsgIntroWaitingShown_System_Boolean = logic_uScriptAct_SetBool_Target_561;
	}

	private void Relay_In_565()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_565.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_565.Out)
		{
			Relay_In_559();
		}
	}

	private void Relay_In_567()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_567 = local_MsgIntroWaiting_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_567.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_567, logic_uScript_RemoveOnScreenMessage_instant_567);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_567.Out)
		{
			Relay_In_550();
		}
	}

	private void Relay_In_570()
	{
		int num = 0;
		Array forcefieldEntranceGroupSpawnData = ForcefieldEntranceGroupSpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_570.Length != num + forcefieldEntranceGroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_570, num + forcefieldEntranceGroupSpawnData.Length);
		}
		Array.Copy(forcefieldEntranceGroupSpawnData, 0, logic_uScript_DestroyTechsFromData_techData_570, num, forcefieldEntranceGroupSpawnData.Length);
		num += forcefieldEntranceGroupSpawnData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_570 = owner_Connection_563;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_570.In(logic_uScript_DestroyTechsFromData_techData_570, logic_uScript_DestroyTechsFromData_shouldExplode_570, logic_uScript_DestroyTechsFromData_ownerNode_570);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_570.Out)
		{
			Relay_In_574();
		}
	}

	private void Relay_In_574()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_574 = Stage1GoalTriggerVolume;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_574.In(logic_uScript_SetEncounterTargetPosition_positionName_574);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_574.Out)
		{
			Relay_In_545();
		}
	}

	private void Relay_TechDestroyedEvent_576()
	{
		local_578_Tank = event_UnityEngine_GameObject_Tech_576;
		Relay_In_580();
	}

	private void Relay_In_579()
	{
		logic_uScript_OverrideTankCameraDistanceMax_tech_579 = local_578_Tank;
		logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_579.In(logic_uScript_OverrideTankCameraDistanceMax_enable_579, logic_uScript_OverrideTankCameraDistanceMax_newDistanceMax_579, logic_uScript_OverrideTankCameraDistanceMax_tech_579);
	}

	private void Relay_In_580()
	{
		logic_uScript_SetDangerMusicMisc_tech_580 = local_578_Tank;
		logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_580.In(logic_uScript_SetDangerMusicMisc_miscDangerMusicType_580, logic_uScript_SetDangerMusicMisc_tech_580);
		if (logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_580.Out)
		{
			Relay_In_579();
		}
	}

	private void Relay_In_581()
	{
		logic_uScriptCon_CompareInt_A_581 = local_CurrentObjective_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_581.In(logic_uScriptCon_CompareInt_A_581, logic_uScriptCon_CompareInt_B_581);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_581.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_581.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_586();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_584();
		}
	}

	private void Relay_In_584()
	{
		logic_uScript_SetTimeOfDay_hour_584 = local_StartingHour_System_Int32;
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_584.In(logic_uScript_SetTimeOfDay_hour_584, logic_uScript_SetTimeOfDay_tech_584);
	}

	private void Relay_In_586()
	{
		logic_uScriptCon_CompareInt_A_586 = local_CurrentObjective_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_586.In(logic_uScriptCon_CompareInt_A_586, logic_uScriptCon_CompareInt_B_586);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_586.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_586.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_588();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_587();
		}
	}

	private void Relay_In_587()
	{
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_587.In(logic_uScript_SetTimeOfDay_hour_587, logic_uScript_SetTimeOfDay_tech_587);
	}

	private void Relay_In_588()
	{
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_588.In(logic_uScript_SetTimeOfDay_hour_588, logic_uScript_SetTimeOfDay_tech_588);
	}

	private void Relay_In_590()
	{
		logic_uScriptCon_CompareBool_Bool_590 = local_TechsInit_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_590.In(logic_uScriptCon_CompareBool_Bool_590);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_590.True)
		{
			Relay_In_581();
		}
	}

	private void Relay_In_592()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_592 = NPCTriggerVolume01;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_592.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_592);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_592.SomeOutOfRange)
		{
			Relay_In_556();
		}
	}

	private void Relay_In_593()
	{
		logic_uScriptCon_CompareBool_Bool_593 = local_MsgIntroStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_593.In(logic_uScriptCon_CompareBool_Bool_593);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_593.False)
		{
			Relay_In_551();
		}
	}

	private void Relay_True_595()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_595.True(out logic_uScriptAct_SetBool_Target_595);
		local_MsgIntroWaitingShown_System_Boolean = logic_uScriptAct_SetBool_Target_595;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_595.Out)
		{
			Relay_False_606();
		}
	}

	private void Relay_False_595()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_595.False(out logic_uScriptAct_SetBool_Target_595);
		local_MsgIntroWaitingShown_System_Boolean = logic_uScriptAct_SetBool_Target_595;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_595.Out)
		{
			Relay_False_606();
		}
	}

	private void Relay_True_597()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_597.True(out logic_uScriptAct_SetBool_Target_597);
		local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_597;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_597.Out)
		{
			Relay_False_595();
		}
	}

	private void Relay_False_597()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_597.False(out logic_uScriptAct_SetBool_Target_597);
		local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_597;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_597.Out)
		{
			Relay_False_595();
		}
	}

	private void Relay_True_602()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_602.True(out logic_uScriptAct_SetBool_Target_602);
		local_MsgIntroStarted_System_Boolean = logic_uScriptAct_SetBool_Target_602;
	}

	private void Relay_False_602()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_602.False(out logic_uScriptAct_SetBool_Target_602);
		local_MsgIntroStarted_System_Boolean = logic_uScriptAct_SetBool_Target_602;
	}

	private void Relay_True_603()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_603.True(out logic_uScriptAct_SetBool_Target_603);
		local_MsgIntroStarted_System_Boolean = logic_uScriptAct_SetBool_Target_603;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_603.Out)
		{
			Relay_In_552();
		}
	}

	private void Relay_False_603()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_603.False(out logic_uScriptAct_SetBool_Target_603);
		local_MsgIntroStarted_System_Boolean = logic_uScriptAct_SetBool_Target_603;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_603.Out)
		{
			Relay_In_552();
		}
	}

	private void Relay_True_606()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_606.True(out logic_uScriptAct_SetBool_Target_606);
		local_MsgIntroStarted_System_Boolean = logic_uScriptAct_SetBool_Target_606;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_606.Out)
		{
			Relay_In_372();
		}
	}

	private void Relay_False_606()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_606.False(out logic_uScriptAct_SetBool_Target_606);
		local_MsgIntroStarted_System_Boolean = logic_uScriptAct_SetBool_Target_606;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_606.Out)
		{
			Relay_In_372();
		}
	}

	private void Relay_Out_638()
	{
	}

	private void Relay_In_638()
	{
		int num = 0;
		Array stage2_Enemy01_Data = Stage2_Enemy01_Data;
		if (logic_SubGraph_KillEmAll_11_ghostTechSpawnData01_638.Length != num + stage2_Enemy01_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_11_ghostTechSpawnData01_638, num + stage2_Enemy01_Data.Length);
		}
		Array.Copy(stage2_Enemy01_Data, 0, logic_SubGraph_KillEmAll_11_ghostTechSpawnData01_638, num, stage2_Enemy01_Data.Length);
		num += stage2_Enemy01_Data.Length;
		int num2 = 0;
		Array stage2_Enemy02_Data = Stage2_Enemy02_Data;
		if (logic_SubGraph_KillEmAll_11_ghostTechSpawnData02_638.Length != num2 + stage2_Enemy02_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_11_ghostTechSpawnData02_638, num2 + stage2_Enemy02_Data.Length);
		}
		Array.Copy(stage2_Enemy02_Data, 0, logic_SubGraph_KillEmAll_11_ghostTechSpawnData02_638, num2, stage2_Enemy02_Data.Length);
		num2 += stage2_Enemy02_Data.Length;
		int num3 = 0;
		Array stage2_Enemy03_Data = Stage2_Enemy03_Data;
		if (logic_SubGraph_KillEmAll_11_ghostTechSpawnData03_638.Length != num3 + stage2_Enemy03_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_11_ghostTechSpawnData03_638, num3 + stage2_Enemy03_Data.Length);
		}
		Array.Copy(stage2_Enemy03_Data, 0, logic_SubGraph_KillEmAll_11_ghostTechSpawnData03_638, num3, stage2_Enemy03_Data.Length);
		num3 += stage2_Enemy03_Data.Length;
		int num4 = 0;
		Array stage2_Enemy04_Data = Stage2_Enemy04_Data;
		if (logic_SubGraph_KillEmAll_11_ghostTechSpawnData04_638.Length != num4 + stage2_Enemy04_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_11_ghostTechSpawnData04_638, num4 + stage2_Enemy04_Data.Length);
		}
		Array.Copy(stage2_Enemy04_Data, 0, logic_SubGraph_KillEmAll_11_ghostTechSpawnData04_638, num4, stage2_Enemy04_Data.Length);
		num4 += stage2_Enemy04_Data.Length;
		int num5 = 0;
		Array stage2_Enemy05_Data = Stage2_Enemy05_Data;
		if (logic_SubGraph_KillEmAll_11_ghostTechSpawnData05_638.Length != num5 + stage2_Enemy05_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_11_ghostTechSpawnData05_638, num5 + stage2_Enemy05_Data.Length);
		}
		Array.Copy(stage2_Enemy05_Data, 0, logic_SubGraph_KillEmAll_11_ghostTechSpawnData05_638, num5, stage2_Enemy05_Data.Length);
		num5 += stage2_Enemy05_Data.Length;
		int num6 = 0;
		Array stage2_Enemy06_Data = Stage2_Enemy06_Data;
		if (logic_SubGraph_KillEmAll_11_ghostTechSpawnData06_638.Length != num6 + stage2_Enemy06_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_11_ghostTechSpawnData06_638, num6 + stage2_Enemy06_Data.Length);
		}
		Array.Copy(stage2_Enemy06_Data, 0, logic_SubGraph_KillEmAll_11_ghostTechSpawnData06_638, num6, stage2_Enemy06_Data.Length);
		num6 += stage2_Enemy06_Data.Length;
		int num7 = 0;
		Array stage2_Enemy07_Data = Stage2_Enemy07_Data;
		if (logic_SubGraph_KillEmAll_11_ghostTechSpawnData07_638.Length != num7 + stage2_Enemy07_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_11_ghostTechSpawnData07_638, num7 + stage2_Enemy07_Data.Length);
		}
		Array.Copy(stage2_Enemy07_Data, 0, logic_SubGraph_KillEmAll_11_ghostTechSpawnData07_638, num7, stage2_Enemy07_Data.Length);
		num7 += stage2_Enemy07_Data.Length;
		int num8 = 0;
		Array stage2_Enemy08_Data = Stage2_Enemy08_Data;
		if (logic_SubGraph_KillEmAll_11_ghostTechSpawnData08_638.Length != num8 + stage2_Enemy08_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_11_ghostTechSpawnData08_638, num8 + stage2_Enemy08_Data.Length);
		}
		Array.Copy(stage2_Enemy08_Data, 0, logic_SubGraph_KillEmAll_11_ghostTechSpawnData08_638, num8, stage2_Enemy08_Data.Length);
		num8 += stage2_Enemy08_Data.Length;
		int num9 = 0;
		Array stage2_Enemy09_Data = Stage2_Enemy09_Data;
		if (logic_SubGraph_KillEmAll_11_ghostTechSpawnData09_638.Length != num9 + stage2_Enemy09_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_11_ghostTechSpawnData09_638, num9 + stage2_Enemy09_Data.Length);
		}
		Array.Copy(stage2_Enemy09_Data, 0, logic_SubGraph_KillEmAll_11_ghostTechSpawnData09_638, num9, stage2_Enemy09_Data.Length);
		num9 += stage2_Enemy09_Data.Length;
		int num10 = 0;
		Array stage2_Enemy10_Data = Stage2_Enemy10_Data;
		if (logic_SubGraph_KillEmAll_11_ghostTechSpawnData10_638.Length != num10 + stage2_Enemy10_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_11_ghostTechSpawnData10_638, num10 + stage2_Enemy10_Data.Length);
		}
		Array.Copy(stage2_Enemy10_Data, 0, logic_SubGraph_KillEmAll_11_ghostTechSpawnData10_638, num10, stage2_Enemy10_Data.Length);
		num10 += stage2_Enemy10_Data.Length;
		int num11 = 0;
		Array stage2_Enemy11_Data = Stage2_Enemy11_Data;
		if (logic_SubGraph_KillEmAll_11_ghostTechSpawnData11_638.Length != num11 + stage2_Enemy11_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_11_ghostTechSpawnData11_638, num11 + stage2_Enemy11_Data.Length);
		}
		Array.Copy(stage2_Enemy11_Data, 0, logic_SubGraph_KillEmAll_11_ghostTechSpawnData11_638, num11, stage2_Enemy11_Data.Length);
		num11 += stage2_Enemy11_Data.Length;
		logic_SubGraph_KillEmAll_11_SubGraph_KillEmAll_11_638.In(logic_SubGraph_KillEmAll_11_ghostTechSpawnData01_638, logic_SubGraph_KillEmAll_11_ghostTechSpawnData02_638, logic_SubGraph_KillEmAll_11_ghostTechSpawnData03_638, logic_SubGraph_KillEmAll_11_ghostTechSpawnData04_638, logic_SubGraph_KillEmAll_11_ghostTechSpawnData05_638, logic_SubGraph_KillEmAll_11_ghostTechSpawnData06_638, logic_SubGraph_KillEmAll_11_ghostTechSpawnData07_638, logic_SubGraph_KillEmAll_11_ghostTechSpawnData08_638, logic_SubGraph_KillEmAll_11_ghostTechSpawnData09_638, logic_SubGraph_KillEmAll_11_ghostTechSpawnData10_638, logic_SubGraph_KillEmAll_11_ghostTechSpawnData11_638);
	}

	private void Relay_Out_639()
	{
	}

	private void Relay_In_639()
	{
		int num = 0;
		Array stage1_Enemy01_Data = Stage1_Enemy01_Data;
		if (logic_SubGraph_KillEmAll_04_ghostTechSpawnData01_639.Length != num + stage1_Enemy01_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_04_ghostTechSpawnData01_639, num + stage1_Enemy01_Data.Length);
		}
		Array.Copy(stage1_Enemy01_Data, 0, logic_SubGraph_KillEmAll_04_ghostTechSpawnData01_639, num, stage1_Enemy01_Data.Length);
		num += stage1_Enemy01_Data.Length;
		int num2 = 0;
		Array stage1_Enemy02_Data = Stage1_Enemy02_Data;
		if (logic_SubGraph_KillEmAll_04_ghostTechSpawnData02_639.Length != num2 + stage1_Enemy02_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_04_ghostTechSpawnData02_639, num2 + stage1_Enemy02_Data.Length);
		}
		Array.Copy(stage1_Enemy02_Data, 0, logic_SubGraph_KillEmAll_04_ghostTechSpawnData02_639, num2, stage1_Enemy02_Data.Length);
		num2 += stage1_Enemy02_Data.Length;
		int num3 = 0;
		Array stage1_Enemy03_Data = Stage1_Enemy03_Data;
		if (logic_SubGraph_KillEmAll_04_ghostTechSpawnData03_639.Length != num3 + stage1_Enemy03_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_04_ghostTechSpawnData03_639, num3 + stage1_Enemy03_Data.Length);
		}
		Array.Copy(stage1_Enemy03_Data, 0, logic_SubGraph_KillEmAll_04_ghostTechSpawnData03_639, num3, stage1_Enemy03_Data.Length);
		num3 += stage1_Enemy03_Data.Length;
		int num4 = 0;
		Array stage1_Enemy04_Data = Stage1_Enemy04_Data;
		if (logic_SubGraph_KillEmAll_04_ghostTechSpawnData04_639.Length != num4 + stage1_Enemy04_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_04_ghostTechSpawnData04_639, num4 + stage1_Enemy04_Data.Length);
		}
		Array.Copy(stage1_Enemy04_Data, 0, logic_SubGraph_KillEmAll_04_ghostTechSpawnData04_639, num4, stage1_Enemy04_Data.Length);
		num4 += stage1_Enemy04_Data.Length;
		logic_SubGraph_KillEmAll_04_SubGraph_KillEmAll_04_639.In(logic_SubGraph_KillEmAll_04_ghostTechSpawnData01_639, logic_SubGraph_KillEmAll_04_ghostTechSpawnData02_639, logic_SubGraph_KillEmAll_04_ghostTechSpawnData03_639, logic_SubGraph_KillEmAll_04_ghostTechSpawnData04_639);
	}

	private void Relay_Out_640()
	{
	}

	private void Relay_In_640()
	{
		int num = 0;
		Array stage3_Enemy01_Data = Stage3_Enemy01_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData01_640.Length != num + stage3_Enemy01_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData01_640, num + stage3_Enemy01_Data.Length);
		}
		Array.Copy(stage3_Enemy01_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData01_640, num, stage3_Enemy01_Data.Length);
		num += stage3_Enemy01_Data.Length;
		int num2 = 0;
		Array stage3_Enemy02_Data = Stage3_Enemy02_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData02_640.Length != num2 + stage3_Enemy02_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData02_640, num2 + stage3_Enemy02_Data.Length);
		}
		Array.Copy(stage3_Enemy02_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData02_640, num2, stage3_Enemy02_Data.Length);
		num2 += stage3_Enemy02_Data.Length;
		int num3 = 0;
		Array stage3_Enemy03_Data = Stage3_Enemy03_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData03_640.Length != num3 + stage3_Enemy03_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData03_640, num3 + stage3_Enemy03_Data.Length);
		}
		Array.Copy(stage3_Enemy03_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData03_640, num3, stage3_Enemy03_Data.Length);
		num3 += stage3_Enemy03_Data.Length;
		int num4 = 0;
		Array stage3_Enemy04_Data = Stage3_Enemy04_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData04_640.Length != num4 + stage3_Enemy04_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData04_640, num4 + stage3_Enemy04_Data.Length);
		}
		Array.Copy(stage3_Enemy04_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData04_640, num4, stage3_Enemy04_Data.Length);
		num4 += stage3_Enemy04_Data.Length;
		int num5 = 0;
		Array stage3_Enemy05_Data = Stage3_Enemy05_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData05_640.Length != num5 + stage3_Enemy05_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData05_640, num5 + stage3_Enemy05_Data.Length);
		}
		Array.Copy(stage3_Enemy05_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData05_640, num5, stage3_Enemy05_Data.Length);
		num5 += stage3_Enemy05_Data.Length;
		int num6 = 0;
		Array stage3_Enemy06_Data = Stage3_Enemy06_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData06_640.Length != num6 + stage3_Enemy06_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData06_640, num6 + stage3_Enemy06_Data.Length);
		}
		Array.Copy(stage3_Enemy06_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData06_640, num6, stage3_Enemy06_Data.Length);
		num6 += stage3_Enemy06_Data.Length;
		int num7 = 0;
		Array stage3_Enemy07_Data = Stage3_Enemy07_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData07_640.Length != num7 + stage3_Enemy07_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData07_640, num7 + stage3_Enemy07_Data.Length);
		}
		Array.Copy(stage3_Enemy07_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData07_640, num7, stage3_Enemy07_Data.Length);
		num7 += stage3_Enemy07_Data.Length;
		int num8 = 0;
		Array stage3_Enemy08_Data = Stage3_Enemy08_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData08_640.Length != num8 + stage3_Enemy08_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData08_640, num8 + stage3_Enemy08_Data.Length);
		}
		Array.Copy(stage3_Enemy08_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData08_640, num8, stage3_Enemy08_Data.Length);
		num8 += stage3_Enemy08_Data.Length;
		int num9 = 0;
		Array stage3_Enemy09_Data = Stage3_Enemy09_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData09_640.Length != num9 + stage3_Enemy09_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData09_640, num9 + stage3_Enemy09_Data.Length);
		}
		Array.Copy(stage3_Enemy09_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData09_640, num9, stage3_Enemy09_Data.Length);
		num9 += stage3_Enemy09_Data.Length;
		int num10 = 0;
		Array stage3_Enemy10_Data = Stage3_Enemy10_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData10_640.Length != num10 + stage3_Enemy10_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData10_640, num10 + stage3_Enemy10_Data.Length);
		}
		Array.Copy(stage3_Enemy10_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData10_640, num10, stage3_Enemy10_Data.Length);
		num10 += stage3_Enemy10_Data.Length;
		int num11 = 0;
		Array stage3_Enemy11_Data = Stage3_Enemy11_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData11_640.Length != num11 + stage3_Enemy11_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData11_640, num11 + stage3_Enemy11_Data.Length);
		}
		Array.Copy(stage3_Enemy11_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData11_640, num11, stage3_Enemy11_Data.Length);
		num11 += stage3_Enemy11_Data.Length;
		int num12 = 0;
		Array stage3_Enemy12_Data = Stage3_Enemy12_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData12_640.Length != num12 + stage3_Enemy12_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData12_640, num12 + stage3_Enemy12_Data.Length);
		}
		Array.Copy(stage3_Enemy12_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData12_640, num12, stage3_Enemy12_Data.Length);
		num12 += stage3_Enemy12_Data.Length;
		int num13 = 0;
		Array stage3_Enemy13_Data = Stage3_Enemy13_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData13_640.Length != num13 + stage3_Enemy13_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData13_640, num13 + stage3_Enemy13_Data.Length);
		}
		Array.Copy(stage3_Enemy13_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData13_640, num13, stage3_Enemy13_Data.Length);
		num13 += stage3_Enemy13_Data.Length;
		int num14 = 0;
		Array stage3_Enemy14_Data = Stage3_Enemy14_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData14_640.Length != num14 + stage3_Enemy14_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData14_640, num14 + stage3_Enemy14_Data.Length);
		}
		Array.Copy(stage3_Enemy14_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData14_640, num14, stage3_Enemy14_Data.Length);
		num14 += stage3_Enemy14_Data.Length;
		int num15 = 0;
		Array stage3_Enemy15_Data = Stage3_Enemy15_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData15_640.Length != num15 + stage3_Enemy15_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData15_640, num15 + stage3_Enemy15_Data.Length);
		}
		Array.Copy(stage3_Enemy15_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData15_640, num15, stage3_Enemy15_Data.Length);
		num15 += stage3_Enemy15_Data.Length;
		int num16 = 0;
		Array stage3_Enemy16_Data = Stage3_Enemy16_Data;
		if (logic_SubGraph_KillEmAll_16_ghostTechSpawnData16_640.Length != num16 + stage3_Enemy16_Data.Length)
		{
			Array.Resize(ref logic_SubGraph_KillEmAll_16_ghostTechSpawnData16_640, num16 + stage3_Enemy16_Data.Length);
		}
		Array.Copy(stage3_Enemy16_Data, 0, logic_SubGraph_KillEmAll_16_ghostTechSpawnData16_640, num16, stage3_Enemy16_Data.Length);
		num16 += stage3_Enemy16_Data.Length;
		logic_SubGraph_KillEmAll_16_SubGraph_KillEmAll_16_640.In(logic_SubGraph_KillEmAll_16_ghostTechSpawnData01_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData02_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData03_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData04_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData05_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData06_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData07_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData08_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData09_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData10_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData11_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData12_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData13_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData14_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData15_640, logic_SubGraph_KillEmAll_16_ghostTechSpawnData16_640);
	}

	private void Relay_In_641()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_641.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_641.Out)
		{
			Relay_In_441();
		}
	}

	private void Relay_In_642()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData02;
		if (logic_uScript_DestroyTechsFromData_techData_642.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_642, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_DestroyTechsFromData_techData_642, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_642 = owner_Connection_643;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_642.In(logic_uScript_DestroyTechsFromData_techData_642, logic_uScript_DestroyTechsFromData_shouldExplode_642, logic_uScript_DestroyTechsFromData_ownerNode_642);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_642.Out)
		{
			Relay_Succeed_41();
		}
	}
}
