using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("", "")]
public class SubGraph_SaveLoadInt : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public int integer;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private int external_16;

	private int external_13;

	private object external_15 = "";

	private string external_9 = "";

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_7;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_4 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_4 = "";

	private GameObject logic_uScript_SaveVariable_owner_4;

	private string logic_uScript_SaveVariable_uniqueId_4 = "";

	private bool logic_uScript_SaveVariable_Out_4 = true;

	private uScript_LoadInt logic_uScript_LoadInt_uScript_LoadInt_12 = new uScript_LoadInt();

	private int logic_uScript_LoadInt_data_12;

	private GameObject logic_uScript_LoadInt_owner_12;

	private string logic_uScript_LoadInt_uniqueName_12 = "";

	private bool logic_uScript_LoadInt_Out_12 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_14 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_14;

	private int logic_uScriptAct_SetInt_Target_14;

	private bool logic_uScriptAct_SetInt_Out_14 = true;

	[FriendlyName("Save Out")]
	public event uScriptEventHandler Save_Out;

	[FriendlyName("Load Out")]
	public event uScriptEventHandler Load_Out;

	[FriendlyName("Restart Out")]
	public event uScriptEventHandler Restart_Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
		}
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
		}
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
		logic_uScript_SaveVariable_uScript_SaveVariable_4.SetParent(g);
		logic_uScript_LoadInt_uScript_LoadInt_12.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_14.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_7 = parentGameObject;
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
		logic_uScript_LoadInt_uScript_LoadInt_12.OnDisable();
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

	[FriendlyName("Save", "")]
	public void Save([FriendlyName("restartValue", "")] int restartValue, [FriendlyName("integer", "")] ref int integer, [FriendlyName("intAsVariable", "")] object intAsVariable, [FriendlyName("uniqueID", "")] string uniqueID)
	{
		external_16 = restartValue;
		external_13 = integer;
		external_15 = intAsVariable;
		external_9 = uniqueID;
		Relay_In_4();
	}

	[FriendlyName("Load", "")]
	public void Load([FriendlyName("restartValue", "")] int restartValue, [FriendlyName("integer", "")] ref int integer, [FriendlyName("intAsVariable", "")] object intAsVariable, [FriendlyName("uniqueID", "")] string uniqueID)
	{
		external_16 = restartValue;
		external_13 = integer;
		external_15 = intAsVariable;
		external_9 = uniqueID;
		Relay_In_12();
	}

	[FriendlyName("Restart", "")]
	public void Restart([FriendlyName("restartValue", "")] int restartValue, [FriendlyName("integer", "")] ref int integer, [FriendlyName("intAsVariable", "")] object intAsVariable, [FriendlyName("uniqueID", "")] string uniqueID)
	{
		external_16 = restartValue;
		external_13 = integer;
		external_15 = intAsVariable;
		external_9 = uniqueID;
		Relay_In_14();
	}

	private void Relay_Connection_1()
	{
	}

	private void Relay_Connection_2()
	{
	}

	private void Relay_Connection_3()
	{
		if (this.Restart_Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.integer = external_13;
			this.Restart_Out(this, e);
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_SaveVariable_variable_4 = external_15;
		logic_uScript_SaveVariable_owner_4 = owner_Connection_0;
		logic_uScript_SaveVariable_uniqueId_4 = external_9;
		logic_uScript_SaveVariable_uScript_SaveVariable_4.In(logic_uScript_SaveVariable_variable_4, logic_uScript_SaveVariable_owner_4, logic_uScript_SaveVariable_uniqueId_4);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_4.Out)
		{
			Relay_Connection_5();
		}
	}

	private void Relay_Connection_5()
	{
		if (this.Save_Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.integer = external_13;
			this.Save_Out(this, e);
		}
	}

	private void Relay_Connection_6()
	{
	}

	private void Relay_Connection_8()
	{
		if (this.Load_Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.integer = external_13;
			this.Load_Out(this, e);
		}
	}

	private void Relay_Connection_9()
	{
	}

	private void Relay_In_12()
	{
		logic_uScript_LoadInt_data_12 = external_13;
		logic_uScript_LoadInt_owner_12 = owner_Connection_7;
		logic_uScript_LoadInt_uniqueName_12 = external_9;
		logic_uScript_LoadInt_uScript_LoadInt_12.In(ref logic_uScript_LoadInt_data_12, logic_uScript_LoadInt_owner_12, logic_uScript_LoadInt_uniqueName_12);
		external_13 = logic_uScript_LoadInt_data_12;
		if (logic_uScript_LoadInt_uScript_LoadInt_12.Out)
		{
			Relay_Connection_8();
		}
	}

	private void Relay_Connection_13()
	{
	}

	private void Relay_In_14()
	{
		logic_uScriptAct_SetInt_Value_14 = external_16;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_14.In(logic_uScriptAct_SetInt_Value_14, out logic_uScriptAct_SetInt_Target_14);
		external_13 = logic_uScriptAct_SetInt_Target_14;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_14.Out)
		{
			Relay_Connection_3();
		}
	}

	private void Relay_Connection_15()
	{
	}

	private void Relay_Connection_16()
	{
	}
}
