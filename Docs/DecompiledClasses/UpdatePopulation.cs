using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("GSO_1X - Vendor: 0 Main", "")]
public class UpdatePopulation : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool PopSize1TinyFlee;

	public bool PopSize1TinyGuard;

	public bool PopSize2LittleFlee;

	public bool PopSize2LittleGuard;

	public bool PopSize3MediumFlee;

	public bool PopSize3MediumGuard;

	public bool PopSize4LargeGuard;

	public bool PopSize5HugeGuard;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_35;

	private uScript_SetPopulationTypeEnabled logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_2 = new uScript_SetPopulationTypeEnabled();

	private string logic_uScript_SetPopulationTypeEnabled_popTypeName_2 = "PopSize1TinyGuard";

	private bool logic_uScript_SetPopulationTypeEnabled_active_2;

	private bool logic_uScript_SetPopulationTypeEnabled_Out_2 = true;

	private uScript_SetPopulationTypeEnabled logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_5 = new uScript_SetPopulationTypeEnabled();

	private string logic_uScript_SetPopulationTypeEnabled_popTypeName_5 = "PopSize5HugeGuard";

	private bool logic_uScript_SetPopulationTypeEnabled_active_5;

	private bool logic_uScript_SetPopulationTypeEnabled_Out_5 = true;

	private uScript_SetPopulationTypeEnabled logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_8 = new uScript_SetPopulationTypeEnabled();

	private string logic_uScript_SetPopulationTypeEnabled_popTypeName_8 = "PopSize4LargeGuard";

	private bool logic_uScript_SetPopulationTypeEnabled_active_8;

	private bool logic_uScript_SetPopulationTypeEnabled_Out_8 = true;

	private uScript_SetPopulationTypeEnabled logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_10 = new uScript_SetPopulationTypeEnabled();

	private string logic_uScript_SetPopulationTypeEnabled_popTypeName_10 = "PopSize3MediumGuard";

	private bool logic_uScript_SetPopulationTypeEnabled_active_10;

	private bool logic_uScript_SetPopulationTypeEnabled_Out_10 = true;

	private uScript_SetPopulationTypeEnabled logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_11 = new uScript_SetPopulationTypeEnabled();

	private string logic_uScript_SetPopulationTypeEnabled_popTypeName_11 = "PopSize2LittleGuard";

	private bool logic_uScript_SetPopulationTypeEnabled_active_11;

	private bool logic_uScript_SetPopulationTypeEnabled_Out_11 = true;

	private uScript_SetPopulationTypeEnabled logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_13 = new uScript_SetPopulationTypeEnabled();

	private string logic_uScript_SetPopulationTypeEnabled_popTypeName_13 = "PopSize1TinyFlee";

	private bool logic_uScript_SetPopulationTypeEnabled_active_13;

	private bool logic_uScript_SetPopulationTypeEnabled_Out_13 = true;

	private uScript_SetPopulationTypeEnabled logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_14 = new uScript_SetPopulationTypeEnabled();

	private string logic_uScript_SetPopulationTypeEnabled_popTypeName_14 = "PopSize2LittleFlee";

	private bool logic_uScript_SetPopulationTypeEnabled_active_14;

	private bool logic_uScript_SetPopulationTypeEnabled_Out_14 = true;

	private uScript_SetPopulationTypeEnabled logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_15 = new uScript_SetPopulationTypeEnabled();

	private string logic_uScript_SetPopulationTypeEnabled_popTypeName_15 = "PopSize3MediumFlee";

	private bool logic_uScript_SetPopulationTypeEnabled_active_15;

	private bool logic_uScript_SetPopulationTypeEnabled_Out_15 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_36 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_36;

	private bool logic_uScript_FinishEncounter_Out_36 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
			if (null != owner_Connection_0)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_0.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
				}
			}
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_0)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_0.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_0)
		{
			uScript_EncounterUpdate component = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_1;
				component.OnSuspend -= Instance_OnSuspend_1;
				component.OnResume -= Instance_OnResume_1;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_2.SetParent(g);
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_5.SetParent(g);
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_8.SetParent(g);
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_10.SetParent(g);
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_11.SetParent(g);
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_13.SetParent(g);
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_14.SetParent(g);
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_15.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_36.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_35 = parentGameObject;
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

	private void Instance_OnUpdate_1(object o, EventArgs e)
	{
		Relay_OnUpdate_1();
	}

	private void Instance_OnSuspend_1(object o, EventArgs e)
	{
		Relay_OnSuspend_1();
	}

	private void Instance_OnResume_1(object o, EventArgs e)
	{
		Relay_OnResume_1();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_SetEnabled_2();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_SetEnabled_2()
	{
		logic_uScript_SetPopulationTypeEnabled_active_2 = PopSize1TinyGuard;
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_2.SetEnabled(logic_uScript_SetPopulationTypeEnabled_popTypeName_2, logic_uScript_SetPopulationTypeEnabled_active_2);
		if (logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_2.Out)
		{
			Relay_SetEnabled_11();
		}
	}

	private void Relay_SetEnabled_5()
	{
		logic_uScript_SetPopulationTypeEnabled_active_5 = PopSize5HugeGuard;
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_5.SetEnabled(logic_uScript_SetPopulationTypeEnabled_popTypeName_5, logic_uScript_SetPopulationTypeEnabled_active_5);
		if (logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_5.Out)
		{
			Relay_SetEnabled_13();
		}
	}

	private void Relay_SetEnabled_8()
	{
		logic_uScript_SetPopulationTypeEnabled_active_8 = PopSize4LargeGuard;
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_8.SetEnabled(logic_uScript_SetPopulationTypeEnabled_popTypeName_8, logic_uScript_SetPopulationTypeEnabled_active_8);
		if (logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_8.Out)
		{
			Relay_SetEnabled_5();
		}
	}

	private void Relay_SetEnabled_10()
	{
		logic_uScript_SetPopulationTypeEnabled_active_10 = PopSize3MediumGuard;
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_10.SetEnabled(logic_uScript_SetPopulationTypeEnabled_popTypeName_10, logic_uScript_SetPopulationTypeEnabled_active_10);
		if (logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_10.Out)
		{
			Relay_SetEnabled_8();
		}
	}

	private void Relay_SetEnabled_11()
	{
		logic_uScript_SetPopulationTypeEnabled_active_11 = PopSize2LittleGuard;
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_11.SetEnabled(logic_uScript_SetPopulationTypeEnabled_popTypeName_11, logic_uScript_SetPopulationTypeEnabled_active_11);
		if (logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_11.Out)
		{
			Relay_SetEnabled_10();
		}
	}

	private void Relay_SetEnabled_13()
	{
		logic_uScript_SetPopulationTypeEnabled_active_13 = PopSize1TinyFlee;
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_13.SetEnabled(logic_uScript_SetPopulationTypeEnabled_popTypeName_13, logic_uScript_SetPopulationTypeEnabled_active_13);
		if (logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_13.Out)
		{
			Relay_SetEnabled_14();
		}
	}

	private void Relay_SetEnabled_14()
	{
		logic_uScript_SetPopulationTypeEnabled_active_14 = PopSize2LittleFlee;
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_14.SetEnabled(logic_uScript_SetPopulationTypeEnabled_popTypeName_14, logic_uScript_SetPopulationTypeEnabled_active_14);
		if (logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_14.Out)
		{
			Relay_SetEnabled_15();
		}
	}

	private void Relay_SetEnabled_15()
	{
		logic_uScript_SetPopulationTypeEnabled_active_15 = PopSize3MediumFlee;
		logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_15.SetEnabled(logic_uScript_SetPopulationTypeEnabled_popTypeName_15, logic_uScript_SetPopulationTypeEnabled_active_15);
		if (logic_uScript_SetPopulationTypeEnabled_uScript_SetPopulationTypeEnabled_15.Out)
		{
			Relay_Succeed_36();
		}
	}

	private void Relay_Succeed_36()
	{
		logic_uScript_FinishEncounter_owner_36 = owner_Connection_35;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_36.Succeed(logic_uScript_FinishEncounter_owner_36);
	}

	private void Relay_Fail_36()
	{
		logic_uScript_FinishEncounter_owner_36 = owner_Connection_35;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_36.Fail(logic_uScript_FinishEncounter_owner_36);
	}
}
