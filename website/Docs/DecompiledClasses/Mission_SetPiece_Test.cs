using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("", "")]
public class Mission_SetPiece_Test : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private Tank local_CubeTech_Tank;

	private Tank local_curTank_Tank;

	private bool local_EncounterComplete_System_Boolean;

	private bool local_NeedsInitialising_System_Boolean = true;

	private Tank[] local_NewlyEnteredTechs_TankArray = new Tank[0];

	private Tank[] local_NewlyExitedTechs_TankArray = new Tank[0];

	private int local_StartingHour_System_Int32;

	private Tank local_Tech_Tank;

	private Tank[] local_Techs_TankArray = new Tank[0];

	private Tank[] local_TechsInside_TankArray = new Tank[0];

	public SpawnTechData[] TechData = new SpawnTechData[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_40;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_4;

	private bool logic_uScriptCon_CompareBool_True_4 = true;

	private bool logic_uScriptCon_CompareBool_False_4 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_6 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_6;

	private bool logic_uScript_FinishEncounter_Out_6 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_8 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_8 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_9 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_9;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_9 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_11 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_11 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_14 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_14;

	private bool logic_uScript_RemoveTech_Out_14 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_18 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_18 = new Tank[0];

	private int logic_uScript_AccessListTech_index_18;

	private Tank logic_uScript_AccessListTech_value_18;

	private bool logic_uScript_AccessListTech_Out_18 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_20 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_20;

	private object logic_uScript_SetEncounterTarget_visibleObject_20 = "";

	private bool logic_uScript_SetEncounterTarget_Out_20 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_21 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_21;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_21 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_21;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_21 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_21 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_21 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_21 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_23;

	private bool logic_uScriptCon_CompareBool_True_23 = true;

	private bool logic_uScriptCon_CompareBool_False_23 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_24 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_24;

	private bool logic_uScriptAct_SetBool_Out_24 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_24 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_24 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_27 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_27 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_27;

	private int logic_uScript_ForEachListTech_currentIndex_27;

	private bool logic_uScript_ForEachListTech_Immediate_27 = true;

	private bool logic_uScript_ForEachListTech_Done_27 = true;

	private bool logic_uScript_ForEachListTech_Iteration_27 = true;

	private uScript_GetTimeOfDay logic_uScript_GetTimeOfDay_uScript_GetTimeOfDay_31 = new uScript_GetTimeOfDay();

	private int logic_uScript_GetTimeOfDay_Return_31;

	private bool logic_uScript_GetTimeOfDay_Out_31 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_35 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_35 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_35;

	private int logic_uScript_ForEachListTech_currentIndex_35;

	private bool logic_uScript_ForEachListTech_Immediate_35 = true;

	private bool logic_uScript_ForEachListTech_Done_35 = true;

	private bool logic_uScript_ForEachListTech_Iteration_35 = true;

	private uScript_GetTechsInTrigger logic_uScript_GetTechsInTrigger_uScript_GetTechsInTrigger_36 = new uScript_GetTechsInTrigger();

	private string logic_uScript_GetTechsInTrigger_triggerAreaName_36 = "MissionRange";

	private Tank[] logic_uScript_GetTechsInTrigger_Entered_36;

	private Tank[] logic_uScript_GetTechsInTrigger_Inside_36;

	private Tank[] logic_uScript_GetTechsInTrigger_Exited_36;

	private bool logic_uScript_GetTechsInTrigger_Out_36 = true;

	private uScript_SetTechMaterialOverride logic_uScript_SetTechMaterialOverride_uScript_SetTechMaterialOverride_37 = new uScript_SetTechMaterialOverride();

	private Tank logic_uScript_SetTechMaterialOverride_tech_37;

	private bool logic_uScript_SetTechMaterialOverride_enable_37 = true;

	private ManTechMaterialSwap.MatType logic_uScript_SetTechMaterialOverride_customMaterialType_37 = ManTechMaterialSwap.MatType.Halloween;

	private bool logic_uScript_SetTechMaterialOverride_Out_37 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_38 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_38 = "Trigger1";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_38 = "Trigger1";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_38;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_38 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_38 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_38 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_38 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_38 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_38 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_38 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_39 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_39 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_39;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_39;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_39;

	private bool logic_uScript_SpawnTechsFromData_Out_39 = true;

	private uScript_SetDangerMusicMisc logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_41 = new uScript_SetDangerMusicMisc();

	private ManMusic.MiscDangerMusicType logic_uScript_SetDangerMusicMisc_miscDangerMusicType_41;

	private Tank logic_uScript_SetDangerMusicMisc_tech_41;

	private bool logic_uScript_SetDangerMusicMisc_Out_41 = true;

	private uScript_SetDangerMusicMisc logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_42 = new uScript_SetDangerMusicMisc();

	private ManMusic.MiscDangerMusicType logic_uScript_SetDangerMusicMisc_miscDangerMusicType_42 = ManMusic.MiscDangerMusicType.Halloween;

	private Tank logic_uScript_SetDangerMusicMisc_tech_42;

	private bool logic_uScript_SetDangerMusicMisc_Out_42 = true;

	private uScript_OverrideTankCameraDistanceMax logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_43 = new uScript_OverrideTankCameraDistanceMax();

	private bool logic_uScript_OverrideTankCameraDistanceMax_enable_43;

	private float logic_uScript_OverrideTankCameraDistanceMax_newDistanceMax_43 = 60f;

	private Tank logic_uScript_OverrideTankCameraDistanceMax_tech_43;

	private bool logic_uScript_OverrideTankCameraDistanceMax_Out_43 = true;

	private uScript_OverrideTankCameraDistanceMax logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_44 = new uScript_OverrideTankCameraDistanceMax();

	private bool logic_uScript_OverrideTankCameraDistanceMax_enable_44 = true;

	private float logic_uScript_OverrideTankCameraDistanceMax_newDistanceMax_44 = 5f;

	private Tank logic_uScript_OverrideTankCameraDistanceMax_tech_44;

	private bool logic_uScript_OverrideTankCameraDistanceMax_Out_44 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_98 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_98 = 5;

	private Tank logic_uScript_SetTimeOfDay_tech_98;

	private bool logic_uScript_SetTimeOfDay_Out_98 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_100 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_100;

	private Tank logic_uScript_SetTimeOfDay_tech_100;

	private bool logic_uScript_SetTimeOfDay_Out_100 = true;

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
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_2;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_2;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_2;
				}
			}
		}
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
		}
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
		}
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
		}
		if (null == owner_Connection_40 || !m_RegisteredForEvents)
		{
			owner_Connection_40 = parentGameObject;
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
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_2;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_2;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_2;
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
				component2.SaveEvent -= Instance_SaveEvent_2;
				component2.LoadEvent -= Instance_LoadEvent_2;
				component2.RestartEvent -= Instance_RestartEvent_2;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_6.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_8.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_9.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_11.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_14.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_18.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_20.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_27.SetParent(g);
		logic_uScript_GetTimeOfDay_uScript_GetTimeOfDay_31.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_35.SetParent(g);
		logic_uScript_GetTechsInTrigger_uScript_GetTechsInTrigger_36.SetParent(g);
		logic_uScript_SetTechMaterialOverride_uScript_SetTechMaterialOverride_37.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_38.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_39.SetParent(g);
		logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_41.SetParent(g);
		logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_42.SetParent(g);
		logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_43.SetParent(g);
		logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_44.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_98.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_100.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_40 = parentGameObject;
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
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_9.OnEnable();
	}

	public void OnDisable()
	{
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

	private void Instance_SaveEvent_2(object o, EventArgs e)
	{
		Relay_SaveEvent_2();
	}

	private void Instance_LoadEvent_2(object o, EventArgs e)
	{
		Relay_LoadEvent_2();
	}

	private void Instance_RestartEvent_2(object o, EventArgs e)
	{
		Relay_RestartEvent_2();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_23();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_SaveEvent_2()
	{
	}

	private void Relay_LoadEvent_2()
	{
	}

	private void Relay_RestartEvent_2()
	{
	}

	private void Relay_In_4()
	{
		logic_uScriptCon_CompareBool_Bool_4 = local_EncounterComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.In(logic_uScriptCon_CompareBool_Bool_4);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.False;
		if (num)
		{
			Relay_In_14();
		}
		if (flag)
		{
			Relay_In_36();
		}
	}

	private void Relay_Succeed_6()
	{
		logic_uScript_FinishEncounter_owner_6 = owner_Connection_7;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_6.Succeed(logic_uScript_FinishEncounter_owner_6);
	}

	private void Relay_Fail_6()
	{
		logic_uScript_FinishEncounter_owner_6 = owner_Connection_7;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_6.Fail(logic_uScript_FinishEncounter_owner_6);
	}

	private void Relay_Pause_8()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_8.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_8.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_UnPause_8()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_8.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_8.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_9()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_9 = owner_Connection_10;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_9.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_9);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_9.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_Pause_11()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_11.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_11.Out)
		{
			Relay_Succeed_6();
		}
	}

	private void Relay_UnPause_11()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_11.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_11.Out)
		{
			Relay_Succeed_6();
		}
	}

	private void Relay_In_14()
	{
		logic_uScript_RemoveTech_tech_14 = local_CubeTech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_14.In(logic_uScript_RemoveTech_tech_14);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_14.Out)
		{
			Relay_Pause_11();
		}
	}

	private void Relay_AtIndex_18()
	{
		int num = 0;
		Array array = local_Techs_TankArray;
		if (logic_uScript_AccessListTech_techList_18.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_18, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_18, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_18.AtIndex(ref logic_uScript_AccessListTech_techList_18, logic_uScript_AccessListTech_index_18, out logic_uScript_AccessListTech_value_18);
		local_Techs_TankArray = logic_uScript_AccessListTech_techList_18;
		local_Tech_Tank = logic_uScript_AccessListTech_value_18;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_18.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_SetEncounterTarget_owner_20 = owner_Connection_22;
		logic_uScript_SetEncounterTarget_visibleObject_20 = local_Tech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_20.In(logic_uScript_SetEncounterTarget_owner_20, logic_uScript_SetEncounterTarget_visibleObject_20);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_20.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_21()
	{
		int num = 0;
		Array techData = TechData;
		if (logic_uScript_GetAndCheckTechs_techData_21.Length != num + techData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_21, num + techData.Length);
		}
		Array.Copy(techData, 0, logic_uScript_GetAndCheckTechs_techData_21, num, techData.Length);
		num += techData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_21 = owner_Connection_19;
		int num2 = 0;
		Array array = local_Techs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_21.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_21, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_21, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_21 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.In(logic_uScript_GetAndCheckTechs_techData_21, logic_uScript_GetAndCheckTechs_ownerNode_21, ref logic_uScript_GetAndCheckTechs_techs_21);
		local_Techs_TankArray = logic_uScript_GetAndCheckTechs_techs_21;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_18();
		}
		if (someAlive)
		{
			Relay_AtIndex_18();
		}
	}

	private void Relay_In_23()
	{
		logic_uScriptCon_CompareBool_Bool_23 = local_NeedsInitialising_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.In(logic_uScriptCon_CompareBool_Bool_23);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.False;
		if (num)
		{
			Relay_Pause_8();
		}
		if (flag)
		{
			Relay_In_4();
		}
	}

	private void Relay_True_24()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.True(out logic_uScriptAct_SetBool_Target_24);
		local_NeedsInitialising_System_Boolean = logic_uScriptAct_SetBool_Target_24;
	}

	private void Relay_False_24()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.False(out logic_uScriptAct_SetBool_Target_24);
		local_NeedsInitialising_System_Boolean = logic_uScriptAct_SetBool_Target_24;
	}

	private void Relay_Reset_27()
	{
		int num = 0;
		Array array = local_NewlyExitedTechs_TankArray;
		if (logic_uScript_ForEachListTech_List_27.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_27, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_27, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_27.Reset(logic_uScript_ForEachListTech_List_27, out logic_uScript_ForEachListTech_Value_27, out logic_uScript_ForEachListTech_currentIndex_27);
		local_curTank_Tank = logic_uScript_ForEachListTech_Value_27;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_27.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_27.Iteration;
		if (done)
		{
			Relay_In_35();
		}
		if (iteration)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_27()
	{
		int num = 0;
		Array array = local_NewlyExitedTechs_TankArray;
		if (logic_uScript_ForEachListTech_List_27.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_27, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_27, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_27.In(logic_uScript_ForEachListTech_List_27, out logic_uScript_ForEachListTech_Value_27, out logic_uScript_ForEachListTech_currentIndex_27);
		local_curTank_Tank = logic_uScript_ForEachListTech_Value_27;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_27.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_27.Iteration;
		if (done)
		{
			Relay_In_35();
		}
		if (iteration)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_31()
	{
		logic_uScript_GetTimeOfDay_Return_31 = logic_uScript_GetTimeOfDay_uScript_GetTimeOfDay_31.In();
		local_StartingHour_System_Int32 = logic_uScript_GetTimeOfDay_Return_31;
		if (logic_uScript_GetTimeOfDay_uScript_GetTimeOfDay_31.Out)
		{
			Relay_False_24();
		}
	}

	private void Relay_Reset_35()
	{
		int num = 0;
		Array array = local_NewlyEnteredTechs_TankArray;
		if (logic_uScript_ForEachListTech_List_35.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_35, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_35, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_35.Reset(logic_uScript_ForEachListTech_List_35, out logic_uScript_ForEachListTech_Value_35, out logic_uScript_ForEachListTech_currentIndex_35);
		local_curTank_Tank = logic_uScript_ForEachListTech_Value_35;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_35.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_35.Iteration;
		if (done)
		{
			Relay_In_38();
		}
		if (iteration)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_35()
	{
		int num = 0;
		Array array = local_NewlyEnteredTechs_TankArray;
		if (logic_uScript_ForEachListTech_List_35.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_35, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_35, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_35.In(logic_uScript_ForEachListTech_List_35, out logic_uScript_ForEachListTech_Value_35, out logic_uScript_ForEachListTech_currentIndex_35);
		local_curTank_Tank = logic_uScript_ForEachListTech_Value_35;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_35.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_35.Iteration;
		if (done)
		{
			Relay_In_38();
		}
		if (iteration)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_36()
	{
		logic_uScript_GetTechsInTrigger_uScript_GetTechsInTrigger_36.In(logic_uScript_GetTechsInTrigger_triggerAreaName_36, out logic_uScript_GetTechsInTrigger_Entered_36, out logic_uScript_GetTechsInTrigger_Inside_36, out logic_uScript_GetTechsInTrigger_Exited_36);
		local_NewlyEnteredTechs_TankArray = logic_uScript_GetTechsInTrigger_Entered_36;
		local_TechsInside_TankArray = logic_uScript_GetTechsInTrigger_Inside_36;
		local_NewlyExitedTechs_TankArray = logic_uScript_GetTechsInTrigger_Exited_36;
		if (logic_uScript_GetTechsInTrigger_uScript_GetTechsInTrigger_36.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_SetTechMaterialOverride_tech_37 = local_Tech_Tank;
		logic_uScript_SetTechMaterialOverride_uScript_SetTechMaterialOverride_37.In(logic_uScript_SetTechMaterialOverride_tech_37, logic_uScript_SetTechMaterialOverride_enable_37, logic_uScript_SetTechMaterialOverride_customMaterialType_37);
		if (logic_uScript_SetTechMaterialOverride_uScript_SetTechMaterialOverride_37.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_In_38()
	{
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_38.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_38, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_38, ref logic_uScript_IsPlayerInTriggerSmart_inside_38);
		bool firstEntered = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_38.FirstEntered;
		bool lastExited = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_38.LastExited;
		if (firstEntered)
		{
			Relay_InitialSpawn_39();
		}
		if (lastExited)
		{
			Relay_In_100();
		}
	}

	private void Relay_InitialSpawn_39()
	{
		int num = 0;
		Array techData = TechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_39.Length != num + techData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_39, num + techData.Length);
		}
		Array.Copy(techData, 0, logic_uScript_SpawnTechsFromData_spawnData_39, num, techData.Length);
		num += techData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_39 = owner_Connection_40;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_39.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_39, logic_uScript_SpawnTechsFromData_ownerNode_39, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_39, logic_uScript_SpawnTechsFromData_allowResurrection_39);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_39.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_41()
	{
		logic_uScript_SetDangerMusicMisc_tech_41 = local_curTank_Tank;
		logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_41.In(logic_uScript_SetDangerMusicMisc_miscDangerMusicType_41, logic_uScript_SetDangerMusicMisc_tech_41);
		if (logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_41.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_42()
	{
		logic_uScript_SetDangerMusicMisc_tech_42 = local_curTank_Tank;
		logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_42.In(logic_uScript_SetDangerMusicMisc_miscDangerMusicType_42, logic_uScript_SetDangerMusicMisc_tech_42);
		if (logic_uScript_SetDangerMusicMisc_uScript_SetDangerMusicMisc_42.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_43()
	{
		logic_uScript_OverrideTankCameraDistanceMax_tech_43 = local_curTank_Tank;
		logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_43.In(logic_uScript_OverrideTankCameraDistanceMax_enable_43, logic_uScript_OverrideTankCameraDistanceMax_newDistanceMax_43, logic_uScript_OverrideTankCameraDistanceMax_tech_43);
	}

	private void Relay_In_44()
	{
		logic_uScript_OverrideTankCameraDistanceMax_tech_44 = local_curTank_Tank;
		logic_uScript_OverrideTankCameraDistanceMax_uScript_OverrideTankCameraDistanceMax_44.In(logic_uScript_OverrideTankCameraDistanceMax_enable_44, logic_uScript_OverrideTankCameraDistanceMax_newDistanceMax_44, logic_uScript_OverrideTankCameraDistanceMax_tech_44);
	}

	private void Relay_In_98()
	{
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_98.In(logic_uScript_SetTimeOfDay_hour_98, logic_uScript_SetTimeOfDay_tech_98);
	}

	private void Relay_In_100()
	{
		logic_uScript_SetTimeOfDay_hour_100 = local_StartingHour_System_Int32;
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_100.In(logic_uScript_SetTimeOfDay_hour_100, logic_uScript_SetTimeOfDay_tech_100);
	}
}
