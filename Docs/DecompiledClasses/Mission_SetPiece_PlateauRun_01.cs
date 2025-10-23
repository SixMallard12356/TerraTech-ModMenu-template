using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_DefeatEnemyTechs", "")]
public class Mission_SetPiece_PlateauRun_01 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool _DEBUGIgnoreMoneyCheck;

	[Multiline(3)]
	public string Bridge1Trigger = "";

	[Multiline(3)]
	public string Bridge2Trigger = "";

	[Multiline(3)]
	public string Bridge3Trigger = "";

	public BlockTypes[] discoverableBlockTypesOnVehicle = new BlockTypes[0];

	[Multiline(3)]
	public string EndTrigger = "";

	[Multiline(3)]
	public string FallTrigger = "";

	[Multiline(3)]
	public string FlightBlocker1Trigger = "";

	[Multiline(3)]
	public string FlightBlocker2Trigger = "";

	[Multiline(3)]
	public string InsideTrigger = "";

	public BlockTypes interactableBlockType;

	private bool local_215_System_Boolean;

	private TankBlock local_221_TankBlock;

	private float local_512_System_Single;

	private int local_546_System_Int32;

	private bool local_BlockLimitCritical_System_Boolean;

	private int local_CurrentMoney_System_Int32;

	private bool local_ExplainedHowToBuy_System_Boolean;

	private bool local_GoToStartTrigger_System_Boolean;

	private bool local_HasEnoughMoney_System_Boolean;

	private int local_MaxPlayers_System_Int32;

	private bool local_msg03aShown_System_Boolean;

	private bool local_msg03bShown_System_Boolean;

	private bool local_msgDespawnTechsShown_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgPurchaseVehicle_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgPurchaseVehicle_Pad_ManOnScreenMessages_OnScreenMessage;

	private bool local_MsgShownBridge1_System_Boolean;

	private bool local_MsgShownBridge2_System_Boolean;

	private bool local_MsgShownBridge3_System_Boolean;

	private bool local_MsgShownRamp1_System_Boolean;

	private bool local_MsgShownRamp2_System_Boolean;

	private bool local_MsgShownRamp3_System_Boolean;

	private bool local_MsgShownRamp4_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgSwitchTech_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgSwitchTech_Pad_ManOnScreenMessages_OnScreenMessage;

	private bool local_NPCIntroMessagePlayed_System_Boolean;

	private bool local_NPCMet_System_Boolean;

	private Tank[] local_NPCPaymentPoints_TankArray = new Tank[0];

	private bool local_NPCSeen_System_Boolean;

	private Tank[] local_NPCTechs_TankArray = new Tank[0];

	private bool local_ObjectiveComplete_System_Boolean;

	private Tank local_PaymentPointTech_Tank;

	private bool local_PreviouslyBoughtVehicle_System_Boolean;

	private bool local_RepeatWaitTime_System_Boolean;

	private bool local_RunAttempted_System_Boolean;

	private bool local_RunGoing_System_Boolean;

	private bool local_RunStarted_System_Boolean = true;

	private bool local_SaidMsgNPCVehicleControls_System_Boolean;

	private bool local_SaidMsgNPCVehicleSwitched_System_Boolean;

	private bool local_ShownMsgFellYouCanBuy_System_Boolean;

	private bool local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;

	private bool local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;

	private bool local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;

	private bool local_ShownMsgStartTooEarly_System_Boolean;

	private bool local_ShownMsgYouCanReBuy_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private bool local_SwitchedVehicle_System_Boolean;

	private Tank local_techNPC_Tank;

	private bool local_TechSetUp_System_Boolean;

	private bool local_TechSpawned_System_Boolean;

	private TankBlock local_TerminalBlock_TankBlock;

	private bool local_VehiclePurchased_System_Boolean;

	private bool local_VehicleSetup_System_Boolean;

	private Tank local_vehicleTech_Tank;

	private Tank[] local_vehicleTechs_TankArray = new Tank[0];

	private bool local_WaitingOnPrompt_System_Boolean;

	private bool local_Zoomer1Alive_System_Boolean;

	private bool local_Zoomer2Alive_System_Boolean;

	private bool local_Zoomer3Alive_System_Boolean;

	public LocalisedString[] msgBoosterControlsGeneric = new LocalisedString[0];

	public LocalisedString[] msgBoosterControlsZoomer = new LocalisedString[0];

	public LocalisedString[] msgBridge2 = new LocalisedString[0];

	public LocalisedString[] msgBridge3 = new LocalisedString[0];

	public uScript_AddMessage.MessageData msgDespawnTechs;

	public LocalisedString[] msgFellOutOfBounds = new LocalisedString[0];

	public LocalisedString[] msgFlewOutOfBounds = new LocalisedString[0];

	public LocalisedString[] msgFlightBlockerHit = new LocalisedString[0];

	public LocalisedString[] msgLeavingEarlyDuringIntro = new LocalisedString[0];

	public LocalisedString[] msgLeavingEarlyPostPurchase = new LocalisedString[0];

	public LocalisedString[] msgLeavingEarlyPrePurchase = new LocalisedString[0];

	public LocalisedString[] msgMissionComplete = new LocalisedString[0];

	public LocalisedString[] msgMissionCompleteNoZoomer = new LocalisedString[0];

	public LocalisedString[] msgNPCIntro = new LocalisedString[0];

	public uScript_AddMessage.MessageData msgNPCNotEnoughMoney;

	public uScript_AddMessage.MessageData msgNPCPurchaseDeclined;

	public LocalisedString[] msgNPCVehiclePurchased = new LocalisedString[0];

	public LocalisedString[] msgNPCVehicleSwitched = new LocalisedString[0];

	public LocalisedString[] msgOutOfTime = new LocalisedString[0];

	public LocalisedString[] msgOutOfTimeCanStillBuy = new LocalisedString[0];

	public LocalisedString[] msgPlateauRunIntro = new LocalisedString[0];

	public LocalisedString msgPromptAccept;

	public LocalisedString msgPromptAccessDenied;

	public LocalisedString msgPromptDecline;

	public LocalisedString msgPromptNoMoney;

	public LocalisedString msgPromptText;

	public uScript_AddMessage.MessageData msgPurchaseVehicle;

	public uScript_AddMessage.MessageData msgPurchaseVehicle_Pad;

	public LocalisedString[] msgRamp1 = new LocalisedString[0];

	public LocalisedString[] msgRamp2 = new LocalisedString[0];

	public LocalisedString[] msgRamp3 = new LocalisedString[0];

	public LocalisedString[] msgRamp4 = new LocalisedString[0];

	public LocalisedString[] msgStartTooEarly = new LocalisedString[0];

	public uScript_AddMessage.MessageData msgSwitchTech;

	public uScript_AddMessage.MessageData msgSwitchTech_Pad;

	[Multiline(3)]
	public string msgTagControls = "";

	[Multiline(3)]
	public string msgTagPurchase = "";

	[Multiline(3)]
	public string msgTagSwitchTech = "";

	public LocalisedString[] msgThrownOutOfBounds = new LocalisedString[0];

	public LocalisedString[] MsgTooManyVehiclesSpawnedAlready = new LocalisedString[0];

	public LocalisedString[] msgYouCanReBuy = new LocalisedString[0];

	[Multiline(3)]
	public string NearNPCTrigger = "";

	public ExternalBehaviorTree NPCFlyAwayBehavior;

	public SpawnTechData[] NPCPaymentPoint = new SpawnTechData[0];

	public ManOnScreenMessages.Speaker NPCSpeaker;

	public SpawnTechData[] NPCTechData = new SpawnTechData[0];

	[Multiline(3)]
	public string Ramp1Trigger = "";

	[Multiline(3)]
	public string Ramp2Trigger = "";

	[Multiline(3)]
	public string Ramp3Trigger = "";

	[Multiline(3)]
	public string Ramp4Trigger = "";

	public float RunTimeLimit = 60f;

	public ManSFX.MiscSfxType SFXRaceComplete = ManSFX.MiscSfxType.StuntComplete;

	public ManSFX.MiscSfxType SFXRaceFailed = ManSFX.MiscSfxType.StuntRing;

	public ManSFX.MiscSfxType SFXRaceStart = ManSFX.MiscSfxType.StuntRingStart;

	public uScript_AddMessage.MessageSpeaker SpeakerNPC;

	[Multiline(3)]
	public string StartPosition = "";

	[Multiline(3)]
	public string StartTrigger = "";

	[Multiline(3)]
	public string TopTrigger = "";

	public int vehicleCost;

	public SpawnTechData[] vehicleSpawnData = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnData2 = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnData3 = new SpawnTechData[0];

	private GameObject owner_Connection_4;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_31;

	private GameObject owner_Connection_65;

	private GameObject owner_Connection_68;

	private GameObject owner_Connection_118;

	private GameObject owner_Connection_127;

	private GameObject owner_Connection_154;

	private GameObject owner_Connection_171;

	private GameObject owner_Connection_179;

	private GameObject owner_Connection_209;

	private GameObject owner_Connection_243;

	private GameObject owner_Connection_478;

	private GameObject owner_Connection_526;

	private GameObject owner_Connection_528;

	private GameObject owner_Connection_530;

	private GameObject owner_Connection_535;

	private GameObject owner_Connection_539;

	private GameObject owner_Connection_542;

	private GameObject owner_Connection_544;

	private GameObject owner_Connection_562;

	private GameObject owner_Connection_564;

	private GameObject owner_Connection_601;

	private GameObject owner_Connection_602;

	private GameObject owner_Connection_603;

	private GameObject owner_Connection_635;

	private GameObject owner_Connection_640;

	private GameObject owner_Connection_654;

	private GameObject owner_Connection_656;

	private GameObject owner_Connection_684;

	private GameObject owner_Connection_687;

	private GameObject owner_Connection_707;

	private GameObject owner_Connection_708;

	private GameObject owner_Connection_709;

	private GameObject owner_Connection_710;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_2 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_2;

	private bool logic_uScriptAct_SetBool_Out_2 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_2 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_2 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_3;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_3 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_3 = "ObjectiveComplete";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_8;

	private bool logic_uScriptCon_CompareBool_True_8 = true;

	private bool logic_uScriptCon_CompareBool_False_8 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_10 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_10;

	private object logic_uScript_SetEncounterTarget_visibleObject_10 = "";

	private bool logic_uScript_SetEncounterTarget_Out_10 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_13 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_13;

	private bool logic_uScript_FinishEncounter_Out_13 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_14 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_14 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_14 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_14;

	private string logic_uScript_AddOnScreenMessage_tag_14 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_14;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_14;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_14;

	private bool logic_uScript_AddOnScreenMessage_Out_14 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_14 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_17;

	private bool logic_uScriptCon_CompareBool_True_17 = true;

	private bool logic_uScriptCon_CompareBool_False_17 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_18 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_18;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_18 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_18;

	private bool logic_uScript_SpawnTechsFromData_Out_18 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_22 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_22;

	private bool logic_uScriptAct_SetBool_Out_22 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_22 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_22 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_24 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_24;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_24 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_24 = "Stage";

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_26;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_26;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_29;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_29 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_29 = "TechSpawned";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_30 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_30 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_30;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_30 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_30;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_30 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_30 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_30 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_30 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_33 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_33 = new Tank[0];

	private int logic_uScript_AccessListTech_index_33;

	private Tank logic_uScript_AccessListTech_value_33;

	private bool logic_uScript_AccessListTech_Out_33 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_34 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_34 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_34;

	private bool logic_uScript_SetTankInvulnerable_Out_34 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_37;

	private bool logic_uScriptCon_CompareBool_True_37 = true;

	private bool logic_uScriptCon_CompareBool_False_37 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_40;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_40 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_40 = "NPCMet";

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_41 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_41 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_41 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_41 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_41 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_44;

	private bool logic_uScriptCon_CompareBool_True_44 = true;

	private bool logic_uScriptCon_CompareBool_False_44 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_46;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_46 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_46 = "NPCIntroMessagePlayed";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_47 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_47;

	private bool logic_uScriptAct_SetBool_Out_47 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_47 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_47 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_48 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_49 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_49;

	private BlockTypes logic_uScript_GetTankBlock_blockType_49;

	private TankBlock logic_uScript_GetTankBlock_Return_49;

	private bool logic_uScript_GetTankBlock_Out_49 = true;

	private bool logic_uScript_GetTankBlock_Returned_49 = true;

	private bool logic_uScript_GetTankBlock_NotFound_49 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_51;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_51;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_51;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_51;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_51;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_53 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_53 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_53 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_53 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_53 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_55;

	private bool logic_uScriptCon_CompareBool_True_55 = true;

	private bool logic_uScriptCon_CompareBool_False_55 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_58 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_58 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_60 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_60;

	private bool logic_uScriptCon_CompareBool_True_60 = true;

	private bool logic_uScriptCon_CompareBool_False_60 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_61 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_61;

	private object logic_uScript_SetEncounterTarget_visibleObject_61 = "";

	private bool logic_uScript_SetEncounterTarget_Out_61 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_64;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_64;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_64;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_64;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_64;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_67;

	private bool logic_uScriptCon_CompareBool_True_67 = true;

	private bool logic_uScriptCon_CompareBool_False_67 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_70 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_70 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_70 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_70 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_71 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_71 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_71 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_71 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_76 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_76;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_76 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_78 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_78 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_80 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_80 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_81 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_81 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_82 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_82;

	private bool logic_uScriptAct_SetBool_Out_82 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_82 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_82 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_84 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_84 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_84 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_84 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_84 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_85 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_85 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_85 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_85;

	private string logic_uScript_AddOnScreenMessage_tag_85 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_85;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_85;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_85;

	private bool logic_uScript_AddOnScreenMessage_Out_85 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_85 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_89 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_89 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_89 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_89 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_93;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_93 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_93 = "SwitchedVehicle";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_94;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_94 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_94 = "VehiclePurchased";

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_96 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_96 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_96 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_96 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_96 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_98 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_98 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_98 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_98 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_98 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_100;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_100 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_100 = "HasEnoughMoney";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_102;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_102 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_102 = "WaitingOnPrompt";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_107;

	private bool logic_uScriptCon_CompareBool_True_107 = true;

	private bool logic_uScriptCon_CompareBool_False_107 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_109 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_109;

	private bool logic_uScriptAct_SetBool_Out_109 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_109 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_109 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_111;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_111 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_111 = "TechSetUp";

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_112 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_112;

	private bool logic_uScript_IsTechPlayer_Out_112 = true;

	private bool logic_uScript_IsTechPlayer_True_112 = true;

	private bool logic_uScript_IsTechPlayer_False_112 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_113;

	private bool logic_uScriptCon_CompareBool_True_113 = true;

	private bool logic_uScriptCon_CompareBool_False_113 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_115;

	private bool logic_uScriptCon_CompareBool_True_115 = true;

	private bool logic_uScriptCon_CompareBool_False_115 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_117 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_117;

	private bool logic_uScriptAct_SetBool_Out_117 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_117 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_117 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_120 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_120 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_120;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_120 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_120;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_120 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_120 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_120 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_120 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_121 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_121 = new Tank[0];

	private int logic_uScript_AccessListTech_index_121;

	private Tank logic_uScript_AccessListTech_value_121;

	private bool logic_uScript_AccessListTech_Out_121 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_122 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_122 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_125;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_125 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_125 = "VehicleSetup";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_128 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_128;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_128 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_128;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_128 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_128 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_128 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_128 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_129 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_129 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_129 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_129 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_135 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_135 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_135;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_135 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_136 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_136 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_137 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_137 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_138 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_138 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_140 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_140 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_140 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_140 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_140 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_142 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_142 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_142;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_142 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_144 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_144 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_144 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_147 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_147 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_147 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_148 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_148;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_148 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_148 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_151 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_151;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_151;

	private bool logic_uScript_LockTechSendToSCU_Out_151 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_155 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_155;

	private object logic_uScript_SetEncounterTarget_visibleObject_155 = "";

	private bool logic_uScript_SetEncounterTarget_Out_155 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_156 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_156;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_156 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_156 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_156;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_156;

	private bool logic_uScript_FlyTechUpAndAway_Out_156 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_159 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_159;

	private bool logic_uScriptAct_SetBool_Out_159 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_159 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_159 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_163 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_163 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_163 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_163;

	private string logic_uScript_AddOnScreenMessage_tag_163 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_163;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_163;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_163;

	private bool logic_uScript_AddOnScreenMessage_Out_163 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_163 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_164 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_164;

	private bool logic_uScriptCon_CompareBool_True_164 = true;

	private bool logic_uScriptCon_CompareBool_False_164 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_168;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_168 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_168 = "SaidMsgNPCVehicleSwitched";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_170 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_170 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_170;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_170 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_170;

	private bool logic_uScript_SpawnTechsFromData_Out_170 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_173 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_173 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_173;

	private bool logic_uScript_SetTankInvulnerable_Out_173 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_176 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_176 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_176;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_176 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_176;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_176 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_176 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_176 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_176 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_177 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_177 = new Tank[0];

	private int logic_uScript_AccessListTech_index_177;

	private Tank logic_uScript_AccessListTech_value_177;

	private bool logic_uScript_AccessListTech_Out_177 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_183;

	private bool logic_uScriptCon_CompareBool_True_183 = true;

	private bool logic_uScriptCon_CompareBool_False_183 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_185 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_185;

	private bool logic_uScriptAct_SetBool_Out_185 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_185 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_185 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_189 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_189 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_192;

	private bool logic_uScriptCon_CompareBool_True_192 = true;

	private bool logic_uScriptCon_CompareBool_False_192 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_193 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_193;

	private int logic_uScriptCon_CompareInt_B_193;

	private bool logic_uScriptCon_CompareInt_GreaterThan_193 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_193 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_193 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_193 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_193 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_193 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_194 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_194;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_194 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_196 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_196;

	private bool logic_uScriptCon_CompareBool_True_196 = true;

	private bool logic_uScriptCon_CompareBool_False_196 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_197;

	private bool logic_uScriptCon_CompareBool_True_197 = true;

	private bool logic_uScriptCon_CompareBool_False_197 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_199 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_199;

	private TankBlock logic_uScript_CompareBlock_B_199;

	private bool logic_uScript_CompareBlock_EqualTo_199 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_199 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_200 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_204 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_204;

	private bool logic_uScriptCon_CompareBool_True_204 = true;

	private bool logic_uScriptCon_CompareBool_False_204 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_205 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_205 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_206 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_206;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_206;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_206;

	private bool logic_uScript_AddMessage_Out_206 = true;

	private bool logic_uScript_AddMessage_Shown_206 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_210 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_210;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_210;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_210;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_210;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_210 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_210;

	private bool logic_uScript_MissionPromptBlock_Show_Out_210 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_211 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_211;

	private bool logic_uScriptAct_SetBool_Out_211 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_211 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_211 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_212 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_212;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_212;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_212;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_212;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_212 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_212;

	private bool logic_uScript_MissionPromptBlock_Show_Out_212 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_214 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_214;

	private bool logic_uScriptAct_SetBool_Out_214 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_214 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_214 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_218 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_218;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_218;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_218;

	private bool logic_uScript_AddMessage_Out_218 = true;

	private bool logic_uScript_AddMessage_Shown_218 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_219;

	private bool logic_uScriptCon_CompareBool_True_219 = true;

	private bool logic_uScriptCon_CompareBool_False_219 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_223 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_223;

	private bool logic_uScriptAct_SetBool_Out_223 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_223 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_223 = true;

	private uScript_DiscoverBlocks logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_225 = new uScript_DiscoverBlocks();

	private BlockTypes[] logic_uScript_DiscoverBlocks_blockTypes_225 = new BlockTypes[0];

	private bool logic_uScript_DiscoverBlocks_Out_225 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_230 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_230 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_231 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_231 = "";

	private bool logic_uScript_EnableGlow_enable_231;

	private bool logic_uScript_EnableGlow_Out_231 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_233 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_233 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_233 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_233 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_233 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_234 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_234 = "";

	private bool logic_uScript_EnableGlow_enable_234 = true;

	private bool logic_uScript_EnableGlow_Out_234 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_236 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_236 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_237 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_237 = "";

	private bool logic_uScript_EnableGlow_enable_237;

	private bool logic_uScript_EnableGlow_Out_237 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_240 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_240;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_240 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_242 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_242;

	private bool logic_uScript_ClearEncounterTarget_Out_242 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_245 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_245;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_245 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_245 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_245;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_245;

	private bool logic_uScript_FlyTechUpAndAway_Out_245 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_247 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_247;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_247 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_250 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_250;

	private bool logic_uScriptCon_CompareBool_True_250 = true;

	private bool logic_uScriptCon_CompareBool_False_250 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_251 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_251 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_251 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_251;

	private string logic_uScript_AddOnScreenMessage_tag_251 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_251;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_251;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_251;

	private bool logic_uScript_AddOnScreenMessage_Out_251 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_251 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_254 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_254;

	private bool logic_uScriptAct_SetBool_Out_254 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_254 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_254 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_257;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_257 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_257 = "ShownMsgLeavingEarlyPrePurchase";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_260 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_260;

	private bool logic_uScriptAct_SetBool_Out_260 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_260 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_260 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_261 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_261 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_261 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_261;

	private string logic_uScript_AddOnScreenMessage_tag_261 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_261;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_261;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_261;

	private bool logic_uScript_AddOnScreenMessage_Out_261 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_261 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_263 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_263;

	private bool logic_uScriptCon_CompareBool_True_263 = true;

	private bool logic_uScriptCon_CompareBool_False_263 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_266;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_266 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_266 = "ShownMsgLeavingEarlyPostPurchase";

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_267 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_267 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_267 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_267;

	private string logic_uScript_AddOnScreenMessage_tag_267 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_267;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_267;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_267;

	private bool logic_uScript_AddOnScreenMessage_Out_267 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_267 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_271;

	private bool logic_uScriptCon_CompareBool_True_271 = true;

	private bool logic_uScriptCon_CompareBool_False_271 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_272 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_272;

	private bool logic_uScriptAct_SetBool_Out_272 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_272 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_272 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_275;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_275 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_275 = "ShownMsgLeavingEarlyDuringIntro";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_276 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_276;

	private bool logic_uScriptCon_CompareBool_True_276 = true;

	private bool logic_uScriptCon_CompareBool_False_276 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_280 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_280;

	private bool logic_uScriptAct_SetBool_Out_280 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_280 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_280 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_282;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_282 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_282 = "NPCSeen";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_284 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_284;

	private bool logic_uScriptAct_SetBool_Out_284 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_284 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_284 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_286 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_286;

	private bool logic_uScriptCon_CompareBool_True_286 = true;

	private bool logic_uScriptCon_CompareBool_False_286 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_287;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_287 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_287 = "GoToStartTrigger";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_288 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_288 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_290 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_290 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_290 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_290 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_290 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_292 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_292;

	private bool logic_uScriptAct_SetBool_Out_292 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_292 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_292 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_294 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_294 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_294 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_294;

	private string logic_uScript_AddOnScreenMessage_tag_294 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_294;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_294;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_294;

	private bool logic_uScript_AddOnScreenMessage_Out_294 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_294 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_297 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_297;

	private bool logic_uScriptCon_CompareBool_True_297 = true;

	private bool logic_uScriptCon_CompareBool_False_297 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_302 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_302;

	private bool logic_uScriptCon_CompareBool_True_302 = true;

	private bool logic_uScriptCon_CompareBool_False_302 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_303 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_303;

	private bool logic_uScriptAct_SetBool_Out_303 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_303 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_303 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_306 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_306 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_306 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_306 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_306 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_309 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_309 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_309 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_311 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_311;

	private bool logic_uScriptAct_SetBool_Out_311 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_311 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_311 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_312 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_312 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_312 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_312;

	private string logic_uScript_AddOnScreenMessage_tag_312 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_312;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_312;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_312;

	private bool logic_uScript_AddOnScreenMessage_Out_312 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_312 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_315 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_315;

	private bool logic_uScriptAct_SetBool_Out_315 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_315 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_315 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_317 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_317 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_317 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_317 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_317 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_319 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_319 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_319 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_319;

	private string logic_uScript_AddOnScreenMessage_tag_319 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_319;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_319;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_319;

	private bool logic_uScript_AddOnScreenMessage_Out_319 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_319 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_322 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_322 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_322 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_322 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_322 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_324 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_324;

	private bool logic_uScriptAct_SetBool_Out_324 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_324 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_324 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_325 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_325 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_327;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_327 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_327 = "ShownMsgYouCanReBuy";

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_329 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_329 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_329 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_329 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_329 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_331 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_331 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_331 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_331;

	private string logic_uScript_AddOnScreenMessage_tag_331 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_331;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_331;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_331;

	private bool logic_uScript_AddOnScreenMessage_Out_331 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_331 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_334 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_334;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_334 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_336 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_336;

	private bool logic_uScriptAct_SetBool_Out_336 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_336 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_336 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_339;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_339 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_339 = "PreviouslyBoughtVehicle";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_341;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_341 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_341 = "ShownMsgStartTooEarly";

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_342 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_342 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_342 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_342 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_342 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_345 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_345 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_345 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_345;

	private string logic_uScript_AddOnScreenMessage_tag_345 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_345;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_345;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_345;

	private bool logic_uScript_AddOnScreenMessage_Out_345 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_345 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_346 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_346;

	private bool logic_uScriptAct_SetBool_Out_346 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_346 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_346 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_347;

	private bool logic_uScriptCon_CompareBool_True_347 = true;

	private bool logic_uScriptCon_CompareBool_False_347 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_352 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_352 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_352 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_352;

	private string logic_uScript_AddOnScreenMessage_tag_352 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_352;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_352;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_352;

	private bool logic_uScript_AddOnScreenMessage_Out_352 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_352 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_355 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_355;

	private bool logic_uScriptAct_SetBool_Out_355 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_355 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_355 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_357 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_357;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_357;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_357;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_357;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_357;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_357;

	private bool logic_uScript_MissionPromptBlock_Show_Out_357 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_358 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_358;

	private bool logic_uScriptCon_CompareBool_True_358 = true;

	private bool logic_uScriptCon_CompareBool_False_358 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_359 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_359;

	private bool logic_uScriptCon_CompareBool_True_359 = true;

	private bool logic_uScriptCon_CompareBool_False_359 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_361 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_361;

	private bool logic_uScriptCon_CompareBool_True_361 = true;

	private bool logic_uScriptCon_CompareBool_False_361 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_363 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_363;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_363;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_363;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_363;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_363;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_363;

	private bool logic_uScript_MissionPromptBlock_Show_Out_363 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_365 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_365 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_365 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_365 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_365 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_370 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_370;

	private int logic_uScriptCon_CompareInt_B_370;

	private bool logic_uScriptCon_CompareInt_GreaterThan_370 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_370 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_370 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_370 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_370 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_370 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_372 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_372;

	private bool logic_uScriptAct_SetBool_Out_372 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_372 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_372 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_374 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_374;

	private bool logic_uScriptCon_CompareBool_True_374 = true;

	private bool logic_uScriptCon_CompareBool_False_374 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_375 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_375;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_375 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_379 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_379;

	private bool logic_uScriptAct_SetBool_Out_379 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_379 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_379 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_380;

	private bool logic_uScriptCon_CompareBool_True_380 = true;

	private bool logic_uScriptCon_CompareBool_False_380 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_383;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_383 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_383 = "ShownMsgYouCanReBuy";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_384 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_384 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_386 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_386 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_386 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_386 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_386 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_387 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_387 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_387 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_387;

	private string logic_uScript_AddOnScreenMessage_tag_387 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_387;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_387;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_387;

	private bool logic_uScript_AddOnScreenMessage_Out_387 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_387 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_388 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_388;

	private bool logic_uScriptAct_SetBool_Out_388 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_388 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_388 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_391 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_391;

	private bool logic_uScriptCon_CompareBool_True_391 = true;

	private bool logic_uScriptCon_CompareBool_False_391 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_396 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_396 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_396 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_396 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_396 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_397 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_397 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_397 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_397;

	private string logic_uScript_AddOnScreenMessage_tag_397 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_397;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_397;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_397;

	private bool logic_uScript_AddOnScreenMessage_Out_397 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_397 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_401 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_401;

	private bool logic_uScriptAct_SetBool_Out_401 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_401 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_401 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_402 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_402;

	private bool logic_uScriptCon_CompareBool_True_402 = true;

	private bool logic_uScriptCon_CompareBool_False_402 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_403 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_403 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_403 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_403;

	private string logic_uScript_AddOnScreenMessage_tag_403 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_403;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_403;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_403;

	private bool logic_uScript_AddOnScreenMessage_Out_403 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_403 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_407 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_407;

	private bool logic_uScriptAct_SetBool_Out_407 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_407 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_407 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_408 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_408 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_408 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_408 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_408 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_410 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_410;

	private bool logic_uScriptCon_CompareBool_True_410 = true;

	private bool logic_uScriptCon_CompareBool_False_410 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_412 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_412;

	private bool logic_uScriptCon_CompareBool_True_412 = true;

	private bool logic_uScriptCon_CompareBool_False_412 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_413 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_413 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_413 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_413;

	private string logic_uScript_AddOnScreenMessage_tag_413 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_413;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_413;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_413;

	private bool logic_uScript_AddOnScreenMessage_Out_413 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_413 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_414 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_414;

	private bool logic_uScriptAct_SetBool_Out_414 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_414 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_414 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_417 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_417 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_417 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_417 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_417 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_421 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_421 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_421 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_421;

	private string logic_uScript_AddOnScreenMessage_tag_421 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_421;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_421;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_421;

	private bool logic_uScript_AddOnScreenMessage_Out_421 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_421 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_424 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_424;

	private bool logic_uScriptAct_SetBool_Out_424 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_424 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_424 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_426 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_426 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_426 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_426 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_426 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_428 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_428;

	private bool logic_uScriptCon_CompareBool_True_428 = true;

	private bool logic_uScriptCon_CompareBool_False_428 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_432 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_432 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_432 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_432;

	private string logic_uScript_AddOnScreenMessage_tag_432 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_432;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_432;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_432;

	private bool logic_uScript_AddOnScreenMessage_Out_432 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_432 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_433 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_433 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_433 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_433 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_433 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_436 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_436;

	private bool logic_uScriptCon_CompareBool_True_436 = true;

	private bool logic_uScriptCon_CompareBool_False_436 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_437 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_437;

	private bool logic_uScriptAct_SetBool_Out_437 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_437 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_437 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_444;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_444 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_444 = "MsgShownRamp1";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_445;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_445 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_445 = "MsgShownRamp2";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_446;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_446 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_446 = "MsgShownRamp3";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_447;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_447 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_447 = "MsgShownBridge1";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_448;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_448 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_448 = "MsgShownBridge2";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_449;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_449 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_449 = "MsgShownBridge3";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_451 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_451;

	private bool logic_uScriptCon_CompareBool_True_451 = true;

	private bool logic_uScriptCon_CompareBool_False_451 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_453 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_453;

	private bool logic_uScriptAct_SetBool_Out_453 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_453 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_453 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_455;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_455 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_455 = "SaidMsgNPCVehicleControls";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_457 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_457;

	private bool logic_uScriptCon_CompareBool_True_457 = true;

	private bool logic_uScriptCon_CompareBool_False_457 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_459 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_459;

	private bool logic_uScriptCon_CompareBool_True_459 = true;

	private bool logic_uScriptCon_CompareBool_False_459 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_460 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_460 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_461 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_461;

	private bool logic_uScriptAct_SetBool_Out_461 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_461 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_461 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_464;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_464 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_464 = "ExplainedHowToBuy";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_468 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_468;

	private bool logic_uScriptCon_CompareBool_True_468 = true;

	private bool logic_uScriptCon_CompareBool_False_468 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_469 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_469 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_470 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_470;

	private bool logic_uScriptCon_CompareBool_True_470 = true;

	private bool logic_uScriptCon_CompareBool_False_470 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_471 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_471;

	private bool logic_uScriptCon_CompareBool_True_471 = true;

	private bool logic_uScriptCon_CompareBool_False_471 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_473 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_473 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_473 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_473;

	private string logic_uScript_AddOnScreenMessage_tag_473 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_473;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_473;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_473;

	private bool logic_uScript_AddOnScreenMessage_Out_473 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_473 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_477 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_477 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_479 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_479 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_479;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_479 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_479 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_479 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_480 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_480;

	private bool logic_uScriptAct_SetBool_Out_480 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_480 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_480 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_482 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_482 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_482 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_482 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_482 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_486 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_486;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_486 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_487 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_487 = "";

	private bool logic_uScript_EnableGlow_enable_487;

	private bool logic_uScript_EnableGlow_Out_487 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_488 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_488 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_490 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_490;

	private bool logic_uScriptCon_CompareBool_True_490 = true;

	private bool logic_uScriptCon_CompareBool_False_490 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_491 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_492 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_492 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_493 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_493 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_495 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_495 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_495 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_497 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_497 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_497 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_499;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_499 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_499 = "ShownMsgFellYouCanBuy";

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_500 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_500 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_500 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_500;

	private string logic_uScript_AddOnScreenMessage_tag_500 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_500;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_500;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_500;

	private bool logic_uScript_AddOnScreenMessage_Out_500 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_500 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_504 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_504;

	private bool logic_uScriptCon_CompareBool_True_504 = true;

	private bool logic_uScriptCon_CompareBool_False_504 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_507 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_507 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_507 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_507;

	private string logic_uScript_AddOnScreenMessage_tag_507 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_507;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_507;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_507;

	private bool logic_uScript_AddOnScreenMessage_Out_507 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_507 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_508 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_508 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_508 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_510 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_510;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_510 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_510 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_513 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_513;

	private float logic_uScriptCon_CompareFloat_B_513;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_513 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_513 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_513 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_513 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_513 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_513 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_514;

	private bool logic_uScriptCon_CompareBool_True_514 = true;

	private bool logic_uScriptCon_CompareBool_False_514 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_515 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_515 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_515 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_515;

	private string logic_uScript_AddOnScreenMessage_tag_515 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_515;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_515;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_515;

	private bool logic_uScript_AddOnScreenMessage_Out_515 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_515 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_516 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_516;

	private bool logic_uScriptAct_SetBool_Out_516 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_516 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_516 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_519 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_519 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_519 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_519 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_519 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_524;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_524 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_524 = "MsgShownRamp4";

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_525 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_525 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_525 = true;

	private uScript_ResetMissionTimer logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_527 = new uScript_ResetMissionTimer();

	private GameObject logic_uScript_ResetMissionTimer_owner_527;

	private bool logic_uScript_ResetMissionTimer_Out_527 = true;

	private uScript_StartMissionTimer logic_uScript_StartMissionTimer_uScript_StartMissionTimer_529 = new uScript_StartMissionTimer();

	private GameObject logic_uScript_StartMissionTimer_owner_529;

	private float logic_uScript_StartMissionTimer_startTime_529;

	private bool logic_uScript_StartMissionTimer_Out_529 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_531 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_531;

	private bool logic_uScript_PlayMiscSFX_Out_531 = true;

	private uScript_GetMissionTimerDisplayTime logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_533 = new uScript_GetMissionTimerDisplayTime();

	private GameObject logic_uScript_GetMissionTimerDisplayTime_owner_533;

	private float logic_uScript_GetMissionTimerDisplayTime_Return_533;

	private bool logic_uScript_GetMissionTimerDisplayTime_Out_533 = true;

	private uScript_ShowMissionTimerUI logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_534 = new uScript_ShowMissionTimerUI();

	private GameObject logic_uScript_ShowMissionTimerUI_owner_534;

	private bool logic_uScript_ShowMissionTimerUI_showBestTime_534;

	private bool logic_uScript_ShowMissionTimerUI_Out_534 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_536 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_536;

	private bool logic_uScript_StopMissionTimer_Out_536 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_537 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_537 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_538 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_538;

	private bool logic_uScript_HideMissionTimerUI_Out_538 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_540 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_540;

	private bool logic_uScript_PlayMiscSFX_Out_540 = true;

	private uScript_ResetMissionTimer logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_541 = new uScript_ResetMissionTimer();

	private GameObject logic_uScript_ResetMissionTimer_owner_541;

	private bool logic_uScript_ResetMissionTimer_Out_541 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_543 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_543;

	private bool logic_uScript_StopMissionTimer_Out_543 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_545 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_545;

	private bool logic_uScript_HideMissionTimerUI_Out_545 = true;

	private uScriptAct_MultiplyInt_v2 logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_548 = new uScriptAct_MultiplyInt_v2();

	private int logic_uScriptAct_MultiplyInt_v2_A_548;

	private int logic_uScriptAct_MultiplyInt_v2_B_548 = -1;

	private int logic_uScriptAct_MultiplyInt_v2_IntResult_548;

	private float logic_uScriptAct_MultiplyInt_v2_FloatResult_548;

	private bool logic_uScriptAct_MultiplyInt_v2_Out_548 = true;

	private uScript_AddMoney logic_uScript_AddMoney_uScript_AddMoney_549 = new uScript_AddMoney();

	private int logic_uScript_AddMoney_amount_549;

	private bool logic_uScript_AddMoney_Out_549 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_551 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_551;

	private bool logic_uScriptAct_SetBool_Out_551 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_551 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_551 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_552 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_552;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_552;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_552;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_552;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_552 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_552;

	private bool logic_uScript_MissionPromptBlock_Show_Out_552 = true;

	private uScript_CanSpawnPlayerTechsWithinBlockLimit logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_556 = new uScript_CanSpawnPlayerTechsWithinBlockLimit();

	private SpawnTechData[] logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_556 = new SpawnTechData[0];

	private int logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_556;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_Out_556 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_True_556 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_False_556 = true;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_559 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_559;

	private bool logic_uScript_GetMaxPlayers_Out_559 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_560 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_560 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_560 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_560;

	private string logic_uScript_AddOnScreenMessage_tag_560 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_560;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_560;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_560;

	private bool logic_uScript_AddOnScreenMessage_Out_560 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_560 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_563 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_563 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_563;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_563 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_563 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_563 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_565 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_565 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_565;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_565 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_565 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_565 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_570 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_570;

	private bool logic_uScriptCon_CompareBool_True_570 = true;

	private bool logic_uScriptCon_CompareBool_False_570 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_572 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_572;

	private bool logic_uScriptAct_SetBool_Out_572 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_572 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_572 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_573 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_573;

	private bool logic_uScriptAct_SetBool_Out_573 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_573 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_573 = true;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_578 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_578;

	private bool logic_uScript_GetMaxPlayers_Out_578 = true;

	private uScript_CanSpawnPlayerTechsWithinBlockLimit logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_579 = new uScript_CanSpawnPlayerTechsWithinBlockLimit();

	private SpawnTechData[] logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_579 = new SpawnTechData[0];

	private int logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_579;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_Out_579 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_True_579 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_False_579 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_581 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_581;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_581;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_581;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_581;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_581;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_581;

	private bool logic_uScript_MissionPromptBlock_Show_Out_581 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_584 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_584;

	private bool logic_uScriptAct_SetBool_Out_584 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_584 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_584 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_586;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_586 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_586 = "BlockLimitCritical";

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_587 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_587;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_587 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_588 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_588;

	private bool logic_uScriptAct_SetBool_Out_588 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_588 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_588 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_592 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_592 = 7f;

	private bool logic_uScript_Wait_repeat_592;

	private bool logic_uScript_Wait_Waited_592 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_595 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_595;

	private bool logic_uScriptAct_SetBool_Out_595 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_595 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_595 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_596 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_596 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_596;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_596 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_596;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_596 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_596 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_596 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_596 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_598 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_598 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_598;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_598 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_598;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_598 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_598 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_598 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_598 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_604 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_604 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_605 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_605 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_606 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_606;

	private bool logic_uScriptAct_SetBool_Out_606 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_606 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_606 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_608 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_608 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_608;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_608 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_608;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_608 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_608 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_608 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_608 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_610 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_610;

	private bool logic_uScriptAct_SetBool_Out_610 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_610 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_610 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_611 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_611;

	private bool logic_uScriptAct_SetBool_Out_611 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_611 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_611 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_616;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_616 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_616 = "Zoomer1Alive";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_617;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_617 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_617 = "Zoomer2Alive";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_618;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_618 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_618 = "Zoomer3Alive";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_620 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_620;

	private bool logic_uScriptCon_CompareBool_True_620 = true;

	private bool logic_uScriptCon_CompareBool_False_620 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_625 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_625;

	private bool logic_uScriptCon_CompareBool_True_625 = true;

	private bool logic_uScriptCon_CompareBool_False_625 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_626 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_626;

	private bool logic_uScriptCon_CompareBool_True_626 = true;

	private bool logic_uScriptCon_CompareBool_False_626 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_627 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_627;

	private bool logic_uScriptAct_SetBool_Out_627 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_627 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_627 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_632 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_632 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_632 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_632;

	private string logic_uScript_AddOnScreenMessage_tag_632 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_632;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_632;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_632;

	private bool logic_uScript_AddOnScreenMessage_Out_632 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_632 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_634 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_634 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_634;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_634 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_634;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_634 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_634 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_634 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_634 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_636 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_636 = new Tank[0];

	private int logic_uScript_AccessListTech_index_636;

	private Tank logic_uScript_AccessListTech_value_636;

	private bool logic_uScript_AccessListTech_Out_636 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_641 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_641 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_641;

	private bool logic_uScript_SetTankInvulnerable_Out_641 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_643 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_643 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_643;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_643 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_643;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_643 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_643 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_643 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_643 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_646 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_646 = new Tank[0];

	private int logic_uScript_AccessListTech_index_646;

	private Tank logic_uScript_AccessListTech_value_646;

	private bool logic_uScript_AccessListTech_Out_646 = true;

	private uScriptAct_Log logic_uScriptAct_Log_uScriptAct_Log_647 = new uScriptAct_Log();

	private object logic_uScriptAct_Log_Prefix_647 = "";

	private object[] logic_uScriptAct_Log_Target_647 = new object[0];

	private object logic_uScriptAct_Log_Postfix_647 = "";

	private bool logic_uScriptAct_Log_Out_647 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_651 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_651;

	private bool logic_uScript_PlayMiscSFX_Out_651 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_652 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_652;

	private bool logic_uScript_HideMissionTimerUI_Out_652 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_653 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_653;

	private bool logic_uScript_StopMissionTimer_Out_653 = true;

	private uScript_ResetMissionTimer logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_655 = new uScript_ResetMissionTimer();

	private GameObject logic_uScript_ResetMissionTimer_owner_655;

	private bool logic_uScript_ResetMissionTimer_Out_655 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_658;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_658 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_658 = "RunStarted";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_660;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_660 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_660 = "RunGoing";

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_662 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_662 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_662 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_664 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_664;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_666 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_666 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_667 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_667 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_668 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_668;

	private bool logic_uScriptCon_CompareBool_True_668 = true;

	private bool logic_uScriptCon_CompareBool_False_668 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_671 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_671 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_671 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_671;

	private string logic_uScript_AddOnScreenMessage_tag_671 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_671;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_671;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_671;

	private bool logic_uScript_AddOnScreenMessage_Out_671 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_671 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_674 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_674 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_674 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_674 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_674 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_676 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_676 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_676 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_676;

	private string logic_uScript_AddOnScreenMessage_tag_676 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_676;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_676;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_676;

	private bool logic_uScript_AddOnScreenMessage_Out_676 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_676 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_678 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_678 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_678 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_678 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_678 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_680 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_680 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_681 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_681 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_682 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_682 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_683 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_683 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_685 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_685;

	private bool logic_uScript_StopMissionTimer_Out_685 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_686 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_686;

	private bool logic_uScript_HideMissionTimerUI_Out_686 = true;

	private uScript_ResetMissionTimer logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_688 = new uScript_ResetMissionTimer();

	private GameObject logic_uScript_ResetMissionTimer_owner_688;

	private bool logic_uScript_ResetMissionTimer_Out_688 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_689 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_689 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_691 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_691 = "";

	private bool logic_uScript_EnableGlow_enable_691;

	private bool logic_uScript_EnableGlow_Out_691 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_692 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_692 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_694 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_694;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_694;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_694;

	private bool logic_uScript_AddMessage_Out_694 = true;

	private bool logic_uScript_AddMessage_Shown_694 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_695 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_695 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_698 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_698;

	private bool logic_uScriptAct_SetBool_Out_698 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_698 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_698 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_699 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_699;

	private bool logic_uScriptCon_CompareBool_True_699 = true;

	private bool logic_uScriptCon_CompareBool_False_699 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_704 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_704;

	private bool logic_uScriptAct_SetBool_Out_704 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_704 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_704 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_705 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_705;

	private bool logic_uScriptAct_SetBool_Out_705 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_705 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_705 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_706 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_706;

	private bool logic_uScriptAct_SetBool_Out_706 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_706 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_706 = true;

	private TankBlock event_UnityEngine_GameObject_TankBlock_182;

	private bool event_UnityEngine_GameObject_Accepted_182;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
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
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
			if (null != owner_Connection_7)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_7.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_7.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_6;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_6;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_6;
				}
			}
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (null == owner_Connection_31 || !m_RegisteredForEvents)
		{
			owner_Connection_31 = parentGameObject;
		}
		if (null == owner_Connection_65 || !m_RegisteredForEvents)
		{
			owner_Connection_65 = parentGameObject;
		}
		if (null == owner_Connection_68 || !m_RegisteredForEvents)
		{
			owner_Connection_68 = parentGameObject;
		}
		if (null == owner_Connection_118 || !m_RegisteredForEvents)
		{
			owner_Connection_118 = parentGameObject;
		}
		if (null == owner_Connection_127 || !m_RegisteredForEvents)
		{
			owner_Connection_127 = parentGameObject;
		}
		if (null == owner_Connection_154 || !m_RegisteredForEvents)
		{
			owner_Connection_154 = parentGameObject;
		}
		if (null == owner_Connection_171 || !m_RegisteredForEvents)
		{
			owner_Connection_171 = parentGameObject;
		}
		if (null == owner_Connection_179 || !m_RegisteredForEvents)
		{
			owner_Connection_179 = parentGameObject;
		}
		if (null == owner_Connection_209 || !m_RegisteredForEvents)
		{
			owner_Connection_209 = parentGameObject;
			if (null != owner_Connection_209)
			{
				uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_209.GetComponent<uScript_MissionPromptBlock_OnResult>();
				if (null == uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2 = owner_Connection_209.AddComponent<uScript_MissionPromptBlock_OnResult>();
				}
				if (null != uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_182;
				}
			}
		}
		if (null == owner_Connection_243 || !m_RegisteredForEvents)
		{
			owner_Connection_243 = parentGameObject;
		}
		if (null == owner_Connection_478 || !m_RegisteredForEvents)
		{
			owner_Connection_478 = parentGameObject;
		}
		if (null == owner_Connection_526 || !m_RegisteredForEvents)
		{
			owner_Connection_526 = parentGameObject;
		}
		if (null == owner_Connection_528 || !m_RegisteredForEvents)
		{
			owner_Connection_528 = parentGameObject;
		}
		if (null == owner_Connection_530 || !m_RegisteredForEvents)
		{
			owner_Connection_530 = parentGameObject;
		}
		if (null == owner_Connection_535 || !m_RegisteredForEvents)
		{
			owner_Connection_535 = parentGameObject;
		}
		if (null == owner_Connection_539 || !m_RegisteredForEvents)
		{
			owner_Connection_539 = parentGameObject;
		}
		if (null == owner_Connection_542 || !m_RegisteredForEvents)
		{
			owner_Connection_542 = parentGameObject;
		}
		if (null == owner_Connection_544 || !m_RegisteredForEvents)
		{
			owner_Connection_544 = parentGameObject;
		}
		if (null == owner_Connection_562 || !m_RegisteredForEvents)
		{
			owner_Connection_562 = parentGameObject;
		}
		if (null == owner_Connection_564 || !m_RegisteredForEvents)
		{
			owner_Connection_564 = parentGameObject;
		}
		if (null == owner_Connection_601 || !m_RegisteredForEvents)
		{
			owner_Connection_601 = parentGameObject;
		}
		if (null == owner_Connection_602 || !m_RegisteredForEvents)
		{
			owner_Connection_602 = parentGameObject;
		}
		if (null == owner_Connection_603 || !m_RegisteredForEvents)
		{
			owner_Connection_603 = parentGameObject;
		}
		if (null == owner_Connection_635 || !m_RegisteredForEvents)
		{
			owner_Connection_635 = parentGameObject;
		}
		if (null == owner_Connection_640 || !m_RegisteredForEvents)
		{
			owner_Connection_640 = parentGameObject;
		}
		if (null == owner_Connection_654 || !m_RegisteredForEvents)
		{
			owner_Connection_654 = parentGameObject;
		}
		if (null == owner_Connection_656 || !m_RegisteredForEvents)
		{
			owner_Connection_656 = parentGameObject;
		}
		if (null == owner_Connection_684 || !m_RegisteredForEvents)
		{
			owner_Connection_684 = parentGameObject;
		}
		if (null == owner_Connection_687 || !m_RegisteredForEvents)
		{
			owner_Connection_687 = parentGameObject;
		}
		if (null == owner_Connection_707 || !m_RegisteredForEvents)
		{
			owner_Connection_707 = parentGameObject;
		}
		if (null == owner_Connection_708 || !m_RegisteredForEvents)
		{
			owner_Connection_708 = parentGameObject;
		}
		if (null == owner_Connection_709 || !m_RegisteredForEvents)
		{
			owner_Connection_709 = parentGameObject;
		}
		if (null == owner_Connection_710 || !m_RegisteredForEvents)
		{
			owner_Connection_710 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
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
		if (!m_RegisteredForEvents && null != owner_Connection_7)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_7.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_7.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_6;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_6;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_6;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_209)
		{
			uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_209.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null == uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2 = owner_Connection_209.AddComponent<uScript_MissionPromptBlock_OnResult>();
			}
			if (null != uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_182;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_4)
		{
			uScript_SaveLoad component = owner_Connection_4.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_5;
				component.LoadEvent -= Instance_LoadEvent_5;
				component.RestartEvent -= Instance_RestartEvent_5;
			}
		}
		if (null != owner_Connection_7)
		{
			uScript_EncounterUpdate component2 = owner_Connection_7.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_6;
				component2.OnSuspend -= Instance_OnSuspend_6;
				component2.OnResume -= Instance_OnResume_6;
			}
		}
		if (null != owner_Connection_209)
		{
			uScript_MissionPromptBlock_OnResult component3 = owner_Connection_209.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null != component3)
			{
				component3.ResponseEvent -= Instance_ResponseEvent_182;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_10.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_13.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_14.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_30.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_33.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_34.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_41.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_49.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_53.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_58.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_60.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_61.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_70.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_71.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_76.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_78.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_80.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_81.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_84.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_85.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_89.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_96.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_98.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_112.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_120.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_121.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_122.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_129.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_135.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_136.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_137.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_138.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_140.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_142.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_144.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_147.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_148.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_151.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_155.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_156.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_163.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_164.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_170.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_173.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_176.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_177.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_185.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_189.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_193.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_194.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_196.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_199.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_204.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_205.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_206.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_210.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_211.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_212.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_218.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_223.SetParent(g);
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_225.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_230.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_231.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_233.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_234.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_236.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_237.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_240.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_242.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_245.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_247.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_250.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_251.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_254.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_260.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_261.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_263.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_267.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_272.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_276.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_280.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_284.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_286.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_288.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_290.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_292.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_294.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_297.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_302.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_303.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_306.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_309.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_311.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_312.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_315.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_317.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_319.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_322.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_324.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_325.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_329.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_331.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_334.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_336.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_342.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_345.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_346.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_352.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_355.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_357.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_358.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_359.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_361.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_363.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_365.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_370.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_372.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_374.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_375.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_379.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_384.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_386.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_387.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_388.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_391.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_396.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_397.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_401.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_402.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_403.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_407.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_408.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_410.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_412.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_413.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_414.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_417.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_421.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_424.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_426.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_428.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_432.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_433.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_436.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_437.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_451.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_453.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_457.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_459.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_460.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_461.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_468.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_469.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_470.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_471.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_473.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_477.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_479.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_480.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_482.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_486.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_487.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_488.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_490.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_492.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_493.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_495.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_497.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_500.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_504.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_507.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_508.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_510.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_513.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_515.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_516.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_519.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_525.SetParent(g);
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_527.SetParent(g);
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_529.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_531.SetParent(g);
		logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_533.SetParent(g);
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_534.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_536.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_537.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_538.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_540.SetParent(g);
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_541.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_543.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_545.SetParent(g);
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_548.SetParent(g);
		logic_uScript_AddMoney_uScript_AddMoney_549.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_551.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_552.SetParent(g);
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_556.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_559.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_560.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_563.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_565.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_570.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_572.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_573.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_578.SetParent(g);
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_579.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_581.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_584.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_587.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_588.SetParent(g);
		logic_uScript_Wait_uScript_Wait_592.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_595.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_596.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_598.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_604.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_605.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_606.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_608.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_610.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_611.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_620.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_625.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_626.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_627.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_632.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_634.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_636.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_641.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_643.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_646.SetParent(g);
		logic_uScriptAct_Log_uScriptAct_Log_647.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_651.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_652.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_653.SetParent(g);
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_655.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_662.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_664.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_666.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_667.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_668.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_671.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_674.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_676.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_678.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_680.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_681.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_682.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_683.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_685.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_686.SetParent(g);
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_688.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_689.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_691.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_692.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_694.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_695.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_698.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_699.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_704.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_705.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_706.SetParent(g);
		owner_Connection_4 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_31 = parentGameObject;
		owner_Connection_65 = parentGameObject;
		owner_Connection_68 = parentGameObject;
		owner_Connection_118 = parentGameObject;
		owner_Connection_127 = parentGameObject;
		owner_Connection_154 = parentGameObject;
		owner_Connection_171 = parentGameObject;
		owner_Connection_179 = parentGameObject;
		owner_Connection_209 = parentGameObject;
		owner_Connection_243 = parentGameObject;
		owner_Connection_478 = parentGameObject;
		owner_Connection_526 = parentGameObject;
		owner_Connection_528 = parentGameObject;
		owner_Connection_530 = parentGameObject;
		owner_Connection_535 = parentGameObject;
		owner_Connection_539 = parentGameObject;
		owner_Connection_542 = parentGameObject;
		owner_Connection_544 = parentGameObject;
		owner_Connection_562 = parentGameObject;
		owner_Connection_564 = parentGameObject;
		owner_Connection_601 = parentGameObject;
		owner_Connection_602 = parentGameObject;
		owner_Connection_603 = parentGameObject;
		owner_Connection_635 = parentGameObject;
		owner_Connection_640 = parentGameObject;
		owner_Connection_654 = parentGameObject;
		owner_Connection_656 = parentGameObject;
		owner_Connection_684 = parentGameObject;
		owner_Connection_687 = parentGameObject;
		owner_Connection_707 = parentGameObject;
		owner_Connection_708 = parentGameObject;
		owner_Connection_709 = parentGameObject;
		owner_Connection_710 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_334.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_664.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Save_Out += SubGraph_SaveLoadBool_Save_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Load_Out += SubGraph_SaveLoadBool_Load_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_3;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Save_Out += SubGraph_SaveLoadInt_Save_Out_24;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Load_Out += SubGraph_SaveLoadInt_Load_Out_24;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_24;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.Out += SubGraph_CompleteObjectiveStage_Out_26;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Save_Out += SubGraph_SaveLoadBool_Save_Out_29;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Load_Out += SubGraph_SaveLoadBool_Load_Out_29;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_29;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Save_Out += SubGraph_SaveLoadBool_Save_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Load_Out += SubGraph_SaveLoadBool_Load_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Save_Out += SubGraph_SaveLoadBool_Save_Out_46;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Load_Out += SubGraph_SaveLoadBool_Load_Out_46;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_46;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51.Out += SubGraph_AddMessageWithPadSupport_Out_51;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51.Shown += SubGraph_AddMessageWithPadSupport_Shown_51;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Out += SubGraph_AddMessageWithPadSupport_Out_64;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Shown += SubGraph_AddMessageWithPadSupport_Shown_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save_Out += SubGraph_SaveLoadBool_Save_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load_Out += SubGraph_SaveLoadBool_Load_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Save_Out += SubGraph_SaveLoadBool_Save_Out_94;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Load_Out += SubGraph_SaveLoadBool_Load_Out_94;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_94;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Save_Out += SubGraph_SaveLoadBool_Save_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Load_Out += SubGraph_SaveLoadBool_Load_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Save_Out += SubGraph_SaveLoadBool_Save_Out_102;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Load_Out += SubGraph_SaveLoadBool_Load_Out_102;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_102;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Save_Out += SubGraph_SaveLoadBool_Save_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Load_Out += SubGraph_SaveLoadBool_Load_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Save_Out += SubGraph_SaveLoadBool_Save_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Load_Out += SubGraph_SaveLoadBool_Load_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Save_Out += SubGraph_SaveLoadBool_Save_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Load_Out += SubGraph_SaveLoadBool_Load_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Save_Out += SubGraph_SaveLoadBool_Save_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Load_Out += SubGraph_SaveLoadBool_Load_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Save_Out += SubGraph_SaveLoadBool_Save_Out_266;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Load_Out += SubGraph_SaveLoadBool_Load_Out_266;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_266;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Save_Out += SubGraph_SaveLoadBool_Save_Out_275;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Load_Out += SubGraph_SaveLoadBool_Load_Out_275;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_275;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Save_Out += SubGraph_SaveLoadBool_Save_Out_282;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Load_Out += SubGraph_SaveLoadBool_Load_Out_282;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_282;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Save_Out += SubGraph_SaveLoadBool_Save_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Load_Out += SubGraph_SaveLoadBool_Load_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Save_Out += SubGraph_SaveLoadBool_Save_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Load_Out += SubGraph_SaveLoadBool_Load_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_327;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_334.Out += SubGraph_CompleteObjectiveStage_Out_334;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Save_Out += SubGraph_SaveLoadBool_Save_Out_339;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Load_Out += SubGraph_SaveLoadBool_Load_Out_339;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_339;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Save_Out += SubGraph_SaveLoadBool_Save_Out_341;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Load_Out += SubGraph_SaveLoadBool_Load_Out_341;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_341;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Save_Out += SubGraph_SaveLoadBool_Save_Out_383;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Load_Out += SubGraph_SaveLoadBool_Load_Out_383;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_383;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Save_Out += SubGraph_SaveLoadBool_Save_Out_444;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Load_Out += SubGraph_SaveLoadBool_Load_Out_444;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_444;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Save_Out += SubGraph_SaveLoadBool_Save_Out_445;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Load_Out += SubGraph_SaveLoadBool_Load_Out_445;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_445;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Save_Out += SubGraph_SaveLoadBool_Save_Out_446;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Load_Out += SubGraph_SaveLoadBool_Load_Out_446;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_446;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Save_Out += SubGraph_SaveLoadBool_Save_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Load_Out += SubGraph_SaveLoadBool_Load_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Save_Out += SubGraph_SaveLoadBool_Save_Out_448;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Load_Out += SubGraph_SaveLoadBool_Load_Out_448;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_448;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Save_Out += SubGraph_SaveLoadBool_Save_Out_449;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Load_Out += SubGraph_SaveLoadBool_Load_Out_449;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_449;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Save_Out += SubGraph_SaveLoadBool_Save_Out_455;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Load_Out += SubGraph_SaveLoadBool_Load_Out_455;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_455;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Save_Out += SubGraph_SaveLoadBool_Save_Out_464;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Load_Out += SubGraph_SaveLoadBool_Load_Out_464;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_464;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Save_Out += SubGraph_SaveLoadBool_Save_Out_499;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Load_Out += SubGraph_SaveLoadBool_Load_Out_499;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_499;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Save_Out += SubGraph_SaveLoadBool_Save_Out_524;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Load_Out += SubGraph_SaveLoadBool_Load_Out_524;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_524;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Save_Out += SubGraph_SaveLoadBool_Save_Out_586;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Load_Out += SubGraph_SaveLoadBool_Load_Out_586;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_586;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Save_Out += SubGraph_SaveLoadBool_Save_Out_616;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Load_Out += SubGraph_SaveLoadBool_Load_Out_616;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_616;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Save_Out += SubGraph_SaveLoadBool_Save_Out_617;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Load_Out += SubGraph_SaveLoadBool_Load_Out_617;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_617;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Save_Out += SubGraph_SaveLoadBool_Save_Out_618;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Load_Out += SubGraph_SaveLoadBool_Load_Out_618;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_618;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Save_Out += SubGraph_SaveLoadBool_Save_Out_658;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Load_Out += SubGraph_SaveLoadBool_Load_Out_658;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_658;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Save_Out += SubGraph_SaveLoadBool_Save_Out_660;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Load_Out += SubGraph_SaveLoadBool_Load_Out_660;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_660;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_664.Out += SubGraph_LoadObjectiveStates_Out_664;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_334.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_664.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_76.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_156.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_245.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_334.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_664.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_14.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_34.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_49.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_85.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_163.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_173.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_206.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_218.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_251.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_261.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_267.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_294.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_312.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_319.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_331.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_334.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_345.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_352.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_387.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_397.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_403.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_413.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_421.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_432.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_473.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_500.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_507.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_515.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.OnDisable();
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_556.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_560.OnDisable();
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_579.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.OnDisable();
		logic_uScript_Wait_uScript_Wait_592.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_632.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_641.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_664.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_671.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_676.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_694.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_334.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_664.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_334.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_664.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Save_Out -= SubGraph_SaveLoadBool_Save_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Load_Out -= SubGraph_SaveLoadBool_Load_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_3;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Save_Out -= SubGraph_SaveLoadInt_Save_Out_24;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Load_Out -= SubGraph_SaveLoadInt_Load_Out_24;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_24;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.Out -= SubGraph_CompleteObjectiveStage_Out_26;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Save_Out -= SubGraph_SaveLoadBool_Save_Out_29;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Load_Out -= SubGraph_SaveLoadBool_Load_Out_29;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_29;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Save_Out -= SubGraph_SaveLoadBool_Save_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Load_Out -= SubGraph_SaveLoadBool_Load_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Save_Out -= SubGraph_SaveLoadBool_Save_Out_46;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Load_Out -= SubGraph_SaveLoadBool_Load_Out_46;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_46;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51.Out -= SubGraph_AddMessageWithPadSupport_Out_51;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51.Shown -= SubGraph_AddMessageWithPadSupport_Shown_51;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Out -= SubGraph_AddMessageWithPadSupport_Out_64;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Shown -= SubGraph_AddMessageWithPadSupport_Shown_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save_Out -= SubGraph_SaveLoadBool_Save_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load_Out -= SubGraph_SaveLoadBool_Load_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Save_Out -= SubGraph_SaveLoadBool_Save_Out_94;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Load_Out -= SubGraph_SaveLoadBool_Load_Out_94;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_94;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Save_Out -= SubGraph_SaveLoadBool_Save_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Load_Out -= SubGraph_SaveLoadBool_Load_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Save_Out -= SubGraph_SaveLoadBool_Save_Out_102;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Load_Out -= SubGraph_SaveLoadBool_Load_Out_102;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_102;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Save_Out -= SubGraph_SaveLoadBool_Save_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Load_Out -= SubGraph_SaveLoadBool_Load_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Save_Out -= SubGraph_SaveLoadBool_Save_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Load_Out -= SubGraph_SaveLoadBool_Load_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Save_Out -= SubGraph_SaveLoadBool_Save_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Load_Out -= SubGraph_SaveLoadBool_Load_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Save_Out -= SubGraph_SaveLoadBool_Save_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Load_Out -= SubGraph_SaveLoadBool_Load_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Save_Out -= SubGraph_SaveLoadBool_Save_Out_266;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Load_Out -= SubGraph_SaveLoadBool_Load_Out_266;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_266;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Save_Out -= SubGraph_SaveLoadBool_Save_Out_275;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Load_Out -= SubGraph_SaveLoadBool_Load_Out_275;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_275;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Save_Out -= SubGraph_SaveLoadBool_Save_Out_282;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Load_Out -= SubGraph_SaveLoadBool_Load_Out_282;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_282;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Save_Out -= SubGraph_SaveLoadBool_Save_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Load_Out -= SubGraph_SaveLoadBool_Load_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Save_Out -= SubGraph_SaveLoadBool_Save_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Load_Out -= SubGraph_SaveLoadBool_Load_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_327;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_334.Out -= SubGraph_CompleteObjectiveStage_Out_334;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Save_Out -= SubGraph_SaveLoadBool_Save_Out_339;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Load_Out -= SubGraph_SaveLoadBool_Load_Out_339;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_339;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Save_Out -= SubGraph_SaveLoadBool_Save_Out_341;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Load_Out -= SubGraph_SaveLoadBool_Load_Out_341;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_341;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Save_Out -= SubGraph_SaveLoadBool_Save_Out_383;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Load_Out -= SubGraph_SaveLoadBool_Load_Out_383;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_383;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Save_Out -= SubGraph_SaveLoadBool_Save_Out_444;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Load_Out -= SubGraph_SaveLoadBool_Load_Out_444;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_444;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Save_Out -= SubGraph_SaveLoadBool_Save_Out_445;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Load_Out -= SubGraph_SaveLoadBool_Load_Out_445;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_445;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Save_Out -= SubGraph_SaveLoadBool_Save_Out_446;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Load_Out -= SubGraph_SaveLoadBool_Load_Out_446;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_446;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Save_Out -= SubGraph_SaveLoadBool_Save_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Load_Out -= SubGraph_SaveLoadBool_Load_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_447;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Save_Out -= SubGraph_SaveLoadBool_Save_Out_448;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Load_Out -= SubGraph_SaveLoadBool_Load_Out_448;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_448;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Save_Out -= SubGraph_SaveLoadBool_Save_Out_449;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Load_Out -= SubGraph_SaveLoadBool_Load_Out_449;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_449;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Save_Out -= SubGraph_SaveLoadBool_Save_Out_455;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Load_Out -= SubGraph_SaveLoadBool_Load_Out_455;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_455;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Save_Out -= SubGraph_SaveLoadBool_Save_Out_464;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Load_Out -= SubGraph_SaveLoadBool_Load_Out_464;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_464;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Save_Out -= SubGraph_SaveLoadBool_Save_Out_499;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Load_Out -= SubGraph_SaveLoadBool_Load_Out_499;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_499;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Save_Out -= SubGraph_SaveLoadBool_Save_Out_524;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Load_Out -= SubGraph_SaveLoadBool_Load_Out_524;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_524;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Save_Out -= SubGraph_SaveLoadBool_Save_Out_586;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Load_Out -= SubGraph_SaveLoadBool_Load_Out_586;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_586;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Save_Out -= SubGraph_SaveLoadBool_Save_Out_616;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Load_Out -= SubGraph_SaveLoadBool_Load_Out_616;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_616;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Save_Out -= SubGraph_SaveLoadBool_Save_Out_617;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Load_Out -= SubGraph_SaveLoadBool_Load_Out_617;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_617;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Save_Out -= SubGraph_SaveLoadBool_Save_Out_618;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Load_Out -= SubGraph_SaveLoadBool_Load_Out_618;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_618;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Save_Out -= SubGraph_SaveLoadBool_Save_Out_658;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Load_Out -= SubGraph_SaveLoadBool_Load_Out_658;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_658;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Save_Out -= SubGraph_SaveLoadBool_Save_Out_660;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Load_Out -= SubGraph_SaveLoadBool_Load_Out_660;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_660;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_664.Out -= SubGraph_LoadObjectiveStates_Out_664;
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

	private void Instance_OnUpdate_6(object o, EventArgs e)
	{
		Relay_OnUpdate_6();
	}

	private void Instance_OnSuspend_6(object o, EventArgs e)
	{
		Relay_OnSuspend_6();
	}

	private void Instance_OnResume_6(object o, EventArgs e)
	{
		Relay_OnResume_6();
	}

	private void Instance_ResponseEvent_182(object o, uScript_MissionPromptBlock_OnResult.PromptResultEventArgs e)
	{
		event_UnityEngine_GameObject_TankBlock_182 = e.TankBlock;
		event_UnityEngine_GameObject_Accepted_182 = e.Accepted;
		Relay_ResponseEvent_182();
	}

	private void SubGraph_SaveLoadBool_Save_Out_3(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_3;
		Relay_Save_Out_3();
	}

	private void SubGraph_SaveLoadBool_Load_Out_3(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_3;
		Relay_Load_Out_3();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_3(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_3;
		Relay_Restart_Out_3();
	}

	private void SubGraph_SaveLoadInt_Save_Out_24(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_24 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_24;
		Relay_Save_Out_24();
	}

	private void SubGraph_SaveLoadInt_Load_Out_24(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_24 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_24;
		Relay_Load_Out_24();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_24(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_24 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_24;
		Relay_Restart_Out_24();
	}

	private void SubGraph_CompleteObjectiveStage_Out_26(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_26 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_26;
		Relay_Out_26();
	}

	private void SubGraph_SaveLoadBool_Save_Out_29(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = e.boolean;
		local_TechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_29;
		Relay_Save_Out_29();
	}

	private void SubGraph_SaveLoadBool_Load_Out_29(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = e.boolean;
		local_TechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_29;
		Relay_Load_Out_29();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_29(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = e.boolean;
		local_TechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_29;
		Relay_Restart_Out_29();
	}

	private void SubGraph_SaveLoadBool_Save_Out_40(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_40;
		Relay_Save_Out_40();
	}

	private void SubGraph_SaveLoadBool_Load_Out_40(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_40;
		Relay_Load_Out_40();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_40(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_40;
		Relay_Restart_Out_40();
	}

	private void SubGraph_SaveLoadBool_Save_Out_46(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_46 = e.boolean;
		local_NPCIntroMessagePlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_46;
		Relay_Save_Out_46();
	}

	private void SubGraph_SaveLoadBool_Load_Out_46(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_46 = e.boolean;
		local_NPCIntroMessagePlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_46;
		Relay_Load_Out_46();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_46(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_46 = e.boolean;
		local_NPCIntroMessagePlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_46;
		Relay_Restart_Out_46();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_51(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_51 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_51 = e.messageControlPadReturn;
		local_MsgPurchaseVehicle_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_51;
		local_MsgPurchaseVehicle_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_51;
		Relay_Out_51();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_51(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_51 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_51 = e.messageControlPadReturn;
		local_MsgPurchaseVehicle_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_51;
		local_MsgPurchaseVehicle_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_51;
		Relay_Shown_51();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_64(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_64 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_64 = e.messageControlPadReturn;
		local_MsgSwitchTech_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_64;
		local_MsgSwitchTech_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_64;
		Relay_Out_64();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_64(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_64 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_64 = e.messageControlPadReturn;
		local_MsgSwitchTech_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_64;
		local_MsgSwitchTech_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_64;
		Relay_Shown_64();
	}

	private void SubGraph_SaveLoadBool_Save_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_SwitchedVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Save_Out_93();
	}

	private void SubGraph_SaveLoadBool_Load_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_SwitchedVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Load_Out_93();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_SwitchedVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Restart_Out_93();
	}

	private void SubGraph_SaveLoadBool_Save_Out_94(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_94;
		Relay_Save_Out_94();
	}

	private void SubGraph_SaveLoadBool_Load_Out_94(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_94;
		Relay_Load_Out_94();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_94(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_94;
		Relay_Restart_Out_94();
	}

	private void SubGraph_SaveLoadBool_Save_Out_100(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_100;
		Relay_Save_Out_100();
	}

	private void SubGraph_SaveLoadBool_Load_Out_100(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_100;
		Relay_Load_Out_100();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_100(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_100;
		Relay_Restart_Out_100();
	}

	private void SubGraph_SaveLoadBool_Save_Out_102(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = e.boolean;
		local_WaitingOnPrompt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_102;
		Relay_Save_Out_102();
	}

	private void SubGraph_SaveLoadBool_Load_Out_102(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = e.boolean;
		local_WaitingOnPrompt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_102;
		Relay_Load_Out_102();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_102(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = e.boolean;
		local_WaitingOnPrompt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_102;
		Relay_Restart_Out_102();
	}

	private void SubGraph_SaveLoadBool_Save_Out_111(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = e.boolean;
		local_TechSetUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_111;
		Relay_Save_Out_111();
	}

	private void SubGraph_SaveLoadBool_Load_Out_111(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = e.boolean;
		local_TechSetUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_111;
		Relay_Load_Out_111();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_111(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = e.boolean;
		local_TechSetUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_111;
		Relay_Restart_Out_111();
	}

	private void SubGraph_SaveLoadBool_Save_Out_125(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = e.boolean;
		local_VehicleSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_125;
		Relay_Save_Out_125();
	}

	private void SubGraph_SaveLoadBool_Load_Out_125(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = e.boolean;
		local_VehicleSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_125;
		Relay_Load_Out_125();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_125(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = e.boolean;
		local_VehicleSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_125;
		Relay_Restart_Out_125();
	}

	private void SubGraph_SaveLoadBool_Save_Out_168(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_168 = e.boolean;
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_168;
		Relay_Save_Out_168();
	}

	private void SubGraph_SaveLoadBool_Load_Out_168(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_168 = e.boolean;
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_168;
		Relay_Load_Out_168();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_168(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_168 = e.boolean;
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_168;
		Relay_Restart_Out_168();
	}

	private void SubGraph_SaveLoadBool_Save_Out_257(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = e.boolean;
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_257;
		Relay_Save_Out_257();
	}

	private void SubGraph_SaveLoadBool_Load_Out_257(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = e.boolean;
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_257;
		Relay_Load_Out_257();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_257(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = e.boolean;
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_257;
		Relay_Restart_Out_257();
	}

	private void SubGraph_SaveLoadBool_Save_Out_266(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_266 = e.boolean;
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_266;
		Relay_Save_Out_266();
	}

	private void SubGraph_SaveLoadBool_Load_Out_266(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_266 = e.boolean;
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_266;
		Relay_Load_Out_266();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_266(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_266 = e.boolean;
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_266;
		Relay_Restart_Out_266();
	}

	private void SubGraph_SaveLoadBool_Save_Out_275(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_275 = e.boolean;
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_275;
		Relay_Save_Out_275();
	}

	private void SubGraph_SaveLoadBool_Load_Out_275(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_275 = e.boolean;
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_275;
		Relay_Load_Out_275();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_275(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_275 = e.boolean;
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_275;
		Relay_Restart_Out_275();
	}

	private void SubGraph_SaveLoadBool_Save_Out_282(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_282 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_282;
		Relay_Save_Out_282();
	}

	private void SubGraph_SaveLoadBool_Load_Out_282(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_282 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_282;
		Relay_Load_Out_282();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_282(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_282 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_282;
		Relay_Restart_Out_282();
	}

	private void SubGraph_SaveLoadBool_Save_Out_287(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = e.boolean;
		local_GoToStartTrigger_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_287;
		Relay_Save_Out_287();
	}

	private void SubGraph_SaveLoadBool_Load_Out_287(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = e.boolean;
		local_GoToStartTrigger_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_287;
		Relay_Load_Out_287();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_287(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = e.boolean;
		local_GoToStartTrigger_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_287;
		Relay_Restart_Out_287();
	}

	private void SubGraph_SaveLoadBool_Save_Out_327(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = e.boolean;
		local_ShownMsgYouCanReBuy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_327;
		Relay_Save_Out_327();
	}

	private void SubGraph_SaveLoadBool_Load_Out_327(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = e.boolean;
		local_ShownMsgYouCanReBuy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_327;
		Relay_Load_Out_327();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_327(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = e.boolean;
		local_ShownMsgYouCanReBuy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_327;
		Relay_Restart_Out_327();
	}

	private void SubGraph_CompleteObjectiveStage_Out_334(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_334 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_334;
		Relay_Out_334();
	}

	private void SubGraph_SaveLoadBool_Save_Out_339(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_339 = e.boolean;
		local_PreviouslyBoughtVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_339;
		Relay_Save_Out_339();
	}

	private void SubGraph_SaveLoadBool_Load_Out_339(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_339 = e.boolean;
		local_PreviouslyBoughtVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_339;
		Relay_Load_Out_339();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_339(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_339 = e.boolean;
		local_PreviouslyBoughtVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_339;
		Relay_Restart_Out_339();
	}

	private void SubGraph_SaveLoadBool_Save_Out_341(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_341 = e.boolean;
		local_ShownMsgStartTooEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_341;
		Relay_Save_Out_341();
	}

	private void SubGraph_SaveLoadBool_Load_Out_341(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_341 = e.boolean;
		local_ShownMsgStartTooEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_341;
		Relay_Load_Out_341();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_341(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_341 = e.boolean;
		local_ShownMsgStartTooEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_341;
		Relay_Restart_Out_341();
	}

	private void SubGraph_SaveLoadBool_Save_Out_383(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = e.boolean;
		local_ShownMsgYouCanReBuy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_383;
		Relay_Save_Out_383();
	}

	private void SubGraph_SaveLoadBool_Load_Out_383(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = e.boolean;
		local_ShownMsgYouCanReBuy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_383;
		Relay_Load_Out_383();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_383(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = e.boolean;
		local_ShownMsgYouCanReBuy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_383;
		Relay_Restart_Out_383();
	}

	private void SubGraph_SaveLoadBool_Save_Out_444(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_444 = e.boolean;
		local_MsgShownRamp1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_444;
		Relay_Save_Out_444();
	}

	private void SubGraph_SaveLoadBool_Load_Out_444(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_444 = e.boolean;
		local_MsgShownRamp1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_444;
		Relay_Load_Out_444();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_444(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_444 = e.boolean;
		local_MsgShownRamp1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_444;
		Relay_Restart_Out_444();
	}

	private void SubGraph_SaveLoadBool_Save_Out_445(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = e.boolean;
		local_MsgShownRamp2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_445;
		Relay_Save_Out_445();
	}

	private void SubGraph_SaveLoadBool_Load_Out_445(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = e.boolean;
		local_MsgShownRamp2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_445;
		Relay_Load_Out_445();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_445(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = e.boolean;
		local_MsgShownRamp2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_445;
		Relay_Restart_Out_445();
	}

	private void SubGraph_SaveLoadBool_Save_Out_446(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = e.boolean;
		local_MsgShownRamp3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_446;
		Relay_Save_Out_446();
	}

	private void SubGraph_SaveLoadBool_Load_Out_446(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = e.boolean;
		local_MsgShownRamp3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_446;
		Relay_Load_Out_446();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_446(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = e.boolean;
		local_MsgShownRamp3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_446;
		Relay_Restart_Out_446();
	}

	private void SubGraph_SaveLoadBool_Save_Out_447(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = e.boolean;
		local_MsgShownBridge1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_447;
		Relay_Save_Out_447();
	}

	private void SubGraph_SaveLoadBool_Load_Out_447(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = e.boolean;
		local_MsgShownBridge1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_447;
		Relay_Load_Out_447();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_447(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = e.boolean;
		local_MsgShownBridge1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_447;
		Relay_Restart_Out_447();
	}

	private void SubGraph_SaveLoadBool_Save_Out_448(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_448 = e.boolean;
		local_MsgShownBridge2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_448;
		Relay_Save_Out_448();
	}

	private void SubGraph_SaveLoadBool_Load_Out_448(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_448 = e.boolean;
		local_MsgShownBridge2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_448;
		Relay_Load_Out_448();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_448(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_448 = e.boolean;
		local_MsgShownBridge2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_448;
		Relay_Restart_Out_448();
	}

	private void SubGraph_SaveLoadBool_Save_Out_449(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_449 = e.boolean;
		local_MsgShownBridge3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_449;
		Relay_Save_Out_449();
	}

	private void SubGraph_SaveLoadBool_Load_Out_449(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_449 = e.boolean;
		local_MsgShownBridge3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_449;
		Relay_Load_Out_449();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_449(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_449 = e.boolean;
		local_MsgShownBridge3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_449;
		Relay_Restart_Out_449();
	}

	private void SubGraph_SaveLoadBool_Save_Out_455(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_455 = e.boolean;
		local_SaidMsgNPCVehicleControls_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_455;
		Relay_Save_Out_455();
	}

	private void SubGraph_SaveLoadBool_Load_Out_455(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_455 = e.boolean;
		local_SaidMsgNPCVehicleControls_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_455;
		Relay_Load_Out_455();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_455(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_455 = e.boolean;
		local_SaidMsgNPCVehicleControls_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_455;
		Relay_Restart_Out_455();
	}

	private void SubGraph_SaveLoadBool_Save_Out_464(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_464 = e.boolean;
		local_ExplainedHowToBuy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_464;
		Relay_Save_Out_464();
	}

	private void SubGraph_SaveLoadBool_Load_Out_464(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_464 = e.boolean;
		local_ExplainedHowToBuy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_464;
		Relay_Load_Out_464();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_464(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_464 = e.boolean;
		local_ExplainedHowToBuy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_464;
		Relay_Restart_Out_464();
	}

	private void SubGraph_SaveLoadBool_Save_Out_499(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_499 = e.boolean;
		local_ShownMsgFellYouCanBuy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_499;
		Relay_Save_Out_499();
	}

	private void SubGraph_SaveLoadBool_Load_Out_499(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_499 = e.boolean;
		local_ShownMsgFellYouCanBuy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_499;
		Relay_Load_Out_499();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_499(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_499 = e.boolean;
		local_ShownMsgFellYouCanBuy_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_499;
		Relay_Restart_Out_499();
	}

	private void SubGraph_SaveLoadBool_Save_Out_524(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = e.boolean;
		local_MsgShownRamp4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_524;
		Relay_Save_Out_524();
	}

	private void SubGraph_SaveLoadBool_Load_Out_524(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = e.boolean;
		local_MsgShownRamp4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_524;
		Relay_Load_Out_524();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_524(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = e.boolean;
		local_MsgShownRamp4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_524;
		Relay_Restart_Out_524();
	}

	private void SubGraph_SaveLoadBool_Save_Out_586(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_586 = e.boolean;
		local_BlockLimitCritical_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_586;
		Relay_Save_Out_586();
	}

	private void SubGraph_SaveLoadBool_Load_Out_586(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_586 = e.boolean;
		local_BlockLimitCritical_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_586;
		Relay_Load_Out_586();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_586(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_586 = e.boolean;
		local_BlockLimitCritical_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_586;
		Relay_Restart_Out_586();
	}

	private void SubGraph_SaveLoadBool_Save_Out_616(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = e.boolean;
		local_Zoomer1Alive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_616;
		Relay_Save_Out_616();
	}

	private void SubGraph_SaveLoadBool_Load_Out_616(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = e.boolean;
		local_Zoomer1Alive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_616;
		Relay_Load_Out_616();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_616(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = e.boolean;
		local_Zoomer1Alive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_616;
		Relay_Restart_Out_616();
	}

	private void SubGraph_SaveLoadBool_Save_Out_617(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_617 = e.boolean;
		local_Zoomer2Alive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_617;
		Relay_Save_Out_617();
	}

	private void SubGraph_SaveLoadBool_Load_Out_617(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_617 = e.boolean;
		local_Zoomer2Alive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_617;
		Relay_Load_Out_617();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_617(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_617 = e.boolean;
		local_Zoomer2Alive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_617;
		Relay_Restart_Out_617();
	}

	private void SubGraph_SaveLoadBool_Save_Out_618(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_618 = e.boolean;
		local_Zoomer3Alive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_618;
		Relay_Save_Out_618();
	}

	private void SubGraph_SaveLoadBool_Load_Out_618(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_618 = e.boolean;
		local_Zoomer3Alive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_618;
		Relay_Load_Out_618();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_618(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_618 = e.boolean;
		local_Zoomer3Alive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_618;
		Relay_Restart_Out_618();
	}

	private void SubGraph_SaveLoadBool_Save_Out_658(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_658 = e.boolean;
		local_RunStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_658;
		Relay_Save_Out_658();
	}

	private void SubGraph_SaveLoadBool_Load_Out_658(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_658 = e.boolean;
		local_RunStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_658;
		Relay_Load_Out_658();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_658(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_658 = e.boolean;
		local_RunStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_658;
		Relay_Restart_Out_658();
	}

	private void SubGraph_SaveLoadBool_Save_Out_660(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_660 = e.boolean;
		local_RunGoing_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_660;
		Relay_Save_Out_660();
	}

	private void SubGraph_SaveLoadBool_Load_Out_660(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_660 = e.boolean;
		local_RunGoing_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_660;
		Relay_Load_Out_660();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_660(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_660 = e.boolean;
		local_RunGoing_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_660;
		Relay_Restart_Out_660();
	}

	private void SubGraph_LoadObjectiveStates_Out_664(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_664();
	}

	private void Relay_True_2()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.True(out logic_uScriptAct_SetBool_Target_2);
		local_TechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_2;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_2.Out)
		{
			Relay_In_608();
		}
	}

	private void Relay_False_2()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.False(out logic_uScriptAct_SetBool_Target_2);
		local_TechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_2;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_2.Out)
		{
			Relay_In_608();
		}
	}

	private void Relay_Save_Out_3()
	{
		Relay_Save_29();
	}

	private void Relay_Load_Out_3()
	{
		Relay_Load_29();
	}

	private void Relay_Restart_Out_3()
	{
		Relay_Set_False_29();
	}

	private void Relay_Save_3()
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_3 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Save(ref logic_SubGraph_SaveLoadBool_boolean_3, logic_SubGraph_SaveLoadBool_boolAsVariable_3, logic_SubGraph_SaveLoadBool_uniqueID_3);
	}

	private void Relay_Load_3()
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_3 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Load(ref logic_SubGraph_SaveLoadBool_boolean_3, logic_SubGraph_SaveLoadBool_boolAsVariable_3, logic_SubGraph_SaveLoadBool_uniqueID_3);
	}

	private void Relay_Set_True_3()
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_3 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_3, logic_SubGraph_SaveLoadBool_boolAsVariable_3, logic_SubGraph_SaveLoadBool_uniqueID_3);
	}

	private void Relay_Set_False_3()
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_3 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_3, logic_SubGraph_SaveLoadBool_boolAsVariable_3, logic_SubGraph_SaveLoadBool_uniqueID_3);
	}

	private void Relay_SaveEvent_5()
	{
		Relay_Save_24();
	}

	private void Relay_LoadEvent_5()
	{
		Relay_Load_24();
	}

	private void Relay_RestartEvent_5()
	{
		Relay_Restart_24();
	}

	private void Relay_OnUpdate_6()
	{
		Relay_In_17();
	}

	private void Relay_OnSuspend_6()
	{
	}

	private void Relay_OnResume_6()
	{
	}

	private void Relay_In_8()
	{
		logic_uScriptCon_CompareBool_Bool_8 = local_TechSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.In(logic_uScriptCon_CompareBool_Bool_8);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.False;
		if (num)
		{
			Relay_In_604();
		}
		if (flag)
		{
			Relay_In_508();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_SetEncounterTarget_owner_10 = owner_Connection_9;
		logic_uScript_SetEncounterTarget_visibleObject_10 = local_techNPC_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_10.In(logic_uScript_SetEncounterTarget_owner_10, logic_uScript_SetEncounterTarget_visibleObject_10);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_10.Out)
		{
			Relay_In_643();
		}
	}

	private void Relay_Succeed_13()
	{
		logic_uScript_FinishEncounter_owner_13 = owner_Connection_11;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_13.Succeed(logic_uScript_FinishEncounter_owner_13);
	}

	private void Relay_Fail_13()
	{
		logic_uScript_FinishEncounter_owner_13 = owner_Connection_11;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_13.Fail(logic_uScript_FinishEncounter_owner_13);
	}

	private void Relay_In_14()
	{
		int num = 0;
		Array array = msgMissionComplete;
		if (logic_uScript_AddOnScreenMessage_locString_14.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_14, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_14, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_14 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_14 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_14.In(logic_uScript_AddOnScreenMessage_locString_14, logic_uScript_AddOnScreenMessage_msgPriority_14, logic_uScript_AddOnScreenMessage_holdMsg_14, logic_uScript_AddOnScreenMessage_tag_14, logic_uScript_AddOnScreenMessage_speaker_14, logic_uScript_AddOnScreenMessage_side_14);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_14.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_17()
	{
		logic_uScriptCon_CompareBool_Bool_17 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.In(logic_uScriptCon_CompareBool_Bool_17);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.False;
		if (num)
		{
			Relay_UnPause_136();
		}
		if (flag)
		{
			Relay_In_8();
		}
	}

	private void Relay_InitialSpawn_18()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_18.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_18, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_18, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_18 = owner_Connection_20;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_18, logic_uScript_SpawnTechsFromData_ownerNode_18, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_18, logic_uScript_SpawnTechsFromData_allowResurrection_18);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_18.Out)
		{
			Relay_True_2();
		}
	}

	private void Relay_True_22()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.True(out logic_uScriptAct_SetBool_Target_22);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_22;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_22.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_False_22()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.False(out logic_uScriptAct_SetBool_Target_22);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_22;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_22.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_Save_Out_24()
	{
		Relay_In_666();
	}

	private void Relay_Load_Out_24()
	{
		Relay_In_664();
	}

	private void Relay_Restart_Out_24()
	{
		Relay_In_667();
	}

	private void Relay_Save_24()
	{
		logic_SubGraph_SaveLoadInt_integer_24 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_24 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Save(logic_SubGraph_SaveLoadInt_restartValue_24, ref logic_SubGraph_SaveLoadInt_integer_24, logic_SubGraph_SaveLoadInt_intAsVariable_24, logic_SubGraph_SaveLoadInt_uniqueID_24);
	}

	private void Relay_Load_24()
	{
		logic_SubGraph_SaveLoadInt_integer_24 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_24 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Load(logic_SubGraph_SaveLoadInt_restartValue_24, ref logic_SubGraph_SaveLoadInt_integer_24, logic_SubGraph_SaveLoadInt_intAsVariable_24, logic_SubGraph_SaveLoadInt_uniqueID_24);
	}

	private void Relay_Restart_24()
	{
		logic_SubGraph_SaveLoadInt_integer_24 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_24 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Restart(logic_SubGraph_SaveLoadInt_restartValue_24, ref logic_SubGraph_SaveLoadInt_integer_24, logic_SubGraph_SaveLoadInt_intAsVariable_24, logic_SubGraph_SaveLoadInt_uniqueID_24);
	}

	private void Relay_Out_26()
	{
	}

	private void Relay_In_26()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_26 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_26, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_26);
	}

	private void Relay_Save_Out_29()
	{
		Relay_Save_40();
	}

	private void Relay_Load_Out_29()
	{
		Relay_Load_40();
	}

	private void Relay_Restart_Out_29()
	{
		Relay_Set_False_40();
	}

	private void Relay_Save_29()
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_29 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Save(ref logic_SubGraph_SaveLoadBool_boolean_29, logic_SubGraph_SaveLoadBool_boolAsVariable_29, logic_SubGraph_SaveLoadBool_uniqueID_29);
	}

	private void Relay_Load_29()
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_29 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Load(ref logic_SubGraph_SaveLoadBool_boolean_29, logic_SubGraph_SaveLoadBool_boolAsVariable_29, logic_SubGraph_SaveLoadBool_uniqueID_29);
	}

	private void Relay_Set_True_29()
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_29 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_29, logic_SubGraph_SaveLoadBool_boolAsVariable_29, logic_SubGraph_SaveLoadBool_uniqueID_29);
	}

	private void Relay_Set_False_29()
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_29 = local_TechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_29, logic_SubGraph_SaveLoadBool_boolAsVariable_29, logic_SubGraph_SaveLoadBool_uniqueID_29);
	}

	private void Relay_In_30()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_30.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_30, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_30, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_30 = owner_Connection_31;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_30.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_30, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_30, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_30 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_30.In(logic_uScript_GetAndCheckTechs_techData_30, logic_uScript_GetAndCheckTechs_ownerNode_30, ref logic_uScript_GetAndCheckTechs_techs_30);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_30;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_30.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_30.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_33();
		}
		if (someAlive)
		{
			Relay_AtIndex_33();
		}
	}

	private void Relay_AtIndex_33()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_33.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_33, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_33, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_33.AtIndex(ref logic_uScript_AccessListTech_techList_33, logic_uScript_AccessListTech_index_33, out logic_uScript_AccessListTech_value_33);
		local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_33;
		local_techNPC_Tank = logic_uScript_AccessListTech_value_33;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_33.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_34()
	{
		logic_uScript_SetTankInvulnerable_tank_34 = local_techNPC_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_34.In(logic_uScript_SetTankInvulnerable_invulnerable_34, logic_uScript_SetTankInvulnerable_tank_34);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_34.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_37()
	{
		logic_uScriptCon_CompareBool_Bool_37 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.In(logic_uScriptCon_CompareBool_Bool_37);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.False;
		if (num)
		{
			Relay_In_302();
		}
		if (flag)
		{
			Relay_In_41();
		}
	}

	private void Relay_Save_Out_40()
	{
		Relay_Save_46();
	}

	private void Relay_Load_Out_40()
	{
		Relay_Load_46();
	}

	private void Relay_Restart_Out_40()
	{
		Relay_Set_False_46();
	}

	private void Relay_Save_40()
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_40 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Save(ref logic_SubGraph_SaveLoadBool_boolean_40, logic_SubGraph_SaveLoadBool_boolAsVariable_40, logic_SubGraph_SaveLoadBool_uniqueID_40);
	}

	private void Relay_Load_40()
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_40 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Load(ref logic_SubGraph_SaveLoadBool_boolean_40, logic_SubGraph_SaveLoadBool_boolAsVariable_40, logic_SubGraph_SaveLoadBool_uniqueID_40);
	}

	private void Relay_Set_True_40()
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_40 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_40, logic_SubGraph_SaveLoadBool_boolAsVariable_40, logic_SubGraph_SaveLoadBool_uniqueID_40);
	}

	private void Relay_Set_False_40()
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_40 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_40, logic_SubGraph_SaveLoadBool_boolAsVariable_40, logic_SubGraph_SaveLoadBool_uniqueID_40);
	}

	private void Relay_In_41()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_41 = NearNPCTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_41.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_41);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_41.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_41.OutOfRange;
		if (inRange)
		{
			Relay_Pause_81();
		}
		if (outOfRange)
		{
			Relay_UnPause_80();
		}
	}

	private void Relay_In_44()
	{
		logic_uScriptCon_CompareBool_Bool_44 = local_NPCIntroMessagePlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44.In(logic_uScriptCon_CompareBool_Bool_44);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44.False;
		if (num)
		{
			Relay_True_22();
		}
		if (flag)
		{
			Relay_In_85();
		}
	}

	private void Relay_Save_Out_46()
	{
		Relay_Save_93();
	}

	private void Relay_Load_Out_46()
	{
		Relay_Load_93();
	}

	private void Relay_Restart_Out_46()
	{
		Relay_Set_False_93();
	}

	private void Relay_Save_46()
	{
		logic_SubGraph_SaveLoadBool_boolean_46 = local_NPCIntroMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_46 = local_NPCIntroMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Save(ref logic_SubGraph_SaveLoadBool_boolean_46, logic_SubGraph_SaveLoadBool_boolAsVariable_46, logic_SubGraph_SaveLoadBool_uniqueID_46);
	}

	private void Relay_Load_46()
	{
		logic_SubGraph_SaveLoadBool_boolean_46 = local_NPCIntroMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_46 = local_NPCIntroMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Load(ref logic_SubGraph_SaveLoadBool_boolean_46, logic_SubGraph_SaveLoadBool_boolAsVariable_46, logic_SubGraph_SaveLoadBool_uniqueID_46);
	}

	private void Relay_Set_True_46()
	{
		logic_SubGraph_SaveLoadBool_boolean_46 = local_NPCIntroMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_46 = local_NPCIntroMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_46, logic_SubGraph_SaveLoadBool_boolAsVariable_46, logic_SubGraph_SaveLoadBool_uniqueID_46);
	}

	private void Relay_Set_False_46()
	{
		logic_SubGraph_SaveLoadBool_boolean_46 = local_NPCIntroMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_46 = local_NPCIntroMessagePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_46, logic_SubGraph_SaveLoadBool_boolAsVariable_46, logic_SubGraph_SaveLoadBool_uniqueID_46);
	}

	private void Relay_True_47()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.True(out logic_uScriptAct_SetBool_Target_47);
		local_GoToStartTrigger_System_Boolean = logic_uScriptAct_SetBool_Target_47;
	}

	private void Relay_False_47()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.False(out logic_uScriptAct_SetBool_Target_47);
		local_GoToStartTrigger_System_Boolean = logic_uScriptAct_SetBool_Target_47;
	}

	private void Relay_In_48()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_49()
	{
		logic_uScript_GetTankBlock_tank_49 = local_PaymentPointTech_Tank;
		logic_uScript_GetTankBlock_blockType_49 = interactableBlockType;
		logic_uScript_GetTankBlock_Return_49 = logic_uScript_GetTankBlock_uScript_GetTankBlock_49.In(logic_uScript_GetTankBlock_tank_49, logic_uScript_GetTankBlock_blockType_49);
		local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_49;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_49.Out)
		{
			Relay_In_113();
		}
	}

	private void Relay_Out_51()
	{
		Relay_In_460();
	}

	private void Relay_Shown_51()
	{
	}

	private void Relay_In_51()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_51 = msgPurchaseVehicle;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_51 = msgPurchaseVehicle_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_51 = SpeakerNPC;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_51.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_51, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_51, logic_SubGraph_AddMessageWithPadSupport_speaker_51);
	}

	private void Relay_In_53()
	{
		logic_uScript_PointArrowAtVisible_targetObject_53 = local_TerminalBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_53.In(logic_uScript_PointArrowAtVisible_targetObject_53, logic_uScript_PointArrowAtVisible_timeToShowFor_53, logic_uScript_PointArrowAtVisible_offset_53);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_53.Out)
		{
			Relay_In_559();
		}
	}

	private void Relay_In_55()
	{
		logic_uScriptCon_CompareBool_Bool_55 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.In(logic_uScriptCon_CompareBool_Bool_55);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.False;
		if (num)
		{
			Relay_In_491();
		}
		if (flag)
		{
			Relay_In_459();
		}
	}

	private void Relay_In_58()
	{
		logic_uScript_HideArrow_uScript_HideArrow_58.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_58.Out)
		{
			Relay_UnPause_137();
		}
	}

	private void Relay_In_60()
	{
		logic_uScriptCon_CompareBool_Bool_60 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_60.In(logic_uScriptCon_CompareBool_Bool_60);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_60.False)
		{
			Relay_In_98();
		}
	}

	private void Relay_In_61()
	{
		logic_uScript_SetEncounterTarget_owner_61 = owner_Connection_68;
		logic_uScript_SetEncounterTarget_visibleObject_61 = local_PaymentPointTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_61.In(logic_uScript_SetEncounterTarget_owner_61, logic_uScript_SetEncounterTarget_visibleObject_61);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_61.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_Out_64()
	{
	}

	private void Relay_Shown_64()
	{
	}

	private void Relay_In_64()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_64 = msgSwitchTech;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_64 = msgSwitchTech_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_64 = SpeakerNPC;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_64, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_64, logic_SubGraph_AddMessageWithPadSupport_speaker_64);
	}

	private void Relay_In_67()
	{
		logic_uScriptCon_CompareBool_Bool_67 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67.In(logic_uScriptCon_CompareBool_Bool_67);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67.False;
		if (num)
		{
			Relay_In_477();
		}
		if (flag)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_70()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_70 = msgTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_70.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_70, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_70);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_70.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_71()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_71 = msgTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_71.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_71, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_71);
	}

	private void Relay_In_76()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_76 = owner_Connection_65;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_76.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_76);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_76.Out)
		{
			Relay_InitialSpawn_170();
		}
	}

	private void Relay_In_78()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_78.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_78.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_Pause_80()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_80.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_80.Out)
		{
			Relay_In_288();
		}
	}

	private void Relay_UnPause_80()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_80.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_80.Out)
		{
			Relay_In_288();
		}
	}

	private void Relay_Pause_81()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_81.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_81.Out)
		{
			Relay_True_280();
		}
	}

	private void Relay_UnPause_81()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_81.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_81.Out)
		{
			Relay_True_280();
		}
	}

	private void Relay_True_82()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.True(out logic_uScriptAct_SetBool_Target_82);
		local_NPCIntroMessagePlayed_System_Boolean = logic_uScriptAct_SetBool_Target_82;
	}

	private void Relay_False_82()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.False(out logic_uScriptAct_SetBool_Target_82);
		local_NPCIntroMessagePlayed_System_Boolean = logic_uScriptAct_SetBool_Target_82;
	}

	private void Relay_In_84()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_84 = NearNPCTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_84.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_84);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_84.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_84.OutOfRange;
		if (inRange)
		{
			Relay_In_61();
		}
		if (outOfRange)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_85()
	{
		int num = 0;
		Array array = msgNPCIntro;
		if (logic_uScript_AddOnScreenMessage_locString_85.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_85, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_85, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_85 = msgTagControls;
		logic_uScript_AddOnScreenMessage_speaker_85 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_85 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_85.In(logic_uScript_AddOnScreenMessage_locString_85, logic_uScript_AddOnScreenMessage_msgPriority_85, logic_uScript_AddOnScreenMessage_holdMsg_85, logic_uScript_AddOnScreenMessage_tag_85, logic_uScript_AddOnScreenMessage_speaker_85, logic_uScript_AddOnScreenMessage_side_85);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_85.Shown)
		{
			Relay_True_82();
		}
	}

	private void Relay_In_89()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_89 = msgTagControls;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_89.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_89, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_89);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_89.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_Save_Out_93()
	{
		Relay_Save_94();
	}

	private void Relay_Load_Out_93()
	{
		Relay_Load_94();
	}

	private void Relay_Restart_Out_93()
	{
		Relay_Set_False_94();
	}

	private void Relay_Save_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Load_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Set_True_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Set_False_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Save_Out_94()
	{
		Relay_Save_100();
	}

	private void Relay_Load_Out_94()
	{
		Relay_Load_100();
	}

	private void Relay_Restart_Out_94()
	{
		Relay_Set_False_100();
	}

	private void Relay_Save_94()
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_94 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Save(ref logic_SubGraph_SaveLoadBool_boolean_94, logic_SubGraph_SaveLoadBool_boolAsVariable_94, logic_SubGraph_SaveLoadBool_uniqueID_94);
	}

	private void Relay_Load_94()
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_94 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Load(ref logic_SubGraph_SaveLoadBool_boolean_94, logic_SubGraph_SaveLoadBool_boolAsVariable_94, logic_SubGraph_SaveLoadBool_uniqueID_94);
	}

	private void Relay_Set_True_94()
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_94 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_94, logic_SubGraph_SaveLoadBool_boolAsVariable_94, logic_SubGraph_SaveLoadBool_uniqueID_94);
	}

	private void Relay_Set_False_94()
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_94 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_94, logic_SubGraph_SaveLoadBool_boolAsVariable_94, logic_SubGraph_SaveLoadBool_uniqueID_94);
	}

	private void Relay_In_96()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_96 = NearNPCTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_96.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_96);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_96.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_96.OutOfRange;
		if (inRange)
		{
			Relay_In_164();
		}
		if (outOfRange)
		{
			Relay_SetVulnerable_147();
		}
	}

	private void Relay_In_98()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_98 = NearNPCTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_98.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_98);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_98.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_98.OutOfRange;
		if (inRange)
		{
			Relay_In_53();
		}
		if (outOfRange)
		{
			Relay_In_58();
		}
	}

	private void Relay_Save_Out_100()
	{
		Relay_Save_102();
	}

	private void Relay_Load_Out_100()
	{
		Relay_Load_102();
	}

	private void Relay_Restart_Out_100()
	{
		Relay_Set_False_102();
	}

	private void Relay_Save_100()
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_100 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Save(ref logic_SubGraph_SaveLoadBool_boolean_100, logic_SubGraph_SaveLoadBool_boolAsVariable_100, logic_SubGraph_SaveLoadBool_uniqueID_100);
	}

	private void Relay_Load_100()
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_100 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Load(ref logic_SubGraph_SaveLoadBool_boolean_100, logic_SubGraph_SaveLoadBool_boolAsVariable_100, logic_SubGraph_SaveLoadBool_uniqueID_100);
	}

	private void Relay_Set_True_100()
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_100 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_100, logic_SubGraph_SaveLoadBool_boolAsVariable_100, logic_SubGraph_SaveLoadBool_uniqueID_100);
	}

	private void Relay_Set_False_100()
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_100 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_100, logic_SubGraph_SaveLoadBool_boolAsVariable_100, logic_SubGraph_SaveLoadBool_uniqueID_100);
	}

	private void Relay_Save_Out_102()
	{
		Relay_Save_111();
	}

	private void Relay_Load_Out_102()
	{
		Relay_Load_111();
	}

	private void Relay_Restart_Out_102()
	{
		Relay_Set_False_111();
	}

	private void Relay_Save_102()
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_102 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Save(ref logic_SubGraph_SaveLoadBool_boolean_102, logic_SubGraph_SaveLoadBool_boolAsVariable_102, logic_SubGraph_SaveLoadBool_uniqueID_102);
	}

	private void Relay_Load_102()
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_102 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Load(ref logic_SubGraph_SaveLoadBool_boolean_102, logic_SubGraph_SaveLoadBool_boolAsVariable_102, logic_SubGraph_SaveLoadBool_uniqueID_102);
	}

	private void Relay_Set_True_102()
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_102 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_102, logic_SubGraph_SaveLoadBool_boolAsVariable_102, logic_SubGraph_SaveLoadBool_uniqueID_102);
	}

	private void Relay_Set_False_102()
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_102 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_102, logic_SubGraph_SaveLoadBool_boolAsVariable_102, logic_SubGraph_SaveLoadBool_uniqueID_102);
	}

	private void Relay_In_107()
	{
		logic_uScriptCon_CompareBool_Bool_107 = local_TechSetUp_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107.In(logic_uScriptCon_CompareBool_Bool_107);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107.False;
		if (num)
		{
			Relay_In_634();
		}
		if (flag)
		{
			Relay_In_30();
		}
	}

	private void Relay_True_109()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.True(out logic_uScriptAct_SetBool_Target_109);
		local_TechSetUp_System_Boolean = logic_uScriptAct_SetBool_Target_109;
	}

	private void Relay_False_109()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.False(out logic_uScriptAct_SetBool_Target_109);
		local_TechSetUp_System_Boolean = logic_uScriptAct_SetBool_Target_109;
	}

	private void Relay_Save_Out_111()
	{
		Relay_Save_125();
	}

	private void Relay_Load_Out_111()
	{
		Relay_Load_125();
	}

	private void Relay_Restart_Out_111()
	{
		Relay_Set_False_125();
	}

	private void Relay_Save_111()
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_111 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Save(ref logic_SubGraph_SaveLoadBool_boolean_111, logic_SubGraph_SaveLoadBool_boolAsVariable_111, logic_SubGraph_SaveLoadBool_uniqueID_111);
	}

	private void Relay_Load_111()
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_111 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Load(ref logic_SubGraph_SaveLoadBool_boolean_111, logic_SubGraph_SaveLoadBool_boolAsVariable_111, logic_SubGraph_SaveLoadBool_uniqueID_111);
	}

	private void Relay_Set_True_111()
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_111 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_111, logic_SubGraph_SaveLoadBool_boolAsVariable_111, logic_SubGraph_SaveLoadBool_uniqueID_111);
	}

	private void Relay_Set_False_111()
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_111 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_111, logic_SubGraph_SaveLoadBool_boolAsVariable_111, logic_SubGraph_SaveLoadBool_uniqueID_111);
	}

	private void Relay_In_112()
	{
		logic_uScript_IsTechPlayer_tech_112 = local_vehicleTech_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_112.In(logic_uScript_IsTechPlayer_tech_112);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_112.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_112.False;
		if (num)
		{
			Relay_In_135();
		}
		if (flag)
		{
			Relay_In_510();
		}
	}

	private void Relay_In_113()
	{
		logic_uScriptCon_CompareBool_Bool_113 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113.In(logic_uScriptCon_CompareBool_Bool_113);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_113.False;
		if (num)
		{
			Relay_In_115();
		}
		if (flag)
		{
			Relay_In_122();
		}
	}

	private void Relay_In_115()
	{
		logic_uScriptCon_CompareBool_Bool_115 = local_VehicleSetup_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.In(logic_uScriptCon_CompareBool_Bool_115);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.False;
		if (num)
		{
			Relay_In_122();
		}
		if (flag)
		{
			Relay_In_120();
		}
	}

	private void Relay_True_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.True(out logic_uScriptAct_SetBool_Target_117);
		local_VehicleSetup_System_Boolean = logic_uScriptAct_SetBool_Target_117;
	}

	private void Relay_False_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.False(out logic_uScriptAct_SetBool_Target_117);
		local_VehicleSetup_System_Boolean = logic_uScriptAct_SetBool_Target_117;
	}

	private void Relay_In_120()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_120.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_120, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_120, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_120 = owner_Connection_118;
		int num2 = 0;
		Array array2 = local_vehicleTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_120.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_120, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_120, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_120 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_120.In(logic_uScript_GetAndCheckTechs_techData_120, logic_uScript_GetAndCheckTechs_ownerNode_120, ref logic_uScript_GetAndCheckTechs_techs_120);
		local_vehicleTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_120;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_120.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_120.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_121();
		}
		if (someAlive)
		{
			Relay_AtIndex_121();
		}
	}

	private void Relay_AtIndex_121()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_121.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_121, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_121, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_121.AtIndex(ref logic_uScript_AccessListTech_techList_121, logic_uScript_AccessListTech_index_121, out logic_uScript_AccessListTech_value_121);
		local_vehicleTechs_TankArray = logic_uScript_AccessListTech_techList_121;
		local_vehicleTech_Tank = logic_uScript_AccessListTech_value_121;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_121.Out)
		{
			Relay_SetInvulnerable_144();
		}
	}

	private void Relay_In_122()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_122.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_122.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_Save_Out_125()
	{
		Relay_Save_168();
	}

	private void Relay_Load_Out_125()
	{
		Relay_Load_168();
	}

	private void Relay_Restart_Out_125()
	{
		Relay_Set_False_168();
	}

	private void Relay_Save_125()
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_125 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Save(ref logic_SubGraph_SaveLoadBool_boolean_125, logic_SubGraph_SaveLoadBool_boolAsVariable_125, logic_SubGraph_SaveLoadBool_uniqueID_125);
	}

	private void Relay_Load_125()
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_125 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Load(ref logic_SubGraph_SaveLoadBool_boolean_125, logic_SubGraph_SaveLoadBool_boolAsVariable_125, logic_SubGraph_SaveLoadBool_uniqueID_125);
	}

	private void Relay_Set_True_125()
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_125 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_125, logic_SubGraph_SaveLoadBool_boolAsVariable_125, logic_SubGraph_SaveLoadBool_uniqueID_125);
	}

	private void Relay_Set_False_125()
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_125 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_125, logic_SubGraph_SaveLoadBool_boolAsVariable_125, logic_SubGraph_SaveLoadBool_uniqueID_125);
	}

	private void Relay_In_128()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_128.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_128, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_128, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_128 = owner_Connection_127;
		int num2 = 0;
		Array array2 = local_vehicleTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_128.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_128, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_128, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_128 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.In(logic_uScript_GetAndCheckTechs_techData_128, logic_uScript_GetAndCheckTechs_ownerNode_128, ref logic_uScript_GetAndCheckTechs_techs_128);
		local_vehicleTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_128;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.SomeAlive;
		if (allAlive)
		{
			Relay_In_112();
		}
		if (someAlive)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_129()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_129 = msgTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_129.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_129, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_129);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_129.Out)
		{
			Relay_In_276();
		}
	}

	private void Relay_In_135()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_135 = msgTagSwitchTech;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_135.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_135, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_135);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_135.Out)
		{
			Relay_In_497();
		}
	}

	private void Relay_Pause_136()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_136.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_136.Out)
		{
			Relay_In_242();
		}
	}

	private void Relay_UnPause_136()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_136.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_136.Out)
		{
			Relay_In_242();
		}
	}

	private void Relay_Pause_137()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_137.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_137.Out)
		{
			Relay_In_247();
		}
	}

	private void Relay_UnPause_137()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_137.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_137.Out)
		{
			Relay_In_247();
		}
	}

	private void Relay_In_138()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_138.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_138.Out)
		{
			Relay_In_250();
		}
	}

	private void Relay_In_140()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_140 = NearNPCTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_140.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_140);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_140.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_140.OutOfRange;
		if (inRange)
		{
			Relay_In_64();
		}
		if (outOfRange)
		{
			Relay_In_263();
		}
	}

	private void Relay_In_142()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_142 = msgTagControls;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_142.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_142, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_142);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_142.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_SetInvulnerable_144()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_144.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_144, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_144, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_144.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_144);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_144.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_SetVulnerable_144()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_144.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_144, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_144, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_144.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_144);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_144.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_SetInvulnerable_147()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_147.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_147, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_147, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_147.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_147);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_147.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_SetVulnerable_147()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_147.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_147, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_147, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_147.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_147);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_147.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_In_148()
	{
		logic_uScript_LockTechSendToSCU_tech_148 = local_vehicleTech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_148.In(logic_uScript_LockTechSendToSCU_tech_148, logic_uScript_LockTechSendToSCU_lockSendToSCU_148);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_148.Out)
		{
			Relay_True_117();
		}
	}

	private void Relay_In_151()
	{
		logic_uScript_LockTechSendToSCU_tech_151 = local_vehicleTech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_151.In(logic_uScript_LockTechSendToSCU_tech_151, logic_uScript_LockTechSendToSCU_lockSendToSCU_151);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_151.Out)
		{
			Relay_True_47();
		}
	}

	private void Relay_In_155()
	{
		logic_uScript_SetEncounterTarget_owner_155 = owner_Connection_154;
		logic_uScript_SetEncounterTarget_visibleObject_155 = local_vehicleTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_155.In(logic_uScript_SetEncounterTarget_owner_155, logic_uScript_SetEncounterTarget_visibleObject_155);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_155.Out)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_FlyTechUpAndAway_tech_156 = local_techNPC_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_156 = NPCFlyAwayBehavior;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_156.In(logic_uScript_FlyTechUpAndAway_tech_156, logic_uScript_FlyTechUpAndAway_maxLifetime_156, logic_uScript_FlyTechUpAndAway_targetHeight_156, logic_uScript_FlyTechUpAndAway_aiTree_156, logic_uScript_FlyTechUpAndAway_removalParticles_156);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_156.Out)
		{
			Relay_In_245();
		}
	}

	private void Relay_True_159()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.True(out logic_uScriptAct_SetBool_Target_159);
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_uScriptAct_SetBool_Target_159;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_159.Out)
		{
			Relay_In_451();
		}
	}

	private void Relay_False_159()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.False(out logic_uScriptAct_SetBool_Target_159);
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_uScriptAct_SetBool_Target_159;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_159.Out)
		{
			Relay_In_451();
		}
	}

	private void Relay_In_163()
	{
		int num = 0;
		Array array = msgNPCVehicleSwitched;
		if (logic_uScript_AddOnScreenMessage_locString_163.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_163, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_163, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_163 = msgTagControls;
		logic_uScript_AddOnScreenMessage_speaker_163 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_163 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_163.In(logic_uScript_AddOnScreenMessage_locString_163, logic_uScript_AddOnScreenMessage_msgPriority_163, logic_uScript_AddOnScreenMessage_holdMsg_163, logic_uScript_AddOnScreenMessage_tag_163, logic_uScript_AddOnScreenMessage_speaker_163, logic_uScript_AddOnScreenMessage_side_163);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_163.Shown)
		{
			Relay_True_159();
		}
	}

	private void Relay_In_164()
	{
		logic_uScriptCon_CompareBool_Bool_164 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_164.In(logic_uScriptCon_CompareBool_Bool_164);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_164.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_164.False;
		if (num)
		{
			Relay_In_451();
		}
		if (flag)
		{
			Relay_In_163();
		}
	}

	private void Relay_Save_Out_168()
	{
		Relay_Save_257();
	}

	private void Relay_Load_Out_168()
	{
		Relay_Load_257();
	}

	private void Relay_Restart_Out_168()
	{
		Relay_Set_False_257();
	}

	private void Relay_Save_168()
	{
		logic_SubGraph_SaveLoadBool_boolean_168 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_168 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Save(ref logic_SubGraph_SaveLoadBool_boolean_168, logic_SubGraph_SaveLoadBool_boolAsVariable_168, logic_SubGraph_SaveLoadBool_uniqueID_168);
	}

	private void Relay_Load_168()
	{
		logic_SubGraph_SaveLoadBool_boolean_168 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_168 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Load(ref logic_SubGraph_SaveLoadBool_boolean_168, logic_SubGraph_SaveLoadBool_boolAsVariable_168, logic_SubGraph_SaveLoadBool_uniqueID_168);
	}

	private void Relay_Set_True_168()
	{
		logic_SubGraph_SaveLoadBool_boolean_168 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_168 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_168, logic_SubGraph_SaveLoadBool_boolAsVariable_168, logic_SubGraph_SaveLoadBool_uniqueID_168);
	}

	private void Relay_Set_False_168()
	{
		logic_SubGraph_SaveLoadBool_boolean_168 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_168 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_168, logic_SubGraph_SaveLoadBool_boolAsVariable_168, logic_SubGraph_SaveLoadBool_uniqueID_168);
	}

	private void Relay_InitialSpawn_170()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_SpawnTechsFromData_spawnData_170.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_170, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_SpawnTechsFromData_spawnData_170, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_170 = owner_Connection_171;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_170.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_170, logic_uScript_SpawnTechsFromData_ownerNode_170, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_170, logic_uScript_SpawnTechsFromData_allowResurrection_170);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_170.Out)
		{
			Relay_InitialSpawn_18();
		}
	}

	private void Relay_In_173()
	{
		logic_uScript_SetTankInvulnerable_tank_173 = local_PaymentPointTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_173.In(logic_uScript_SetTankInvulnerable_invulnerable_173, logic_uScript_SetTankInvulnerable_tank_173);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_173.Out)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_176()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_176.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_176, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_176, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_176 = owner_Connection_179;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_176.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_176, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_176, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_176 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_176.In(logic_uScript_GetAndCheckTechs_techData_176, logic_uScript_GetAndCheckTechs_ownerNode_176, ref logic_uScript_GetAndCheckTechs_techs_176);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_176;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_176.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_176.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_177();
		}
		if (someAlive)
		{
			Relay_AtIndex_177();
		}
	}

	private void Relay_AtIndex_177()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_177.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_177, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_177, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_177.AtIndex(ref logic_uScript_AccessListTech_techList_177, logic_uScript_AccessListTech_index_177, out logic_uScript_AccessListTech_value_177);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_177;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_177;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_177.Out)
		{
			Relay_In_173();
		}
	}

	private void Relay_ResponseEvent_182()
	{
		local_221_TankBlock = event_UnityEngine_GameObject_TankBlock_182;
		local_215_System_Boolean = event_UnityEngine_GameObject_Accepted_182;
		Relay_In_199();
	}

	private void Relay_In_183()
	{
		logic_uScriptCon_CompareBool_Bool_183 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.In(logic_uScriptCon_CompareBool_Bool_183);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.False;
		if (num)
		{
			Relay_In_212();
		}
		if (flag)
		{
			Relay_In_212();
		}
	}

	private void Relay_True_185()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_185.True(out logic_uScriptAct_SetBool_Target_185);
		local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_185;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_185.Out)
		{
			Relay_In_200();
		}
	}

	private void Relay_False_185()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_185.False(out logic_uScriptAct_SetBool_Target_185);
		local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_185;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_185.Out)
		{
			Relay_In_200();
		}
	}

	private void Relay_In_189()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_189.In();
	}

	private void Relay_In_192()
	{
		logic_uScriptCon_CompareBool_Bool_192 = local_215_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.In(logic_uScriptCon_CompareBool_Bool_192);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.False;
		if (num)
		{
			Relay_In_570();
		}
		if (flag)
		{
			Relay_In_457();
		}
	}

	private void Relay_In_193()
	{
		logic_uScriptCon_CompareInt_A_193 = local_CurrentMoney_System_Int32;
		logic_uScriptCon_CompareInt_B_193 = vehicleCost;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_193.In(logic_uScriptCon_CompareInt_A_193, logic_uScriptCon_CompareInt_B_193);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_193.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_193.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_183();
		}
		if (lessThan)
		{
			Relay_In_196();
		}
	}

	private void Relay_In_194()
	{
		logic_uScript_GetCurrentMoneyEarned_Return_194 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_194.In();
		local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_194;
		if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_194.Out)
		{
			Relay_In_193();
		}
	}

	private void Relay_In_196()
	{
		logic_uScriptCon_CompareBool_Bool_196 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_196.In(logic_uScriptCon_CompareBool_Bool_196);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_196.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_196.False;
		if (num)
		{
			Relay_In_210();
		}
		if (flag)
		{
			Relay_In_210();
		}
	}

	private void Relay_In_197()
	{
		logic_uScriptCon_CompareBool_Bool_197 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.In(logic_uScriptCon_CompareBool_Bool_197);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.False;
		if (num)
		{
			Relay_In_230();
		}
		if (flag)
		{
			Relay_In_219();
		}
	}

	private void Relay_In_199()
	{
		logic_uScript_CompareBlock_A_199 = local_221_TankBlock;
		logic_uScript_CompareBlock_B_199 = local_TerminalBlock_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_199.In(logic_uScript_CompareBlock_A_199, logic_uScript_CompareBlock_B_199);
		if (logic_uScript_CompareBlock_uScript_CompareBlock_199.EqualTo)
		{
			Relay_In_192();
		}
	}

	private void Relay_In_200()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200.In();
	}

	private void Relay_In_204()
	{
		logic_uScriptCon_CompareBool_Bool_204 = _DEBUGIgnoreMoneyCheck;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_204.In(logic_uScriptCon_CompareBool_Bool_204);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_204.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_204.False;
		if (num)
		{
			Relay_In_205();
		}
		if (flag)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_205()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_205.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_205.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_In_206()
	{
		logic_uScript_AddMessage_messageData_206 = msgNPCNotEnoughMoney;
		logic_uScript_AddMessage_speaker_206 = SpeakerNPC;
		logic_uScript_AddMessage_Return_206 = logic_uScript_AddMessage_uScript_AddMessage_206.In(logic_uScript_AddMessage_messageData_206, logic_uScript_AddMessage_speaker_206);
		if (logic_uScript_AddMessage_uScript_AddMessage_206.Out)
		{
			Relay_True_185();
		}
	}

	private void Relay_In_210()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_210 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_210 = msgPromptNoMoney;
		logic_uScript_MissionPromptBlock_Show_targetBlock_210 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_210.In(logic_uScript_MissionPromptBlock_Show_bodyText_210, logic_uScript_MissionPromptBlock_Show_acceptButtonText_210, logic_uScript_MissionPromptBlock_Show_rejectButtonText_210, logic_uScript_MissionPromptBlock_Show_targetBlock_210, logic_uScript_MissionPromptBlock_Show_highlightBlock_210, logic_uScript_MissionPromptBlock_Show_singleUse_210);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_210.Out)
		{
			Relay_False_214();
		}
	}

	private void Relay_True_211()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_211.True(out logic_uScriptAct_SetBool_Target_211);
		local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_211;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_211.Out)
		{
			Relay_In_189();
		}
	}

	private void Relay_False_211()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_211.False(out logic_uScriptAct_SetBool_Target_211);
		local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_211;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_211.Out)
		{
			Relay_In_189();
		}
	}

	private void Relay_In_212()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_212 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_212 = msgPromptAccept;
		logic_uScript_MissionPromptBlock_Show_rejectButtonText_212 = msgPromptDecline;
		logic_uScript_MissionPromptBlock_Show_targetBlock_212 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_212.In(logic_uScript_MissionPromptBlock_Show_bodyText_212, logic_uScript_MissionPromptBlock_Show_acceptButtonText_212, logic_uScript_MissionPromptBlock_Show_rejectButtonText_212, logic_uScript_MissionPromptBlock_Show_targetBlock_212, logic_uScript_MissionPromptBlock_Show_highlightBlock_212, logic_uScript_MissionPromptBlock_Show_singleUse_212);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_212.Out)
		{
			Relay_True_214();
		}
	}

	private void Relay_True_214()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.True(out logic_uScriptAct_SetBool_Target_214);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_214;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_214.Out)
		{
			Relay_True_461();
		}
	}

	private void Relay_False_214()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.False(out logic_uScriptAct_SetBool_Target_214);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_214;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_214.Out)
		{
			Relay_True_461();
		}
	}

	private void Relay_In_218()
	{
		logic_uScript_AddMessage_messageData_218 = msgNPCPurchaseDeclined;
		logic_uScript_AddMessage_speaker_218 = SpeakerNPC;
		logic_uScript_AddMessage_Return_218 = logic_uScript_AddMessage_uScript_AddMessage_218.In(logic_uScript_AddMessage_messageData_218, logic_uScript_AddMessage_speaker_218);
		if (logic_uScript_AddMessage_uScript_AddMessage_218.Out)
		{
			Relay_True_211();
		}
	}

	private void Relay_In_219()
	{
		logic_uScriptCon_CompareBool_Bool_219 = local_msg03bShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219.In(logic_uScriptCon_CompareBool_Bool_219);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219.False;
		if (num)
		{
			Relay_In_200();
		}
		if (flag)
		{
			Relay_In_206();
		}
	}

	private void Relay_True_223()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_223.True(out logic_uScriptAct_SetBool_Target_223);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_223;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_223.Out)
		{
			Relay_In_689();
		}
	}

	private void Relay_False_223()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_223.False(out logic_uScriptAct_SetBool_Target_223);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_223;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_223.Out)
		{
			Relay_In_689();
		}
	}

	private void Relay_In_225()
	{
		int num = 0;
		Array array = discoverableBlockTypesOnVehicle;
		if (logic_uScript_DiscoverBlocks_blockTypes_225.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DiscoverBlocks_blockTypes_225, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DiscoverBlocks_blockTypes_225, num, array.Length);
		num += array.Length;
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_225.In(logic_uScript_DiscoverBlocks_blockTypes_225);
		if (logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_225.Out)
		{
			Relay_In_620();
		}
	}

	private void Relay_In_230()
	{
		logic_uScript_HideArrow_uScript_HideArrow_230.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_230.Out)
		{
			Relay_In_240();
		}
	}

	private void Relay_In_231()
	{
		logic_uScript_EnableGlow_targetObject_231 = local_TerminalBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_231.In(logic_uScript_EnableGlow_targetObject_231, logic_uScript_EnableGlow_enable_231);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_231.Out)
		{
			Relay_In_225();
		}
	}

	private void Relay_In_233()
	{
		logic_uScript_PointArrowAtVisible_targetObject_233 = local_vehicleTech_Tank;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_233.In(logic_uScript_PointArrowAtVisible_targetObject_233, logic_uScript_PointArrowAtVisible_timeToShowFor_233, logic_uScript_PointArrowAtVisible_offset_233);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_233.Out)
		{
			Relay_In_234();
		}
	}

	private void Relay_In_234()
	{
		logic_uScript_EnableGlow_targetObject_234 = local_vehicleTech_Tank;
		logic_uScript_EnableGlow_uScript_EnableGlow_234.In(logic_uScript_EnableGlow_targetObject_234, logic_uScript_EnableGlow_enable_234);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_234.Out)
		{
			Relay_In_155();
		}
	}

	private void Relay_In_236()
	{
		logic_uScript_HideArrow_uScript_HideArrow_236.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_236.Out)
		{
			Relay_In_237();
		}
	}

	private void Relay_In_237()
	{
		logic_uScript_EnableGlow_targetObject_237 = local_vehicleTech_Tank;
		logic_uScript_EnableGlow_uScript_EnableGlow_237.In(logic_uScript_EnableGlow_targetObject_237, logic_uScript_EnableGlow_enable_237);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_237.Out)
		{
			Relay_In_96();
		}
	}

	private void Relay_In_240()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_240 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_240.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_240);
		if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_240.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_In_242()
	{
		logic_uScript_ClearEncounterTarget_owner_242 = owner_Connection_243;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_242.In(logic_uScript_ClearEncounterTarget_owner_242);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_242.Out)
		{
			Relay_In_543();
		}
	}

	private void Relay_In_245()
	{
		logic_uScript_FlyTechUpAndAway_tech_245 = local_PaymentPointTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_245 = NPCFlyAwayBehavior;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_245.In(logic_uScript_FlyTechUpAndAway_tech_245, logic_uScript_FlyTechUpAndAway_maxLifetime_245, logic_uScript_FlyTechUpAndAway_targetHeight_245, logic_uScript_FlyTechUpAndAway_aiTree_245, logic_uScript_FlyTechUpAndAway_removalParticles_245);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_245.Out)
		{
			Relay_Succeed_13();
		}
	}

	private void Relay_In_247()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_247 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_247.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_247);
	}

	private void Relay_In_250()
	{
		logic_uScriptCon_CompareBool_Bool_250 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_250.In(logic_uScriptCon_CompareBool_Bool_250);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_250.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_250.False;
		if (num)
		{
			Relay_In_495();
		}
		if (flag)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_251()
	{
		int num = 0;
		Array array = msgLeavingEarlyPrePurchase;
		if (logic_uScript_AddOnScreenMessage_locString_251.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_251, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_251, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_251 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_251 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_251.In(logic_uScript_AddOnScreenMessage_locString_251, logic_uScript_AddOnScreenMessage_msgPriority_251, logic_uScript_AddOnScreenMessage_holdMsg_251, logic_uScript_AddOnScreenMessage_tag_251, logic_uScript_AddOnScreenMessage_speaker_251, logic_uScript_AddOnScreenMessage_side_251);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_251.Out)
		{
			Relay_True_254();
		}
	}

	private void Relay_True_254()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_254.True(out logic_uScriptAct_SetBool_Target_254);
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_uScriptAct_SetBool_Target_254;
	}

	private void Relay_False_254()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_254.False(out logic_uScriptAct_SetBool_Target_254);
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_uScriptAct_SetBool_Target_254;
	}

	private void Relay_Save_Out_257()
	{
		Relay_Save_266();
	}

	private void Relay_Load_Out_257()
	{
		Relay_Load_266();
	}

	private void Relay_Restart_Out_257()
	{
		Relay_Set_False_266();
	}

	private void Relay_Save_257()
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_257 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Save(ref logic_SubGraph_SaveLoadBool_boolean_257, logic_SubGraph_SaveLoadBool_boolAsVariable_257, logic_SubGraph_SaveLoadBool_uniqueID_257);
	}

	private void Relay_Load_257()
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_257 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Load(ref logic_SubGraph_SaveLoadBool_boolean_257, logic_SubGraph_SaveLoadBool_boolAsVariable_257, logic_SubGraph_SaveLoadBool_uniqueID_257);
	}

	private void Relay_Set_True_257()
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_257 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_257, logic_SubGraph_SaveLoadBool_boolAsVariable_257, logic_SubGraph_SaveLoadBool_uniqueID_257);
	}

	private void Relay_Set_False_257()
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_257 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_257, logic_SubGraph_SaveLoadBool_boolAsVariable_257, logic_SubGraph_SaveLoadBool_uniqueID_257);
	}

	private void Relay_True_260()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_260.True(out logic_uScriptAct_SetBool_Target_260);
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_uScriptAct_SetBool_Target_260;
	}

	private void Relay_False_260()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_260.False(out logic_uScriptAct_SetBool_Target_260);
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_uScriptAct_SetBool_Target_260;
	}

	private void Relay_In_261()
	{
		int num = 0;
		Array array = msgLeavingEarlyPostPurchase;
		if (logic_uScript_AddOnScreenMessage_locString_261.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_261, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_261, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_261 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_261 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_261.In(logic_uScript_AddOnScreenMessage_locString_261, logic_uScript_AddOnScreenMessage_msgPriority_261, logic_uScript_AddOnScreenMessage_holdMsg_261, logic_uScript_AddOnScreenMessage_tag_261, logic_uScript_AddOnScreenMessage_speaker_261, logic_uScript_AddOnScreenMessage_side_261);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_261.Out)
		{
			Relay_True_260();
		}
	}

	private void Relay_In_263()
	{
		logic_uScriptCon_CompareBool_Bool_263 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_263.In(logic_uScriptCon_CompareBool_Bool_263);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_263.False)
		{
			Relay_In_261();
		}
	}

	private void Relay_Save_Out_266()
	{
		Relay_Save_275();
	}

	private void Relay_Load_Out_266()
	{
		Relay_Load_275();
	}

	private void Relay_Restart_Out_266()
	{
		Relay_Set_False_275();
	}

	private void Relay_Save_266()
	{
		logic_SubGraph_SaveLoadBool_boolean_266 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_266 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Save(ref logic_SubGraph_SaveLoadBool_boolean_266, logic_SubGraph_SaveLoadBool_boolAsVariable_266, logic_SubGraph_SaveLoadBool_uniqueID_266);
	}

	private void Relay_Load_266()
	{
		logic_SubGraph_SaveLoadBool_boolean_266 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_266 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Load(ref logic_SubGraph_SaveLoadBool_boolean_266, logic_SubGraph_SaveLoadBool_boolAsVariable_266, logic_SubGraph_SaveLoadBool_uniqueID_266);
	}

	private void Relay_Set_True_266()
	{
		logic_SubGraph_SaveLoadBool_boolean_266 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_266 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_266, logic_SubGraph_SaveLoadBool_boolAsVariable_266, logic_SubGraph_SaveLoadBool_uniqueID_266);
	}

	private void Relay_Set_False_266()
	{
		logic_SubGraph_SaveLoadBool_boolean_266 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_266 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_266.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_266, logic_SubGraph_SaveLoadBool_boolAsVariable_266, logic_SubGraph_SaveLoadBool_uniqueID_266);
	}

	private void Relay_In_267()
	{
		int num = 0;
		Array array = msgLeavingEarlyDuringIntro;
		if (logic_uScript_AddOnScreenMessage_locString_267.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_267, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_267, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_267 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_267 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_267.In(logic_uScript_AddOnScreenMessage_locString_267, logic_uScript_AddOnScreenMessage_msgPriority_267, logic_uScript_AddOnScreenMessage_holdMsg_267, logic_uScript_AddOnScreenMessage_tag_267, logic_uScript_AddOnScreenMessage_speaker_267, logic_uScript_AddOnScreenMessage_side_267);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_267.Out)
		{
			Relay_True_272();
		}
	}

	private void Relay_In_271()
	{
		logic_uScriptCon_CompareBool_Bool_271 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.In(logic_uScriptCon_CompareBool_Bool_271);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.False)
		{
			Relay_In_267();
		}
	}

	private void Relay_True_272()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_272.True(out logic_uScriptAct_SetBool_Target_272);
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_uScriptAct_SetBool_Target_272;
	}

	private void Relay_False_272()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_272.False(out logic_uScriptAct_SetBool_Target_272);
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_uScriptAct_SetBool_Target_272;
	}

	private void Relay_Save_Out_275()
	{
		Relay_Save_282();
	}

	private void Relay_Load_Out_275()
	{
		Relay_Load_282();
	}

	private void Relay_Restart_Out_275()
	{
		Relay_Set_False_282();
	}

	private void Relay_Save_275()
	{
		logic_SubGraph_SaveLoadBool_boolean_275 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_275 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Save(ref logic_SubGraph_SaveLoadBool_boolean_275, logic_SubGraph_SaveLoadBool_boolAsVariable_275, logic_SubGraph_SaveLoadBool_uniqueID_275);
	}

	private void Relay_Load_275()
	{
		logic_SubGraph_SaveLoadBool_boolean_275 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_275 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Load(ref logic_SubGraph_SaveLoadBool_boolean_275, logic_SubGraph_SaveLoadBool_boolAsVariable_275, logic_SubGraph_SaveLoadBool_uniqueID_275);
	}

	private void Relay_Set_True_275()
	{
		logic_SubGraph_SaveLoadBool_boolean_275 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_275 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_275, logic_SubGraph_SaveLoadBool_boolAsVariable_275, logic_SubGraph_SaveLoadBool_uniqueID_275);
	}

	private void Relay_Set_False_275()
	{
		logic_SubGraph_SaveLoadBool_boolean_275 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_275 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_275.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_275, logic_SubGraph_SaveLoadBool_boolAsVariable_275, logic_SubGraph_SaveLoadBool_uniqueID_275);
	}

	private void Relay_In_276()
	{
		logic_uScriptCon_CompareBool_Bool_276 = local_NPCSeen_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_276.In(logic_uScriptCon_CompareBool_Bool_276);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_276.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_276.False;
		if (num)
		{
			Relay_In_271();
		}
		if (flag)
		{
			Relay_In_342();
		}
	}

	private void Relay_True_280()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_280.True(out logic_uScriptAct_SetBool_Target_280);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_280;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_280.Out)
		{
			Relay_False_284();
		}
	}

	private void Relay_False_280()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_280.False(out logic_uScriptAct_SetBool_Target_280);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_280;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_280.Out)
		{
			Relay_False_284();
		}
	}

	private void Relay_Save_Out_282()
	{
		Relay_Save_287();
	}

	private void Relay_Load_Out_282()
	{
		Relay_Load_287();
	}

	private void Relay_Restart_Out_282()
	{
		Relay_Set_False_287();
	}

	private void Relay_Save_282()
	{
		logic_SubGraph_SaveLoadBool_boolean_282 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_282 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Save(ref logic_SubGraph_SaveLoadBool_boolean_282, logic_SubGraph_SaveLoadBool_boolAsVariable_282, logic_SubGraph_SaveLoadBool_uniqueID_282);
	}

	private void Relay_Load_282()
	{
		logic_SubGraph_SaveLoadBool_boolean_282 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_282 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Load(ref logic_SubGraph_SaveLoadBool_boolean_282, logic_SubGraph_SaveLoadBool_boolAsVariable_282, logic_SubGraph_SaveLoadBool_uniqueID_282);
	}

	private void Relay_Set_True_282()
	{
		logic_SubGraph_SaveLoadBool_boolean_282 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_282 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_282, logic_SubGraph_SaveLoadBool_boolAsVariable_282, logic_SubGraph_SaveLoadBool_uniqueID_282);
	}

	private void Relay_Set_False_282()
	{
		logic_SubGraph_SaveLoadBool_boolean_282 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_282 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_282.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_282, logic_SubGraph_SaveLoadBool_boolAsVariable_282, logic_SubGraph_SaveLoadBool_uniqueID_282);
	}

	private void Relay_True_284()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_284.True(out logic_uScriptAct_SetBool_Target_284);
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_uScriptAct_SetBool_Target_284;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_284.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_False_284()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_284.False(out logic_uScriptAct_SetBool_Target_284);
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_uScriptAct_SetBool_Target_284;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_284.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_286()
	{
		logic_uScriptCon_CompareBool_Bool_286 = local_GoToStartTrigger_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_286.In(logic_uScriptCon_CompareBool_Bool_286);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_286.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_286.False;
		if (num)
		{
			Relay_In_662();
		}
		if (flag)
		{
			Relay_In_67();
		}
	}

	private void Relay_Save_Out_287()
	{
		Relay_Save_327();
	}

	private void Relay_Load_Out_287()
	{
		Relay_Load_327();
	}

	private void Relay_Restart_Out_287()
	{
		Relay_Set_False_327();
	}

	private void Relay_Save_287()
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = local_GoToStartTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_287 = local_GoToStartTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Save(ref logic_SubGraph_SaveLoadBool_boolean_287, logic_SubGraph_SaveLoadBool_boolAsVariable_287, logic_SubGraph_SaveLoadBool_uniqueID_287);
	}

	private void Relay_Load_287()
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = local_GoToStartTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_287 = local_GoToStartTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Load(ref logic_SubGraph_SaveLoadBool_boolean_287, logic_SubGraph_SaveLoadBool_boolAsVariable_287, logic_SubGraph_SaveLoadBool_uniqueID_287);
	}

	private void Relay_Set_True_287()
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = local_GoToStartTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_287 = local_GoToStartTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_287, logic_SubGraph_SaveLoadBool_boolAsVariable_287, logic_SubGraph_SaveLoadBool_uniqueID_287);
	}

	private void Relay_Set_False_287()
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = local_GoToStartTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_287 = local_GoToStartTrigger_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_287, logic_SubGraph_SaveLoadBool_boolAsVariable_287, logic_SubGraph_SaveLoadBool_uniqueID_287);
	}

	private void Relay_In_288()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_288.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_288.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_290()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_290 = StartTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_290.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_290);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_290.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_290.OutOfRange;
		if (inRange)
		{
			Relay_In_294();
		}
		if (outOfRange)
		{
			Relay_In_685();
		}
	}

	private void Relay_True_292()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_292.True(out logic_uScriptAct_SetBool_Target_292);
		local_RunStarted_System_Boolean = logic_uScriptAct_SetBool_Target_292;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_292.Out)
		{
			Relay_In_525();
		}
	}

	private void Relay_False_292()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_292.False(out logic_uScriptAct_SetBool_Target_292);
		local_RunStarted_System_Boolean = logic_uScriptAct_SetBool_Target_292;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_292.Out)
		{
			Relay_In_525();
		}
	}

	private void Relay_In_294()
	{
		int num = 0;
		Array array = msgPlateauRunIntro;
		if (logic_uScript_AddOnScreenMessage_locString_294.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_294, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_294, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_294 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_294 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_294.In(logic_uScript_AddOnScreenMessage_locString_294, logic_uScript_AddOnScreenMessage_msgPriority_294, logic_uScript_AddOnScreenMessage_holdMsg_294, logic_uScript_AddOnScreenMessage_tag_294, logic_uScript_AddOnScreenMessage_speaker_294, logic_uScript_AddOnScreenMessage_side_294);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_294.Out)
		{
			Relay_True_292();
		}
	}

	private void Relay_In_297()
	{
		logic_uScriptCon_CompareBool_Bool_297 = local_RunStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_297.In(logic_uScriptCon_CompareBool_Bool_297);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_297.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_297.False;
		if (num)
		{
			Relay_True_379();
		}
		if (flag)
		{
			Relay_In_290();
		}
	}

	private void Relay_In_302()
	{
		logic_uScriptCon_CompareBool_Bool_302 = local_RunGoing_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_302.In(logic_uScriptCon_CompareBool_Bool_302);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_302.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_302.False;
		if (num)
		{
			Relay_Pause_492();
		}
		if (flag)
		{
			Relay_UnPause_493();
		}
	}

	private void Relay_True_303()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_303.True(out logic_uScriptAct_SetBool_Target_303);
		local_RunGoing_System_Boolean = logic_uScriptAct_SetBool_Target_303;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_303.Out)
		{
			Relay_In_529();
		}
	}

	private void Relay_False_303()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_303.False(out logic_uScriptAct_SetBool_Target_303);
		local_RunGoing_System_Boolean = logic_uScriptAct_SetBool_Target_303;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_303.Out)
		{
			Relay_In_529();
		}
	}

	private void Relay_In_306()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_306 = FallTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_306.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_306);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_306.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_306.OutOfRange;
		if (inRange)
		{
			Relay_In_312();
		}
		if (outOfRange)
		{
			Relay_In_317();
		}
	}

	private void Relay_In_309()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_309 = StartTrigger;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_309.In(logic_uScript_SetEncounterTargetPosition_positionName_309);
	}

	private void Relay_True_311()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_311.True(out logic_uScriptAct_SetBool_Target_311);
		local_RunStarted_System_Boolean = logic_uScriptAct_SetBool_Target_311;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_311.Out)
		{
			Relay_False_315();
		}
	}

	private void Relay_False_311()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_311.False(out logic_uScriptAct_SetBool_Target_311);
		local_RunStarted_System_Boolean = logic_uScriptAct_SetBool_Target_311;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_311.Out)
		{
			Relay_False_315();
		}
	}

	private void Relay_In_312()
	{
		int num = 0;
		Array array = msgFellOutOfBounds;
		if (logic_uScript_AddOnScreenMessage_locString_312.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_312, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_312, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_312 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_312 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_312.In(logic_uScript_AddOnScreenMessage_locString_312, logic_uScript_AddOnScreenMessage_msgPriority_312, logic_uScript_AddOnScreenMessage_holdMsg_312, logic_uScript_AddOnScreenMessage_tag_312, logic_uScript_AddOnScreenMessage_speaker_312, logic_uScript_AddOnScreenMessage_side_312);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_312.Out)
		{
			Relay_In_536();
		}
	}

	private void Relay_True_315()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_315.True(out logic_uScriptAct_SetBool_Target_315);
		local_RunGoing_System_Boolean = logic_uScriptAct_SetBool_Target_315;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_315.Out)
		{
			Relay_In_309();
		}
	}

	private void Relay_False_315()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_315.False(out logic_uScriptAct_SetBool_Target_315);
		local_RunGoing_System_Boolean = logic_uScriptAct_SetBool_Target_315;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_315.Out)
		{
			Relay_In_309();
		}
	}

	private void Relay_In_317()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_317 = InsideTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_317.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_317);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_317.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_317.OutOfRange;
		if (inRange)
		{
			Relay_In_322();
		}
		if (outOfRange)
		{
			Relay_In_319();
		}
	}

	private void Relay_In_319()
	{
		int num = 0;
		Array array = msgThrownOutOfBounds;
		if (logic_uScript_AddOnScreenMessage_locString_319.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_319, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_319, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_319 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_319 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_319.In(logic_uScript_AddOnScreenMessage_locString_319, logic_uScript_AddOnScreenMessage_msgPriority_319, logic_uScript_AddOnScreenMessage_holdMsg_319, logic_uScript_AddOnScreenMessage_tag_319, logic_uScript_AddOnScreenMessage_speaker_319, logic_uScript_AddOnScreenMessage_side_319);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_319.Out)
		{
			Relay_In_536();
		}
	}

	private void Relay_In_322()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_322 = EndTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_322.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_322);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_322.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_322.OutOfRange;
		if (inRange)
		{
			Relay_In_325();
		}
		if (outOfRange)
		{
			Relay_In_384();
		}
	}

	private void Relay_True_324()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_324.True(out logic_uScriptAct_SetBool_Target_324);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_324;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_324.Out)
		{
			Relay_In_334();
		}
	}

	private void Relay_False_324()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_324.False(out logic_uScriptAct_SetBool_Target_324);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_324;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_324.Out)
		{
			Relay_In_334();
		}
	}

	private void Relay_In_325()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_325.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_325.Out)
		{
			Relay_True_324();
		}
	}

	private void Relay_Save_Out_327()
	{
		Relay_Save_339();
	}

	private void Relay_Load_Out_327()
	{
		Relay_Load_339();
	}

	private void Relay_Restart_Out_327()
	{
		Relay_Set_False_339();
	}

	private void Relay_Save_327()
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_327 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Save(ref logic_SubGraph_SaveLoadBool_boolean_327, logic_SubGraph_SaveLoadBool_boolAsVariable_327, logic_SubGraph_SaveLoadBool_uniqueID_327);
	}

	private void Relay_Load_327()
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_327 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Load(ref logic_SubGraph_SaveLoadBool_boolean_327, logic_SubGraph_SaveLoadBool_boolAsVariable_327, logic_SubGraph_SaveLoadBool_uniqueID_327);
	}

	private void Relay_Set_True_327()
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_327 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_327, logic_SubGraph_SaveLoadBool_boolAsVariable_327, logic_SubGraph_SaveLoadBool_uniqueID_327);
	}

	private void Relay_Set_False_327()
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_327 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_327, logic_SubGraph_SaveLoadBool_boolAsVariable_327, logic_SubGraph_SaveLoadBool_uniqueID_327);
	}

	private void Relay_In_329()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_329 = TopTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_329.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_329);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_329.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_329.OutOfRange;
		if (inRange)
		{
			Relay_In_331();
		}
		if (outOfRange)
		{
			Relay_In_306();
		}
	}

	private void Relay_In_331()
	{
		int num = 0;
		Array array = msgFlewOutOfBounds;
		if (logic_uScript_AddOnScreenMessage_locString_331.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_331, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_331, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_331 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_331 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_331.In(logic_uScript_AddOnScreenMessage_locString_331, logic_uScript_AddOnScreenMessage_msgPriority_331, logic_uScript_AddOnScreenMessage_holdMsg_331, logic_uScript_AddOnScreenMessage_tag_331, logic_uScript_AddOnScreenMessage_speaker_331, logic_uScript_AddOnScreenMessage_side_331);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_331.Out)
		{
			Relay_In_536();
		}
	}

	private void Relay_Out_334()
	{
		Relay_In_651();
	}

	private void Relay_In_334()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_334 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_334.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_334, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_334);
	}

	private void Relay_True_336()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_336.True(out logic_uScriptAct_SetBool_Target_336);
		local_PreviouslyBoughtVehicle_System_Boolean = logic_uScriptAct_SetBool_Target_336;
	}

	private void Relay_False_336()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_336.False(out logic_uScriptAct_SetBool_Target_336);
		local_PreviouslyBoughtVehicle_System_Boolean = logic_uScriptAct_SetBool_Target_336;
	}

	private void Relay_Save_Out_339()
	{
		Relay_Save_341();
	}

	private void Relay_Load_Out_339()
	{
		Relay_Load_341();
	}

	private void Relay_Restart_Out_339()
	{
		Relay_Set_False_341();
	}

	private void Relay_Save_339()
	{
		logic_SubGraph_SaveLoadBool_boolean_339 = local_PreviouslyBoughtVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_339 = local_PreviouslyBoughtVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Save(ref logic_SubGraph_SaveLoadBool_boolean_339, logic_SubGraph_SaveLoadBool_boolAsVariable_339, logic_SubGraph_SaveLoadBool_uniqueID_339);
	}

	private void Relay_Load_339()
	{
		logic_SubGraph_SaveLoadBool_boolean_339 = local_PreviouslyBoughtVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_339 = local_PreviouslyBoughtVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Load(ref logic_SubGraph_SaveLoadBool_boolean_339, logic_SubGraph_SaveLoadBool_boolAsVariable_339, logic_SubGraph_SaveLoadBool_uniqueID_339);
	}

	private void Relay_Set_True_339()
	{
		logic_SubGraph_SaveLoadBool_boolean_339 = local_PreviouslyBoughtVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_339 = local_PreviouslyBoughtVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_339, logic_SubGraph_SaveLoadBool_boolAsVariable_339, logic_SubGraph_SaveLoadBool_uniqueID_339);
	}

	private void Relay_Set_False_339()
	{
		logic_SubGraph_SaveLoadBool_boolean_339 = local_PreviouslyBoughtVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_339 = local_PreviouslyBoughtVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_339.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_339, logic_SubGraph_SaveLoadBool_boolAsVariable_339, logic_SubGraph_SaveLoadBool_uniqueID_339);
	}

	private void Relay_Save_Out_341()
	{
		Relay_Save_383();
	}

	private void Relay_Load_Out_341()
	{
		Relay_Load_383();
	}

	private void Relay_Restart_Out_341()
	{
		Relay_Set_False_383();
	}

	private void Relay_Save_341()
	{
		logic_SubGraph_SaveLoadBool_boolean_341 = local_ShownMsgStartTooEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_341 = local_ShownMsgStartTooEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Save(ref logic_SubGraph_SaveLoadBool_boolean_341, logic_SubGraph_SaveLoadBool_boolAsVariable_341, logic_SubGraph_SaveLoadBool_uniqueID_341);
	}

	private void Relay_Load_341()
	{
		logic_SubGraph_SaveLoadBool_boolean_341 = local_ShownMsgStartTooEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_341 = local_ShownMsgStartTooEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Load(ref logic_SubGraph_SaveLoadBool_boolean_341, logic_SubGraph_SaveLoadBool_boolAsVariable_341, logic_SubGraph_SaveLoadBool_uniqueID_341);
	}

	private void Relay_Set_True_341()
	{
		logic_SubGraph_SaveLoadBool_boolean_341 = local_ShownMsgStartTooEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_341 = local_ShownMsgStartTooEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_341, logic_SubGraph_SaveLoadBool_boolAsVariable_341, logic_SubGraph_SaveLoadBool_uniqueID_341);
	}

	private void Relay_Set_False_341()
	{
		logic_SubGraph_SaveLoadBool_boolean_341 = local_ShownMsgStartTooEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_341 = local_ShownMsgStartTooEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_341.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_341, logic_SubGraph_SaveLoadBool_boolAsVariable_341, logic_SubGraph_SaveLoadBool_uniqueID_341);
	}

	private void Relay_In_342()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_342 = InsideTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_342.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_342);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_342.InRange)
		{
			Relay_In_347();
		}
	}

	private void Relay_In_345()
	{
		int num = 0;
		Array array = msgStartTooEarly;
		if (logic_uScript_AddOnScreenMessage_locString_345.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_345, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_345, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_345 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_345 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_345.In(logic_uScript_AddOnScreenMessage_locString_345, logic_uScript_AddOnScreenMessage_msgPriority_345, logic_uScript_AddOnScreenMessage_holdMsg_345, logic_uScript_AddOnScreenMessage_tag_345, logic_uScript_AddOnScreenMessage_speaker_345, logic_uScript_AddOnScreenMessage_side_345);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_345.Shown)
		{
			Relay_True_346();
		}
	}

	private void Relay_True_346()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_346.True(out logic_uScriptAct_SetBool_Target_346);
		local_ShownMsgStartTooEarly_System_Boolean = logic_uScriptAct_SetBool_Target_346;
	}

	private void Relay_False_346()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_346.False(out logic_uScriptAct_SetBool_Target_346);
		local_ShownMsgStartTooEarly_System_Boolean = logic_uScriptAct_SetBool_Target_346;
	}

	private void Relay_In_347()
	{
		logic_uScriptCon_CompareBool_Bool_347 = local_ShownMsgStartTooEarly_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347.In(logic_uScriptCon_CompareBool_Bool_347);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347.False)
		{
			Relay_In_345();
		}
	}

	private void Relay_In_352()
	{
		int num = 0;
		Array array = msgYouCanReBuy;
		if (logic_uScript_AddOnScreenMessage_locString_352.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_352, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_352, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_352 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_352 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_352.In(logic_uScript_AddOnScreenMessage_locString_352, logic_uScript_AddOnScreenMessage_msgPriority_352, logic_uScript_AddOnScreenMessage_holdMsg_352, logic_uScript_AddOnScreenMessage_tag_352, logic_uScript_AddOnScreenMessage_speaker_352, logic_uScript_AddOnScreenMessage_side_352);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_352.Out)
		{
			Relay_True_372();
		}
	}

	private void Relay_True_355()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_355.True(out logic_uScriptAct_SetBool_Target_355);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_355;
	}

	private void Relay_False_355()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_355.False(out logic_uScriptAct_SetBool_Target_355);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_355;
	}

	private void Relay_In_357()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_357 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_357 = msgPromptNoMoney;
		logic_uScript_MissionPromptBlock_Show_targetBlock_357 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_357.In(logic_uScript_MissionPromptBlock_Show_bodyText_357, logic_uScript_MissionPromptBlock_Show_acceptButtonText_357, logic_uScript_MissionPromptBlock_Show_rejectButtonText_357, logic_uScript_MissionPromptBlock_Show_targetBlock_357, logic_uScript_MissionPromptBlock_Show_highlightBlock_357, logic_uScript_MissionPromptBlock_Show_singleUse_357);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_357.Out)
		{
			Relay_False_355();
		}
	}

	private void Relay_In_358()
	{
		logic_uScriptCon_CompareBool_Bool_358 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_358.In(logic_uScriptCon_CompareBool_Bool_358);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_358.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_358.False;
		if (num)
		{
			Relay_In_363();
		}
		if (flag)
		{
			Relay_In_363();
		}
	}

	private void Relay_In_359()
	{
		logic_uScriptCon_CompareBool_Bool_359 = _DEBUGIgnoreMoneyCheck;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_359.In(logic_uScriptCon_CompareBool_Bool_359);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_359.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_359.False;
		if (num)
		{
			Relay_In_358();
		}
		if (flag)
		{
			Relay_In_375();
		}
	}

	private void Relay_In_361()
	{
		logic_uScriptCon_CompareBool_Bool_361 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_361.In(logic_uScriptCon_CompareBool_Bool_361);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_361.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_361.False;
		if (num)
		{
			Relay_In_357();
		}
		if (flag)
		{
			Relay_In_357();
		}
	}

	private void Relay_In_363()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_363 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_363 = msgPromptAccept;
		logic_uScript_MissionPromptBlock_Show_rejectButtonText_363 = msgPromptDecline;
		logic_uScript_MissionPromptBlock_Show_targetBlock_363 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_363.In(logic_uScript_MissionPromptBlock_Show_bodyText_363, logic_uScript_MissionPromptBlock_Show_acceptButtonText_363, logic_uScript_MissionPromptBlock_Show_rejectButtonText_363, logic_uScript_MissionPromptBlock_Show_targetBlock_363, logic_uScript_MissionPromptBlock_Show_highlightBlock_363, logic_uScript_MissionPromptBlock_Show_singleUse_363);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_363.Out)
		{
			Relay_True_355();
		}
	}

	private void Relay_In_365()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_365 = NearNPCTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_365.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_365);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_365.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_365.OutOfRange;
		if (inRange)
		{
			Relay_In_374();
		}
		if (outOfRange)
		{
			Relay_In_682();
		}
	}

	private void Relay_In_370()
	{
		logic_uScriptCon_CompareInt_A_370 = local_CurrentMoney_System_Int32;
		logic_uScriptCon_CompareInt_B_370 = vehicleCost;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_370.In(logic_uScriptCon_CompareInt_A_370, logic_uScriptCon_CompareInt_B_370);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_370.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_370.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_358();
		}
		if (lessThan)
		{
			Relay_In_361();
		}
	}

	private void Relay_True_372()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_372.True(out logic_uScriptAct_SetBool_Target_372);
		local_ShownMsgYouCanReBuy_System_Boolean = logic_uScriptAct_SetBool_Target_372;
	}

	private void Relay_False_372()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_372.False(out logic_uScriptAct_SetBool_Target_372);
		local_ShownMsgYouCanReBuy_System_Boolean = logic_uScriptAct_SetBool_Target_372;
	}

	private void Relay_In_374()
	{
		logic_uScriptCon_CompareBool_Bool_374 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_374.In(logic_uScriptCon_CompareBool_Bool_374);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_374.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_374.False;
		if (num)
		{
			Relay_In_468();
		}
		if (flag)
		{
			Relay_In_352();
		}
	}

	private void Relay_In_375()
	{
		logic_uScript_GetCurrentMoneyEarned_Return_375 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_375.In();
		local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_375;
		if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_375.Out)
		{
			Relay_In_370();
		}
	}

	private void Relay_True_379()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_379.True(out logic_uScriptAct_SetBool_Target_379);
		local_RunAttempted_System_Boolean = logic_uScriptAct_SetBool_Target_379;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_379.Out)
		{
			Relay_True_303();
		}
	}

	private void Relay_False_379()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_379.False(out logic_uScriptAct_SetBool_Target_379);
		local_RunAttempted_System_Boolean = logic_uScriptAct_SetBool_Target_379;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_379.Out)
		{
			Relay_True_303();
		}
	}

	private void Relay_In_380()
	{
		logic_uScriptCon_CompareBool_Bool_380 = local_RunAttempted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380.In(logic_uScriptCon_CompareBool_Bool_380);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380.False;
		if (num)
		{
			Relay_In_365();
		}
		if (flag)
		{
			Relay_In_490();
		}
	}

	private void Relay_Save_Out_383()
	{
		Relay_Save_444();
	}

	private void Relay_Load_Out_383()
	{
		Relay_Load_444();
	}

	private void Relay_Restart_Out_383()
	{
		Relay_Set_False_444();
	}

	private void Relay_Save_383()
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_383 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Save(ref logic_SubGraph_SaveLoadBool_boolean_383, logic_SubGraph_SaveLoadBool_boolAsVariable_383, logic_SubGraph_SaveLoadBool_uniqueID_383);
	}

	private void Relay_Load_383()
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_383 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Load(ref logic_SubGraph_SaveLoadBool_boolean_383, logic_SubGraph_SaveLoadBool_boolAsVariable_383, logic_SubGraph_SaveLoadBool_uniqueID_383);
	}

	private void Relay_Set_True_383()
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_383 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_383, logic_SubGraph_SaveLoadBool_boolAsVariable_383, logic_SubGraph_SaveLoadBool_uniqueID_383);
	}

	private void Relay_Set_False_383()
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_383 = local_ShownMsgYouCanReBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_383, logic_SubGraph_SaveLoadBool_boolAsVariable_383, logic_SubGraph_SaveLoadBool_uniqueID_383);
	}

	private void Relay_In_384()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_384.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_384.Out)
		{
			Relay_In_386();
		}
	}

	private void Relay_In_386()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_386 = Ramp1Trigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_386.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_386);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_386.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_386.OutOfRange;
		if (inRange)
		{
			Relay_In_391();
		}
		if (outOfRange)
		{
			Relay_In_396();
		}
	}

	private void Relay_In_387()
	{
		int num = 0;
		Array array = msgRamp1;
		if (logic_uScript_AddOnScreenMessage_locString_387.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_387, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_387, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_387 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_387 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_387.In(logic_uScript_AddOnScreenMessage_locString_387, logic_uScript_AddOnScreenMessage_msgPriority_387, logic_uScript_AddOnScreenMessage_holdMsg_387, logic_uScript_AddOnScreenMessage_tag_387, logic_uScript_AddOnScreenMessage_speaker_387, logic_uScript_AddOnScreenMessage_side_387);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_387.Out)
		{
			Relay_True_388();
		}
	}

	private void Relay_True_388()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_388.True(out logic_uScriptAct_SetBool_Target_388);
		local_MsgShownRamp1_System_Boolean = logic_uScriptAct_SetBool_Target_388;
	}

	private void Relay_False_388()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_388.False(out logic_uScriptAct_SetBool_Target_388);
		local_MsgShownRamp1_System_Boolean = logic_uScriptAct_SetBool_Target_388;
	}

	private void Relay_In_391()
	{
		logic_uScriptCon_CompareBool_Bool_391 = local_MsgShownRamp1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_391.In(logic_uScriptCon_CompareBool_Bool_391);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_391.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_391.False;
		if (num)
		{
			Relay_In_396();
		}
		if (flag)
		{
			Relay_In_387();
		}
	}

	private void Relay_In_396()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_396 = Ramp2Trigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_396.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_396);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_396.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_396.OutOfRange;
		if (inRange)
		{
			Relay_In_402();
		}
		if (outOfRange)
		{
			Relay_In_408();
		}
	}

	private void Relay_In_397()
	{
		int num = 0;
		Array array = msgRamp2;
		if (logic_uScript_AddOnScreenMessage_locString_397.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_397, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_397, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_397 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_397 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_397.In(logic_uScript_AddOnScreenMessage_locString_397, logic_uScript_AddOnScreenMessage_msgPriority_397, logic_uScript_AddOnScreenMessage_holdMsg_397, logic_uScript_AddOnScreenMessage_tag_397, logic_uScript_AddOnScreenMessage_speaker_397, logic_uScript_AddOnScreenMessage_side_397);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_397.Out)
		{
			Relay_True_401();
		}
	}

	private void Relay_True_401()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_401.True(out logic_uScriptAct_SetBool_Target_401);
		local_MsgShownRamp2_System_Boolean = logic_uScriptAct_SetBool_Target_401;
	}

	private void Relay_False_401()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_401.False(out logic_uScriptAct_SetBool_Target_401);
		local_MsgShownRamp2_System_Boolean = logic_uScriptAct_SetBool_Target_401;
	}

	private void Relay_In_402()
	{
		logic_uScriptCon_CompareBool_Bool_402 = local_MsgShownRamp2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_402.In(logic_uScriptCon_CompareBool_Bool_402);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_402.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_402.False;
		if (num)
		{
			Relay_In_408();
		}
		if (flag)
		{
			Relay_In_397();
		}
	}

	private void Relay_In_403()
	{
		int num = 0;
		Array array = msgRamp3;
		if (logic_uScript_AddOnScreenMessage_locString_403.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_403, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_403, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_403 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_403 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_403.In(logic_uScript_AddOnScreenMessage_locString_403, logic_uScript_AddOnScreenMessage_msgPriority_403, logic_uScript_AddOnScreenMessage_holdMsg_403, logic_uScript_AddOnScreenMessage_tag_403, logic_uScript_AddOnScreenMessage_speaker_403, logic_uScript_AddOnScreenMessage_side_403);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_403.Out)
		{
			Relay_True_407();
		}
	}

	private void Relay_True_407()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_407.True(out logic_uScriptAct_SetBool_Target_407);
		local_MsgShownRamp3_System_Boolean = logic_uScriptAct_SetBool_Target_407;
	}

	private void Relay_False_407()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_407.False(out logic_uScriptAct_SetBool_Target_407);
		local_MsgShownRamp3_System_Boolean = logic_uScriptAct_SetBool_Target_407;
	}

	private void Relay_In_408()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_408 = Ramp3Trigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_408.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_408);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_408.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_408.OutOfRange;
		if (inRange)
		{
			Relay_In_410();
		}
		if (outOfRange)
		{
			Relay_In_417();
		}
	}

	private void Relay_In_410()
	{
		logic_uScriptCon_CompareBool_Bool_410 = local_MsgShownRamp3_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_410.In(logic_uScriptCon_CompareBool_Bool_410);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_410.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_410.False;
		if (num)
		{
			Relay_In_417();
		}
		if (flag)
		{
			Relay_In_403();
		}
	}

	private void Relay_In_412()
	{
		logic_uScriptCon_CompareBool_Bool_412 = local_MsgShownBridge1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_412.In(logic_uScriptCon_CompareBool_Bool_412);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_412.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_412.False;
		if (num)
		{
			Relay_In_426();
		}
		if (flag)
		{
			Relay_In_413();
		}
	}

	private void Relay_In_413()
	{
		int num = 0;
		Array array = msgBoosterControlsGeneric;
		if (logic_uScript_AddOnScreenMessage_locString_413.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_413, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_413, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_413 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_413 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_413.In(logic_uScript_AddOnScreenMessage_locString_413, logic_uScript_AddOnScreenMessage_msgPriority_413, logic_uScript_AddOnScreenMessage_holdMsg_413, logic_uScript_AddOnScreenMessage_tag_413, logic_uScript_AddOnScreenMessage_speaker_413, logic_uScript_AddOnScreenMessage_side_413);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_413.Out)
		{
			Relay_True_414();
		}
	}

	private void Relay_True_414()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_414.True(out logic_uScriptAct_SetBool_Target_414);
		local_MsgShownBridge1_System_Boolean = logic_uScriptAct_SetBool_Target_414;
	}

	private void Relay_False_414()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_414.False(out logic_uScriptAct_SetBool_Target_414);
		local_MsgShownBridge1_System_Boolean = logic_uScriptAct_SetBool_Target_414;
	}

	private void Relay_In_417()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_417 = Bridge1Trigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_417.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_417);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_417.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_417.OutOfRange;
		if (inRange)
		{
			Relay_In_412();
		}
		if (outOfRange)
		{
			Relay_In_426();
		}
	}

	private void Relay_In_421()
	{
		int num = 0;
		Array array = msgBridge2;
		if (logic_uScript_AddOnScreenMessage_locString_421.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_421, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_421, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_421 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_421 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_421.In(logic_uScript_AddOnScreenMessage_locString_421, logic_uScript_AddOnScreenMessage_msgPriority_421, logic_uScript_AddOnScreenMessage_holdMsg_421, logic_uScript_AddOnScreenMessage_tag_421, logic_uScript_AddOnScreenMessage_speaker_421, logic_uScript_AddOnScreenMessage_side_421);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_421.Out)
		{
			Relay_True_424();
		}
	}

	private void Relay_True_424()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_424.True(out logic_uScriptAct_SetBool_Target_424);
		local_MsgShownBridge2_System_Boolean = logic_uScriptAct_SetBool_Target_424;
	}

	private void Relay_False_424()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_424.False(out logic_uScriptAct_SetBool_Target_424);
		local_MsgShownBridge2_System_Boolean = logic_uScriptAct_SetBool_Target_424;
	}

	private void Relay_In_426()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_426 = Bridge2Trigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_426.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_426);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_426.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_426.OutOfRange;
		if (inRange)
		{
			Relay_In_428();
		}
		if (outOfRange)
		{
			Relay_In_433();
		}
	}

	private void Relay_In_428()
	{
		logic_uScriptCon_CompareBool_Bool_428 = local_MsgShownBridge2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_428.In(logic_uScriptCon_CompareBool_Bool_428);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_428.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_428.False;
		if (num)
		{
			Relay_In_433();
		}
		if (flag)
		{
			Relay_In_421();
		}
	}

	private void Relay_In_432()
	{
		int num = 0;
		Array array = msgBridge3;
		if (logic_uScript_AddOnScreenMessage_locString_432.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_432, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_432, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_432 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_432 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_432.In(logic_uScript_AddOnScreenMessage_locString_432, logic_uScript_AddOnScreenMessage_msgPriority_432, logic_uScript_AddOnScreenMessage_holdMsg_432, logic_uScript_AddOnScreenMessage_tag_432, logic_uScript_AddOnScreenMessage_speaker_432, logic_uScript_AddOnScreenMessage_side_432);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_432.Out)
		{
			Relay_True_437();
		}
	}

	private void Relay_In_433()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_433 = Bridge3Trigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_433.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_433);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_433.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_433.OutOfRange;
		if (inRange)
		{
			Relay_In_436();
		}
		if (outOfRange)
		{
			Relay_In_519();
		}
	}

	private void Relay_In_436()
	{
		logic_uScriptCon_CompareBool_Bool_436 = local_MsgShownBridge3_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_436.In(logic_uScriptCon_CompareBool_Bool_436);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_436.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_436.False;
		if (num)
		{
			Relay_In_519();
		}
		if (flag)
		{
			Relay_In_432();
		}
	}

	private void Relay_True_437()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_437.True(out logic_uScriptAct_SetBool_Target_437);
		local_MsgShownBridge3_System_Boolean = logic_uScriptAct_SetBool_Target_437;
	}

	private void Relay_False_437()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_437.False(out logic_uScriptAct_SetBool_Target_437);
		local_MsgShownBridge3_System_Boolean = logic_uScriptAct_SetBool_Target_437;
	}

	private void Relay_Save_Out_444()
	{
		Relay_Save_445();
	}

	private void Relay_Load_Out_444()
	{
		Relay_Load_445();
	}

	private void Relay_Restart_Out_444()
	{
		Relay_Set_False_445();
	}

	private void Relay_Save_444()
	{
		logic_SubGraph_SaveLoadBool_boolean_444 = local_MsgShownRamp1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_444 = local_MsgShownRamp1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Save(ref logic_SubGraph_SaveLoadBool_boolean_444, logic_SubGraph_SaveLoadBool_boolAsVariable_444, logic_SubGraph_SaveLoadBool_uniqueID_444);
	}

	private void Relay_Load_444()
	{
		logic_SubGraph_SaveLoadBool_boolean_444 = local_MsgShownRamp1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_444 = local_MsgShownRamp1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Load(ref logic_SubGraph_SaveLoadBool_boolean_444, logic_SubGraph_SaveLoadBool_boolAsVariable_444, logic_SubGraph_SaveLoadBool_uniqueID_444);
	}

	private void Relay_Set_True_444()
	{
		logic_SubGraph_SaveLoadBool_boolean_444 = local_MsgShownRamp1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_444 = local_MsgShownRamp1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_444, logic_SubGraph_SaveLoadBool_boolAsVariable_444, logic_SubGraph_SaveLoadBool_uniqueID_444);
	}

	private void Relay_Set_False_444()
	{
		logic_SubGraph_SaveLoadBool_boolean_444 = local_MsgShownRamp1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_444 = local_MsgShownRamp1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_444.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_444, logic_SubGraph_SaveLoadBool_boolAsVariable_444, logic_SubGraph_SaveLoadBool_uniqueID_444);
	}

	private void Relay_Save_Out_445()
	{
		Relay_Save_446();
	}

	private void Relay_Load_Out_445()
	{
		Relay_Load_446();
	}

	private void Relay_Restart_Out_445()
	{
		Relay_Set_False_446();
	}

	private void Relay_Save_445()
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = local_MsgShownRamp2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_445 = local_MsgShownRamp2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Save(ref logic_SubGraph_SaveLoadBool_boolean_445, logic_SubGraph_SaveLoadBool_boolAsVariable_445, logic_SubGraph_SaveLoadBool_uniqueID_445);
	}

	private void Relay_Load_445()
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = local_MsgShownRamp2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_445 = local_MsgShownRamp2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Load(ref logic_SubGraph_SaveLoadBool_boolean_445, logic_SubGraph_SaveLoadBool_boolAsVariable_445, logic_SubGraph_SaveLoadBool_uniqueID_445);
	}

	private void Relay_Set_True_445()
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = local_MsgShownRamp2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_445 = local_MsgShownRamp2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_445, logic_SubGraph_SaveLoadBool_boolAsVariable_445, logic_SubGraph_SaveLoadBool_uniqueID_445);
	}

	private void Relay_Set_False_445()
	{
		logic_SubGraph_SaveLoadBool_boolean_445 = local_MsgShownRamp2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_445 = local_MsgShownRamp2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_445.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_445, logic_SubGraph_SaveLoadBool_boolAsVariable_445, logic_SubGraph_SaveLoadBool_uniqueID_445);
	}

	private void Relay_Save_Out_446()
	{
		Relay_Save_447();
	}

	private void Relay_Load_Out_446()
	{
		Relay_Load_447();
	}

	private void Relay_Restart_Out_446()
	{
		Relay_Set_False_447();
	}

	private void Relay_Save_446()
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = local_MsgShownRamp3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_446 = local_MsgShownRamp3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Save(ref logic_SubGraph_SaveLoadBool_boolean_446, logic_SubGraph_SaveLoadBool_boolAsVariable_446, logic_SubGraph_SaveLoadBool_uniqueID_446);
	}

	private void Relay_Load_446()
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = local_MsgShownRamp3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_446 = local_MsgShownRamp3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Load(ref logic_SubGraph_SaveLoadBool_boolean_446, logic_SubGraph_SaveLoadBool_boolAsVariable_446, logic_SubGraph_SaveLoadBool_uniqueID_446);
	}

	private void Relay_Set_True_446()
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = local_MsgShownRamp3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_446 = local_MsgShownRamp3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_446, logic_SubGraph_SaveLoadBool_boolAsVariable_446, logic_SubGraph_SaveLoadBool_uniqueID_446);
	}

	private void Relay_Set_False_446()
	{
		logic_SubGraph_SaveLoadBool_boolean_446 = local_MsgShownRamp3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_446 = local_MsgShownRamp3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_446.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_446, logic_SubGraph_SaveLoadBool_boolAsVariable_446, logic_SubGraph_SaveLoadBool_uniqueID_446);
	}

	private void Relay_Save_Out_447()
	{
		Relay_Save_448();
	}

	private void Relay_Load_Out_447()
	{
		Relay_Load_448();
	}

	private void Relay_Restart_Out_447()
	{
		Relay_Set_False_448();
	}

	private void Relay_Save_447()
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = local_MsgShownBridge1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_447 = local_MsgShownBridge1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Save(ref logic_SubGraph_SaveLoadBool_boolean_447, logic_SubGraph_SaveLoadBool_boolAsVariable_447, logic_SubGraph_SaveLoadBool_uniqueID_447);
	}

	private void Relay_Load_447()
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = local_MsgShownBridge1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_447 = local_MsgShownBridge1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Load(ref logic_SubGraph_SaveLoadBool_boolean_447, logic_SubGraph_SaveLoadBool_boolAsVariable_447, logic_SubGraph_SaveLoadBool_uniqueID_447);
	}

	private void Relay_Set_True_447()
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = local_MsgShownBridge1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_447 = local_MsgShownBridge1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_447, logic_SubGraph_SaveLoadBool_boolAsVariable_447, logic_SubGraph_SaveLoadBool_uniqueID_447);
	}

	private void Relay_Set_False_447()
	{
		logic_SubGraph_SaveLoadBool_boolean_447 = local_MsgShownBridge1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_447 = local_MsgShownBridge1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_447.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_447, logic_SubGraph_SaveLoadBool_boolAsVariable_447, logic_SubGraph_SaveLoadBool_uniqueID_447);
	}

	private void Relay_Save_Out_448()
	{
		Relay_Save_449();
	}

	private void Relay_Load_Out_448()
	{
		Relay_Load_449();
	}

	private void Relay_Restart_Out_448()
	{
		Relay_Set_False_449();
	}

	private void Relay_Save_448()
	{
		logic_SubGraph_SaveLoadBool_boolean_448 = local_MsgShownBridge2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_448 = local_MsgShownBridge2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Save(ref logic_SubGraph_SaveLoadBool_boolean_448, logic_SubGraph_SaveLoadBool_boolAsVariable_448, logic_SubGraph_SaveLoadBool_uniqueID_448);
	}

	private void Relay_Load_448()
	{
		logic_SubGraph_SaveLoadBool_boolean_448 = local_MsgShownBridge2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_448 = local_MsgShownBridge2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Load(ref logic_SubGraph_SaveLoadBool_boolean_448, logic_SubGraph_SaveLoadBool_boolAsVariable_448, logic_SubGraph_SaveLoadBool_uniqueID_448);
	}

	private void Relay_Set_True_448()
	{
		logic_SubGraph_SaveLoadBool_boolean_448 = local_MsgShownBridge2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_448 = local_MsgShownBridge2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_448, logic_SubGraph_SaveLoadBool_boolAsVariable_448, logic_SubGraph_SaveLoadBool_uniqueID_448);
	}

	private void Relay_Set_False_448()
	{
		logic_SubGraph_SaveLoadBool_boolean_448 = local_MsgShownBridge2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_448 = local_MsgShownBridge2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_448.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_448, logic_SubGraph_SaveLoadBool_boolAsVariable_448, logic_SubGraph_SaveLoadBool_uniqueID_448);
	}

	private void Relay_Save_Out_449()
	{
		Relay_Save_455();
	}

	private void Relay_Load_Out_449()
	{
		Relay_Load_455();
	}

	private void Relay_Restart_Out_449()
	{
		Relay_Set_False_455();
	}

	private void Relay_Save_449()
	{
		logic_SubGraph_SaveLoadBool_boolean_449 = local_MsgShownBridge3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_449 = local_MsgShownBridge3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Save(ref logic_SubGraph_SaveLoadBool_boolean_449, logic_SubGraph_SaveLoadBool_boolAsVariable_449, logic_SubGraph_SaveLoadBool_uniqueID_449);
	}

	private void Relay_Load_449()
	{
		logic_SubGraph_SaveLoadBool_boolean_449 = local_MsgShownBridge3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_449 = local_MsgShownBridge3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Load(ref logic_SubGraph_SaveLoadBool_boolean_449, logic_SubGraph_SaveLoadBool_boolAsVariable_449, logic_SubGraph_SaveLoadBool_uniqueID_449);
	}

	private void Relay_Set_True_449()
	{
		logic_SubGraph_SaveLoadBool_boolean_449 = local_MsgShownBridge3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_449 = local_MsgShownBridge3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_449, logic_SubGraph_SaveLoadBool_boolAsVariable_449, logic_SubGraph_SaveLoadBool_uniqueID_449);
	}

	private void Relay_Set_False_449()
	{
		logic_SubGraph_SaveLoadBool_boolean_449 = local_MsgShownBridge3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_449 = local_MsgShownBridge3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_449.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_449, logic_SubGraph_SaveLoadBool_boolAsVariable_449, logic_SubGraph_SaveLoadBool_uniqueID_449);
	}

	private void Relay_In_451()
	{
		logic_uScriptCon_CompareBool_Bool_451 = local_SaidMsgNPCVehicleControls_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_451.In(logic_uScriptCon_CompareBool_Bool_451);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_451.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_451.False;
		if (num)
		{
			Relay_True_480();
		}
		if (flag)
		{
			Relay_In_632();
		}
	}

	private void Relay_True_453()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_453.True(out logic_uScriptAct_SetBool_Target_453);
		local_SaidMsgNPCVehicleControls_System_Boolean = logic_uScriptAct_SetBool_Target_453;
	}

	private void Relay_False_453()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_453.False(out logic_uScriptAct_SetBool_Target_453);
		local_SaidMsgNPCVehicleControls_System_Boolean = logic_uScriptAct_SetBool_Target_453;
	}

	private void Relay_Save_Out_455()
	{
		Relay_Save_464();
	}

	private void Relay_Load_Out_455()
	{
		Relay_Load_464();
	}

	private void Relay_Restart_Out_455()
	{
		Relay_Set_False_464();
	}

	private void Relay_Save_455()
	{
		logic_SubGraph_SaveLoadBool_boolean_455 = local_SaidMsgNPCVehicleControls_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_455 = local_SaidMsgNPCVehicleControls_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Save(ref logic_SubGraph_SaveLoadBool_boolean_455, logic_SubGraph_SaveLoadBool_boolAsVariable_455, logic_SubGraph_SaveLoadBool_uniqueID_455);
	}

	private void Relay_Load_455()
	{
		logic_SubGraph_SaveLoadBool_boolean_455 = local_SaidMsgNPCVehicleControls_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_455 = local_SaidMsgNPCVehicleControls_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Load(ref logic_SubGraph_SaveLoadBool_boolean_455, logic_SubGraph_SaveLoadBool_boolAsVariable_455, logic_SubGraph_SaveLoadBool_uniqueID_455);
	}

	private void Relay_Set_True_455()
	{
		logic_SubGraph_SaveLoadBool_boolean_455 = local_SaidMsgNPCVehicleControls_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_455 = local_SaidMsgNPCVehicleControls_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_455, logic_SubGraph_SaveLoadBool_boolAsVariable_455, logic_SubGraph_SaveLoadBool_uniqueID_455);
	}

	private void Relay_Set_False_455()
	{
		logic_SubGraph_SaveLoadBool_boolean_455 = local_SaidMsgNPCVehicleControls_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_455 = local_SaidMsgNPCVehicleControls_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_455.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_455, logic_SubGraph_SaveLoadBool_boolAsVariable_455, logic_SubGraph_SaveLoadBool_uniqueID_455);
	}

	private void Relay_In_457()
	{
		logic_uScriptCon_CompareBool_Bool_457 = local_msg03aShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_457.In(logic_uScriptCon_CompareBool_Bool_457);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_457.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_457.False;
		if (num)
		{
			Relay_In_189();
		}
		if (flag)
		{
			Relay_In_471();
		}
	}

	private void Relay_In_459()
	{
		logic_uScriptCon_CompareBool_Bool_459 = local_ExplainedHowToBuy_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_459.In(logic_uScriptCon_CompareBool_Bool_459);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_459.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_459.False;
		if (num)
		{
			Relay_False_704();
		}
		if (flag)
		{
			Relay_In_51();
		}
	}

	private void Relay_In_460()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_460.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_460.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_True_461()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_461.True(out logic_uScriptAct_SetBool_Target_461);
		local_ExplainedHowToBuy_System_Boolean = logic_uScriptAct_SetBool_Target_461;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_461.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_False_461()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_461.False(out logic_uScriptAct_SetBool_Target_461);
		local_ExplainedHowToBuy_System_Boolean = logic_uScriptAct_SetBool_Target_461;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_461.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_Save_Out_464()
	{
		Relay_Save_499();
	}

	private void Relay_Load_Out_464()
	{
		Relay_Load_499();
	}

	private void Relay_Restart_Out_464()
	{
		Relay_Set_False_499();
	}

	private void Relay_Save_464()
	{
		logic_SubGraph_SaveLoadBool_boolean_464 = local_ExplainedHowToBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_464 = local_ExplainedHowToBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Save(ref logic_SubGraph_SaveLoadBool_boolean_464, logic_SubGraph_SaveLoadBool_boolAsVariable_464, logic_SubGraph_SaveLoadBool_uniqueID_464);
	}

	private void Relay_Load_464()
	{
		logic_SubGraph_SaveLoadBool_boolean_464 = local_ExplainedHowToBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_464 = local_ExplainedHowToBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Load(ref logic_SubGraph_SaveLoadBool_boolean_464, logic_SubGraph_SaveLoadBool_boolAsVariable_464, logic_SubGraph_SaveLoadBool_uniqueID_464);
	}

	private void Relay_Set_True_464()
	{
		logic_SubGraph_SaveLoadBool_boolean_464 = local_ExplainedHowToBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_464 = local_ExplainedHowToBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_464, logic_SubGraph_SaveLoadBool_boolAsVariable_464, logic_SubGraph_SaveLoadBool_uniqueID_464);
	}

	private void Relay_Set_False_464()
	{
		logic_SubGraph_SaveLoadBool_boolean_464 = local_ExplainedHowToBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_464 = local_ExplainedHowToBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_464.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_464, logic_SubGraph_SaveLoadBool_boolAsVariable_464, logic_SubGraph_SaveLoadBool_uniqueID_464);
	}

	private void Relay_In_468()
	{
		logic_uScriptCon_CompareBool_Bool_468 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_468.In(logic_uScriptCon_CompareBool_Bool_468);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_468.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_468.False;
		if (num)
		{
			Relay_In_587();
		}
		if (flag)
		{
			Relay_In_469();
		}
	}

	private void Relay_In_469()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_469.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_469.Out)
		{
			Relay_In_578();
		}
	}

	private void Relay_In_470()
	{
		logic_uScriptCon_CompareBool_Bool_470 = local_PreviouslyBoughtVehicle_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_470.In(logic_uScriptCon_CompareBool_Bool_470);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_470.False)
		{
			Relay_In_473();
		}
	}

	private void Relay_In_471()
	{
		logic_uScriptCon_CompareBool_Bool_471 = local_PreviouslyBoughtVehicle_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_471.In(logic_uScriptCon_CompareBool_Bool_471);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_471.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_471.False;
		if (num)
		{
			Relay_In_189();
		}
		if (flag)
		{
			Relay_In_218();
		}
	}

	private void Relay_In_473()
	{
		int num = 0;
		Array array = msgNPCVehiclePurchased;
		if (logic_uScript_AddOnScreenMessage_locString_473.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_473, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_473, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_473 = msgTagSwitchTech;
		logic_uScript_AddOnScreenMessage_speaker_473 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_473 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_473.In(logic_uScript_AddOnScreenMessage_locString_473, logic_uScript_AddOnScreenMessage_msgPriority_473, logic_uScript_AddOnScreenMessage_holdMsg_473, logic_uScript_AddOnScreenMessage_tag_473, logic_uScript_AddOnScreenMessage_speaker_473, logic_uScript_AddOnScreenMessage_side_473);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_473.Out)
		{
			Relay_True_336();
		}
	}

	private void Relay_In_477()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_477.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_477.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_InitialSpawn_479()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_479.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_479, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_479, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_479 = owner_Connection_478;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_479.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_479, logic_uScript_SpawnTechsFromData_ownerNode_479, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_479, logic_uScript_SpawnTechsFromData_allowResurrection_479);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_479.Out)
		{
			Relay_In_548();
		}
	}

	private void Relay_True_480()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_480.True(out logic_uScriptAct_SetBool_Target_480);
		local_GoToStartTrigger_System_Boolean = logic_uScriptAct_SetBool_Target_480;
	}

	private void Relay_False_480()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_480.False(out logic_uScriptAct_SetBool_Target_480);
		local_GoToStartTrigger_System_Boolean = logic_uScriptAct_SetBool_Target_480;
	}

	private void Relay_In_482()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_482 = StartTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_482.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_482);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_482.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_482.OutOfRange;
		if (inRange)
		{
			Relay_In_681();
		}
		if (outOfRange)
		{
			Relay_In_286();
		}
	}

	private void Relay_In_486()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_486 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_486.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_486);
		if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_486.Out)
		{
			Relay_In_487();
		}
	}

	private void Relay_In_487()
	{
		logic_uScript_EnableGlow_targetObject_487 = local_TerminalBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_487.In(logic_uScript_EnableGlow_targetObject_487, logic_uScript_EnableGlow_enable_487);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_487.Out)
		{
			Relay_In_297();
		}
	}

	private void Relay_In_488()
	{
		logic_uScript_HideArrow_uScript_HideArrow_488.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_488.Out)
		{
			Relay_In_486();
		}
	}

	private void Relay_In_490()
	{
		logic_uScriptCon_CompareBool_Bool_490 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_490.In(logic_uScriptCon_CompareBool_Bool_490);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_490.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_490.False;
		if (num)
		{
			Relay_In_297();
		}
		if (flag)
		{
			Relay_In_488();
		}
	}

	private void Relay_In_491()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_Pause_492()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_492.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_492.Out)
		{
			Relay_In_533();
		}
	}

	private void Relay_UnPause_492()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_492.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_492.Out)
		{
			Relay_In_533();
		}
	}

	private void Relay_Pause_493()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_493.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_493.Out)
		{
			Relay_In_482();
		}
	}

	private void Relay_UnPause_493()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_493.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_493.Out)
		{
			Relay_In_482();
		}
	}

	private void Relay_In_495()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_495 = StartTrigger;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_495.In(logic_uScript_SetEncounterTargetPosition_positionName_495);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_495.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_In_497()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_497 = StartTrigger;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_497.In(logic_uScript_SetEncounterTargetPosition_positionName_497);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_497.Out)
		{
			Relay_In_236();
		}
	}

	private void Relay_Save_Out_499()
	{
		Relay_Save_524();
	}

	private void Relay_Load_Out_499()
	{
		Relay_Load_524();
	}

	private void Relay_Restart_Out_499()
	{
		Relay_Set_False_524();
	}

	private void Relay_Save_499()
	{
		logic_SubGraph_SaveLoadBool_boolean_499 = local_ShownMsgFellYouCanBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_499 = local_ShownMsgFellYouCanBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Save(ref logic_SubGraph_SaveLoadBool_boolean_499, logic_SubGraph_SaveLoadBool_boolAsVariable_499, logic_SubGraph_SaveLoadBool_uniqueID_499);
	}

	private void Relay_Load_499()
	{
		logic_SubGraph_SaveLoadBool_boolean_499 = local_ShownMsgFellYouCanBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_499 = local_ShownMsgFellYouCanBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Load(ref logic_SubGraph_SaveLoadBool_boolean_499, logic_SubGraph_SaveLoadBool_boolAsVariable_499, logic_SubGraph_SaveLoadBool_uniqueID_499);
	}

	private void Relay_Set_True_499()
	{
		logic_SubGraph_SaveLoadBool_boolean_499 = local_ShownMsgFellYouCanBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_499 = local_ShownMsgFellYouCanBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_499, logic_SubGraph_SaveLoadBool_boolAsVariable_499, logic_SubGraph_SaveLoadBool_uniqueID_499);
	}

	private void Relay_Set_False_499()
	{
		logic_SubGraph_SaveLoadBool_boolean_499 = local_ShownMsgFellYouCanBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_499 = local_ShownMsgFellYouCanBuy_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_499.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_499, logic_SubGraph_SaveLoadBool_boolAsVariable_499, logic_SubGraph_SaveLoadBool_uniqueID_499);
	}

	private void Relay_In_500()
	{
		int num = 0;
		Array array = msgOutOfTime;
		if (logic_uScript_AddOnScreenMessage_locString_500.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_500, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_500, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_500 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_500 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_500.In(logic_uScript_AddOnScreenMessage_locString_500, logic_uScript_AddOnScreenMessage_msgPriority_500, logic_uScript_AddOnScreenMessage_holdMsg_500, logic_uScript_AddOnScreenMessage_tag_500, logic_uScript_AddOnScreenMessage_speaker_500, logic_uScript_AddOnScreenMessage_side_500);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_500.Out)
		{
			Relay_In_536();
		}
	}

	private void Relay_In_504()
	{
		logic_uScriptCon_CompareBool_Bool_504 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_504.In(logic_uScriptCon_CompareBool_Bool_504);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_504.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_504.False;
		if (num)
		{
			Relay_In_500();
		}
		if (flag)
		{
			Relay_In_507();
		}
	}

	private void Relay_In_507()
	{
		int num = 0;
		Array array = msgOutOfTimeCanStillBuy;
		if (logic_uScript_AddOnScreenMessage_locString_507.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_507, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_507, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_507 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_507 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_507.In(logic_uScript_AddOnScreenMessage_locString_507, logic_uScript_AddOnScreenMessage_msgPriority_507, logic_uScript_AddOnScreenMessage_holdMsg_507, logic_uScript_AddOnScreenMessage_tag_507, logic_uScript_AddOnScreenMessage_speaker_507, logic_uScript_AddOnScreenMessage_side_507);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_507.Out)
		{
			Relay_In_536();
		}
	}

	private void Relay_In_508()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_508 = StartPosition;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_508.In(logic_uScript_SetEncounterTargetPosition_positionName_508);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_508.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_In_510()
	{
		logic_uScript_LockTechSendToSCU_tech_510 = local_vehicleTech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_510.In(logic_uScript_LockTechSendToSCU_tech_510, logic_uScript_LockTechSendToSCU_lockSendToSCU_510);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_510.Out)
		{
			Relay_In_233();
		}
	}

	private void Relay_In_513()
	{
		logic_uScriptCon_CompareFloat_A_513 = local_512_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_513.In(logic_uScriptCon_CompareFloat_A_513, logic_uScriptCon_CompareFloat_B_513);
		bool greaterThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_513.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_513.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_674();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_680();
		}
	}

	private void Relay_In_514()
	{
		logic_uScriptCon_CompareBool_Bool_514 = local_MsgShownRamp4_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514.In(logic_uScriptCon_CompareBool_Bool_514);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514.False)
		{
			Relay_In_515();
		}
	}

	private void Relay_In_515()
	{
		int num = 0;
		Array array = msgRamp4;
		if (logic_uScript_AddOnScreenMessage_locString_515.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_515, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_515, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_515 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_515 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_515.In(logic_uScript_AddOnScreenMessage_locString_515, logic_uScript_AddOnScreenMessage_msgPriority_515, logic_uScript_AddOnScreenMessage_holdMsg_515, logic_uScript_AddOnScreenMessage_tag_515, logic_uScript_AddOnScreenMessage_speaker_515, logic_uScript_AddOnScreenMessage_side_515);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_515.Out)
		{
			Relay_True_516();
		}
	}

	private void Relay_True_516()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_516.True(out logic_uScriptAct_SetBool_Target_516);
		local_MsgShownRamp4_System_Boolean = logic_uScriptAct_SetBool_Target_516;
	}

	private void Relay_False_516()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_516.False(out logic_uScriptAct_SetBool_Target_516);
		local_MsgShownRamp4_System_Boolean = logic_uScriptAct_SetBool_Target_516;
	}

	private void Relay_In_519()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_519 = Ramp4Trigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_519.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_519);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_519.InRange)
		{
			Relay_In_514();
		}
	}

	private void Relay_Save_Out_524()
	{
		Relay_Save_586();
	}

	private void Relay_Load_Out_524()
	{
		Relay_Load_586();
	}

	private void Relay_Restart_Out_524()
	{
		Relay_Set_False_586();
	}

	private void Relay_Save_524()
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = local_MsgShownRamp4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_524 = local_MsgShownRamp4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Save(ref logic_SubGraph_SaveLoadBool_boolean_524, logic_SubGraph_SaveLoadBool_boolAsVariable_524, logic_SubGraph_SaveLoadBool_uniqueID_524);
	}

	private void Relay_Load_524()
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = local_MsgShownRamp4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_524 = local_MsgShownRamp4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Load(ref logic_SubGraph_SaveLoadBool_boolean_524, logic_SubGraph_SaveLoadBool_boolAsVariable_524, logic_SubGraph_SaveLoadBool_uniqueID_524);
	}

	private void Relay_Set_True_524()
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = local_MsgShownRamp4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_524 = local_MsgShownRamp4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_524, logic_SubGraph_SaveLoadBool_boolAsVariable_524, logic_SubGraph_SaveLoadBool_uniqueID_524);
	}

	private void Relay_Set_False_524()
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = local_MsgShownRamp4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_524 = local_MsgShownRamp4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_524, logic_SubGraph_SaveLoadBool_boolAsVariable_524, logic_SubGraph_SaveLoadBool_uniqueID_524);
	}

	private void Relay_In_525()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_525 = EndTrigger;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_525.In(logic_uScript_SetEncounterTargetPosition_positionName_525);
	}

	private void Relay_In_527()
	{
		logic_uScript_ResetMissionTimer_owner_527 = owner_Connection_528;
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_527.In(logic_uScript_ResetMissionTimer_owner_527);
		if (logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_527.Out)
		{
			Relay_In_538();
		}
	}

	private void Relay_In_529()
	{
		logic_uScript_StartMissionTimer_owner_529 = owner_Connection_530;
		logic_uScript_StartMissionTimer_startTime_529 = RunTimeLimit;
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_529.In(logic_uScript_StartMissionTimer_owner_529, logic_uScript_StartMissionTimer_startTime_529);
		if (logic_uScript_StartMissionTimer_uScript_StartMissionTimer_529.Out)
		{
			Relay_In_534();
		}
	}

	private void Relay_In_531()
	{
		logic_uScript_PlayMiscSFX_miscSFXType_531 = SFXRaceStart;
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_531.In(logic_uScript_PlayMiscSFX_miscSFXType_531);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_531.Out)
		{
			Relay_In_647();
		}
	}

	private void Relay_In_533()
	{
		logic_uScript_GetMissionTimerDisplayTime_owner_533 = owner_Connection_526;
		logic_uScript_GetMissionTimerDisplayTime_Return_533 = logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_533.In(logic_uScript_GetMissionTimerDisplayTime_owner_533);
		local_512_System_Single = logic_uScript_GetMissionTimerDisplayTime_Return_533;
		if (logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_533.Out)
		{
			Relay_In_513();
		}
	}

	private void Relay_In_534()
	{
		logic_uScript_ShowMissionTimerUI_owner_534 = owner_Connection_539;
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_534.In(logic_uScript_ShowMissionTimerUI_owner_534, logic_uScript_ShowMissionTimerUI_showBestTime_534);
		if (logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_534.Out)
		{
			Relay_In_531();
		}
	}

	private void Relay_In_536()
	{
		logic_uScript_StopMissionTimer_owner_536 = owner_Connection_535;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_536.In(logic_uScript_StopMissionTimer_owner_536);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_536.Out)
		{
			Relay_In_527();
		}
	}

	private void Relay_In_537()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_537.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_537.Out)
		{
			Relay_False_311();
		}
	}

	private void Relay_In_538()
	{
		logic_uScript_HideMissionTimerUI_owner_538 = owner_Connection_707;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_538.In(logic_uScript_HideMissionTimerUI_owner_538);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_538.Out)
		{
			Relay_In_540();
		}
	}

	private void Relay_In_540()
	{
		logic_uScript_PlayMiscSFX_miscSFXType_540 = SFXRaceFailed;
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_540.In(logic_uScript_PlayMiscSFX_miscSFXType_540);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_540.Out)
		{
			Relay_In_537();
		}
	}

	private void Relay_In_541()
	{
		logic_uScript_ResetMissionTimer_owner_541 = owner_Connection_544;
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_541.In(logic_uScript_ResetMissionTimer_owner_541);
		if (logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_541.Out)
		{
			Relay_In_545();
		}
	}

	private void Relay_In_543()
	{
		logic_uScript_StopMissionTimer_owner_543 = owner_Connection_542;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_543.In(logic_uScript_StopMissionTimer_owner_543);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_543.Out)
		{
			Relay_In_541();
		}
	}

	private void Relay_In_545()
	{
		logic_uScript_HideMissionTimerUI_owner_545 = owner_Connection_710;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_545.In(logic_uScript_HideMissionTimerUI_owner_545);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_545.Out)
		{
			Relay_In_668();
		}
	}

	private void Relay_In_548()
	{
		logic_uScriptAct_MultiplyInt_v2_A_548 = vehicleCost;
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_548.In(logic_uScriptAct_MultiplyInt_v2_A_548, logic_uScriptAct_MultiplyInt_v2_B_548, out logic_uScriptAct_MultiplyInt_v2_IntResult_548, out logic_uScriptAct_MultiplyInt_v2_FloatResult_548);
		local_546_System_Int32 = logic_uScriptAct_MultiplyInt_v2_IntResult_548;
		if (logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_548.Out)
		{
			Relay_In_549();
		}
	}

	private void Relay_In_549()
	{
		logic_uScript_AddMoney_amount_549 = local_546_System_Int32;
		logic_uScript_AddMoney_uScript_AddMoney_549.In(logic_uScript_AddMoney_amount_549);
		if (logic_uScript_AddMoney_uScript_AddMoney_549.Out)
		{
			Relay_True_223();
		}
	}

	private void Relay_True_551()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_551.True(out logic_uScriptAct_SetBool_Target_551);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_551;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_551.Out)
		{
			Relay_In_552();
		}
	}

	private void Relay_False_551()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_551.False(out logic_uScriptAct_SetBool_Target_551);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_551;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_551.Out)
		{
			Relay_In_552();
		}
	}

	private void Relay_In_552()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_552 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_552 = msgPromptAccessDenied;
		logic_uScript_MissionPromptBlock_Show_targetBlock_552 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_552.In(logic_uScript_MissionPromptBlock_Show_bodyText_552, logic_uScript_MissionPromptBlock_Show_acceptButtonText_552, logic_uScript_MissionPromptBlock_Show_rejectButtonText_552, logic_uScript_MissionPromptBlock_Show_targetBlock_552, logic_uScript_MissionPromptBlock_Show_highlightBlock_552, logic_uScript_MissionPromptBlock_Show_singleUse_552);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_552.Out)
		{
			Relay_In_692();
		}
	}

	private void Relay_In_556()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_556.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_556, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_556, num, array.Length);
		num += array.Length;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_556 = local_MaxPlayers_System_Int32;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_556.In(logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_556, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_556);
		bool num2 = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_556.True;
		bool flag = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_556.False;
		if (num2)
		{
			Relay_False_572();
		}
		if (flag)
		{
			Relay_True_551();
		}
	}

	private void Relay_In_559()
	{
		logic_uScript_GetMaxPlayers_Return_559 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_559.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_559;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_559.Out)
		{
			Relay_In_556();
		}
	}

	private void Relay_In_560()
	{
		int num = 0;
		Array msgTooManyVehiclesSpawnedAlready = MsgTooManyVehiclesSpawnedAlready;
		if (logic_uScript_AddOnScreenMessage_locString_560.Length != num + msgTooManyVehiclesSpawnedAlready.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_560, num + msgTooManyVehiclesSpawnedAlready.Length);
		}
		Array.Copy(msgTooManyVehiclesSpawnedAlready, 0, logic_uScript_AddOnScreenMessage_locString_560, num, msgTooManyVehiclesSpawnedAlready.Length);
		num += msgTooManyVehiclesSpawnedAlready.Length;
		logic_uScript_AddOnScreenMessage_tag_560 = msgTagSwitchTech;
		logic_uScript_AddOnScreenMessage_speaker_560 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_560 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_560.In(logic_uScript_AddOnScreenMessage_locString_560, logic_uScript_AddOnScreenMessage_msgPriority_560, logic_uScript_AddOnScreenMessage_holdMsg_560, logic_uScript_AddOnScreenMessage_tag_560, logic_uScript_AddOnScreenMessage_speaker_560, logic_uScript_AddOnScreenMessage_side_560);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_560.Out)
		{
			Relay_False_627();
		}
	}

	private void Relay_InitialSpawn_563()
	{
		int num = 0;
		Array array = vehicleSpawnData2;
		if (logic_uScript_SpawnTechsFromData_spawnData_563.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_563, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_563, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_563 = owner_Connection_562;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_563.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_563, logic_uScript_SpawnTechsFromData_ownerNode_563, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_563, logic_uScript_SpawnTechsFromData_allowResurrection_563);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_563.Out)
		{
			Relay_In_548();
		}
	}

	private void Relay_InitialSpawn_565()
	{
		int num = 0;
		Array array = vehicleSpawnData3;
		if (logic_uScript_SpawnTechsFromData_spawnData_565.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_565, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_565, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_565 = owner_Connection_564;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_565.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_565, logic_uScript_SpawnTechsFromData_ownerNode_565, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_565, logic_uScript_SpawnTechsFromData_allowResurrection_565);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_565.Out)
		{
			Relay_In_548();
		}
	}

	private void Relay_In_570()
	{
		logic_uScriptCon_CompareBool_Bool_570 = local_BlockLimitCritical_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_570.In(logic_uScriptCon_CompareBool_Bool_570);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_570.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_570.False;
		if (num)
		{
			Relay_In_699();
		}
		if (flag)
		{
			Relay_In_197();
		}
	}

	private void Relay_True_572()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_572.True(out logic_uScriptAct_SetBool_Target_572);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_572;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_572.Out)
		{
			Relay_In_204();
		}
	}

	private void Relay_False_572()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_572.False(out logic_uScriptAct_SetBool_Target_572);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_572;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_572.Out)
		{
			Relay_In_204();
		}
	}

	private void Relay_True_573()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_573.True(out logic_uScriptAct_SetBool_Target_573);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_573;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_573.Out)
		{
			Relay_In_359();
		}
	}

	private void Relay_False_573()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_573.False(out logic_uScriptAct_SetBool_Target_573);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_573;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_573.Out)
		{
			Relay_In_359();
		}
	}

	private void Relay_In_578()
	{
		logic_uScript_GetMaxPlayers_Return_578 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_578.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_578;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_578.Out)
		{
			Relay_In_579();
		}
	}

	private void Relay_In_579()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_579.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_579, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_579, num, array.Length);
		num += array.Length;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_579 = local_MaxPlayers_System_Int32;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_579.In(logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_579, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_579);
		bool num2 = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_579.True;
		bool flag = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_579.False;
		if (num2)
		{
			Relay_False_573();
		}
		if (flag)
		{
			Relay_True_584();
		}
	}

	private void Relay_In_581()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_581 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_581 = msgPromptAccessDenied;
		logic_uScript_MissionPromptBlock_Show_targetBlock_581 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_581.In(logic_uScript_MissionPromptBlock_Show_bodyText_581, logic_uScript_MissionPromptBlock_Show_acceptButtonText_581, logic_uScript_MissionPromptBlock_Show_rejectButtonText_581, logic_uScript_MissionPromptBlock_Show_targetBlock_581, logic_uScript_MissionPromptBlock_Show_highlightBlock_581, logic_uScript_MissionPromptBlock_Show_singleUse_581);
	}

	private void Relay_True_584()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_584.True(out logic_uScriptAct_SetBool_Target_584);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_584;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_584.Out)
		{
			Relay_In_581();
		}
	}

	private void Relay_False_584()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_584.False(out logic_uScriptAct_SetBool_Target_584);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_584;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_584.Out)
		{
			Relay_In_581();
		}
	}

	private void Relay_Save_Out_586()
	{
		Relay_Save_616();
	}

	private void Relay_Load_Out_586()
	{
		Relay_Load_616();
	}

	private void Relay_Restart_Out_586()
	{
		Relay_Set_False_616();
	}

	private void Relay_Save_586()
	{
		logic_SubGraph_SaveLoadBool_boolean_586 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_586 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Save(ref logic_SubGraph_SaveLoadBool_boolean_586, logic_SubGraph_SaveLoadBool_boolAsVariable_586, logic_SubGraph_SaveLoadBool_uniqueID_586);
	}

	private void Relay_Load_586()
	{
		logic_SubGraph_SaveLoadBool_boolean_586 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_586 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Load(ref logic_SubGraph_SaveLoadBool_boolean_586, logic_SubGraph_SaveLoadBool_boolAsVariable_586, logic_SubGraph_SaveLoadBool_uniqueID_586);
	}

	private void Relay_Set_True_586()
	{
		logic_SubGraph_SaveLoadBool_boolean_586 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_586 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_586, logic_SubGraph_SaveLoadBool_boolAsVariable_586, logic_SubGraph_SaveLoadBool_uniqueID_586);
	}

	private void Relay_Set_False_586()
	{
		logic_SubGraph_SaveLoadBool_boolean_586 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_586 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_586.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_586, logic_SubGraph_SaveLoadBool_boolAsVariable_586, logic_SubGraph_SaveLoadBool_uniqueID_586);
	}

	private void Relay_In_587()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_587 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_587.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_587);
		if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_587.Out)
		{
			Relay_In_592();
		}
	}

	private void Relay_True_588()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_588.True(out logic_uScriptAct_SetBool_Target_588);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_588;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_588.Out)
		{
			Relay_True_595();
		}
	}

	private void Relay_False_588()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_588.False(out logic_uScriptAct_SetBool_Target_588);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_588;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_588.Out)
		{
			Relay_True_595();
		}
	}

	private void Relay_In_592()
	{
		logic_uScript_Wait_repeat_592 = local_RepeatWaitTime_System_Boolean;
		logic_uScript_Wait_uScript_Wait_592.In(logic_uScript_Wait_seconds_592, logic_uScript_Wait_repeat_592);
		if (logic_uScript_Wait_uScript_Wait_592.Waited)
		{
			Relay_False_588();
		}
	}

	private void Relay_True_595()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_595.True(out logic_uScriptAct_SetBool_Target_595);
		local_RepeatWaitTime_System_Boolean = logic_uScriptAct_SetBool_Target_595;
	}

	private void Relay_False_595()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_595.False(out logic_uScriptAct_SetBool_Target_595);
		local_RepeatWaitTime_System_Boolean = logic_uScriptAct_SetBool_Target_595;
	}

	private void Relay_In_596()
	{
		int num = 0;
		Array array = vehicleSpawnData3;
		if (logic_uScript_GetAndCheckTechs_techData_596.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_596, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_596, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_596 = owner_Connection_601;
		logic_uScript_GetAndCheckTechs_Return_596 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_596.In(logic_uScript_GetAndCheckTechs_techData_596, logic_uScript_GetAndCheckTechs_ownerNode_596, ref logic_uScript_GetAndCheckTechs_techs_596);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_596.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_596.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_596.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_596.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_611();
		}
		if (someAlive)
		{
			Relay_True_611();
		}
		if (allDead)
		{
			Relay_False_611();
		}
		if (waitingToSpawn)
		{
			Relay_False_611();
		}
	}

	private void Relay_In_598()
	{
		int num = 0;
		Array array = vehicleSpawnData2;
		if (logic_uScript_GetAndCheckTechs_techData_598.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_598, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_598, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_598 = owner_Connection_602;
		logic_uScript_GetAndCheckTechs_Return_598 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_598.In(logic_uScript_GetAndCheckTechs_techData_598, logic_uScript_GetAndCheckTechs_ownerNode_598, ref logic_uScript_GetAndCheckTechs_techs_598);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_598.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_598.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_598.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_598.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_610();
		}
		if (someAlive)
		{
			Relay_True_610();
		}
		if (allDead)
		{
			Relay_False_610();
		}
		if (waitingToSpawn)
		{
			Relay_False_610();
		}
	}

	private void Relay_In_604()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_604.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_604.Out)
		{
			Relay_In_605();
		}
	}

	private void Relay_In_605()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_605.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_605.Out)
		{
			Relay_In_608();
		}
	}

	private void Relay_True_606()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_606.True(out logic_uScriptAct_SetBool_Target_606);
		local_Zoomer1Alive_System_Boolean = logic_uScriptAct_SetBool_Target_606;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_606.Out)
		{
			Relay_In_598();
		}
	}

	private void Relay_False_606()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_606.False(out logic_uScriptAct_SetBool_Target_606);
		local_Zoomer1Alive_System_Boolean = logic_uScriptAct_SetBool_Target_606;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_606.Out)
		{
			Relay_In_598();
		}
	}

	private void Relay_In_608()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_608.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_608, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_608, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_608 = owner_Connection_603;
		logic_uScript_GetAndCheckTechs_Return_608 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_608.In(logic_uScript_GetAndCheckTechs_techData_608, logic_uScript_GetAndCheckTechs_ownerNode_608, ref logic_uScript_GetAndCheckTechs_techs_608);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_608.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_608.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_608.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_608.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_606();
		}
		if (someAlive)
		{
			Relay_True_606();
		}
		if (allDead)
		{
			Relay_False_606();
		}
		if (waitingToSpawn)
		{
			Relay_False_606();
		}
	}

	private void Relay_True_610()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_610.True(out logic_uScriptAct_SetBool_Target_610);
		local_Zoomer2Alive_System_Boolean = logic_uScriptAct_SetBool_Target_610;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_610.Out)
		{
			Relay_In_596();
		}
	}

	private void Relay_False_610()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_610.False(out logic_uScriptAct_SetBool_Target_610);
		local_Zoomer2Alive_System_Boolean = logic_uScriptAct_SetBool_Target_610;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_610.Out)
		{
			Relay_In_596();
		}
	}

	private void Relay_True_611()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_611.True(out logic_uScriptAct_SetBool_Target_611);
		local_Zoomer3Alive_System_Boolean = logic_uScriptAct_SetBool_Target_611;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_611.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_False_611()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_611.False(out logic_uScriptAct_SetBool_Target_611);
		local_Zoomer3Alive_System_Boolean = logic_uScriptAct_SetBool_Target_611;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_611.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_Save_Out_616()
	{
		Relay_Save_617();
	}

	private void Relay_Load_Out_616()
	{
		Relay_Load_617();
	}

	private void Relay_Restart_Out_616()
	{
		Relay_Set_False_617();
	}

	private void Relay_Save_616()
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = local_Zoomer1Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_616 = local_Zoomer1Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Save(ref logic_SubGraph_SaveLoadBool_boolean_616, logic_SubGraph_SaveLoadBool_boolAsVariable_616, logic_SubGraph_SaveLoadBool_uniqueID_616);
	}

	private void Relay_Load_616()
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = local_Zoomer1Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_616 = local_Zoomer1Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Load(ref logic_SubGraph_SaveLoadBool_boolean_616, logic_SubGraph_SaveLoadBool_boolAsVariable_616, logic_SubGraph_SaveLoadBool_uniqueID_616);
	}

	private void Relay_Set_True_616()
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = local_Zoomer1Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_616 = local_Zoomer1Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_616, logic_SubGraph_SaveLoadBool_boolAsVariable_616, logic_SubGraph_SaveLoadBool_uniqueID_616);
	}

	private void Relay_Set_False_616()
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = local_Zoomer1Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_616 = local_Zoomer1Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_616, logic_SubGraph_SaveLoadBool_boolAsVariable_616, logic_SubGraph_SaveLoadBool_uniqueID_616);
	}

	private void Relay_Save_Out_617()
	{
		Relay_Save_618();
	}

	private void Relay_Load_Out_617()
	{
		Relay_Load_618();
	}

	private void Relay_Restart_Out_617()
	{
		Relay_Set_False_618();
	}

	private void Relay_Save_617()
	{
		logic_SubGraph_SaveLoadBool_boolean_617 = local_Zoomer2Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_617 = local_Zoomer2Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Save(ref logic_SubGraph_SaveLoadBool_boolean_617, logic_SubGraph_SaveLoadBool_boolAsVariable_617, logic_SubGraph_SaveLoadBool_uniqueID_617);
	}

	private void Relay_Load_617()
	{
		logic_SubGraph_SaveLoadBool_boolean_617 = local_Zoomer2Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_617 = local_Zoomer2Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Load(ref logic_SubGraph_SaveLoadBool_boolean_617, logic_SubGraph_SaveLoadBool_boolAsVariable_617, logic_SubGraph_SaveLoadBool_uniqueID_617);
	}

	private void Relay_Set_True_617()
	{
		logic_SubGraph_SaveLoadBool_boolean_617 = local_Zoomer2Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_617 = local_Zoomer2Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_617, logic_SubGraph_SaveLoadBool_boolAsVariable_617, logic_SubGraph_SaveLoadBool_uniqueID_617);
	}

	private void Relay_Set_False_617()
	{
		logic_SubGraph_SaveLoadBool_boolean_617 = local_Zoomer2Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_617 = local_Zoomer2Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_617.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_617, logic_SubGraph_SaveLoadBool_boolAsVariable_617, logic_SubGraph_SaveLoadBool_uniqueID_617);
	}

	private void Relay_Save_Out_618()
	{
		Relay_Save_658();
	}

	private void Relay_Load_Out_618()
	{
		Relay_Set_False_658();
	}

	private void Relay_Restart_Out_618()
	{
		Relay_Set_False_658();
	}

	private void Relay_Save_618()
	{
		logic_SubGraph_SaveLoadBool_boolean_618 = local_Zoomer3Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_618 = local_Zoomer3Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Save(ref logic_SubGraph_SaveLoadBool_boolean_618, logic_SubGraph_SaveLoadBool_boolAsVariable_618, logic_SubGraph_SaveLoadBool_uniqueID_618);
	}

	private void Relay_Load_618()
	{
		logic_SubGraph_SaveLoadBool_boolean_618 = local_Zoomer3Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_618 = local_Zoomer3Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Load(ref logic_SubGraph_SaveLoadBool_boolean_618, logic_SubGraph_SaveLoadBool_boolAsVariable_618, logic_SubGraph_SaveLoadBool_uniqueID_618);
	}

	private void Relay_Set_True_618()
	{
		logic_SubGraph_SaveLoadBool_boolean_618 = local_Zoomer3Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_618 = local_Zoomer3Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_618, logic_SubGraph_SaveLoadBool_boolAsVariable_618, logic_SubGraph_SaveLoadBool_uniqueID_618);
	}

	private void Relay_Set_False_618()
	{
		logic_SubGraph_SaveLoadBool_boolean_618 = local_Zoomer3Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_618 = local_Zoomer3Alive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_618.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_618, logic_SubGraph_SaveLoadBool_boolAsVariable_618, logic_SubGraph_SaveLoadBool_uniqueID_618);
	}

	private void Relay_In_620()
	{
		logic_uScriptCon_CompareBool_Bool_620 = local_Zoomer1Alive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_620.In(logic_uScriptCon_CompareBool_Bool_620);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_620.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_620.False;
		if (num)
		{
			Relay_In_625();
		}
		if (flag)
		{
			Relay_InitialSpawn_479();
		}
	}

	private void Relay_In_625()
	{
		logic_uScriptCon_CompareBool_Bool_625 = local_Zoomer2Alive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_625.In(logic_uScriptCon_CompareBool_Bool_625);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_625.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_625.False;
		if (num)
		{
			Relay_In_626();
		}
		if (flag)
		{
			Relay_InitialSpawn_563();
		}
	}

	private void Relay_In_626()
	{
		logic_uScriptCon_CompareBool_Bool_626 = local_Zoomer3Alive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_626.In(logic_uScriptCon_CompareBool_Bool_626);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_626.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_626.False;
		if (num)
		{
			Relay_In_560();
		}
		if (flag)
		{
			Relay_InitialSpawn_565();
		}
	}

	private void Relay_True_627()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_627.True(out logic_uScriptAct_SetBool_Target_627);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_627;
	}

	private void Relay_False_627()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_627.False(out logic_uScriptAct_SetBool_Target_627);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_627;
	}

	private void Relay_In_632()
	{
		int num = 0;
		Array array = msgBoosterControlsZoomer;
		if (logic_uScript_AddOnScreenMessage_locString_632.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_632, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_632, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_632 = msgTagControls;
		logic_uScript_AddOnScreenMessage_speaker_632 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_632 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_632.In(logic_uScript_AddOnScreenMessage_locString_632, logic_uScript_AddOnScreenMessage_msgPriority_632, logic_uScript_AddOnScreenMessage_holdMsg_632, logic_uScript_AddOnScreenMessage_tag_632, logic_uScript_AddOnScreenMessage_speaker_632, logic_uScript_AddOnScreenMessage_side_632);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_632.Out)
		{
			Relay_True_453();
		}
	}

	private void Relay_In_634()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_634.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_634, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_634, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_634 = owner_Connection_635;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_634.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_634, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_634, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_634 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_634.In(logic_uScript_GetAndCheckTechs_techData_634, logic_uScript_GetAndCheckTechs_ownerNode_634, ref logic_uScript_GetAndCheckTechs_techs_634);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_634;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_634.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_634.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_636();
		}
		if (someAlive)
		{
			Relay_AtIndex_636();
		}
	}

	private void Relay_AtIndex_636()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_636.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_636, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_636, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_636.AtIndex(ref logic_uScript_AccessListTech_techList_636, logic_uScript_AccessListTech_index_636, out logic_uScript_AccessListTech_value_636);
		local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_636;
		local_techNPC_Tank = logic_uScript_AccessListTech_value_636;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_636.Out)
		{
			Relay_In_176();
		}
	}

	private void Relay_In_641()
	{
		logic_uScript_SetTankInvulnerable_tank_641 = local_PaymentPointTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_641.In(logic_uScript_SetTankInvulnerable_invulnerable_641, logic_uScript_SetTankInvulnerable_tank_641);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_641.Out)
		{
			Relay_True_109();
		}
	}

	private void Relay_In_643()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_643.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_643, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_643, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_643 = owner_Connection_640;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_643.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_643, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_643, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_643 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_643.In(logic_uScript_GetAndCheckTechs_techData_643, logic_uScript_GetAndCheckTechs_ownerNode_643, ref logic_uScript_GetAndCheckTechs_techs_643);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_643;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_643.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_643.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_646();
		}
		if (someAlive)
		{
			Relay_AtIndex_646();
		}
	}

	private void Relay_AtIndex_646()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_646.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_646, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_646, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_646.AtIndex(ref logic_uScript_AccessListTech_techList_646, logic_uScript_AccessListTech_index_646, out logic_uScript_AccessListTech_value_646);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_646;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_646;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_646.Out)
		{
			Relay_In_641();
		}
	}

	private void Relay_In_647()
	{
		int num = 0;
		if (logic_uScriptAct_Log_Target_647.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Log_Target_647, num + 1);
		}
		logic_uScriptAct_Log_Target_647[num++] = SFXRaceStart;
		logic_uScriptAct_Log_uScriptAct_Log_647.In(logic_uScriptAct_Log_Prefix_647, logic_uScriptAct_Log_Target_647, logic_uScriptAct_Log_Postfix_647);
	}

	private void Relay_In_651()
	{
		logic_uScript_PlayMiscSFX_miscSFXType_651 = SFXRaceComplete;
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_651.In(logic_uScript_PlayMiscSFX_miscSFXType_651);
	}

	private void Relay_In_652()
	{
		logic_uScript_HideMissionTimerUI_owner_652 = owner_Connection_708;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_652.In(logic_uScript_HideMissionTimerUI_owner_652);
	}

	private void Relay_In_653()
	{
		logic_uScript_StopMissionTimer_owner_653 = owner_Connection_656;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_653.In(logic_uScript_StopMissionTimer_owner_653);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_653.Out)
		{
			Relay_In_655();
		}
	}

	private void Relay_In_655()
	{
		logic_uScript_ResetMissionTimer_owner_655 = owner_Connection_654;
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_655.In(logic_uScript_ResetMissionTimer_owner_655);
		if (logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_655.Out)
		{
			Relay_In_652();
		}
	}

	private void Relay_Save_Out_658()
	{
		Relay_Save_660();
	}

	private void Relay_Load_Out_658()
	{
		Relay_Set_False_660();
	}

	private void Relay_Restart_Out_658()
	{
		Relay_Set_False_660();
	}

	private void Relay_Save_658()
	{
		logic_SubGraph_SaveLoadBool_boolean_658 = local_RunStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_658 = local_RunStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Save(ref logic_SubGraph_SaveLoadBool_boolean_658, logic_SubGraph_SaveLoadBool_boolAsVariable_658, logic_SubGraph_SaveLoadBool_uniqueID_658);
	}

	private void Relay_Load_658()
	{
		logic_SubGraph_SaveLoadBool_boolean_658 = local_RunStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_658 = local_RunStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Load(ref logic_SubGraph_SaveLoadBool_boolean_658, logic_SubGraph_SaveLoadBool_boolAsVariable_658, logic_SubGraph_SaveLoadBool_uniqueID_658);
	}

	private void Relay_Set_True_658()
	{
		logic_SubGraph_SaveLoadBool_boolean_658 = local_RunStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_658 = local_RunStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_658, logic_SubGraph_SaveLoadBool_boolAsVariable_658, logic_SubGraph_SaveLoadBool_uniqueID_658);
	}

	private void Relay_Set_False_658()
	{
		logic_SubGraph_SaveLoadBool_boolean_658 = local_RunStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_658 = local_RunStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_658.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_658, logic_SubGraph_SaveLoadBool_boolAsVariable_658, logic_SubGraph_SaveLoadBool_uniqueID_658);
	}

	private void Relay_Save_Out_660()
	{
	}

	private void Relay_Load_Out_660()
	{
		Relay_In_653();
	}

	private void Relay_Restart_Out_660()
	{
	}

	private void Relay_Save_660()
	{
		logic_SubGraph_SaveLoadBool_boolean_660 = local_RunGoing_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_660 = local_RunGoing_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Save(ref logic_SubGraph_SaveLoadBool_boolean_660, logic_SubGraph_SaveLoadBool_boolAsVariable_660, logic_SubGraph_SaveLoadBool_uniqueID_660);
	}

	private void Relay_Load_660()
	{
		logic_SubGraph_SaveLoadBool_boolean_660 = local_RunGoing_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_660 = local_RunGoing_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Load(ref logic_SubGraph_SaveLoadBool_boolean_660, logic_SubGraph_SaveLoadBool_boolAsVariable_660, logic_SubGraph_SaveLoadBool_uniqueID_660);
	}

	private void Relay_Set_True_660()
	{
		logic_SubGraph_SaveLoadBool_boolean_660 = local_RunGoing_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_660 = local_RunGoing_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_660, logic_SubGraph_SaveLoadBool_boolAsVariable_660, logic_SubGraph_SaveLoadBool_uniqueID_660);
	}

	private void Relay_Set_False_660()
	{
		logic_SubGraph_SaveLoadBool_boolean_660 = local_RunGoing_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_660 = local_RunGoing_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_660.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_660, logic_SubGraph_SaveLoadBool_boolAsVariable_660, logic_SubGraph_SaveLoadBool_uniqueID_660);
	}

	private void Relay_In_662()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_662 = StartTrigger;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_662.In(logic_uScript_SetEncounterTargetPosition_positionName_662);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_662.Out)
		{
			Relay_In_380();
		}
	}

	private void Relay_Out_664()
	{
		Relay_Load_3();
	}

	private void Relay_In_664()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_664 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_664.In(logic_SubGraph_LoadObjectiveStates_currentObjective_664);
	}

	private void Relay_In_666()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_666.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_666.Out)
		{
			Relay_Save_3();
		}
	}

	private void Relay_In_667()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_667.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_667.Out)
		{
			Relay_Set_False_3();
		}
	}

	private void Relay_In_668()
	{
		logic_uScriptCon_CompareBool_Bool_668 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_668.In(logic_uScriptCon_CompareBool_Bool_668);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_668.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_668.False;
		if (num)
		{
			Relay_In_14();
		}
		if (flag)
		{
			Relay_In_671();
		}
	}

	private void Relay_In_671()
	{
		int num = 0;
		Array array = msgMissionCompleteNoZoomer;
		if (logic_uScript_AddOnScreenMessage_locString_671.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_671, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_671, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_671 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_671 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_671.In(logic_uScript_AddOnScreenMessage_locString_671, logic_uScript_AddOnScreenMessage_msgPriority_671, logic_uScript_AddOnScreenMessage_holdMsg_671, logic_uScript_AddOnScreenMessage_tag_671, logic_uScript_AddOnScreenMessage_speaker_671, logic_uScript_AddOnScreenMessage_side_671);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_671.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_674()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_674 = FlightBlocker1Trigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_674.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_674);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_674.Out)
		{
			Relay_In_678();
		}
	}

	private void Relay_In_676()
	{
		int num = 0;
		Array array = msgFlightBlockerHit;
		if (logic_uScript_AddOnScreenMessage_locString_676.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_676, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_676, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_676 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_676 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_676.In(logic_uScript_AddOnScreenMessage_locString_676, logic_uScript_AddOnScreenMessage_msgPriority_676, logic_uScript_AddOnScreenMessage_holdMsg_676, logic_uScript_AddOnScreenMessage_tag_676, logic_uScript_AddOnScreenMessage_speaker_676, logic_uScript_AddOnScreenMessage_side_676);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_676.Out)
		{
			Relay_In_536();
		}
	}

	private void Relay_In_678()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_678 = FlightBlocker2Trigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_678.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_678);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_678.Out)
		{
			Relay_In_329();
		}
	}

	private void Relay_In_680()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_680.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_680.Out)
		{
			Relay_In_504();
		}
	}

	private void Relay_In_681()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_681.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_681.Out)
		{
			Relay_In_490();
		}
	}

	private void Relay_In_682()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_682.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_682.Out)
		{
			Relay_In_683();
		}
	}

	private void Relay_In_683()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_683.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_683.Out)
		{
			Relay_In_297();
		}
	}

	private void Relay_In_685()
	{
		logic_uScript_StopMissionTimer_owner_685 = owner_Connection_684;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_685.In(logic_uScript_StopMissionTimer_owner_685);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_685.Out)
		{
			Relay_In_688();
		}
	}

	private void Relay_In_686()
	{
		logic_uScript_HideMissionTimerUI_owner_686 = owner_Connection_709;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_686.In(logic_uScript_HideMissionTimerUI_owner_686);
	}

	private void Relay_In_688()
	{
		logic_uScript_ResetMissionTimer_owner_688 = owner_Connection_687;
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_688.In(logic_uScript_ResetMissionTimer_owner_688);
		if (logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_688.Out)
		{
			Relay_In_686();
		}
	}

	private void Relay_In_689()
	{
		logic_uScript_HideArrow_uScript_HideArrow_689.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_689.Out)
		{
			Relay_In_691();
		}
	}

	private void Relay_In_691()
	{
		logic_uScript_EnableGlow_targetObject_691 = local_TerminalBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_691.In(logic_uScript_EnableGlow_targetObject_691, logic_uScript_EnableGlow_enable_691);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_691.Out)
		{
			Relay_In_470();
		}
	}

	private void Relay_In_692()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_692.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_692.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_694()
	{
		logic_uScript_AddMessage_messageData_694 = msgDespawnTechs;
		logic_uScript_AddMessage_speaker_694 = SpeakerNPC;
		logic_uScript_AddMessage_Return_694 = logic_uScript_AddMessage_uScript_AddMessage_694.In(logic_uScript_AddMessage_messageData_694, logic_uScript_AddMessage_speaker_694);
		if (logic_uScript_AddMessage_uScript_AddMessage_694.Out)
		{
			Relay_True_698();
		}
	}

	private void Relay_In_695()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_695.In();
	}

	private void Relay_True_698()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_698.True(out logic_uScriptAct_SetBool_Target_698);
		local_msgDespawnTechsShown_System_Boolean = logic_uScriptAct_SetBool_Target_698;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_698.Out)
		{
			Relay_In_695();
		}
	}

	private void Relay_False_698()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_698.False(out logic_uScriptAct_SetBool_Target_698);
		local_msgDespawnTechsShown_System_Boolean = logic_uScriptAct_SetBool_Target_698;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_698.Out)
		{
			Relay_In_695();
		}
	}

	private void Relay_In_699()
	{
		logic_uScriptCon_CompareBool_Bool_699 = local_msgDespawnTechsShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_699.In(logic_uScriptCon_CompareBool_Bool_699);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_699.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_699.False;
		if (num)
		{
			Relay_In_695();
		}
		if (flag)
		{
			Relay_In_694();
		}
	}

	private void Relay_True_704()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_704.True(out logic_uScriptAct_SetBool_Target_704);
		local_msgDespawnTechsShown_System_Boolean = logic_uScriptAct_SetBool_Target_704;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_704.Out)
		{
			Relay_False_706();
		}
	}

	private void Relay_False_704()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_704.False(out logic_uScriptAct_SetBool_Target_704);
		local_msgDespawnTechsShown_System_Boolean = logic_uScriptAct_SetBool_Target_704;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_704.Out)
		{
			Relay_False_706();
		}
	}

	private void Relay_True_705()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_705.True(out logic_uScriptAct_SetBool_Target_705);
		local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_705;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_705.Out)
		{
			Relay_In_460();
		}
	}

	private void Relay_False_705()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_705.False(out logic_uScriptAct_SetBool_Target_705);
		local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_705;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_705.Out)
		{
			Relay_In_460();
		}
	}

	private void Relay_True_706()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_706.True(out logic_uScriptAct_SetBool_Target_706);
		local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_706;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_706.Out)
		{
			Relay_False_705();
		}
	}

	private void Relay_False_706()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_706.False(out logic_uScriptAct_SetBool_Target_706);
		local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_706;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_706.Out)
		{
			Relay_False_705();
		}
	}
}
