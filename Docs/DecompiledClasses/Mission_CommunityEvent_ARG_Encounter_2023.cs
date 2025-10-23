using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_CommunityEvent_ARG_Encounter_2023 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private bool local_HUBLTextShown_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker01;

	public uScript_AddMessage.MessageSpeaker messageSpeaker02;

	public uScript_AddMessage.MessageData msg01HUBL;

	public uScript_AddMessage.MessageData msg02RUSTY;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_10;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_0 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_0;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_0 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_0 = "Stage";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_3 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_3;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_3;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_3;

	private bool logic_uScript_AddMessage_Out_3 = true;

	private bool logic_uScript_AddMessage_Shown_3 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_5;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_9 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_9;

	private bool logic_uScript_FinishEncounter_Out_9 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_16 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_16;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_16;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_16;

	private bool logic_uScript_AddMessage_Out_16 = true;

	private bool logic_uScript_AddMessage_Shown_16 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_32;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_32 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_32 = "HUBLTextiShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_39 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_39;

	private bool logic_uScriptAct_SetBool_Out_39 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_39 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_39 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_42;

	private bool logic_uScriptCon_CompareBool_True_42 = true;

	private bool logic_uScriptCon_CompareBool_False_42 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
			if (null != owner_Connection_2)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_2.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_6;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_6;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_6;
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
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_13;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_13;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_13;
				}
			}
		}
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_2)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_2.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_6;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_6;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_6;
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
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_13;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_13;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_13;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_2)
		{
			uScript_EncounterUpdate component = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_6;
				component.OnSuspend -= Instance_OnSuspend_6;
				component.OnResume -= Instance_OnResume_6;
			}
		}
		if (null != owner_Connection_8)
		{
			uScript_SaveLoad component2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_13;
				component2.LoadEvent -= Instance_LoadEvent_13;
				component2.RestartEvent -= Instance_RestartEvent_13;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_3.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_9.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_16.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_10 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Save_Out += SubGraph_SaveLoadInt_Save_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Load_Out += SubGraph_SaveLoadInt_Load_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output1 += uScriptCon_ManualSwitch_Output1_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output2 += uScriptCon_ManualSwitch_Output2_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output3 += uScriptCon_ManualSwitch_Output3_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output4 += uScriptCon_ManualSwitch_Output4_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output5 += uScriptCon_ManualSwitch_Output5_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output6 += uScriptCon_ManualSwitch_Output6_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output7 += uScriptCon_ManualSwitch_Output7_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output8 += uScriptCon_ManualSwitch_Output8_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Save_Out += SubGraph_SaveLoadBool_Save_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Load_Out += SubGraph_SaveLoadBool_Load_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_32;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_3.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_16.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Save_Out -= SubGraph_SaveLoadInt_Save_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Load_Out -= SubGraph_SaveLoadInt_Load_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output1 -= uScriptCon_ManualSwitch_Output1_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output2 -= uScriptCon_ManualSwitch_Output2_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output3 -= uScriptCon_ManualSwitch_Output3_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output4 -= uScriptCon_ManualSwitch_Output4_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output5 -= uScriptCon_ManualSwitch_Output5_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output6 -= uScriptCon_ManualSwitch_Output6_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output7 -= uScriptCon_ManualSwitch_Output7_5;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.Output8 -= uScriptCon_ManualSwitch_Output8_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Save_Out -= SubGraph_SaveLoadBool_Save_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Load_Out -= SubGraph_SaveLoadBool_Load_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_32;
	}

	private void Instance_OnUpdate_6(object o, EventArgs e)
	{
		Relay_OnUpdate_6();
	}

	private void Instance_OnSuspend_6(object o, EventArgs e)
	{
		Relay_OnSuspend_6();
	}

	private void Instance_OnResume_6(object o, EventArgs e)
	{
		Relay_OnResume_6();
	}

	private void Instance_SaveEvent_13(object o, EventArgs e)
	{
		Relay_SaveEvent_13();
	}

	private void Instance_LoadEvent_13(object o, EventArgs e)
	{
		Relay_LoadEvent_13();
	}

	private void Instance_RestartEvent_13(object o, EventArgs e)
	{
		Relay_RestartEvent_13();
	}

	private void SubGraph_SaveLoadInt_Save_Out_0(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_0 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_0;
		Relay_Save_Out_0();
	}

	private void SubGraph_SaveLoadInt_Load_Out_0(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_0 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_0;
		Relay_Load_Out_0();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_0(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_0 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_0;
		Relay_Restart_Out_0();
	}

	private void uScriptCon_ManualSwitch_Output1_5(object o, EventArgs e)
	{
		Relay_Output1_5();
	}

	private void uScriptCon_ManualSwitch_Output2_5(object o, EventArgs e)
	{
		Relay_Output2_5();
	}

	private void uScriptCon_ManualSwitch_Output3_5(object o, EventArgs e)
	{
		Relay_Output3_5();
	}

	private void uScriptCon_ManualSwitch_Output4_5(object o, EventArgs e)
	{
		Relay_Output4_5();
	}

	private void uScriptCon_ManualSwitch_Output5_5(object o, EventArgs e)
	{
		Relay_Output5_5();
	}

	private void uScriptCon_ManualSwitch_Output6_5(object o, EventArgs e)
	{
		Relay_Output6_5();
	}

	private void uScriptCon_ManualSwitch_Output7_5(object o, EventArgs e)
	{
		Relay_Output7_5();
	}

	private void uScriptCon_ManualSwitch_Output8_5(object o, EventArgs e)
	{
		Relay_Output8_5();
	}

	private void SubGraph_SaveLoadBool_Save_Out_32(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = e.boolean;
		local_HUBLTextShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_32;
		Relay_Save_Out_32();
	}

	private void SubGraph_SaveLoadBool_Load_Out_32(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = e.boolean;
		local_HUBLTextShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_32;
		Relay_Load_Out_32();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_32(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = e.boolean;
		local_HUBLTextShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_32;
		Relay_Restart_Out_32();
	}

	private void Relay_Save_Out_0()
	{
		Relay_Save_32();
	}

	private void Relay_Load_Out_0()
	{
		Relay_Load_32();
	}

	private void Relay_Restart_Out_0()
	{
		Relay_Set_False_32();
	}

	private void Relay_Save_0()
	{
		logic_SubGraph_SaveLoadInt_integer_0 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_0 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Save(logic_SubGraph_SaveLoadInt_restartValue_0, ref logic_SubGraph_SaveLoadInt_integer_0, logic_SubGraph_SaveLoadInt_intAsVariable_0, logic_SubGraph_SaveLoadInt_uniqueID_0);
	}

	private void Relay_Load_0()
	{
		logic_SubGraph_SaveLoadInt_integer_0 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_0 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Load(logic_SubGraph_SaveLoadInt_restartValue_0, ref logic_SubGraph_SaveLoadInt_integer_0, logic_SubGraph_SaveLoadInt_intAsVariable_0, logic_SubGraph_SaveLoadInt_uniqueID_0);
	}

	private void Relay_Restart_0()
	{
		logic_SubGraph_SaveLoadInt_integer_0 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_0 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Restart(logic_SubGraph_SaveLoadInt_restartValue_0, ref logic_SubGraph_SaveLoadInt_integer_0, logic_SubGraph_SaveLoadInt_intAsVariable_0, logic_SubGraph_SaveLoadInt_uniqueID_0);
	}

	private void Relay_In_3()
	{
		logic_uScript_AddMessage_messageData_3 = msg01HUBL;
		logic_uScript_AddMessage_speaker_3 = messageSpeaker01;
		logic_uScript_AddMessage_Return_3 = logic_uScript_AddMessage_uScript_AddMessage_3.In(logic_uScript_AddMessage_messageData_3, logic_uScript_AddMessage_speaker_3);
		if (logic_uScript_AddMessage_uScript_AddMessage_3.Shown)
		{
			Relay_True_39();
		}
	}

	private void Relay_Output1_5()
	{
		Relay_In_42();
	}

	private void Relay_Output2_5()
	{
	}

	private void Relay_Output3_5()
	{
	}

	private void Relay_Output4_5()
	{
	}

	private void Relay_Output5_5()
	{
	}

	private void Relay_Output6_5()
	{
	}

	private void Relay_Output7_5()
	{
	}

	private void Relay_Output8_5()
	{
	}

	private void Relay_In_5()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_5 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_5.In(logic_uScriptCon_ManualSwitch_CurrentOutput_5);
	}

	private void Relay_OnUpdate_6()
	{
		Relay_In_5();
	}

	private void Relay_OnSuspend_6()
	{
	}

	private void Relay_OnResume_6()
	{
	}

	private void Relay_Succeed_9()
	{
		logic_uScript_FinishEncounter_owner_9 = owner_Connection_10;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_9.Succeed(logic_uScript_FinishEncounter_owner_9);
	}

	private void Relay_Fail_9()
	{
		logic_uScript_FinishEncounter_owner_9 = owner_Connection_10;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_9.Fail(logic_uScript_FinishEncounter_owner_9);
	}

	private void Relay_SaveEvent_13()
	{
		Relay_Save_0();
	}

	private void Relay_LoadEvent_13()
	{
		Relay_Load_0();
	}

	private void Relay_RestartEvent_13()
	{
		Relay_Restart_0();
	}

	private void Relay_In_16()
	{
		logic_uScript_AddMessage_messageData_16 = msg02RUSTY;
		logic_uScript_AddMessage_speaker_16 = messageSpeaker02;
		logic_uScript_AddMessage_Return_16 = logic_uScript_AddMessage_uScript_AddMessage_16.In(logic_uScript_AddMessage_messageData_16, logic_uScript_AddMessage_speaker_16);
		if (logic_uScript_AddMessage_uScript_AddMessage_16.Shown)
		{
			Relay_Succeed_9();
		}
	}

	private void Relay_Save_Out_32()
	{
	}

	private void Relay_Load_Out_32()
	{
	}

	private void Relay_Restart_Out_32()
	{
	}

	private void Relay_Save_32()
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = local_HUBLTextShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_32 = local_HUBLTextShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Save(ref logic_SubGraph_SaveLoadBool_boolean_32, logic_SubGraph_SaveLoadBool_boolAsVariable_32, logic_SubGraph_SaveLoadBool_uniqueID_32);
	}

	private void Relay_Load_32()
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = local_HUBLTextShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_32 = local_HUBLTextShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Load(ref logic_SubGraph_SaveLoadBool_boolean_32, logic_SubGraph_SaveLoadBool_boolAsVariable_32, logic_SubGraph_SaveLoadBool_uniqueID_32);
	}

	private void Relay_Set_True_32()
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = local_HUBLTextShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_32 = local_HUBLTextShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_32, logic_SubGraph_SaveLoadBool_boolAsVariable_32, logic_SubGraph_SaveLoadBool_uniqueID_32);
	}

	private void Relay_Set_False_32()
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = local_HUBLTextShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_32 = local_HUBLTextShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_32, logic_SubGraph_SaveLoadBool_boolAsVariable_32, logic_SubGraph_SaveLoadBool_uniqueID_32);
	}

	private void Relay_True_39()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.True(out logic_uScriptAct_SetBool_Target_39);
		local_HUBLTextShown_System_Boolean = logic_uScriptAct_SetBool_Target_39;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_39.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_False_39()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.False(out logic_uScriptAct_SetBool_Target_39);
		local_HUBLTextShown_System_Boolean = logic_uScriptAct_SetBool_Target_39;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_39.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_42()
	{
		logic_uScriptCon_CompareBool_Bool_42 = local_HUBLTextShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.In(logic_uScriptCon_CompareBool_Bool_42);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.False;
		if (num)
		{
			Relay_In_16();
		}
		if (flag)
		{
			Relay_In_3();
		}
	}
}
