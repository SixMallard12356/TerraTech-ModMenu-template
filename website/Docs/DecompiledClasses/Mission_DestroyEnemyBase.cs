using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_DestroyEnemyBase : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public SpawnTechData baseData;

	public SpawnTechData[] guardianData = new SpawnTechData[0];

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgBaseDestroyed = new LocalisedString[0];

	public LocalisedString[] msgBaseSpotted = new LocalisedString[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_10;

	private SubGraph_CaptureEnemyBase logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5 = new SubGraph_CaptureEnemyBase();

	private string logic_SubGraph_CaptureEnemyBase_clearSceneryPos_5 = "";

	private float logic_SubGraph_CaptureEnemyBase_clearSceneryRadius_5 = 50f;

	private SpawnTechData[] logic_SubGraph_CaptureEnemyBase_baseData_5 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_CaptureEnemyBase_guardianData_5 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_CaptureEnemyBase_harvesterData_5 = new SpawnTechData[0];

	private LocalisedString[] logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_5 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_CaptureEnemyBase_msgBaseCaptured_5 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_CaptureEnemyBase_msgBaseDestroyed_5 = new LocalisedString[0];

	private ManOnScreenMessages.Speaker logic_SubGraph_CaptureEnemyBase_messageSpeaker_5;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_7 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_7 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_9 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_9;

	private bool logic_uScript_FinishEncounter_Out_9 = true;

	private uScript_IsCurrentEncounterInQuestLog logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_11 = new uScript_IsCurrentEncounterInQuestLog();

	private GameObject logic_uScript_IsCurrentEncounterInQuestLog_owner_11;

	private bool logic_uScript_IsCurrentEncounterInQuestLog_Out_11 = true;

	private bool logic_uScript_IsCurrentEncounterInQuestLog_True_11 = true;

	private bool logic_uScript_IsCurrentEncounterInQuestLog_False_11 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_12 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_12 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_12 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_12;

	private string logic_uScript_AddOnScreenMessage_tag_12 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_12;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_12;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_12;

	private bool logic_uScript_AddOnScreenMessage_Out_12 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_12 = true;

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
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
		}
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
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
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_7.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_9.SetParent(g);
		logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_11.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_12.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_6 = parentGameObject;
		owner_Connection_10 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.Awake();
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.Base_Captured += SubGraph_CaptureEnemyBase_Base_Captured_5;
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.Base_Destroyed += SubGraph_CaptureEnemyBase_Base_Destroyed_5;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_12.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.OnDestroy();
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.Base_Captured -= SubGraph_CaptureEnemyBase_Base_Captured_5;
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.Base_Destroyed -= SubGraph_CaptureEnemyBase_Base_Destroyed_5;
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

	private void SubGraph_CaptureEnemyBase_Base_Captured_5(object o, SubGraph_CaptureEnemyBase.LogicEventArgs e)
	{
		Relay_Base_Captured_5();
	}

	private void SubGraph_CaptureEnemyBase_Base_Destroyed_5(object o, SubGraph_CaptureEnemyBase.LogicEventArgs e)
	{
		Relay_Base_Destroyed_5();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_Destroy_Base_5();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Base_Captured_5()
	{
	}

	private void Relay_Base_Destroyed_5()
	{
		Relay_In_11();
	}

	private void Relay_Capture_Base_5()
	{
		int num = 0;
		if (logic_SubGraph_CaptureEnemyBase_baseData_5.Length <= num)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_baseData_5, num + 1);
		}
		logic_SubGraph_CaptureEnemyBase_baseData_5[num++] = baseData;
		int num2 = 0;
		Array array = guardianData;
		if (logic_SubGraph_CaptureEnemyBase_guardianData_5.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_guardianData_5, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_CaptureEnemyBase_guardianData_5, num2, array.Length);
		num2 += array.Length;
		int num3 = 0;
		Array array2 = msgBaseSpotted;
		if (logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_5.Length != num3 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_5, num3 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_5, num3, array2.Length);
		num3 += array2.Length;
		logic_SubGraph_CaptureEnemyBase_messageSpeaker_5 = messageSpeaker;
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.Capture_Base(logic_SubGraph_CaptureEnemyBase_clearSceneryPos_5, logic_SubGraph_CaptureEnemyBase_clearSceneryRadius_5, logic_SubGraph_CaptureEnemyBase_baseData_5, logic_SubGraph_CaptureEnemyBase_guardianData_5, logic_SubGraph_CaptureEnemyBase_harvesterData_5, logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_5, logic_SubGraph_CaptureEnemyBase_msgBaseCaptured_5, logic_SubGraph_CaptureEnemyBase_msgBaseDestroyed_5, logic_SubGraph_CaptureEnemyBase_messageSpeaker_5);
	}

	private void Relay_Destroy_Base_5()
	{
		int num = 0;
		if (logic_SubGraph_CaptureEnemyBase_baseData_5.Length <= num)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_baseData_5, num + 1);
		}
		logic_SubGraph_CaptureEnemyBase_baseData_5[num++] = baseData;
		int num2 = 0;
		Array array = guardianData;
		if (logic_SubGraph_CaptureEnemyBase_guardianData_5.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_guardianData_5, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_CaptureEnemyBase_guardianData_5, num2, array.Length);
		num2 += array.Length;
		int num3 = 0;
		Array array2 = msgBaseSpotted;
		if (logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_5.Length != num3 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_5, num3 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_5, num3, array2.Length);
		num3 += array2.Length;
		logic_SubGraph_CaptureEnemyBase_messageSpeaker_5 = messageSpeaker;
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_5.Destroy_Base(logic_SubGraph_CaptureEnemyBase_clearSceneryPos_5, logic_SubGraph_CaptureEnemyBase_clearSceneryRadius_5, logic_SubGraph_CaptureEnemyBase_baseData_5, logic_SubGraph_CaptureEnemyBase_guardianData_5, logic_SubGraph_CaptureEnemyBase_harvesterData_5, logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_5, logic_SubGraph_CaptureEnemyBase_msgBaseCaptured_5, logic_SubGraph_CaptureEnemyBase_msgBaseDestroyed_5, logic_SubGraph_CaptureEnemyBase_messageSpeaker_5);
	}

	private void Relay_In_7()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_7.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_7.Out)
		{
			Relay_Fail_9();
		}
	}

	private void Relay_Succeed_9()
	{
		logic_uScript_FinishEncounter_owner_9 = owner_Connection_10;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_9.Succeed(logic_uScript_FinishEncounter_owner_9);
	}

	private void Relay_Fail_9()
	{
		logic_uScript_FinishEncounter_owner_9 = owner_Connection_10;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_9.Fail(logic_uScript_FinishEncounter_owner_9);
	}

	private void Relay_In_11()
	{
		logic_uScript_IsCurrentEncounterInQuestLog_owner_11 = owner_Connection_6;
		logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_11.In(logic_uScript_IsCurrentEncounterInQuestLog_owner_11);
		bool num = logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_11.True;
		bool flag = logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_11.False;
		if (num)
		{
			Relay_In_12();
		}
		if (flag)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_12()
	{
		int num = 0;
		Array array = msgBaseDestroyed;
		if (logic_uScript_AddOnScreenMessage_locString_12.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_12, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_12, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_12 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_12 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_12.In(logic_uScript_AddOnScreenMessage_locString_12, logic_uScript_AddOnScreenMessage_msgPriority_12, logic_uScript_AddOnScreenMessage_holdMsg_12, logic_uScript_AddOnScreenMessage_tag_12, logic_uScript_AddOnScreenMessage_speaker_12, logic_uScript_AddOnScreenMessage_side_12);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_12.Out)
		{
			Relay_Succeed_9();
		}
	}
}
