using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_SetPiece_CanyonSpiral : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public float distAtWhichNPCTechFound = 75f;

	public float distEnemiesSpottedBoss;

	public float distEnemiesSpottedMinions;

	[Multiline(3)]
	public string EnemyPosH = "";

	public SpawnTechData[] enemyTechDataA = new SpawnTechData[0];

	public SpawnTechData[] enemyTechDataB = new SpawnTechData[0];

	public SpawnTechData[] enemyTechDataBoss = new SpawnTechData[0];

	public SpawnTechData[] enemyTechDataC = new SpawnTechData[0];

	public SpawnTechData[] enemyTechDataChargingPoint = new SpawnTechData[0];

	public SpawnTechData[] enemyTechDataD = new SpawnTechData[0];

	public SpawnTechData[] enemyTechDataE = new SpawnTechData[0];

	public SpawnTechData[] enemyTechDataF = new SpawnTechData[0];

	public SpawnTechData[] enemyTechDataG = new SpawnTechData[0];

	private string local_265_System_String = "Trigger4";

	private string local_266_System_String = "Trigger4";

	private string local_267_System_String = "Trigger4";

	private string local_268_System_String = "Trigger4";

	private string local_271_System_String = "Trigger4";

	private string local_272_System_String = "Trigger3";

	private string local_274_System_String = "Trigger3";

	private string local_275_System_String = "Trigger3";

	private string local_277_System_String = "Trigger3";

	private string local_278_System_String = "Trigger3";

	private string local_280_System_String = "Trigger2";

	private string local_283_System_String = "Trigger2";

	private string local_284_System_String = "Trigger2";

	private string local_286_System_String = "Trigger2";

	private string local_287_System_String = "Trigger2";

	private string local_288_System_String = "Trigger2";

	private string local_290_System_String = "Trigger2";

	private string local_292_System_String = "Trigger1";

	private string local_293_System_String = "Trigger1";

	private string local_294_System_String = "Trigger1";

	private string local_295_System_String = "Trigger1";

	private string local_297_System_String = "Trigger1";

	private string local_299_System_String = "Trigger1";

	private string local_300_System_String = "Trigger1";

	private string local_306_System_String = "msgMeeting";

	private string local_314_System_String = "msgMeeting";

	private string local_361_System_String = "Trigger1";

	private string local_362_System_String = "Trigger2";

	private string local_364_System_String = "Trigger3";

	private string local_367_System_String = "Trigger4";

	private string local_368_System_String = "BossGreeting";

	private string local_369_System_String = "BossGreeting";

	private Tank local_BossTech_Tank;

	private bool local_BossTechsSpawned_System_Boolean;

	private Tank local_ChargingTech_Tank;

	private bool local_EnemySpottedA_System_Boolean;

	private bool local_EnemySpottedB_System_Boolean;

	private bool local_EnemySpottedBoss_System_Boolean;

	private bool local_EnemySpottedC_System_Boolean;

	private bool local_EnemySpottedD_System_Boolean;

	private bool local_EnemySpottedE_System_Boolean;

	private bool local_EnemySpottedF_System_Boolean;

	private Tank[] local_EnemyTechsA_TankArray = new Tank[0];

	private Tank[] local_EnemyTechsB_TankArray = new Tank[0];

	private Tank[] local_EnemyTechsBoss_TankArray = new Tank[0];

	private Tank[] local_EnemyTechsC_TankArray = new Tank[0];

	private Tank[] local_EnemyTechsChargingPoint_TankArray = new Tank[0];

	private Tank[] local_EnemyTechsD_TankArray = new Tank[0];

	private Tank[] local_EnemyTechsE_TankArray = new Tank[0];

	private Tank[] local_EnemyTechsF_TankArray = new Tank[0];

	private Tank[] local_EnemyTechsG_TankArray = new Tank[0];

	private bool local_NPCIgnored_System_Boolean;

	private bool local_NPCMet_System_Boolean;

	private bool local_NPCSeen_System_Boolean;

	private Tank local_NPCTank_Tank;

	private Tank[] local_NPCTanks_TankArray = new Tank[0];

	private bool local_NPCTechsSpawned_System_Boolean;

	private bool local_ObjectiveComplete_System_Boolean;

	private int local_ObjectiveStage_System_Int32;

	private bool local_TechsDeadA_System_Boolean;

	private bool local_TechsDeadB_System_Boolean;

	private bool local_TechsDeadBandA_System_Boolean;

	private bool local_TechsDeadC_System_Boolean;

	private bool local_TechsDeadCandD_System_Boolean;

	private bool local_TechsDeadD_System_Boolean;

	private bool local_TechsDeadE_System_Boolean;

	private bool local_TechsDeadF_System_Boolean;

	private bool local_TriggerHit1_System_Boolean;

	private bool local_TriggerHit2_System_Boolean;

	private bool local_TriggerHit3_System_Boolean;

	private bool local_TriggerHit4_System_Boolean;

	private bool local_TriggerHit5or6_System_Boolean;

	private bool local_TriggerHitA_System_Boolean;

	private bool local_TriggerHitB_System_Boolean;

	private bool local_TriggerHitC_System_Boolean;

	private bool local_TriggerHitD_System_Boolean;

	public ManOnScreenMessages.Speaker messageSpeakerBoss;

	public ManOnScreenMessages.Speaker messageSpeakerGeneric;

	public ManOnScreenMessages.Speaker messageSpeakerMinion;

	public ManOnScreenMessages.Speaker messageSpeakerNPC;

	public LocalisedString[] msgEnemySpottedA = new LocalisedString[0];

	public LocalisedString[] msgEnemySpottedB = new LocalisedString[0];

	public LocalisedString[] msgEnemySpottedBoss = new LocalisedString[0];

	public LocalisedString[] msgEnemySpottedC = new LocalisedString[0];

	public LocalisedString[] msgEnemySpottedD = new LocalisedString[0];

	public LocalisedString[] msgEnemySpottedE = new LocalisedString[0];

	public LocalisedString[] msgEnemySpottedF = new LocalisedString[0];

	public LocalisedString[] msgMissionCompleteGeneric = new LocalisedString[0];

	public LocalisedString[] msgMissionCompleteNPC = new LocalisedString[0];

	public LocalisedString[] msgNPCGreeting = new LocalisedString[0];

	public LocalisedString[] msgNPCGreetingInturrupt = new LocalisedString[0];

	public LocalisedString[] msgTechsDeadBandA = new LocalisedString[0];

	public LocalisedString[] msgTechsDeadCandD = new LocalisedString[0];

	public LocalisedString[] msgTechsDeadE = new LocalisedString[0];

	public LocalisedString[] msgTechsDeadF = new LocalisedString[0];

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	[Multiline(3)]
	public string NPCPos = "";

	public SpawnTechData[] NPCTechData = new SpawnTechData[0];

	[Multiline(3)]
	public string Trigger1 = "";

	[Multiline(3)]
	public string Trigger2 = "";

	[Multiline(3)]
	public string Trigger3 = "";

	[Multiline(3)]
	public string Trigger4 = "";

	[Multiline(3)]
	public string Trigger5 = "";

	[Multiline(3)]
	public string Trigger6 = "";

	[Multiline(3)]
	public string TriggerA = "";

	[Multiline(3)]
	public string TriggerB = "";

	[Multiline(3)]
	public string TriggerC = "";

	[Multiline(3)]
	public string TriggerD = "";

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_23;

	private GameObject owner_Connection_32;

	private GameObject owner_Connection_55;

	private GameObject owner_Connection_68;

	private GameObject owner_Connection_122;

	private GameObject owner_Connection_123;

	private GameObject owner_Connection_149;

	private GameObject owner_Connection_154;

	private GameObject owner_Connection_177;

	private GameObject owner_Connection_182;

	private GameObject owner_Connection_205;

	private GameObject owner_Connection_231;

	private GameObject owner_Connection_237;

	private GameObject owner_Connection_257;

	private GameObject owner_Connection_318;

	private GameObject owner_Connection_342;

	private GameObject owner_Connection_347;

	private GameObject owner_Connection_371;

	private GameObject owner_Connection_373;

	private GameObject owner_Connection_397;

	private GameObject owner_Connection_455;

	private GameObject owner_Connection_458;

	private GameObject owner_Connection_463;

	private GameObject owner_Connection_465;

	private GameObject owner_Connection_473;

	private GameObject owner_Connection_480;

	private GameObject owner_Connection_483;

	private GameObject owner_Connection_485;

	private GameObject owner_Connection_493;

	private GameObject owner_Connection_502;

	private GameObject owner_Connection_503;

	private GameObject owner_Connection_504;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_4 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_4;

	private bool logic_uScript_FinishEncounter_Out_4 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_12;

	private bool logic_uScriptCon_CompareBool_True_12 = true;

	private bool logic_uScriptCon_CompareBool_False_12 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_15 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_15 = ManOnScreenMessages.MessagePriority.High;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_15;

	private string logic_uScript_AddOnScreenMessage_tag_15 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_15;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_15;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_15;

	private bool logic_uScript_AddOnScreenMessage_Out_15 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_15 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_17;

	private bool logic_uScriptCon_CompareBool_True_17 = true;

	private bool logic_uScriptCon_CompareBool_False_17 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_18;

	private bool logic_uScriptCon_CompareBool_True_18 = true;

	private bool logic_uScriptCon_CompareBool_False_18 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_20 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_20 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_20 = ManOnScreenMessages.MessagePriority.High;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_20;

	private string logic_uScript_AddOnScreenMessage_tag_20 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_20;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_20;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_20;

	private bool logic_uScript_AddOnScreenMessage_Out_20 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_20 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_21 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_21;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_21 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_21;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_21 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_21 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_21 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_21 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_22 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_22;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_22 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_22;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_22 = true;

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_24 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_24 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_24;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_24 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_24 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_24 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_26 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_26;

	private bool logic_uScriptAct_SetBool_Out_26 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_26 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_26 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_27 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_27;

	private bool logic_uScriptAct_SetBool_Out_27 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_27 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_27 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_29 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_29 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_29 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_29 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_31 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_31 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_31;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_31 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_31 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_35 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_35 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_35 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_35 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_35 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_40 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_40;

	private bool logic_uScriptAct_SetBool_Out_40 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_40 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_40 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_43;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_43 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_43 = "EnemySpottedBoss";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_45 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_47;

	private bool logic_uScriptCon_CompareBool_True_47 = true;

	private bool logic_uScriptCon_CompareBool_False_47 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_49 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_50 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_50;

	private bool logic_uScriptAct_SetBool_Out_50 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_50 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_50 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_51 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_51 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_51 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_51 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_51 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_54 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_54 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_54;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_54 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_54 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_57 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_57 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_57 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_57;

	private string logic_uScript_AddOnScreenMessage_tag_57 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_57;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_57;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_57;

	private bool logic_uScript_AddOnScreenMessage_Out_57 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_57 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_58 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_58;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_58 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_58;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_58 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_58 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_58 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_58 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_60 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_60;

	private bool logic_uScriptAct_SetBool_Out_60 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_60 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_60 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_61;

	private bool logic_uScriptCon_CompareBool_True_61 = true;

	private bool logic_uScriptCon_CompareBool_False_61 = true;

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_62 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_62 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_62;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_62 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_62 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_62 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_69 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_69 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_69 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_69;

	private string logic_uScript_AddOnScreenMessage_tag_69 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_69;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_69;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_69;

	private bool logic_uScript_AddOnScreenMessage_Out_69 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_69 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_77;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_77 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_77 = "TriggerHit4";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_78;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_78 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_78 = "TriggerHit5or6";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_79;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_79 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_79 = "TriggerHit3";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_80;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_80 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_80 = "TriggerHit2";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_81;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_81 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_81 = "TriggerHit1";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_85;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_85 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_85 = "EnemySpottedF";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_87;

	private bool logic_uScriptCon_CompareBool_True_87 = true;

	private bool logic_uScriptCon_CompareBool_False_87 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_88 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_88;

	private bool logic_uScriptAct_SetBool_Out_88 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_88 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_88 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_90 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_92;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_92 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_92 = "TechsDeadF";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_93;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_93 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_93 = "ObjectiveComplete";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_95;

	private bool logic_uScriptCon_CompareBool_True_95 = true;

	private bool logic_uScriptCon_CompareBool_False_95 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_99 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_99 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_99 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_99;

	private string logic_uScript_AddOnScreenMessage_tag_99 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_99;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_99;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_99;

	private bool logic_uScript_AddOnScreenMessage_Out_99 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_99 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_100 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_100 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_100 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_100;

	private string logic_uScript_AddOnScreenMessage_tag_100 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_100;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_100;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_100;

	private bool logic_uScript_AddOnScreenMessage_Out_100 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_100 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_102;

	private bool logic_uScriptCon_CompareBool_True_102 = true;

	private bool logic_uScriptCon_CompareBool_False_102 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_105 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_105;

	private bool logic_uScriptAct_SetBool_Out_105 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_105 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_105 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_106 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_106 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_106;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_106 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_106;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_106 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_106 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_106 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_106 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_107 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_109 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_109 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_109 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_109 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_109 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_113;

	private bool logic_uScriptCon_CompareBool_True_113 = true;

	private bool logic_uScriptCon_CompareBool_False_113 = true;

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_114 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_114 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_114;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_114 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_114 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_114 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_115 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_115 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_115;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_115 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_115 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_117 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_117;

	private bool logic_uScriptAct_SetBool_Out_117 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_117 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_117 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_124 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_124;

	private bool logic_uScriptAct_SetBool_Out_124 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_124 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_124 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_127;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_127 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_127 = "EnemySpottedE";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_128;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_128 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_128 = "TechsDeadE";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_130;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_130 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_130 = "EnemySpottedD";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_133;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_133 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_133 = "TechsDeadD";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_134;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_134 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_134 = "EnemySpottedC";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_137;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_137 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_137 = "TechsDeadC";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_139;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_139 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_139 = "EnemySpottedB";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_140;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_140 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_140 = "TechsDeadB";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_143;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_143 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_143 = "EnemySpottedA";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_145;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_145 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_145 = "TechsDeadA";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_147 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_147 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_147;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_147 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_147;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_147 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_147 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_147 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_147 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_151 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_151;

	private bool logic_uScriptAct_SetBool_Out_151 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_151 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_151 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_153 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_153 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_153 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_153;

	private string logic_uScript_AddOnScreenMessage_tag_153 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_153;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_153;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_153;

	private bool logic_uScript_AddOnScreenMessage_Out_153 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_153 = true;

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_155 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_155 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_155;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_155 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_155 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_155 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_157 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_157;

	private bool logic_uScriptAct_SetBool_Out_157 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_157 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_157 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_159 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_159 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_159;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_159 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_159 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_161 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_161 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_161;

	private string logic_uScript_AddOnScreenMessage_tag_161 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_161;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_161;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_161;

	private bool logic_uScript_AddOnScreenMessage_Out_161 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_161 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_163;

	private bool logic_uScriptCon_CompareBool_True_163 = true;

	private bool logic_uScriptCon_CompareBool_False_163 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_165 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_165 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_169 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_169;

	private bool logic_uScriptCon_CompareBool_True_169 = true;

	private bool logic_uScriptCon_CompareBool_False_169 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_170 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_170;

	private bool logic_uScriptCon_CompareBool_True_170 = true;

	private bool logic_uScriptCon_CompareBool_False_170 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_174 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_174 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_174 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_174 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_174 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_175 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_175;

	private bool logic_uScriptAct_SetBool_Out_175 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_175 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_175 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_178 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_178 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_178;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_178 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_178 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_179 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_179 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_179;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_179 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_179;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_179 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_179 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_179 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_179 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_183 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_183;

	private bool logic_uScriptAct_SetBool_Out_183 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_183 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_183 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_184 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_184;

	private bool logic_uScriptAct_SetBool_Out_184 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_184 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_184 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_185;

	private bool logic_uScriptCon_CompareBool_True_185 = true;

	private bool logic_uScriptCon_CompareBool_False_185 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_186;

	private bool logic_uScriptCon_CompareBool_True_186 = true;

	private bool logic_uScriptCon_CompareBool_False_186 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_193;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_193 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_193 = "TechsDeadCandD";

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_195 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_195 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_195;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_195 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_195 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_195 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_200 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_200 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_200;

	private string logic_uScript_AddOnScreenMessage_tag_200 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_200;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_200;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_200;

	private bool logic_uScript_AddOnScreenMessage_Out_200 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_200 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_201 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_201;

	private bool logic_uScriptAct_SetBool_Out_201 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_201 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_201 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_202 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_202;

	private bool logic_uScriptCon_CompareBool_True_202 = true;

	private bool logic_uScriptCon_CompareBool_False_202 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_209 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_209 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_209;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_209 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_209;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_209 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_209 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_209 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_209 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_210 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_210 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_210;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_210 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_210;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_210 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_210 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_210 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_210 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_216 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_216;

	private bool logic_uScriptCon_CompareBool_True_216 = true;

	private bool logic_uScriptCon_CompareBool_False_216 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_222 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_222;

	private bool logic_uScriptAct_SetBool_Out_222 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_222 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_222 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_223;

	private bool logic_uScriptCon_CompareBool_True_223 = true;

	private bool logic_uScriptCon_CompareBool_False_223 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_226 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_226;

	private bool logic_uScriptCon_CompareBool_True_226 = true;

	private bool logic_uScriptCon_CompareBool_False_226 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_229 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_229;

	private bool logic_uScriptAct_SetBool_Out_229 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_229 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_229 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_234 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_234;

	private bool logic_uScriptAct_SetBool_Out_234 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_234 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_234 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_235 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_235;

	private bool logic_uScriptAct_SetBool_Out_235 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_235 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_235 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_236 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_236 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_236 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_236 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_236 = true;

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_238 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_238 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_238;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_238 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_238 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_238 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_240 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_240 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_240;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_240 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_240 = true;

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_241 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_241 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_241;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_241 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_241 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_241 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_243 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_243 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_243 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_243;

	private string logic_uScript_AddOnScreenMessage_tag_243 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_243;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_243;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_243;

	private bool logic_uScript_AddOnScreenMessage_Out_243 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_243 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_245 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_245 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_245 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_245;

	private string logic_uScript_AddOnScreenMessage_tag_245 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_245;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_245;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_245;

	private bool logic_uScript_AddOnScreenMessage_Out_245 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_245 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_246 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_246;

	private bool logic_uScriptAct_SetBool_Out_246 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_246 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_246 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_247 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_247 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_247;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_247 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_247 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_250 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_250;

	private bool logic_uScriptCon_CompareBool_True_250 = true;

	private bool logic_uScriptCon_CompareBool_False_250 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_251 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_251;

	private bool logic_uScriptCon_CompareBool_True_251 = true;

	private bool logic_uScriptCon_CompareBool_False_251 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_252 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_252;

	private bool logic_uScriptCon_CompareBool_True_252 = true;

	private bool logic_uScriptCon_CompareBool_False_252 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_253 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_253 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_253 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_253;

	private string logic_uScript_AddOnScreenMessage_tag_253 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_253;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_253;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_253;

	private bool logic_uScript_AddOnScreenMessage_Out_253 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_253 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_258 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_258;

	private bool logic_uScriptAct_SetBool_Out_258 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_258 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_258 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_259 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_259 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_261;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_261 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_261 = "TechsDeadBandA";

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_262 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_262 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_262;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_262 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_263 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_263 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_263;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_263 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_264 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_264 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_264;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_264 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_269 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_269 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_269;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_269 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_270 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_270 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_270;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_270 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_273 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_273 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_273;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_273 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_276 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_276 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_276;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_276 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_279 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_279 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_279;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_279 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_281 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_281 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_281;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_281 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_282 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_282 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_282;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_282 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_285 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_285 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_285;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_285 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_289 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_289 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_289;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_289 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_291 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_291 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_291;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_291 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_296 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_296 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_296;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_296 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_298 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_298 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_298;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_298 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_301 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_301 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_301;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_301 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_303 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_303 = new Tank[0];

	private int logic_uScript_AccessListTech_index_303;

	private Tank logic_uScript_AccessListTech_value_303;

	private bool logic_uScript_AccessListTech_Out_303 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_307 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_307;

	private bool logic_uScriptAct_SetBool_Out_307 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_307 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_307 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_308 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_308;

	private bool logic_uScriptAct_SetBool_Out_308 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_308 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_308 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_309 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_309;

	private bool logic_uScriptAct_SetBool_Out_309 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_309 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_309 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_311 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_311 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_311;

	private bool logic_uScript_SetTankInvulnerable_Out_311 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_313 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_313 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_313;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_313 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_313;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_313 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_313 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_313 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_313 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_315;

	private bool logic_uScriptCon_CompareBool_True_315 = true;

	private bool logic_uScriptCon_CompareBool_False_315 = true;

	private uScript_InRangeOfTech logic_uScript_InRangeOfTech_uScript_InRangeOfTech_319 = new uScript_InRangeOfTech();

	private Tank logic_uScript_InRangeOfTech_tank_319;

	private float logic_uScript_InRangeOfTech_range_319;

	private bool logic_uScript_InRangeOfTech_Out_319 = true;

	private bool logic_uScript_InRangeOfTech_InRange_319 = true;

	private bool logic_uScript_InRangeOfTech_OutOfRange_319 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_320 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_320;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_320 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_320 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_320;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_320;

	private bool logic_uScript_FlyTechUpAndAway_Out_320 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_321 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_321 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_321 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_321;

	private string logic_uScript_AddOnScreenMessage_tag_321 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_321;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_321;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_321;

	private bool logic_uScript_AddOnScreenMessage_Out_321 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_321 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_323 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_323 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_323 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_323;

	private string logic_uScript_AddOnScreenMessage_tag_323 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_323;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_323;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_323;

	private bool logic_uScript_AddOnScreenMessage_Out_323 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_323 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_324 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_324;

	private bool logic_uScriptCon_CompareBool_True_324 = true;

	private bool logic_uScriptCon_CompareBool_False_324 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_327 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_327 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_327;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_327 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_333 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_333;

	private bool logic_uScriptCon_CompareBool_True_333 = true;

	private bool logic_uScriptCon_CompareBool_False_333 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_334 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_334;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_334 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_334 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_334;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_334;

	private bool logic_uScript_FlyTechUpAndAway_Out_334 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_338 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_338;

	private bool logic_uScriptCon_CompareBool_True_338 = true;

	private bool logic_uScriptCon_CompareBool_False_338 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_340 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_340 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_340;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_340 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_340 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_341 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_341;

	private bool logic_uScriptAct_SetBool_Out_341 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_341 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_341 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_346;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_346 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_346 = "NPCTechsSpawned";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_348 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_348 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_348;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_348 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_348 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_350 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_350;

	private int logic_uScript_SetTankTeam_team_350 = 1;

	private bool logic_uScript_SetTankTeam_Out_350 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_355;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_355 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_355 = "NPCMet";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_356;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_356 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_356 = "NPCIgnored";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_357;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_357 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_357 = "NPCSeen";

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_363 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_363 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_363;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_363 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_365 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_365 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_365;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_365 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_366 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_366 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_366;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_366 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_370 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_370 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_370;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_370 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_370 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_374 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_374 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_374;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_374 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_374;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_374 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_374 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_374 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_374 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_377 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_377;

	private int logic_uScript_SetTankTeam_team_377 = 1;

	private bool logic_uScript_SetTankTeam_Out_377 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_378 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_378 = new Tank[0];

	private int logic_uScript_AccessListTech_index_378;

	private Tank logic_uScript_AccessListTech_value_378;

	private bool logic_uScript_AccessListTech_Out_378 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_381 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_381;

	private bool logic_uScriptCon_CompareBool_True_381 = true;

	private bool logic_uScriptCon_CompareBool_False_381 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_383 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_383;

	private bool logic_uScriptCon_CompareBool_True_383 = true;

	private bool logic_uScriptCon_CompareBool_False_383 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_385 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_385;

	private bool logic_uScriptCon_CompareBool_True_385 = true;

	private bool logic_uScriptCon_CompareBool_False_385 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_387 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_387;

	private bool logic_uScriptCon_CompareBool_True_387 = true;

	private bool logic_uScriptCon_CompareBool_False_387 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_389 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_389;

	private bool logic_uScriptCon_CompareBool_True_389 = true;

	private bool logic_uScriptCon_CompareBool_False_389 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_392 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_392 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_392 = ManOnScreenMessages.MessagePriority.High;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_392;

	private string logic_uScript_AddOnScreenMessage_tag_392 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_392;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_392;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_392;

	private bool logic_uScript_AddOnScreenMessage_Out_392 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_392 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_394 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_394 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_394 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_396 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_396;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_396 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_396;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_396 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_399 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_399;

	private bool logic_uScriptCon_CompareBool_True_399 = true;

	private bool logic_uScriptCon_CompareBool_False_399 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_401 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_401 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_401 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_401 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_401 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_403 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_403;

	private bool logic_uScriptAct_SetBool_Out_403 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_403 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_403 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_405 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_405;

	private bool logic_uScriptAct_SetBool_Out_405 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_405 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_405 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_406 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_406;

	private bool logic_uScriptCon_CompareBool_True_406 = true;

	private bool logic_uScriptCon_CompareBool_False_406 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_409 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_409 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_409 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_410 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_410 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_410 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_410 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_410 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_412 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_412 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_412 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_413 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_413 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_413 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_413 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_413 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_415 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_415;

	private bool logic_uScriptCon_CompareBool_True_415 = true;

	private bool logic_uScriptCon_CompareBool_False_415 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_418 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_418;

	private bool logic_uScriptAct_SetBool_Out_418 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_418 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_418 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_419 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_419 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_419 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_419 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_419 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_420 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_420;

	private bool logic_uScriptAct_SetBool_Out_420 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_420 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_420 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_421 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_421;

	private bool logic_uScriptCon_CompareBool_True_421 = true;

	private bool logic_uScriptCon_CompareBool_False_421 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_422 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_422 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_422 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_427;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_427 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_427 = "TriggerHitA";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_429;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_429 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_429 = "TriggerHitB";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_431;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_431 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_431 = "TriggerHitC";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_432;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_432 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_432 = "TriggerHitD";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_434 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_434;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_434 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_434 = "ObjectiveStage";

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_436 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_436 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_436 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_438 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_438;

	private bool logic_uScriptCon_CompareBool_True_438 = true;

	private bool logic_uScriptCon_CompareBool_False_438 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_440 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_440;

	private bool logic_uScriptCon_CompareBool_True_440 = true;

	private bool logic_uScriptCon_CompareBool_False_440 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_442 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_442;

	private bool logic_uScriptAct_SetBool_Out_442 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_442 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_442 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_445;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_445 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_445 = "BossTechsSpawned";

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_446 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_446 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_446 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_446 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_446 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_448 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_448 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_450 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_450 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_450 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_451 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_451 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_451 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_451;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_451;

	private bool logic_uScript_DamageTechs_Out_451 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_452 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_452 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_452;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_452 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_452;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_452 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_452 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_452 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_452 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_456 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_456 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_456;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_456 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_456;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_456 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_456 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_456 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_456 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_460 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_460 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_460 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_460;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_460;

	private bool logic_uScript_DamageTechs_Out_460 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_461 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_461 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_462 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_462 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_467 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_467 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_467;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_467 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_467;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_467 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_467 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_467 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_467 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_469 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_469 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_469 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_469;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_469;

	private bool logic_uScript_DamageTechs_Out_469 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_470 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_470 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_470;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_470 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_470;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_470 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_470 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_470 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_470 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_471 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_471 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_471 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_471;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_471;

	private bool logic_uScript_DamageTechs_Out_471 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_476 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_476 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_476 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_476;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_476;

	private bool logic_uScript_DamageTechs_Out_476 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_477 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_477 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_477;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_477 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_477;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_477 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_477 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_477 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_477 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_479 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_479 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_479 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_479;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_479;

	private bool logic_uScript_DamageTechs_Out_479 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_482 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_482 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_482;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_482 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_482;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_482 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_482 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_482 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_482 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_487 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_487;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_487 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_487;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_487 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_487 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_487 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_487 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_489 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_489 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_489 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_489;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_489;

	private bool logic_uScript_DamageTechs_Out_489 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_490 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_490 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_490;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_490 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_490;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_490 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_490 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_490 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_490 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_491 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_491 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_491 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_491;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_491;

	private bool logic_uScript_DamageTechs_Out_491 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_494 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_494 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_494 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_494;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_494;

	private bool logic_uScript_DamageTechs_Out_494 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_495 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_495 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_495;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_495 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_495;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_495 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_495 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_495 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_495 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_499 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_499 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_499;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_499 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_499;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_499 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_499 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_499 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_499 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_501 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_501 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_501 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_501;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_501;

	private bool logic_uScript_DamageTechs_Out_501 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_506 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_506 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_506 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_506;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_506;

	private bool logic_uScript_DamageTechs_Out_506 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_507 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_507 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_507;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_507 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_507;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_507 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_507 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_507 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_507 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_511 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_511 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_511;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_511 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_511;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_511 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_511 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_511 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_511 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_512 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_512 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_512 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_512;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_512;

	private bool logic_uScript_DamageTechs_Out_512 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_513 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_513 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_513 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_513 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_513 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_515 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_515 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_515 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_515 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_515 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_518 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_518 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_518 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_518 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_518 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
			if (null != owner_Connection_0)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_0.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
				}
			}
		}
		if (null == owner_Connection_5 || !m_RegisteredForEvents)
		{
			owner_Connection_5 = parentGameObject;
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
			if (null != owner_Connection_9)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_9.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_9.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_25;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_25;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_25;
				}
			}
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
		}
		if (null == owner_Connection_23 || !m_RegisteredForEvents)
		{
			owner_Connection_23 = parentGameObject;
		}
		if (null == owner_Connection_32 || !m_RegisteredForEvents)
		{
			owner_Connection_32 = parentGameObject;
		}
		if (null == owner_Connection_55 || !m_RegisteredForEvents)
		{
			owner_Connection_55 = parentGameObject;
		}
		if (null == owner_Connection_68 || !m_RegisteredForEvents)
		{
			owner_Connection_68 = parentGameObject;
		}
		if (null == owner_Connection_122 || !m_RegisteredForEvents)
		{
			owner_Connection_122 = parentGameObject;
		}
		if (null == owner_Connection_123 || !m_RegisteredForEvents)
		{
			owner_Connection_123 = parentGameObject;
		}
		if (null == owner_Connection_149 || !m_RegisteredForEvents)
		{
			owner_Connection_149 = parentGameObject;
		}
		if (null == owner_Connection_154 || !m_RegisteredForEvents)
		{
			owner_Connection_154 = parentGameObject;
		}
		if (null == owner_Connection_177 || !m_RegisteredForEvents)
		{
			owner_Connection_177 = parentGameObject;
		}
		if (null == owner_Connection_182 || !m_RegisteredForEvents)
		{
			owner_Connection_182 = parentGameObject;
		}
		if (null == owner_Connection_205 || !m_RegisteredForEvents)
		{
			owner_Connection_205 = parentGameObject;
		}
		if (null == owner_Connection_231 || !m_RegisteredForEvents)
		{
			owner_Connection_231 = parentGameObject;
		}
		if (null == owner_Connection_237 || !m_RegisteredForEvents)
		{
			owner_Connection_237 = parentGameObject;
		}
		if (null == owner_Connection_257 || !m_RegisteredForEvents)
		{
			owner_Connection_257 = parentGameObject;
		}
		if (null == owner_Connection_318 || !m_RegisteredForEvents)
		{
			owner_Connection_318 = parentGameObject;
		}
		if (null == owner_Connection_342 || !m_RegisteredForEvents)
		{
			owner_Connection_342 = parentGameObject;
		}
		if (null == owner_Connection_347 || !m_RegisteredForEvents)
		{
			owner_Connection_347 = parentGameObject;
		}
		if (null == owner_Connection_371 || !m_RegisteredForEvents)
		{
			owner_Connection_371 = parentGameObject;
		}
		if (null == owner_Connection_373 || !m_RegisteredForEvents)
		{
			owner_Connection_373 = parentGameObject;
		}
		if (null == owner_Connection_397 || !m_RegisteredForEvents)
		{
			owner_Connection_397 = parentGameObject;
		}
		if (null == owner_Connection_455 || !m_RegisteredForEvents)
		{
			owner_Connection_455 = parentGameObject;
		}
		if (null == owner_Connection_458 || !m_RegisteredForEvents)
		{
			owner_Connection_458 = parentGameObject;
		}
		if (null == owner_Connection_463 || !m_RegisteredForEvents)
		{
			owner_Connection_463 = parentGameObject;
		}
		if (null == owner_Connection_465 || !m_RegisteredForEvents)
		{
			owner_Connection_465 = parentGameObject;
		}
		if (null == owner_Connection_473 || !m_RegisteredForEvents)
		{
			owner_Connection_473 = parentGameObject;
		}
		if (null == owner_Connection_480 || !m_RegisteredForEvents)
		{
			owner_Connection_480 = parentGameObject;
		}
		if (null == owner_Connection_483 || !m_RegisteredForEvents)
		{
			owner_Connection_483 = parentGameObject;
		}
		if (null == owner_Connection_485 || !m_RegisteredForEvents)
		{
			owner_Connection_485 = parentGameObject;
		}
		if (null == owner_Connection_493 || !m_RegisteredForEvents)
		{
			owner_Connection_493 = parentGameObject;
		}
		if (null == owner_Connection_502 || !m_RegisteredForEvents)
		{
			owner_Connection_502 = parentGameObject;
		}
		if (null == owner_Connection_503 || !m_RegisteredForEvents)
		{
			owner_Connection_503 = parentGameObject;
		}
		if (null == owner_Connection_504 || !m_RegisteredForEvents)
		{
			owner_Connection_504 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_0)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_0.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_9)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_9.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_9.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_25;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_25;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_25;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_0)
		{
			uScript_EncounterUpdate component = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_1;
				component.OnSuspend -= Instance_OnSuspend_1;
				component.OnResume -= Instance_OnResume_1;
			}
		}
		if (null != owner_Connection_9)
		{
			uScript_SaveLoad component2 = owner_Connection_9.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_25;
				component2.LoadEvent -= Instance_LoadEvent_25;
				component2.RestartEvent -= Instance_RestartEvent_25;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_4.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_20.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_22.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_24.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_31.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_35.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_51.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_54.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_57.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_62.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_69.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_99.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_100.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_105.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_106.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_109.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_114.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_115.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_147.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_151.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_153.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_155.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_157.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_159.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_165.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_169.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_170.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_174.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_175.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_178.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_179.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_183.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_184.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_195.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_201.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_202.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_209.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_210.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_216.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_226.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_229.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_234.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_235.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_236.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_238.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_240.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_241.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_243.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_245.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_246.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_247.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_250.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_251.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_252.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_253.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_258.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_259.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_262.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_263.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_264.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_269.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_270.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_273.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_276.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_279.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_281.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_282.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_285.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_289.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_291.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_296.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_298.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_301.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_303.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_307.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_308.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_309.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_311.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_313.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315.SetParent(g);
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_319.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_320.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_321.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_323.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_324.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_327.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_333.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_334.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_338.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_340.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_341.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_348.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_350.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_363.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_365.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_366.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_370.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_374.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_377.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_378.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_381.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_383.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_385.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_387.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_389.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_392.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_394.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_396.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_399.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_401.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_403.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_405.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_406.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_409.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_410.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_412.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_413.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_415.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_418.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_419.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_420.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_421.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_422.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_436.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_438.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_440.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_442.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_446.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_448.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_450.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_451.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_452.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_456.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_460.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_461.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_462.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_467.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_469.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_470.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_471.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_476.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_477.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_479.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_482.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_489.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_490.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_491.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_494.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_495.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_499.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_501.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_506.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_507.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_511.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_512.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_513.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_515.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_518.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_23 = parentGameObject;
		owner_Connection_32 = parentGameObject;
		owner_Connection_55 = parentGameObject;
		owner_Connection_68 = parentGameObject;
		owner_Connection_122 = parentGameObject;
		owner_Connection_123 = parentGameObject;
		owner_Connection_149 = parentGameObject;
		owner_Connection_154 = parentGameObject;
		owner_Connection_177 = parentGameObject;
		owner_Connection_182 = parentGameObject;
		owner_Connection_205 = parentGameObject;
		owner_Connection_231 = parentGameObject;
		owner_Connection_237 = parentGameObject;
		owner_Connection_257 = parentGameObject;
		owner_Connection_318 = parentGameObject;
		owner_Connection_342 = parentGameObject;
		owner_Connection_347 = parentGameObject;
		owner_Connection_371 = parentGameObject;
		owner_Connection_373 = parentGameObject;
		owner_Connection_397 = parentGameObject;
		owner_Connection_455 = parentGameObject;
		owner_Connection_458 = parentGameObject;
		owner_Connection_463 = parentGameObject;
		owner_Connection_465 = parentGameObject;
		owner_Connection_473 = parentGameObject;
		owner_Connection_480 = parentGameObject;
		owner_Connection_483 = parentGameObject;
		owner_Connection_485 = parentGameObject;
		owner_Connection_493 = parentGameObject;
		owner_Connection_502 = parentGameObject;
		owner_Connection_503 = parentGameObject;
		owner_Connection_504 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Save_Out += SubGraph_SaveLoadBool_Save_Out_43;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Load_Out += SubGraph_SaveLoadBool_Load_Out_43;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_43;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Save_Out += SubGraph_SaveLoadBool_Save_Out_77;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Load_Out += SubGraph_SaveLoadBool_Load_Out_77;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_77;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Save_Out += SubGraph_SaveLoadBool_Save_Out_78;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Load_Out += SubGraph_SaveLoadBool_Load_Out_78;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_78;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Save_Out += SubGraph_SaveLoadBool_Save_Out_79;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Load_Out += SubGraph_SaveLoadBool_Load_Out_79;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_79;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Save_Out += SubGraph_SaveLoadBool_Save_Out_80;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Load_Out += SubGraph_SaveLoadBool_Load_Out_80;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_80;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Save_Out += SubGraph_SaveLoadBool_Save_Out_81;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Load_Out += SubGraph_SaveLoadBool_Load_Out_81;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_81;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Save_Out += SubGraph_SaveLoadBool_Save_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Load_Out += SubGraph_SaveLoadBool_Load_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Save_Out += SubGraph_SaveLoadBool_Save_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Load_Out += SubGraph_SaveLoadBool_Load_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save_Out += SubGraph_SaveLoadBool_Save_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load_Out += SubGraph_SaveLoadBool_Load_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Save_Out += SubGraph_SaveLoadBool_Save_Out_127;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Load_Out += SubGraph_SaveLoadBool_Load_Out_127;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_127;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Save_Out += SubGraph_SaveLoadBool_Save_Out_128;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Load_Out += SubGraph_SaveLoadBool_Load_Out_128;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_128;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Save_Out += SubGraph_SaveLoadBool_Save_Out_130;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Load_Out += SubGraph_SaveLoadBool_Load_Out_130;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_130;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Save_Out += SubGraph_SaveLoadBool_Save_Out_133;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Load_Out += SubGraph_SaveLoadBool_Load_Out_133;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_133;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Save_Out += SubGraph_SaveLoadBool_Save_Out_134;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Load_Out += SubGraph_SaveLoadBool_Load_Out_134;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_134;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Save_Out += SubGraph_SaveLoadBool_Save_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Load_Out += SubGraph_SaveLoadBool_Load_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Save_Out += SubGraph_SaveLoadBool_Save_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Load_Out += SubGraph_SaveLoadBool_Load_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Save_Out += SubGraph_SaveLoadBool_Save_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Load_Out += SubGraph_SaveLoadBool_Load_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Save_Out += SubGraph_SaveLoadBool_Save_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Load_Out += SubGraph_SaveLoadBool_Load_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Save_Out += SubGraph_SaveLoadBool_Save_Out_145;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Load_Out += SubGraph_SaveLoadBool_Load_Out_145;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_145;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Save_Out += SubGraph_SaveLoadBool_Save_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Load_Out += SubGraph_SaveLoadBool_Load_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Save_Out += SubGraph_SaveLoadBool_Save_Out_261;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Load_Out += SubGraph_SaveLoadBool_Load_Out_261;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_261;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Save_Out += SubGraph_SaveLoadBool_Save_Out_346;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Load_Out += SubGraph_SaveLoadBool_Load_Out_346;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_346;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Save_Out += SubGraph_SaveLoadBool_Save_Out_355;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Load_Out += SubGraph_SaveLoadBool_Load_Out_355;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_355;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Save_Out += SubGraph_SaveLoadBool_Save_Out_356;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Load_Out += SubGraph_SaveLoadBool_Load_Out_356;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_356;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Save_Out += SubGraph_SaveLoadBool_Save_Out_357;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Load_Out += SubGraph_SaveLoadBool_Load_Out_357;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_357;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Save_Out += SubGraph_SaveLoadBool_Save_Out_427;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Load_Out += SubGraph_SaveLoadBool_Load_Out_427;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_427;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Save_Out += SubGraph_SaveLoadBool_Save_Out_429;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Load_Out += SubGraph_SaveLoadBool_Load_Out_429;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_429;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Save_Out += SubGraph_SaveLoadBool_Save_Out_431;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Load_Out += SubGraph_SaveLoadBool_Load_Out_431;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_431;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Save_Out += SubGraph_SaveLoadBool_Save_Out_432;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Load_Out += SubGraph_SaveLoadBool_Load_Out_432;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_432;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.Save_Out += SubGraph_SaveLoadInt_Save_Out_434;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.Load_Out += SubGraph_SaveLoadInt_Load_Out_434;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_434;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Save_Out += SubGraph_SaveLoadBool_Save_Out_445;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Load_Out += SubGraph_SaveLoadBool_Load_Out_445;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_445;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_20.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_22.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_24.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_57.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_62.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_69.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_99.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_100.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_114.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_153.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_155.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_195.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_238.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_241.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_243.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_245.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_253.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_311.OnDisable();
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_319.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_321.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_323.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_392.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_396.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Save_Out -= SubGraph_SaveLoadBool_Save_Out_43;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Load_Out -= SubGraph_SaveLoadBool_Load_Out_43;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_43;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Save_Out -= SubGraph_SaveLoadBool_Save_Out_77;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Load_Out -= SubGraph_SaveLoadBool_Load_Out_77;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_77;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Save_Out -= SubGraph_SaveLoadBool_Save_Out_78;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Load_Out -= SubGraph_SaveLoadBool_Load_Out_78;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_78;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Save_Out -= SubGraph_SaveLoadBool_Save_Out_79;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Load_Out -= SubGraph_SaveLoadBool_Load_Out_79;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_79;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Save_Out -= SubGraph_SaveLoadBool_Save_Out_80;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Load_Out -= SubGraph_SaveLoadBool_Load_Out_80;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_80;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Save_Out -= SubGraph_SaveLoadBool_Save_Out_81;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Load_Out -= SubGraph_SaveLoadBool_Load_Out_81;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_81;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Save_Out -= SubGraph_SaveLoadBool_Save_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Load_Out -= SubGraph_SaveLoadBool_Load_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Save_Out -= SubGraph_SaveLoadBool_Save_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Load_Out -= SubGraph_SaveLoadBool_Load_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save_Out -= SubGraph_SaveLoadBool_Save_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load_Out -= SubGraph_SaveLoadBool_Load_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Save_Out -= SubGraph_SaveLoadBool_Save_Out_127;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Load_Out -= SubGraph_SaveLoadBool_Load_Out_127;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_127;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Save_Out -= SubGraph_SaveLoadBool_Save_Out_128;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Load_Out -= SubGraph_SaveLoadBool_Load_Out_128;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_128;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Save_Out -= SubGraph_SaveLoadBool_Save_Out_130;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Load_Out -= SubGraph_SaveLoadBool_Load_Out_130;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_130;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Save_Out -= SubGraph_SaveLoadBool_Save_Out_133;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Load_Out -= SubGraph_SaveLoadBool_Load_Out_133;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_133;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Save_Out -= SubGraph_SaveLoadBool_Save_Out_134;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Load_Out -= SubGraph_SaveLoadBool_Load_Out_134;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_134;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Save_Out -= SubGraph_SaveLoadBool_Save_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Load_Out -= SubGraph_SaveLoadBool_Load_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Save_Out -= SubGraph_SaveLoadBool_Save_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Load_Out -= SubGraph_SaveLoadBool_Load_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Save_Out -= SubGraph_SaveLoadBool_Save_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Load_Out -= SubGraph_SaveLoadBool_Load_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Save_Out -= SubGraph_SaveLoadBool_Save_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Load_Out -= SubGraph_SaveLoadBool_Load_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Save_Out -= SubGraph_SaveLoadBool_Save_Out_145;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Load_Out -= SubGraph_SaveLoadBool_Load_Out_145;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_145;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Save_Out -= SubGraph_SaveLoadBool_Save_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Load_Out -= SubGraph_SaveLoadBool_Load_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Save_Out -= SubGraph_SaveLoadBool_Save_Out_261;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Load_Out -= SubGraph_SaveLoadBool_Load_Out_261;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_261;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Save_Out -= SubGraph_SaveLoadBool_Save_Out_346;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Load_Out -= SubGraph_SaveLoadBool_Load_Out_346;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_346;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Save_Out -= SubGraph_SaveLoadBool_Save_Out_355;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Load_Out -= SubGraph_SaveLoadBool_Load_Out_355;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_355;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Save_Out -= SubGraph_SaveLoadBool_Save_Out_356;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Load_Out -= SubGraph_SaveLoadBool_Load_Out_356;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_356;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Save_Out -= SubGraph_SaveLoadBool_Save_Out_357;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Load_Out -= SubGraph_SaveLoadBool_Load_Out_357;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_357;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Save_Out -= SubGraph_SaveLoadBool_Save_Out_427;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Load_Out -= SubGraph_SaveLoadBool_Load_Out_427;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_427;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Save_Out -= SubGraph_SaveLoadBool_Save_Out_429;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Load_Out -= SubGraph_SaveLoadBool_Load_Out_429;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_429;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Save_Out -= SubGraph_SaveLoadBool_Save_Out_431;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Load_Out -= SubGraph_SaveLoadBool_Load_Out_431;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_431;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Save_Out -= SubGraph_SaveLoadBool_Save_Out_432;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Load_Out -= SubGraph_SaveLoadBool_Load_Out_432;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_432;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.Save_Out -= SubGraph_SaveLoadInt_Save_Out_434;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.Load_Out -= SubGraph_SaveLoadInt_Load_Out_434;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_434;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Save_Out -= SubGraph_SaveLoadBool_Save_Out_445;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Load_Out -= SubGraph_SaveLoadBool_Load_Out_445;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_445;
	}

	private void Instance_OnUpdate_1(object o, EventArgs e)
	{
		Relay_OnUpdate_1();
	}

	private void Instance_OnSuspend_1(object o, EventArgs e)
	{
		Relay_OnSuspend_1();
	}

	private void Instance_OnResume_1(object o, EventArgs e)
	{
		Relay_OnResume_1();
	}

	private void Instance_SaveEvent_25(object o, EventArgs e)
	{
		Relay_SaveEvent_25();
	}

	private void Instance_LoadEvent_25(object o, EventArgs e)
	{
		Relay_LoadEvent_25();
	}

	private void Instance_RestartEvent_25(object o, EventArgs e)
	{
		Relay_RestartEvent_25();
	}

	private void SubGraph_SaveLoadBool_Save_Out_43(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_43 = e.boolean;
		local_EnemySpottedBoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_43;
		Relay_Save_Out_43();
	}

	private void SubGraph_SaveLoadBool_Load_Out_43(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_43 = e.boolean;
		local_EnemySpottedBoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_43;
		Relay_Load_Out_43();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_43(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_43 = e.boolean;
		local_EnemySpottedBoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_43;
		Relay_Restart_Out_43();
	}

	private void SubGraph_SaveLoadBool_Save_Out_77(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = e.boolean;
		local_TriggerHit4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_77;
		Relay_Save_Out_77();
	}

	private void SubGraph_SaveLoadBool_Load_Out_77(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = e.boolean;
		local_TriggerHit4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_77;
		Relay_Load_Out_77();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_77(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = e.boolean;
		local_TriggerHit4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_77;
		Relay_Restart_Out_77();
	}

	private void SubGraph_SaveLoadBool_Save_Out_78(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_78 = e.boolean;
		local_TriggerHit5or6_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_78;
		Relay_Save_Out_78();
	}

	private void SubGraph_SaveLoadBool_Load_Out_78(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_78 = e.boolean;
		local_TriggerHit5or6_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_78;
		Relay_Load_Out_78();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_78(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_78 = e.boolean;
		local_TriggerHit5or6_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_78;
		Relay_Restart_Out_78();
	}

	private void SubGraph_SaveLoadBool_Save_Out_79(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_79 = e.boolean;
		local_TriggerHit3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_79;
		Relay_Save_Out_79();
	}

	private void SubGraph_SaveLoadBool_Load_Out_79(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_79 = e.boolean;
		local_TriggerHit3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_79;
		Relay_Load_Out_79();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_79(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_79 = e.boolean;
		local_TriggerHit3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_79;
		Relay_Restart_Out_79();
	}

	private void SubGraph_SaveLoadBool_Save_Out_80(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_80 = e.boolean;
		local_TriggerHit2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_80;
		Relay_Save_Out_80();
	}

	private void SubGraph_SaveLoadBool_Load_Out_80(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_80 = e.boolean;
		local_TriggerHit2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_80;
		Relay_Load_Out_80();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_80(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_80 = e.boolean;
		local_TriggerHit2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_80;
		Relay_Restart_Out_80();
	}

	private void SubGraph_SaveLoadBool_Save_Out_81(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = e.boolean;
		local_TriggerHit1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_81;
		Relay_Save_Out_81();
	}

	private void SubGraph_SaveLoadBool_Load_Out_81(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = e.boolean;
		local_TriggerHit1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_81;
		Relay_Load_Out_81();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_81(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = e.boolean;
		local_TriggerHit1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_81;
		Relay_Restart_Out_81();
	}

	private void SubGraph_SaveLoadBool_Save_Out_85(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = e.boolean;
		local_EnemySpottedF_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_85;
		Relay_Save_Out_85();
	}

	private void SubGraph_SaveLoadBool_Load_Out_85(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = e.boolean;
		local_EnemySpottedF_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_85;
		Relay_Load_Out_85();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_85(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = e.boolean;
		local_EnemySpottedF_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_85;
		Relay_Restart_Out_85();
	}

	private void SubGraph_SaveLoadBool_Save_Out_92(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = e.boolean;
		local_TechsDeadF_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_92;
		Relay_Save_Out_92();
	}

	private void SubGraph_SaveLoadBool_Load_Out_92(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = e.boolean;
		local_TechsDeadF_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_92;
		Relay_Load_Out_92();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_92(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = e.boolean;
		local_TechsDeadF_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_92;
		Relay_Restart_Out_92();
	}

	private void SubGraph_SaveLoadBool_Save_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Save_Out_93();
	}

	private void SubGraph_SaveLoadBool_Load_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Load_Out_93();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Restart_Out_93();
	}

	private void SubGraph_SaveLoadBool_Save_Out_127(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_127 = e.boolean;
		local_EnemySpottedE_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_127;
		Relay_Save_Out_127();
	}

	private void SubGraph_SaveLoadBool_Load_Out_127(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_127 = e.boolean;
		local_EnemySpottedE_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_127;
		Relay_Load_Out_127();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_127(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_127 = e.boolean;
		local_EnemySpottedE_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_127;
		Relay_Restart_Out_127();
	}

	private void SubGraph_SaveLoadBool_Save_Out_128(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = e.boolean;
		local_TechsDeadE_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_128;
		Relay_Save_Out_128();
	}

	private void SubGraph_SaveLoadBool_Load_Out_128(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = e.boolean;
		local_TechsDeadE_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_128;
		Relay_Load_Out_128();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_128(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = e.boolean;
		local_TechsDeadE_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_128;
		Relay_Restart_Out_128();
	}

	private void SubGraph_SaveLoadBool_Save_Out_130(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_130 = e.boolean;
		local_EnemySpottedD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_130;
		Relay_Save_Out_130();
	}

	private void SubGraph_SaveLoadBool_Load_Out_130(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_130 = e.boolean;
		local_EnemySpottedD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_130;
		Relay_Load_Out_130();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_130(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_130 = e.boolean;
		local_EnemySpottedD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_130;
		Relay_Restart_Out_130();
	}

	private void SubGraph_SaveLoadBool_Save_Out_133(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_133 = e.boolean;
		local_TechsDeadD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_133;
		Relay_Save_Out_133();
	}

	private void SubGraph_SaveLoadBool_Load_Out_133(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_133 = e.boolean;
		local_TechsDeadD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_133;
		Relay_Load_Out_133();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_133(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_133 = e.boolean;
		local_TechsDeadD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_133;
		Relay_Restart_Out_133();
	}

	private void SubGraph_SaveLoadBool_Save_Out_134(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = e.boolean;
		local_EnemySpottedC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_134;
		Relay_Save_Out_134();
	}

	private void SubGraph_SaveLoadBool_Load_Out_134(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = e.boolean;
		local_EnemySpottedC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_134;
		Relay_Load_Out_134();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_134(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = e.boolean;
		local_EnemySpottedC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_134;
		Relay_Restart_Out_134();
	}

	private void SubGraph_SaveLoadBool_Save_Out_137(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = e.boolean;
		local_TechsDeadC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_137;
		Relay_Save_Out_137();
	}

	private void SubGraph_SaveLoadBool_Load_Out_137(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = e.boolean;
		local_TechsDeadC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_137;
		Relay_Load_Out_137();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_137(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = e.boolean;
		local_TechsDeadC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_137;
		Relay_Restart_Out_137();
	}

	private void SubGraph_SaveLoadBool_Save_Out_139(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = e.boolean;
		local_EnemySpottedB_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_139;
		Relay_Save_Out_139();
	}

	private void SubGraph_SaveLoadBool_Load_Out_139(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = e.boolean;
		local_EnemySpottedB_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_139;
		Relay_Load_Out_139();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_139(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = e.boolean;
		local_EnemySpottedB_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_139;
		Relay_Restart_Out_139();
	}

	private void SubGraph_SaveLoadBool_Save_Out_140(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = e.boolean;
		local_TechsDeadB_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_140;
		Relay_Save_Out_140();
	}

	private void SubGraph_SaveLoadBool_Load_Out_140(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = e.boolean;
		local_TechsDeadB_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_140;
		Relay_Load_Out_140();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_140(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = e.boolean;
		local_TechsDeadB_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_140;
		Relay_Restart_Out_140();
	}

	private void SubGraph_SaveLoadBool_Save_Out_143(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = e.boolean;
		local_EnemySpottedA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_143;
		Relay_Save_Out_143();
	}

	private void SubGraph_SaveLoadBool_Load_Out_143(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = e.boolean;
		local_EnemySpottedA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_143;
		Relay_Load_Out_143();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_143(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = e.boolean;
		local_EnemySpottedA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_143;
		Relay_Restart_Out_143();
	}

	private void SubGraph_SaveLoadBool_Save_Out_145(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_145 = e.boolean;
		local_TechsDeadA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_145;
		Relay_Save_Out_145();
	}

	private void SubGraph_SaveLoadBool_Load_Out_145(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_145 = e.boolean;
		local_TechsDeadA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_145;
		Relay_Load_Out_145();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_145(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_145 = e.boolean;
		local_TechsDeadA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_145;
		Relay_Restart_Out_145();
	}

	private void SubGraph_SaveLoadBool_Save_Out_193(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = e.boolean;
		local_TechsDeadCandD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_193;
		Relay_Save_Out_193();
	}

	private void SubGraph_SaveLoadBool_Load_Out_193(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = e.boolean;
		local_TechsDeadCandD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_193;
		Relay_Load_Out_193();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_193(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = e.boolean;
		local_TechsDeadCandD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_193;
		Relay_Restart_Out_193();
	}

	private void SubGraph_SaveLoadBool_Save_Out_261(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = e.boolean;
		local_TechsDeadBandA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_261;
		Relay_Save_Out_261();
	}

	private void SubGraph_SaveLoadBool_Load_Out_261(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = e.boolean;
		local_TechsDeadBandA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_261;
		Relay_Load_Out_261();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_261(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = e.boolean;
		local_TechsDeadBandA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_261;
		Relay_Restart_Out_261();
	}

	private void SubGraph_SaveLoadBool_Save_Out_346(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_346 = e.boolean;
		local_NPCTechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_346;
		Relay_Save_Out_346();
	}

	private void SubGraph_SaveLoadBool_Load_Out_346(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_346 = e.boolean;
		local_NPCTechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_346;
		Relay_Load_Out_346();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_346(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_346 = e.boolean;
		local_NPCTechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_346;
		Relay_Restart_Out_346();
	}

	private void SubGraph_SaveLoadBool_Save_Out_355(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_355;
		Relay_Save_Out_355();
	}

	private void SubGraph_SaveLoadBool_Load_Out_355(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_355;
		Relay_Load_Out_355();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_355(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_355;
		Relay_Restart_Out_355();
	}

	private void SubGraph_SaveLoadBool_Save_Out_356(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_356;
		Relay_Save_Out_356();
	}

	private void SubGraph_SaveLoadBool_Load_Out_356(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_356;
		Relay_Load_Out_356();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_356(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_356;
		Relay_Restart_Out_356();
	}

	private void SubGraph_SaveLoadBool_Save_Out_357(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_357;
		Relay_Save_Out_357();
	}

	private void SubGraph_SaveLoadBool_Load_Out_357(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_357;
		Relay_Load_Out_357();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_357(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_357;
		Relay_Restart_Out_357();
	}

	private void SubGraph_SaveLoadBool_Save_Out_427(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_427 = e.boolean;
		local_TriggerHitA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_427;
		Relay_Save_Out_427();
	}

	private void SubGraph_SaveLoadBool_Load_Out_427(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_427 = e.boolean;
		local_TriggerHitA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_427;
		Relay_Load_Out_427();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_427(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_427 = e.boolean;
		local_TriggerHitA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_427;
		Relay_Restart_Out_427();
	}

	private void SubGraph_SaveLoadBool_Save_Out_429(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_429 = e.boolean;
		local_TriggerHitB_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_429;
		Relay_Save_Out_429();
	}

	private void SubGraph_SaveLoadBool_Load_Out_429(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_429 = e.boolean;
		local_TriggerHitB_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_429;
		Relay_Load_Out_429();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_429(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_429 = e.boolean;
		local_TriggerHitB_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_429;
		Relay_Restart_Out_429();
	}

	private void SubGraph_SaveLoadBool_Save_Out_431(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = e.boolean;
		local_TriggerHitC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_431;
		Relay_Save_Out_431();
	}

	private void SubGraph_SaveLoadBool_Load_Out_431(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = e.boolean;
		local_TriggerHitC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_431;
		Relay_Load_Out_431();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_431(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = e.boolean;
		local_TriggerHitC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_431;
		Relay_Restart_Out_431();
	}

	private void SubGraph_SaveLoadBool_Save_Out_432(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_432 = e.boolean;
		local_TriggerHitD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_432;
		Relay_Save_Out_432();
	}

	private void SubGraph_SaveLoadBool_Load_Out_432(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_432 = e.boolean;
		local_TriggerHitD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_432;
		Relay_Load_Out_432();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_432(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_432 = e.boolean;
		local_TriggerHitD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_432;
		Relay_Restart_Out_432();
	}

	private void SubGraph_SaveLoadInt_Save_Out_434(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_434 = e.integer;
		local_ObjectiveStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_434;
		Relay_Save_Out_434();
	}

	private void SubGraph_SaveLoadInt_Load_Out_434(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_434 = e.integer;
		local_ObjectiveStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_434;
		Relay_Load_Out_434();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_434(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_434 = e.integer;
		local_ObjectiveStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_434;
		Relay_Restart_Out_434();
	}

	private void SubGraph_SaveLoadBool_Save_Out_445(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = e.boolean;
		local_BossTechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_445;
		Relay_Save_Out_445();
	}

	private void SubGraph_SaveLoadBool_Load_Out_445(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = e.boolean;
		local_BossTechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_445;
		Relay_Load_Out_445();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_445(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = e.boolean;
		local_BossTechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_445;
		Relay_Restart_Out_445();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_17();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Succeed_4()
	{
		logic_uScript_FinishEncounter_owner_4 = owner_Connection_5;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_4.Succeed(logic_uScript_FinishEncounter_owner_4);
	}

	private void Relay_Fail_4()
	{
		logic_uScript_FinishEncounter_owner_4 = owner_Connection_5;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_4.Fail(logic_uScript_FinishEncounter_owner_4);
	}

	private void Relay_In_12()
	{
		logic_uScriptCon_CompareBool_Bool_12 = local_EnemySpottedBoss_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.In(logic_uScriptCon_CompareBool_Bool_12);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.False;
		if (num)
		{
			Relay_In_461();
		}
		if (flag)
		{
			Relay_In_263();
		}
	}

	private void Relay_In_15()
	{
		int num = 0;
		Array array = msgEnemySpottedBoss;
		if (logic_uScript_AddOnScreenMessage_locString_15.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_15, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_15, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_15 = local_368_System_String;
		logic_uScript_AddOnScreenMessage_speaker_15 = messageSpeakerBoss;
		logic_uScript_AddOnScreenMessage_Return_15 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15.In(logic_uScript_AddOnScreenMessage_locString_15, logic_uScript_AddOnScreenMessage_msgPriority_15, logic_uScript_AddOnScreenMessage_holdMsg_15, logic_uScript_AddOnScreenMessage_tag_15, logic_uScript_AddOnScreenMessage_speaker_15, logic_uScript_AddOnScreenMessage_side_15);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15.Shown)
		{
			Relay_In_350();
		}
	}

	private void Relay_In_17()
	{
		logic_uScriptCon_CompareBool_Bool_17 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.In(logic_uScriptCon_CompareBool_Bool_17);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.False;
		if (num)
		{
			Relay_Succeed_4();
		}
		if (flag)
		{
			Relay_In_338();
		}
	}

	private void Relay_In_18()
	{
		logic_uScriptCon_CompareBool_Bool_18 = local_TriggerHit5or6_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.In(logic_uScriptCon_CompareBool_Bool_18);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.False;
		if (num)
		{
			Relay_In_440();
		}
		if (flag)
		{
			Relay_In_446();
		}
	}

	private void Relay_In_20()
	{
		int num = 0;
		Array array = msgMissionCompleteNPC;
		if (logic_uScript_AddOnScreenMessage_locString_20.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_20, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_20, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_20 = messageSpeakerNPC;
		logic_uScript_AddOnScreenMessage_Return_20 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_20.In(logic_uScript_AddOnScreenMessage_locString_20, logic_uScript_AddOnScreenMessage_msgPriority_20, logic_uScript_AddOnScreenMessage_holdMsg_20, logic_uScript_AddOnScreenMessage_tag_20, logic_uScript_AddOnScreenMessage_speaker_20, logic_uScript_AddOnScreenMessage_side_20);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_20.Shown)
		{
			Relay_True_27();
		}
	}

	private void Relay_In_21()
	{
		int num = 0;
		Array array = enemyTechDataBoss;
		if (logic_uScript_GetAndCheckTechs_techData_21.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_21, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_21, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_21 = owner_Connection_23;
		int num2 = 0;
		Array array2 = local_EnemyTechsBoss_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_21.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_21, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_21, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_21 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.In(logic_uScript_GetAndCheckTechs_techData_21, logic_uScript_GetAndCheckTechs_ownerNode_21, ref logic_uScript_GetAndCheckTechs_techs_21);
		local_EnemyTechsBoss_TankArray = logic_uScript_GetAndCheckTechs_techs_21;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_374();
		}
		if (someAlive)
		{
			Relay_In_374();
		}
		if (allDead)
		{
			Relay_In_262();
		}
		if (waitingToSpawn)
		{
			Relay_In_450();
		}
	}

	private void Relay_In_22()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_22 = owner_Connection_14;
		int num = 0;
		Array array = local_EnemyTechsBoss_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_22.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_22, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_22, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_22 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_22.In(logic_uScript_SetOneTechAsEncounterTarget_owner_22, logic_uScript_SetOneTechAsEncounterTarget_techs_22);
		local_BossTech_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_22;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_22.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_24()
	{
		int num = 0;
		Array array = local_EnemyTechsBoss_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_24.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_24, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_24, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_range_24 = distEnemiesSpottedBoss;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_24.In(logic_uScript_InRangeOfAtLeastOneTech_techs_24, logic_uScript_InRangeOfAtLeastOneTech_range_24);
		bool inRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_24.InRange;
		bool outOfRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_24.OutOfRange;
		if (inRange)
		{
			Relay_In_12();
		}
		if (outOfRange)
		{
			Relay_In_461();
		}
	}

	private void Relay_SaveEvent_25()
	{
		Relay_Save_43();
	}

	private void Relay_LoadEvent_25()
	{
		Relay_Load_43();
	}

	private void Relay_RestartEvent_25()
	{
		Relay_Set_False_43();
	}

	private void Relay_True_26()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.True(out logic_uScriptAct_SetBool_Target_26);
		local_TriggerHit5or6_System_Boolean = logic_uScriptAct_SetBool_Target_26;
	}

	private void Relay_False_26()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.False(out logic_uScriptAct_SetBool_Target_26);
		local_TriggerHit5or6_System_Boolean = logic_uScriptAct_SetBool_Target_26;
	}

	private void Relay_True_27()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.True(out logic_uScriptAct_SetBool_Target_27);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_27;
	}

	private void Relay_False_27()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.False(out logic_uScriptAct_SetBool_Target_27);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_27;
	}

	private void Relay_In_29()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_29 = Trigger5;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_29);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29.InRange;
		if (num)
		{
			Relay_In_35();
		}
		if (inRange)
		{
			Relay_In_45();
		}
	}

	private void Relay_InitialSpawn_31()
	{
		int num = 0;
		Array array = enemyTechDataBoss;
		if (logic_uScript_SpawnTechsFromData_spawnData_31.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_31, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_31, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_31 = owner_Connection_32;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_31.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_31, logic_uScript_SpawnTechsFromData_ownerNode_31, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_31);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_31.Out)
		{
			Relay_InitialSpawn_370();
		}
	}

	private void Relay_In_35()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_35 = Trigger6;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_35.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_35);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_35.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_35.InRange;
		if (num)
		{
			Relay_In_49();
		}
		if (inRange)
		{
			Relay_In_490();
		}
	}

	private void Relay_True_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.True(out logic_uScriptAct_SetBool_Target_40);
		local_EnemySpottedBoss_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_False_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.False(out logic_uScriptAct_SetBool_Target_40);
		local_EnemySpottedBoss_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_Save_Out_43()
	{
		Relay_Save_85();
	}

	private void Relay_Load_Out_43()
	{
		Relay_Load_85();
	}

	private void Relay_Restart_Out_43()
	{
		Relay_Set_False_85();
	}

	private void Relay_Save_43()
	{
		logic_SubGraph_SaveLoadBool_boolean_43 = local_EnemySpottedBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_43 = local_EnemySpottedBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Save(ref logic_SubGraph_SaveLoadBool_boolean_43, logic_SubGraph_SaveLoadBool_boolAsVariable_43, logic_SubGraph_SaveLoadBool_uniqueID_43);
	}

	private void Relay_Load_43()
	{
		logic_SubGraph_SaveLoadBool_boolean_43 = local_EnemySpottedBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_43 = local_EnemySpottedBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Load(ref logic_SubGraph_SaveLoadBool_boolean_43, logic_SubGraph_SaveLoadBool_boolAsVariable_43, logic_SubGraph_SaveLoadBool_uniqueID_43);
	}

	private void Relay_Set_True_43()
	{
		logic_SubGraph_SaveLoadBool_boolean_43 = local_EnemySpottedBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_43 = local_EnemySpottedBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_43, logic_SubGraph_SaveLoadBool_boolAsVariable_43, logic_SubGraph_SaveLoadBool_uniqueID_43);
	}

	private void Relay_Set_False_43()
	{
		logic_SubGraph_SaveLoadBool_boolean_43 = local_EnemySpottedBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_43 = local_EnemySpottedBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_43.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_43, logic_SubGraph_SaveLoadBool_boolAsVariable_43, logic_SubGraph_SaveLoadBool_uniqueID_43);
	}

	private void Relay_In_45()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45.Out)
		{
			Relay_In_490();
		}
	}

	private void Relay_In_47()
	{
		logic_uScriptCon_CompareBool_Bool_47 = local_TriggerHit4_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.In(logic_uScriptCon_CompareBool_Bool_47);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.False;
		if (num)
		{
			Relay_In_58();
		}
		if (flag)
		{
			Relay_In_51();
		}
	}

	private void Relay_In_49()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_True_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.True(out logic_uScriptAct_SetBool_Target_50);
		local_TriggerHit4_System_Boolean = logic_uScriptAct_SetBool_Target_50;
	}

	private void Relay_False_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.False(out logic_uScriptAct_SetBool_Target_50);
		local_TriggerHit4_System_Boolean = logic_uScriptAct_SetBool_Target_50;
	}

	private void Relay_In_51()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_51 = Trigger4;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_51.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_51);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_51.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_51.OutOfRange;
		if (inRange)
		{
			Relay_InitialSpawn_54();
		}
		if (outOfRange)
		{
			Relay_In_264();
		}
	}

	private void Relay_InitialSpawn_54()
	{
		int num = 0;
		Array array = enemyTechDataF;
		if (logic_uScript_SpawnTechsFromData_spawnData_54.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_54, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_54, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_54 = owner_Connection_55;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_54.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_54, logic_uScript_SpawnTechsFromData_ownerNode_54, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_54);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_54.Out)
		{
			Relay_True_50();
		}
	}

	private void Relay_In_57()
	{
		int num = 0;
		Array array = msgTechsDeadF;
		if (logic_uScript_AddOnScreenMessage_locString_57.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_57, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_57, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_57 = local_266_System_String;
		logic_uScript_AddOnScreenMessage_speaker_57 = messageSpeakerNPC;
		logic_uScript_AddOnScreenMessage_Return_57 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_57.In(logic_uScript_AddOnScreenMessage_locString_57, logic_uScript_AddOnScreenMessage_msgPriority_57, logic_uScript_AddOnScreenMessage_holdMsg_57, logic_uScript_AddOnScreenMessage_tag_57, logic_uScript_AddOnScreenMessage_speaker_57, logic_uScript_AddOnScreenMessage_side_57);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_57.Out)
		{
			Relay_True_88();
		}
	}

	private void Relay_In_58()
	{
		int num = 0;
		Array array = enemyTechDataF;
		if (logic_uScript_GetAndCheckTechs_techData_58.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_58, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_58, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_58 = owner_Connection_68;
		int num2 = 0;
		Array array2 = local_EnemyTechsF_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_58.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_58, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_58, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_58 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.In(logic_uScript_GetAndCheckTechs_techData_58, logic_uScript_GetAndCheckTechs_ownerNode_58, ref logic_uScript_GetAndCheckTechs_techs_58);
		local_EnemyTechsF_TankArray = logic_uScript_GetAndCheckTechs_techs_58;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.AllDead;
		if (allAlive)
		{
			Relay_In_62();
		}
		if (someAlive)
		{
			Relay_In_62();
		}
		if (allDead)
		{
			Relay_In_87();
		}
	}

	private void Relay_True_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.True(out logic_uScriptAct_SetBool_Target_60);
		local_EnemySpottedF_System_Boolean = logic_uScriptAct_SetBool_Target_60;
	}

	private void Relay_False_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.False(out logic_uScriptAct_SetBool_Target_60);
		local_EnemySpottedF_System_Boolean = logic_uScriptAct_SetBool_Target_60;
	}

	private void Relay_In_61()
	{
		logic_uScriptCon_CompareBool_Bool_61 = local_EnemySpottedF_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61.In(logic_uScriptCon_CompareBool_Bool_61);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61.False;
		if (num)
		{
			Relay_In_90();
		}
		if (flag)
		{
			Relay_In_270();
		}
	}

	private void Relay_In_62()
	{
		int num = 0;
		Array array = local_EnemyTechsF_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_62.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_62, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_62, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_range_62 = distEnemiesSpottedMinions;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_62.In(logic_uScript_InRangeOfAtLeastOneTech_techs_62, logic_uScript_InRangeOfAtLeastOneTech_range_62);
		bool inRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_62.InRange;
		bool outOfRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_62.OutOfRange;
		if (inRange)
		{
			Relay_In_61();
		}
		if (outOfRange)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_69()
	{
		int num = 0;
		Array array = msgEnemySpottedF;
		if (logic_uScript_AddOnScreenMessage_locString_69.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_69, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_69, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_69 = local_267_System_String;
		logic_uScript_AddOnScreenMessage_speaker_69 = messageSpeakerMinion;
		logic_uScript_AddOnScreenMessage_Return_69 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_69.In(logic_uScript_AddOnScreenMessage_locString_69, logic_uScript_AddOnScreenMessage_msgPriority_69, logic_uScript_AddOnScreenMessage_holdMsg_69, logic_uScript_AddOnScreenMessage_tag_69, logic_uScript_AddOnScreenMessage_speaker_69, logic_uScript_AddOnScreenMessage_side_69);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_69.Out)
		{
			Relay_True_60();
		}
	}

	private void Relay_Save_Out_77()
	{
		Relay_Save_79();
	}

	private void Relay_Load_Out_77()
	{
		Relay_Load_79();
	}

	private void Relay_Restart_Out_77()
	{
		Relay_Set_False_79();
	}

	private void Relay_Save_77()
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = local_TriggerHit4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_77 = local_TriggerHit4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Save(ref logic_SubGraph_SaveLoadBool_boolean_77, logic_SubGraph_SaveLoadBool_boolAsVariable_77, logic_SubGraph_SaveLoadBool_uniqueID_77);
	}

	private void Relay_Load_77()
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = local_TriggerHit4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_77 = local_TriggerHit4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Load(ref logic_SubGraph_SaveLoadBool_boolean_77, logic_SubGraph_SaveLoadBool_boolAsVariable_77, logic_SubGraph_SaveLoadBool_uniqueID_77);
	}

	private void Relay_Set_True_77()
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = local_TriggerHit4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_77 = local_TriggerHit4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_77, logic_SubGraph_SaveLoadBool_boolAsVariable_77, logic_SubGraph_SaveLoadBool_uniqueID_77);
	}

	private void Relay_Set_False_77()
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = local_TriggerHit4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_77 = local_TriggerHit4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_77, logic_SubGraph_SaveLoadBool_boolAsVariable_77, logic_SubGraph_SaveLoadBool_uniqueID_77);
	}

	private void Relay_Save_Out_78()
	{
		Relay_Save_77();
	}

	private void Relay_Load_Out_78()
	{
		Relay_Load_77();
	}

	private void Relay_Restart_Out_78()
	{
		Relay_Set_False_77();
	}

	private void Relay_Save_78()
	{
		logic_SubGraph_SaveLoadBool_boolean_78 = local_TriggerHit5or6_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_78 = local_TriggerHit5or6_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Save(ref logic_SubGraph_SaveLoadBool_boolean_78, logic_SubGraph_SaveLoadBool_boolAsVariable_78, logic_SubGraph_SaveLoadBool_uniqueID_78);
	}

	private void Relay_Load_78()
	{
		logic_SubGraph_SaveLoadBool_boolean_78 = local_TriggerHit5or6_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_78 = local_TriggerHit5or6_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Load(ref logic_SubGraph_SaveLoadBool_boolean_78, logic_SubGraph_SaveLoadBool_boolAsVariable_78, logic_SubGraph_SaveLoadBool_uniqueID_78);
	}

	private void Relay_Set_True_78()
	{
		logic_SubGraph_SaveLoadBool_boolean_78 = local_TriggerHit5or6_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_78 = local_TriggerHit5or6_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_78, logic_SubGraph_SaveLoadBool_boolAsVariable_78, logic_SubGraph_SaveLoadBool_uniqueID_78);
	}

	private void Relay_Set_False_78()
	{
		logic_SubGraph_SaveLoadBool_boolean_78 = local_TriggerHit5or6_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_78 = local_TriggerHit5or6_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_78.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_78, logic_SubGraph_SaveLoadBool_boolAsVariable_78, logic_SubGraph_SaveLoadBool_uniqueID_78);
	}

	private void Relay_Save_Out_79()
	{
		Relay_Save_80();
	}

	private void Relay_Load_Out_79()
	{
		Relay_Load_80();
	}

	private void Relay_Restart_Out_79()
	{
		Relay_Set_False_80();
	}

	private void Relay_Save_79()
	{
		logic_SubGraph_SaveLoadBool_boolean_79 = local_TriggerHit3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_79 = local_TriggerHit3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Save(ref logic_SubGraph_SaveLoadBool_boolean_79, logic_SubGraph_SaveLoadBool_boolAsVariable_79, logic_SubGraph_SaveLoadBool_uniqueID_79);
	}

	private void Relay_Load_79()
	{
		logic_SubGraph_SaveLoadBool_boolean_79 = local_TriggerHit3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_79 = local_TriggerHit3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Load(ref logic_SubGraph_SaveLoadBool_boolean_79, logic_SubGraph_SaveLoadBool_boolAsVariable_79, logic_SubGraph_SaveLoadBool_uniqueID_79);
	}

	private void Relay_Set_True_79()
	{
		logic_SubGraph_SaveLoadBool_boolean_79 = local_TriggerHit3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_79 = local_TriggerHit3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_79, logic_SubGraph_SaveLoadBool_boolAsVariable_79, logic_SubGraph_SaveLoadBool_uniqueID_79);
	}

	private void Relay_Set_False_79()
	{
		logic_SubGraph_SaveLoadBool_boolean_79 = local_TriggerHit3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_79 = local_TriggerHit3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_79.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_79, logic_SubGraph_SaveLoadBool_boolAsVariable_79, logic_SubGraph_SaveLoadBool_uniqueID_79);
	}

	private void Relay_Save_Out_80()
	{
		Relay_Save_81();
	}

	private void Relay_Load_Out_80()
	{
		Relay_Load_81();
	}

	private void Relay_Restart_Out_80()
	{
		Relay_Set_False_81();
	}

	private void Relay_Save_80()
	{
		logic_SubGraph_SaveLoadBool_boolean_80 = local_TriggerHit2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_80 = local_TriggerHit2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Save(ref logic_SubGraph_SaveLoadBool_boolean_80, logic_SubGraph_SaveLoadBool_boolAsVariable_80, logic_SubGraph_SaveLoadBool_uniqueID_80);
	}

	private void Relay_Load_80()
	{
		logic_SubGraph_SaveLoadBool_boolean_80 = local_TriggerHit2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_80 = local_TriggerHit2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Load(ref logic_SubGraph_SaveLoadBool_boolean_80, logic_SubGraph_SaveLoadBool_boolAsVariable_80, logic_SubGraph_SaveLoadBool_uniqueID_80);
	}

	private void Relay_Set_True_80()
	{
		logic_SubGraph_SaveLoadBool_boolean_80 = local_TriggerHit2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_80 = local_TriggerHit2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_80, logic_SubGraph_SaveLoadBool_boolAsVariable_80, logic_SubGraph_SaveLoadBool_uniqueID_80);
	}

	private void Relay_Set_False_80()
	{
		logic_SubGraph_SaveLoadBool_boolean_80 = local_TriggerHit2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_80 = local_TriggerHit2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_80.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_80, logic_SubGraph_SaveLoadBool_boolAsVariable_80, logic_SubGraph_SaveLoadBool_uniqueID_80);
	}

	private void Relay_Save_Out_81()
	{
		Relay_Save_93();
	}

	private void Relay_Load_Out_81()
	{
		Relay_Load_93();
	}

	private void Relay_Restart_Out_81()
	{
		Relay_Set_False_93();
	}

	private void Relay_Save_81()
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = local_TriggerHit1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_81 = local_TriggerHit1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Save(ref logic_SubGraph_SaveLoadBool_boolean_81, logic_SubGraph_SaveLoadBool_boolAsVariable_81, logic_SubGraph_SaveLoadBool_uniqueID_81);
	}

	private void Relay_Load_81()
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = local_TriggerHit1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_81 = local_TriggerHit1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Load(ref logic_SubGraph_SaveLoadBool_boolean_81, logic_SubGraph_SaveLoadBool_boolAsVariable_81, logic_SubGraph_SaveLoadBool_uniqueID_81);
	}

	private void Relay_Set_True_81()
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = local_TriggerHit1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_81 = local_TriggerHit1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_81, logic_SubGraph_SaveLoadBool_boolAsVariable_81, logic_SubGraph_SaveLoadBool_uniqueID_81);
	}

	private void Relay_Set_False_81()
	{
		logic_SubGraph_SaveLoadBool_boolean_81 = local_TriggerHit1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_81 = local_TriggerHit1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_81.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_81, logic_SubGraph_SaveLoadBool_boolAsVariable_81, logic_SubGraph_SaveLoadBool_uniqueID_81);
	}

	private void Relay_Save_Out_85()
	{
		Relay_Save_127();
	}

	private void Relay_Load_Out_85()
	{
		Relay_Load_127();
	}

	private void Relay_Restart_Out_85()
	{
		Relay_Set_False_127();
	}

	private void Relay_Save_85()
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = local_EnemySpottedF_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_85 = local_EnemySpottedF_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Save(ref logic_SubGraph_SaveLoadBool_boolean_85, logic_SubGraph_SaveLoadBool_boolAsVariable_85, logic_SubGraph_SaveLoadBool_uniqueID_85);
	}

	private void Relay_Load_85()
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = local_EnemySpottedF_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_85 = local_EnemySpottedF_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Load(ref logic_SubGraph_SaveLoadBool_boolean_85, logic_SubGraph_SaveLoadBool_boolAsVariable_85, logic_SubGraph_SaveLoadBool_uniqueID_85);
	}

	private void Relay_Set_True_85()
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = local_EnemySpottedF_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_85 = local_EnemySpottedF_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_85, logic_SubGraph_SaveLoadBool_boolAsVariable_85, logic_SubGraph_SaveLoadBool_uniqueID_85);
	}

	private void Relay_Set_False_85()
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = local_EnemySpottedF_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_85 = local_EnemySpottedF_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_85, logic_SubGraph_SaveLoadBool_boolAsVariable_85, logic_SubGraph_SaveLoadBool_uniqueID_85);
	}

	private void Relay_In_87()
	{
		logic_uScriptCon_CompareBool_Bool_87 = local_TechsDeadF_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.In(logic_uScriptCon_CompareBool_Bool_87);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.False;
		if (num)
		{
			Relay_In_90();
		}
		if (flag)
		{
			Relay_In_387();
		}
	}

	private void Relay_True_88()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.True(out logic_uScriptAct_SetBool_Target_88);
		local_TechsDeadF_System_Boolean = logic_uScriptAct_SetBool_Target_88;
	}

	private void Relay_False_88()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.False(out logic_uScriptAct_SetBool_Target_88);
		local_TechsDeadF_System_Boolean = logic_uScriptAct_SetBool_Target_88;
	}

	private void Relay_In_90()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.Out)
		{
			Relay_In_113();
		}
	}

	private void Relay_Save_Out_92()
	{
		Relay_Save_128();
	}

	private void Relay_Load_Out_92()
	{
		Relay_Load_128();
	}

	private void Relay_Restart_Out_92()
	{
		Relay_Set_False_128();
	}

	private void Relay_Save_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_TechsDeadF_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_TechsDeadF_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Save(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Load_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_TechsDeadF_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_TechsDeadF_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Load(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Set_True_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_TechsDeadF_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_TechsDeadF_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Set_False_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_TechsDeadF_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_TechsDeadF_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Save_Out_93()
	{
		Relay_Save_92();
	}

	private void Relay_Load_Out_93()
	{
		Relay_Load_92();
	}

	private void Relay_Restart_Out_93()
	{
		Relay_Set_False_92();
	}

	private void Relay_Save_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Load_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Set_True_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Set_False_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_In_95()
	{
		logic_uScriptCon_CompareBool_Bool_95 = local_TechsDeadE_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.In(logic_uScriptCon_CompareBool_Bool_95);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.False;
		if (num)
		{
			Relay_In_107();
		}
		if (flag)
		{
			Relay_In_385();
		}
	}

	private void Relay_In_99()
	{
		int num = 0;
		Array array = msgTechsDeadE;
		if (logic_uScript_AddOnScreenMessage_locString_99.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_99, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_99, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_99 = local_274_System_String;
		logic_uScript_AddOnScreenMessage_speaker_99 = messageSpeakerNPC;
		logic_uScript_AddOnScreenMessage_Return_99 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_99.In(logic_uScript_AddOnScreenMessage_locString_99, logic_uScript_AddOnScreenMessage_msgPriority_99, logic_uScript_AddOnScreenMessage_holdMsg_99, logic_uScript_AddOnScreenMessage_tag_99, logic_uScript_AddOnScreenMessage_speaker_99, logic_uScript_AddOnScreenMessage_side_99);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_99.Out)
		{
			Relay_True_117();
		}
	}

	private void Relay_In_100()
	{
		int num = 0;
		Array array = msgEnemySpottedE;
		if (logic_uScript_AddOnScreenMessage_locString_100.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_100, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_100, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_100 = local_275_System_String;
		logic_uScript_AddOnScreenMessage_speaker_100 = messageSpeakerMinion;
		logic_uScript_AddOnScreenMessage_Return_100 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_100.In(logic_uScript_AddOnScreenMessage_locString_100, logic_uScript_AddOnScreenMessage_msgPriority_100, logic_uScript_AddOnScreenMessage_holdMsg_100, logic_uScript_AddOnScreenMessage_tag_100, logic_uScript_AddOnScreenMessage_speaker_100, logic_uScript_AddOnScreenMessage_side_100);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_100.Out)
		{
			Relay_True_124();
		}
	}

	private void Relay_In_102()
	{
		logic_uScriptCon_CompareBool_Bool_102 = local_EnemySpottedE_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102.In(logic_uScriptCon_CompareBool_Bool_102);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102.False;
		if (num)
		{
			Relay_In_107();
		}
		if (flag)
		{
			Relay_In_276();
		}
	}

	private void Relay_True_105()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_105.True(out logic_uScriptAct_SetBool_Target_105);
		local_TriggerHit3_System_Boolean = logic_uScriptAct_SetBool_Target_105;
	}

	private void Relay_False_105()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_105.False(out logic_uScriptAct_SetBool_Target_105);
		local_TriggerHit3_System_Boolean = logic_uScriptAct_SetBool_Target_105;
	}

	private void Relay_In_106()
	{
		int num = 0;
		Array array = enemyTechDataE;
		if (logic_uScript_GetAndCheckTechs_techData_106.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_106, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_106, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_106 = owner_Connection_123;
		int num2 = 0;
		Array array2 = local_EnemyTechsE_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_106.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_106, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_106, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_106 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_106.In(logic_uScript_GetAndCheckTechs_techData_106, logic_uScript_GetAndCheckTechs_ownerNode_106, ref logic_uScript_GetAndCheckTechs_techs_106);
		local_EnemyTechsE_TankArray = logic_uScript_GetAndCheckTechs_techs_106;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_106.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_106.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_106.AllDead;
		if (allAlive)
		{
			Relay_In_114();
		}
		if (someAlive)
		{
			Relay_In_114();
		}
		if (allDead)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_107()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_In_109()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_109 = Trigger3;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_109.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_109);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_109.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_109.OutOfRange;
		if (inRange)
		{
			Relay_InitialSpawn_115();
		}
		if (outOfRange)
		{
			Relay_In_273();
		}
	}

	private void Relay_In_113()
	{
		logic_uScriptCon_CompareBool_Bool_113 = local_TriggerHit3_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113.In(logic_uScriptCon_CompareBool_Bool_113);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113.False;
		if (num)
		{
			Relay_In_106();
		}
		if (flag)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_114()
	{
		int num = 0;
		Array array = local_EnemyTechsE_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_114.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_114, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_114, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_range_114 = distEnemiesSpottedMinions;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_114.In(logic_uScript_InRangeOfAtLeastOneTech_techs_114, logic_uScript_InRangeOfAtLeastOneTech_range_114);
		bool inRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_114.InRange;
		bool outOfRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_114.OutOfRange;
		if (inRange)
		{
			Relay_In_102();
		}
		if (outOfRange)
		{
			Relay_In_107();
		}
	}

	private void Relay_InitialSpawn_115()
	{
		int num = 0;
		Array array = enemyTechDataE;
		if (logic_uScript_SpawnTechsFromData_spawnData_115.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_115, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_115, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_115 = owner_Connection_122;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_115.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_115, logic_uScript_SpawnTechsFromData_ownerNode_115, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_115);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_115.Out)
		{
			Relay_True_105();
		}
	}

	private void Relay_True_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.True(out logic_uScriptAct_SetBool_Target_117);
		local_TechsDeadE_System_Boolean = logic_uScriptAct_SetBool_Target_117;
	}

	private void Relay_False_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.False(out logic_uScriptAct_SetBool_Target_117);
		local_TechsDeadE_System_Boolean = logic_uScriptAct_SetBool_Target_117;
	}

	private void Relay_True_124()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.True(out logic_uScriptAct_SetBool_Target_124);
		local_EnemySpottedE_System_Boolean = logic_uScriptAct_SetBool_Target_124;
	}

	private void Relay_False_124()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.False(out logic_uScriptAct_SetBool_Target_124);
		local_EnemySpottedE_System_Boolean = logic_uScriptAct_SetBool_Target_124;
	}

	private void Relay_Save_Out_127()
	{
		Relay_Save_130();
	}

	private void Relay_Load_Out_127()
	{
		Relay_Load_130();
	}

	private void Relay_Restart_Out_127()
	{
		Relay_Set_False_130();
	}

	private void Relay_Save_127()
	{
		logic_SubGraph_SaveLoadBool_boolean_127 = local_EnemySpottedE_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_127 = local_EnemySpottedE_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Save(ref logic_SubGraph_SaveLoadBool_boolean_127, logic_SubGraph_SaveLoadBool_boolAsVariable_127, logic_SubGraph_SaveLoadBool_uniqueID_127);
	}

	private void Relay_Load_127()
	{
		logic_SubGraph_SaveLoadBool_boolean_127 = local_EnemySpottedE_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_127 = local_EnemySpottedE_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Load(ref logic_SubGraph_SaveLoadBool_boolean_127, logic_SubGraph_SaveLoadBool_boolAsVariable_127, logic_SubGraph_SaveLoadBool_uniqueID_127);
	}

	private void Relay_Set_True_127()
	{
		logic_SubGraph_SaveLoadBool_boolean_127 = local_EnemySpottedE_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_127 = local_EnemySpottedE_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_127, logic_SubGraph_SaveLoadBool_boolAsVariable_127, logic_SubGraph_SaveLoadBool_uniqueID_127);
	}

	private void Relay_Set_False_127()
	{
		logic_SubGraph_SaveLoadBool_boolean_127 = local_EnemySpottedE_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_127 = local_EnemySpottedE_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_127.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_127, logic_SubGraph_SaveLoadBool_boolAsVariable_127, logic_SubGraph_SaveLoadBool_uniqueID_127);
	}

	private void Relay_Save_Out_128()
	{
		Relay_Save_133();
	}

	private void Relay_Load_Out_128()
	{
		Relay_Load_133();
	}

	private void Relay_Restart_Out_128()
	{
		Relay_Set_False_133();
	}

	private void Relay_Save_128()
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = local_TechsDeadE_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_128 = local_TechsDeadE_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Save(ref logic_SubGraph_SaveLoadBool_boolean_128, logic_SubGraph_SaveLoadBool_boolAsVariable_128, logic_SubGraph_SaveLoadBool_uniqueID_128);
	}

	private void Relay_Load_128()
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = local_TechsDeadE_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_128 = local_TechsDeadE_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Load(ref logic_SubGraph_SaveLoadBool_boolean_128, logic_SubGraph_SaveLoadBool_boolAsVariable_128, logic_SubGraph_SaveLoadBool_uniqueID_128);
	}

	private void Relay_Set_True_128()
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = local_TechsDeadE_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_128 = local_TechsDeadE_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_128, logic_SubGraph_SaveLoadBool_boolAsVariable_128, logic_SubGraph_SaveLoadBool_uniqueID_128);
	}

	private void Relay_Set_False_128()
	{
		logic_SubGraph_SaveLoadBool_boolean_128 = local_TechsDeadE_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_128 = local_TechsDeadE_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_128.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_128, logic_SubGraph_SaveLoadBool_boolAsVariable_128, logic_SubGraph_SaveLoadBool_uniqueID_128);
	}

	private void Relay_Save_Out_130()
	{
		Relay_Save_134();
	}

	private void Relay_Load_Out_130()
	{
		Relay_Load_134();
	}

	private void Relay_Restart_Out_130()
	{
		Relay_Set_False_134();
	}

	private void Relay_Save_130()
	{
		logic_SubGraph_SaveLoadBool_boolean_130 = local_EnemySpottedD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_130 = local_EnemySpottedD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Save(ref logic_SubGraph_SaveLoadBool_boolean_130, logic_SubGraph_SaveLoadBool_boolAsVariable_130, logic_SubGraph_SaveLoadBool_uniqueID_130);
	}

	private void Relay_Load_130()
	{
		logic_SubGraph_SaveLoadBool_boolean_130 = local_EnemySpottedD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_130 = local_EnemySpottedD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Load(ref logic_SubGraph_SaveLoadBool_boolean_130, logic_SubGraph_SaveLoadBool_boolAsVariable_130, logic_SubGraph_SaveLoadBool_uniqueID_130);
	}

	private void Relay_Set_True_130()
	{
		logic_SubGraph_SaveLoadBool_boolean_130 = local_EnemySpottedD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_130 = local_EnemySpottedD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_130, logic_SubGraph_SaveLoadBool_boolAsVariable_130, logic_SubGraph_SaveLoadBool_uniqueID_130);
	}

	private void Relay_Set_False_130()
	{
		logic_SubGraph_SaveLoadBool_boolean_130 = local_EnemySpottedD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_130 = local_EnemySpottedD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_130, logic_SubGraph_SaveLoadBool_boolAsVariable_130, logic_SubGraph_SaveLoadBool_uniqueID_130);
	}

	private void Relay_Save_Out_133()
	{
		Relay_Save_137();
	}

	private void Relay_Load_Out_133()
	{
		Relay_Load_137();
	}

	private void Relay_Restart_Out_133()
	{
		Relay_Set_False_137();
	}

	private void Relay_Save_133()
	{
		logic_SubGraph_SaveLoadBool_boolean_133 = local_TechsDeadD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_133 = local_TechsDeadD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Save(ref logic_SubGraph_SaveLoadBool_boolean_133, logic_SubGraph_SaveLoadBool_boolAsVariable_133, logic_SubGraph_SaveLoadBool_uniqueID_133);
	}

	private void Relay_Load_133()
	{
		logic_SubGraph_SaveLoadBool_boolean_133 = local_TechsDeadD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_133 = local_TechsDeadD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Load(ref logic_SubGraph_SaveLoadBool_boolean_133, logic_SubGraph_SaveLoadBool_boolAsVariable_133, logic_SubGraph_SaveLoadBool_uniqueID_133);
	}

	private void Relay_Set_True_133()
	{
		logic_SubGraph_SaveLoadBool_boolean_133 = local_TechsDeadD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_133 = local_TechsDeadD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_133, logic_SubGraph_SaveLoadBool_boolAsVariable_133, logic_SubGraph_SaveLoadBool_uniqueID_133);
	}

	private void Relay_Set_False_133()
	{
		logic_SubGraph_SaveLoadBool_boolean_133 = local_TechsDeadD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_133 = local_TechsDeadD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_133.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_133, logic_SubGraph_SaveLoadBool_boolAsVariable_133, logic_SubGraph_SaveLoadBool_uniqueID_133);
	}

	private void Relay_Save_Out_134()
	{
		Relay_Save_139();
	}

	private void Relay_Load_Out_134()
	{
		Relay_Load_139();
	}

	private void Relay_Restart_Out_134()
	{
		Relay_Set_False_139();
	}

	private void Relay_Save_134()
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = local_EnemySpottedC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_134 = local_EnemySpottedC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Save(ref logic_SubGraph_SaveLoadBool_boolean_134, logic_SubGraph_SaveLoadBool_boolAsVariable_134, logic_SubGraph_SaveLoadBool_uniqueID_134);
	}

	private void Relay_Load_134()
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = local_EnemySpottedC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_134 = local_EnemySpottedC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Load(ref logic_SubGraph_SaveLoadBool_boolean_134, logic_SubGraph_SaveLoadBool_boolAsVariable_134, logic_SubGraph_SaveLoadBool_uniqueID_134);
	}

	private void Relay_Set_True_134()
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = local_EnemySpottedC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_134 = local_EnemySpottedC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_134, logic_SubGraph_SaveLoadBool_boolAsVariable_134, logic_SubGraph_SaveLoadBool_uniqueID_134);
	}

	private void Relay_Set_False_134()
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = local_EnemySpottedC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_134 = local_EnemySpottedC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_134, logic_SubGraph_SaveLoadBool_boolAsVariable_134, logic_SubGraph_SaveLoadBool_uniqueID_134);
	}

	private void Relay_Save_Out_137()
	{
		Relay_Save_140();
	}

	private void Relay_Load_Out_137()
	{
		Relay_Load_140();
	}

	private void Relay_Restart_Out_137()
	{
		Relay_Set_False_140();
	}

	private void Relay_Save_137()
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = local_TechsDeadC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_TechsDeadC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Save(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
	}

	private void Relay_Load_137()
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = local_TechsDeadC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_TechsDeadC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Load(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
	}

	private void Relay_Set_True_137()
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = local_TechsDeadC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_TechsDeadC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
	}

	private void Relay_Set_False_137()
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = local_TechsDeadC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_TechsDeadC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
	}

	private void Relay_Save_Out_139()
	{
		Relay_Save_143();
	}

	private void Relay_Load_Out_139()
	{
		Relay_Load_143();
	}

	private void Relay_Restart_Out_139()
	{
		Relay_Set_False_143();
	}

	private void Relay_Save_139()
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = local_EnemySpottedB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_139 = local_EnemySpottedB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Save(ref logic_SubGraph_SaveLoadBool_boolean_139, logic_SubGraph_SaveLoadBool_boolAsVariable_139, logic_SubGraph_SaveLoadBool_uniqueID_139);
	}

	private void Relay_Load_139()
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = local_EnemySpottedB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_139 = local_EnemySpottedB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Load(ref logic_SubGraph_SaveLoadBool_boolean_139, logic_SubGraph_SaveLoadBool_boolAsVariable_139, logic_SubGraph_SaveLoadBool_uniqueID_139);
	}

	private void Relay_Set_True_139()
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = local_EnemySpottedB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_139 = local_EnemySpottedB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_139, logic_SubGraph_SaveLoadBool_boolAsVariable_139, logic_SubGraph_SaveLoadBool_uniqueID_139);
	}

	private void Relay_Set_False_139()
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = local_EnemySpottedB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_139 = local_EnemySpottedB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_139, logic_SubGraph_SaveLoadBool_boolAsVariable_139, logic_SubGraph_SaveLoadBool_uniqueID_139);
	}

	private void Relay_Save_Out_140()
	{
		Relay_Save_145();
	}

	private void Relay_Load_Out_140()
	{
		Relay_Load_145();
	}

	private void Relay_Restart_Out_140()
	{
		Relay_Set_False_145();
	}

	private void Relay_Save_140()
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = local_TechsDeadB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_140 = local_TechsDeadB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Save(ref logic_SubGraph_SaveLoadBool_boolean_140, logic_SubGraph_SaveLoadBool_boolAsVariable_140, logic_SubGraph_SaveLoadBool_uniqueID_140);
	}

	private void Relay_Load_140()
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = local_TechsDeadB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_140 = local_TechsDeadB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Load(ref logic_SubGraph_SaveLoadBool_boolean_140, logic_SubGraph_SaveLoadBool_boolAsVariable_140, logic_SubGraph_SaveLoadBool_uniqueID_140);
	}

	private void Relay_Set_True_140()
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = local_TechsDeadB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_140 = local_TechsDeadB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_140, logic_SubGraph_SaveLoadBool_boolAsVariable_140, logic_SubGraph_SaveLoadBool_uniqueID_140);
	}

	private void Relay_Set_False_140()
	{
		logic_SubGraph_SaveLoadBool_boolean_140 = local_TechsDeadB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_140 = local_TechsDeadB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_140.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_140, logic_SubGraph_SaveLoadBool_boolAsVariable_140, logic_SubGraph_SaveLoadBool_uniqueID_140);
	}

	private void Relay_Save_Out_143()
	{
		Relay_Save_78();
	}

	private void Relay_Load_Out_143()
	{
		Relay_Load_78();
	}

	private void Relay_Restart_Out_143()
	{
		Relay_Set_False_78();
	}

	private void Relay_Save_143()
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = local_EnemySpottedA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_143 = local_EnemySpottedA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Save(ref logic_SubGraph_SaveLoadBool_boolean_143, logic_SubGraph_SaveLoadBool_boolAsVariable_143, logic_SubGraph_SaveLoadBool_uniqueID_143);
	}

	private void Relay_Load_143()
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = local_EnemySpottedA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_143 = local_EnemySpottedA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Load(ref logic_SubGraph_SaveLoadBool_boolean_143, logic_SubGraph_SaveLoadBool_boolAsVariable_143, logic_SubGraph_SaveLoadBool_uniqueID_143);
	}

	private void Relay_Set_True_143()
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = local_EnemySpottedA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_143 = local_EnemySpottedA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_143, logic_SubGraph_SaveLoadBool_boolAsVariable_143, logic_SubGraph_SaveLoadBool_uniqueID_143);
	}

	private void Relay_Set_False_143()
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = local_EnemySpottedA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_143 = local_EnemySpottedA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_143, logic_SubGraph_SaveLoadBool_boolAsVariable_143, logic_SubGraph_SaveLoadBool_uniqueID_143);
	}

	private void Relay_Save_Out_145()
	{
		Relay_Save_193();
	}

	private void Relay_Load_Out_145()
	{
		Relay_Load_193();
	}

	private void Relay_Restart_Out_145()
	{
		Relay_Set_False_193();
	}

	private void Relay_Save_145()
	{
		logic_SubGraph_SaveLoadBool_boolean_145 = local_TechsDeadA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_145 = local_TechsDeadA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Save(ref logic_SubGraph_SaveLoadBool_boolean_145, logic_SubGraph_SaveLoadBool_boolAsVariable_145, logic_SubGraph_SaveLoadBool_uniqueID_145);
	}

	private void Relay_Load_145()
	{
		logic_SubGraph_SaveLoadBool_boolean_145 = local_TechsDeadA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_145 = local_TechsDeadA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Load(ref logic_SubGraph_SaveLoadBool_boolean_145, logic_SubGraph_SaveLoadBool_boolAsVariable_145, logic_SubGraph_SaveLoadBool_uniqueID_145);
	}

	private void Relay_Set_True_145()
	{
		logic_SubGraph_SaveLoadBool_boolean_145 = local_TechsDeadA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_145 = local_TechsDeadA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_145, logic_SubGraph_SaveLoadBool_boolAsVariable_145, logic_SubGraph_SaveLoadBool_uniqueID_145);
	}

	private void Relay_Set_False_145()
	{
		logic_SubGraph_SaveLoadBool_boolean_145 = local_TechsDeadA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_145 = local_TechsDeadA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_145.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_145, logic_SubGraph_SaveLoadBool_boolAsVariable_145, logic_SubGraph_SaveLoadBool_uniqueID_145);
	}

	private void Relay_In_147()
	{
		int num = 0;
		Array array = enemyTechDataC;
		if (logic_uScript_GetAndCheckTechs_techData_147.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_147, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_147, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_147 = owner_Connection_149;
		int num2 = 0;
		Array array2 = local_EnemyTechsC_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_147.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_147, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_147, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_147 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_147.In(logic_uScript_GetAndCheckTechs_techData_147, logic_uScript_GetAndCheckTechs_ownerNode_147, ref logic_uScript_GetAndCheckTechs_techs_147);
		local_EnemyTechsC_TankArray = logic_uScript_GetAndCheckTechs_techs_147;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_147.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_147.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_147.AllDead;
		if (allAlive)
		{
			Relay_In_155();
		}
		if (someAlive)
		{
			Relay_In_155();
		}
		if (allDead)
		{
			Relay_True_184();
		}
	}

	private void Relay_True_151()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_151.True(out logic_uScriptAct_SetBool_Target_151);
		local_TechsDeadCandD_System_Boolean = logic_uScriptAct_SetBool_Target_151;
	}

	private void Relay_False_151()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_151.False(out logic_uScriptAct_SetBool_Target_151);
		local_TechsDeadCandD_System_Boolean = logic_uScriptAct_SetBool_Target_151;
	}

	private void Relay_In_153()
	{
		int num = 0;
		Array array = msgEnemySpottedC;
		if (logic_uScript_AddOnScreenMessage_locString_153.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_153, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_153, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_153 = local_287_System_String;
		logic_uScript_AddOnScreenMessage_speaker_153 = messageSpeakerMinion;
		logic_uScript_AddOnScreenMessage_Return_153 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_153.In(logic_uScript_AddOnScreenMessage_locString_153, logic_uScript_AddOnScreenMessage_msgPriority_153, logic_uScript_AddOnScreenMessage_holdMsg_153, logic_uScript_AddOnScreenMessage_tag_153, logic_uScript_AddOnScreenMessage_speaker_153, logic_uScript_AddOnScreenMessage_side_153);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_153.Out)
		{
			Relay_True_157();
		}
	}

	private void Relay_In_155()
	{
		int num = 0;
		Array array = local_EnemyTechsC_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_155.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_155, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_155, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_range_155 = distEnemiesSpottedMinions;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_155.In(logic_uScript_InRangeOfAtLeastOneTech_techs_155, logic_uScript_InRangeOfAtLeastOneTech_range_155);
		bool inRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_155.InRange;
		bool outOfRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_155.OutOfRange;
		if (inRange)
		{
			Relay_In_169();
		}
		if (outOfRange)
		{
			Relay_In_165();
		}
	}

	private void Relay_True_157()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_157.True(out logic_uScriptAct_SetBool_Target_157);
		local_EnemySpottedC_System_Boolean = logic_uScriptAct_SetBool_Target_157;
	}

	private void Relay_False_157()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_157.False(out logic_uScriptAct_SetBool_Target_157);
		local_EnemySpottedC_System_Boolean = logic_uScriptAct_SetBool_Target_157;
	}

	private void Relay_InitialSpawn_159()
	{
		int num = 0;
		Array array = enemyTechDataD;
		if (logic_uScript_SpawnTechsFromData_spawnData_159.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_159, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_159, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_159 = owner_Connection_154;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_159.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_159, logic_uScript_SpawnTechsFromData_ownerNode_159, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_159);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_159.Out)
		{
			Relay_InitialSpawn_178();
		}
	}

	private void Relay_In_161()
	{
		int num = 0;
		Array array = msgTechsDeadCandD;
		if (logic_uScript_AddOnScreenMessage_locString_161.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_161, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_161, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_161 = local_290_System_String;
		logic_uScript_AddOnScreenMessage_speaker_161 = messageSpeakerNPC;
		logic_uScript_AddOnScreenMessage_Return_161 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161.In(logic_uScript_AddOnScreenMessage_locString_161, logic_uScript_AddOnScreenMessage_msgPriority_161, logic_uScript_AddOnScreenMessage_holdMsg_161, logic_uScript_AddOnScreenMessage_tag_161, logic_uScript_AddOnScreenMessage_speaker_161, logic_uScript_AddOnScreenMessage_side_161);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161.Out)
		{
			Relay_True_151();
		}
	}

	private void Relay_In_163()
	{
		logic_uScriptCon_CompareBool_Bool_163 = local_TriggerHit2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163.In(logic_uScriptCon_CompareBool_Bool_163);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163.False;
		if (num)
		{
			Relay_In_147();
			Relay_In_179();
		}
		if (flag)
		{
			Relay_In_174();
		}
	}

	private void Relay_In_165()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_165.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_165.Out)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_169()
	{
		logic_uScriptCon_CompareBool_Bool_169 = local_EnemySpottedC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_169.In(logic_uScriptCon_CompareBool_Bool_169);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_169.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_169.False;
		if (num)
		{
			Relay_In_165();
		}
		if (flag)
		{
			Relay_In_285();
		}
	}

	private void Relay_In_170()
	{
		logic_uScriptCon_CompareBool_Bool_170 = local_TechsDeadCandD_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_170.In(logic_uScriptCon_CompareBool_Bool_170);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_170.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_170.False;
		if (num)
		{
			Relay_In_165();
		}
		if (flag)
		{
			Relay_In_383();
		}
	}

	private void Relay_In_174()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_174 = Trigger2;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_174.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_174);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_174.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_174.OutOfRange;
		if (inRange)
		{
			Relay_InitialSpawn_159();
		}
		if (outOfRange)
		{
			Relay_In_281();
		}
	}

	private void Relay_True_175()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_175.True(out logic_uScriptAct_SetBool_Target_175);
		local_TriggerHit2_System_Boolean = logic_uScriptAct_SetBool_Target_175;
	}

	private void Relay_False_175()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_175.False(out logic_uScriptAct_SetBool_Target_175);
		local_TriggerHit2_System_Boolean = logic_uScriptAct_SetBool_Target_175;
	}

	private void Relay_InitialSpawn_178()
	{
		int num = 0;
		Array array = enemyTechDataC;
		if (logic_uScript_SpawnTechsFromData_spawnData_178.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_178, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_178, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_178 = owner_Connection_177;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_178.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_178, logic_uScript_SpawnTechsFromData_ownerNode_178, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_178);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_178.Out)
		{
			Relay_True_175();
		}
	}

	private void Relay_In_179()
	{
		int num = 0;
		Array array = enemyTechDataD;
		if (logic_uScript_GetAndCheckTechs_techData_179.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_179, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_179, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_179 = owner_Connection_182;
		int num2 = 0;
		Array array2 = local_EnemyTechsD_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_179.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_179, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_179, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_179 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_179.In(logic_uScript_GetAndCheckTechs_techData_179, logic_uScript_GetAndCheckTechs_ownerNode_179, ref logic_uScript_GetAndCheckTechs_techs_179);
		local_EnemyTechsD_TankArray = logic_uScript_GetAndCheckTechs_techs_179;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_179.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_179.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_179.AllDead;
		if (allAlive)
		{
			Relay_In_195();
		}
		if (someAlive)
		{
			Relay_In_195();
		}
		if (allDead)
		{
			Relay_True_183();
		}
	}

	private void Relay_True_183()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_183.True(out logic_uScriptAct_SetBool_Target_183);
		local_TechsDeadD_System_Boolean = logic_uScriptAct_SetBool_Target_183;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_183.SetTrue)
		{
			Relay_In_185();
		}
	}

	private void Relay_False_183()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_183.False(out logic_uScriptAct_SetBool_Target_183);
		local_TechsDeadD_System_Boolean = logic_uScriptAct_SetBool_Target_183;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_183.SetTrue)
		{
			Relay_In_185();
		}
	}

	private void Relay_True_184()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_184.True(out logic_uScriptAct_SetBool_Target_184);
		local_TechsDeadC_System_Boolean = logic_uScriptAct_SetBool_Target_184;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_184.SetTrue)
		{
			Relay_In_186();
		}
	}

	private void Relay_False_184()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_184.False(out logic_uScriptAct_SetBool_Target_184);
		local_TechsDeadC_System_Boolean = logic_uScriptAct_SetBool_Target_184;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_184.SetTrue)
		{
			Relay_In_186();
		}
	}

	private void Relay_In_185()
	{
		logic_uScriptCon_CompareBool_Bool_185 = local_TechsDeadC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185.In(logic_uScriptCon_CompareBool_Bool_185);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185.False;
		if (num)
		{
			Relay_In_170();
		}
		if (flag)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_186()
	{
		logic_uScriptCon_CompareBool_Bool_186 = local_TechsDeadD_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186.In(logic_uScriptCon_CompareBool_Bool_186);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186.False;
		if (num)
		{
			Relay_In_170();
		}
		if (flag)
		{
			Relay_In_165();
		}
	}

	private void Relay_Save_Out_193()
	{
		Relay_Save_261();
	}

	private void Relay_Load_Out_193()
	{
		Relay_Load_261();
	}

	private void Relay_Restart_Out_193()
	{
		Relay_Set_False_261();
	}

	private void Relay_Save_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_TechsDeadCandD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_TechsDeadCandD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Save(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Load_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_TechsDeadCandD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_TechsDeadCandD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Load(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Set_True_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_TechsDeadCandD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_TechsDeadCandD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Set_False_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_TechsDeadCandD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_TechsDeadCandD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_In_195()
	{
		int num = 0;
		Array array = local_EnemyTechsD_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_195.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_195, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_195, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_range_195 = distEnemiesSpottedMinions;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_195.In(logic_uScript_InRangeOfAtLeastOneTech_techs_195, logic_uScript_InRangeOfAtLeastOneTech_range_195);
		bool inRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_195.InRange;
		bool outOfRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_195.OutOfRange;
		if (inRange)
		{
			Relay_In_202();
		}
		if (outOfRange)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_200()
	{
		int num = 0;
		Array array = msgEnemySpottedD;
		if (logic_uScript_AddOnScreenMessage_locString_200.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_200, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_200, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_200 = local_284_System_String;
		logic_uScript_AddOnScreenMessage_speaker_200 = messageSpeakerMinion;
		logic_uScript_AddOnScreenMessage_Return_200 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200.In(logic_uScript_AddOnScreenMessage_locString_200, logic_uScript_AddOnScreenMessage_msgPriority_200, logic_uScript_AddOnScreenMessage_holdMsg_200, logic_uScript_AddOnScreenMessage_tag_200, logic_uScript_AddOnScreenMessage_speaker_200, logic_uScript_AddOnScreenMessage_side_200);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200.Out)
		{
			Relay_True_201();
		}
	}

	private void Relay_True_201()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_201.True(out logic_uScriptAct_SetBool_Target_201);
		local_EnemySpottedD_System_Boolean = logic_uScriptAct_SetBool_Target_201;
	}

	private void Relay_False_201()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_201.False(out logic_uScriptAct_SetBool_Target_201);
		local_EnemySpottedD_System_Boolean = logic_uScriptAct_SetBool_Target_201;
	}

	private void Relay_In_202()
	{
		logic_uScriptCon_CompareBool_Bool_202 = local_EnemySpottedD_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_202.In(logic_uScriptCon_CompareBool_Bool_202);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_202.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_202.False;
		if (num)
		{
			Relay_In_165();
		}
		if (flag)
		{
			Relay_In_282();
		}
	}

	private void Relay_In_209()
	{
		int num = 0;
		Array array = enemyTechDataB;
		if (logic_uScript_GetAndCheckTechs_techData_209.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_209, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_209, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_209 = owner_Connection_237;
		int num2 = 0;
		Array array2 = local_EnemyTechsB_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_209.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_209, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_209, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_209 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_209.In(logic_uScript_GetAndCheckTechs_techData_209, logic_uScript_GetAndCheckTechs_ownerNode_209, ref logic_uScript_GetAndCheckTechs_techs_209);
		local_EnemyTechsB_TankArray = logic_uScript_GetAndCheckTechs_techs_209;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_209.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_209.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_209.AllDead;
		if (allAlive)
		{
			Relay_In_241();
		}
		if (someAlive)
		{
			Relay_In_241();
		}
		if (allDead)
		{
			Relay_True_235();
		}
	}

	private void Relay_In_210()
	{
		int num = 0;
		Array array = enemyTechDataA;
		if (logic_uScript_GetAndCheckTechs_techData_210.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_210, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_210, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_210 = owner_Connection_257;
		int num2 = 0;
		Array array2 = local_EnemyTechsA_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_210.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_210, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_210, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_210 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_210.In(logic_uScript_GetAndCheckTechs_techData_210, logic_uScript_GetAndCheckTechs_ownerNode_210, ref logic_uScript_GetAndCheckTechs_techs_210);
		local_EnemyTechsA_TankArray = logic_uScript_GetAndCheckTechs_techs_210;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_210.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_210.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_210.AllDead;
		if (allAlive)
		{
			Relay_In_238();
		}
		if (someAlive)
		{
			Relay_In_238();
		}
		if (allDead)
		{
			Relay_True_234();
		}
	}

	private void Relay_In_216()
	{
		logic_uScriptCon_CompareBool_Bool_216 = local_TechsDeadB_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_216.In(logic_uScriptCon_CompareBool_Bool_216);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_216.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_216.False;
		if (num)
		{
			Relay_In_252();
		}
		if (flag)
		{
			Relay_In_259();
		}
	}

	private void Relay_True_222()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.True(out logic_uScriptAct_SetBool_Target_222);
		local_TechsDeadBandA_System_Boolean = logic_uScriptAct_SetBool_Target_222;
	}

	private void Relay_False_222()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.False(out logic_uScriptAct_SetBool_Target_222);
		local_TechsDeadBandA_System_Boolean = logic_uScriptAct_SetBool_Target_222;
	}

	private void Relay_In_223()
	{
		logic_uScriptCon_CompareBool_Bool_223 = local_TechsDeadA_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.In(logic_uScriptCon_CompareBool_Bool_223);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.False;
		if (num)
		{
			Relay_In_252();
		}
		if (flag)
		{
			Relay_In_259();
		}
	}

	private void Relay_In_226()
	{
		logic_uScriptCon_CompareBool_Bool_226 = local_EnemySpottedB_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_226.In(logic_uScriptCon_CompareBool_Bool_226);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_226.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_226.False;
		if (num)
		{
			Relay_In_259();
		}
		if (flag)
		{
			Relay_In_296();
		}
	}

	private void Relay_True_229()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_229.True(out logic_uScriptAct_SetBool_Target_229);
		local_TriggerHit1_System_Boolean = logic_uScriptAct_SetBool_Target_229;
	}

	private void Relay_False_229()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_229.False(out logic_uScriptAct_SetBool_Target_229);
		local_TriggerHit1_System_Boolean = logic_uScriptAct_SetBool_Target_229;
	}

	private void Relay_True_234()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_234.True(out logic_uScriptAct_SetBool_Target_234);
		local_TechsDeadA_System_Boolean = logic_uScriptAct_SetBool_Target_234;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_234.SetTrue)
		{
			Relay_In_216();
		}
	}

	private void Relay_False_234()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_234.False(out logic_uScriptAct_SetBool_Target_234);
		local_TechsDeadA_System_Boolean = logic_uScriptAct_SetBool_Target_234;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_234.SetTrue)
		{
			Relay_In_216();
		}
	}

	private void Relay_True_235()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_235.True(out logic_uScriptAct_SetBool_Target_235);
		local_TechsDeadB_System_Boolean = logic_uScriptAct_SetBool_Target_235;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_235.SetTrue)
		{
			Relay_In_223();
		}
	}

	private void Relay_False_235()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_235.False(out logic_uScriptAct_SetBool_Target_235);
		local_TechsDeadB_System_Boolean = logic_uScriptAct_SetBool_Target_235;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_235.SetTrue)
		{
			Relay_In_223();
		}
	}

	private void Relay_In_236()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_236 = Trigger1;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_236.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_236);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_236.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_236.OutOfRange;
		if (inRange)
		{
			Relay_InitialSpawn_240();
		}
		if (outOfRange)
		{
			Relay_In_291();
		}
	}

	private void Relay_In_238()
	{
		int num = 0;
		Array array = local_EnemyTechsA_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_238.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_238, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_238, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_range_238 = distEnemiesSpottedMinions;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_238.In(logic_uScript_InRangeOfAtLeastOneTech_techs_238, logic_uScript_InRangeOfAtLeastOneTech_range_238);
		bool inRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_238.InRange;
		bool outOfRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_238.OutOfRange;
		if (inRange)
		{
			Relay_In_250();
		}
		if (outOfRange)
		{
			Relay_In_259();
		}
	}

	private void Relay_InitialSpawn_240()
	{
		int num = 0;
		Array array = enemyTechDataB;
		if (logic_uScript_SpawnTechsFromData_spawnData_240.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_240, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_240, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_240 = owner_Connection_231;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_240.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_240, logic_uScript_SpawnTechsFromData_ownerNode_240, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_240);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_240.Out)
		{
			Relay_InitialSpawn_247();
		}
	}

	private void Relay_In_241()
	{
		int num = 0;
		Array array = local_EnemyTechsB_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_241.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_241, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_241, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_range_241 = distEnemiesSpottedMinions;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_241.In(logic_uScript_InRangeOfAtLeastOneTech_techs_241, logic_uScript_InRangeOfAtLeastOneTech_range_241);
		bool inRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_241.InRange;
		bool outOfRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_241.OutOfRange;
		if (inRange)
		{
			Relay_In_226();
		}
		if (outOfRange)
		{
			Relay_In_259();
		}
	}

	private void Relay_In_243()
	{
		int num = 0;
		Array array = msgEnemySpottedA;
		if (logic_uScript_AddOnScreenMessage_locString_243.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_243, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_243, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_243 = local_294_System_String;
		logic_uScript_AddOnScreenMessage_speaker_243 = messageSpeakerMinion;
		logic_uScript_AddOnScreenMessage_Return_243 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_243.In(logic_uScript_AddOnScreenMessage_locString_243, logic_uScript_AddOnScreenMessage_msgPriority_243, logic_uScript_AddOnScreenMessage_holdMsg_243, logic_uScript_AddOnScreenMessage_tag_243, logic_uScript_AddOnScreenMessage_speaker_243, logic_uScript_AddOnScreenMessage_side_243);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_243.Out)
		{
			Relay_True_246();
		}
	}

	private void Relay_In_245()
	{
		int num = 0;
		Array array = msgEnemySpottedB;
		if (logic_uScript_AddOnScreenMessage_locString_245.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_245, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_245, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_245 = local_295_System_String;
		logic_uScript_AddOnScreenMessage_speaker_245 = messageSpeakerMinion;
		logic_uScript_AddOnScreenMessage_Return_245 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_245.In(logic_uScript_AddOnScreenMessage_locString_245, logic_uScript_AddOnScreenMessage_msgPriority_245, logic_uScript_AddOnScreenMessage_holdMsg_245, logic_uScript_AddOnScreenMessage_tag_245, logic_uScript_AddOnScreenMessage_speaker_245, logic_uScript_AddOnScreenMessage_side_245);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_245.Out)
		{
			Relay_True_258();
		}
	}

	private void Relay_True_246()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_246.True(out logic_uScriptAct_SetBool_Target_246);
		local_EnemySpottedA_System_Boolean = logic_uScriptAct_SetBool_Target_246;
	}

	private void Relay_False_246()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_246.False(out logic_uScriptAct_SetBool_Target_246);
		local_EnemySpottedA_System_Boolean = logic_uScriptAct_SetBool_Target_246;
	}

	private void Relay_InitialSpawn_247()
	{
		int num = 0;
		Array array = enemyTechDataA;
		if (logic_uScript_SpawnTechsFromData_spawnData_247.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_247, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_247, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_247 = owner_Connection_205;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_247.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_247, logic_uScript_SpawnTechsFromData_ownerNode_247, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_247);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_247.Out)
		{
			Relay_True_229();
		}
	}

	private void Relay_In_250()
	{
		logic_uScriptCon_CompareBool_Bool_250 = local_EnemySpottedA_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_250.In(logic_uScriptCon_CompareBool_Bool_250);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_250.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_250.False;
		if (num)
		{
			Relay_In_259();
		}
		if (flag)
		{
			Relay_In_298();
		}
	}

	private void Relay_In_251()
	{
		logic_uScriptCon_CompareBool_Bool_251 = local_TriggerHit1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_251.In(logic_uScriptCon_CompareBool_Bool_251);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_251.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_251.False;
		if (num)
		{
			Relay_In_210();
			Relay_In_209();
		}
		if (flag)
		{
			Relay_In_236();
		}
	}

	private void Relay_In_252()
	{
		logic_uScriptCon_CompareBool_Bool_252 = local_TechsDeadBandA_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_252.In(logic_uScriptCon_CompareBool_Bool_252);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_252.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_252.False;
		if (num)
		{
			Relay_In_259();
		}
		if (flag)
		{
			Relay_In_381();
		}
	}

	private void Relay_In_253()
	{
		int num = 0;
		Array array = msgTechsDeadBandA;
		if (logic_uScript_AddOnScreenMessage_locString_253.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_253, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_253, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_253 = local_293_System_String;
		logic_uScript_AddOnScreenMessage_speaker_253 = messageSpeakerNPC;
		logic_uScript_AddOnScreenMessage_Return_253 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_253.In(logic_uScript_AddOnScreenMessage_locString_253, logic_uScript_AddOnScreenMessage_msgPriority_253, logic_uScript_AddOnScreenMessage_holdMsg_253, logic_uScript_AddOnScreenMessage_tag_253, logic_uScript_AddOnScreenMessage_speaker_253, logic_uScript_AddOnScreenMessage_side_253);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_253.Out)
		{
			Relay_True_222();
		}
	}

	private void Relay_True_258()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_258.True(out logic_uScriptAct_SetBool_Target_258);
		local_EnemySpottedB_System_Boolean = logic_uScriptAct_SetBool_Target_258;
	}

	private void Relay_False_258()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_258.False(out logic_uScriptAct_SetBool_Target_258);
		local_EnemySpottedB_System_Boolean = logic_uScriptAct_SetBool_Target_258;
	}

	private void Relay_In_259()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_259.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_259.Out)
		{
			Relay_In_315();
		}
	}

	private void Relay_Save_Out_261()
	{
		Relay_Save_346();
	}

	private void Relay_Load_Out_261()
	{
		Relay_Load_346();
	}

	private void Relay_Restart_Out_261()
	{
		Relay_Set_False_346();
	}

	private void Relay_Save_261()
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = local_TechsDeadBandA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_261 = local_TechsDeadBandA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Save(ref logic_SubGraph_SaveLoadBool_boolean_261, logic_SubGraph_SaveLoadBool_boolAsVariable_261, logic_SubGraph_SaveLoadBool_uniqueID_261);
	}

	private void Relay_Load_261()
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = local_TechsDeadBandA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_261 = local_TechsDeadBandA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Load(ref logic_SubGraph_SaveLoadBool_boolean_261, logic_SubGraph_SaveLoadBool_boolAsVariable_261, logic_SubGraph_SaveLoadBool_uniqueID_261);
	}

	private void Relay_Set_True_261()
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = local_TechsDeadBandA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_261 = local_TechsDeadBandA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_261, logic_SubGraph_SaveLoadBool_boolAsVariable_261, logic_SubGraph_SaveLoadBool_uniqueID_261);
	}

	private void Relay_Set_False_261()
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = local_TechsDeadBandA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_261 = local_TechsDeadBandA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_261, logic_SubGraph_SaveLoadBool_boolAsVariable_261, logic_SubGraph_SaveLoadBool_uniqueID_261);
	}

	private void Relay_In_262()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_262 = local_369_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_262.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_262, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_262);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_262.Out)
		{
			Relay_In_334();
		}
	}

	private void Relay_In_263()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_263 = local_361_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_263.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_263, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_263);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_263.Out)
		{
			Relay_In_363();
		}
	}

	private void Relay_In_264()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_264 = local_265_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_264.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_264, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_264);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_264.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_269()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_269 = local_268_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_269.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_269, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_269);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_269.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_270()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_270 = local_271_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_270.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_270, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_270);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_270.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_273()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_273 = local_272_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_273.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_273, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_273);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_273.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_276()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_276 = local_277_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_276.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_276, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_276);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_276.Out)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_279()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_279 = local_278_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_279.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_279, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_279);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_279.Out)
		{
			Relay_In_99();
		}
	}

	private void Relay_In_281()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_281 = local_280_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_281.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_281, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_281);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_281.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_282()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_282 = local_283_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_282.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_282, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_282);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_282.Out)
		{
			Relay_In_200();
		}
	}

	private void Relay_In_285()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_285 = local_286_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_285.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_285, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_285);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_285.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_289()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_289 = local_288_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_289.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_289, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_289);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_289.Out)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_291()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_291 = local_292_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_291.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_291, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_291);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_291.Out)
		{
			Relay_In_259();
		}
	}

	private void Relay_In_296()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_296 = local_297_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_296.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_296, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_296);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_296.Out)
		{
			Relay_In_245();
		}
	}

	private void Relay_In_298()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_298 = local_299_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_298.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_298, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_298);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_298.Out)
		{
			Relay_In_243();
		}
	}

	private void Relay_In_301()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_301 = local_300_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_301.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_301, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_301);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_301.Out)
		{
			Relay_In_253();
		}
	}

	private void Relay_AtIndex_303()
	{
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_AccessListTech_techList_303.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_303, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_303, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_303.AtIndex(ref logic_uScript_AccessListTech_techList_303, logic_uScript_AccessListTech_index_303, out logic_uScript_AccessListTech_value_303);
		local_NPCTanks_TankArray = logic_uScript_AccessListTech_techList_303;
		local_NPCTank_Tank = logic_uScript_AccessListTech_value_303;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_303.Out)
		{
			Relay_In_311();
		}
	}

	private void Relay_True_307()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_307.True(out logic_uScriptAct_SetBool_Target_307);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_307;
	}

	private void Relay_False_307()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_307.False(out logic_uScriptAct_SetBool_Target_307);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_307;
	}

	private void Relay_True_308()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_308.True(out logic_uScriptAct_SetBool_Target_308);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_308;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_308.Out)
		{
			Relay_In_396();
		}
	}

	private void Relay_False_308()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_308.False(out logic_uScriptAct_SetBool_Target_308);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_308;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_308.Out)
		{
			Relay_In_396();
		}
	}

	private void Relay_True_309()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_309.True(out logic_uScriptAct_SetBool_Target_309);
		local_NPCIgnored_System_Boolean = logic_uScriptAct_SetBool_Target_309;
	}

	private void Relay_False_309()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_309.False(out logic_uScriptAct_SetBool_Target_309);
		local_NPCIgnored_System_Boolean = logic_uScriptAct_SetBool_Target_309;
	}

	private void Relay_In_311()
	{
		logic_uScript_SetTankInvulnerable_tank_311 = local_NPCTank_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_311.In(logic_uScript_SetTankInvulnerable_invulnerable_311, logic_uScript_SetTankInvulnerable_tank_311);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_311.Out)
		{
			Relay_In_333();
		}
	}

	private void Relay_In_313()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_313.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_313, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_313, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_313 = owner_Connection_318;
		int num2 = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_313.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_313, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_313, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_313 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_313.In(logic_uScript_GetAndCheckTechs_techData_313, logic_uScript_GetAndCheckTechs_ownerNode_313, ref logic_uScript_GetAndCheckTechs_techs_313);
		local_NPCTanks_TankArray = logic_uScript_GetAndCheckTechs_techs_313;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_313.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_313.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_303();
		}
		if (someAlive)
		{
			Relay_AtIndex_303();
		}
	}

	private void Relay_In_315()
	{
		logic_uScriptCon_CompareBool_Bool_315 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315.In(logic_uScriptCon_CompareBool_Bool_315);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315.False;
		if (num)
		{
			Relay_In_399();
		}
		if (flag)
		{
			Relay_In_438();
		}
	}

	private void Relay_In_319()
	{
		logic_uScript_InRangeOfTech_tank_319 = local_NPCTank_Tank;
		logic_uScript_InRangeOfTech_range_319 = distAtWhichNPCTechFound;
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_319.In(logic_uScript_InRangeOfTech_tank_319, logic_uScript_InRangeOfTech_range_319);
		bool inRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_319.InRange;
		bool outOfRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_319.OutOfRange;
		if (inRange)
		{
			Relay_True_308();
		}
		if (outOfRange)
		{
			Relay_In_324();
		}
	}

	private void Relay_In_320()
	{
		logic_uScript_FlyTechUpAndAway_tech_320 = local_NPCTank_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_320 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_320 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_320.In(logic_uScript_FlyTechUpAndAway_tech_320, logic_uScript_FlyTechUpAndAway_maxLifetime_320, logic_uScript_FlyTechUpAndAway_targetHeight_320, logic_uScript_FlyTechUpAndAway_aiTree_320, logic_uScript_FlyTechUpAndAway_removalParticles_320);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_320.Out)
		{
			Relay_True_307();
		}
	}

	private void Relay_In_321()
	{
		int num = 0;
		Array array = msgNPCGreeting;
		if (logic_uScript_AddOnScreenMessage_locString_321.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_321, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_321, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_321 = local_314_System_String;
		logic_uScript_AddOnScreenMessage_speaker_321 = messageSpeakerNPC;
		logic_uScript_AddOnScreenMessage_Return_321 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_321.In(logic_uScript_AddOnScreenMessage_locString_321, logic_uScript_AddOnScreenMessage_msgPriority_321, logic_uScript_AddOnScreenMessage_holdMsg_321, logic_uScript_AddOnScreenMessage_tag_321, logic_uScript_AddOnScreenMessage_speaker_321, logic_uScript_AddOnScreenMessage_side_321);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_321.Shown)
		{
			Relay_In_320();
		}
	}

	private void Relay_In_323()
	{
		int num = 0;
		Array array = msgNPCGreetingInturrupt;
		if (logic_uScript_AddOnScreenMessage_locString_323.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_323, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_323, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_323 = messageSpeakerNPC;
		logic_uScript_AddOnScreenMessage_Return_323 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_323.In(logic_uScript_AddOnScreenMessage_locString_323, logic_uScript_AddOnScreenMessage_msgPriority_323, logic_uScript_AddOnScreenMessage_holdMsg_323, logic_uScript_AddOnScreenMessage_tag_323, logic_uScript_AddOnScreenMessage_speaker_323, logic_uScript_AddOnScreenMessage_side_323);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_323.Shown)
		{
			Relay_In_320();
		}
	}

	private void Relay_In_324()
	{
		logic_uScriptCon_CompareBool_Bool_324 = local_NPCSeen_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_324.In(logic_uScriptCon_CompareBool_Bool_324);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_324.True)
		{
			Relay_True_309();
		}
	}

	private void Relay_In_327()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_327 = local_306_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_327.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_327, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_327);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_327.Out)
		{
			Relay_In_323();
		}
	}

	private void Relay_In_333()
	{
		logic_uScriptCon_CompareBool_Bool_333 = local_NPCIgnored_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_333.In(logic_uScriptCon_CompareBool_Bool_333);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_333.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_333.False;
		if (num)
		{
			Relay_In_327();
		}
		if (flag)
		{
			Relay_In_319();
		}
	}

	private void Relay_In_334()
	{
		logic_uScript_FlyTechUpAndAway_tech_334 = local_NPCTank_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_334 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_334 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_334.In(logic_uScript_FlyTechUpAndAway_tech_334, logic_uScript_FlyTechUpAndAway_maxLifetime_334, logic_uScript_FlyTechUpAndAway_targetHeight_334, logic_uScript_FlyTechUpAndAway_aiTree_334, logic_uScript_FlyTechUpAndAway_removalParticles_334);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_334.Out)
		{
			Relay_In_507();
		}
	}

	private void Relay_In_338()
	{
		logic_uScriptCon_CompareBool_Bool_338 = local_NPCTechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_338.In(logic_uScriptCon_CompareBool_Bool_338);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_338.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_338.False;
		if (num)
		{
			Relay_In_18();
		}
		if (flag)
		{
			Relay_InitialSpawn_340();
		}
	}

	private void Relay_InitialSpawn_340()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_340.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_340, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_340, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_340 = owner_Connection_342;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_340.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_340, logic_uScript_SpawnTechsFromData_ownerNode_340, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_340);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_340.Out)
		{
			Relay_True_341();
		}
	}

	private void Relay_True_341()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_341.True(out logic_uScriptAct_SetBool_Target_341);
		local_NPCTechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_341;
	}

	private void Relay_False_341()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_341.False(out logic_uScriptAct_SetBool_Target_341);
		local_NPCTechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_341;
	}

	private void Relay_Save_Out_346()
	{
		Relay_Save_355();
	}

	private void Relay_Load_Out_346()
	{
		Relay_Load_355();
	}

	private void Relay_Restart_Out_346()
	{
		Relay_Set_False_355();
	}

	private void Relay_Save_346()
	{
		logic_SubGraph_SaveLoadBool_boolean_346 = local_NPCTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_346 = local_NPCTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Save(ref logic_SubGraph_SaveLoadBool_boolean_346, logic_SubGraph_SaveLoadBool_boolAsVariable_346, logic_SubGraph_SaveLoadBool_uniqueID_346);
	}

	private void Relay_Load_346()
	{
		logic_SubGraph_SaveLoadBool_boolean_346 = local_NPCTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_346 = local_NPCTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Load(ref logic_SubGraph_SaveLoadBool_boolean_346, logic_SubGraph_SaveLoadBool_boolAsVariable_346, logic_SubGraph_SaveLoadBool_uniqueID_346);
	}

	private void Relay_Set_True_346()
	{
		logic_SubGraph_SaveLoadBool_boolean_346 = local_NPCTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_346 = local_NPCTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_346, logic_SubGraph_SaveLoadBool_boolAsVariable_346, logic_SubGraph_SaveLoadBool_uniqueID_346);
	}

	private void Relay_Set_False_346()
	{
		logic_SubGraph_SaveLoadBool_boolean_346 = local_NPCTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_346 = local_NPCTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_346.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_346, logic_SubGraph_SaveLoadBool_boolAsVariable_346, logic_SubGraph_SaveLoadBool_uniqueID_346);
	}

	private void Relay_InitialSpawn_348()
	{
		int num = 0;
		Array array = enemyTechDataG;
		if (logic_uScript_SpawnTechsFromData_spawnData_348.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_348, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_348, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_348 = owner_Connection_347;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_348.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_348, logic_uScript_SpawnTechsFromData_ownerNode_348, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_348);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_348.Out)
		{
			Relay_True_40();
		}
	}

	private void Relay_In_350()
	{
		logic_uScript_SetTankTeam_tank_350 = local_BossTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_350.In(logic_uScript_SetTankTeam_tank_350, logic_uScript_SetTankTeam_team_350);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_350.Out)
		{
			Relay_In_377();
		}
	}

	private void Relay_Save_Out_355()
	{
		Relay_Save_356();
	}

	private void Relay_Load_Out_355()
	{
		Relay_Load_356();
	}

	private void Relay_Restart_Out_355()
	{
		Relay_Set_False_356();
	}

	private void Relay_Save_355()
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_355 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Save(ref logic_SubGraph_SaveLoadBool_boolean_355, logic_SubGraph_SaveLoadBool_boolAsVariable_355, logic_SubGraph_SaveLoadBool_uniqueID_355);
	}

	private void Relay_Load_355()
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_355 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Load(ref logic_SubGraph_SaveLoadBool_boolean_355, logic_SubGraph_SaveLoadBool_boolAsVariable_355, logic_SubGraph_SaveLoadBool_uniqueID_355);
	}

	private void Relay_Set_True_355()
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_355 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_355, logic_SubGraph_SaveLoadBool_boolAsVariable_355, logic_SubGraph_SaveLoadBool_uniqueID_355);
	}

	private void Relay_Set_False_355()
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_355 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_355, logic_SubGraph_SaveLoadBool_boolAsVariable_355, logic_SubGraph_SaveLoadBool_uniqueID_355);
	}

	private void Relay_Save_Out_356()
	{
		Relay_Save_357();
	}

	private void Relay_Load_Out_356()
	{
		Relay_Load_357();
	}

	private void Relay_Restart_Out_356()
	{
		Relay_Set_False_357();
	}

	private void Relay_Save_356()
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_356 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Save(ref logic_SubGraph_SaveLoadBool_boolean_356, logic_SubGraph_SaveLoadBool_boolAsVariable_356, logic_SubGraph_SaveLoadBool_uniqueID_356);
	}

	private void Relay_Load_356()
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_356 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Load(ref logic_SubGraph_SaveLoadBool_boolean_356, logic_SubGraph_SaveLoadBool_boolAsVariable_356, logic_SubGraph_SaveLoadBool_uniqueID_356);
	}

	private void Relay_Set_True_356()
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_356 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_356, logic_SubGraph_SaveLoadBool_boolAsVariable_356, logic_SubGraph_SaveLoadBool_uniqueID_356);
	}

	private void Relay_Set_False_356()
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_356 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_356, logic_SubGraph_SaveLoadBool_boolAsVariable_356, logic_SubGraph_SaveLoadBool_uniqueID_356);
	}

	private void Relay_Save_Out_357()
	{
		Relay_Save_427();
	}

	private void Relay_Load_Out_357()
	{
		Relay_Load_427();
	}

	private void Relay_Restart_Out_357()
	{
		Relay_Set_False_427();
	}

	private void Relay_Save_357()
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_357 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Save(ref logic_SubGraph_SaveLoadBool_boolean_357, logic_SubGraph_SaveLoadBool_boolAsVariable_357, logic_SubGraph_SaveLoadBool_uniqueID_357);
	}

	private void Relay_Load_357()
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_357 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Load(ref logic_SubGraph_SaveLoadBool_boolean_357, logic_SubGraph_SaveLoadBool_boolAsVariable_357, logic_SubGraph_SaveLoadBool_uniqueID_357);
	}

	private void Relay_Set_True_357()
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_357 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_357, logic_SubGraph_SaveLoadBool_boolAsVariable_357, logic_SubGraph_SaveLoadBool_uniqueID_357);
	}

	private void Relay_Set_False_357()
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_357 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_357, logic_SubGraph_SaveLoadBool_boolAsVariable_357, logic_SubGraph_SaveLoadBool_uniqueID_357);
	}

	private void Relay_In_363()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_363 = local_362_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_363.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_363, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_363);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_363.Out)
		{
			Relay_In_365();
		}
	}

	private void Relay_In_365()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_365 = local_364_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_365.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_365, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_365);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_365.Out)
		{
			Relay_In_366();
		}
	}

	private void Relay_In_366()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_366 = local_367_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_366.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_366, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_366);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_366.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_InitialSpawn_370()
	{
		int num = 0;
		Array array = enemyTechDataChargingPoint;
		if (logic_uScript_SpawnTechsFromData_spawnData_370.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_370, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_370, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_370 = owner_Connection_371;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_370.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_370, logic_uScript_SpawnTechsFromData_ownerNode_370, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_370);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_370.Out)
		{
			Relay_True_442();
		}
	}

	private void Relay_In_374()
	{
		int num = 0;
		Array array = enemyTechDataChargingPoint;
		if (logic_uScript_GetAndCheckTechs_techData_374.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_374, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_374, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_374 = owner_Connection_373;
		int num2 = 0;
		Array array2 = local_EnemyTechsChargingPoint_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_374.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_374, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_374, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_374 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_374.In(logic_uScript_GetAndCheckTechs_techData_374, logic_uScript_GetAndCheckTechs_ownerNode_374, ref logic_uScript_GetAndCheckTechs_techs_374);
		local_EnemyTechsChargingPoint_TankArray = logic_uScript_GetAndCheckTechs_techs_374;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_374.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_374.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_378();
		}
		if (someAlive)
		{
			Relay_AtIndex_378();
		}
	}

	private void Relay_In_377()
	{
		logic_uScript_SetTankTeam_tank_377 = local_ChargingTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_377.In(logic_uScript_SetTankTeam_tank_377, logic_uScript_SetTankTeam_team_377);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_377.Out)
		{
			Relay_InitialSpawn_348();
		}
	}

	private void Relay_AtIndex_378()
	{
		int num = 0;
		Array array = local_EnemyTechsChargingPoint_TankArray;
		if (logic_uScript_AccessListTech_techList_378.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_378, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_378, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_378.AtIndex(ref logic_uScript_AccessListTech_techList_378, logic_uScript_AccessListTech_index_378, out logic_uScript_AccessListTech_value_378);
		local_EnemyTechsChargingPoint_TankArray = logic_uScript_AccessListTech_techList_378;
		local_ChargingTech_Tank = logic_uScript_AccessListTech_value_378;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_378.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_381()
	{
		logic_uScriptCon_CompareBool_Bool_381 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_381.In(logic_uScriptCon_CompareBool_Bool_381);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_381.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_381.False;
		if (num)
		{
			Relay_In_518();
		}
		if (flag)
		{
			Relay_True_222();
		}
	}

	private void Relay_In_383()
	{
		logic_uScriptCon_CompareBool_Bool_383 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_383.In(logic_uScriptCon_CompareBool_Bool_383);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_383.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_383.False;
		if (num)
		{
			Relay_In_513();
		}
		if (flag)
		{
			Relay_True_151();
		}
	}

	private void Relay_In_385()
	{
		logic_uScriptCon_CompareBool_Bool_385 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_385.In(logic_uScriptCon_CompareBool_Bool_385);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_385.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_385.False;
		if (num)
		{
			Relay_In_279();
		}
		if (flag)
		{
			Relay_True_117();
		}
	}

	private void Relay_In_387()
	{
		logic_uScriptCon_CompareBool_Bool_387 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_387.In(logic_uScriptCon_CompareBool_Bool_387);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_387.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_387.False;
		if (num)
		{
			Relay_In_269();
		}
		if (flag)
		{
			Relay_True_88();
		}
	}

	private void Relay_In_389()
	{
		logic_uScriptCon_CompareBool_Bool_389 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_389.In(logic_uScriptCon_CompareBool_Bool_389);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_389.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_389.False;
		if (num)
		{
			Relay_In_20();
		}
		if (flag)
		{
			Relay_In_392();
		}
	}

	private void Relay_In_392()
	{
		int num = 0;
		Array array = msgMissionCompleteGeneric;
		if (logic_uScript_AddOnScreenMessage_locString_392.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_392, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_392, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_392 = messageSpeakerGeneric;
		logic_uScript_AddOnScreenMessage_Return_392 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_392.In(logic_uScript_AddOnScreenMessage_locString_392, logic_uScript_AddOnScreenMessage_msgPriority_392, logic_uScript_AddOnScreenMessage_holdMsg_392, logic_uScript_AddOnScreenMessage_tag_392, logic_uScript_AddOnScreenMessage_speaker_392, logic_uScript_AddOnScreenMessage_side_392);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_392.Shown)
		{
			Relay_True_27();
		}
	}

	private void Relay_In_394()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_394 = TriggerA;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_394.In(logic_uScript_SetEncounterTargetPosition_positionName_394);
	}

	private void Relay_In_396()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_396 = owner_Connection_397;
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_396.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_396, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_396, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_396 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_396.In(logic_uScript_SetOneTechAsEncounterTarget_owner_396, logic_uScript_SetOneTechAsEncounterTarget_techs_396);
		local_NPCTank_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_396;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_396.Out)
		{
			Relay_In_321();
		}
	}

	private void Relay_In_399()
	{
		logic_uScriptCon_CompareBool_Bool_399 = local_TriggerHitA_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_399.In(logic_uScriptCon_CompareBool_Bool_399);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_399.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_399.False;
		if (num)
		{
			Relay_True_403();
		}
		if (flag)
		{
			Relay_In_401();
		}
	}

	private void Relay_In_401()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_401 = TriggerA;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_401.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_401);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_401.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_401.OutOfRange;
		if (inRange)
		{
			Relay_True_403();
		}
		if (outOfRange)
		{
			Relay_In_394();
		}
	}

	private void Relay_True_403()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_403.True(out logic_uScriptAct_SetBool_Target_403);
		local_TriggerHitA_System_Boolean = logic_uScriptAct_SetBool_Target_403;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_403.Out)
		{
			Relay_In_406();
		}
	}

	private void Relay_False_403()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_403.False(out logic_uScriptAct_SetBool_Target_403);
		local_TriggerHitA_System_Boolean = logic_uScriptAct_SetBool_Target_403;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_403.Out)
		{
			Relay_In_406();
		}
	}

	private void Relay_True_405()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_405.True(out logic_uScriptAct_SetBool_Target_405);
		local_TriggerHitB_System_Boolean = logic_uScriptAct_SetBool_Target_405;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_405.Out)
		{
			Relay_In_415();
		}
	}

	private void Relay_False_405()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_405.False(out logic_uScriptAct_SetBool_Target_405);
		local_TriggerHitB_System_Boolean = logic_uScriptAct_SetBool_Target_405;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_405.Out)
		{
			Relay_In_415();
		}
	}

	private void Relay_In_406()
	{
		logic_uScriptCon_CompareBool_Bool_406 = local_TriggerHitB_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_406.In(logic_uScriptCon_CompareBool_Bool_406);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_406.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_406.False;
		if (num)
		{
			Relay_True_405();
		}
		if (flag)
		{
			Relay_In_410();
		}
	}

	private void Relay_In_409()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_409 = TriggerB;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_409.In(logic_uScript_SetEncounterTargetPosition_positionName_409);
	}

	private void Relay_In_410()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_410 = TriggerB;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_410.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_410);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_410.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_410.OutOfRange;
		if (inRange)
		{
			Relay_In_470();
		}
		if (outOfRange)
		{
			Relay_In_409();
		}
	}

	private void Relay_In_412()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_412 = TriggerC;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_412.In(logic_uScript_SetEncounterTargetPosition_positionName_412);
	}

	private void Relay_In_413()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_413 = TriggerC;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_413.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_413);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_413.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_413.OutOfRange;
		if (inRange)
		{
			Relay_In_477();
		}
		if (outOfRange)
		{
			Relay_In_412();
		}
	}

	private void Relay_In_415()
	{
		logic_uScriptCon_CompareBool_Bool_415 = local_TriggerHitC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_415.In(logic_uScriptCon_CompareBool_Bool_415);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_415.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_415.False;
		if (num)
		{
			Relay_True_418();
		}
		if (flag)
		{
			Relay_In_413();
		}
	}

	private void Relay_True_418()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_418.True(out logic_uScriptAct_SetBool_Target_418);
		local_TriggerHitC_System_Boolean = logic_uScriptAct_SetBool_Target_418;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_418.Out)
		{
			Relay_In_421();
		}
	}

	private void Relay_False_418()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_418.False(out logic_uScriptAct_SetBool_Target_418);
		local_TriggerHitC_System_Boolean = logic_uScriptAct_SetBool_Target_418;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_418.Out)
		{
			Relay_In_421();
		}
	}

	private void Relay_In_419()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_419 = TriggerD;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_419.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_419);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_419.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_419.OutOfRange;
		if (inRange)
		{
			Relay_In_482();
		}
		if (outOfRange)
		{
			Relay_In_422();
		}
	}

	private void Relay_True_420()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_420.True(out logic_uScriptAct_SetBool_Target_420);
		local_TriggerHitD_System_Boolean = logic_uScriptAct_SetBool_Target_420;
	}

	private void Relay_False_420()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_420.False(out logic_uScriptAct_SetBool_Target_420);
		local_TriggerHitD_System_Boolean = logic_uScriptAct_SetBool_Target_420;
	}

	private void Relay_In_421()
	{
		logic_uScriptCon_CompareBool_Bool_421 = local_TriggerHitD_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_421.In(logic_uScriptCon_CompareBool_Bool_421);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_421.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_421.False;
		if (num)
		{
			Relay_True_420();
		}
		if (flag)
		{
			Relay_In_419();
		}
	}

	private void Relay_In_422()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_422 = TriggerD;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_422.In(logic_uScript_SetEncounterTargetPosition_positionName_422);
	}

	private void Relay_Save_Out_427()
	{
		Relay_Save_429();
	}

	private void Relay_Load_Out_427()
	{
		Relay_Load_429();
	}

	private void Relay_Restart_Out_427()
	{
		Relay_Set_False_429();
	}

	private void Relay_Save_427()
	{
		logic_SubGraph_SaveLoadBool_boolean_427 = local_TriggerHitA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_427 = local_TriggerHitA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Save(ref logic_SubGraph_SaveLoadBool_boolean_427, logic_SubGraph_SaveLoadBool_boolAsVariable_427, logic_SubGraph_SaveLoadBool_uniqueID_427);
	}

	private void Relay_Load_427()
	{
		logic_SubGraph_SaveLoadBool_boolean_427 = local_TriggerHitA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_427 = local_TriggerHitA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Load(ref logic_SubGraph_SaveLoadBool_boolean_427, logic_SubGraph_SaveLoadBool_boolAsVariable_427, logic_SubGraph_SaveLoadBool_uniqueID_427);
	}

	private void Relay_Set_True_427()
	{
		logic_SubGraph_SaveLoadBool_boolean_427 = local_TriggerHitA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_427 = local_TriggerHitA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_427, logic_SubGraph_SaveLoadBool_boolAsVariable_427, logic_SubGraph_SaveLoadBool_uniqueID_427);
	}

	private void Relay_Set_False_427()
	{
		logic_SubGraph_SaveLoadBool_boolean_427 = local_TriggerHitA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_427 = local_TriggerHitA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_427.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_427, logic_SubGraph_SaveLoadBool_boolAsVariable_427, logic_SubGraph_SaveLoadBool_uniqueID_427);
	}

	private void Relay_Save_Out_429()
	{
		Relay_Save_431();
	}

	private void Relay_Load_Out_429()
	{
		Relay_Load_431();
	}

	private void Relay_Restart_Out_429()
	{
		Relay_Set_False_431();
	}

	private void Relay_Save_429()
	{
		logic_SubGraph_SaveLoadBool_boolean_429 = local_TriggerHitB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_429 = local_TriggerHitB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Save(ref logic_SubGraph_SaveLoadBool_boolean_429, logic_SubGraph_SaveLoadBool_boolAsVariable_429, logic_SubGraph_SaveLoadBool_uniqueID_429);
	}

	private void Relay_Load_429()
	{
		logic_SubGraph_SaveLoadBool_boolean_429 = local_TriggerHitB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_429 = local_TriggerHitB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Load(ref logic_SubGraph_SaveLoadBool_boolean_429, logic_SubGraph_SaveLoadBool_boolAsVariable_429, logic_SubGraph_SaveLoadBool_uniqueID_429);
	}

	private void Relay_Set_True_429()
	{
		logic_SubGraph_SaveLoadBool_boolean_429 = local_TriggerHitB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_429 = local_TriggerHitB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_429, logic_SubGraph_SaveLoadBool_boolAsVariable_429, logic_SubGraph_SaveLoadBool_uniqueID_429);
	}

	private void Relay_Set_False_429()
	{
		logic_SubGraph_SaveLoadBool_boolean_429 = local_TriggerHitB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_429 = local_TriggerHitB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_429.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_429, logic_SubGraph_SaveLoadBool_boolAsVariable_429, logic_SubGraph_SaveLoadBool_uniqueID_429);
	}

	private void Relay_Save_Out_431()
	{
		Relay_Save_432();
	}

	private void Relay_Load_Out_431()
	{
		Relay_Load_432();
	}

	private void Relay_Restart_Out_431()
	{
		Relay_Set_False_432();
	}

	private void Relay_Save_431()
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = local_TriggerHitC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_431 = local_TriggerHitC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Save(ref logic_SubGraph_SaveLoadBool_boolean_431, logic_SubGraph_SaveLoadBool_boolAsVariable_431, logic_SubGraph_SaveLoadBool_uniqueID_431);
	}

	private void Relay_Load_431()
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = local_TriggerHitC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_431 = local_TriggerHitC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Load(ref logic_SubGraph_SaveLoadBool_boolean_431, logic_SubGraph_SaveLoadBool_boolAsVariable_431, logic_SubGraph_SaveLoadBool_uniqueID_431);
	}

	private void Relay_Set_True_431()
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = local_TriggerHitC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_431 = local_TriggerHitC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_431, logic_SubGraph_SaveLoadBool_boolAsVariable_431, logic_SubGraph_SaveLoadBool_uniqueID_431);
	}

	private void Relay_Set_False_431()
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = local_TriggerHitC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_431 = local_TriggerHitC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_431, logic_SubGraph_SaveLoadBool_boolAsVariable_431, logic_SubGraph_SaveLoadBool_uniqueID_431);
	}

	private void Relay_Save_Out_432()
	{
		Relay_Save_434();
	}

	private void Relay_Load_Out_432()
	{
		Relay_Load_434();
	}

	private void Relay_Restart_Out_432()
	{
		Relay_Restart_434();
	}

	private void Relay_Save_432()
	{
		logic_SubGraph_SaveLoadBool_boolean_432 = local_TriggerHitD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_432 = local_TriggerHitD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Save(ref logic_SubGraph_SaveLoadBool_boolean_432, logic_SubGraph_SaveLoadBool_boolAsVariable_432, logic_SubGraph_SaveLoadBool_uniqueID_432);
	}

	private void Relay_Load_432()
	{
		logic_SubGraph_SaveLoadBool_boolean_432 = local_TriggerHitD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_432 = local_TriggerHitD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Load(ref logic_SubGraph_SaveLoadBool_boolean_432, logic_SubGraph_SaveLoadBool_boolAsVariable_432, logic_SubGraph_SaveLoadBool_uniqueID_432);
	}

	private void Relay_Set_True_432()
	{
		logic_SubGraph_SaveLoadBool_boolean_432 = local_TriggerHitD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_432 = local_TriggerHitD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_432, logic_SubGraph_SaveLoadBool_boolAsVariable_432, logic_SubGraph_SaveLoadBool_uniqueID_432);
	}

	private void Relay_Set_False_432()
	{
		logic_SubGraph_SaveLoadBool_boolean_432 = local_TriggerHitD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_432 = local_TriggerHitD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_432.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_432, logic_SubGraph_SaveLoadBool_boolAsVariable_432, logic_SubGraph_SaveLoadBool_uniqueID_432);
	}

	private void Relay_Save_Out_434()
	{
		Relay_Save_445();
	}

	private void Relay_Load_Out_434()
	{
		Relay_Load_445();
	}

	private void Relay_Restart_Out_434()
	{
		Relay_Set_False_445();
	}

	private void Relay_Save_434()
	{
		logic_SubGraph_SaveLoadInt_integer_434 = local_ObjectiveStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_434 = local_ObjectiveStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.Save(logic_SubGraph_SaveLoadInt_restartValue_434, ref logic_SubGraph_SaveLoadInt_integer_434, logic_SubGraph_SaveLoadInt_intAsVariable_434, logic_SubGraph_SaveLoadInt_uniqueID_434);
	}

	private void Relay_Load_434()
	{
		logic_SubGraph_SaveLoadInt_integer_434 = local_ObjectiveStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_434 = local_ObjectiveStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.Load(logic_SubGraph_SaveLoadInt_restartValue_434, ref logic_SubGraph_SaveLoadInt_integer_434, logic_SubGraph_SaveLoadInt_intAsVariable_434, logic_SubGraph_SaveLoadInt_uniqueID_434);
	}

	private void Relay_Restart_434()
	{
		logic_SubGraph_SaveLoadInt_integer_434 = local_ObjectiveStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_434 = local_ObjectiveStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_434.Restart(logic_SubGraph_SaveLoadInt_restartValue_434, ref logic_SubGraph_SaveLoadInt_integer_434, logic_SubGraph_SaveLoadInt_intAsVariable_434, logic_SubGraph_SaveLoadInt_uniqueID_434);
	}

	private void Relay_In_436()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_436 = NPCPos;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_436.In(logic_uScript_SetEncounterTargetPosition_positionName_436);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_436.Out)
		{
			Relay_In_313();
		}
	}

	private void Relay_In_438()
	{
		logic_uScriptCon_CompareBool_Bool_438 = local_NPCSeen_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_438.In(logic_uScriptCon_CompareBool_Bool_438);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_438.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_438.False;
		if (num)
		{
			Relay_In_313();
		}
		if (flag)
		{
			Relay_In_436();
		}
	}

	private void Relay_In_440()
	{
		logic_uScriptCon_CompareBool_Bool_440 = local_BossTechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_440.In(logic_uScriptCon_CompareBool_Bool_440);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_440.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_440.False;
		if (num)
		{
			Relay_In_21();
		}
		if (flag)
		{
			Relay_InitialSpawn_31();
		}
	}

	private void Relay_True_442()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_442.True(out logic_uScriptAct_SetBool_Target_442);
		local_BossTechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_442;
	}

	private void Relay_False_442()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_442.False(out logic_uScriptAct_SetBool_Target_442);
		local_BossTechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_442;
	}

	private void Relay_Save_Out_445()
	{
	}

	private void Relay_Load_Out_445()
	{
	}

	private void Relay_Restart_Out_445()
	{
	}

	private void Relay_Save_445()
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = local_BossTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_445 = local_BossTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Save(ref logic_SubGraph_SaveLoadBool_boolean_445, logic_SubGraph_SaveLoadBool_boolAsVariable_445, logic_SubGraph_SaveLoadBool_uniqueID_445);
	}

	private void Relay_Load_445()
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = local_BossTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_445 = local_BossTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Load(ref logic_SubGraph_SaveLoadBool_boolean_445, logic_SubGraph_SaveLoadBool_boolAsVariable_445, logic_SubGraph_SaveLoadBool_uniqueID_445);
	}

	private void Relay_Set_True_445()
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = local_BossTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_445 = local_BossTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_445, logic_SubGraph_SaveLoadBool_boolAsVariable_445, logic_SubGraph_SaveLoadBool_uniqueID_445);
	}

	private void Relay_Set_False_445()
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = local_BossTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_445 = local_BossTechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_445, logic_SubGraph_SaveLoadBool_boolAsVariable_445, logic_SubGraph_SaveLoadBool_uniqueID_445);
	}

	private void Relay_In_446()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_446 = TriggerD;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_446.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_446);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_446.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_446.InRange;
		if (num)
		{
			Relay_In_29();
		}
		if (inRange)
		{
			Relay_In_448();
		}
	}

	private void Relay_In_448()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_448.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_448.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_450()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_450 = EnemyPosH;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_450.In(logic_uScript_SetEncounterTargetPosition_positionName_450);
	}

	private void Relay_In_451()
	{
		int num = 0;
		Array array = local_EnemyTechsG_TankArray;
		if (logic_uScript_DamageTechs_techs_451.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_451, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_451, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_451.In(logic_uScript_DamageTechs_techs_451, logic_uScript_DamageTechs_damagePercentage_451, logic_uScript_DamageTechs_givePlayerCredit_451, logic_uScript_DamageTechs_leaveBlocksPercentage_451);
	}

	private void Relay_In_452()
	{
		int num = 0;
		Array array = enemyTechDataG;
		if (logic_uScript_GetAndCheckTechs_techData_452.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_452, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_452, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_452 = owner_Connection_455;
		int num2 = 0;
		Array array2 = local_EnemyTechsG_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_452.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_452, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_452, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_452 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_452.In(logic_uScript_GetAndCheckTechs_techData_452, logic_uScript_GetAndCheckTechs_ownerNode_452, ref logic_uScript_GetAndCheckTechs_techs_452);
		local_EnemyTechsG_TankArray = logic_uScript_GetAndCheckTechs_techs_452;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_452.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_452.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_452.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_452.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_451();
		}
		if (someAlive)
		{
			Relay_In_451();
		}
		if (allDead)
		{
			Relay_In_456();
		}
		if (waitingToSpawn)
		{
			Relay_In_456();
		}
	}

	private void Relay_In_456()
	{
		int num = 0;
		Array array = enemyTechDataChargingPoint;
		if (logic_uScript_GetAndCheckTechs_techData_456.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_456, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_456, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_456 = owner_Connection_458;
		int num2 = 0;
		Array array2 = local_EnemyTechsChargingPoint_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_456.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_456, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_456, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_456 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_456.In(logic_uScript_GetAndCheckTechs_techData_456, logic_uScript_GetAndCheckTechs_ownerNode_456, ref logic_uScript_GetAndCheckTechs_techs_456);
		local_EnemyTechsChargingPoint_TankArray = logic_uScript_GetAndCheckTechs_techs_456;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_456.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_456.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_456.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_456.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_460();
		}
		if (someAlive)
		{
			Relay_In_460();
		}
		if (allDead)
		{
			Relay_In_389();
		}
		if (waitingToSpawn)
		{
			Relay_In_389();
		}
	}

	private void Relay_In_460()
	{
		int num = 0;
		Array array = local_EnemyTechsChargingPoint_TankArray;
		if (logic_uScript_DamageTechs_techs_460.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_460, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_460, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_460.In(logic_uScript_DamageTechs_techs_460, logic_uScript_DamageTechs_damagePercentage_460, logic_uScript_DamageTechs_givePlayerCredit_460, logic_uScript_DamageTechs_leaveBlocksPercentage_460);
	}

	private void Relay_In_461()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_461.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_461.Out)
		{
			Relay_In_462();
		}
	}

	private void Relay_In_462()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_462.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_462.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_In_467()
	{
		int num = 0;
		Array array = enemyTechDataB;
		if (logic_uScript_GetAndCheckTechs_techData_467.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_467, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_467, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_467 = owner_Connection_465;
		int num2 = 0;
		Array array2 = local_EnemyTechsB_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_467.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_467, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_467, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_467 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_467.In(logic_uScript_GetAndCheckTechs_techData_467, logic_uScript_GetAndCheckTechs_ownerNode_467, ref logic_uScript_GetAndCheckTechs_techs_467);
		local_EnemyTechsB_TankArray = logic_uScript_GetAndCheckTechs_techs_467;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_467.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_467.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_467.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_467.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_469();
		}
		if (someAlive)
		{
			Relay_In_469();
		}
		if (allDead)
		{
			Relay_True_405();
		}
		if (waitingToSpawn)
		{
			Relay_True_405();
		}
	}

	private void Relay_In_469()
	{
		int num = 0;
		Array array = local_EnemyTechsB_TankArray;
		if (logic_uScript_DamageTechs_techs_469.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_469, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_469, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_469.In(logic_uScript_DamageTechs_techs_469, logic_uScript_DamageTechs_damagePercentage_469, logic_uScript_DamageTechs_givePlayerCredit_469, logic_uScript_DamageTechs_leaveBlocksPercentage_469);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_469.Out)
		{
			Relay_True_405();
		}
	}

	private void Relay_In_470()
	{
		int num = 0;
		Array array = enemyTechDataA;
		if (logic_uScript_GetAndCheckTechs_techData_470.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_470, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_470, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_470 = owner_Connection_463;
		int num2 = 0;
		Array array2 = local_EnemyTechsA_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_470.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_470, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_470, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_470 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_470.In(logic_uScript_GetAndCheckTechs_techData_470, logic_uScript_GetAndCheckTechs_ownerNode_470, ref logic_uScript_GetAndCheckTechs_techs_470);
		local_EnemyTechsA_TankArray = logic_uScript_GetAndCheckTechs_techs_470;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_470.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_470.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_470.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_470.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_471();
		}
		if (someAlive)
		{
			Relay_In_471();
		}
		if (allDead)
		{
			Relay_In_467();
		}
		if (waitingToSpawn)
		{
			Relay_In_467();
		}
	}

	private void Relay_In_471()
	{
		int num = 0;
		Array array = local_EnemyTechsA_TankArray;
		if (logic_uScript_DamageTechs_techs_471.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_471, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_471, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_471.In(logic_uScript_DamageTechs_techs_471, logic_uScript_DamageTechs_damagePercentage_471, logic_uScript_DamageTechs_givePlayerCredit_471, logic_uScript_DamageTechs_leaveBlocksPercentage_471);
	}

	private void Relay_In_476()
	{
		int num = 0;
		Array array = local_EnemyTechsC_TankArray;
		if (logic_uScript_DamageTechs_techs_476.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_476, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_476, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_476.In(logic_uScript_DamageTechs_techs_476, logic_uScript_DamageTechs_damagePercentage_476, logic_uScript_DamageTechs_givePlayerCredit_476, logic_uScript_DamageTechs_leaveBlocksPercentage_476);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_476.Out)
		{
			Relay_True_418();
		}
	}

	private void Relay_In_477()
	{
		int num = 0;
		Array array = enemyTechDataC;
		if (logic_uScript_GetAndCheckTechs_techData_477.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_477, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_477, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_477 = owner_Connection_473;
		int num2 = 0;
		Array array2 = local_EnemyTechsC_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_477.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_477, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_477, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_477 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_477.In(logic_uScript_GetAndCheckTechs_techData_477, logic_uScript_GetAndCheckTechs_ownerNode_477, ref logic_uScript_GetAndCheckTechs_techs_477);
		local_EnemyTechsC_TankArray = logic_uScript_GetAndCheckTechs_techs_477;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_477.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_477.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_477.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_477.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_476();
		}
		if (someAlive)
		{
			Relay_In_476();
		}
		if (allDead)
		{
			Relay_True_418();
		}
		if (waitingToSpawn)
		{
			Relay_True_418();
		}
	}

	private void Relay_In_479()
	{
		int num = 0;
		Array array = local_EnemyTechsD_TankArray;
		if (logic_uScript_DamageTechs_techs_479.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_479, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_479, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_479.In(logic_uScript_DamageTechs_techs_479, logic_uScript_DamageTechs_damagePercentage_479, logic_uScript_DamageTechs_givePlayerCredit_479, logic_uScript_DamageTechs_leaveBlocksPercentage_479);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_479.Out)
		{
			Relay_True_420();
		}
	}

	private void Relay_In_482()
	{
		int num = 0;
		Array array = enemyTechDataD;
		if (logic_uScript_GetAndCheckTechs_techData_482.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_482, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_482, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_482 = owner_Connection_480;
		int num2 = 0;
		Array array2 = local_EnemyTechsD_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_482.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_482, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_482, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_482 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_482.In(logic_uScript_GetAndCheckTechs_techData_482, logic_uScript_GetAndCheckTechs_ownerNode_482, ref logic_uScript_GetAndCheckTechs_techs_482);
		local_EnemyTechsD_TankArray = logic_uScript_GetAndCheckTechs_techs_482;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_482.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_482.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_482.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_482.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_479();
		}
		if (someAlive)
		{
			Relay_In_479();
		}
		if (allDead)
		{
			Relay_True_420();
		}
		if (waitingToSpawn)
		{
			Relay_True_420();
		}
	}

	private void Relay_In_487()
	{
		int num = 0;
		Array array = enemyTechDataB;
		if (logic_uScript_GetAndCheckTechs_techData_487.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_487, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_487, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_487 = owner_Connection_485;
		int num2 = 0;
		Array array2 = local_EnemyTechsB_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_487.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_487, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_487, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_487 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487.In(logic_uScript_GetAndCheckTechs_techData_487, logic_uScript_GetAndCheckTechs_ownerNode_487, ref logic_uScript_GetAndCheckTechs_techs_487);
		local_EnemyTechsB_TankArray = logic_uScript_GetAndCheckTechs_techs_487;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_489();
		}
		if (someAlive)
		{
			Relay_In_489();
		}
		if (allDead)
		{
			Relay_In_495();
		}
		if (waitingToSpawn)
		{
			Relay_In_495();
		}
	}

	private void Relay_In_489()
	{
		int num = 0;
		Array array = local_EnemyTechsB_TankArray;
		if (logic_uScript_DamageTechs_techs_489.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_489, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_489, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_489.In(logic_uScript_DamageTechs_techs_489, logic_uScript_DamageTechs_damagePercentage_489, logic_uScript_DamageTechs_givePlayerCredit_489, logic_uScript_DamageTechs_leaveBlocksPercentage_489);
	}

	private void Relay_In_490()
	{
		int num = 0;
		Array array = enemyTechDataA;
		if (logic_uScript_GetAndCheckTechs_techData_490.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_490, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_490, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_490 = owner_Connection_483;
		int num2 = 0;
		Array array2 = local_EnemyTechsA_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_490.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_490, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_490, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_490 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_490.In(logic_uScript_GetAndCheckTechs_techData_490, logic_uScript_GetAndCheckTechs_ownerNode_490, ref logic_uScript_GetAndCheckTechs_techs_490);
		local_EnemyTechsA_TankArray = logic_uScript_GetAndCheckTechs_techs_490;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_490.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_490.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_490.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_490.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_491();
		}
		if (someAlive)
		{
			Relay_In_491();
		}
		if (allDead)
		{
			Relay_In_487();
		}
		if (waitingToSpawn)
		{
			Relay_In_487();
		}
	}

	private void Relay_In_491()
	{
		int num = 0;
		Array array = local_EnemyTechsA_TankArray;
		if (logic_uScript_DamageTechs_techs_491.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_491, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_491, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_491.In(logic_uScript_DamageTechs_techs_491, logic_uScript_DamageTechs_damagePercentage_491, logic_uScript_DamageTechs_givePlayerCredit_491, logic_uScript_DamageTechs_leaveBlocksPercentage_491);
	}

	private void Relay_In_494()
	{
		int num = 0;
		Array array = local_EnemyTechsC_TankArray;
		if (logic_uScript_DamageTechs_techs_494.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_494, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_494, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_494.In(logic_uScript_DamageTechs_techs_494, logic_uScript_DamageTechs_damagePercentage_494, logic_uScript_DamageTechs_givePlayerCredit_494, logic_uScript_DamageTechs_leaveBlocksPercentage_494);
	}

	private void Relay_In_495()
	{
		int num = 0;
		Array array = enemyTechDataC;
		if (logic_uScript_GetAndCheckTechs_techData_495.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_495, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_495, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_495 = owner_Connection_493;
		int num2 = 0;
		Array array2 = local_EnemyTechsC_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_495.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_495, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_495, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_495 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_495.In(logic_uScript_GetAndCheckTechs_techData_495, logic_uScript_GetAndCheckTechs_ownerNode_495, ref logic_uScript_GetAndCheckTechs_techs_495);
		local_EnemyTechsC_TankArray = logic_uScript_GetAndCheckTechs_techs_495;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_495.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_495.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_495.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_495.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_494();
		}
		if (someAlive)
		{
			Relay_In_494();
		}
		if (allDead)
		{
			Relay_In_499();
		}
		if (waitingToSpawn)
		{
			Relay_In_499();
		}
	}

	private void Relay_In_499()
	{
		int num = 0;
		Array array = enemyTechDataD;
		if (logic_uScript_GetAndCheckTechs_techData_499.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_499, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_499, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_499 = owner_Connection_502;
		int num2 = 0;
		Array array2 = local_EnemyTechsD_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_499.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_499, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_499, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_499 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_499.In(logic_uScript_GetAndCheckTechs_techData_499, logic_uScript_GetAndCheckTechs_ownerNode_499, ref logic_uScript_GetAndCheckTechs_techs_499);
		local_EnemyTechsD_TankArray = logic_uScript_GetAndCheckTechs_techs_499;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_499.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_499.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_499.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_499.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_501();
		}
		if (someAlive)
		{
			Relay_In_501();
		}
		if (allDead)
		{
			Relay_True_26();
		}
		if (waitingToSpawn)
		{
			Relay_True_26();
		}
	}

	private void Relay_In_501()
	{
		int num = 0;
		Array array = local_EnemyTechsD_TankArray;
		if (logic_uScript_DamageTechs_techs_501.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_501, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_501, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_501.In(logic_uScript_DamageTechs_techs_501, logic_uScript_DamageTechs_damagePercentage_501, logic_uScript_DamageTechs_givePlayerCredit_501, logic_uScript_DamageTechs_leaveBlocksPercentage_501);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_501.Out)
		{
			Relay_True_26();
		}
	}

	private void Relay_In_506()
	{
		int num = 0;
		Array array = local_EnemyTechsF_TankArray;
		if (logic_uScript_DamageTechs_techs_506.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_506, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_506, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_506.In(logic_uScript_DamageTechs_techs_506, logic_uScript_DamageTechs_damagePercentage_506, logic_uScript_DamageTechs_givePlayerCredit_506, logic_uScript_DamageTechs_leaveBlocksPercentage_506);
	}

	private void Relay_In_507()
	{
		int num = 0;
		Array array = enemyTechDataE;
		if (logic_uScript_GetAndCheckTechs_techData_507.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_507, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_507, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_507 = owner_Connection_503;
		int num2 = 0;
		Array array2 = local_EnemyTechsE_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_507.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_507, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_507, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_507 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_507.In(logic_uScript_GetAndCheckTechs_techData_507, logic_uScript_GetAndCheckTechs_ownerNode_507, ref logic_uScript_GetAndCheckTechs_techs_507);
		local_EnemyTechsE_TankArray = logic_uScript_GetAndCheckTechs_techs_507;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_507.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_507.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_507.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_507.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_512();
		}
		if (someAlive)
		{
			Relay_In_512();
		}
		if (allDead)
		{
			Relay_In_511();
		}
		if (waitingToSpawn)
		{
			Relay_In_511();
		}
	}

	private void Relay_In_511()
	{
		int num = 0;
		Array array = enemyTechDataF;
		if (logic_uScript_GetAndCheckTechs_techData_511.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_511, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_511, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_511 = owner_Connection_504;
		int num2 = 0;
		Array array2 = local_EnemyTechsF_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_511.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_511, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_511, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_511 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_511.In(logic_uScript_GetAndCheckTechs_techData_511, logic_uScript_GetAndCheckTechs_ownerNode_511, ref logic_uScript_GetAndCheckTechs_techs_511);
		local_EnemyTechsF_TankArray = logic_uScript_GetAndCheckTechs_techs_511;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_511.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_511.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_511.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_511.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_506();
		}
		if (someAlive)
		{
			Relay_In_506();
		}
		if (allDead)
		{
			Relay_In_452();
		}
		if (waitingToSpawn)
		{
			Relay_In_452();
		}
	}

	private void Relay_In_512()
	{
		int num = 0;
		Array array = local_EnemyTechsE_TankArray;
		if (logic_uScript_DamageTechs_techs_512.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_512, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_512, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_512.In(logic_uScript_DamageTechs_techs_512, logic_uScript_DamageTechs_damagePercentage_512, logic_uScript_DamageTechs_givePlayerCredit_512, logic_uScript_DamageTechs_leaveBlocksPercentage_512);
	}

	private void Relay_In_513()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_513 = TriggerC;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_513.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_513);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_513.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_513.OutOfRange;
		if (inRange)
		{
			Relay_True_151();
		}
		if (outOfRange)
		{
			Relay_In_515();
		}
	}

	private void Relay_In_515()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_515 = TriggerD;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_515.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_515);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_515.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_515.OutOfRange;
		if (inRange)
		{
			Relay_True_151();
		}
		if (outOfRange)
		{
			Relay_In_289();
		}
	}

	private void Relay_In_518()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_518 = TriggerB;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_518.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_518);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_518.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_518.OutOfRange;
		if (inRange)
		{
			Relay_True_222();
		}
		if (outOfRange)
		{
			Relay_In_301();
		}
	}
}
