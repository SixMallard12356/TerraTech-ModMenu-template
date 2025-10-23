using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_UnlockReticuleResearchLicense : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private Tank[] local_42_TankArray = new Tank[0];

	private Tank local_45_Tank;

	private Tank local_49_Tank;

	private Tank[] local_51_TankArray = new Tank[0];

	private int local_CurrentObjective_System_Int32 = 1;

	private bool local_Init_System_Boolean;

	private int local_Objective2SubStage_System_Int32 = 1;

	public uScript_AddMessage.MessageData msg00Intro;

	public uScript_AddMessage.MessageData msg01aNPC;

	public uScript_AddMessage.MessageData msg01bGSO;

	public uScript_AddMessage.MessageData msg02aNPC;

	public uScript_AddMessage.MessageData msg02bGSO;

	public uScript_AddMessage.MessageData msg03NPC;

	public uScript_AddMessage.MessageData msg04NPC;

	public uScript_AddMessage.MessageData msg05NPC;

	public Transform NPCDespawnParticleEffect;

	public SpawnTechData[] NPCTech01 = new SpawnTechData[0];

	public SpawnTechData[] NPCTech02 = new SpawnTechData[0];

	public SpawnTechData[] NPCTech03 = new SpawnTechData[0];

	public SpawnTechData[] NPCTech04 = new SpawnTechData[0];

	public SpawnTechData[] NPCTech05 = new SpawnTechData[0];

	public uScript_AddMessage.MessageSpeaker speakerGSO;

	public uScript_AddMessage.MessageSpeaker speakerNPC;

	[Multiline(1)]
	public string triggerVolumeNPC01 = "";

	[Multiline(1)]
	public string triggerVolumeNPC02 = "";

	[Multiline(1)]
	public string triggerVolumeNPC03 = "";

	[Multiline(1)]
	public string triggerVolumeNPC04 = "";

	[Multiline(1)]
	public string triggerVolumeNPC05 = "";

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_13;

	private GameObject owner_Connection_26;

	private GameObject owner_Connection_28;

	private GameObject owner_Connection_30;

	private GameObject owner_Connection_32;

	private GameObject owner_Connection_35;

	private GameObject owner_Connection_40;

	private GameObject owner_Connection_47;

	private GameObject owner_Connection_52;

	private GameObject owner_Connection_99;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_2;

	private bool logic_uScriptCon_CompareBool_True_2 = true;

	private bool logic_uScriptCon_CompareBool_False_2 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_3 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_3;

	private bool logic_uScriptAct_SetBool_Out_3 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_3 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_3 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_5 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_5 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_5;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_5 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_5;

	private bool logic_uScript_SpawnTechsFromData_Out_5 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_8;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_10 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_10;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_10 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_10 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_10 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_14;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_14 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_14 = "Init";

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_16 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_16;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_16;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_18 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_18;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_18;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_20 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_20;

	private int logic_uScriptAct_AddInt_v2_B_20 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_20;

	private float logic_uScriptAct_AddInt_v2_FloatResult_20;

	private bool logic_uScriptAct_AddInt_v2_Out_20 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_22 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_22;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_22 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_22 = "CurrentObjective";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_24;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_25 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_25;

	private bool logic_uScript_FinishEncounter_Out_25 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_27 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_27;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_27 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_29 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_29;

	private object logic_uScript_SetEncounterTarget_visibleObject_29 = "";

	private bool logic_uScript_SetEncounterTarget_Out_29 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_31 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_31;

	private bool logic_uScript_ClearEncounterTarget_Out_31 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_33 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_33 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_34 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_34;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_34 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_34 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_34 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_43 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_43 = new Tank[0];

	private int logic_uScript_AccessListTech_index_43;

	private Tank logic_uScript_AccessListTech_value_43;

	private bool logic_uScript_AccessListTech_Out_43 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_44 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_44 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_44;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_44 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_44;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_44 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_44 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_44 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_44 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_46 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_46 = new Tank[0];

	private int logic_uScript_AccessListTech_index_46;

	private Tank logic_uScript_AccessListTech_value_46;

	private bool logic_uScript_AccessListTech_Out_46 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_48 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_48 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_48;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_48 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_48;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_48 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_48 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_48 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_48 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_50 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_50;

	private object logic_uScript_SetEncounterTarget_visibleObject_50 = "";

	private bool logic_uScript_SetEncounterTarget_Out_50 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_54;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_56 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_56;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_56 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_56 = "Objective2SubStage";

	private SubGraph_RR_License_NPCController logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_61 = new SubGraph_RR_License_NPCController();

	private SpawnTechData[] logic_SubGraph_RR_License_NPCController_currentNPCTechData_61 = new SpawnTechData[0];

	private bool logic_SubGraph_RR_License_NPCController_hideNPCMarker__61;

	private string logic_SubGraph_RR_License_NPCController_npcTriggerVolume_61 = "";

	private uScript_AddMessage.MessageData logic_SubGraph_RR_License_NPCController_npcDialogue_61;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_RR_License_NPCController_npcSpeaker_61;

	private bool logic_SubGraph_RR_License_NPCController_waitUnitDialogueFinished__61;

	private Transform logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_61;

	private bool logic_SubGraph_RR_License_NPCController_spawnNextNPC__61 = true;

	private SpawnTechData[] logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_61 = new SpawnTechData[0];

	private SubGraph_RR_License_NPCController logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_70 = new SubGraph_RR_License_NPCController();

	private SpawnTechData[] logic_SubGraph_RR_License_NPCController_currentNPCTechData_70 = new SpawnTechData[0];

	private bool logic_SubGraph_RR_License_NPCController_hideNPCMarker__70;

	private string logic_SubGraph_RR_License_NPCController_npcTriggerVolume_70 = "";

	private uScript_AddMessage.MessageData logic_SubGraph_RR_License_NPCController_npcDialogue_70;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_RR_License_NPCController_npcSpeaker_70;

	private bool logic_SubGraph_RR_License_NPCController_waitUnitDialogueFinished__70;

	private Transform logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_70;

	private bool logic_SubGraph_RR_License_NPCController_spawnNextNPC__70 = true;

	private SpawnTechData[] logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_70 = new SpawnTechData[0];

	private SubGraph_RR_License_NPCController logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_71 = new SubGraph_RR_License_NPCController();

	private SpawnTechData[] logic_SubGraph_RR_License_NPCController_currentNPCTechData_71 = new SpawnTechData[0];

	private bool logic_SubGraph_RR_License_NPCController_hideNPCMarker__71 = true;

	private string logic_SubGraph_RR_License_NPCController_npcTriggerVolume_71 = "";

	private uScript_AddMessage.MessageData logic_SubGraph_RR_License_NPCController_npcDialogue_71;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_RR_License_NPCController_npcSpeaker_71;

	private bool logic_SubGraph_RR_License_NPCController_waitUnitDialogueFinished__71 = true;

	private Transform logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_71;

	private bool logic_SubGraph_RR_License_NPCController_spawnNextNPC__71 = true;

	private SpawnTechData[] logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_71 = new SpawnTechData[0];

	private SubGraph_RR_License_NPCController logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_76 = new SubGraph_RR_License_NPCController();

	private SpawnTechData[] logic_SubGraph_RR_License_NPCController_currentNPCTechData_76 = new SpawnTechData[0];

	private bool logic_SubGraph_RR_License_NPCController_hideNPCMarker__76 = true;

	private string logic_SubGraph_RR_License_NPCController_npcTriggerVolume_76 = "";

	private uScript_AddMessage.MessageData logic_SubGraph_RR_License_NPCController_npcDialogue_76;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_RR_License_NPCController_npcSpeaker_76;

	private bool logic_SubGraph_RR_License_NPCController_waitUnitDialogueFinished__76 = true;

	private Transform logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_76;

	private bool logic_SubGraph_RR_License_NPCController_spawnNextNPC__76 = true;

	private SpawnTechData[] logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_76 = new SpawnTechData[0];

	private SubGraph_RR_License_NPCController logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_81 = new SubGraph_RR_License_NPCController();

	private SpawnTechData[] logic_SubGraph_RR_License_NPCController_currentNPCTechData_81 = new SpawnTechData[0];

	private bool logic_SubGraph_RR_License_NPCController_hideNPCMarker__81 = true;

	private string logic_SubGraph_RR_License_NPCController_npcTriggerVolume_81 = "";

	private uScript_AddMessage.MessageData logic_SubGraph_RR_License_NPCController_npcDialogue_81;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_RR_License_NPCController_npcSpeaker_81;

	private bool logic_SubGraph_RR_License_NPCController_waitUnitDialogueFinished__81 = true;

	private Transform logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_81;

	private bool logic_SubGraph_RR_License_NPCController_spawnNextNPC__81;

	private SpawnTechData[] logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_81 = new SpawnTechData[0];

	private uScript_Wait logic_uScript_Wait_uScript_Wait_86 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_86 = 2f;

	private bool logic_uScript_Wait_repeat_86;

	private bool logic_uScript_Wait_Waited_86 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_88 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_88;

	private int logic_uScriptAct_AddInt_v2_B_88 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_88;

	private float logic_uScriptAct_AddInt_v2_FloatResult_88;

	private bool logic_uScriptAct_AddInt_v2_Out_88 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_89 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_89 = 2f;

	private bool logic_uScript_Wait_repeat_89;

	private bool logic_uScript_Wait_Waited_89 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_91 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_91;

	private int logic_uScriptAct_AddInt_v2_B_91 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_91;

	private float logic_uScriptAct_AddInt_v2_FloatResult_91;

	private bool logic_uScriptAct_AddInt_v2_Out_91 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_93 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_93;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_93;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_95;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_95;

	private uScript_SetEncounterWaypointVisibility logic_uScript_SetEncounterWaypointVisibility_uScript_SetEncounterWaypointVisibility_98 = new uScript_SetEncounterWaypointVisibility();

	private GameObject logic_uScript_SetEncounterWaypointVisibility_ownerNode_98;

	private bool logic_uScript_SetEncounterWaypointVisibility_Out_98 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_110 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_110;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_110;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_110;

	private bool logic_uScript_AddMessage_Out_110 = true;

	private bool logic_uScript_AddMessage_Shown_110 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_113 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_113;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_113;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_113;

	private bool logic_uScript_AddMessage_Out_113 = true;

	private bool logic_uScript_AddMessage_Shown_113 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_117 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_117;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_117;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_117;

	private bool logic_uScript_AddMessage_Out_117 = true;

	private bool logic_uScript_AddMessage_Shown_117 = true;

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
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
		}
		if (null == owner_Connection_13 || !m_RegisteredForEvents)
		{
			owner_Connection_13 = parentGameObject;
			if (null != owner_Connection_13)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_13.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_13.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_12;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_12;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_12;
				}
			}
		}
		if (null == owner_Connection_26 || !m_RegisteredForEvents)
		{
			owner_Connection_26 = parentGameObject;
		}
		if (null == owner_Connection_28 || !m_RegisteredForEvents)
		{
			owner_Connection_28 = parentGameObject;
		}
		if (null == owner_Connection_30 || !m_RegisteredForEvents)
		{
			owner_Connection_30 = parentGameObject;
		}
		if (null == owner_Connection_32 || !m_RegisteredForEvents)
		{
			owner_Connection_32 = parentGameObject;
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
		if (null == owner_Connection_40 || !m_RegisteredForEvents)
		{
			owner_Connection_40 = parentGameObject;
		}
		if (null == owner_Connection_47 || !m_RegisteredForEvents)
		{
			owner_Connection_47 = parentGameObject;
		}
		if (null == owner_Connection_52 || !m_RegisteredForEvents)
		{
			owner_Connection_52 = parentGameObject;
		}
		if (null == owner_Connection_99 || !m_RegisteredForEvents)
		{
			owner_Connection_99 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_13)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_13.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_13.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_12;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_12;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_12;
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
		if (null != owner_Connection_13)
		{
			uScript_SaveLoad component2 = owner_Connection_13.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_12;
				component2.LoadEvent -= Instance_LoadEvent_12;
				component2.RestartEvent -= Instance_RestartEvent_12;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_3.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_5.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_10.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_16.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_18.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_20.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_25.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_27.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_29.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_31.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_33.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_34.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_43.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_44.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_46.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_48.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_50.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.SetParent(g);
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_61.SetParent(g);
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_70.SetParent(g);
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_71.SetParent(g);
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_76.SetParent(g);
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_81.SetParent(g);
		logic_uScript_Wait_uScript_Wait_86.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_88.SetParent(g);
		logic_uScript_Wait_uScript_Wait_89.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_91.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_93.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.SetParent(g);
		logic_uScript_SetEncounterWaypointVisibility_uScript_SetEncounterWaypointVisibility_98.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_110.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_113.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_117.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_6 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_13 = parentGameObject;
		owner_Connection_26 = parentGameObject;
		owner_Connection_28 = parentGameObject;
		owner_Connection_30 = parentGameObject;
		owner_Connection_32 = parentGameObject;
		owner_Connection_35 = parentGameObject;
		owner_Connection_40 = parentGameObject;
		owner_Connection_47 = parentGameObject;
		owner_Connection_52 = parentGameObject;
		owner_Connection_99 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_16.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_18.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Awake();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_61.Awake();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_70.Awake();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_71.Awake();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_76.Awake();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_81.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_93.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output1 += uScriptCon_ManualSwitch_Output1_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output2 += uScriptCon_ManualSwitch_Output2_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output3 += uScriptCon_ManualSwitch_Output3_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output4 += uScriptCon_ManualSwitch_Output4_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output5 += uScriptCon_ManualSwitch_Output5_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output6 += uScriptCon_ManualSwitch_Output6_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output7 += uScriptCon_ManualSwitch_Output7_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output8 += uScriptCon_ManualSwitch_Output8_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Save_Out += SubGraph_SaveLoadBool_Save_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Load_Out += SubGraph_SaveLoadBool_Load_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_14;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_16.Out += SubGraph_CompleteObjectiveStage_Out_16;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_18.Out += SubGraph_CompleteObjectiveStage_Out_18;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Save_Out += SubGraph_SaveLoadInt_Save_Out_22;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Load_Out += SubGraph_SaveLoadInt_Load_Out_22;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_22;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Out += SubGraph_LoadObjectiveStates_Out_24;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output1 += uScriptCon_ManualSwitch_Output1_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output2 += uScriptCon_ManualSwitch_Output2_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output3 += uScriptCon_ManualSwitch_Output3_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output4 += uScriptCon_ManualSwitch_Output4_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output5 += uScriptCon_ManualSwitch_Output5_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output6 += uScriptCon_ManualSwitch_Output6_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output7 += uScriptCon_ManualSwitch_Output7_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output8 += uScriptCon_ManualSwitch_Output8_54;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Save_Out += SubGraph_SaveLoadInt_Save_Out_56;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Load_Out += SubGraph_SaveLoadInt_Load_Out_56;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_56;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_61.Out += SubGraph_RR_License_NPCController_Out_61;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_70.Out += SubGraph_RR_License_NPCController_Out_70;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_71.Out += SubGraph_RR_License_NPCController_Out_71;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_76.Out += SubGraph_RR_License_NPCController_Out_76;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_81.Out += SubGraph_RR_License_NPCController_Out_81;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_93.Out += SubGraph_CompleteObjectiveStage_Out_93;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.Out += SubGraph_CompleteObjectiveStage_Out_95;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_16.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_18.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Start();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_61.Start();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_70.Start();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_71.Start();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_76.Start();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_81.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_93.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_16.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_18.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_27.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.OnEnable();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_61.OnEnable();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_70.OnEnable();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_71.OnEnable();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_76.OnEnable();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_81.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_93.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_10.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_16.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_18.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_34.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.OnDisable();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_61.OnDisable();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_70.OnDisable();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_71.OnDisable();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_76.OnDisable();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_81.OnDisable();
		logic_uScript_Wait_uScript_Wait_86.OnDisable();
		logic_uScript_Wait_uScript_Wait_89.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_93.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_110.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_113.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_117.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_16.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_18.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Update();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_61.Update();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_70.Update();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_71.Update();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_76.Update();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_81.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_93.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_16.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_18.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.OnDestroy();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_61.OnDestroy();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_70.OnDestroy();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_71.OnDestroy();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_76.OnDestroy();
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_81.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_93.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output1 -= uScriptCon_ManualSwitch_Output1_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output2 -= uScriptCon_ManualSwitch_Output2_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output3 -= uScriptCon_ManualSwitch_Output3_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output4 -= uScriptCon_ManualSwitch_Output4_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output5 -= uScriptCon_ManualSwitch_Output5_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output6 -= uScriptCon_ManualSwitch_Output6_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output7 -= uScriptCon_ManualSwitch_Output7_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.Output8 -= uScriptCon_ManualSwitch_Output8_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Save_Out -= SubGraph_SaveLoadBool_Save_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Load_Out -= SubGraph_SaveLoadBool_Load_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_14;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_16.Out -= SubGraph_CompleteObjectiveStage_Out_16;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_18.Out -= SubGraph_CompleteObjectiveStage_Out_18;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Save_Out -= SubGraph_SaveLoadInt_Save_Out_22;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Load_Out -= SubGraph_SaveLoadInt_Load_Out_22;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_22;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Out -= SubGraph_LoadObjectiveStates_Out_24;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output1 -= uScriptCon_ManualSwitch_Output1_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output2 -= uScriptCon_ManualSwitch_Output2_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output3 -= uScriptCon_ManualSwitch_Output3_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output4 -= uScriptCon_ManualSwitch_Output4_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output5 -= uScriptCon_ManualSwitch_Output5_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output6 -= uScriptCon_ManualSwitch_Output6_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output7 -= uScriptCon_ManualSwitch_Output7_54;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.Output8 -= uScriptCon_ManualSwitch_Output8_54;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Save_Out -= SubGraph_SaveLoadInt_Save_Out_56;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Load_Out -= SubGraph_SaveLoadInt_Load_Out_56;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_56;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_61.Out -= SubGraph_RR_License_NPCController_Out_61;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_70.Out -= SubGraph_RR_License_NPCController_Out_70;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_71.Out -= SubGraph_RR_License_NPCController_Out_71;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_76.Out -= SubGraph_RR_License_NPCController_Out_76;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_81.Out -= SubGraph_RR_License_NPCController_Out_81;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_93.Out -= SubGraph_CompleteObjectiveStage_Out_93;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.Out -= SubGraph_CompleteObjectiveStage_Out_95;
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

	private void Instance_SaveEvent_12(object o, EventArgs e)
	{
		Relay_SaveEvent_12();
	}

	private void Instance_LoadEvent_12(object o, EventArgs e)
	{
		Relay_LoadEvent_12();
	}

	private void Instance_RestartEvent_12(object o, EventArgs e)
	{
		Relay_RestartEvent_12();
	}

	private void uScriptCon_ManualSwitch_Output1_8(object o, EventArgs e)
	{
		Relay_Output1_8();
	}

	private void uScriptCon_ManualSwitch_Output2_8(object o, EventArgs e)
	{
		Relay_Output2_8();
	}

	private void uScriptCon_ManualSwitch_Output3_8(object o, EventArgs e)
	{
		Relay_Output3_8();
	}

	private void uScriptCon_ManualSwitch_Output4_8(object o, EventArgs e)
	{
		Relay_Output4_8();
	}

	private void uScriptCon_ManualSwitch_Output5_8(object o, EventArgs e)
	{
		Relay_Output5_8();
	}

	private void uScriptCon_ManualSwitch_Output6_8(object o, EventArgs e)
	{
		Relay_Output6_8();
	}

	private void uScriptCon_ManualSwitch_Output7_8(object o, EventArgs e)
	{
		Relay_Output7_8();
	}

	private void uScriptCon_ManualSwitch_Output8_8(object o, EventArgs e)
	{
		Relay_Output8_8();
	}

	private void SubGraph_SaveLoadBool_Save_Out_14(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_14;
		Relay_Save_Out_14();
	}

	private void SubGraph_SaveLoadBool_Load_Out_14(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_14;
		Relay_Load_Out_14();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_14(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_14;
		Relay_Restart_Out_14();
	}

	private void SubGraph_CompleteObjectiveStage_Out_16(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_16 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_16;
		Relay_Out_16();
	}

	private void SubGraph_CompleteObjectiveStage_Out_18(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_18 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_18;
		Relay_Out_18();
	}

	private void SubGraph_SaveLoadInt_Save_Out_22(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_22 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_22;
		Relay_Save_Out_22();
	}

	private void SubGraph_SaveLoadInt_Load_Out_22(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_22 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_22;
		Relay_Load_Out_22();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_22(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_22 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_22;
		Relay_Restart_Out_22();
	}

	private void SubGraph_LoadObjectiveStates_Out_24(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_24();
	}

	private void uScriptCon_ManualSwitch_Output1_54(object o, EventArgs e)
	{
		Relay_Output1_54();
	}

	private void uScriptCon_ManualSwitch_Output2_54(object o, EventArgs e)
	{
		Relay_Output2_54();
	}

	private void uScriptCon_ManualSwitch_Output3_54(object o, EventArgs e)
	{
		Relay_Output3_54();
	}

	private void uScriptCon_ManualSwitch_Output4_54(object o, EventArgs e)
	{
		Relay_Output4_54();
	}

	private void uScriptCon_ManualSwitch_Output5_54(object o, EventArgs e)
	{
		Relay_Output5_54();
	}

	private void uScriptCon_ManualSwitch_Output6_54(object o, EventArgs e)
	{
		Relay_Output6_54();
	}

	private void uScriptCon_ManualSwitch_Output7_54(object o, EventArgs e)
	{
		Relay_Output7_54();
	}

	private void uScriptCon_ManualSwitch_Output8_54(object o, EventArgs e)
	{
		Relay_Output8_54();
	}

	private void SubGraph_SaveLoadInt_Save_Out_56(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_56 = e.integer;
		local_Objective2SubStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_56;
		Relay_Save_Out_56();
	}

	private void SubGraph_SaveLoadInt_Load_Out_56(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_56 = e.integer;
		local_Objective2SubStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_56;
		Relay_Load_Out_56();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_56(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_56 = e.integer;
		local_Objective2SubStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_56;
		Relay_Restart_Out_56();
	}

	private void SubGraph_RR_License_NPCController_Out_61(object o, SubGraph_RR_License_NPCController.LogicEventArgs e)
	{
		Relay_Out_61();
	}

	private void SubGraph_RR_License_NPCController_Out_70(object o, SubGraph_RR_License_NPCController.LogicEventArgs e)
	{
		Relay_Out_70();
	}

	private void SubGraph_RR_License_NPCController_Out_71(object o, SubGraph_RR_License_NPCController.LogicEventArgs e)
	{
		Relay_Out_71();
	}

	private void SubGraph_RR_License_NPCController_Out_76(object o, SubGraph_RR_License_NPCController.LogicEventArgs e)
	{
		Relay_Out_76();
	}

	private void SubGraph_RR_License_NPCController_Out_81(object o, SubGraph_RR_License_NPCController.LogicEventArgs e)
	{
		Relay_Out_81();
	}

	private void SubGraph_CompleteObjectiveStage_Out_93(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_93 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_93;
		Relay_Out_93();
	}

	private void SubGraph_CompleteObjectiveStage_Out_95(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_95 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_95;
		Relay_Out_95();
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

	private void Relay_In_2()
	{
		logic_uScriptCon_CompareBool_Bool_2 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.In(logic_uScriptCon_CompareBool_Bool_2);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.False;
		if (num)
		{
			Relay_In_27();
		}
		if (flag)
		{
			Relay_True_3();
		}
	}

	private void Relay_True_3()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_3.True(out logic_uScriptAct_SetBool_Target_3);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_3;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_3.Out)
		{
			Relay_InitialSpawn_5();
		}
	}

	private void Relay_False_3()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_3.False(out logic_uScriptAct_SetBool_Target_3);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_3;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_3.Out)
		{
			Relay_InitialSpawn_5();
		}
	}

	private void Relay_InitialSpawn_5()
	{
		int num = 0;
		Array nPCTech = NPCTech01;
		if (logic_uScript_SpawnTechsFromData_spawnData_5.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_5, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_uScript_SpawnTechsFromData_spawnData_5, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_5 = owner_Connection_6;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_5.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_5, logic_uScript_SpawnTechsFromData_ownerNode_5, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_5, logic_uScript_SpawnTechsFromData_allowResurrection_5);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_5.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_Output1_8()
	{
		Relay_In_44();
	}

	private void Relay_Output2_8()
	{
		Relay_In_54();
	}

	private void Relay_Output3_8()
	{
		Relay_In_71();
	}

	private void Relay_Output4_8()
	{
		Relay_In_76();
	}

	private void Relay_Output5_8()
	{
		Relay_In_81();
	}

	private void Relay_Output6_8()
	{
	}

	private void Relay_Output7_8()
	{
	}

	private void Relay_Output8_8()
	{
	}

	private void Relay_In_8()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_8 = local_CurrentObjective_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_8.In(logic_uScriptCon_ManualSwitch_CurrentOutput_8);
	}

	private void Relay_In_10()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_10 = owner_Connection_11;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_10.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_10);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_10.True)
		{
			Relay_In_117();
		}
	}

	private void Relay_SaveEvent_12()
	{
		Relay_Save_14();
	}

	private void Relay_LoadEvent_12()
	{
		Relay_Load_14();
	}

	private void Relay_RestartEvent_12()
	{
		Relay_Set_False_14();
	}

	private void Relay_Save_Out_14()
	{
		Relay_Save_22();
	}

	private void Relay_Load_Out_14()
	{
		Relay_Load_22();
	}

	private void Relay_Restart_Out_14()
	{
		Relay_Restart_22();
	}

	private void Relay_Save_14()
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Save(ref logic_SubGraph_SaveLoadBool_boolean_14, logic_SubGraph_SaveLoadBool_boolAsVariable_14, logic_SubGraph_SaveLoadBool_uniqueID_14);
	}

	private void Relay_Load_14()
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Load(ref logic_SubGraph_SaveLoadBool_boolean_14, logic_SubGraph_SaveLoadBool_boolAsVariable_14, logic_SubGraph_SaveLoadBool_uniqueID_14);
	}

	private void Relay_Set_True_14()
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_14, logic_SubGraph_SaveLoadBool_boolAsVariable_14, logic_SubGraph_SaveLoadBool_uniqueID_14);
	}

	private void Relay_Set_False_14()
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_14, logic_SubGraph_SaveLoadBool_boolAsVariable_14, logic_SubGraph_SaveLoadBool_uniqueID_14);
	}

	private void Relay_Out_16()
	{
	}

	private void Relay_In_16()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_16 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_16.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_16, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_16);
	}

	private void Relay_Out_18()
	{
	}

	private void Relay_In_18()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_18 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_18.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_18, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_18);
	}

	private void Relay_In_20()
	{
		logic_uScriptAct_AddInt_v2_A_20 = local_Objective2SubStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_20.In(logic_uScriptAct_AddInt_v2_A_20, logic_uScriptAct_AddInt_v2_B_20, out logic_uScriptAct_AddInt_v2_IntResult_20, out logic_uScriptAct_AddInt_v2_FloatResult_20);
		local_Objective2SubStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_20;
	}

	private void Relay_Save_Out_22()
	{
		Relay_Save_56();
	}

	private void Relay_Load_Out_22()
	{
		Relay_Load_56();
	}

	private void Relay_Restart_Out_22()
	{
		Relay_Restart_56();
	}

	private void Relay_Save_22()
	{
		logic_SubGraph_SaveLoadInt_integer_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Save(logic_SubGraph_SaveLoadInt_restartValue_22, ref logic_SubGraph_SaveLoadInt_integer_22, logic_SubGraph_SaveLoadInt_intAsVariable_22, logic_SubGraph_SaveLoadInt_uniqueID_22);
	}

	private void Relay_Load_22()
	{
		logic_SubGraph_SaveLoadInt_integer_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Load(logic_SubGraph_SaveLoadInt_restartValue_22, ref logic_SubGraph_SaveLoadInt_integer_22, logic_SubGraph_SaveLoadInt_intAsVariable_22, logic_SubGraph_SaveLoadInt_uniqueID_22);
	}

	private void Relay_Restart_22()
	{
		logic_SubGraph_SaveLoadInt_integer_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Restart(logic_SubGraph_SaveLoadInt_restartValue_22, ref logic_SubGraph_SaveLoadInt_integer_22, logic_SubGraph_SaveLoadInt_intAsVariable_22, logic_SubGraph_SaveLoadInt_uniqueID_22);
	}

	private void Relay_Out_24()
	{
	}

	private void Relay_In_24()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_24 = local_CurrentObjective_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.In(logic_SubGraph_LoadObjectiveStates_currentObjective_24);
	}

	private void Relay_Succeed_25()
	{
		logic_uScript_FinishEncounter_owner_25 = owner_Connection_26;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_25.Succeed(logic_uScript_FinishEncounter_owner_25);
	}

	private void Relay_Fail_25()
	{
		logic_uScript_FinishEncounter_owner_25 = owner_Connection_26;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_25.Fail(logic_uScript_FinishEncounter_owner_25);
	}

	private void Relay_In_27()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_27 = owner_Connection_28;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_27.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_27);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_27.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_29()
	{
		logic_uScript_SetEncounterTarget_owner_29 = owner_Connection_30;
		logic_uScript_SetEncounterTarget_visibleObject_29 = local_45_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_29.In(logic_uScript_SetEncounterTarget_owner_29, logic_uScript_SetEncounterTarget_visibleObject_29);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_29.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_31()
	{
		logic_uScript_ClearEncounterTarget_owner_31 = owner_Connection_32;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_31.In(logic_uScript_ClearEncounterTarget_owner_31);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_31.Out)
		{
			Relay_Hide_98();
		}
	}

	private void Relay_Pause_33()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_33.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_33.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_UnPause_33()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_33.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_33.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_34()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_34 = owner_Connection_35;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_34.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_34);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_34.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_34.False;
		if (num)
		{
			Relay_Pause_33();
		}
		if (flag)
		{
			Relay_UnPause_33();
		}
	}

	private void Relay_AtIndex_43()
	{
		int num = 0;
		Array array = local_42_TankArray;
		if (logic_uScript_AccessListTech_techList_43.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_43, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_43, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_43.AtIndex(ref logic_uScript_AccessListTech_techList_43, logic_uScript_AccessListTech_index_43, out logic_uScript_AccessListTech_value_43);
		local_42_TankArray = logic_uScript_AccessListTech_techList_43;
		local_45_Tank = logic_uScript_AccessListTech_value_43;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_43.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_44()
	{
		int num = 0;
		Array nPCTech = NPCTech01;
		if (logic_uScript_GetAndCheckTechs_techData_44.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_44, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_uScript_GetAndCheckTechs_techData_44, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_44 = owner_Connection_40;
		int num2 = 0;
		Array array = local_42_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_44.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_44, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_44, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_44 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_44.In(logic_uScript_GetAndCheckTechs_techData_44, logic_uScript_GetAndCheckTechs_ownerNode_44, ref logic_uScript_GetAndCheckTechs_techs_44);
		local_42_TankArray = logic_uScript_GetAndCheckTechs_techs_44;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_44.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_44.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_43();
		}
		if (someAlive)
		{
			Relay_AtIndex_43();
		}
	}

	private void Relay_AtIndex_46()
	{
		int num = 0;
		Array array = local_51_TankArray;
		if (logic_uScript_AccessListTech_techList_46.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_46, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_46, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_46.AtIndex(ref logic_uScript_AccessListTech_techList_46, logic_uScript_AccessListTech_index_46, out logic_uScript_AccessListTech_value_46);
		local_51_TankArray = logic_uScript_AccessListTech_techList_46;
		local_49_Tank = logic_uScript_AccessListTech_value_46;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_46.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_48()
	{
		int num = 0;
		Array nPCTech = NPCTech02;
		if (logic_uScript_GetAndCheckTechs_techData_48.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_48, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_uScript_GetAndCheckTechs_techData_48, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_48 = owner_Connection_52;
		int num2 = 0;
		Array array = local_51_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_48.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_48, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_48, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_48 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_48.In(logic_uScript_GetAndCheckTechs_techData_48, logic_uScript_GetAndCheckTechs_ownerNode_48, ref logic_uScript_GetAndCheckTechs_techs_48);
		local_51_TankArray = logic_uScript_GetAndCheckTechs_techs_48;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_48.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_48.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_46();
		}
		if (someAlive)
		{
			Relay_AtIndex_46();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_SetEncounterTarget_owner_50 = owner_Connection_47;
		logic_uScript_SetEncounterTarget_visibleObject_50 = local_49_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_50.In(logic_uScript_SetEncounterTarget_owner_50, logic_uScript_SetEncounterTarget_visibleObject_50);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_50.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_Output1_54()
	{
		Relay_In_61();
	}

	private void Relay_Output2_54()
	{
		Relay_In_86();
	}

	private void Relay_Output3_54()
	{
		Relay_In_70();
	}

	private void Relay_Output4_54()
	{
		Relay_In_89();
	}

	private void Relay_Output5_54()
	{
	}

	private void Relay_Output6_54()
	{
	}

	private void Relay_Output7_54()
	{
	}

	private void Relay_Output8_54()
	{
	}

	private void Relay_In_54()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_54 = local_Objective2SubStage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_54.In(logic_uScriptCon_ManualSwitch_CurrentOutput_54);
	}

	private void Relay_Save_Out_56()
	{
	}

	private void Relay_Load_Out_56()
	{
		Relay_In_24();
	}

	private void Relay_Restart_Out_56()
	{
	}

	private void Relay_Save_56()
	{
		logic_SubGraph_SaveLoadInt_integer_56 = local_Objective2SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_56 = local_Objective2SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Save(logic_SubGraph_SaveLoadInt_restartValue_56, ref logic_SubGraph_SaveLoadInt_integer_56, logic_SubGraph_SaveLoadInt_intAsVariable_56, logic_SubGraph_SaveLoadInt_uniqueID_56);
	}

	private void Relay_Load_56()
	{
		logic_SubGraph_SaveLoadInt_integer_56 = local_Objective2SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_56 = local_Objective2SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Load(logic_SubGraph_SaveLoadInt_restartValue_56, ref logic_SubGraph_SaveLoadInt_integer_56, logic_SubGraph_SaveLoadInt_intAsVariable_56, logic_SubGraph_SaveLoadInt_uniqueID_56);
	}

	private void Relay_Restart_56()
	{
		logic_SubGraph_SaveLoadInt_integer_56 = local_Objective2SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_56 = local_Objective2SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Restart(logic_SubGraph_SaveLoadInt_restartValue_56, ref logic_SubGraph_SaveLoadInt_integer_56, logic_SubGraph_SaveLoadInt_intAsVariable_56, logic_SubGraph_SaveLoadInt_uniqueID_56);
	}

	private void Relay_Out_61()
	{
		Relay_In_48();
	}

	private void Relay_In_61()
	{
		int num = 0;
		Array nPCTech = NPCTech01;
		if (logic_SubGraph_RR_License_NPCController_currentNPCTechData_61.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_License_NPCController_currentNPCTechData_61, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_SubGraph_RR_License_NPCController_currentNPCTechData_61, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_SubGraph_RR_License_NPCController_npcTriggerVolume_61 = triggerVolumeNPC01;
		logic_SubGraph_RR_License_NPCController_npcDialogue_61 = msg01aNPC;
		logic_SubGraph_RR_License_NPCController_npcSpeaker_61 = speakerNPC;
		logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_61 = NPCDespawnParticleEffect;
		int num2 = 0;
		Array nPCTech2 = NPCTech02;
		if (logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_61.Length != num2 + nPCTech2.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_61, num2 + nPCTech2.Length);
		}
		Array.Copy(nPCTech2, 0, logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_61, num2, nPCTech2.Length);
		num2 += nPCTech2.Length;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_61.In(logic_SubGraph_RR_License_NPCController_currentNPCTechData_61, logic_SubGraph_RR_License_NPCController_hideNPCMarker__61, logic_SubGraph_RR_License_NPCController_npcTriggerVolume_61, logic_SubGraph_RR_License_NPCController_npcDialogue_61, logic_SubGraph_RR_License_NPCController_npcSpeaker_61, logic_SubGraph_RR_License_NPCController_waitUnitDialogueFinished__61, logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_61, logic_SubGraph_RR_License_NPCController_spawnNextNPC__61, logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_61);
	}

	private void Relay_Out_70()
	{
		Relay_In_31();
	}

	private void Relay_In_70()
	{
		int num = 0;
		Array nPCTech = NPCTech02;
		if (logic_SubGraph_RR_License_NPCController_currentNPCTechData_70.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_License_NPCController_currentNPCTechData_70, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_SubGraph_RR_License_NPCController_currentNPCTechData_70, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_SubGraph_RR_License_NPCController_npcTriggerVolume_70 = triggerVolumeNPC02;
		logic_SubGraph_RR_License_NPCController_npcDialogue_70 = msg02aNPC;
		logic_SubGraph_RR_License_NPCController_npcSpeaker_70 = speakerNPC;
		logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_70 = NPCDespawnParticleEffect;
		int num2 = 0;
		Array nPCTech2 = NPCTech03;
		if (logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_70.Length != num2 + nPCTech2.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_70, num2 + nPCTech2.Length);
		}
		Array.Copy(nPCTech2, 0, logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_70, num2, nPCTech2.Length);
		num2 += nPCTech2.Length;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_70.In(logic_SubGraph_RR_License_NPCController_currentNPCTechData_70, logic_SubGraph_RR_License_NPCController_hideNPCMarker__70, logic_SubGraph_RR_License_NPCController_npcTriggerVolume_70, logic_SubGraph_RR_License_NPCController_npcDialogue_70, logic_SubGraph_RR_License_NPCController_npcSpeaker_70, logic_SubGraph_RR_License_NPCController_waitUnitDialogueFinished__70, logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_70, logic_SubGraph_RR_License_NPCController_spawnNextNPC__70, logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_70);
	}

	private void Relay_Out_71()
	{
		Relay_In_93();
	}

	private void Relay_In_71()
	{
		int num = 0;
		Array nPCTech = NPCTech03;
		if (logic_SubGraph_RR_License_NPCController_currentNPCTechData_71.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_License_NPCController_currentNPCTechData_71, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_SubGraph_RR_License_NPCController_currentNPCTechData_71, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_SubGraph_RR_License_NPCController_npcTriggerVolume_71 = triggerVolumeNPC03;
		logic_SubGraph_RR_License_NPCController_npcDialogue_71 = msg03NPC;
		logic_SubGraph_RR_License_NPCController_npcSpeaker_71 = speakerNPC;
		logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_71 = NPCDespawnParticleEffect;
		int num2 = 0;
		Array nPCTech2 = NPCTech04;
		if (logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_71.Length != num2 + nPCTech2.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_71, num2 + nPCTech2.Length);
		}
		Array.Copy(nPCTech2, 0, logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_71, num2, nPCTech2.Length);
		num2 += nPCTech2.Length;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_71.In(logic_SubGraph_RR_License_NPCController_currentNPCTechData_71, logic_SubGraph_RR_License_NPCController_hideNPCMarker__71, logic_SubGraph_RR_License_NPCController_npcTriggerVolume_71, logic_SubGraph_RR_License_NPCController_npcDialogue_71, logic_SubGraph_RR_License_NPCController_npcSpeaker_71, logic_SubGraph_RR_License_NPCController_waitUnitDialogueFinished__71, logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_71, logic_SubGraph_RR_License_NPCController_spawnNextNPC__71, logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_71);
	}

	private void Relay_Out_76()
	{
		Relay_In_95();
	}

	private void Relay_In_76()
	{
		int num = 0;
		Array nPCTech = NPCTech04;
		if (logic_SubGraph_RR_License_NPCController_currentNPCTechData_76.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_License_NPCController_currentNPCTechData_76, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_SubGraph_RR_License_NPCController_currentNPCTechData_76, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_SubGraph_RR_License_NPCController_npcTriggerVolume_76 = triggerVolumeNPC04;
		logic_SubGraph_RR_License_NPCController_npcDialogue_76 = msg04NPC;
		logic_SubGraph_RR_License_NPCController_npcSpeaker_76 = speakerNPC;
		logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_76 = NPCDespawnParticleEffect;
		int num2 = 0;
		Array nPCTech2 = NPCTech05;
		if (logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_76.Length != num2 + nPCTech2.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_76, num2 + nPCTech2.Length);
		}
		Array.Copy(nPCTech2, 0, logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_76, num2, nPCTech2.Length);
		num2 += nPCTech2.Length;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_76.In(logic_SubGraph_RR_License_NPCController_currentNPCTechData_76, logic_SubGraph_RR_License_NPCController_hideNPCMarker__76, logic_SubGraph_RR_License_NPCController_npcTriggerVolume_76, logic_SubGraph_RR_License_NPCController_npcDialogue_76, logic_SubGraph_RR_License_NPCController_npcSpeaker_76, logic_SubGraph_RR_License_NPCController_waitUnitDialogueFinished__76, logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_76, logic_SubGraph_RR_License_NPCController_spawnNextNPC__76, logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_76);
	}

	private void Relay_Out_81()
	{
		Relay_Succeed_25();
	}

	private void Relay_In_81()
	{
		int num = 0;
		Array nPCTech = NPCTech05;
		if (logic_SubGraph_RR_License_NPCController_currentNPCTechData_81.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_License_NPCController_currentNPCTechData_81, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_SubGraph_RR_License_NPCController_currentNPCTechData_81, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_SubGraph_RR_License_NPCController_npcTriggerVolume_81 = triggerVolumeNPC05;
		logic_SubGraph_RR_License_NPCController_npcDialogue_81 = msg05NPC;
		logic_SubGraph_RR_License_NPCController_npcSpeaker_81 = speakerNPC;
		logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_81 = NPCDespawnParticleEffect;
		logic_SubGraph_RR_License_NPCController_SubGraph_RR_License_NPCController_81.In(logic_SubGraph_RR_License_NPCController_currentNPCTechData_81, logic_SubGraph_RR_License_NPCController_hideNPCMarker__81, logic_SubGraph_RR_License_NPCController_npcTriggerVolume_81, logic_SubGraph_RR_License_NPCController_npcDialogue_81, logic_SubGraph_RR_License_NPCController_npcSpeaker_81, logic_SubGraph_RR_License_NPCController_waitUnitDialogueFinished__81, logic_SubGraph_RR_License_NPCController_npcDespawnParticleEffect_81, logic_SubGraph_RR_License_NPCController_spawnNextNPC__81, logic_SubGraph_RR_License_NPCController_nextNPCSpawnData_81);
	}

	private void Relay_In_86()
	{
		logic_uScript_Wait_uScript_Wait_86.In(logic_uScript_Wait_seconds_86, logic_uScript_Wait_repeat_86);
		if (logic_uScript_Wait_uScript_Wait_86.Waited)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_88()
	{
		logic_uScriptAct_AddInt_v2_A_88 = local_Objective2SubStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_88.In(logic_uScriptAct_AddInt_v2_A_88, logic_uScriptAct_AddInt_v2_B_88, out logic_uScriptAct_AddInt_v2_IntResult_88, out logic_uScriptAct_AddInt_v2_FloatResult_88);
		local_Objective2SubStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_88;
	}

	private void Relay_In_89()
	{
		logic_uScript_Wait_uScript_Wait_89.In(logic_uScript_Wait_seconds_89, logic_uScript_Wait_repeat_89);
		if (logic_uScript_Wait_uScript_Wait_89.Waited)
		{
			Relay_In_113();
		}
	}

	private void Relay_In_91()
	{
		logic_uScriptAct_AddInt_v2_A_91 = local_Objective2SubStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_91.In(logic_uScriptAct_AddInt_v2_A_91, logic_uScriptAct_AddInt_v2_B_91, out logic_uScriptAct_AddInt_v2_IntResult_91, out logic_uScriptAct_AddInt_v2_FloatResult_91);
		local_Objective2SubStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_91;
	}

	private void Relay_Out_93()
	{
	}

	private void Relay_In_93()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_93 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_93.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_93, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_93);
	}

	private void Relay_Out_95()
	{
	}

	private void Relay_In_95()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_95 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_95.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_95, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_95);
	}

	private void Relay_Show_98()
	{
		logic_uScript_SetEncounterWaypointVisibility_ownerNode_98 = owner_Connection_99;
		logic_uScript_SetEncounterWaypointVisibility_uScript_SetEncounterWaypointVisibility_98.Show(logic_uScript_SetEncounterWaypointVisibility_ownerNode_98);
		if (logic_uScript_SetEncounterWaypointVisibility_uScript_SetEncounterWaypointVisibility_98.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_Hide_98()
	{
		logic_uScript_SetEncounterWaypointVisibility_ownerNode_98 = owner_Connection_99;
		logic_uScript_SetEncounterWaypointVisibility_uScript_SetEncounterWaypointVisibility_98.Hide(logic_uScript_SetEncounterWaypointVisibility_ownerNode_98);
		if (logic_uScript_SetEncounterWaypointVisibility_uScript_SetEncounterWaypointVisibility_98.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_110()
	{
		logic_uScript_AddMessage_messageData_110 = msg01bGSO;
		logic_uScript_AddMessage_speaker_110 = speakerGSO;
		logic_uScript_AddMessage_Return_110 = logic_uScript_AddMessage_uScript_AddMessage_110.In(logic_uScript_AddMessage_messageData_110, logic_uScript_AddMessage_speaker_110);
		if (logic_uScript_AddMessage_uScript_AddMessage_110.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_113()
	{
		logic_uScript_AddMessage_messageData_113 = msg02bGSO;
		logic_uScript_AddMessage_speaker_113 = speakerGSO;
		logic_uScript_AddMessage_Return_113 = logic_uScript_AddMessage_uScript_AddMessage_113.In(logic_uScript_AddMessage_messageData_113, logic_uScript_AddMessage_speaker_113);
		if (logic_uScript_AddMessage_uScript_AddMessage_113.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_117()
	{
		logic_uScript_AddMessage_messageData_117 = msg00Intro;
		logic_uScript_AddMessage_speaker_117 = speakerGSO;
		logic_uScript_AddMessage_Return_117 = logic_uScript_AddMessage_uScript_AddMessage_117.In(logic_uScript_AddMessage_messageData_117, logic_uScript_AddMessage_speaker_117);
		if (logic_uScript_AddMessage_uScript_AddMessage_117.Out)
		{
			Relay_In_16();
		}
	}
}
