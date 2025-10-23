using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_PursueFleeingTech : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public float caughtUpDistance = 10f;

	public SpawnTechData fleeingTechData;

	public bool keepFleeingTechAlive;

	public LocalisedString[] msgCaughtUp = new LocalisedString[0];

	public LocalisedString[] msgIntro = new LocalisedString[0];

	public LocalisedString[] msgTechDestroyed = new LocalisedString[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_17;

	private SubGraph_PursueFleeingTech logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6 = new SubGraph_PursueFleeingTech();

	private SpawnTechData[] logic_SubGraph_PursueFleeingTech_fleeingTechData_6 = new SpawnTechData[0];

	private bool logic_SubGraph_PursueFleeingTech_keepFleeingTechAlive_6;

	private float logic_SubGraph_PursueFleeingTech_caughtUpDistance_6;

	private LocalisedString[] logic_SubGraph_PursueFleeingTech_msgIntro_6 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_PursueFleeingTech_msgCaughtUp_6 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_PursueFleeingTech_msgTechDestroyed_6 = new LocalisedString[0];

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_18 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_18;

	private bool logic_uScript_FinishEncounter_Out_18 = true;

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
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
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
		logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_18.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_17 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6.Awake();
		logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6.Complete += SubGraph_PursueFleeingTech_Complete_6;
		logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6.Failed += SubGraph_PursueFleeingTech_Failed_6;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6.OnDestroy();
		logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6.Complete -= SubGraph_PursueFleeingTech_Complete_6;
		logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6.Failed -= SubGraph_PursueFleeingTech_Failed_6;
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

	private void SubGraph_PursueFleeingTech_Complete_6(object o, SubGraph_PursueFleeingTech.LogicEventArgs e)
	{
		Relay_Complete_6();
	}

	private void SubGraph_PursueFleeingTech_Failed_6(object o, SubGraph_PursueFleeingTech.LogicEventArgs e)
	{
		Relay_Failed_6();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_6();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Complete_6()
	{
		Relay_Succeed_18();
	}

	private void Relay_Failed_6()
	{
		Relay_Fail_18();
	}

	private void Relay_In_6()
	{
		int num = 0;
		if (logic_SubGraph_PursueFleeingTech_fleeingTechData_6.Length <= num)
		{
			Array.Resize(ref logic_SubGraph_PursueFleeingTech_fleeingTechData_6, num + 1);
		}
		logic_SubGraph_PursueFleeingTech_fleeingTechData_6[num++] = fleeingTechData;
		logic_SubGraph_PursueFleeingTech_keepFleeingTechAlive_6 = keepFleeingTechAlive;
		logic_SubGraph_PursueFleeingTech_caughtUpDistance_6 = caughtUpDistance;
		int num2 = 0;
		Array array = msgIntro;
		if (logic_SubGraph_PursueFleeingTech_msgIntro_6.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_PursueFleeingTech_msgIntro_6, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_PursueFleeingTech_msgIntro_6, num2, array.Length);
		num2 += array.Length;
		int num3 = 0;
		Array array2 = msgCaughtUp;
		if (logic_SubGraph_PursueFleeingTech_msgCaughtUp_6.Length != num3 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_PursueFleeingTech_msgCaughtUp_6, num3 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_PursueFleeingTech_msgCaughtUp_6, num3, array2.Length);
		num3 += array2.Length;
		int num4 = 0;
		Array array3 = msgTechDestroyed;
		if (logic_SubGraph_PursueFleeingTech_msgTechDestroyed_6.Length != num4 + array3.Length)
		{
			Array.Resize(ref logic_SubGraph_PursueFleeingTech_msgTechDestroyed_6, num4 + array3.Length);
		}
		Array.Copy(array3, 0, logic_SubGraph_PursueFleeingTech_msgTechDestroyed_6, num4, array3.Length);
		num4 += array3.Length;
		logic_SubGraph_PursueFleeingTech_SubGraph_PursueFleeingTech_6.In(logic_SubGraph_PursueFleeingTech_fleeingTechData_6, logic_SubGraph_PursueFleeingTech_keepFleeingTechAlive_6, logic_SubGraph_PursueFleeingTech_caughtUpDistance_6, logic_SubGraph_PursueFleeingTech_msgIntro_6, logic_SubGraph_PursueFleeingTech_msgCaughtUp_6, logic_SubGraph_PursueFleeingTech_msgTechDestroyed_6);
	}

	private void Relay_Succeed_18()
	{
		logic_uScript_FinishEncounter_owner_18 = owner_Connection_17;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_18.Succeed(logic_uScript_FinishEncounter_owner_18);
	}

	private void Relay_Fail_18()
	{
		logic_uScript_FinishEncounter_owner_18 = owner_Connection_17;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_18.Fail(logic_uScript_FinishEncounter_owner_18);
	}
}
