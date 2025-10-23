using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_SellResources : uScriptLogic
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

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_14;

	private Subgraph_Stats_ResourceTracker logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0 = new Subgraph_Stats_ResourceTracker();

	private ChunkTypes logic_Subgraph_Stats_ResourceTracker_resourceType_0;

	private int logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_0;

	private int logic_Subgraph_Stats_ResourceTracker_resourceTotal_0;

	private bool logic_Subgraph_Stats_ResourceTracker_useResourceType_0;

	private ChunkTypes logic_Subgraph_Stats_ResourceTracker_targetResourceType_0;

	private uScript_AddMessage.MessageSpeaker logic_Subgraph_Stats_ResourceTracker_messageSpeaker_0;

	private uScript_AddMessage.MessageData logic_Subgraph_Stats_ResourceTracker_msgComplete_0;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_15 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_15;

	private bool logic_uScript_FinishEncounter_Out_15 = true;

	private ChunkTypes event_UnityEngine_GameObject_ResourceType_12;

	private int event_UnityEngine_GameObject_ResourceTypeTotal_12;

	private int event_UnityEngine_GameObject_SoldTotal_12;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
		}
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
			if (null != owner_Connection_8)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_8.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_8.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_3;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_3;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_3;
				}
			}
		}
		if (!(null == owner_Connection_14) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_14 = parentGameObject;
		if (null != owner_Connection_14)
		{
			uScript_ResourceSoldEvent uScript_ResourceSoldEvent2 = owner_Connection_14.GetComponent<uScript_ResourceSoldEvent>();
			if (null == uScript_ResourceSoldEvent2)
			{
				uScript_ResourceSoldEvent2 = owner_Connection_14.AddComponent<uScript_ResourceSoldEvent>();
			}
			if (null != uScript_ResourceSoldEvent2)
			{
				uScript_ResourceSoldEvent2.ResourceSoldEvent += Instance_ResourceSoldEvent_12;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_8)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_8.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_8.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_3;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_3;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_3;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_14)
		{
			uScript_ResourceSoldEvent uScript_ResourceSoldEvent2 = owner_Connection_14.GetComponent<uScript_ResourceSoldEvent>();
			if (null == uScript_ResourceSoldEvent2)
			{
				uScript_ResourceSoldEvent2 = owner_Connection_14.AddComponent<uScript_ResourceSoldEvent>();
			}
			if (null != uScript_ResourceSoldEvent2)
			{
				uScript_ResourceSoldEvent2.ResourceSoldEvent += Instance_ResourceSoldEvent_12;
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
			uScript_EncounterUpdate component = owner_Connection_8.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_3;
				component.OnSuspend -= Instance_OnSuspend_3;
				component.OnResume -= Instance_OnResume_3;
			}
		}
		if (null != owner_Connection_14)
		{
			uScript_ResourceSoldEvent component2 = owner_Connection_14.GetComponent<uScript_ResourceSoldEvent>();
			if (null != component2)
			{
				component2.ResourceSoldEvent -= Instance_ResourceSoldEvent_12;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_15.SetParent(g);
		owner_Connection_6 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_14 = parentGameObject;
	}

	public void Awake()
	{
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.Awake();
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.Target_Reached += Subgraph_Stats_ResourceTracker_Target_Reached_0;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.OnEnable();
	}

	public void OnDisable()
	{
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.Update();
	}

	public void OnDestroy()
	{
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.OnDestroy();
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.Target_Reached -= Subgraph_Stats_ResourceTracker_Target_Reached_0;
	}

	private void Instance_OnUpdate_3(object o, EventArgs e)
	{
		Relay_OnUpdate_3();
	}

	private void Instance_OnSuspend_3(object o, EventArgs e)
	{
		Relay_OnSuspend_3();
	}

	private void Instance_OnResume_3(object o, EventArgs e)
	{
		Relay_OnResume_3();
	}

	private void Instance_ResourceSoldEvent_12(object o, uScript_ResourceSoldEvent.ResourceSoldEventArgs e)
	{
		event_UnityEngine_GameObject_ResourceType_12 = e.ResourceType;
		event_UnityEngine_GameObject_ResourceTypeTotal_12 = e.ResourceTypeTotal;
		event_UnityEngine_GameObject_SoldTotal_12 = e.SoldTotal;
		Relay_ResourceSoldEvent_12();
	}

	private void Subgraph_Stats_ResourceTracker_Target_Reached_0(object o, Subgraph_Stats_ResourceTracker.LogicEventArgs e)
	{
		Relay_Target_Reached_0();
	}

	private void Relay_Target_Reached_0()
	{
		Relay_Succeed_15();
	}

	private void Relay_Init_Harvest_0()
	{
		logic_Subgraph_Stats_ResourceTracker_resourceType_0 = local_ResourceType_ChunkTypes;
		logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_0 = local_CurrentAmountType_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_resourceTotal_0 = local_CurrentAmountTotal_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_useResourceType_0 = useResourceType;
		logic_Subgraph_Stats_ResourceTracker_targetResourceType_0 = targetResourceType;
		logic_Subgraph_Stats_ResourceTracker_messageSpeaker_0 = messageSpeaker;
		logic_Subgraph_Stats_ResourceTracker_msgComplete_0 = msgComplete;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.Init_Harvest(logic_Subgraph_Stats_ResourceTracker_resourceType_0, logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_0, logic_Subgraph_Stats_ResourceTracker_resourceTotal_0, logic_Subgraph_Stats_ResourceTracker_useResourceType_0, logic_Subgraph_Stats_ResourceTracker_targetResourceType_0, logic_Subgraph_Stats_ResourceTracker_messageSpeaker_0, logic_Subgraph_Stats_ResourceTracker_msgComplete_0);
	}

	private void Relay_Init_Refine_0()
	{
		logic_Subgraph_Stats_ResourceTracker_resourceType_0 = local_ResourceType_ChunkTypes;
		logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_0 = local_CurrentAmountType_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_resourceTotal_0 = local_CurrentAmountTotal_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_useResourceType_0 = useResourceType;
		logic_Subgraph_Stats_ResourceTracker_targetResourceType_0 = targetResourceType;
		logic_Subgraph_Stats_ResourceTracker_messageSpeaker_0 = messageSpeaker;
		logic_Subgraph_Stats_ResourceTracker_msgComplete_0 = msgComplete;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.Init_Refine(logic_Subgraph_Stats_ResourceTracker_resourceType_0, logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_0, logic_Subgraph_Stats_ResourceTracker_resourceTotal_0, logic_Subgraph_Stats_ResourceTracker_useResourceType_0, logic_Subgraph_Stats_ResourceTracker_targetResourceType_0, logic_Subgraph_Stats_ResourceTracker_messageSpeaker_0, logic_Subgraph_Stats_ResourceTracker_msgComplete_0);
	}

	private void Relay_Init_Sell_0()
	{
		logic_Subgraph_Stats_ResourceTracker_resourceType_0 = local_ResourceType_ChunkTypes;
		logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_0 = local_CurrentAmountType_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_resourceTotal_0 = local_CurrentAmountTotal_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_useResourceType_0 = useResourceType;
		logic_Subgraph_Stats_ResourceTracker_targetResourceType_0 = targetResourceType;
		logic_Subgraph_Stats_ResourceTracker_messageSpeaker_0 = messageSpeaker;
		logic_Subgraph_Stats_ResourceTracker_msgComplete_0 = msgComplete;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.Init_Sell(logic_Subgraph_Stats_ResourceTracker_resourceType_0, logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_0, logic_Subgraph_Stats_ResourceTracker_resourceTotal_0, logic_Subgraph_Stats_ResourceTracker_useResourceType_0, logic_Subgraph_Stats_ResourceTracker_targetResourceType_0, logic_Subgraph_Stats_ResourceTracker_messageSpeaker_0, logic_Subgraph_Stats_ResourceTracker_msgComplete_0);
	}

	private void Relay_Event_Fired_0()
	{
		logic_Subgraph_Stats_ResourceTracker_resourceType_0 = local_ResourceType_ChunkTypes;
		logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_0 = local_CurrentAmountType_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_resourceTotal_0 = local_CurrentAmountTotal_System_Int32;
		logic_Subgraph_Stats_ResourceTracker_useResourceType_0 = useResourceType;
		logic_Subgraph_Stats_ResourceTracker_targetResourceType_0 = targetResourceType;
		logic_Subgraph_Stats_ResourceTracker_messageSpeaker_0 = messageSpeaker;
		logic_Subgraph_Stats_ResourceTracker_msgComplete_0 = msgComplete;
		logic_Subgraph_Stats_ResourceTracker_Subgraph_Stats_ResourceTracker_0.Event_Fired(logic_Subgraph_Stats_ResourceTracker_resourceType_0, logic_Subgraph_Stats_ResourceTracker_resourceTypeTotal_0, logic_Subgraph_Stats_ResourceTracker_resourceTotal_0, logic_Subgraph_Stats_ResourceTracker_useResourceType_0, logic_Subgraph_Stats_ResourceTracker_targetResourceType_0, logic_Subgraph_Stats_ResourceTracker_messageSpeaker_0, logic_Subgraph_Stats_ResourceTracker_msgComplete_0);
	}

	private void Relay_OnUpdate_3()
	{
		Relay_Init_Sell_0();
	}

	private void Relay_OnSuspend_3()
	{
	}

	private void Relay_OnResume_3()
	{
	}

	private void Relay_ResourceSoldEvent_12()
	{
		local_ResourceType_ChunkTypes = event_UnityEngine_GameObject_ResourceType_12;
		local_CurrentAmountType_System_Int32 = event_UnityEngine_GameObject_ResourceTypeTotal_12;
		local_CurrentAmountTotal_System_Int32 = event_UnityEngine_GameObject_SoldTotal_12;
		Relay_Event_Fired_0();
	}

	private void Relay_Succeed_15()
	{
		logic_uScript_FinishEncounter_owner_15 = owner_Connection_6;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_15.Succeed(logic_uScript_FinishEncounter_owner_15);
	}

	private void Relay_Fail_15()
	{
		logic_uScript_FinishEncounter_owner_15 = owner_Connection_6;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_15.Fail(logic_uScript_FinishEncounter_owner_15);
	}
}
