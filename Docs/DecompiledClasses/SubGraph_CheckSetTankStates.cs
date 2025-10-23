using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_CheckSetTankStates", "")]
[NodePath("Graphs")]
public class SubGraph_CheckSetTankStates : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public Tank[] techs = new Tank[0];

		public Tank tank;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private Tank[] external_4 = new Tank[0];

	private int external_6;

	private Tank external_3;

	private bool external_18;

	private bool external_19;

	private bool external_20;

	private bool external_21;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_0;

	private bool logic_uScript_SetTechMarkerState_Out_0 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_1 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_1 = new Tank[0];

	private int logic_uScript_AccessListTech_index_1;

	private Tank logic_uScript_AccessListTech_value_1;

	private bool logic_uScript_AccessListTech_Out_1 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_5;

	private bool logic_uScriptCon_CompareBool_True_5 = true;

	private bool logic_uScriptCon_CompareBool_False_5 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_7 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_7 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_7;

	private bool logic_uScript_SetTankInvulnerable_Out_7 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_8;

	private bool logic_uScriptCon_CompareBool_True_8 = true;

	private bool logic_uScriptCon_CompareBool_False_8 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_9 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_9 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_10 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_10 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_11 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_11 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_12;

	private bool logic_uScriptCon_CompareBool_True_12 = true;

	private bool logic_uScriptCon_CompareBool_False_12 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_13;

	private bool logic_uScriptCon_CompareBool_True_13 = true;

	private bool logic_uScriptCon_CompareBool_False_13 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_14 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_14;

	private Tank logic_uScript_SetTankInvulnerable_tank_14;

	private bool logic_uScript_SetTankInvulnerable_Out_14 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_15 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_15 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_17 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
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
		logic_uScript_AccessListTech_uScript_AccessListTech_1.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_7.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_9.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_10.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_11.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_14.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_15.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.SetParent(g);
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
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_7.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_14.OnDisable();
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
	public void In([FriendlyName("techs", "")] ref Tank[] techs, [FriendlyName("Index", "")] int Index, [FriendlyName("tank", "")] ref Tank tank, [FriendlyName("Set Tank Invul", "")] bool Set_Tank_Invul, [FriendlyName("Invul", "")] bool Invul, [FriendlyName("Set Marker Visibility", "")] bool Set_Marker_Visibility, [FriendlyName("Marker", "")] bool Marker)
	{
		external_4 = techs;
		external_6 = Index;
		external_3 = tank;
		external_18 = Set_Tank_Invul;
		external_19 = Invul;
		external_20 = Set_Marker_Visibility;
		external_21 = Marker;
		Relay_AtIndex_1();
	}

	private void Relay_Show_0()
	{
		logic_uScript_SetTechMarkerState_tech_0 = external_3;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.Show(logic_uScript_SetTechMarkerState_tech_0);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.Out)
		{
			Relay_In_17();
		}
	}

	private void Relay_Hide_0()
	{
		logic_uScript_SetTechMarkerState_tech_0 = external_3;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.Hide(logic_uScript_SetTechMarkerState_tech_0);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_0.Out)
		{
			Relay_In_17();
		}
	}

	private void Relay_AtIndex_1()
	{
		int num = 0;
		Array array = external_4;
		if (logic_uScript_AccessListTech_techList_1.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_1, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_1, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_index_1 = external_6;
		logic_uScript_AccessListTech_uScript_AccessListTech_1.AtIndex(ref logic_uScript_AccessListTech_techList_1, logic_uScript_AccessListTech_index_1, out logic_uScript_AccessListTech_value_1);
		external_4 = logic_uScript_AccessListTech_techList_1;
		external_3 = logic_uScript_AccessListTech_value_1;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_1.Out)
		{
			Relay_In_13();
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
	}

	private void Relay_In_5()
	{
		logic_uScriptCon_CompareBool_Bool_5 = external_20;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.In(logic_uScriptCon_CompareBool_Bool_5);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.False;
		if (num)
		{
			Relay_In_8();
		}
		if (flag)
		{
			Relay_In_9();
		}
	}

	private void Relay_Connection_6()
	{
	}

	private void Relay_In_7()
	{
		logic_uScript_SetTankInvulnerable_tank_7 = external_3;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_7.In(logic_uScript_SetTankInvulnerable_invulnerable_7, logic_uScript_SetTankInvulnerable_tank_7);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_7.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_8()
	{
		logic_uScriptCon_CompareBool_Bool_8 = external_21;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.In(logic_uScriptCon_CompareBool_Bool_8);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.False;
		if (num)
		{
			Relay_Show_0();
		}
		if (flag)
		{
			Relay_Hide_0();
		}
	}

	private void Relay_In_9()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_9.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_9.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_10()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_10.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_10.Out)
		{
			Relay_In_17();
		}
	}

	private void Relay_In_11()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_11.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_11.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_12()
	{
		logic_uScriptCon_CompareBool_Bool_12 = external_19;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.In(logic_uScriptCon_CompareBool_Bool_12);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.False;
		if (num)
		{
			Relay_In_7();
		}
		if (flag)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_13()
	{
		logic_uScriptCon_CompareBool_Bool_13 = external_18;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.In(logic_uScriptCon_CompareBool_Bool_13);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.False;
		if (num)
		{
			Relay_In_12();
		}
		if (flag)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_14()
	{
		logic_uScript_SetTankInvulnerable_tank_14 = external_3;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_14.In(logic_uScript_SetTankInvulnerable_invulnerable_14, logic_uScript_SetTankInvulnerable_tank_14);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_14.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_15()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_15.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_15.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_Connection_16()
	{
		if (this.Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.techs = external_4;
			e.tank = external_3;
			this.Out(this, e);
		}
	}

	private void Relay_In_17()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.Out)
		{
			Relay_Connection_16();
		}
	}

	private void Relay_Connection_18()
	{
	}

	private void Relay_Connection_19()
	{
	}

	private void Relay_Connection_20()
	{
	}

	private void Relay_Connection_21()
	{
	}
}
