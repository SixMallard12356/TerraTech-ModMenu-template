using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Destroy Fleeing Tech", "")]
public class Mission_DestroyFleeingTech : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius = 50f;

	public SpawnTechData[] enemyTechData = new SpawnTechData[0];

	private Tank local_44_Tank;

	private float local_DistEnemiesSpotted_System_Single = 200f;

	private float local_DistLostEnemy_System_Single = 300f;

	private bool local_EnemySpawned_System_Boolean;

	private Tank local_EnemyTech_Tank;

	private Tank[] local_EnemyTechs_TankArray = new Tank[0];

	private int local_Stage_System_Int32 = 1;

	private float local_TimeRemaining_System_Single;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgEnemiesSpotted = new LocalisedString[0];

	public LocalisedString[] msgFailedLostThem = new LocalisedString[0];

	public LocalisedString[] msgFailedOutOfTime = new LocalisedString[0];

	public LocalisedString[] msgMissionComplete = new LocalisedString[0];

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_16;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_26;

	private GameObject owner_Connection_46;

	private GameObject owner_Connection_47;

	private GameObject owner_Connection_53;

	private GameObject owner_Connection_63;

	private GameObject owner_Connection_66;

	private GameObject owner_Connection_74;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_6;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_6 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_6 = "EnemySpawned";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_7;

	private bool logic_uScriptCon_CompareBool_True_7 = true;

	private bool logic_uScriptCon_CompareBool_False_7 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_8 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_8 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_8;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_8 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_8 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_10 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_10 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_10;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_10 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_10;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_10 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_10 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_10 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_10 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_13 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_13;

	private bool logic_uScriptAct_SetBool_Out_13 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_13 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_13 = true;

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_19 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_19 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_19;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_19 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_19 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_19 = true;

	private uScript_GetEncounterTimeRemaining logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_21 = new uScript_GetEncounterTimeRemaining();

	private GameObject logic_uScript_GetEncounterTimeRemaining_owner_21;

	private float logic_uScript_GetEncounterTimeRemaining_Return_21;

	private bool logic_uScript_GetEncounterTimeRemaining_Out_21 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_25 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_25;

	private bool logic_uScript_FinishEncounter_Out_25 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_27 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_27;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_27 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_27 = "Stage";

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_29 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_29;

	private float logic_uScriptCon_CompareFloat_B_29;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_29 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_29 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_29 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_29 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_29 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_29 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_30 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_30;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_32 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_32 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_32;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_32;

	private string logic_uScript_AddOnScreenMessage_tag_32 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_32;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_32;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_32;

	private bool logic_uScript_AddOnScreenMessage_Out_32 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_32 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_33 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_33 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_33;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_33;

	private string logic_uScript_AddOnScreenMessage_tag_33 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_33;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_33;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_33;

	private bool logic_uScript_AddOnScreenMessage_Out_33 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_33 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_36 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_36;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_36;

	private string logic_uScript_AddOnScreenMessage_tag_36 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_36;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_36;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_36;

	private bool logic_uScript_AddOnScreenMessage_Out_36 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_36 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_41;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_45 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_45;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_45 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_45;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_45 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_48 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_48;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_48 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_48 = true;

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_51 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_51 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_51;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_51 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_51 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_51 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_52;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_52;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_54 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_54 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_54 = -1;

	private bool logic_uScript_SetTechsTeam_Out_54 = true;

	private uScript_StartEncounterTimer logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_55 = new uScript_StartEncounterTimer();

	private GameObject logic_uScript_StartEncounterTimer_owner_55;

	private bool logic_uScript_StartEncounterTimer_Out_55 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_59 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_59 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_59 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_59;

	private string logic_uScript_AddOnScreenMessage_tag_59 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_59;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_59;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_59;

	private bool logic_uScript_AddOnScreenMessage_Out_59 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_59 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_65 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_65;

	private bool logic_uScript_FinishEncounter_Out_65 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_67 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_67;

	private string logic_uScript_RemoveScenery_positionName_67 = "";

	private float logic_uScript_RemoveScenery_radius_67;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_67;

	private bool logic_uScript_RemoveScenery_Out_67 = true;

	private uScript_HasTechBeenDamagedByPlayer logic_uScript_HasTechBeenDamagedByPlayer_uScript_HasTechBeenDamagedByPlayer_70 = new uScript_HasTechBeenDamagedByPlayer();

	private Tank logic_uScript_HasTechBeenDamagedByPlayer_tech_70;

	private bool logic_uScript_HasTechBeenDamagedByPlayer_Damaged_70 = true;

	private bool logic_uScript_HasTechBeenDamagedByPlayer_NotDamaged_70 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_71 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_71 = new Tank[0];

	private int logic_uScript_AccessListTech_index_71;

	private Tank logic_uScript_AccessListTech_value_71;

	private bool logic_uScript_AccessListTech_Out_71 = true;

	private uScript_RemoveEncounterTimer logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_75 = new uScript_RemoveEncounterTimer();

	private GameObject logic_uScript_RemoveEncounterTimer_owner_75;

	private bool logic_uScript_RemoveEncounterTimer_Out_75 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_76 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_76 = true;

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
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_3;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_3;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_3;
				}
			}
		}
		if (null == owner_Connection_16 || !m_RegisteredForEvents)
		{
			owner_Connection_16 = parentGameObject;
			if (null != owner_Connection_16)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_16.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_16.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_15;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_15;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_15;
				}
			}
		}
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
		}
		if (null == owner_Connection_26 || !m_RegisteredForEvents)
		{
			owner_Connection_26 = parentGameObject;
		}
		if (null == owner_Connection_46 || !m_RegisteredForEvents)
		{
			owner_Connection_46 = parentGameObject;
		}
		if (null == owner_Connection_47 || !m_RegisteredForEvents)
		{
			owner_Connection_47 = parentGameObject;
		}
		if (null == owner_Connection_53 || !m_RegisteredForEvents)
		{
			owner_Connection_53 = parentGameObject;
		}
		if (null == owner_Connection_63 || !m_RegisteredForEvents)
		{
			owner_Connection_63 = parentGameObject;
		}
		if (null == owner_Connection_66 || !m_RegisteredForEvents)
		{
			owner_Connection_66 = parentGameObject;
		}
		if (null == owner_Connection_74 || !m_RegisteredForEvents)
		{
			owner_Connection_74 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_5)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_5.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_5.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_3;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_3;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_3;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_16)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_16.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_16.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_15;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_15;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_15;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_5)
		{
			uScript_SaveLoad component = owner_Connection_5.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_3;
				component.LoadEvent -= Instance_LoadEvent_3;
				component.RestartEvent -= Instance_RestartEvent_3;
			}
		}
		if (null != owner_Connection_16)
		{
			uScript_EncounterUpdate component2 = owner_Connection_16.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_15;
				component2.OnSuspend -= Instance_OnSuspend_15;
				component2.OnResume -= Instance_OnResume_15;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_8.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_10.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_19.SetParent(g);
		logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_21.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_25.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_29.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_30.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_32.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_33.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_45.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_48.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_51.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_54.SetParent(g);
		logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_55.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_59.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_65.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_67.SetParent(g);
		logic_uScript_HasTechBeenDamagedByPlayer_uScript_HasTechBeenDamagedByPlayer_70.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_71.SetParent(g);
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_75.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_76.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_2 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_16 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_26 = parentGameObject;
		owner_Connection_46 = parentGameObject;
		owner_Connection_47 = parentGameObject;
		owner_Connection_53 = parentGameObject;
		owner_Connection_63 = parentGameObject;
		owner_Connection_66 = parentGameObject;
		owner_Connection_74 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_30.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save_Out += SubGraph_SaveLoadBool_Save_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load_Out += SubGraph_SaveLoadBool_Load_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_6;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Save_Out += SubGraph_SaveLoadInt_Save_Out_27;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Load_Out += SubGraph_SaveLoadInt_Load_Out_27;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_27;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_30.Out += SubGraph_LoadObjectiveStates_Out_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output1 += uScriptCon_ManualSwitch_Output1_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output2 += uScriptCon_ManualSwitch_Output2_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output3 += uScriptCon_ManualSwitch_Output3_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output4 += uScriptCon_ManualSwitch_Output4_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output5 += uScriptCon_ManualSwitch_Output5_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output6 += uScriptCon_ManualSwitch_Output6_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output7 += uScriptCon_ManualSwitch_Output7_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output8 += uScriptCon_ManualSwitch_Output8_41;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.Out += SubGraph_CompleteObjectiveStage_Out_52;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_30.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_30.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_19.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_30.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_32.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_33.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_45.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_51.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_59.OnDisable();
		logic_uScript_HasTechBeenDamagedByPlayer_uScript_HasTechBeenDamagedByPlayer_70.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_30.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_30.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save_Out -= SubGraph_SaveLoadBool_Save_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load_Out -= SubGraph_SaveLoadBool_Load_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_6;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Save_Out -= SubGraph_SaveLoadInt_Save_Out_27;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Load_Out -= SubGraph_SaveLoadInt_Load_Out_27;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_27;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_30.Out -= SubGraph_LoadObjectiveStates_Out_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output1 -= uScriptCon_ManualSwitch_Output1_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output2 -= uScriptCon_ManualSwitch_Output2_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output3 -= uScriptCon_ManualSwitch_Output3_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output4 -= uScriptCon_ManualSwitch_Output4_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output5 -= uScriptCon_ManualSwitch_Output5_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output6 -= uScriptCon_ManualSwitch_Output6_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output7 -= uScriptCon_ManualSwitch_Output7_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output8 -= uScriptCon_ManualSwitch_Output8_41;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.Out -= SubGraph_CompleteObjectiveStage_Out_52;
	}

	private void Instance_SaveEvent_3(object o, EventArgs e)
	{
		Relay_SaveEvent_3();
	}

	private void Instance_LoadEvent_3(object o, EventArgs e)
	{
		Relay_LoadEvent_3();
	}

	private void Instance_RestartEvent_3(object o, EventArgs e)
	{
		Relay_RestartEvent_3();
	}

	private void Instance_OnUpdate_15(object o, EventArgs e)
	{
		Relay_OnUpdate_15();
	}

	private void Instance_OnSuspend_15(object o, EventArgs e)
	{
		Relay_OnSuspend_15();
	}

	private void Instance_OnResume_15(object o, EventArgs e)
	{
		Relay_OnResume_15();
	}

	private void SubGraph_SaveLoadBool_Save_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Save_Out_6();
	}

	private void SubGraph_SaveLoadBool_Load_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Load_Out_6();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Restart_Out_6();
	}

	private void SubGraph_SaveLoadInt_Save_Out_27(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_27 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_27;
		Relay_Save_Out_27();
	}

	private void SubGraph_SaveLoadInt_Load_Out_27(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_27 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_27;
		Relay_Load_Out_27();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_27(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_27 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_27;
		Relay_Restart_Out_27();
	}

	private void SubGraph_LoadObjectiveStates_Out_30(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_30();
	}

	private void uScriptCon_ManualSwitch_Output1_41(object o, EventArgs e)
	{
		Relay_Output1_41();
	}

	private void uScriptCon_ManualSwitch_Output2_41(object o, EventArgs e)
	{
		Relay_Output2_41();
	}

	private void uScriptCon_ManualSwitch_Output3_41(object o, EventArgs e)
	{
		Relay_Output3_41();
	}

	private void uScriptCon_ManualSwitch_Output4_41(object o, EventArgs e)
	{
		Relay_Output4_41();
	}

	private void uScriptCon_ManualSwitch_Output5_41(object o, EventArgs e)
	{
		Relay_Output5_41();
	}

	private void uScriptCon_ManualSwitch_Output6_41(object o, EventArgs e)
	{
		Relay_Output6_41();
	}

	private void uScriptCon_ManualSwitch_Output7_41(object o, EventArgs e)
	{
		Relay_Output7_41();
	}

	private void uScriptCon_ManualSwitch_Output8_41(object o, EventArgs e)
	{
		Relay_Output8_41();
	}

	private void SubGraph_CompleteObjectiveStage_Out_52(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_52 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_52;
		Relay_Out_52();
	}

	private void Relay_SaveEvent_3()
	{
		Relay_Save_6();
	}

	private void Relay_LoadEvent_3()
	{
		Relay_Load_6();
	}

	private void Relay_RestartEvent_3()
	{
		Relay_Set_False_6();
	}

	private void Relay_Save_Out_6()
	{
		Relay_Save_27();
	}

	private void Relay_Load_Out_6()
	{
		Relay_Load_27();
	}

	private void Relay_Restart_Out_6()
	{
		Relay_Restart_27();
	}

	private void Relay_Save_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Load_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Set_True_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Set_False_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_In_7()
	{
		logic_uScriptCon_CompareBool_Bool_7 = local_EnemySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.In(logic_uScriptCon_CompareBool_Bool_7);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.False;
		if (num)
		{
			Relay_In_10();
		}
		if (flag)
		{
			Relay_True_13();
		}
	}

	private void Relay_InitialSpawn_8()
	{
		int num = 0;
		Array array = enemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_8.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_8, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_8, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_8 = owner_Connection_1;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_8.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_8, logic_uScript_SpawnTechsFromData_ownerNode_8, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_8);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_8.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_In_10()
	{
		int num = 0;
		Array array = enemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_10.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_10, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_10, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_10 = owner_Connection_2;
		int num2 = 0;
		Array array2 = local_EnemyTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_10.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_10, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_10, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_10 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_10.In(logic_uScript_GetAndCheckTechs_techData_10, logic_uScript_GetAndCheckTechs_ownerNode_10, ref logic_uScript_GetAndCheckTechs_techs_10);
		local_EnemyTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_10;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_10.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_10.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_10.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_71();
		}
		if (someAlive)
		{
			Relay_AtIndex_71();
		}
		if (allDead)
		{
			Relay_In_75();
		}
	}

	private void Relay_True_13()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.True(out logic_uScriptAct_SetBool_Target_13);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_13;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_13.Out)
		{
			Relay_InitialSpawn_8();
		}
	}

	private void Relay_False_13()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.False(out logic_uScriptAct_SetBool_Target_13);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_13;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_13.Out)
		{
			Relay_InitialSpawn_8();
		}
	}

	private void Relay_OnUpdate_15()
	{
		Relay_In_7();
	}

	private void Relay_OnSuspend_15()
	{
	}

	private void Relay_OnResume_15()
	{
	}

	private void Relay_In_19()
	{
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_19.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_19, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_19, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_range_19 = local_DistLostEnemy_System_Single;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_19.In(logic_uScript_InRangeOfAtLeastOneTech_techs_19, logic_uScript_InRangeOfAtLeastOneTech_range_19);
		bool inRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_19.InRange;
		bool outOfRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_19.OutOfRange;
		if (inRange)
		{
			Relay_TimeRemaining_21();
		}
		if (outOfRange)
		{
			Relay_In_36();
		}
	}

	private void Relay_TimeRemaining_21()
	{
		logic_uScript_GetEncounterTimeRemaining_owner_21 = owner_Connection_22;
		logic_uScript_GetEncounterTimeRemaining_Return_21 = logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_21.TimeRemaining(logic_uScript_GetEncounterTimeRemaining_owner_21);
		local_TimeRemaining_System_Single = logic_uScript_GetEncounterTimeRemaining_Return_21;
		if (logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_21.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_TimeRemainingPercent_21()
	{
		logic_uScript_GetEncounterTimeRemaining_owner_21 = owner_Connection_22;
		logic_uScript_GetEncounterTimeRemaining_Return_21 = logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_21.TimeRemainingPercent(logic_uScript_GetEncounterTimeRemaining_owner_21);
		local_TimeRemaining_System_Single = logic_uScript_GetEncounterTimeRemaining_Return_21;
		if (logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_21.Out)
		{
			Relay_In_29();
		}
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

	private void Relay_Save_Out_27()
	{
	}

	private void Relay_Load_Out_27()
	{
		Relay_In_30();
	}

	private void Relay_Restart_Out_27()
	{
	}

	private void Relay_Save_27()
	{
		logic_SubGraph_SaveLoadInt_integer_27 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_27 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Save(logic_SubGraph_SaveLoadInt_restartValue_27, ref logic_SubGraph_SaveLoadInt_integer_27, logic_SubGraph_SaveLoadInt_intAsVariable_27, logic_SubGraph_SaveLoadInt_uniqueID_27);
	}

	private void Relay_Load_27()
	{
		logic_SubGraph_SaveLoadInt_integer_27 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_27 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Load(logic_SubGraph_SaveLoadInt_restartValue_27, ref logic_SubGraph_SaveLoadInt_integer_27, logic_SubGraph_SaveLoadInt_intAsVariable_27, logic_SubGraph_SaveLoadInt_uniqueID_27);
	}

	private void Relay_Restart_27()
	{
		logic_SubGraph_SaveLoadInt_integer_27 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_27 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Restart(logic_SubGraph_SaveLoadInt_restartValue_27, ref logic_SubGraph_SaveLoadInt_integer_27, logic_SubGraph_SaveLoadInt_intAsVariable_27, logic_SubGraph_SaveLoadInt_uniqueID_27);
	}

	private void Relay_In_29()
	{
		logic_uScriptCon_CompareFloat_A_29 = local_TimeRemaining_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_29.In(logic_uScriptCon_CompareFloat_A_29, logic_uScriptCon_CompareFloat_B_29);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_29.LessThanOrEqualTo)
		{
			Relay_In_33();
		}
	}

	private void Relay_Out_30()
	{
	}

	private void Relay_In_30()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_30 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_30.In(logic_SubGraph_LoadObjectiveStates_currentObjective_30);
	}

	private void Relay_In_32()
	{
		int num = 0;
		Array array = msgMissionComplete;
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
			Relay_Succeed_65();
		}
	}

	private void Relay_In_33()
	{
		int num = 0;
		Array array = msgFailedOutOfTime;
		if (logic_uScript_AddOnScreenMessage_locString_33.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_33, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_33, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_33 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_33 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_33.In(logic_uScript_AddOnScreenMessage_locString_33, logic_uScript_AddOnScreenMessage_msgPriority_33, logic_uScript_AddOnScreenMessage_holdMsg_33, logic_uScript_AddOnScreenMessage_tag_33, logic_uScript_AddOnScreenMessage_speaker_33, logic_uScript_AddOnScreenMessage_side_33);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_33.Out)
		{
			Relay_Fail_25();
		}
	}

	private void Relay_In_36()
	{
		int num = 0;
		Array array = msgFailedLostThem;
		if (logic_uScript_AddOnScreenMessage_locString_36.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_36, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_36, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_36 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_36 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.In(logic_uScript_AddOnScreenMessage_locString_36, logic_uScript_AddOnScreenMessage_msgPriority_36, logic_uScript_AddOnScreenMessage_holdMsg_36, logic_uScript_AddOnScreenMessage_tag_36, logic_uScript_AddOnScreenMessage_speaker_36, logic_uScript_AddOnScreenMessage_side_36);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.Out)
		{
			Relay_Fail_25();
		}
	}

	private void Relay_Output1_41()
	{
		Relay_In_51();
	}

	private void Relay_Output2_41()
	{
		Relay_In_19();
	}

	private void Relay_Output3_41()
	{
	}

	private void Relay_Output4_41()
	{
	}

	private void Relay_Output5_41()
	{
	}

	private void Relay_Output6_41()
	{
	}

	private void Relay_Output7_41()
	{
	}

	private void Relay_Output8_41()
	{
	}

	private void Relay_In_41()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_41 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.In(logic_uScriptCon_ManualSwitch_CurrentOutput_41);
	}

	private void Relay_In_45()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_45 = owner_Connection_46;
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_45.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_45, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_45, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_45 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_45.In(logic_uScript_SetOneTechAsEncounterTarget_owner_45, logic_uScript_SetOneTechAsEncounterTarget_techs_45);
		local_44_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_45;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_45.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_In_48()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_48 = owner_Connection_47;
		logic_uScript_MoveEncounterWithVisible_visibleObject_48 = local_44_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_48.In(logic_uScript_MoveEncounterWithVisible_ownerNode_48, logic_uScript_MoveEncounterWithVisible_visibleObject_48);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_48.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_51()
	{
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_51.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_51, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_51, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_range_51 = local_DistEnemiesSpotted_System_Single;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_51.In(logic_uScript_InRangeOfAtLeastOneTech_techs_51, logic_uScript_InRangeOfAtLeastOneTech_range_51);
		if (logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_51.InRange)
		{
			Relay_In_59();
		}
	}

	private void Relay_Out_52()
	{
	}

	private void Relay_In_52()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_52 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_52, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_52);
	}

	private void Relay_In_54()
	{
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_54.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_54, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_54, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_54.In(ref logic_uScript_SetTechsTeam_techs_54, logic_uScript_SetTechsTeam_team_54);
		local_EnemyTechs_TankArray = logic_uScript_SetTechsTeam_techs_54;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_54.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_55()
	{
		logic_uScript_StartEncounterTimer_owner_55 = owner_Connection_53;
		logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_55.In(logic_uScript_StartEncounterTimer_owner_55);
		if (logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_55.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_59()
	{
		int num = 0;
		Array array = msgEnemiesSpotted;
		if (logic_uScript_AddOnScreenMessage_locString_59.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_59, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_59, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_59 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_59 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_59.In(logic_uScript_AddOnScreenMessage_locString_59, logic_uScript_AddOnScreenMessage_msgPriority_59, logic_uScript_AddOnScreenMessage_holdMsg_59, logic_uScript_AddOnScreenMessage_tag_59, logic_uScript_AddOnScreenMessage_speaker_59, logic_uScript_AddOnScreenMessage_side_59);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_59.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_Succeed_65()
	{
		logic_uScript_FinishEncounter_owner_65 = owner_Connection_63;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_65.Succeed(logic_uScript_FinishEncounter_owner_65);
	}

	private void Relay_Fail_65()
	{
		logic_uScript_FinishEncounter_owner_65 = owner_Connection_63;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_65.Fail(logic_uScript_FinishEncounter_owner_65);
	}

	private void Relay_In_67()
	{
		logic_uScript_RemoveScenery_ownerNode_67 = owner_Connection_66;
		logic_uScript_RemoveScenery_positionName_67 = clearSceneryPos;
		logic_uScript_RemoveScenery_radius_67 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_67.In(logic_uScript_RemoveScenery_ownerNode_67, logic_uScript_RemoveScenery_positionName_67, logic_uScript_RemoveScenery_radius_67, logic_uScript_RemoveScenery_preventChunksSpawning_67);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_67.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_70()
	{
		logic_uScript_HasTechBeenDamagedByPlayer_tech_70 = local_EnemyTech_Tank;
		logic_uScript_HasTechBeenDamagedByPlayer_uScript_HasTechBeenDamagedByPlayer_70.In(logic_uScript_HasTechBeenDamagedByPlayer_tech_70);
		bool damaged = logic_uScript_HasTechBeenDamagedByPlayer_uScript_HasTechBeenDamagedByPlayer_70.Damaged;
		bool notDamaged = logic_uScript_HasTechBeenDamagedByPlayer_uScript_HasTechBeenDamagedByPlayer_70.NotDamaged;
		if (damaged)
		{
			Relay_In_32();
		}
		if (notDamaged)
		{
			Relay_In_76();
		}
	}

	private void Relay_AtIndex_71()
	{
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_71.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_71, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_71, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_71.AtIndex(ref logic_uScript_AccessListTech_techList_71, logic_uScript_AccessListTech_index_71, out logic_uScript_AccessListTech_value_71);
		local_EnemyTechs_TankArray = logic_uScript_AccessListTech_techList_71;
		local_EnemyTech_Tank = logic_uScript_AccessListTech_value_71;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_71.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_75()
	{
		logic_uScript_RemoveEncounterTimer_owner_75 = owner_Connection_74;
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_75.In(logic_uScript_RemoveEncounterTimer_owner_75);
		if (logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_75.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_In_76()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_76.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_76.Out)
		{
			Relay_Fail_65();
		}
	}
}
