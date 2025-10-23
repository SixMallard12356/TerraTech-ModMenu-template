using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_NavigateToWaypoint", "")]
[NodePath("Graphs")]
public class SubGraph_NavigateToWaypoint : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private string external_18 = "";

	private float external_13;

	private ManOnScreenMessages.Speaker external_22;

	private LocalisedString[] external_9 = new LocalisedString[0];

	private Vector3 local_17_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_MsgCompleteShown_System_Boolean;

	private bool local_ObjectiveComplete_System_Boolean;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_16;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_2;

	private bool logic_uScriptCon_CompareBool_True_2 = true;

	private bool logic_uScriptCon_CompareBool_False_2 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_8 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_8 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_8;

	private string logic_uScript_AddOnScreenMessage_tag_8 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_8;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_8;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_8;

	private bool logic_uScript_AddOnScreenMessage_Out_8 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_8 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_11 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_11;

	private bool logic_uScriptAct_SetBool_Out_11 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_11 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_11 = true;

	private uScript_PlayerInRange logic_uScript_PlayerInRange_uScript_PlayerInRange_14 = new uScript_PlayerInRange();

	private Vector3 logic_uScript_PlayerInRange_position_14;

	private float logic_uScript_PlayerInRange_range_14;

	private bool logic_uScript_PlayerInRange_True_14 = true;

	private bool logic_uScript_PlayerInRange_False_14 = true;

	private bool logic_uScript_PlayerInRange_Out_14 = true;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_15 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_15;

	private string logic_uScript_GetPositionInEncounter_posName_15 = "";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_15;

	private bool logic_uScript_GetPositionInEncounter_Out_15 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_19;

	private bool logic_uScriptCon_CompareBool_True_19 = true;

	private bool logic_uScriptCon_CompareBool_False_19 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_20 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_20;

	private bool logic_uScriptAct_SetBool_Out_20 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_20 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_20 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_44;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_44 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_44 = "ObjectiveComplete_NavigateToWaypoint";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_49;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_49 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_49 = "MsgCompleteShown_NavigateToWaypoint";

	[FriendlyName("Complete")]
	public event uScriptEventHandler Complete;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
			if (null != owner_Connection_7)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_7.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_7.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_6;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_6;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_6;
				}
			}
		}
		if (null == owner_Connection_16 || !m_RegisteredForEvents)
		{
			owner_Connection_16 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_7)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_7.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_7.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_6;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_6;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_6;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_7)
		{
			uScript_SaveLoad component = owner_Connection_7.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_6;
				component.LoadEvent -= Instance_LoadEvent_6;
				component.RestartEvent -= Instance_RestartEvent_6;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_11.SetParent(g);
		logic_uScript_PlayerInRange_uScript_PlayerInRange_14.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_15.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_20.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.SetParent(g);
		owner_Connection_7 = parentGameObject;
		owner_Connection_16 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save_Out += SubGraph_SaveLoadBool_Save_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load_Out += SubGraph_SaveLoadBool_Load_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save_Out += SubGraph_SaveLoadBool_Save_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load_Out += SubGraph_SaveLoadBool_Load_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_49;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save_Out -= SubGraph_SaveLoadBool_Save_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load_Out -= SubGraph_SaveLoadBool_Load_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save_Out -= SubGraph_SaveLoadBool_Save_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load_Out -= SubGraph_SaveLoadBool_Load_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_49;
	}

	private void Instance_SaveEvent_6(object o, EventArgs e)
	{
		Relay_SaveEvent_6();
	}

	private void Instance_LoadEvent_6(object o, EventArgs e)
	{
		Relay_LoadEvent_6();
	}

	private void Instance_RestartEvent_6(object o, EventArgs e)
	{
		Relay_RestartEvent_6();
	}

	private void SubGraph_SaveLoadBool_Save_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Save_Out_44();
	}

	private void SubGraph_SaveLoadBool_Load_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Load_Out_44();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Restart_Out_44();
	}

	private void SubGraph_SaveLoadBool_Save_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_MsgCompleteShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Save_Out_49();
	}

	private void SubGraph_SaveLoadBool_Load_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_MsgCompleteShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Load_Out_49();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_MsgCompleteShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Restart_Out_49();
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("encounterPos", "")] string encounterPos, [FriendlyName("distInRangeOfWaypoint", "")] float distInRangeOfWaypoint, [FriendlyName("messageSpeaker", "")] ManOnScreenMessages.Speaker messageSpeaker, [FriendlyName("msgComplete", "")] LocalisedString[] msgComplete)
	{
		external_18 = encounterPos;
		external_13 = distInRangeOfWaypoint;
		external_22 = messageSpeaker;
		external_9 = msgComplete;
		Relay_In_2();
	}

	private void Relay_Connection_0()
	{
	}

	private void Relay_Connection_1()
	{
		if (this.Complete != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Complete(this, args);
		}
	}

	private void Relay_In_2()
	{
		logic_uScriptCon_CompareBool_Bool_2 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.In(logic_uScriptCon_CompareBool_Bool_2);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.False;
		if (num)
		{
			Relay_In_19();
		}
		if (flag)
		{
			Relay_In_15();
		}
	}

	private void Relay_SaveEvent_6()
	{
		Relay_Save_44();
	}

	private void Relay_LoadEvent_6()
	{
		Relay_Load_44();
	}

	private void Relay_RestartEvent_6()
	{
		Relay_Set_False_44();
	}

	private void Relay_In_8()
	{
		int num = 0;
		Array array = external_9;
		if (logic_uScript_AddOnScreenMessage_locString_8.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_8, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_8, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_8 = external_22;
		logic_uScript_AddOnScreenMessage_Return_8 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8.In(logic_uScript_AddOnScreenMessage_locString_8, logic_uScript_AddOnScreenMessage_msgPriority_8, logic_uScript_AddOnScreenMessage_holdMsg_8, logic_uScript_AddOnScreenMessage_tag_8, logic_uScript_AddOnScreenMessage_speaker_8, logic_uScript_AddOnScreenMessage_side_8);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8.Out)
		{
			Relay_Connection_1();
		}
	}

	private void Relay_Connection_9()
	{
	}

	private void Relay_True_11()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_11.True(out logic_uScriptAct_SetBool_Target_11);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_11;
	}

	private void Relay_False_11()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_11.False(out logic_uScriptAct_SetBool_Target_11);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_11;
	}

	private void Relay_Connection_13()
	{
	}

	private void Relay_In_14()
	{
		logic_uScript_PlayerInRange_position_14 = local_17_UnityEngine_Vector3;
		logic_uScript_PlayerInRange_range_14 = external_13;
		logic_uScript_PlayerInRange_uScript_PlayerInRange_14.In(logic_uScript_PlayerInRange_position_14, logic_uScript_PlayerInRange_range_14);
		if (logic_uScript_PlayerInRange_uScript_PlayerInRange_14.True)
		{
			Relay_True_11();
		}
	}

	private void Relay_In_15()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_15 = owner_Connection_16;
		logic_uScript_GetPositionInEncounter_posName_15 = external_18;
		logic_uScript_GetPositionInEncounter_Return_15 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_15.In(logic_uScript_GetPositionInEncounter_ownerNode_15, logic_uScript_GetPositionInEncounter_posName_15);
		local_17_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_15;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_15.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_Connection_18()
	{
	}

	private void Relay_In_19()
	{
		logic_uScriptCon_CompareBool_Bool_19 = local_MsgCompleteShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.In(logic_uScriptCon_CompareBool_Bool_19);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.False;
		if (num)
		{
			Relay_Connection_1();
		}
		if (flag)
		{
			Relay_True_20();
		}
	}

	private void Relay_True_20()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_20.True(out logic_uScriptAct_SetBool_Target_20);
		local_MsgCompleteShown_System_Boolean = logic_uScriptAct_SetBool_Target_20;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_20.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_False_20()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_20.False(out logic_uScriptAct_SetBool_Target_20);
		local_MsgCompleteShown_System_Boolean = logic_uScriptAct_SetBool_Target_20;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_20.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_Connection_22()
	{
	}

	private void Relay_Save_Out_44()
	{
		Relay_Save_49();
	}

	private void Relay_Load_Out_44()
	{
		Relay_Load_49();
	}

	private void Relay_Restart_Out_44()
	{
		Relay_Set_False_49();
	}

	private void Relay_Save_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Load_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Set_True_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Set_False_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Save_Out_49()
	{
	}

	private void Relay_Load_Out_49()
	{
	}

	private void Relay_Restart_Out_49()
	{
	}

	private void Relay_Save_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_MsgCompleteShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_MsgCompleteShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Load_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_MsgCompleteShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_MsgCompleteShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Set_True_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_MsgCompleteShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_MsgCompleteShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Set_False_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_MsgCompleteShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_MsgCompleteShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}
}
