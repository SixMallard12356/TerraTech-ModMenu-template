using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_CheckStatsTarget", "")]
[NodePath("Graphs")]
public class SubGraph_CheckStatsTarget : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public int currentAmount;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private int external_17;

	private int external_16;

	private int external_5;

	private int external_14;

	private int local_TargetCount_System_Int32;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_13;

	private uScriptAct_SubtractInt logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_0 = new uScriptAct_SubtractInt();

	private int logic_uScriptAct_SubtractInt_A_0;

	private int logic_uScriptAct_SubtractInt_B_0;

	private int logic_uScriptAct_SubtractInt_IntResult_0;

	private float logic_uScriptAct_SubtractInt_FloatResult_0;

	private bool logic_uScriptAct_SubtractInt_Out_0 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_1;

	private int logic_uScriptCon_CompareInt_B_1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_1 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_1 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_1 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_1 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_1 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_1 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_9 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_9;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_9;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_9;

	private bool logic_uScript_SetQuestObjectiveCount_Out_9 = true;

	private uScript_GetQuestObjectiveTargetCount logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_11 = new uScript_GetQuestObjectiveTargetCount();

	private GameObject logic_uScript_GetQuestObjectiveTargetCount_owner_11;

	private int logic_uScript_GetQuestObjectiveTargetCount_objectiveId_11;

	private int logic_uScript_GetQuestObjectiveTargetCount_Return_11;

	private bool logic_uScript_GetQuestObjectiveTargetCount_Out_11 = true;

	[FriendlyName("Reached")]
	public event uScriptEventHandler Reached;

	[FriendlyName("Not Reached")]
	public event uScriptEventHandler Not_Reached;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
		}
		if (null == owner_Connection_13 || !m_RegisteredForEvents)
		{
			owner_Connection_13 = parentGameObject;
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
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_0.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_9.SetParent(g);
		logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_11.SetParent(g);
		owner_Connection_10 = parentGameObject;
		owner_Connection_13 = parentGameObject;
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
	public void In([FriendlyName("objectiveID", "")] int objectiveID, [FriendlyName("totalAmount", "")] int totalAmount, [FriendlyName("initialAmount", "")] int initialAmount, [FriendlyName("currentAmount", "")] ref int currentAmount)
	{
		external_17 = objectiveID;
		external_16 = totalAmount;
		external_5 = initialAmount;
		external_14 = currentAmount;
		Relay_In_0();
	}

	private void Relay_In_0()
	{
		logic_uScriptAct_SubtractInt_A_0 = external_16;
		logic_uScriptAct_SubtractInt_B_0 = external_5;
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_0.In(logic_uScriptAct_SubtractInt_A_0, logic_uScriptAct_SubtractInt_B_0, out logic_uScriptAct_SubtractInt_IntResult_0, out logic_uScriptAct_SubtractInt_FloatResult_0);
		external_14 = logic_uScriptAct_SubtractInt_IntResult_0;
		if (logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_0.Out)
		{
			Relay_SetCount_9();
		}
	}

	private void Relay_In_1()
	{
		logic_uScriptCon_CompareInt_A_1 = external_14;
		logic_uScriptCon_CompareInt_B_1 = local_TargetCount_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1.In(logic_uScriptCon_CompareInt_A_1, logic_uScriptCon_CompareInt_B_1);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_Connection_3();
		}
		if (lessThan)
		{
			Relay_Connection_4();
		}
	}

	private void Relay_Connection_2()
	{
	}

	private void Relay_Connection_3()
	{
		if (this.Reached != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.currentAmount = external_14;
			this.Reached(this, e);
		}
	}

	private void Relay_Connection_4()
	{
		if (this.Not_Reached != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.currentAmount = external_14;
			this.Not_Reached(this, e);
		}
	}

	private void Relay_Connection_5()
	{
	}

	private void Relay_SetCount_9()
	{
		logic_uScript_SetQuestObjectiveCount_owner_9 = owner_Connection_10;
		logic_uScript_SetQuestObjectiveCount_objectiveId_9 = external_17;
		logic_uScript_SetQuestObjectiveCount_currentCount_9 = external_14;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_9.SetCount(logic_uScript_SetQuestObjectiveCount_owner_9, logic_uScript_SetQuestObjectiveCount_objectiveId_9, logic_uScript_SetQuestObjectiveCount_currentCount_9);
		if (logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_9.Out)
		{
			Relay_GetTargetCount_11();
		}
	}

	private void Relay_GetTargetCount_11()
	{
		logic_uScript_GetQuestObjectiveTargetCount_owner_11 = owner_Connection_13;
		logic_uScript_GetQuestObjectiveTargetCount_objectiveId_11 = external_17;
		logic_uScript_GetQuestObjectiveTargetCount_Return_11 = logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_11.GetTargetCount(logic_uScript_GetQuestObjectiveTargetCount_owner_11, logic_uScript_GetQuestObjectiveTargetCount_objectiveId_11);
		local_TargetCount_System_Int32 = logic_uScript_GetQuestObjectiveTargetCount_Return_11;
		if (logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_11.Out)
		{
			Relay_In_1();
		}
	}

	private void Relay_Connection_14()
	{
	}

	private void Relay_Connection_16()
	{
	}

	private void Relay_Connection_17()
	{
	}
}
