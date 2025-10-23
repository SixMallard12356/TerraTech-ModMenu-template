using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_Unlock_License_MP : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_7;

	private uScript_CloseMissionBoard logic_uScript_CloseMissionBoard_uScript_CloseMissionBoard_3 = new uScript_CloseMissionBoard();

	private bool logic_uScript_CloseMissionBoard_Out_3 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_5 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_5;

	private bool logic_uScript_FinishEncounter_Out_5 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_1.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
				}
			}
		}
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_1.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
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
			uScript_EncounterUpdate component = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_0;
				component.OnSuspend -= Instance_OnSuspend_0;
				component.OnResume -= Instance_OnResume_0;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_CloseMissionBoard_uScript_CloseMissionBoard_3.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_5.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_7 = parentGameObject;
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
		logic_uScript_CloseMissionBoard_uScript_CloseMissionBoard_3.OnEnable();
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

	private void Relay_OnUpdate_0()
	{
		Relay_In_3();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_In_3()
	{
		logic_uScript_CloseMissionBoard_uScript_CloseMissionBoard_3.In();
		if (logic_uScript_CloseMissionBoard_uScript_CloseMissionBoard_3.Out)
		{
			Relay_Succeed_5();
		}
	}

	private void Relay_Succeed_5()
	{
		logic_uScript_FinishEncounter_owner_5 = owner_Connection_7;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_5.Succeed(logic_uScript_FinishEncounter_owner_5);
	}

	private void Relay_Fail_5()
	{
		logic_uScript_FinishEncounter_owner_5 = owner_Connection_7;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_5.Fail(logic_uScript_FinishEncounter_owner_5);
	}
}
