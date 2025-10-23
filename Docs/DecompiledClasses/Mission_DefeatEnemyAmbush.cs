using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_DefeatEnemyAmbush : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius = 50f;

	[Multiline(3)]
	public string EncounterCentralPos = "";

	public SpawnTechData[] enemyTechData = new SpawnTechData[0];

	public bool ItsATrap;

	private float local_85_System_Single;

	private Crate local_Crate_Crate;

	private bool local_CrateSpawned_System_Boolean;

	private bool local_MsgApproachCrateShown_System_Boolean;

	private bool local_ShownMsgTrapSprung_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgApproachCrate = new LocalisedString[0];

	public LocalisedString[] msgComplete = new LocalisedString[0];

	public LocalisedString[] msgEnemySpotted = new LocalisedString[0];

	public LocalisedString[] msgTrapSprung = new LocalisedString[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_23;

	private GameObject owner_Connection_37;

	private GameObject owner_Connection_43;

	private GameObject owner_Connection_44;

	private GameObject owner_Connection_56;

	private GameObject owner_Connection_57;

	private GameObject owner_Connection_67;

	private SubGraph_DefeatEnemyTechs logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5 = new SubGraph_DefeatEnemyTechs();

	private SpawnTechData[] logic_SubGraph_DefeatEnemyTechs_enemyTechData_5 = new SpawnTechData[0];

	private float logic_SubGraph_DefeatEnemyTechs_distEnemiesSpotted_5 = 999999f;

	private string logic_SubGraph_DefeatEnemyTechs_clearSceneryPos_5 = "";

	private float logic_SubGraph_DefeatEnemyTechs_clearSceneryRadius_5;

	private LocalisedString[] logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_5 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_DefeatEnemyTechs_msgComplete_5 = new LocalisedString[0];

	private ManOnScreenMessages.Speaker logic_SubGraph_DefeatEnemyTechs_messageSpeaker_5;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_7 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_7;

	private bool logic_uScriptAct_SetBool_Out_7 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_7 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_7 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_8;

	private bool logic_uScriptCon_CompareBool_True_8 = true;

	private bool logic_uScriptCon_CompareBool_False_8 = true;

	private uScript_SpawnDeliveryCrate logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_10 = new uScript_SpawnDeliveryCrate();

	private string logic_uScript_SpawnDeliveryCrate_positionName_10 = "";

	private GameObject logic_uScript_SpawnDeliveryCrate_ownerNode_10;

	private bool logic_uScript_SpawnDeliveryCrate_visibleOnRadar_10 = true;

	private bool logic_uScript_SpawnDeliveryCrate_Out_10 = true;

	private uScript_CheckDeliveryCrateSpawned logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_13 = new uScript_CheckDeliveryCrateSpawned();

	private GameObject logic_uScript_CheckDeliveryCrateSpawned_ownerNode_13;

	private bool logic_uScript_CheckDeliveryCrateSpawned_Out_13 = true;

	private bool logic_uScript_CheckDeliveryCrateSpawned_Yes_13 = true;

	private bool logic_uScript_CheckDeliveryCrateSpawned_No_13 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_15;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_15 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_15 = "CrateSpawned";

	private uScript_GetDeliveryCrate logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_21 = new uScript_GetDeliveryCrate();

	private GameObject logic_uScript_GetDeliveryCrate_ownerNode_21;

	private Crate logic_uScript_GetDeliveryCrate_Return_21;

	private bool logic_uScript_GetDeliveryCrate_Out_21 = true;

	private bool logic_uScript_GetDeliveryCrate_Success_21 = true;

	private bool logic_uScript_GetDeliveryCrate_Failure_21 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_22 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_22;

	private object logic_uScript_SetEncounterTarget_visibleObject_22 = "";

	private bool logic_uScript_SetEncounterTarget_Out_22 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_25 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_25 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_25;

	private string logic_uScript_AddOnScreenMessage_tag_25 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_25;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_25;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_25;

	private bool logic_uScript_AddOnScreenMessage_Out_25 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_25 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_27;

	private bool logic_uScriptCon_CompareBool_True_27 = true;

	private bool logic_uScriptCon_CompareBool_False_27 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_28 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_28;

	private bool logic_uScriptAct_SetBool_Out_28 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_28 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_28 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_31;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_31 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_31 = "ShownMsgTrapSprung";

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_32;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_34 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_34;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_34;

	private uScript_UnlockDeliveryCrate logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_36 = new uScript_UnlockDeliveryCrate();

	private GameObject logic_uScript_UnlockDeliveryCrate_ownerNode_36;

	private bool logic_uScript_UnlockDeliveryCrate_Out_36 = true;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_41 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_41 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_41;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_41 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_41 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_41 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_42 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_42;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_42 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_42 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_45 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_45;

	private bool logic_uScript_FinishEncounter_Out_45 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_46 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_46;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_46;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_48 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_48;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_48 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_48 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_50 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_50;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_54;

	private bool logic_uScriptCon_CompareBool_True_54 = true;

	private bool logic_uScriptCon_CompareBool_False_54 = true;

	private uScript_SetQuestObjectiveCompleted logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_55 = new uScript_SetQuestObjectiveCompleted();

	private GameObject logic_uScript_SetQuestObjectiveCompleted_owner_55;

	private int logic_uScript_SetQuestObjectiveCompleted_objectiveId_55 = 1;

	private bool logic_uScript_SetQuestObjectiveCompleted_completed_55 = true;

	private bool logic_uScript_SetQuestObjectiveCompleted_Out_55 = true;

	private uScript_SetQuestObjectiveVisible logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_58 = new uScript_SetQuestObjectiveVisible();

	private GameObject logic_uScript_SetQuestObjectiveVisible_owner_58;

	private int logic_uScript_SetQuestObjectiveVisible_objectiveId_58 = 3;

	private bool logic_uScript_SetQuestObjectiveVisible_visible_58 = true;

	private bool logic_uScript_SetQuestObjectiveVisible_Out_58 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_59 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_59 = 3;

	private int logic_uScriptAct_SetInt_Target_59;

	private bool logic_uScriptAct_SetInt_Out_59 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_68 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_68;

	private string logic_uScript_RemoveScenery_positionName_68 = "";

	private float logic_uScript_RemoveScenery_radius_68;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_68;

	private bool logic_uScript_RemoveScenery_Out_68 = true;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_73 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_73 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_73 = 100f;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_73 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_73 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_73 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_75;

	private bool logic_uScriptCon_CompareBool_True_75 = true;

	private bool logic_uScriptCon_CompareBool_False_75 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_76 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_76 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_76 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_76;

	private string logic_uScript_AddOnScreenMessage_tag_76 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_76;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_76;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_76;

	private bool logic_uScript_AddOnScreenMessage_Out_76 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_76 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_78 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_78;

	private bool logic_uScriptAct_SetBool_Out_78 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_78 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_78 = true;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_82 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_82 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_82 = 25f;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_82 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_82 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_82 = true;

	private uScript_GetCrateOpenTriggerRange logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_83 = new uScript_GetCrateOpenTriggerRange();

	private Crate logic_uScript_GetCrateOpenTriggerRange_crate_83;

	private float logic_uScript_GetCrateOpenTriggerRange_Return_83;

	private bool logic_uScript_GetCrateOpenTriggerRange_Out_83 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_86;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_86 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_86 = "MsgApproachCrateShown";

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
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
		}
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
			if (null != owner_Connection_17)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_17.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_17.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_16;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_16;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_16;
				}
			}
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (null == owner_Connection_23 || !m_RegisteredForEvents)
		{
			owner_Connection_23 = parentGameObject;
		}
		if (null == owner_Connection_37 || !m_RegisteredForEvents)
		{
			owner_Connection_37 = parentGameObject;
		}
		if (null == owner_Connection_43 || !m_RegisteredForEvents)
		{
			owner_Connection_43 = parentGameObject;
		}
		if (null == owner_Connection_44 || !m_RegisteredForEvents)
		{
			owner_Connection_44 = parentGameObject;
		}
		if (null == owner_Connection_56 || !m_RegisteredForEvents)
		{
			owner_Connection_56 = parentGameObject;
		}
		if (null == owner_Connection_57 || !m_RegisteredForEvents)
		{
			owner_Connection_57 = parentGameObject;
		}
		if (null == owner_Connection_67 || !m_RegisteredForEvents)
		{
			owner_Connection_67 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_17)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_17.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_17.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_16;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_16;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_16;
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
		if (null != owner_Connection_17)
		{
			uScript_SaveLoad component2 = owner_Connection_17.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_16;
				component2.LoadEvent -= Instance_LoadEvent_16;
				component2.RestartEvent -= Instance_RestartEvent_16;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.SetParent(g);
		logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_10.SetParent(g);
		logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_13.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.SetParent(g);
		logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_21.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_22.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_34.SetParent(g);
		logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_36.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_41.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_42.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_45.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_46.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_50.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.SetParent(g);
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_55.SetParent(g);
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_58.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_59.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_68.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_73.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_76.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_82.SetParent(g);
		logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_83.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_23 = parentGameObject;
		owner_Connection_37 = parentGameObject;
		owner_Connection_43 = parentGameObject;
		owner_Connection_44 = parentGameObject;
		owner_Connection_56 = parentGameObject;
		owner_Connection_57 = parentGameObject;
		owner_Connection_67 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_34.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_46.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_50.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Awake();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.Complete += SubGraph_DefeatEnemyTechs_Complete_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Save_Out += SubGraph_SaveLoadBool_Save_Out_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Load_Out += SubGraph_SaveLoadBool_Load_Out_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Save_Out += SubGraph_SaveLoadBool_Save_Out_31;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Load_Out += SubGraph_SaveLoadBool_Load_Out_31;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_31;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output1 += uScriptCon_ManualSwitch_Output1_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output2 += uScriptCon_ManualSwitch_Output2_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output3 += uScriptCon_ManualSwitch_Output3_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output4 += uScriptCon_ManualSwitch_Output4_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output5 += uScriptCon_ManualSwitch_Output5_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output6 += uScriptCon_ManualSwitch_Output6_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output7 += uScriptCon_ManualSwitch_Output7_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output8 += uScriptCon_ManualSwitch_Output8_32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_34.Out += SubGraph_CompleteObjectiveStage_Out_34;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_46.Out += SubGraph_CompleteObjectiveStage_Out_46;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.Save_Out += SubGraph_SaveLoadInt_Save_Out_48;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.Load_Out += SubGraph_SaveLoadInt_Load_Out_48;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_48;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_50.Out += SubGraph_LoadObjectiveStates_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Save_Out += SubGraph_SaveLoadBool_Save_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Load_Out += SubGraph_SaveLoadBool_Load_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_86;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_34.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_46.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_50.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.OnEnable();
		logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_13.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.OnEnable();
		logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_21.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_34.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_46.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_50.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_34.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_41.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_46.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_50.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_73.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_76.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_82.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_34.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_46.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_50.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_34.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_46.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_50.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.OnDestroy();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_5.Complete -= SubGraph_DefeatEnemyTechs_Complete_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Save_Out -= SubGraph_SaveLoadBool_Save_Out_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Load_Out -= SubGraph_SaveLoadBool_Load_Out_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Save_Out -= SubGraph_SaveLoadBool_Save_Out_31;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Load_Out -= SubGraph_SaveLoadBool_Load_Out_31;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_31;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output1 -= uScriptCon_ManualSwitch_Output1_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output2 -= uScriptCon_ManualSwitch_Output2_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output3 -= uScriptCon_ManualSwitch_Output3_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output4 -= uScriptCon_ManualSwitch_Output4_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output5 -= uScriptCon_ManualSwitch_Output5_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output6 -= uScriptCon_ManualSwitch_Output6_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output7 -= uScriptCon_ManualSwitch_Output7_32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.Output8 -= uScriptCon_ManualSwitch_Output8_32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_34.Out -= SubGraph_CompleteObjectiveStage_Out_34;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_46.Out -= SubGraph_CompleteObjectiveStage_Out_46;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.Save_Out -= SubGraph_SaveLoadInt_Save_Out_48;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.Load_Out -= SubGraph_SaveLoadInt_Load_Out_48;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_48;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_50.Out -= SubGraph_LoadObjectiveStates_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Save_Out -= SubGraph_SaveLoadBool_Save_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Load_Out -= SubGraph_SaveLoadBool_Load_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_86;
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

	private void Instance_SaveEvent_16(object o, EventArgs e)
	{
		Relay_SaveEvent_16();
	}

	private void Instance_LoadEvent_16(object o, EventArgs e)
	{
		Relay_LoadEvent_16();
	}

	private void Instance_RestartEvent_16(object o, EventArgs e)
	{
		Relay_RestartEvent_16();
	}

	private void SubGraph_DefeatEnemyTechs_Complete_5(object o, SubGraph_DefeatEnemyTechs.LogicEventArgs e)
	{
		Relay_Complete_5();
	}

	private void SubGraph_SaveLoadBool_Save_Out_15(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = e.boolean;
		local_CrateSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_15;
		Relay_Save_Out_15();
	}

	private void SubGraph_SaveLoadBool_Load_Out_15(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = e.boolean;
		local_CrateSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_15;
		Relay_Load_Out_15();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_15(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = e.boolean;
		local_CrateSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_15;
		Relay_Restart_Out_15();
	}

	private void SubGraph_SaveLoadBool_Save_Out_31(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = e.boolean;
		local_ShownMsgTrapSprung_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_31;
		Relay_Save_Out_31();
	}

	private void SubGraph_SaveLoadBool_Load_Out_31(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = e.boolean;
		local_ShownMsgTrapSprung_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_31;
		Relay_Load_Out_31();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_31(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = e.boolean;
		local_ShownMsgTrapSprung_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_31;
		Relay_Restart_Out_31();
	}

	private void uScriptCon_ManualSwitch_Output1_32(object o, EventArgs e)
	{
		Relay_Output1_32();
	}

	private void uScriptCon_ManualSwitch_Output2_32(object o, EventArgs e)
	{
		Relay_Output2_32();
	}

	private void uScriptCon_ManualSwitch_Output3_32(object o, EventArgs e)
	{
		Relay_Output3_32();
	}

	private void uScriptCon_ManualSwitch_Output4_32(object o, EventArgs e)
	{
		Relay_Output4_32();
	}

	private void uScriptCon_ManualSwitch_Output5_32(object o, EventArgs e)
	{
		Relay_Output5_32();
	}

	private void uScriptCon_ManualSwitch_Output6_32(object o, EventArgs e)
	{
		Relay_Output6_32();
	}

	private void uScriptCon_ManualSwitch_Output7_32(object o, EventArgs e)
	{
		Relay_Output7_32();
	}

	private void uScriptCon_ManualSwitch_Output8_32(object o, EventArgs e)
	{
		Relay_Output8_32();
	}

	private void SubGraph_CompleteObjectiveStage_Out_34(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_34 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_34;
		Relay_Out_34();
	}

	private void SubGraph_CompleteObjectiveStage_Out_46(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_46 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_46;
		Relay_Out_46();
	}

	private void SubGraph_SaveLoadInt_Save_Out_48(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_48 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_48;
		Relay_Save_Out_48();
	}

	private void SubGraph_SaveLoadInt_Load_Out_48(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_48 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_48;
		Relay_Load_Out_48();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_48(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_48 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_48;
		Relay_Restart_Out_48();
	}

	private void SubGraph_LoadObjectiveStates_Out_50(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_50();
	}

	private void SubGraph_SaveLoadBool_Save_Out_86(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = e.boolean;
		local_MsgApproachCrateShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_86;
		Relay_Save_Out_86();
	}

	private void SubGraph_SaveLoadBool_Load_Out_86(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = e.boolean;
		local_MsgApproachCrateShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_86;
		Relay_Load_Out_86();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_86(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = e.boolean;
		local_MsgApproachCrateShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_86;
		Relay_Restart_Out_86();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_8();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Complete_5()
	{
		Relay_In_46();
	}

	private void Relay_In_5()
	{
		int num = 0;
		Array array = enemyTechData;
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

	private void Relay_True_7()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.True(out logic_uScriptAct_SetBool_Target_7);
		local_CrateSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_7;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_7.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_False_7()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.False(out logic_uScriptAct_SetBool_Target_7);
		local_CrateSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_7;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_7.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_8()
	{
		logic_uScriptCon_CompareBool_Bool_8 = local_CrateSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.In(logic_uScriptCon_CompareBool_Bool_8);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.False;
		if (num)
		{
			Relay_In_13();
		}
		if (flag)
		{
			Relay_True_7();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_SpawnDeliveryCrate_positionName_10 = EncounterCentralPos;
		logic_uScript_SpawnDeliveryCrate_ownerNode_10 = owner_Connection_9;
		logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_10.In(logic_uScript_SpawnDeliveryCrate_positionName_10, logic_uScript_SpawnDeliveryCrate_ownerNode_10, logic_uScript_SpawnDeliveryCrate_visibleOnRadar_10);
		if (logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_10.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_In_13()
	{
		logic_uScript_CheckDeliveryCrateSpawned_ownerNode_13 = owner_Connection_12;
		logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_13.In(logic_uScript_CheckDeliveryCrateSpawned_ownerNode_13);
		if (logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_13.Yes)
		{
			Relay_In_21();
		}
	}

	private void Relay_Save_Out_15()
	{
		Relay_Save_86();
	}

	private void Relay_Load_Out_15()
	{
		Relay_Load_86();
	}

	private void Relay_Restart_Out_15()
	{
		Relay_Set_False_86();
	}

	private void Relay_Save_15()
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_15 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Save(ref logic_SubGraph_SaveLoadBool_boolean_15, logic_SubGraph_SaveLoadBool_boolAsVariable_15, logic_SubGraph_SaveLoadBool_uniqueID_15);
	}

	private void Relay_Load_15()
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_15 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Load(ref logic_SubGraph_SaveLoadBool_boolean_15, logic_SubGraph_SaveLoadBool_boolAsVariable_15, logic_SubGraph_SaveLoadBool_uniqueID_15);
	}

	private void Relay_Set_True_15()
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_15 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_15, logic_SubGraph_SaveLoadBool_boolAsVariable_15, logic_SubGraph_SaveLoadBool_uniqueID_15);
	}

	private void Relay_Set_False_15()
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_15 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_15, logic_SubGraph_SaveLoadBool_boolAsVariable_15, logic_SubGraph_SaveLoadBool_uniqueID_15);
	}

	private void Relay_SaveEvent_16()
	{
		Relay_Save_15();
	}

	private void Relay_LoadEvent_16()
	{
		Relay_Load_15();
	}

	private void Relay_RestartEvent_16()
	{
		Relay_Set_False_15();
	}

	private void Relay_In_21()
	{
		logic_uScript_GetDeliveryCrate_ownerNode_21 = owner_Connection_20;
		logic_uScript_GetDeliveryCrate_Return_21 = logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_21.In(logic_uScript_GetDeliveryCrate_ownerNode_21);
		local_Crate_Crate = logic_uScript_GetDeliveryCrate_Return_21;
		if (logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_21.Success)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_22()
	{
		logic_uScript_SetEncounterTarget_owner_22 = owner_Connection_23;
		logic_uScript_SetEncounterTarget_visibleObject_22 = local_Crate_Crate;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_22.In(logic_uScript_SetEncounterTarget_owner_22, logic_uScript_SetEncounterTarget_visibleObject_22);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_22.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_25()
	{
		int num = 0;
		Array array = msgTrapSprung;
		if (logic_uScript_AddOnScreenMessage_locString_25.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_25, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_25, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_25 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_25 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25.In(logic_uScript_AddOnScreenMessage_locString_25, logic_uScript_AddOnScreenMessage_msgPriority_25, logic_uScript_AddOnScreenMessage_holdMsg_25, logic_uScript_AddOnScreenMessage_tag_25, logic_uScript_AddOnScreenMessage_speaker_25, logic_uScript_AddOnScreenMessage_side_25);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25.Out)
		{
			Relay_True_28();
		}
	}

	private void Relay_In_27()
	{
		logic_uScriptCon_CompareBool_Bool_27 = local_ShownMsgTrapSprung_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27.In(logic_uScriptCon_CompareBool_Bool_27);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27.False;
		if (num)
		{
			Relay_In_5();
		}
		if (flag)
		{
			Relay_In_25();
		}
	}

	private void Relay_True_28()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.True(out logic_uScriptAct_SetBool_Target_28);
		local_ShownMsgTrapSprung_System_Boolean = logic_uScriptAct_SetBool_Target_28;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_28.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_False_28()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.False(out logic_uScriptAct_SetBool_Target_28);
		local_ShownMsgTrapSprung_System_Boolean = logic_uScriptAct_SetBool_Target_28;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_28.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_Save_Out_31()
	{
		Relay_Save_48();
	}

	private void Relay_Load_Out_31()
	{
		Relay_Load_48();
	}

	private void Relay_Restart_Out_31()
	{
		Relay_Restart_48();
	}

	private void Relay_Save_31()
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_31 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Save(ref logic_SubGraph_SaveLoadBool_boolean_31, logic_SubGraph_SaveLoadBool_boolAsVariable_31, logic_SubGraph_SaveLoadBool_uniqueID_31);
	}

	private void Relay_Load_31()
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_31 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Load(ref logic_SubGraph_SaveLoadBool_boolean_31, logic_SubGraph_SaveLoadBool_boolAsVariable_31, logic_SubGraph_SaveLoadBool_uniqueID_31);
	}

	private void Relay_Set_True_31()
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_31 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_31, logic_SubGraph_SaveLoadBool_boolAsVariable_31, logic_SubGraph_SaveLoadBool_uniqueID_31);
	}

	private void Relay_Set_False_31()
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_31 = local_ShownMsgTrapSprung_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_31, logic_SubGraph_SaveLoadBool_boolAsVariable_31, logic_SubGraph_SaveLoadBool_uniqueID_31);
	}

	private void Relay_Output1_32()
	{
		Relay_In_73();
	}

	private void Relay_Output2_32()
	{
		Relay_In_27();
	}

	private void Relay_Output3_32()
	{
		Relay_In_42();
	}

	private void Relay_Output4_32()
	{
	}

	private void Relay_Output5_32()
	{
	}

	private void Relay_Output6_32()
	{
	}

	private void Relay_Output7_32()
	{
	}

	private void Relay_Output8_32()
	{
	}

	private void Relay_In_32()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_32 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_32.In(logic_uScriptCon_ManualSwitch_CurrentOutput_32);
	}

	private void Relay_Out_34()
	{
	}

	private void Relay_In_34()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_34 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_34.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_34, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_34);
	}

	private void Relay_In_36()
	{
		logic_uScript_UnlockDeliveryCrate_ownerNode_36 = owner_Connection_43;
		logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_36.In(logic_uScript_UnlockDeliveryCrate_ownerNode_36);
		if (logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_36.Out)
		{
			Relay_Succeed_45();
		}
	}

	private void Relay_In_41()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_41 = local_Crate_Crate;
		logic_uScript_IsPlayerInRangeOfVisible_range_41 = local_85_System_Single;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_41.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_41, logic_uScript_IsPlayerInRangeOfVisible_range_41);
		if (logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_41.InRange)
		{
			Relay_In_36();
		}
	}

	private void Relay_In_42()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_42 = owner_Connection_44;
		logic_uScript_MoveEncounterWithVisible_visibleObject_42 = local_Crate_Crate;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_42.In(logic_uScript_MoveEncounterWithVisible_ownerNode_42, logic_uScript_MoveEncounterWithVisible_visibleObject_42);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_42.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_Succeed_45()
	{
		logic_uScript_FinishEncounter_owner_45 = owner_Connection_37;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_45.Succeed(logic_uScript_FinishEncounter_owner_45);
	}

	private void Relay_Fail_45()
	{
		logic_uScript_FinishEncounter_owner_45 = owner_Connection_37;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_45.Fail(logic_uScript_FinishEncounter_owner_45);
	}

	private void Relay_Out_46()
	{
	}

	private void Relay_In_46()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_46 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_46.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_46, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_46);
	}

	private void Relay_Save_Out_48()
	{
	}

	private void Relay_Load_Out_48()
	{
		Relay_In_50();
	}

	private void Relay_Restart_Out_48()
	{
	}

	private void Relay_Save_48()
	{
		logic_SubGraph_SaveLoadInt_integer_48 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_48 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.Save(logic_SubGraph_SaveLoadInt_restartValue_48, ref logic_SubGraph_SaveLoadInt_integer_48, logic_SubGraph_SaveLoadInt_intAsVariable_48, logic_SubGraph_SaveLoadInt_uniqueID_48);
	}

	private void Relay_Load_48()
	{
		logic_SubGraph_SaveLoadInt_integer_48 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_48 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.Load(logic_SubGraph_SaveLoadInt_restartValue_48, ref logic_SubGraph_SaveLoadInt_integer_48, logic_SubGraph_SaveLoadInt_intAsVariable_48, logic_SubGraph_SaveLoadInt_uniqueID_48);
	}

	private void Relay_Restart_48()
	{
		logic_SubGraph_SaveLoadInt_integer_48 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_48 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_48.Restart(logic_SubGraph_SaveLoadInt_restartValue_48, ref logic_SubGraph_SaveLoadInt_integer_48, logic_SubGraph_SaveLoadInt_intAsVariable_48, logic_SubGraph_SaveLoadInt_uniqueID_48);
	}

	private void Relay_Out_50()
	{
	}

	private void Relay_In_50()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_50 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_50.In(logic_SubGraph_LoadObjectiveStates_currentObjective_50);
	}

	private void Relay_In_54()
	{
		logic_uScriptCon_CompareBool_Bool_54 = ItsATrap;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.In(logic_uScriptCon_CompareBool_Bool_54);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.False;
		if (num)
		{
			Relay_In_34();
		}
		if (flag)
		{
			Relay_MarkCompleted_55();
		}
	}

	private void Relay_MarkCompleted_55()
	{
		logic_uScript_SetQuestObjectiveCompleted_owner_55 = owner_Connection_56;
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_55.MarkCompleted(logic_uScript_SetQuestObjectiveCompleted_owner_55, logic_uScript_SetQuestObjectiveCompleted_objectiveId_55, logic_uScript_SetQuestObjectiveCompleted_completed_55);
		if (logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_55.Out)
		{
			Relay_SetVisible_58();
		}
	}

	private void Relay_SetVisible_58()
	{
		logic_uScript_SetQuestObjectiveVisible_owner_58 = owner_Connection_57;
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_58.SetVisible(logic_uScript_SetQuestObjectiveVisible_owner_58, logic_uScript_SetQuestObjectiveVisible_objectiveId_58, logic_uScript_SetQuestObjectiveVisible_visible_58);
		if (logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_58.Out)
		{
			Relay_In_59();
		}
	}

	private void Relay_In_59()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_59.In(logic_uScriptAct_SetInt_Value_59, out logic_uScriptAct_SetInt_Target_59);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_59;
	}

	private void Relay_In_68()
	{
		logic_uScript_RemoveScenery_ownerNode_68 = owner_Connection_67;
		logic_uScript_RemoveScenery_positionName_68 = clearSceneryPos;
		logic_uScript_RemoveScenery_radius_68 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_68.In(logic_uScript_RemoveScenery_ownerNode_68, logic_uScript_RemoveScenery_positionName_68, logic_uScript_RemoveScenery_radius_68, logic_uScript_RemoveScenery_preventChunksSpawning_68);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_68.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_73 = local_Crate_Crate;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_73.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_73, logic_uScript_IsPlayerInRangeOfVisible_range_73);
		if (logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_73.InRange)
		{
			Relay_In_75();
		}
	}

	private void Relay_In_75()
	{
		logic_uScriptCon_CompareBool_Bool_75 = local_MsgApproachCrateShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75.In(logic_uScriptCon_CompareBool_Bool_75);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75.False;
		if (num)
		{
			Relay_In_82();
		}
		if (flag)
		{
			Relay_True_78();
		}
	}

	private void Relay_In_76()
	{
		int num = 0;
		Array array = msgApproachCrate;
		if (logic_uScript_AddOnScreenMessage_locString_76.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_76, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_76, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_76 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_76 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_76.In(logic_uScript_AddOnScreenMessage_locString_76, logic_uScript_AddOnScreenMessage_msgPriority_76, logic_uScript_AddOnScreenMessage_holdMsg_76, logic_uScript_AddOnScreenMessage_tag_76, logic_uScript_AddOnScreenMessage_speaker_76, logic_uScript_AddOnScreenMessage_side_76);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_76.Out)
		{
			Relay_In_82();
		}
	}

	private void Relay_True_78()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.True(out logic_uScriptAct_SetBool_Target_78);
		local_MsgApproachCrateShown_System_Boolean = logic_uScriptAct_SetBool_Target_78;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_78.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_False_78()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.False(out logic_uScriptAct_SetBool_Target_78);
		local_MsgApproachCrateShown_System_Boolean = logic_uScriptAct_SetBool_Target_78;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_78.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_In_82()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_82 = local_Crate_Crate;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_82.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_82, logic_uScript_IsPlayerInRangeOfVisible_range_82);
		if (logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_82.InRange)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_83()
	{
		logic_uScript_GetCrateOpenTriggerRange_crate_83 = local_Crate_Crate;
		logic_uScript_GetCrateOpenTriggerRange_Return_83 = logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_83.In(logic_uScript_GetCrateOpenTriggerRange_crate_83);
		local_85_System_Single = logic_uScript_GetCrateOpenTriggerRange_Return_83;
		if (logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_83.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_Save_Out_86()
	{
		Relay_Save_31();
	}

	private void Relay_Load_Out_86()
	{
		Relay_Load_31();
	}

	private void Relay_Restart_Out_86()
	{
		Relay_Set_False_31();
	}

	private void Relay_Save_86()
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_86 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Save(ref logic_SubGraph_SaveLoadBool_boolean_86, logic_SubGraph_SaveLoadBool_boolAsVariable_86, logic_SubGraph_SaveLoadBool_uniqueID_86);
	}

	private void Relay_Load_86()
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_86 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Load(ref logic_SubGraph_SaveLoadBool_boolean_86, logic_SubGraph_SaveLoadBool_boolAsVariable_86, logic_SubGraph_SaveLoadBool_uniqueID_86);
	}

	private void Relay_Set_True_86()
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_86 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_86, logic_SubGraph_SaveLoadBool_boolAsVariable_86, logic_SubGraph_SaveLoadBool_uniqueID_86);
	}

	private void Relay_Set_False_86()
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_86 = local_MsgApproachCrateShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_86, logic_SubGraph_SaveLoadBool_boolAsVariable_86, logic_SubGraph_SaveLoadBool_uniqueID_86);
	}
}
