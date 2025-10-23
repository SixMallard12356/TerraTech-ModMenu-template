using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("", "")]
[NodePath("Graphs")]
public class Mission_SetPiece_Bombfield : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool _DEBUGIgnoreMoneyCheck;

	public BlockTypes[] discoverableBlockTypesOnVehicle = new BlockTypes[0];

	public BlockTypes interactableBlockType;

	private bool local_368_System_Boolean;

	private TankBlock local_369_TankBlock;

	private int local_459_System_Int32;

	private string local_560_System_String = "Stage:";

	private string local_562_System_String = "";

	private string local_566_System_String = "";

	private string local_567_System_String = "";

	private string local_569_System_String = "";

	private bool local_AllTurretsDead_System_Boolean;

	private bool local_BlockLimitCritical_System_Boolean;

	private int local_CurrentMoney_System_Int32;

	private bool local_HasEnoughMoney_System_Boolean;

	private Tank local_MinefieldTech_Tank;

	private bool local_MsgArrivedAtMission_System_Boolean;

	private string local_MsgDead_System_String = "MsgDead";

	private bool local_MsgFirstTurretDead_System_Boolean;

	private bool local_MsgSecondTurretDead_System_Boolean;

	private bool local_MsgThirdTurretDead_System_Boolean;

	private string local_MsgTriggerA_System_String = "MsgTriggerA";

	private string local_MsgTriggerB_System_String = "MsgTriggerB";

	private string local_MsgTriggerC_System_String = "MsgTriggerC";

	private string local_MsgTriggerD_System_String = "MsgTriggerD";

	private Tank[] local_NPCPaymentPoints_TankArray = new Tank[0];

	private bool local_ObjectiveComplete_System_Boolean;

	private Tank local_PaymentPointTech_Tank;

	private Tank local_PlayerTech_Tank;

	private bool local_QLStepped1_System_Boolean;

	private bool local_QLStepped2_System_Boolean;

	private bool local_QLStepped3_System_Boolean;

	private bool local_RepeatWaitTime_System_Boolean;

	private bool local_SalesTechSetUp_System_Boolean;

	private bool local_ShownMsgBuyNewPlaneOffer_System_Boolean;

	private bool local_ShownMsgMinefieldWarning_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private Tank[] local_TechsInMinefield_TankArray = new Tank[0];

	private bool local_TechsSpawned_System_Boolean;

	private TankBlock local_TerminalBlock_TankBlock;

	private bool local_TriggerEnteredA_System_Boolean;

	private bool local_TriggerEnteredB_System_Boolean;

	private bool local_TriggerEnteredC_System_Boolean;

	private bool local_TriggerEnteredD_System_Boolean;

	private bool local_TurretMarkedDead1_System_Boolean;

	private bool local_TurretMarkedDead2_System_Boolean;

	private bool local_TurretMarkedDead3_System_Boolean;

	private Tank[] local_Turrets1_TankArray = new Tank[0];

	private Tank[] local_Turrets2_TankArray = new Tank[0];

	private Tank[] local_Turrets3_TankArray = new Tank[0];

	private int local_TurretsDead_System_Int32;

	private bool local_VehiclePurchased_System_Boolean;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] MsgArrivedAtMission = new LocalisedString[0];

	public LocalisedString[] MsgAtRightHeight = new LocalisedString[0];

	public LocalisedString[] MsgBuyNewPlaneOffer = new LocalisedString[0];

	public LocalisedString[] msgDespawnTechs = new LocalisedString[0];

	public LocalisedString[] MsgEnemyHitAMine = new LocalisedString[0];

	public LocalisedString[] MsgFirstTurretDead = new LocalisedString[0];

	public LocalisedString[] MsgMinefieldWarning = new LocalisedString[0];

	public LocalisedString[] MsgMissionComplete = new LocalisedString[0];

	public LocalisedString[] MsgNearTurret1 = new LocalisedString[0];

	public LocalisedString[] MsgNearTurret2 = new LocalisedString[0];

	public LocalisedString[] MsgNearTurret3 = new LocalisedString[0];

	public LocalisedString[] MsgPlayerHitAMine = new LocalisedString[0];

	public LocalisedString msgPromptAccept;

	public LocalisedString msgPromptAccessDenied;

	public LocalisedString msgPromptDecline;

	public LocalisedString msgPromptNoMoney;

	public LocalisedString msgPromptTextBlackbird;

	public LocalisedString[] MsgSecondTurretDead = new LocalisedString[0];

	public LocalisedString[] MsgThirdTurretDead = new LocalisedString[0];

	public LocalisedString[] MsgTooManyPlanesSpawnedAlready = new LocalisedString[0];

	public ExternalBehaviorTree NPCFlyAwayBehaviour;

	public SpawnTechData[] NPCPaymentPoint = new SpawnTechData[0];

	public BlockTypes PlayerTechBombBlock;

	public BlockTypes PlayerTechPropBlock;

	[Multiline(3)]
	public string TriggerA = "";

	[Multiline(3)]
	public string TriggerB = "";

	[Multiline(3)]
	public string TriggerC = "";

	[Multiline(3)]
	public string TriggerD = "";

	[Multiline(3)]
	public string TriggerG = "";

	[Multiline(3)]
	public string TriggerX = "";

	public SpawnTechData[] TurretData1 = new SpawnTechData[0];

	public SpawnTechData[] TurretData2 = new SpawnTechData[0];

	public SpawnTechData[] TurretData3 = new SpawnTechData[0];

	public int vehicleCost;

	public SpawnTechData[] vehicleSpawnData = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnData2 = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnData3 = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnData4 = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnData5 = new SpawnTechData[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_29;

	private GameObject owner_Connection_32;

	private GameObject owner_Connection_71;

	private GameObject owner_Connection_81;

	private GameObject owner_Connection_84;

	private GameObject owner_Connection_151;

	private GameObject owner_Connection_169;

	private GameObject owner_Connection_211;

	private GameObject owner_Connection_229;

	private GameObject owner_Connection_289;

	private GameObject owner_Connection_291;

	private GameObject owner_Connection_295;

	private GameObject owner_Connection_342;

	private GameObject owner_Connection_350;

	private GameObject owner_Connection_404;

	private GameObject owner_Connection_410;

	private GameObject owner_Connection_419;

	private GameObject owner_Connection_430;

	private GameObject owner_Connection_455;

	private GameObject owner_Connection_463;

	private GameObject owner_Connection_465;

	private GameObject owner_Connection_468;

	private GameObject owner_Connection_470;

	private GameObject owner_Connection_473;

	private GameObject owner_Connection_477;

	private GameObject owner_Connection_478;

	private GameObject owner_Connection_479;

	private GameObject owner_Connection_481;

	private GameObject owner_Connection_489;

	private GameObject owner_Connection_492;

	private GameObject owner_Connection_494;

	private GameObject owner_Connection_497;

	private GameObject owner_Connection_501;

	private GameObject owner_Connection_503;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_2 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_2;

	private bool logic_uScript_FinishEncounter_Out_2 = true;

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

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_12;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_12 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_12 = "TechsSpawned";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_13;

	private bool logic_uScriptCon_CompareBool_True_13 = true;

	private bool logic_uScriptCon_CompareBool_False_13 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_14 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_14 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_14;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_14 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_14;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_14 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_14 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_14 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_14 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_15;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_15 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_15 = "ObjectiveComplete";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_18;

	private bool logic_uScriptCon_CompareBool_True_18 = true;

	private bool logic_uScriptCon_CompareBool_False_18 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_19 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_19;

	private bool logic_uScriptAct_SetBool_Out_19 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_19 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_19 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_21 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_21 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_21;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_21 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_21;

	private bool logic_uScript_SpawnTechsFromData_Out_21 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_26 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_26;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_27 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_27;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_27 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_27 = "Stage";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_30 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_30 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_30;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_30 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_30;

	private bool logic_uScript_SpawnTechsFromData_Out_30 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_31 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_31 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_31;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_31 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_31;

	private bool logic_uScript_SpawnTechsFromData_Out_31 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_34 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_34;

	private bool logic_uScriptCon_CompareBool_True_34 = true;

	private bool logic_uScriptCon_CompareBool_False_34 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_36 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_36;

	private bool logic_uScriptAct_SetBool_Out_36 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_36 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_36 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_37 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_37 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_37 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_37;

	private string logic_uScript_AddOnScreenMessage_tag_37 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_37;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_37;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_37;

	private bool logic_uScript_AddOnScreenMessage_Out_37 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_37 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_41;

	private bool logic_uScriptCon_CompareBool_True_41 = true;

	private bool logic_uScriptCon_CompareBool_False_41 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_44 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_44;

	private bool logic_uScriptAct_SetBool_Out_44 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_44 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_44 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_45 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_45 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_45 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_45;

	private string logic_uScript_AddOnScreenMessage_tag_45 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_45;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_45;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_45;

	private bool logic_uScript_AddOnScreenMessage_Out_45 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_45 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_46;

	private bool logic_uScriptCon_CompareBool_True_46 = true;

	private bool logic_uScriptCon_CompareBool_False_46 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_48 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_48;

	private bool logic_uScriptAct_SetBool_Out_48 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_48 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_48 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_50 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_50 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_50;

	private string logic_uScript_AddOnScreenMessage_tag_50 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_50;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_50;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_50;

	private bool logic_uScript_AddOnScreenMessage_Out_50 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_50 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_53 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_53;

	private bool logic_uScriptAct_SetBool_Out_53 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_53 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_53 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_55;

	private bool logic_uScriptCon_CompareBool_True_55 = true;

	private bool logic_uScriptCon_CompareBool_False_55 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_57 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_57 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_57 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_57;

	private string logic_uScript_AddOnScreenMessage_tag_57 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_57;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_57;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_57;

	private bool logic_uScript_AddOnScreenMessage_Out_57 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_57 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_60 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_60;

	private bool logic_uScriptAct_SetBool_Out_60 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_60 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_60 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_61 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_61 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_62 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_62 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_66;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_66 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_66 = "MsgArrivedAtMission";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_67;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_67 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_67 = "MsgFirstTurretDead";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_68;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_68 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_68 = "MsgSecondTurretDead";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_69;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_69 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_69 = "MsgThirdTurretDead";

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_70 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_70;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_70 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_72 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_72 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_74 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_74 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_74 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_74 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_79 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_79 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_80 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_80 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_80;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_80 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_80;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_80 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_80 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_80 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_80 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_85 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_85 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_85;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_85 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_85;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_85 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_85 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_85 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_85 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_86;

	private bool logic_uScriptCon_CompareBool_True_86 = true;

	private bool logic_uScriptCon_CompareBool_False_86 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_89 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_89;

	private bool logic_uScriptAct_SetBool_Out_89 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_89 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_89 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_90 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_93;

	private bool logic_uScriptCon_CompareBool_True_93 = true;

	private bool logic_uScriptCon_CompareBool_False_93 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_94 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_94 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_95 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_95;

	private bool logic_uScriptAct_SetBool_Out_95 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_95 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_95 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_96;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_96;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_99 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_99;

	private bool logic_uScriptCon_CompareBool_True_99 = true;

	private bool logic_uScriptCon_CompareBool_False_99 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_100 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_100;

	private bool logic_uScriptAct_SetBool_Out_100 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_100 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_100 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_102 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_102 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_106;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_106 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_106 = "TurretMarkedDead1";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_107;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_107 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_107 = "TurretMarkedDead2";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_108;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_108 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_108 = "TurretMarkedDead3";

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_109 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_109 = 1;

	private int logic_uScriptAct_AddInt_v2_B_109;

	private int logic_uScriptAct_AddInt_v2_IntResult_109;

	private float logic_uScriptAct_AddInt_v2_FloatResult_109;

	private bool logic_uScriptAct_AddInt_v2_Out_109 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_110 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_110;

	private int logic_uScriptCon_CompareInt_B_110;

	private bool logic_uScriptCon_CompareInt_GreaterThan_110 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_110 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_110 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_110 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_110 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_110 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_112 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_112 = 1;

	private int logic_uScriptAct_AddInt_v2_B_112;

	private int logic_uScriptAct_AddInt_v2_IntResult_112;

	private float logic_uScriptAct_AddInt_v2_FloatResult_112;

	private bool logic_uScriptAct_AddInt_v2_Out_112 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_113 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_113 = 1;

	private int logic_uScriptAct_AddInt_v2_B_113;

	private int logic_uScriptAct_AddInt_v2_IntResult_113;

	private float logic_uScriptAct_AddInt_v2_FloatResult_113;

	private bool logic_uScriptAct_AddInt_v2_Out_113 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_115 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_115;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_115;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_117 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_117;

	private bool logic_uScriptAct_SetBool_Out_117 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_117 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_117 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_118;

	private bool logic_uScriptCon_CompareBool_True_118 = true;

	private bool logic_uScriptCon_CompareBool_False_118 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_120 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_120;

	private bool logic_uScriptAct_SetBool_Out_120 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_120 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_120 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_121;

	private bool logic_uScriptCon_CompareBool_True_121 = true;

	private bool logic_uScriptCon_CompareBool_False_121 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_123;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_123;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_126;

	private bool logic_uScriptCon_CompareBool_True_126 = true;

	private bool logic_uScriptCon_CompareBool_False_126 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_129 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_129;

	private bool logic_uScriptAct_SetBool_Out_129 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_129 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_129 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_131;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_131;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_132;

	private int logic_SubGraph_SaveLoadInt_integer_132;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_132 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_132 = "TurretsDead";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_133 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_133 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_134 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_134 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_135;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_136 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_136 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_136 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_136 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_137 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_137 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_137 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_137 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_138 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_138 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_138 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_138 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_139 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_139 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_139 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_139 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_139 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_139 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_139 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_142 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_142;

	private bool logic_uScriptCon_CompareBool_True_142 = true;

	private bool logic_uScriptCon_CompareBool_False_142 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_143 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_143 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_143 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_143;

	private string logic_uScript_AddOnScreenMessage_tag_143 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_143;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_143;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_143;

	private bool logic_uScript_AddOnScreenMessage_Out_143 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_143 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_146 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_146 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_146 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_146 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_147 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_147;

	private bool logic_uScriptAct_SetBool_Out_147 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_147 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_147 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_152 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_152;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_152 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_152;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_152 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_152 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_152 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_152 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_153 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_154 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_154 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_155 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_155 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_159 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_159 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_159 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_159;

	private string logic_uScript_AddOnScreenMessage_tag_159 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_159;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_159;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_159;

	private bool logic_uScript_AddOnScreenMessage_Out_159 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_159 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_160 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_160 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_162 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_162;

	private bool logic_uScriptAct_SetBool_Out_162 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_162 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_162 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_163 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_163 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_163 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_163 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_163 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_163 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_163 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_164 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_164 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_164;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_164 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_164;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_164 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_164 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_164 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_164 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_165 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_165 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_167 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_168;

	private bool logic_uScriptCon_CompareBool_True_168 = true;

	private bool logic_uScriptCon_CompareBool_False_168 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_172 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_172 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_172 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_172 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_173 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_173 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_173 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_173 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_175 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_175 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_175 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_175 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_178 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_178 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_178 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_178 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_183 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_183 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_183 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_183 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_184 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_184 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_184 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_184 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_191;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_191 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_191 = "TriggerEnteredA";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_192;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_192 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_192 = "TriggerEnteredB";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_193;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_193 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_193 = "QLStepped1";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_194;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_194 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_194 = "QLStepped2";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_195;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_195 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_195 = "QLStepped3";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_196 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_196;

	private bool logic_uScriptAct_SetBool_Out_196 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_196 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_196 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_198 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_198 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_198 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_198 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_199 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_199 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_199 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_199;

	private string logic_uScript_AddOnScreenMessage_tag_199 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_199;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_199;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_199;

	private bool logic_uScript_AddOnScreenMessage_Out_199 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_199 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_200 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_200;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_200 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_200;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_200 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_200 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_200 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_200 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_203 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_203 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_203 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_203 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_205 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_205 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_205 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_205 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_205 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_205 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_205 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_206;

	private bool logic_uScriptCon_CompareBool_True_206 = true;

	private bool logic_uScriptCon_CompareBool_False_206 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_207 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_207 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_209 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_213 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_213 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_214 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_214 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_214 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_214 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_216 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_216 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_216 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_216 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_219 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_219 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_219 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_219 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_221 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_221 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_221 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_221 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_223 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_223 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_223 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_223 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_225 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_225 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_225 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_225 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_227;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_227 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_227 = "TriggerEnteredC";

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_230 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_230;

	private bool logic_uScript_ClearEncounterTarget_Out_230 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_231 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_231 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_232 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_232 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_233 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_233 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_234 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_234 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_235 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_236 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_236 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_237 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_237 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_247 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_247 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_259 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_259 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_259 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_259 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_260 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_260 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_260 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_260;

	private string logic_uScript_AddOnScreenMessage_tag_260 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_260;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_260;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_260;

	private bool logic_uScript_AddOnScreenMessage_Out_260 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_260 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_261 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_261 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_262 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_262;

	private bool logic_uScriptAct_SetBool_Out_262 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_262 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_262 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_263 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_263 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_263 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_263 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_264 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_264 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_264 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_264 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_264 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_264 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_264 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_267 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_267 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_272 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_272 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_272 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_272 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_273 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_273;

	private bool logic_uScriptCon_CompareBool_True_273 = true;

	private bool logic_uScriptCon_CompareBool_False_273 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_276 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_276 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_278 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_278 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_278 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_278 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_281 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_281 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_281 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_281 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_283 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_283 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_283 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_283 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_285 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_285 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_285 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_285 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_286 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_286 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_286;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_286 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_286;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_286 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_286 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_286 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_286 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_292 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_292 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_292;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_292 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_292;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_292 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_292 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_292 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_292 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_296 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_296 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_296;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_296 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_296;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_296 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_296 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_296 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_296 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_299;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_299 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_299 = "TriggerEnteredD";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_300 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_300;

	private bool logic_uScriptCon_CompareBool_True_300 = true;

	private bool logic_uScriptCon_CompareBool_False_300 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_302 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_302 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_304 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_304;

	private bool logic_uScriptAct_SetBool_Out_304 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_304 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_304 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_305;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_305 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_305 = "AllTurretsDead";

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_308 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_308;

	private bool logic_uScript_GetPlayerTank_Returned_308 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_308 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_310 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_310 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_310 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_310 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_310 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_310 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_310 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_312 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_312 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_313 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_313 = true;

	private uScript_IsTechGrounded logic_uScript_IsTechGrounded_uScript_IsTechGrounded_315 = new uScript_IsTechGrounded();

	private Tank logic_uScript_IsTechGrounded_tech_315;

	private bool logic_uScript_IsTechGrounded_Out_315 = true;

	private bool logic_uScript_IsTechGrounded_True_315 = true;

	private bool logic_uScript_IsTechGrounded_False_315 = true;

	private uScript_IsTechGrounded logic_uScript_IsTechGrounded_uScript_IsTechGrounded_317 = new uScript_IsTechGrounded();

	private Tank logic_uScript_IsTechGrounded_tech_317;

	private bool logic_uScript_IsTechGrounded_Out_317 = true;

	private bool logic_uScript_IsTechGrounded_True_317 = true;

	private bool logic_uScript_IsTechGrounded_False_317 = true;

	private uScript_IsTechGrounded logic_uScript_IsTechGrounded_uScript_IsTechGrounded_319 = new uScript_IsTechGrounded();

	private Tank logic_uScript_IsTechGrounded_tech_319;

	private bool logic_uScript_IsTechGrounded_Out_319 = true;

	private bool logic_uScript_IsTechGrounded_True_319 = true;

	private bool logic_uScript_IsTechGrounded_False_319 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_320 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_320;

	private bool logic_uScriptCon_CompareBool_True_320 = true;

	private bool logic_uScriptCon_CompareBool_False_320 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_324 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_324 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_324 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_324;

	private string logic_uScript_AddOnScreenMessage_tag_324 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_324;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_324;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_324;

	private bool logic_uScript_AddOnScreenMessage_Out_324 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_324 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_325 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_325;

	private bool logic_uScriptAct_SetBool_Out_325 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_325 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_325 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_327;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_327 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_327 = "VehiclePurchased";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_328;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_328 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_328 = "HasEnoughMoney";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_332;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_332 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_332 = "SalesTechSetUp";

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_334 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_334;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_334 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_334 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_334;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_334;

	private bool logic_uScript_FlyTechUpAndAway_Out_334 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_335 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_335;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_335;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_335;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_335;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_335;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_335;

	private bool logic_uScript_MissionPromptBlock_Show_Out_335 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_336 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_336;

	private bool logic_uScriptAct_SetBool_Out_336 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_336 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_336 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_337 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_337;

	private bool logic_uScriptAct_SetBool_Out_337 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_337 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_337 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_339 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_339;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_339;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_339;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_339;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_339;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_339;

	private bool logic_uScript_MissionPromptBlock_Show_Out_339 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_345 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_345;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_345 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_346 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_346;

	private bool logic_uScriptAct_SetBool_Out_346 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_346 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_346 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_349 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_349;

	private bool logic_uScriptCon_CompareBool_True_349 = true;

	private bool logic_uScriptCon_CompareBool_False_349 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_352 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_352;

	private TankBlock logic_uScript_CompareBlock_B_352;

	private bool logic_uScript_CompareBlock_EqualTo_352 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_352 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_353 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_353;

	private bool logic_uScriptCon_CompareBool_True_353 = true;

	private bool logic_uScriptCon_CompareBool_False_353 = true;

	private uScript_DiscoverBlocks logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_355 = new uScript_DiscoverBlocks();

	private BlockTypes[] logic_uScript_DiscoverBlocks_blockTypes_355 = new BlockTypes[0];

	private bool logic_uScript_DiscoverBlocks_Out_355 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_357 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_357;

	private bool logic_uScriptCon_CompareBool_True_357 = true;

	private bool logic_uScriptCon_CompareBool_False_357 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_358 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_358;

	private bool logic_uScriptCon_CompareBool_True_358 = true;

	private bool logic_uScriptCon_CompareBool_False_358 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_360 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_360 = 7f;

	private bool logic_uScript_Wait_repeat_360;

	private bool logic_uScript_Wait_Waited_360 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_362 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_362;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_362 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_371 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_371 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_371;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_371 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_371 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_371 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_373 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_373;

	private int logic_uScriptCon_CompareInt_B_373;

	private bool logic_uScriptCon_CompareInt_GreaterThan_373 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_373 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_373 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_373 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_373 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_373 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_377 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_377;

	private bool logic_uScriptAct_SetBool_Out_377 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_377 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_377 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_378 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_378;

	private bool logic_uScriptCon_CompareBool_True_378 = true;

	private bool logic_uScriptCon_CompareBool_False_378 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_379 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_379;

	private bool logic_uScriptCon_CompareBool_True_379 = true;

	private bool logic_uScriptCon_CompareBool_False_379 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_382 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_382 = true;

	private uScript_IsTechInTrigger logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_383 = new uScript_IsTechInTrigger();

	private string logic_uScript_IsTechInTrigger_triggerAreaName_383 = "";

	private Tank[] logic_uScript_IsTechInTrigger_techs_383 = new Tank[0];

	private bool logic_uScript_IsTechInTrigger_Out_383 = true;

	private bool logic_uScript_IsTechInTrigger_InRange_383 = true;

	private bool logic_uScript_IsTechInTrigger_OutOfRange_383 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_384 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_384 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_384 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_384;

	private float logic_uScript_DamageTechs_leaveBlksPercent_384;

	private bool logic_uScript_DamageTechs_makeVulnerable_384;

	private bool logic_uScript_DamageTechs_Out_384 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_387 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_387 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_390 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_390 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_390 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_390;

	private string logic_uScript_AddOnScreenMessage_tag_390 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_390;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_390;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_390;

	private bool logic_uScript_AddOnScreenMessage_Out_390 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_390 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_392 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_392;

	private bool logic_uScriptAct_SetBool_Out_392 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_392 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_392 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_393 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_393;

	private bool logic_uScriptCon_CompareBool_True_393 = true;

	private bool logic_uScriptCon_CompareBool_False_393 = true;

	private uScript_GetPlayerTankWithBlock logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_396 = new uScript_GetPlayerTankWithBlock();

	private BlockTypes logic_uScript_GetPlayerTankWithBlock_block_396;

	private TankBlock logic_uScript_GetPlayerTankWithBlock_tankBlock_396;

	private bool logic_uScript_GetPlayerTankWithBlock_useBlockType_396;

	private Tank logic_uScript_GetPlayerTankWithBlock_Return_396;

	private bool logic_uScript_GetPlayerTankWithBlock_Returned_396 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_NotReturned_396 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_Out_396 = true;

	private uScript_GetPlayerTankWithBlock logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_398 = new uScript_GetPlayerTankWithBlock();

	private BlockTypes logic_uScript_GetPlayerTankWithBlock_block_398;

	private TankBlock logic_uScript_GetPlayerTankWithBlock_tankBlock_398;

	private bool logic_uScript_GetPlayerTankWithBlock_useBlockType_398;

	private Tank logic_uScript_GetPlayerTankWithBlock_Return_398;

	private bool logic_uScript_GetPlayerTankWithBlock_Returned_398 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_NotReturned_398 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_Out_398 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_400 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_400 = new Tank[0];

	private int logic_uScript_AccessListTech_index_400;

	private Tank logic_uScript_AccessListTech_value_400;

	private bool logic_uScript_AccessListTech_Out_400 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_406 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_406;

	private bool logic_uScriptAct_SetBool_Out_406 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_406 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_406 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_407 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_407 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_407;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_407;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_407;

	private bool logic_uScript_SpawnTechsFromData_Out_407 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_411 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_411 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_411;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_411 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_411;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_411 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_411 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_411 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_411 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_415 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_415;

	private BlockTypes logic_uScript_GetTankBlock_blockType_415;

	private TankBlock logic_uScript_GetTankBlock_Return_415;

	private bool logic_uScript_GetTankBlock_Out_415 = true;

	private bool logic_uScript_GetTankBlock_Returned_415 = true;

	private bool logic_uScript_GetTankBlock_NotFound_415 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_416 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_416;

	private bool logic_uScriptCon_CompareBool_True_416 = true;

	private bool logic_uScriptCon_CompareBool_False_416 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_417 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_417 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_417;

	private bool logic_uScript_SetTankInvulnerable_Out_417 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_418 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_418;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_418 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_418;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_418 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_422 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_422 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_422 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_422;

	private string logic_uScript_AddOnScreenMessage_tag_422 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_422;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_422;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_422;

	private bool logic_uScript_AddOnScreenMessage_Out_422 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_422 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_424 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_424 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_424 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_424 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_424 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_424 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_424 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_425 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_425 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_429 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_429 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_429;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_429 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_429;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_429 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_429 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_429 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_429 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_431 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_431 = new Tank[0];

	private int logic_uScript_AccessListTech_index_431;

	private Tank logic_uScript_AccessListTech_value_431;

	private bool logic_uScript_AccessListTech_Out_431 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_433 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_433 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_434 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_434 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_435 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_435 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_436 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_436;

	private bool logic_uScript_GetPlayerTank_Returned_436 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_436 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_442 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_442 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_442 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_442;

	private float logic_uScript_DamageTechs_leaveBlksPercent_442;

	private bool logic_uScript_DamageTechs_makeVulnerable_442;

	private bool logic_uScript_DamageTechs_Out_442 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_443 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_443 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_443 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_443;

	private string logic_uScript_AddOnScreenMessage_tag_443 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_443;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_443;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_443;

	private bool logic_uScript_AddOnScreenMessage_Out_443 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_443 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_444 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_444 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_445 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_445;

	private bool logic_uScript_GetPlayerTank_Returned_445 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_445 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_446 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_446 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_452 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_452 = new Tank[0];

	private int logic_uScript_AccessListTech_index_452;

	private Tank logic_uScript_AccessListTech_value_452;

	private bool logic_uScript_AccessListTech_Out_452 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_453 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_453;

	private BlockTypes logic_uScript_GetTankBlock_blockType_453;

	private TankBlock logic_uScript_GetTankBlock_Return_453;

	private bool logic_uScript_GetTankBlock_Out_453 = true;

	private bool logic_uScript_GetTankBlock_Returned_453 = true;

	private bool logic_uScript_GetTankBlock_NotFound_453 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_454 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_454 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_454;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_454 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_454;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_454 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_454 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_454 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_454 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_457 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_457 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_457 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_458 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_458 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_458;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_458 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_458;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_458 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_458 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_458 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_458 = true;

	private uScript_AddMoney logic_uScript_AddMoney_uScript_AddMoney_461 = new uScript_AddMoney();

	private int logic_uScript_AddMoney_amount_461;

	private bool logic_uScript_AddMoney_Out_461 = true;

	private uScriptAct_MultiplyInt_v2 logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_462 = new uScriptAct_MultiplyInt_v2();

	private int logic_uScriptAct_MultiplyInt_v2_A_462;

	private int logic_uScriptAct_MultiplyInt_v2_B_462 = -1;

	private int logic_uScriptAct_MultiplyInt_v2_IntResult_462;

	private float logic_uScriptAct_MultiplyInt_v2_FloatResult_462;

	private bool logic_uScriptAct_MultiplyInt_v2_Out_462 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_464 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_464 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_464;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_464 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_464;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_464 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_464 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_464 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_464 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_467 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_467 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_467;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_467 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_467 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_467 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_471 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_471 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_471;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_471 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_471;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_471 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_471 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_471 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_471 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_472 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_472 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_472;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_472 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_472 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_472 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_474 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_474 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_474;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_474 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_474 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_474 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_475 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_475 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_475;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_475 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_475;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_475 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_475 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_475 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_475 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_480 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_480 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_480;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_480 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_480;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_480 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_480 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_480 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_480 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_482 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_482 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_482;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_482 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_482 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_482 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_486 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_486 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_486 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_486;

	private string logic_uScript_AddOnScreenMessage_tag_486 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_486;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_486;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_486;

	private bool logic_uScript_AddOnScreenMessage_Out_486 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_486 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_487 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_487;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_487 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_487;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_487 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_487 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_487 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_487 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_491 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_491;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_491 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_491;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_491 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_496 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_496 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_496;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_496 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_496;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_496 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_496 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_496 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_496 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_500 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_500 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_500;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_500 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_500;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_500 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_500 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_500 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_500 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_502 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_502;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_502 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_502;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_502 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_504 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_504;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_504 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_504;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_504 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_505 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_505 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_506 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_506 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_507 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_507 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_508 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_508 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_509 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_509 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_510 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_510 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_511 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_511 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_514 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_514;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_514;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_514;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_514;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_514;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_514;

	private bool logic_uScript_MissionPromptBlock_Show_Out_514 = true;

	private uScript_CanSpawnPlayerTechsWithinBlockLimit logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_515 = new uScript_CanSpawnPlayerTechsWithinBlockLimit();

	private SpawnTechData[] logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_515 = new SpawnTechData[0];

	private int logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_515 = 1;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_Out_515 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_True_515 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_False_515 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_516 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_516 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_518 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_518 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_520 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_520;

	private bool logic_uScriptAct_SetBool_Out_520 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_520 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_520 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_521 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_521;

	private bool logic_uScriptAct_SetBool_Out_521 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_521 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_521 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_524;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_524 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_524 = "BlockLimitCritical";

	private uScript_IsTechWheelGrounded logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_525 = new uScript_IsTechWheelGrounded();

	private Tank logic_uScript_IsTechWheelGrounded_tech_525;

	private bool logic_uScript_IsTechWheelGrounded_Out_525 = true;

	private bool logic_uScript_IsTechWheelGrounded_True_525 = true;

	private bool logic_uScript_IsTechWheelGrounded_False_525 = true;

	private uScript_IsTechTouchingTerrain logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_526 = new uScript_IsTechTouchingTerrain();

	private Tank logic_uScript_IsTechTouchingTerrain_tech_526;

	private bool logic_uScript_IsTechTouchingTerrain_Out_526 = true;

	private bool logic_uScript_IsTechTouchingTerrain_True_526 = true;

	private bool logic_uScript_IsTechTouchingTerrain_False_526 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_528 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_528 = new Tank[0];

	private int logic_uScript_AccessListTech_index_528;

	private Tank logic_uScript_AccessListTech_value_528;

	private bool logic_uScript_AccessListTech_Out_528 = true;

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_530 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_530;

	private bool logic_uScript_IsTechPlayer_Out_530 = true;

	private bool logic_uScript_IsTechPlayer_True_530 = true;

	private bool logic_uScript_IsTechPlayer_False_530 = true;

	private uScript_IsTechWheelGrounded logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_531 = new uScript_IsTechWheelGrounded();

	private Tank logic_uScript_IsTechWheelGrounded_tech_531;

	private bool logic_uScript_IsTechWheelGrounded_Out_531 = true;

	private bool logic_uScript_IsTechWheelGrounded_True_531 = true;

	private bool logic_uScript_IsTechWheelGrounded_False_531 = true;

	private uScript_IsTechTouchingTerrain logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_532 = new uScript_IsTechTouchingTerrain();

	private Tank logic_uScript_IsTechTouchingTerrain_tech_532;

	private bool logic_uScript_IsTechTouchingTerrain_Out_532 = true;

	private bool logic_uScript_IsTechTouchingTerrain_True_532 = true;

	private bool logic_uScript_IsTechTouchingTerrain_False_532 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_533 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_533 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_534 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_534 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_536 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_536 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_537 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_537 = true;

	private uScript_IsTechAnchored logic_uScript_IsTechAnchored_uScript_IsTechAnchored_538 = new uScript_IsTechAnchored();

	private Tank logic_uScript_IsTechAnchored_tech_538;

	private bool logic_uScript_IsTechAnchored_Out_538 = true;

	private bool logic_uScript_IsTechAnchored_True_538 = true;

	private bool logic_uScript_IsTechAnchored_False_538 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_539 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_539 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_541;

	private bool logic_uScriptCon_CompareBool_True_541 = true;

	private bool logic_uScriptCon_CompareBool_False_541 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_545 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_545 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_545 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_545;

	private string logic_uScript_AddOnScreenMessage_tag_545 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_545;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_545;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_545;

	private bool logic_uScript_AddOnScreenMessage_Out_545 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_545 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_547 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_547 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_548 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_548 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_549 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_549 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_549;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_549;

	private bool logic_uScript_DestroyTechsFromData_Out_549 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_551 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_551;

	private bool logic_uScriptAct_SetBool_Out_551 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_551 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_551 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_553 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_553;

	private bool logic_uScriptAct_SetBool_Out_553 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_553 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_553 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_555 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_555;

	private bool logic_uScriptAct_SetBool_Out_555 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_555 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_555 = true;

	private uScriptAct_PrintText logic_uScriptAct_PrintText_uScriptAct_PrintText_556 = new uScriptAct_PrintText();

	private string logic_uScriptAct_PrintText_Text_556 = "";

	private int logic_uScriptAct_PrintText_FontSize_556 = 16;

	private FontStyle logic_uScriptAct_PrintText_FontStyle_556;

	private Color logic_uScriptAct_PrintText_FontColor_556 = new Color(0f, 0f, 0f, 1f);

	private TextAnchor logic_uScriptAct_PrintText_textAnchor_556;

	private int logic_uScriptAct_PrintText_EdgePadding_556 = 8;

	private float logic_uScriptAct_PrintText_time_556;

	private bool logic_uScriptAct_PrintText_Out_556 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_558 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_558 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_558 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_558 = "";

	private string logic_uScriptAct_Concatenate_Result_558;

	private bool logic_uScriptAct_Concatenate_Out_558 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_561 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_561 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_561 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_561 = "";

	private string logic_uScriptAct_Concatenate_Result_561;

	private bool logic_uScriptAct_Concatenate_Out_561 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_563 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_563 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_563 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_563 = "";

	private string logic_uScriptAct_Concatenate_Result_563;

	private bool logic_uScriptAct_Concatenate_Out_563 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_565 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_565 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_565 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_565 = "";

	private string logic_uScriptAct_Concatenate_Result_565;

	private bool logic_uScriptAct_Concatenate_Out_565 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_571 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_571;

	private bool logic_uScriptAct_SetBool_Out_571 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_571 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_571 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_572 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_572;

	private bool logic_uScriptAct_SetBool_Out_572 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_572 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_572 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_575 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_575;

	private bool logic_uScriptAct_SetBool_Out_575 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_575 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_575 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_577 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_577;

	private bool logic_uScriptAct_SetBool_Out_577 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_577 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_577 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_579 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_579;

	private bool logic_uScriptAct_SetBool_Out_579 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_579 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_579 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_581 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_581;

	private bool logic_uScriptAct_SetBool_Out_581 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_581 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_581 = true;

	private TankBlock event_UnityEngine_GameObject_TankBlock_361;

	private bool event_UnityEngine_GameObject_Accepted_361;

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
		}
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
			if (null != owner_Connection_11)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_11.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_11.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_9;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_9;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_9;
				}
			}
		}
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
		}
		if (null == owner_Connection_29 || !m_RegisteredForEvents)
		{
			owner_Connection_29 = parentGameObject;
		}
		if (null == owner_Connection_32 || !m_RegisteredForEvents)
		{
			owner_Connection_32 = parentGameObject;
		}
		if (null == owner_Connection_71 || !m_RegisteredForEvents)
		{
			owner_Connection_71 = parentGameObject;
		}
		if (null == owner_Connection_81 || !m_RegisteredForEvents)
		{
			owner_Connection_81 = parentGameObject;
		}
		if (null == owner_Connection_84 || !m_RegisteredForEvents)
		{
			owner_Connection_84 = parentGameObject;
		}
		if (null == owner_Connection_151 || !m_RegisteredForEvents)
		{
			owner_Connection_151 = parentGameObject;
		}
		if (null == owner_Connection_169 || !m_RegisteredForEvents)
		{
			owner_Connection_169 = parentGameObject;
		}
		if (null == owner_Connection_211 || !m_RegisteredForEvents)
		{
			owner_Connection_211 = parentGameObject;
		}
		if (null == owner_Connection_229 || !m_RegisteredForEvents)
		{
			owner_Connection_229 = parentGameObject;
		}
		if (null == owner_Connection_289 || !m_RegisteredForEvents)
		{
			owner_Connection_289 = parentGameObject;
		}
		if (null == owner_Connection_291 || !m_RegisteredForEvents)
		{
			owner_Connection_291 = parentGameObject;
		}
		if (null == owner_Connection_295 || !m_RegisteredForEvents)
		{
			owner_Connection_295 = parentGameObject;
		}
		if (null == owner_Connection_342 || !m_RegisteredForEvents)
		{
			owner_Connection_342 = parentGameObject;
		}
		if (null == owner_Connection_350 || !m_RegisteredForEvents)
		{
			owner_Connection_350 = parentGameObject;
			if (null != owner_Connection_350)
			{
				uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_350.GetComponent<uScript_MissionPromptBlock_OnResult>();
				if (null == uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2 = owner_Connection_350.AddComponent<uScript_MissionPromptBlock_OnResult>();
				}
				if (null != uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_361;
				}
			}
		}
		if (null == owner_Connection_404 || !m_RegisteredForEvents)
		{
			owner_Connection_404 = parentGameObject;
		}
		if (null == owner_Connection_410 || !m_RegisteredForEvents)
		{
			owner_Connection_410 = parentGameObject;
		}
		if (null == owner_Connection_419 || !m_RegisteredForEvents)
		{
			owner_Connection_419 = parentGameObject;
		}
		if (null == owner_Connection_430 || !m_RegisteredForEvents)
		{
			owner_Connection_430 = parentGameObject;
		}
		if (null == owner_Connection_455 || !m_RegisteredForEvents)
		{
			owner_Connection_455 = parentGameObject;
		}
		if (null == owner_Connection_463 || !m_RegisteredForEvents)
		{
			owner_Connection_463 = parentGameObject;
		}
		if (null == owner_Connection_465 || !m_RegisteredForEvents)
		{
			owner_Connection_465 = parentGameObject;
		}
		if (null == owner_Connection_468 || !m_RegisteredForEvents)
		{
			owner_Connection_468 = parentGameObject;
		}
		if (null == owner_Connection_470 || !m_RegisteredForEvents)
		{
			owner_Connection_470 = parentGameObject;
		}
		if (null == owner_Connection_473 || !m_RegisteredForEvents)
		{
			owner_Connection_473 = parentGameObject;
		}
		if (null == owner_Connection_477 || !m_RegisteredForEvents)
		{
			owner_Connection_477 = parentGameObject;
		}
		if (null == owner_Connection_478 || !m_RegisteredForEvents)
		{
			owner_Connection_478 = parentGameObject;
		}
		if (null == owner_Connection_479 || !m_RegisteredForEvents)
		{
			owner_Connection_479 = parentGameObject;
		}
		if (null == owner_Connection_481 || !m_RegisteredForEvents)
		{
			owner_Connection_481 = parentGameObject;
		}
		if (null == owner_Connection_489 || !m_RegisteredForEvents)
		{
			owner_Connection_489 = parentGameObject;
		}
		if (null == owner_Connection_492 || !m_RegisteredForEvents)
		{
			owner_Connection_492 = parentGameObject;
		}
		if (null == owner_Connection_494 || !m_RegisteredForEvents)
		{
			owner_Connection_494 = parentGameObject;
		}
		if (null == owner_Connection_497 || !m_RegisteredForEvents)
		{
			owner_Connection_497 = parentGameObject;
		}
		if (null == owner_Connection_501 || !m_RegisteredForEvents)
		{
			owner_Connection_501 = parentGameObject;
		}
		if (null == owner_Connection_503 || !m_RegisteredForEvents)
		{
			owner_Connection_503 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_11)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_11.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_11.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_9;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_9;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_9;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_350)
		{
			uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_350.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null == uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2 = owner_Connection_350.AddComponent<uScript_MissionPromptBlock_OnResult>();
			}
			if (null != uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_361;
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
		if (null != owner_Connection_11)
		{
			uScript_SaveLoad component2 = owner_Connection_11.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_9;
				component2.LoadEvent -= Instance_LoadEvent_9;
				component2.RestartEvent -= Instance_RestartEvent_9;
			}
		}
		if (null != owner_Connection_350)
		{
			uScript_MissionPromptBlock_OnResult component3 = owner_Connection_350.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null != component3)
			{
				component3.ResponseEvent -= Instance_ResponseEvent_361;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_2.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_14.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_19.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_21.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_26.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_30.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_31.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_34.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_36.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_37.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_45.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_48.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_57.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_61.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_62.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_70.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_72.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_74.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_79.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_80.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_85.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_89.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_94.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_95.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_99.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_100.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_102.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_109.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_110.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_112.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_113.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_115.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_120.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_129.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_133.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_134.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_136.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_137.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_138.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_139.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_142.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_143.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_146.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_147.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_154.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_155.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_159.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_160.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_162.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_163.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_164.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_165.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_172.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_173.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_175.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_178.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_183.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_184.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_196.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_198.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_199.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_203.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_205.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_207.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_213.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_214.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_216.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_219.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_221.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_223.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_225.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_230.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_231.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_232.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_233.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_234.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_236.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_237.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_247.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_259.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_260.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_261.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_262.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_263.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_264.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_267.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_272.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_273.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_276.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_278.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_281.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_283.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_285.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_286.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_292.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_296.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_300.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_302.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_304.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_308.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_310.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_312.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_313.SetParent(g);
		logic_uScript_IsTechGrounded_uScript_IsTechGrounded_315.SetParent(g);
		logic_uScript_IsTechGrounded_uScript_IsTechGrounded_317.SetParent(g);
		logic_uScript_IsTechGrounded_uScript_IsTechGrounded_319.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_320.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_324.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_325.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_334.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_335.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_336.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_337.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_339.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_345.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_346.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_349.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_352.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_353.SetParent(g);
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_355.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_357.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_358.SetParent(g);
		logic_uScript_Wait_uScript_Wait_360.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_362.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_371.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_373.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_377.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_378.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_379.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_382.SetParent(g);
		logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_383.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_384.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_387.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_390.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_392.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_393.SetParent(g);
		logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_396.SetParent(g);
		logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_398.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_400.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_406.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_407.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_411.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_415.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_416.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_417.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_418.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_422.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_424.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_425.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_429.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_431.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_433.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_434.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_435.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_436.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_442.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_443.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_444.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_445.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_446.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_452.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_453.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_454.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_457.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_458.SetParent(g);
		logic_uScript_AddMoney_uScript_AddMoney_461.SetParent(g);
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_462.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_464.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_467.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_471.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_472.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_474.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_475.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_480.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_482.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_486.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_491.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_496.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_500.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_502.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_504.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_505.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_506.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_507.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_508.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_509.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_510.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_511.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_514.SetParent(g);
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_515.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_516.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_518.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_520.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_521.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.SetParent(g);
		logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_525.SetParent(g);
		logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_526.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_528.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_530.SetParent(g);
		logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_531.SetParent(g);
		logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_532.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_533.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_534.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_536.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_537.SetParent(g);
		logic_uScript_IsTechAnchored_uScript_IsTechAnchored_538.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_539.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_545.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_547.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_548.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_549.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_551.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_553.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_555.SetParent(g);
		logic_uScriptAct_PrintText_uScriptAct_PrintText_556.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_558.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_561.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_563.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_565.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_571.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_572.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_575.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_577.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_579.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_581.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_29 = parentGameObject;
		owner_Connection_32 = parentGameObject;
		owner_Connection_71 = parentGameObject;
		owner_Connection_81 = parentGameObject;
		owner_Connection_84 = parentGameObject;
		owner_Connection_151 = parentGameObject;
		owner_Connection_169 = parentGameObject;
		owner_Connection_211 = parentGameObject;
		owner_Connection_229 = parentGameObject;
		owner_Connection_289 = parentGameObject;
		owner_Connection_291 = parentGameObject;
		owner_Connection_295 = parentGameObject;
		owner_Connection_342 = parentGameObject;
		owner_Connection_350 = parentGameObject;
		owner_Connection_404 = parentGameObject;
		owner_Connection_410 = parentGameObject;
		owner_Connection_419 = parentGameObject;
		owner_Connection_430 = parentGameObject;
		owner_Connection_455 = parentGameObject;
		owner_Connection_463 = parentGameObject;
		owner_Connection_465 = parentGameObject;
		owner_Connection_468 = parentGameObject;
		owner_Connection_470 = parentGameObject;
		owner_Connection_473 = parentGameObject;
		owner_Connection_477 = parentGameObject;
		owner_Connection_478 = parentGameObject;
		owner_Connection_479 = parentGameObject;
		owner_Connection_481 = parentGameObject;
		owner_Connection_489 = parentGameObject;
		owner_Connection_492 = parentGameObject;
		owner_Connection_494 = parentGameObject;
		owner_Connection_497 = parentGameObject;
		owner_Connection_501 = parentGameObject;
		owner_Connection_503 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_26.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_115.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save_Out += SubGraph_SaveLoadBool_Save_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load_Out += SubGraph_SaveLoadBool_Load_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Save_Out += SubGraph_SaveLoadBool_Save_Out_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Load_Out += SubGraph_SaveLoadBool_Load_Out_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_15;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_26.Out += SubGraph_LoadObjectiveStates_Out_26;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Save_Out += SubGraph_SaveLoadInt_Save_Out_27;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Load_Out += SubGraph_SaveLoadInt_Load_Out_27;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Save_Out += SubGraph_SaveLoadBool_Save_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Load_Out += SubGraph_SaveLoadBool_Load_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Save_Out += SubGraph_SaveLoadBool_Save_Out_67;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Load_Out += SubGraph_SaveLoadBool_Load_Out_67;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_67;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Save_Out += SubGraph_SaveLoadBool_Save_Out_68;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Load_Out += SubGraph_SaveLoadBool_Load_Out_68;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_68;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Save_Out += SubGraph_SaveLoadBool_Save_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Load_Out += SubGraph_SaveLoadBool_Load_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_69;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Out += SubGraph_CompleteObjectiveStage_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Save_Out += SubGraph_SaveLoadBool_Save_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Load_Out += SubGraph_SaveLoadBool_Load_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Save_Out += SubGraph_SaveLoadBool_Save_Out_107;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Load_Out += SubGraph_SaveLoadBool_Load_Out_107;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_107;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Save_Out += SubGraph_SaveLoadBool_Save_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Load_Out += SubGraph_SaveLoadBool_Load_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_108;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_115.Out += SubGraph_CompleteObjectiveStage_Out_115;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.Out += SubGraph_CompleteObjectiveStage_Out_123;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.Out += SubGraph_CompleteObjectiveStage_Out_131;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.Save_Out += SubGraph_SaveLoadInt_Save_Out_132;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.Load_Out += SubGraph_SaveLoadInt_Load_Out_132;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output1 += uScriptCon_ManualSwitch_Output1_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output2 += uScriptCon_ManualSwitch_Output2_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output3 += uScriptCon_ManualSwitch_Output3_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output4 += uScriptCon_ManualSwitch_Output4_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output5 += uScriptCon_ManualSwitch_Output5_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output6 += uScriptCon_ManualSwitch_Output6_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output7 += uScriptCon_ManualSwitch_Output7_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output8 += uScriptCon_ManualSwitch_Output8_135;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Save_Out += SubGraph_SaveLoadBool_Save_Out_191;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Load_Out += SubGraph_SaveLoadBool_Load_Out_191;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_191;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Save_Out += SubGraph_SaveLoadBool_Save_Out_192;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Load_Out += SubGraph_SaveLoadBool_Load_Out_192;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_192;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Save_Out += SubGraph_SaveLoadBool_Save_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Load_Out += SubGraph_SaveLoadBool_Load_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Save_Out += SubGraph_SaveLoadBool_Save_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Load_Out += SubGraph_SaveLoadBool_Load_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Save_Out += SubGraph_SaveLoadBool_Save_Out_195;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Load_Out += SubGraph_SaveLoadBool_Load_Out_195;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_195;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Save_Out += SubGraph_SaveLoadBool_Save_Out_227;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Load_Out += SubGraph_SaveLoadBool_Load_Out_227;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_227;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Save_Out += SubGraph_SaveLoadBool_Save_Out_299;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Load_Out += SubGraph_SaveLoadBool_Load_Out_299;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_299;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Save_Out += SubGraph_SaveLoadBool_Save_Out_305;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Load_Out += SubGraph_SaveLoadBool_Load_Out_305;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_305;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Save_Out += SubGraph_SaveLoadBool_Save_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Load_Out += SubGraph_SaveLoadBool_Load_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Save_Out += SubGraph_SaveLoadBool_Save_Out_328;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Load_Out += SubGraph_SaveLoadBool_Load_Out_328;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_328;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Save_Out += SubGraph_SaveLoadBool_Save_Out_332;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Load_Out += SubGraph_SaveLoadBool_Load_Out_332;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_332;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Save_Out += SubGraph_SaveLoadBool_Save_Out_524;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Load_Out += SubGraph_SaveLoadBool_Load_Out_524;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_524;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_26.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_115.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_26.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_70.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_115.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_334.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_26.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_37.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_45.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_57.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_115.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_143.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_159.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_199.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_260.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_324.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.OnDisable();
		logic_uScript_Wait_uScript_Wait_360.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_390.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_415.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_417.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_418.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_422.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_443.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_453.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_457.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_486.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_491.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_502.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_504.OnDisable();
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_515.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.OnDisable();
		logic_uScript_IsTechAnchored_uScript_IsTechAnchored_538.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_545.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_26.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_115.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_26.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_115.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save_Out -= SubGraph_SaveLoadBool_Save_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load_Out -= SubGraph_SaveLoadBool_Load_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Save_Out -= SubGraph_SaveLoadBool_Save_Out_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Load_Out -= SubGraph_SaveLoadBool_Load_Out_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_15;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_26.Out -= SubGraph_LoadObjectiveStates_Out_26;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Save_Out -= SubGraph_SaveLoadInt_Save_Out_27;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Load_Out -= SubGraph_SaveLoadInt_Load_Out_27;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Save_Out -= SubGraph_SaveLoadBool_Save_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Load_Out -= SubGraph_SaveLoadBool_Load_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Save_Out -= SubGraph_SaveLoadBool_Save_Out_67;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Load_Out -= SubGraph_SaveLoadBool_Load_Out_67;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_67;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Save_Out -= SubGraph_SaveLoadBool_Save_Out_68;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Load_Out -= SubGraph_SaveLoadBool_Load_Out_68;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_68;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Save_Out -= SubGraph_SaveLoadBool_Save_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Load_Out -= SubGraph_SaveLoadBool_Load_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_69;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Out -= SubGraph_CompleteObjectiveStage_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Save_Out -= SubGraph_SaveLoadBool_Save_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Load_Out -= SubGraph_SaveLoadBool_Load_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Save_Out -= SubGraph_SaveLoadBool_Save_Out_107;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Load_Out -= SubGraph_SaveLoadBool_Load_Out_107;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_107;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Save_Out -= SubGraph_SaveLoadBool_Save_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Load_Out -= SubGraph_SaveLoadBool_Load_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_108;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_115.Out -= SubGraph_CompleteObjectiveStage_Out_115;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.Out -= SubGraph_CompleteObjectiveStage_Out_123;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.Out -= SubGraph_CompleteObjectiveStage_Out_131;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.Save_Out -= SubGraph_SaveLoadInt_Save_Out_132;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.Load_Out -= SubGraph_SaveLoadInt_Load_Out_132;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output1 -= uScriptCon_ManualSwitch_Output1_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output2 -= uScriptCon_ManualSwitch_Output2_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output3 -= uScriptCon_ManualSwitch_Output3_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output4 -= uScriptCon_ManualSwitch_Output4_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output5 -= uScriptCon_ManualSwitch_Output5_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output6 -= uScriptCon_ManualSwitch_Output6_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output7 -= uScriptCon_ManualSwitch_Output7_135;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.Output8 -= uScriptCon_ManualSwitch_Output8_135;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Save_Out -= SubGraph_SaveLoadBool_Save_Out_191;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Load_Out -= SubGraph_SaveLoadBool_Load_Out_191;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_191;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Save_Out -= SubGraph_SaveLoadBool_Save_Out_192;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Load_Out -= SubGraph_SaveLoadBool_Load_Out_192;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_192;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Save_Out -= SubGraph_SaveLoadBool_Save_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Load_Out -= SubGraph_SaveLoadBool_Load_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Save_Out -= SubGraph_SaveLoadBool_Save_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Load_Out -= SubGraph_SaveLoadBool_Load_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Save_Out -= SubGraph_SaveLoadBool_Save_Out_195;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Load_Out -= SubGraph_SaveLoadBool_Load_Out_195;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_195;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Save_Out -= SubGraph_SaveLoadBool_Save_Out_227;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Load_Out -= SubGraph_SaveLoadBool_Load_Out_227;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_227;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Save_Out -= SubGraph_SaveLoadBool_Save_Out_299;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Load_Out -= SubGraph_SaveLoadBool_Load_Out_299;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_299;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Save_Out -= SubGraph_SaveLoadBool_Save_Out_305;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Load_Out -= SubGraph_SaveLoadBool_Load_Out_305;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_305;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Save_Out -= SubGraph_SaveLoadBool_Save_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Load_Out -= SubGraph_SaveLoadBool_Load_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Save_Out -= SubGraph_SaveLoadBool_Save_Out_328;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Load_Out -= SubGraph_SaveLoadBool_Load_Out_328;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_328;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Save_Out -= SubGraph_SaveLoadBool_Save_Out_332;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Load_Out -= SubGraph_SaveLoadBool_Load_Out_332;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_332;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Save_Out -= SubGraph_SaveLoadBool_Save_Out_524;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Load_Out -= SubGraph_SaveLoadBool_Load_Out_524;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_524;
	}

	public void OnGUI()
	{
		logic_uScriptAct_PrintText_uScriptAct_PrintText_556.OnGUI();
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

	private void Instance_SaveEvent_9(object o, EventArgs e)
	{
		Relay_SaveEvent_9();
	}

	private void Instance_LoadEvent_9(object o, EventArgs e)
	{
		Relay_LoadEvent_9();
	}

	private void Instance_RestartEvent_9(object o, EventArgs e)
	{
		Relay_RestartEvent_9();
	}

	private void Instance_ResponseEvent_361(object o, uScript_MissionPromptBlock_OnResult.PromptResultEventArgs e)
	{
		event_UnityEngine_GameObject_TankBlock_361 = e.TankBlock;
		event_UnityEngine_GameObject_Accepted_361 = e.Accepted;
		Relay_ResponseEvent_361();
	}

	private void SubGraph_SaveLoadBool_Save_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Save_Out_12();
	}

	private void SubGraph_SaveLoadBool_Load_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Load_Out_12();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Restart_Out_12();
	}

	private void SubGraph_SaveLoadBool_Save_Out_15(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_15;
		Relay_Save_Out_15();
	}

	private void SubGraph_SaveLoadBool_Load_Out_15(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_15;
		Relay_Load_Out_15();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_15(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_15;
		Relay_Restart_Out_15();
	}

	private void SubGraph_LoadObjectiveStates_Out_26(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_26();
	}

	private void SubGraph_SaveLoadInt_Save_Out_27(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_27 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_27;
		Relay_Save_Out_27();
	}

	private void SubGraph_SaveLoadInt_Load_Out_27(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_27 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_27;
		Relay_Load_Out_27();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_27(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_27 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_27;
		Relay_Restart_Out_27();
	}

	private void SubGraph_SaveLoadBool_Save_Out_66(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = e.boolean;
		local_MsgArrivedAtMission_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_66;
		Relay_Save_Out_66();
	}

	private void SubGraph_SaveLoadBool_Load_Out_66(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = e.boolean;
		local_MsgArrivedAtMission_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_66;
		Relay_Load_Out_66();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_66(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = e.boolean;
		local_MsgArrivedAtMission_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_66;
		Relay_Restart_Out_66();
	}

	private void SubGraph_SaveLoadBool_Save_Out_67(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = e.boolean;
		local_MsgFirstTurretDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_67;
		Relay_Save_Out_67();
	}

	private void SubGraph_SaveLoadBool_Load_Out_67(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = e.boolean;
		local_MsgFirstTurretDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_67;
		Relay_Load_Out_67();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_67(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = e.boolean;
		local_MsgFirstTurretDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_67;
		Relay_Restart_Out_67();
	}

	private void SubGraph_SaveLoadBool_Save_Out_68(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = e.boolean;
		local_MsgSecondTurretDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_68;
		Relay_Save_Out_68();
	}

	private void SubGraph_SaveLoadBool_Load_Out_68(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = e.boolean;
		local_MsgSecondTurretDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_68;
		Relay_Load_Out_68();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_68(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = e.boolean;
		local_MsgSecondTurretDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_68;
		Relay_Restart_Out_68();
	}

	private void SubGraph_SaveLoadBool_Save_Out_69(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = e.boolean;
		local_MsgThirdTurretDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_69;
		Relay_Save_Out_69();
	}

	private void SubGraph_SaveLoadBool_Load_Out_69(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = e.boolean;
		local_MsgThirdTurretDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_69;
		Relay_Load_Out_69();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_69(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = e.boolean;
		local_MsgThirdTurretDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_69;
		Relay_Restart_Out_69();
	}

	private void SubGraph_CompleteObjectiveStage_Out_96(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_96 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_96;
		Relay_Out_96();
	}

	private void SubGraph_SaveLoadBool_Save_Out_106(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = e.boolean;
		local_TurretMarkedDead1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_106;
		Relay_Save_Out_106();
	}

	private void SubGraph_SaveLoadBool_Load_Out_106(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = e.boolean;
		local_TurretMarkedDead1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_106;
		Relay_Load_Out_106();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_106(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = e.boolean;
		local_TurretMarkedDead1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_106;
		Relay_Restart_Out_106();
	}

	private void SubGraph_SaveLoadBool_Save_Out_107(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_107 = e.boolean;
		local_TurretMarkedDead2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_107;
		Relay_Save_Out_107();
	}

	private void SubGraph_SaveLoadBool_Load_Out_107(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_107 = e.boolean;
		local_TurretMarkedDead2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_107;
		Relay_Load_Out_107();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_107(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_107 = e.boolean;
		local_TurretMarkedDead2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_107;
		Relay_Restart_Out_107();
	}

	private void SubGraph_SaveLoadBool_Save_Out_108(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = e.boolean;
		local_TurretMarkedDead3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_108;
		Relay_Save_Out_108();
	}

	private void SubGraph_SaveLoadBool_Load_Out_108(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = e.boolean;
		local_TurretMarkedDead3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_108;
		Relay_Load_Out_108();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_108(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = e.boolean;
		local_TurretMarkedDead3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_108;
		Relay_Restart_Out_108();
	}

	private void SubGraph_CompleteObjectiveStage_Out_115(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_115 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_115;
		Relay_Out_115();
	}

	private void SubGraph_CompleteObjectiveStage_Out_123(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_123 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_123;
		Relay_Out_123();
	}

	private void SubGraph_CompleteObjectiveStage_Out_131(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_131 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_131;
		Relay_Out_131();
	}

	private void SubGraph_SaveLoadInt_Save_Out_132(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_132 = e.integer;
		local_TurretsDead_System_Int32 = logic_SubGraph_SaveLoadInt_integer_132;
		Relay_Save_Out_132();
	}

	private void SubGraph_SaveLoadInt_Load_Out_132(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_132 = e.integer;
		local_TurretsDead_System_Int32 = logic_SubGraph_SaveLoadInt_integer_132;
		Relay_Load_Out_132();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_132(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_132 = e.integer;
		local_TurretsDead_System_Int32 = logic_SubGraph_SaveLoadInt_integer_132;
		Relay_Restart_Out_132();
	}

	private void uScriptCon_ManualSwitch_Output1_135(object o, EventArgs e)
	{
		Relay_Output1_135();
	}

	private void uScriptCon_ManualSwitch_Output2_135(object o, EventArgs e)
	{
		Relay_Output2_135();
	}

	private void uScriptCon_ManualSwitch_Output3_135(object o, EventArgs e)
	{
		Relay_Output3_135();
	}

	private void uScriptCon_ManualSwitch_Output4_135(object o, EventArgs e)
	{
		Relay_Output4_135();
	}

	private void uScriptCon_ManualSwitch_Output5_135(object o, EventArgs e)
	{
		Relay_Output5_135();
	}

	private void uScriptCon_ManualSwitch_Output6_135(object o, EventArgs e)
	{
		Relay_Output6_135();
	}

	private void uScriptCon_ManualSwitch_Output7_135(object o, EventArgs e)
	{
		Relay_Output7_135();
	}

	private void uScriptCon_ManualSwitch_Output8_135(object o, EventArgs e)
	{
		Relay_Output8_135();
	}

	private void SubGraph_SaveLoadBool_Save_Out_191(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_191 = e.boolean;
		local_TriggerEnteredA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_191;
		Relay_Save_Out_191();
	}

	private void SubGraph_SaveLoadBool_Load_Out_191(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_191 = e.boolean;
		local_TriggerEnteredA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_191;
		Relay_Load_Out_191();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_191(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_191 = e.boolean;
		local_TriggerEnteredA_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_191;
		Relay_Restart_Out_191();
	}

	private void SubGraph_SaveLoadBool_Save_Out_192(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = e.boolean;
		local_TriggerEnteredB_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_192;
		Relay_Save_Out_192();
	}

	private void SubGraph_SaveLoadBool_Load_Out_192(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = e.boolean;
		local_TriggerEnteredB_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_192;
		Relay_Load_Out_192();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_192(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = e.boolean;
		local_TriggerEnteredB_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_192;
		Relay_Restart_Out_192();
	}

	private void SubGraph_SaveLoadBool_Save_Out_193(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = e.boolean;
		local_QLStepped1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_193;
		Relay_Save_Out_193();
	}

	private void SubGraph_SaveLoadBool_Load_Out_193(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = e.boolean;
		local_QLStepped1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_193;
		Relay_Load_Out_193();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_193(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = e.boolean;
		local_QLStepped1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_193;
		Relay_Restart_Out_193();
	}

	private void SubGraph_SaveLoadBool_Save_Out_194(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = e.boolean;
		local_QLStepped2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_194;
		Relay_Save_Out_194();
	}

	private void SubGraph_SaveLoadBool_Load_Out_194(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = e.boolean;
		local_QLStepped2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_194;
		Relay_Load_Out_194();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_194(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = e.boolean;
		local_QLStepped2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_194;
		Relay_Restart_Out_194();
	}

	private void SubGraph_SaveLoadBool_Save_Out_195(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_195 = e.boolean;
		local_QLStepped3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_195;
		Relay_Save_Out_195();
	}

	private void SubGraph_SaveLoadBool_Load_Out_195(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_195 = e.boolean;
		local_QLStepped3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_195;
		Relay_Load_Out_195();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_195(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_195 = e.boolean;
		local_QLStepped3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_195;
		Relay_Restart_Out_195();
	}

	private void SubGraph_SaveLoadBool_Save_Out_227(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = e.boolean;
		local_TriggerEnteredC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_227;
		Relay_Save_Out_227();
	}

	private void SubGraph_SaveLoadBool_Load_Out_227(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = e.boolean;
		local_TriggerEnteredC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_227;
		Relay_Load_Out_227();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_227(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = e.boolean;
		local_TriggerEnteredC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_227;
		Relay_Restart_Out_227();
	}

	private void SubGraph_SaveLoadBool_Save_Out_299(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = e.boolean;
		local_TriggerEnteredD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_299;
		Relay_Save_Out_299();
	}

	private void SubGraph_SaveLoadBool_Load_Out_299(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = e.boolean;
		local_TriggerEnteredD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_299;
		Relay_Load_Out_299();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_299(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = e.boolean;
		local_TriggerEnteredD_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_299;
		Relay_Restart_Out_299();
	}

	private void SubGraph_SaveLoadBool_Save_Out_305(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_305 = e.boolean;
		local_AllTurretsDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_305;
		Relay_Save_Out_305();
	}

	private void SubGraph_SaveLoadBool_Load_Out_305(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_305 = e.boolean;
		local_AllTurretsDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_305;
		Relay_Load_Out_305();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_305(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_305 = e.boolean;
		local_AllTurretsDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_305;
		Relay_Restart_Out_305();
	}

	private void SubGraph_SaveLoadBool_Save_Out_327(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_327;
		Relay_Save_Out_327();
	}

	private void SubGraph_SaveLoadBool_Load_Out_327(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_327;
		Relay_Load_Out_327();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_327(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_327;
		Relay_Restart_Out_327();
	}

	private void SubGraph_SaveLoadBool_Save_Out_328(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_328;
		Relay_Save_Out_328();
	}

	private void SubGraph_SaveLoadBool_Load_Out_328(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_328;
		Relay_Load_Out_328();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_328(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_328;
		Relay_Restart_Out_328();
	}

	private void SubGraph_SaveLoadBool_Save_Out_332(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_332 = e.boolean;
		local_SalesTechSetUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_332;
		Relay_Save_Out_332();
	}

	private void SubGraph_SaveLoadBool_Load_Out_332(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_332 = e.boolean;
		local_SalesTechSetUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_332;
		Relay_Load_Out_332();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_332(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_332 = e.boolean;
		local_SalesTechSetUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_332;
		Relay_Restart_Out_332();
	}

	private void SubGraph_SaveLoadBool_Save_Out_524(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = e.boolean;
		local_BlockLimitCritical_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_524;
		Relay_Save_Out_524();
	}

	private void SubGraph_SaveLoadBool_Load_Out_524(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = e.boolean;
		local_BlockLimitCritical_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_524;
		Relay_Load_Out_524();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_524(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = e.boolean;
		local_BlockLimitCritical_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_524;
		Relay_Restart_Out_524();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_18();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Succeed_2()
	{
		logic_uScript_FinishEncounter_owner_2 = owner_Connection_3;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_2.Succeed(logic_uScript_FinishEncounter_owner_2);
	}

	private void Relay_Fail_2()
	{
		logic_uScript_FinishEncounter_owner_2 = owner_Connection_3;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_2.Fail(logic_uScript_FinishEncounter_owner_2);
	}

	private void Relay_In_4()
	{
		int num = 0;
		Array msgMissionComplete = MsgMissionComplete;
		if (logic_uScript_AddOnScreenMessage_locString_4.Length != num + msgMissionComplete.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_4, num + msgMissionComplete.Length);
		}
		Array.Copy(msgMissionComplete, 0, logic_uScript_AddOnScreenMessage_locString_4, num, msgMissionComplete.Length);
		num += msgMissionComplete.Length;
		logic_uScript_AddOnScreenMessage_speaker_4 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_4 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.In(logic_uScript_AddOnScreenMessage_locString_4, logic_uScript_AddOnScreenMessage_msgPriority_4, logic_uScript_AddOnScreenMessage_holdMsg_4, logic_uScript_AddOnScreenMessage_tag_4, logic_uScript_AddOnScreenMessage_speaker_4, logic_uScript_AddOnScreenMessage_side_4);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.Out)
		{
			Relay_In_230();
		}
	}

	private void Relay_SaveEvent_9()
	{
		Relay_Save_132();
	}

	private void Relay_LoadEvent_9()
	{
		Relay_Load_132();
	}

	private void Relay_RestartEvent_9()
	{
		Relay_Restart_132();
	}

	private void Relay_Save_Out_12()
	{
		Relay_Save_66();
	}

	private void Relay_Load_Out_12()
	{
		Relay_Load_66();
	}

	private void Relay_Restart_Out_12()
	{
		Relay_Set_False_66();
	}

	private void Relay_Save_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Load_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Set_True_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Set_False_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_In_13()
	{
		logic_uScriptCon_CompareBool_Bool_13 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.In(logic_uScriptCon_CompareBool_Bool_13);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.False;
		if (num)
		{
			Relay_In_424();
		}
		if (flag)
		{
			Relay_In_70();
		}
	}

	private void Relay_In_14()
	{
		int num = 0;
		Array turretData = TurretData1;
		if (logic_uScript_GetAndCheckTechs_techData_14.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_14, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_GetAndCheckTechs_techData_14, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_14 = owner_Connection_8;
		int num2 = 0;
		Array array = local_Turrets1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_14.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_14, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_14, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_14 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_14.In(logic_uScript_GetAndCheckTechs_techData_14, logic_uScript_GetAndCheckTechs_ownerNode_14, ref logic_uScript_GetAndCheckTechs_techs_14);
		local_Turrets1_TankArray = logic_uScript_GetAndCheckTechs_techs_14;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_14.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_14.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_14.AllDead;
		if (allAlive)
		{
			Relay_In_90();
		}
		if (someAlive)
		{
			Relay_In_90();
		}
		if (allDead)
		{
			Relay_In_109();
		}
	}

	private void Relay_Save_Out_15()
	{
		Relay_Save_12();
	}

	private void Relay_Load_Out_15()
	{
		Relay_Load_12();
	}

	private void Relay_Restart_Out_15()
	{
		Relay_Set_False_12();
	}

	private void Relay_Save_15()
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_15 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Save(ref logic_SubGraph_SaveLoadBool_boolean_15, logic_SubGraph_SaveLoadBool_boolAsVariable_15, logic_SubGraph_SaveLoadBool_uniqueID_15);
	}

	private void Relay_Load_15()
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_15 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Load(ref logic_SubGraph_SaveLoadBool_boolean_15, logic_SubGraph_SaveLoadBool_boolAsVariable_15, logic_SubGraph_SaveLoadBool_uniqueID_15);
	}

	private void Relay_Set_True_15()
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_15 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_15, logic_SubGraph_SaveLoadBool_boolAsVariable_15, logic_SubGraph_SaveLoadBool_uniqueID_15);
	}

	private void Relay_Set_False_15()
	{
		logic_SubGraph_SaveLoadBool_boolean_15 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_15 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_15.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_15, logic_SubGraph_SaveLoadBool_boolAsVariable_15, logic_SubGraph_SaveLoadBool_uniqueID_15);
	}

	private void Relay_In_18()
	{
		logic_uScriptCon_CompareBool_Bool_18 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.In(logic_uScriptCon_CompareBool_Bool_18);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.False;
		if (num)
		{
			Relay_In_445();
		}
		if (flag)
		{
			Relay_In_383();
		}
	}

	private void Relay_True_19()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_19.True(out logic_uScriptAct_SetBool_Target_19);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_19;
	}

	private void Relay_False_19()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_19.False(out logic_uScriptAct_SetBool_Target_19);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_19;
	}

	private void Relay_InitialSpawn_21()
	{
		int num = 0;
		Array turretData = TurretData2;
		if (logic_uScript_SpawnTechsFromData_spawnData_21.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_21, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_SpawnTechsFromData_spawnData_21, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_21 = owner_Connection_22;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_21.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_21, logic_uScript_SpawnTechsFromData_ownerNode_21, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_21, logic_uScript_SpawnTechsFromData_allowResurrection_21);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_21.Out)
		{
			Relay_InitialSpawn_30();
		}
	}

	private void Relay_Out_26()
	{
		Relay_Load_15();
	}

	private void Relay_In_26()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_26 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_26.In(logic_SubGraph_LoadObjectiveStates_currentObjective_26);
	}

	private void Relay_Save_Out_27()
	{
		Relay_In_134();
	}

	private void Relay_Load_Out_27()
	{
		Relay_In_26();
	}

	private void Relay_Restart_Out_27()
	{
		Relay_In_133();
	}

	private void Relay_Save_27()
	{
		logic_SubGraph_SaveLoadInt_integer_27 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_27 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Save(logic_SubGraph_SaveLoadInt_restartValue_27, ref logic_SubGraph_SaveLoadInt_integer_27, logic_SubGraph_SaveLoadInt_intAsVariable_27, logic_SubGraph_SaveLoadInt_uniqueID_27);
	}

	private void Relay_Load_27()
	{
		logic_SubGraph_SaveLoadInt_integer_27 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_27 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Load(logic_SubGraph_SaveLoadInt_restartValue_27, ref logic_SubGraph_SaveLoadInt_integer_27, logic_SubGraph_SaveLoadInt_intAsVariable_27, logic_SubGraph_SaveLoadInt_uniqueID_27);
	}

	private void Relay_Restart_27()
	{
		logic_SubGraph_SaveLoadInt_integer_27 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_27 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_27.Restart(logic_SubGraph_SaveLoadInt_restartValue_27, ref logic_SubGraph_SaveLoadInt_integer_27, logic_SubGraph_SaveLoadInt_intAsVariable_27, logic_SubGraph_SaveLoadInt_uniqueID_27);
	}

	private void Relay_InitialSpawn_30()
	{
		int num = 0;
		Array turretData = TurretData3;
		if (logic_uScript_SpawnTechsFromData_spawnData_30.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_30, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_SpawnTechsFromData_spawnData_30, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_30 = owner_Connection_29;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_30.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_30, logic_uScript_SpawnTechsFromData_ownerNode_30, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_30, logic_uScript_SpawnTechsFromData_allowResurrection_30);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_30.Out)
		{
			Relay_InitialSpawn_407();
		}
	}

	private void Relay_InitialSpawn_31()
	{
		int num = 0;
		Array turretData = TurretData1;
		if (logic_uScript_SpawnTechsFromData_spawnData_31.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_31, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_SpawnTechsFromData_spawnData_31, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_31 = owner_Connection_32;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_31.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_31, logic_uScript_SpawnTechsFromData_ownerNode_31, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_31, logic_uScript_SpawnTechsFromData_allowResurrection_31);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_31.Out)
		{
			Relay_InitialSpawn_21();
		}
	}

	private void Relay_In_34()
	{
		logic_uScriptCon_CompareBool_Bool_34 = local_MsgArrivedAtMission_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_34.In(logic_uScriptCon_CompareBool_Bool_34);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_34.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_34.False;
		if (num)
		{
			Relay_True_36();
		}
		if (flag)
		{
			Relay_In_37();
		}
	}

	private void Relay_True_36()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_36.True(out logic_uScriptAct_SetBool_Target_36);
		local_MsgArrivedAtMission_System_Boolean = logic_uScriptAct_SetBool_Target_36;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_36.Out)
		{
			Relay_In_487();
		}
	}

	private void Relay_False_36()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_36.False(out logic_uScriptAct_SetBool_Target_36);
		local_MsgArrivedAtMission_System_Boolean = logic_uScriptAct_SetBool_Target_36;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_36.Out)
		{
			Relay_In_487();
		}
	}

	private void Relay_In_37()
	{
		int num = 0;
		Array msgArrivedAtMission = MsgArrivedAtMission;
		if (logic_uScript_AddOnScreenMessage_locString_37.Length != num + msgArrivedAtMission.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_37, num + msgArrivedAtMission.Length);
		}
		Array.Copy(msgArrivedAtMission, 0, logic_uScript_AddOnScreenMessage_locString_37, num, msgArrivedAtMission.Length);
		num += msgArrivedAtMission.Length;
		logic_uScript_AddOnScreenMessage_tag_37 = local_MsgTriggerC_System_String;
		logic_uScript_AddOnScreenMessage_speaker_37 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_37 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_37.In(logic_uScript_AddOnScreenMessage_locString_37, logic_uScript_AddOnScreenMessage_msgPriority_37, logic_uScript_AddOnScreenMessage_holdMsg_37, logic_uScript_AddOnScreenMessage_tag_37, logic_uScript_AddOnScreenMessage_speaker_37, logic_uScript_AddOnScreenMessage_side_37);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_37.Shown)
		{
			Relay_In_96();
		}
	}

	private void Relay_In_41()
	{
		logic_uScriptCon_CompareBool_Bool_41 = local_MsgFirstTurretDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41.In(logic_uScriptCon_CompareBool_Bool_41);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41.False;
		if (num)
		{
			Relay_True_44();
		}
		if (flag)
		{
			Relay_In_136();
		}
	}

	private void Relay_True_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.True(out logic_uScriptAct_SetBool_Target_44);
		local_MsgFirstTurretDead_System_Boolean = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_236();
		}
	}

	private void Relay_False_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.False(out logic_uScriptAct_SetBool_Target_44);
		local_MsgFirstTurretDead_System_Boolean = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_236();
		}
	}

	private void Relay_In_45()
	{
		int num = 0;
		Array msgFirstTurretDead = MsgFirstTurretDead;
		if (logic_uScript_AddOnScreenMessage_locString_45.Length != num + msgFirstTurretDead.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_45, num + msgFirstTurretDead.Length);
		}
		Array.Copy(msgFirstTurretDead, 0, logic_uScript_AddOnScreenMessage_locString_45, num, msgFirstTurretDead.Length);
		num += msgFirstTurretDead.Length;
		logic_uScript_AddOnScreenMessage_tag_45 = local_MsgDead_System_String;
		logic_uScript_AddOnScreenMessage_speaker_45 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_45 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_45.In(logic_uScript_AddOnScreenMessage_locString_45, logic_uScript_AddOnScreenMessage_msgPriority_45, logic_uScript_AddOnScreenMessage_holdMsg_45, logic_uScript_AddOnScreenMessage_tag_45, logic_uScript_AddOnScreenMessage_speaker_45, logic_uScript_AddOnScreenMessage_side_45);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_45.Out)
		{
			Relay_True_44();
		}
	}

	private void Relay_In_46()
	{
		logic_uScriptCon_CompareBool_Bool_46 = local_MsgSecondTurretDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.In(logic_uScriptCon_CompareBool_Bool_46);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.False;
		if (num)
		{
			Relay_True_48();
		}
		if (flag)
		{
			Relay_In_137();
		}
	}

	private void Relay_True_48()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_48.True(out logic_uScriptAct_SetBool_Target_48);
		local_MsgSecondTurretDead_System_Boolean = logic_uScriptAct_SetBool_Target_48;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_48.Out)
		{
			Relay_In_236();
		}
	}

	private void Relay_False_48()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_48.False(out logic_uScriptAct_SetBool_Target_48);
		local_MsgSecondTurretDead_System_Boolean = logic_uScriptAct_SetBool_Target_48;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_48.Out)
		{
			Relay_In_236();
		}
	}

	private void Relay_In_50()
	{
		int num = 0;
		Array msgSecondTurretDead = MsgSecondTurretDead;
		if (logic_uScript_AddOnScreenMessage_locString_50.Length != num + msgSecondTurretDead.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_50, num + msgSecondTurretDead.Length);
		}
		Array.Copy(msgSecondTurretDead, 0, logic_uScript_AddOnScreenMessage_locString_50, num, msgSecondTurretDead.Length);
		num += msgSecondTurretDead.Length;
		logic_uScript_AddOnScreenMessage_tag_50 = local_MsgDead_System_String;
		logic_uScript_AddOnScreenMessage_speaker_50 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_50 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50.In(logic_uScript_AddOnScreenMessage_locString_50, logic_uScript_AddOnScreenMessage_msgPriority_50, logic_uScript_AddOnScreenMessage_holdMsg_50, logic_uScript_AddOnScreenMessage_tag_50, logic_uScript_AddOnScreenMessage_speaker_50, logic_uScript_AddOnScreenMessage_side_50);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_50.Out)
		{
			Relay_True_48();
		}
	}

	private void Relay_True_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.True(out logic_uScriptAct_SetBool_Target_53);
		local_MsgThirdTurretDead_System_Boolean = logic_uScriptAct_SetBool_Target_53;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_53.Out)
		{
			Relay_True_60();
		}
	}

	private void Relay_False_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.False(out logic_uScriptAct_SetBool_Target_53);
		local_MsgThirdTurretDead_System_Boolean = logic_uScriptAct_SetBool_Target_53;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_53.Out)
		{
			Relay_True_60();
		}
	}

	private void Relay_In_55()
	{
		logic_uScriptCon_CompareBool_Bool_55 = local_MsgThirdTurretDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.In(logic_uScriptCon_CompareBool_Bool_55);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.False;
		if (num)
		{
			Relay_True_60();
		}
		if (flag)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_57()
	{
		int num = 0;
		Array msgThirdTurretDead = MsgThirdTurretDead;
		if (logic_uScript_AddOnScreenMessage_locString_57.Length != num + msgThirdTurretDead.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_57, num + msgThirdTurretDead.Length);
		}
		Array.Copy(msgThirdTurretDead, 0, logic_uScript_AddOnScreenMessage_locString_57, num, msgThirdTurretDead.Length);
		num += msgThirdTurretDead.Length;
		logic_uScript_AddOnScreenMessage_tag_57 = local_MsgDead_System_String;
		logic_uScript_AddOnScreenMessage_speaker_57 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_57 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_57.In(logic_uScript_AddOnScreenMessage_locString_57, logic_uScript_AddOnScreenMessage_msgPriority_57, logic_uScript_AddOnScreenMessage_holdMsg_57, logic_uScript_AddOnScreenMessage_tag_57, logic_uScript_AddOnScreenMessage_speaker_57, logic_uScript_AddOnScreenMessage_side_57);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_57.Shown)
		{
			Relay_True_53();
		}
	}

	private void Relay_True_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.True(out logic_uScriptAct_SetBool_Target_60);
		local_AllTurretsDead_System_Boolean = logic_uScriptAct_SetBool_Target_60;
	}

	private void Relay_False_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.False(out logic_uScriptAct_SetBool_Target_60);
		local_AllTurretsDead_System_Boolean = logic_uScriptAct_SetBool_Target_60;
	}

	private void Relay_In_61()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_61.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_61.Out)
		{
			Relay_In_62();
		}
	}

	private void Relay_In_62()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_62.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_62.Out)
		{
			Relay_In_236();
		}
	}

	private void Relay_Save_Out_66()
	{
		Relay_Save_67();
	}

	private void Relay_Load_Out_66()
	{
		Relay_Load_67();
	}

	private void Relay_Restart_Out_66()
	{
		Relay_Set_False_67();
	}

	private void Relay_Save_66()
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = local_MsgArrivedAtMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_66 = local_MsgArrivedAtMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Save(ref logic_SubGraph_SaveLoadBool_boolean_66, logic_SubGraph_SaveLoadBool_boolAsVariable_66, logic_SubGraph_SaveLoadBool_uniqueID_66);
	}

	private void Relay_Load_66()
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = local_MsgArrivedAtMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_66 = local_MsgArrivedAtMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Load(ref logic_SubGraph_SaveLoadBool_boolean_66, logic_SubGraph_SaveLoadBool_boolAsVariable_66, logic_SubGraph_SaveLoadBool_uniqueID_66);
	}

	private void Relay_Set_True_66()
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = local_MsgArrivedAtMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_66 = local_MsgArrivedAtMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_66, logic_SubGraph_SaveLoadBool_boolAsVariable_66, logic_SubGraph_SaveLoadBool_uniqueID_66);
	}

	private void Relay_Set_False_66()
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = local_MsgArrivedAtMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_66 = local_MsgArrivedAtMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_66, logic_SubGraph_SaveLoadBool_boolAsVariable_66, logic_SubGraph_SaveLoadBool_uniqueID_66);
	}

	private void Relay_Save_Out_67()
	{
		Relay_Save_68();
	}

	private void Relay_Load_Out_67()
	{
		Relay_Load_68();
	}

	private void Relay_Restart_Out_67()
	{
		Relay_Set_False_68();
	}

	private void Relay_Save_67()
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = local_MsgFirstTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_67 = local_MsgFirstTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Save(ref logic_SubGraph_SaveLoadBool_boolean_67, logic_SubGraph_SaveLoadBool_boolAsVariable_67, logic_SubGraph_SaveLoadBool_uniqueID_67);
	}

	private void Relay_Load_67()
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = local_MsgFirstTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_67 = local_MsgFirstTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Load(ref logic_SubGraph_SaveLoadBool_boolean_67, logic_SubGraph_SaveLoadBool_boolAsVariable_67, logic_SubGraph_SaveLoadBool_uniqueID_67);
	}

	private void Relay_Set_True_67()
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = local_MsgFirstTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_67 = local_MsgFirstTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_67, logic_SubGraph_SaveLoadBool_boolAsVariable_67, logic_SubGraph_SaveLoadBool_uniqueID_67);
	}

	private void Relay_Set_False_67()
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = local_MsgFirstTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_67 = local_MsgFirstTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_67, logic_SubGraph_SaveLoadBool_boolAsVariable_67, logic_SubGraph_SaveLoadBool_uniqueID_67);
	}

	private void Relay_Save_Out_68()
	{
		Relay_Save_69();
	}

	private void Relay_Load_Out_68()
	{
		Relay_Load_69();
	}

	private void Relay_Restart_Out_68()
	{
		Relay_Set_False_69();
	}

	private void Relay_Save_68()
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = local_MsgSecondTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_68 = local_MsgSecondTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Save(ref logic_SubGraph_SaveLoadBool_boolean_68, logic_SubGraph_SaveLoadBool_boolAsVariable_68, logic_SubGraph_SaveLoadBool_uniqueID_68);
	}

	private void Relay_Load_68()
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = local_MsgSecondTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_68 = local_MsgSecondTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Load(ref logic_SubGraph_SaveLoadBool_boolean_68, logic_SubGraph_SaveLoadBool_boolAsVariable_68, logic_SubGraph_SaveLoadBool_uniqueID_68);
	}

	private void Relay_Set_True_68()
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = local_MsgSecondTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_68 = local_MsgSecondTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_68, logic_SubGraph_SaveLoadBool_boolAsVariable_68, logic_SubGraph_SaveLoadBool_uniqueID_68);
	}

	private void Relay_Set_False_68()
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = local_MsgSecondTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_68 = local_MsgSecondTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_68, logic_SubGraph_SaveLoadBool_boolAsVariable_68, logic_SubGraph_SaveLoadBool_uniqueID_68);
	}

	private void Relay_Save_Out_69()
	{
		Relay_Save_106();
	}

	private void Relay_Load_Out_69()
	{
		Relay_Load_106();
	}

	private void Relay_Restart_Out_69()
	{
		Relay_Set_False_106();
	}

	private void Relay_Save_69()
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = local_MsgThirdTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_69 = local_MsgThirdTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Save(ref logic_SubGraph_SaveLoadBool_boolean_69, logic_SubGraph_SaveLoadBool_boolAsVariable_69, logic_SubGraph_SaveLoadBool_uniqueID_69);
	}

	private void Relay_Load_69()
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = local_MsgThirdTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_69 = local_MsgThirdTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Load(ref logic_SubGraph_SaveLoadBool_boolean_69, logic_SubGraph_SaveLoadBool_boolAsVariable_69, logic_SubGraph_SaveLoadBool_uniqueID_69);
	}

	private void Relay_Set_True_69()
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = local_MsgThirdTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_69 = local_MsgThirdTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_69, logic_SubGraph_SaveLoadBool_boolAsVariable_69, logic_SubGraph_SaveLoadBool_uniqueID_69);
	}

	private void Relay_Set_False_69()
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = local_MsgThirdTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_69 = local_MsgThirdTurretDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_69, logic_SubGraph_SaveLoadBool_boolAsVariable_69, logic_SubGraph_SaveLoadBool_uniqueID_69);
	}

	private void Relay_In_70()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_70 = owner_Connection_71;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_70.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_70);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_70.Out)
		{
			Relay_InitialSpawn_31();
		}
	}

	private void Relay_Pause_72()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_72.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_72.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_UnPause_72()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_72.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_72.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_74()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_74 = local_MsgDead_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_74.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_74, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_74);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_74.Out)
		{
			Relay_In_4();
		}
	}

	private void Relay_Pause_79()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_79.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_79.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_UnPause_79()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_79.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_79.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_In_80()
	{
		int num = 0;
		Array turretData = TurretData2;
		if (logic_uScript_GetAndCheckTechs_techData_80.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_80, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_GetAndCheckTechs_techData_80, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_80 = owner_Connection_81;
		int num2 = 0;
		Array array = local_Turrets2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_80.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_80, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_80, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_80 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_80.In(logic_uScript_GetAndCheckTechs_techData_80, logic_uScript_GetAndCheckTechs_ownerNode_80, ref logic_uScript_GetAndCheckTechs_techs_80);
		local_Turrets2_TankArray = logic_uScript_GetAndCheckTechs_techs_80;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_80.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_80.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_80.AllDead;
		if (allAlive)
		{
			Relay_In_94();
		}
		if (someAlive)
		{
			Relay_In_94();
		}
		if (allDead)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_85()
	{
		int num = 0;
		Array turretData = TurretData3;
		if (logic_uScript_GetAndCheckTechs_techData_85.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_85, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_GetAndCheckTechs_techData_85, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_85 = owner_Connection_84;
		int num2 = 0;
		Array array = local_Turrets3_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_85.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_85, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_85, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_85 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_85.In(logic_uScript_GetAndCheckTechs_techData_85, logic_uScript_GetAndCheckTechs_ownerNode_85, ref logic_uScript_GetAndCheckTechs_techs_85);
		local_Turrets3_TankArray = logic_uScript_GetAndCheckTechs_techs_85;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_85.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_85.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_85.AllDead;
		if (allAlive)
		{
			Relay_In_102();
		}
		if (someAlive)
		{
			Relay_In_102();
		}
		if (allDead)
		{
			Relay_In_113();
		}
	}

	private void Relay_In_86()
	{
		logic_uScriptCon_CompareBool_Bool_86 = local_TurretMarkedDead1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.In(logic_uScriptCon_CompareBool_Bool_86);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.False;
		if (num)
		{
			Relay_In_90();
		}
		if (flag)
		{
			Relay_In_14();
		}
	}

	private void Relay_True_89()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_89.True(out logic_uScriptAct_SetBool_Target_89);
		local_TurretMarkedDead1_System_Boolean = logic_uScriptAct_SetBool_Target_89;
	}

	private void Relay_False_89()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_89.False(out logic_uScriptAct_SetBool_Target_89);
		local_TurretMarkedDead1_System_Boolean = logic_uScriptAct_SetBool_Target_89;
	}

	private void Relay_In_90()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.Out)
		{
			Relay_In_93();
		}
	}

	private void Relay_In_93()
	{
		logic_uScriptCon_CompareBool_Bool_93 = local_TurretMarkedDead2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.In(logic_uScriptCon_CompareBool_Bool_93);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.False;
		if (num)
		{
			Relay_In_94();
		}
		if (flag)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_94()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_94.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_94.Out)
		{
			Relay_In_99();
		}
	}

	private void Relay_True_95()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_95.True(out logic_uScriptAct_SetBool_Target_95);
		local_TurretMarkedDead2_System_Boolean = logic_uScriptAct_SetBool_Target_95;
	}

	private void Relay_False_95()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_95.False(out logic_uScriptAct_SetBool_Target_95);
		local_TurretMarkedDead2_System_Boolean = logic_uScriptAct_SetBool_Target_95;
	}

	private void Relay_Out_96()
	{
		Relay_True_36();
	}

	private void Relay_In_96()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_96 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_96, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_96);
	}

	private void Relay_In_99()
	{
		logic_uScriptCon_CompareBool_Bool_99 = local_TurretMarkedDead3_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_99.In(logic_uScriptCon_CompareBool_Bool_99);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_99.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_99.False;
		if (num)
		{
			Relay_In_102();
		}
		if (flag)
		{
			Relay_In_85();
		}
	}

	private void Relay_True_100()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_100.True(out logic_uScriptAct_SetBool_Target_100);
		local_TurretMarkedDead3_System_Boolean = logic_uScriptAct_SetBool_Target_100;
	}

	private void Relay_False_100()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_100.False(out logic_uScriptAct_SetBool_Target_100);
		local_TurretMarkedDead3_System_Boolean = logic_uScriptAct_SetBool_Target_100;
	}

	private void Relay_In_102()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_102.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_102.Out)
		{
			Relay_In_300();
		}
	}

	private void Relay_Save_Out_106()
	{
		Relay_Save_107();
	}

	private void Relay_Load_Out_106()
	{
		Relay_Load_107();
	}

	private void Relay_Restart_Out_106()
	{
		Relay_Set_False_107();
	}

	private void Relay_Save_106()
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = local_TurretMarkedDead1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_106 = local_TurretMarkedDead1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Save(ref logic_SubGraph_SaveLoadBool_boolean_106, logic_SubGraph_SaveLoadBool_boolAsVariable_106, logic_SubGraph_SaveLoadBool_uniqueID_106);
	}

	private void Relay_Load_106()
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = local_TurretMarkedDead1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_106 = local_TurretMarkedDead1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Load(ref logic_SubGraph_SaveLoadBool_boolean_106, logic_SubGraph_SaveLoadBool_boolAsVariable_106, logic_SubGraph_SaveLoadBool_uniqueID_106);
	}

	private void Relay_Set_True_106()
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = local_TurretMarkedDead1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_106 = local_TurretMarkedDead1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_106, logic_SubGraph_SaveLoadBool_boolAsVariable_106, logic_SubGraph_SaveLoadBool_uniqueID_106);
	}

	private void Relay_Set_False_106()
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = local_TurretMarkedDead1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_106 = local_TurretMarkedDead1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_106, logic_SubGraph_SaveLoadBool_boolAsVariable_106, logic_SubGraph_SaveLoadBool_uniqueID_106);
	}

	private void Relay_Save_Out_107()
	{
		Relay_Save_108();
	}

	private void Relay_Load_Out_107()
	{
		Relay_Load_108();
	}

	private void Relay_Restart_Out_107()
	{
		Relay_Set_False_108();
	}

	private void Relay_Save_107()
	{
		logic_SubGraph_SaveLoadBool_boolean_107 = local_TurretMarkedDead2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_107 = local_TurretMarkedDead2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Save(ref logic_SubGraph_SaveLoadBool_boolean_107, logic_SubGraph_SaveLoadBool_boolAsVariable_107, logic_SubGraph_SaveLoadBool_uniqueID_107);
	}

	private void Relay_Load_107()
	{
		logic_SubGraph_SaveLoadBool_boolean_107 = local_TurretMarkedDead2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_107 = local_TurretMarkedDead2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Load(ref logic_SubGraph_SaveLoadBool_boolean_107, logic_SubGraph_SaveLoadBool_boolAsVariable_107, logic_SubGraph_SaveLoadBool_uniqueID_107);
	}

	private void Relay_Set_True_107()
	{
		logic_SubGraph_SaveLoadBool_boolean_107 = local_TurretMarkedDead2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_107 = local_TurretMarkedDead2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_107, logic_SubGraph_SaveLoadBool_boolAsVariable_107, logic_SubGraph_SaveLoadBool_uniqueID_107);
	}

	private void Relay_Set_False_107()
	{
		logic_SubGraph_SaveLoadBool_boolean_107 = local_TurretMarkedDead2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_107 = local_TurretMarkedDead2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_107.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_107, logic_SubGraph_SaveLoadBool_boolAsVariable_107, logic_SubGraph_SaveLoadBool_uniqueID_107);
	}

	private void Relay_Save_Out_108()
	{
		Relay_Save_191();
	}

	private void Relay_Load_Out_108()
	{
		Relay_Load_191();
	}

	private void Relay_Restart_Out_108()
	{
		Relay_Set_False_191();
	}

	private void Relay_Save_108()
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = local_TurretMarkedDead3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_108 = local_TurretMarkedDead3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Save(ref logic_SubGraph_SaveLoadBool_boolean_108, logic_SubGraph_SaveLoadBool_boolAsVariable_108, logic_SubGraph_SaveLoadBool_uniqueID_108);
	}

	private void Relay_Load_108()
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = local_TurretMarkedDead3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_108 = local_TurretMarkedDead3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Load(ref logic_SubGraph_SaveLoadBool_boolean_108, logic_SubGraph_SaveLoadBool_boolAsVariable_108, logic_SubGraph_SaveLoadBool_uniqueID_108);
	}

	private void Relay_Set_True_108()
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = local_TurretMarkedDead3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_108 = local_TurretMarkedDead3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_108, logic_SubGraph_SaveLoadBool_boolAsVariable_108, logic_SubGraph_SaveLoadBool_uniqueID_108);
	}

	private void Relay_Set_False_108()
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = local_TurretMarkedDead3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_108 = local_TurretMarkedDead3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_108, logic_SubGraph_SaveLoadBool_boolAsVariable_108, logic_SubGraph_SaveLoadBool_uniqueID_108);
	}

	private void Relay_In_109()
	{
		logic_uScriptAct_AddInt_v2_B_109 = local_TurretsDead_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_109.In(logic_uScriptAct_AddInt_v2_A_109, logic_uScriptAct_AddInt_v2_B_109, out logic_uScriptAct_AddInt_v2_IntResult_109, out logic_uScriptAct_AddInt_v2_FloatResult_109);
		local_TurretsDead_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_109;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_109.Out)
		{
			Relay_True_89();
		}
	}

	private void Relay_In_110()
	{
		logic_uScriptCon_CompareInt_A_110 = local_TurretsDead_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_110.In(logic_uScriptCon_CompareInt_A_110, logic_uScriptCon_CompareInt_B_110);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_110.GreaterThan;
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_110.EqualTo;
		if (greaterThan)
		{
			Relay_In_135();
		}
		if (equalTo)
		{
			Relay_In_235();
		}
	}

	private void Relay_In_112()
	{
		logic_uScriptAct_AddInt_v2_B_112 = local_TurretsDead_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_112.In(logic_uScriptAct_AddInt_v2_A_112, logic_uScriptAct_AddInt_v2_B_112, out logic_uScriptAct_AddInt_v2_IntResult_112, out logic_uScriptAct_AddInt_v2_FloatResult_112);
		local_TurretsDead_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_112;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_112.Out)
		{
			Relay_True_95();
		}
	}

	private void Relay_In_113()
	{
		logic_uScriptAct_AddInt_v2_B_113 = local_TurretsDead_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_113.In(logic_uScriptAct_AddInt_v2_A_113, logic_uScriptAct_AddInt_v2_B_113, out logic_uScriptAct_AddInt_v2_IntResult_113, out logic_uScriptAct_AddInt_v2_FloatResult_113);
		local_TurretsDead_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_113;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_113.Out)
		{
			Relay_True_100();
		}
	}

	private void Relay_Out_115()
	{
		Relay_True_117();
	}

	private void Relay_In_115()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_115 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_115.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_115, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_115);
	}

	private void Relay_True_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.True(out logic_uScriptAct_SetBool_Target_117);
		local_QLStepped3_System_Boolean = logic_uScriptAct_SetBool_Target_117;
	}

	private void Relay_False_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.False(out logic_uScriptAct_SetBool_Target_117);
		local_QLStepped3_System_Boolean = logic_uScriptAct_SetBool_Target_117;
	}

	private void Relay_In_118()
	{
		logic_uScriptCon_CompareBool_Bool_118 = local_QLStepped3_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.In(logic_uScriptCon_CompareBool_Bool_118);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.False;
		if (num)
		{
			Relay_In_232();
		}
		if (flag)
		{
			Relay_In_115();
		}
	}

	private void Relay_True_120()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_120.True(out logic_uScriptAct_SetBool_Target_120);
		local_QLStepped2_System_Boolean = logic_uScriptAct_SetBool_Target_120;
	}

	private void Relay_False_120()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_120.False(out logic_uScriptAct_SetBool_Target_120);
		local_QLStepped2_System_Boolean = logic_uScriptAct_SetBool_Target_120;
	}

	private void Relay_In_121()
	{
		logic_uScriptCon_CompareBool_Bool_121 = local_QLStepped2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.In(logic_uScriptCon_CompareBool_Bool_121);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.False;
		if (num)
		{
			Relay_In_233();
		}
		if (flag)
		{
			Relay_In_123();
		}
	}

	private void Relay_Out_123()
	{
		Relay_True_120();
	}

	private void Relay_In_123()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_123 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_123, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_123);
	}

	private void Relay_In_126()
	{
		logic_uScriptCon_CompareBool_Bool_126 = local_QLStepped1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.In(logic_uScriptCon_CompareBool_Bool_126);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.False;
		if (num)
		{
			Relay_In_234();
		}
		if (flag)
		{
			Relay_In_131();
		}
	}

	private void Relay_True_129()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_129.True(out logic_uScriptAct_SetBool_Target_129);
		local_QLStepped1_System_Boolean = logic_uScriptAct_SetBool_Target_129;
	}

	private void Relay_False_129()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_129.False(out logic_uScriptAct_SetBool_Target_129);
		local_QLStepped1_System_Boolean = logic_uScriptAct_SetBool_Target_129;
	}

	private void Relay_Out_131()
	{
		Relay_True_129();
	}

	private void Relay_In_131()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_131 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_131, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_131);
	}

	private void Relay_Save_Out_132()
	{
		Relay_Save_27();
	}

	private void Relay_Load_Out_132()
	{
		Relay_Load_27();
	}

	private void Relay_Restart_Out_132()
	{
		Relay_Restart_27();
	}

	private void Relay_Save_132()
	{
		logic_SubGraph_SaveLoadInt_integer_132 = local_TurretsDead_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_132 = local_TurretsDead_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.Save(logic_SubGraph_SaveLoadInt_restartValue_132, ref logic_SubGraph_SaveLoadInt_integer_132, logic_SubGraph_SaveLoadInt_intAsVariable_132, logic_SubGraph_SaveLoadInt_uniqueID_132);
	}

	private void Relay_Load_132()
	{
		logic_SubGraph_SaveLoadInt_integer_132 = local_TurretsDead_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_132 = local_TurretsDead_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.Load(logic_SubGraph_SaveLoadInt_restartValue_132, ref logic_SubGraph_SaveLoadInt_integer_132, logic_SubGraph_SaveLoadInt_intAsVariable_132, logic_SubGraph_SaveLoadInt_uniqueID_132);
	}

	private void Relay_Restart_132()
	{
		logic_SubGraph_SaveLoadInt_integer_132 = local_TurretsDead_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_132 = local_TurretsDead_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_132.Restart(logic_SubGraph_SaveLoadInt_restartValue_132, ref logic_SubGraph_SaveLoadInt_integer_132, logic_SubGraph_SaveLoadInt_intAsVariable_132, logic_SubGraph_SaveLoadInt_uniqueID_132);
	}

	private void Relay_In_133()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_133.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_133.Out)
		{
			Relay_Set_False_15();
		}
	}

	private void Relay_In_134()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_134.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_134.Out)
		{
			Relay_Save_15();
		}
	}

	private void Relay_Output1_135()
	{
		Relay_In_126();
	}

	private void Relay_Output2_135()
	{
		Relay_In_121();
	}

	private void Relay_Output3_135()
	{
		Relay_In_118();
	}

	private void Relay_Output4_135()
	{
	}

	private void Relay_Output5_135()
	{
	}

	private void Relay_Output6_135()
	{
	}

	private void Relay_Output7_135()
	{
	}

	private void Relay_Output8_135()
	{
	}

	private void Relay_In_135()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_135 = local_TurretsDead_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_135.In(logic_uScriptCon_ManualSwitch_CurrentOutput_135);
	}

	private void Relay_In_136()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_136 = local_MsgTriggerA_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_136.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_136, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_136);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_136.Out)
		{
			Relay_In_178();
		}
	}

	private void Relay_In_137()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_137 = local_MsgTriggerA_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_137.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_137, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_137);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_137.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_In_138()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_138 = local_MsgTriggerA_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_138.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_138, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_138);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_138.Out)
		{
			Relay_In_184();
		}
	}

	private void Relay_In_139()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_139 = TriggerA;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_139.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_139);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_139.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_139.OutOfRange;
		if (inRange)
		{
			Relay_In_152();
		}
		if (outOfRange)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_142()
	{
		logic_uScriptCon_CompareBool_Bool_142 = local_TriggerEnteredA_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_142.In(logic_uScriptCon_CompareBool_Bool_142);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_142.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_142.False;
		if (num)
		{
			Relay_In_155();
			Relay_False_551();
		}
		if (flag)
		{
			Relay_In_146();
		}
	}

	private void Relay_In_143()
	{
		int num = 0;
		Array msgNearTurret = MsgNearTurret1;
		if (logic_uScript_AddOnScreenMessage_locString_143.Length != num + msgNearTurret.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_143, num + msgNearTurret.Length);
		}
		Array.Copy(msgNearTurret, 0, logic_uScript_AddOnScreenMessage_locString_143, num, msgNearTurret.Length);
		num += msgNearTurret.Length;
		logic_uScript_AddOnScreenMessage_tag_143 = local_MsgTriggerA_System_String;
		logic_uScript_AddOnScreenMessage_speaker_143 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_143 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_143.In(logic_uScript_AddOnScreenMessage_locString_143, logic_uScript_AddOnScreenMessage_msgPriority_143, logic_uScript_AddOnScreenMessage_holdMsg_143, logic_uScript_AddOnScreenMessage_tag_143, logic_uScript_AddOnScreenMessage_speaker_143, logic_uScript_AddOnScreenMessage_side_143);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_143.Out)
		{
			Relay_True_147();
		}
	}

	private void Relay_In_146()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_146 = local_MsgDead_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_146.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_146, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_146);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_146.Out)
		{
			Relay_In_173();
		}
	}

	private void Relay_True_147()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_147.True(out logic_uScriptAct_SetBool_Target_147);
		local_TriggerEnteredA_System_Boolean = logic_uScriptAct_SetBool_Target_147;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_147.Out)
		{
			Relay_False_571();
		}
	}

	private void Relay_False_147()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_147.False(out logic_uScriptAct_SetBool_Target_147);
		local_TriggerEnteredA_System_Boolean = logic_uScriptAct_SetBool_Target_147;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_147.Out)
		{
			Relay_False_571();
		}
	}

	private void Relay_In_152()
	{
		int num = 0;
		Array turretData = TurretData1;
		if (logic_uScript_GetAndCheckTechs_techData_152.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_152, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_GetAndCheckTechs_techData_152, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_152 = owner_Connection_151;
		int num2 = 0;
		Array array = local_Turrets1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_152.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_152, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_152, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_152 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.In(logic_uScript_GetAndCheckTechs_techData_152, logic_uScript_GetAndCheckTechs_ownerNode_152, ref logic_uScript_GetAndCheckTechs_techs_152);
		local_Turrets1_TankArray = logic_uScript_GetAndCheckTechs_techs_152;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.AllDead;
		if (allAlive)
		{
			Relay_In_142();
		}
		if (someAlive)
		{
			Relay_In_142();
		}
		if (allDead)
		{
			Relay_In_154();
		}
	}

	private void Relay_In_153()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153.Out)
		{
			Relay_In_154();
		}
	}

	private void Relay_In_154()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_154.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_154.Out)
		{
			Relay_In_155();
		}
	}

	private void Relay_In_155()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_155.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_155.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_In_159()
	{
		int num = 0;
		Array msgNearTurret = MsgNearTurret2;
		if (logic_uScript_AddOnScreenMessage_locString_159.Length != num + msgNearTurret.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_159, num + msgNearTurret.Length);
		}
		Array.Copy(msgNearTurret, 0, logic_uScript_AddOnScreenMessage_locString_159, num, msgNearTurret.Length);
		num += msgNearTurret.Length;
		logic_uScript_AddOnScreenMessage_tag_159 = local_MsgTriggerB_System_String;
		logic_uScript_AddOnScreenMessage_speaker_159 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_159 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_159.In(logic_uScript_AddOnScreenMessage_locString_159, logic_uScript_AddOnScreenMessage_msgPriority_159, logic_uScript_AddOnScreenMessage_holdMsg_159, logic_uScript_AddOnScreenMessage_tag_159, logic_uScript_AddOnScreenMessage_speaker_159, logic_uScript_AddOnScreenMessage_side_159);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_159.Out)
		{
			Relay_True_162();
		}
	}

	private void Relay_In_160()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_160.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_160.Out)
		{
			Relay_In_205();
		}
	}

	private void Relay_True_162()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_162.True(out logic_uScriptAct_SetBool_Target_162);
		local_TriggerEnteredB_System_Boolean = logic_uScriptAct_SetBool_Target_162;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_162.Out)
		{
			Relay_False_575();
		}
	}

	private void Relay_False_162()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_162.False(out logic_uScriptAct_SetBool_Target_162);
		local_TriggerEnteredB_System_Boolean = logic_uScriptAct_SetBool_Target_162;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_162.Out)
		{
			Relay_False_575();
		}
	}

	private void Relay_In_163()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_163 = TriggerB;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_163.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_163);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_163.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_163.OutOfRange;
		if (inRange)
		{
			Relay_In_164();
		}
		if (outOfRange)
		{
			Relay_In_167();
		}
	}

	private void Relay_In_164()
	{
		int num = 0;
		Array turretData = TurretData2;
		if (logic_uScript_GetAndCheckTechs_techData_164.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_164, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_GetAndCheckTechs_techData_164, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_164 = owner_Connection_169;
		int num2 = 0;
		Array array = local_Turrets2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_164.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_164, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_164, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_164 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_164.In(logic_uScript_GetAndCheckTechs_techData_164, logic_uScript_GetAndCheckTechs_ownerNode_164, ref logic_uScript_GetAndCheckTechs_techs_164);
		local_Turrets2_TankArray = logic_uScript_GetAndCheckTechs_techs_164;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_164.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_164.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_164.AllDead;
		if (allAlive)
		{
			Relay_In_168();
		}
		if (someAlive)
		{
			Relay_In_168();
		}
		if (allDead)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_165()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_165.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_165.Out)
		{
			Relay_In_160();
		}
	}

	private void Relay_In_167()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_168()
	{
		logic_uScriptCon_CompareBool_Bool_168 = local_TriggerEnteredB_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.In(logic_uScriptCon_CompareBool_Bool_168);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.False;
		if (num)
		{
			Relay_False_553();
		}
		if (flag)
		{
			Relay_In_172();
		}
	}

	private void Relay_In_172()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_172 = local_MsgDead_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_172.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_172, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_172);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_172.Out)
		{
			Relay_In_175();
		}
	}

	private void Relay_In_173()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_173 = local_MsgTriggerB_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_173.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_173, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_173);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_173.Out)
		{
			Relay_In_214();
		}
	}

	private void Relay_In_175()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_175 = local_MsgTriggerA_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_175.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_175, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_175);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_175.Out)
		{
			Relay_In_216();
		}
	}

	private void Relay_In_178()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_178 = local_MsgTriggerB_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_178.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_178, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_178);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_178.Out)
		{
			Relay_In_225();
		}
	}

	private void Relay_In_183()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_183 = local_MsgTriggerB_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_183.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_183, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_183);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_183.Out)
		{
			Relay_In_223();
		}
	}

	private void Relay_In_184()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_184 = local_MsgTriggerB_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_184.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_184, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_184);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_184.Out)
		{
			Relay_In_221();
		}
	}

	private void Relay_Save_Out_191()
	{
		Relay_Save_192();
	}

	private void Relay_Load_Out_191()
	{
		Relay_Load_192();
	}

	private void Relay_Restart_Out_191()
	{
		Relay_Set_False_192();
	}

	private void Relay_Save_191()
	{
		logic_SubGraph_SaveLoadBool_boolean_191 = local_TriggerEnteredA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_191 = local_TriggerEnteredA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Save(ref logic_SubGraph_SaveLoadBool_boolean_191, logic_SubGraph_SaveLoadBool_boolAsVariable_191, logic_SubGraph_SaveLoadBool_uniqueID_191);
	}

	private void Relay_Load_191()
	{
		logic_SubGraph_SaveLoadBool_boolean_191 = local_TriggerEnteredA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_191 = local_TriggerEnteredA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Load(ref logic_SubGraph_SaveLoadBool_boolean_191, logic_SubGraph_SaveLoadBool_boolAsVariable_191, logic_SubGraph_SaveLoadBool_uniqueID_191);
	}

	private void Relay_Set_True_191()
	{
		logic_SubGraph_SaveLoadBool_boolean_191 = local_TriggerEnteredA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_191 = local_TriggerEnteredA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_191, logic_SubGraph_SaveLoadBool_boolAsVariable_191, logic_SubGraph_SaveLoadBool_uniqueID_191);
	}

	private void Relay_Set_False_191()
	{
		logic_SubGraph_SaveLoadBool_boolean_191 = local_TriggerEnteredA_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_191 = local_TriggerEnteredA_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_191.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_191, logic_SubGraph_SaveLoadBool_boolAsVariable_191, logic_SubGraph_SaveLoadBool_uniqueID_191);
	}

	private void Relay_Save_Out_192()
	{
		Relay_Save_227();
	}

	private void Relay_Load_Out_192()
	{
		Relay_Load_227();
	}

	private void Relay_Restart_Out_192()
	{
		Relay_Set_False_227();
	}

	private void Relay_Save_192()
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = local_TriggerEnteredB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_192 = local_TriggerEnteredB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Save(ref logic_SubGraph_SaveLoadBool_boolean_192, logic_SubGraph_SaveLoadBool_boolAsVariable_192, logic_SubGraph_SaveLoadBool_uniqueID_192);
	}

	private void Relay_Load_192()
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = local_TriggerEnteredB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_192 = local_TriggerEnteredB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Load(ref logic_SubGraph_SaveLoadBool_boolean_192, logic_SubGraph_SaveLoadBool_boolAsVariable_192, logic_SubGraph_SaveLoadBool_uniqueID_192);
	}

	private void Relay_Set_True_192()
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = local_TriggerEnteredB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_192 = local_TriggerEnteredB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_192, logic_SubGraph_SaveLoadBool_boolAsVariable_192, logic_SubGraph_SaveLoadBool_uniqueID_192);
	}

	private void Relay_Set_False_192()
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = local_TriggerEnteredB_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_192 = local_TriggerEnteredB_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_192, logic_SubGraph_SaveLoadBool_boolAsVariable_192, logic_SubGraph_SaveLoadBool_uniqueID_192);
	}

	private void Relay_Save_Out_193()
	{
		Relay_Save_194();
	}

	private void Relay_Load_Out_193()
	{
		Relay_Load_194();
	}

	private void Relay_Restart_Out_193()
	{
		Relay_Set_False_194();
	}

	private void Relay_Save_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_QLStepped1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_QLStepped1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Save(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Load_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_QLStepped1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_QLStepped1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Load(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Set_True_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_QLStepped1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_QLStepped1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Set_False_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_QLStepped1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_QLStepped1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Save_Out_194()
	{
		Relay_Save_195();
	}

	private void Relay_Load_Out_194()
	{
		Relay_Load_195();
	}

	private void Relay_Restart_Out_194()
	{
		Relay_Set_False_195();
	}

	private void Relay_Save_194()
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = local_QLStepped2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_194 = local_QLStepped2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Save(ref logic_SubGraph_SaveLoadBool_boolean_194, logic_SubGraph_SaveLoadBool_boolAsVariable_194, logic_SubGraph_SaveLoadBool_uniqueID_194);
	}

	private void Relay_Load_194()
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = local_QLStepped2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_194 = local_QLStepped2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Load(ref logic_SubGraph_SaveLoadBool_boolean_194, logic_SubGraph_SaveLoadBool_boolAsVariable_194, logic_SubGraph_SaveLoadBool_uniqueID_194);
	}

	private void Relay_Set_True_194()
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = local_QLStepped2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_194 = local_QLStepped2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_194, logic_SubGraph_SaveLoadBool_boolAsVariable_194, logic_SubGraph_SaveLoadBool_uniqueID_194);
	}

	private void Relay_Set_False_194()
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = local_QLStepped2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_194 = local_QLStepped2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_194, logic_SubGraph_SaveLoadBool_boolAsVariable_194, logic_SubGraph_SaveLoadBool_uniqueID_194);
	}

	private void Relay_Save_Out_195()
	{
		Relay_Save_299();
	}

	private void Relay_Load_Out_195()
	{
		Relay_Load_299();
	}

	private void Relay_Restart_Out_195()
	{
		Relay_Set_False_299();
	}

	private void Relay_Save_195()
	{
		logic_SubGraph_SaveLoadBool_boolean_195 = local_QLStepped3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_195 = local_QLStepped3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Save(ref logic_SubGraph_SaveLoadBool_boolean_195, logic_SubGraph_SaveLoadBool_boolAsVariable_195, logic_SubGraph_SaveLoadBool_uniqueID_195);
	}

	private void Relay_Load_195()
	{
		logic_SubGraph_SaveLoadBool_boolean_195 = local_QLStepped3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_195 = local_QLStepped3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Load(ref logic_SubGraph_SaveLoadBool_boolean_195, logic_SubGraph_SaveLoadBool_boolAsVariable_195, logic_SubGraph_SaveLoadBool_uniqueID_195);
	}

	private void Relay_Set_True_195()
	{
		logic_SubGraph_SaveLoadBool_boolean_195 = local_QLStepped3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_195 = local_QLStepped3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_195, logic_SubGraph_SaveLoadBool_boolAsVariable_195, logic_SubGraph_SaveLoadBool_uniqueID_195);
	}

	private void Relay_Set_False_195()
	{
		logic_SubGraph_SaveLoadBool_boolean_195 = local_QLStepped3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_195 = local_QLStepped3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_195.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_195, logic_SubGraph_SaveLoadBool_boolAsVariable_195, logic_SubGraph_SaveLoadBool_uniqueID_195);
	}

	private void Relay_True_196()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_196.True(out logic_uScriptAct_SetBool_Target_196);
		local_TriggerEnteredC_System_Boolean = logic_uScriptAct_SetBool_Target_196;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_196.Out)
		{
			Relay_False_579();
		}
	}

	private void Relay_False_196()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_196.False(out logic_uScriptAct_SetBool_Target_196);
		local_TriggerEnteredC_System_Boolean = logic_uScriptAct_SetBool_Target_196;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_196.Out)
		{
			Relay_False_579();
		}
	}

	private void Relay_In_198()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_198 = local_MsgDead_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_198.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_198, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_198);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_198.Out)
		{
			Relay_In_203();
		}
	}

	private void Relay_In_199()
	{
		int num = 0;
		Array msgNearTurret = MsgNearTurret3;
		if (logic_uScript_AddOnScreenMessage_locString_199.Length != num + msgNearTurret.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_199, num + msgNearTurret.Length);
		}
		Array.Copy(msgNearTurret, 0, logic_uScript_AddOnScreenMessage_locString_199, num, msgNearTurret.Length);
		num += msgNearTurret.Length;
		logic_uScript_AddOnScreenMessage_tag_199 = local_MsgTriggerC_System_String;
		logic_uScript_AddOnScreenMessage_speaker_199 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_199 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_199.In(logic_uScript_AddOnScreenMessage_locString_199, logic_uScript_AddOnScreenMessage_msgPriority_199, logic_uScript_AddOnScreenMessage_holdMsg_199, logic_uScript_AddOnScreenMessage_tag_199, logic_uScript_AddOnScreenMessage_speaker_199, logic_uScript_AddOnScreenMessage_side_199);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_199.Out)
		{
			Relay_True_196();
		}
	}

	private void Relay_In_200()
	{
		int num = 0;
		Array turretData = TurretData3;
		if (logic_uScript_GetAndCheckTechs_techData_200.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_200, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_GetAndCheckTechs_techData_200, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_200 = owner_Connection_211;
		int num2 = 0;
		Array array = local_Turrets3_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_200.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_200, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_200, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_200 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200.In(logic_uScript_GetAndCheckTechs_techData_200, logic_uScript_GetAndCheckTechs_ownerNode_200, ref logic_uScript_GetAndCheckTechs_techs_200);
		local_Turrets3_TankArray = logic_uScript_GetAndCheckTechs_techs_200;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_200.AllDead;
		if (allAlive)
		{
			Relay_In_206();
		}
		if (someAlive)
		{
			Relay_In_206();
		}
		if (allDead)
		{
			Relay_In_207();
		}
	}

	private void Relay_In_203()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_203 = local_MsgTriggerA_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_203.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_203, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_203);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_203.Out)
		{
			Relay_In_219();
		}
	}

	private void Relay_In_205()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_205 = TriggerC;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_205.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_205);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_205.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_205.OutOfRange;
		if (inRange)
		{
			Relay_In_200();
		}
		if (outOfRange)
		{
			Relay_In_213();
		}
	}

	private void Relay_In_206()
	{
		logic_uScriptCon_CompareBool_Bool_206 = local_TriggerEnteredC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206.In(logic_uScriptCon_CompareBool_Bool_206);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206.False;
		if (num)
		{
			Relay_In_209();
			Relay_False_555();
		}
		if (flag)
		{
			Relay_In_198();
		}
	}

	private void Relay_In_207()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_207.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_207.Out)
		{
			Relay_In_209();
		}
	}

	private void Relay_In_209()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.Out)
		{
			Relay_In_264();
		}
	}

	private void Relay_In_213()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_213.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_213.Out)
		{
			Relay_In_207();
		}
	}

	private void Relay_In_214()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_214 = local_MsgTriggerC_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_214.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_214, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_214);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_214.Out)
		{
			Relay_In_315();
		}
	}

	private void Relay_In_216()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_216 = local_MsgTriggerC_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_216.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_216, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_216);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_216.Out)
		{
			Relay_In_317();
		}
	}

	private void Relay_In_219()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_219 = local_MsgTriggerB_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_219.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_219, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_219);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_219.Out)
		{
			Relay_In_319();
		}
	}

	private void Relay_In_221()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_221 = local_MsgTriggerC_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_221.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_221, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_221);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_221.Out)
		{
			Relay_In_281();
		}
	}

	private void Relay_In_223()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_223 = local_MsgTriggerC_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_223.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_223, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_223);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_223.Out)
		{
			Relay_In_283();
		}
	}

	private void Relay_In_225()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_225 = local_MsgTriggerC_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_225.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_225, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_225);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_225.Out)
		{
			Relay_In_285();
		}
	}

	private void Relay_Save_Out_227()
	{
		Relay_Save_193();
	}

	private void Relay_Load_Out_227()
	{
		Relay_Load_193();
	}

	private void Relay_Restart_Out_227()
	{
		Relay_Set_False_193();
	}

	private void Relay_Save_227()
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = local_TriggerEnteredC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_227 = local_TriggerEnteredC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Save(ref logic_SubGraph_SaveLoadBool_boolean_227, logic_SubGraph_SaveLoadBool_boolAsVariable_227, logic_SubGraph_SaveLoadBool_uniqueID_227);
	}

	private void Relay_Load_227()
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = local_TriggerEnteredC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_227 = local_TriggerEnteredC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Load(ref logic_SubGraph_SaveLoadBool_boolean_227, logic_SubGraph_SaveLoadBool_boolAsVariable_227, logic_SubGraph_SaveLoadBool_uniqueID_227);
	}

	private void Relay_Set_True_227()
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = local_TriggerEnteredC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_227 = local_TriggerEnteredC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_227, logic_SubGraph_SaveLoadBool_boolAsVariable_227, logic_SubGraph_SaveLoadBool_uniqueID_227);
	}

	private void Relay_Set_False_227()
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = local_TriggerEnteredC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_227 = local_TriggerEnteredC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_227, logic_SubGraph_SaveLoadBool_boolAsVariable_227, logic_SubGraph_SaveLoadBool_uniqueID_227);
	}

	private void Relay_In_230()
	{
		logic_uScript_ClearEncounterTarget_owner_230 = owner_Connection_229;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_230.In(logic_uScript_ClearEncounterTarget_owner_230);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_230.Out)
		{
			Relay_In_429();
		}
	}

	private void Relay_In_231()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_231.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_231.Out)
		{
			Relay_In_416();
		}
	}

	private void Relay_In_232()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_232.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_232.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_233()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_233.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_233.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_234()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_234.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_234.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_235()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235.Out)
		{
			Relay_In_247();
		}
	}

	private void Relay_In_236()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_236.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_236.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_In_237()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_237.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_237.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_In_247()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_247.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_247.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_259()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_259 = local_MsgTriggerB_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_259.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_259, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_259);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_259.Out)
		{
			Relay_In_278();
		}
	}

	private void Relay_In_260()
	{
		int num = 0;
		Array msgAtRightHeight = MsgAtRightHeight;
		if (logic_uScript_AddOnScreenMessage_locString_260.Length != num + msgAtRightHeight.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_260, num + msgAtRightHeight.Length);
		}
		Array.Copy(msgAtRightHeight, 0, logic_uScript_AddOnScreenMessage_locString_260, num, msgAtRightHeight.Length);
		num += msgAtRightHeight.Length;
		logic_uScript_AddOnScreenMessage_tag_260 = local_MsgTriggerD_System_String;
		logic_uScript_AddOnScreenMessage_speaker_260 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_260 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_260.In(logic_uScript_AddOnScreenMessage_locString_260, logic_uScript_AddOnScreenMessage_msgPriority_260, logic_uScript_AddOnScreenMessage_holdMsg_260, logic_uScript_AddOnScreenMessage_tag_260, logic_uScript_AddOnScreenMessage_speaker_260, logic_uScript_AddOnScreenMessage_side_260);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_260.Out)
		{
			Relay_True_262();
		}
	}

	private void Relay_In_261()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_261.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_261.Out)
		{
			Relay_In_267();
		}
	}

	private void Relay_True_262()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_262.True(out logic_uScriptAct_SetBool_Target_262);
		local_TriggerEnteredD_System_Boolean = logic_uScriptAct_SetBool_Target_262;
	}

	private void Relay_False_262()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_262.False(out logic_uScriptAct_SetBool_Target_262);
		local_TriggerEnteredD_System_Boolean = logic_uScriptAct_SetBool_Target_262;
	}

	private void Relay_In_263()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_263 = local_MsgTriggerA_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_263.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_263, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_263);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_263.Out)
		{
			Relay_In_259();
		}
	}

	private void Relay_In_264()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_264 = TriggerD;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_264.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_264);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_264.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_264.OutOfRange;
		if (inRange)
		{
			Relay_In_292();
		}
		if (outOfRange)
		{
			Relay_In_276();
		}
	}

	private void Relay_In_267()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_267.In();
	}

	private void Relay_In_272()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_272 = local_MsgDead_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_272.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_272, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_272);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_272.Out)
		{
			Relay_In_263();
		}
	}

	private void Relay_In_273()
	{
		logic_uScriptCon_CompareBool_Bool_273 = local_TriggerEnteredD_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_273.In(logic_uScriptCon_CompareBool_Bool_273);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_273.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_273.False;
		if (num)
		{
			Relay_In_261();
		}
		if (flag)
		{
			Relay_In_272();
		}
	}

	private void Relay_In_276()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_276.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_276.Out)
		{
			Relay_In_261();
		}
	}

	private void Relay_In_278()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_278 = local_MsgTriggerC_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_278.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_278, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_278);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_278.Out)
		{
			Relay_In_260();
		}
	}

	private void Relay_In_281()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_281 = local_MsgTriggerD_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_281.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_281, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_281);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_281.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_283()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_283 = local_MsgTriggerD_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_283.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_283, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_283);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_283.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_285()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_285 = local_MsgTriggerD_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_285.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_285, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_285);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_285.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_286()
	{
		int num = 0;
		Array turretData = TurretData2;
		if (logic_uScript_GetAndCheckTechs_techData_286.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_286, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_GetAndCheckTechs_techData_286, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_286 = owner_Connection_289;
		int num2 = 0;
		Array array = local_Turrets2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_286.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_286, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_286, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_286 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_286.In(logic_uScript_GetAndCheckTechs_techData_286, logic_uScript_GetAndCheckTechs_ownerNode_286, ref logic_uScript_GetAndCheckTechs_techs_286);
		local_Turrets2_TankArray = logic_uScript_GetAndCheckTechs_techs_286;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_286.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_286.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_286.AllDead;
		if (allAlive)
		{
			Relay_In_273();
		}
		if (someAlive)
		{
			Relay_In_273();
		}
		if (allDead)
		{
			Relay_In_296();
		}
	}

	private void Relay_In_292()
	{
		int num = 0;
		Array turretData = TurretData1;
		if (logic_uScript_GetAndCheckTechs_techData_292.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_292, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_GetAndCheckTechs_techData_292, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_292 = owner_Connection_291;
		int num2 = 0;
		Array array = local_Turrets1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_292.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_292, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_292, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_292 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_292.In(logic_uScript_GetAndCheckTechs_techData_292, logic_uScript_GetAndCheckTechs_ownerNode_292, ref logic_uScript_GetAndCheckTechs_techs_292);
		local_Turrets1_TankArray = logic_uScript_GetAndCheckTechs_techs_292;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_292.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_292.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_292.AllDead;
		if (allAlive)
		{
			Relay_In_273();
		}
		if (someAlive)
		{
			Relay_In_273();
		}
		if (allDead)
		{
			Relay_In_286();
		}
	}

	private void Relay_In_296()
	{
		int num = 0;
		Array turretData = TurretData3;
		if (logic_uScript_GetAndCheckTechs_techData_296.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_296, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_GetAndCheckTechs_techData_296, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_296 = owner_Connection_295;
		int num2 = 0;
		Array array = local_Turrets3_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_296.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_296, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_296, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_296 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_296.In(logic_uScript_GetAndCheckTechs_techData_296, logic_uScript_GetAndCheckTechs_ownerNode_296, ref logic_uScript_GetAndCheckTechs_techs_296);
		local_Turrets3_TankArray = logic_uScript_GetAndCheckTechs_techs_296;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_296.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_296.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_296.AllDead;
		if (allAlive)
		{
			Relay_In_273();
		}
		if (someAlive)
		{
			Relay_In_273();
		}
		if (allDead)
		{
			Relay_In_261();
		}
	}

	private void Relay_Save_Out_299()
	{
		Relay_Save_305();
	}

	private void Relay_Load_Out_299()
	{
		Relay_Load_305();
	}

	private void Relay_Restart_Out_299()
	{
		Relay_Set_False_305();
	}

	private void Relay_Save_299()
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = local_TriggerEnteredD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_299 = local_TriggerEnteredD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Save(ref logic_SubGraph_SaveLoadBool_boolean_299, logic_SubGraph_SaveLoadBool_boolAsVariable_299, logic_SubGraph_SaveLoadBool_uniqueID_299);
	}

	private void Relay_Load_299()
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = local_TriggerEnteredD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_299 = local_TriggerEnteredD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Load(ref logic_SubGraph_SaveLoadBool_boolean_299, logic_SubGraph_SaveLoadBool_boolAsVariable_299, logic_SubGraph_SaveLoadBool_uniqueID_299);
	}

	private void Relay_Set_True_299()
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = local_TriggerEnteredD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_299 = local_TriggerEnteredD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_299, logic_SubGraph_SaveLoadBool_boolAsVariable_299, logic_SubGraph_SaveLoadBool_uniqueID_299);
	}

	private void Relay_Set_False_299()
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = local_TriggerEnteredD_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_299 = local_TriggerEnteredD_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_299, logic_SubGraph_SaveLoadBool_boolAsVariable_299, logic_SubGraph_SaveLoadBool_uniqueID_299);
	}

	private void Relay_In_300()
	{
		logic_uScriptCon_CompareBool_Bool_300 = local_AllTurretsDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_300.In(logic_uScriptCon_CompareBool_Bool_300);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_300.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_300.False;
		if (num)
		{
			Relay_In_302();
		}
		if (flag)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_302()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_302.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_302.Out)
		{
			Relay_True_304();
		}
	}

	private void Relay_True_304()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_304.True(out logic_uScriptAct_SetBool_Target_304);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_304;
	}

	private void Relay_False_304()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_304.False(out logic_uScriptAct_SetBool_Target_304);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_304;
	}

	private void Relay_Save_Out_305()
	{
		Relay_Save_327();
	}

	private void Relay_Load_Out_305()
	{
		Relay_Load_327();
	}

	private void Relay_Restart_Out_305()
	{
		Relay_Set_False_327();
	}

	private void Relay_Save_305()
	{
		logic_SubGraph_SaveLoadBool_boolean_305 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_305 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Save(ref logic_SubGraph_SaveLoadBool_boolean_305, logic_SubGraph_SaveLoadBool_boolAsVariable_305, logic_SubGraph_SaveLoadBool_uniqueID_305);
	}

	private void Relay_Load_305()
	{
		logic_SubGraph_SaveLoadBool_boolean_305 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_305 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Load(ref logic_SubGraph_SaveLoadBool_boolean_305, logic_SubGraph_SaveLoadBool_boolAsVariable_305, logic_SubGraph_SaveLoadBool_uniqueID_305);
	}

	private void Relay_Set_True_305()
	{
		logic_SubGraph_SaveLoadBool_boolean_305 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_305 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_305, logic_SubGraph_SaveLoadBool_boolAsVariable_305, logic_SubGraph_SaveLoadBool_uniqueID_305);
	}

	private void Relay_Set_False_305()
	{
		logic_SubGraph_SaveLoadBool_boolean_305 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_305 = local_AllTurretsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_305.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_305, logic_SubGraph_SaveLoadBool_boolAsVariable_305, logic_SubGraph_SaveLoadBool_uniqueID_305);
	}

	private void Relay_In_308()
	{
		logic_uScript_GetPlayerTank_Return_308 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_308.In();
		local_PlayerTech_Tank = logic_uScript_GetPlayerTank_Return_308;
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_308.Returned)
		{
			Relay_In_310();
		}
	}

	private void Relay_In_310()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_310 = TriggerG;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_310.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_310);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_310.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_310.OutOfRange;
		if (inRange)
		{
			Relay_In_525();
		}
		if (outOfRange)
		{
			Relay_In_312();
		}
	}

	private void Relay_In_312()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_312.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_312.Out)
		{
			Relay_In_313();
		}
	}

	private void Relay_In_313()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_313.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_313.Out)
		{
			Relay_In_382();
		}
	}

	private void Relay_In_315()
	{
		logic_uScript_IsTechGrounded_tech_315 = local_PlayerTech_Tank;
		logic_uScript_IsTechGrounded_uScript_IsTechGrounded_315.In(logic_uScript_IsTechGrounded_tech_315);
		if (logic_uScript_IsTechGrounded_uScript_IsTechGrounded_315.False)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_317()
	{
		logic_uScript_IsTechGrounded_tech_317 = local_PlayerTech_Tank;
		logic_uScript_IsTechGrounded_uScript_IsTechGrounded_317.In(logic_uScript_IsTechGrounded_tech_317);
		if (logic_uScript_IsTechGrounded_uScript_IsTechGrounded_317.False)
		{
			Relay_In_159();
		}
	}

	private void Relay_In_319()
	{
		logic_uScript_IsTechGrounded_tech_319 = local_PlayerTech_Tank;
		logic_uScript_IsTechGrounded_uScript_IsTechGrounded_319.In(logic_uScript_IsTechGrounded_tech_319);
		if (logic_uScript_IsTechGrounded_uScript_IsTechGrounded_319.False)
		{
			Relay_In_199();
		}
	}

	private void Relay_In_320()
	{
		logic_uScriptCon_CompareBool_Bool_320 = local_ShownMsgMinefieldWarning_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_320.In(logic_uScriptCon_CompareBool_Bool_320);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_320.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_320.False;
		if (num)
		{
			Relay_In_454();
		}
		if (flag)
		{
			Relay_In_324();
		}
	}

	private void Relay_In_324()
	{
		int num = 0;
		Array msgMinefieldWarning = MsgMinefieldWarning;
		if (logic_uScript_AddOnScreenMessage_locString_324.Length != num + msgMinefieldWarning.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_324, num + msgMinefieldWarning.Length);
		}
		Array.Copy(msgMinefieldWarning, 0, logic_uScript_AddOnScreenMessage_locString_324, num, msgMinefieldWarning.Length);
		num += msgMinefieldWarning.Length;
		logic_uScript_AddOnScreenMessage_speaker_324 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_324 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_324.In(logic_uScript_AddOnScreenMessage_locString_324, logic_uScript_AddOnScreenMessage_msgPriority_324, logic_uScript_AddOnScreenMessage_holdMsg_324, logic_uScript_AddOnScreenMessage_tag_324, logic_uScript_AddOnScreenMessage_speaker_324, logic_uScript_AddOnScreenMessage_side_324);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_324.Shown)
		{
			Relay_True_325();
		}
	}

	private void Relay_True_325()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_325.True(out logic_uScriptAct_SetBool_Target_325);
		local_ShownMsgMinefieldWarning_System_Boolean = logic_uScriptAct_SetBool_Target_325;
	}

	private void Relay_False_325()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_325.False(out logic_uScriptAct_SetBool_Target_325);
		local_ShownMsgMinefieldWarning_System_Boolean = logic_uScriptAct_SetBool_Target_325;
	}

	private void Relay_Save_Out_327()
	{
		Relay_Save_328();
	}

	private void Relay_Load_Out_327()
	{
		Relay_Load_328();
	}

	private void Relay_Restart_Out_327()
	{
		Relay_Set_False_328();
	}

	private void Relay_Save_327()
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_327 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Save(ref logic_SubGraph_SaveLoadBool_boolean_327, logic_SubGraph_SaveLoadBool_boolAsVariable_327, logic_SubGraph_SaveLoadBool_uniqueID_327);
	}

	private void Relay_Load_327()
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_327 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Load(ref logic_SubGraph_SaveLoadBool_boolean_327, logic_SubGraph_SaveLoadBool_boolAsVariable_327, logic_SubGraph_SaveLoadBool_uniqueID_327);
	}

	private void Relay_Set_True_327()
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_327 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_327, logic_SubGraph_SaveLoadBool_boolAsVariable_327, logic_SubGraph_SaveLoadBool_uniqueID_327);
	}

	private void Relay_Set_False_327()
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_327 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_327, logic_SubGraph_SaveLoadBool_boolAsVariable_327, logic_SubGraph_SaveLoadBool_uniqueID_327);
	}

	private void Relay_Save_Out_328()
	{
		Relay_Save_332();
	}

	private void Relay_Load_Out_328()
	{
		Relay_Load_332();
	}

	private void Relay_Restart_Out_328()
	{
		Relay_Set_False_332();
	}

	private void Relay_Save_328()
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_328 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Save(ref logic_SubGraph_SaveLoadBool_boolean_328, logic_SubGraph_SaveLoadBool_boolAsVariable_328, logic_SubGraph_SaveLoadBool_uniqueID_328);
	}

	private void Relay_Load_328()
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_328 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Load(ref logic_SubGraph_SaveLoadBool_boolean_328, logic_SubGraph_SaveLoadBool_boolAsVariable_328, logic_SubGraph_SaveLoadBool_uniqueID_328);
	}

	private void Relay_Set_True_328()
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_328 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_328, logic_SubGraph_SaveLoadBool_boolAsVariable_328, logic_SubGraph_SaveLoadBool_uniqueID_328);
	}

	private void Relay_Set_False_328()
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_328 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_328, logic_SubGraph_SaveLoadBool_boolAsVariable_328, logic_SubGraph_SaveLoadBool_uniqueID_328);
	}

	private void Relay_Save_Out_332()
	{
		Relay_Save_524();
	}

	private void Relay_Load_Out_332()
	{
		Relay_Load_524();
	}

	private void Relay_Restart_Out_332()
	{
		Relay_Set_False_524();
	}

	private void Relay_Save_332()
	{
		logic_SubGraph_SaveLoadBool_boolean_332 = local_SalesTechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_332 = local_SalesTechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Save(ref logic_SubGraph_SaveLoadBool_boolean_332, logic_SubGraph_SaveLoadBool_boolAsVariable_332, logic_SubGraph_SaveLoadBool_uniqueID_332);
	}

	private void Relay_Load_332()
	{
		logic_SubGraph_SaveLoadBool_boolean_332 = local_SalesTechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_332 = local_SalesTechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Load(ref logic_SubGraph_SaveLoadBool_boolean_332, logic_SubGraph_SaveLoadBool_boolAsVariable_332, logic_SubGraph_SaveLoadBool_uniqueID_332);
	}

	private void Relay_Set_True_332()
	{
		logic_SubGraph_SaveLoadBool_boolean_332 = local_SalesTechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_332 = local_SalesTechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_332, logic_SubGraph_SaveLoadBool_boolAsVariable_332, logic_SubGraph_SaveLoadBool_uniqueID_332);
	}

	private void Relay_Set_False_332()
	{
		logic_SubGraph_SaveLoadBool_boolean_332 = local_SalesTechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_332 = local_SalesTechSetUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_332.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_332, logic_SubGraph_SaveLoadBool_boolAsVariable_332, logic_SubGraph_SaveLoadBool_uniqueID_332);
	}

	private void Relay_In_334()
	{
		logic_uScript_FlyTechUpAndAway_tech_334 = local_PaymentPointTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_334 = NPCFlyAwayBehaviour;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_334.In(logic_uScript_FlyTechUpAndAway_tech_334, logic_uScript_FlyTechUpAndAway_maxLifetime_334, logic_uScript_FlyTechUpAndAway_targetHeight_334, logic_uScript_FlyTechUpAndAway_aiTree_334, logic_uScript_FlyTechUpAndAway_removalParticles_334);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_334.Out)
		{
			Relay_Succeed_2();
		}
	}

	private void Relay_In_335()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_335 = msgPromptTextBlackbird;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_335 = msgPromptNoMoney;
		logic_uScript_MissionPromptBlock_Show_targetBlock_335 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_335.In(logic_uScript_MissionPromptBlock_Show_bodyText_335, logic_uScript_MissionPromptBlock_Show_acceptButtonText_335, logic_uScript_MissionPromptBlock_Show_rejectButtonText_335, logic_uScript_MissionPromptBlock_Show_targetBlock_335, logic_uScript_MissionPromptBlock_Show_highlightBlock_335, logic_uScript_MissionPromptBlock_Show_singleUse_335);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_335.Out)
		{
			Relay_False_346();
		}
	}

	private void Relay_True_336()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_336.True(out logic_uScriptAct_SetBool_Target_336);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_336;
	}

	private void Relay_False_336()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_336.False(out logic_uScriptAct_SetBool_Target_336);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_336;
	}

	private void Relay_True_337()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_337.True(out logic_uScriptAct_SetBool_Target_337);
		local_RepeatWaitTime_System_Boolean = logic_uScriptAct_SetBool_Target_337;
	}

	private void Relay_False_337()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_337.False(out logic_uScriptAct_SetBool_Target_337);
		local_RepeatWaitTime_System_Boolean = logic_uScriptAct_SetBool_Target_337;
	}

	private void Relay_In_339()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_339 = msgPromptTextBlackbird;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_339 = msgPromptAccept;
		logic_uScript_MissionPromptBlock_Show_rejectButtonText_339 = msgPromptDecline;
		logic_uScript_MissionPromptBlock_Show_targetBlock_339 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_339.In(logic_uScript_MissionPromptBlock_Show_bodyText_339, logic_uScript_MissionPromptBlock_Show_acceptButtonText_339, logic_uScript_MissionPromptBlock_Show_rejectButtonText_339, logic_uScript_MissionPromptBlock_Show_targetBlock_339, logic_uScript_MissionPromptBlock_Show_highlightBlock_339, logic_uScript_MissionPromptBlock_Show_singleUse_339);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_339.Out)
		{
			Relay_True_346();
		}
	}

	private void Relay_In_345()
	{
		logic_uScript_GetCurrentMoneyEarned_Return_345 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_345.In();
		local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_345;
		if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_345.Out)
		{
			Relay_In_373();
		}
	}

	private void Relay_True_346()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_346.True(out logic_uScriptAct_SetBool_Target_346);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_346;
	}

	private void Relay_False_346()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_346.False(out logic_uScriptAct_SetBool_Target_346);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_346;
	}

	private void Relay_In_349()
	{
		logic_uScriptCon_CompareBool_Bool_349 = local_368_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_349.In(logic_uScriptCon_CompareBool_Bool_349);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_349.True)
		{
			Relay_In_358();
		}
	}

	private void Relay_In_352()
	{
		logic_uScript_CompareBlock_A_352 = local_369_TankBlock;
		logic_uScript_CompareBlock_B_352 = local_TerminalBlock_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_352.In(logic_uScript_CompareBlock_A_352, logic_uScript_CompareBlock_B_352);
		if (logic_uScript_CompareBlock_uScript_CompareBlock_352.EqualTo)
		{
			Relay_In_349();
		}
	}

	private void Relay_In_353()
	{
		logic_uScriptCon_CompareBool_Bool_353 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_353.In(logic_uScriptCon_CompareBool_Bool_353);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_353.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_353.False;
		if (num)
		{
			Relay_In_335();
		}
		if (flag)
		{
			Relay_In_335();
		}
	}

	private void Relay_In_355()
	{
		int num = 0;
		Array array = discoverableBlockTypesOnVehicle;
		if (logic_uScript_DiscoverBlocks_blockTypes_355.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DiscoverBlocks_blockTypes_355, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DiscoverBlocks_blockTypes_355, num, array.Length);
		num += array.Length;
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_355.In(logic_uScript_DiscoverBlocks_blockTypes_355);
		if (logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_355.Out)
		{
			Relay_True_336();
		}
	}

	private void Relay_In_357()
	{
		logic_uScriptCon_CompareBool_Bool_357 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_357.In(logic_uScriptCon_CompareBool_Bool_357);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_357.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_357.False;
		if (num)
		{
			Relay_In_339();
		}
		if (flag)
		{
			Relay_In_339();
		}
	}

	private void Relay_In_358()
	{
		logic_uScriptCon_CompareBool_Bool_358 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_358.In(logic_uScriptCon_CompareBool_Bool_358);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_358.True)
		{
			Relay_In_541();
		}
	}

	private void Relay_In_360()
	{
		logic_uScript_Wait_repeat_360 = local_RepeatWaitTime_System_Boolean;
		logic_uScript_Wait_uScript_Wait_360.In(logic_uScript_Wait_seconds_360, logic_uScript_Wait_repeat_360);
		if (logic_uScript_Wait_uScript_Wait_360.Waited)
		{
			Relay_False_377();
		}
	}

	private void Relay_ResponseEvent_361()
	{
		local_369_TankBlock = event_UnityEngine_GameObject_TankBlock_361;
		local_368_System_Boolean = event_UnityEngine_GameObject_Accepted_361;
		Relay_In_352();
	}

	private void Relay_In_362()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_362 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_362.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_362);
		if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_362.Out)
		{
			Relay_In_360();
		}
	}

	private void Relay_InitialSpawn_371()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_371.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_371, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_371, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_371 = owner_Connection_342;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_371.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_371, logic_uScript_SpawnTechsFromData_ownerNode_371, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_371, logic_uScript_SpawnTechsFromData_allowResurrection_371);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_371.Out)
		{
			Relay_In_462();
		}
	}

	private void Relay_In_373()
	{
		logic_uScriptCon_CompareInt_A_373 = local_CurrentMoney_System_Int32;
		logic_uScriptCon_CompareInt_B_373 = vehicleCost;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_373.In(logic_uScriptCon_CompareInt_A_373, logic_uScriptCon_CompareInt_B_373);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_373.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_373.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_357();
		}
		if (lessThan)
		{
			Relay_In_353();
		}
	}

	private void Relay_True_377()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_377.True(out logic_uScriptAct_SetBool_Target_377);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_377;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_377.Out)
		{
			Relay_True_337();
		}
	}

	private void Relay_False_377()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_377.False(out logic_uScriptAct_SetBool_Target_377);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_377;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_377.Out)
		{
			Relay_True_337();
		}
	}

	private void Relay_In_378()
	{
		logic_uScriptCon_CompareBool_Bool_378 = _DEBUGIgnoreMoneyCheck;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_378.In(logic_uScriptCon_CompareBool_Bool_378);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_378.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_378.False;
		if (num)
		{
			Relay_In_357();
		}
		if (flag)
		{
			Relay_In_345();
		}
	}

	private void Relay_In_379()
	{
		logic_uScriptCon_CompareBool_Bool_379 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_379.In(logic_uScriptCon_CompareBool_Bool_379);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_379.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_379.False;
		if (num)
		{
			Relay_In_362();
		}
		if (flag)
		{
			Relay_In_515();
		}
	}

	private void Relay_In_382()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_382.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_382.Out)
		{
			Relay_In_435();
		}
	}

	private void Relay_In_383()
	{
		logic_uScript_IsTechInTrigger_triggerAreaName_383 = TriggerX;
		int num = 0;
		Array array = local_TechsInMinefield_TankArray;
		if (logic_uScript_IsTechInTrigger_techs_383.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_IsTechInTrigger_techs_383, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_IsTechInTrigger_techs_383, num, array.Length);
		num += array.Length;
		logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_383.In(logic_uScript_IsTechInTrigger_triggerAreaName_383, ref logic_uScript_IsTechInTrigger_techs_383);
		local_TechsInMinefield_TankArray = logic_uScript_IsTechInTrigger_techs_383;
		bool inRange = logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_383.InRange;
		bool outOfRange = logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_383.OutOfRange;
		if (inRange)
		{
			Relay_AtIndex_528();
		}
		if (outOfRange)
		{
			Relay_In_536();
		}
	}

	private void Relay_In_384()
	{
		int num = 0;
		Array array = local_TechsInMinefield_TankArray;
		if (logic_uScript_DamageTechs_techs_384.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_384, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_384, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_384.In(logic_uScript_DamageTechs_techs_384, logic_uScript_DamageTechs_dmgPercent_384, logic_uScript_DamageTechs_givePlyrCredit_384, logic_uScript_DamageTechs_leaveBlksPercent_384, logic_uScript_DamageTechs_makeVulnerable_384);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_384.Out)
		{
			Relay_In_457();
		}
	}

	private void Relay_In_387()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_387.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_387.Out)
		{
			Relay_In_393();
		}
	}

	private void Relay_In_390()
	{
		int num = 0;
		Array msgBuyNewPlaneOffer = MsgBuyNewPlaneOffer;
		if (logic_uScript_AddOnScreenMessage_locString_390.Length != num + msgBuyNewPlaneOffer.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_390, num + msgBuyNewPlaneOffer.Length);
		}
		Array.Copy(msgBuyNewPlaneOffer, 0, logic_uScript_AddOnScreenMessage_locString_390, num, msgBuyNewPlaneOffer.Length);
		num += msgBuyNewPlaneOffer.Length;
		logic_uScript_AddOnScreenMessage_speaker_390 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_390 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_390.In(logic_uScript_AddOnScreenMessage_locString_390, logic_uScript_AddOnScreenMessage_msgPriority_390, logic_uScript_AddOnScreenMessage_holdMsg_390, logic_uScript_AddOnScreenMessage_tag_390, logic_uScript_AddOnScreenMessage_speaker_390, logic_uScript_AddOnScreenMessage_side_390);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_390.Out)
		{
			Relay_True_392();
		}
	}

	private void Relay_True_392()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_392.True(out logic_uScriptAct_SetBool_Target_392);
		local_ShownMsgBuyNewPlaneOffer_System_Boolean = logic_uScriptAct_SetBool_Target_392;
	}

	private void Relay_False_392()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_392.False(out logic_uScriptAct_SetBool_Target_392);
		local_ShownMsgBuyNewPlaneOffer_System_Boolean = logic_uScriptAct_SetBool_Target_392;
	}

	private void Relay_In_393()
	{
		logic_uScriptCon_CompareBool_Bool_393 = local_ShownMsgBuyNewPlaneOffer_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_393.In(logic_uScriptCon_CompareBool_Bool_393);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_393.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_393.False;
		if (num)
		{
			Relay_In_418();
		}
		if (flag)
		{
			Relay_In_390();
		}
	}

	private void Relay_In_396()
	{
		logic_uScript_GetPlayerTankWithBlock_block_396 = PlayerTechPropBlock;
		logic_uScript_GetPlayerTankWithBlock_Return_396 = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_396.In(logic_uScript_GetPlayerTankWithBlock_block_396, logic_uScript_GetPlayerTankWithBlock_tankBlock_396, logic_uScript_GetPlayerTankWithBlock_useBlockType_396);
		bool returned = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_396.Returned;
		bool notReturned = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_396.NotReturned;
		if (returned)
		{
			Relay_In_398();
		}
		if (notReturned)
		{
			Relay_In_387();
		}
	}

	private void Relay_In_398()
	{
		logic_uScript_GetPlayerTankWithBlock_block_398 = PlayerTechBombBlock;
		logic_uScript_GetPlayerTankWithBlock_Return_398 = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_398.In(logic_uScript_GetPlayerTankWithBlock_block_398, logic_uScript_GetPlayerTankWithBlock_tankBlock_398, logic_uScript_GetPlayerTankWithBlock_useBlockType_398);
		bool returned = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_398.Returned;
		bool notReturned = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_398.NotReturned;
		if (returned)
		{
			Relay_In_382();
		}
		if (notReturned)
		{
			Relay_In_393();
		}
	}

	private void Relay_AtIndex_400()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_400.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_400, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_400, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_400.AtIndex(ref logic_uScript_AccessListTech_techList_400, logic_uScript_AccessListTech_index_400, out logic_uScript_AccessListTech_value_400);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_400;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_400;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_400.Out)
		{
			Relay_In_417();
		}
	}

	private void Relay_True_406()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_406.True(out logic_uScriptAct_SetBool_Target_406);
		local_SalesTechSetUp_System_Boolean = logic_uScriptAct_SetBool_Target_406;
	}

	private void Relay_False_406()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_406.False(out logic_uScriptAct_SetBool_Target_406);
		local_SalesTechSetUp_System_Boolean = logic_uScriptAct_SetBool_Target_406;
	}

	private void Relay_InitialSpawn_407()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_SpawnTechsFromData_spawnData_407.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_407, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_SpawnTechsFromData_spawnData_407, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_407 = owner_Connection_410;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_407.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_407, logic_uScript_SpawnTechsFromData_ownerNode_407, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_407, logic_uScript_SpawnTechsFromData_allowResurrection_407);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_407.Out)
		{
			Relay_True_19();
		}
	}

	private void Relay_In_411()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_411.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_411, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_411, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_411 = owner_Connection_404;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_411.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_411, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_411, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_411 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_411.In(logic_uScript_GetAndCheckTechs_techData_411, logic_uScript_GetAndCheckTechs_ownerNode_411, ref logic_uScript_GetAndCheckTechs_techs_411);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_411;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_411.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_411.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_400();
		}
		if (someAlive)
		{
			Relay_AtIndex_400();
		}
	}

	private void Relay_In_415()
	{
		logic_uScript_GetTankBlock_tank_415 = local_PaymentPointTech_Tank;
		logic_uScript_GetTankBlock_blockType_415 = interactableBlockType;
		logic_uScript_GetTankBlock_Return_415 = logic_uScript_GetTankBlock_uScript_GetTankBlock_415.In(logic_uScript_GetTankBlock_tank_415, logic_uScript_GetTankBlock_blockType_415);
		local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_415;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_415.Returned)
		{
			Relay_True_406();
		}
	}

	private void Relay_In_416()
	{
		logic_uScriptCon_CompareBool_Bool_416 = local_SalesTechSetUp_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_416.In(logic_uScriptCon_CompareBool_Bool_416);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_416.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_416.False;
		if (num)
		{
			Relay_In_308();
		}
		if (flag)
		{
			Relay_In_411();
		}
	}

	private void Relay_In_417()
	{
		logic_uScript_SetTankInvulnerable_tank_417 = local_PaymentPointTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_417.In(logic_uScript_SetTankInvulnerable_invulnerable_417, logic_uScript_SetTankInvulnerable_tank_417);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_417.Out)
		{
			Relay_In_415();
		}
	}

	private void Relay_In_418()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_418 = owner_Connection_419;
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_418.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_418, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_418, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_418 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_418.In(logic_uScript_SetOneTechAsEncounterTarget_owner_418, logic_uScript_SetOneTechAsEncounterTarget_techs_418);
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_418.Out)
		{
			Relay_In_379();
		}
	}

	private void Relay_In_422()
	{
		int num = 0;
		Array msgPlayerHitAMine = MsgPlayerHitAMine;
		if (logic_uScript_AddOnScreenMessage_locString_422.Length != num + msgPlayerHitAMine.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_422, num + msgPlayerHitAMine.Length);
		}
		Array.Copy(msgPlayerHitAMine, 0, logic_uScript_AddOnScreenMessage_locString_422, num, msgPlayerHitAMine.Length);
		num += msgPlayerHitAMine.Length;
		logic_uScript_AddOnScreenMessage_speaker_422 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_422 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_422.In(logic_uScript_AddOnScreenMessage_locString_422, logic_uScript_AddOnScreenMessage_msgPriority_422, logic_uScript_AddOnScreenMessage_holdMsg_422, logic_uScript_AddOnScreenMessage_tag_422, logic_uScript_AddOnScreenMessage_speaker_422, logic_uScript_AddOnScreenMessage_side_422);
	}

	private void Relay_In_424()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_424 = TriggerG;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_424.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_424);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_424.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_424.OutOfRange;
		if (inRange)
		{
			Relay_In_436();
		}
		if (outOfRange)
		{
			Relay_UnPause_444();
		}
	}

	private void Relay_In_425()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_425.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_425.Out)
		{
			Relay_In_446();
		}
	}

	private void Relay_In_429()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_429.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_429, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_429, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_429 = owner_Connection_430;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_429.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_429, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_429, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_429 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_429.In(logic_uScript_GetAndCheckTechs_techData_429, logic_uScript_GetAndCheckTechs_ownerNode_429, ref logic_uScript_GetAndCheckTechs_techs_429);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_429;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_429.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_429.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_429.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_429.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_431();
		}
		if (someAlive)
		{
			Relay_AtIndex_431();
		}
		if (allDead)
		{
			Relay_In_433();
		}
		if (waitingToSpawn)
		{
			Relay_In_433();
		}
	}

	private void Relay_AtIndex_431()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_431.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_431, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_431, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_431.AtIndex(ref logic_uScript_AccessListTech_techList_431, logic_uScript_AccessListTech_index_431, out logic_uScript_AccessListTech_value_431);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_431;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_431;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_431.Out)
		{
			Relay_In_334();
		}
	}

	private void Relay_In_433()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_433.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_433.Out)
		{
			Relay_In_549();
		}
	}

	private void Relay_In_434()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_434.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_434.Out)
		{
			Relay_Succeed_2();
		}
	}

	private void Relay_In_435()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_435.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_435.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_436()
	{
		logic_uScript_GetPlayerTank_Return_436 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_436.In();
		local_PlayerTech_Tank = logic_uScript_GetPlayerTank_Return_436;
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_436.Returned)
		{
			Relay_Pause_72();
		}
	}

	private void Relay_In_442()
	{
		int num = 0;
		Array array = local_TechsInMinefield_TankArray;
		if (logic_uScript_DamageTechs_techs_442.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_442, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_442, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_442.In(logic_uScript_DamageTechs_techs_442, logic_uScript_DamageTechs_dmgPercent_442, logic_uScript_DamageTechs_givePlyrCredit_442, logic_uScript_DamageTechs_leaveBlksPercent_442, logic_uScript_DamageTechs_makeVulnerable_442);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_442.Out)
		{
			Relay_In_443();
		}
	}

	private void Relay_In_443()
	{
		int num = 0;
		Array msgEnemyHitAMine = MsgEnemyHitAMine;
		if (logic_uScript_AddOnScreenMessage_locString_443.Length != num + msgEnemyHitAMine.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_443, num + msgEnemyHitAMine.Length);
		}
		Array.Copy(msgEnemyHitAMine, 0, logic_uScript_AddOnScreenMessage_locString_443, num, msgEnemyHitAMine.Length);
		num += msgEnemyHitAMine.Length;
		logic_uScript_AddOnScreenMessage_speaker_443 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_443 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_443.In(logic_uScript_AddOnScreenMessage_locString_443, logic_uScript_AddOnScreenMessage_msgPriority_443, logic_uScript_AddOnScreenMessage_holdMsg_443, logic_uScript_AddOnScreenMessage_tag_443, logic_uScript_AddOnScreenMessage_speaker_443, logic_uScript_AddOnScreenMessage_side_443);
	}

	private void Relay_Pause_444()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_444.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_444.Out)
		{
			Relay_In_425();
		}
	}

	private void Relay_UnPause_444()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_444.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_444.Out)
		{
			Relay_In_425();
		}
	}

	private void Relay_In_445()
	{
		logic_uScript_GetPlayerTank_Return_445 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_445.In();
		local_PlayerTech_Tank = logic_uScript_GetPlayerTank_Return_445;
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_445.Returned)
		{
			Relay_UnPause_79();
		}
	}

	private void Relay_In_446()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_446.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_446.Out)
		{
			Relay_In_237();
		}
	}

	private void Relay_AtIndex_452()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_452.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_452, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_452, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_452.AtIndex(ref logic_uScript_AccessListTech_techList_452, logic_uScript_AccessListTech_index_452, out logic_uScript_AccessListTech_value_452);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_452;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_452;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_452.Out)
		{
			Relay_In_453();
		}
	}

	private void Relay_In_453()
	{
		logic_uScript_GetTankBlock_tank_453 = local_PaymentPointTech_Tank;
		logic_uScript_GetTankBlock_blockType_453 = interactableBlockType;
		logic_uScript_GetTankBlock_Return_453 = logic_uScript_GetTankBlock_uScript_GetTankBlock_453.In(logic_uScript_GetTankBlock_tank_453, logic_uScript_GetTankBlock_blockType_453);
		local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_453;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_453.Returned)
		{
			Relay_In_396();
		}
	}

	private void Relay_In_454()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_454.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_454, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_454, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_454 = owner_Connection_455;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_454.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_454, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_454, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_454 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_454.In(logic_uScript_GetAndCheckTechs_techData_454, logic_uScript_GetAndCheckTechs_ownerNode_454, ref logic_uScript_GetAndCheckTechs_techs_454);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_454;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_454.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_454.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_452();
		}
		if (someAlive)
		{
			Relay_AtIndex_452();
		}
	}

	private void Relay_In_457()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_457.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_457.SinglePlayer)
		{
			Relay_In_422();
		}
	}

	private void Relay_In_458()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_458.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_458, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_458, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_458 = owner_Connection_463;
		logic_uScript_GetAndCheckTechs_Return_458 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_458.In(logic_uScript_GetAndCheckTechs_techData_458, logic_uScript_GetAndCheckTechs_ownerNode_458, ref logic_uScript_GetAndCheckTechs_techs_458);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_458.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_458.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_458.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_458.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_464();
		}
		if (someAlive)
		{
			Relay_In_464();
		}
		if (allDead)
		{
			Relay_InitialSpawn_371();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_371();
		}
	}

	private void Relay_In_461()
	{
		logic_uScript_AddMoney_amount_461 = local_459_System_Int32;
		logic_uScript_AddMoney_uScript_AddMoney_461.In(logic_uScript_AddMoney_amount_461);
		if (logic_uScript_AddMoney_uScript_AddMoney_461.Out)
		{
			Relay_In_355();
		}
	}

	private void Relay_In_462()
	{
		logic_uScriptAct_MultiplyInt_v2_A_462 = vehicleCost;
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_462.In(logic_uScriptAct_MultiplyInt_v2_A_462, logic_uScriptAct_MultiplyInt_v2_B_462, out logic_uScriptAct_MultiplyInt_v2_IntResult_462, out logic_uScriptAct_MultiplyInt_v2_FloatResult_462);
		local_459_System_Int32 = logic_uScriptAct_MultiplyInt_v2_IntResult_462;
		if (logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_462.Out)
		{
			Relay_In_461();
		}
	}

	private void Relay_In_464()
	{
		int num = 0;
		Array array = vehicleSpawnData2;
		if (logic_uScript_GetAndCheckTechs_techData_464.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_464, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_464, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_464 = owner_Connection_465;
		logic_uScript_GetAndCheckTechs_Return_464 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_464.In(logic_uScript_GetAndCheckTechs_techData_464, logic_uScript_GetAndCheckTechs_ownerNode_464, ref logic_uScript_GetAndCheckTechs_techs_464);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_464.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_464.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_464.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_464.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_471();
		}
		if (someAlive)
		{
			Relay_In_471();
		}
		if (allDead)
		{
			Relay_InitialSpawn_467();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_467();
		}
	}

	private void Relay_InitialSpawn_467()
	{
		int num = 0;
		Array array = vehicleSpawnData2;
		if (logic_uScript_SpawnTechsFromData_spawnData_467.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_467, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_467, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_467 = owner_Connection_468;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_467.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_467, logic_uScript_SpawnTechsFromData_ownerNode_467, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_467, logic_uScript_SpawnTechsFromData_allowResurrection_467);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_467.Out)
		{
			Relay_In_462();
		}
	}

	private void Relay_In_471()
	{
		int num = 0;
		Array array = vehicleSpawnData3;
		if (logic_uScript_GetAndCheckTechs_techData_471.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_471, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_471, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_471 = owner_Connection_473;
		logic_uScript_GetAndCheckTechs_Return_471 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_471.In(logic_uScript_GetAndCheckTechs_techData_471, logic_uScript_GetAndCheckTechs_ownerNode_471, ref logic_uScript_GetAndCheckTechs_techs_471);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_471.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_471.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_471.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_471.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_475();
		}
		if (someAlive)
		{
			Relay_In_475();
		}
		if (allDead)
		{
			Relay_InitialSpawn_472();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_472();
		}
	}

	private void Relay_InitialSpawn_472()
	{
		int num = 0;
		Array array = vehicleSpawnData3;
		if (logic_uScript_SpawnTechsFromData_spawnData_472.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_472, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_472, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_472 = owner_Connection_470;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_472.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_472, logic_uScript_SpawnTechsFromData_ownerNode_472, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_472, logic_uScript_SpawnTechsFromData_allowResurrection_472);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_472.Out)
		{
			Relay_In_462();
		}
	}

	private void Relay_InitialSpawn_474()
	{
		int num = 0;
		Array array = vehicleSpawnData4;
		if (logic_uScript_SpawnTechsFromData_spawnData_474.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_474, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_474, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_474 = owner_Connection_478;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_474.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_474, logic_uScript_SpawnTechsFromData_ownerNode_474, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_474, logic_uScript_SpawnTechsFromData_allowResurrection_474);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_474.Out)
		{
			Relay_In_462();
		}
	}

	private void Relay_In_475()
	{
		int num = 0;
		Array array = vehicleSpawnData4;
		if (logic_uScript_GetAndCheckTechs_techData_475.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_475, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_475, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_475 = owner_Connection_477;
		logic_uScript_GetAndCheckTechs_Return_475 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_475.In(logic_uScript_GetAndCheckTechs_techData_475, logic_uScript_GetAndCheckTechs_ownerNode_475, ref logic_uScript_GetAndCheckTechs_techs_475);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_475.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_475.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_475.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_475.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_480();
		}
		if (someAlive)
		{
			Relay_In_480();
		}
		if (allDead)
		{
			Relay_InitialSpawn_474();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_474();
		}
	}

	private void Relay_In_480()
	{
		int num = 0;
		Array array = vehicleSpawnData5;
		if (logic_uScript_GetAndCheckTechs_techData_480.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_480, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_480, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_480 = owner_Connection_481;
		logic_uScript_GetAndCheckTechs_Return_480 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_480.In(logic_uScript_GetAndCheckTechs_techData_480, logic_uScript_GetAndCheckTechs_ownerNode_480, ref logic_uScript_GetAndCheckTechs_techs_480);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_480.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_480.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_480.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_480.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_486();
		}
		if (someAlive)
		{
			Relay_In_486();
		}
		if (allDead)
		{
			Relay_InitialSpawn_482();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_482();
		}
	}

	private void Relay_InitialSpawn_482()
	{
		int num = 0;
		Array array = vehicleSpawnData5;
		if (logic_uScript_SpawnTechsFromData_spawnData_482.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_482, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_482, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_482 = owner_Connection_479;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_482.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_482, logic_uScript_SpawnTechsFromData_ownerNode_482, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_482, logic_uScript_SpawnTechsFromData_allowResurrection_482);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_482.Out)
		{
			Relay_In_462();
		}
	}

	private void Relay_In_486()
	{
		int num = 0;
		Array msgTooManyPlanesSpawnedAlready = MsgTooManyPlanesSpawnedAlready;
		if (logic_uScript_AddOnScreenMessage_locString_486.Length != num + msgTooManyPlanesSpawnedAlready.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_486, num + msgTooManyPlanesSpawnedAlready.Length);
		}
		Array.Copy(msgTooManyPlanesSpawnedAlready, 0, logic_uScript_AddOnScreenMessage_locString_486, num, msgTooManyPlanesSpawnedAlready.Length);
		num += msgTooManyPlanesSpawnedAlready.Length;
		logic_uScript_AddOnScreenMessage_speaker_486 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_486 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_486.In(logic_uScript_AddOnScreenMessage_locString_486, logic_uScript_AddOnScreenMessage_msgPriority_486, logic_uScript_AddOnScreenMessage_holdMsg_486, logic_uScript_AddOnScreenMessage_tag_486, logic_uScript_AddOnScreenMessage_speaker_486, logic_uScript_AddOnScreenMessage_side_486);
	}

	private void Relay_In_487()
	{
		int num = 0;
		Array turretData = TurretData1;
		if (logic_uScript_GetAndCheckTechs_techData_487.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_487, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_GetAndCheckTechs_techData_487, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_487 = owner_Connection_489;
		int num2 = 0;
		Array array = local_Turrets1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_487.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_487, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_487, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_487 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487.In(logic_uScript_GetAndCheckTechs_techData_487, logic_uScript_GetAndCheckTechs_ownerNode_487, ref logic_uScript_GetAndCheckTechs_techs_487);
		local_Turrets1_TankArray = logic_uScript_GetAndCheckTechs_techs_487;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_487.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_491();
		}
		if (someAlive)
		{
			Relay_In_491();
		}
		if (allDead)
		{
			Relay_In_506();
		}
		if (waitingToSpawn)
		{
			Relay_In_506();
		}
	}

	private void Relay_In_491()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_491 = owner_Connection_492;
		int num = 0;
		Array array = local_Turrets1_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_491.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_491, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_491, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_491 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_491.In(logic_uScript_SetOneTechAsEncounterTarget_owner_491, logic_uScript_SetOneTechAsEncounterTarget_techs_491);
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_491.Out)
		{
			Relay_In_505();
		}
	}

	private void Relay_In_496()
	{
		int num = 0;
		Array turretData = TurretData2;
		if (logic_uScript_GetAndCheckTechs_techData_496.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_496, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_GetAndCheckTechs_techData_496, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_496 = owner_Connection_494;
		int num2 = 0;
		Array array = local_Turrets2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_496.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_496, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_496, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_496 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_496.In(logic_uScript_GetAndCheckTechs_techData_496, logic_uScript_GetAndCheckTechs_ownerNode_496, ref logic_uScript_GetAndCheckTechs_techs_496);
		local_Turrets2_TankArray = logic_uScript_GetAndCheckTechs_techs_496;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_496.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_496.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_496.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_496.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_502();
		}
		if (someAlive)
		{
			Relay_In_502();
		}
		if (allDead)
		{
			Relay_In_508();
		}
		if (waitingToSpawn)
		{
			Relay_In_508();
		}
	}

	private void Relay_In_500()
	{
		int num = 0;
		Array turretData = TurretData3;
		if (logic_uScript_GetAndCheckTechs_techData_500.Length != num + turretData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_500, num + turretData.Length);
		}
		Array.Copy(turretData, 0, logic_uScript_GetAndCheckTechs_techData_500, num, turretData.Length);
		num += turretData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_500 = owner_Connection_497;
		int num2 = 0;
		Array array = local_Turrets3_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_500.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_500, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_500, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_500 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_500.In(logic_uScript_GetAndCheckTechs_techData_500, logic_uScript_GetAndCheckTechs_ownerNode_500, ref logic_uScript_GetAndCheckTechs_techs_500);
		local_Turrets3_TankArray = logic_uScript_GetAndCheckTechs_techs_500;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_500.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_500.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_500.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_500.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_504();
		}
		if (someAlive)
		{
			Relay_In_504();
		}
		if (allDead)
		{
			Relay_In_510();
		}
		if (waitingToSpawn)
		{
			Relay_In_510();
		}
	}

	private void Relay_In_502()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_502 = owner_Connection_501;
		int num = 0;
		Array array = local_Turrets2_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_502.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_502, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_502, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_502 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_502.In(logic_uScript_SetOneTechAsEncounterTarget_owner_502, logic_uScript_SetOneTechAsEncounterTarget_techs_502);
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_502.Out)
		{
			Relay_In_507();
		}
	}

	private void Relay_In_504()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_504 = owner_Connection_503;
		int num = 0;
		Array array = local_Turrets3_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_504.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_504, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_504, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_504 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_504.In(logic_uScript_SetOneTechAsEncounterTarget_owner_504, logic_uScript_SetOneTechAsEncounterTarget_techs_504);
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_504.Out)
		{
			Relay_In_511();
		}
	}

	private void Relay_In_505()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_505.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_505.Out)
		{
			Relay_In_507();
		}
	}

	private void Relay_In_506()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_506.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_506.Out)
		{
			Relay_In_496();
		}
	}

	private void Relay_In_507()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_507.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_507.Out)
		{
			Relay_In_509();
		}
	}

	private void Relay_In_508()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_508.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_508.Out)
		{
			Relay_In_500();
		}
	}

	private void Relay_In_509()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_509.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_509.Out)
		{
			Relay_In_511();
		}
	}

	private void Relay_In_510()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_510.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_510.Out)
		{
			Relay_In_511();
		}
	}

	private void Relay_In_511()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_511.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_511.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_In_514()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_514 = msgPromptTextBlackbird;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_514 = msgPromptAccessDenied;
		logic_uScript_MissionPromptBlock_Show_targetBlock_514 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_514.In(logic_uScript_MissionPromptBlock_Show_bodyText_514, logic_uScript_MissionPromptBlock_Show_acceptButtonText_514, logic_uScript_MissionPromptBlock_Show_rejectButtonText_514, logic_uScript_MissionPromptBlock_Show_targetBlock_514, logic_uScript_MissionPromptBlock_Show_highlightBlock_514, logic_uScript_MissionPromptBlock_Show_singleUse_514);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_514.Out)
		{
			Relay_In_518();
		}
	}

	private void Relay_In_515()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_515.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_515, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_515, num, array.Length);
		num += array.Length;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_515.In(logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_515, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_515);
		bool num2 = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_515.True;
		bool flag = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_515.False;
		if (num2)
		{
			Relay_False_521();
		}
		if (flag)
		{
			Relay_In_516();
		}
	}

	private void Relay_In_516()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_516.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_516.Out)
		{
			Relay_True_520();
		}
	}

	private void Relay_In_518()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_518.In();
	}

	private void Relay_True_520()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_520.True(out logic_uScriptAct_SetBool_Target_520);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_520;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_520.Out)
		{
			Relay_In_514();
		}
	}

	private void Relay_False_520()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_520.False(out logic_uScriptAct_SetBool_Target_520);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_520;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_520.Out)
		{
			Relay_In_514();
		}
	}

	private void Relay_True_521()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_521.True(out logic_uScriptAct_SetBool_Target_521);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_521;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_521.Out)
		{
			Relay_In_378();
		}
	}

	private void Relay_False_521()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_521.False(out logic_uScriptAct_SetBool_Target_521);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_521;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_521.Out)
		{
			Relay_In_378();
		}
	}

	private void Relay_Save_Out_524()
	{
	}

	private void Relay_Load_Out_524()
	{
	}

	private void Relay_Restart_Out_524()
	{
	}

	private void Relay_Save_524()
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_524 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Save(ref logic_SubGraph_SaveLoadBool_boolean_524, logic_SubGraph_SaveLoadBool_boolAsVariable_524, logic_SubGraph_SaveLoadBool_uniqueID_524);
	}

	private void Relay_Load_524()
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_524 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Load(ref logic_SubGraph_SaveLoadBool_boolean_524, logic_SubGraph_SaveLoadBool_boolAsVariable_524, logic_SubGraph_SaveLoadBool_uniqueID_524);
	}

	private void Relay_Set_True_524()
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_524 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_524, logic_SubGraph_SaveLoadBool_boolAsVariable_524, logic_SubGraph_SaveLoadBool_uniqueID_524);
	}

	private void Relay_Set_False_524()
	{
		logic_SubGraph_SaveLoadBool_boolean_524 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_524 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_524.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_524, logic_SubGraph_SaveLoadBool_boolAsVariable_524, logic_SubGraph_SaveLoadBool_uniqueID_524);
	}

	private void Relay_In_525()
	{
		logic_uScript_IsTechWheelGrounded_tech_525 = local_PlayerTech_Tank;
		logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_525.In(logic_uScript_IsTechWheelGrounded_tech_525);
		bool num = logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_525.True;
		bool flag = logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_525.False;
		if (num)
		{
			Relay_In_320();
		}
		if (flag)
		{
			Relay_In_526();
		}
	}

	private void Relay_In_526()
	{
		logic_uScript_IsTechTouchingTerrain_tech_526 = local_PlayerTech_Tank;
		logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_526.In(logic_uScript_IsTechTouchingTerrain_tech_526);
		bool num = logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_526.True;
		bool flag = logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_526.False;
		if (num)
		{
			Relay_In_320();
		}
		if (flag)
		{
			Relay_In_313();
		}
	}

	private void Relay_AtIndex_528()
	{
		int num = 0;
		Array array = local_TechsInMinefield_TankArray;
		if (logic_uScript_AccessListTech_techList_528.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_528, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_528, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_528.AtIndex(ref logic_uScript_AccessListTech_techList_528, logic_uScript_AccessListTech_index_528, out logic_uScript_AccessListTech_value_528);
		local_TechsInMinefield_TankArray = logic_uScript_AccessListTech_techList_528;
		local_MinefieldTech_Tank = logic_uScript_AccessListTech_value_528;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_528.Out)
		{
			Relay_In_531();
		}
	}

	private void Relay_In_530()
	{
		logic_uScript_IsTechPlayer_tech_530 = local_MinefieldTech_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_530.In(logic_uScript_IsTechPlayer_tech_530);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_530.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_530.False;
		if (num)
		{
			Relay_In_384();
		}
		if (flag)
		{
			Relay_In_442();
		}
	}

	private void Relay_In_531()
	{
		logic_uScript_IsTechWheelGrounded_tech_531 = local_MinefieldTech_Tank;
		logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_531.In(logic_uScript_IsTechWheelGrounded_tech_531);
		bool num = logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_531.True;
		bool flag = logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_531.False;
		if (num)
		{
			Relay_In_533();
		}
		if (flag)
		{
			Relay_In_532();
		}
	}

	private void Relay_In_532()
	{
		logic_uScript_IsTechTouchingTerrain_tech_532 = local_MinefieldTech_Tank;
		logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_532.In(logic_uScript_IsTechTouchingTerrain_tech_532);
		bool num = logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_532.True;
		bool flag = logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_532.False;
		if (num)
		{
			Relay_In_534();
		}
		if (flag)
		{
			Relay_In_538();
		}
	}

	private void Relay_In_533()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_533.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_533.Out)
		{
			Relay_In_534();
		}
	}

	private void Relay_In_534()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_534.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_534.Out)
		{
			Relay_In_539();
		}
	}

	private void Relay_In_536()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_536.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_536.Out)
		{
			Relay_In_537();
		}
	}

	private void Relay_In_537()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_537.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_537.Out)
		{
			Relay_In_547();
		}
	}

	private void Relay_In_538()
	{
		logic_uScript_IsTechAnchored_tech_538 = local_MinefieldTech_Tank;
		logic_uScript_IsTechAnchored_uScript_IsTechAnchored_538.In(logic_uScript_IsTechAnchored_tech_538);
		if (logic_uScript_IsTechAnchored_uScript_IsTechAnchored_538.True)
		{
			Relay_In_539();
		}
	}

	private void Relay_In_539()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_539.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_539.Out)
		{
			Relay_In_530();
		}
	}

	private void Relay_In_541()
	{
		logic_uScriptCon_CompareBool_Bool_541 = local_BlockLimitCritical_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541.In(logic_uScriptCon_CompareBool_Bool_541);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541.False;
		if (num)
		{
			Relay_In_545();
		}
		if (flag)
		{
			Relay_In_458();
		}
	}

	private void Relay_In_545()
	{
		int num = 0;
		Array array = msgDespawnTechs;
		if (logic_uScript_AddOnScreenMessage_locString_545.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_545, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_545, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_545 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_545 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_545.In(logic_uScript_AddOnScreenMessage_locString_545, logic_uScript_AddOnScreenMessage_msgPriority_545, logic_uScript_AddOnScreenMessage_holdMsg_545, logic_uScript_AddOnScreenMessage_tag_545, logic_uScript_AddOnScreenMessage_speaker_545, logic_uScript_AddOnScreenMessage_side_545);
	}

	private void Relay_In_547()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_547.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_547.Out)
		{
			Relay_In_548();
		}
	}

	private void Relay_In_548()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_548.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_548.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_549()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_DestroyTechsFromData_techData_549.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_549, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_DestroyTechsFromData_techData_549, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_549 = owner_Connection_430;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_549.In(logic_uScript_DestroyTechsFromData_techData_549, logic_uScript_DestroyTechsFromData_shouldExplode_549, logic_uScript_DestroyTechsFromData_ownerNode_549);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_549.Out)
		{
			Relay_In_434();
		}
	}

	private void Relay_True_551()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_551.True(out logic_uScriptAct_SetBool_Target_551);
		local_TriggerEnteredA_System_Boolean = logic_uScriptAct_SetBool_Target_551;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_551.Out)
		{
			Relay_In_155();
		}
	}

	private void Relay_False_551()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_551.False(out logic_uScriptAct_SetBool_Target_551);
		local_TriggerEnteredA_System_Boolean = logic_uScriptAct_SetBool_Target_551;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_551.Out)
		{
			Relay_In_155();
		}
	}

	private void Relay_True_553()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_553.True(out logic_uScriptAct_SetBool_Target_553);
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_553.Out)
		{
			Relay_In_160();
		}
	}

	private void Relay_False_553()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_553.False(out logic_uScriptAct_SetBool_Target_553);
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_553.Out)
		{
			Relay_In_160();
		}
	}

	private void Relay_True_555()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_555.True(out logic_uScriptAct_SetBool_Target_555);
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_555.Out)
		{
			Relay_In_209();
		}
	}

	private void Relay_False_555()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_555.False(out logic_uScriptAct_SetBool_Target_555);
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_555.Out)
		{
			Relay_In_209();
		}
	}

	private void Relay_ShowLabel_556()
	{
		logic_uScriptAct_PrintText_Text_556 = local_567_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_556.ShowLabel(logic_uScriptAct_PrintText_Text_556, logic_uScriptAct_PrintText_FontSize_556, logic_uScriptAct_PrintText_FontStyle_556, logic_uScriptAct_PrintText_FontColor_556, logic_uScriptAct_PrintText_textAnchor_556, logic_uScriptAct_PrintText_EdgePadding_556, logic_uScriptAct_PrintText_time_556);
	}

	private void Relay_HideLabel_556()
	{
		logic_uScriptAct_PrintText_Text_556 = local_567_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_556.HideLabel(logic_uScriptAct_PrintText_Text_556, logic_uScriptAct_PrintText_FontSize_556, logic_uScriptAct_PrintText_FontStyle_556, logic_uScriptAct_PrintText_FontColor_556, logic_uScriptAct_PrintText_textAnchor_556, logic_uScriptAct_PrintText_EdgePadding_556, logic_uScriptAct_PrintText_time_556);
	}

	private void Relay_In_558()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_558.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_558, num + 1);
		}
		logic_uScriptAct_Concatenate_A_558[num++] = local_562_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_558.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_558, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_558[num2++] = local_TriggerEnteredA_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_558.In(logic_uScriptAct_Concatenate_A_558, logic_uScriptAct_Concatenate_B_558, logic_uScriptAct_Concatenate_Separator_558, out logic_uScriptAct_Concatenate_Result_558);
		local_569_System_String = logic_uScriptAct_Concatenate_Result_558;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_558.Out)
		{
			Relay_In_565();
		}
	}

	private void Relay_In_561()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_561.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_561, num + 1);
		}
		logic_uScriptAct_Concatenate_A_561[num++] = local_560_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_561.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_561, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_561[num2++] = local_Stage_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_561.In(logic_uScriptAct_Concatenate_A_561, logic_uScriptAct_Concatenate_B_561, logic_uScriptAct_Concatenate_Separator_561, out logic_uScriptAct_Concatenate_Result_561);
		local_562_System_String = logic_uScriptAct_Concatenate_Result_561;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_561.Out)
		{
			Relay_In_558();
		}
	}

	private void Relay_In_563()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_563.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_563, num + 1);
		}
		logic_uScriptAct_Concatenate_A_563[num++] = local_566_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_563.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_563, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_563[num2++] = local_TriggerEnteredC_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_563.In(logic_uScriptAct_Concatenate_A_563, logic_uScriptAct_Concatenate_B_563, logic_uScriptAct_Concatenate_Separator_563, out logic_uScriptAct_Concatenate_Result_563);
		local_567_System_String = logic_uScriptAct_Concatenate_Result_563;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_563.Out)
		{
			Relay_ShowLabel_556();
		}
	}

	private void Relay_In_565()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_565.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_565, num + 1);
		}
		logic_uScriptAct_Concatenate_A_565[num++] = local_569_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_565.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_565, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_565[num2++] = local_TriggerEnteredB_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_565.In(logic_uScriptAct_Concatenate_A_565, logic_uScriptAct_Concatenate_B_565, logic_uScriptAct_Concatenate_Separator_565, out logic_uScriptAct_Concatenate_Result_565);
		local_566_System_String = logic_uScriptAct_Concatenate_Result_565;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_565.Out)
		{
			Relay_In_563();
		}
	}

	private void Relay_True_571()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_571.True(out logic_uScriptAct_SetBool_Target_571);
		local_TriggerEnteredB_System_Boolean = logic_uScriptAct_SetBool_Target_571;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_571.Out)
		{
			Relay_False_572();
		}
	}

	private void Relay_False_571()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_571.False(out logic_uScriptAct_SetBool_Target_571);
		local_TriggerEnteredB_System_Boolean = logic_uScriptAct_SetBool_Target_571;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_571.Out)
		{
			Relay_False_572();
		}
	}

	private void Relay_True_572()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_572.True(out logic_uScriptAct_SetBool_Target_572);
		local_TriggerEnteredC_System_Boolean = logic_uScriptAct_SetBool_Target_572;
	}

	private void Relay_False_572()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_572.False(out logic_uScriptAct_SetBool_Target_572);
		local_TriggerEnteredC_System_Boolean = logic_uScriptAct_SetBool_Target_572;
	}

	private void Relay_True_575()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_575.True(out logic_uScriptAct_SetBool_Target_575);
		local_TriggerEnteredA_System_Boolean = logic_uScriptAct_SetBool_Target_575;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_575.Out)
		{
			Relay_False_577();
		}
	}

	private void Relay_False_575()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_575.False(out logic_uScriptAct_SetBool_Target_575);
		local_TriggerEnteredA_System_Boolean = logic_uScriptAct_SetBool_Target_575;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_575.Out)
		{
			Relay_False_577();
		}
	}

	private void Relay_True_577()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_577.True(out logic_uScriptAct_SetBool_Target_577);
		local_TriggerEnteredC_System_Boolean = logic_uScriptAct_SetBool_Target_577;
	}

	private void Relay_False_577()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_577.False(out logic_uScriptAct_SetBool_Target_577);
		local_TriggerEnteredC_System_Boolean = logic_uScriptAct_SetBool_Target_577;
	}

	private void Relay_True_579()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_579.True(out logic_uScriptAct_SetBool_Target_579);
		local_TriggerEnteredA_System_Boolean = logic_uScriptAct_SetBool_Target_579;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_579.Out)
		{
			Relay_False_581();
		}
	}

	private void Relay_False_579()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_579.False(out logic_uScriptAct_SetBool_Target_579);
		local_TriggerEnteredA_System_Boolean = logic_uScriptAct_SetBool_Target_579;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_579.Out)
		{
			Relay_False_581();
		}
	}

	private void Relay_True_581()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_581.True(out logic_uScriptAct_SetBool_Target_581);
		local_TriggerEnteredB_System_Boolean = logic_uScriptAct_SetBool_Target_581;
	}

	private void Relay_False_581()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_581.False(out logic_uScriptAct_SetBool_Target_581);
		local_TriggerEnteredB_System_Boolean = logic_uScriptAct_SetBool_Target_581;
	}
}
