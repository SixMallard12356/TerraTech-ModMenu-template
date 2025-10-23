using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_CompleteObjectiveStage", "")]
[NodePath("Graphs")]
public class SubGraph_CompleteObjectiveStage : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public int objectiveStage;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private int external_7;

	private bool external_10;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_3;

	private uScript_SetQuestObjectiveVisible logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_0 = new uScript_SetQuestObjectiveVisible();

	private GameObject logic_uScript_SetQuestObjectiveVisible_owner_0;

	private int logic_uScript_SetQuestObjectiveVisible_objectiveId_0;

	private bool logic_uScript_SetQuestObjectiveVisible_visible_0 = true;

	private bool logic_uScript_SetQuestObjectiveVisible_Out_0 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_2 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_2;

	private int logic_uScriptAct_AddInt_v2_B_2 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_2;

	private float logic_uScriptAct_AddInt_v2_FloatResult_2;

	private bool logic_uScriptAct_AddInt_v2_Out_2 = true;

	private uScript_SetQuestObjectiveCompleted logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_4 = new uScript_SetQuestObjectiveCompleted();

	private GameObject logic_uScript_SetQuestObjectiveCompleted_owner_4;

	private int logic_uScript_SetQuestObjectiveCompleted_objectiveId_4;

	private bool logic_uScript_SetQuestObjectiveCompleted_completed_4 = true;

	private bool logic_uScript_SetQuestObjectiveCompleted_Out_4 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_11;

	private bool logic_uScriptCon_CompareBool_True_11 = true;

	private bool logic_uScriptCon_CompareBool_False_11 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_12 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_12 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
		}
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
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
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_0.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_2.SetParent(g);
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_4.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_12.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_3 = parentGameObject;
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

	[FriendlyName("In", "")]
	public void In([FriendlyName("objectiveStage", "")] ref int objectiveStage, [FriendlyName("isFinalObjective", "")] bool isFinalObjective)
	{
		external_7 = objectiveStage;
		external_10 = isFinalObjective;
		Relay_MarkCompleted_4();
	}

	private void Relay_SetVisible_0()
	{
		logic_uScript_SetQuestObjectiveVisible_owner_0 = owner_Connection_1;
		logic_uScript_SetQuestObjectiveVisible_objectiveId_0 = external_7;
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_0.SetVisible(logic_uScript_SetQuestObjectiveVisible_owner_0, logic_uScript_SetQuestObjectiveVisible_objectiveId_0, logic_uScript_SetQuestObjectiveVisible_visible_0);
		if (logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_0.Out)
		{
			Relay_Connection_6();
		}
	}

	private void Relay_In_2()
	{
		logic_uScriptAct_AddInt_v2_A_2 = external_7;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_2.In(logic_uScriptAct_AddInt_v2_A_2, logic_uScriptAct_AddInt_v2_B_2, out logic_uScriptAct_AddInt_v2_IntResult_2, out logic_uScriptAct_AddInt_v2_FloatResult_2);
		external_7 = logic_uScriptAct_AddInt_v2_IntResult_2;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_2.Out)
		{
			Relay_SetVisible_0();
		}
	}

	private void Relay_MarkCompleted_4()
	{
		logic_uScript_SetQuestObjectiveCompleted_owner_4 = owner_Connection_3;
		logic_uScript_SetQuestObjectiveCompleted_objectiveId_4 = external_7;
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_4.MarkCompleted(logic_uScript_SetQuestObjectiveCompleted_owner_4, logic_uScript_SetQuestObjectiveCompleted_objectiveId_4, logic_uScript_SetQuestObjectiveCompleted_completed_4);
		if (logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_4.Out)
		{
			Relay_In_11();
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
			e.objectiveStage = external_7;
			this.Out(this, e);
		}
	}

	private void Relay_Connection_7()
	{
	}

	private void Relay_Connection_10()
	{
	}

	private void Relay_In_11()
	{
		logic_uScriptCon_CompareBool_Bool_11 = external_10;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11.In(logic_uScriptCon_CompareBool_Bool_11);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11.False;
		if (num)
		{
			Relay_In_12();
		}
		if (flag)
		{
			Relay_In_2();
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
