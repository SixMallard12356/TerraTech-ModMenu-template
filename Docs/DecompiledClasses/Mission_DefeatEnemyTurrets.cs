using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_DefeatEnemyTurrets : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius;

	public float distEnemiesSpotted;

	public float DistInRangeOfNPC = 100f;

	public SpawnTechData[] enemyTechData = new SpawnTechData[0];

	private string local_101_System_String = "msgMeeting";

	private string local_282_System_String = "msgInturrupted";

	private string local_285_System_String = "msgInturrupted";

	private string local_98_System_String = "msgMeeting";

	private string local_99_System_String = "msgMeeting";

	private bool local_EnemyDeadEarly_System_Boolean;

	private bool local_EnemySpotted_System_Boolean;

	private Tank local_EnemyTanks_Tank;

	private Tank[] local_EnemyTechs_TankArray = new Tank[0];

	private bool local_NPCIgnored_System_Boolean;

	private bool local_NPCMet_System_Boolean;

	private bool local_NPCSeen_System_Boolean;

	private Tank local_NPCTank_Tank;

	private Tank[] local_NPCTanks_TankArray = new Tank[0];

	private bool local_ObjectiveComplete_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private bool local_TechsSpawned_System_Boolean;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgComplete = new LocalisedString[0];

	public LocalisedString[] msgEnemiesSpotted = new LocalisedString[0];

	public LocalisedString[] msgNPCGreeting = new LocalisedString[0];

	public LocalisedString[] msgNPCGreetingEnemyDead = new LocalisedString[0];

	public LocalisedString[] msgNPCGreetingInturrupt = new LocalisedString[0];

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCTech = new SpawnTechData[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_13;

	private GameObject owner_Connection_23;

	private GameObject owner_Connection_25;

	private GameObject owner_Connection_47;

	private GameObject owner_Connection_65;

	private GameObject owner_Connection_68;

	private GameObject owner_Connection_73;

	private GameObject owner_Connection_119;

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

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_7 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_7;

	private bool logic_uScriptAct_SetBool_Out_7 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_7 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_7 = true;

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_11 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_11 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_11;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_11 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_11 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_11 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_20;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_20 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_20 = "EnemySpotted";

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_21 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_21 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_21 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_21;

	private string logic_uScript_AddOnScreenMessage_tag_21 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_21;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_21;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_21;

	private bool logic_uScript_AddOnScreenMessage_Out_21 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_21 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_22 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_22;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_22 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_22;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_22 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_27;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_27 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_27 = "EnemySpawned";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_28;

	private bool logic_uScriptCon_CompareBool_True_28 = true;

	private bool logic_uScriptCon_CompareBool_False_28 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_29;

	private bool logic_uScriptCon_CompareBool_True_29 = true;

	private bool logic_uScriptCon_CompareBool_False_29 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_30 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_30 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_30;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_30 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_30 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_31 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_31;

	private bool logic_uScriptAct_SetBool_Out_31 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_31 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_31 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_32 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_32 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_32;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_32 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_32;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_32 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_32 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_32 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_32 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_33;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_33 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_33 = "ObjectiveComplete";

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_34 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_34;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_34 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_34 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_38 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_38;

	private bool logic_uScriptCon_CompareBool_True_38 = true;

	private bool logic_uScriptCon_CompareBool_False_38 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_40 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_40;

	private bool logic_uScriptAct_SetBool_Out_40 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_40 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_40 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_46 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_46 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_46;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_46 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_46 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_50;

	private bool logic_uScriptCon_CompareBool_True_50 = true;

	private bool logic_uScriptCon_CompareBool_False_50 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_52;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_52 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_52 = "NPCMet";

	private uScript_InRangeOfTech logic_uScript_InRangeOfTech_uScript_InRangeOfTech_53 = new uScript_InRangeOfTech();

	private Tank logic_uScript_InRangeOfTech_tank_53;

	private float logic_uScript_InRangeOfTech_range_53;

	private bool logic_uScript_InRangeOfTech_Out_53 = true;

	private bool logic_uScript_InRangeOfTech_InRange_53 = true;

	private bool logic_uScript_InRangeOfTech_OutOfRange_53 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_56 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_56 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_56 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_56;

	private string logic_uScript_AddOnScreenMessage_tag_56 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_56;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_56;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_56;

	private bool logic_uScript_AddOnScreenMessage_Out_56 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_56 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_60 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_60 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_60;

	private bool logic_uScript_SetTankInvulnerable_Out_60 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_63 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_63 = new Tank[0];

	private int logic_uScript_AccessListTech_index_63;

	private Tank logic_uScript_AccessListTech_value_63;

	private bool logic_uScript_AccessListTech_Out_63 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_66 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_66;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_66 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_66;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_66 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_66 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_66 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_66 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_67 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_67;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_67 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_67;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_67 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_71 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_71;

	private bool logic_uScriptAct_SetBool_Out_71 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_71 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_71 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_75 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_75;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_75 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_75;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_75 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_75 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_75 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_75 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_77 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_77 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_77 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_77;

	private string logic_uScript_AddOnScreenMessage_tag_77 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_77;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_77;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_77;

	private bool logic_uScript_AddOnScreenMessage_Out_77 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_77 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_79 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_79;

	private bool logic_uScriptAct_SetBool_Out_79 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_79 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_79 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_80 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_80;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_80 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_80 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_80;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_80;

	private bool logic_uScript_FlyTechUpAndAway_Out_80 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_86;

	private bool logic_uScriptCon_CompareBool_True_86 = true;

	private bool logic_uScriptCon_CompareBool_False_86 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_88;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_88 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_88 = "EnemyDeadEarly";

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_89;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_92 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_92 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_92 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_92;

	private string logic_uScript_AddOnScreenMessage_tag_92 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_92;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_92;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_92;

	private bool logic_uScript_AddOnScreenMessage_Out_92 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_92 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_95;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_95;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_97 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_97;

	private bool logic_uScriptAct_SetBool_Out_97 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_97 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_97 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_100 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_100 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_100;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_100 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_102;

	private bool logic_uScriptCon_CompareBool_True_102 = true;

	private bool logic_uScriptCon_CompareBool_False_102 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_105;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_105 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_105 = "NPCSeen";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_108;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_109 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_109;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_109 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_109 = "Stage";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_110 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_110;

	private bool logic_uScriptAct_SetBool_Out_110 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_110 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_110 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_112;

	private bool logic_uScriptCon_CompareBool_True_112 = true;

	private bool logic_uScriptCon_CompareBool_False_112 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_114 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_114;

	private bool logic_uScriptAct_SetBool_Out_114 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_114 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_114 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_117;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_117 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_117 = "NPCIgnored";

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_118 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_118;

	private string logic_uScript_RemoveScenery_positionName_118 = "";

	private float logic_uScript_RemoveScenery_radius_118;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_118 = true;

	private bool logic_uScript_RemoveScenery_Out_118 = true;

	private uScript_InRangeOfTech logic_uScript_InRangeOfTech_uScript_InRangeOfTech_276 = new uScript_InRangeOfTech();

	private Tank logic_uScript_InRangeOfTech_tank_276;

	private float logic_uScript_InRangeOfTech_range_276;

	private bool logic_uScript_InRangeOfTech_Out_276 = true;

	private bool logic_uScript_InRangeOfTech_InRange_276 = true;

	private bool logic_uScript_InRangeOfTech_OutOfRange_276 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_283 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_283 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_283;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_283 = true;

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
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
		}
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
		}
		if (null == owner_Connection_13 || !m_RegisteredForEvents)
		{
			owner_Connection_13 = parentGameObject;
		}
		if (null == owner_Connection_23 || !m_RegisteredForEvents)
		{
			owner_Connection_23 = parentGameObject;
			if (null != owner_Connection_23)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_23.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_23.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_17;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_17;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_17;
				}
			}
		}
		if (null == owner_Connection_25 || !m_RegisteredForEvents)
		{
			owner_Connection_25 = parentGameObject;
		}
		if (null == owner_Connection_47 || !m_RegisteredForEvents)
		{
			owner_Connection_47 = parentGameObject;
		}
		if (null == owner_Connection_65 || !m_RegisteredForEvents)
		{
			owner_Connection_65 = parentGameObject;
		}
		if (null == owner_Connection_68 || !m_RegisteredForEvents)
		{
			owner_Connection_68 = parentGameObject;
		}
		if (null == owner_Connection_73 || !m_RegisteredForEvents)
		{
			owner_Connection_73 = parentGameObject;
		}
		if (null == owner_Connection_119 || !m_RegisteredForEvents)
		{
			owner_Connection_119 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_23)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_23.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_23.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_17;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_17;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_17;
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
		if (null != owner_Connection_23)
		{
			uScript_SaveLoad component2 = owner_Connection_23.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_17;
				component2.LoadEvent -= Instance_LoadEvent_17;
				component2.RestartEvent -= Instance_RestartEvent_17;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_2.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_11.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_21.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_22.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_30.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_32.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_34.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_38.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_46.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.SetParent(g);
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_53.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_56.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_60.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_63.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_67.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_77.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_79.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_80.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_92.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_100.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_110.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_114.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_118.SetParent(g);
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_276.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_283.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_13 = parentGameObject;
		owner_Connection_23 = parentGameObject;
		owner_Connection_25 = parentGameObject;
		owner_Connection_47 = parentGameObject;
		owner_Connection_65 = parentGameObject;
		owner_Connection_68 = parentGameObject;
		owner_Connection_73 = parentGameObject;
		owner_Connection_119 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Save_Out += SubGraph_SaveLoadBool_Save_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Load_Out += SubGraph_SaveLoadBool_Load_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Save_Out += SubGraph_SaveLoadBool_Save_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Load_Out += SubGraph_SaveLoadBool_Load_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Save_Out += SubGraph_SaveLoadBool_Save_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Load_Out += SubGraph_SaveLoadBool_Load_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Save_Out += SubGraph_SaveLoadBool_Save_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Load_Out += SubGraph_SaveLoadBool_Load_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Save_Out += SubGraph_SaveLoadBool_Save_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Load_Out += SubGraph_SaveLoadBool_Load_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_88;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output1 += uScriptCon_ManualSwitch_Output1_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output2 += uScriptCon_ManualSwitch_Output2_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output3 += uScriptCon_ManualSwitch_Output3_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output4 += uScriptCon_ManualSwitch_Output4_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output5 += uScriptCon_ManualSwitch_Output5_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output6 += uScriptCon_ManualSwitch_Output6_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output7 += uScriptCon_ManualSwitch_Output7_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output8 += uScriptCon_ManualSwitch_Output8_89;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.Out += SubGraph_CompleteObjectiveStage_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Save_Out += SubGraph_SaveLoadBool_Save_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Load_Out += SubGraph_SaveLoadBool_Load_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_105;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.Out += SubGraph_LoadObjectiveStates_Out_108;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Save_Out += SubGraph_SaveLoadInt_Save_Out_109;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Load_Out += SubGraph_SaveLoadInt_Load_Out_109;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Save_Out += SubGraph_SaveLoadBool_Save_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Load_Out += SubGraph_SaveLoadBool_Load_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_117;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_11.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_21.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_22.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.OnDisable();
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_53.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_56.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_60.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_67.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_77.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_92.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.OnDisable();
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_276.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Save_Out -= SubGraph_SaveLoadBool_Save_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Load_Out -= SubGraph_SaveLoadBool_Load_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Save_Out -= SubGraph_SaveLoadBool_Save_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Load_Out -= SubGraph_SaveLoadBool_Load_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Save_Out -= SubGraph_SaveLoadBool_Save_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Load_Out -= SubGraph_SaveLoadBool_Load_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Save_Out -= SubGraph_SaveLoadBool_Save_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Load_Out -= SubGraph_SaveLoadBool_Load_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Save_Out -= SubGraph_SaveLoadBool_Save_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Load_Out -= SubGraph_SaveLoadBool_Load_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_88;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output1 -= uScriptCon_ManualSwitch_Output1_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output2 -= uScriptCon_ManualSwitch_Output2_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output3 -= uScriptCon_ManualSwitch_Output3_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output4 -= uScriptCon_ManualSwitch_Output4_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output5 -= uScriptCon_ManualSwitch_Output5_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output6 -= uScriptCon_ManualSwitch_Output6_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output7 -= uScriptCon_ManualSwitch_Output7_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output8 -= uScriptCon_ManualSwitch_Output8_89;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.Out -= SubGraph_CompleteObjectiveStage_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Save_Out -= SubGraph_SaveLoadBool_Save_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Load_Out -= SubGraph_SaveLoadBool_Load_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_105;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.Out -= SubGraph_LoadObjectiveStates_Out_108;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Save_Out -= SubGraph_SaveLoadInt_Save_Out_109;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Load_Out -= SubGraph_SaveLoadInt_Load_Out_109;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Save_Out -= SubGraph_SaveLoadBool_Save_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Load_Out -= SubGraph_SaveLoadBool_Load_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_117;
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

	private void Instance_SaveEvent_17(object o, EventArgs e)
	{
		Relay_SaveEvent_17();
	}

	private void Instance_LoadEvent_17(object o, EventArgs e)
	{
		Relay_LoadEvent_17();
	}

	private void Instance_RestartEvent_17(object o, EventArgs e)
	{
		Relay_RestartEvent_17();
	}

	private void SubGraph_SaveLoadBool_Save_Out_20(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = e.boolean;
		local_EnemySpotted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_20;
		Relay_Save_Out_20();
	}

	private void SubGraph_SaveLoadBool_Load_Out_20(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = e.boolean;
		local_EnemySpotted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_20;
		Relay_Load_Out_20();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_20(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = e.boolean;
		local_EnemySpotted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_20;
		Relay_Restart_Out_20();
	}

	private void SubGraph_SaveLoadBool_Save_Out_27(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_27;
		Relay_Save_Out_27();
	}

	private void SubGraph_SaveLoadBool_Load_Out_27(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_27;
		Relay_Load_Out_27();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_27(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_27;
		Relay_Restart_Out_27();
	}

	private void SubGraph_SaveLoadBool_Save_Out_33(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_33;
		Relay_Save_Out_33();
	}

	private void SubGraph_SaveLoadBool_Load_Out_33(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_33;
		Relay_Load_Out_33();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_33(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_33;
		Relay_Restart_Out_33();
	}

	private void SubGraph_SaveLoadBool_Save_Out_52(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_52;
		Relay_Save_Out_52();
	}

	private void SubGraph_SaveLoadBool_Load_Out_52(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_52;
		Relay_Load_Out_52();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_52(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_52;
		Relay_Restart_Out_52();
	}

	private void SubGraph_SaveLoadBool_Save_Out_88(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = e.boolean;
		local_EnemyDeadEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_88;
		Relay_Save_Out_88();
	}

	private void SubGraph_SaveLoadBool_Load_Out_88(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = e.boolean;
		local_EnemyDeadEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_88;
		Relay_Load_Out_88();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_88(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = e.boolean;
		local_EnemyDeadEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_88;
		Relay_Restart_Out_88();
	}

	private void uScriptCon_ManualSwitch_Output1_89(object o, EventArgs e)
	{
		Relay_Output1_89();
	}

	private void uScriptCon_ManualSwitch_Output2_89(object o, EventArgs e)
	{
		Relay_Output2_89();
	}

	private void uScriptCon_ManualSwitch_Output3_89(object o, EventArgs e)
	{
		Relay_Output3_89();
	}

	private void uScriptCon_ManualSwitch_Output4_89(object o, EventArgs e)
	{
		Relay_Output4_89();
	}

	private void uScriptCon_ManualSwitch_Output5_89(object o, EventArgs e)
	{
		Relay_Output5_89();
	}

	private void uScriptCon_ManualSwitch_Output6_89(object o, EventArgs e)
	{
		Relay_Output6_89();
	}

	private void uScriptCon_ManualSwitch_Output7_89(object o, EventArgs e)
	{
		Relay_Output7_89();
	}

	private void uScriptCon_ManualSwitch_Output8_89(object o, EventArgs e)
	{
		Relay_Output8_89();
	}

	private void SubGraph_CompleteObjectiveStage_Out_95(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_95 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_95;
		Relay_Out_95();
	}

	private void SubGraph_SaveLoadBool_Save_Out_105(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_105;
		Relay_Save_Out_105();
	}

	private void SubGraph_SaveLoadBool_Load_Out_105(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_105;
		Relay_Load_Out_105();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_105(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_105;
		Relay_Restart_Out_105();
	}

	private void SubGraph_LoadObjectiveStates_Out_108(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_108();
	}

	private void SubGraph_SaveLoadInt_Save_Out_109(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_109 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_109;
		Relay_Save_Out_109();
	}

	private void SubGraph_SaveLoadInt_Load_Out_109(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_109 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_109;
		Relay_Load_Out_109();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_109(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_109 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_109;
		Relay_Restart_Out_109();
	}

	private void SubGraph_SaveLoadBool_Save_Out_117(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_117;
		Relay_Save_Out_117();
	}

	private void SubGraph_SaveLoadBool_Load_Out_117(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_117;
		Relay_Load_Out_117();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_117(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_117;
		Relay_Restart_Out_117();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_89();
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
			Relay_Succeed_2();
		}
	}

	private void Relay_True_7()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.True(out logic_uScriptAct_SetBool_Target_7);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_7;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_7.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_False_7()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.False(out logic_uScriptAct_SetBool_Target_7);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_7;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_7.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_11()
	{
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_11.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_11, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_11, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_range_11 = distEnemiesSpotted;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_11.In(logic_uScript_InRangeOfAtLeastOneTech_techs_11, logic_uScript_InRangeOfAtLeastOneTech_range_11);
		if (logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_11.InRange)
		{
			Relay_In_28();
		}
	}

	private void Relay_SaveEvent_17()
	{
		Relay_Save_109();
	}

	private void Relay_LoadEvent_17()
	{
		Relay_Load_109();
	}

	private void Relay_RestartEvent_17()
	{
		Relay_Restart_109();
	}

	private void Relay_Save_Out_20()
	{
		Relay_Save_33();
	}

	private void Relay_Load_Out_20()
	{
		Relay_Load_33();
	}

	private void Relay_Restart_Out_20()
	{
		Relay_Set_False_33();
	}

	private void Relay_Save_20()
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Save(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
	}

	private void Relay_Load_20()
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Load(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
	}

	private void Relay_Set_True_20()
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
	}

	private void Relay_Set_False_20()
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
	}

	private void Relay_In_21()
	{
		int num = 0;
		Array array = msgEnemiesSpotted;
		if (logic_uScript_AddOnScreenMessage_locString_21.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_21, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_21, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_21 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_21 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_21.In(logic_uScript_AddOnScreenMessage_locString_21, logic_uScript_AddOnScreenMessage_msgPriority_21, logic_uScript_AddOnScreenMessage_holdMsg_21, logic_uScript_AddOnScreenMessage_tag_21, logic_uScript_AddOnScreenMessage_speaker_21, logic_uScript_AddOnScreenMessage_side_21);
	}

	private void Relay_In_22()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_22 = owner_Connection_25;
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_22.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_22, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_22, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_22 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_22.In(logic_uScript_SetOneTechAsEncounterTarget_owner_22, logic_uScript_SetOneTechAsEncounterTarget_techs_22);
		local_EnemyTanks_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_22;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_22.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_Save_Out_27()
	{
		Relay_Save_52();
	}

	private void Relay_Load_Out_27()
	{
		Relay_Load_52();
	}

	private void Relay_Restart_Out_27()
	{
		Relay_Set_False_52();
	}

	private void Relay_Save_27()
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_27 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Save(ref logic_SubGraph_SaveLoadBool_boolean_27, logic_SubGraph_SaveLoadBool_boolAsVariable_27, logic_SubGraph_SaveLoadBool_uniqueID_27);
	}

	private void Relay_Load_27()
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_27 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Load(ref logic_SubGraph_SaveLoadBool_boolean_27, logic_SubGraph_SaveLoadBool_boolAsVariable_27, logic_SubGraph_SaveLoadBool_uniqueID_27);
	}

	private void Relay_Set_True_27()
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_27 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_27, logic_SubGraph_SaveLoadBool_boolAsVariable_27, logic_SubGraph_SaveLoadBool_uniqueID_27);
	}

	private void Relay_Set_False_27()
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_27 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_27, logic_SubGraph_SaveLoadBool_boolAsVariable_27, logic_SubGraph_SaveLoadBool_uniqueID_27);
	}

	private void Relay_In_28()
	{
		logic_uScriptCon_CompareBool_Bool_28 = local_EnemySpotted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.In(logic_uScriptCon_CompareBool_Bool_28);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.False)
		{
			Relay_True_7();
		}
	}

	private void Relay_In_29()
	{
		logic_uScriptCon_CompareBool_Bool_29 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.In(logic_uScriptCon_CompareBool_Bool_29);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.False;
		if (num)
		{
			Relay_In_50();
		}
		if (flag)
		{
			Relay_InitialSpawn_30();
		}
	}

	private void Relay_InitialSpawn_30()
	{
		int num = 0;
		Array array = enemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_30.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_30, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_30, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_30 = owner_Connection_10;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_30.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_30, logic_uScript_SpawnTechsFromData_ownerNode_30, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_30);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_30.Out)
		{
			Relay_InitialSpawn_46();
		}
	}

	private void Relay_True_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.True(out logic_uScriptAct_SetBool_Target_31);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_31;
	}

	private void Relay_False_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.False(out logic_uScriptAct_SetBool_Target_31);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_31;
	}

	private void Relay_In_32()
	{
		int num = 0;
		Array array = enemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_32.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_32, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_32, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_32 = owner_Connection_13;
		int num2 = 0;
		Array array2 = local_EnemyTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_32.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_32, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_32, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_32 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_32.In(logic_uScript_GetAndCheckTechs_techData_32, logic_uScript_GetAndCheckTechs_ownerNode_32, ref logic_uScript_GetAndCheckTechs_techs_32);
		local_EnemyTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_32;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_32.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_32.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_32.AllDead;
		if (allAlive)
		{
			Relay_In_22();
		}
		if (someAlive)
		{
			Relay_In_22();
		}
		if (allDead)
		{
			Relay_True_31();
		}
	}

	private void Relay_Save_Out_33()
	{
		Relay_Save_27();
	}

	private void Relay_Load_Out_33()
	{
		Relay_Load_27();
	}

	private void Relay_Restart_Out_33()
	{
		Relay_Set_False_27();
	}

	private void Relay_Save_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Save(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Load_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Load(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Set_True_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Set_False_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_In_34()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_34 = owner_Connection_12;
		logic_uScript_MoveEncounterWithVisible_visibleObject_34 = local_EnemyTanks_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_34.In(logic_uScript_MoveEncounterWithVisible_ownerNode_34, logic_uScript_MoveEncounterWithVisible_visibleObject_34);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_34.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_38()
	{
		logic_uScriptCon_CompareBool_Bool_38 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_38.In(logic_uScriptCon_CompareBool_Bool_38);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_38.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_38.False;
		if (num)
		{
			Relay_In_86();
		}
		if (flag)
		{
			Relay_In_29();
		}
	}

	private void Relay_True_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.True(out logic_uScriptAct_SetBool_Target_40);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_40;
	}

	private void Relay_False_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.False(out logic_uScriptAct_SetBool_Target_40);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_40;
	}

	private void Relay_InitialSpawn_46()
	{
		int num = 0;
		Array nPCTech = NPCTech;
		if (logic_uScript_SpawnTechsFromData_spawnData_46.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_46, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_uScript_SpawnTechsFromData_spawnData_46, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_46 = owner_Connection_47;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_46.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_46, logic_uScript_SpawnTechsFromData_ownerNode_46, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_46);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_46.Out)
		{
			Relay_In_118();
		}
	}

	private void Relay_In_50()
	{
		logic_uScriptCon_CompareBool_Bool_50 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.In(logic_uScriptCon_CompareBool_Bool_50);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.False;
		if (num)
		{
			Relay_In_32();
		}
		if (flag)
		{
			Relay_In_66();
		}
	}

	private void Relay_Save_Out_52()
	{
		Relay_Save_88();
	}

	private void Relay_Load_Out_52()
	{
		Relay_Load_88();
	}

	private void Relay_Restart_Out_52()
	{
		Relay_Set_False_88();
	}

	private void Relay_Save_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Save(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_Load_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Load(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_Set_True_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_Set_False_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_In_53()
	{
		logic_uScript_InRangeOfTech_tank_53 = local_NPCTank_Tank;
		logic_uScript_InRangeOfTech_range_53 = DistInRangeOfNPC;
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_53.In(logic_uScript_InRangeOfTech_tank_53, logic_uScript_InRangeOfTech_range_53);
		bool inRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_53.InRange;
		bool outOfRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_53.OutOfRange;
		if (inRange)
		{
			Relay_True_97();
		}
		if (outOfRange)
		{
			Relay_In_102();
		}
	}

	private void Relay_In_56()
	{
		int num = 0;
		Array array = msgNPCGreeting;
		if (logic_uScript_AddOnScreenMessage_locString_56.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_56, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_56, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_56 = local_99_System_String;
		logic_uScript_AddOnScreenMessage_speaker_56 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_56 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_56.In(logic_uScript_AddOnScreenMessage_locString_56, logic_uScript_AddOnScreenMessage_msgPriority_56, logic_uScript_AddOnScreenMessage_holdMsg_56, logic_uScript_AddOnScreenMessage_tag_56, logic_uScript_AddOnScreenMessage_speaker_56, logic_uScript_AddOnScreenMessage_side_56);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_56.Shown)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_60()
	{
		logic_uScript_SetTankInvulnerable_tank_60 = local_NPCTank_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_60.In(logic_uScript_SetTankInvulnerable_invulnerable_60, logic_uScript_SetTankInvulnerable_tank_60);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_60.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_AtIndex_63()
	{
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_AccessListTech_techList_63.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_63, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_63, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_63.AtIndex(ref logic_uScript_AccessListTech_techList_63, logic_uScript_AccessListTech_index_63, out logic_uScript_AccessListTech_value_63);
		local_NPCTanks_TankArray = logic_uScript_AccessListTech_techList_63;
		local_NPCTank_Tank = logic_uScript_AccessListTech_value_63;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_63.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_66()
	{
		int num = 0;
		Array nPCTech = NPCTech;
		if (logic_uScript_GetAndCheckTechs_techData_66.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_66, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_uScript_GetAndCheckTechs_techData_66, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_66 = owner_Connection_65;
		int num2 = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_66.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_66, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_66, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_66 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.In(logic_uScript_GetAndCheckTechs_techData_66, logic_uScript_GetAndCheckTechs_ownerNode_66, ref logic_uScript_GetAndCheckTechs_techs_66);
		local_NPCTanks_TankArray = logic_uScript_GetAndCheckTechs_techs_66;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_63();
		}
		if (someAlive)
		{
			Relay_AtIndex_63();
		}
	}

	private void Relay_In_67()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_67 = owner_Connection_68;
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_67.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_67, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_67, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_67 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_67.In(logic_uScript_SetOneTechAsEncounterTarget_owner_67, logic_uScript_SetOneTechAsEncounterTarget_techs_67);
		local_NPCTank_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_67;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_67.Out)
		{
			Relay_In_112();
		}
	}

	private void Relay_True_71()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.True(out logic_uScriptAct_SetBool_Target_71);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_71;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_71.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_False_71()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.False(out logic_uScriptAct_SetBool_Target_71);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_71;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_71.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_75()
	{
		int num = 0;
		Array array = enemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_75.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_75, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_75, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_75 = owner_Connection_73;
		int num2 = 0;
		Array array2 = local_EnemyTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_75.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_75, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_75, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_75 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75.In(logic_uScript_GetAndCheckTechs_techData_75, logic_uScript_GetAndCheckTechs_ownerNode_75, ref logic_uScript_GetAndCheckTechs_techs_75);
		local_EnemyTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_75;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_75.AllDead;
		if (allAlive)
		{
			Relay_In_56();
		}
		if (someAlive)
		{
			Relay_In_56();
		}
		if (allDead)
		{
			Relay_In_77();
		}
	}

	private void Relay_In_77()
	{
		int num = 0;
		Array array = msgNPCGreetingEnemyDead;
		if (logic_uScript_AddOnScreenMessage_locString_77.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_77, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_77, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_77 = local_98_System_String;
		logic_uScript_AddOnScreenMessage_speaker_77 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_77 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_77.In(logic_uScript_AddOnScreenMessage_locString_77, logic_uScript_AddOnScreenMessage_msgPriority_77, logic_uScript_AddOnScreenMessage_holdMsg_77, logic_uScript_AddOnScreenMessage_tag_77, logic_uScript_AddOnScreenMessage_speaker_77, logic_uScript_AddOnScreenMessage_side_77);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_77.Shown)
		{
			Relay_True_79();
		}
	}

	private void Relay_True_79()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_79.True(out logic_uScriptAct_SetBool_Target_79);
		local_EnemyDeadEarly_System_Boolean = logic_uScriptAct_SetBool_Target_79;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_79.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_False_79()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_79.False(out logic_uScriptAct_SetBool_Target_79);
		local_EnemyDeadEarly_System_Boolean = logic_uScriptAct_SetBool_Target_79;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_79.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_80()
	{
		logic_uScript_FlyTechUpAndAway_tech_80 = local_NPCTank_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_80 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_80 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_80.In(logic_uScript_FlyTechUpAndAway_tech_80, logic_uScript_FlyTechUpAndAway_maxLifetime_80, logic_uScript_FlyTechUpAndAway_targetHeight_80, logic_uScript_FlyTechUpAndAway_aiTree_80, logic_uScript_FlyTechUpAndAway_removalParticles_80);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_80.Out)
		{
			Relay_True_71();
		}
	}

	private void Relay_In_86()
	{
		logic_uScriptCon_CompareBool_Bool_86 = local_EnemyDeadEarly_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.In(logic_uScriptCon_CompareBool_Bool_86);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.False;
		if (num)
		{
			Relay_Succeed_2();
		}
		if (flag)
		{
			Relay_In_4();
		}
	}

	private void Relay_Save_Out_88()
	{
		Relay_Save_105();
	}

	private void Relay_Load_Out_88()
	{
		Relay_Load_105();
	}

	private void Relay_Restart_Out_88()
	{
		Relay_Set_False_105();
	}

	private void Relay_Save_88()
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_88 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Save(ref logic_SubGraph_SaveLoadBool_boolean_88, logic_SubGraph_SaveLoadBool_boolAsVariable_88, logic_SubGraph_SaveLoadBool_uniqueID_88);
	}

	private void Relay_Load_88()
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_88 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Load(ref logic_SubGraph_SaveLoadBool_boolean_88, logic_SubGraph_SaveLoadBool_boolAsVariable_88, logic_SubGraph_SaveLoadBool_uniqueID_88);
	}

	private void Relay_Set_True_88()
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_88 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_88, logic_SubGraph_SaveLoadBool_boolAsVariable_88, logic_SubGraph_SaveLoadBool_uniqueID_88);
	}

	private void Relay_Set_False_88()
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_88 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_88, logic_SubGraph_SaveLoadBool_boolAsVariable_88, logic_SubGraph_SaveLoadBool_uniqueID_88);
	}

	private void Relay_Output1_89()
	{
		Relay_In_38();
	}

	private void Relay_Output2_89()
	{
		Relay_In_38();
	}

	private void Relay_Output3_89()
	{
	}

	private void Relay_Output4_89()
	{
	}

	private void Relay_Output5_89()
	{
	}

	private void Relay_Output6_89()
	{
	}

	private void Relay_Output7_89()
	{
	}

	private void Relay_Output8_89()
	{
	}

	private void Relay_In_89()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_89 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.In(logic_uScriptCon_ManualSwitch_CurrentOutput_89);
	}

	private void Relay_In_92()
	{
		int num = 0;
		Array array = msgNPCGreetingInturrupt;
		if (logic_uScript_AddOnScreenMessage_locString_92.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_92, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_92, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_92 = local_285_System_String;
		logic_uScript_AddOnScreenMessage_speaker_92 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_92 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_92.In(logic_uScript_AddOnScreenMessage_locString_92, logic_uScript_AddOnScreenMessage_msgPriority_92, logic_uScript_AddOnScreenMessage_holdMsg_92, logic_uScript_AddOnScreenMessage_tag_92, logic_uScript_AddOnScreenMessage_speaker_92, logic_uScript_AddOnScreenMessage_side_92);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_92.Shown)
		{
			Relay_True_110();
		}
	}

	private void Relay_Out_95()
	{
	}

	private void Relay_In_95()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_95 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_95, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_95);
	}

	private void Relay_True_97()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.True(out logic_uScriptAct_SetBool_Target_97);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_97;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_97.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_False_97()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.False(out logic_uScriptAct_SetBool_Target_97);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_97;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_97.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_In_100()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_100 = local_101_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_100.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_100, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_100);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_100.Out)
		{
			Relay_In_276();
		}
	}

	private void Relay_In_102()
	{
		logic_uScriptCon_CompareBool_Bool_102 = local_NPCSeen_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102.In(logic_uScriptCon_CompareBool_Bool_102);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102.True)
		{
			Relay_True_114();
		}
	}

	private void Relay_Save_Out_105()
	{
		Relay_Save_117();
	}

	private void Relay_Load_Out_105()
	{
		Relay_Load_117();
	}

	private void Relay_Restart_Out_105()
	{
		Relay_Set_False_117();
	}

	private void Relay_Save_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Save(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Load_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Load(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Set_True_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Set_False_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Out_108()
	{
	}

	private void Relay_In_108()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_108 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_108.In(logic_SubGraph_LoadObjectiveStates_currentObjective_108);
	}

	private void Relay_Save_Out_109()
	{
		Relay_Save_20();
	}

	private void Relay_Load_Out_109()
	{
		Relay_Load_20();
	}

	private void Relay_Restart_Out_109()
	{
		Relay_Set_False_20();
	}

	private void Relay_Save_109()
	{
		logic_SubGraph_SaveLoadInt_integer_109 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_109 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Save(logic_SubGraph_SaveLoadInt_restartValue_109, ref logic_SubGraph_SaveLoadInt_integer_109, logic_SubGraph_SaveLoadInt_intAsVariable_109, logic_SubGraph_SaveLoadInt_uniqueID_109);
	}

	private void Relay_Load_109()
	{
		logic_SubGraph_SaveLoadInt_integer_109 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_109 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Load(logic_SubGraph_SaveLoadInt_restartValue_109, ref logic_SubGraph_SaveLoadInt_integer_109, logic_SubGraph_SaveLoadInt_intAsVariable_109, logic_SubGraph_SaveLoadInt_uniqueID_109);
	}

	private void Relay_Restart_109()
	{
		logic_SubGraph_SaveLoadInt_integer_109 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_109 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Restart(logic_SubGraph_SaveLoadInt_restartValue_109, ref logic_SubGraph_SaveLoadInt_integer_109, logic_SubGraph_SaveLoadInt_intAsVariable_109, logic_SubGraph_SaveLoadInt_uniqueID_109);
	}

	private void Relay_True_110()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_110.True(out logic_uScriptAct_SetBool_Target_110);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_110;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_110.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_False_110()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_110.False(out logic_uScriptAct_SetBool_Target_110);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_110;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_110.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_112()
	{
		logic_uScriptCon_CompareBool_Bool_112 = local_NPCIgnored_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.In(logic_uScriptCon_CompareBool_Bool_112);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.False;
		if (num)
		{
			Relay_In_100();
		}
		if (flag)
		{
			Relay_In_53();
		}
	}

	private void Relay_True_114()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_114.True(out logic_uScriptAct_SetBool_Target_114);
		local_NPCIgnored_System_Boolean = logic_uScriptAct_SetBool_Target_114;
	}

	private void Relay_False_114()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_114.False(out logic_uScriptAct_SetBool_Target_114);
		local_NPCIgnored_System_Boolean = logic_uScriptAct_SetBool_Target_114;
	}

	private void Relay_Save_Out_117()
	{
	}

	private void Relay_Load_Out_117()
	{
		Relay_In_108();
	}

	private void Relay_Restart_Out_117()
	{
	}

	private void Relay_Save_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Save(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Load_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Load(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Set_True_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Set_False_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_In_118()
	{
		logic_uScript_RemoveScenery_ownerNode_118 = owner_Connection_119;
		logic_uScript_RemoveScenery_positionName_118 = clearSceneryPos;
		logic_uScript_RemoveScenery_radius_118 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_118.In(logic_uScript_RemoveScenery_ownerNode_118, logic_uScript_RemoveScenery_positionName_118, logic_uScript_RemoveScenery_radius_118, logic_uScript_RemoveScenery_preventChunksSpawning_118);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_118.Out)
		{
			Relay_True_40();
		}
	}

	private void Relay_In_276()
	{
		logic_uScript_InRangeOfTech_tank_276 = local_NPCTank_Tank;
		logic_uScript_InRangeOfTech_range_276 = DistInRangeOfNPC;
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_276.In(logic_uScript_InRangeOfTech_tank_276, logic_uScript_InRangeOfTech_range_276);
		bool inRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_276.InRange;
		bool outOfRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_276.OutOfRange;
		if (inRange)
		{
			Relay_In_92();
		}
		if (outOfRange)
		{
			Relay_In_283();
		}
	}

	private void Relay_In_283()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_283 = local_282_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_283.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_283, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_283);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_283.Out)
		{
			Relay_True_110();
		}
	}
}
