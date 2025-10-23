using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_RR_CombatLab_ForcefieldAndChargerController", "")]
public class SubGraph_RR_CombatLab_ForcefieldAndChargerController : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public bool chargersDestroyed;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_16 = new SpawnTechData[0];

	private SpawnTechData[] external_17 = new SpawnTechData[0];

	private bool external_35;

	private Tank local_ChargerTech01_Tank;

	private Tank local_ChargerTech02_Tank;

	private Tank[] local_ChargerTechData_TankArray = new Tank[0];

	private bool local_ForcefieldEnabled_System_Boolean;

	private Tank[] local_Forcefields_TankArray = new Tank[0];

	private Tank local_ForcefieldTech_Tank;

	private GameObject owner_Connection_4;

	private GameObject owner_Connection_14;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_0;

	private bool logic_uScript_SetTechMarkerState_Out_0 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_3 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_3;

	private bool logic_uScriptAct_SetBool_Out_3 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_3 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_3 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_5 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_5 = new Tank[0];

	private int logic_uScript_AccessListTech_index_5;

	private Tank logic_uScript_AccessListTech_value_5;

	private bool logic_uScript_AccessListTech_Out_5 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_6 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_6 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_8 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_8;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_8 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_8;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_8 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_8 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_8 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_8 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_11 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_11 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_11;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_11 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_11;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_11 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_11 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_11 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_11 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_19 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_19 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_20 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_20 = new Tank[0];

	private int logic_uScript_AccessListTech_index_20 = 1;

	private Tank logic_uScript_AccessListTech_value_20;

	private bool logic_uScript_AccessListTech_Out_20 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_22 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_22;

	private bool logic_uScript_SetTechMarkerState_Out_22 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_24;

	private bool logic_uScriptCon_CompareBool_True_24 = true;

	private bool logic_uScriptCon_CompareBool_False_24 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_26 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_26;

	private bool logic_uScript_RemoveTech_Out_26 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_28 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_29 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_29 = new Tank[0];

	private int logic_uScript_AccessListTech_index_29;

	private Tank logic_uScript_AccessListTech_value_29;

	private bool logic_uScript_AccessListTech_Out_29 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_32 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_32;

	private bool logic_uScript_SetTechMarkerState_Out_32 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_33 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_33 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_34 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_34;

	private bool logic_uScriptAct_SetBool_Out_34 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_34 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_34 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_36 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_36;

	private bool logic_uScriptAct_SetBool_Out_36 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_36 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_36 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_38 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_38 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_39 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_40 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_40;

	private bool logic_uScript_IsTechAlive_Alive_40 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_40 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_41 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_41;

	private bool logic_uScript_IsTechAlive_Alive_41 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_41 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_42 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_96 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_96 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_4 || !m_RegisteredForEvents)
		{
			owner_Connection_4 = parentGameObject;
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
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
		logic_uScriptAct_SetBool_uScriptAct_SetBool_3.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_5.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_6.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_11.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_19.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_20.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_22.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_26.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_29.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_32.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_33.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_34.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_36.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_38.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_40.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_41.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_96.SetParent(g);
		owner_Connection_4 = parentGameObject;
		owner_Connection_14 = parentGameObject;
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
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_22.OnDisable();
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_32.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_40.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_41.OnDisable();
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
	public void In([FriendlyName("chargerSpawnData", "")] SpawnTechData[] chargerSpawnData, [FriendlyName("forcefieldSpawnData", "")] SpawnTechData[] forcefieldSpawnData)
	{
		external_16 = chargerSpawnData;
		external_17 = forcefieldSpawnData;
		Relay_In_11();
	}

	private void Relay_Show_0()
	{
		logic_uScript_SetTechMarkerState_tech_0 = local_ChargerTech01_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.Show(logic_uScript_SetTechMarkerState_tech_0);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.Out)
		{
			Relay_AtIndex_20();
		}
	}

	private void Relay_Hide_0()
	{
		logic_uScript_SetTechMarkerState_tech_0 = local_ChargerTech01_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.Hide(logic_uScript_SetTechMarkerState_tech_0);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.Out)
		{
			Relay_AtIndex_20();
		}
	}

	private void Relay_True_3()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_3.True(out logic_uScriptAct_SetBool_Target_3);
		local_ForcefieldEnabled_System_Boolean = logic_uScriptAct_SetBool_Target_3;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_3.Out)
		{
			Relay_AtIndex_5();
		}
	}

	private void Relay_False_3()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_3.False(out logic_uScriptAct_SetBool_Target_3);
		local_ForcefieldEnabled_System_Boolean = logic_uScriptAct_SetBool_Target_3;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_3.Out)
		{
			Relay_AtIndex_5();
		}
	}

	private void Relay_AtIndex_5()
	{
		int num = 0;
		Array array = local_ChargerTechData_TankArray;
		if (logic_uScript_AccessListTech_techList_5.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_5, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_5, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_5.AtIndex(ref logic_uScript_AccessListTech_techList_5, logic_uScript_AccessListTech_index_5, out logic_uScript_AccessListTech_value_5);
		local_ChargerTechData_TankArray = logic_uScript_AccessListTech_techList_5;
		local_ChargerTech01_Tank = logic_uScript_AccessListTech_value_5;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_5.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_6()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_6.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_6.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_8()
	{
		int num = 0;
		Array array = external_17;
		if (logic_uScript_GetAndCheckTechs_techData_8.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_8, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_8, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_8 = owner_Connection_4;
		int num2 = 0;
		Array array2 = local_Forcefields_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_8.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_8, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_8, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_8 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8.In(logic_uScript_GetAndCheckTechs_techData_8, logic_uScript_GetAndCheckTechs_ownerNode_8, ref logic_uScript_GetAndCheckTechs_techs_8);
		local_Forcefields_TankArray = logic_uScript_GetAndCheckTechs_techs_8;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_8.WaitingToSpawn;
		if (allAlive)
		{
			Relay_False_34();
		}
		if (someAlive)
		{
			Relay_False_34();
		}
		if (allDead)
		{
			Relay_True_36();
		}
		if (waitingToSpawn)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_11()
	{
		int num = 0;
		Array array = external_16;
		if (logic_uScript_GetAndCheckTechs_techData_11.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_11, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_11, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_11 = owner_Connection_14;
		int num2 = 0;
		Array array2 = local_ChargerTechData_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_11.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_11, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_11, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_11 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_11.In(logic_uScript_GetAndCheckTechs_techData_11, logic_uScript_GetAndCheckTechs_ownerNode_11, ref logic_uScript_GetAndCheckTechs_techs_11);
		local_ChargerTechData_TankArray = logic_uScript_GetAndCheckTechs_techs_11;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_11.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_11.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_11.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_11.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_3();
		}
		if (someAlive)
		{
			Relay_True_3();
		}
		if (allDead)
		{
			Relay_False_3();
		}
		if (waitingToSpawn)
		{
			Relay_In_19();
		}
	}

	private void Relay_Connection_15()
	{
	}

	private void Relay_Connection_16()
	{
	}

	private void Relay_Connection_17()
	{
	}

	private void Relay_Connection_18()
	{
		if (this.Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.chargersDestroyed = external_35;
			this.Out(this, e);
		}
	}

	private void Relay_In_19()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_19.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_19.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_AtIndex_20()
	{
		int num = 0;
		Array array = local_ChargerTechData_TankArray;
		if (logic_uScript_AccessListTech_techList_20.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_20, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_20, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_20.AtIndex(ref logic_uScript_AccessListTech_techList_20, logic_uScript_AccessListTech_index_20, out logic_uScript_AccessListTech_value_20);
		local_ChargerTechData_TankArray = logic_uScript_AccessListTech_techList_20;
		local_ChargerTech02_Tank = logic_uScript_AccessListTech_value_20;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_20.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_Show_22()
	{
		logic_uScript_SetTechMarkerState_tech_22 = local_ChargerTech02_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_22.Show(logic_uScript_SetTechMarkerState_tech_22);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_22.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_Hide_22()
	{
		logic_uScript_SetTechMarkerState_tech_22 = local_ChargerTech02_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_22.Hide(logic_uScript_SetTechMarkerState_tech_22);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_22.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_24()
	{
		logic_uScriptCon_CompareBool_Bool_24 = local_ForcefieldEnabled_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24.In(logic_uScriptCon_CompareBool_Bool_24);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24.False;
		if (num)
		{
			Relay_In_38();
		}
		if (flag)
		{
			Relay_In_26();
		}
	}

	private void Relay_In_26()
	{
		logic_uScript_RemoveTech_tech_26 = local_ForcefieldTech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_26.In(logic_uScript_RemoveTech_tech_26);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_26.Out)
		{
			Relay_Connection_18();
		}
	}

	private void Relay_In_28()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_AtIndex_29()
	{
		int num = 0;
		Array array = local_Forcefields_TankArray;
		if (logic_uScript_AccessListTech_techList_29.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_29, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_29, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_29.AtIndex(ref logic_uScript_AccessListTech_techList_29, logic_uScript_AccessListTech_index_29, out logic_uScript_AccessListTech_value_29);
		local_Forcefields_TankArray = logic_uScript_AccessListTech_techList_29;
		local_ForcefieldTech_Tank = logic_uScript_AccessListTech_value_29;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_29.Out)
		{
			Relay_Hide_32();
		}
	}

	private void Relay_Show_32()
	{
		logic_uScript_SetTechMarkerState_tech_32 = local_ForcefieldTech_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_32.Show(logic_uScript_SetTechMarkerState_tech_32);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_32.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_Hide_32()
	{
		logic_uScript_SetTechMarkerState_tech_32 = local_ForcefieldTech_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_32.Hide(logic_uScript_SetTechMarkerState_tech_32);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_32.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_33()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_33.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_33.Out)
		{
			Relay_Connection_18();
		}
	}

	private void Relay_True_34()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_34.True(out logic_uScriptAct_SetBool_Target_34);
		external_35 = logic_uScriptAct_SetBool_Target_34;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_34.Out)
		{
			Relay_AtIndex_29();
		}
	}

	private void Relay_False_34()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_34.False(out logic_uScriptAct_SetBool_Target_34);
		external_35 = logic_uScriptAct_SetBool_Target_34;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_34.Out)
		{
			Relay_AtIndex_29();
		}
	}

	private void Relay_Connection_35()
	{
	}

	private void Relay_True_36()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_36.True(out logic_uScriptAct_SetBool_Target_36);
		external_35 = logic_uScriptAct_SetBool_Target_36;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_36.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_False_36()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_36.False(out logic_uScriptAct_SetBool_Target_36);
		external_35 = logic_uScriptAct_SetBool_Target_36;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_36.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_38()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_38.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_38.Out)
		{
			Relay_Connection_18();
		}
	}

	private void Relay_In_39()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_In_40()
	{
		logic_uScript_IsTechAlive_tech_40 = local_ChargerTech01_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_40.In(logic_uScript_IsTechAlive_tech_40);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_40.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_40.Destroyed;
		if (alive)
		{
			Relay_Hide_0();
		}
		if (destroyed)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_41()
	{
		logic_uScript_IsTechAlive_tech_41 = local_ChargerTech02_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_41.In(logic_uScript_IsTechAlive_tech_41);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_41.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_41.Destroyed;
		if (alive)
		{
			Relay_Hide_22();
		}
		if (destroyed)
		{
			Relay_In_96();
		}
	}

	private void Relay_In_42()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42.Out)
		{
			Relay_AtIndex_20();
		}
	}

	private void Relay_In_96()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_96.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_96.Out)
		{
			Relay_In_8();
		}
	}
}
