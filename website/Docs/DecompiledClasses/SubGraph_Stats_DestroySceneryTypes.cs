using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_Stats_DestroySceneryTypes", "")]
public class SubGraph_Stats_DestroySceneryTypes : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SceneryTypes[] external_21 = new SceneryTypes[0];

	private uScript_AddMessage.MessageSpeaker external_38;

	private uScript_AddMessage.MessageData external_37;

	private int local_CurrentAmount_System_Int32;

	private bool local_Init_System_Boolean;

	private SceneryTypes local_SceneryType_SceneryTypes;

	private int local_TargetAmount_System_Int32;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_30;

	private GameObject owner_Connection_35;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_1;

	private bool logic_uScriptCon_CompareBool_True_1 = true;

	private bool logic_uScriptCon_CompareBool_False_1 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_3 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_3;

	private bool logic_uScriptAct_SetBool_Out_3 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_3 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_3 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_4;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_4 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_4 = "Init";

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_11 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_11;

	private int logic_uScriptAct_SetInt_Target_11;

	private bool logic_uScriptAct_SetInt_Out_11 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_13 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_13 = 1;

	private int logic_uScriptAct_AddInt_v2_B_13;

	private int logic_uScriptAct_AddInt_v2_IntResult_13;

	private float logic_uScriptAct_AddInt_v2_FloatResult_13;

	private bool logic_uScriptAct_AddInt_v2_Out_13 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_15 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_15;

	private int logic_uScriptCon_CompareInt_B_15;

	private bool logic_uScriptCon_CompareInt_GreaterThan_15 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_15 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_15 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_15 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_15 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_15 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_16;

	private int logic_SubGraph_SaveLoadInt_integer_16;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_16 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_16 = "InitialAmount01";

	private uScript_CompareSceneryTypeList logic_uScript_CompareSceneryTypeList_uScript_CompareSceneryTypeList_18 = new uScript_CompareSceneryTypeList();

	private SceneryTypes logic_uScript_CompareSceneryTypeList_A_18;

	private SceneryTypes[] logic_uScript_CompareSceneryTypeList_B_18 = new SceneryTypes[0];

	private bool logic_uScript_CompareSceneryTypeList_EqualTo_18 = true;

	private bool logic_uScript_CompareSceneryTypeList_NotEqualTo_18 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_26 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_26;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_26 = 1;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_26;

	private bool logic_uScript_SetQuestObjectiveCount_Out_26 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_27 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_27;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_27 = 1;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_27;

	private bool logic_uScript_SetQuestObjectiveCount_Out_27 = true;

	private uScript_GetQuestObjectiveTargetCount logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_28 = new uScript_GetQuestObjectiveTargetCount();

	private GameObject logic_uScript_GetQuestObjectiveTargetCount_owner_28;

	private int logic_uScript_GetQuestObjectiveTargetCount_objectiveId_28 = 1;

	private int logic_uScript_GetQuestObjectiveTargetCount_Return_28;

	private bool logic_uScript_GetQuestObjectiveTargetCount_Out_28 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_33 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_33;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_33 = 1;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_33;

	private bool logic_uScript_SetQuestObjectiveCount_Out_33 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_36 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_36;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_36;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_36;

	private bool logic_uScript_AddMessage_Out_36 = true;

	private bool logic_uScript_AddMessage_Shown_36 = true;

	private SceneryTypes event_UnityEngine_GameObject_SceneryObjectType_9;

	private int event_UnityEngine_GameObject_SceneryObjectTypeTotal_9;

	private int event_UnityEngine_GameObject_SoldTotal_9;

	[FriendlyName("Target Reached")]
	public event uScriptEventHandler Target_Reached;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
			if (null != owner_Connection_0)
			{
				uScript_SceneryObjectDestroyedEvent uScript_SceneryObjectDestroyedEvent2 = owner_Connection_0.GetComponent<uScript_SceneryObjectDestroyedEvent>();
				if (null == uScript_SceneryObjectDestroyedEvent2)
				{
					uScript_SceneryObjectDestroyedEvent2 = owner_Connection_0.AddComponent<uScript_SceneryObjectDestroyedEvent>();
				}
				if (null != uScript_SceneryObjectDestroyedEvent2)
				{
					uScript_SceneryObjectDestroyedEvent2.SceneryObjectDestroyedEvent += Instance_SceneryObjectDestroyedEvent_9;
				}
			}
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
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_5;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_5;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_5;
				}
			}
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (null == owner_Connection_30 || !m_RegisteredForEvents)
		{
			owner_Connection_30 = parentGameObject;
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_0)
		{
			uScript_SceneryObjectDestroyedEvent uScript_SceneryObjectDestroyedEvent2 = owner_Connection_0.GetComponent<uScript_SceneryObjectDestroyedEvent>();
			if (null == uScript_SceneryObjectDestroyedEvent2)
			{
				uScript_SceneryObjectDestroyedEvent2 = owner_Connection_0.AddComponent<uScript_SceneryObjectDestroyedEvent>();
			}
			if (null != uScript_SceneryObjectDestroyedEvent2)
			{
				uScript_SceneryObjectDestroyedEvent2.SceneryObjectDestroyedEvent += Instance_SceneryObjectDestroyedEvent_9;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_8)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_5;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_5;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_5;
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
			uScript_SceneryObjectDestroyedEvent component = owner_Connection_0.GetComponent<uScript_SceneryObjectDestroyedEvent>();
			if (null != component)
			{
				component.SceneryObjectDestroyedEvent -= Instance_SceneryObjectDestroyedEvent_9;
			}
		}
		if (null != owner_Connection_8)
		{
			uScript_SaveLoad component2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_5;
				component2.LoadEvent -= Instance_LoadEvent_5;
				component2.RestartEvent -= Instance_RestartEvent_5;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_3.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_11.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_13.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_15.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.SetParent(g);
		logic_uScript_CompareSceneryTypeList_uScript_CompareSceneryTypeList_18.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_26.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_27.SetParent(g);
		logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_28.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_33.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_36.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_30 = parentGameObject;
		owner_Connection_35 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Save_Out += SubGraph_SaveLoadBool_Save_Out_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Load_Out += SubGraph_SaveLoadBool_Load_Out_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_4;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Save_Out += SubGraph_SaveLoadInt_Save_Out_16;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Load_Out += SubGraph_SaveLoadInt_Load_Out_16;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_16;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_36.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Save_Out -= SubGraph_SaveLoadBool_Save_Out_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Load_Out -= SubGraph_SaveLoadBool_Load_Out_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_4;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Save_Out -= SubGraph_SaveLoadInt_Save_Out_16;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Load_Out -= SubGraph_SaveLoadInt_Load_Out_16;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_16;
	}

	private void Instance_SaveEvent_5(object o, EventArgs e)
	{
		Relay_SaveEvent_5();
	}

	private void Instance_LoadEvent_5(object o, EventArgs e)
	{
		Relay_LoadEvent_5();
	}

	private void Instance_RestartEvent_5(object o, EventArgs e)
	{
		Relay_RestartEvent_5();
	}

	private void Instance_SceneryObjectDestroyedEvent_9(object o, uScript_SceneryObjectDestroyedEvent.SceneryObjectDestroyedEventArgs e)
	{
		event_UnityEngine_GameObject_SceneryObjectType_9 = e.SceneryObjectType;
		event_UnityEngine_GameObject_SceneryObjectTypeTotal_9 = e.SceneryObjectTypeTotal;
		event_UnityEngine_GameObject_SoldTotal_9 = e.SoldTotal;
		Relay_SceneryObjectDestroyedEvent_9();
	}

	private void SubGraph_SaveLoadBool_Save_Out_4(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_4;
		Relay_Save_Out_4();
	}

	private void SubGraph_SaveLoadBool_Load_Out_4(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_4;
		Relay_Load_Out_4();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_4(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_4;
		Relay_Restart_Out_4();
	}

	private void SubGraph_SaveLoadInt_Save_Out_16(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_16 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_16;
		Relay_Save_Out_16();
	}

	private void SubGraph_SaveLoadInt_Load_Out_16(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_16 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_16;
		Relay_Load_Out_16();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_16(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_16 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_16;
		Relay_Restart_Out_16();
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("sceneryTypes", "")] SceneryTypes[] sceneryTypes, [FriendlyName("messageSpeaker", "")] uScript_AddMessage.MessageSpeaker messageSpeaker, [FriendlyName("msgComplete", "")] uScript_AddMessage.MessageData msgComplete)
	{
		external_21 = sceneryTypes;
		external_38 = messageSpeaker;
		external_37 = msgComplete;
		Relay_In_1();
	}

	private void Relay_In_1()
	{
		logic_uScriptCon_CompareBool_Bool_1 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1.In(logic_uScriptCon_CompareBool_Bool_1);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1.False)
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
			Relay_In_11();
		}
	}

	private void Relay_False_3()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_3.False(out logic_uScriptAct_SetBool_Target_3);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_3;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_3.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_Save_Out_4()
	{
		Relay_Save_16();
	}

	private void Relay_Load_Out_4()
	{
		Relay_Load_16();
	}

	private void Relay_Restart_Out_4()
	{
		Relay_Restart_16();
	}

	private void Relay_Save_4()
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Save(ref logic_SubGraph_SaveLoadBool_boolean_4, logic_SubGraph_SaveLoadBool_boolAsVariable_4, logic_SubGraph_SaveLoadBool_uniqueID_4);
	}

	private void Relay_Load_4()
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Load(ref logic_SubGraph_SaveLoadBool_boolean_4, logic_SubGraph_SaveLoadBool_boolAsVariable_4, logic_SubGraph_SaveLoadBool_uniqueID_4);
	}

	private void Relay_Set_True_4()
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_4, logic_SubGraph_SaveLoadBool_boolAsVariable_4, logic_SubGraph_SaveLoadBool_uniqueID_4);
	}

	private void Relay_Set_False_4()
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_4, logic_SubGraph_SaveLoadBool_boolAsVariable_4, logic_SubGraph_SaveLoadBool_uniqueID_4);
	}

	private void Relay_SaveEvent_5()
	{
		Relay_Save_4();
	}

	private void Relay_LoadEvent_5()
	{
		Relay_Load_4();
	}

	private void Relay_RestartEvent_5()
	{
		Relay_Set_False_4();
	}

	private void Relay_SceneryObjectDestroyedEvent_9()
	{
		local_SceneryType_SceneryTypes = event_UnityEngine_GameObject_SceneryObjectType_9;
		Relay_In_18();
	}

	private void Relay_In_11()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_11.In(logic_uScriptAct_SetInt_Value_11, out logic_uScriptAct_SetInt_Target_11);
		local_CurrentAmount_System_Int32 = logic_uScriptAct_SetInt_Target_11;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_11.Out)
		{
			Relay_SetCount_26();
		}
	}

	private void Relay_In_13()
	{
		logic_uScriptAct_AddInt_v2_B_13 = local_CurrentAmount_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_13.In(logic_uScriptAct_AddInt_v2_A_13, logic_uScriptAct_AddInt_v2_B_13, out logic_uScriptAct_AddInt_v2_IntResult_13, out logic_uScriptAct_AddInt_v2_FloatResult_13);
		local_CurrentAmount_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_13;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_13.Out)
		{
			Relay_SetCount_27();
		}
	}

	private void Relay_In_15()
	{
		logic_uScriptCon_CompareInt_A_15 = local_CurrentAmount_System_Int32;
		logic_uScriptCon_CompareInt_B_15 = local_TargetAmount_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_15.In(logic_uScriptCon_CompareInt_A_15, logic_uScriptCon_CompareInt_B_15);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_15.GreaterThanOrEqualTo)
		{
			Relay_In_36();
		}
	}

	private void Relay_Save_Out_16()
	{
	}

	private void Relay_Load_Out_16()
	{
		Relay_SetCount_33();
	}

	private void Relay_Restart_Out_16()
	{
	}

	private void Relay_Save_16()
	{
		logic_SubGraph_SaveLoadInt_integer_16 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_16 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Save(logic_SubGraph_SaveLoadInt_restartValue_16, ref logic_SubGraph_SaveLoadInt_integer_16, logic_SubGraph_SaveLoadInt_intAsVariable_16, logic_SubGraph_SaveLoadInt_uniqueID_16);
	}

	private void Relay_Load_16()
	{
		logic_SubGraph_SaveLoadInt_integer_16 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_16 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Load(logic_SubGraph_SaveLoadInt_restartValue_16, ref logic_SubGraph_SaveLoadInt_integer_16, logic_SubGraph_SaveLoadInt_intAsVariable_16, logic_SubGraph_SaveLoadInt_uniqueID_16);
	}

	private void Relay_Restart_16()
	{
		logic_SubGraph_SaveLoadInt_integer_16 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_16 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Restart(logic_SubGraph_SaveLoadInt_restartValue_16, ref logic_SubGraph_SaveLoadInt_integer_16, logic_SubGraph_SaveLoadInt_intAsVariable_16, logic_SubGraph_SaveLoadInt_uniqueID_16);
	}

	private void Relay_In_18()
	{
		logic_uScript_CompareSceneryTypeList_A_18 = local_SceneryType_SceneryTypes;
		int num = 0;
		Array array = external_21;
		if (logic_uScript_CompareSceneryTypeList_B_18.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_CompareSceneryTypeList_B_18, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_CompareSceneryTypeList_B_18, num, array.Length);
		num += array.Length;
		logic_uScript_CompareSceneryTypeList_uScript_CompareSceneryTypeList_18.In(logic_uScript_CompareSceneryTypeList_A_18, logic_uScript_CompareSceneryTypeList_B_18);
		if (logic_uScript_CompareSceneryTypeList_uScript_CompareSceneryTypeList_18.EqualTo)
		{
			Relay_In_13();
		}
	}

	private void Relay_Connection_21()
	{
	}

	private void Relay_Connection_22()
	{
	}

	private void Relay_Connection_23()
	{
		if (this.Target_Reached != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Target_Reached(this, args);
		}
	}

	private void Relay_SetCount_26()
	{
		logic_uScript_SetQuestObjectiveCount_owner_26 = owner_Connection_19;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_26.SetCount(logic_uScript_SetQuestObjectiveCount_owner_26, logic_uScript_SetQuestObjectiveCount_objectiveId_26, logic_uScript_SetQuestObjectiveCount_currentCount_26);
	}

	private void Relay_SetCount_27()
	{
		logic_uScript_SetQuestObjectiveCount_owner_27 = owner_Connection_20;
		logic_uScript_SetQuestObjectiveCount_currentCount_27 = local_CurrentAmount_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_27.SetCount(logic_uScript_SetQuestObjectiveCount_owner_27, logic_uScript_SetQuestObjectiveCount_objectiveId_27, logic_uScript_SetQuestObjectiveCount_currentCount_27);
		if (logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_27.Out)
		{
			Relay_GetTargetCount_28();
		}
	}

	private void Relay_GetTargetCount_28()
	{
		logic_uScript_GetQuestObjectiveTargetCount_owner_28 = owner_Connection_30;
		logic_uScript_GetQuestObjectiveTargetCount_Return_28 = logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_28.GetTargetCount(logic_uScript_GetQuestObjectiveTargetCount_owner_28, logic_uScript_GetQuestObjectiveTargetCount_objectiveId_28);
		local_TargetAmount_System_Int32 = logic_uScript_GetQuestObjectiveTargetCount_Return_28;
		if (logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_28.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_SetCount_33()
	{
		logic_uScript_SetQuestObjectiveCount_owner_33 = owner_Connection_35;
		logic_uScript_SetQuestObjectiveCount_currentCount_33 = local_CurrentAmount_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_33.SetCount(logic_uScript_SetQuestObjectiveCount_owner_33, logic_uScript_SetQuestObjectiveCount_objectiveId_33, logic_uScript_SetQuestObjectiveCount_currentCount_33);
	}

	private void Relay_In_36()
	{
		logic_uScript_AddMessage_messageData_36 = external_37;
		logic_uScript_AddMessage_speaker_36 = external_38;
		logic_uScript_AddMessage_Return_36 = logic_uScript_AddMessage_uScript_AddMessage_36.In(logic_uScript_AddMessage_messageData_36, logic_uScript_AddMessage_speaker_36);
		if (logic_uScript_AddMessage_uScript_AddMessage_36.Out)
		{
			Relay_Connection_23();
		}
	}

	private void Relay_Connection_37()
	{
	}

	private void Relay_Connection_38()
	{
	}
}
