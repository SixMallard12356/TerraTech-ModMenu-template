using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_GSO_1_5_DefeatEnemies : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private int local_CurrentKillTotal_System_Int32;

	private bool local_MsgIntroShown_System_Boolean;

	private Tank local_NearestEnemyTech_Tank;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public uScript_AddMessage.MessageData msgComplete;

	public uScript_AddMessage.MessageData msgIntro;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_4;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_29;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_6 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_6;

	private bool logic_uScript_FinishEncounter_Out_6 = true;

	private Subgraph_Stats_KillTracker logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7 = new Subgraph_Stats_KillTracker();

	private int logic_Subgraph_Stats_KillTracker_killTotal_7;

	private int logic_Subgraph_Stats_KillTracker_factionKillTotal_7;

	private FactionSubTypes logic_Subgraph_Stats_KillTracker_factionType_7;

	private bool logic_Subgraph_Stats_KillTracker_useFactionType_7;

	private FactionSubTypes logic_Subgraph_Stats_KillTracker_targetFactionType_7;

	private uScript_AddMessage.MessageSpeaker logic_Subgraph_Stats_KillTracker_messageSpeaker_7;

	private uScript_AddMessage.MessageData logic_Subgraph_Stats_KillTracker_msgComplete_7;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_13;

	private bool logic_uScriptCon_CompareBool_True_13 = true;

	private bool logic_uScriptCon_CompareBool_False_13 = true;

	private uScript_GetNearestTech logic_uScript_GetNearestTech_uScript_GetNearestTech_15 = new uScript_GetNearestTech();

	private int logic_uScript_GetNearestTech_team_15 = 1;

	private bool logic_uScript_GetNearestTech_recheck_15 = true;

	private Tank logic_uScript_GetNearestTech_Return_15;

	private bool logic_uScript_GetNearestTech_Found_15 = true;

	private bool logic_uScript_GetNearestTech_NotFound_15 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_17 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_17;

	private object logic_uScript_SetEncounterTarget_visibleObject_17 = "";

	private bool logic_uScript_SetEncounterTarget_Out_17 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_18 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_18;

	private bool logic_uScript_ClearEncounterTarget_Out_18 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_25 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_25;

	private bool logic_uScriptAct_SetBool_Out_25 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_25 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_25 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_26 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_26;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_26;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_26;

	private bool logic_uScript_AddMessage_Out_26 = true;

	private bool logic_uScript_AddMessage_Shown_26 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_31;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_31 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_31 = "MsgIntroShown";

	private FactionSubTypes event_UnityEngine_GameObject_Faction_8;

	private int event_UnityEngine_GameObject_FactionTotal_8;

	private int event_UnityEngine_GameObject_NumEnemyTechsDestroyed_8;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
		}
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
			if (null != owner_Connection_3)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_3.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_3.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
				}
			}
		}
		if (null == owner_Connection_4 || !m_RegisteredForEvents)
		{
			owner_Connection_4 = parentGameObject;
			if (null != owner_Connection_4)
			{
				uScript_EnemyTechDestroyedEvent uScript_EnemyTechDestroyedEvent2 = owner_Connection_4.GetComponent<uScript_EnemyTechDestroyedEvent>();
				if (null == uScript_EnemyTechDestroyedEvent2)
				{
					uScript_EnemyTechDestroyedEvent2 = owner_Connection_4.AddComponent<uScript_EnemyTechDestroyedEvent>();
				}
				if (null != uScript_EnemyTechDestroyedEvent2)
				{
					uScript_EnemyTechDestroyedEvent2.EnemyTechDestroyedEvent += Instance_EnemyTechDestroyedEvent_8;
				}
			}
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (!(null == owner_Connection_29) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_29 = parentGameObject;
		if (null != owner_Connection_29)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_29.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_29.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_32;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_32;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_32;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_3)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_3.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_3.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_4)
		{
			uScript_EnemyTechDestroyedEvent uScript_EnemyTechDestroyedEvent2 = owner_Connection_4.GetComponent<uScript_EnemyTechDestroyedEvent>();
			if (null == uScript_EnemyTechDestroyedEvent2)
			{
				uScript_EnemyTechDestroyedEvent2 = owner_Connection_4.AddComponent<uScript_EnemyTechDestroyedEvent>();
			}
			if (null != uScript_EnemyTechDestroyedEvent2)
			{
				uScript_EnemyTechDestroyedEvent2.EnemyTechDestroyedEvent += Instance_EnemyTechDestroyedEvent_8;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_29)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_29.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_29.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_32;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_32;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_32;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_3)
		{
			uScript_EncounterUpdate component = owner_Connection_3.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_0;
				component.OnSuspend -= Instance_OnSuspend_0;
				component.OnResume -= Instance_OnResume_0;
			}
		}
		if (null != owner_Connection_4)
		{
			uScript_EnemyTechDestroyedEvent component2 = owner_Connection_4.GetComponent<uScript_EnemyTechDestroyedEvent>();
			if (null != component2)
			{
				component2.EnemyTechDestroyedEvent -= Instance_EnemyTechDestroyedEvent_8;
			}
		}
		if (null != owner_Connection_29)
		{
			uScript_SaveLoad component3 = owner_Connection_29.GetComponent<uScript_SaveLoad>();
			if (null != component3)
			{
				component3.SaveEvent -= Instance_SaveEvent_32;
				component3.LoadEvent -= Instance_LoadEvent_32;
				component3.RestartEvent -= Instance_RestartEvent_32;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_6.SetParent(g);
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.SetParent(g);
		logic_uScript_GetNearestTech_uScript_GetNearestTech_15.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_17.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_18.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_25.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_26.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_4 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_29 = parentGameObject;
	}

	public void Awake()
	{
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Awake();
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Target_Reached += Subgraph_Stats_KillTracker_Target_Reached_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Save_Out += SubGraph_SaveLoadBool_Save_Out_31;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Load_Out += SubGraph_SaveLoadBool_Load_Out_31;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_31;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.OnEnable();
	}

	public void OnDisable()
	{
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.OnDisable();
		logic_uScript_GetNearestTech_uScript_GetNearestTech_15.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_26.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Update();
	}

	public void OnDestroy()
	{
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.OnDestroy();
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Target_Reached -= Subgraph_Stats_KillTracker_Target_Reached_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Save_Out -= SubGraph_SaveLoadBool_Save_Out_31;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Load_Out -= SubGraph_SaveLoadBool_Load_Out_31;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_31;
	}

	private void Instance_OnUpdate_0(object o, EventArgs e)
	{
		Relay_OnUpdate_0();
	}

	private void Instance_OnSuspend_0(object o, EventArgs e)
	{
		Relay_OnSuspend_0();
	}

	private void Instance_OnResume_0(object o, EventArgs e)
	{
		Relay_OnResume_0();
	}

	private void Instance_EnemyTechDestroyedEvent_8(object o, uScript_EnemyTechDestroyedEvent.EnemyTechDestroyedEventArgs e)
	{
		event_UnityEngine_GameObject_Faction_8 = e.Faction;
		event_UnityEngine_GameObject_FactionTotal_8 = e.FactionTotal;
		event_UnityEngine_GameObject_NumEnemyTechsDestroyed_8 = e.NumEnemyTechsDestroyed;
		Relay_EnemyTechDestroyedEvent_8();
	}

	private void Instance_SaveEvent_32(object o, EventArgs e)
	{
		Relay_SaveEvent_32();
	}

	private void Instance_LoadEvent_32(object o, EventArgs e)
	{
		Relay_LoadEvent_32();
	}

	private void Instance_RestartEvent_32(object o, EventArgs e)
	{
		Relay_RestartEvent_32();
	}

	private void Subgraph_Stats_KillTracker_Target_Reached_7(object o, Subgraph_Stats_KillTracker.LogicEventArgs e)
	{
		Relay_Target_Reached_7();
	}

	private void SubGraph_SaveLoadBool_Save_Out_31(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = e.boolean;
		local_MsgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_31;
		Relay_Save_Out_31();
	}

	private void SubGraph_SaveLoadBool_Load_Out_31(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = e.boolean;
		local_MsgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_31;
		Relay_Load_Out_31();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_31(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = e.boolean;
		local_MsgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_31;
		Relay_Restart_Out_31();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_13();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_Succeed_6()
	{
		logic_uScript_FinishEncounter_owner_6 = owner_Connection_2;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_6.Succeed(logic_uScript_FinishEncounter_owner_6);
	}

	private void Relay_Fail_6()
	{
		logic_uScript_FinishEncounter_owner_6 = owner_Connection_2;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_6.Fail(logic_uScript_FinishEncounter_owner_6);
	}

	private void Relay_Target_Reached_7()
	{
		Relay_Succeed_6();
	}

	private void Relay_Track_Enemies_7()
	{
		logic_Subgraph_Stats_KillTracker_killTotal_7 = local_CurrentKillTotal_System_Int32;
		logic_Subgraph_Stats_KillTracker_messageSpeaker_7 = messageSpeaker;
		logic_Subgraph_Stats_KillTracker_msgComplete_7 = msgComplete;
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Track_Enemies(logic_Subgraph_Stats_KillTracker_killTotal_7, logic_Subgraph_Stats_KillTracker_factionKillTotal_7, logic_Subgraph_Stats_KillTracker_factionType_7, logic_Subgraph_Stats_KillTracker_useFactionType_7, logic_Subgraph_Stats_KillTracker_targetFactionType_7, logic_Subgraph_Stats_KillTracker_messageSpeaker_7, logic_Subgraph_Stats_KillTracker_msgComplete_7);
	}

	private void Relay_Track_Invaders_7()
	{
		logic_Subgraph_Stats_KillTracker_killTotal_7 = local_CurrentKillTotal_System_Int32;
		logic_Subgraph_Stats_KillTracker_messageSpeaker_7 = messageSpeaker;
		logic_Subgraph_Stats_KillTracker_msgComplete_7 = msgComplete;
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Track_Invaders(logic_Subgraph_Stats_KillTracker_killTotal_7, logic_Subgraph_Stats_KillTracker_factionKillTotal_7, logic_Subgraph_Stats_KillTracker_factionType_7, logic_Subgraph_Stats_KillTracker_useFactionType_7, logic_Subgraph_Stats_KillTracker_targetFactionType_7, logic_Subgraph_Stats_KillTracker_messageSpeaker_7, logic_Subgraph_Stats_KillTracker_msgComplete_7);
	}

	private void Relay_Event_Fired_7()
	{
		logic_Subgraph_Stats_KillTracker_killTotal_7 = local_CurrentKillTotal_System_Int32;
		logic_Subgraph_Stats_KillTracker_messageSpeaker_7 = messageSpeaker;
		logic_Subgraph_Stats_KillTracker_msgComplete_7 = msgComplete;
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Event_Fired(logic_Subgraph_Stats_KillTracker_killTotal_7, logic_Subgraph_Stats_KillTracker_factionKillTotal_7, logic_Subgraph_Stats_KillTracker_factionType_7, logic_Subgraph_Stats_KillTracker_useFactionType_7, logic_Subgraph_Stats_KillTracker_targetFactionType_7, logic_Subgraph_Stats_KillTracker_messageSpeaker_7, logic_Subgraph_Stats_KillTracker_msgComplete_7);
	}

	private void Relay_EnemyTechDestroyedEvent_8()
	{
		local_CurrentKillTotal_System_Int32 = event_UnityEngine_GameObject_NumEnemyTechsDestroyed_8;
		Relay_Event_Fired_7();
	}

	private void Relay_In_13()
	{
		logic_uScriptCon_CompareBool_Bool_13 = local_MsgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.In(logic_uScriptCon_CompareBool_Bool_13);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.False;
		if (num)
		{
			Relay_In_15();
		}
		if (flag)
		{
			Relay_True_25();
		}
	}

	private void Relay_In_15()
	{
		logic_uScript_GetNearestTech_Return_15 = logic_uScript_GetNearestTech_uScript_GetNearestTech_15.In(logic_uScript_GetNearestTech_team_15, logic_uScript_GetNearestTech_recheck_15);
		local_NearestEnemyTech_Tank = logic_uScript_GetNearestTech_Return_15;
		bool found = logic_uScript_GetNearestTech_uScript_GetNearestTech_15.Found;
		bool notFound = logic_uScript_GetNearestTech_uScript_GetNearestTech_15.NotFound;
		if (found)
		{
			Relay_In_17();
		}
		if (notFound)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_17()
	{
		logic_uScript_SetEncounterTarget_owner_17 = owner_Connection_19;
		logic_uScript_SetEncounterTarget_visibleObject_17 = local_NearestEnemyTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_17.In(logic_uScript_SetEncounterTarget_owner_17, logic_uScript_SetEncounterTarget_visibleObject_17);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_17.Out)
		{
			Relay_Track_Enemies_7();
		}
	}

	private void Relay_In_18()
	{
		logic_uScript_ClearEncounterTarget_owner_18 = owner_Connection_20;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_18.In(logic_uScript_ClearEncounterTarget_owner_18);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_18.Out)
		{
			Relay_Track_Enemies_7();
		}
	}

	private void Relay_True_25()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_25.True(out logic_uScriptAct_SetBool_Target_25);
		local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_25;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_25.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_False_25()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_25.False(out logic_uScriptAct_SetBool_Target_25);
		local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_25;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_25.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_In_26()
	{
		logic_uScript_AddMessage_messageData_26 = msgIntro;
		logic_uScript_AddMessage_speaker_26 = messageSpeaker;
		logic_uScript_AddMessage_Return_26 = logic_uScript_AddMessage_uScript_AddMessage_26.In(logic_uScript_AddMessage_messageData_26, logic_uScript_AddMessage_speaker_26);
		if (logic_uScript_AddMessage_uScript_AddMessage_26.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_Save_Out_31()
	{
	}

	private void Relay_Load_Out_31()
	{
	}

	private void Relay_Restart_Out_31()
	{
	}

	private void Relay_Save_31()
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_31 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Save(ref logic_SubGraph_SaveLoadBool_boolean_31, logic_SubGraph_SaveLoadBool_boolAsVariable_31, logic_SubGraph_SaveLoadBool_uniqueID_31);
	}

	private void Relay_Load_31()
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_31 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Load(ref logic_SubGraph_SaveLoadBool_boolean_31, logic_SubGraph_SaveLoadBool_boolAsVariable_31, logic_SubGraph_SaveLoadBool_uniqueID_31);
	}

	private void Relay_Set_True_31()
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_31 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_31, logic_SubGraph_SaveLoadBool_boolAsVariable_31, logic_SubGraph_SaveLoadBool_uniqueID_31);
	}

	private void Relay_Set_False_31()
	{
		logic_SubGraph_SaveLoadBool_boolean_31 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_31 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_31.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_31, logic_SubGraph_SaveLoadBool_boolAsVariable_31, logic_SubGraph_SaveLoadBool_uniqueID_31);
	}

	private void Relay_SaveEvent_32()
	{
		Relay_Save_31();
	}

	private void Relay_LoadEvent_32()
	{
		Relay_Load_31();
	}

	private void Relay_RestartEvent_32()
	{
		Relay_Set_False_31();
	}
}
