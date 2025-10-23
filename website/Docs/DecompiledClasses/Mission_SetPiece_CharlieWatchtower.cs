using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_SetPiece_CharlieWatchtower : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public SpawnTechData[] BossTechData = new SpawnTechData[0];

	public SpawnTechData[] ChargerTech1 = new SpawnTechData[0];

	public SpawnTechData[] ChargerTech2 = new SpawnTechData[0];

	[Multiline(3)]
	public string IntroMsg = "";

	private string local_104_System_String = "Tag5";

	private string local_107_System_String = "Tag4";

	private string local_155_System_String = "Tag1";

	private string local_156_System_String = "Tag2";

	private string local_158_System_String = "Tag7";

	private string local_162_System_String = "Tag3";

	private string local_164_System_String = "Tag5";

	private string local_165_System_String = "Tag6";

	private string local_166_System_String = "Tag4";

	private string local_170_System_String = "Tag8";

	private string local_177_System_String = "Tag8";

	private string local_181_System_String = "Tag6";

	private string local_183_System_String = "Tag7";

	private string local_192_System_String = "Tag7";

	private string local_202_System_String = "Tag0";

	private string local_203_System_String = "Tag0";

	private bool local_262_System_Boolean;

	private string local_59_System_String = "Tag1";

	private string local_61_System_String = "Tag1";

	private string local_66_System_String = "Tag3";

	private string local_86_System_String = "Tag3";

	private string local_91_System_String = "Tag2";

	private string local_92_System_String = "Tag2";

	private string local_94_System_String = "Tag4";

	private bool local_AdvanceObjective1_System_Boolean;

	private Tank local_BossTech_Tank;

	private Tank[] local_BossTechs_TankArray = new Tank[0];

	private Tank[] local_ChargerTechs1_TankArray = new Tank[0];

	private Tank[] local_ChargerTechs2_TankArray = new Tank[0];

	private Tank local_CharTech1_Tank;

	private Tank local_CharTech2_Tank;

	private bool local_FinalObjectiveSet_System_Boolean;

	private bool local_IntroMsgPlayed_System_Boolean;

	private bool local_ObjectiveComplete_System_Boolean;

	private bool local_ShieldsOn_System_Boolean;

	private bool local_ShieldStateEnabled_System_Boolean = true;

	private TankBlock local_SpecialShield_TankBlock;

	private int local_Stage_System_Int32 = 1;

	private bool local_TechDeadBoss_System_Boolean;

	private bool local_TechDeadCharger1_System_Boolean;

	private bool local_TechDeadCharger2_System_Boolean;

	private bool local_TechNearBoss_System_Boolean;

	private bool local_TechNearCharger1_System_Boolean;

	private bool local_TechNearCharger2_System_Boolean;

	private bool local_TechsSpawned_System_Boolean;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgComplete = new LocalisedString[0];

	public LocalisedString[] msgNPCGreeting = new LocalisedString[0];

	public LocalisedString[] msgTechDeadCharger1 = new LocalisedString[0];

	public LocalisedString[] msgTechDeadCharger2 = new LocalisedString[0];

	public LocalisedString[] msgTechDeadChargersAll = new LocalisedString[0];

	public LocalisedString[] msgTechNearBoss = new LocalisedString[0];

	public LocalisedString[] msgTechNearCharger1 = new LocalisedString[0];

	public LocalisedString[] msgTechNearCharger2 = new LocalisedString[0];

	public BlockTypes Special_Shield_Data;

	[Multiline(3)]
	public string Trigger1 = "";

	[Multiline(3)]
	public string Trigger2 = "";

	[Multiline(3)]
	public string Trigger3 = "";

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_4;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_32;

	private GameObject owner_Connection_35;

	private GameObject owner_Connection_39;

	private GameObject owner_Connection_65;

	private GameObject owner_Connection_90;

	private GameObject owner_Connection_115;

	private GameObject owner_Connection_136;

	private GameObject owner_Connection_142;

	private GameObject owner_Connection_145;

	private GameObject owner_Connection_147;

	private GameObject owner_Connection_178;

	private GameObject owner_Connection_189;

	private GameObject owner_Connection_241;

	private GameObject owner_Connection_257;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_9 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_9;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_9 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_9;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_9 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_13;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_13 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_13 = "TechsSpawned";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_14;

	private bool logic_uScriptCon_CompareBool_True_14 = true;

	private bool logic_uScriptCon_CompareBool_False_14 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_15 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_15 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_15;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_15 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_15;

	private bool logic_uScript_SpawnTechsFromData_Out_15 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_16 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_16;

	private bool logic_uScriptAct_SetBool_Out_16 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_16 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_16 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_17 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_17 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_17;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_17 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_17;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_17 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_17 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_17 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_17 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_18;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_18 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_18 = "ObjectiveComplete";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_22;

	private bool logic_uScriptCon_CompareBool_True_22 = true;

	private bool logic_uScriptCon_CompareBool_False_22 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_24 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_24;

	private bool logic_uScriptAct_SetBool_Out_24 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_24 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_24 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_28 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_28;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_29 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_29;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_29 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_29 = "Stage";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_33 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_33 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_33;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_33 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_33;

	private bool logic_uScript_SpawnTechsFromData_Out_33 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_34 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_34 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_34;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_34 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_34;

	private bool logic_uScript_SpawnTechsFromData_Out_34 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_38 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_38;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_38 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_38;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_38 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_38 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_38 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_38 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_42;

	private bool logic_uScriptCon_CompareBool_True_42 = true;

	private bool logic_uScriptCon_CompareBool_False_42 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_45 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_45;

	private bool logic_uScriptAct_SetBool_Out_45 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_45 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_45 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_46 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_46 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_46;

	private string logic_uScript_AddOnScreenMessage_tag_46 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_46;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_46;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_46;

	private bool logic_uScript_AddOnScreenMessage_Out_46 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_46 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_50;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_50 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_50 = "TechDeadCharger1";

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_51 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_51 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_51 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_51 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_51 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_51 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_51 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_54;

	private bool logic_uScriptCon_CompareBool_True_54 = true;

	private bool logic_uScriptCon_CompareBool_False_54 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_57 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_57;

	private bool logic_uScriptAct_SetBool_Out_57 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_57 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_57 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_58 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_58 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_58 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_58;

	private string logic_uScript_AddOnScreenMessage_tag_58 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_58;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_58;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_58;

	private bool logic_uScript_AddOnScreenMessage_Out_58 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_58 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_60 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_60 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_60;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_60 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_64;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_64 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_64 = "TechNearCharger1";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_69 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_69;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_69 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_69;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_69 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_69 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_69 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_69 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_72 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_72;

	private bool logic_uScriptAct_SetBool_Out_72 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_72 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_72 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_74;

	private bool logic_uScriptCon_CompareBool_True_74 = true;

	private bool logic_uScriptCon_CompareBool_False_74 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_75 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_75 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_75 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_75;

	private string logic_uScript_AddOnScreenMessage_tag_75 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_75;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_75;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_75;

	private bool logic_uScript_AddOnScreenMessage_Out_75 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_75 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_77 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_77;

	private bool logic_uScriptCon_CompareBool_True_77 = true;

	private bool logic_uScriptCon_CompareBool_False_77 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_78 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_78;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_78 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_78;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_78 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_82 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_82;

	private bool logic_uScriptAct_SetBool_Out_82 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_82 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_82 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_84 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_84 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_84 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_84;

	private string logic_uScript_AddOnScreenMessage_tag_84 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_84;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_84;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_84;

	private bool logic_uScript_AddOnScreenMessage_Out_84 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_84 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_87 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_87 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_87 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_87 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_87 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_87 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_87 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_89 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_89 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_89;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_89 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_93 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_93 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_93;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_93 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_97;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_97 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_97 = "TechDeadCharger2";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_98;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_98 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_98 = "TechNearCharger2";

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_99 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_99 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_99;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_99 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_101 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_101;

	private bool logic_uScriptAct_SetBool_Out_101 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_101 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_101 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_102 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_102 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_102 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_102 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_102 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_102 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_102 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_103 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_103 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_103;

	private string logic_uScript_AddOnScreenMessage_tag_103 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_103;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_103;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_103;

	private bool logic_uScript_AddOnScreenMessage_Out_103 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_103 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_109;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_109 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_109 = "TechNearCharger3";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_112;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_112 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_112 = "TechDeadCharger3";

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_114 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_114;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_114 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_114;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_114 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_118 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_118;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_118 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_121;

	private bool logic_uScriptCon_CompareBool_True_121 = true;

	private bool logic_uScriptCon_CompareBool_False_121 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_122 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_122;

	private bool logic_uScriptAct_SetBool_Out_122 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_122 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_122 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_125;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_125 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_125 = "FinalObjectiveSet";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_126;

	private bool logic_uScriptCon_CompareBool_True_126 = true;

	private bool logic_uScriptCon_CompareBool_False_126 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_129 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_129;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_129;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_130 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_130 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_132 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_132;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_132;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_133 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_133 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_134 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_134;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_134 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_134;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_134 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_140 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_140 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_140;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_140 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_140;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_140 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_140 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_140 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_140 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_141 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_141 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_141;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_141 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_141;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_141 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_141 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_141 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_141 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_143 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_143;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_143 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_143;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_143 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_151 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_151;

	private bool logic_uScriptCon_CompareBool_True_151 = true;

	private bool logic_uScriptCon_CompareBool_False_151 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_153;

	private bool logic_uScriptCon_CompareBool_True_153 = true;

	private bool logic_uScriptCon_CompareBool_False_153 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_154 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_154 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_154;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_154 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_157 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_157 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_157;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_157 = true;

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

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_163 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_163 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_163;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_163 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_167 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_167 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_167;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_167 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_168 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_168 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_168;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_168 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_169 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_169 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_169;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_169 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_171 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_171 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_171;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_171 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_173 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_173 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_173;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_173 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_174 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_174 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_174;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_174 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_176 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_176 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_176;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_176 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_179 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_179 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_179 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_179;

	private string logic_uScript_AddOnScreenMessage_tag_179 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_179;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_179;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_179;

	private bool logic_uScript_AddOnScreenMessage_Out_179 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_179 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_182 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_182;

	private bool logic_uScript_FinishEncounter_Out_182 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_186;

	private bool logic_uScriptCon_CompareBool_True_186 = true;

	private bool logic_uScriptCon_CompareBool_False_186 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_188 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_188 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_188;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_188 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_188;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_188 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_188 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_188 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_188 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_191 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_191;

	private bool logic_uScriptAct_SetBool_Out_191 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_191 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_191 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_193 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_193 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_193 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_193;

	private string logic_uScript_AddOnScreenMessage_tag_193 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_193;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_193;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_193;

	private bool logic_uScript_AddOnScreenMessage_Out_193 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_193 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_197;

	private bool logic_uScriptCon_CompareBool_True_197 = true;

	private bool logic_uScriptCon_CompareBool_False_197 = true;

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

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_204 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_204 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_204;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_204 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_205 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_205;

	private bool logic_uScriptAct_SetBool_Out_205 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_205 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_205 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_208 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_208 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_208 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_208 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_208 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_208 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_208 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_210 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_210;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_210;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_212;

	private bool logic_uScriptCon_CompareBool_True_212 = true;

	private bool logic_uScriptCon_CompareBool_False_212 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_214 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_214;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_214;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_215 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_215;

	private bool logic_uScriptAct_SetBool_Out_215 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_215 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_215 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_217 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_217 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_218;

	private bool logic_uScriptCon_CompareBool_True_218 = true;

	private bool logic_uScriptCon_CompareBool_False_218 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_220;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_220;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_222 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_222;

	private bool logic_uScriptAct_SetBool_Out_222 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_222 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_222 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_224 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_224 = new Tank[0];

	private int logic_uScript_AccessListTech_index_224;

	private Tank logic_uScript_AccessListTech_value_224;

	private bool logic_uScript_AccessListTech_Out_224 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_227 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_227;

	private bool logic_uScriptCon_CompareBool_True_227 = true;

	private bool logic_uScriptCon_CompareBool_False_227 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_229 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_229 = "";

	private bool logic_uScript_SetShieldEnabled_enable_229;

	private bool logic_uScript_SetShieldEnabled_Out_229 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_230 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_230;

	private BlockTypes logic_uScript_GetTankBlock_blockType_230;

	private TankBlock logic_uScript_GetTankBlock_Return_230;

	private bool logic_uScript_GetTankBlock_Out_230 = true;

	private bool logic_uScript_GetTankBlock_Returned_230 = true;

	private bool logic_uScript_GetTankBlock_NotFound_230 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_235 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_235;

	private bool logic_uScriptAct_SetBool_Out_235 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_235 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_235 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_238 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_238;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_238 = 0.5f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_238 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_240 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_240;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_240;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_244 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_244;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_244 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_244;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_244 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_247;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_247 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_247 = "ShieldStateEnabled";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_250;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_250 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_250 = "ShieldsOn";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_252;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_252 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_252 = "AdvanceObjective1";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_255;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_255 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_255 = "IntroMsgPlayed";

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_256 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_256;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_256 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_256 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_256 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_258 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_258 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_259 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_259;

	private Tank logic_uScript_SetTankInvulnerable_tank_259;

	private bool logic_uScript_SetTankInvulnerable_Out_259 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_261 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_261 = "";

	private bool logic_uScript_SetShieldEnabled_enable_261;

	private bool logic_uScript_SetShieldEnabled_Out_261 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_263 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_263;

	private Tank logic_uScript_SetTankInvulnerable_tank_263;

	private bool logic_uScript_SetTankInvulnerable_Out_263 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_266 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_266 = "";

	private bool logic_uScript_SetShieldEnabled_enable_266;

	private bool logic_uScript_SetShieldEnabled_Out_266 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_268 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_268;

	private Tank logic_uScript_SetTankInvulnerable_tank_268;

	private bool logic_uScript_SetTankInvulnerable_Out_268 = true;

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
		if (null == owner_Connection_4 || !m_RegisteredForEvents)
		{
			owner_Connection_4 = parentGameObject;
		}
		if (null == owner_Connection_5 || !m_RegisteredForEvents)
		{
			owner_Connection_5 = parentGameObject;
		}
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
			if (null != owner_Connection_10)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_10.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_10.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_7;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_7;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_7;
				}
			}
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
		}
		if (null == owner_Connection_32 || !m_RegisteredForEvents)
		{
			owner_Connection_32 = parentGameObject;
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
		if (null == owner_Connection_39 || !m_RegisteredForEvents)
		{
			owner_Connection_39 = parentGameObject;
		}
		if (null == owner_Connection_65 || !m_RegisteredForEvents)
		{
			owner_Connection_65 = parentGameObject;
		}
		if (null == owner_Connection_90 || !m_RegisteredForEvents)
		{
			owner_Connection_90 = parentGameObject;
		}
		if (null == owner_Connection_115 || !m_RegisteredForEvents)
		{
			owner_Connection_115 = parentGameObject;
		}
		if (null == owner_Connection_136 || !m_RegisteredForEvents)
		{
			owner_Connection_136 = parentGameObject;
		}
		if (null == owner_Connection_142 || !m_RegisteredForEvents)
		{
			owner_Connection_142 = parentGameObject;
		}
		if (null == owner_Connection_145 || !m_RegisteredForEvents)
		{
			owner_Connection_145 = parentGameObject;
		}
		if (null == owner_Connection_147 || !m_RegisteredForEvents)
		{
			owner_Connection_147 = parentGameObject;
		}
		if (null == owner_Connection_178 || !m_RegisteredForEvents)
		{
			owner_Connection_178 = parentGameObject;
		}
		if (null == owner_Connection_189 || !m_RegisteredForEvents)
		{
			owner_Connection_189 = parentGameObject;
		}
		if (null == owner_Connection_241 || !m_RegisteredForEvents)
		{
			owner_Connection_241 = parentGameObject;
		}
		if (null == owner_Connection_257 || !m_RegisteredForEvents)
		{
			owner_Connection_257 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_10)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_10.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_10.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_7;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_7;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_7;
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
		if (null != owner_Connection_10)
		{
			uScript_SaveLoad component2 = owner_Connection_10.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_7;
				component2.LoadEvent -= Instance_LoadEvent_7;
				component2.RestartEvent -= Instance_RestartEvent_7;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_9.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_15.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_16.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_17.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_28.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_33.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_34.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_51.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_58.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_60.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_72.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_75.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_77.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_78.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_84.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_87.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_89.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_93.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_99.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_101.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_102.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_114.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_118.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_122.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_129.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_130.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_132.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_133.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_134.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_140.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_141.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_143.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_151.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_154.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_157.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_163.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_167.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_168.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_169.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_171.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_173.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_174.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_176.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_179.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_182.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_188.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_191.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_193.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_204.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_205.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_208.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_210.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_214.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_215.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_217.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_224.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_227.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_229.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_230.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_235.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_238.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_240.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_244.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_256.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_258.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_259.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_261.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_263.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_266.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_268.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_4 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_32 = parentGameObject;
		owner_Connection_35 = parentGameObject;
		owner_Connection_39 = parentGameObject;
		owner_Connection_65 = parentGameObject;
		owner_Connection_90 = parentGameObject;
		owner_Connection_115 = parentGameObject;
		owner_Connection_136 = parentGameObject;
		owner_Connection_142 = parentGameObject;
		owner_Connection_145 = parentGameObject;
		owner_Connection_147 = parentGameObject;
		owner_Connection_178 = parentGameObject;
		owner_Connection_189 = parentGameObject;
		owner_Connection_241 = parentGameObject;
		owner_Connection_257 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_28.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_118.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_129.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_132.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_210.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_214.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_240.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Save_Out += SubGraph_SaveLoadBool_Save_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Load_Out += SubGraph_SaveLoadBool_Load_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save_Out += SubGraph_SaveLoadBool_Save_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load_Out += SubGraph_SaveLoadBool_Load_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_18;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_28.Out += SubGraph_LoadObjectiveStates_Out_28;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Save_Out += SubGraph_SaveLoadInt_Save_Out_29;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Load_Out += SubGraph_SaveLoadInt_Load_Out_29;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_29;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Save_Out += SubGraph_SaveLoadBool_Save_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Load_Out += SubGraph_SaveLoadBool_Load_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Save_Out += SubGraph_SaveLoadBool_Save_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Load_Out += SubGraph_SaveLoadBool_Load_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Save_Out += SubGraph_SaveLoadBool_Save_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Load_Out += SubGraph_SaveLoadBool_Load_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Save_Out += SubGraph_SaveLoadBool_Save_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Load_Out += SubGraph_SaveLoadBool_Load_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Save_Out += SubGraph_SaveLoadBool_Save_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Load_Out += SubGraph_SaveLoadBool_Load_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Save_Out += SubGraph_SaveLoadBool_Save_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Load_Out += SubGraph_SaveLoadBool_Load_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_112;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_118.Out += SubGraph_CompleteObjectiveStage_Out_118;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Save_Out += SubGraph_SaveLoadBool_Save_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Load_Out += SubGraph_SaveLoadBool_Load_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_125;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_129.Out += SubGraph_CompleteObjectiveStage_Out_129;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_132.Out += SubGraph_CompleteObjectiveStage_Out_132;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_210.Out += SubGraph_CompleteObjectiveStage_Out_210;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_214.Out += SubGraph_CompleteObjectiveStage_Out_214;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.Out += SubGraph_CompleteObjectiveStage_Out_220;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_240.Out += SubGraph_CompleteObjectiveStage_Out_240;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Save_Out += SubGraph_SaveLoadBool_Save_Out_247;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Load_Out += SubGraph_SaveLoadBool_Load_Out_247;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_247;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Save_Out += SubGraph_SaveLoadBool_Save_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Load_Out += SubGraph_SaveLoadBool_Load_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Save_Out += SubGraph_SaveLoadBool_Save_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Load_Out += SubGraph_SaveLoadBool_Load_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Save_Out += SubGraph_SaveLoadBool_Save_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Load_Out += SubGraph_SaveLoadBool_Load_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_255;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_28.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_118.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_129.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_132.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_210.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_214.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_240.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_28.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_118.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_129.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_132.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_210.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_214.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_240.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_9.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_28.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_58.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_75.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_78.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_84.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_114.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_118.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_129.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_132.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_134.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_143.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_179.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_193.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_210.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_214.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_230.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_240.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_244.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_256.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_259.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_263.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_268.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_28.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_118.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_129.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_132.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_210.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_214.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_240.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_28.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_118.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_129.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_132.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_210.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_214.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_240.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Save_Out -= SubGraph_SaveLoadBool_Save_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Load_Out -= SubGraph_SaveLoadBool_Load_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save_Out -= SubGraph_SaveLoadBool_Save_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load_Out -= SubGraph_SaveLoadBool_Load_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_18;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_28.Out -= SubGraph_LoadObjectiveStates_Out_28;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Save_Out -= SubGraph_SaveLoadInt_Save_Out_29;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Load_Out -= SubGraph_SaveLoadInt_Load_Out_29;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_29;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Save_Out -= SubGraph_SaveLoadBool_Save_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Load_Out -= SubGraph_SaveLoadBool_Load_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Save_Out -= SubGraph_SaveLoadBool_Save_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Load_Out -= SubGraph_SaveLoadBool_Load_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Save_Out -= SubGraph_SaveLoadBool_Save_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Load_Out -= SubGraph_SaveLoadBool_Load_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Save_Out -= SubGraph_SaveLoadBool_Save_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Load_Out -= SubGraph_SaveLoadBool_Load_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Save_Out -= SubGraph_SaveLoadBool_Save_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Load_Out -= SubGraph_SaveLoadBool_Load_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Save_Out -= SubGraph_SaveLoadBool_Save_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Load_Out -= SubGraph_SaveLoadBool_Load_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_112;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_118.Out -= SubGraph_CompleteObjectiveStage_Out_118;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Save_Out -= SubGraph_SaveLoadBool_Save_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Load_Out -= SubGraph_SaveLoadBool_Load_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_125;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_129.Out -= SubGraph_CompleteObjectiveStage_Out_129;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_132.Out -= SubGraph_CompleteObjectiveStage_Out_132;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_210.Out -= SubGraph_CompleteObjectiveStage_Out_210;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_214.Out -= SubGraph_CompleteObjectiveStage_Out_214;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.Out -= SubGraph_CompleteObjectiveStage_Out_220;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_240.Out -= SubGraph_CompleteObjectiveStage_Out_240;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Save_Out -= SubGraph_SaveLoadBool_Save_Out_247;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Load_Out -= SubGraph_SaveLoadBool_Load_Out_247;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_247;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Save_Out -= SubGraph_SaveLoadBool_Save_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Load_Out -= SubGraph_SaveLoadBool_Load_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Save_Out -= SubGraph_SaveLoadBool_Save_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Load_Out -= SubGraph_SaveLoadBool_Load_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Save_Out -= SubGraph_SaveLoadBool_Save_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Load_Out -= SubGraph_SaveLoadBool_Load_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_255;
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

	private void Instance_SaveEvent_7(object o, EventArgs e)
	{
		Relay_SaveEvent_7();
	}

	private void Instance_LoadEvent_7(object o, EventArgs e)
	{
		Relay_LoadEvent_7();
	}

	private void Instance_RestartEvent_7(object o, EventArgs e)
	{
		Relay_RestartEvent_7();
	}

	private void SubGraph_SaveLoadBool_Save_Out_13(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_13;
		Relay_Save_Out_13();
	}

	private void SubGraph_SaveLoadBool_Load_Out_13(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_13;
		Relay_Load_Out_13();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_13(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_13;
		Relay_Restart_Out_13();
	}

	private void SubGraph_SaveLoadBool_Save_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Save_Out_18();
	}

	private void SubGraph_SaveLoadBool_Load_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Load_Out_18();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Restart_Out_18();
	}

	private void SubGraph_LoadObjectiveStates_Out_28(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_28();
	}

	private void SubGraph_SaveLoadInt_Save_Out_29(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_29 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_29;
		Relay_Save_Out_29();
	}

	private void SubGraph_SaveLoadInt_Load_Out_29(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_29 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_29;
		Relay_Load_Out_29();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_29(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_29 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_29;
		Relay_Restart_Out_29();
	}

	private void SubGraph_SaveLoadBool_Save_Out_50(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = e.boolean;
		local_TechDeadCharger1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_50;
		Relay_Save_Out_50();
	}

	private void SubGraph_SaveLoadBool_Load_Out_50(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = e.boolean;
		local_TechDeadCharger1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_50;
		Relay_Load_Out_50();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_50(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = e.boolean;
		local_TechDeadCharger1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_50;
		Relay_Restart_Out_50();
	}

	private void SubGraph_SaveLoadBool_Save_Out_64(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = e.boolean;
		local_TechNearCharger1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_64;
		Relay_Save_Out_64();
	}

	private void SubGraph_SaveLoadBool_Load_Out_64(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = e.boolean;
		local_TechNearCharger1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_64;
		Relay_Load_Out_64();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_64(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = e.boolean;
		local_TechNearCharger1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_64;
		Relay_Restart_Out_64();
	}

	private void SubGraph_SaveLoadBool_Save_Out_97(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = e.boolean;
		local_TechDeadCharger2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_97;
		Relay_Save_Out_97();
	}

	private void SubGraph_SaveLoadBool_Load_Out_97(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = e.boolean;
		local_TechDeadCharger2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_97;
		Relay_Load_Out_97();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_97(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = e.boolean;
		local_TechDeadCharger2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_97;
		Relay_Restart_Out_97();
	}

	private void SubGraph_SaveLoadBool_Save_Out_98(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = e.boolean;
		local_TechNearCharger2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_98;
		Relay_Save_Out_98();
	}

	private void SubGraph_SaveLoadBool_Load_Out_98(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = e.boolean;
		local_TechNearCharger2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_98;
		Relay_Load_Out_98();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_98(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = e.boolean;
		local_TechNearCharger2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_98;
		Relay_Restart_Out_98();
	}

	private void SubGraph_SaveLoadBool_Save_Out_109(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = e.boolean;
		local_TechNearBoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_109;
		Relay_Save_Out_109();
	}

	private void SubGraph_SaveLoadBool_Load_Out_109(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = e.boolean;
		local_TechNearBoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_109;
		Relay_Load_Out_109();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_109(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = e.boolean;
		local_TechNearBoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_109;
		Relay_Restart_Out_109();
	}

	private void SubGraph_SaveLoadBool_Save_Out_112(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = e.boolean;
		local_TechDeadBoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_112;
		Relay_Save_Out_112();
	}

	private void SubGraph_SaveLoadBool_Load_Out_112(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = e.boolean;
		local_TechDeadBoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_112;
		Relay_Load_Out_112();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_112(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = e.boolean;
		local_TechDeadBoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_112;
		Relay_Restart_Out_112();
	}

	private void SubGraph_CompleteObjectiveStage_Out_118(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_118 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_118;
		Relay_Out_118();
	}

	private void SubGraph_SaveLoadBool_Save_Out_125(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = e.boolean;
		local_FinalObjectiveSet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_125;
		Relay_Save_Out_125();
	}

	private void SubGraph_SaveLoadBool_Load_Out_125(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = e.boolean;
		local_FinalObjectiveSet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_125;
		Relay_Load_Out_125();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_125(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = e.boolean;
		local_FinalObjectiveSet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_125;
		Relay_Restart_Out_125();
	}

	private void SubGraph_CompleteObjectiveStage_Out_129(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_129 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_129;
		Relay_Out_129();
	}

	private void SubGraph_CompleteObjectiveStage_Out_132(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_132 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_132;
		Relay_Out_132();
	}

	private void SubGraph_CompleteObjectiveStage_Out_210(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_210 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_210;
		Relay_Out_210();
	}

	private void SubGraph_CompleteObjectiveStage_Out_214(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_214 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_214;
		Relay_Out_214();
	}

	private void SubGraph_CompleteObjectiveStage_Out_220(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_220 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_220;
		Relay_Out_220();
	}

	private void SubGraph_CompleteObjectiveStage_Out_240(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_240 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_240;
		Relay_Out_240();
	}

	private void SubGraph_SaveLoadBool_Save_Out_247(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = e.boolean;
		local_ShieldStateEnabled_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_247;
		Relay_Save_Out_247();
	}

	private void SubGraph_SaveLoadBool_Load_Out_247(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = e.boolean;
		local_ShieldStateEnabled_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_247;
		Relay_Load_Out_247();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_247(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = e.boolean;
		local_ShieldStateEnabled_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_247;
		Relay_Restart_Out_247();
	}

	private void SubGraph_SaveLoadBool_Save_Out_250(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = e.boolean;
		local_ShieldsOn_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_250;
		Relay_Save_Out_250();
	}

	private void SubGraph_SaveLoadBool_Load_Out_250(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = e.boolean;
		local_ShieldsOn_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_250;
		Relay_Load_Out_250();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_250(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = e.boolean;
		local_ShieldsOn_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_250;
		Relay_Restart_Out_250();
	}

	private void SubGraph_SaveLoadBool_Save_Out_252(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = e.boolean;
		local_AdvanceObjective1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_252;
		Relay_Save_Out_252();
	}

	private void SubGraph_SaveLoadBool_Load_Out_252(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = e.boolean;
		local_AdvanceObjective1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_252;
		Relay_Load_Out_252();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_252(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = e.boolean;
		local_AdvanceObjective1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_252;
		Relay_Restart_Out_252();
	}

	private void SubGraph_SaveLoadBool_Save_Out_255(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = e.boolean;
		local_IntroMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_255;
		Relay_Save_Out_255();
	}

	private void SubGraph_SaveLoadBool_Load_Out_255(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = e.boolean;
		local_IntroMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_255;
		Relay_Load_Out_255();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_255(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = e.boolean;
		local_IntroMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_255;
		Relay_Restart_Out_255();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_256();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_SaveEvent_7()
	{
		Relay_Save_29();
	}

	private void Relay_LoadEvent_7()
	{
		Relay_Load_29();
	}

	private void Relay_RestartEvent_7()
	{
		Relay_Restart_29();
	}

	private void Relay_In_9()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_9 = owner_Connection_11;
		int num = 0;
		Array array = local_ChargerTechs1_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_9.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_9, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_9, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_9 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_9.In(logic_uScript_SetOneTechAsEncounterTarget_owner_9, logic_uScript_SetOneTechAsEncounterTarget_techs_9);
		local_CharTech1_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_9;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_9.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_Save_Out_13()
	{
		Relay_Save_50();
	}

	private void Relay_Load_Out_13()
	{
		Relay_Load_50();
	}

	private void Relay_Restart_Out_13()
	{
		Relay_Set_False_50();
	}

	private void Relay_Save_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Save(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_Load_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Load(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_Set_True_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_Set_False_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_In_14()
	{
		logic_uScriptCon_CompareBool_Bool_14 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.In(logic_uScriptCon_CompareBool_Bool_14);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.False;
		if (num)
		{
			Relay_In_17();
			Relay_In_197();
		}
		if (flag)
		{
			Relay_InitialSpawn_15();
		}
	}

	private void Relay_InitialSpawn_15()
	{
		int num = 0;
		Array bossTechData = BossTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_15.Length != num + bossTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_15, num + bossTechData.Length);
		}
		Array.Copy(bossTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_15, num, bossTechData.Length);
		num += bossTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_15 = owner_Connection_4;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_15.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_15, logic_uScript_SpawnTechsFromData_ownerNode_15, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_15, logic_uScript_SpawnTechsFromData_allowResurrection_15);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_15.Out)
		{
			Relay_InitialSpawn_33();
		}
	}

	private void Relay_True_16()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_16.True(out logic_uScriptAct_SetBool_Target_16);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_16;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_16.Out)
		{
			Relay_In_118();
		}
	}

	private void Relay_False_16()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_16.False(out logic_uScriptAct_SetBool_Target_16);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_16;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_16.Out)
		{
			Relay_In_118();
		}
	}

	private void Relay_In_17()
	{
		int num = 0;
		Array bossTechData = BossTechData;
		if (logic_uScript_GetAndCheckTechs_techData_17.Length != num + bossTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_17, num + bossTechData.Length);
		}
		Array.Copy(bossTechData, 0, logic_uScript_GetAndCheckTechs_techData_17, num, bossTechData.Length);
		num += bossTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_17 = owner_Connection_5;
		int num2 = 0;
		Array array = local_BossTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_17.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_17, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_17, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_17 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_17.In(logic_uScript_GetAndCheckTechs_techData_17, logic_uScript_GetAndCheckTechs_ownerNode_17, ref logic_uScript_GetAndCheckTechs_techs_17);
		local_BossTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_17;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_17.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_17.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_17.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_224();
		}
		if (someAlive)
		{
			Relay_AtIndex_224();
		}
		if (allDead)
		{
			Relay_True_16();
		}
	}

	private void Relay_Save_Out_18()
	{
		Relay_Save_13();
	}

	private void Relay_Load_Out_18()
	{
		Relay_Load_13();
	}

	private void Relay_Restart_Out_18()
	{
		Relay_Set_False_13();
	}

	private void Relay_Save_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Load_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Set_True_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Set_False_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_In_22()
	{
		logic_uScriptCon_CompareBool_Bool_22 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.In(logic_uScriptCon_CompareBool_Bool_22);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.False;
		if (num)
		{
			Relay_In_173();
		}
		if (flag)
		{
			Relay_In_14();
		}
	}

	private void Relay_True_24()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.True(out logic_uScriptAct_SetBool_Target_24);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_24;
	}

	private void Relay_False_24()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.False(out logic_uScriptAct_SetBool_Target_24);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_24;
	}

	private void Relay_Out_28()
	{
	}

	private void Relay_In_28()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_28 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_28.In(logic_SubGraph_LoadObjectiveStates_currentObjective_28);
	}

	private void Relay_Save_Out_29()
	{
		Relay_Save_18();
	}

	private void Relay_Load_Out_29()
	{
		Relay_Load_18();
	}

	private void Relay_Restart_Out_29()
	{
		Relay_Set_False_18();
	}

	private void Relay_Save_29()
	{
		logic_SubGraph_SaveLoadInt_integer_29 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_29 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Save(logic_SubGraph_SaveLoadInt_restartValue_29, ref logic_SubGraph_SaveLoadInt_integer_29, logic_SubGraph_SaveLoadInt_intAsVariable_29, logic_SubGraph_SaveLoadInt_uniqueID_29);
	}

	private void Relay_Load_29()
	{
		logic_SubGraph_SaveLoadInt_integer_29 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_29 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Load(logic_SubGraph_SaveLoadInt_restartValue_29, ref logic_SubGraph_SaveLoadInt_integer_29, logic_SubGraph_SaveLoadInt_intAsVariable_29, logic_SubGraph_SaveLoadInt_uniqueID_29);
	}

	private void Relay_Restart_29()
	{
		logic_SubGraph_SaveLoadInt_integer_29 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_29 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Restart(logic_SubGraph_SaveLoadInt_restartValue_29, ref logic_SubGraph_SaveLoadInt_integer_29, logic_SubGraph_SaveLoadInt_intAsVariable_29, logic_SubGraph_SaveLoadInt_uniqueID_29);
	}

	private void Relay_InitialSpawn_33()
	{
		int num = 0;
		Array chargerTech = ChargerTech1;
		if (logic_uScript_SpawnTechsFromData_spawnData_33.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_33, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_SpawnTechsFromData_spawnData_33, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_33 = owner_Connection_32;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_33.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_33, logic_uScript_SpawnTechsFromData_ownerNode_33, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_33, logic_uScript_SpawnTechsFromData_allowResurrection_33);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_33.Out)
		{
			Relay_InitialSpawn_34();
		}
	}

	private void Relay_InitialSpawn_34()
	{
		int num = 0;
		Array chargerTech = ChargerTech2;
		if (logic_uScript_SpawnTechsFromData_spawnData_34.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_34, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_SpawnTechsFromData_spawnData_34, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_34 = owner_Connection_35;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_34.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_34, logic_uScript_SpawnTechsFromData_ownerNode_34, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_34, logic_uScript_SpawnTechsFromData_allowResurrection_34);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_34.Out)
		{
			Relay_True_24();
		}
	}

	private void Relay_In_38()
	{
		int num = 0;
		Array chargerTech = ChargerTech1;
		if (logic_uScript_GetAndCheckTechs_techData_38.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_38, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_GetAndCheckTechs_techData_38, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_38 = owner_Connection_39;
		int num2 = 0;
		Array array = local_ChargerTechs1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_38.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_38, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_38, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_38 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.In(logic_uScript_GetAndCheckTechs_techData_38, logic_uScript_GetAndCheckTechs_ownerNode_38, ref logic_uScript_GetAndCheckTechs_techs_38);
		local_ChargerTechs1_TankArray = logic_uScript_GetAndCheckTechs_techs_38;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.AllDead;
		if (allAlive)
		{
			Relay_In_51();
		}
		if (someAlive)
		{
			Relay_In_51();
		}
		if (allDead)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_42()
	{
		logic_uScriptCon_CompareBool_Bool_42 = local_TechDeadCharger1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.In(logic_uScriptCon_CompareBool_Bool_42);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.False;
		if (num)
		{
			Relay_In_130();
		}
		if (flag)
		{
			Relay_In_188();
		}
	}

	private void Relay_True_45()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.True(out logic_uScriptAct_SetBool_Target_45);
		local_TechDeadCharger1_System_Boolean = logic_uScriptAct_SetBool_Target_45;
	}

	private void Relay_False_45()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.False(out logic_uScriptAct_SetBool_Target_45);
		local_TechDeadCharger1_System_Boolean = logic_uScriptAct_SetBool_Target_45;
	}

	private void Relay_In_46()
	{
		int num = 0;
		Array array = msgTechDeadCharger1;
		if (logic_uScript_AddOnScreenMessage_locString_46.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_46, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_46, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_46 = local_91_System_String;
		logic_uScript_AddOnScreenMessage_speaker_46 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_46 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46.In(logic_uScript_AddOnScreenMessage_locString_46, logic_uScript_AddOnScreenMessage_msgPriority_46, logic_uScript_AddOnScreenMessage_holdMsg_46, logic_uScript_AddOnScreenMessage_tag_46, logic_uScript_AddOnScreenMessage_speaker_46, logic_uScript_AddOnScreenMessage_side_46);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_Save_Out_50()
	{
		Relay_Save_64();
	}

	private void Relay_Load_Out_50()
	{
		Relay_Load_64();
	}

	private void Relay_Restart_Out_50()
	{
		Relay_Set_False_64();
	}

	private void Relay_Save_50()
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_50 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Save(ref logic_SubGraph_SaveLoadBool_boolean_50, logic_SubGraph_SaveLoadBool_boolAsVariable_50, logic_SubGraph_SaveLoadBool_uniqueID_50);
	}

	private void Relay_Load_50()
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_50 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Load(ref logic_SubGraph_SaveLoadBool_boolean_50, logic_SubGraph_SaveLoadBool_boolAsVariable_50, logic_SubGraph_SaveLoadBool_uniqueID_50);
	}

	private void Relay_Set_True_50()
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_50 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_50, logic_SubGraph_SaveLoadBool_boolAsVariable_50, logic_SubGraph_SaveLoadBool_uniqueID_50);
	}

	private void Relay_Set_False_50()
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_50 = local_TechDeadCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_50, logic_SubGraph_SaveLoadBool_boolAsVariable_50, logic_SubGraph_SaveLoadBool_uniqueID_50);
	}

	private void Relay_In_51()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_51 = Trigger1;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_51.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_51);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_51.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_51.OutOfRange;
		if (inRange)
		{
			Relay_In_9();
			Relay_In_212();
		}
		if (outOfRange)
		{
			Relay_In_130();
		}
	}

	private void Relay_In_54()
	{
		logic_uScriptCon_CompareBool_Bool_54 = local_TechNearCharger1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.In(logic_uScriptCon_CompareBool_Bool_54);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.False)
		{
			Relay_In_58();
		}
	}

	private void Relay_True_57()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.True(out logic_uScriptAct_SetBool_Target_57);
		local_TechNearCharger1_System_Boolean = logic_uScriptAct_SetBool_Target_57;
	}

	private void Relay_False_57()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.False(out logic_uScriptAct_SetBool_Target_57);
		local_TechNearCharger1_System_Boolean = logic_uScriptAct_SetBool_Target_57;
	}

	private void Relay_In_58()
	{
		int num = 0;
		Array array = msgTechNearCharger1;
		if (logic_uScript_AddOnScreenMessage_locString_58.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_58, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_58, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_58 = local_59_System_String;
		logic_uScript_AddOnScreenMessage_speaker_58 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_58 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_58.In(logic_uScript_AddOnScreenMessage_locString_58, logic_uScript_AddOnScreenMessage_msgPriority_58, logic_uScript_AddOnScreenMessage_holdMsg_58, logic_uScript_AddOnScreenMessage_tag_58, logic_uScript_AddOnScreenMessage_speaker_58, logic_uScript_AddOnScreenMessage_side_58);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_58.Out)
		{
			Relay_True_57();
		}
	}

	private void Relay_In_60()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_60 = local_61_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_60.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_60, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_60);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_60.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_Save_Out_64()
	{
		Relay_Save_97();
	}

	private void Relay_Load_Out_64()
	{
		Relay_Load_97();
	}

	private void Relay_Restart_Out_64()
	{
		Relay_Set_False_97();
	}

	private void Relay_Save_64()
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_64 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Save(ref logic_SubGraph_SaveLoadBool_boolean_64, logic_SubGraph_SaveLoadBool_boolAsVariable_64, logic_SubGraph_SaveLoadBool_uniqueID_64);
	}

	private void Relay_Load_64()
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_64 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Load(ref logic_SubGraph_SaveLoadBool_boolean_64, logic_SubGraph_SaveLoadBool_boolAsVariable_64, logic_SubGraph_SaveLoadBool_uniqueID_64);
	}

	private void Relay_Set_True_64()
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_64 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_64, logic_SubGraph_SaveLoadBool_boolAsVariable_64, logic_SubGraph_SaveLoadBool_uniqueID_64);
	}

	private void Relay_Set_False_64()
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_64 = local_TechNearCharger1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_64, logic_SubGraph_SaveLoadBool_boolAsVariable_64, logic_SubGraph_SaveLoadBool_uniqueID_64);
	}

	private void Relay_In_69()
	{
		int num = 0;
		Array chargerTech = ChargerTech2;
		if (logic_uScript_GetAndCheckTechs_techData_69.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_69, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_GetAndCheckTechs_techData_69, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_69 = owner_Connection_90;
		int num2 = 0;
		Array array = local_ChargerTechs2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_69.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_69, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_69, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_69 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.In(logic_uScript_GetAndCheckTechs_techData_69, logic_uScript_GetAndCheckTechs_ownerNode_69, ref logic_uScript_GetAndCheckTechs_techs_69);
		local_ChargerTechs2_TankArray = logic_uScript_GetAndCheckTechs_techs_69;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.AllDead;
		if (allAlive)
		{
			Relay_In_87();
		}
		if (someAlive)
		{
			Relay_In_87();
		}
		if (allDead)
		{
			Relay_In_77();
		}
	}

	private void Relay_True_72()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_72.True(out logic_uScriptAct_SetBool_Target_72);
		local_TechNearBoss_System_Boolean = logic_uScriptAct_SetBool_Target_72;
	}

	private void Relay_False_72()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_72.False(out logic_uScriptAct_SetBool_Target_72);
		local_TechNearBoss_System_Boolean = logic_uScriptAct_SetBool_Target_72;
	}

	private void Relay_In_74()
	{
		logic_uScriptCon_CompareBool_Bool_74 = local_TechNearBoss_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.In(logic_uScriptCon_CompareBool_Bool_74);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.False)
		{
			Relay_In_93();
		}
	}

	private void Relay_In_75()
	{
		int num = 0;
		Array array = msgTechNearBoss;
		if (logic_uScript_AddOnScreenMessage_locString_75.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_75, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_75, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_75 = local_86_System_String;
		logic_uScript_AddOnScreenMessage_speaker_75 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_75 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_75.In(logic_uScript_AddOnScreenMessage_locString_75, logic_uScript_AddOnScreenMessage_msgPriority_75, logic_uScript_AddOnScreenMessage_holdMsg_75, logic_uScript_AddOnScreenMessage_tag_75, logic_uScript_AddOnScreenMessage_speaker_75, logic_uScript_AddOnScreenMessage_side_75);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_75.Out)
		{
			Relay_True_72();
		}
	}

	private void Relay_In_77()
	{
		logic_uScriptCon_CompareBool_Bool_77 = local_TechDeadCharger2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_77.In(logic_uScriptCon_CompareBool_Bool_77);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_77.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_77.False;
		if (num)
		{
			Relay_In_133();
		}
		if (flag)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_78()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_78 = owner_Connection_65;
		int num = 0;
		Array array = local_ChargerTechs2_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_78.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_78, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_78, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_78 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_78.In(logic_uScript_SetOneTechAsEncounterTarget_owner_78, logic_uScript_SetOneTechAsEncounterTarget_techs_78);
		local_CharTech2_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_78;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_78.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_True_82()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.True(out logic_uScriptAct_SetBool_Target_82);
		local_TechDeadCharger2_System_Boolean = logic_uScriptAct_SetBool_Target_82;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_82.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_False_82()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.False(out logic_uScriptAct_SetBool_Target_82);
		local_TechDeadCharger2_System_Boolean = logic_uScriptAct_SetBool_Target_82;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_82.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_In_84()
	{
		int num = 0;
		Array array = msgTechDeadCharger2;
		if (logic_uScript_AddOnScreenMessage_locString_84.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_84, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_84, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_84 = local_94_System_String;
		logic_uScript_AddOnScreenMessage_speaker_84 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_84 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_84.In(logic_uScript_AddOnScreenMessage_locString_84, logic_uScript_AddOnScreenMessage_msgPriority_84, logic_uScript_AddOnScreenMessage_holdMsg_84, logic_uScript_AddOnScreenMessage_tag_84, logic_uScript_AddOnScreenMessage_speaker_84, logic_uScript_AddOnScreenMessage_side_84);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_84.Out)
		{
			Relay_In_132();
		}
	}

	private void Relay_In_87()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_87 = Trigger2;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_87.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_87);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_87.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_87.OutOfRange;
		if (inRange)
		{
			Relay_In_78();
			Relay_In_218();
		}
		if (outOfRange)
		{
			Relay_In_133();
		}
	}

	private void Relay_In_89()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_89 = local_66_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_89.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_89, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_89);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_89.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_93()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_93 = local_92_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_93.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_93, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_93);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_93.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_Save_Out_97()
	{
		Relay_Save_98();
	}

	private void Relay_Load_Out_97()
	{
		Relay_Load_98();
	}

	private void Relay_Restart_Out_97()
	{
		Relay_Set_False_98();
	}

	private void Relay_Save_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Save(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Load_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Load(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Set_True_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Set_False_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_TechDeadCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Save_Out_98()
	{
		Relay_Save_112();
	}

	private void Relay_Load_Out_98()
	{
		Relay_Load_112();
	}

	private void Relay_Restart_Out_98()
	{
		Relay_Set_False_112();
	}

	private void Relay_Save_98()
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_98 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Save(ref logic_SubGraph_SaveLoadBool_boolean_98, logic_SubGraph_SaveLoadBool_boolAsVariable_98, logic_SubGraph_SaveLoadBool_uniqueID_98);
	}

	private void Relay_Load_98()
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_98 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Load(ref logic_SubGraph_SaveLoadBool_boolean_98, logic_SubGraph_SaveLoadBool_boolAsVariable_98, logic_SubGraph_SaveLoadBool_uniqueID_98);
	}

	private void Relay_Set_True_98()
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_98 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_98, logic_SubGraph_SaveLoadBool_boolAsVariable_98, logic_SubGraph_SaveLoadBool_uniqueID_98);
	}

	private void Relay_Set_False_98()
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_98 = local_TechNearCharger2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_98, logic_SubGraph_SaveLoadBool_boolAsVariable_98, logic_SubGraph_SaveLoadBool_uniqueID_98);
	}

	private void Relay_In_99()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_99 = local_107_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_99.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_99, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_99);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_99.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_True_101()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_101.True(out logic_uScriptAct_SetBool_Target_101);
		local_TechNearCharger2_System_Boolean = logic_uScriptAct_SetBool_Target_101;
	}

	private void Relay_False_101()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_101.False(out logic_uScriptAct_SetBool_Target_101);
		local_TechNearCharger2_System_Boolean = logic_uScriptAct_SetBool_Target_101;
	}

	private void Relay_In_102()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_102 = Trigger3;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_102.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_102);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_102.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_102.OutOfRange;
		if (inRange)
		{
			Relay_In_186();
		}
		if (outOfRange)
		{
			Relay_In_141();
		}
	}

	private void Relay_In_103()
	{
		int num = 0;
		Array array = msgTechNearCharger2;
		if (logic_uScript_AddOnScreenMessage_locString_103.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_103, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_103, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_103 = local_104_System_String;
		logic_uScript_AddOnScreenMessage_speaker_103 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_103 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103.In(logic_uScript_AddOnScreenMessage_locString_103, logic_uScript_AddOnScreenMessage_msgPriority_103, logic_uScript_AddOnScreenMessage_holdMsg_103, logic_uScript_AddOnScreenMessage_tag_103, logic_uScript_AddOnScreenMessage_speaker_103, logic_uScript_AddOnScreenMessage_side_103);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103.Out)
		{
			Relay_True_101();
		}
	}

	private void Relay_Save_Out_109()
	{
		Relay_Save_250();
	}

	private void Relay_Load_Out_109()
	{
		Relay_Load_250();
	}

	private void Relay_Restart_Out_109()
	{
		Relay_Set_False_250();
	}

	private void Relay_Save_109()
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = local_TechNearBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_109 = local_TechNearBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Save(ref logic_SubGraph_SaveLoadBool_boolean_109, logic_SubGraph_SaveLoadBool_boolAsVariable_109, logic_SubGraph_SaveLoadBool_uniqueID_109);
	}

	private void Relay_Load_109()
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = local_TechNearBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_109 = local_TechNearBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Load(ref logic_SubGraph_SaveLoadBool_boolean_109, logic_SubGraph_SaveLoadBool_boolAsVariable_109, logic_SubGraph_SaveLoadBool_uniqueID_109);
	}

	private void Relay_Set_True_109()
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = local_TechNearBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_109 = local_TechNearBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_109, logic_SubGraph_SaveLoadBool_boolAsVariable_109, logic_SubGraph_SaveLoadBool_uniqueID_109);
	}

	private void Relay_Set_False_109()
	{
		logic_SubGraph_SaveLoadBool_boolean_109 = local_TechNearBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_109 = local_TechNearBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_109.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_109, logic_SubGraph_SaveLoadBool_boolAsVariable_109, logic_SubGraph_SaveLoadBool_uniqueID_109);
	}

	private void Relay_Save_Out_112()
	{
		Relay_Save_109();
	}

	private void Relay_Load_Out_112()
	{
		Relay_Load_109();
	}

	private void Relay_Restart_Out_112()
	{
		Relay_Set_False_109();
	}

	private void Relay_Save_112()
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = local_TechDeadBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_112 = local_TechDeadBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Save(ref logic_SubGraph_SaveLoadBool_boolean_112, logic_SubGraph_SaveLoadBool_boolAsVariable_112, logic_SubGraph_SaveLoadBool_uniqueID_112);
	}

	private void Relay_Load_112()
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = local_TechDeadBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_112 = local_TechDeadBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Load(ref logic_SubGraph_SaveLoadBool_boolean_112, logic_SubGraph_SaveLoadBool_boolAsVariable_112, logic_SubGraph_SaveLoadBool_uniqueID_112);
	}

	private void Relay_Set_True_112()
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = local_TechDeadBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_112 = local_TechDeadBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_112, logic_SubGraph_SaveLoadBool_boolAsVariable_112, logic_SubGraph_SaveLoadBool_uniqueID_112);
	}

	private void Relay_Set_False_112()
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = local_TechDeadBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_112 = local_TechDeadBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_112, logic_SubGraph_SaveLoadBool_boolAsVariable_112, logic_SubGraph_SaveLoadBool_uniqueID_112);
	}

	private void Relay_In_114()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_114 = owner_Connection_115;
		int num = 0;
		Array array = local_BossTechs_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_114.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_114, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_114, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_114 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_114.In(logic_uScript_SetOneTechAsEncounterTarget_owner_114, logic_uScript_SetOneTechAsEncounterTarget_techs_114);
		local_BossTech_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_114;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_114.Out)
		{
			Relay_In_121();
		}
	}

	private void Relay_Out_118()
	{
	}

	private void Relay_In_118()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_118 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_118.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_118, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_118);
	}

	private void Relay_In_121()
	{
		logic_uScriptCon_CompareBool_Bool_121 = local_FinalObjectiveSet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.In(logic_uScriptCon_CompareBool_Bool_121);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.False)
		{
			Relay_In_204();
		}
	}

	private void Relay_True_122()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_122.True(out logic_uScriptAct_SetBool_Target_122);
		local_FinalObjectiveSet_System_Boolean = logic_uScriptAct_SetBool_Target_122;
	}

	private void Relay_False_122()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_122.False(out logic_uScriptAct_SetBool_Target_122);
		local_FinalObjectiveSet_System_Boolean = logic_uScriptAct_SetBool_Target_122;
	}

	private void Relay_Save_Out_125()
	{
		Relay_Save_247();
	}

	private void Relay_Load_Out_125()
	{
		Relay_Load_247();
	}

	private void Relay_Restart_Out_125()
	{
		Relay_Set_True_247();
	}

	private void Relay_Save_125()
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_125 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Save(ref logic_SubGraph_SaveLoadBool_boolean_125, logic_SubGraph_SaveLoadBool_boolAsVariable_125, logic_SubGraph_SaveLoadBool_uniqueID_125);
	}

	private void Relay_Load_125()
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_125 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Load(ref logic_SubGraph_SaveLoadBool_boolean_125, logic_SubGraph_SaveLoadBool_boolAsVariable_125, logic_SubGraph_SaveLoadBool_uniqueID_125);
	}

	private void Relay_Set_True_125()
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_125 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_125, logic_SubGraph_SaveLoadBool_boolAsVariable_125, logic_SubGraph_SaveLoadBool_uniqueID_125);
	}

	private void Relay_Set_False_125()
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_125 = local_FinalObjectiveSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_125, logic_SubGraph_SaveLoadBool_boolAsVariable_125, logic_SubGraph_SaveLoadBool_uniqueID_125);
	}

	private void Relay_In_126()
	{
		logic_uScriptCon_CompareBool_Bool_126 = local_FinalObjectiveSet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.In(logic_uScriptCon_CompareBool_Bool_126);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.False;
		if (num)
		{
			Relay_In_244();
		}
		if (flag)
		{
			Relay_In_38();
			Relay_In_238();
		}
	}

	private void Relay_Out_129()
	{
		Relay_True_45();
	}

	private void Relay_In_129()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_129 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_129.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_129, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_129);
	}

	private void Relay_In_130()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_130.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_130.Out)
		{
			Relay_In_217();
		}
	}

	private void Relay_Out_132()
	{
		Relay_True_82();
	}

	private void Relay_In_132()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_132 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_132.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_132, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_132);
	}

	private void Relay_In_133()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_133.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_133.Out)
		{
			Relay_In_102();
		}
	}

	private void Relay_In_134()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_134 = owner_Connection_142;
		int num = 0;
		Array array = local_ChargerTechs1_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_134.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_134, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_134, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_134 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_134.In(logic_uScript_SetOneTechAsEncounterTarget_owner_134, logic_uScript_SetOneTechAsEncounterTarget_techs_134);
		local_CharTech1_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_134;
	}

	private void Relay_In_140()
	{
		int num = 0;
		Array chargerTech = ChargerTech2;
		if (logic_uScript_GetAndCheckTechs_techData_140.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_140, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_GetAndCheckTechs_techData_140, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_140 = owner_Connection_136;
		int num2 = 0;
		Array array = local_ChargerTechs2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_140.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_140, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_140, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_140 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_140.In(logic_uScript_GetAndCheckTechs_techData_140, logic_uScript_GetAndCheckTechs_ownerNode_140, ref logic_uScript_GetAndCheckTechs_techs_140);
		local_ChargerTechs2_TankArray = logic_uScript_GetAndCheckTechs_techs_140;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_140.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_140.SomeAlive;
		if (allAlive)
		{
			Relay_In_143();
		}
		if (someAlive)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_141()
	{
		int num = 0;
		Array chargerTech = ChargerTech1;
		if (logic_uScript_GetAndCheckTechs_techData_141.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_141, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_GetAndCheckTechs_techData_141, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_141 = owner_Connection_145;
		int num2 = 0;
		Array array = local_ChargerTechs1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_141.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_141, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_141, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_141 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_141.In(logic_uScript_GetAndCheckTechs_techData_141, logic_uScript_GetAndCheckTechs_ownerNode_141, ref logic_uScript_GetAndCheckTechs_techs_141);
		local_ChargerTechs1_TankArray = logic_uScript_GetAndCheckTechs_techs_141;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_141.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_141.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_141.AllDead;
		if (allAlive)
		{
			Relay_In_134();
		}
		if (someAlive)
		{
			Relay_In_134();
		}
		if (allDead)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_143()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_143 = owner_Connection_147;
		int num = 0;
		Array array = local_ChargerTechs2_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_143.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_143, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_143, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_143 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_143.In(logic_uScript_SetOneTechAsEncounterTarget_owner_143, logic_uScript_SetOneTechAsEncounterTarget_techs_143);
		local_CharTech2_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_143;
	}

	private void Relay_In_151()
	{
		logic_uScriptCon_CompareBool_Bool_151 = local_TechDeadCharger2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_151.In(logic_uScriptCon_CompareBool_Bool_151);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_151.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_151.False;
		if (num)
		{
			Relay_In_153();
		}
		if (flag)
		{
			Relay_In_141();
		}
	}

	private void Relay_In_153()
	{
		logic_uScriptCon_CompareBool_Bool_153 = local_TechDeadCharger1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153.In(logic_uScriptCon_CompareBool_Bool_153);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153.False;
		if (num)
		{
			Relay_In_114();
		}
		if (flag)
		{
			Relay_In_141();
		}
	}

	private void Relay_In_154()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_154 = local_155_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_154.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_154, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_154);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_154.Out)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_157()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_157 = local_156_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_157.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_157, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_157);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_157.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_In_161()
	{
		int num = 0;
		Array array = msgTechDeadChargersAll;
		if (logic_uScript_AddOnScreenMessage_locString_161.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_161, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_161, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_161 = local_158_System_String;
		logic_uScript_AddOnScreenMessage_speaker_161 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_161 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161.In(logic_uScript_AddOnScreenMessage_locString_161, logic_uScript_AddOnScreenMessage_msgPriority_161, logic_uScript_AddOnScreenMessage_holdMsg_161, logic_uScript_AddOnScreenMessage_tag_161, logic_uScript_AddOnScreenMessage_speaker_161, logic_uScript_AddOnScreenMessage_side_161);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161.Out)
		{
			Relay_In_266();
		}
	}

	private void Relay_In_163()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_163 = local_162_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_163.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_163, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_163);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_163.Out)
		{
			Relay_In_169();
		}
	}

	private void Relay_In_167()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_167 = local_165_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_167.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_167, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_167);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_167.Out)
		{
			Relay_In_171();
		}
	}

	private void Relay_In_168()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_168 = local_164_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_168.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_168, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_168);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_168.Out)
		{
			Relay_In_167();
		}
	}

	private void Relay_In_169()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_169 = local_166_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_169.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_169, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_169);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_169.Out)
		{
			Relay_In_168();
		}
	}

	private void Relay_In_171()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_171 = local_170_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_171.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_171, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_171);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_171.Out)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_173()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_173 = local_181_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_173.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_173, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_173);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_173.Out)
		{
			Relay_In_176();
		}
	}

	private void Relay_In_174()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_174 = local_177_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_174.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_174, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_174);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_174.Out)
		{
			Relay_In_179();
		}
	}

	private void Relay_In_176()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_176 = local_183_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_176.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_176, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_176);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_176.Out)
		{
			Relay_In_174();
		}
	}

	private void Relay_In_179()
	{
		int num = 0;
		Array array = msgComplete;
		if (logic_uScript_AddOnScreenMessage_locString_179.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_179, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_179, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_179 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_179 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_179.In(logic_uScript_AddOnScreenMessage_locString_179, logic_uScript_AddOnScreenMessage_msgPriority_179, logic_uScript_AddOnScreenMessage_holdMsg_179, logic_uScript_AddOnScreenMessage_tag_179, logic_uScript_AddOnScreenMessage_speaker_179, logic_uScript_AddOnScreenMessage_side_179);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_179.Out)
		{
			Relay_Succeed_182();
		}
	}

	private void Relay_Succeed_182()
	{
		logic_uScript_FinishEncounter_owner_182 = owner_Connection_178;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_182.Succeed(logic_uScript_FinishEncounter_owner_182);
	}

	private void Relay_Fail_182()
	{
		logic_uScript_FinishEncounter_owner_182 = owner_Connection_178;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_182.Fail(logic_uScript_FinishEncounter_owner_182);
	}

	private void Relay_In_186()
	{
		logic_uScriptCon_CompareBool_Bool_186 = local_TechNearCharger2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186.In(logic_uScriptCon_CompareBool_Bool_186);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186.False)
		{
			Relay_In_99();
		}
	}

	private void Relay_In_188()
	{
		int num = 0;
		Array chargerTech = ChargerTech2;
		if (logic_uScript_GetAndCheckTechs_techData_188.Length != num + chargerTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_188, num + chargerTech.Length);
		}
		Array.Copy(chargerTech, 0, logic_uScript_GetAndCheckTechs_techData_188, num, chargerTech.Length);
		num += chargerTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_188 = owner_Connection_189;
		int num2 = 0;
		Array array = local_ChargerTechs2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_188.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_188, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_188, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_188 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_188.In(logic_uScript_GetAndCheckTechs_techData_188, logic_uScript_GetAndCheckTechs_ownerNode_188, ref logic_uScript_GetAndCheckTechs_techs_188);
		local_ChargerTechs2_TankArray = logic_uScript_GetAndCheckTechs_techs_188;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_188.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_188.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_188.AllDead;
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
			Relay_In_193();
		}
	}

	private void Relay_True_191()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_191.True(out logic_uScriptAct_SetBool_Target_191);
		local_FinalObjectiveSet_System_Boolean = logic_uScriptAct_SetBool_Target_191;
	}

	private void Relay_False_191()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_191.False(out logic_uScriptAct_SetBool_Target_191);
		local_FinalObjectiveSet_System_Boolean = logic_uScriptAct_SetBool_Target_191;
	}

	private void Relay_In_193()
	{
		int num = 0;
		Array array = msgTechDeadChargersAll;
		if (logic_uScript_AddOnScreenMessage_locString_193.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_193, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_193, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_193 = local_192_System_String;
		logic_uScript_AddOnScreenMessage_speaker_193 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_193 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_193.In(logic_uScript_AddOnScreenMessage_locString_193, logic_uScript_AddOnScreenMessage_msgPriority_193, logic_uScript_AddOnScreenMessage_holdMsg_193, logic_uScript_AddOnScreenMessage_tag_193, logic_uScript_AddOnScreenMessage_speaker_193, logic_uScript_AddOnScreenMessage_side_193);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_193.Out)
		{
			Relay_In_240();
		}
	}

	private void Relay_In_197()
	{
		logic_uScriptCon_CompareBool_Bool_197 = local_IntroMsgPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.In(logic_uScriptCon_CompareBool_Bool_197);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.False)
		{
			Relay_In_208();
		}
	}

	private void Relay_In_200()
	{
		int num = 0;
		Array array = msgNPCGreeting;
		if (logic_uScript_AddOnScreenMessage_locString_200.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_200, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_200, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_200 = local_202_System_String;
		logic_uScript_AddOnScreenMessage_speaker_200 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_200 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200.In(logic_uScript_AddOnScreenMessage_locString_200, logic_uScript_AddOnScreenMessage_msgPriority_200, logic_uScript_AddOnScreenMessage_holdMsg_200, logic_uScript_AddOnScreenMessage_tag_200, logic_uScript_AddOnScreenMessage_speaker_200, logic_uScript_AddOnScreenMessage_side_200);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_200.Out)
		{
			Relay_True_205();
		}
	}

	private void Relay_In_204()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_204 = local_203_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_204.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_204, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_204);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_204.Out)
		{
			Relay_In_154();
		}
	}

	private void Relay_True_205()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_205.True(out logic_uScriptAct_SetBool_Target_205);
		local_IntroMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_205;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_205.Out)
		{
			Relay_In_210();
		}
	}

	private void Relay_False_205()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_205.False(out logic_uScriptAct_SetBool_Target_205);
		local_IntroMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_205;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_205.Out)
		{
			Relay_In_210();
		}
	}

	private void Relay_In_208()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_208 = IntroMsg;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_208.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_208);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_208.InRange)
		{
			Relay_In_200();
		}
	}

	private void Relay_Out_210()
	{
	}

	private void Relay_In_210()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_210 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_210.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_210, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_210);
	}

	private void Relay_In_212()
	{
		logic_uScriptCon_CompareBool_Bool_212 = local_AdvanceObjective1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.In(logic_uScriptCon_CompareBool_Bool_212);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.False)
		{
			Relay_In_214();
		}
	}

	private void Relay_Out_214()
	{
		Relay_True_215();
	}

	private void Relay_In_214()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_214 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_214.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_214, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_214);
	}

	private void Relay_True_215()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_215.True(out logic_uScriptAct_SetBool_Target_215);
		local_AdvanceObjective1_System_Boolean = logic_uScriptAct_SetBool_Target_215;
	}

	private void Relay_False_215()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_215.False(out logic_uScriptAct_SetBool_Target_215);
		local_AdvanceObjective1_System_Boolean = logic_uScriptAct_SetBool_Target_215;
	}

	private void Relay_In_217()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_217.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_217.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_218()
	{
		logic_uScriptCon_CompareBool_Bool_218 = local_AdvanceObjective1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.In(logic_uScriptCon_CompareBool_Bool_218);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.False)
		{
			Relay_In_220();
		}
	}

	private void Relay_Out_220()
	{
		Relay_True_222();
	}

	private void Relay_In_220()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_220 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_220, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_220);
	}

	private void Relay_True_222()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.True(out logic_uScriptAct_SetBool_Target_222);
		local_AdvanceObjective1_System_Boolean = logic_uScriptAct_SetBool_Target_222;
	}

	private void Relay_False_222()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.False(out logic_uScriptAct_SetBool_Target_222);
		local_AdvanceObjective1_System_Boolean = logic_uScriptAct_SetBool_Target_222;
	}

	private void Relay_AtIndex_224()
	{
		int num = 0;
		Array array = local_BossTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_224.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_224, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_224, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_224.AtIndex(ref logic_uScript_AccessListTech_techList_224, logic_uScript_AccessListTech_index_224, out logic_uScript_AccessListTech_value_224);
		local_BossTechs_TankArray = logic_uScript_AccessListTech_techList_224;
		local_BossTech_Tank = logic_uScript_AccessListTech_value_224;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_224.Out)
		{
			Relay_In_230();
		}
	}

	private void Relay_In_227()
	{
		logic_uScriptCon_CompareBool_Bool_227 = local_ShieldsOn_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_227.In(logic_uScriptCon_CompareBool_Bool_227);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_227.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_227.False;
		if (num)
		{
			Relay_In_126();
		}
		if (flag)
		{
			Relay_In_229();
		}
	}

	private void Relay_In_229()
	{
		logic_uScript_SetShieldEnabled_targetObject_229 = local_SpecialShield_TankBlock;
		logic_uScript_SetShieldEnabled_enable_229 = local_ShieldStateEnabled_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_229.In(logic_uScript_SetShieldEnabled_targetObject_229, logic_uScript_SetShieldEnabled_enable_229);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_229.Out)
		{
			Relay_In_259();
		}
	}

	private void Relay_In_230()
	{
		logic_uScript_GetTankBlock_tank_230 = local_BossTech_Tank;
		logic_uScript_GetTankBlock_blockType_230 = Special_Shield_Data;
		logic_uScript_GetTankBlock_Return_230 = logic_uScript_GetTankBlock_uScript_GetTankBlock_230.In(logic_uScript_GetTankBlock_tank_230, logic_uScript_GetTankBlock_blockType_230);
		local_SpecialShield_TankBlock = logic_uScript_GetTankBlock_Return_230;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_230.Out)
		{
			Relay_In_227();
		}
	}

	private void Relay_True_235()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_235.True(out logic_uScriptAct_SetBool_Target_235);
		local_ShieldsOn_System_Boolean = logic_uScriptAct_SetBool_Target_235;
	}

	private void Relay_False_235()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_235.False(out logic_uScriptAct_SetBool_Target_235);
		local_ShieldsOn_System_Boolean = logic_uScriptAct_SetBool_Target_235;
	}

	private void Relay_In_238()
	{
		logic_uScript_SetBatteryChargeAmount_tech_238 = local_BossTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_238.In(logic_uScript_SetBatteryChargeAmount_tech_238, logic_uScript_SetBatteryChargeAmount_chargeAmount_238);
	}

	private void Relay_Out_240()
	{
		Relay_True_191();
	}

	private void Relay_In_240()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_240 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_240.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_240, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_240);
	}

	private void Relay_In_244()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_244 = owner_Connection_241;
		int num = 0;
		Array array = local_BossTechs_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_244.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_244, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_244, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_244 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_244.In(logic_uScript_SetOneTechAsEncounterTarget_owner_244, logic_uScript_SetOneTechAsEncounterTarget_techs_244);
		local_BossTech_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_244;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_244.Out)
		{
			Relay_In_261();
		}
	}

	private void Relay_Save_Out_247()
	{
	}

	private void Relay_Load_Out_247()
	{
		Relay_In_28();
	}

	private void Relay_Restart_Out_247()
	{
	}

	private void Relay_Save_247()
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = local_ShieldStateEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_247 = local_ShieldStateEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Save(ref logic_SubGraph_SaveLoadBool_boolean_247, logic_SubGraph_SaveLoadBool_boolAsVariable_247, logic_SubGraph_SaveLoadBool_uniqueID_247);
	}

	private void Relay_Load_247()
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = local_ShieldStateEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_247 = local_ShieldStateEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Load(ref logic_SubGraph_SaveLoadBool_boolean_247, logic_SubGraph_SaveLoadBool_boolAsVariable_247, logic_SubGraph_SaveLoadBool_uniqueID_247);
	}

	private void Relay_Set_True_247()
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = local_ShieldStateEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_247 = local_ShieldStateEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_247, logic_SubGraph_SaveLoadBool_boolAsVariable_247, logic_SubGraph_SaveLoadBool_uniqueID_247);
	}

	private void Relay_Set_False_247()
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = local_ShieldStateEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_247 = local_ShieldStateEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_247, logic_SubGraph_SaveLoadBool_boolAsVariable_247, logic_SubGraph_SaveLoadBool_uniqueID_247);
	}

	private void Relay_Save_Out_250()
	{
		Relay_Save_252();
	}

	private void Relay_Load_Out_250()
	{
		Relay_Load_252();
	}

	private void Relay_Restart_Out_250()
	{
		Relay_Set_False_252();
	}

	private void Relay_Save_250()
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = local_ShieldsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_250 = local_ShieldsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Save(ref logic_SubGraph_SaveLoadBool_boolean_250, logic_SubGraph_SaveLoadBool_boolAsVariable_250, logic_SubGraph_SaveLoadBool_uniqueID_250);
	}

	private void Relay_Load_250()
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = local_ShieldsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_250 = local_ShieldsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Load(ref logic_SubGraph_SaveLoadBool_boolean_250, logic_SubGraph_SaveLoadBool_boolAsVariable_250, logic_SubGraph_SaveLoadBool_uniqueID_250);
	}

	private void Relay_Set_True_250()
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = local_ShieldsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_250 = local_ShieldsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_250, logic_SubGraph_SaveLoadBool_boolAsVariable_250, logic_SubGraph_SaveLoadBool_uniqueID_250);
	}

	private void Relay_Set_False_250()
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = local_ShieldsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_250 = local_ShieldsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_250, logic_SubGraph_SaveLoadBool_boolAsVariable_250, logic_SubGraph_SaveLoadBool_uniqueID_250);
	}

	private void Relay_Save_Out_252()
	{
		Relay_Save_255();
	}

	private void Relay_Load_Out_252()
	{
		Relay_Load_255();
	}

	private void Relay_Restart_Out_252()
	{
		Relay_Set_False_255();
	}

	private void Relay_Save_252()
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = local_AdvanceObjective1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_252 = local_AdvanceObjective1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Save(ref logic_SubGraph_SaveLoadBool_boolean_252, logic_SubGraph_SaveLoadBool_boolAsVariable_252, logic_SubGraph_SaveLoadBool_uniqueID_252);
	}

	private void Relay_Load_252()
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = local_AdvanceObjective1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_252 = local_AdvanceObjective1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Load(ref logic_SubGraph_SaveLoadBool_boolean_252, logic_SubGraph_SaveLoadBool_boolAsVariable_252, logic_SubGraph_SaveLoadBool_uniqueID_252);
	}

	private void Relay_Set_True_252()
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = local_AdvanceObjective1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_252 = local_AdvanceObjective1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_252, logic_SubGraph_SaveLoadBool_boolAsVariable_252, logic_SubGraph_SaveLoadBool_uniqueID_252);
	}

	private void Relay_Set_False_252()
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = local_AdvanceObjective1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_252 = local_AdvanceObjective1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_252, logic_SubGraph_SaveLoadBool_boolAsVariable_252, logic_SubGraph_SaveLoadBool_uniqueID_252);
	}

	private void Relay_Save_Out_255()
	{
		Relay_Save_125();
	}

	private void Relay_Load_Out_255()
	{
		Relay_Load_125();
	}

	private void Relay_Restart_Out_255()
	{
		Relay_Set_False_125();
	}

	private void Relay_Save_255()
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_255 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Save(ref logic_SubGraph_SaveLoadBool_boolean_255, logic_SubGraph_SaveLoadBool_boolAsVariable_255, logic_SubGraph_SaveLoadBool_uniqueID_255);
	}

	private void Relay_Load_255()
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_255 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Load(ref logic_SubGraph_SaveLoadBool_boolean_255, logic_SubGraph_SaveLoadBool_boolAsVariable_255, logic_SubGraph_SaveLoadBool_uniqueID_255);
	}

	private void Relay_Set_True_255()
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_255 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_255, logic_SubGraph_SaveLoadBool_boolAsVariable_255, logic_SubGraph_SaveLoadBool_uniqueID_255);
	}

	private void Relay_Set_False_255()
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_255 = local_IntroMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_255, logic_SubGraph_SaveLoadBool_boolAsVariable_255, logic_SubGraph_SaveLoadBool_uniqueID_255);
	}

	private void Relay_In_256()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_256 = owner_Connection_257;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_256.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_256);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_256.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_256.False;
		if (num)
		{
			Relay_Pause_258();
		}
		if (flag)
		{
			Relay_UnPause_258();
		}
	}

	private void Relay_Pause_258()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_258.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_258.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_UnPause_258()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_258.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_258.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_259()
	{
		logic_uScript_SetTankInvulnerable_invulnerable_259 = local_ShieldStateEnabled_System_Boolean;
		logic_uScript_SetTankInvulnerable_tank_259 = local_BossTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_259.In(logic_uScript_SetTankInvulnerable_invulnerable_259, logic_uScript_SetTankInvulnerable_tank_259);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_259.Out)
		{
			Relay_True_235();
		}
	}

	private void Relay_In_261()
	{
		logic_uScript_SetShieldEnabled_targetObject_261 = local_SpecialShield_TankBlock;
		logic_uScript_SetShieldEnabled_enable_261 = local_262_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_261.In(logic_uScript_SetShieldEnabled_targetObject_261, logic_uScript_SetShieldEnabled_enable_261);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_261.Out)
		{
			Relay_In_263();
		}
	}

	private void Relay_In_263()
	{
		logic_uScript_SetTankInvulnerable_invulnerable_263 = local_262_System_Boolean;
		logic_uScript_SetTankInvulnerable_tank_263 = local_BossTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_263.In(logic_uScript_SetTankInvulnerable_invulnerable_263, logic_uScript_SetTankInvulnerable_tank_263);
	}

	private void Relay_In_266()
	{
		logic_uScript_SetShieldEnabled_targetObject_266 = local_SpecialShield_TankBlock;
		logic_uScript_SetShieldEnabled_enable_266 = local_ShieldStateEnabled_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_266.In(logic_uScript_SetShieldEnabled_targetObject_266, logic_uScript_SetShieldEnabled_enable_266);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_266.Out)
		{
			Relay_In_268();
		}
	}

	private void Relay_In_268()
	{
		logic_uScript_SetTankInvulnerable_invulnerable_268 = local_ShieldStateEnabled_System_Boolean;
		logic_uScript_SetTankInvulnerable_tank_268 = local_BossTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_268.In(logic_uScript_SetTankInvulnerable_invulnerable_268, logic_uScript_SetTankInvulnerable_tank_268);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_268.Out)
		{
			Relay_True_122();
		}
	}
}
