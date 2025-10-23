using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_RefineResources : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private int local_CurrentAmountTotal_System_Int32;

	private int local_CurrentAmountType_System_Int32;

	private ChunkTypes local_ResourceType_ChunkTypes;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public uScript_AddMessage.MessageData msgComplete;

	public ChunkTypes targetResourceType;

	public bool useResourceType;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_12;

	private Subgraph_Stats_ResourceTracker logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10 = new Subgraph_Stats_ResourceTracker();

	private ChunkTypes logic_Subgraph_Stats_ResourceTracker_resourceType_10;

	private int logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_10;

	private int logic_Subgraph_Stats_ResourceTracker_resourceTotal_10;

	private bool logic_Subgraph_Stats_ResourceTracker_useResourceType_10;

	private ChunkTypes logic_Subgraph_Stats_ResourceTracker_targetResourceType_10;

	private uScript_AddMessage.MessageSpeaker logic_Subgraph_Stats_ResourceTracker_messageSpeaker_10;

	private uScript_AddMessage.MessageData logic_Subgraph_Stats_ResourceTracker_msgComplete_10;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_15 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_15;

	private bool logic_uScript_FinishEncounter_Out_15 = true;

	private ChunkTypes event_UnityEngine_GameObject_ResourceType_3;

	private int event_UnityEngine_GameObject_ResourceTypeTotal_3;

	private int event_UnityEngine_GameObject_RefinedTotal_3;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_ResourceRefinedEvent uScript_ResourceRefinedEvent2 = owner_Connection_1.GetComponent<uScript_ResourceRefinedEvent>();
				if (null == uScript_ResourceRefinedEvent2)
				{
					uScript_ResourceRefinedEvent2 = owner_Connection_1.AddComponent<uScript_ResourceRefinedEvent>();
				}
				if (null != uScript_ResourceRefinedEvent2)
				{
					uScript_ResourceRefinedEvent2.ResourceRefinedEvent += Instance_ResourceRefinedEvent_3;
				}
			}
		}
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
		}
		if (!(null == owner_Connection_12) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_12 = parentGameObject;
		if (null != owner_Connection_12)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_12.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_12.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_13;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_13;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_13;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_ResourceRefinedEvent uScript_ResourceRefinedEvent2 = owner_Connection_1.GetComponent<uScript_ResourceRefinedEvent>();
			if (null == uScript_ResourceRefinedEvent2)
			{
				uScript_ResourceRefinedEvent2 = owner_Connection_1.AddComponent<uScript_ResourceRefinedEvent>();
			}
			if (null != uScript_ResourceRefinedEvent2)
			{
				uScript_ResourceRefinedEvent2.ResourceRefinedEvent += Instance_ResourceRefinedEvent_3;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_12)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_12.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_12.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_13;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_13;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_13;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_1)
		{
			uScript_ResourceRefinedEvent component = owner_Connection_1.GetComponent<uScript_ResourceRefinedEvent>();
			if (null != component)
			{
				component.ResourceRefinedEvent -= Instance_ResourceRefinedEvent_3;
			}
		}
		if (null != owner_Connection_12)
		{
			uScript_EncounterUpdate component2 = owner_Connection_12.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_13;
				component2.OnSuspend -= Instance_OnSuspend_13;
				component2.OnResume -= Instance_OnResume_13;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_15.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_12 = parentGameObject;
	}

	public void Awake()
	{
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.Awake();
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.Target_Reached += Subgraph_Stats_ResourceTracker_Target_Reached_10;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.OnEnable();
	}

	public void OnDisable()
	{
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.Update();
	}

	public void OnDestroy()
	{
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.OnDestroy();
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.Target_Reached -= Subgraph_Stats_ResourceTracker_Target_Reached_10;
	}

	private void Instance_ResourceRefinedEvent_3(object o, uScript_ResourceRefinedEvent.ResourceRefinedEventArgs e)
	{
		event_UnityEngine_GameObject_ResourceType_3 = e.ResourceType;
		event_UnityEngine_GameObject_ResourceTypeTotal_3 = e.ResourceTypeTotal;
		event_UnityEngine_GameObject_RefinedTotal_3 = e.RefinedTotal;
		Relay_ResourceRefinedEvent_3();
	}

	private void Instance_OnUpdate_13(object o, EventArgs e)
	{
		Relay_OnUpdate_13();
	}

	private void Instance_OnSuspend_13(object o, EventArgs e)
	{
		Relay_OnSuspend_13();
	}

	private void Instance_OnResume_13(object o, EventArgs e)
	{
		Relay_OnResume_13();
	}

	private void Subgraph_Stats_ResourceTracker_Target_Reached_10(object o, Subgraph_Stats_ResourceTracker.LogicEventArgs e)
	{
		Relay_Target_Reached_10();
	}

	private void Relay_ResourceRefinedEvent_3()
	{
		local_ResourceType_ChunkTypes = event_UnityEngine_GameObject_ResourceType_3;
		local_CurrentAmountType_System_Int32 = event_UnityEngine_GameObject_ResourceTypeTotal_3;
		local_CurrentAmountTotal_System_Int32 = event_UnityEngine_GameObject_RefinedTotal_3;
		Relay_Event_Fired_10();
	}

	private void Relay_Target_Reached_10()
	{
		Relay_Succeed_15();
	}

	private void Relay_Init_Harvest_10()
	{
		logic_Subgraph_Stats_ResourceTracker_resourceType_10 = local_ResourceType_ChunkTypes;
		logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_10 = local_CurrentAmountType_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_resourceTotal_10 = local_CurrentAmountTotal_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_useResourceType_10 = useResourceType;
		logic_Subgraph_Stats_ResourceTracker_targetResourceType_10 = targetResourceType;
		logic_Subgraph_Stats_ResourceTracker_messageSpeaker_10 = messageSpeaker;
		logic_Subgraph_Stats_ResourceTracker_msgComplete_10 = msgComplete;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.Init_Harvest(logic_Subgraph_Stats_ResourceTracker_resourceType_10, logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_10, logic_Subgraph_Stats_ResourceTracker_resourceTotal_10, logic_Subgraph_Stats_ResourceTracker_useResourceType_10, logic_Subgraph_Stats_ResourceTracker_targetResourceType_10, logic_Subgraph_Stats_ResourceTracker_messageSpeaker_10, logic_Subgraph_Stats_ResourceTracker_msgComplete_10);
	}

	private void Relay_Init_Refine_10()
	{
		logic_Subgraph_Stats_ResourceTracker_resourceType_10 = local_ResourceType_ChunkTypes;
		logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_10 = local_CurrentAmountType_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_resourceTotal_10 = local_CurrentAmountTotal_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_useResourceType_10 = useResourceType;
		logic_Subgraph_Stats_ResourceTracker_targetResourceType_10 = targetResourceType;
		logic_Subgraph_Stats_ResourceTracker_messageSpeaker_10 = messageSpeaker;
		logic_Subgraph_Stats_ResourceTracker_msgComplete_10 = msgComplete;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.Init_Refine(logic_Subgraph_Stats_ResourceTracker_resourceType_10, logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_10, logic_Subgraph_Stats_ResourceTracker_resourceTotal_10, logic_Subgraph_Stats_ResourceTracker_useResourceType_10, logic_Subgraph_Stats_ResourceTracker_targetResourceType_10, logic_Subgraph_Stats_ResourceTracker_messageSpeaker_10, logic_Subgraph_Stats_ResourceTracker_msgComplete_10);
	}

	private void Relay_Init_Sell_10()
	{
		logic_Subgraph_Stats_ResourceTracker_resourceType_10 = local_ResourceType_ChunkTypes;
		logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_10 = local_CurrentAmountType_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_resourceTotal_10 = local_CurrentAmountTotal_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_useResourceType_10 = useResourceType;
		logic_Subgraph_Stats_ResourceTracker_targetResourceType_10 = targetResourceType;
		logic_Subgraph_Stats_ResourceTracker_messageSpeaker_10 = messageSpeaker;
		logic_Subgraph_Stats_ResourceTracker_msgComplete_10 = msgComplete;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.Init_Sell(logic_Subgraph_Stats_ResourceTracker_resourceType_10, logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_10, logic_Subgraph_Stats_ResourceTracker_resourceTotal_10, logic_Subgraph_Stats_ResourceTracker_useResourceType_10, logic_Subgraph_Stats_ResourceTracker_targetResourceType_10, logic_Subgraph_Stats_ResourceTracker_messageSpeaker_10, logic_Subgraph_Stats_ResourceTracker_msgComplete_10);
	}

	private void Relay_Event_Fired_10()
	{
		logic_Subgraph_Stats_ResourceTracker_resourceType_10 = local_ResourceType_ChunkTypes;
		logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_10 = local_CurrentAmountType_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_resourceTotal_10 = local_CurrentAmountTotal_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_useResourceType_10 = useResourceType;
		logic_Subgraph_Stats_ResourceTracker_targetResourceType_10 = targetResourceType;
		logic_Subgraph_Stats_ResourceTracker_messageSpeaker_10 = messageSpeaker;
		logic_Subgraph_Stats_ResourceTracker_msgComplete_10 = msgComplete;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.Event_Fired(logic_Subgraph_Stats_ResourceTracker_resourceType_10, logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_10, logic_Subgraph_Stats_ResourceTracker_resourceTotal_10, logic_Subgraph_Stats_ResourceTracker_useResourceType_10, logic_Subgraph_Stats_ResourceTracker_targetResourceType_10, logic_Subgraph_Stats_ResourceTracker_messageSpeaker_10, logic_Subgraph_Stats_ResourceTracker_msgComplete_10);
	}

	private void Relay_OnUpdate_13()
	{
		Relay_Init_Refine_10();
	}

	private void Relay_OnSuspend_13()
	{
	}

	private void Relay_OnResume_13()
	{
	}

	private void Relay_Succeed_15()
	{
		logic_uScript_FinishEncounter_owner_15 = owner_Connection_7;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_15.Succeed(logic_uScript_FinishEncounter_owner_15);
	}

	private void Relay_Fail_15()
	{
		logic_uScript_FinishEncounter_owner_15 = owner_Connection_7;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_15.Fail(logic_uScript_FinishEncounter_owner_15);
	}
}
