using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_CaptureEnemyBase : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public SpawnTechData baseData;

	[Multiline(3)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius = 50f;

	public SpawnTechData[] guardianData = new SpawnTechData[0];

	public SpawnTechData[] harvesterData = new SpawnTechData[0];

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgBaseCaptured = new LocalisedString[0];

	public LocalisedString[] msgBaseDestroyed = new LocalisedString[0];

	public LocalisedString[] msgBaseSpotted = new LocalisedString[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_11;

	private SubGraph_CaptureEnemyBase logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6 = new SubGraph_CaptureEnemyBase();

	private string logic_SubGraph_CaptureEnemyBase_clearSceneryPos_6 = "";

	private float logic_SubGraph_CaptureEnemyBase_clearSceneryRadius_6;

	private SpawnTechData[] logic_SubGraph_CaptureEnemyBase_baseData_6 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_CaptureEnemyBase_guardianData_6 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_CaptureEnemyBase_harvesterData_6 = new SpawnTechData[0];

	private LocalisedString[] logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_6 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_CaptureEnemyBase_msgBaseCaptured_6 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_CaptureEnemyBase_msgBaseDestroyed_6 = new LocalisedString[0];

	private ManOnScreenMessages.Speaker logic_SubGraph_CaptureEnemyBase_messageSpeaker_6;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_7 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_7;

	private bool logic_uScript_FinishEncounter_Out_7 = true;

	private uScript_IsCurrentEncounterInQuestLog logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_10 = new uScript_IsCurrentEncounterInQuestLog();

	private GameObject logic_uScript_IsCurrentEncounterInQuestLog_owner_10;

	private bool logic_uScript_IsCurrentEncounterInQuestLog_Out_10 = true;

	private bool logic_uScript_IsCurrentEncounterInQuestLog_True_10 = true;

	private bool logic_uScript_IsCurrentEncounterInQuestLog_False_10 = true;

	private uScript_IsCurrentEncounterInQuestLog logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_12 = new uScript_IsCurrentEncounterInQuestLog();

	private GameObject logic_uScript_IsCurrentEncounterInQuestLog_owner_12;

	private bool logic_uScript_IsCurrentEncounterInQuestLog_Out_12 = true;

	private bool logic_uScript_IsCurrentEncounterInQuestLog_True_12 = true;

	private bool logic_uScript_IsCurrentEncounterInQuestLog_False_12 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_13 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_13 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_13;

	private string logic_uScript_AddOnScreenMessage_tag_13 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_13;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_13;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_13;

	private bool logic_uScript_AddOnScreenMessage_Out_13 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_13 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_16 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_16 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_16;

	private string logic_uScript_AddOnScreenMessage_tag_16 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_16;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_16;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_16;

	private bool logic_uScript_AddOnScreenMessage_Out_16 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_16 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_17 = true;

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
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
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
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_7.SetParent(g);
		logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_10.SetParent(g);
		logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_12.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_11 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.Awake();
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.Base_Captured += SubGraph_CaptureEnemyBase_Base_Captured_6;
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.Base_Destroyed += SubGraph_CaptureEnemyBase_Base_Destroyed_6;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.OnDestroy();
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.Base_Captured -= SubGraph_CaptureEnemyBase_Base_Captured_6;
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.Base_Destroyed -= SubGraph_CaptureEnemyBase_Base_Destroyed_6;
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

	private void SubGraph_CaptureEnemyBase_Base_Captured_6(object o, SubGraph_CaptureEnemyBase.LogicEventArgs e)
	{
		Relay_Base_Captured_6();
	}

	private void SubGraph_CaptureEnemyBase_Base_Destroyed_6(object o, SubGraph_CaptureEnemyBase.LogicEventArgs e)
	{
		Relay_Base_Destroyed_6();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_Capture_Base_6();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Base_Captured_6()
	{
		Relay_In_10();
	}

	private void Relay_Base_Destroyed_6()
	{
		Relay_In_12();
	}

	private void Relay_Capture_Base_6()
	{
		logic_SubGraph_CaptureEnemyBase_clearSceneryPos_6 = clearSceneryPos;
		logic_SubGraph_CaptureEnemyBase_clearSceneryRadius_6 = clearSceneryRadius;
		int num = 0;
		if (logic_SubGraph_CaptureEnemyBase_baseData_6.Length <= num)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_baseData_6, num + 1);
		}
		logic_SubGraph_CaptureEnemyBase_baseData_6[num++] = baseData;
		int num2 = 0;
		Array array = guardianData;
		if (logic_SubGraph_CaptureEnemyBase_guardianData_6.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_guardianData_6, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_CaptureEnemyBase_guardianData_6, num2, array.Length);
		num2 += array.Length;
		int num3 = 0;
		Array array2 = harvesterData;
		if (logic_SubGraph_CaptureEnemyBase_harvesterData_6.Length != num3 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_harvesterData_6, num3 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_CaptureEnemyBase_harvesterData_6, num3, array2.Length);
		num3 += array2.Length;
		int num4 = 0;
		Array array3 = msgBaseSpotted;
		if (logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_6.Length != num4 + array3.Length)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_6, num4 + array3.Length);
		}
		Array.Copy(array3, 0, logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_6, num4, array3.Length);
		num4 += array3.Length;
		logic_SubGraph_CaptureEnemyBase_messageSpeaker_6 = messageSpeaker;
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.Capture_Base(logic_SubGraph_CaptureEnemyBase_clearSceneryPos_6, logic_SubGraph_CaptureEnemyBase_clearSceneryRadius_6, logic_SubGraph_CaptureEnemyBase_baseData_6, logic_SubGraph_CaptureEnemyBase_guardianData_6, logic_SubGraph_CaptureEnemyBase_harvesterData_6, logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_6, logic_SubGraph_CaptureEnemyBase_msgBaseCaptured_6, logic_SubGraph_CaptureEnemyBase_msgBaseDestroyed_6, logic_SubGraph_CaptureEnemyBase_messageSpeaker_6);
	}

	private void Relay_Destroy_Base_6()
	{
		logic_SubGraph_CaptureEnemyBase_clearSceneryPos_6 = clearSceneryPos;
		logic_SubGraph_CaptureEnemyBase_clearSceneryRadius_6 = clearSceneryRadius;
		int num = 0;
		if (logic_SubGraph_CaptureEnemyBase_baseData_6.Length <= num)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_baseData_6, num + 1);
		}
		logic_SubGraph_CaptureEnemyBase_baseData_6[num++] = baseData;
		int num2 = 0;
		Array array = guardianData;
		if (logic_SubGraph_CaptureEnemyBase_guardianData_6.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_guardianData_6, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_CaptureEnemyBase_guardianData_6, num2, array.Length);
		num2 += array.Length;
		int num3 = 0;
		Array array2 = harvesterData;
		if (logic_SubGraph_CaptureEnemyBase_harvesterData_6.Length != num3 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_harvesterData_6, num3 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_CaptureEnemyBase_harvesterData_6, num3, array2.Length);
		num3 += array2.Length;
		int num4 = 0;
		Array array3 = msgBaseSpotted;
		if (logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_6.Length != num4 + array3.Length)
		{
			Array.Resize(ref logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_6, num4 + array3.Length);
		}
		Array.Copy(array3, 0, logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_6, num4, array3.Length);
		num4 += array3.Length;
		logic_SubGraph_CaptureEnemyBase_messageSpeaker_6 = messageSpeaker;
		logic_SubGraph_CaptureEnemyBase_SubGraph_CaptureEnemyBase_6.Destroy_Base(logic_SubGraph_CaptureEnemyBase_clearSceneryPos_6, logic_SubGraph_CaptureEnemyBase_clearSceneryRadius_6, logic_SubGraph_CaptureEnemyBase_baseData_6, logic_SubGraph_CaptureEnemyBase_guardianData_6, logic_SubGraph_CaptureEnemyBase_harvesterData_6, logic_SubGraph_CaptureEnemyBase_msgBaseSpotted_6, logic_SubGraph_CaptureEnemyBase_msgBaseCaptured_6, logic_SubGraph_CaptureEnemyBase_msgBaseDestroyed_6, logic_SubGraph_CaptureEnemyBase_messageSpeaker_6);
	}

	private void Relay_Succeed_7()
	{
		logic_uScript_FinishEncounter_owner_7 = owner_Connection_8;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_7.Succeed(logic_uScript_FinishEncounter_owner_7);
	}

	private void Relay_Fail_7()
	{
		logic_uScript_FinishEncounter_owner_7 = owner_Connection_8;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_7.Fail(logic_uScript_FinishEncounter_owner_7);
	}

	private void Relay_In_10()
	{
		logic_uScript_IsCurrentEncounterInQuestLog_owner_10 = owner_Connection_9;
		logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_10.In(logic_uScript_IsCurrentEncounterInQuestLog_owner_10);
		bool num = logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_10.True;
		bool flag = logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_10.False;
		if (num)
		{
			Relay_In_16();
		}
		if (flag)
		{
			Relay_Fail_7();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_IsCurrentEncounterInQuestLog_owner_12 = owner_Connection_11;
		logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_12.In(logic_uScript_IsCurrentEncounterInQuestLog_owner_12);
		bool num = logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_12.True;
		bool flag = logic_uScript_IsCurrentEncounterInQuestLog_uScript_IsCurrentEncounterInQuestLog_12.False;
		if (num)
		{
			Relay_In_13();
		}
		if (flag)
		{
			Relay_In_17();
		}
	}

	private void Relay_In_13()
	{
		int num = 0;
		Array array = msgBaseDestroyed;
		if (logic_uScript_AddOnScreenMessage_locString_13.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_13, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_13, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_13 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_13 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13.In(logic_uScript_AddOnScreenMessage_locString_13, logic_uScript_AddOnScreenMessage_msgPriority_13, logic_uScript_AddOnScreenMessage_holdMsg_13, logic_uScript_AddOnScreenMessage_tag_13, logic_uScript_AddOnScreenMessage_speaker_13, logic_uScript_AddOnScreenMessage_side_13);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13.Out)
		{
			Relay_Fail_7();
		}
	}

	private void Relay_In_16()
	{
		int num = 0;
		Array array = msgBaseCaptured;
		if (logic_uScript_AddOnScreenMessage_locString_16.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_16, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_16, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_16 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_16 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16.In(logic_uScript_AddOnScreenMessage_locString_16, logic_uScript_AddOnScreenMessage_msgPriority_16, logic_uScript_AddOnScreenMessage_holdMsg_16, logic_uScript_AddOnScreenMessage_tag_16, logic_uScript_AddOnScreenMessage_speaker_16, logic_uScript_AddOnScreenMessage_side_16);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16.Out)
		{
			Relay_Succeed_7();
		}
	}

	private void Relay_In_17()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.Out)
		{
			Relay_Fail_7();
		}
	}
}
