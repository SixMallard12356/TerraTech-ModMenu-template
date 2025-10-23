using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_RR_CombatLab_BossTurretController", "")]
[NodePath("Graphs")]
public class SubGraph_RR_CombatLab_BossTurretController : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public bool turretDestroyed;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_8 = new SpawnTechData[0];

	private SpawnTechData[] external_13 = new SpawnTechData[0];

	private BlockTypes external_15;

	private bool external_28;

	private Tank local_BossTurretTech_Tank;

	private Tank[] local_ChargerTechData_TankArray = new Tank[0];

	private Tank local_CurrentCharger_Tank;

	private int local_CurrentChargerIndex_System_Int32;

	private Tank[] local_Turrets_TankArray = new Tank[0];

	private TankBlock local_TurretShieldBlock_TankBlock;

	private bool local_TurretShieldsEnabled_System_Boolean;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_11;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1;

	private bool logic_uScriptAct_SetBool_Out_1 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_2 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_2 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_5 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_5 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_5;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_5 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_5;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_5 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_5 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_5 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_5 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_10 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_10 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_12 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_12 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_12;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_12 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_12;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_12 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_12 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_12 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_12 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_17 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_17;

	private BlockTypes logic_uScript_GetTankBlock_blockType_17;

	private TankBlock logic_uScript_GetTankBlock_Return_17;

	private bool logic_uScript_GetTankBlock_Out_17 = true;

	private bool logic_uScript_GetTankBlock_Returned_17 = true;

	private bool logic_uScript_GetTankBlock_NotFound_17 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_20;

	private bool logic_uScriptCon_CompareBool_True_20 = true;

	private bool logic_uScriptCon_CompareBool_False_20 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_21 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_21;

	private bool logic_uScript_SetTechMarkerState_Out_21 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_22 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_22 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_23 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_23 = "";

	private bool logic_uScript_SetShieldEnabled_enable_23;

	private bool logic_uScript_SetShieldEnabled_Out_23 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_25 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_25;

	private bool logic_uScriptAct_SetBool_Out_25 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_25 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_25 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_26 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_26 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_27 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_27;

	private bool logic_uScriptAct_SetBool_Out_27 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_27 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_27 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_29 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_30 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_30 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_31 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_36;

	private bool logic_uScriptCon_CompareBool_True_36 = true;

	private bool logic_uScriptCon_CompareBool_False_36 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_37 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_37 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_37 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_38 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_38 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_38;

	private int logic_uScript_ForEachListTech_currentIndex_38;

	private bool logic_uScript_ForEachListTech_Immediate_38 = true;

	private bool logic_uScript_ForEachListTech_Done_38 = true;

	private bool logic_uScript_ForEachListTech_Iteration_38 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_39 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_39;

	private bool logic_uScript_SetTechMarkerState_Out_39 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_40 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_41 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_43 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_43 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_48 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_49 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_49;

	private bool logic_uScript_IsTechAlive_Alive_49 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_49 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_50 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_50 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_52 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_52 = new Tank[0];

	private int logic_uScript_AccessListTech_index_52;

	private Tank logic_uScript_AccessListTech_value_52;

	private bool logic_uScript_AccessListTech_Out_52 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_57 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_57;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_57 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_57 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_59 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_59 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
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
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_2.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_5.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_10.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_12.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_17.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_21.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_22.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_23.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_25.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_26.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_30.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_37.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_38.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_39.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_43.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_49.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_50.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_52.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_57.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_59.SetParent(g);
		owner_Connection_6 = parentGameObject;
		owner_Connection_11 = parentGameObject;
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
		logic_uScript_GetTankBlock_uScript_GetTankBlock_17.OnDisable();
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_21.OnDisable();
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_39.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_49.OnDisable();
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
	public void In([FriendlyName("chargerSpawnData", "")] SpawnTechData[] chargerSpawnData, [FriendlyName("turretSpawnData", "")] SpawnTechData[] turretSpawnData, [FriendlyName("teslaTurretShieldBlockType", "")] BlockTypes teslaTurretShieldBlockType)
	{
		external_8 = chargerSpawnData;
		external_13 = turretSpawnData;
		external_15 = teslaTurretShieldBlockType;
		Relay_In_5();
	}

	private void Relay_True_1()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1.True(out logic_uScriptAct_SetBool_Target_1);
		local_TurretShieldsEnabled_System_Boolean = logic_uScriptAct_SetBool_Target_1;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_1.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_1.SetFalse;
		if (setTrue)
		{
			Relay_In_38();
		}
		if (setFalse)
		{
			Relay_In_10();
		}
	}

	private void Relay_False_1()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1.False(out logic_uScriptAct_SetBool_Target_1);
		local_TurretShieldsEnabled_System_Boolean = logic_uScriptAct_SetBool_Target_1;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_1.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_1.SetFalse;
		if (setTrue)
		{
			Relay_In_38();
		}
		if (setFalse)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_2()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_2.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_2.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_5()
	{
		int num = 0;
		Array array = external_8;
		if (logic_uScript_GetAndCheckTechs_techData_5.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_5, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_5, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_5 = owner_Connection_6;
		int num2 = 0;
		Array array2 = local_ChargerTechData_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_5.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_5, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_5, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_5 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_5.In(logic_uScript_GetAndCheckTechs_techData_5, logic_uScript_GetAndCheckTechs_ownerNode_5, ref logic_uScript_GetAndCheckTechs_techs_5);
		local_ChargerTechData_TankArray = logic_uScript_GetAndCheckTechs_techs_5;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_5.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_5.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_5.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_5.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_1();
		}
		if (someAlive)
		{
			Relay_True_1();
		}
		if (allDead)
		{
			Relay_False_1();
		}
		if (waitingToSpawn)
		{
			Relay_In_10();
		}
	}

	private void Relay_Connection_7()
	{
	}

	private void Relay_Connection_8()
	{
	}

	private void Relay_Connection_9()
	{
		if (this.Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.turretDestroyed = external_28;
			this.Out(this, e);
		}
	}

	private void Relay_In_10()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_10.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_10.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_12()
	{
		int num = 0;
		Array array = external_13;
		if (logic_uScript_GetAndCheckTechs_techData_12.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_12, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_12, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_12 = owner_Connection_11;
		int num2 = 0;
		Array array2 = local_Turrets_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_12.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_12, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_12, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_12 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_12.In(logic_uScript_GetAndCheckTechs_techData_12, logic_uScript_GetAndCheckTechs_ownerNode_12, ref logic_uScript_GetAndCheckTechs_techs_12);
		local_Turrets_TankArray = logic_uScript_GetAndCheckTechs_techs_12;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_12.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_12.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_12.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_12.WaitingToSpawn;
		if (allAlive)
		{
			Relay_False_25();
		}
		if (someAlive)
		{
			Relay_False_25();
		}
		if (allDead)
		{
			Relay_True_27();
		}
		if (waitingToSpawn)
		{
			Relay_In_26();
		}
	}

	private void Relay_Connection_13()
	{
	}

	private void Relay_Connection_15()
	{
	}

	private void Relay_In_17()
	{
		logic_uScript_GetTankBlock_tank_17 = local_BossTurretTech_Tank;
		logic_uScript_GetTankBlock_blockType_17 = external_15;
		logic_uScript_GetTankBlock_Return_17 = logic_uScript_GetTankBlock_uScript_GetTankBlock_17.In(logic_uScript_GetTankBlock_tank_17, logic_uScript_GetTankBlock_blockType_17);
		local_TurretShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_17;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_17.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_17.NotFound;
		if (returned)
		{
			Relay_In_23();
		}
		if (notFound)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_20()
	{
		logic_uScriptCon_CompareBool_Bool_20 = local_TurretShieldsEnabled_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.In(logic_uScriptCon_CompareBool_Bool_20);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.False;
		if (num)
		{
			Relay_Hide_21();
		}
		if (flag)
		{
			Relay_Show_21();
		}
	}

	private void Relay_Show_21()
	{
		logic_uScript_SetTechMarkerState_tech_21 = local_BossTurretTech_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_21.Show(logic_uScript_SetTechMarkerState_tech_21);
	}

	private void Relay_Hide_21()
	{
		logic_uScript_SetTechMarkerState_tech_21 = local_BossTurretTech_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_21.Hide(logic_uScript_SetTechMarkerState_tech_21);
	}

	private void Relay_In_22()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_22.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_22.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_23()
	{
		logic_uScript_SetShieldEnabled_targetObject_23 = local_TurretShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_enable_23 = local_TurretShieldsEnabled_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_23.In(logic_uScript_SetShieldEnabled_targetObject_23, logic_uScript_SetShieldEnabled_enable_23);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_23.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_True_25()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_25.True(out logic_uScriptAct_SetBool_Target_25);
		external_28 = logic_uScriptAct_SetBool_Target_25;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_25.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_False_25()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_25.False(out logic_uScriptAct_SetBool_Target_25);
		external_28 = logic_uScriptAct_SetBool_Target_25;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_25.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_In_26()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_26.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_26.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_True_27()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.True(out logic_uScriptAct_SetBool_Target_27);
		external_28 = logic_uScriptAct_SetBool_Target_27;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_27.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_False_27()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.False(out logic_uScriptAct_SetBool_Target_27);
		external_28 = logic_uScriptAct_SetBool_Target_27;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_27.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_Connection_28()
	{
	}

	private void Relay_In_29()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29.Out)
		{
			Relay_Connection_9();
		}
	}

	private void Relay_In_30()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_30.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_30.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_31()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_36()
	{
		logic_uScriptCon_CompareBool_Bool_36 = local_TurretShieldsEnabled_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.In(logic_uScriptCon_CompareBool_Bool_36);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.False;
		if (num)
		{
			Relay_In_57();
		}
		if (flag)
		{
			Relay_In_59();
		}
	}

	private void Relay_SetInvulnerable_37()
	{
		int num = 0;
		Array array = local_Turrets_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_37.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_37, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_37, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_37.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_37);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_37.Out)
		{
			Relay_AtIndex_52();
		}
	}

	private void Relay_SetVulnerable_37()
	{
		int num = 0;
		Array array = local_Turrets_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_37.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_37, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_37, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_37.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_37);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_37.Out)
		{
			Relay_AtIndex_52();
		}
	}

	private void Relay_Reset_38()
	{
		int num = 0;
		Array array = local_ChargerTechData_TankArray;
		if (logic_uScript_ForEachListTech_List_38.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_38, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_38, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_38.Reset(logic_uScript_ForEachListTech_List_38, out logic_uScript_ForEachListTech_Value_38, out logic_uScript_ForEachListTech_currentIndex_38);
		local_CurrentCharger_Tank = logic_uScript_ForEachListTech_Value_38;
		local_CurrentChargerIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_38;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_38.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_38.Iteration;
		if (done)
		{
			Relay_In_50();
		}
		if (iteration)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_38()
	{
		int num = 0;
		Array array = local_ChargerTechData_TankArray;
		if (logic_uScript_ForEachListTech_List_38.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_38, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_38, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_38.In(logic_uScript_ForEachListTech_List_38, out logic_uScript_ForEachListTech_Value_38, out logic_uScript_ForEachListTech_currentIndex_38);
		local_CurrentCharger_Tank = logic_uScript_ForEachListTech_Value_38;
		local_CurrentChargerIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_38;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_38.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_38.Iteration;
		if (done)
		{
			Relay_In_50();
		}
		if (iteration)
		{
			Relay_In_49();
		}
	}

	private void Relay_Show_39()
	{
		logic_uScript_SetTechMarkerState_tech_39 = local_CurrentCharger_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_39.Show(logic_uScript_SetTechMarkerState_tech_39);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_39.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_Hide_39()
	{
		logic_uScript_SetTechMarkerState_tech_39 = local_CurrentCharger_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_39.Hide(logic_uScript_SetTechMarkerState_tech_39);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_39.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_40()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.Out)
		{
			Relay_In_48();
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

	private void Relay_In_43()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_43.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_43.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_48()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.Out)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_49()
	{
		logic_uScript_IsTechAlive_tech_49 = local_CurrentCharger_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_49.In(logic_uScript_IsTechAlive_tech_49);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_49.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_49.Destroyed;
		if (alive)
		{
			Relay_Hide_39();
		}
		if (destroyed)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_50()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_50.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_50.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_AtIndex_52()
	{
		int num = 0;
		Array array = local_Turrets_TankArray;
		if (logic_uScript_AccessListTech_techList_52.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_52, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_52, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_52.AtIndex(ref logic_uScript_AccessListTech_techList_52, logic_uScript_AccessListTech_index_52, out logic_uScript_AccessListTech_value_52);
		local_Turrets_TankArray = logic_uScript_AccessListTech_techList_52;
		local_BossTurretTech_Tank = logic_uScript_AccessListTech_value_52;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_52.Out)
		{
			Relay_In_17();
		}
	}

	private void Relay_In_57()
	{
		logic_uScript_SetBatteryChargeAmount_tech_57 = local_BossTurretTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_57.In(logic_uScript_SetBatteryChargeAmount_tech_57, logic_uScript_SetBatteryChargeAmount_chargeAmount_57);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_57.Out)
		{
			Relay_SetInvulnerable_37();
		}
	}

	private void Relay_In_59()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_59.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_59.Out)
		{
			Relay_SetVulnerable_37();
		}
	}
}
