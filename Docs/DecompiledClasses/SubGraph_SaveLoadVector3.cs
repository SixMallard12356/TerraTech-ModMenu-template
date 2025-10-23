using System;
using UnityEngine;

[Serializable]
[FriendlyName("", "")]
[NodePath("Graphs")]
public class SubGraph_SaveLoadVector3 : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public Vector3 vector;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private Vector3 external_14;

	private Vector3 external_12;

	private object external_13 = "";

	private string external_9 = "";

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_7;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_4 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_4 = "";

	private GameObject logic_uScript_SaveVariable_owner_4;

	private string logic_uScript_SaveVariable_uniqueId_4 = "";

	private bool logic_uScript_SaveVariable_Out_4 = true;

	private uScript_LoadVector3 logic_uScript_LoadVector3_uScript_LoadVector3_15 = new uScript_LoadVector3();

	private Vector3 logic_uScript_LoadVector3_data_15;

	private GameObject logic_uScript_LoadVector3_owner_15;

	private string logic_uScript_LoadVector3_uniqueName_15 = "";

	private bool logic_uScript_LoadVector3_Out_15 = true;

	private uScriptAct_SetVector3 logic_uScriptAct_SetVector3_uScriptAct_SetVector3_16 = new uScriptAct_SetVector3();

	private Vector3 logic_uScriptAct_SetVector3_Value_16;

	private Vector3 logic_uScriptAct_SetVector3_TargetVector3_16;

	private bool logic_uScriptAct_SetVector3_Out_16 = true;

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
		logic_uScript_LoadVector3_uScript_LoadVector3_15.SetParent(g);
		logic_uScriptAct_SetVector3_uScriptAct_SetVector3_16.SetParent(g);
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
		logic_uScript_LoadVector3_uScript_LoadVector3_15.OnDisable();
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
	public void Save([FriendlyName("restartValue", "")] Vector3 restartValue, [FriendlyName("vector", "")] ref Vector3 vector, [FriendlyName("vectorAsVariable", "")] object vectorAsVariable, [FriendlyName("uniqueID", "")] string uniqueID)
	{
		external_14 = restartValue;
		external_12 = vector;
		external_13 = vectorAsVariable;
		external_9 = uniqueID;
		Relay_In_4();
	}

	[FriendlyName("Load", "")]
	public void Load([FriendlyName("restartValue", "")] Vector3 restartValue, [FriendlyName("vector", "")] ref Vector3 vector, [FriendlyName("vectorAsVariable", "")] object vectorAsVariable, [FriendlyName("uniqueID", "")] string uniqueID)
	{
		external_14 = restartValue;
		external_12 = vector;
		external_13 = vectorAsVariable;
		external_9 = uniqueID;
		Relay_In_15();
	}

	[FriendlyName("Restart", "")]
	public void Restart([FriendlyName("restartValue", "")] Vector3 restartValue, [FriendlyName("vector", "")] ref Vector3 vector, [FriendlyName("vectorAsVariable", "")] object vectorAsVariable, [FriendlyName("uniqueID", "")] string uniqueID)
	{
		external_14 = restartValue;
		external_12 = vector;
		external_13 = vectorAsVariable;
		external_9 = uniqueID;
		Relay_In_16();
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
			e.vector = external_12;
			this.Restart_Out(this, e);
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_SaveVariable_variable_4 = external_13;
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
			e.vector = external_12;
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
			e.vector = external_12;
			this.Load_Out(this, e);
		}
	}

	private void Relay_Connection_9()
	{
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

	private void Relay_In_15()
	{
		logic_uScript_LoadVector3_data_15 = external_12;
		logic_uScript_LoadVector3_owner_15 = owner_Connection_7;
		logic_uScript_LoadVector3_uniqueName_15 = external_9;
		logic_uScript_LoadVector3_uScript_LoadVector3_15.In(ref logic_uScript_LoadVector3_data_15, logic_uScript_LoadVector3_owner_15, logic_uScript_LoadVector3_uniqueName_15);
		external_12 = logic_uScript_LoadVector3_data_15;
		if (logic_uScript_LoadVector3_uScript_LoadVector3_15.Out)
		{
			Relay_Connection_8();
		}
	}

	private void Relay_In_16()
	{
		logic_uScriptAct_SetVector3_Value_16 = external_14;
		logic_uScriptAct_SetVector3_uScriptAct_SetVector3_16.In(logic_uScriptAct_SetVector3_Value_16, out logic_uScriptAct_SetVector3_TargetVector3_16);
		external_12 = logic_uScriptAct_SetVector3_TargetVector3_16;
		if (logic_uScriptAct_SetVector3_uScriptAct_SetVector3_16.Out)
		{
			Relay_Connection_3();
		}
	}
}
