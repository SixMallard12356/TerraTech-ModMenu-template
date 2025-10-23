using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_DefeatEnemyWaves : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius;

	public SpawnTechData[] enemyTechData1 = new SpawnTechData[0];

	public SpawnTechData[] enemyTechData2 = new SpawnTechData[0];

	public SpawnTechData[] enemyTechData3 = new SpawnTechData[0];

	public SpawnTechData[] enemyTechData4 = new SpawnTechData[0];

	public SpawnTechData[] enemyTechData5 = new SpawnTechData[0];

	private bool local_EnemySpawned01_System_Boolean;

	private bool local_EnemySpawned02_System_Boolean;

	private bool local_EnemySpawned03_System_Boolean;

	private bool local_EnemySpawned04_System_Boolean;

	private bool local_EnemySpawned05_System_Boolean;

	private bool local_EnemySpotted01_System_Boolean;

	private bool local_EnemySpotted02_System_Boolean;

	private bool local_EnemySpotted03_System_Boolean;

	private bool local_EnemySpotted04_System_Boolean;

	private bool local_EnemySpotted05_System_Boolean;

	private Tank[] local_EnemyTechs01_TankArray = new Tank[0];

	private Tank[] local_EnemyTechs02_TankArray = new Tank[0];

	private Tank[] local_EnemyTechs03_TankArray = new Tank[0];

	private Tank[] local_EnemyTechs04_TankArray = new Tank[0];

	private Tank[] local_EnemyTechs05_TankArray = new Tank[0];

	private bool local_Init_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgComplete1 = new LocalisedString[0];

	public LocalisedString[] msgComplete2 = new LocalisedString[0];

	public LocalisedString[] msgComplete3 = new LocalisedString[0];

	public LocalisedString[] msgComplete4 = new LocalisedString[0];

	public LocalisedString[] msgComplete5 = new LocalisedString[0];

	public LocalisedString[] msgEnemiesSpotted1 = new LocalisedString[0];

	public LocalisedString[] msgEnemiesSpotted2 = new LocalisedString[0];

	public LocalisedString[] msgEnemiesSpotted3 = new LocalisedString[0];

	public LocalisedString[] msgEnemiesSpotted4 = new LocalisedString[0];

	public LocalisedString[] msgEnemiesSpotted5 = new LocalisedString[0];

	public int numWaves;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_15;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_23;

	private GameObject owner_Connection_26;

	private GameObject owner_Connection_28;

	private GameObject owner_Connection_35;

	private GameObject owner_Connection_39;

	private GameObject owner_Connection_40;

	private GameObject owner_Connection_58;

	private GameObject owner_Connection_62;

	private GameObject owner_Connection_64;

	private GameObject owner_Connection_66;

	private GameObject owner_Connection_69;

	private GameObject owner_Connection_102;

	private GameObject owner_Connection_105;

	private GameObject owner_Connection_116;

	private GameObject owner_Connection_118;

	private GameObject owner_Connection_120;

	private GameObject owner_Connection_123;

	private GameObject owner_Connection_132;

	private GameObject owner_Connection_146;

	private GameObject owner_Connection_149;

	private GameObject owner_Connection_158;

	private GameObject owner_Connection_160;

	private GameObject owner_Connection_162;

	private GameObject owner_Connection_170;

	private GameObject owner_Connection_175;

	private GameObject owner_Connection_176;

	private GameObject owner_Connection_179;

	private GameObject owner_Connection_180;

	private GameObject owner_Connection_186;

	private GameObject owner_Connection_207;

	private GameObject owner_Connection_210;

	private GameObject owner_Connection_212;

	private GameObject owner_Connection_214;

	private GameObject owner_Connection_216;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_3;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_6 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_6;

	private int logic_uScriptCon_CompareInt_B_6;

	private bool logic_uScriptCon_CompareInt_GreaterThan_6 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_6 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_6 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_6 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_6 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_6 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_9 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_9;

	private int logic_uScriptAct_AddInt_v2_B_9 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_9;

	private float logic_uScriptAct_AddInt_v2_FloatResult_9;

	private bool logic_uScriptAct_AddInt_v2_Out_9 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_11 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_11;

	private int logic_uScriptAct_AddInt_v2_B_11 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_11;

	private float logic_uScriptAct_AddInt_v2_FloatResult_11;

	private bool logic_uScriptAct_AddInt_v2_Out_11 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_12 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_12;

	private int logic_uScriptCon_CompareInt_B_12;

	private bool logic_uScriptCon_CompareInt_GreaterThan_12 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_12 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_12 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_12 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_12 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_12 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_14 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_14;

	private bool logic_uScript_FinishEncounter_Out_14 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_16 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_16;

	private bool logic_uScript_FinishEncounter_Out_16 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_18 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_18;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_18 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_18 = "Wave";

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_20 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_20 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_20 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_20;

	private string logic_uScript_AddOnScreenMessage_tag_20 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_20;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_20;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_20;

	private bool logic_uScript_AddOnScreenMessage_Out_20 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_20 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_24 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_24;

	private bool logic_uScriptAct_SetBool_Out_24 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_24 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_24 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_27 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_27;

	private bool logic_uScript_ClearEncounterTarget_Out_27 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_30 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_30;

	private string logic_uScript_RemoveScenery_positionName_30 = "";

	private float logic_uScript_RemoveScenery_radius_30;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_30 = true;

	private bool logic_uScript_RemoveScenery_Out_30 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_32 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_32 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_32 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_32;

	private string logic_uScript_AddOnScreenMessage_tag_32 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_32;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_32;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_32;

	private bool logic_uScript_AddOnScreenMessage_Out_32 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_32 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_33 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_33;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_33 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_33;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_33 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_36;

	private bool logic_uScriptCon_CompareBool_True_36 = true;

	private bool logic_uScriptCon_CompareBool_False_36 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_37;

	private bool logic_uScriptCon_CompareBool_True_37 = true;

	private bool logic_uScriptCon_CompareBool_False_37 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_38 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_38 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_38;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_38 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_38 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_41 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_41;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_41 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_41;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_41 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_41 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_41 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_41 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_42 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_42;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_42 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_42 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_42 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_44 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_44;

	private bool logic_uScriptAct_SetBool_Out_44 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_44 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_44 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_45;

	private bool logic_uScriptCon_CompareBool_True_45 = true;

	private bool logic_uScriptCon_CompareBool_False_45 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_48 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_48;

	private bool logic_uScriptAct_SetBool_Out_48 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_48 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_48 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_53 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_53 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_53 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_53;

	private string logic_uScript_AddOnScreenMessage_tag_53 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_53;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_53;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_53;

	private bool logic_uScript_AddOnScreenMessage_Out_53 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_53 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_57 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_57 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_57;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_57 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_57 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_59;

	private bool logic_uScriptCon_CompareBool_True_59 = true;

	private bool logic_uScriptCon_CompareBool_False_59 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_60 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_60;

	private bool logic_uScriptAct_SetBool_Out_60 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_60 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_60 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_68;

	private bool logic_uScriptCon_CompareBool_True_68 = true;

	private bool logic_uScriptCon_CompareBool_False_68 = true;

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

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_71 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_71;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_71 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_71;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_71 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_72 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_72;

	private bool logic_uScript_ClearEncounterTarget_Out_72 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_73 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_73;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_73 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_73 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_73 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_75 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_75;

	private bool logic_uScriptAct_SetBool_Out_75 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_75 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_75 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_76 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_76 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_76;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_76 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_76;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_76 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_76 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_76 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_76 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_84;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_84 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_84 = "EnemySpawned01";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_86;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_86 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_86 = "EnemySpawned02";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_88;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_88 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_88 = "EnemySpotted01";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_90;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_90 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_90 = "EnemySpotted02";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_96 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_96;

	private bool logic_uScriptAct_SetBool_Out_96 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_96 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_96 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_98 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_98;

	private bool logic_uScriptCon_CompareBool_True_98 = true;

	private bool logic_uScriptCon_CompareBool_False_98 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_99 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_99;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_99 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_99;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_99 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_101 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_101 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_101 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_101;

	private string logic_uScript_AddOnScreenMessage_tag_101 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_101;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_101;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_101;

	private bool logic_uScript_AddOnScreenMessage_Out_101 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_101 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_104 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_104;

	private int logic_uScriptAct_AddInt_v2_B_104 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_104;

	private float logic_uScriptAct_AddInt_v2_FloatResult_104;

	private bool logic_uScriptAct_AddInt_v2_Out_104 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_107 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_107;

	private bool logic_uScript_ClearEncounterTarget_Out_107 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_109 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_109;

	private bool logic_uScriptAct_SetBool_Out_109 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_109 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_109 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_110 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_110 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_110;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_110 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_110 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_111;

	private bool logic_uScriptCon_CompareBool_True_111 = true;

	private bool logic_uScriptCon_CompareBool_False_111 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_114 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_114 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_114 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_114;

	private string logic_uScript_AddOnScreenMessage_tag_114 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_114;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_114;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_114;

	private bool logic_uScript_AddOnScreenMessage_Out_114 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_114 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_119 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_119;

	private bool logic_uScript_FinishEncounter_Out_119 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_121 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_121 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_121;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_121 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_121;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_121 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_121 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_121 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_121 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_125 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_125;

	private int logic_uScriptCon_CompareInt_B_125;

	private bool logic_uScriptCon_CompareInt_GreaterThan_125 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_125 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_125 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_125 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_125 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_125 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_127 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_127;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_127 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_127 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_127 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_134 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_134 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_134;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_134 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_134;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_134 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_134 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_134 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_134 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_141 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_141;

	private bool logic_uScriptCon_CompareBool_True_141 = true;

	private bool logic_uScriptCon_CompareBool_False_141 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_144 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_144;

	private bool logic_uScript_ClearEncounterTarget_Out_144 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_147 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_147 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_147;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_147 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_147 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_148 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_148;

	private bool logic_uScriptAct_SetBool_Out_148 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_148 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_148 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_150 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_150;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_150 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_150;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_150 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_151 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_151;

	private int logic_uScriptCon_CompareInt_B_151;

	private bool logic_uScriptCon_CompareInt_GreaterThan_151 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_151 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_151 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_151 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_151 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_151 = true;

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

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_154;

	private bool logic_uScriptCon_CompareBool_True_154 = true;

	private bool logic_uScriptCon_CompareBool_False_154 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_155 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_155;

	private int logic_uScriptAct_AddInt_v2_B_155 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_155;

	private float logic_uScriptAct_AddInt_v2_FloatResult_155;

	private bool logic_uScriptAct_AddInt_v2_Out_155 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_157 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_157;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_157 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_157 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_157 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_159 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_159;

	private bool logic_uScriptAct_SetBool_Out_159 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_159 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_159 = true;

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

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_163 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_163;

	private bool logic_uScript_FinishEncounter_Out_163 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_165 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_165;

	private bool logic_uScript_ClearEncounterTarget_Out_165 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_166 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_166 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_166;

	private string logic_uScript_AddOnScreenMessage_tag_166 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_166;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_166;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_166;

	private bool logic_uScript_AddOnScreenMessage_Out_166 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_166 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_167 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_167;

	private bool logic_uScriptAct_SetBool_Out_167 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_167 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_167 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_169 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_169;

	private bool logic_uScriptCon_CompareBool_True_169 = true;

	private bool logic_uScriptCon_CompareBool_False_169 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_173 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_173 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_173 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_173;

	private string logic_uScript_AddOnScreenMessage_tag_173 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_173;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_173;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_173;

	private bool logic_uScript_AddOnScreenMessage_Out_173 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_173 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_178 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_178;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_178 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_178 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_178 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_181;

	private bool logic_uScriptCon_CompareBool_True_181 = true;

	private bool logic_uScriptCon_CompareBool_False_181 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_185 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_185 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_185;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_185 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_185;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_185 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_185 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_185 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_185 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_187 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_187;

	private bool logic_uScript_FinishEncounter_Out_187 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_191 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_191 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_191;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_191 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_191 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_193 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_193;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_193 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_193;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_193 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_194 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_194;

	private bool logic_uScriptAct_SetBool_Out_194 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_194 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_194 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_196;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_196 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_196 = "EnemySpawned04";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_198;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_198 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_198 = "EnemySpawned03";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_199;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_199 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_199 = "EnemySpawned05";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_201;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_201 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_201 = "EnemySpotted04";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_204;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_204 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_204 = "EnemySpotted03";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_206;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_206 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_206 = "EnemySpotted05";

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_208 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_208;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_208 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_208 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_208 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_209 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_209;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_209 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_209 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_209 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_211 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_211;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_211 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_211 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_211 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_213 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_213;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_213 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_213 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_213 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_215 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_215;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_215 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_215 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_215 = true;

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
			if (null != owner_Connection_5)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_5.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_5.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_4;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_4;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_4;
				}
			}
		}
		if (null == owner_Connection_15 || !m_RegisteredForEvents)
		{
			owner_Connection_15 = parentGameObject;
		}
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
		}
		if (null == owner_Connection_23 || !m_RegisteredForEvents)
		{
			owner_Connection_23 = parentGameObject;
		}
		if (null == owner_Connection_26 || !m_RegisteredForEvents)
		{
			owner_Connection_26 = parentGameObject;
		}
		if (null == owner_Connection_28 || !m_RegisteredForEvents)
		{
			owner_Connection_28 = parentGameObject;
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
		if (null == owner_Connection_39 || !m_RegisteredForEvents)
		{
			owner_Connection_39 = parentGameObject;
		}
		if (null == owner_Connection_40 || !m_RegisteredForEvents)
		{
			owner_Connection_40 = parentGameObject;
		}
		if (null == owner_Connection_58 || !m_RegisteredForEvents)
		{
			owner_Connection_58 = parentGameObject;
		}
		if (null == owner_Connection_62 || !m_RegisteredForEvents)
		{
			owner_Connection_62 = parentGameObject;
		}
		if (null == owner_Connection_64 || !m_RegisteredForEvents)
		{
			owner_Connection_64 = parentGameObject;
		}
		if (null == owner_Connection_66 || !m_RegisteredForEvents)
		{
			owner_Connection_66 = parentGameObject;
		}
		if (null == owner_Connection_69 || !m_RegisteredForEvents)
		{
			owner_Connection_69 = parentGameObject;
		}
		if (null == owner_Connection_102 || !m_RegisteredForEvents)
		{
			owner_Connection_102 = parentGameObject;
		}
		if (null == owner_Connection_105 || !m_RegisteredForEvents)
		{
			owner_Connection_105 = parentGameObject;
		}
		if (null == owner_Connection_116 || !m_RegisteredForEvents)
		{
			owner_Connection_116 = parentGameObject;
		}
		if (null == owner_Connection_118 || !m_RegisteredForEvents)
		{
			owner_Connection_118 = parentGameObject;
		}
		if (null == owner_Connection_120 || !m_RegisteredForEvents)
		{
			owner_Connection_120 = parentGameObject;
		}
		if (null == owner_Connection_123 || !m_RegisteredForEvents)
		{
			owner_Connection_123 = parentGameObject;
		}
		if (null == owner_Connection_132 || !m_RegisteredForEvents)
		{
			owner_Connection_132 = parentGameObject;
		}
		if (null == owner_Connection_146 || !m_RegisteredForEvents)
		{
			owner_Connection_146 = parentGameObject;
		}
		if (null == owner_Connection_149 || !m_RegisteredForEvents)
		{
			owner_Connection_149 = parentGameObject;
		}
		if (null == owner_Connection_158 || !m_RegisteredForEvents)
		{
			owner_Connection_158 = parentGameObject;
		}
		if (null == owner_Connection_160 || !m_RegisteredForEvents)
		{
			owner_Connection_160 = parentGameObject;
		}
		if (null == owner_Connection_162 || !m_RegisteredForEvents)
		{
			owner_Connection_162 = parentGameObject;
		}
		if (null == owner_Connection_170 || !m_RegisteredForEvents)
		{
			owner_Connection_170 = parentGameObject;
		}
		if (null == owner_Connection_175 || !m_RegisteredForEvents)
		{
			owner_Connection_175 = parentGameObject;
		}
		if (null == owner_Connection_176 || !m_RegisteredForEvents)
		{
			owner_Connection_176 = parentGameObject;
		}
		if (null == owner_Connection_179 || !m_RegisteredForEvents)
		{
			owner_Connection_179 = parentGameObject;
		}
		if (null == owner_Connection_180 || !m_RegisteredForEvents)
		{
			owner_Connection_180 = parentGameObject;
		}
		if (null == owner_Connection_186 || !m_RegisteredForEvents)
		{
			owner_Connection_186 = parentGameObject;
		}
		if (null == owner_Connection_207 || !m_RegisteredForEvents)
		{
			owner_Connection_207 = parentGameObject;
		}
		if (null == owner_Connection_210 || !m_RegisteredForEvents)
		{
			owner_Connection_210 = parentGameObject;
		}
		if (null == owner_Connection_212 || !m_RegisteredForEvents)
		{
			owner_Connection_212 = parentGameObject;
		}
		if (null == owner_Connection_214 || !m_RegisteredForEvents)
		{
			owner_Connection_214 = parentGameObject;
		}
		if (null == owner_Connection_216 || !m_RegisteredForEvents)
		{
			owner_Connection_216 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_5)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_5.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_5.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_4;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_4;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_4;
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
		if (null != owner_Connection_5)
		{
			uScript_SaveLoad component2 = owner_Connection_5.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_4;
				component2.LoadEvent -= Instance_LoadEvent_4;
				component2.RestartEvent -= Instance_RestartEvent_4;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_6.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_9.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_11.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_12.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_14.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_16.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_20.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_27.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_30.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_32.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_33.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_38.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_42.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_48.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_53.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_57.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_70.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_71.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_72.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_73.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_75.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_76.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_98.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_99.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_101.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_104.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_107.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_110.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_114.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_119.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_121.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_125.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_127.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_134.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_141.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_144.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_147.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_150.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_151.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_153.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_155.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_157.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_163.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_165.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_167.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_169.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_173.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_178.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_185.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_187.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_191.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_193.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_194.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_208.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_209.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_211.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_213.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_215.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_15 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_23 = parentGameObject;
		owner_Connection_26 = parentGameObject;
		owner_Connection_28 = parentGameObject;
		owner_Connection_35 = parentGameObject;
		owner_Connection_39 = parentGameObject;
		owner_Connection_40 = parentGameObject;
		owner_Connection_58 = parentGameObject;
		owner_Connection_62 = parentGameObject;
		owner_Connection_64 = parentGameObject;
		owner_Connection_66 = parentGameObject;
		owner_Connection_69 = parentGameObject;
		owner_Connection_102 = parentGameObject;
		owner_Connection_105 = parentGameObject;
		owner_Connection_116 = parentGameObject;
		owner_Connection_118 = parentGameObject;
		owner_Connection_120 = parentGameObject;
		owner_Connection_123 = parentGameObject;
		owner_Connection_132 = parentGameObject;
		owner_Connection_146 = parentGameObject;
		owner_Connection_149 = parentGameObject;
		owner_Connection_158 = parentGameObject;
		owner_Connection_160 = parentGameObject;
		owner_Connection_162 = parentGameObject;
		owner_Connection_170 = parentGameObject;
		owner_Connection_175 = parentGameObject;
		owner_Connection_176 = parentGameObject;
		owner_Connection_179 = parentGameObject;
		owner_Connection_180 = parentGameObject;
		owner_Connection_186 = parentGameObject;
		owner_Connection_207 = parentGameObject;
		owner_Connection_210 = parentGameObject;
		owner_Connection_212 = parentGameObject;
		owner_Connection_214 = parentGameObject;
		owner_Connection_216 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output1 += uScriptCon_ManualSwitch_Output1_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output2 += uScriptCon_ManualSwitch_Output2_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output3 += uScriptCon_ManualSwitch_Output3_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output4 += uScriptCon_ManualSwitch_Output4_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output5 += uScriptCon_ManualSwitch_Output5_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output6 += uScriptCon_ManualSwitch_Output6_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output7 += uScriptCon_ManualSwitch_Output7_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output8 += uScriptCon_ManualSwitch_Output8_3;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Save_Out += SubGraph_SaveLoadInt_Save_Out_18;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Load_Out += SubGraph_SaveLoadInt_Load_Out_18;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Save_Out += SubGraph_SaveLoadBool_Save_Out_84;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Load_Out += SubGraph_SaveLoadBool_Load_Out_84;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_84;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Save_Out += SubGraph_SaveLoadBool_Save_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Load_Out += SubGraph_SaveLoadBool_Load_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Save_Out += SubGraph_SaveLoadBool_Save_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Load_Out += SubGraph_SaveLoadBool_Load_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Save_Out += SubGraph_SaveLoadBool_Save_Out_90;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Load_Out += SubGraph_SaveLoadBool_Load_Out_90;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_90;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Save_Out += SubGraph_SaveLoadBool_Save_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Load_Out += SubGraph_SaveLoadBool_Load_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Save_Out += SubGraph_SaveLoadBool_Save_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Load_Out += SubGraph_SaveLoadBool_Load_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Save_Out += SubGraph_SaveLoadBool_Save_Out_199;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Load_Out += SubGraph_SaveLoadBool_Load_Out_199;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_199;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Save_Out += SubGraph_SaveLoadBool_Save_Out_201;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Load_Out += SubGraph_SaveLoadBool_Load_Out_201;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_201;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Save_Out += SubGraph_SaveLoadBool_Save_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Load_Out += SubGraph_SaveLoadBool_Load_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Save_Out += SubGraph_SaveLoadBool_Save_Out_206;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Load_Out += SubGraph_SaveLoadBool_Load_Out_206;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_206;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_20.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_32.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_33.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_42.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_53.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_70.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_71.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_73.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_99.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_101.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_114.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_127.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_150.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_153.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_157.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_173.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_178.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_193.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_208.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_209.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_211.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_213.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_215.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output1 -= uScriptCon_ManualSwitch_Output1_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output2 -= uScriptCon_ManualSwitch_Output2_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output3 -= uScriptCon_ManualSwitch_Output3_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output4 -= uScriptCon_ManualSwitch_Output4_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output5 -= uScriptCon_ManualSwitch_Output5_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output6 -= uScriptCon_ManualSwitch_Output6_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output7 -= uScriptCon_ManualSwitch_Output7_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output8 -= uScriptCon_ManualSwitch_Output8_3;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Save_Out -= SubGraph_SaveLoadInt_Save_Out_18;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Load_Out -= SubGraph_SaveLoadInt_Load_Out_18;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Save_Out -= SubGraph_SaveLoadBool_Save_Out_84;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Load_Out -= SubGraph_SaveLoadBool_Load_Out_84;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_84;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Save_Out -= SubGraph_SaveLoadBool_Save_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Load_Out -= SubGraph_SaveLoadBool_Load_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Save_Out -= SubGraph_SaveLoadBool_Save_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Load_Out -= SubGraph_SaveLoadBool_Load_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Save_Out -= SubGraph_SaveLoadBool_Save_Out_90;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Load_Out -= SubGraph_SaveLoadBool_Load_Out_90;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_90;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Save_Out -= SubGraph_SaveLoadBool_Save_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Load_Out -= SubGraph_SaveLoadBool_Load_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Save_Out -= SubGraph_SaveLoadBool_Save_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Load_Out -= SubGraph_SaveLoadBool_Load_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Save_Out -= SubGraph_SaveLoadBool_Save_Out_199;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Load_Out -= SubGraph_SaveLoadBool_Load_Out_199;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_199;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Save_Out -= SubGraph_SaveLoadBool_Save_Out_201;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Load_Out -= SubGraph_SaveLoadBool_Load_Out_201;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_201;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Save_Out -= SubGraph_SaveLoadBool_Save_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Load_Out -= SubGraph_SaveLoadBool_Load_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Save_Out -= SubGraph_SaveLoadBool_Save_Out_206;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Load_Out -= SubGraph_SaveLoadBool_Load_Out_206;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_206;
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

	private void Instance_SaveEvent_4(object o, EventArgs e)
	{
		Relay_SaveEvent_4();
	}

	private void Instance_LoadEvent_4(object o, EventArgs e)
	{
		Relay_LoadEvent_4();
	}

	private void Instance_RestartEvent_4(object o, EventArgs e)
	{
		Relay_RestartEvent_4();
	}

	private void uScriptCon_ManualSwitch_Output1_3(object o, EventArgs e)
	{
		Relay_Output1_3();
	}

	private void uScriptCon_ManualSwitch_Output2_3(object o, EventArgs e)
	{
		Relay_Output2_3();
	}

	private void uScriptCon_ManualSwitch_Output3_3(object o, EventArgs e)
	{
		Relay_Output3_3();
	}

	private void uScriptCon_ManualSwitch_Output4_3(object o, EventArgs e)
	{
		Relay_Output4_3();
	}

	private void uScriptCon_ManualSwitch_Output5_3(object o, EventArgs e)
	{
		Relay_Output5_3();
	}

	private void uScriptCon_ManualSwitch_Output6_3(object o, EventArgs e)
	{
		Relay_Output6_3();
	}

	private void uScriptCon_ManualSwitch_Output7_3(object o, EventArgs e)
	{
		Relay_Output7_3();
	}

	private void uScriptCon_ManualSwitch_Output8_3(object o, EventArgs e)
	{
		Relay_Output8_3();
	}

	private void SubGraph_SaveLoadInt_Save_Out_18(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_18 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_18;
		Relay_Save_Out_18();
	}

	private void SubGraph_SaveLoadInt_Load_Out_18(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_18 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_18;
		Relay_Load_Out_18();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_18(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_18 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_18;
		Relay_Restart_Out_18();
	}

	private void SubGraph_SaveLoadBool_Save_Out_84(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = e.boolean;
		local_EnemySpawned01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_84;
		Relay_Save_Out_84();
	}

	private void SubGraph_SaveLoadBool_Load_Out_84(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = e.boolean;
		local_EnemySpawned01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_84;
		Relay_Load_Out_84();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_84(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = e.boolean;
		local_EnemySpawned01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_84;
		Relay_Restart_Out_84();
	}

	private void SubGraph_SaveLoadBool_Save_Out_86(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = e.boolean;
		local_EnemySpawned02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_86;
		Relay_Save_Out_86();
	}

	private void SubGraph_SaveLoadBool_Load_Out_86(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = e.boolean;
		local_EnemySpawned02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_86;
		Relay_Load_Out_86();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_86(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = e.boolean;
		local_EnemySpawned02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_86;
		Relay_Restart_Out_86();
	}

	private void SubGraph_SaveLoadBool_Save_Out_88(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = e.boolean;
		local_EnemySpotted01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_88;
		Relay_Save_Out_88();
	}

	private void SubGraph_SaveLoadBool_Load_Out_88(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = e.boolean;
		local_EnemySpotted01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_88;
		Relay_Load_Out_88();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_88(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = e.boolean;
		local_EnemySpotted01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_88;
		Relay_Restart_Out_88();
	}

	private void SubGraph_SaveLoadBool_Save_Out_90(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = e.boolean;
		local_EnemySpotted02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_90;
		Relay_Save_Out_90();
	}

	private void SubGraph_SaveLoadBool_Load_Out_90(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = e.boolean;
		local_EnemySpotted02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_90;
		Relay_Load_Out_90();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_90(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = e.boolean;
		local_EnemySpotted02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_90;
		Relay_Restart_Out_90();
	}

	private void SubGraph_SaveLoadBool_Save_Out_196(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = e.boolean;
		local_EnemySpawned04_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_196;
		Relay_Save_Out_196();
	}

	private void SubGraph_SaveLoadBool_Load_Out_196(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = e.boolean;
		local_EnemySpawned04_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_196;
		Relay_Load_Out_196();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_196(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = e.boolean;
		local_EnemySpawned04_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_196;
		Relay_Restart_Out_196();
	}

	private void SubGraph_SaveLoadBool_Save_Out_198(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = e.boolean;
		local_EnemySpawned03_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_198;
		Relay_Save_Out_198();
	}

	private void SubGraph_SaveLoadBool_Load_Out_198(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = e.boolean;
		local_EnemySpawned03_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_198;
		Relay_Load_Out_198();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_198(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = e.boolean;
		local_EnemySpawned03_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_198;
		Relay_Restart_Out_198();
	}

	private void SubGraph_SaveLoadBool_Save_Out_199(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_199 = e.boolean;
		local_EnemySpawned05_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_199;
		Relay_Save_Out_199();
	}

	private void SubGraph_SaveLoadBool_Load_Out_199(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_199 = e.boolean;
		local_EnemySpawned05_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_199;
		Relay_Load_Out_199();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_199(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_199 = e.boolean;
		local_EnemySpawned05_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_199;
		Relay_Restart_Out_199();
	}

	private void SubGraph_SaveLoadBool_Save_Out_201(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_201 = e.boolean;
		local_EnemySpotted04_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_201;
		Relay_Save_Out_201();
	}

	private void SubGraph_SaveLoadBool_Load_Out_201(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_201 = e.boolean;
		local_EnemySpotted04_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_201;
		Relay_Load_Out_201();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_201(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_201 = e.boolean;
		local_EnemySpotted04_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_201;
		Relay_Restart_Out_201();
	}

	private void SubGraph_SaveLoadBool_Save_Out_204(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = e.boolean;
		local_EnemySpotted03_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_204;
		Relay_Save_Out_204();
	}

	private void SubGraph_SaveLoadBool_Load_Out_204(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = e.boolean;
		local_EnemySpotted03_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_204;
		Relay_Load_Out_204();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_204(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = e.boolean;
		local_EnemySpotted03_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_204;
		Relay_Restart_Out_204();
	}

	private void SubGraph_SaveLoadBool_Save_Out_206(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = e.boolean;
		local_EnemySpotted05_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_206;
		Relay_Save_Out_206();
	}

	private void SubGraph_SaveLoadBool_Load_Out_206(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = e.boolean;
		local_EnemySpotted05_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_206;
		Relay_Load_Out_206();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_206(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = e.boolean;
		local_EnemySpotted05_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_206;
		Relay_Restart_Out_206();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_45();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Output1_3()
	{
		Relay_In_37();
	}

	private void Relay_Output2_3()
	{
		Relay_In_59();
	}

	private void Relay_Output3_3()
	{
		Relay_In_111();
	}

	private void Relay_Output4_3()
	{
		Relay_In_154();
	}

	private void Relay_Output5_3()
	{
		Relay_In_169();
	}

	private void Relay_Output6_3()
	{
	}

	private void Relay_Output7_3()
	{
	}

	private void Relay_Output8_3()
	{
	}

	private void Relay_In_3()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_3 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.In(logic_uScriptCon_ManualSwitch_CurrentOutput_3);
	}

	private void Relay_SaveEvent_4()
	{
		Relay_Save_18();
	}

	private void Relay_LoadEvent_4()
	{
		Relay_Load_18();
	}

	private void Relay_RestartEvent_4()
	{
		Relay_Restart_18();
	}

	private void Relay_In_6()
	{
		logic_uScriptCon_CompareInt_A_6 = numWaves;
		logic_uScriptCon_CompareInt_B_6 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_6.In(logic_uScriptCon_CompareInt_A_6, logic_uScriptCon_CompareInt_B_6);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_6.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_6.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_9();
		}
		if (lessThanOrEqualTo)
		{
			Relay_Succeed_14();
		}
	}

	private void Relay_In_9()
	{
		logic_uScriptAct_AddInt_v2_A_9 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_9.In(logic_uScriptAct_AddInt_v2_A_9, logic_uScriptAct_AddInt_v2_B_9, out logic_uScriptAct_AddInt_v2_IntResult_9, out logic_uScriptAct_AddInt_v2_FloatResult_9);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_9;
	}

	private void Relay_In_11()
	{
		logic_uScriptAct_AddInt_v2_A_11 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_11.In(logic_uScriptAct_AddInt_v2_A_11, logic_uScriptAct_AddInt_v2_B_11, out logic_uScriptAct_AddInt_v2_IntResult_11, out logic_uScriptAct_AddInt_v2_FloatResult_11);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_11;
	}

	private void Relay_In_12()
	{
		logic_uScriptCon_CompareInt_A_12 = numWaves;
		logic_uScriptCon_CompareInt_B_12 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_12.In(logic_uScriptCon_CompareInt_A_12, logic_uScriptCon_CompareInt_B_12);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_12.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_12.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_11();
		}
		if (lessThanOrEqualTo)
		{
			Relay_Succeed_16();
		}
	}

	private void Relay_Succeed_14()
	{
		logic_uScript_FinishEncounter_owner_14 = owner_Connection_15;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_14.Succeed(logic_uScript_FinishEncounter_owner_14);
	}

	private void Relay_Fail_14()
	{
		logic_uScript_FinishEncounter_owner_14 = owner_Connection_15;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_14.Fail(logic_uScript_FinishEncounter_owner_14);
	}

	private void Relay_Succeed_16()
	{
		logic_uScript_FinishEncounter_owner_16 = owner_Connection_17;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_16.Succeed(logic_uScript_FinishEncounter_owner_16);
	}

	private void Relay_Fail_16()
	{
		logic_uScript_FinishEncounter_owner_16 = owner_Connection_17;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_16.Fail(logic_uScript_FinishEncounter_owner_16);
	}

	private void Relay_Save_Out_18()
	{
		Relay_Save_84();
	}

	private void Relay_Load_Out_18()
	{
		Relay_Load_84();
	}

	private void Relay_Restart_Out_18()
	{
		Relay_Set_False_84();
	}

	private void Relay_Save_18()
	{
		logic_SubGraph_SaveLoadInt_integer_18 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_18 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Save(logic_SubGraph_SaveLoadInt_restartValue_18, ref logic_SubGraph_SaveLoadInt_integer_18, logic_SubGraph_SaveLoadInt_intAsVariable_18, logic_SubGraph_SaveLoadInt_uniqueID_18);
	}

	private void Relay_Load_18()
	{
		logic_SubGraph_SaveLoadInt_integer_18 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_18 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Load(logic_SubGraph_SaveLoadInt_restartValue_18, ref logic_SubGraph_SaveLoadInt_integer_18, logic_SubGraph_SaveLoadInt_intAsVariable_18, logic_SubGraph_SaveLoadInt_uniqueID_18);
	}

	private void Relay_Restart_18()
	{
		logic_SubGraph_SaveLoadInt_integer_18 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_18 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Restart(logic_SubGraph_SaveLoadInt_restartValue_18, ref logic_SubGraph_SaveLoadInt_integer_18, logic_SubGraph_SaveLoadInt_intAsVariable_18, logic_SubGraph_SaveLoadInt_uniqueID_18);
	}

	private void Relay_In_20()
	{
		int num = 0;
		Array array = msgComplete1;
		if (logic_uScript_AddOnScreenMessage_locString_20.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_20, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_20, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_20 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_20 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_20.In(logic_uScript_AddOnScreenMessage_locString_20, logic_uScript_AddOnScreenMessage_msgPriority_20, logic_uScript_AddOnScreenMessage_holdMsg_20, logic_uScript_AddOnScreenMessage_tag_20, logic_uScript_AddOnScreenMessage_speaker_20, logic_uScript_AddOnScreenMessage_side_20);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_20.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_True_24()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.True(out logic_uScriptAct_SetBool_Target_24);
		local_EnemySpotted01_System_Boolean = logic_uScriptAct_SetBool_Target_24;
	}

	private void Relay_False_24()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.False(out logic_uScriptAct_SetBool_Target_24);
		local_EnemySpotted01_System_Boolean = logic_uScriptAct_SetBool_Target_24;
	}

	private void Relay_In_27()
	{
		logic_uScript_ClearEncounterTarget_owner_27 = owner_Connection_39;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_27.In(logic_uScript_ClearEncounterTarget_owner_27);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_27.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_30()
	{
		logic_uScript_RemoveScenery_ownerNode_30 = owner_Connection_23;
		logic_uScript_RemoveScenery_positionName_30 = clearSceneryPos;
		logic_uScript_RemoveScenery_radius_30 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_30.In(logic_uScript_RemoveScenery_ownerNode_30, logic_uScript_RemoveScenery_positionName_30, logic_uScript_RemoveScenery_radius_30, logic_uScript_RemoveScenery_preventChunksSpawning_30);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_30.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_In_32()
	{
		int num = 0;
		Array array = msgEnemiesSpotted1;
		if (logic_uScript_AddOnScreenMessage_locString_32.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_32, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_32, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_32 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_32 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_32.In(logic_uScript_AddOnScreenMessage_locString_32, logic_uScript_AddOnScreenMessage_msgPriority_32, logic_uScript_AddOnScreenMessage_holdMsg_32, logic_uScript_AddOnScreenMessage_tag_32, logic_uScript_AddOnScreenMessage_speaker_32, logic_uScript_AddOnScreenMessage_side_32);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_32.Out)
		{
			Relay_True_24();
		}
	}

	private void Relay_In_33()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_33 = owner_Connection_35;
		int num = 0;
		Array array = local_EnemyTechs01_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_33.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_33, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_33, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_33 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_33.In(logic_uScript_SetOneTechAsEncounterTarget_owner_33, logic_uScript_SetOneTechAsEncounterTarget_techs_33);
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_33.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_In_36()
	{
		logic_uScriptCon_CompareBool_Bool_36 = local_EnemySpotted01_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.In(logic_uScriptCon_CompareBool_Bool_36);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.False)
		{
			Relay_In_208();
		}
	}

	private void Relay_In_37()
	{
		logic_uScriptCon_CompareBool_Bool_37 = local_EnemySpawned01_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.In(logic_uScriptCon_CompareBool_Bool_37);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.False;
		if (num)
		{
			Relay_In_41();
		}
		if (flag)
		{
			Relay_In_42();
		}
	}

	private void Relay_InitialSpawn_38()
	{
		int num = 0;
		Array array = enemyTechData1;
		if (logic_uScript_SpawnTechsFromData_spawnData_38.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_38, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_38, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_38 = owner_Connection_26;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_38.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_38, logic_uScript_SpawnTechsFromData_ownerNode_38, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_38);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_38.Out)
		{
			Relay_True_44();
		}
	}

	private void Relay_In_41()
	{
		int num = 0;
		Array array = enemyTechData1;
		if (logic_uScript_GetAndCheckTechs_techData_41.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_41, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_41, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_41 = owner_Connection_28;
		int num2 = 0;
		Array array2 = local_EnemyTechs01_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_41.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_41, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_41, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_41 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.In(logic_uScript_GetAndCheckTechs_techData_41, logic_uScript_GetAndCheckTechs_ownerNode_41, ref logic_uScript_GetAndCheckTechs_techs_41);
		local_EnemyTechs01_TankArray = logic_uScript_GetAndCheckTechs_techs_41;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.AllDead;
		if (allAlive)
		{
			Relay_In_33();
		}
		if (someAlive)
		{
			Relay_In_33();
		}
		if (allDead)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_42()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_42 = owner_Connection_40;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_42.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_42);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_42.True)
		{
			Relay_InitialSpawn_38();
		}
	}

	private void Relay_True_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.True(out logic_uScriptAct_SetBool_Target_44);
		local_EnemySpawned01_System_Boolean = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_False_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.False(out logic_uScriptAct_SetBool_Target_44);
		local_EnemySpawned01_System_Boolean = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_45()
	{
		logic_uScriptCon_CompareBool_Bool_45 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.In(logic_uScriptCon_CompareBool_Bool_45);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.False;
		if (num)
		{
			Relay_In_3();
		}
		if (flag)
		{
			Relay_True_48();
		}
	}

	private void Relay_True_48()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_48.True(out logic_uScriptAct_SetBool_Target_48);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_48;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_48.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_False_48()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_48.False(out logic_uScriptAct_SetBool_Target_48);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_48;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_48.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_53()
	{
		int num = 0;
		Array array = msgEnemiesSpotted2;
		if (logic_uScript_AddOnScreenMessage_locString_53.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_53, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_53, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_53 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_53 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_53.In(logic_uScript_AddOnScreenMessage_locString_53, logic_uScript_AddOnScreenMessage_msgPriority_53, logic_uScript_AddOnScreenMessage_holdMsg_53, logic_uScript_AddOnScreenMessage_tag_53, logic_uScript_AddOnScreenMessage_speaker_53, logic_uScript_AddOnScreenMessage_side_53);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_53.Out)
		{
			Relay_True_60();
		}
	}

	private void Relay_InitialSpawn_57()
	{
		int num = 0;
		Array array = enemyTechData2;
		if (logic_uScript_SpawnTechsFromData_spawnData_57.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_57, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_57, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_57 = owner_Connection_58;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_57.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_57, logic_uScript_SpawnTechsFromData_ownerNode_57, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_57);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_57.Out)
		{
			Relay_True_75();
		}
	}

	private void Relay_In_59()
	{
		logic_uScriptCon_CompareBool_Bool_59 = local_EnemySpawned02_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.In(logic_uScriptCon_CompareBool_Bool_59);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.False;
		if (num)
		{
			Relay_In_76();
		}
		if (flag)
		{
			Relay_In_73();
		}
	}

	private void Relay_True_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.True(out logic_uScriptAct_SetBool_Target_60);
		local_EnemySpotted02_System_Boolean = logic_uScriptAct_SetBool_Target_60;
	}

	private void Relay_False_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.False(out logic_uScriptAct_SetBool_Target_60);
		local_EnemySpotted02_System_Boolean = logic_uScriptAct_SetBool_Target_60;
	}

	private void Relay_In_68()
	{
		logic_uScriptCon_CompareBool_Bool_68 = local_EnemySpotted02_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.In(logic_uScriptCon_CompareBool_Bool_68);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.False)
		{
			Relay_In_209();
		}
	}

	private void Relay_In_70()
	{
		int num = 0;
		Array array = msgComplete2;
		if (logic_uScript_AddOnScreenMessage_locString_70.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_70, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_70, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_70 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_70 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_70.In(logic_uScript_AddOnScreenMessage_locString_70, logic_uScript_AddOnScreenMessage_msgPriority_70, logic_uScript_AddOnScreenMessage_holdMsg_70, logic_uScript_AddOnScreenMessage_tag_70, logic_uScript_AddOnScreenMessage_speaker_70, logic_uScript_AddOnScreenMessage_side_70);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_70.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_71()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_71 = owner_Connection_62;
		int num = 0;
		Array array = local_EnemyTechs02_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_71.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_71, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_71, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_71 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_71.In(logic_uScript_SetOneTechAsEncounterTarget_owner_71, logic_uScript_SetOneTechAsEncounterTarget_techs_71);
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_71.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_In_72()
	{
		logic_uScript_ClearEncounterTarget_owner_72 = owner_Connection_66;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_72.In(logic_uScript_ClearEncounterTarget_owner_72);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_72.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_73 = owner_Connection_64;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_73.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_73);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_73.True)
		{
			Relay_InitialSpawn_57();
		}
	}

	private void Relay_True_75()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_75.True(out logic_uScriptAct_SetBool_Target_75);
		local_EnemySpawned02_System_Boolean = logic_uScriptAct_SetBool_Target_75;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_75.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_False_75()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_75.False(out logic_uScriptAct_SetBool_Target_75);
		local_EnemySpawned02_System_Boolean = logic_uScriptAct_SetBool_Target_75;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_75.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_In_76()
	{
		int num = 0;
		Array array = enemyTechData2;
		if (logic_uScript_GetAndCheckTechs_techData_76.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_76, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_76, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_76 = owner_Connection_69;
		int num2 = 0;
		Array array2 = local_EnemyTechs02_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_76.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_76, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_76, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_76 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_76.In(logic_uScript_GetAndCheckTechs_techData_76, logic_uScript_GetAndCheckTechs_ownerNode_76, ref logic_uScript_GetAndCheckTechs_techs_76);
		local_EnemyTechs02_TankArray = logic_uScript_GetAndCheckTechs_techs_76;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_76.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_76.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_76.AllDead;
		if (allAlive)
		{
			Relay_In_71();
		}
		if (someAlive)
		{
			Relay_In_71();
		}
		if (allDead)
		{
			Relay_In_72();
		}
	}

	private void Relay_Save_Out_84()
	{
		Relay_Save_86();
	}

	private void Relay_Load_Out_84()
	{
		Relay_Load_86();
	}

	private void Relay_Restart_Out_84()
	{
		Relay_Set_False_86();
	}

	private void Relay_Save_84()
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = local_EnemySpawned01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_84 = local_EnemySpawned01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Save(ref logic_SubGraph_SaveLoadBool_boolean_84, logic_SubGraph_SaveLoadBool_boolAsVariable_84, logic_SubGraph_SaveLoadBool_uniqueID_84);
	}

	private void Relay_Load_84()
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = local_EnemySpawned01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_84 = local_EnemySpawned01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Load(ref logic_SubGraph_SaveLoadBool_boolean_84, logic_SubGraph_SaveLoadBool_boolAsVariable_84, logic_SubGraph_SaveLoadBool_uniqueID_84);
	}

	private void Relay_Set_True_84()
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = local_EnemySpawned01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_84 = local_EnemySpawned01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_84, logic_SubGraph_SaveLoadBool_boolAsVariable_84, logic_SubGraph_SaveLoadBool_uniqueID_84);
	}

	private void Relay_Set_False_84()
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = local_EnemySpawned01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_84 = local_EnemySpawned01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_84, logic_SubGraph_SaveLoadBool_boolAsVariable_84, logic_SubGraph_SaveLoadBool_uniqueID_84);
	}

	private void Relay_Save_Out_86()
	{
		Relay_Save_198();
	}

	private void Relay_Load_Out_86()
	{
		Relay_Load_198();
	}

	private void Relay_Restart_Out_86()
	{
		Relay_Set_False_198();
	}

	private void Relay_Save_86()
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = local_EnemySpawned02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_86 = local_EnemySpawned02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Save(ref logic_SubGraph_SaveLoadBool_boolean_86, logic_SubGraph_SaveLoadBool_boolAsVariable_86, logic_SubGraph_SaveLoadBool_uniqueID_86);
	}

	private void Relay_Load_86()
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = local_EnemySpawned02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_86 = local_EnemySpawned02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Load(ref logic_SubGraph_SaveLoadBool_boolean_86, logic_SubGraph_SaveLoadBool_boolAsVariable_86, logic_SubGraph_SaveLoadBool_uniqueID_86);
	}

	private void Relay_Set_True_86()
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = local_EnemySpawned02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_86 = local_EnemySpawned02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_86, logic_SubGraph_SaveLoadBool_boolAsVariable_86, logic_SubGraph_SaveLoadBool_uniqueID_86);
	}

	private void Relay_Set_False_86()
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = local_EnemySpawned02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_86 = local_EnemySpawned02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_86, logic_SubGraph_SaveLoadBool_boolAsVariable_86, logic_SubGraph_SaveLoadBool_uniqueID_86);
	}

	private void Relay_Save_Out_88()
	{
		Relay_Save_90();
	}

	private void Relay_Load_Out_88()
	{
		Relay_Load_90();
	}

	private void Relay_Restart_Out_88()
	{
		Relay_Set_False_90();
	}

	private void Relay_Save_88()
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = local_EnemySpotted01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_88 = local_EnemySpotted01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Save(ref logic_SubGraph_SaveLoadBool_boolean_88, logic_SubGraph_SaveLoadBool_boolAsVariable_88, logic_SubGraph_SaveLoadBool_uniqueID_88);
	}

	private void Relay_Load_88()
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = local_EnemySpotted01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_88 = local_EnemySpotted01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Load(ref logic_SubGraph_SaveLoadBool_boolean_88, logic_SubGraph_SaveLoadBool_boolAsVariable_88, logic_SubGraph_SaveLoadBool_uniqueID_88);
	}

	private void Relay_Set_True_88()
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = local_EnemySpotted01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_88 = local_EnemySpotted01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_88, logic_SubGraph_SaveLoadBool_boolAsVariable_88, logic_SubGraph_SaveLoadBool_uniqueID_88);
	}

	private void Relay_Set_False_88()
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = local_EnemySpotted01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_88 = local_EnemySpotted01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_88, logic_SubGraph_SaveLoadBool_boolAsVariable_88, logic_SubGraph_SaveLoadBool_uniqueID_88);
	}

	private void Relay_Save_Out_90()
	{
		Relay_Save_204();
	}

	private void Relay_Load_Out_90()
	{
		Relay_Load_204();
	}

	private void Relay_Restart_Out_90()
	{
		Relay_Set_False_204();
	}

	private void Relay_Save_90()
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = local_EnemySpotted02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_90 = local_EnemySpotted02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Save(ref logic_SubGraph_SaveLoadBool_boolean_90, logic_SubGraph_SaveLoadBool_boolAsVariable_90, logic_SubGraph_SaveLoadBool_uniqueID_90);
	}

	private void Relay_Load_90()
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = local_EnemySpotted02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_90 = local_EnemySpotted02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Load(ref logic_SubGraph_SaveLoadBool_boolean_90, logic_SubGraph_SaveLoadBool_boolAsVariable_90, logic_SubGraph_SaveLoadBool_uniqueID_90);
	}

	private void Relay_Set_True_90()
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = local_EnemySpotted02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_90 = local_EnemySpotted02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_90, logic_SubGraph_SaveLoadBool_boolAsVariable_90, logic_SubGraph_SaveLoadBool_uniqueID_90);
	}

	private void Relay_Set_False_90()
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = local_EnemySpotted02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_90 = local_EnemySpotted02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_90, logic_SubGraph_SaveLoadBool_boolAsVariable_90, logic_SubGraph_SaveLoadBool_uniqueID_90);
	}

	private void Relay_True_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.True(out logic_uScriptAct_SetBool_Target_96);
		local_EnemySpawned03_System_Boolean = logic_uScriptAct_SetBool_Target_96;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_96.Out)
		{
			Relay_In_121();
		}
	}

	private void Relay_False_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.False(out logic_uScriptAct_SetBool_Target_96);
		local_EnemySpawned03_System_Boolean = logic_uScriptAct_SetBool_Target_96;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_96.Out)
		{
			Relay_In_121();
		}
	}

	private void Relay_In_98()
	{
		logic_uScriptCon_CompareBool_Bool_98 = local_EnemySpotted03_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_98.In(logic_uScriptCon_CompareBool_Bool_98);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_98.False)
		{
			Relay_In_211();
		}
	}

	private void Relay_In_99()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_99 = owner_Connection_118;
		int num = 0;
		Array array = local_EnemyTechs03_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_99.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_99, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_99, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_99 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_99.In(logic_uScript_SetOneTechAsEncounterTarget_owner_99, logic_uScript_SetOneTechAsEncounterTarget_techs_99);
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_99.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_In_101()
	{
		int num = 0;
		Array array = msgEnemiesSpotted3;
		if (logic_uScript_AddOnScreenMessage_locString_101.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_101, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_101, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_101 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_101 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_101.In(logic_uScript_AddOnScreenMessage_locString_101, logic_uScript_AddOnScreenMessage_msgPriority_101, logic_uScript_AddOnScreenMessage_holdMsg_101, logic_uScript_AddOnScreenMessage_tag_101, logic_uScript_AddOnScreenMessage_speaker_101, logic_uScript_AddOnScreenMessage_side_101);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_101.Out)
		{
			Relay_True_109();
		}
	}

	private void Relay_In_104()
	{
		logic_uScriptAct_AddInt_v2_A_104 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_104.In(logic_uScriptAct_AddInt_v2_A_104, logic_uScriptAct_AddInt_v2_B_104, out logic_uScriptAct_AddInt_v2_IntResult_104, out logic_uScriptAct_AddInt_v2_FloatResult_104);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_104;
	}

	private void Relay_In_107()
	{
		logic_uScript_ClearEncounterTarget_owner_107 = owner_Connection_123;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_107.In(logic_uScript_ClearEncounterTarget_owner_107);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_107.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_True_109()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.True(out logic_uScriptAct_SetBool_Target_109);
		local_EnemySpotted03_System_Boolean = logic_uScriptAct_SetBool_Target_109;
	}

	private void Relay_False_109()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.False(out logic_uScriptAct_SetBool_Target_109);
		local_EnemySpotted03_System_Boolean = logic_uScriptAct_SetBool_Target_109;
	}

	private void Relay_InitialSpawn_110()
	{
		int num = 0;
		Array array = enemyTechData3;
		if (logic_uScript_SpawnTechsFromData_spawnData_110.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_110, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_110, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_110 = owner_Connection_102;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_110.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_110, logic_uScript_SpawnTechsFromData_ownerNode_110, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_110);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_110.Out)
		{
			Relay_True_96();
		}
	}

	private void Relay_In_111()
	{
		logic_uScriptCon_CompareBool_Bool_111 = local_EnemySpawned03_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.In(logic_uScriptCon_CompareBool_Bool_111);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.False;
		if (num)
		{
			Relay_In_121();
		}
		if (flag)
		{
			Relay_In_127();
		}
	}

	private void Relay_In_114()
	{
		int num = 0;
		Array array = msgComplete3;
		if (logic_uScript_AddOnScreenMessage_locString_114.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_114, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_114, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_114 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_114 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_114.In(logic_uScript_AddOnScreenMessage_locString_114, logic_uScript_AddOnScreenMessage_msgPriority_114, logic_uScript_AddOnScreenMessage_holdMsg_114, logic_uScript_AddOnScreenMessage_tag_114, logic_uScript_AddOnScreenMessage_speaker_114, logic_uScript_AddOnScreenMessage_side_114);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_114.Out)
		{
			Relay_In_125();
		}
	}

	private void Relay_Succeed_119()
	{
		logic_uScript_FinishEncounter_owner_119 = owner_Connection_116;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_119.Succeed(logic_uScript_FinishEncounter_owner_119);
	}

	private void Relay_Fail_119()
	{
		logic_uScript_FinishEncounter_owner_119 = owner_Connection_116;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_119.Fail(logic_uScript_FinishEncounter_owner_119);
	}

	private void Relay_In_121()
	{
		int num = 0;
		Array array = enemyTechData3;
		if (logic_uScript_GetAndCheckTechs_techData_121.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_121, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_121, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_121 = owner_Connection_120;
		int num2 = 0;
		Array array2 = local_EnemyTechs03_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_121.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_121, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_121, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_121 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_121.In(logic_uScript_GetAndCheckTechs_techData_121, logic_uScript_GetAndCheckTechs_ownerNode_121, ref logic_uScript_GetAndCheckTechs_techs_121);
		local_EnemyTechs03_TankArray = logic_uScript_GetAndCheckTechs_techs_121;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_121.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_121.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_121.AllDead;
		if (allAlive)
		{
			Relay_In_99();
		}
		if (someAlive)
		{
			Relay_In_99();
		}
		if (allDead)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_125()
	{
		logic_uScriptCon_CompareInt_A_125 = numWaves;
		logic_uScriptCon_CompareInt_B_125 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_125.In(logic_uScriptCon_CompareInt_A_125, logic_uScriptCon_CompareInt_B_125);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_125.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_125.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_104();
		}
		if (lessThanOrEqualTo)
		{
			Relay_Succeed_119();
		}
	}

	private void Relay_In_127()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_127 = owner_Connection_105;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_127.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_127);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_127.True)
		{
			Relay_InitialSpawn_110();
		}
	}

	private void Relay_In_134()
	{
		int num = 0;
		Array array = enemyTechData4;
		if (logic_uScript_GetAndCheckTechs_techData_134.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_134, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_134, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_134 = owner_Connection_162;
		int num2 = 0;
		Array array2 = local_EnemyTechs04_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_134.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_134, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_134, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_134 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_134.In(logic_uScript_GetAndCheckTechs_techData_134, logic_uScript_GetAndCheckTechs_ownerNode_134, ref logic_uScript_GetAndCheckTechs_techs_134);
		local_EnemyTechs04_TankArray = logic_uScript_GetAndCheckTechs_techs_134;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_134.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_134.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_134.AllDead;
		if (allAlive)
		{
			Relay_In_150();
		}
		if (someAlive)
		{
			Relay_In_150();
		}
		if (allDead)
		{
			Relay_In_144();
		}
	}

	private void Relay_In_141()
	{
		logic_uScriptCon_CompareBool_Bool_141 = local_EnemySpotted04_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_141.In(logic_uScriptCon_CompareBool_Bool_141);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_141.False)
		{
			Relay_In_213();
		}
	}

	private void Relay_In_144()
	{
		logic_uScript_ClearEncounterTarget_owner_144 = owner_Connection_132;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_144.In(logic_uScript_ClearEncounterTarget_owner_144);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_144.Out)
		{
			Relay_In_161();
		}
	}

	private void Relay_InitialSpawn_147()
	{
		int num = 0;
		Array array = enemyTechData4;
		if (logic_uScript_SpawnTechsFromData_spawnData_147.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_147, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_147, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_147 = owner_Connection_160;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_147.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_147, logic_uScript_SpawnTechsFromData_ownerNode_147, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_147);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_147.Out)
		{
			Relay_True_159();
		}
	}

	private void Relay_True_148()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.True(out logic_uScriptAct_SetBool_Target_148);
		local_EnemySpotted04_System_Boolean = logic_uScriptAct_SetBool_Target_148;
	}

	private void Relay_False_148()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.False(out logic_uScriptAct_SetBool_Target_148);
		local_EnemySpotted04_System_Boolean = logic_uScriptAct_SetBool_Target_148;
	}

	private void Relay_In_150()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_150 = owner_Connection_158;
		int num = 0;
		Array array = local_EnemyTechs04_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_150.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_150, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_150, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_150 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_150.In(logic_uScript_SetOneTechAsEncounterTarget_owner_150, logic_uScript_SetOneTechAsEncounterTarget_techs_150);
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_150.Out)
		{
			Relay_In_141();
		}
	}

	private void Relay_In_151()
	{
		logic_uScriptCon_CompareInt_A_151 = numWaves;
		logic_uScriptCon_CompareInt_B_151 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_151.In(logic_uScriptCon_CompareInt_A_151, logic_uScriptCon_CompareInt_B_151);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_151.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_151.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_155();
		}
		if (lessThanOrEqualTo)
		{
			Relay_Succeed_163();
		}
	}

	private void Relay_In_153()
	{
		int num = 0;
		Array array = msgEnemiesSpotted4;
		if (logic_uScript_AddOnScreenMessage_locString_153.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_153, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_153, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_153 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_153 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_153.In(logic_uScript_AddOnScreenMessage_locString_153, logic_uScript_AddOnScreenMessage_msgPriority_153, logic_uScript_AddOnScreenMessage_holdMsg_153, logic_uScript_AddOnScreenMessage_tag_153, logic_uScript_AddOnScreenMessage_speaker_153, logic_uScript_AddOnScreenMessage_side_153);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_153.Out)
		{
			Relay_True_148();
		}
	}

	private void Relay_In_154()
	{
		logic_uScriptCon_CompareBool_Bool_154 = local_EnemySpawned04_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.In(logic_uScriptCon_CompareBool_Bool_154);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.False;
		if (num)
		{
			Relay_In_134();
		}
		if (flag)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_155()
	{
		logic_uScriptAct_AddInt_v2_A_155 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_155.In(logic_uScriptAct_AddInt_v2_A_155, logic_uScriptAct_AddInt_v2_B_155, out logic_uScriptAct_AddInt_v2_IntResult_155, out logic_uScriptAct_AddInt_v2_FloatResult_155);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_155;
	}

	private void Relay_In_157()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_157 = owner_Connection_149;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_157.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_157);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_157.True)
		{
			Relay_InitialSpawn_147();
		}
	}

	private void Relay_True_159()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.True(out logic_uScriptAct_SetBool_Target_159);
		local_EnemySpawned04_System_Boolean = logic_uScriptAct_SetBool_Target_159;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_159.Out)
		{
			Relay_In_134();
		}
	}

	private void Relay_False_159()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.False(out logic_uScriptAct_SetBool_Target_159);
		local_EnemySpawned04_System_Boolean = logic_uScriptAct_SetBool_Target_159;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_159.Out)
		{
			Relay_In_134();
		}
	}

	private void Relay_In_161()
	{
		int num = 0;
		Array array = msgComplete4;
		if (logic_uScript_AddOnScreenMessage_locString_161.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_161, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_161, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_161 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_161 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161.In(logic_uScript_AddOnScreenMessage_locString_161, logic_uScript_AddOnScreenMessage_msgPriority_161, logic_uScript_AddOnScreenMessage_holdMsg_161, logic_uScript_AddOnScreenMessage_tag_161, logic_uScript_AddOnScreenMessage_speaker_161, logic_uScript_AddOnScreenMessage_side_161);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_161.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_Succeed_163()
	{
		logic_uScript_FinishEncounter_owner_163 = owner_Connection_146;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_163.Succeed(logic_uScript_FinishEncounter_owner_163);
	}

	private void Relay_Fail_163()
	{
		logic_uScript_FinishEncounter_owner_163 = owner_Connection_146;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_163.Fail(logic_uScript_FinishEncounter_owner_163);
	}

	private void Relay_In_165()
	{
		logic_uScript_ClearEncounterTarget_owner_165 = owner_Connection_180;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_165.In(logic_uScript_ClearEncounterTarget_owner_165);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_165.Out)
		{
			Relay_In_173();
		}
	}

	private void Relay_In_166()
	{
		int num = 0;
		Array array = msgEnemiesSpotted5;
		if (logic_uScript_AddOnScreenMessage_locString_166.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_166, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_166, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_166 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_166 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166.In(logic_uScript_AddOnScreenMessage_locString_166, logic_uScript_AddOnScreenMessage_msgPriority_166, logic_uScript_AddOnScreenMessage_holdMsg_166, logic_uScript_AddOnScreenMessage_tag_166, logic_uScript_AddOnScreenMessage_speaker_166, logic_uScript_AddOnScreenMessage_side_166);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166.Out)
		{
			Relay_True_167();
		}
	}

	private void Relay_True_167()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_167.True(out logic_uScriptAct_SetBool_Target_167);
		local_EnemySpotted05_System_Boolean = logic_uScriptAct_SetBool_Target_167;
	}

	private void Relay_False_167()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_167.False(out logic_uScriptAct_SetBool_Target_167);
		local_EnemySpotted05_System_Boolean = logic_uScriptAct_SetBool_Target_167;
	}

	private void Relay_In_169()
	{
		logic_uScriptCon_CompareBool_Bool_169 = local_EnemySpawned05_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_169.In(logic_uScriptCon_CompareBool_Bool_169);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_169.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_169.False;
		if (num)
		{
			Relay_In_185();
		}
		if (flag)
		{
			Relay_In_178();
		}
	}

	private void Relay_In_173()
	{
		int num = 0;
		Array array = msgComplete5;
		if (logic_uScript_AddOnScreenMessage_locString_173.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_173, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_173, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_173 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_173 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_173.In(logic_uScript_AddOnScreenMessage_locString_173, logic_uScript_AddOnScreenMessage_msgPriority_173, logic_uScript_AddOnScreenMessage_holdMsg_173, logic_uScript_AddOnScreenMessage_tag_173, logic_uScript_AddOnScreenMessage_speaker_173, logic_uScript_AddOnScreenMessage_side_173);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_173.Out)
		{
			Relay_Succeed_187();
		}
	}

	private void Relay_In_178()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_178 = owner_Connection_186;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_178.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_178);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_178.True)
		{
			Relay_InitialSpawn_191();
		}
	}

	private void Relay_In_181()
	{
		logic_uScriptCon_CompareBool_Bool_181 = local_EnemySpotted05_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.In(logic_uScriptCon_CompareBool_Bool_181);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.False)
		{
			Relay_In_215();
		}
	}

	private void Relay_In_185()
	{
		int num = 0;
		Array array = enemyTechData5;
		if (logic_uScript_GetAndCheckTechs_techData_185.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_185, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_185, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_185 = owner_Connection_176;
		int num2 = 0;
		Array array2 = local_EnemyTechs05_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_185.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_185, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_185, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_185 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_185.In(logic_uScript_GetAndCheckTechs_techData_185, logic_uScript_GetAndCheckTechs_ownerNode_185, ref logic_uScript_GetAndCheckTechs_techs_185);
		local_EnemyTechs05_TankArray = logic_uScript_GetAndCheckTechs_techs_185;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_185.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_185.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_185.AllDead;
		if (allAlive)
		{
			Relay_In_193();
		}
		if (someAlive)
		{
			Relay_In_193();
		}
		if (allDead)
		{
			Relay_In_165();
		}
	}

	private void Relay_Succeed_187()
	{
		logic_uScript_FinishEncounter_owner_187 = owner_Connection_175;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_187.Succeed(logic_uScript_FinishEncounter_owner_187);
	}

	private void Relay_Fail_187()
	{
		logic_uScript_FinishEncounter_owner_187 = owner_Connection_175;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_187.Fail(logic_uScript_FinishEncounter_owner_187);
	}

	private void Relay_InitialSpawn_191()
	{
		int num = 0;
		Array array = enemyTechData5;
		if (logic_uScript_SpawnTechsFromData_spawnData_191.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_191, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_191, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_191 = owner_Connection_179;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_191.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_191, logic_uScript_SpawnTechsFromData_ownerNode_191, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_191);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_191.Out)
		{
			Relay_True_194();
		}
	}

	private void Relay_In_193()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_193 = owner_Connection_170;
		int num = 0;
		Array array = local_EnemyTechs05_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_193.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_193, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_193, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_193 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_193.In(logic_uScript_SetOneTechAsEncounterTarget_owner_193, logic_uScript_SetOneTechAsEncounterTarget_techs_193);
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_193.Out)
		{
			Relay_In_181();
		}
	}

	private void Relay_True_194()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_194.True(out logic_uScriptAct_SetBool_Target_194);
		local_EnemySpawned05_System_Boolean = logic_uScriptAct_SetBool_Target_194;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_194.Out)
		{
			Relay_In_185();
		}
	}

	private void Relay_False_194()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_194.False(out logic_uScriptAct_SetBool_Target_194);
		local_EnemySpawned05_System_Boolean = logic_uScriptAct_SetBool_Target_194;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_194.Out)
		{
			Relay_In_185();
		}
	}

	private void Relay_Save_Out_196()
	{
		Relay_Save_199();
	}

	private void Relay_Load_Out_196()
	{
		Relay_Load_199();
	}

	private void Relay_Restart_Out_196()
	{
		Relay_Set_False_199();
	}

	private void Relay_Save_196()
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = local_EnemySpawned04_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_196 = local_EnemySpawned04_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Save(ref logic_SubGraph_SaveLoadBool_boolean_196, logic_SubGraph_SaveLoadBool_boolAsVariable_196, logic_SubGraph_SaveLoadBool_uniqueID_196);
	}

	private void Relay_Load_196()
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = local_EnemySpawned04_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_196 = local_EnemySpawned04_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Load(ref logic_SubGraph_SaveLoadBool_boolean_196, logic_SubGraph_SaveLoadBool_boolAsVariable_196, logic_SubGraph_SaveLoadBool_uniqueID_196);
	}

	private void Relay_Set_True_196()
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = local_EnemySpawned04_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_196 = local_EnemySpawned04_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_196, logic_SubGraph_SaveLoadBool_boolAsVariable_196, logic_SubGraph_SaveLoadBool_uniqueID_196);
	}

	private void Relay_Set_False_196()
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = local_EnemySpawned04_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_196 = local_EnemySpawned04_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_196, logic_SubGraph_SaveLoadBool_boolAsVariable_196, logic_SubGraph_SaveLoadBool_uniqueID_196);
	}

	private void Relay_Save_Out_198()
	{
		Relay_Save_196();
	}

	private void Relay_Load_Out_198()
	{
		Relay_Load_196();
	}

	private void Relay_Restart_Out_198()
	{
		Relay_Set_False_196();
	}

	private void Relay_Save_198()
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = local_EnemySpawned03_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_EnemySpawned03_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Save(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
	}

	private void Relay_Load_198()
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = local_EnemySpawned03_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_EnemySpawned03_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Load(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
	}

	private void Relay_Set_True_198()
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = local_EnemySpawned03_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_EnemySpawned03_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
	}

	private void Relay_Set_False_198()
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = local_EnemySpawned03_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_EnemySpawned03_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
	}

	private void Relay_Save_Out_199()
	{
		Relay_Save_88();
	}

	private void Relay_Load_Out_199()
	{
		Relay_Load_88();
	}

	private void Relay_Restart_Out_199()
	{
		Relay_Set_False_88();
	}

	private void Relay_Save_199()
	{
		logic_SubGraph_SaveLoadBool_boolean_199 = local_EnemySpawned05_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_199 = local_EnemySpawned05_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Save(ref logic_SubGraph_SaveLoadBool_boolean_199, logic_SubGraph_SaveLoadBool_boolAsVariable_199, logic_SubGraph_SaveLoadBool_uniqueID_199);
	}

	private void Relay_Load_199()
	{
		logic_SubGraph_SaveLoadBool_boolean_199 = local_EnemySpawned05_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_199 = local_EnemySpawned05_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Load(ref logic_SubGraph_SaveLoadBool_boolean_199, logic_SubGraph_SaveLoadBool_boolAsVariable_199, logic_SubGraph_SaveLoadBool_uniqueID_199);
	}

	private void Relay_Set_True_199()
	{
		logic_SubGraph_SaveLoadBool_boolean_199 = local_EnemySpawned05_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_199 = local_EnemySpawned05_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_199, logic_SubGraph_SaveLoadBool_boolAsVariable_199, logic_SubGraph_SaveLoadBool_uniqueID_199);
	}

	private void Relay_Set_False_199()
	{
		logic_SubGraph_SaveLoadBool_boolean_199 = local_EnemySpawned05_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_199 = local_EnemySpawned05_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_199.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_199, logic_SubGraph_SaveLoadBool_boolAsVariable_199, logic_SubGraph_SaveLoadBool_uniqueID_199);
	}

	private void Relay_Save_Out_201()
	{
		Relay_Save_206();
	}

	private void Relay_Load_Out_201()
	{
		Relay_Load_206();
	}

	private void Relay_Restart_Out_201()
	{
		Relay_Set_False_206();
	}

	private void Relay_Save_201()
	{
		logic_SubGraph_SaveLoadBool_boolean_201 = local_EnemySpotted04_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_201 = local_EnemySpotted04_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Save(ref logic_SubGraph_SaveLoadBool_boolean_201, logic_SubGraph_SaveLoadBool_boolAsVariable_201, logic_SubGraph_SaveLoadBool_uniqueID_201);
	}

	private void Relay_Load_201()
	{
		logic_SubGraph_SaveLoadBool_boolean_201 = local_EnemySpotted04_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_201 = local_EnemySpotted04_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Load(ref logic_SubGraph_SaveLoadBool_boolean_201, logic_SubGraph_SaveLoadBool_boolAsVariable_201, logic_SubGraph_SaveLoadBool_uniqueID_201);
	}

	private void Relay_Set_True_201()
	{
		logic_SubGraph_SaveLoadBool_boolean_201 = local_EnemySpotted04_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_201 = local_EnemySpotted04_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_201, logic_SubGraph_SaveLoadBool_boolAsVariable_201, logic_SubGraph_SaveLoadBool_uniqueID_201);
	}

	private void Relay_Set_False_201()
	{
		logic_SubGraph_SaveLoadBool_boolean_201 = local_EnemySpotted04_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_201 = local_EnemySpotted04_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_201, logic_SubGraph_SaveLoadBool_boolAsVariable_201, logic_SubGraph_SaveLoadBool_uniqueID_201);
	}

	private void Relay_Save_Out_204()
	{
		Relay_Save_201();
	}

	private void Relay_Load_Out_204()
	{
		Relay_Load_201();
	}

	private void Relay_Restart_Out_204()
	{
		Relay_Set_False_201();
	}

	private void Relay_Save_204()
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = local_EnemySpotted03_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_204 = local_EnemySpotted03_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Save(ref logic_SubGraph_SaveLoadBool_boolean_204, logic_SubGraph_SaveLoadBool_boolAsVariable_204, logic_SubGraph_SaveLoadBool_uniqueID_204);
	}

	private void Relay_Load_204()
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = local_EnemySpotted03_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_204 = local_EnemySpotted03_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Load(ref logic_SubGraph_SaveLoadBool_boolean_204, logic_SubGraph_SaveLoadBool_boolAsVariable_204, logic_SubGraph_SaveLoadBool_uniqueID_204);
	}

	private void Relay_Set_True_204()
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = local_EnemySpotted03_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_204 = local_EnemySpotted03_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_204, logic_SubGraph_SaveLoadBool_boolAsVariable_204, logic_SubGraph_SaveLoadBool_uniqueID_204);
	}

	private void Relay_Set_False_204()
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = local_EnemySpotted03_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_204 = local_EnemySpotted03_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_204, logic_SubGraph_SaveLoadBool_boolAsVariable_204, logic_SubGraph_SaveLoadBool_uniqueID_204);
	}

	private void Relay_Save_Out_206()
	{
	}

	private void Relay_Load_Out_206()
	{
	}

	private void Relay_Restart_Out_206()
	{
	}

	private void Relay_Save_206()
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = local_EnemySpotted05_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_206 = local_EnemySpotted05_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Save(ref logic_SubGraph_SaveLoadBool_boolean_206, logic_SubGraph_SaveLoadBool_boolAsVariable_206, logic_SubGraph_SaveLoadBool_uniqueID_206);
	}

	private void Relay_Load_206()
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = local_EnemySpotted05_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_206 = local_EnemySpotted05_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Load(ref logic_SubGraph_SaveLoadBool_boolean_206, logic_SubGraph_SaveLoadBool_boolAsVariable_206, logic_SubGraph_SaveLoadBool_uniqueID_206);
	}

	private void Relay_Set_True_206()
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = local_EnemySpotted05_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_206 = local_EnemySpotted05_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_206, logic_SubGraph_SaveLoadBool_boolAsVariable_206, logic_SubGraph_SaveLoadBool_uniqueID_206);
	}

	private void Relay_Set_False_206()
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = local_EnemySpotted05_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_206 = local_EnemySpotted05_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_206, logic_SubGraph_SaveLoadBool_boolAsVariable_206, logic_SubGraph_SaveLoadBool_uniqueID_206);
	}

	private void Relay_In_208()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_208 = owner_Connection_207;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_208.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_208);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_208.True)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_209()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_209 = owner_Connection_210;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_209.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_209);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_209.True)
		{
			Relay_In_53();
		}
	}

	private void Relay_In_211()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_211 = owner_Connection_212;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_211.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_211);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_211.True)
		{
			Relay_In_101();
		}
	}

	private void Relay_In_213()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_213 = owner_Connection_214;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_213.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_213);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_213.True)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_215()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_215 = owner_Connection_216;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_215.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_215);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_215.True)
		{
			Relay_In_166();
		}
	}
}
