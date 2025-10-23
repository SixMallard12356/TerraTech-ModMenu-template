using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_DestroyRocks : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SceneryTypes[] local_Rocks_SceneryTypesArray = new SceneryTypes[10]
	{
		SceneryTypes.SmokerRock,
		SceneryTypes.DesertRock,
		SceneryTypes.CarbiteSeam,
		SceneryTypes.OleiteSeam,
		SceneryTypes.PlumbiteSeam,
		SceneryTypes.MountainRock,
		SceneryTypes.GrasslandRock,
		SceneryTypes.RoditeSeam,
		SceneryTypes.TitaniteSeam,
		SceneryTypes.WastelandRock
	};

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public uScript_AddMessage.MessageData msgComplete;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_4;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_3 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_3;

	private bool logic_uScript_FinishEncounter_Out_3 = true;

	private SubGraph_Stats_DestroySceneryTypes logic_SubGraph_Stats_DestroySceneryTypes_SubGraph_Stats_DestroySceneryTypes_5 = new SubGraph_Stats_DestroySceneryTypes();

	private SceneryTypes[] logic_SubGraph_Stats_DestroySceneryTypes_sceneryTypes_5 = new SceneryTypes[0];

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Stats_DestroySceneryTypes_messageSpeaker_5;

	private uScript_AddMessage.MessageData logic_SubGraph_Stats_DestroySceneryTypes_msgComplete_5;

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
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
				}
			}
		}
		if (null == owner_Connection_4 || !m_RegisteredForEvents)
		{
			owner_Connection_4 = parentGameObject;
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
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
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
				component.OnUpdate -= Instance_OnUpdate_1;
				component.OnSuspend -= Instance_OnSuspend_1;
				component.OnResume -= Instance_OnResume_1;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_3.SetParent(g);
		logic_SubGraph_Stats_DestroySceneryTypes_SubGraph_Stats_DestroySceneryTypes_5.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_4 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_Stats_DestroySceneryTypes_SubGraph_Stats_DestroySceneryTypes_5.Awake();
		logic_SubGraph_Stats_DestroySceneryTypes_SubGraph_Stats_DestroySceneryTypes_5.Target_Reached += SubGraph_Stats_DestroySceneryTypes_Target_Reached_5;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_Stats_DestroySceneryTypes_SubGraph_Stats_DestroySceneryTypes_5.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_Stats_DestroySceneryTypes_SubGraph_Stats_DestroySceneryTypes_5.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_Stats_DestroySceneryTypes_SubGraph_Stats_DestroySceneryTypes_5.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_Stats_DestroySceneryTypes_SubGraph_Stats_DestroySceneryTypes_5.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_Stats_DestroySceneryTypes_SubGraph_Stats_DestroySceneryTypes_5.OnDestroy();
		logic_SubGraph_Stats_DestroySceneryTypes_SubGraph_Stats_DestroySceneryTypes_5.Target_Reached -= SubGraph_Stats_DestroySceneryTypes_Target_Reached_5;
	}

	private void Instance_OnUpdate_1(object o, EventArgs e)
	{
		Relay_OnUpdate_1();
	}

	private void Instance_OnSuspend_1(object o, EventArgs e)
	{
		Relay_OnSuspend_1();
	}

	private void Instance_OnResume_1(object o, EventArgs e)
	{
		Relay_OnResume_1();
	}

	private void SubGraph_Stats_DestroySceneryTypes_Target_Reached_5(object o, SubGraph_Stats_DestroySceneryTypes.LogicEventArgs e)
	{
		Relay_Target_Reached_5();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_5();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Succeed_3()
	{
		logic_uScript_FinishEncounter_owner_3 = owner_Connection_4;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_3.Succeed(logic_uScript_FinishEncounter_owner_3);
	}

	private void Relay_Fail_3()
	{
		logic_uScript_FinishEncounter_owner_3 = owner_Connection_4;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_3.Fail(logic_uScript_FinishEncounter_owner_3);
	}

	private void Relay_Target_Reached_5()
	{
		Relay_Succeed_3();
	}

	private void Relay_In_5()
	{
		int num = 0;
		Array array = local_Rocks_SceneryTypesArray;
		if (logic_SubGraph_Stats_DestroySceneryTypes_sceneryTypes_5.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Stats_DestroySceneryTypes_sceneryTypes_5, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Stats_DestroySceneryTypes_sceneryTypes_5, num, array.Length);
		num += array.Length;
		logic_SubGraph_Stats_DestroySceneryTypes_messageSpeaker_5 = messageSpeaker;
		logic_SubGraph_Stats_DestroySceneryTypes_msgComplete_5 = msgComplete;
		logic_SubGraph_Stats_DestroySceneryTypes_SubGraph_Stats_DestroySceneryTypes_5.In(logic_SubGraph_Stats_DestroySceneryTypes_sceneryTypes_5, logic_SubGraph_Stats_DestroySceneryTypes_messageSpeaker_5, logic_SubGraph_Stats_DestroySceneryTypes_msgComplete_5);
	}
}
