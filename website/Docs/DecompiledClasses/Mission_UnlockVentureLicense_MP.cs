using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_UnlockVentureLicense_MP : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public float clearSceneryRadius = 50f;

	public float distLeavingMission;

	public float distNearNPC;

	public float distNearTradingStation;

	private Tank[] local_26_TankArray = new Tank[0];

	private bool local_ChallengeEnded_System_Boolean;

	private bool local_ChallengeInProgess_System_Boolean;

	private Tank local_GSOVendor_Tank;

	private bool local_Init_System_Boolean;

	private bool local_MissionComplete_System_Boolean;

	private bool local_MsgHalfwayRound_System_Boolean;

	private bool local_MsgIntro_System_Boolean;

	private bool local_MsgNPCFound_System_Boolean;

	private Tank local_NPCTech_Tank;

	private int local_Stage_System_Int32 = 1;

	private float local_TimeRemaining_System_Single;

	private Vector3 local_VendorPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msgComplete;

	public uScript_AddMessage.MessageData msgIntro;

	public uScript_AddMessage.MessageData msgNPCFound;

	public uScript_AddMessage.MessageData msgOutOfTime;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	[Multiline(3)]
	public string raceStartPosition = "";

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_24;

	private GameObject owner_Connection_68;

	private GameObject owner_Connection_71;

	private GameObject owner_Connection_87;

	private GameObject owner_Connection_99;

	private GameObject owner_Connection_110;

	private GameObject owner_Connection_113;

	private GameObject owner_Connection_117;

	private GameObject owner_Connection_121;

	private GameObject owner_Connection_127;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_2;

	private bool logic_uScriptCon_CompareBool_True_2 = true;

	private bool logic_uScriptCon_CompareBool_False_2 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_4 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_4;

	private bool logic_uScriptAct_SetBool_Out_4 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_4 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_4 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_7;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_7 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_7 = "Init";

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_12 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_12;

	private string logic_uScript_RemoveScenery_positionName_12 = "";

	private float logic_uScript_RemoveScenery_radius_12;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_12 = true;

	private bool logic_uScript_RemoveScenery_Out_12 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_13 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_13;

	private bool logic_uScript_FinishEncounter_Out_13 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_16 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_16;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_16;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_16;

	private bool logic_uScript_AddMessage_Out_16 = true;

	private bool logic_uScript_AddMessage_Shown_16 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_18 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_18;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_18;

	private bool logic_uScript_SpawnTechsFromData_Out_18 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_21;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_25 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_25 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_25;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_25 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_25;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_25 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_25 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_25 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_25 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_28 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_28 = new Tank[0];

	private int logic_uScript_AccessListTech_index_28;

	private Tank logic_uScript_AccessListTech_value_28;

	private bool logic_uScript_AccessListTech_Out_28 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_31 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_31;

	private float logic_uScript_IsPlayerInRangeOfTech_range_31;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_31 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_31 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_31 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_31 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_32 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_32;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_32;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_32;

	private bool logic_uScript_AddMessage_Out_32 = true;

	private bool logic_uScript_AddMessage_Shown_32 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_33 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_33;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_33;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_38;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_38 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_38 = "MsgIntro";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_39 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_39;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_39 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_39 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_41 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_41;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_44;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_44 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_44 = "MsgHalfwayRound";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_45;

	private bool logic_uScriptCon_CompareBool_True_45 = true;

	private bool logic_uScriptCon_CompareBool_False_45 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_51;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_51 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_51 = "ChallengeEnded";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_52;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_52 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_52 = "MissionComplete";

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_55 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_55;

	private float logic_uScript_IsPlayerInRangeOfTech_range_55;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_55 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_55 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_55 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_55 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_57;

	private bool logic_uScriptCon_CompareBool_True_57 = true;

	private bool logic_uScriptCon_CompareBool_False_57 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_59 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_59 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_59 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_59 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_61 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_61;

	private bool logic_uScriptAct_SetBool_Out_61 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_61 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_61 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_63;

	private bool logic_uScriptCon_CompareBool_True_63 = true;

	private bool logic_uScriptCon_CompareBool_False_63 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_65;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_65 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_65 = "MsgNPCFound";

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_66 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_66 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_66;

	private bool logic_uScript_SetTankInvulnerable_Out_66 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_69 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_69;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_69 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_70 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_70;

	private object logic_uScript_SetEncounterTarget_visibleObject_70 = "";

	private bool logic_uScript_SetEncounterTarget_Out_70 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_75 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_75;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_75;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_75;

	private bool logic_uScript_AddMessage_Out_75 = true;

	private bool logic_uScript_AddMessage_Shown_75 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_77 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_77;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_77 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_77 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_77;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_77;

	private bool logic_uScript_FlyTechUpAndAway_Out_77 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_80 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_80;

	private bool logic_uScriptAct_SetBool_Out_80 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_80 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_80 = true;

	private uScript_SetEncounterPosition logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_83 = new uScript_SetEncounterPosition();

	private GameObject logic_uScript_SetEncounterPosition_ownerNode_83;

	private Vector3 logic_uScript_SetEncounterPosition_position_83;

	private bool logic_uScript_SetEncounterPosition_Out_83 = true;

	private uScript_GetNearestVendorPos logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_84 = new uScript_GetNearestVendorPos();

	private Vector3 logic_uScript_GetNearestVendorPos_Return_84;

	private bool logic_uScript_GetNearestVendorPos_Out_84 = true;

	private bool logic_uScript_GetNearestVendorPos_Found_84 = true;

	private bool logic_uScript_GetNearestVendorPos_Missing_84 = true;

	private uScript_FindNearestVendor logic_uScript_FindNearestVendor_uScript_FindNearestVendor_88 = new uScript_FindNearestVendor();

	private Tank logic_uScript_FindNearestVendor_Return_88;

	private bool logic_uScript_FindNearestVendor_Out_88 = true;

	private bool logic_uScript_FindNearestVendor_Returned_88 = true;

	private bool logic_uScript_FindNearestVendor_NotReturned_88 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_93 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_93;

	private bool logic_uScriptAct_SetBool_Out_93 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_93 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_93 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_94 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_94;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_94;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_94;

	private bool logic_uScript_AddMessage_Out_94 = true;

	private bool logic_uScript_AddMessage_Shown_94 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_95;

	private bool logic_uScriptCon_CompareBool_True_95 = true;

	private bool logic_uScriptCon_CompareBool_False_95 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_97 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_97;

	private bool logic_uScriptAct_SetBool_Out_97 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_97 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_97 = true;

	private uScript_StartEncounterTimer logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_98 = new uScript_StartEncounterTimer();

	private GameObject logic_uScript_StartEncounterTimer_owner_98;

	private bool logic_uScript_StartEncounterTimer_Out_98 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_101;

	private bool logic_uScriptCon_CompareBool_True_101 = true;

	private bool logic_uScriptCon_CompareBool_False_101 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_103 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_103;

	private float logic_uScript_IsPlayerInRangeOfTech_range_103;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_103 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_103 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_103 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_103 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_105 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_105;

	private bool logic_uScriptAct_SetBool_Out_105 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_105 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_105 = true;

	private uScript_RemoveEncounterTimer logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_109 = new uScript_RemoveEncounterTimer();

	private GameObject logic_uScript_RemoveEncounterTimer_owner_109;

	private bool logic_uScript_RemoveEncounterTimer_Out_109 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_111 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_111;

	private object logic_uScript_SetEncounterTarget_visibleObject_111 = "";

	private bool logic_uScript_SetEncounterTarget_Out_111 = true;

	private uScript_GetEncounterTimeRemaining logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_116 = new uScript_GetEncounterTimeRemaining();

	private GameObject logic_uScript_GetEncounterTimeRemaining_owner_116;

	private float logic_uScript_GetEncounterTimeRemaining_Return_116;

	private bool logic_uScript_GetEncounterTimeRemaining_Out_116 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_119 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_119;

	private float logic_uScriptCon_CompareFloat_B_119;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_119 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_119 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_119 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_119 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_119 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_119 = true;

	private uScript_SetQuestObjectiveCompleted logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_120 = new uScript_SetQuestObjectiveCompleted();

	private GameObject logic_uScript_SetQuestObjectiveCompleted_owner_120;

	private int logic_uScript_SetQuestObjectiveCompleted_objectiveId_120;

	private bool logic_uScript_SetQuestObjectiveCompleted_completed_120;

	private bool logic_uScript_SetQuestObjectiveCompleted_Out_120 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_125 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_125;

	private bool logic_uScriptAct_SetBool_Out_125 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_125 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_125 = true;

	private uScript_RemoveEncounterTimer logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_128 = new uScript_RemoveEncounterTimer();

	private GameObject logic_uScript_RemoveEncounterTimer_owner_128;

	private bool logic_uScript_RemoveEncounterTimer_Out_128 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_129 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_129 = 1;

	private int logic_uScriptAct_SetInt_Target_129;

	private bool logic_uScriptAct_SetInt_Out_129 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
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
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_1;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_1;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_1;
				}
			}
		}
		if (null == owner_Connection_5 || !m_RegisteredForEvents)
		{
			owner_Connection_5 = parentGameObject;
			if (null != owner_Connection_5)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_5.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_5.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
				}
			}
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (null == owner_Connection_24 || !m_RegisteredForEvents)
		{
			owner_Connection_24 = parentGameObject;
		}
		if (null == owner_Connection_68 || !m_RegisteredForEvents)
		{
			owner_Connection_68 = parentGameObject;
		}
		if (null == owner_Connection_71 || !m_RegisteredForEvents)
		{
			owner_Connection_71 = parentGameObject;
		}
		if (null == owner_Connection_87 || !m_RegisteredForEvents)
		{
			owner_Connection_87 = parentGameObject;
		}
		if (null == owner_Connection_99 || !m_RegisteredForEvents)
		{
			owner_Connection_99 = parentGameObject;
		}
		if (null == owner_Connection_110 || !m_RegisteredForEvents)
		{
			owner_Connection_110 = parentGameObject;
		}
		if (null == owner_Connection_113 || !m_RegisteredForEvents)
		{
			owner_Connection_113 = parentGameObject;
		}
		if (null == owner_Connection_117 || !m_RegisteredForEvents)
		{
			owner_Connection_117 = parentGameObject;
		}
		if (null == owner_Connection_121 || !m_RegisteredForEvents)
		{
			owner_Connection_121 = parentGameObject;
		}
		if (null == owner_Connection_127 || !m_RegisteredForEvents)
		{
			owner_Connection_127 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_3)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_3.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_3.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_1;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_1;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_1;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_5)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_5.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_5.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_3)
		{
			uScript_SaveLoad component = owner_Connection_3.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_1;
				component.LoadEvent -= Instance_LoadEvent_1;
				component.RestartEvent -= Instance_RestartEvent_1;
			}
		}
		if (null != owner_Connection_5)
		{
			uScript_EncounterUpdate component2 = owner_Connection_5.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_0;
				component2.OnSuspend -= Instance_OnSuspend_0;
				component2.OnResume -= Instance_OnResume_0;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_12.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_13.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_16.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_25.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_28.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_31.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_32.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_33.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_41.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_55.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_59.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_61.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_66.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_69.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_70.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_75.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_77.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.SetParent(g);
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_83.SetParent(g);
		logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_84.SetParent(g);
		logic_uScript_FindNearestVendor_uScript_FindNearestVendor_88.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_94.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.SetParent(g);
		logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_98.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_103.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_105.SetParent(g);
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_109.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_111.SetParent(g);
		logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_116.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_119.SetParent(g);
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_120.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.SetParent(g);
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_128.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_129.SetParent(g);
		owner_Connection_3 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_24 = parentGameObject;
		owner_Connection_68 = parentGameObject;
		owner_Connection_71 = parentGameObject;
		owner_Connection_87 = parentGameObject;
		owner_Connection_99 = parentGameObject;
		owner_Connection_110 = parentGameObject;
		owner_Connection_113 = parentGameObject;
		owner_Connection_117 = parentGameObject;
		owner_Connection_121 = parentGameObject;
		owner_Connection_127 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_33.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_41.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output1 += uScriptCon_ManualSwitch_Output1_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output2 += uScriptCon_ManualSwitch_Output2_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output3 += uScriptCon_ManualSwitch_Output3_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output4 += uScriptCon_ManualSwitch_Output4_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output5 += uScriptCon_ManualSwitch_Output5_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output6 += uScriptCon_ManualSwitch_Output6_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output7 += uScriptCon_ManualSwitch_Output7_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output8 += uScriptCon_ManualSwitch_Output8_21;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_33.Out += SubGraph_CompleteObjectiveStage_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save_Out += SubGraph_SaveLoadBool_Save_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load_Out += SubGraph_SaveLoadBool_Load_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_38;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Save_Out += SubGraph_SaveLoadInt_Save_Out_39;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Load_Out += SubGraph_SaveLoadInt_Load_Out_39;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_39;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_41.Out += SubGraph_LoadObjectiveStates_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save_Out += SubGraph_SaveLoadBool_Save_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load_Out += SubGraph_SaveLoadBool_Load_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Save_Out += SubGraph_SaveLoadBool_Save_Out_51;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Load_Out += SubGraph_SaveLoadBool_Load_Out_51;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_51;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Save_Out += SubGraph_SaveLoadBool_Save_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Load_Out += SubGraph_SaveLoadBool_Load_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Save_Out += SubGraph_SaveLoadBool_Save_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Load_Out += SubGraph_SaveLoadBool_Load_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_65;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_33.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_41.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_33.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_41.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_69.OnEnable();
		logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_84.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_16.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_31.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_32.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_33.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_41.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_55.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_66.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_75.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_94.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_103.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_33.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_41.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_33.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_41.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output1 -= uScriptCon_ManualSwitch_Output1_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output2 -= uScriptCon_ManualSwitch_Output2_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output3 -= uScriptCon_ManualSwitch_Output3_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output4 -= uScriptCon_ManualSwitch_Output4_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output5 -= uScriptCon_ManualSwitch_Output5_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output6 -= uScriptCon_ManualSwitch_Output6_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output7 -= uScriptCon_ManualSwitch_Output7_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output8 -= uScriptCon_ManualSwitch_Output8_21;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_33.Out -= SubGraph_CompleteObjectiveStage_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save_Out -= SubGraph_SaveLoadBool_Save_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load_Out -= SubGraph_SaveLoadBool_Load_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_38;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Save_Out -= SubGraph_SaveLoadInt_Save_Out_39;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Load_Out -= SubGraph_SaveLoadInt_Load_Out_39;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_39.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_39;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_41.Out -= SubGraph_LoadObjectiveStates_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save_Out -= SubGraph_SaveLoadBool_Save_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load_Out -= SubGraph_SaveLoadBool_Load_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Save_Out -= SubGraph_SaveLoadBool_Save_Out_51;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Load_Out -= SubGraph_SaveLoadBool_Load_Out_51;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_51;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Save_Out -= SubGraph_SaveLoadBool_Save_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Load_Out -= SubGraph_SaveLoadBool_Load_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Save_Out -= SubGraph_SaveLoadBool_Save_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Load_Out -= SubGraph_SaveLoadBool_Load_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_65;
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

	private void uScriptCon_ManualSwitch_Output1_21(object o, EventArgs e)
	{
		Relay_Output1_21();
	}

	private void uScriptCon_ManualSwitch_Output2_21(object o, EventArgs e)
	{
		Relay_Output2_21();
	}

	private void uScriptCon_ManualSwitch_Output3_21(object o, EventArgs e)
	{
		Relay_Output3_21();
	}

	private void uScriptCon_ManualSwitch_Output4_21(object o, EventArgs e)
	{
		Relay_Output4_21();
	}

	private void uScriptCon_ManualSwitch_Output5_21(object o, EventArgs e)
	{
		Relay_Output5_21();
	}

	private void uScriptCon_ManualSwitch_Output6_21(object o, EventArgs e)
	{
		Relay_Output6_21();
	}

	private void uScriptCon_ManualSwitch_Output7_21(object o, EventArgs e)
	{
		Relay_Output7_21();
	}

	private void uScriptCon_ManualSwitch_Output8_21(object o, EventArgs e)
	{
		Relay_Output8_21();
	}

	private void SubGraph_CompleteObjectiveStage_Out_33(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_33 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_33;
		Relay_Out_33();
	}

	private void SubGraph_SaveLoadBool_Save_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_MsgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Save_Out_38();
	}

	private void SubGraph_SaveLoadBool_Load_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_MsgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Load_Out_38();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_MsgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Restart_Out_38();
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

	private void SubGraph_LoadObjectiveStates_Out_41(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_41();
	}

	private void SubGraph_SaveLoadBool_Save_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_MsgHalfwayRound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Save_Out_44();
	}

	private void SubGraph_SaveLoadBool_Load_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_MsgHalfwayRound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Load_Out_44();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_MsgHalfwayRound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Restart_Out_44();
	}

	private void SubGraph_SaveLoadBool_Save_Out_51(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_51 = e.boolean;
		local_ChallengeEnded_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_51;
		Relay_Save_Out_51();
	}

	private void SubGraph_SaveLoadBool_Load_Out_51(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_51 = e.boolean;
		local_ChallengeEnded_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_51;
		Relay_Load_Out_51();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_51(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_51 = e.boolean;
		local_ChallengeEnded_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_51;
		Relay_Restart_Out_51();
	}

	private void SubGraph_SaveLoadBool_Save_Out_52(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = e.boolean;
		local_MissionComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_52;
		Relay_Save_Out_52();
	}

	private void SubGraph_SaveLoadBool_Load_Out_52(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = e.boolean;
		local_MissionComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_52;
		Relay_Load_Out_52();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_52(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = e.boolean;
		local_MissionComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_52;
		Relay_Restart_Out_52();
	}

	private void SubGraph_SaveLoadBool_Save_Out_65(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = e.boolean;
		local_MsgNPCFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_65;
		Relay_Save_Out_65();
	}

	private void SubGraph_SaveLoadBool_Load_Out_65(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = e.boolean;
		local_MsgNPCFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_65;
		Relay_Load_Out_65();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_65(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = e.boolean;
		local_MsgNPCFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_65;
		Relay_Restart_Out_65();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_2();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
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

	private void Relay_In_2()
	{
		logic_uScriptCon_CompareBool_Bool_2 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.In(logic_uScriptCon_CompareBool_Bool_2);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.False;
		if (num)
		{
			Relay_In_25();
		}
		if (flag)
		{
			Relay_True_4();
		}
	}

	private void Relay_True_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.True(out logic_uScriptAct_SetBool_Target_4);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_False_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.False(out logic_uScriptAct_SetBool_Target_4);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_Save_Out_7()
	{
		Relay_Save_38();
	}

	private void Relay_Load_Out_7()
	{
		Relay_Load_38();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_Set_False_38();
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

	private void Relay_In_12()
	{
		logic_uScript_RemoveScenery_ownerNode_12 = owner_Connection_11;
		logic_uScript_RemoveScenery_positionName_12 = raceStartPosition;
		logic_uScript_RemoveScenery_radius_12 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_12.In(logic_uScript_RemoveScenery_ownerNode_12, logic_uScript_RemoveScenery_positionName_12, logic_uScript_RemoveScenery_radius_12, logic_uScript_RemoveScenery_preventChunksSpawning_12);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_12.Out)
		{
			Relay_InitialSpawn_18();
		}
	}

	private void Relay_Succeed_13()
	{
		logic_uScript_FinishEncounter_owner_13 = owner_Connection_14;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_13.Succeed(logic_uScript_FinishEncounter_owner_13);
	}

	private void Relay_Fail_13()
	{
		logic_uScript_FinishEncounter_owner_13 = owner_Connection_14;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_13.Fail(logic_uScript_FinishEncounter_owner_13);
	}

	private void Relay_In_16()
	{
		logic_uScript_AddMessage_messageData_16 = msgComplete;
		logic_uScript_AddMessage_speaker_16 = messageSpeaker;
		logic_uScript_AddMessage_Return_16 = logic_uScript_AddMessage_uScript_AddMessage_16.In(logic_uScript_AddMessage_messageData_16, logic_uScript_AddMessage_speaker_16);
		if (logic_uScript_AddMessage_uScript_AddMessage_16.Shown)
		{
			Relay_In_77();
		}
	}

	private void Relay_InitialSpawn_18()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_18.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_18, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_18, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_18 = owner_Connection_20;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_18, logic_uScript_SpawnTechsFromData_ownerNode_18, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_18);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_Output1_21()
	{
		Relay_In_31();
	}

	private void Relay_Output2_21()
	{
		Relay_In_45();
	}

	private void Relay_Output3_21()
	{
	}

	private void Relay_Output4_21()
	{
	}

	private void Relay_Output5_21()
	{
	}

	private void Relay_Output6_21()
	{
	}

	private void Relay_Output7_21()
	{
	}

	private void Relay_Output8_21()
	{
	}

	private void Relay_In_21()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_21 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.In(logic_uScriptCon_ManualSwitch_CurrentOutput_21);
	}

	private void Relay_In_25()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_25.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_25, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_25, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_25 = owner_Connection_24;
		int num2 = 0;
		Array array = local_26_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_25.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_25, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_25, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_25 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_25.In(logic_uScript_GetAndCheckTechs_techData_25, logic_uScript_GetAndCheckTechs_ownerNode_25, ref logic_uScript_GetAndCheckTechs_techs_25);
		local_26_TankArray = logic_uScript_GetAndCheckTechs_techs_25;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_25.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_25.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_28();
		}
		if (someAlive)
		{
			Relay_AtIndex_28();
		}
	}

	private void Relay_AtIndex_28()
	{
		int num = 0;
		Array array = local_26_TankArray;
		if (logic_uScript_AccessListTech_techList_28.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_28, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_28, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_28.AtIndex(ref logic_uScript_AccessListTech_techList_28, logic_uScript_AccessListTech_index_28, out logic_uScript_AccessListTech_value_28);
		local_26_TankArray = logic_uScript_AccessListTech_techList_28;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_28;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_28.Out)
		{
			Relay_In_66();
		}
	}

	private void Relay_In_31()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_31 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_31 = distNearNPC;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_31.In(logic_uScript_IsPlayerInRangeOfTech_tech_31, logic_uScript_IsPlayerInRangeOfTech_range_31, logic_uScript_IsPlayerInRangeOfTech_techs_31);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_31.InRange)
		{
			Relay_In_33();
		}
	}

	private void Relay_In_32()
	{
		logic_uScript_AddMessage_messageData_32 = msgIntro;
		logic_uScript_AddMessage_speaker_32 = messageSpeaker;
		logic_uScript_AddMessage_Return_32 = logic_uScript_AddMessage_uScript_AddMessage_32.In(logic_uScript_AddMessage_messageData_32, logic_uScript_AddMessage_speaker_32);
		if (logic_uScript_AddMessage_uScript_AddMessage_32.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_Out_33()
	{
	}

	private void Relay_In_33()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_33 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_33.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_33, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_33);
	}

	private void Relay_Save_Out_38()
	{
		Relay_Save_65();
	}

	private void Relay_Load_Out_38()
	{
		Relay_Load_65();
	}

	private void Relay_Restart_Out_38()
	{
		Relay_Set_False_65();
	}

	private void Relay_Save_38()
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
	}

	private void Relay_Load_38()
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
	}

	private void Relay_Set_True_38()
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
	}

	private void Relay_Set_False_38()
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_MsgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
	}

	private void Relay_Save_Out_39()
	{
	}

	private void Relay_Load_Out_39()
	{
		Relay_In_41();
	}

	private void Relay_Restart_Out_39()
	{
		Relay_False_80();
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

	private void Relay_Out_41()
	{
	}

	private void Relay_In_41()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_41 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_41.In(logic_SubGraph_LoadObjectiveStates_currentObjective_41);
	}

	private void Relay_Save_Out_44()
	{
		Relay_Save_51();
	}

	private void Relay_Load_Out_44()
	{
		Relay_Load_51();
	}

	private void Relay_Restart_Out_44()
	{
		Relay_Set_False_51();
	}

	private void Relay_Save_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Load_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Set_True_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Set_False_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_MsgHalfwayRound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_In_45()
	{
		logic_uScriptCon_CompareBool_Bool_45 = local_MissionComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.In(logic_uScriptCon_CompareBool_Bool_45);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.False;
		if (num)
		{
			Relay_In_16();
		}
		if (flag)
		{
			Relay_In_101();
		}
	}

	private void Relay_Save_Out_51()
	{
		Relay_Save_52();
	}

	private void Relay_Load_Out_51()
	{
		Relay_Load_52();
	}

	private void Relay_Restart_Out_51()
	{
		Relay_Set_False_52();
	}

	private void Relay_Save_51()
	{
		logic_SubGraph_SaveLoadBool_boolean_51 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_51 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Save(ref logic_SubGraph_SaveLoadBool_boolean_51, logic_SubGraph_SaveLoadBool_boolAsVariable_51, logic_SubGraph_SaveLoadBool_uniqueID_51);
	}

	private void Relay_Load_51()
	{
		logic_SubGraph_SaveLoadBool_boolean_51 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_51 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Load(ref logic_SubGraph_SaveLoadBool_boolean_51, logic_SubGraph_SaveLoadBool_boolAsVariable_51, logic_SubGraph_SaveLoadBool_uniqueID_51);
	}

	private void Relay_Set_True_51()
	{
		logic_SubGraph_SaveLoadBool_boolean_51 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_51 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_51, logic_SubGraph_SaveLoadBool_boolAsVariable_51, logic_SubGraph_SaveLoadBool_uniqueID_51);
	}

	private void Relay_Set_False_51()
	{
		logic_SubGraph_SaveLoadBool_boolean_51 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_51 = local_ChallengeEnded_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_51.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_51, logic_SubGraph_SaveLoadBool_boolAsVariable_51, logic_SubGraph_SaveLoadBool_uniqueID_51);
	}

	private void Relay_Save_Out_52()
	{
		Relay_Save_39();
	}

	private void Relay_Load_Out_52()
	{
		Relay_Load_39();
	}

	private void Relay_Restart_Out_52()
	{
		Relay_Restart_39();
	}

	private void Relay_Save_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Save(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_Load_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Load(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_Set_True_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_Set_False_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_In_55()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_55 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_55 = distLeavingMission;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_55.In(logic_uScript_IsPlayerInRangeOfTech_tech_55, logic_uScript_IsPlayerInRangeOfTech_range_55, logic_uScript_IsPlayerInRangeOfTech_techs_55);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_55.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_55.OutOfRange;
		if (inRange)
		{
			Relay_In_63();
		}
		if (outOfRange)
		{
			Relay_In_59();
		}
	}

	private void Relay_In_57()
	{
		logic_uScriptCon_CompareBool_Bool_57 = local_ChallengeInProgess_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.In(logic_uScriptCon_CompareBool_Bool_57);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.False;
		if (num)
		{
			Relay_In_21();
		}
		if (flag)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_59()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_59 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_59.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_59, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_59);
	}

	private void Relay_True_61()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_61.True(out logic_uScriptAct_SetBool_Target_61);
		local_MsgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_61;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_61.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_False_61()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_61.False(out logic_uScriptAct_SetBool_Target_61);
		local_MsgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_61;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_61.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_63()
	{
		logic_uScriptCon_CompareBool_Bool_63 = local_MsgIntro_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.In(logic_uScriptCon_CompareBool_Bool_63);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.False;
		if (num)
		{
			Relay_In_21();
		}
		if (flag)
		{
			Relay_True_61();
		}
	}

	private void Relay_Save_Out_65()
	{
		Relay_Save_44();
	}

	private void Relay_Load_Out_65()
	{
		Relay_Load_44();
	}

	private void Relay_Restart_Out_65()
	{
		Relay_Set_False_44();
	}

	private void Relay_Save_65()
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_65 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Save(ref logic_SubGraph_SaveLoadBool_boolean_65, logic_SubGraph_SaveLoadBool_boolAsVariable_65, logic_SubGraph_SaveLoadBool_uniqueID_65);
	}

	private void Relay_Load_65()
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_65 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Load(ref logic_SubGraph_SaveLoadBool_boolean_65, logic_SubGraph_SaveLoadBool_boolAsVariable_65, logic_SubGraph_SaveLoadBool_uniqueID_65);
	}

	private void Relay_Set_True_65()
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_65 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_65, logic_SubGraph_SaveLoadBool_boolAsVariable_65, logic_SubGraph_SaveLoadBool_uniqueID_65);
	}

	private void Relay_Set_False_65()
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_65 = local_MsgNPCFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_65, logic_SubGraph_SaveLoadBool_boolAsVariable_65, logic_SubGraph_SaveLoadBool_uniqueID_65);
	}

	private void Relay_In_66()
	{
		logic_uScript_SetTankInvulnerable_tank_66 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_66.In(logic_uScript_SetTankInvulnerable_invulnerable_66, logic_uScript_SetTankInvulnerable_tank_66);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_66.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_In_69()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_69 = owner_Connection_68;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_69.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_69);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_69.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_70()
	{
		logic_uScript_SetEncounterTarget_owner_70 = owner_Connection_71;
		logic_uScript_SetEncounterTarget_visibleObject_70 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_70.In(logic_uScript_SetEncounterTarget_owner_70, logic_uScript_SetEncounterTarget_visibleObject_70);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_70.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_75()
	{
		logic_uScript_AddMessage_messageData_75 = msgOutOfTime;
		logic_uScript_AddMessage_speaker_75 = messageSpeaker;
		logic_uScript_AddMessage_Return_75 = logic_uScript_AddMessage_uScript_AddMessage_75.In(logic_uScript_AddMessage_messageData_75, logic_uScript_AddMessage_speaker_75);
		if (logic_uScript_AddMessage_uScript_AddMessage_75.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_In_77()
	{
		logic_uScript_FlyTechUpAndAway_tech_77 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_77 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_77 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_77.In(logic_uScript_FlyTechUpAndAway_tech_77, logic_uScript_FlyTechUpAndAway_maxLifetime_77, logic_uScript_FlyTechUpAndAway_targetHeight_77, logic_uScript_FlyTechUpAndAway_aiTree_77, logic_uScript_FlyTechUpAndAway_removalParticles_77);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_77.Out)
		{
			Relay_Succeed_13();
		}
	}

	private void Relay_True_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.True(out logic_uScriptAct_SetBool_Target_80);
		local_ChallengeInProgess_System_Boolean = logic_uScriptAct_SetBool_Target_80;
	}

	private void Relay_False_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.False(out logic_uScriptAct_SetBool_Target_80);
		local_ChallengeInProgess_System_Boolean = logic_uScriptAct_SetBool_Target_80;
	}

	private void Relay_In_83()
	{
		logic_uScript_SetEncounterPosition_ownerNode_83 = owner_Connection_87;
		logic_uScript_SetEncounterPosition_position_83 = local_VendorPos_UnityEngine_Vector3;
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_83.In(logic_uScript_SetEncounterPosition_ownerNode_83, logic_uScript_SetEncounterPosition_position_83);
		if (logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_83.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_84()
	{
		logic_uScript_GetNearestVendorPos_Return_84 = logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_84.In();
		local_VendorPos_UnityEngine_Vector3 = logic_uScript_GetNearestVendorPos_Return_84;
		if (logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_84.Found)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_88()
	{
		logic_uScript_FindNearestVendor_Return_88 = logic_uScript_FindNearestVendor_uScript_FindNearestVendor_88.In();
		local_GSOVendor_Tank = logic_uScript_FindNearestVendor_Return_88;
		if (logic_uScript_FindNearestVendor_uScript_FindNearestVendor_88.Returned)
		{
			Relay_In_111();
		}
	}

	private void Relay_True_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.True(out logic_uScriptAct_SetBool_Target_93);
		local_MsgNPCFound_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_False_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.False(out logic_uScriptAct_SetBool_Target_93);
		local_MsgNPCFound_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_In_94()
	{
		logic_uScript_AddMessage_messageData_94 = msgNPCFound;
		logic_uScript_AddMessage_speaker_94 = messageSpeaker;
		logic_uScript_AddMessage_Return_94 = logic_uScript_AddMessage_uScript_AddMessage_94.In(logic_uScript_AddMessage_messageData_94, logic_uScript_AddMessage_speaker_94);
		if (logic_uScript_AddMessage_uScript_AddMessage_94.Shown)
		{
			Relay_True_93();
		}
	}

	private void Relay_In_95()
	{
		logic_uScriptCon_CompareBool_Bool_95 = local_MsgNPCFound_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.In(logic_uScriptCon_CompareBool_Bool_95);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.False;
		if (num)
		{
			Relay_In_98();
		}
		if (flag)
		{
			Relay_In_94();
		}
	}

	private void Relay_True_97()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.True(out logic_uScriptAct_SetBool_Target_97);
		local_ChallengeInProgess_System_Boolean = logic_uScriptAct_SetBool_Target_97;
	}

	private void Relay_False_97()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.False(out logic_uScriptAct_SetBool_Target_97);
		local_ChallengeInProgess_System_Boolean = logic_uScriptAct_SetBool_Target_97;
	}

	private void Relay_In_98()
	{
		logic_uScript_StartEncounterTimer_owner_98 = owner_Connection_99;
		logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_98.In(logic_uScript_StartEncounterTimer_owner_98);
		if (logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_98.Out)
		{
			Relay_True_97();
		}
	}

	private void Relay_In_101()
	{
		logic_uScriptCon_CompareBool_Bool_101 = local_ChallengeInProgess_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.In(logic_uScriptCon_CompareBool_Bool_101);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.False;
		if (num)
		{
			Relay_In_84();
		}
		if (flag)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_103()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_103 = local_GSOVendor_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_103 = distNearTradingStation;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_103.In(logic_uScript_IsPlayerInRangeOfTech_tech_103, logic_uScript_IsPlayerInRangeOfTech_range_103, logic_uScript_IsPlayerInRangeOfTech_techs_103);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_103.InRange)
		{
			Relay_In_109();
		}
	}

	private void Relay_True_105()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_105.True(out logic_uScriptAct_SetBool_Target_105);
		local_MissionComplete_System_Boolean = logic_uScriptAct_SetBool_Target_105;
	}

	private void Relay_False_105()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_105.False(out logic_uScriptAct_SetBool_Target_105);
		local_MissionComplete_System_Boolean = logic_uScriptAct_SetBool_Target_105;
	}

	private void Relay_In_109()
	{
		logic_uScript_RemoveEncounterTimer_owner_109 = owner_Connection_110;
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_109.In(logic_uScript_RemoveEncounterTimer_owner_109);
		if (logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_109.Out)
		{
			Relay_True_105();
		}
	}

	private void Relay_In_111()
	{
		logic_uScript_SetEncounterTarget_owner_111 = owner_Connection_113;
		logic_uScript_SetEncounterTarget_visibleObject_111 = local_GSOVendor_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_111.In(logic_uScript_SetEncounterTarget_owner_111, logic_uScript_SetEncounterTarget_visibleObject_111);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_111.Out)
		{
			Relay_TimeRemaining_116();
		}
	}

	private void Relay_TimeRemaining_116()
	{
		logic_uScript_GetEncounterTimeRemaining_owner_116 = owner_Connection_117;
		logic_uScript_GetEncounterTimeRemaining_Return_116 = logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_116.TimeRemaining(logic_uScript_GetEncounterTimeRemaining_owner_116);
		local_TimeRemaining_System_Single = logic_uScript_GetEncounterTimeRemaining_Return_116;
		if (logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_116.Out)
		{
			Relay_In_119();
		}
	}

	private void Relay_TimeRemainingPercent_116()
	{
		logic_uScript_GetEncounterTimeRemaining_owner_116 = owner_Connection_117;
		logic_uScript_GetEncounterTimeRemaining_Return_116 = logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_116.TimeRemainingPercent(logic_uScript_GetEncounterTimeRemaining_owner_116);
		local_TimeRemaining_System_Single = logic_uScript_GetEncounterTimeRemaining_Return_116;
		if (logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_116.Out)
		{
			Relay_In_119();
		}
	}

	private void Relay_In_119()
	{
		logic_uScriptCon_CompareFloat_A_119 = local_TimeRemaining_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_119.In(logic_uScriptCon_CompareFloat_A_119, logic_uScriptCon_CompareFloat_B_119);
		bool greaterThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_119.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_119.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_103();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_75();
		}
	}

	private void Relay_MarkCompleted_120()
	{
		logic_uScript_SetQuestObjectiveCompleted_owner_120 = owner_Connection_121;
		logic_uScript_SetQuestObjectiveCompleted_objectiveId_120 = local_Stage_System_Int32;
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_120.MarkCompleted(logic_uScript_SetQuestObjectiveCompleted_owner_120, logic_uScript_SetQuestObjectiveCompleted_objectiveId_120, logic_uScript_SetQuestObjectiveCompleted_completed_120);
		if (logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_120.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_True_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.True(out logic_uScriptAct_SetBool_Target_125);
		local_ChallengeInProgess_System_Boolean = logic_uScriptAct_SetBool_Target_125;
	}

	private void Relay_False_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.False(out logic_uScriptAct_SetBool_Target_125);
		local_ChallengeInProgess_System_Boolean = logic_uScriptAct_SetBool_Target_125;
	}

	private void Relay_In_128()
	{
		logic_uScript_RemoveEncounterTimer_owner_128 = owner_Connection_127;
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_128.In(logic_uScript_RemoveEncounterTimer_owner_128);
		if (logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_128.Out)
		{
			Relay_MarkCompleted_120();
		}
	}

	private void Relay_In_129()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_129.In(logic_uScriptAct_SetInt_Value_129, out logic_uScriptAct_SetInt_Target_129);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_129;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_129.Out)
		{
			Relay_False_125();
		}
	}
}
