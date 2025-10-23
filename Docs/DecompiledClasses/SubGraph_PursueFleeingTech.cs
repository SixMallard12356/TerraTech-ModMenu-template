using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_PursueFleeingTech", "")]
[NodePath("Graphs")]
public class SubGraph_PursueFleeingTech : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_4 = new SpawnTechData[0];

	private bool external_11;

	private float external_19;

	private LocalisedString[] external_39 = new LocalisedString[0];

	private LocalisedString[] external_7 = new LocalisedString[0];

	private LocalisedString[] external_28 = new LocalisedString[0];

	private Tank[] local_68_TankArray = new Tank[0];

	private Tank local_FleeingTech_Tank;

	private bool local_FleeingTechDestroyed_System_Boolean;

	private Vector3 local_InitialPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_IntroMessageShown_System_Boolean;

	private bool local_ObjectiveComplete_System_Boolean;

	private bool local_ObjectiveFailed_System_Boolean;

	private bool local_StopFleeingTech_System_Boolean;

	private bool local_TechsSpawned_System_Boolean;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_71;

	private GameObject owner_Connection_72;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1;

	private bool logic_uScriptAct_SetBool_Out_1 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1 = true;

	private uScript_GraphEvents logic_uScript_GraphEvents_uScript_GraphEvents_2 = new uScript_GraphEvents();

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_6 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_6 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_6 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_6;

	private string logic_uScript_AddOnScreenMessage_tag_6 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_6;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_6;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_6;

	private bool logic_uScript_AddOnScreenMessage_Out_6 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_6 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_8 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_8;

	private object logic_uScript_SetEncounterTarget_visibleObject_8 = "";

	private bool logic_uScript_SetEncounterTarget_Out_8 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_10;

	private bool logic_uScriptCon_CompareBool_True_10 = true;

	private bool logic_uScriptCon_CompareBool_False_10 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_14;

	private bool logic_uScriptCon_CompareBool_True_14 = true;

	private bool logic_uScriptCon_CompareBool_False_14 = true;

	private uScript_InRangeOfTech logic_uScript_InRangeOfTech_uScript_InRangeOfTech_17 = new uScript_InRangeOfTech();

	private Tank logic_uScript_InRangeOfTech_tank_17;

	private float logic_uScript_InRangeOfTech_range_17;

	private bool logic_uScript_InRangeOfTech_Out_17 = true;

	private bool logic_uScript_InRangeOfTech_InRange_17 = true;

	private bool logic_uScript_InRangeOfTech_OutOfRange_17 = true;

	private uScript_AI_SetPOI logic_uScript_AI_SetPOI_uScript_AI_SetPOI_21 = new uScript_AI_SetPOI();

	private Tank logic_uScript_AI_SetPOI_tank_21;

	private bool logic_uScript_AI_SetPOI_usePOI_21 = true;

	private Vector3 logic_uScript_AI_SetPOI_position_21;

	private float logic_uScript_AI_SetPOI_distance_21 = 250f;

	private bool logic_uScript_AI_SetPOI_Out_21 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_27 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_27 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_27 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_27;

	private string logic_uScript_AddOnScreenMessage_tag_27 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_27;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_27;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_27;

	private bool logic_uScript_AddOnScreenMessage_Out_27 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_27 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_30 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_30 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_30 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_30;

	private string logic_uScript_AddOnScreenMessage_tag_30 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_30;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_30;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_30;

	private bool logic_uScript_AddOnScreenMessage_Out_30 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_30 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_31 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_31;

	private bool logic_uScriptAct_SetBool_Out_31 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_31 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_31 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_33 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_33;

	private bool logic_uScriptAct_SetBool_Out_33 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_33 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_33 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_35;

	private bool logic_uScriptCon_CompareBool_True_35 = true;

	private bool logic_uScriptCon_CompareBool_False_35 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_37 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_37;

	private bool logic_uScriptAct_SetBool_Out_37 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_37 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_37 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_40 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_40 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_40 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_40;

	private string logic_uScript_AddOnScreenMessage_tag_40 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_40;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_40;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_40;

	private bool logic_uScript_AddOnScreenMessage_Out_40 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_40 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_42 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_42;

	private bool logic_uScriptAct_SetBool_Out_42 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_42 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_42 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_43;

	private bool logic_uScriptCon_CompareBool_True_43 = true;

	private bool logic_uScriptCon_CompareBool_False_43 = true;

	private uScript_AI_SetBehaviourType logic_uScript_AI_SetBehaviourType_uScript_AI_SetBehaviourType_45 = new uScript_AI_SetBehaviourType();

	private Tank logic_uScript_AI_SetBehaviourType_tank_45;

	private TechAI.AITypes logic_uScript_AI_SetBehaviourType_aiType_45 = TechAI.AITypes.Idle;

	private bool logic_uScript_AI_SetBehaviourType_Out_45 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_48;

	private bool logic_uScriptCon_CompareBool_True_48 = true;

	private bool logic_uScriptCon_CompareBool_False_48 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_50 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_50;

	private bool logic_uScriptAct_SetBool_Out_50 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_50 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_50 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_51 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_51;

	private bool logic_uScriptAct_SetBool_Out_51 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_51 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_51 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_54 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_54;

	private bool logic_uScriptAct_SetBool_Out_54 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_54 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_54 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_55;

	private bool logic_uScriptCon_CompareBool_True_55 = true;

	private bool logic_uScriptCon_CompareBool_False_55 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_58;

	private bool logic_uScriptCon_CompareBool_True_58 = true;

	private bool logic_uScriptCon_CompareBool_False_58 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_62 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_62;

	private bool logic_uScriptAct_SetBool_Out_62 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_62 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_62 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_63 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_63;

	private bool logic_uScriptAct_SetBool_Out_63 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_63 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_63 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_64;

	private bool logic_uScriptCon_CompareBool_True_64 = true;

	private bool logic_uScriptCon_CompareBool_False_64 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_65 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_65;

	private bool logic_uScriptAct_SetBool_Out_65 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_65 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_65 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_66 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_66;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_66 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_66;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_66 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_66 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_66 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_66 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_67 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_67 = new Tank[0];

	private int logic_uScript_AccessListTech_index_67;

	private Tank logic_uScript_AccessListTech_value_67;

	private bool logic_uScript_AccessListTech_Out_67 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_70 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_70 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_70;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_70 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_70 = true;

	[FriendlyName("Complete")]
	public event uScriptEventHandler Complete;

	[FriendlyName("Failed")]
	public event uScriptEventHandler Failed;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_71 || !m_RegisteredForEvents)
		{
			owner_Connection_71 = parentGameObject;
		}
		if (null == owner_Connection_72 || !m_RegisteredForEvents)
		{
			owner_Connection_72 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1.SetParent(g);
		logic_uScript_GraphEvents_uScript_GraphEvents_2.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_6.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_8.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.SetParent(g);
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_17.SetParent(g);
		logic_uScript_AI_SetPOI_uScript_AI_SetPOI_21.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_27.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_30.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_33.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_37.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_40.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_42.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.SetParent(g);
		logic_uScript_AI_SetBehaviourType_uScript_AI_SetBehaviourType_45.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_54.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_62.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_67.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_70.SetParent(g);
		owner_Connection_9 = parentGameObject;
		owner_Connection_71 = parentGameObject;
		owner_Connection_72 = parentGameObject;
	}

	public void Awake()
	{
		logic_uScript_GraphEvents_uScript_GraphEvents_2.uScriptEnable += uScript_GraphEvents_uScriptEnable_2;
		logic_uScript_GraphEvents_uScript_GraphEvents_2.uScriptDisable += uScript_GraphEvents_uScriptDisable_2;
		logic_uScript_GraphEvents_uScript_GraphEvents_2.uScriptDestroy += uScript_GraphEvents_uScriptDestroy_2;
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
		logic_uScript_GraphEvents_uScript_GraphEvents_2.OnEnable();
		logic_uScript_AI_SetBehaviourType_uScript_AI_SetBehaviourType_45.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_GraphEvents_uScript_GraphEvents_2.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_6.OnDisable();
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_17.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_27.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_30.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_40.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
	}

	public void OnDestroy()
	{
		logic_uScript_GraphEvents_uScript_GraphEvents_2.OnDestroy();
		logic_uScript_GraphEvents_uScript_GraphEvents_2.uScriptEnable -= uScript_GraphEvents_uScriptEnable_2;
		logic_uScript_GraphEvents_uScript_GraphEvents_2.uScriptDisable -= uScript_GraphEvents_uScriptDisable_2;
		logic_uScript_GraphEvents_uScript_GraphEvents_2.uScriptDestroy -= uScript_GraphEvents_uScriptDestroy_2;
	}

	private void uScript_GraphEvents_uScriptEnable_2(object o, EventArgs e)
	{
		Relay_uScriptEnable_2();
	}

	private void uScript_GraphEvents_uScriptDisable_2(object o, EventArgs e)
	{
		Relay_uScriptDisable_2();
	}

	private void uScript_GraphEvents_uScriptDestroy_2(object o, EventArgs e)
	{
		Relay_uScriptDestroy_2();
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("fleeingTechData", "")] SpawnTechData[] fleeingTechData, [FriendlyName("keepFleeingTechAlive", "")] bool keepFleeingTechAlive, [FriendlyName("caughtUpDistance", "")] float caughtUpDistance, [FriendlyName("msgIntro", "")] LocalisedString[] msgIntro, [FriendlyName("msgCaughtUp", "")] LocalisedString[] msgCaughtUp, [FriendlyName("msgTechDestroyed", "")] LocalisedString[] msgTechDestroyed)
	{
		external_4 = fleeingTechData;
		external_11 = keepFleeingTechAlive;
		external_19 = caughtUpDistance;
		external_39 = msgIntro;
		external_7 = msgCaughtUp;
		external_28 = msgTechDestroyed;
		Relay_In_55();
	}

	private void Relay_True_1()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1.True(out logic_uScriptAct_SetBool_Target_1);
		local_FleeingTechDestroyed_System_Boolean = logic_uScriptAct_SetBool_Target_1;
	}

	private void Relay_False_1()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1.False(out logic_uScriptAct_SetBool_Target_1);
		local_FleeingTechDestroyed_System_Boolean = logic_uScriptAct_SetBool_Target_1;
	}

	private void Relay_uScriptEnable_2()
	{
	}

	private void Relay_uScriptDisable_2()
	{
		Relay_False_31();
	}

	private void Relay_uScriptDestroy_2()
	{
	}

	private void Relay_Connection_3()
	{
	}

	private void Relay_Connection_4()
	{
	}

	private void Relay_In_6()
	{
		int num = 0;
		Array array = external_7;
		if (logic_uScript_AddOnScreenMessage_locString_6.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_6, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_6, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_6 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_6.In(logic_uScript_AddOnScreenMessage_locString_6, logic_uScript_AddOnScreenMessage_msgPriority_6, logic_uScript_AddOnScreenMessage_holdMsg_6, logic_uScript_AddOnScreenMessage_tag_6, logic_uScript_AddOnScreenMessage_speaker_6, logic_uScript_AddOnScreenMessage_side_6);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_6.Out)
		{
			Relay_True_33();
		}
	}

	private void Relay_Connection_7()
	{
	}

	private void Relay_In_8()
	{
		logic_uScript_SetEncounterTarget_owner_8 = owner_Connection_9;
		logic_uScript_SetEncounterTarget_visibleObject_8 = local_FleeingTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_8.In(logic_uScript_SetEncounterTarget_owner_8, logic_uScript_SetEncounterTarget_visibleObject_8);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_8.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_10()
	{
		logic_uScriptCon_CompareBool_Bool_10 = external_11;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.In(logic_uScriptCon_CompareBool_Bool_10);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.False;
		if (num)
		{
			Relay_In_14();
		}
		if (flag)
		{
			Relay_In_48();
		}
	}

	private void Relay_Connection_11()
	{
	}

	private void Relay_In_14()
	{
		logic_uScriptCon_CompareBool_Bool_14 = local_FleeingTechDestroyed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.In(logic_uScriptCon_CompareBool_Bool_14);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.False;
		if (num)
		{
			Relay_In_27();
		}
		if (flag)
		{
			Relay_In_17();
		}
	}

	private void Relay_Connection_16()
	{
		if (this.Failed != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Failed(this, args);
		}
	}

	private void Relay_In_17()
	{
		logic_uScript_InRangeOfTech_tank_17 = local_FleeingTech_Tank;
		logic_uScript_InRangeOfTech_range_17 = external_19;
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_17.In(logic_uScript_InRangeOfTech_tank_17, logic_uScript_InRangeOfTech_range_17);
		if (logic_uScript_InRangeOfTech_uScript_InRangeOfTech_17.InRange)
		{
			Relay_In_6();
		}
	}

	private void Relay_Connection_19()
	{
	}

	private void Relay_Connection_20()
	{
		if (this.Complete != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Complete(this, args);
		}
	}

	private void Relay_In_21()
	{
		logic_uScript_AI_SetPOI_tank_21 = local_FleeingTech_Tank;
		logic_uScript_AI_SetPOI_position_21 = local_InitialPos_UnityEngine_Vector3;
		logic_uScript_AI_SetPOI_uScript_AI_SetPOI_21.In(logic_uScript_AI_SetPOI_tank_21, logic_uScript_AI_SetPOI_usePOI_21, logic_uScript_AI_SetPOI_position_21, logic_uScript_AI_SetPOI_distance_21);
		if (logic_uScript_AI_SetPOI_uScript_AI_SetPOI_21.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_27()
	{
		int num = 0;
		Array array = external_28;
		if (logic_uScript_AddOnScreenMessage_locString_27.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_27, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_27, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_27 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_27.In(logic_uScript_AddOnScreenMessage_locString_27, logic_uScript_AddOnScreenMessage_msgPriority_27, logic_uScript_AddOnScreenMessage_holdMsg_27, logic_uScript_AddOnScreenMessage_tag_27, logic_uScript_AddOnScreenMessage_speaker_27, logic_uScript_AddOnScreenMessage_side_27);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_27.Out)
		{
			Relay_True_51();
		}
	}

	private void Relay_Connection_28()
	{
	}

	private void Relay_In_30()
	{
		int num = 0;
		Array array = external_28;
		if (logic_uScript_AddOnScreenMessage_locString_30.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_30, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_30, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_30 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_30.In(logic_uScript_AddOnScreenMessage_locString_30, logic_uScript_AddOnScreenMessage_msgPriority_30, logic_uScript_AddOnScreenMessage_holdMsg_30, logic_uScript_AddOnScreenMessage_tag_30, logic_uScript_AddOnScreenMessage_speaker_30, logic_uScript_AddOnScreenMessage_side_30);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_30.Out)
		{
			Relay_True_54();
		}
	}

	private void Relay_True_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.True(out logic_uScriptAct_SetBool_Target_31);
		local_FleeingTechDestroyed_System_Boolean = logic_uScriptAct_SetBool_Target_31;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_31.Out)
		{
			Relay_False_37();
		}
	}

	private void Relay_False_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.False(out logic_uScriptAct_SetBool_Target_31);
		local_FleeingTechDestroyed_System_Boolean = logic_uScriptAct_SetBool_Target_31;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_31.Out)
		{
			Relay_False_37();
		}
	}

	private void Relay_True_33()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_33.True(out logic_uScriptAct_SetBool_Target_33);
		local_StopFleeingTech_System_Boolean = logic_uScriptAct_SetBool_Target_33;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_33.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_False_33()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_33.False(out logic_uScriptAct_SetBool_Target_33);
		local_StopFleeingTech_System_Boolean = logic_uScriptAct_SetBool_Target_33;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_33.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_35()
	{
		logic_uScriptCon_CompareBool_Bool_35 = local_StopFleeingTech_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.In(logic_uScriptCon_CompareBool_Bool_35);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.False)
		{
			Relay_In_21();
		}
	}

	private void Relay_True_37()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_37.True(out logic_uScriptAct_SetBool_Target_37);
		local_StopFleeingTech_System_Boolean = logic_uScriptAct_SetBool_Target_37;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_37.Out)
		{
			Relay_False_50();
		}
	}

	private void Relay_False_37()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_37.False(out logic_uScriptAct_SetBool_Target_37);
		local_StopFleeingTech_System_Boolean = logic_uScriptAct_SetBool_Target_37;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_37.Out)
		{
			Relay_False_50();
		}
	}

	private void Relay_Connection_39()
	{
	}

	private void Relay_In_40()
	{
		int num = 0;
		Array array = external_39;
		if (logic_uScript_AddOnScreenMessage_locString_40.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_40, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_40, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_40 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_40.In(logic_uScript_AddOnScreenMessage_locString_40, logic_uScript_AddOnScreenMessage_msgPriority_40, logic_uScript_AddOnScreenMessage_holdMsg_40, logic_uScript_AddOnScreenMessage_tag_40, logic_uScript_AddOnScreenMessage_speaker_40, logic_uScript_AddOnScreenMessage_side_40);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_40.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_True_42()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_42.True(out logic_uScriptAct_SetBool_Target_42);
		local_IntroMessageShown_System_Boolean = logic_uScriptAct_SetBool_Target_42;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_42.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_False_42()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_42.False(out logic_uScriptAct_SetBool_Target_42);
		local_IntroMessageShown_System_Boolean = logic_uScriptAct_SetBool_Target_42;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_42.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_43()
	{
		logic_uScriptCon_CompareBool_Bool_43 = local_IntroMessageShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.In(logic_uScriptCon_CompareBool_Bool_43);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.False;
		if (num)
		{
			Relay_In_10();
		}
		if (flag)
		{
			Relay_True_42();
		}
	}

	private void Relay_In_45()
	{
		logic_uScript_AI_SetBehaviourType_tank_45 = local_FleeingTech_Tank;
		logic_uScript_AI_SetBehaviourType_uScript_AI_SetBehaviourType_45.In(logic_uScript_AI_SetBehaviourType_tank_45, logic_uScript_AI_SetBehaviourType_aiType_45);
		if (logic_uScript_AI_SetBehaviourType_uScript_AI_SetBehaviourType_45.Out)
		{
			Relay_True_54();
		}
	}

	private void Relay_In_48()
	{
		logic_uScriptCon_CompareBool_Bool_48 = local_FleeingTechDestroyed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.In(logic_uScriptCon_CompareBool_Bool_48);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.True)
		{
			Relay_In_30();
		}
	}

	private void Relay_True_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.True(out logic_uScriptAct_SetBool_Target_50);
		local_IntroMessageShown_System_Boolean = logic_uScriptAct_SetBool_Target_50;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_50.Out)
		{
			Relay_False_62();
		}
	}

	private void Relay_False_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.False(out logic_uScriptAct_SetBool_Target_50);
		local_IntroMessageShown_System_Boolean = logic_uScriptAct_SetBool_Target_50;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_50.Out)
		{
			Relay_False_62();
		}
	}

	private void Relay_True_51()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.True(out logic_uScriptAct_SetBool_Target_51);
		local_ObjectiveFailed_System_Boolean = logic_uScriptAct_SetBool_Target_51;
	}

	private void Relay_False_51()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.False(out logic_uScriptAct_SetBool_Target_51);
		local_ObjectiveFailed_System_Boolean = logic_uScriptAct_SetBool_Target_51;
	}

	private void Relay_True_54()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_54.True(out logic_uScriptAct_SetBool_Target_54);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_54;
	}

	private void Relay_False_54()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_54.False(out logic_uScriptAct_SetBool_Target_54);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_54;
	}

	private void Relay_In_55()
	{
		logic_uScriptCon_CompareBool_Bool_55 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.In(logic_uScriptCon_CompareBool_Bool_55);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.False;
		if (num)
		{
			Relay_Connection_20();
		}
		if (flag)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_58()
	{
		logic_uScriptCon_CompareBool_Bool_58 = local_ObjectiveFailed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.In(logic_uScriptCon_CompareBool_Bool_58);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.False;
		if (num)
		{
			Relay_Connection_16();
		}
		if (flag)
		{
			Relay_In_64();
		}
	}

	private void Relay_True_62()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_62.True(out logic_uScriptAct_SetBool_Target_62);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_62;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_62.Out)
		{
			Relay_False_63();
		}
	}

	private void Relay_False_62()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_62.False(out logic_uScriptAct_SetBool_Target_62);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_62;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_62.Out)
		{
			Relay_False_63();
		}
	}

	private void Relay_True_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.True(out logic_uScriptAct_SetBool_Target_63);
		local_ObjectiveFailed_System_Boolean = logic_uScriptAct_SetBool_Target_63;
	}

	private void Relay_False_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.False(out logic_uScriptAct_SetBool_Target_63);
		local_ObjectiveFailed_System_Boolean = logic_uScriptAct_SetBool_Target_63;
	}

	private void Relay_In_64()
	{
		logic_uScriptCon_CompareBool_Bool_64 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.In(logic_uScriptCon_CompareBool_Bool_64);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.False;
		if (num)
		{
			Relay_In_66();
		}
		if (flag)
		{
			Relay_True_65();
		}
	}

	private void Relay_True_65()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.True(out logic_uScriptAct_SetBool_Target_65);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_65;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_65.Out)
		{
			Relay_InitialSpawn_70();
		}
	}

	private void Relay_False_65()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.False(out logic_uScriptAct_SetBool_Target_65);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_65;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_65.Out)
		{
			Relay_InitialSpawn_70();
		}
	}

	private void Relay_In_66()
	{
		int num = 0;
		Array array = external_4;
		if (logic_uScript_GetAndCheckTechs_techData_66.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_66, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_66, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_66 = owner_Connection_72;
		int num2 = 0;
		Array array2 = local_68_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_66.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_66, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_66, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_66 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.In(logic_uScript_GetAndCheckTechs_techData_66, logic_uScript_GetAndCheckTechs_ownerNode_66, ref logic_uScript_GetAndCheckTechs_techs_66);
		local_68_TankArray = logic_uScript_GetAndCheckTechs_techs_66;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_67();
		}
		if (someAlive)
		{
			Relay_AtIndex_67();
		}
		if (allDead)
		{
			Relay_True_1();
		}
	}

	private void Relay_AtIndex_67()
	{
		int num = 0;
		Array array = local_68_TankArray;
		if (logic_uScript_AccessListTech_techList_67.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_67, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_67, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_67.AtIndex(ref logic_uScript_AccessListTech_techList_67, logic_uScript_AccessListTech_index_67, out logic_uScript_AccessListTech_value_67);
		local_68_TankArray = logic_uScript_AccessListTech_techList_67;
		local_FleeingTech_Tank = logic_uScript_AccessListTech_value_67;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_67.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_InitialSpawn_70()
	{
		int num = 0;
		Array array = external_4;
		if (logic_uScript_SpawnTechsFromData_spawnData_70.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_70, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_70, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_70 = owner_Connection_71;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_70.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_70, logic_uScript_SpawnTechsFromData_ownerNode_70, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_70);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_70.Out)
		{
			Relay_In_66();
		}
	}
}
