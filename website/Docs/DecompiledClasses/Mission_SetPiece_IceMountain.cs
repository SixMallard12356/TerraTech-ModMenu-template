using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("", "")]
public class Mission_SetPiece_IceMountain : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public ExternalBehaviorTree AIFlyTree;

	[Multiline(3)]
	public string Checkpoint01 = "";

	[Multiline(3)]
	public string Checkpoint02 = "";

	[Multiline(3)]
	public string Checkpoint03 = "";

	public uScript_PlayDialogue.Dialogue CheckpointMissed01;

	[Multiline(3)]
	public string EnemySpawnTrigger01 = "";

	[Multiline(3)]
	public string EnemySpawnTrigger02 = "";

	[Multiline(3)]
	public string EnemySpawnTrigger03 = "";

	[Multiline(3)]
	public string EnemySpawnTrigger05 = "";

	public SpawnTechData[] EnemyWaveData01 = new SpawnTechData[0];

	public SpawnTechData[] EnemyWaveData02 = new SpawnTechData[0];

	public SpawnTechData[] EnemyWaveData03 = new SpawnTechData[0];

	public SpawnTechData[] EnemyWaveData04 = new SpawnTechData[0];

	public SpawnTechData[] EnemyWaveData05 = new SpawnTechData[0];

	public uScript_PlayDialogue.Dialogue FailDeadDialogue;

	public uScript_PlayDialogue.Dialogue FailDialogue;

	[Multiline(3)]
	public string FinishTriggerArea = "";

	public uScript_PlayDialogue.Dialogue ForbiddenBlockDialogue;

	[Multiline(3)]
	public string ForbiddenBlockTag = "";

	public uScript_PlayDialogue.Dialogue IntroDialogue;

	[Multiline(3)]
	public string IntroTag = "";

	[Multiline(3)]
	public string IntroTrigger = "";

	[Multiline(3)]
	public string Level01Area = "";

	[Multiline(3)]
	public string Level02Area = "";

	[Multiline(3)]
	public string Level03Area = "";

	private Tank[] local_112_TankArray = new Tank[0];

	private Tank[] local_114_TankArray = new Tank[0];

	private string local_28_System_String = "";

	private string local_29_System_String = "";

	private string local_30_System_String = "Stage:";

	private string local_313_System_String = ",Dialogue:";

	private string local_314_System_String = "";

	private string local_315_System_String = "";

	private string local_318_System_String = "";

	private string local_319_System_String = ",OBJ:";

	private string local_32_System_String = "";

	private string local_320_System_String = "";

	private string local_322_System_String = "";

	private string local_325_System_String = ",A4:";

	private string local_326_System_String = "";

	private string local_327_System_String = ",Inside:";

	private string local_328_System_String = "";

	private string local_330_System_String = "";

	private string local_331_System_String = ",CP:";

	private string local_333_System_String = "";

	private string local_36_System_String = ",A:";

	private string local_37_System_String = "";

	private Tank local_411_Tank;

	private int local_Checkpoint_System_Int32;

	private int local_DialogueProgress_System_Int32;

	private bool local_EnemyWave01_System_Boolean;

	private Tank local_EnemyWave01Tank01_Tank;

	private Tank local_EnemyWave01Tank02_Tank;

	private bool local_EnemyWave02_System_Boolean;

	private Tank local_EnemyWave02Tank01_Tank;

	private Tank local_EnemyWave02Tank02_Tank;

	private Tank local_EnemyWave02Tank03_Tank;

	private Tank local_EnemyWave02Tank04_Tank;

	private Tank local_EnemyWave02Tank05_Tank;

	private bool local_EnemyWave03_System_Boolean;

	private Tank local_EnemyWave03Tank01_Tank;

	private Tank local_EnemyWave03Tank02_Tank;

	private bool local_EnemyWave04_System_Boolean;

	private Tank local_EnemyWave04Tank01_Tank;

	private Tank local_EnemyWave04Tank02_Tank;

	private Tank local_EnemyWave04Tank03_Tank;

	private Tank local_EnemyWave04Tank04_Tank;

	private bool local_EnemyWave05_System_Boolean;

	private Tank local_EnemyWave05Tank01_Tank;

	private Tank local_EnemyWave05Tank02_Tank;

	private Tank local_EnemyWave05Tank03_Tank;

	private Tank local_EnemyWave05Tank04_Tank;

	private Tank[] local_EnemyWaveTechs01_TankArray = new Tank[0];

	private Tank[] local_EnemyWaveTechs02_TankArray = new Tank[0];

	private Tank[] local_EnemyWaveTechs03_TankArray = new Tank[0];

	private Tank[] local_EnemyWaveTechs04_TankArray = new Tank[0];

	private Tank[] local_EnemyWaveTechs05_TankArray = new Tank[0];

	private bool local_HasFlightBlock_System_Boolean;

	private bool local_Init_System_Boolean;

	private bool local_InsideMissionArea_System_Boolean;

	private bool local_inStartTrigger_System_Boolean;

	private bool local_IntroPlayed_System_Boolean;

	private bool local_isinFinishArea_System_Boolean;

	private bool local_isinLevel01Area_System_Boolean;

	private bool local_isinLevel02Area_System_Boolean;

	private bool local_isinLevel03Area_System_Boolean;

	private int local_LevelArea_System_Int32;

	private Tank local_NPCEndTech01_Tank;

	private Tank local_NPCEndTech02_Tank;

	private Tank local_NPCTech01_Tank;

	private Tank local_NPCTech02_Tank;

	private Tank[] local_NPCTechs_TankArray = new Tank[0];

	private Tank[] local_NPCTechsEnd01_TankArray = new Tank[0];

	private int local_Objective_System_Int32 = 1;

	private bool local_PlayerDead_System_Boolean;

	private bool local_SetFailOnLoad_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private bool local_VisitedNPC_System_Boolean;

	[Multiline(3)]
	public string MissionAreaTrigger = "";

	public uScript_PlayDialogue.Dialogue MissionCompleteDialogue;

	public LocalisedString[] msgForbiddenBlock = new LocalisedString[0];

	public LocalisedString[] msgForbiddenBlockRunning = new LocalisedString[0];

	public LocalisedString[] msgIntroInterrupted = new LocalisedString[0];

	public ManOnScreenMessages.Speaker NPCSpeaker;

	public SpawnTechData[] NPCTechData = new SpawnTechData[0];

	public SpawnTechData[] NPCTechEnd01Data = new SpawnTechData[0];

	[Multiline(3)]
	public string ObjPos01 = "";

	[Multiline(3)]
	public string ObjPos02 = "";

	[Multiline(3)]
	public string ObjPos03 = "";

	[Multiline(3)]
	public string ObjPos04 = "";

	public uScript_PlayDialogue.Dialogue OutOfBounds;

	[Multiline(3)]
	public string OutsideArea01 = "";

	[Multiline(3)]
	public string OutsideArea02 = "";

	[Multiline(3)]
	public string OutsideArea02Half01 = "";

	[Multiline(3)]
	public string OutsideArea02Half02 = "";

	[Multiline(3)]
	public string OutsideArea02Half03 = "";

	[Multiline(3)]
	public string OutsideArea02Half04 = "";

	[Multiline(3)]
	public string OutsideArea03 = "";

	[Multiline(3)]
	public string OutsideArea04 = "";

	[Multiline(3)]
	public string OutsideArea05 = "";

	[Multiline(3)]
	public string OutsideArea06 = "";

	[Multiline(3)]
	public string OutsideArea07 = "";

	[Multiline(3)]
	public string OutsideIntroTrigger = "";

	public uScript_PlayDialogue.Dialogue RaceStartDialogue;

	public Transform RemovalParticles;

	[Multiline(3)]
	public string RespawnArea = "";

	[Multiline(3)]
	public string StartLineTrigger = "";

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_62;

	private GameObject owner_Connection_69;

	private GameObject owner_Connection_80;

	private GameObject owner_Connection_88;

	private GameObject owner_Connection_101;

	private GameObject owner_Connection_168;

	private GameObject owner_Connection_174;

	private GameObject owner_Connection_184;

	private GameObject owner_Connection_191;

	private GameObject owner_Connection_199;

	private GameObject owner_Connection_227;

	private GameObject owner_Connection_294;

	private GameObject owner_Connection_412;

	private GameObject owner_Connection_455;

	private GameObject owner_Connection_460;

	private GameObject owner_Connection_462;

	private GameObject owner_Connection_486;

	private GameObject owner_Connection_495;

	private GameObject owner_Connection_504;

	private GameObject owner_Connection_507;

	private GameObject owner_Connection_510;

	private GameObject owner_Connection_513;

	private GameObject owner_Connection_515;

	private GameObject owner_Connection_519;

	private GameObject owner_Connection_523;

	private GameObject owner_Connection_526;

	private GameObject owner_Connection_543;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_6 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_6;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_6 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_6 = "Stage";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_7;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_7 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_7 = "Init";

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_8 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_8;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_8 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_8 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_8 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_10 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_10 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_11 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_11;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_11 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_13 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_13;

	private bool logic_uScriptAct_SetBool_Out_13 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_13 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_13 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_14;

	private bool logic_uScriptCon_CompareBool_True_14 = true;

	private bool logic_uScriptCon_CompareBool_False_14 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_17 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_17 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_17 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_17;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_17 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_17 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_17 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_17 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_17 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_17 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_17 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_18 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_18;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_18 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_18;

	private bool logic_uScript_SpawnTechsFromData_Out_18 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_21 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_21;

	private int logic_uScript_PlayDialogue_progress_21;

	private bool logic_uScript_PlayDialogue_Out_21 = true;

	private bool logic_uScript_PlayDialogue_Shown_21 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_21 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_22 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_22 = 2;

	private int logic_uScriptAct_SetInt_Target_22;

	private bool logic_uScriptAct_SetInt_Out_22 = true;

	private uScriptCon_BigManualSwitch logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24 = new uScriptCon_BigManualSwitch();

	private int logic_uScriptCon_BigManualSwitch_CurrentOutput_24;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_31 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_31 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_31 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_31 = "";

	private string logic_uScriptAct_Concatenate_Result_31;

	private bool logic_uScriptAct_Concatenate_Out_31 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_33 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_33 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_33 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_33 = "";

	private string logic_uScriptAct_Concatenate_Result_33;

	private bool logic_uScriptAct_Concatenate_Out_33 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_34 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_34 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_34 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_34 = "";

	private string logic_uScriptAct_Concatenate_Result_34;

	private bool logic_uScriptAct_Concatenate_Out_34 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_35 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_35 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_35 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_35 = "";

	private string logic_uScriptAct_Concatenate_Result_35;

	private bool logic_uScriptAct_Concatenate_Out_35 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_39 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_40 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_40 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_40 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_40;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_40 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_40 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_40 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_40 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_40 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_40 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_40 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_42 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_42 = 3;

	private int logic_uScriptAct_SetInt_Target_42;

	private bool logic_uScriptAct_SetInt_Out_42 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_43 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_43 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_43 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_43;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_43 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_43 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_43 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_43 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_43 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_43 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_43 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_45 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_45 = 5;

	private int logic_uScriptAct_SetInt_Target_45;

	private bool logic_uScriptAct_SetInt_Out_45 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_46 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_46 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_46 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_46;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_46 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_46 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_46 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_46 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_46 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_46 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_46 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_50 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_50;

	private int logic_uScript_PlayDialogue_progress_50;

	private bool logic_uScript_PlayDialogue_Out_50 = true;

	private bool logic_uScript_PlayDialogue_Shown_50 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_50 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_52 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_52 = 2;

	private int logic_uScriptAct_SetInt_Target_52;

	private bool logic_uScriptAct_SetInt_Out_52 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_57 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_57;

	private int logic_uScript_PlayDialogue_progress_57;

	private bool logic_uScript_PlayDialogue_Out_57 = true;

	private bool logic_uScript_PlayDialogue_Shown_57 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_57 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_59 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_59 = 6;

	private int logic_uScriptAct_SetInt_Target_59;

	private bool logic_uScriptAct_SetInt_Out_59 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_60 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_60;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_60;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_60;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_60;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_60;

	private bool logic_uScript_FlyTechUpAndAway_Out_60 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_61 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_61;

	private bool logic_uScript_FinishEncounter_Out_61 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_63 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_63 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_63;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_63 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_63;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_63 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_63 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_63 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_63 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_67 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_67 = new Tank[0];

	private int logic_uScript_AccessListTech_index_67;

	private Tank logic_uScript_AccessListTech_value_67;

	private bool logic_uScript_AccessListTech_Out_67 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_73 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_73 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_73 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_73;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_73 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_73 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_73 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_73 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_73 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_73 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_73 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_76 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_76 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_76;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_76 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_76 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_76 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_77 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_77;

	private bool logic_uScriptCon_CompareBool_True_77 = true;

	private bool logic_uScriptCon_CompareBool_False_77 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_79 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_79;

	private bool logic_uScriptAct_SetBool_Out_79 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_79 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_79 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_83 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_83;

	private bool logic_uScriptAct_SetBool_Out_83 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_83 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_83 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_84;

	private bool logic_uScriptCon_CompareBool_True_84 = true;

	private bool logic_uScriptCon_CompareBool_False_84 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_86 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_86;

	private bool logic_uScriptAct_SetBool_Out_86 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_86 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_86 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_89;

	private bool logic_uScriptCon_CompareBool_True_89 = true;

	private bool logic_uScriptCon_CompareBool_False_89 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_91 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_91 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_91;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_91 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_91 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_91 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_92 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_92 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_92 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_92;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_92 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_92 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_92 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_92 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_92 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_92 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_92 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_93 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_93;

	private bool logic_uScriptAct_SetBool_Out_93 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_93 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_93 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_96 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_96;

	private bool logic_uScriptAct_SetBool_Out_96 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_96 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_96 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_98 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_98;

	private bool logic_uScriptAct_SetBool_Out_98 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_98 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_98 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_99 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_99 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_99 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_99;

	private bool logic_uScript_DestroyTechsFromData_Out_99 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_102 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_102 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_104 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_104;

	private int logic_uScript_PlayDialogue_progress_104;

	private bool logic_uScript_PlayDialogue_Out_104 = true;

	private bool logic_uScript_PlayDialogue_Shown_104 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_104 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_107 = true;

	private uScript_IsTechInTrigger logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_109 = new uScript_IsTechInTrigger();

	private string logic_uScript_IsTechInTrigger_triggerAreaName_109 = "";

	private Tank[] logic_uScript_IsTechInTrigger_techs_109 = new Tank[0];

	private bool logic_uScript_IsTechInTrigger_Out_109 = true;

	private bool logic_uScript_IsTechInTrigger_InRange_109 = true;

	private bool logic_uScript_IsTechInTrigger_OutOfRange_109 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_110 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_111 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_111 = true;

	private uScript_IsTechFriendlyToPlayer logic_uScript_IsTechFriendlyToPlayer_uScript_IsTechFriendlyToPlayer_113 = new uScript_IsTechFriendlyToPlayer();

	private Tank[] logic_uScript_IsTechFriendlyToPlayer_techsIn_113 = new Tank[0];

	private Tank[] logic_uScript_IsTechFriendlyToPlayer_techsOut_113 = new Tank[0];

	private bool logic_uScript_IsTechFriendlyToPlayer_Out_113 = true;

	private bool logic_uScript_IsTechFriendlyToPlayer_True_113 = true;

	private bool logic_uScript_IsTechFriendlyToPlayer_False_113 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_116 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_116;

	private bool logic_uScriptAct_SetBool_Out_116 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_116 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_116 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_117 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_121 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_121;

	private int logic_uScriptAct_SetInt_Target_121;

	private bool logic_uScriptAct_SetInt_Out_121 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_123 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_123 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_123 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_123;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_123 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_123 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_123 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_123 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_123 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_123 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_123 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_125 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_125 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_125 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_125;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_125 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_125 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_125 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_125 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_125 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_125 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_125 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_127 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_127 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_127 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_127;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_127 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_127 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_127 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_127 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_127 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_127 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_127 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_129 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_129 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_129 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_129;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_129 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_129 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_129 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_129 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_129 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_129 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_129 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_131 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_131 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_131 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_131;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_131 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_131 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_131 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_131 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_131 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_131 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_131 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_133 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_133 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_140 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_140;

	private bool logic_uScriptAct_SetBool_Out_140 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_140 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_140 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_142 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_142 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_146 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_146;

	private int logic_uScript_PlayDialogue_progress_146;

	private bool logic_uScript_PlayDialogue_Out_146 = true;

	private bool logic_uScript_PlayDialogue_Shown_146 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_146 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_148 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_148;

	private int logic_uScriptAct_SetInt_Target_148;

	private bool logic_uScriptAct_SetInt_Out_148 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_150 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_150;

	private bool logic_uScriptCon_CompareBool_True_150 = true;

	private bool logic_uScriptCon_CompareBool_False_150 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_151 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_151 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_154 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_154;

	private int logic_uScriptAct_SetInt_Target_154;

	private bool logic_uScriptAct_SetInt_Out_154 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_156 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_156;

	private int logic_uScript_PlayDialogue_progress_156;

	private bool logic_uScript_PlayDialogue_Out_156 = true;

	private bool logic_uScript_PlayDialogue_Shown_156 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_156 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_158 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_158 = 4;

	private int logic_uScriptAct_SetInt_Target_158;

	private bool logic_uScriptAct_SetInt_Out_158 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_159 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_159 = 4;

	private int logic_uScriptAct_SetInt_Target_159;

	private bool logic_uScriptAct_SetInt_Out_159 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_166 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_166 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_166;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_166 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_166 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_166 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_169 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_169 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_169 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_169;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_169 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_169 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_169 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_169 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_169 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_169 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_169 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_171;

	private bool logic_uScriptCon_CompareBool_True_171 = true;

	private bool logic_uScriptCon_CompareBool_False_171 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_172 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_172 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_172;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_172 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_172 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_172 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_173 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_173;

	private bool logic_uScriptAct_SetBool_Out_173 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_173 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_173 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_178 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_178;

	private bool logic_uScriptAct_SetBool_Out_178 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_178 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_178 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_180 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_180;

	private bool logic_uScriptAct_SetBool_Out_180 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_180 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_180 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_182 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_182 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_182;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_182 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_182;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_182 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_182 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_182 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_182 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_188 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_188 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_189 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_189 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_193 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_193 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_193;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_193 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_193;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_193 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_193 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_193 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_193 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_194 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_195 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_200 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_200;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_200 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_200;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_200 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_200 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_200 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_200 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_204 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_204 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_205 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_205 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_206 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_206 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_208;

	private bool logic_uScriptCon_CompareBool_True_208 = true;

	private bool logic_uScriptCon_CompareBool_False_208 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_209 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_211;

	private bool logic_uScriptCon_CompareBool_True_211 = true;

	private bool logic_uScriptCon_CompareBool_False_211 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_212 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_212 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_214;

	private bool logic_uScriptCon_CompareBool_True_214 = true;

	private bool logic_uScriptCon_CompareBool_False_214 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_215 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_215 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_216 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_216 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_218 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_218 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_219 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_219 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_220 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_220;

	private bool logic_uScriptCon_CompareBool_True_220 = true;

	private bool logic_uScriptCon_CompareBool_False_220 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_223 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_223 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_223;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_223 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_223;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_223 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_223 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_223 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_223 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_228 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_228 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_231 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_231;

	private int logic_uScriptAct_SetInt_Target_231;

	private bool logic_uScriptAct_SetInt_Out_231 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_234 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_234;

	private int logic_uScriptAct_SetInt_Target_234;

	private bool logic_uScriptAct_SetInt_Out_234 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_235 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_235 = 7;

	private int logic_uScriptAct_SetInt_Target_235;

	private bool logic_uScriptAct_SetInt_Out_235 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_238 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_238 = 8;

	private int logic_uScriptAct_SetInt_Target_238;

	private bool logic_uScriptAct_SetInt_Out_238 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_239 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_239 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_240 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_240 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_241 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_241 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_243 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_243;

	private bool logic_uScriptCon_CompareBool_True_243 = true;

	private bool logic_uScriptCon_CompareBool_False_243 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_245 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_245 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_246 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_246;

	private int logic_uScript_PlayDialogue_progress_246;

	private bool logic_uScript_PlayDialogue_Out_246 = true;

	private bool logic_uScript_PlayDialogue_Shown_246 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_246 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_249 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_249;

	private int logic_uScriptAct_SetInt_Target_249;

	private bool logic_uScriptAct_SetInt_Out_249 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_251 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_251 = 4;

	private int logic_uScriptAct_SetInt_Target_251;

	private bool logic_uScriptAct_SetInt_Out_251 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_255;

	private bool logic_uScriptCon_CompareBool_True_255 = true;

	private bool logic_uScriptCon_CompareBool_False_255 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_256 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_256 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_256;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_256;

	private string logic_uScript_AddOnScreenMessage_tag_256 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_256;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_256;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_256;

	private bool logic_uScript_AddOnScreenMessage_Out_256 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_256 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_260 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_260 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_260;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_260 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_266 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_266 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_266;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_266;

	private string logic_uScript_AddOnScreenMessage_tag_266 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_266;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_266;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_266;

	private bool logic_uScript_AddOnScreenMessage_Out_266 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_266 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_269 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_269;

	private bool logic_uScriptCon_CompareBool_True_269 = true;

	private bool logic_uScriptCon_CompareBool_False_269 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_270 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_270 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_271 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_272 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_272 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_272 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_273 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_273;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_276 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_276;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_276;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_278 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_278 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_278 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_281 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_281 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_281 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_282;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_282;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_285 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_285;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_285 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_290 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_290 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_290 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_292 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_292 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_292 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_293 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_293;

	private bool logic_uScript_ClearEncounterTarget_Out_293 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_297;

	private int logic_SubGraph_SaveLoadInt_integer_297;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_297 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_297 = "DialogueProgress";

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_298 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_298 = 1;

	private int logic_uScriptAct_SetInt_Target_298;

	private bool logic_uScriptAct_SetInt_Out_298 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_301 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_301 = 2;

	private int logic_uScriptAct_SetInt_Target_301;

	private bool logic_uScriptAct_SetInt_Out_301 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_302 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_302 = 3;

	private int logic_uScriptAct_SetInt_Target_302;

	private bool logic_uScriptAct_SetInt_Out_302 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_305 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_305;

	private int logic_uScriptCon_CompareInt_B_305 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_305 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_305 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_305 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_305 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_305 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_305 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_306 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_306;

	private int logic_uScriptAct_SetInt_Target_306;

	private bool logic_uScriptAct_SetInt_Out_306 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_309 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_309;

	private int logic_uScriptAct_SetInt_Target_309;

	private bool logic_uScriptAct_SetInt_Out_309 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_311 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_311 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_311 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_312 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_312 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_312 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_312 = "";

	private string logic_uScriptAct_Concatenate_Result_312;

	private bool logic_uScriptAct_Concatenate_Out_312 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_316 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_316 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_316 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_316 = "";

	private string logic_uScriptAct_Concatenate_Result_316;

	private bool logic_uScriptAct_Concatenate_Out_316 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_317 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_317 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_317 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_317 = "";

	private string logic_uScriptAct_Concatenate_Result_317;

	private bool logic_uScriptAct_Concatenate_Out_317 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_321 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_321 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_321 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_321 = "";

	private string logic_uScriptAct_Concatenate_Result_321;

	private bool logic_uScriptAct_Concatenate_Out_321 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_323 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_323 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_323 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_323 = "";

	private string logic_uScriptAct_Concatenate_Result_323;

	private bool logic_uScriptAct_Concatenate_Out_323 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_324 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_324 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_324 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_324 = "";

	private string logic_uScriptAct_Concatenate_Result_324;

	private bool logic_uScriptAct_Concatenate_Out_324 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_329 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_329 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_329 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_329 = "";

	private string logic_uScriptAct_Concatenate_Result_329;

	private bool logic_uScriptAct_Concatenate_Out_329 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_334 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_334 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_334 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_334 = "";

	private string logic_uScriptAct_Concatenate_Result_334;

	private bool logic_uScriptAct_Concatenate_Out_334 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_335 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_335 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_335 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_335 = "";

	private string logic_uScriptAct_Concatenate_Result_335;

	private bool logic_uScriptAct_Concatenate_Out_335 = true;

	private uScriptAct_PrintText logic_uScriptAct_PrintText_uScriptAct_PrintText_340 = new uScriptAct_PrintText();

	private string logic_uScriptAct_PrintText_Text_340 = "";

	private int logic_uScriptAct_PrintText_FontSize_340 = 16;

	private FontStyle logic_uScriptAct_PrintText_FontStyle_340;

	private Color logic_uScriptAct_PrintText_FontColor_340 = new Color(0f, 0f, 0f, 1f);

	private TextAnchor logic_uScriptAct_PrintText_textAnchor_340;

	private int logic_uScriptAct_PrintText_EdgePadding_340 = 8;

	private float logic_uScriptAct_PrintText_time_340;

	private bool logic_uScriptAct_PrintText_Out_340 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_344 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_344;

	private int logic_uScriptCon_CheckIntEquals_B_344;

	private bool logic_uScriptCon_CheckIntEquals_True_344 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_344 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_346 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_346;

	private int logic_uScriptCon_CheckIntEquals_B_346 = 1;

	private bool logic_uScriptCon_CheckIntEquals_True_346 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_346 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_348 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_348;

	private int logic_uScriptCon_CheckIntEquals_B_348 = 2;

	private bool logic_uScriptCon_CheckIntEquals_True_348 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_348 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_360 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_360 = 1;

	private int logic_uScriptAct_SetInt_Target_360;

	private bool logic_uScriptAct_SetInt_Out_360 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_363 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_363;

	private int logic_uScriptCon_CompareInt_B_363;

	private bool logic_uScriptCon_CompareInt_GreaterThan_363 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_363 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_363 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_363 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_363 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_363 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_365 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_365;

	private int logic_uScriptCon_CompareInt_B_365 = 1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_365 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_365 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_365 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_365 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_365 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_365 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_366 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_366 = 2;

	private int logic_uScriptAct_SetInt_Target_366;

	private bool logic_uScriptAct_SetInt_Out_366 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_368 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_368;

	private int logic_uScriptCon_CompareInt_B_368;

	private bool logic_uScriptCon_CompareInt_GreaterThan_368 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_368 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_368 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_368 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_368 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_368 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_369 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_369;

	private int logic_uScriptAct_SetInt_Target_369;

	private bool logic_uScriptAct_SetInt_Out_369 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_370 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_370;

	private int logic_uScript_PlayDialogue_progress_370;

	private bool logic_uScript_PlayDialogue_Out_370 = true;

	private bool logic_uScript_PlayDialogue_Shown_370 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_370 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_373 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_373 = 4;

	private int logic_uScriptAct_SetInt_Target_373;

	private bool logic_uScriptAct_SetInt_Out_373 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_381 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_381;

	private int logic_uScriptAct_SetInt_Target_381;

	private bool logic_uScriptAct_SetInt_Out_381 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_382 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_382 = 3;

	private int logic_uScriptCon_CheckIntEquals_B_382;

	private bool logic_uScriptCon_CheckIntEquals_True_382 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_382 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_385 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_385;

	private bool logic_uScriptAct_SetBool_Out_385 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_385 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_385 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_387 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_387;

	private bool logic_uScriptCon_CompareBool_True_387 = true;

	private bool logic_uScriptCon_CompareBool_False_387 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_390 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_390;

	private int logic_uScriptAct_SetInt_Target_390;

	private bool logic_uScriptAct_SetInt_Out_390 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_391 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_391;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_391;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_393 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_393 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_394 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_394;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_394;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_394;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_394;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_394;

	private bool logic_uScript_FlyTechUpAndAway_Out_394 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_396 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_396 = new Tank[0];

	private int logic_uScript_AccessListTech_index_396 = 1;

	private Tank logic_uScript_AccessListTech_value_396;

	private bool logic_uScript_AccessListTech_Out_396 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_397 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_397 = true;

	private uScript_CheckTechFlightBlocks logic_uScript_CheckTechFlightBlocks_uScript_CheckTechFlightBlocks_400 = new uScript_CheckTechFlightBlocks();

	private Tank[] logic_uScript_CheckTechFlightBlocks_techs_400 = new Tank[0];

	private bool logic_uScript_CheckTechFlightBlocks_Out_400 = true;

	private bool logic_uScript_CheckTechFlightBlocks_HasFlightBlocks_400 = true;

	private bool logic_uScript_CheckTechFlightBlocks_DoesntHaveFlightBlocks_400 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_401 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_401;

	private bool logic_uScriptAct_SetBool_Out_401 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_401 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_401 = true;

	private uScript_IsPlayerInTriggersMultiple logic_uScript_IsPlayerInTriggersMultiple_uScript_IsPlayerInTriggersMultiple_403 = new uScript_IsPlayerInTriggersMultiple();

	private string[] logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403 = new string[0];

	private bool logic_uScript_IsPlayerInTriggersMultiple_inside_403;

	private bool logic_uScript_IsPlayerInTriggersMultiple_Out_403 = true;

	private bool logic_uScript_IsPlayerInTriggersMultiple_FirstEntered_403 = true;

	private bool logic_uScript_IsPlayerInTriggersMultiple_AllInside_403 = true;

	private bool logic_uScript_IsPlayerInTriggersMultiple_LastExited_403 = true;

	private bool logic_uScript_IsPlayerInTriggersMultiple_AllOutside_403 = true;

	private bool logic_uScript_IsPlayerInTriggersMultiple_SomeInside_403 = true;

	private bool logic_uScript_IsPlayerInTriggersMultiple_SomeOutside_403 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_404 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_404 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_407;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_407 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_407 = "IntroPlayed";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_410 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_410;

	private bool logic_uScriptAct_SetBool_Out_410 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_410 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_410 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_413 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_413;

	private bool logic_uScriptCon_CompareBool_True_413 = true;

	private bool logic_uScriptCon_CompareBool_False_413 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_415 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_415 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_415 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_415;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_415 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_415 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_415 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_415 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_415 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_415 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_415 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_418 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_418;

	private bool logic_uScriptAct_SetBool_Out_418 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_418 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_418 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_421 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_421;

	private int logic_uScript_PlayDialogue_progress_421;

	private bool logic_uScript_PlayDialogue_Out_421 = true;

	private bool logic_uScript_PlayDialogue_Shown_421 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_421 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_423 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_423;

	private bool logic_uScriptCon_CompareBool_True_423 = true;

	private bool logic_uScriptCon_CompareBool_False_423 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_424 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_424 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_427 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_427;

	private int logic_uScriptAct_SetInt_Target_427;

	private bool logic_uScriptAct_SetInt_Out_427 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_429 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_429;

	private bool logic_uScriptAct_SetBool_Out_429 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_429 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_429 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_432;

	private bool logic_uScriptCon_CompareBool_True_432 = true;

	private bool logic_uScriptCon_CompareBool_False_432 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_433 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_433 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_435 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_435 = 4;

	private int logic_uScriptAct_SetInt_Target_435;

	private bool logic_uScriptAct_SetInt_Out_435 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_436 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_436;

	private int logic_uScriptAct_SetInt_Target_436;

	private bool logic_uScriptAct_SetInt_Out_436 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_438 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_438;

	private bool logic_uScriptAct_SetBool_Out_438 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_438 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_438 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_441 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_441 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_441;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_441 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_441;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_441 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_441 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_441 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_441 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_444 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_444;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_444;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_444;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_444;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_444;

	private bool logic_uScript_FlyTechUpAndAway_Out_444 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_445 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_445 = new Tank[0];

	private int logic_uScript_AccessListTech_index_445;

	private Tank logic_uScript_AccessListTech_value_445;

	private bool logic_uScript_AccessListTech_Out_445 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_450 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_450;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_450;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_450;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_450;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_450;

	private bool logic_uScript_FlyTechUpAndAway_Out_450 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_454 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_454 = new Tank[0];

	private int logic_uScript_AccessListTech_index_454 = 1;

	private Tank logic_uScript_AccessListTech_value_454;

	private bool logic_uScript_AccessListTech_Out_454 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_456 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_456 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_457 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_457 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_459 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_459 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_459;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_459 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_459;

	private bool logic_uScript_SpawnTechsFromData_Out_459 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_463 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_463 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_463;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_463 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_463;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_463 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_463 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_463 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_463 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_464 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_464 = new Tank[0];

	private int logic_uScript_AccessListTech_index_464;

	private Tank logic_uScript_AccessListTech_value_464;

	private bool logic_uScript_AccessListTech_Out_464 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_465 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_465 = new Tank[0];

	private int logic_uScript_AccessListTech_index_465 = 1;

	private Tank logic_uScript_AccessListTech_value_465;

	private bool logic_uScript_AccessListTech_Out_465 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_466 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_466 = new Tank[0];

	private int logic_uScript_AccessListTech_index_466;

	private Tank logic_uScript_AccessListTech_value_466;

	private bool logic_uScript_AccessListTech_Out_466 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_467 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_467 = new Tank[0];

	private int logic_uScript_AccessListTech_index_467 = 1;

	private Tank logic_uScript_AccessListTech_value_467;

	private bool logic_uScript_AccessListTech_Out_467 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_468 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_468 = new Tank[0];

	private int logic_uScript_AccessListTech_index_468 = 2;

	private Tank logic_uScript_AccessListTech_value_468;

	private bool logic_uScript_AccessListTech_Out_468 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_469 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_469 = new Tank[0];

	private int logic_uScript_AccessListTech_index_469 = 3;

	private Tank logic_uScript_AccessListTech_value_469;

	private bool logic_uScript_AccessListTech_Out_469 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_470 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_470 = new Tank[0];

	private int logic_uScript_AccessListTech_index_470;

	private Tank logic_uScript_AccessListTech_value_470;

	private bool logic_uScript_AccessListTech_Out_470 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_471 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_471 = new Tank[0];

	private int logic_uScript_AccessListTech_index_471 = 1;

	private Tank logic_uScript_AccessListTech_value_471;

	private bool logic_uScript_AccessListTech_Out_471 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_472 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_472 = new Tank[0];

	private int logic_uScript_AccessListTech_index_472;

	private Tank logic_uScript_AccessListTech_value_472;

	private bool logic_uScript_AccessListTech_Out_472 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_473 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_473 = new Tank[0];

	private int logic_uScript_AccessListTech_index_473 = 1;

	private Tank logic_uScript_AccessListTech_value_473;

	private bool logic_uScript_AccessListTech_Out_473 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_474 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_474 = new Tank[0];

	private int logic_uScript_AccessListTech_index_474 = 2;

	private Tank logic_uScript_AccessListTech_value_474;

	private bool logic_uScript_AccessListTech_Out_474 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_475 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_475 = new Tank[0];

	private int logic_uScript_AccessListTech_index_475 = 3;

	private Tank logic_uScript_AccessListTech_value_475;

	private bool logic_uScript_AccessListTech_Out_475 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_476 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_476 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_476 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_476;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_476 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_476 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_476 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_476 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_476 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_476 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_476 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_479 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_479 = new Tank[0];

	private int logic_uScript_AccessListTech_index_479;

	private Tank logic_uScript_AccessListTech_value_479;

	private bool logic_uScript_AccessListTech_Out_479 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_480;

	private bool logic_uScriptCon_CompareBool_True_480 = true;

	private bool logic_uScriptCon_CompareBool_False_480 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_481 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_481 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_482 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_482 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_483 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_483 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_483;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_483 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_483;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_483 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_483 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_483 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_483 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_485 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_485 = new Tank[0];

	private int logic_uScript_AccessListTech_index_485 = 1;

	private Tank logic_uScript_AccessListTech_value_485;

	private bool logic_uScript_AccessListTech_Out_485 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_490 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_490 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_492 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_492 = new Tank[0];

	private int logic_uScript_AccessListTech_index_492 = 2;

	private Tank logic_uScript_AccessListTech_value_492;

	private bool logic_uScript_AccessListTech_Out_492 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_493 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_493;

	private bool logic_uScriptCon_CompareBool_True_493 = true;

	private bool logic_uScriptCon_CompareBool_False_493 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_494 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_494 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_494;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_494 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_494 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_494 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_496 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_496;

	private bool logic_uScriptAct_SetBool_Out_496 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_496 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_496 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_499 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_499 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_500 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_500 = new Tank[0];

	private int logic_uScript_AccessListTech_index_500 = 3;

	private Tank logic_uScript_AccessListTech_value_500;

	private bool logic_uScript_AccessListTech_Out_500 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_505 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_505 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_505 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_505;

	private bool logic_uScript_DestroyTechsFromData_Out_505 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_508 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_508 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_508 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_508;

	private bool logic_uScript_DestroyTechsFromData_Out_508 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_511 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_511 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_511 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_511;

	private bool logic_uScript_DestroyTechsFromData_Out_511 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_514 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_514 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_514 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_514;

	private bool logic_uScript_DestroyTechsFromData_Out_514 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_516 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_516 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_516 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_516;

	private bool logic_uScript_DestroyTechsFromData_Out_516 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_517 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_517 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_517 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_517;

	private bool logic_uScript_DestroyTechsFromData_Out_517 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_518 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_518 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_518 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_518;

	private bool logic_uScript_DestroyTechsFromData_Out_518 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_524 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_524 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_524 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_524;

	private bool logic_uScript_DestroyTechsFromData_Out_524 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_527 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_527 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_527 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_527 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_529 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_529 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_529;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_529;

	private string logic_uScript_AddOnScreenMessage_tag_529 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_529;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_529;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_529;

	private bool logic_uScript_AddOnScreenMessage_Out_529 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_529 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_533 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_533;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_533 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_533 = "Objective";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_535 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_535;

	private bool logic_uScriptCon_CompareBool_True_535 = true;

	private bool logic_uScriptCon_CompareBool_False_535 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_536 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_536;

	private bool logic_uScriptAct_SetBool_Out_536 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_536 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_536 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_538 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_538;

	private bool logic_uScriptCon_CompareBool_True_538 = true;

	private bool logic_uScriptCon_CompareBool_False_538 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_540 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_540 = new Tank[0];

	private int logic_uScript_AccessListTech_index_540 = 4;

	private Tank logic_uScript_AccessListTech_value_540;

	private bool logic_uScript_AccessListTech_Out_540 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_542 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_542 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_544 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_544 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_544 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_544;

	private bool logic_uScript_DestroyTechsFromData_Out_544 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_547;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_547 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_547 = "EnemyWave01";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_548;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_548 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_548 = "EnemyWave02";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_550;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_550 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_550 = "EnemyWave03";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_552;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_552 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_552 = "EnemyWave04";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_554;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_554 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_554 = "EnemyWave05";

	private Tank event_UnityEngine_GameObject_Tech_408;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
			if (null != owner_Connection_0)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_0.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_0.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_1;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_1;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_1;
				}
			}
		}
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
			if (null != owner_Connection_3)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_3.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_3.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_2;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_2;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_2;
				}
			}
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (null == owner_Connection_62 || !m_RegisteredForEvents)
		{
			owner_Connection_62 = parentGameObject;
		}
		if (null == owner_Connection_69 || !m_RegisteredForEvents)
		{
			owner_Connection_69 = parentGameObject;
		}
		if (null == owner_Connection_80 || !m_RegisteredForEvents)
		{
			owner_Connection_80 = parentGameObject;
		}
		if (null == owner_Connection_88 || !m_RegisteredForEvents)
		{
			owner_Connection_88 = parentGameObject;
		}
		if (null == owner_Connection_101 || !m_RegisteredForEvents)
		{
			owner_Connection_101 = parentGameObject;
		}
		if (null == owner_Connection_168 || !m_RegisteredForEvents)
		{
			owner_Connection_168 = parentGameObject;
		}
		if (null == owner_Connection_174 || !m_RegisteredForEvents)
		{
			owner_Connection_174 = parentGameObject;
		}
		if (null == owner_Connection_184 || !m_RegisteredForEvents)
		{
			owner_Connection_184 = parentGameObject;
		}
		if (null == owner_Connection_191 || !m_RegisteredForEvents)
		{
			owner_Connection_191 = parentGameObject;
		}
		if (null == owner_Connection_199 || !m_RegisteredForEvents)
		{
			owner_Connection_199 = parentGameObject;
		}
		if (null == owner_Connection_227 || !m_RegisteredForEvents)
		{
			owner_Connection_227 = parentGameObject;
		}
		if (null == owner_Connection_294 || !m_RegisteredForEvents)
		{
			owner_Connection_294 = parentGameObject;
		}
		if (null == owner_Connection_412 || !m_RegisteredForEvents)
		{
			owner_Connection_412 = parentGameObject;
			if (null != owner_Connection_412)
			{
				uScript_PlayerTechDestroyedEvent uScript_PlayerTechDestroyedEvent2 = owner_Connection_412.GetComponent<uScript_PlayerTechDestroyedEvent>();
				if (null == uScript_PlayerTechDestroyedEvent2)
				{
					uScript_PlayerTechDestroyedEvent2 = owner_Connection_412.AddComponent<uScript_PlayerTechDestroyedEvent>();
				}
				if (null != uScript_PlayerTechDestroyedEvent2)
				{
					uScript_PlayerTechDestroyedEvent2.TechDestroyedEvent += Instance_TechDestroyedEvent_408;
				}
			}
		}
		if (null == owner_Connection_455 || !m_RegisteredForEvents)
		{
			owner_Connection_455 = parentGameObject;
		}
		if (null == owner_Connection_460 || !m_RegisteredForEvents)
		{
			owner_Connection_460 = parentGameObject;
		}
		if (null == owner_Connection_462 || !m_RegisteredForEvents)
		{
			owner_Connection_462 = parentGameObject;
		}
		if (null == owner_Connection_486 || !m_RegisteredForEvents)
		{
			owner_Connection_486 = parentGameObject;
		}
		if (null == owner_Connection_495 || !m_RegisteredForEvents)
		{
			owner_Connection_495 = parentGameObject;
		}
		if (null == owner_Connection_504 || !m_RegisteredForEvents)
		{
			owner_Connection_504 = parentGameObject;
		}
		if (null == owner_Connection_507 || !m_RegisteredForEvents)
		{
			owner_Connection_507 = parentGameObject;
		}
		if (null == owner_Connection_510 || !m_RegisteredForEvents)
		{
			owner_Connection_510 = parentGameObject;
		}
		if (null == owner_Connection_513 || !m_RegisteredForEvents)
		{
			owner_Connection_513 = parentGameObject;
		}
		if (null == owner_Connection_515 || !m_RegisteredForEvents)
		{
			owner_Connection_515 = parentGameObject;
		}
		if (null == owner_Connection_519 || !m_RegisteredForEvents)
		{
			owner_Connection_519 = parentGameObject;
		}
		if (null == owner_Connection_523 || !m_RegisteredForEvents)
		{
			owner_Connection_523 = parentGameObject;
		}
		if (null == owner_Connection_526 || !m_RegisteredForEvents)
		{
			owner_Connection_526 = parentGameObject;
		}
		if (null == owner_Connection_543 || !m_RegisteredForEvents)
		{
			owner_Connection_543 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_0)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_0.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_0.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_1;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_1;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_1;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_3)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_3.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_3.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_2;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_2;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_2;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_412)
		{
			uScript_PlayerTechDestroyedEvent uScript_PlayerTechDestroyedEvent2 = owner_Connection_412.GetComponent<uScript_PlayerTechDestroyedEvent>();
			if (null == uScript_PlayerTechDestroyedEvent2)
			{
				uScript_PlayerTechDestroyedEvent2 = owner_Connection_412.AddComponent<uScript_PlayerTechDestroyedEvent>();
			}
			if (null != uScript_PlayerTechDestroyedEvent2)
			{
				uScript_PlayerTechDestroyedEvent2.TechDestroyedEvent += Instance_TechDestroyedEvent_408;
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
			uScript_SaveLoad component = owner_Connection_0.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_1;
				component.LoadEvent -= Instance_LoadEvent_1;
				component.RestartEvent -= Instance_RestartEvent_1;
			}
		}
		if (null != owner_Connection_3)
		{
			uScript_EncounterUpdate component2 = owner_Connection_3.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_2;
				component2.OnSuspend -= Instance_OnSuspend_2;
				component2.OnResume -= Instance_OnResume_2;
			}
		}
		if (null != owner_Connection_412)
		{
			uScript_PlayerTechDestroyedEvent component3 = owner_Connection_412.GetComponent<uScript_PlayerTechDestroyedEvent>();
			if (null != component3)
			{
				component3.TechDestroyedEvent -= Instance_TechDestroyedEvent_408;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_8.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_10.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_11.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_17.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_21.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_22.SetParent(g);
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_31.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_33.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_34.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_35.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_40.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_42.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_43.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_45.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_46.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_50.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_52.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_57.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_59.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_60.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_61.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_63.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_67.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_73.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_76.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_77.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_79.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_83.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_91.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_92.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_99.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_102.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_104.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.SetParent(g);
		logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_109.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_111.SetParent(g);
		logic_uScript_IsTechFriendlyToPlayer_uScript_IsTechFriendlyToPlayer_113.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_116.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_121.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_123.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_125.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_127.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_129.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_131.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_133.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_140.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_142.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_146.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_148.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_150.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_151.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_154.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_156.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_158.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_159.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_166.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_169.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_172.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_173.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_178.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_180.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_182.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_188.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_189.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_193.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_204.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_205.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_206.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_212.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_215.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_216.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_218.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_219.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_220.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_223.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_228.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_231.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_234.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_235.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_238.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_239.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_240.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_241.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_243.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_245.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_246.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_249.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_251.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_256.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_260.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_266.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_269.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_270.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_272.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_273.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_276.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_278.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_281.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_285.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_290.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_292.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_293.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_298.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_301.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_302.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_305.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_306.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_309.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_311.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_312.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_316.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_317.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_321.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_323.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_324.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_329.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_334.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_335.SetParent(g);
		logic_uScriptAct_PrintText_uScriptAct_PrintText_340.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_344.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_346.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_348.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_360.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_363.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_365.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_366.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_368.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_369.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_370.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_373.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_381.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_382.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_385.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_387.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_390.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_391.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_393.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_394.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_396.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_397.SetParent(g);
		logic_uScript_CheckTechFlightBlocks_uScript_CheckTechFlightBlocks_400.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_401.SetParent(g);
		logic_uScript_IsPlayerInTriggersMultiple_uScript_IsPlayerInTriggersMultiple_403.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_404.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_410.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_413.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_415.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_418.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_421.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_423.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_424.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_427.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_429.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_433.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_435.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_436.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_438.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_441.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_444.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_445.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_450.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_454.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_456.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_457.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_459.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_463.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_464.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_465.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_466.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_467.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_468.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_469.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_470.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_471.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_472.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_473.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_474.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_475.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_476.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_479.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_481.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_482.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_483.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_485.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_490.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_492.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_493.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_494.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_496.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_499.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_500.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_505.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_508.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_511.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_514.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_516.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_517.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_518.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_524.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_527.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_529.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_535.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_536.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_538.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_540.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_542.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_544.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_62 = parentGameObject;
		owner_Connection_69 = parentGameObject;
		owner_Connection_80 = parentGameObject;
		owner_Connection_88 = parentGameObject;
		owner_Connection_101 = parentGameObject;
		owner_Connection_168 = parentGameObject;
		owner_Connection_174 = parentGameObject;
		owner_Connection_184 = parentGameObject;
		owner_Connection_191 = parentGameObject;
		owner_Connection_199 = parentGameObject;
		owner_Connection_227 = parentGameObject;
		owner_Connection_294 = parentGameObject;
		owner_Connection_412 = parentGameObject;
		owner_Connection_455 = parentGameObject;
		owner_Connection_460 = parentGameObject;
		owner_Connection_462 = parentGameObject;
		owner_Connection_486 = parentGameObject;
		owner_Connection_495 = parentGameObject;
		owner_Connection_504 = parentGameObject;
		owner_Connection_507 = parentGameObject;
		owner_Connection_510 = parentGameObject;
		owner_Connection_513 = parentGameObject;
		owner_Connection_515 = parentGameObject;
		owner_Connection_519 = parentGameObject;
		owner_Connection_523 = parentGameObject;
		owner_Connection_526 = parentGameObject;
		owner_Connection_543 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_273.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_276.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_285.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_391.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Save_Out += SubGraph_SaveLoadInt_Save_Out_6;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Load_Out += SubGraph_SaveLoadInt_Load_Out_6;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output1 += uScriptCon_BigManualSwitch_Output1_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output2 += uScriptCon_BigManualSwitch_Output2_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output3 += uScriptCon_BigManualSwitch_Output3_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output4 += uScriptCon_BigManualSwitch_Output4_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output5 += uScriptCon_BigManualSwitch_Output5_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output6 += uScriptCon_BigManualSwitch_Output6_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output7 += uScriptCon_BigManualSwitch_Output7_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output8 += uScriptCon_BigManualSwitch_Output8_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output9 += uScriptCon_BigManualSwitch_Output9_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output10 += uScriptCon_BigManualSwitch_Output10_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output11 += uScriptCon_BigManualSwitch_Output11_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output12 += uScriptCon_BigManualSwitch_Output12_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output13 += uScriptCon_BigManualSwitch_Output13_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output14 += uScriptCon_BigManualSwitch_Output14_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output15 += uScriptCon_BigManualSwitch_Output15_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output16 += uScriptCon_BigManualSwitch_Output16_24;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_273.Out += SubGraph_LoadObjectiveStates_Out_273;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_276.Out += SubGraph_CompleteObjectiveStage_Out_276;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.Out += SubGraph_CompleteObjectiveStage_Out_282;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_285.Out += SubGraph_CompleteObjectiveStage_Out_285;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.Save_Out += SubGraph_SaveLoadInt_Save_Out_297;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.Load_Out += SubGraph_SaveLoadInt_Load_Out_297;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_297;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_391.Out += SubGraph_CompleteObjectiveStage_Out_391;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Save_Out += SubGraph_SaveLoadBool_Save_Out_407;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Load_Out += SubGraph_SaveLoadBool_Load_Out_407;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_407;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.Save_Out += SubGraph_SaveLoadInt_Save_Out_533;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.Load_Out += SubGraph_SaveLoadInt_Load_Out_533;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_533;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Save_Out += SubGraph_SaveLoadBool_Save_Out_547;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Load_Out += SubGraph_SaveLoadBool_Load_Out_547;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_547;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Save_Out += SubGraph_SaveLoadBool_Save_Out_548;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Load_Out += SubGraph_SaveLoadBool_Load_Out_548;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_548;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Save_Out += SubGraph_SaveLoadBool_Save_Out_550;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Load_Out += SubGraph_SaveLoadBool_Load_Out_550;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_550;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Save_Out += SubGraph_SaveLoadBool_Save_Out_552;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Load_Out += SubGraph_SaveLoadBool_Load_Out_552;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_552;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Save_Out += SubGraph_SaveLoadBool_Save_Out_554;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Load_Out += SubGraph_SaveLoadBool_Load_Out_554;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_554;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_273.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_276.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_285.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_391.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_11.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_21.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_50.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_57.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_60.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_104.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_146.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_156.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_246.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_273.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_276.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_285.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_370.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_391.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_394.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_421.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_444.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_450.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_8.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_21.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_50.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_57.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_104.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_146.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_156.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_246.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_256.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_266.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_273.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_276.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_285.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_370.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_391.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_421.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_529.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_273.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_276.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_285.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_391.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_273.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_276.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_285.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_391.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Save_Out -= SubGraph_SaveLoadInt_Save_Out_6;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Load_Out -= SubGraph_SaveLoadInt_Load_Out_6;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output1 -= uScriptCon_BigManualSwitch_Output1_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output2 -= uScriptCon_BigManualSwitch_Output2_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output3 -= uScriptCon_BigManualSwitch_Output3_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output4 -= uScriptCon_BigManualSwitch_Output4_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output5 -= uScriptCon_BigManualSwitch_Output5_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output6 -= uScriptCon_BigManualSwitch_Output6_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output7 -= uScriptCon_BigManualSwitch_Output7_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output8 -= uScriptCon_BigManualSwitch_Output8_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output9 -= uScriptCon_BigManualSwitch_Output9_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output10 -= uScriptCon_BigManualSwitch_Output10_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output11 -= uScriptCon_BigManualSwitch_Output11_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output12 -= uScriptCon_BigManualSwitch_Output12_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output13 -= uScriptCon_BigManualSwitch_Output13_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output14 -= uScriptCon_BigManualSwitch_Output14_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output15 -= uScriptCon_BigManualSwitch_Output15_24;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.Output16 -= uScriptCon_BigManualSwitch_Output16_24;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_273.Out -= SubGraph_LoadObjectiveStates_Out_273;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_276.Out -= SubGraph_CompleteObjectiveStage_Out_276;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.Out -= SubGraph_CompleteObjectiveStage_Out_282;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_285.Out -= SubGraph_CompleteObjectiveStage_Out_285;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.Save_Out -= SubGraph_SaveLoadInt_Save_Out_297;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.Load_Out -= SubGraph_SaveLoadInt_Load_Out_297;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_297;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_391.Out -= SubGraph_CompleteObjectiveStage_Out_391;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Save_Out -= SubGraph_SaveLoadBool_Save_Out_407;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Load_Out -= SubGraph_SaveLoadBool_Load_Out_407;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_407;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.Save_Out -= SubGraph_SaveLoadInt_Save_Out_533;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.Load_Out -= SubGraph_SaveLoadInt_Load_Out_533;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_533;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Save_Out -= SubGraph_SaveLoadBool_Save_Out_547;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Load_Out -= SubGraph_SaveLoadBool_Load_Out_547;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_547;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Save_Out -= SubGraph_SaveLoadBool_Save_Out_548;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Load_Out -= SubGraph_SaveLoadBool_Load_Out_548;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_548;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Save_Out -= SubGraph_SaveLoadBool_Save_Out_550;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Load_Out -= SubGraph_SaveLoadBool_Load_Out_550;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_550;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Save_Out -= SubGraph_SaveLoadBool_Save_Out_552;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Load_Out -= SubGraph_SaveLoadBool_Load_Out_552;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_552;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Save_Out -= SubGraph_SaveLoadBool_Save_Out_554;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Load_Out -= SubGraph_SaveLoadBool_Load_Out_554;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_554;
	}

	public void OnGUI()
	{
		logic_uScriptAct_PrintText_uScriptAct_PrintText_340.OnGUI();
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

	private void Instance_OnUpdate_2(object o, EventArgs e)
	{
		Relay_OnUpdate_2();
	}

	private void Instance_OnSuspend_2(object o, EventArgs e)
	{
		Relay_OnSuspend_2();
	}

	private void Instance_OnResume_2(object o, EventArgs e)
	{
		Relay_OnResume_2();
	}

	private void Instance_TechDestroyedEvent_408(object o, uScript_PlayerTechDestroyedEvent.TechDestroyedEventArgs e)
	{
		event_UnityEngine_GameObject_Tech_408 = e.Tech;
		Relay_TechDestroyedEvent_408();
	}

	private void SubGraph_SaveLoadInt_Save_Out_6(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_6 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_6;
		Relay_Save_Out_6();
	}

	private void SubGraph_SaveLoadInt_Load_Out_6(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_6 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_6;
		Relay_Load_Out_6();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_6(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_6 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_6;
		Relay_Restart_Out_6();
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

	private void uScriptCon_BigManualSwitch_Output1_24(object o, EventArgs e)
	{
		Relay_Output1_24();
	}

	private void uScriptCon_BigManualSwitch_Output2_24(object o, EventArgs e)
	{
		Relay_Output2_24();
	}

	private void uScriptCon_BigManualSwitch_Output3_24(object o, EventArgs e)
	{
		Relay_Output3_24();
	}

	private void uScriptCon_BigManualSwitch_Output4_24(object o, EventArgs e)
	{
		Relay_Output4_24();
	}

	private void uScriptCon_BigManualSwitch_Output5_24(object o, EventArgs e)
	{
		Relay_Output5_24();
	}

	private void uScriptCon_BigManualSwitch_Output6_24(object o, EventArgs e)
	{
		Relay_Output6_24();
	}

	private void uScriptCon_BigManualSwitch_Output7_24(object o, EventArgs e)
	{
		Relay_Output7_24();
	}

	private void uScriptCon_BigManualSwitch_Output8_24(object o, EventArgs e)
	{
		Relay_Output8_24();
	}

	private void uScriptCon_BigManualSwitch_Output9_24(object o, EventArgs e)
	{
		Relay_Output9_24();
	}

	private void uScriptCon_BigManualSwitch_Output10_24(object o, EventArgs e)
	{
		Relay_Output10_24();
	}

	private void uScriptCon_BigManualSwitch_Output11_24(object o, EventArgs e)
	{
		Relay_Output11_24();
	}

	private void uScriptCon_BigManualSwitch_Output12_24(object o, EventArgs e)
	{
		Relay_Output12_24();
	}

	private void uScriptCon_BigManualSwitch_Output13_24(object o, EventArgs e)
	{
		Relay_Output13_24();
	}

	private void uScriptCon_BigManualSwitch_Output14_24(object o, EventArgs e)
	{
		Relay_Output14_24();
	}

	private void uScriptCon_BigManualSwitch_Output15_24(object o, EventArgs e)
	{
		Relay_Output15_24();
	}

	private void uScriptCon_BigManualSwitch_Output16_24(object o, EventArgs e)
	{
		Relay_Output16_24();
	}

	private void SubGraph_LoadObjectiveStates_Out_273(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_273();
	}

	private void SubGraph_CompleteObjectiveStage_Out_276(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_276 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_276;
		Relay_Out_276();
	}

	private void SubGraph_CompleteObjectiveStage_Out_282(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_282 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_282;
		Relay_Out_282();
	}

	private void SubGraph_CompleteObjectiveStage_Out_285(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_285 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_285;
		Relay_Out_285();
	}

	private void SubGraph_SaveLoadInt_Save_Out_297(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_297 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_297;
		Relay_Save_Out_297();
	}

	private void SubGraph_SaveLoadInt_Load_Out_297(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_297 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_297;
		Relay_Load_Out_297();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_297(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_297 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_297;
		Relay_Restart_Out_297();
	}

	private void SubGraph_CompleteObjectiveStage_Out_391(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_391 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_391;
		Relay_Out_391();
	}

	private void SubGraph_SaveLoadBool_Save_Out_407(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = e.boolean;
		local_IntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_407;
		Relay_Save_Out_407();
	}

	private void SubGraph_SaveLoadBool_Load_Out_407(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = e.boolean;
		local_IntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_407;
		Relay_Load_Out_407();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_407(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = e.boolean;
		local_IntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_407;
		Relay_Restart_Out_407();
	}

	private void SubGraph_SaveLoadInt_Save_Out_533(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_533 = e.integer;
		local_Objective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_533;
		Relay_Save_Out_533();
	}

	private void SubGraph_SaveLoadInt_Load_Out_533(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_533 = e.integer;
		local_Objective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_533;
		Relay_Load_Out_533();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_533(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_533 = e.integer;
		local_Objective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_533;
		Relay_Restart_Out_533();
	}

	private void SubGraph_SaveLoadBool_Save_Out_547(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_547 = e.boolean;
		local_EnemyWave01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_547;
		Relay_Save_Out_547();
	}

	private void SubGraph_SaveLoadBool_Load_Out_547(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_547 = e.boolean;
		local_EnemyWave01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_547;
		Relay_Load_Out_547();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_547(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_547 = e.boolean;
		local_EnemyWave01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_547;
		Relay_Restart_Out_547();
	}

	private void SubGraph_SaveLoadBool_Save_Out_548(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_548 = e.boolean;
		local_EnemyWave02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_548;
		Relay_Save_Out_548();
	}

	private void SubGraph_SaveLoadBool_Load_Out_548(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_548 = e.boolean;
		local_EnemyWave02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_548;
		Relay_Load_Out_548();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_548(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_548 = e.boolean;
		local_EnemyWave02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_548;
		Relay_Restart_Out_548();
	}

	private void SubGraph_SaveLoadBool_Save_Out_550(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_550 = e.boolean;
		local_EnemyWave03_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_550;
		Relay_Save_Out_550();
	}

	private void SubGraph_SaveLoadBool_Load_Out_550(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_550 = e.boolean;
		local_EnemyWave03_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_550;
		Relay_Load_Out_550();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_550(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_550 = e.boolean;
		local_EnemyWave03_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_550;
		Relay_Restart_Out_550();
	}

	private void SubGraph_SaveLoadBool_Save_Out_552(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_552 = e.boolean;
		local_EnemyWave04_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_552;
		Relay_Save_Out_552();
	}

	private void SubGraph_SaveLoadBool_Load_Out_552(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_552 = e.boolean;
		local_EnemyWave04_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_552;
		Relay_Load_Out_552();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_552(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_552 = e.boolean;
		local_EnemyWave04_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_552;
		Relay_Restart_Out_552();
	}

	private void SubGraph_SaveLoadBool_Save_Out_554(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_554 = e.boolean;
		local_EnemyWave05_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_554;
		Relay_Save_Out_554();
	}

	private void SubGraph_SaveLoadBool_Load_Out_554(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_554 = e.boolean;
		local_EnemyWave05_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_554;
		Relay_Load_Out_554();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_554(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_554 = e.boolean;
		local_EnemyWave05_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_554;
		Relay_Restart_Out_554();
	}

	private void Relay_SaveEvent_1()
	{
		Relay_Save_7();
	}

	private void Relay_LoadEvent_1()
	{
		Relay_Load_7();
	}

	private void Relay_RestartEvent_1()
	{
		Relay_Set_False_7();
	}

	private void Relay_OnUpdate_2()
	{
		Relay_In_8();
	}

	private void Relay_OnSuspend_2()
	{
	}

	private void Relay_OnResume_2()
	{
	}

	private void Relay_Save_Out_6()
	{
		Relay_Save_297();
	}

	private void Relay_Load_Out_6()
	{
		Relay_Load_297();
	}

	private void Relay_Restart_Out_6()
	{
		Relay_Restart_297();
	}

	private void Relay_Save_6()
	{
		logic_SubGraph_SaveLoadInt_integer_6 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_6 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Save(logic_SubGraph_SaveLoadInt_restartValue_6, ref logic_SubGraph_SaveLoadInt_integer_6, logic_SubGraph_SaveLoadInt_intAsVariable_6, logic_SubGraph_SaveLoadInt_uniqueID_6);
	}

	private void Relay_Load_6()
	{
		logic_SubGraph_SaveLoadInt_integer_6 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_6 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Load(logic_SubGraph_SaveLoadInt_restartValue_6, ref logic_SubGraph_SaveLoadInt_integer_6, logic_SubGraph_SaveLoadInt_intAsVariable_6, logic_SubGraph_SaveLoadInt_uniqueID_6);
	}

	private void Relay_Restart_6()
	{
		logic_SubGraph_SaveLoadInt_integer_6 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_6 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Restart(logic_SubGraph_SaveLoadInt_restartValue_6, ref logic_SubGraph_SaveLoadInt_integer_6, logic_SubGraph_SaveLoadInt_intAsVariable_6, logic_SubGraph_SaveLoadInt_uniqueID_6);
	}

	private void Relay_Save_Out_7()
	{
		Relay_Save_407();
	}

	private void Relay_Load_Out_7()
	{
		Relay_Load_407();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_Set_False_407();
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

	private void Relay_In_8()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_8 = owner_Connection_9;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_8.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_8);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_8.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_8.False;
		if (num)
		{
			Relay_Pause_10();
		}
		if (flag)
		{
			Relay_UnPause_10();
		}
	}

	private void Relay_Pause_10()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_10.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_10.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_UnPause_10()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_10.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_10.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_11 = owner_Connection_19;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_11.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_11);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_11.Out)
		{
			Relay_InitialSpawn_18();
		}
	}

	private void Relay_True_13()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.True(out logic_uScriptAct_SetBool_Target_13);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_13;
	}

	private void Relay_False_13()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.False(out logic_uScriptAct_SetBool_Target_13);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_13;
	}

	private void Relay_In_14()
	{
		logic_uScriptCon_CompareBool_Bool_14 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.In(logic_uScriptCon_CompareBool_Bool_14);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.False;
		if (num)
		{
			Relay_In_39();
		}
		if (flag)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_17()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_17 = IntroTrigger;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_17 = OutsideIntroTrigger;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_17.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_17, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_17, ref logic_uScript_IsPlayerInTriggerSmart_inside_17);
		bool lastExited = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_17.LastExited;
		bool someInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_17.SomeInside;
		if (lastExited)
		{
			Relay_In_538();
		}
		if (someInside)
		{
			Relay_In_535();
		}
	}

	private void Relay_InitialSpawn_18()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_18.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_18, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_18, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_18 = owner_Connection_20;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_18, logic_uScript_SpawnTechsFromData_ownerNode_18, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_18, logic_uScript_SpawnTechsFromData_allowResurrection_18);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18.Out)
		{
			Relay_In_272();
		}
	}

	private void Relay_In_21()
	{
		logic_uScript_PlayDialogue_dialogue_21 = IntroDialogue;
		logic_uScript_PlayDialogue_progress_21 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_21.In(logic_uScript_PlayDialogue_dialogue_21, ref logic_uScript_PlayDialogue_progress_21);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_21;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_21.Shown)
		{
			Relay_In_278();
		}
	}

	private void Relay_In_22()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_22.In(logic_uScriptAct_SetInt_Value_22, out logic_uScriptAct_SetInt_Target_22);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_22;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_22.Out)
		{
			Relay_In_121();
		}
	}

	private void Relay_Output1_24()
	{
		Relay_In_17();
	}

	private void Relay_Output2_24()
	{
		Relay_In_40();
	}

	private void Relay_Output3_24()
	{
		Relay_In_403();
	}

	private void Relay_Output4_24()
	{
		Relay_In_413();
	}

	private void Relay_Output5_24()
	{
		Relay_In_542();
	}

	private void Relay_Output6_24()
	{
		Relay_In_57();
	}

	private void Relay_Output7_24()
	{
		Relay_In_63();
	}

	private void Relay_Output8_24()
	{
		Relay_In_285();
	}

	private void Relay_Output9_24()
	{
	}

	private void Relay_Output10_24()
	{
	}

	private void Relay_Output11_24()
	{
	}

	private void Relay_Output12_24()
	{
	}

	private void Relay_Output13_24()
	{
	}

	private void Relay_Output14_24()
	{
	}

	private void Relay_Output15_24()
	{
	}

	private void Relay_Output16_24()
	{
	}

	private void Relay_In_24()
	{
		logic_uScriptCon_BigManualSwitch_CurrentOutput_24 = local_Stage_System_Int32;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_24.In(logic_uScriptCon_BigManualSwitch_CurrentOutput_24);
	}

	private void Relay_In_31()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_31.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_31, num + 1);
		}
		logic_uScriptAct_Concatenate_A_31[num++] = local_328_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_31.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_31, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_31[num2++] = local_InsideMissionArea_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_31.In(logic_uScriptAct_Concatenate_A_31, logic_uScriptAct_Concatenate_B_31, logic_uScriptAct_Concatenate_Separator_31, out logic_uScriptAct_Concatenate_Result_31);
		local_32_System_String = logic_uScriptAct_Concatenate_Result_31;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_31.Out)
		{
			Relay_In_334();
		}
	}

	private void Relay_In_33()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_33.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_33, num + 1);
		}
		logic_uScriptAct_Concatenate_A_33[num++] = local_30_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_33.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_33, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_33[num2++] = local_Stage_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_33.In(logic_uScriptAct_Concatenate_A_33, logic_uScriptAct_Concatenate_B_33, logic_uScriptAct_Concatenate_Separator_33, out logic_uScriptAct_Concatenate_Result_33);
		local_37_System_String = logic_uScriptAct_Concatenate_Result_33;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_33.Out)
		{
			Relay_In_329();
		}
	}

	private void Relay_In_34()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_34.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_34, num + 1);
		}
		logic_uScriptAct_Concatenate_A_34[num++] = local_29_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_34.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_34, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_34[num2++] = local_LevelArea_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_34.In(logic_uScriptAct_Concatenate_A_34, logic_uScriptAct_Concatenate_B_34, logic_uScriptAct_Concatenate_Separator_34, out logic_uScriptAct_Concatenate_Result_34);
		local_28_System_String = logic_uScriptAct_Concatenate_Result_34;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_34.Out)
		{
			Relay_In_312();
		}
	}

	private void Relay_In_35()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_35.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_35, num + 1);
		}
		logic_uScriptAct_Concatenate_A_35[num++] = local_330_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_35.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_35, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_35[num2++] = local_36_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_35.In(logic_uScriptAct_Concatenate_A_35, logic_uScriptAct_Concatenate_B_35, logic_uScriptAct_Concatenate_Separator_35, out logic_uScriptAct_Concatenate_Result_35);
		local_29_System_String = logic_uScriptAct_Concatenate_Result_35;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_35.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_39()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_40()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_40 = StartLineTrigger;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_40 = StartLineTrigger;
		logic_uScript_IsPlayerInTriggerSmart_inside_40 = local_inStartTrigger_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_40.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_40, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_40, ref logic_uScript_IsPlayerInTriggerSmart_inside_40);
		local_inStartTrigger_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_40;
		if (logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_40.AllInside)
		{
			Relay_In_255();
		}
	}

	private void Relay_In_42()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_42.In(logic_uScriptAct_SetInt_Value_42, out logic_uScriptAct_SetInt_Target_42);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_42;
	}

	private void Relay_In_43()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_43 = Level01Area;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_43 = Level01Area;
		logic_uScript_IsPlayerInTriggerSmart_inside_43 = local_isinLevel01Area_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_43.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_43, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_43, ref logic_uScript_IsPlayerInTriggerSmart_inside_43);
		local_isinLevel01Area_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_43;
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_43.Out;
		bool someInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_43.SomeInside;
		if (num)
		{
			Relay_In_208();
		}
		if (someInside)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_45()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_45.In(logic_uScriptAct_SetInt_Value_45, out logic_uScriptAct_SetInt_Target_45);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_45;
	}

	private void Relay_In_46()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_46 = FinishTriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_46 = FinishTriggerArea;
		logic_uScript_IsPlayerInTriggerSmart_inside_46 = local_isinFinishArea_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_46.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_46, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_46, ref logic_uScript_IsPlayerInTriggerSmart_inside_46);
		local_isinFinishArea_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_46;
		if (logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_46.SomeInside)
		{
			Relay_In_305();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_PlayDialogue_dialogue_50 = FailDialogue;
		logic_uScript_PlayDialogue_progress_50 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_50.In(logic_uScript_PlayDialogue_dialogue_50, ref logic_uScript_PlayDialogue_progress_50);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_50;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_50.Shown)
		{
			Relay_False_93();
		}
	}

	private void Relay_In_52()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_52.In(logic_uScriptAct_SetInt_Value_52, out logic_uScriptAct_SetInt_Target_52);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_52;
	}

	private void Relay_In_57()
	{
		logic_uScript_PlayDialogue_dialogue_57 = MissionCompleteDialogue;
		logic_uScript_PlayDialogue_progress_57 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_57.In(logic_uScript_PlayDialogue_dialogue_57, ref logic_uScript_PlayDialogue_progress_57);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_57;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_57.Shown)
		{
			Relay_In_235();
		}
	}

	private void Relay_In_59()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_59.In(logic_uScriptAct_SetInt_Value_59, out logic_uScriptAct_SetInt_Target_59);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_59;
	}

	private void Relay_In_60()
	{
		logic_uScript_FlyTechUpAndAway_tech_60 = local_NPCEndTech01_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_60 = AIFlyTree;
		logic_uScript_FlyTechUpAndAway_removalParticles_60 = RemovalParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_60.In(logic_uScript_FlyTechUpAndAway_tech_60, logic_uScript_FlyTechUpAndAway_maxLifetime_60, logic_uScript_FlyTechUpAndAway_targetHeight_60, logic_uScript_FlyTechUpAndAway_aiTree_60, logic_uScript_FlyTechUpAndAway_removalParticles_60);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_60.Out)
		{
			Relay_In_394();
		}
	}

	private void Relay_Succeed_61()
	{
		logic_uScript_FinishEncounter_owner_61 = owner_Connection_62;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_61.Succeed(logic_uScript_FinishEncounter_owner_61);
	}

	private void Relay_Fail_61()
	{
		logic_uScript_FinishEncounter_owner_61 = owner_Connection_62;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_61.Fail(logic_uScript_FinishEncounter_owner_61);
	}

	private void Relay_In_63()
	{
		int num = 0;
		Array nPCTechEnd01Data = NPCTechEnd01Data;
		if (logic_uScript_GetAndCheckTechs_techData_63.Length != num + nPCTechEnd01Data.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_63, num + nPCTechEnd01Data.Length);
		}
		Array.Copy(nPCTechEnd01Data, 0, logic_uScript_GetAndCheckTechs_techData_63, num, nPCTechEnd01Data.Length);
		num += nPCTechEnd01Data.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_63 = owner_Connection_69;
		int num2 = 0;
		Array array = local_NPCTechsEnd01_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_63.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_63, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_63, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_63 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_63.In(logic_uScript_GetAndCheckTechs_techData_63, logic_uScript_GetAndCheckTechs_ownerNode_63, ref logic_uScript_GetAndCheckTechs_techs_63);
		local_NPCTechsEnd01_TankArray = logic_uScript_GetAndCheckTechs_techs_63;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_63.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_63.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_63.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_67();
		}
		if (someAlive)
		{
			Relay_AtIndex_67();
		}
		if (allDead)
		{
			Relay_In_239();
		}
	}

	private void Relay_AtIndex_67()
	{
		int num = 0;
		Array array = local_NPCTechsEnd01_TankArray;
		if (logic_uScript_AccessListTech_techList_67.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_67, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_67, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_67.AtIndex(ref logic_uScript_AccessListTech_techList_67, logic_uScript_AccessListTech_index_67, out logic_uScript_AccessListTech_value_67);
		local_NPCTechsEnd01_TankArray = logic_uScript_AccessListTech_techList_67;
		local_NPCEndTech01_Tank = logic_uScript_AccessListTech_value_67;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_67.Out)
		{
			Relay_AtIndex_396();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_73 = EnemySpawnTrigger01;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_73 = EnemySpawnTrigger01;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_73.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_73, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_73, ref logic_uScript_IsPlayerInTriggerSmart_inside_73);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_73.Out;
		bool firstEntered = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_73.FirstEntered;
		if (num)
		{
			Relay_In_92();
		}
		if (firstEntered)
		{
			Relay_In_77();
		}
	}

	private void Relay_InitialSpawn_76()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData01;
		if (logic_uScript_SpawnTechsFromData_spawnData_76.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_76, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_SpawnTechsFromData_spawnData_76, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_76 = owner_Connection_80;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_76.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_76, logic_uScript_SpawnTechsFromData_ownerNode_76, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_76, logic_uScript_SpawnTechsFromData_allowResurrection_76);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_76.Out)
		{
			Relay_True_79();
		}
	}

	private void Relay_In_77()
	{
		logic_uScriptCon_CompareBool_Bool_77 = local_EnemyWave01_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_77.In(logic_uScriptCon_CompareBool_Bool_77);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_77.False)
		{
			Relay_InitialSpawn_76();
		}
	}

	private void Relay_True_79()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_79.True(out logic_uScriptAct_SetBool_Target_79);
		local_EnemyWave01_System_Boolean = logic_uScriptAct_SetBool_Target_79;
	}

	private void Relay_False_79()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_79.False(out logic_uScriptAct_SetBool_Target_79);
		local_EnemyWave01_System_Boolean = logic_uScriptAct_SetBool_Target_79;
	}

	private void Relay_True_83()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_83.True(out logic_uScriptAct_SetBool_Target_83);
		local_EnemyWave03_System_Boolean = logic_uScriptAct_SetBool_Target_83;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_83.Out)
		{
			Relay_In_493();
		}
	}

	private void Relay_False_83()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_83.False(out logic_uScriptAct_SetBool_Target_83);
		local_EnemyWave03_System_Boolean = logic_uScriptAct_SetBool_Target_83;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_83.Out)
		{
			Relay_In_493();
		}
	}

	private void Relay_In_84()
	{
		logic_uScriptCon_CompareBool_Bool_84 = local_EnemyWave03_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.In(logic_uScriptCon_CompareBool_Bool_84);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.False;
		if (num)
		{
			Relay_In_499();
		}
		if (flag)
		{
			Relay_InitialSpawn_166();
		}
	}

	private void Relay_True_86()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.True(out logic_uScriptAct_SetBool_Target_86);
		local_EnemyWave02_System_Boolean = logic_uScriptAct_SetBool_Target_86;
	}

	private void Relay_False_86()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.False(out logic_uScriptAct_SetBool_Target_86);
		local_EnemyWave02_System_Boolean = logic_uScriptAct_SetBool_Target_86;
	}

	private void Relay_In_89()
	{
		logic_uScriptCon_CompareBool_Bool_89 = local_EnemyWave02_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89.In(logic_uScriptCon_CompareBool_Bool_89);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89.False)
		{
			Relay_InitialSpawn_91();
		}
	}

	private void Relay_InitialSpawn_91()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData02;
		if (logic_uScript_SpawnTechsFromData_spawnData_91.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_91, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_SpawnTechsFromData_spawnData_91, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_91 = owner_Connection_88;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_91.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_91, logic_uScript_SpawnTechsFromData_ownerNode_91, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_91, logic_uScript_SpawnTechsFromData_allowResurrection_91);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_91.Out)
		{
			Relay_True_86();
		}
	}

	private void Relay_In_92()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_92 = EnemySpawnTrigger02;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_92 = EnemySpawnTrigger02;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_92.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_92, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_92, ref logic_uScript_IsPlayerInTriggerSmart_inside_92);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_92.Out;
		bool firstEntered = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_92.FirstEntered;
		if (num)
		{
			Relay_In_476();
		}
		if (firstEntered)
		{
			Relay_In_89();
		}
	}

	private void Relay_True_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.True(out logic_uScriptAct_SetBool_Target_93);
		local_EnemyWave01_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_False_96();
		}
	}

	private void Relay_False_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.False(out logic_uScriptAct_SetBool_Target_93);
		local_EnemyWave01_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_False_96();
		}
	}

	private void Relay_True_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.True(out logic_uScriptAct_SetBool_Target_96);
		local_EnemyWave02_System_Boolean = logic_uScriptAct_SetBool_Target_96;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_96.Out)
		{
			Relay_False_98();
		}
	}

	private void Relay_False_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.False(out logic_uScriptAct_SetBool_Target_96);
		local_EnemyWave02_System_Boolean = logic_uScriptAct_SetBool_Target_96;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_96.Out)
		{
			Relay_False_98();
		}
	}

	private void Relay_True_98()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.True(out logic_uScriptAct_SetBool_Target_98);
		local_EnemyWave03_System_Boolean = logic_uScriptAct_SetBool_Target_98;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_98.Out)
		{
			Relay_False_178();
		}
	}

	private void Relay_False_98()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.False(out logic_uScriptAct_SetBool_Target_98);
		local_EnemyWave03_System_Boolean = logic_uScriptAct_SetBool_Target_98;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_98.Out)
		{
			Relay_False_178();
		}
	}

	private void Relay_In_99()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData01;
		if (logic_uScript_DestroyTechsFromData_techData_99.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_99, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_DestroyTechsFromData_techData_99, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_99 = owner_Connection_101;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_99.In(logic_uScript_DestroyTechsFromData_techData_99, logic_uScript_DestroyTechsFromData_shouldExplode_99, logic_uScript_DestroyTechsFromData_ownerNode_99);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_99.Out)
		{
			Relay_In_505();
		}
	}

	private void Relay_In_102()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_102.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_102.Out)
		{
			Relay_In_243();
		}
	}

	private void Relay_In_104()
	{
		logic_uScript_PlayDialogue_dialogue_104 = RaceStartDialogue;
		logic_uScript_PlayDialogue_progress_104 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_104.In(logic_uScript_PlayDialogue_dialogue_104, ref logic_uScript_PlayDialogue_progress_104);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_104;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_104.Out)
		{
			Relay_In_290();
		}
	}

	private void Relay_In_107()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_109()
	{
		logic_uScript_IsTechInTrigger_triggerAreaName_109 = MissionAreaTrigger;
		int num = 0;
		Array array = local_114_TankArray;
		if (logic_uScript_IsTechInTrigger_techs_109.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_IsTechInTrigger_techs_109, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_IsTechInTrigger_techs_109, num, array.Length);
		num += array.Length;
		logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_109.In(logic_uScript_IsTechInTrigger_triggerAreaName_109, ref logic_uScript_IsTechInTrigger_techs_109);
		local_114_TankArray = logic_uScript_IsTechInTrigger_techs_109;
		bool inRange = logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_109.InRange;
		bool outOfRange = logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_109.OutOfRange;
		if (inRange)
		{
			Relay_In_113();
		}
		if (outOfRange)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_110()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_111()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_111.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_111.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_113()
	{
		int num = 0;
		Array array = local_114_TankArray;
		if (logic_uScript_IsTechFriendlyToPlayer_techsIn_113.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_IsTechFriendlyToPlayer_techsIn_113, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_IsTechFriendlyToPlayer_techsIn_113, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = local_112_TankArray;
		if (logic_uScript_IsTechFriendlyToPlayer_techsOut_113.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_IsTechFriendlyToPlayer_techsOut_113, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_IsTechFriendlyToPlayer_techsOut_113, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_IsTechFriendlyToPlayer_uScript_IsTechFriendlyToPlayer_113.In(logic_uScript_IsTechFriendlyToPlayer_techsIn_113, ref logic_uScript_IsTechFriendlyToPlayer_techsOut_113);
		local_112_TankArray = logic_uScript_IsTechFriendlyToPlayer_techsOut_113;
		bool num3 = logic_uScript_IsTechFriendlyToPlayer_uScript_IsTechFriendlyToPlayer_113.True;
		bool flag = logic_uScript_IsTechFriendlyToPlayer_uScript_IsTechFriendlyToPlayer_113.False;
		if (num3)
		{
			Relay_In_400();
		}
		if (flag)
		{
			Relay_In_111();
		}
	}

	private void Relay_True_116()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_116.True(out logic_uScriptAct_SetBool_Target_116);
		local_HasFlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_116;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_116.Out)
		{
			Relay_In_102();
		}
	}

	private void Relay_False_116()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_116.False(out logic_uScriptAct_SetBool_Target_116);
		local_HasFlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_116;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_116.Out)
		{
			Relay_In_102();
		}
	}

	private void Relay_In_117()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_121()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_121.In(logic_uScriptAct_SetInt_Value_121, out logic_uScriptAct_SetInt_Target_121);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_121;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_121.Out)
		{
			Relay_True_385();
		}
	}

	private void Relay_In_123()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_123 = Checkpoint01;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_123 = Checkpoint01;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_123.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_123, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_123, ref logic_uScript_IsPlayerInTriggerSmart_inside_123);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_123.Out;
		bool firstEntered = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_123.FirstEntered;
		if (num)
		{
			Relay_In_125();
		}
		if (firstEntered)
		{
			Relay_In_344();
		}
	}

	private void Relay_In_125()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_125 = Level02Area;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_125 = Level02Area;
		logic_uScript_IsPlayerInTriggerSmart_inside_125 = local_isinLevel02Area_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_125.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_125, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_125, ref logic_uScript_IsPlayerInTriggerSmart_inside_125);
		local_isinLevel02Area_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_125;
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_125.Out;
		bool someInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_125.SomeInside;
		if (num)
		{
			Relay_In_216();
		}
		if (someInside)
		{
			Relay_In_363();
		}
	}

	private void Relay_In_127()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_127 = Level03Area;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_127 = Level03Area;
		logic_uScript_IsPlayerInTriggerSmart_inside_127 = local_isinLevel03Area_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_127.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_127, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_127, ref logic_uScript_IsPlayerInTriggerSmart_inside_127);
		local_isinLevel03Area_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_127;
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_127.Out;
		bool firstEntered = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_127.FirstEntered;
		bool someInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_127.SomeInside;
		if (num)
		{
			Relay_In_131();
		}
		if (firstEntered)
		{
			Relay_In_463();
		}
		if (someInside)
		{
			Relay_In_365();
		}
	}

	private void Relay_In_129()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_129 = Checkpoint02;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_129 = Checkpoint02;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_129.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_129, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_129, ref logic_uScript_IsPlayerInTriggerSmart_inside_129);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_129.Out;
		bool firstEntered = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_129.FirstEntered;
		if (num)
		{
			Relay_In_127();
		}
		if (firstEntered)
		{
			Relay_In_346();
		}
	}

	private void Relay_In_131()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_131 = Checkpoint03;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_131 = Checkpoint03;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_131.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_131, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_131, ref logic_uScript_IsPlayerInTriggerSmart_inside_131);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_131.Out;
		bool firstEntered = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_131.FirstEntered;
		if (num)
		{
			Relay_In_46();
		}
		if (firstEntered)
		{
			Relay_In_348();
		}
	}

	private void Relay_In_133()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_133.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_133.Out)
		{
			Relay_In_169();
		}
	}

	private void Relay_True_140()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_140.True(out logic_uScriptAct_SetBool_Target_140);
		local_InsideMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_140;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_140.Out)
		{
			Relay_In_150();
		}
	}

	private void Relay_False_140()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_140.False(out logic_uScriptAct_SetBool_Target_140);
		local_InsideMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_140;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_140.Out)
		{
			Relay_In_150();
		}
	}

	private void Relay_In_142()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_142.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_142.Out)
		{
			Relay_False_140();
		}
	}

	private void Relay_In_146()
	{
		logic_uScript_PlayDialogue_dialogue_146 = CheckpointMissed01;
		logic_uScript_PlayDialogue_progress_146 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_146.In(logic_uScript_PlayDialogue_dialogue_146, ref logic_uScript_PlayDialogue_progress_146);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_146;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_146.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_148()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_148.In(logic_uScriptAct_SetInt_Value_148, out logic_uScriptAct_SetInt_Target_148);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_148;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_148.Out)
		{
			Relay_In_159();
		}
	}

	private void Relay_In_150()
	{
		logic_uScriptCon_CompareBool_Bool_150 = local_InsideMissionArea_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_150.In(logic_uScriptCon_CompareBool_Bool_150);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_150.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_150.False;
		if (num)
		{
			Relay_In_368();
		}
		if (flag)
		{
			Relay_In_423();
		}
	}

	private void Relay_In_151()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_151.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_151.Out)
		{
			Relay_In_424();
		}
	}

	private void Relay_In_154()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_154.In(logic_uScriptAct_SetInt_Value_154, out logic_uScriptAct_SetInt_Target_154);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_154;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_154.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_PlayDialogue_dialogue_156 = OutOfBounds;
		logic_uScript_PlayDialogue_progress_156 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_156.In(logic_uScript_PlayDialogue_dialogue_156, ref logic_uScript_PlayDialogue_progress_156);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_156;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_156.Shown)
		{
			Relay_In_154();
		}
	}

	private void Relay_In_158()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_158.In(logic_uScriptAct_SetInt_Value_158, out logic_uScriptAct_SetInt_Target_158);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_158;
	}

	private void Relay_In_159()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_159.In(logic_uScriptAct_SetInt_Value_159, out logic_uScriptAct_SetInt_Target_159);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_159;
	}

	private void Relay_InitialSpawn_166()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData03;
		if (logic_uScript_SpawnTechsFromData_spawnData_166.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_166, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_SpawnTechsFromData_spawnData_166, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_166 = owner_Connection_168;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_166.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_166, logic_uScript_SpawnTechsFromData_ownerNode_166, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_166, logic_uScript_SpawnTechsFromData_allowResurrection_166);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_166.Out)
		{
			Relay_True_83();
		}
	}

	private void Relay_In_169()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_169 = EnemySpawnTrigger05;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_169 = EnemySpawnTrigger05;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_169.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_169, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_169, ref logic_uScript_IsPlayerInTriggerSmart_inside_169);
		if (logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_169.FirstEntered)
		{
			Relay_In_171();
		}
	}

	private void Relay_In_171()
	{
		logic_uScriptCon_CompareBool_Bool_171 = local_EnemyWave05_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171.In(logic_uScriptCon_CompareBool_Bool_171);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171.False)
		{
			Relay_InitialSpawn_172();
		}
	}

	private void Relay_InitialSpawn_172()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData05;
		if (logic_uScript_SpawnTechsFromData_spawnData_172.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_172, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_SpawnTechsFromData_spawnData_172, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_172 = owner_Connection_174;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_172.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_172, logic_uScript_SpawnTechsFromData_ownerNode_172, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_172, logic_uScript_SpawnTechsFromData_allowResurrection_172);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_172.Out)
		{
			Relay_True_173();
		}
	}

	private void Relay_True_173()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_173.True(out logic_uScriptAct_SetBool_Target_173);
		local_EnemyWave05_System_Boolean = logic_uScriptAct_SetBool_Target_173;
	}

	private void Relay_False_173()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_173.False(out logic_uScriptAct_SetBool_Target_173);
		local_EnemyWave05_System_Boolean = logic_uScriptAct_SetBool_Target_173;
	}

	private void Relay_True_178()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_178.True(out logic_uScriptAct_SetBool_Target_178);
		local_EnemyWave04_System_Boolean = logic_uScriptAct_SetBool_Target_178;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_178.Out)
		{
			Relay_False_180();
		}
	}

	private void Relay_False_178()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_178.False(out logic_uScriptAct_SetBool_Target_178);
		local_EnemyWave04_System_Boolean = logic_uScriptAct_SetBool_Target_178;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_178.Out)
		{
			Relay_False_180();
		}
	}

	private void Relay_True_180()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_180.True(out logic_uScriptAct_SetBool_Target_180);
		local_EnemyWave05_System_Boolean = logic_uScriptAct_SetBool_Target_180;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_180.Out)
		{
			Relay_In_306();
		}
	}

	private void Relay_False_180()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_180.False(out logic_uScriptAct_SetBool_Target_180);
		local_EnemyWave05_System_Boolean = logic_uScriptAct_SetBool_Target_180;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_180.Out)
		{
			Relay_In_306();
		}
	}

	private void Relay_In_182()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData01;
		if (logic_uScript_GetAndCheckTechs_techData_182.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_182, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_GetAndCheckTechs_techData_182, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_182 = owner_Connection_184;
		int num2 = 0;
		Array array = local_EnemyWaveTechs01_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_182.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_182, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_182, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_182 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_182.In(logic_uScript_GetAndCheckTechs_techData_182, logic_uScript_GetAndCheckTechs_ownerNode_182, ref logic_uScript_GetAndCheckTechs_techs_182);
		local_EnemyWaveTechs01_TankArray = logic_uScript_GetAndCheckTechs_techs_182;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_182.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_182.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_182.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_182.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_464();
		}
		if (someAlive)
		{
			Relay_AtIndex_464();
		}
		if (allDead)
		{
			Relay_In_188();
		}
		if (waitingToSpawn)
		{
			Relay_In_188();
		}
	}

	private void Relay_In_188()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_188.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_188.Out)
		{
			Relay_In_189();
		}
	}

	private void Relay_In_189()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_189.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_189.Out)
		{
			Relay_In_211();
		}
	}

	private void Relay_In_193()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData02;
		if (logic_uScript_GetAndCheckTechs_techData_193.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_193, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_GetAndCheckTechs_techData_193, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_193 = owner_Connection_191;
		int num2 = 0;
		Array array = local_EnemyWaveTechs02_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_193.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_193, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_193, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_193 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_193.In(logic_uScript_GetAndCheckTechs_techData_193, logic_uScript_GetAndCheckTechs_ownerNode_193, ref logic_uScript_GetAndCheckTechs_techs_193);
		local_EnemyWaveTechs02_TankArray = logic_uScript_GetAndCheckTechs_techs_193;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_193.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_193.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_193.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_193.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_466();
		}
		if (someAlive)
		{
			Relay_AtIndex_466();
		}
		if (allDead)
		{
			Relay_In_194();
		}
		if (waitingToSpawn)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_194()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194.Out)
		{
			Relay_In_195();
		}
	}

	private void Relay_In_195()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195.Out)
		{
			Relay_In_214();
		}
	}

	private void Relay_In_200()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData03;
		if (logic_uScript_GetAndCheckTechs_techData_200.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_200, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_GetAndCheckTechs_techData_200, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_200 = owner_Connection_199;
		int num2 = 0;
		Array array = local_EnemyWaveTechs03_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_200.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_200, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_200, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_200 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200.In(logic_uScript_GetAndCheckTechs_techData_200, logic_uScript_GetAndCheckTechs_ownerNode_200, ref logic_uScript_GetAndCheckTechs_techs_200);
		local_EnemyWaveTechs03_TankArray = logic_uScript_GetAndCheckTechs_techs_200;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_470();
		}
		if (someAlive)
		{
			Relay_AtIndex_470();
		}
		if (allDead)
		{
			Relay_In_204();
		}
		if (waitingToSpawn)
		{
			Relay_In_204();
		}
	}

	private void Relay_In_204()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_204.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_204.Out)
		{
			Relay_In_205();
		}
	}

	private void Relay_In_205()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_205.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_205.Out)
		{
			Relay_In_480();
		}
	}

	private void Relay_In_206()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_206.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_206.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_In_208()
	{
		logic_uScriptCon_CompareBool_Bool_208 = local_EnemyWave01_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.In(logic_uScriptCon_CompareBool_Bool_208);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.False;
		if (num)
		{
			Relay_In_182();
		}
		if (flag)
		{
			Relay_In_209();
		}
	}

	private void Relay_In_209()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.Out)
		{
			Relay_In_188();
		}
	}

	private void Relay_In_211()
	{
		logic_uScriptCon_CompareBool_Bool_211 = local_EnemyWave02_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.In(logic_uScriptCon_CompareBool_Bool_211);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.False;
		if (num)
		{
			Relay_In_193();
		}
		if (flag)
		{
			Relay_In_212();
		}
	}

	private void Relay_In_212()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_212.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_212.Out)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_214()
	{
		logic_uScriptCon_CompareBool_Bool_214 = local_EnemyWave03_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214.In(logic_uScriptCon_CompareBool_Bool_214);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214.False;
		if (num)
		{
			Relay_In_200();
		}
		if (flag)
		{
			Relay_In_215();
		}
	}

	private void Relay_In_215()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_215.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_215.Out)
		{
			Relay_In_204();
		}
	}

	private void Relay_In_216()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_216.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_216.Out)
		{
			Relay_In_220();
		}
	}

	private void Relay_In_218()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_218.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_218.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_In_219()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_219.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_219.Out)
		{
			Relay_In_241();
		}
	}

	private void Relay_In_220()
	{
		logic_uScriptCon_CompareBool_Bool_220 = local_EnemyWave05_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_220.In(logic_uScriptCon_CompareBool_Bool_220);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_220.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_220.False;
		if (num)
		{
			Relay_In_223();
		}
		if (flag)
		{
			Relay_In_218();
		}
	}

	private void Relay_In_223()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData05;
		if (logic_uScript_GetAndCheckTechs_techData_223.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_223, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_GetAndCheckTechs_techData_223, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_223 = owner_Connection_227;
		int num2 = 0;
		Array array = local_EnemyWaveTechs05_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_223.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_223, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_223, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_223 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_223.In(logic_uScript_GetAndCheckTechs_techData_223, logic_uScript_GetAndCheckTechs_ownerNode_223, ref logic_uScript_GetAndCheckTechs_techs_223);
		local_EnemyWaveTechs05_TankArray = logic_uScript_GetAndCheckTechs_techs_223;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_223.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_223.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_223.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_223.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_472();
		}
		if (someAlive)
		{
			Relay_AtIndex_472();
		}
		if (allDead)
		{
			Relay_In_228();
		}
		if (waitingToSpawn)
		{
			Relay_In_228();
		}
	}

	private void Relay_In_228()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_228.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_228.Out)
		{
			Relay_In_219();
		}
	}

	private void Relay_In_231()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_231.In(logic_uScriptAct_SetInt_Value_231, out logic_uScriptAct_SetInt_Target_231);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_231;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_231.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_234()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_234.In(logic_uScriptAct_SetInt_Value_234, out logic_uScriptAct_SetInt_Target_234);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_234;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_234.Out)
		{
			Relay_In_59();
		}
	}

	private void Relay_In_235()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_235.In(logic_uScriptAct_SetInt_Value_235, out logic_uScriptAct_SetInt_Target_235);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_235;
	}

	private void Relay_In_238()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_238.In(logic_uScriptAct_SetInt_Value_238, out logic_uScriptAct_SetInt_Target_238);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_238;
	}

	private void Relay_In_239()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_239.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_239.Out)
		{
			Relay_In_240();
		}
	}

	private void Relay_In_240()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_240.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_240.Out)
		{
			Relay_In_397();
		}
	}

	private void Relay_In_241()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_241.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_241.Out)
		{
			Relay_In_432();
		}
	}

	private void Relay_In_243()
	{
		logic_uScriptCon_CompareBool_Bool_243 = local_HasFlightBlock_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_243.In(logic_uScriptCon_CompareBool_Bool_243);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_243.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_243.False;
		if (num)
		{
			Relay_In_382();
		}
		if (flag)
		{
			Relay_In_245();
		}
	}

	private void Relay_In_245()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_245.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_245.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_246()
	{
		logic_uScript_PlayDialogue_dialogue_246 = ForbiddenBlockDialogue;
		logic_uScript_PlayDialogue_progress_246 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_246.In(logic_uScript_PlayDialogue_dialogue_246, ref logic_uScript_PlayDialogue_progress_246);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_246;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_246.Out)
		{
			Relay_In_249();
		}
	}

	private void Relay_In_249()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_249.In(logic_uScriptAct_SetInt_Value_249, out logic_uScriptAct_SetInt_Target_249);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_249;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_249.Out)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_251()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_251.In(logic_uScriptAct_SetInt_Value_251, out logic_uScriptAct_SetInt_Target_251);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_251;
	}

	private void Relay_In_255()
	{
		logic_uScriptCon_CompareBool_Bool_255 = local_HasFlightBlock_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255.In(logic_uScriptCon_CompareBool_Bool_255);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255.False;
		if (num)
		{
			Relay_In_256();
		}
		if (flag)
		{
			Relay_In_260();
		}
	}

	private void Relay_In_256()
	{
		int num = 0;
		Array array = msgForbiddenBlock;
		if (logic_uScript_AddOnScreenMessage_locString_256.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_256, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_256, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_256 = ForbiddenBlockTag;
		logic_uScript_AddOnScreenMessage_speaker_256 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_256 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_256.In(logic_uScript_AddOnScreenMessage_locString_256, logic_uScript_AddOnScreenMessage_msgPriority_256, logic_uScript_AddOnScreenMessage_holdMsg_256, logic_uScript_AddOnScreenMessage_tag_256, logic_uScript_AddOnScreenMessage_speaker_256, logic_uScript_AddOnScreenMessage_side_256);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_256.Shown)
		{
			Relay_False_401();
		}
	}

	private void Relay_In_260()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_260 = ForbiddenBlockTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_260.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_260, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_260);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_260.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_In_266()
	{
		int num = 0;
		Array array = msgForbiddenBlockRunning;
		if (logic_uScript_AddOnScreenMessage_locString_266.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_266, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_266, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_266 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_266 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_266.In(logic_uScript_AddOnScreenMessage_locString_266, logic_uScript_AddOnScreenMessage_msgPriority_266, logic_uScript_AddOnScreenMessage_holdMsg_266, logic_uScript_AddOnScreenMessage_tag_266, logic_uScript_AddOnScreenMessage_speaker_266, logic_uScript_AddOnScreenMessage_side_266);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_266.Out)
		{
			Relay_In_249();
		}
	}

	private void Relay_In_269()
	{
		logic_uScriptCon_CompareBool_Bool_269 = local_inStartTrigger_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_269.In(logic_uScriptCon_CompareBool_Bool_269);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_269.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_269.False;
		if (num)
		{
			Relay_In_270();
		}
		if (flag)
		{
			Relay_In_266();
		}
	}

	private void Relay_In_270()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_270.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_270.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_271()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271.Out)
		{
			Relay_In_249();
		}
	}

	private void Relay_In_272()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_272 = IntroTrigger;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_272.In(logic_uScript_SetEncounterTargetPosition_positionName_272);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_272.Out)
		{
			Relay_In_391();
		}
	}

	private void Relay_Out_273()
	{
		Relay_In_309();
	}

	private void Relay_In_273()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_273 = local_Objective_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_273.In(logic_SubGraph_LoadObjectiveStates_currentObjective_273);
	}

	private void Relay_Out_276()
	{
		Relay_In_22();
	}

	private void Relay_In_276()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_276 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_276.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_276, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_276);
	}

	private void Relay_In_278()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_278 = ObjPos01;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_278.In(logic_uScript_SetEncounterTargetPosition_positionName_278);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_278.Out)
		{
			Relay_In_276();
		}
	}

	private void Relay_In_281()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_281 = ObjPos03;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_281.In(logic_uScript_SetEncounterTargetPosition_positionName_281);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_281.Out)
		{
			Relay_In_298();
		}
	}

	private void Relay_Out_282()
	{
		Relay_In_293();
	}

	private void Relay_In_282()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_282 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_282, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_282);
	}

	private void Relay_Out_285()
	{
		Relay_Succeed_61();
	}

	private void Relay_In_285()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_285 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_285.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_285, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_285);
	}

	private void Relay_In_290()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_290 = ObjPos02;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_290.In(logic_uScript_SetEncounterTargetPosition_positionName_290);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_290.Out)
		{
			Relay_In_441();
		}
	}

	private void Relay_In_292()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_292 = ObjPos04;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_292.In(logic_uScript_SetEncounterTargetPosition_positionName_292);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_292.Out)
		{
			Relay_In_301();
		}
	}

	private void Relay_In_293()
	{
		logic_uScript_ClearEncounterTarget_owner_293 = owner_Connection_294;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_293.In(logic_uScript_ClearEncounterTarget_owner_293);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_293.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_Save_Out_297()
	{
	}

	private void Relay_Load_Out_297()
	{
		Relay_In_273();
	}

	private void Relay_Restart_Out_297()
	{
	}

	private void Relay_Save_297()
	{
		logic_SubGraph_SaveLoadInt_integer_297 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_297 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.Save(logic_SubGraph_SaveLoadInt_restartValue_297, ref logic_SubGraph_SaveLoadInt_integer_297, logic_SubGraph_SaveLoadInt_intAsVariable_297, logic_SubGraph_SaveLoadInt_uniqueID_297);
	}

	private void Relay_Load_297()
	{
		logic_SubGraph_SaveLoadInt_integer_297 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_297 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.Load(logic_SubGraph_SaveLoadInt_restartValue_297, ref logic_SubGraph_SaveLoadInt_integer_297, logic_SubGraph_SaveLoadInt_intAsVariable_297, logic_SubGraph_SaveLoadInt_uniqueID_297);
	}

	private void Relay_Restart_297()
	{
		logic_SubGraph_SaveLoadInt_integer_297 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_297 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_297.Restart(logic_SubGraph_SaveLoadInt_restartValue_297, ref logic_SubGraph_SaveLoadInt_integer_297, logic_SubGraph_SaveLoadInt_intAsVariable_297, logic_SubGraph_SaveLoadInt_uniqueID_297);
	}

	private void Relay_In_298()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_298.In(logic_uScriptAct_SetInt_Value_298, out logic_uScriptAct_SetInt_Target_298);
		local_Checkpoint_System_Int32 = logic_uScriptAct_SetInt_Target_298;
	}

	private void Relay_In_301()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_301.In(logic_uScriptAct_SetInt_Value_301, out logic_uScriptAct_SetInt_Target_301);
		local_Checkpoint_System_Int32 = logic_uScriptAct_SetInt_Target_301;
	}

	private void Relay_In_302()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_302.In(logic_uScriptAct_SetInt_Value_302, out logic_uScriptAct_SetInt_Target_302);
		local_Checkpoint_System_Int32 = logic_uScriptAct_SetInt_Target_302;
	}

	private void Relay_In_305()
	{
		logic_uScriptCon_CompareInt_A_305 = local_Checkpoint_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_305.In(logic_uScriptCon_CompareInt_A_305, logic_uScriptCon_CompareInt_B_305);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_305.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_305.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_282();
		}
		if (lessThan)
		{
			Relay_In_146();
		}
	}

	private void Relay_In_306()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_306.In(logic_uScriptAct_SetInt_Value_306, out logic_uScriptAct_SetInt_Target_306);
		local_Checkpoint_System_Int32 = logic_uScriptAct_SetInt_Target_306;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_306.Out)
		{
			Relay_In_381();
		}
	}

	private void Relay_In_309()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_309.In(logic_uScriptAct_SetInt_Value_309, out logic_uScriptAct_SetInt_Target_309);
		local_Checkpoint_System_Int32 = logic_uScriptAct_SetInt_Target_309;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_309.Out)
		{
			Relay_In_390();
		}
	}

	private void Relay_In_311()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_311 = ObjPos01;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_311.In(logic_uScript_SetEncounterTargetPosition_positionName_311);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_311.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_In_312()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_312.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_312, num + 1);
		}
		logic_uScriptAct_Concatenate_A_312[num++] = local_28_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_312.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_312, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_312[num2++] = local_313_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_312.In(logic_uScriptAct_Concatenate_A_312, logic_uScriptAct_Concatenate_B_312, logic_uScriptAct_Concatenate_Separator_312, out logic_uScriptAct_Concatenate_Result_312);
		local_314_System_String = logic_uScriptAct_Concatenate_Result_312;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_312.Out)
		{
			Relay_In_316();
		}
	}

	private void Relay_In_316()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_316.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_316, num + 1);
		}
		logic_uScriptAct_Concatenate_A_316[num++] = local_314_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_316.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_316, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_316[num2++] = local_DialogueProgress_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_316.In(logic_uScriptAct_Concatenate_A_316, logic_uScriptAct_Concatenate_B_316, logic_uScriptAct_Concatenate_Separator_316, out logic_uScriptAct_Concatenate_Result_316);
		local_318_System_String = logic_uScriptAct_Concatenate_Result_316;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_316.Out)
		{
			Relay_In_317();
		}
	}

	private void Relay_In_317()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_317.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_317, num + 1);
		}
		logic_uScriptAct_Concatenate_A_317[num++] = local_318_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_317.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_317, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_317[num2++] = local_319_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_317.In(logic_uScriptAct_Concatenate_A_317, logic_uScriptAct_Concatenate_B_317, logic_uScriptAct_Concatenate_Separator_317, out logic_uScriptAct_Concatenate_Result_317);
		local_315_System_String = logic_uScriptAct_Concatenate_Result_317;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_317.Out)
		{
			Relay_In_321();
		}
	}

	private void Relay_In_321()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_321.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_321, num + 1);
		}
		logic_uScriptAct_Concatenate_A_321[num++] = local_315_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_321.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_321, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_321[num2++] = local_Objective_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_321.In(logic_uScriptAct_Concatenate_A_321, logic_uScriptAct_Concatenate_B_321, logic_uScriptAct_Concatenate_Separator_321, out logic_uScriptAct_Concatenate_Result_321);
		local_320_System_String = logic_uScriptAct_Concatenate_Result_321;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_321.Out)
		{
			Relay_In_324();
		}
	}

	private void Relay_In_323()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_323.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_323, num + 1);
		}
		logic_uScriptAct_Concatenate_A_323[num++] = local_322_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_323.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_323, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_323[num2++] = local_isinFinishArea_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_323.In(logic_uScriptAct_Concatenate_A_323, logic_uScriptAct_Concatenate_B_323, logic_uScriptAct_Concatenate_Separator_323, out logic_uScriptAct_Concatenate_Result_323);
		local_326_System_String = logic_uScriptAct_Concatenate_Result_323;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_323.Out)
		{
			Relay_ShowLabel_340();
		}
	}

	private void Relay_In_324()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_324.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_324, num + 1);
		}
		logic_uScriptAct_Concatenate_A_324[num++] = local_320_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_324.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_324, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_324[num2++] = local_325_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_324.In(logic_uScriptAct_Concatenate_A_324, logic_uScriptAct_Concatenate_B_324, logic_uScriptAct_Concatenate_Separator_324, out logic_uScriptAct_Concatenate_Result_324);
		local_322_System_String = logic_uScriptAct_Concatenate_Result_324;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_324.Out)
		{
			Relay_In_323();
		}
	}

	private void Relay_In_329()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_329.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_329, num + 1);
		}
		logic_uScriptAct_Concatenate_A_329[num++] = local_37_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_329.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_329, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_329[num2++] = local_327_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_329.In(logic_uScriptAct_Concatenate_A_329, logic_uScriptAct_Concatenate_B_329, logic_uScriptAct_Concatenate_Separator_329, out logic_uScriptAct_Concatenate_Result_329);
		local_328_System_String = logic_uScriptAct_Concatenate_Result_329;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_329.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_334()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_334.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_334, num + 1);
		}
		logic_uScriptAct_Concatenate_A_334[num++] = local_32_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_334.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_334, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_334[num2++] = local_331_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_334.In(logic_uScriptAct_Concatenate_A_334, logic_uScriptAct_Concatenate_B_334, logic_uScriptAct_Concatenate_Separator_334, out logic_uScriptAct_Concatenate_Result_334);
		local_333_System_String = logic_uScriptAct_Concatenate_Result_334;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_334.Out)
		{
			Relay_In_335();
		}
	}

	private void Relay_In_335()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_335.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_335, num + 1);
		}
		logic_uScriptAct_Concatenate_A_335[num++] = local_333_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_335.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_335, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_335[num2++] = local_Checkpoint_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_335.In(logic_uScriptAct_Concatenate_A_335, logic_uScriptAct_Concatenate_B_335, logic_uScriptAct_Concatenate_Separator_335, out logic_uScriptAct_Concatenate_Result_335);
		local_330_System_String = logic_uScriptAct_Concatenate_Result_335;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_335.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_ShowLabel_340()
	{
		logic_uScriptAct_PrintText_Text_340 = local_326_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_340.ShowLabel(logic_uScriptAct_PrintText_Text_340, logic_uScriptAct_PrintText_FontSize_340, logic_uScriptAct_PrintText_FontStyle_340, logic_uScriptAct_PrintText_FontColor_340, logic_uScriptAct_PrintText_textAnchor_340, logic_uScriptAct_PrintText_EdgePadding_340, logic_uScriptAct_PrintText_time_340);
	}

	private void Relay_HideLabel_340()
	{
		logic_uScriptAct_PrintText_Text_340 = local_326_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_340.HideLabel(logic_uScriptAct_PrintText_Text_340, logic_uScriptAct_PrintText_FontSize_340, logic_uScriptAct_PrintText_FontStyle_340, logic_uScriptAct_PrintText_FontColor_340, logic_uScriptAct_PrintText_textAnchor_340, logic_uScriptAct_PrintText_EdgePadding_340, logic_uScriptAct_PrintText_time_340);
	}

	private void Relay_In_344()
	{
		logic_uScriptCon_CheckIntEquals_A_344 = local_Checkpoint_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_344.In(logic_uScriptCon_CheckIntEquals_A_344, logic_uScriptCon_CheckIntEquals_B_344);
		if (logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_344.True)
		{
			Relay_In_281();
		}
	}

	private void Relay_In_346()
	{
		logic_uScriptCon_CheckIntEquals_A_346 = local_Checkpoint_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_346.In(logic_uScriptCon_CheckIntEquals_A_346, logic_uScriptCon_CheckIntEquals_B_346);
		if (logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_346.True)
		{
			Relay_In_292();
		}
	}

	private void Relay_In_348()
	{
		logic_uScriptCon_CheckIntEquals_A_348 = local_Checkpoint_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_348.In(logic_uScriptCon_CheckIntEquals_A_348, logic_uScriptCon_CheckIntEquals_B_348);
		if (logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_348.True)
		{
			Relay_In_302();
		}
	}

	private void Relay_In_360()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_360.In(logic_uScriptAct_SetInt_Value_360, out logic_uScriptAct_SetInt_Target_360);
		local_LevelArea_System_Int32 = logic_uScriptAct_SetInt_Target_360;
	}

	private void Relay_In_363()
	{
		logic_uScriptCon_CompareInt_A_363 = local_LevelArea_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_363.In(logic_uScriptCon_CompareInt_A_363, logic_uScriptCon_CompareInt_B_363);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_363.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_363.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_133();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_360();
		}
	}

	private void Relay_In_365()
	{
		logic_uScriptCon_CompareInt_A_365 = local_LevelArea_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_365.In(logic_uScriptCon_CompareInt_A_365, logic_uScriptCon_CompareInt_B_365);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_365.LessThanOrEqualTo)
		{
			Relay_In_366();
		}
	}

	private void Relay_In_366()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_366.In(logic_uScriptAct_SetInt_Value_366, out logic_uScriptAct_SetInt_Target_366);
		local_LevelArea_System_Int32 = logic_uScriptAct_SetInt_Target_366;
	}

	private void Relay_In_368()
	{
		logic_uScriptCon_CompareInt_A_368 = local_Checkpoint_System_Int32;
		logic_uScriptCon_CompareInt_B_368 = local_LevelArea_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_368.In(logic_uScriptCon_CompareInt_A_368, logic_uScriptCon_CompareInt_B_368);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_368.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_368.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_43();
		}
		if (lessThan)
		{
			Relay_In_370();
		}
	}

	private void Relay_In_369()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_369.In(logic_uScriptAct_SetInt_Value_369, out logic_uScriptAct_SetInt_Target_369);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_369;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_369.Out)
		{
			Relay_In_373();
		}
	}

	private void Relay_In_370()
	{
		logic_uScript_PlayDialogue_dialogue_370 = CheckpointMissed01;
		logic_uScript_PlayDialogue_progress_370 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_370.In(logic_uScript_PlayDialogue_dialogue_370, ref logic_uScript_PlayDialogue_progress_370);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_370;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_370.Out)
		{
			Relay_In_369();
		}
	}

	private void Relay_In_373()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_373.In(logic_uScriptAct_SetInt_Value_373, out logic_uScriptAct_SetInt_Target_373);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_373;
	}

	private void Relay_In_381()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_381.In(logic_uScriptAct_SetInt_Value_381, out logic_uScriptAct_SetInt_Target_381);
		local_LevelArea_System_Int32 = logic_uScriptAct_SetInt_Target_381;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_381.Out)
		{
			Relay_In_99();
		}
	}

	private void Relay_In_382()
	{
		logic_uScriptCon_CheckIntEquals_B_382 = local_Stage_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_382.In(logic_uScriptCon_CheckIntEquals_A_382, logic_uScriptCon_CheckIntEquals_B_382);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_382.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_382.False;
		if (num)
		{
			Relay_In_246();
		}
		if (flag)
		{
			Relay_In_245();
		}
	}

	private void Relay_True_385()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_385.True(out logic_uScriptAct_SetBool_Target_385);
		local_IntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_385;
	}

	private void Relay_False_385()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_385.False(out logic_uScriptAct_SetBool_Target_385);
		local_IntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_385;
	}

	private void Relay_In_387()
	{
		logic_uScriptCon_CompareBool_Bool_387 = local_IntroPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_387.In(logic_uScriptCon_CompareBool_Bool_387);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_387.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_387.False;
		if (num)
		{
			Relay_True_429();
		}
		if (flag)
		{
			Relay_False_429();
		}
	}

	private void Relay_In_390()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_390.In(logic_uScriptAct_SetInt_Value_390, out logic_uScriptAct_SetInt_Target_390);
		local_LevelArea_System_Int32 = logic_uScriptAct_SetInt_Target_390;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_390.Out)
		{
			Relay_In_387();
		}
	}

	private void Relay_Out_391()
	{
		Relay_True_13();
	}

	private void Relay_In_391()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_391 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_391.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_391, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_391);
	}

	private void Relay_In_393()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_393.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_393.Out)
		{
			Relay_In_238();
		}
	}

	private void Relay_In_394()
	{
		logic_uScript_FlyTechUpAndAway_tech_394 = local_NPCEndTech02_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_394 = AIFlyTree;
		logic_uScript_FlyTechUpAndAway_removalParticles_394 = RemovalParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_394.In(logic_uScript_FlyTechUpAndAway_tech_394, logic_uScript_FlyTechUpAndAway_maxLifetime_394, logic_uScript_FlyTechUpAndAway_targetHeight_394, logic_uScript_FlyTechUpAndAway_aiTree_394, logic_uScript_FlyTechUpAndAway_removalParticles_394);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_394.Out)
		{
			Relay_In_238();
		}
	}

	private void Relay_AtIndex_396()
	{
		int num = 0;
		Array array = local_NPCTechsEnd01_TankArray;
		if (logic_uScript_AccessListTech_techList_396.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_396, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_396, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_396.AtIndex(ref logic_uScript_AccessListTech_techList_396, logic_uScript_AccessListTech_index_396, out logic_uScript_AccessListTech_value_396);
		local_NPCTechsEnd01_TankArray = logic_uScript_AccessListTech_techList_396;
		local_NPCEndTech02_Tank = logic_uScript_AccessListTech_value_396;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_396.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_397()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_397.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_397.Out)
		{
			Relay_In_393();
		}
	}

	private void Relay_In_400()
	{
		int num = 0;
		Array array = local_112_TankArray;
		if (logic_uScript_CheckTechFlightBlocks_techs_400.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_CheckTechFlightBlocks_techs_400, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_CheckTechFlightBlocks_techs_400, num, array.Length);
		num += array.Length;
		logic_uScript_CheckTechFlightBlocks_uScript_CheckTechFlightBlocks_400.In(logic_uScript_CheckTechFlightBlocks_techs_400);
		bool hasFlightBlocks = logic_uScript_CheckTechFlightBlocks_uScript_CheckTechFlightBlocks_400.HasFlightBlocks;
		bool doesntHaveFlightBlocks = logic_uScript_CheckTechFlightBlocks_uScript_CheckTechFlightBlocks_400.DoesntHaveFlightBlocks;
		if (hasFlightBlocks)
		{
			Relay_True_116();
		}
		if (doesntHaveFlightBlocks)
		{
			Relay_False_116();
		}
	}

	private void Relay_True_401()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_401.True(out logic_uScriptAct_SetBool_Target_401);
		local_HasFlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_401;
	}

	private void Relay_False_401()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_401.False(out logic_uScriptAct_SetBool_Target_401);
		local_HasFlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_401;
	}

	private void Relay_In_403()
	{
		int num = 0;
		if (logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403.Length <= num)
		{
			Array.Resize(ref logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403, num + 1);
		}
		logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403[num++] = OutsideArea01;
		if (logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403.Length <= num)
		{
			Array.Resize(ref logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403, num + 1);
		}
		logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403[num++] = OutsideArea02;
		if (logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403.Length <= num)
		{
			Array.Resize(ref logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403, num + 1);
		}
		logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403[num++] = OutsideArea02Half01;
		if (logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403.Length <= num)
		{
			Array.Resize(ref logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403, num + 1);
		}
		logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403[num++] = OutsideArea02Half02;
		if (logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403.Length <= num)
		{
			Array.Resize(ref logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403, num + 1);
		}
		logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403[num++] = OutsideArea02Half03;
		if (logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403.Length <= num)
		{
			Array.Resize(ref logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403, num + 1);
		}
		logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403[num++] = OutsideArea03;
		if (logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403.Length <= num)
		{
			Array.Resize(ref logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403, num + 1);
		}
		logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403[num++] = OutsideArea04;
		if (logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403.Length <= num)
		{
			Array.Resize(ref logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403, num + 1);
		}
		logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403[num++] = OutsideArea05;
		if (logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403.Length <= num)
		{
			Array.Resize(ref logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403, num + 1);
		}
		logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403[num++] = OutsideArea06;
		if (logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403.Length <= num)
		{
			Array.Resize(ref logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403, num + 1);
		}
		logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403[num++] = OutsideArea07;
		if (logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403.Length <= num)
		{
			Array.Resize(ref logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403, num + 1);
		}
		logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403[num++] = OutsideArea02Half04;
		logic_uScript_IsPlayerInTriggersMultiple_uScript_IsPlayerInTriggersMultiple_403.In(logic_uScript_IsPlayerInTriggersMultiple_triggerAreas_403, ref logic_uScript_IsPlayerInTriggersMultiple_inside_403);
		bool allOutside = logic_uScript_IsPlayerInTriggersMultiple_uScript_IsPlayerInTriggersMultiple_403.AllOutside;
		bool someInside = logic_uScript_IsPlayerInTriggersMultiple_uScript_IsPlayerInTriggersMultiple_403.SomeInside;
		if (allOutside)
		{
			Relay_In_404();
		}
		if (someInside)
		{
			Relay_In_142();
		}
	}

	private void Relay_In_404()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_404.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_404.Out)
		{
			Relay_True_140();
		}
	}

	private void Relay_Save_Out_407()
	{
		Relay_Save_547();
	}

	private void Relay_Load_Out_407()
	{
		Relay_Load_547();
	}

	private void Relay_Restart_Out_407()
	{
		Relay_Set_False_547();
	}

	private void Relay_Save_407()
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = local_IntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_407 = local_IntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Save(ref logic_SubGraph_SaveLoadBool_boolean_407, logic_SubGraph_SaveLoadBool_boolAsVariable_407, logic_SubGraph_SaveLoadBool_uniqueID_407);
	}

	private void Relay_Load_407()
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = local_IntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_407 = local_IntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Load(ref logic_SubGraph_SaveLoadBool_boolean_407, logic_SubGraph_SaveLoadBool_boolAsVariable_407, logic_SubGraph_SaveLoadBool_uniqueID_407);
	}

	private void Relay_Set_True_407()
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = local_IntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_407 = local_IntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_407, logic_SubGraph_SaveLoadBool_boolAsVariable_407, logic_SubGraph_SaveLoadBool_uniqueID_407);
	}

	private void Relay_Set_False_407()
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = local_IntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_407 = local_IntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_407, logic_SubGraph_SaveLoadBool_boolAsVariable_407, logic_SubGraph_SaveLoadBool_uniqueID_407);
	}

	private void Relay_TechDestroyedEvent_408()
	{
		local_411_Tank = event_UnityEngine_GameObject_Tech_408;
		Relay_True_410();
	}

	private void Relay_True_410()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_410.True(out logic_uScriptAct_SetBool_Target_410);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_410;
	}

	private void Relay_False_410()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_410.False(out logic_uScriptAct_SetBool_Target_410);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_410;
	}

	private void Relay_In_413()
	{
		logic_uScriptCon_CompareBool_Bool_413 = local_PlayerDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_413.In(logic_uScriptCon_CompareBool_Bool_413);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_413.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_413.False;
		if (num)
		{
			Relay_In_415();
		}
		if (flag)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_415()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_415 = RespawnArea;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_415 = RespawnArea;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_415.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_415, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_415, ref logic_uScript_IsPlayerInTriggerSmart_inside_415);
		if (logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_415.SomeInside)
		{
			Relay_False_418();
		}
	}

	private void Relay_True_418()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_418.True(out logic_uScriptAct_SetBool_Target_418);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_418;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_418.Out)
		{
			Relay_In_421();
		}
	}

	private void Relay_False_418()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_418.False(out logic_uScriptAct_SetBool_Target_418);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_418;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_418.Out)
		{
			Relay_In_421();
		}
	}

	private void Relay_In_421()
	{
		logic_uScript_PlayDialogue_dialogue_421 = FailDeadDialogue;
		logic_uScript_PlayDialogue_progress_421 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_421.In(logic_uScript_PlayDialogue_dialogue_421, ref logic_uScript_PlayDialogue_progress_421);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_421;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_421.Out)
		{
			Relay_False_93();
		}
	}

	private void Relay_In_423()
	{
		logic_uScriptCon_CompareBool_Bool_423 = local_PlayerDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_423.In(logic_uScriptCon_CompareBool_Bool_423);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_423.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_423.False;
		if (num)
		{
			Relay_In_151();
		}
		if (flag)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_424()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_424.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_424.Out)
		{
			Relay_In_154();
		}
	}

	private void Relay_In_427()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_427.In(logic_uScriptAct_SetInt_Value_427, out logic_uScriptAct_SetInt_Target_427);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_427;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_427.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_True_429()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_429.True(out logic_uScriptAct_SetBool_Target_429);
		local_SetFailOnLoad_System_Boolean = logic_uScriptAct_SetBool_Target_429;
	}

	private void Relay_False_429()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_429.False(out logic_uScriptAct_SetBool_Target_429);
		local_SetFailOnLoad_System_Boolean = logic_uScriptAct_SetBool_Target_429;
	}

	private void Relay_In_432()
	{
		logic_uScriptCon_CompareBool_Bool_432 = local_SetFailOnLoad_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432.In(logic_uScriptCon_CompareBool_Bool_432);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432.False;
		if (num)
		{
			Relay_In_433();
		}
		if (flag)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_433()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_433.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_433.Out)
		{
			Relay_In_436();
		}
	}

	private void Relay_In_435()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_435.In(logic_uScriptAct_SetInt_Value_435, out logic_uScriptAct_SetInt_Target_435);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_435;
	}

	private void Relay_In_436()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_436.In(logic_uScriptAct_SetInt_Value_436, out logic_uScriptAct_SetInt_Target_436);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_436;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_436.Out)
		{
			Relay_In_435();
		}
	}

	private void Relay_True_438()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_438.True(out logic_uScriptAct_SetBool_Target_438);
		local_SetFailOnLoad_System_Boolean = logic_uScriptAct_SetBool_Target_438;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_438.Out)
		{
			Relay_In_427();
		}
	}

	private void Relay_False_438()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_438.False(out logic_uScriptAct_SetBool_Target_438);
		local_SetFailOnLoad_System_Boolean = logic_uScriptAct_SetBool_Target_438;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_438.Out)
		{
			Relay_In_427();
		}
	}

	private void Relay_In_441()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_441.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_441, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_441, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_441 = owner_Connection_455;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_441.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_441, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_441, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_441 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_441.In(logic_uScript_GetAndCheckTechs_techData_441, logic_uScript_GetAndCheckTechs_ownerNode_441, ref logic_uScript_GetAndCheckTechs_techs_441);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_441;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_441.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_441.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_441.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_441.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_445();
		}
		if (someAlive)
		{
			Relay_AtIndex_445();
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

	private void Relay_In_444()
	{
		logic_uScript_FlyTechUpAndAway_tech_444 = local_NPCTech01_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_444 = AIFlyTree;
		logic_uScript_FlyTechUpAndAway_removalParticles_444 = RemovalParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_444.In(logic_uScript_FlyTechUpAndAway_tech_444, logic_uScript_FlyTechUpAndAway_maxLifetime_444, logic_uScript_FlyTechUpAndAway_targetHeight_444, logic_uScript_FlyTechUpAndAway_aiTree_444, logic_uScript_FlyTechUpAndAway_removalParticles_444);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_444.Out)
		{
			Relay_AtIndex_454();
		}
	}

	private void Relay_AtIndex_445()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_445.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_445, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_445, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_445.AtIndex(ref logic_uScript_AccessListTech_techList_445, logic_uScript_AccessListTech_index_445, out logic_uScript_AccessListTech_value_445);
		local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_445;
		local_NPCTech01_Tank = logic_uScript_AccessListTech_value_445;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_445.Out)
		{
			Relay_In_444();
		}
	}

	private void Relay_In_450()
	{
		logic_uScript_FlyTechUpAndAway_tech_450 = local_NPCTech02_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_450 = AIFlyTree;
		logic_uScript_FlyTechUpAndAway_removalParticles_450 = RemovalParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_450.In(logic_uScript_FlyTechUpAndAway_tech_450, logic_uScript_FlyTechUpAndAway_maxLifetime_450, logic_uScript_FlyTechUpAndAway_targetHeight_450, logic_uScript_FlyTechUpAndAway_aiTree_450, logic_uScript_FlyTechUpAndAway_removalParticles_450);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_450.Out)
		{
			Relay_False_438();
		}
	}

	private void Relay_AtIndex_454()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_454.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_454, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_454, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_454.AtIndex(ref logic_uScript_AccessListTech_techList_454, logic_uScript_AccessListTech_index_454, out logic_uScript_AccessListTech_value_454);
		local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_454;
		local_NPCTech02_Tank = logic_uScript_AccessListTech_value_454;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_454.Out)
		{
			Relay_In_450();
		}
	}

	private void Relay_In_456()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_456.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_456.Out)
		{
			Relay_In_457();
		}
	}

	private void Relay_In_457()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_457.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_457.Out)
		{
			Relay_False_438();
		}
	}

	private void Relay_InitialSpawn_459()
	{
		int num = 0;
		Array nPCTechEnd01Data = NPCTechEnd01Data;
		if (logic_uScript_SpawnTechsFromData_spawnData_459.Length != num + nPCTechEnd01Data.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_459, num + nPCTechEnd01Data.Length);
		}
		Array.Copy(nPCTechEnd01Data, 0, logic_uScript_SpawnTechsFromData_spawnData_459, num, nPCTechEnd01Data.Length);
		num += nPCTechEnd01Data.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_459 = owner_Connection_460;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_459.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_459, logic_uScript_SpawnTechsFromData_ownerNode_459, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_459, logic_uScript_SpawnTechsFromData_allowResurrection_459);
	}

	private void Relay_In_463()
	{
		int num = 0;
		Array nPCTechEnd01Data = NPCTechEnd01Data;
		if (logic_uScript_GetAndCheckTechs_techData_463.Length != num + nPCTechEnd01Data.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_463, num + nPCTechEnd01Data.Length);
		}
		Array.Copy(nPCTechEnd01Data, 0, logic_uScript_GetAndCheckTechs_techData_463, num, nPCTechEnd01Data.Length);
		num += nPCTechEnd01Data.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_463 = owner_Connection_462;
		logic_uScript_GetAndCheckTechs_Return_463 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_463.In(logic_uScript_GetAndCheckTechs_techData_463, logic_uScript_GetAndCheckTechs_ownerNode_463, ref logic_uScript_GetAndCheckTechs_techs_463);
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_463.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_463.WaitingToSpawn;
		if (allDead)
		{
			Relay_InitialSpawn_459();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_459();
		}
	}

	private void Relay_AtIndex_464()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs01_TankArray;
		if (logic_uScript_AccessListTech_techList_464.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_464, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_464, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_464.AtIndex(ref logic_uScript_AccessListTech_techList_464, logic_uScript_AccessListTech_index_464, out logic_uScript_AccessListTech_value_464);
		local_EnemyWaveTechs01_TankArray = logic_uScript_AccessListTech_techList_464;
		local_EnemyWave01Tank01_Tank = logic_uScript_AccessListTech_value_464;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_464.Out)
		{
			Relay_AtIndex_465();
		}
	}

	private void Relay_AtIndex_465()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs01_TankArray;
		if (logic_uScript_AccessListTech_techList_465.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_465, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_465, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_465.AtIndex(ref logic_uScript_AccessListTech_techList_465, logic_uScript_AccessListTech_index_465, out logic_uScript_AccessListTech_value_465);
		local_EnemyWaveTechs01_TankArray = logic_uScript_AccessListTech_techList_465;
		local_EnemyWave01Tank02_Tank = logic_uScript_AccessListTech_value_465;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_465.Out)
		{
			Relay_In_189();
		}
	}

	private void Relay_AtIndex_466()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs02_TankArray;
		if (logic_uScript_AccessListTech_techList_466.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_466, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_466, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_466.AtIndex(ref logic_uScript_AccessListTech_techList_466, logic_uScript_AccessListTech_index_466, out logic_uScript_AccessListTech_value_466);
		local_EnemyWaveTechs02_TankArray = logic_uScript_AccessListTech_techList_466;
		local_EnemyWave02Tank01_Tank = logic_uScript_AccessListTech_value_466;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_466.Out)
		{
			Relay_AtIndex_467();
		}
	}

	private void Relay_AtIndex_467()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs02_TankArray;
		if (logic_uScript_AccessListTech_techList_467.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_467, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_467, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_467.AtIndex(ref logic_uScript_AccessListTech_techList_467, logic_uScript_AccessListTech_index_467, out logic_uScript_AccessListTech_value_467);
		local_EnemyWaveTechs02_TankArray = logic_uScript_AccessListTech_techList_467;
		local_EnemyWave02Tank02_Tank = logic_uScript_AccessListTech_value_467;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_467.Out)
		{
			Relay_AtIndex_468();
		}
	}

	private void Relay_AtIndex_468()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs02_TankArray;
		if (logic_uScript_AccessListTech_techList_468.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_468, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_468, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_468.AtIndex(ref logic_uScript_AccessListTech_techList_468, logic_uScript_AccessListTech_index_468, out logic_uScript_AccessListTech_value_468);
		local_EnemyWaveTechs02_TankArray = logic_uScript_AccessListTech_techList_468;
		local_EnemyWave02Tank03_Tank = logic_uScript_AccessListTech_value_468;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_468.Out)
		{
			Relay_AtIndex_469();
		}
	}

	private void Relay_AtIndex_469()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs02_TankArray;
		if (logic_uScript_AccessListTech_techList_469.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_469, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_469, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_469.AtIndex(ref logic_uScript_AccessListTech_techList_469, logic_uScript_AccessListTech_index_469, out logic_uScript_AccessListTech_value_469);
		local_EnemyWaveTechs02_TankArray = logic_uScript_AccessListTech_techList_469;
		local_EnemyWave02Tank04_Tank = logic_uScript_AccessListTech_value_469;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_469.Out)
		{
			Relay_AtIndex_540();
		}
	}

	private void Relay_AtIndex_470()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs03_TankArray;
		if (logic_uScript_AccessListTech_techList_470.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_470, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_470, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_470.AtIndex(ref logic_uScript_AccessListTech_techList_470, logic_uScript_AccessListTech_index_470, out logic_uScript_AccessListTech_value_470);
		local_EnemyWaveTechs03_TankArray = logic_uScript_AccessListTech_techList_470;
		local_EnemyWave03Tank01_Tank = logic_uScript_AccessListTech_value_470;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_470.Out)
		{
			Relay_AtIndex_471();
		}
	}

	private void Relay_AtIndex_471()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs03_TankArray;
		if (logic_uScript_AccessListTech_techList_471.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_471, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_471, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_471.AtIndex(ref logic_uScript_AccessListTech_techList_471, logic_uScript_AccessListTech_index_471, out logic_uScript_AccessListTech_value_471);
		local_EnemyWaveTechs03_TankArray = logic_uScript_AccessListTech_techList_471;
		local_EnemyWave03Tank02_Tank = logic_uScript_AccessListTech_value_471;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_471.Out)
		{
			Relay_In_205();
		}
	}

	private void Relay_AtIndex_472()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs05_TankArray;
		if (logic_uScript_AccessListTech_techList_472.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_472, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_472, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_472.AtIndex(ref logic_uScript_AccessListTech_techList_472, logic_uScript_AccessListTech_index_472, out logic_uScript_AccessListTech_value_472);
		local_EnemyWaveTechs05_TankArray = logic_uScript_AccessListTech_techList_472;
		local_EnemyWave05Tank01_Tank = logic_uScript_AccessListTech_value_472;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_472.Out)
		{
			Relay_AtIndex_473();
		}
	}

	private void Relay_AtIndex_473()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs05_TankArray;
		if (logic_uScript_AccessListTech_techList_473.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_473, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_473, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_473.AtIndex(ref logic_uScript_AccessListTech_techList_473, logic_uScript_AccessListTech_index_473, out logic_uScript_AccessListTech_value_473);
		local_EnemyWaveTechs05_TankArray = logic_uScript_AccessListTech_techList_473;
		local_EnemyWave05Tank02_Tank = logic_uScript_AccessListTech_value_473;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_473.Out)
		{
			Relay_AtIndex_474();
		}
	}

	private void Relay_AtIndex_474()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs05_TankArray;
		if (logic_uScript_AccessListTech_techList_474.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_474, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_474, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_474.AtIndex(ref logic_uScript_AccessListTech_techList_474, logic_uScript_AccessListTech_index_474, out logic_uScript_AccessListTech_value_474);
		local_EnemyWaveTechs05_TankArray = logic_uScript_AccessListTech_techList_474;
		local_EnemyWave05Tank03_Tank = logic_uScript_AccessListTech_value_474;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_474.Out)
		{
			Relay_AtIndex_475();
		}
	}

	private void Relay_AtIndex_475()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs05_TankArray;
		if (logic_uScript_AccessListTech_techList_475.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_475, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_475, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_475.AtIndex(ref logic_uScript_AccessListTech_techList_475, logic_uScript_AccessListTech_index_475, out logic_uScript_AccessListTech_value_475);
		local_EnemyWaveTechs05_TankArray = logic_uScript_AccessListTech_techList_475;
		local_EnemyWave05Tank04_Tank = logic_uScript_AccessListTech_value_475;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_475.Out)
		{
			Relay_In_219();
		}
	}

	private void Relay_In_476()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_476 = EnemySpawnTrigger03;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_476 = EnemySpawnTrigger03;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_476.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_476, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_476, ref logic_uScript_IsPlayerInTriggerSmart_inside_476);
		if (logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_476.FirstEntered)
		{
			Relay_In_84();
		}
	}

	private void Relay_AtIndex_479()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs04_TankArray;
		if (logic_uScript_AccessListTech_techList_479.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_479, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_479, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_479.AtIndex(ref logic_uScript_AccessListTech_techList_479, logic_uScript_AccessListTech_index_479, out logic_uScript_AccessListTech_value_479);
		local_EnemyWaveTechs04_TankArray = logic_uScript_AccessListTech_techList_479;
		local_EnemyWave04Tank01_Tank = logic_uScript_AccessListTech_value_479;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_479.Out)
		{
			Relay_AtIndex_485();
		}
	}

	private void Relay_In_480()
	{
		logic_uScriptCon_CompareBool_Bool_480 = local_EnemyWave04_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480.In(logic_uScriptCon_CompareBool_Bool_480);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480.False;
		if (num)
		{
			Relay_In_483();
		}
		if (flag)
		{
			Relay_In_481();
		}
	}

	private void Relay_In_481()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_481.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_481.Out)
		{
			Relay_In_490();
		}
	}

	private void Relay_In_482()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_482.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_482.Out)
		{
			Relay_In_206();
		}
	}

	private void Relay_In_483()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData04;
		if (logic_uScript_GetAndCheckTechs_techData_483.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_483, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_GetAndCheckTechs_techData_483, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_483 = owner_Connection_486;
		int num2 = 0;
		Array array = local_EnemyWaveTechs04_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_483.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_483, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_483, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_483 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_483.In(logic_uScript_GetAndCheckTechs_techData_483, logic_uScript_GetAndCheckTechs_ownerNode_483, ref logic_uScript_GetAndCheckTechs_techs_483);
		local_EnemyWaveTechs04_TankArray = logic_uScript_GetAndCheckTechs_techs_483;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_483.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_483.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_483.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_483.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_479();
		}
		if (someAlive)
		{
			Relay_AtIndex_479();
		}
		if (allDead)
		{
			Relay_In_490();
		}
		if (waitingToSpawn)
		{
			Relay_In_490();
		}
	}

	private void Relay_AtIndex_485()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs04_TankArray;
		if (logic_uScript_AccessListTech_techList_485.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_485, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_485, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_485.AtIndex(ref logic_uScript_AccessListTech_techList_485, logic_uScript_AccessListTech_index_485, out logic_uScript_AccessListTech_value_485);
		local_EnemyWaveTechs04_TankArray = logic_uScript_AccessListTech_techList_485;
		local_EnemyWave04Tank02_Tank = logic_uScript_AccessListTech_value_485;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_485.Out)
		{
			Relay_AtIndex_492();
		}
	}

	private void Relay_In_490()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_490.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_490.Out)
		{
			Relay_In_482();
		}
	}

	private void Relay_AtIndex_492()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs04_TankArray;
		if (logic_uScript_AccessListTech_techList_492.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_492, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_492, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_492.AtIndex(ref logic_uScript_AccessListTech_techList_492, logic_uScript_AccessListTech_index_492, out logic_uScript_AccessListTech_value_492);
		local_EnemyWaveTechs04_TankArray = logic_uScript_AccessListTech_techList_492;
		local_EnemyWave04Tank03_Tank = logic_uScript_AccessListTech_value_492;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_492.Out)
		{
			Relay_AtIndex_500();
		}
	}

	private void Relay_In_493()
	{
		logic_uScriptCon_CompareBool_Bool_493 = local_EnemyWave04_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_493.In(logic_uScriptCon_CompareBool_Bool_493);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_493.False)
		{
			Relay_InitialSpawn_494();
		}
	}

	private void Relay_InitialSpawn_494()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData04;
		if (logic_uScript_SpawnTechsFromData_spawnData_494.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_494, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_SpawnTechsFromData_spawnData_494, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_494 = owner_Connection_495;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_494.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_494, logic_uScript_SpawnTechsFromData_ownerNode_494, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_494, logic_uScript_SpawnTechsFromData_allowResurrection_494);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_494.Out)
		{
			Relay_True_496();
		}
	}

	private void Relay_True_496()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_496.True(out logic_uScriptAct_SetBool_Target_496);
		local_EnemyWave04_System_Boolean = logic_uScriptAct_SetBool_Target_496;
	}

	private void Relay_False_496()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_496.False(out logic_uScriptAct_SetBool_Target_496);
		local_EnemyWave04_System_Boolean = logic_uScriptAct_SetBool_Target_496;
	}

	private void Relay_In_499()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_499.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_499.Out)
		{
			Relay_In_493();
		}
	}

	private void Relay_AtIndex_500()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs04_TankArray;
		if (logic_uScript_AccessListTech_techList_500.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_500, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_500, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_500.AtIndex(ref logic_uScript_AccessListTech_techList_500, logic_uScript_AccessListTech_index_500, out logic_uScript_AccessListTech_value_500);
		local_EnemyWaveTechs04_TankArray = logic_uScript_AccessListTech_techList_500;
		local_EnemyWave04Tank04_Tank = logic_uScript_AccessListTech_value_500;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_500.Out)
		{
			Relay_In_482();
		}
	}

	private void Relay_In_505()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData02;
		if (logic_uScript_DestroyTechsFromData_techData_505.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_505, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_DestroyTechsFromData_techData_505, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_505 = owner_Connection_504;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_505.In(logic_uScript_DestroyTechsFromData_techData_505, logic_uScript_DestroyTechsFromData_shouldExplode_505, logic_uScript_DestroyTechsFromData_ownerNode_505);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_505.Out)
		{
			Relay_In_508();
		}
	}

	private void Relay_In_508()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData03;
		if (logic_uScript_DestroyTechsFromData_techData_508.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_508, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_DestroyTechsFromData_techData_508, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_508 = owner_Connection_507;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_508.In(logic_uScript_DestroyTechsFromData_techData_508, logic_uScript_DestroyTechsFromData_shouldExplode_508, logic_uScript_DestroyTechsFromData_ownerNode_508);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_508.Out)
		{
			Relay_In_511();
		}
	}

	private void Relay_In_511()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData04;
		if (logic_uScript_DestroyTechsFromData_techData_511.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_511, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_DestroyTechsFromData_techData_511, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_511 = owner_Connection_510;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_511.In(logic_uScript_DestroyTechsFromData_techData_511, logic_uScript_DestroyTechsFromData_shouldExplode_511, logic_uScript_DestroyTechsFromData_ownerNode_511);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_511.Out)
		{
			Relay_In_514();
		}
	}

	private void Relay_In_514()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData05;
		if (logic_uScript_DestroyTechsFromData_techData_514.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_514, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_DestroyTechsFromData_techData_514, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_514 = owner_Connection_513;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_514.In(logic_uScript_DestroyTechsFromData_techData_514, logic_uScript_DestroyTechsFromData_shouldExplode_514, logic_uScript_DestroyTechsFromData_ownerNode_514);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_514.Out)
		{
			Relay_In_311();
		}
	}

	private void Relay_In_516()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData02;
		if (logic_uScript_DestroyTechsFromData_techData_516.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_516, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_DestroyTechsFromData_techData_516, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_516 = owner_Connection_515;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_516.In(logic_uScript_DestroyTechsFromData_techData_516, logic_uScript_DestroyTechsFromData_shouldExplode_516, logic_uScript_DestroyTechsFromData_ownerNode_516);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_516.Out)
		{
			Relay_In_544();
		}
	}

	private void Relay_In_517()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData04;
		if (logic_uScript_DestroyTechsFromData_techData_517.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_517, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_DestroyTechsFromData_techData_517, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_517 = owner_Connection_519;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_517.In(logic_uScript_DestroyTechsFromData_techData_517, logic_uScript_DestroyTechsFromData_shouldExplode_517, logic_uScript_DestroyTechsFromData_ownerNode_517);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_517.Out)
		{
			Relay_In_518();
		}
	}

	private void Relay_In_518()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData05;
		if (logic_uScript_DestroyTechsFromData_techData_518.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_518, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_DestroyTechsFromData_techData_518, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_518 = owner_Connection_526;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_518.In(logic_uScript_DestroyTechsFromData_techData_518, logic_uScript_DestroyTechsFromData_shouldExplode_518, logic_uScript_DestroyTechsFromData_ownerNode_518);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_518.Out)
		{
			Relay_In_234();
		}
	}

	private void Relay_In_524()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData01;
		if (logic_uScript_DestroyTechsFromData_techData_524.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_524, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_DestroyTechsFromData_techData_524, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_524 = owner_Connection_523;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_524.In(logic_uScript_DestroyTechsFromData_techData_524, logic_uScript_DestroyTechsFromData_shouldExplode_524, logic_uScript_DestroyTechsFromData_ownerNode_524);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_524.Out)
		{
			Relay_In_516();
		}
	}

	private void Relay_In_527()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_527 = IntroTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_527.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_527, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_527);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_527.Out)
		{
			Relay_In_529();
		}
	}

	private void Relay_In_529()
	{
		int num = 0;
		Array array = msgIntroInterrupted;
		if (logic_uScript_AddOnScreenMessage_locString_529.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_529, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_529, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_529 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_529 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_529.In(logic_uScript_AddOnScreenMessage_locString_529, logic_uScript_AddOnScreenMessage_msgPriority_529, logic_uScript_AddOnScreenMessage_holdMsg_529, logic_uScript_AddOnScreenMessage_tag_529, logic_uScript_AddOnScreenMessage_speaker_529, logic_uScript_AddOnScreenMessage_side_529);
	}

	private void Relay_Save_Out_533()
	{
		Relay_Save_6();
	}

	private void Relay_Load_Out_533()
	{
		Relay_Load_6();
	}

	private void Relay_Restart_Out_533()
	{
		Relay_Restart_6();
	}

	private void Relay_Save_533()
	{
		logic_SubGraph_SaveLoadInt_integer_533 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_533 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.Save(logic_SubGraph_SaveLoadInt_restartValue_533, ref logic_SubGraph_SaveLoadInt_integer_533, logic_SubGraph_SaveLoadInt_intAsVariable_533, logic_SubGraph_SaveLoadInt_uniqueID_533);
	}

	private void Relay_Load_533()
	{
		logic_SubGraph_SaveLoadInt_integer_533 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_533 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.Load(logic_SubGraph_SaveLoadInt_restartValue_533, ref logic_SubGraph_SaveLoadInt_integer_533, logic_SubGraph_SaveLoadInt_intAsVariable_533, logic_SubGraph_SaveLoadInt_uniqueID_533);
	}

	private void Relay_Restart_533()
	{
		logic_SubGraph_SaveLoadInt_integer_533 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_533 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_533.Restart(logic_SubGraph_SaveLoadInt_restartValue_533, ref logic_SubGraph_SaveLoadInt_integer_533, logic_SubGraph_SaveLoadInt_intAsVariable_533, logic_SubGraph_SaveLoadInt_uniqueID_533);
	}

	private void Relay_In_535()
	{
		logic_uScriptCon_CompareBool_Bool_535 = local_VisitedNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_535.In(logic_uScriptCon_CompareBool_Bool_535);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_535.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_535.False;
		if (num)
		{
			Relay_In_21();
		}
		if (flag)
		{
			Relay_True_536();
		}
	}

	private void Relay_True_536()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_536.True(out logic_uScriptAct_SetBool_Target_536);
		local_VisitedNPC_System_Boolean = logic_uScriptAct_SetBool_Target_536;
	}

	private void Relay_False_536()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_536.False(out logic_uScriptAct_SetBool_Target_536);
		local_VisitedNPC_System_Boolean = logic_uScriptAct_SetBool_Target_536;
	}

	private void Relay_In_538()
	{
		logic_uScriptCon_CompareBool_Bool_538 = local_VisitedNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_538.In(logic_uScriptCon_CompareBool_Bool_538);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_538.True)
		{
			Relay_In_527();
		}
	}

	private void Relay_AtIndex_540()
	{
		int num = 0;
		Array array = local_EnemyWaveTechs02_TankArray;
		if (logic_uScript_AccessListTech_techList_540.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_540, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_540, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_540.AtIndex(ref logic_uScript_AccessListTech_techList_540, logic_uScript_AccessListTech_index_540, out logic_uScript_AccessListTech_value_540);
		local_EnemyWaveTechs02_TankArray = logic_uScript_AccessListTech_techList_540;
		local_EnemyWave02Tank05_Tank = logic_uScript_AccessListTech_value_540;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_540.Out)
		{
			Relay_In_195();
		}
	}

	private void Relay_In_542()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_542.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_542.Out)
		{
			Relay_In_524();
		}
	}

	private void Relay_In_544()
	{
		int num = 0;
		Array enemyWaveData = EnemyWaveData03;
		if (logic_uScript_DestroyTechsFromData_techData_544.Length != num + enemyWaveData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_544, num + enemyWaveData.Length);
		}
		Array.Copy(enemyWaveData, 0, logic_uScript_DestroyTechsFromData_techData_544, num, enemyWaveData.Length);
		num += enemyWaveData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_544 = owner_Connection_543;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_544.In(logic_uScript_DestroyTechsFromData_techData_544, logic_uScript_DestroyTechsFromData_shouldExplode_544, logic_uScript_DestroyTechsFromData_ownerNode_544);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_544.Out)
		{
			Relay_In_517();
		}
	}

	private void Relay_Save_Out_547()
	{
		Relay_Save_548();
	}

	private void Relay_Load_Out_547()
	{
		Relay_Load_548();
	}

	private void Relay_Restart_Out_547()
	{
		Relay_Set_False_548();
	}

	private void Relay_Save_547()
	{
		logic_SubGraph_SaveLoadBool_boolean_547 = local_EnemyWave01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_547 = local_EnemyWave01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Save(ref logic_SubGraph_SaveLoadBool_boolean_547, logic_SubGraph_SaveLoadBool_boolAsVariable_547, logic_SubGraph_SaveLoadBool_uniqueID_547);
	}

	private void Relay_Load_547()
	{
		logic_SubGraph_SaveLoadBool_boolean_547 = local_EnemyWave01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_547 = local_EnemyWave01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Load(ref logic_SubGraph_SaveLoadBool_boolean_547, logic_SubGraph_SaveLoadBool_boolAsVariable_547, logic_SubGraph_SaveLoadBool_uniqueID_547);
	}

	private void Relay_Set_True_547()
	{
		logic_SubGraph_SaveLoadBool_boolean_547 = local_EnemyWave01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_547 = local_EnemyWave01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_547, logic_SubGraph_SaveLoadBool_boolAsVariable_547, logic_SubGraph_SaveLoadBool_uniqueID_547);
	}

	private void Relay_Set_False_547()
	{
		logic_SubGraph_SaveLoadBool_boolean_547 = local_EnemyWave01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_547 = local_EnemyWave01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_547.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_547, logic_SubGraph_SaveLoadBool_boolAsVariable_547, logic_SubGraph_SaveLoadBool_uniqueID_547);
	}

	private void Relay_Save_Out_548()
	{
		Relay_Save_550();
	}

	private void Relay_Load_Out_548()
	{
		Relay_Load_550();
	}

	private void Relay_Restart_Out_548()
	{
		Relay_Set_False_550();
	}

	private void Relay_Save_548()
	{
		logic_SubGraph_SaveLoadBool_boolean_548 = local_EnemyWave02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_548 = local_EnemyWave02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Save(ref logic_SubGraph_SaveLoadBool_boolean_548, logic_SubGraph_SaveLoadBool_boolAsVariable_548, logic_SubGraph_SaveLoadBool_uniqueID_548);
	}

	private void Relay_Load_548()
	{
		logic_SubGraph_SaveLoadBool_boolean_548 = local_EnemyWave02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_548 = local_EnemyWave02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Load(ref logic_SubGraph_SaveLoadBool_boolean_548, logic_SubGraph_SaveLoadBool_boolAsVariable_548, logic_SubGraph_SaveLoadBool_uniqueID_548);
	}

	private void Relay_Set_True_548()
	{
		logic_SubGraph_SaveLoadBool_boolean_548 = local_EnemyWave02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_548 = local_EnemyWave02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_548, logic_SubGraph_SaveLoadBool_boolAsVariable_548, logic_SubGraph_SaveLoadBool_uniqueID_548);
	}

	private void Relay_Set_False_548()
	{
		logic_SubGraph_SaveLoadBool_boolean_548 = local_EnemyWave02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_548 = local_EnemyWave02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_548.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_548, logic_SubGraph_SaveLoadBool_boolAsVariable_548, logic_SubGraph_SaveLoadBool_uniqueID_548);
	}

	private void Relay_Save_Out_550()
	{
		Relay_Save_552();
	}

	private void Relay_Load_Out_550()
	{
		Relay_Load_552();
	}

	private void Relay_Restart_Out_550()
	{
		Relay_Set_False_552();
	}

	private void Relay_Save_550()
	{
		logic_SubGraph_SaveLoadBool_boolean_550 = local_EnemyWave03_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_550 = local_EnemyWave03_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Save(ref logic_SubGraph_SaveLoadBool_boolean_550, logic_SubGraph_SaveLoadBool_boolAsVariable_550, logic_SubGraph_SaveLoadBool_uniqueID_550);
	}

	private void Relay_Load_550()
	{
		logic_SubGraph_SaveLoadBool_boolean_550 = local_EnemyWave03_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_550 = local_EnemyWave03_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Load(ref logic_SubGraph_SaveLoadBool_boolean_550, logic_SubGraph_SaveLoadBool_boolAsVariable_550, logic_SubGraph_SaveLoadBool_uniqueID_550);
	}

	private void Relay_Set_True_550()
	{
		logic_SubGraph_SaveLoadBool_boolean_550 = local_EnemyWave03_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_550 = local_EnemyWave03_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_550, logic_SubGraph_SaveLoadBool_boolAsVariable_550, logic_SubGraph_SaveLoadBool_uniqueID_550);
	}

	private void Relay_Set_False_550()
	{
		logic_SubGraph_SaveLoadBool_boolean_550 = local_EnemyWave03_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_550 = local_EnemyWave03_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_550.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_550, logic_SubGraph_SaveLoadBool_boolAsVariable_550, logic_SubGraph_SaveLoadBool_uniqueID_550);
	}

	private void Relay_Save_Out_552()
	{
		Relay_Save_554();
	}

	private void Relay_Load_Out_552()
	{
		Relay_Load_554();
	}

	private void Relay_Restart_Out_552()
	{
		Relay_Set_False_554();
	}

	private void Relay_Save_552()
	{
		logic_SubGraph_SaveLoadBool_boolean_552 = local_EnemyWave04_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_552 = local_EnemyWave04_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Save(ref logic_SubGraph_SaveLoadBool_boolean_552, logic_SubGraph_SaveLoadBool_boolAsVariable_552, logic_SubGraph_SaveLoadBool_uniqueID_552);
	}

	private void Relay_Load_552()
	{
		logic_SubGraph_SaveLoadBool_boolean_552 = local_EnemyWave04_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_552 = local_EnemyWave04_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Load(ref logic_SubGraph_SaveLoadBool_boolean_552, logic_SubGraph_SaveLoadBool_boolAsVariable_552, logic_SubGraph_SaveLoadBool_uniqueID_552);
	}

	private void Relay_Set_True_552()
	{
		logic_SubGraph_SaveLoadBool_boolean_552 = local_EnemyWave04_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_552 = local_EnemyWave04_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_552, logic_SubGraph_SaveLoadBool_boolAsVariable_552, logic_SubGraph_SaveLoadBool_uniqueID_552);
	}

	private void Relay_Set_False_552()
	{
		logic_SubGraph_SaveLoadBool_boolean_552 = local_EnemyWave04_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_552 = local_EnemyWave04_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_552.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_552, logic_SubGraph_SaveLoadBool_boolAsVariable_552, logic_SubGraph_SaveLoadBool_uniqueID_552);
	}

	private void Relay_Save_Out_554()
	{
		Relay_Save_533();
	}

	private void Relay_Load_Out_554()
	{
		Relay_Load_533();
	}

	private void Relay_Restart_Out_554()
	{
		Relay_Restart_533();
	}

	private void Relay_Save_554()
	{
		logic_SubGraph_SaveLoadBool_boolean_554 = local_EnemyWave05_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_554 = local_EnemyWave05_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Save(ref logic_SubGraph_SaveLoadBool_boolean_554, logic_SubGraph_SaveLoadBool_boolAsVariable_554, logic_SubGraph_SaveLoadBool_uniqueID_554);
	}

	private void Relay_Load_554()
	{
		logic_SubGraph_SaveLoadBool_boolean_554 = local_EnemyWave05_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_554 = local_EnemyWave05_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Load(ref logic_SubGraph_SaveLoadBool_boolean_554, logic_SubGraph_SaveLoadBool_boolAsVariable_554, logic_SubGraph_SaveLoadBool_uniqueID_554);
	}

	private void Relay_Set_True_554()
	{
		logic_SubGraph_SaveLoadBool_boolean_554 = local_EnemyWave05_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_554 = local_EnemyWave05_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_554, logic_SubGraph_SaveLoadBool_boolAsVariable_554, logic_SubGraph_SaveLoadBool_uniqueID_554);
	}

	private void Relay_Set_False_554()
	{
		logic_SubGraph_SaveLoadBool_boolean_554 = local_EnemyWave05_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_554 = local_EnemyWave05_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_554.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_554, logic_SubGraph_SaveLoadBool_boolAsVariable_554, logic_SubGraph_SaveLoadBool_uniqueID_554);
	}
}
