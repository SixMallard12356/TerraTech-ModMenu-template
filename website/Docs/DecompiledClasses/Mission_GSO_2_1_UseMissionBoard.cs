using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_GSO_2_1_UseMissionBoard : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private ManHUD.HUDElementType local_30_ManHUD_HUDElementType = ManHUD.HUDElementType.MissionBoard;

	private int local_DialogueProgress_System_Int32;

	private Tank local_GSOVendor_Tank;

	private TankBlock local_MissionBoardBlock_TankBlock;

	private bool local_MsgIntroShown_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private Vector3 local_VendorPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public Vector3 missionBoardArrowOffset = new Vector3(0f, 0f, 0f);

	public BlockTypes missionBoardBlockType;

	public uScript_AddMessage.MessageData msg01TradingStationIntro;

	public uScript_AddMessage.MessageData msg02OpenMissionBoard;

	public uScript_AddMessage.MessageData msg02OpenMissionBoard_Pad;

	public uScript_AddMessage.MessageData msg03AcceptMission;

	public uScript_AddMessage.MessageData msg03AcceptMission_Pad;

	public uScript_PlayDialogue.Dialogue TradingStationIntroDialogue;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_4;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_13;

	private GameObject owner_Connection_40;

	private GameObject owner_Connection_49;

	private GameObject owner_Connection_55;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_0 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_0;

	private float logic_uScript_IsPlayerInRangeOfTech_range_0 = 50f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_0 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_0 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_0 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_0 = true;

	private uScript_GetNearestVendorPos logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_6 = new uScript_GetNearestVendorPos();

	private Vector3 logic_uScript_GetNearestVendorPos_Return_6;

	private bool logic_uScript_GetNearestVendorPos_Out_6 = true;

	private bool logic_uScript_GetNearestVendorPos_Found_6 = true;

	private bool logic_uScript_GetNearestVendorPos_Missing_6 = true;

	private uScript_SetEncounterPosition logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_8 = new uScript_SetEncounterPosition();

	private GameObject logic_uScript_SetEncounterPosition_ownerNode_8;

	private Vector3 logic_uScript_SetEncounterPosition_position_8;

	private bool logic_uScript_SetEncounterPosition_Out_8 = true;

	private uScript_FindNearestVendorToEncounter logic_uScript_FindNearestVendorToEncounter_uScript_FindNearestVendorToEncounter_12 = new uScript_FindNearestVendorToEncounter();

	private GameObject logic_uScript_FindNearestVendorToEncounter_ownerNode_12;

	private Tank logic_uScript_FindNearestVendorToEncounter_Return_12;

	private bool logic_uScript_FindNearestVendorToEncounter_Out_12 = true;

	private bool logic_uScript_FindNearestVendorToEncounter_Returned_12 = true;

	private bool logic_uScript_FindNearestVendorToEncounter_NotReturned_12 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_14;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_16 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_16;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_19 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_19;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_19 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_19 = "Stage";

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_24;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_24;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_25 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_25;

	private float logic_uScript_IsPlayerInRangeOfTech_range_25 = 50f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_25 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_25 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_25 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_25 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_27 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_27 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_27 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_27 = true;

	private uScript_IsHUDElementVisible logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_29 = new uScript_IsHUDElementVisible();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementVisible_hudElement_29;

	private bool logic_uScript_IsHUDElementVisible_True_29 = true;

	private bool logic_uScript_IsHUDElementVisible_False_29 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_31;

	private bool logic_uScriptCon_CompareBool_True_31 = true;

	private bool logic_uScriptCon_CompareBool_False_31 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_32 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_32;

	private bool logic_uScriptAct_SetBool_Out_32 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_32 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_32 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_34 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_34;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_34;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_34;

	private bool logic_uScript_AddMessage_Out_34 = true;

	private bool logic_uScript_AddMessage_Shown_34 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_37;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_37 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_37 = "MsgIntroShown";

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_41 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_41;

	private object logic_uScript_SetEncounterTarget_visibleObject_41 = "";

	private bool logic_uScript_SetEncounterTarget_Out_41 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_43 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_43;

	private BlockTypes logic_uScript_GetTankBlock_blockType_43;

	private TankBlock logic_uScript_GetTankBlock_Return_43;

	private bool logic_uScript_GetTankBlock_Out_43 = true;

	private bool logic_uScript_GetTankBlock_Returned_43 = true;

	private bool logic_uScript_GetTankBlock_NotFound_43 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_47 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_47 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_50 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_50;

	private bool logic_uScript_FinishEncounter_Out_50 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_52 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_52 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_52 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_52 = true;

	private uScript_HasAcceptedAnyMissionFromBoard logic_uScript_HasAcceptedAnyMissionFromBoard_uScript_HasAcceptedAnyMissionFromBoard_53 = new uScript_HasAcceptedAnyMissionFromBoard();

	private bool logic_uScript_HasAcceptedAnyMissionFromBoard_Out_53 = true;

	private bool logic_uScript_HasAcceptedAnyMissionFromBoard_WaitingForAccept_53 = true;

	private bool logic_uScript_HasAcceptedAnyMissionFromBoard_AnyMissionAccepted_53 = true;

	private uScript_CloseMissionBoard logic_uScript_CloseMissionBoard_uScript_CloseMissionBoard_54 = new uScript_CloseMissionBoard();

	private bool logic_uScript_CloseMissionBoard_Out_54 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_56 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_56;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_56 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_57 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_57 = true;

	private bool logic_uScript_LockPlayerInput_includeCamera_57 = true;

	private bool logic_uScript_LockPlayerInput_Out_57 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_58 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_58;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_58 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_58 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_59 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_59;

	private bool logic_uScript_LockPlayerInput_includeCamera_59 = true;

	private bool logic_uScript_LockPlayerInput_Out_59 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_60 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_60 = true;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_60 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_60 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_61 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_61;

	private bool logic_uScript_LockPlayerInput_includeCamera_61 = true;

	private bool logic_uScript_LockPlayerInput_Out_61 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_62 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_62;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_62 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_62 = true;

	private uScript_SetVendorsEnabled logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_63 = new uScript_SetVendorsEnabled();

	private bool logic_uScript_SetVendorsEnabled_enableShop_63;

	private bool logic_uScript_SetVendorsEnabled_enableMissionBoard_63 = true;

	private bool logic_uScript_SetVendorsEnabled_enableSelling_63;

	private bool logic_uScript_SetVendorsEnabled_enableSCU_63;

	private bool logic_uScript_SetVendorsEnabled_enableCharging_63 = true;

	private bool logic_uScript_SetVendorsEnabled_Out_63 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_64;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_64;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_64;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_64;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_64;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_68;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_68;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_68;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_68;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_68;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_73 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_73 = "";

	private bool logic_uScript_EnableGlow_enable_73 = true;

	private bool logic_uScript_EnableGlow_Out_73 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_77 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_77 = "";

	private bool logic_uScript_EnableGlow_enable_77;

	private bool logic_uScript_EnableGlow_Out_77 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_78 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_78;

	private BlockTypes logic_uScript_GetTankBlock_blockType_78;

	private TankBlock logic_uScript_GetTankBlock_Return_78;

	private bool logic_uScript_GetTankBlock_Out_78 = true;

	private bool logic_uScript_GetTankBlock_Returned_78 = true;

	private bool logic_uScript_GetTankBlock_NotFound_78 = true;

	private uScript_IsHUDElementVisible logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_79 = new uScript_IsHUDElementVisible();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementVisible_hudElement_79 = ManHUD.HUDElementType.LicenceLevelUp;

	private bool logic_uScript_IsHUDElementVisible_True_79 = true;

	private bool logic_uScript_IsHUDElementVisible_False_79 = true;

	private uScript_PointArrowAtBlock logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_81 = new uScript_PointArrowAtBlock();

	private TankBlock logic_uScript_PointArrowAtBlock_block_81;

	private float logic_uScript_PointArrowAtBlock_timeToShowFor_81 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtBlock_offset_81;

	private bool logic_uScript_PointArrowAtBlock_Out_81 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_83 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_83 = "tutorial_stage";

	private string logic_uScript_SendAnaliticsEvent_parameterName_83 = "stage_complete";

	private object logic_uScript_SendAnaliticsEvent_parameter_83 = "use_mission_board";

	private bool logic_uScript_SendAnaliticsEvent_Out_83 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_84 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_84;

	private bool logic_uScript_LockHudGroup_locked_84 = true;

	private bool logic_uScript_LockHudGroup_Out_84 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_85 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_85;

	private bool logic_uScript_LockHudGroup_locked_85;

	private bool logic_uScript_LockHudGroup_Out_85 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_86 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_86;

	private bool logic_uScript_LockHudGroup_locked_86;

	private bool logic_uScript_LockHudGroup_Out_86 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_170 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_170;

	private int logic_uScript_PlayDialogue_progress_170;

	private bool logic_uScript_PlayDialogue_Out_170 = true;

	private bool logic_uScript_PlayDialogue_Shown_170 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_170 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_178;

	private int logic_SubGraph_SaveLoadInt_integer_178;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_178 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_178 = "DialogueProgress";

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
			if (null != owner_Connection_2)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_2.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_3;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_3;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_3;
				}
			}
		}
		if (null == owner_Connection_4 || !m_RegisteredForEvents)
		{
			owner_Connection_4 = parentGameObject;
			if (null != owner_Connection_4)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_4.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_4.AddComponent<uScript_SaveLoad>();
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
		if (null == owner_Connection_13 || !m_RegisteredForEvents)
		{
			owner_Connection_13 = parentGameObject;
		}
		if (null == owner_Connection_40 || !m_RegisteredForEvents)
		{
			owner_Connection_40 = parentGameObject;
		}
		if (null == owner_Connection_49 || !m_RegisteredForEvents)
		{
			owner_Connection_49 = parentGameObject;
		}
		if (null == owner_Connection_55 || !m_RegisteredForEvents)
		{
			owner_Connection_55 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_2)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_2.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_3;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_3;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_3;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_4)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_4.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_4.AddComponent<uScript_SaveLoad>();
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
		if (null != owner_Connection_2)
		{
			uScript_EncounterUpdate component = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_3;
				component.OnSuspend -= Instance_OnSuspend_3;
				component.OnResume -= Instance_OnResume_3;
			}
		}
		if (null != owner_Connection_4)
		{
			uScript_SaveLoad component2 = owner_Connection_4.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_5;
				component2.LoadEvent -= Instance_LoadEvent_5;
				component2.RestartEvent -= Instance_RestartEvent_5;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_0.SetParent(g);
		logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_6.SetParent(g);
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_8.SetParent(g);
		logic_uScript_FindNearestVendorToEncounter_uScript_FindNearestVendorToEncounter_12.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_16.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_25.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_27.SetParent(g);
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_29.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_34.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_41.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_43.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_47.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_50.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_52.SetParent(g);
		logic_uScript_HasAcceptedAnyMissionFromBoard_uScript_HasAcceptedAnyMissionFromBoard_53.SetParent(g);
		logic_uScript_CloseMissionBoard_uScript_CloseMissionBoard_54.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_56.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_57.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_58.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_59.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_60.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_61.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_62.SetParent(g);
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_63.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_73.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_77.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_78.SetParent(g);
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_79.SetParent(g);
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_81.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_83.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_84.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_85.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_86.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_170.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_4 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_13 = parentGameObject;
		owner_Connection_40 = parentGameObject;
		owner_Connection_49 = parentGameObject;
		owner_Connection_55 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_16.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output1 += uScriptCon_ManualSwitch_Output1_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output2 += uScriptCon_ManualSwitch_Output2_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output3 += uScriptCon_ManualSwitch_Output3_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output4 += uScriptCon_ManualSwitch_Output4_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output5 += uScriptCon_ManualSwitch_Output5_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output6 += uScriptCon_ManualSwitch_Output6_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output7 += uScriptCon_ManualSwitch_Output7_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output8 += uScriptCon_ManualSwitch_Output8_14;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_16.Out += SubGraph_LoadObjectiveStates_Out_16;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.Save_Out += SubGraph_SaveLoadInt_Save_Out_19;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.Load_Out += SubGraph_SaveLoadInt_Load_Out_19;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_19;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.Out += SubGraph_CompleteObjectiveStage_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Save_Out += SubGraph_SaveLoadBool_Save_Out_37;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Load_Out += SubGraph_SaveLoadBool_Load_Out_37;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_37;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Out += SubGraph_AddMessageWithPadSupport_Out_64;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Shown += SubGraph_AddMessageWithPadSupport_Shown_64;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68.Out += SubGraph_AddMessageWithPadSupport_Out_68;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68.Shown += SubGraph_AddMessageWithPadSupport_Shown_68;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Save_Out += SubGraph_SaveLoadInt_Save_Out_178;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Load_Out += SubGraph_SaveLoadInt_Load_Out_178;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_178;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_16.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_6.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_16.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.OnEnable();
		logic_uScript_CloseMissionBoard_uScript_CloseMissionBoard_54.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_56.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_170.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_0.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_16.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_25.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_34.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_43.OnDisable();
		logic_uScript_HasAcceptedAnyMissionFromBoard_uScript_HasAcceptedAnyMissionFromBoard_53.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_78.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_170.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_16.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_16.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output1 -= uScriptCon_ManualSwitch_Output1_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output2 -= uScriptCon_ManualSwitch_Output2_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output3 -= uScriptCon_ManualSwitch_Output3_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output4 -= uScriptCon_ManualSwitch_Output4_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output5 -= uScriptCon_ManualSwitch_Output5_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output6 -= uScriptCon_ManualSwitch_Output6_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output7 -= uScriptCon_ManualSwitch_Output7_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.Output8 -= uScriptCon_ManualSwitch_Output8_14;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_16.Out -= SubGraph_LoadObjectiveStates_Out_16;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.Save_Out -= SubGraph_SaveLoadInt_Save_Out_19;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.Load_Out -= SubGraph_SaveLoadInt_Load_Out_19;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_19;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.Out -= SubGraph_CompleteObjectiveStage_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Save_Out -= SubGraph_SaveLoadBool_Save_Out_37;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Load_Out -= SubGraph_SaveLoadBool_Load_Out_37;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_37;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Out -= SubGraph_AddMessageWithPadSupport_Out_64;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Shown -= SubGraph_AddMessageWithPadSupport_Shown_64;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68.Out -= SubGraph_AddMessageWithPadSupport_Out_68;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68.Shown -= SubGraph_AddMessageWithPadSupport_Shown_68;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Save_Out -= SubGraph_SaveLoadInt_Save_Out_178;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Load_Out -= SubGraph_SaveLoadInt_Load_Out_178;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_178;
	}

	private void Instance_OnUpdate_3(object o, EventArgs e)
	{
		Relay_OnUpdate_3();
	}

	private void Instance_OnSuspend_3(object o, EventArgs e)
	{
		Relay_OnSuspend_3();
	}

	private void Instance_OnResume_3(object o, EventArgs e)
	{
		Relay_OnResume_3();
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

	private void uScriptCon_ManualSwitch_Output1_14(object o, EventArgs e)
	{
		Relay_Output1_14();
	}

	private void uScriptCon_ManualSwitch_Output2_14(object o, EventArgs e)
	{
		Relay_Output2_14();
	}

	private void uScriptCon_ManualSwitch_Output3_14(object o, EventArgs e)
	{
		Relay_Output3_14();
	}

	private void uScriptCon_ManualSwitch_Output4_14(object o, EventArgs e)
	{
		Relay_Output4_14();
	}

	private void uScriptCon_ManualSwitch_Output5_14(object o, EventArgs e)
	{
		Relay_Output5_14();
	}

	private void uScriptCon_ManualSwitch_Output6_14(object o, EventArgs e)
	{
		Relay_Output6_14();
	}

	private void uScriptCon_ManualSwitch_Output7_14(object o, EventArgs e)
	{
		Relay_Output7_14();
	}

	private void uScriptCon_ManualSwitch_Output8_14(object o, EventArgs e)
	{
		Relay_Output8_14();
	}

	private void SubGraph_LoadObjectiveStates_Out_16(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_16();
	}

	private void SubGraph_SaveLoadInt_Save_Out_19(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_19 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_19;
		Relay_Save_Out_19();
	}

	private void SubGraph_SaveLoadInt_Load_Out_19(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_19 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_19;
		Relay_Load_Out_19();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_19(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_19 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_19;
		Relay_Restart_Out_19();
	}

	private void SubGraph_CompleteObjectiveStage_Out_24(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_24 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_24;
		Relay_Out_24();
	}

	private void SubGraph_SaveLoadBool_Save_Out_37(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = e.boolean;
		local_MsgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_37;
		Relay_Save_Out_37();
	}

	private void SubGraph_SaveLoadBool_Load_Out_37(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = e.boolean;
		local_MsgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_37;
		Relay_Load_Out_37();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_37(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = e.boolean;
		local_MsgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_37;
		Relay_Restart_Out_37();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_64(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_64 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_64 = e.messageControlPadReturn;
		Relay_Out_64();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_64(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_64 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_64 = e.messageControlPadReturn;
		Relay_Shown_64();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_68(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_68 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_68 = e.messageControlPadReturn;
		Relay_Out_68();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_68(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_68 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_68 = e.messageControlPadReturn;
		Relay_Shown_68();
	}

	private void SubGraph_SaveLoadInt_Save_Out_178(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_178 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_178;
		Relay_Save_Out_178();
	}

	private void SubGraph_SaveLoadInt_Load_Out_178(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_178 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_178;
		Relay_Load_Out_178();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_178(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_178 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_178;
		Relay_Restart_Out_178();
	}

	private void Relay_In_0()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_0 = local_GSOVendor_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_0.In(logic_uScript_IsPlayerInRangeOfTech_tech_0, logic_uScript_IsPlayerInRangeOfTech_range_0, logic_uScript_IsPlayerInRangeOfTech_techs_0);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_0.InRange)
		{
			Relay_In_24();
		}
	}

	private void Relay_OnUpdate_3()
	{
		Relay_In_6();
	}

	private void Relay_OnSuspend_3()
	{
	}

	private void Relay_OnResume_3()
	{
	}

	private void Relay_SaveEvent_5()
	{
		Relay_Save_37();
	}

	private void Relay_LoadEvent_5()
	{
		Relay_Load_37();
	}

	private void Relay_RestartEvent_5()
	{
		Relay_Set_False_37();
	}

	private void Relay_In_6()
	{
		logic_uScript_GetNearestVendorPos_Return_6 = logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_6.In();
		local_VendorPos_UnityEngine_Vector3 = logic_uScript_GetNearestVendorPos_Return_6;
		if (logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_6.Found)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_8()
	{
		logic_uScript_SetEncounterPosition_ownerNode_8 = owner_Connection_9;
		logic_uScript_SetEncounterPosition_position_8 = local_VendorPos_UnityEngine_Vector3;
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_8.In(logic_uScript_SetEncounterPosition_ownerNode_8, logic_uScript_SetEncounterPosition_position_8);
		if (logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_8.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_FindNearestVendorToEncounter_ownerNode_12 = owner_Connection_13;
		logic_uScript_FindNearestVendorToEncounter_Return_12 = logic_uScript_FindNearestVendorToEncounter_uScript_FindNearestVendorToEncounter_12.In(logic_uScript_FindNearestVendorToEncounter_ownerNode_12);
		local_GSOVendor_Tank = logic_uScript_FindNearestVendorToEncounter_Return_12;
		if (logic_uScript_FindNearestVendorToEncounter_uScript_FindNearestVendorToEncounter_12.Returned)
		{
			Relay_In_41();
		}
	}

	private void Relay_Output1_14()
	{
		Relay_In_0();
	}

	private void Relay_Output2_14()
	{
		Relay_In_29();
	}

	private void Relay_Output3_14()
	{
	}

	private void Relay_Output4_14()
	{
	}

	private void Relay_Output5_14()
	{
	}

	private void Relay_Output6_14()
	{
	}

	private void Relay_Output7_14()
	{
	}

	private void Relay_Output8_14()
	{
	}

	private void Relay_In_14()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_14 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_14.In(logic_uScriptCon_ManualSwitch_CurrentOutput_14);
	}

	private void Relay_Out_16()
	{
	}

	private void Relay_In_16()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_16 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_16.In(logic_SubGraph_LoadObjectiveStates_currentObjective_16);
	}

	private void Relay_Save_Out_19()
	{
	}

	private void Relay_Load_Out_19()
	{
		Relay_In_16();
	}

	private void Relay_Restart_Out_19()
	{
	}

	private void Relay_Save_19()
	{
		logic_SubGraph_SaveLoadInt_integer_19 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_19 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.Save(logic_SubGraph_SaveLoadInt_restartValue_19, ref logic_SubGraph_SaveLoadInt_integer_19, logic_SubGraph_SaveLoadInt_intAsVariable_19, logic_SubGraph_SaveLoadInt_uniqueID_19);
	}

	private void Relay_Load_19()
	{
		logic_SubGraph_SaveLoadInt_integer_19 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_19 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.Load(logic_SubGraph_SaveLoadInt_restartValue_19, ref logic_SubGraph_SaveLoadInt_integer_19, logic_SubGraph_SaveLoadInt_intAsVariable_19, logic_SubGraph_SaveLoadInt_uniqueID_19);
	}

	private void Relay_Restart_19()
	{
		logic_SubGraph_SaveLoadInt_integer_19 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_19 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_19.Restart(logic_SubGraph_SaveLoadInt_restartValue_19, ref logic_SubGraph_SaveLoadInt_integer_19, logic_SubGraph_SaveLoadInt_intAsVariable_19, logic_SubGraph_SaveLoadInt_uniqueID_19);
	}

	private void Relay_Out_24()
	{
	}

	private void Relay_In_24()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_24 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_24, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_24);
	}

	private void Relay_In_25()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_25 = local_GSOVendor_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_25.In(logic_uScript_IsPlayerInRangeOfTech_tech_25, logic_uScript_IsPlayerInRangeOfTech_range_25, logic_uScript_IsPlayerInRangeOfTech_techs_25);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_25.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_25.OutOfRange;
		if (inRange)
		{
			Relay_In_14();
		}
		if (outOfRange)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_27()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_27 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_27.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_27, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_27);
	}

	private void Relay_In_29()
	{
		logic_uScript_IsHUDElementVisible_hudElement_29 = local_30_ManHUD_HUDElementType;
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_29.In(logic_uScript_IsHUDElementVisible_hudElement_29);
		bool num = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_29.True;
		bool flag = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_29.False;
		if (num)
		{
			Relay_In_47();
		}
		if (flag)
		{
			Relay_In_64();
		}
	}

	private void Relay_In_31()
	{
		logic_uScriptCon_CompareBool_Bool_31 = local_MsgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.In(logic_uScriptCon_CompareBool_Bool_31);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.False;
		if (num)
		{
			Relay_In_63();
		}
		if (flag)
		{
			Relay_In_170();
		}
	}

	private void Relay_True_32()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.True(out logic_uScriptAct_SetBool_Target_32);
		local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_32;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_32.Out)
		{
			Relay_In_63();
		}
	}

	private void Relay_False_32()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.False(out logic_uScriptAct_SetBool_Target_32);
		local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_32;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_32.Out)
		{
			Relay_In_63();
		}
	}

	private void Relay_In_34()
	{
		logic_uScript_AddMessage_messageData_34 = msg01TradingStationIntro;
		logic_uScript_AddMessage_speaker_34 = messageSpeaker;
		logic_uScript_AddMessage_Return_34 = logic_uScript_AddMessage_uScript_AddMessage_34.In(logic_uScript_AddMessage_messageData_34, logic_uScript_AddMessage_speaker_34);
	}

	private void Relay_Save_Out_37()
	{
		Relay_Save_178();
	}

	private void Relay_Load_Out_37()
	{
		Relay_Load_178();
	}

	private void Relay_Restart_Out_37()
	{
		Relay_Restart_178();
	}

	private void Relay_Save_37()
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_37 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Save(ref logic_SubGraph_SaveLoadBool_boolean_37, logic_SubGraph_SaveLoadBool_boolAsVariable_37, logic_SubGraph_SaveLoadBool_uniqueID_37);
	}

	private void Relay_Load_37()
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_37 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Load(ref logic_SubGraph_SaveLoadBool_boolean_37, logic_SubGraph_SaveLoadBool_boolAsVariable_37, logic_SubGraph_SaveLoadBool_uniqueID_37);
	}

	private void Relay_Set_True_37()
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_37 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_37, logic_SubGraph_SaveLoadBool_boolAsVariable_37, logic_SubGraph_SaveLoadBool_uniqueID_37);
	}

	private void Relay_Set_False_37()
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_37 = local_MsgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_37, logic_SubGraph_SaveLoadBool_boolAsVariable_37, logic_SubGraph_SaveLoadBool_uniqueID_37);
	}

	private void Relay_In_41()
	{
		logic_uScript_SetEncounterTarget_owner_41 = owner_Connection_40;
		logic_uScript_SetEncounterTarget_visibleObject_41 = local_GSOVendor_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_41.In(logic_uScript_SetEncounterTarget_owner_41, logic_uScript_SetEncounterTarget_visibleObject_41);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_41.Out)
		{
			Relay_In_56();
		}
	}

	private void Relay_In_43()
	{
		logic_uScript_GetTankBlock_tank_43 = local_GSOVendor_Tank;
		logic_uScript_GetTankBlock_blockType_43 = missionBoardBlockType;
		logic_uScript_GetTankBlock_Return_43 = logic_uScript_GetTankBlock_uScript_GetTankBlock_43.In(logic_uScript_GetTankBlock_tank_43, logic_uScript_GetTankBlock_blockType_43);
		local_MissionBoardBlock_TankBlock = logic_uScript_GetTankBlock_Return_43;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_43.Returned)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_47()
	{
		logic_uScript_HideArrow_uScript_HideArrow_47.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_47.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_Succeed_50()
	{
		logic_uScript_FinishEncounter_owner_50 = owner_Connection_49;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_50.Succeed(logic_uScript_FinishEncounter_owner_50);
	}

	private void Relay_Fail_50()
	{
		logic_uScript_FinishEncounter_owner_50 = owner_Connection_49;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_50.Fail(logic_uScript_FinishEncounter_owner_50);
	}

	private void Relay_In_52()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_52 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_52.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_52, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_52);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_52.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_53()
	{
		logic_uScript_HasAcceptedAnyMissionFromBoard_uScript_HasAcceptedAnyMissionFromBoard_53.In();
		bool waitingForAccept = logic_uScript_HasAcceptedAnyMissionFromBoard_uScript_HasAcceptedAnyMissionFromBoard_53.WaitingForAccept;
		bool anyMissionAccepted = logic_uScript_HasAcceptedAnyMissionFromBoard_uScript_HasAcceptedAnyMissionFromBoard_53.AnyMissionAccepted;
		if (waitingForAccept)
		{
			Relay_In_68();
		}
		if (anyMissionAccepted)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_54()
	{
		logic_uScript_CloseMissionBoard_uScript_CloseMissionBoard_54.In();
		if (logic_uScript_CloseMissionBoard_uScript_CloseMissionBoard_54.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_56()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_56 = owner_Connection_55;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_56.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_56);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_56.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_57()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_57.In(logic_uScript_LockPlayerInput_lockInput_57, logic_uScript_LockPlayerInput_includeCamera_57);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_57.Out)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_58()
	{
		logic_uScript_LockPause_uScript_LockPause_58.In(logic_uScript_LockPause_lockPause_58, logic_uScript_LockPause_disabledReason_58);
		if (logic_uScript_LockPause_uScript_LockPause_58.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_59()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_59.In(logic_uScript_LockPlayerInput_lockInput_59, logic_uScript_LockPlayerInput_includeCamera_59);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_59.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_60()
	{
		logic_uScript_LockPause_uScript_LockPause_60.In(logic_uScript_LockPause_lockPause_60, logic_uScript_LockPause_disabledReason_60);
		if (logic_uScript_LockPause_uScript_LockPause_60.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_61()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_61.In(logic_uScript_LockPlayerInput_lockInput_61, logic_uScript_LockPlayerInput_includeCamera_61);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_61.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_62()
	{
		logic_uScript_LockPause_uScript_LockPause_62.In(logic_uScript_LockPause_lockPause_62, logic_uScript_LockPause_disabledReason_62);
		if (logic_uScript_LockPause_uScript_LockPause_62.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_63()
	{
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_63.In(logic_uScript_SetVendorsEnabled_enableShop_63, logic_uScript_SetVendorsEnabled_enableMissionBoard_63, logic_uScript_SetVendorsEnabled_enableSelling_63, logic_uScript_SetVendorsEnabled_enableSCU_63, logic_uScript_SetVendorsEnabled_enableCharging_63);
		if (logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_63.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_Out_64()
	{
		Relay_In_62();
	}

	private void Relay_Shown_64()
	{
	}

	private void Relay_In_64()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_64 = msg02OpenMissionBoard;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_64 = msg02OpenMissionBoard_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_64 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_64, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_64, logic_SubGraph_AddMessageWithPadSupport_speaker_64);
	}

	private void Relay_Out_68()
	{
		Relay_In_60();
	}

	private void Relay_Shown_68()
	{
	}

	private void Relay_In_68()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_68 = msg03AcceptMission;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_68 = msg03AcceptMission_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_68 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_68.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_68, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_68, logic_SubGraph_AddMessageWithPadSupport_speaker_68);
	}

	private void Relay_In_73()
	{
		logic_uScript_EnableGlow_targetObject_73 = local_MissionBoardBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_73.In(logic_uScript_EnableGlow_targetObject_73, logic_uScript_EnableGlow_enable_73);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_73.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_In_77()
	{
		logic_uScript_EnableGlow_targetObject_77 = local_MissionBoardBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_77.In(logic_uScript_EnableGlow_targetObject_77, logic_uScript_EnableGlow_enable_77);
	}

	private void Relay_In_78()
	{
		logic_uScript_GetTankBlock_tank_78 = local_GSOVendor_Tank;
		logic_uScript_GetTankBlock_blockType_78 = missionBoardBlockType;
		logic_uScript_GetTankBlock_Return_78 = logic_uScript_GetTankBlock_uScript_GetTankBlock_78.In(logic_uScript_GetTankBlock_tank_78, logic_uScript_GetTankBlock_blockType_78);
		local_MissionBoardBlock_TankBlock = logic_uScript_GetTankBlock_Return_78;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_78.Returned)
		{
			Relay_In_77();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_79.In(logic_uScript_IsHUDElementVisible_hudElement_79);
		if (logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_79.False)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_81()
	{
		logic_uScript_PointArrowAtBlock_block_81 = local_MissionBoardBlock_TankBlock;
		logic_uScript_PointArrowAtBlock_offset_81 = missionBoardArrowOffset;
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_81.In(logic_uScript_PointArrowAtBlock_block_81, logic_uScript_PointArrowAtBlock_timeToShowFor_81, logic_uScript_PointArrowAtBlock_offset_81);
	}

	private void Relay_In_83()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_83.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_83, logic_uScript_SendAnaliticsEvent_parameterName_83, logic_uScript_SendAnaliticsEvent_parameter_83);
		if (logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_83.Out)
		{
			Relay_Succeed_50();
		}
	}

	private void Relay_In_84()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_84.In(logic_uScript_LockHudGroup_group_84, logic_uScript_LockHudGroup_locked_84);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_84.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_85()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_85.In(logic_uScript_LockHudGroup_group_85, logic_uScript_LockHudGroup_locked_85);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_85.Out)
		{
			Relay_In_59();
		}
	}

	private void Relay_In_86()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_86.In(logic_uScript_LockHudGroup_group_86, logic_uScript_LockHudGroup_locked_86);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_86.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_170()
	{
		logic_uScript_PlayDialogue_dialogue_170 = TradingStationIntroDialogue;
		logic_uScript_PlayDialogue_progress_170 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_170.In(logic_uScript_PlayDialogue_dialogue_170, ref logic_uScript_PlayDialogue_progress_170);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_170;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_170.Shown)
		{
			Relay_True_32();
		}
	}

	private void Relay_Save_Out_178()
	{
		Relay_Save_19();
	}

	private void Relay_Load_Out_178()
	{
		Relay_Load_19();
	}

	private void Relay_Restart_Out_178()
	{
		Relay_Restart_19();
	}

	private void Relay_Save_178()
	{
		logic_SubGraph_SaveLoadInt_integer_178 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_178 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Save(logic_SubGraph_SaveLoadInt_restartValue_178, ref logic_SubGraph_SaveLoadInt_integer_178, logic_SubGraph_SaveLoadInt_intAsVariable_178, logic_SubGraph_SaveLoadInt_uniqueID_178);
	}

	private void Relay_Load_178()
	{
		logic_SubGraph_SaveLoadInt_integer_178 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_178 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Load(logic_SubGraph_SaveLoadInt_restartValue_178, ref logic_SubGraph_SaveLoadInt_integer_178, logic_SubGraph_SaveLoadInt_intAsVariable_178, logic_SubGraph_SaveLoadInt_uniqueID_178);
	}

	private void Relay_Restart_178()
	{
		logic_SubGraph_SaveLoadInt_integer_178 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_178 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Restart(logic_SubGraph_SaveLoadInt_restartValue_178, ref logic_SubGraph_SaveLoadInt_integer_178, logic_SubGraph_SaveLoadInt_intAsVariable_178, logic_SubGraph_SaveLoadInt_uniqueID_178);
	}
}
