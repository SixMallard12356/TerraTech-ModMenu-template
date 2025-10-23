using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_LoadObjectiveStates", "")]
[NodePath("Graphs")]
public class SubGraph_LoadObjectiveStates : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private int external_16;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_13;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_26;

	private GameObject owner_Connection_30;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_0 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_0;

	private int logic_uScriptCon_CompareInt_B_0 = 4;

	private bool logic_uScriptCon_CompareInt_GreaterThan_0 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_0 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_0 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_0 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_0 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_0 = true;

	private uScript_SetQuestObjectiveVisible logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_2 = new uScript_SetQuestObjectiveVisible();

	private GameObject logic_uScript_SetQuestObjectiveVisible_owner_2;

	private int logic_uScript_SetQuestObjectiveVisible_objectiveId_2 = 4;

	private bool logic_uScript_SetQuestObjectiveVisible_visible_2 = true;

	private bool logic_uScript_SetQuestObjectiveVisible_Out_2 = true;

	private uScript_SetQuestObjectiveVisible logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_3 = new uScript_SetQuestObjectiveVisible();

	private GameObject logic_uScript_SetQuestObjectiveVisible_owner_3;

	private int logic_uScript_SetQuestObjectiveVisible_objectiveId_3 = 2;

	private bool logic_uScript_SetQuestObjectiveVisible_visible_3 = true;

	private bool logic_uScript_SetQuestObjectiveVisible_Out_3 = true;

	private uScript_SetQuestObjectiveCompleted logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_4 = new uScript_SetQuestObjectiveCompleted();

	private GameObject logic_uScript_SetQuestObjectiveCompleted_owner_4;

	private int logic_uScript_SetQuestObjectiveCompleted_objectiveId_4 = 3;

	private bool logic_uScript_SetQuestObjectiveCompleted_completed_4 = true;

	private bool logic_uScript_SetQuestObjectiveCompleted_Out_4 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_5 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_5;

	private int logic_uScriptCon_CompareInt_B_5 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_5 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_5 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_5 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_5 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_5 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_5 = true;

	private uScript_SetQuestObjectiveVisible logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_8 = new uScript_SetQuestObjectiveVisible();

	private GameObject logic_uScript_SetQuestObjectiveVisible_owner_8;

	private int logic_uScript_SetQuestObjectiveVisible_objectiveId_8 = 3;

	private bool logic_uScript_SetQuestObjectiveVisible_visible_8 = true;

	private bool logic_uScript_SetQuestObjectiveVisible_Out_8 = true;

	private uScript_SetQuestObjectiveCompleted logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_9 = new uScript_SetQuestObjectiveCompleted();

	private GameObject logic_uScript_SetQuestObjectiveCompleted_owner_9;

	private int logic_uScript_SetQuestObjectiveCompleted_objectiveId_9 = 1;

	private bool logic_uScript_SetQuestObjectiveCompleted_completed_9 = true;

	private bool logic_uScript_SetQuestObjectiveCompleted_Out_9 = true;

	private uScript_SetQuestObjectiveCompleted logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_10 = new uScript_SetQuestObjectiveCompleted();

	private GameObject logic_uScript_SetQuestObjectiveCompleted_owner_10;

	private int logic_uScript_SetQuestObjectiveCompleted_objectiveId_10 = 2;

	private bool logic_uScript_SetQuestObjectiveCompleted_completed_10 = true;

	private bool logic_uScript_SetQuestObjectiveCompleted_Out_10 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_14 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_14;

	private int logic_uScriptCon_CompareInt_B_14 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_14 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_14 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_14 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_14 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_14 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_14 = true;

	private uScript_SetQuestObjectiveVisible logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_21 = new uScript_SetQuestObjectiveVisible();

	private GameObject logic_uScript_SetQuestObjectiveVisible_owner_21;

	private int logic_uScript_SetQuestObjectiveVisible_objectiveId_21 = 5;

	private bool logic_uScript_SetQuestObjectiveVisible_visible_21 = true;

	private bool logic_uScript_SetQuestObjectiveVisible_Out_21 = true;

	private uScript_SetQuestObjectiveCompleted logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_22 = new uScript_SetQuestObjectiveCompleted();

	private GameObject logic_uScript_SetQuestObjectiveCompleted_owner_22;

	private int logic_uScript_SetQuestObjectiveCompleted_objectiveId_22 = 4;

	private bool logic_uScript_SetQuestObjectiveCompleted_completed_22 = true;

	private bool logic_uScript_SetQuestObjectiveCompleted_Out_22 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_24 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_24;

	private int logic_uScriptCon_CompareInt_B_24 = 5;

	private bool logic_uScriptCon_CompareInt_GreaterThan_24 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_24 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_24 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_24 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_24 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_24 = true;

	private uScript_SetQuestObjectiveVisible logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_25 = new uScript_SetQuestObjectiveVisible();

	private GameObject logic_uScript_SetQuestObjectiveVisible_owner_25;

	private int logic_uScript_SetQuestObjectiveVisible_objectiveId_25 = 6;

	private bool logic_uScript_SetQuestObjectiveVisible_visible_25 = true;

	private bool logic_uScript_SetQuestObjectiveVisible_Out_25 = true;

	private uScript_SetQuestObjectiveCompleted logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_27 = new uScript_SetQuestObjectiveCompleted();

	private GameObject logic_uScript_SetQuestObjectiveCompleted_owner_27;

	private int logic_uScript_SetQuestObjectiveCompleted_objectiveId_27 = 5;

	private bool logic_uScript_SetQuestObjectiveCompleted_completed_27 = true;

	private bool logic_uScript_SetQuestObjectiveCompleted_Out_27 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_28 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_28;

	private int logic_uScriptCon_CompareInt_B_28 = 6;

	private bool logic_uScriptCon_CompareInt_GreaterThan_28 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_28 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_28 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_28 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_28 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_28 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_37 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_37 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_38 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_38 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_39 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_40 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_41 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
		}
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
		}
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
		}
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
		}
		if (null == owner_Connection_13 || !m_RegisteredForEvents)
		{
			owner_Connection_13 = parentGameObject;
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (null == owner_Connection_26 || !m_RegisteredForEvents)
		{
			owner_Connection_26 = parentGameObject;
		}
		if (null == owner_Connection_30 || !m_RegisteredForEvents)
		{
			owner_Connection_30 = parentGameObject;
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
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_0.SetParent(g);
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_2.SetParent(g);
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_3.SetParent(g);
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_4.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_5.SetParent(g);
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_8.SetParent(g);
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_9.SetParent(g);
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_10.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_14.SetParent(g);
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_21.SetParent(g);
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_22.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_24.SetParent(g);
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_25.SetParent(g);
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_27.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_28.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_37.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_38.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_6 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_13 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_26 = parentGameObject;
		owner_Connection_30 = parentGameObject;
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
	public void In([FriendlyName("currentObjective", "")] int currentObjective)
	{
		external_16 = currentObjective;
		Relay_In_5();
	}

	private void Relay_In_0()
	{
		logic_uScriptCon_CompareInt_A_0 = external_16;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_0.In(logic_uScriptCon_CompareInt_A_0, logic_uScriptCon_CompareInt_B_0);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_0.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_0.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_MarkCompleted_4();
		}
		if (lessThan)
		{
			Relay_In_39();
		}
	}

	private void Relay_SetVisible_2()
	{
		logic_uScript_SetQuestObjectiveVisible_owner_2 = owner_Connection_6;
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_2.SetVisible(logic_uScript_SetQuestObjectiveVisible_owner_2, logic_uScript_SetQuestObjectiveVisible_objectiveId_2, logic_uScript_SetQuestObjectiveVisible_visible_2);
		if (logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_2.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_SetVisible_3()
	{
		logic_uScript_SetQuestObjectiveVisible_owner_3 = owner_Connection_7;
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_3.SetVisible(logic_uScript_SetQuestObjectiveVisible_owner_3, logic_uScript_SetQuestObjectiveVisible_objectiveId_3, logic_uScript_SetQuestObjectiveVisible_visible_3);
		if (logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_3.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_MarkCompleted_4()
	{
		logic_uScript_SetQuestObjectiveCompleted_owner_4 = owner_Connection_13;
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_4.MarkCompleted(logic_uScript_SetQuestObjectiveCompleted_owner_4, logic_uScript_SetQuestObjectiveCompleted_objectiveId_4, logic_uScript_SetQuestObjectiveCompleted_completed_4);
		if (logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_4.Out)
		{
			Relay_SetVisible_2();
		}
	}

	private void Relay_In_5()
	{
		logic_uScriptCon_CompareInt_A_5 = external_16;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_5.In(logic_uScriptCon_CompareInt_A_5, logic_uScriptCon_CompareInt_B_5);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_5.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_5.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_MarkCompleted_9();
		}
		if (lessThan)
		{
			Relay_In_37();
		}
	}

	private void Relay_SetVisible_8()
	{
		logic_uScript_SetQuestObjectiveVisible_owner_8 = owner_Connection_11;
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_8.SetVisible(logic_uScript_SetQuestObjectiveVisible_owner_8, logic_uScript_SetQuestObjectiveVisible_objectiveId_8, logic_uScript_SetQuestObjectiveVisible_visible_8);
		if (logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_8.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_MarkCompleted_9()
	{
		logic_uScript_SetQuestObjectiveCompleted_owner_9 = owner_Connection_12;
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_9.MarkCompleted(logic_uScript_SetQuestObjectiveCompleted_owner_9, logic_uScript_SetQuestObjectiveCompleted_objectiveId_9, logic_uScript_SetQuestObjectiveCompleted_completed_9);
		if (logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_9.Out)
		{
			Relay_SetVisible_3();
		}
	}

	private void Relay_MarkCompleted_10()
	{
		logic_uScript_SetQuestObjectiveCompleted_owner_10 = owner_Connection_1;
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_10.MarkCompleted(logic_uScript_SetQuestObjectiveCompleted_owner_10, logic_uScript_SetQuestObjectiveCompleted_objectiveId_10, logic_uScript_SetQuestObjectiveCompleted_completed_10);
		if (logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_10.Out)
		{
			Relay_SetVisible_8();
		}
	}

	private void Relay_In_14()
	{
		logic_uScriptCon_CompareInt_A_14 = external_16;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_14.In(logic_uScriptCon_CompareInt_A_14, logic_uScriptCon_CompareInt_B_14);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_14.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_14.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_MarkCompleted_10();
		}
		if (lessThan)
		{
			Relay_In_38();
		}
	}

	private void Relay_Connection_15()
	{
	}

	private void Relay_Connection_16()
	{
	}

	private void Relay_SetVisible_21()
	{
		logic_uScript_SetQuestObjectiveVisible_owner_21 = owner_Connection_20;
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_21.SetVisible(logic_uScript_SetQuestObjectiveVisible_owner_21, logic_uScript_SetQuestObjectiveVisible_objectiveId_21, logic_uScript_SetQuestObjectiveVisible_visible_21);
		if (logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_21.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_MarkCompleted_22()
	{
		logic_uScript_SetQuestObjectiveCompleted_owner_22 = owner_Connection_19;
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_22.MarkCompleted(logic_uScript_SetQuestObjectiveCompleted_owner_22, logic_uScript_SetQuestObjectiveCompleted_objectiveId_22, logic_uScript_SetQuestObjectiveCompleted_completed_22);
		if (logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_22.Out)
		{
			Relay_SetVisible_21();
		}
	}

	private void Relay_In_24()
	{
		logic_uScriptCon_CompareInt_A_24 = external_16;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_24.In(logic_uScriptCon_CompareInt_A_24, logic_uScriptCon_CompareInt_B_24);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_24.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_24.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_MarkCompleted_22();
		}
		if (lessThan)
		{
			Relay_In_41();
		}
	}

	private void Relay_SetVisible_25()
	{
		logic_uScript_SetQuestObjectiveVisible_owner_25 = owner_Connection_30;
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_25.SetVisible(logic_uScript_SetQuestObjectiveVisible_owner_25, logic_uScript_SetQuestObjectiveVisible_objectiveId_25, logic_uScript_SetQuestObjectiveVisible_visible_25);
		if (logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_25.Out)
		{
			Relay_Connection_31();
		}
	}

	private void Relay_MarkCompleted_27()
	{
		logic_uScript_SetQuestObjectiveCompleted_owner_27 = owner_Connection_26;
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_27.MarkCompleted(logic_uScript_SetQuestObjectiveCompleted_owner_27, logic_uScript_SetQuestObjectiveCompleted_objectiveId_27, logic_uScript_SetQuestObjectiveCompleted_completed_27);
		if (logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_27.Out)
		{
			Relay_SetVisible_25();
		}
	}

	private void Relay_In_28()
	{
		logic_uScriptCon_CompareInt_A_28 = external_16;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_28.In(logic_uScriptCon_CompareInt_A_28, logic_uScriptCon_CompareInt_B_28);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_28.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_28.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_MarkCompleted_27();
		}
		if (lessThan)
		{
			Relay_In_40();
		}
	}

	private void Relay_Connection_31()
	{
		if (this.Out != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Out(this, args);
		}
	}

	private void Relay_In_37()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_37.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_37.Out)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_38()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_38.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_38.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_39()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_40()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.Out)
		{
			Relay_Connection_31();
		}
	}

	private void Relay_In_41()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.Out)
		{
			Relay_In_40();
		}
	}
}
