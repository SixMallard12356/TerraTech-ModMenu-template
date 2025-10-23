using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_HarvestResources : uScriptLogic
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

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_9;

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

	private ChunkTypes event_UnityEngine_GameObject_ResourceType_0;

	private int event_UnityEngine_GameObject_ResourceTypeTotal_0;

	private int event_UnityEngine_GameObject_HarvestedTotal_0;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
			if (null != owner_Connection_2)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_2.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_4;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_4;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_4;
				}
			}
		}
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
			if (null != owner_Connection_3)
			{
				uScript_ResourceHarvestedEvent uScript_ResourceHarvestedEvent2 = owner_Connection_3.GetComponent<uScript_ResourceHarvestedEvent>();
				if (null == uScript_ResourceHarvestedEvent2)
				{
					uScript_ResourceHarvestedEvent2 = owner_Connection_3.AddComponent<uScript_ResourceHarvestedEvent>();
				}
				if (null != uScript_ResourceHarvestedEvent2)
				{
					uScript_ResourceHarvestedEvent2.ResourceHarvestedEvent += Instance_ResourceHarvestedEvent_0;
				}
			}
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_2)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_2.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_4;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_4;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_4;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_3)
		{
			uScript_ResourceHarvestedEvent uScript_ResourceHarvestedEvent2 = owner_Connection_3.GetComponent<uScript_ResourceHarvestedEvent>();
			if (null == uScript_ResourceHarvestedEvent2)
			{
				uScript_ResourceHarvestedEvent2 = owner_Connection_3.AddComponent<uScript_ResourceHarvestedEvent>();
			}
			if (null != uScript_ResourceHarvestedEvent2)
			{
				uScript_ResourceHarvestedEvent2.ResourceHarvestedEvent += Instance_ResourceHarvestedEvent_0;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_2)
		{
			uScript_EncounterUpdate component = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_4;
				component.OnSuspend -= Instance_OnSuspend_4;
				component.OnResume -= Instance_OnResume_4;
			}
		}
		if (null != owner_Connection_3)
		{
			uScript_ResourceHarvestedEvent component2 = owner_Connection_3.GetComponent<uScript_ResourceHarvestedEvent>();
			if (null != component2)
			{
				component2.ResourceHarvestedEvent -= Instance_ResourceHarvestedEvent_0;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_10.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_15.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_9 = parentGameObject;
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

	private void Instance_ResourceHarvestedEvent_0(object o, uScript_ResourceHarvestedEvent.ResourceHarvestedEventArgs e)
	{
		event_UnityEngine_GameObject_ResourceType_0 = e.ResourceType;
		event_UnityEngine_GameObject_ResourceTypeTotal_0 = e.ResourceTypeTotal;
		event_UnityEngine_GameObject_HarvestedTotal_0 = e.HarvestedTotal;
		Relay_ResourceHarvestedEvent_0();
	}

	private void Instance_OnUpdate_4(object o, EventArgs e)
	{
		Relay_OnUpdate_4();
	}

	private void Instance_OnSuspend_4(object o, EventArgs e)
	{
		Relay_OnSuspend_4();
	}

	private void Instance_OnResume_4(object o, EventArgs e)
	{
		Relay_OnResume_4();
	}

	private void Subgraph_Stats_ResourceTracker_Target_Reached_10(object o, Subgraph_Stats_ResourceTracker.LogicEventArgs e)
	{
		Relay_Target_Reached_10();
	}

	private void Relay_ResourceHarvestedEvent_0()
	{
		local_ResourceType_ChunkTypes = event_UnityEngine_GameObject_ResourceType_0;
		local_CurrentAmountType_System_Int32 = event_UnityEngine_GameObject_ResourceTypeTotal_0;
		local_CurrentAmountTotal_System_Int32 = event_UnityEngine_GameObject_HarvestedTotal_0;
		Relay_Event_Fired_10();
	}

	private void Relay_OnUpdate_4()
	{
		Relay_Init_Harvest_10();
	}

	private void Relay_OnSuspend_4()
	{
	}

	private void Relay_OnResume_4()
	{
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

	private void Relay_Succeed_15()
	{
		logic_uScript_FinishEncounter_owner_15 = owner_Connection_9;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_15.Succeed(logic_uScript_FinishEncounter_owner_15);
	}

	private void Relay_Fail_15()
	{
		logic_uScript_FinishEncounter_owner_15 = owner_Connection_9;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_15.Fail(logic_uScript_FinishEncounter_owner_15);
	}
}
