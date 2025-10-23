using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_DefeatEnemyTechs", "")]
public class Mission_SetPiece_Airfield_01 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool _DEBUGIgnoreMoneyCheck;

	public BlockTypes[] discoverableBlockTypesOnVehicle = new BlockTypes[0];

	public BlockTypes interactableBlockType;

	private int local_193_System_Int32;

	private bool local_232_System_Boolean;

	private TankBlock local_238_TankBlock;

	private bool local_BlockLimitCritical_System_Boolean;

	private int local_CurrentMoney_System_Int32;

	private bool local_HasEnoughMoney_System_Boolean;

	private int local_MaxPlayers_System_Int32;

	private bool local_msg03aShown_System_Boolean;

	private bool local_msg03bShown_System_Boolean;

	private bool local_msgDespawnTechsShown_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgPurchaseVehicle_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgPurchaseVehicle_Pad_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgSwitchTech_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgSwitchTech_Pad_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgVehicleControls_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgVehicleControls_Pad_ManOnScreenMessages_OnScreenMessage;

	private bool local_NPCIntroMessagePlayed_System_Boolean;

	private bool local_NPCMet_System_Boolean;

	private Tank[] local_NPCPaymentPoints_TankArray = new Tank[0];

	private bool local_NPCSeen_System_Boolean;

	private Tank[] local_NPCTechs_TankArray = new Tank[0];

	private bool local_ObjectiveComplete_System_Boolean;

	private Tank local_PaymentPointTech_Tank;

	private bool local_SaidMsgNPCVehiclePurchased_System_Boolean;

	private bool local_SaidMsgNPCVehicleSwitched_System_Boolean;

	private bool local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;

	private bool local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;

	private bool local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private bool local_SwitchedVehicle_System_Boolean;

	private Tank local_techNPC_Tank;

	private bool local_TechSetUp_System_Boolean;

	private bool local_TechSpawned_System_Boolean;

	private TankBlock local_TerminalBlock_TankBlock;

	private bool local_VehiclePurchased_System_Boolean;

	private bool local_VehicleSetup_System_Boolean;

	private Tank local_vehicleTech_Tank;

	private Tank local_vehicleTech2_Tank;

	private Tank local_vehicleTech3_Tank;

	private Tank local_vehicleTech4_Tank;

	private Tank[] local_vehicleTechs_TankArray = new Tank[0];

	private Tank[] local_vehicleTechs2_TankArray = new Tank[0];

	private Tank[] local_vehicleTechs3_TankArray = new Tank[0];

	private Tank[] local_vehicleTechs4_TankArray = new Tank[0];

	private bool local_Wait_System_Boolean;

	private bool local_WaitingOnPrompt_System_Boolean;

	public uScript_AddMessage.MessageData msgDespawnTechs;

	public LocalisedString[] msgLeavingEarlyDuringIntro = new LocalisedString[0];

	public LocalisedString[] msgLeavingEarlyPostPurchase = new LocalisedString[0];

	public LocalisedString[] msgLeavingEarlyPrePurchase = new LocalisedString[0];

	public LocalisedString[] msgMissionComplete = new LocalisedString[0];

	public LocalisedString[] msgNPCIntro = new LocalisedString[0];

	public uScript_AddMessage.MessageData msgNPCNotEnoughMoney;

	public uScript_AddMessage.MessageData msgNPCPurchaseDeclined;

	public LocalisedString[] msgNPCVehiclePurchased = new LocalisedString[0];

	public LocalisedString[] msgNPCVehicleSwitched = new LocalisedString[0];

	public LocalisedString msgPromptAccept;

	public LocalisedString msgPromptAccessDenied;

	public LocalisedString msgPromptDecline;

	public LocalisedString msgPromptNoMoney;

	public LocalisedString msgPromptText;

	public uScript_AddMessage.MessageData msgPurchaseVehicle;

	public uScript_AddMessage.MessageData msgPurchaseVehicle_Pad;

	public uScript_AddMessage.MessageData msgSwitchTech;

	public uScript_AddMessage.MessageData msgSwitchTech_Pad;

	[Multiline(3)]
	public string msgTagControls = "";

	[Multiline(3)]
	public string msgTagPurchase = "";

	[Multiline(3)]
	public string msgTagSwitchTech = "";

	public uScript_AddMessage.MessageData msgVehicleControls;

	public uScript_AddMessage.MessageData msgVehicleControls_Pad;

	[Multiline(3)]
	public string NearNPCTrigger = "";

	public ExternalBehaviorTree NPCFlyAwayBehavior;

	public SpawnTechData[] NPCPaymentPoint = new SpawnTechData[0];

	public ManOnScreenMessages.Speaker NPCSpeaker;

	public SpawnTechData[] NPCTechData = new SpawnTechData[0];

	public uScript_AddMessage.MessageSpeaker SpeakerNPC;

	public int vehicleCost;

	public SpawnTechData[] vehicleSpawnData = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnData2 = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnData3 = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnData4 = new SpawnTechData[0];

	private GameObject owner_Connection_4;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_31;

	private GameObject owner_Connection_66;

	private GameObject owner_Connection_69;

	private GameObject owner_Connection_117;

	private GameObject owner_Connection_125;

	private GameObject owner_Connection_132;

	private GameObject owner_Connection_157;

	private GameObject owner_Connection_184;

	private GameObject owner_Connection_191;

	private GameObject owner_Connection_225;

	private GameObject owner_Connection_249;

	private GameObject owner_Connection_264;

	private GameObject owner_Connection_315;

	private GameObject owner_Connection_317;

	private GameObject owner_Connection_329;

	private GameObject owner_Connection_333;

	private GameObject owner_Connection_336;

	private GameObject owner_Connection_354;

	private GameObject owner_Connection_402;

	private GameObject owner_Connection_405;

	private GameObject owner_Connection_410;

	private GameObject owner_Connection_440;

	private GameObject owner_Connection_446;

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

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_48 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_48;

	private bool logic_uScriptAct_SetBool_Out_48 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_48 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_48 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_49 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_50 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_50;

	private BlockTypes logic_uScript_GetTankBlock_blockType_50;

	private TankBlock logic_uScript_GetTankBlock_Return_50;

	private bool logic_uScript_GetTankBlock_Out_50 = true;

	private bool logic_uScript_GetTankBlock_Returned_50 = true;

	private bool logic_uScript_GetTankBlock_NotFound_50 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_52;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_52;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_52;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_52;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_52;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_54 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_54 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_54 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_54 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_54 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_56 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_56;

	private bool logic_uScriptCon_CompareBool_True_56 = true;

	private bool logic_uScriptCon_CompareBool_False_56 = true;

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

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_65 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_65 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_68;

	private bool logic_uScriptCon_CompareBool_True_68 = true;

	private bool logic_uScriptCon_CompareBool_False_68 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_71 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_71 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_71 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_71 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_72 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_72 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_72 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_72 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_77 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_77;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_77 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_79 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_79 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_81 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_81;

	private bool logic_uScriptAct_SetBool_Out_81 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_81 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_81 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_83 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_83 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_83 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_83 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_83 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_84 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_84 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_84 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_84;

	private string logic_uScript_AddOnScreenMessage_tag_84 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_84;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_84;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_84;

	private bool logic_uScript_AddOnScreenMessage_Out_84 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_84 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_88 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_88 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_88 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_88 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_92;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_92 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_92 = "SwitchedVehicle";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_93;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_93 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_93 = "VehiclePurchased";

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_95 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_95 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_95 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_95 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_95 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_97 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_97 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_97 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_97 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_97 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_99;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_99 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_99 = "HasEnoughMoney";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_101;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_101 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_101 = "WaitingOnPrompt";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_106;

	private bool logic_uScriptCon_CompareBool_True_106 = true;

	private bool logic_uScriptCon_CompareBool_False_106 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_108 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_108;

	private bool logic_uScriptAct_SetBool_Out_108 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_108 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_108 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_110;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_110 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_110 = "TechSetUp";

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_111 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_111;

	private bool logic_uScript_IsTechPlayer_Out_111 = true;

	private bool logic_uScript_IsTechPlayer_True_111 = true;

	private bool logic_uScript_IsTechPlayer_False_111 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_112;

	private bool logic_uScriptCon_CompareBool_True_112 = true;

	private bool logic_uScriptCon_CompareBool_False_112 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_114;

	private bool logic_uScriptCon_CompareBool_True_114 = true;

	private bool logic_uScriptCon_CompareBool_False_114 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_116 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_116;

	private bool logic_uScriptAct_SetBool_Out_116 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_116 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_116 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_119 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_119 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_119;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_119 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_119;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_119 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_119 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_119 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_119 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_120 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_120 = new Tank[0];

	private int logic_uScript_AccessListTech_index_120;

	private Tank logic_uScript_AccessListTech_value_120;

	private bool logic_uScript_AccessListTech_Out_120 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_123;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_123 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_123 = "VehicleSetup";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_126 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_126 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_126;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_126 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_126;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_126 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_126 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_126 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_126 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_127 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_127 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_127 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_127 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_133 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_133;

	private bool logic_uScript_ClearEncounterTarget_Out_133 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_135 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_135 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_135;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_135 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_138;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_138;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_138;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_138;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_138;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_143 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_143;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_143 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_144 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_144 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_145 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_145 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_147 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_147 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_147 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_147 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_147 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_149 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_149 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_149;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_149 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_151 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_151 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_151 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_153 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_153;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_153 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_153 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_158 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_158;

	private object logic_uScript_SetEncounterTarget_visibleObject_158 = "";

	private bool logic_uScript_SetEncounterTarget_Out_158 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_159 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_159;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_159 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_159 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_159;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_159;

	private bool logic_uScript_FlyTechUpAndAway_Out_159 = true;

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

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_166 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_166;

	private bool logic_uScriptCon_CompareBool_True_166 = true;

	private bool logic_uScriptCon_CompareBool_False_166 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_168 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_168;

	private bool logic_uScriptAct_SetBool_Out_168 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_168 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_168 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_170 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_170;

	private bool logic_uScriptAct_SetBool_Out_170 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_170 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_170 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_174 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_174 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_174 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_174;

	private string logic_uScript_AddOnScreenMessage_tag_174 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_174;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_174;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_174;

	private bool logic_uScript_AddOnScreenMessage_Out_174 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_174 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_175;

	private bool logic_uScriptCon_CompareBool_True_175 = true;

	private bool logic_uScriptCon_CompareBool_False_175 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_180;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_180 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_180 = "SaidMsgNPCVehiclePurchased";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_181;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_181 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_181 = "SaidMsgNPCVehicleSwitched";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_183 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_183 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_183;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_183 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_183;

	private bool logic_uScript_SpawnTechsFromData_Out_183 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_185 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_185;

	private bool logic_uScript_SetTankInvulnerable_Out_185 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_188 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_188 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_188;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_188 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_188;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_188 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_188 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_188 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_188 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_189 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_189 = new Tank[0];

	private int logic_uScript_AccessListTech_index_189;

	private Tank logic_uScript_AccessListTech_value_189;

	private bool logic_uScript_AccessListTech_Out_189 = true;

	private uScriptAct_MultiplyInt_v2 logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_194 = new uScriptAct_MultiplyInt_v2();

	private int logic_uScriptAct_MultiplyInt_v2_A_194;

	private int logic_uScriptAct_MultiplyInt_v2_B_194 = -1;

	private int logic_uScriptAct_MultiplyInt_v2_IntResult_194;

	private float logic_uScriptAct_MultiplyInt_v2_FloatResult_194;

	private bool logic_uScriptAct_MultiplyInt_v2_Out_194 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_197;

	private bool logic_uScriptCon_CompareBool_True_197 = true;

	private bool logic_uScriptCon_CompareBool_False_197 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_199 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_199;

	private bool logic_uScriptAct_SetBool_Out_199 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_199 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_199 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_203 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_203 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_206;

	private bool logic_uScriptCon_CompareBool_True_206 = true;

	private bool logic_uScriptCon_CompareBool_False_206 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_207 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_207;

	private int logic_uScriptCon_CompareInt_B_207;

	private bool logic_uScriptCon_CompareInt_GreaterThan_207 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_207 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_207 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_207 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_207 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_207 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_208;

	private bool logic_uScriptCon_CompareBool_True_208 = true;

	private bool logic_uScriptCon_CompareBool_False_208 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_209 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_209;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_209 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_211;

	private bool logic_uScriptCon_CompareBool_True_211 = true;

	private bool logic_uScriptCon_CompareBool_False_211 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_212;

	private bool logic_uScriptCon_CompareBool_True_212 = true;

	private bool logic_uScriptCon_CompareBool_False_212 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_214 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_214;

	private TankBlock logic_uScript_CompareBlock_B_214;

	private bool logic_uScript_CompareBlock_EqualTo_214 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_214 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_215 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_215 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_219;

	private bool logic_uScriptCon_CompareBool_True_219 = true;

	private bool logic_uScriptCon_CompareBool_False_219 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_220 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_220 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_221 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_221;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_221;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_221;

	private bool logic_uScript_AddMessage_Out_221 = true;

	private bool logic_uScript_AddMessage_Shown_221 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_226 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_226;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_226;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_226;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_226;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_226 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_226;

	private bool logic_uScript_MissionPromptBlock_Show_Out_226 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_227 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_227;

	private bool logic_uScriptAct_SetBool_Out_227 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_227 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_227 = true;

	private uScript_AddMoney logic_uScript_AddMoney_uScript_AddMoney_228 = new uScript_AddMoney();

	private int logic_uScript_AddMoney_amount_228;

	private bool logic_uScript_AddMoney_Out_228 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_229 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_229;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_229;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_229;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_229;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_229 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_229 = true;

	private bool logic_uScript_MissionPromptBlock_Show_Out_229 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_231 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_231;

	private bool logic_uScriptAct_SetBool_Out_231 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_231 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_231 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_235 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_235;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_235;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_235;

	private bool logic_uScript_AddMessage_Out_235 = true;

	private bool logic_uScript_AddMessage_Shown_235 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_236 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_236;

	private bool logic_uScriptCon_CompareBool_True_236 = true;

	private bool logic_uScriptCon_CompareBool_False_236 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_241 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_241;

	private bool logic_uScriptAct_SetBool_Out_241 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_241 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_241 = true;

	private uScript_DiscoverBlocks logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_243 = new uScript_DiscoverBlocks();

	private BlockTypes[] logic_uScript_DiscoverBlocks_blockTypes_243 = new BlockTypes[0];

	private bool logic_uScript_DiscoverBlocks_Out_243 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_247 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_247 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_247;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_247 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_247;

	private bool logic_uScript_SpawnTechsFromData_Out_247 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_248 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_248;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_248;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_253 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_253 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_254 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_254 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_254 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_254 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_254 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_255 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_255 = "";

	private bool logic_uScript_EnableGlow_enable_255 = true;

	private bool logic_uScript_EnableGlow_Out_255 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_257 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_257 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_258 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_258 = "";

	private bool logic_uScript_EnableGlow_enable_258;

	private bool logic_uScript_EnableGlow_Out_258 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_261 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_261;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_261 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_263 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_263;

	private bool logic_uScript_ClearEncounterTarget_Out_263 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_266 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_266;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_266 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_266 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_266;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_266;

	private bool logic_uScript_FlyTechUpAndAway_Out_266 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_268 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_268;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_268 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_271;

	private bool logic_uScriptCon_CompareBool_True_271 = true;

	private bool logic_uScriptCon_CompareBool_False_271 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_272 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_272 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_272 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_272;

	private string logic_uScript_AddOnScreenMessage_tag_272 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_272;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_272;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_272;

	private bool logic_uScript_AddOnScreenMessage_Out_272 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_272 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_275 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_275;

	private bool logic_uScriptAct_SetBool_Out_275 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_275 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_275 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_278;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_278 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_278 = "ShownMsgLeavingEarlyPrePurchase";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_281 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_281;

	private bool logic_uScriptAct_SetBool_Out_281 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_281 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_281 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_282 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_282 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_282 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_282;

	private string logic_uScript_AddOnScreenMessage_tag_282 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_282;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_282;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_282;

	private bool logic_uScript_AddOnScreenMessage_Out_282 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_282 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_284 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_284;

	private bool logic_uScriptCon_CompareBool_True_284 = true;

	private bool logic_uScriptCon_CompareBool_False_284 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_287;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_287 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_287 = "ShownMsgLeavingEarlyPostPurchase";

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_288 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_288 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_288 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_288;

	private string logic_uScript_AddOnScreenMessage_tag_288 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_288;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_288;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_288;

	private bool logic_uScript_AddOnScreenMessage_Out_288 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_288 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_292 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_292;

	private bool logic_uScriptCon_CompareBool_True_292 = true;

	private bool logic_uScriptCon_CompareBool_False_292 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_293 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_293;

	private bool logic_uScriptAct_SetBool_Out_293 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_293 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_293 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_296;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_296 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_296 = "ShownMsgLeavingEarlyDuringIntro";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_297 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_297;

	private bool logic_uScriptCon_CompareBool_True_297 = true;

	private bool logic_uScriptCon_CompareBool_False_297 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_301 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_301;

	private bool logic_uScriptAct_SetBool_Out_301 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_301 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_301 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_303;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_303 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_303 = "NPCSeen";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_305 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_305;

	private bool logic_uScriptAct_SetBool_Out_305 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_305 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_305 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_306 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_306;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_306 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_306 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_308 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_308 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_309 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_309 = new Tank[0];

	private int logic_uScript_AccessListTech_index_309;

	private Tank logic_uScript_AccessListTech_value_309;

	private bool logic_uScript_AccessListTech_Out_309 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_312 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_312 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_312;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_312 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_312;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_312 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_312 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_312 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_312 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_314 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_314 = new Tank[0];

	private int logic_uScript_AccessListTech_index_314;

	private Tank logic_uScript_AccessListTech_value_314;

	private bool logic_uScript_AccessListTech_Out_314 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_319 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_319 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_319;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_319 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_319;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_319 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_319 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_319 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_319 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_320 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_320 = new Tank[0];

	private int logic_uScript_AccessListTech_index_320;

	private Tank logic_uScript_AccessListTech_value_320;

	private bool logic_uScript_AccessListTech_Out_320 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_322 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_322;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_322;

	private bool logic_uScript_LockTech_Out_322 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_323 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_323;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_323 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_323 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_325 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_325;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_325;

	private bool logic_uScript_LockTech_Out_325 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_326 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_326;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_326 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_326 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_327 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_327;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_327 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_327 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_328 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_328 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_328 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_330 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_330 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_330;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_330 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_330;

	private bool logic_uScript_SpawnTechsFromData_Out_330 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_334 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_334 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_334;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_334 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_334;

	private bool logic_uScript_SpawnTechsFromData_Out_334 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_337 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_337 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_337;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_337 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_337;

	private bool logic_uScript_SpawnTechsFromData_Out_337 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_338 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_338 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_338 = true;

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_342 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_342;

	private bool logic_uScript_IsTechPlayer_Out_342 = true;

	private bool logic_uScript_IsTechPlayer_True_342 = true;

	private bool logic_uScript_IsTechPlayer_False_342 = true;

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_343 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_343;

	private bool logic_uScript_IsTechPlayer_Out_343 = true;

	private bool logic_uScript_IsTechPlayer_True_343 = true;

	private bool logic_uScript_IsTechPlayer_False_343 = true;

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_344 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_344;

	private bool logic_uScript_IsTechPlayer_Out_344 = true;

	private bool logic_uScript_IsTechPlayer_True_344 = true;

	private bool logic_uScript_IsTechPlayer_False_344 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_345 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_345 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_345 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_346 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_346 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_347 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_347 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_348 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_348 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_349 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_349 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_350 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_350 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_350 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_351 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_351 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_351 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_352 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_352 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_353 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_353;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_353 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_353 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_353 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_355 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_355 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_356 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_356 = true;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_357 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_357;

	private bool logic_uScript_GetMaxPlayers_Out_357 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_359;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_360;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_362 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_362;

	private bool logic_uScript_GetMaxPlayers_Out_362 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_363 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_363;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_363 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_363 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_365 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_365;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_365;

	private bool logic_uScript_LockTech_Out_365 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_366 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_366;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_366 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_366 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_367 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_367;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_367 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_367 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_369 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_369;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_369;

	private bool logic_uScript_LockTech_Out_369 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_370 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_370;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_370 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_370 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_371 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_371;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_371;

	private bool logic_uScript_LockTech_Out_371 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_372 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_372;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_372 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_372 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_374 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_374;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_374 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_374 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_375 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_375 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_376 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_376;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_376 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_376 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_377 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_377;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_377 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_377 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_379 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_379;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_379 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_379 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_381;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_383 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_383;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_383 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_383 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_385 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_385;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_385;

	private bool logic_uScript_LockTech_Out_385 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_386 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_386;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_386;

	private bool logic_uScript_LockTech_Out_386 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_387 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_387;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_387 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_387 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_388 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_388 = true;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_389 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_389;

	private bool logic_uScript_GetMaxPlayers_Out_389 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_390 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_390;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_390 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_390 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_391 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_391;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_391;

	private bool logic_uScript_LockTech_Out_391 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_392;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_394 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_394;

	private bool logic_uScript_GetMaxPlayers_Out_394 = true;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_395 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_395;

	private bool logic_uScript_GetMaxPlayers_Out_395 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_396;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_399 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_399 = new Tank[0];

	private int logic_uScript_AccessListTech_index_399;

	private Tank logic_uScript_AccessListTech_value_399;

	private bool logic_uScript_AccessListTech_Out_399 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_403 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_403 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_403;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_403 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_403;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_403 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_403 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_403 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_403 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_406 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_406 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_406;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_406 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_406;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_406 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_406 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_406 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_406 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_408 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_408 = new Tank[0];

	private int logic_uScript_AccessListTech_index_408;

	private Tank logic_uScript_AccessListTech_value_408;

	private bool logic_uScript_AccessListTech_Out_408 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_412 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_412 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_412;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_412 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_412;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_412 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_412 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_412 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_412 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_414 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_414 = new Tank[0];

	private int logic_uScript_AccessListTech_index_414;

	private Tank logic_uScript_AccessListTech_value_414;

	private bool logic_uScript_AccessListTech_Out_414 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_416 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_416 = true;

	private uScript_CanSpawnPlayerTechsWithinBlockLimit logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_422 = new uScript_CanSpawnPlayerTechsWithinBlockLimit();

	private SpawnTechData[] logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_422 = new SpawnTechData[0];

	private int logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_422;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_Out_422 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_True_422 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_False_422 = true;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_425 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_425;

	private bool logic_uScript_GetMaxPlayers_Out_425 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_427 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_427;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_427;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_427;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_427;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_427 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_427;

	private bool logic_uScript_MissionPromptBlock_Show_Out_427 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_431;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_431 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_431 = "BlockLimitCritical";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_433 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_433;

	private bool logic_uScriptAct_SetBool_Out_433 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_433 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_433 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_434 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_434;

	private bool logic_uScriptAct_SetBool_Out_434 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_434 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_434 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_437 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_437 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_437;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_437 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_437;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_437 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_437 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_437 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_437 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_439 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_439 = new Tank[0];

	private int logic_uScript_AccessListTech_index_439;

	private Tank logic_uScript_AccessListTech_value_439;

	private bool logic_uScript_AccessListTech_Out_439 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_443 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_443 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_443;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_443 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_443;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_443 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_443 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_443 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_443 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_445 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_445 = new Tank[0];

	private int logic_uScript_AccessListTech_index_445;

	private Tank logic_uScript_AccessListTech_value_445;

	private bool logic_uScript_AccessListTech_Out_445 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_448 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_448 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_449 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_449 = "";

	private bool logic_uScript_EnableGlow_enable_449;

	private bool logic_uScript_EnableGlow_Out_449 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_451 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_451 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_452;

	private bool logic_uScriptCon_CompareBool_True_452 = true;

	private bool logic_uScriptCon_CompareBool_False_452 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_457 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_457 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_458 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_458;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_458;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_458;

	private bool logic_uScript_AddMessage_Out_458 = true;

	private bool logic_uScript_AddMessage_Shown_458 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_459 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_459;

	private bool logic_uScriptAct_SetBool_Out_459 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_459 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_459 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_460 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_460;

	private bool logic_uScriptCon_CompareBool_True_460 = true;

	private bool logic_uScriptCon_CompareBool_False_460 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_463;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_463 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_463 = "msgDespawnTechsShown";

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_464 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_464 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_466 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_466;

	private bool logic_uScriptAct_SetBool_Out_466 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_466 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_466 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_467 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_467;

	private bool logic_uScriptAct_SetBool_Out_467 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_467 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_467 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_469 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_469;

	private bool logic_uScriptAct_SetBool_Out_469 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_469 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_469 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_471 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_471;

	private bool logic_uScriptAct_SetBool_Out_471 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_471 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_471 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_473 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_473 = 10f;

	private bool logic_uScript_Wait_repeat_473 = true;

	private bool logic_uScript_Wait_Waited_473 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_474 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_474;

	private bool logic_uScriptCon_CompareBool_True_474 = true;

	private bool logic_uScriptCon_CompareBool_False_474 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_477 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_477;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_479;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_479 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_479 = "Wait";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_481 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_481;

	private bool logic_uScriptAct_SetBool_Out_481 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_481 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_481 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_484 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_484;

	private bool logic_uScriptAct_SetBool_Out_484 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_484 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_484 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_485 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_485;

	private bool logic_uScriptAct_SetBool_Out_485 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_485 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_485 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_486 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_486 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_486 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_488 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_488;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_488;

	private bool logic_uScript_LockTechSendToSCU_Out_488 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_489 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_489;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_489;

	private bool logic_uScript_LockTechSendToSCU_Out_489 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_491 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_491;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_491;

	private bool logic_uScript_LockTechSendToSCU_Out_491 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_493 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_493;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_493;

	private bool logic_uScript_LockTechSendToSCU_Out_493 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_494;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_499 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_499 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_499 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_500 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_500 = true;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_501 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_501;

	private bool logic_uScript_GetMaxPlayers_Out_501 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_502 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_502 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_503 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_503 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_504 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_504 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_505 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_505 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_506 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_506 = true;

	private TankBlock event_UnityEngine_GameObject_TankBlock_196;

	private bool event_UnityEngine_GameObject_Accepted_196;

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
		if (null == owner_Connection_66 || !m_RegisteredForEvents)
		{
			owner_Connection_66 = parentGameObject;
		}
		if (null == owner_Connection_69 || !m_RegisteredForEvents)
		{
			owner_Connection_69 = parentGameObject;
		}
		if (null == owner_Connection_117 || !m_RegisteredForEvents)
		{
			owner_Connection_117 = parentGameObject;
		}
		if (null == owner_Connection_125 || !m_RegisteredForEvents)
		{
			owner_Connection_125 = parentGameObject;
		}
		if (null == owner_Connection_132 || !m_RegisteredForEvents)
		{
			owner_Connection_132 = parentGameObject;
		}
		if (null == owner_Connection_157 || !m_RegisteredForEvents)
		{
			owner_Connection_157 = parentGameObject;
		}
		if (null == owner_Connection_184 || !m_RegisteredForEvents)
		{
			owner_Connection_184 = parentGameObject;
		}
		if (null == owner_Connection_191 || !m_RegisteredForEvents)
		{
			owner_Connection_191 = parentGameObject;
		}
		if (null == owner_Connection_225 || !m_RegisteredForEvents)
		{
			owner_Connection_225 = parentGameObject;
			if (null != owner_Connection_225)
			{
				uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_225.GetComponent<uScript_MissionPromptBlock_OnResult>();
				if (null == uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2 = owner_Connection_225.AddComponent<uScript_MissionPromptBlock_OnResult>();
				}
				if (null != uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_196;
				}
			}
		}
		if (null == owner_Connection_249 || !m_RegisteredForEvents)
		{
			owner_Connection_249 = parentGameObject;
		}
		if (null == owner_Connection_264 || !m_RegisteredForEvents)
		{
			owner_Connection_264 = parentGameObject;
		}
		if (null == owner_Connection_315 || !m_RegisteredForEvents)
		{
			owner_Connection_315 = parentGameObject;
		}
		if (null == owner_Connection_317 || !m_RegisteredForEvents)
		{
			owner_Connection_317 = parentGameObject;
		}
		if (null == owner_Connection_329 || !m_RegisteredForEvents)
		{
			owner_Connection_329 = parentGameObject;
		}
		if (null == owner_Connection_333 || !m_RegisteredForEvents)
		{
			owner_Connection_333 = parentGameObject;
		}
		if (null == owner_Connection_336 || !m_RegisteredForEvents)
		{
			owner_Connection_336 = parentGameObject;
		}
		if (null == owner_Connection_354 || !m_RegisteredForEvents)
		{
			owner_Connection_354 = parentGameObject;
		}
		if (null == owner_Connection_402 || !m_RegisteredForEvents)
		{
			owner_Connection_402 = parentGameObject;
		}
		if (null == owner_Connection_405 || !m_RegisteredForEvents)
		{
			owner_Connection_405 = parentGameObject;
		}
		if (null == owner_Connection_410 || !m_RegisteredForEvents)
		{
			owner_Connection_410 = parentGameObject;
		}
		if (null == owner_Connection_440 || !m_RegisteredForEvents)
		{
			owner_Connection_440 = parentGameObject;
		}
		if (null == owner_Connection_446 || !m_RegisteredForEvents)
		{
			owner_Connection_446 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_225)
		{
			uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_225.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null == uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2 = owner_Connection_225.AddComponent<uScript_MissionPromptBlock_OnResult>();
			}
			if (null != uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_196;
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
		if (null != owner_Connection_225)
		{
			uScript_MissionPromptBlock_OnResult component3 = owner_Connection_225.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null != component3)
			{
				component3.ResponseEvent -= Instance_ResponseEvent_196;
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
		logic_uScriptAct_SetBool_uScriptAct_SetBool_48.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_50.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_54.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_56.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_60.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_61.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_65.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_71.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_72.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_77.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_79.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_81.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_83.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_84.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_88.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_95.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_97.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_111.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_116.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_119.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_120.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_126.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_127.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_133.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_135.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_143.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_144.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_145.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_147.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_149.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_151.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_153.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_158.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_159.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_163.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_166.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_168.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_170.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_174.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_183.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_188.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_189.SetParent(g);
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_194.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_199.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_203.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_207.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_209.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_214.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_215.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_220.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_221.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_226.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_227.SetParent(g);
		logic_uScript_AddMoney_uScript_AddMoney_228.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_229.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_231.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_235.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_236.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_241.SetParent(g);
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_243.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_247.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_248.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_253.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_254.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_255.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_257.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_258.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_261.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_263.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_266.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_268.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_272.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_275.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_281.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_282.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_284.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_288.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_292.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_297.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_301.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_305.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_306.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_308.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_309.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_312.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_314.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_319.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_320.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_322.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_323.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_325.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_326.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_327.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_328.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_330.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_334.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_337.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_338.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_342.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_343.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_344.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_345.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_346.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_347.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_348.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_349.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_350.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_351.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_352.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_353.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_355.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_356.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_357.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_362.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_363.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_365.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_366.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_367.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_369.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_370.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_371.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_372.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_374.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_375.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_376.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_377.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_379.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_383.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_385.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_386.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_387.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_388.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_389.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_390.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_391.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_394.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_395.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_399.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_403.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_406.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_408.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_412.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_414.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_416.SetParent(g);
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_422.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_425.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_427.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_433.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_434.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_437.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_439.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_443.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_445.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_448.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_449.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_451.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_457.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_458.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_459.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_460.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_464.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_466.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_467.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_469.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_471.SetParent(g);
		logic_uScript_Wait_uScript_Wait_473.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_474.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_477.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_481.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_484.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_485.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_486.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_488.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_489.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_491.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_493.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_499.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_500.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_501.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_502.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_503.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_504.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_505.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_506.SetParent(g);
		owner_Connection_4 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_31 = parentGameObject;
		owner_Connection_66 = parentGameObject;
		owner_Connection_69 = parentGameObject;
		owner_Connection_117 = parentGameObject;
		owner_Connection_125 = parentGameObject;
		owner_Connection_132 = parentGameObject;
		owner_Connection_157 = parentGameObject;
		owner_Connection_184 = parentGameObject;
		owner_Connection_191 = parentGameObject;
		owner_Connection_225 = parentGameObject;
		owner_Connection_249 = parentGameObject;
		owner_Connection_264 = parentGameObject;
		owner_Connection_315 = parentGameObject;
		owner_Connection_317 = parentGameObject;
		owner_Connection_329 = parentGameObject;
		owner_Connection_333 = parentGameObject;
		owner_Connection_336 = parentGameObject;
		owner_Connection_354 = parentGameObject;
		owner_Connection_402 = parentGameObject;
		owner_Connection_405 = parentGameObject;
		owner_Connection_410 = parentGameObject;
		owner_Connection_440 = parentGameObject;
		owner_Connection_446 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_143.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_248.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_477.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Awake();
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
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52.Out += SubGraph_AddMessageWithPadSupport_Out_52;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52.Shown += SubGraph_AddMessageWithPadSupport_Shown_52;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Out += SubGraph_AddMessageWithPadSupport_Out_64;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Shown += SubGraph_AddMessageWithPadSupport_Shown_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Save_Out += SubGraph_SaveLoadBool_Save_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Load_Out += SubGraph_SaveLoadBool_Load_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save_Out += SubGraph_SaveLoadBool_Save_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load_Out += SubGraph_SaveLoadBool_Load_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Save_Out += SubGraph_SaveLoadBool_Save_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Load_Out += SubGraph_SaveLoadBool_Load_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Save_Out += SubGraph_SaveLoadBool_Save_Out_101;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Load_Out += SubGraph_SaveLoadBool_Load_Out_101;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_101;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Save_Out += SubGraph_SaveLoadBool_Save_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Load_Out += SubGraph_SaveLoadBool_Load_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Save_Out += SubGraph_SaveLoadBool_Save_Out_123;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Load_Out += SubGraph_SaveLoadBool_Load_Out_123;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_123;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138.Out += SubGraph_AddMessageWithPadSupport_Out_138;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138.Shown += SubGraph_AddMessageWithPadSupport_Shown_138;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_143.Out += SubGraph_CompleteObjectiveStage_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Save_Out += SubGraph_SaveLoadBool_Save_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Load_Out += SubGraph_SaveLoadBool_Load_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Save_Out += SubGraph_SaveLoadBool_Save_Out_181;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Load_Out += SubGraph_SaveLoadBool_Load_Out_181;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_181;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_248.Out += SubGraph_CompleteObjectiveStage_Out_248;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Save_Out += SubGraph_SaveLoadBool_Save_Out_278;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Load_Out += SubGraph_SaveLoadBool_Load_Out_278;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_278;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Save_Out += SubGraph_SaveLoadBool_Save_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Load_Out += SubGraph_SaveLoadBool_Load_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Save_Out += SubGraph_SaveLoadBool_Save_Out_296;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Load_Out += SubGraph_SaveLoadBool_Load_Out_296;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_296;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Save_Out += SubGraph_SaveLoadBool_Save_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Load_Out += SubGraph_SaveLoadBool_Load_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_303;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output1 += uScriptCon_ManualSwitch_Output1_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output2 += uScriptCon_ManualSwitch_Output2_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output3 += uScriptCon_ManualSwitch_Output3_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output4 += uScriptCon_ManualSwitch_Output4_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output5 += uScriptCon_ManualSwitch_Output5_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output6 += uScriptCon_ManualSwitch_Output6_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output7 += uScriptCon_ManualSwitch_Output7_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output8 += uScriptCon_ManualSwitch_Output8_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output1 += uScriptCon_ManualSwitch_Output1_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output2 += uScriptCon_ManualSwitch_Output2_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output3 += uScriptCon_ManualSwitch_Output3_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output4 += uScriptCon_ManualSwitch_Output4_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output5 += uScriptCon_ManualSwitch_Output5_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output6 += uScriptCon_ManualSwitch_Output6_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output7 += uScriptCon_ManualSwitch_Output7_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output8 += uScriptCon_ManualSwitch_Output8_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output1 += uScriptCon_ManualSwitch_Output1_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output2 += uScriptCon_ManualSwitch_Output2_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output3 += uScriptCon_ManualSwitch_Output3_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output4 += uScriptCon_ManualSwitch_Output4_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output5 += uScriptCon_ManualSwitch_Output5_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output6 += uScriptCon_ManualSwitch_Output6_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output7 += uScriptCon_ManualSwitch_Output7_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output8 += uScriptCon_ManualSwitch_Output8_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output1 += uScriptCon_ManualSwitch_Output1_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output2 += uScriptCon_ManualSwitch_Output2_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output3 += uScriptCon_ManualSwitch_Output3_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output4 += uScriptCon_ManualSwitch_Output4_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output5 += uScriptCon_ManualSwitch_Output5_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output6 += uScriptCon_ManualSwitch_Output6_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output7 += uScriptCon_ManualSwitch_Output7_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output8 += uScriptCon_ManualSwitch_Output8_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output1 += uScriptCon_ManualSwitch_Output1_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output2 += uScriptCon_ManualSwitch_Output2_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output3 += uScriptCon_ManualSwitch_Output3_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output4 += uScriptCon_ManualSwitch_Output4_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output5 += uScriptCon_ManualSwitch_Output5_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output6 += uScriptCon_ManualSwitch_Output6_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output7 += uScriptCon_ManualSwitch_Output7_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output8 += uScriptCon_ManualSwitch_Output8_396;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Save_Out += SubGraph_SaveLoadBool_Save_Out_431;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Load_Out += SubGraph_SaveLoadBool_Load_Out_431;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_431;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Save_Out += SubGraph_SaveLoadBool_Save_Out_463;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Load_Out += SubGraph_SaveLoadBool_Load_Out_463;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_463;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_477.Out += SubGraph_LoadObjectiveStates_Out_477;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Save_Out += SubGraph_SaveLoadBool_Save_Out_479;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Load_Out += SubGraph_SaveLoadBool_Load_Out_479;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_479;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output1 += uScriptCon_ManualSwitch_Output1_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output2 += uScriptCon_ManualSwitch_Output2_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output3 += uScriptCon_ManualSwitch_Output3_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output4 += uScriptCon_ManualSwitch_Output4_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output5 += uScriptCon_ManualSwitch_Output5_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output6 += uScriptCon_ManualSwitch_Output6_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output7 += uScriptCon_ManualSwitch_Output7_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output8 += uScriptCon_ManualSwitch_Output8_494;
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
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_143.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_248.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_477.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Start();
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
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_77.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_143.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_159.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_248.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_266.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_477.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.OnEnable();
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
		logic_uScript_GetTankBlock_uScript_GetTankBlock_50.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_84.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_143.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_163.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_174.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_221.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_235.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_248.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_272.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_282.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_288.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_328.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_338.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_345.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_350.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_351.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_353.OnDisable();
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_422.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_458.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.OnDisable();
		logic_uScript_Wait_uScript_Wait_473.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_477.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_499.OnDisable();
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
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_143.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_248.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_477.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_24.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_143.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_248.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_477.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.OnDestroy();
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
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52.Out -= SubGraph_AddMessageWithPadSupport_Out_52;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52.Shown -= SubGraph_AddMessageWithPadSupport_Shown_52;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Out -= SubGraph_AddMessageWithPadSupport_Out_64;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_64.Shown -= SubGraph_AddMessageWithPadSupport_Shown_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Save_Out -= SubGraph_SaveLoadBool_Save_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Load_Out -= SubGraph_SaveLoadBool_Load_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save_Out -= SubGraph_SaveLoadBool_Save_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load_Out -= SubGraph_SaveLoadBool_Load_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Save_Out -= SubGraph_SaveLoadBool_Save_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Load_Out -= SubGraph_SaveLoadBool_Load_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Save_Out -= SubGraph_SaveLoadBool_Save_Out_101;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Load_Out -= SubGraph_SaveLoadBool_Load_Out_101;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_101;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Save_Out -= SubGraph_SaveLoadBool_Save_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Load_Out -= SubGraph_SaveLoadBool_Load_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Save_Out -= SubGraph_SaveLoadBool_Save_Out_123;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Load_Out -= SubGraph_SaveLoadBool_Load_Out_123;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_123;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138.Out -= SubGraph_AddMessageWithPadSupport_Out_138;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138.Shown -= SubGraph_AddMessageWithPadSupport_Shown_138;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_143.Out -= SubGraph_CompleteObjectiveStage_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Save_Out -= SubGraph_SaveLoadBool_Save_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Load_Out -= SubGraph_SaveLoadBool_Load_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Save_Out -= SubGraph_SaveLoadBool_Save_Out_181;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Load_Out -= SubGraph_SaveLoadBool_Load_Out_181;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_181;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_248.Out -= SubGraph_CompleteObjectiveStage_Out_248;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Save_Out -= SubGraph_SaveLoadBool_Save_Out_278;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Load_Out -= SubGraph_SaveLoadBool_Load_Out_278;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_278;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Save_Out -= SubGraph_SaveLoadBool_Save_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Load_Out -= SubGraph_SaveLoadBool_Load_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Save_Out -= SubGraph_SaveLoadBool_Save_Out_296;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Load_Out -= SubGraph_SaveLoadBool_Load_Out_296;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_296;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Save_Out -= SubGraph_SaveLoadBool_Save_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Load_Out -= SubGraph_SaveLoadBool_Load_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_303;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output1 -= uScriptCon_ManualSwitch_Output1_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output2 -= uScriptCon_ManualSwitch_Output2_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output3 -= uScriptCon_ManualSwitch_Output3_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output4 -= uScriptCon_ManualSwitch_Output4_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output5 -= uScriptCon_ManualSwitch_Output5_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output6 -= uScriptCon_ManualSwitch_Output6_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output7 -= uScriptCon_ManualSwitch_Output7_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.Output8 -= uScriptCon_ManualSwitch_Output8_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output1 -= uScriptCon_ManualSwitch_Output1_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output2 -= uScriptCon_ManualSwitch_Output2_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output3 -= uScriptCon_ManualSwitch_Output3_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output4 -= uScriptCon_ManualSwitch_Output4_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output5 -= uScriptCon_ManualSwitch_Output5_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output6 -= uScriptCon_ManualSwitch_Output6_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output7 -= uScriptCon_ManualSwitch_Output7_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.Output8 -= uScriptCon_ManualSwitch_Output8_360;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output1 -= uScriptCon_ManualSwitch_Output1_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output2 -= uScriptCon_ManualSwitch_Output2_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output3 -= uScriptCon_ManualSwitch_Output3_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output4 -= uScriptCon_ManualSwitch_Output4_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output5 -= uScriptCon_ManualSwitch_Output5_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output6 -= uScriptCon_ManualSwitch_Output6_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output7 -= uScriptCon_ManualSwitch_Output7_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.Output8 -= uScriptCon_ManualSwitch_Output8_381;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output1 -= uScriptCon_ManualSwitch_Output1_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output2 -= uScriptCon_ManualSwitch_Output2_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output3 -= uScriptCon_ManualSwitch_Output3_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output4 -= uScriptCon_ManualSwitch_Output4_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output5 -= uScriptCon_ManualSwitch_Output5_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output6 -= uScriptCon_ManualSwitch_Output6_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output7 -= uScriptCon_ManualSwitch_Output7_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.Output8 -= uScriptCon_ManualSwitch_Output8_392;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output1 -= uScriptCon_ManualSwitch_Output1_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output2 -= uScriptCon_ManualSwitch_Output2_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output3 -= uScriptCon_ManualSwitch_Output3_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output4 -= uScriptCon_ManualSwitch_Output4_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output5 -= uScriptCon_ManualSwitch_Output5_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output6 -= uScriptCon_ManualSwitch_Output6_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output7 -= uScriptCon_ManualSwitch_Output7_396;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.Output8 -= uScriptCon_ManualSwitch_Output8_396;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Save_Out -= SubGraph_SaveLoadBool_Save_Out_431;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Load_Out -= SubGraph_SaveLoadBool_Load_Out_431;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_431;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Save_Out -= SubGraph_SaveLoadBool_Save_Out_463;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Load_Out -= SubGraph_SaveLoadBool_Load_Out_463;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_463;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_477.Out -= SubGraph_LoadObjectiveStates_Out_477;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Save_Out -= SubGraph_SaveLoadBool_Save_Out_479;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Load_Out -= SubGraph_SaveLoadBool_Load_Out_479;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_479;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output1 -= uScriptCon_ManualSwitch_Output1_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output2 -= uScriptCon_ManualSwitch_Output2_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output3 -= uScriptCon_ManualSwitch_Output3_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output4 -= uScriptCon_ManualSwitch_Output4_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output5 -= uScriptCon_ManualSwitch_Output5_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output6 -= uScriptCon_ManualSwitch_Output6_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output7 -= uScriptCon_ManualSwitch_Output7_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output8 -= uScriptCon_ManualSwitch_Output8_494;
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

	private void Instance_ResponseEvent_196(object o, uScript_MissionPromptBlock_OnResult.PromptResultEventArgs e)
	{
		event_UnityEngine_GameObject_TankBlock_196 = e.TankBlock;
		event_UnityEngine_GameObject_Accepted_196 = e.Accepted;
		Relay_ResponseEvent_196();
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

	private void SubGraph_AddMessageWithPadSupport_Out_52(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_52 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_52 = e.messageControlPadReturn;
		local_MsgPurchaseVehicle_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_52;
		local_MsgPurchaseVehicle_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_52;
		Relay_Out_52();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_52(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_52 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_52 = e.messageControlPadReturn;
		local_MsgPurchaseVehicle_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_52;
		local_MsgPurchaseVehicle_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_52;
		Relay_Shown_52();
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

	private void SubGraph_SaveLoadBool_Save_Out_92(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = e.boolean;
		local_SwitchedVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_92;
		Relay_Save_Out_92();
	}

	private void SubGraph_SaveLoadBool_Load_Out_92(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = e.boolean;
		local_SwitchedVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_92;
		Relay_Load_Out_92();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_92(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = e.boolean;
		local_SwitchedVehicle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_92;
		Relay_Restart_Out_92();
	}

	private void SubGraph_SaveLoadBool_Save_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Save_Out_93();
	}

	private void SubGraph_SaveLoadBool_Load_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Load_Out_93();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Restart_Out_93();
	}

	private void SubGraph_SaveLoadBool_Save_Out_99(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_99;
		Relay_Save_Out_99();
	}

	private void SubGraph_SaveLoadBool_Load_Out_99(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_99;
		Relay_Load_Out_99();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_99(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_99;
		Relay_Restart_Out_99();
	}

	private void SubGraph_SaveLoadBool_Save_Out_101(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = e.boolean;
		local_WaitingOnPrompt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_101;
		Relay_Save_Out_101();
	}

	private void SubGraph_SaveLoadBool_Load_Out_101(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = e.boolean;
		local_WaitingOnPrompt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_101;
		Relay_Load_Out_101();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_101(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = e.boolean;
		local_WaitingOnPrompt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_101;
		Relay_Restart_Out_101();
	}

	private void SubGraph_SaveLoadBool_Save_Out_110(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = e.boolean;
		local_TechSetUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_110;
		Relay_Save_Out_110();
	}

	private void SubGraph_SaveLoadBool_Load_Out_110(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = e.boolean;
		local_TechSetUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_110;
		Relay_Load_Out_110();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_110(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = e.boolean;
		local_TechSetUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_110;
		Relay_Restart_Out_110();
	}

	private void SubGraph_SaveLoadBool_Save_Out_123(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_123 = e.boolean;
		local_VehicleSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_123;
		Relay_Save_Out_123();
	}

	private void SubGraph_SaveLoadBool_Load_Out_123(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_123 = e.boolean;
		local_VehicleSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_123;
		Relay_Load_Out_123();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_123(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_123 = e.boolean;
		local_VehicleSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_123;
		Relay_Restart_Out_123();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_138(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_138 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_138 = e.messageControlPadReturn;
		local_MsgVehicleControls_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_138;
		local_MsgVehicleControls_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_138;
		Relay_Out_138();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_138(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_138 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_138 = e.messageControlPadReturn;
		local_MsgVehicleControls_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_138;
		local_MsgVehicleControls_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_138;
		Relay_Shown_138();
	}

	private void SubGraph_CompleteObjectiveStage_Out_143(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_143 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_143;
		Relay_Out_143();
	}

	private void SubGraph_SaveLoadBool_Save_Out_180(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = e.boolean;
		local_SaidMsgNPCVehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_180;
		Relay_Save_Out_180();
	}

	private void SubGraph_SaveLoadBool_Load_Out_180(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = e.boolean;
		local_SaidMsgNPCVehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_180;
		Relay_Load_Out_180();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_180(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = e.boolean;
		local_SaidMsgNPCVehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_180;
		Relay_Restart_Out_180();
	}

	private void SubGraph_SaveLoadBool_Save_Out_181(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = e.boolean;
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_181;
		Relay_Save_Out_181();
	}

	private void SubGraph_SaveLoadBool_Load_Out_181(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = e.boolean;
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_181;
		Relay_Load_Out_181();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_181(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = e.boolean;
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_181;
		Relay_Restart_Out_181();
	}

	private void SubGraph_CompleteObjectiveStage_Out_248(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_248 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_248;
		Relay_Out_248();
	}

	private void SubGraph_SaveLoadBool_Save_Out_278(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_278 = e.boolean;
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_278;
		Relay_Save_Out_278();
	}

	private void SubGraph_SaveLoadBool_Load_Out_278(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_278 = e.boolean;
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_278;
		Relay_Load_Out_278();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_278(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_278 = e.boolean;
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_278;
		Relay_Restart_Out_278();
	}

	private void SubGraph_SaveLoadBool_Save_Out_287(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = e.boolean;
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_287;
		Relay_Save_Out_287();
	}

	private void SubGraph_SaveLoadBool_Load_Out_287(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = e.boolean;
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_287;
		Relay_Load_Out_287();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_287(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = e.boolean;
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_287;
		Relay_Restart_Out_287();
	}

	private void SubGraph_SaveLoadBool_Save_Out_296(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = e.boolean;
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_296;
		Relay_Save_Out_296();
	}

	private void SubGraph_SaveLoadBool_Load_Out_296(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = e.boolean;
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_296;
		Relay_Load_Out_296();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_296(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = e.boolean;
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_296;
		Relay_Restart_Out_296();
	}

	private void SubGraph_SaveLoadBool_Save_Out_303(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_303;
		Relay_Save_Out_303();
	}

	private void SubGraph_SaveLoadBool_Load_Out_303(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_303;
		Relay_Load_Out_303();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_303(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_303;
		Relay_Restart_Out_303();
	}

	private void uScriptCon_ManualSwitch_Output1_359(object o, EventArgs e)
	{
		Relay_Output1_359();
	}

	private void uScriptCon_ManualSwitch_Output2_359(object o, EventArgs e)
	{
		Relay_Output2_359();
	}

	private void uScriptCon_ManualSwitch_Output3_359(object o, EventArgs e)
	{
		Relay_Output3_359();
	}

	private void uScriptCon_ManualSwitch_Output4_359(object o, EventArgs e)
	{
		Relay_Output4_359();
	}

	private void uScriptCon_ManualSwitch_Output5_359(object o, EventArgs e)
	{
		Relay_Output5_359();
	}

	private void uScriptCon_ManualSwitch_Output6_359(object o, EventArgs e)
	{
		Relay_Output6_359();
	}

	private void uScriptCon_ManualSwitch_Output7_359(object o, EventArgs e)
	{
		Relay_Output7_359();
	}

	private void uScriptCon_ManualSwitch_Output8_359(object o, EventArgs e)
	{
		Relay_Output8_359();
	}

	private void uScriptCon_ManualSwitch_Output1_360(object o, EventArgs e)
	{
		Relay_Output1_360();
	}

	private void uScriptCon_ManualSwitch_Output2_360(object o, EventArgs e)
	{
		Relay_Output2_360();
	}

	private void uScriptCon_ManualSwitch_Output3_360(object o, EventArgs e)
	{
		Relay_Output3_360();
	}

	private void uScriptCon_ManualSwitch_Output4_360(object o, EventArgs e)
	{
		Relay_Output4_360();
	}

	private void uScriptCon_ManualSwitch_Output5_360(object o, EventArgs e)
	{
		Relay_Output5_360();
	}

	private void uScriptCon_ManualSwitch_Output6_360(object o, EventArgs e)
	{
		Relay_Output6_360();
	}

	private void uScriptCon_ManualSwitch_Output7_360(object o, EventArgs e)
	{
		Relay_Output7_360();
	}

	private void uScriptCon_ManualSwitch_Output8_360(object o, EventArgs e)
	{
		Relay_Output8_360();
	}

	private void uScriptCon_ManualSwitch_Output1_381(object o, EventArgs e)
	{
		Relay_Output1_381();
	}

	private void uScriptCon_ManualSwitch_Output2_381(object o, EventArgs e)
	{
		Relay_Output2_381();
	}

	private void uScriptCon_ManualSwitch_Output3_381(object o, EventArgs e)
	{
		Relay_Output3_381();
	}

	private void uScriptCon_ManualSwitch_Output4_381(object o, EventArgs e)
	{
		Relay_Output4_381();
	}

	private void uScriptCon_ManualSwitch_Output5_381(object o, EventArgs e)
	{
		Relay_Output5_381();
	}

	private void uScriptCon_ManualSwitch_Output6_381(object o, EventArgs e)
	{
		Relay_Output6_381();
	}

	private void uScriptCon_ManualSwitch_Output7_381(object o, EventArgs e)
	{
		Relay_Output7_381();
	}

	private void uScriptCon_ManualSwitch_Output8_381(object o, EventArgs e)
	{
		Relay_Output8_381();
	}

	private void uScriptCon_ManualSwitch_Output1_392(object o, EventArgs e)
	{
		Relay_Output1_392();
	}

	private void uScriptCon_ManualSwitch_Output2_392(object o, EventArgs e)
	{
		Relay_Output2_392();
	}

	private void uScriptCon_ManualSwitch_Output3_392(object o, EventArgs e)
	{
		Relay_Output3_392();
	}

	private void uScriptCon_ManualSwitch_Output4_392(object o, EventArgs e)
	{
		Relay_Output4_392();
	}

	private void uScriptCon_ManualSwitch_Output5_392(object o, EventArgs e)
	{
		Relay_Output5_392();
	}

	private void uScriptCon_ManualSwitch_Output6_392(object o, EventArgs e)
	{
		Relay_Output6_392();
	}

	private void uScriptCon_ManualSwitch_Output7_392(object o, EventArgs e)
	{
		Relay_Output7_392();
	}

	private void uScriptCon_ManualSwitch_Output8_392(object o, EventArgs e)
	{
		Relay_Output8_392();
	}

	private void uScriptCon_ManualSwitch_Output1_396(object o, EventArgs e)
	{
		Relay_Output1_396();
	}

	private void uScriptCon_ManualSwitch_Output2_396(object o, EventArgs e)
	{
		Relay_Output2_396();
	}

	private void uScriptCon_ManualSwitch_Output3_396(object o, EventArgs e)
	{
		Relay_Output3_396();
	}

	private void uScriptCon_ManualSwitch_Output4_396(object o, EventArgs e)
	{
		Relay_Output4_396();
	}

	private void uScriptCon_ManualSwitch_Output5_396(object o, EventArgs e)
	{
		Relay_Output5_396();
	}

	private void uScriptCon_ManualSwitch_Output6_396(object o, EventArgs e)
	{
		Relay_Output6_396();
	}

	private void uScriptCon_ManualSwitch_Output7_396(object o, EventArgs e)
	{
		Relay_Output7_396();
	}

	private void uScriptCon_ManualSwitch_Output8_396(object o, EventArgs e)
	{
		Relay_Output8_396();
	}

	private void SubGraph_SaveLoadBool_Save_Out_431(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = e.boolean;
		local_BlockLimitCritical_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_431;
		Relay_Save_Out_431();
	}

	private void SubGraph_SaveLoadBool_Load_Out_431(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = e.boolean;
		local_BlockLimitCritical_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_431;
		Relay_Load_Out_431();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_431(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = e.boolean;
		local_BlockLimitCritical_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_431;
		Relay_Restart_Out_431();
	}

	private void SubGraph_SaveLoadBool_Save_Out_463(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_463 = e.boolean;
		local_msgDespawnTechsShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_463;
		Relay_Save_Out_463();
	}

	private void SubGraph_SaveLoadBool_Load_Out_463(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_463 = e.boolean;
		local_msgDespawnTechsShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_463;
		Relay_Load_Out_463();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_463(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_463 = e.boolean;
		local_msgDespawnTechsShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_463;
		Relay_Restart_Out_463();
	}

	private void SubGraph_LoadObjectiveStates_Out_477(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_477();
	}

	private void SubGraph_SaveLoadBool_Save_Out_479(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = e.boolean;
		local_Wait_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_479;
		Relay_Save_Out_479();
	}

	private void SubGraph_SaveLoadBool_Load_Out_479(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = e.boolean;
		local_Wait_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_479;
		Relay_Load_Out_479();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_479(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = e.boolean;
		local_Wait_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_479;
		Relay_Restart_Out_479();
	}

	private void uScriptCon_ManualSwitch_Output1_494(object o, EventArgs e)
	{
		Relay_Output1_494();
	}

	private void uScriptCon_ManualSwitch_Output2_494(object o, EventArgs e)
	{
		Relay_Output2_494();
	}

	private void uScriptCon_ManualSwitch_Output3_494(object o, EventArgs e)
	{
		Relay_Output3_494();
	}

	private void uScriptCon_ManualSwitch_Output4_494(object o, EventArgs e)
	{
		Relay_Output4_494();
	}

	private void uScriptCon_ManualSwitch_Output5_494(object o, EventArgs e)
	{
		Relay_Output5_494();
	}

	private void uScriptCon_ManualSwitch_Output6_494(object o, EventArgs e)
	{
		Relay_Output6_494();
	}

	private void uScriptCon_ManualSwitch_Output7_494(object o, EventArgs e)
	{
		Relay_Output7_494();
	}

	private void uScriptCon_ManualSwitch_Output8_494(object o, EventArgs e)
	{
		Relay_Output8_494();
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
			Relay_In_106();
		}
		if (flag)
		{
			Relay_In_77();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_SetEncounterTarget_owner_10 = owner_Connection_9;
		logic_uScript_SetEncounterTarget_visibleObject_10 = local_techNPC_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_10.In(logic_uScript_SetEncounterTarget_owner_10, logic_uScript_SetEncounterTarget_visibleObject_10);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_10.Out)
		{
			Relay_In_188();
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
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_14.Shown)
		{
			Relay_In_159();
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
			Relay_UnPause_352();
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
		Relay_Save_3();
	}

	private void Relay_Load_Out_24()
	{
		Relay_Load_3();
	}

	private void Relay_Restart_Out_24()
	{
		Relay_Set_False_3();
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
			Relay_In_319();
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
			Relay_True_301();
		}
		if (outOfRange)
		{
			Relay_In_308();
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
			Relay_In_84();
		}
	}

	private void Relay_Save_Out_46()
	{
		Relay_Save_92();
	}

	private void Relay_Load_Out_46()
	{
		Relay_Load_92();
	}

	private void Relay_Restart_Out_46()
	{
		Relay_Set_False_92();
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

	private void Relay_True_48()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_48.True(out logic_uScriptAct_SetBool_Target_48);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_48;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_48.Out)
		{
			Relay_In_143();
		}
	}

	private void Relay_False_48()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_48.False(out logic_uScriptAct_SetBool_Target_48);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_48;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_48.Out)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_49()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_GetTankBlock_tank_50 = local_PaymentPointTech_Tank;
		logic_uScript_GetTankBlock_blockType_50 = interactableBlockType;
		logic_uScript_GetTankBlock_Return_50 = logic_uScript_GetTankBlock_uScript_GetTankBlock_50.In(logic_uScript_GetTankBlock_tank_50, logic_uScript_GetTankBlock_blockType_50);
		local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_50;
		bool num = logic_uScript_GetTankBlock_uScript_GetTankBlock_50.Out;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_50.Returned;
		if (num)
		{
			Relay_In_68();
		}
		if (returned)
		{
			Relay_In_68();
		}
	}

	private void Relay_Out_52()
	{
		Relay_In_60();
	}

	private void Relay_Shown_52()
	{
	}

	private void Relay_In_52()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_52 = msgPurchaseVehicle;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_52 = msgPurchaseVehicle_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_52 = SpeakerNPC;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_52.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_52, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_52, logic_SubGraph_AddMessageWithPadSupport_speaker_52);
	}

	private void Relay_In_54()
	{
		logic_uScript_PointArrowAtVisible_targetObject_54 = local_TerminalBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_54.In(logic_uScript_PointArrowAtVisible_targetObject_54, logic_uScript_PointArrowAtVisible_timeToShowFor_54, logic_uScript_PointArrowAtVisible_offset_54);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_54.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_In_56()
	{
		logic_uScriptCon_CompareBool_Bool_56 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_56.In(logic_uScriptCon_CompareBool_Bool_56);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_56.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_56.False;
		if (num)
		{
			Relay_In_49();
		}
		if (flag)
		{
			Relay_In_474();
		}
	}

	private void Relay_In_60()
	{
		logic_uScriptCon_CompareBool_Bool_60 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_60.In(logic_uScriptCon_CompareBool_Bool_60);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_60.False)
		{
			Relay_In_97();
		}
	}

	private void Relay_In_61()
	{
		logic_uScript_SetEncounterTarget_owner_61 = owner_Connection_69;
		logic_uScript_SetEncounterTarget_visibleObject_61 = local_PaymentPointTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_61.In(logic_uScript_SetEncounterTarget_owner_61, logic_uScript_SetEncounterTarget_visibleObject_61);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_61.Out)
		{
			Relay_In_56();
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

	private void Relay_In_65()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_65.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_65.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_68()
	{
		logic_uScriptCon_CompareBool_Bool_68 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.In(logic_uScriptCon_CompareBool_Bool_68);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.False;
		if (num)
		{
			Relay_In_126();
		}
		if (flag)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_71()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_71 = msgTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_71.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_71, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_71);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_71.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_72()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_72 = msgTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_72.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_72, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_72);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_72.Out)
		{
			Relay_In_425();
		}
	}

	private void Relay_In_77()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_77 = owner_Connection_66;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_77.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_77);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_77.Out)
		{
			Relay_InitialSpawn_183();
		}
	}

	private void Relay_In_79()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_79.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_79.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_True_81()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_81.True(out logic_uScriptAct_SetBool_Target_81);
		local_NPCIntroMessagePlayed_System_Boolean = logic_uScriptAct_SetBool_Target_81;
	}

	private void Relay_False_81()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_81.False(out logic_uScriptAct_SetBool_Target_81);
		local_NPCIntroMessagePlayed_System_Boolean = logic_uScriptAct_SetBool_Target_81;
	}

	private void Relay_In_83()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_83 = NearNPCTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_83.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_83);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_83.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_83.OutOfRange;
		if (inRange)
		{
			Relay_In_312();
		}
		if (outOfRange)
		{
			Relay_In_145();
		}
	}

	private void Relay_In_84()
	{
		int num = 0;
		Array array = msgNPCIntro;
		if (logic_uScript_AddOnScreenMessage_locString_84.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_84, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_84, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_84 = msgTagControls;
		logic_uScript_AddOnScreenMessage_speaker_84 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_84 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_84.In(logic_uScript_AddOnScreenMessage_locString_84, logic_uScript_AddOnScreenMessage_msgPriority_84, logic_uScript_AddOnScreenMessage_holdMsg_84, logic_uScript_AddOnScreenMessage_tag_84, logic_uScript_AddOnScreenMessage_speaker_84, logic_uScript_AddOnScreenMessage_side_84);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_84.Shown)
		{
			Relay_True_81();
		}
	}

	private void Relay_In_88()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_88 = msgTagControls;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_88.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_88, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_88);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_88.Out)
		{
			Relay_In_127();
		}
	}

	private void Relay_Save_Out_92()
	{
		Relay_Save_93();
	}

	private void Relay_Load_Out_92()
	{
		Relay_Load_93();
	}

	private void Relay_Restart_Out_92()
	{
		Relay_Set_False_93();
	}

	private void Relay_Save_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Save(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Load_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Load(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Set_True_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Set_False_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_SwitchedVehicle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Save_Out_93()
	{
		Relay_Save_99();
	}

	private void Relay_Load_Out_93()
	{
		Relay_Load_99();
	}

	private void Relay_Restart_Out_93()
	{
		Relay_Set_False_99();
	}

	private void Relay_Save_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Load_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Set_True_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Set_False_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_In_95()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_95 = NearNPCTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_95.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_95);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_95.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_95.OutOfRange;
		if (inRange)
		{
			Relay_In_175();
		}
		if (outOfRange)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_97()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_97 = NearNPCTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_97.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_97);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_97.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_97.OutOfRange;
		if (inRange)
		{
			Relay_In_54();
		}
		if (outOfRange)
		{
			Relay_UnPause_144();
		}
	}

	private void Relay_Save_Out_99()
	{
		Relay_Save_101();
	}

	private void Relay_Load_Out_99()
	{
		Relay_Load_101();
	}

	private void Relay_Restart_Out_99()
	{
		Relay_Set_False_101();
	}

	private void Relay_Save_99()
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_99 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Save(ref logic_SubGraph_SaveLoadBool_boolean_99, logic_SubGraph_SaveLoadBool_boolAsVariable_99, logic_SubGraph_SaveLoadBool_uniqueID_99);
	}

	private void Relay_Load_99()
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_99 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Load(ref logic_SubGraph_SaveLoadBool_boolean_99, logic_SubGraph_SaveLoadBool_boolAsVariable_99, logic_SubGraph_SaveLoadBool_uniqueID_99);
	}

	private void Relay_Set_True_99()
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_99 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_99, logic_SubGraph_SaveLoadBool_boolAsVariable_99, logic_SubGraph_SaveLoadBool_uniqueID_99);
	}

	private void Relay_Set_False_99()
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_99 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_99, logic_SubGraph_SaveLoadBool_boolAsVariable_99, logic_SubGraph_SaveLoadBool_uniqueID_99);
	}

	private void Relay_Save_Out_101()
	{
		Relay_Save_110();
	}

	private void Relay_Load_Out_101()
	{
		Relay_Load_110();
	}

	private void Relay_Restart_Out_101()
	{
		Relay_Set_False_110();
	}

	private void Relay_Save_101()
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_101 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Save(ref logic_SubGraph_SaveLoadBool_boolean_101, logic_SubGraph_SaveLoadBool_boolAsVariable_101, logic_SubGraph_SaveLoadBool_uniqueID_101);
	}

	private void Relay_Load_101()
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_101 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Load(ref logic_SubGraph_SaveLoadBool_boolean_101, logic_SubGraph_SaveLoadBool_boolAsVariable_101, logic_SubGraph_SaveLoadBool_uniqueID_101);
	}

	private void Relay_Set_True_101()
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_101 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_101, logic_SubGraph_SaveLoadBool_boolAsVariable_101, logic_SubGraph_SaveLoadBool_uniqueID_101);
	}

	private void Relay_Set_False_101()
	{
		logic_SubGraph_SaveLoadBool_boolean_101 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_101 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_101.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_101, logic_SubGraph_SaveLoadBool_boolAsVariable_101, logic_SubGraph_SaveLoadBool_uniqueID_101);
	}

	private void Relay_In_106()
	{
		logic_uScriptCon_CompareBool_Bool_106 = local_TechSetUp_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106.In(logic_uScriptCon_CompareBool_Bool_106);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106.False;
		if (num)
		{
			Relay_In_353();
		}
		if (flag)
		{
			Relay_In_30();
		}
	}

	private void Relay_True_108()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.True(out logic_uScriptAct_SetBool_Target_108);
		local_TechSetUp_System_Boolean = logic_uScriptAct_SetBool_Target_108;
	}

	private void Relay_False_108()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.False(out logic_uScriptAct_SetBool_Target_108);
		local_TechSetUp_System_Boolean = logic_uScriptAct_SetBool_Target_108;
	}

	private void Relay_Save_Out_110()
	{
		Relay_Save_123();
	}

	private void Relay_Load_Out_110()
	{
		Relay_Load_123();
	}

	private void Relay_Restart_Out_110()
	{
		Relay_Set_False_123();
	}

	private void Relay_Save_110()
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_110 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Save(ref logic_SubGraph_SaveLoadBool_boolean_110, logic_SubGraph_SaveLoadBool_boolAsVariable_110, logic_SubGraph_SaveLoadBool_uniqueID_110);
	}

	private void Relay_Load_110()
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_110 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Load(ref logic_SubGraph_SaveLoadBool_boolean_110, logic_SubGraph_SaveLoadBool_boolAsVariable_110, logic_SubGraph_SaveLoadBool_uniqueID_110);
	}

	private void Relay_Set_True_110()
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_110 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_110, logic_SubGraph_SaveLoadBool_boolAsVariable_110, logic_SubGraph_SaveLoadBool_uniqueID_110);
	}

	private void Relay_Set_False_110()
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_110 = local_TechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_110, logic_SubGraph_SaveLoadBool_boolAsVariable_110, logic_SubGraph_SaveLoadBool_uniqueID_110);
	}

	private void Relay_In_111()
	{
		logic_uScript_IsTechPlayer_tech_111 = local_vehicleTech_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_111.In(logic_uScript_IsTechPlayer_tech_111);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_111.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_111.False;
		if (num)
		{
			Relay_In_348();
		}
		if (flag)
		{
			Relay_In_345();
		}
	}

	private void Relay_In_112()
	{
		logic_uScriptCon_CompareBool_Bool_112 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.In(logic_uScriptCon_CompareBool_Bool_112);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.False;
		if (num)
		{
			Relay_In_114();
		}
		if (flag)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_114()
	{
		logic_uScriptCon_CompareBool_Bool_114 = local_VehicleSetup_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.In(logic_uScriptCon_CompareBool_Bool_114);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.False;
		if (num)
		{
			Relay_In_37();
		}
		if (flag)
		{
			Relay_In_119();
		}
	}

	private void Relay_True_116()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_116.True(out logic_uScriptAct_SetBool_Target_116);
		local_VehicleSetup_System_Boolean = logic_uScriptAct_SetBool_Target_116;
	}

	private void Relay_False_116()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_116.False(out logic_uScriptAct_SetBool_Target_116);
		local_VehicleSetup_System_Boolean = logic_uScriptAct_SetBool_Target_116;
	}

	private void Relay_In_119()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_119.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_119, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_119, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_119 = owner_Connection_117;
		int num2 = 0;
		Array array2 = local_vehicleTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_119.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_119, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_119, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_119 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_119.In(logic_uScript_GetAndCheckTechs_techData_119, logic_uScript_GetAndCheckTechs_ownerNode_119, ref logic_uScript_GetAndCheckTechs_techs_119);
		local_vehicleTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_119;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_119.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_119.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_120();
		}
		if (someAlive)
		{
			Relay_AtIndex_120();
		}
	}

	private void Relay_AtIndex_120()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_120.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_120, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_120, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_120.AtIndex(ref logic_uScript_AccessListTech_techList_120, logic_uScript_AccessListTech_index_120, out logic_uScript_AccessListTech_value_120);
		local_vehicleTechs_TankArray = logic_uScript_AccessListTech_techList_120;
		local_vehicleTech_Tank = logic_uScript_AccessListTech_value_120;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_120.Out)
		{
			Relay_SetInvulnerable_151();
		}
	}

	private void Relay_Save_Out_123()
	{
		Relay_Save_180();
	}

	private void Relay_Load_Out_123()
	{
		Relay_Load_180();
	}

	private void Relay_Restart_Out_123()
	{
		Relay_Set_False_180();
	}

	private void Relay_Save_123()
	{
		logic_SubGraph_SaveLoadBool_boolean_123 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_123 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Save(ref logic_SubGraph_SaveLoadBool_boolean_123, logic_SubGraph_SaveLoadBool_boolAsVariable_123, logic_SubGraph_SaveLoadBool_uniqueID_123);
	}

	private void Relay_Load_123()
	{
		logic_SubGraph_SaveLoadBool_boolean_123 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_123 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Load(ref logic_SubGraph_SaveLoadBool_boolean_123, logic_SubGraph_SaveLoadBool_boolAsVariable_123, logic_SubGraph_SaveLoadBool_uniqueID_123);
	}

	private void Relay_Set_True_123()
	{
		logic_SubGraph_SaveLoadBool_boolean_123 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_123 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_123, logic_SubGraph_SaveLoadBool_boolAsVariable_123, logic_SubGraph_SaveLoadBool_uniqueID_123);
	}

	private void Relay_Set_False_123()
	{
		logic_SubGraph_SaveLoadBool_boolean_123 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_123 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_123, logic_SubGraph_SaveLoadBool_boolAsVariable_123, logic_SubGraph_SaveLoadBool_uniqueID_123);
	}

	private void Relay_In_126()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_126.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_126, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_126, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_126 = owner_Connection_125;
		int num2 = 0;
		Array array2 = local_vehicleTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_126.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_126, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_126, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_126 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_126.In(logic_uScript_GetAndCheckTechs_techData_126, logic_uScript_GetAndCheckTechs_ownerNode_126, ref logic_uScript_GetAndCheckTechs_techs_126);
		local_vehicleTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_126;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_126.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_126.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_309();
		}
		if (someAlive)
		{
			Relay_AtIndex_309();
		}
	}

	private void Relay_In_127()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_127 = msgTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_127.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_127, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_127);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_127.Out)
		{
			Relay_In_297();
		}
	}

	private void Relay_In_133()
	{
		logic_uScript_ClearEncounterTarget_owner_133 = owner_Connection_132;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_133.In(logic_uScript_ClearEncounterTarget_owner_133);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_133.Out)
		{
			Relay_In_257();
		}
	}

	private void Relay_In_135()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_135 = msgTagSwitchTech;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_135.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_135, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_135);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_135.Out)
		{
			Relay_In_133();
		}
	}

	private void Relay_Out_138()
	{
	}

	private void Relay_Shown_138()
	{
	}

	private void Relay_In_138()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_138 = msgVehicleControls;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_138 = msgVehicleControls_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_138 = SpeakerNPC;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_138.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_138, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_138, logic_SubGraph_AddMessageWithPadSupport_speaker_138);
	}

	private void Relay_Out_143()
	{
		Relay_In_443();
	}

	private void Relay_In_143()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_143 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_143.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_143, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_143);
	}

	private void Relay_Pause_144()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_144.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_144.Out)
		{
			Relay_In_268();
		}
	}

	private void Relay_UnPause_144()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_144.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_144.Out)
		{
			Relay_In_268();
		}
	}

	private void Relay_In_145()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_145.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_145.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_147()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_147 = NearNPCTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_147.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_147);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_147.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_147.OutOfRange;
		if (inRange)
		{
			Relay_In_166();
		}
		if (outOfRange)
		{
			Relay_In_284();
		}
	}

	private void Relay_In_149()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_149 = msgTagControls;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_149.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_149, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_149);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_149.Out)
		{
			Relay_True_48();
		}
	}

	private void Relay_SetInvulnerable_151()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_151.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_151, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_151, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_151.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_151);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_151.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_SetVulnerable_151()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_151.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_151, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_151, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_151.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_151);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_151.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_153()
	{
		logic_uScript_LockTechSendToSCU_tech_153 = local_vehicleTech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_153.In(logic_uScript_LockTechSendToSCU_tech_153, logic_uScript_LockTechSendToSCU_lockSendToSCU_153);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_153.Out)
		{
			Relay_True_116();
		}
	}

	private void Relay_In_158()
	{
		logic_uScript_SetEncounterTarget_owner_158 = owner_Connection_157;
		logic_uScript_SetEncounterTarget_visibleObject_158 = local_vehicleTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_158.In(logic_uScript_SetEncounterTarget_owner_158, logic_uScript_SetEncounterTarget_visibleObject_158);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_158.Out)
		{
			Relay_In_147();
		}
	}

	private void Relay_In_159()
	{
		logic_uScript_FlyTechUpAndAway_tech_159 = local_techNPC_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_159 = NPCFlyAwayBehavior;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_159.In(logic_uScript_FlyTechUpAndAway_tech_159, logic_uScript_FlyTechUpAndAway_maxLifetime_159, logic_uScript_FlyTechUpAndAway_targetHeight_159, logic_uScript_FlyTechUpAndAway_aiTree_159, logic_uScript_FlyTechUpAndAway_removalParticles_159);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_159.Out)
		{
			Relay_In_266();
		}
	}

	private void Relay_In_163()
	{
		int num = 0;
		Array array = msgNPCVehiclePurchased;
		if (logic_uScript_AddOnScreenMessage_locString_163.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_163, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_163, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_163 = msgTagSwitchTech;
		logic_uScript_AddOnScreenMessage_speaker_163 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_163 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_163.In(logic_uScript_AddOnScreenMessage_locString_163, logic_uScript_AddOnScreenMessage_msgPriority_163, logic_uScript_AddOnScreenMessage_holdMsg_163, logic_uScript_AddOnScreenMessage_tag_163, logic_uScript_AddOnScreenMessage_speaker_163, logic_uScript_AddOnScreenMessage_side_163);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_163.Shown)
		{
			Relay_True_168();
		}
	}

	private void Relay_In_166()
	{
		logic_uScriptCon_CompareBool_Bool_166 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_166.In(logic_uScriptCon_CompareBool_Bool_166);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_166.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_166.False;
		if (num)
		{
			Relay_In_64();
		}
		if (flag)
		{
			Relay_In_163();
		}
	}

	private void Relay_True_168()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_168.True(out logic_uScriptAct_SetBool_Target_168);
		local_SaidMsgNPCVehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_168;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_168.Out)
		{
			Relay_In_64();
		}
	}

	private void Relay_False_168()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_168.False(out logic_uScriptAct_SetBool_Target_168);
		local_SaidMsgNPCVehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_168;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_168.Out)
		{
			Relay_In_64();
		}
	}

	private void Relay_True_170()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_170.True(out logic_uScriptAct_SetBool_Target_170);
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_uScriptAct_SetBool_Target_170;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_170.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_False_170()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_170.False(out logic_uScriptAct_SetBool_Target_170);
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_uScriptAct_SetBool_Target_170;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_170.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_174()
	{
		int num = 0;
		Array array = msgNPCVehicleSwitched;
		if (logic_uScript_AddOnScreenMessage_locString_174.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_174, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_174, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_174 = msgTagControls;
		logic_uScript_AddOnScreenMessage_speaker_174 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_174 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_174.In(logic_uScript_AddOnScreenMessage_locString_174, logic_uScript_AddOnScreenMessage_msgPriority_174, logic_uScript_AddOnScreenMessage_holdMsg_174, logic_uScript_AddOnScreenMessage_tag_174, logic_uScript_AddOnScreenMessage_speaker_174, logic_uScript_AddOnScreenMessage_side_174);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_174.Shown)
		{
			Relay_True_170();
		}
	}

	private void Relay_In_175()
	{
		logic_uScriptCon_CompareBool_Bool_175 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175.In(logic_uScriptCon_CompareBool_Bool_175);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175.False;
		if (num)
		{
			Relay_In_138();
		}
		if (flag)
		{
			Relay_In_174();
		}
	}

	private void Relay_Save_Out_180()
	{
		Relay_Save_181();
	}

	private void Relay_Load_Out_180()
	{
		Relay_Load_181();
	}

	private void Relay_Restart_Out_180()
	{
		Relay_Set_False_181();
	}

	private void Relay_Save_180()
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_180 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Save(ref logic_SubGraph_SaveLoadBool_boolean_180, logic_SubGraph_SaveLoadBool_boolAsVariable_180, logic_SubGraph_SaveLoadBool_uniqueID_180);
	}

	private void Relay_Load_180()
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_180 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Load(ref logic_SubGraph_SaveLoadBool_boolean_180, logic_SubGraph_SaveLoadBool_boolAsVariable_180, logic_SubGraph_SaveLoadBool_uniqueID_180);
	}

	private void Relay_Set_True_180()
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_180 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_180, logic_SubGraph_SaveLoadBool_boolAsVariable_180, logic_SubGraph_SaveLoadBool_uniqueID_180);
	}

	private void Relay_Set_False_180()
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_180 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_180, logic_SubGraph_SaveLoadBool_boolAsVariable_180, logic_SubGraph_SaveLoadBool_uniqueID_180);
	}

	private void Relay_Save_Out_181()
	{
		Relay_Save_278();
	}

	private void Relay_Load_Out_181()
	{
		Relay_Load_278();
	}

	private void Relay_Restart_Out_181()
	{
		Relay_Set_False_278();
	}

	private void Relay_Save_181()
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_181 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Save(ref logic_SubGraph_SaveLoadBool_boolean_181, logic_SubGraph_SaveLoadBool_boolAsVariable_181, logic_SubGraph_SaveLoadBool_uniqueID_181);
	}

	private void Relay_Load_181()
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_181 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Load(ref logic_SubGraph_SaveLoadBool_boolean_181, logic_SubGraph_SaveLoadBool_boolAsVariable_181, logic_SubGraph_SaveLoadBool_uniqueID_181);
	}

	private void Relay_Set_True_181()
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_181 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_181, logic_SubGraph_SaveLoadBool_boolAsVariable_181, logic_SubGraph_SaveLoadBool_uniqueID_181);
	}

	private void Relay_Set_False_181()
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_181 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_181, logic_SubGraph_SaveLoadBool_boolAsVariable_181, logic_SubGraph_SaveLoadBool_uniqueID_181);
	}

	private void Relay_InitialSpawn_183()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_SpawnTechsFromData_spawnData_183.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_183, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_SpawnTechsFromData_spawnData_183, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_183 = owner_Connection_184;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_183.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_183, logic_uScript_SpawnTechsFromData_ownerNode_183, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_183, logic_uScript_SpawnTechsFromData_allowResurrection_183);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_183.Out)
		{
			Relay_InitialSpawn_18();
		}
	}

	private void Relay_In_185()
	{
		logic_uScript_SetTankInvulnerable_tank_185 = local_PaymentPointTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185.In(logic_uScript_SetTankInvulnerable_invulnerable_185, logic_uScript_SetTankInvulnerable_tank_185);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185.Out)
		{
			Relay_True_108();
		}
	}

	private void Relay_In_188()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_188.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_188, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_188, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_188 = owner_Connection_191;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_188.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_188, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_188, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_188 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_188.In(logic_uScript_GetAndCheckTechs_techData_188, logic_uScript_GetAndCheckTechs_ownerNode_188, ref logic_uScript_GetAndCheckTechs_techs_188);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_188;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_188.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_188.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_189();
		}
		if (someAlive)
		{
			Relay_AtIndex_189();
		}
	}

	private void Relay_AtIndex_189()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_189.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_189, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_189, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_189.AtIndex(ref logic_uScript_AccessListTech_techList_189, logic_uScript_AccessListTech_index_189, out logic_uScript_AccessListTech_value_189);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_189;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_189;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_189.Out)
		{
			Relay_In_185();
		}
	}

	private void Relay_In_194()
	{
		logic_uScriptAct_MultiplyInt_v2_A_194 = vehicleCost;
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_194.In(logic_uScriptAct_MultiplyInt_v2_A_194, logic_uScriptAct_MultiplyInt_v2_B_194, out logic_uScriptAct_MultiplyInt_v2_IntResult_194, out logic_uScriptAct_MultiplyInt_v2_FloatResult_194);
		local_193_System_Int32 = logic_uScriptAct_MultiplyInt_v2_IntResult_194;
		if (logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_194.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_ResponseEvent_196()
	{
		local_238_TankBlock = event_UnityEngine_GameObject_TankBlock_196;
		local_232_System_Boolean = event_UnityEngine_GameObject_Accepted_196;
		Relay_In_214();
	}

	private void Relay_In_197()
	{
		logic_uScriptCon_CompareBool_Bool_197 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.In(logic_uScriptCon_CompareBool_Bool_197);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.False;
		if (num)
		{
			Relay_In_229();
		}
		if (flag)
		{
			Relay_In_229();
		}
	}

	private void Relay_True_199()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_199.True(out logic_uScriptAct_SetBool_Target_199);
		local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_199;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_199.Out)
		{
			Relay_True_467();
		}
	}

	private void Relay_False_199()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_199.False(out logic_uScriptAct_SetBool_Target_199);
		local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_199;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_199.Out)
		{
			Relay_True_467();
		}
	}

	private void Relay_In_203()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_203.In();
	}

	private void Relay_In_206()
	{
		logic_uScriptCon_CompareBool_Bool_206 = local_232_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206.In(logic_uScriptCon_CompareBool_Bool_206);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206.False;
		if (num)
		{
			Relay_In_452();
		}
		if (flag)
		{
			Relay_In_208();
		}
	}

	private void Relay_In_207()
	{
		logic_uScriptCon_CompareInt_A_207 = local_CurrentMoney_System_Int32;
		logic_uScriptCon_CompareInt_B_207 = vehicleCost;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_207.In(logic_uScriptCon_CompareInt_A_207, logic_uScriptCon_CompareInt_B_207);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_207.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_207.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_197();
		}
		if (lessThan)
		{
			Relay_In_211();
		}
	}

	private void Relay_In_208()
	{
		logic_uScriptCon_CompareBool_Bool_208 = local_msg03aShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.In(logic_uScriptCon_CompareBool_Bool_208);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.False;
		if (num)
		{
			Relay_In_203();
		}
		if (flag)
		{
			Relay_In_235();
		}
	}

	private void Relay_In_209()
	{
		logic_uScript_GetCurrentMoneyEarned_Return_209 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_209.In();
		local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_209;
		if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_209.Out)
		{
			Relay_In_207();
		}
	}

	private void Relay_In_211()
	{
		logic_uScriptCon_CompareBool_Bool_211 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.In(logic_uScriptCon_CompareBool_Bool_211);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.False;
		if (num)
		{
			Relay_In_226();
		}
		if (flag)
		{
			Relay_In_226();
		}
	}

	private void Relay_In_212()
	{
		logic_uScriptCon_CompareBool_Bool_212 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.In(logic_uScriptCon_CompareBool_Bool_212);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.False;
		if (num)
		{
			Relay_In_194();
		}
		if (flag)
		{
			Relay_In_236();
		}
	}

	private void Relay_In_214()
	{
		logic_uScript_CompareBlock_A_214 = local_238_TankBlock;
		logic_uScript_CompareBlock_B_214 = local_TerminalBlock_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_214.In(logic_uScript_CompareBlock_A_214, logic_uScript_CompareBlock_B_214);
		if (logic_uScript_CompareBlock_uScript_CompareBlock_214.EqualTo)
		{
			Relay_In_206();
		}
	}

	private void Relay_In_215()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_215.In();
	}

	private void Relay_In_219()
	{
		logic_uScriptCon_CompareBool_Bool_219 = _DEBUGIgnoreMoneyCheck;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219.In(logic_uScriptCon_CompareBool_Bool_219);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219.False;
		if (num)
		{
			Relay_In_220();
		}
		if (flag)
		{
			Relay_In_209();
		}
	}

	private void Relay_In_220()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_220.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_220.Out)
		{
			Relay_In_197();
		}
	}

	private void Relay_In_221()
	{
		logic_uScript_AddMessage_messageData_221 = msgNPCNotEnoughMoney;
		logic_uScript_AddMessage_speaker_221 = SpeakerNPC;
		logic_uScript_AddMessage_Return_221 = logic_uScript_AddMessage_uScript_AddMessage_221.In(logic_uScript_AddMessage_messageData_221, logic_uScript_AddMessage_speaker_221);
		if (logic_uScript_AddMessage_uScript_AddMessage_221.Out)
		{
			Relay_True_199();
		}
	}

	private void Relay_In_226()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_226 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_226 = msgPromptNoMoney;
		logic_uScript_MissionPromptBlock_Show_targetBlock_226 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_226.In(logic_uScript_MissionPromptBlock_Show_bodyText_226, logic_uScript_MissionPromptBlock_Show_acceptButtonText_226, logic_uScript_MissionPromptBlock_Show_rejectButtonText_226, logic_uScript_MissionPromptBlock_Show_targetBlock_226, logic_uScript_MissionPromptBlock_Show_highlightBlock_226, logic_uScript_MissionPromptBlock_Show_singleUse_226);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_226.Out)
		{
			Relay_False_231();
		}
	}

	private void Relay_True_227()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_227.True(out logic_uScriptAct_SetBool_Target_227);
		local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_227;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_227.Out)
		{
			Relay_True_469();
		}
	}

	private void Relay_False_227()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_227.False(out logic_uScriptAct_SetBool_Target_227);
		local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_227;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_227.Out)
		{
			Relay_True_469();
		}
	}

	private void Relay_In_228()
	{
		logic_uScript_AddMoney_amount_228 = local_193_System_Int32;
		logic_uScript_AddMoney_uScript_AddMoney_228.In(logic_uScript_AddMoney_amount_228);
		if (logic_uScript_AddMoney_uScript_AddMoney_228.Out)
		{
			Relay_InitialSpawn_247();
		}
	}

	private void Relay_In_229()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_229 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_229 = msgPromptAccept;
		logic_uScript_MissionPromptBlock_Show_rejectButtonText_229 = msgPromptDecline;
		logic_uScript_MissionPromptBlock_Show_targetBlock_229 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_229.In(logic_uScript_MissionPromptBlock_Show_bodyText_229, logic_uScript_MissionPromptBlock_Show_acceptButtonText_229, logic_uScript_MissionPromptBlock_Show_rejectButtonText_229, logic_uScript_MissionPromptBlock_Show_targetBlock_229, logic_uScript_MissionPromptBlock_Show_highlightBlock_229, logic_uScript_MissionPromptBlock_Show_singleUse_229);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_229.Out)
		{
			Relay_True_231();
		}
	}

	private void Relay_True_231()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_231.True(out logic_uScriptAct_SetBool_Target_231);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_231;
	}

	private void Relay_False_231()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_231.False(out logic_uScriptAct_SetBool_Target_231);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_231;
	}

	private void Relay_In_235()
	{
		logic_uScript_AddMessage_messageData_235 = msgNPCPurchaseDeclined;
		logic_uScript_AddMessage_speaker_235 = SpeakerNPC;
		logic_uScript_AddMessage_Return_235 = logic_uScript_AddMessage_uScript_AddMessage_235.In(logic_uScript_AddMessage_messageData_235, logic_uScript_AddMessage_speaker_235);
		if (logic_uScript_AddMessage_uScript_AddMessage_235.Out)
		{
			Relay_True_227();
		}
	}

	private void Relay_In_236()
	{
		logic_uScriptCon_CompareBool_Bool_236 = local_msg03bShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_236.In(logic_uScriptCon_CompareBool_Bool_236);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_236.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_236.False;
		if (num)
		{
			Relay_In_215();
		}
		if (flag)
		{
			Relay_In_221();
		}
	}

	private void Relay_True_241()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_241.True(out logic_uScriptAct_SetBool_Target_241);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_241;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_241.Out)
		{
			Relay_In_248();
		}
	}

	private void Relay_False_241()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_241.False(out logic_uScriptAct_SetBool_Target_241);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_241;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_241.Out)
		{
			Relay_In_248();
		}
	}

	private void Relay_In_243()
	{
		int num = 0;
		Array array = discoverableBlockTypesOnVehicle;
		if (logic_uScript_DiscoverBlocks_blockTypes_243.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DiscoverBlocks_blockTypes_243, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DiscoverBlocks_blockTypes_243, num, array.Length);
		num += array.Length;
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_243.In(logic_uScript_DiscoverBlocks_blockTypes_243);
		if (logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_243.Out)
		{
			Relay_True_241();
		}
	}

	private void Relay_InitialSpawn_247()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_247.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_247, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_247, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_247 = owner_Connection_249;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_247.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_247, logic_uScript_SpawnTechsFromData_ownerNode_247, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_247, logic_uScript_SpawnTechsFromData_allowResurrection_247);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_247.Out)
		{
			Relay_In_328();
		}
	}

	private void Relay_Out_248()
	{
		Relay_In_449();
	}

	private void Relay_In_248()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_248 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_248.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_248, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_248);
	}

	private void Relay_In_253()
	{
		logic_uScript_HideArrow_uScript_HideArrow_253.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_253.Out)
		{
			Relay_In_261();
		}
	}

	private void Relay_In_254()
	{
		logic_uScript_PointArrowAtVisible_targetObject_254 = local_vehicleTech_Tank;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_254.In(logic_uScript_PointArrowAtVisible_targetObject_254, logic_uScript_PointArrowAtVisible_timeToShowFor_254, logic_uScript_PointArrowAtVisible_offset_254);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_254.Out)
		{
			Relay_In_255();
		}
	}

	private void Relay_In_255()
	{
		logic_uScript_EnableGlow_targetObject_255 = local_vehicleTech_Tank;
		logic_uScript_EnableGlow_uScript_EnableGlow_255.In(logic_uScript_EnableGlow_targetObject_255, logic_uScript_EnableGlow_enable_255);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_255.Out)
		{
			Relay_In_350();
		}
	}

	private void Relay_In_257()
	{
		logic_uScript_HideArrow_uScript_HideArrow_257.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_257.Out)
		{
			Relay_In_258();
		}
	}

	private void Relay_In_258()
	{
		logic_uScript_EnableGlow_targetObject_258 = local_vehicleTech_Tank;
		logic_uScript_EnableGlow_uScript_EnableGlow_258.In(logic_uScript_EnableGlow_targetObject_258, logic_uScript_EnableGlow_enable_258);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_258.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_261()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_261 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_261.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_261);
		if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_261.Out)
		{
			Relay_In_243();
		}
	}

	private void Relay_In_263()
	{
		logic_uScript_ClearEncounterTarget_owner_263 = owner_Connection_264;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_263.In(logic_uScript_ClearEncounterTarget_owner_263);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_263.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_266()
	{
		logic_uScript_FlyTechUpAndAway_tech_266 = local_PaymentPointTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_266 = NPCFlyAwayBehavior;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_266.In(logic_uScript_FlyTechUpAndAway_tech_266, logic_uScript_FlyTechUpAndAway_maxLifetime_266, logic_uScript_FlyTechUpAndAway_targetHeight_266, logic_uScript_FlyTechUpAndAway_aiTree_266, logic_uScript_FlyTechUpAndAway_removalParticles_266);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_266.Out)
		{
			Relay_SetVulnerable_486();
		}
	}

	private void Relay_In_268()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_268 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_268.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_268);
		if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_268.Out)
		{
			Relay_In_464();
		}
	}

	private void Relay_In_271()
	{
		logic_uScriptCon_CompareBool_Bool_271 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.In(logic_uScriptCon_CompareBool_Bool_271);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.False;
		if (num)
		{
			Relay_In_71();
		}
		if (flag)
		{
			Relay_In_272();
		}
	}

	private void Relay_In_272()
	{
		int num = 0;
		Array array = msgLeavingEarlyPrePurchase;
		if (logic_uScript_AddOnScreenMessage_locString_272.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_272, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_272, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_272 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_272 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_272.In(logic_uScript_AddOnScreenMessage_locString_272, logic_uScript_AddOnScreenMessage_msgPriority_272, logic_uScript_AddOnScreenMessage_holdMsg_272, logic_uScript_AddOnScreenMessage_tag_272, logic_uScript_AddOnScreenMessage_speaker_272, logic_uScript_AddOnScreenMessage_side_272);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_272.Out)
		{
			Relay_True_275();
		}
	}

	private void Relay_True_275()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_275.True(out logic_uScriptAct_SetBool_Target_275);
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_uScriptAct_SetBool_Target_275;
	}

	private void Relay_False_275()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_275.False(out logic_uScriptAct_SetBool_Target_275);
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_uScriptAct_SetBool_Target_275;
	}

	private void Relay_Save_Out_278()
	{
		Relay_Save_287();
	}

	private void Relay_Load_Out_278()
	{
		Relay_Load_287();
	}

	private void Relay_Restart_Out_278()
	{
		Relay_Set_False_287();
	}

	private void Relay_Save_278()
	{
		logic_SubGraph_SaveLoadBool_boolean_278 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_278 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Save(ref logic_SubGraph_SaveLoadBool_boolean_278, logic_SubGraph_SaveLoadBool_boolAsVariable_278, logic_SubGraph_SaveLoadBool_uniqueID_278);
	}

	private void Relay_Load_278()
	{
		logic_SubGraph_SaveLoadBool_boolean_278 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_278 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Load(ref logic_SubGraph_SaveLoadBool_boolean_278, logic_SubGraph_SaveLoadBool_boolAsVariable_278, logic_SubGraph_SaveLoadBool_uniqueID_278);
	}

	private void Relay_Set_True_278()
	{
		logic_SubGraph_SaveLoadBool_boolean_278 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_278 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_278, logic_SubGraph_SaveLoadBool_boolAsVariable_278, logic_SubGraph_SaveLoadBool_uniqueID_278);
	}

	private void Relay_Set_False_278()
	{
		logic_SubGraph_SaveLoadBool_boolean_278 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_278 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_278.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_278, logic_SubGraph_SaveLoadBool_boolAsVariable_278, logic_SubGraph_SaveLoadBool_uniqueID_278);
	}

	private void Relay_True_281()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_281.True(out logic_uScriptAct_SetBool_Target_281);
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_uScriptAct_SetBool_Target_281;
	}

	private void Relay_False_281()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_281.False(out logic_uScriptAct_SetBool_Target_281);
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_uScriptAct_SetBool_Target_281;
	}

	private void Relay_In_282()
	{
		int num = 0;
		Array array = msgLeavingEarlyPostPurchase;
		if (logic_uScript_AddOnScreenMessage_locString_282.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_282, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_282, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_282 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_282 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_282.In(logic_uScript_AddOnScreenMessage_locString_282, logic_uScript_AddOnScreenMessage_msgPriority_282, logic_uScript_AddOnScreenMessage_holdMsg_282, logic_uScript_AddOnScreenMessage_tag_282, logic_uScript_AddOnScreenMessage_speaker_282, logic_uScript_AddOnScreenMessage_side_282);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_282.Out)
		{
			Relay_True_281();
		}
	}

	private void Relay_In_284()
	{
		logic_uScriptCon_CompareBool_Bool_284 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_284.In(logic_uScriptCon_CompareBool_Bool_284);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_284.False)
		{
			Relay_In_282();
		}
	}

	private void Relay_Save_Out_287()
	{
		Relay_Save_296();
	}

	private void Relay_Load_Out_287()
	{
		Relay_Load_296();
	}

	private void Relay_Restart_Out_287()
	{
		Relay_Set_False_296();
	}

	private void Relay_Save_287()
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_287 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Save(ref logic_SubGraph_SaveLoadBool_boolean_287, logic_SubGraph_SaveLoadBool_boolAsVariable_287, logic_SubGraph_SaveLoadBool_uniqueID_287);
	}

	private void Relay_Load_287()
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_287 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Load(ref logic_SubGraph_SaveLoadBool_boolean_287, logic_SubGraph_SaveLoadBool_boolAsVariable_287, logic_SubGraph_SaveLoadBool_uniqueID_287);
	}

	private void Relay_Set_True_287()
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_287 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_287, logic_SubGraph_SaveLoadBool_boolAsVariable_287, logic_SubGraph_SaveLoadBool_uniqueID_287);
	}

	private void Relay_Set_False_287()
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_287 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_287, logic_SubGraph_SaveLoadBool_boolAsVariable_287, logic_SubGraph_SaveLoadBool_uniqueID_287);
	}

	private void Relay_In_288()
	{
		int num = 0;
		Array array = msgLeavingEarlyDuringIntro;
		if (logic_uScript_AddOnScreenMessage_locString_288.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_288, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_288, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_288 = NPCSpeaker;
		logic_uScript_AddOnScreenMessage_Return_288 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_288.In(logic_uScript_AddOnScreenMessage_locString_288, logic_uScript_AddOnScreenMessage_msgPriority_288, logic_uScript_AddOnScreenMessage_holdMsg_288, logic_uScript_AddOnScreenMessage_tag_288, logic_uScript_AddOnScreenMessage_speaker_288, logic_uScript_AddOnScreenMessage_side_288);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_288.Out)
		{
			Relay_True_293();
		}
	}

	private void Relay_In_292()
	{
		logic_uScriptCon_CompareBool_Bool_292 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_292.In(logic_uScriptCon_CompareBool_Bool_292);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_292.False)
		{
			Relay_In_288();
		}
	}

	private void Relay_True_293()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.True(out logic_uScriptAct_SetBool_Target_293);
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_uScriptAct_SetBool_Target_293;
	}

	private void Relay_False_293()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.False(out logic_uScriptAct_SetBool_Target_293);
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_uScriptAct_SetBool_Target_293;
	}

	private void Relay_Save_Out_296()
	{
		Relay_Save_303();
	}

	private void Relay_Load_Out_296()
	{
		Relay_Load_303();
	}

	private void Relay_Restart_Out_296()
	{
		Relay_Set_False_303();
	}

	private void Relay_Save_296()
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_296 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Save(ref logic_SubGraph_SaveLoadBool_boolean_296, logic_SubGraph_SaveLoadBool_boolAsVariable_296, logic_SubGraph_SaveLoadBool_uniqueID_296);
	}

	private void Relay_Load_296()
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_296 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Load(ref logic_SubGraph_SaveLoadBool_boolean_296, logic_SubGraph_SaveLoadBool_boolAsVariable_296, logic_SubGraph_SaveLoadBool_uniqueID_296);
	}

	private void Relay_Set_True_296()
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_296 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_296, logic_SubGraph_SaveLoadBool_boolAsVariable_296, logic_SubGraph_SaveLoadBool_uniqueID_296);
	}

	private void Relay_Set_False_296()
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_296 = local_ShownMsgLeavingEarlyDuringIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_296, logic_SubGraph_SaveLoadBool_boolAsVariable_296, logic_SubGraph_SaveLoadBool_uniqueID_296);
	}

	private void Relay_In_297()
	{
		logic_uScriptCon_CompareBool_Bool_297 = local_NPCSeen_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_297.In(logic_uScriptCon_CompareBool_Bool_297);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_297.True)
		{
			Relay_In_292();
		}
	}

	private void Relay_True_301()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_301.True(out logic_uScriptAct_SetBool_Target_301);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_301;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_301.Out)
		{
			Relay_False_305();
		}
	}

	private void Relay_False_301()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_301.False(out logic_uScriptAct_SetBool_Target_301);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_301;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_301.Out)
		{
			Relay_False_305();
		}
	}

	private void Relay_Save_Out_303()
	{
		Relay_Save_431();
	}

	private void Relay_Load_Out_303()
	{
		Relay_Load_431();
	}

	private void Relay_Restart_Out_303()
	{
		Relay_Set_False_431();
	}

	private void Relay_Save_303()
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_303 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Save(ref logic_SubGraph_SaveLoadBool_boolean_303, logic_SubGraph_SaveLoadBool_boolAsVariable_303, logic_SubGraph_SaveLoadBool_uniqueID_303);
	}

	private void Relay_Load_303()
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_303 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Load(ref logic_SubGraph_SaveLoadBool_boolean_303, logic_SubGraph_SaveLoadBool_boolAsVariable_303, logic_SubGraph_SaveLoadBool_uniqueID_303);
	}

	private void Relay_Set_True_303()
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_303 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_303, logic_SubGraph_SaveLoadBool_boolAsVariable_303, logic_SubGraph_SaveLoadBool_uniqueID_303);
	}

	private void Relay_Set_False_303()
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_303 = local_NPCSeen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_303, logic_SubGraph_SaveLoadBool_boolAsVariable_303, logic_SubGraph_SaveLoadBool_uniqueID_303);
	}

	private void Relay_True_305()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_305.True(out logic_uScriptAct_SetBool_Target_305);
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_uScriptAct_SetBool_Target_305;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_305.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_False_305()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_305.False(out logic_uScriptAct_SetBool_Target_305);
		local_ShownMsgLeavingEarlyDuringIntro_System_Boolean = logic_uScriptAct_SetBool_Target_305;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_305.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_306()
	{
		logic_uScript_LockTechSendToSCU_tech_306 = local_vehicleTech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_306.In(logic_uScript_LockTechSendToSCU_tech_306, logic_uScript_LockTechSendToSCU_lockSendToSCU_306);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_306.Out)
		{
			Relay_In_254();
		}
	}

	private void Relay_In_308()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_308.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_308.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_AtIndex_309()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_309.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_309, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_309, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_309.AtIndex(ref logic_uScript_AccessListTech_techList_309, logic_uScript_AccessListTech_index_309, out logic_uScript_AccessListTech_value_309);
		local_vehicleTechs_TankArray = logic_uScript_AccessListTech_techList_309;
		local_vehicleTech_Tank = logic_uScript_AccessListTech_value_309;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_309.Out)
		{
			Relay_In_338();
		}
	}

	private void Relay_In_312()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_312.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_312, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_312, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_312 = owner_Connection_315;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_312.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_312, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_312, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_312 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_312.In(logic_uScript_GetAndCheckTechs_techData_312, logic_uScript_GetAndCheckTechs_ownerNode_312, ref logic_uScript_GetAndCheckTechs_techs_312);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_312;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_312.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_312.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_314();
		}
		if (someAlive)
		{
			Relay_AtIndex_314();
		}
	}

	private void Relay_AtIndex_314()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_314.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_314, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_314, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_314.AtIndex(ref logic_uScript_AccessListTech_techList_314, logic_uScript_AccessListTech_index_314, out logic_uScript_AccessListTech_value_314);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_314;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_314;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_314.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_319()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_319.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_319, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_319, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_319 = owner_Connection_317;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_319.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_319, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_319, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_319 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_319.In(logic_uScript_GetAndCheckTechs_techData_319, logic_uScript_GetAndCheckTechs_ownerNode_319, ref logic_uScript_GetAndCheckTechs_techs_319);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_319;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_319.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_319.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_320();
		}
		if (someAlive)
		{
			Relay_AtIndex_320();
		}
	}

	private void Relay_AtIndex_320()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_320.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_320, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_320, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_320.AtIndex(ref logic_uScript_AccessListTech_techList_320, logic_uScript_AccessListTech_index_320, out logic_uScript_AccessListTech_value_320);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_320;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_320;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_320.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_322()
	{
		logic_uScript_LockTech_tech_322 = local_vehicleTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_322.In(logic_uScript_LockTech_tech_322, logic_uScript_LockTech_lockType_322);
		if (logic_uScript_LockTech_uScript_LockTech_322.Out)
		{
			Relay_In_306();
		}
	}

	private void Relay_In_323()
	{
		logic_uScript_LockTechSendToSCU_tech_323 = local_vehicleTech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_323.In(logic_uScript_LockTechSendToSCU_tech_323, logic_uScript_LockTechSendToSCU_lockSendToSCU_323);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_323.Out)
		{
			Relay_In_351();
		}
	}

	private void Relay_In_325()
	{
		logic_uScript_LockTech_tech_325 = local_vehicleTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_325.In(logic_uScript_LockTech_tech_325, logic_uScript_LockTech_lockType_325);
		if (logic_uScript_LockTech_uScript_LockTech_325.Out)
		{
			Relay_In_323();
		}
	}

	private void Relay_In_326()
	{
		logic_uScript_LockTech_tech_326 = local_vehicleTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_326.In(logic_uScript_LockTech_tech_326, logic_uScript_LockTech_lockType_326);
		if (logic_uScript_LockTech_uScript_LockTech_326.Out)
		{
			Relay_In_322();
		}
	}

	private void Relay_In_327()
	{
		logic_uScript_LockTech_tech_327 = local_vehicleTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_327.In(logic_uScript_LockTech_tech_327, logic_uScript_LockTech_lockType_327);
		if (logic_uScript_LockTech_uScript_LockTech_327.Out)
		{
			Relay_In_325();
		}
	}

	private void Relay_In_328()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_328.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_328.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_328.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_253();
		}
		if (multiplayer)
		{
			Relay_In_357();
		}
	}

	private void Relay_InitialSpawn_330()
	{
		int num = 0;
		Array array = vehicleSpawnData2;
		if (logic_uScript_SpawnTechsFromData_spawnData_330.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_330, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_330, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_330 = owner_Connection_329;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_330.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_330, logic_uScript_SpawnTechsFromData_ownerNode_330, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_330, logic_uScript_SpawnTechsFromData_allowResurrection_330);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_330.Out)
		{
			Relay_In_253();
		}
	}

	private void Relay_InitialSpawn_334()
	{
		int num = 0;
		Array array = vehicleSpawnData3;
		if (logic_uScript_SpawnTechsFromData_spawnData_334.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_334, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_334, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_334 = owner_Connection_333;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_334.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_334, logic_uScript_SpawnTechsFromData_ownerNode_334, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_334, logic_uScript_SpawnTechsFromData_allowResurrection_334);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_334.Out)
		{
			Relay_InitialSpawn_330();
		}
	}

	private void Relay_InitialSpawn_337()
	{
		int num = 0;
		Array array = vehicleSpawnData4;
		if (logic_uScript_SpawnTechsFromData_spawnData_337.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_337, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_337, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_337 = owner_Connection_336;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_337.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_337, logic_uScript_SpawnTechsFromData_ownerNode_337, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_337, logic_uScript_SpawnTechsFromData_allowResurrection_337);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_337.Out)
		{
			Relay_InitialSpawn_334();
		}
	}

	private void Relay_In_338()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_338.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_338.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_338.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_506();
		}
		if (multiplayer)
		{
			Relay_In_395();
		}
	}

	private void Relay_In_342()
	{
		logic_uScript_IsTechPlayer_tech_342 = local_vehicleTech4_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_342.In(logic_uScript_IsTechPlayer_tech_342);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_342.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_342.False;
		if (num)
		{
			Relay_In_346();
		}
		if (flag)
		{
			Relay_In_343();
		}
	}

	private void Relay_In_343()
	{
		logic_uScript_IsTechPlayer_tech_343 = local_vehicleTech3_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_343.In(logic_uScript_IsTechPlayer_tech_343);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_343.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_343.False;
		if (num)
		{
			Relay_In_347();
		}
		if (flag)
		{
			Relay_In_344();
		}
	}

	private void Relay_In_344()
	{
		logic_uScript_IsTechPlayer_tech_344 = local_vehicleTech2_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_344.In(logic_uScript_IsTechPlayer_tech_344);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_344.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_344.False;
		if (num)
		{
			Relay_In_327();
		}
		if (flag)
		{
			Relay_In_326();
		}
	}

	private void Relay_In_345()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_345.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_345.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_345.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_349();
		}
		if (multiplayer)
		{
			Relay_In_394();
		}
	}

	private void Relay_In_346()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_346.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_346.Out)
		{
			Relay_In_347();
		}
	}

	private void Relay_In_347()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_347.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_347.Out)
		{
			Relay_In_327();
		}
	}

	private void Relay_In_348()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_348.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_348.Out)
		{
			Relay_In_346();
		}
	}

	private void Relay_In_349()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_349.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_349.Out)
		{
			Relay_In_326();
		}
	}

	private void Relay_In_350()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_350.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_350.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_350.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_505();
		}
		if (multiplayer)
		{
			Relay_In_389();
		}
	}

	private void Relay_In_351()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_351.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_351.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_351.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_504();
		}
		if (multiplayer)
		{
			Relay_In_362();
		}
	}

	private void Relay_Pause_352()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_352.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_352.Out)
		{
			Relay_In_263();
		}
	}

	private void Relay_UnPause_352()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_352.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_352.Out)
		{
			Relay_In_263();
		}
	}

	private void Relay_In_353()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_353 = owner_Connection_354;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_353.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_353);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_353.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_353.False;
		if (num)
		{
			Relay_Pause_356();
		}
		if (flag)
		{
			Relay_UnPause_355();
		}
	}

	private void Relay_Pause_355()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_355.Pause();
	}

	private void Relay_UnPause_355()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_355.UnPause();
	}

	private void Relay_Pause_356()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_356.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_356.Out)
		{
			Relay_In_112();
		}
	}

	private void Relay_UnPause_356()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_356.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_356.Out)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_357()
	{
		logic_uScript_GetMaxPlayers_Return_357 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_357.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_357;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_357.Out)
		{
			Relay_In_359();
		}
	}

	private void Relay_Output1_359()
	{
		Relay_In_253();
	}

	private void Relay_Output2_359()
	{
		Relay_InitialSpawn_330();
	}

	private void Relay_Output3_359()
	{
		Relay_InitialSpawn_334();
	}

	private void Relay_Output4_359()
	{
		Relay_InitialSpawn_337();
	}

	private void Relay_Output5_359()
	{
	}

	private void Relay_Output6_359()
	{
	}

	private void Relay_Output7_359()
	{
	}

	private void Relay_Output8_359()
	{
	}

	private void Relay_In_359()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_359 = local_MaxPlayers_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_359.In(logic_uScriptCon_ManualSwitch_CurrentOutput_359);
	}

	private void Relay_Output1_360()
	{
		Relay_In_375();
	}

	private void Relay_Output2_360()
	{
		Relay_In_363();
	}

	private void Relay_Output3_360()
	{
		Relay_In_367();
	}

	private void Relay_Output4_360()
	{
		Relay_In_372();
	}

	private void Relay_Output5_360()
	{
	}

	private void Relay_Output6_360()
	{
	}

	private void Relay_Output7_360()
	{
	}

	private void Relay_Output8_360()
	{
	}

	private void Relay_In_360()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_360 = local_MaxPlayers_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_360.In(logic_uScriptCon_ManualSwitch_CurrentOutput_360);
	}

	private void Relay_In_362()
	{
		logic_uScript_GetMaxPlayers_Return_362 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_362.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_362;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_362.Out)
		{
			Relay_In_360();
		}
	}

	private void Relay_In_363()
	{
		logic_uScript_LockTech_tech_363 = local_vehicleTech2_Tank;
		logic_uScript_LockTech_uScript_LockTech_363.In(logic_uScript_LockTech_tech_363, logic_uScript_LockTech_lockType_363);
		if (logic_uScript_LockTech_uScript_LockTech_363.Out)
		{
			Relay_In_365();
		}
	}

	private void Relay_In_365()
	{
		logic_uScript_LockTech_tech_365 = local_vehicleTech2_Tank;
		logic_uScript_LockTech_uScript_LockTech_365.In(logic_uScript_LockTech_tech_365, logic_uScript_LockTech_lockType_365);
		if (logic_uScript_LockTech_uScript_LockTech_365.Out)
		{
			Relay_In_366();
		}
	}

	private void Relay_In_366()
	{
		logic_uScript_LockTechSendToSCU_tech_366 = local_vehicleTech2_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_366.In(logic_uScript_LockTechSendToSCU_tech_366, logic_uScript_LockTechSendToSCU_lockSendToSCU_366);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_366.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_367()
	{
		logic_uScript_LockTech_tech_367 = local_vehicleTech3_Tank;
		logic_uScript_LockTech_uScript_LockTech_367.In(logic_uScript_LockTech_tech_367, logic_uScript_LockTech_lockType_367);
		if (logic_uScript_LockTech_uScript_LockTech_367.Out)
		{
			Relay_In_369();
		}
	}

	private void Relay_In_369()
	{
		logic_uScript_LockTech_tech_369 = local_vehicleTech3_Tank;
		logic_uScript_LockTech_uScript_LockTech_369.In(logic_uScript_LockTech_tech_369, logic_uScript_LockTech_lockType_369);
		if (logic_uScript_LockTech_uScript_LockTech_369.Out)
		{
			Relay_In_370();
		}
	}

	private void Relay_In_370()
	{
		logic_uScript_LockTechSendToSCU_tech_370 = local_vehicleTech3_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_370.In(logic_uScript_LockTechSendToSCU_tech_370, logic_uScript_LockTechSendToSCU_lockSendToSCU_370);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_370.Out)
		{
			Relay_In_363();
		}
	}

	private void Relay_In_371()
	{
		logic_uScript_LockTech_tech_371 = local_vehicleTech4_Tank;
		logic_uScript_LockTech_uScript_LockTech_371.In(logic_uScript_LockTech_tech_371, logic_uScript_LockTech_lockType_371);
		if (logic_uScript_LockTech_uScript_LockTech_371.Out)
		{
			Relay_In_374();
		}
	}

	private void Relay_In_372()
	{
		logic_uScript_LockTech_tech_372 = local_vehicleTech4_Tank;
		logic_uScript_LockTech_uScript_LockTech_372.In(logic_uScript_LockTech_tech_372, logic_uScript_LockTech_lockType_372);
		if (logic_uScript_LockTech_uScript_LockTech_372.Out)
		{
			Relay_In_371();
		}
	}

	private void Relay_In_374()
	{
		logic_uScript_LockTechSendToSCU_tech_374 = local_vehicleTech4_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_374.In(logic_uScript_LockTechSendToSCU_tech_374, logic_uScript_LockTechSendToSCU_lockSendToSCU_374);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_374.Out)
		{
			Relay_In_367();
		}
	}

	private void Relay_In_375()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_375.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_375.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_376()
	{
		logic_uScript_LockTechSendToSCU_tech_376 = local_vehicleTech3_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_376.In(logic_uScript_LockTechSendToSCU_tech_376, logic_uScript_LockTechSendToSCU_lockSendToSCU_376);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_376.Out)
		{
			Relay_In_387();
		}
	}

	private void Relay_In_377()
	{
		logic_uScript_LockTechSendToSCU_tech_377 = local_vehicleTech2_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_377.In(logic_uScript_LockTechSendToSCU_tech_377, logic_uScript_LockTechSendToSCU_lockSendToSCU_377);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_377.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_379()
	{
		logic_uScript_LockTechSendToSCU_tech_379 = local_vehicleTech4_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_379.In(logic_uScript_LockTechSendToSCU_tech_379, logic_uScript_LockTechSendToSCU_lockSendToSCU_379);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_379.Out)
		{
			Relay_In_390();
		}
	}

	private void Relay_Output1_381()
	{
		Relay_In_388();
	}

	private void Relay_Output2_381()
	{
		Relay_In_387();
	}

	private void Relay_Output3_381()
	{
		Relay_In_390();
	}

	private void Relay_Output4_381()
	{
		Relay_In_383();
	}

	private void Relay_Output5_381()
	{
	}

	private void Relay_Output6_381()
	{
	}

	private void Relay_Output7_381()
	{
	}

	private void Relay_Output8_381()
	{
	}

	private void Relay_In_381()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_381 = local_MaxPlayers_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_381.In(logic_uScriptCon_ManualSwitch_CurrentOutput_381);
	}

	private void Relay_In_383()
	{
		logic_uScript_LockTech_tech_383 = local_vehicleTech4_Tank;
		logic_uScript_LockTech_uScript_LockTech_383.In(logic_uScript_LockTech_tech_383, logic_uScript_LockTech_lockType_383);
		if (logic_uScript_LockTech_uScript_LockTech_383.Out)
		{
			Relay_In_391();
		}
	}

	private void Relay_In_385()
	{
		logic_uScript_LockTech_tech_385 = local_vehicleTech2_Tank;
		logic_uScript_LockTech_uScript_LockTech_385.In(logic_uScript_LockTech_tech_385, logic_uScript_LockTech_lockType_385);
		if (logic_uScript_LockTech_uScript_LockTech_385.Out)
		{
			Relay_In_377();
		}
	}

	private void Relay_In_386()
	{
		logic_uScript_LockTech_tech_386 = local_vehicleTech3_Tank;
		logic_uScript_LockTech_uScript_LockTech_386.In(logic_uScript_LockTech_tech_386, logic_uScript_LockTech_lockType_386);
		if (logic_uScript_LockTech_uScript_LockTech_386.Out)
		{
			Relay_In_376();
		}
	}

	private void Relay_In_387()
	{
		logic_uScript_LockTech_tech_387 = local_vehicleTech2_Tank;
		logic_uScript_LockTech_uScript_LockTech_387.In(logic_uScript_LockTech_tech_387, logic_uScript_LockTech_lockType_387);
		if (logic_uScript_LockTech_uScript_LockTech_387.Out)
		{
			Relay_In_385();
		}
	}

	private void Relay_In_388()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_388.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_388.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_389()
	{
		logic_uScript_GetMaxPlayers_Return_389 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_389.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_389;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_389.Out)
		{
			Relay_In_381();
		}
	}

	private void Relay_In_390()
	{
		logic_uScript_LockTech_tech_390 = local_vehicleTech3_Tank;
		logic_uScript_LockTech_uScript_LockTech_390.In(logic_uScript_LockTech_tech_390, logic_uScript_LockTech_lockType_390);
		if (logic_uScript_LockTech_uScript_LockTech_390.Out)
		{
			Relay_In_386();
		}
	}

	private void Relay_In_391()
	{
		logic_uScript_LockTech_tech_391 = local_vehicleTech4_Tank;
		logic_uScript_LockTech_uScript_LockTech_391.In(logic_uScript_LockTech_tech_391, logic_uScript_LockTech_lockType_391);
		if (logic_uScript_LockTech_uScript_LockTech_391.Out)
		{
			Relay_In_379();
		}
	}

	private void Relay_Output1_392()
	{
	}

	private void Relay_Output2_392()
	{
		Relay_In_344();
	}

	private void Relay_Output3_392()
	{
		Relay_In_343();
	}

	private void Relay_Output4_392()
	{
		Relay_In_342();
	}

	private void Relay_Output5_392()
	{
	}

	private void Relay_Output6_392()
	{
	}

	private void Relay_Output7_392()
	{
	}

	private void Relay_Output8_392()
	{
	}

	private void Relay_In_392()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_392 = local_MaxPlayers_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_392.In(logic_uScriptCon_ManualSwitch_CurrentOutput_392);
	}

	private void Relay_In_394()
	{
		logic_uScript_GetMaxPlayers_Return_394 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_394.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_394;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_394.Out)
		{
			Relay_In_392();
		}
	}

	private void Relay_In_395()
	{
		logic_uScript_GetMaxPlayers_Return_395 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_395.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_395;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_395.Out)
		{
			Relay_In_396();
		}
	}

	private void Relay_Output1_396()
	{
		Relay_In_416();
	}

	private void Relay_Output2_396()
	{
		Relay_In_412();
	}

	private void Relay_Output3_396()
	{
		Relay_In_406();
	}

	private void Relay_Output4_396()
	{
		Relay_In_403();
	}

	private void Relay_Output5_396()
	{
	}

	private void Relay_Output6_396()
	{
	}

	private void Relay_Output7_396()
	{
	}

	private void Relay_Output8_396()
	{
	}

	private void Relay_In_396()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_396 = local_MaxPlayers_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_396.In(logic_uScriptCon_ManualSwitch_CurrentOutput_396);
	}

	private void Relay_AtIndex_399()
	{
		int num = 0;
		Array array = local_vehicleTechs4_TankArray;
		if (logic_uScript_AccessListTech_techList_399.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_399, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_399, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_399.AtIndex(ref logic_uScript_AccessListTech_techList_399, logic_uScript_AccessListTech_index_399, out logic_uScript_AccessListTech_value_399);
		local_vehicleTechs4_TankArray = logic_uScript_AccessListTech_techList_399;
		local_vehicleTech4_Tank = logic_uScript_AccessListTech_value_399;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_399.Out)
		{
			Relay_In_406();
		}
	}

	private void Relay_In_403()
	{
		int num = 0;
		Array array = vehicleSpawnData4;
		if (logic_uScript_GetAndCheckTechs_techData_403.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_403, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_403, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_403 = owner_Connection_402;
		int num2 = 0;
		Array array2 = local_vehicleTechs4_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_403.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_403, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_403, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_403 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_403.In(logic_uScript_GetAndCheckTechs_techData_403, logic_uScript_GetAndCheckTechs_ownerNode_403, ref logic_uScript_GetAndCheckTechs_techs_403);
		local_vehicleTechs4_TankArray = logic_uScript_GetAndCheckTechs_techs_403;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_403.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_403.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_399();
		}
		if (someAlive)
		{
			Relay_AtIndex_399();
		}
	}

	private void Relay_In_406()
	{
		int num = 0;
		Array array = vehicleSpawnData3;
		if (logic_uScript_GetAndCheckTechs_techData_406.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_406, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_406, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_406 = owner_Connection_405;
		int num2 = 0;
		Array array2 = local_vehicleTechs3_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_406.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_406, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_406, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_406 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_406.In(logic_uScript_GetAndCheckTechs_techData_406, logic_uScript_GetAndCheckTechs_ownerNode_406, ref logic_uScript_GetAndCheckTechs_techs_406);
		local_vehicleTechs3_TankArray = logic_uScript_GetAndCheckTechs_techs_406;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_406.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_406.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_408();
		}
		if (someAlive)
		{
			Relay_AtIndex_408();
		}
	}

	private void Relay_AtIndex_408()
	{
		int num = 0;
		Array array = local_vehicleTechs3_TankArray;
		if (logic_uScript_AccessListTech_techList_408.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_408, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_408, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_408.AtIndex(ref logic_uScript_AccessListTech_techList_408, logic_uScript_AccessListTech_index_408, out logic_uScript_AccessListTech_value_408);
		local_vehicleTechs3_TankArray = logic_uScript_AccessListTech_techList_408;
		local_vehicleTech3_Tank = logic_uScript_AccessListTech_value_408;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_408.Out)
		{
			Relay_In_412();
		}
	}

	private void Relay_In_412()
	{
		int num = 0;
		Array array = vehicleSpawnData2;
		if (logic_uScript_GetAndCheckTechs_techData_412.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_412, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_412, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_412 = owner_Connection_410;
		int num2 = 0;
		Array array2 = local_vehicleTechs2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_412.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_412, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_412, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_412 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_412.In(logic_uScript_GetAndCheckTechs_techData_412, logic_uScript_GetAndCheckTechs_ownerNode_412, ref logic_uScript_GetAndCheckTechs_techs_412);
		local_vehicleTechs2_TankArray = logic_uScript_GetAndCheckTechs_techs_412;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_412.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_412.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_414();
		}
		if (someAlive)
		{
			Relay_AtIndex_414();
		}
	}

	private void Relay_AtIndex_414()
	{
		int num = 0;
		Array array = local_vehicleTechs2_TankArray;
		if (logic_uScript_AccessListTech_techList_414.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_414, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_414, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_414.AtIndex(ref logic_uScript_AccessListTech_techList_414, logic_uScript_AccessListTech_index_414, out logic_uScript_AccessListTech_value_414);
		local_vehicleTechs2_TankArray = logic_uScript_AccessListTech_techList_414;
		local_vehicleTech2_Tank = logic_uScript_AccessListTech_value_414;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_414.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_416()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_416.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_416.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_422()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_422.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_422, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_422, num, array.Length);
		num += array.Length;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_422 = local_MaxPlayers_System_Int32;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_422.In(logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_422, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_422);
		bool num2 = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_422.True;
		bool flag = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_422.False;
		if (num2)
		{
			Relay_False_434();
		}
		if (flag)
		{
			Relay_In_448();
		}
	}

	private void Relay_In_425()
	{
		logic_uScript_GetMaxPlayers_Return_425 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_425.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_425;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_425.Out)
		{
			Relay_In_422();
		}
	}

	private void Relay_In_427()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_427 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_427 = msgPromptAccessDenied;
		logic_uScript_MissionPromptBlock_Show_targetBlock_427 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_427.In(logic_uScript_MissionPromptBlock_Show_bodyText_427, logic_uScript_MissionPromptBlock_Show_acceptButtonText_427, logic_uScript_MissionPromptBlock_Show_rejectButtonText_427, logic_uScript_MissionPromptBlock_Show_targetBlock_427, logic_uScript_MissionPromptBlock_Show_highlightBlock_427, logic_uScript_MissionPromptBlock_Show_singleUse_427);
	}

	private void Relay_Save_Out_431()
	{
		Relay_Save_463();
	}

	private void Relay_Load_Out_431()
	{
		Relay_Load_463();
	}

	private void Relay_Restart_Out_431()
	{
		Relay_Set_False_463();
	}

	private void Relay_Save_431()
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_431 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Save(ref logic_SubGraph_SaveLoadBool_boolean_431, logic_SubGraph_SaveLoadBool_boolAsVariable_431, logic_SubGraph_SaveLoadBool_uniqueID_431);
	}

	private void Relay_Load_431()
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_431 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Load(ref logic_SubGraph_SaveLoadBool_boolean_431, logic_SubGraph_SaveLoadBool_boolAsVariable_431, logic_SubGraph_SaveLoadBool_uniqueID_431);
	}

	private void Relay_Set_True_431()
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_431 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_431, logic_SubGraph_SaveLoadBool_boolAsVariable_431, logic_SubGraph_SaveLoadBool_uniqueID_431);
	}

	private void Relay_Set_False_431()
	{
		logic_SubGraph_SaveLoadBool_boolean_431 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_431 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_431.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_431, logic_SubGraph_SaveLoadBool_boolAsVariable_431, logic_SubGraph_SaveLoadBool_uniqueID_431);
	}

	private void Relay_True_433()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_433.True(out logic_uScriptAct_SetBool_Target_433);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_433;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_433.Out)
		{
			Relay_In_427();
		}
	}

	private void Relay_False_433()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_433.False(out logic_uScriptAct_SetBool_Target_433);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_433;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_433.Out)
		{
			Relay_In_427();
		}
	}

	private void Relay_True_434()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_434.True(out logic_uScriptAct_SetBool_Target_434);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_434;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_434.Out)
		{
			Relay_In_219();
		}
	}

	private void Relay_False_434()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_434.False(out logic_uScriptAct_SetBool_Target_434);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_434;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_434.Out)
		{
			Relay_In_219();
		}
	}

	private void Relay_In_437()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_437.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_437, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_437, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_437 = owner_Connection_440;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_437.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_437, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_437, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_437 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_437.In(logic_uScript_GetAndCheckTechs_techData_437, logic_uScript_GetAndCheckTechs_ownerNode_437, ref logic_uScript_GetAndCheckTechs_techs_437);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_437;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_437.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_437.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_439();
		}
		if (someAlive)
		{
			Relay_AtIndex_439();
		}
	}

	private void Relay_AtIndex_439()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_439.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_439, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_439, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_439.AtIndex(ref logic_uScript_AccessListTech_techList_439, logic_uScript_AccessListTech_index_439, out logic_uScript_AccessListTech_value_439);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_439;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_439;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_439.Out)
		{
			Relay_In_493();
		}
	}

	private void Relay_In_443()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_443.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_443, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_443, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_443 = owner_Connection_446;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_443.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_443, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_443, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_443 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_443.In(logic_uScript_GetAndCheckTechs_techData_443, logic_uScript_GetAndCheckTechs_ownerNode_443, ref logic_uScript_GetAndCheckTechs_techs_443);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_443;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_443.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_443.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_445();
		}
		if (someAlive)
		{
			Relay_AtIndex_445();
		}
	}

	private void Relay_AtIndex_445()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_445.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_445, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_445, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_445.AtIndex(ref logic_uScript_AccessListTech_techList_445, logic_uScript_AccessListTech_index_445, out logic_uScript_AccessListTech_value_445);
		local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_445;
		local_techNPC_Tank = logic_uScript_AccessListTech_value_445;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_445.Out)
		{
			Relay_In_437();
		}
	}

	private void Relay_In_448()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_448.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_448.Out)
		{
			Relay_True_433();
		}
	}

	private void Relay_In_449()
	{
		logic_uScript_EnableGlow_targetObject_449 = local_TerminalBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_449.In(logic_uScript_EnableGlow_targetObject_449, logic_uScript_EnableGlow_enable_449);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_449.Out)
		{
			Relay_In_451();
		}
	}

	private void Relay_In_451()
	{
		logic_uScript_HideArrow_uScript_HideArrow_451.In();
	}

	private void Relay_In_452()
	{
		logic_uScriptCon_CompareBool_Bool_452 = local_BlockLimitCritical_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452.In(logic_uScriptCon_CompareBool_Bool_452);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452.False;
		if (num)
		{
			Relay_In_460();
		}
		if (flag)
		{
			Relay_In_212();
		}
	}

	private void Relay_In_457()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_457.In();
	}

	private void Relay_In_458()
	{
		logic_uScript_AddMessage_messageData_458 = msgDespawnTechs;
		logic_uScript_AddMessage_speaker_458 = SpeakerNPC;
		logic_uScript_AddMessage_Return_458 = logic_uScript_AddMessage_uScript_AddMessage_458.In(logic_uScript_AddMessage_messageData_458, logic_uScript_AddMessage_speaker_458);
		if (logic_uScript_AddMessage_uScript_AddMessage_458.Out)
		{
			Relay_True_459();
		}
	}

	private void Relay_True_459()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_459.True(out logic_uScriptAct_SetBool_Target_459);
		local_msgDespawnTechsShown_System_Boolean = logic_uScriptAct_SetBool_Target_459;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_459.Out)
		{
			Relay_True_466();
		}
	}

	private void Relay_False_459()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_459.False(out logic_uScriptAct_SetBool_Target_459);
		local_msgDespawnTechsShown_System_Boolean = logic_uScriptAct_SetBool_Target_459;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_459.Out)
		{
			Relay_True_466();
		}
	}

	private void Relay_In_460()
	{
		logic_uScriptCon_CompareBool_Bool_460 = local_msgDespawnTechsShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_460.In(logic_uScriptCon_CompareBool_Bool_460);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_460.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_460.False;
		if (num)
		{
			Relay_In_457();
		}
		if (flag)
		{
			Relay_In_458();
		}
	}

	private void Relay_Save_Out_463()
	{
		Relay_Save_479();
	}

	private void Relay_Load_Out_463()
	{
		Relay_Load_479();
	}

	private void Relay_Restart_Out_463()
	{
		Relay_Set_False_479();
	}

	private void Relay_Save_463()
	{
		logic_SubGraph_SaveLoadBool_boolean_463 = local_msgDespawnTechsShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_463 = local_msgDespawnTechsShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Save(ref logic_SubGraph_SaveLoadBool_boolean_463, logic_SubGraph_SaveLoadBool_boolAsVariable_463, logic_SubGraph_SaveLoadBool_uniqueID_463);
	}

	private void Relay_Load_463()
	{
		logic_SubGraph_SaveLoadBool_boolean_463 = local_msgDespawnTechsShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_463 = local_msgDespawnTechsShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Load(ref logic_SubGraph_SaveLoadBool_boolean_463, logic_SubGraph_SaveLoadBool_boolAsVariable_463, logic_SubGraph_SaveLoadBool_uniqueID_463);
	}

	private void Relay_Set_True_463()
	{
		logic_SubGraph_SaveLoadBool_boolean_463 = local_msgDespawnTechsShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_463 = local_msgDespawnTechsShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_463, logic_SubGraph_SaveLoadBool_boolAsVariable_463, logic_SubGraph_SaveLoadBool_uniqueID_463);
	}

	private void Relay_Set_False_463()
	{
		logic_SubGraph_SaveLoadBool_boolean_463 = local_msgDespawnTechsShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_463 = local_msgDespawnTechsShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_463.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_463, logic_SubGraph_SaveLoadBool_boolAsVariable_463, logic_SubGraph_SaveLoadBool_uniqueID_463);
	}

	private void Relay_In_464()
	{
		logic_uScript_HideArrow_uScript_HideArrow_464.In();
	}

	private void Relay_True_466()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_466.True(out logic_uScriptAct_SetBool_Target_466);
		local_Wait_System_Boolean = logic_uScriptAct_SetBool_Target_466;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_466.Out)
		{
			Relay_In_457();
		}
	}

	private void Relay_False_466()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_466.False(out logic_uScriptAct_SetBool_Target_466);
		local_Wait_System_Boolean = logic_uScriptAct_SetBool_Target_466;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_466.Out)
		{
			Relay_In_457();
		}
	}

	private void Relay_True_467()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_467.True(out logic_uScriptAct_SetBool_Target_467);
		local_Wait_System_Boolean = logic_uScriptAct_SetBool_Target_467;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_467.Out)
		{
			Relay_In_215();
		}
	}

	private void Relay_False_467()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_467.False(out logic_uScriptAct_SetBool_Target_467);
		local_Wait_System_Boolean = logic_uScriptAct_SetBool_Target_467;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_467.Out)
		{
			Relay_In_215();
		}
	}

	private void Relay_True_469()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_469.True(out logic_uScriptAct_SetBool_Target_469);
		local_Wait_System_Boolean = logic_uScriptAct_SetBool_Target_469;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_469.Out)
		{
			Relay_In_203();
		}
	}

	private void Relay_False_469()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_469.False(out logic_uScriptAct_SetBool_Target_469);
		local_Wait_System_Boolean = logic_uScriptAct_SetBool_Target_469;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_469.Out)
		{
			Relay_In_203();
		}
	}

	private void Relay_True_471()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_471.True(out logic_uScriptAct_SetBool_Target_471);
		local_Wait_System_Boolean = logic_uScriptAct_SetBool_Target_471;
	}

	private void Relay_False_471()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_471.False(out logic_uScriptAct_SetBool_Target_471);
		local_Wait_System_Boolean = logic_uScriptAct_SetBool_Target_471;
	}

	private void Relay_In_473()
	{
		logic_uScript_Wait_uScript_Wait_473.In(logic_uScript_Wait_seconds_473, logic_uScript_Wait_repeat_473);
		if (logic_uScript_Wait_uScript_Wait_473.Waited)
		{
			Relay_False_471();
		}
	}

	private void Relay_In_474()
	{
		logic_uScriptCon_CompareBool_Bool_474 = local_Wait_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_474.In(logic_uScriptCon_CompareBool_Bool_474);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_474.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_474.False;
		if (num)
		{
			Relay_In_473();
		}
		if (flag)
		{
			Relay_False_481();
		}
	}

	private void Relay_Out_477()
	{
	}

	private void Relay_In_477()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_477 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_477.In(logic_SubGraph_LoadObjectiveStates_currentObjective_477);
	}

	private void Relay_Save_Out_479()
	{
	}

	private void Relay_Load_Out_479()
	{
		Relay_In_477();
	}

	private void Relay_Restart_Out_479()
	{
	}

	private void Relay_Save_479()
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = local_Wait_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_479 = local_Wait_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Save(ref logic_SubGraph_SaveLoadBool_boolean_479, logic_SubGraph_SaveLoadBool_boolAsVariable_479, logic_SubGraph_SaveLoadBool_uniqueID_479);
	}

	private void Relay_Load_479()
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = local_Wait_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_479 = local_Wait_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Load(ref logic_SubGraph_SaveLoadBool_boolean_479, logic_SubGraph_SaveLoadBool_boolAsVariable_479, logic_SubGraph_SaveLoadBool_uniqueID_479);
	}

	private void Relay_Set_True_479()
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = local_Wait_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_479 = local_Wait_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_479, logic_SubGraph_SaveLoadBool_boolAsVariable_479, logic_SubGraph_SaveLoadBool_uniqueID_479);
	}

	private void Relay_Set_False_479()
	{
		logic_SubGraph_SaveLoadBool_boolean_479 = local_Wait_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_479 = local_Wait_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_479.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_479, logic_SubGraph_SaveLoadBool_boolAsVariable_479, logic_SubGraph_SaveLoadBool_uniqueID_479);
	}

	private void Relay_True_481()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_481.True(out logic_uScriptAct_SetBool_Target_481);
		local_msgDespawnTechsShown_System_Boolean = logic_uScriptAct_SetBool_Target_481;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_481.Out)
		{
			Relay_False_484();
		}
	}

	private void Relay_False_481()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_481.False(out logic_uScriptAct_SetBool_Target_481);
		local_msgDespawnTechsShown_System_Boolean = logic_uScriptAct_SetBool_Target_481;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_481.Out)
		{
			Relay_False_484();
		}
	}

	private void Relay_True_484()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_484.True(out logic_uScriptAct_SetBool_Target_484);
		local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_484;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_484.Out)
		{
			Relay_False_485();
		}
	}

	private void Relay_False_484()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_484.False(out logic_uScriptAct_SetBool_Target_484);
		local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_484;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_484.Out)
		{
			Relay_False_485();
		}
	}

	private void Relay_True_485()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_485.True(out logic_uScriptAct_SetBool_Target_485);
		local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_485;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_485.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_False_485()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_485.False(out logic_uScriptAct_SetBool_Target_485);
		local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_485;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_485.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_SetInvulnerable_486()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_486.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_486, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_486, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_486.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_486);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_486.Out)
		{
			Relay_Succeed_13();
		}
	}

	private void Relay_SetVulnerable_486()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_486.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_486, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_486, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_486.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_486);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_486.Out)
		{
			Relay_Succeed_13();
		}
	}

	private void Relay_In_488()
	{
		logic_uScript_LockTechSendToSCU_tech_488 = local_vehicleTech3_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_488.In(logic_uScript_LockTechSendToSCU_tech_488, logic_uScript_LockTechSendToSCU_lockSendToSCU_488);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_488.Out)
		{
			Relay_In_489();
		}
	}

	private void Relay_In_489()
	{
		logic_uScript_LockTechSendToSCU_tech_489 = local_vehicleTech2_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_489.In(logic_uScript_LockTechSendToSCU_tech_489, logic_uScript_LockTechSendToSCU_lockSendToSCU_489);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_489.Out)
		{
			Relay_In_502();
		}
	}

	private void Relay_In_491()
	{
		logic_uScript_LockTechSendToSCU_tech_491 = local_vehicleTech4_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_491.In(logic_uScript_LockTechSendToSCU_tech_491, logic_uScript_LockTechSendToSCU_lockSendToSCU_491);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_491.Out)
		{
			Relay_In_488();
		}
	}

	private void Relay_In_493()
	{
		logic_uScript_LockTechSendToSCU_tech_493 = local_vehicleTech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_493.In(logic_uScript_LockTechSendToSCU_tech_493, logic_uScript_LockTechSendToSCU_lockSendToSCU_493);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_493.Out)
		{
			Relay_In_499();
		}
	}

	private void Relay_Output1_494()
	{
		Relay_In_500();
	}

	private void Relay_Output2_494()
	{
		Relay_In_489();
	}

	private void Relay_Output3_494()
	{
		Relay_In_488();
	}

	private void Relay_Output4_494()
	{
		Relay_In_491();
	}

	private void Relay_Output5_494()
	{
	}

	private void Relay_Output6_494()
	{
	}

	private void Relay_Output7_494()
	{
	}

	private void Relay_Output8_494()
	{
	}

	private void Relay_In_494()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_494 = local_MaxPlayers_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.In(logic_uScriptCon_ManualSwitch_CurrentOutput_494);
	}

	private void Relay_In_499()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_499.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_499.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_499.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_503();
		}
		if (multiplayer)
		{
			Relay_In_501();
		}
	}

	private void Relay_In_500()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_500.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_500.Out)
		{
			Relay_In_502();
		}
	}

	private void Relay_In_501()
	{
		logic_uScript_GetMaxPlayers_Return_501 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_501.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_501;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_501.Out)
		{
			Relay_In_494();
		}
	}

	private void Relay_In_502()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_502.In();
	}

	private void Relay_In_503()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_503.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_503.Out)
		{
			Relay_In_500();
		}
	}

	private void Relay_In_504()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_504.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_504.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_505()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_505.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_505.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_506()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_506.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_506.Out)
		{
			Relay_In_111();
		}
	}
}
