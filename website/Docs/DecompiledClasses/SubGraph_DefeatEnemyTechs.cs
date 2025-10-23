using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_DefeatEnemyTechs", "")]
public class SubGraph_DefeatEnemyTechs : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_19 = new SpawnTechData[0];

	private float external_22;

	private string external_51 = "";

	private float external_52;

	private LocalisedString[] external_13 = new LocalisedString[0];

	private LocalisedString[] external_11 = new LocalisedString[0];

	private ManOnScreenMessages.Speaker external_48;

	private Tank local_35_Tank;

	private bool local_EnemySpawned_System_Boolean;

	private bool local_EnemySpotted_System_Boolean;

	private Tank[] local_EnemyTechs_TankArray = new Tank[0];

	private bool local_ObjectiveComplete_System_Boolean;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_23;

	private GameObject owner_Connection_33;

	private GameObject owner_Connection_37;

	private GameObject owner_Connection_47;

	private GameObject owner_Connection_53;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_3;

	private bool logic_uScriptCon_CompareBool_True_3 = true;

	private bool logic_uScriptCon_CompareBool_False_3 = true;

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

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_12;

	private bool logic_uScriptCon_CompareBool_True_12 = true;

	private bool logic_uScriptCon_CompareBool_False_12 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_14 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_14;

	private bool logic_uScriptAct_SetBool_Out_14 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_14 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_14 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_15 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_15 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_15;

	private string logic_uScript_AddOnScreenMessage_tag_15 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_15;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_15;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_15;

	private bool logic_uScript_AddOnScreenMessage_Out_15 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_15 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_18 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_18;

	private bool logic_uScriptAct_SetBool_Out_18 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_18 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_18 = true;

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_20 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_20 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_20;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_20 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_20 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_20 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_26 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_26;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_26 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_26;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_26 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_27 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_27 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_27;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_27 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_27 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_29;

	private bool logic_uScriptCon_CompareBool_True_29 = true;

	private bool logic_uScriptCon_CompareBool_False_29 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_30 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_30;

	private bool logic_uScriptAct_SetBool_Out_30 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_30 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_30 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_31 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_31 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_31;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_31 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_31;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_31 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_31 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_31 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_31 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_36 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_36;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_36 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_36 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_38;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_38 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_38 = "EnemySpotted_DefeatEnemyTechs";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_41;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_41 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_41 = "ObjectiveComplete_DefeatEnemyTechs";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_42;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_42 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_42 = "EnemySpawned_DefeatEnemyTechs";

	private uScript_IsOnScreenMessageStringValid logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_44 = new uScript_IsOnScreenMessageStringValid();

	private LocalisedString[] logic_uScript_IsOnScreenMessageStringValid_locString_44 = new LocalisedString[0];

	private bool logic_uScript_IsOnScreenMessageStringValid_True_44 = true;

	private bool logic_uScript_IsOnScreenMessageStringValid_False_44 = true;

	private uScript_IsOnScreenMessageStringValid logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_45 = new uScript_IsOnScreenMessageStringValid();

	private LocalisedString[] logic_uScript_IsOnScreenMessageStringValid_locString_45 = new LocalisedString[0];

	private bool logic_uScript_IsOnScreenMessageStringValid_True_45 = true;

	private bool logic_uScript_IsOnScreenMessageStringValid_False_45 = true;

	private uScript_ShowQuestLog logic_uScript_ShowQuestLog_uScript_ShowQuestLog_46 = new uScript_ShowQuestLog();

	private GameObject logic_uScript_ShowQuestLog_owner_46;

	private bool logic_uScript_ShowQuestLog_Out_46 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_50 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_50;

	private string logic_uScript_RemoveScenery_positionName_50 = "";

	private float logic_uScript_RemoveScenery_radius_50;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_50;

	private bool logic_uScript_RemoveScenery_Out_50 = true;

	[FriendlyName("Complete")]
	public event uScriptEventHandler Complete;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
		}
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
			if (null != owner_Connection_8)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_7;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_7;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_7;
				}
			}
		}
		if (null == owner_Connection_23 || !m_RegisteredForEvents)
		{
			owner_Connection_23 = parentGameObject;
		}
		if (null == owner_Connection_33 || !m_RegisteredForEvents)
		{
			owner_Connection_33 = parentGameObject;
		}
		if (null == owner_Connection_37 || !m_RegisteredForEvents)
		{
			owner_Connection_37 = parentGameObject;
		}
		if (null == owner_Connection_47 || !m_RegisteredForEvents)
		{
			owner_Connection_47 = parentGameObject;
		}
		if (null == owner_Connection_53 || !m_RegisteredForEvents)
		{
			owner_Connection_53 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_8)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_7;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_7;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_7;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_8)
		{
			uScript_SaveLoad component = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_7;
				component.LoadEvent -= Instance_LoadEvent_7;
				component.RestartEvent -= Instance_RestartEvent_7;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_10.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_14.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_18.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_20.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_26.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_27.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_30.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_31.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_36.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.SetParent(g);
		logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_44.SetParent(g);
		logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_45.SetParent(g);
		logic_uScript_ShowQuestLog_uScript_ShowQuestLog_46.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_50.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_23 = parentGameObject;
		owner_Connection_33 = parentGameObject;
		owner_Connection_37 = parentGameObject;
		owner_Connection_47 = parentGameObject;
		owner_Connection_53 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save_Out += SubGraph_SaveLoadBool_Save_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load_Out += SubGraph_SaveLoadBool_Load_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Save_Out += SubGraph_SaveLoadBool_Save_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Load_Out += SubGraph_SaveLoadBool_Load_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Save_Out += SubGraph_SaveLoadBool_Save_Out_42;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Load_Out += SubGraph_SaveLoadBool_Load_Out_42;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_42;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_10.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_20.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_26.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save_Out -= SubGraph_SaveLoadBool_Save_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load_Out -= SubGraph_SaveLoadBool_Load_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Save_Out -= SubGraph_SaveLoadBool_Save_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Load_Out -= SubGraph_SaveLoadBool_Load_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Save_Out -= SubGraph_SaveLoadBool_Save_Out_42;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Load_Out -= SubGraph_SaveLoadBool_Load_Out_42;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_42;
	}

	private void Instance_SaveEvent_7(object o, EventArgs e)
	{
		Relay_SaveEvent_7();
	}

	private void Instance_LoadEvent_7(object o, EventArgs e)
	{
		Relay_LoadEvent_7();
	}

	private void Instance_RestartEvent_7(object o, EventArgs e)
	{
		Relay_RestartEvent_7();
	}

	private void SubGraph_SaveLoadBool_Save_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_EnemySpotted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Save_Out_38();
	}

	private void SubGraph_SaveLoadBool_Load_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_EnemySpotted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Load_Out_38();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_EnemySpotted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Restart_Out_38();
	}

	private void SubGraph_SaveLoadBool_Save_Out_41(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_41 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_41;
		Relay_Save_Out_41();
	}

	private void SubGraph_SaveLoadBool_Load_Out_41(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_41 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_41;
		Relay_Load_Out_41();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_41(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_41 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_41;
		Relay_Restart_Out_41();
	}

	private void SubGraph_SaveLoadBool_Save_Out_42(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_42;
		Relay_Save_Out_42();
	}

	private void SubGraph_SaveLoadBool_Load_Out_42(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_42;
		Relay_Load_Out_42();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_42(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_42;
		Relay_Restart_Out_42();
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("enemyTechData", "")] SpawnTechData[] enemyTechData, [FriendlyName("distEnemiesSpotted", "")] float distEnemiesSpotted, [FriendlyName("clearSceneryPos", "")] string clearSceneryPos, [FriendlyName("clearSceneryRadius", "")] float clearSceneryRadius, [FriendlyName("msgEnemySpotted", "")] LocalisedString[] msgEnemySpotted, [FriendlyName("msgComplete", "")] LocalisedString[] msgComplete, [FriendlyName("messageSpeaker", "")] ManOnScreenMessages.Speaker messageSpeaker)
	{
		external_19 = enemyTechData;
		external_22 = distEnemiesSpotted;
		external_51 = clearSceneryPos;
		external_52 = clearSceneryRadius;
		external_13 = msgEnemySpotted;
		external_11 = msgComplete;
		external_48 = messageSpeaker;
		Relay_In_3();
	}

	private void Relay_Connection_1()
	{
	}

	private void Relay_Connection_2()
	{
		if (this.Complete != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Complete(this, args);
		}
	}

	private void Relay_In_3()
	{
		logic_uScriptCon_CompareBool_Bool_3 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3.In(logic_uScriptCon_CompareBool_Bool_3);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3.False;
		if (num)
		{
			Relay_Connection_2();
		}
		if (flag)
		{
			Relay_In_29();
		}
	}

	private void Relay_SaveEvent_7()
	{
		Relay_Save_38();
	}

	private void Relay_LoadEvent_7()
	{
		Relay_Load_38();
	}

	private void Relay_RestartEvent_7()
	{
		Relay_Set_False_38();
	}

	private void Relay_In_10()
	{
		int num = 0;
		Array array = external_11;
		if (logic_uScript_AddOnScreenMessage_locString_10.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_10, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_10, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_10 = external_48;
		logic_uScript_AddOnScreenMessage_Return_10 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_10.In(logic_uScript_AddOnScreenMessage_locString_10, logic_uScript_AddOnScreenMessage_msgPriority_10, logic_uScript_AddOnScreenMessage_holdMsg_10, logic_uScript_AddOnScreenMessage_tag_10, logic_uScript_AddOnScreenMessage_speaker_10, logic_uScript_AddOnScreenMessage_side_10);
	}

	private void Relay_Connection_11()
	{
	}

	private void Relay_In_12()
	{
		logic_uScriptCon_CompareBool_Bool_12 = local_EnemySpotted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.In(logic_uScriptCon_CompareBool_Bool_12);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.False)
		{
			Relay_True_14();
		}
	}

	private void Relay_Connection_13()
	{
	}

	private void Relay_True_14()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_14.True(out logic_uScriptAct_SetBool_Target_14);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_14;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_14.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_False_14()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_14.False(out logic_uScriptAct_SetBool_Target_14);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_14;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_14.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_15()
	{
		int num = 0;
		Array array = external_13;
		if (logic_uScript_AddOnScreenMessage_locString_15.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_15, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_15, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_15 = external_48;
		logic_uScript_AddOnScreenMessage_Return_15 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15.In(logic_uScript_AddOnScreenMessage_locString_15, logic_uScript_AddOnScreenMessage_msgPriority_15, logic_uScript_AddOnScreenMessage_holdMsg_15, logic_uScript_AddOnScreenMessage_tag_15, logic_uScript_AddOnScreenMessage_speaker_15, logic_uScript_AddOnScreenMessage_side_15);
	}

	private void Relay_True_18()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_18.True(out logic_uScriptAct_SetBool_Target_18);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_18;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_18.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_False_18()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_18.False(out logic_uScriptAct_SetBool_Target_18);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_18;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_18.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_Connection_19()
	{
	}

	private void Relay_In_20()
	{
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_20.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_20, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_20, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_range_20 = external_22;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_20.In(logic_uScript_InRangeOfAtLeastOneTech_techs_20, logic_uScript_InRangeOfAtLeastOneTech_range_20);
		if (logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_20.InRange)
		{
			Relay_In_12();
		}
	}

	private void Relay_Connection_22()
	{
	}

	private void Relay_In_26()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_26 = owner_Connection_23;
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_26.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_26, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_26, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_26 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_26.In(logic_uScript_SetOneTechAsEncounterTarget_owner_26, logic_uScript_SetOneTechAsEncounterTarget_techs_26);
		local_35_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_26;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_26.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_InitialSpawn_27()
	{
		int num = 0;
		Array array = external_19;
		if (logic_uScript_SpawnTechsFromData_spawnData_27.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_27, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_27, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_27 = owner_Connection_0;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_27.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_27, logic_uScript_SpawnTechsFromData_ownerNode_27, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_27);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_27.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_29()
	{
		logic_uScriptCon_CompareBool_Bool_29 = local_EnemySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.In(logic_uScriptCon_CompareBool_Bool_29);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.False;
		if (num)
		{
			Relay_In_31();
		}
		if (flag)
		{
			Relay_InitialSpawn_27();
		}
	}

	private void Relay_True_30()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_30.True(out logic_uScriptAct_SetBool_Target_30);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_30;
	}

	private void Relay_False_30()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_30.False(out logic_uScriptAct_SetBool_Target_30);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_30;
	}

	private void Relay_In_31()
	{
		int num = 0;
		Array array = external_19;
		if (logic_uScript_GetAndCheckTechs_techData_31.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_31, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_31, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_31 = owner_Connection_33;
		int num2 = 0;
		Array array2 = local_EnemyTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_31.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_31, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_31, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_31 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_31.In(logic_uScript_GetAndCheckTechs_techData_31, logic_uScript_GetAndCheckTechs_ownerNode_31, ref logic_uScript_GetAndCheckTechs_techs_31);
		local_EnemyTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_31;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_31.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_31.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_31.AllDead;
		if (allAlive)
		{
			Relay_In_26();
		}
		if (someAlive)
		{
			Relay_In_26();
		}
		if (allDead)
		{
			Relay_True_18();
		}
	}

	private void Relay_In_36()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_36 = owner_Connection_37;
		logic_uScript_MoveEncounterWithVisible_visibleObject_36 = local_35_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_36.In(logic_uScript_MoveEncounterWithVisible_ownerNode_36, logic_uScript_MoveEncounterWithVisible_visibleObject_36);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_36.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_Save_Out_38()
	{
		Relay_Save_41();
	}

	private void Relay_Load_Out_38()
	{
		Relay_Load_41();
	}

	private void Relay_Restart_Out_38()
	{
		Relay_Set_False_41();
	}

	private void Relay_Save_38()
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
	}

	private void Relay_Load_38()
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
	}

	private void Relay_Set_True_38()
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
	}

	private void Relay_Set_False_38()
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_EnemySpotted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
	}

	private void Relay_Save_Out_41()
	{
		Relay_Save_42();
	}

	private void Relay_Load_Out_41()
	{
		Relay_Load_42();
	}

	private void Relay_Restart_Out_41()
	{
		Relay_Set_False_42();
	}

	private void Relay_Save_41()
	{
		logic_SubGraph_SaveLoadBool_boolean_41 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_41 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Save(ref logic_SubGraph_SaveLoadBool_boolean_41, logic_SubGraph_SaveLoadBool_boolAsVariable_41, logic_SubGraph_SaveLoadBool_uniqueID_41);
	}

	private void Relay_Load_41()
	{
		logic_SubGraph_SaveLoadBool_boolean_41 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_41 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Load(ref logic_SubGraph_SaveLoadBool_boolean_41, logic_SubGraph_SaveLoadBool_boolAsVariable_41, logic_SubGraph_SaveLoadBool_uniqueID_41);
	}

	private void Relay_Set_True_41()
	{
		logic_SubGraph_SaveLoadBool_boolean_41 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_41 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_41, logic_SubGraph_SaveLoadBool_boolAsVariable_41, logic_SubGraph_SaveLoadBool_uniqueID_41);
	}

	private void Relay_Set_False_41()
	{
		logic_SubGraph_SaveLoadBool_boolean_41 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_41 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_41, logic_SubGraph_SaveLoadBool_boolAsVariable_41, logic_SubGraph_SaveLoadBool_uniqueID_41);
	}

	private void Relay_Save_Out_42()
	{
	}

	private void Relay_Load_Out_42()
	{
	}

	private void Relay_Restart_Out_42()
	{
	}

	private void Relay_Save_42()
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_42 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Save(ref logic_SubGraph_SaveLoadBool_boolean_42, logic_SubGraph_SaveLoadBool_boolAsVariable_42, logic_SubGraph_SaveLoadBool_uniqueID_42);
	}

	private void Relay_Load_42()
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_42 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Load(ref logic_SubGraph_SaveLoadBool_boolean_42, logic_SubGraph_SaveLoadBool_boolAsVariable_42, logic_SubGraph_SaveLoadBool_uniqueID_42);
	}

	private void Relay_Set_True_42()
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_42 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_42, logic_SubGraph_SaveLoadBool_boolAsVariable_42, logic_SubGraph_SaveLoadBool_uniqueID_42);
	}

	private void Relay_Set_False_42()
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_42 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_42, logic_SubGraph_SaveLoadBool_boolAsVariable_42, logic_SubGraph_SaveLoadBool_uniqueID_42);
	}

	private void Relay_In_44()
	{
		int num = 0;
		Array array = external_11;
		if (logic_uScript_IsOnScreenMessageStringValid_locString_44.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_IsOnScreenMessageStringValid_locString_44, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_IsOnScreenMessageStringValid_locString_44, num, array.Length);
		num += array.Length;
		logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_44.In(logic_uScript_IsOnScreenMessageStringValid_locString_44);
		if (logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_44.True)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_45()
	{
		int num = 0;
		Array array = external_13;
		if (logic_uScript_IsOnScreenMessageStringValid_locString_45.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_IsOnScreenMessageStringValid_locString_45, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_IsOnScreenMessageStringValid_locString_45, num, array.Length);
		num += array.Length;
		logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_45.In(logic_uScript_IsOnScreenMessageStringValid_locString_45);
		if (logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_45.True)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_46()
	{
		logic_uScript_ShowQuestLog_owner_46 = owner_Connection_47;
		logic_uScript_ShowQuestLog_uScript_ShowQuestLog_46.In(logic_uScript_ShowQuestLog_owner_46);
		if (logic_uScript_ShowQuestLog_uScript_ShowQuestLog_46.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_Connection_48()
	{
	}

	private void Relay_In_50()
	{
		logic_uScript_RemoveScenery_ownerNode_50 = owner_Connection_53;
		logic_uScript_RemoveScenery_positionName_50 = external_51;
		logic_uScript_RemoveScenery_radius_50 = external_52;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_50.In(logic_uScript_RemoveScenery_ownerNode_50, logic_uScript_RemoveScenery_positionName_50, logic_uScript_RemoveScenery_radius_50, logic_uScript_RemoveScenery_preventChunksSpawning_50);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_50.Out)
		{
			Relay_True_30();
		}
	}

	private void Relay_Connection_51()
	{
	}

	private void Relay_Connection_52()
	{
	}
}
