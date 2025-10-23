using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_Harvesting_Mission_ResourceTracker", "")]
[NodePath("Graphs")]
public class SubGraph_Harvesting_Mission_ResourceTracker : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private TankBlock external_19;

	private ChunkTypes external_20;

	private int external_22;

	private int local_HeldResourcesCount_System_Int32;

	private int local_TargetCount_System_Int32;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_16;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_1;

	private int logic_uScriptCon_CompareInt_B_1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_1 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_1 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_1 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_1 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_1 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_1 = true;

	private uScript_GetQuestObjectiveTargetCount logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_2 = new uScript_GetQuestObjectiveTargetCount();

	private GameObject logic_uScript_GetQuestObjectiveTargetCount_owner_2;

	private int logic_uScript_GetQuestObjectiveTargetCount_objectiveId_2;

	private int logic_uScript_GetQuestObjectiveTargetCount_Return_2;

	private bool logic_uScript_GetQuestObjectiveTargetCount_Out_2 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_3 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_3;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_3;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_3;

	private bool logic_uScript_SetQuestObjectiveCount_Out_3 = true;

	private uScript_GetHeldResourcesCount logic_uScript_GetHeldResourcesCount_uScript_GetHeldResourcesCount_6 = new uScript_GetHeldResourcesCount();

	private TankBlock logic_uScript_GetHeldResourcesCount_block_6;

	private ChunkTypes logic_uScript_GetHeldResourcesCount_resourceType_6;

	private int logic_uScript_GetHeldResourcesCount_Return_6;

	private bool logic_uScript_GetHeldResourcesCount_Out_6 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_8 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_8;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_8;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_8;

	private bool logic_uScript_SetQuestObjectiveCount_Out_8 = true;

	private uScript_SetQuestObjectiveCompleted logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_13 = new uScript_SetQuestObjectiveCompleted();

	private GameObject logic_uScript_SetQuestObjectiveCompleted_owner_13;

	private int logic_uScript_SetQuestObjectiveCompleted_objectiveId_13;

	private bool logic_uScript_SetQuestObjectiveCompleted_completed_13 = true;

	private bool logic_uScript_SetQuestObjectiveCompleted_Out_13 = true;

	private uScript_SetQuestObjectiveCompleted logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_15 = new uScript_SetQuestObjectiveCompleted();

	private GameObject logic_uScript_SetQuestObjectiveCompleted_owner_15;

	private int logic_uScript_SetQuestObjectiveCompleted_objectiveId_15;

	private bool logic_uScript_SetQuestObjectiveCompleted_completed_15;

	private bool logic_uScript_SetQuestObjectiveCompleted_Out_15 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
		}
		if (null == owner_Connection_5 || !m_RegisteredForEvents)
		{
			owner_Connection_5 = parentGameObject;
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
		}
		if (null == owner_Connection_16 || !m_RegisteredForEvents)
		{
			owner_Connection_16 = parentGameObject;
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
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1.SetParent(g);
		logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_2.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_3.SetParent(g);
		logic_uScript_GetHeldResourcesCount_uScript_GetHeldResourcesCount_6.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_8.SetParent(g);
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_13.SetParent(g);
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_15.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_16 = parentGameObject;
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
		logic_uScript_GetHeldResourcesCount_uScript_GetHeldResourcesCount_6.OnDisable();
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
	public void In([FriendlyName("block", "")] TankBlock block, [FriendlyName("resourceType", "")] ChunkTypes resourceType, [FriendlyName("objectiveID", "")] int objectiveID)
	{
		external_19 = block;
		external_20 = resourceType;
		external_22 = objectiveID;
		Relay_In_6();
	}

	private void Relay_In_1()
	{
		logic_uScriptCon_CompareInt_A_1 = local_HeldResourcesCount_System_Int32;
		logic_uScriptCon_CompareInt_B_1 = local_TargetCount_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1.In(logic_uScriptCon_CompareInt_A_1, logic_uScriptCon_CompareInt_B_1);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_SetCount_3();
		}
		if (lessThan)
		{
			Relay_SetCount_8();
		}
	}

	private void Relay_GetTargetCount_2()
	{
		logic_uScript_GetQuestObjectiveTargetCount_owner_2 = owner_Connection_5;
		logic_uScript_GetQuestObjectiveTargetCount_objectiveId_2 = external_22;
		logic_uScript_GetQuestObjectiveTargetCount_Return_2 = logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_2.GetTargetCount(logic_uScript_GetQuestObjectiveTargetCount_owner_2, logic_uScript_GetQuestObjectiveTargetCount_objectiveId_2);
		local_TargetCount_System_Int32 = logic_uScript_GetQuestObjectiveTargetCount_Return_2;
		if (logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_2.Out)
		{
			Relay_In_1();
		}
	}

	private void Relay_SetCount_3()
	{
		logic_uScript_SetQuestObjectiveCount_owner_3 = owner_Connection_0;
		logic_uScript_SetQuestObjectiveCount_objectiveId_3 = external_22;
		logic_uScript_SetQuestObjectiveCount_currentCount_3 = local_TargetCount_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_3.SetCount(logic_uScript_SetQuestObjectiveCount_owner_3, logic_uScript_SetQuestObjectiveCount_objectiveId_3, logic_uScript_SetQuestObjectiveCount_currentCount_3);
		if (logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_3.Out)
		{
			Relay_MarkCompleted_13();
		}
	}

	private void Relay_In_6()
	{
		logic_uScript_GetHeldResourcesCount_block_6 = external_19;
		logic_uScript_GetHeldResourcesCount_resourceType_6 = external_20;
		logic_uScript_GetHeldResourcesCount_Return_6 = logic_uScript_GetHeldResourcesCount_uScript_GetHeldResourcesCount_6.In(logic_uScript_GetHeldResourcesCount_block_6, logic_uScript_GetHeldResourcesCount_resourceType_6);
		local_HeldResourcesCount_System_Int32 = logic_uScript_GetHeldResourcesCount_Return_6;
		if (logic_uScript_GetHeldResourcesCount_uScript_GetHeldResourcesCount_6.Out)
		{
			Relay_GetTargetCount_2();
		}
	}

	private void Relay_SetCount_8()
	{
		logic_uScript_SetQuestObjectiveCount_owner_8 = owner_Connection_9;
		logic_uScript_SetQuestObjectiveCount_objectiveId_8 = external_22;
		logic_uScript_SetQuestObjectiveCount_currentCount_8 = local_HeldResourcesCount_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_8.SetCount(logic_uScript_SetQuestObjectiveCount_owner_8, logic_uScript_SetQuestObjectiveCount_objectiveId_8, logic_uScript_SetQuestObjectiveCount_currentCount_8);
		if (logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_8.Out)
		{
			Relay_MarkCompleted_15();
		}
	}

	private void Relay_MarkCompleted_13()
	{
		logic_uScript_SetQuestObjectiveCompleted_owner_13 = owner_Connection_14;
		logic_uScript_SetQuestObjectiveCompleted_objectiveId_13 = external_22;
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_13.MarkCompleted(logic_uScript_SetQuestObjectiveCompleted_owner_13, logic_uScript_SetQuestObjectiveCompleted_objectiveId_13, logic_uScript_SetQuestObjectiveCompleted_completed_13);
		if (logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_13.Out)
		{
			Relay_Connection_21();
		}
	}

	private void Relay_MarkCompleted_15()
	{
		logic_uScript_SetQuestObjectiveCompleted_owner_15 = owner_Connection_16;
		logic_uScript_SetQuestObjectiveCompleted_objectiveId_15 = external_22;
		logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_15.MarkCompleted(logic_uScript_SetQuestObjectiveCompleted_owner_15, logic_uScript_SetQuestObjectiveCompleted_objectiveId_15, logic_uScript_SetQuestObjectiveCompleted_completed_15);
		if (logic_uScript_SetQuestObjectiveCompleted_uScript_SetQuestObjectiveCompleted_15.Out)
		{
			Relay_Connection_21();
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
		if (this.Out != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Out(this, args);
		}
	}

	private void Relay_Connection_22()
	{
	}
}
