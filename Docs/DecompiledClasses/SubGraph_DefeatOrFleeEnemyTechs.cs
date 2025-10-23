using System;
using UnityEngine;

[Serializable]
[FriendlyName("DefeatOrFleeEnemyTechs", "")]
[NodePath("Graphs")]
public class SubGraph_DefeatOrFleeEnemyTechs : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_20 = new SpawnTechData[0];

	private float external_23;

	private LocalisedString[] external_16 = new LocalisedString[0];

	private LocalisedString[] external_28 = new LocalisedString[0];

	private LocalisedString[] external_14 = new LocalisedString[0];

	private Tank local_66_Tank;

	private bool local_EnemiesDefeated_System_Boolean;

	private string local_EnemiesDefeatedID_System_String = "EnemiesDefeated";

	private bool local_EnemySpawned_System_Boolean;

	private bool local_EnemySpotted_System_Boolean;

	private string local_EnemySpottedID_System_String = "EnemySpotted";

	private Tank[] local_EnemyTechs_TankArray = new Tank[0];

	private bool local_PlayerEscaped_System_Boolean;

	private string local_PlayerEscapedID_System_String = "PlayerEscaped";

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_24;

	private GameObject owner_Connection_33;

	private GameObject owner_Connection_34;

	private GameObject owner_Connection_53;

	private GameObject owner_Connection_60;

	private GameObject owner_Connection_64;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_7 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_7 = "";

	private GameObject logic_uScript_SaveVariable_owner_7;

	private string logic_uScript_SaveVariable_uniqueId_7 = "";

	private bool logic_uScript_SaveVariable_Out_7 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_8 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_8;

	private GameObject logic_uScript_LoadBool_owner_8;

	private string logic_uScript_LoadBool_uniqueName_8 = "";

	private bool logic_uScript_LoadBool_Out_8 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_11 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_11;

	private bool logic_uScriptAct_SetBool_Out_11 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_11 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_11 = true;

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

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_15;

	private bool logic_uScriptCon_CompareBool_True_15 = true;

	private bool logic_uScriptCon_CompareBool_False_15 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_17 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_17;

	private bool logic_uScriptAct_SetBool_Out_17 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_17 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_17 = true;

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

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_21 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_21 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_21;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_21 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_21 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_21 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_27 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_27;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_27 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_27;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_27 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_29 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_29 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_29 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_29;

	private string logic_uScript_AddOnScreenMessage_tag_29 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_29;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_29;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_29;

	private bool logic_uScript_AddOnScreenMessage_Out_29 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_29 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_35 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_35 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_35;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_35 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_35 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_38 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_38;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_38 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_38;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_38 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_38 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_38 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_38 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_39;

	private bool logic_uScriptCon_CompareBool_True_39 = true;

	private bool logic_uScriptCon_CompareBool_False_39 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_41;

	private bool logic_uScriptCon_CompareBool_True_41 = true;

	private bool logic_uScriptCon_CompareBool_False_41 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_42 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_42;

	private bool logic_uScriptAct_SetBool_Out_42 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_42 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_42 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_44;

	private bool logic_uScriptCon_CompareBool_True_44 = true;

	private bool logic_uScriptCon_CompareBool_False_44 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_46 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_46;

	private bool logic_uScriptAct_SetBool_Out_46 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_46 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_46 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_49 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_49;

	private bool logic_uScriptAct_SetBool_Out_49 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_49 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_49 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_50 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_50;

	private bool logic_uScriptAct_SetBool_Out_50 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_50 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_50 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_55 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_55 = "";

	private GameObject logic_uScript_SaveVariable_owner_55;

	private string logic_uScript_SaveVariable_uniqueId_55 = "";

	private bool logic_uScript_SaveVariable_Out_55 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_56 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_56;

	private GameObject logic_uScript_LoadBool_owner_56;

	private string logic_uScript_LoadBool_uniqueName_56 = "";

	private bool logic_uScript_LoadBool_Out_56 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_57 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_57;

	private bool logic_uScriptAct_SetBool_Out_57 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_57 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_57 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_62 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_62 = "";

	private GameObject logic_uScript_SaveVariable_owner_62;

	private string logic_uScript_SaveVariable_uniqueId_62 = "";

	private bool logic_uScript_SaveVariable_Out_62 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_63 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_63;

	private GameObject logic_uScript_LoadBool_owner_63;

	private string logic_uScript_LoadBool_uniqueName_63 = "";

	private bool logic_uScript_LoadBool_Out_63 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_65 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_65;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_65 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_65 = true;

	[FriendlyName("FledComplete")]
	public event uScriptEventHandler FledComplete;

	[FriendlyName("DeadComplete")]
	public event uScriptEventHandler DeadComplete;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
			if (null != owner_Connection_6)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_6.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_6.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_5;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_5;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_5;
				}
			}
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_24 || !m_RegisteredForEvents)
		{
			owner_Connection_24 = parentGameObject;
		}
		if (null == owner_Connection_33 || !m_RegisteredForEvents)
		{
			owner_Connection_33 = parentGameObject;
		}
		if (null == owner_Connection_34 || !m_RegisteredForEvents)
		{
			owner_Connection_34 = parentGameObject;
		}
		if (null == owner_Connection_53 || !m_RegisteredForEvents)
		{
			owner_Connection_53 = parentGameObject;
		}
		if (null == owner_Connection_60 || !m_RegisteredForEvents)
		{
			owner_Connection_60 = parentGameObject;
		}
		if (null == owner_Connection_64 || !m_RegisteredForEvents)
		{
			owner_Connection_64 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_6)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_6.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_6.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_5;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_5;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_5;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_6)
		{
			uScript_SaveLoad component = owner_Connection_6.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_5;
				component.LoadEvent -= Instance_LoadEvent_5;
				component.RestartEvent -= Instance_RestartEvent_5;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SaveVariable_uScript_SaveVariable_7.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_8.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_11.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_21.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_27.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_29.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_35.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_42.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_46.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_55.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_56.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_62.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_63.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_65.SetParent(g);
		owner_Connection_6 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_24 = parentGameObject;
		owner_Connection_33 = parentGameObject;
		owner_Connection_34 = parentGameObject;
		owner_Connection_53 = parentGameObject;
		owner_Connection_60 = parentGameObject;
		owner_Connection_64 = parentGameObject;
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
		logic_uScript_LoadBool_uScript_LoadBool_8.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_21.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_27.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_29.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_56.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_63.OnDisable();
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

	private void Instance_SaveEvent_5(object o, EventArgs e)
	{
		Relay_SaveEvent_5();
	}

	private void Instance_LoadEvent_5(object o, EventArgs e)
	{
		Relay_LoadEvent_5();
	}

	private void Instance_RestartEvent_5(object o, EventArgs e)
	{
		Relay_RestartEvent_5();
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("enemyTechData", "")] SpawnTechData[] enemyTechData, [FriendlyName("distEnemiesRange", "")] float distEnemiesRange, [FriendlyName("msgEnemySpotted", "")] LocalisedString[] msgEnemySpotted, [FriendlyName("msgFledComplete", "")] LocalisedString[] msgFledComplete, [FriendlyName("msgAllDeadComplete", "")] LocalisedString[] msgAllDeadComplete)
	{
		external_20 = enemyTechData;
		external_23 = distEnemiesRange;
		external_16 = msgEnemySpotted;
		external_28 = msgFledComplete;
		external_14 = msgAllDeadComplete;
		Relay_In_41();
	}

	private void Relay_Connection_0()
	{
	}

	private void Relay_Connection_1()
	{
		if (this.DeadComplete != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.DeadComplete(this, args);
		}
	}

	private void Relay_SaveEvent_5()
	{
		Relay_In_7();
	}

	private void Relay_LoadEvent_5()
	{
		Relay_In_8();
	}

	private void Relay_RestartEvent_5()
	{
		Relay_False_11();
	}

	private void Relay_In_7()
	{
		logic_uScript_SaveVariable_variable_7 = local_EnemiesDefeated_System_Boolean;
		logic_uScript_SaveVariable_owner_7 = owner_Connection_9;
		logic_uScript_SaveVariable_uniqueId_7 = local_EnemiesDefeatedID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_7.In(logic_uScript_SaveVariable_variable_7, logic_uScript_SaveVariable_owner_7, logic_uScript_SaveVariable_uniqueId_7);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_7.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_8()
	{
		logic_uScript_LoadBool_data_8 = local_EnemiesDefeated_System_Boolean;
		logic_uScript_LoadBool_owner_8 = owner_Connection_9;
		logic_uScript_LoadBool_uniqueName_8 = local_EnemiesDefeatedID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_8.In(ref logic_uScript_LoadBool_data_8, logic_uScript_LoadBool_owner_8, logic_uScript_LoadBool_uniqueName_8);
		local_EnemiesDefeated_System_Boolean = logic_uScript_LoadBool_data_8;
		if (logic_uScript_LoadBool_uScript_LoadBool_8.Out)
		{
			Relay_In_56();
		}
	}

	private void Relay_True_11()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_11.True(out logic_uScriptAct_SetBool_Target_11);
		local_EnemiesDefeated_System_Boolean = logic_uScriptAct_SetBool_Target_11;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_11.Out)
		{
			Relay_False_50();
		}
	}

	private void Relay_False_11()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_11.False(out logic_uScriptAct_SetBool_Target_11);
		local_EnemiesDefeated_System_Boolean = logic_uScriptAct_SetBool_Target_11;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_11.Out)
		{
			Relay_False_50();
		}
	}

	private void Relay_In_13()
	{
		int num = 0;
		Array array = external_14;
		if (logic_uScript_AddOnScreenMessage_locString_13.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_13, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_13, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_13 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13.In(logic_uScript_AddOnScreenMessage_locString_13, logic_uScript_AddOnScreenMessage_msgPriority_13, logic_uScript_AddOnScreenMessage_holdMsg_13, logic_uScript_AddOnScreenMessage_tag_13, logic_uScript_AddOnScreenMessage_speaker_13, logic_uScript_AddOnScreenMessage_side_13);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13.Out)
		{
			Relay_True_46();
		}
	}

	private void Relay_Connection_14()
	{
	}

	private void Relay_In_15()
	{
		logic_uScriptCon_CompareBool_Bool_15 = local_EnemySpotted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.In(logic_uScriptCon_CompareBool_Bool_15);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.False)
		{
			Relay_True_17();
		}
	}

	private void Relay_Connection_16()
	{
	}

	private void Relay_True_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.True(out logic_uScriptAct_SetBool_Target_17);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_False_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.False(out logic_uScriptAct_SetBool_Target_17);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_18()
	{
		int num = 0;
		Array array = external_16;
		if (logic_uScript_AddOnScreenMessage_locString_18.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_18, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_18, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_18 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.In(logic_uScript_AddOnScreenMessage_locString_18, logic_uScript_AddOnScreenMessage_msgPriority_18, logic_uScript_AddOnScreenMessage_holdMsg_18, logic_uScript_AddOnScreenMessage_tag_18, logic_uScript_AddOnScreenMessage_speaker_18, logic_uScript_AddOnScreenMessage_side_18);
	}

	private void Relay_Connection_20()
	{
	}

	private void Relay_In_21()
	{
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_21.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_21, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_21, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_range_21 = external_23;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_21.In(logic_uScript_InRangeOfAtLeastOneTech_techs_21, logic_uScript_InRangeOfAtLeastOneTech_range_21);
		bool inRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_21.InRange;
		bool outOfRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_21.OutOfRange;
		if (inRange)
		{
			Relay_In_15();
		}
		if (outOfRange)
		{
			Relay_In_29();
		}
	}

	private void Relay_Connection_23()
	{
	}

	private void Relay_In_27()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_27 = owner_Connection_24;
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_27.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_27, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_27, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_27 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_27.In(logic_uScript_SetOneTechAsEncounterTarget_owner_27, logic_uScript_SetOneTechAsEncounterTarget_techs_27);
		local_66_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_27;
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_27.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_Connection_28()
	{
	}

	private void Relay_In_29()
	{
		int num = 0;
		Array array = external_28;
		if (logic_uScript_AddOnScreenMessage_locString_29.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_29, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_29, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_29 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_29.In(logic_uScript_AddOnScreenMessage_locString_29, logic_uScript_AddOnScreenMessage_msgPriority_29, logic_uScript_AddOnScreenMessage_holdMsg_29, logic_uScript_AddOnScreenMessage_tag_29, logic_uScript_AddOnScreenMessage_speaker_29, logic_uScript_AddOnScreenMessage_side_29);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_29.Out)
		{
			Relay_True_49();
		}
	}

	private void Relay_Connection_31()
	{
		if (this.FledComplete != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.FledComplete(this, args);
		}
	}

	private void Relay_InitialSpawn_35()
	{
		int num = 0;
		Array array = external_20;
		if (logic_uScript_SpawnTechsFromData_spawnData_35.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_35, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_35, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_35 = owner_Connection_33;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_35.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_35, logic_uScript_SpawnTechsFromData_ownerNode_35, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_35);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_35.Out)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_38()
	{
		int num = 0;
		Array array = external_20;
		if (logic_uScript_GetAndCheckTechs_techData_38.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_38, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_38, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_38 = owner_Connection_34;
		int num2 = 0;
		Array array2 = local_EnemyTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_38.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_38, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_38, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_38 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.In(logic_uScript_GetAndCheckTechs_techData_38, logic_uScript_GetAndCheckTechs_ownerNode_38, ref logic_uScript_GetAndCheckTechs_techs_38);
		local_EnemyTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_38;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_38.AllDead;
		if (allAlive)
		{
			Relay_In_27();
		}
		if (someAlive)
		{
			Relay_In_27();
		}
		if (allDead)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_39()
	{
		logic_uScriptCon_CompareBool_Bool_39 = local_EnemySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.In(logic_uScriptCon_CompareBool_Bool_39);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.False;
		if (num)
		{
			Relay_In_38();
		}
		if (flag)
		{
			Relay_True_42();
		}
	}

	private void Relay_In_41()
	{
		logic_uScriptCon_CompareBool_Bool_41 = local_EnemiesDefeated_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41.In(logic_uScriptCon_CompareBool_Bool_41);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41.False;
		if (num)
		{
			Relay_Connection_1();
		}
		if (flag)
		{
			Relay_In_44();
		}
	}

	private void Relay_True_42()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_42.True(out logic_uScriptAct_SetBool_Target_42);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_42;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_42.Out)
		{
			Relay_InitialSpawn_35();
		}
	}

	private void Relay_False_42()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_42.False(out logic_uScriptAct_SetBool_Target_42);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_42;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_42.Out)
		{
			Relay_InitialSpawn_35();
		}
	}

	private void Relay_In_44()
	{
		logic_uScriptCon_CompareBool_Bool_44 = local_PlayerEscaped_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44.In(logic_uScriptCon_CompareBool_Bool_44);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44.False;
		if (num)
		{
			Relay_Connection_31();
		}
		if (flag)
		{
			Relay_In_39();
		}
	}

	private void Relay_True_46()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_46.True(out logic_uScriptAct_SetBool_Target_46);
		local_EnemiesDefeated_System_Boolean = logic_uScriptAct_SetBool_Target_46;
	}

	private void Relay_False_46()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_46.False(out logic_uScriptAct_SetBool_Target_46);
		local_EnemiesDefeated_System_Boolean = logic_uScriptAct_SetBool_Target_46;
	}

	private void Relay_True_49()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.True(out logic_uScriptAct_SetBool_Target_49);
		local_PlayerEscaped_System_Boolean = logic_uScriptAct_SetBool_Target_49;
	}

	private void Relay_False_49()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.False(out logic_uScriptAct_SetBool_Target_49);
		local_PlayerEscaped_System_Boolean = logic_uScriptAct_SetBool_Target_49;
	}

	private void Relay_True_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.True(out logic_uScriptAct_SetBool_Target_50);
		local_PlayerEscaped_System_Boolean = logic_uScriptAct_SetBool_Target_50;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_50.Out)
		{
			Relay_False_57();
		}
	}

	private void Relay_False_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.False(out logic_uScriptAct_SetBool_Target_50);
		local_PlayerEscaped_System_Boolean = logic_uScriptAct_SetBool_Target_50;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_50.Out)
		{
			Relay_False_57();
		}
	}

	private void Relay_In_55()
	{
		logic_uScript_SaveVariable_variable_55 = local_PlayerEscaped_System_Boolean;
		logic_uScript_SaveVariable_owner_55 = owner_Connection_53;
		logic_uScript_SaveVariable_uniqueId_55 = local_PlayerEscapedID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_55.In(logic_uScript_SaveVariable_variable_55, logic_uScript_SaveVariable_owner_55, logic_uScript_SaveVariable_uniqueId_55);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_55.Out)
		{
			Relay_In_62();
		}
	}

	private void Relay_In_56()
	{
		logic_uScript_LoadBool_data_56 = local_PlayerEscaped_System_Boolean;
		logic_uScript_LoadBool_owner_56 = owner_Connection_53;
		logic_uScript_LoadBool_uniqueName_56 = local_PlayerEscapedID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_56.In(ref logic_uScript_LoadBool_data_56, logic_uScript_LoadBool_owner_56, logic_uScript_LoadBool_uniqueName_56);
		local_PlayerEscaped_System_Boolean = logic_uScript_LoadBool_data_56;
		if (logic_uScript_LoadBool_uScript_LoadBool_56.Out)
		{
			Relay_In_63();
		}
	}

	private void Relay_True_57()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.True(out logic_uScriptAct_SetBool_Target_57);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_57;
	}

	private void Relay_False_57()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.False(out logic_uScriptAct_SetBool_Target_57);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_57;
	}

	private void Relay_In_62()
	{
		logic_uScript_SaveVariable_variable_62 = local_EnemySpotted_System_Boolean;
		logic_uScript_SaveVariable_owner_62 = owner_Connection_60;
		logic_uScript_SaveVariable_uniqueId_62 = local_EnemySpottedID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_62.In(logic_uScript_SaveVariable_variable_62, logic_uScript_SaveVariable_owner_62, logic_uScript_SaveVariable_uniqueId_62);
	}

	private void Relay_In_63()
	{
		logic_uScript_LoadBool_data_63 = local_EnemySpotted_System_Boolean;
		logic_uScript_LoadBool_owner_63 = owner_Connection_60;
		logic_uScript_LoadBool_uniqueName_63 = local_EnemySpottedID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_63.In(ref logic_uScript_LoadBool_data_63, logic_uScript_LoadBool_owner_63, logic_uScript_LoadBool_uniqueName_63);
		local_EnemySpotted_System_Boolean = logic_uScript_LoadBool_data_63;
	}

	private void Relay_In_65()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_65 = owner_Connection_64;
		logic_uScript_MoveEncounterWithVisible_visibleObject_65 = local_66_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_65.In(logic_uScript_MoveEncounterWithVisible_ownerNode_65, logic_uScript_MoveEncounterWithVisible_visibleObject_65);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_65.Out)
		{
			Relay_In_21();
		}
	}
}
