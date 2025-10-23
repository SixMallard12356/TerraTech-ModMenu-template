using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_Stats_KillTracker", "")]
public class Subgraph_Stats_KillTracker : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private int external_41;

	private int external_11;

	private FactionSubTypes external_36;

	private bool external_33;

	private FactionSubTypes external_34;

	private uScript_AddMessage.MessageSpeaker external_55;

	private uScript_AddMessage.MessageData external_28;

	private int local_CurrentAmount_System_Int32;

	private bool local_Init_System_Boolean;

	private int local_InitialAmount_System_Int32;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_46;

	private GameObject owner_Connection_51;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_0;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_0 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_0 = "Init";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_3;

	private int logic_SubGraph_SaveLoadInt_integer_3;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_3 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_3 = "InitialAmount";

	private SubGraph_CheckStatsTarget logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5 = new SubGraph_CheckStatsTarget();

	private int logic_SubGraph_CheckStatsTarget_objectiveID_5 = 2;

	private int logic_SubGraph_CheckStatsTarget_totalAmount_5;

	private int logic_SubGraph_CheckStatsTarget_initialAmount_5;

	private int logic_SubGraph_CheckStatsTarget_currentAmount_5;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_16;

	private bool logic_uScriptCon_CompareBool_True_16 = true;

	private bool logic_uScriptCon_CompareBool_False_16 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_17 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_17;

	private bool logic_uScriptAct_SetBool_Out_17 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_17 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_17 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_19;

	private bool logic_uScriptCon_CompareBool_True_19 = true;

	private bool logic_uScriptCon_CompareBool_False_19 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_20 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_20;

	private bool logic_uScriptAct_SetBool_Out_20 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_20 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_20 = true;

	private uScript_GetNumEnemyTechsDestroyed logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_31 = new uScript_GetNumEnemyTechsDestroyed();

	private FactionSubTypes logic_uScript_GetNumEnemyTechsDestroyed_faction_31;

	private int logic_uScript_GetNumEnemyTechsDestroyed_Return_31;

	private bool logic_uScript_GetNumEnemyTechsDestroyed_Out_31 = true;

	private uScript_GetNumInvadersDestroyed logic_uScript_GetNumInvadersDestroyed_uScript_GetNumInvadersDestroyed_32 = new uScript_GetNumInvadersDestroyed();

	private int logic_uScript_GetNumInvadersDestroyed_Return_32;

	private bool logic_uScript_GetNumInvadersDestroyed_Out_32 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_35 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_37;

	private bool logic_uScriptCon_CompareBool_True_37 = true;

	private bool logic_uScriptCon_CompareBool_False_37 = true;

	private uScript_CompareFactionSubTypes logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_38 = new uScript_CompareFactionSubTypes();

	private FactionSubTypes logic_uScript_CompareFactionSubTypes_A_38;

	private FactionSubTypes logic_uScript_CompareFactionSubTypes_B_38;

	private bool logic_uScript_CompareFactionSubTypes_EqualTo_38 = true;

	private bool logic_uScript_CompareFactionSubTypes_NotEqualTo_38 = true;

	private SubGraph_CheckStatsTarget logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39 = new SubGraph_CheckStatsTarget();

	private int logic_SubGraph_CheckStatsTarget_objectiveID_39 = 2;

	private int logic_SubGraph_CheckStatsTarget_totalAmount_39;

	private int logic_SubGraph_CheckStatsTarget_initialAmount_39;

	private int logic_SubGraph_CheckStatsTarget_currentAmount_39;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_43;

	private bool logic_uScriptCon_CompareBool_True_43 = true;

	private bool logic_uScriptCon_CompareBool_False_43 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_45 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_45;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_45 = 2;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_45;

	private bool logic_uScript_SetQuestObjectiveCount_Out_45 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_49;

	private int logic_SubGraph_SaveLoadInt_integer_49;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_49 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_49 = "CurrentAmount";

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_52 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_52;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_52 = 2;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_52;

	private bool logic_uScript_SetQuestObjectiveCount_Out_52 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_54 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_54;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_54;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_54;

	private bool logic_uScript_AddMessage_Out_54 = true;

	private bool logic_uScript_AddMessage_Shown_54 = true;

	[FriendlyName("Target Reached")]
	public event uScriptEventHandler Target_Reached;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
			if (null != owner_Connection_8)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_1;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_1;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_1;
				}
			}
		}
		if (null == owner_Connection_46 || !m_RegisteredForEvents)
		{
			owner_Connection_46 = parentGameObject;
		}
		if (null == owner_Connection_51 || !m_RegisteredForEvents)
		{
			owner_Connection_51 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_8)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_1;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_1;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_1;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_8)
		{
			uScript_SaveLoad component = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_1;
				component.LoadEvent -= Instance_LoadEvent_1;
				component.RestartEvent -= Instance_RestartEvent_1;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.SetParent(g);
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_20.SetParent(g);
		logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_31.SetParent(g);
		logic_uScript_GetNumInvadersDestroyed_uScript_GetNumInvadersDestroyed_32.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.SetParent(g);
		logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_38.SetParent(g);
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_45.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_52.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_54.SetParent(g);
		owner_Connection_8 = parentGameObject;
		owner_Connection_46 = parentGameObject;
		owner_Connection_51 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.Awake();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5.Awake();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Save_Out += SubGraph_SaveLoadBool_Save_Out_0;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Load_Out += SubGraph_SaveLoadBool_Load_Out_0;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.Save_Out += SubGraph_SaveLoadInt_Save_Out_3;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.Load_Out += SubGraph_SaveLoadInt_Load_Out_3;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_3;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5.Reached += SubGraph_CheckStatsTarget_Reached_5;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5.Not_Reached += SubGraph_CheckStatsTarget_Not_Reached_5;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39.Reached += SubGraph_CheckStatsTarget_Reached_39;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39.Not_Reached += SubGraph_CheckStatsTarget_Not_Reached_39;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.Save_Out += SubGraph_SaveLoadInt_Save_Out_49;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.Load_Out += SubGraph_SaveLoadInt_Load_Out_49;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_49;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.Start();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5.Start();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.OnEnable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5.OnEnable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.OnDisable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5.OnDisable();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_54.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.Update();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5.Update();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.OnDestroy();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5.OnDestroy();
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Save_Out -= SubGraph_SaveLoadBool_Save_Out_0;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Load_Out -= SubGraph_SaveLoadBool_Load_Out_0;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.Save_Out -= SubGraph_SaveLoadInt_Save_Out_3;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.Load_Out -= SubGraph_SaveLoadInt_Load_Out_3;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_3;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5.Reached -= SubGraph_CheckStatsTarget_Reached_5;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5.Not_Reached -= SubGraph_CheckStatsTarget_Not_Reached_5;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39.Reached -= SubGraph_CheckStatsTarget_Reached_39;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39.Not_Reached -= SubGraph_CheckStatsTarget_Not_Reached_39;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.Save_Out -= SubGraph_SaveLoadInt_Save_Out_49;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.Load_Out -= SubGraph_SaveLoadInt_Load_Out_49;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_49;
	}

	private void Instance_SaveEvent_1(object o, EventArgs e)
	{
		Relay_SaveEvent_1();
	}

	private void Instance_LoadEvent_1(object o, EventArgs e)
	{
		Relay_LoadEvent_1();
	}

	private void Instance_RestartEvent_1(object o, EventArgs e)
	{
		Relay_RestartEvent_1();
	}

	private void SubGraph_SaveLoadBool_Save_Out_0(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_0 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_0;
		Relay_Save_Out_0();
	}

	private void SubGraph_SaveLoadBool_Load_Out_0(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_0 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_0;
		Relay_Load_Out_0();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_0(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_0 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_0;
		Relay_Restart_Out_0();
	}

	private void SubGraph_SaveLoadInt_Save_Out_3(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_3 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_3;
		Relay_Save_Out_3();
	}

	private void SubGraph_SaveLoadInt_Load_Out_3(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_3 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_3;
		Relay_Load_Out_3();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_3(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_3 = e.integer;
		local_InitialAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_3;
		Relay_Restart_Out_3();
	}

	private void SubGraph_CheckStatsTarget_Reached_5(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_5 = e.currentAmount;
		local_CurrentAmount_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_5;
		Relay_Reached_5();
	}

	private void SubGraph_CheckStatsTarget_Not_Reached_5(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_5 = e.currentAmount;
		local_CurrentAmount_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_5;
		Relay_Not_Reached_5();
	}

	private void SubGraph_CheckStatsTarget_Reached_39(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_39 = e.currentAmount;
		local_CurrentAmount_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_39;
		Relay_Reached_39();
	}

	private void SubGraph_CheckStatsTarget_Not_Reached_39(object o, SubGraph_CheckStatsTarget.LogicEventArgs e)
	{
		logic_SubGraph_CheckStatsTarget_currentAmount_39 = e.currentAmount;
		local_CurrentAmount_System_Int32 = logic_SubGraph_CheckStatsTarget_currentAmount_39;
		Relay_Not_Reached_39();
	}

	private void SubGraph_SaveLoadInt_Save_Out_49(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_49 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_49;
		Relay_Save_Out_49();
	}

	private void SubGraph_SaveLoadInt_Load_Out_49(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_49 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_49;
		Relay_Load_Out_49();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_49(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_49 = e.integer;
		local_CurrentAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_49;
		Relay_Restart_Out_49();
	}

	[FriendlyName("Track Enemies", "")]
	public void Track_Enemies([FriendlyName("killTotal", "")] int killTotal, [FriendlyName("factionKillTotal", "")] int factionKillTotal, [FriendlyName("factionType", "")] FactionSubTypes factionType, [FriendlyName("useFactionType", "")] bool useFactionType, [FriendlyName("targetFactionType", "")] FactionSubTypes targetFactionType, [FriendlyName("messageSpeaker", "")] uScript_AddMessage.MessageSpeaker messageSpeaker, [FriendlyName("msgComplete", "")] uScript_AddMessage.MessageData msgComplete)
	{
		external_41 = killTotal;
		external_11 = factionKillTotal;
		external_36 = factionType;
		external_33 = useFactionType;
		external_34 = targetFactionType;
		external_55 = messageSpeaker;
		external_28 = msgComplete;
		Relay_In_16();
	}

	[FriendlyName("Track Invaders", "")]
	public void Track_Invaders([FriendlyName("killTotal", "")] int killTotal, [FriendlyName("factionKillTotal", "")] int factionKillTotal, [FriendlyName("factionType", "")] FactionSubTypes factionType, [FriendlyName("useFactionType", "")] bool useFactionType, [FriendlyName("targetFactionType", "")] FactionSubTypes targetFactionType, [FriendlyName("messageSpeaker", "")] uScript_AddMessage.MessageSpeaker messageSpeaker, [FriendlyName("msgComplete", "")] uScript_AddMessage.MessageData msgComplete)
	{
		external_41 = killTotal;
		external_11 = factionKillTotal;
		external_36 = factionType;
		external_33 = useFactionType;
		external_34 = targetFactionType;
		external_55 = messageSpeaker;
		external_28 = msgComplete;
		Relay_In_19();
	}

	[FriendlyName("Event Fired", "")]
	public void Event_Fired([FriendlyName("killTotal", "")] int killTotal, [FriendlyName("factionKillTotal", "")] int factionKillTotal, [FriendlyName("factionType", "")] FactionSubTypes factionType, [FriendlyName("useFactionType", "")] bool useFactionType, [FriendlyName("targetFactionType", "")] FactionSubTypes targetFactionType, [FriendlyName("messageSpeaker", "")] uScript_AddMessage.MessageSpeaker messageSpeaker, [FriendlyName("msgComplete", "")] uScript_AddMessage.MessageData msgComplete)
	{
		external_41 = killTotal;
		external_11 = factionKillTotal;
		external_36 = factionType;
		external_33 = useFactionType;
		external_34 = targetFactionType;
		external_55 = messageSpeaker;
		external_28 = msgComplete;
		Relay_In_37();
	}

	private void Relay_Save_Out_0()
	{
		Relay_Save_3();
	}

	private void Relay_Load_Out_0()
	{
		Relay_Load_3();
	}

	private void Relay_Restart_Out_0()
	{
		Relay_Restart_3();
	}

	private void Relay_Save_0()
	{
		logic_SubGraph_SaveLoadBool_boolean_0 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_0 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Save(ref logic_SubGraph_SaveLoadBool_boolean_0, logic_SubGraph_SaveLoadBool_boolAsVariable_0, logic_SubGraph_SaveLoadBool_uniqueID_0);
	}

	private void Relay_Load_0()
	{
		logic_SubGraph_SaveLoadBool_boolean_0 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_0 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Load(ref logic_SubGraph_SaveLoadBool_boolean_0, logic_SubGraph_SaveLoadBool_boolAsVariable_0, logic_SubGraph_SaveLoadBool_uniqueID_0);
	}

	private void Relay_Set_True_0()
	{
		logic_SubGraph_SaveLoadBool_boolean_0 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_0 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_0, logic_SubGraph_SaveLoadBool_boolAsVariable_0, logic_SubGraph_SaveLoadBool_uniqueID_0);
	}

	private void Relay_Set_False_0()
	{
		logic_SubGraph_SaveLoadBool_boolean_0 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_0 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_0.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_0, logic_SubGraph_SaveLoadBool_boolAsVariable_0, logic_SubGraph_SaveLoadBool_uniqueID_0);
	}

	private void Relay_SaveEvent_1()
	{
		Relay_Save_0();
	}

	private void Relay_LoadEvent_1()
	{
		Relay_Load_0();
	}

	private void Relay_RestartEvent_1()
	{
		Relay_Set_False_0();
	}

	private void Relay_Save_Out_3()
	{
		Relay_Save_49();
	}

	private void Relay_Load_Out_3()
	{
		Relay_Load_49();
	}

	private void Relay_Restart_Out_3()
	{
		Relay_Restart_49();
	}

	private void Relay_Save_3()
	{
		logic_SubGraph_SaveLoadInt_integer_3 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_3 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.Save(logic_SubGraph_SaveLoadInt_restartValue_3, ref logic_SubGraph_SaveLoadInt_integer_3, logic_SubGraph_SaveLoadInt_intAsVariable_3, logic_SubGraph_SaveLoadInt_uniqueID_3);
	}

	private void Relay_Load_3()
	{
		logic_SubGraph_SaveLoadInt_integer_3 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_3 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.Load(logic_SubGraph_SaveLoadInt_restartValue_3, ref logic_SubGraph_SaveLoadInt_integer_3, logic_SubGraph_SaveLoadInt_intAsVariable_3, logic_SubGraph_SaveLoadInt_uniqueID_3);
	}

	private void Relay_Restart_3()
	{
		logic_SubGraph_SaveLoadInt_integer_3 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_3 = local_InitialAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_3.Restart(logic_SubGraph_SaveLoadInt_restartValue_3, ref logic_SubGraph_SaveLoadInt_integer_3, logic_SubGraph_SaveLoadInt_intAsVariable_3, logic_SubGraph_SaveLoadInt_uniqueID_3);
	}

	private void Relay_Reached_5()
	{
		Relay_In_54();
	}

	private void Relay_Not_Reached_5()
	{
	}

	private void Relay_In_5()
	{
		logic_SubGraph_CheckStatsTarget_totalAmount_5 = external_11;
		logic_SubGraph_CheckStatsTarget_initialAmount_5 = local_InitialAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_currentAmount_5 = local_CurrentAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_5.In(logic_SubGraph_CheckStatsTarget_objectiveID_5, logic_SubGraph_CheckStatsTarget_totalAmount_5, logic_SubGraph_CheckStatsTarget_initialAmount_5, ref logic_SubGraph_CheckStatsTarget_currentAmount_5);
	}

	private void Relay_Connection_9()
	{
	}

	private void Relay_Connection_10()
	{
		if (this.Target_Reached != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Target_Reached(this, args);
		}
	}

	private void Relay_Connection_11()
	{
	}

	private void Relay_Connection_14()
	{
	}

	private void Relay_Connection_15()
	{
	}

	private void Relay_In_16()
	{
		logic_uScriptCon_CompareBool_Bool_16 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.In(logic_uScriptCon_CompareBool_Bool_16);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.False)
		{
			Relay_True_17();
		}
	}

	private void Relay_True_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.True(out logic_uScriptAct_SetBool_Target_17);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_False_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.False(out logic_uScriptAct_SetBool_Target_17);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_19()
	{
		logic_uScriptCon_CompareBool_Bool_19 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.In(logic_uScriptCon_CompareBool_Bool_19);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.False)
		{
			Relay_True_20();
		}
	}

	private void Relay_True_20()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_20.True(out logic_uScriptAct_SetBool_Target_20);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_20;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_20.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_False_20()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_20.False(out logic_uScriptAct_SetBool_Target_20);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_20;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_20.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_Connection_28()
	{
	}

	private void Relay_TotalEnemiesDestroyed_31()
	{
		logic_uScript_GetNumEnemyTechsDestroyed_faction_31 = external_34;
		logic_uScript_GetNumEnemyTechsDestroyed_Return_31 = logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_31.TotalEnemiesDestroyed(logic_uScript_GetNumEnemyTechsDestroyed_faction_31);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumEnemyTechsDestroyed_Return_31;
		if (logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_31.Out)
		{
			Relay_SetCount_45();
		}
	}

	private void Relay_EnemiesDestroyedOfFaction_31()
	{
		logic_uScript_GetNumEnemyTechsDestroyed_faction_31 = external_34;
		logic_uScript_GetNumEnemyTechsDestroyed_Return_31 = logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_31.EnemiesDestroyedOfFaction(logic_uScript_GetNumEnemyTechsDestroyed_faction_31);
		local_InitialAmount_System_Int32 = logic_uScript_GetNumEnemyTechsDestroyed_Return_31;
		if (logic_uScript_GetNumEnemyTechsDestroyed_uScript_GetNumEnemyTechsDestroyed_31.Out)
		{
			Relay_SetCount_45();
		}
	}

	private void Relay_In_32()
	{
		logic_uScript_GetNumInvadersDestroyed_Return_32 = logic_uScript_GetNumInvadersDestroyed_uScript_GetNumInvadersDestroyed_32.In();
		local_InitialAmount_System_Int32 = logic_uScript_GetNumInvadersDestroyed_Return_32;
		if (logic_uScript_GetNumInvadersDestroyed_uScript_GetNumInvadersDestroyed_32.Out)
		{
			Relay_SetCount_45();
		}
	}

	private void Relay_Connection_33()
	{
	}

	private void Relay_Connection_34()
	{
	}

	private void Relay_In_35()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_Connection_36()
	{
	}

	private void Relay_In_37()
	{
		logic_uScriptCon_CompareBool_Bool_37 = external_33;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.In(logic_uScriptCon_CompareBool_Bool_37);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.False;
		if (num)
		{
			Relay_In_38();
		}
		if (flag)
		{
			Relay_In_35();
		}
	}

	private void Relay_In_38()
	{
		logic_uScript_CompareFactionSubTypes_A_38 = external_36;
		logic_uScript_CompareFactionSubTypes_B_38 = external_34;
		logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_38.In(logic_uScript_CompareFactionSubTypes_A_38, logic_uScript_CompareFactionSubTypes_B_38);
		if (logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_38.EqualTo)
		{
			Relay_In_5();
		}
	}

	private void Relay_Reached_39()
	{
		Relay_In_54();
	}

	private void Relay_Not_Reached_39()
	{
	}

	private void Relay_In_39()
	{
		logic_SubGraph_CheckStatsTarget_totalAmount_39 = external_41;
		logic_SubGraph_CheckStatsTarget_initialAmount_39 = local_InitialAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_currentAmount_39 = local_CurrentAmount_System_Int32;
		logic_SubGraph_CheckStatsTarget_SubGraph_CheckStatsTarget_39.In(logic_SubGraph_CheckStatsTarget_objectiveID_39, logic_SubGraph_CheckStatsTarget_totalAmount_39, logic_SubGraph_CheckStatsTarget_initialAmount_39, ref logic_SubGraph_CheckStatsTarget_currentAmount_39);
	}

	private void Relay_Connection_41()
	{
	}

	private void Relay_In_43()
	{
		logic_uScriptCon_CompareBool_Bool_43 = external_33;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.In(logic_uScriptCon_CompareBool_Bool_43);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.False;
		if (num)
		{
			Relay_EnemiesDestroyedOfFaction_31();
		}
		if (flag)
		{
			Relay_TotalEnemiesDestroyed_31();
		}
	}

	private void Relay_SetCount_45()
	{
		logic_uScript_SetQuestObjectiveCount_owner_45 = owner_Connection_46;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_45.SetCount(logic_uScript_SetQuestObjectiveCount_owner_45, logic_uScript_SetQuestObjectiveCount_objectiveId_45, logic_uScript_SetQuestObjectiveCount_currentCount_45);
	}

	private void Relay_Save_Out_49()
	{
	}

	private void Relay_Load_Out_49()
	{
		Relay_SetCount_52();
	}

	private void Relay_Restart_Out_49()
	{
	}

	private void Relay_Save_49()
	{
		logic_SubGraph_SaveLoadInt_integer_49 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_49 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.Save(logic_SubGraph_SaveLoadInt_restartValue_49, ref logic_SubGraph_SaveLoadInt_integer_49, logic_SubGraph_SaveLoadInt_intAsVariable_49, logic_SubGraph_SaveLoadInt_uniqueID_49);
	}

	private void Relay_Load_49()
	{
		logic_SubGraph_SaveLoadInt_integer_49 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_49 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.Load(logic_SubGraph_SaveLoadInt_restartValue_49, ref logic_SubGraph_SaveLoadInt_integer_49, logic_SubGraph_SaveLoadInt_intAsVariable_49, logic_SubGraph_SaveLoadInt_uniqueID_49);
	}

	private void Relay_Restart_49()
	{
		logic_SubGraph_SaveLoadInt_integer_49 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_49 = local_CurrentAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_49.Restart(logic_SubGraph_SaveLoadInt_restartValue_49, ref logic_SubGraph_SaveLoadInt_integer_49, logic_SubGraph_SaveLoadInt_intAsVariable_49, logic_SubGraph_SaveLoadInt_uniqueID_49);
	}

	private void Relay_SetCount_52()
	{
		logic_uScript_SetQuestObjectiveCount_owner_52 = owner_Connection_51;
		logic_uScript_SetQuestObjectiveCount_currentCount_52 = local_CurrentAmount_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_52.SetCount(logic_uScript_SetQuestObjectiveCount_owner_52, logic_uScript_SetQuestObjectiveCount_objectiveId_52, logic_uScript_SetQuestObjectiveCount_currentCount_52);
	}

	private void Relay_In_54()
	{
		logic_uScript_AddMessage_messageData_54 = external_28;
		logic_uScript_AddMessage_speaker_54 = external_55;
		logic_uScript_AddMessage_Return_54 = logic_uScript_AddMessage_uScript_AddMessage_54.In(logic_uScript_AddMessage_messageData_54, logic_uScript_AddMessage_speaker_54);
		if (logic_uScript_AddMessage_uScript_AddMessage_54.Out)
		{
			Relay_Connection_10();
		}
	}

	private void Relay_Connection_55()
	{
	}
}
