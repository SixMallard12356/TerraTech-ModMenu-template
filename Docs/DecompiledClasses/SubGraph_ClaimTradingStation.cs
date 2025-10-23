using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_ClaimTradingStation", "")]
public class SubGraph_ClaimTradingStation : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_5 = new SpawnTechData[0];

	private SpawnTechData[] external_6 = new SpawnTechData[0];

	private LocalisedString[] external_31 = new LocalisedString[0];

	private LocalisedString[] external_32 = new LocalisedString[0];

	private LocalisedString[] external_33 = new LocalisedString[0];

	private LocalisedString[] external_35 = new LocalisedString[0];

	private Tank[] local_57_TankArray = new Tank[0];

	private Tank[] local_72_TankArray = new Tank[0];

	private Tank local_Boss_Tank;

	private Vector3 local_InitialPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private string local_msg_System_String = "msg";

	private ManOnScreenMessages.OnScreenMessage local_Msg1_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg2_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg6_ManOnScreenMessages_OnScreenMessage;

	private bool local_TechsSpawned_System_Boolean;

	private Tank local_TradingStation_Tank;

	private bool local_VendorPointerShown_System_Boolean;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_13;

	private GameObject owner_Connection_24;

	private GameObject owner_Connection_48;

	private GameObject owner_Connection_56;

	private GameObject owner_Connection_60;

	private GameObject owner_Connection_61;

	private GameObject owner_Connection_64;

	private GameObject owner_Connection_73;

	private GameObject owner_Connection_80;

	private GameObject owner_Connection_82;

	private GameObject owner_Connection_83;

	private GameObject owner_Connection_84;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_0 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_0 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_0;

	private bool logic_uScript_SetTankInvulnerable_Out_0 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_8 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_8;

	private object logic_uScript_SetEncounterTarget_visibleObject_8 = "";

	private bool logic_uScript_SetEncounterTarget_Out_8 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_10 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_10 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_10 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_10;

	private string logic_uScript_AddOnScreenMessage_tag_10 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_10;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_10;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_10;

	private bool logic_uScript_AddOnScreenMessage_Out_10 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_10 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_12 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_12;

	private bool logic_uScript_ClearEncounterTarget_Out_12 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_17 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_17;

	private bool logic_uScript_RemoveOnScreenMessage_instant_17;

	private bool logic_uScript_RemoveOnScreenMessage_Out_17 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_18 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_18 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_18;

	private string logic_uScript_AddOnScreenMessage_tag_18 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_18;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_18;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_18;

	private bool logic_uScript_AddOnScreenMessage_Out_18 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_18 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_20 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_20;

	private object logic_uScript_SetEncounterTarget_visibleObject_20 = "";

	private bool logic_uScript_SetEncounterTarget_Out_20 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_21 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_21 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_21 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_21;

	private string logic_uScript_AddOnScreenMessage_tag_21 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_21;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_21;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_21;

	private bool logic_uScript_AddOnScreenMessage_Out_21 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_21 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_23;

	private bool logic_uScriptCon_CompareBool_True_23 = true;

	private bool logic_uScriptCon_CompareBool_False_23 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_26 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_26;

	private float logic_uScript_IsPlayerInRangeOfTech_range_26 = 50f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_26 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_26 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_26 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_26 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_27;

	private float logic_uScript_IsPlayerInRangeOfTech_range_27 = 150f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_27 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_27 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_27 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_27 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_28 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_28;

	private bool logic_uScriptAct_SetBool_Out_28 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_28 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_28 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_29 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_29;

	private bool logic_uScript_RemoveOnScreenMessage_instant_29;

	private bool logic_uScript_RemoveOnScreenMessage_Out_29 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_30 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_30;

	private float logic_uScript_IsPlayerInRangeOfTech_range_30 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_30 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_30 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_30 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_30 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_36 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_36 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_36;

	private string logic_uScript_AddOnScreenMessage_tag_36 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_36;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_36;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_36;

	private bool logic_uScript_AddOnScreenMessage_Out_36 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_36 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_44 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_44 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_44 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_44 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_46 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_46;

	private object logic_uScript_SetEncounterTarget_visibleObject_46 = "";

	private bool logic_uScript_SetEncounterTarget_Out_46 = true;

	private uScript_AI_SetPOI logic_uScript_AI_SetPOI_uScript_AI_SetPOI_50 = new uScript_AI_SetPOI();

	private Tank logic_uScript_AI_SetPOI_tank_50;

	private bool logic_uScript_AI_SetPOI_usePOI_50 = true;

	private Vector3 logic_uScript_AI_SetPOI_position_50;

	private float logic_uScript_AI_SetPOI_distance_50 = 250f;

	private bool logic_uScript_AI_SetPOI_Out_50 = true;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_53 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_53;

	private string logic_uScript_GetPositionInEncounter_posName_53 = "BossPos";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_53;

	private bool logic_uScript_GetPositionInEncounter_Out_53 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_58 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_58 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_58;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_58 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_58 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_62 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_62 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_62;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_62 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_62 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_63 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_63;

	private bool logic_uScriptAct_SetBool_Out_63 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_63 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_63 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_65 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_65 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_65;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_65 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_65;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_65 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_65 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_65 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_65 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_66;

	private bool logic_uScriptCon_CompareBool_True_66 = true;

	private bool logic_uScriptCon_CompareBool_False_66 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_67 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_67 = new Tank[0];

	private int logic_uScript_AccessListTech_index_67;

	private Tank logic_uScript_AccessListTech_value_67;

	private bool logic_uScript_AccessListTech_Out_67 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_69 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_69;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_69 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_69;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_69 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_69 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_69 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_69 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_71 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_71 = new Tank[0];

	private int logic_uScript_AccessListTech_index_71;

	private Tank logic_uScript_AccessListTech_value_71;

	private bool logic_uScript_AccessListTech_Out_71 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_75 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_75;

	private GameObject logic_uScript_LoadBool_owner_75;

	private string logic_uScript_LoadBool_uniqueName_75 = "VendorPointerShown";

	private bool logic_uScript_LoadBool_Out_75 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_79 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_79;

	private bool logic_uScriptAct_SetBool_Out_79 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_79 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_79 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_81 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_81;

	private bool logic_uScriptAct_SetBool_Out_81 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_81 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_81 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_86 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_86;

	private GameObject logic_uScript_LoadBool_owner_86;

	private string logic_uScript_LoadBool_uniqueName_86 = "TechsSpawned";

	private bool logic_uScript_LoadBool_Out_86 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_89 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_89 = "";

	private GameObject logic_uScript_SaveVariable_owner_89;

	private string logic_uScript_SaveVariable_uniqueId_89 = "TechsSpawned";

	private bool logic_uScript_SaveVariable_Out_89 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_90 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_90 = "";

	private GameObject logic_uScript_SaveVariable_owner_90;

	private string logic_uScript_SaveVariable_uniqueId_90 = "VendorPointerShown";

	private bool logic_uScript_SaveVariable_Out_90 = true;

	[FriendlyName("Complete")]
	public event uScriptEventHandler Complete;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_13 || !m_RegisteredForEvents)
		{
			owner_Connection_13 = parentGameObject;
		}
		if (null == owner_Connection_24 || !m_RegisteredForEvents)
		{
			owner_Connection_24 = parentGameObject;
		}
		if (null == owner_Connection_48 || !m_RegisteredForEvents)
		{
			owner_Connection_48 = parentGameObject;
		}
		if (null == owner_Connection_56 || !m_RegisteredForEvents)
		{
			owner_Connection_56 = parentGameObject;
		}
		if (null == owner_Connection_60 || !m_RegisteredForEvents)
		{
			owner_Connection_60 = parentGameObject;
		}
		if (null == owner_Connection_61 || !m_RegisteredForEvents)
		{
			owner_Connection_61 = parentGameObject;
		}
		if (null == owner_Connection_64 || !m_RegisteredForEvents)
		{
			owner_Connection_64 = parentGameObject;
		}
		if (null == owner_Connection_73 || !m_RegisteredForEvents)
		{
			owner_Connection_73 = parentGameObject;
			if (null != owner_Connection_73)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_73.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_73.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_85;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_85;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_85;
				}
			}
		}
		if (null == owner_Connection_80 || !m_RegisteredForEvents)
		{
			owner_Connection_80 = parentGameObject;
		}
		if (null == owner_Connection_82 || !m_RegisteredForEvents)
		{
			owner_Connection_82 = parentGameObject;
		}
		if (null == owner_Connection_83 || !m_RegisteredForEvents)
		{
			owner_Connection_83 = parentGameObject;
		}
		if (null == owner_Connection_84 || !m_RegisteredForEvents)
		{
			owner_Connection_84 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_73)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_73.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_73.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_85;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_85;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_85;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_73)
		{
			uScript_SaveLoad component = owner_Connection_73.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_85;
				component.LoadEvent -= Instance_LoadEvent_85;
				component.RestartEvent -= Instance_RestartEvent_85;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_0.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_8.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_10.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_12.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_17.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_20.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_21.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_26.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_29.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_30.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_44.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_46.SetParent(g);
		logic_uScript_AI_SetPOI_uScript_AI_SetPOI_50.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_53.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_58.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_62.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_65.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_67.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_71.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_75.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_79.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_81.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_86.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_89.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_90.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_13 = parentGameObject;
		owner_Connection_24 = parentGameObject;
		owner_Connection_48 = parentGameObject;
		owner_Connection_56 = parentGameObject;
		owner_Connection_60 = parentGameObject;
		owner_Connection_61 = parentGameObject;
		owner_Connection_64 = parentGameObject;
		owner_Connection_73 = parentGameObject;
		owner_Connection_80 = parentGameObject;
		owner_Connection_82 = parentGameObject;
		owner_Connection_83 = parentGameObject;
		owner_Connection_84 = parentGameObject;
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
	}

	public void OnDisable()
	{
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_0.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_10.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_21.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_26.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_30.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_75.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_86.OnDisable();
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

	private void Instance_SaveEvent_85(object o, EventArgs e)
	{
		Relay_SaveEvent_85();
	}

	private void Instance_LoadEvent_85(object o, EventArgs e)
	{
		Relay_LoadEvent_85();
	}

	private void Instance_RestartEvent_85(object o, EventArgs e)
	{
		Relay_RestartEvent_85();
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("bossTechData", "")] SpawnTechData[] bossTechData, [FriendlyName("tradingStationData", "")] SpawnTechData[] tradingStationData, [FriendlyName("msgTradingStationSpotted", "")] LocalisedString[] msgTradingStationSpotted, [FriendlyName("msgBossSpotted", "")] LocalisedString[] msgBossSpotted, [FriendlyName("msgBossInRange", "")] LocalisedString[] msgBossInRange, [FriendlyName("msgBossDefeated", "")] LocalisedString[] msgBossDefeated)
	{
		external_5 = bossTechData;
		external_6 = tradingStationData;
		external_31 = msgTradingStationSpotted;
		external_32 = msgBossSpotted;
		external_33 = msgBossInRange;
		external_35 = msgBossDefeated;
		Relay_In_66();
	}

	private void Relay_In_0()
	{
		logic_uScript_SetTankInvulnerable_tank_0 = local_TradingStation_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_0.In(logic_uScript_SetTankInvulnerable_invulnerable_0, logic_uScript_SetTankInvulnerable_tank_0);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_0.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_Connection_4()
	{
	}

	private void Relay_Connection_5()
	{
	}

	private void Relay_Connection_6()
	{
	}

	private void Relay_Connection_7()
	{
		if (this.Complete != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Complete(this, args);
		}
	}

	private void Relay_In_8()
	{
		logic_uScript_SetEncounterTarget_owner_8 = owner_Connection_9;
		logic_uScript_SetEncounterTarget_visibleObject_8 = local_Boss_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_8.In(logic_uScript_SetEncounterTarget_owner_8, logic_uScript_SetEncounterTarget_visibleObject_8);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_8.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_10()
	{
		int num = 0;
		Array array = external_31;
		if (logic_uScript_AddOnScreenMessage_locString_10.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_10, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_10, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_10 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_10 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_10.In(logic_uScript_AddOnScreenMessage_locString_10, logic_uScript_AddOnScreenMessage_msgPriority_10, logic_uScript_AddOnScreenMessage_holdMsg_10, logic_uScript_AddOnScreenMessage_tag_10, logic_uScript_AddOnScreenMessage_speaker_10, logic_uScript_AddOnScreenMessage_side_10);
		local_Msg6_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_10;
	}

	private void Relay_In_12()
	{
		logic_uScript_ClearEncounterTarget_owner_12 = owner_Connection_13;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_12.In(logic_uScript_ClearEncounterTarget_owner_12);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_12.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_17()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_17 = local_Msg1_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_17.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_17, logic_uScript_RemoveOnScreenMessage_instant_17);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_17.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_18()
	{
		int num = 0;
		Array array = external_32;
		if (logic_uScript_AddOnScreenMessage_locString_18.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_18, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_18, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_18 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_18 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.In(logic_uScript_AddOnScreenMessage_locString_18, logic_uScript_AddOnScreenMessage_msgPriority_18, logic_uScript_AddOnScreenMessage_holdMsg_18, logic_uScript_AddOnScreenMessage_tag_18, logic_uScript_AddOnScreenMessage_speaker_18, logic_uScript_AddOnScreenMessage_side_18);
		local_Msg2_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_18;
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_SetEncounterTarget_owner_20 = owner_Connection_24;
		logic_uScript_SetEncounterTarget_visibleObject_20 = local_TradingStation_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_20.In(logic_uScript_SetEncounterTarget_owner_20, logic_uScript_SetEncounterTarget_visibleObject_20);
	}

	private void Relay_In_21()
	{
		int num = 0;
		Array array = external_33;
		if (logic_uScript_AddOnScreenMessage_locString_21.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_21, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_21, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_21 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_21 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_21.In(logic_uScript_AddOnScreenMessage_locString_21, logic_uScript_AddOnScreenMessage_msgPriority_21, logic_uScript_AddOnScreenMessage_holdMsg_21, logic_uScript_AddOnScreenMessage_tag_21, logic_uScript_AddOnScreenMessage_speaker_21, logic_uScript_AddOnScreenMessage_side_21);
		local_Msg1_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_21;
	}

	private void Relay_In_23()
	{
		logic_uScriptCon_CompareBool_Bool_23 = local_VendorPointerShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.In(logic_uScriptCon_CompareBool_Bool_23);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.False)
		{
			Relay_True_28();
		}
	}

	private void Relay_In_26()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_26 = local_TradingStation_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_26.In(logic_uScript_IsPlayerInRangeOfTech_tech_26, logic_uScript_IsPlayerInRangeOfTech_range_26, logic_uScript_IsPlayerInRangeOfTech_techs_26);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_26.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_26.OutOfRange;
		if (inRange)
		{
			Relay_In_8();
		}
		if (outOfRange)
		{
			Relay_In_17();
		}
	}

	private void Relay_In_27()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_27 = local_TradingStation_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27.In(logic_uScript_IsPlayerInRangeOfTech_tech_27, logic_uScript_IsPlayerInRangeOfTech_range_27, logic_uScript_IsPlayerInRangeOfTech_techs_27);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27.OutOfRange)
		{
			Relay_In_12();
		}
	}

	private void Relay_True_28()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.True(out logic_uScriptAct_SetBool_Target_28);
		local_VendorPointerShown_System_Boolean = logic_uScriptAct_SetBool_Target_28;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_28.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_False_28()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.False(out logic_uScriptAct_SetBool_Target_28);
		local_VendorPointerShown_System_Boolean = logic_uScriptAct_SetBool_Target_28;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_28.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_29()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_29 = local_Msg2_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_29.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_29, logic_uScript_RemoveOnScreenMessage_instant_29);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_29.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_30()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_30 = local_TradingStation_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_30.In(logic_uScript_IsPlayerInRangeOfTech_tech_30, logic_uScript_IsPlayerInRangeOfTech_range_30, logic_uScript_IsPlayerInRangeOfTech_techs_30);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_30.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_30.OutOfRange;
		if (inRange)
		{
			Relay_In_18();
		}
		if (outOfRange)
		{
			Relay_In_29();
		}
	}

	private void Relay_Connection_31()
	{
	}

	private void Relay_Connection_32()
	{
	}

	private void Relay_Connection_33()
	{
	}

	private void Relay_Connection_35()
	{
	}

	private void Relay_In_36()
	{
		int num = 0;
		Array array = external_35;
		if (logic_uScript_AddOnScreenMessage_locString_36.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_36, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_36, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_36 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.In(logic_uScript_AddOnScreenMessage_locString_36, logic_uScript_AddOnScreenMessage_msgPriority_36, logic_uScript_AddOnScreenMessage_holdMsg_36, logic_uScript_AddOnScreenMessage_tag_36, logic_uScript_AddOnScreenMessage_speaker_36, logic_uScript_AddOnScreenMessage_side_36);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.Shown)
		{
			Relay_Connection_7();
		}
	}

	private void Relay_In_44()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_44 = local_msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_44.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_44, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_44);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_44.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_In_46()
	{
		logic_uScript_SetEncounterTarget_owner_46 = owner_Connection_48;
		logic_uScript_SetEncounterTarget_visibleObject_46 = local_TradingStation_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_46.In(logic_uScript_SetEncounterTarget_owner_46, logic_uScript_SetEncounterTarget_visibleObject_46);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_46.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_AI_SetPOI_tank_50 = local_Boss_Tank;
		logic_uScript_AI_SetPOI_position_50 = local_InitialPos_UnityEngine_Vector3;
		logic_uScript_AI_SetPOI_uScript_AI_SetPOI_50.In(logic_uScript_AI_SetPOI_tank_50, logic_uScript_AI_SetPOI_usePOI_50, logic_uScript_AI_SetPOI_position_50, logic_uScript_AI_SetPOI_distance_50);
		if (logic_uScript_AI_SetPOI_uScript_AI_SetPOI_50.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_In_53()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_53 = owner_Connection_56;
		logic_uScript_GetPositionInEncounter_Return_53 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_53.In(logic_uScript_GetPositionInEncounter_ownerNode_53, logic_uScript_GetPositionInEncounter_posName_53);
		local_InitialPos_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_53;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_53.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_InitialSpawn_58()
	{
		int num = 0;
		Array array = external_5;
		if (logic_uScript_SpawnTechsFromData_spawnData_58.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_58, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_58, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_58 = owner_Connection_64;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_58.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_58, logic_uScript_SpawnTechsFromData_ownerNode_58, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_58);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_58.Out)
		{
			Relay_InitialSpawn_62();
		}
	}

	private void Relay_InitialSpawn_62()
	{
		int num = 0;
		Array array = external_6;
		if (logic_uScript_SpawnTechsFromData_spawnData_62.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_62, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_62, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_62 = owner_Connection_60;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_62.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_62, logic_uScript_SpawnTechsFromData_ownerNode_62, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_62);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_62.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_True_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.True(out logic_uScriptAct_SetBool_Target_63);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_63;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_63.Out)
		{
			Relay_InitialSpawn_58();
		}
	}

	private void Relay_False_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.False(out logic_uScriptAct_SetBool_Target_63);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_63;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_63.Out)
		{
			Relay_InitialSpawn_58();
		}
	}

	private void Relay_In_65()
	{
		int num = 0;
		Array array = external_5;
		if (logic_uScript_GetAndCheckTechs_techData_65.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_65, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_65, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_65 = owner_Connection_61;
		int num2 = 0;
		Array array2 = local_57_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_65.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_65, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_65, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_65 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_65.In(logic_uScript_GetAndCheckTechs_techData_65, logic_uScript_GetAndCheckTechs_ownerNode_65, ref logic_uScript_GetAndCheckTechs_techs_65);
		local_57_TankArray = logic_uScript_GetAndCheckTechs_techs_65;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_65.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_65.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_65.AllDead;
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
			Relay_In_46();
		}
	}

	private void Relay_In_66()
	{
		logic_uScriptCon_CompareBool_Bool_66 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66.In(logic_uScriptCon_CompareBool_Bool_66);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66.False;
		if (num)
		{
			Relay_In_69();
		}
		if (flag)
		{
			Relay_True_63();
		}
	}

	private void Relay_AtIndex_67()
	{
		int num = 0;
		Array array = local_57_TankArray;
		if (logic_uScript_AccessListTech_techList_67.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_67, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_67, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_67.AtIndex(ref logic_uScript_AccessListTech_techList_67, logic_uScript_AccessListTech_index_67, out logic_uScript_AccessListTech_value_67);
		local_57_TankArray = logic_uScript_AccessListTech_techList_67;
		local_Boss_Tank = logic_uScript_AccessListTech_value_67;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_67.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_In_69()
	{
		int num = 0;
		Array array = external_6;
		if (logic_uScript_GetAndCheckTechs_techData_69.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_69, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_69, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_69 = owner_Connection_1;
		int num2 = 0;
		Array array2 = local_72_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_69.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_69, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_69, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_69 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.In(logic_uScript_GetAndCheckTechs_techData_69, logic_uScript_GetAndCheckTechs_ownerNode_69, ref logic_uScript_GetAndCheckTechs_techs_69);
		local_72_TankArray = logic_uScript_GetAndCheckTechs_techs_69;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_71();
		}
		if (someAlive)
		{
			Relay_AtIndex_71();
		}
	}

	private void Relay_AtIndex_71()
	{
		int num = 0;
		Array array = local_72_TankArray;
		if (logic_uScript_AccessListTech_techList_71.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_71, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_71, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_71.AtIndex(ref logic_uScript_AccessListTech_techList_71, logic_uScript_AccessListTech_index_71, out logic_uScript_AccessListTech_value_71);
		local_72_TankArray = logic_uScript_AccessListTech_techList_71;
		local_TradingStation_Tank = logic_uScript_AccessListTech_value_71;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_71.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_In_75()
	{
		logic_uScript_LoadBool_data_75 = local_VendorPointerShown_System_Boolean;
		logic_uScript_LoadBool_owner_75 = owner_Connection_84;
		logic_uScript_LoadBool_uScript_LoadBool_75.In(ref logic_uScript_LoadBool_data_75, logic_uScript_LoadBool_owner_75, logic_uScript_LoadBool_uniqueName_75);
		local_VendorPointerShown_System_Boolean = logic_uScript_LoadBool_data_75;
	}

	private void Relay_True_79()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_79.True(out logic_uScriptAct_SetBool_Target_79);
		local_VendorPointerShown_System_Boolean = logic_uScriptAct_SetBool_Target_79;
	}

	private void Relay_False_79()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_79.False(out logic_uScriptAct_SetBool_Target_79);
		local_VendorPointerShown_System_Boolean = logic_uScriptAct_SetBool_Target_79;
	}

	private void Relay_True_81()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_81.True(out logic_uScriptAct_SetBool_Target_81);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_81;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_81.Out)
		{
			Relay_False_79();
		}
	}

	private void Relay_False_81()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_81.False(out logic_uScriptAct_SetBool_Target_81);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_81;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_81.Out)
		{
			Relay_False_79();
		}
	}

	private void Relay_SaveEvent_85()
	{
		Relay_In_89();
	}

	private void Relay_LoadEvent_85()
	{
		Relay_In_86();
	}

	private void Relay_RestartEvent_85()
	{
		Relay_False_81();
	}

	private void Relay_In_86()
	{
		logic_uScript_LoadBool_data_86 = local_TechsSpawned_System_Boolean;
		logic_uScript_LoadBool_owner_86 = owner_Connection_82;
		logic_uScript_LoadBool_uScript_LoadBool_86.In(ref logic_uScript_LoadBool_data_86, logic_uScript_LoadBool_owner_86, logic_uScript_LoadBool_uniqueName_86);
		local_TechsSpawned_System_Boolean = logic_uScript_LoadBool_data_86;
		if (logic_uScript_LoadBool_uScript_LoadBool_86.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_In_89()
	{
		logic_uScript_SaveVariable_variable_89 = local_TechsSpawned_System_Boolean;
		logic_uScript_SaveVariable_owner_89 = owner_Connection_83;
		logic_uScript_SaveVariable_uScript_SaveVariable_89.In(logic_uScript_SaveVariable_variable_89, logic_uScript_SaveVariable_owner_89, logic_uScript_SaveVariable_uniqueId_89);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_89.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_90()
	{
		logic_uScript_SaveVariable_variable_90 = local_VendorPointerShown_System_Boolean;
		logic_uScript_SaveVariable_owner_90 = owner_Connection_80;
		logic_uScript_SaveVariable_uScript_SaveVariable_90.In(logic_uScript_SaveVariable_variable_90, logic_uScript_SaveVariable_owner_90, logic_uScript_SaveVariable_uniqueId_90);
	}
}
