using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_DefeatOrFleeEnemyAmbush : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public float distEnemyRange = 50f;

	public float distToTriggerAmbush;

	[Multiline(3)]
	public string EncounterCentralPos = "";

	public SpawnTechData[] enemyTechData = new SpawnTechData[0];

	public LocalisedString[] msgAllDeadComplete = new LocalisedString[0];

	public LocalisedString[] msgEnemySpotted = new LocalisedString[0];

	public LocalisedString[] msgFledComplete = new LocalisedString[0];

	public LocalisedString[] msgTrapSprung = new LocalisedString[0];

	public LocalisedString QLFlee;

	public LocalisedString QLTitle;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_15;

	private SubGraph_NavigateToWaypoint logic_SubGraph_NavigateToWaypoint_SubGraph_NavigateToWaypoint_7 = new SubGraph_NavigateToWaypoint();

	private string logic_SubGraph_NavigateToWaypoint_encounterPos_7 = "";

	private float logic_SubGraph_NavigateToWaypoint_distInRangeOfWaypoint_7;

	private ManOnScreenMessages.Speaker logic_SubGraph_NavigateToWaypoint_messageSpeaker_7;

	private LocalisedString[] logic_SubGraph_NavigateToWaypoint_msgComplete_7 = new LocalisedString[0];

	private SubGraph_DefeatOrFleeEnemyTechs logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10 = new SubGraph_DefeatOrFleeEnemyTechs();

	private SpawnTechData[] logic_SubGraph_DefeatOrFleeEnemyTechs_enemyTechData_10 = new SpawnTechData[0];

	private float logic_SubGraph_DefeatOrFleeEnemyTechs_distEnemiesRange_10;

	private LocalisedString[] logic_SubGraph_DefeatOrFleeEnemyTechs_msgEnemySpotted_10 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_DefeatOrFleeEnemyTechs_msgFledComplete_10 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_DefeatOrFleeEnemyTechs_msgAllDeadComplete_10 = new LocalisedString[0];

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_14 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_14;

	private bool logic_uScript_FinishEncounter_Out_14 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
			if (null != owner_Connection_0)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_0.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
				}
			}
		}
		if (null == owner_Connection_15 || !m_RegisteredForEvents)
		{
			owner_Connection_15 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_0)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_0.AddComponent<uScript_EncounterUpdate>();
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
		if (null != owner_Connection_0)
		{
			uScript_EncounterUpdate component = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
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
		logic_SubGraph_NavigateToWaypoint_SubGraph_NavigateToWaypoint_7.SetParent(g);
		logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_14.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_15 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_NavigateToWaypoint_SubGraph_NavigateToWaypoint_7.Awake();
		logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10.Awake();
		logic_SubGraph_NavigateToWaypoint_SubGraph_NavigateToWaypoint_7.Complete += SubGraph_NavigateToWaypoint_Complete_7;
		logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10.FledComplete += SubGraph_DefeatOrFleeEnemyTechs_FledComplete_10;
		logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10.DeadComplete += SubGraph_DefeatOrFleeEnemyTechs_DeadComplete_10;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_NavigateToWaypoint_SubGraph_NavigateToWaypoint_7.Start();
		logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_NavigateToWaypoint_SubGraph_NavigateToWaypoint_7.OnEnable();
		logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_NavigateToWaypoint_SubGraph_NavigateToWaypoint_7.OnDisable();
		logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_NavigateToWaypoint_SubGraph_NavigateToWaypoint_7.Update();
		logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_NavigateToWaypoint_SubGraph_NavigateToWaypoint_7.OnDestroy();
		logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10.OnDestroy();
		logic_SubGraph_NavigateToWaypoint_SubGraph_NavigateToWaypoint_7.Complete -= SubGraph_NavigateToWaypoint_Complete_7;
		logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10.FledComplete -= SubGraph_DefeatOrFleeEnemyTechs_FledComplete_10;
		logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10.DeadComplete -= SubGraph_DefeatOrFleeEnemyTechs_DeadComplete_10;
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

	private void SubGraph_NavigateToWaypoint_Complete_7(object o, SubGraph_NavigateToWaypoint.LogicEventArgs e)
	{
		Relay_Complete_7();
	}

	private void SubGraph_DefeatOrFleeEnemyTechs_FledComplete_10(object o, SubGraph_DefeatOrFleeEnemyTechs.LogicEventArgs e)
	{
		Relay_FledComplete_10();
	}

	private void SubGraph_DefeatOrFleeEnemyTechs_DeadComplete_10(object o, SubGraph_DefeatOrFleeEnemyTechs.LogicEventArgs e)
	{
		Relay_DeadComplete_10();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_7();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Complete_7()
	{
		Relay_In_10();
	}

	private void Relay_In_7()
	{
		logic_SubGraph_NavigateToWaypoint_encounterPos_7 = EncounterCentralPos;
		logic_SubGraph_NavigateToWaypoint_distInRangeOfWaypoint_7 = distToTriggerAmbush;
		int num = 0;
		Array array = msgTrapSprung;
		if (logic_SubGraph_NavigateToWaypoint_msgComplete_7.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_NavigateToWaypoint_msgComplete_7, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_NavigateToWaypoint_msgComplete_7, num, array.Length);
		num += array.Length;
		logic_SubGraph_NavigateToWaypoint_SubGraph_NavigateToWaypoint_7.In(logic_SubGraph_NavigateToWaypoint_encounterPos_7, logic_SubGraph_NavigateToWaypoint_distInRangeOfWaypoint_7, logic_SubGraph_NavigateToWaypoint_messageSpeaker_7, logic_SubGraph_NavigateToWaypoint_msgComplete_7);
	}

	private void Relay_FledComplete_10()
	{
		Relay_Succeed_14();
	}

	private void Relay_DeadComplete_10()
	{
		Relay_Succeed_14();
	}

	private void Relay_In_10()
	{
		int num = 0;
		Array array = enemyTechData;
		if (logic_SubGraph_DefeatOrFleeEnemyTechs_enemyTechData_10.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_DefeatOrFleeEnemyTechs_enemyTechData_10, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_DefeatOrFleeEnemyTechs_enemyTechData_10, num, array.Length);
		num += array.Length;
		logic_SubGraph_DefeatOrFleeEnemyTechs_distEnemiesRange_10 = distEnemyRange;
		int num2 = 0;
		Array array2 = msgEnemySpotted;
		if (logic_SubGraph_DefeatOrFleeEnemyTechs_msgEnemySpotted_10.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_DefeatOrFleeEnemyTechs_msgEnemySpotted_10, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_DefeatOrFleeEnemyTechs_msgEnemySpotted_10, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array array3 = msgFledComplete;
		if (logic_SubGraph_DefeatOrFleeEnemyTechs_msgFledComplete_10.Length != num3 + array3.Length)
		{
			Array.Resize(ref logic_SubGraph_DefeatOrFleeEnemyTechs_msgFledComplete_10, num3 + array3.Length);
		}
		Array.Copy(array3, 0, logic_SubGraph_DefeatOrFleeEnemyTechs_msgFledComplete_10, num3, array3.Length);
		num3 += array3.Length;
		int num4 = 0;
		Array array4 = msgAllDeadComplete;
		if (logic_SubGraph_DefeatOrFleeEnemyTechs_msgAllDeadComplete_10.Length != num4 + array4.Length)
		{
			Array.Resize(ref logic_SubGraph_DefeatOrFleeEnemyTechs_msgAllDeadComplete_10, num4 + array4.Length);
		}
		Array.Copy(array4, 0, logic_SubGraph_DefeatOrFleeEnemyTechs_msgAllDeadComplete_10, num4, array4.Length);
		num4 += array4.Length;
		logic_SubGraph_DefeatOrFleeEnemyTechs_SubGraph_DefeatOrFleeEnemyTechs_10.In(logic_SubGraph_DefeatOrFleeEnemyTechs_enemyTechData_10, logic_SubGraph_DefeatOrFleeEnemyTechs_distEnemiesRange_10, logic_SubGraph_DefeatOrFleeEnemyTechs_msgEnemySpotted_10, logic_SubGraph_DefeatOrFleeEnemyTechs_msgFledComplete_10, logic_SubGraph_DefeatOrFleeEnemyTechs_msgAllDeadComplete_10);
	}

	private void Relay_Succeed_14()
	{
		logic_uScript_FinishEncounter_owner_14 = owner_Connection_15;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_14.Succeed(logic_uScript_FinishEncounter_owner_14);
	}

	private void Relay_Fail_14()
	{
		logic_uScript_FinishEncounter_owner_14 = owner_Connection_15;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_14.Fail(logic_uScript_FinishEncounter_owner_14);
	}
}
