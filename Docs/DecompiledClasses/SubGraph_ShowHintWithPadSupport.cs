using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_ShowHintWithPadSupport", "")]
public class SubGraph_ShowHintWithPadSupport : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private UIHintFloating.HintFloatTypes external_5;

	private UIHintFloating.HintFloatTypes external_6;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_0 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_0 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_0 = true;

	private uScript_ShowHintFloating logic_uScript_ShowHintFloating_uScript_ShowHintFloating_3 = new uScript_ShowHintFloating();

	private UIHintFloating.HintFloatTypes logic_uScript_ShowHintFloating_hintAnimation_3;

	private bool logic_uScript_ShowHintFloating_Out_3 = true;

	private uScript_ShowHintFloating logic_uScript_ShowHintFloating_uScript_ShowHintFloating_4 = new uScript_ShowHintFloating();

	private UIHintFloating.HintFloatTypes logic_uScript_ShowHintFloating_hintAnimation_4;

	private bool logic_uScript_ShowHintFloating_Out_4 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

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
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_0.SetParent(g);
		logic_uScript_ShowHintFloating_uScript_ShowHintFloating_3.SetParent(g);
		logic_uScript_ShowHintFloating_uScript_ShowHintFloating_4.SetParent(g);
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
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_0.OnDisable();
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
	public void In([FriendlyName("hintControlPad", "")] UIHintFloating.HintFloatTypes hintControlPad, [FriendlyName("hintMouseKeyboard", "")] UIHintFloating.HintFloatTypes hintMouseKeyboard)
	{
		external_5 = hintControlPad;
		external_6 = hintMouseKeyboard;
		Relay_In_0();
	}

	private void Relay_In_0()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_0.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_0.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_0.MouseAndKeyboard;
		if (joypad)
		{
			Relay_In_3();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_4();
		}
	}

	private void Relay_Connection_1()
	{
	}

	private void Relay_Connection_2()
	{
		if (this.Out != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Out(this, args);
		}
	}

	private void Relay_In_3()
	{
		logic_uScript_ShowHintFloating_hintAnimation_3 = external_5;
		logic_uScript_ShowHintFloating_uScript_ShowHintFloating_3.In(logic_uScript_ShowHintFloating_hintAnimation_3);
		if (logic_uScript_ShowHintFloating_uScript_ShowHintFloating_3.Out)
		{
			Relay_Connection_2();
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_ShowHintFloating_hintAnimation_4 = external_6;
		logic_uScript_ShowHintFloating_uScript_ShowHintFloating_4.In(logic_uScript_ShowHintFloating_hintAnimation_4);
		if (logic_uScript_ShowHintFloating_uScript_ShowHintFloating_4.Out)
		{
			Relay_Connection_2();
		}
	}

	private void Relay_Connection_5()
	{
	}

	private void Relay_Connection_6()
	{
	}
}
