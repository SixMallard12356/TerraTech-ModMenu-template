using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "msg\n")]
[NodePath("Graphs")]
public class Prototype_NavigateToWaypoint : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool destinationIsLocation;

	public bool destinationIsTech;

	[Multiline(3)]
	public string destinationLocationPrefab = "";

	public float destinationRadius = 10f;

	public SpawnTechData destinationTech;

	public SpawnTechData enemyTechData;

	public bool itemDelivery;

	public ItemTypeInfo itemDeliveryData;

	public int itemDeliveryNum = 1;

	private float local_17_System_Single;

	private BlockTypes local_37_BlockTypes = BlockTypes.GSOTractorMini_111;

	private Tank[] local_60_TankArray = new Tank[0];

	private Tank[] local_76_TankArray = new Tank[0];

	private Tank local_DestinationTech_Tank;

	private bool local_DestinationTechSpawned_System_Boolean;

	private bool local_EnemiesSpawned_System_Boolean;

	private bool local_MissionStarted_System_Boolean;

	private string local_MsgMinion_System_String = "MsgMinion";

	private string local_MsgTony_System_String = "MsgTony";

	private Tank local_OriginTech_Tank;

	private Tank local_Player_Tank;

	public LocalisedString[] msgLose = new LocalisedString[0];

	public LocalisedString[] msgMissionPrototype001 = new LocalisedString[0];

	public LocalisedString[] msgMissionPrototype002 = new LocalisedString[0];

	public LocalisedString[] msgMissionPrototype003 = new LocalisedString[0];

	public LocalisedString[] msgMissionPrototype004 = new LocalisedString[0];

	public float originRadius = 10f;

	public bool timeLimit;

	public float timeLimitInSecs = 60f;

	private GameObject owner_Connection_25;

	private GameObject owner_Connection_32;

	private GameObject owner_Connection_64;

	private GameObject owner_Connection_69;

	private GameObject owner_Connection_75;

	private GameObject owner_Connection_77;

	private GameObject owner_Connection_82;

	private GameObject owner_Connection_86;

	private GameObject owner_Connection_88;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_0 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_0 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_0;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_0 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_0 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_0 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_3 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_3 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_3 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_3;

	private string logic_uScript_AddOnScreenMessage_tag_3 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_3;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_3;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_3;

	private bool logic_uScript_AddOnScreenMessage_Out_3 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_3 = true;

	private uScript_IsTechHolding logic_uScript_IsTechHolding_uScript_IsTechHolding_4 = new uScript_IsTechHolding();

	private Tank logic_uScript_IsTechHolding_tech_4;

	private ItemTypeInfo logic_uScript_IsTechHolding_heldItem_4;

	private int logic_uScript_IsTechHolding_Quantity_4;

	private bool logic_uScript_IsTechHolding_True_4 = true;

	private bool logic_uScript_IsTechHolding_False_4 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_5;

	private bool logic_uScriptCon_CompareBool_True_5 = true;

	private bool logic_uScriptCon_CompareBool_False_5 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_7 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_7;

	private bool logic_uScript_GetPlayerTank_Returned_7 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_7 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_13;

	private bool logic_uScriptCon_CompareBool_True_13 = true;

	private bool logic_uScriptCon_CompareBool_False_13 = true;

	private uScriptAct_Stopwatch logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_14 = new uScriptAct_Stopwatch();

	private float logic_uScriptAct_Stopwatch_Seconds_14;

	private bool logic_uScriptAct_Stopwatch_Started_14 = true;

	private bool logic_uScriptAct_Stopwatch_Stopped_14 = true;

	private bool logic_uScriptAct_Stopwatch_Reset_14 = true;

	private bool logic_uScriptAct_Stopwatch_CheckedTime_14 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_16 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_16;

	private float logic_uScriptCon_CompareFloat_B_16;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_16 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_16 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_16 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_16 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_16 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_16 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_19 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_19 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_19 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_19;

	private string logic_uScript_AddOnScreenMessage_tag_19 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_19;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_19;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_19;

	private bool logic_uScript_AddOnScreenMessage_Out_19 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_19 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_21;

	private bool logic_uScriptCon_CompareBool_True_21 = true;

	private bool logic_uScriptCon_CompareBool_False_21 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_24;

	private bool logic_uScriptCon_CompareBool_True_24 = true;

	private bool logic_uScriptCon_CompareBool_False_24 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_30 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_30;

	private object logic_uScript_SetEncounterTarget_visibleObject_30 = "";

	private bool logic_uScript_SetEncounterTarget_Out_30 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_31 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_31 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_31;

	private bool logic_uScript_SetTankInvulnerable_Out_31 = true;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_34 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_34 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_34;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_34 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_34 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_34 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_36 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_36;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_36 = 1;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_36 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_36 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_39;

	private bool logic_uScriptCon_CompareBool_True_39 = true;

	private bool logic_uScriptCon_CompareBool_False_39 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_40 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_40;

	private bool logic_uScriptAct_SetBool_Out_40 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_40 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_40 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_42 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_42 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_42 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_42;

	private string logic_uScript_AddOnScreenMessage_tag_42 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_42;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_42;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_42;

	private bool logic_uScript_AddOnScreenMessage_Out_42 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_42 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_45 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_45 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_45 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_45 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_47 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_47 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_47 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_47 = true;

	private string logic_uScript_AddOnScreenMessage_tag_47 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_47;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_47;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_47;

	private bool logic_uScript_AddOnScreenMessage_Out_47 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_47 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_50 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_50 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_50 = true;

	private string logic_uScript_AddOnScreenMessage_tag_50 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_50;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_50;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_50;

	private bool logic_uScript_AddOnScreenMessage_Out_50 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_50 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_53 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_53 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_53 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_53 = true;

	private uScriptAct_SpawnPrefabAtLocation logic_uScriptAct_SpawnPrefabAtLocation_uScriptAct_SpawnPrefabAtLocation_56 = new uScriptAct_SpawnPrefabAtLocation();

	private string logic_uScriptAct_SpawnPrefabAtLocation_PrefabName_56 = "";

	private string logic_uScriptAct_SpawnPrefabAtLocation_ResourcePath_56 = "";

	private Vector3 logic_uScriptAct_SpawnPrefabAtLocation_SpawnPosition_56 = new Vector3(0f, 0f, 0f);

	private Quaternion logic_uScriptAct_SpawnPrefabAtLocation_SpawnRotation_56 = new Quaternion(0f, 0f, 0f, 0f);

	private string logic_uScriptAct_SpawnPrefabAtLocation_SpawnedName_56 = "";

	private GameObject logic_uScriptAct_SpawnPrefabAtLocation_SpawnedGameObject_56;

	private int logic_uScriptAct_SpawnPrefabAtLocation_SpawnedInstancedID_56;

	private bool logic_uScriptAct_SpawnPrefabAtLocation_Immediate_56 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_61 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_61;

	private bool logic_uScriptAct_SetBool_Out_61 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_61 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_61 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_62;

	private bool logic_uScriptCon_CompareBool_True_62 = true;

	private bool logic_uScriptCon_CompareBool_False_62 = true;

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

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_68 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_68 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_68;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_68 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_68 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_72 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_72 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_72;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_72 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_72 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_73;

	private bool logic_uScriptCon_CompareBool_True_73 = true;

	private bool logic_uScriptCon_CompareBool_False_73 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_74 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_74;

	private bool logic_uScriptAct_SetBool_Out_74 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_74 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_74 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_79 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_79;

	private object logic_uScript_SetEncounterTarget_visibleObject_79 = "";

	private bool logic_uScript_SetEncounterTarget_Out_79 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_80 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_80 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_80;

	private bool logic_uScript_SetTankInvulnerable_Out_80 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_81 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_81 = new Tank[0];

	private int logic_uScript_AccessListTech_index_81;

	private Tank logic_uScript_AccessListTech_value_81;

	private bool logic_uScript_AccessListTech_Out_81 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_85 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_85 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_85;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_85 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_85;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_85 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_85 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_85 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_85 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_87 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_87;

	private bool logic_uScript_FinishEncounter_Out_87 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_89 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_89;

	private bool logic_uScript_FinishEncounter_Out_89 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_25 || !m_RegisteredForEvents)
		{
			owner_Connection_25 = parentGameObject;
			if (null != owner_Connection_25)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_25.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_25.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_26;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_26;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_26;
				}
			}
		}
		if (null == owner_Connection_32 || !m_RegisteredForEvents)
		{
			owner_Connection_32 = parentGameObject;
		}
		if (null == owner_Connection_64 || !m_RegisteredForEvents)
		{
			owner_Connection_64 = parentGameObject;
		}
		if (null == owner_Connection_69 || !m_RegisteredForEvents)
		{
			owner_Connection_69 = parentGameObject;
		}
		if (null == owner_Connection_75 || !m_RegisteredForEvents)
		{
			owner_Connection_75 = parentGameObject;
		}
		if (null == owner_Connection_77 || !m_RegisteredForEvents)
		{
			owner_Connection_77 = parentGameObject;
		}
		if (null == owner_Connection_82 || !m_RegisteredForEvents)
		{
			owner_Connection_82 = parentGameObject;
		}
		if (null == owner_Connection_86 || !m_RegisteredForEvents)
		{
			owner_Connection_86 = parentGameObject;
		}
		if (null == owner_Connection_88 || !m_RegisteredForEvents)
		{
			owner_Connection_88 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_25)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_25.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_25.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_26;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_26;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_26;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_25)
		{
			uScript_EncounterUpdate component = owner_Connection_25.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_26;
				component.OnSuspend -= Instance_OnSuspend_26;
				component.OnResume -= Instance_OnResume_26;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_0.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_3.SetParent(g);
		logic_uScript_IsTechHolding_uScript_IsTechHolding_4.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_7.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.SetParent(g);
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_14.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_16.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_19.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_30.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_31.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_34.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_36.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_42.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_45.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_47.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_53.SetParent(g);
		logic_uScriptAct_SpawnPrefabAtLocation_uScriptAct_SpawnPrefabAtLocation_56.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_61.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_67.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_68.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_72.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_79.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_80.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_81.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_85.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_87.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_89.SetParent(g);
		owner_Connection_25 = parentGameObject;
		owner_Connection_32 = parentGameObject;
		owner_Connection_64 = parentGameObject;
		owner_Connection_69 = parentGameObject;
		owner_Connection_75 = parentGameObject;
		owner_Connection_77 = parentGameObject;
		owner_Connection_82 = parentGameObject;
		owner_Connection_86 = parentGameObject;
		owner_Connection_88 = parentGameObject;
	}

	public void Awake()
	{
		logic_uScriptAct_SpawnPrefabAtLocation_uScriptAct_SpawnPrefabAtLocation_56.FinishedSpawning += uScriptAct_SpawnPrefabAtLocation_FinishedSpawning_56;
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
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_0.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_3.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_19.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_31.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_34.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_36.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_42.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_47.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_80.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_14.Update();
		logic_uScriptAct_SpawnPrefabAtLocation_uScriptAct_SpawnPrefabAtLocation_56.Update();
	}

	public void OnDestroy()
	{
		logic_uScriptAct_SpawnPrefabAtLocation_uScriptAct_SpawnPrefabAtLocation_56.FinishedSpawning -= uScriptAct_SpawnPrefabAtLocation_FinishedSpawning_56;
	}

	private void Instance_OnUpdate_26(object o, EventArgs e)
	{
		Relay_OnUpdate_26();
	}

	private void Instance_OnSuspend_26(object o, EventArgs e)
	{
		Relay_OnSuspend_26();
	}

	private void Instance_OnResume_26(object o, EventArgs e)
	{
		Relay_OnResume_26();
	}

	private void uScriptAct_SpawnPrefabAtLocation_FinishedSpawning_56(object o, EventArgs e)
	{
		Relay_FinishedSpawning_56();
	}

	private void Relay_In_0()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_0 = local_DestinationTech_Tank;
		logic_uScript_IsPlayerInRangeOfVisible_range_0 = destinationRadius;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_0.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_0, logic_uScript_IsPlayerInRangeOfVisible_range_0);
		bool inRange = logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_0.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_0.OutOfRange;
		if (inRange)
		{
			Relay_In_4();
		}
		if (outOfRange)
		{
			Relay_In_53();
		}
	}

	private void Relay_In_3()
	{
		int num = 0;
		Array array = msgMissionPrototype003;
		if (logic_uScript_AddOnScreenMessage_locString_3.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_3, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_3, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_3 = local_MsgTony_System_String;
		logic_uScript_AddOnScreenMessage_Return_3 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_3.In(logic_uScript_AddOnScreenMessage_locString_3, logic_uScript_AddOnScreenMessage_msgPriority_3, logic_uScript_AddOnScreenMessage_holdMsg_3, logic_uScript_AddOnScreenMessage_tag_3, logic_uScript_AddOnScreenMessage_speaker_3, logic_uScript_AddOnScreenMessage_side_3);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_3.Out)
		{
			Relay_Succeed_89();
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_IsTechHolding_tech_4 = local_Player_Tank;
		logic_uScript_IsTechHolding_heldItem_4 = itemDeliveryData;
		logic_uScript_IsTechHolding_Quantity_4 = itemDeliveryNum;
		logic_uScript_IsTechHolding_uScript_IsTechHolding_4.In(logic_uScript_IsTechHolding_tech_4, logic_uScript_IsTechHolding_heldItem_4, logic_uScript_IsTechHolding_Quantity_4);
		bool num = logic_uScript_IsTechHolding_uScript_IsTechHolding_4.True;
		bool flag = logic_uScript_IsTechHolding_uScript_IsTechHolding_4.False;
		if (num)
		{
			Relay_In_3();
		}
		if (flag)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_5()
	{
		logic_uScriptCon_CompareBool_Bool_5 = itemDelivery;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.In(logic_uScriptCon_CompareBool_Bool_5);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.True)
		{
			Relay_In_0();
		}
	}

	private void Relay_In_7()
	{
		logic_uScript_GetPlayerTank_Return_7 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_7.In();
		local_Player_Tank = logic_uScript_GetPlayerTank_Return_7;
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_7.Returned)
		{
			Relay_In_62();
		}
	}

	private void Relay_In_13()
	{
		logic_uScriptCon_CompareBool_Bool_13 = timeLimit;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.In(logic_uScriptCon_CompareBool_Bool_13);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.False;
		if (num)
		{
			Relay_StartTimer_14();
		}
		if (flag)
		{
			Relay_In_5();
		}
	}

	private void Relay_StartTimer_14()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_14.StartTimer(out logic_uScriptAct_Stopwatch_Seconds_14);
		local_17_System_Single = logic_uScriptAct_Stopwatch_Seconds_14;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_14.Started)
		{
			Relay_In_16();
		}
	}

	private void Relay_Stop_14()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_14.Stop(out logic_uScriptAct_Stopwatch_Seconds_14);
		local_17_System_Single = logic_uScriptAct_Stopwatch_Seconds_14;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_14.Started)
		{
			Relay_In_16();
		}
	}

	private void Relay_ResetTimer_14()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_14.ResetTimer(out logic_uScriptAct_Stopwatch_Seconds_14);
		local_17_System_Single = logic_uScriptAct_Stopwatch_Seconds_14;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_14.Started)
		{
			Relay_In_16();
		}
	}

	private void Relay_CheckTime_14()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_14.CheckTime(out logic_uScriptAct_Stopwatch_Seconds_14);
		local_17_System_Single = logic_uScriptAct_Stopwatch_Seconds_14;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_14.Started)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_16()
	{
		logic_uScriptCon_CompareFloat_A_16 = local_17_System_Single;
		logic_uScriptCon_CompareFloat_B_16 = timeLimitInSecs;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_16.In(logic_uScriptCon_CompareFloat_A_16, logic_uScriptCon_CompareFloat_B_16);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_16.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_16.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_19();
		}
		if (lessThan)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_19()
	{
		int num = 0;
		Array array = msgLose;
		if (logic_uScript_AddOnScreenMessage_locString_19.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_19, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_19, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_19 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_19.In(logic_uScript_AddOnScreenMessage_locString_19, logic_uScript_AddOnScreenMessage_msgPriority_19, logic_uScript_AddOnScreenMessage_holdMsg_19, logic_uScript_AddOnScreenMessage_tag_19, logic_uScript_AddOnScreenMessage_speaker_19, logic_uScript_AddOnScreenMessage_side_19);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_19.Out)
		{
			Relay_Fail_87();
		}
	}

	private void Relay_In_21()
	{
		logic_uScriptCon_CompareBool_Bool_21 = destinationIsTech;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.In(logic_uScriptCon_CompareBool_Bool_21);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.False;
		if (num)
		{
			Relay_In_73();
		}
		if (flag)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_24()
	{
		logic_uScriptCon_CompareBool_Bool_24 = destinationIsLocation;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24.In(logic_uScriptCon_CompareBool_Bool_24);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24.True)
		{
			Relay_In_56();
		}
	}

	private void Relay_OnUpdate_26()
	{
		Relay_In_7();
	}

	private void Relay_OnSuspend_26()
	{
	}

	private void Relay_OnResume_26()
	{
	}

	private void Relay_In_30()
	{
		logic_uScript_SetEncounterTarget_owner_30 = owner_Connection_32;
		logic_uScript_SetEncounterTarget_visibleObject_30 = local_OriginTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_30.In(logic_uScript_SetEncounterTarget_owner_30, logic_uScript_SetEncounterTarget_visibleObject_30);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_30.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_31()
	{
		logic_uScript_SetTankInvulnerable_tank_31 = local_OriginTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_31.In(logic_uScript_SetTankInvulnerable_invulnerable_31, logic_uScript_SetTankInvulnerable_tank_31);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_31.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_34()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_34 = local_OriginTech_Tank;
		logic_uScript_IsPlayerInRangeOfVisible_range_34 = originRadius;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_34.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_34, logic_uScript_IsPlayerInRangeOfVisible_range_34);
		bool inRange = logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_34.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_34.OutOfRange;
		if (inRange)
		{
			Relay_In_36();
		}
		if (outOfRange)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_36()
	{
		logic_uScript_DoesPlayerTankHaveBlock_blockType_36 = local_37_BlockTypes;
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_36.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_36, logic_uScript_DoesPlayerTankHaveBlock_amount_36);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_36.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_36.False;
		if (num)
		{
			Relay_In_42();
		}
		if (flag)
		{
			Relay_In_47();
		}
	}

	private void Relay_In_39()
	{
		logic_uScriptCon_CompareBool_Bool_39 = local_MissionStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.In(logic_uScriptCon_CompareBool_Bool_39);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.False;
		if (num)
		{
			Relay_In_21();
		}
		if (flag)
		{
			Relay_In_34();
		}
	}

	private void Relay_True_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.True(out logic_uScriptAct_SetBool_Target_40);
		local_MissionStarted_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_False_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.False(out logic_uScriptAct_SetBool_Target_40);
		local_MissionStarted_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_42()
	{
		int num = 0;
		Array array = msgMissionPrototype002;
		if (logic_uScript_AddOnScreenMessage_locString_42.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_42, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_42, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_42 = local_MsgMinion_System_String;
		logic_uScript_AddOnScreenMessage_Return_42 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_42.In(logic_uScript_AddOnScreenMessage_locString_42, logic_uScript_AddOnScreenMessage_msgPriority_42, logic_uScript_AddOnScreenMessage_holdMsg_42, logic_uScript_AddOnScreenMessage_tag_42, logic_uScript_AddOnScreenMessage_speaker_42, logic_uScript_AddOnScreenMessage_side_42);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_42.Out)
		{
			Relay_True_40();
		}
	}

	private void Relay_In_45()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_45 = local_MsgMinion_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_45.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_45, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_45);
	}

	private void Relay_In_47()
	{
		int num = 0;
		Array array = msgMissionPrototype001;
		if (logic_uScript_AddOnScreenMessage_locString_47.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_47, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_47, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_47 = local_MsgMinion_System_String;
		logic_uScript_AddOnScreenMessage_Return_47 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_47.In(logic_uScript_AddOnScreenMessage_locString_47, logic_uScript_AddOnScreenMessage_msgPriority_47, logic_uScript_AddOnScreenMessage_holdMsg_47, logic_uScript_AddOnScreenMessage_tag_47, logic_uScript_AddOnScreenMessage_speaker_47, logic_uScript_AddOnScreenMessage_side_47);
	}

	private void Relay_In_50()
	{
		int num = 0;
		Array array = msgMissionPrototype004;
		if (logic_uScript_AddOnScreenMessage_locString_50.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_50, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_50, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_50 = local_MsgTony_System_String;
		logic_uScript_AddOnScreenMessage_Return_50 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50.In(logic_uScript_AddOnScreenMessage_locString_50, logic_uScript_AddOnScreenMessage_msgPriority_50, logic_uScript_AddOnScreenMessage_holdMsg_50, logic_uScript_AddOnScreenMessage_tag_50, logic_uScript_AddOnScreenMessage_speaker_50, logic_uScript_AddOnScreenMessage_side_50);
	}

	private void Relay_In_53()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_53 = local_MsgTony_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_53.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_53, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_53);
	}

	private void Relay_FinishedSpawning_56()
	{
		Relay_In_13();
	}

	private void Relay_In_56()
	{
		logic_uScriptAct_SpawnPrefabAtLocation_PrefabName_56 = destinationLocationPrefab;
		logic_uScriptAct_SpawnPrefabAtLocation_uScriptAct_SpawnPrefabAtLocation_56.In(logic_uScriptAct_SpawnPrefabAtLocation_PrefabName_56, logic_uScriptAct_SpawnPrefabAtLocation_ResourcePath_56, logic_uScriptAct_SpawnPrefabAtLocation_SpawnPosition_56, logic_uScriptAct_SpawnPrefabAtLocation_SpawnRotation_56, logic_uScriptAct_SpawnPrefabAtLocation_SpawnedName_56, out logic_uScriptAct_SpawnPrefabAtLocation_SpawnedGameObject_56, out logic_uScriptAct_SpawnPrefabAtLocation_SpawnedInstancedID_56);
	}

	private void Relay_True_61()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_61.True(out logic_uScriptAct_SetBool_Target_61);
		local_EnemiesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_61;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_61.Out)
		{
			Relay_InitialSpawn_68();
		}
	}

	private void Relay_False_61()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_61.False(out logic_uScriptAct_SetBool_Target_61);
		local_EnemiesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_61;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_61.Out)
		{
			Relay_InitialSpawn_68();
		}
	}

	private void Relay_In_62()
	{
		logic_uScriptCon_CompareBool_Bool_62 = local_EnemiesSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.In(logic_uScriptCon_CompareBool_Bool_62);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.False;
		if (num)
		{
			Relay_In_66();
		}
		if (flag)
		{
			Relay_True_61();
		}
	}

	private void Relay_In_66()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_66.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_66, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_66[num++] = enemyTechData;
		logic_uScript_GetAndCheckTechs_ownerNode_66 = owner_Connection_64;
		int num2 = 0;
		Array array = local_60_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_66.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_66, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_66, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_66 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.In(logic_uScript_GetAndCheckTechs_techData_66, logic_uScript_GetAndCheckTechs_ownerNode_66, ref logic_uScript_GetAndCheckTechs_techs_66);
		local_60_TankArray = logic_uScript_GetAndCheckTechs_techs_66;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_66.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_67();
		}
		if (someAlive)
		{
			Relay_AtIndex_67();
		}
	}

	private void Relay_AtIndex_67()
	{
		int num = 0;
		Array array = local_60_TankArray;
		if (logic_uScript_AccessListTech_techList_67.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_67, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_67, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_67.AtIndex(ref logic_uScript_AccessListTech_techList_67, logic_uScript_AccessListTech_index_67, out logic_uScript_AccessListTech_value_67);
		local_60_TankArray = logic_uScript_AccessListTech_techList_67;
		local_OriginTech_Tank = logic_uScript_AccessListTech_value_67;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_67.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_InitialSpawn_68()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_68.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_68, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_68[num++] = enemyTechData;
		logic_uScript_SpawnTechsFromData_ownerNode_68 = owner_Connection_69;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_68.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_68, logic_uScript_SpawnTechsFromData_ownerNode_68, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_68);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_68.Out)
		{
			Relay_In_66();
		}
	}

	private void Relay_InitialSpawn_72()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_72.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_72, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_72[num++] = destinationTech;
		logic_uScript_SpawnTechsFromData_ownerNode_72 = owner_Connection_75;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_72.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_72, logic_uScript_SpawnTechsFromData_ownerNode_72, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_72);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_72.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_73()
	{
		logic_uScriptCon_CompareBool_Bool_73 = local_DestinationTechSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73.In(logic_uScriptCon_CompareBool_Bool_73);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73.False;
		if (num)
		{
			Relay_In_85();
		}
		if (flag)
		{
			Relay_True_74();
		}
	}

	private void Relay_True_74()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.True(out logic_uScriptAct_SetBool_Target_74);
		local_DestinationTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_74;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_74.Out)
		{
			Relay_InitialSpawn_72();
		}
	}

	private void Relay_False_74()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.False(out logic_uScriptAct_SetBool_Target_74);
		local_DestinationTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_74;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_74.Out)
		{
			Relay_InitialSpawn_72();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_SetEncounterTarget_owner_79 = owner_Connection_77;
		logic_uScript_SetEncounterTarget_visibleObject_79 = local_DestinationTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_79.In(logic_uScript_SetEncounterTarget_owner_79, logic_uScript_SetEncounterTarget_visibleObject_79);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_79.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_80()
	{
		logic_uScript_SetTankInvulnerable_tank_80 = local_DestinationTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_80.In(logic_uScript_SetTankInvulnerable_invulnerable_80, logic_uScript_SetTankInvulnerable_tank_80);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_80.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_AtIndex_81()
	{
		int num = 0;
		Array array = local_76_TankArray;
		if (logic_uScript_AccessListTech_techList_81.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_81, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_81, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_81.AtIndex(ref logic_uScript_AccessListTech_techList_81, logic_uScript_AccessListTech_index_81, out logic_uScript_AccessListTech_value_81);
		local_76_TankArray = logic_uScript_AccessListTech_techList_81;
		local_DestinationTech_Tank = logic_uScript_AccessListTech_value_81;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_81.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_85()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_85.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_85, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_85[num++] = destinationTech;
		logic_uScript_GetAndCheckTechs_ownerNode_85 = owner_Connection_82;
		int num2 = 0;
		Array array = local_76_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_85.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_85, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_85, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_85 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_85.In(logic_uScript_GetAndCheckTechs_techData_85, logic_uScript_GetAndCheckTechs_ownerNode_85, ref logic_uScript_GetAndCheckTechs_techs_85);
		local_76_TankArray = logic_uScript_GetAndCheckTechs_techs_85;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_85.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_85.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_81();
		}
		if (someAlive)
		{
			Relay_AtIndex_81();
		}
	}

	private void Relay_Succeed_87()
	{
		logic_uScript_FinishEncounter_owner_87 = owner_Connection_86;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_87.Succeed(logic_uScript_FinishEncounter_owner_87);
	}

	private void Relay_Fail_87()
	{
		logic_uScript_FinishEncounter_owner_87 = owner_Connection_86;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_87.Fail(logic_uScript_FinishEncounter_owner_87);
	}

	private void Relay_Succeed_89()
	{
		logic_uScript_FinishEncounter_owner_89 = owner_Connection_88;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_89.Succeed(logic_uScript_FinishEncounter_owner_89);
	}

	private void Relay_Fail_89()
	{
		logic_uScript_FinishEncounter_owner_89 = owner_Connection_88;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_89.Fail(logic_uScript_FinishEncounter_owner_89);
	}
}
