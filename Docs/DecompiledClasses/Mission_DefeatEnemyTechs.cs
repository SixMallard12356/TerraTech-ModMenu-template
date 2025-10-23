using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_DefeatEnemyTechs : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius = 50f;

	public float distEnemiesSpotted;

	public SpawnTechData[] enemyTechData = new SpawnTechData[0];

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgComplete = new LocalisedString[0];

	public LocalisedString[] msgEnemiesSpotted = new LocalisedString[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_11;

	private SubGraph_DefeatEnemyTechs logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_3 = new SubGraph_DefeatEnemyTechs();

	private SpawnTechData[] logic_SubGraph_DefeatEnemyTechs_enemyTechData_3 = new SpawnTechData[0];

	private float logic_SubGraph_DefeatEnemyTechs_distEnemiesSpotted_3;

	private string logic_SubGraph_DefeatEnemyTechs_clearSceneryPos_3 = "";

	private float logic_SubGraph_DefeatEnemyTechs_clearSceneryRadius_3;

	private LocalisedString[] logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_3 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_DefeatEnemyTechs_msgComplete_3 = new LocalisedString[0];

	private ManOnScreenMessages.Speaker logic_SubGraph_DefeatEnemyTechs_messageSpeaker_3;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_6 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_6;

	private bool logic_uScript_FinishEncounter_Out_6 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_8 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_8 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_8;

	private string logic_uScript_AddOnScreenMessage_tag_8 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_8;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_8;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_8;

	private bool logic_uScript_AddOnScreenMessage_Out_8 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_8 = true;

	private uScript_IsCurrentEncounterInQuestLog logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_10 = new uScript_IsCurrentEncounterInQuestLog();

	private GameObject logic_uScript_IsCurrentEncounterInQuestLog_owner_10;

	private bool logic_uScript_IsCurrentEncounterInQuestLog_Out_10 = true;

	private bool logic_uScript_IsCurrentEncounterInQuestLog_True_10 = true;

	private bool logic_uScript_IsCurrentEncounterInQuestLog_False_10 = true;

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
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
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
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_3.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_6.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8.SetParent(g);
		logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_10.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_11 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_3.Awake();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_3.Complete += SubGraph_DefeatEnemyTechs_Complete_3;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_3.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_3.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_3.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_3.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_3.OnDestroy();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_3.Complete -= SubGraph_DefeatEnemyTechs_Complete_3;
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

	private void SubGraph_DefeatEnemyTechs_Complete_3(object o, SubGraph_DefeatEnemyTechs.LogicEventArgs e)
	{
		Relay_Complete_3();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_3();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Complete_3()
	{
		Relay_In_10();
	}

	private void Relay_In_3()
	{
		int num = 0;
		Array array = enemyTechData;
		if (logic_SubGraph_DefeatEnemyTechs_enemyTechData_3.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_DefeatEnemyTechs_enemyTechData_3, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_DefeatEnemyTechs_enemyTechData_3, num, array.Length);
		num += array.Length;
		logic_SubGraph_DefeatEnemyTechs_distEnemiesSpotted_3 = distEnemiesSpotted;
		logic_SubGraph_DefeatEnemyTechs_clearSceneryPos_3 = clearSceneryPos;
		logic_SubGraph_DefeatEnemyTechs_clearSceneryRadius_3 = clearSceneryRadius;
		int num2 = 0;
		Array array2 = msgEnemiesSpotted;
		if (logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_3.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_3, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_3, num2, array2.Length);
		num2 += array2.Length;
		logic_SubGraph_DefeatEnemyTechs_messageSpeaker_3 = messageSpeaker;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_3.In(logic_SubGraph_DefeatEnemyTechs_enemyTechData_3, logic_SubGraph_DefeatEnemyTechs_distEnemiesSpotted_3, logic_SubGraph_DefeatEnemyTechs_clearSceneryPos_3, logic_SubGraph_DefeatEnemyTechs_clearSceneryRadius_3, logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_3, logic_SubGraph_DefeatEnemyTechs_msgComplete_3, logic_SubGraph_DefeatEnemyTechs_messageSpeaker_3);
	}

	private void Relay_Succeed_6()
	{
		logic_uScript_FinishEncounter_owner_6 = owner_Connection_7;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_6.Succeed(logic_uScript_FinishEncounter_owner_6);
	}

	private void Relay_Fail_6()
	{
		logic_uScript_FinishEncounter_owner_6 = owner_Connection_7;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_6.Fail(logic_uScript_FinishEncounter_owner_6);
	}

	private void Relay_In_8()
	{
		int num = 0;
		Array array = msgComplete;
		if (logic_uScript_AddOnScreenMessage_locString_8.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_8, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_8, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_8 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_8 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8.In(logic_uScript_AddOnScreenMessage_locString_8, logic_uScript_AddOnScreenMessage_msgPriority_8, logic_uScript_AddOnScreenMessage_holdMsg_8, logic_uScript_AddOnScreenMessage_tag_8, logic_uScript_AddOnScreenMessage_speaker_8, logic_uScript_AddOnScreenMessage_side_8);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8.Out)
		{
			Relay_Succeed_6();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_IsCurrentEncounterInQuestLog_owner_10 = owner_Connection_11;
		logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_10.In(logic_uScript_IsCurrentEncounterInQuestLog_owner_10);
		bool num = logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_10.True;
		bool flag = logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_10.False;
		if (num)
		{
			Relay_In_8();
		}
		if (flag)
		{
			Relay_Fail_6();
		}
	}
}
