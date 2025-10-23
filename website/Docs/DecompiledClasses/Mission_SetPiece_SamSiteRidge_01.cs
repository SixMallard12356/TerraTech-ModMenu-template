using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_SetPiece_SamSiteRidge_01 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public float DistInRangeOfNPC = 75f;

	private string local_MsgInturrupted_System_String = "msgInturrupted";

	private string local_msgMeeting_System_String = "msgMeeting";

	private bool local_NPCIgnored_System_Boolean;

	private bool local_NPCMet_System_Boolean;

	private bool local_NPCSeen_System_Boolean;

	private Tank local_NPCTank_Tank;

	private Tank[] local_NPCTanks_TankArray = new Tank[0];

	private bool local_ObjectiveComplete_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private bool local_TechsSpawned_System_Boolean;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgNPCGreeting = new LocalisedString[0];

	public LocalisedString[] msgNPCGreetingInturrupt = new LocalisedString[0];

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCTech = new SpawnTechData[0];

	public float TotallyOutOfRangeOfNPC = 150f;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_38;

	private GameObject owner_Connection_41;

	private GameObject owner_Connection_180;

	private GameObject owner_Connection_185;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_2 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_2;

	private bool logic_uScript_FinishEncounter_Out_2 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_10;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_10 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_10 = "TechsSpawned";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_11;

	private bool logic_uScriptCon_CompareBool_True_11 = true;

	private bool logic_uScriptCon_CompareBool_False_11 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_12 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_12;

	private bool logic_uScriptAct_SetBool_Out_12 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_12 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_12 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_13;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_13 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_13 = "ObjectiveComplete";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_16;

	private bool logic_uScriptCon_CompareBool_True_16 = true;

	private bool logic_uScriptCon_CompareBool_False_16 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_18 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_18;

	private bool logic_uScriptAct_SetBool_Out_18 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_18 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_18 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_19 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_19 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_19;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_19 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_19 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_23;

	private bool logic_uScriptCon_CompareBool_True_23 = true;

	private bool logic_uScriptCon_CompareBool_False_23 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_25;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_25 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_25 = "NPCMet";

	private uScript_InRangeOfTech logic_uScript_InRangeOfTech_uScript_InRangeOfTech_26 = new uScript_InRangeOfTech();

	private Tank logic_uScript_InRangeOfTech_tank_26;

	private float logic_uScript_InRangeOfTech_range_26;

	private bool logic_uScript_InRangeOfTech_Out_26 = true;

	private bool logic_uScript_InRangeOfTech_InRange_26 = true;

	private bool logic_uScript_InRangeOfTech_OutOfRange_26 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_29 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_29 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_29 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_29;

	private string logic_uScript_AddOnScreenMessage_tag_29 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_29;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_29;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_29;

	private bool logic_uScript_AddOnScreenMessage_Out_29 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_29 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_33 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_33 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_33;

	private bool logic_uScript_SetTankInvulnerable_Out_33 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_36 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_36 = new Tank[0];

	private int logic_uScript_AccessListTech_index_36;

	private Tank logic_uScript_AccessListTech_value_36;

	private bool logic_uScript_AccessListTech_Out_36 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_39 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_39 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_39;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_39 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_39;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_39 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_39 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_39 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_39 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_40 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_40;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_40 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_40;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_40 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_44 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_44;

	private bool logic_uScriptAct_SetBool_Out_44 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_44 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_44 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_45 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_45;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_45 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_45 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_45;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_45;

	private bool logic_uScript_FlyTechUpAndAway_Out_45 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_50 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_50 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_50;

	private string logic_uScript_AddOnScreenMessage_tag_50 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_50;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_50;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_50;

	private bool logic_uScript_AddOnScreenMessage_Out_50 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_50 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_53 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_53;

	private bool logic_uScriptAct_SetBool_Out_53 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_53 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_53 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_54 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_54 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_54;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_54 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_56 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_56;

	private bool logic_uScriptCon_CompareBool_True_56 = true;

	private bool logic_uScriptCon_CompareBool_False_56 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_59;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_59 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_59 = "NPCSeen";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_62;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_63 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_63;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_63 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_63 = "Stage";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_64;

	private bool logic_uScriptCon_CompareBool_True_64 = true;

	private bool logic_uScriptCon_CompareBool_False_64 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_66 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_66;

	private bool logic_uScriptAct_SetBool_Out_66 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_66 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_66 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_69;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_69 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_69 = "NPCIgnored";

	private uScript_InRangeOfTech logic_uScript_InRangeOfTech_uScript_InRangeOfTech_71 = new uScript_InRangeOfTech();

	private Tank logic_uScript_InRangeOfTech_tank_71;

	private float logic_uScript_InRangeOfTech_range_71;

	private bool logic_uScript_InRangeOfTech_Out_71 = true;

	private bool logic_uScript_InRangeOfTech_InRange_71 = true;

	private bool logic_uScript_InRangeOfTech_OutOfRange_71 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_74 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_74 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_74;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_74 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_78;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_78;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_80;

	private bool logic_uScriptCon_CompareBool_True_80 = true;

	private bool logic_uScriptCon_CompareBool_False_80 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_81 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_81;

	private bool logic_uScript_ClearEncounterTarget_Out_81 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_184 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_184;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_184 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_190 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_190;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_190 = true;

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
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
			if (null != owner_Connection_9)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_9.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_9.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_7;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_7;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_7;
				}
			}
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (null == owner_Connection_38 || !m_RegisteredForEvents)
		{
			owner_Connection_38 = parentGameObject;
		}
		if (null == owner_Connection_41 || !m_RegisteredForEvents)
		{
			owner_Connection_41 = parentGameObject;
		}
		if (null == owner_Connection_180 || !m_RegisteredForEvents)
		{
			owner_Connection_180 = parentGameObject;
		}
		if (null == owner_Connection_185 || !m_RegisteredForEvents)
		{
			owner_Connection_185 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_9)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_9.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_9.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_7;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_7;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_7;
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
		if (null != owner_Connection_9)
		{
			uScript_SaveLoad component2 = owner_Connection_9.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_7;
				component2.LoadEvent -= Instance_LoadEvent_7;
				component2.RestartEvent -= Instance_RestartEvent_7;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_2.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_12.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_18.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_19.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.SetParent(g);
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_26.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_29.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_33.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_36.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_39.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_40.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_45.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_54.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_56.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.SetParent(g);
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_71.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_74.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_81.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_184.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_190.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_38 = parentGameObject;
		owner_Connection_41 = parentGameObject;
		owner_Connection_180 = parentGameObject;
		owner_Connection_185 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_190.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Save_Out += SubGraph_SaveLoadBool_Save_Out_10;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Load_Out += SubGraph_SaveLoadBool_Load_Out_10;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_10;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Save_Out += SubGraph_SaveLoadBool_Save_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Load_Out += SubGraph_SaveLoadBool_Load_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Save_Out += SubGraph_SaveLoadBool_Save_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Load_Out += SubGraph_SaveLoadBool_Load_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Save_Out += SubGraph_SaveLoadBool_Save_Out_59;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Load_Out += SubGraph_SaveLoadBool_Load_Out_59;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_59;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.Out += SubGraph_LoadObjectiveStates_Out_62;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.Save_Out += SubGraph_SaveLoadInt_Save_Out_63;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.Load_Out += SubGraph_SaveLoadInt_Load_Out_63;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Save_Out += SubGraph_SaveLoadBool_Save_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Load_Out += SubGraph_SaveLoadBool_Load_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_69;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.Out += SubGraph_CompleteObjectiveStage_Out_78;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_190.Out += SubGraph_CompleteObjectiveStage_Out_190;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_190.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_45.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_184.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_190.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.OnDisable();
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_26.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_29.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_33.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_40.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.OnDisable();
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_71.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_190.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_190.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_190.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Save_Out -= SubGraph_SaveLoadBool_Save_Out_10;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Load_Out -= SubGraph_SaveLoadBool_Load_Out_10;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_10;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Save_Out -= SubGraph_SaveLoadBool_Save_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Load_Out -= SubGraph_SaveLoadBool_Load_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Save_Out -= SubGraph_SaveLoadBool_Save_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Load_Out -= SubGraph_SaveLoadBool_Load_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Save_Out -= SubGraph_SaveLoadBool_Save_Out_59;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Load_Out -= SubGraph_SaveLoadBool_Load_Out_59;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_59;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.Out -= SubGraph_LoadObjectiveStates_Out_62;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.Save_Out -= SubGraph_SaveLoadInt_Save_Out_63;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.Load_Out -= SubGraph_SaveLoadInt_Load_Out_63;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Save_Out -= SubGraph_SaveLoadBool_Save_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Load_Out -= SubGraph_SaveLoadBool_Load_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_69;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.Out -= SubGraph_CompleteObjectiveStage_Out_78;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_190.Out -= SubGraph_CompleteObjectiveStage_Out_190;
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

	private void Instance_SaveEvent_7(object o, EventArgs e)
	{
		Relay_SaveEvent_7();
	}

	private void Instance_LoadEvent_7(object o, EventArgs e)
	{
		Relay_LoadEvent_7();
	}

	private void Instance_RestartEvent_7(object o, EventArgs e)
	{
		Relay_RestartEvent_7();
	}

	private void SubGraph_SaveLoadBool_Save_Out_10(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_10 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_10;
		Relay_Save_Out_10();
	}

	private void SubGraph_SaveLoadBool_Load_Out_10(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_10 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_10;
		Relay_Load_Out_10();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_10(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_10 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_10;
		Relay_Restart_Out_10();
	}

	private void SubGraph_SaveLoadBool_Save_Out_13(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_13;
		Relay_Save_Out_13();
	}

	private void SubGraph_SaveLoadBool_Load_Out_13(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_13;
		Relay_Load_Out_13();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_13(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_13;
		Relay_Restart_Out_13();
	}

	private void SubGraph_SaveLoadBool_Save_Out_25(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_25;
		Relay_Save_Out_25();
	}

	private void SubGraph_SaveLoadBool_Load_Out_25(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_25;
		Relay_Load_Out_25();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_25(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_25;
		Relay_Restart_Out_25();
	}

	private void SubGraph_SaveLoadBool_Save_Out_59(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_59;
		Relay_Save_Out_59();
	}

	private void SubGraph_SaveLoadBool_Load_Out_59(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_59;
		Relay_Load_Out_59();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_59(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_59;
		Relay_Restart_Out_59();
	}

	private void SubGraph_LoadObjectiveStates_Out_62(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_62();
	}

	private void SubGraph_SaveLoadInt_Save_Out_63(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_63 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_63;
		Relay_Save_Out_63();
	}

	private void SubGraph_SaveLoadInt_Load_Out_63(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_63 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_63;
		Relay_Load_Out_63();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_63(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_63 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_63;
		Relay_Restart_Out_63();
	}

	private void SubGraph_SaveLoadBool_Save_Out_69(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_69;
		Relay_Save_Out_69();
	}

	private void SubGraph_SaveLoadBool_Load_Out_69(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_69;
		Relay_Load_Out_69();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_69(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_69;
		Relay_Restart_Out_69();
	}

	private void SubGraph_CompleteObjectiveStage_Out_78(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_78 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_78;
		Relay_Out_78();
	}

	private void SubGraph_CompleteObjectiveStage_Out_190(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_190 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_190;
		Relay_Out_190();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_16();
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

	private void Relay_SaveEvent_7()
	{
		Relay_Save_63();
	}

	private void Relay_LoadEvent_7()
	{
		Relay_Load_63();
	}

	private void Relay_RestartEvent_7()
	{
		Relay_Restart_63();
	}

	private void Relay_Save_Out_10()
	{
		Relay_Save_25();
	}

	private void Relay_Load_Out_10()
	{
		Relay_Load_25();
	}

	private void Relay_Restart_Out_10()
	{
		Relay_Set_False_25();
	}

	private void Relay_Save_10()
	{
		logic_SubGraph_SaveLoadBool_boolean_10 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_10 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Save(ref logic_SubGraph_SaveLoadBool_boolean_10, logic_SubGraph_SaveLoadBool_boolAsVariable_10, logic_SubGraph_SaveLoadBool_uniqueID_10);
	}

	private void Relay_Load_10()
	{
		logic_SubGraph_SaveLoadBool_boolean_10 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_10 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Load(ref logic_SubGraph_SaveLoadBool_boolean_10, logic_SubGraph_SaveLoadBool_boolAsVariable_10, logic_SubGraph_SaveLoadBool_uniqueID_10);
	}

	private void Relay_Set_True_10()
	{
		logic_SubGraph_SaveLoadBool_boolean_10 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_10 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_10, logic_SubGraph_SaveLoadBool_boolAsVariable_10, logic_SubGraph_SaveLoadBool_uniqueID_10);
	}

	private void Relay_Set_False_10()
	{
		logic_SubGraph_SaveLoadBool_boolean_10 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_10 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_10.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_10, logic_SubGraph_SaveLoadBool_boolAsVariable_10, logic_SubGraph_SaveLoadBool_uniqueID_10);
	}

	private void Relay_In_11()
	{
		logic_uScriptCon_CompareBool_Bool_11 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11.In(logic_uScriptCon_CompareBool_Bool_11);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11.False;
		if (num)
		{
			Relay_In_23();
		}
		if (flag)
		{
			Relay_In_184();
		}
	}

	private void Relay_True_12()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_12.True(out logic_uScriptAct_SetBool_Target_12);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_12;
	}

	private void Relay_False_12()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_12.False(out logic_uScriptAct_SetBool_Target_12);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_12;
	}

	private void Relay_Save_Out_13()
	{
		Relay_Save_10();
	}

	private void Relay_Load_Out_13()
	{
		Relay_Load_10();
	}

	private void Relay_Restart_Out_13()
	{
		Relay_Set_False_10();
	}

	private void Relay_Save_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Save(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_Load_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Load(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_Set_True_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_Set_False_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_In_16()
	{
		logic_uScriptCon_CompareBool_Bool_16 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.In(logic_uScriptCon_CompareBool_Bool_16);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.False;
		if (num)
		{
			Relay_In_81();
		}
		if (flag)
		{
			Relay_In_11();
		}
	}

	private void Relay_True_18()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_18.True(out logic_uScriptAct_SetBool_Target_18);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_18;
	}

	private void Relay_False_18()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_18.False(out logic_uScriptAct_SetBool_Target_18);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_18;
	}

	private void Relay_InitialSpawn_19()
	{
		int num = 0;
		Array nPCTech = NPCTech;
		if (logic_uScript_SpawnTechsFromData_spawnData_19.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_19, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_uScript_SpawnTechsFromData_spawnData_19, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_19 = owner_Connection_20;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_19.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_19, logic_uScript_SpawnTechsFromData_ownerNode_19, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_19);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_19.Out)
		{
			Relay_True_18();
		}
	}

	private void Relay_In_23()
	{
		logic_uScriptCon_CompareBool_Bool_23 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.In(logic_uScriptCon_CompareBool_Bool_23);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.False;
		if (num)
		{
			Relay_True_12();
		}
		if (flag)
		{
			Relay_In_39();
		}
	}

	private void Relay_Save_Out_25()
	{
		Relay_Save_59();
	}

	private void Relay_Load_Out_25()
	{
		Relay_Load_59();
	}

	private void Relay_Restart_Out_25()
	{
		Relay_Set_False_59();
	}

	private void Relay_Save_25()
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_25 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Save(ref logic_SubGraph_SaveLoadBool_boolean_25, logic_SubGraph_SaveLoadBool_boolAsVariable_25, logic_SubGraph_SaveLoadBool_uniqueID_25);
	}

	private void Relay_Load_25()
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_25 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Load(ref logic_SubGraph_SaveLoadBool_boolean_25, logic_SubGraph_SaveLoadBool_boolAsVariable_25, logic_SubGraph_SaveLoadBool_uniqueID_25);
	}

	private void Relay_Set_True_25()
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_25 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_25, logic_SubGraph_SaveLoadBool_boolAsVariable_25, logic_SubGraph_SaveLoadBool_uniqueID_25);
	}

	private void Relay_Set_False_25()
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_25 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_25, logic_SubGraph_SaveLoadBool_boolAsVariable_25, logic_SubGraph_SaveLoadBool_uniqueID_25);
	}

	private void Relay_In_26()
	{
		logic_uScript_InRangeOfTech_tank_26 = local_NPCTank_Tank;
		logic_uScript_InRangeOfTech_range_26 = DistInRangeOfNPC;
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_26.In(logic_uScript_InRangeOfTech_tank_26, logic_uScript_InRangeOfTech_range_26);
		bool inRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_26.InRange;
		bool outOfRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_26.OutOfRange;
		if (inRange)
		{
			Relay_In_80();
		}
		if (outOfRange)
		{
			Relay_In_56();
		}
	}

	private void Relay_In_29()
	{
		int num = 0;
		Array array = msgNPCGreeting;
		if (logic_uScript_AddOnScreenMessage_locString_29.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_29, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_29, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_29 = local_msgMeeting_System_String;
		logic_uScript_AddOnScreenMessage_speaker_29 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_29 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_29.In(logic_uScript_AddOnScreenMessage_locString_29, logic_uScript_AddOnScreenMessage_msgPriority_29, logic_uScript_AddOnScreenMessage_holdMsg_29, logic_uScript_AddOnScreenMessage_tag_29, logic_uScript_AddOnScreenMessage_speaker_29, logic_uScript_AddOnScreenMessage_side_29);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_29.Shown)
		{
			Relay_In_190();
		}
	}

	private void Relay_In_33()
	{
		logic_uScript_SetTankInvulnerable_tank_33 = local_NPCTank_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_33.In(logic_uScript_SetTankInvulnerable_invulnerable_33, logic_uScript_SetTankInvulnerable_tank_33);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_33.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_AtIndex_36()
	{
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_AccessListTech_techList_36.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_36, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_36, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_36.AtIndex(ref logic_uScript_AccessListTech_techList_36, logic_uScript_AccessListTech_index_36, out logic_uScript_AccessListTech_value_36);
		local_NPCTanks_TankArray = logic_uScript_AccessListTech_techList_36;
		local_NPCTank_Tank = logic_uScript_AccessListTech_value_36;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_36.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_In_39()
	{
		int num = 0;
		Array nPCTech = NPCTech;
		if (logic_uScript_GetAndCheckTechs_techData_39.Length != num + nPCTech.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_39, num + nPCTech.Length);
		}
		Array.Copy(nPCTech, 0, logic_uScript_GetAndCheckTechs_techData_39, num, nPCTech.Length);
		num += nPCTech.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_39 = owner_Connection_38;
		int num2 = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_39.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_39, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_39, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_39 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_39.In(logic_uScript_GetAndCheckTechs_techData_39, logic_uScript_GetAndCheckTechs_ownerNode_39, ref logic_uScript_GetAndCheckTechs_techs_39);
		local_NPCTanks_TankArray = logic_uScript_GetAndCheckTechs_techs_39;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_39.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_39.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_36();
		}
		if (someAlive)
		{
			Relay_AtIndex_36();
		}
	}

	private void Relay_In_40()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_40 = owner_Connection_41;
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_40.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_40, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_40, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_40 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_40.In(logic_uScript_SetOneTechAsEncounterTarget_owner_40, logic_uScript_SetOneTechAsEncounterTarget_techs_40);
		local_NPCTank_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_40;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_40.Out)
		{
			Relay_In_64();
		}
	}

	private void Relay_True_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.True(out logic_uScriptAct_SetBool_Target_44);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_44;
	}

	private void Relay_False_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.False(out logic_uScriptAct_SetBool_Target_44);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_44;
	}

	private void Relay_In_45()
	{
		logic_uScript_FlyTechUpAndAway_tech_45 = local_NPCTank_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_45 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_45 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_45.In(logic_uScript_FlyTechUpAndAway_tech_45, logic_uScript_FlyTechUpAndAway_maxLifetime_45, logic_uScript_FlyTechUpAndAway_targetHeight_45, logic_uScript_FlyTechUpAndAway_aiTree_45, logic_uScript_FlyTechUpAndAway_removalParticles_45);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_45.Out)
		{
			Relay_True_44();
		}
	}

	private void Relay_In_50()
	{
		int num = 0;
		Array array = msgNPCGreetingInturrupt;
		if (logic_uScript_AddOnScreenMessage_locString_50.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_50, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_50, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_50 = local_MsgInturrupted_System_String;
		logic_uScript_AddOnScreenMessage_speaker_50 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_50 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50.In(logic_uScript_AddOnScreenMessage_locString_50, logic_uScript_AddOnScreenMessage_msgPriority_50, logic_uScript_AddOnScreenMessage_holdMsg_50, logic_uScript_AddOnScreenMessage_tag_50, logic_uScript_AddOnScreenMessage_speaker_50, logic_uScript_AddOnScreenMessage_side_50);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50.Shown)
		{
			Relay_In_45();
		}
	}

	private void Relay_True_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.True(out logic_uScriptAct_SetBool_Target_53);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_53;
	}

	private void Relay_False_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.False(out logic_uScriptAct_SetBool_Target_53);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_53;
	}

	private void Relay_In_54()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_54 = local_msgMeeting_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_54.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_54, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_54);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_54.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_56()
	{
		logic_uScriptCon_CompareBool_Bool_56 = local_NPCSeen_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_56.In(logic_uScriptCon_CompareBool_Bool_56);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_56.True)
		{
			Relay_True_66();
		}
	}

	private void Relay_Save_Out_59()
	{
		Relay_Save_69();
	}

	private void Relay_Load_Out_59()
	{
		Relay_Load_69();
	}

	private void Relay_Restart_Out_59()
	{
		Relay_Set_False_69();
	}

	private void Relay_Save_59()
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_59 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Save(ref logic_SubGraph_SaveLoadBool_boolean_59, logic_SubGraph_SaveLoadBool_boolAsVariable_59, logic_SubGraph_SaveLoadBool_uniqueID_59);
	}

	private void Relay_Load_59()
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_59 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Load(ref logic_SubGraph_SaveLoadBool_boolean_59, logic_SubGraph_SaveLoadBool_boolAsVariable_59, logic_SubGraph_SaveLoadBool_uniqueID_59);
	}

	private void Relay_Set_True_59()
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_59 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_59, logic_SubGraph_SaveLoadBool_boolAsVariable_59, logic_SubGraph_SaveLoadBool_uniqueID_59);
	}

	private void Relay_Set_False_59()
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_59 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_59, logic_SubGraph_SaveLoadBool_boolAsVariable_59, logic_SubGraph_SaveLoadBool_uniqueID_59);
	}

	private void Relay_Out_62()
	{
	}

	private void Relay_In_62()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_62 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.In(logic_SubGraph_LoadObjectiveStates_currentObjective_62);
	}

	private void Relay_Save_Out_63()
	{
		Relay_Save_13();
	}

	private void Relay_Load_Out_63()
	{
		Relay_Load_13();
	}

	private void Relay_Restart_Out_63()
	{
		Relay_Set_False_13();
	}

	private void Relay_Save_63()
	{
		logic_SubGraph_SaveLoadInt_integer_63 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_63 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.Save(logic_SubGraph_SaveLoadInt_restartValue_63, ref logic_SubGraph_SaveLoadInt_integer_63, logic_SubGraph_SaveLoadInt_intAsVariable_63, logic_SubGraph_SaveLoadInt_uniqueID_63);
	}

	private void Relay_Load_63()
	{
		logic_SubGraph_SaveLoadInt_integer_63 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_63 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.Load(logic_SubGraph_SaveLoadInt_restartValue_63, ref logic_SubGraph_SaveLoadInt_integer_63, logic_SubGraph_SaveLoadInt_intAsVariable_63, logic_SubGraph_SaveLoadInt_uniqueID_63);
	}

	private void Relay_Restart_63()
	{
		logic_SubGraph_SaveLoadInt_integer_63 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_63 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_63.Restart(logic_SubGraph_SaveLoadInt_restartValue_63, ref logic_SubGraph_SaveLoadInt_integer_63, logic_SubGraph_SaveLoadInt_intAsVariable_63, logic_SubGraph_SaveLoadInt_uniqueID_63);
	}

	private void Relay_In_64()
	{
		logic_uScriptCon_CompareBool_Bool_64 = local_NPCIgnored_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.In(logic_uScriptCon_CompareBool_Bool_64);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.False;
		if (num)
		{
			Relay_In_54();
		}
		if (flag)
		{
			Relay_In_26();
		}
	}

	private void Relay_True_66()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.True(out logic_uScriptAct_SetBool_Target_66);
		local_NPCIgnored_System_Boolean = logic_uScriptAct_SetBool_Target_66;
	}

	private void Relay_False_66()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.False(out logic_uScriptAct_SetBool_Target_66);
		local_NPCIgnored_System_Boolean = logic_uScriptAct_SetBool_Target_66;
	}

	private void Relay_Save_Out_69()
	{
	}

	private void Relay_Load_Out_69()
	{
		Relay_In_62();
	}

	private void Relay_Restart_Out_69()
	{
	}

	private void Relay_Save_69()
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_69 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Save(ref logic_SubGraph_SaveLoadBool_boolean_69, logic_SubGraph_SaveLoadBool_boolAsVariable_69, logic_SubGraph_SaveLoadBool_uniqueID_69);
	}

	private void Relay_Load_69()
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_69 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Load(ref logic_SubGraph_SaveLoadBool_boolean_69, logic_SubGraph_SaveLoadBool_boolAsVariable_69, logic_SubGraph_SaveLoadBool_uniqueID_69);
	}

	private void Relay_Set_True_69()
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_69 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_69, logic_SubGraph_SaveLoadBool_boolAsVariable_69, logic_SubGraph_SaveLoadBool_uniqueID_69);
	}

	private void Relay_Set_False_69()
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_69 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_69, logic_SubGraph_SaveLoadBool_boolAsVariable_69, logic_SubGraph_SaveLoadBool_uniqueID_69);
	}

	private void Relay_In_71()
	{
		logic_uScript_InRangeOfTech_tank_71 = local_NPCTank_Tank;
		logic_uScript_InRangeOfTech_range_71 = TotallyOutOfRangeOfNPC;
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_71.In(logic_uScript_InRangeOfTech_tank_71, logic_uScript_InRangeOfTech_range_71);
		bool inRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_71.InRange;
		bool outOfRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_71.OutOfRange;
		if (inRange)
		{
			Relay_In_50();
		}
		if (outOfRange)
		{
			Relay_In_74();
		}
	}

	private void Relay_In_74()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_74 = local_MsgInturrupted_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_74.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_74, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_74);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_74.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_Out_78()
	{
		Relay_True_53();
	}

	private void Relay_In_78()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_78 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_78, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_78);
	}

	private void Relay_In_80()
	{
		logic_uScriptCon_CompareBool_Bool_80 = local_NPCSeen_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.In(logic_uScriptCon_CompareBool_Bool_80);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.False;
		if (num)
		{
			Relay_In_29();
		}
		if (flag)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_81()
	{
		logic_uScript_ClearEncounterTarget_owner_81 = owner_Connection_180;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_81.In(logic_uScript_ClearEncounterTarget_owner_81);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_81.Out)
		{
			Relay_Succeed_2();
		}
	}

	private void Relay_In_184()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_184 = owner_Connection_185;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_184.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_184);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_184.Out)
		{
			Relay_InitialSpawn_19();
		}
	}

	private void Relay_Out_190()
	{
		Relay_In_45();
	}

	private void Relay_In_190()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_190 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_190.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_190, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_190);
	}
}
