using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_Stats_ResourceTracker", "")]
[NodePath("Graphs")]
public class Subgraph_Stats_ResourceTracker : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private ChunkTypes external_26;

	private int external_27;

	private int external_28;

	private bool external_34;

	private ChunkTypes external_24;

	private uScript_AddMessage.MessageSpeaker external_68;

	private uScript_AddMessage.MessageData external_69;

	private int local_CurrentAmount_System_Int32;

	private bool local_Init_System_Boolean;

	private int local_InitialAmount_System_Int32;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_59;

	private GameObject owner_Connection_60;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_0 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_0;

	private bool logic_uScriptCon_CompareBool_True_0 = true;

	private bool logic_uScriptCon_CompareBool_False_0 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_2 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_2;

	private bool logic_uScriptAct_SetBool_Out_2 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_2 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_2 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_4;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_4 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_4 = "Init";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_7;

	private int logic_SubGraph_SaveLoadInt_integer_7;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_7 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_7 = "InitialAmount";

	private SubGraph_CheckStatsTarget logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9 = new SubGraph_CheckStatsTarget();

	private int logic_SubGraph_CheckStatsTarget_objectiveID_9 = 1;

	private int logic_SubGraph_CheckStatsTarget_totalAmount_9;

	private int logic_SubGraph_CheckStatsTarget_initialAmount_9;

	private int logic_SubGraph_CheckStatsTarget_currentAmount_9;

	private uScript_GetNumResourcesSold logic_uScript_GetNumResourcesSold_uScript_GetNumResourcesSold_13 = new uScript_GetNumResourcesSold();

	private ChunkTypes logic_uScript_GetNumResourcesSold_resourceType_13;

	private int logic_uScript_GetNumResourcesSold_Return_13;

	private bool logic_uScript_GetNumResourcesSold_Out_13 = true;

	private uScript_CompareChunkTypes logic_uScript_CompareChunkTypes_uScript_CompareChunkTypes_14 = new uScript_CompareChunkTypes();

	private ChunkTypes logic_uScript_CompareChunkTypes_A_14;

	private ChunkTypes logic_uScript_CompareChunkTypes_B_14;

	private bool logic_uScript_CompareChunkTypes_EqualTo_14 = true;

	private bool logic_uScript_CompareChunkTypes_NotEqualTo_14 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_15;

	private bool logic_uScriptCon_CompareBool_True_15 = true;

	private bool logic_uScriptCon_CompareBool_False_15 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_16;

	private bool logic_uScriptCon_CompareBool_True_16 = true;

	private bool logic_uScriptCon_CompareBool_False_16 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_17 = true;

	private SubGraph_CheckStatsTarget logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18 = new SubGraph_CheckStatsTarget();

	private int logic_SubGraph_CheckStatsTarget_objectiveID_18 = 1;

	private int logic_SubGraph_CheckStatsTarget_totalAmount_18;

	private int logic_SubGraph_CheckStatsTarget_initialAmount_18;

	private int logic_SubGraph_CheckStatsTarget_currentAmount_18;

	private uScript_GetNumResourcesHarvested logic_uScript_GetNumResourcesHarvested_uScript_GetNumResourcesHarvested_31 = new uScript_GetNumResourcesHarvested();

	private ChunkTypes logic_uScript_GetNumResourcesHarvested_resourceType_31;

	private int logic_uScript_GetNumResourcesHarvested_Return_31;

	private bool logic_uScript_GetNumResourcesHarvested_Out_31 = true;

	private uScript_GetNumResourcesRefined logic_uScript_GetNumResourcesRefined_uScript_GetNumResourcesRefined_32 = new uScript_GetNumResourcesRefined();

	private ChunkTypes logic_uScript_GetNumResourcesRefined_resourceType_32;

	private int logic_uScript_GetNumResourcesRefined_Return_32;

	private bool logic_uScript_GetNumResourcesRefined_Out_32 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_33;

	private bool logic_uScriptCon_CompareBool_True_33 = true;

	private bool logic_uScriptCon_CompareBool_False_33 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_35;

	private bool logic_uScriptCon_CompareBool_True_35 = true;

	private bool logic_uScriptCon_CompareBool_False_35 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_40 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_40;

	private bool logic_uScriptCon_CompareBool_True_40 = true;

	private bool logic_uScriptCon_CompareBool_False_40 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_41 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_41;

	private bool logic_uScriptAct_SetBool_Out_41 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_41 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_41 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_43;

	private bool logic_uScriptCon_CompareBool_True_43 = true;

	private bool logic_uScriptCon_CompareBool_False_43 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_44 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_44;

	private bool logic_uScriptAct_SetBool_Out_44 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_44 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_44 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_58 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_58;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_58 = 1;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_58;

	private bool logic_uScript_SetQuestObjectiveCount_Out_58 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_61;

	private int logic_SubGraph_SaveLoadInt_integer_61;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_61 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_61 = "CurrentAmount";

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_64 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_64;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_64 = 1;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_64;

	private bool logic_uScript_SetQuestObjectiveCount_Out_64 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_67 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_67;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_67;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_67;

	private bool logic_uScript_AddMessage_Out_67 = true;

	private bool logic_uScript_AddMessage_Shown_67 = true;

	[FriendlyName("Target Reached")]
	public event uScriptEventHandler Target_Reached;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
			if (null != owner_Connection_12)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_12.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_12.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_5;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_5;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_5;
				}
			}
		}
		if (null == owner_Connection_59 || !m_RegisteredForEvents)
		{
			owner_Connection_59 = parentGameObject;
		}
		if (null == owner_Connection_60 || !m_RegisteredForEvents)
		{
			owner_Connection_60 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_12)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_12.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_12.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_5;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_5;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_5;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_12)
		{
			uScript_SaveLoad component = owner_Connection_12.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_5;
				component.LoadEvent -= Instance_LoadEvent_5;
				component.RestartEvent -= Instance_RestartEvent_5;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_0.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.SetParent(g);
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9.SetParent(g);
		logic_uScript_GetNumResourcesSold_uScript_GetNumResourcesSold_13.SetParent(g);
		logic_uScript_CompareChunkTypes_uScript_CompareChunkTypes_14.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.SetParent(g);
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18.SetParent(g);
		logic_uScript_GetNumResourcesHarvested_uScript_GetNumResourcesHarvested_31.SetParent(g);
		logic_uScript_GetNumResourcesRefined_uScript_GetNumResourcesRefined_32.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_40.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_58.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_64.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_67.SetParent(g);
		owner_Connection_12 = parentGameObject;
		owner_Connection_59 = parentGameObject;
		owner_Connection_60 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.Awake();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9.Awake();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Save_Out += SubGraph_SaveLoadBool_Save_Out_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Load_Out += SubGraph_SaveLoadBool_Load_Out_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_4;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.Save_Out += SubGraph_SaveLoadInt_Save_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.Load_Out += SubGraph_SaveLoadInt_Load_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_7;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9.Reached += SubGraph_CheckStatsTarget_Reached_9;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9.Not_Reached += SubGraph_CheckStatsTarget_Not_Reached_9;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18.Reached += SubGraph_CheckStatsTarget_Reached_18;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18.Not_Reached += SubGraph_CheckStatsTarget_Not_Reached_18;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.Save_Out += SubGraph_SaveLoadInt_Save_Out_61;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.Load_Out += SubGraph_SaveLoadInt_Load_Out_61;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_61;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.Start();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9.Start();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.OnEnable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9.OnEnable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.OnDisable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9.OnDisable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_67.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.Update();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9.Update();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.OnDestroy();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9.OnDestroy();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Save_Out -= SubGraph_SaveLoadBool_Save_Out_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Load_Out -= SubGraph_SaveLoadBool_Load_Out_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_4;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.Save_Out -= SubGraph_SaveLoadInt_Save_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.Load_Out -= SubGraph_SaveLoadInt_Load_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_7;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9.Reached -= SubGraph_CheckStatsTarget_Reached_9;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9.Not_Reached -= SubGraph_CheckStatsTarget_Not_Reached_9;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18.Reached -= SubGraph_CheckStatsTarget_Reached_18;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18.Not_Reached -= SubGraph_CheckStatsTarget_Not_Reached_18;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.Save_Out -= SubGraph_SaveLoadInt_Save_Out_61;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.Load_Out -= SubGraph_SaveLoadInt_Load_Out_61;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_61;
	}

	private void Instance_SaveEvent_5(object o, EventArgs e)
	{
		Relay_SaveEvent_5();
	}

	private void Instance_LoadEvent_5(object o, EventArgs e)
	{
		Relay_LoadEvent_5();
	}

	private void Instance_RestartEvent_5(object o, EventArgs e)
	{
		Relay_RestartEvent_5();
	}

	private void SubGraph_SaveLoadBool_Save_Out_4(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_4;
		Relay_Save_Out_4();
	}

	private void SubGraph_SaveLoadBool_Load_Out_4(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_4;
		Relay_Load_Out_4();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_4(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_4;
		Relay_Restart_Out_4();
	}

	private void SubGraph_SaveLoadInt_Save_Out_7(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_7 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_7;
		Relay_Save_Out_7();
	}

	private void SubGraph_SaveLoadInt_Load_Out_7(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_7 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_7;
		Relay_Load_Out_7();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_7(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_7 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_7;
		Relay_Restart_Out_7();
	}

	private void SubGraph_CheckStatsTarget_Reached_9(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_9 = e.currentAmount;
		local_CurrentAmount_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_9;
		Relay_Reached_9();
	}

	private void SubGraph_CheckStatsTarget_Not_Reached_9(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_9 = e.currentAmount;
		local_CurrentAmount_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_9;
		Relay_Not_Reached_9();
	}

	private void SubGraph_CheckStatsTarget_Reached_18(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_18 = e.currentAmount;
		local_CurrentAmount_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_18;
		Relay_Reached_18();
	}

	private void SubGraph_CheckStatsTarget_Not_Reached_18(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_18 = e.currentAmount;
		local_CurrentAmount_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_18;
		Relay_Not_Reached_18();
	}

	private void SubGraph_SaveLoadInt_Save_Out_61(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_61 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_61;
		Relay_Save_Out_61();
	}

	private void SubGraph_SaveLoadInt_Load_Out_61(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_61 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_61;
		Relay_Load_Out_61();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_61(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_61 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_61;
		Relay_Restart_Out_61();
	}

	[FriendlyName("Init Harvest", "")]
	public void Init_Harvest([FriendlyName("resourceType", "")] ChunkTypes resourceType, [FriendlyName("resourceTypeTotal", "")] int resourceTypeTotal, [FriendlyName("resourceTotal", "")] int resourceTotal, [FriendlyName("useResourceType", "")] bool useResourceType, [FriendlyName("targetResourceType", "")] ChunkTypes targetResourceType, [FriendlyName("messageSpeaker", "")] uScript_AddMessage.MessageSpeaker messageSpeaker, [FriendlyName("msgComplete", "")] uScript_AddMessage.MessageData msgComplete)
	{
		external_26 = resourceType;
		external_27 = resourceTypeTotal;
		external_28 = resourceTotal;
		external_34 = useResourceType;
		external_24 = targetResourceType;
		external_68 = messageSpeaker;
		external_69 = msgComplete;
		Relay_In_40();
	}

	[FriendlyName("Init Refine", "")]
	public void Init_Refine([FriendlyName("resourceType", "")] ChunkTypes resourceType, [FriendlyName("resourceTypeTotal", "")] int resourceTypeTotal, [FriendlyName("resourceTotal", "")] int resourceTotal, [FriendlyName("useResourceType", "")] bool useResourceType, [FriendlyName("targetResourceType", "")] ChunkTypes targetResourceType, [FriendlyName("messageSpeaker", "")] uScript_AddMessage.MessageSpeaker messageSpeaker, [FriendlyName("msgComplete", "")] uScript_AddMessage.MessageData msgComplete)
	{
		external_26 = resourceType;
		external_27 = resourceTypeTotal;
		external_28 = resourceTotal;
		external_34 = useResourceType;
		external_24 = targetResourceType;
		external_68 = messageSpeaker;
		external_69 = msgComplete;
		Relay_In_43();
	}

	[FriendlyName("Init Sell", "")]
	public void Init_Sell([FriendlyName("resourceType", "")] ChunkTypes resourceType, [FriendlyName("resourceTypeTotal", "")] int resourceTypeTotal, [FriendlyName("resourceTotal", "")] int resourceTotal, [FriendlyName("useResourceType", "")] bool useResourceType, [FriendlyName("targetResourceType", "")] ChunkTypes targetResourceType, [FriendlyName("messageSpeaker", "")] uScript_AddMessage.MessageSpeaker messageSpeaker, [FriendlyName("msgComplete", "")] uScript_AddMessage.MessageData msgComplete)
	{
		external_26 = resourceType;
		external_27 = resourceTypeTotal;
		external_28 = resourceTotal;
		external_34 = useResourceType;
		external_24 = targetResourceType;
		external_68 = messageSpeaker;
		external_69 = msgComplete;
		Relay_In_0();
	}

	[FriendlyName("Event Fired", "")]
	public void Event_Fired([FriendlyName("resourceType", "")] ChunkTypes resourceType, [FriendlyName("resourceTypeTotal", "")] int resourceTypeTotal, [FriendlyName("resourceTotal", "")] int resourceTotal, [FriendlyName("useResourceType", "")] bool useResourceType, [FriendlyName("targetResourceType", "")] ChunkTypes targetResourceType, [FriendlyName("messageSpeaker", "")] uScript_AddMessage.MessageSpeaker messageSpeaker, [FriendlyName("msgComplete", "")] uScript_AddMessage.MessageData msgComplete)
	{
		external_26 = resourceType;
		external_27 = resourceTypeTotal;
		external_28 = resourceTotal;
		external_34 = useResourceType;
		external_24 = targetResourceType;
		external_68 = messageSpeaker;
		external_69 = msgComplete;
		Relay_In_16();
	}

	private void Relay_In_0()
	{
		logic_uScriptCon_CompareBool_Bool_0 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_0.In(logic_uScriptCon_CompareBool_Bool_0);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_0.False)
		{
			Relay_True_2();
		}
	}

	private void Relay_True_2()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.True(out logic_uScriptAct_SetBool_Target_2);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_2;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_2.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_False_2()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.False(out logic_uScriptAct_SetBool_Target_2);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_2;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_2.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_Save_Out_4()
	{
		Relay_Save_7();
	}

	private void Relay_Load_Out_4()
	{
		Relay_Load_7();
	}

	private void Relay_Restart_Out_4()
	{
		Relay_Restart_7();
	}

	private void Relay_Save_4()
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Save(ref logic_SubGraph_SaveLoadBool_boolean_4, logic_SubGraph_SaveLoadBool_boolAsVariable_4, logic_SubGraph_SaveLoadBool_uniqueID_4);
	}

	private void Relay_Load_4()
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Load(ref logic_SubGraph_SaveLoadBool_boolean_4, logic_SubGraph_SaveLoadBool_boolAsVariable_4, logic_SubGraph_SaveLoadBool_uniqueID_4);
	}

	private void Relay_Set_True_4()
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_4, logic_SubGraph_SaveLoadBool_boolAsVariable_4, logic_SubGraph_SaveLoadBool_uniqueID_4);
	}

	private void Relay_Set_False_4()
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_4 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_4, logic_SubGraph_SaveLoadBool_boolAsVariable_4, logic_SubGraph_SaveLoadBool_uniqueID_4);
	}

	private void Relay_SaveEvent_5()
	{
		Relay_Save_4();
	}

	private void Relay_LoadEvent_5()
	{
		Relay_Load_4();
	}

	private void Relay_RestartEvent_5()
	{
		Relay_Set_False_4();
	}

	private void Relay_Save_Out_7()
	{
		Relay_Save_61();
	}

	private void Relay_Load_Out_7()
	{
		Relay_Load_61();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_Restart_61();
	}

	private void Relay_Save_7()
	{
		logic_SubGraph_SaveLoadInt_integer_7 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_7 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.Save(logic_SubGraph_SaveLoadInt_restartValue_7, ref logic_SubGraph_SaveLoadInt_integer_7, logic_SubGraph_SaveLoadInt_intAsVariable_7, logic_SubGraph_SaveLoadInt_uniqueID_7);
	}

	private void Relay_Load_7()
	{
		logic_SubGraph_SaveLoadInt_integer_7 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_7 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.Load(logic_SubGraph_SaveLoadInt_restartValue_7, ref logic_SubGraph_SaveLoadInt_integer_7, logic_SubGraph_SaveLoadInt_intAsVariable_7, logic_SubGraph_SaveLoadInt_uniqueID_7);
	}

	private void Relay_Restart_7()
	{
		logic_SubGraph_SaveLoadInt_integer_7 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_7 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_7.Restart(logic_SubGraph_SaveLoadInt_restartValue_7, ref logic_SubGraph_SaveLoadInt_integer_7, logic_SubGraph_SaveLoadInt_intAsVariable_7, logic_SubGraph_SaveLoadInt_uniqueID_7);
	}

	private void Relay_Reached_9()
	{
		Relay_In_67();
	}

	private void Relay_Not_Reached_9()
	{
	}

	private void Relay_In_9()
	{
		logic_SubGraph_CheckStatsTarget_totalAmount_9 = external_27;
		logic_SubGraph_CheckStatsTarget_initialAmount_9 = local_InitialAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_currentAmount_9 = local_CurrentAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_9.In(logic_SubGraph_CheckStatsTarget_objectiveID_9, logic_SubGraph_CheckStatsTarget_totalAmount_9, logic_SubGraph_CheckStatsTarget_initialAmount_9, ref logic_SubGraph_CheckStatsTarget_currentAmount_9);
	}

	private void Relay_AllResources_13()
	{
		logic_uScript_GetNumResourcesSold_resourceType_13 = external_24;
		logic_uScript_GetNumResourcesSold_Return_13 = logic_uScript_GetNumResourcesSold_uScript_GetNumResourcesSold_13.AllResources(logic_uScript_GetNumResourcesSold_resourceType_13);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumResourcesSold_Return_13;
		if (logic_uScript_GetNumResourcesSold_uScript_GetNumResourcesSold_13.Out)
		{
			Relay_SetCount_58();
		}
	}

	private void Relay_ResourcesOfType_13()
	{
		logic_uScript_GetNumResourcesSold_resourceType_13 = external_24;
		logic_uScript_GetNumResourcesSold_Return_13 = logic_uScript_GetNumResourcesSold_uScript_GetNumResourcesSold_13.ResourcesOfType(logic_uScript_GetNumResourcesSold_resourceType_13);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumResourcesSold_Return_13;
		if (logic_uScript_GetNumResourcesSold_uScript_GetNumResourcesSold_13.Out)
		{
			Relay_SetCount_58();
		}
	}

	private void Relay_In_14()
	{
		logic_uScript_CompareChunkTypes_A_14 = external_26;
		logic_uScript_CompareChunkTypes_B_14 = external_24;
		logic_uScript_CompareChunkTypes_uScript_CompareChunkTypes_14.In(logic_uScript_CompareChunkTypes_A_14, logic_uScript_CompareChunkTypes_B_14);
		if (logic_uScript_CompareChunkTypes_uScript_CompareChunkTypes_14.EqualTo)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_15()
	{
		logic_uScriptCon_CompareBool_Bool_15 = external_34;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.In(logic_uScriptCon_CompareBool_Bool_15);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.False;
		if (num)
		{
			Relay_ResourcesOfType_13();
		}
		if (flag)
		{
			Relay_AllResources_13();
		}
	}

	private void Relay_In_16()
	{
		logic_uScriptCon_CompareBool_Bool_16 = external_34;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.In(logic_uScriptCon_CompareBool_Bool_16);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.False;
		if (num)
		{
			Relay_In_14();
		}
		if (flag)
		{
			Relay_In_17();
		}
	}

	private void Relay_In_17()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_Reached_18()
	{
		Relay_In_67();
	}

	private void Relay_Not_Reached_18()
	{
	}

	private void Relay_In_18()
	{
		logic_SubGraph_CheckStatsTarget_totalAmount_18 = external_28;
		logic_SubGraph_CheckStatsTarget_initialAmount_18 = local_InitialAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_currentAmount_18 = local_CurrentAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_18.In(logic_SubGraph_CheckStatsTarget_objectiveID_18, logic_SubGraph_CheckStatsTarget_totalAmount_18, logic_SubGraph_CheckStatsTarget_initialAmount_18, ref logic_SubGraph_CheckStatsTarget_currentAmount_18);
	}

	private void Relay_Connection_20()
	{
	}

	private void Relay_Connection_21()
	{
		if (this.Target_Reached != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Target_Reached(this, args);
		}
	}

	private void Relay_Connection_24()
	{
	}

	private void Relay_Connection_26()
	{
	}

	private void Relay_Connection_27()
	{
	}

	private void Relay_Connection_28()
	{
	}

	private void Relay_AllResources_31()
	{
		logic_uScript_GetNumResourcesHarvested_resourceType_31 = external_24;
		logic_uScript_GetNumResourcesHarvested_Return_31 = logic_uScript_GetNumResourcesHarvested_uScript_GetNumResourcesHarvested_31.AllResources(logic_uScript_GetNumResourcesHarvested_resourceType_31);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumResourcesHarvested_Return_31;
		if (logic_uScript_GetNumResourcesHarvested_uScript_GetNumResourcesHarvested_31.Out)
		{
			Relay_SetCount_58();
		}
	}

	private void Relay_ResourcesOfType_31()
	{
		logic_uScript_GetNumResourcesHarvested_resourceType_31 = external_24;
		logic_uScript_GetNumResourcesHarvested_Return_31 = logic_uScript_GetNumResourcesHarvested_uScript_GetNumResourcesHarvested_31.ResourcesOfType(logic_uScript_GetNumResourcesHarvested_resourceType_31);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumResourcesHarvested_Return_31;
		if (logic_uScript_GetNumResourcesHarvested_uScript_GetNumResourcesHarvested_31.Out)
		{
			Relay_SetCount_58();
		}
	}

	private void Relay_AllResources_32()
	{
		logic_uScript_GetNumResourcesRefined_resourceType_32 = external_24;
		logic_uScript_GetNumResourcesRefined_Return_32 = logic_uScript_GetNumResourcesRefined_uScript_GetNumResourcesRefined_32.AllResources(logic_uScript_GetNumResourcesRefined_resourceType_32);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumResourcesRefined_Return_32;
		if (logic_uScript_GetNumResourcesRefined_uScript_GetNumResourcesRefined_32.Out)
		{
			Relay_SetCount_58();
		}
	}

	private void Relay_ResourcesOfType_32()
	{
		logic_uScript_GetNumResourcesRefined_resourceType_32 = external_24;
		logic_uScript_GetNumResourcesRefined_Return_32 = logic_uScript_GetNumResourcesRefined_uScript_GetNumResourcesRefined_32.ResourcesOfType(logic_uScript_GetNumResourcesRefined_resourceType_32);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumResourcesRefined_Return_32;
		if (logic_uScript_GetNumResourcesRefined_uScript_GetNumResourcesRefined_32.Out)
		{
			Relay_SetCount_58();
		}
	}

	private void Relay_In_33()
	{
		logic_uScriptCon_CompareBool_Bool_33 = external_34;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.In(logic_uScriptCon_CompareBool_Bool_33);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.False;
		if (num)
		{
			Relay_ResourcesOfType_31();
		}
		if (flag)
		{
			Relay_AllResources_31();
		}
	}

	private void Relay_Connection_34()
	{
	}

	private void Relay_In_35()
	{
		logic_uScriptCon_CompareBool_Bool_35 = external_34;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.In(logic_uScriptCon_CompareBool_Bool_35);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.False;
		if (num)
		{
			Relay_ResourcesOfType_32();
		}
		if (flag)
		{
			Relay_AllResources_32();
		}
	}

	private void Relay_Connection_37()
	{
	}

	private void Relay_Connection_38()
	{
	}

	private void Relay_Connection_39()
	{
	}

	private void Relay_In_40()
	{
		logic_uScriptCon_CompareBool_Bool_40 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_40.In(logic_uScriptCon_CompareBool_Bool_40);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_40.False)
		{
			Relay_True_41();
		}
	}

	private void Relay_True_41()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.True(out logic_uScriptAct_SetBool_Target_41);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_41;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_41.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_False_41()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.False(out logic_uScriptAct_SetBool_Target_41);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_41;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_41.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_In_43()
	{
		logic_uScriptCon_CompareBool_Bool_43 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.In(logic_uScriptCon_CompareBool_Bool_43);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.False)
		{
			Relay_True_44();
		}
	}

	private void Relay_True_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.True(out logic_uScriptAct_SetBool_Target_44);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_False_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.False(out logic_uScriptAct_SetBool_Target_44);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_SetCount_58()
	{
		logic_uScript_SetQuestObjectiveCount_owner_58 = owner_Connection_59;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_58.SetCount(logic_uScript_SetQuestObjectiveCount_owner_58, logic_uScript_SetQuestObjectiveCount_objectiveId_58, logic_uScript_SetQuestObjectiveCount_currentCount_58);
	}

	private void Relay_Save_Out_61()
	{
	}

	private void Relay_Load_Out_61()
	{
		Relay_SetCount_64();
	}

	private void Relay_Restart_Out_61()
	{
	}

	private void Relay_Save_61()
	{
		logic_SubGraph_SaveLoadInt_integer_61 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_61 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.Save(logic_SubGraph_SaveLoadInt_restartValue_61, ref logic_SubGraph_SaveLoadInt_integer_61, logic_SubGraph_SaveLoadInt_intAsVariable_61, logic_SubGraph_SaveLoadInt_uniqueID_61);
	}

	private void Relay_Load_61()
	{
		logic_SubGraph_SaveLoadInt_integer_61 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_61 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.Load(logic_SubGraph_SaveLoadInt_restartValue_61, ref logic_SubGraph_SaveLoadInt_integer_61, logic_SubGraph_SaveLoadInt_intAsVariable_61, logic_SubGraph_SaveLoadInt_uniqueID_61);
	}

	private void Relay_Restart_61()
	{
		logic_SubGraph_SaveLoadInt_integer_61 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_61 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_61.Restart(logic_SubGraph_SaveLoadInt_restartValue_61, ref logic_SubGraph_SaveLoadInt_integer_61, logic_SubGraph_SaveLoadInt_intAsVariable_61, logic_SubGraph_SaveLoadInt_uniqueID_61);
	}

	private void Relay_SetCount_64()
	{
		logic_uScript_SetQuestObjectiveCount_owner_64 = owner_Connection_60;
		logic_uScript_SetQuestObjectiveCount_currentCount_64 = local_CurrentAmount_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_64.SetCount(logic_uScript_SetQuestObjectiveCount_owner_64, logic_uScript_SetQuestObjectiveCount_objectiveId_64, logic_uScript_SetQuestObjectiveCount_currentCount_64);
	}

	private void Relay_In_67()
	{
		logic_uScript_AddMessage_messageData_67 = external_69;
		logic_uScript_AddMessage_speaker_67 = external_68;
		logic_uScript_AddMessage_Return_67 = logic_uScript_AddMessage_uScript_AddMessage_67.In(logic_uScript_AddMessage_messageData_67, logic_uScript_AddMessage_speaker_67);
		if (logic_uScript_AddMessage_uScript_AddMessage_67.Out)
		{
			Relay_Connection_21();
		}
	}

	private void Relay_Connection_68()
	{
	}

	private void Relay_Connection_69()
	{
	}
}
