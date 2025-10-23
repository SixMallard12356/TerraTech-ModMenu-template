using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_SetPiece_MountainAmbush : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public SpawnTechData[] ambushEnemyTechData = new SpawnTechData[0];

	public SpawnTechData[] ChaseEnemiesSpawnData = new SpawnTechData[0];

	[Multiline(3)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius = 50f;

	[Multiline(3)]
	public string crateSpawnPos = "";

	public SpawnTechData[] firstEnemiesSpawnData = new SpawnTechData[0];

	private string local_124_System_String = "ApproachCrateMsg";

	private string local_126_System_String = "ApproachCrateMsg";

	private string local_127_System_String = "ApproachCrateMsg";

	private string local_153_System_String = "msg";

	private string local_167_System_String = "msg";

	private string local_176_System_String = "";

	private string local_383_System_String = "msg";

	private float local_57_System_Single;

	private Tank[] local_ChaseTechs_TankArray = new Tank[0];

	private bool local_ChaseTechsSpawned_System_Boolean;

	private Crate local_Crate_Crate;

	private bool local_CrateOpened_System_Boolean;

	private bool local_CrateSpawned_System_Boolean;

	private Tank[] local_firstEnemies_TankArray = new Tank[0];

	private bool local_FirstEnemySpawned_System_Boolean;

	private bool local_MetNPC_System_Boolean;

	private bool local_MsgApproachCrateShown_System_Boolean;

	private bool local_NPCExit_System_Boolean;

	private bool local_NPCMet_System_Boolean;

	private bool local_NPCSpawned_System_Boolean;

	private Tank local_NPCTech_Tank;

	private Tank[] local_NPCTechs_TankArray = new Tank[0];

	private bool local_ObjectiveMessagePlayed_System_Boolean;

	private bool local_ShownMsgTrapSprung_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private bool local_WelcomeMessagePlayed_System_Boolean;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgApproachCrate = new LocalisedString[0];

	public LocalisedString[] msgComplete = new LocalisedString[0];

	public LocalisedString[] msgEnemySpotted = new LocalisedString[0];

	public LocalisedString[] msgMissionInRange = new LocalisedString[0];

	public LocalisedString[] msgNPCGreeting01 = new LocalisedString[0];

	public LocalisedString[] msgObjective = new LocalisedString[0];

	public LocalisedString[] msgRunAway = new LocalisedString[0];

	public LocalisedString[] msgTrapSprung = new LocalisedString[0];

	public LocalisedString[] msgTurretWarning = new LocalisedString[0];

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCTechSpawn = new SpawnTechData[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_30;

	private GameObject owner_Connection_63;

	private GameObject owner_Connection_65;

	private GameObject owner_Connection_71;

	private GameObject owner_Connection_77;

	private GameObject owner_Connection_83;

	private GameObject owner_Connection_84;

	private GameObject owner_Connection_89;

	private GameObject owner_Connection_94;

	private GameObject owner_Connection_106;

	private GameObject owner_Connection_111;

	private GameObject owner_Connection_142;

	private GameObject owner_Connection_147;

	private GameObject owner_Connection_160;

	private SubGraph_DefeatEnemyTechs logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5 = new SubGraph_DefeatEnemyTechs();

	private SpawnTechData[] logic_SubGraph_DefeatEnemyTechs_enemyTechData_5 = new SpawnTechData[0];

	private float logic_SubGraph_DefeatEnemyTechs_distEnemiesSpotted_5 = 999999f;

	private string logic_SubGraph_DefeatEnemyTechs_clearSceneryPos_5 = "";

	private float logic_SubGraph_DefeatEnemyTechs_clearSceneryRadius_5;

	private LocalisedString[] logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_5 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_DefeatEnemyTechs_msgComplete_5 = new LocalisedString[0];

	private ManOnScreenMessages.Speaker logic_SubGraph_DefeatEnemyTechs_messageSpeaker_5;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_7;

	private bool logic_uScriptCon_CompareBool_True_7 = true;

	private bool logic_uScriptCon_CompareBool_False_7 = true;

	private uScript_SpawnDeliveryCrate logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_9 = new uScript_SpawnDeliveryCrate();

	private string logic_uScript_SpawnDeliveryCrate_positionName_9 = "";

	private GameObject logic_uScript_SpawnDeliveryCrate_ownerNode_9;

	private bool logic_uScript_SpawnDeliveryCrate_visibleOnRadar_9 = true;

	private bool logic_uScript_SpawnDeliveryCrate_Out_9 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_12;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_12 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_12 = "CrateSpawned";

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_16 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_16 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_16;

	private string logic_uScript_AddOnScreenMessage_tag_16 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_16;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_16;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_16;

	private bool logic_uScript_AddOnScreenMessage_Out_16 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_16 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_18;

	private bool logic_uScriptCon_CompareBool_True_18 = true;

	private bool logic_uScriptCon_CompareBool_False_18 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_19 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_19;

	private bool logic_uScriptAct_SetBool_Out_19 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_19 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_19 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_22;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_22 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_22 = "ShownMsgTrapSprung";

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_23;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_25 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_25;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_25;

	private uScript_UnlockDeliveryCrate logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_27 = new uScript_UnlockDeliveryCrate();

	private GameObject logic_uScript_UnlockDeliveryCrate_ownerNode_27;

	private bool logic_uScript_UnlockDeliveryCrate_Out_27 = true;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_29 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_29 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_29;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_29 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_29 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_29 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_31;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_31;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_33 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_33;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_33 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_33 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_35 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_35;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_45 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_45 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_45 = 150f;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_45 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_45 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_45 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_47;

	private bool logic_uScriptCon_CompareBool_True_47 = true;

	private bool logic_uScriptCon_CompareBool_False_47 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_48 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_48 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_48 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_48;

	private string logic_uScript_AddOnScreenMessage_tag_48 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_48;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_48;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_48;

	private bool logic_uScript_AddOnScreenMessage_Out_48 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_48 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_50 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_50;

	private bool logic_uScriptAct_SetBool_Out_50 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_50 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_50 = true;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_54 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_54 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_54 = 25f;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_54 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_54 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_54 = true;

	private uScript_GetCrateOpenTriggerRange logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_55 = new uScript_GetCrateOpenTriggerRange();

	private Crate logic_uScript_GetCrateOpenTriggerRange_crate_55;

	private float logic_uScript_GetCrateOpenTriggerRange_Return_55;

	private bool logic_uScript_GetCrateOpenTriggerRange_Out_55 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_58;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_58 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_58 = "MsgApproachCrateShown";

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_64 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_64;

	private bool logic_uScript_FinishEncounter_Out_64 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_66 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_66 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_66;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_66 = 3f;

	private bool logic_uScript_SpawnTechsFromData_Out_66 = true;

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

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_70 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_70 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_70;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_70 = 10f;

	private bool logic_uScript_SpawnTechsFromData_Out_70 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_72;

	private bool logic_uScriptCon_CompareBool_True_72 = true;

	private bool logic_uScriptCon_CompareBool_False_72 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_74;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_74 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_74 = "NPCSpawned";

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_76 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_76 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_76 = 150f;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_76 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_76 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_76 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_78 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_78 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_78;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_78 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_78;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_78 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_78 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_78 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_78 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_80 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_80;

	private bool logic_uScriptAct_SetBool_Out_80 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_80 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_80 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_82 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_82;

	private object logic_uScript_SetEncounterTarget_visibleObject_82 = "";

	private bool logic_uScript_SetEncounterTarget_Out_82 = true;

	private uScript_CheckDeliveryCrateSpawned logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_85 = new uScript_CheckDeliveryCrateSpawned();

	private GameObject logic_uScript_CheckDeliveryCrateSpawned_ownerNode_85;

	private bool logic_uScript_CheckDeliveryCrateSpawned_Out_85 = true;

	private bool logic_uScript_CheckDeliveryCrateSpawned_Yes_85 = true;

	private bool logic_uScript_CheckDeliveryCrateSpawned_No_85 = true;

	private uScript_GetDeliveryCrate logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_86 = new uScript_GetDeliveryCrate();

	private GameObject logic_uScript_GetDeliveryCrate_ownerNode_86;

	private Crate logic_uScript_GetDeliveryCrate_Return_86;

	private bool logic_uScript_GetDeliveryCrate_Out_86 = true;

	private bool logic_uScript_GetDeliveryCrate_Success_86 = true;

	private bool logic_uScript_GetDeliveryCrate_Failure_86 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_91 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_91;

	private bool logic_uScriptCon_CompareBool_True_91 = true;

	private bool logic_uScriptCon_CompareBool_False_91 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_96 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_96 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_96;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_96 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_96;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_96 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_96 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_96 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_96 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_98 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_98;

	private bool logic_uScriptAct_SetBool_Out_98 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_98 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_98 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_100 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_100;

	private bool logic_uScriptAct_SetBool_Out_100 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_100 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_100 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_101;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_101 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_101 = "WelcomeMessagePlayed";

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_104 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_104 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_104;

	private bool logic_uScript_SetTankInvulnerable_Out_104 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_105 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_105;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_105 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_105;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_105 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_105 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_105 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_105 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_107 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_107 = new Tank[0];

	private int logic_uScript_AccessListTech_index_107;

	private Tank logic_uScript_AccessListTech_value_107;

	private bool logic_uScript_AccessListTech_Out_107 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_115 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_115 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_115;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_115 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_115 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_116 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_116;

	private bool logic_uScriptCon_CompareBool_True_116 = true;

	private bool logic_uScriptCon_CompareBool_False_116 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_117 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_117;

	private bool logic_uScriptAct_SetBool_Out_117 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_117 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_117 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_120 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_120 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_120 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_120;

	private string logic_uScript_AddOnScreenMessage_tag_120 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_120;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_120;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_120;

	private bool logic_uScript_AddOnScreenMessage_Out_120 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_120 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_121 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_121;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_121 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_121 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_121;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_121;

	private bool logic_uScript_FlyTechUpAndAway_Out_121 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_125 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_125 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_125;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_125 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_128 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_128 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_128;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_128 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_129;

	private bool logic_uScriptCon_CompareBool_True_129 = true;

	private bool logic_uScriptCon_CompareBool_False_129 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_131;

	private bool logic_uScriptCon_CompareBool_True_131 = true;

	private bool logic_uScriptCon_CompareBool_False_131 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_133 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_133;

	private bool logic_uScriptAct_SetBool_Out_133 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_133 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_133 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_135;

	private bool logic_uScriptCon_CompareBool_True_135 = true;

	private bool logic_uScriptCon_CompareBool_False_135 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_137 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_137;

	private bool logic_uScriptAct_SetBool_Out_137 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_137 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_137 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_141 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_141;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_141 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_141;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_141 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_145 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_145 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_145;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_145 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_145;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_145 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_145 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_145 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_145 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_148 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_148;

	private bool logic_uScriptCon_CompareBool_True_148 = true;

	private bool logic_uScriptCon_CompareBool_False_148 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_150 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_150;

	private bool logic_uScriptAct_SetBool_Out_150 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_150 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_150 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_152 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_152 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_152 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_152;

	private string logic_uScript_AddOnScreenMessage_tag_152 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_152;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_152;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_152;

	private bool logic_uScript_AddOnScreenMessage_Out_152 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_152 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_156 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_156;

	private bool logic_uScriptAct_SetBool_Out_156 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_156 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_156 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_158 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_158 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_158;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_158 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_158;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_158 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_158 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_158 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_158 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_162;

	private bool logic_uScriptCon_CompareBool_True_162 = true;

	private bool logic_uScriptCon_CompareBool_False_162 = true;

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

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_169 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_169;

	private bool logic_uScriptAct_SetBool_Out_169 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_169 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_169 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_171 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_171;

	private float logic_uScript_IsPlayerInRangeOfTech_range_171 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_171 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_171 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_171 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_171 = true;

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_173 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_173 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_173 = 50f;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_173 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_173 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_173 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_175 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_175 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_175 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_175;

	private string logic_uScript_AddOnScreenMessage_tag_175 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_175;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_175;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_175;

	private bool logic_uScript_AddOnScreenMessage_Out_175 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_175 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_384 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_384 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_384;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_384 = true;

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
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
			if (null != owner_Connection_14)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_14.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_14.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_13;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_13;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_13;
				}
			}
		}
		if (null == owner_Connection_30 || !m_RegisteredForEvents)
		{
			owner_Connection_30 = parentGameObject;
		}
		if (null == owner_Connection_63 || !m_RegisteredForEvents)
		{
			owner_Connection_63 = parentGameObject;
		}
		if (null == owner_Connection_65 || !m_RegisteredForEvents)
		{
			owner_Connection_65 = parentGameObject;
		}
		if (null == owner_Connection_71 || !m_RegisteredForEvents)
		{
			owner_Connection_71 = parentGameObject;
		}
		if (null == owner_Connection_77 || !m_RegisteredForEvents)
		{
			owner_Connection_77 = parentGameObject;
		}
		if (null == owner_Connection_83 || !m_RegisteredForEvents)
		{
			owner_Connection_83 = parentGameObject;
		}
		if (null == owner_Connection_84 || !m_RegisteredForEvents)
		{
			owner_Connection_84 = parentGameObject;
		}
		if (null == owner_Connection_89 || !m_RegisteredForEvents)
		{
			owner_Connection_89 = parentGameObject;
		}
		if (null == owner_Connection_94 || !m_RegisteredForEvents)
		{
			owner_Connection_94 = parentGameObject;
		}
		if (null == owner_Connection_106 || !m_RegisteredForEvents)
		{
			owner_Connection_106 = parentGameObject;
		}
		if (null == owner_Connection_111 || !m_RegisteredForEvents)
		{
			owner_Connection_111 = parentGameObject;
		}
		if (null == owner_Connection_142 || !m_RegisteredForEvents)
		{
			owner_Connection_142 = parentGameObject;
		}
		if (null == owner_Connection_147 || !m_RegisteredForEvents)
		{
			owner_Connection_147 = parentGameObject;
		}
		if (null == owner_Connection_160 || !m_RegisteredForEvents)
		{
			owner_Connection_160 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_14)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_14.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_14.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_13;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_13;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_13;
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
		if (null != owner_Connection_14)
		{
			uScript_SaveLoad component2 = owner_Connection_14.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_13;
				component2.LoadEvent -= Instance_LoadEvent_13;
				component2.RestartEvent -= Instance_RestartEvent_13;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.SetParent(g);
		logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_9.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_19.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_25.SetParent(g);
		logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_27.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_29.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_35.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_45.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_48.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_54.SetParent(g);
		logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_55.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_64.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_66.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_69.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_70.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_76.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_78.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_82.SetParent(g);
		logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_85.SetParent(g);
		logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_86.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_91.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_96.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_100.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_104.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_107.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_115.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_116.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_120.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_121.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_125.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_128.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_133.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_137.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_141.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_145.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_148.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_150.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_152.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_156.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_158.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_169.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_171.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_173.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_175.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_384.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_30 = parentGameObject;
		owner_Connection_63 = parentGameObject;
		owner_Connection_65 = parentGameObject;
		owner_Connection_71 = parentGameObject;
		owner_Connection_77 = parentGameObject;
		owner_Connection_83 = parentGameObject;
		owner_Connection_84 = parentGameObject;
		owner_Connection_89 = parentGameObject;
		owner_Connection_94 = parentGameObject;
		owner_Connection_106 = parentGameObject;
		owner_Connection_111 = parentGameObject;
		owner_Connection_142 = parentGameObject;
		owner_Connection_147 = parentGameObject;
		owner_Connection_160 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_25.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_35.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Awake();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.Complete += SubGraph_DefeatEnemyTechs_Complete_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save_Out += SubGraph_SaveLoadBool_Save_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load_Out += SubGraph_SaveLoadBool_Load_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Save_Out += SubGraph_SaveLoadBool_Save_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Load_Out += SubGraph_SaveLoadBool_Load_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output1 += uScriptCon_ManualSwitch_Output1_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output2 += uScriptCon_ManualSwitch_Output2_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output3 += uScriptCon_ManualSwitch_Output3_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output4 += uScriptCon_ManualSwitch_Output4_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output5 += uScriptCon_ManualSwitch_Output5_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output6 += uScriptCon_ManualSwitch_Output6_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output7 += uScriptCon_ManualSwitch_Output7_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output8 += uScriptCon_ManualSwitch_Output8_23;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_25.Out += SubGraph_CompleteObjectiveStage_Out_25;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.Out += SubGraph_CompleteObjectiveStage_Out_31;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.Save_Out += SubGraph_SaveLoadInt_Save_Out_33;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.Load_Out += SubGraph_SaveLoadInt_Load_Out_33;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_33;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_35.Out += SubGraph_LoadObjectiveStates_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save_Out += SubGraph_SaveLoadBool_Save_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load_Out += SubGraph_SaveLoadBool_Load_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Save_Out += SubGraph_SaveLoadBool_Save_Out_74;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Load_Out += SubGraph_SaveLoadBool_Load_Out_74;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_74;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Save_Out += SubGraph_SaveLoadBool_Save_Out_101;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Load_Out += SubGraph_SaveLoadBool_Load_Out_101;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_101;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_25.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_35.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_25.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_35.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.OnEnable();
		logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_85.OnEnable();
		logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_86.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_25.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_29.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_35.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_45.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_48.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_54.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_69.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_76.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_104.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_120.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_141.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_152.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_171.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_173.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_175.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_25.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_35.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_25.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_35.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.OnDestroy();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.Complete -= SubGraph_DefeatEnemyTechs_Complete_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save_Out -= SubGraph_SaveLoadBool_Save_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load_Out -= SubGraph_SaveLoadBool_Load_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Save_Out -= SubGraph_SaveLoadBool_Save_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Load_Out -= SubGraph_SaveLoadBool_Load_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output1 -= uScriptCon_ManualSwitch_Output1_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output2 -= uScriptCon_ManualSwitch_Output2_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output3 -= uScriptCon_ManualSwitch_Output3_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output4 -= uScriptCon_ManualSwitch_Output4_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output5 -= uScriptCon_ManualSwitch_Output5_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output6 -= uScriptCon_ManualSwitch_Output6_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output7 -= uScriptCon_ManualSwitch_Output7_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output8 -= uScriptCon_ManualSwitch_Output8_23;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_25.Out -= SubGraph_CompleteObjectiveStage_Out_25;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.Out -= SubGraph_CompleteObjectiveStage_Out_31;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.Save_Out -= SubGraph_SaveLoadInt_Save_Out_33;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.Load_Out -= SubGraph_SaveLoadInt_Load_Out_33;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_33;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_35.Out -= SubGraph_LoadObjectiveStates_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save_Out -= SubGraph_SaveLoadBool_Save_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load_Out -= SubGraph_SaveLoadBool_Load_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Save_Out -= SubGraph_SaveLoadBool_Save_Out_74;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Load_Out -= SubGraph_SaveLoadBool_Load_Out_74;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_74;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Save_Out -= SubGraph_SaveLoadBool_Save_Out_101;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Load_Out -= SubGraph_SaveLoadBool_Load_Out_101;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_101;
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

	private void Instance_SaveEvent_13(object o, EventArgs e)
	{
		Relay_SaveEvent_13();
	}

	private void Instance_LoadEvent_13(object o, EventArgs e)
	{
		Relay_LoadEvent_13();
	}

	private void Instance_RestartEvent_13(object o, EventArgs e)
	{
		Relay_RestartEvent_13();
	}

	private void SubGraph_DefeatEnemyTechs_Complete_5(object o, SubGraph_DefeatEnemyTechs.LogicEventArgs e)
	{
		Relay_Complete_5();
	}

	private void SubGraph_SaveLoadBool_Save_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_CrateSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Save_Out_12();
	}

	private void SubGraph_SaveLoadBool_Load_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_CrateSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Load_Out_12();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_CrateSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Restart_Out_12();
	}

	private void SubGraph_SaveLoadBool_Save_Out_22(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = e.boolean;
		local_ShownMsgTrapSprung_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_22;
		Relay_Save_Out_22();
	}

	private void SubGraph_SaveLoadBool_Load_Out_22(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = e.boolean;
		local_ShownMsgTrapSprung_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_22;
		Relay_Load_Out_22();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_22(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = e.boolean;
		local_ShownMsgTrapSprung_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_22;
		Relay_Restart_Out_22();
	}

	private void uScriptCon_ManualSwitch_Output1_23(object o, EventArgs e)
	{
		Relay_Output1_23();
	}

	private void uScriptCon_ManualSwitch_Output2_23(object o, EventArgs e)
	{
		Relay_Output2_23();
	}

	private void uScriptCon_ManualSwitch_Output3_23(object o, EventArgs e)
	{
		Relay_Output3_23();
	}

	private void uScriptCon_ManualSwitch_Output4_23(object o, EventArgs e)
	{
		Relay_Output4_23();
	}

	private void uScriptCon_ManualSwitch_Output5_23(object o, EventArgs e)
	{
		Relay_Output5_23();
	}

	private void uScriptCon_ManualSwitch_Output6_23(object o, EventArgs e)
	{
		Relay_Output6_23();
	}

	private void uScriptCon_ManualSwitch_Output7_23(object o, EventArgs e)
	{
		Relay_Output7_23();
	}

	private void uScriptCon_ManualSwitch_Output8_23(object o, EventArgs e)
	{
		Relay_Output8_23();
	}

	private void SubGraph_CompleteObjectiveStage_Out_25(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_25 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_25;
		Relay_Out_25();
	}

	private void SubGraph_CompleteObjectiveStage_Out_31(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_31 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_31;
		Relay_Out_31();
	}

	private void SubGraph_SaveLoadInt_Save_Out_33(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_33 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_33;
		Relay_Save_Out_33();
	}

	private void SubGraph_SaveLoadInt_Load_Out_33(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_33 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_33;
		Relay_Load_Out_33();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_33(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_33 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_33;
		Relay_Restart_Out_33();
	}

	private void SubGraph_LoadObjectiveStates_Out_35(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_35();
	}

	private void SubGraph_SaveLoadBool_Save_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_MsgApproachCrateShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Save_Out_58();
	}

	private void SubGraph_SaveLoadBool_Load_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_MsgApproachCrateShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Load_Out_58();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_MsgApproachCrateShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Restart_Out_58();
	}

	private void SubGraph_SaveLoadBool_Save_Out_74(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = e.boolean;
		local_NPCSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_74;
		Relay_Save_Out_74();
	}

	private void SubGraph_SaveLoadBool_Load_Out_74(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = e.boolean;
		local_NPCSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_74;
		Relay_Load_Out_74();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_74(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = e.boolean;
		local_NPCSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_74;
		Relay_Restart_Out_74();
	}

	private void SubGraph_SaveLoadBool_Save_Out_101(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = e.boolean;
		local_WelcomeMessagePlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_101;
		Relay_Save_Out_101();
	}

	private void SubGraph_SaveLoadBool_Load_Out_101(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = e.boolean;
		local_WelcomeMessagePlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_101;
		Relay_Load_Out_101();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_101(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = e.boolean;
		local_WelcomeMessagePlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_101;
		Relay_Restart_Out_101();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_72();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Complete_5()
	{
		Relay_In_31();
	}

	private void Relay_In_5()
	{
		int num = 0;
		Array array = ambushEnemyTechData;
		if (logic_SubGraph_DefeatEnemyTechs_enemyTechData_5.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_DefeatEnemyTechs_enemyTechData_5, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_DefeatEnemyTechs_enemyTechData_5, num, array.Length);
		num += array.Length;
		logic_SubGraph_DefeatEnemyTechs_clearSceneryPos_5 = clearSceneryPos;
		logic_SubGraph_DefeatEnemyTechs_clearSceneryRadius_5 = clearSceneryRadius;
		int num2 = 0;
		Array array2 = msgEnemySpotted;
		if (logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_5.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_5, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_5, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array array3 = msgComplete;
		if (logic_SubGraph_DefeatEnemyTechs_msgComplete_5.Length != num3 + array3.Length)
		{
			Array.Resize(ref logic_SubGraph_DefeatEnemyTechs_msgComplete_5, num3 + array3.Length);
		}
		Array.Copy(array3, 0, logic_SubGraph_DefeatEnemyTechs_msgComplete_5, num3, array3.Length);
		num3 += array3.Length;
		logic_SubGraph_DefeatEnemyTechs_messageSpeaker_5 = messageSpeaker;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.In(logic_SubGraph_DefeatEnemyTechs_enemyTechData_5, logic_SubGraph_DefeatEnemyTechs_distEnemiesSpotted_5, logic_SubGraph_DefeatEnemyTechs_clearSceneryPos_5, logic_SubGraph_DefeatEnemyTechs_clearSceneryRadius_5, logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_5, logic_SubGraph_DefeatEnemyTechs_msgComplete_5, logic_SubGraph_DefeatEnemyTechs_messageSpeaker_5);
	}

	private void Relay_In_7()
	{
		logic_uScriptCon_CompareBool_Bool_7 = local_CrateSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.In(logic_uScriptCon_CompareBool_Bool_7);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.False;
		if (num)
		{
			Relay_In_162();
		}
		if (flag)
		{
			Relay_In_96();
		}
	}

	private void Relay_In_9()
	{
		logic_uScript_SpawnDeliveryCrate_positionName_9 = crateSpawnPos;
		logic_uScript_SpawnDeliveryCrate_ownerNode_9 = owner_Connection_8;
		logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_9.In(logic_uScript_SpawnDeliveryCrate_positionName_9, logic_uScript_SpawnDeliveryCrate_ownerNode_9, logic_uScript_SpawnDeliveryCrate_visibleOnRadar_9);
		if (logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_9.Out)
		{
			Relay_True_98();
		}
	}

	private void Relay_Save_Out_12()
	{
		Relay_Save_101();
	}

	private void Relay_Load_Out_12()
	{
		Relay_Load_101();
	}

	private void Relay_Restart_Out_12()
	{
		Relay_Set_False_101();
	}

	private void Relay_Save_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Load_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Set_True_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Set_False_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_SaveEvent_13()
	{
		Relay_Save_74();
	}

	private void Relay_LoadEvent_13()
	{
		Relay_Load_74();
	}

	private void Relay_RestartEvent_13()
	{
		Relay_Set_False_74();
	}

	private void Relay_In_16()
	{
		int num = 0;
		Array array = msgTrapSprung;
		if (logic_uScript_AddOnScreenMessage_locString_16.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_16, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_16, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_16 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_16 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16.In(logic_uScript_AddOnScreenMessage_locString_16, logic_uScript_AddOnScreenMessage_msgPriority_16, logic_uScript_AddOnScreenMessage_holdMsg_16, logic_uScript_AddOnScreenMessage_tag_16, logic_uScript_AddOnScreenMessage_speaker_16, logic_uScript_AddOnScreenMessage_side_16);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16.Out)
		{
			Relay_True_19();
		}
	}

	private void Relay_In_18()
	{
		logic_uScriptCon_CompareBool_Bool_18 = local_ShownMsgTrapSprung_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.In(logic_uScriptCon_CompareBool_Bool_18);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.False;
		if (num)
		{
			Relay_In_5();
		}
		if (flag)
		{
			Relay_In_125();
		}
	}

	private void Relay_True_19()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_19.True(out logic_uScriptAct_SetBool_Target_19);
		local_ShownMsgTrapSprung_System_Boolean = logic_uScriptAct_SetBool_Target_19;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_19.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_False_19()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_19.False(out logic_uScriptAct_SetBool_Target_19);
		local_ShownMsgTrapSprung_System_Boolean = logic_uScriptAct_SetBool_Target_19;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_19.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_Save_Out_22()
	{
		Relay_Save_33();
	}

	private void Relay_Load_Out_22()
	{
		Relay_Load_33();
	}

	private void Relay_Restart_Out_22()
	{
		Relay_Restart_33();
	}

	private void Relay_Save_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Save(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Load_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Load(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Set_True_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Set_False_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Output1_23()
	{
		Relay_In_45();
	}

	private void Relay_Output2_23()
	{
		Relay_In_18();
	}

	private void Relay_Output3_23()
	{
		Relay_In_129();
	}

	private void Relay_Output4_23()
	{
	}

	private void Relay_Output5_23()
	{
	}

	private void Relay_Output6_23()
	{
	}

	private void Relay_Output7_23()
	{
	}

	private void Relay_Output8_23()
	{
	}

	private void Relay_In_23()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_23 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.In(logic_uScriptCon_ManualSwitch_CurrentOutput_23);
	}

	private void Relay_Out_25()
	{
	}

	private void Relay_In_25()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_25 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_25.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_25, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_25);
	}

	private void Relay_In_27()
	{
		logic_uScript_UnlockDeliveryCrate_ownerNode_27 = owner_Connection_30;
		logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_27.In(logic_uScript_UnlockDeliveryCrate_ownerNode_27);
		if (logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_27.Out)
		{
			Relay_In_120();
		}
	}

	private void Relay_In_29()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_29 = local_Crate_Crate;
		logic_uScript_IsPlayerInRangeOfVisible_range_29 = local_57_System_Single;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_29.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_29, logic_uScript_IsPlayerInRangeOfVisible_range_29);
		if (logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_29.InRange)
		{
			Relay_In_27();
		}
	}

	private void Relay_Out_31()
	{
	}

	private void Relay_In_31()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_31 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_31, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_31);
	}

	private void Relay_Save_Out_33()
	{
	}

	private void Relay_Load_Out_33()
	{
		Relay_In_35();
	}

	private void Relay_Restart_Out_33()
	{
	}

	private void Relay_Save_33()
	{
		logic_SubGraph_SaveLoadInt_integer_33 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_33 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.Save(logic_SubGraph_SaveLoadInt_restartValue_33, ref logic_SubGraph_SaveLoadInt_integer_33, logic_SubGraph_SaveLoadInt_intAsVariable_33, logic_SubGraph_SaveLoadInt_uniqueID_33);
	}

	private void Relay_Load_33()
	{
		logic_SubGraph_SaveLoadInt_integer_33 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_33 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.Load(logic_SubGraph_SaveLoadInt_restartValue_33, ref logic_SubGraph_SaveLoadInt_integer_33, logic_SubGraph_SaveLoadInt_intAsVariable_33, logic_SubGraph_SaveLoadInt_uniqueID_33);
	}

	private void Relay_Restart_33()
	{
		logic_SubGraph_SaveLoadInt_integer_33 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_33 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_33.Restart(logic_SubGraph_SaveLoadInt_restartValue_33, ref logic_SubGraph_SaveLoadInt_integer_33, logic_SubGraph_SaveLoadInt_intAsVariable_33, logic_SubGraph_SaveLoadInt_uniqueID_33);
	}

	private void Relay_Out_35()
	{
	}

	private void Relay_In_35()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_35 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_35.In(logic_SubGraph_LoadObjectiveStates_currentObjective_35);
	}

	private void Relay_In_45()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_45 = local_Crate_Crate;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_45.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_45, logic_uScript_IsPlayerInRangeOfVisible_range_45);
		if (logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_45.InRange)
		{
			Relay_In_47();
		}
	}

	private void Relay_In_47()
	{
		logic_uScriptCon_CompareBool_Bool_47 = local_MsgApproachCrateShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.In(logic_uScriptCon_CompareBool_Bool_47);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.False;
		if (num)
		{
			Relay_In_54();
		}
		if (flag)
		{
			Relay_True_50();
		}
	}

	private void Relay_In_48()
	{
		int num = 0;
		Array array = msgApproachCrate;
		if (logic_uScript_AddOnScreenMessage_locString_48.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_48, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_48, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_48 = local_124_System_String;
		logic_uScript_AddOnScreenMessage_speaker_48 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_48 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_48.In(logic_uScript_AddOnScreenMessage_locString_48, logic_uScript_AddOnScreenMessage_msgPriority_48, logic_uScript_AddOnScreenMessage_holdMsg_48, logic_uScript_AddOnScreenMessage_tag_48, logic_uScript_AddOnScreenMessage_speaker_48, logic_uScript_AddOnScreenMessage_side_48);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_48.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_True_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.True(out logic_uScriptAct_SetBool_Target_50);
		local_MsgApproachCrateShown_System_Boolean = logic_uScriptAct_SetBool_Target_50;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_50.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_False_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.False(out logic_uScriptAct_SetBool_Target_50);
		local_MsgApproachCrateShown_System_Boolean = logic_uScriptAct_SetBool_Target_50;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_50.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_In_54()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_54 = local_Crate_Crate;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_54.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_54, logic_uScript_IsPlayerInRangeOfVisible_range_54);
		if (logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_54.InRange)
		{
			Relay_In_25();
		}
	}

	private void Relay_In_55()
	{
		logic_uScript_GetCrateOpenTriggerRange_crate_55 = local_Crate_Crate;
		logic_uScript_GetCrateOpenTriggerRange_Return_55 = logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_55.In(logic_uScript_GetCrateOpenTriggerRange_crate_55);
		local_57_System_Single = logic_uScript_GetCrateOpenTriggerRange_Return_55;
		if (logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_55.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_Save_Out_58()
	{
		Relay_Save_22();
	}

	private void Relay_Load_Out_58()
	{
		Relay_Load_22();
	}

	private void Relay_Restart_Out_58()
	{
		Relay_Set_False_22();
	}

	private void Relay_Save_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Load_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Set_True_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Set_False_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Succeed_64()
	{
		logic_uScript_FinishEncounter_owner_64 = owner_Connection_63;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_64.Succeed(logic_uScript_FinishEncounter_owner_64);
	}

	private void Relay_Fail_64()
	{
		logic_uScript_FinishEncounter_owner_64 = owner_Connection_63;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_64.Fail(logic_uScript_FinishEncounter_owner_64);
	}

	private void Relay_InitialSpawn_66()
	{
		int num = 0;
		Array chaseEnemiesSpawnData = ChaseEnemiesSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_66.Length != num + chaseEnemiesSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_66, num + chaseEnemiesSpawnData.Length);
		}
		Array.Copy(chaseEnemiesSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_66, num, chaseEnemiesSpawnData.Length);
		num += chaseEnemiesSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_66 = owner_Connection_65;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_66.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_66, logic_uScript_SpawnTechsFromData_ownerNode_66, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_66);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_66.Out)
		{
			Relay_True_137();
		}
	}

	private void Relay_In_69()
	{
		int num = 0;
		Array array = msgTurretWarning;
		if (logic_uScript_AddOnScreenMessage_locString_69.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_69, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_69, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_69 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_69 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_69.In(logic_uScript_AddOnScreenMessage_locString_69, logic_uScript_AddOnScreenMessage_msgPriority_69, logic_uScript_AddOnScreenMessage_holdMsg_69, logic_uScript_AddOnScreenMessage_tag_69, logic_uScript_AddOnScreenMessage_speaker_69, logic_uScript_AddOnScreenMessage_side_69);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_69.Out)
		{
			Relay_True_100();
		}
	}

	private void Relay_InitialSpawn_70()
	{
		int num = 0;
		Array nPCTechSpawn = NPCTechSpawn;
		if (logic_uScript_SpawnTechsFromData_spawnData_70.Length != num + nPCTechSpawn.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_70, num + nPCTechSpawn.Length);
		}
		Array.Copy(nPCTechSpawn, 0, logic_uScript_SpawnTechsFromData_spawnData_70, num, nPCTechSpawn.Length);
		num += nPCTechSpawn.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_70 = owner_Connection_71;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_70.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_70, logic_uScript_SpawnTechsFromData_ownerNode_70, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_70);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_70.Out)
		{
			Relay_True_80();
		}
	}

	private void Relay_In_72()
	{
		logic_uScriptCon_CompareBool_Bool_72 = local_NPCSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.In(logic_uScriptCon_CompareBool_Bool_72);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.False;
		if (num)
		{
			Relay_In_105();
		}
		if (flag)
		{
			Relay_InitialSpawn_70();
		}
	}

	private void Relay_Save_Out_74()
	{
		Relay_Save_12();
	}

	private void Relay_Load_Out_74()
	{
		Relay_Load_12();
	}

	private void Relay_Restart_Out_74()
	{
		Relay_Set_False_12();
	}

	private void Relay_Save_74()
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_74 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Save(ref logic_SubGraph_SaveLoadBool_boolean_74, logic_SubGraph_SaveLoadBool_boolAsVariable_74, logic_SubGraph_SaveLoadBool_uniqueID_74);
	}

	private void Relay_Load_74()
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_74 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Load(ref logic_SubGraph_SaveLoadBool_boolean_74, logic_SubGraph_SaveLoadBool_boolAsVariable_74, logic_SubGraph_SaveLoadBool_uniqueID_74);
	}

	private void Relay_Set_True_74()
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_74 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_74, logic_SubGraph_SaveLoadBool_boolAsVariable_74, logic_SubGraph_SaveLoadBool_uniqueID_74);
	}

	private void Relay_Set_False_74()
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_74 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_74, logic_SubGraph_SaveLoadBool_boolAsVariable_74, logic_SubGraph_SaveLoadBool_uniqueID_74);
	}

	private void Relay_In_76()
	{
		int num = 0;
		Array array = local_firstEnemies_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_76.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_76, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_76, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_76.In(logic_uScript_InRangeOfAtLeastOneTech_techs_76, logic_uScript_InRangeOfAtLeastOneTech_range_76);
		if (logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_76.InRange)
		{
			Relay_In_384();
		}
	}

	private void Relay_In_78()
	{
		int num = 0;
		Array array = firstEnemiesSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_78.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_78, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_78, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_78 = owner_Connection_77;
		int num2 = 0;
		Array array2 = local_firstEnemies_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_78.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_78, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_78, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_78 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_78.In(logic_uScript_GetAndCheckTechs_techData_78, logic_uScript_GetAndCheckTechs_ownerNode_78, ref logic_uScript_GetAndCheckTechs_techs_78);
		local_firstEnemies_TankArray = logic_uScript_GetAndCheckTechs_techs_78;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_78.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_78.SomeAlive;
		if (allAlive)
		{
			Relay_In_76();
		}
		if (someAlive)
		{
			Relay_In_76();
		}
	}

	private void Relay_True_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.True(out logic_uScriptAct_SetBool_Target_80);
		local_NPCSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_80;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_80.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_False_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.False(out logic_uScriptAct_SetBool_Target_80);
		local_NPCSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_80;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_80.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_In_82()
	{
		logic_uScript_SetEncounterTarget_owner_82 = owner_Connection_83;
		logic_uScript_SetEncounterTarget_visibleObject_82 = local_Crate_Crate;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_82.In(logic_uScript_SetEncounterTarget_owner_82, logic_uScript_SetEncounterTarget_visibleObject_82);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_82.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_85()
	{
		logic_uScript_CheckDeliveryCrateSpawned_ownerNode_85 = owner_Connection_89;
		logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_85.In(logic_uScript_CheckDeliveryCrateSpawned_ownerNode_85);
		if (logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_85.Yes)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_86()
	{
		logic_uScript_GetDeliveryCrate_ownerNode_86 = owner_Connection_84;
		logic_uScript_GetDeliveryCrate_Return_86 = logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_86.In(logic_uScript_GetDeliveryCrate_ownerNode_86);
		local_Crate_Crate = logic_uScript_GetDeliveryCrate_Return_86;
		if (logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_86.Success)
		{
			Relay_In_82();
		}
	}

	private void Relay_In_91()
	{
		logic_uScriptCon_CompareBool_Bool_91 = local_ObjectiveMessagePlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_91.In(logic_uScriptCon_CompareBool_Bool_91);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_91.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_91.False;
		if (num)
		{
			Relay_In_23();
		}
		if (flag)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_96()
	{
		int num = 0;
		Array nPCTechSpawn = NPCTechSpawn;
		if (logic_uScript_GetAndCheckTechs_techData_96.Length != num + nPCTechSpawn.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_96, num + nPCTechSpawn.Length);
		}
		Array.Copy(nPCTechSpawn, 0, logic_uScript_GetAndCheckTechs_techData_96, num, nPCTechSpawn.Length);
		num += nPCTechSpawn.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_96 = owner_Connection_94;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_96.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_96, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_96, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_96 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_96.In(logic_uScript_GetAndCheckTechs_techData_96, logic_uScript_GetAndCheckTechs_ownerNode_96, ref logic_uScript_GetAndCheckTechs_techs_96);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_96;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_96.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_96.SomeAlive;
		if (allAlive)
		{
			Relay_In_171();
		}
		if (someAlive)
		{
			Relay_In_171();
		}
	}

	private void Relay_True_98()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.True(out logic_uScriptAct_SetBool_Target_98);
		local_CrateSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_98;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_98.Out)
		{
			Relay_In_162();
		}
	}

	private void Relay_False_98()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.False(out logic_uScriptAct_SetBool_Target_98);
		local_CrateSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_98;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_98.Out)
		{
			Relay_In_162();
		}
	}

	private void Relay_True_100()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_100.True(out logic_uScriptAct_SetBool_Target_100);
		local_ObjectiveMessagePlayed_System_Boolean = logic_uScriptAct_SetBool_Target_100;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_100.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_False_100()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_100.False(out logic_uScriptAct_SetBool_Target_100);
		local_ObjectiveMessagePlayed_System_Boolean = logic_uScriptAct_SetBool_Target_100;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_100.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_Save_Out_101()
	{
		Relay_Save_58();
	}

	private void Relay_Load_Out_101()
	{
		Relay_Load_58();
	}

	private void Relay_Restart_Out_101()
	{
		Relay_Set_False_58();
	}

	private void Relay_Save_101()
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = local_WelcomeMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_101 = local_WelcomeMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Save(ref logic_SubGraph_SaveLoadBool_boolean_101, logic_SubGraph_SaveLoadBool_boolAsVariable_101, logic_SubGraph_SaveLoadBool_uniqueID_101);
	}

	private void Relay_Load_101()
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = local_WelcomeMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_101 = local_WelcomeMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Load(ref logic_SubGraph_SaveLoadBool_boolean_101, logic_SubGraph_SaveLoadBool_boolAsVariable_101, logic_SubGraph_SaveLoadBool_uniqueID_101);
	}

	private void Relay_Set_True_101()
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = local_WelcomeMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_101 = local_WelcomeMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_101, logic_SubGraph_SaveLoadBool_boolAsVariable_101, logic_SubGraph_SaveLoadBool_uniqueID_101);
	}

	private void Relay_Set_False_101()
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = local_WelcomeMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_101 = local_WelcomeMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_101, logic_SubGraph_SaveLoadBool_boolAsVariable_101, logic_SubGraph_SaveLoadBool_uniqueID_101);
	}

	private void Relay_In_104()
	{
		logic_uScript_SetTankInvulnerable_tank_104 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_104.In(logic_uScript_SetTankInvulnerable_invulnerable_104, logic_uScript_SetTankInvulnerable_tank_104);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_104.Out)
		{
			Relay_In_141();
		}
	}

	private void Relay_In_105()
	{
		int num = 0;
		Array nPCTechSpawn = NPCTechSpawn;
		if (logic_uScript_GetAndCheckTechs_techData_105.Length != num + nPCTechSpawn.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_105, num + nPCTechSpawn.Length);
		}
		Array.Copy(nPCTechSpawn, 0, logic_uScript_GetAndCheckTechs_techData_105, num, nPCTechSpawn.Length);
		num += nPCTechSpawn.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_105 = owner_Connection_106;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_105.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_105, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_105, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_105 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.In(logic_uScript_GetAndCheckTechs_techData_105, logic_uScript_GetAndCheckTechs_ownerNode_105, ref logic_uScript_GetAndCheckTechs_techs_105);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_105;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_107();
		}
		if (someAlive)
		{
			Relay_AtIndex_107();
		}
	}

	private void Relay_AtIndex_107()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_107.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_107, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_107, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_107.AtIndex(ref logic_uScript_AccessListTech_techList_107, logic_uScript_AccessListTech_index_107, out logic_uScript_AccessListTech_value_107);
		local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_107;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_107;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_107.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_InitialSpawn_115()
	{
		int num = 0;
		Array array = firstEnemiesSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_115.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_115, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_115, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_115 = owner_Connection_111;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_115.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_115, logic_uScript_SpawnTechsFromData_ownerNode_115, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_115);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_115.Out)
		{
			Relay_True_117();
		}
	}

	private void Relay_In_116()
	{
		logic_uScriptCon_CompareBool_Bool_116 = local_FirstEnemySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_116.In(logic_uScriptCon_CompareBool_Bool_116);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_116.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_116.False;
		if (num)
		{
			Relay_In_148();
		}
		if (flag)
		{
			Relay_InitialSpawn_115();
		}
	}

	private void Relay_True_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.True(out logic_uScriptAct_SetBool_Target_117);
		local_FirstEnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_117;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_117.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_False_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.False(out logic_uScriptAct_SetBool_Target_117);
		local_FirstEnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_117;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_117.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_120()
	{
		int num = 0;
		Array array = msgRunAway;
		if (logic_uScript_AddOnScreenMessage_locString_120.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_120, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_120, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_120 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_120 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_120.In(logic_uScript_AddOnScreenMessage_locString_120, logic_uScript_AddOnScreenMessage_msgPriority_120, logic_uScript_AddOnScreenMessage_holdMsg_120, logic_uScript_AddOnScreenMessage_tag_120, logic_uScript_AddOnScreenMessage_speaker_120, logic_uScript_AddOnScreenMessage_side_120);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_120.Shown)
		{
			Relay_True_156();
		}
	}

	private void Relay_In_121()
	{
		logic_uScript_FlyTechUpAndAway_tech_121 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_121 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_121 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_121.In(logic_uScript_FlyTechUpAndAway_tech_121, logic_uScript_FlyTechUpAndAway_maxLifetime_121, logic_uScript_FlyTechUpAndAway_targetHeight_121, logic_uScript_FlyTechUpAndAway_aiTree_121, logic_uScript_FlyTechUpAndAway_removalParticles_121);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_121.Out)
		{
			Relay_True_133();
		}
	}

	private void Relay_In_125()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_125 = local_126_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_125.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_125, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_125);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_125.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_128()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_128 = local_127_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_128.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_128, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_128);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_128.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_In_129()
	{
		logic_uScriptCon_CompareBool_Bool_129 = local_CrateOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.In(logic_uScriptCon_CompareBool_Bool_129);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.False;
		if (num)
		{
			Relay_In_131();
		}
		if (flag)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_131()
	{
		logic_uScriptCon_CompareBool_Bool_131 = local_NPCExit_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.In(logic_uScriptCon_CompareBool_Bool_131);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.False;
		if (num)
		{
			Relay_In_135();
		}
		if (flag)
		{
			Relay_In_121();
		}
	}

	private void Relay_True_133()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_133.True(out logic_uScriptAct_SetBool_Target_133);
		local_NPCExit_System_Boolean = logic_uScriptAct_SetBool_Target_133;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_133.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_False_133()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_133.False(out logic_uScriptAct_SetBool_Target_133);
		local_NPCExit_System_Boolean = logic_uScriptAct_SetBool_Target_133;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_133.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_135()
	{
		logic_uScriptCon_CompareBool_Bool_135 = local_ChaseTechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.In(logic_uScriptCon_CompareBool_Bool_135);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.False;
		if (num)
		{
			Relay_In_158();
		}
		if (flag)
		{
			Relay_InitialSpawn_66();
		}
	}

	private void Relay_True_137()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_137.True(out logic_uScriptAct_SetBool_Target_137);
		local_ChaseTechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_137;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_137.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_False_137()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_137.False(out logic_uScriptAct_SetBool_Target_137);
		local_ChaseTechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_137;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_137.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_141()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_141 = owner_Connection_142;
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_141.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_141, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_141, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_141 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_141.In(logic_uScript_SetOneTechAsEncounterTarget_owner_141, logic_uScript_SetOneTechAsEncounterTarget_techs_141);
		local_NPCTech_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_141;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_141.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_In_145()
	{
		int num = 0;
		Array nPCTechSpawn = NPCTechSpawn;
		if (logic_uScript_GetAndCheckTechs_techData_145.Length != num + nPCTechSpawn.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_145, num + nPCTechSpawn.Length);
		}
		Array.Copy(nPCTechSpawn, 0, logic_uScript_GetAndCheckTechs_techData_145, num, nPCTechSpawn.Length);
		num += nPCTechSpawn.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_145 = owner_Connection_147;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_145.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_145, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_145, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_145 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_145.In(logic_uScript_GetAndCheckTechs_techData_145, logic_uScript_GetAndCheckTechs_ownerNode_145, ref logic_uScript_GetAndCheckTechs_techs_145);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_145;
	}

	private void Relay_In_148()
	{
		logic_uScriptCon_CompareBool_Bool_148 = local_MetNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_148.In(logic_uScriptCon_CompareBool_Bool_148);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_148.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_148.False;
		if (num)
		{
			Relay_In_7();
		}
		if (flag)
		{
			Relay_In_152();
		}
	}

	private void Relay_True_150()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_150.True(out logic_uScriptAct_SetBool_Target_150);
		local_MetNPC_System_Boolean = logic_uScriptAct_SetBool_Target_150;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_150.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_False_150()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_150.False(out logic_uScriptAct_SetBool_Target_150);
		local_MetNPC_System_Boolean = logic_uScriptAct_SetBool_Target_150;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_150.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_152()
	{
		int num = 0;
		Array array = msgNPCGreeting01;
		if (logic_uScript_AddOnScreenMessage_locString_152.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_152, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_152, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_152 = local_153_System_String;
		logic_uScript_AddOnScreenMessage_speaker_152 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_152 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_152.In(logic_uScript_AddOnScreenMessage_locString_152, logic_uScript_AddOnScreenMessage_msgPriority_152, logic_uScript_AddOnScreenMessage_holdMsg_152, logic_uScript_AddOnScreenMessage_tag_152, logic_uScript_AddOnScreenMessage_speaker_152, logic_uScript_AddOnScreenMessage_side_152);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_152.Out)
		{
			Relay_True_150();
		}
	}

	private void Relay_True_156()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_156.True(out logic_uScriptAct_SetBool_Target_156);
		local_CrateOpened_System_Boolean = logic_uScriptAct_SetBool_Target_156;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_156.Out)
		{
			Relay_In_131();
		}
	}

	private void Relay_False_156()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_156.False(out logic_uScriptAct_SetBool_Target_156);
		local_CrateOpened_System_Boolean = logic_uScriptAct_SetBool_Target_156;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_156.Out)
		{
			Relay_In_131();
		}
	}

	private void Relay_In_158()
	{
		int num = 0;
		Array chaseEnemiesSpawnData = ChaseEnemiesSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_158.Length != num + chaseEnemiesSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_158, num + chaseEnemiesSpawnData.Length);
		}
		Array.Copy(chaseEnemiesSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_158, num, chaseEnemiesSpawnData.Length);
		num += chaseEnemiesSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_158 = owner_Connection_160;
		int num2 = 0;
		Array array = local_ChaseTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_158.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_158, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_158, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_158 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_158.In(logic_uScript_GetAndCheckTechs_techData_158, logic_uScript_GetAndCheckTechs_ownerNode_158, ref logic_uScript_GetAndCheckTechs_techs_158);
		local_ChaseTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_158;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_158.AllAlive)
		{
			Relay_Succeed_64();
		}
	}

	private void Relay_In_162()
	{
		logic_uScriptCon_CompareBool_Bool_162 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.In(logic_uScriptCon_CompareBool_Bool_162);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.False;
		if (num)
		{
			Relay_In_85();
		}
		if (flag)
		{
			Relay_In_173();
		}
	}

	private void Relay_In_166()
	{
		int num = 0;
		Array array = msgObjective;
		if (logic_uScript_AddOnScreenMessage_locString_166.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_166, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_166, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_166 = local_167_System_String;
		logic_uScript_AddOnScreenMessage_speaker_166 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_166 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166.In(logic_uScript_AddOnScreenMessage_locString_166, logic_uScript_AddOnScreenMessage_msgPriority_166, logic_uScript_AddOnScreenMessage_holdMsg_166, logic_uScript_AddOnScreenMessage_tag_166, logic_uScript_AddOnScreenMessage_speaker_166, logic_uScript_AddOnScreenMessage_side_166);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166.Out)
		{
			Relay_True_169();
		}
	}

	private void Relay_True_169()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_169.True(out logic_uScriptAct_SetBool_Target_169);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_169;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_169.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_False_169()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_169.False(out logic_uScriptAct_SetBool_Target_169);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_169;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_169.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_171()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_IsPlayerInRangeOfTech_techs_171.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_IsPlayerInRangeOfTech_techs_171, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_IsPlayerInRangeOfTech_techs_171, num, array.Length);
		num += array.Length;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_171.In(logic_uScript_IsPlayerInRangeOfTech_tech_171, logic_uScript_IsPlayerInRangeOfTech_range_171, logic_uScript_IsPlayerInRangeOfTech_techs_171);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_171.InRange)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_173()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_173.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_173, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_173, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_173.In(logic_uScript_InRangeOfAtLeastOneTech_techs_173, logic_uScript_InRangeOfAtLeastOneTech_range_173);
		if (logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_173.InRange)
		{
			Relay_In_166();
		}
	}

	private void Relay_In_175()
	{
		int num = 0;
		Array array = msgMissionInRange;
		if (logic_uScript_AddOnScreenMessage_locString_175.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_175, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_175, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_175 = local_176_System_String;
		logic_uScript_AddOnScreenMessage_speaker_175 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_175 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_175.In(logic_uScript_AddOnScreenMessage_locString_175, logic_uScript_AddOnScreenMessage_msgPriority_175, logic_uScript_AddOnScreenMessage_holdMsg_175, logic_uScript_AddOnScreenMessage_tag_175, logic_uScript_AddOnScreenMessage_speaker_175, logic_uScript_AddOnScreenMessage_side_175);
	}

	private void Relay_In_384()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_384 = local_383_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_384.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_384, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_384);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_384.Out)
		{
			Relay_In_69();
		}
	}
}
