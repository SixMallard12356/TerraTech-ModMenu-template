using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_EscortAlliedTechs : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public SpawnTechData destinationObject;

	public SpawnTechData[] enemyGroup1 = new SpawnTechData[0];

	public SpawnTechData[] enemyGroup2 = new SpawnTechData[0];

	public SpawnTechData[] enemyGroup3 = new SpawnTechData[0];

	public float enemySpawnDistFromPlayer = 50f;

	public float[] enemySpawnThresholds = new float[0];

	private Tank[] local_53_TankArray = new Tank[0];

	private Tank local_DestinationObject_Tank;

	private int local_SpawnCounter_System_Int32 = 1;

	private bool local_TechsSpawned_System_Boolean;

	private Tank[] local_TechsToDefend_TankArray = new Tank[0];

	public LocalisedString[] msgLose = new LocalisedString[0];

	public LocalisedString[] msgWin = new LocalisedString[0];

	public SpawnTechData[] techsToDefend = new SpawnTechData[0];

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_38;

	private GameObject owner_Connection_42;

	private GameObject owner_Connection_46;

	private GameObject owner_Connection_50;

	private GameObject owner_Connection_55;

	private GameObject owner_Connection_114;

	private GameObject owner_Connection_118;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_2 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_2 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_2;

	private string logic_uScript_AddOnScreenMessage_tag_2 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_2;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_2;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_2;

	private bool logic_uScript_AddOnScreenMessage_Out_2 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_2 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_6;

	private float logic_uScript_IsPlayerInRangeOfTech_range_6 = 15f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_6 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_6 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_6 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_6 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_7 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_7 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_7;

	private bool logic_uScript_SetTankInvulnerable_Out_7 = true;

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

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_11 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_11 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_11 = 15f;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_11 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_11 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_11 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_13 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_13;

	private object logic_uScript_SetEncounterTarget_visibleObject_13 = "";

	private bool logic_uScript_SetEncounterTarget_Out_13 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_15;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_21 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_21;

	private int logic_uScriptAct_AddInt_v2_B_21 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_21;

	private float logic_uScriptAct_AddInt_v2_FloatResult_21;

	private bool logic_uScriptAct_AddInt_v2_Out_21 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_23 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_23;

	private int logic_uScriptAct_AddInt_v2_B_23 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_23;

	private float logic_uScriptAct_AddInt_v2_FloatResult_23;

	private bool logic_uScriptAct_AddInt_v2_Out_23 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_29 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_29;

	private int logic_uScriptAct_AddInt_v2_B_29 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_29;

	private float logic_uScriptAct_AddInt_v2_FloatResult_29;

	private bool logic_uScriptAct_AddInt_v2_Out_29 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_41 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_41;

	private bool logic_uScriptAct_SetBool_Out_41 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_41 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_41 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_43;

	private bool logic_uScriptCon_CompareBool_True_43 = true;

	private bool logic_uScriptCon_CompareBool_False_43 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_44 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_44 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_44;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_44 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_44 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_47 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_47 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_47;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_47 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_47 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_48 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_48 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_48;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_48 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_48;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_48 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_48 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_48 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_48 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_49 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_49 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_49;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_49 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_49;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_49 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_49 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_49 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_49 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_52 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_52 = new Tank[0];

	private int logic_uScript_AccessListTech_index_52;

	private Tank logic_uScript_AccessListTech_value_52;

	private bool logic_uScript_AccessListTech_Out_52 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_57 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_57 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_58 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_58 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_59 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_59 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_113 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_113;

	private bool logic_uScript_FinishEncounter_Out_113 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_117 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_117;

	private bool logic_uScript_FinishEncounter_Out_117 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
		}
		if (null == owner_Connection_38 || !m_RegisteredForEvents)
		{
			owner_Connection_38 = parentGameObject;
			if (null != owner_Connection_38)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_38.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_38.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_39;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_39;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_39;
				}
			}
		}
		if (null == owner_Connection_42 || !m_RegisteredForEvents)
		{
			owner_Connection_42 = parentGameObject;
		}
		if (null == owner_Connection_46 || !m_RegisteredForEvents)
		{
			owner_Connection_46 = parentGameObject;
		}
		if (null == owner_Connection_50 || !m_RegisteredForEvents)
		{
			owner_Connection_50 = parentGameObject;
		}
		if (null == owner_Connection_55 || !m_RegisteredForEvents)
		{
			owner_Connection_55 = parentGameObject;
		}
		if (null == owner_Connection_114 || !m_RegisteredForEvents)
		{
			owner_Connection_114 = parentGameObject;
		}
		if (null == owner_Connection_118 || !m_RegisteredForEvents)
		{
			owner_Connection_118 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_38)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_38.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_38.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_39;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_39;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_39;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_38)
		{
			uScript_EncounterUpdate component = owner_Connection_38.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_39;
				component.OnSuspend -= Instance_OnSuspend_39;
				component.OnResume -= Instance_OnResume_39;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_7.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_10.SetParent(g);
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_11.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_13.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_21.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_23.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_29.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_44.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_47.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_48.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_49.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_52.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_57.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_58.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_59.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_113.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_117.SetParent(g);
		owner_Connection_14 = parentGameObject;
		owner_Connection_38 = parentGameObject;
		owner_Connection_42 = parentGameObject;
		owner_Connection_46 = parentGameObject;
		owner_Connection_50 = parentGameObject;
		owner_Connection_55 = parentGameObject;
		owner_Connection_114 = parentGameObject;
		owner_Connection_118 = parentGameObject;
	}

	public void Awake()
	{
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output1 += uScriptCon_ManualSwitch_Output1_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output2 += uScriptCon_ManualSwitch_Output2_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output3 += uScriptCon_ManualSwitch_Output3_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output4 += uScriptCon_ManualSwitch_Output4_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output5 += uScriptCon_ManualSwitch_Output5_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output6 += uScriptCon_ManualSwitch_Output6_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output7 += uScriptCon_ManualSwitch_Output7_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output8 += uScriptCon_ManualSwitch_Output8_15;
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
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_7.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_10.OnDisable();
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_11.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
	}

	public void OnDestroy()
	{
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output1 -= uScriptCon_ManualSwitch_Output1_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output2 -= uScriptCon_ManualSwitch_Output2_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output3 -= uScriptCon_ManualSwitch_Output3_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output4 -= uScriptCon_ManualSwitch_Output4_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output5 -= uScriptCon_ManualSwitch_Output5_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output6 -= uScriptCon_ManualSwitch_Output6_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output7 -= uScriptCon_ManualSwitch_Output7_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output8 -= uScriptCon_ManualSwitch_Output8_15;
	}

	private void Instance_OnUpdate_39(object o, EventArgs e)
	{
		Relay_OnUpdate_39();
	}

	private void Instance_OnSuspend_39(object o, EventArgs e)
	{
		Relay_OnSuspend_39();
	}

	private void Instance_OnResume_39(object o, EventArgs e)
	{
		Relay_OnResume_39();
	}

	private void uScriptCon_ManualSwitch_Output1_15(object o, EventArgs e)
	{
		Relay_Output1_15();
	}

	private void uScriptCon_ManualSwitch_Output2_15(object o, EventArgs e)
	{
		Relay_Output2_15();
	}

	private void uScriptCon_ManualSwitch_Output3_15(object o, EventArgs e)
	{
		Relay_Output3_15();
	}

	private void uScriptCon_ManualSwitch_Output4_15(object o, EventArgs e)
	{
		Relay_Output4_15();
	}

	private void uScriptCon_ManualSwitch_Output5_15(object o, EventArgs e)
	{
		Relay_Output5_15();
	}

	private void uScriptCon_ManualSwitch_Output6_15(object o, EventArgs e)
	{
		Relay_Output6_15();
	}

	private void uScriptCon_ManualSwitch_Output7_15(object o, EventArgs e)
	{
		Relay_Output7_15();
	}

	private void uScriptCon_ManualSwitch_Output8_15(object o, EventArgs e)
	{
		Relay_Output8_15();
	}

	private void Relay_In_2()
	{
		int num = 0;
		Array array = msgLose;
		if (logic_uScript_AddOnScreenMessage_locString_2.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_2, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_2, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_2 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2.In(logic_uScript_AddOnScreenMessage_locString_2, logic_uScript_AddOnScreenMessage_msgPriority_2, logic_uScript_AddOnScreenMessage_holdMsg_2, logic_uScript_AddOnScreenMessage_tag_2, logic_uScript_AddOnScreenMessage_speaker_2, logic_uScript_AddOnScreenMessage_side_2);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2.Out)
		{
			Relay_Fail_117();
		}
	}

	private void Relay_In_6()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_6 = local_DestinationObject_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.In(logic_uScript_IsPlayerInRangeOfTech_tech_6, logic_uScript_IsPlayerInRangeOfTech_range_6, logic_uScript_IsPlayerInRangeOfTech_techs_6);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.OutOfRange;
		if (inRange)
		{
			Relay_In_11();
		}
		if (outOfRange)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_7()
	{
		logic_uScript_SetTankInvulnerable_tank_7 = local_DestinationObject_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_7.In(logic_uScript_SetTankInvulnerable_invulnerable_7, logic_uScript_SetTankInvulnerable_tank_7);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_7.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_10()
	{
		int num = 0;
		Array array = msgWin;
		if (logic_uScript_AddOnScreenMessage_locString_10.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_10, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_10, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_10 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_10.In(logic_uScript_AddOnScreenMessage_locString_10, logic_uScript_AddOnScreenMessage_msgPriority_10, logic_uScript_AddOnScreenMessage_holdMsg_10, logic_uScript_AddOnScreenMessage_tag_10, logic_uScript_AddOnScreenMessage_speaker_10, logic_uScript_AddOnScreenMessage_side_10);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_10.Out)
		{
			Relay_Succeed_113();
		}
	}

	private void Relay_In_11()
	{
		int num = 0;
		Array array = local_TechsToDefend_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_11.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_11, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_11, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_11.In(logic_uScript_InRangeOfAtLeastOneTech_techs_11, logic_uScript_InRangeOfAtLeastOneTech_range_11);
		bool inRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_11.InRange;
		bool outOfRange = logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_11.OutOfRange;
		if (inRange)
		{
			Relay_In_10();
		}
		if (outOfRange)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_13()
	{
		logic_uScript_SetEncounterTarget_owner_13 = owner_Connection_14;
		logic_uScript_SetEncounterTarget_visibleObject_13 = local_DestinationObject_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_13.In(logic_uScript_SetEncounterTarget_owner_13, logic_uScript_SetEncounterTarget_visibleObject_13);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_13.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_Output1_15()
	{
		Relay_In_57();
	}

	private void Relay_Output2_15()
	{
		Relay_In_58();
	}

	private void Relay_Output3_15()
	{
		Relay_In_59();
	}

	private void Relay_Output4_15()
	{
	}

	private void Relay_Output5_15()
	{
	}

	private void Relay_Output6_15()
	{
	}

	private void Relay_Output7_15()
	{
	}

	private void Relay_Output8_15()
	{
	}

	private void Relay_In_15()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_15 = local_SpawnCounter_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.In(logic_uScriptCon_ManualSwitch_CurrentOutput_15);
	}

	private void Relay_In_21()
	{
		logic_uScriptAct_AddInt_v2_A_21 = local_SpawnCounter_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_21.In(logic_uScriptAct_AddInt_v2_A_21, logic_uScriptAct_AddInt_v2_B_21, out logic_uScriptAct_AddInt_v2_IntResult_21, out logic_uScriptAct_AddInt_v2_FloatResult_21);
		local_SpawnCounter_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_21;
	}

	private void Relay_In_23()
	{
		logic_uScriptAct_AddInt_v2_A_23 = local_SpawnCounter_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_23.In(logic_uScriptAct_AddInt_v2_A_23, logic_uScriptAct_AddInt_v2_B_23, out logic_uScriptAct_AddInt_v2_IntResult_23, out logic_uScriptAct_AddInt_v2_FloatResult_23);
		local_SpawnCounter_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_23;
	}

	private void Relay_In_29()
	{
		logic_uScriptAct_AddInt_v2_A_29 = local_SpawnCounter_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_29.In(logic_uScriptAct_AddInt_v2_A_29, logic_uScriptAct_AddInt_v2_B_29, out logic_uScriptAct_AddInt_v2_IntResult_29, out logic_uScriptAct_AddInt_v2_FloatResult_29);
		local_SpawnCounter_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_29;
	}

	private void Relay_OnUpdate_39()
	{
		Relay_In_43();
	}

	private void Relay_OnSuspend_39()
	{
	}

	private void Relay_OnResume_39()
	{
	}

	private void Relay_True_41()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.True(out logic_uScriptAct_SetBool_Target_41);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_41;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_41.Out)
		{
			Relay_InitialSpawn_47();
		}
	}

	private void Relay_False_41()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.False(out logic_uScriptAct_SetBool_Target_41);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_41;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_41.Out)
		{
			Relay_InitialSpawn_47();
		}
	}

	private void Relay_In_43()
	{
		logic_uScriptCon_CompareBool_Bool_43 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.In(logic_uScriptCon_CompareBool_Bool_43);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_43.False;
		if (num)
		{
			Relay_In_49();
		}
		if (flag)
		{
			Relay_True_41();
		}
	}

	private void Relay_InitialSpawn_44()
	{
		int num = 0;
		Array array = techsToDefend;
		if (logic_uScript_SpawnTechsFromData_spawnData_44.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_44, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_44, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_44 = owner_Connection_42;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_44.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_44, logic_uScript_SpawnTechsFromData_ownerNode_44, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_44);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_44.Out)
		{
			Relay_In_49();
		}
	}

	private void Relay_InitialSpawn_47()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_47.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_47, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_47[num++] = destinationObject;
		logic_uScript_SpawnTechsFromData_ownerNode_47 = owner_Connection_46;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_47.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_47, logic_uScript_SpawnTechsFromData_ownerNode_47, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_47);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_47.Out)
		{
			Relay_InitialSpawn_44();
		}
	}

	private void Relay_In_48()
	{
		int num = 0;
		Array array = techsToDefend;
		if (logic_uScript_GetAndCheckTechs_techData_48.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_48, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_48, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_48 = owner_Connection_55;
		int num2 = 0;
		Array array2 = local_TechsToDefend_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_48.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_48, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_48, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_48 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_48.In(logic_uScript_GetAndCheckTechs_techData_48, logic_uScript_GetAndCheckTechs_ownerNode_48, ref logic_uScript_GetAndCheckTechs_techs_48);
		local_TechsToDefend_TankArray = logic_uScript_GetAndCheckTechs_techs_48;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_48.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_48.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_48.AllDead;
		if (allAlive)
		{
			Relay_In_6();
		}
		if (someAlive)
		{
			Relay_In_6();
		}
		if (allDead)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_49()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_49.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_49, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_49[num++] = destinationObject;
		logic_uScript_GetAndCheckTechs_ownerNode_49 = owner_Connection_50;
		int num2 = 0;
		Array array = local_53_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_49.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_49, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_49, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_49 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_49.In(logic_uScript_GetAndCheckTechs_techData_49, logic_uScript_GetAndCheckTechs_ownerNode_49, ref logic_uScript_GetAndCheckTechs_techs_49);
		local_53_TankArray = logic_uScript_GetAndCheckTechs_techs_49;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_49.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_49.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_52();
		}
		if (someAlive)
		{
			Relay_AtIndex_52();
		}
	}

	private void Relay_AtIndex_52()
	{
		int num = 0;
		Array array = local_53_TankArray;
		if (logic_uScript_AccessListTech_techList_52.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_52, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_52, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_52.AtIndex(ref logic_uScript_AccessListTech_techList_52, logic_uScript_AccessListTech_index_52, out logic_uScript_AccessListTech_value_52);
		local_53_TankArray = logic_uScript_AccessListTech_techList_52;
		local_DestinationObject_Tank = logic_uScript_AccessListTech_value_52;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_52.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_57()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_57.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_57.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_58()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_58.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_58.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_59()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_59.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_59.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_Succeed_113()
	{
		logic_uScript_FinishEncounter_owner_113 = owner_Connection_114;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_113.Succeed(logic_uScript_FinishEncounter_owner_113);
	}

	private void Relay_Fail_113()
	{
		logic_uScript_FinishEncounter_owner_113 = owner_Connection_114;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_113.Fail(logic_uScript_FinishEncounter_owner_113);
	}

	private void Relay_Succeed_117()
	{
		logic_uScript_FinishEncounter_owner_117 = owner_Connection_118;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_117.Succeed(logic_uScript_FinishEncounter_owner_117);
	}

	private void Relay_Fail_117()
	{
		logic_uScript_FinishEncounter_owner_117 = owner_Connection_118;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_117.Fail(logic_uScript_FinishEncounter_owner_117);
	}
}
