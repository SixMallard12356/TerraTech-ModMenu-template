using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_KillEmAll_04", "")]
[NodePath("Graphs")]
public class SubGraph_KillEmAll_04 : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_4 = new SpawnTechData[0];

	private SpawnTechData[] external_11 = new SpawnTechData[0];

	private SpawnTechData[] external_15 = new SpawnTechData[0];

	private SpawnTechData[] external_18 = new SpawnTechData[0];

	private Tank local_ghostTech01_Tank;

	private Tank local_ghostTech02_Tank;

	private Tank local_ghostTech03_Tank;

	private Tank local_ghostTech04_Tank;

	private Tank[] local_ghostTechs01_TankArray = new Tank[0];

	private Tank[] local_ghostTechs02_TankArray = new Tank[0];

	private Tank[] local_ghostTechs03_TankArray = new Tank[0];

	private Tank[] local_ghostTechs04_TankArray = new Tank[0];

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_21;

	private GameObject owner_Connection_30;

	private GameObject owner_Connection_37;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_0 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_0;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_0 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_0;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_0 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_0 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_0 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_0 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_6 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_6 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_6 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_6;

	private bool logic_uScript_DestroyTechsFromData_Out_6 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_7 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_7 = new Tank[0];

	private int logic_uScript_AccessListTech_index_7;

	private Tank logic_uScript_AccessListTech_value_7;

	private bool logic_uScript_AccessListTech_Out_7 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_9;

	private Tank logic_uScript_SetTankInvulnerable_tank_9;

	private bool logic_uScript_SetTankInvulnerable_Out_9 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_13 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_13 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_13 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_13;

	private bool logic_uScript_DestroyTechsFromData_Out_13 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_16 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_16 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_16 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_16;

	private bool logic_uScript_DestroyTechsFromData_Out_16 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_19 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_19 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_19 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_19;

	private bool logic_uScript_DestroyTechsFromData_Out_19 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_20 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_20 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_22 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_22;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_22 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_22;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_22 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_22 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_22 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_22 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_23 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_23;

	private Tank logic_uScript_SetTankInvulnerable_tank_23;

	private bool logic_uScript_SetTankInvulnerable_Out_23 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_24 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_24 = new Tank[0];

	private int logic_uScript_AccessListTech_index_24;

	private Tank logic_uScript_AccessListTech_value_24;

	private bool logic_uScript_AccessListTech_Out_24 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_25 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_25 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_28 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_29 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_29 = new Tank[0];

	private int logic_uScript_AccessListTech_index_29;

	private Tank logic_uScript_AccessListTech_value_29;

	private bool logic_uScript_AccessListTech_Out_29 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_32 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_32;

	private Tank logic_uScript_SetTankInvulnerable_tank_32;

	private bool logic_uScript_SetTankInvulnerable_Out_32 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_33 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_33;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_33 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_33;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_33 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_33 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_33 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_33 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_35 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_36 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_36 = new Tank[0];

	private int logic_uScript_AccessListTech_index_36;

	private Tank logic_uScript_AccessListTech_value_36;

	private bool logic_uScript_AccessListTech_Out_36 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_38 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_38;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_38 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_38;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_38 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_38 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_38 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_38 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_39 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_39;

	private Tank logic_uScript_SetTankInvulnerable_tank_39;

	private bool logic_uScript_SetTankInvulnerable_Out_39 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_5 || !m_RegisteredForEvents)
		{
			owner_Connection_5 = parentGameObject;
		}
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
		}
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
		}
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
		}
		if (null == owner_Connection_21 || !m_RegisteredForEvents)
		{
			owner_Connection_21 = parentGameObject;
		}
		if (null == owner_Connection_30 || !m_RegisteredForEvents)
		{
			owner_Connection_30 = parentGameObject;
		}
		if (null == owner_Connection_37 || !m_RegisteredForEvents)
		{
			owner_Connection_37 = parentGameObject;
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
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_6.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_7.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_13.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_16.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_19.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_20.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_23.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_24.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_25.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_29.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_32.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_36.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_39.SetParent(g);
		owner_Connection_5 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_21 = parentGameObject;
		owner_Connection_30 = parentGameObject;
		owner_Connection_37 = parentGameObject;
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
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_23.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_32.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_39.OnDisable();
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
	public void In([FriendlyName("ghostTechSpawnData01", "")] SpawnTechData[] ghostTechSpawnData01, [FriendlyName("ghostTechSpawnData02", "")] SpawnTechData[] ghostTechSpawnData02, [FriendlyName("ghostTechSpawnData03", "")] SpawnTechData[] ghostTechSpawnData03, [FriendlyName("ghostTechSpawnData04", "")] SpawnTechData[] ghostTechSpawnData04)
	{
		external_4 = ghostTechSpawnData01;
		external_11 = ghostTechSpawnData02;
		external_15 = ghostTechSpawnData03;
		external_18 = ghostTechSpawnData04;
		Relay_In_0();
	}

	private void Relay_In_0()
	{
		int num = 0;
		Array array = external_4;
		if (logic_uScript_GetAndCheckTechs_techData_0.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_0, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_0, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_0 = owner_Connection_5;
		int num2 = 0;
		Array array2 = local_ghostTechs01_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_0.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_0, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_0, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_0 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.In(logic_uScript_GetAndCheckTechs_techData_0, logic_uScript_GetAndCheckTechs_ownerNode_0, ref logic_uScript_GetAndCheckTechs_techs_0);
		local_ghostTechs01_TankArray = logic_uScript_GetAndCheckTechs_techs_0;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_7();
		}
		if (someAlive)
		{
			Relay_AtIndex_7();
		}
		if (allDead)
		{
			Relay_In_20();
		}
		if (waitingToSpawn)
		{
			Relay_In_20();
		}
	}

	private void Relay_Connection_2()
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

	private void Relay_In_6()
	{
		int num = 0;
		Array array = external_4;
		if (logic_uScript_DestroyTechsFromData_techData_6.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_6, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_6, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_6 = owner_Connection_8;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_6.In(logic_uScript_DestroyTechsFromData_techData_6, logic_uScript_DestroyTechsFromData_shouldExplode_6, logic_uScript_DestroyTechsFromData_ownerNode_6);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_6.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_AtIndex_7()
	{
		int num = 0;
		Array array = local_ghostTechs01_TankArray;
		if (logic_uScript_AccessListTech_techList_7.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_7, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_7, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_7.AtIndex(ref logic_uScript_AccessListTech_techList_7, logic_uScript_AccessListTech_index_7, out logic_uScript_AccessListTech_value_7);
		local_ghostTechs01_TankArray = logic_uScript_AccessListTech_techList_7;
		local_ghostTech01_Tank = logic_uScript_AccessListTech_value_7;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_7.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_9()
	{
		logic_uScript_SetTankInvulnerable_tank_9 = local_ghostTech01_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9.In(logic_uScript_SetTankInvulnerable_invulnerable_9, logic_uScript_SetTankInvulnerable_tank_9);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_Connection_10()
	{
	}

	private void Relay_Connection_11()
	{
	}

	private void Relay_In_13()
	{
		int num = 0;
		Array array = external_11;
		if (logic_uScript_DestroyTechsFromData_techData_13.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_13, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_13, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_13 = owner_Connection_12;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_13.In(logic_uScript_DestroyTechsFromData_techData_13, logic_uScript_DestroyTechsFromData_shouldExplode_13, logic_uScript_DestroyTechsFromData_ownerNode_13);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_13.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_Connection_15()
	{
	}

	private void Relay_In_16()
	{
		int num = 0;
		Array array = external_18;
		if (logic_uScript_DestroyTechsFromData_techData_16.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_16, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_16, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_16 = owner_Connection_14;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_16.In(logic_uScript_DestroyTechsFromData_techData_16, logic_uScript_DestroyTechsFromData_shouldExplode_16, logic_uScript_DestroyTechsFromData_ownerNode_16);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_16.Out)
		{
			Relay_Connection_2();
		}
	}

	private void Relay_Connection_18()
	{
	}

	private void Relay_In_19()
	{
		int num = 0;
		Array array = external_15;
		if (logic_uScript_DestroyTechsFromData_techData_19.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_19, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_19, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_19 = owner_Connection_17;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_19.In(logic_uScript_DestroyTechsFromData_techData_19, logic_uScript_DestroyTechsFromData_shouldExplode_19, logic_uScript_DestroyTechsFromData_ownerNode_19);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_19.Out)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_20()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_20.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_20.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_In_22()
	{
		int num = 0;
		Array array = external_11;
		if (logic_uScript_GetAndCheckTechs_techData_22.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_22, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_22, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_22 = owner_Connection_21;
		int num2 = 0;
		Array array2 = local_ghostTechs02_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_22.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_22, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_22, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_22 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.In(logic_uScript_GetAndCheckTechs_techData_22, logic_uScript_GetAndCheckTechs_ownerNode_22, ref logic_uScript_GetAndCheckTechs_techs_22);
		local_ghostTechs02_TankArray = logic_uScript_GetAndCheckTechs_techs_22;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_24();
		}
		if (someAlive)
		{
			Relay_AtIndex_24();
		}
		if (allDead)
		{
			Relay_In_25();
		}
		if (waitingToSpawn)
		{
			Relay_In_25();
		}
	}

	private void Relay_In_23()
	{
		logic_uScript_SetTankInvulnerable_tank_23 = local_ghostTech02_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_23.In(logic_uScript_SetTankInvulnerable_invulnerable_23, logic_uScript_SetTankInvulnerable_tank_23);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_23.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_AtIndex_24()
	{
		int num = 0;
		Array array = local_ghostTechs02_TankArray;
		if (logic_uScript_AccessListTech_techList_24.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_24, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_24, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_24.AtIndex(ref logic_uScript_AccessListTech_techList_24, logic_uScript_AccessListTech_index_24, out logic_uScript_AccessListTech_value_24);
		local_ghostTechs02_TankArray = logic_uScript_AccessListTech_techList_24;
		local_ghostTech02_Tank = logic_uScript_AccessListTech_value_24;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_24.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_25()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_25.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_25.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_28()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_AtIndex_29()
	{
		int num = 0;
		Array array = local_ghostTechs03_TankArray;
		if (logic_uScript_AccessListTech_techList_29.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_29, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_29, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_29.AtIndex(ref logic_uScript_AccessListTech_techList_29, logic_uScript_AccessListTech_index_29, out logic_uScript_AccessListTech_value_29);
		local_ghostTechs03_TankArray = logic_uScript_AccessListTech_techList_29;
		local_ghostTech03_Tank = logic_uScript_AccessListTech_value_29;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_29.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_32()
	{
		logic_uScript_SetTankInvulnerable_tank_32 = local_ghostTech03_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_32.In(logic_uScript_SetTankInvulnerable_invulnerable_32, logic_uScript_SetTankInvulnerable_tank_32);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_32.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_33()
	{
		int num = 0;
		Array array = external_15;
		if (logic_uScript_GetAndCheckTechs_techData_33.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_33, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_33, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_33 = owner_Connection_30;
		int num2 = 0;
		Array array2 = local_ghostTechs03_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_33.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_33, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_33, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_33 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.In(logic_uScript_GetAndCheckTechs_techData_33, logic_uScript_GetAndCheckTechs_ownerNode_33, ref logic_uScript_GetAndCheckTechs_techs_33);
		local_ghostTechs03_TankArray = logic_uScript_GetAndCheckTechs_techs_33;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_29();
		}
		if (someAlive)
		{
			Relay_AtIndex_29();
		}
		if (allDead)
		{
			Relay_In_28();
		}
		if (waitingToSpawn)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_35()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_AtIndex_36()
	{
		int num = 0;
		Array array = local_ghostTechs04_TankArray;
		if (logic_uScript_AccessListTech_techList_36.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_36, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_36, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_36.AtIndex(ref logic_uScript_AccessListTech_techList_36, logic_uScript_AccessListTech_index_36, out logic_uScript_AccessListTech_value_36);
		local_ghostTechs04_TankArray = logic_uScript_AccessListTech_techList_36;
		local_ghostTech04_Tank = logic_uScript_AccessListTech_value_36;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_36.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_38()
	{
		int num = 0;
		Array array = external_18;
		if (logic_uScript_GetAndCheckTechs_techData_38.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_38, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_38, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_38 = owner_Connection_37;
		int num2 = 0;
		Array array2 = local_ghostTechs04_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_38.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_38, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_38, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_38 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.In(logic_uScript_GetAndCheckTechs_techData_38, logic_uScript_GetAndCheckTechs_ownerNode_38, ref logic_uScript_GetAndCheckTechs_techs_38);
		local_ghostTechs04_TankArray = logic_uScript_GetAndCheckTechs_techs_38;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_36();
		}
		if (someAlive)
		{
			Relay_AtIndex_36();
		}
		if (allDead)
		{
			Relay_In_35();
		}
		if (waitingToSpawn)
		{
			Relay_In_35();
		}
	}

	private void Relay_In_39()
	{
		logic_uScript_SetTankInvulnerable_tank_39 = local_ghostTech04_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_39.In(logic_uScript_SetTankInvulnerable_invulnerable_39, logic_uScript_SetTankInvulnerable_tank_39);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_39.Out)
		{
			Relay_In_16();
		}
	}
}
