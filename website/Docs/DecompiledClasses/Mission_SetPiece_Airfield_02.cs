using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_DefeatEnemyTechs", "")]
public class Mission_SetPiece_Airfield_02 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool _DEBUGIgnoreMoneyCheck;

	public BlockTypes[] discoverableBlockTypesOnVehicle = new BlockTypes[0];

	public BlockTypes interactableBlockType;

	private int local_38_System_Int32;

	private bool local_70_System_Boolean;

	private TankBlock local_75_TankBlock;

	private int local_CurrentMoney_System_Int32;

	private bool local_HasEnoughMoney_System_Boolean;

	private Tank[] local_NPCPaymentPoints_TankArray = new Tank[0];

	private Tank local_PaymentPointTech_Tank;

	private bool local_RepeatWaitTime_System_Boolean;

	private bool local_TechSetUp_System_Boolean;

	private bool local_TechSpawned_System_Boolean;

	private TankBlock local_TerminalBlock_TankBlock;

	private bool local_VehiclePurchased_System_Boolean;

	private bool local_VehicleSetup_System_Boolean;

	private bool local_WaitingOnPrompt_System_Boolean;

	public LocalisedString msgPromptAccept;

	public LocalisedString msgPromptDecline;

	public LocalisedString msgPromptNoMoney;

	public LocalisedString msgPromptTextBlackbird;

	[Multiline(3)]
	public string NearNPCTrigger = "";

	public SpawnTechData[] NPCPaymentPoint = new SpawnTechData[0];

	public int vehicleCost;

	public SpawnTechData[] vehicleSpawnData = new SpawnTechData[0];

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_31;

	private GameObject owner_Connection_37;

	private GameObject owner_Connection_64;

	private GameObject owner_Connection_84;

	private GameObject owner_Connection_94;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_2 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_2;

	private bool logic_uScriptAct_SetBool_Out_2 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_2 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_2 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_7;

	private bool logic_uScriptCon_CompareBool_True_7 = true;

	private bool logic_uScriptCon_CompareBool_False_7 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_9;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_9 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_9 = "TechSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_12;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_12 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_12 = "VehiclePurchased";

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_14 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_14 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_14 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_14 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_14 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_16;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_16 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_16 = "HasEnoughMoney";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_18;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_18 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_18 = "WaitingOnPrompt";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_20;

	private bool logic_uScriptCon_CompareBool_True_20 = true;

	private bool logic_uScriptCon_CompareBool_False_20 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_22 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_22;

	private bool logic_uScriptAct_SetBool_Out_22 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_22 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_22 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_24;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_24 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_24 = "TechSetUp";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_25 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_25;

	private bool logic_uScriptCon_CompareBool_True_25 = true;

	private bool logic_uScriptCon_CompareBool_False_25 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_28;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_28 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_28 = "VehicleSetup";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_30 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_30 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_30;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_30 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_30;

	private bool logic_uScript_SpawnTechsFromData_Out_30 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_32 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_32 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_32;

	private bool logic_uScript_SetTankInvulnerable_Out_32 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_35 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_35 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_35;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_35 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_35;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_35 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_35 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_35 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_35 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_36 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_36 = new Tank[0];

	private int logic_uScript_AccessListTech_index_36;

	private Tank logic_uScript_AccessListTech_value_36;

	private bool logic_uScript_AccessListTech_Out_36 = true;

	private uScriptAct_MultiplyInt_v2 logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_39 = new uScriptAct_MultiplyInt_v2();

	private int logic_uScriptAct_MultiplyInt_v2_A_39;

	private int logic_uScriptAct_MultiplyInt_v2_B_39 = -1;

	private int logic_uScriptAct_MultiplyInt_v2_IntResult_39;

	private float logic_uScriptAct_MultiplyInt_v2_FloatResult_39;

	private bool logic_uScriptAct_MultiplyInt_v2_Out_39 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_42;

	private bool logic_uScriptCon_CompareBool_True_42 = true;

	private bool logic_uScriptCon_CompareBool_False_42 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_46;

	private bool logic_uScriptCon_CompareBool_True_46 = true;

	private bool logic_uScriptCon_CompareBool_False_46 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_49 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_49;

	private bool logic_uScriptAct_SetBool_Out_49 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_49 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_49 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_51 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_51;

	private bool logic_uScriptCon_CompareBool_True_51 = true;

	private bool logic_uScriptCon_CompareBool_False_51 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_52 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_52;

	private int logic_uScriptCon_CompareInt_B_52;

	private bool logic_uScriptCon_CompareInt_GreaterThan_52 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_52 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_52 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_52 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_52 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_52 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_54 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_54;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_54 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_56 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_56;

	private bool logic_uScriptAct_SetBool_Out_56 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_56 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_56 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_58;

	private bool logic_uScriptCon_CompareBool_True_58 = true;

	private bool logic_uScriptCon_CompareBool_False_58 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_59;

	private bool logic_uScriptCon_CompareBool_True_59 = true;

	private bool logic_uScriptCon_CompareBool_False_59 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_60 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_60;

	private TankBlock logic_uScript_CompareBlock_B_60;

	private bool logic_uScript_CompareBlock_EqualTo_60 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_60 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_62;

	private bool logic_uScriptCon_CompareBool_True_62 = true;

	private bool logic_uScriptCon_CompareBool_False_62 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_65 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_65;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_65;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_65;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_65;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_65;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_65;

	private bool logic_uScript_MissionPromptBlock_Show_Out_65 = true;

	private uScript_AddMoney logic_uScript_AddMoney_uScript_AddMoney_66 = new uScript_AddMoney();

	private int logic_uScript_AddMoney_amount_66;

	private bool logic_uScript_AddMoney_Out_66 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_67 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_67;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_67;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_67;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_67;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_67;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_67;

	private bool logic_uScript_MissionPromptBlock_Show_Out_67 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_69 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_69;

	private bool logic_uScriptAct_SetBool_Out_69 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_69 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_69 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_73;

	private bool logic_uScriptCon_CompareBool_True_73 = true;

	private bool logic_uScriptCon_CompareBool_False_73 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_78 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_78;

	private bool logic_uScriptAct_SetBool_Out_78 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_78 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_78 = true;

	private uScript_DiscoverBlocks logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_80 = new uScript_DiscoverBlocks();

	private BlockTypes[] logic_uScript_DiscoverBlocks_blockTypes_80 = new BlockTypes[0];

	private bool logic_uScript_DiscoverBlocks_Out_80 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_83 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_83 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_83;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_83 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_83;

	private bool logic_uScript_SpawnTechsFromData_Out_83 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_87 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_87;

	private BlockTypes logic_uScript_GetTankBlock_blockType_87;

	private TankBlock logic_uScript_GetTankBlock_Return_87;

	private bool logic_uScript_GetTankBlock_Out_87 = true;

	private bool logic_uScript_GetTankBlock_Returned_87 = true;

	private bool logic_uScript_GetTankBlock_NotFound_87 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_90 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_91 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_91;

	private bool logic_uScriptAct_SetBool_Out_91 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_91 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_91 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_93 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_93;

	private object logic_uScript_SetEncounterTarget_visibleObject_93 = "";

	private bool logic_uScript_SetEncounterTarget_Out_93 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_96 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_96 = 7f;

	private bool logic_uScript_Wait_repeat_96;

	private bool logic_uScript_Wait_Waited_96 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_98 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_98;

	private bool logic_uScriptAct_SetBool_Out_98 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_98 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_98 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_100 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_100;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_100 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_103 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_103;

	private bool logic_uScriptAct_SetBool_Out_103 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_103 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_103 = true;

	private TankBlock event_UnityEngine_GameObject_TankBlock_41;

	private bool event_UnityEngine_GameObject_Accepted_41;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
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
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
			if (null != owner_Connection_6)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_6.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_6.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_5;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_5;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_5;
				}
			}
		}
		if (null == owner_Connection_31 || !m_RegisteredForEvents)
		{
			owner_Connection_31 = parentGameObject;
		}
		if (null == owner_Connection_37 || !m_RegisteredForEvents)
		{
			owner_Connection_37 = parentGameObject;
		}
		if (null == owner_Connection_64 || !m_RegisteredForEvents)
		{
			owner_Connection_64 = parentGameObject;
			if (null != owner_Connection_64)
			{
				uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_64.GetComponent<uScript_MissionPromptBlock_OnResult>();
				if (null == uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2 = owner_Connection_64.AddComponent<uScript_MissionPromptBlock_OnResult>();
				}
				if (null != uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_41;
				}
			}
		}
		if (null == owner_Connection_84 || !m_RegisteredForEvents)
		{
			owner_Connection_84 = parentGameObject;
		}
		if (null == owner_Connection_94 || !m_RegisteredForEvents)
		{
			owner_Connection_94 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
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
		if (!m_RegisteredForEvents && null != owner_Connection_6)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_6.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_6.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_5;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_5;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_5;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_64)
		{
			uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_64.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null == uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2 = owner_Connection_64.AddComponent<uScript_MissionPromptBlock_OnResult>();
			}
			if (null != uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_41;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_3)
		{
			uScript_SaveLoad component = owner_Connection_3.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_4;
				component.LoadEvent -= Instance_LoadEvent_4;
				component.RestartEvent -= Instance_RestartEvent_4;
			}
		}
		if (null != owner_Connection_6)
		{
			uScript_EncounterUpdate component2 = owner_Connection_6.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_5;
				component2.OnSuspend -= Instance_OnSuspend_5;
				component2.OnResume -= Instance_OnResume_5;
			}
		}
		if (null != owner_Connection_64)
		{
			uScript_MissionPromptBlock_OnResult component3 = owner_Connection_64.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null != component3)
			{
				component3.ResponseEvent -= Instance_ResponseEvent_41;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_14.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_25.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_30.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_32.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_35.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_36.SetParent(g);
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_39.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_51.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_52.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_54.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_56.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_60.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_65.SetParent(g);
		logic_uScript_AddMoney_uScript_AddMoney_66.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_67.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_69.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.SetParent(g);
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_80.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_83.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_87.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_91.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_93.SetParent(g);
		logic_uScript_Wait_uScript_Wait_96.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_100.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_103.SetParent(g);
		owner_Connection_3 = parentGameObject;
		owner_Connection_6 = parentGameObject;
		owner_Connection_31 = parentGameObject;
		owner_Connection_37 = parentGameObject;
		owner_Connection_64 = parentGameObject;
		owner_Connection_84 = parentGameObject;
		owner_Connection_94 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out += SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out += SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save_Out += SubGraph_SaveLoadBool_Save_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load_Out += SubGraph_SaveLoadBool_Load_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Save_Out += SubGraph_SaveLoadBool_Save_Out_16;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Load_Out += SubGraph_SaveLoadBool_Load_Out_16;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_16;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save_Out += SubGraph_SaveLoadBool_Save_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load_Out += SubGraph_SaveLoadBool_Load_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Save_Out += SubGraph_SaveLoadBool_Save_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Load_Out += SubGraph_SaveLoadBool_Load_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Save_Out += SubGraph_SaveLoadBool_Save_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Load_Out += SubGraph_SaveLoadBool_Load_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_28;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_32.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_87.OnDisable();
		logic_uScript_Wait_uScript_Wait_96.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out -= SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out -= SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save_Out -= SubGraph_SaveLoadBool_Save_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load_Out -= SubGraph_SaveLoadBool_Load_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Save_Out -= SubGraph_SaveLoadBool_Save_Out_16;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Load_Out -= SubGraph_SaveLoadBool_Load_Out_16;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_16;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save_Out -= SubGraph_SaveLoadBool_Save_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load_Out -= SubGraph_SaveLoadBool_Load_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Save_Out -= SubGraph_SaveLoadBool_Save_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Load_Out -= SubGraph_SaveLoadBool_Load_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Save_Out -= SubGraph_SaveLoadBool_Save_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Load_Out -= SubGraph_SaveLoadBool_Load_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_28;
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

	private void Instance_OnUpdate_5(object o, EventArgs e)
	{
		Relay_OnUpdate_5();
	}

	private void Instance_OnSuspend_5(object o, EventArgs e)
	{
		Relay_OnSuspend_5();
	}

	private void Instance_OnResume_5(object o, EventArgs e)
	{
		Relay_OnResume_5();
	}

	private void Instance_ResponseEvent_41(object o, uScript_MissionPromptBlock_OnResult.PromptResultEventArgs e)
	{
		event_UnityEngine_GameObject_TankBlock_41 = e.TankBlock;
		event_UnityEngine_GameObject_Accepted_41 = e.Accepted;
		Relay_ResponseEvent_41();
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

	private void SubGraph_SaveLoadBool_Save_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Save_Out_12();
	}

	private void SubGraph_SaveLoadBool_Load_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Load_Out_12();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Restart_Out_12();
	}

	private void SubGraph_SaveLoadBool_Save_Out_16(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_16;
		Relay_Save_Out_16();
	}

	private void SubGraph_SaveLoadBool_Load_Out_16(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_16;
		Relay_Load_Out_16();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_16(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_16;
		Relay_Restart_Out_16();
	}

	private void SubGraph_SaveLoadBool_Save_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_WaitingOnPrompt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Save_Out_18();
	}

	private void SubGraph_SaveLoadBool_Load_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_WaitingOnPrompt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Load_Out_18();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_WaitingOnPrompt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Restart_Out_18();
	}

	private void SubGraph_SaveLoadBool_Save_Out_24(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = e.boolean;
		local_TechSetUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_24;
		Relay_Save_Out_24();
	}

	private void SubGraph_SaveLoadBool_Load_Out_24(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = e.boolean;
		local_TechSetUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_24;
		Relay_Load_Out_24();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_24(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = e.boolean;
		local_TechSetUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_24;
		Relay_Restart_Out_24();
	}

	private void SubGraph_SaveLoadBool_Save_Out_28(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = e.boolean;
		local_VehicleSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_28;
		Relay_Save_Out_28();
	}

	private void SubGraph_SaveLoadBool_Load_Out_28(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = e.boolean;
		local_VehicleSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_28;
		Relay_Load_Out_28();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_28(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = e.boolean;
		local_VehicleSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_28;
		Relay_Restart_Out_28();
	}

	private void Relay_True_2()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.True(out logic_uScriptAct_SetBool_Target_2);
		local_TechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_2;
	}

	private void Relay_False_2()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.False(out logic_uScriptAct_SetBool_Target_2);
		local_TechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_2;
	}

	private void Relay_SaveEvent_4()
	{
		Relay_Save_9();
	}

	private void Relay_LoadEvent_4()
	{
		Relay_Load_9();
	}

	private void Relay_RestartEvent_4()
	{
		Relay_Set_False_9();
	}

	private void Relay_OnUpdate_5()
	{
		Relay_In_7();
	}

	private void Relay_OnSuspend_5()
	{
	}

	private void Relay_OnResume_5()
	{
	}

	private void Relay_In_7()
	{
		logic_uScriptCon_CompareBool_Bool_7 = local_TechSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.In(logic_uScriptCon_CompareBool_Bool_7);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.False;
		if (num)
		{
			Relay_In_20();
		}
		if (flag)
		{
			Relay_InitialSpawn_30();
		}
	}

	private void Relay_Save_Out_9()
	{
		Relay_Save_12();
	}

	private void Relay_Load_Out_9()
	{
		Relay_Load_12();
	}

	private void Relay_Restart_Out_9()
	{
		Relay_Set_False_12();
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

	private void Relay_Save_Out_12()
	{
		Relay_Save_16();
	}

	private void Relay_Load_Out_12()
	{
		Relay_Load_16();
	}

	private void Relay_Restart_Out_12()
	{
		Relay_Set_False_16();
	}

	private void Relay_Save_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Load_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Set_True_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Set_False_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_In_14()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_14 = NearNPCTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_14.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_14);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_14.InRange)
		{
			Relay_In_62();
		}
	}

	private void Relay_Save_Out_16()
	{
		Relay_Save_18();
	}

	private void Relay_Load_Out_16()
	{
		Relay_Load_18();
	}

	private void Relay_Restart_Out_16()
	{
		Relay_Set_False_18();
	}

	private void Relay_Save_16()
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_16 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Save(ref logic_SubGraph_SaveLoadBool_boolean_16, logic_SubGraph_SaveLoadBool_boolAsVariable_16, logic_SubGraph_SaveLoadBool_uniqueID_16);
	}

	private void Relay_Load_16()
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_16 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Load(ref logic_SubGraph_SaveLoadBool_boolean_16, logic_SubGraph_SaveLoadBool_boolAsVariable_16, logic_SubGraph_SaveLoadBool_uniqueID_16);
	}

	private void Relay_Set_True_16()
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_16 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_16, logic_SubGraph_SaveLoadBool_boolAsVariable_16, logic_SubGraph_SaveLoadBool_uniqueID_16);
	}

	private void Relay_Set_False_16()
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_16 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_16, logic_SubGraph_SaveLoadBool_boolAsVariable_16, logic_SubGraph_SaveLoadBool_uniqueID_16);
	}

	private void Relay_Save_Out_18()
	{
		Relay_Save_24();
	}

	private void Relay_Load_Out_18()
	{
		Relay_Load_24();
	}

	private void Relay_Restart_Out_18()
	{
		Relay_Set_False_24();
	}

	private void Relay_Save_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Load_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Set_True_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Set_False_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_In_20()
	{
		logic_uScriptCon_CompareBool_Bool_20 = local_TechSetUp_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.In(logic_uScriptCon_CompareBool_Bool_20);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.False;
		if (num)
		{
			Relay_In_25();
		}
		if (flag)
		{
			Relay_In_35();
		}
	}

	private void Relay_True_22()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.True(out logic_uScriptAct_SetBool_Target_22);
		local_TechSetUp_System_Boolean = logic_uScriptAct_SetBool_Target_22;
	}

	private void Relay_False_22()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.False(out logic_uScriptAct_SetBool_Target_22);
		local_TechSetUp_System_Boolean = logic_uScriptAct_SetBool_Target_22;
	}

	private void Relay_Save_Out_24()
	{
		Relay_Save_28();
	}

	private void Relay_Load_Out_24()
	{
		Relay_Load_28();
	}

	private void Relay_Restart_Out_24()
	{
		Relay_Set_False_28();
	}

	private void Relay_Save_24()
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_24 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Save(ref logic_SubGraph_SaveLoadBool_boolean_24, logic_SubGraph_SaveLoadBool_boolAsVariable_24, logic_SubGraph_SaveLoadBool_uniqueID_24);
	}

	private void Relay_Load_24()
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_24 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Load(ref logic_SubGraph_SaveLoadBool_boolean_24, logic_SubGraph_SaveLoadBool_boolAsVariable_24, logic_SubGraph_SaveLoadBool_uniqueID_24);
	}

	private void Relay_Set_True_24()
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_24 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_24, logic_SubGraph_SaveLoadBool_boolAsVariable_24, logic_SubGraph_SaveLoadBool_uniqueID_24);
	}

	private void Relay_Set_False_24()
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_24 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_24, logic_SubGraph_SaveLoadBool_boolAsVariable_24, logic_SubGraph_SaveLoadBool_uniqueID_24);
	}

	private void Relay_In_25()
	{
		logic_uScriptCon_CompareBool_Bool_25 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_25.In(logic_uScriptCon_CompareBool_Bool_25);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_25.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_25.False;
		if (num)
		{
			Relay_In_100();
		}
		if (flag)
		{
			Relay_In_14();
		}
	}

	private void Relay_Save_Out_28()
	{
	}

	private void Relay_Load_Out_28()
	{
	}

	private void Relay_Restart_Out_28()
	{
	}

	private void Relay_Save_28()
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_28 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Save(ref logic_SubGraph_SaveLoadBool_boolean_28, logic_SubGraph_SaveLoadBool_boolAsVariable_28, logic_SubGraph_SaveLoadBool_uniqueID_28);
	}

	private void Relay_Load_28()
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_28 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Load(ref logic_SubGraph_SaveLoadBool_boolean_28, logic_SubGraph_SaveLoadBool_boolAsVariable_28, logic_SubGraph_SaveLoadBool_uniqueID_28);
	}

	private void Relay_Set_True_28()
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_28 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_28, logic_SubGraph_SaveLoadBool_boolAsVariable_28, logic_SubGraph_SaveLoadBool_uniqueID_28);
	}

	private void Relay_Set_False_28()
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_28 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_28, logic_SubGraph_SaveLoadBool_boolAsVariable_28, logic_SubGraph_SaveLoadBool_uniqueID_28);
	}

	private void Relay_InitialSpawn_30()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_SpawnTechsFromData_spawnData_30.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_30, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_SpawnTechsFromData_spawnData_30, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_30 = owner_Connection_31;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_30.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_30, logic_uScript_SpawnTechsFromData_ownerNode_30, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_30, logic_uScript_SpawnTechsFromData_allowResurrection_30);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_30.Out)
		{
			Relay_True_2();
		}
	}

	private void Relay_In_32()
	{
		logic_uScript_SetTankInvulnerable_tank_32 = local_PaymentPointTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_32.In(logic_uScript_SetTankInvulnerable_invulnerable_32, logic_uScript_SetTankInvulnerable_tank_32);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_32.Out)
		{
			Relay_In_93();
		}
	}

	private void Relay_In_35()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_35.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_35, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_35, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_35 = owner_Connection_37;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_35.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_35, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_35, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_35 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_35.In(logic_uScript_GetAndCheckTechs_techData_35, logic_uScript_GetAndCheckTechs_ownerNode_35, ref logic_uScript_GetAndCheckTechs_techs_35);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_35;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_35.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_35.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_36();
		}
		if (someAlive)
		{
			Relay_AtIndex_36();
		}
	}

	private void Relay_AtIndex_36()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_36.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_36, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_36, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_36.AtIndex(ref logic_uScript_AccessListTech_techList_36, logic_uScript_AccessListTech_index_36, out logic_uScript_AccessListTech_value_36);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_36;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_36;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_36.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_39()
	{
		logic_uScriptAct_MultiplyInt_v2_A_39 = vehicleCost;
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_39.In(logic_uScriptAct_MultiplyInt_v2_A_39, logic_uScriptAct_MultiplyInt_v2_B_39, out logic_uScriptAct_MultiplyInt_v2_IntResult_39, out logic_uScriptAct_MultiplyInt_v2_FloatResult_39);
		local_38_System_Int32 = logic_uScriptAct_MultiplyInt_v2_IntResult_39;
		if (logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_39.Out)
		{
			Relay_In_66();
		}
	}

	private void Relay_ResponseEvent_41()
	{
		local_75_TankBlock = event_UnityEngine_GameObject_TankBlock_41;
		local_70_System_Boolean = event_UnityEngine_GameObject_Accepted_41;
		Relay_In_60();
	}

	private void Relay_In_42()
	{
		logic_uScriptCon_CompareBool_Bool_42 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.In(logic_uScriptCon_CompareBool_Bool_42);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.False;
		if (num)
		{
			Relay_In_73();
		}
		if (flag)
		{
			Relay_In_67();
		}
	}

	private void Relay_In_46()
	{
		logic_uScriptCon_CompareBool_Bool_46 = local_WaitingOnPrompt_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.In(logic_uScriptCon_CompareBool_Bool_46);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.False)
		{
			Relay_In_65();
		}
	}

	private void Relay_True_49()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.True(out logic_uScriptAct_SetBool_Target_49);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_49;
	}

	private void Relay_False_49()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.False(out logic_uScriptAct_SetBool_Target_49);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_49;
	}

	private void Relay_In_51()
	{
		logic_uScriptCon_CompareBool_Bool_51 = local_70_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_51.In(logic_uScriptCon_CompareBool_Bool_51);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_51.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_51.False;
		if (num)
		{
			Relay_In_59();
		}
		if (flag)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_52()
	{
		logic_uScriptCon_CompareInt_A_52 = local_CurrentMoney_System_Int32;
		logic_uScriptCon_CompareInt_B_52 = vehicleCost;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_52.In(logic_uScriptCon_CompareInt_A_52, logic_uScriptCon_CompareInt_B_52);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_52.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_52.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_42();
		}
		if (lessThan)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_54()
	{
		logic_uScript_GetCurrentMoneyEarned_Return_54 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_54.In();
		local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_54;
		if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_54.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_True_56()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_56.True(out logic_uScriptAct_SetBool_Target_56);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_56;
	}

	private void Relay_False_56()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_56.False(out logic_uScriptAct_SetBool_Target_56);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_56;
	}

	private void Relay_In_58()
	{
		logic_uScriptCon_CompareBool_Bool_58 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.In(logic_uScriptCon_CompareBool_Bool_58);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.False;
		if (num)
		{
			Relay_In_65();
		}
		if (flag)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_59()
	{
		logic_uScriptCon_CompareBool_Bool_59 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.In(logic_uScriptCon_CompareBool_Bool_59);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.False;
		if (num)
		{
			Relay_In_39();
		}
		if (flag)
		{
			Relay_True_56();
		}
	}

	private void Relay_In_60()
	{
		logic_uScript_CompareBlock_A_60 = local_75_TankBlock;
		logic_uScript_CompareBlock_B_60 = local_TerminalBlock_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_60.In(logic_uScript_CompareBlock_A_60, logic_uScript_CompareBlock_B_60);
		if (logic_uScript_CompareBlock_uScript_CompareBlock_60.EqualTo)
		{
			Relay_In_51();
		}
	}

	private void Relay_In_62()
	{
		logic_uScriptCon_CompareBool_Bool_62 = _DEBUGIgnoreMoneyCheck;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.In(logic_uScriptCon_CompareBool_Bool_62);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.False;
		if (num)
		{
			Relay_In_42();
		}
		if (flag)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_65()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_65 = msgPromptTextBlackbird;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_65 = msgPromptNoMoney;
		logic_uScript_MissionPromptBlock_Show_targetBlock_65 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_65.In(logic_uScript_MissionPromptBlock_Show_bodyText_65, logic_uScript_MissionPromptBlock_Show_acceptButtonText_65, logic_uScript_MissionPromptBlock_Show_rejectButtonText_65, logic_uScript_MissionPromptBlock_Show_targetBlock_65, logic_uScript_MissionPromptBlock_Show_highlightBlock_65, logic_uScript_MissionPromptBlock_Show_singleUse_65);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_65.Out)
		{
			Relay_False_69();
		}
	}

	private void Relay_In_66()
	{
		logic_uScript_AddMoney_amount_66 = local_38_System_Int32;
		logic_uScript_AddMoney_uScript_AddMoney_66.In(logic_uScript_AddMoney_amount_66);
		if (logic_uScript_AddMoney_uScript_AddMoney_66.Out)
		{
			Relay_InitialSpawn_83();
		}
	}

	private void Relay_In_67()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_67 = msgPromptTextBlackbird;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_67 = msgPromptAccept;
		logic_uScript_MissionPromptBlock_Show_rejectButtonText_67 = msgPromptDecline;
		logic_uScript_MissionPromptBlock_Show_targetBlock_67 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_67.In(logic_uScript_MissionPromptBlock_Show_bodyText_67, logic_uScript_MissionPromptBlock_Show_acceptButtonText_67, logic_uScript_MissionPromptBlock_Show_rejectButtonText_67, logic_uScript_MissionPromptBlock_Show_targetBlock_67, logic_uScript_MissionPromptBlock_Show_highlightBlock_67, logic_uScript_MissionPromptBlock_Show_singleUse_67);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_67.Out)
		{
			Relay_True_69();
		}
	}

	private void Relay_True_69()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_69.True(out logic_uScriptAct_SetBool_Target_69);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_69;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_69.Out)
		{
			Relay_True_49();
		}
	}

	private void Relay_False_69()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_69.False(out logic_uScriptAct_SetBool_Target_69);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_69;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_69.Out)
		{
			Relay_True_49();
		}
	}

	private void Relay_In_73()
	{
		logic_uScriptCon_CompareBool_Bool_73 = local_WaitingOnPrompt_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73.In(logic_uScriptCon_CompareBool_Bool_73);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73.False)
		{
			Relay_In_67();
		}
	}

	private void Relay_True_78()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.True(out logic_uScriptAct_SetBool_Target_78);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_78;
	}

	private void Relay_False_78()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.False(out logic_uScriptAct_SetBool_Target_78);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_78;
	}

	private void Relay_In_80()
	{
		int num = 0;
		Array array = discoverableBlockTypesOnVehicle;
		if (logic_uScript_DiscoverBlocks_blockTypes_80.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DiscoverBlocks_blockTypes_80, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DiscoverBlocks_blockTypes_80, num, array.Length);
		num += array.Length;
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_80.In(logic_uScript_DiscoverBlocks_blockTypes_80);
		if (logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_80.Out)
		{
			Relay_True_78();
		}
	}

	private void Relay_InitialSpawn_83()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_83.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_83, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_83, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_83 = owner_Connection_84;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_83.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_83, logic_uScript_SpawnTechsFromData_ownerNode_83, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_83, logic_uScript_SpawnTechsFromData_allowResurrection_83);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_83.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_87()
	{
		logic_uScript_GetTankBlock_tank_87 = local_PaymentPointTech_Tank;
		logic_uScript_GetTankBlock_blockType_87 = interactableBlockType;
		logic_uScript_GetTankBlock_Return_87 = logic_uScript_GetTankBlock_uScript_GetTankBlock_87.In(logic_uScript_GetTankBlock_tank_87, logic_uScript_GetTankBlock_blockType_87);
		local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_87;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_87.Returned)
		{
			Relay_True_22();
		}
	}

	private void Relay_In_90()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.Out)
		{
			Relay_True_56();
		}
	}

	private void Relay_True_91()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_91.True(out logic_uScriptAct_SetBool_Target_91);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_91;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_91.Out)
		{
			Relay_True_98();
		}
	}

	private void Relay_False_91()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_91.False(out logic_uScriptAct_SetBool_Target_91);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_91;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_91.Out)
		{
			Relay_True_98();
		}
	}

	private void Relay_In_93()
	{
		logic_uScript_SetEncounterTarget_owner_93 = owner_Connection_94;
		logic_uScript_SetEncounterTarget_visibleObject_93 = local_PaymentPointTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_93.In(logic_uScript_SetEncounterTarget_owner_93, logic_uScript_SetEncounterTarget_visibleObject_93);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_93.Out)
		{
			Relay_In_87();
		}
	}

	private void Relay_In_96()
	{
		logic_uScript_Wait_repeat_96 = local_RepeatWaitTime_System_Boolean;
		logic_uScript_Wait_uScript_Wait_96.In(logic_uScript_Wait_seconds_96, logic_uScript_Wait_repeat_96);
		if (logic_uScript_Wait_uScript_Wait_96.Waited)
		{
			Relay_False_91();
		}
	}

	private void Relay_True_98()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.True(out logic_uScriptAct_SetBool_Target_98);
		local_RepeatWaitTime_System_Boolean = logic_uScriptAct_SetBool_Target_98;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_98.Out)
		{
			Relay_False_103();
		}
	}

	private void Relay_False_98()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.False(out logic_uScriptAct_SetBool_Target_98);
		local_RepeatWaitTime_System_Boolean = logic_uScriptAct_SetBool_Target_98;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_98.Out)
		{
			Relay_False_103();
		}
	}

	private void Relay_In_100()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_100 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_100.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_100);
		if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_100.Out)
		{
			Relay_In_96();
		}
	}

	private void Relay_True_103()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_103.True(out logic_uScriptAct_SetBool_Target_103);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_103;
	}

	private void Relay_False_103()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_103.False(out logic_uScriptAct_SetBool_Target_103);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_103;
	}
}
