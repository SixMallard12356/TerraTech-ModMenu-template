using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_SpawnEnemyWave", "")]
[NodePath("Graphs")]
public class SubGraph_SpawnEnemyWave : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_5 = new SpawnTechData[0];

	private float external_4;

	private bool external_8;

	private float external_6;

	private bool local_TechsSpawned_System_Boolean;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_15;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_0 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_0;

	private bool logic_uScript_Wait_repeat_0;

	private bool logic_uScript_Wait_Waited_0 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_1 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_1;

	private bool logic_uScript_Wait_repeat_1;

	private bool logic_uScript_Wait_Waited_1 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_7;

	private bool logic_uScriptCon_CompareBool_True_7 = true;

	private bool logic_uScriptCon_CompareBool_False_7 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_10 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_10 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_10;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_10 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_10 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_11 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_11;

	private bool logic_uScriptAct_SetBool_Out_11 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_11 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_11 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_13;

	private bool logic_uScriptCon_CompareBool_True_13 = true;

	private bool logic_uScriptCon_CompareBool_False_13 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_16 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_16;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_16 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_16;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_16 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_16 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_16 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_16 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
		}
		if (null == owner_Connection_15 || !m_RegisteredForEvents)
		{
			owner_Connection_15 = parentGameObject;
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
		logic_uScript_Wait_uScript_Wait_0.SetParent(g);
		logic_uScript_Wait_uScript_Wait_1.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_10.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_11.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.SetParent(g);
		owner_Connection_12 = parentGameObject;
		owner_Connection_15 = parentGameObject;
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
		logic_uScript_Wait_uScript_Wait_0.OnDisable();
		logic_uScript_Wait_uScript_Wait_1.OnDisable();
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
	public void In([FriendlyName("techsInWave", "")] SpawnTechData[] techsInWave, [FriendlyName("timeBeforeNextWave", "")] float timeBeforeNextWave, [FriendlyName("autoTriggerNextWave?", "")] bool autoTriggerNextWave_, [FriendlyName("timeBeforeAutoTrigger", "")] float timeBeforeAutoTrigger)
	{
		external_5 = techsInWave;
		external_4 = timeBeforeNextWave;
		external_8 = autoTriggerNextWave_;
		external_6 = timeBeforeAutoTrigger;
		Relay_In_13();
	}

	private void Relay_In_0()
	{
		logic_uScript_Wait_seconds_0 = external_6;
		logic_uScript_Wait_uScript_Wait_0.In(logic_uScript_Wait_seconds_0, logic_uScript_Wait_repeat_0);
		if (logic_uScript_Wait_uScript_Wait_0.Waited)
		{
			Relay_Connection_3();
		}
	}

	private void Relay_In_1()
	{
		logic_uScript_Wait_seconds_1 = external_4;
		logic_uScript_Wait_uScript_Wait_1.In(logic_uScript_Wait_seconds_1, logic_uScript_Wait_repeat_1);
		if (logic_uScript_Wait_uScript_Wait_1.Waited)
		{
			Relay_Connection_3();
		}
	}

	private void Relay_Connection_2()
	{
	}

	private void Relay_Connection_3()
	{
		if (this.Out != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Out(this, args);
		}
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

	private void Relay_In_7()
	{
		logic_uScriptCon_CompareBool_Bool_7 = external_8;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.In(logic_uScriptCon_CompareBool_Bool_7);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.True)
		{
			Relay_In_0();
		}
	}

	private void Relay_Connection_8()
	{
	}

	private void Relay_InitialSpawn_10()
	{
		int num = 0;
		Array array = external_5;
		if (logic_uScript_SpawnTechsFromData_spawnData_10.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_10, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_10, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_10 = owner_Connection_15;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_10.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_10, logic_uScript_SpawnTechsFromData_ownerNode_10, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_10);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_10.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_True_11()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_11.True(out logic_uScriptAct_SetBool_Target_11);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_11;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_11.Out)
		{
			Relay_InitialSpawn_10();
		}
	}

	private void Relay_False_11()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_11.False(out logic_uScriptAct_SetBool_Target_11);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_11;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_11.Out)
		{
			Relay_InitialSpawn_10();
		}
	}

	private void Relay_In_13()
	{
		logic_uScriptCon_CompareBool_Bool_13 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.In(logic_uScriptCon_CompareBool_Bool_13);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.False;
		if (num)
		{
			Relay_In_16();
		}
		if (flag)
		{
			Relay_True_11();
		}
	}

	private void Relay_In_16()
	{
		int num = 0;
		Array array = external_5;
		if (logic_uScript_GetAndCheckTechs_techData_16.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_16, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_16, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_16 = owner_Connection_12;
		logic_uScript_GetAndCheckTechs_Return_16 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.In(logic_uScript_GetAndCheckTechs_techData_16, logic_uScript_GetAndCheckTechs_ownerNode_16, ref logic_uScript_GetAndCheckTechs_techs_16);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.AllDead;
		if (allAlive)
		{
			Relay_In_7();
		}
		if (someAlive)
		{
			Relay_In_7();
		}
		if (allDead)
		{
			Relay_In_1();
		}
	}
}
