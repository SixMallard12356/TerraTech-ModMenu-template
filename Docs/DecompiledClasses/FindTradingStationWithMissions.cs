using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Find Trading Station With Missions", "")]
public class FindTradingStationWithMissions : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private ManHUD.HUDElementType local_53_ManHUD_HUDElementType = ManHUD.HUDElementType.MissionBoard;

	private string local_msg_System_String = "msg";

	private bool local_ShownMsgFindTradingStation_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private Vector3 local_TradingStationPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private Tank local_TradingStationTech_Tank;

	public LocalisedString[] msgAcceptAMission = new LocalisedString[0];

	public LocalisedString[] msgFindTradingStation = new LocalisedString[0];

	public LocalisedString[] msgOpenTheMissionBoard = new LocalisedString[0];

	public LocalisedString[] msgTradingStationFound = new LocalisedString[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_19;

	private uScript_GetNearestVendorPos logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_6 = new uScript_GetNearestVendorPos();

	private Vector3 logic_uScript_GetNearestVendorPos_Return_6;

	private bool logic_uScript_GetNearestVendorPos_Out_6 = true;

	private bool logic_uScript_GetNearestVendorPos_Found_6 = true;

	private bool logic_uScript_GetNearestVendorPos_Missing_6 = true;

	private uScript_SetEncounterPosition logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_8 = new uScript_SetEncounterPosition();

	private GameObject logic_uScript_SetEncounterPosition_ownerNode_8;

	private Vector3 logic_uScript_SetEncounterPosition_position_8;

	private bool logic_uScript_SetEncounterPosition_Out_8 = true;

	private uScript_FindNearestVendorToEncounter logic_uScript_FindNearestVendorToEncounter_uScript_FindNearestVendorToEncounter_11 = new uScript_FindNearestVendorToEncounter();

	private GameObject logic_uScript_FindNearestVendorToEncounter_ownerNode_11;

	private Tank logic_uScript_FindNearestVendorToEncounter_Return_11;

	private bool logic_uScript_FindNearestVendorToEncounter_Out_11 = true;

	private bool logic_uScript_FindNearestVendorToEncounter_Returned_11 = true;

	private bool logic_uScript_FindNearestVendorToEncounter_NotReturned_11 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_13 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_13;

	private bool logic_uScript_FinishEncounter_Out_13 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_14 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_14;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_17 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_17;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_17 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_17 = "Stage";

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_20 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_20;

	private object logic_uScript_SetEncounterTarget_visibleObject_20 = "";

	private bool logic_uScript_SetEncounterTarget_Out_20 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_22 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_22;

	private float logic_uScript_IsPlayerInRangeOfTech_range_22 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_22 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_22 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_22 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_22 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_24 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_24 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_24;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_24;

	private string logic_uScript_AddOnScreenMessage_tag_24 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_24;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_24;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_24;

	private bool logic_uScript_AddOnScreenMessage_Out_24 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_24 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_26 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_26 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_26;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_26;

	private string logic_uScript_AddOnScreenMessage_tag_26 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_26;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_26;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_26;

	private bool logic_uScript_AddOnScreenMessage_Out_26 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_26 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_28;

	private uScript_IsHUDElementVisible logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_31 = new uScript_IsHUDElementVisible();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementVisible_hudElement_31;

	private bool logic_uScript_IsHUDElementVisible_True_31 = true;

	private bool logic_uScript_IsHUDElementVisible_False_31 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_32 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_32 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_32;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_32 = true;

	private string logic_uScript_AddOnScreenMessage_tag_32 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_32;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_32;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_32;

	private bool logic_uScript_AddOnScreenMessage_Out_32 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_32 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_37 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_37 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_37 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_37 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_39 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_39;

	private bool logic_uScriptAct_SetBool_Out_39 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_39 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_39 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_42 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_42;

	private float logic_uScript_IsPlayerInRangeOfTech_range_42 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_42 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_42 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_42 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_42 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_43 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_43 = 1;

	private int logic_uScriptAct_SetInt_Target_43;

	private bool logic_uScriptAct_SetInt_Out_43 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_45;

	private bool logic_uScriptCon_CompareBool_True_45 = true;

	private bool logic_uScriptCon_CompareBool_False_45 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_47 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_47 = 300f;

	private bool logic_uScript_Wait_repeat_47 = true;

	private bool logic_uScript_Wait_Waited_47 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_49;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_49 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_49 = "ShownMsgFindTradingStation";

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_50;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_50;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_52;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_52;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_54 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_54 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_54;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_54;

	private string logic_uScript_AddOnScreenMessage_tag_54 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_54;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_54;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_54;

	private bool logic_uScript_AddOnScreenMessage_Out_54 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_54 = true;

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
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
		}
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
			if (null != owner_Connection_3)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_3.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_3.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_4;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_4;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_4;
				}
			}
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_3)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_3.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_3.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_4;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_4;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_4;
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
		if (null != owner_Connection_3)
		{
			uScript_SaveLoad component2 = owner_Connection_3.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_4;
				component2.LoadEvent -= Instance_LoadEvent_4;
				component2.RestartEvent -= Instance_RestartEvent_4;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_6.SetParent(g);
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_8.SetParent(g);
		logic_uScript_FindNearestVendorToEncounter_uScript_FindNearestVendorToEncounter_11.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_13.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_14.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_20.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_22.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_24.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_26.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.SetParent(g);
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_31.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_32.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_37.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_42.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_43.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.SetParent(g);
		logic_uScript_Wait_uScript_Wait_47.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_54.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_2 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_19 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_14.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_14.Out += SubGraph_LoadObjectiveStates_Out_14;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.Save_Out += SubGraph_SaveLoadInt_Save_Out_17;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.Load_Out += SubGraph_SaveLoadInt_Load_Out_17;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_17;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output1 += uScriptCon_ManualSwitch_Output1_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output2 += uScriptCon_ManualSwitch_Output2_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output3 += uScriptCon_ManualSwitch_Output3_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output4 += uScriptCon_ManualSwitch_Output4_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output5 += uScriptCon_ManualSwitch_Output5_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output6 += uScriptCon_ManualSwitch_Output6_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output7 += uScriptCon_ManualSwitch_Output7_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output8 += uScriptCon_ManualSwitch_Output8_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save_Out += SubGraph_SaveLoadBool_Save_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load_Out += SubGraph_SaveLoadBool_Load_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_49;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.Out += SubGraph_CompleteObjectiveStage_Out_50;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.Out += SubGraph_CompleteObjectiveStage_Out_52;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_14.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_6.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_14.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_14.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_22.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_24.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_26.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_32.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_42.OnDisable();
		logic_uScript_Wait_uScript_Wait_47.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_54.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_14.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_14.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_14.Out -= SubGraph_LoadObjectiveStates_Out_14;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.Save_Out -= SubGraph_SaveLoadInt_Save_Out_17;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.Load_Out -= SubGraph_SaveLoadInt_Load_Out_17;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_17;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output1 -= uScriptCon_ManualSwitch_Output1_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output2 -= uScriptCon_ManualSwitch_Output2_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output3 -= uScriptCon_ManualSwitch_Output3_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output4 -= uScriptCon_ManualSwitch_Output4_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output5 -= uScriptCon_ManualSwitch_Output5_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output6 -= uScriptCon_ManualSwitch_Output6_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output7 -= uScriptCon_ManualSwitch_Output7_28;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.Output8 -= uScriptCon_ManualSwitch_Output8_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save_Out -= SubGraph_SaveLoadBool_Save_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load_Out -= SubGraph_SaveLoadBool_Load_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_49;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.Out -= SubGraph_CompleteObjectiveStage_Out_50;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.Out -= SubGraph_CompleteObjectiveStage_Out_52;
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

	private void Instance_SaveEvent_4(object o, EventArgs e)
	{
		Relay_SaveEvent_4();
	}

	private void Instance_LoadEvent_4(object o, EventArgs e)
	{
		Relay_LoadEvent_4();
	}

	private void Instance_RestartEvent_4(object o, EventArgs e)
	{
		Relay_RestartEvent_4();
	}

	private void SubGraph_LoadObjectiveStates_Out_14(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_14();
	}

	private void SubGraph_SaveLoadInt_Save_Out_17(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_17 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_17;
		Relay_Save_Out_17();
	}

	private void SubGraph_SaveLoadInt_Load_Out_17(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_17 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_17;
		Relay_Load_Out_17();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_17(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_17 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_17;
		Relay_Restart_Out_17();
	}

	private void uScriptCon_ManualSwitch_Output1_28(object o, EventArgs e)
	{
		Relay_Output1_28();
	}

	private void uScriptCon_ManualSwitch_Output2_28(object o, EventArgs e)
	{
		Relay_Output2_28();
	}

	private void uScriptCon_ManualSwitch_Output3_28(object o, EventArgs e)
	{
		Relay_Output3_28();
	}

	private void uScriptCon_ManualSwitch_Output4_28(object o, EventArgs e)
	{
		Relay_Output4_28();
	}

	private void uScriptCon_ManualSwitch_Output5_28(object o, EventArgs e)
	{
		Relay_Output5_28();
	}

	private void uScriptCon_ManualSwitch_Output6_28(object o, EventArgs e)
	{
		Relay_Output6_28();
	}

	private void uScriptCon_ManualSwitch_Output7_28(object o, EventArgs e)
	{
		Relay_Output7_28();
	}

	private void uScriptCon_ManualSwitch_Output8_28(object o, EventArgs e)
	{
		Relay_Output8_28();
	}

	private void SubGraph_SaveLoadBool_Save_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_ShownMsgFindTradingStation_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Save_Out_49();
	}

	private void SubGraph_SaveLoadBool_Load_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_ShownMsgFindTradingStation_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Load_Out_49();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_ShownMsgFindTradingStation_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Restart_Out_49();
	}

	private void SubGraph_CompleteObjectiveStage_Out_50(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_50 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_50;
		Relay_Out_50();
	}

	private void SubGraph_CompleteObjectiveStage_Out_52(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_52 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_52;
		Relay_Out_52();
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

	private void Relay_SaveEvent_4()
	{
		Relay_Save_49();
	}

	private void Relay_LoadEvent_4()
	{
		Relay_Load_49();
	}

	private void Relay_RestartEvent_4()
	{
		Relay_Set_False_49();
	}

	private void Relay_In_6()
	{
		logic_uScript_GetNearestVendorPos_Return_6 = logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_6.In();
		local_TradingStationPos_UnityEngine_Vector3 = logic_uScript_GetNearestVendorPos_Return_6;
		if (logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_6.Found)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_8()
	{
		logic_uScript_SetEncounterPosition_ownerNode_8 = owner_Connection_9;
		logic_uScript_SetEncounterPosition_position_8 = local_TradingStationPos_UnityEngine_Vector3;
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_8.In(logic_uScript_SetEncounterPosition_ownerNode_8, logic_uScript_SetEncounterPosition_position_8);
		if (logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_8.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_FindNearestVendorToEncounter_ownerNode_11 = owner_Connection_12;
		logic_uScript_FindNearestVendorToEncounter_Return_11 = logic_uScript_FindNearestVendorToEncounter_uScript_FindNearestVendorToEncounter_11.In(logic_uScript_FindNearestVendorToEncounter_ownerNode_11);
		local_TradingStationTech_Tank = logic_uScript_FindNearestVendorToEncounter_Return_11;
		if (logic_uScript_FindNearestVendorToEncounter_uScript_FindNearestVendorToEncounter_11.Returned)
		{
			Relay_In_20();
		}
	}

	private void Relay_Succeed_13()
	{
		logic_uScript_FinishEncounter_owner_13 = owner_Connection_2;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_13.Succeed(logic_uScript_FinishEncounter_owner_13);
	}

	private void Relay_Fail_13()
	{
		logic_uScript_FinishEncounter_owner_13 = owner_Connection_2;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_13.Fail(logic_uScript_FinishEncounter_owner_13);
	}

	private void Relay_Out_14()
	{
	}

	private void Relay_In_14()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_14 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_14.In(logic_SubGraph_LoadObjectiveStates_currentObjective_14);
	}

	private void Relay_Save_Out_17()
	{
	}

	private void Relay_Load_Out_17()
	{
		Relay_In_14();
	}

	private void Relay_Restart_Out_17()
	{
	}

	private void Relay_Save_17()
	{
		logic_SubGraph_SaveLoadInt_integer_17 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_17 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.Save(logic_SubGraph_SaveLoadInt_restartValue_17, ref logic_SubGraph_SaveLoadInt_integer_17, logic_SubGraph_SaveLoadInt_intAsVariable_17, logic_SubGraph_SaveLoadInt_uniqueID_17);
	}

	private void Relay_Load_17()
	{
		logic_SubGraph_SaveLoadInt_integer_17 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_17 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.Load(logic_SubGraph_SaveLoadInt_restartValue_17, ref logic_SubGraph_SaveLoadInt_integer_17, logic_SubGraph_SaveLoadInt_intAsVariable_17, logic_SubGraph_SaveLoadInt_uniqueID_17);
	}

	private void Relay_Restart_17()
	{
		logic_SubGraph_SaveLoadInt_integer_17 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_17 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_17.Restart(logic_SubGraph_SaveLoadInt_restartValue_17, ref logic_SubGraph_SaveLoadInt_integer_17, logic_SubGraph_SaveLoadInt_intAsVariable_17, logic_SubGraph_SaveLoadInt_uniqueID_17);
	}

	private void Relay_In_20()
	{
		logic_uScript_SetEncounterTarget_owner_20 = owner_Connection_19;
		logic_uScript_SetEncounterTarget_visibleObject_20 = local_TradingStationTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_20.In(logic_uScript_SetEncounterTarget_owner_20, logic_uScript_SetEncounterTarget_visibleObject_20);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_20.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_22()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_22 = local_TradingStationTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_22.In(logic_uScript_IsPlayerInRangeOfTech_tech_22, logic_uScript_IsPlayerInRangeOfTech_range_22, logic_uScript_IsPlayerInRangeOfTech_techs_22);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_22.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_22.OutOfRange;
		if (inRange)
		{
			Relay_In_26();
		}
		if (outOfRange)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_24()
	{
		int num = 0;
		Array array = msgFindTradingStation;
		if (logic_uScript_AddOnScreenMessage_locString_24.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_24, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_24, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_24 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_24 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_24.In(logic_uScript_AddOnScreenMessage_locString_24, logic_uScript_AddOnScreenMessage_msgPriority_24, logic_uScript_AddOnScreenMessage_holdMsg_24, logic_uScript_AddOnScreenMessage_tag_24, logic_uScript_AddOnScreenMessage_speaker_24, logic_uScript_AddOnScreenMessage_side_24);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_24.Out)
		{
			Relay_True_39();
		}
	}

	private void Relay_In_26()
	{
		int num = 0;
		Array array = msgTradingStationFound;
		if (logic_uScript_AddOnScreenMessage_locString_26.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_26, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_26, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_26 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_26 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_26.In(logic_uScript_AddOnScreenMessage_locString_26, logic_uScript_AddOnScreenMessage_msgPriority_26, logic_uScript_AddOnScreenMessage_holdMsg_26, logic_uScript_AddOnScreenMessage_tag_26, logic_uScript_AddOnScreenMessage_speaker_26, logic_uScript_AddOnScreenMessage_side_26);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_26.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_Output1_28()
	{
		Relay_In_22();
	}

	private void Relay_Output2_28()
	{
		Relay_In_42();
	}

	private void Relay_Output3_28()
	{
		Relay_In_54();
	}

	private void Relay_Output4_28()
	{
	}

	private void Relay_Output5_28()
	{
	}

	private void Relay_Output6_28()
	{
	}

	private void Relay_Output7_28()
	{
	}

	private void Relay_Output8_28()
	{
	}

	private void Relay_In_28()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_28 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_28.In(logic_uScriptCon_ManualSwitch_CurrentOutput_28);
	}

	private void Relay_In_31()
	{
		logic_uScript_IsHUDElementVisible_hudElement_31 = local_53_ManHUD_HUDElementType;
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_31.In(logic_uScript_IsHUDElementVisible_hudElement_31);
		bool num = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_31.True;
		bool flag = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_31.False;
		if (num)
		{
			Relay_In_37();
		}
		if (flag)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_32()
	{
		int num = 0;
		Array array = msgOpenTheMissionBoard;
		if (logic_uScript_AddOnScreenMessage_locString_32.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_32, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_32, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_32 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_32 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_32.In(logic_uScript_AddOnScreenMessage_locString_32, logic_uScript_AddOnScreenMessage_msgPriority_32, logic_uScript_AddOnScreenMessage_holdMsg_32, logic_uScript_AddOnScreenMessage_tag_32, logic_uScript_AddOnScreenMessage_speaker_32, logic_uScript_AddOnScreenMessage_side_32);
	}

	private void Relay_In_37()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_37 = local_msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_37.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_37, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_37);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_37.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_True_39()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.True(out logic_uScriptAct_SetBool_Target_39);
		local_ShownMsgFindTradingStation_System_Boolean = logic_uScriptAct_SetBool_Target_39;
	}

	private void Relay_False_39()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.False(out logic_uScriptAct_SetBool_Target_39);
		local_ShownMsgFindTradingStation_System_Boolean = logic_uScriptAct_SetBool_Target_39;
	}

	private void Relay_In_42()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_42 = local_TradingStationTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_42.In(logic_uScript_IsPlayerInRangeOfTech_tech_42, logic_uScript_IsPlayerInRangeOfTech_range_42, logic_uScript_IsPlayerInRangeOfTech_techs_42);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_42.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_42.OutOfRange;
		if (inRange)
		{
			Relay_In_31();
		}
		if (outOfRange)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_43()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_43.In(logic_uScriptAct_SetInt_Value_43, out logic_uScriptAct_SetInt_Target_43);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_43;
	}

	private void Relay_In_45()
	{
		logic_uScriptCon_CompareBool_Bool_45 = local_ShownMsgFindTradingStation_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.In(logic_uScriptCon_CompareBool_Bool_45);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.False;
		if (num)
		{
			Relay_In_47();
		}
		if (flag)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_47()
	{
		logic_uScript_Wait_uScript_Wait_47.In(logic_uScript_Wait_seconds_47, logic_uScript_Wait_repeat_47);
		if (logic_uScript_Wait_uScript_Wait_47.Waited)
		{
			Relay_False_39();
		}
	}

	private void Relay_Save_Out_49()
	{
		Relay_Save_17();
	}

	private void Relay_Load_Out_49()
	{
		Relay_Load_17();
	}

	private void Relay_Restart_Out_49()
	{
		Relay_Restart_17();
	}

	private void Relay_Save_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_ShownMsgFindTradingStation_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_ShownMsgFindTradingStation_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Load_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_ShownMsgFindTradingStation_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_ShownMsgFindTradingStation_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Set_True_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_ShownMsgFindTradingStation_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_ShownMsgFindTradingStation_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Set_False_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_ShownMsgFindTradingStation_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_ShownMsgFindTradingStation_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Out_50()
	{
	}

	private void Relay_In_50()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_50 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_50, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_50);
	}

	private void Relay_Out_52()
	{
	}

	private void Relay_In_52()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_52 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_52.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_52, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_52);
	}

	private void Relay_In_54()
	{
		int num = 0;
		Array array = msgAcceptAMission;
		if (logic_uScript_AddOnScreenMessage_locString_54.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_54, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_54, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_54 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_54.In(logic_uScript_AddOnScreenMessage_locString_54, logic_uScript_AddOnScreenMessage_msgPriority_54, logic_uScript_AddOnScreenMessage_holdMsg_54, logic_uScript_AddOnScreenMessage_tag_54, logic_uScript_AddOnScreenMessage_speaker_54, logic_uScript_AddOnScreenMessage_side_54);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_54.Out)
		{
			Relay_Succeed_13();
		}
	}
}
