using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_SetPiece_SamSiteRidge_02 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public SpawnTechData[] BossTechData = new SpawnTechData[0];

	public SpawnTechData[] ChargerTech1 = new SpawnTechData[0];

	public SpawnTechData[] ChargerTech2 = new SpawnTechData[0];

	public SpawnTechData[] ChargerTech3 = new SpawnTechData[0];

	private string local_102_System_String = "Tag2";

	private string local_103_System_String = "Tag2";

	private string local_105_System_String = "Tag4";

	private string local_115_System_String = "Tag5";

	private string local_122_System_String = "Tag5";

	private string local_131_System_String = "Tag4";

	private string local_135_System_String = "Tag6";

	private string local_151_System_String = "Tag8";

	private string local_194_System_String = "Tag1";

	private string local_195_System_String = "Tag2";

	private string local_197_System_String = "Tag7";

	private string local_201_System_String = "Tag3";

	private string local_203_System_String = "Tag5";

	private string local_204_System_String = "Tag6";

	private string local_205_System_String = "Tag4";

	private string local_221_System_String = "Tag2";

	private string local_222_System_String = "Tag5";

	private string local_223_System_String = "Tag3";

	private string local_225_System_String = "Tag1";

	private string local_227_System_String = "Tag6";

	private string local_229_System_String = "Tag4";

	private string local_230_System_String = "Tag8";

	private string local_234_System_String = "Tag7";

	private string local_236_System_String = "Tag8";

	private string local_241_System_String = "Tag6";

	private string local_71_System_String = "Tag1";

	private string local_73_System_String = "Tag1";

	private string local_77_System_String = "Tag3";

	private string local_97_System_String = "Tag3";

	private bool local_AproachedSAMEarly_System_Boolean;

	private Tank local_BossTech_Tank;

	private Tank[] local_BossTechs_TankArray = new Tank[0];

	private Tank[] local_ChargerTechs1_TankArray = new Tank[0];

	private Tank[] local_ChargerTechs2_TankArray = new Tank[0];

	private Tank[] local_ChargerTechs3_TankArray = new Tank[0];

	private Tank local_CharTech1_Tank;

	private Tank local_CharTech2_Tank;

	private Tank local_CharTech3_Tank;

	private bool local_EnemyDeadEarly_System_Boolean;

	private bool local_FinalObjectiveSet_System_Boolean;

	private bool local_FoundEncounter_System_Boolean;

	private Tank[] local_Mobs4_TankArray = new Tank[0];

	private Tank[] local_Mobs5_TankArray = new Tank[0];

	private Tank[] local_Mobs6_TankArray = new Tank[0];

	private bool local_ObjectiveComplete_System_Boolean;

	private bool local_ShieldDisabled_System_Boolean;

	private bool local_ShieldEnabled_System_Boolean = true;

	private bool local_ShieldSwitchedOn_System_Boolean;

	private bool local_SpawnedMobs1_System_Boolean;

	private bool local_SpawnedMobs2_System_Boolean;

	private bool local_SpawnedMobs3_System_Boolean;

	private bool local_SpawnedMobs4_System_Boolean;

	private bool local_SpawnedMobsTrigger4_System_Boolean;

	private bool local_SpawnedMobsTrigger5_System_Boolean;

	private bool local_SpawnedMobsTrigger6_System_Boolean;

	private bool local_SpawnedMobsTrigger8_System_Boolean;

	private bool local_SpawnedMobsTrigger9_System_Boolean;

	private TankBlock local_SpecialShieldBlock_TankBlock;

	private int local_Stage_System_Int32 = 1;

	private bool local_TechDeadCharger1_System_Boolean;

	private bool local_TechDeadCharger2_System_Boolean;

	private bool local_TechDeadCharger3_System_Boolean;

	private bool local_TechNearCharger1_System_Boolean;

	private bool local_TechNearCharger2_System_Boolean;

	private bool local_TechNearCharger3_System_Boolean;

	private bool local_TechsSpawned_System_Boolean;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public SpawnTechData[] MobsData1 = new SpawnTechData[0];

	public SpawnTechData[] MobsData2 = new SpawnTechData[0];

	public SpawnTechData[] MobsData3 = new SpawnTechData[0];

	public SpawnTechData[] MobsData4 = new SpawnTechData[0];

	public SpawnTechData[] MobsDataTrigger4 = new SpawnTechData[0];

	public SpawnTechData[] MobsDataTrigger5 = new SpawnTechData[0];

	public SpawnTechData[] MobsDataTrigger6 = new SpawnTechData[0];

	public SpawnTechData[] MobsDataTrigger8 = new SpawnTechData[0];

	public SpawnTechData[] MobsDataTrigger9 = new SpawnTechData[0];

	public LocalisedString[] msgAproachedSAMEarly = new LocalisedString[0];

	public LocalisedString[] msgComplete = new LocalisedString[0];

	public LocalisedString[] msgFoundMissionArea = new LocalisedString[0];

	public LocalisedString[] msgTechDeadCharger1 = new LocalisedString[0];

	public LocalisedString[] msgTechDeadCharger2 = new LocalisedString[0];

	public LocalisedString[] msgTechDeadCharger3 = new LocalisedString[0];

	public LocalisedString[] msgTechDeadChargersAll = new LocalisedString[0];

	public LocalisedString[] msgTechNearCharger1 = new LocalisedString[0];

	public LocalisedString[] msgTechNearCharger2 = new LocalisedString[0];

	public LocalisedString[] msgTechNearCharger3 = new LocalisedString[0];

	public BlockTypes SpecialShieldBlockData;

	[Multiline(3)]
	public string Trigger1 = "";

	[Multiline(3)]
	public string Trigger10 = "";

	[Multiline(3)]
	public string Trigger11 = "";

	[Multiline(3)]
	public string Trigger12 = "";

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
	public string Trigger7 = "";

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_15;

	private GameObject owner_Connection_16;

	private GameObject owner_Connection_42;

	private GameObject owner_Connection_45;

	private GameObject owner_Connection_47;

	private GameObject owner_Connection_52;

	private GameObject owner_Connection_76;

	private GameObject owner_Connection_101;

	private GameObject owner_Connection_117;

	private GameObject owner_Connection_127;

	private GameObject owner_Connection_146;

	private GameObject owner_Connection_172;

	private GameObject owner_Connection_179;

	private GameObject owner_Connection_182;

	private GameObject owner_Connection_184;

	private GameObject owner_Connection_186;

	private GameObject owner_Connection_248;

	private GameObject owner_Connection_281;

	private GameObject owner_Connection_288;

	private GameObject owner_Connection_295;

	private GameObject owner_Connection_312;

	private GameObject owner_Connection_318;

	private GameObject owner_Connection_328;

	private GameObject owner_Connection_332;

	private GameObject owner_Connection_338;

	private GameObject owner_Connection_359;

	private GameObject owner_Connection_372;

	private GameObject owner_Connection_376;

	private GameObject owner_Connection_399;

	private GameObject owner_Connection_402;

	private GameObject owner_Connection_416;

	private GameObject owner_Connection_427;

	private GameObject owner_Connection_432;

	private GameObject owner_Connection_435;

	private GameObject owner_Connection_450;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_2 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_2;

	private bool logic_uScript_FinishEncounter_Out_2 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_4 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_4 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_4;

	private string logic_uScript_AddOnScreenMessage_tag_4 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_4;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_4;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_4;

	private bool logic_uScript_AddOnScreenMessage_Out_4 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_4 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_14 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_14;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_14 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_14;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_14 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_18;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_18 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_18 = "TechsSpawned";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_19;

	private bool logic_uScriptCon_CompareBool_True_19 = true;

	private bool logic_uScriptCon_CompareBool_False_19 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_20 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_20 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_20;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_20 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_20;

	private bool logic_uScript_SpawnTechsFromData_Out_20 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_21 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_21;

	private bool logic_uScriptAct_SetBool_Out_21 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_21 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_21 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_22 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_22;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_22 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_22;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_22 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_22 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_22 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_22 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_23;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_23 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_23 = "ObjectiveComplete";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_26;

	private bool logic_uScriptCon_CompareBool_True_26 = true;

	private bool logic_uScriptCon_CompareBool_False_26 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_28 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_28;

	private bool logic_uScriptAct_SetBool_Out_28 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_28 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_28 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_31;

	private bool logic_uScriptCon_CompareBool_True_31 = true;

	private bool logic_uScriptCon_CompareBool_False_31 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_32;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_35 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_35;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_35;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_38 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_38;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_39 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_39;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_39 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_39 = "Stage";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_43 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_43 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_43;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_43 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_43;

	private bool logic_uScript_SpawnTechsFromData_Out_43 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_44 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_44 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_44;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_44 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_44;

	private bool logic_uScript_SpawnTechsFromData_Out_44 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_49 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_49 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_49;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_49 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_49;

	private bool logic_uScript_SpawnTechsFromData_Out_49 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_51 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_51 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_51;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_51 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_51;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_51 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_51 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_51 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_51 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_55;

	private bool logic_uScriptCon_CompareBool_True_55 = true;

	private bool logic_uScriptCon_CompareBool_False_55 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_58 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_58;

	private bool logic_uScriptAct_SetBool_Out_58 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_58 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_58 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_59 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_59 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_59 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_59;

	private string logic_uScript_AddOnScreenMessage_tag_59 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_59;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_59;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_59;

	private bool logic_uScript_AddOnScreenMessage_Out_59 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_59 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_63;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_63 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_63 = "TechDeadCharger1";

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_64 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_64 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_64 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_64 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_64 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_67;

	private bool logic_uScriptCon_CompareBool_True_67 = true;

	private bool logic_uScriptCon_CompareBool_False_67 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_70 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_70 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_70 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_70;

	private string logic_uScript_AddOnScreenMessage_tag_70 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_70;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_70;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_70;

	private bool logic_uScript_AddOnScreenMessage_Out_70 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_70 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_72 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_72 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_72;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_72 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_75;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_75 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_75 = "TechNearCharger1";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_80 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_80 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_80;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_80 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_80;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_80 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_80 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_80 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_80 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_83 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_83;

	private bool logic_uScriptAct_SetBool_Out_83 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_83 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_83 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_85;

	private bool logic_uScriptCon_CompareBool_True_85 = true;

	private bool logic_uScriptCon_CompareBool_False_85 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_86 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_86 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_86 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_86;

	private string logic_uScript_AddOnScreenMessage_tag_86 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_86;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_86;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_86;

	private bool logic_uScript_AddOnScreenMessage_Out_86 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_86 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_88;

	private bool logic_uScriptCon_CompareBool_True_88 = true;

	private bool logic_uScriptCon_CompareBool_False_88 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_89 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_89;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_89 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_89;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_89 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_93 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_93;

	private bool logic_uScriptAct_SetBool_Out_93 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_93 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_93 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_95 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_95 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_95 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_95;

	private string logic_uScript_AddOnScreenMessage_tag_95 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_95;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_95;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_95;

	private bool logic_uScript_AddOnScreenMessage_Out_95 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_95 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_98 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_98 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_98 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_98 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_98 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_100 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_100 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_100;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_100 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_104 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_104 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_104;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_104 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_108;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_108 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_108 = "TechDeadCharger2";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_109;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_109 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_109 = "TechNearCharger2";

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_110 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_110 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_110;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_110 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_113 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_113;

	private bool logic_uScriptAct_SetBool_Out_113 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_113 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_113 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_116 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_116;

	private bool logic_uScriptCon_CompareBool_True_116 = true;

	private bool logic_uScriptCon_CompareBool_False_116 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_119 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_119 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_119 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_119 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_119 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_120 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_120 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_120;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_120 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_121 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_121 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_121 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_121;

	private string logic_uScript_AddOnScreenMessage_tag_121 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_121;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_121;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_121;

	private bool logic_uScript_AddOnScreenMessage_Out_121 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_121 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_128 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_128;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_128 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_128;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_128 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_128 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_128 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_128 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_129 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_129;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_129 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_129;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_129 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_130;

	private bool logic_uScriptCon_CompareBool_True_130 = true;

	private bool logic_uScriptCon_CompareBool_False_130 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_136 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_136;

	private bool logic_uScriptAct_SetBool_Out_136 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_136 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_136 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_137 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_137 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_137 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_137;

	private string logic_uScript_AddOnScreenMessage_tag_137 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_137;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_137;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_137;

	private bool logic_uScript_AddOnScreenMessage_Out_137 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_137 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_139;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_139 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_139 = "TechNearCharger3";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_142;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_142 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_142 = "TechDeadCharger3";

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_145 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_145;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_145 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_145;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_145 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_149 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_149;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_149 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_152 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_152 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_152;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_152 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_154;

	private bool logic_uScriptCon_CompareBool_True_154 = true;

	private bool logic_uScriptCon_CompareBool_False_154 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_155 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_155;

	private bool logic_uScriptAct_SetBool_Out_155 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_155 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_155 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_158;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_158 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_158 = "FinalObjectiveSet";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_159;

	private bool logic_uScriptCon_CompareBool_True_159 = true;

	private bool logic_uScriptCon_CompareBool_False_159 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_162 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_162;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_162 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_163 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_165;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_165;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_166 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_166 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_167;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_167;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_170 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_170;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_170 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_170;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_170 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_177 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_177 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_177;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_177 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_177;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_177 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_177 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_177 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_177 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_178 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_178 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_178;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_178 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_178;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_178 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_178 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_178 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_178 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_180 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_180;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_180 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_180;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_180 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_183 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_183;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_183 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_183;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_183 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_190;

	private bool logic_uScriptCon_CompareBool_True_190 = true;

	private bool logic_uScriptCon_CompareBool_False_190 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_192;

	private bool logic_uScriptCon_CompareBool_True_192 = true;

	private bool logic_uScriptCon_CompareBool_False_192 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_193 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_193 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_193;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_193 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_196 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_196 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_196;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_196 = true;

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

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_202 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_202 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_202;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_202 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_206 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_206 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_206;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_206 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_207 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_207 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_207;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_207 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_208 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_208 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_208;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_208 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_210 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_210 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_210 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_210 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_210 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_212 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_212 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_212 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_212 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_212 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_214 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_214 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_214 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_214 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_214 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_216 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_216;

	private bool logic_uScriptCon_CompareBool_True_216 = true;

	private bool logic_uScriptCon_CompareBool_False_216 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_219 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_219 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_219 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_219;

	private string logic_uScript_AddOnScreenMessage_tag_219 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_219;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_219;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_219;

	private bool logic_uScript_AddOnScreenMessage_Out_219 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_219 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_220 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_220 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_220;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_220 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_224 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_224 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_224;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_224 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_226 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_226 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_226;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_226 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_228 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_228 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_228;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_228 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_231 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_231;

	private bool logic_uScriptAct_SetBool_Out_231 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_231 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_231 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_232 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_232 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_232;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_232 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_233 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_233 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_233;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_233 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_235 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_235 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_235;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_235 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_237 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_237 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_237;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_237 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_240;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_240 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_240 = "AproachedSAMEarly";

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_242 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_242 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_242;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_242 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_243 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_243;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_243 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_243 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_243 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_245;

	private bool logic_uScriptCon_CompareBool_True_245 = true;

	private bool logic_uScriptCon_CompareBool_False_245 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_246 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_246;

	private bool logic_uScriptAct_SetBool_Out_246 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_246 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_246 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_251 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_251 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_251 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_251;

	private string logic_uScript_AddOnScreenMessage_tag_251 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_251;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_251;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_251;

	private bool logic_uScript_AddOnScreenMessage_Out_251 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_251 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_253;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_253 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_253 = "FoundEncounter";

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_254 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_254 = "";

	private bool logic_uScript_SetShieldEnabled_enable_254;

	private bool logic_uScript_SetShieldEnabled_Out_254 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_255 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_255;

	private BlockTypes logic_uScript_GetTankBlock_blockType_255;

	private TankBlock logic_uScript_GetTankBlock_Return_255;

	private bool logic_uScript_GetTankBlock_Out_255 = true;

	private bool logic_uScript_GetTankBlock_Returned_255 = true;

	private bool logic_uScript_GetTankBlock_NotFound_255 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_256 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_256 = new Tank[0];

	private int logic_uScript_AccessListTech_index_256;

	private Tank logic_uScript_AccessListTech_value_256;

	private bool logic_uScript_AccessListTech_Out_256 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_264 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_264 = "";

	private bool logic_uScript_SetShieldEnabled_enable_264;

	private bool logic_uScript_SetShieldEnabled_Out_264 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_266 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_266;

	private bool logic_uScriptCon_CompareBool_True_266 = true;

	private bool logic_uScriptCon_CompareBool_False_266 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_267 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_267;

	private bool logic_uScriptAct_SetBool_Out_267 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_267 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_267 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_270;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_270 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_270 = "ShieldSwitchedOn";

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_272 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_272 = "";

	private bool logic_uScript_SetShieldEnabled_enable_272;

	private bool logic_uScript_SetShieldEnabled_Out_272 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_273 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_273 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_274 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_274 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_276 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_276 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_276 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_276 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_276 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_278 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_278;

	private bool logic_uScriptCon_CompareBool_True_278 = true;

	private bool logic_uScriptCon_CompareBool_False_278 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_279 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_279;

	private bool logic_uScriptAct_SetBool_Out_279 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_279 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_279 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_283 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_283 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_283;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_283 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_283;

	private bool logic_uScript_SpawnTechsFromData_Out_283 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_284 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_284 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_285 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_285 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_285 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_285 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_285 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_287 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_287;

	private bool logic_uScriptCon_CompareBool_True_287 = true;

	private bool logic_uScriptCon_CompareBool_False_287 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_291 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_291;

	private bool logic_uScriptAct_SetBool_Out_291 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_291 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_291 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_293 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_293 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_294 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_294 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_294;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_294 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_294;

	private bool logic_uScript_SpawnTechsFromData_Out_294 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_296 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_296 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_296;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_296 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_296;

	private bool logic_uScript_SpawnTechsFromData_Out_296 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_297 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_297;

	private bool logic_uScriptAct_SetBool_Out_297 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_297 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_297 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_300 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_300 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_300 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_300 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_300 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_302 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_302 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_303 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_303;

	private bool logic_uScriptCon_CompareBool_True_303 = true;

	private bool logic_uScriptCon_CompareBool_False_303 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_305 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_305 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_305;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_305 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_305;

	private bool logic_uScript_SpawnTechsFromData_Out_305 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_307 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_307 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_309 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_309;

	private bool logic_uScriptAct_SetBool_Out_309 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_309 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_309 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_311 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_311 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_311 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_311 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_311 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_314 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_314;

	private bool logic_uScriptCon_CompareBool_True_314 = true;

	private bool logic_uScriptCon_CompareBool_False_314 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_317 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_317 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_317;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_317 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_317;

	private bool logic_uScript_SpawnTechsFromData_Out_317 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_319 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_319;

	private bool logic_uScriptCon_CompareBool_True_319 = true;

	private bool logic_uScriptCon_CompareBool_False_319 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_320 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_320;

	private bool logic_uScriptAct_SetBool_Out_320 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_320 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_320 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_323 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_323 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_323;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_323 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_323;

	private bool logic_uScript_SpawnTechsFromData_Out_323 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_325 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_325;

	private bool logic_uScriptAct_SetBool_Out_325 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_325 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_325 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_326 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_326;

	private bool logic_uScriptCon_CompareBool_True_326 = true;

	private bool logic_uScriptCon_CompareBool_False_326 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_329 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_329;

	private bool logic_uScriptAct_SetBool_Out_329 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_329 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_329 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_333 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_333 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_333;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_333 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_333;

	private bool logic_uScript_SpawnTechsFromData_Out_333 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_334 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_334;

	private bool logic_uScriptCon_CompareBool_True_334 = true;

	private bool logic_uScriptCon_CompareBool_False_334 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_337 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_337;

	private bool logic_uScriptAct_SetBool_Out_337 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_337 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_337 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_339 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_339;

	private bool logic_uScriptAct_SetBool_Out_339 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_339 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_339 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_340 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_340 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_340;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_340 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_340;

	private bool logic_uScript_SpawnTechsFromData_Out_340 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_341 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_341;

	private bool logic_uScriptCon_CompareBool_True_341 = true;

	private bool logic_uScriptCon_CompareBool_False_341 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_351 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_351 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_352 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_352 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_353;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_353 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_353 = "SpawnedMobs1";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_354;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_354 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_354 = "SpawnedMobs2";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_355;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_355 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_355 = "SpawnedMobs3";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_356;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_356 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_356 = "SpawnedMobs4";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_357;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_357 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_357 = "SpawnedMobsTrigger8";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_358;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_358 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_358 = "SpawnedMobsTrigger9";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_360 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_360;

	private bool logic_uScriptAct_SetBool_Out_360 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_360 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_360 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_361 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_361 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_361;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_361 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_361;

	private bool logic_uScript_SpawnTechsFromData_Out_361 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_362 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_362;

	private bool logic_uScriptCon_CompareBool_True_362 = true;

	private bool logic_uScriptCon_CompareBool_False_362 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_366 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_366;

	private bool logic_uScriptAct_SetBool_Out_366 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_366 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_366 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_370 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_370;

	private bool logic_uScriptCon_CompareBool_True_370 = true;

	private bool logic_uScriptCon_CompareBool_False_370 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_371 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_371 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_371;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_371 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_371;

	private bool logic_uScript_SpawnTechsFromData_Out_371 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_374 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_374;

	private bool logic_uScriptCon_CompareBool_True_374 = true;

	private bool logic_uScriptCon_CompareBool_False_374 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_377 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_377;

	private bool logic_uScriptAct_SetBool_Out_377 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_377 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_377 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_378 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_378 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_378;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_378 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_378;

	private bool logic_uScript_SpawnTechsFromData_Out_378 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_380 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_380 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_381 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_381 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_382 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_382 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_383 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_383 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_384 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_384 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_385 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_385 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_386 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_386 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_387 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_387 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_391;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_391 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_391 = "SpawnedMobsTrigger4";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_392;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_392 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_392 = "SpawnedMobsTrigger5";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_393;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_393 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_393 = "SpawnedMobsTrigger6";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_396 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_396;

	private bool logic_uScriptAct_SetBool_Out_396 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_396 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_396 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_398 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_398;

	private bool logic_uScriptCon_CompareBool_True_398 = true;

	private bool logic_uScriptCon_CompareBool_False_398 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_401 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_401;

	private bool logic_uScriptCon_CompareBool_True_401 = true;

	private bool logic_uScriptCon_CompareBool_False_401 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_407 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_407;

	private bool logic_uScriptAct_SetBool_Out_407 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_407 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_407 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_410 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_410;

	private bool logic_uScriptCon_CompareBool_True_410 = true;

	private bool logic_uScriptCon_CompareBool_False_410 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_411 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_411 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_411 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_411 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_411 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_412 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_412 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_412 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_412 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_412 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_413 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_413 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_413;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_413 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_413;

	private bool logic_uScript_SpawnTechsFromData_Out_413 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_414 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_414 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_414;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_414 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_414;

	private bool logic_uScript_SpawnTechsFromData_Out_414 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_415 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_415 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_415;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_415 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_415;

	private bool logic_uScript_SpawnTechsFromData_Out_415 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_417 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_417 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_419 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_419;

	private bool logic_uScriptAct_SetBool_Out_419 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_419 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_419 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_420 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_420 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_420 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_420 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_420 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_422 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_422 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_423 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_423 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_426 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_426 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_426;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_426 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_426;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_426 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_426 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_426 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_426 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_429 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_429 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_429 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_429;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_429;

	private bool logic_uScript_DamageTechs_Out_429 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_431 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_431 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_431;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_431 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_431;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_431 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_431 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_431 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_431 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_434 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_434 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_434 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_434;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_434;

	private bool logic_uScript_DamageTechs_Out_434 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_436 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_436 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_436 = 100f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_436;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_436;

	private bool logic_uScript_DamageTechs_Out_436 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_437 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_437 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_437;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_437 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_437;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_437 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_437 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_437 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_437 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_439 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_439;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_439 = 0.5f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_439 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_446;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_446 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_446 = "ShieldDisabled";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_447;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_447 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_447 = "ShieldEnabled";

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_449 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_449;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_449 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_451 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_451 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_452 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_452 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_453 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_453 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_454 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_454 = "";

	private bool logic_uScript_SetShieldEnabled_enable_454;

	private bool logic_uScript_SetShieldEnabled_Out_454 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_457 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_457 = "";

	private bool logic_uScript_SetShieldEnabled_enable_457;

	private bool logic_uScript_SetShieldEnabled_Out_457 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_460 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_460 = "";

	private bool logic_uScript_SetShieldEnabled_enable_460;

	private bool logic_uScript_SetShieldEnabled_Out_460 = true;

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
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
		}
		if (null == owner_Connection_15 || !m_RegisteredForEvents)
		{
			owner_Connection_15 = parentGameObject;
			if (null != owner_Connection_15)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_15.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_15.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_12;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_12;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_12;
				}
			}
		}
		if (null == owner_Connection_16 || !m_RegisteredForEvents)
		{
			owner_Connection_16 = parentGameObject;
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
		if (null == owner_Connection_52 || !m_RegisteredForEvents)
		{
			owner_Connection_52 = parentGameObject;
		}
		if (null == owner_Connection_76 || !m_RegisteredForEvents)
		{
			owner_Connection_76 = parentGameObject;
		}
		if (null == owner_Connection_101 || !m_RegisteredForEvents)
		{
			owner_Connection_101 = parentGameObject;
		}
		if (null == owner_Connection_117 || !m_RegisteredForEvents)
		{
			owner_Connection_117 = parentGameObject;
		}
		if (null == owner_Connection_127 || !m_RegisteredForEvents)
		{
			owner_Connection_127 = parentGameObject;
		}
		if (null == owner_Connection_146 || !m_RegisteredForEvents)
		{
			owner_Connection_146 = parentGameObject;
		}
		if (null == owner_Connection_172 || !m_RegisteredForEvents)
		{
			owner_Connection_172 = parentGameObject;
		}
		if (null == owner_Connection_179 || !m_RegisteredForEvents)
		{
			owner_Connection_179 = parentGameObject;
		}
		if (null == owner_Connection_182 || !m_RegisteredForEvents)
		{
			owner_Connection_182 = parentGameObject;
		}
		if (null == owner_Connection_184 || !m_RegisteredForEvents)
		{
			owner_Connection_184 = parentGameObject;
		}
		if (null == owner_Connection_186 || !m_RegisteredForEvents)
		{
			owner_Connection_186 = parentGameObject;
		}
		if (null == owner_Connection_248 || !m_RegisteredForEvents)
		{
			owner_Connection_248 = parentGameObject;
		}
		if (null == owner_Connection_281 || !m_RegisteredForEvents)
		{
			owner_Connection_281 = parentGameObject;
		}
		if (null == owner_Connection_288 || !m_RegisteredForEvents)
		{
			owner_Connection_288 = parentGameObject;
		}
		if (null == owner_Connection_295 || !m_RegisteredForEvents)
		{
			owner_Connection_295 = parentGameObject;
		}
		if (null == owner_Connection_312 || !m_RegisteredForEvents)
		{
			owner_Connection_312 = parentGameObject;
		}
		if (null == owner_Connection_318 || !m_RegisteredForEvents)
		{
			owner_Connection_318 = parentGameObject;
		}
		if (null == owner_Connection_328 || !m_RegisteredForEvents)
		{
			owner_Connection_328 = parentGameObject;
		}
		if (null == owner_Connection_332 || !m_RegisteredForEvents)
		{
			owner_Connection_332 = parentGameObject;
		}
		if (null == owner_Connection_338 || !m_RegisteredForEvents)
		{
			owner_Connection_338 = parentGameObject;
		}
		if (null == owner_Connection_359 || !m_RegisteredForEvents)
		{
			owner_Connection_359 = parentGameObject;
		}
		if (null == owner_Connection_372 || !m_RegisteredForEvents)
		{
			owner_Connection_372 = parentGameObject;
		}
		if (null == owner_Connection_376 || !m_RegisteredForEvents)
		{
			owner_Connection_376 = parentGameObject;
		}
		if (null == owner_Connection_399 || !m_RegisteredForEvents)
		{
			owner_Connection_399 = parentGameObject;
		}
		if (null == owner_Connection_402 || !m_RegisteredForEvents)
		{
			owner_Connection_402 = parentGameObject;
		}
		if (null == owner_Connection_416 || !m_RegisteredForEvents)
		{
			owner_Connection_416 = parentGameObject;
		}
		if (null == owner_Connection_427 || !m_RegisteredForEvents)
		{
			owner_Connection_427 = parentGameObject;
		}
		if (null == owner_Connection_432 || !m_RegisteredForEvents)
		{
			owner_Connection_432 = parentGameObject;
		}
		if (null == owner_Connection_435 || !m_RegisteredForEvents)
		{
			owner_Connection_435 = parentGameObject;
		}
		if (null == owner_Connection_450 || !m_RegisteredForEvents)
		{
			owner_Connection_450 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_15)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_15.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_15.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_12;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_12;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_12;
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
		if (null != owner_Connection_15)
		{
			uScript_SaveLoad component2 = owner_Connection_15.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_12;
				component2.LoadEvent -= Instance_LoadEvent_12;
				component2.RestartEvent -= Instance_RestartEvent_12;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_2.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_14.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_20.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_21.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_35.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_38.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_43.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_44.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_49.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_51.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_58.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_59.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_64.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_70.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_72.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_80.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_83.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_86.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_89.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_95.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_98.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_100.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_104.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_110.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_113.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_116.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_119.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_120.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_121.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_129.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_137.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_145.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_149.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_152.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_155.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_162.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_166.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_170.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_177.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_178.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_180.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_183.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_193.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_196.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_202.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_206.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_207.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_208.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_210.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_212.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_214.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_216.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_219.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_220.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_224.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_226.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_228.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_231.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_232.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_233.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_235.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_237.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_242.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_243.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_246.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_251.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_254.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_255.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_256.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_264.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_266.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_267.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_272.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_273.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_274.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_276.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_278.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_279.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_283.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_284.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_285.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_287.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_291.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_293.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_294.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_296.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_297.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_300.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_302.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_303.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_305.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_307.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_309.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_311.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_314.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_317.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_319.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_320.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_323.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_325.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_326.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_329.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_333.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_334.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_337.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_339.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_340.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_341.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_351.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_352.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_360.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_361.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_362.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_366.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_370.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_371.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_374.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_377.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_378.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_380.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_381.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_382.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_383.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_384.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_385.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_386.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_387.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_396.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_398.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_401.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_407.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_410.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_411.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_412.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_413.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_414.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_415.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_417.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_419.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_420.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_422.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_423.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_426.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_429.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_431.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_434.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_436.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_437.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_439.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_449.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_451.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_452.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_453.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_454.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_457.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_460.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_15 = parentGameObject;
		owner_Connection_16 = parentGameObject;
		owner_Connection_42 = parentGameObject;
		owner_Connection_45 = parentGameObject;
		owner_Connection_47 = parentGameObject;
		owner_Connection_52 = parentGameObject;
		owner_Connection_76 = parentGameObject;
		owner_Connection_101 = parentGameObject;
		owner_Connection_117 = parentGameObject;
		owner_Connection_127 = parentGameObject;
		owner_Connection_146 = parentGameObject;
		owner_Connection_172 = parentGameObject;
		owner_Connection_179 = parentGameObject;
		owner_Connection_182 = parentGameObject;
		owner_Connection_184 = parentGameObject;
		owner_Connection_186 = parentGameObject;
		owner_Connection_248 = parentGameObject;
		owner_Connection_281 = parentGameObject;
		owner_Connection_288 = parentGameObject;
		owner_Connection_295 = parentGameObject;
		owner_Connection_312 = parentGameObject;
		owner_Connection_318 = parentGameObject;
		owner_Connection_328 = parentGameObject;
		owner_Connection_332 = parentGameObject;
		owner_Connection_338 = parentGameObject;
		owner_Connection_359 = parentGameObject;
		owner_Connection_372 = parentGameObject;
		owner_Connection_376 = parentGameObject;
		owner_Connection_399 = parentGameObject;
		owner_Connection_402 = parentGameObject;
		owner_Connection_416 = parentGameObject;
		owner_Connection_427 = parentGameObject;
		owner_Connection_432 = parentGameObject;
		owner_Connection_435 = parentGameObject;
		owner_Connection_450 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_35.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_38.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_149.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_162.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save_Out += SubGraph_SaveLoadBool_Save_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load_Out += SubGraph_SaveLoadBool_Load_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Save_Out += SubGraph_SaveLoadBool_Save_Out_23;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Load_Out += SubGraph_SaveLoadBool_Load_Out_23;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output1 += uScriptCon_ManualSwitch_Output1_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output2 += uScriptCon_ManualSwitch_Output2_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output3 += uScriptCon_ManualSwitch_Output3_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output4 += uScriptCon_ManualSwitch_Output4_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output5 += uScriptCon_ManualSwitch_Output5_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output6 += uScriptCon_ManualSwitch_Output6_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output7 += uScriptCon_ManualSwitch_Output7_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output8 += uScriptCon_ManualSwitch_Output8_32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_35.Out += SubGraph_CompleteObjectiveStage_Out_35;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_38.Out += SubGraph_LoadObjectiveStates_Out_38;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Save_Out += SubGraph_SaveLoadInt_Save_Out_39;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Load_Out += SubGraph_SaveLoadInt_Load_Out_39;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_39;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Save_Out += SubGraph_SaveLoadBool_Save_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Load_Out += SubGraph_SaveLoadBool_Load_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Save_Out += SubGraph_SaveLoadBool_Save_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Load_Out += SubGraph_SaveLoadBool_Load_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Save_Out += SubGraph_SaveLoadBool_Save_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Load_Out += SubGraph_SaveLoadBool_Load_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Save_Out += SubGraph_SaveLoadBool_Save_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Load_Out += SubGraph_SaveLoadBool_Load_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Save_Out += SubGraph_SaveLoadBool_Save_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Load_Out += SubGraph_SaveLoadBool_Load_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Save_Out += SubGraph_SaveLoadBool_Save_Out_142;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Load_Out += SubGraph_SaveLoadBool_Load_Out_142;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_142;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_149.Out += SubGraph_CompleteObjectiveStage_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Save_Out += SubGraph_SaveLoadBool_Save_Out_158;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Load_Out += SubGraph_SaveLoadBool_Load_Out_158;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_158;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_162.Out += SubGraph_CompleteObjectiveStage_Out_162;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.Out += SubGraph_CompleteObjectiveStage_Out_165;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.Out += SubGraph_CompleteObjectiveStage_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Save_Out += SubGraph_SaveLoadBool_Save_Out_240;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Load_Out += SubGraph_SaveLoadBool_Load_Out_240;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_240;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Save_Out += SubGraph_SaveLoadBool_Save_Out_253;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Load_Out += SubGraph_SaveLoadBool_Load_Out_253;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_253;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Save_Out += SubGraph_SaveLoadBool_Save_Out_270;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Load_Out += SubGraph_SaveLoadBool_Load_Out_270;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_270;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Save_Out += SubGraph_SaveLoadBool_Save_Out_353;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Load_Out += SubGraph_SaveLoadBool_Load_Out_353;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_353;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Save_Out += SubGraph_SaveLoadBool_Save_Out_354;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Load_Out += SubGraph_SaveLoadBool_Load_Out_354;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_354;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Save_Out += SubGraph_SaveLoadBool_Save_Out_355;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Load_Out += SubGraph_SaveLoadBool_Load_Out_355;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_355;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Save_Out += SubGraph_SaveLoadBool_Save_Out_356;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Load_Out += SubGraph_SaveLoadBool_Load_Out_356;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_356;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Save_Out += SubGraph_SaveLoadBool_Save_Out_357;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Load_Out += SubGraph_SaveLoadBool_Load_Out_357;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_357;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Save_Out += SubGraph_SaveLoadBool_Save_Out_358;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Load_Out += SubGraph_SaveLoadBool_Load_Out_358;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_358;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Save_Out += SubGraph_SaveLoadBool_Save_Out_391;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Load_Out += SubGraph_SaveLoadBool_Load_Out_391;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_391;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Save_Out += SubGraph_SaveLoadBool_Save_Out_392;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Load_Out += SubGraph_SaveLoadBool_Load_Out_392;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_392;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Save_Out += SubGraph_SaveLoadBool_Save_Out_393;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Load_Out += SubGraph_SaveLoadBool_Load_Out_393;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_393;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Save_Out += SubGraph_SaveLoadBool_Save_Out_446;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Load_Out += SubGraph_SaveLoadBool_Load_Out_446;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_446;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Save_Out += SubGraph_SaveLoadBool_Save_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Load_Out += SubGraph_SaveLoadBool_Load_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_447;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_35.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_38.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_149.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_162.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_35.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_38.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_149.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_162.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_449.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_14.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_35.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_38.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_59.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_70.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_86.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_89.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_95.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_121.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_129.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_137.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_145.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_149.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_162.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_170.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_180.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_183.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_219.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_243.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_251.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_255.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_35.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_38.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_149.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_162.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_35.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_38.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_149.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_162.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save_Out -= SubGraph_SaveLoadBool_Save_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load_Out -= SubGraph_SaveLoadBool_Load_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Save_Out -= SubGraph_SaveLoadBool_Save_Out_23;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Load_Out -= SubGraph_SaveLoadBool_Load_Out_23;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output1 -= uScriptCon_ManualSwitch_Output1_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output2 -= uScriptCon_ManualSwitch_Output2_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output3 -= uScriptCon_ManualSwitch_Output3_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output4 -= uScriptCon_ManualSwitch_Output4_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output5 -= uScriptCon_ManualSwitch_Output5_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output6 -= uScriptCon_ManualSwitch_Output6_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output7 -= uScriptCon_ManualSwitch_Output7_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output8 -= uScriptCon_ManualSwitch_Output8_32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_35.Out -= SubGraph_CompleteObjectiveStage_Out_35;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_38.Out -= SubGraph_LoadObjectiveStates_Out_38;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Save_Out -= SubGraph_SaveLoadInt_Save_Out_39;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Load_Out -= SubGraph_SaveLoadInt_Load_Out_39;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_39;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Save_Out -= SubGraph_SaveLoadBool_Save_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Load_Out -= SubGraph_SaveLoadBool_Load_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Save_Out -= SubGraph_SaveLoadBool_Save_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Load_Out -= SubGraph_SaveLoadBool_Load_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Save_Out -= SubGraph_SaveLoadBool_Save_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Load_Out -= SubGraph_SaveLoadBool_Load_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Save_Out -= SubGraph_SaveLoadBool_Save_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Load_Out -= SubGraph_SaveLoadBool_Load_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Save_Out -= SubGraph_SaveLoadBool_Save_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Load_Out -= SubGraph_SaveLoadBool_Load_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Save_Out -= SubGraph_SaveLoadBool_Save_Out_142;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Load_Out -= SubGraph_SaveLoadBool_Load_Out_142;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_142;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_149.Out -= SubGraph_CompleteObjectiveStage_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Save_Out -= SubGraph_SaveLoadBool_Save_Out_158;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Load_Out -= SubGraph_SaveLoadBool_Load_Out_158;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_158;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_162.Out -= SubGraph_CompleteObjectiveStage_Out_162;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.Out -= SubGraph_CompleteObjectiveStage_Out_165;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.Out -= SubGraph_CompleteObjectiveStage_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Save_Out -= SubGraph_SaveLoadBool_Save_Out_240;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Load_Out -= SubGraph_SaveLoadBool_Load_Out_240;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_240;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Save_Out -= SubGraph_SaveLoadBool_Save_Out_253;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Load_Out -= SubGraph_SaveLoadBool_Load_Out_253;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_253;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Save_Out -= SubGraph_SaveLoadBool_Save_Out_270;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Load_Out -= SubGraph_SaveLoadBool_Load_Out_270;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_270;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Save_Out -= SubGraph_SaveLoadBool_Save_Out_353;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Load_Out -= SubGraph_SaveLoadBool_Load_Out_353;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_353;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Save_Out -= SubGraph_SaveLoadBool_Save_Out_354;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Load_Out -= SubGraph_SaveLoadBool_Load_Out_354;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_354;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Save_Out -= SubGraph_SaveLoadBool_Save_Out_355;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Load_Out -= SubGraph_SaveLoadBool_Load_Out_355;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_355;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Save_Out -= SubGraph_SaveLoadBool_Save_Out_356;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Load_Out -= SubGraph_SaveLoadBool_Load_Out_356;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_356;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Save_Out -= SubGraph_SaveLoadBool_Save_Out_357;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Load_Out -= SubGraph_SaveLoadBool_Load_Out_357;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_357;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Save_Out -= SubGraph_SaveLoadBool_Save_Out_358;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Load_Out -= SubGraph_SaveLoadBool_Load_Out_358;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_358;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Save_Out -= SubGraph_SaveLoadBool_Save_Out_391;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Load_Out -= SubGraph_SaveLoadBool_Load_Out_391;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_391;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Save_Out -= SubGraph_SaveLoadBool_Save_Out_392;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Load_Out -= SubGraph_SaveLoadBool_Load_Out_392;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_392;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Save_Out -= SubGraph_SaveLoadBool_Save_Out_393;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Load_Out -= SubGraph_SaveLoadBool_Load_Out_393;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_393;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Save_Out -= SubGraph_SaveLoadBool_Save_Out_446;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Load_Out -= SubGraph_SaveLoadBool_Load_Out_446;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_446;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Save_Out -= SubGraph_SaveLoadBool_Save_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Load_Out -= SubGraph_SaveLoadBool_Load_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_447;
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

	private void Instance_SaveEvent_12(object o, EventArgs e)
	{
		Relay_SaveEvent_12();
	}

	private void Instance_LoadEvent_12(object o, EventArgs e)
	{
		Relay_LoadEvent_12();
	}

	private void Instance_RestartEvent_12(object o, EventArgs e)
	{
		Relay_RestartEvent_12();
	}

	private void SubGraph_SaveLoadBool_Save_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Save_Out_18();
	}

	private void SubGraph_SaveLoadBool_Load_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Load_Out_18();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Restart_Out_18();
	}

	private void SubGraph_SaveLoadBool_Save_Out_23(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_23;
		Relay_Save_Out_23();
	}

	private void SubGraph_SaveLoadBool_Load_Out_23(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_23;
		Relay_Load_Out_23();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_23(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_23;
		Relay_Restart_Out_23();
	}

	private void uScriptCon_ManualSwitch_Output1_32(object o, EventArgs e)
	{
		Relay_Output1_32();
	}

	private void uScriptCon_ManualSwitch_Output2_32(object o, EventArgs e)
	{
		Relay_Output2_32();
	}

	private void uScriptCon_ManualSwitch_Output3_32(object o, EventArgs e)
	{
		Relay_Output3_32();
	}

	private void uScriptCon_ManualSwitch_Output4_32(object o, EventArgs e)
	{
		Relay_Output4_32();
	}

	private void uScriptCon_ManualSwitch_Output5_32(object o, EventArgs e)
	{
		Relay_Output5_32();
	}

	private void uScriptCon_ManualSwitch_Output6_32(object o, EventArgs e)
	{
		Relay_Output6_32();
	}

	private void uScriptCon_ManualSwitch_Output7_32(object o, EventArgs e)
	{
		Relay_Output7_32();
	}

	private void uScriptCon_ManualSwitch_Output8_32(object o, EventArgs e)
	{
		Relay_Output8_32();
	}

	private void SubGraph_CompleteObjectiveStage_Out_35(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_35 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_35;
		Relay_Out_35();
	}

	private void SubGraph_LoadObjectiveStates_Out_38(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_38();
	}

	private void SubGraph_SaveLoadInt_Save_Out_39(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_39 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_39;
		Relay_Save_Out_39();
	}

	private void SubGraph_SaveLoadInt_Load_Out_39(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_39 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_39;
		Relay_Load_Out_39();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_39(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_39 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_39;
		Relay_Restart_Out_39();
	}

	private void SubGraph_SaveLoadBool_Save_Out_63(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = e.boolean;
		local_TechDeadCharger1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_63;
		Relay_Save_Out_63();
	}

	private void SubGraph_SaveLoadBool_Load_Out_63(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = e.boolean;
		local_TechDeadCharger1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_63;
		Relay_Load_Out_63();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_63(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = e.boolean;
		local_TechDeadCharger1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_63;
		Relay_Restart_Out_63();
	}

	private void SubGraph_SaveLoadBool_Save_Out_75(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = e.boolean;
		local_TechNearCharger1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_75;
		Relay_Save_Out_75();
	}

	private void SubGraph_SaveLoadBool_Load_Out_75(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = e.boolean;
		local_TechNearCharger1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_75;
		Relay_Load_Out_75();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_75(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = e.boolean;
		local_TechNearCharger1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_75;
		Relay_Restart_Out_75();
	}

	private void SubGraph_SaveLoadBool_Save_Out_108(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = e.boolean;
		local_TechDeadCharger2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_108;
		Relay_Save_Out_108();
	}

	private void SubGraph_SaveLoadBool_Load_Out_108(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = e.boolean;
		local_TechDeadCharger2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_108;
		Relay_Load_Out_108();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_108(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = e.boolean;
		local_TechDeadCharger2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_108;
		Relay_Restart_Out_108();
	}

	private void SubGraph_SaveLoadBool_Save_Out_109(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = e.boolean;
		local_TechNearCharger2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_109;
		Relay_Save_Out_109();
	}

	private void SubGraph_SaveLoadBool_Load_Out_109(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = e.boolean;
		local_TechNearCharger2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_109;
		Relay_Load_Out_109();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_109(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = e.boolean;
		local_TechNearCharger2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_109;
		Relay_Restart_Out_109();
	}

	private void SubGraph_SaveLoadBool_Save_Out_139(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = e.boolean;
		local_TechNearCharger3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_139;
		Relay_Save_Out_139();
	}

	private void SubGraph_SaveLoadBool_Load_Out_139(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = e.boolean;
		local_TechNearCharger3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_139;
		Relay_Load_Out_139();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_139(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = e.boolean;
		local_TechNearCharger3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_139;
		Relay_Restart_Out_139();
	}

	private void SubGraph_SaveLoadBool_Save_Out_142(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = e.boolean;
		local_TechDeadCharger3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_142;
		Relay_Save_Out_142();
	}

	private void SubGraph_SaveLoadBool_Load_Out_142(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = e.boolean;
		local_TechDeadCharger3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_142;
		Relay_Load_Out_142();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_142(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = e.boolean;
		local_TechDeadCharger3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_142;
		Relay_Restart_Out_142();
	}

	private void SubGraph_CompleteObjectiveStage_Out_149(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_149 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_149;
		Relay_Out_149();
	}

	private void SubGraph_SaveLoadBool_Save_Out_158(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = e.boolean;
		local_FinalObjectiveSet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_158;
		Relay_Save_Out_158();
	}

	private void SubGraph_SaveLoadBool_Load_Out_158(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = e.boolean;
		local_FinalObjectiveSet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_158;
		Relay_Load_Out_158();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_158(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = e.boolean;
		local_FinalObjectiveSet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_158;
		Relay_Restart_Out_158();
	}

	private void SubGraph_CompleteObjectiveStage_Out_162(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_162 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_162;
		Relay_Out_162();
	}

	private void SubGraph_CompleteObjectiveStage_Out_165(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_165 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_165;
		Relay_Out_165();
	}

	private void SubGraph_CompleteObjectiveStage_Out_167(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_167 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_167;
		Relay_Out_167();
	}

	private void SubGraph_SaveLoadBool_Save_Out_240(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_240 = e.boolean;
		local_AproachedSAMEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_240;
		Relay_Save_Out_240();
	}

	private void SubGraph_SaveLoadBool_Load_Out_240(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_240 = e.boolean;
		local_AproachedSAMEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_240;
		Relay_Load_Out_240();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_240(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_240 = e.boolean;
		local_AproachedSAMEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_240;
		Relay_Restart_Out_240();
	}

	private void SubGraph_SaveLoadBool_Save_Out_253(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = e.boolean;
		local_FoundEncounter_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_253;
		Relay_Save_Out_253();
	}

	private void SubGraph_SaveLoadBool_Load_Out_253(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = e.boolean;
		local_FoundEncounter_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_253;
		Relay_Load_Out_253();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_253(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = e.boolean;
		local_FoundEncounter_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_253;
		Relay_Restart_Out_253();
	}

	private void SubGraph_SaveLoadBool_Save_Out_270(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = e.boolean;
		local_ShieldSwitchedOn_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_270;
		Relay_Save_Out_270();
	}

	private void SubGraph_SaveLoadBool_Load_Out_270(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = e.boolean;
		local_ShieldSwitchedOn_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_270;
		Relay_Load_Out_270();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_270(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = e.boolean;
		local_ShieldSwitchedOn_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_270;
		Relay_Restart_Out_270();
	}

	private void SubGraph_SaveLoadBool_Save_Out_353(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_353 = e.boolean;
		local_SpawnedMobs1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_353;
		Relay_Save_Out_353();
	}

	private void SubGraph_SaveLoadBool_Load_Out_353(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_353 = e.boolean;
		local_SpawnedMobs1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_353;
		Relay_Load_Out_353();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_353(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_353 = e.boolean;
		local_SpawnedMobs1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_353;
		Relay_Restart_Out_353();
	}

	private void SubGraph_SaveLoadBool_Save_Out_354(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_354 = e.boolean;
		local_SpawnedMobs2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_354;
		Relay_Save_Out_354();
	}

	private void SubGraph_SaveLoadBool_Load_Out_354(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_354 = e.boolean;
		local_SpawnedMobs2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_354;
		Relay_Load_Out_354();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_354(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_354 = e.boolean;
		local_SpawnedMobs2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_354;
		Relay_Restart_Out_354();
	}

	private void SubGraph_SaveLoadBool_Save_Out_355(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = e.boolean;
		local_SpawnedMobs3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_355;
		Relay_Save_Out_355();
	}

	private void SubGraph_SaveLoadBool_Load_Out_355(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = e.boolean;
		local_SpawnedMobs3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_355;
		Relay_Load_Out_355();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_355(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = e.boolean;
		local_SpawnedMobs3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_355;
		Relay_Restart_Out_355();
	}

	private void SubGraph_SaveLoadBool_Save_Out_356(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = e.boolean;
		local_SpawnedMobs4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_356;
		Relay_Save_Out_356();
	}

	private void SubGraph_SaveLoadBool_Load_Out_356(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = e.boolean;
		local_SpawnedMobs4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_356;
		Relay_Load_Out_356();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_356(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = e.boolean;
		local_SpawnedMobs4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_356;
		Relay_Restart_Out_356();
	}

	private void SubGraph_SaveLoadBool_Save_Out_357(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = e.boolean;
		local_SpawnedMobsTrigger8_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_357;
		Relay_Save_Out_357();
	}

	private void SubGraph_SaveLoadBool_Load_Out_357(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = e.boolean;
		local_SpawnedMobsTrigger8_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_357;
		Relay_Load_Out_357();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_357(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = e.boolean;
		local_SpawnedMobsTrigger8_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_357;
		Relay_Restart_Out_357();
	}

	private void SubGraph_SaveLoadBool_Save_Out_358(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_358 = e.boolean;
		local_SpawnedMobsTrigger9_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_358;
		Relay_Save_Out_358();
	}

	private void SubGraph_SaveLoadBool_Load_Out_358(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_358 = e.boolean;
		local_SpawnedMobsTrigger9_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_358;
		Relay_Load_Out_358();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_358(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_358 = e.boolean;
		local_SpawnedMobsTrigger9_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_358;
		Relay_Restart_Out_358();
	}

	private void SubGraph_SaveLoadBool_Save_Out_391(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_391 = e.boolean;
		local_SpawnedMobsTrigger4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_391;
		Relay_Save_Out_391();
	}

	private void SubGraph_SaveLoadBool_Load_Out_391(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_391 = e.boolean;
		local_SpawnedMobsTrigger4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_391;
		Relay_Load_Out_391();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_391(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_391 = e.boolean;
		local_SpawnedMobsTrigger4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_391;
		Relay_Restart_Out_391();
	}

	private void SubGraph_SaveLoadBool_Save_Out_392(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_392 = e.boolean;
		local_SpawnedMobsTrigger5_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_392;
		Relay_Save_Out_392();
	}

	private void SubGraph_SaveLoadBool_Load_Out_392(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_392 = e.boolean;
		local_SpawnedMobsTrigger5_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_392;
		Relay_Load_Out_392();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_392(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_392 = e.boolean;
		local_SpawnedMobsTrigger5_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_392;
		Relay_Restart_Out_392();
	}

	private void SubGraph_SaveLoadBool_Save_Out_393(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = e.boolean;
		local_SpawnedMobsTrigger6_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_393;
		Relay_Save_Out_393();
	}

	private void SubGraph_SaveLoadBool_Load_Out_393(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = e.boolean;
		local_SpawnedMobsTrigger6_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_393;
		Relay_Load_Out_393();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_393(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = e.boolean;
		local_SpawnedMobsTrigger6_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_393;
		Relay_Restart_Out_393();
	}

	private void SubGraph_SaveLoadBool_Save_Out_446(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = e.boolean;
		local_ShieldDisabled_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_446;
		Relay_Save_Out_446();
	}

	private void SubGraph_SaveLoadBool_Load_Out_446(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = e.boolean;
		local_ShieldDisabled_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_446;
		Relay_Load_Out_446();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_446(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = e.boolean;
		local_ShieldDisabled_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_446;
		Relay_Restart_Out_446();
	}

	private void SubGraph_SaveLoadBool_Save_Out_447(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = e.boolean;
		local_ShieldEnabled_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_447;
		Relay_Save_Out_447();
	}

	private void SubGraph_SaveLoadBool_Load_Out_447(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = e.boolean;
		local_ShieldEnabled_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_447;
		Relay_Load_Out_447();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_447(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = e.boolean;
		local_ShieldEnabled_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_447;
		Relay_Restart_Out_447();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_243();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Succeed_2()
	{
		logic_uScript_FinishEncounter_owner_2 = owner_Connection_3;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_2.Succeed(logic_uScript_FinishEncounter_owner_2);
	}

	private void Relay_Fail_2()
	{
		logic_uScript_FinishEncounter_owner_2 = owner_Connection_3;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_2.Fail(logic_uScript_FinishEncounter_owner_2);
	}

	private void Relay_In_4()
	{
		int num = 0;
		Array array = msgComplete;
		if (logic_uScript_AddOnScreenMessage_locString_4.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_4, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_4, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_4 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_4 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.In(logic_uScript_AddOnScreenMessage_locString_4, logic_uScript_AddOnScreenMessage_msgPriority_4, logic_uScript_AddOnScreenMessage_holdMsg_4, logic_uScript_AddOnScreenMessage_tag_4, logic_uScript_AddOnScreenMessage_speaker_4, logic_uScript_AddOnScreenMessage_side_4);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.Out)
		{
			Relay_UnPause_453();
		}
	}

	private void Relay_SaveEvent_12()
	{
		Relay_Save_39();
	}

	private void Relay_LoadEvent_12()
	{
		Relay_Load_39();
	}

	private void Relay_RestartEvent_12()
	{
		Relay_Restart_39();
	}

	private void Relay_In_14()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_14 = owner_Connection_16;
		int num = 0;
		Array array = local_ChargerTechs1_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_14.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_14, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_14, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_14 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_14.In(logic_uScript_SetOneTechAsEncounterTarget_owner_14, logic_uScript_SetOneTechAsEncounterTarget_techs_14);
		local_CharTech1_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_14;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_14.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_Save_Out_18()
	{
		Relay_Save_63();
	}

	private void Relay_Load_Out_18()
	{
		Relay_Load_63();
	}

	private void Relay_Restart_Out_18()
	{
		Relay_Set_False_63();
	}

	private void Relay_Save_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Load_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Set_True_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Set_False_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_In_19()
	{
		logic_uScriptCon_CompareBool_Bool_19 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.In(logic_uScriptCon_CompareBool_Bool_19);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.False;
		if (num)
		{
			Relay_In_22();
		}
		if (flag)
		{
			Relay_In_449();
		}
	}

	private void Relay_InitialSpawn_20()
	{
		int num = 0;
		Array bossTechData = BossTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_20.Length != num + bossTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_20, num + bossTechData.Length);
		}
		Array.Copy(bossTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_20, num, bossTechData.Length);
		num += bossTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_20 = owner_Connection_9;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_20.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_20, logic_uScript_SpawnTechsFromData_ownerNode_20, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_20, logic_uScript_SpawnTechsFromData_allowResurrection_20);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_20.Out)
		{
			Relay_InitialSpawn_43();
		}
	}

	private void Relay_True_21()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_21.True(out logic_uScriptAct_SetBool_Target_21);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_21;
	}

	private void Relay_False_21()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_21.False(out logic_uScriptAct_SetBool_Target_21);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_21;
	}

	private void Relay_In_22()
	{
		int num = 0;
		Array bossTechData = BossTechData;
		if (logic_uScript_GetAndCheckTechs_techData_22.Length != num + bossTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_22, num + bossTechData.Length);
		}
		Array.Copy(bossTechData, 0, logic_uScript_GetAndCheckTechs_techData_22, num, bossTechData.Length);
		num += bossTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_22 = owner_Connection_10;
		int num2 = 0;
		Array array = local_BossTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_22.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_22, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_22, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_22 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.In(logic_uScript_GetAndCheckTechs_techData_22, logic_uScript_GetAndCheckTechs_ownerNode_22, ref logic_uScript_GetAndCheckTechs_techs_22);
		local_BossTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_22;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_256();
		}
		if (someAlive)
		{
			Relay_AtIndex_256();
		}
		if (allDead)
		{
			Relay_In_272();
		}
	}

	private void Relay_Save_Out_23()
	{
		Relay_Save_18();
	}

	private void Relay_Load_Out_23()
	{
		Relay_Load_18();
	}

	private void Relay_Restart_Out_23()
	{
		Relay_Set_False_18();
	}

	private void Relay_Save_23()
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_23 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Save(ref logic_SubGraph_SaveLoadBool_boolean_23, logic_SubGraph_SaveLoadBool_boolAsVariable_23, logic_SubGraph_SaveLoadBool_uniqueID_23);
	}

	private void Relay_Load_23()
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_23 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Load(ref logic_SubGraph_SaveLoadBool_boolean_23, logic_SubGraph_SaveLoadBool_boolAsVariable_23, logic_SubGraph_SaveLoadBool_uniqueID_23);
	}

	private void Relay_Set_True_23()
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_23 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_23, logic_SubGraph_SaveLoadBool_boolAsVariable_23, logic_SubGraph_SaveLoadBool_uniqueID_23);
	}

	private void Relay_Set_False_23()
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_23 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_23, logic_SubGraph_SaveLoadBool_boolAsVariable_23, logic_SubGraph_SaveLoadBool_uniqueID_23);
	}

	private void Relay_In_26()
	{
		logic_uScriptCon_CompareBool_Bool_26 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.In(logic_uScriptCon_CompareBool_Bool_26);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.False;
		if (num)
		{
			Relay_In_31();
		}
		if (flag)
		{
			Relay_In_19();
		}
	}

	private void Relay_True_28()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.True(out logic_uScriptAct_SetBool_Target_28);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_28;
	}

	private void Relay_False_28()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.False(out logic_uScriptAct_SetBool_Target_28);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_28;
	}

	private void Relay_In_31()
	{
		logic_uScriptCon_CompareBool_Bool_31 = local_EnemyDeadEarly_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.In(logic_uScriptCon_CompareBool_Bool_31);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.False;
		if (num)
		{
			Relay_UnPause_453();
		}
		if (flag)
		{
			Relay_In_242();
		}
	}

	private void Relay_Output1_32()
	{
		Relay_In_26();
	}

	private void Relay_Output2_32()
	{
		Relay_In_26();
	}

	private void Relay_Output3_32()
	{
		Relay_In_26();
	}

	private void Relay_Output4_32()
	{
		Relay_In_26();
	}

	private void Relay_Output5_32()
	{
		Relay_In_26();
	}

	private void Relay_Output6_32()
	{
	}

	private void Relay_Output7_32()
	{
	}

	private void Relay_Output8_32()
	{
	}

	private void Relay_In_32()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_32 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.In(logic_uScriptCon_ManualSwitch_CurrentOutput_32);
	}

	private void Relay_Out_35()
	{
		Relay_True_246();
	}

	private void Relay_In_35()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_35 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_35.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_35, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_35);
	}

	private void Relay_Out_38()
	{
		Relay_Load_353();
	}

	private void Relay_In_38()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_38 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_38.In(logic_SubGraph_LoadObjectiveStates_currentObjective_38);
	}

	private void Relay_Save_Out_39()
	{
		Relay_Save_23();
	}

	private void Relay_Load_Out_39()
	{
		Relay_Load_23();
	}

	private void Relay_Restart_Out_39()
	{
		Relay_Set_False_23();
	}

	private void Relay_Save_39()
	{
		logic_SubGraph_SaveLoadInt_integer_39 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_39 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Save(logic_SubGraph_SaveLoadInt_restartValue_39, ref logic_SubGraph_SaveLoadInt_integer_39, logic_SubGraph_SaveLoadInt_intAsVariable_39, logic_SubGraph_SaveLoadInt_uniqueID_39);
	}

	private void Relay_Load_39()
	{
		logic_SubGraph_SaveLoadInt_integer_39 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_39 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Load(logic_SubGraph_SaveLoadInt_restartValue_39, ref logic_SubGraph_SaveLoadInt_integer_39, logic_SubGraph_SaveLoadInt_intAsVariable_39, logic_SubGraph_SaveLoadInt_uniqueID_39);
	}

	private void Relay_Restart_39()
	{
		logic_SubGraph_SaveLoadInt_integer_39 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_39 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Restart(logic_SubGraph_SaveLoadInt_restartValue_39, ref logic_SubGraph_SaveLoadInt_integer_39, logic_SubGraph_SaveLoadInt_intAsVariable_39, logic_SubGraph_SaveLoadInt_uniqueID_39);
	}

	private void Relay_InitialSpawn_43()
	{
		int num = 0;
		Array chargerTech = ChargerTech1;
		if (logic_uScript_SpawnTechsFromData_spawnData_43.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_43, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_SpawnTechsFromData_spawnData_43, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_43 = owner_Connection_42;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_43.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_43, logic_uScript_SpawnTechsFromData_ownerNode_43, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_43, logic_uScript_SpawnTechsFromData_allowResurrection_43);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_43.Out)
		{
			Relay_InitialSpawn_44();
		}
	}

	private void Relay_InitialSpawn_44()
	{
		int num = 0;
		Array chargerTech = ChargerTech2;
		if (logic_uScript_SpawnTechsFromData_spawnData_44.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_44, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_SpawnTechsFromData_spawnData_44, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_44 = owner_Connection_45;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_44.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_44, logic_uScript_SpawnTechsFromData_ownerNode_44, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_44, logic_uScript_SpawnTechsFromData_allowResurrection_44);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_44.Out)
		{
			Relay_InitialSpawn_49();
		}
	}

	private void Relay_InitialSpawn_49()
	{
		int num = 0;
		Array chargerTech = ChargerTech3;
		if (logic_uScript_SpawnTechsFromData_spawnData_49.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_49, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_SpawnTechsFromData_spawnData_49, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_49 = owner_Connection_47;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_49.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_49, logic_uScript_SpawnTechsFromData_ownerNode_49, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_49, logic_uScript_SpawnTechsFromData_allowResurrection_49);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_49.Out)
		{
			Relay_True_28();
		}
	}

	private void Relay_In_51()
	{
		int num = 0;
		Array chargerTech = ChargerTech1;
		if (logic_uScript_GetAndCheckTechs_techData_51.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_51, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_GetAndCheckTechs_techData_51, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_51 = owner_Connection_52;
		int num2 = 0;
		Array array = local_ChargerTechs1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_51.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_51, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_51, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_51 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_51.In(logic_uScript_GetAndCheckTechs_techData_51, logic_uScript_GetAndCheckTechs_ownerNode_51, ref logic_uScript_GetAndCheckTechs_techs_51);
		local_ChargerTechs1_TankArray = logic_uScript_GetAndCheckTechs_techs_51;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_51.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_51.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_51.AllDead;
		if (allAlive)
		{
			Relay_In_64();
		}
		if (someAlive)
		{
			Relay_In_64();
		}
		if (allDead)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_55()
	{
		logic_uScriptCon_CompareBool_Bool_55 = local_TechDeadCharger1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.In(logic_uScriptCon_CompareBool_Bool_55);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.False;
		if (num)
		{
			Relay_In_163();
		}
		if (flag)
		{
			Relay_In_72();
		}
	}

	private void Relay_True_58()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_58.True(out logic_uScriptAct_SetBool_Target_58);
		local_TechDeadCharger1_System_Boolean = logic_uScriptAct_SetBool_Target_58;
	}

	private void Relay_False_58()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_58.False(out logic_uScriptAct_SetBool_Target_58);
		local_TechDeadCharger1_System_Boolean = logic_uScriptAct_SetBool_Target_58;
	}

	private void Relay_In_59()
	{
		int num = 0;
		Array array = msgTechDeadCharger1;
		if (logic_uScript_AddOnScreenMessage_locString_59.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_59, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_59, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_59 = local_102_System_String;
		logic_uScript_AddOnScreenMessage_speaker_59 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_59 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_59.In(logic_uScript_AddOnScreenMessage_locString_59, logic_uScript_AddOnScreenMessage_msgPriority_59, logic_uScript_AddOnScreenMessage_holdMsg_59, logic_uScript_AddOnScreenMessage_tag_59, logic_uScript_AddOnScreenMessage_speaker_59, logic_uScript_AddOnScreenMessage_side_59);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_59.Out)
		{
			Relay_In_162();
		}
	}

	private void Relay_Save_Out_63()
	{
		Relay_Save_75();
	}

	private void Relay_Load_Out_63()
	{
		Relay_Load_75();
	}

	private void Relay_Restart_Out_63()
	{
		Relay_Set_False_75();
	}

	private void Relay_Save_63()
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_63 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Save(ref logic_SubGraph_SaveLoadBool_boolean_63, logic_SubGraph_SaveLoadBool_boolAsVariable_63, logic_SubGraph_SaveLoadBool_uniqueID_63);
	}

	private void Relay_Load_63()
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_63 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Load(ref logic_SubGraph_SaveLoadBool_boolean_63, logic_SubGraph_SaveLoadBool_boolAsVariable_63, logic_SubGraph_SaveLoadBool_uniqueID_63);
	}

	private void Relay_Set_True_63()
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_63 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_63, logic_SubGraph_SaveLoadBool_boolAsVariable_63, logic_SubGraph_SaveLoadBool_uniqueID_63);
	}

	private void Relay_Set_False_63()
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_63 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_63, logic_SubGraph_SaveLoadBool_boolAsVariable_63, logic_SubGraph_SaveLoadBool_uniqueID_63);
	}

	private void Relay_In_64()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_64 = Trigger1;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_64.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_64);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_64.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_64.OutOfRange;
		if (inRange)
		{
			Relay_In_14();
		}
		if (outOfRange)
		{
			Relay_In_163();
		}
	}

	private void Relay_In_67()
	{
		logic_uScriptCon_CompareBool_Bool_67 = local_TechNearCharger1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67.In(logic_uScriptCon_CompareBool_Bool_67);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67.False)
		{
			Relay_In_70();
		}
	}

	private void Relay_In_70()
	{
		int num = 0;
		Array array = msgTechNearCharger1;
		if (logic_uScript_AddOnScreenMessage_locString_70.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_70, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_70, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_70 = local_71_System_String;
		logic_uScript_AddOnScreenMessage_speaker_70 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_70 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_70.In(logic_uScript_AddOnScreenMessage_locString_70, logic_uScript_AddOnScreenMessage_msgPriority_70, logic_uScript_AddOnScreenMessage_holdMsg_70, logic_uScript_AddOnScreenMessage_tag_70, logic_uScript_AddOnScreenMessage_speaker_70, logic_uScript_AddOnScreenMessage_side_70);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_70.Out)
		{
			Relay_In_387();
		}
	}

	private void Relay_In_72()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_72 = local_73_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_72.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_72, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_72);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_72.Out)
		{
			Relay_In_59();
		}
	}

	private void Relay_Save_Out_75()
	{
		Relay_Save_108();
	}

	private void Relay_Load_Out_75()
	{
		Relay_Load_108();
	}

	private void Relay_Restart_Out_75()
	{
		Relay_Set_False_108();
	}

	private void Relay_Save_75()
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_75 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Save(ref logic_SubGraph_SaveLoadBool_boolean_75, logic_SubGraph_SaveLoadBool_boolAsVariable_75, logic_SubGraph_SaveLoadBool_uniqueID_75);
	}

	private void Relay_Load_75()
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_75 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Load(ref logic_SubGraph_SaveLoadBool_boolean_75, logic_SubGraph_SaveLoadBool_boolAsVariable_75, logic_SubGraph_SaveLoadBool_uniqueID_75);
	}

	private void Relay_Set_True_75()
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_75 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_75, logic_SubGraph_SaveLoadBool_boolAsVariable_75, logic_SubGraph_SaveLoadBool_uniqueID_75);
	}

	private void Relay_Set_False_75()
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_75 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_75, logic_SubGraph_SaveLoadBool_boolAsVariable_75, logic_SubGraph_SaveLoadBool_uniqueID_75);
	}

	private void Relay_In_80()
	{
		int num = 0;
		Array chargerTech = ChargerTech2;
		if (logic_uScript_GetAndCheckTechs_techData_80.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_80, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_GetAndCheckTechs_techData_80, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_80 = owner_Connection_101;
		int num2 = 0;
		Array array = local_ChargerTechs2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_80.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_80, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_80, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_80 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_80.In(logic_uScript_GetAndCheckTechs_techData_80, logic_uScript_GetAndCheckTechs_ownerNode_80, ref logic_uScript_GetAndCheckTechs_techs_80);
		local_ChargerTechs2_TankArray = logic_uScript_GetAndCheckTechs_techs_80;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_80.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_80.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_80.AllDead;
		if (allAlive)
		{
			Relay_In_98();
		}
		if (someAlive)
		{
			Relay_In_98();
		}
		if (allDead)
		{
			Relay_In_88();
		}
	}

	private void Relay_True_83()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_83.True(out logic_uScriptAct_SetBool_Target_83);
		local_TechNearCharger2_System_Boolean = logic_uScriptAct_SetBool_Target_83;
	}

	private void Relay_False_83()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_83.False(out logic_uScriptAct_SetBool_Target_83);
		local_TechNearCharger2_System_Boolean = logic_uScriptAct_SetBool_Target_83;
	}

	private void Relay_In_85()
	{
		logic_uScriptCon_CompareBool_Bool_85 = local_TechNearCharger2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85.In(logic_uScriptCon_CompareBool_Bool_85);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85.False)
		{
			Relay_In_104();
		}
	}

	private void Relay_In_86()
	{
		int num = 0;
		Array array = msgTechNearCharger2;
		if (logic_uScript_AddOnScreenMessage_locString_86.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_86, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_86, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_86 = local_97_System_String;
		logic_uScript_AddOnScreenMessage_speaker_86 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_86 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_86.In(logic_uScript_AddOnScreenMessage_locString_86, logic_uScript_AddOnScreenMessage_msgPriority_86, logic_uScript_AddOnScreenMessage_holdMsg_86, logic_uScript_AddOnScreenMessage_tag_86, logic_uScript_AddOnScreenMessage_speaker_86, logic_uScript_AddOnScreenMessage_side_86);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_86.Out)
		{
			Relay_In_326();
		}
	}

	private void Relay_In_88()
	{
		logic_uScriptCon_CompareBool_Bool_88 = local_TechDeadCharger2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88.In(logic_uScriptCon_CompareBool_Bool_88);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88.False;
		if (num)
		{
			Relay_In_166();
		}
		if (flag)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_89()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_89 = owner_Connection_76;
		int num = 0;
		Array array = local_ChargerTechs2_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_89.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_89, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_89, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_89 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_89.In(logic_uScript_SetOneTechAsEncounterTarget_owner_89, logic_uScript_SetOneTechAsEncounterTarget_techs_89);
		local_CharTech2_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_89;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_89.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_True_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.True(out logic_uScriptAct_SetBool_Target_93);
		local_TechDeadCharger2_System_Boolean = logic_uScriptAct_SetBool_Target_93;
	}

	private void Relay_False_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.False(out logic_uScriptAct_SetBool_Target_93);
		local_TechDeadCharger2_System_Boolean = logic_uScriptAct_SetBool_Target_93;
	}

	private void Relay_In_95()
	{
		int num = 0;
		Array array = msgTechDeadCharger2;
		if (logic_uScript_AddOnScreenMessage_locString_95.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_95, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_95, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_95 = local_105_System_String;
		logic_uScript_AddOnScreenMessage_speaker_95 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_95 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_95.In(logic_uScript_AddOnScreenMessage_locString_95, logic_uScript_AddOnScreenMessage_msgPriority_95, logic_uScript_AddOnScreenMessage_holdMsg_95, logic_uScript_AddOnScreenMessage_tag_95, logic_uScript_AddOnScreenMessage_speaker_95, logic_uScript_AddOnScreenMessage_side_95);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_95.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_98()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_98 = Trigger2;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_98.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_98);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_98.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_98.OutOfRange;
		if (inRange)
		{
			Relay_In_89();
		}
		if (outOfRange)
		{
			Relay_In_166();
		}
	}

	private void Relay_In_100()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_100 = local_77_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_100.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_100, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_100);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_100.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_104()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_104 = local_103_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_104.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_104, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_104);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_104.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_Save_Out_108()
	{
		Relay_Save_109();
	}

	private void Relay_Load_Out_108()
	{
		Relay_Load_109();
	}

	private void Relay_Restart_Out_108()
	{
		Relay_Set_False_109();
	}

	private void Relay_Save_108()
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_108 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Save(ref logic_SubGraph_SaveLoadBool_boolean_108, logic_SubGraph_SaveLoadBool_boolAsVariable_108, logic_SubGraph_SaveLoadBool_uniqueID_108);
	}

	private void Relay_Load_108()
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_108 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Load(ref logic_SubGraph_SaveLoadBool_boolean_108, logic_SubGraph_SaveLoadBool_boolAsVariable_108, logic_SubGraph_SaveLoadBool_uniqueID_108);
	}

	private void Relay_Set_True_108()
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_108 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_108, logic_SubGraph_SaveLoadBool_boolAsVariable_108, logic_SubGraph_SaveLoadBool_uniqueID_108);
	}

	private void Relay_Set_False_108()
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_108 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_108, logic_SubGraph_SaveLoadBool_boolAsVariable_108, logic_SubGraph_SaveLoadBool_uniqueID_108);
	}

	private void Relay_Save_Out_109()
	{
		Relay_Save_142();
	}

	private void Relay_Load_Out_109()
	{
		Relay_Load_142();
	}

	private void Relay_Restart_Out_109()
	{
		Relay_Set_False_142();
	}

	private void Relay_Save_109()
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_109 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Save(ref logic_SubGraph_SaveLoadBool_boolean_109, logic_SubGraph_SaveLoadBool_boolAsVariable_109, logic_SubGraph_SaveLoadBool_uniqueID_109);
	}

	private void Relay_Load_109()
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_109 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Load(ref logic_SubGraph_SaveLoadBool_boolean_109, logic_SubGraph_SaveLoadBool_boolAsVariable_109, logic_SubGraph_SaveLoadBool_uniqueID_109);
	}

	private void Relay_Set_True_109()
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_109 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_109, logic_SubGraph_SaveLoadBool_boolAsVariable_109, logic_SubGraph_SaveLoadBool_uniqueID_109);
	}

	private void Relay_Set_False_109()
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_109 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_109, logic_SubGraph_SaveLoadBool_boolAsVariable_109, logic_SubGraph_SaveLoadBool_uniqueID_109);
	}

	private void Relay_In_110()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_110 = local_131_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_110.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_110, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_110);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_110.Out)
		{
			Relay_In_121();
		}
	}

	private void Relay_True_113()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_113.True(out logic_uScriptAct_SetBool_Target_113);
		local_TechNearCharger3_System_Boolean = logic_uScriptAct_SetBool_Target_113;
	}

	private void Relay_False_113()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_113.False(out logic_uScriptAct_SetBool_Target_113);
		local_TechNearCharger3_System_Boolean = logic_uScriptAct_SetBool_Target_113;
	}

	private void Relay_In_116()
	{
		logic_uScriptCon_CompareBool_Bool_116 = local_TechNearCharger3_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_116.In(logic_uScriptCon_CompareBool_Bool_116);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_116.True)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_119()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_119 = Trigger3;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_119.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_119);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_119.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_119.OutOfRange;
		if (inRange)
		{
			Relay_In_129();
		}
		if (outOfRange)
		{
			Relay_In_178();
		}
	}

	private void Relay_In_120()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_120 = local_115_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_120.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_120, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_120);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_120.Out)
		{
			Relay_In_137();
		}
	}

	private void Relay_In_121()
	{
		int num = 0;
		Array array = msgTechNearCharger3;
		if (logic_uScript_AddOnScreenMessage_locString_121.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_121, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_121, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_121 = local_122_System_String;
		logic_uScript_AddOnScreenMessage_speaker_121 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_121 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_121.In(logic_uScript_AddOnScreenMessage_locString_121, logic_uScript_AddOnScreenMessage_msgPriority_121, logic_uScript_AddOnScreenMessage_holdMsg_121, logic_uScript_AddOnScreenMessage_tag_121, logic_uScript_AddOnScreenMessage_speaker_121, logic_uScript_AddOnScreenMessage_side_121);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_121.Out)
		{
			Relay_In_341();
		}
	}

	private void Relay_In_128()
	{
		int num = 0;
		Array chargerTech = ChargerTech3;
		if (logic_uScript_GetAndCheckTechs_techData_128.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_128, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_GetAndCheckTechs_techData_128, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_128 = owner_Connection_127;
		int num2 = 0;
		Array array = local_ChargerTechs3_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_128.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_128, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_128, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_128 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.In(logic_uScript_GetAndCheckTechs_techData_128, logic_uScript_GetAndCheckTechs_ownerNode_128, ref logic_uScript_GetAndCheckTechs_techs_128);
		local_ChargerTechs3_TankArray = logic_uScript_GetAndCheckTechs_techs_128;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.AllDead;
		if (allAlive)
		{
			Relay_In_457();
		}
		if (someAlive)
		{
			Relay_In_457();
		}
		if (allDead)
		{
			Relay_In_130();
		}
	}

	private void Relay_In_129()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_129 = owner_Connection_117;
		int num = 0;
		Array array = local_ChargerTechs3_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_129.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_129, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_129, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_129 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_129.In(logic_uScript_SetOneTechAsEncounterTarget_owner_129, logic_uScript_SetOneTechAsEncounterTarget_techs_129);
		local_CharTech3_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_129;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_129.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_In_130()
	{
		logic_uScriptCon_CompareBool_Bool_130 = local_TechDeadCharger3_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130.In(logic_uScriptCon_CompareBool_Bool_130);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130.False;
		if (num)
		{
			Relay_In_190();
		}
		if (flag)
		{
			Relay_In_120();
		}
	}

	private void Relay_True_136()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.True(out logic_uScriptAct_SetBool_Target_136);
		local_TechDeadCharger3_System_Boolean = logic_uScriptAct_SetBool_Target_136;
	}

	private void Relay_False_136()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.False(out logic_uScriptAct_SetBool_Target_136);
		local_TechDeadCharger3_System_Boolean = logic_uScriptAct_SetBool_Target_136;
	}

	private void Relay_In_137()
	{
		int num = 0;
		Array array = msgTechDeadCharger3;
		if (logic_uScript_AddOnScreenMessage_locString_137.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_137, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_137, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_137 = local_135_System_String;
		logic_uScript_AddOnScreenMessage_speaker_137 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_137 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_137.In(logic_uScript_AddOnScreenMessage_locString_137, logic_uScript_AddOnScreenMessage_msgPriority_137, logic_uScript_AddOnScreenMessage_holdMsg_137, logic_uScript_AddOnScreenMessage_tag_137, logic_uScript_AddOnScreenMessage_speaker_137, logic_uScript_AddOnScreenMessage_side_137);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_137.Out)
		{
			Relay_In_167();
		}
	}

	private void Relay_Save_Out_139()
	{
		Relay_Save_158();
	}

	private void Relay_Load_Out_139()
	{
		Relay_Load_158();
	}

	private void Relay_Restart_Out_139()
	{
		Relay_Set_False_158();
	}

	private void Relay_Save_139()
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = local_TechNearCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_139 = local_TechNearCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Save(ref logic_SubGraph_SaveLoadBool_boolean_139, logic_SubGraph_SaveLoadBool_boolAsVariable_139, logic_SubGraph_SaveLoadBool_uniqueID_139);
	}

	private void Relay_Load_139()
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = local_TechNearCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_139 = local_TechNearCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Load(ref logic_SubGraph_SaveLoadBool_boolean_139, logic_SubGraph_SaveLoadBool_boolAsVariable_139, logic_SubGraph_SaveLoadBool_uniqueID_139);
	}

	private void Relay_Set_True_139()
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = local_TechNearCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_139 = local_TechNearCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_139, logic_SubGraph_SaveLoadBool_boolAsVariable_139, logic_SubGraph_SaveLoadBool_uniqueID_139);
	}

	private void Relay_Set_False_139()
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = local_TechNearCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_139 = local_TechNearCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_139, logic_SubGraph_SaveLoadBool_boolAsVariable_139, logic_SubGraph_SaveLoadBool_uniqueID_139);
	}

	private void Relay_Save_Out_142()
	{
		Relay_Save_139();
	}

	private void Relay_Load_Out_142()
	{
		Relay_Load_139();
	}

	private void Relay_Restart_Out_142()
	{
		Relay_Set_False_139();
	}

	private void Relay_Save_142()
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = local_TechDeadCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_142 = local_TechDeadCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Save(ref logic_SubGraph_SaveLoadBool_boolean_142, logic_SubGraph_SaveLoadBool_boolAsVariable_142, logic_SubGraph_SaveLoadBool_uniqueID_142);
	}

	private void Relay_Load_142()
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = local_TechDeadCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_142 = local_TechDeadCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Load(ref logic_SubGraph_SaveLoadBool_boolean_142, logic_SubGraph_SaveLoadBool_boolAsVariable_142, logic_SubGraph_SaveLoadBool_uniqueID_142);
	}

	private void Relay_Set_True_142()
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = local_TechDeadCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_142 = local_TechDeadCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_142, logic_SubGraph_SaveLoadBool_boolAsVariable_142, logic_SubGraph_SaveLoadBool_uniqueID_142);
	}

	private void Relay_Set_False_142()
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = local_TechDeadCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_142 = local_TechDeadCharger3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_142, logic_SubGraph_SaveLoadBool_boolAsVariable_142, logic_SubGraph_SaveLoadBool_uniqueID_142);
	}

	private void Relay_In_145()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_145 = owner_Connection_146;
		int num = 0;
		Array array = local_BossTechs_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_145.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_145, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_145, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_145 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_145.In(logic_uScript_SetOneTechAsEncounterTarget_owner_145, logic_uScript_SetOneTechAsEncounterTarget_techs_145);
		local_BossTech_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_145;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_145.Out)
		{
			Relay_In_154();
		}
	}

	private void Relay_Out_149()
	{
		Relay_True_21();
	}

	private void Relay_In_149()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_149 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_149.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_149, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_149);
	}

	private void Relay_In_152()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_152 = local_151_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_152.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_152, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_152);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_152.Out)
		{
			Relay_In_4();
		}
	}

	private void Relay_In_154()
	{
		logic_uScriptCon_CompareBool_Bool_154 = local_FinalObjectiveSet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.In(logic_uScriptCon_CompareBool_Bool_154);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.False)
		{
			Relay_In_193();
		}
	}

	private void Relay_True_155()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_155.True(out logic_uScriptAct_SetBool_Target_155);
		local_FinalObjectiveSet_System_Boolean = logic_uScriptAct_SetBool_Target_155;
	}

	private void Relay_False_155()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_155.False(out logic_uScriptAct_SetBool_Target_155);
		local_FinalObjectiveSet_System_Boolean = logic_uScriptAct_SetBool_Target_155;
	}

	private void Relay_Save_Out_158()
	{
		Relay_Save_240();
	}

	private void Relay_Load_Out_158()
	{
		Relay_Load_240();
	}

	private void Relay_Restart_Out_158()
	{
		Relay_Set_False_240();
	}

	private void Relay_Save_158()
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_158 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Save(ref logic_SubGraph_SaveLoadBool_boolean_158, logic_SubGraph_SaveLoadBool_boolAsVariable_158, logic_SubGraph_SaveLoadBool_uniqueID_158);
	}

	private void Relay_Load_158()
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_158 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Load(ref logic_SubGraph_SaveLoadBool_boolean_158, logic_SubGraph_SaveLoadBool_boolAsVariable_158, logic_SubGraph_SaveLoadBool_uniqueID_158);
	}

	private void Relay_Set_True_158()
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_158 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_158, logic_SubGraph_SaveLoadBool_boolAsVariable_158, logic_SubGraph_SaveLoadBool_uniqueID_158);
	}

	private void Relay_Set_False_158()
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_158 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_158, logic_SubGraph_SaveLoadBool_boolAsVariable_158, logic_SubGraph_SaveLoadBool_uniqueID_158);
	}

	private void Relay_In_159()
	{
		logic_uScriptCon_CompareBool_Bool_159 = local_FinalObjectiveSet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.In(logic_uScriptCon_CompareBool_Bool_159);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.False;
		if (num)
		{
			Relay_In_454();
		}
		if (flag)
		{
			Relay_In_51();
		}
	}

	private void Relay_Out_162()
	{
		Relay_True_58();
	}

	private void Relay_In_162()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_162 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_162.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_162, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_162);
	}

	private void Relay_In_163()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_Out_165()
	{
		Relay_True_93();
	}

	private void Relay_In_165()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_165 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_165, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_165);
	}

	private void Relay_In_166()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_166.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_166.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_Out_167()
	{
		Relay_True_136();
	}

	private void Relay_In_167()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_167 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_167, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_167);
	}

	private void Relay_In_170()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_170 = owner_Connection_179;
		int num = 0;
		Array array = local_ChargerTechs1_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_170.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_170, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_170, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_170 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_170.In(logic_uScript_SetOneTechAsEncounterTarget_owner_170, logic_uScript_SetOneTechAsEncounterTarget_techs_170);
		local_CharTech1_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_170;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_170.Out)
		{
			Relay_In_210();
		}
	}

	private void Relay_In_177()
	{
		int num = 0;
		Array chargerTech = ChargerTech2;
		if (logic_uScript_GetAndCheckTechs_techData_177.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_177, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_GetAndCheckTechs_techData_177, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_177 = owner_Connection_172;
		int num2 = 0;
		Array array = local_ChargerTechs2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_177.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_177, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_177, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_177 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_177.In(logic_uScript_GetAndCheckTechs_techData_177, logic_uScript_GetAndCheckTechs_ownerNode_177, ref logic_uScript_GetAndCheckTechs_techs_177);
		local_ChargerTechs2_TankArray = logic_uScript_GetAndCheckTechs_techs_177;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_177.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_177.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_177.AllDead;
		if (allAlive)
		{
			Relay_In_180();
		}
		if (someAlive)
		{
			Relay_In_180();
		}
		if (allDead)
		{
			Relay_In_183();
		}
	}

	private void Relay_In_178()
	{
		int num = 0;
		Array chargerTech = ChargerTech1;
		if (logic_uScript_GetAndCheckTechs_techData_178.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_178, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_GetAndCheckTechs_techData_178, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_178 = owner_Connection_184;
		int num2 = 0;
		Array array = local_ChargerTechs1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_178.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_178, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_178, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_178 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_178.In(logic_uScript_GetAndCheckTechs_techData_178, logic_uScript_GetAndCheckTechs_ownerNode_178, ref logic_uScript_GetAndCheckTechs_techs_178);
		local_ChargerTechs1_TankArray = logic_uScript_GetAndCheckTechs_techs_178;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_178.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_178.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_178.AllDead;
		if (allAlive)
		{
			Relay_In_170();
		}
		if (someAlive)
		{
			Relay_In_170();
		}
		if (allDead)
		{
			Relay_In_177();
		}
	}

	private void Relay_In_180()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_180 = owner_Connection_186;
		int num = 0;
		Array array = local_ChargerTechs2_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_180.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_180, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_180, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_180 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_180.In(logic_uScript_SetOneTechAsEncounterTarget_owner_180, logic_uScript_SetOneTechAsEncounterTarget_techs_180);
		local_CharTech2_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_180;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_180.Out)
		{
			Relay_In_210();
		}
	}

	private void Relay_In_183()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_183 = owner_Connection_182;
		int num = 0;
		Array array = local_ChargerTechs3_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_183.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_183, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_183, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_183 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_183.In(logic_uScript_SetOneTechAsEncounterTarget_owner_183, logic_uScript_SetOneTechAsEncounterTarget_techs_183);
		local_CharTech3_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_183;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_183.Out)
		{
			Relay_In_210();
		}
	}

	private void Relay_In_190()
	{
		logic_uScriptCon_CompareBool_Bool_190 = local_TechDeadCharger2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.In(logic_uScriptCon_CompareBool_Bool_190);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.False;
		if (num)
		{
			Relay_In_192();
		}
		if (flag)
		{
			Relay_In_460();
		}
	}

	private void Relay_In_192()
	{
		logic_uScriptCon_CompareBool_Bool_192 = local_TechDeadCharger1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.In(logic_uScriptCon_CompareBool_Bool_192);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.False;
		if (num)
		{
			Relay_In_145();
		}
		if (flag)
		{
			Relay_In_460();
		}
	}

	private void Relay_In_193()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_193 = local_194_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_193.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_193, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_193);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_193.Out)
		{
			Relay_In_196();
		}
	}

	private void Relay_In_196()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_196 = local_195_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_196.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_196, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_196);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_196.Out)
		{
			Relay_In_202();
		}
	}

	private void Relay_In_200()
	{
		int num = 0;
		Array array = msgTechDeadChargersAll;
		if (logic_uScript_AddOnScreenMessage_locString_200.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_200, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_200, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_200 = local_197_System_String;
		logic_uScript_AddOnScreenMessage_speaker_200 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_200 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200.In(logic_uScript_AddOnScreenMessage_locString_200, logic_uScript_AddOnScreenMessage_msgPriority_200, logic_uScript_AddOnScreenMessage_holdMsg_200, logic_uScript_AddOnScreenMessage_tag_200, logic_uScript_AddOnScreenMessage_speaker_200, logic_uScript_AddOnScreenMessage_side_200);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200.Out)
		{
			Relay_In_254();
		}
	}

	private void Relay_In_202()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_202 = local_201_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_202.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_202, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_202);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_202.Out)
		{
			Relay_In_208();
		}
	}

	private void Relay_In_206()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_206 = local_204_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_206.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_206, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_206);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_206.Out)
		{
			Relay_In_237();
		}
	}

	private void Relay_In_207()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_207 = local_203_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_207.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_207, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_207);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_207.Out)
		{
			Relay_In_206();
		}
	}

	private void Relay_In_208()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_208 = local_205_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_208.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_208, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_208);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_208.Out)
		{
			Relay_In_207();
		}
	}

	private void Relay_In_210()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_210 = Trigger4;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_210.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_210);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_210.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_210.InRange;
		if (num)
		{
			Relay_In_212();
		}
		if (inRange)
		{
			Relay_In_381();
		}
	}

	private void Relay_In_212()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_212 = Trigger5;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_212.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_212);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_212.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_212.InRange;
		if (num)
		{
			Relay_In_214();
		}
		if (inRange)
		{
			Relay_In_382();
		}
	}

	private void Relay_In_214()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_214 = Trigger6;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_214.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_214);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_214.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_214.OutOfRange;
		if (inRange)
		{
			Relay_In_374();
		}
		if (outOfRange)
		{
			Relay_In_384();
		}
	}

	private void Relay_In_216()
	{
		logic_uScriptCon_CompareBool_Bool_216 = local_AproachedSAMEarly_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_216.In(logic_uScriptCon_CompareBool_Bool_216);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_216.False)
		{
			Relay_In_232();
		}
	}

	private void Relay_In_219()
	{
		int num = 0;
		Array array = msgAproachedSAMEarly;
		if (logic_uScript_AddOnScreenMessage_locString_219.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_219, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_219, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_219 = local_230_System_String;
		logic_uScript_AddOnScreenMessage_speaker_219 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_219 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_219.In(logic_uScript_AddOnScreenMessage_locString_219, logic_uScript_AddOnScreenMessage_msgPriority_219, logic_uScript_AddOnScreenMessage_holdMsg_219, logic_uScript_AddOnScreenMessage_tag_219, logic_uScript_AddOnScreenMessage_speaker_219, logic_uScript_AddOnScreenMessage_side_219);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_219.Out)
		{
			Relay_True_231();
		}
	}

	private void Relay_In_220()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_220 = local_229_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_220.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_220, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_220);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_220.Out)
		{
			Relay_In_224();
		}
	}

	private void Relay_In_224()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_224 = local_222_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_224.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_224, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_224);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_224.Out)
		{
			Relay_In_233();
		}
	}

	private void Relay_In_226()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_226 = local_223_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_226.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_226, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_226);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_226.Out)
		{
			Relay_In_220();
		}
	}

	private void Relay_In_228()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_228 = local_221_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_228.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_228, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_228);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_228.Out)
		{
			Relay_In_226();
		}
	}

	private void Relay_True_231()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_231.True(out logic_uScriptAct_SetBool_Target_231);
		local_AproachedSAMEarly_System_Boolean = logic_uScriptAct_SetBool_Target_231;
	}

	private void Relay_False_231()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_231.False(out logic_uScriptAct_SetBool_Target_231);
		local_AproachedSAMEarly_System_Boolean = logic_uScriptAct_SetBool_Target_231;
	}

	private void Relay_In_232()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_232 = local_225_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_232.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_232, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_232);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_232.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_In_233()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_233 = local_227_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_233.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_233, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_233);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_233.Out)
		{
			Relay_In_219();
		}
	}

	private void Relay_In_235()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_235 = local_234_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_235.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_235, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_235);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_235.Out)
		{
			Relay_In_152();
		}
	}

	private void Relay_In_237()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_237 = local_236_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_237.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_237, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_237);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_237.Out)
		{
			Relay_In_200();
		}
	}

	private void Relay_Save_Out_240()
	{
		Relay_Save_253();
	}

	private void Relay_Load_Out_240()
	{
		Relay_Load_253();
	}

	private void Relay_Restart_Out_240()
	{
		Relay_Set_False_253();
	}

	private void Relay_Save_240()
	{
		logic_SubGraph_SaveLoadBool_boolean_240 = local_AproachedSAMEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_240 = local_AproachedSAMEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Save(ref logic_SubGraph_SaveLoadBool_boolean_240, logic_SubGraph_SaveLoadBool_boolAsVariable_240, logic_SubGraph_SaveLoadBool_uniqueID_240);
	}

	private void Relay_Load_240()
	{
		logic_SubGraph_SaveLoadBool_boolean_240 = local_AproachedSAMEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_240 = local_AproachedSAMEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Load(ref logic_SubGraph_SaveLoadBool_boolean_240, logic_SubGraph_SaveLoadBool_boolAsVariable_240, logic_SubGraph_SaveLoadBool_uniqueID_240);
	}

	private void Relay_Set_True_240()
	{
		logic_SubGraph_SaveLoadBool_boolean_240 = local_AproachedSAMEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_240 = local_AproachedSAMEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_240, logic_SubGraph_SaveLoadBool_boolAsVariable_240, logic_SubGraph_SaveLoadBool_uniqueID_240);
	}

	private void Relay_Set_False_240()
	{
		logic_SubGraph_SaveLoadBool_boolean_240 = local_AproachedSAMEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_240 = local_AproachedSAMEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_240.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_240, logic_SubGraph_SaveLoadBool_boolAsVariable_240, logic_SubGraph_SaveLoadBool_uniqueID_240);
	}

	private void Relay_In_242()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_242 = local_241_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_242.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_242, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_242);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_242.Out)
		{
			Relay_In_235();
		}
	}

	private void Relay_In_243()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_243 = owner_Connection_248;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_243.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_243);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_243.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_243.False;
		if (num)
		{
			Relay_Pause_451();
		}
		if (flag)
		{
			Relay_UnPause_452();
		}
	}

	private void Relay_In_245()
	{
		logic_uScriptCon_CompareBool_Bool_245 = local_FoundEncounter_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245.In(logic_uScriptCon_CompareBool_Bool_245);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245.False;
		if (num)
		{
			Relay_In_32();
		}
		if (flag)
		{
			Relay_In_251();
		}
	}

	private void Relay_True_246()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_246.True(out logic_uScriptAct_SetBool_Target_246);
		local_FoundEncounter_System_Boolean = logic_uScriptAct_SetBool_Target_246;
	}

	private void Relay_False_246()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_246.False(out logic_uScriptAct_SetBool_Target_246);
		local_FoundEncounter_System_Boolean = logic_uScriptAct_SetBool_Target_246;
	}

	private void Relay_In_251()
	{
		int num = 0;
		Array array = msgFoundMissionArea;
		if (logic_uScript_AddOnScreenMessage_locString_251.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_251, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_251, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_251 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_251 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_251.In(logic_uScript_AddOnScreenMessage_locString_251, logic_uScript_AddOnScreenMessage_msgPriority_251, logic_uScript_AddOnScreenMessage_holdMsg_251, logic_uScript_AddOnScreenMessage_tag_251, logic_uScript_AddOnScreenMessage_speaker_251, logic_uScript_AddOnScreenMessage_side_251);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_251.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_Save_Out_253()
	{
		Relay_Save_270();
	}

	private void Relay_Load_Out_253()
	{
		Relay_Load_270();
	}

	private void Relay_Restart_Out_253()
	{
		Relay_Set_False_270();
	}

	private void Relay_Save_253()
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = local_FoundEncounter_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_253 = local_FoundEncounter_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Save(ref logic_SubGraph_SaveLoadBool_boolean_253, logic_SubGraph_SaveLoadBool_boolAsVariable_253, logic_SubGraph_SaveLoadBool_uniqueID_253);
	}

	private void Relay_Load_253()
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = local_FoundEncounter_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_253 = local_FoundEncounter_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Load(ref logic_SubGraph_SaveLoadBool_boolean_253, logic_SubGraph_SaveLoadBool_boolAsVariable_253, logic_SubGraph_SaveLoadBool_uniqueID_253);
	}

	private void Relay_Set_True_253()
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = local_FoundEncounter_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_253 = local_FoundEncounter_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_253, logic_SubGraph_SaveLoadBool_boolAsVariable_253, logic_SubGraph_SaveLoadBool_uniqueID_253);
	}

	private void Relay_Set_False_253()
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = local_FoundEncounter_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_253 = local_FoundEncounter_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_253, logic_SubGraph_SaveLoadBool_boolAsVariable_253, logic_SubGraph_SaveLoadBool_uniqueID_253);
	}

	private void Relay_In_254()
	{
		logic_uScript_SetShieldEnabled_targetObject_254 = local_SpecialShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_enable_254 = local_ShieldDisabled_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_254.In(logic_uScript_SetShieldEnabled_targetObject_254, logic_uScript_SetShieldEnabled_enable_254);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_254.Out)
		{
			Relay_True_155();
		}
	}

	private void Relay_In_255()
	{
		logic_uScript_GetTankBlock_tank_255 = local_BossTech_Tank;
		logic_uScript_GetTankBlock_blockType_255 = SpecialShieldBlockData;
		logic_uScript_GetTankBlock_Return_255 = logic_uScript_GetTankBlock_uScript_GetTankBlock_255.In(logic_uScript_GetTankBlock_tank_255, logic_uScript_GetTankBlock_blockType_255);
		local_SpecialShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_255;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_255.Out)
		{
			Relay_In_266();
		}
	}

	private void Relay_AtIndex_256()
	{
		int num = 0;
		Array array = local_BossTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_256.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_256, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_256, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_256.AtIndex(ref logic_uScript_AccessListTech_techList_256, logic_uScript_AccessListTech_index_256, out logic_uScript_AccessListTech_value_256);
		local_BossTechs_TankArray = logic_uScript_AccessListTech_techList_256;
		local_BossTech_Tank = logic_uScript_AccessListTech_value_256;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_256.Out)
		{
			Relay_In_439();
		}
	}

	private void Relay_In_264()
	{
		logic_uScript_SetShieldEnabled_targetObject_264 = local_SpecialShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_enable_264 = local_ShieldEnabled_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_264.In(logic_uScript_SetShieldEnabled_targetObject_264, logic_uScript_SetShieldEnabled_enable_264);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_264.Out)
		{
			Relay_True_267();
		}
	}

	private void Relay_In_266()
	{
		logic_uScriptCon_CompareBool_Bool_266 = local_ShieldSwitchedOn_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_266.In(logic_uScriptCon_CompareBool_Bool_266);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_266.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_266.False;
		if (num)
		{
			Relay_In_159();
		}
		if (flag)
		{
			Relay_In_264();
		}
	}

	private void Relay_True_267()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_267.True(out logic_uScriptAct_SetBool_Target_267);
		local_ShieldSwitchedOn_System_Boolean = logic_uScriptAct_SetBool_Target_267;
	}

	private void Relay_False_267()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_267.False(out logic_uScriptAct_SetBool_Target_267);
		local_ShieldSwitchedOn_System_Boolean = logic_uScriptAct_SetBool_Target_267;
	}

	private void Relay_Save_Out_270()
	{
		Relay_In_352();
	}

	private void Relay_Load_Out_270()
	{
		Relay_In_38();
	}

	private void Relay_Restart_Out_270()
	{
		Relay_In_351();
	}

	private void Relay_Save_270()
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = local_ShieldSwitchedOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_270 = local_ShieldSwitchedOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Save(ref logic_SubGraph_SaveLoadBool_boolean_270, logic_SubGraph_SaveLoadBool_boolAsVariable_270, logic_SubGraph_SaveLoadBool_uniqueID_270);
	}

	private void Relay_Load_270()
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = local_ShieldSwitchedOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_270 = local_ShieldSwitchedOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Load(ref logic_SubGraph_SaveLoadBool_boolean_270, logic_SubGraph_SaveLoadBool_boolAsVariable_270, logic_SubGraph_SaveLoadBool_uniqueID_270);
	}

	private void Relay_Set_True_270()
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = local_ShieldSwitchedOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_270 = local_ShieldSwitchedOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_270, logic_SubGraph_SaveLoadBool_boolAsVariable_270, logic_SubGraph_SaveLoadBool_uniqueID_270);
	}

	private void Relay_Set_False_270()
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = local_ShieldSwitchedOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_270 = local_ShieldSwitchedOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_270, logic_SubGraph_SaveLoadBool_boolAsVariable_270, logic_SubGraph_SaveLoadBool_uniqueID_270);
	}

	private void Relay_In_272()
	{
		logic_uScript_SetShieldEnabled_targetObject_272 = local_SpecialShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_enable_272 = local_ShieldDisabled_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_272.In(logic_uScript_SetShieldEnabled_targetObject_272, logic_uScript_SetShieldEnabled_enable_272);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_272.Out)
		{
			Relay_In_423();
		}
	}

	private void Relay_In_273()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_273.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_273.Out)
		{
			Relay_In_274();
		}
	}

	private void Relay_In_274()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_274.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_274.Out)
		{
			Relay_In_276();
		}
	}

	private void Relay_In_276()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_276 = Trigger7;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_276.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_276);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_276.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_276.InRange;
		if (num)
		{
			Relay_In_284();
		}
		if (inRange)
		{
			Relay_In_278();
		}
	}

	private void Relay_In_278()
	{
		logic_uScriptCon_CompareBool_Bool_278 = local_SpawnedMobs1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_278.In(logic_uScriptCon_CompareBool_Bool_278);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_278.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_278.False;
		if (num)
		{
			Relay_In_284();
		}
		if (flag)
		{
			Relay_InitialSpawn_283();
		}
	}

	private void Relay_True_279()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_279.True(out logic_uScriptAct_SetBool_Target_279);
		local_SpawnedMobs1_System_Boolean = logic_uScriptAct_SetBool_Target_279;
	}

	private void Relay_False_279()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_279.False(out logic_uScriptAct_SetBool_Target_279);
		local_SpawnedMobs1_System_Boolean = logic_uScriptAct_SetBool_Target_279;
	}

	private void Relay_InitialSpawn_283()
	{
		int num = 0;
		Array mobsData = MobsData1;
		if (logic_uScript_SpawnTechsFromData_spawnData_283.Length != num + mobsData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_283, num + mobsData.Length);
		}
		Array.Copy(mobsData, 0, logic_uScript_SpawnTechsFromData_spawnData_283, num, mobsData.Length);
		num += mobsData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_283 = owner_Connection_281;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_283.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_283, logic_uScript_SpawnTechsFromData_ownerNode_283, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_283, logic_uScript_SpawnTechsFromData_allowResurrection_283);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_283.Out)
		{
			Relay_True_279();
		}
	}

	private void Relay_In_284()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_284.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_284.Out)
		{
			Relay_In_285();
		}
	}

	private void Relay_In_285()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_285 = Trigger12;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_285.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_285);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_285.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_285.InRange;
		if (num)
		{
			Relay_In_293();
		}
		if (inRange)
		{
			Relay_In_287();
		}
	}

	private void Relay_In_287()
	{
		logic_uScriptCon_CompareBool_Bool_287 = local_SpawnedMobs2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_287.In(logic_uScriptCon_CompareBool_Bool_287);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_287.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_287.False;
		if (num)
		{
			Relay_In_293();
		}
		if (flag)
		{
			Relay_InitialSpawn_294();
		}
	}

	private void Relay_True_291()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_291.True(out logic_uScriptAct_SetBool_Target_291);
		local_SpawnedMobs2_System_Boolean = logic_uScriptAct_SetBool_Target_291;
	}

	private void Relay_False_291()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_291.False(out logic_uScriptAct_SetBool_Target_291);
		local_SpawnedMobs2_System_Boolean = logic_uScriptAct_SetBool_Target_291;
	}

	private void Relay_In_293()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_293.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_293.Out)
		{
			Relay_In_300();
		}
	}

	private void Relay_InitialSpawn_294()
	{
		int num = 0;
		Array mobsData = MobsData2;
		if (logic_uScript_SpawnTechsFromData_spawnData_294.Length != num + mobsData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_294, num + mobsData.Length);
		}
		Array.Copy(mobsData, 0, logic_uScript_SpawnTechsFromData_spawnData_294, num, mobsData.Length);
		num += mobsData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_294 = owner_Connection_288;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_294.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_294, logic_uScript_SpawnTechsFromData_ownerNode_294, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_294, logic_uScript_SpawnTechsFromData_allowResurrection_294);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_294.Out)
		{
			Relay_True_291();
		}
	}

	private void Relay_InitialSpawn_296()
	{
		int num = 0;
		Array mobsData = MobsData3;
		if (logic_uScript_SpawnTechsFromData_spawnData_296.Length != num + mobsData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_296, num + mobsData.Length);
		}
		Array.Copy(mobsData, 0, logic_uScript_SpawnTechsFromData_spawnData_296, num, mobsData.Length);
		num += mobsData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_296 = owner_Connection_295;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_296.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_296, logic_uScript_SpawnTechsFromData_ownerNode_296, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_296, logic_uScript_SpawnTechsFromData_allowResurrection_296);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_296.Out)
		{
			Relay_True_297();
		}
	}

	private void Relay_True_297()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_297.True(out logic_uScriptAct_SetBool_Target_297);
		local_SpawnedMobs3_System_Boolean = logic_uScriptAct_SetBool_Target_297;
	}

	private void Relay_False_297()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_297.False(out logic_uScriptAct_SetBool_Target_297);
		local_SpawnedMobs3_System_Boolean = logic_uScriptAct_SetBool_Target_297;
	}

	private void Relay_In_300()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_300 = Trigger11;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_300.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_300);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_300.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_300.InRange;
		if (num)
		{
			Relay_In_302();
		}
		if (inRange)
		{
			Relay_In_303();
		}
	}

	private void Relay_In_302()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_302.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_302.Out)
		{
			Relay_In_311();
		}
	}

	private void Relay_In_303()
	{
		logic_uScriptCon_CompareBool_Bool_303 = local_SpawnedMobs3_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_303.In(logic_uScriptCon_CompareBool_Bool_303);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_303.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_303.False;
		if (num)
		{
			Relay_In_302();
		}
		if (flag)
		{
			Relay_InitialSpawn_296();
		}
	}

	private void Relay_InitialSpawn_305()
	{
		int num = 0;
		Array mobsData = MobsData4;
		if (logic_uScript_SpawnTechsFromData_spawnData_305.Length != num + mobsData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_305, num + mobsData.Length);
		}
		Array.Copy(mobsData, 0, logic_uScript_SpawnTechsFromData_spawnData_305, num, mobsData.Length);
		num += mobsData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_305 = owner_Connection_312;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_305.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_305, logic_uScript_SpawnTechsFromData_ownerNode_305, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_305, logic_uScript_SpawnTechsFromData_allowResurrection_305);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_305.Out)
		{
			Relay_True_309();
		}
	}

	private void Relay_In_307()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_307.In();
	}

	private void Relay_True_309()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_309.True(out logic_uScriptAct_SetBool_Target_309);
		local_SpawnedMobs4_System_Boolean = logic_uScriptAct_SetBool_Target_309;
	}

	private void Relay_False_309()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_309.False(out logic_uScriptAct_SetBool_Target_309);
		local_SpawnedMobs4_System_Boolean = logic_uScriptAct_SetBool_Target_309;
	}

	private void Relay_In_311()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_311 = Trigger10;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_311.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_311);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_311.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_311.InRange;
		if (num)
		{
			Relay_In_307();
		}
		if (inRange)
		{
			Relay_In_314();
		}
	}

	private void Relay_In_314()
	{
		logic_uScriptCon_CompareBool_Bool_314 = local_SpawnedMobs4_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_314.In(logic_uScriptCon_CompareBool_Bool_314);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_314.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_314.False;
		if (num)
		{
			Relay_In_307();
		}
		if (flag)
		{
			Relay_InitialSpawn_305();
		}
	}

	private void Relay_InitialSpawn_317()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger8;
		if (logic_uScript_SpawnTechsFromData_spawnData_317.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_317, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_SpawnTechsFromData_spawnData_317, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_317 = owner_Connection_318;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_317.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_317, logic_uScript_SpawnTechsFromData_ownerNode_317, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_317, logic_uScript_SpawnTechsFromData_allowResurrection_317);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_317.Out)
		{
			Relay_True_320();
		}
	}

	private void Relay_In_319()
	{
		logic_uScriptCon_CompareBool_Bool_319 = local_SpawnedMobsTrigger8_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_319.In(logic_uScriptCon_CompareBool_Bool_319);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_319.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_319.False;
		if (num)
		{
			Relay_True_337();
		}
		if (flag)
		{
			Relay_InitialSpawn_317();
		}
	}

	private void Relay_True_320()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_320.True(out logic_uScriptAct_SetBool_Target_320);
		local_SpawnedMobsTrigger8_System_Boolean = logic_uScriptAct_SetBool_Target_320;
	}

	private void Relay_False_320()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_320.False(out logic_uScriptAct_SetBool_Target_320);
		local_SpawnedMobsTrigger8_System_Boolean = logic_uScriptAct_SetBool_Target_320;
	}

	private void Relay_InitialSpawn_323()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger8;
		if (logic_uScript_SpawnTechsFromData_spawnData_323.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_323, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_SpawnTechsFromData_spawnData_323, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_323 = owner_Connection_328;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_323.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_323, logic_uScript_SpawnTechsFromData_ownerNode_323, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_323, logic_uScript_SpawnTechsFromData_allowResurrection_323);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_323.Out)
		{
			Relay_True_325();
		}
	}

	private void Relay_True_325()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_325.True(out logic_uScriptAct_SetBool_Target_325);
		local_SpawnedMobsTrigger8_System_Boolean = logic_uScriptAct_SetBool_Target_325;
	}

	private void Relay_False_325()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_325.False(out logic_uScriptAct_SetBool_Target_325);
		local_SpawnedMobsTrigger8_System_Boolean = logic_uScriptAct_SetBool_Target_325;
	}

	private void Relay_In_326()
	{
		logic_uScriptCon_CompareBool_Bool_326 = local_SpawnedMobsTrigger8_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_326.In(logic_uScriptCon_CompareBool_Bool_326);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_326.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_326.False;
		if (num)
		{
			Relay_In_334();
		}
		if (flag)
		{
			Relay_InitialSpawn_323();
		}
	}

	private void Relay_True_329()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_329.True(out logic_uScriptAct_SetBool_Target_329);
		local_SpawnedMobsTrigger9_System_Boolean = logic_uScriptAct_SetBool_Target_329;
	}

	private void Relay_False_329()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_329.False(out logic_uScriptAct_SetBool_Target_329);
		local_SpawnedMobsTrigger9_System_Boolean = logic_uScriptAct_SetBool_Target_329;
	}

	private void Relay_InitialSpawn_333()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger9;
		if (logic_uScript_SpawnTechsFromData_spawnData_333.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_333, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_SpawnTechsFromData_spawnData_333, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_333 = owner_Connection_332;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_333.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_333, logic_uScript_SpawnTechsFromData_ownerNode_333, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_333, logic_uScript_SpawnTechsFromData_allowResurrection_333);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_333.Out)
		{
			Relay_True_329();
		}
	}

	private void Relay_In_334()
	{
		logic_uScriptCon_CompareBool_Bool_334 = local_SpawnedMobsTrigger9_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_334.In(logic_uScriptCon_CompareBool_Bool_334);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_334.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_334.False;
		if (num)
		{
			Relay_True_83();
		}
		if (flag)
		{
			Relay_InitialSpawn_333();
		}
	}

	private void Relay_True_337()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_337.True(out logic_uScriptAct_SetBool_Target_337);
		local_TechNearCharger1_System_Boolean = logic_uScriptAct_SetBool_Target_337;
	}

	private void Relay_False_337()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_337.False(out logic_uScriptAct_SetBool_Target_337);
		local_TechNearCharger1_System_Boolean = logic_uScriptAct_SetBool_Target_337;
	}

	private void Relay_True_339()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_339.True(out logic_uScriptAct_SetBool_Target_339);
		local_SpawnedMobsTrigger9_System_Boolean = logic_uScriptAct_SetBool_Target_339;
	}

	private void Relay_False_339()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_339.False(out logic_uScriptAct_SetBool_Target_339);
		local_SpawnedMobsTrigger9_System_Boolean = logic_uScriptAct_SetBool_Target_339;
	}

	private void Relay_InitialSpawn_340()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger9;
		if (logic_uScript_SpawnTechsFromData_spawnData_340.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_340, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_SpawnTechsFromData_spawnData_340, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_340 = owner_Connection_338;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_340.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_340, logic_uScript_SpawnTechsFromData_ownerNode_340, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_340, logic_uScript_SpawnTechsFromData_allowResurrection_340);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_340.Out)
		{
			Relay_True_339();
		}
	}

	private void Relay_In_341()
	{
		logic_uScriptCon_CompareBool_Bool_341 = local_SpawnedMobsTrigger9_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_341.In(logic_uScriptCon_CompareBool_Bool_341);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_341.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_341.False;
		if (num)
		{
			Relay_True_113();
		}
		if (flag)
		{
			Relay_InitialSpawn_340();
		}
	}

	private void Relay_In_351()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_351.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_351.Out)
		{
			Relay_Set_False_353();
		}
	}

	private void Relay_In_352()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_352.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_352.Out)
		{
			Relay_Save_353();
		}
	}

	private void Relay_Save_Out_353()
	{
		Relay_Save_354();
	}

	private void Relay_Load_Out_353()
	{
		Relay_Load_354();
	}

	private void Relay_Restart_Out_353()
	{
		Relay_Set_False_354();
	}

	private void Relay_Save_353()
	{
		logic_SubGraph_SaveLoadBool_boolean_353 = local_SpawnedMobs1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_353 = local_SpawnedMobs1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Save(ref logic_SubGraph_SaveLoadBool_boolean_353, logic_SubGraph_SaveLoadBool_boolAsVariable_353, logic_SubGraph_SaveLoadBool_uniqueID_353);
	}

	private void Relay_Load_353()
	{
		logic_SubGraph_SaveLoadBool_boolean_353 = local_SpawnedMobs1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_353 = local_SpawnedMobs1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Load(ref logic_SubGraph_SaveLoadBool_boolean_353, logic_SubGraph_SaveLoadBool_boolAsVariable_353, logic_SubGraph_SaveLoadBool_uniqueID_353);
	}

	private void Relay_Set_True_353()
	{
		logic_SubGraph_SaveLoadBool_boolean_353 = local_SpawnedMobs1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_353 = local_SpawnedMobs1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_353, logic_SubGraph_SaveLoadBool_boolAsVariable_353, logic_SubGraph_SaveLoadBool_uniqueID_353);
	}

	private void Relay_Set_False_353()
	{
		logic_SubGraph_SaveLoadBool_boolean_353 = local_SpawnedMobs1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_353 = local_SpawnedMobs1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_353.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_353, logic_SubGraph_SaveLoadBool_boolAsVariable_353, logic_SubGraph_SaveLoadBool_uniqueID_353);
	}

	private void Relay_Save_Out_354()
	{
		Relay_Save_355();
	}

	private void Relay_Load_Out_354()
	{
		Relay_Load_355();
	}

	private void Relay_Restart_Out_354()
	{
		Relay_Set_False_355();
	}

	private void Relay_Save_354()
	{
		logic_SubGraph_SaveLoadBool_boolean_354 = local_SpawnedMobs2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_354 = local_SpawnedMobs2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Save(ref logic_SubGraph_SaveLoadBool_boolean_354, logic_SubGraph_SaveLoadBool_boolAsVariable_354, logic_SubGraph_SaveLoadBool_uniqueID_354);
	}

	private void Relay_Load_354()
	{
		logic_SubGraph_SaveLoadBool_boolean_354 = local_SpawnedMobs2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_354 = local_SpawnedMobs2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Load(ref logic_SubGraph_SaveLoadBool_boolean_354, logic_SubGraph_SaveLoadBool_boolAsVariable_354, logic_SubGraph_SaveLoadBool_uniqueID_354);
	}

	private void Relay_Set_True_354()
	{
		logic_SubGraph_SaveLoadBool_boolean_354 = local_SpawnedMobs2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_354 = local_SpawnedMobs2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_354, logic_SubGraph_SaveLoadBool_boolAsVariable_354, logic_SubGraph_SaveLoadBool_uniqueID_354);
	}

	private void Relay_Set_False_354()
	{
		logic_SubGraph_SaveLoadBool_boolean_354 = local_SpawnedMobs2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_354 = local_SpawnedMobs2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_354.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_354, logic_SubGraph_SaveLoadBool_boolAsVariable_354, logic_SubGraph_SaveLoadBool_uniqueID_354);
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
		logic_SubGraph_SaveLoadBool_boolean_355 = local_SpawnedMobs3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_355 = local_SpawnedMobs3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Save(ref logic_SubGraph_SaveLoadBool_boolean_355, logic_SubGraph_SaveLoadBool_boolAsVariable_355, logic_SubGraph_SaveLoadBool_uniqueID_355);
	}

	private void Relay_Load_355()
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = local_SpawnedMobs3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_355 = local_SpawnedMobs3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Load(ref logic_SubGraph_SaveLoadBool_boolean_355, logic_SubGraph_SaveLoadBool_boolAsVariable_355, logic_SubGraph_SaveLoadBool_uniqueID_355);
	}

	private void Relay_Set_True_355()
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = local_SpawnedMobs3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_355 = local_SpawnedMobs3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_355.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_355, logic_SubGraph_SaveLoadBool_boolAsVariable_355, logic_SubGraph_SaveLoadBool_uniqueID_355);
	}

	private void Relay_Set_False_355()
	{
		logic_SubGraph_SaveLoadBool_boolean_355 = local_SpawnedMobs3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_355 = local_SpawnedMobs3_System_Boolean;
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
		logic_SubGraph_SaveLoadBool_boolean_356 = local_SpawnedMobs4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_356 = local_SpawnedMobs4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Save(ref logic_SubGraph_SaveLoadBool_boolean_356, logic_SubGraph_SaveLoadBool_boolAsVariable_356, logic_SubGraph_SaveLoadBool_uniqueID_356);
	}

	private void Relay_Load_356()
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = local_SpawnedMobs4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_356 = local_SpawnedMobs4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Load(ref logic_SubGraph_SaveLoadBool_boolean_356, logic_SubGraph_SaveLoadBool_boolAsVariable_356, logic_SubGraph_SaveLoadBool_uniqueID_356);
	}

	private void Relay_Set_True_356()
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = local_SpawnedMobs4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_356 = local_SpawnedMobs4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_356, logic_SubGraph_SaveLoadBool_boolAsVariable_356, logic_SubGraph_SaveLoadBool_uniqueID_356);
	}

	private void Relay_Set_False_356()
	{
		logic_SubGraph_SaveLoadBool_boolean_356 = local_SpawnedMobs4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_356 = local_SpawnedMobs4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_356.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_356, logic_SubGraph_SaveLoadBool_boolAsVariable_356, logic_SubGraph_SaveLoadBool_uniqueID_356);
	}

	private void Relay_Save_Out_357()
	{
		Relay_Save_358();
	}

	private void Relay_Load_Out_357()
	{
		Relay_Load_358();
	}

	private void Relay_Restart_Out_357()
	{
		Relay_Set_False_358();
	}

	private void Relay_Save_357()
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = local_SpawnedMobsTrigger8_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_357 = local_SpawnedMobsTrigger8_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Save(ref logic_SubGraph_SaveLoadBool_boolean_357, logic_SubGraph_SaveLoadBool_boolAsVariable_357, logic_SubGraph_SaveLoadBool_uniqueID_357);
	}

	private void Relay_Load_357()
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = local_SpawnedMobsTrigger8_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_357 = local_SpawnedMobsTrigger8_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Load(ref logic_SubGraph_SaveLoadBool_boolean_357, logic_SubGraph_SaveLoadBool_boolAsVariable_357, logic_SubGraph_SaveLoadBool_uniqueID_357);
	}

	private void Relay_Set_True_357()
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = local_SpawnedMobsTrigger8_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_357 = local_SpawnedMobsTrigger8_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_357, logic_SubGraph_SaveLoadBool_boolAsVariable_357, logic_SubGraph_SaveLoadBool_uniqueID_357);
	}

	private void Relay_Set_False_357()
	{
		logic_SubGraph_SaveLoadBool_boolean_357 = local_SpawnedMobsTrigger8_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_357 = local_SpawnedMobsTrigger8_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_357.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_357, logic_SubGraph_SaveLoadBool_boolAsVariable_357, logic_SubGraph_SaveLoadBool_uniqueID_357);
	}

	private void Relay_Save_Out_358()
	{
		Relay_Save_391();
	}

	private void Relay_Load_Out_358()
	{
		Relay_Load_391();
	}

	private void Relay_Restart_Out_358()
	{
		Relay_Set_False_391();
	}

	private void Relay_Save_358()
	{
		logic_SubGraph_SaveLoadBool_boolean_358 = local_SpawnedMobsTrigger9_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_358 = local_SpawnedMobsTrigger9_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Save(ref logic_SubGraph_SaveLoadBool_boolean_358, logic_SubGraph_SaveLoadBool_boolAsVariable_358, logic_SubGraph_SaveLoadBool_uniqueID_358);
	}

	private void Relay_Load_358()
	{
		logic_SubGraph_SaveLoadBool_boolean_358 = local_SpawnedMobsTrigger9_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_358 = local_SpawnedMobsTrigger9_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Load(ref logic_SubGraph_SaveLoadBool_boolean_358, logic_SubGraph_SaveLoadBool_boolAsVariable_358, logic_SubGraph_SaveLoadBool_uniqueID_358);
	}

	private void Relay_Set_True_358()
	{
		logic_SubGraph_SaveLoadBool_boolean_358 = local_SpawnedMobsTrigger9_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_358 = local_SpawnedMobsTrigger9_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_358, logic_SubGraph_SaveLoadBool_boolAsVariable_358, logic_SubGraph_SaveLoadBool_uniqueID_358);
	}

	private void Relay_Set_False_358()
	{
		logic_SubGraph_SaveLoadBool_boolean_358 = local_SpawnedMobsTrigger9_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_358 = local_SpawnedMobsTrigger9_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_358.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_358, logic_SubGraph_SaveLoadBool_boolAsVariable_358, logic_SubGraph_SaveLoadBool_uniqueID_358);
	}

	private void Relay_True_360()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_360.True(out logic_uScriptAct_SetBool_Target_360);
		local_SpawnedMobsTrigger4_System_Boolean = logic_uScriptAct_SetBool_Target_360;
	}

	private void Relay_False_360()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_360.False(out logic_uScriptAct_SetBool_Target_360);
		local_SpawnedMobsTrigger4_System_Boolean = logic_uScriptAct_SetBool_Target_360;
	}

	private void Relay_InitialSpawn_361()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger4;
		if (logic_uScript_SpawnTechsFromData_spawnData_361.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_361, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_SpawnTechsFromData_spawnData_361, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_361 = owner_Connection_359;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_361.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_361, logic_uScript_SpawnTechsFromData_ownerNode_361, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_361, logic_uScript_SpawnTechsFromData_allowResurrection_361);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_361.Out)
		{
			Relay_True_360();
		}
	}

	private void Relay_In_362()
	{
		logic_uScriptCon_CompareBool_Bool_362 = local_SpawnedMobsTrigger4_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_362.In(logic_uScriptCon_CompareBool_Bool_362);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_362.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_362.False;
		if (num)
		{
			Relay_In_380();
		}
		if (flag)
		{
			Relay_InitialSpawn_361();
		}
	}

	private void Relay_True_366()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_366.True(out logic_uScriptAct_SetBool_Target_366);
		local_SpawnedMobsTrigger5_System_Boolean = logic_uScriptAct_SetBool_Target_366;
	}

	private void Relay_False_366()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_366.False(out logic_uScriptAct_SetBool_Target_366);
		local_SpawnedMobsTrigger5_System_Boolean = logic_uScriptAct_SetBool_Target_366;
	}

	private void Relay_In_370()
	{
		logic_uScriptCon_CompareBool_Bool_370 = local_SpawnedMobsTrigger5_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_370.In(logic_uScriptCon_CompareBool_Bool_370);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_370.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_370.False;
		if (num)
		{
			Relay_In_383();
		}
		if (flag)
		{
			Relay_InitialSpawn_371();
		}
	}

	private void Relay_InitialSpawn_371()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger5;
		if (logic_uScript_SpawnTechsFromData_spawnData_371.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_371, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_SpawnTechsFromData_spawnData_371, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_371 = owner_Connection_372;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_371.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_371, logic_uScript_SpawnTechsFromData_ownerNode_371, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_371, logic_uScript_SpawnTechsFromData_allowResurrection_371);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_371.Out)
		{
			Relay_True_366();
		}
	}

	private void Relay_In_374()
	{
		logic_uScriptCon_CompareBool_Bool_374 = local_SpawnedMobsTrigger6_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_374.In(logic_uScriptCon_CompareBool_Bool_374);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_374.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_374.False;
		if (num)
		{
			Relay_In_385();
		}
		if (flag)
		{
			Relay_InitialSpawn_378();
		}
	}

	private void Relay_True_377()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_377.True(out logic_uScriptAct_SetBool_Target_377);
		local_SpawnedMobsTrigger6_System_Boolean = logic_uScriptAct_SetBool_Target_377;
	}

	private void Relay_False_377()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_377.False(out logic_uScriptAct_SetBool_Target_377);
		local_SpawnedMobsTrigger6_System_Boolean = logic_uScriptAct_SetBool_Target_377;
	}

	private void Relay_InitialSpawn_378()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger6;
		if (logic_uScript_SpawnTechsFromData_spawnData_378.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_378, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_SpawnTechsFromData_spawnData_378, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_378 = owner_Connection_376;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_378.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_378, logic_uScript_SpawnTechsFromData_ownerNode_378, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_378, logic_uScript_SpawnTechsFromData_allowResurrection_378);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_378.Out)
		{
			Relay_True_377();
		}
	}

	private void Relay_In_380()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_380.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_380.Out)
		{
			Relay_In_216();
		}
	}

	private void Relay_In_381()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_381.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_381.Out)
		{
			Relay_In_362();
		}
	}

	private void Relay_In_382()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_382.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_382.Out)
		{
			Relay_In_370();
		}
	}

	private void Relay_In_383()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_383.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_383.Out)
		{
			Relay_In_216();
		}
	}

	private void Relay_In_384()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_384.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_384.Out)
		{
			Relay_In_386();
		}
	}

	private void Relay_In_385()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_385.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_385.Out)
		{
			Relay_In_216();
		}
	}

	private void Relay_In_386()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_386.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_386.Out)
		{
			Relay_In_273();
		}
	}

	private void Relay_In_387()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_387.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_387.Out)
		{
			Relay_In_319();
		}
	}

	private void Relay_Save_Out_391()
	{
		Relay_Save_392();
	}

	private void Relay_Load_Out_391()
	{
		Relay_Load_392();
	}

	private void Relay_Restart_Out_391()
	{
		Relay_Set_False_392();
	}

	private void Relay_Save_391()
	{
		logic_SubGraph_SaveLoadBool_boolean_391 = local_SpawnedMobsTrigger4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_391 = local_SpawnedMobsTrigger4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Save(ref logic_SubGraph_SaveLoadBool_boolean_391, logic_SubGraph_SaveLoadBool_boolAsVariable_391, logic_SubGraph_SaveLoadBool_uniqueID_391);
	}

	private void Relay_Load_391()
	{
		logic_SubGraph_SaveLoadBool_boolean_391 = local_SpawnedMobsTrigger4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_391 = local_SpawnedMobsTrigger4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Load(ref logic_SubGraph_SaveLoadBool_boolean_391, logic_SubGraph_SaveLoadBool_boolAsVariable_391, logic_SubGraph_SaveLoadBool_uniqueID_391);
	}

	private void Relay_Set_True_391()
	{
		logic_SubGraph_SaveLoadBool_boolean_391 = local_SpawnedMobsTrigger4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_391 = local_SpawnedMobsTrigger4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_391, logic_SubGraph_SaveLoadBool_boolAsVariable_391, logic_SubGraph_SaveLoadBool_uniqueID_391);
	}

	private void Relay_Set_False_391()
	{
		logic_SubGraph_SaveLoadBool_boolean_391 = local_SpawnedMobsTrigger4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_391 = local_SpawnedMobsTrigger4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_391.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_391, logic_SubGraph_SaveLoadBool_boolAsVariable_391, logic_SubGraph_SaveLoadBool_uniqueID_391);
	}

	private void Relay_Save_Out_392()
	{
		Relay_Save_393();
	}

	private void Relay_Load_Out_392()
	{
		Relay_Load_393();
	}

	private void Relay_Restart_Out_392()
	{
		Relay_Set_False_393();
	}

	private void Relay_Save_392()
	{
		logic_SubGraph_SaveLoadBool_boolean_392 = local_SpawnedMobsTrigger5_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_392 = local_SpawnedMobsTrigger5_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Save(ref logic_SubGraph_SaveLoadBool_boolean_392, logic_SubGraph_SaveLoadBool_boolAsVariable_392, logic_SubGraph_SaveLoadBool_uniqueID_392);
	}

	private void Relay_Load_392()
	{
		logic_SubGraph_SaveLoadBool_boolean_392 = local_SpawnedMobsTrigger5_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_392 = local_SpawnedMobsTrigger5_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Load(ref logic_SubGraph_SaveLoadBool_boolean_392, logic_SubGraph_SaveLoadBool_boolAsVariable_392, logic_SubGraph_SaveLoadBool_uniqueID_392);
	}

	private void Relay_Set_True_392()
	{
		logic_SubGraph_SaveLoadBool_boolean_392 = local_SpawnedMobsTrigger5_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_392 = local_SpawnedMobsTrigger5_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_392, logic_SubGraph_SaveLoadBool_boolAsVariable_392, logic_SubGraph_SaveLoadBool_uniqueID_392);
	}

	private void Relay_Set_False_392()
	{
		logic_SubGraph_SaveLoadBool_boolean_392 = local_SpawnedMobsTrigger5_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_392 = local_SpawnedMobsTrigger5_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_392.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_392, logic_SubGraph_SaveLoadBool_boolAsVariable_392, logic_SubGraph_SaveLoadBool_uniqueID_392);
	}

	private void Relay_Save_Out_393()
	{
		Relay_Save_446();
	}

	private void Relay_Load_Out_393()
	{
		Relay_Load_446();
	}

	private void Relay_Restart_Out_393()
	{
		Relay_Set_False_446();
	}

	private void Relay_Save_393()
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = local_SpawnedMobsTrigger6_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_393 = local_SpawnedMobsTrigger6_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Save(ref logic_SubGraph_SaveLoadBool_boolean_393, logic_SubGraph_SaveLoadBool_boolAsVariable_393, logic_SubGraph_SaveLoadBool_uniqueID_393);
	}

	private void Relay_Load_393()
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = local_SpawnedMobsTrigger6_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_393 = local_SpawnedMobsTrigger6_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Load(ref logic_SubGraph_SaveLoadBool_boolean_393, logic_SubGraph_SaveLoadBool_boolAsVariable_393, logic_SubGraph_SaveLoadBool_uniqueID_393);
	}

	private void Relay_Set_True_393()
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = local_SpawnedMobsTrigger6_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_393 = local_SpawnedMobsTrigger6_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_393, logic_SubGraph_SaveLoadBool_boolAsVariable_393, logic_SubGraph_SaveLoadBool_uniqueID_393);
	}

	private void Relay_Set_False_393()
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = local_SpawnedMobsTrigger6_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_393 = local_SpawnedMobsTrigger6_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_393, logic_SubGraph_SaveLoadBool_boolAsVariable_393, logic_SubGraph_SaveLoadBool_uniqueID_393);
	}

	private void Relay_True_396()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_396.True(out logic_uScriptAct_SetBool_Target_396);
		local_SpawnedMobsTrigger4_System_Boolean = logic_uScriptAct_SetBool_Target_396;
	}

	private void Relay_False_396()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_396.False(out logic_uScriptAct_SetBool_Target_396);
		local_SpawnedMobsTrigger4_System_Boolean = logic_uScriptAct_SetBool_Target_396;
	}

	private void Relay_In_398()
	{
		logic_uScriptCon_CompareBool_Bool_398 = local_SpawnedMobsTrigger5_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_398.In(logic_uScriptCon_CompareBool_Bool_398);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_398.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_398.False;
		if (num)
		{
			Relay_In_401();
		}
		if (flag)
		{
			Relay_InitialSpawn_415();
		}
	}

	private void Relay_In_401()
	{
		logic_uScriptCon_CompareBool_Bool_401 = local_SpawnedMobsTrigger6_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_401.In(logic_uScriptCon_CompareBool_Bool_401);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_401.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_401.False;
		if (num)
		{
			Relay_In_422();
		}
		if (flag)
		{
			Relay_InitialSpawn_414();
		}
	}

	private void Relay_True_407()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_407.True(out logic_uScriptAct_SetBool_Target_407);
		local_SpawnedMobsTrigger5_System_Boolean = logic_uScriptAct_SetBool_Target_407;
	}

	private void Relay_False_407()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_407.False(out logic_uScriptAct_SetBool_Target_407);
		local_SpawnedMobsTrigger5_System_Boolean = logic_uScriptAct_SetBool_Target_407;
	}

	private void Relay_In_410()
	{
		logic_uScriptCon_CompareBool_Bool_410 = local_SpawnedMobsTrigger4_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_410.In(logic_uScriptCon_CompareBool_Bool_410);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_410.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_410.False;
		if (num)
		{
			Relay_In_398();
		}
		if (flag)
		{
			Relay_InitialSpawn_413();
		}
	}

	private void Relay_In_411()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_411 = Trigger4;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_411.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_411);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_411.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_411.OutOfRange;
		if (inRange)
		{
			Relay_In_417();
		}
		if (outOfRange)
		{
			Relay_In_412();
		}
	}

	private void Relay_In_412()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_412 = Trigger5;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_412.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_412);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_412.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_412.OutOfRange;
		if (inRange)
		{
			Relay_In_417();
		}
		if (outOfRange)
		{
			Relay_In_420();
		}
	}

	private void Relay_InitialSpawn_413()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger4;
		if (logic_uScript_SpawnTechsFromData_spawnData_413.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_413, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_SpawnTechsFromData_spawnData_413, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_413 = owner_Connection_416;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_413.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_413, logic_uScript_SpawnTechsFromData_ownerNode_413, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_413, logic_uScript_SpawnTechsFromData_allowResurrection_413);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_413.Out)
		{
			Relay_True_396();
		}
	}

	private void Relay_InitialSpawn_414()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger6;
		if (logic_uScript_SpawnTechsFromData_spawnData_414.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_414, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_SpawnTechsFromData_spawnData_414, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_414 = owner_Connection_399;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_414.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_414, logic_uScript_SpawnTechsFromData_ownerNode_414, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_414, logic_uScript_SpawnTechsFromData_allowResurrection_414);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_414.Out)
		{
			Relay_True_419();
		}
	}

	private void Relay_InitialSpawn_415()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger5;
		if (logic_uScript_SpawnTechsFromData_spawnData_415.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_415, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_SpawnTechsFromData_spawnData_415, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_415 = owner_Connection_402;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_415.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_415, logic_uScript_SpawnTechsFromData_ownerNode_415, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_415, logic_uScript_SpawnTechsFromData_allowResurrection_415);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_415.Out)
		{
			Relay_True_407();
		}
	}

	private void Relay_In_417()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_417.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_417.Out)
		{
			Relay_In_410();
		}
	}

	private void Relay_True_419()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_419.True(out logic_uScriptAct_SetBool_Target_419);
		local_SpawnedMobsTrigger6_System_Boolean = logic_uScriptAct_SetBool_Target_419;
	}

	private void Relay_False_419()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_419.False(out logic_uScriptAct_SetBool_Target_419);
		local_SpawnedMobsTrigger6_System_Boolean = logic_uScriptAct_SetBool_Target_419;
	}

	private void Relay_In_420()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_420 = Trigger6;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_420.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_420);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_420.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_420.OutOfRange;
		if (inRange)
		{
			Relay_In_417();
		}
		if (outOfRange)
		{
			Relay_In_401();
		}
	}

	private void Relay_In_422()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_422.In();
	}

	private void Relay_In_423()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_423.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_423.Out)
		{
			Relay_In_426();
		}
	}

	private void Relay_In_426()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger4;
		if (logic_uScript_GetAndCheckTechs_techData_426.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_426, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_GetAndCheckTechs_techData_426, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_426 = owner_Connection_427;
		int num2 = 0;
		Array array = local_Mobs4_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_426.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_426, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_426, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_426 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_426.In(logic_uScript_GetAndCheckTechs_techData_426, logic_uScript_GetAndCheckTechs_ownerNode_426, ref logic_uScript_GetAndCheckTechs_techs_426);
		local_Mobs4_TankArray = logic_uScript_GetAndCheckTechs_techs_426;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_426.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_426.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_426.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_426.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_429();
		}
		if (someAlive)
		{
			Relay_In_429();
		}
		if (allDead)
		{
			Relay_In_431();
		}
		if (waitingToSpawn)
		{
			Relay_In_431();
		}
	}

	private void Relay_In_429()
	{
		int num = 0;
		Array array = local_Mobs4_TankArray;
		if (logic_uScript_DamageTechs_techs_429.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_429, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_429, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_429.In(logic_uScript_DamageTechs_techs_429, logic_uScript_DamageTechs_damagePercentage_429, logic_uScript_DamageTechs_givePlayerCredit_429, logic_uScript_DamageTechs_leaveBlocksPercentage_429);
	}

	private void Relay_In_431()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger5;
		if (logic_uScript_GetAndCheckTechs_techData_431.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_431, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_GetAndCheckTechs_techData_431, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_431 = owner_Connection_432;
		int num2 = 0;
		Array array = local_Mobs5_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_431.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_431, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_431, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_431 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_431.In(logic_uScript_GetAndCheckTechs_techData_431, logic_uScript_GetAndCheckTechs_ownerNode_431, ref logic_uScript_GetAndCheckTechs_techs_431);
		local_Mobs5_TankArray = logic_uScript_GetAndCheckTechs_techs_431;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_431.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_431.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_431.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_431.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_434();
		}
		if (someAlive)
		{
			Relay_In_434();
		}
		if (allDead)
		{
			Relay_In_437();
		}
		if (waitingToSpawn)
		{
			Relay_In_437();
		}
	}

	private void Relay_In_434()
	{
		int num = 0;
		Array array = local_Mobs5_TankArray;
		if (logic_uScript_DamageTechs_techs_434.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_434, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_434, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_434.In(logic_uScript_DamageTechs_techs_434, logic_uScript_DamageTechs_damagePercentage_434, logic_uScript_DamageTechs_givePlayerCredit_434, logic_uScript_DamageTechs_leaveBlocksPercentage_434);
	}

	private void Relay_In_436()
	{
		int num = 0;
		Array array = local_Mobs6_TankArray;
		if (logic_uScript_DamageTechs_techs_436.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_436, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_436, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_436.In(logic_uScript_DamageTechs_techs_436, logic_uScript_DamageTechs_damagePercentage_436, logic_uScript_DamageTechs_givePlayerCredit_436, logic_uScript_DamageTechs_leaveBlocksPercentage_436);
	}

	private void Relay_In_437()
	{
		int num = 0;
		Array mobsDataTrigger = MobsDataTrigger6;
		if (logic_uScript_GetAndCheckTechs_techData_437.Length != num + mobsDataTrigger.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_437, num + mobsDataTrigger.Length);
		}
		Array.Copy(mobsDataTrigger, 0, logic_uScript_GetAndCheckTechs_techData_437, num, mobsDataTrigger.Length);
		num += mobsDataTrigger.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_437 = owner_Connection_435;
		int num2 = 0;
		Array array = local_Mobs6_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_437.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_437, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_437, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_437 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_437.In(logic_uScript_GetAndCheckTechs_techData_437, logic_uScript_GetAndCheckTechs_ownerNode_437, ref logic_uScript_GetAndCheckTechs_techs_437);
		local_Mobs6_TankArray = logic_uScript_GetAndCheckTechs_techs_437;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_437.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_437.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_437.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_437.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_436();
		}
		if (someAlive)
		{
			Relay_In_436();
		}
		if (allDead)
		{
			Relay_In_149();
		}
		if (waitingToSpawn)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_439()
	{
		logic_uScript_SetBatteryChargeAmount_tech_439 = local_BossTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_439.In(logic_uScript_SetBatteryChargeAmount_tech_439, logic_uScript_SetBatteryChargeAmount_chargeAmount_439);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_439.Out)
		{
			Relay_In_255();
		}
	}

	private void Relay_Save_Out_446()
	{
		Relay_Save_447();
	}

	private void Relay_Load_Out_446()
	{
		Relay_Load_447();
	}

	private void Relay_Restart_Out_446()
	{
		Relay_Set_True_447();
	}

	private void Relay_Save_446()
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = local_ShieldDisabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_446 = local_ShieldDisabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Save(ref logic_SubGraph_SaveLoadBool_boolean_446, logic_SubGraph_SaveLoadBool_boolAsVariable_446, logic_SubGraph_SaveLoadBool_uniqueID_446);
	}

	private void Relay_Load_446()
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = local_ShieldDisabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_446 = local_ShieldDisabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Load(ref logic_SubGraph_SaveLoadBool_boolean_446, logic_SubGraph_SaveLoadBool_boolAsVariable_446, logic_SubGraph_SaveLoadBool_uniqueID_446);
	}

	private void Relay_Set_True_446()
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = local_ShieldDisabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_446 = local_ShieldDisabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_446, logic_SubGraph_SaveLoadBool_boolAsVariable_446, logic_SubGraph_SaveLoadBool_uniqueID_446);
	}

	private void Relay_Set_False_446()
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = local_ShieldDisabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_446 = local_ShieldDisabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_446, logic_SubGraph_SaveLoadBool_boolAsVariable_446, logic_SubGraph_SaveLoadBool_uniqueID_446);
	}

	private void Relay_Save_Out_447()
	{
	}

	private void Relay_Load_Out_447()
	{
	}

	private void Relay_Restart_Out_447()
	{
	}

	private void Relay_Save_447()
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = local_ShieldEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_447 = local_ShieldEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Save(ref logic_SubGraph_SaveLoadBool_boolean_447, logic_SubGraph_SaveLoadBool_boolAsVariable_447, logic_SubGraph_SaveLoadBool_uniqueID_447);
	}

	private void Relay_Load_447()
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = local_ShieldEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_447 = local_ShieldEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Load(ref logic_SubGraph_SaveLoadBool_boolean_447, logic_SubGraph_SaveLoadBool_boolAsVariable_447, logic_SubGraph_SaveLoadBool_uniqueID_447);
	}

	private void Relay_Set_True_447()
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = local_ShieldEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_447 = local_ShieldEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_447, logic_SubGraph_SaveLoadBool_boolAsVariable_447, logic_SubGraph_SaveLoadBool_uniqueID_447);
	}

	private void Relay_Set_False_447()
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = local_ShieldEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_447 = local_ShieldEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_447, logic_SubGraph_SaveLoadBool_boolAsVariable_447, logic_SubGraph_SaveLoadBool_uniqueID_447);
	}

	private void Relay_In_449()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_449 = owner_Connection_450;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_449.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_449);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_449.Out)
		{
			Relay_InitialSpawn_20();
		}
	}

	private void Relay_Pause_451()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_451.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_451.Out)
		{
			Relay_In_245();
		}
	}

	private void Relay_UnPause_451()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_451.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_451.Out)
		{
			Relay_In_245();
		}
	}

	private void Relay_Pause_452()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_452.Pause();
	}

	private void Relay_UnPause_452()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_452.UnPause();
	}

	private void Relay_Pause_453()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_453.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_453.Out)
		{
			Relay_Succeed_2();
		}
	}

	private void Relay_UnPause_453()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_453.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_453.Out)
		{
			Relay_Succeed_2();
		}
	}

	private void Relay_In_454()
	{
		logic_uScript_SetShieldEnabled_targetObject_454 = local_SpecialShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_enable_454 = local_ShieldDisabled_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_454.In(logic_uScript_SetShieldEnabled_targetObject_454, logic_uScript_SetShieldEnabled_enable_454);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_454.Out)
		{
			Relay_In_411();
		}
	}

	private void Relay_In_457()
	{
		logic_uScript_SetShieldEnabled_targetObject_457 = local_SpecialShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_enable_457 = local_ShieldEnabled_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_457.In(logic_uScript_SetShieldEnabled_targetObject_457, logic_uScript_SetShieldEnabled_enable_457);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_457.Out)
		{
			Relay_In_119();
		}
	}

	private void Relay_In_460()
	{
		logic_uScript_SetShieldEnabled_targetObject_460 = local_SpecialShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_enable_460 = local_ShieldEnabled_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_460.In(logic_uScript_SetShieldEnabled_targetObject_460, logic_uScript_SetShieldEnabled_enable_460);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_460.Out)
		{
			Relay_In_178();
		}
	}
}
