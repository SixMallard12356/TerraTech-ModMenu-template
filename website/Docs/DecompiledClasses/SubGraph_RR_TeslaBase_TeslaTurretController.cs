using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_RR_TeslaBase_TeslaTurretController", "")]
public class SubGraph_RR_TeslaBase_TeslaTurretController : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public bool turretsDestroyed;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_11 = new SpawnTechData[0];

	private SpawnTechData[] external_17 = new SpawnTechData[0];

	private BlockTypes external_20;

	private bool external_47;

	private Tank[] local_ChargerTechData_TankArray = new Tank[0];

	private Tank local_CurrentCharger_Tank;

	private int local_CurrentChargerIndex_System_Int32;

	private Tank local_CurrentTurret_Tank;

	private int local_CurrentTurretIndex_System_Int32;

	private float local_TurretBatteryChargeAmount_System_Single;

	private Tank[] local_Turrets_TankArray = new Tank[0];

	private TankBlock local_TurretShieldBlock_TankBlock;

	private bool local_TurretShieldsEnabled_System_Boolean;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_15;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1;

	private bool logic_uScriptAct_SetBool_Out_1 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1 = true;

	private uScriptAct_SetFloat logic_uScriptAct_SetFloat_uScriptAct_SetFloat_2 = new uScriptAct_SetFloat();

	private float logic_uScriptAct_SetFloat_Value_2 = 100f;

	private float logic_uScriptAct_SetFloat_Target_2;

	private bool logic_uScriptAct_SetFloat_Out_2 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_3 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_3 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_6 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_6;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_6 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_6;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_6 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_6 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_6 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_6 = true;

	private uScriptAct_SetFloat logic_uScriptAct_SetFloat_uScriptAct_SetFloat_8 = new uScriptAct_SetFloat();

	private float logic_uScriptAct_SetFloat_Value_8;

	private float logic_uScriptAct_SetFloat_Target_8;

	private bool logic_uScriptAct_SetFloat_Out_8 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_14 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_14 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_16 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_16;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_16 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_16;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_16 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_16 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_16 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_16 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_22 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_22;

	private BlockTypes logic_uScript_GetTankBlock_blockType_22;

	private TankBlock logic_uScript_GetTankBlock_Return_22;

	private bool logic_uScript_GetTankBlock_Out_22 = true;

	private bool logic_uScript_GetTankBlock_Returned_22 = true;

	private bool logic_uScript_GetTankBlock_NotFound_22 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_26;

	private bool logic_uScriptCon_CompareBool_True_26 = true;

	private bool logic_uScriptCon_CompareBool_False_26 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_27 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_27;

	private bool logic_uScript_SetTechMarkerState_Out_27 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_28 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_30 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_30;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_30;

	private bool logic_uScript_SetBatteryChargeAmount_Out_30 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_31 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_31 = "";

	private bool logic_uScript_SetShieldEnabled_enable_31;

	private bool logic_uScript_SetShieldEnabled_Out_31 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_38 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_38;

	private bool logic_uScript_IsTechAlive_Alive_38 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_38 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_39 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_39 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_39;

	private int logic_uScript_ForEachListTech_currentIndex_39;

	private bool logic_uScript_ForEachListTech_Immediate_39 = true;

	private bool logic_uScript_ForEachListTech_Done_39 = true;

	private bool logic_uScript_ForEachListTech_Iteration_39 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_40 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_41 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_42 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_43 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_43 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_44 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_44;

	private bool logic_uScriptAct_SetBool_Out_44 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_44 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_44 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_45 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_46 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_46;

	private bool logic_uScriptAct_SetBool_Out_46 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_46 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_46 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_49 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_50 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_50 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_51 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_51 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_52 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_52 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_57;

	private bool logic_uScriptCon_CompareBool_True_57 = true;

	private bool logic_uScriptCon_CompareBool_False_57 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_58 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_58 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_58 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_60 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_60 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_60;

	private int logic_uScript_ForEachListTech_currentIndex_60;

	private bool logic_uScript_ForEachListTech_Immediate_60 = true;

	private bool logic_uScript_ForEachListTech_Done_60 = true;

	private bool logic_uScript_ForEachListTech_Iteration_60 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_61 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_61;

	private bool logic_uScript_SetTechMarkerState_Out_61 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_62 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_62 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_63 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_63 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_65 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_65 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_70 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_70 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_71 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_71;

	private bool logic_uScript_IsTechAlive_Alive_71 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_71 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_72 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
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
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1.SetParent(g);
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_2.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_3.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.SetParent(g);
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_8.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_14.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_22.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_27.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_30.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_31.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_38.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_39.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_43.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_46.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_50.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_51.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_52.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_58.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_60.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_61.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_62.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_63.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_65.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_70.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_71.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.SetParent(g);
		owner_Connection_9 = parentGameObject;
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
		logic_uScript_GetTankBlock_uScript_GetTankBlock_22.OnDisable();
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_27.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_38.OnDisable();
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_61.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_71.OnDisable();
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
		external_11 = chargerSpawnData;
		external_17 = turretSpawnData;
		external_20 = teslaTurretShieldBlockType;
		Relay_In_6();
	}

	private void Relay_True_1()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1.True(out logic_uScriptAct_SetBool_Target_1);
		local_TurretShieldsEnabled_System_Boolean = logic_uScriptAct_SetBool_Target_1;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_1.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_1.SetFalse;
		if (setTrue)
		{
			Relay_In_2();
		}
		if (setFalse)
		{
			Relay_In_8();
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
			Relay_In_2();
		}
		if (setFalse)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_2()
	{
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_2.In(logic_uScriptAct_SetFloat_Value_2, out logic_uScriptAct_SetFloat_Target_2);
		local_TurretBatteryChargeAmount_System_Single = logic_uScriptAct_SetFloat_Target_2;
		if (logic_uScriptAct_SetFloat_uScriptAct_SetFloat_2.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_3()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_3.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_3.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_6()
	{
		int num = 0;
		Array array = external_11;
		if (logic_uScript_GetAndCheckTechs_techData_6.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_6, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_6, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_6 = owner_Connection_9;
		int num2 = 0;
		Array array2 = local_ChargerTechData_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_6.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_6, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_6, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_6 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.In(logic_uScript_GetAndCheckTechs_techData_6, logic_uScript_GetAndCheckTechs_ownerNode_6, ref logic_uScript_GetAndCheckTechs_techs_6);
		local_ChargerTechData_TankArray = logic_uScript_GetAndCheckTechs_techs_6;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.WaitingToSpawn;
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
			Relay_In_14();
		}
	}

	private void Relay_In_8()
	{
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_8.In(logic_uScriptAct_SetFloat_Value_8, out logic_uScriptAct_SetFloat_Target_8);
		local_TurretBatteryChargeAmount_System_Single = logic_uScriptAct_SetFloat_Target_8;
		if (logic_uScriptAct_SetFloat_uScriptAct_SetFloat_8.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_Connection_10()
	{
	}

	private void Relay_Connection_11()
	{
	}

	private void Relay_Connection_13()
	{
		if (this.Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.turretsDestroyed = external_47;
			this.Out(this, e);
		}
	}

	private void Relay_In_14()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_14.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_14.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_In_16()
	{
		int num = 0;
		Array array = external_17;
		if (logic_uScript_GetAndCheckTechs_techData_16.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_16, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_16, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_16 = owner_Connection_15;
		int num2 = 0;
		Array array2 = local_Turrets_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_16.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_16, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_16, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_16 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.In(logic_uScript_GetAndCheckTechs_techData_16, logic_uScript_GetAndCheckTechs_ownerNode_16, ref logic_uScript_GetAndCheckTechs_techs_16);
		local_Turrets_TankArray = logic_uScript_GetAndCheckTechs_techs_16;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_16.WaitingToSpawn;
		if (allAlive)
		{
			Relay_False_44();
		}
		if (someAlive)
		{
			Relay_False_44();
		}
		if (allDead)
		{
			Relay_True_46();
		}
		if (waitingToSpawn)
		{
			Relay_In_45();
		}
	}

	private void Relay_Connection_17()
	{
	}

	private void Relay_Connection_20()
	{
	}

	private void Relay_In_22()
	{
		logic_uScript_GetTankBlock_tank_22 = local_CurrentTurret_Tank;
		logic_uScript_GetTankBlock_blockType_22 = external_20;
		logic_uScript_GetTankBlock_Return_22 = logic_uScript_GetTankBlock_uScript_GetTankBlock_22.In(logic_uScript_GetTankBlock_tank_22, logic_uScript_GetTankBlock_blockType_22);
		local_TurretShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_22;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_22.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_22.NotFound;
		if (returned)
		{
			Relay_In_31();
		}
		if (notFound)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_26()
	{
		logic_uScriptCon_CompareBool_Bool_26 = local_TurretShieldsEnabled_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.In(logic_uScriptCon_CompareBool_Bool_26);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.False;
		if (num)
		{
			Relay_Hide_27();
		}
		if (flag)
		{
			Relay_Show_27();
		}
	}

	private void Relay_Show_27()
	{
		logic_uScript_SetTechMarkerState_tech_27 = local_CurrentTurret_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_27.Show(logic_uScript_SetTechMarkerState_tech_27);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_27.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_Hide_27()
	{
		logic_uScript_SetTechMarkerState_tech_27 = local_CurrentTurret_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_27.Hide(logic_uScript_SetTechMarkerState_tech_27);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_27.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_28()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_30()
	{
		logic_uScript_SetBatteryChargeAmount_tech_30 = local_CurrentTurret_Tank;
		logic_uScript_SetBatteryChargeAmount_chargeAmount_30 = local_TurretBatteryChargeAmount_System_Single;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_30.In(logic_uScript_SetBatteryChargeAmount_tech_30, logic_uScript_SetBatteryChargeAmount_chargeAmount_30);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_30.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_In_31()
	{
		logic_uScript_SetShieldEnabled_targetObject_31 = local_TurretShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_enable_31 = local_TurretShieldsEnabled_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_31.In(logic_uScript_SetShieldEnabled_targetObject_31, logic_uScript_SetShieldEnabled_enable_31);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_31.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_38()
	{
		logic_uScript_IsTechAlive_tech_38 = local_CurrentTurret_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_38.In(logic_uScript_IsTechAlive_tech_38);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_38.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_38.Destroyed;
		if (alive)
		{
			Relay_In_22();
		}
		if (destroyed)
		{
			Relay_In_41();
		}
	}

	private void Relay_Reset_39()
	{
		int num = 0;
		Array array = local_Turrets_TankArray;
		if (logic_uScript_ForEachListTech_List_39.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_39, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_39, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_39.Reset(logic_uScript_ForEachListTech_List_39, out logic_uScript_ForEachListTech_Value_39, out logic_uScript_ForEachListTech_currentIndex_39);
		local_CurrentTurret_Tank = logic_uScript_ForEachListTech_Value_39;
		local_CurrentTurretIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_39;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_39.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_39.Iteration;
		if (done)
		{
			Relay_In_49();
		}
		if (iteration)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_39()
	{
		int num = 0;
		Array array = local_Turrets_TankArray;
		if (logic_uScript_ForEachListTech_List_39.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_39, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_39, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_39.In(logic_uScript_ForEachListTech_List_39, out logic_uScript_ForEachListTech_Value_39, out logic_uScript_ForEachListTech_currentIndex_39);
		local_CurrentTurret_Tank = logic_uScript_ForEachListTech_Value_39;
		local_CurrentTurretIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_39;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_39.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_39.Iteration;
		if (done)
		{
			Relay_In_49();
		}
		if (iteration)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_40()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.Out)
		{
			Relay_In_39();
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
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42.Out)
		{
			Relay_In_43();
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

	private void Relay_True_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.True(out logic_uScriptAct_SetBool_Target_44);
		external_47 = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_False_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.False(out logic_uScriptAct_SetBool_Target_44);
		external_47 = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_45()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_True_46()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_46.True(out logic_uScriptAct_SetBool_Target_46);
		external_47 = logic_uScriptAct_SetBool_Target_46;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_46.Out)
		{
			Relay_In_51();
		}
	}

	private void Relay_False_46()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_46.False(out logic_uScriptAct_SetBool_Target_46);
		external_47 = logic_uScriptAct_SetBool_Target_46;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_46.Out)
		{
			Relay_In_51();
		}
	}

	private void Relay_Connection_47()
	{
	}

	private void Relay_In_49()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_50()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_50.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_50.Out)
		{
			Relay_Connection_13();
		}
	}

	private void Relay_In_51()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_51.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_51.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_52()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_52.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_52.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_57()
	{
		logic_uScriptCon_CompareBool_Bool_57 = local_TurretShieldsEnabled_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.In(logic_uScriptCon_CompareBool_Bool_57);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.False;
		if (num)
		{
			Relay_SetInvulnerable_58();
		}
		if (flag)
		{
			Relay_SetVulnerable_58();
		}
	}

	private void Relay_SetInvulnerable_58()
	{
		int num = 0;
		Array array = local_Turrets_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_58.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_58, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_58, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_58.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_58);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_58.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_SetVulnerable_58()
	{
		int num = 0;
		Array array = local_Turrets_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_58.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_58, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_58, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_58.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_58);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_58.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_Reset_60()
	{
		int num = 0;
		Array array = local_ChargerTechData_TankArray;
		if (logic_uScript_ForEachListTech_List_60.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_60, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_60, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_60.Reset(logic_uScript_ForEachListTech_List_60, out logic_uScript_ForEachListTech_Value_60, out logic_uScript_ForEachListTech_currentIndex_60);
		local_CurrentCharger_Tank = logic_uScript_ForEachListTech_Value_60;
		local_CurrentChargerIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_60;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_60.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_60.Iteration;
		if (done)
		{
			Relay_In_72();
		}
		if (iteration)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_60()
	{
		int num = 0;
		Array array = local_ChargerTechData_TankArray;
		if (logic_uScript_ForEachListTech_List_60.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_60, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_60, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_60.In(logic_uScript_ForEachListTech_List_60, out logic_uScript_ForEachListTech_Value_60, out logic_uScript_ForEachListTech_currentIndex_60);
		local_CurrentCharger_Tank = logic_uScript_ForEachListTech_Value_60;
		local_CurrentChargerIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_60;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_60.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_60.Iteration;
		if (done)
		{
			Relay_In_72();
		}
		if (iteration)
		{
			Relay_In_71();
		}
	}

	private void Relay_Show_61()
	{
		logic_uScript_SetTechMarkerState_tech_61 = local_CurrentCharger_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_61.Show(logic_uScript_SetTechMarkerState_tech_61);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_61.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_Hide_61()
	{
		logic_uScript_SetTechMarkerState_tech_61 = local_CurrentCharger_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_61.Hide(logic_uScript_SetTechMarkerState_tech_61);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_61.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_In_62()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_62.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_62.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_In_63()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_63.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_63.Out)
		{
			Relay_In_62();
		}
	}

	private void Relay_In_65()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_65.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_65.Out)
		{
			Relay_In_62();
		}
	}

	private void Relay_In_70()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_70.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_70.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_71()
	{
		logic_uScript_IsTechAlive_tech_71 = local_CurrentCharger_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_71.In(logic_uScript_IsTechAlive_tech_71);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_71.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_71.Destroyed;
		if (alive)
		{
			Relay_Hide_61();
		}
		if (destroyed)
		{
			Relay_In_63();
		}
	}

	private void Relay_In_72()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.Out)
		{
			Relay_In_16();
		}
	}
}
