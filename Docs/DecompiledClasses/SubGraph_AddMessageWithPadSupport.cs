using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_AddMessageWithPadSupport", "")]
public class SubGraph_AddMessageWithPadSupport : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public ManOnScreenMessages.OnScreenMessage messageMouseKeyboardReturn;

		public ManOnScreenMessages.OnScreenMessage messageControlPadReturn;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private uScript_AddMessage.MessageData external_4;

	private uScript_AddMessage.MessageData external_5;

	private uScript_AddMessage.MessageSpeaker external_6;

	private ManOnScreenMessages.OnScreenMessage external_10;

	private ManOnScreenMessages.OnScreenMessage external_11;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_0 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_0;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_0;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_0;

	private bool logic_uScript_AddMessage_Out_0 = true;

	private bool logic_uScript_AddMessage_Shown_0 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_1 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_1;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_1;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_1;

	private bool logic_uScript_AddMessage_Out_1 = true;

	private bool logic_uScript_AddMessage_Shown_1 = true;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_2 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_2 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_2 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	[FriendlyName("Shown")]
	public event uScriptEventHandler Shown;

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
		logic_uScript_AddMessage_uScript_AddMessage_0.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_1.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_2.SetParent(g);
	}

	public void Awake()
	{
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
	}

	public void OnDisable()
	{
		logic_uScript_AddMessage_uScript_AddMessage_0.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_1.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_2.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
	}

	public void OnDestroy()
	{
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("messageMouseKeyboard", "")] uScript_AddMessage.MessageData messageMouseKeyboard, [FriendlyName("messageControlPad", "")] uScript_AddMessage.MessageData messageControlPad, [FriendlyName("speaker", "")] uScript_AddMessage.MessageSpeaker speaker)
	{
		external_4 = messageMouseKeyboard;
		external_5 = messageControlPad;
		external_6 = speaker;
		Relay_In_2();
	}

	private void Relay_In_0()
	{
		logic_uScript_AddMessage_messageData_0 = external_5;
		logic_uScript_AddMessage_speaker_0 = external_6;
		logic_uScript_AddMessage_Return_0 = logic_uScript_AddMessage_uScript_AddMessage_0.In(logic_uScript_AddMessage_messageData_0, logic_uScript_AddMessage_speaker_0);
		external_11 = logic_uScript_AddMessage_Return_0;
		bool num = logic_uScript_AddMessage_uScript_AddMessage_0.Out;
		bool shown = logic_uScript_AddMessage_uScript_AddMessage_0.Shown;
		if (num)
		{
			Relay_Connection_8();
		}
		if (shown)
		{
			Relay_Connection_9();
		}
	}

	private void Relay_In_1()
	{
		logic_uScript_AddMessage_messageData_1 = external_4;
		logic_uScript_AddMessage_speaker_1 = external_6;
		logic_uScript_AddMessage_Return_1 = logic_uScript_AddMessage_uScript_AddMessage_1.In(logic_uScript_AddMessage_messageData_1, logic_uScript_AddMessage_speaker_1);
		external_10 = logic_uScript_AddMessage_Return_1;
		bool num = logic_uScript_AddMessage_uScript_AddMessage_1.Out;
		bool shown = logic_uScript_AddMessage_uScript_AddMessage_1.Shown;
		if (num)
		{
			Relay_Connection_8();
		}
		if (shown)
		{
			Relay_Connection_9();
		}
	}

	private void Relay_In_2()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_2.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_2.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_2.MouseAndKeyboard;
		if (joypad)
		{
			Relay_In_0();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_1();
		}
	}

	private void Relay_Connection_3()
	{
	}

	private void Relay_Connection_4()
	{
	}

	private void Relay_Connection_5()
	{
	}

	private void Relay_Connection_6()
	{
	}

	private void Relay_Connection_8()
	{
		if (this.Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.messageMouseKeyboardReturn = external_10;
			e.messageControlPadReturn = external_11;
			this.Out(this, e);
		}
	}

	private void Relay_Connection_9()
	{
		if (this.Shown != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.messageMouseKeyboardReturn = external_10;
			e.messageControlPadReturn = external_11;
			this.Shown(this, e);
		}
	}

	private void Relay_Connection_10()
	{
	}

	private void Relay_Connection_11()
	{
	}
}
