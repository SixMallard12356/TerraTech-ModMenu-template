using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_FindDeliveryCrate : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string cratePosition = "";

	[Multiline(3)]
	public string enemyPosition = "";

	public SpawnTechData[] enemySpawnData = new SpawnTechData[0];

	private float local_124_System_Single;

	private Tank local_41_Tank;

	private Quaternion local_42_UnityEngine_Quaternion = new Quaternion(0f, 0f, 0f, 0f);

	private Crate local_Crate_Crate;

	private float local_CrateOpenRange_System_Single;

	private bool local_EnemySpawned_System_Boolean;

	private bool local_EnemySpawnStarted_System_Boolean;

	private Tank[] local_EnemyTechs_TankArray = new Tank[0];

	private bool local_Init_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private float local_TimerCount_System_Single;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public uScript_AddMessage.MessageData msgCrateUnlocked;

	public uScript_AddMessage.MessageData msgEnemyDestroyed;

	public uScript_AddMessage.MessageData msgEnemyLanding;

	public uScript_AddMessage.MessageData msgEnteringArea;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_18;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_38;

	private GameObject owner_Connection_44;

	private GameObject owner_Connection_56;

	private GameObject owner_Connection_58;

	private GameObject owner_Connection_62;

	private GameObject owner_Connection_68;

	private GameObject owner_Connection_72;

	private GameObject owner_Connection_81;

	private GameObject owner_Connection_82;

	private GameObject owner_Connection_90;

	private GameObject owner_Connection_105;

	private GameObject owner_Connection_108;

	private GameObject owner_Connection_113;

	private GameObject owner_Connection_115;

	private GameObject owner_Connection_116;

	private GameObject owner_Connection_121;

	private GameObject owner_Connection_126;

	private uScript_SpawnDeliveryCrate logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_2 = new uScript_SpawnDeliveryCrate();

	private string logic_uScript_SpawnDeliveryCrate_positionName_2 = "";

	private GameObject logic_uScript_SpawnDeliveryCrate_ownerNode_2;

	private bool logic_uScript_SpawnDeliveryCrate_visibleOnRadar_2;

	private bool logic_uScript_SpawnDeliveryCrate_Out_2 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_4;

	private bool logic_uScriptCon_CompareBool_True_4 = true;

	private bool logic_uScriptCon_CompareBool_False_4 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_6 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_6;

	private bool logic_uScriptAct_SetBool_Out_6 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_6 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_6 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_9;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_9 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_9 = "Init";

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_13 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_13;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_13 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_13 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_13 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_15;

	private bool logic_uScriptCon_CompareBool_True_15 = true;

	private bool logic_uScriptCon_CompareBool_False_15 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_17 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_17 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_17;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_17 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_17;

	private bool logic_uScript_SpawnTechsFromData_Out_17 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_20 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_20;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_20 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_20;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_20 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_20 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_20 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_20 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_23 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_23;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_23;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_23;

	private bool logic_uScript_AddMessage_Out_23 = true;

	private bool logic_uScript_AddMessage_Shown_23 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_28 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_28;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_28;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_28;

	private bool logic_uScript_AddMessage_Out_28 = true;

	private bool logic_uScript_AddMessage_Shown_28 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_29 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_29;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_29;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_29;

	private bool logic_uScript_AddMessage_Out_29 = true;

	private bool logic_uScript_AddMessage_Shown_29 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_33;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_33 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_33 = "EnemySpawned";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_35 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_35;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_35 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_35 = "Stage";

	private uScript_SetEncounterRotation logic_uScript_SetEncounterRotation_uScript_SetEncounterRotation_37 = new uScript_SetEncounterRotation();

	private GameObject logic_uScript_SetEncounterRotation_ownerNode_37;

	private Quaternion logic_uScript_SetEncounterRotation_rotation_37;

	private bool logic_uScript_SetEncounterRotation_Out_37 = true;

	private uScript_GetTechRotation logic_uScript_GetTechRotation_uScript_GetTechRotation_39 = new uScript_GetTechRotation();

	private Tank logic_uScript_GetTechRotation_tech_39;

	private Quaternion logic_uScript_GetTechRotation_Return_39;

	private bool logic_uScript_GetTechRotation_Out_39 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_40 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_40;

	private bool logic_uScript_GetPlayerTank_Returned_40 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_40 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_43 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_43;

	private string logic_uScript_RemoveScenery_positionName_43 = "";

	private float logic_uScript_RemoveScenery_radius_43 = 15f;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_43 = true;

	private bool logic_uScript_RemoveScenery_Out_43 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_46;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_48 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_49;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_49;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_52 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_52;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_52;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_52;

	private bool logic_uScript_AddMessage_Out_52 = true;

	private bool logic_uScript_AddMessage_Shown_52 = true;

	private uScript_CheckDeliveryCrateSpawned logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_55 = new uScript_CheckDeliveryCrateSpawned();

	private GameObject logic_uScript_CheckDeliveryCrateSpawned_ownerNode_55;

	private bool logic_uScript_CheckDeliveryCrateSpawned_Out_55 = true;

	private bool logic_uScript_CheckDeliveryCrateSpawned_Yes_55 = true;

	private bool logic_uScript_CheckDeliveryCrateSpawned_No_55 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_57 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_57;

	private bool logic_uScript_FinishEncounter_Out_57 = true;

	private uScript_UnlockDeliveryCrate logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_60 = new uScript_UnlockDeliveryCrate();

	private GameObject logic_uScript_UnlockDeliveryCrate_ownerNode_60;

	private bool logic_uScript_UnlockDeliveryCrate_Out_60 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_61;

	private bool logic_uScriptCon_CompareBool_True_61 = true;

	private bool logic_uScriptCon_CompareBool_False_61 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_63 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_63;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_63;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_65;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_65;

	private uScript_SetQuestObjectiveCompleted logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_67 = new uScript_SetQuestObjectiveCompleted();

	private GameObject logic_uScript_SetQuestObjectiveCompleted_owner_67;

	private int logic_uScript_SetQuestObjectiveCompleted_objectiveId_67;

	private bool logic_uScript_SetQuestObjectiveCompleted_completed_67 = true;

	private bool logic_uScript_SetQuestObjectiveCompleted_Out_67 = true;

	private uScript_SetQuestObjectiveVisible logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_69 = new uScript_SetQuestObjectiveVisible();

	private GameObject logic_uScript_SetQuestObjectiveVisible_owner_69;

	private int logic_uScript_SetQuestObjectiveVisible_objectiveId_69 = 4;

	private bool logic_uScript_SetQuestObjectiveVisible_visible_69 = true;

	private bool logic_uScript_SetQuestObjectiveVisible_Out_69 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_73 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_73 = 4;

	private int logic_uScriptAct_SetInt_Target_73;

	private bool logic_uScriptAct_SetInt_Out_73 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_75 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_75;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_78 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_78;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_78 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_78;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_78 = true;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_85 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_85 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_85;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_85 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_85 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_85 = true;

	private uScript_GetCrateOpenTriggerRange logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_86 = new uScript_GetCrateOpenTriggerRange();

	private Crate logic_uScript_GetCrateOpenTriggerRange_crate_86;

	private float logic_uScript_GetCrateOpenTriggerRange_Return_86;

	private bool logic_uScript_GetCrateOpenTriggerRange_Out_86 = true;

	private uScript_GetDeliveryCrate logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_89 = new uScript_GetDeliveryCrate();

	private GameObject logic_uScript_GetDeliveryCrate_ownerNode_89;

	private Crate logic_uScript_GetDeliveryCrate_Return_89;

	private bool logic_uScript_GetDeliveryCrate_Out_89 = true;

	private bool logic_uScript_GetDeliveryCrate_Success_89 = true;

	private bool logic_uScript_GetDeliveryCrate_Failure_89 = true;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_93 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_93 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_93;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_93 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_93 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_93 = true;

	private uScript_GetCrateOpenTriggerRange logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_94 = new uScript_GetCrateOpenTriggerRange();

	private Crate logic_uScript_GetCrateOpenTriggerRange_crate_94;

	private float logic_uScript_GetCrateOpenTriggerRange_Return_94;

	private bool logic_uScript_GetCrateOpenTriggerRange_Out_94 = true;

	private uScript_GetDeliveryCrate logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_97 = new uScript_GetDeliveryCrate();

	private GameObject logic_uScript_GetDeliveryCrate_ownerNode_97;

	private Crate logic_uScript_GetDeliveryCrate_Return_97;

	private bool logic_uScript_GetDeliveryCrate_Out_97 = true;

	private bool logic_uScript_GetDeliveryCrate_Success_97 = true;

	private bool logic_uScript_GetDeliveryCrate_Failure_97 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_104 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_104;

	private object logic_uScript_SetEncounterTarget_visibleObject_104 = "";

	private bool logic_uScript_SetEncounterTarget_Out_104 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_109 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_109;

	private float logic_uScriptCon_CompareFloat_B_109;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_109 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_109 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_109 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_109 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_109 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_109 = true;

	private uScript_GetEncounterTimeRemaining logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_111 = new uScript_GetEncounterTimeRemaining();

	private GameObject logic_uScript_GetEncounterTimeRemaining_owner_111;

	private float logic_uScript_GetEncounterTimeRemaining_Return_111;

	private bool logic_uScript_GetEncounterTimeRemaining_Out_111 = true;

	private uScript_StartEncounterTimer logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_112 = new uScript_StartEncounterTimer();

	private GameObject logic_uScript_StartEncounterTimer_owner_112;

	private bool logic_uScript_StartEncounterTimer_Out_112 = true;

	private uScript_RemoveEncounterTimer logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_114 = new uScript_RemoveEncounterTimer();

	private GameObject logic_uScript_RemoveEncounterTimer_owner_114;

	private bool logic_uScript_RemoveEncounterTimer_Out_114 = true;

	private uScript_RemoveEncounterTimer logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_117 = new uScript_RemoveEncounterTimer();

	private GameObject logic_uScript_RemoveEncounterTimer_owner_117;

	private bool logic_uScript_RemoveEncounterTimer_Out_117 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_120 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_120;

	private string logic_uScript_RemoveScenery_positionName_120 = "";

	private float logic_uScript_RemoveScenery_radius_120 = 25f;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_120 = true;

	private bool logic_uScript_RemoveScenery_Out_120 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_122 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_122;

	private float logic_uScriptCon_CompareFloat_B_122 = 20f;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_122 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_122 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_122 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_122 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_122 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_122 = true;

	private uScriptAct_Stopwatch logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_123 = new uScriptAct_Stopwatch();

	private float logic_uScriptAct_Stopwatch_Seconds_123;

	private bool logic_uScriptAct_Stopwatch_Started_123 = true;

	private bool logic_uScriptAct_Stopwatch_Stopped_123 = true;

	private bool logic_uScriptAct_Stopwatch_Reset_123 = true;

	private bool logic_uScriptAct_Stopwatch_CheckedTime_123 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_125 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_125 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_125;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_125 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_125;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_125 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_125 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_125 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_125 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_129 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_129;

	private bool logic_uScriptAct_SetBool_Out_129 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_129 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_129 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_132;

	private bool logic_uScriptCon_CompareBool_True_132 = true;

	private bool logic_uScriptCon_CompareBool_False_132 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_133 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_133;

	private bool logic_uScriptAct_SetBool_Out_133 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_133 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_133 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_1.AddComponent<uScript_EncounterUpdate>();
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
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
			if (null != owner_Connection_11)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_11.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_11.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_8;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_8;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_8;
				}
			}
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
		}
		if (null == owner_Connection_18 || !m_RegisteredForEvents)
		{
			owner_Connection_18 = parentGameObject;
		}
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
		}
		if (null == owner_Connection_38 || !m_RegisteredForEvents)
		{
			owner_Connection_38 = parentGameObject;
		}
		if (null == owner_Connection_44 || !m_RegisteredForEvents)
		{
			owner_Connection_44 = parentGameObject;
		}
		if (null == owner_Connection_56 || !m_RegisteredForEvents)
		{
			owner_Connection_56 = parentGameObject;
		}
		if (null == owner_Connection_58 || !m_RegisteredForEvents)
		{
			owner_Connection_58 = parentGameObject;
		}
		if (null == owner_Connection_62 || !m_RegisteredForEvents)
		{
			owner_Connection_62 = parentGameObject;
		}
		if (null == owner_Connection_68 || !m_RegisteredForEvents)
		{
			owner_Connection_68 = parentGameObject;
		}
		if (null == owner_Connection_72 || !m_RegisteredForEvents)
		{
			owner_Connection_72 = parentGameObject;
		}
		if (null == owner_Connection_81 || !m_RegisteredForEvents)
		{
			owner_Connection_81 = parentGameObject;
		}
		if (null == owner_Connection_82 || !m_RegisteredForEvents)
		{
			owner_Connection_82 = parentGameObject;
		}
		if (null == owner_Connection_90 || !m_RegisteredForEvents)
		{
			owner_Connection_90 = parentGameObject;
		}
		if (null == owner_Connection_105 || !m_RegisteredForEvents)
		{
			owner_Connection_105 = parentGameObject;
		}
		if (null == owner_Connection_108 || !m_RegisteredForEvents)
		{
			owner_Connection_108 = parentGameObject;
		}
		if (null == owner_Connection_113 || !m_RegisteredForEvents)
		{
			owner_Connection_113 = parentGameObject;
		}
		if (null == owner_Connection_115 || !m_RegisteredForEvents)
		{
			owner_Connection_115 = parentGameObject;
		}
		if (null == owner_Connection_116 || !m_RegisteredForEvents)
		{
			owner_Connection_116 = parentGameObject;
		}
		if (null == owner_Connection_121 || !m_RegisteredForEvents)
		{
			owner_Connection_121 = parentGameObject;
		}
		if (null == owner_Connection_126 || !m_RegisteredForEvents)
		{
			owner_Connection_126 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_1.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_11)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_11.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_11.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_8;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_8;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_8;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_1)
		{
			uScript_EncounterUpdate component = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_0;
				component.OnSuspend -= Instance_OnSuspend_0;
				component.OnResume -= Instance_OnResume_0;
			}
		}
		if (null != owner_Connection_11)
		{
			uScript_SaveLoad component2 = owner_Connection_11.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_8;
				component2.LoadEvent -= Instance_LoadEvent_8;
				component2.RestartEvent -= Instance_RestartEvent_8;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_2.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_6.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_13.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_17.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_23.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_28.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_29.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.SetParent(g);
		logic_uScript_SetEncounterRotation_uScript_SetEncounterRotation_37.SetParent(g);
		logic_uScript_GetTechRotation_uScript_GetTechRotation_39.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_40.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_43.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_52.SetParent(g);
		logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_55.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_57.SetParent(g);
		logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_60.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_63.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.SetParent(g);
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_67.SetParent(g);
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_69.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_73.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_75.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_78.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_85.SetParent(g);
		logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_86.SetParent(g);
		logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_89.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_93.SetParent(g);
		logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_94.SetParent(g);
		logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_97.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_104.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_109.SetParent(g);
		logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_111.SetParent(g);
		logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_112.SetParent(g);
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_114.SetParent(g);
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_117.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_120.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_122.SetParent(g);
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_123.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_125.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_129.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_133.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_18 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_38 = parentGameObject;
		owner_Connection_44 = parentGameObject;
		owner_Connection_56 = parentGameObject;
		owner_Connection_58 = parentGameObject;
		owner_Connection_62 = parentGameObject;
		owner_Connection_68 = parentGameObject;
		owner_Connection_72 = parentGameObject;
		owner_Connection_81 = parentGameObject;
		owner_Connection_82 = parentGameObject;
		owner_Connection_90 = parentGameObject;
		owner_Connection_105 = parentGameObject;
		owner_Connection_108 = parentGameObject;
		owner_Connection_113 = parentGameObject;
		owner_Connection_115 = parentGameObject;
		owner_Connection_116 = parentGameObject;
		owner_Connection_121 = parentGameObject;
		owner_Connection_126 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_63.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_75.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out += SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out += SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Save_Out += SubGraph_SaveLoadBool_Save_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Load_Out += SubGraph_SaveLoadBool_Load_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_33;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.Save_Out += SubGraph_SaveLoadInt_Save_Out_35;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.Load_Out += SubGraph_SaveLoadInt_Load_Out_35;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_35;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output1 += uScriptCon_ManualSwitch_Output1_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output2 += uScriptCon_ManualSwitch_Output2_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output3 += uScriptCon_ManualSwitch_Output3_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output4 += uScriptCon_ManualSwitch_Output4_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output5 += uScriptCon_ManualSwitch_Output5_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output6 += uScriptCon_ManualSwitch_Output6_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output7 += uScriptCon_ManualSwitch_Output7_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output8 += uScriptCon_ManualSwitch_Output8_46;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.Out += SubGraph_CompleteObjectiveStage_Out_49;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_63.Out += SubGraph_CompleteObjectiveStage_Out_63;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.Out += SubGraph_CompleteObjectiveStage_Out_65;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_75.Out += SubGraph_LoadObjectiveStates_Out_75;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_63.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_75.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.OnEnable();
		logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_55.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_63.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_75.OnEnable();
		logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_89.OnEnable();
		logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_97.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_13.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_23.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_28.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_29.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_52.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_63.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_75.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_78.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_85.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_93.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_63.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_75.Update();
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_123.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_63.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_75.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out -= SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out -= SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Save_Out -= SubGraph_SaveLoadBool_Save_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Load_Out -= SubGraph_SaveLoadBool_Load_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_33;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.Save_Out -= SubGraph_SaveLoadInt_Save_Out_35;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.Load_Out -= SubGraph_SaveLoadInt_Load_Out_35;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_35;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output1 -= uScriptCon_ManualSwitch_Output1_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output2 -= uScriptCon_ManualSwitch_Output2_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output3 -= uScriptCon_ManualSwitch_Output3_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output4 -= uScriptCon_ManualSwitch_Output4_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output5 -= uScriptCon_ManualSwitch_Output5_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output6 -= uScriptCon_ManualSwitch_Output6_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output7 -= uScriptCon_ManualSwitch_Output7_46;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.Output8 -= uScriptCon_ManualSwitch_Output8_46;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.Out -= SubGraph_CompleteObjectiveStage_Out_49;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_63.Out -= SubGraph_CompleteObjectiveStage_Out_63;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.Out -= SubGraph_CompleteObjectiveStage_Out_65;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_75.Out -= SubGraph_LoadObjectiveStates_Out_75;
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

	private void Instance_SaveEvent_8(object o, EventArgs e)
	{
		Relay_SaveEvent_8();
	}

	private void Instance_LoadEvent_8(object o, EventArgs e)
	{
		Relay_LoadEvent_8();
	}

	private void Instance_RestartEvent_8(object o, EventArgs e)
	{
		Relay_RestartEvent_8();
	}

	private void SubGraph_SaveLoadBool_Save_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Save_Out_9();
	}

	private void SubGraph_SaveLoadBool_Load_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Load_Out_9();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Restart_Out_9();
	}

	private void SubGraph_SaveLoadBool_Save_Out_33(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_33;
		Relay_Save_Out_33();
	}

	private void SubGraph_SaveLoadBool_Load_Out_33(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_33;
		Relay_Load_Out_33();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_33(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_33;
		Relay_Restart_Out_33();
	}

	private void SubGraph_SaveLoadInt_Save_Out_35(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_35 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_35;
		Relay_Save_Out_35();
	}

	private void SubGraph_SaveLoadInt_Load_Out_35(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_35 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_35;
		Relay_Load_Out_35();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_35(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_35 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_35;
		Relay_Restart_Out_35();
	}

	private void uScriptCon_ManualSwitch_Output1_46(object o, EventArgs e)
	{
		Relay_Output1_46();
	}

	private void uScriptCon_ManualSwitch_Output2_46(object o, EventArgs e)
	{
		Relay_Output2_46();
	}

	private void uScriptCon_ManualSwitch_Output3_46(object o, EventArgs e)
	{
		Relay_Output3_46();
	}

	private void uScriptCon_ManualSwitch_Output4_46(object o, EventArgs e)
	{
		Relay_Output4_46();
	}

	private void uScriptCon_ManualSwitch_Output5_46(object o, EventArgs e)
	{
		Relay_Output5_46();
	}

	private void uScriptCon_ManualSwitch_Output6_46(object o, EventArgs e)
	{
		Relay_Output6_46();
	}

	private void uScriptCon_ManualSwitch_Output7_46(object o, EventArgs e)
	{
		Relay_Output7_46();
	}

	private void uScriptCon_ManualSwitch_Output8_46(object o, EventArgs e)
	{
		Relay_Output8_46();
	}

	private void SubGraph_CompleteObjectiveStage_Out_49(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_49 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_49;
		Relay_Out_49();
	}

	private void SubGraph_CompleteObjectiveStage_Out_63(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_63 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_63;
		Relay_Out_63();
	}

	private void SubGraph_CompleteObjectiveStage_Out_65(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_65 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_65;
		Relay_Out_65();
	}

	private void SubGraph_LoadObjectiveStates_Out_75(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_75();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_4();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_In_2()
	{
		logic_uScript_SpawnDeliveryCrate_positionName_2 = cratePosition;
		logic_uScript_SpawnDeliveryCrate_ownerNode_2 = owner_Connection_3;
		logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_2.In(logic_uScript_SpawnDeliveryCrate_positionName_2, logic_uScript_SpawnDeliveryCrate_ownerNode_2, logic_uScript_SpawnDeliveryCrate_visibleOnRadar_2);
		if (logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_2.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_4()
	{
		logic_uScriptCon_CompareBool_Bool_4 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.In(logic_uScriptCon_CompareBool_Bool_4);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.False;
		if (num)
		{
			Relay_In_43();
		}
		if (flag)
		{
			Relay_True_6();
		}
	}

	private void Relay_True_6()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_6.True(out logic_uScriptAct_SetBool_Target_6);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_6;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_6.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_False_6()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_6.False(out logic_uScriptAct_SetBool_Target_6);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_6;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_6.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_SaveEvent_8()
	{
		Relay_Save_9();
	}

	private void Relay_LoadEvent_8()
	{
		Relay_Load_9();
	}

	private void Relay_RestartEvent_8()
	{
		Relay_Set_False_9();
	}

	private void Relay_Save_Out_9()
	{
		Relay_Save_33();
	}

	private void Relay_Load_Out_9()
	{
		Relay_Load_33();
	}

	private void Relay_Restart_Out_9()
	{
		Relay_Set_False_33();
	}

	private void Relay_Save_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Load_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Set_True_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Set_False_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_In_13()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_13 = owner_Connection_14;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_13.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_13);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_13.True)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_15()
	{
		logic_uScriptCon_CompareBool_Bool_15 = local_EnemySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.In(logic_uScriptCon_CompareBool_Bool_15);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.False;
		if (num)
		{
			Relay_In_20();
		}
		if (flag)
		{
			Relay_In_132();
		}
	}

	private void Relay_InitialSpawn_17()
	{
		int num = 0;
		Array array = enemySpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_17.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_17, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_17, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_17 = owner_Connection_18;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_17.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_17, logic_uScript_SpawnTechsFromData_ownerNode_17, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_17, logic_uScript_SpawnTechsFromData_allowResurrection_17);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_17.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_20()
	{
		int num = 0;
		Array array = enemySpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_20.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_20, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_20, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_20 = owner_Connection_22;
		int num2 = 0;
		Array array2 = local_EnemyTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_20.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_20, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_20, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_20 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20.In(logic_uScript_GetAndCheckTechs_techData_20, logic_uScript_GetAndCheckTechs_ownerNode_20, ref logic_uScript_GetAndCheckTechs_techs_20);
		local_EnemyTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_20;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20.AllDead;
		if (allAlive)
		{
			Relay_In_78();
		}
		if (someAlive)
		{
			Relay_In_78();
		}
		if (allDead)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_23()
	{
		logic_uScript_AddMessage_messageData_23 = msgEnteringArea;
		logic_uScript_AddMessage_speaker_23 = messageSpeaker;
		logic_uScript_AddMessage_Return_23 = logic_uScript_AddMessage_uScript_AddMessage_23.In(logic_uScript_AddMessage_messageData_23, logic_uScript_AddMessage_speaker_23);
		if (logic_uScript_AddMessage_uScript_AddMessage_23.Out)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_AddMessage_messageData_28 = msgEnemyLanding;
		logic_uScript_AddMessage_speaker_28 = messageSpeaker;
		logic_uScript_AddMessage_Return_28 = logic_uScript_AddMessage_uScript_AddMessage_28.In(logic_uScript_AddMessage_messageData_28, logic_uScript_AddMessage_speaker_28);
		if (logic_uScript_AddMessage_uScript_AddMessage_28.Out)
		{
			Relay_In_125();
		}
	}

	private void Relay_In_29()
	{
		logic_uScript_AddMessage_messageData_29 = msgEnemyDestroyed;
		logic_uScript_AddMessage_speaker_29 = messageSpeaker;
		logic_uScript_AddMessage_Return_29 = logic_uScript_AddMessage_uScript_AddMessage_29.In(logic_uScript_AddMessage_messageData_29, logic_uScript_AddMessage_speaker_29);
		if (logic_uScript_AddMessage_uScript_AddMessage_29.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_Save_Out_33()
	{
		Relay_Save_35();
	}

	private void Relay_Load_Out_33()
	{
		Relay_Load_35();
	}

	private void Relay_Restart_Out_33()
	{
		Relay_Restart_35();
	}

	private void Relay_Save_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Save(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Load_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Load(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Set_True_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Set_False_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Save_Out_35()
	{
	}

	private void Relay_Load_Out_35()
	{
		Relay_In_75();
	}

	private void Relay_Restart_Out_35()
	{
	}

	private void Relay_Save_35()
	{
		logic_SubGraph_SaveLoadInt_integer_35 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_35 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.Save(logic_SubGraph_SaveLoadInt_restartValue_35, ref logic_SubGraph_SaveLoadInt_integer_35, logic_SubGraph_SaveLoadInt_intAsVariable_35, logic_SubGraph_SaveLoadInt_uniqueID_35);
	}

	private void Relay_Load_35()
	{
		logic_SubGraph_SaveLoadInt_integer_35 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_35 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.Load(logic_SubGraph_SaveLoadInt_restartValue_35, ref logic_SubGraph_SaveLoadInt_integer_35, logic_SubGraph_SaveLoadInt_intAsVariable_35, logic_SubGraph_SaveLoadInt_uniqueID_35);
	}

	private void Relay_Restart_35()
	{
		logic_SubGraph_SaveLoadInt_integer_35 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_35 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_35.Restart(logic_SubGraph_SaveLoadInt_restartValue_35, ref logic_SubGraph_SaveLoadInt_integer_35, logic_SubGraph_SaveLoadInt_intAsVariable_35, logic_SubGraph_SaveLoadInt_uniqueID_35);
	}

	private void Relay_In_37()
	{
		logic_uScript_SetEncounterRotation_ownerNode_37 = owner_Connection_38;
		logic_uScript_SetEncounterRotation_rotation_37 = local_42_UnityEngine_Quaternion;
		logic_uScript_SetEncounterRotation_uScript_SetEncounterRotation_37.In(logic_uScript_SetEncounterRotation_ownerNode_37, logic_uScript_SetEncounterRotation_rotation_37);
		if (logic_uScript_SetEncounterRotation_uScript_SetEncounterRotation_37.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_39()
	{
		logic_uScript_GetTechRotation_tech_39 = local_41_Tank;
		logic_uScript_GetTechRotation_Return_39 = logic_uScript_GetTechRotation_uScript_GetTechRotation_39.In(logic_uScript_GetTechRotation_tech_39);
		local_42_UnityEngine_Quaternion = logic_uScript_GetTechRotation_Return_39;
		if (logic_uScript_GetTechRotation_uScript_GetTechRotation_39.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_40()
	{
		logic_uScript_GetPlayerTank_Return_40 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_40.In();
		local_41_Tank = logic_uScript_GetPlayerTank_Return_40;
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_40.Returned)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_43()
	{
		logic_uScript_RemoveScenery_ownerNode_43 = owner_Connection_44;
		logic_uScript_RemoveScenery_positionName_43 = cratePosition;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_43.In(logic_uScript_RemoveScenery_ownerNode_43, logic_uScript_RemoveScenery_positionName_43, logic_uScript_RemoveScenery_radius_43, logic_uScript_RemoveScenery_preventChunksSpawning_43);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_43.Out)
		{
			Relay_In_120();
		}
	}

	private void Relay_Output1_46()
	{
		Relay_In_13();
	}

	private void Relay_Output2_46()
	{
		Relay_TimeRemaining_111();
	}

	private void Relay_Output3_46()
	{
		Relay_In_15();
	}

	private void Relay_Output4_46()
	{
		Relay_In_89();
	}

	private void Relay_Output5_46()
	{
	}

	private void Relay_Output6_46()
	{
	}

	private void Relay_Output7_46()
	{
	}

	private void Relay_Output8_46()
	{
	}

	private void Relay_In_46()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_46 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_46.In(logic_uScriptCon_ManualSwitch_CurrentOutput_46);
	}

	private void Relay_In_48()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_Out_49()
	{
	}

	private void Relay_In_49()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_49 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_49, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_49);
	}

	private void Relay_In_52()
	{
		logic_uScript_AddMessage_messageData_52 = msgCrateUnlocked;
		logic_uScript_AddMessage_speaker_52 = messageSpeaker;
		logic_uScript_AddMessage_Return_52 = logic_uScript_AddMessage_uScript_AddMessage_52.In(logic_uScript_AddMessage_messageData_52, logic_uScript_AddMessage_speaker_52);
		if (logic_uScript_AddMessage_uScript_AddMessage_52.Out)
		{
			Relay_Succeed_57();
		}
	}

	private void Relay_In_55()
	{
		logic_uScript_CheckDeliveryCrateSpawned_ownerNode_55 = owner_Connection_62;
		logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_55.In(logic_uScript_CheckDeliveryCrateSpawned_ownerNode_55);
		if (logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_55.Yes)
		{
			Relay_In_60();
		}
	}

	private void Relay_Succeed_57()
	{
		logic_uScript_FinishEncounter_owner_57 = owner_Connection_56;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_57.Succeed(logic_uScript_FinishEncounter_owner_57);
	}

	private void Relay_Fail_57()
	{
		logic_uScript_FinishEncounter_owner_57 = owner_Connection_56;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_57.Fail(logic_uScript_FinishEncounter_owner_57);
	}

	private void Relay_In_60()
	{
		logic_uScript_UnlockDeliveryCrate_ownerNode_60 = owner_Connection_58;
		logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_60.In(logic_uScript_UnlockDeliveryCrate_ownerNode_60);
		if (logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_60.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_61()
	{
		logic_uScriptCon_CompareBool_Bool_61 = local_EnemySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61.In(logic_uScriptCon_CompareBool_Bool_61);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61.False;
		if (num)
		{
			Relay_Succeed_57();
		}
		if (flag)
		{
			Relay_In_52();
		}
	}

	private void Relay_Out_63()
	{
	}

	private void Relay_In_63()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_63 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_63.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_63, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_63);
	}

	private void Relay_Out_65()
	{
	}

	private void Relay_In_65()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_65 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_65.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_65, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_65);
	}

	private void Relay_MarkCompleted_67()
	{
		logic_uScript_SetQuestObjectiveCompleted_owner_67 = owner_Connection_68;
		logic_uScript_SetQuestObjectiveCompleted_objectiveId_67 = local_Stage_System_Int32;
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_67.MarkCompleted(logic_uScript_SetQuestObjectiveCompleted_owner_67, logic_uScript_SetQuestObjectiveCompleted_objectiveId_67, logic_uScript_SetQuestObjectiveCompleted_completed_67);
		if (logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_67.Out)
		{
			Relay_SetVisible_69();
		}
	}

	private void Relay_SetVisible_69()
	{
		logic_uScript_SetQuestObjectiveVisible_owner_69 = owner_Connection_72;
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_69.SetVisible(logic_uScript_SetQuestObjectiveVisible_owner_69, logic_uScript_SetQuestObjectiveVisible_objectiveId_69, logic_uScript_SetQuestObjectiveVisible_visible_69);
		if (logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_69.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_73()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_73.In(logic_uScriptAct_SetInt_Value_73, out logic_uScriptAct_SetInt_Target_73);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_73;
	}

	private void Relay_Out_75()
	{
	}

	private void Relay_In_75()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_75 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_75.In(logic_SubGraph_LoadObjectiveStates_currentObjective_75);
	}

	private void Relay_In_78()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_78 = owner_Connection_81;
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_78.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_78, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_78, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_78 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_78.In(logic_uScript_SetOneTechAsEncounterTarget_owner_78, logic_uScript_SetOneTechAsEncounterTarget_techs_78);
	}

	private void Relay_In_85()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_85 = local_Crate_Crate;
		logic_uScript_IsPlayerInRangeOfVisible_range_85 = local_CrateOpenRange_System_Single;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_85.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_85, logic_uScript_IsPlayerInRangeOfVisible_range_85);
		if (logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_85.InRange)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_86()
	{
		logic_uScript_GetCrateOpenTriggerRange_crate_86 = local_Crate_Crate;
		logic_uScript_GetCrateOpenTriggerRange_Return_86 = logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_86.In(logic_uScript_GetCrateOpenTriggerRange_crate_86);
		local_CrateOpenRange_System_Single = logic_uScript_GetCrateOpenTriggerRange_Return_86;
		if (logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_86.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_89()
	{
		logic_uScript_GetDeliveryCrate_ownerNode_89 = owner_Connection_82;
		logic_uScript_GetDeliveryCrate_Return_89 = logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_89.In(logic_uScript_GetDeliveryCrate_ownerNode_89);
		local_Crate_Crate = logic_uScript_GetDeliveryCrate_Return_89;
		if (logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_89.Success)
		{
			Relay_In_104();
		}
	}

	private void Relay_In_93()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_93 = local_Crate_Crate;
		logic_uScript_IsPlayerInRangeOfVisible_range_93 = local_CrateOpenRange_System_Single;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_93.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_93, logic_uScript_IsPlayerInRangeOfVisible_range_93);
		if (logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_93.InRange)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_94()
	{
		logic_uScript_GetCrateOpenTriggerRange_crate_94 = local_Crate_Crate;
		logic_uScript_GetCrateOpenTriggerRange_Return_94 = logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_94.In(logic_uScript_GetCrateOpenTriggerRange_crate_94);
		local_CrateOpenRange_System_Single = logic_uScript_GetCrateOpenTriggerRange_Return_94;
		if (logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_94.Out)
		{
			Relay_In_93();
		}
	}

	private void Relay_In_97()
	{
		logic_uScript_GetDeliveryCrate_ownerNode_97 = owner_Connection_90;
		logic_uScript_GetDeliveryCrate_Return_97 = logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_97.In(logic_uScript_GetDeliveryCrate_ownerNode_97);
		local_Crate_Crate = logic_uScript_GetDeliveryCrate_Return_97;
		if (logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_97.Success)
		{
			Relay_In_94();
		}
	}

	private void Relay_In_104()
	{
		logic_uScript_SetEncounterTarget_owner_104 = owner_Connection_105;
		logic_uScript_SetEncounterTarget_visibleObject_104 = local_Crate_Crate;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_104.In(logic_uScript_SetEncounterTarget_owner_104, logic_uScript_SetEncounterTarget_visibleObject_104);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_104.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_109()
	{
		logic_uScriptCon_CompareFloat_A_109 = local_TimerCount_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_109.In(logic_uScriptCon_CompareFloat_A_109, logic_uScriptCon_CompareFloat_B_109);
		bool greaterThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_109.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_109.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_97();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_117();
		}
	}

	private void Relay_TimeRemaining_111()
	{
		logic_uScript_GetEncounterTimeRemaining_owner_111 = owner_Connection_108;
		logic_uScript_GetEncounterTimeRemaining_Return_111 = logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_111.TimeRemaining(logic_uScript_GetEncounterTimeRemaining_owner_111);
		local_TimerCount_System_Single = logic_uScript_GetEncounterTimeRemaining_Return_111;
		if (logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_111.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_TimeRemainingPercent_111()
	{
		logic_uScript_GetEncounterTimeRemaining_owner_111 = owner_Connection_108;
		logic_uScript_GetEncounterTimeRemaining_Return_111 = logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_111.TimeRemainingPercent(logic_uScript_GetEncounterTimeRemaining_owner_111);
		local_TimerCount_System_Single = logic_uScript_GetEncounterTimeRemaining_Return_111;
		if (logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_111.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_112()
	{
		logic_uScript_StartEncounterTimer_owner_112 = owner_Connection_113;
		logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_112.In(logic_uScript_StartEncounterTimer_owner_112);
		if (logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_112.Out)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_114()
	{
		logic_uScript_RemoveEncounterTimer_owner_114 = owner_Connection_115;
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_114.In(logic_uScript_RemoveEncounterTimer_owner_114);
		if (logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_114.Out)
		{
			Relay_MarkCompleted_67();
		}
	}

	private void Relay_In_117()
	{
		logic_uScript_RemoveEncounterTimer_owner_117 = owner_Connection_116;
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_117.In(logic_uScript_RemoveEncounterTimer_owner_117);
		if (logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_117.Out)
		{
			Relay_In_63();
		}
	}

	private void Relay_In_120()
	{
		logic_uScript_RemoveScenery_ownerNode_120 = owner_Connection_121;
		logic_uScript_RemoveScenery_positionName_120 = enemyPosition;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_120.In(logic_uScript_RemoveScenery_ownerNode_120, logic_uScript_RemoveScenery_positionName_120, logic_uScript_RemoveScenery_radius_120, logic_uScript_RemoveScenery_preventChunksSpawning_120);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_120.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_In_122()
	{
		logic_uScriptCon_CompareFloat_A_122 = local_124_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_122.In(logic_uScriptCon_CompareFloat_A_122, logic_uScriptCon_CompareFloat_B_122);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_122.GreaterThan)
		{
			Relay_In_29();
		}
	}

	private void Relay_StartTimer_123()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_123.StartTimer(out logic_uScriptAct_Stopwatch_Seconds_123);
		local_124_System_Single = logic_uScriptAct_Stopwatch_Seconds_123;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_123.Started)
		{
			Relay_In_122();
		}
	}

	private void Relay_Stop_123()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_123.Stop(out logic_uScriptAct_Stopwatch_Seconds_123);
		local_124_System_Single = logic_uScriptAct_Stopwatch_Seconds_123;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_123.Started)
		{
			Relay_In_122();
		}
	}

	private void Relay_ResetTimer_123()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_123.ResetTimer(out logic_uScriptAct_Stopwatch_Seconds_123);
		local_124_System_Single = logic_uScriptAct_Stopwatch_Seconds_123;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_123.Started)
		{
			Relay_In_122();
		}
	}

	private void Relay_CheckTime_123()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_123.CheckTime(out logic_uScriptAct_Stopwatch_Seconds_123);
		local_124_System_Single = logic_uScriptAct_Stopwatch_Seconds_123;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_123.Started)
		{
			Relay_In_122();
		}
	}

	private void Relay_In_125()
	{
		int num = 0;
		Array array = enemySpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_125.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_125, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_125, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_125 = owner_Connection_126;
		int num2 = 0;
		Array array2 = local_EnemyTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_125.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_125, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_125, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_125 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_125.In(logic_uScript_GetAndCheckTechs_techData_125, logic_uScript_GetAndCheckTechs_ownerNode_125, ref logic_uScript_GetAndCheckTechs_techs_125);
		local_EnemyTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_125;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_125.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_125.SomeAlive;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_125.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_129();
		}
		if (someAlive)
		{
			Relay_True_129();
		}
		if (waitingToSpawn)
		{
			Relay_StartTimer_123();
		}
	}

	private void Relay_True_129()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_129.True(out logic_uScriptAct_SetBool_Target_129);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_129;
	}

	private void Relay_False_129()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_129.False(out logic_uScriptAct_SetBool_Target_129);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_129;
	}

	private void Relay_In_132()
	{
		logic_uScriptCon_CompareBool_Bool_132 = local_EnemySpawnStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132.In(logic_uScriptCon_CompareBool_Bool_132);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132.False;
		if (num)
		{
			Relay_In_125();
		}
		if (flag)
		{
			Relay_True_133();
		}
	}

	private void Relay_True_133()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_133.True(out logic_uScriptAct_SetBool_Target_133);
		local_EnemySpawnStarted_System_Boolean = logic_uScriptAct_SetBool_Target_133;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_133.Out)
		{
			Relay_InitialSpawn_17();
		}
	}

	private void Relay_False_133()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_133.False(out logic_uScriptAct_SetBool_Target_133);
		local_EnemySpawnStarted_System_Boolean = logic_uScriptAct_SetBool_Target_133;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_133.Out)
		{
			Relay_InitialSpawn_17();
		}
	}
}
