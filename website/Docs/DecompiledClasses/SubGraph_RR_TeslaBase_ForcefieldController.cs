using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_RR_TeslaBase_ForcefieldController", "")]
public class SubGraph_RR_TeslaBase_ForcefieldController : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public Tank[] Forcefields = new Tank[0];
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_4 = new SpawnTechData[0];

	private Tank[] external_22 = new Tank[0];

	private Tank local_CurrentForcefield_Tank;

	private int local_CurrentForcefieldIndex_System_Int32;

	private GameObject owner_Connection_2;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_3 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_3 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_3;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_3 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_3;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_3 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_3 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_3 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_3 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_6 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_6 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_6;

	private bool logic_uScript_SetTankInvulnerable_Out_6 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_8 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_8;

	private bool logic_uScript_SetTechMarkerState_Out_8 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_12 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_12;

	private bool logic_uScript_IsTechAlive_Alive_12 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_12 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_13 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_13 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_13;

	private int logic_uScript_ForEachListTech_currentIndex_13;

	private bool logic_uScript_ForEachListTech_Immediate_13 = true;

	private bool logic_uScript_ForEachListTech_Done_13 = true;

	private bool logic_uScript_ForEachListTech_Iteration_13 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_14 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_14 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_15 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_15 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_16 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_16 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_17 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_19 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_19 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_20 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_20 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_21 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
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
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_3.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_6.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_8.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_12.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_13.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_14.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_15.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_16.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_19.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_20.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21.SetParent(g);
		owner_Connection_2 = parentGameObject;
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
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_6.OnDisable();
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_8.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_12.OnDisable();
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
	public void In([FriendlyName("forcefieldSpawnData", "")] SpawnTechData[] forcefieldSpawnData, [FriendlyName("Forcefields", "")] ref Tank[] Forcefields)
	{
		external_4 = forcefieldSpawnData;
		external_22 = Forcefields;
		Relay_In_3();
	}

	private void Relay_Connection_0()
	{
	}

	private void Relay_Connection_1()
	{
		if (this.Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.Forcefields = external_22;
			this.Out(this, e);
		}
	}

	private void Relay_In_3()
	{
		int num = 0;
		Array array = external_4;
		if (logic_uScript_GetAndCheckTechs_techData_3.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_3, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_3, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_3 = owner_Connection_2;
		int num2 = 0;
		Array array2 = external_22;
		if (logic_uScript_GetAndCheckTechs_techs_3.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_3, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_3, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_3 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_3.In(logic_uScript_GetAndCheckTechs_techData_3, logic_uScript_GetAndCheckTechs_ownerNode_3, ref logic_uScript_GetAndCheckTechs_techs_3);
		external_22 = logic_uScript_GetAndCheckTechs_techs_3;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_3.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_3.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_3.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_3.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_13();
		}
		if (someAlive)
		{
			Relay_In_13();
		}
		if (allDead)
		{
			Relay_In_17();
		}
		if (waitingToSpawn)
		{
			Relay_In_13();
		}
	}

	private void Relay_Connection_4()
	{
	}

	private void Relay_In_6()
	{
		logic_uScript_SetTankInvulnerable_tank_6 = local_CurrentForcefield_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_6.In(logic_uScript_SetTankInvulnerable_invulnerable_6, logic_uScript_SetTankInvulnerable_tank_6);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_6.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_Show_8()
	{
		logic_uScript_SetTechMarkerState_tech_8 = local_CurrentForcefield_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_8.Show(logic_uScript_SetTechMarkerState_tech_8);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_8.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_Hide_8()
	{
		logic_uScript_SetTechMarkerState_tech_8 = local_CurrentForcefield_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_8.Hide(logic_uScript_SetTechMarkerState_tech_8);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_8.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_IsTechAlive_tech_12 = local_CurrentForcefield_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_12.In(logic_uScript_IsTechAlive_tech_12);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_12.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_12.Destroyed;
		if (alive)
		{
			Relay_Hide_8();
		}
		if (destroyed)
		{
			Relay_In_16();
		}
	}

	private void Relay_Reset_13()
	{
		int num = 0;
		Array array = external_22;
		if (logic_uScript_ForEachListTech_List_13.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_13, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_13, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_13.Reset(logic_uScript_ForEachListTech_List_13, out logic_uScript_ForEachListTech_Value_13, out logic_uScript_ForEachListTech_currentIndex_13);
		local_CurrentForcefield_Tank = logic_uScript_ForEachListTech_Value_13;
		local_CurrentForcefieldIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_13;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_13.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_13.Iteration;
		if (done)
		{
			Relay_In_19();
		}
		if (iteration)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_13()
	{
		int num = 0;
		Array array = external_22;
		if (logic_uScript_ForEachListTech_List_13.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_13, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_13, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_13.In(logic_uScript_ForEachListTech_List_13, out logic_uScript_ForEachListTech_Value_13, out logic_uScript_ForEachListTech_currentIndex_13);
		local_CurrentForcefield_Tank = logic_uScript_ForEachListTech_Value_13;
		local_CurrentForcefieldIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_13;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_13.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_13.Iteration;
		if (done)
		{
			Relay_In_19();
		}
		if (iteration)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_14()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_14.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_14.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_15()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_15.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_15.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_16()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_16.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_16.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_17()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_19()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_19.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_19.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_20()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_20.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_20.Out)
		{
			Relay_Connection_1();
		}
	}

	private void Relay_In_21()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_Connection_22()
	{
	}
}
