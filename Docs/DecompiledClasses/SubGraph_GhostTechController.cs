using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_GhostTechController", "")]
public class SubGraph_GhostTechController : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private string external_11 = "";

	private string external_6 = "";

	private string external_30 = "";

	private SpawnTechData[] external_9 = new SpawnTechData[0];

	private bool external_37;

	private bool local_EnteredPreTrigger_System_Boolean;

	private Tank local_ghostTech_Tank;

	private Tank[] local_ghostTechs_TankArray = new Tank[0];

	private GameObject owner_Connection_4;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_21;

	private GameObject owner_Connection_43;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_0 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_0;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_0 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_0;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_0 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_0 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_0 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_0 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_2 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_2;

	private bool logic_uScript_SetTechMarkerState_Out_2 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_5 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_5 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_5;

	private bool logic_uScript_SetTankInvulnerable_Out_5 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_8 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_8 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_8 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_8;

	private bool logic_uScript_DestroyTechsFromData_Out_8 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_12 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_12;

	private bool logic_uScriptAct_SetBool_Out_12 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_12 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_12 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_15 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_15;

	private bool logic_uScriptAct_SetBool_Out_15 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_15 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_15 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_18 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_18;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_18;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_18 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_18 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_19;

	private bool logic_uScriptCon_CompareBool_True_19 = true;

	private bool logic_uScriptCon_CompareBool_False_19 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_20;

	private bool logic_uScriptCon_CompareBool_True_20 = true;

	private bool logic_uScriptCon_CompareBool_False_20 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_23 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_23 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_23 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_23 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_23 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_23 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_23 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_24 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_24 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_24 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_24 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_24 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_24 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_24 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_25 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_25;

	private Tank logic_uScript_SetTankInvulnerable_tank_25;

	private bool logic_uScript_SetTankInvulnerable_Out_25 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_26 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_26 = new Tank[0];

	private int logic_uScript_AccessListTech_index_26;

	private Tank logic_uScript_AccessListTech_value_26;

	private bool logic_uScript_AccessListTech_Out_26 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_29 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_29 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_29 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_29 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_29 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_29 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_31 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_32 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_32 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_33;

	private bool logic_uScriptCon_CompareBool_True_33 = true;

	private bool logic_uScriptCon_CompareBool_False_33 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_34 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_35 = true;

	private uScript_SetTechMaterialOverride logic_uScript_SetTechMaterialOverride_uScript_SetTechMaterialOverride_39 = new uScript_SetTechMaterialOverride();

	private Tank logic_uScript_SetTechMaterialOverride_tech_39;

	private bool logic_uScript_SetTechMaterialOverride_enable_39 = true;

	private ManTechMaterialSwap.MatType logic_uScript_SetTechMaterialOverride_customMaterialType_39 = ManTechMaterialSwap.MatType.Halloween;

	private bool logic_uScript_SetTechMaterialOverride_Out_39 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_40 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_41 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_42 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_42 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_42;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_42 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_42;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_42 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_42 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_42 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_42 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_4 || !m_RegisteredForEvents)
		{
			owner_Connection_4 = parentGameObject;
		}
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
		}
		if (null == owner_Connection_21 || !m_RegisteredForEvents)
		{
			owner_Connection_21 = parentGameObject;
		}
		if (null == owner_Connection_43 || !m_RegisteredForEvents)
		{
			owner_Connection_43 = parentGameObject;
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
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_2.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_5.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_8.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_12.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_15.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_23.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_24.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_25.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_26.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_32.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.SetParent(g);
		logic_uScript_SetTechMaterialOverride_uScript_SetTechMaterialOverride_39.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_42.SetParent(g);
		owner_Connection_4 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_21 = parentGameObject;
		owner_Connection_43 = parentGameObject;
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
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_2.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_5.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_25.OnDisable();
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
	public void In([FriendlyName("preSpawnTrigger", "")] string preSpawnTrigger, [FriendlyName("ghostSpawnTrigger", "")] string ghostSpawnTrigger, [FriendlyName("ghostKillTrigger", "")] string ghostKillTrigger, [FriendlyName("ghostTechSpawnData", "")] SpawnTechData[] ghostTechSpawnData, [FriendlyName("UsePreSpawnTrigger", "")] bool UsePreSpawnTrigger)
	{
		external_11 = preSpawnTrigger;
		external_6 = ghostSpawnTrigger;
		external_30 = ghostKillTrigger;
		external_9 = ghostTechSpawnData;
		external_37 = UsePreSpawnTrigger;
		Relay_In_33();
	}

	private void Relay_In_0()
	{
		int num = 0;
		Array array = external_9;
		if (logic_uScript_GetAndCheckTechs_techData_0.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_0, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_0, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_0 = owner_Connection_4;
		int num2 = 0;
		Array array2 = local_ghostTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_0.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_0, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_0, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_0 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.In(logic_uScript_GetAndCheckTechs_techData_0, logic_uScript_GetAndCheckTechs_ownerNode_0, ref logic_uScript_GetAndCheckTechs_techs_0);
		local_ghostTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_0;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_26();
		}
		if (someAlive)
		{
			Relay_AtIndex_26();
		}
		if (allDead)
		{
			Relay_In_31();
		}
		if (waitingToSpawn)
		{
			Relay_In_31();
		}
	}

	private void Relay_Show_2()
	{
		logic_uScript_SetTechMarkerState_tech_2 = local_ghostTech_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_2.Show(logic_uScript_SetTechMarkerState_tech_2);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_2.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_Hide_2()
	{
		logic_uScript_SetTechMarkerState_tech_2 = local_ghostTech_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_2.Hide(logic_uScript_SetTechMarkerState_tech_2);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_2.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_Connection_3()
	{
		if (this.Out != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Out(this, args);
		}
	}

	private void Relay_In_5()
	{
		logic_uScript_SetTankInvulnerable_tank_5 = local_ghostTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_5.In(logic_uScript_SetTankInvulnerable_invulnerable_5, logic_uScript_SetTankInvulnerable_tank_5);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_5.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_Connection_6()
	{
	}

	private void Relay_Connection_7()
	{
	}

	private void Relay_In_8()
	{
		int num = 0;
		Array array = external_9;
		if (logic_uScript_DestroyTechsFromData_techData_8.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_8, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_8, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_8 = owner_Connection_10;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_8.In(logic_uScript_DestroyTechsFromData_techData_8, logic_uScript_DestroyTechsFromData_shouldExplode_8, logic_uScript_DestroyTechsFromData_ownerNode_8);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_8.Out)
		{
			Relay_False_15();
		}
	}

	private void Relay_Connection_9()
	{
	}

	private void Relay_Connection_11()
	{
	}

	private void Relay_True_12()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_12.True(out logic_uScriptAct_SetBool_Target_12);
		local_EnteredPreTrigger_System_Boolean = logic_uScriptAct_SetBool_Target_12;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_12.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_False_12()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_12.False(out logic_uScriptAct_SetBool_Target_12);
		local_EnteredPreTrigger_System_Boolean = logic_uScriptAct_SetBool_Target_12;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_12.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_True_15()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_15.True(out logic_uScriptAct_SetBool_Target_15);
		local_EnteredPreTrigger_System_Boolean = logic_uScriptAct_SetBool_Target_15;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_15.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_False_15()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_15.False(out logic_uScriptAct_SetBool_Target_15);
		local_EnteredPreTrigger_System_Boolean = logic_uScriptAct_SetBool_Target_15;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_15.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_InitialSpawn_18()
	{
		int num = 0;
		Array array = external_9;
		if (logic_uScript_SpawnTechsFromData_spawnData_18.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_18, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_18, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_18 = owner_Connection_21;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_18, logic_uScript_SpawnTechsFromData_ownerNode_18, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_18, logic_uScript_SpawnTechsFromData_allowResurrection_18);
	}

	private void Relay_In_19()
	{
		logic_uScriptCon_CompareBool_Bool_19 = external_37;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.In(logic_uScriptCon_CompareBool_Bool_19);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.False;
		if (num)
		{
			Relay_In_20();
		}
		if (flag)
		{
			Relay_InitialSpawn_18();
		}
	}

	private void Relay_In_20()
	{
		logic_uScriptCon_CompareBool_Bool_20 = local_EnteredPreTrigger_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.In(logic_uScriptCon_CompareBool_Bool_20);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.True)
		{
			Relay_InitialSpawn_18();
		}
	}

	private void Relay_In_23()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_23 = external_11;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_23.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_23);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_23.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_23.InRange;
		if (num)
		{
			Relay_In_24();
		}
		if (inRange)
		{
			Relay_True_12();
		}
	}

	private void Relay_In_24()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_24 = external_6;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_24.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_24);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_24.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_24.InRange;
		if (num)
		{
			Relay_In_0();
		}
		if (inRange)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_25()
	{
		logic_uScript_SetTankInvulnerable_tank_25 = local_ghostTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_25.In(logic_uScript_SetTankInvulnerable_invulnerable_25, logic_uScript_SetTankInvulnerable_tank_25);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_25.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_AtIndex_26()
	{
		int num = 0;
		Array array = local_ghostTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_26.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_26, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_26, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_26.AtIndex(ref logic_uScript_AccessListTech_techList_26, logic_uScript_AccessListTech_index_26, out logic_uScript_AccessListTech_value_26);
		local_ghostTechs_TankArray = logic_uScript_AccessListTech_techList_26;
		local_ghostTech_Tank = logic_uScript_AccessListTech_value_26;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_26.Out)
		{
			Relay_Hide_2();
		}
	}

	private void Relay_In_29()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_29 = external_30;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_29);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29.OutOfRange;
		if (inRange)
		{
			Relay_In_32();
		}
		if (outOfRange)
		{
			Relay_In_25();
		}
	}

	private void Relay_Connection_30()
	{
	}

	private void Relay_In_31()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_32()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_32.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_32.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_33()
	{
		logic_uScriptCon_CompareBool_Bool_33 = external_37;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.In(logic_uScriptCon_CompareBool_Bool_33);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.False;
		if (num)
		{
			Relay_In_23();
		}
		if (flag)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_34()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_In_35()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_Connection_37()
	{
	}

	private void Relay_In_39()
	{
		logic_uScript_SetTechMaterialOverride_tech_39 = local_ghostTech_Tank;
		logic_uScript_SetTechMaterialOverride_uScript_SetTechMaterialOverride_39.In(logic_uScript_SetTechMaterialOverride_tech_39, logic_uScript_SetTechMaterialOverride_enable_39, logic_uScript_SetTechMaterialOverride_customMaterialType_39);
		if (logic_uScript_SetTechMaterialOverride_uScript_SetTechMaterialOverride_39.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_40()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.Out)
		{
			Relay_Connection_3();
		}
	}

	private void Relay_In_41()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_42()
	{
		int num = 0;
		Array array = external_9;
		if (logic_uScript_GetAndCheckTechs_techData_42.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_42, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_42, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_42 = owner_Connection_43;
		int num2 = 0;
		Array array2 = local_ghostTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_42.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_42, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_42, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_42 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_42.In(logic_uScript_GetAndCheckTechs_techData_42, logic_uScript_GetAndCheckTechs_ownerNode_42, ref logic_uScript_GetAndCheckTechs_techs_42);
		local_ghostTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_42;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_42.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_42.WaitingToSpawn;
		if (allDead)
		{
			Relay_In_19();
		}
		if (waitingToSpawn)
		{
			Relay_In_19();
		}
	}
}
