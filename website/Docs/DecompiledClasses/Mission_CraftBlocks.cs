using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_CraftBlocks : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private BlockTypes local_BlockType_BlockTypes;

	private int local_CurrentAmountTotal_System_Int32;

	private int local_CurrentAmountType_System_Int32;

	private bool local_Init_System_Boolean;

	private int local_InitialAmount_System_Int32;

	public LocalisedString[] msgComplete = new LocalisedString[0];

	public LocalisedString[] msgStart = new LocalisedString[0];

	public int targetAmount;

	public BlockTypes targetBlockType;

	public bool useBlockType;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_13;

	private GameObject owner_Connection_41;

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

	private uScript_CompareBlockTypes logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_16 = new uScript_CompareBlockTypes();

	private BlockTypes logic_uScript_CompareBlockTypes_A_16;

	private BlockTypes logic_uScript_CompareBlockTypes_B_16;

	private bool logic_uScript_CompareBlockTypes_EqualTo_16 = true;

	private bool logic_uScript_CompareBlockTypes_NotEqualTo_16 = true;

	private uScript_GetNumBlocksCrafted logic_uScript_GetNumBlocksCrafted_uScript_GetNumBlocksCrafted_20 = new uScript_GetNumBlocksCrafted();

	private BlockTypes logic_uScript_GetNumBlocksCrafted_blockType_20;

	private int logic_uScript_GetNumBlocksCrafted_Return_20;

	private bool logic_uScript_GetNumBlocksCrafted_Out_20 = true;

	private SubGraph_CheckStatsTarget logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23 = new SubGraph_CheckStatsTarget();

	private int logic_SubGraph_CheckStatsTarget_objectiveID_23 = 1;

	private int logic_SubGraph_CheckStatsTarget_totalAmount_23;

	private int logic_SubGraph_CheckStatsTarget_initialAmount_23;

	private int logic_SubGraph_CheckStatsTarget_currentAmount_23;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_25 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_25 = true;

	private SubGraph_CheckStatsTarget logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28 = new SubGraph_CheckStatsTarget();

	private int logic_SubGraph_CheckStatsTarget_objectiveID_28 = 1;

	private int logic_SubGraph_CheckStatsTarget_totalAmount_28;

	private int logic_SubGraph_CheckStatsTarget_initialAmount_28;

	private int logic_SubGraph_CheckStatsTarget_currentAmount_28;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_32;

	private bool logic_uScriptCon_CompareBool_True_32 = true;

	private bool logic_uScriptCon_CompareBool_False_32 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_35;

	private bool logic_uScriptCon_CompareBool_True_35 = true;

	private bool logic_uScriptCon_CompareBool_False_35 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_36 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_36 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_36;

	private string logic_uScript_AddOnScreenMessage_tag_36 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_36;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_36;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_36;

	private bool logic_uScript_AddOnScreenMessage_Out_36 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_36 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_38 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_38 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_38 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_38;

	private string logic_uScript_AddOnScreenMessage_tag_38 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_38;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_38;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_38;

	private bool logic_uScript_AddOnScreenMessage_Out_38 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_38 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_40 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_40;

	private bool logic_uScript_FinishEncounter_Out_40 = true;

	private BlockTypes event_UnityEngine_GameObject_BlockType_14;

	private int event_UnityEngine_GameObject_BlockTypeTotal_14;

	private int event_UnityEngine_GameObject_BlockTotal_14;

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
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_21;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_21;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_21;
				}
			}
		}
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_BlockCraftedEvent uScript_BlockCraftedEvent2 = owner_Connection_1.GetComponent<uScript_BlockCraftedEvent>();
				if (null == uScript_BlockCraftedEvent2)
				{
					uScript_BlockCraftedEvent2 = owner_Connection_1.AddComponent<uScript_BlockCraftedEvent>();
				}
				if (null != uScript_BlockCraftedEvent2)
				{
					uScript_BlockCraftedEvent2.BlockCraftedEvent += Instance_BlockCraftedEvent_14;
				}
			}
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
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_8;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_8;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_8;
				}
			}
		}
		if (null == owner_Connection_41 || !m_RegisteredForEvents)
		{
			owner_Connection_41 = parentGameObject;
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
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_21;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_21;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_21;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_BlockCraftedEvent uScript_BlockCraftedEvent2 = owner_Connection_1.GetComponent<uScript_BlockCraftedEvent>();
			if (null == uScript_BlockCraftedEvent2)
			{
				uScript_BlockCraftedEvent2 = owner_Connection_1.AddComponent<uScript_BlockCraftedEvent>();
			}
			if (null != uScript_BlockCraftedEvent2)
			{
				uScript_BlockCraftedEvent2.BlockCraftedEvent += Instance_BlockCraftedEvent_14;
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
				component.OnUpdate -= Instance_OnUpdate_21;
				component.OnSuspend -= Instance_OnSuspend_21;
				component.OnResume -= Instance_OnResume_21;
			}
		}
		if (null != owner_Connection_1)
		{
			uScript_BlockCraftedEvent component2 = owner_Connection_1.GetComponent<uScript_BlockCraftedEvent>();
			if (null != component2)
			{
				component2.BlockCraftedEvent -= Instance_BlockCraftedEvent_14;
			}
		}
		if (null != owner_Connection_13)
		{
			uScript_SaveLoad component3 = owner_Connection_13.GetComponent<uScript_SaveLoad>();
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
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_16.SetParent(g);
		logic_uScript_GetNumBlocksCrafted_uScript_GetNumBlocksCrafted_20.SetParent(g);
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_25.SetParent(g);
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_38.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_40.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_1 = parentGameObject;
		owner_Connection_13 = parentGameObject;
		owner_Connection_41 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Awake();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23.Awake();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Save_Out += SubGraph_SaveLoadInt_Save_Out_10;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Load_Out += SubGraph_SaveLoadInt_Load_Out_10;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_10;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23.Reached += SubGraph_CheckStatsTarget_Reached_23;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23.Not_Reached += SubGraph_CheckStatsTarget_Not_Reached_23;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28.Reached += SubGraph_CheckStatsTarget_Reached_28;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28.Not_Reached += SubGraph_CheckStatsTarget_Not_Reached_28;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Start();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23.Start();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.OnEnable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23.OnEnable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.OnDisable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23.OnDisable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_38.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Update();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23.Update();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.OnDestroy();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23.OnDestroy();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Save_Out -= SubGraph_SaveLoadInt_Save_Out_10;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Load_Out -= SubGraph_SaveLoadInt_Load_Out_10;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_10.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_10;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23.Reached -= SubGraph_CheckStatsTarget_Reached_23;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23.Not_Reached -= SubGraph_CheckStatsTarget_Not_Reached_23;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28.Reached -= SubGraph_CheckStatsTarget_Reached_28;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28.Not_Reached -= SubGraph_CheckStatsTarget_Not_Reached_28;
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

	private void Instance_BlockCraftedEvent_14(object o, uScript_BlockCraftedEvent.BlockCraftedEventArgs e)
	{
		event_UnityEngine_GameObject_BlockType_14 = e.BlockType;
		event_UnityEngine_GameObject_BlockTypeTotal_14 = e.BlockTypeTotal;
		event_UnityEngine_GameObject_BlockTotal_14 = e.BlockTotal;
		Relay_BlockCraftedEvent_14();
	}

	private void Instance_OnUpdate_21(object o, EventArgs e)
	{
		Relay_OnUpdate_21();
	}

	private void Instance_OnSuspend_21(object o, EventArgs e)
	{
		Relay_OnSuspend_21();
	}

	private void Instance_OnResume_21(object o, EventArgs e)
	{
		Relay_OnResume_21();
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

	private void SubGraph_CheckStatsTarget_Reached_23(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_23 = e.currentAmount;
		local_CurrentAmountType_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_23;
		Relay_Reached_23();
	}

	private void SubGraph_CheckStatsTarget_Not_Reached_23(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_23 = e.currentAmount;
		local_CurrentAmountType_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_23;
		Relay_Not_Reached_23();
	}

	private void SubGraph_CheckStatsTarget_Reached_28(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_28 = e.currentAmount;
		local_CurrentAmountTotal_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_28;
		Relay_Reached_28();
	}

	private void SubGraph_CheckStatsTarget_Not_Reached_28(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_28 = e.currentAmount;
		local_CurrentAmountTotal_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_28;
		Relay_Not_Reached_28();
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
			Relay_In_35();
		}
	}

	private void Relay_False_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.False(out logic_uScriptAct_SetBool_Target_4);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_35();
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

	private void Relay_BlockCraftedEvent_14()
	{
		local_BlockType_BlockTypes = event_UnityEngine_GameObject_BlockType_14;
		local_CurrentAmountType_System_Int32 = event_UnityEngine_GameObject_BlockTypeTotal_14;
		local_CurrentAmountTotal_System_Int32 = event_UnityEngine_GameObject_BlockTotal_14;
		Relay_In_32();
	}

	private void Relay_In_16()
	{
		logic_uScript_CompareBlockTypes_A_16 = local_BlockType_BlockTypes;
		logic_uScript_CompareBlockTypes_B_16 = targetBlockType;
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_16.In(logic_uScript_CompareBlockTypes_A_16, logic_uScript_CompareBlockTypes_B_16);
		if (logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_16.EqualTo)
		{
			Relay_In_23();
		}
	}

	private void Relay_AllBlocks_20()
	{
		logic_uScript_GetNumBlocksCrafted_blockType_20 = targetBlockType;
		logic_uScript_GetNumBlocksCrafted_Return_20 = logic_uScript_GetNumBlocksCrafted_uScript_GetNumBlocksCrafted_20.AllBlocks(logic_uScript_GetNumBlocksCrafted_blockType_20);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumBlocksCrafted_Return_20;
		if (logic_uScript_GetNumBlocksCrafted_uScript_GetNumBlocksCrafted_20.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_BlocksOfType_20()
	{
		logic_uScript_GetNumBlocksCrafted_blockType_20 = targetBlockType;
		logic_uScript_GetNumBlocksCrafted_Return_20 = logic_uScript_GetNumBlocksCrafted_uScript_GetNumBlocksCrafted_20.BlocksOfType(logic_uScript_GetNumBlocksCrafted_blockType_20);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumBlocksCrafted_Return_20;
		if (logic_uScript_GetNumBlocksCrafted_uScript_GetNumBlocksCrafted_20.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_OnUpdate_21()
	{
		Relay_In_2();
	}

	private void Relay_OnSuspend_21()
	{
	}

	private void Relay_OnResume_21()
	{
	}

	private void Relay_Reached_23()
	{
		Relay_In_38();
	}

	private void Relay_Not_Reached_23()
	{
	}

	private void Relay_In_23()
	{
		logic_SubGraph_CheckStatsTarget_initialAmount_23 = local_InitialAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_currentAmount_23 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_23.In(logic_SubGraph_CheckStatsTarget_objectiveID_23, logic_SubGraph_CheckStatsTarget_totalAmount_23, logic_SubGraph_CheckStatsTarget_initialAmount_23, ref logic_SubGraph_CheckStatsTarget_currentAmount_23);
	}

	private void Relay_In_25()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_25.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_25.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_Reached_28()
	{
		Relay_In_38();
	}

	private void Relay_Not_Reached_28()
	{
	}

	private void Relay_In_28()
	{
		logic_SubGraph_CheckStatsTarget_initialAmount_28 = local_InitialAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_currentAmount_28 = local_CurrentAmountTotal_System_Int32;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_28.In(logic_SubGraph_CheckStatsTarget_objectiveID_28, logic_SubGraph_CheckStatsTarget_totalAmount_28, logic_SubGraph_CheckStatsTarget_initialAmount_28, ref logic_SubGraph_CheckStatsTarget_currentAmount_28);
	}

	private void Relay_In_32()
	{
		logic_uScriptCon_CompareBool_Bool_32 = useBlockType;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32.In(logic_uScriptCon_CompareBool_Bool_32);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32.False;
		if (num)
		{
			Relay_In_16();
		}
		if (flag)
		{
			Relay_In_25();
		}
	}

	private void Relay_In_35()
	{
		logic_uScriptCon_CompareBool_Bool_35 = useBlockType;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.In(logic_uScriptCon_CompareBool_Bool_35);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.False;
		if (num)
		{
			Relay_BlocksOfType_20();
		}
		if (flag)
		{
			Relay_AllBlocks_20();
		}
	}

	private void Relay_In_36()
	{
		int num = 0;
		Array array = msgStart;
		if (logic_uScript_AddOnScreenMessage_locString_36.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_36, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_36, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_36 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.In(logic_uScript_AddOnScreenMessage_locString_36, logic_uScript_AddOnScreenMessage_msgPriority_36, logic_uScript_AddOnScreenMessage_holdMsg_36, logic_uScript_AddOnScreenMessage_tag_36, logic_uScript_AddOnScreenMessage_speaker_36, logic_uScript_AddOnScreenMessage_side_36);
	}

	private void Relay_In_38()
	{
		int num = 0;
		Array array = msgComplete;
		if (logic_uScript_AddOnScreenMessage_locString_38.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_38, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_38, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_38 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_38.In(logic_uScript_AddOnScreenMessage_locString_38, logic_uScript_AddOnScreenMessage_msgPriority_38, logic_uScript_AddOnScreenMessage_holdMsg_38, logic_uScript_AddOnScreenMessage_tag_38, logic_uScript_AddOnScreenMessage_speaker_38, logic_uScript_AddOnScreenMessage_side_38);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_38.Out)
		{
			Relay_Succeed_40();
		}
	}

	private void Relay_Succeed_40()
	{
		logic_uScript_FinishEncounter_owner_40 = owner_Connection_41;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_40.Succeed(logic_uScript_FinishEncounter_owner_40);
	}

	private void Relay_Fail_40()
	{
		logic_uScript_FinishEncounter_owner_40 = owner_Connection_41;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_40.Fail(logic_uScript_FinishEncounter_owner_40);
	}
}
