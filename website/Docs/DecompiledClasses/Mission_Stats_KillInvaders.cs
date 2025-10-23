using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_Stats_KillInvaders : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private int local_CurrentKillTotal_System_Int32;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public uScript_AddMessage.MessageData msgComplete;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_4;

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

	private int event_UnityEngine_GameObject_NumInvadersDestroyed_8;

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
		if (!(null == owner_Connection_4) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_4 = parentGameObject;
		if (null != owner_Connection_4)
		{
			uScript_InvaderDestroyedEvent uScript_InvaderDestroyedEvent2 = owner_Connection_4.GetComponent<uScript_InvaderDestroyedEvent>();
			if (null == uScript_InvaderDestroyedEvent2)
			{
				uScript_InvaderDestroyedEvent2 = owner_Connection_4.AddComponent<uScript_InvaderDestroyedEvent>();
			}
			if (null != uScript_InvaderDestroyedEvent2)
			{
				uScript_InvaderDestroyedEvent2.InvaderDestroyedEvent += Instance_InvaderDestroyedEvent_8;
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
			uScript_InvaderDestroyedEvent uScript_InvaderDestroyedEvent2 = owner_Connection_4.GetComponent<uScript_InvaderDestroyedEvent>();
			if (null == uScript_InvaderDestroyedEvent2)
			{
				uScript_InvaderDestroyedEvent2 = owner_Connection_4.AddComponent<uScript_InvaderDestroyedEvent>();
			}
			if (null != uScript_InvaderDestroyedEvent2)
			{
				uScript_InvaderDestroyedEvent2.InvaderDestroyedEvent += Instance_InvaderDestroyedEvent_8;
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
			uScript_InvaderDestroyedEvent component2 = owner_Connection_4.GetComponent<uScript_InvaderDestroyedEvent>();
			if (null != component2)
			{
				component2.InvaderDestroyedEvent -= Instance_InvaderDestroyedEvent_8;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_6.SetParent(g);
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_4 = parentGameObject;
	}

	public void Awake()
	{
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Awake();
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Target_Reached += Subgraph_Stats_KillTracker_Target_Reached_7;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.OnEnable();
	}

	public void OnDisable()
	{
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Update();
	}

	public void OnDestroy()
	{
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.OnDestroy();
		logic_Subgraph_Stats_KillTracker_Subgraph_Stats_KillTracker_7.Target_Reached -= Subgraph_Stats_KillTracker_Target_Reached_7;
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

	private void Instance_InvaderDestroyedEvent_8(object o, uScript_InvaderDestroyedEvent.InvaderDestroyedEventArgs e)
	{
		event_UnityEngine_GameObject_NumInvadersDestroyed_8 = e.NumInvadersDestroyed;
		Relay_InvaderDestroyedEvent_8();
	}

	private void Subgraph_Stats_KillTracker_Target_Reached_7(object o, Subgraph_Stats_KillTracker.LogicEventArgs e)
	{
		Relay_Target_Reached_7();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_Track_Invaders_7();
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

	private void Relay_InvaderDestroyedEvent_8()
	{
		local_CurrentKillTotal_System_Int32 = event_UnityEngine_GameObject_NumInvadersDestroyed_8;
		Relay_Event_Fired_7();
	}
}
