using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_Stats_KillEnemies_HE : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius;

	public float DistInRangeOfNPC = 50f;

	private string local_24_System_String = "msgMeeting";

	private string local_29_System_String = "msgMeeting";

	private int local_CurrentAmount_System_Int32;

	private int local_CurrentKillTotal_System_Int32;

	private int local_InitialAmount_System_Int32;

	private bool local_InitialisedCount_System_Boolean;

	private Tank local_NearestEnemyTech_Tank;

	private Vector3 local_NearestEnemyTechPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_NPCIgnored_System_Boolean;

	private bool local_NPCMet_System_Boolean;

	private bool local_NPCSeen_System_Boolean;

	private bool local_NPCSpawned_System_Boolean;

	private Tank local_NPCTank_Tank;

	private Tank[] local_NPCTanks_TankArray = new Tank[0];

	private int local_Stage_System_Int32 = 1;

	private int local_TargetCount_System_Int32;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgComplete = new LocalisedString[0];

	public LocalisedString[] msgNPCGreeting = new LocalisedString[0];

	public LocalisedString[] msgNPCGreetingInturrupt = new LocalisedString[0];

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCTech = new SpawnTechData[0];

	public int ObjectiveToCount = 2;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_23;

	private GameObject owner_Connection_33;

	private GameObject owner_Connection_35;

	private GameObject owner_Connection_56;

	private GameObject owner_Connection_77;

	private GameObject owner_Connection_87;

	private GameObject owner_Connection_104;

	private GameObject owner_Connection_117;

	private GameObject owner_Connection_120;

	private GameObject owner_Connection_123;

	private GameObject owner_Connection_128;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_5 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_5;

	private bool logic_uScript_FinishEncounter_Out_5 = true;

	private uScript_GetNearestTech logic_uScript_GetNearestTech_uScript_GetNearestTech_7 = new uScript_GetNearestTech();

	private int logic_uScript_GetNearestTech_team_7 = 1;

	private bool logic_uScript_GetNearestTech_recheck_7 = true;

	private Tank logic_uScript_GetNearestTech_Return_7;

	private bool logic_uScript_GetNearestTech_Found_7 = true;

	private bool logic_uScript_GetNearestTech_NotFound_7 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_9 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_9;

	private object logic_uScript_SetEncounterTarget_visibleObject_9 = "";

	private bool logic_uScript_SetEncounterTarget_Out_9 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_10 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_10;

	private bool logic_uScript_ClearEncounterTarget_Out_10 = true;

	private uScript_InRangeOfTech logic_uScript_InRangeOfTech_uScript_InRangeOfTech_25 = new uScript_InRangeOfTech();

	private Tank logic_uScript_InRangeOfTech_tank_25;

	private float logic_uScript_InRangeOfTech_range_25;

	private bool logic_uScript_InRangeOfTech_Out_25 = true;

	private bool logic_uScript_InRangeOfTech_InRange_25 = true;

	private bool logic_uScript_InRangeOfTech_OutOfRange_25 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_26 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_26;

	private bool logic_uScriptAct_SetBool_Out_26 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_26 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_26 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_28 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_28 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_28;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_28 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_28;

	private bool logic_uScript_SpawnTechsFromData_Out_28 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_34 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_34 = new Tank[0];

	private int logic_uScript_AccessListTech_index_34;

	private Tank logic_uScript_AccessListTech_value_34;

	private bool logic_uScript_AccessListTech_Out_34 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_37 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_37;

	private string logic_uScript_RemoveScenery_positionName_37 = "";

	private float logic_uScript_RemoveScenery_radius_37;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_37 = true;

	private bool logic_uScript_RemoveScenery_Out_37 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_38;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_39;

	private bool logic_uScriptCon_CompareBool_True_39 = true;

	private bool logic_uScriptCon_CompareBool_False_39 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_41 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_41 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_41 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_41;

	private string logic_uScript_AddOnScreenMessage_tag_41 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_41 = ManOnScreenMessages.Speaker.HEGeneric;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_41;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_41;

	private bool logic_uScript_AddOnScreenMessage_Out_41 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_41 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_43 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_43 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_43;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_43 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_45 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_45;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_45 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_45 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_45;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_45;

	private bool logic_uScript_FlyTechUpAndAway_Out_45 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_46;

	private bool logic_uScriptCon_CompareBool_True_46 = true;

	private bool logic_uScriptCon_CompareBool_False_46 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_47 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_47 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_47;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_47 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_47;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_47 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_47 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_47 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_47 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_50 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_50;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_50 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_50;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_50 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_51 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_51 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_51 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_51;

	private string logic_uScript_AddOnScreenMessage_tag_51 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_51 = ManOnScreenMessages.Speaker.HEGeneric;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_51;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_51;

	private bool logic_uScript_AddOnScreenMessage_Out_51 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_51 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_52 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_52;

	private bool logic_uScriptAct_SetBool_Out_52 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_52 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_52 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_54 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_54 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_54;

	private bool logic_uScript_SetTankInvulnerable_Out_54 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_57;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_57 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_57 = "NPCMet";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_58;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_58 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_58 = "NPCSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_60;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_60 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_60 = "NPCSeen";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_62;

	private bool logic_uScriptCon_CompareBool_True_62 = true;

	private bool logic_uScriptCon_CompareBool_False_62 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_65;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_65;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_69 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_69;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_72 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_72;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_72 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_72 = "Stage";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_74 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_74;

	private bool logic_uScriptAct_SetBool_Out_74 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_74 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_74 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_75;

	private int logic_SubGraph_SaveLoadInt_integer_75;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_75 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_75 = "InitialAmount";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_78;

	private int logic_SubGraph_SaveLoadInt_integer_78;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_78 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_78 = "CurrentAmount";

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_81 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_81;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_81 = 2;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_81;

	private bool logic_uScript_SetQuestObjectiveCount_Out_81 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_85 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_85 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_85 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_85;

	private string logic_uScript_AddOnScreenMessage_tag_85 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_85;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_85;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_85;

	private bool logic_uScript_AddOnScreenMessage_Out_85 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_85 = true;

	private uScriptAct_SubtractInt logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_88 = new uScriptAct_SubtractInt();

	private int logic_uScriptAct_SubtractInt_A_88;

	private int logic_uScriptAct_SubtractInt_B_88;

	private int logic_uScriptAct_SubtractInt_IntResult_88;

	private float logic_uScriptAct_SubtractInt_FloatResult_88;

	private bool logic_uScriptAct_SubtractInt_Out_88 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_89 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_89;

	private int logic_uScriptCon_CompareInt_B_89;

	private bool logic_uScriptCon_CompareInt_GreaterThan_89 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_89 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_89 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_89 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_89 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_89 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_90 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_90;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_90;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_90;

	private bool logic_uScript_SetQuestObjectiveCount_Out_90 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_93;

	private bool logic_uScriptCon_CompareBool_True_93 = true;

	private bool logic_uScriptCon_CompareBool_False_93 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_95 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_95;

	private bool logic_uScriptAct_SetBool_Out_95 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_95 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_95 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_97;

	private bool logic_uScriptCon_CompareBool_True_97 = true;

	private bool logic_uScriptCon_CompareBool_False_97 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_100;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_100 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_100 = "NPCIgnored";

	private uScript_GetQuestObjectiveTargetCount logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_101 = new uScript_GetQuestObjectiveTargetCount();

	private GameObject logic_uScript_GetQuestObjectiveTargetCount_owner_101;

	private int logic_uScript_GetQuestObjectiveTargetCount_objectiveId_101;

	private int logic_uScript_GetQuestObjectiveTargetCount_Return_101;

	private bool logic_uScript_GetQuestObjectiveTargetCount_Out_101 = true;

	private uScript_GetNumEnemyTechsDestroyed logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_105 = new uScript_GetNumEnemyTechsDestroyed();

	private FactionSubTypes logic_uScript_GetNumEnemyTechsDestroyed_faction_105;

	private int logic_uScript_GetNumEnemyTechsDestroyed_Return_105;

	private bool logic_uScript_GetNumEnemyTechsDestroyed_Out_105 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_107 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_107;

	private bool logic_uScriptAct_SetBool_Out_107 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_107 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_107 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_109;

	private bool logic_uScriptCon_CompareBool_True_109 = true;

	private bool logic_uScriptCon_CompareBool_False_109 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_112;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_112 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_112 = "InitialisedCount";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_114;

	private int logic_SubGraph_SaveLoadInt_integer_114;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_114 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_114 = "TargetCount";

	private uScript_SetEncounterPosition logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_115 = new uScript_SetEncounterPosition();

	private GameObject logic_uScript_SetEncounterPosition_ownerNode_115;

	private Vector3 logic_uScript_SetEncounterPosition_position_115;

	private bool logic_uScript_SetEncounterPosition_Out_115 = true;

	private uScript_GetPositionOfTech logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_116 = new uScript_GetPositionOfTech();

	private Tank logic_uScript_GetPositionOfTech_tech_116;

	private Vector3 logic_uScript_GetPositionOfTech_Return_116;

	private bool logic_uScript_GetPositionOfTech_Out_116 = true;

	private bool logic_uScript_GetPositionOfTech_TechValid_116 = true;

	private bool logic_uScript_GetPositionOfTech_TechNull_116 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_119 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_119;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_119 = true;

	private uScript_GetQuestObjectiveTargetCount logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_122 = new uScript_GetQuestObjectiveTargetCount();

	private GameObject logic_uScript_GetQuestObjectiveTargetCount_owner_122;

	private int logic_uScript_GetQuestObjectiveTargetCount_objectiveId_122;

	private int logic_uScript_GetQuestObjectiveTargetCount_Return_122;

	private bool logic_uScript_GetQuestObjectiveTargetCount_Out_122 = true;

	private uScript_GetNumEnemyTechsDestroyed logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_124 = new uScript_GetNumEnemyTechsDestroyed();

	private FactionSubTypes logic_uScript_GetNumEnemyTechsDestroyed_faction_124;

	private int logic_uScript_GetNumEnemyTechsDestroyed_Return_124;

	private bool logic_uScript_GetNumEnemyTechsDestroyed_Out_124 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_129 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_129;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_129;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_129;

	private bool logic_uScript_SetQuestObjectiveCount_Out_129 = true;

	private FactionSubTypes event_UnityEngine_GameObject_Faction_6;

	private int event_UnityEngine_GameObject_FactionTotal_6;

	private int event_UnityEngine_GameObject_NumEnemyTechsDestroyed_6;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
		}
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
			if (null != owner_Connection_2)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_2.AddComponent<uScript_EncounterUpdate>();
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
				uScript_EnemyTechDestroyedEvent uScript_EnemyTechDestroyedEvent2 = owner_Connection_3.GetComponent<uScript_EnemyTechDestroyedEvent>();
				if (null == uScript_EnemyTechDestroyedEvent2)
				{
					uScript_EnemyTechDestroyedEvent2 = owner_Connection_3.AddComponent<uScript_EnemyTechDestroyedEvent>();
				}
				if (null != uScript_EnemyTechDestroyedEvent2)
				{
					uScript_EnemyTechDestroyedEvent2.EnemyTechDestroyedEvent += Instance_EnemyTechDestroyedEvent_6;
				}
			}
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
		}
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (null == owner_Connection_23 || !m_RegisteredForEvents)
		{
			owner_Connection_23 = parentGameObject;
		}
		if (null == owner_Connection_33 || !m_RegisteredForEvents)
		{
			owner_Connection_33 = parentGameObject;
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
		if (null == owner_Connection_56 || !m_RegisteredForEvents)
		{
			owner_Connection_56 = parentGameObject;
			if (null != owner_Connection_56)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_56.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_56.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_61;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_61;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_61;
				}
			}
		}
		if (null == owner_Connection_77 || !m_RegisteredForEvents)
		{
			owner_Connection_77 = parentGameObject;
		}
		if (null == owner_Connection_87 || !m_RegisteredForEvents)
		{
			owner_Connection_87 = parentGameObject;
		}
		if (null == owner_Connection_104 || !m_RegisteredForEvents)
		{
			owner_Connection_104 = parentGameObject;
		}
		if (null == owner_Connection_117 || !m_RegisteredForEvents)
		{
			owner_Connection_117 = parentGameObject;
		}
		if (null == owner_Connection_120 || !m_RegisteredForEvents)
		{
			owner_Connection_120 = parentGameObject;
		}
		if (null == owner_Connection_123 || !m_RegisteredForEvents)
		{
			owner_Connection_123 = parentGameObject;
		}
		if (null == owner_Connection_128 || !m_RegisteredForEvents)
		{
			owner_Connection_128 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_2)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_2.AddComponent<uScript_EncounterUpdate>();
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
			uScript_EnemyTechDestroyedEvent uScript_EnemyTechDestroyedEvent2 = owner_Connection_3.GetComponent<uScript_EnemyTechDestroyedEvent>();
			if (null == uScript_EnemyTechDestroyedEvent2)
			{
				uScript_EnemyTechDestroyedEvent2 = owner_Connection_3.AddComponent<uScript_EnemyTechDestroyedEvent>();
			}
			if (null != uScript_EnemyTechDestroyedEvent2)
			{
				uScript_EnemyTechDestroyedEvent2.EnemyTechDestroyedEvent += Instance_EnemyTechDestroyedEvent_6;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_56)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_56.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_56.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_61;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_61;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_61;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_2)
		{
			uScript_EncounterUpdate component = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_0;
				component.OnSuspend -= Instance_OnSuspend_0;
				component.OnResume -= Instance_OnResume_0;
			}
		}
		if (null != owner_Connection_3)
		{
			uScript_EnemyTechDestroyedEvent component2 = owner_Connection_3.GetComponent<uScript_EnemyTechDestroyedEvent>();
			if (null != component2)
			{
				component2.EnemyTechDestroyedEvent -= Instance_EnemyTechDestroyedEvent_6;
			}
		}
		if (null != owner_Connection_56)
		{
			uScript_SaveLoad component3 = owner_Connection_56.GetComponent<uScript_SaveLoad>();
			if (null != component3)
			{
				component3.SaveEvent -= Instance_SaveEvent_61;
				component3.LoadEvent -= Instance_LoadEvent_61;
				component3.RestartEvent -= Instance_RestartEvent_61;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_5.SetParent(g);
		logic_uScript_GetNearestTech_uScript_GetNearestTech_7.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_9.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_10.SetParent(g);
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_25.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_28.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_34.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_37.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_41.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_43.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_45.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_47.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_50.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_51.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_52.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_54.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_69.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_81.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_85.SetParent(g);
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_88.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_89.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_90.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_95.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.SetParent(g);
		logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_101.SetParent(g);
		logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_105.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_107.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.SetParent(g);
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_115.SetParent(g);
		logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_116.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_119.SetParent(g);
		logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_122.SetParent(g);
		logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_124.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_129.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_2 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_23 = parentGameObject;
		owner_Connection_33 = parentGameObject;
		owner_Connection_35 = parentGameObject;
		owner_Connection_56 = parentGameObject;
		owner_Connection_77 = parentGameObject;
		owner_Connection_87 = parentGameObject;
		owner_Connection_104 = parentGameObject;
		owner_Connection_117 = parentGameObject;
		owner_Connection_120 = parentGameObject;
		owner_Connection_123 = parentGameObject;
		owner_Connection_128 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_69.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output1 += uScriptCon_ManualSwitch_Output1_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output2 += uScriptCon_ManualSwitch_Output2_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output3 += uScriptCon_ManualSwitch_Output3_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output4 += uScriptCon_ManualSwitch_Output4_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output5 += uScriptCon_ManualSwitch_Output5_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output6 += uScriptCon_ManualSwitch_Output6_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output7 += uScriptCon_ManualSwitch_Output7_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output8 += uScriptCon_ManualSwitch_Output8_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Save_Out += SubGraph_SaveLoadBool_Save_Out_57;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Load_Out += SubGraph_SaveLoadBool_Load_Out_57;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_57;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save_Out += SubGraph_SaveLoadBool_Save_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load_Out += SubGraph_SaveLoadBool_Load_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Save_Out += SubGraph_SaveLoadBool_Save_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Load_Out += SubGraph_SaveLoadBool_Load_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_60;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.Out += SubGraph_CompleteObjectiveStage_Out_65;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_69.Out += SubGraph_LoadObjectiveStates_Out_69;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.Save_Out += SubGraph_SaveLoadInt_Save_Out_72;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.Load_Out += SubGraph_SaveLoadInt_Load_Out_72;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_72;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.Save_Out += SubGraph_SaveLoadInt_Save_Out_75;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.Load_Out += SubGraph_SaveLoadInt_Load_Out_75;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_75;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Save_Out += SubGraph_SaveLoadInt_Save_Out_78;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Load_Out += SubGraph_SaveLoadInt_Load_Out_78;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_78;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Save_Out += SubGraph_SaveLoadBool_Save_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Load_Out += SubGraph_SaveLoadBool_Load_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Save_Out += SubGraph_SaveLoadBool_Save_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Load_Out += SubGraph_SaveLoadBool_Load_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_112;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.Save_Out += SubGraph_SaveLoadInt_Save_Out_114;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.Load_Out += SubGraph_SaveLoadInt_Load_Out_114;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_114;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_69.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_45.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_69.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_119.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_GetNearestTech_uScript_GetNearestTech_7.OnDisable();
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_25.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_41.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_50.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_51.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_54.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_69.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_85.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.OnDisable();
		logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_116.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_69.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_69.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output1 -= uScriptCon_ManualSwitch_Output1_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output2 -= uScriptCon_ManualSwitch_Output2_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output3 -= uScriptCon_ManualSwitch_Output3_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output4 -= uScriptCon_ManualSwitch_Output4_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output5 -= uScriptCon_ManualSwitch_Output5_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output6 -= uScriptCon_ManualSwitch_Output6_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output7 -= uScriptCon_ManualSwitch_Output7_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output8 -= uScriptCon_ManualSwitch_Output8_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Save_Out -= SubGraph_SaveLoadBool_Save_Out_57;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Load_Out -= SubGraph_SaveLoadBool_Load_Out_57;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_57;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save_Out -= SubGraph_SaveLoadBool_Save_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load_Out -= SubGraph_SaveLoadBool_Load_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Save_Out -= SubGraph_SaveLoadBool_Save_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Load_Out -= SubGraph_SaveLoadBool_Load_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_60;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.Out -= SubGraph_CompleteObjectiveStage_Out_65;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_69.Out -= SubGraph_LoadObjectiveStates_Out_69;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.Save_Out -= SubGraph_SaveLoadInt_Save_Out_72;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.Load_Out -= SubGraph_SaveLoadInt_Load_Out_72;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_72;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.Save_Out -= SubGraph_SaveLoadInt_Save_Out_75;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.Load_Out -= SubGraph_SaveLoadInt_Load_Out_75;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_75;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Save_Out -= SubGraph_SaveLoadInt_Save_Out_78;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Load_Out -= SubGraph_SaveLoadInt_Load_Out_78;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_78;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Save_Out -= SubGraph_SaveLoadBool_Save_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Load_Out -= SubGraph_SaveLoadBool_Load_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Save_Out -= SubGraph_SaveLoadBool_Save_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Load_Out -= SubGraph_SaveLoadBool_Load_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_112;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.Save_Out -= SubGraph_SaveLoadInt_Save_Out_114;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.Load_Out -= SubGraph_SaveLoadInt_Load_Out_114;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_114;
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

	private void Instance_EnemyTechDestroyedEvent_6(object o, uScript_EnemyTechDestroyedEvent.EnemyTechDestroyedEventArgs e)
	{
		event_UnityEngine_GameObject_Faction_6 = e.Faction;
		event_UnityEngine_GameObject_FactionTotal_6 = e.FactionTotal;
		event_UnityEngine_GameObject_NumEnemyTechsDestroyed_6 = e.NumEnemyTechsDestroyed;
		Relay_EnemyTechDestroyedEvent_6();
	}

	private void Instance_SaveEvent_61(object o, EventArgs e)
	{
		Relay_SaveEvent_61();
	}

	private void Instance_LoadEvent_61(object o, EventArgs e)
	{
		Relay_LoadEvent_61();
	}

	private void Instance_RestartEvent_61(object o, EventArgs e)
	{
		Relay_RestartEvent_61();
	}

	private void uScriptCon_ManualSwitch_Output1_38(object o, EventArgs e)
	{
		Relay_Output1_38();
	}

	private void uScriptCon_ManualSwitch_Output2_38(object o, EventArgs e)
	{
		Relay_Output2_38();
	}

	private void uScriptCon_ManualSwitch_Output3_38(object o, EventArgs e)
	{
		Relay_Output3_38();
	}

	private void uScriptCon_ManualSwitch_Output4_38(object o, EventArgs e)
	{
		Relay_Output4_38();
	}

	private void uScriptCon_ManualSwitch_Output5_38(object o, EventArgs e)
	{
		Relay_Output5_38();
	}

	private void uScriptCon_ManualSwitch_Output6_38(object o, EventArgs e)
	{
		Relay_Output6_38();
	}

	private void uScriptCon_ManualSwitch_Output7_38(object o, EventArgs e)
	{
		Relay_Output7_38();
	}

	private void uScriptCon_ManualSwitch_Output8_38(object o, EventArgs e)
	{
		Relay_Output8_38();
	}

	private void SubGraph_SaveLoadBool_Save_Out_57(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_57 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_57;
		Relay_Save_Out_57();
	}

	private void SubGraph_SaveLoadBool_Load_Out_57(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_57 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_57;
		Relay_Load_Out_57();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_57(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_57 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_57;
		Relay_Restart_Out_57();
	}

	private void SubGraph_SaveLoadBool_Save_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_NPCSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Save_Out_58();
	}

	private void SubGraph_SaveLoadBool_Load_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_NPCSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Load_Out_58();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_NPCSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Restart_Out_58();
	}

	private void SubGraph_SaveLoadBool_Save_Out_60(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_60;
		Relay_Save_Out_60();
	}

	private void SubGraph_SaveLoadBool_Load_Out_60(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_60;
		Relay_Load_Out_60();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_60(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_60;
		Relay_Restart_Out_60();
	}

	private void SubGraph_CompleteObjectiveStage_Out_65(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_65 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_65;
		Relay_Out_65();
	}

	private void SubGraph_LoadObjectiveStates_Out_69(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_69();
	}

	private void SubGraph_SaveLoadInt_Save_Out_72(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_72 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_72;
		Relay_Save_Out_72();
	}

	private void SubGraph_SaveLoadInt_Load_Out_72(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_72 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_72;
		Relay_Load_Out_72();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_72(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_72 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_72;
		Relay_Restart_Out_72();
	}

	private void SubGraph_SaveLoadInt_Save_Out_75(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_75 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_75;
		Relay_Save_Out_75();
	}

	private void SubGraph_SaveLoadInt_Load_Out_75(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_75 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_75;
		Relay_Load_Out_75();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_75(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_75 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_75;
		Relay_Restart_Out_75();
	}

	private void SubGraph_SaveLoadInt_Save_Out_78(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_78 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_78;
		Relay_Save_Out_78();
	}

	private void SubGraph_SaveLoadInt_Load_Out_78(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_78 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_78;
		Relay_Load_Out_78();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_78(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_78 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_78;
		Relay_Restart_Out_78();
	}

	private void SubGraph_SaveLoadBool_Save_Out_100(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_100;
		Relay_Save_Out_100();
	}

	private void SubGraph_SaveLoadBool_Load_Out_100(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_100;
		Relay_Load_Out_100();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_100(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_100;
		Relay_Restart_Out_100();
	}

	private void SubGraph_SaveLoadBool_Save_Out_112(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = e.boolean;
		local_InitialisedCount_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_112;
		Relay_Save_Out_112();
	}

	private void SubGraph_SaveLoadBool_Load_Out_112(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = e.boolean;
		local_InitialisedCount_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_112;
		Relay_Load_Out_112();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_112(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = e.boolean;
		local_InitialisedCount_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_112;
		Relay_Restart_Out_112();
	}

	private void SubGraph_SaveLoadInt_Save_Out_114(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_114 = e.integer;
		local_TargetCount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_114;
		Relay_Save_Out_114();
	}

	private void SubGraph_SaveLoadInt_Load_Out_114(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_114 = e.integer;
		local_TargetCount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_114;
		Relay_Load_Out_114();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_114(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_114 = e.integer;
		local_TargetCount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_114;
		Relay_Restart_Out_114();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_38();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_Succeed_5()
	{
		logic_uScript_FinishEncounter_owner_5 = owner_Connection_1;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_5.Succeed(logic_uScript_FinishEncounter_owner_5);
	}

	private void Relay_Fail_5()
	{
		logic_uScript_FinishEncounter_owner_5 = owner_Connection_1;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_5.Fail(logic_uScript_FinishEncounter_owner_5);
	}

	private void Relay_EnemyTechDestroyedEvent_6()
	{
		local_CurrentKillTotal_System_Int32 = event_UnityEngine_GameObject_NumEnemyTechsDestroyed_6;
		Relay_In_93();
	}

	private void Relay_In_7()
	{
		logic_uScript_GetNearestTech_Return_7 = logic_uScript_GetNearestTech_uScript_GetNearestTech_7.In(logic_uScript_GetNearestTech_team_7, logic_uScript_GetNearestTech_recheck_7);
		local_NearestEnemyTech_Tank = logic_uScript_GetNearestTech_Return_7;
		bool found = logic_uScript_GetNearestTech_uScript_GetNearestTech_7.Found;
		bool notFound = logic_uScript_GetNearestTech_uScript_GetNearestTech_7.NotFound;
		if (found)
		{
			Relay_In_9();
		}
		if (notFound)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_9()
	{
		logic_uScript_SetEncounterTarget_owner_9 = owner_Connection_11;
		logic_uScript_SetEncounterTarget_visibleObject_9 = local_NearestEnemyTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_9.In(logic_uScript_SetEncounterTarget_owner_9, logic_uScript_SetEncounterTarget_visibleObject_9);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_9.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_ClearEncounterTarget_owner_10 = owner_Connection_12;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_10.In(logic_uScript_ClearEncounterTarget_owner_10);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_10.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_25()
	{
		logic_uScript_InRangeOfTech_tank_25 = local_NPCTank_Tank;
		logic_uScript_InRangeOfTech_range_25 = DistInRangeOfNPC;
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_25.In(logic_uScript_InRangeOfTech_tank_25, logic_uScript_InRangeOfTech_range_25);
		bool inRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_25.InRange;
		bool outOfRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_25.OutOfRange;
		if (inRange)
		{
			Relay_True_52();
		}
		if (outOfRange)
		{
			Relay_In_46();
		}
	}

	private void Relay_True_26()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.True(out logic_uScriptAct_SetBool_Target_26);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_26;
	}

	private void Relay_False_26()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.False(out logic_uScriptAct_SetBool_Target_26);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_26;
	}

	private void Relay_InitialSpawn_28()
	{
		int num = 0;
		Array nPCTech = NPCTech;
		if (logic_uScript_SpawnTechsFromData_spawnData_28.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_28, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_uScript_SpawnTechsFromData_spawnData_28, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_28 = owner_Connection_33;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_28.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_28, logic_uScript_SpawnTechsFromData_ownerNode_28, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_28, logic_uScript_SpawnTechsFromData_allowResurrection_28);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_28.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_AtIndex_34()
	{
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_AccessListTech_techList_34.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_34, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_34, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_34.AtIndex(ref logic_uScript_AccessListTech_techList_34, logic_uScript_AccessListTech_index_34, out logic_uScript_AccessListTech_value_34);
		local_NPCTanks_TankArray = logic_uScript_AccessListTech_techList_34;
		local_NPCTank_Tank = logic_uScript_AccessListTech_value_34;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_34.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_RemoveScenery_ownerNode_37 = owner_Connection_23;
		logic_uScript_RemoveScenery_positionName_37 = clearSceneryPos;
		logic_uScript_RemoveScenery_radius_37 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_37.In(logic_uScript_RemoveScenery_ownerNode_37, logic_uScript_RemoveScenery_positionName_37, logic_uScript_RemoveScenery_radius_37, logic_uScript_RemoveScenery_preventChunksSpawning_37);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_37.Out)
		{
			Relay_True_74();
		}
	}

	private void Relay_Output1_38()
	{
		Relay_In_62();
	}

	private void Relay_Output2_38()
	{
		Relay_In_7();
	}

	private void Relay_Output3_38()
	{
	}

	private void Relay_Output4_38()
	{
	}

	private void Relay_Output5_38()
	{
	}

	private void Relay_Output6_38()
	{
	}

	private void Relay_Output7_38()
	{
	}

	private void Relay_Output8_38()
	{
	}

	private void Relay_In_38()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_38 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.In(logic_uScriptCon_ManualSwitch_CurrentOutput_38);
	}

	private void Relay_In_39()
	{
		logic_uScriptCon_CompareBool_Bool_39 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.In(logic_uScriptCon_CompareBool_Bool_39);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.False;
		if (num)
		{
			Relay_GetTargetCount_122();
		}
		if (flag)
		{
			Relay_In_119();
		}
	}

	private void Relay_In_41()
	{
		int num = 0;
		Array array = msgNPCGreeting;
		if (logic_uScript_AddOnScreenMessage_locString_41.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_41, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_41, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_41 = local_24_System_String;
		logic_uScript_AddOnScreenMessage_Return_41 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_41.In(logic_uScript_AddOnScreenMessage_locString_41, logic_uScript_AddOnScreenMessage_msgPriority_41, logic_uScript_AddOnScreenMessage_holdMsg_41, logic_uScript_AddOnScreenMessage_tag_41, logic_uScript_AddOnScreenMessage_speaker_41, logic_uScript_AddOnScreenMessage_side_41);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_41.Shown)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_43()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_43 = local_29_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_43.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_43, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_43);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_43.Out)
		{
			Relay_In_51();
		}
	}

	private void Relay_In_45()
	{
		logic_uScript_FlyTechUpAndAway_tech_45 = local_NPCTank_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_45 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_45 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_45.In(logic_uScript_FlyTechUpAndAway_tech_45, logic_uScript_FlyTechUpAndAway_maxLifetime_45, logic_uScript_FlyTechUpAndAway_targetHeight_45, logic_uScript_FlyTechUpAndAway_aiTree_45, logic_uScript_FlyTechUpAndAway_removalParticles_45);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_45.Out)
		{
			Relay_True_26();
		}
	}

	private void Relay_In_46()
	{
		logic_uScriptCon_CompareBool_Bool_46 = local_NPCSeen_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.In(logic_uScriptCon_CompareBool_Bool_46);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.True)
		{
			Relay_True_95();
		}
	}

	private void Relay_In_47()
	{
		int num = 0;
		Array nPCTech = NPCTech;
		if (logic_uScript_GetAndCheckTechs_techData_47.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_47, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_uScript_GetAndCheckTechs_techData_47, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_47 = owner_Connection_20;
		int num2 = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_47.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_47, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_47, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_47 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_47.In(logic_uScript_GetAndCheckTechs_techData_47, logic_uScript_GetAndCheckTechs_ownerNode_47, ref logic_uScript_GetAndCheckTechs_techs_47);
		local_NPCTanks_TankArray = logic_uScript_GetAndCheckTechs_techs_47;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_47.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_47.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_34();
		}
		if (someAlive)
		{
			Relay_AtIndex_34();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_50 = owner_Connection_35;
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_50.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_50, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_50, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_50 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_50.In(logic_uScript_SetOneTechAsEncounterTarget_owner_50, logic_uScript_SetOneTechAsEncounterTarget_techs_50);
		local_NPCTank_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_50;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_50.Out)
		{
			Relay_In_97();
		}
	}

	private void Relay_In_51()
	{
		int num = 0;
		Array array = msgNPCGreetingInturrupt;
		if (logic_uScript_AddOnScreenMessage_locString_51.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_51, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_51, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_51 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_51.In(logic_uScript_AddOnScreenMessage_locString_51, logic_uScript_AddOnScreenMessage_msgPriority_51, logic_uScript_AddOnScreenMessage_holdMsg_51, logic_uScript_AddOnScreenMessage_tag_51, logic_uScript_AddOnScreenMessage_speaker_51, logic_uScript_AddOnScreenMessage_side_51);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_51.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_True_52()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_52.True(out logic_uScriptAct_SetBool_Target_52);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_52;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_52.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_False_52()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_52.False(out logic_uScriptAct_SetBool_Target_52);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_52;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_52.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_54()
	{
		logic_uScript_SetTankInvulnerable_tank_54 = local_NPCTank_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_54.In(logic_uScript_SetTankInvulnerable_invulnerable_54, logic_uScript_SetTankInvulnerable_tank_54);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_54.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_Save_Out_57()
	{
		Relay_Save_60();
	}

	private void Relay_Load_Out_57()
	{
		Relay_Load_60();
	}

	private void Relay_Restart_Out_57()
	{
		Relay_Set_False_60();
	}

	private void Relay_Save_57()
	{
		logic_SubGraph_SaveLoadBool_boolean_57 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_57 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Save(ref logic_SubGraph_SaveLoadBool_boolean_57, logic_SubGraph_SaveLoadBool_boolAsVariable_57, logic_SubGraph_SaveLoadBool_uniqueID_57);
	}

	private void Relay_Load_57()
	{
		logic_SubGraph_SaveLoadBool_boolean_57 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_57 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Load(ref logic_SubGraph_SaveLoadBool_boolean_57, logic_SubGraph_SaveLoadBool_boolAsVariable_57, logic_SubGraph_SaveLoadBool_uniqueID_57);
	}

	private void Relay_Set_True_57()
	{
		logic_SubGraph_SaveLoadBool_boolean_57 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_57 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_57, logic_SubGraph_SaveLoadBool_boolAsVariable_57, logic_SubGraph_SaveLoadBool_uniqueID_57);
	}

	private void Relay_Set_False_57()
	{
		logic_SubGraph_SaveLoadBool_boolean_57 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_57 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_57.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_57, logic_SubGraph_SaveLoadBool_boolAsVariable_57, logic_SubGraph_SaveLoadBool_uniqueID_57);
	}

	private void Relay_Save_Out_58()
	{
		Relay_Save_57();
	}

	private void Relay_Load_Out_58()
	{
		Relay_Load_57();
	}

	private void Relay_Restart_Out_58()
	{
		Relay_Set_False_57();
	}

	private void Relay_Save_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Load_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Set_True_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Set_False_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_NPCSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Save_Out_60()
	{
		Relay_Save_100();
	}

	private void Relay_Load_Out_60()
	{
		Relay_Load_100();
	}

	private void Relay_Restart_Out_60()
	{
		Relay_Set_False_100();
	}

	private void Relay_Save_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Save(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Load_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Load(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Set_True_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Set_False_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_SaveEvent_61()
	{
		Relay_Save_58();
	}

	private void Relay_LoadEvent_61()
	{
		Relay_Load_58();
	}

	private void Relay_RestartEvent_61()
	{
		Relay_Set_False_58();
	}

	private void Relay_In_62()
	{
		logic_uScriptCon_CompareBool_Bool_62 = local_NPCSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.In(logic_uScriptCon_CompareBool_Bool_62);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.False;
		if (num)
		{
			Relay_In_39();
		}
		if (flag)
		{
			Relay_InitialSpawn_28();
		}
	}

	private void Relay_Out_65()
	{
	}

	private void Relay_In_65()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_65 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_65, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_65);
	}

	private void Relay_Out_69()
	{
		Relay_Load_75();
	}

	private void Relay_In_69()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_69 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_69.In(logic_SubGraph_LoadObjectiveStates_currentObjective_69);
	}

	private void Relay_Save_Out_72()
	{
		Relay_Save_75();
	}

	private void Relay_Load_Out_72()
	{
		Relay_In_69();
	}

	private void Relay_Restart_Out_72()
	{
		Relay_Restart_75();
	}

	private void Relay_Save_72()
	{
		logic_SubGraph_SaveLoadInt_integer_72 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_72 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.Save(logic_SubGraph_SaveLoadInt_restartValue_72, ref logic_SubGraph_SaveLoadInt_integer_72, logic_SubGraph_SaveLoadInt_intAsVariable_72, logic_SubGraph_SaveLoadInt_uniqueID_72);
	}

	private void Relay_Load_72()
	{
		logic_SubGraph_SaveLoadInt_integer_72 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_72 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.Load(logic_SubGraph_SaveLoadInt_restartValue_72, ref logic_SubGraph_SaveLoadInt_integer_72, logic_SubGraph_SaveLoadInt_intAsVariable_72, logic_SubGraph_SaveLoadInt_uniqueID_72);
	}

	private void Relay_Restart_72()
	{
		logic_SubGraph_SaveLoadInt_integer_72 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_72 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_72.Restart(logic_SubGraph_SaveLoadInt_restartValue_72, ref logic_SubGraph_SaveLoadInt_integer_72, logic_SubGraph_SaveLoadInt_intAsVariable_72, logic_SubGraph_SaveLoadInt_uniqueID_72);
	}

	private void Relay_True_74()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.True(out logic_uScriptAct_SetBool_Target_74);
		local_NPCSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_74;
	}

	private void Relay_False_74()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.False(out logic_uScriptAct_SetBool_Target_74);
		local_NPCSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_74;
	}

	private void Relay_Save_Out_75()
	{
		Relay_Save_78();
	}

	private void Relay_Load_Out_75()
	{
		Relay_Load_78();
	}

	private void Relay_Restart_Out_75()
	{
		Relay_Restart_78();
	}

	private void Relay_Save_75()
	{
		logic_SubGraph_SaveLoadInt_integer_75 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_75 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.Save(logic_SubGraph_SaveLoadInt_restartValue_75, ref logic_SubGraph_SaveLoadInt_integer_75, logic_SubGraph_SaveLoadInt_intAsVariable_75, logic_SubGraph_SaveLoadInt_uniqueID_75);
	}

	private void Relay_Load_75()
	{
		logic_SubGraph_SaveLoadInt_integer_75 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_75 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.Load(logic_SubGraph_SaveLoadInt_restartValue_75, ref logic_SubGraph_SaveLoadInt_integer_75, logic_SubGraph_SaveLoadInt_intAsVariable_75, logic_SubGraph_SaveLoadInt_uniqueID_75);
	}

	private void Relay_Restart_75()
	{
		logic_SubGraph_SaveLoadInt_integer_75 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_75 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_75.Restart(logic_SubGraph_SaveLoadInt_restartValue_75, ref logic_SubGraph_SaveLoadInt_integer_75, logic_SubGraph_SaveLoadInt_intAsVariable_75, logic_SubGraph_SaveLoadInt_uniqueID_75);
	}

	private void Relay_Save_Out_78()
	{
		Relay_Save_114();
	}

	private void Relay_Load_Out_78()
	{
		Relay_Load_114();
	}

	private void Relay_Restart_Out_78()
	{
		Relay_Restart_114();
	}

	private void Relay_Save_78()
	{
		logic_SubGraph_SaveLoadInt_integer_78 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_78 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Save(logic_SubGraph_SaveLoadInt_restartValue_78, ref logic_SubGraph_SaveLoadInt_integer_78, logic_SubGraph_SaveLoadInt_intAsVariable_78, logic_SubGraph_SaveLoadInt_uniqueID_78);
	}

	private void Relay_Load_78()
	{
		logic_SubGraph_SaveLoadInt_integer_78 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_78 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Load(logic_SubGraph_SaveLoadInt_restartValue_78, ref logic_SubGraph_SaveLoadInt_integer_78, logic_SubGraph_SaveLoadInt_intAsVariable_78, logic_SubGraph_SaveLoadInt_uniqueID_78);
	}

	private void Relay_Restart_78()
	{
		logic_SubGraph_SaveLoadInt_integer_78 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_78 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Restart(logic_SubGraph_SaveLoadInt_restartValue_78, ref logic_SubGraph_SaveLoadInt_integer_78, logic_SubGraph_SaveLoadInt_intAsVariable_78, logic_SubGraph_SaveLoadInt_uniqueID_78);
	}

	private void Relay_SetCount_81()
	{
		logic_uScript_SetQuestObjectiveCount_owner_81 = owner_Connection_77;
		logic_uScript_SetQuestObjectiveCount_currentCount_81 = local_CurrentAmount_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_81.SetCount(logic_uScript_SetQuestObjectiveCount_owner_81, logic_uScript_SetQuestObjectiveCount_objectiveId_81, logic_uScript_SetQuestObjectiveCount_currentCount_81);
	}

	private void Relay_In_85()
	{
		int num = 0;
		Array array = msgComplete;
		if (logic_uScript_AddOnScreenMessage_locString_85.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_85, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_85, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_85 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_85 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_85.In(logic_uScript_AddOnScreenMessage_locString_85, logic_uScript_AddOnScreenMessage_msgPriority_85, logic_uScript_AddOnScreenMessage_holdMsg_85, logic_uScript_AddOnScreenMessage_tag_85, logic_uScript_AddOnScreenMessage_speaker_85, logic_uScript_AddOnScreenMessage_side_85);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_85.Out)
		{
			Relay_Succeed_5();
		}
	}

	private void Relay_In_88()
	{
		logic_uScriptAct_SubtractInt_A_88 = local_CurrentKillTotal_System_Int32;
		logic_uScriptAct_SubtractInt_B_88 = local_InitialAmount_System_Int32;
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_88.In(logic_uScriptAct_SubtractInt_A_88, logic_uScriptAct_SubtractInt_B_88, out logic_uScriptAct_SubtractInt_IntResult_88, out logic_uScriptAct_SubtractInt_FloatResult_88);
		local_CurrentAmount_System_Int32 = logic_uScriptAct_SubtractInt_IntResult_88;
		if (logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_88.Out)
		{
			Relay_SetCount_90();
		}
	}

	private void Relay_In_89()
	{
		logic_uScriptCon_CompareInt_A_89 = local_CurrentAmount_System_Int32;
		logic_uScriptCon_CompareInt_B_89 = local_TargetCount_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_89.In(logic_uScriptCon_CompareInt_A_89, logic_uScriptCon_CompareInt_B_89);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_89.GreaterThanOrEqualTo)
		{
			Relay_In_85();
		}
	}

	private void Relay_SetCount_90()
	{
		logic_uScript_SetQuestObjectiveCount_owner_90 = owner_Connection_87;
		logic_uScript_SetQuestObjectiveCount_objectiveId_90 = ObjectiveToCount;
		logic_uScript_SetQuestObjectiveCount_currentCount_90 = local_CurrentAmount_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_90.SetCount(logic_uScript_SetQuestObjectiveCount_owner_90, logic_uScript_SetQuestObjectiveCount_objectiveId_90, logic_uScript_SetQuestObjectiveCount_currentCount_90);
		if (logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_90.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_93()
	{
		logic_uScriptCon_CompareBool_Bool_93 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.In(logic_uScriptCon_CompareBool_Bool_93);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.True)
		{
			Relay_In_88();
		}
	}

	private void Relay_True_95()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_95.True(out logic_uScriptAct_SetBool_Target_95);
		local_NPCIgnored_System_Boolean = logic_uScriptAct_SetBool_Target_95;
	}

	private void Relay_False_95()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_95.False(out logic_uScriptAct_SetBool_Target_95);
		local_NPCIgnored_System_Boolean = logic_uScriptAct_SetBool_Target_95;
	}

	private void Relay_In_97()
	{
		logic_uScriptCon_CompareBool_Bool_97 = local_NPCIgnored_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97.In(logic_uScriptCon_CompareBool_Bool_97);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97.False;
		if (num)
		{
			Relay_In_43();
		}
		if (flag)
		{
			Relay_In_25();
		}
	}

	private void Relay_Save_Out_100()
	{
		Relay_Save_112();
	}

	private void Relay_Load_Out_100()
	{
		Relay_Load_112();
	}

	private void Relay_Restart_Out_100()
	{
		Relay_Set_False_112();
	}

	private void Relay_Save_100()
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_100 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Save(ref logic_SubGraph_SaveLoadBool_boolean_100, logic_SubGraph_SaveLoadBool_boolAsVariable_100, logic_SubGraph_SaveLoadBool_uniqueID_100);
	}

	private void Relay_Load_100()
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_100 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Load(ref logic_SubGraph_SaveLoadBool_boolean_100, logic_SubGraph_SaveLoadBool_boolAsVariable_100, logic_SubGraph_SaveLoadBool_uniqueID_100);
	}

	private void Relay_Set_True_100()
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_100 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_100, logic_SubGraph_SaveLoadBool_boolAsVariable_100, logic_SubGraph_SaveLoadBool_uniqueID_100);
	}

	private void Relay_Set_False_100()
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_100 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_100, logic_SubGraph_SaveLoadBool_boolAsVariable_100, logic_SubGraph_SaveLoadBool_uniqueID_100);
	}

	private void Relay_GetTargetCount_101()
	{
		logic_uScript_GetQuestObjectiveTargetCount_owner_101 = owner_Connection_104;
		logic_uScript_GetQuestObjectiveTargetCount_objectiveId_101 = ObjectiveToCount;
		logic_uScript_GetQuestObjectiveTargetCount_Return_101 = logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_101.GetTargetCount(logic_uScript_GetQuestObjectiveTargetCount_owner_101, logic_uScript_GetQuestObjectiveTargetCount_objectiveId_101);
		local_TargetCount_System_Int32 = logic_uScript_GetQuestObjectiveTargetCount_Return_101;
		if (logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_101.Out)
		{
			Relay_TotalEnemiesDestroyed_105();
		}
	}

	private void Relay_TotalEnemiesDestroyed_105()
	{
		logic_uScript_GetNumEnemyTechsDestroyed_Return_105 = logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_105.TotalEnemiesDestroyed(logic_uScript_GetNumEnemyTechsDestroyed_faction_105);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumEnemyTechsDestroyed_Return_105;
		if (logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_105.Out)
		{
			Relay_True_107();
		}
	}

	private void Relay_EnemiesDestroyedOfFaction_105()
	{
		logic_uScript_GetNumEnemyTechsDestroyed_Return_105 = logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_105.EnemiesDestroyedOfFaction(logic_uScript_GetNumEnemyTechsDestroyed_faction_105);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumEnemyTechsDestroyed_Return_105;
		if (logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_105.Out)
		{
			Relay_True_107();
		}
	}

	private void Relay_True_107()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_107.True(out logic_uScriptAct_SetBool_Target_107);
		local_InitialisedCount_System_Boolean = logic_uScriptAct_SetBool_Target_107;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_107.Out)
		{
			Relay_SetCount_129();
		}
	}

	private void Relay_False_107()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_107.False(out logic_uScriptAct_SetBool_Target_107);
		local_InitialisedCount_System_Boolean = logic_uScriptAct_SetBool_Target_107;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_107.Out)
		{
			Relay_SetCount_129();
		}
	}

	private void Relay_In_109()
	{
		logic_uScriptCon_CompareBool_Bool_109 = local_InitialisedCount_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.In(logic_uScriptCon_CompareBool_Bool_109);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.False)
		{
			Relay_GetTargetCount_101();
		}
	}

	private void Relay_Save_Out_112()
	{
		Relay_Save_72();
	}

	private void Relay_Load_Out_112()
	{
		Relay_Load_72();
	}

	private void Relay_Restart_Out_112()
	{
		Relay_Restart_72();
	}

	private void Relay_Save_112()
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = local_InitialisedCount_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_112 = local_InitialisedCount_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Save(ref logic_SubGraph_SaveLoadBool_boolean_112, logic_SubGraph_SaveLoadBool_boolAsVariable_112, logic_SubGraph_SaveLoadBool_uniqueID_112);
	}

	private void Relay_Load_112()
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = local_InitialisedCount_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_112 = local_InitialisedCount_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Load(ref logic_SubGraph_SaveLoadBool_boolean_112, logic_SubGraph_SaveLoadBool_boolAsVariable_112, logic_SubGraph_SaveLoadBool_uniqueID_112);
	}

	private void Relay_Set_True_112()
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = local_InitialisedCount_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_112 = local_InitialisedCount_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_112, logic_SubGraph_SaveLoadBool_boolAsVariable_112, logic_SubGraph_SaveLoadBool_uniqueID_112);
	}

	private void Relay_Set_False_112()
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = local_InitialisedCount_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_112 = local_InitialisedCount_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_112, logic_SubGraph_SaveLoadBool_boolAsVariable_112, logic_SubGraph_SaveLoadBool_uniqueID_112);
	}

	private void Relay_Save_Out_114()
	{
	}

	private void Relay_Load_Out_114()
	{
		Relay_SetCount_81();
	}

	private void Relay_Restart_Out_114()
	{
	}

	private void Relay_Save_114()
	{
		logic_SubGraph_SaveLoadInt_integer_114 = local_TargetCount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_114 = local_TargetCount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.Save(logic_SubGraph_SaveLoadInt_restartValue_114, ref logic_SubGraph_SaveLoadInt_integer_114, logic_SubGraph_SaveLoadInt_intAsVariable_114, logic_SubGraph_SaveLoadInt_uniqueID_114);
	}

	private void Relay_Load_114()
	{
		logic_SubGraph_SaveLoadInt_integer_114 = local_TargetCount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_114 = local_TargetCount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.Load(logic_SubGraph_SaveLoadInt_restartValue_114, ref logic_SubGraph_SaveLoadInt_integer_114, logic_SubGraph_SaveLoadInt_intAsVariable_114, logic_SubGraph_SaveLoadInt_uniqueID_114);
	}

	private void Relay_Restart_114()
	{
		logic_SubGraph_SaveLoadInt_integer_114 = local_TargetCount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_114 = local_TargetCount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_114.Restart(logic_SubGraph_SaveLoadInt_restartValue_114, ref logic_SubGraph_SaveLoadInt_integer_114, logic_SubGraph_SaveLoadInt_intAsVariable_114, logic_SubGraph_SaveLoadInt_uniqueID_114);
	}

	private void Relay_In_115()
	{
		logic_uScript_SetEncounterPosition_ownerNode_115 = owner_Connection_117;
		logic_uScript_SetEncounterPosition_position_115 = local_NearestEnemyTechPos_UnityEngine_Vector3;
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_115.In(logic_uScript_SetEncounterPosition_ownerNode_115, logic_uScript_SetEncounterPosition_position_115);
		if (logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_115.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_116()
	{
		logic_uScript_GetPositionOfTech_tech_116 = local_NearestEnemyTech_Tank;
		logic_uScript_GetPositionOfTech_Return_116 = logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_116.In(logic_uScript_GetPositionOfTech_tech_116);
		local_NearestEnemyTechPos_UnityEngine_Vector3 = logic_uScript_GetPositionOfTech_Return_116;
		if (logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_116.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_In_119()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_119 = owner_Connection_120;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_119.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_119);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_119.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_GetTargetCount_122()
	{
		logic_uScript_GetQuestObjectiveTargetCount_owner_122 = owner_Connection_123;
		logic_uScript_GetQuestObjectiveTargetCount_objectiveId_122 = ObjectiveToCount;
		logic_uScript_GetQuestObjectiveTargetCount_Return_122 = logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_122.GetTargetCount(logic_uScript_GetQuestObjectiveTargetCount_owner_122, logic_uScript_GetQuestObjectiveTargetCount_objectiveId_122);
		local_TargetCount_System_Int32 = logic_uScript_GetQuestObjectiveTargetCount_Return_122;
		if (logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_122.Out)
		{
			Relay_TotalEnemiesDestroyed_124();
		}
	}

	private void Relay_TotalEnemiesDestroyed_124()
	{
		logic_uScript_GetNumEnemyTechsDestroyed_Return_124 = logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_124.TotalEnemiesDestroyed(logic_uScript_GetNumEnemyTechsDestroyed_faction_124);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumEnemyTechsDestroyed_Return_124;
		if (logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_124.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_EnemiesDestroyedOfFaction_124()
	{
		logic_uScript_GetNumEnemyTechsDestroyed_Return_124 = logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_124.EnemiesDestroyedOfFaction(logic_uScript_GetNumEnemyTechsDestroyed_faction_124);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumEnemyTechsDestroyed_Return_124;
		if (logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_124.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_SetCount_129()
	{
		logic_uScript_SetQuestObjectiveCount_owner_129 = owner_Connection_128;
		logic_uScript_SetQuestObjectiveCount_objectiveId_129 = ObjectiveToCount;
		logic_uScript_SetQuestObjectiveCount_currentCount_129 = local_CurrentAmount_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_129.SetCount(logic_uScript_SetQuestObjectiveCount_owner_129, logic_uScript_SetQuestObjectiveCount_objectiveId_129, logic_uScript_SetQuestObjectiveCount_currentCount_129);
	}
}
