using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_EscortAlliedTechs_SpawnEnemyGroup", "")]
[NodePath("Graphs")]
public class SubGraph_EscortAlliedTechs_SpawnEnemyGroup : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private float[] external_7 = new float[0];

	private Tank external_9;

	private int external_6;

	private float local_1_System_Single;

	private int local_4_System_Int32;

	private uScriptAct_AccessListFloat logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_0 = new uScriptAct_AccessListFloat();

	private float[] logic_uScriptAct_AccessListFloat_FloatList_0 = new float[0];

	private int logic_uScriptAct_AccessListFloat_Index_0;

	private float logic_uScriptAct_AccessListFloat_Value_0;

	private bool logic_uScriptAct_AccessListFloat_Out_0 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_2 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_2;

	private int logic_uScriptAct_AddInt_v2_B_2 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_2;

	private float logic_uScriptAct_AddInt_v2_FloatResult_2;

	private bool logic_uScriptAct_AddInt_v2_Out_2 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_3 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_3;

	private float logic_uScript_IsPlayerInRangeOfTech_range_3;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_3 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_3 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_3 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_3 = true;

	private uScript_BlankNode logic_uScript_BlankNode_uScript_BlankNode_10 = new uScript_BlankNode();

	private bool logic_uScript_BlankNode_Out_10 = true;

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
		logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_0.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_2.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_3.SetParent(g);
		logic_uScript_BlankNode_uScript_BlankNode_10.SetParent(g);
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
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_3.OnDisable();
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
	public void In([FriendlyName("enemySpawnThresholds", "")] float[] enemySpawnThresholds, [FriendlyName("DestinationObject", "")] Tank DestinationObject, [FriendlyName("SpawnCounter", "")] int SpawnCounter)
	{
		external_7 = enemySpawnThresholds;
		external_9 = DestinationObject;
		external_6 = SpawnCounter;
		Relay_In_2();
	}

	private void Relay_First_0()
	{
		int num = 0;
		Array array = external_7;
		if (logic_uScriptAct_AccessListFloat_FloatList_0.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListFloat_FloatList_0, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListFloat_FloatList_0, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListFloat_Index_0 = local_4_System_Int32;
		logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_0.First(logic_uScriptAct_AccessListFloat_FloatList_0, logic_uScriptAct_AccessListFloat_Index_0, out logic_uScriptAct_AccessListFloat_Value_0);
		local_1_System_Single = logic_uScriptAct_AccessListFloat_Value_0;
		if (logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_0.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_Last_0()
	{
		int num = 0;
		Array array = external_7;
		if (logic_uScriptAct_AccessListFloat_FloatList_0.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListFloat_FloatList_0, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListFloat_FloatList_0, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListFloat_Index_0 = local_4_System_Int32;
		logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_0.Last(logic_uScriptAct_AccessListFloat_FloatList_0, logic_uScriptAct_AccessListFloat_Index_0, out logic_uScriptAct_AccessListFloat_Value_0);
		local_1_System_Single = logic_uScriptAct_AccessListFloat_Value_0;
		if (logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_0.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_Random_0()
	{
		int num = 0;
		Array array = external_7;
		if (logic_uScriptAct_AccessListFloat_FloatList_0.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListFloat_FloatList_0, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListFloat_FloatList_0, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListFloat_Index_0 = local_4_System_Int32;
		logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_0.Random(logic_uScriptAct_AccessListFloat_FloatList_0, logic_uScriptAct_AccessListFloat_Index_0, out logic_uScriptAct_AccessListFloat_Value_0);
		local_1_System_Single = logic_uScriptAct_AccessListFloat_Value_0;
		if (logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_0.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_AtIndex_0()
	{
		int num = 0;
		Array array = external_7;
		if (logic_uScriptAct_AccessListFloat_FloatList_0.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListFloat_FloatList_0, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListFloat_FloatList_0, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListFloat_Index_0 = local_4_System_Int32;
		logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_0.AtIndex(logic_uScriptAct_AccessListFloat_FloatList_0, logic_uScriptAct_AccessListFloat_Index_0, out logic_uScriptAct_AccessListFloat_Value_0);
		local_1_System_Single = logic_uScriptAct_AccessListFloat_Value_0;
		if (logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_0.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_In_2()
	{
		logic_uScriptAct_AddInt_v2_A_2 = external_6;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_2.In(logic_uScriptAct_AddInt_v2_A_2, logic_uScriptAct_AddInt_v2_B_2, out logic_uScriptAct_AddInt_v2_IntResult_2, out logic_uScriptAct_AddInt_v2_FloatResult_2);
		local_4_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_2;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_2.Out)
		{
			Relay_AtIndex_0();
		}
	}

	private void Relay_In_3()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_3 = external_9;
		logic_uScript_IsPlayerInRangeOfTech_range_3 = local_1_System_Single;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_3.In(logic_uScript_IsPlayerInRangeOfTech_tech_3, logic_uScript_IsPlayerInRangeOfTech_range_3, logic_uScript_IsPlayerInRangeOfTech_techs_3);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_3.InRange)
		{
			Relay_In_10();
		}
	}

	private void Relay_Connection_5()
	{
	}

	private void Relay_Connection_6()
	{
	}

	private void Relay_Connection_7()
	{
	}

	private void Relay_Connection_8()
	{
		if (this.Out != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Out(this, args);
		}
	}

	private void Relay_Connection_9()
	{
	}

	private void Relay_In_10()
	{
		logic_uScript_BlankNode_uScript_BlankNode_10.In();
		if (logic_uScript_BlankNode_uScript_BlankNode_10.Out)
		{
			Relay_Connection_8();
		}
	}
}
