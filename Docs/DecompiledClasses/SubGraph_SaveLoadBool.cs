using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("", "")]
public class SubGraph_SaveLoadBool : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public bool boolean;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private bool external_12;

	private object external_13 = "";

	private string external_14 = "";

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_10;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_1 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_1;

	private GameObject logic_uScript_LoadBool_owner_1;

	private string logic_uScript_LoadBool_uniqueName_1 = "";

	private bool logic_uScript_LoadBool_Out_1 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_5 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_5 = "";

	private GameObject logic_uScript_SaveVariable_owner_5;

	private string logic_uScript_SaveVariable_uniqueId_5 = "";

	private bool logic_uScript_SaveVariable_Out_5 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_9 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_9;

	private bool logic_uScriptAct_SetBool_Out_9 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_9 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_9 = true;

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
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
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
		logic_uScript_LoadBool_uScript_LoadBool_1.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_5.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_9.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_10 = parentGameObject;
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
		logic_uScript_LoadBool_uScript_LoadBool_1.OnDisable();
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
	public void Save([FriendlyName("boolean", "")] ref bool boolean, [FriendlyName("boolAsVariable", "")] object boolAsVariable, [FriendlyName("uniqueID", "")] string uniqueID)
	{
		external_12 = boolean;
		external_13 = boolAsVariable;
		external_14 = uniqueID;
		Relay_In_5();
	}

	[FriendlyName("Load", "")]
	public void Load([FriendlyName("boolean", "")] ref bool boolean, [FriendlyName("boolAsVariable", "")] object boolAsVariable, [FriendlyName("uniqueID", "")] string uniqueID)
	{
		external_12 = boolean;
		external_13 = boolAsVariable;
		external_14 = uniqueID;
		Relay_In_1();
	}

	[FriendlyName("Set True", "")]
	public void Set_True([FriendlyName("boolean", "")] ref bool boolean, [FriendlyName("boolAsVariable", "")] object boolAsVariable, [FriendlyName("uniqueID", "")] string uniqueID)
	{
		external_12 = boolean;
		external_13 = boolAsVariable;
		external_14 = uniqueID;
		Relay_True_9();
	}

	[FriendlyName("Set False", "")]
	public void Set_False([FriendlyName("boolean", "")] ref bool boolean, [FriendlyName("boolAsVariable", "")] object boolAsVariable, [FriendlyName("uniqueID", "")] string uniqueID)
	{
		external_12 = boolean;
		external_13 = boolAsVariable;
		external_14 = uniqueID;
		Relay_False_9();
	}

	private void Relay_In_1()
	{
		logic_uScript_LoadBool_data_1 = external_12;
		logic_uScript_LoadBool_owner_1 = owner_Connection_10;
		logic_uScript_LoadBool_uniqueName_1 = external_14;
		logic_uScript_LoadBool_uScript_LoadBool_1.In(ref logic_uScript_LoadBool_data_1, logic_uScript_LoadBool_owner_1, logic_uScript_LoadBool_uniqueName_1);
		external_12 = logic_uScript_LoadBool_data_1;
		if (logic_uScript_LoadBool_uScript_LoadBool_1.Out)
		{
			Relay_Connection_11();
		}
	}

	private void Relay_Connection_2()
	{
	}

	private void Relay_Connection_3()
	{
	}

	private void Relay_Connection_4()
	{
		if (this.Restart_Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.boolean = external_12;
			this.Restart_Out(this, e);
		}
	}

	private void Relay_In_5()
	{
		logic_uScript_SaveVariable_variable_5 = external_13;
		logic_uScript_SaveVariable_owner_5 = owner_Connection_0;
		logic_uScript_SaveVariable_uniqueId_5 = external_14;
		logic_uScript_SaveVariable_uScript_SaveVariable_5.In(logic_uScript_SaveVariable_variable_5, logic_uScript_SaveVariable_owner_5, logic_uScript_SaveVariable_uniqueId_5);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_5.Out)
		{
			Relay_Connection_6();
		}
	}

	private void Relay_Connection_6()
	{
		if (this.Save_Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.boolean = external_12;
			this.Save_Out(this, e);
		}
	}

	private void Relay_Connection_7()
	{
	}

	private void Relay_Connection_8()
	{
	}

	private void Relay_True_9()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_9.True(out logic_uScriptAct_SetBool_Target_9);
		external_12 = logic_uScriptAct_SetBool_Target_9;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_9.Out)
		{
			Relay_Connection_4();
		}
	}

	private void Relay_False_9()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_9.False(out logic_uScriptAct_SetBool_Target_9);
		external_12 = logic_uScriptAct_SetBool_Target_9;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_9.Out)
		{
			Relay_Connection_4();
		}
	}

	private void Relay_Connection_11()
	{
		if (this.Load_Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.boolean = external_12;
			this.Load_Out(this, e);
		}
	}

	private void Relay_Connection_12()
	{
	}

	private void Relay_Connection_13()
	{
	}

	private void Relay_Connection_14()
	{
	}
}
