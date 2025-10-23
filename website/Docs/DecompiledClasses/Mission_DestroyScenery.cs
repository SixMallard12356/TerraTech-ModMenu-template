using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_DestroyScenery : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private int local_CurrentAmountTotal_System_Int32;

	private int local_CurrentAmountType_System_Int32;

	private bool local_Init_System_Boolean;

	private int local_InitialAmount_System_Int32;

	private SceneryTypes local_SceneryType_SceneryTypes;

	public LocalisedString[] msgComplete = new LocalisedString[0];

	public LocalisedString[] msgStart = new LocalisedString[0];

	public int targetAmount;

	public SceneryTypes targetSceneryType;

	public bool useSceneryType;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_15;

	private GameObject owner_Connection_48;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_2;

	private bool logic_uScriptCon_CompareBool_True_2 = true;

	private bool logic_uScriptCon_CompareBool_False_2 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_4 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_4;

	private bool logic_uScriptAct_SetBool_Out_4 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_4 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_4 = true;

	private uScriptAct_Log logic_uScriptAct_Log_uScriptAct_Log_6 = new uScriptAct_Log();

	private object logic_uScriptAct_Log_Prefix_6 = "Target Amount: ";

	private object[] logic_uScriptAct_Log_Target_6 = new object[0];

	private object logic_uScriptAct_Log_Postfix_6 = "";

	private bool logic_uScriptAct_Log_Out_6 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_9;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_9 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_9 = "Init";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_12;

	private int logic_SubGraph_SaveLoadInt_integer_12;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_12 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_12 = "InitialAmount";

	private uScript_GetNumSceneryObjectsDestroyed logic_uScript_GetNumSceneryObjectsDestroyed_uScript_GetNumSceneryObjectsDestroyed_16 = new uScript_GetNumSceneryObjectsDestroyed();

	private SceneryTypes logic_uScript_GetNumSceneryObjectsDestroyed_sceneryType_16;

	private int logic_uScript_GetNumSceneryObjectsDestroyed_Return_16;

	private bool logic_uScript_GetNumSceneryObjectsDestroyed_Out_16 = true;

	private uScript_CompareSceneryTypes logic_uScript_CompareSceneryTypes_uScript_CompareSceneryTypes_20 = new uScript_CompareSceneryTypes();

	private SceneryTypes logic_uScript_CompareSceneryTypes_A_20;

	private SceneryTypes logic_uScript_CompareSceneryTypes_B_20;

	private bool logic_uScript_CompareSceneryTypes_EqualTo_20 = true;

	private bool logic_uScript_CompareSceneryTypes_NotEqualTo_20 = true;

	private uScriptAct_Log logic_uScriptAct_Log_uScriptAct_Log_28 = new uScriptAct_Log();

	private object logic_uScriptAct_Log_Prefix_28 = "Target Reached!";

	private object[] logic_uScriptAct_Log_Target_28 = new object[0];

	private object logic_uScriptAct_Log_Postfix_28 = "";

	private bool logic_uScriptAct_Log_Out_28 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_29 = true;

	private SubGraph_CheckStatsTarget logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31 = new SubGraph_CheckStatsTarget();

	private int logic_SubGraph_CheckStatsTarget_objectiveID_31 = 1;

	private int logic_SubGraph_CheckStatsTarget_totalAmount_31;

	private int logic_SubGraph_CheckStatsTarget_initialAmount_31;

	private int logic_SubGraph_CheckStatsTarget_currentAmount_31;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_33 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_33 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_35 = true;

	private SubGraph_CheckStatsTarget logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36 = new SubGraph_CheckStatsTarget();

	private int logic_SubGraph_CheckStatsTarget_objectiveID_36 = 1;

	private int logic_SubGraph_CheckStatsTarget_totalAmount_36;

	private int logic_SubGraph_CheckStatsTarget_initialAmount_36;

	private int logic_SubGraph_CheckStatsTarget_currentAmount_36;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_37;

	private bool logic_uScriptCon_CompareBool_True_37 = true;

	private bool logic_uScriptCon_CompareBool_False_37 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_42;

	private bool logic_uScriptCon_CompareBool_True_42 = true;

	private bool logic_uScriptCon_CompareBool_False_42 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_44 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_44 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_44 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_44;

	private string logic_uScript_AddOnScreenMessage_tag_44 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_44;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_44;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_44;

	private bool logic_uScript_AddOnScreenMessage_Out_44 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_44 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_45 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_45 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_45 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_45;

	private string logic_uScript_AddOnScreenMessage_tag_45 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_45;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_45;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_45;

	private bool logic_uScript_AddOnScreenMessage_Out_45 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_45 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_47 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_47;

	private bool logic_uScript_FinishEncounter_Out_47 = true;

	private SceneryTypes event_UnityEngine_GameObject_SceneryObjectType_18;

	private int event_UnityEngine_GameObject_SceneryObjectTypeTotal_18;

	private int event_UnityEngine_GameObject_SoldTotal_18;

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
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_23;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_23;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_23;
				}
			}
		}
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_SceneryObjectDestroyedEvent uScript_SceneryObjectDestroyedEvent2 = owner_Connection_1.GetComponent<uScript_SceneryObjectDestroyedEvent>();
				if (null == uScript_SceneryObjectDestroyedEvent2)
				{
					uScript_SceneryObjectDestroyedEvent2 = owner_Connection_1.AddComponent<uScript_SceneryObjectDestroyedEvent>();
				}
				if (null != uScript_SceneryObjectDestroyedEvent2)
				{
					uScript_SceneryObjectDestroyedEvent2.SceneryObjectDestroyedEvent += Instance_SceneryObjectDestroyedEvent_18;
				}
			}
		}
		if (null == owner_Connection_15 || !m_RegisteredForEvents)
		{
			owner_Connection_15 = parentGameObject;
			if (null != owner_Connection_15)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_15.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_15.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_10;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_10;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_10;
				}
			}
		}
		if (null == owner_Connection_48 || !m_RegisteredForEvents)
		{
			owner_Connection_48 = parentGameObject;
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
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_23;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_23;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_23;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_SceneryObjectDestroyedEvent uScript_SceneryObjectDestroyedEvent2 = owner_Connection_1.GetComponent<uScript_SceneryObjectDestroyedEvent>();
			if (null == uScript_SceneryObjectDestroyedEvent2)
			{
				uScript_SceneryObjectDestroyedEvent2 = owner_Connection_1.AddComponent<uScript_SceneryObjectDestroyedEvent>();
			}
			if (null != uScript_SceneryObjectDestroyedEvent2)
			{
				uScript_SceneryObjectDestroyedEvent2.SceneryObjectDestroyedEvent += Instance_SceneryObjectDestroyedEvent_18;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_15)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_15.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_15.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_10;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_10;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_10;
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
				component.OnUpdate -= Instance_OnUpdate_23;
				component.OnSuspend -= Instance_OnSuspend_23;
				component.OnResume -= Instance_OnResume_23;
			}
		}
		if (null != owner_Connection_1)
		{
			uScript_SceneryObjectDestroyedEvent component2 = owner_Connection_1.GetComponent<uScript_SceneryObjectDestroyedEvent>();
			if (null != component2)
			{
				component2.SceneryObjectDestroyedEvent -= Instance_SceneryObjectDestroyedEvent_18;
			}
		}
		if (null != owner_Connection_15)
		{
			uScript_SaveLoad component3 = owner_Connection_15.GetComponent<uScript_SaveLoad>();
			if (null != component3)
			{
				component3.SaveEvent -= Instance_SaveEvent_10;
				component3.LoadEvent -= Instance_LoadEvent_10;
				component3.RestartEvent -= Instance_RestartEvent_10;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.SetParent(g);
		logic_uScriptAct_Log_uScriptAct_Log_6.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.SetParent(g);
		logic_uScript_GetNumSceneryObjectsDestroyed_uScript_GetNumSceneryObjectsDestroyed_16.SetParent(g);
		logic_uScript_CompareSceneryTypes_uScript_CompareSceneryTypes_20.SetParent(g);
		logic_uScriptAct_Log_uScriptAct_Log_28.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29.SetParent(g);
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_33.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.SetParent(g);
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_44.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_45.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_47.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_1 = parentGameObject;
		owner_Connection_15 = parentGameObject;
		owner_Connection_48 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Awake();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31.Awake();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out += SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out += SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Save_Out += SubGraph_SaveLoadInt_Save_Out_12;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Load_Out += SubGraph_SaveLoadInt_Load_Out_12;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_12;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31.Reached += SubGraph_CheckStatsTarget_Reached_31;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31.Not_Reached += SubGraph_CheckStatsTarget_Not_Reached_31;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36.Reached += SubGraph_CheckStatsTarget_Reached_36;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36.Not_Reached += SubGraph_CheckStatsTarget_Not_Reached_36;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Start();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31.Start();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.OnEnable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31.OnEnable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.OnDisable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31.OnDisable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_44.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_45.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Update();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31.Update();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.OnDestroy();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31.OnDestroy();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out -= SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out -= SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Save_Out -= SubGraph_SaveLoadInt_Save_Out_12;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Load_Out -= SubGraph_SaveLoadInt_Load_Out_12;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_12;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31.Reached -= SubGraph_CheckStatsTarget_Reached_31;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31.Not_Reached -= SubGraph_CheckStatsTarget_Not_Reached_31;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36.Reached -= SubGraph_CheckStatsTarget_Reached_36;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36.Not_Reached -= SubGraph_CheckStatsTarget_Not_Reached_36;
	}

	private void Instance_SaveEvent_10(object o, EventArgs e)
	{
		Relay_SaveEvent_10();
	}

	private void Instance_LoadEvent_10(object o, EventArgs e)
	{
		Relay_LoadEvent_10();
	}

	private void Instance_RestartEvent_10(object o, EventArgs e)
	{
		Relay_RestartEvent_10();
	}

	private void Instance_SceneryObjectDestroyedEvent_18(object o, uScript_SceneryObjectDestroyedEvent.SceneryObjectDestroyedEventArgs e)
	{
		event_UnityEngine_GameObject_SceneryObjectType_18 = e.SceneryObjectType;
		event_UnityEngine_GameObject_SceneryObjectTypeTotal_18 = e.SceneryObjectTypeTotal;
		event_UnityEngine_GameObject_SoldTotal_18 = e.SoldTotal;
		Relay_SceneryObjectDestroyedEvent_18();
	}

	private void Instance_OnUpdate_23(object o, EventArgs e)
	{
		Relay_OnUpdate_23();
	}

	private void Instance_OnSuspend_23(object o, EventArgs e)
	{
		Relay_OnSuspend_23();
	}

	private void Instance_OnResume_23(object o, EventArgs e)
	{
		Relay_OnResume_23();
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

	private void SubGraph_SaveLoadInt_Save_Out_12(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_12 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_12;
		Relay_Save_Out_12();
	}

	private void SubGraph_SaveLoadInt_Load_Out_12(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_12 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_12;
		Relay_Load_Out_12();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_12(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_12 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_12;
		Relay_Restart_Out_12();
	}

	private void SubGraph_CheckStatsTarget_Reached_31(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_31 = e.currentAmount;
		local_CurrentAmountTotal_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_31;
		Relay_Reached_31();
	}

	private void SubGraph_CheckStatsTarget_Not_Reached_31(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_31 = e.currentAmount;
		local_CurrentAmountTotal_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_31;
		Relay_Not_Reached_31();
	}

	private void SubGraph_CheckStatsTarget_Reached_36(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_36 = e.currentAmount;
		local_CurrentAmountType_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_36;
		Relay_Reached_36();
	}

	private void SubGraph_CheckStatsTarget_Not_Reached_36(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_36 = e.currentAmount;
		local_CurrentAmountType_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_36;
		Relay_Not_Reached_36();
	}

	private void Relay_In_2()
	{
		logic_uScriptCon_CompareBool_Bool_2 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.In(logic_uScriptCon_CompareBool_Bool_2);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.False)
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
			Relay_In_42();
		}
	}

	private void Relay_False_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.False(out logic_uScriptAct_SetBool_Target_4);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_6()
	{
		int num = 0;
		if (logic_uScriptAct_Log_Target_6.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Log_Target_6, num + 1);
		}
		logic_uScriptAct_Log_Target_6[num++] = targetAmount;
		logic_uScriptAct_Log_uScriptAct_Log_6.In(logic_uScriptAct_Log_Prefix_6, logic_uScriptAct_Log_Target_6, logic_uScriptAct_Log_Postfix_6);
		if (logic_uScriptAct_Log_uScriptAct_Log_6.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_Save_Out_9()
	{
		Relay_Save_12();
	}

	private void Relay_Load_Out_9()
	{
		Relay_Load_12();
	}

	private void Relay_Restart_Out_9()
	{
		Relay_Restart_12();
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

	private void Relay_SaveEvent_10()
	{
		Relay_Save_9();
	}

	private void Relay_LoadEvent_10()
	{
		Relay_Load_9();
	}

	private void Relay_RestartEvent_10()
	{
		Relay_Set_False_9();
	}

	private void Relay_Save_Out_12()
	{
	}

	private void Relay_Load_Out_12()
	{
	}

	private void Relay_Restart_Out_12()
	{
	}

	private void Relay_Save_12()
	{
		logic_SubGraph_SaveLoadInt_integer_12 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_12 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Save(logic_SubGraph_SaveLoadInt_restartValue_12, ref logic_SubGraph_SaveLoadInt_integer_12, logic_SubGraph_SaveLoadInt_intAsVariable_12, logic_SubGraph_SaveLoadInt_uniqueID_12);
	}

	private void Relay_Load_12()
	{
		logic_SubGraph_SaveLoadInt_integer_12 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_12 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Load(logic_SubGraph_SaveLoadInt_restartValue_12, ref logic_SubGraph_SaveLoadInt_integer_12, logic_SubGraph_SaveLoadInt_intAsVariable_12, logic_SubGraph_SaveLoadInt_uniqueID_12);
	}

	private void Relay_Restart_12()
	{
		logic_SubGraph_SaveLoadInt_integer_12 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_12 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Restart(logic_SubGraph_SaveLoadInt_restartValue_12, ref logic_SubGraph_SaveLoadInt_integer_12, logic_SubGraph_SaveLoadInt_intAsVariable_12, logic_SubGraph_SaveLoadInt_uniqueID_12);
	}

	private void Relay_AllSceneryObjects_16()
	{
		logic_uScript_GetNumSceneryObjectsDestroyed_sceneryType_16 = targetSceneryType;
		logic_uScript_GetNumSceneryObjectsDestroyed_Return_16 = logic_uScript_GetNumSceneryObjectsDestroyed_uScript_GetNumSceneryObjectsDestroyed_16.AllSceneryObjects(logic_uScript_GetNumSceneryObjectsDestroyed_sceneryType_16);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumSceneryObjectsDestroyed_Return_16;
		if (logic_uScript_GetNumSceneryObjectsDestroyed_uScript_GetNumSceneryObjectsDestroyed_16.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_SceneryObjectsOfType_16()
	{
		logic_uScript_GetNumSceneryObjectsDestroyed_sceneryType_16 = targetSceneryType;
		logic_uScript_GetNumSceneryObjectsDestroyed_Return_16 = logic_uScript_GetNumSceneryObjectsDestroyed_uScript_GetNumSceneryObjectsDestroyed_16.SceneryObjectsOfType(logic_uScript_GetNumSceneryObjectsDestroyed_sceneryType_16);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumSceneryObjectsDestroyed_Return_16;
		if (logic_uScript_GetNumSceneryObjectsDestroyed_uScript_GetNumSceneryObjectsDestroyed_16.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_SceneryObjectDestroyedEvent_18()
	{
		local_SceneryType_SceneryTypes = event_UnityEngine_GameObject_SceneryObjectType_18;
		local_CurrentAmountType_System_Int32 = event_UnityEngine_GameObject_SceneryObjectTypeTotal_18;
		local_CurrentAmountTotal_System_Int32 = event_UnityEngine_GameObject_SoldTotal_18;
		Relay_In_37();
	}

	private void Relay_In_20()
	{
		logic_uScript_CompareSceneryTypes_A_20 = local_SceneryType_SceneryTypes;
		logic_uScript_CompareSceneryTypes_B_20 = targetSceneryType;
		logic_uScript_CompareSceneryTypes_uScript_CompareSceneryTypes_20.In(logic_uScript_CompareSceneryTypes_A_20, logic_uScript_CompareSceneryTypes_B_20);
		if (logic_uScript_CompareSceneryTypes_uScript_CompareSceneryTypes_20.EqualTo)
		{
			Relay_In_33();
		}
	}

	private void Relay_OnUpdate_23()
	{
		Relay_In_2();
	}

	private void Relay_OnSuspend_23()
	{
	}

	private void Relay_OnResume_23()
	{
	}

	private void Relay_In_28()
	{
		logic_uScriptAct_Log_uScriptAct_Log_28.In(logic_uScriptAct_Log_Prefix_28, logic_uScriptAct_Log_Target_28, logic_uScriptAct_Log_Postfix_28);
		if (logic_uScriptAct_Log_uScriptAct_Log_28.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_29()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_Reached_31()
	{
		Relay_In_28();
	}

	private void Relay_Not_Reached_31()
	{
	}

	private void Relay_In_31()
	{
		logic_SubGraph_CheckStatsTarget_initialAmount_31 = local_InitialAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_currentAmount_31 = local_CurrentAmountTotal_System_Int32;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_31.In(logic_SubGraph_CheckStatsTarget_objectiveID_31, logic_SubGraph_CheckStatsTarget_totalAmount_31, logic_SubGraph_CheckStatsTarget_initialAmount_31, ref logic_SubGraph_CheckStatsTarget_currentAmount_31);
	}

	private void Relay_In_33()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_33.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_33.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_In_35()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_Reached_36()
	{
		Relay_In_28();
	}

	private void Relay_Not_Reached_36()
	{
	}

	private void Relay_In_36()
	{
		logic_SubGraph_CheckStatsTarget_initialAmount_36 = local_InitialAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_currentAmount_36 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_36.In(logic_SubGraph_CheckStatsTarget_objectiveID_36, logic_SubGraph_CheckStatsTarget_totalAmount_36, logic_SubGraph_CheckStatsTarget_initialAmount_36, ref logic_SubGraph_CheckStatsTarget_currentAmount_36);
	}

	private void Relay_In_37()
	{
		logic_uScriptCon_CompareBool_Bool_37 = useSceneryType;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.In(logic_uScriptCon_CompareBool_Bool_37);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.False;
		if (num)
		{
			Relay_In_20();
		}
		if (flag)
		{
			Relay_In_35();
		}
	}

	private void Relay_In_42()
	{
		logic_uScriptCon_CompareBool_Bool_42 = useSceneryType;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.In(logic_uScriptCon_CompareBool_Bool_42);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.False;
		if (num)
		{
			Relay_SceneryObjectsOfType_16();
		}
		if (flag)
		{
			Relay_AllSceneryObjects_16();
		}
	}

	private void Relay_In_44()
	{
		int num = 0;
		Array array = msgStart;
		if (logic_uScript_AddOnScreenMessage_locString_44.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_44, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_44, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_44 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_44.In(logic_uScript_AddOnScreenMessage_locString_44, logic_uScript_AddOnScreenMessage_msgPriority_44, logic_uScript_AddOnScreenMessage_holdMsg_44, logic_uScript_AddOnScreenMessage_tag_44, logic_uScript_AddOnScreenMessage_speaker_44, logic_uScript_AddOnScreenMessage_side_44);
	}

	private void Relay_In_45()
	{
		int num = 0;
		Array array = msgComplete;
		if (logic_uScript_AddOnScreenMessage_locString_45.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_45, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_45, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_45 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_45.In(logic_uScript_AddOnScreenMessage_locString_45, logic_uScript_AddOnScreenMessage_msgPriority_45, logic_uScript_AddOnScreenMessage_holdMsg_45, logic_uScript_AddOnScreenMessage_tag_45, logic_uScript_AddOnScreenMessage_speaker_45, logic_uScript_AddOnScreenMessage_side_45);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_45.Out)
		{
			Relay_Succeed_47();
		}
	}

	private void Relay_Succeed_47()
	{
		logic_uScript_FinishEncounter_owner_47 = owner_Connection_48;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_47.Succeed(logic_uScript_FinishEncounter_owner_47);
	}

	private void Relay_Fail_47()
	{
		logic_uScript_FinishEncounter_owner_47 = owner_Connection_48;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_47.Fail(logic_uScript_FinishEncounter_owner_47);
	}
}
