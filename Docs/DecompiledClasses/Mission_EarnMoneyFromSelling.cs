using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_EarnMoneyFromSelling : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private int local_CurrentAmountTotal_System_Int32;

	private bool local_Init_System_Boolean;

	private int local_InitialAmount_System_Int32;

	public LocalisedString[] msgComplete = new LocalisedString[0];

	public LocalisedString[] msgStart = new LocalisedString[0];

	public int targetAmount;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_26;

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

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_10;

	private int logic_SubGraph_SaveLoadInt_integer_10;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_10 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_10 = "InitialAmount";

	private SubGraph_CheckStatsTarget logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12 = new SubGraph_CheckStatsTarget();

	private int logic_SubGraph_CheckStatsTarget_objectiveID_12 = 1;

	private int logic_SubGraph_CheckStatsTarget_totalAmount_12;

	private int logic_SubGraph_CheckStatsTarget_initialAmount_12;

	private int logic_SubGraph_CheckStatsTarget_currentAmount_12;

	private uScript_GetMoneyFromResourceSales logic_uScript_GetMoneyFromResourceSales_uScript_GetMoneyFromResourceSales_18 = new uScript_GetMoneyFromResourceSales();

	private ChunkTypes logic_uScript_GetMoneyFromResourceSales_resourceType_18;

	private int logic_uScript_GetMoneyFromResourceSales_Return_18;

	private bool logic_uScript_GetMoneyFromResourceSales_Out_18 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_21 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_21 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_21 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_21;

	private string logic_uScript_AddOnScreenMessage_tag_21 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_21;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_21;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_21;

	private bool logic_uScript_AddOnScreenMessage_Out_21 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_21 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_22 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_22 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_22 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_22;

	private string logic_uScript_AddOnScreenMessage_tag_22 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_22;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_22;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_22;

	private bool logic_uScript_AddOnScreenMessage_Out_22 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_22 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_25 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_25;

	private bool logic_uScript_FinishEncounter_Out_25 = true;

	private ChunkTypes event_UnityEngine_GameObject_ResourceType_19;

	private int event_UnityEngine_GameObject_ResourceTypeTotal_19;

	private int event_UnityEngine_GameObject_MoneyTotal_19;

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
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_20;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_20;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_20;
				}
			}
		}
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_MoneyFromResourceSaleEvent uScript_MoneyFromResourceSaleEvent2 = owner_Connection_1.GetComponent<uScript_MoneyFromResourceSaleEvent>();
				if (null == uScript_MoneyFromResourceSaleEvent2)
				{
					uScript_MoneyFromResourceSaleEvent2 = owner_Connection_1.AddComponent<uScript_MoneyFromResourceSaleEvent>();
				}
				if (null != uScript_MoneyFromResourceSaleEvent2)
				{
					uScript_MoneyFromResourceSaleEvent2.MoneyFromResourceSaleEvent += Instance_MoneyFromResourceSaleEvent_19;
				}
			}
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
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_8;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_8;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_8;
				}
			}
		}
		if (null == owner_Connection_26 || !m_RegisteredForEvents)
		{
			owner_Connection_26 = parentGameObject;
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
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_20;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_20;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_20;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_MoneyFromResourceSaleEvent uScript_MoneyFromResourceSaleEvent2 = owner_Connection_1.GetComponent<uScript_MoneyFromResourceSaleEvent>();
			if (null == uScript_MoneyFromResourceSaleEvent2)
			{
				uScript_MoneyFromResourceSaleEvent2 = owner_Connection_1.AddComponent<uScript_MoneyFromResourceSaleEvent>();
			}
			if (null != uScript_MoneyFromResourceSaleEvent2)
			{
				uScript_MoneyFromResourceSaleEvent2.MoneyFromResourceSaleEvent += Instance_MoneyFromResourceSaleEvent_19;
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
		if (null != owner_Connection_0)
		{
			uScript_EncounterUpdate component = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_20;
				component.OnSuspend -= Instance_OnSuspend_20;
				component.OnResume -= Instance_OnResume_20;
			}
		}
		if (null != owner_Connection_1)
		{
			uScript_MoneyFromResourceSaleEvent component2 = owner_Connection_1.GetComponent<uScript_MoneyFromResourceSaleEvent>();
			if (null != component2)
			{
				component2.MoneyFromResourceSaleEvent -= Instance_MoneyFromResourceSaleEvent_19;
			}
		}
		if (null != owner_Connection_17)
		{
			uScript_SaveLoad component3 = owner_Connection_17.GetComponent<uScript_SaveLoad>();
			if (null != component3)
			{
				component3.SaveEvent -= Instance_SaveEvent_8;
				component3.LoadEvent -= Instance_LoadEvent_8;
				component3.RestartEvent -= Instance_RestartEvent_8;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.SetParent(g);
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12.SetParent(g);
		logic_uScript_GetMoneyFromResourceSales_uScript_GetMoneyFromResourceSales_18.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_21.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_22.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_25.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_1 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_26 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Awake();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Save_Out += SubGraph_SaveLoadInt_Save_Out_10;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Load_Out += SubGraph_SaveLoadInt_Load_Out_10;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_10;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12.Reached += SubGraph_CheckStatsTarget_Reached_12;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12.Not_Reached += SubGraph_CheckStatsTarget_Not_Reached_12;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Start();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.OnEnable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.OnDisable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_21.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_22.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Update();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.OnDestroy();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Save_Out -= SubGraph_SaveLoadInt_Save_Out_10;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Load_Out -= SubGraph_SaveLoadInt_Load_Out_10;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_10;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12.Reached -= SubGraph_CheckStatsTarget_Reached_12;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12.Not_Reached -= SubGraph_CheckStatsTarget_Not_Reached_12;
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

	private void Instance_MoneyFromResourceSaleEvent_19(object o, uScript_MoneyFromResourceSaleEvent.MoneyFromResourceSaleEventArgs e)
	{
		event_UnityEngine_GameObject_ResourceType_19 = e.ResourceType;
		event_UnityEngine_GameObject_ResourceTypeTotal_19 = e.ResourceTypeTotal;
		event_UnityEngine_GameObject_MoneyTotal_19 = e.MoneyTotal;
		Relay_MoneyFromResourceSaleEvent_19();
	}

	private void Instance_OnUpdate_20(object o, EventArgs e)
	{
		Relay_OnUpdate_20();
	}

	private void Instance_OnSuspend_20(object o, EventArgs e)
	{
		Relay_OnSuspend_20();
	}

	private void Instance_OnResume_20(object o, EventArgs e)
	{
		Relay_OnResume_20();
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

	private void SubGraph_SaveLoadInt_Save_Out_10(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_10 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_10;
		Relay_Save_Out_10();
	}

	private void SubGraph_SaveLoadInt_Load_Out_10(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_10 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_10;
		Relay_Load_Out_10();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_10(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_10 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_10;
		Relay_Restart_Out_10();
	}

	private void SubGraph_CheckStatsTarget_Reached_12(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_12 = e.currentAmount;
		local_CurrentAmountTotal_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_12;
		Relay_Reached_12();
	}

	private void SubGraph_CheckStatsTarget_Not_Reached_12(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_12 = e.currentAmount;
		local_CurrentAmountTotal_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_12;
		Relay_Not_Reached_12();
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
			Relay_AllResources_18();
		}
	}

	private void Relay_False_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.False(out logic_uScriptAct_SetBool_Target_4);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_AllResources_18();
		}
	}

	private void Relay_Save_Out_7()
	{
		Relay_Save_10();
	}

	private void Relay_Load_Out_7()
	{
		Relay_Load_10();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_Restart_10();
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

	private void Relay_SaveEvent_8()
	{
		Relay_Save_7();
	}

	private void Relay_LoadEvent_8()
	{
		Relay_Load_7();
	}

	private void Relay_RestartEvent_8()
	{
		Relay_Set_False_7();
	}

	private void Relay_Save_Out_10()
	{
	}

	private void Relay_Load_Out_10()
	{
	}

	private void Relay_Restart_Out_10()
	{
	}

	private void Relay_Save_10()
	{
		logic_SubGraph_SaveLoadInt_integer_10 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_10 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Save(logic_SubGraph_SaveLoadInt_restartValue_10, ref logic_SubGraph_SaveLoadInt_integer_10, logic_SubGraph_SaveLoadInt_intAsVariable_10, logic_SubGraph_SaveLoadInt_uniqueID_10);
	}

	private void Relay_Load_10()
	{
		logic_SubGraph_SaveLoadInt_integer_10 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_10 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Load(logic_SubGraph_SaveLoadInt_restartValue_10, ref logic_SubGraph_SaveLoadInt_integer_10, logic_SubGraph_SaveLoadInt_intAsVariable_10, logic_SubGraph_SaveLoadInt_uniqueID_10);
	}

	private void Relay_Restart_10()
	{
		logic_SubGraph_SaveLoadInt_integer_10 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_10 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Restart(logic_SubGraph_SaveLoadInt_restartValue_10, ref logic_SubGraph_SaveLoadInt_integer_10, logic_SubGraph_SaveLoadInt_intAsVariable_10, logic_SubGraph_SaveLoadInt_uniqueID_10);
	}

	private void Relay_Reached_12()
	{
		Relay_In_22();
	}

	private void Relay_Not_Reached_12()
	{
	}

	private void Relay_In_12()
	{
		logic_SubGraph_CheckStatsTarget_initialAmount_12 = local_InitialAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_currentAmount_12 = local_CurrentAmountTotal_System_Int32;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_12.In(logic_SubGraph_CheckStatsTarget_objectiveID_12, logic_SubGraph_CheckStatsTarget_totalAmount_12, logic_SubGraph_CheckStatsTarget_initialAmount_12, ref logic_SubGraph_CheckStatsTarget_currentAmount_12);
	}

	private void Relay_AllResources_18()
	{
		logic_uScript_GetMoneyFromResourceSales_Return_18 = logic_uScript_GetMoneyFromResourceSales_uScript_GetMoneyFromResourceSales_18.AllResources(logic_uScript_GetMoneyFromResourceSales_resourceType_18);
		local_InitialAmount_System_Int32 = logic_uScript_GetMoneyFromResourceSales_Return_18;
		if (logic_uScript_GetMoneyFromResourceSales_uScript_GetMoneyFromResourceSales_18.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_ResourcesOfType_18()
	{
		logic_uScript_GetMoneyFromResourceSales_Return_18 = logic_uScript_GetMoneyFromResourceSales_uScript_GetMoneyFromResourceSales_18.ResourcesOfType(logic_uScript_GetMoneyFromResourceSales_resourceType_18);
		local_InitialAmount_System_Int32 = logic_uScript_GetMoneyFromResourceSales_Return_18;
		if (logic_uScript_GetMoneyFromResourceSales_uScript_GetMoneyFromResourceSales_18.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_MoneyFromResourceSaleEvent_19()
	{
		local_CurrentAmountTotal_System_Int32 = event_UnityEngine_GameObject_MoneyTotal_19;
		Relay_In_12();
	}

	private void Relay_OnUpdate_20()
	{
		Relay_In_2();
	}

	private void Relay_OnSuspend_20()
	{
	}

	private void Relay_OnResume_20()
	{
	}

	private void Relay_In_21()
	{
		int num = 0;
		Array array = msgStart;
		if (logic_uScript_AddOnScreenMessage_locString_21.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_21, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_21, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_21 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_21.In(logic_uScript_AddOnScreenMessage_locString_21, logic_uScript_AddOnScreenMessage_msgPriority_21, logic_uScript_AddOnScreenMessage_holdMsg_21, logic_uScript_AddOnScreenMessage_tag_21, logic_uScript_AddOnScreenMessage_speaker_21, logic_uScript_AddOnScreenMessage_side_21);
	}

	private void Relay_In_22()
	{
		int num = 0;
		Array array = msgComplete;
		if (logic_uScript_AddOnScreenMessage_locString_22.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_22, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_22, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_22 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_22.In(logic_uScript_AddOnScreenMessage_locString_22, logic_uScript_AddOnScreenMessage_msgPriority_22, logic_uScript_AddOnScreenMessage_holdMsg_22, logic_uScript_AddOnScreenMessage_tag_22, logic_uScript_AddOnScreenMessage_speaker_22, logic_uScript_AddOnScreenMessage_side_22);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_22.Out)
		{
			Relay_Succeed_25();
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
}
