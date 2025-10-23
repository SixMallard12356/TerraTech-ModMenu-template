using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Subgraph_Warnings_PlayerTippedOver", "")]
public class Subgraph_Warnings_PlayerTippedOver : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private uScript_AddMessage.MessageData external_19;

	private uScript_AddMessage.MessageData external_21;

	private uScript_AddMessage.MessageData external_23;

	private uScript_AddMessage.MessageData external_24;

	private uScript_AddMessage.MessageSpeaker external_31;

	private bool local_FallenOver_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgTippedOverFallen_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgTippedOverFallen_Pad_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgTippedOverRecovered_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgTippedOverRecovered_Pad_ManOnScreenMessages_OnScreenMessage;

	private uScript_CheckIfPlayerTippedOver logic_uScript_CheckIfPlayerTippedOver_uScript_CheckIfPlayerTippedOver_0 = new uScript_CheckIfPlayerTippedOver();

	private bool logic_uScript_CheckIfPlayerTippedOver_TippedOver_0 = true;

	private bool logic_uScript_CheckIfPlayerTippedOver_NotTippedOver_0 = true;

	private bool logic_uScript_CheckIfPlayerTippedOver_Recovered_0 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_2 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_2;

	private bool logic_uScript_RemoveOnScreenMessage_instant_2 = true;

	private bool logic_uScript_RemoveOnScreenMessage_Out_2 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_5 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_5;

	private bool logic_uScript_RemoveOnScreenMessage_instant_5;

	private bool logic_uScript_RemoveOnScreenMessage_Out_5 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_7 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_7;

	private bool logic_uScriptAct_SetBool_Out_7 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_7 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_7 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_8;

	private bool logic_uScriptCon_CompareBool_True_8 = true;

	private bool logic_uScriptCon_CompareBool_False_8 = true;

	private uScript_IsPlayerInBeam logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_11 = new uScript_IsPlayerInBeam();

	private bool logic_uScript_IsPlayerInBeam_True_11 = true;

	private bool logic_uScript_IsPlayerInBeam_False_11 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_13 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_13;

	private bool logic_uScriptAct_SetBool_Out_13 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_13 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_13 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_16 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_16;

	private bool logic_uScriptAct_SetBool_Out_16 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_16 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_16 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_17 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_17;

	private bool logic_uScript_GetPlayerTank_Returned_17 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_17 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_20;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_20;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_20;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_20;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_20;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_22;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_22;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_22;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_22;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_22;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_27 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_27;

	private bool logic_uScript_RemoveOnScreenMessage_instant_27 = true;

	private bool logic_uScript_RemoveOnScreenMessage_Out_27 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_30 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_30;

	private bool logic_uScript_RemoveOnScreenMessage_instant_30;

	private bool logic_uScript_RemoveOnScreenMessage_Out_30 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_CheckIfPlayerTippedOver_uScript_CheckIfPlayerTippedOver_0.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_2.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_5.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.SetParent(g);
		logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_11.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_16.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_17.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_27.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_30.SetParent(g);
	}

	public void Awake()
	{
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20.Out += SubGraph_AddMessageWithPadSupport_Out_20;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20.Shown += SubGraph_AddMessageWithPadSupport_Shown_20;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22.Out += SubGraph_AddMessageWithPadSupport_Out_22;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22.Shown += SubGraph_AddMessageWithPadSupport_Shown_22;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20.Out -= SubGraph_AddMessageWithPadSupport_Out_20;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20.Shown -= SubGraph_AddMessageWithPadSupport_Shown_20;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22.Out -= SubGraph_AddMessageWithPadSupport_Out_22;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22.Shown -= SubGraph_AddMessageWithPadSupport_Shown_22;
	}

	private void SubGraph_AddMessageWithPadSupport_Out_20(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_20 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_20 = e.messageControlPadReturn;
		local_MsgTippedOverFallen_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_20;
		local_MsgTippedOverFallen_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_20;
		Relay_Out_20();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_20(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_20 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_20 = e.messageControlPadReturn;
		local_MsgTippedOverFallen_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_20;
		local_MsgTippedOverFallen_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_20;
		Relay_Shown_20();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_22(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_22 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_22 = e.messageControlPadReturn;
		local_MsgTippedOverRecovered_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_22;
		local_MsgTippedOverRecovered_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_22;
		Relay_Out_22();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_22(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_22 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_22 = e.messageControlPadReturn;
		local_MsgTippedOverRecovered_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_22;
		local_MsgTippedOverRecovered_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_22;
		Relay_Shown_22();
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("msgTippedOverFallen", "")] uScript_AddMessage.MessageData msgTippedOverFallen, [FriendlyName("msgTippedOverFallen_Pad", "")] uScript_AddMessage.MessageData msgTippedOverFallen_Pad, [FriendlyName("msgTippedOverRecovered", "")] uScript_AddMessage.MessageData msgTippedOverRecovered, [FriendlyName("msgTippedOverRecovered_Pad", "")] uScript_AddMessage.MessageData msgTippedOverRecovered_Pad, [FriendlyName("messageSpeaker", "")] uScript_AddMessage.MessageSpeaker messageSpeaker)
	{
		external_19 = msgTippedOverFallen;
		external_21 = msgTippedOverFallen_Pad;
		external_23 = msgTippedOverRecovered;
		external_24 = msgTippedOverRecovered_Pad;
		external_31 = messageSpeaker;
		Relay_In_17();
	}

	private void Relay_In_0()
	{
		logic_uScript_CheckIfPlayerTippedOver_uScript_CheckIfPlayerTippedOver_0.In();
		bool tippedOver = logic_uScript_CheckIfPlayerTippedOver_uScript_CheckIfPlayerTippedOver_0.TippedOver;
		bool notTippedOver = logic_uScript_CheckIfPlayerTippedOver_uScript_CheckIfPlayerTippedOver_0.NotTippedOver;
		if (tippedOver)
		{
			Relay_In_20();
		}
		if (notTippedOver)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_2()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_2 = local_MsgTippedOverFallen_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_2.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_2, logic_uScript_RemoveOnScreenMessage_instant_2);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_2.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_5()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_5 = local_MsgTippedOverRecovered_Pad_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_5.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_5, logic_uScript_RemoveOnScreenMessage_instant_5);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_5.Out)
		{
			Relay_False_13();
		}
	}

	private void Relay_True_7()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.True(out logic_uScriptAct_SetBool_Target_7);
		local_FallenOver_System_Boolean = logic_uScriptAct_SetBool_Target_7;
	}

	private void Relay_False_7()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.False(out logic_uScriptAct_SetBool_Target_7);
		local_FallenOver_System_Boolean = logic_uScriptAct_SetBool_Target_7;
	}

	private void Relay_In_8()
	{
		logic_uScriptCon_CompareBool_Bool_8 = local_FallenOver_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.In(logic_uScriptCon_CompareBool_Bool_8);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.True)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_11.In();
		bool num = logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_11.True;
		bool flag = logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_11.False;
		if (num)
		{
			Relay_In_8();
		}
		if (flag)
		{
			Relay_In_30();
		}
	}

	private void Relay_True_13()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.True(out logic_uScriptAct_SetBool_Target_13);
		local_FallenOver_System_Boolean = logic_uScriptAct_SetBool_Target_13;
	}

	private void Relay_False_13()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.False(out logic_uScriptAct_SetBool_Target_13);
		local_FallenOver_System_Boolean = logic_uScriptAct_SetBool_Target_13;
	}

	private void Relay_True_16()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_16.True(out logic_uScriptAct_SetBool_Target_16);
		local_FallenOver_System_Boolean = logic_uScriptAct_SetBool_Target_16;
	}

	private void Relay_False_16()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_16.False(out logic_uScriptAct_SetBool_Target_16);
		local_FallenOver_System_Boolean = logic_uScriptAct_SetBool_Target_16;
	}

	private void Relay_In_17()
	{
		logic_uScript_GetPlayerTank_Return_17 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_17.In();
		bool returned = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_17.Returned;
		bool notReturned = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_17.NotReturned;
		if (returned)
		{
			Relay_In_0();
		}
		if (notReturned)
		{
			Relay_False_16();
		}
	}

	private void Relay_Connection_18()
	{
	}

	private void Relay_Connection_19()
	{
	}

	private void Relay_Out_20()
	{
		Relay_True_7();
	}

	private void Relay_Shown_20()
	{
	}

	private void Relay_In_20()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_20 = external_19;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_20 = external_21;
		logic_SubGraph_AddMessageWithPadSupport_speaker_20 = external_31;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_20.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_20, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_20, logic_SubGraph_AddMessageWithPadSupport_speaker_20);
	}

	private void Relay_Connection_21()
	{
	}

	private void Relay_Out_22()
	{
	}

	private void Relay_Shown_22()
	{
	}

	private void Relay_In_22()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_22 = external_23;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_22 = external_24;
		logic_SubGraph_AddMessageWithPadSupport_speaker_22 = external_31;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_22.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_22, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_22, logic_SubGraph_AddMessageWithPadSupport_speaker_22);
	}

	private void Relay_Connection_23()
	{
	}

	private void Relay_Connection_24()
	{
	}

	private void Relay_In_27()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_27 = local_MsgTippedOverFallen_Pad_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_27.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_27, logic_uScript_RemoveOnScreenMessage_instant_27);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_27.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_30()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_30 = local_MsgTippedOverRecovered_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_30.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_30, logic_uScript_RemoveOnScreenMessage_instant_30);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_30.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_Connection_31()
	{
	}
}
