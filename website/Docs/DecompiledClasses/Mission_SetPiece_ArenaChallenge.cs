using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("", "")]
[NodePath("Graphs")]
public class Mission_SetPiece_ArenaChallenge : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string ArenaTriggerArea = "";

	[Multiline(3)]
	public string CornerArena01TriggerArea = "";

	[Multiline(3)]
	public string CornerArena02TriggerArea = "";

	[Multiline(3)]
	public string CornerArena03TriggerArea = "";

	[Multiline(3)]
	public string CornerArena04TriggerArea = "";

	[Multiline(3)]
	public string EarlyTag = "";

	[Multiline(3)]
	public string EntranceTriggerArea = "";

	public ExternalBehaviorTree FlyAI;

	public Transform FlyParticle;

	public SpawnTechData[] GuardianTechData = new SpawnTechData[0];

	private string local_134_System_String = ",TAlive:";

	private string local_136_System_String = "";

	private string local_137_System_String = "";

	private string local_139_System_String = ",CAlive:";

	private string local_141_System_String = "";

	private string local_142_System_String = "";

	private string local_240_System_String = ",AllTDead:";

	private string local_241_System_String = "";

	private string local_244_System_String = "";

	private Tank local_544_Tank;

	private string local_56_System_String = "Stage:";

	private string local_57_System_String = "";

	private string local_59_System_String = ",npc trigger area:";

	private string local_61_System_String = "";

	private string local_63_System_String = "";

	private string local_64_System_String = "";

	private string local_65_System_String = "";

	private string local_66_System_String = ",Respawn area:";

	private string local_68_System_String = ",Round active:";

	private string local_73_System_String = "";

	private string local_75_System_String = "";

	private string local_95_System_String = "";

	private string local_96_System_String = "";

	private string local_97_System_String = ",Dialogue:";

	private bool local_AllTurretsDead_System_Boolean;

	private int local_ChargersAlive_System_Int32;

	private bool local_Child01Alive_System_Boolean;

	private Tank[] local_Child01Techs_TankArray = new Tank[0];

	private bool local_Child02Alive_System_Boolean;

	private Tank[] local_Child02Techs_TankArray = new Tank[0];

	private bool local_Child03Alive_System_Boolean;

	private Tank[] local_Child03Techs_TankArray = new Tank[0];

	private int local_DialogueProgress_System_Int32;

	private int local_DialogueProgressExtra_System_Int32;

	private Tank local_Guardian01Tech_Tank;

	private Tank local_Guardian02Tech_Tank;

	private Tank local_Guardian03Tech_Tank;

	private Tank local_Guardian04Tech_Tank;

	private Tank[] local_GuardianTechs_TankArray = new Tank[0];

	private string local_IntroTag_System_String = "";

	private bool local_isAllInsideArenaArea_System_Boolean;

	private bool local_isAllInsideNPCTrigger_System_Boolean;

	private bool local_isInsideAnyCornerArea_System_Boolean;

	private bool local_isInsideArenaArea_System_Boolean;

	private bool local_isInsideCornerArena01Area_System_Boolean;

	private bool local_isInsideCornerArena02Area_System_Boolean;

	private bool local_isInsideCornerArena03Area_System_Boolean;

	private bool local_isInsideCornerArena04Area_System_Boolean;

	private bool local_isInsideEntranceArea_System_Boolean;

	private bool local_isInsideNPCTrigger_System_Boolean;

	private bool local_isInsideRespawnArea_System_Boolean;

	private bool local_MissionStarted_System_Boolean;

	private string local_MPHoldTag_System_String = "MPHoldTag";

	private Tank local_NPC2Tech_Tank;

	private Tank[] local_NPC2Techs_TankArray = new Tank[0];

	private Tank local_NPCTech_Tank;

	private bool local_NPCTechAlive_System_Boolean;

	private Tank[] local_NPCTechs_TankArray = new Tank[0];

	private int local_Objective_System_Int32 = 1;

	private bool local_PlayedEarlyMessage_System_Boolean;

	private bool local_PlayedReadyGoDialogue_System_Boolean;

	private bool local_PlayedShieldsDownDialogue_System_Boolean;

	private bool local_PlayerDead_System_Boolean;

	private Tank local_R1MainTech_Tank;

	private Tank[] local_R1MainTechs_TankArray = new Tank[0];

	private TankBlock local_R1MainTechShieldBlock_TankBlock;

	private Tank local_R1Turret01Tech_Tank;

	private TankBlock local_R1Turret01TechShieldBlock_TankBlock;

	private Tank local_R1Turret02Tech_Tank;

	private TankBlock local_R1Turret02TechShieldBlock_TankBlock;

	private Tank local_R1Turret03Tech_Tank;

	private TankBlock local_R1Turret03TechShieldBlock_TankBlock;

	private Tank local_R1Turret04Tech_Tank;

	private TankBlock local_R1Turret04TechShieldBlock_TankBlock;

	private bool local_RoundActive_System_Boolean;

	private bool local_S1T01Alive_System_Boolean;

	private bool local_SetTeamsToEnemy_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private int local_TurretsAlive_System_Int32;

	private Tank[] local_TurretTechs_TankArray = new Tank[0];

	private bool local_VisitedNPC_System_Boolean;

	public LocalisedString[] MsgOutOfArena = new LocalisedString[0];

	[Multiline(3)]
	public string MsgOutOfArenaTag = "";

	public LocalisedString[] MsgPlayerDead = new LocalisedString[0];

	[Multiline(3)]
	public string MsgPlayerDeadTag = "";

	public LocalisedString[] MsgR1IntroInterrupted = new LocalisedString[0];

	public LocalisedString[] MsgTooEarly = new LocalisedString[0];

	[Multiline(3)]
	public string MsgTooEarlyTag = "";

	public SpawnTechData[] NPC2TechData = new SpawnTechData[0];

	public SpawnTechData[] NPCTechData = new SpawnTechData[0];

	[Multiline(3)]
	public string NPCTriggerArea = "";

	[Multiline(3)]
	public string NPCTriggerAreaOuter = "";

	public uScript_PlayDialogue.Dialogue R1DeadDialogue;

	public uScript_PlayDialogue.Dialogue R1IntroDialogue;

	public uScript_PlayDialogue.Dialogue R1IntroDialogueMP;

	public SpawnTechData[] R1MainTechData = new SpawnTechData[0];

	public BlockTypes R1Turret01TechShieldBlockData;

	public BlockTypes R1Turret02TechShieldBlockData;

	public BlockTypes R1Turret03TechShieldBlockData;

	public BlockTypes R1Turret04TechShieldBlockData;

	public SpawnTechData R2Child01TechData;

	public SpawnTechData R2Child02TechData;

	public SpawnTechData R2Child03TechData;

	public uScript_PlayDialogue.Dialogue ReadyGoDialogue;

	[Multiline(3)]
	public string RespawnTriggerArea = "";

	public BlockTypes S1MainTechShieldBlockData;

	public uScript_PlayDialogue.Dialogue ShieldsDownDialogue;

	public ManOnScreenMessages.Speaker TechSpeaker;

	public SpawnTechData[] TurretTechData = new SpawnTechData[0];

	public uScript_PlayDialogue.Dialogue VictoryDialogue;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_15;

	private GameObject owner_Connection_45;

	private GameObject owner_Connection_152;

	private GameObject owner_Connection_156;

	private GameObject owner_Connection_173;

	private GameObject owner_Connection_324;

	private GameObject owner_Connection_326;

	private GameObject owner_Connection_328;

	private GameObject owner_Connection_355;

	private GameObject owner_Connection_356;

	private GameObject owner_Connection_370;

	private GameObject owner_Connection_373;

	private GameObject owner_Connection_375;

	private GameObject owner_Connection_380;

	private GameObject owner_Connection_389;

	private GameObject owner_Connection_391;

	private GameObject owner_Connection_393;

	private GameObject owner_Connection_394;

	private GameObject owner_Connection_414;

	private GameObject owner_Connection_415;

	private GameObject owner_Connection_417;

	private GameObject owner_Connection_418;

	private GameObject owner_Connection_428;

	private GameObject owner_Connection_432;

	private GameObject owner_Connection_433;

	private GameObject owner_Connection_437;

	private GameObject owner_Connection_496;

	private GameObject owner_Connection_503;

	private GameObject owner_Connection_506;

	private GameObject owner_Connection_546;

	private GameObject owner_Connection_655;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_5 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_5;

	private bool logic_uScript_FinishEncounter_Out_5 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_7 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_7 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_8 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_8;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_8 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_10 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_10 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_14 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_14 = new Tank[0];

	private int logic_uScript_AccessListTech_index_14;

	private Tank logic_uScript_AccessListTech_value_14;

	private bool logic_uScript_AccessListTech_Out_14 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_16 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_16;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_16 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_16;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_16 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_16 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_16 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_16 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_18;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_22 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_22;

	private bool logic_uScriptAct_SetBool_Out_22 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_22 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_22 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_26 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_26 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_26 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_26;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_26 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_26 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_26 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_26 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_26 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_26 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_26 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_28;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_28 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_28 = "MissionStarted";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_29 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_29;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_29 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_29 = "Objective";

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_32 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_32 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_32 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_32;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_32 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_32 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_32 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_32 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_32 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_32 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_32 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_37 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_37 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_37 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_37;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_37 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_37 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_37 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_37 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_37 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_37 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_37 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_39;

	private bool logic_uScriptCon_CompareBool_True_39 = true;

	private bool logic_uScriptCon_CompareBool_False_39 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_40 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_40 = new Tank[0];

	private int logic_uScript_AccessListTech_index_40;

	private Tank logic_uScript_AccessListTech_value_40;

	private bool logic_uScript_AccessListTech_Out_40 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_44 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_44 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_44;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_44 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_44;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_44 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_44 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_44 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_44 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_47;

	private bool logic_uScriptCon_CompareBool_True_47 = true;

	private bool logic_uScriptCon_CompareBool_False_47 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_49 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_49;

	private int logic_uScript_PlayDialogue_progress_49;

	private bool logic_uScript_PlayDialogue_Out_49 = true;

	private bool logic_uScript_PlayDialogue_Shown_49 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_49 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_52 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_52 = 6;

	private int logic_uScriptAct_SetInt_Target_52;

	private bool logic_uScriptAct_SetInt_Out_52 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_55 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_55 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_58 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_58 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_58 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_58 = "";

	private string logic_uScriptAct_Concatenate_Result_58;

	private bool logic_uScriptAct_Concatenate_Out_58 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_62 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_62 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_62 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_62 = "";

	private string logic_uScriptAct_Concatenate_Result_62;

	private bool logic_uScriptAct_Concatenate_Out_62 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_67 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_67 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_67 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_67 = "";

	private string logic_uScriptAct_Concatenate_Result_67;

	private bool logic_uScriptAct_Concatenate_Out_67 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_69 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_69 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_69 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_69 = "";

	private string logic_uScriptAct_Concatenate_Result_69;

	private bool logic_uScriptAct_Concatenate_Out_69 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_71 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_71 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_71 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_71 = "";

	private string logic_uScriptAct_Concatenate_Result_71;

	private bool logic_uScriptAct_Concatenate_Out_71 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_72 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_72 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_72 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_72 = "";

	private string logic_uScriptAct_Concatenate_Result_72;

	private bool logic_uScriptAct_Concatenate_Out_72 = true;

	private uScriptAct_PrintText logic_uScriptAct_PrintText_uScriptAct_PrintText_74 = new uScriptAct_PrintText();

	private string logic_uScriptAct_PrintText_Text_74 = "";

	private int logic_uScriptAct_PrintText_FontSize_74 = 16;

	private FontStyle logic_uScriptAct_PrintText_FontStyle_74;

	private Color logic_uScriptAct_PrintText_FontColor_74 = new Color(0f, 0f, 0f, 1f);

	private TextAnchor logic_uScriptAct_PrintText_textAnchor_74;

	private int logic_uScriptAct_PrintText_EdgePadding_74 = 8;

	private float logic_uScriptAct_PrintText_time_74;

	private bool logic_uScriptAct_PrintText_Out_74 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_76 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_76 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_76 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_76 = "";

	private string logic_uScriptAct_Concatenate_Result_76;

	private bool logic_uScriptAct_Concatenate_Out_76 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_80;

	private bool logic_uScriptCon_CompareBool_True_80 = true;

	private bool logic_uScriptCon_CompareBool_False_80 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_82 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_82;

	private bool logic_uScriptAct_SetBool_Out_82 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_82 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_82 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_85 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_85 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_85 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_85;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_85 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_85 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_85 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_85 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_85 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_85 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_85 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_88;

	private bool logic_uScriptCon_CompareBool_True_88 = true;

	private bool logic_uScriptCon_CompareBool_False_88 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_91 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_91 = 2;

	private int logic_uScriptAct_SetInt_Target_91;

	private bool logic_uScriptAct_SetInt_Out_91 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_93 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_93 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_94 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_94 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_94 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_94 = "";

	private string logic_uScriptAct_Concatenate_Result_94;

	private bool logic_uScriptAct_Concatenate_Out_94 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_98 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_98 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_98 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_98 = "";

	private string logic_uScriptAct_Concatenate_Result_98;

	private bool logic_uScriptAct_Concatenate_Out_98 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_102 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_102;

	private int logic_uScriptAct_SetInt_Target_102;

	private bool logic_uScriptAct_SetInt_Out_102 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_103 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_103 = 4;

	private int logic_uScriptAct_SetInt_Target_103;

	private bool logic_uScriptAct_SetInt_Out_103 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_104 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_104;

	private bool logic_uScriptAct_SetBool_Out_104 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_104 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_104 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_106 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_106 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_107 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_107;

	private int logic_uScriptAct_SetInt_Target_107;

	private bool logic_uScriptAct_SetInt_Out_107 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_108 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_108 = 3;

	private int logic_uScriptAct_SetInt_Target_108;

	private bool logic_uScriptAct_SetInt_Out_108 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_111 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_111 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_111 = 1;

	private bool logic_uScript_SetTechsTeam_Out_111 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_113 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_113;

	private int logic_uScript_PlayDialogue_progress_113;

	private bool logic_uScript_PlayDialogue_Out_113 = true;

	private bool logic_uScript_PlayDialogue_Shown_113 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_113 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_115;

	private bool logic_uScriptCon_CompareBool_True_115 = true;

	private bool logic_uScriptCon_CompareBool_False_115 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_117 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_118;

	private bool logic_uScriptCon_CompareBool_True_118 = true;

	private bool logic_uScriptCon_CompareBool_False_118 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_119 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_119;

	private bool logic_uScriptAct_SetBool_Out_119 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_119 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_119 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_121 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_121;

	private bool logic_uScriptAct_SetBool_Out_121 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_121 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_121 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_123 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_123;

	private bool logic_uScriptAct_SetBool_Out_123 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_123 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_123 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_125;

	private bool logic_uScriptCon_CompareBool_True_125 = true;

	private bool logic_uScriptCon_CompareBool_False_125 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_129 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_129 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_129 = -2;

	private bool logic_uScript_SetTechsTeam_Out_129 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_133 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_133 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_133 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_133 = "";

	private string logic_uScriptAct_Concatenate_Result_133;

	private bool logic_uScriptAct_Concatenate_Out_133 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_135 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_135 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_135 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_135 = "";

	private string logic_uScriptAct_Concatenate_Result_135;

	private bool logic_uScriptAct_Concatenate_Out_135 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_138 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_138 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_138 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_138 = "";

	private string logic_uScriptAct_Concatenate_Result_138;

	private bool logic_uScriptAct_Concatenate_Out_138 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_140 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_140 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_140 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_140 = "";

	private string logic_uScriptAct_Concatenate_Result_140;

	private bool logic_uScriptAct_Concatenate_Out_140 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_143;

	private bool logic_uScriptCon_CompareBool_True_143 = true;

	private bool logic_uScriptCon_CompareBool_False_143 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_145 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_145;

	private bool logic_uScriptAct_SetBool_Out_145 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_145 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_145 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_147 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_147;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_147;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_150;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_150;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_153 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_153;

	private object logic_uScript_SetEncounterTarget_visibleObject_153 = "";

	private bool logic_uScript_SetEncounterTarget_Out_153 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_159 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_159;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_159 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_159;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_159 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_159 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_159 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_159 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_160 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_160 = new Tank[0];

	private int logic_uScript_AccessListTech_index_160;

	private Tank logic_uScript_AccessListTech_value_160;

	private bool logic_uScript_AccessListTech_Out_160 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_163 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_163 = new Tank[0];

	private int logic_uScript_AccessListTech_index_163 = 1;

	private Tank logic_uScript_AccessListTech_value_163;

	private bool logic_uScript_AccessListTech_Out_163 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_165 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_165 = new Tank[0];

	private int logic_uScript_AccessListTech_index_165 = 2;

	private Tank logic_uScript_AccessListTech_value_165;

	private bool logic_uScript_AccessListTech_Out_165 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_168 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_168 = new Tank[0];

	private int logic_uScript_AccessListTech_index_168 = 3;

	private Tank logic_uScript_AccessListTech_value_168;

	private bool logic_uScript_AccessListTech_Out_168 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_170 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_170 = new Tank[0];

	private int logic_uScript_AccessListTech_index_170;

	private Tank logic_uScript_AccessListTech_value_170;

	private bool logic_uScript_AccessListTech_Out_170 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_174 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_174 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_174;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_174 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_174;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_174 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_174 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_174 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_174 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_177 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_177 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_178 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_178 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_179 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_179 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_180 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_180 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_182 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_182 = new Tank[0];

	private int logic_uScript_AccessListTech_index_182 = 1;

	private Tank logic_uScript_AccessListTech_value_182;

	private bool logic_uScript_AccessListTech_Out_182 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_185 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_185 = new Tank[0];

	private int logic_uScript_AccessListTech_index_185 = 2;

	private Tank logic_uScript_AccessListTech_value_185;

	private bool logic_uScript_AccessListTech_Out_185 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_188 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_188 = new Tank[0];

	private int logic_uScript_AccessListTech_index_188 = 3;

	private Tank logic_uScript_AccessListTech_value_188;

	private bool logic_uScript_AccessListTech_Out_188 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_193 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_193 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_193 = -2;

	private bool logic_uScript_SetTechsTeam_Out_193 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_196 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_196 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_196 = -2;

	private bool logic_uScript_SetTechsTeam_Out_196 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_197 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_197;

	private int logic_uScriptCon_CompareInt_B_197;

	private bool logic_uScriptCon_CompareInt_GreaterThan_197 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_197 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_197 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_197 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_197 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_197 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_201 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_201;

	private BlockTypes logic_uScript_GetTankBlock_blockType_201;

	private TankBlock logic_uScript_GetTankBlock_Return_201;

	private bool logic_uScript_GetTankBlock_Out_201 = true;

	private bool logic_uScript_GetTankBlock_Returned_201 = true;

	private bool logic_uScript_GetTankBlock_NotFound_201 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_202 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_202 = "";

	private bool logic_uScript_SetShieldEnabled_enable_202;

	private bool logic_uScript_SetShieldEnabled_Out_202 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_204 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_204 = "";

	private bool logic_uScript_SetShieldEnabled_enable_204 = true;

	private bool logic_uScript_SetShieldEnabled_Out_204 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_207 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_207;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_207 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_207 = true;

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_210 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_210;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_210 = TechSequencer.ChainType.ShieldBubble;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_210 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_213 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_213;

	private BlockTypes logic_uScript_GetTankBlock_blockType_213;

	private TankBlock logic_uScript_GetTankBlock_Return_213;

	private bool logic_uScript_GetTankBlock_Out_213 = true;

	private bool logic_uScript_GetTankBlock_Returned_213 = true;

	private bool logic_uScript_GetTankBlock_NotFound_213 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_216 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_216;

	private bool logic_uScriptAct_SetBool_Out_216 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_216 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_216 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_218 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_218;

	private int logic_uScriptCon_CompareInt_B_218;

	private bool logic_uScriptCon_CompareInt_GreaterThan_218 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_218 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_218 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_218 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_218 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_218 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_219 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_219;

	private int logic_uScriptCon_CompareInt_B_219;

	private bool logic_uScriptCon_CompareInt_GreaterThan_219 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_219 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_219 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_219 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_219 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_219 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_221 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_222 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_223 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_223 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_224 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_224 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_225 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_225 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_225 = 1;

	private bool logic_uScript_SetTechsTeam_Out_225 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_228 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_228;

	private int logic_uScriptCon_CompareInt_B_228;

	private bool logic_uScriptCon_CompareInt_GreaterThan_228 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_228 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_228 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_228 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_228 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_228 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_229 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_229 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_229 = 1;

	private bool logic_uScript_SetTechsTeam_Out_229 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_231 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_231;

	private int logic_uScriptCon_CompareInt_B_231;

	private bool logic_uScriptCon_CompareInt_GreaterThan_231 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_231 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_231 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_231 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_231 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_231 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_233 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_233;

	private bool logic_uScriptAct_SetBool_Out_233 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_233 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_233 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_234 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_234;

	private bool logic_uScriptCon_CompareBool_True_234 = true;

	private bool logic_uScriptCon_CompareBool_False_234 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_237 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_237 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_242 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_242 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_242 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_242 = "";

	private string logic_uScriptAct_Concatenate_Result_242;

	private bool logic_uScriptAct_Concatenate_Out_242 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_243 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_243 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_243 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_243 = "";

	private string logic_uScriptAct_Concatenate_Result_243;

	private bool logic_uScriptAct_Concatenate_Out_243 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_245 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_245 = "";

	private bool logic_uScript_SetShieldEnabled_enable_245 = true;

	private bool logic_uScript_SetShieldEnabled_Out_245 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_247 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_247;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_247 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_247 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_248 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_248;

	private BlockTypes logic_uScript_GetTankBlock_blockType_248;

	private TankBlock logic_uScript_GetTankBlock_Return_248;

	private bool logic_uScript_GetTankBlock_Out_248 = true;

	private bool logic_uScript_GetTankBlock_Returned_248 = true;

	private bool logic_uScript_GetTankBlock_NotFound_248 = true;

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_251 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_251;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_251 = TechSequencer.ChainType.ShieldBubble;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_251 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_255 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_255 = "";

	private bool logic_uScript_SetShieldEnabled_enable_255 = true;

	private bool logic_uScript_SetShieldEnabled_Out_255 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_257 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_257;

	private BlockTypes logic_uScript_GetTankBlock_blockType_257;

	private TankBlock logic_uScript_GetTankBlock_Return_257;

	private bool logic_uScript_GetTankBlock_Out_257 = true;

	private bool logic_uScript_GetTankBlock_Returned_257 = true;

	private bool logic_uScript_GetTankBlock_NotFound_257 = true;

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_258 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_258;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_258 = TechSequencer.ChainType.ShieldBubble;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_258 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_259 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_259;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_259 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_259 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_265 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_265;

	private BlockTypes logic_uScript_GetTankBlock_blockType_265;

	private TankBlock logic_uScript_GetTankBlock_Return_265;

	private bool logic_uScript_GetTankBlock_Out_265 = true;

	private bool logic_uScript_GetTankBlock_Returned_265 = true;

	private bool logic_uScript_GetTankBlock_NotFound_265 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_267 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_267 = "";

	private bool logic_uScript_SetShieldEnabled_enable_267 = true;

	private bool logic_uScript_SetShieldEnabled_Out_267 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_269 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_269;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_269 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_269 = true;

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_271 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_271;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_271 = TechSequencer.ChainType.ShieldBubble;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_271 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_277 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_277;

	private BlockTypes logic_uScript_GetTankBlock_blockType_277;

	private TankBlock logic_uScript_GetTankBlock_Return_277;

	private bool logic_uScript_GetTankBlock_Out_277 = true;

	private bool logic_uScript_GetTankBlock_Returned_277 = true;

	private bool logic_uScript_GetTankBlock_NotFound_277 = true;

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_279 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_279;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_279 = TechSequencer.ChainType.ShieldBubble;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_279 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_282 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_282;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_282 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_282 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_283 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_283 = "";

	private bool logic_uScript_SetShieldEnabled_enable_283 = true;

	private bool logic_uScript_SetShieldEnabled_Out_283 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_287 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_287;

	private BlockTypes logic_uScript_GetTankBlock_blockType_287;

	private TankBlock logic_uScript_GetTankBlock_Return_287;

	private bool logic_uScript_GetTankBlock_Out_287 = true;

	private bool logic_uScript_GetTankBlock_Returned_287 = true;

	private bool logic_uScript_GetTankBlock_NotFound_287 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_288 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_288;

	private BlockTypes logic_uScript_GetTankBlock_blockType_288;

	private TankBlock logic_uScript_GetTankBlock_Return_288;

	private bool logic_uScript_GetTankBlock_Out_288 = true;

	private bool logic_uScript_GetTankBlock_Returned_288 = true;

	private bool logic_uScript_GetTankBlock_NotFound_288 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_291 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_291;

	private BlockTypes logic_uScript_GetTankBlock_blockType_291;

	private TankBlock logic_uScript_GetTankBlock_Return_291;

	private bool logic_uScript_GetTankBlock_Out_291 = true;

	private bool logic_uScript_GetTankBlock_Returned_291 = true;

	private bool logic_uScript_GetTankBlock_NotFound_291 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_300 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_300;

	private BlockTypes logic_uScript_GetTankBlock_blockType_300;

	private TankBlock logic_uScript_GetTankBlock_Return_300;

	private bool logic_uScript_GetTankBlock_Out_300 = true;

	private bool logic_uScript_GetTankBlock_Returned_300 = true;

	private bool logic_uScript_GetTankBlock_NotFound_300 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_302 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_302 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_303 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_303 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_304 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_304 = "";

	private bool logic_uScript_SetShieldEnabled_enable_304;

	private bool logic_uScript_SetShieldEnabled_Out_304 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_306 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_306 = "";

	private bool logic_uScript_SetShieldEnabled_enable_306;

	private bool logic_uScript_SetShieldEnabled_Out_306 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_307 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_307 = "";

	private bool logic_uScript_SetShieldEnabled_enable_307;

	private bool logic_uScript_SetShieldEnabled_Out_307 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_308 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_308 = "";

	private bool logic_uScript_SetShieldEnabled_enable_308;

	private bool logic_uScript_SetShieldEnabled_Out_308 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_309 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_309 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_310 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_310 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_314 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_314 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_319 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_319;

	private bool logic_uScriptCon_CompareBool_True_319 = true;

	private bool logic_uScriptCon_CompareBool_False_319 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_322 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_322 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_323 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_323 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_323 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_323;

	private bool logic_uScript_DestroyTechsFromData_Out_323 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_325 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_325 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_325 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_325;

	private bool logic_uScript_DestroyTechsFromData_Out_325 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_327 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_327 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_327 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_327;

	private bool logic_uScript_DestroyTechsFromData_Out_327 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_329 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_329;

	private bool logic_uScriptAct_SetBool_Out_329 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_329 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_329 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_332 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_332 = 3;

	private int logic_uScriptAct_SetInt_Target_332;

	private bool logic_uScriptAct_SetInt_Out_332 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_333 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_333 = 5;

	private int logic_uScriptAct_SetInt_Target_333;

	private bool logic_uScriptAct_SetInt_Out_333 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_335 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_335;

	private int logic_uScriptAct_SetInt_Target_335;

	private bool logic_uScriptAct_SetInt_Out_335 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_338 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_338;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_338;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_339 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_339;

	private bool logic_uScriptAct_SetBool_Out_339 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_339 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_339 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_342 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_342;

	private bool logic_uScriptCon_CompareBool_True_342 = true;

	private bool logic_uScriptCon_CompareBool_False_342 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_345 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_345;

	private bool logic_uScriptCon_CompareBool_True_345 = true;

	private bool logic_uScriptCon_CompareBool_False_345 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_347 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_347;

	private bool logic_uScriptAct_SetBool_Out_347 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_347 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_347 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_352 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_352;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_352;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_352;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_352;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_352;

	private bool logic_uScript_FlyTechUpAndAway_Out_352 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_357 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_357;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_357 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_357;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_357 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_357 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_357 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_357 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_358 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_358;

	private bool logic_uScriptAct_SetBool_Out_358 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_358 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_358 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_365 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_365 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_365;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_365 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_365;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_365 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_365 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_365 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_365 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_368 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_368;

	private bool logic_uScriptAct_SetBool_Out_368 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_368 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_368 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_371 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_371;

	private bool logic_uScriptAct_SetBool_Out_371 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_371 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_371 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_372 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_372 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_372;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_372 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_372;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_372 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_372 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_372 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_372 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_374 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_374 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_374;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_374 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_374 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_374 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_378 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_378 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_378;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_378 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_378 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_378 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_379 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_379 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_379;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_379 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_379 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_379 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_382 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_382 = 30f;

	private bool logic_uScript_Wait_repeat_382 = true;

	private bool logic_uScript_Wait_Waited_382 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_383 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_383 = 30f;

	private bool logic_uScript_Wait_repeat_383 = true;

	private bool logic_uScript_Wait_Waited_383 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_384 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_384 = 30f;

	private bool logic_uScript_Wait_repeat_384 = true;

	private bool logic_uScript_Wait_Waited_384 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_386 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_386 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_386;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_386 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_386 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_386 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_387 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_387 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_387;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_387 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_387 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_387 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_388 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_388 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_388;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_388 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_388 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_388 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_395 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_395;

	private object logic_uScript_SetEncounterTarget_visibleObject_395 = "";

	private bool logic_uScript_SetEncounterTarget_Out_395 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_397 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_397;

	private int logic_uScriptAct_SetInt_Target_397;

	private bool logic_uScriptAct_SetInt_Out_397 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_400 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_400;

	private int logic_uScriptAct_SetInt_Target_400;

	private bool logic_uScriptAct_SetInt_Out_400 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_402 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_402;

	private bool logic_uScriptAct_SetBool_Out_402 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_402 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_402 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_404 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_404;

	private bool logic_uScriptCon_CompareBool_True_404 = true;

	private bool logic_uScriptCon_CompareBool_False_404 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_406 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_406;

	private bool logic_uScriptAct_SetBool_Out_406 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_406 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_406 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_408 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_408;

	private bool logic_uScriptAct_SetBool_Out_408 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_408 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_408 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_410 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_410 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_410 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_410 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_419 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_419 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_419;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_419;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_419;

	private bool logic_uScript_SpawnTechsFromData_Out_419 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_420 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_420 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_420;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_420 = 0.5f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_420;

	private bool logic_uScript_SpawnTechsFromData_Out_420 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_421 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_421 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_421;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_421 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_421;

	private bool logic_uScript_SpawnTechsFromData_Out_421 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_422 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_422 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_422;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_422 = 1f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_422;

	private bool logic_uScript_SpawnTechsFromData_Out_422 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_425 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_425;

	private int logic_uScriptAct_SetInt_Target_425;

	private bool logic_uScriptAct_SetInt_Out_425 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_426 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_426 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_426 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_426;

	private bool logic_uScript_DestroyTechsFromData_Out_426 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_431 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_431 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_431 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_431;

	private bool logic_uScript_DestroyTechsFromData_Out_431 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_434 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_434 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_434 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_434;

	private bool logic_uScript_DestroyTechsFromData_Out_434 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_435 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_435 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_435 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_435;

	private bool logic_uScript_DestroyTechsFromData_Out_435 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_438 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_438 = 3;

	private int logic_uScriptCon_CompareInt_B_438;

	private bool logic_uScriptCon_CompareInt_GreaterThan_438 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_438 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_438 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_438 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_438 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_438 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_440 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_440 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_440 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_440;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_440 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_440 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_440 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_440 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_440 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_440 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_440 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_442 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_442 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_444 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_444;

	private bool logic_uScriptAct_SetBool_Out_444 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_444 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_444 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_447;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_447 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_447 = "AllTurretsDead";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_457;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_457 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_457 = "RoundActive";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_458;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_458 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_458 = "PlayedEarlyMessage";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_459;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_459 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_459 = "VisitedNPC";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_460;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_460 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_460 = "PlayedReadyGoDialogue";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_461;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_461 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_461 = "SetTeamsToEnemy";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_462;

	private int logic_SubGraph_SaveLoadInt_integer_462;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_462 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_462 = "TurretsAlive";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_463;

	private int logic_SubGraph_SaveLoadInt_integer_463;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_463 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_463 = "ChargersAlive";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_464;

	private int logic_SubGraph_SaveLoadInt_integer_464;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_464 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_464 = "DialogueProgress";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_465;

	private int logic_SubGraph_SaveLoadInt_integer_465;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_465 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_465 = "DialogueProgressExtra";

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_467 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_467;

	private int logic_uScript_PlayDialogue_progress_467;

	private bool logic_uScript_PlayDialogue_Out_467 = true;

	private bool logic_uScript_PlayDialogue_Shown_467 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_467 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_469 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_469;

	private int logic_uScript_PlayDialogue_progress_469;

	private bool logic_uScript_PlayDialogue_Out_469 = true;

	private bool logic_uScript_PlayDialogue_Shown_469 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_469 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_472 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_472;

	private int logic_uScriptAct_SetInt_Target_472;

	private bool logic_uScriptAct_SetInt_Out_472 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_474 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_474;

	private bool logic_uScriptCon_CompareBool_True_474 = true;

	private bool logic_uScriptCon_CompareBool_False_474 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_476 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_476 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_477 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_477;

	private bool logic_uScriptAct_SetBool_Out_477 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_477 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_477 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_479;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_479 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_479 = "PlayedShieldsDownDialogue";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_481 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_481;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_481 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_481 = "Stage";

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_482 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_482;

	private int logic_uScript_PlayDialogue_progress_482;

	private bool logic_uScript_PlayDialogue_Out_482 = true;

	private bool logic_uScript_PlayDialogue_Shown_482 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_482 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_485 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_485;

	private int logic_uScriptAct_SetInt_Target_485;

	private bool logic_uScriptAct_SetInt_Out_485 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_487 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_487;

	private bool logic_uScriptCon_CompareBool_True_487 = true;

	private bool logic_uScriptCon_CompareBool_False_487 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_488 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_488;

	private bool logic_uScriptCon_CompareBool_True_488 = true;

	private bool logic_uScriptCon_CompareBool_False_488 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_489 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_489;

	private bool logic_uScriptAct_SetBool_Out_489 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_489 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_489 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_492 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_492;

	private bool logic_uScriptAct_SetBool_Out_492 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_492 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_492 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_494 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_494 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_494;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_494;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_494;

	private bool logic_uScript_SpawnTechsFromData_Out_494 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_500 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_500;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_500;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_500;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_500;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_500;

	private bool logic_uScript_FlyTechUpAndAway_Out_500 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_501 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_501;

	private object logic_uScript_SetEncounterTarget_visibleObject_501 = "";

	private bool logic_uScript_SetEncounterTarget_Out_501 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_505 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_505 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_505;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_505 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_505;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_505 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_505 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_505 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_505 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_508 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_508 = new Tank[0];

	private int logic_uScript_AccessListTech_index_508;

	private Tank logic_uScript_AccessListTech_value_508;

	private bool logic_uScript_AccessListTech_Out_508 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_511 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_511 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_514 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_514 = 7;

	private int logic_uScriptAct_SetInt_Target_514;

	private bool logic_uScriptAct_SetInt_Out_514 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_515 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_515;

	private int logic_uScriptAct_SetInt_Target_515;

	private bool logic_uScriptAct_SetInt_Out_515 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_517 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_517 = 2;

	private int logic_uScriptCon_CheckIntEquals_B_517;

	private bool logic_uScriptCon_CheckIntEquals_True_517 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_517 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_518 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_518 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_518;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_518;

	private string logic_uScript_AddOnScreenMessage_tag_518 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_518;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_518;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_518;

	private bool logic_uScript_AddOnScreenMessage_Out_518 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_518 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_524 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_524 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_524;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_524;

	private string logic_uScript_AddOnScreenMessage_tag_524 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_524;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_524;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_524;

	private bool logic_uScript_AddOnScreenMessage_Out_524 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_524 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_527 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_527;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_527;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_530;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_530 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_530 = "isAllInsideNPCTrigger";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_531;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_531 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_531 = "isAllInsideArenaArea";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_533 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_533;

	private bool logic_uScriptCon_CompareBool_True_533 = true;

	private bool logic_uScriptCon_CompareBool_False_533 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_535 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_535;

	private int logic_uScriptAct_SetInt_Target_535;

	private bool logic_uScriptAct_SetInt_Out_535 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_537 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_537;

	private bool logic_uScriptAct_SetBool_Out_537 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_537 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_537 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_538 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_538 = 3;

	private int logic_uScriptAct_SetInt_Target_538;

	private bool logic_uScriptAct_SetInt_Out_538 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_541;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_541 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_541 = "PlayerDead";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_542 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_542;

	private bool logic_uScriptAct_SetBool_Out_542 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_542 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_542 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_547 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_547 = 4;

	private int logic_uScriptCon_CheckIntEquals_B_547;

	private bool logic_uScriptCon_CheckIntEquals_True_547 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_547 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_549 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_549 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_549;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_549;

	private string logic_uScript_AddOnScreenMessage_tag_549 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_549;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_549;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_549;

	private bool logic_uScript_AddOnScreenMessage_Out_549 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_549 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_550 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_550;

	private bool logic_uScriptAct_SetBool_Out_550 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_550 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_550 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_556 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_556 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_557 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_557;

	private BlockTypes logic_uScript_GetTankBlock_blockType_557;

	private TankBlock logic_uScript_GetTankBlock_Return_557;

	private bool logic_uScript_GetTankBlock_Out_557 = true;

	private bool logic_uScript_GetTankBlock_Returned_557 = true;

	private bool logic_uScript_GetTankBlock_NotFound_557 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_558 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_558 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_560 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_560 = "";

	private bool logic_uScript_SetShieldEnabled_enable_560;

	private bool logic_uScript_SetShieldEnabled_Out_560 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_566 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_566;

	private BlockTypes logic_uScript_GetTankBlock_blockType_566;

	private TankBlock logic_uScript_GetTankBlock_Return_566;

	private bool logic_uScript_GetTankBlock_Out_566 = true;

	private bool logic_uScript_GetTankBlock_Returned_566 = true;

	private bool logic_uScript_GetTankBlock_NotFound_566 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_570 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_570 = "";

	private bool logic_uScript_SetShieldEnabled_enable_570;

	private bool logic_uScript_SetShieldEnabled_Out_570 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_574 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_574 = "";

	private bool logic_uScript_SetShieldEnabled_enable_574;

	private bool logic_uScript_SetShieldEnabled_Out_574 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_575 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_575;

	private BlockTypes logic_uScript_GetTankBlock_blockType_575;

	private TankBlock logic_uScript_GetTankBlock_Return_575;

	private bool logic_uScript_GetTankBlock_Out_575 = true;

	private bool logic_uScript_GetTankBlock_Returned_575 = true;

	private bool logic_uScript_GetTankBlock_NotFound_575 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_577 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_577;

	private BlockTypes logic_uScript_GetTankBlock_blockType_577;

	private TankBlock logic_uScript_GetTankBlock_Return_577;

	private bool logic_uScript_GetTankBlock_Out_577 = true;

	private bool logic_uScript_GetTankBlock_Returned_577 = true;

	private bool logic_uScript_GetTankBlock_NotFound_577 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_580 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_580 = "";

	private bool logic_uScript_SetShieldEnabled_enable_580;

	private bool logic_uScript_SetShieldEnabled_Out_580 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_581 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_581 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_583 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_583 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_583 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_583;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_583 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_583 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_583 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_583 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_583 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_583 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_583 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_584 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_584 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_584 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_584;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_584 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_584 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_584 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_584 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_584 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_584 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_584 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_585 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_585 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_585 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_585;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_585 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_585 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_585 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_585 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_585 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_585 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_585 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_586 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_586 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_586 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_586;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_586 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_586 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_586 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_586 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_586 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_586 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_586 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_589 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_589;

	private bool logic_uScriptAct_SetBool_Out_589 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_589 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_589 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_590 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_590 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_590 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_592 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_592;

	private bool logic_uScriptAct_SetBool_Out_592 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_592 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_592 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_594 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_594;

	private bool logic_uScriptAct_SetBool_Out_594 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_594 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_594 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_596 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_596;

	private bool logic_uScriptAct_SetBool_Out_596 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_596 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_596 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_597 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_597;

	private bool logic_uScriptAct_SetBool_Out_597 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_597 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_597 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_598 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_598;

	private bool logic_uScriptAct_SetBool_Out_598 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_598 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_598 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_601 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_601;

	private bool logic_uScriptAct_SetBool_Out_601 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_601 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_601 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_602 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_602;

	private bool logic_uScriptAct_SetBool_Out_602 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_602 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_602 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_606 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_606 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_606 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_606;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_606 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_606 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_606 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_606 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_606 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_606 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_606 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_608 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_608 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_608 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_608;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_608 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_608 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_608 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_608 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_608 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_608 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_608 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_610 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_610 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_610 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_610;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_610 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_610 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_610 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_610 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_610 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_610 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_610 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_611 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_611 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_611 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_611;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_611 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_611 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_611 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_611 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_611 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_611 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_611 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_614 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_614;

	private bool logic_uScriptAct_SetBool_Out_614 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_614 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_614 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_616 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_616;

	private bool logic_uScriptAct_SetBool_Out_616 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_616 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_616 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_617 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_617;

	private bool logic_uScriptAct_SetBool_Out_617 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_617 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_617 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_619 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_619;

	private bool logic_uScriptAct_SetBool_Out_619 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_619 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_619 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_621 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_621 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_622 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_622 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_623 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_623;

	private bool logic_uScriptCon_CompareBool_True_623 = true;

	private bool logic_uScriptCon_CompareBool_False_623 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_625 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_625;

	private bool logic_uScriptCon_CompareBool_True_625 = true;

	private bool logic_uScriptCon_CompareBool_False_625 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_629 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_629;

	private bool logic_uScriptCon_CompareBool_True_629 = true;

	private bool logic_uScriptCon_CompareBool_False_629 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_630 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_630;

	private bool logic_uScriptCon_CompareBool_True_630 = true;

	private bool logic_uScriptCon_CompareBool_False_630 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_631 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_631 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_632 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_632 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_633 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_633 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_634 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_634 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_635 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_635;

	private bool logic_uScriptAct_SetBool_Out_635 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_635 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_635 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_638 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_638;

	private bool logic_uScriptCon_CompareBool_True_638 = true;

	private bool logic_uScriptCon_CompareBool_False_638 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_639 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_639;

	private bool logic_uScriptCon_CompareBool_True_639 = true;

	private bool logic_uScriptCon_CompareBool_False_639 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_642 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_642;

	private bool logic_uScriptAct_SetBool_Out_642 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_642 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_642 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_644;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_644 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_644 = "isInsideAnyCornerArea";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_646 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_646;

	private bool logic_uScriptCon_CompareBool_True_646 = true;

	private bool logic_uScriptCon_CompareBool_False_646 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_648 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_648 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_648 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_648 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_653 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_653 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_653 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_654 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_654;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_654 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_654 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_654 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_656 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_656;

	private bool logic_uScriptCon_CompareBool_True_656 = true;

	private bool logic_uScriptCon_CompareBool_False_656 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_659 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_659;

	private bool logic_uScriptCon_CompareBool_True_659 = true;

	private bool logic_uScriptCon_CompareBool_False_659 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_661 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_661;

	private bool logic_uScriptAct_SetBool_Out_661 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_661 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_661 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_662 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_662;

	private bool logic_uScript_IsTechAlive_Alive_662 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_662 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_664 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_664;

	private bool logic_uScript_IsTechAlive_Alive_664 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_664 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_666 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_666;

	private bool logic_uScript_IsTechAlive_Alive_666 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_666 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_669 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_669;

	private bool logic_uScript_IsTechAlive_Alive_669 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_669 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_670 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_670;

	private bool logic_uScript_IsTechAlive_Alive_670 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_670 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_672 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_672;

	private bool logic_uScript_IsTechAlive_Alive_672 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_672 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_674 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_674;

	private bool logic_uScript_IsTechAlive_Alive_674 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_674 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_676 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_676;

	private bool logic_uScript_IsTechAlive_Alive_676 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_676 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_678 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_678;

	private bool logic_uScript_IsTechAlive_Alive_678 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_678 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_680 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_680;

	private bool logic_uScript_IsTechAlive_Alive_680 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_680 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_683 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_683;

	private bool logic_uScript_IsTechAlive_Alive_683 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_683 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_684 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_684;

	private bool logic_uScript_IsTechAlive_Alive_684 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_684 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_687 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_687;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_688 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_688;

	private bool logic_uScriptCon_CompareBool_True_688 = true;

	private bool logic_uScriptCon_CompareBool_False_688 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_690 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_690 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_691 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_691 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_693 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_693;

	private bool logic_uScriptAct_SetBool_Out_693 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_693 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_693 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_694 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_694 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_694;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_694;

	private string logic_uScript_AddOnScreenMessage_tag_694 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_694;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_694;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_694;

	private bool logic_uScript_AddOnScreenMessage_Out_694 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_694 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_697 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_697 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_697;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_697 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_1534 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_1534;

	private int logic_uScript_PlayDialogue_progress_1534;

	private bool logic_uScript_PlayDialogue_Out_1534 = true;

	private bool logic_uScript_PlayDialogue_Shown_1534 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_1534 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1539 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_1539 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1541 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_1541 = true;

	private Tank event_UnityEngine_GameObject_Tech_543;

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
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_15 || !m_RegisteredForEvents)
		{
			owner_Connection_15 = parentGameObject;
		}
		if (null == owner_Connection_45 || !m_RegisteredForEvents)
		{
			owner_Connection_45 = parentGameObject;
		}
		if (null == owner_Connection_152 || !m_RegisteredForEvents)
		{
			owner_Connection_152 = parentGameObject;
		}
		if (null == owner_Connection_156 || !m_RegisteredForEvents)
		{
			owner_Connection_156 = parentGameObject;
		}
		if (null == owner_Connection_173 || !m_RegisteredForEvents)
		{
			owner_Connection_173 = parentGameObject;
		}
		if (null == owner_Connection_324 || !m_RegisteredForEvents)
		{
			owner_Connection_324 = parentGameObject;
		}
		if (null == owner_Connection_326 || !m_RegisteredForEvents)
		{
			owner_Connection_326 = parentGameObject;
		}
		if (null == owner_Connection_328 || !m_RegisteredForEvents)
		{
			owner_Connection_328 = parentGameObject;
		}
		if (null == owner_Connection_355 || !m_RegisteredForEvents)
		{
			owner_Connection_355 = parentGameObject;
		}
		if (null == owner_Connection_356 || !m_RegisteredForEvents)
		{
			owner_Connection_356 = parentGameObject;
		}
		if (null == owner_Connection_370 || !m_RegisteredForEvents)
		{
			owner_Connection_370 = parentGameObject;
		}
		if (null == owner_Connection_373 || !m_RegisteredForEvents)
		{
			owner_Connection_373 = parentGameObject;
		}
		if (null == owner_Connection_375 || !m_RegisteredForEvents)
		{
			owner_Connection_375 = parentGameObject;
		}
		if (null == owner_Connection_380 || !m_RegisteredForEvents)
		{
			owner_Connection_380 = parentGameObject;
		}
		if (null == owner_Connection_389 || !m_RegisteredForEvents)
		{
			owner_Connection_389 = parentGameObject;
		}
		if (null == owner_Connection_391 || !m_RegisteredForEvents)
		{
			owner_Connection_391 = parentGameObject;
		}
		if (null == owner_Connection_393 || !m_RegisteredForEvents)
		{
			owner_Connection_393 = parentGameObject;
		}
		if (null == owner_Connection_394 || !m_RegisteredForEvents)
		{
			owner_Connection_394 = parentGameObject;
		}
		if (null == owner_Connection_414 || !m_RegisteredForEvents)
		{
			owner_Connection_414 = parentGameObject;
		}
		if (null == owner_Connection_415 || !m_RegisteredForEvents)
		{
			owner_Connection_415 = parentGameObject;
		}
		if (null == owner_Connection_417 || !m_RegisteredForEvents)
		{
			owner_Connection_417 = parentGameObject;
		}
		if (null == owner_Connection_418 || !m_RegisteredForEvents)
		{
			owner_Connection_418 = parentGameObject;
		}
		if (null == owner_Connection_428 || !m_RegisteredForEvents)
		{
			owner_Connection_428 = parentGameObject;
		}
		if (null == owner_Connection_432 || !m_RegisteredForEvents)
		{
			owner_Connection_432 = parentGameObject;
		}
		if (null == owner_Connection_433 || !m_RegisteredForEvents)
		{
			owner_Connection_433 = parentGameObject;
		}
		if (null == owner_Connection_437 || !m_RegisteredForEvents)
		{
			owner_Connection_437 = parentGameObject;
		}
		if (null == owner_Connection_496 || !m_RegisteredForEvents)
		{
			owner_Connection_496 = parentGameObject;
		}
		if (null == owner_Connection_503 || !m_RegisteredForEvents)
		{
			owner_Connection_503 = parentGameObject;
		}
		if (null == owner_Connection_506 || !m_RegisteredForEvents)
		{
			owner_Connection_506 = parentGameObject;
		}
		if (null == owner_Connection_546 || !m_RegisteredForEvents)
		{
			owner_Connection_546 = parentGameObject;
			if (null != owner_Connection_546)
			{
				uScript_PlayerTechDestroyedEvent uScript_PlayerTechDestroyedEvent2 = owner_Connection_546.GetComponent<uScript_PlayerTechDestroyedEvent>();
				if (null == uScript_PlayerTechDestroyedEvent2)
				{
					uScript_PlayerTechDestroyedEvent2 = owner_Connection_546.AddComponent<uScript_PlayerTechDestroyedEvent>();
				}
				if (null != uScript_PlayerTechDestroyedEvent2)
				{
					uScript_PlayerTechDestroyedEvent2.TechDestroyedEvent += Instance_TechDestroyedEvent_543;
				}
			}
		}
		if (null == owner_Connection_655 || !m_RegisteredForEvents)
		{
			owner_Connection_655 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_546)
		{
			uScript_PlayerTechDestroyedEvent uScript_PlayerTechDestroyedEvent2 = owner_Connection_546.GetComponent<uScript_PlayerTechDestroyedEvent>();
			if (null == uScript_PlayerTechDestroyedEvent2)
			{
				uScript_PlayerTechDestroyedEvent2 = owner_Connection_546.AddComponent<uScript_PlayerTechDestroyedEvent>();
			}
			if (null != uScript_PlayerTechDestroyedEvent2)
			{
				uScript_PlayerTechDestroyedEvent2.TechDestroyedEvent += Instance_TechDestroyedEvent_543;
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
		if (null != owner_Connection_546)
		{
			uScript_PlayerTechDestroyedEvent component3 = owner_Connection_546.GetComponent<uScript_PlayerTechDestroyedEvent>();
			if (null != component3)
			{
				component3.TechDestroyedEvent -= Instance_TechDestroyedEvent_543;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_5.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_7.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_8.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_10.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_14.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_26.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_32.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_37.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_40.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_44.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_49.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_52.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_55.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_58.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_62.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_67.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_69.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_71.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_72.SetParent(g);
		logic_uScriptAct_PrintText_uScriptAct_PrintText_74.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_76.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_85.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_91.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_93.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_94.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_98.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_102.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_103.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_106.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_107.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_108.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_111.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_113.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_121.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_129.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_133.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_135.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_138.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_140.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_145.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_147.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_153.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_160.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_163.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_165.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_168.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_170.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_174.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_177.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_178.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_179.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_180.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_182.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_185.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_188.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_193.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_196.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_197.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_201.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_202.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_204.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_207.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_210.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_213.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_216.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_218.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_219.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_223.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_224.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_225.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_228.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_229.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_231.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_233.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_234.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_237.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_242.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_243.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_245.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_247.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_248.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_251.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_255.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_257.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_258.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_259.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_265.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_267.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_269.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_271.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_277.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_279.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_282.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_283.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_287.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_288.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_291.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_300.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_302.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_303.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_304.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_306.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_307.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_308.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_309.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_310.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_314.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_319.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_322.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_323.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_325.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_327.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_329.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_332.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_333.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_335.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_338.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_339.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_342.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_345.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_347.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_352.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_358.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_365.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_368.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_371.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_372.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_374.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_378.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_379.SetParent(g);
		logic_uScript_Wait_uScript_Wait_382.SetParent(g);
		logic_uScript_Wait_uScript_Wait_383.SetParent(g);
		logic_uScript_Wait_uScript_Wait_384.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_386.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_387.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_388.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_395.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_397.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_400.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_402.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_404.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_406.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_408.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_410.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_419.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_420.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_421.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_422.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_425.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_426.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_431.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_434.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_435.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_438.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_440.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_442.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_444.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_467.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_469.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_472.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_474.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_476.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_477.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_482.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_485.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_487.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_488.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_489.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_492.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_494.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_500.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_501.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_505.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_508.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_511.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_514.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_515.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_517.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_518.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_524.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_527.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_533.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_535.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_537.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_538.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_542.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_547.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_549.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_550.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_556.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_557.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_558.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_560.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_566.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_570.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_574.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_575.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_577.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_580.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_581.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_583.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_584.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_585.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_586.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_589.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_590.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_592.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_594.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_596.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_597.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_598.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_601.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_602.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_606.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_608.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_610.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_611.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_614.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_616.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_617.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_619.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_621.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_622.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_623.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_625.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_629.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_630.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_631.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_632.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_633.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_634.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_635.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_638.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_639.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_642.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_646.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_648.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_653.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_654.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_656.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_659.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_661.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_662.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_664.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_666.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_669.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_670.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_672.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_674.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_676.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_678.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_680.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_683.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_684.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_687.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_688.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_690.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_691.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_693.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_694.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_697.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_1534.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1539.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1541.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_6 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_15 = parentGameObject;
		owner_Connection_45 = parentGameObject;
		owner_Connection_152 = parentGameObject;
		owner_Connection_156 = parentGameObject;
		owner_Connection_173 = parentGameObject;
		owner_Connection_324 = parentGameObject;
		owner_Connection_326 = parentGameObject;
		owner_Connection_328 = parentGameObject;
		owner_Connection_355 = parentGameObject;
		owner_Connection_356 = parentGameObject;
		owner_Connection_370 = parentGameObject;
		owner_Connection_373 = parentGameObject;
		owner_Connection_375 = parentGameObject;
		owner_Connection_380 = parentGameObject;
		owner_Connection_389 = parentGameObject;
		owner_Connection_391 = parentGameObject;
		owner_Connection_393 = parentGameObject;
		owner_Connection_394 = parentGameObject;
		owner_Connection_414 = parentGameObject;
		owner_Connection_415 = parentGameObject;
		owner_Connection_417 = parentGameObject;
		owner_Connection_418 = parentGameObject;
		owner_Connection_428 = parentGameObject;
		owner_Connection_432 = parentGameObject;
		owner_Connection_433 = parentGameObject;
		owner_Connection_437 = parentGameObject;
		owner_Connection_496 = parentGameObject;
		owner_Connection_503 = parentGameObject;
		owner_Connection_506 = parentGameObject;
		owner_Connection_546 = parentGameObject;
		owner_Connection_655 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_147.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_338.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_527.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_687.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output1 += uScriptCon_ManualSwitch_Output1_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output2 += uScriptCon_ManualSwitch_Output2_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output3 += uScriptCon_ManualSwitch_Output3_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output4 += uScriptCon_ManualSwitch_Output4_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output5 += uScriptCon_ManualSwitch_Output5_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output6 += uScriptCon_ManualSwitch_Output6_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output7 += uScriptCon_ManualSwitch_Output7_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output8 += uScriptCon_ManualSwitch_Output8_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Save_Out += SubGraph_SaveLoadBool_Save_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Load_Out += SubGraph_SaveLoadBool_Load_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_28;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Save_Out += SubGraph_SaveLoadInt_Save_Out_29;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Load_Out += SubGraph_SaveLoadInt_Load_Out_29;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_29;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_147.Out += SubGraph_CompleteObjectiveStage_Out_147;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.Out += SubGraph_CompleteObjectiveStage_Out_150;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_338.Out += SubGraph_CompleteObjectiveStage_Out_338;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Save_Out += SubGraph_SaveLoadBool_Save_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Load_Out += SubGraph_SaveLoadBool_Load_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Save_Out += SubGraph_SaveLoadBool_Save_Out_457;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Load_Out += SubGraph_SaveLoadBool_Load_Out_457;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_457;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Save_Out += SubGraph_SaveLoadBool_Save_Out_458;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Load_Out += SubGraph_SaveLoadBool_Load_Out_458;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_458;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Save_Out += SubGraph_SaveLoadBool_Save_Out_459;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Load_Out += SubGraph_SaveLoadBool_Load_Out_459;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_459;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Save_Out += SubGraph_SaveLoadBool_Save_Out_460;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Load_Out += SubGraph_SaveLoadBool_Load_Out_460;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_460;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Save_Out += SubGraph_SaveLoadBool_Save_Out_461;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Load_Out += SubGraph_SaveLoadBool_Load_Out_461;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_461;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.Save_Out += SubGraph_SaveLoadInt_Save_Out_462;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.Load_Out += SubGraph_SaveLoadInt_Load_Out_462;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_462;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.Save_Out += SubGraph_SaveLoadInt_Save_Out_463;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.Load_Out += SubGraph_SaveLoadInt_Load_Out_463;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_463;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.Save_Out += SubGraph_SaveLoadInt_Save_Out_464;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.Load_Out += SubGraph_SaveLoadInt_Load_Out_464;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_464;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.Save_Out += SubGraph_SaveLoadInt_Save_Out_465;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.Load_Out += SubGraph_SaveLoadInt_Load_Out_465;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_465;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Save_Out += SubGraph_SaveLoadBool_Save_Out_479;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Load_Out += SubGraph_SaveLoadBool_Load_Out_479;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_479;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.Save_Out += SubGraph_SaveLoadInt_Save_Out_481;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.Load_Out += SubGraph_SaveLoadInt_Load_Out_481;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_481;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_527.Out += SubGraph_CompleteObjectiveStage_Out_527;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Save_Out += SubGraph_SaveLoadBool_Save_Out_530;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Load_Out += SubGraph_SaveLoadBool_Load_Out_530;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_530;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Save_Out += SubGraph_SaveLoadBool_Save_Out_531;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Load_Out += SubGraph_SaveLoadBool_Load_Out_531;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_531;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Save_Out += SubGraph_SaveLoadBool_Save_Out_541;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Load_Out += SubGraph_SaveLoadBool_Load_Out_541;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_541;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Save_Out += SubGraph_SaveLoadBool_Save_Out_644;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Load_Out += SubGraph_SaveLoadBool_Load_Out_644;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_644;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_687.Out += SubGraph_LoadObjectiveStates_Out_687;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_147.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_338.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_527.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_687.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_8.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_49.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_113.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_147.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_338.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_352.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_467.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_469.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_482.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_500.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_527.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_687.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_1534.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_49.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_113.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_147.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_201.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_213.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_248.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_257.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_265.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_277.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_287.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_288.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_291.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_300.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_338.OnDisable();
		logic_uScript_Wait_uScript_Wait_382.OnDisable();
		logic_uScript_Wait_uScript_Wait_383.OnDisable();
		logic_uScript_Wait_uScript_Wait_384.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_467.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_469.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_482.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_518.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_524.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_527.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_549.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_557.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_566.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_575.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_577.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_590.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_653.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_654.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_662.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_664.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_666.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_669.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_670.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_672.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_674.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_676.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_678.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_680.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_683.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_684.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_687.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_694.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_1534.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_147.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_338.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_527.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_687.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_147.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_338.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_527.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_687.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output1 -= uScriptCon_ManualSwitch_Output1_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output2 -= uScriptCon_ManualSwitch_Output2_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output3 -= uScriptCon_ManualSwitch_Output3_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output4 -= uScriptCon_ManualSwitch_Output4_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output5 -= uScriptCon_ManualSwitch_Output5_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output6 -= uScriptCon_ManualSwitch_Output6_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output7 -= uScriptCon_ManualSwitch_Output7_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.Output8 -= uScriptCon_ManualSwitch_Output8_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Save_Out -= SubGraph_SaveLoadBool_Save_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Load_Out -= SubGraph_SaveLoadBool_Load_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_28;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Save_Out -= SubGraph_SaveLoadInt_Save_Out_29;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Load_Out -= SubGraph_SaveLoadInt_Load_Out_29;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_29;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_147.Out -= SubGraph_CompleteObjectiveStage_Out_147;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.Out -= SubGraph_CompleteObjectiveStage_Out_150;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_338.Out -= SubGraph_CompleteObjectiveStage_Out_338;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Save_Out -= SubGraph_SaveLoadBool_Save_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Load_Out -= SubGraph_SaveLoadBool_Load_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Save_Out -= SubGraph_SaveLoadBool_Save_Out_457;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Load_Out -= SubGraph_SaveLoadBool_Load_Out_457;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_457;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Save_Out -= SubGraph_SaveLoadBool_Save_Out_458;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Load_Out -= SubGraph_SaveLoadBool_Load_Out_458;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_458;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Save_Out -= SubGraph_SaveLoadBool_Save_Out_459;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Load_Out -= SubGraph_SaveLoadBool_Load_Out_459;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_459;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Save_Out -= SubGraph_SaveLoadBool_Save_Out_460;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Load_Out -= SubGraph_SaveLoadBool_Load_Out_460;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_460;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Save_Out -= SubGraph_SaveLoadBool_Save_Out_461;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Load_Out -= SubGraph_SaveLoadBool_Load_Out_461;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_461;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.Save_Out -= SubGraph_SaveLoadInt_Save_Out_462;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.Load_Out -= SubGraph_SaveLoadInt_Load_Out_462;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_462;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.Save_Out -= SubGraph_SaveLoadInt_Save_Out_463;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.Load_Out -= SubGraph_SaveLoadInt_Load_Out_463;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_463;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.Save_Out -= SubGraph_SaveLoadInt_Save_Out_464;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.Load_Out -= SubGraph_SaveLoadInt_Load_Out_464;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_464;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.Save_Out -= SubGraph_SaveLoadInt_Save_Out_465;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.Load_Out -= SubGraph_SaveLoadInt_Load_Out_465;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_465;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Save_Out -= SubGraph_SaveLoadBool_Save_Out_479;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Load_Out -= SubGraph_SaveLoadBool_Load_Out_479;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_479;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.Save_Out -= SubGraph_SaveLoadInt_Save_Out_481;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.Load_Out -= SubGraph_SaveLoadInt_Load_Out_481;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_481;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_527.Out -= SubGraph_CompleteObjectiveStage_Out_527;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Save_Out -= SubGraph_SaveLoadBool_Save_Out_530;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Load_Out -= SubGraph_SaveLoadBool_Load_Out_530;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_530;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Save_Out -= SubGraph_SaveLoadBool_Save_Out_531;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Load_Out -= SubGraph_SaveLoadBool_Load_Out_531;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_531;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Save_Out -= SubGraph_SaveLoadBool_Save_Out_541;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Load_Out -= SubGraph_SaveLoadBool_Load_Out_541;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_541;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Save_Out -= SubGraph_SaveLoadBool_Save_Out_644;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Load_Out -= SubGraph_SaveLoadBool_Load_Out_644;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_644;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_687.Out -= SubGraph_LoadObjectiveStates_Out_687;
	}

	public void OnGUI()
	{
		logic_uScriptAct_PrintText_uScriptAct_PrintText_74.OnGUI();
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

	private void Instance_TechDestroyedEvent_543(object o, uScript_PlayerTechDestroyedEvent.TechDestroyedEventArgs e)
	{
		event_UnityEngine_GameObject_Tech_543 = e.Tech;
		Relay_TechDestroyedEvent_543();
	}

	private void uScriptCon_ManualSwitch_Output1_18(object o, EventArgs e)
	{
		Relay_Output1_18();
	}

	private void uScriptCon_ManualSwitch_Output2_18(object o, EventArgs e)
	{
		Relay_Output2_18();
	}

	private void uScriptCon_ManualSwitch_Output3_18(object o, EventArgs e)
	{
		Relay_Output3_18();
	}

	private void uScriptCon_ManualSwitch_Output4_18(object o, EventArgs e)
	{
		Relay_Output4_18();
	}

	private void uScriptCon_ManualSwitch_Output5_18(object o, EventArgs e)
	{
		Relay_Output5_18();
	}

	private void uScriptCon_ManualSwitch_Output6_18(object o, EventArgs e)
	{
		Relay_Output6_18();
	}

	private void uScriptCon_ManualSwitch_Output7_18(object o, EventArgs e)
	{
		Relay_Output7_18();
	}

	private void uScriptCon_ManualSwitch_Output8_18(object o, EventArgs e)
	{
		Relay_Output8_18();
	}

	private void SubGraph_SaveLoadBool_Save_Out_28(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = e.boolean;
		local_MissionStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_28;
		Relay_Save_Out_28();
	}

	private void SubGraph_SaveLoadBool_Load_Out_28(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = e.boolean;
		local_MissionStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_28;
		Relay_Load_Out_28();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_28(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = e.boolean;
		local_MissionStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_28;
		Relay_Restart_Out_28();
	}

	private void SubGraph_SaveLoadInt_Save_Out_29(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_29 = e.integer;
		local_Objective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_29;
		Relay_Save_Out_29();
	}

	private void SubGraph_SaveLoadInt_Load_Out_29(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_29 = e.integer;
		local_Objective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_29;
		Relay_Load_Out_29();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_29(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_29 = e.integer;
		local_Objective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_29;
		Relay_Restart_Out_29();
	}

	private void SubGraph_CompleteObjectiveStage_Out_147(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_147 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_147;
		Relay_Out_147();
	}

	private void SubGraph_CompleteObjectiveStage_Out_150(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_150 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_150;
		Relay_Out_150();
	}

	private void SubGraph_CompleteObjectiveStage_Out_338(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_338 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_338;
		Relay_Out_338();
	}

	private void SubGraph_SaveLoadBool_Save_Out_447(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = e.boolean;
		local_AllTurretsDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_447;
		Relay_Save_Out_447();
	}

	private void SubGraph_SaveLoadBool_Load_Out_447(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = e.boolean;
		local_AllTurretsDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_447;
		Relay_Load_Out_447();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_447(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = e.boolean;
		local_AllTurretsDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_447;
		Relay_Restart_Out_447();
	}

	private void SubGraph_SaveLoadBool_Save_Out_457(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = e.boolean;
		local_RoundActive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_457;
		Relay_Save_Out_457();
	}

	private void SubGraph_SaveLoadBool_Load_Out_457(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = e.boolean;
		local_RoundActive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_457;
		Relay_Load_Out_457();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_457(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = e.boolean;
		local_RoundActive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_457;
		Relay_Restart_Out_457();
	}

	private void SubGraph_SaveLoadBool_Save_Out_458(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = e.boolean;
		local_PlayedEarlyMessage_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_458;
		Relay_Save_Out_458();
	}

	private void SubGraph_SaveLoadBool_Load_Out_458(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = e.boolean;
		local_PlayedEarlyMessage_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_458;
		Relay_Load_Out_458();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_458(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = e.boolean;
		local_PlayedEarlyMessage_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_458;
		Relay_Restart_Out_458();
	}

	private void SubGraph_SaveLoadBool_Save_Out_459(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_459 = e.boolean;
		local_VisitedNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_459;
		Relay_Save_Out_459();
	}

	private void SubGraph_SaveLoadBool_Load_Out_459(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_459 = e.boolean;
		local_VisitedNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_459;
		Relay_Load_Out_459();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_459(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_459 = e.boolean;
		local_VisitedNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_459;
		Relay_Restart_Out_459();
	}

	private void SubGraph_SaveLoadBool_Save_Out_460(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_460 = e.boolean;
		local_PlayedReadyGoDialogue_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_460;
		Relay_Save_Out_460();
	}

	private void SubGraph_SaveLoadBool_Load_Out_460(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_460 = e.boolean;
		local_PlayedReadyGoDialogue_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_460;
		Relay_Load_Out_460();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_460(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_460 = e.boolean;
		local_PlayedReadyGoDialogue_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_460;
		Relay_Restart_Out_460();
	}

	private void SubGraph_SaveLoadBool_Save_Out_461(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_461 = e.boolean;
		local_SetTeamsToEnemy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_461;
		Relay_Save_Out_461();
	}

	private void SubGraph_SaveLoadBool_Load_Out_461(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_461 = e.boolean;
		local_SetTeamsToEnemy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_461;
		Relay_Load_Out_461();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_461(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_461 = e.boolean;
		local_SetTeamsToEnemy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_461;
		Relay_Restart_Out_461();
	}

	private void SubGraph_SaveLoadInt_Save_Out_462(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_462 = e.integer;
		local_TurretsAlive_System_Int32 = logic_SubGraph_SaveLoadInt_integer_462;
		Relay_Save_Out_462();
	}

	private void SubGraph_SaveLoadInt_Load_Out_462(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_462 = e.integer;
		local_TurretsAlive_System_Int32 = logic_SubGraph_SaveLoadInt_integer_462;
		Relay_Load_Out_462();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_462(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_462 = e.integer;
		local_TurretsAlive_System_Int32 = logic_SubGraph_SaveLoadInt_integer_462;
		Relay_Restart_Out_462();
	}

	private void SubGraph_SaveLoadInt_Save_Out_463(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_463 = e.integer;
		local_ChargersAlive_System_Int32 = logic_SubGraph_SaveLoadInt_integer_463;
		Relay_Save_Out_463();
	}

	private void SubGraph_SaveLoadInt_Load_Out_463(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_463 = e.integer;
		local_ChargersAlive_System_Int32 = logic_SubGraph_SaveLoadInt_integer_463;
		Relay_Load_Out_463();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_463(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_463 = e.integer;
		local_ChargersAlive_System_Int32 = logic_SubGraph_SaveLoadInt_integer_463;
		Relay_Restart_Out_463();
	}

	private void SubGraph_SaveLoadInt_Save_Out_464(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_464 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_464;
		Relay_Save_Out_464();
	}

	private void SubGraph_SaveLoadInt_Load_Out_464(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_464 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_464;
		Relay_Load_Out_464();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_464(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_464 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_464;
		Relay_Restart_Out_464();
	}

	private void SubGraph_SaveLoadInt_Save_Out_465(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_465 = e.integer;
		local_DialogueProgressExtra_System_Int32 = logic_SubGraph_SaveLoadInt_integer_465;
		Relay_Save_Out_465();
	}

	private void SubGraph_SaveLoadInt_Load_Out_465(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_465 = e.integer;
		local_DialogueProgressExtra_System_Int32 = logic_SubGraph_SaveLoadInt_integer_465;
		Relay_Load_Out_465();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_465(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_465 = e.integer;
		local_DialogueProgressExtra_System_Int32 = logic_SubGraph_SaveLoadInt_integer_465;
		Relay_Restart_Out_465();
	}

	private void SubGraph_SaveLoadBool_Save_Out_479(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = e.boolean;
		local_PlayedShieldsDownDialogue_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_479;
		Relay_Save_Out_479();
	}

	private void SubGraph_SaveLoadBool_Load_Out_479(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = e.boolean;
		local_PlayedShieldsDownDialogue_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_479;
		Relay_Load_Out_479();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_479(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = e.boolean;
		local_PlayedShieldsDownDialogue_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_479;
		Relay_Restart_Out_479();
	}

	private void SubGraph_SaveLoadInt_Save_Out_481(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_481 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_481;
		Relay_Save_Out_481();
	}

	private void SubGraph_SaveLoadInt_Load_Out_481(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_481 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_481;
		Relay_Load_Out_481();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_481(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_481 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_481;
		Relay_Restart_Out_481();
	}

	private void SubGraph_CompleteObjectiveStage_Out_527(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_527 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_527;
		Relay_Out_527();
	}

	private void SubGraph_SaveLoadBool_Save_Out_530(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_530 = e.boolean;
		local_isAllInsideNPCTrigger_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_530;
		Relay_Save_Out_530();
	}

	private void SubGraph_SaveLoadBool_Load_Out_530(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_530 = e.boolean;
		local_isAllInsideNPCTrigger_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_530;
		Relay_Load_Out_530();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_530(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_530 = e.boolean;
		local_isAllInsideNPCTrigger_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_530;
		Relay_Restart_Out_530();
	}

	private void SubGraph_SaveLoadBool_Save_Out_531(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = e.boolean;
		local_isAllInsideArenaArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_531;
		Relay_Save_Out_531();
	}

	private void SubGraph_SaveLoadBool_Load_Out_531(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = e.boolean;
		local_isAllInsideArenaArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_531;
		Relay_Load_Out_531();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_531(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = e.boolean;
		local_isAllInsideArenaArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_531;
		Relay_Restart_Out_531();
	}

	private void SubGraph_SaveLoadBool_Save_Out_541(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_541 = e.boolean;
		local_PlayerDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_541;
		Relay_Save_Out_541();
	}

	private void SubGraph_SaveLoadBool_Load_Out_541(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_541 = e.boolean;
		local_PlayerDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_541;
		Relay_Load_Out_541();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_541(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_541 = e.boolean;
		local_PlayerDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_541;
		Relay_Restart_Out_541();
	}

	private void SubGraph_SaveLoadBool_Save_Out_644(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_644 = e.boolean;
		local_isInsideAnyCornerArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_644;
		Relay_Save_Out_644();
	}

	private void SubGraph_SaveLoadBool_Load_Out_644(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_644 = e.boolean;
		local_isInsideAnyCornerArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_644;
		Relay_Load_Out_644();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_644(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_644 = e.boolean;
		local_isInsideAnyCornerArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_644;
		Relay_Restart_Out_644();
	}

	private void SubGraph_LoadObjectiveStates_Out_687(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_687();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_80();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_SaveEvent_2()
	{
		Relay_Save_29();
	}

	private void Relay_LoadEvent_2()
	{
		Relay_Load_29();
	}

	private void Relay_RestartEvent_2()
	{
		Relay_Restart_29();
	}

	private void Relay_Succeed_5()
	{
		logic_uScript_FinishEncounter_owner_5 = owner_Connection_6;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_5.Succeed(logic_uScript_FinishEncounter_owner_5);
	}

	private void Relay_Fail_5()
	{
		logic_uScript_FinishEncounter_owner_5 = owner_Connection_6;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_5.Fail(logic_uScript_FinishEncounter_owner_5);
	}

	private void Relay_Pause_7()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_7.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_7.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_UnPause_7()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_7.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_7.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_8()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_8 = owner_Connection_9;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_8.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_8);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_8.Out)
		{
			Relay_True_82();
		}
	}

	private void Relay_Pause_10()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_10.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_10.Out)
		{
			Relay_Succeed_5();
		}
	}

	private void Relay_UnPause_10()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_10.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_10.Out)
		{
			Relay_Succeed_5();
		}
	}

	private void Relay_AtIndex_14()
	{
		int num = 0;
		Array array = local_R1MainTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_14.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_14, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_14, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_14.AtIndex(ref logic_uScript_AccessListTech_techList_14, logic_uScript_AccessListTech_index_14, out logic_uScript_AccessListTech_value_14);
		local_R1MainTechs_TankArray = logic_uScript_AccessListTech_techList_14;
		local_R1MainTech_Tank = logic_uScript_AccessListTech_value_14;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_14.Out)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_16()
	{
		int num = 0;
		Array r1MainTechData = R1MainTechData;
		if (logic_uScript_GetAndCheckTechs_techData_16.Length != num + r1MainTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_16, num + r1MainTechData.Length);
		}
		Array.Copy(r1MainTechData, 0, logic_uScript_GetAndCheckTechs_techData_16, num, r1MainTechData.Length);
		num += r1MainTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_16 = owner_Connection_15;
		int num2 = 0;
		Array array = local_R1MainTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_16.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_16, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_16, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_16 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.In(logic_uScript_GetAndCheckTechs_techData_16, logic_uScript_GetAndCheckTechs_ownerNode_16, ref logic_uScript_GetAndCheckTechs_techs_16);
		local_R1MainTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_16;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_22();
		}
		if (someAlive)
		{
			Relay_True_22();
		}
		if (allDead)
		{
			Relay_False_22();
		}
		if (waitingToSpawn)
		{
			Relay_False_22();
		}
	}

	private void Relay_Output1_18()
	{
		Relay_InitialSpawn_419();
	}

	private void Relay_Output2_18()
	{
		Relay_In_115();
	}

	private void Relay_Output3_18()
	{
		Relay_In_88();
	}

	private void Relay_Output4_18()
	{
		Relay_In_47();
	}

	private void Relay_Output5_18()
	{
		Relay_In_426();
	}

	private void Relay_Output6_18()
	{
		Relay_In_467();
	}

	private void Relay_Output7_18()
	{
		Relay_In_488();
	}

	private void Relay_Output8_18()
	{
	}

	private void Relay_In_18()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_18 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_18.In(logic_uScriptCon_ManualSwitch_CurrentOutput_18);
	}

	private void Relay_True_22()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.True(out logic_uScriptAct_SetBool_Target_22);
		local_S1T01Alive_System_Boolean = logic_uScriptAct_SetBool_Target_22;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_22.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_22.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_22.SetFalse;
		if (num)
		{
			Relay_In_159();
		}
		if (setTrue)
		{
			Relay_AtIndex_14();
		}
		if (setFalse)
		{
			Relay_In_662();
		}
	}

	private void Relay_False_22()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.False(out logic_uScriptAct_SetBool_Target_22);
		local_S1T01Alive_System_Boolean = logic_uScriptAct_SetBool_Target_22;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_22.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_22.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_22.SetFalse;
		if (num)
		{
			Relay_In_159();
		}
		if (setTrue)
		{
			Relay_AtIndex_14();
		}
		if (setFalse)
		{
			Relay_In_662();
		}
	}

	private void Relay_In_26()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_26 = EntranceTriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_26 = EntranceTriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_inside_26 = local_isInsideEntranceArea_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_26.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_26, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_26, ref logic_uScript_IsPlayerInTriggerSmart_inside_26);
		local_isInsideEntranceArea_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_26;
		if (logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_26.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_Save_Out_28()
	{
		Relay_Save_447();
	}

	private void Relay_Load_Out_28()
	{
		Relay_Load_447();
	}

	private void Relay_Restart_Out_28()
	{
		Relay_Set_False_447();
	}

	private void Relay_Save_28()
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = local_MissionStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_28 = local_MissionStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Save(ref logic_SubGraph_SaveLoadBool_boolean_28, logic_SubGraph_SaveLoadBool_boolAsVariable_28, logic_SubGraph_SaveLoadBool_uniqueID_28);
	}

	private void Relay_Load_28()
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = local_MissionStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_28 = local_MissionStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Load(ref logic_SubGraph_SaveLoadBool_boolean_28, logic_SubGraph_SaveLoadBool_boolAsVariable_28, logic_SubGraph_SaveLoadBool_uniqueID_28);
	}

	private void Relay_Set_True_28()
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = local_MissionStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_28 = local_MissionStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_28, logic_SubGraph_SaveLoadBool_boolAsVariable_28, logic_SubGraph_SaveLoadBool_uniqueID_28);
	}

	private void Relay_Set_False_28()
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = local_MissionStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_28 = local_MissionStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_28, logic_SubGraph_SaveLoadBool_boolAsVariable_28, logic_SubGraph_SaveLoadBool_uniqueID_28);
	}

	private void Relay_Save_Out_29()
	{
		Relay_Save_481();
	}

	private void Relay_Load_Out_29()
	{
		Relay_Load_481();
	}

	private void Relay_Restart_Out_29()
	{
		Relay_Restart_481();
	}

	private void Relay_Save_29()
	{
		logic_SubGraph_SaveLoadInt_integer_29 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_29 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Save(logic_SubGraph_SaveLoadInt_restartValue_29, ref logic_SubGraph_SaveLoadInt_integer_29, logic_SubGraph_SaveLoadInt_intAsVariable_29, logic_SubGraph_SaveLoadInt_uniqueID_29);
	}

	private void Relay_Load_29()
	{
		logic_SubGraph_SaveLoadInt_integer_29 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_29 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Load(logic_SubGraph_SaveLoadInt_restartValue_29, ref logic_SubGraph_SaveLoadInt_integer_29, logic_SubGraph_SaveLoadInt_intAsVariable_29, logic_SubGraph_SaveLoadInt_uniqueID_29);
	}

	private void Relay_Restart_29()
	{
		logic_SubGraph_SaveLoadInt_integer_29 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_29 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_29.Restart(logic_SubGraph_SaveLoadInt_restartValue_29, ref logic_SubGraph_SaveLoadInt_integer_29, logic_SubGraph_SaveLoadInt_intAsVariable_29, logic_SubGraph_SaveLoadInt_uniqueID_29);
	}

	private void Relay_In_32()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_32 = NPCTriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_32 = NPCTriggerAreaOuter;
		logic_uScript_IsPlayerInTriggerSmart_inside_32 = local_isInsideNPCTrigger_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_32.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_32, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_32, ref logic_uScript_IsPlayerInTriggerSmart_inside_32);
		local_isInsideNPCTrigger_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_32;
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_32.Out;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_32.AllInside;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_32.AllOutside;
		bool someOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_32.SomeOutside;
		if (num)
		{
			Relay_In_37();
		}
		if (allInside)
		{
			Relay_True_145();
		}
		if (allOutside)
		{
			Relay_False_145();
		}
		if (someOutside)
		{
			Relay_False_145();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_37 = RespawnTriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_37 = RespawnTriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_inside_37 = local_isInsideRespawnArea_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_37.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_37, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_37, ref logic_uScript_IsPlayerInTriggerSmart_inside_37);
		local_isInsideRespawnArea_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_37;
		if (logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_37.Out)
		{
			Relay_In_590();
		}
	}

	private void Relay_In_39()
	{
		logic_uScriptCon_CompareBool_Bool_39 = local_S1T01Alive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.In(logic_uScriptCon_CompareBool_Bool_39);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.False;
		if (num)
		{
			Relay_In_93();
		}
		if (flag)
		{
			Relay_In_117();
		}
	}

	private void Relay_AtIndex_40()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_40.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_40, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_40, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_40.AtIndex(ref logic_uScript_AccessListTech_techList_40, logic_uScript_AccessListTech_index_40, out logic_uScript_AccessListTech_value_40);
		local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_40;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_40;
	}

	private void Relay_In_44()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_44.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_44, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_44, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_44 = owner_Connection_45;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_44.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_44, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_44, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_44 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_44.In(logic_uScript_GetAndCheckTechs_techData_44, logic_uScript_GetAndCheckTechs_ownerNode_44, ref logic_uScript_GetAndCheckTechs_techs_44);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_44;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_44.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_44.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_44.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_44.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_339();
		}
		if (someAlive)
		{
			Relay_True_339();
		}
		if (allDead)
		{
			Relay_False_339();
		}
		if (waitingToSpawn)
		{
			Relay_False_339();
		}
	}

	private void Relay_In_47()
	{
		logic_uScriptCon_CompareBool_Bool_47 = local_RoundActive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.In(logic_uScriptCon_CompareBool_Bool_47);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.False;
		if (num)
		{
			Relay_In_39();
		}
		if (flag)
		{
			Relay_In_125();
		}
	}

	private void Relay_In_49()
	{
		logic_uScript_PlayDialogue_dialogue_49 = ReadyGoDialogue;
		logic_uScript_PlayDialogue_progress_49 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_49.In(logic_uScript_PlayDialogue_dialogue_49, ref logic_uScript_PlayDialogue_progress_49);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_49;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_49.Out)
		{
			Relay_True_233();
		}
	}

	private void Relay_In_52()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_52.In(logic_uScriptAct_SetInt_Value_52, out logic_uScriptAct_SetInt_Target_52);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_52;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_52.Out)
		{
			Relay_In_485();
		}
	}

	private void Relay_In_55()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_55.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_55.Out)
		{
			Relay_In_438();
		}
	}

	private void Relay_In_58()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_58.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_58, num + 1);
		}
		logic_uScriptAct_Concatenate_A_58[num++] = local_56_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_58.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_58, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_58[num2++] = local_Stage_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_58.In(logic_uScriptAct_Concatenate_A_58, logic_uScriptAct_Concatenate_B_58, logic_uScriptAct_Concatenate_Separator_58, out logic_uScriptAct_Concatenate_Result_58);
		local_73_System_String = logic_uScriptAct_Concatenate_Result_58;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_58.Out)
		{
			Relay_In_94();
		}
	}

	private void Relay_In_62()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_62.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_62, num + 1);
		}
		logic_uScriptAct_Concatenate_A_62[num++] = local_64_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_62.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_62, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_62[num2++] = local_RoundActive_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_62.In(logic_uScriptAct_Concatenate_A_62, logic_uScriptAct_Concatenate_B_62, logic_uScriptAct_Concatenate_Separator_62, out logic_uScriptAct_Concatenate_Result_62);
		local_75_System_String = logic_uScriptAct_Concatenate_Result_62;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_62.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_67()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_67.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_67, num + 1);
		}
		logic_uScriptAct_Concatenate_A_67[num++] = local_63_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_67.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_67, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_67[num2++] = local_59_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_67.In(logic_uScriptAct_Concatenate_A_67, logic_uScriptAct_Concatenate_B_67, logic_uScriptAct_Concatenate_Separator_67, out logic_uScriptAct_Concatenate_Result_67);
		local_61_System_String = logic_uScriptAct_Concatenate_Result_67;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_67.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_In_69()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_69.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_69, num + 1);
		}
		logic_uScriptAct_Concatenate_A_69[num++] = local_65_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_69.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_69, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_69[num2++] = local_isInsideEntranceArea_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_69.In(logic_uScriptAct_Concatenate_A_69, logic_uScriptAct_Concatenate_B_69, logic_uScriptAct_Concatenate_Separator_69, out logic_uScriptAct_Concatenate_Result_69);
		local_63_System_String = logic_uScriptAct_Concatenate_Result_69;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_69.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_In_71()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_71.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_71, num + 1);
		}
		logic_uScriptAct_Concatenate_A_71[num++] = local_57_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_71.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_71, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_71[num2++] = local_68_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_71.In(logic_uScriptAct_Concatenate_A_71, logic_uScriptAct_Concatenate_B_71, logic_uScriptAct_Concatenate_Separator_71, out logic_uScriptAct_Concatenate_Result_71);
		local_64_System_String = logic_uScriptAct_Concatenate_Result_71;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_71.Out)
		{
			Relay_In_62();
		}
	}

	private void Relay_In_72()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_72.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_72, num + 1);
		}
		logic_uScriptAct_Concatenate_A_72[num++] = local_61_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_72.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_72, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_72[num2++] = local_isInsideNPCTrigger_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_72.In(logic_uScriptAct_Concatenate_A_72, logic_uScriptAct_Concatenate_B_72, logic_uScriptAct_Concatenate_Separator_72, out logic_uScriptAct_Concatenate_Result_72);
		local_57_System_String = logic_uScriptAct_Concatenate_Result_72;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_72.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_ShowLabel_74()
	{
		logic_uScriptAct_PrintText_Text_74 = local_141_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_74.ShowLabel(logic_uScriptAct_PrintText_Text_74, logic_uScriptAct_PrintText_FontSize_74, logic_uScriptAct_PrintText_FontStyle_74, logic_uScriptAct_PrintText_FontColor_74, logic_uScriptAct_PrintText_textAnchor_74, logic_uScriptAct_PrintText_EdgePadding_74, logic_uScriptAct_PrintText_time_74);
	}

	private void Relay_HideLabel_74()
	{
		logic_uScriptAct_PrintText_Text_74 = local_141_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_74.HideLabel(logic_uScriptAct_PrintText_Text_74, logic_uScriptAct_PrintText_FontSize_74, logic_uScriptAct_PrintText_FontStyle_74, logic_uScriptAct_PrintText_FontColor_74, logic_uScriptAct_PrintText_textAnchor_74, logic_uScriptAct_PrintText_EdgePadding_74, logic_uScriptAct_PrintText_time_74);
	}

	private void Relay_In_76()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_76.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_76, num + 1);
		}
		logic_uScriptAct_Concatenate_A_76[num++] = local_95_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_76.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_76, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_76[num2++] = local_66_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_76.In(logic_uScriptAct_Concatenate_A_76, logic_uScriptAct_Concatenate_B_76, logic_uScriptAct_Concatenate_Separator_76, out logic_uScriptAct_Concatenate_Result_76);
		local_65_System_String = logic_uScriptAct_Concatenate_Result_76;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_76.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_80()
	{
		logic_uScriptCon_CompareBool_Bool_80 = local_MissionStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.In(logic_uScriptCon_CompareBool_Bool_80);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.False;
		if (num)
		{
			Relay_In_654();
		}
		if (flag)
		{
			Relay_In_8();
		}
	}

	private void Relay_True_82()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.True(out logic_uScriptAct_SetBool_Target_82);
		local_MissionStarted_System_Boolean = logic_uScriptAct_SetBool_Target_82;
	}

	private void Relay_False_82()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.False(out logic_uScriptAct_SetBool_Target_82);
		local_MissionStarted_System_Boolean = logic_uScriptAct_SetBool_Target_82;
	}

	private void Relay_In_85()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_85 = ArenaTriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_85 = ArenaTriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_inside_85 = local_isInsideArenaArea_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_85.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_85, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_85, ref logic_uScript_IsPlayerInTriggerSmart_inside_85);
		local_isInsideArenaArea_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_85;
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_85.Out;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_85.AllInside;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_85.AllOutside;
		bool someInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_85.SomeInside;
		if (num)
		{
			Relay_In_26();
		}
		if (allInside)
		{
			Relay_In_639();
		}
		if (allOutside)
		{
			Relay_In_118();
		}
		if (someInside)
		{
			Relay_In_639();
		}
	}

	private void Relay_In_88()
	{
		logic_uScriptCon_CompareBool_Bool_88 = local_isAllInsideArenaArea_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88.In(logic_uScriptCon_CompareBool_Bool_88);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88.False;
		if (num)
		{
			Relay_In_646();
		}
		if (flag)
		{
			Relay_In_656();
		}
	}

	private void Relay_In_91()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_91.In(logic_uScriptAct_SetInt_Value_91, out logic_uScriptAct_SetInt_Target_91);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_91;
	}

	private void Relay_In_93()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_93.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_93.Out)
		{
			Relay_In_314();
		}
	}

	private void Relay_In_94()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_94.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_94, num + 1);
		}
		logic_uScriptAct_Concatenate_A_94[num++] = local_73_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_94.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_94, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_94[num2++] = local_97_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_94.In(logic_uScriptAct_Concatenate_A_94, logic_uScriptAct_Concatenate_B_94, logic_uScriptAct_Concatenate_Separator_94, out logic_uScriptAct_Concatenate_Result_94);
		local_96_System_String = logic_uScriptAct_Concatenate_Result_94;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_94.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_In_98()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_98.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_98, num + 1);
		}
		logic_uScriptAct_Concatenate_A_98[num++] = local_96_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_98.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_98, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_98[num2++] = local_DialogueProgress_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_98.In(logic_uScriptAct_Concatenate_A_98, logic_uScriptAct_Concatenate_B_98, logic_uScriptAct_Concatenate_Separator_98, out logic_uScriptAct_Concatenate_Result_98);
		local_95_System_String = logic_uScriptAct_Concatenate_Result_98;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_98.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_In_102()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_102.In(logic_uScriptAct_SetInt_Value_102, out logic_uScriptAct_SetInt_Target_102);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_102;
	}

	private void Relay_In_103()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_103.In(logic_uScriptAct_SetInt_Value_103, out logic_uScriptAct_SetInt_Target_103);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_103;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_103.Out)
		{
			Relay_In_102();
		}
	}

	private void Relay_True_104()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.True(out logic_uScriptAct_SetBool_Target_104);
		local_RoundActive_System_Boolean = logic_uScriptAct_SetBool_Target_104;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_104.Out)
		{
			Relay_True_121();
		}
	}

	private void Relay_False_104()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.False(out logic_uScriptAct_SetBool_Target_104);
		local_RoundActive_System_Boolean = logic_uScriptAct_SetBool_Target_104;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_104.Out)
		{
			Relay_True_121();
		}
	}

	private void Relay_In_106()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_106.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_106.Out)
		{
			Relay_In_108();
		}
	}

	private void Relay_In_107()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_107.In(logic_uScriptAct_SetInt_Value_107, out logic_uScriptAct_SetInt_Target_107);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_107;
	}

	private void Relay_In_108()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_108.In(logic_uScriptAct_SetInt_Value_108, out logic_uScriptAct_SetInt_Target_108);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_108;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_108.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_111()
	{
		int num = 0;
		Array array = local_R1MainTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_111.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_111, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_111, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_111.In(ref logic_uScript_SetTechsTeam_techs_111, logic_uScript_SetTechsTeam_team_111);
		local_R1MainTechs_TankArray = logic_uScript_SetTechsTeam_techs_111;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_111.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_In_113()
	{
		logic_uScript_PlayDialogue_dialogue_113 = R1IntroDialogue;
		logic_uScript_PlayDialogue_progress_113 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_113.In(logic_uScript_PlayDialogue_dialogue_113, ref logic_uScript_PlayDialogue_progress_113);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_113;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_113.Shown)
		{
			Relay_In_352();
		}
	}

	private void Relay_In_115()
	{
		logic_uScriptCon_CompareBool_Bool_115 = local_VisitedNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.In(logic_uScriptCon_CompareBool_Bool_115);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.False;
		if (num)
		{
			Relay_In_688();
		}
		if (flag)
		{
			Relay_In_395();
		}
	}

	private void Relay_In_117()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117.Out)
		{
			Relay_In_333();
		}
	}

	private void Relay_In_118()
	{
		logic_uScriptCon_CompareBool_Bool_118 = local_RoundActive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.In(logic_uScriptCon_CompareBool_Bool_118);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.True)
		{
			Relay_False_123();
		}
	}

	private void Relay_True_119()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.True(out logic_uScriptAct_SetBool_Target_119);
		local_RoundActive_System_Boolean = logic_uScriptAct_SetBool_Target_119;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_119.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_False_119()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.False(out logic_uScriptAct_SetBool_Target_119);
		local_RoundActive_System_Boolean = logic_uScriptAct_SetBool_Target_119;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_119.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_True_121()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_121.True(out logic_uScriptAct_SetBool_Target_121);
		local_SetTeamsToEnemy_System_Boolean = logic_uScriptAct_SetBool_Target_121;
	}

	private void Relay_False_121()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_121.False(out logic_uScriptAct_SetBool_Target_121);
		local_SetTeamsToEnemy_System_Boolean = logic_uScriptAct_SetBool_Target_121;
	}

	private void Relay_True_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.True(out logic_uScriptAct_SetBool_Target_123);
		local_RoundActive_System_Boolean = logic_uScriptAct_SetBool_Target_123;
	}

	private void Relay_False_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.False(out logic_uScriptAct_SetBool_Target_123);
		local_RoundActive_System_Boolean = logic_uScriptAct_SetBool_Target_123;
	}

	private void Relay_In_125()
	{
		logic_uScriptCon_CompareBool_Bool_125 = local_SetTeamsToEnemy_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125.In(logic_uScriptCon_CompareBool_Bool_125);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125.False;
		if (num)
		{
			Relay_False_119();
		}
		if (flag)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_129()
	{
		int num = 0;
		Array array = local_R1MainTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_129.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_129, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_129, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_129.In(ref logic_uScript_SetTechsTeam_techs_129, logic_uScript_SetTechsTeam_team_129);
		local_R1MainTechs_TankArray = logic_uScript_SetTechsTeam_techs_129;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_129.Out)
		{
			Relay_In_218();
		}
	}

	private void Relay_In_133()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_133.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_133, num + 1);
		}
		logic_uScriptAct_Concatenate_A_133[num++] = local_137_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_133.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_133, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_133[num2++] = local_TurretsAlive_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_133.In(logic_uScriptAct_Concatenate_A_133, logic_uScriptAct_Concatenate_B_133, logic_uScriptAct_Concatenate_Separator_133, out logic_uScriptAct_Concatenate_Result_133);
		local_136_System_String = logic_uScriptAct_Concatenate_Result_133;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_133.Out)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_135()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_135.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_135, num + 1);
		}
		logic_uScriptAct_Concatenate_A_135[num++] = local_75_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_135.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_135, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_135[num2++] = local_134_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_135.In(logic_uScriptAct_Concatenate_A_135, logic_uScriptAct_Concatenate_B_135, logic_uScriptAct_Concatenate_Separator_135, out logic_uScriptAct_Concatenate_Result_135);
		local_137_System_String = logic_uScriptAct_Concatenate_Result_135;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_135.Out)
		{
			Relay_In_133();
		}
	}

	private void Relay_In_138()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_138.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_138, num + 1);
		}
		logic_uScriptAct_Concatenate_A_138[num++] = local_244_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_138.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_138, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_138[num2++] = local_AllTurretsDead_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_138.In(logic_uScriptAct_Concatenate_A_138, logic_uScriptAct_Concatenate_B_138, logic_uScriptAct_Concatenate_Separator_138, out logic_uScriptAct_Concatenate_Result_138);
		local_141_System_String = logic_uScriptAct_Concatenate_Result_138;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_138.Out)
		{
			Relay_ShowLabel_74();
		}
	}

	private void Relay_In_140()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_140.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_140, num + 1);
		}
		logic_uScriptAct_Concatenate_A_140[num++] = local_136_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_140.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_140, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_140[num2++] = local_139_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_140.In(logic_uScriptAct_Concatenate_A_140, logic_uScriptAct_Concatenate_B_140, logic_uScriptAct_Concatenate_Separator_140, out logic_uScriptAct_Concatenate_Result_140);
		local_142_System_String = logic_uScriptAct_Concatenate_Result_140;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_140.Out)
		{
			Relay_In_243();
		}
	}

	private void Relay_In_143()
	{
		logic_uScriptCon_CompareBool_Bool_143 = local_AllTurretsDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.In(logic_uScriptCon_CompareBool_Bool_143);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.False;
		if (num)
		{
			Relay_In_201();
		}
		if (flag)
		{
			Relay_In_213();
		}
	}

	private void Relay_True_145()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_145.True(out logic_uScriptAct_SetBool_Target_145);
		local_isAllInsideNPCTrigger_System_Boolean = logic_uScriptAct_SetBool_Target_145;
	}

	private void Relay_False_145()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_145.False(out logic_uScriptAct_SetBool_Target_145);
		local_isAllInsideNPCTrigger_System_Boolean = logic_uScriptAct_SetBool_Target_145;
	}

	private void Relay_Out_147()
	{
		Relay_In_106();
	}

	private void Relay_In_147()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_147 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_147.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_147, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_147);
	}

	private void Relay_Out_150()
	{
		Relay_In_49();
	}

	private void Relay_In_150()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_150 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_150, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_150);
	}

	private void Relay_In_153()
	{
		logic_uScript_SetEncounterTarget_owner_153 = owner_Connection_152;
		logic_uScript_SetEncounterTarget_visibleObject_153 = local_R1MainTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_153.In(logic_uScript_SetEncounterTarget_owner_153, logic_uScript_SetEncounterTarget_visibleObject_153);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_153.Out)
		{
			Relay_In_147();
		}
	}

	private void Relay_In_159()
	{
		int num = 0;
		Array turretTechData = TurretTechData;
		if (logic_uScript_GetAndCheckTechs_techData_159.Length != num + turretTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_159, num + turretTechData.Length);
		}
		Array.Copy(turretTechData, 0, logic_uScript_GetAndCheckTechs_techData_159, num, turretTechData.Length);
		num += turretTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_159 = owner_Connection_156;
		int num2 = 0;
		Array array = local_TurretTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_159.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_159, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_159, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_159 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159.In(logic_uScript_GetAndCheckTechs_techData_159, logic_uScript_GetAndCheckTechs_ownerNode_159, ref logic_uScript_GetAndCheckTechs_techs_159);
		local_TurretTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_159;
		local_TurretsAlive_System_Int32 = logic_uScript_GetAndCheckTechs_Return_159;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_160();
		}
		if (someAlive)
		{
			Relay_AtIndex_160();
		}
		if (allDead)
		{
			Relay_In_178();
		}
		if (waitingToSpawn)
		{
			Relay_In_178();
		}
	}

	private void Relay_AtIndex_160()
	{
		int num = 0;
		Array array = local_TurretTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_160.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_160, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_160, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_160.AtIndex(ref logic_uScript_AccessListTech_techList_160, logic_uScript_AccessListTech_index_160, out logic_uScript_AccessListTech_value_160);
		local_TurretTechs_TankArray = logic_uScript_AccessListTech_techList_160;
		local_R1Turret01Tech_Tank = logic_uScript_AccessListTech_value_160;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_160.Out)
		{
			Relay_AtIndex_163();
		}
	}

	private void Relay_AtIndex_163()
	{
		int num = 0;
		Array array = local_TurretTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_163.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_163, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_163, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_163.AtIndex(ref logic_uScript_AccessListTech_techList_163, logic_uScript_AccessListTech_index_163, out logic_uScript_AccessListTech_value_163);
		local_TurretTechs_TankArray = logic_uScript_AccessListTech_techList_163;
		local_R1Turret02Tech_Tank = logic_uScript_AccessListTech_value_163;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_163.Out)
		{
			Relay_AtIndex_165();
		}
	}

	private void Relay_AtIndex_165()
	{
		int num = 0;
		Array array = local_TurretTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_165.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_165, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_165, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_165.AtIndex(ref logic_uScript_AccessListTech_techList_165, logic_uScript_AccessListTech_index_165, out logic_uScript_AccessListTech_value_165);
		local_TurretTechs_TankArray = logic_uScript_AccessListTech_techList_165;
		local_R1Turret03Tech_Tank = logic_uScript_AccessListTech_value_165;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_165.Out)
		{
			Relay_AtIndex_168();
		}
	}

	private void Relay_AtIndex_168()
	{
		int num = 0;
		Array array = local_TurretTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_168.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_168, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_168, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_168.AtIndex(ref logic_uScript_AccessListTech_techList_168, logic_uScript_AccessListTech_index_168, out logic_uScript_AccessListTech_value_168);
		local_TurretTechs_TankArray = logic_uScript_AccessListTech_techList_168;
		local_R1Turret04Tech_Tank = logic_uScript_AccessListTech_value_168;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_168.Out)
		{
			Relay_In_174();
		}
	}

	private void Relay_AtIndex_170()
	{
		int num = 0;
		Array array = local_GuardianTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_170.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_170, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_170, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_170.AtIndex(ref logic_uScript_AccessListTech_techList_170, logic_uScript_AccessListTech_index_170, out logic_uScript_AccessListTech_value_170);
		local_GuardianTechs_TankArray = logic_uScript_AccessListTech_techList_170;
		local_Guardian01Tech_Tank = logic_uScript_AccessListTech_value_170;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_170.Out)
		{
			Relay_AtIndex_182();
		}
	}

	private void Relay_In_174()
	{
		int num = 0;
		Array guardianTechData = GuardianTechData;
		if (logic_uScript_GetAndCheckTechs_techData_174.Length != num + guardianTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_174, num + guardianTechData.Length);
		}
		Array.Copy(guardianTechData, 0, logic_uScript_GetAndCheckTechs_techData_174, num, guardianTechData.Length);
		num += guardianTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_174 = owner_Connection_173;
		int num2 = 0;
		Array array = local_GuardianTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_174.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_174, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_174, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_174 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_174.In(logic_uScript_GetAndCheckTechs_techData_174, logic_uScript_GetAndCheckTechs_ownerNode_174, ref logic_uScript_GetAndCheckTechs_techs_174);
		local_GuardianTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_174;
		local_ChargersAlive_System_Int32 = logic_uScript_GetAndCheckTechs_Return_174;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_174.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_174.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_174.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_174.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_170();
		}
		if (someAlive)
		{
			Relay_AtIndex_170();
		}
		if (allDead)
		{
			Relay_In_179();
		}
		if (waitingToSpawn)
		{
			Relay_In_179();
		}
	}

	private void Relay_In_177()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_177.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_177.Out)
		{
			Relay_In_174();
		}
	}

	private void Relay_In_178()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_178.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_178.Out)
		{
			Relay_In_177();
		}
	}

	private void Relay_In_179()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_179.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_179.Out)
		{
			Relay_In_180();
		}
	}

	private void Relay_In_180()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_180.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_180.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_AtIndex_182()
	{
		int num = 0;
		Array array = local_GuardianTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_182.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_182, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_182, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_182.AtIndex(ref logic_uScript_AccessListTech_techList_182, logic_uScript_AccessListTech_index_182, out logic_uScript_AccessListTech_value_182);
		local_GuardianTechs_TankArray = logic_uScript_AccessListTech_techList_182;
		local_Guardian02Tech_Tank = logic_uScript_AccessListTech_value_182;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_182.Out)
		{
			Relay_AtIndex_185();
		}
	}

	private void Relay_AtIndex_185()
	{
		int num = 0;
		Array array = local_GuardianTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_185.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_185, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_185, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_185.AtIndex(ref logic_uScript_AccessListTech_techList_185, logic_uScript_AccessListTech_index_185, out logic_uScript_AccessListTech_value_185);
		local_GuardianTechs_TankArray = logic_uScript_AccessListTech_techList_185;
		local_Guardian03Tech_Tank = logic_uScript_AccessListTech_value_185;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_185.Out)
		{
			Relay_AtIndex_188();
		}
	}

	private void Relay_AtIndex_188()
	{
		int num = 0;
		Array array = local_GuardianTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_188.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_188, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_188, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_188.AtIndex(ref logic_uScript_AccessListTech_techList_188, logic_uScript_AccessListTech_index_188, out logic_uScript_AccessListTech_value_188);
		local_GuardianTechs_TankArray = logic_uScript_AccessListTech_techList_188;
		local_Guardian04Tech_Tank = logic_uScript_AccessListTech_value_188;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_188.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_193()
	{
		int num = 0;
		Array array = local_TurretTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_193.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_193, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_193, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_193.In(ref logic_uScript_SetTechsTeam_techs_193, logic_uScript_SetTechsTeam_team_193);
		local_TurretTechs_TankArray = logic_uScript_SetTechsTeam_techs_193;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_193.Out)
		{
			Relay_In_219();
		}
	}

	private void Relay_In_196()
	{
		int num = 0;
		Array array = local_GuardianTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_196.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_196, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_196, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_196.In(ref logic_uScript_SetTechsTeam_techs_196, logic_uScript_SetTechsTeam_team_196);
		local_GuardianTechs_TankArray = logic_uScript_SetTechsTeam_techs_196;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_196.Out)
		{
			Relay_In_322();
		}
	}

	private void Relay_In_197()
	{
		logic_uScriptCon_CompareInt_B_197 = local_ChargersAlive_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_197.In(logic_uScriptCon_CompareInt_A_197, logic_uScriptCon_CompareInt_B_197);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_197.GreaterThan;
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_197.EqualTo;
		if (greaterThan)
		{
			Relay_False_216();
		}
		if (equalTo)
		{
			Relay_True_216();
		}
	}

	private void Relay_In_201()
	{
		logic_uScript_GetTankBlock_tank_201 = local_R1MainTech_Tank;
		logic_uScript_GetTankBlock_blockType_201 = S1MainTechShieldBlockData;
		logic_uScript_GetTankBlock_Return_201 = logic_uScript_GetTankBlock_uScript_GetTankBlock_201.In(logic_uScript_GetTankBlock_tank_201, logic_uScript_GetTankBlock_blockType_201);
		local_R1MainTechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_201;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_201.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_201.NotFound;
		if (returned)
		{
			Relay_In_202();
		}
		if (notFound)
		{
			Relay_In_302();
		}
	}

	private void Relay_In_202()
	{
		logic_uScript_SetShieldEnabled_targetObject_202 = local_R1MainTechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_202.In(logic_uScript_SetShieldEnabled_targetObject_202, logic_uScript_SetShieldEnabled_enable_202);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_202.Out)
		{
			Relay_In_678();
		}
	}

	private void Relay_In_204()
	{
		logic_uScript_SetShieldEnabled_targetObject_204 = local_R1MainTechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_204.In(logic_uScript_SetShieldEnabled_targetObject_204, logic_uScript_SetShieldEnabled_enable_204);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_204.Out)
		{
			Relay_In_210();
		}
	}

	private void Relay_In_207()
	{
		logic_uScript_SetBatteryChargeAmount_tech_207 = local_R1MainTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_207.In(logic_uScript_SetBatteryChargeAmount_tech_207, logic_uScript_SetBatteryChargeAmount_chargeAmount_207);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_207.Out)
		{
			Relay_In_670();
		}
	}

	private void Relay_In_210()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_210 = local_R1MainTech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_210.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_210, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_210);
		if (logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_210.Out)
		{
			Relay_In_207();
		}
	}

	private void Relay_In_213()
	{
		logic_uScript_GetTankBlock_tank_213 = local_R1MainTech_Tank;
		logic_uScript_GetTankBlock_blockType_213 = S1MainTechShieldBlockData;
		logic_uScript_GetTankBlock_Return_213 = logic_uScript_GetTankBlock_uScript_GetTankBlock_213.In(logic_uScript_GetTankBlock_tank_213, logic_uScript_GetTankBlock_blockType_213);
		local_R1MainTechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_213;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_213.Returned)
		{
			Relay_In_204();
		}
	}

	private void Relay_True_216()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_216.True(out logic_uScriptAct_SetBool_Target_216);
		local_AllTurretsDead_System_Boolean = logic_uScriptAct_SetBool_Target_216;
	}

	private void Relay_False_216()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_216.False(out logic_uScriptAct_SetBool_Target_216);
		local_AllTurretsDead_System_Boolean = logic_uScriptAct_SetBool_Target_216;
	}

	private void Relay_In_218()
	{
		logic_uScriptCon_CompareInt_B_218 = local_TurretsAlive_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_218.In(logic_uScriptCon_CompareInt_A_218, logic_uScriptCon_CompareInt_B_218);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_218.GreaterThan;
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_218.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_218.NotEqualTo;
		if (greaterThan)
		{
			Relay_In_193();
		}
		if (equalTo)
		{
			Relay_In_221();
		}
		if (notEqualTo)
		{
			Relay_In_193();
		}
	}

	private void Relay_In_219()
	{
		logic_uScriptCon_CompareInt_B_219 = local_ChargersAlive_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_219.In(logic_uScriptCon_CompareInt_A_219, logic_uScriptCon_CompareInt_B_219);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_219.GreaterThan;
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_219.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_219.NotEqualTo;
		if (greaterThan)
		{
			Relay_In_196();
		}
		if (equalTo)
		{
			Relay_In_222();
		}
		if (notEqualTo)
		{
			Relay_In_196();
		}
	}

	private void Relay_In_221()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221.Out)
		{
			Relay_In_219();
		}
	}

	private void Relay_In_222()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222.Out)
		{
			Relay_In_322();
		}
	}

	private void Relay_In_223()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_223.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_223.Out)
		{
			Relay_True_104();
		}
	}

	private void Relay_In_224()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_224.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_224.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_In_225()
	{
		int num = 0;
		Array array = local_GuardianTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_225.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_225, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_225, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_225.In(ref logic_uScript_SetTechsTeam_techs_225, logic_uScript_SetTechsTeam_team_225);
		local_GuardianTechs_TankArray = logic_uScript_SetTechsTeam_techs_225;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_225.Out)
		{
			Relay_True_104();
		}
	}

	private void Relay_In_228()
	{
		logic_uScriptCon_CompareInt_B_228 = local_ChargersAlive_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_228.In(logic_uScriptCon_CompareInt_A_228, logic_uScriptCon_CompareInt_B_228);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_228.GreaterThan;
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_228.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_228.NotEqualTo;
		if (greaterThan)
		{
			Relay_In_225();
		}
		if (equalTo)
		{
			Relay_In_223();
		}
		if (notEqualTo)
		{
			Relay_In_225();
		}
	}

	private void Relay_In_229()
	{
		int num = 0;
		Array array = local_TurretTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_229.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_229, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_229, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_229.In(ref logic_uScript_SetTechsTeam_techs_229, logic_uScript_SetTechsTeam_team_229);
		local_TurretTechs_TankArray = logic_uScript_SetTechsTeam_techs_229;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_229.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_In_231()
	{
		logic_uScriptCon_CompareInt_B_231 = local_TurretsAlive_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_231.In(logic_uScriptCon_CompareInt_A_231, logic_uScriptCon_CompareInt_B_231);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_231.GreaterThan;
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_231.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_231.NotEqualTo;
		if (greaterThan)
		{
			Relay_In_229();
		}
		if (equalTo)
		{
			Relay_In_224();
		}
		if (notEqualTo)
		{
			Relay_In_229();
		}
	}

	private void Relay_True_233()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_233.True(out logic_uScriptAct_SetBool_Target_233);
		local_PlayedReadyGoDialogue_System_Boolean = logic_uScriptAct_SetBool_Target_233;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_233.Out)
		{
			Relay_InitialSpawn_388();
		}
	}

	private void Relay_False_233()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_233.False(out logic_uScriptAct_SetBool_Target_233);
		local_PlayedReadyGoDialogue_System_Boolean = logic_uScriptAct_SetBool_Target_233;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_233.Out)
		{
			Relay_InitialSpawn_388();
		}
	}

	private void Relay_In_234()
	{
		logic_uScriptCon_CompareBool_Bool_234 = local_PlayedReadyGoDialogue_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_234.In(logic_uScriptCon_CompareBool_Bool_234);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_234.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_234.False;
		if (num)
		{
			Relay_In_237();
		}
		if (flag)
		{
			Relay_In_150();
		}
	}

	private void Relay_In_237()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_237.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_237.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_242()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_242.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_242, num + 1);
		}
		logic_uScriptAct_Concatenate_A_242[num++] = local_241_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_242.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_242, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_242[num2++] = local_240_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_242.In(logic_uScriptAct_Concatenate_A_242, logic_uScriptAct_Concatenate_B_242, logic_uScriptAct_Concatenate_Separator_242, out logic_uScriptAct_Concatenate_Result_242);
		local_244_System_String = logic_uScriptAct_Concatenate_Result_242;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_242.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_243()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_243.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_243, num + 1);
		}
		logic_uScriptAct_Concatenate_A_243[num++] = local_142_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_243.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_243, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_243[num2++] = local_ChargersAlive_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_243.In(logic_uScriptAct_Concatenate_A_243, logic_uScriptAct_Concatenate_B_243, logic_uScriptAct_Concatenate_Separator_243, out logic_uScriptAct_Concatenate_Result_243);
		local_241_System_String = logic_uScriptAct_Concatenate_Result_243;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_243.Out)
		{
			Relay_In_242();
		}
	}

	private void Relay_In_245()
	{
		logic_uScript_SetShieldEnabled_targetObject_245 = local_R1Turret01TechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_245.In(logic_uScript_SetShieldEnabled_targetObject_245, logic_uScript_SetShieldEnabled_enable_245);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_245.Out)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_247()
	{
		logic_uScript_SetBatteryChargeAmount_tech_247 = local_R1Turret01Tech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_247.In(logic_uScript_SetBatteryChargeAmount_tech_247, logic_uScript_SetBatteryChargeAmount_chargeAmount_247);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_247.Out)
		{
			Relay_In_672();
		}
	}

	private void Relay_In_248()
	{
		logic_uScript_GetTankBlock_tank_248 = local_R1Turret01Tech_Tank;
		logic_uScript_GetTankBlock_blockType_248 = R1Turret01TechShieldBlockData;
		logic_uScript_GetTankBlock_Return_248 = logic_uScript_GetTankBlock_uScript_GetTankBlock_248.In(logic_uScript_GetTankBlock_tank_248, logic_uScript_GetTankBlock_blockType_248);
		local_R1Turret01TechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_248;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_248.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_248.NotFound;
		if (returned)
		{
			Relay_In_245();
		}
		if (notFound)
		{
			Relay_In_672();
		}
	}

	private void Relay_In_251()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_251 = local_R1Turret01Tech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_251.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_251, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_251);
		if (logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_251.Out)
		{
			Relay_In_247();
		}
	}

	private void Relay_In_255()
	{
		logic_uScript_SetShieldEnabled_targetObject_255 = local_R1Turret02TechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_255.In(logic_uScript_SetShieldEnabled_targetObject_255, logic_uScript_SetShieldEnabled_enable_255);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_255.Out)
		{
			Relay_In_258();
		}
	}

	private void Relay_In_257()
	{
		logic_uScript_GetTankBlock_tank_257 = local_R1Turret02Tech_Tank;
		logic_uScript_GetTankBlock_blockType_257 = R1Turret02TechShieldBlockData;
		logic_uScript_GetTankBlock_Return_257 = logic_uScript_GetTankBlock_uScript_GetTankBlock_257.In(logic_uScript_GetTankBlock_tank_257, logic_uScript_GetTankBlock_blockType_257);
		local_R1Turret02TechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_257;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_257.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_257.NotFound;
		if (returned)
		{
			Relay_In_255();
		}
		if (notFound)
		{
			Relay_In_674();
		}
	}

	private void Relay_In_258()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_258 = local_R1Turret02Tech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_258.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_258, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_258);
		if (logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_258.Out)
		{
			Relay_In_259();
		}
	}

	private void Relay_In_259()
	{
		logic_uScript_SetBatteryChargeAmount_tech_259 = local_R1Turret02Tech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_259.In(logic_uScript_SetBatteryChargeAmount_tech_259, logic_uScript_SetBatteryChargeAmount_chargeAmount_259);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_259.Out)
		{
			Relay_In_674();
		}
	}

	private void Relay_In_265()
	{
		logic_uScript_GetTankBlock_tank_265 = local_R1Turret03Tech_Tank;
		logic_uScript_GetTankBlock_blockType_265 = R1Turret03TechShieldBlockData;
		logic_uScript_GetTankBlock_Return_265 = logic_uScript_GetTankBlock_uScript_GetTankBlock_265.In(logic_uScript_GetTankBlock_tank_265, logic_uScript_GetTankBlock_blockType_265);
		local_R1Turret03TechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_265;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_265.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_265.NotFound;
		if (returned)
		{
			Relay_In_267();
		}
		if (notFound)
		{
			Relay_In_676();
		}
	}

	private void Relay_In_267()
	{
		logic_uScript_SetShieldEnabled_targetObject_267 = local_R1Turret03TechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_267.In(logic_uScript_SetShieldEnabled_targetObject_267, logic_uScript_SetShieldEnabled_enable_267);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_267.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_269()
	{
		logic_uScript_SetBatteryChargeAmount_tech_269 = local_R1Turret03Tech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_269.In(logic_uScript_SetBatteryChargeAmount_tech_269, logic_uScript_SetBatteryChargeAmount_chargeAmount_269);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_269.Out)
		{
			Relay_In_676();
		}
	}

	private void Relay_In_271()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_271 = local_R1Turret03Tech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_271.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_271, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_271);
		if (logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_271.Out)
		{
			Relay_In_269();
		}
	}

	private void Relay_In_277()
	{
		logic_uScript_GetTankBlock_tank_277 = local_R1Turret04Tech_Tank;
		logic_uScript_GetTankBlock_blockType_277 = R1Turret04TechShieldBlockData;
		logic_uScript_GetTankBlock_Return_277 = logic_uScript_GetTankBlock_uScript_GetTankBlock_277.In(logic_uScript_GetTankBlock_tank_277, logic_uScript_GetTankBlock_blockType_277);
		local_R1Turret04TechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_277;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_277.Returned)
		{
			Relay_In_283();
		}
	}

	private void Relay_In_279()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_279 = local_R1Turret04Tech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_279.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_279, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_279);
		if (logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_279.Out)
		{
			Relay_In_282();
		}
	}

	private void Relay_In_282()
	{
		logic_uScript_SetBatteryChargeAmount_tech_282 = local_R1Turret04Tech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_282.In(logic_uScript_SetBatteryChargeAmount_tech_282, logic_uScript_SetBatteryChargeAmount_chargeAmount_282);
	}

	private void Relay_In_283()
	{
		logic_uScript_SetShieldEnabled_targetObject_283 = local_R1Turret04TechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_283.In(logic_uScript_SetShieldEnabled_targetObject_283, logic_uScript_SetShieldEnabled_enable_283);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_283.Out)
		{
			Relay_In_279();
		}
	}

	private void Relay_In_287()
	{
		logic_uScript_GetTankBlock_tank_287 = local_R1Turret02Tech_Tank;
		logic_uScript_GetTankBlock_blockType_287 = R1Turret02TechShieldBlockData;
		logic_uScript_GetTankBlock_Return_287 = logic_uScript_GetTankBlock_uScript_GetTankBlock_287.In(logic_uScript_GetTankBlock_tank_287, logic_uScript_GetTankBlock_blockType_287);
		local_R1Turret02TechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_287;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_287.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_287.NotFound;
		if (returned)
		{
			Relay_In_306();
		}
		if (notFound)
		{
			Relay_In_309();
		}
	}

	private void Relay_In_288()
	{
		logic_uScript_GetTankBlock_tank_288 = local_R1Turret03Tech_Tank;
		logic_uScript_GetTankBlock_blockType_288 = R1Turret03TechShieldBlockData;
		logic_uScript_GetTankBlock_Return_288 = logic_uScript_GetTankBlock_uScript_GetTankBlock_288.In(logic_uScript_GetTankBlock_tank_288, logic_uScript_GetTankBlock_blockType_288);
		local_R1Turret03TechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_288;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_288.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_288.NotFound;
		if (returned)
		{
			Relay_In_307();
		}
		if (notFound)
		{
			Relay_In_310();
		}
	}

	private void Relay_In_291()
	{
		logic_uScript_GetTankBlock_tank_291 = local_R1Turret01Tech_Tank;
		logic_uScript_GetTankBlock_blockType_291 = R1Turret01TechShieldBlockData;
		logic_uScript_GetTankBlock_Return_291 = logic_uScript_GetTankBlock_uScript_GetTankBlock_291.In(logic_uScript_GetTankBlock_tank_291, logic_uScript_GetTankBlock_blockType_291);
		local_R1Turret01TechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_291;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_291.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_291.NotFound;
		if (returned)
		{
			Relay_In_304();
		}
		if (notFound)
		{
			Relay_In_303();
		}
	}

	private void Relay_In_300()
	{
		logic_uScript_GetTankBlock_tank_300 = local_R1Turret04Tech_Tank;
		logic_uScript_GetTankBlock_blockType_300 = R1Turret04TechShieldBlockData;
		logic_uScript_GetTankBlock_Return_300 = logic_uScript_GetTankBlock_uScript_GetTankBlock_300.In(logic_uScript_GetTankBlock_tank_300, logic_uScript_GetTankBlock_blockType_300);
		local_R1Turret04TechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_300;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_300.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_300.NotFound;
		if (returned)
		{
			Relay_In_308();
		}
		if (notFound)
		{
			Relay_In_476();
		}
	}

	private void Relay_In_302()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_302.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_302.Out)
		{
			Relay_In_678();
		}
	}

	private void Relay_In_303()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_303.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_303.Out)
		{
			Relay_In_680();
		}
	}

	private void Relay_In_304()
	{
		logic_uScript_SetShieldEnabled_targetObject_304 = local_R1Turret01TechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_304.In(logic_uScript_SetShieldEnabled_targetObject_304, logic_uScript_SetShieldEnabled_enable_304);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_304.Out)
		{
			Relay_In_680();
		}
	}

	private void Relay_In_306()
	{
		logic_uScript_SetShieldEnabled_targetObject_306 = local_R1Turret02TechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_306.In(logic_uScript_SetShieldEnabled_targetObject_306, logic_uScript_SetShieldEnabled_enable_306);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_306.Out)
		{
			Relay_In_683();
		}
	}

	private void Relay_In_307()
	{
		logic_uScript_SetShieldEnabled_targetObject_307 = local_R1Turret03TechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_307.In(logic_uScript_SetShieldEnabled_targetObject_307, logic_uScript_SetShieldEnabled_enable_307);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_307.Out)
		{
			Relay_In_684();
		}
	}

	private void Relay_In_308()
	{
		logic_uScript_SetShieldEnabled_targetObject_308 = local_R1Turret04TechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_308.In(logic_uScript_SetShieldEnabled_targetObject_308, logic_uScript_SetShieldEnabled_enable_308);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_308.Out)
		{
			Relay_In_474();
		}
	}

	private void Relay_In_309()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_309.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_309.Out)
		{
			Relay_In_683();
		}
	}

	private void Relay_In_310()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_310.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_310.Out)
		{
			Relay_In_684();
		}
	}

	private void Relay_In_314()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_314.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_314.Out)
		{
			Relay_In_372();
		}
	}

	private void Relay_In_319()
	{
		logic_uScriptCon_CompareBool_Bool_319 = local_isInsideEntranceArea_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_319.In(logic_uScriptCon_CompareBool_Bool_319);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_319.False)
		{
			Relay_In_234();
		}
	}

	private void Relay_In_322()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_322.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_322.Out)
		{
			Relay_In_327();
		}
	}

	private void Relay_In_323()
	{
		int num = 0;
		if (logic_uScript_DestroyTechsFromData_techData_323.Length <= num)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_323, num + 1);
		}
		logic_uScript_DestroyTechsFromData_techData_323[num++] = R2Child02TechData;
		logic_uScript_DestroyTechsFromData_ownerNode_323 = owner_Connection_324;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_323.In(logic_uScript_DestroyTechsFromData_techData_323, logic_uScript_DestroyTechsFromData_shouldExplode_323, logic_uScript_DestroyTechsFromData_ownerNode_323);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_323.Out)
		{
			Relay_In_325();
		}
	}

	private void Relay_In_325()
	{
		int num = 0;
		if (logic_uScript_DestroyTechsFromData_techData_325.Length <= num)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_325, num + 1);
		}
		logic_uScript_DestroyTechsFromData_techData_325[num++] = R2Child03TechData;
		logic_uScript_DestroyTechsFromData_ownerNode_325 = owner_Connection_326;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_325.In(logic_uScript_DestroyTechsFromData_techData_325, logic_uScript_DestroyTechsFromData_shouldExplode_325, logic_uScript_DestroyTechsFromData_ownerNode_325);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_325.Out)
		{
			Relay_In_533();
		}
	}

	private void Relay_In_327()
	{
		int num = 0;
		if (logic_uScript_DestroyTechsFromData_techData_327.Length <= num)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_327, num + 1);
		}
		logic_uScript_DestroyTechsFromData_techData_327[num++] = R2Child01TechData;
		logic_uScript_DestroyTechsFromData_ownerNode_327 = owner_Connection_328;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_327.In(logic_uScript_DestroyTechsFromData_techData_327, logic_uScript_DestroyTechsFromData_shouldExplode_327, logic_uScript_DestroyTechsFromData_ownerNode_327);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_327.Out)
		{
			Relay_In_323();
		}
	}

	private void Relay_True_329()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_329.True(out logic_uScriptAct_SetBool_Target_329);
		local_SetTeamsToEnemy_System_Boolean = logic_uScriptAct_SetBool_Target_329;
	}

	private void Relay_False_329()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_329.False(out logic_uScriptAct_SetBool_Target_329);
		local_SetTeamsToEnemy_System_Boolean = logic_uScriptAct_SetBool_Target_329;
	}

	private void Relay_In_332()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_332.In(logic_uScriptAct_SetInt_Value_332, out logic_uScriptAct_SetInt_Target_332);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_332;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_332.Out)
		{
			Relay_False_329();
		}
	}

	private void Relay_In_333()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_333.In(logic_uScriptAct_SetInt_Value_333, out logic_uScriptAct_SetInt_Target_333);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_333;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_333.Out)
		{
			Relay_In_335();
		}
	}

	private void Relay_In_335()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_335.In(logic_uScriptAct_SetInt_Value_335, out logic_uScriptAct_SetInt_Target_335);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_335;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_335.Out)
		{
			Relay_In_338();
		}
	}

	private void Relay_Out_338()
	{
	}

	private void Relay_In_338()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_338 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_338.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_338, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_338);
	}

	private void Relay_True_339()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_339.True(out logic_uScriptAct_SetBool_Target_339);
		local_NPCTechAlive_System_Boolean = logic_uScriptAct_SetBool_Target_339;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_339.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_339.SetTrue;
		if (num)
		{
			Relay_In_505();
		}
		if (setTrue)
		{
			Relay_AtIndex_40();
		}
	}

	private void Relay_False_339()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_339.False(out logic_uScriptAct_SetBool_Target_339);
		local_NPCTechAlive_System_Boolean = logic_uScriptAct_SetBool_Target_339;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_339.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_339.SetTrue;
		if (num)
		{
			Relay_In_505();
		}
		if (setTrue)
		{
			Relay_AtIndex_40();
		}
	}

	private void Relay_In_342()
	{
		logic_uScriptCon_CompareBool_Bool_342 = local_NPCTechAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_342.In(logic_uScriptCon_CompareBool_Bool_342);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_342.True)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_345()
	{
		logic_uScriptCon_CompareBool_Bool_345 = local_isAllInsideNPCTrigger_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_345.In(logic_uScriptCon_CompareBool_Bool_345);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_345.True)
		{
			Relay_True_347();
		}
	}

	private void Relay_True_347()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_347.True(out logic_uScriptAct_SetBool_Target_347);
		local_VisitedNPC_System_Boolean = logic_uScriptAct_SetBool_Target_347;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_347.Out)
		{
			Relay_False_408();
		}
	}

	private void Relay_False_347()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_347.False(out logic_uScriptAct_SetBool_Target_347);
		local_VisitedNPC_System_Boolean = logic_uScriptAct_SetBool_Target_347;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_347.Out)
		{
			Relay_False_408();
		}
	}

	private void Relay_In_352()
	{
		logic_uScript_FlyTechUpAndAway_tech_352 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_352 = FlyAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_352 = FlyParticle;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_352.In(logic_uScript_FlyTechUpAndAway_tech_352, logic_uScript_FlyTechUpAndAway_maxLifetime_352, logic_uScript_FlyTechUpAndAway_targetHeight_352, logic_uScript_FlyTechUpAndAway_aiTree_352, logic_uScript_FlyTechUpAndAway_removalParticles_352);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_352.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_357()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_357.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_357, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_357[num++] = R2Child02TechData;
		logic_uScript_GetAndCheckTechs_ownerNode_357 = owner_Connection_355;
		int num2 = 0;
		Array array = local_Child02Techs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_357.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_357, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_357, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_357 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.In(logic_uScript_GetAndCheckTechs_techData_357, logic_uScript_GetAndCheckTechs_ownerNode_357, ref logic_uScript_GetAndCheckTechs_techs_357);
		local_Child02Techs_TankArray = logic_uScript_GetAndCheckTechs_techs_357;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_358();
		}
		if (someAlive)
		{
			Relay_True_358();
		}
		if (allDead)
		{
			Relay_False_358();
		}
		if (waitingToSpawn)
		{
			Relay_False_358();
		}
	}

	private void Relay_True_358()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_358.True(out logic_uScriptAct_SetBool_Target_358);
		local_Child02Alive_System_Boolean = logic_uScriptAct_SetBool_Target_358;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_358.Out;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_358.SetFalse;
		if (num)
		{
			Relay_In_365();
		}
		if (setFalse)
		{
			Relay_In_383();
		}
	}

	private void Relay_False_358()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_358.False(out logic_uScriptAct_SetBool_Target_358);
		local_Child02Alive_System_Boolean = logic_uScriptAct_SetBool_Target_358;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_358.Out;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_358.SetFalse;
		if (num)
		{
			Relay_In_365();
		}
		if (setFalse)
		{
			Relay_In_383();
		}
	}

	private void Relay_In_365()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_365.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_365, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_365[num++] = R2Child03TechData;
		logic_uScript_GetAndCheckTechs_ownerNode_365 = owner_Connection_370;
		int num2 = 0;
		Array array = local_Child03Techs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_365.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_365, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_365, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_365 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_365.In(logic_uScript_GetAndCheckTechs_techData_365, logic_uScript_GetAndCheckTechs_ownerNode_365, ref logic_uScript_GetAndCheckTechs_techs_365);
		local_Child03Techs_TankArray = logic_uScript_GetAndCheckTechs_techs_365;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_365.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_365.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_365.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_365.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_371();
		}
		if (someAlive)
		{
			Relay_True_371();
		}
		if (allDead)
		{
			Relay_False_371();
		}
		if (waitingToSpawn)
		{
			Relay_False_371();
		}
	}

	private void Relay_True_368()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_368.True(out logic_uScriptAct_SetBool_Target_368);
		local_Child01Alive_System_Boolean = logic_uScriptAct_SetBool_Target_368;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_368.Out;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_368.SetFalse;
		if (num)
		{
			Relay_In_357();
		}
		if (setFalse)
		{
			Relay_In_382();
		}
	}

	private void Relay_False_368()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_368.False(out logic_uScriptAct_SetBool_Target_368);
		local_Child01Alive_System_Boolean = logic_uScriptAct_SetBool_Target_368;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_368.Out;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_368.SetFalse;
		if (num)
		{
			Relay_In_357();
		}
		if (setFalse)
		{
			Relay_In_382();
		}
	}

	private void Relay_True_371()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_371.True(out logic_uScriptAct_SetBool_Target_371);
		local_Child03Alive_System_Boolean = logic_uScriptAct_SetBool_Target_371;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_371.Out;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_371.SetFalse;
		if (num)
		{
			Relay_In_197();
		}
		if (setFalse)
		{
			Relay_In_384();
		}
	}

	private void Relay_False_371()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_371.False(out logic_uScriptAct_SetBool_Target_371);
		local_Child03Alive_System_Boolean = logic_uScriptAct_SetBool_Target_371;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_371.Out;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_371.SetFalse;
		if (num)
		{
			Relay_In_197();
		}
		if (setFalse)
		{
			Relay_In_384();
		}
	}

	private void Relay_In_372()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_372.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_372, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_372[num++] = R2Child01TechData;
		logic_uScript_GetAndCheckTechs_ownerNode_372 = owner_Connection_356;
		int num2 = 0;
		Array array = local_Child01Techs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_372.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_372, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_372, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_372 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_372.In(logic_uScript_GetAndCheckTechs_techData_372, logic_uScript_GetAndCheckTechs_ownerNode_372, ref logic_uScript_GetAndCheckTechs_techs_372);
		local_Child01Techs_TankArray = logic_uScript_GetAndCheckTechs_techs_372;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_372.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_372.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_372.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_372.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_368();
		}
		if (someAlive)
		{
			Relay_True_368();
		}
		if (allDead)
		{
			Relay_False_368();
		}
		if (waitingToSpawn)
		{
			Relay_False_368();
		}
	}

	private void Relay_InitialSpawn_374()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_374.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_374, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_374[num++] = R2Child02TechData;
		logic_uScript_SpawnTechsFromData_ownerNode_374 = owner_Connection_380;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_374.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_374, logic_uScript_SpawnTechsFromData_ownerNode_374, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_374, logic_uScript_SpawnTechsFromData_allowResurrection_374);
	}

	private void Relay_InitialSpawn_378()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_378.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_378, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_378[num++] = R2Child01TechData;
		logic_uScript_SpawnTechsFromData_ownerNode_378 = owner_Connection_373;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_378.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_378, logic_uScript_SpawnTechsFromData_ownerNode_378, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_378, logic_uScript_SpawnTechsFromData_allowResurrection_378);
	}

	private void Relay_InitialSpawn_379()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_379.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_379, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_379[num++] = R2Child03TechData;
		logic_uScript_SpawnTechsFromData_ownerNode_379 = owner_Connection_375;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_379.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_379, logic_uScript_SpawnTechsFromData_ownerNode_379, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_379, logic_uScript_SpawnTechsFromData_allowResurrection_379);
	}

	private void Relay_In_382()
	{
		logic_uScript_Wait_uScript_Wait_382.In(logic_uScript_Wait_seconds_382, logic_uScript_Wait_repeat_382);
		if (logic_uScript_Wait_uScript_Wait_382.Waited)
		{
			Relay_InitialSpawn_378();
		}
	}

	private void Relay_In_383()
	{
		logic_uScript_Wait_uScript_Wait_383.In(logic_uScript_Wait_seconds_383, logic_uScript_Wait_repeat_383);
		if (logic_uScript_Wait_uScript_Wait_383.Waited)
		{
			Relay_InitialSpawn_374();
		}
	}

	private void Relay_In_384()
	{
		logic_uScript_Wait_uScript_Wait_384.In(logic_uScript_Wait_seconds_384, logic_uScript_Wait_repeat_384);
		if (logic_uScript_Wait_uScript_Wait_384.Waited)
		{
			Relay_InitialSpawn_379();
		}
	}

	private void Relay_InitialSpawn_386()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_386.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_386, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_386[num++] = R2Child03TechData;
		logic_uScript_SpawnTechsFromData_ownerNode_386 = owner_Connection_389;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_386.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_386, logic_uScript_SpawnTechsFromData_ownerNode_386, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_386, logic_uScript_SpawnTechsFromData_allowResurrection_386);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_386.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_InitialSpawn_387()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_387.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_387, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_387[num++] = R2Child02TechData;
		logic_uScript_SpawnTechsFromData_ownerNode_387 = owner_Connection_393;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_387.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_387, logic_uScript_SpawnTechsFromData_ownerNode_387, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_387, logic_uScript_SpawnTechsFromData_allowResurrection_387);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_387.Out)
		{
			Relay_InitialSpawn_386();
		}
	}

	private void Relay_InitialSpawn_388()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_388.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_388, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_388[num++] = R2Child01TechData;
		logic_uScript_SpawnTechsFromData_ownerNode_388 = owner_Connection_391;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_388.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_388, logic_uScript_SpawnTechsFromData_ownerNode_388, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_388, logic_uScript_SpawnTechsFromData_allowResurrection_388);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_388.Out)
		{
			Relay_InitialSpawn_387();
		}
	}

	private void Relay_In_395()
	{
		logic_uScript_SetEncounterTarget_owner_395 = owner_Connection_394;
		logic_uScript_SetEncounterTarget_visibleObject_395 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_395.In(logic_uScript_SetEncounterTarget_owner_395, logic_uScript_SetEncounterTarget_visibleObject_395);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_395.Out)
		{
			Relay_In_345();
		}
	}

	private void Relay_In_397()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_397.In(logic_uScriptAct_SetInt_Value_397, out logic_uScriptAct_SetInt_Target_397);
		local_DialogueProgressExtra_System_Int32 = logic_uScriptAct_SetInt_Target_397;
	}

	private void Relay_In_400()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_400.In(logic_uScriptAct_SetInt_Value_400, out logic_uScriptAct_SetInt_Target_400);
		local_DialogueProgressExtra_System_Int32 = logic_uScriptAct_SetInt_Target_400;
	}

	private void Relay_True_402()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_402.True(out logic_uScriptAct_SetBool_Target_402);
		local_VisitedNPC_System_Boolean = logic_uScriptAct_SetBool_Target_402;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_402.Out)
		{
			Relay_True_406();
		}
	}

	private void Relay_False_402()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_402.False(out logic_uScriptAct_SetBool_Target_402);
		local_VisitedNPC_System_Boolean = logic_uScriptAct_SetBool_Target_402;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_402.Out)
		{
			Relay_True_406();
		}
	}

	private void Relay_In_404()
	{
		logic_uScriptCon_CompareBool_Bool_404 = local_PlayedEarlyMessage_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_404.In(logic_uScriptCon_CompareBool_Bool_404);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_404.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_404.False;
		if (num)
		{
			Relay_In_397();
		}
		if (flag)
		{
			Relay_In_410();
		}
	}

	private void Relay_True_406()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_406.True(out logic_uScriptAct_SetBool_Target_406);
		local_PlayedEarlyMessage_System_Boolean = logic_uScriptAct_SetBool_Target_406;
	}

	private void Relay_False_406()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_406.False(out logic_uScriptAct_SetBool_Target_406);
		local_PlayedEarlyMessage_System_Boolean = logic_uScriptAct_SetBool_Target_406;
	}

	private void Relay_True_408()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_408.True(out logic_uScriptAct_SetBool_Target_408);
		local_PlayedEarlyMessage_System_Boolean = logic_uScriptAct_SetBool_Target_408;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_408.Out)
		{
			Relay_In_400();
		}
	}

	private void Relay_False_408()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_408.False(out logic_uScriptAct_SetBool_Target_408);
		local_PlayedEarlyMessage_System_Boolean = logic_uScriptAct_SetBool_Target_408;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_408.Out)
		{
			Relay_In_400();
		}
	}

	private void Relay_In_410()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_410 = EarlyTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_410.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_410, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_410);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_410.Out)
		{
			Relay_In_518();
		}
	}

	private void Relay_InitialSpawn_419()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_419.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_419, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_419, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_419 = owner_Connection_417;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_419.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_419, logic_uScript_SpawnTechsFromData_ownerNode_419, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_419, logic_uScript_SpawnTechsFromData_allowResurrection_419);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_419.Out)
		{
			Relay_InitialSpawn_420();
		}
	}

	private void Relay_InitialSpawn_420()
	{
		int num = 0;
		Array r1MainTechData = R1MainTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_420.Length != num + r1MainTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_420, num + r1MainTechData.Length);
		}
		Array.Copy(r1MainTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_420, num, r1MainTechData.Length);
		num += r1MainTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_420 = owner_Connection_414;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_420.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_420, logic_uScript_SpawnTechsFromData_ownerNode_420, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_420, logic_uScript_SpawnTechsFromData_allowResurrection_420);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_420.Out)
		{
			Relay_InitialSpawn_421();
		}
	}

	private void Relay_InitialSpawn_421()
	{
		int num = 0;
		Array turretTechData = TurretTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_421.Length != num + turretTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_421, num + turretTechData.Length);
		}
		Array.Copy(turretTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_421, num, turretTechData.Length);
		num += turretTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_421 = owner_Connection_415;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_421.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_421, logic_uScript_SpawnTechsFromData_ownerNode_421, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_421, logic_uScript_SpawnTechsFromData_allowResurrection_421);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_421.Out)
		{
			Relay_InitialSpawn_422();
		}
	}

	private void Relay_InitialSpawn_422()
	{
		int num = 0;
		Array guardianTechData = GuardianTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_422.Length != num + guardianTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_422, num + guardianTechData.Length);
		}
		Array.Copy(guardianTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_422, num, guardianTechData.Length);
		num += guardianTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_422 = owner_Connection_418;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_422.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_422, logic_uScript_SpawnTechsFromData_ownerNode_422, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_422, logic_uScript_SpawnTechsFromData_allowResurrection_422);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_422.Out)
		{
			Relay_In_342();
		}
	}

	private void Relay_In_425()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_425.In(logic_uScriptAct_SetInt_Value_425, out logic_uScriptAct_SetInt_Target_425);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_425;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_425.Out)
		{
			Relay_In_332();
		}
	}

	private void Relay_In_426()
	{
		int num = 0;
		Array turretTechData = TurretTechData;
		if (logic_uScript_DestroyTechsFromData_techData_426.Length != num + turretTechData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_426, num + turretTechData.Length);
		}
		Array.Copy(turretTechData, 0, logic_uScript_DestroyTechsFromData_techData_426, num, turretTechData.Length);
		num += turretTechData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_426 = owner_Connection_428;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_426.In(logic_uScript_DestroyTechsFromData_techData_426, logic_uScript_DestroyTechsFromData_shouldExplode_426, logic_uScript_DestroyTechsFromData_ownerNode_426);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_426.Out)
		{
			Relay_In_431();
		}
	}

	private void Relay_In_431()
	{
		int num = 0;
		if (logic_uScript_DestroyTechsFromData_techData_431.Length <= num)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_431, num + 1);
		}
		logic_uScript_DestroyTechsFromData_techData_431[num++] = R2Child01TechData;
		logic_uScript_DestroyTechsFromData_ownerNode_431 = owner_Connection_437;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_431.In(logic_uScript_DestroyTechsFromData_techData_431, logic_uScript_DestroyTechsFromData_shouldExplode_431, logic_uScript_DestroyTechsFromData_ownerNode_431);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_431.Out)
		{
			Relay_In_434();
		}
	}

	private void Relay_In_434()
	{
		int num = 0;
		if (logic_uScript_DestroyTechsFromData_techData_434.Length <= num)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_434, num + 1);
		}
		logic_uScript_DestroyTechsFromData_techData_434[num++] = R2Child02TechData;
		logic_uScript_DestroyTechsFromData_ownerNode_434 = owner_Connection_432;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_434.In(logic_uScript_DestroyTechsFromData_techData_434, logic_uScript_DestroyTechsFromData_shouldExplode_434, logic_uScript_DestroyTechsFromData_ownerNode_434);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_434.Out)
		{
			Relay_In_435();
		}
	}

	private void Relay_In_435()
	{
		int num = 0;
		if (logic_uScript_DestroyTechsFromData_techData_435.Length <= num)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_435, num + 1);
		}
		logic_uScript_DestroyTechsFromData_techData_435[num++] = R2Child03TechData;
		logic_uScript_DestroyTechsFromData_ownerNode_435 = owner_Connection_433;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_435.In(logic_uScript_DestroyTechsFromData_techData_435, logic_uScript_DestroyTechsFromData_shouldExplode_435, logic_uScript_DestroyTechsFromData_ownerNode_435);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_435.Out)
		{
			Relay_InitialSpawn_494();
		}
	}

	private void Relay_In_438()
	{
		logic_uScriptCon_CompareInt_B_438 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_438.In(logic_uScriptCon_CompareInt_A_438, logic_uScriptCon_CompareInt_B_438);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_438.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_438.NotEqualTo;
		if (equalTo)
		{
			Relay_In_440();
		}
		if (notEqualTo)
		{
			Relay_In_442();
		}
	}

	private void Relay_In_440()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_440 = ArenaTriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_440 = ArenaTriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_440.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_440, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_440, ref logic_uScript_IsPlayerInTriggerSmart_inside_440);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_440.Out;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_440.AllInside;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_440.AllOutside;
		bool someOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_440.SomeOutside;
		if (num)
		{
			Relay_In_85();
		}
		if (allInside)
		{
			Relay_True_444();
		}
		if (allOutside)
		{
			Relay_False_444();
		}
		if (someOutside)
		{
			Relay_False_444();
		}
	}

	private void Relay_In_442()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_442.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_442.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_True_444()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_444.True(out logic_uScriptAct_SetBool_Target_444);
		local_isAllInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_444;
	}

	private void Relay_False_444()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_444.False(out logic_uScriptAct_SetBool_Target_444);
		local_isAllInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_444;
	}

	private void Relay_Save_Out_447()
	{
		Relay_Save_457();
	}

	private void Relay_Load_Out_447()
	{
		Relay_Load_457();
	}

	private void Relay_Restart_Out_447()
	{
		Relay_Set_False_457();
	}

	private void Relay_Save_447()
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_447 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Save(ref logic_SubGraph_SaveLoadBool_boolean_447, logic_SubGraph_SaveLoadBool_boolAsVariable_447, logic_SubGraph_SaveLoadBool_uniqueID_447);
	}

	private void Relay_Load_447()
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_447 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Load(ref logic_SubGraph_SaveLoadBool_boolean_447, logic_SubGraph_SaveLoadBool_boolAsVariable_447, logic_SubGraph_SaveLoadBool_uniqueID_447);
	}

	private void Relay_Set_True_447()
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_447 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_447, logic_SubGraph_SaveLoadBool_boolAsVariable_447, logic_SubGraph_SaveLoadBool_uniqueID_447);
	}

	private void Relay_Set_False_447()
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_447 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_447, logic_SubGraph_SaveLoadBool_boolAsVariable_447, logic_SubGraph_SaveLoadBool_uniqueID_447);
	}

	private void Relay_Save_Out_457()
	{
		Relay_Save_458();
	}

	private void Relay_Load_Out_457()
	{
		Relay_Load_458();
	}

	private void Relay_Restart_Out_457()
	{
		Relay_Set_False_458();
	}

	private void Relay_Save_457()
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = local_RoundActive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_457 = local_RoundActive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Save(ref logic_SubGraph_SaveLoadBool_boolean_457, logic_SubGraph_SaveLoadBool_boolAsVariable_457, logic_SubGraph_SaveLoadBool_uniqueID_457);
	}

	private void Relay_Load_457()
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = local_RoundActive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_457 = local_RoundActive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Load(ref logic_SubGraph_SaveLoadBool_boolean_457, logic_SubGraph_SaveLoadBool_boolAsVariable_457, logic_SubGraph_SaveLoadBool_uniqueID_457);
	}

	private void Relay_Set_True_457()
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = local_RoundActive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_457 = local_RoundActive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_457, logic_SubGraph_SaveLoadBool_boolAsVariable_457, logic_SubGraph_SaveLoadBool_uniqueID_457);
	}

	private void Relay_Set_False_457()
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = local_RoundActive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_457 = local_RoundActive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_457, logic_SubGraph_SaveLoadBool_boolAsVariable_457, logic_SubGraph_SaveLoadBool_uniqueID_457);
	}

	private void Relay_Save_Out_458()
	{
		Relay_Save_459();
	}

	private void Relay_Load_Out_458()
	{
		Relay_Load_459();
	}

	private void Relay_Restart_Out_458()
	{
		Relay_Set_False_459();
	}

	private void Relay_Save_458()
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = local_PlayedEarlyMessage_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_458 = local_PlayedEarlyMessage_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Save(ref logic_SubGraph_SaveLoadBool_boolean_458, logic_SubGraph_SaveLoadBool_boolAsVariable_458, logic_SubGraph_SaveLoadBool_uniqueID_458);
	}

	private void Relay_Load_458()
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = local_PlayedEarlyMessage_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_458 = local_PlayedEarlyMessage_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Load(ref logic_SubGraph_SaveLoadBool_boolean_458, logic_SubGraph_SaveLoadBool_boolAsVariable_458, logic_SubGraph_SaveLoadBool_uniqueID_458);
	}

	private void Relay_Set_True_458()
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = local_PlayedEarlyMessage_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_458 = local_PlayedEarlyMessage_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_458, logic_SubGraph_SaveLoadBool_boolAsVariable_458, logic_SubGraph_SaveLoadBool_uniqueID_458);
	}

	private void Relay_Set_False_458()
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = local_PlayedEarlyMessage_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_458 = local_PlayedEarlyMessage_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_458, logic_SubGraph_SaveLoadBool_boolAsVariable_458, logic_SubGraph_SaveLoadBool_uniqueID_458);
	}

	private void Relay_Save_Out_459()
	{
		Relay_Save_460();
	}

	private void Relay_Load_Out_459()
	{
		Relay_Load_460();
	}

	private void Relay_Restart_Out_459()
	{
		Relay_Set_False_460();
	}

	private void Relay_Save_459()
	{
		logic_SubGraph_SaveLoadBool_boolean_459 = local_VisitedNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_459 = local_VisitedNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Save(ref logic_SubGraph_SaveLoadBool_boolean_459, logic_SubGraph_SaveLoadBool_boolAsVariable_459, logic_SubGraph_SaveLoadBool_uniqueID_459);
	}

	private void Relay_Load_459()
	{
		logic_SubGraph_SaveLoadBool_boolean_459 = local_VisitedNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_459 = local_VisitedNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Load(ref logic_SubGraph_SaveLoadBool_boolean_459, logic_SubGraph_SaveLoadBool_boolAsVariable_459, logic_SubGraph_SaveLoadBool_uniqueID_459);
	}

	private void Relay_Set_True_459()
	{
		logic_SubGraph_SaveLoadBool_boolean_459 = local_VisitedNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_459 = local_VisitedNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_459, logic_SubGraph_SaveLoadBool_boolAsVariable_459, logic_SubGraph_SaveLoadBool_uniqueID_459);
	}

	private void Relay_Set_False_459()
	{
		logic_SubGraph_SaveLoadBool_boolean_459 = local_VisitedNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_459 = local_VisitedNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_459.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_459, logic_SubGraph_SaveLoadBool_boolAsVariable_459, logic_SubGraph_SaveLoadBool_uniqueID_459);
	}

	private void Relay_Save_Out_460()
	{
		Relay_Save_461();
	}

	private void Relay_Load_Out_460()
	{
		Relay_Load_461();
	}

	private void Relay_Restart_Out_460()
	{
		Relay_Set_False_461();
	}

	private void Relay_Save_460()
	{
		logic_SubGraph_SaveLoadBool_boolean_460 = local_PlayedReadyGoDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_460 = local_PlayedReadyGoDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Save(ref logic_SubGraph_SaveLoadBool_boolean_460, logic_SubGraph_SaveLoadBool_boolAsVariable_460, logic_SubGraph_SaveLoadBool_uniqueID_460);
	}

	private void Relay_Load_460()
	{
		logic_SubGraph_SaveLoadBool_boolean_460 = local_PlayedReadyGoDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_460 = local_PlayedReadyGoDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Load(ref logic_SubGraph_SaveLoadBool_boolean_460, logic_SubGraph_SaveLoadBool_boolAsVariable_460, logic_SubGraph_SaveLoadBool_uniqueID_460);
	}

	private void Relay_Set_True_460()
	{
		logic_SubGraph_SaveLoadBool_boolean_460 = local_PlayedReadyGoDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_460 = local_PlayedReadyGoDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_460, logic_SubGraph_SaveLoadBool_boolAsVariable_460, logic_SubGraph_SaveLoadBool_uniqueID_460);
	}

	private void Relay_Set_False_460()
	{
		logic_SubGraph_SaveLoadBool_boolean_460 = local_PlayedReadyGoDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_460 = local_PlayedReadyGoDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_460.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_460, logic_SubGraph_SaveLoadBool_boolAsVariable_460, logic_SubGraph_SaveLoadBool_uniqueID_460);
	}

	private void Relay_Save_Out_461()
	{
		Relay_Save_479();
	}

	private void Relay_Load_Out_461()
	{
		Relay_Load_479();
	}

	private void Relay_Restart_Out_461()
	{
		Relay_Set_False_479();
	}

	private void Relay_Save_461()
	{
		logic_SubGraph_SaveLoadBool_boolean_461 = local_SetTeamsToEnemy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_461 = local_SetTeamsToEnemy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Save(ref logic_SubGraph_SaveLoadBool_boolean_461, logic_SubGraph_SaveLoadBool_boolAsVariable_461, logic_SubGraph_SaveLoadBool_uniqueID_461);
	}

	private void Relay_Load_461()
	{
		logic_SubGraph_SaveLoadBool_boolean_461 = local_SetTeamsToEnemy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_461 = local_SetTeamsToEnemy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Load(ref logic_SubGraph_SaveLoadBool_boolean_461, logic_SubGraph_SaveLoadBool_boolAsVariable_461, logic_SubGraph_SaveLoadBool_uniqueID_461);
	}

	private void Relay_Set_True_461()
	{
		logic_SubGraph_SaveLoadBool_boolean_461 = local_SetTeamsToEnemy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_461 = local_SetTeamsToEnemy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_461, logic_SubGraph_SaveLoadBool_boolAsVariable_461, logic_SubGraph_SaveLoadBool_uniqueID_461);
	}

	private void Relay_Set_False_461()
	{
		logic_SubGraph_SaveLoadBool_boolean_461 = local_SetTeamsToEnemy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_461 = local_SetTeamsToEnemy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_461.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_461, logic_SubGraph_SaveLoadBool_boolAsVariable_461, logic_SubGraph_SaveLoadBool_uniqueID_461);
	}

	private void Relay_Save_Out_462()
	{
		Relay_Save_463();
	}

	private void Relay_Load_Out_462()
	{
		Relay_Load_463();
	}

	private void Relay_Restart_Out_462()
	{
		Relay_Restart_463();
	}

	private void Relay_Save_462()
	{
		logic_SubGraph_SaveLoadInt_integer_462 = local_TurretsAlive_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_462 = local_TurretsAlive_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.Save(logic_SubGraph_SaveLoadInt_restartValue_462, ref logic_SubGraph_SaveLoadInt_integer_462, logic_SubGraph_SaveLoadInt_intAsVariable_462, logic_SubGraph_SaveLoadInt_uniqueID_462);
	}

	private void Relay_Load_462()
	{
		logic_SubGraph_SaveLoadInt_integer_462 = local_TurretsAlive_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_462 = local_TurretsAlive_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.Load(logic_SubGraph_SaveLoadInt_restartValue_462, ref logic_SubGraph_SaveLoadInt_integer_462, logic_SubGraph_SaveLoadInt_intAsVariable_462, logic_SubGraph_SaveLoadInt_uniqueID_462);
	}

	private void Relay_Restart_462()
	{
		logic_SubGraph_SaveLoadInt_integer_462 = local_TurretsAlive_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_462 = local_TurretsAlive_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_462.Restart(logic_SubGraph_SaveLoadInt_restartValue_462, ref logic_SubGraph_SaveLoadInt_integer_462, logic_SubGraph_SaveLoadInt_intAsVariable_462, logic_SubGraph_SaveLoadInt_uniqueID_462);
	}

	private void Relay_Save_Out_463()
	{
		Relay_Save_464();
	}

	private void Relay_Load_Out_463()
	{
		Relay_Load_464();
	}

	private void Relay_Restart_Out_463()
	{
		Relay_Restart_464();
	}

	private void Relay_Save_463()
	{
		logic_SubGraph_SaveLoadInt_integer_463 = local_ChargersAlive_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_463 = local_ChargersAlive_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.Save(logic_SubGraph_SaveLoadInt_restartValue_463, ref logic_SubGraph_SaveLoadInt_integer_463, logic_SubGraph_SaveLoadInt_intAsVariable_463, logic_SubGraph_SaveLoadInt_uniqueID_463);
	}

	private void Relay_Load_463()
	{
		logic_SubGraph_SaveLoadInt_integer_463 = local_ChargersAlive_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_463 = local_ChargersAlive_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.Load(logic_SubGraph_SaveLoadInt_restartValue_463, ref logic_SubGraph_SaveLoadInt_integer_463, logic_SubGraph_SaveLoadInt_intAsVariable_463, logic_SubGraph_SaveLoadInt_uniqueID_463);
	}

	private void Relay_Restart_463()
	{
		logic_SubGraph_SaveLoadInt_integer_463 = local_ChargersAlive_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_463 = local_ChargersAlive_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_463.Restart(logic_SubGraph_SaveLoadInt_restartValue_463, ref logic_SubGraph_SaveLoadInt_integer_463, logic_SubGraph_SaveLoadInt_intAsVariable_463, logic_SubGraph_SaveLoadInt_uniqueID_463);
	}

	private void Relay_Save_Out_464()
	{
		Relay_Save_465();
	}

	private void Relay_Load_Out_464()
	{
		Relay_Load_465();
	}

	private void Relay_Restart_Out_464()
	{
		Relay_Restart_465();
	}

	private void Relay_Save_464()
	{
		logic_SubGraph_SaveLoadInt_integer_464 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_464 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.Save(logic_SubGraph_SaveLoadInt_restartValue_464, ref logic_SubGraph_SaveLoadInt_integer_464, logic_SubGraph_SaveLoadInt_intAsVariable_464, logic_SubGraph_SaveLoadInt_uniqueID_464);
	}

	private void Relay_Load_464()
	{
		logic_SubGraph_SaveLoadInt_integer_464 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_464 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.Load(logic_SubGraph_SaveLoadInt_restartValue_464, ref logic_SubGraph_SaveLoadInt_integer_464, logic_SubGraph_SaveLoadInt_intAsVariable_464, logic_SubGraph_SaveLoadInt_uniqueID_464);
	}

	private void Relay_Restart_464()
	{
		logic_SubGraph_SaveLoadInt_integer_464 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_464 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_464.Restart(logic_SubGraph_SaveLoadInt_restartValue_464, ref logic_SubGraph_SaveLoadInt_integer_464, logic_SubGraph_SaveLoadInt_intAsVariable_464, logic_SubGraph_SaveLoadInt_uniqueID_464);
	}

	private void Relay_Save_Out_465()
	{
		Relay_Save_28();
	}

	private void Relay_Load_Out_465()
	{
		Relay_Load_28();
	}

	private void Relay_Restart_Out_465()
	{
		Relay_Set_False_28();
	}

	private void Relay_Save_465()
	{
		logic_SubGraph_SaveLoadInt_integer_465 = local_DialogueProgressExtra_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_465 = local_DialogueProgressExtra_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.Save(logic_SubGraph_SaveLoadInt_restartValue_465, ref logic_SubGraph_SaveLoadInt_integer_465, logic_SubGraph_SaveLoadInt_intAsVariable_465, logic_SubGraph_SaveLoadInt_uniqueID_465);
	}

	private void Relay_Load_465()
	{
		logic_SubGraph_SaveLoadInt_integer_465 = local_DialogueProgressExtra_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_465 = local_DialogueProgressExtra_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.Load(logic_SubGraph_SaveLoadInt_restartValue_465, ref logic_SubGraph_SaveLoadInt_integer_465, logic_SubGraph_SaveLoadInt_intAsVariable_465, logic_SubGraph_SaveLoadInt_uniqueID_465);
	}

	private void Relay_Restart_465()
	{
		logic_SubGraph_SaveLoadInt_integer_465 = local_DialogueProgressExtra_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_465 = local_DialogueProgressExtra_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_465.Restart(logic_SubGraph_SaveLoadInt_restartValue_465, ref logic_SubGraph_SaveLoadInt_integer_465, logic_SubGraph_SaveLoadInt_intAsVariable_465, logic_SubGraph_SaveLoadInt_uniqueID_465);
	}

	private void Relay_In_467()
	{
		logic_uScript_PlayDialogue_dialogue_467 = R1DeadDialogue;
		logic_uScript_PlayDialogue_progress_467 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_467.In(logic_uScript_PlayDialogue_dialogue_467, ref logic_uScript_PlayDialogue_progress_467);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_467;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_467.Shown)
		{
			Relay_In_514();
		}
	}

	private void Relay_In_469()
	{
		logic_uScript_PlayDialogue_dialogue_469 = ShieldsDownDialogue;
		logic_uScript_PlayDialogue_progress_469 = local_DialogueProgressExtra_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_469.In(logic_uScript_PlayDialogue_dialogue_469, ref logic_uScript_PlayDialogue_progress_469);
		local_DialogueProgressExtra_System_Int32 = logic_uScript_PlayDialogue_progress_469;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_469.Out)
		{
			Relay_In_527();
		}
	}

	private void Relay_In_472()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_472.In(logic_uScriptAct_SetInt_Value_472, out logic_uScriptAct_SetInt_Target_472);
		local_DialogueProgressExtra_System_Int32 = logic_uScriptAct_SetInt_Target_472;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_472.Out)
		{
			Relay_True_477();
		}
	}

	private void Relay_In_474()
	{
		logic_uScriptCon_CompareBool_Bool_474 = local_PlayedShieldsDownDialogue_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_474.In(logic_uScriptCon_CompareBool_Bool_474);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_474.False)
		{
			Relay_In_469();
		}
	}

	private void Relay_In_476()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_476.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_476.Out)
		{
			Relay_In_474();
		}
	}

	private void Relay_True_477()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_477.True(out logic_uScriptAct_SetBool_Target_477);
		local_PlayedShieldsDownDialogue_System_Boolean = logic_uScriptAct_SetBool_Target_477;
	}

	private void Relay_False_477()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_477.False(out logic_uScriptAct_SetBool_Target_477);
		local_PlayedShieldsDownDialogue_System_Boolean = logic_uScriptAct_SetBool_Target_477;
	}

	private void Relay_Save_Out_479()
	{
		Relay_Save_530();
	}

	private void Relay_Load_Out_479()
	{
		Relay_Load_530();
	}

	private void Relay_Restart_Out_479()
	{
		Relay_Set_False_530();
	}

	private void Relay_Save_479()
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = local_PlayedShieldsDownDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_479 = local_PlayedShieldsDownDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Save(ref logic_SubGraph_SaveLoadBool_boolean_479, logic_SubGraph_SaveLoadBool_boolAsVariable_479, logic_SubGraph_SaveLoadBool_uniqueID_479);
	}

	private void Relay_Load_479()
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = local_PlayedShieldsDownDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_479 = local_PlayedShieldsDownDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Load(ref logic_SubGraph_SaveLoadBool_boolean_479, logic_SubGraph_SaveLoadBool_boolAsVariable_479, logic_SubGraph_SaveLoadBool_uniqueID_479);
	}

	private void Relay_Set_True_479()
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = local_PlayedShieldsDownDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_479 = local_PlayedShieldsDownDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_479, logic_SubGraph_SaveLoadBool_boolAsVariable_479, logic_SubGraph_SaveLoadBool_uniqueID_479);
	}

	private void Relay_Set_False_479()
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = local_PlayedShieldsDownDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_479 = local_PlayedShieldsDownDialogue_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_479, logic_SubGraph_SaveLoadBool_boolAsVariable_479, logic_SubGraph_SaveLoadBool_uniqueID_479);
	}

	private void Relay_Save_Out_481()
	{
		Relay_Save_462();
	}

	private void Relay_Load_Out_481()
	{
		Relay_Load_462();
	}

	private void Relay_Restart_Out_481()
	{
		Relay_Restart_462();
	}

	private void Relay_Save_481()
	{
		logic_SubGraph_SaveLoadInt_integer_481 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_481 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.Save(logic_SubGraph_SaveLoadInt_restartValue_481, ref logic_SubGraph_SaveLoadInt_integer_481, logic_SubGraph_SaveLoadInt_intAsVariable_481, logic_SubGraph_SaveLoadInt_uniqueID_481);
	}

	private void Relay_Load_481()
	{
		logic_SubGraph_SaveLoadInt_integer_481 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_481 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.Load(logic_SubGraph_SaveLoadInt_restartValue_481, ref logic_SubGraph_SaveLoadInt_integer_481, logic_SubGraph_SaveLoadInt_intAsVariable_481, logic_SubGraph_SaveLoadInt_uniqueID_481);
	}

	private void Relay_Restart_481()
	{
		logic_SubGraph_SaveLoadInt_integer_481 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_481 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_481.Restart(logic_SubGraph_SaveLoadInt_restartValue_481, ref logic_SubGraph_SaveLoadInt_integer_481, logic_SubGraph_SaveLoadInt_intAsVariable_481, logic_SubGraph_SaveLoadInt_uniqueID_481);
	}

	private void Relay_In_482()
	{
		logic_uScript_PlayDialogue_dialogue_482 = VictoryDialogue;
		logic_uScript_PlayDialogue_progress_482 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_482.In(logic_uScript_PlayDialogue_dialogue_482, ref logic_uScript_PlayDialogue_progress_482);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_482;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_482.Shown)
		{
			Relay_In_500();
		}
	}

	private void Relay_In_485()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_485.In(logic_uScriptAct_SetInt_Value_485, out logic_uScriptAct_SetInt_Target_485);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_485;
	}

	private void Relay_In_487()
	{
		logic_uScriptCon_CompareBool_Bool_487 = local_isAllInsideNPCTrigger_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_487.In(logic_uScriptCon_CompareBool_Bool_487);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_487.True)
		{
			Relay_True_492();
		}
	}

	private void Relay_In_488()
	{
		logic_uScriptCon_CompareBool_Bool_488 = local_VisitedNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_488.In(logic_uScriptCon_CompareBool_Bool_488);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_488.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_488.False;
		if (num)
		{
			Relay_In_482();
		}
		if (flag)
		{
			Relay_In_501();
		}
	}

	private void Relay_True_489()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_489.True(out logic_uScriptAct_SetBool_Target_489);
		local_VisitedNPC_System_Boolean = logic_uScriptAct_SetBool_Target_489;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_489.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_False_489()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_489.False(out logic_uScriptAct_SetBool_Target_489);
		local_VisitedNPC_System_Boolean = logic_uScriptAct_SetBool_Target_489;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_489.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_True_492()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_492.True(out logic_uScriptAct_SetBool_Target_492);
		local_VisitedNPC_System_Boolean = logic_uScriptAct_SetBool_Target_492;
	}

	private void Relay_False_492()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_492.False(out logic_uScriptAct_SetBool_Target_492);
		local_VisitedNPC_System_Boolean = logic_uScriptAct_SetBool_Target_492;
	}

	private void Relay_InitialSpawn_494()
	{
		int num = 0;
		Array nPC2TechData = NPC2TechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_494.Length != num + nPC2TechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_494, num + nPC2TechData.Length);
		}
		Array.Copy(nPC2TechData, 0, logic_uScript_SpawnTechsFromData_spawnData_494, num, nPC2TechData.Length);
		num += nPC2TechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_494 = owner_Connection_496;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_494.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_494, logic_uScript_SpawnTechsFromData_ownerNode_494, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_494, logic_uScript_SpawnTechsFromData_allowResurrection_494);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_494.Out)
		{
			Relay_False_489();
		}
	}

	private void Relay_In_500()
	{
		logic_uScript_FlyTechUpAndAway_tech_500 = local_NPC2Tech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_500 = FlyAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_500 = FlyParticle;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_500.In(logic_uScript_FlyTechUpAndAway_tech_500, logic_uScript_FlyTechUpAndAway_maxLifetime_500, logic_uScript_FlyTechUpAndAway_targetHeight_500, logic_uScript_FlyTechUpAndAway_aiTree_500, logic_uScript_FlyTechUpAndAway_removalParticles_500);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_500.Out)
		{
			Relay_UnPause_10();
		}
	}

	private void Relay_In_501()
	{
		logic_uScript_SetEncounterTarget_owner_501 = owner_Connection_503;
		logic_uScript_SetEncounterTarget_visibleObject_501 = local_NPC2Tech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_501.In(logic_uScript_SetEncounterTarget_owner_501, logic_uScript_SetEncounterTarget_visibleObject_501);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_501.Out)
		{
			Relay_In_487();
		}
	}

	private void Relay_In_505()
	{
		int num = 0;
		Array nPC2TechData = NPC2TechData;
		if (logic_uScript_GetAndCheckTechs_techData_505.Length != num + nPC2TechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_505, num + nPC2TechData.Length);
		}
		Array.Copy(nPC2TechData, 0, logic_uScript_GetAndCheckTechs_techData_505, num, nPC2TechData.Length);
		num += nPC2TechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_505 = owner_Connection_506;
		int num2 = 0;
		Array array = local_NPC2Techs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_505.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_505, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_505, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_505 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_505.In(logic_uScript_GetAndCheckTechs_techData_505, logic_uScript_GetAndCheckTechs_ownerNode_505, ref logic_uScript_GetAndCheckTechs_techs_505);
		local_NPC2Techs_TankArray = logic_uScript_GetAndCheckTechs_techs_505;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_505.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_505.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_505.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_505.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_508();
		}
		if (someAlive)
		{
			Relay_AtIndex_508();
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

	private void Relay_AtIndex_508()
	{
		int num = 0;
		Array array = local_NPC2Techs_TankArray;
		if (logic_uScript_AccessListTech_techList_508.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_508, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_508, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_508.AtIndex(ref logic_uScript_AccessListTech_techList_508, logic_uScript_AccessListTech_index_508, out logic_uScript_AccessListTech_value_508);
		local_NPC2Techs_TankArray = logic_uScript_AccessListTech_techList_508;
		local_NPC2Tech_Tank = logic_uScript_AccessListTech_value_508;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_508.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_511()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_511.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_511.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_514()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_514.In(logic_uScriptAct_SetInt_Value_514, out logic_uScriptAct_SetInt_Target_514);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_514;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_514.Out)
		{
			Relay_In_515();
		}
	}

	private void Relay_In_515()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_515.In(logic_uScriptAct_SetInt_Value_515, out logic_uScriptAct_SetInt_Target_515);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_515;
	}

	private void Relay_In_517()
	{
		logic_uScriptCon_CheckIntEquals_B_517 = local_Stage_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_517.In(logic_uScriptCon_CheckIntEquals_A_517, logic_uScriptCon_CheckIntEquals_B_517);
		if (logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_517.True)
		{
			Relay_In_404();
		}
	}

	private void Relay_In_518()
	{
		int num = 0;
		Array msgTooEarly = MsgTooEarly;
		if (logic_uScript_AddOnScreenMessage_locString_518.Length != num + msgTooEarly.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_518, num + msgTooEarly.Length);
		}
		Array.Copy(msgTooEarly, 0, logic_uScript_AddOnScreenMessage_locString_518, num, msgTooEarly.Length);
		num += msgTooEarly.Length;
		logic_uScript_AddOnScreenMessage_tag_518 = MsgTooEarlyTag;
		logic_uScript_AddOnScreenMessage_speaker_518 = TechSpeaker;
		logic_uScript_AddOnScreenMessage_Return_518 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_518.In(logic_uScript_AddOnScreenMessage_locString_518, logic_uScript_AddOnScreenMessage_msgPriority_518, logic_uScript_AddOnScreenMessage_holdMsg_518, logic_uScript_AddOnScreenMessage_tag_518, logic_uScript_AddOnScreenMessage_speaker_518, logic_uScript_AddOnScreenMessage_side_518);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_518.Out)
		{
			Relay_False_402();
		}
	}

	private void Relay_In_524()
	{
		int num = 0;
		Array msgOutOfArena = MsgOutOfArena;
		if (logic_uScript_AddOnScreenMessage_locString_524.Length != num + msgOutOfArena.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_524, num + msgOutOfArena.Length);
		}
		Array.Copy(msgOutOfArena, 0, logic_uScript_AddOnScreenMessage_locString_524, num, msgOutOfArena.Length);
		num += msgOutOfArena.Length;
		logic_uScript_AddOnScreenMessage_tag_524 = MsgOutOfArenaTag;
		logic_uScript_AddOnScreenMessage_speaker_524 = TechSpeaker;
		logic_uScript_AddOnScreenMessage_Return_524 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_524.In(logic_uScript_AddOnScreenMessage_locString_524, logic_uScript_AddOnScreenMessage_msgPriority_524, logic_uScript_AddOnScreenMessage_holdMsg_524, logic_uScript_AddOnScreenMessage_tag_524, logic_uScript_AddOnScreenMessage_speaker_524, logic_uScript_AddOnScreenMessage_side_524);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_524.Out)
		{
			Relay_In_425();
		}
	}

	private void Relay_Out_527()
	{
		Relay_In_472();
	}

	private void Relay_In_527()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_527 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_527.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_527, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_527);
	}

	private void Relay_Save_Out_530()
	{
		Relay_Save_531();
	}

	private void Relay_Load_Out_530()
	{
		Relay_Load_531();
	}

	private void Relay_Restart_Out_530()
	{
		Relay_Set_False_531();
	}

	private void Relay_Save_530()
	{
		logic_SubGraph_SaveLoadBool_boolean_530 = local_isAllInsideNPCTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_530 = local_isAllInsideNPCTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Save(ref logic_SubGraph_SaveLoadBool_boolean_530, logic_SubGraph_SaveLoadBool_boolAsVariable_530, logic_SubGraph_SaveLoadBool_uniqueID_530);
	}

	private void Relay_Load_530()
	{
		logic_SubGraph_SaveLoadBool_boolean_530 = local_isAllInsideNPCTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_530 = local_isAllInsideNPCTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Load(ref logic_SubGraph_SaveLoadBool_boolean_530, logic_SubGraph_SaveLoadBool_boolAsVariable_530, logic_SubGraph_SaveLoadBool_uniqueID_530);
	}

	private void Relay_Set_True_530()
	{
		logic_SubGraph_SaveLoadBool_boolean_530 = local_isAllInsideNPCTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_530 = local_isAllInsideNPCTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_530, logic_SubGraph_SaveLoadBool_boolAsVariable_530, logic_SubGraph_SaveLoadBool_uniqueID_530);
	}

	private void Relay_Set_False_530()
	{
		logic_SubGraph_SaveLoadBool_boolean_530 = local_isAllInsideNPCTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_530 = local_isAllInsideNPCTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_530.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_530, logic_SubGraph_SaveLoadBool_boolAsVariable_530, logic_SubGraph_SaveLoadBool_uniqueID_530);
	}

	private void Relay_Save_Out_531()
	{
		Relay_Save_541();
	}

	private void Relay_Load_Out_531()
	{
		Relay_Load_541();
	}

	private void Relay_Restart_Out_531()
	{
		Relay_Set_False_541();
	}

	private void Relay_Save_531()
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = local_isAllInsideArenaArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_531 = local_isAllInsideArenaArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Save(ref logic_SubGraph_SaveLoadBool_boolean_531, logic_SubGraph_SaveLoadBool_boolAsVariable_531, logic_SubGraph_SaveLoadBool_uniqueID_531);
	}

	private void Relay_Load_531()
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = local_isAllInsideArenaArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_531 = local_isAllInsideArenaArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Load(ref logic_SubGraph_SaveLoadBool_boolean_531, logic_SubGraph_SaveLoadBool_boolAsVariable_531, logic_SubGraph_SaveLoadBool_uniqueID_531);
	}

	private void Relay_Set_True_531()
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = local_isAllInsideArenaArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_531 = local_isAllInsideArenaArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_531, logic_SubGraph_SaveLoadBool_boolAsVariable_531, logic_SubGraph_SaveLoadBool_uniqueID_531);
	}

	private void Relay_Set_False_531()
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = local_isAllInsideArenaArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_531 = local_isAllInsideArenaArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_531, logic_SubGraph_SaveLoadBool_boolAsVariable_531, logic_SubGraph_SaveLoadBool_uniqueID_531);
	}

	private void Relay_In_533()
	{
		logic_uScriptCon_CompareBool_Bool_533 = local_PlayerDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_533.In(logic_uScriptCon_CompareBool_Bool_533);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_533.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_533.False;
		if (num)
		{
			Relay_In_535();
		}
		if (flag)
		{
			Relay_In_524();
		}
	}

	private void Relay_In_535()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_535.In(logic_uScriptAct_SetInt_Value_535, out logic_uScriptAct_SetInt_Target_535);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_535;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_535.Out)
		{
			Relay_In_538();
		}
	}

	private void Relay_True_537()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_537.True(out logic_uScriptAct_SetBool_Target_537);
		local_SetTeamsToEnemy_System_Boolean = logic_uScriptAct_SetBool_Target_537;
	}

	private void Relay_False_537()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_537.False(out logic_uScriptAct_SetBool_Target_537);
		local_SetTeamsToEnemy_System_Boolean = logic_uScriptAct_SetBool_Target_537;
	}

	private void Relay_In_538()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_538.In(logic_uScriptAct_SetInt_Value_538, out logic_uScriptAct_SetInt_Target_538);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_538;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_538.Out)
		{
			Relay_False_537();
		}
	}

	private void Relay_Save_Out_541()
	{
		Relay_Save_644();
	}

	private void Relay_Load_Out_541()
	{
		Relay_Load_644();
	}

	private void Relay_Restart_Out_541()
	{
		Relay_Set_False_644();
	}

	private void Relay_Save_541()
	{
		logic_SubGraph_SaveLoadBool_boolean_541 = local_PlayerDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_541 = local_PlayerDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Save(ref logic_SubGraph_SaveLoadBool_boolean_541, logic_SubGraph_SaveLoadBool_boolAsVariable_541, logic_SubGraph_SaveLoadBool_uniqueID_541);
	}

	private void Relay_Load_541()
	{
		logic_SubGraph_SaveLoadBool_boolean_541 = local_PlayerDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_541 = local_PlayerDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Load(ref logic_SubGraph_SaveLoadBool_boolean_541, logic_SubGraph_SaveLoadBool_boolAsVariable_541, logic_SubGraph_SaveLoadBool_uniqueID_541);
	}

	private void Relay_Set_True_541()
	{
		logic_SubGraph_SaveLoadBool_boolean_541 = local_PlayerDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_541 = local_PlayerDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_541, logic_SubGraph_SaveLoadBool_boolAsVariable_541, logic_SubGraph_SaveLoadBool_uniqueID_541);
	}

	private void Relay_Set_False_541()
	{
		logic_SubGraph_SaveLoadBool_boolean_541 = local_PlayerDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_541 = local_PlayerDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_541.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_541, logic_SubGraph_SaveLoadBool_boolAsVariable_541, logic_SubGraph_SaveLoadBool_uniqueID_541);
	}

	private void Relay_True_542()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_542.True(out logic_uScriptAct_SetBool_Target_542);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_542;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_542.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_False_542()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_542.False(out logic_uScriptAct_SetBool_Target_542);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_542;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_542.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_TechDestroyedEvent_543()
	{
		local_544_Tank = event_UnityEngine_GameObject_Tech_543;
		Relay_In_547();
	}

	private void Relay_In_547()
	{
		logic_uScriptCon_CheckIntEquals_B_547 = local_Stage_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_547.In(logic_uScriptCon_CheckIntEquals_A_547, logic_uScriptCon_CheckIntEquals_B_547);
		if (logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_547.True)
		{
			Relay_True_542();
		}
	}

	private void Relay_In_549()
	{
		int num = 0;
		Array msgPlayerDead = MsgPlayerDead;
		if (logic_uScript_AddOnScreenMessage_locString_549.Length != num + msgPlayerDead.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_549, num + msgPlayerDead.Length);
		}
		Array.Copy(msgPlayerDead, 0, logic_uScript_AddOnScreenMessage_locString_549, num, msgPlayerDead.Length);
		num += msgPlayerDead.Length;
		logic_uScript_AddOnScreenMessage_tag_549 = MsgPlayerDeadTag;
		logic_uScript_AddOnScreenMessage_speaker_549 = TechSpeaker;
		logic_uScript_AddOnScreenMessage_Return_549 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_549.In(logic_uScript_AddOnScreenMessage_locString_549, logic_uScript_AddOnScreenMessage_msgPriority_549, logic_uScript_AddOnScreenMessage_holdMsg_549, logic_uScript_AddOnScreenMessage_tag_549, logic_uScript_AddOnScreenMessage_speaker_549, logic_uScript_AddOnScreenMessage_side_549);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_549.Out)
		{
			Relay_False_550();
		}
	}

	private void Relay_True_550()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_550.True(out logic_uScriptAct_SetBool_Target_550);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_550;
	}

	private void Relay_False_550()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_550.False(out logic_uScriptAct_SetBool_Target_550);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_550;
	}

	private void Relay_In_556()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_556.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_556.Out)
		{
			Relay_In_669();
		}
	}

	private void Relay_In_557()
	{
		logic_uScript_GetTankBlock_tank_557 = local_R1Turret04Tech_Tank;
		logic_uScript_GetTankBlock_blockType_557 = R1Turret04TechShieldBlockData;
		logic_uScript_GetTankBlock_Return_557 = logic_uScript_GetTankBlock_uScript_GetTankBlock_557.In(logic_uScript_GetTankBlock_tank_557, logic_uScript_GetTankBlock_blockType_557);
		local_R1Turret04TechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_557;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_557.Returned)
		{
			Relay_In_574();
		}
	}

	private void Relay_In_558()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_558.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_558.Out)
		{
			Relay_In_666();
		}
	}

	private void Relay_In_560()
	{
		logic_uScript_SetShieldEnabled_targetObject_560 = local_R1Turret01TechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_560.In(logic_uScript_SetShieldEnabled_targetObject_560, logic_uScript_SetShieldEnabled_enable_560);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_560.Out)
		{
			Relay_In_664();
		}
	}

	private void Relay_In_566()
	{
		logic_uScript_GetTankBlock_tank_566 = local_R1Turret01Tech_Tank;
		logic_uScript_GetTankBlock_blockType_566 = R1Turret01TechShieldBlockData;
		logic_uScript_GetTankBlock_Return_566 = logic_uScript_GetTankBlock_uScript_GetTankBlock_566.In(logic_uScript_GetTankBlock_tank_566, logic_uScript_GetTankBlock_blockType_566);
		local_R1Turret01TechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_566;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_566.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_566.NotFound;
		if (returned)
		{
			Relay_In_560();
		}
		if (notFound)
		{
			Relay_In_581();
		}
	}

	private void Relay_In_570()
	{
		logic_uScript_SetShieldEnabled_targetObject_570 = local_R1Turret02TechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_570.In(logic_uScript_SetShieldEnabled_targetObject_570, logic_uScript_SetShieldEnabled_enable_570);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_570.Out)
		{
			Relay_In_666();
		}
	}

	private void Relay_In_574()
	{
		logic_uScript_SetShieldEnabled_targetObject_574 = local_R1Turret04TechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_574.In(logic_uScript_SetShieldEnabled_targetObject_574, logic_uScript_SetShieldEnabled_enable_574);
	}

	private void Relay_In_575()
	{
		logic_uScript_GetTankBlock_tank_575 = local_R1Turret03Tech_Tank;
		logic_uScript_GetTankBlock_blockType_575 = R1Turret03TechShieldBlockData;
		logic_uScript_GetTankBlock_Return_575 = logic_uScript_GetTankBlock_uScript_GetTankBlock_575.In(logic_uScript_GetTankBlock_tank_575, logic_uScript_GetTankBlock_blockType_575);
		local_R1Turret03TechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_575;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_575.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_575.NotFound;
		if (returned)
		{
			Relay_In_580();
		}
		if (notFound)
		{
			Relay_In_556();
		}
	}

	private void Relay_In_577()
	{
		logic_uScript_GetTankBlock_tank_577 = local_R1Turret02Tech_Tank;
		logic_uScript_GetTankBlock_blockType_577 = R1Turret02TechShieldBlockData;
		logic_uScript_GetTankBlock_Return_577 = logic_uScript_GetTankBlock_uScript_GetTankBlock_577.In(logic_uScript_GetTankBlock_tank_577, logic_uScript_GetTankBlock_blockType_577);
		local_R1Turret02TechShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_577;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_577.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_577.NotFound;
		if (returned)
		{
			Relay_In_570();
		}
		if (notFound)
		{
			Relay_In_558();
		}
	}

	private void Relay_In_580()
	{
		logic_uScript_SetShieldEnabled_targetObject_580 = local_R1Turret03TechShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_580.In(logic_uScript_SetShieldEnabled_targetObject_580, logic_uScript_SetShieldEnabled_enable_580);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_580.Out)
		{
			Relay_In_669();
		}
	}

	private void Relay_In_581()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_581.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_581.Out)
		{
			Relay_In_664();
		}
	}

	private void Relay_In_583()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_583 = CornerArena01TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_583 = CornerArena01TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_583.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_583, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_583, ref logic_uScript_IsPlayerInTriggerSmart_inside_583);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_583.Out;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_583.AllOutside;
		if (num)
		{
			Relay_In_584();
		}
		if (allOutside)
		{
			Relay_False_589();
		}
	}

	private void Relay_In_584()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_584 = CornerArena02TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_584 = CornerArena02TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_584.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_584, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_584, ref logic_uScript_IsPlayerInTriggerSmart_inside_584);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_584.Out;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_584.AllOutside;
		if (num)
		{
			Relay_In_585();
		}
		if (allOutside)
		{
			Relay_False_596();
		}
	}

	private void Relay_In_585()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_585 = CornerArena03TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_585 = CornerArena03TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_585.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_585, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_585, ref logic_uScript_IsPlayerInTriggerSmart_inside_585);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_585.Out;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_585.AllOutside;
		if (num)
		{
			Relay_In_586();
		}
		if (allOutside)
		{
			Relay_False_597();
		}
	}

	private void Relay_In_586()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_586 = CornerArena04TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_586 = CornerArena04TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_586.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_586, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_586, ref logic_uScript_IsPlayerInTriggerSmart_inside_586);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_586.Out;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_586.AllOutside;
		if (num)
		{
			Relay_In_621();
		}
		if (allOutside)
		{
			Relay_False_601();
		}
	}

	private void Relay_True_589()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_589.True(out logic_uScriptAct_SetBool_Target_589);
		local_isInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_589;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_589.Out)
		{
			Relay_False_592();
		}
	}

	private void Relay_False_589()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_589.False(out logic_uScriptAct_SetBool_Target_589);
		local_isInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_589;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_589.Out)
		{
			Relay_False_592();
		}
	}

	private void Relay_In_590()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_590.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_590.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_590.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_583();
		}
		if (multiplayer)
		{
			Relay_In_606();
		}
	}

	private void Relay_True_592()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_592.True(out logic_uScriptAct_SetBool_Target_592);
		local_isAllInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_592;
	}

	private void Relay_False_592()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_592.False(out logic_uScriptAct_SetBool_Target_592);
		local_isAllInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_592;
	}

	private void Relay_True_594()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_594.True(out logic_uScriptAct_SetBool_Target_594);
		local_isAllInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_594;
	}

	private void Relay_False_594()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_594.False(out logic_uScriptAct_SetBool_Target_594);
		local_isAllInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_594;
	}

	private void Relay_True_596()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_596.True(out logic_uScriptAct_SetBool_Target_596);
		local_isInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_596;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_596.Out)
		{
			Relay_False_594();
		}
	}

	private void Relay_False_596()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_596.False(out logic_uScriptAct_SetBool_Target_596);
		local_isInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_596;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_596.Out)
		{
			Relay_False_594();
		}
	}

	private void Relay_True_597()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_597.True(out logic_uScriptAct_SetBool_Target_597);
		local_isInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_597;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_597.Out)
		{
			Relay_False_598();
		}
	}

	private void Relay_False_597()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_597.False(out logic_uScriptAct_SetBool_Target_597);
		local_isInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_597;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_597.Out)
		{
			Relay_False_598();
		}
	}

	private void Relay_True_598()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_598.True(out logic_uScriptAct_SetBool_Target_598);
		local_isAllInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_598;
	}

	private void Relay_False_598()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_598.False(out logic_uScriptAct_SetBool_Target_598);
		local_isAllInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_598;
	}

	private void Relay_True_601()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_601.True(out logic_uScriptAct_SetBool_Target_601);
		local_isInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_601;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_601.Out)
		{
			Relay_False_602();
		}
	}

	private void Relay_False_601()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_601.False(out logic_uScriptAct_SetBool_Target_601);
		local_isInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_601;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_601.Out)
		{
			Relay_False_602();
		}
	}

	private void Relay_True_602()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_602.True(out logic_uScriptAct_SetBool_Target_602);
		local_isAllInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_602;
	}

	private void Relay_False_602()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_602.False(out logic_uScriptAct_SetBool_Target_602);
		local_isAllInsideArenaArea_System_Boolean = logic_uScriptAct_SetBool_Target_602;
	}

	private void Relay_In_606()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_606 = CornerArena01TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_606 = CornerArena01TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_606.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_606, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_606, ref logic_uScript_IsPlayerInTriggerSmart_inside_606);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_606.Out;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_606.AllInside;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_606.AllOutside;
		bool someInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_606.SomeInside;
		if (num)
		{
			Relay_In_608();
		}
		if (allInside)
		{
			Relay_True_614();
		}
		if (allOutside)
		{
			Relay_False_614();
		}
		if (someInside)
		{
			Relay_True_614();
		}
	}

	private void Relay_In_608()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_608 = CornerArena02TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_608 = CornerArena02TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_608.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_608, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_608, ref logic_uScript_IsPlayerInTriggerSmart_inside_608);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_608.Out;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_608.AllInside;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_608.AllOutside;
		bool someInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_608.SomeInside;
		if (num)
		{
			Relay_In_610();
		}
		if (allInside)
		{
			Relay_True_616();
		}
		if (allOutside)
		{
			Relay_False_616();
		}
		if (someInside)
		{
			Relay_True_616();
		}
	}

	private void Relay_In_610()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_610 = CornerArena03TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_610 = CornerArena03TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_610.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_610, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_610, ref logic_uScript_IsPlayerInTriggerSmart_inside_610);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_610.Out;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_610.AllInside;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_610.AllOutside;
		bool someInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_610.SomeInside;
		if (num)
		{
			Relay_In_611();
		}
		if (allInside)
		{
			Relay_True_617();
		}
		if (allOutside)
		{
			Relay_False_617();
		}
		if (someInside)
		{
			Relay_True_617();
		}
	}

	private void Relay_In_611()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_611 = CornerArena04TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_611 = CornerArena04TriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_611.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_611, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_611, ref logic_uScript_IsPlayerInTriggerSmart_inside_611);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_611.Out;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_611.AllInside;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_611.AllOutside;
		bool someInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_611.SomeInside;
		if (num)
		{
			Relay_In_623();
		}
		if (allInside)
		{
			Relay_True_619();
		}
		if (allOutside)
		{
			Relay_False_619();
		}
		if (someInside)
		{
			Relay_True_619();
		}
	}

	private void Relay_True_614()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_614.True(out logic_uScriptAct_SetBool_Target_614);
		local_isInsideCornerArena01Area_System_Boolean = logic_uScriptAct_SetBool_Target_614;
	}

	private void Relay_False_614()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_614.False(out logic_uScriptAct_SetBool_Target_614);
		local_isInsideCornerArena01Area_System_Boolean = logic_uScriptAct_SetBool_Target_614;
	}

	private void Relay_True_616()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_616.True(out logic_uScriptAct_SetBool_Target_616);
		local_isInsideCornerArena02Area_System_Boolean = logic_uScriptAct_SetBool_Target_616;
	}

	private void Relay_False_616()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_616.False(out logic_uScriptAct_SetBool_Target_616);
		local_isInsideCornerArena02Area_System_Boolean = logic_uScriptAct_SetBool_Target_616;
	}

	private void Relay_True_617()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_617.True(out logic_uScriptAct_SetBool_Target_617);
		local_isInsideCornerArena03Area_System_Boolean = logic_uScriptAct_SetBool_Target_617;
	}

	private void Relay_False_617()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_617.False(out logic_uScriptAct_SetBool_Target_617);
		local_isInsideCornerArena03Area_System_Boolean = logic_uScriptAct_SetBool_Target_617;
	}

	private void Relay_True_619()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_619.True(out logic_uScriptAct_SetBool_Target_619);
		local_isInsideCornerArena04Area_System_Boolean = logic_uScriptAct_SetBool_Target_619;
	}

	private void Relay_False_619()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_619.False(out logic_uScriptAct_SetBool_Target_619);
		local_isInsideCornerArena04Area_System_Boolean = logic_uScriptAct_SetBool_Target_619;
	}

	private void Relay_In_621()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_621.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_621.Out)
		{
			Relay_In_622();
		}
	}

	private void Relay_In_622()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_622.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_622.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_623()
	{
		logic_uScriptCon_CompareBool_Bool_623 = local_isInsideCornerArena01Area_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_623.In(logic_uScriptCon_CompareBool_Bool_623);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_623.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_623.False;
		if (num)
		{
			Relay_In_631();
		}
		if (flag)
		{
			Relay_In_625();
		}
	}

	private void Relay_In_625()
	{
		logic_uScriptCon_CompareBool_Bool_625 = local_isInsideCornerArena02Area_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_625.In(logic_uScriptCon_CompareBool_Bool_625);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_625.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_625.False;
		if (num)
		{
			Relay_In_632();
		}
		if (flag)
		{
			Relay_In_629();
		}
	}

	private void Relay_In_629()
	{
		logic_uScriptCon_CompareBool_Bool_629 = local_isInsideCornerArena03Area_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_629.In(logic_uScriptCon_CompareBool_Bool_629);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_629.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_629.False;
		if (num)
		{
			Relay_In_633();
		}
		if (flag)
		{
			Relay_In_630();
		}
	}

	private void Relay_In_630()
	{
		logic_uScriptCon_CompareBool_Bool_630 = local_isInsideCornerArena04Area_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_630.In(logic_uScriptCon_CompareBool_Bool_630);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_630.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_630.False;
		if (num)
		{
			Relay_True_635();
		}
		if (flag)
		{
			Relay_False_635();
		}
	}

	private void Relay_In_631()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_631.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_631.Out)
		{
			Relay_In_632();
		}
	}

	private void Relay_In_632()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_632.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_632.Out)
		{
			Relay_In_633();
		}
	}

	private void Relay_In_633()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_633.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_633.Out)
		{
			Relay_True_635();
		}
	}

	private void Relay_In_634()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_634.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_634.Out)
		{
			Relay_In_621();
		}
	}

	private void Relay_True_635()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_635.True(out logic_uScriptAct_SetBool_Target_635);
		local_isInsideAnyCornerArea_System_Boolean = logic_uScriptAct_SetBool_Target_635;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_635.Out)
		{
			Relay_In_634();
		}
	}

	private void Relay_False_635()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_635.False(out logic_uScriptAct_SetBool_Target_635);
		local_isInsideAnyCornerArea_System_Boolean = logic_uScriptAct_SetBool_Target_635;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_635.Out)
		{
			Relay_In_634();
		}
	}

	private void Relay_In_638()
	{
		logic_uScriptCon_CompareBool_Bool_638 = local_isInsideAnyCornerArea_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_638.In(logic_uScriptCon_CompareBool_Bool_638);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_638.True)
		{
			Relay_False_642();
		}
	}

	private void Relay_In_639()
	{
		logic_uScriptCon_CompareBool_Bool_639 = local_RoundActive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_639.In(logic_uScriptCon_CompareBool_Bool_639);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_639.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_639.False;
		if (num)
		{
			Relay_In_638();
		}
		if (flag)
		{
			Relay_In_517();
		}
	}

	private void Relay_True_642()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_642.True(out logic_uScriptAct_SetBool_Target_642);
		local_RoundActive_System_Boolean = logic_uScriptAct_SetBool_Target_642;
	}

	private void Relay_False_642()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_642.False(out logic_uScriptAct_SetBool_Target_642);
		local_RoundActive_System_Boolean = logic_uScriptAct_SetBool_Target_642;
	}

	private void Relay_Save_Out_644()
	{
	}

	private void Relay_Load_Out_644()
	{
		Relay_In_687();
	}

	private void Relay_Restart_Out_644()
	{
	}

	private void Relay_Save_644()
	{
		logic_SubGraph_SaveLoadBool_boolean_644 = local_isInsideAnyCornerArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_644 = local_isInsideAnyCornerArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Save(ref logic_SubGraph_SaveLoadBool_boolean_644, logic_SubGraph_SaveLoadBool_boolAsVariable_644, logic_SubGraph_SaveLoadBool_uniqueID_644);
	}

	private void Relay_Load_644()
	{
		logic_SubGraph_SaveLoadBool_boolean_644 = local_isInsideAnyCornerArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_644 = local_isInsideAnyCornerArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Load(ref logic_SubGraph_SaveLoadBool_boolean_644, logic_SubGraph_SaveLoadBool_boolAsVariable_644, logic_SubGraph_SaveLoadBool_uniqueID_644);
	}

	private void Relay_Set_True_644()
	{
		logic_SubGraph_SaveLoadBool_boolean_644 = local_isInsideAnyCornerArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_644 = local_isInsideAnyCornerArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_644, logic_SubGraph_SaveLoadBool_boolAsVariable_644, logic_SubGraph_SaveLoadBool_uniqueID_644);
	}

	private void Relay_Set_False_644()
	{
		logic_SubGraph_SaveLoadBool_boolean_644 = local_isInsideAnyCornerArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_644 = local_isInsideAnyCornerArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_644.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_644, logic_SubGraph_SaveLoadBool_boolAsVariable_644, logic_SubGraph_SaveLoadBool_uniqueID_644);
	}

	private void Relay_In_646()
	{
		logic_uScriptCon_CompareBool_Bool_646 = local_isInsideAnyCornerArea_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_646.In(logic_uScriptCon_CompareBool_Bool_646);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_646.False)
		{
			Relay_False_661();
		}
	}

	private void Relay_In_648()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_648 = local_MPHoldTag_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_648.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_648, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_648);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_648.Out)
		{
			Relay_In_319();
		}
	}

	private void Relay_In_653()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_653.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_653.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_653.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_113();
		}
		if (multiplayer)
		{
			Relay_In_1534();
		}
	}

	private void Relay_In_654()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_654 = owner_Connection_655;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_654.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_654);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_654.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_654.False;
		if (num)
		{
			Relay_Pause_7();
		}
		if (flag)
		{
			Relay_UnPause_7();
		}
	}

	private void Relay_In_656()
	{
		logic_uScriptCon_CompareBool_Bool_656 = local_PlayerDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_656.In(logic_uScriptCon_CompareBool_Bool_656);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_656.True)
		{
			Relay_In_659();
		}
	}

	private void Relay_In_659()
	{
		logic_uScriptCon_CompareBool_Bool_659 = local_isInsideRespawnArea_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_659.In(logic_uScriptCon_CompareBool_Bool_659);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_659.True)
		{
			Relay_In_549();
		}
	}

	private void Relay_True_661()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_661.True(out logic_uScriptAct_SetBool_Target_661);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_661;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_661.Out)
		{
			Relay_In_648();
		}
	}

	private void Relay_False_661()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_661.False(out logic_uScriptAct_SetBool_Target_661);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_661;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_661.Out)
		{
			Relay_In_648();
		}
	}

	private void Relay_In_662()
	{
		logic_uScript_IsTechAlive_tech_662 = local_R1Turret01Tech_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_662.In(logic_uScript_IsTechAlive_tech_662);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_662.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_662.Destroyed;
		if (alive)
		{
			Relay_In_566();
		}
		if (destroyed)
		{
			Relay_In_581();
		}
	}

	private void Relay_In_664()
	{
		logic_uScript_IsTechAlive_tech_664 = local_R1Turret02Tech_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_664.In(logic_uScript_IsTechAlive_tech_664);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_664.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_664.Destroyed;
		if (alive)
		{
			Relay_In_577();
		}
		if (destroyed)
		{
			Relay_In_558();
		}
	}

	private void Relay_In_666()
	{
		logic_uScript_IsTechAlive_tech_666 = local_R1Turret03Tech_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_666.In(logic_uScript_IsTechAlive_tech_666);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_666.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_666.Destroyed;
		if (alive)
		{
			Relay_In_575();
		}
		if (destroyed)
		{
			Relay_In_556();
		}
	}

	private void Relay_In_669()
	{
		logic_uScript_IsTechAlive_tech_669 = local_R1Turret04Tech_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_669.In(logic_uScript_IsTechAlive_tech_669);
		if (logic_uScript_IsTechAlive_uScript_IsTechAlive_669.Alive)
		{
			Relay_In_557();
		}
	}

	private void Relay_In_670()
	{
		logic_uScript_IsTechAlive_tech_670 = local_R1Turret01Tech_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_670.In(logic_uScript_IsTechAlive_tech_670);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_670.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_670.Destroyed;
		if (alive)
		{
			Relay_In_248();
		}
		if (destroyed)
		{
			Relay_In_257();
		}
	}

	private void Relay_In_672()
	{
		logic_uScript_IsTechAlive_tech_672 = local_R1Turret02Tech_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_672.In(logic_uScript_IsTechAlive_tech_672);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_672.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_672.Destroyed;
		if (alive)
		{
			Relay_In_257();
		}
		if (destroyed)
		{
			Relay_In_265();
		}
	}

	private void Relay_In_674()
	{
		logic_uScript_IsTechAlive_tech_674 = local_R1Turret03Tech_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_674.In(logic_uScript_IsTechAlive_tech_674);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_674.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_674.Destroyed;
		if (alive)
		{
			Relay_In_265();
		}
		if (destroyed)
		{
			Relay_In_277();
		}
	}

	private void Relay_In_676()
	{
		logic_uScript_IsTechAlive_tech_676 = local_R1Turret04Tech_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_676.In(logic_uScript_IsTechAlive_tech_676);
		if (logic_uScript_IsTechAlive_uScript_IsTechAlive_676.Alive)
		{
			Relay_In_277();
		}
	}

	private void Relay_In_678()
	{
		logic_uScript_IsTechAlive_tech_678 = local_R1Turret01Tech_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_678.In(logic_uScript_IsTechAlive_tech_678);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_678.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_678.Destroyed;
		if (alive)
		{
			Relay_In_291();
		}
		if (destroyed)
		{
			Relay_In_303();
		}
	}

	private void Relay_In_680()
	{
		logic_uScript_IsTechAlive_tech_680 = local_R1Turret02Tech_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_680.In(logic_uScript_IsTechAlive_tech_680);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_680.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_680.Destroyed;
		if (alive)
		{
			Relay_In_287();
		}
		if (destroyed)
		{
			Relay_In_309();
		}
	}

	private void Relay_In_683()
	{
		logic_uScript_IsTechAlive_tech_683 = local_R1Turret03Tech_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_683.In(logic_uScript_IsTechAlive_tech_683);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_683.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_683.Destroyed;
		if (alive)
		{
			Relay_In_288();
		}
		if (destroyed)
		{
			Relay_In_310();
		}
	}

	private void Relay_In_684()
	{
		logic_uScript_IsTechAlive_tech_684 = local_R1Turret04Tech_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_684.In(logic_uScript_IsTechAlive_tech_684);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_684.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_684.Destroyed;
		if (alive)
		{
			Relay_In_300();
		}
		if (destroyed)
		{
			Relay_In_476();
		}
	}

	private void Relay_Out_687()
	{
	}

	private void Relay_In_687()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_687 = local_Objective_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_687.In(logic_SubGraph_LoadObjectiveStates_currentObjective_687);
	}

	private void Relay_In_688()
	{
		logic_uScriptCon_CompareBool_Bool_688 = local_isAllInsideNPCTrigger_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_688.In(logic_uScriptCon_CompareBool_Bool_688);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_688.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_688.False;
		if (num)
		{
			Relay_In_653();
		}
		if (flag)
		{
			Relay_In_690();
		}
	}

	private void Relay_In_690()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_690.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_690.Out)
		{
			Relay_In_691();
		}
	}

	private void Relay_In_691()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_691.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_691.Out)
		{
			Relay_In_697();
		}
	}

	private void Relay_True_693()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_693.True(out logic_uScriptAct_SetBool_Target_693);
		local_VisitedNPC_System_Boolean = logic_uScriptAct_SetBool_Target_693;
	}

	private void Relay_False_693()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_693.False(out logic_uScriptAct_SetBool_Target_693);
		local_VisitedNPC_System_Boolean = logic_uScriptAct_SetBool_Target_693;
	}

	private void Relay_In_694()
	{
		int num = 0;
		Array msgR1IntroInterrupted = MsgR1IntroInterrupted;
		if (logic_uScript_AddOnScreenMessage_locString_694.Length != num + msgR1IntroInterrupted.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_694, num + msgR1IntroInterrupted.Length);
		}
		Array.Copy(msgR1IntroInterrupted, 0, logic_uScript_AddOnScreenMessage_locString_694, num, msgR1IntroInterrupted.Length);
		num += msgR1IntroInterrupted.Length;
		logic_uScript_AddOnScreenMessage_speaker_694 = TechSpeaker;
		logic_uScript_AddOnScreenMessage_Return_694 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_694.In(logic_uScript_AddOnScreenMessage_locString_694, logic_uScript_AddOnScreenMessage_msgPriority_694, logic_uScript_AddOnScreenMessage_holdMsg_694, logic_uScript_AddOnScreenMessage_tag_694, logic_uScript_AddOnScreenMessage_speaker_694, logic_uScript_AddOnScreenMessage_side_694);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_694.Out)
		{
			Relay_False_693();
		}
	}

	private void Relay_In_697()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_697 = local_IntroTag_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_697.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_697, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_697);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_697.Out)
		{
			Relay_In_694();
		}
	}

	private void Relay_In_1534()
	{
		logic_uScript_PlayDialogue_dialogue_1534 = R1IntroDialogueMP;
		logic_uScript_PlayDialogue_progress_1534 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_1534.In(logic_uScript_PlayDialogue_dialogue_1534, ref logic_uScript_PlayDialogue_progress_1534);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_1534;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_1534.Shown)
		{
			Relay_In_1539();
		}
	}

	private void Relay_In_1539()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1539.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1539.Out)
		{
			Relay_In_1541();
		}
	}

	private void Relay_In_1541()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1541.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1541.Out)
		{
			Relay_In_352();
		}
	}
}
