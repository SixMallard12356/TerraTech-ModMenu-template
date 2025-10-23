using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_DefeatEnemyTechs", "")]
public class Mission_AttackProtectedTarget : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool AllowEnemyGroupToRespawn = true;

	[Multiline(3)]
	public string clearSceneryPosNPC = "";

	[Multiline(3)]
	public string clearSceneryPosTarget = "";

	public float clearSceneryRadius;

	public float DelayBetweenEnemyGroupSpaws;

	public float DelayBetweenRespawnArivals;

	public float distAtWhichFriendlyTechFound = 75f;

	public float DistInRangeOfNPC = 50f;

	public SpawnTechData enemyGroupData;

	public WaveSizeSpecification EnemyGroupSize;

	private string local_106_System_String = "msgMeeting";

	private string local_70_System_String = "msgMeeting";

	private string local_87_System_String = "msgMeeting";

	private bool local_EnemyDeadEarly_System_Boolean;

	private string local_Msg_System_String = "Msg";

	private bool local_MsgFoundShown_System_Boolean;

	private bool local_NPCIgnored_System_Boolean;

	private bool local_NPCMet_System_Boolean;

	private bool local_NPCSeen_System_Boolean;

	private Tank local_NPCTank_Tank;

	private Tank[] local_NPCTanks_TankArray = new Tank[0];

	private bool local_ObjectiveComplete_System_Boolean;

	private bool local_ShownMsgEnemiesGivingUp_System_Boolean;

	private bool local_ShownMsgEnemySpotted_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private int local_StartValue_System_Int32;

	private Tank local_TargetTech_Tank;

	private bool local_TargetTechFound_System_Boolean;

	private Tank[] local_targetTechs_TankArray = new Tank[0];

	private bool local_TargetTechSpawned_System_Boolean;

	private int local_TotalGroupEnemiesSpawned_System_Int32;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgMissionComplete = new LocalisedString[0];

	public LocalisedString[] msgNPCGreeting = new LocalisedString[0];

	public LocalisedString[] msgNPCGreetingEnemyDead = new LocalisedString[0];

	public LocalisedString[] msgNPCGreetingInturrupt = new LocalisedString[0];

	public LocalisedString[] msgTargetTechFound = new LocalisedString[0];

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCTech = new SpawnTechData[0];

	public int ReinforcementSubGroupSize;

	public SpawnTechData[] targetTechData = new SpawnTechData[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_30;

	private GameObject owner_Connection_33;

	private GameObject owner_Connection_38;

	private GameObject owner_Connection_42;

	private GameObject owner_Connection_67;

	private GameObject owner_Connection_81;

	private GameObject owner_Connection_108;

	private GameObject owner_Connection_113;

	private GameObject owner_Connection_118;

	private GameObject owner_Connection_128;

	private GameObject owner_Connection_144;

	private GameObject owner_Connection_155;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_2 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_2 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_2;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_2 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_2 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_4 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_4;

	private bool logic_uScriptAct_SetBool_Out_4 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_4 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_4 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_5;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_5 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_5 = "TargetTechSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_6;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_6 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_6 = "MsgFoundShown";

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_13 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_13;

	private float logic_uScript_IsPlayerInRangeOfTech_range_13;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_13 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_13 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_13 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_13 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_16;

	private bool logic_uScriptCon_CompareBool_True_16 = true;

	private bool logic_uScriptCon_CompareBool_False_16 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_17 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_17;

	private bool logic_uScriptAct_SetBool_Out_17 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_17 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_17 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_18 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_18 = ManOnScreenMessages.MessagePriority.High;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_18;

	private string logic_uScript_AddOnScreenMessage_tag_18 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_18;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_18;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_18;

	private bool logic_uScript_AddOnScreenMessage_Out_18 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_18 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_21;

	private bool logic_uScriptCon_CompareBool_True_21 = true;

	private bool logic_uScriptCon_CompareBool_False_21 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_22;

	private bool logic_uScriptCon_CompareBool_True_22 = true;

	private bool logic_uScriptCon_CompareBool_False_22 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_24 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_24;

	private bool logic_uScriptAct_SetBool_Out_24 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_24 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_24 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_25;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_25 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_25 = "TargetTechFound";

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_34 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_34 = new Tank[0];

	private int logic_uScript_AccessListTech_index_34;

	private Tank logic_uScript_AccessListTech_value_34;

	private bool logic_uScript_AccessListTech_Out_34 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_37 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_37;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_37 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_37 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_39 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_39;

	private object logic_uScript_SetEncounterTarget_visibleObject_39 = "";

	private bool logic_uScript_SetEncounterTarget_Out_39 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_40 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_40;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_40 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_40;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_40 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_40 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_40 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_40 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_44 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_44;

	private bool logic_uScript_FinishEncounter_Out_44 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_45;

	private int logic_SubGraph_SaveLoadInt_integer_45;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_45 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_45 = "EnemyGroup";

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_47 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_47 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_47 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_47;

	private string logic_uScript_AddOnScreenMessage_tag_47 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_47;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_47;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_47;

	private bool logic_uScript_AddOnScreenMessage_Out_47 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_47 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_49 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_49 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_49 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_49 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_54;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_54 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_54 = "ShownMsgEnemySpotted";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_55;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_55 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_55 = "ShownMsgEnemiesGivingUp";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_56;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_56 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_56 = "AllowEnemyGroupToRespawn";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_62;

	private bool logic_uScriptCon_CompareBool_True_62 = true;

	private bool logic_uScriptCon_CompareBool_False_62 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_63;

	private bool logic_uScriptCon_CompareBool_True_63 = true;

	private bool logic_uScriptCon_CompareBool_False_63 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_65 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_65 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_65;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_65 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_65 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_68 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_68;

	private bool logic_uScriptAct_SetBool_Out_68 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_68 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_68 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_71 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_71 = new Tank[0];

	private int logic_uScript_AccessListTech_index_71;

	private Tank logic_uScript_AccessListTech_value_71;

	private bool logic_uScript_AccessListTech_Out_71 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_72;

	private bool logic_uScriptCon_CompareBool_True_72 = true;

	private bool logic_uScriptCon_CompareBool_False_72 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_74 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_74 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_74 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_74;

	private string logic_uScript_AddOnScreenMessage_tag_74 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_74;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_74;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_74;

	private bool logic_uScript_AddOnScreenMessage_Out_74 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_74 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_75 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_75 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_75;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_75 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_77 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_77;

	private bool logic_uScriptAct_SetBool_Out_77 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_77 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_77 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_83 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_83 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_83 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_83;

	private string logic_uScript_AddOnScreenMessage_tag_83 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_83;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_83;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_83;

	private bool logic_uScript_AddOnScreenMessage_Out_83 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_83 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_84 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_84;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_84 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_84 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_84;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_84;

	private bool logic_uScript_FlyTechUpAndAway_Out_84 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_88;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_88;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_92 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_92;

	private bool logic_uScriptAct_SetBool_Out_92 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_92 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_92 = true;

	private uScript_InRangeOfTech logic_uScript_InRangeOfTech_uScript_InRangeOfTech_94 = new uScript_InRangeOfTech();

	private Tank logic_uScript_InRangeOfTech_tank_94;

	private float logic_uScript_InRangeOfTech_range_94;

	private bool logic_uScript_InRangeOfTech_Out_94 = true;

	private bool logic_uScript_InRangeOfTech_InRange_94 = true;

	private bool logic_uScript_InRangeOfTech_OutOfRange_94 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_96 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_96;

	private bool logic_uScriptAct_SetBool_Out_96 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_96 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_96 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_99 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_99 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_99;

	private bool logic_uScript_SetTankInvulnerable_Out_99 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_102 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_102;

	private bool logic_uScriptAct_SetBool_Out_102 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_102 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_102 = true;

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

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_107 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_107 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_107;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_107 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_107;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_107 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_107 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_107 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_107 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_109 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_109 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_109;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_109 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_109;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_109 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_109 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_109 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_109 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_110 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_110;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_110 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_110;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_110 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_111;

	private bool logic_uScriptCon_CompareBool_True_111 = true;

	private bool logic_uScriptCon_CompareBool_False_111 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_119 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_119;

	private string logic_uScript_RemoveScenery_positionName_119 = "";

	private float logic_uScript_RemoveScenery_radius_119;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_119 = true;

	private bool logic_uScript_RemoveScenery_Out_119 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_120;

	private uScript_SpawnTechWaveFromData logic_uScript_SpawnTechWaveFromData_uScript_SpawnTechWaveFromData_122 = new uScript_SpawnTechWaveFromData();

	private GameObject logic_uScript_SpawnTechWaveFromData_ownerNode_122;

	private SpawnTechData logic_uScript_SpawnTechWaveFromData_spawnData_122;

	private WaveSizeSpecification logic_uScript_SpawnTechWaveFromData_waveSize_122;

	private float logic_uScript_SpawnTechWaveFromData_delayBetweenSpawns_122;

	private int logic_uScript_SpawnTechWaveFromData_numSpawnedSoFar_122;

	private bool logic_uScript_SpawnTechWaveFromData_allowRespawn_122;

	private int logic_uScript_SpawnTechWaveFromData_respawnGroupSize_122;

	private float logic_uScript_SpawnTechWaveFromData_delayBetweenRespawnGroups_122;

	private int logic_uScript_SpawnTechWaveFromData_Return_122;

	private bool logic_uScript_SpawnTechWaveFromData_Out_122 = true;

	private bool logic_uScript_SpawnTechWaveFromData_RespawnedWaveGroup_122 = true;

	private bool logic_uScript_SpawnTechWaveFromData_AllTechKilled_122 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_134;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_134 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_134 = "EnemyDeadEarly";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_135;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_135 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_135 = "NPCSeen";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_136;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_136 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_136 = "NPCMet";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_138;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_138 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_138 = "ObjectiveComplete";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_142 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_142;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_142 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_142;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_142 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_142 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_142 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_142 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_147 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_147;

	private bool logic_uScriptAct_SetBool_Out_147 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_147 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_147 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_148 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_148;

	private bool logic_uScriptAct_SetBool_Out_148 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_148 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_148 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_150 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_150;

	private bool logic_uScriptCon_CompareBool_True_150 = true;

	private bool logic_uScriptCon_CompareBool_False_150 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_153;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_153 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_153 = "NPCIgnored";

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_157 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_157;

	private string logic_uScript_RemoveScenery_positionName_157 = "";

	private float logic_uScript_RemoveScenery_radius_157;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_157 = true;

	private bool logic_uScript_RemoveScenery_Out_157 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_159 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_159;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_159 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_159 = "Stage";

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
		}
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
			if (null != owner_Connection_8)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_9;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_9;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_9;
				}
			}
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
			if (null != owner_Connection_11)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_11.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_11.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_10;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_10;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_10;
				}
			}
		}
		if (null == owner_Connection_30 || !m_RegisteredForEvents)
		{
			owner_Connection_30 = parentGameObject;
		}
		if (null == owner_Connection_33 || !m_RegisteredForEvents)
		{
			owner_Connection_33 = parentGameObject;
		}
		if (null == owner_Connection_38 || !m_RegisteredForEvents)
		{
			owner_Connection_38 = parentGameObject;
		}
		if (null == owner_Connection_42 || !m_RegisteredForEvents)
		{
			owner_Connection_42 = parentGameObject;
		}
		if (null == owner_Connection_67 || !m_RegisteredForEvents)
		{
			owner_Connection_67 = parentGameObject;
		}
		if (null == owner_Connection_81 || !m_RegisteredForEvents)
		{
			owner_Connection_81 = parentGameObject;
		}
		if (null == owner_Connection_108 || !m_RegisteredForEvents)
		{
			owner_Connection_108 = parentGameObject;
		}
		if (null == owner_Connection_113 || !m_RegisteredForEvents)
		{
			owner_Connection_113 = parentGameObject;
		}
		if (null == owner_Connection_118 || !m_RegisteredForEvents)
		{
			owner_Connection_118 = parentGameObject;
		}
		if (null == owner_Connection_128 || !m_RegisteredForEvents)
		{
			owner_Connection_128 = parentGameObject;
		}
		if (null == owner_Connection_144 || !m_RegisteredForEvents)
		{
			owner_Connection_144 = parentGameObject;
		}
		if (null == owner_Connection_155 || !m_RegisteredForEvents)
		{
			owner_Connection_155 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_8)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_9;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_9;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_9;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_11)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_11.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_11.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_10;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_10;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_10;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_8)
		{
			uScript_SaveLoad component = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_9;
				component.LoadEvent -= Instance_LoadEvent_9;
				component.RestartEvent -= Instance_RestartEvent_9;
			}
		}
		if (null != owner_Connection_11)
		{
			uScript_EncounterUpdate component2 = owner_Connection_11.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_10;
				component2.OnSuspend -= Instance_OnSuspend_10;
				component2.OnResume -= Instance_OnResume_10;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_2.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_13.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_34.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_37.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_39.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_44.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_47.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_49.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_65.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_68.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_71.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_74.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_75.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_77.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_83.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_84.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.SetParent(g);
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_94.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_99.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_107.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_109.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_110.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_119.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.SetParent(g);
		logic_uScript_SpawnTechWaveFromData_uScript_SpawnTechWaveFromData_122.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_147.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_150.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_157.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_30 = parentGameObject;
		owner_Connection_33 = parentGameObject;
		owner_Connection_38 = parentGameObject;
		owner_Connection_42 = parentGameObject;
		owner_Connection_67 = parentGameObject;
		owner_Connection_81 = parentGameObject;
		owner_Connection_108 = parentGameObject;
		owner_Connection_113 = parentGameObject;
		owner_Connection_118 = parentGameObject;
		owner_Connection_128 = parentGameObject;
		owner_Connection_144 = parentGameObject;
		owner_Connection_155 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Save_Out += SubGraph_SaveLoadBool_Save_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Load_Out += SubGraph_SaveLoadBool_Load_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save_Out += SubGraph_SaveLoadBool_Save_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load_Out += SubGraph_SaveLoadBool_Load_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Save_Out += SubGraph_SaveLoadBool_Save_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Load_Out += SubGraph_SaveLoadBool_Load_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_25;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.Save_Out += SubGraph_SaveLoadInt_Save_Out_45;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.Load_Out += SubGraph_SaveLoadInt_Load_Out_45;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_45;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Save_Out += SubGraph_SaveLoadBool_Save_Out_54;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Load_Out += SubGraph_SaveLoadBool_Load_Out_54;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_54;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Save_Out += SubGraph_SaveLoadBool_Save_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Load_Out += SubGraph_SaveLoadBool_Load_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Save_Out += SubGraph_SaveLoadBool_Save_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Load_Out += SubGraph_SaveLoadBool_Load_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_56;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Out += SubGraph_CompleteObjectiveStage_Out_88;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output1 += uScriptCon_ManualSwitch_Output1_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output2 += uScriptCon_ManualSwitch_Output2_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output3 += uScriptCon_ManualSwitch_Output3_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output4 += uScriptCon_ManualSwitch_Output4_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output5 += uScriptCon_ManualSwitch_Output5_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output6 += uScriptCon_ManualSwitch_Output6_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output7 += uScriptCon_ManualSwitch_Output7_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output8 += uScriptCon_ManualSwitch_Output8_120;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Save_Out += SubGraph_SaveLoadBool_Save_Out_134;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Load_Out += SubGraph_SaveLoadBool_Load_Out_134;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_134;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Save_Out += SubGraph_SaveLoadBool_Save_Out_135;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Load_Out += SubGraph_SaveLoadBool_Load_Out_135;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_135;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Save_Out += SubGraph_SaveLoadBool_Save_Out_136;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Load_Out += SubGraph_SaveLoadBool_Load_Out_136;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_136;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Save_Out += SubGraph_SaveLoadBool_Save_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Load_Out += SubGraph_SaveLoadBool_Load_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Save_Out += SubGraph_SaveLoadBool_Save_Out_153;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Load_Out += SubGraph_SaveLoadBool_Load_Out_153;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_153;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.Save_Out += SubGraph_SaveLoadInt_Save_Out_159;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.Load_Out += SubGraph_SaveLoadInt_Load_Out_159;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_159;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_84.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_13.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_47.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_74.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_83.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.OnDisable();
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_94.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_99.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_110.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Save_Out -= SubGraph_SaveLoadBool_Save_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Load_Out -= SubGraph_SaveLoadBool_Load_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save_Out -= SubGraph_SaveLoadBool_Save_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load_Out -= SubGraph_SaveLoadBool_Load_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Save_Out -= SubGraph_SaveLoadBool_Save_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Load_Out -= SubGraph_SaveLoadBool_Load_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_25;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.Save_Out -= SubGraph_SaveLoadInt_Save_Out_45;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.Load_Out -= SubGraph_SaveLoadInt_Load_Out_45;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_45;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Save_Out -= SubGraph_SaveLoadBool_Save_Out_54;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Load_Out -= SubGraph_SaveLoadBool_Load_Out_54;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_54;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Save_Out -= SubGraph_SaveLoadBool_Save_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Load_Out -= SubGraph_SaveLoadBool_Load_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Save_Out -= SubGraph_SaveLoadBool_Save_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Load_Out -= SubGraph_SaveLoadBool_Load_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_56;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Out -= SubGraph_CompleteObjectiveStage_Out_88;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output1 -= uScriptCon_ManualSwitch_Output1_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output2 -= uScriptCon_ManualSwitch_Output2_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output3 -= uScriptCon_ManualSwitch_Output3_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output4 -= uScriptCon_ManualSwitch_Output4_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output5 -= uScriptCon_ManualSwitch_Output5_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output6 -= uScriptCon_ManualSwitch_Output6_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output7 -= uScriptCon_ManualSwitch_Output7_120;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.Output8 -= uScriptCon_ManualSwitch_Output8_120;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Save_Out -= SubGraph_SaveLoadBool_Save_Out_134;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Load_Out -= SubGraph_SaveLoadBool_Load_Out_134;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_134;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Save_Out -= SubGraph_SaveLoadBool_Save_Out_135;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Load_Out -= SubGraph_SaveLoadBool_Load_Out_135;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_135;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Save_Out -= SubGraph_SaveLoadBool_Save_Out_136;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Load_Out -= SubGraph_SaveLoadBool_Load_Out_136;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_136;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Save_Out -= SubGraph_SaveLoadBool_Save_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Load_Out -= SubGraph_SaveLoadBool_Load_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Save_Out -= SubGraph_SaveLoadBool_Save_Out_153;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Load_Out -= SubGraph_SaveLoadBool_Load_Out_153;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_153;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.Save_Out -= SubGraph_SaveLoadInt_Save_Out_159;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.Load_Out -= SubGraph_SaveLoadInt_Load_Out_159;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_159;
	}

	private void Instance_SaveEvent_9(object o, EventArgs e)
	{
		Relay_SaveEvent_9();
	}

	private void Instance_LoadEvent_9(object o, EventArgs e)
	{
		Relay_LoadEvent_9();
	}

	private void Instance_RestartEvent_9(object o, EventArgs e)
	{
		Relay_RestartEvent_9();
	}

	private void Instance_OnUpdate_10(object o, EventArgs e)
	{
		Relay_OnUpdate_10();
	}

	private void Instance_OnSuspend_10(object o, EventArgs e)
	{
		Relay_OnSuspend_10();
	}

	private void Instance_OnResume_10(object o, EventArgs e)
	{
		Relay_OnResume_10();
	}

	private void SubGraph_SaveLoadBool_Save_Out_5(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = e.boolean;
		local_TargetTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_5;
		Relay_Save_Out_5();
	}

	private void SubGraph_SaveLoadBool_Load_Out_5(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = e.boolean;
		local_TargetTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_5;
		Relay_Load_Out_5();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_5(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = e.boolean;
		local_TargetTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_5;
		Relay_Restart_Out_5();
	}

	private void SubGraph_SaveLoadBool_Save_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_MsgFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Save_Out_6();
	}

	private void SubGraph_SaveLoadBool_Load_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_MsgFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Load_Out_6();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_MsgFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Restart_Out_6();
	}

	private void SubGraph_SaveLoadBool_Save_Out_25(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = e.boolean;
		local_TargetTechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_25;
		Relay_Save_Out_25();
	}

	private void SubGraph_SaveLoadBool_Load_Out_25(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = e.boolean;
		local_TargetTechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_25;
		Relay_Load_Out_25();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_25(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = e.boolean;
		local_TargetTechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_25;
		Relay_Restart_Out_25();
	}

	private void SubGraph_SaveLoadInt_Save_Out_45(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_45 = e.integer;
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_SubGraph_SaveLoadInt_integer_45;
		Relay_Save_Out_45();
	}

	private void SubGraph_SaveLoadInt_Load_Out_45(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_45 = e.integer;
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_SubGraph_SaveLoadInt_integer_45;
		Relay_Load_Out_45();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_45(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_45 = e.integer;
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_SubGraph_SaveLoadInt_integer_45;
		Relay_Restart_Out_45();
	}

	private void SubGraph_SaveLoadBool_Save_Out_54(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = e.boolean;
		local_ShownMsgEnemySpotted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_54;
		Relay_Save_Out_54();
	}

	private void SubGraph_SaveLoadBool_Load_Out_54(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = e.boolean;
		local_ShownMsgEnemySpotted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_54;
		Relay_Load_Out_54();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_54(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = e.boolean;
		local_ShownMsgEnemySpotted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_54;
		Relay_Restart_Out_54();
	}

	private void SubGraph_SaveLoadBool_Save_Out_55(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = e.boolean;
		local_ShownMsgEnemiesGivingUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_55;
		Relay_Save_Out_55();
	}

	private void SubGraph_SaveLoadBool_Load_Out_55(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = e.boolean;
		local_ShownMsgEnemiesGivingUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_55;
		Relay_Load_Out_55();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_55(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = e.boolean;
		local_ShownMsgEnemiesGivingUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_55;
		Relay_Restart_Out_55();
	}

	private void SubGraph_SaveLoadBool_Save_Out_56(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = e.boolean;
		AllowEnemyGroupToRespawn = logic_SubGraph_SaveLoadBool_boolean_56;
		Relay_Save_Out_56();
	}

	private void SubGraph_SaveLoadBool_Load_Out_56(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = e.boolean;
		AllowEnemyGroupToRespawn = logic_SubGraph_SaveLoadBool_boolean_56;
		Relay_Load_Out_56();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_56(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = e.boolean;
		AllowEnemyGroupToRespawn = logic_SubGraph_SaveLoadBool_boolean_56;
		Relay_Restart_Out_56();
	}

	private void SubGraph_CompleteObjectiveStage_Out_88(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_88 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_88;
		Relay_Out_88();
	}

	private void uScriptCon_ManualSwitch_Output1_120(object o, EventArgs e)
	{
		Relay_Output1_120();
	}

	private void uScriptCon_ManualSwitch_Output2_120(object o, EventArgs e)
	{
		Relay_Output2_120();
	}

	private void uScriptCon_ManualSwitch_Output3_120(object o, EventArgs e)
	{
		Relay_Output3_120();
	}

	private void uScriptCon_ManualSwitch_Output4_120(object o, EventArgs e)
	{
		Relay_Output4_120();
	}

	private void uScriptCon_ManualSwitch_Output5_120(object o, EventArgs e)
	{
		Relay_Output5_120();
	}

	private void uScriptCon_ManualSwitch_Output6_120(object o, EventArgs e)
	{
		Relay_Output6_120();
	}

	private void uScriptCon_ManualSwitch_Output7_120(object o, EventArgs e)
	{
		Relay_Output7_120();
	}

	private void uScriptCon_ManualSwitch_Output8_120(object o, EventArgs e)
	{
		Relay_Output8_120();
	}

	private void SubGraph_SaveLoadBool_Save_Out_134(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = e.boolean;
		local_EnemyDeadEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_134;
		Relay_Save_Out_134();
	}

	private void SubGraph_SaveLoadBool_Load_Out_134(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = e.boolean;
		local_EnemyDeadEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_134;
		Relay_Load_Out_134();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_134(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = e.boolean;
		local_EnemyDeadEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_134;
		Relay_Restart_Out_134();
	}

	private void SubGraph_SaveLoadBool_Save_Out_135(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_135 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_135;
		Relay_Save_Out_135();
	}

	private void SubGraph_SaveLoadBool_Load_Out_135(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_135 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_135;
		Relay_Load_Out_135();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_135(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_135 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_135;
		Relay_Restart_Out_135();
	}

	private void SubGraph_SaveLoadBool_Save_Out_136(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_136 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_136;
		Relay_Save_Out_136();
	}

	private void SubGraph_SaveLoadBool_Load_Out_136(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_136 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_136;
		Relay_Load_Out_136();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_136(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_136 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_136;
		Relay_Restart_Out_136();
	}

	private void SubGraph_SaveLoadBool_Save_Out_138(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_138;
		Relay_Save_Out_138();
	}

	private void SubGraph_SaveLoadBool_Load_Out_138(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_138;
		Relay_Load_Out_138();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_138(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_138;
		Relay_Restart_Out_138();
	}

	private void SubGraph_SaveLoadBool_Save_Out_153(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_153;
		Relay_Save_Out_153();
	}

	private void SubGraph_SaveLoadBool_Load_Out_153(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_153;
		Relay_Load_Out_153();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_153(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_153;
		Relay_Restart_Out_153();
	}

	private void SubGraph_SaveLoadInt_Save_Out_159(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_159 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_159;
		Relay_Save_Out_159();
	}

	private void SubGraph_SaveLoadInt_Load_Out_159(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_159 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_159;
		Relay_Load_Out_159();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_159(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_159 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_159;
		Relay_Restart_Out_159();
	}

	private void Relay_InitialSpawn_2()
	{
		int num = 0;
		Array array = targetTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_2.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_2, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_2, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_2 = owner_Connection_0;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_2.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_2, logic_uScript_SpawnTechsFromData_ownerNode_2, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_2);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_2.Out)
		{
			Relay_InitialSpawn_65();
		}
	}

	private void Relay_True_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.True(out logic_uScriptAct_SetBool_Target_4);
		local_TargetTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_False_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.False(out logic_uScriptAct_SetBool_Target_4);
		local_TargetTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_Save_Out_5()
	{
		Relay_Save_25();
	}

	private void Relay_Load_Out_5()
	{
		Relay_Load_25();
	}

	private void Relay_Restart_Out_5()
	{
		Relay_Set_False_25();
	}

	private void Relay_Save_5()
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = local_TargetTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_TargetTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Save(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
	}

	private void Relay_Load_5()
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = local_TargetTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_TargetTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Load(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
	}

	private void Relay_Set_True_5()
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = local_TargetTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_TargetTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
	}

	private void Relay_Set_False_5()
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = local_TargetTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_TargetTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
	}

	private void Relay_Save_Out_6()
	{
		Relay_Save_45();
	}

	private void Relay_Load_Out_6()
	{
		Relay_Load_45();
	}

	private void Relay_Restart_Out_6()
	{
		Relay_Restart_45();
	}

	private void Relay_Save_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Load_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Set_True_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Set_False_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_SaveEvent_9()
	{
		Relay_Save_5();
	}

	private void Relay_LoadEvent_9()
	{
		Relay_Load_5();
	}

	private void Relay_RestartEvent_9()
	{
		Relay_Set_False_5();
	}

	private void Relay_OnUpdate_10()
	{
		Relay_In_120();
	}

	private void Relay_OnSuspend_10()
	{
	}

	private void Relay_OnResume_10()
	{
	}

	private void Relay_In_13()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_13 = local_TargetTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_13 = distAtWhichFriendlyTechFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_13.In(logic_uScript_IsPlayerInRangeOfTech_tech_13, logic_uScript_IsPlayerInRangeOfTech_range_13, logic_uScript_IsPlayerInRangeOfTech_techs_13);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_13.InRange)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_16()
	{
		logic_uScriptCon_CompareBool_Bool_16 = local_MsgFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.In(logic_uScriptCon_CompareBool_Bool_16);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.False)
		{
			Relay_In_18();
		}
	}

	private void Relay_True_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.True(out logic_uScriptAct_SetBool_Target_17);
		local_MsgFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_True_24();
		}
	}

	private void Relay_False_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.False(out logic_uScriptAct_SetBool_Target_17);
		local_MsgFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_True_24();
		}
	}

	private void Relay_In_18()
	{
		int num = 0;
		Array array = msgTargetTechFound;
		if (logic_uScript_AddOnScreenMessage_locString_18.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_18, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_18, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_18 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_18 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.In(logic_uScript_AddOnScreenMessage_locString_18, logic_uScript_AddOnScreenMessage_msgPriority_18, logic_uScript_AddOnScreenMessage_holdMsg_18, logic_uScript_AddOnScreenMessage_tag_18, logic_uScript_AddOnScreenMessage_speaker_18, logic_uScript_AddOnScreenMessage_side_18);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.Shown)
		{
			Relay_True_17();
		}
	}

	private void Relay_In_21()
	{
		logic_uScriptCon_CompareBool_Bool_21 = local_TargetTechSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.In(logic_uScriptCon_CompareBool_Bool_21);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.False;
		if (num)
		{
			Relay_In_142();
		}
		if (flag)
		{
			Relay_InitialSpawn_2();
		}
	}

	private void Relay_In_22()
	{
		logic_uScriptCon_CompareBool_Bool_22 = local_TargetTechFound_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.In(logic_uScriptCon_CompareBool_Bool_22);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.False)
		{
			Relay_In_13();
		}
	}

	private void Relay_True_24()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.True(out logic_uScriptAct_SetBool_Target_24);
		local_TargetTechFound_System_Boolean = logic_uScriptAct_SetBool_Target_24;
	}

	private void Relay_False_24()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.False(out logic_uScriptAct_SetBool_Target_24);
		local_TargetTechFound_System_Boolean = logic_uScriptAct_SetBool_Target_24;
	}

	private void Relay_Save_Out_25()
	{
		Relay_Save_6();
	}

	private void Relay_Load_Out_25()
	{
		Relay_Load_6();
	}

	private void Relay_Restart_Out_25()
	{
		Relay_Set_False_6();
	}

	private void Relay_Save_25()
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = local_TargetTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_25 = local_TargetTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Save(ref logic_SubGraph_SaveLoadBool_boolean_25, logic_SubGraph_SaveLoadBool_boolAsVariable_25, logic_SubGraph_SaveLoadBool_uniqueID_25);
	}

	private void Relay_Load_25()
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = local_TargetTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_25 = local_TargetTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Load(ref logic_SubGraph_SaveLoadBool_boolean_25, logic_SubGraph_SaveLoadBool_boolAsVariable_25, logic_SubGraph_SaveLoadBool_uniqueID_25);
	}

	private void Relay_Set_True_25()
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = local_TargetTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_25 = local_TargetTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_25, logic_SubGraph_SaveLoadBool_boolAsVariable_25, logic_SubGraph_SaveLoadBool_uniqueID_25);
	}

	private void Relay_Set_False_25()
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = local_TargetTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_25 = local_TargetTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_25, logic_SubGraph_SaveLoadBool_boolAsVariable_25, logic_SubGraph_SaveLoadBool_uniqueID_25);
	}

	private void Relay_AtIndex_34()
	{
		int num = 0;
		Array array = local_targetTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_34.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_34, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_34, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_34.AtIndex(ref logic_uScript_AccessListTech_techList_34, logic_uScript_AccessListTech_index_34, out logic_uScript_AccessListTech_value_34);
		local_targetTechs_TankArray = logic_uScript_AccessListTech_techList_34;
		local_TargetTech_Tank = logic_uScript_AccessListTech_value_34;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_34.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_37 = owner_Connection_38;
		logic_uScript_MoveEncounterWithVisible_visibleObject_37 = local_TargetTech_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_37.In(logic_uScript_MoveEncounterWithVisible_ownerNode_37, logic_uScript_MoveEncounterWithVisible_visibleObject_37);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_37.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_39()
	{
		logic_uScript_SetEncounterTarget_owner_39 = owner_Connection_30;
		logic_uScript_SetEncounterTarget_visibleObject_39 = local_TargetTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_39.In(logic_uScript_SetEncounterTarget_owner_39, logic_uScript_SetEncounterTarget_visibleObject_39);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_39.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_40()
	{
		int num = 0;
		Array array = targetTechData;
		if (logic_uScript_GetAndCheckTechs_techData_40.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_40, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_40, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_40 = owner_Connection_33;
		int num2 = 0;
		Array array2 = local_targetTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_40.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_40, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_40, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_40 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.In(logic_uScript_GetAndCheckTechs_techData_40, logic_uScript_GetAndCheckTechs_ownerNode_40, ref logic_uScript_GetAndCheckTechs_techs_40);
		local_targetTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_40;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_40.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_34();
		}
		if (someAlive)
		{
			Relay_AtIndex_34();
		}
		if (allDead)
		{
			Relay_True_68();
		}
	}

	private void Relay_Succeed_44()
	{
		logic_uScript_FinishEncounter_owner_44 = owner_Connection_42;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_44.Succeed(logic_uScript_FinishEncounter_owner_44);
	}

	private void Relay_Fail_44()
	{
		logic_uScript_FinishEncounter_owner_44 = owner_Connection_42;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_44.Fail(logic_uScript_FinishEncounter_owner_44);
	}

	private void Relay_Save_Out_45()
	{
		Relay_Save_54();
	}

	private void Relay_Load_Out_45()
	{
		Relay_Load_54();
	}

	private void Relay_Restart_Out_45()
	{
		Relay_Set_False_54();
	}

	private void Relay_Save_45()
	{
		logic_SubGraph_SaveLoadInt_restartValue_45 = local_StartValue_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_45 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_45 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.Save(logic_SubGraph_SaveLoadInt_restartValue_45, ref logic_SubGraph_SaveLoadInt_integer_45, logic_SubGraph_SaveLoadInt_intAsVariable_45, logic_SubGraph_SaveLoadInt_uniqueID_45);
	}

	private void Relay_Load_45()
	{
		logic_SubGraph_SaveLoadInt_restartValue_45 = local_StartValue_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_45 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_45 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.Load(logic_SubGraph_SaveLoadInt_restartValue_45, ref logic_SubGraph_SaveLoadInt_integer_45, logic_SubGraph_SaveLoadInt_intAsVariable_45, logic_SubGraph_SaveLoadInt_uniqueID_45);
	}

	private void Relay_Restart_45()
	{
		logic_SubGraph_SaveLoadInt_restartValue_45 = local_StartValue_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_45 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_45 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_45.Restart(logic_SubGraph_SaveLoadInt_restartValue_45, ref logic_SubGraph_SaveLoadInt_integer_45, logic_SubGraph_SaveLoadInt_intAsVariable_45, logic_SubGraph_SaveLoadInt_uniqueID_45);
	}

	private void Relay_In_47()
	{
		int num = 0;
		Array array = msgMissionComplete;
		if (logic_uScript_AddOnScreenMessage_locString_47.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_47, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_47, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_47 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_47 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_47.In(logic_uScript_AddOnScreenMessage_locString_47, logic_uScript_AddOnScreenMessage_msgPriority_47, logic_uScript_AddOnScreenMessage_holdMsg_47, logic_uScript_AddOnScreenMessage_tag_47, logic_uScript_AddOnScreenMessage_speaker_47, logic_uScript_AddOnScreenMessage_side_47);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_47.Shown)
		{
			Relay_Succeed_44();
		}
	}

	private void Relay_In_49()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_49 = local_Msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_49.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_49, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_49);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_49.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_Save_Out_54()
	{
		Relay_Save_55();
	}

	private void Relay_Load_Out_54()
	{
		Relay_Load_55();
	}

	private void Relay_Restart_Out_54()
	{
		Relay_Set_False_55();
	}

	private void Relay_Save_54()
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = local_ShownMsgEnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_54 = local_ShownMsgEnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Save(ref logic_SubGraph_SaveLoadBool_boolean_54, logic_SubGraph_SaveLoadBool_boolAsVariable_54, logic_SubGraph_SaveLoadBool_uniqueID_54);
	}

	private void Relay_Load_54()
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = local_ShownMsgEnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_54 = local_ShownMsgEnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Load(ref logic_SubGraph_SaveLoadBool_boolean_54, logic_SubGraph_SaveLoadBool_boolAsVariable_54, logic_SubGraph_SaveLoadBool_uniqueID_54);
	}

	private void Relay_Set_True_54()
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = local_ShownMsgEnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_54 = local_ShownMsgEnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_54, logic_SubGraph_SaveLoadBool_boolAsVariable_54, logic_SubGraph_SaveLoadBool_uniqueID_54);
	}

	private void Relay_Set_False_54()
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = local_ShownMsgEnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_54 = local_ShownMsgEnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_54, logic_SubGraph_SaveLoadBool_boolAsVariable_54, logic_SubGraph_SaveLoadBool_uniqueID_54);
	}

	private void Relay_Save_Out_55()
	{
		Relay_Save_56();
	}

	private void Relay_Load_Out_55()
	{
		Relay_Load_56();
	}

	private void Relay_Restart_Out_55()
	{
		Relay_Set_True_56();
	}

	private void Relay_Save_55()
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_55 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Save(ref logic_SubGraph_SaveLoadBool_boolean_55, logic_SubGraph_SaveLoadBool_boolAsVariable_55, logic_SubGraph_SaveLoadBool_uniqueID_55);
	}

	private void Relay_Load_55()
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_55 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Load(ref logic_SubGraph_SaveLoadBool_boolean_55, logic_SubGraph_SaveLoadBool_boolAsVariable_55, logic_SubGraph_SaveLoadBool_uniqueID_55);
	}

	private void Relay_Set_True_55()
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_55 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_55, logic_SubGraph_SaveLoadBool_boolAsVariable_55, logic_SubGraph_SaveLoadBool_uniqueID_55);
	}

	private void Relay_Set_False_55()
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_55 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_55, logic_SubGraph_SaveLoadBool_boolAsVariable_55, logic_SubGraph_SaveLoadBool_uniqueID_55);
	}

	private void Relay_Save_Out_56()
	{
		Relay_Save_134();
	}

	private void Relay_Load_Out_56()
	{
		Relay_Load_134();
	}

	private void Relay_Restart_Out_56()
	{
		Relay_Set_False_134();
	}

	private void Relay_Save_56()
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_boolAsVariable_56 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Save(ref logic_SubGraph_SaveLoadBool_boolean_56, logic_SubGraph_SaveLoadBool_boolAsVariable_56, logic_SubGraph_SaveLoadBool_uniqueID_56);
	}

	private void Relay_Load_56()
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_boolAsVariable_56 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Load(ref logic_SubGraph_SaveLoadBool_boolean_56, logic_SubGraph_SaveLoadBool_boolAsVariable_56, logic_SubGraph_SaveLoadBool_uniqueID_56);
	}

	private void Relay_Set_True_56()
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_boolAsVariable_56 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_56, logic_SubGraph_SaveLoadBool_boolAsVariable_56, logic_SubGraph_SaveLoadBool_uniqueID_56);
	}

	private void Relay_Set_False_56()
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_boolAsVariable_56 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_56, logic_SubGraph_SaveLoadBool_boolAsVariable_56, logic_SubGraph_SaveLoadBool_uniqueID_56);
	}

	private void Relay_In_62()
	{
		logic_uScriptCon_CompareBool_Bool_62 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.In(logic_uScriptCon_CompareBool_Bool_62);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.False;
		if (num)
		{
			Relay_In_63();
		}
		if (flag)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_63()
	{
		logic_uScriptCon_CompareBool_Bool_63 = local_EnemyDeadEarly_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.In(logic_uScriptCon_CompareBool_Bool_63);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.False;
		if (num)
		{
			Relay_Succeed_44();
		}
		if (flag)
		{
			Relay_In_49();
		}
	}

	private void Relay_InitialSpawn_65()
	{
		int num = 0;
		Array nPCTech = NPCTech;
		if (logic_uScript_SpawnTechsFromData_spawnData_65.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_65, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_uScript_SpawnTechsFromData_spawnData_65, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_65 = owner_Connection_67;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_65.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_65, logic_uScript_SpawnTechsFromData_ownerNode_65, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_65);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_65.Out)
		{
			Relay_In_119();
		}
	}

	private void Relay_True_68()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_68.True(out logic_uScriptAct_SetBool_Target_68);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_68;
	}

	private void Relay_False_68()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_68.False(out logic_uScriptAct_SetBool_Target_68);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_68;
	}

	private void Relay_AtIndex_71()
	{
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_AccessListTech_techList_71.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_71, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_71, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_71.AtIndex(ref logic_uScript_AccessListTech_techList_71, logic_uScript_AccessListTech_index_71, out logic_uScript_AccessListTech_value_71);
		local_NPCTanks_TankArray = logic_uScript_AccessListTech_techList_71;
		local_NPCTank_Tank = logic_uScript_AccessListTech_value_71;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_71.Out)
		{
			Relay_In_99();
		}
	}

	private void Relay_In_72()
	{
		logic_uScriptCon_CompareBool_Bool_72 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.In(logic_uScriptCon_CompareBool_Bool_72);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.False;
		if (num)
		{
			Relay_In_40();
		}
		if (flag)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_74()
	{
		int num = 0;
		Array array = msgNPCGreetingEnemyDead;
		if (logic_uScript_AddOnScreenMessage_locString_74.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_74, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_74, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_74 = local_87_System_String;
		logic_uScript_AddOnScreenMessage_speaker_74 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_74 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_74.In(logic_uScript_AddOnScreenMessage_locString_74, logic_uScript_AddOnScreenMessage_msgPriority_74, logic_uScript_AddOnScreenMessage_holdMsg_74, logic_uScript_AddOnScreenMessage_tag_74, logic_uScript_AddOnScreenMessage_speaker_74, logic_uScript_AddOnScreenMessage_side_74);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_74.Shown)
		{
			Relay_True_96();
		}
	}

	private void Relay_In_75()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_75 = local_70_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_75.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_75, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_75);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_75.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_True_77()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_77.True(out logic_uScriptAct_SetBool_Target_77);
		local_TargetTechFound_System_Boolean = logic_uScriptAct_SetBool_Target_77;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_77.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_False_77()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_77.False(out logic_uScriptAct_SetBool_Target_77);
		local_TargetTechFound_System_Boolean = logic_uScriptAct_SetBool_Target_77;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_77.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_83()
	{
		int num = 0;
		Array array = msgNPCGreeting;
		if (logic_uScript_AddOnScreenMessage_locString_83.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_83, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_83, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_83 = local_106_System_String;
		logic_uScript_AddOnScreenMessage_speaker_83 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_83 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_83.In(logic_uScript_AddOnScreenMessage_locString_83, logic_uScript_AddOnScreenMessage_msgPriority_83, logic_uScript_AddOnScreenMessage_holdMsg_83, logic_uScript_AddOnScreenMessage_tag_83, logic_uScript_AddOnScreenMessage_speaker_83, logic_uScript_AddOnScreenMessage_side_83);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_83.Shown)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_84()
	{
		logic_uScript_FlyTechUpAndAway_tech_84 = local_NPCTank_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_84 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_84 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_84.In(logic_uScript_FlyTechUpAndAway_tech_84, logic_uScript_FlyTechUpAndAway_maxLifetime_84, logic_uScript_FlyTechUpAndAway_targetHeight_84, logic_uScript_FlyTechUpAndAway_aiTree_84, logic_uScript_FlyTechUpAndAway_removalParticles_84);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_84.Out)
		{
			Relay_True_92();
		}
	}

	private void Relay_Out_88()
	{
	}

	private void Relay_In_88()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_88 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_88, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_88);
	}

	private void Relay_True_92()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.True(out logic_uScriptAct_SetBool_Target_92);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_92;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_92.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_False_92()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.False(out logic_uScriptAct_SetBool_Target_92);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_92;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_92.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_94()
	{
		logic_uScript_InRangeOfTech_tank_94 = local_NPCTank_Tank;
		logic_uScript_InRangeOfTech_range_94 = DistInRangeOfNPC;
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_94.In(logic_uScript_InRangeOfTech_tank_94, logic_uScript_InRangeOfTech_range_94);
		bool inRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_94.InRange;
		bool outOfRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_94.OutOfRange;
		if (inRange)
		{
			Relay_True_102();
		}
		if (outOfRange)
		{
			Relay_In_111();
		}
	}

	private void Relay_True_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.True(out logic_uScriptAct_SetBool_Target_96);
		local_EnemyDeadEarly_System_Boolean = logic_uScriptAct_SetBool_Target_96;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_96.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_False_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.False(out logic_uScriptAct_SetBool_Target_96);
		local_EnemyDeadEarly_System_Boolean = logic_uScriptAct_SetBool_Target_96;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_96.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_99()
	{
		logic_uScript_SetTankInvulnerable_tank_99 = local_NPCTank_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_99.In(logic_uScript_SetTankInvulnerable_invulnerable_99, logic_uScript_SetTankInvulnerable_tank_99);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_99.Out)
		{
			Relay_In_110();
		}
	}

	private void Relay_True_102()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.True(out logic_uScriptAct_SetBool_Target_102);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_102;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_102.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_False_102()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.False(out logic_uScriptAct_SetBool_Target_102);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_102;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_102.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_103()
	{
		int num = 0;
		Array array = msgNPCGreetingInturrupt;
		if (logic_uScript_AddOnScreenMessage_locString_103.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_103, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_103, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_103 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_103 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103.In(logic_uScript_AddOnScreenMessage_locString_103, logic_uScript_AddOnScreenMessage_msgPriority_103, logic_uScript_AddOnScreenMessage_holdMsg_103, logic_uScript_AddOnScreenMessage_tag_103, logic_uScript_AddOnScreenMessage_speaker_103, logic_uScript_AddOnScreenMessage_side_103);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103.Shown)
		{
			Relay_True_77();
		}
	}

	private void Relay_In_107()
	{
		int num = 0;
		Array nPCTech = NPCTech;
		if (logic_uScript_GetAndCheckTechs_techData_107.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_107, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_uScript_GetAndCheckTechs_techData_107, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_107 = owner_Connection_108;
		int num2 = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_107.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_107, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_107, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_107 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_107.In(logic_uScript_GetAndCheckTechs_techData_107, logic_uScript_GetAndCheckTechs_ownerNode_107, ref logic_uScript_GetAndCheckTechs_techs_107);
		local_NPCTanks_TankArray = logic_uScript_GetAndCheckTechs_techs_107;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_107.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_107.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_71();
		}
		if (someAlive)
		{
			Relay_AtIndex_71();
		}
	}

	private void Relay_In_109()
	{
		int num = 0;
		Array array = targetTechData;
		if (logic_uScript_GetAndCheckTechs_techData_109.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_109, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_109, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_109 = owner_Connection_81;
		int num2 = 0;
		Array array2 = local_targetTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_109.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_109, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_109, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_109 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_109.In(logic_uScript_GetAndCheckTechs_techData_109, logic_uScript_GetAndCheckTechs_ownerNode_109, ref logic_uScript_GetAndCheckTechs_techs_109);
		local_targetTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_109;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_109.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_109.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_109.AllDead;
		if (allAlive)
		{
			Relay_In_83();
		}
		if (someAlive)
		{
			Relay_In_83();
		}
		if (allDead)
		{
			Relay_In_74();
		}
	}

	private void Relay_In_110()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_110 = owner_Connection_113;
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_110.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_110, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_110, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_110 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_110.In(logic_uScript_SetOneTechAsEncounterTarget_owner_110, logic_uScript_SetOneTechAsEncounterTarget_techs_110);
		local_NPCTank_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_110;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_110.Out)
		{
			Relay_In_150();
		}
	}

	private void Relay_In_111()
	{
		logic_uScriptCon_CompareBool_Bool_111 = local_NPCSeen_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.In(logic_uScriptCon_CompareBool_Bool_111);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.True)
		{
			Relay_True_148();
		}
	}

	private void Relay_In_119()
	{
		logic_uScript_RemoveScenery_ownerNode_119 = owner_Connection_118;
		logic_uScript_RemoveScenery_positionName_119 = clearSceneryPosTarget;
		logic_uScript_RemoveScenery_radius_119 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_119.In(logic_uScript_RemoveScenery_ownerNode_119, logic_uScript_RemoveScenery_positionName_119, logic_uScript_RemoveScenery_radius_119, logic_uScript_RemoveScenery_preventChunksSpawning_119);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_119.Out)
		{
			Relay_In_157();
		}
	}

	private void Relay_Output1_120()
	{
		Relay_In_62();
	}

	private void Relay_Output2_120()
	{
		Relay_In_62();
	}

	private void Relay_Output3_120()
	{
	}

	private void Relay_Output4_120()
	{
	}

	private void Relay_Output5_120()
	{
	}

	private void Relay_Output6_120()
	{
	}

	private void Relay_Output7_120()
	{
	}

	private void Relay_Output8_120()
	{
	}

	private void Relay_In_120()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_120 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_120.In(logic_uScriptCon_ManualSwitch_CurrentOutput_120);
	}

	private void Relay_TickWave_122()
	{
		logic_uScript_SpawnTechWaveFromData_ownerNode_122 = owner_Connection_128;
		logic_uScript_SpawnTechWaveFromData_spawnData_122 = enemyGroupData;
		logic_uScript_SpawnTechWaveFromData_waveSize_122 = EnemyGroupSize;
		logic_uScript_SpawnTechWaveFromData_delayBetweenSpawns_122 = DelayBetweenEnemyGroupSpaws;
		logic_uScript_SpawnTechWaveFromData_numSpawnedSoFar_122 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_uScript_SpawnTechWaveFromData_allowRespawn_122 = AllowEnemyGroupToRespawn;
		logic_uScript_SpawnTechWaveFromData_respawnGroupSize_122 = ReinforcementSubGroupSize;
		logic_uScript_SpawnTechWaveFromData_delayBetweenRespawnGroups_122 = DelayBetweenRespawnArivals;
		logic_uScript_SpawnTechWaveFromData_Return_122 = logic_uScript_SpawnTechWaveFromData_uScript_SpawnTechWaveFromData_122.TickWave(logic_uScript_SpawnTechWaveFromData_ownerNode_122, logic_uScript_SpawnTechWaveFromData_spawnData_122, logic_uScript_SpawnTechWaveFromData_waveSize_122, logic_uScript_SpawnTechWaveFromData_delayBetweenSpawns_122, logic_uScript_SpawnTechWaveFromData_numSpawnedSoFar_122, logic_uScript_SpawnTechWaveFromData_allowRespawn_122, logic_uScript_SpawnTechWaveFromData_respawnGroupSize_122, logic_uScript_SpawnTechWaveFromData_delayBetweenRespawnGroups_122);
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_uScript_SpawnTechWaveFromData_Return_122;
		if (logic_uScript_SpawnTechWaveFromData_uScript_SpawnTechWaveFromData_122.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_Save_Out_134()
	{
		Relay_Save_135();
	}

	private void Relay_Load_Out_134()
	{
		Relay_Load_135();
	}

	private void Relay_Restart_Out_134()
	{
		Relay_Set_False_135();
	}

	private void Relay_Save_134()
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_134 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Save(ref logic_SubGraph_SaveLoadBool_boolean_134, logic_SubGraph_SaveLoadBool_boolAsVariable_134, logic_SubGraph_SaveLoadBool_uniqueID_134);
	}

	private void Relay_Load_134()
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_134 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Load(ref logic_SubGraph_SaveLoadBool_boolean_134, logic_SubGraph_SaveLoadBool_boolAsVariable_134, logic_SubGraph_SaveLoadBool_uniqueID_134);
	}

	private void Relay_Set_True_134()
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_134 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_134, logic_SubGraph_SaveLoadBool_boolAsVariable_134, logic_SubGraph_SaveLoadBool_uniqueID_134);
	}

	private void Relay_Set_False_134()
	{
		logic_SubGraph_SaveLoadBool_boolean_134 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_134 = local_EnemyDeadEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_134.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_134, logic_SubGraph_SaveLoadBool_boolAsVariable_134, logic_SubGraph_SaveLoadBool_uniqueID_134);
	}

	private void Relay_Save_Out_135()
	{
		Relay_Save_136();
	}

	private void Relay_Load_Out_135()
	{
		Relay_Load_136();
	}

	private void Relay_Restart_Out_135()
	{
		Relay_Set_False_136();
	}

	private void Relay_Save_135()
	{
		logic_SubGraph_SaveLoadBool_boolean_135 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_135 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Save(ref logic_SubGraph_SaveLoadBool_boolean_135, logic_SubGraph_SaveLoadBool_boolAsVariable_135, logic_SubGraph_SaveLoadBool_uniqueID_135);
	}

	private void Relay_Load_135()
	{
		logic_SubGraph_SaveLoadBool_boolean_135 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_135 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Load(ref logic_SubGraph_SaveLoadBool_boolean_135, logic_SubGraph_SaveLoadBool_boolAsVariable_135, logic_SubGraph_SaveLoadBool_uniqueID_135);
	}

	private void Relay_Set_True_135()
	{
		logic_SubGraph_SaveLoadBool_boolean_135 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_135 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_135, logic_SubGraph_SaveLoadBool_boolAsVariable_135, logic_SubGraph_SaveLoadBool_uniqueID_135);
	}

	private void Relay_Set_False_135()
	{
		logic_SubGraph_SaveLoadBool_boolean_135 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_135 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_135.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_135, logic_SubGraph_SaveLoadBool_boolAsVariable_135, logic_SubGraph_SaveLoadBool_uniqueID_135);
	}

	private void Relay_Save_Out_136()
	{
		Relay_Save_138();
	}

	private void Relay_Load_Out_136()
	{
		Relay_Load_138();
	}

	private void Relay_Restart_Out_136()
	{
		Relay_Set_False_138();
	}

	private void Relay_Save_136()
	{
		logic_SubGraph_SaveLoadBool_boolean_136 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_136 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Save(ref logic_SubGraph_SaveLoadBool_boolean_136, logic_SubGraph_SaveLoadBool_boolAsVariable_136, logic_SubGraph_SaveLoadBool_uniqueID_136);
	}

	private void Relay_Load_136()
	{
		logic_SubGraph_SaveLoadBool_boolean_136 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_136 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Load(ref logic_SubGraph_SaveLoadBool_boolean_136, logic_SubGraph_SaveLoadBool_boolAsVariable_136, logic_SubGraph_SaveLoadBool_uniqueID_136);
	}

	private void Relay_Set_True_136()
	{
		logic_SubGraph_SaveLoadBool_boolean_136 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_136 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_136, logic_SubGraph_SaveLoadBool_boolAsVariable_136, logic_SubGraph_SaveLoadBool_uniqueID_136);
	}

	private void Relay_Set_False_136()
	{
		logic_SubGraph_SaveLoadBool_boolean_136 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_136 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_136.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_136, logic_SubGraph_SaveLoadBool_boolAsVariable_136, logic_SubGraph_SaveLoadBool_uniqueID_136);
	}

	private void Relay_Save_Out_138()
	{
		Relay_Save_153();
	}

	private void Relay_Load_Out_138()
	{
		Relay_Load_153();
	}

	private void Relay_Restart_Out_138()
	{
		Relay_Set_False_153();
	}

	private void Relay_Save_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Save(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Load_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Load(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Set_True_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Set_False_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_In_142()
	{
		int num = 0;
		Array array = targetTechData;
		if (logic_uScript_GetAndCheckTechs_techData_142.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_142, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_142, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_142 = owner_Connection_144;
		int num2 = 0;
		Array array2 = local_targetTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_142.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_142, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_142, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_142 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142.In(logic_uScript_GetAndCheckTechs_techData_142, logic_uScript_GetAndCheckTechs_ownerNode_142, ref logic_uScript_GetAndCheckTechs_techs_142);
		local_targetTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_142;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142.AllDead;
		if (allAlive)
		{
			Relay_TickWave_122();
		}
		if (someAlive)
		{
			Relay_TickWave_122();
		}
		if (allDead)
		{
			Relay_False_147();
		}
	}

	private void Relay_True_147()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_147.True(out logic_uScriptAct_SetBool_Target_147);
		AllowEnemyGroupToRespawn = logic_uScriptAct_SetBool_Target_147;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_147.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_False_147()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_147.False(out logic_uScriptAct_SetBool_Target_147);
		AllowEnemyGroupToRespawn = logic_uScriptAct_SetBool_Target_147;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_147.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_True_148()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.True(out logic_uScriptAct_SetBool_Target_148);
		local_NPCIgnored_System_Boolean = logic_uScriptAct_SetBool_Target_148;
	}

	private void Relay_False_148()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.False(out logic_uScriptAct_SetBool_Target_148);
		local_NPCIgnored_System_Boolean = logic_uScriptAct_SetBool_Target_148;
	}

	private void Relay_In_150()
	{
		logic_uScriptCon_CompareBool_Bool_150 = local_NPCIgnored_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_150.In(logic_uScriptCon_CompareBool_Bool_150);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_150.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_150.False;
		if (num)
		{
			Relay_In_75();
		}
		if (flag)
		{
			Relay_In_94();
		}
	}

	private void Relay_Save_Out_153()
	{
		Relay_Save_159();
	}

	private void Relay_Load_Out_153()
	{
		Relay_Load_159();
	}

	private void Relay_Restart_Out_153()
	{
		Relay_Restart_159();
	}

	private void Relay_Save_153()
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_153 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Save(ref logic_SubGraph_SaveLoadBool_boolean_153, logic_SubGraph_SaveLoadBool_boolAsVariable_153, logic_SubGraph_SaveLoadBool_uniqueID_153);
	}

	private void Relay_Load_153()
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_153 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Load(ref logic_SubGraph_SaveLoadBool_boolean_153, logic_SubGraph_SaveLoadBool_boolAsVariable_153, logic_SubGraph_SaveLoadBool_uniqueID_153);
	}

	private void Relay_Set_True_153()
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_153 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_153, logic_SubGraph_SaveLoadBool_boolAsVariable_153, logic_SubGraph_SaveLoadBool_uniqueID_153);
	}

	private void Relay_Set_False_153()
	{
		logic_SubGraph_SaveLoadBool_boolean_153 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_153 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_153.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_153, logic_SubGraph_SaveLoadBool_boolAsVariable_153, logic_SubGraph_SaveLoadBool_uniqueID_153);
	}

	private void Relay_In_157()
	{
		logic_uScript_RemoveScenery_ownerNode_157 = owner_Connection_155;
		logic_uScript_RemoveScenery_positionName_157 = clearSceneryPosNPC;
		logic_uScript_RemoveScenery_radius_157 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_157.In(logic_uScript_RemoveScenery_ownerNode_157, logic_uScript_RemoveScenery_positionName_157, logic_uScript_RemoveScenery_radius_157, logic_uScript_RemoveScenery_preventChunksSpawning_157);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_157.Out)
		{
			Relay_True_4();
		}
	}

	private void Relay_Save_Out_159()
	{
	}

	private void Relay_Load_Out_159()
	{
	}

	private void Relay_Restart_Out_159()
	{
	}

	private void Relay_Save_159()
	{
		logic_SubGraph_SaveLoadInt_integer_159 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_159 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.Save(logic_SubGraph_SaveLoadInt_restartValue_159, ref logic_SubGraph_SaveLoadInt_integer_159, logic_SubGraph_SaveLoadInt_intAsVariable_159, logic_SubGraph_SaveLoadInt_uniqueID_159);
	}

	private void Relay_Load_159()
	{
		logic_SubGraph_SaveLoadInt_integer_159 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_159 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.Load(logic_SubGraph_SaveLoadInt_restartValue_159, ref logic_SubGraph_SaveLoadInt_integer_159, logic_SubGraph_SaveLoadInt_intAsVariable_159, logic_SubGraph_SaveLoadInt_uniqueID_159);
	}

	private void Relay_Restart_159()
	{
		logic_SubGraph_SaveLoadInt_integer_159 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_159 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_159.Restart(logic_SubGraph_SaveLoadInt_restartValue_159, ref logic_SubGraph_SaveLoadInt_integer_159, logic_SubGraph_SaveLoadInt_intAsVariable_159, logic_SubGraph_SaveLoadInt_uniqueID_159);
	}
}
