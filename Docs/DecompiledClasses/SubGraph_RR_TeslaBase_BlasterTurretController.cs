using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_RR_TeslaBase_BlasterTurretController", "")]
public class SubGraph_RR_TeslaBase_BlasterTurretController : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public bool turretsDestroyed;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_15 = new SpawnTechData[0];

	private SpawnTechData[] external_21 = new SpawnTechData[0];

	private BlockTypes external_24;

	private string[] external_59 = new string[0];

	private bool external_51;

	private Tank local_ChargerTech_Tank;

	private Tank[] local_ChargerTechData_TankArray = new Tank[0];

	private Tank local_CurrentTurret_Tank;

	private int local_CurrentTurretIndex_System_Int32;

	private string local_TurretAlertZone_System_String = "";

	private float local_TurretBatteryChargeAmount_System_Single;

	private Tank[] local_Turrets_TankArray = new Tank[0];

	private TankBlock local_TurretShieldBlock_TankBlock;

	private bool local_TurretShieldsEnabled_System_Boolean;

	private GameObject owner_Connection_13;

	private GameObject owner_Connection_19;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_0;

	private bool logic_uScript_SetTechMarkerState_Out_0 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_2 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_2;

	private bool logic_uScriptAct_SetBool_Out_2 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_2 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_2 = true;

	private uScriptAct_SetFloat logic_uScriptAct_SetFloat_uScriptAct_SetFloat_3 = new uScriptAct_SetFloat();

	private float logic_uScriptAct_SetFloat_Value_3 = 100f;

	private float logic_uScriptAct_SetFloat_Target_3;

	private bool logic_uScriptAct_SetFloat_Out_3 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_4 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_4 = new Tank[0];

	private int logic_uScript_AccessListTech_index_4;

	private Tank logic_uScript_AccessListTech_value_4;

	private bool logic_uScript_AccessListTech_Out_4 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_5 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_5 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_8 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_8;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_8 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_8;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_8 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_8 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_8 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_8 = true;

	private uScriptAct_SetFloat logic_uScriptAct_SetFloat_uScriptAct_SetFloat_12 = new uScriptAct_SetFloat();

	private float logic_uScriptAct_SetFloat_Value_12;

	private float logic_uScriptAct_SetFloat_Target_12;

	private bool logic_uScriptAct_SetFloat_Out_12 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_18 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_18 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_20 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_20;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_20 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_20;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_20 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_20 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_20 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_20 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_26 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_26;

	private BlockTypes logic_uScript_GetTankBlock_blockType_26;

	private TankBlock logic_uScript_GetTankBlock_Return_26;

	private bool logic_uScript_GetTankBlock_Out_26 = true;

	private bool logic_uScript_GetTankBlock_Returned_26 = true;

	private bool logic_uScript_GetTankBlock_NotFound_26 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_30;

	private bool logic_uScriptCon_CompareBool_True_30 = true;

	private bool logic_uScriptCon_CompareBool_False_30 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_31 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_31;

	private bool logic_uScript_SetTechMarkerState_Out_31 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_32 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_32 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_34 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_34;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_34;

	private bool logic_uScript_SetBatteryChargeAmount_Out_34 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_35 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_35 = "";

	private bool logic_uScript_SetShieldEnabled_enable_35;

	private bool logic_uScript_SetShieldEnabled_Out_35 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_42 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_42;

	private bool logic_uScript_IsTechAlive_Alive_42 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_42 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_43 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_43 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_43;

	private int logic_uScript_ForEachListTech_currentIndex_43;

	private bool logic_uScript_ForEachListTech_Immediate_43 = true;

	private bool logic_uScript_ForEachListTech_Done_43 = true;

	private bool logic_uScript_ForEachListTech_Iteration_43 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_44 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_44 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_45 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_46 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_46 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_47 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_47 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_48 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_48;

	private bool logic_uScriptAct_SetBool_Out_48 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_48 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_48 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_49 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_50 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_50;

	private bool logic_uScriptAct_SetBool_Out_50 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_50 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_50 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_53 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_53 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_54 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_54 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_55 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_55 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_56 = true;

	private uScriptAct_AccessListString logic_uScriptAct_AccessListString_uScriptAct_AccessListString_61 = new uScriptAct_AccessListString();

	private string[] logic_uScriptAct_AccessListString_StringList_61 = new string[0];

	private int logic_uScriptAct_AccessListString_Index_61;

	private string logic_uScriptAct_AccessListString_Value_61;

	private bool logic_uScriptAct_AccessListString_Out_61 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_63 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_63 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_63 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_63 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_63 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_63 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_63 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_64 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_64;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_64 = AITreeType.AITypes.Guard;

	private bool logic_uScript_SetTechAIType_Out_64 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_65 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_65;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_65;

	private bool logic_uScript_SetTechAIType_Out_65 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_69 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_69 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_69 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_71 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_71;

	private bool logic_uScriptCon_CompareBool_True_71 = true;

	private bool logic_uScriptCon_CompareBool_False_71 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_13 || !m_RegisteredForEvents)
		{
			owner_Connection_13 = parentGameObject;
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
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
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.SetParent(g);
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_3.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_4.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_5.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8.SetParent(g);
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_12.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_18.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_26.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_31.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_32.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_34.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_35.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_42.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_43.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_44.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_46.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_47.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_48.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_53.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_54.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_55.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56.SetParent(g);
		logic_uScriptAct_AccessListString_uScriptAct_AccessListString_61.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_63.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_64.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_65.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_69.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_71.SetParent(g);
		owner_Connection_13 = parentGameObject;
		owner_Connection_19 = parentGameObject;
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
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_26.OnDisable();
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_31.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_42.OnDisable();
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
	public void In([FriendlyName("chargerSpawnData", "")] SpawnTechData[] chargerSpawnData, [FriendlyName("turretSpawnData", "")] SpawnTechData[] turretSpawnData, [FriendlyName("turretShieldBlockType", "")] BlockTypes turretShieldBlockType, [FriendlyName("turretAlertZones", "")] string[] turretAlertZones)
	{
		external_15 = chargerSpawnData;
		external_21 = turretSpawnData;
		external_24 = turretShieldBlockType;
		external_59 = turretAlertZones;
		Relay_In_8();
	}

	private void Relay_Show_0()
	{
		logic_uScript_SetTechMarkerState_tech_0 = local_ChargerTech_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.Show(logic_uScript_SetTechMarkerState_tech_0);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_Hide_0()
	{
		logic_uScript_SetTechMarkerState_tech_0 = local_ChargerTech_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.Hide(logic_uScript_SetTechMarkerState_tech_0);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_True_2()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.True(out logic_uScriptAct_SetBool_Target_2);
		local_TurretShieldsEnabled_System_Boolean = logic_uScriptAct_SetBool_Target_2;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_2.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_2.SetFalse;
		if (setTrue)
		{
			Relay_In_3();
		}
		if (setFalse)
		{
			Relay_In_12();
		}
	}

	private void Relay_False_2()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.False(out logic_uScriptAct_SetBool_Target_2);
		local_TurretShieldsEnabled_System_Boolean = logic_uScriptAct_SetBool_Target_2;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_2.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_2.SetFalse;
		if (setTrue)
		{
			Relay_In_3();
		}
		if (setFalse)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_3()
	{
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_3.In(logic_uScriptAct_SetFloat_Value_3, out logic_uScriptAct_SetFloat_Target_3);
		local_TurretBatteryChargeAmount_System_Single = logic_uScriptAct_SetFloat_Target_3;
		if (logic_uScriptAct_SetFloat_uScriptAct_SetFloat_3.Out)
		{
			Relay_AtIndex_4();
		}
	}

	private void Relay_AtIndex_4()
	{
		int num = 0;
		Array array = local_ChargerTechData_TankArray;
		if (logic_uScript_AccessListTech_techList_4.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_4, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_4, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_4.AtIndex(ref logic_uScript_AccessListTech_techList_4, logic_uScript_AccessListTech_index_4, out logic_uScript_AccessListTech_value_4);
		local_ChargerTechData_TankArray = logic_uScript_AccessListTech_techList_4;
		local_ChargerTech_Tank = logic_uScript_AccessListTech_value_4;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_4.Out)
		{
			Relay_Hide_0();
		}
	}

	private void Relay_In_5()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_5.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_5.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_8()
	{
		int num = 0;
		Array array = external_15;
		if (logic_uScript_GetAndCheckTechs_techData_8.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_8, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_8, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_8 = owner_Connection_13;
		int num2 = 0;
		Array array2 = local_ChargerTechData_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_8.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_8, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_8, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_8 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8.In(logic_uScript_GetAndCheckTechs_techData_8, logic_uScript_GetAndCheckTechs_ownerNode_8, ref logic_uScript_GetAndCheckTechs_techs_8);
		local_ChargerTechData_TankArray = logic_uScript_GetAndCheckTechs_techs_8;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_2();
		}
		if (someAlive)
		{
			Relay_True_2();
		}
		if (allDead)
		{
			Relay_False_2();
		}
		if (waitingToSpawn)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_12()
	{
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_12.In(logic_uScriptAct_SetFloat_Value_12, out logic_uScriptAct_SetFloat_Target_12);
		local_TurretBatteryChargeAmount_System_Single = logic_uScriptAct_SetFloat_Target_12;
		if (logic_uScriptAct_SetFloat_uScriptAct_SetFloat_12.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_Connection_14()
	{
	}

	private void Relay_Connection_15()
	{
	}

	private void Relay_Connection_17()
	{
		if (this.Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.turretsDestroyed = external_51;
			this.Out(this, e);
		}
	}

	private void Relay_In_18()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_18.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_18.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_20()
	{
		int num = 0;
		Array array = external_21;
		if (logic_uScript_GetAndCheckTechs_techData_20.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_20, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_20, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_20 = owner_Connection_19;
		int num2 = 0;
		Array array2 = local_Turrets_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_20.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_20, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_20, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_20 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20.In(logic_uScript_GetAndCheckTechs_techData_20, logic_uScript_GetAndCheckTechs_ownerNode_20, ref logic_uScript_GetAndCheckTechs_techs_20);
		local_Turrets_TankArray = logic_uScript_GetAndCheckTechs_techs_20;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_20.WaitingToSpawn;
		if (allAlive)
		{
			Relay_False_48();
		}
		if (someAlive)
		{
			Relay_False_48();
		}
		if (allDead)
		{
			Relay_True_50();
		}
		if (waitingToSpawn)
		{
			Relay_In_49();
		}
	}

	private void Relay_Connection_21()
	{
	}

	private void Relay_Connection_24()
	{
	}

	private void Relay_In_26()
	{
		logic_uScript_GetTankBlock_tank_26 = local_CurrentTurret_Tank;
		logic_uScript_GetTankBlock_blockType_26 = external_24;
		logic_uScript_GetTankBlock_Return_26 = logic_uScript_GetTankBlock_uScript_GetTankBlock_26.In(logic_uScript_GetTankBlock_tank_26, logic_uScript_GetTankBlock_blockType_26);
		local_TurretShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_26;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_26.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_26.NotFound;
		if (returned)
		{
			Relay_In_35();
		}
		if (notFound)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_30()
	{
		logic_uScriptCon_CompareBool_Bool_30 = local_TurretShieldsEnabled_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.In(logic_uScriptCon_CompareBool_Bool_30);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.False;
		if (num)
		{
			Relay_Hide_31();
		}
		if (flag)
		{
			Relay_Show_31();
		}
	}

	private void Relay_Show_31()
	{
		logic_uScript_SetTechMarkerState_tech_31 = local_CurrentTurret_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_31.Show(logic_uScript_SetTechMarkerState_tech_31);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_31.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_Hide_31()
	{
		logic_uScript_SetTechMarkerState_tech_31 = local_CurrentTurret_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_31.Hide(logic_uScript_SetTechMarkerState_tech_31);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_31.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_32()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_32.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_32.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_34()
	{
		logic_uScript_SetBatteryChargeAmount_tech_34 = local_CurrentTurret_Tank;
		logic_uScript_SetBatteryChargeAmount_chargeAmount_34 = local_TurretBatteryChargeAmount_System_Single;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_34.In(logic_uScript_SetBatteryChargeAmount_tech_34, logic_uScript_SetBatteryChargeAmount_chargeAmount_34);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_34.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_35()
	{
		logic_uScript_SetShieldEnabled_targetObject_35 = local_TurretShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_enable_35 = local_TurretShieldsEnabled_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_35.In(logic_uScript_SetShieldEnabled_targetObject_35, logic_uScript_SetShieldEnabled_enable_35);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_35.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_42()
	{
		logic_uScript_IsTechAlive_tech_42 = local_CurrentTurret_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_42.In(logic_uScript_IsTechAlive_tech_42);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_42.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_42.Destroyed;
		if (alive)
		{
			Relay_AtIndex_61();
		}
		if (destroyed)
		{
			Relay_In_45();
		}
	}

	private void Relay_Reset_43()
	{
		int num = 0;
		Array array = local_Turrets_TankArray;
		if (logic_uScript_ForEachListTech_List_43.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_43, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_43, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_43.Reset(logic_uScript_ForEachListTech_List_43, out logic_uScript_ForEachListTech_Value_43, out logic_uScript_ForEachListTech_currentIndex_43);
		local_CurrentTurret_Tank = logic_uScript_ForEachListTech_Value_43;
		local_CurrentTurretIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_43;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_43.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_43.Iteration;
		if (done)
		{
			Relay_In_53();
		}
		if (iteration)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_43()
	{
		int num = 0;
		Array array = local_Turrets_TankArray;
		if (logic_uScript_ForEachListTech_List_43.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_43, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_43, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_43.In(logic_uScript_ForEachListTech_List_43, out logic_uScript_ForEachListTech_Value_43, out logic_uScript_ForEachListTech_currentIndex_43);
		local_CurrentTurret_Tank = logic_uScript_ForEachListTech_Value_43;
		local_CurrentTurretIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_43;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_43.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_43.Iteration;
		if (done)
		{
			Relay_In_53();
		}
		if (iteration)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_44()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_44.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_44.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_45()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_46()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_46.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_46.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_In_47()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_47.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_47.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_True_48()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_48.True(out logic_uScriptAct_SetBool_Target_48);
		external_51 = logic_uScriptAct_SetBool_Target_48;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_48.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_False_48()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_48.False(out logic_uScriptAct_SetBool_Target_48);
		external_51 = logic_uScriptAct_SetBool_Target_48;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_48.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_49()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_True_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.True(out logic_uScriptAct_SetBool_Target_50);
		external_51 = logic_uScriptAct_SetBool_Target_50;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_50.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_False_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.False(out logic_uScriptAct_SetBool_Target_50);
		external_51 = logic_uScriptAct_SetBool_Target_50;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_50.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_Connection_51()
	{
	}

	private void Relay_In_53()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_53.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_53.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_54()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_54.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_54.Out)
		{
			Relay_Connection_17();
		}
	}

	private void Relay_In_55()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_55.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_55.Out)
		{
			Relay_In_56();
		}
	}

	private void Relay_In_56()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_Connection_59()
	{
	}

	private void Relay_First_61()
	{
		int num = 0;
		Array array = external_59;
		if (logic_uScriptAct_AccessListString_StringList_61.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListString_StringList_61, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListString_StringList_61, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListString_Index_61 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AccessListString_uScriptAct_AccessListString_61.First(logic_uScriptAct_AccessListString_StringList_61, logic_uScriptAct_AccessListString_Index_61, out logic_uScriptAct_AccessListString_Value_61);
		local_TurretAlertZone_System_String = logic_uScriptAct_AccessListString_Value_61;
		if (logic_uScriptAct_AccessListString_uScriptAct_AccessListString_61.Out)
		{
			Relay_In_63();
		}
	}

	private void Relay_Last_61()
	{
		int num = 0;
		Array array = external_59;
		if (logic_uScriptAct_AccessListString_StringList_61.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListString_StringList_61, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListString_StringList_61, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListString_Index_61 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AccessListString_uScriptAct_AccessListString_61.Last(logic_uScriptAct_AccessListString_StringList_61, logic_uScriptAct_AccessListString_Index_61, out logic_uScriptAct_AccessListString_Value_61);
		local_TurretAlertZone_System_String = logic_uScriptAct_AccessListString_Value_61;
		if (logic_uScriptAct_AccessListString_uScriptAct_AccessListString_61.Out)
		{
			Relay_In_63();
		}
	}

	private void Relay_Random_61()
	{
		int num = 0;
		Array array = external_59;
		if (logic_uScriptAct_AccessListString_StringList_61.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListString_StringList_61, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListString_StringList_61, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListString_Index_61 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AccessListString_uScriptAct_AccessListString_61.Random(logic_uScriptAct_AccessListString_StringList_61, logic_uScriptAct_AccessListString_Index_61, out logic_uScriptAct_AccessListString_Value_61);
		local_TurretAlertZone_System_String = logic_uScriptAct_AccessListString_Value_61;
		if (logic_uScriptAct_AccessListString_uScriptAct_AccessListString_61.Out)
		{
			Relay_In_63();
		}
	}

	private void Relay_AtIndex_61()
	{
		int num = 0;
		Array array = external_59;
		if (logic_uScriptAct_AccessListString_StringList_61.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListString_StringList_61, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListString_StringList_61, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListString_Index_61 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AccessListString_uScriptAct_AccessListString_61.AtIndex(logic_uScriptAct_AccessListString_StringList_61, logic_uScriptAct_AccessListString_Index_61, out logic_uScriptAct_AccessListString_Value_61);
		local_TurretAlertZone_System_String = logic_uScriptAct_AccessListString_Value_61;
		if (logic_uScriptAct_AccessListString_uScriptAct_AccessListString_61.Out)
		{
			Relay_In_63();
		}
	}

	private void Relay_In_63()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_63 = local_TurretAlertZone_System_String;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_63.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_63);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_63.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_63.OutOfRange;
		if (inRange)
		{
			Relay_In_64();
		}
		if (outOfRange)
		{
			Relay_In_65();
		}
	}

	private void Relay_In_64()
	{
		logic_uScript_SetTechAIType_tech_64 = local_CurrentTurret_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_64.In(logic_uScript_SetTechAIType_tech_64, logic_uScript_SetTechAIType_aiType_64);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_64.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_In_65()
	{
		logic_uScript_SetTechAIType_tech_65 = local_CurrentTurret_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_65.In(logic_uScript_SetTechAIType_tech_65, logic_uScript_SetTechAIType_aiType_65);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_65.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_SetInvulnerable_69()
	{
		int num = 0;
		Array array = local_Turrets_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_69.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_69, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_69, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_69.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_69);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_69.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_SetVulnerable_69()
	{
		int num = 0;
		Array array = local_Turrets_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_69.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_69, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_69, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_69.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_69);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_69.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_71()
	{
		logic_uScriptCon_CompareBool_Bool_71 = local_TurretShieldsEnabled_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_71.In(logic_uScriptCon_CompareBool_Bool_71);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_71.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_71.False;
		if (num)
		{
			Relay_SetInvulnerable_69();
		}
		if (flag)
		{
			Relay_SetVulnerable_69();
		}
	}
}
