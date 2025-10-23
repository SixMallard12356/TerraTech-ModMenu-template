using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_DefeatEnemyTechs", "")]
public class Mission_GSO_3_AITutorial : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public float distAtWhichTechFound;

	public FTUEEnumType FTUEAIMenuOpened;

	private bool local_AIMenuOpened_System_Boolean;

	private string local_Msg_System_String = "";

	private bool local_MsgFoundShown_System_Boolean;

	private Tank local_Tech_Tank;

	private bool local_TechFound_System_Boolean;

	private Tank[] local_Techs_TankArray = new Tank[0];

	private bool local_TechSpawned_System_Boolean;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01FindTech;

	public uScript_AddMessage.MessageData msg02TechFound;

	public uScript_AddMessage.MessageData msg03AccessMenu;

	public uScript_AddMessage.MessageData msg04TutorialComplete;

	public SpawnTechData[] techData = new SpawnTechData[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_23;

	private GameObject owner_Connection_24;

	private GameObject owner_Connection_37;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_3 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_3 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_3;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_3 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_3 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_5 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_5;

	private bool logic_uScriptAct_SetBool_Out_5 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_5 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_5 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_6 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_6;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_6 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_6;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_6 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_6 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_6 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_6 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_9;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_9 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_9 = "TechSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_11;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_11 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_11 = "AIMenuOpened";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_12;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_12 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_12 = "MsgFoundShown";

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_20 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_20 = new Tank[0];

	private int logic_uScript_AccessListTech_index_20;

	private Tank logic_uScript_AccessListTech_value_20;

	private bool logic_uScript_AccessListTech_Out_20 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_22 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_22;

	private object logic_uScript_SetEncounterTarget_visibleObject_22 = "";

	private bool logic_uScript_SetEncounterTarget_Out_22 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_25 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_25;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_25 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_25 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_29 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_29;

	private float logic_uScript_IsPlayerInRangeOfTech_range_29;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_29 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_29 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_29 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_29 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_33;

	private bool logic_uScriptCon_CompareBool_True_33 = true;

	private bool logic_uScriptCon_CompareBool_False_33 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_34 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_34;

	private bool logic_uScriptAct_SetBool_Out_34 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_34 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_34 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_36;

	private bool logic_uScriptCon_CompareBool_True_36 = true;

	private bool logic_uScriptCon_CompareBool_False_36 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_38 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_38;

	private int logic_uScript_SetTankTeam_team_38;

	private bool logic_uScript_SetTankTeam_Out_38 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_41 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_41 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_41 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_41 = true;

	private uScript_CheckFTUEAction logic_uScript_CheckFTUEAction_uScript_CheckFTUEAction_42 = new uScript_CheckFTUEAction();

	private FTUEEnumType logic_uScript_CheckFTUEAction_enumType_42;

	private bool logic_uScript_CheckFTUEAction_True_42 = true;

	private bool logic_uScript_CheckFTUEAction_False_42 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_44 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_44;

	private bool logic_uScriptAct_SetBool_Out_44 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_44 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_44 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_48;

	private bool logic_uScriptCon_CompareBool_True_48 = true;

	private bool logic_uScriptCon_CompareBool_False_48 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_50 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_50 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_50;

	private bool logic_uScript_SetTankInvulnerable_Out_50 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_52 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_52;

	private Tank logic_uScript_SetTankInvulnerable_tank_52;

	private bool logic_uScript_SetTankInvulnerable_Out_52 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_54;

	private bool logic_uScriptCon_CompareBool_True_54 = true;

	private bool logic_uScriptCon_CompareBool_False_54 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_56 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_56;

	private bool logic_uScriptAct_SetBool_Out_56 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_56 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_56 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_60 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_60;

	private float logic_uScript_IsPlayerInRangeOfTech_range_60;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_60 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_60 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_60 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_60 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_61 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_61 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_61 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_61 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_64;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_64 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_64 = "TechFound";

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_65 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_65;

	private bool logic_uScript_FinishEncounter_Out_65 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_66 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_66;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_66;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_66;

	private bool logic_uScript_AddMessage_Out_66 = true;

	private bool logic_uScript_AddMessage_Shown_66 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_71 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_71;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_71;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_71;

	private bool logic_uScript_AddMessage_Out_71 = true;

	private bool logic_uScript_AddMessage_Shown_71 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_73 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_73;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_73;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_73;

	private bool logic_uScript_AddMessage_Out_73 = true;

	private bool logic_uScript_AddMessage_Shown_73 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_77 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_77;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_77;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_77;

	private bool logic_uScript_AddMessage_Out_77 = true;

	private bool logic_uScript_AddMessage_Shown_77 = true;

	private uScript_DiscoverBlock logic_uScript_DiscoverBlock_uScript_DiscoverBlock_78 = new uScript_DiscoverBlock();

	private BlockTypes logic_uScript_DiscoverBlock_blockType_78 = BlockTypes.GSOAIGuardController_111;

	private bool logic_uScript_DiscoverBlock_Out_78 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
		}
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
			if (null != owner_Connection_14)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_14.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_14.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_15;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_15;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_15;
				}
			}
		}
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
			if (null != owner_Connection_17)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_17.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_17.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_16;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_16;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_16;
				}
			}
		}
		if (null == owner_Connection_23 || !m_RegisteredForEvents)
		{
			owner_Connection_23 = parentGameObject;
		}
		if (null == owner_Connection_24 || !m_RegisteredForEvents)
		{
			owner_Connection_24 = parentGameObject;
		}
		if (null == owner_Connection_37 || !m_RegisteredForEvents)
		{
			owner_Connection_37 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_14)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_14.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_14.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_15;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_15;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_15;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_17)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_17.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_17.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_16;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_16;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_16;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_14)
		{
			uScript_SaveLoad component = owner_Connection_14.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_15;
				component.LoadEvent -= Instance_LoadEvent_15;
				component.RestartEvent -= Instance_RestartEvent_15;
			}
		}
		if (null != owner_Connection_17)
		{
			uScript_EncounterUpdate component2 = owner_Connection_17.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_16;
				component2.OnSuspend -= Instance_OnSuspend_16;
				component2.OnResume -= Instance_OnResume_16;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_3.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_5.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_20.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_22.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_25.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_29.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_34.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_38.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_41.SetParent(g);
		logic_uScript_CheckFTUEAction_uScript_CheckFTUEAction_42.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_50.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_52.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_56.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_60.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_61.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_65.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_66.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_71.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_73.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_77.SetParent(g);
		logic_uScript_DiscoverBlock_uScript_DiscoverBlock_78.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_23 = parentGameObject;
		owner_Connection_24 = parentGameObject;
		owner_Connection_37 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out += SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out += SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save_Out += SubGraph_SaveLoadBool_Save_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load_Out += SubGraph_SaveLoadBool_Load_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save_Out += SubGraph_SaveLoadBool_Save_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load_Out += SubGraph_SaveLoadBool_Load_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Save_Out += SubGraph_SaveLoadBool_Save_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Load_Out += SubGraph_SaveLoadBool_Load_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_64;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_29.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_50.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_52.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_60.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_66.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_71.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_73.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_77.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out -= SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out -= SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save_Out -= SubGraph_SaveLoadBool_Save_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load_Out -= SubGraph_SaveLoadBool_Load_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save_Out -= SubGraph_SaveLoadBool_Save_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load_Out -= SubGraph_SaveLoadBool_Load_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Save_Out -= SubGraph_SaveLoadBool_Save_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Load_Out -= SubGraph_SaveLoadBool_Load_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_64;
	}

	private void Instance_SaveEvent_15(object o, EventArgs e)
	{
		Relay_SaveEvent_15();
	}

	private void Instance_LoadEvent_15(object o, EventArgs e)
	{
		Relay_LoadEvent_15();
	}

	private void Instance_RestartEvent_15(object o, EventArgs e)
	{
		Relay_RestartEvent_15();
	}

	private void Instance_OnUpdate_16(object o, EventArgs e)
	{
		Relay_OnUpdate_16();
	}

	private void Instance_OnSuspend_16(object o, EventArgs e)
	{
		Relay_OnSuspend_16();
	}

	private void Instance_OnResume_16(object o, EventArgs e)
	{
		Relay_OnResume_16();
	}

	private void SubGraph_SaveLoadBool_Save_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_TechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Save_Out_9();
	}

	private void SubGraph_SaveLoadBool_Load_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_TechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Load_Out_9();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_TechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Restart_Out_9();
	}

	private void SubGraph_SaveLoadBool_Save_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_AIMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Save_Out_11();
	}

	private void SubGraph_SaveLoadBool_Load_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_AIMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Load_Out_11();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_AIMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Restart_Out_11();
	}

	private void SubGraph_SaveLoadBool_Save_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_MsgFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Save_Out_12();
	}

	private void SubGraph_SaveLoadBool_Load_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_MsgFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Load_Out_12();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_MsgFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Restart_Out_12();
	}

	private void SubGraph_SaveLoadBool_Save_Out_64(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = e.boolean;
		local_TechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_64;
		Relay_Save_Out_64();
	}

	private void SubGraph_SaveLoadBool_Load_Out_64(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = e.boolean;
		local_TechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_64;
		Relay_Load_Out_64();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_64(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = e.boolean;
		local_TechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_64;
		Relay_Restart_Out_64();
	}

	private void Relay_InitialSpawn_3()
	{
		int num = 0;
		Array array = techData;
		if (logic_uScript_SpawnTechsFromData_spawnData_3.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_3, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_3, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_3 = owner_Connection_0;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_3.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_3, logic_uScript_SpawnTechsFromData_ownerNode_3, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_3);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_3.Out)
		{
			Relay_In_66();
		}
	}

	private void Relay_True_5()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_5.True(out logic_uScriptAct_SetBool_Target_5);
		local_TechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_5;
	}

	private void Relay_False_5()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_5.False(out logic_uScriptAct_SetBool_Target_5);
		local_TechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_5;
	}

	private void Relay_In_6()
	{
		int num = 0;
		Array array = techData;
		if (logic_uScript_GetAndCheckTechs_techData_6.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_6, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_6, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_6 = owner_Connection_7;
		int num2 = 0;
		Array array2 = local_Techs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_6.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_6, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_6, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_6 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.In(logic_uScript_GetAndCheckTechs_techData_6, logic_uScript_GetAndCheckTechs_ownerNode_6, ref logic_uScript_GetAndCheckTechs_techs_6);
		local_Techs_TankArray = logic_uScript_GetAndCheckTechs_techs_6;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_20();
		}
		if (someAlive)
		{
			Relay_AtIndex_20();
		}
	}

	private void Relay_Save_Out_9()
	{
		Relay_Save_64();
	}

	private void Relay_Load_Out_9()
	{
		Relay_Load_64();
	}

	private void Relay_Restart_Out_9()
	{
		Relay_Set_False_64();
	}

	private void Relay_Save_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Load_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Set_True_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Set_False_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Save_Out_11()
	{
		Relay_Save_12();
	}

	private void Relay_Load_Out_11()
	{
		Relay_Load_12();
	}

	private void Relay_Restart_Out_11()
	{
		Relay_Set_False_12();
	}

	private void Relay_Save_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_AIMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_AIMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Load_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_AIMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_AIMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Set_True_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_AIMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_AIMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Set_False_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_AIMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_AIMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Save_Out_12()
	{
	}

	private void Relay_Load_Out_12()
	{
	}

	private void Relay_Restart_Out_12()
	{
	}

	private void Relay_Save_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Load_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Set_True_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Set_False_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_SaveEvent_15()
	{
		Relay_Save_9();
	}

	private void Relay_LoadEvent_15()
	{
		Relay_Load_9();
	}

	private void Relay_RestartEvent_15()
	{
		Relay_Set_False_9();
	}

	private void Relay_OnUpdate_16()
	{
		Relay_In_36();
	}

	private void Relay_OnSuspend_16()
	{
	}

	private void Relay_OnResume_16()
	{
	}

	private void Relay_AtIndex_20()
	{
		int num = 0;
		Array array = local_Techs_TankArray;
		if (logic_uScript_AccessListTech_techList_20.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_20, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_20, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_20.AtIndex(ref logic_uScript_AccessListTech_techList_20, logic_uScript_AccessListTech_index_20, out logic_uScript_AccessListTech_value_20);
		local_Techs_TankArray = logic_uScript_AccessListTech_techList_20;
		local_Tech_Tank = logic_uScript_AccessListTech_value_20;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_20.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_22()
	{
		logic_uScript_SetEncounterTarget_owner_22 = owner_Connection_23;
		logic_uScript_SetEncounterTarget_visibleObject_22 = local_Tech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_22.In(logic_uScript_SetEncounterTarget_owner_22, logic_uScript_SetEncounterTarget_visibleObject_22);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_22.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_In_25()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_25 = owner_Connection_24;
		logic_uScript_MoveEncounterWithVisible_visibleObject_25 = local_Tech_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_25.In(logic_uScript_MoveEncounterWithVisible_ownerNode_25, logic_uScript_MoveEncounterWithVisible_visibleObject_25);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_25.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_29()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_29 = local_Tech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_29 = distAtWhichTechFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_29.In(logic_uScript_IsPlayerInRangeOfTech_tech_29, logic_uScript_IsPlayerInRangeOfTech_range_29, logic_uScript_IsPlayerInRangeOfTech_techs_29);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_29.InRange)
		{
			Relay_In_33();
		}
	}

	private void Relay_In_33()
	{
		logic_uScriptCon_CompareBool_Bool_33 = local_MsgFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.In(logic_uScriptCon_CompareBool_Bool_33);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.False)
		{
			Relay_In_71();
		}
	}

	private void Relay_True_34()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_34.True(out logic_uScriptAct_SetBool_Target_34);
		local_MsgFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_34;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_34.Out)
		{
			Relay_True_56();
		}
	}

	private void Relay_False_34()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_34.False(out logic_uScriptAct_SetBool_Target_34);
		local_MsgFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_34;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_34.Out)
		{
			Relay_True_56();
		}
	}

	private void Relay_In_36()
	{
		logic_uScriptCon_CompareBool_Bool_36 = local_TechSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.In(logic_uScriptCon_CompareBool_Bool_36);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.False;
		if (num)
		{
			Relay_In_6();
		}
		if (flag)
		{
			Relay_InitialSpawn_3();
		}
	}

	private void Relay_In_38()
	{
		logic_uScript_SetTankTeam_tank_38 = local_Tech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_38.In(logic_uScript_SetTankTeam_tank_38, logic_uScript_SetTankTeam_team_38);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_38.Out)
		{
			Relay_True_34();
		}
	}

	private void Relay_In_41()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_41 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_41.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_41, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_41);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_41.Out)
		{
			Relay_In_77();
		}
	}

	private void Relay_In_42()
	{
		logic_uScript_CheckFTUEAction_enumType_42 = FTUEAIMenuOpened;
		logic_uScript_CheckFTUEAction_uScript_CheckFTUEAction_42.In(logic_uScript_CheckFTUEAction_enumType_42);
		bool num = logic_uScript_CheckFTUEAction_uScript_CheckFTUEAction_42.True;
		bool flag = logic_uScript_CheckFTUEAction_uScript_CheckFTUEAction_42.False;
		if (num)
		{
			Relay_True_44();
		}
		if (flag)
		{
			Relay_False_44();
		}
	}

	private void Relay_True_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.True(out logic_uScriptAct_SetBool_Target_44);
		local_AIMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_False_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.False(out logic_uScriptAct_SetBool_Target_44);
		local_AIMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_48()
	{
		logic_uScriptCon_CompareBool_Bool_48 = local_AIMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.In(logic_uScriptCon_CompareBool_Bool_48);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.False;
		if (num)
		{
			Relay_In_41();
		}
		if (flag)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_SetTankInvulnerable_tank_50 = local_Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_50.In(logic_uScript_SetTankInvulnerable_invulnerable_50, logic_uScript_SetTankInvulnerable_tank_50);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_50.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_52()
	{
		logic_uScript_SetTankInvulnerable_tank_52 = local_Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_52.In(logic_uScript_SetTankInvulnerable_invulnerable_52, logic_uScript_SetTankInvulnerable_tank_52);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_52.Out)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_54()
	{
		logic_uScriptCon_CompareBool_Bool_54 = local_TechFound_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.In(logic_uScriptCon_CompareBool_Bool_54);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.False;
		if (num)
		{
			Relay_In_48();
		}
		if (flag)
		{
			Relay_In_29();
		}
	}

	private void Relay_True_56()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_56.True(out logic_uScriptAct_SetBool_Target_56);
		local_TechFound_System_Boolean = logic_uScriptAct_SetBool_Target_56;
	}

	private void Relay_False_56()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_56.False(out logic_uScriptAct_SetBool_Target_56);
		local_TechFound_System_Boolean = logic_uScriptAct_SetBool_Target_56;
	}

	private void Relay_In_60()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_60 = local_Tech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_60 = distAtWhichTechFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_60.In(logic_uScript_IsPlayerInRangeOfTech_tech_60, logic_uScript_IsPlayerInRangeOfTech_range_60, logic_uScript_IsPlayerInRangeOfTech_techs_60);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_60.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_60.OutOfRange;
		if (inRange)
		{
			Relay_In_73();
		}
		if (outOfRange)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_61()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_61 = local_Msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_61.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_61, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_61);
	}

	private void Relay_Save_Out_64()
	{
		Relay_Save_11();
	}

	private void Relay_Load_Out_64()
	{
		Relay_Load_11();
	}

	private void Relay_Restart_Out_64()
	{
		Relay_Set_False_11();
	}

	private void Relay_Save_64()
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = local_TechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_64 = local_TechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Save(ref logic_SubGraph_SaveLoadBool_boolean_64, logic_SubGraph_SaveLoadBool_boolAsVariable_64, logic_SubGraph_SaveLoadBool_uniqueID_64);
	}

	private void Relay_Load_64()
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = local_TechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_64 = local_TechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Load(ref logic_SubGraph_SaveLoadBool_boolean_64, logic_SubGraph_SaveLoadBool_boolAsVariable_64, logic_SubGraph_SaveLoadBool_uniqueID_64);
	}

	private void Relay_Set_True_64()
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = local_TechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_64 = local_TechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_64, logic_SubGraph_SaveLoadBool_boolAsVariable_64, logic_SubGraph_SaveLoadBool_uniqueID_64);
	}

	private void Relay_Set_False_64()
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = local_TechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_64 = local_TechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_64, logic_SubGraph_SaveLoadBool_boolAsVariable_64, logic_SubGraph_SaveLoadBool_uniqueID_64);
	}

	private void Relay_Succeed_65()
	{
		logic_uScript_FinishEncounter_owner_65 = owner_Connection_37;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_65.Succeed(logic_uScript_FinishEncounter_owner_65);
	}

	private void Relay_Fail_65()
	{
		logic_uScript_FinishEncounter_owner_65 = owner_Connection_37;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_65.Fail(logic_uScript_FinishEncounter_owner_65);
	}

	private void Relay_In_66()
	{
		logic_uScript_AddMessage_messageData_66 = msg01FindTech;
		logic_uScript_AddMessage_speaker_66 = messageSpeaker;
		logic_uScript_AddMessage_Return_66 = logic_uScript_AddMessage_uScript_AddMessage_66.In(logic_uScript_AddMessage_messageData_66, logic_uScript_AddMessage_speaker_66);
		if (logic_uScript_AddMessage_uScript_AddMessage_66.Out)
		{
			Relay_True_5();
		}
	}

	private void Relay_In_71()
	{
		logic_uScript_AddMessage_messageData_71 = msg02TechFound;
		logic_uScript_AddMessage_speaker_71 = messageSpeaker;
		logic_uScript_AddMessage_Return_71 = logic_uScript_AddMessage_uScript_AddMessage_71.In(logic_uScript_AddMessage_messageData_71, logic_uScript_AddMessage_speaker_71);
		if (logic_uScript_AddMessage_uScript_AddMessage_71.Shown)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_AddMessage_messageData_73 = msg03AccessMenu;
		logic_uScript_AddMessage_speaker_73 = messageSpeaker;
		logic_uScript_AddMessage_Return_73 = logic_uScript_AddMessage_uScript_AddMessage_73.In(logic_uScript_AddMessage_messageData_73, logic_uScript_AddMessage_speaker_73);
	}

	private void Relay_In_77()
	{
		logic_uScript_AddMessage_messageData_77 = msg04TutorialComplete;
		logic_uScript_AddMessage_speaker_77 = messageSpeaker;
		logic_uScript_AddMessage_Return_77 = logic_uScript_AddMessage_uScript_AddMessage_77.In(logic_uScript_AddMessage_messageData_77, logic_uScript_AddMessage_speaker_77);
		if (logic_uScript_AddMessage_uScript_AddMessage_77.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_78()
	{
		logic_uScript_DiscoverBlock_uScript_DiscoverBlock_78.In(logic_uScript_DiscoverBlock_blockType_78);
		if (logic_uScript_DiscoverBlock_uScript_DiscoverBlock_78.Out)
		{
			Relay_Succeed_65();
		}
	}
}
