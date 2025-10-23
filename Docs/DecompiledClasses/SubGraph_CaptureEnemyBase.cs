using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_CaptureEnemyBase", "Note: Only supports spawning one base curently")]
public class SubGraph_CaptureEnemyBase : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private string external_125 = "";

	private float external_126;

	private SpawnTechData[] external_10 = new SpawnTechData[0];

	private SpawnTechData[] external_11 = new SpawnTechData[0];

	private SpawnTechData[] external_12 = new SpawnTechData[0];

	private LocalisedString[] external_17 = new LocalisedString[0];

	private LocalisedString[] external_9 = new LocalisedString[0];

	private LocalisedString[] external_8 = new LocalisedString[0];

	private ManOnScreenMessages.Speaker external_121;

	private float local_46_System_Single = 0.5f;

	private Tank[] local_90_TankArray = new Tank[0];

	private bool local_BaseCaptured_System_Boolean;

	private string local_BaseCapturedID_System_String = "BaseCaptured";

	private bool local_BaseDestroyed_System_Boolean;

	private string local_BaseDestroyedID_System_String = "BaseDestroyed";

	private Tank local_BaseTech_Tank;

	private bool local_CaptureObjective_System_Boolean;

	private float local_DistNearBase_System_Single = 50f;

	private bool local_EnemiesSpawned_System_Boolean;

	private string local_EnemiesSpawnedID_System_String = "EnemiesSpawned";

	private Tank[] local_GuardianTechs_TankArray = new Tank[0];

	private Tank[] local_HarvesterTechs_TankArray = new Tank[0];

	private bool local_MsgBaseSpottedShown_System_Boolean;

	private string local_MsgBaseSpottedShownID_System_String = "MsgBaseSpottedShown";

	private int local_PlayerTeam_System_Int32;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_66;

	private GameObject owner_Connection_67;

	private GameObject owner_Connection_72;

	private GameObject owner_Connection_75;

	private GameObject owner_Connection_81;

	private GameObject owner_Connection_95;

	private GameObject owner_Connection_100;

	private GameObject owner_Connection_105;

	private GameObject owner_Connection_110;

	private GameObject owner_Connection_119;

	private GameObject owner_Connection_123;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_4 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_4 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_4;

	private string logic_uScript_AddOnScreenMessage_tag_4 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_4;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_4;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_4;

	private bool logic_uScript_AddOnScreenMessage_Out_4 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_4 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_7 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_7 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_7 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_7;

	private string logic_uScript_AddOnScreenMessage_tag_7 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_7;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_7;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_7;

	private bool logic_uScript_AddOnScreenMessage_Out_7 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_7 = true;

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

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_18 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_18;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_18;

	private bool logic_uScript_SetBatteryChargeAmount_Out_18 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_21 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_21;

	private int logic_uScript_SetTankTeam_team_21;

	private bool logic_uScript_SetTankTeam_Out_21 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_22;

	private bool logic_uScriptCon_CompareBool_True_22 = true;

	private bool logic_uScriptCon_CompareBool_False_22 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_24 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_24;

	private bool logic_uScriptAct_SetBool_Out_24 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_24 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_24 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_26 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_26;

	private bool logic_uScriptAct_SetBool_Out_26 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_26 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_26 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_27 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_27;

	private bool logic_uScriptAct_SetBool_Out_27 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_27 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_27 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_29 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_29;

	private bool logic_uScriptAct_SetBool_Out_29 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_29 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_29 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_31;

	private bool logic_uScriptCon_CompareBool_True_31 = true;

	private bool logic_uScriptCon_CompareBool_False_31 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_34 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_34;

	private bool logic_uScriptCon_CompareBool_True_34 = true;

	private bool logic_uScriptCon_CompareBool_False_34 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_38 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_38;

	private bool logic_uScriptAct_SetBool_Out_38 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_38 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_38 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_39 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_39;

	private bool logic_uScriptAct_SetBool_Out_39 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_39 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_39 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_40 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_40;

	private float logic_uScript_IsPlayerInRangeOfTech_range_40;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_40 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_40 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_40 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_40 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_44 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_44;

	private bool logic_uScriptAct_SetBool_Out_44 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_44 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_44 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_50 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_50 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_50;

	private bool logic_uScript_SetTechsTeam_Out_50 = true;

	private uScript_GetPlayerTeam logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_52 = new uScript_GetPlayerTeam();

	private int logic_uScript_GetPlayerTeam_Return_52;

	private bool logic_uScript_GetPlayerTeam_Out_52 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_58 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_58 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_58;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_58 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_58 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_59 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_59 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_59;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_59 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_59 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_60 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_60 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_60;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_60 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_60 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_61 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_61 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_61;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_61 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_61;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_61 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_61 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_61 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_61 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_62;

	private bool logic_uScriptCon_CompareBool_True_62 = true;

	private bool logic_uScriptCon_CompareBool_False_62 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_64 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_64 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_64;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_64 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_64;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_64 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_64 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_64 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_64 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_71 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_71;

	private bool logic_uScriptCon_CompareBool_True_71 = true;

	private bool logic_uScriptCon_CompareBool_False_71 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_73 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_73;

	private bool logic_uScriptAct_SetBool_Out_73 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_73 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_73 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_74 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_74;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_74 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_74;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_74 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_82 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_82 = new Tank[0];

	private int logic_uScript_AccessListTech_index_82;

	private Tank logic_uScript_AccessListTech_value_82;

	private bool logic_uScript_AccessListTech_Out_82 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_85;

	private bool logic_uScriptCon_CompareBool_True_85 = true;

	private bool logic_uScriptCon_CompareBool_False_85 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_86 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_86;

	private object logic_uScript_SetEncounterTarget_visibleObject_86 = "";

	private bool logic_uScript_SetEncounterTarget_Out_86 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_92 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_92;

	private bool logic_uScriptAct_SetBool_Out_92 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_92 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_92 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_96 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_96 = "";

	private GameObject logic_uScript_SaveVariable_owner_96;

	private string logic_uScript_SaveVariable_uniqueId_96 = "";

	private bool logic_uScript_SaveVariable_Out_96 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_97 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_97;

	private GameObject logic_uScript_LoadBool_owner_97;

	private string logic_uScript_LoadBool_uniqueName_97 = "";

	private bool logic_uScript_LoadBool_Out_97 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_101 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_101;

	private GameObject logic_uScript_LoadBool_owner_101;

	private string logic_uScript_LoadBool_uniqueName_101 = "";

	private bool logic_uScript_LoadBool_Out_101 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_102 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_102 = "";

	private GameObject logic_uScript_SaveVariable_owner_102;

	private string logic_uScript_SaveVariable_uniqueId_102 = "";

	private bool logic_uScript_SaveVariable_Out_102 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_106 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_106;

	private GameObject logic_uScript_LoadBool_owner_106;

	private string logic_uScript_LoadBool_uniqueName_106 = "";

	private bool logic_uScript_LoadBool_Out_106 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_107 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_107 = "";

	private GameObject logic_uScript_SaveVariable_owner_107;

	private string logic_uScript_SaveVariable_uniqueId_107 = "";

	private bool logic_uScript_SaveVariable_Out_107 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_111 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_111;

	private GameObject logic_uScript_LoadBool_owner_111;

	private string logic_uScript_LoadBool_uniqueName_111 = "";

	private bool logic_uScript_LoadBool_Out_111 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_112 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_112 = "";

	private GameObject logic_uScript_SaveVariable_owner_112;

	private string logic_uScript_SaveVariable_uniqueId_112 = "";

	private bool logic_uScript_SaveVariable_Out_112 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_113;

	private bool logic_uScriptCon_CompareBool_True_113 = true;

	private bool logic_uScriptCon_CompareBool_False_113 = true;

	private uScript_IsOnScreenMessageStringValid logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_115 = new uScript_IsOnScreenMessageStringValid();

	private LocalisedString[] logic_uScript_IsOnScreenMessageStringValid_locString_115 = new LocalisedString[0];

	private bool logic_uScript_IsOnScreenMessageStringValid_True_115 = true;

	private bool logic_uScript_IsOnScreenMessageStringValid_False_115 = true;

	private uScript_IsOnScreenMessageStringValid logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_116 = new uScript_IsOnScreenMessageStringValid();

	private LocalisedString[] logic_uScript_IsOnScreenMessageStringValid_locString_116 = new LocalisedString[0];

	private bool logic_uScript_IsOnScreenMessageStringValid_True_116 = true;

	private bool logic_uScript_IsOnScreenMessageStringValid_False_116 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_117 = true;

	private uScript_ShowQuestLog logic_uScript_ShowQuestLog_uScript_ShowQuestLog_118 = new uScript_ShowQuestLog();

	private GameObject logic_uScript_ShowQuestLog_owner_118;

	private bool logic_uScript_ShowQuestLog_Out_118 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_120 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_120 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_124 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_124;

	private string logic_uScript_RemoveScenery_positionName_124 = "";

	private float logic_uScript_RemoveScenery_radius_124;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_124;

	private bool logic_uScript_RemoveScenery_Out_124 = true;

	[FriendlyName("Base Captured")]
	public event uScriptEventHandler Base_Captured;

	[FriendlyName("Base Destroyed")]
	public event uScriptEventHandler Base_Destroyed;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
		}
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
		}
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
		}
		if (null == owner_Connection_66 || !m_RegisteredForEvents)
		{
			owner_Connection_66 = parentGameObject;
		}
		if (null == owner_Connection_67 || !m_RegisteredForEvents)
		{
			owner_Connection_67 = parentGameObject;
		}
		if (null == owner_Connection_72 || !m_RegisteredForEvents)
		{
			owner_Connection_72 = parentGameObject;
		}
		if (null == owner_Connection_75 || !m_RegisteredForEvents)
		{
			owner_Connection_75 = parentGameObject;
		}
		if (null == owner_Connection_81 || !m_RegisteredForEvents)
		{
			owner_Connection_81 = parentGameObject;
			if (null != owner_Connection_81)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_81.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_81.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_80;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_80;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_80;
				}
			}
		}
		if (null == owner_Connection_95 || !m_RegisteredForEvents)
		{
			owner_Connection_95 = parentGameObject;
		}
		if (null == owner_Connection_100 || !m_RegisteredForEvents)
		{
			owner_Connection_100 = parentGameObject;
		}
		if (null == owner_Connection_105 || !m_RegisteredForEvents)
		{
			owner_Connection_105 = parentGameObject;
		}
		if (null == owner_Connection_110 || !m_RegisteredForEvents)
		{
			owner_Connection_110 = parentGameObject;
		}
		if (null == owner_Connection_119 || !m_RegisteredForEvents)
		{
			owner_Connection_119 = parentGameObject;
		}
		if (null == owner_Connection_123 || !m_RegisteredForEvents)
		{
			owner_Connection_123 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_81)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_81.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_81.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_80;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_80;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_80;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_81)
		{
			uScript_SaveLoad component = owner_Connection_81.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_80;
				component.LoadEvent -= Instance_LoadEvent_80;
				component.RestartEvent -= Instance_RestartEvent_80;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_7.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_18.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_21.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_34.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_38.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_40.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_50.SetParent(g);
		logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_52.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_58.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_59.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_60.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_61.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_64.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_71.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_74.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_82.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_86.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_96.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_97.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_101.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_102.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_106.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_107.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_111.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_112.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113.SetParent(g);
		logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_115.SetParent(g);
		logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_116.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117.SetParent(g);
		logic_uScript_ShowQuestLog_uScript_ShowQuestLog_118.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_120.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_124.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_1 = parentGameObject;
		owner_Connection_2 = parentGameObject;
		owner_Connection_66 = parentGameObject;
		owner_Connection_67 = parentGameObject;
		owner_Connection_72 = parentGameObject;
		owner_Connection_75 = parentGameObject;
		owner_Connection_81 = parentGameObject;
		owner_Connection_95 = parentGameObject;
		owner_Connection_100 = parentGameObject;
		owner_Connection_105 = parentGameObject;
		owner_Connection_110 = parentGameObject;
		owner_Connection_119 = parentGameObject;
		owner_Connection_123 = parentGameObject;
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
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_7.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_40.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_74.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_97.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_101.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_106.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_111.OnDisable();
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

	private void Instance_SaveEvent_80(object o, EventArgs e)
	{
		Relay_SaveEvent_80();
	}

	private void Instance_LoadEvent_80(object o, EventArgs e)
	{
		Relay_LoadEvent_80();
	}

	private void Instance_RestartEvent_80(object o, EventArgs e)
	{
		Relay_RestartEvent_80();
	}

	[FriendlyName("Capture Base", "")]
	public void Capture_Base([FriendlyName("clearSceneryPos", "")] string clearSceneryPos, [FriendlyName("clearSceneryRadius", "")] float clearSceneryRadius, [FriendlyName("baseData", "")] SpawnTechData[] baseData, [FriendlyName("guardianData", "")] SpawnTechData[] guardianData, [FriendlyName("harvesterData", "")] SpawnTechData[] harvesterData, [FriendlyName("msgBaseSpotted", "")] LocalisedString[] msgBaseSpotted, [FriendlyName("msgBaseCaptured", "")] LocalisedString[] msgBaseCaptured, [FriendlyName("msgBaseDestroyed", "")] LocalisedString[] msgBaseDestroyed, [FriendlyName("messageSpeaker", "")] ManOnScreenMessages.Speaker messageSpeaker)
	{
		external_125 = clearSceneryPos;
		external_126 = clearSceneryRadius;
		external_10 = baseData;
		external_11 = guardianData;
		external_12 = harvesterData;
		external_17 = msgBaseSpotted;
		external_9 = msgBaseCaptured;
		external_8 = msgBaseDestroyed;
		external_121 = messageSpeaker;
		Relay_True_44();
	}

	[FriendlyName("Destroy Base", "")]
	public void Destroy_Base([FriendlyName("clearSceneryPos", "")] string clearSceneryPos, [FriendlyName("clearSceneryRadius", "")] float clearSceneryRadius, [FriendlyName("baseData", "")] SpawnTechData[] baseData, [FriendlyName("guardianData", "")] SpawnTechData[] guardianData, [FriendlyName("harvesterData", "")] SpawnTechData[] harvesterData, [FriendlyName("msgBaseSpotted", "")] LocalisedString[] msgBaseSpotted, [FriendlyName("msgBaseCaptured", "")] LocalisedString[] msgBaseCaptured, [FriendlyName("msgBaseDestroyed", "")] LocalisedString[] msgBaseDestroyed, [FriendlyName("messageSpeaker", "")] ManOnScreenMessages.Speaker messageSpeaker)
	{
		external_125 = clearSceneryPos;
		external_126 = clearSceneryRadius;
		external_10 = baseData;
		external_11 = guardianData;
		external_12 = harvesterData;
		external_17 = msgBaseSpotted;
		external_9 = msgBaseCaptured;
		external_8 = msgBaseDestroyed;
		external_121 = messageSpeaker;
		Relay_False_44();
	}

	private void Relay_Connection_3()
	{
	}

	private void Relay_In_4()
	{
		int num = 0;
		Array array = external_8;
		if (logic_uScript_AddOnScreenMessage_locString_4.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_4, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_4, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_4 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.In(logic_uScript_AddOnScreenMessage_locString_4, logic_uScript_AddOnScreenMessage_msgPriority_4, logic_uScript_AddOnScreenMessage_holdMsg_4, logic_uScript_AddOnScreenMessage_tag_4, logic_uScript_AddOnScreenMessage_speaker_4, logic_uScript_AddOnScreenMessage_side_4);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.Out)
		{
			Relay_True_27();
		}
	}

	private void Relay_Connection_5()
	{
		if (this.Base_Destroyed != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Base_Destroyed(this, args);
		}
	}

	private void Relay_Connection_6()
	{
		if (this.Base_Captured != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Base_Captured(this, args);
		}
	}

	private void Relay_In_7()
	{
		int num = 0;
		Array array = external_9;
		if (logic_uScript_AddOnScreenMessage_locString_7.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_7, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_7, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_7 = external_121;
		logic_uScript_AddOnScreenMessage_Return_7 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_7.In(logic_uScript_AddOnScreenMessage_locString_7, logic_uScript_AddOnScreenMessage_msgPriority_7, logic_uScript_AddOnScreenMessage_holdMsg_7, logic_uScript_AddOnScreenMessage_tag_7, logic_uScript_AddOnScreenMessage_speaker_7, logic_uScript_AddOnScreenMessage_side_7);
	}

	private void Relay_Connection_8()
	{
	}

	private void Relay_Connection_9()
	{
	}

	private void Relay_Connection_10()
	{
	}

	private void Relay_Connection_11()
	{
	}

	private void Relay_Connection_12()
	{
	}

	private void Relay_In_16()
	{
		int num = 0;
		Array array = external_17;
		if (logic_uScript_AddOnScreenMessage_locString_16.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_16, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_16, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_16 = external_121;
		logic_uScript_AddOnScreenMessage_Return_16 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16.In(logic_uScript_AddOnScreenMessage_locString_16, logic_uScript_AddOnScreenMessage_msgPriority_16, logic_uScript_AddOnScreenMessage_holdMsg_16, logic_uScript_AddOnScreenMessage_tag_16, logic_uScript_AddOnScreenMessage_speaker_16, logic_uScript_AddOnScreenMessage_side_16);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_16.Out)
		{
			Relay_In_118();
		}
	}

	private void Relay_Connection_17()
	{
	}

	private void Relay_In_18()
	{
		logic_uScript_SetBatteryChargeAmount_tech_18 = local_BaseTech_Tank;
		logic_uScript_SetBatteryChargeAmount_chargeAmount_18 = local_46_System_Single;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_18.In(logic_uScript_SetBatteryChargeAmount_tech_18, logic_uScript_SetBatteryChargeAmount_chargeAmount_18);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_18.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_21()
	{
		logic_uScript_SetTankTeam_tank_21 = local_BaseTech_Tank;
		logic_uScript_SetTankTeam_team_21 = local_PlayerTeam_System_Int32;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_21.In(logic_uScript_SetTankTeam_tank_21, logic_uScript_SetTankTeam_team_21);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_21.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_22()
	{
		logic_uScriptCon_CompareBool_Bool_22 = local_MsgBaseSpottedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.In(logic_uScriptCon_CompareBool_Bool_22);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_22.False;
		if (num)
		{
			Relay_In_85();
		}
		if (flag)
		{
			Relay_True_24();
		}
	}

	private void Relay_True_24()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.True(out logic_uScriptAct_SetBool_Target_24);
		local_MsgBaseSpottedShown_System_Boolean = logic_uScriptAct_SetBool_Target_24;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_24.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_False_24()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.False(out logic_uScriptAct_SetBool_Target_24);
		local_MsgBaseSpottedShown_System_Boolean = logic_uScriptAct_SetBool_Target_24;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_24.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_True_26()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.True(out logic_uScriptAct_SetBool_Target_26);
		local_MsgBaseSpottedShown_System_Boolean = logic_uScriptAct_SetBool_Target_26;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_26.Out)
		{
			Relay_False_38();
		}
	}

	private void Relay_False_26()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.False(out logic_uScriptAct_SetBool_Target_26);
		local_MsgBaseSpottedShown_System_Boolean = logic_uScriptAct_SetBool_Target_26;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_26.Out)
		{
			Relay_False_38();
		}
	}

	private void Relay_True_27()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.True(out logic_uScriptAct_SetBool_Target_27);
		local_BaseDestroyed_System_Boolean = logic_uScriptAct_SetBool_Target_27;
	}

	private void Relay_False_27()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.False(out logic_uScriptAct_SetBool_Target_27);
		local_BaseDestroyed_System_Boolean = logic_uScriptAct_SetBool_Target_27;
	}

	private void Relay_True_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.True(out logic_uScriptAct_SetBool_Target_29);
		local_BaseCaptured_System_Boolean = logic_uScriptAct_SetBool_Target_29;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_29.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_False_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.False(out logic_uScriptAct_SetBool_Target_29);
		local_BaseCaptured_System_Boolean = logic_uScriptAct_SetBool_Target_29;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_29.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_31()
	{
		logic_uScriptCon_CompareBool_Bool_31 = local_BaseCaptured_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.In(logic_uScriptCon_CompareBool_Bool_31);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.False;
		if (num)
		{
			Relay_Connection_6();
		}
		if (flag)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_34()
	{
		logic_uScriptCon_CompareBool_Bool_34 = local_BaseDestroyed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_34.In(logic_uScriptCon_CompareBool_Bool_34);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_34.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_34.False;
		if (num)
		{
			Relay_Connection_5();
		}
		if (flag)
		{
			Relay_In_62();
		}
	}

	private void Relay_True_38()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_38.True(out logic_uScriptAct_SetBool_Target_38);
		local_BaseCaptured_System_Boolean = logic_uScriptAct_SetBool_Target_38;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_38.Out)
		{
			Relay_False_39();
		}
	}

	private void Relay_False_38()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_38.False(out logic_uScriptAct_SetBool_Target_38);
		local_BaseCaptured_System_Boolean = logic_uScriptAct_SetBool_Target_38;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_38.Out)
		{
			Relay_False_39();
		}
	}

	private void Relay_True_39()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.True(out logic_uScriptAct_SetBool_Target_39);
		local_BaseDestroyed_System_Boolean = logic_uScriptAct_SetBool_Target_39;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_39.Out)
		{
			Relay_False_92();
		}
	}

	private void Relay_False_39()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.False(out logic_uScriptAct_SetBool_Target_39);
		local_BaseDestroyed_System_Boolean = logic_uScriptAct_SetBool_Target_39;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_39.Out)
		{
			Relay_False_92();
		}
	}

	private void Relay_In_40()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_40 = local_BaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_40 = local_DistNearBase_System_Single;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_40.In(logic_uScript_IsPlayerInRangeOfTech_tech_40, logic_uScript_IsPlayerInRangeOfTech_range_40, logic_uScript_IsPlayerInRangeOfTech_techs_40);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_40.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_40.OutOfRange;
		if (inRange)
		{
			Relay_In_22();
		}
		if (outOfRange)
		{
			Relay_In_18();
		}
	}

	private void Relay_Connection_42()
	{
	}

	private void Relay_True_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.True(out logic_uScriptAct_SetBool_Target_44);
		local_CaptureObjective_System_Boolean = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_False_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.False(out logic_uScriptAct_SetBool_Target_44);
		local_CaptureObjective_System_Boolean = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_50()
	{
		int num = 0;
		Array array = local_HarvesterTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_50.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_50, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_50, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_team_50 = local_PlayerTeam_System_Int32;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_50.In(ref logic_uScript_SetTechsTeam_techs_50, logic_uScript_SetTechsTeam_team_50);
		local_HarvesterTechs_TankArray = logic_uScript_SetTechsTeam_techs_50;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_50.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_In_52()
	{
		logic_uScript_GetPlayerTeam_Return_52 = logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_52.In();
		local_PlayerTeam_System_Int32 = logic_uScript_GetPlayerTeam_Return_52;
		if (logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_52.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_InitialSpawn_58()
	{
		int num = 0;
		Array array = external_10;
		if (logic_uScript_SpawnTechsFromData_spawnData_58.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_58, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_58, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_58 = owner_Connection_0;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_58.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_58, logic_uScript_SpawnTechsFromData_ownerNode_58, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_58);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_58.Out)
		{
			Relay_InitialSpawn_59();
		}
	}

	private void Relay_InitialSpawn_59()
	{
		int num = 0;
		Array array = external_11;
		if (logic_uScript_SpawnTechsFromData_spawnData_59.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_59, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_59, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_59 = owner_Connection_1;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_59.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_59, logic_uScript_SpawnTechsFromData_ownerNode_59, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_59);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_59.Out)
		{
			Relay_In_124();
		}
	}

	private void Relay_InitialSpawn_60()
	{
		int num = 0;
		Array array = external_12;
		if (logic_uScript_SpawnTechsFromData_spawnData_60.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_60, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_60, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_60 = owner_Connection_2;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_60.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_60, logic_uScript_SpawnTechsFromData_ownerNode_60, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_60);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_60.Out)
		{
			Relay_In_64();
		}
	}

	private void Relay_In_61()
	{
		int num = 0;
		Array array = external_11;
		if (logic_uScript_GetAndCheckTechs_techData_61.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_61, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_61, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_61 = owner_Connection_67;
		int num2 = 0;
		Array array2 = local_GuardianTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_61.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_61, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_61, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_61 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_61.In(logic_uScript_GetAndCheckTechs_techData_61, logic_uScript_GetAndCheckTechs_ownerNode_61, ref logic_uScript_GetAndCheckTechs_techs_61);
		local_GuardianTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_61;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_61.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_61.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_61.AllDead;
		if (allAlive)
		{
			Relay_In_74();
		}
		if (someAlive)
		{
			Relay_In_74();
		}
		if (allDead)
		{
			Relay_True_29();
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
			Relay_In_64();
		}
		if (flag)
		{
			Relay_True_73();
		}
	}

	private void Relay_In_64()
	{
		int num = 0;
		Array array = external_10;
		if (logic_uScript_GetAndCheckTechs_techData_64.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_64, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_64, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_64 = owner_Connection_66;
		int num2 = 0;
		Array array2 = local_90_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_64.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_64, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_64, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_64 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_64.In(logic_uScript_GetAndCheckTechs_techData_64, logic_uScript_GetAndCheckTechs_ownerNode_64, ref logic_uScript_GetAndCheckTechs_techs_64);
		local_90_TankArray = logic_uScript_GetAndCheckTechs_techs_64;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_64.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_64.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_64.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_82();
		}
		if (someAlive)
		{
			Relay_AtIndex_82();
		}
		if (allDead)
		{
			Relay_In_4();
		}
	}

	private void Relay_In_71()
	{
		logic_uScriptCon_CompareBool_Bool_71 = local_CaptureObjective_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_71.In(logic_uScriptCon_CompareBool_Bool_71);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_71.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_71.False;
		if (num)
		{
			Relay_In_40();
		}
		if (flag)
		{
			Relay_In_86();
		}
	}

	private void Relay_True_73()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.True(out logic_uScriptAct_SetBool_Target_73);
		local_EnemiesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_73;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_73.Out)
		{
			Relay_InitialSpawn_58();
		}
	}

	private void Relay_False_73()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.False(out logic_uScriptAct_SetBool_Target_73);
		local_EnemiesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_73;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_73.Out)
		{
			Relay_InitialSpawn_58();
		}
	}

	private void Relay_In_74()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_74 = owner_Connection_75;
		int num = 0;
		Array array = local_GuardianTechs_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_74.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_74, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_74, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_74 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_74.In(logic_uScript_SetOneTechAsEncounterTarget_owner_74, logic_uScript_SetOneTechAsEncounterTarget_techs_74);
	}

	private void Relay_SaveEvent_80()
	{
		Relay_In_96();
	}

	private void Relay_LoadEvent_80()
	{
		Relay_In_97();
	}

	private void Relay_RestartEvent_80()
	{
		Relay_False_26();
	}

	private void Relay_AtIndex_82()
	{
		int num = 0;
		Array array = local_90_TankArray;
		if (logic_uScript_AccessListTech_techList_82.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_82, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_82, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_82.AtIndex(ref logic_uScript_AccessListTech_techList_82, logic_uScript_AccessListTech_index_82, out logic_uScript_AccessListTech_value_82);
		local_90_TankArray = logic_uScript_AccessListTech_techList_82;
		local_BaseTech_Tank = logic_uScript_AccessListTech_value_82;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_82.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_85()
	{
		logic_uScriptCon_CompareBool_Bool_85 = local_CaptureObjective_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85.In(logic_uScriptCon_CompareBool_Bool_85);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85.True)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_86()
	{
		logic_uScript_SetEncounterTarget_owner_86 = owner_Connection_72;
		logic_uScript_SetEncounterTarget_visibleObject_86 = local_BaseTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_86.In(logic_uScript_SetEncounterTarget_owner_86, logic_uScript_SetEncounterTarget_visibleObject_86);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_86.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_True_92()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.True(out logic_uScriptAct_SetBool_Target_92);
		local_EnemiesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_92;
	}

	private void Relay_False_92()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.False(out logic_uScriptAct_SetBool_Target_92);
		local_EnemiesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_92;
	}

	private void Relay_In_96()
	{
		logic_uScript_SaveVariable_variable_96 = local_MsgBaseSpottedShown_System_Boolean;
		logic_uScript_SaveVariable_owner_96 = owner_Connection_95;
		logic_uScript_SaveVariable_uniqueId_96 = local_MsgBaseSpottedShownID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_96.In(logic_uScript_SaveVariable_variable_96, logic_uScript_SaveVariable_owner_96, logic_uScript_SaveVariable_uniqueId_96);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_96.Out)
		{
			Relay_In_102();
		}
	}

	private void Relay_In_97()
	{
		logic_uScript_LoadBool_data_97 = local_MsgBaseSpottedShown_System_Boolean;
		logic_uScript_LoadBool_owner_97 = owner_Connection_95;
		logic_uScript_LoadBool_uniqueName_97 = local_MsgBaseSpottedShownID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_97.In(ref logic_uScript_LoadBool_data_97, logic_uScript_LoadBool_owner_97, logic_uScript_LoadBool_uniqueName_97);
		local_MsgBaseSpottedShown_System_Boolean = logic_uScript_LoadBool_data_97;
		if (logic_uScript_LoadBool_uScript_LoadBool_97.Out)
		{
			Relay_In_101();
		}
	}

	private void Relay_In_101()
	{
		logic_uScript_LoadBool_data_101 = local_BaseCaptured_System_Boolean;
		logic_uScript_LoadBool_owner_101 = owner_Connection_100;
		logic_uScript_LoadBool_uniqueName_101 = local_BaseCapturedID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_101.In(ref logic_uScript_LoadBool_data_101, logic_uScript_LoadBool_owner_101, logic_uScript_LoadBool_uniqueName_101);
		local_BaseCaptured_System_Boolean = logic_uScript_LoadBool_data_101;
		if (logic_uScript_LoadBool_uScript_LoadBool_101.Out)
		{
			Relay_In_106();
		}
	}

	private void Relay_In_102()
	{
		logic_uScript_SaveVariable_variable_102 = local_BaseCaptured_System_Boolean;
		logic_uScript_SaveVariable_owner_102 = owner_Connection_100;
		logic_uScript_SaveVariable_uniqueId_102 = local_BaseCapturedID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_102.In(logic_uScript_SaveVariable_variable_102, logic_uScript_SaveVariable_owner_102, logic_uScript_SaveVariable_uniqueId_102);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_102.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_106()
	{
		logic_uScript_LoadBool_data_106 = local_BaseDestroyed_System_Boolean;
		logic_uScript_LoadBool_owner_106 = owner_Connection_105;
		logic_uScript_LoadBool_uniqueName_106 = local_BaseDestroyedID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_106.In(ref logic_uScript_LoadBool_data_106, logic_uScript_LoadBool_owner_106, logic_uScript_LoadBool_uniqueName_106);
		local_BaseDestroyed_System_Boolean = logic_uScript_LoadBool_data_106;
		if (logic_uScript_LoadBool_uScript_LoadBool_106.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_107()
	{
		logic_uScript_SaveVariable_variable_107 = local_BaseDestroyed_System_Boolean;
		logic_uScript_SaveVariable_owner_107 = owner_Connection_105;
		logic_uScript_SaveVariable_uniqueId_107 = local_BaseDestroyedID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_107.In(logic_uScript_SaveVariable_variable_107, logic_uScript_SaveVariable_owner_107, logic_uScript_SaveVariable_uniqueId_107);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_107.Out)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_111()
	{
		logic_uScript_LoadBool_data_111 = local_EnemiesSpawned_System_Boolean;
		logic_uScript_LoadBool_owner_111 = owner_Connection_110;
		logic_uScript_LoadBool_uniqueName_111 = local_EnemiesSpawnedID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_111.In(ref logic_uScript_LoadBool_data_111, logic_uScript_LoadBool_owner_111, logic_uScript_LoadBool_uniqueName_111);
		local_EnemiesSpawned_System_Boolean = logic_uScript_LoadBool_data_111;
	}

	private void Relay_In_112()
	{
		logic_uScript_SaveVariable_variable_112 = local_EnemiesSpawned_System_Boolean;
		logic_uScript_SaveVariable_owner_112 = owner_Connection_110;
		logic_uScript_SaveVariable_uniqueId_112 = local_EnemiesSpawnedID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_112.In(logic_uScript_SaveVariable_variable_112, logic_uScript_SaveVariable_owner_112, logic_uScript_SaveVariable_uniqueId_112);
	}

	private void Relay_In_113()
	{
		logic_uScriptCon_CompareBool_Bool_113 = local_CaptureObjective_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113.In(logic_uScriptCon_CompareBool_Bool_113);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113.False;
		if (num)
		{
			Relay_In_64();
		}
		if (flag)
		{
			Relay_In_120();
		}
	}

	private void Relay_In_115()
	{
		int num = 0;
		Array array = external_9;
		if (logic_uScript_IsOnScreenMessageStringValid_locString_115.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_IsOnScreenMessageStringValid_locString_115, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_IsOnScreenMessageStringValid_locString_115, num, array.Length);
		num += array.Length;
		logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_115.In(logic_uScript_IsOnScreenMessageStringValid_locString_115);
		if (logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_115.True)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_116()
	{
		int num = 0;
		Array array = external_17;
		if (logic_uScript_IsOnScreenMessageStringValid_locString_116.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_IsOnScreenMessageStringValid_locString_116, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_IsOnScreenMessageStringValid_locString_116, num, array.Length);
		num += array.Length;
		logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_116.In(logic_uScript_IsOnScreenMessageStringValid_locString_116);
		bool num2 = logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_116.True;
		bool flag = logic_uScript_IsOnScreenMessageStringValid_uScript_IsOnScreenMessageStringValid_116.False;
		if (num2)
		{
			Relay_In_16();
		}
		if (flag)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_117()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117.Out)
		{
			Relay_In_118();
		}
	}

	private void Relay_In_118()
	{
		logic_uScript_ShowQuestLog_owner_118 = owner_Connection_119;
		logic_uScript_ShowQuestLog_uScript_ShowQuestLog_118.In(logic_uScript_ShowQuestLog_owner_118);
		if (logic_uScript_ShowQuestLog_uScript_ShowQuestLog_118.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_120()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_120.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_120.Out)
		{
			Relay_In_64();
		}
	}

	private void Relay_Connection_121()
	{
	}

	private void Relay_In_124()
	{
		logic_uScript_RemoveScenery_ownerNode_124 = owner_Connection_123;
		logic_uScript_RemoveScenery_positionName_124 = external_125;
		logic_uScript_RemoveScenery_radius_124 = external_126;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_124.In(logic_uScript_RemoveScenery_ownerNode_124, logic_uScript_RemoveScenery_positionName_124, logic_uScript_RemoveScenery_radius_124, logic_uScript_RemoveScenery_preventChunksSpawning_124);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_124.Out)
		{
			Relay_In_113();
		}
	}

	private void Relay_Connection_125()
	{
	}

	private void Relay_Connection_126()
	{
	}
}
