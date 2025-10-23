using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_BaseBomb_ShowQuestLog", "")]
public class SubGraph_BaseBomb_ShowQuestLog : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public bool Flag_QuestLogShown;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private bool external_7;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_4;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_1 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_1;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_1 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_1 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_1 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_2;

	private bool logic_uScriptCon_CompareBool_True_2 = true;

	private bool logic_uScriptCon_CompareBool_False_2 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_3 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_3;

	private bool logic_uScriptAct_SetBool_Out_3 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_3 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_3 = true;

	private uScript_SetQuestLogVisible logic_uScript_SetQuestLogVisible_uScript_SetQuestLogVisible_9 = new uScript_SetQuestLogVisible();

	private bool logic_uScript_SetQuestLogVisible_visible_9 = true;

	private bool logic_uScript_SetQuestLogVisible_Out_9 = true;

	private uScript_IsCoreEncounterCompleted logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_10 = new uScript_IsCoreEncounterCompleted();

	private FactionSubTypes logic_uScript_IsCoreEncounterCompleted_corp_10 = FactionSubTypes.GSO;

	private int logic_uScript_IsCoreEncounterCompleted_grade_10 = 1;

	private string logic_uScript_IsCoreEncounterCompleted_encounterName_10 = "1-4 Solar Gen";

	private bool logic_uScript_IsCoreEncounterCompleted_True_10 = true;

	private bool logic_uScript_IsCoreEncounterCompleted_False_10 = true;

	private uScript_ShowQuestLog logic_uScript_ShowQuestLog_uScript_ShowQuestLog_11 = new uScript_ShowQuestLog();

	private GameObject logic_uScript_ShowQuestLog_owner_11;

	private bool logic_uScript_ShowQuestLog_Out_11 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_12 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_12 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
		}
		if (null == owner_Connection_4 || !m_RegisteredForEvents)
		{
			owner_Connection_4 = parentGameObject;
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
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_1.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_3.SetParent(g);
		logic_uScript_SetQuestLogVisible_uScript_SetQuestLogVisible_9.SetParent(g);
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_10.SetParent(g);
		logic_uScript_ShowQuestLog_uScript_ShowQuestLog_11.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_12.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_4 = parentGameObject;
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
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_10.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_1.OnDisable();
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
	public void In([FriendlyName("Flag_QuestLogShown", "")] ref bool Flag_QuestLogShown)
	{
		external_7 = Flag_QuestLogShown;
		Relay_In_2();
	}

	private void Relay_In_1()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_1 = owner_Connection_0;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_1.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_1);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_1.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_1.False;
		if (num)
		{
			Relay_True_3();
		}
		if (flag)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_2()
	{
		logic_uScriptCon_CompareBool_Bool_2 = external_7;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.In(logic_uScriptCon_CompareBool_Bool_2);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.False;
		if (num)
		{
			Relay_Connection_6();
		}
		if (flag)
		{
			Relay_In_1();
		}
	}

	private void Relay_True_3()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_3.True(out logic_uScriptAct_SetBool_Target_3);
		external_7 = logic_uScriptAct_SetBool_Target_3;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_3.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_False_3()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_3.False(out logic_uScriptAct_SetBool_Target_3);
		external_7 = logic_uScriptAct_SetBool_Target_3;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_3.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_Connection_5()
	{
	}

	private void Relay_Connection_6()
	{
		if (this.Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.Flag_QuestLogShown = external_7;
			this.Out(this, e);
		}
	}

	private void Relay_Connection_7()
	{
	}

	private void Relay_In_9()
	{
		logic_uScript_SetQuestLogVisible_uScript_SetQuestLogVisible_9.In(logic_uScript_SetQuestLogVisible_visible_9);
		if (logic_uScript_SetQuestLogVisible_uScript_SetQuestLogVisible_9.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_10.In(logic_uScript_IsCoreEncounterCompleted_corp_10, logic_uScript_IsCoreEncounterCompleted_grade_10, logic_uScript_IsCoreEncounterCompleted_encounterName_10);
		bool num = logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_10.True;
		bool flag = logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_10.False;
		if (num)
		{
			Relay_True_3();
		}
		if (flag)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_ShowQuestLog_owner_11 = owner_Connection_4;
		logic_uScript_ShowQuestLog_uScript_ShowQuestLog_11.In(logic_uScript_ShowQuestLog_owner_11);
		if (logic_uScript_ShowQuestLog_uScript_ShowQuestLog_11.Out)
		{
			Relay_Connection_6();
		}
	}

	private void Relay_In_12()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_12.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_12.Out)
		{
			Relay_Connection_6();
		}
	}
}
