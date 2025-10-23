using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_DefeatEnemyTechs", "")]
public class Mission_SetPiece_HordeCrater_01 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool _DEBUGIgnoreMoneyCheck;

	public BlockTypes[] discoverableBlockTypesOnVehicle = new BlockTypes[0];

	public SpawnTechData enemyGroup1Data;

	public SpawnTechData enemyGroup2Data;

	public SpawnTechData enemyGroup3Data;

	public SpawnTechData enemyGroup4Data;

	public SpawnTechData enemyGroup5Data;

	public ExternalBehaviorTree FlyAwayBehaviour;

	public BlockTypes interactableBlockType;

	private float local_102_System_Single;

	private Tank local_1068_Tank;

	private string local_191_System_String = "msgNPC";

	private float local_239_System_Single;

	private float local_240_System_Single;

	private string local_244_System_String = "msgNPC";

	private string local_252_System_String = "msgNPC";

	private string local_275_System_String = "msgNPC";

	private string local_282_System_String = "msgNPC";

	private float local_360_System_Single;

	private float local_365_System_Single;

	private float local_367_System_Single;

	private float local_371_System_Single;

	private string local_54_System_String = "msgNPC";

	private bool local_619_System_Boolean;

	private TankBlock local_635_TankBlock;

	private float local_711_System_Single;

	private int local_808_System_Int32;

	private Tank[] local_921_TankArray = new Tank[0];

	private Tank[] local_927_TankArray = new Tank[0];

	private bool local_AdditionalVehiclePurchased_System_Boolean;

	private bool local_BlockLimitCritical_System_Boolean;

	private bool local_BuyMenuOpened_System_Boolean;

	private int local_CurrentMoney_System_Int32;

	private bool local_EnemyAlive1_System_Boolean;

	private bool local_EnemyAlive2_System_Boolean;

	private bool local_EnemyAlive3_System_Boolean;

	private bool local_EnemyAlive4_System_Boolean;

	private bool local_EnemyAlive5_System_Boolean;

	private bool local_FinishedJoke_System_Boolean;

	private bool local_HasEnoughMoney_System_Boolean;

	private int local_MaxPlayers_System_Int32;

	private Tank[] local_MobTechs1_TankArray = new Tank[0];

	private Tank[] local_MobTechs2_TankArray = new Tank[0];

	private Tank[] local_MobTechs3_TankArray = new Tank[0];

	private Tank[] local_MobTechs4_TankArray = new Tank[0];

	private Tank[] local_MobTechs5_TankArray = new Tank[0];

	private bool local_msg03aShown_System_Boolean;

	private bool local_msg03bShown_System_Boolean;

	private bool local_msgAllPlayersMustBeInShown_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgPurchaseVehicle_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgPurchaseVehicle_Pad_ManOnScreenMessages_OnScreenMessage;

	private bool local_msgRebuyShown_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgSwitchTech_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgSwitchTech_Pad_ManOnScreenMessages_OnScreenMessage;

	private string local_msgTagSwitchedTech_System_String = "";

	private string local_msgTagTestComplete_System_String = "";

	private bool local_NewWeaponAttached_System_Boolean;

	private bool local_NPCMet_System_Boolean;

	private Tank[] local_NPCPaymentPoints_TankArray = new Tank[0];

	private bool local_NPCSeen_System_Boolean;

	private Tank local_NPCTank_Tank;

	private Tank[] local_NPCTanks_TankArray = new Tank[0];

	private bool local_ObjectiveComplete_System_Boolean;

	private bool local_OutOfBounds_System_Boolean;

	private Tank local_PaymentPointTech_Tank;

	private bool local_PlayerDead_System_Boolean;

	private bool local_PlayerDetachedWeapon_System_Boolean;

	private bool local_PlayerSpawnedTech_System_Boolean;

	private bool local_PlayerSwitchedTech_System_Boolean;

	private bool local_Rebriefed_System_Boolean;

	private bool local_RepeatWaitTime_System_Boolean;

	private bool local_RetryingTest_System_Boolean;

	private bool local_ReturnedToNPC_System_Boolean;

	private bool local_SaidMsgNPCVehiclePurchased_System_Boolean;

	private bool local_SaidMsgNPCVehicleSwitched_System_Boolean;

	private ManSFX.MiscSfxType local_SFXChallengeFailed_ManSFX_MiscSfxType = ManSFX.MiscSfxType.StuntRing;

	private bool local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;

	private bool local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private int local_StartValue_System_Int32;

	private bool local_SwitchedTech_System_Boolean;

	private bool local_TechsSetup_System_Boolean;

	private bool local_TechsSpawned_System_Boolean;

	private TankBlock local_TerminalBlock_TankBlock;

	private bool local_TestAttempted_System_Boolean;

	private bool local_TestComplete_System_Boolean;

	private bool local_TestFailed_System_Boolean;

	private bool local_TestStarted_System_Boolean;

	private int local_TotalGroupEnemiesSpawned_System_Int32;

	private bool local_VehiclePurchased_System_Boolean;

	private bool local_VehicleSetup_System_Boolean;

	private Tank local_vehicleTech_Tank;

	private Tank local_vehicleTech2_Tank;

	private Tank local_vehicleTech3_Tank;

	private Tank local_vehicleTech4_Tank;

	private Tank local_vehicleTechRebuy1_Tank;

	private Tank local_vehicleTechRebuy2_Tank;

	private Tank local_vehicleTechRebuy3_Tank;

	private Tank local_vehicleTechRebuy4_Tank;

	private Tank[] local_vehicleTechs_TankArray = new Tank[0];

	private Tank[] local_vehicleTechs2_TankArray = new Tank[0];

	private Tank[] local_vehicleTechs3_TankArray = new Tank[0];

	private Tank[] local_vehicleTechs4_TankArray = new Tank[0];

	private Tank[] local_vehicleTechsRebuy1_TankArray = new Tank[0];

	private Tank[] local_vehicleTechsRebuy2_TankArray = new Tank[0];

	private Tank[] local_vehicleTechsRebuy3_TankArray = new Tank[0];

	private Tank[] local_vehicleTechsRebuy4_TankArray = new Tank[0];

	private bool local_WaitingOnPrompt_System_Boolean;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgLeavingEarlyPostPurchase = new LocalisedString[0];

	public LocalisedString[] msgLeavingEarlyPrePurchase = new LocalisedString[0];

	public LocalisedString[] msgMissionComplete = new LocalisedString[0];

	public uScript_AddMessage.MessageData msgNPCAllPlayersMustBeInAreaForTest;

	public LocalisedString[] msgNPCAttack = new LocalisedString[0];

	public LocalisedString[] msgNPCGreeting = new LocalisedString[0];

	public LocalisedString[] msgNPCIntroJoke = new LocalisedString[0];

	public uScript_AddMessage.MessageData msgNPCNotEnoughMoney;

	public uScript_AddMessage.MessageData msgNPCPurchaseDeclined;

	public LocalisedString[] msgNPCRebriefing = new LocalisedString[0];

	public LocalisedString[] msgNPCRetrying = new LocalisedString[0];

	public uScript_AddMessage.MessageData msgNPCSoldOut;

	public LocalisedString[] msgNPCTestComplete = new LocalisedString[0];

	public LocalisedString[] msgNPCTestFailedPlayerDead = new LocalisedString[0];

	public LocalisedString[] msgNPCTestFailedPlayerDetachedWeapon = new LocalisedString[0];

	public LocalisedString[] msgNPCTestFailedPlayerFled = new LocalisedString[0];

	public LocalisedString[] msgNPCTestFailedPlayerSwitchedTech = new LocalisedString[0];

	public LocalisedString[] msgNPCTestFailedSaveLoaded = new LocalisedString[0];

	public LocalisedString[] msgNPCVehiclePurchased = new LocalisedString[0];

	public LocalisedString[] msgNPCVehicleSwitched = new LocalisedString[0];

	public LocalisedString[] msgNPCWellDone = new LocalisedString[0];

	public uScript_AddMessage.MessageData msgNPCYouCanRebuy;

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
	public string msgTagPurchase = "";

	[Multiline(3)]
	public string msgTagSwitchTech = "";

	public SpawnTechData[] NPCPaymentPoint = new SpawnTechData[0];

	public SpawnTechData[] NPCTechData = new SpawnTechData[0];

	public BlockTypes PrototypeWeaponBlock;

	public float RunTimeLimit;

	public ManSFX.MiscSfxType SFXChallengeComplete = ManSFX.MiscSfxType.StuntComplete;

	public ManSFX.MiscSfxType SFXChallengeStarted = ManSFX.MiscSfxType.StuntRingStart;

	public uScript_AddMessage.MessageSpeaker SpeakerNPC;

	[Multiline(3)]
	public string TriggerCrater = "";

	[Multiline(3)]
	public string TriggerNPC = "";

	public int vehicleCost;

	public SpawnTechData[] vehicleSpawnData = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnData2 = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnData3 = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnData4 = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnDataRebuy1 = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnDataRebuy2 = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnDataRebuy3 = new SpawnTechData[0];

	public SpawnTechData[] vehicleSpawnDataRebuy4 = new SpawnTechData[0];

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_34;

	private GameObject owner_Connection_37;

	private GameObject owner_Connection_45;

	private GameObject owner_Connection_72;

	private GameObject owner_Connection_77;

	private GameObject owner_Connection_93;

	private GameObject owner_Connection_95;

	private GameObject owner_Connection_96;

	private GameObject owner_Connection_99;

	private GameObject owner_Connection_103;

	private GameObject owner_Connection_109;

	private GameObject owner_Connection_127;

	private GameObject owner_Connection_134;

	private GameObject owner_Connection_135;

	private GameObject owner_Connection_151;

	private GameObject owner_Connection_169;

	private GameObject owner_Connection_172;

	private GameObject owner_Connection_182;

	private GameObject owner_Connection_183;

	private GameObject owner_Connection_198;

	private GameObject owner_Connection_206;

	private GameObject owner_Connection_220;

	private GameObject owner_Connection_224;

	private GameObject owner_Connection_255;

	private GameObject owner_Connection_256;

	private GameObject owner_Connection_258;

	private GameObject owner_Connection_262;

	private GameObject owner_Connection_265;

	private GameObject owner_Connection_270;

	private GameObject owner_Connection_273;

	private GameObject owner_Connection_335;

	private GameObject owner_Connection_342;

	private GameObject owner_Connection_344;

	private GameObject owner_Connection_350;

	private GameObject owner_Connection_358;

	private GameObject owner_Connection_361;

	private GameObject owner_Connection_366;

	private GameObject owner_Connection_369;

	private GameObject owner_Connection_373;

	private GameObject owner_Connection_385;

	private GameObject owner_Connection_392;

	private GameObject owner_Connection_401;

	private GameObject owner_Connection_402;

	private GameObject owner_Connection_419;

	private GameObject owner_Connection_424;

	private GameObject owner_Connection_452;

	private GameObject owner_Connection_470;

	private GameObject owner_Connection_472;

	private GameObject owner_Connection_538;

	private GameObject owner_Connection_556;

	private GameObject owner_Connection_562;

	private GameObject owner_Connection_599;

	private GameObject owner_Connection_606;

	private GameObject owner_Connection_634;

	private GameObject owner_Connection_703;

	private GameObject owner_Connection_713;

	private GameObject owner_Connection_720;

	private GameObject owner_Connection_751;

	private GameObject owner_Connection_753;

	private GameObject owner_Connection_796;

	private GameObject owner_Connection_804;

	private GameObject owner_Connection_809;

	private GameObject owner_Connection_814;

	private GameObject owner_Connection_817;

	private GameObject owner_Connection_820;

	private GameObject owner_Connection_823;

	private GameObject owner_Connection_826;

	private GameObject owner_Connection_829;

	private GameObject owner_Connection_832;

	private GameObject owner_Connection_843;

	private GameObject owner_Connection_847;

	private GameObject owner_Connection_851;

	private GameObject owner_Connection_858;

	private GameObject owner_Connection_859;

	private GameObject owner_Connection_860;

	private GameObject owner_Connection_861;

	private GameObject owner_Connection_868;

	private GameObject owner_Connection_871;

	private GameObject owner_Connection_879;

	private GameObject owner_Connection_890;

	private GameObject owner_Connection_966;

	private GameObject owner_Connection_969;

	private GameObject owner_Connection_973;

	private GameObject owner_Connection_982;

	private GameObject owner_Connection_997;

	private GameObject owner_Connection_1012;

	private GameObject owner_Connection_1018;

	private GameObject owner_Connection_1020;

	private GameObject owner_Connection_1065;

	private GameObject owner_Connection_1080;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1;

	private bool logic_uScriptAct_SetBool_Out_1 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_2;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_2 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_2 = "ObjectiveComplete";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_7;

	private bool logic_uScriptCon_CompareBool_True_7 = true;

	private bool logic_uScriptCon_CompareBool_False_7 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_11 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_11;

	private bool logic_uScript_FinishEncounter_Out_11 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_12;

	private int logic_SubGraph_SaveLoadInt_integer_12;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_12 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_12 = "EnemyGroup";

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

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_18;

	private bool logic_uScriptCon_CompareBool_True_18 = true;

	private bool logic_uScriptCon_CompareBool_False_18 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_20 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_20 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_20;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_20 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_20;

	private bool logic_uScript_SpawnTechsFromData_Out_20 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_25 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_25 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_25;

	private string logic_uScript_AddOnScreenMessage_tag_25 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_25;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_25;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_25;

	private bool logic_uScript_AddOnScreenMessage_Out_25 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_25 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_27 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_27;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_27;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_28 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_28;

	private bool logic_uScriptAct_SetBool_Out_28 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_28 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_28 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_31 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_32 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_32 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_32 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_32;

	private float logic_uScript_DamageTechs_leaveBlksPercent_32;

	private bool logic_uScript_DamageTechs_makeVulnerable_32;

	private bool logic_uScript_DamageTechs_Out_32 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_36 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_36;

	private bool logic_uScript_ClearEncounterTarget_Out_36 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_40;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_40 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_40 = "TechsSpawned";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_41 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_41;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_41 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_41;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_41 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_41 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_41 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_41 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_43 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_43 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_43;

	private bool logic_uScript_SetTankInvulnerable_Out_43 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_44 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_44 = new Tank[0];

	private int logic_uScript_AccessListTech_index_44;

	private Tank logic_uScript_AccessListTech_value_44;

	private bool logic_uScript_AccessListTech_Out_44 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_48;

	private bool logic_uScriptCon_CompareBool_True_48 = true;

	private bool logic_uScriptCon_CompareBool_False_48 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_50 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_50;

	private bool logic_uScriptAct_SetBool_Out_50 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_50 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_50 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_53;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_53 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_53 = "TechsSetup";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_55;

	private bool logic_uScriptCon_CompareBool_True_55 = true;

	private bool logic_uScriptCon_CompareBool_False_55 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_57;

	private bool logic_uScriptCon_CompareBool_True_57 = true;

	private bool logic_uScriptCon_CompareBool_False_57 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_59;

	private bool logic_uScriptCon_CompareBool_True_59 = true;

	private bool logic_uScriptCon_CompareBool_False_59 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_61 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_61;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_61;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_63 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_63;

	private bool logic_uScriptAct_SetBool_Out_63 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_63 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_63 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_65;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_65 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_65 = "TestComplete";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_68;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_68 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_68 = "NPCMet";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_69;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_69 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_69 = "TestStarted";

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_71 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_71 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_71 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_71;

	private float logic_uScript_DamageTechs_leaveBlksPercent_71;

	private bool logic_uScript_DamageTechs_makeVulnerable_71;

	private bool logic_uScript_DamageTechs_Out_71 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_74 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_74 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_76 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_76 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_76 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_76;

	private float logic_uScript_DamageTechs_leaveBlksPercent_76;

	private bool logic_uScript_DamageTechs_makeVulnerable_76;

	private bool logic_uScript_DamageTechs_Out_76 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_79 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_79 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_81;

	private bool logic_uScriptCon_CompareBool_True_81 = true;

	private bool logic_uScriptCon_CompareBool_False_81 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_84;

	private bool logic_uScriptCon_CompareBool_True_84 = true;

	private bool logic_uScriptCon_CompareBool_False_84 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_87;

	private bool logic_uScriptCon_CompareBool_True_87 = true;

	private bool logic_uScriptCon_CompareBool_False_87 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_89;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_89 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_89 = "EnemyAlive1";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_90;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_90 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_90 = "EnemyAlive2";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_91;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_91 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_91 = "EnemyAlive3";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_92 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_92 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_92;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_92;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_92 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_92 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_94 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_94 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_94;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_94;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_94 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_94 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_97 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_97 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_97;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_97;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_97 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_97 = true;

	private uScript_GetMissionTimerDisplayTime logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_98 = new uScript_GetMissionTimerDisplayTime();

	private GameObject logic_uScript_GetMissionTimerDisplayTime_owner_98;

	private float logic_uScript_GetMissionTimerDisplayTime_Return_98;

	private bool logic_uScript_GetMissionTimerDisplayTime_Out_98 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_100 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_100;

	private float logic_uScriptCon_CompareFloat_B_100;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_100 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_100 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_100 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_100 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_100 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_100 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_101 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_101;

	private bool logic_uScript_StopMissionTimer_Out_101 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_104 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_104;

	private bool logic_uScript_HideMissionTimerUI_Out_104 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_106 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_106;

	private bool logic_uScriptAct_SetBool_Out_106 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_106 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_106 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_107 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_107;

	private bool logic_uScript_StopMissionTimer_Out_107 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_108 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_108;

	private bool logic_uScript_HideMissionTimerUI_Out_108 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_110 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_110;

	private bool logic_uScriptAct_SetBool_Out_110 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_110 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_110 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_111 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_111;

	private bool logic_uScriptAct_SetBool_Out_111 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_111 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_111 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_112 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_112;

	private bool logic_uScriptAct_SetBool_Out_112 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_112 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_112 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_114 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_114;

	private bool logic_uScript_PlayMiscSFX_Out_114 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_115 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_115;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_115 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_115;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_115 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_115 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_115 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_115 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_116 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_116;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_116 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_116;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_116 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_116 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_116 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_116 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_117 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_117 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_117;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_117 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_117;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_117 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_117 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_117 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_117 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_124;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_124 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_124 = "EnemyAlive4";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_125;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_125 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_125 = "EnemyAlive5";

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_126 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_126;

	private object logic_uScript_SetEncounterTarget_visibleObject_126 = "";

	private bool logic_uScript_SetEncounterTarget_Out_126 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_129 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_129 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_129 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_129;

	private string logic_uScript_AddOnScreenMessage_tag_129 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_129;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_129;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_129;

	private bool logic_uScript_AddOnScreenMessage_Out_129 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_129 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_132 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_132;

	private bool logic_uScriptAct_SetBool_Out_132 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_132 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_132 = true;

	private uScript_ResetMissionTimerTimeElapsed logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_133 = new uScript_ResetMissionTimerTimeElapsed();

	private GameObject logic_uScript_ResetMissionTimerTimeElapsed_owner_133;

	private float logic_uScript_ResetMissionTimerTimeElapsed_startTime_133;

	private bool logic_uScript_ResetMissionTimerTimeElapsed_Out_133 = true;

	private uScript_ResetMissionTimerTimeElapsed logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_136 = new uScript_ResetMissionTimerTimeElapsed();

	private GameObject logic_uScript_ResetMissionTimerTimeElapsed_owner_136;

	private float logic_uScript_ResetMissionTimerTimeElapsed_startTime_136;

	private bool logic_uScript_ResetMissionTimerTimeElapsed_Out_136 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_137 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_137;

	private bool logic_uScriptAct_SetBool_Out_137 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_137 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_137 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_139 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_139;

	private bool logic_uScriptCon_CompareBool_True_139 = true;

	private bool logic_uScriptCon_CompareBool_False_139 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_141 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_141;

	private bool logic_uScriptAct_SetBool_Out_141 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_141 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_141 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_144 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_144 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_144 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_144;

	private string logic_uScript_AddOnScreenMessage_tag_144 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_144;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_144;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_144;

	private bool logic_uScript_AddOnScreenMessage_Out_144 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_144 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_147;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_147 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_147 = "RetryingTest";

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_148 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_148;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_148 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_148 = 100f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_148;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_148;

	private bool logic_uScript_FlyTechUpAndAway_Out_148 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_149 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_149 = new Tank[0];

	private int logic_uScript_AccessListTech_index_149;

	private Tank logic_uScript_AccessListTech_value_149;

	private bool logic_uScript_AccessListTech_Out_149 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_152 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_152;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_152 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_152;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_152 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_152 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_152 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_152 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_156 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_156 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_156 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_156 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_156 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_156 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_156 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_158;

	private bool logic_uScriptCon_CompareBool_True_158 = true;

	private bool logic_uScriptCon_CompareBool_False_158 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_160 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_160;

	private bool logic_uScriptAct_SetBool_Out_160 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_160 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_160 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_163 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_163 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_163;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_163;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_163 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_163 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_164 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_164;

	private bool logic_uScriptAct_SetBool_Out_164 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_164 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_164 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_165;

	private bool logic_uScriptCon_CompareBool_True_165 = true;

	private bool logic_uScriptCon_CompareBool_False_165 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_166 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_166 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_166;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_166;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_166 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_166 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_168;

	private bool logic_uScriptCon_CompareBool_True_168 = true;

	private bool logic_uScriptCon_CompareBool_False_168 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_174 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_174 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_174 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_174;

	private float logic_uScript_DamageTechs_leaveBlksPercent_174;

	private bool logic_uScript_DamageTechs_makeVulnerable_174;

	private bool logic_uScript_DamageTechs_Out_174 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_175 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_175 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_175 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_175;

	private float logic_uScript_DamageTechs_leaveBlksPercent_175;

	private bool logic_uScript_DamageTechs_makeVulnerable_175;

	private bool logic_uScript_DamageTechs_Out_175 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_176 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_176 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_180 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_180 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_180;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_180 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_180;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_180 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_180 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_180 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_180 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_184 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_184;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_184 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_184;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_184 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_184 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_184 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_184 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_185 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_185 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_187 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_187;

	private bool logic_uScriptCon_CompareBool_True_187 = true;

	private bool logic_uScriptCon_CompareBool_False_187 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_188 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_188 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_188 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_188 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_188 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_188 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_188 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_190 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_190;

	private bool logic_uScriptAct_SetBool_Out_190 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_190 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_190 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_192 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_192 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_192 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_192;

	private string logic_uScript_AddOnScreenMessage_tag_192 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_192;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_192;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_192;

	private bool logic_uScript_AddOnScreenMessage_Out_192 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_192 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_196 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_196;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_196 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_199 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_199;

	private object logic_uScript_SetEncounterTarget_visibleObject_199 = "";

	private bool logic_uScript_SetEncounterTarget_Out_199 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_200 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_200 = new Tank[0];

	private int logic_uScript_AccessListTech_index_200;

	private Tank logic_uScript_AccessListTech_value_200;

	private bool logic_uScript_AccessListTech_Out_200 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_202 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_202 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_202;

	private bool logic_uScript_SetTankInvulnerable_Out_202 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_204 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_204 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_204;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_204 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_204;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_204 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_204 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_204 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_204 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_209;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_209 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_209 = "ReturnedToNPC";

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_211 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_211 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_211 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_211;

	private string logic_uScript_AddOnScreenMessage_tag_211 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_211;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_211;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_211;

	private bool logic_uScript_AddOnScreenMessage_Out_211 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_211 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_216 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_216 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_216;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_216 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_216;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_216 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_216 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_216 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_216 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_219 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_219 = new Tank[0];

	private int logic_uScript_AccessListTech_index_219;

	private Tank logic_uScript_AccessListTech_value_219;

	private bool logic_uScript_AccessListTech_Out_219 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_221 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_221;

	private object logic_uScript_SetEncounterTarget_visibleObject_221 = "";

	private bool logic_uScript_SetEncounterTarget_Out_221 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_222 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_222 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_222;

	private bool logic_uScript_SetTankInvulnerable_Out_222 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_226 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_226 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_226 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_228 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_228 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_228 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_232 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_232 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_232 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_232;

	private string logic_uScript_AddOnScreenMessage_tag_232 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_232;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_232;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_232;

	private bool logic_uScript_AddOnScreenMessage_Out_232 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_232 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_234 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_234;

	private bool logic_uScriptAct_SetBool_Out_234 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_234 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_234 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_235 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_235;

	private bool logic_uScriptAct_SetBool_Out_235 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_235 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_235 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_238;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_238 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_238 = "Rebriefed";

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_243 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_243 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_243 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_243;

	private string logic_uScript_AddOnScreenMessage_tag_243 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_243;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_243;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_243;

	private bool logic_uScript_AddOnScreenMessage_Out_243 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_243 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_245 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_245;

	private bool logic_uScriptAct_SetBool_Out_245 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_245 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_245 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_247 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_247;

	private bool logic_uScriptCon_CompareBool_True_247 = true;

	private bool logic_uScriptCon_CompareBool_False_247 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_249 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_249;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_249 = 1;

	private bool logic_uScript_SetCustomRadarTeamID_Out_249 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_251;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_251 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_251 = "FinishedJoke";

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_253 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_253 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_253;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_253 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_254 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_254;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_254 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_254 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_254 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_257 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_257 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_257;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_257 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_257 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_257 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_259 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_259 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_259;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_259 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_259;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_259 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_259 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_259 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_259 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_260 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_260 = new Tank[0];

	private int logic_uScript_AccessListTech_index_260;

	private Tank logic_uScript_AccessListTech_value_260;

	private bool logic_uScript_AccessListTech_Out_260 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_261 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_261 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_263 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_263;

	private bool logic_uScript_PlayMiscSFX_Out_263 = true;

	private uScript_StartMissionTimer logic_uScript_StartMissionTimer_uScript_StartMissionTimer_266 = new uScript_StartMissionTimer();

	private GameObject logic_uScript_StartMissionTimer_owner_266;

	private float logic_uScript_StartMissionTimer_startTime_266;

	private bool logic_uScript_StartMissionTimer_Out_266 = true;

	private uScript_ShowMissionTimerUI logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_267 = new uScript_ShowMissionTimerUI();

	private GameObject logic_uScript_ShowMissionTimerUI_owner_267;

	private bool logic_uScript_ShowMissionTimerUI_showBestTime_267;

	private bool logic_uScript_ShowMissionTimerUI_Out_267 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_269 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_269;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_269 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_271 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_271 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_272 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_272;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_272 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_272 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_272 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_274 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_274 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_274;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_274 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_276 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_276 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_276 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_276;

	private string logic_uScript_AddOnScreenMessage_tag_276 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_276;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_276;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_276;

	private bool logic_uScript_AddOnScreenMessage_Out_276 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_276 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_279 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_279;

	private bool logic_uScript_PlayMiscSFX_Out_279 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_280 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_280 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_280;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_280 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_283 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_283;

	private bool logic_uScriptCon_CompareBool_True_283 = true;

	private bool logic_uScriptCon_CompareBool_False_283 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_285 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_285;

	private bool logic_uScriptCon_CompareBool_True_285 = true;

	private bool logic_uScriptCon_CompareBool_False_285 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_288;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_288 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_288 = "TestFailed";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_289 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_289;

	private bool logic_uScriptAct_SetBool_Out_289 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_289 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_289 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_290 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_290;

	private bool logic_uScriptAct_SetBool_Out_290 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_290 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_290 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_295 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_295;

	private bool logic_uScriptCon_CompareBool_True_295 = true;

	private bool logic_uScriptCon_CompareBool_False_295 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_296 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_296;

	private bool logic_uScriptCon_CompareBool_True_296 = true;

	private bool logic_uScriptCon_CompareBool_False_296 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_297 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_297 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_298 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_298;

	private bool logic_uScriptAct_SetBool_Out_298 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_298 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_298 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_301;

	private bool logic_uScriptCon_CompareBool_True_301 = true;

	private bool logic_uScriptCon_CompareBool_False_301 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_303 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_303 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_303 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_303;

	private string logic_uScript_AddOnScreenMessage_tag_303 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_303;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_303;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_303;

	private bool logic_uScript_AddOnScreenMessage_Out_303 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_303 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_306 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_306;

	private bool logic_uScriptAct_SetBool_Out_306 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_306 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_306 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_308 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_308;

	private bool logic_uScriptAct_SetBool_Out_308 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_308 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_308 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_309 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_309;

	private bool logic_uScriptAct_SetBool_Out_309 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_309 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_309 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_314;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_314 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_314 = "OutOfBounds";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_315;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_315 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_315 = "PlayerSwitchedTech";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_316;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_316 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_316 = "PlayerSpawnedTech";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_317 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_317 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_318 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_318 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_319 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_319;

	private bool logic_uScriptAct_SetBool_Out_319 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_319 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_319 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_321 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_321;

	private bool logic_uScriptAct_SetBool_Out_321 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_321 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_321 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_323 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_323;

	private bool logic_uScriptCon_CompareBool_True_323 = true;

	private bool logic_uScriptCon_CompareBool_False_323 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_325 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_325;

	private bool logic_uScriptAct_SetBool_Out_325 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_325 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_325 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_328;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_328 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_328 = "TestAttempted";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_329 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_329;

	private bool logic_uScriptCon_CompareBool_True_329 = true;

	private bool logic_uScriptCon_CompareBool_False_329 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_331 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_331;

	private bool logic_uScriptCon_CompareBool_True_331 = true;

	private bool logic_uScriptCon_CompareBool_False_331 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_333 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_333 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_334 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_334 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_334;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_334 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_334;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_334 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_334 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_334 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_334 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_336 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_336;

	private bool logic_uScriptAct_SetBool_Out_336 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_336 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_336 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_340 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_340 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_340;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_340 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_340;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_340 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_340 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_340 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_340 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_343 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_343;

	private bool logic_uScriptAct_SetBool_Out_343 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_343 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_343 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_346 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_346;

	private bool logic_uScriptAct_SetBool_Out_346 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_346 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_346 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_347 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_347 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_347;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_347 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_347;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_347 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_347 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_347 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_347 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_352 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_352;

	private bool logic_uScriptAct_SetBool_Out_352 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_352 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_352 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_353 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_353 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_353;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_353 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_353;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_353 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_353 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_353 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_353 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_356 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_356;

	private bool logic_uScriptAct_SetBool_Out_356 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_356 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_356 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_357 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_357;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_357 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_357;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_357 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_357 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_357 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_357 = true;

	private uScript_GetMissionTimerDisplayTime logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_359 = new uScript_GetMissionTimerDisplayTime();

	private GameObject logic_uScript_GetMissionTimerDisplayTime_owner_359;

	private float logic_uScript_GetMissionTimerDisplayTime_Return_359;

	private bool logic_uScript_GetMissionTimerDisplayTime_Out_359 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_362 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_362;

	private float logic_uScriptCon_CompareFloat_B_362 = 60f;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_362 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_362 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_362 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_362 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_362 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_362 = true;

	private uScript_GetMissionTimerDisplayTime logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_363 = new uScript_GetMissionTimerDisplayTime();

	private GameObject logic_uScript_GetMissionTimerDisplayTime_owner_363;

	private float logic_uScript_GetMissionTimerDisplayTime_Return_363;

	private bool logic_uScript_GetMissionTimerDisplayTime_Out_363 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_364 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_364;

	private float logic_uScriptCon_CompareFloat_B_364 = 80f;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_364 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_364 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_364 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_364 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_364 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_364 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_368 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_368;

	private float logic_uScriptCon_CompareFloat_B_368 = 100f;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_368 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_368 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_368 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_368 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_368 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_368 = true;

	private uScript_GetMissionTimerDisplayTime logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_370 = new uScript_GetMissionTimerDisplayTime();

	private GameObject logic_uScript_GetMissionTimerDisplayTime_owner_370;

	private float logic_uScript_GetMissionTimerDisplayTime_Return_370;

	private bool logic_uScript_GetMissionTimerDisplayTime_Out_370 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_372 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_372;

	private float logic_uScriptCon_CompareFloat_B_372 = 119f;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_372 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_372 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_372 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_372 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_372 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_372 = true;

	private uScript_GetMissionTimerDisplayTime logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_374 = new uScript_GetMissionTimerDisplayTime();

	private GameObject logic_uScript_GetMissionTimerDisplayTime_owner_374;

	private float logic_uScript_GetMissionTimerDisplayTime_Return_374;

	private bool logic_uScript_GetMissionTimerDisplayTime_Out_374 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_375 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_375 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_375;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_375 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_375;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_375 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_375 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_375 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_375 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_376;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_377 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_377;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_377;

	private bool logic_uScript_LockTechSendToSCU_Out_377 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_380 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_380;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_380;

	private bool logic_uScript_LockTechSendToSCU_Out_380 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_384 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_384 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_387 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_387;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_387 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_387 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_388 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_388 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_388 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_389 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_389 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_389 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_389;

	private string logic_uScript_AddOnScreenMessage_tag_389 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_389;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_389;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_389;

	private bool logic_uScript_AddOnScreenMessage_Out_389 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_389 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_390 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_390;

	private bool logic_uScriptCon_CompareBool_True_390 = true;

	private bool logic_uScriptCon_CompareBool_False_390 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_394 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_394 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_394 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_394 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_397 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_397;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_397 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_398 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_398 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_398;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_398 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_399 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_399 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_400 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_400 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_400;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_400 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_400 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_400 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_403 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_403 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_404 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_404 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_404;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_404 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_404;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_404 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_404 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_404 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_404 = true;

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_407 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_407;

	private bool logic_uScript_IsTechPlayer_Out_407 = true;

	private bool logic_uScript_IsTechPlayer_True_407 = true;

	private bool logic_uScript_IsTechPlayer_False_407 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_411 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_411 = true;

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_413 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_413;

	private bool logic_uScript_IsTechPlayer_Out_413 = true;

	private bool logic_uScript_IsTechPlayer_True_413 = true;

	private bool logic_uScript_IsTechPlayer_False_413 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_417 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_417;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_417;

	private bool logic_uScript_LockTechSendToSCU_Out_417 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_418 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_418;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_418;

	private bool logic_uScript_LockTech_Out_418 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_420 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_420 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_420;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_420 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_420;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_420 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_420 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_420 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_420 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_423 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_423 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_423;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_423 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_423 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_423 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_427 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_427;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_427;

	private bool logic_uScript_LockTech_Out_427 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_428 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_428;

	private bool logic_uScriptCon_CompareBool_True_428 = true;

	private bool logic_uScriptCon_CompareBool_False_428 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_429 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_429 = new Tank[0];

	private int logic_uScript_AccessListTech_index_429;

	private Tank logic_uScript_AccessListTech_value_429;

	private bool logic_uScript_AccessListTech_Out_429 = true;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_430 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_430;

	private bool logic_uScript_GetMaxPlayers_Out_430 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_432 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_432;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_432 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_432 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_433 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_433;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_433 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_433 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_436 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_436 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_436 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_436 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_436 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_436 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_436 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_442 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_442 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_443 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_443;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_443 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_443 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_447 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_447;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_447 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_447 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_448 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_448 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_450 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_450;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_450 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_450 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_451 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_451 = "";

	private bool logic_uScript_EnableGlow_enable_451;

	private bool logic_uScript_EnableGlow_Out_451 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_453 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_453;

	private bool logic_uScriptCon_CompareBool_True_453 = true;

	private bool logic_uScriptCon_CompareBool_False_453 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_454 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_454;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_454 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_456 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_456 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_456 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_456 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_456 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_456 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_456 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_458 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_458 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_459 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_459;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_459 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_459 = true;

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_460 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_460;

	private bool logic_uScript_IsTechPlayer_Out_460 = true;

	private bool logic_uScript_IsTechPlayer_True_460 = true;

	private bool logic_uScript_IsTechPlayer_False_460 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_465 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_465 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_465 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_465;

	private string logic_uScript_AddOnScreenMessage_tag_465 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_465;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_465;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_465;

	private bool logic_uScript_AddOnScreenMessage_Out_465 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_465 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_466 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_466 = new Tank[0];

	private int logic_uScript_AccessListTech_index_466;

	private Tank logic_uScript_AccessListTech_value_466;

	private bool logic_uScript_AccessListTech_Out_466 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_467;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_468 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_468 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_468;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_468 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_468;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_468 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_468 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_468 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_468 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_475 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_475;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_475 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_475 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_481 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_481;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_481;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_481;

	private bool logic_uScript_AddMessage_Out_481 = true;

	private bool logic_uScript_AddMessage_Shown_481 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_483 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_483;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_483 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_483 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_485;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_489 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_489;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_489;

	private bool logic_uScript_LockTech_Out_489 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_492 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_492 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_492 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_493 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_493;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_493 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_493 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_494 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_494;

	private object logic_uScript_SetEncounterTarget_visibleObject_494 = "";

	private bool logic_uScript_SetEncounterTarget_Out_494 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_495 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_495;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_495;

	private bool logic_uScript_LockTech_Out_495 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_496 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_496 = "";

	private bool logic_uScript_EnableGlow_enable_496 = true;

	private bool logic_uScript_EnableGlow_Out_496 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_497 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_497;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_497 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_497 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_498 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_498;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_498;

	private bool logic_uScript_LockTech_Out_498 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_499 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_499;

	private bool logic_uScriptAct_SetBool_Out_499 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_499 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_499 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_502 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_502 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_502 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_502;

	private string logic_uScript_AddOnScreenMessage_tag_502 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_502;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_502;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_502;

	private bool logic_uScript_AddOnScreenMessage_Out_502 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_502 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_503 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_503 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_503 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_503 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_503 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_504 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_504;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_504 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_504 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_507 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_507 = new Tank[0];

	private int logic_uScript_AccessListTech_index_507;

	private Tank logic_uScript_AccessListTech_value_507;

	private bool logic_uScript_AccessListTech_Out_507 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_508 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_508 = new Tank[0];

	private int logic_uScript_AccessListTech_index_508;

	private Tank logic_uScript_AccessListTech_value_508;

	private bool logic_uScript_AccessListTech_Out_508 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_511 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_511 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_511;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_511 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_511;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_511 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_511 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_511 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_511 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_512 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_512 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_513 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_513 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_514;

	private bool logic_uScriptCon_CompareBool_True_514 = true;

	private bool logic_uScriptCon_CompareBool_False_514 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_515 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_515 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_518 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_518 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_520 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_520 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_520;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_520 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_520;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_520 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_520 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_520 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_520 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_521 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_521 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_521 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_522 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_522 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_523 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_523;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_523;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_523;

	private bool logic_uScript_AddMessage_Out_523 = true;

	private bool logic_uScript_AddMessage_Shown_523 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_526 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_526;

	private bool logic_uScriptCon_CompareBool_True_526 = true;

	private bool logic_uScriptCon_CompareBool_False_526 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_527 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_527;

	private TankBlock logic_uScript_CompareBlock_B_527;

	private bool logic_uScript_CompareBlock_EqualTo_527 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_527 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_528 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_528;

	private bool logic_uScriptCon_CompareBool_True_528 = true;

	private bool logic_uScriptCon_CompareBool_False_528 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_529 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_529;

	private bool logic_uScriptCon_CompareBool_True_529 = true;

	private bool logic_uScriptCon_CompareBool_False_529 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_531 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_531;

	private bool logic_uScript_ClearEncounterTarget_Out_531 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_532 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_532 = new Tank[0];

	private int logic_uScript_AccessListTech_index_532;

	private Tank logic_uScript_AccessListTech_value_532;

	private bool logic_uScript_AccessListTech_Out_532 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_533 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_533 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_535 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_535;

	private bool logic_uScriptAct_SetBool_Out_535 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_535 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_535 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_537 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_537;

	private bool logic_uScriptCon_CompareBool_True_537 = true;

	private bool logic_uScriptCon_CompareBool_False_537 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_539;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_539;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_539;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_539;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_539;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_541 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_541;

	private bool logic_uScriptAct_SetBool_Out_541 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_541 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_541 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_542 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_542;

	private bool logic_uScriptAct_SetBool_Out_542 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_542 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_542 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_545 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_545 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_545 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_546 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_546;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_546;

	private bool logic_uScript_LockTech_Out_546 = true;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_547 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_547;

	private bool logic_uScript_GetMaxPlayers_Out_547 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_548 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_548;

	private bool logic_uScriptCon_CompareBool_True_548 = true;

	private bool logic_uScriptCon_CompareBool_False_548 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_549 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_549;

	private bool logic_uScriptAct_SetBool_Out_549 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_549 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_549 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_550 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_550;

	private bool logic_uScriptAct_SetBool_Out_550 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_550 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_550 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_551 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_551;

	private bool logic_uScriptAct_SetBool_Out_551 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_551 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_551 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_552 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_552;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_552 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_552 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_553 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_553 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_554 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_554;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_554;

	private bool logic_uScript_LockTech_Out_554 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_555 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_555 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_555;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_555 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_555;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_555 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_555 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_555 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_555 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_557 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_557;

	private BlockTypes logic_uScript_GetTankBlock_blockType_557;

	private TankBlock logic_uScript_GetTankBlock_Return_557;

	private bool logic_uScript_GetTankBlock_Out_557 = true;

	private bool logic_uScript_GetTankBlock_Returned_557 = true;

	private bool logic_uScript_GetTankBlock_NotFound_557 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_559 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_559;

	private object logic_uScript_SetEncounterTarget_visibleObject_559 = "";

	private bool logic_uScript_SetEncounterTarget_Out_559 = true;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_560 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_560;

	private bool logic_uScript_GetMaxPlayers_Out_560 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_561 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_561;

	private bool logic_uScriptCon_CompareBool_True_561 = true;

	private bool logic_uScriptCon_CompareBool_False_561 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_563 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_563;

	private bool logic_uScriptCon_CompareBool_True_563 = true;

	private bool logic_uScriptCon_CompareBool_False_563 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_565 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_565 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_566 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_566;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_566;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_566;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_566;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_566 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_566;

	private bool logic_uScript_MissionPromptBlock_Show_Out_566 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_567 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_567 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_568 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_568 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_568;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_568 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_568 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_568 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_569 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_569 = new Tank[0];

	private int logic_uScript_AccessListTech_index_569;

	private Tank logic_uScript_AccessListTech_value_569;

	private bool logic_uScript_AccessListTech_Out_569 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_571 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_571;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_571;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_572 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_572;

	private bool logic_uScript_GetMaxPlayers_Out_572 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_573 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_573;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_573;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_573;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_573;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_573 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_573;

	private bool logic_uScript_MissionPromptBlock_Show_Out_573 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_574;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_575 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_575 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_575 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_578 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_578 = new Tank[0];

	private int logic_uScript_AccessListTech_index_578;

	private Tank logic_uScript_AccessListTech_value_578;

	private bool logic_uScript_AccessListTech_Out_578 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_579 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_579 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_579;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_579 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_579 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_579 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_580 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_580;

	private bool logic_uScriptCon_CompareBool_True_580 = true;

	private bool logic_uScriptCon_CompareBool_False_580 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_583 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_583 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_584 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_584;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_584;

	private bool logic_uScript_LockTech_Out_584 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_586 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_586 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_586 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_586 = true;

	private uScript_DiscoverBlocks logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_588 = new uScript_DiscoverBlocks();

	private BlockTypes[] logic_uScript_DiscoverBlocks_blockTypes_588 = new BlockTypes[0];

	private bool logic_uScript_DiscoverBlocks_Out_588 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_592 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_592 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_592 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_592 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_592 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_592 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_592 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_596 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_596 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_596 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_596 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_596 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_604;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_608 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_608 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_614 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_614;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_614 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_615 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_615;

	private int logic_uScriptCon_CompareInt_B_615;

	private bool logic_uScriptCon_CompareInt_GreaterThan_615 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_615 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_615 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_615 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_615 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_615 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_616 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_616;

	private bool logic_uScriptCon_CompareBool_True_616 = true;

	private bool logic_uScriptCon_CompareBool_False_616 = true;

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_617 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_617;

	private bool logic_uScript_IsTechPlayer_Out_617 = true;

	private bool logic_uScript_IsTechPlayer_True_617 = true;

	private bool logic_uScript_IsTechPlayer_False_617 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_620 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_620;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_620 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_620 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_622;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_622;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_622;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_622;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_622;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_625 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_625;

	private bool logic_uScript_GetMaxPlayers_Out_625 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_628 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_628;

	private bool logic_uScriptCon_CompareBool_True_628 = true;

	private bool logic_uScriptCon_CompareBool_False_628 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_629 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_629;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_629;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_632 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_632;

	private bool logic_uScriptCon_CompareBool_True_632 = true;

	private bool logic_uScriptCon_CompareBool_False_632 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_637 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_637;

	private bool logic_uScriptAct_SetBool_Out_637 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_637 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_637 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_639 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_639;

	private bool logic_uScriptCon_CompareBool_True_639 = true;

	private bool logic_uScriptCon_CompareBool_False_639 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_645 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_645 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_646 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_646;

	private bool logic_uScriptCon_CompareBool_True_646 = true;

	private bool logic_uScriptCon_CompareBool_False_646 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_649 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_649 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_650 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_650 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_661;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_661 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_661 = "SwitchedTech";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_662;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_662 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_662 = "VehiclePurchased";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_663;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_663 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_663 = "VehicleSetup";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_664;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_664 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_664 = "msg03aShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_665;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_665 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_665 = "msg03bShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_666;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_666 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_666 = "HasEnoughMoney";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_667;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_667 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_667 = "WaitingOnPrompt";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_668;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_668 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_668 = "SaidMsgNPCVehiclePurchased";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_669;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_669 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_669 = "ShownMsgLeavingEarlyPostPurchase";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_670;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_670 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_670 = "ShownMsgLeavingEarlyPrePurchase";

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_680 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_680;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_680;

	private bool logic_uScript_LockTechSendToSCU_Out_680 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_683 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_683 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_683 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_683;

	private string logic_uScript_AddOnScreenMessage_tag_683 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_683;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_683;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_683;

	private bool logic_uScript_AddOnScreenMessage_Out_683 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_683 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_686 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_686;

	private bool logic_uScriptCon_CompareBool_True_686 = true;

	private bool logic_uScriptCon_CompareBool_False_686 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_688 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_688;

	private bool logic_uScriptAct_SetBool_Out_688 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_688 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_688 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_689 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_689 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_689;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_689 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_692;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_692 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_692 = "SaidMsgNPCVehicleSwitched";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_694;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_694 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_694 = "NewWeaponAttached";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_695 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_695 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_696 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_696 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_697 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_697 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_698 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_698 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_701 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_701 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_701;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_701 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_701;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_701 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_701 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_701 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_701 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_702 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_702 = new Tank[0];

	private int logic_uScript_AccessListTech_index_702;

	private Tank logic_uScript_AccessListTech_value_702;

	private bool logic_uScript_AccessListTech_Out_702 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_706 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_706;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_706 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_706 = 100f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_706;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_706;

	private bool logic_uScript_FlyTechUpAndAway_Out_706 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_707 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_707 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_709 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_709 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_709;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_709 = true;

	private uScript_GetMissionTimerDisplayTime logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_712 = new uScript_GetMissionTimerDisplayTime();

	private GameObject logic_uScript_GetMissionTimerDisplayTime_owner_712;

	private float logic_uScript_GetMissionTimerDisplayTime_Return_712;

	private bool logic_uScript_GetMissionTimerDisplayTime_Out_712 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_714 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_714;

	private float logic_uScriptCon_CompareFloat_B_714 = 10f;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_714 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_714 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_714 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_714 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_714 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_714 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_716 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_716;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_716 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_716 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_719 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_719 = new Tank[0];

	private int logic_uScript_AccessListTech_index_719;

	private Tank logic_uScript_AccessListTech_value_719;

	private bool logic_uScript_AccessListTech_Out_719 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_721 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_721 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_721;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_721 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_721;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_721 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_721 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_721 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_721 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_722 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_722 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_724 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_724;

	private bool logic_uScriptAct_SetBool_Out_724 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_724 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_724 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_725 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_725;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_725;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_725;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_725;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_725 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_725;

	private bool logic_uScript_MissionPromptBlock_Show_Out_725 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_729 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_729;

	private bool logic_uScriptCon_CompareBool_True_729 = true;

	private bool logic_uScriptCon_CompareBool_False_729 = true;

	private uScript_CanSpawnPlayerTechsWithinBlockLimit logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_730 = new uScript_CanSpawnPlayerTechsWithinBlockLimit();

	private SpawnTechData[] logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_730 = new SpawnTechData[0];

	private int logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_730;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_Out_730 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_True_730 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_False_730 = true;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_733 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_733;

	private bool logic_uScript_GetMaxPlayers_Out_733 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_736 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_736;

	private bool logic_uScriptCon_CompareBool_True_736 = true;

	private bool logic_uScriptCon_CompareBool_False_736 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_738 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_738;

	private bool logic_uScriptAct_SetBool_Out_738 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_738 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_738 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_740;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_740 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_740 = "BlockLimitCritical";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_741 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_741;

	private bool logic_uScriptAct_SetBool_Out_741 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_741 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_741 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_743 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_743;

	private bool logic_uScriptCon_CompareBool_True_743 = true;

	private bool logic_uScriptCon_CompareBool_False_743 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_746 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_746;

	private uScript_ResetMissionTimer logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_747 = new uScript_ResetMissionTimer();

	private GameObject logic_uScript_ResetMissionTimer_owner_747;

	private bool logic_uScript_ResetMissionTimer_Out_747 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_748 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_748;

	private bool logic_uScript_StopMissionTimer_Out_748 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_749 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_749;

	private bool logic_uScript_HideMissionTimerUI_Out_749 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_752 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_752;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_752 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_752 = "Stage";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_754 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_754;

	private bool logic_uScriptCon_CompareBool_True_754 = true;

	private bool logic_uScriptCon_CompareBool_False_754 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_755 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_755;

	private bool logic_uScriptAct_SetBool_Out_755 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_755 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_755 = true;

	private uScript_GetMaxPlayers logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_760 = new uScript_GetMaxPlayers();

	private int logic_uScript_GetMaxPlayers_Return_760;

	private bool logic_uScript_GetMaxPlayers_Out_760 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_764 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_764;

	private bool logic_uScriptCon_CompareBool_True_764 = true;

	private bool logic_uScriptCon_CompareBool_False_764 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_766 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_766;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_766;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_766;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_766;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_766;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_766;

	private bool logic_uScript_MissionPromptBlock_Show_Out_766 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_767 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_767;

	private int logic_uScriptCon_CompareInt_B_767;

	private bool logic_uScriptCon_CompareInt_GreaterThan_767 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_767 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_767 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_767 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_767 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_767 = true;

	private uScript_CanSpawnPlayerTechsWithinBlockLimit logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_768 = new uScript_CanSpawnPlayerTechsWithinBlockLimit();

	private SpawnTechData[] logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_768 = new SpawnTechData[0];

	private int logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_768;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_Out_768 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_True_768 = true;

	private bool logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_False_768 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_769 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_769;

	private bool logic_uScriptCon_CompareBool_True_769 = true;

	private bool logic_uScriptCon_CompareBool_False_769 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_770 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_770 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_773 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_773;

	private bool logic_uScriptAct_SetBool_Out_773 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_773 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_773 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_777 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_777;

	private bool logic_uScriptAct_SetBool_Out_777 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_777 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_777 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_780 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_780;

	private bool logic_uScriptCon_CompareBool_True_780 = true;

	private bool logic_uScriptCon_CompareBool_False_780 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_784 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_784;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_784;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_784;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_784;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_784;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_784;

	private bool logic_uScript_MissionPromptBlock_Show_Out_784 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_786 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_786;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_786 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_789 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_789;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_789;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_789;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_789;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_789;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_789;

	private bool logic_uScript_MissionPromptBlock_Show_Out_789 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_790 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_790 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_791 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_791;

	private bool logic_uScriptAct_SetBool_Out_791 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_791 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_791 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_795 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_795 = new Tank[0];

	private int logic_uScript_AccessListTech_index_795;

	private Tank logic_uScript_AccessListTech_value_795;

	private bool logic_uScript_AccessListTech_Out_795 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_799 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_799;

	private BlockTypes logic_uScript_GetTankBlock_blockType_799;

	private TankBlock logic_uScript_GetTankBlock_Return_799;

	private bool logic_uScript_GetTankBlock_Out_799 = true;

	private bool logic_uScript_GetTankBlock_Returned_799 = true;

	private bool logic_uScript_GetTankBlock_NotFound_799 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_802 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_802 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_802;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_802 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_802;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_802 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_802 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_802 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_802 = true;

	private uScript_AddMoney logic_uScript_AddMoney_uScript_AddMoney_805 = new uScript_AddMoney();

	private int logic_uScript_AddMoney_amount_805;

	private bool logic_uScript_AddMoney_Out_805 = true;

	private uScriptAct_MultiplyInt_v2 logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_806 = new uScriptAct_MultiplyInt_v2();

	private int logic_uScriptAct_MultiplyInt_v2_A_806;

	private int logic_uScriptAct_MultiplyInt_v2_B_806 = -1;

	private int logic_uScriptAct_MultiplyInt_v2_IntResult_806;

	private float logic_uScriptAct_MultiplyInt_v2_FloatResult_806;

	private bool logic_uScriptAct_MultiplyInt_v2_Out_806 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_811 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_811 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_811;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_811 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_811;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_811 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_811 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_811 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_811 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_813 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_813 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_813;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_813 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_813;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_813 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_813 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_813 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_813 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_815 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_815 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_815;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_815 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_815;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_815 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_815 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_815 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_815 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_819 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_819 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_819;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_819 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_819 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_819 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_822 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_822 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_822;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_822 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_822 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_822 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_825 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_825 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_825;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_825 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_825 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_825 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_828 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_828 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_828;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_828 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_828 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_828 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_830 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_830 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_830;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_830 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_830;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_830 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_830 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_830 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_830 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_833 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_833 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_834 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_834 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_835 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_835 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_836 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_836 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_839 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_839;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_839;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_839;

	private bool logic_uScript_AddMessage_Out_839 = true;

	private bool logic_uScript_AddMessage_Shown_839 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_840 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_840 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_841 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_841 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_842 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_842 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_844 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_844 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_844;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_844 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_844;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_844 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_844 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_844 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_844 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_846 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_846 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_846;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_846 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_846;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_846 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_846 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_846 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_846 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_850 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_850 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_850;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_850 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_850;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_850 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_850 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_850 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_850 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_852 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_852 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_853 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_853 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_854 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_854;

	private bool logic_uScriptAct_SetBool_Out_854 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_854 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_854 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_857;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_857 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_857 = "msgRebuyShown";

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_862 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_862 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_862 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_862;

	private float logic_uScript_DamageTechs_leaveBlksPercent_862;

	private bool logic_uScript_DamageTechs_makeVulnerable_862;

	private bool logic_uScript_DamageTechs_Out_862 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_865 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_865 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_865 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_865;

	private float logic_uScript_DamageTechs_leaveBlksPercent_865;

	private bool logic_uScript_DamageTechs_makeVulnerable_865;

	private bool logic_uScript_DamageTechs_Out_865 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_866 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_866 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_869 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_869 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_869;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_869 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_869;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_869 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_869 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_869 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_869 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_870 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_870 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_870;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_870 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_870;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_870 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_870 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_870 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_870 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_875 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_875 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_875 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_875;

	private float logic_uScript_DamageTechs_leaveBlksPercent_875;

	private bool logic_uScript_DamageTechs_makeVulnerable_875;

	private bool logic_uScript_DamageTechs_Out_875 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_878 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_878 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_880 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_880 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_880 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_880;

	private float logic_uScript_DamageTechs_leaveBlksPercent_880;

	private bool logic_uScript_DamageTechs_makeVulnerable_880;

	private bool logic_uScript_DamageTechs_Out_880 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_881 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_881 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_881;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_881 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_881;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_881 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_881 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_881 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_881 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_882 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_882 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_883 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_883 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_884 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_884 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_884;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_884 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_884;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_884 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_884 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_884 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_884 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_885 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_885 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_887 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_887 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_888 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_888 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_888 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_888;

	private float logic_uScript_DamageTechs_leaveBlksPercent_888;

	private bool logic_uScript_DamageTechs_makeVulnerable_888;

	private bool logic_uScript_DamageTechs_Out_888 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_891 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_891 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_891;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_891 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_891;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_891 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_891 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_891 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_891 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_892 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_892 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_893 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_893 = 8f;

	private bool logic_uScript_Wait_repeat_893;

	private bool logic_uScript_Wait_Waited_893 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_894 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_894;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_894 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_895 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_895;

	private bool logic_uScriptAct_SetBool_Out_895 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_895 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_895 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_899 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_899;

	private bool logic_uScriptCon_CompareBool_True_899 = true;

	private bool logic_uScriptCon_CompareBool_False_899 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_900 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_900;

	private bool logic_uScriptAct_SetBool_Out_900 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_900 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_900 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_902 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_902;

	private bool logic_uScriptAct_SetBool_Out_902 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_902 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_902 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_906;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_906 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_906 = "AdditionalVehiclePurchased";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_908 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_908;

	private bool logic_uScriptAct_SetBool_Out_908 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_908 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_908 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_909 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_909 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_909 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_909;

	private string logic_uScript_AddOnScreenMessage_tag_909 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_909;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_909;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_909;

	private bool logic_uScript_AddOnScreenMessage_Out_909 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_909 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_912 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_912;

	private bool logic_uScriptCon_CompareBool_True_912 = true;

	private bool logic_uScriptCon_CompareBool_False_912 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_914 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_914 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_916 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_916;

	private bool logic_uScriptCon_CompareBool_True_916 = true;

	private bool logic_uScriptCon_CompareBool_False_916 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_917 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_917;

	private bool logic_uScriptCon_CompareBool_True_917 = true;

	private bool logic_uScriptCon_CompareBool_False_917 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_919 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_919 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_920 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_920 = 0.1f;

	private bool logic_uScript_Wait_repeat_920 = true;

	private bool logic_uScript_Wait_Waited_920 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_922 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_922 = true;

	private uScript_IsTechInTrigger logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_924 = new uScript_IsTechInTrigger();

	private string logic_uScript_IsTechInTrigger_triggerAreaName_924 = "";

	private Tank[] logic_uScript_IsTechInTrigger_techs_924 = new Tank[0];

	private bool logic_uScript_IsTechInTrigger_Out_924 = true;

	private bool logic_uScript_IsTechInTrigger_InRange_924 = true;

	private bool logic_uScript_IsTechInTrigger_OutOfRange_924 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_925 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_925 = true;

	private uScript_CheckTechWeapons logic_uScript_CheckTechWeapons_uScript_CheckTechWeapons_928 = new uScript_CheckTechWeapons();

	private Tank[] logic_uScript_CheckTechWeapons_techs_928 = new Tank[0];

	private BlockTypes logic_uScript_CheckTechWeapons_WeaponBlockType_928;

	private bool logic_uScript_CheckTechWeapons_Out_928 = true;

	private bool logic_uScript_CheckTechWeapons_HasOnlyGivenWeapon_928 = true;

	private bool logic_uScript_CheckTechWeapons_HasNoWeapons_928 = true;

	private bool logic_uScript_CheckTechWeapons_HasOtherWeapons_928 = true;

	private uScript_IsTechFriendlyToPlayer logic_uScript_IsTechFriendlyToPlayer_uScript_IsTechFriendlyToPlayer_929 = new uScript_IsTechFriendlyToPlayer();

	private Tank[] logic_uScript_IsTechFriendlyToPlayer_techsIn_929 = new Tank[0];

	private Tank[] logic_uScript_IsTechFriendlyToPlayer_techsOut_929 = new Tank[0];

	private bool logic_uScript_IsTechFriendlyToPlayer_Out_929 = true;

	private bool logic_uScript_IsTechFriendlyToPlayer_True_929 = true;

	private bool logic_uScript_IsTechFriendlyToPlayer_False_929 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_956 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_956;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_956 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_956 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_957 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_957;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_957 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_957 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_958 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_958;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_958 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_958 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_959 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_959;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_959 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_959 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_960 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_960 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_960;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_960 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_960;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_960 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_960 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_960 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_960 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_962 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_962 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_962;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_962 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_962;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_962 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_962 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_962 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_962 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_964 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_964 = new Tank[0];

	private int logic_uScript_AccessListTech_index_964;

	private Tank logic_uScript_AccessListTech_value_964;

	private bool logic_uScript_AccessListTech_Out_964 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_971 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_971 = new Tank[0];

	private int logic_uScript_AccessListTech_index_971;

	private Tank logic_uScript_AccessListTech_value_971;

	private bool logic_uScript_AccessListTech_Out_971 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_972 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_972 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_972;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_972 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_972;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_972 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_972 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_972 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_972 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_977 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_977 = new Tank[0];

	private int logic_uScript_AccessListTech_index_977;

	private Tank logic_uScript_AccessListTech_value_977;

	private bool logic_uScript_AccessListTech_Out_977 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_978 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_978 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_978;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_978 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_978;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_978 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_978 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_978 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_978 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_979 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_979 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_980 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_980 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_981 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_981 = new Tank[0];

	private int logic_uScript_AccessListTech_index_981;

	private Tank logic_uScript_AccessListTech_value_981;

	private bool logic_uScript_AccessListTech_Out_981 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_986 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_986 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_986;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_986 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_986;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_986 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_986 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_986 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_986 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_987 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_987 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_988 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_988;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_988 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_988 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_989 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_989;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_989 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_989 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_990 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_990;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_990 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_990 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_995 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_995 = new Tank[0];

	private int logic_uScript_AccessListTech_index_995;

	private Tank logic_uScript_AccessListTech_value_995;

	private bool logic_uScript_AccessListTech_Out_995 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_996 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_996 = new Tank[0];

	private int logic_uScript_AccessListTech_index_996;

	private Tank logic_uScript_AccessListTech_value_996;

	private bool logic_uScript_AccessListTech_Out_996 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_998 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_998;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_998 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_998 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1001 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_1001;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_1001 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_1001 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1003 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_1003 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_1003;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_1003 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_1003;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_1003 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_1003 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_1003 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_1003 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1004 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_1004 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_1004;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_1004 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_1004;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_1004 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_1004 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_1004 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_1004 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1007 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_1007 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_1007;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_1007 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_1007;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_1007 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_1007 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_1007 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_1007 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1008 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_1008 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_1008;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_1008 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_1008;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_1008 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_1008 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_1008 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_1008 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_1009 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_1009 = new Tank[0];

	private int logic_uScript_AccessListTech_index_1009;

	private Tank logic_uScript_AccessListTech_value_1009;

	private bool logic_uScript_AccessListTech_Out_1009 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1010 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_1010 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1013 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_1013 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_1015 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_1015 = new Tank[0];

	private int logic_uScript_AccessListTech_index_1015;

	private Tank logic_uScript_AccessListTech_value_1015;

	private bool logic_uScript_AccessListTech_Out_1015 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1016 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_1016;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_1016 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_1016 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1019 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_1019 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1021 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_1021 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1022 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_1022;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_1022 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_1022 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1023 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_1023;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_1023 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_1023 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_1026 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_1026;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_1026;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_1026;

	private bool logic_uScript_AddMessage_Out_1026 = true;

	private bool logic_uScript_AddMessage_Shown_1026 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1028 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1028;

	private bool logic_uScriptAct_SetBool_Out_1028 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1028 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1028 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_1030 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_1030 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1031 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_1031;

	private bool logic_uScriptCon_CompareBool_True_1031 = true;

	private bool logic_uScriptCon_CompareBool_False_1031 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_1033;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_1033 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_1033 = "msgAllPlayersMustBeInShown";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_1037 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_1037;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_1037;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_1037;

	private bool logic_uScript_AddMessage_Out_1037 = true;

	private bool logic_uScript_AddMessage_Shown_1037 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1039 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_1039;

	private bool logic_uScriptCon_CompareBool_True_1039 = true;

	private bool logic_uScriptCon_CompareBool_False_1039 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1040 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1040;

	private bool logic_uScriptAct_SetBool_Out_1040 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1040 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1040 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1041 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_1041 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_1041 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1043 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1043;

	private bool logic_uScriptAct_SetBool_Out_1043 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1043 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1043 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_1045;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_1045 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_1045 = "PlayerDetachedWeapon";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1047 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_1047;

	private bool logic_uScriptCon_CompareBool_True_1047 = true;

	private bool logic_uScriptCon_CompareBool_False_1047 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1048 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_1048 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_1048 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_1048;

	private string logic_uScript_AddOnScreenMessage_tag_1048 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_1048;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_1048;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_1048;

	private bool logic_uScript_AddOnScreenMessage_Out_1048 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_1048 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1050 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1050;

	private bool logic_uScriptAct_SetBool_Out_1050 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1050 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1050 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1053 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_1053;

	private bool logic_uScriptCon_CompareBool_True_1053 = true;

	private bool logic_uScriptCon_CompareBool_False_1053 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1055 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_1055 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1057 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1057;

	private bool logic_uScriptAct_SetBool_Out_1057 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1057 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1057 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_1059;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_1059 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_1059 = "BuyMenuOpened";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1060 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1060;

	private bool logic_uScriptAct_SetBool_Out_1060 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1060 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1060 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1063 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_1063;

	private bool logic_uScriptCon_CompareBool_True_1063 = true;

	private bool logic_uScriptCon_CompareBool_False_1063 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1067 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_1067 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1069 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1069;

	private bool logic_uScriptAct_SetBool_Out_1069 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1069 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1069 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1072 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1072;

	private bool logic_uScriptAct_SetBool_Out_1072 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1072 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1072 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1073 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_1073;

	private bool logic_uScriptCon_CompareBool_True_1073 = true;

	private bool logic_uScriptCon_CompareBool_False_1073 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1075 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1075;

	private bool logic_uScriptAct_SetBool_Out_1075 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1075 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1075 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1078 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_1078 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_1078 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_1078;

	private string logic_uScript_AddOnScreenMessage_tag_1078 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_1078;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_1078;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_1078;

	private bool logic_uScript_AddOnScreenMessage_Out_1078 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_1078 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_1079 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_1079;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_1079 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1082 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1082;

	private bool logic_uScriptAct_SetBool_Out_1082 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1082 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1082 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1084 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_1084 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1085 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_1085 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_1085 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1086 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_1086 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_1086 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_1086 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_1086 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_1086 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_1086 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1088 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_1088 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_1088 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_1088 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_1088 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_1088 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_1088 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1089 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_1089 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_1089 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_1089 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_1089 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_1089 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_1089 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1090 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_1090 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_1090 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_1090 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_1090 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_1090 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_1090 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1091 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_1091 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_1091 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1093 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_1093;

	private bool logic_uScriptCon_CompareBool_True_1093 = true;

	private bool logic_uScriptCon_CompareBool_False_1093 = true;

	private TankBlock event_UnityEngine_GameObject_TankBlock_506;

	private bool event_UnityEngine_GameObject_Accepted_506;

	private Tank event_UnityEngine_GameObject_Tech_1062;

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
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
		}
		if (null == owner_Connection_34 || !m_RegisteredForEvents)
		{
			owner_Connection_34 = parentGameObject;
		}
		if (null == owner_Connection_37 || !m_RegisteredForEvents)
		{
			owner_Connection_37 = parentGameObject;
		}
		if (null == owner_Connection_45 || !m_RegisteredForEvents)
		{
			owner_Connection_45 = parentGameObject;
		}
		if (null == owner_Connection_72 || !m_RegisteredForEvents)
		{
			owner_Connection_72 = parentGameObject;
		}
		if (null == owner_Connection_77 || !m_RegisteredForEvents)
		{
			owner_Connection_77 = parentGameObject;
		}
		if (null == owner_Connection_93 || !m_RegisteredForEvents)
		{
			owner_Connection_93 = parentGameObject;
		}
		if (null == owner_Connection_95 || !m_RegisteredForEvents)
		{
			owner_Connection_95 = parentGameObject;
		}
		if (null == owner_Connection_96 || !m_RegisteredForEvents)
		{
			owner_Connection_96 = parentGameObject;
		}
		if (null == owner_Connection_99 || !m_RegisteredForEvents)
		{
			owner_Connection_99 = parentGameObject;
		}
		if (null == owner_Connection_103 || !m_RegisteredForEvents)
		{
			owner_Connection_103 = parentGameObject;
		}
		if (null == owner_Connection_109 || !m_RegisteredForEvents)
		{
			owner_Connection_109 = parentGameObject;
		}
		if (null == owner_Connection_127 || !m_RegisteredForEvents)
		{
			owner_Connection_127 = parentGameObject;
		}
		if (null == owner_Connection_134 || !m_RegisteredForEvents)
		{
			owner_Connection_134 = parentGameObject;
		}
		if (null == owner_Connection_135 || !m_RegisteredForEvents)
		{
			owner_Connection_135 = parentGameObject;
		}
		if (null == owner_Connection_151 || !m_RegisteredForEvents)
		{
			owner_Connection_151 = parentGameObject;
		}
		if (null == owner_Connection_169 || !m_RegisteredForEvents)
		{
			owner_Connection_169 = parentGameObject;
		}
		if (null == owner_Connection_172 || !m_RegisteredForEvents)
		{
			owner_Connection_172 = parentGameObject;
		}
		if (null == owner_Connection_182 || !m_RegisteredForEvents)
		{
			owner_Connection_182 = parentGameObject;
		}
		if (null == owner_Connection_183 || !m_RegisteredForEvents)
		{
			owner_Connection_183 = parentGameObject;
		}
		if (null == owner_Connection_198 || !m_RegisteredForEvents)
		{
			owner_Connection_198 = parentGameObject;
		}
		if (null == owner_Connection_206 || !m_RegisteredForEvents)
		{
			owner_Connection_206 = parentGameObject;
		}
		if (null == owner_Connection_220 || !m_RegisteredForEvents)
		{
			owner_Connection_220 = parentGameObject;
		}
		if (null == owner_Connection_224 || !m_RegisteredForEvents)
		{
			owner_Connection_224 = parentGameObject;
		}
		if (null == owner_Connection_255 || !m_RegisteredForEvents)
		{
			owner_Connection_255 = parentGameObject;
		}
		if (null == owner_Connection_256 || !m_RegisteredForEvents)
		{
			owner_Connection_256 = parentGameObject;
		}
		if (null == owner_Connection_258 || !m_RegisteredForEvents)
		{
			owner_Connection_258 = parentGameObject;
		}
		if (null == owner_Connection_262 || !m_RegisteredForEvents)
		{
			owner_Connection_262 = parentGameObject;
		}
		if (null == owner_Connection_265 || !m_RegisteredForEvents)
		{
			owner_Connection_265 = parentGameObject;
		}
		if (null == owner_Connection_270 || !m_RegisteredForEvents)
		{
			owner_Connection_270 = parentGameObject;
		}
		if (null == owner_Connection_273 || !m_RegisteredForEvents)
		{
			owner_Connection_273 = parentGameObject;
		}
		if (null == owner_Connection_335 || !m_RegisteredForEvents)
		{
			owner_Connection_335 = parentGameObject;
		}
		if (null == owner_Connection_342 || !m_RegisteredForEvents)
		{
			owner_Connection_342 = parentGameObject;
		}
		if (null == owner_Connection_344 || !m_RegisteredForEvents)
		{
			owner_Connection_344 = parentGameObject;
		}
		if (null == owner_Connection_350 || !m_RegisteredForEvents)
		{
			owner_Connection_350 = parentGameObject;
		}
		if (null == owner_Connection_358 || !m_RegisteredForEvents)
		{
			owner_Connection_358 = parentGameObject;
		}
		if (null == owner_Connection_361 || !m_RegisteredForEvents)
		{
			owner_Connection_361 = parentGameObject;
		}
		if (null == owner_Connection_366 || !m_RegisteredForEvents)
		{
			owner_Connection_366 = parentGameObject;
		}
		if (null == owner_Connection_369 || !m_RegisteredForEvents)
		{
			owner_Connection_369 = parentGameObject;
		}
		if (null == owner_Connection_373 || !m_RegisteredForEvents)
		{
			owner_Connection_373 = parentGameObject;
		}
		if (null == owner_Connection_385 || !m_RegisteredForEvents)
		{
			owner_Connection_385 = parentGameObject;
		}
		if (null == owner_Connection_392 || !m_RegisteredForEvents)
		{
			owner_Connection_392 = parentGameObject;
		}
		if (null == owner_Connection_401 || !m_RegisteredForEvents)
		{
			owner_Connection_401 = parentGameObject;
		}
		if (null == owner_Connection_402 || !m_RegisteredForEvents)
		{
			owner_Connection_402 = parentGameObject;
		}
		if (null == owner_Connection_419 || !m_RegisteredForEvents)
		{
			owner_Connection_419 = parentGameObject;
		}
		if (null == owner_Connection_424 || !m_RegisteredForEvents)
		{
			owner_Connection_424 = parentGameObject;
		}
		if (null == owner_Connection_452 || !m_RegisteredForEvents)
		{
			owner_Connection_452 = parentGameObject;
			if (null != owner_Connection_452)
			{
				uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_452.GetComponent<uScript_MissionPromptBlock_OnResult>();
				if (null == uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2 = owner_Connection_452.AddComponent<uScript_MissionPromptBlock_OnResult>();
				}
				if (null != uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_506;
				}
			}
		}
		if (null == owner_Connection_470 || !m_RegisteredForEvents)
		{
			owner_Connection_470 = parentGameObject;
		}
		if (null == owner_Connection_472 || !m_RegisteredForEvents)
		{
			owner_Connection_472 = parentGameObject;
		}
		if (null == owner_Connection_538 || !m_RegisteredForEvents)
		{
			owner_Connection_538 = parentGameObject;
		}
		if (null == owner_Connection_556 || !m_RegisteredForEvents)
		{
			owner_Connection_556 = parentGameObject;
		}
		if (null == owner_Connection_562 || !m_RegisteredForEvents)
		{
			owner_Connection_562 = parentGameObject;
		}
		if (null == owner_Connection_599 || !m_RegisteredForEvents)
		{
			owner_Connection_599 = parentGameObject;
		}
		if (null == owner_Connection_606 || !m_RegisteredForEvents)
		{
			owner_Connection_606 = parentGameObject;
		}
		if (null == owner_Connection_634 || !m_RegisteredForEvents)
		{
			owner_Connection_634 = parentGameObject;
		}
		if (null == owner_Connection_703 || !m_RegisteredForEvents)
		{
			owner_Connection_703 = parentGameObject;
		}
		if (null == owner_Connection_713 || !m_RegisteredForEvents)
		{
			owner_Connection_713 = parentGameObject;
		}
		if (null == owner_Connection_720 || !m_RegisteredForEvents)
		{
			owner_Connection_720 = parentGameObject;
		}
		if (null == owner_Connection_751 || !m_RegisteredForEvents)
		{
			owner_Connection_751 = parentGameObject;
		}
		if (null == owner_Connection_753 || !m_RegisteredForEvents)
		{
			owner_Connection_753 = parentGameObject;
		}
		if (null == owner_Connection_796 || !m_RegisteredForEvents)
		{
			owner_Connection_796 = parentGameObject;
		}
		if (null == owner_Connection_804 || !m_RegisteredForEvents)
		{
			owner_Connection_804 = parentGameObject;
		}
		if (null == owner_Connection_809 || !m_RegisteredForEvents)
		{
			owner_Connection_809 = parentGameObject;
		}
		if (null == owner_Connection_814 || !m_RegisteredForEvents)
		{
			owner_Connection_814 = parentGameObject;
		}
		if (null == owner_Connection_817 || !m_RegisteredForEvents)
		{
			owner_Connection_817 = parentGameObject;
		}
		if (null == owner_Connection_820 || !m_RegisteredForEvents)
		{
			owner_Connection_820 = parentGameObject;
		}
		if (null == owner_Connection_823 || !m_RegisteredForEvents)
		{
			owner_Connection_823 = parentGameObject;
		}
		if (null == owner_Connection_826 || !m_RegisteredForEvents)
		{
			owner_Connection_826 = parentGameObject;
		}
		if (null == owner_Connection_829 || !m_RegisteredForEvents)
		{
			owner_Connection_829 = parentGameObject;
		}
		if (null == owner_Connection_832 || !m_RegisteredForEvents)
		{
			owner_Connection_832 = parentGameObject;
		}
		if (null == owner_Connection_843 || !m_RegisteredForEvents)
		{
			owner_Connection_843 = parentGameObject;
		}
		if (null == owner_Connection_847 || !m_RegisteredForEvents)
		{
			owner_Connection_847 = parentGameObject;
		}
		if (null == owner_Connection_851 || !m_RegisteredForEvents)
		{
			owner_Connection_851 = parentGameObject;
		}
		if (null == owner_Connection_858 || !m_RegisteredForEvents)
		{
			owner_Connection_858 = parentGameObject;
		}
		if (null == owner_Connection_859 || !m_RegisteredForEvents)
		{
			owner_Connection_859 = parentGameObject;
		}
		if (null == owner_Connection_860 || !m_RegisteredForEvents)
		{
			owner_Connection_860 = parentGameObject;
		}
		if (null == owner_Connection_861 || !m_RegisteredForEvents)
		{
			owner_Connection_861 = parentGameObject;
		}
		if (null == owner_Connection_868 || !m_RegisteredForEvents)
		{
			owner_Connection_868 = parentGameObject;
		}
		if (null == owner_Connection_871 || !m_RegisteredForEvents)
		{
			owner_Connection_871 = parentGameObject;
		}
		if (null == owner_Connection_879 || !m_RegisteredForEvents)
		{
			owner_Connection_879 = parentGameObject;
		}
		if (null == owner_Connection_890 || !m_RegisteredForEvents)
		{
			owner_Connection_890 = parentGameObject;
		}
		if (null == owner_Connection_966 || !m_RegisteredForEvents)
		{
			owner_Connection_966 = parentGameObject;
		}
		if (null == owner_Connection_969 || !m_RegisteredForEvents)
		{
			owner_Connection_969 = parentGameObject;
		}
		if (null == owner_Connection_973 || !m_RegisteredForEvents)
		{
			owner_Connection_973 = parentGameObject;
		}
		if (null == owner_Connection_982 || !m_RegisteredForEvents)
		{
			owner_Connection_982 = parentGameObject;
		}
		if (null == owner_Connection_997 || !m_RegisteredForEvents)
		{
			owner_Connection_997 = parentGameObject;
		}
		if (null == owner_Connection_1012 || !m_RegisteredForEvents)
		{
			owner_Connection_1012 = parentGameObject;
		}
		if (null == owner_Connection_1018 || !m_RegisteredForEvents)
		{
			owner_Connection_1018 = parentGameObject;
		}
		if (null == owner_Connection_1020 || !m_RegisteredForEvents)
		{
			owner_Connection_1020 = parentGameObject;
		}
		if (null == owner_Connection_1065 || !m_RegisteredForEvents)
		{
			owner_Connection_1065 = parentGameObject;
			if (null != owner_Connection_1065)
			{
				uScript_PlayerTechDestroyedEvent uScript_PlayerTechDestroyedEvent2 = owner_Connection_1065.GetComponent<uScript_PlayerTechDestroyedEvent>();
				if (null == uScript_PlayerTechDestroyedEvent2)
				{
					uScript_PlayerTechDestroyedEvent2 = owner_Connection_1065.AddComponent<uScript_PlayerTechDestroyedEvent>();
				}
				if (null != uScript_PlayerTechDestroyedEvent2)
				{
					uScript_PlayerTechDestroyedEvent2.TechDestroyedEvent += Instance_TechDestroyedEvent_1062;
				}
			}
		}
		if (null == owner_Connection_1080 || !m_RegisteredForEvents)
		{
			owner_Connection_1080 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_452)
		{
			uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_452.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null == uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2 = owner_Connection_452.AddComponent<uScript_MissionPromptBlock_OnResult>();
			}
			if (null != uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_506;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_1065)
		{
			uScript_PlayerTechDestroyedEvent uScript_PlayerTechDestroyedEvent2 = owner_Connection_1065.GetComponent<uScript_PlayerTechDestroyedEvent>();
			if (null == uScript_PlayerTechDestroyedEvent2)
			{
				uScript_PlayerTechDestroyedEvent2 = owner_Connection_1065.AddComponent<uScript_PlayerTechDestroyedEvent>();
			}
			if (null != uScript_PlayerTechDestroyedEvent2)
			{
				uScript_PlayerTechDestroyedEvent2.TechDestroyedEvent += Instance_TechDestroyedEvent_1062;
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
		if (null != owner_Connection_452)
		{
			uScript_MissionPromptBlock_OnResult component3 = owner_Connection_452.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null != component3)
			{
				component3.ResponseEvent -= Instance_ResponseEvent_506;
			}
		}
		if (null != owner_Connection_1065)
		{
			uScript_PlayerTechDestroyedEvent component4 = owner_Connection_1065.GetComponent<uScript_PlayerTechDestroyedEvent>();
			if (null != component4)
			{
				component4.TechDestroyedEvent -= Instance_TechDestroyedEvent_1062;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_11.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_20.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_27.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_32.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_36.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_43.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_44.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_61.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_71.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_74.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_76.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_79.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_92.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_94.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_97.SetParent(g);
		logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_98.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_100.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_101.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_104.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_106.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_107.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_108.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_110.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_111.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_114.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_117.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_126.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_129.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.SetParent(g);
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_133.SetParent(g);
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_136.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_137.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_139.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_141.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_144.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_148.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_149.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_156.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_160.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_163.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_166.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_174.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_175.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_176.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_180.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_185.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_187.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_188.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_192.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_196.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_199.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_200.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_202.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_204.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_211.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_216.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_219.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_221.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_222.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_226.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_228.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_232.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_234.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_235.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_243.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_245.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_247.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_249.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_253.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_254.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_257.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_259.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_260.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_261.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_263.SetParent(g);
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_266.SetParent(g);
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_267.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_269.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_271.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_272.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_274.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_276.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_279.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_280.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_283.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_285.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_289.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_290.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_295.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_296.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_297.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_298.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_303.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_306.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_308.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_309.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_317.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_318.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_319.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_321.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_323.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_325.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_329.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_331.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_333.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_334.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_336.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_340.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_343.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_346.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_347.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_352.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_353.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_356.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.SetParent(g);
		logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_359.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_362.SetParent(g);
		logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_363.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_364.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_368.SetParent(g);
		logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_370.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_372.SetParent(g);
		logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_374.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_375.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_377.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_380.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_384.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_387.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_388.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_389.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_390.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_394.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_397.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_398.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_399.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_400.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_403.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_404.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_407.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_411.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_413.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_417.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_418.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_420.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_423.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_427.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_428.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_429.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_430.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_432.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_433.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_436.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_442.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_443.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_447.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_448.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_450.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_451.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_453.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_454.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_456.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_458.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_459.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_460.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_465.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_466.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_468.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_475.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_481.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_483.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_489.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_492.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_493.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_494.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_495.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_496.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_497.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_498.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_499.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_502.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_503.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_504.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_507.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_508.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_511.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_512.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_513.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_515.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_518.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_520.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_521.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_522.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_523.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_526.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_527.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_528.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_529.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_531.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_532.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_533.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_535.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_537.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_541.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_542.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_545.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_546.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_547.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_548.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_549.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_550.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_551.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_552.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_553.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_554.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_555.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_557.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_559.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_560.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_561.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_563.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_565.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_566.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_567.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_568.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_569.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_571.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_572.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_573.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_575.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_578.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_579.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_580.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_583.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_584.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_586.SetParent(g);
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_588.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_592.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_596.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_608.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_614.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_615.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_616.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_617.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_620.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_625.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_628.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_629.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_632.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_637.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_639.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_645.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_646.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_649.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_650.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_680.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_683.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_686.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_688.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_689.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_695.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_696.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_697.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_698.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_701.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_702.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_706.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_707.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_709.SetParent(g);
		logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_712.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_714.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_716.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_719.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_721.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_722.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_724.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_725.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_729.SetParent(g);
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_730.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_733.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_736.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_738.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_741.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_743.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_746.SetParent(g);
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_747.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_748.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_749.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_754.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_755.SetParent(g);
		logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_760.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_764.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_766.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_767.SetParent(g);
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_768.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_769.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_770.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_773.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_777.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_780.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_784.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_786.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_789.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_790.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_791.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_795.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_799.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_802.SetParent(g);
		logic_uScript_AddMoney_uScript_AddMoney_805.SetParent(g);
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_806.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_811.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_813.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_815.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_819.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_822.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_825.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_828.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_830.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_833.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_834.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_835.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_836.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_839.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_840.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_841.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_842.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_844.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_846.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_850.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_852.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_853.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_854.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_862.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_865.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_866.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_869.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_870.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_875.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_878.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_880.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_881.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_882.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_883.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_884.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_885.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_887.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_888.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_891.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_892.SetParent(g);
		logic_uScript_Wait_uScript_Wait_893.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_894.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_895.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_899.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_900.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_902.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_908.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_909.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_912.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_914.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_916.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_917.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_919.SetParent(g);
		logic_uScript_Wait_uScript_Wait_920.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_922.SetParent(g);
		logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_924.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_925.SetParent(g);
		logic_uScript_CheckTechWeapons_uScript_CheckTechWeapons_928.SetParent(g);
		logic_uScript_IsTechFriendlyToPlayer_uScript_IsTechFriendlyToPlayer_929.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_956.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_957.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_958.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_959.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_960.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_962.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_964.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_971.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_972.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_977.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_978.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_979.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_980.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_981.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_986.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_987.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_988.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_989.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_990.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_995.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_996.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_998.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1001.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1003.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1004.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1007.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1008.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_1009.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1010.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1013.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_1015.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1016.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1019.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1021.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1022.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1023.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_1026.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1028.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_1030.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1031.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_1037.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1039.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1040.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1041.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1043.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1047.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1048.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1050.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1053.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1055.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1057.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1060.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1063.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1067.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1069.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1072.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1073.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1075.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1078.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_1079.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1082.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1084.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1085.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1086.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1088.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1089.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1090.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1091.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1093.SetParent(g);
		owner_Connection_3 = parentGameObject;
		owner_Connection_6 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_34 = parentGameObject;
		owner_Connection_37 = parentGameObject;
		owner_Connection_45 = parentGameObject;
		owner_Connection_72 = parentGameObject;
		owner_Connection_77 = parentGameObject;
		owner_Connection_93 = parentGameObject;
		owner_Connection_95 = parentGameObject;
		owner_Connection_96 = parentGameObject;
		owner_Connection_99 = parentGameObject;
		owner_Connection_103 = parentGameObject;
		owner_Connection_109 = parentGameObject;
		owner_Connection_127 = parentGameObject;
		owner_Connection_134 = parentGameObject;
		owner_Connection_135 = parentGameObject;
		owner_Connection_151 = parentGameObject;
		owner_Connection_169 = parentGameObject;
		owner_Connection_172 = parentGameObject;
		owner_Connection_182 = parentGameObject;
		owner_Connection_183 = parentGameObject;
		owner_Connection_198 = parentGameObject;
		owner_Connection_206 = parentGameObject;
		owner_Connection_220 = parentGameObject;
		owner_Connection_224 = parentGameObject;
		owner_Connection_255 = parentGameObject;
		owner_Connection_256 = parentGameObject;
		owner_Connection_258 = parentGameObject;
		owner_Connection_262 = parentGameObject;
		owner_Connection_265 = parentGameObject;
		owner_Connection_270 = parentGameObject;
		owner_Connection_273 = parentGameObject;
		owner_Connection_335 = parentGameObject;
		owner_Connection_342 = parentGameObject;
		owner_Connection_344 = parentGameObject;
		owner_Connection_350 = parentGameObject;
		owner_Connection_358 = parentGameObject;
		owner_Connection_361 = parentGameObject;
		owner_Connection_366 = parentGameObject;
		owner_Connection_369 = parentGameObject;
		owner_Connection_373 = parentGameObject;
		owner_Connection_385 = parentGameObject;
		owner_Connection_392 = parentGameObject;
		owner_Connection_401 = parentGameObject;
		owner_Connection_402 = parentGameObject;
		owner_Connection_419 = parentGameObject;
		owner_Connection_424 = parentGameObject;
		owner_Connection_452 = parentGameObject;
		owner_Connection_470 = parentGameObject;
		owner_Connection_472 = parentGameObject;
		owner_Connection_538 = parentGameObject;
		owner_Connection_556 = parentGameObject;
		owner_Connection_562 = parentGameObject;
		owner_Connection_599 = parentGameObject;
		owner_Connection_606 = parentGameObject;
		owner_Connection_634 = parentGameObject;
		owner_Connection_703 = parentGameObject;
		owner_Connection_713 = parentGameObject;
		owner_Connection_720 = parentGameObject;
		owner_Connection_751 = parentGameObject;
		owner_Connection_753 = parentGameObject;
		owner_Connection_796 = parentGameObject;
		owner_Connection_804 = parentGameObject;
		owner_Connection_809 = parentGameObject;
		owner_Connection_814 = parentGameObject;
		owner_Connection_817 = parentGameObject;
		owner_Connection_820 = parentGameObject;
		owner_Connection_823 = parentGameObject;
		owner_Connection_826 = parentGameObject;
		owner_Connection_829 = parentGameObject;
		owner_Connection_832 = parentGameObject;
		owner_Connection_843 = parentGameObject;
		owner_Connection_847 = parentGameObject;
		owner_Connection_851 = parentGameObject;
		owner_Connection_858 = parentGameObject;
		owner_Connection_859 = parentGameObject;
		owner_Connection_860 = parentGameObject;
		owner_Connection_861 = parentGameObject;
		owner_Connection_868 = parentGameObject;
		owner_Connection_871 = parentGameObject;
		owner_Connection_879 = parentGameObject;
		owner_Connection_890 = parentGameObject;
		owner_Connection_966 = parentGameObject;
		owner_Connection_969 = parentGameObject;
		owner_Connection_973 = parentGameObject;
		owner_Connection_982 = parentGameObject;
		owner_Connection_997 = parentGameObject;
		owner_Connection_1012 = parentGameObject;
		owner_Connection_1018 = parentGameObject;
		owner_Connection_1020 = parentGameObject;
		owner_Connection_1065 = parentGameObject;
		owner_Connection_1080 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_27.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_61.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_196.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_571.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_629.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_746.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Save_Out += SubGraph_SaveLoadBool_Save_Out_2;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Load_Out += SubGraph_SaveLoadBool_Load_Out_2;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_2;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Save_Out += SubGraph_SaveLoadInt_Save_Out_12;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Load_Out += SubGraph_SaveLoadInt_Load_Out_12;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_12;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_27.Out += SubGraph_CompleteObjectiveStage_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Save_Out += SubGraph_SaveLoadBool_Save_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Load_Out += SubGraph_SaveLoadBool_Load_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Save_Out += SubGraph_SaveLoadBool_Save_Out_53;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Load_Out += SubGraph_SaveLoadBool_Load_Out_53;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_53;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_61.Out += SubGraph_CompleteObjectiveStage_Out_61;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Save_Out += SubGraph_SaveLoadBool_Save_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Load_Out += SubGraph_SaveLoadBool_Load_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Save_Out += SubGraph_SaveLoadBool_Save_Out_68;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Load_Out += SubGraph_SaveLoadBool_Load_Out_68;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_68;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Save_Out += SubGraph_SaveLoadBool_Save_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Load_Out += SubGraph_SaveLoadBool_Load_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Save_Out += SubGraph_SaveLoadBool_Save_Out_89;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Load_Out += SubGraph_SaveLoadBool_Load_Out_89;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_89;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Save_Out += SubGraph_SaveLoadBool_Save_Out_90;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Load_Out += SubGraph_SaveLoadBool_Load_Out_90;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_90;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Save_Out += SubGraph_SaveLoadBool_Save_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Load_Out += SubGraph_SaveLoadBool_Load_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Save_Out += SubGraph_SaveLoadBool_Save_Out_124;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Load_Out += SubGraph_SaveLoadBool_Load_Out_124;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_124;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Save_Out += SubGraph_SaveLoadBool_Save_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Load_Out += SubGraph_SaveLoadBool_Load_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Save_Out += SubGraph_SaveLoadBool_Save_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Load_Out += SubGraph_SaveLoadBool_Load_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_147;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_196.Out += SubGraph_CompleteObjectiveStage_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Save_Out += SubGraph_SaveLoadBool_Save_Out_209;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Load_Out += SubGraph_SaveLoadBool_Load_Out_209;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_209;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Save_Out += SubGraph_SaveLoadBool_Save_Out_238;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Load_Out += SubGraph_SaveLoadBool_Load_Out_238;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_238;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Save_Out += SubGraph_SaveLoadBool_Save_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Load_Out += SubGraph_SaveLoadBool_Load_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Save_Out += SubGraph_SaveLoadBool_Save_Out_288;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Load_Out += SubGraph_SaveLoadBool_Load_Out_288;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_288;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Save_Out += SubGraph_SaveLoadBool_Save_Out_314;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Load_Out += SubGraph_SaveLoadBool_Load_Out_314;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_314;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Save_Out += SubGraph_SaveLoadBool_Save_Out_315;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Load_Out += SubGraph_SaveLoadBool_Load_Out_315;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_315;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Save_Out += SubGraph_SaveLoadBool_Save_Out_316;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Load_Out += SubGraph_SaveLoadBool_Load_Out_316;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_316;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Save_Out += SubGraph_SaveLoadBool_Save_Out_328;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Load_Out += SubGraph_SaveLoadBool_Load_Out_328;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_328;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output1 += uScriptCon_ManualSwitch_Output1_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output2 += uScriptCon_ManualSwitch_Output2_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output3 += uScriptCon_ManualSwitch_Output3_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output4 += uScriptCon_ManualSwitch_Output4_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output5 += uScriptCon_ManualSwitch_Output5_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output6 += uScriptCon_ManualSwitch_Output6_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output7 += uScriptCon_ManualSwitch_Output7_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output8 += uScriptCon_ManualSwitch_Output8_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output1 += uScriptCon_ManualSwitch_Output1_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output2 += uScriptCon_ManualSwitch_Output2_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output3 += uScriptCon_ManualSwitch_Output3_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output4 += uScriptCon_ManualSwitch_Output4_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output5 += uScriptCon_ManualSwitch_Output5_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output6 += uScriptCon_ManualSwitch_Output6_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output7 += uScriptCon_ManualSwitch_Output7_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output8 += uScriptCon_ManualSwitch_Output8_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output1 += uScriptCon_ManualSwitch_Output1_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output2 += uScriptCon_ManualSwitch_Output2_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output3 += uScriptCon_ManualSwitch_Output3_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output4 += uScriptCon_ManualSwitch_Output4_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output5 += uScriptCon_ManualSwitch_Output5_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output6 += uScriptCon_ManualSwitch_Output6_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output7 += uScriptCon_ManualSwitch_Output7_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output8 += uScriptCon_ManualSwitch_Output8_485;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539.Out += SubGraph_AddMessageWithPadSupport_Out_539;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539.Shown += SubGraph_AddMessageWithPadSupport_Shown_539;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_571.Out += SubGraph_CompleteObjectiveStage_Out_571;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output1 += uScriptCon_ManualSwitch_Output1_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output2 += uScriptCon_ManualSwitch_Output2_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output3 += uScriptCon_ManualSwitch_Output3_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output4 += uScriptCon_ManualSwitch_Output4_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output5 += uScriptCon_ManualSwitch_Output5_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output6 += uScriptCon_ManualSwitch_Output6_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output7 += uScriptCon_ManualSwitch_Output7_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output8 += uScriptCon_ManualSwitch_Output8_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output1 += uScriptCon_ManualSwitch_Output1_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output2 += uScriptCon_ManualSwitch_Output2_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output3 += uScriptCon_ManualSwitch_Output3_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output4 += uScriptCon_ManualSwitch_Output4_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output5 += uScriptCon_ManualSwitch_Output5_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output6 += uScriptCon_ManualSwitch_Output6_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output7 += uScriptCon_ManualSwitch_Output7_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output8 += uScriptCon_ManualSwitch_Output8_604;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622.Out += SubGraph_AddMessageWithPadSupport_Out_622;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622.Shown += SubGraph_AddMessageWithPadSupport_Shown_622;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_629.Out += SubGraph_CompleteObjectiveStage_Out_629;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Save_Out += SubGraph_SaveLoadBool_Save_Out_661;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Load_Out += SubGraph_SaveLoadBool_Load_Out_661;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_661;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Save_Out += SubGraph_SaveLoadBool_Save_Out_662;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Load_Out += SubGraph_SaveLoadBool_Load_Out_662;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_662;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Save_Out += SubGraph_SaveLoadBool_Save_Out_663;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Load_Out += SubGraph_SaveLoadBool_Load_Out_663;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_663;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Save_Out += SubGraph_SaveLoadBool_Save_Out_664;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Load_Out += SubGraph_SaveLoadBool_Load_Out_664;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_664;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Save_Out += SubGraph_SaveLoadBool_Save_Out_665;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Load_Out += SubGraph_SaveLoadBool_Load_Out_665;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_665;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Save_Out += SubGraph_SaveLoadBool_Save_Out_666;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Load_Out += SubGraph_SaveLoadBool_Load_Out_666;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_666;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Save_Out += SubGraph_SaveLoadBool_Save_Out_667;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Load_Out += SubGraph_SaveLoadBool_Load_Out_667;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_667;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Save_Out += SubGraph_SaveLoadBool_Save_Out_668;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Load_Out += SubGraph_SaveLoadBool_Load_Out_668;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_668;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Save_Out += SubGraph_SaveLoadBool_Save_Out_669;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Load_Out += SubGraph_SaveLoadBool_Load_Out_669;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_669;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Save_Out += SubGraph_SaveLoadBool_Save_Out_670;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Load_Out += SubGraph_SaveLoadBool_Load_Out_670;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_670;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Save_Out += SubGraph_SaveLoadBool_Save_Out_692;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Load_Out += SubGraph_SaveLoadBool_Load_Out_692;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_692;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Save_Out += SubGraph_SaveLoadBool_Save_Out_694;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Load_Out += SubGraph_SaveLoadBool_Load_Out_694;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_694;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Save_Out += SubGraph_SaveLoadBool_Save_Out_740;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Load_Out += SubGraph_SaveLoadBool_Load_Out_740;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_740;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_746.Out += SubGraph_LoadObjectiveStates_Out_746;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.Save_Out += SubGraph_SaveLoadInt_Save_Out_752;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.Load_Out += SubGraph_SaveLoadInt_Load_Out_752;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_752;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Save_Out += SubGraph_SaveLoadBool_Save_Out_857;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Load_Out += SubGraph_SaveLoadBool_Load_Out_857;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_857;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Save_Out += SubGraph_SaveLoadBool_Save_Out_906;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Load_Out += SubGraph_SaveLoadBool_Load_Out_906;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_906;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Save_Out += SubGraph_SaveLoadBool_Save_Out_1033;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Load_Out += SubGraph_SaveLoadBool_Load_Out_1033;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_1033;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Save_Out += SubGraph_SaveLoadBool_Save_Out_1045;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Load_Out += SubGraph_SaveLoadBool_Load_Out_1045;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_1045;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Save_Out += SubGraph_SaveLoadBool_Save_Out_1059;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Load_Out += SubGraph_SaveLoadBool_Load_Out_1059;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_1059;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_27.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_61.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_196.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_571.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_629.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_746.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_27.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_61.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_148.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_196.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_269.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_571.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_629.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_706.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_746.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_1079.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_27.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_43.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_61.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_129.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_144.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_192.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_196.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_202.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_211.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_222.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_232.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_243.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_254.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_272.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_276.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_303.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_388.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_389.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_465.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_481.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_492.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_502.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_521.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_523.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_545.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_557.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_571.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_575.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_629.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_683.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.OnDisable();
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_730.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_746.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.OnDisable();
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_768.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_799.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_839.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.OnDisable();
		logic_uScript_Wait_uScript_Wait_893.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_909.OnDisable();
		logic_uScript_Wait_uScript_Wait_920.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_1026.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_1037.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1041.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1048.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1078.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1085.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1091.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_27.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_61.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_196.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_571.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_629.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_746.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_27.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_61.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_196.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_571.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_629.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_746.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Save_Out -= SubGraph_SaveLoadBool_Save_Out_2;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Load_Out -= SubGraph_SaveLoadBool_Load_Out_2;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_2;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Save_Out -= SubGraph_SaveLoadInt_Save_Out_12;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Load_Out -= SubGraph_SaveLoadInt_Load_Out_12;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_12;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_27.Out -= SubGraph_CompleteObjectiveStage_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Save_Out -= SubGraph_SaveLoadBool_Save_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Load_Out -= SubGraph_SaveLoadBool_Load_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Save_Out -= SubGraph_SaveLoadBool_Save_Out_53;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Load_Out -= SubGraph_SaveLoadBool_Load_Out_53;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_53;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_61.Out -= SubGraph_CompleteObjectiveStage_Out_61;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Save_Out -= SubGraph_SaveLoadBool_Save_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Load_Out -= SubGraph_SaveLoadBool_Load_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Save_Out -= SubGraph_SaveLoadBool_Save_Out_68;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Load_Out -= SubGraph_SaveLoadBool_Load_Out_68;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_68;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Save_Out -= SubGraph_SaveLoadBool_Save_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Load_Out -= SubGraph_SaveLoadBool_Load_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_69;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Save_Out -= SubGraph_SaveLoadBool_Save_Out_89;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Load_Out -= SubGraph_SaveLoadBool_Load_Out_89;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_89;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Save_Out -= SubGraph_SaveLoadBool_Save_Out_90;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Load_Out -= SubGraph_SaveLoadBool_Load_Out_90;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_90;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Save_Out -= SubGraph_SaveLoadBool_Save_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Load_Out -= SubGraph_SaveLoadBool_Load_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Save_Out -= SubGraph_SaveLoadBool_Save_Out_124;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Load_Out -= SubGraph_SaveLoadBool_Load_Out_124;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_124;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Save_Out -= SubGraph_SaveLoadBool_Save_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Load_Out -= SubGraph_SaveLoadBool_Load_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_125;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Save_Out -= SubGraph_SaveLoadBool_Save_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Load_Out -= SubGraph_SaveLoadBool_Load_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_147;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_196.Out -= SubGraph_CompleteObjectiveStage_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Save_Out -= SubGraph_SaveLoadBool_Save_Out_209;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Load_Out -= SubGraph_SaveLoadBool_Load_Out_209;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_209;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Save_Out -= SubGraph_SaveLoadBool_Save_Out_238;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Load_Out -= SubGraph_SaveLoadBool_Load_Out_238;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_238;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Save_Out -= SubGraph_SaveLoadBool_Save_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Load_Out -= SubGraph_SaveLoadBool_Load_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Save_Out -= SubGraph_SaveLoadBool_Save_Out_288;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Load_Out -= SubGraph_SaveLoadBool_Load_Out_288;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_288;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Save_Out -= SubGraph_SaveLoadBool_Save_Out_314;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Load_Out -= SubGraph_SaveLoadBool_Load_Out_314;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_314;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Save_Out -= SubGraph_SaveLoadBool_Save_Out_315;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Load_Out -= SubGraph_SaveLoadBool_Load_Out_315;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_315;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Save_Out -= SubGraph_SaveLoadBool_Save_Out_316;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Load_Out -= SubGraph_SaveLoadBool_Load_Out_316;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_316;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Save_Out -= SubGraph_SaveLoadBool_Save_Out_328;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Load_Out -= SubGraph_SaveLoadBool_Load_Out_328;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_328;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output1 -= uScriptCon_ManualSwitch_Output1_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output2 -= uScriptCon_ManualSwitch_Output2_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output3 -= uScriptCon_ManualSwitch_Output3_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output4 -= uScriptCon_ManualSwitch_Output4_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output5 -= uScriptCon_ManualSwitch_Output5_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output6 -= uScriptCon_ManualSwitch_Output6_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output7 -= uScriptCon_ManualSwitch_Output7_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output8 -= uScriptCon_ManualSwitch_Output8_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output1 -= uScriptCon_ManualSwitch_Output1_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output2 -= uScriptCon_ManualSwitch_Output2_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output3 -= uScriptCon_ManualSwitch_Output3_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output4 -= uScriptCon_ManualSwitch_Output4_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output5 -= uScriptCon_ManualSwitch_Output5_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output6 -= uScriptCon_ManualSwitch_Output6_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output7 -= uScriptCon_ManualSwitch_Output7_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.Output8 -= uScriptCon_ManualSwitch_Output8_467;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output1 -= uScriptCon_ManualSwitch_Output1_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output2 -= uScriptCon_ManualSwitch_Output2_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output3 -= uScriptCon_ManualSwitch_Output3_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output4 -= uScriptCon_ManualSwitch_Output4_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output5 -= uScriptCon_ManualSwitch_Output5_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output6 -= uScriptCon_ManualSwitch_Output6_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output7 -= uScriptCon_ManualSwitch_Output7_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output8 -= uScriptCon_ManualSwitch_Output8_485;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539.Out -= SubGraph_AddMessageWithPadSupport_Out_539;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539.Shown -= SubGraph_AddMessageWithPadSupport_Shown_539;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_571.Out -= SubGraph_CompleteObjectiveStage_Out_571;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output1 -= uScriptCon_ManualSwitch_Output1_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output2 -= uScriptCon_ManualSwitch_Output2_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output3 -= uScriptCon_ManualSwitch_Output3_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output4 -= uScriptCon_ManualSwitch_Output4_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output5 -= uScriptCon_ManualSwitch_Output5_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output6 -= uScriptCon_ManualSwitch_Output6_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output7 -= uScriptCon_ManualSwitch_Output7_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.Output8 -= uScriptCon_ManualSwitch_Output8_574;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output1 -= uScriptCon_ManualSwitch_Output1_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output2 -= uScriptCon_ManualSwitch_Output2_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output3 -= uScriptCon_ManualSwitch_Output3_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output4 -= uScriptCon_ManualSwitch_Output4_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output5 -= uScriptCon_ManualSwitch_Output5_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output6 -= uScriptCon_ManualSwitch_Output6_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output7 -= uScriptCon_ManualSwitch_Output7_604;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.Output8 -= uScriptCon_ManualSwitch_Output8_604;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622.Out -= SubGraph_AddMessageWithPadSupport_Out_622;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622.Shown -= SubGraph_AddMessageWithPadSupport_Shown_622;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_629.Out -= SubGraph_CompleteObjectiveStage_Out_629;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Save_Out -= SubGraph_SaveLoadBool_Save_Out_661;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Load_Out -= SubGraph_SaveLoadBool_Load_Out_661;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_661;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Save_Out -= SubGraph_SaveLoadBool_Save_Out_662;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Load_Out -= SubGraph_SaveLoadBool_Load_Out_662;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_662;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Save_Out -= SubGraph_SaveLoadBool_Save_Out_663;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Load_Out -= SubGraph_SaveLoadBool_Load_Out_663;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_663;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Save_Out -= SubGraph_SaveLoadBool_Save_Out_664;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Load_Out -= SubGraph_SaveLoadBool_Load_Out_664;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_664;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Save_Out -= SubGraph_SaveLoadBool_Save_Out_665;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Load_Out -= SubGraph_SaveLoadBool_Load_Out_665;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_665;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Save_Out -= SubGraph_SaveLoadBool_Save_Out_666;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Load_Out -= SubGraph_SaveLoadBool_Load_Out_666;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_666;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Save_Out -= SubGraph_SaveLoadBool_Save_Out_667;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Load_Out -= SubGraph_SaveLoadBool_Load_Out_667;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_667;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Save_Out -= SubGraph_SaveLoadBool_Save_Out_668;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Load_Out -= SubGraph_SaveLoadBool_Load_Out_668;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_668;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Save_Out -= SubGraph_SaveLoadBool_Save_Out_669;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Load_Out -= SubGraph_SaveLoadBool_Load_Out_669;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_669;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Save_Out -= SubGraph_SaveLoadBool_Save_Out_670;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Load_Out -= SubGraph_SaveLoadBool_Load_Out_670;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_670;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Save_Out -= SubGraph_SaveLoadBool_Save_Out_692;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Load_Out -= SubGraph_SaveLoadBool_Load_Out_692;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_692;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Save_Out -= SubGraph_SaveLoadBool_Save_Out_694;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Load_Out -= SubGraph_SaveLoadBool_Load_Out_694;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_694;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Save_Out -= SubGraph_SaveLoadBool_Save_Out_740;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Load_Out -= SubGraph_SaveLoadBool_Load_Out_740;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_740;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_746.Out -= SubGraph_LoadObjectiveStates_Out_746;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.Save_Out -= SubGraph_SaveLoadInt_Save_Out_752;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.Load_Out -= SubGraph_SaveLoadInt_Load_Out_752;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_752;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Save_Out -= SubGraph_SaveLoadBool_Save_Out_857;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Load_Out -= SubGraph_SaveLoadBool_Load_Out_857;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_857;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Save_Out -= SubGraph_SaveLoadBool_Save_Out_906;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Load_Out -= SubGraph_SaveLoadBool_Load_Out_906;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_906;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Save_Out -= SubGraph_SaveLoadBool_Save_Out_1033;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Load_Out -= SubGraph_SaveLoadBool_Load_Out_1033;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_1033;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Save_Out -= SubGraph_SaveLoadBool_Save_Out_1045;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Load_Out -= SubGraph_SaveLoadBool_Load_Out_1045;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_1045;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Save_Out -= SubGraph_SaveLoadBool_Save_Out_1059;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Load_Out -= SubGraph_SaveLoadBool_Load_Out_1059;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_1059;
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

	private void Instance_ResponseEvent_506(object o, uScript_MissionPromptBlock_OnResult.PromptResultEventArgs e)
	{
		event_UnityEngine_GameObject_TankBlock_506 = e.TankBlock;
		event_UnityEngine_GameObject_Accepted_506 = e.Accepted;
		Relay_ResponseEvent_506();
	}

	private void Instance_TechDestroyedEvent_1062(object o, uScript_PlayerTechDestroyedEvent.TechDestroyedEventArgs e)
	{
		event_UnityEngine_GameObject_Tech_1062 = e.Tech;
		Relay_TechDestroyedEvent_1062();
	}

	private void SubGraph_SaveLoadBool_Save_Out_2(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_2 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_2;
		Relay_Save_Out_2();
	}

	private void SubGraph_SaveLoadBool_Load_Out_2(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_2 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_2;
		Relay_Load_Out_2();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_2(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_2 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_2;
		Relay_Restart_Out_2();
	}

	private void SubGraph_SaveLoadInt_Save_Out_12(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_12 = e.integer;
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_SubGraph_SaveLoadInt_integer_12;
		Relay_Save_Out_12();
	}

	private void SubGraph_SaveLoadInt_Load_Out_12(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_12 = e.integer;
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_SubGraph_SaveLoadInt_integer_12;
		Relay_Load_Out_12();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_12(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_12 = e.integer;
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_SubGraph_SaveLoadInt_integer_12;
		Relay_Restart_Out_12();
	}

	private void SubGraph_CompleteObjectiveStage_Out_27(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_27 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_27;
		Relay_Out_27();
	}

	private void SubGraph_SaveLoadBool_Save_Out_40(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_40;
		Relay_Save_Out_40();
	}

	private void SubGraph_SaveLoadBool_Load_Out_40(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_40;
		Relay_Load_Out_40();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_40(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_40;
		Relay_Restart_Out_40();
	}

	private void SubGraph_SaveLoadBool_Save_Out_53(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = e.boolean;
		local_TechsSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_53;
		Relay_Save_Out_53();
	}

	private void SubGraph_SaveLoadBool_Load_Out_53(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = e.boolean;
		local_TechsSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_53;
		Relay_Load_Out_53();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_53(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = e.boolean;
		local_TechsSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_53;
		Relay_Restart_Out_53();
	}

	private void SubGraph_CompleteObjectiveStage_Out_61(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_61 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_61;
		Relay_Out_61();
	}

	private void SubGraph_SaveLoadBool_Save_Out_65(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = e.boolean;
		local_TestComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_65;
		Relay_Save_Out_65();
	}

	private void SubGraph_SaveLoadBool_Load_Out_65(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = e.boolean;
		local_TestComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_65;
		Relay_Load_Out_65();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_65(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = e.boolean;
		local_TestComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_65;
		Relay_Restart_Out_65();
	}

	private void SubGraph_SaveLoadBool_Save_Out_68(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_68;
		Relay_Save_Out_68();
	}

	private void SubGraph_SaveLoadBool_Load_Out_68(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_68;
		Relay_Load_Out_68();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_68(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_68;
		Relay_Restart_Out_68();
	}

	private void SubGraph_SaveLoadBool_Save_Out_69(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = e.boolean;
		local_TestStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_69;
		Relay_Save_Out_69();
	}

	private void SubGraph_SaveLoadBool_Load_Out_69(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = e.boolean;
		local_TestStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_69;
		Relay_Load_Out_69();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_69(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = e.boolean;
		local_TestStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_69;
		Relay_Restart_Out_69();
	}

	private void SubGraph_SaveLoadBool_Save_Out_89(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = e.boolean;
		local_EnemyAlive1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_89;
		Relay_Save_Out_89();
	}

	private void SubGraph_SaveLoadBool_Load_Out_89(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = e.boolean;
		local_EnemyAlive1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_89;
		Relay_Load_Out_89();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_89(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = e.boolean;
		local_EnemyAlive1_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_89;
		Relay_Restart_Out_89();
	}

	private void SubGraph_SaveLoadBool_Save_Out_90(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = e.boolean;
		local_EnemyAlive2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_90;
		Relay_Save_Out_90();
	}

	private void SubGraph_SaveLoadBool_Load_Out_90(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = e.boolean;
		local_EnemyAlive2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_90;
		Relay_Load_Out_90();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_90(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = e.boolean;
		local_EnemyAlive2_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_90;
		Relay_Restart_Out_90();
	}

	private void SubGraph_SaveLoadBool_Save_Out_91(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = e.boolean;
		local_EnemyAlive3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_91;
		Relay_Save_Out_91();
	}

	private void SubGraph_SaveLoadBool_Load_Out_91(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = e.boolean;
		local_EnemyAlive3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_91;
		Relay_Load_Out_91();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_91(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = e.boolean;
		local_EnemyAlive3_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_91;
		Relay_Restart_Out_91();
	}

	private void SubGraph_SaveLoadBool_Save_Out_124(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_124 = e.boolean;
		local_EnemyAlive4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_124;
		Relay_Save_Out_124();
	}

	private void SubGraph_SaveLoadBool_Load_Out_124(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_124 = e.boolean;
		local_EnemyAlive4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_124;
		Relay_Load_Out_124();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_124(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_124 = e.boolean;
		local_EnemyAlive4_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_124;
		Relay_Restart_Out_124();
	}

	private void SubGraph_SaveLoadBool_Save_Out_125(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = e.boolean;
		local_EnemyAlive5_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_125;
		Relay_Save_Out_125();
	}

	private void SubGraph_SaveLoadBool_Load_Out_125(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = e.boolean;
		local_EnemyAlive5_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_125;
		Relay_Load_Out_125();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_125(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = e.boolean;
		local_EnemyAlive5_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_125;
		Relay_Restart_Out_125();
	}

	private void SubGraph_SaveLoadBool_Save_Out_147(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = e.boolean;
		local_RetryingTest_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_147;
		Relay_Save_Out_147();
	}

	private void SubGraph_SaveLoadBool_Load_Out_147(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = e.boolean;
		local_RetryingTest_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_147;
		Relay_Load_Out_147();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_147(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = e.boolean;
		local_RetryingTest_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_147;
		Relay_Restart_Out_147();
	}

	private void SubGraph_CompleteObjectiveStage_Out_196(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_196 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_196;
		Relay_Out_196();
	}

	private void SubGraph_SaveLoadBool_Save_Out_209(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_209 = e.boolean;
		local_ReturnedToNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_209;
		Relay_Save_Out_209();
	}

	private void SubGraph_SaveLoadBool_Load_Out_209(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_209 = e.boolean;
		local_ReturnedToNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_209;
		Relay_Load_Out_209();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_209(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_209 = e.boolean;
		local_ReturnedToNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_209;
		Relay_Restart_Out_209();
	}

	private void SubGraph_SaveLoadBool_Save_Out_238(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_238 = e.boolean;
		local_Rebriefed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_238;
		Relay_Save_Out_238();
	}

	private void SubGraph_SaveLoadBool_Load_Out_238(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_238 = e.boolean;
		local_Rebriefed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_238;
		Relay_Load_Out_238();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_238(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_238 = e.boolean;
		local_Rebriefed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_238;
		Relay_Restart_Out_238();
	}

	private void SubGraph_SaveLoadBool_Save_Out_251(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = e.boolean;
		local_FinishedJoke_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_251;
		Relay_Save_Out_251();
	}

	private void SubGraph_SaveLoadBool_Load_Out_251(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = e.boolean;
		local_FinishedJoke_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_251;
		Relay_Load_Out_251();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_251(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = e.boolean;
		local_FinishedJoke_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_251;
		Relay_Restart_Out_251();
	}

	private void SubGraph_SaveLoadBool_Save_Out_288(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = e.boolean;
		local_TestFailed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_288;
		Relay_Save_Out_288();
	}

	private void SubGraph_SaveLoadBool_Load_Out_288(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = e.boolean;
		local_TestFailed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_288;
		Relay_Load_Out_288();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_288(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = e.boolean;
		local_TestFailed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_288;
		Relay_Restart_Out_288();
	}

	private void SubGraph_SaveLoadBool_Save_Out_314(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_314 = e.boolean;
		local_OutOfBounds_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_314;
		Relay_Save_Out_314();
	}

	private void SubGraph_SaveLoadBool_Load_Out_314(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_314 = e.boolean;
		local_OutOfBounds_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_314;
		Relay_Load_Out_314();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_314(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_314 = e.boolean;
		local_OutOfBounds_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_314;
		Relay_Restart_Out_314();
	}

	private void SubGraph_SaveLoadBool_Save_Out_315(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = e.boolean;
		local_PlayerSwitchedTech_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_315;
		Relay_Save_Out_315();
	}

	private void SubGraph_SaveLoadBool_Load_Out_315(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = e.boolean;
		local_PlayerSwitchedTech_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_315;
		Relay_Load_Out_315();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_315(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = e.boolean;
		local_PlayerSwitchedTech_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_315;
		Relay_Restart_Out_315();
	}

	private void SubGraph_SaveLoadBool_Save_Out_316(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_316 = e.boolean;
		local_PlayerSpawnedTech_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_316;
		Relay_Save_Out_316();
	}

	private void SubGraph_SaveLoadBool_Load_Out_316(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_316 = e.boolean;
		local_PlayerSpawnedTech_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_316;
		Relay_Load_Out_316();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_316(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_316 = e.boolean;
		local_PlayerSpawnedTech_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_316;
		Relay_Restart_Out_316();
	}

	private void SubGraph_SaveLoadBool_Save_Out_328(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = e.boolean;
		local_TestAttempted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_328;
		Relay_Save_Out_328();
	}

	private void SubGraph_SaveLoadBool_Load_Out_328(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = e.boolean;
		local_TestAttempted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_328;
		Relay_Load_Out_328();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_328(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = e.boolean;
		local_TestAttempted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_328;
		Relay_Restart_Out_328();
	}

	private void uScriptCon_ManualSwitch_Output1_376(object o, EventArgs e)
	{
		Relay_Output1_376();
	}

	private void uScriptCon_ManualSwitch_Output2_376(object o, EventArgs e)
	{
		Relay_Output2_376();
	}

	private void uScriptCon_ManualSwitch_Output3_376(object o, EventArgs e)
	{
		Relay_Output3_376();
	}

	private void uScriptCon_ManualSwitch_Output4_376(object o, EventArgs e)
	{
		Relay_Output4_376();
	}

	private void uScriptCon_ManualSwitch_Output5_376(object o, EventArgs e)
	{
		Relay_Output5_376();
	}

	private void uScriptCon_ManualSwitch_Output6_376(object o, EventArgs e)
	{
		Relay_Output6_376();
	}

	private void uScriptCon_ManualSwitch_Output7_376(object o, EventArgs e)
	{
		Relay_Output7_376();
	}

	private void uScriptCon_ManualSwitch_Output8_376(object o, EventArgs e)
	{
		Relay_Output8_376();
	}

	private void uScriptCon_ManualSwitch_Output1_467(object o, EventArgs e)
	{
		Relay_Output1_467();
	}

	private void uScriptCon_ManualSwitch_Output2_467(object o, EventArgs e)
	{
		Relay_Output2_467();
	}

	private void uScriptCon_ManualSwitch_Output3_467(object o, EventArgs e)
	{
		Relay_Output3_467();
	}

	private void uScriptCon_ManualSwitch_Output4_467(object o, EventArgs e)
	{
		Relay_Output4_467();
	}

	private void uScriptCon_ManualSwitch_Output5_467(object o, EventArgs e)
	{
		Relay_Output5_467();
	}

	private void uScriptCon_ManualSwitch_Output6_467(object o, EventArgs e)
	{
		Relay_Output6_467();
	}

	private void uScriptCon_ManualSwitch_Output7_467(object o, EventArgs e)
	{
		Relay_Output7_467();
	}

	private void uScriptCon_ManualSwitch_Output8_467(object o, EventArgs e)
	{
		Relay_Output8_467();
	}

	private void uScriptCon_ManualSwitch_Output1_485(object o, EventArgs e)
	{
		Relay_Output1_485();
	}

	private void uScriptCon_ManualSwitch_Output2_485(object o, EventArgs e)
	{
		Relay_Output2_485();
	}

	private void uScriptCon_ManualSwitch_Output3_485(object o, EventArgs e)
	{
		Relay_Output3_485();
	}

	private void uScriptCon_ManualSwitch_Output4_485(object o, EventArgs e)
	{
		Relay_Output4_485();
	}

	private void uScriptCon_ManualSwitch_Output5_485(object o, EventArgs e)
	{
		Relay_Output5_485();
	}

	private void uScriptCon_ManualSwitch_Output6_485(object o, EventArgs e)
	{
		Relay_Output6_485();
	}

	private void uScriptCon_ManualSwitch_Output7_485(object o, EventArgs e)
	{
		Relay_Output7_485();
	}

	private void uScriptCon_ManualSwitch_Output8_485(object o, EventArgs e)
	{
		Relay_Output8_485();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_539(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_539 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_539 = e.messageControlPadReturn;
		local_MsgSwitchTech_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_539;
		local_MsgSwitchTech_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_539;
		Relay_Out_539();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_539(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_539 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_539 = e.messageControlPadReturn;
		local_MsgSwitchTech_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_539;
		local_MsgSwitchTech_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_539;
		Relay_Shown_539();
	}

	private void SubGraph_CompleteObjectiveStage_Out_571(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_571 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_571;
		Relay_Out_571();
	}

	private void uScriptCon_ManualSwitch_Output1_574(object o, EventArgs e)
	{
		Relay_Output1_574();
	}

	private void uScriptCon_ManualSwitch_Output2_574(object o, EventArgs e)
	{
		Relay_Output2_574();
	}

	private void uScriptCon_ManualSwitch_Output3_574(object o, EventArgs e)
	{
		Relay_Output3_574();
	}

	private void uScriptCon_ManualSwitch_Output4_574(object o, EventArgs e)
	{
		Relay_Output4_574();
	}

	private void uScriptCon_ManualSwitch_Output5_574(object o, EventArgs e)
	{
		Relay_Output5_574();
	}

	private void uScriptCon_ManualSwitch_Output6_574(object o, EventArgs e)
	{
		Relay_Output6_574();
	}

	private void uScriptCon_ManualSwitch_Output7_574(object o, EventArgs e)
	{
		Relay_Output7_574();
	}

	private void uScriptCon_ManualSwitch_Output8_574(object o, EventArgs e)
	{
		Relay_Output8_574();
	}

	private void uScriptCon_ManualSwitch_Output1_604(object o, EventArgs e)
	{
		Relay_Output1_604();
	}

	private void uScriptCon_ManualSwitch_Output2_604(object o, EventArgs e)
	{
		Relay_Output2_604();
	}

	private void uScriptCon_ManualSwitch_Output3_604(object o, EventArgs e)
	{
		Relay_Output3_604();
	}

	private void uScriptCon_ManualSwitch_Output4_604(object o, EventArgs e)
	{
		Relay_Output4_604();
	}

	private void uScriptCon_ManualSwitch_Output5_604(object o, EventArgs e)
	{
		Relay_Output5_604();
	}

	private void uScriptCon_ManualSwitch_Output6_604(object o, EventArgs e)
	{
		Relay_Output6_604();
	}

	private void uScriptCon_ManualSwitch_Output7_604(object o, EventArgs e)
	{
		Relay_Output7_604();
	}

	private void uScriptCon_ManualSwitch_Output8_604(object o, EventArgs e)
	{
		Relay_Output8_604();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_622(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_622 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_622 = e.messageControlPadReturn;
		local_MsgPurchaseVehicle_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_622;
		local_MsgPurchaseVehicle_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_622;
		Relay_Out_622();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_622(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_622 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_622 = e.messageControlPadReturn;
		local_MsgPurchaseVehicle_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_622;
		local_MsgPurchaseVehicle_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_622;
		Relay_Shown_622();
	}

	private void SubGraph_CompleteObjectiveStage_Out_629(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_629 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_629;
		Relay_Out_629();
	}

	private void SubGraph_SaveLoadBool_Save_Out_661(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_661 = e.boolean;
		local_SwitchedTech_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_661;
		Relay_Save_Out_661();
	}

	private void SubGraph_SaveLoadBool_Load_Out_661(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_661 = e.boolean;
		local_SwitchedTech_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_661;
		Relay_Load_Out_661();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_661(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_661 = e.boolean;
		local_SwitchedTech_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_661;
		Relay_Restart_Out_661();
	}

	private void SubGraph_SaveLoadBool_Save_Out_662(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_662 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_662;
		Relay_Save_Out_662();
	}

	private void SubGraph_SaveLoadBool_Load_Out_662(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_662 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_662;
		Relay_Load_Out_662();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_662(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_662 = e.boolean;
		local_VehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_662;
		Relay_Restart_Out_662();
	}

	private void SubGraph_SaveLoadBool_Save_Out_663(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_663 = e.boolean;
		local_VehicleSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_663;
		Relay_Save_Out_663();
	}

	private void SubGraph_SaveLoadBool_Load_Out_663(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_663 = e.boolean;
		local_VehicleSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_663;
		Relay_Load_Out_663();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_663(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_663 = e.boolean;
		local_VehicleSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_663;
		Relay_Restart_Out_663();
	}

	private void SubGraph_SaveLoadBool_Save_Out_664(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = e.boolean;
		local_msg03aShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_664;
		Relay_Save_Out_664();
	}

	private void SubGraph_SaveLoadBool_Load_Out_664(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = e.boolean;
		local_msg03aShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_664;
		Relay_Load_Out_664();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_664(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = e.boolean;
		local_msg03aShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_664;
		Relay_Restart_Out_664();
	}

	private void SubGraph_SaveLoadBool_Save_Out_665(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = e.boolean;
		local_msg03bShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_665;
		Relay_Save_Out_665();
	}

	private void SubGraph_SaveLoadBool_Load_Out_665(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = e.boolean;
		local_msg03bShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_665;
		Relay_Load_Out_665();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_665(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = e.boolean;
		local_msg03bShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_665;
		Relay_Restart_Out_665();
	}

	private void SubGraph_SaveLoadBool_Save_Out_666(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_666 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_666;
		Relay_Save_Out_666();
	}

	private void SubGraph_SaveLoadBool_Load_Out_666(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_666 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_666;
		Relay_Load_Out_666();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_666(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_666 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_666;
		Relay_Restart_Out_666();
	}

	private void SubGraph_SaveLoadBool_Save_Out_667(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_667 = e.boolean;
		local_WaitingOnPrompt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_667;
		Relay_Save_Out_667();
	}

	private void SubGraph_SaveLoadBool_Load_Out_667(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_667 = e.boolean;
		local_WaitingOnPrompt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_667;
		Relay_Load_Out_667();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_667(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_667 = e.boolean;
		local_WaitingOnPrompt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_667;
		Relay_Restart_Out_667();
	}

	private void SubGraph_SaveLoadBool_Save_Out_668(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_668 = e.boolean;
		local_SaidMsgNPCVehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_668;
		Relay_Save_Out_668();
	}

	private void SubGraph_SaveLoadBool_Load_Out_668(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_668 = e.boolean;
		local_SaidMsgNPCVehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_668;
		Relay_Load_Out_668();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_668(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_668 = e.boolean;
		local_SaidMsgNPCVehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_668;
		Relay_Restart_Out_668();
	}

	private void SubGraph_SaveLoadBool_Save_Out_669(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_669 = e.boolean;
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_669;
		Relay_Save_Out_669();
	}

	private void SubGraph_SaveLoadBool_Load_Out_669(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_669 = e.boolean;
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_669;
		Relay_Load_Out_669();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_669(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_669 = e.boolean;
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_669;
		Relay_Restart_Out_669();
	}

	private void SubGraph_SaveLoadBool_Save_Out_670(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = e.boolean;
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_670;
		Relay_Save_Out_670();
	}

	private void SubGraph_SaveLoadBool_Load_Out_670(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = e.boolean;
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_670;
		Relay_Load_Out_670();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_670(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = e.boolean;
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_670;
		Relay_Restart_Out_670();
	}

	private void SubGraph_SaveLoadBool_Save_Out_692(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_692 = e.boolean;
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_692;
		Relay_Save_Out_692();
	}

	private void SubGraph_SaveLoadBool_Load_Out_692(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_692 = e.boolean;
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_692;
		Relay_Load_Out_692();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_692(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_692 = e.boolean;
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_692;
		Relay_Restart_Out_692();
	}

	private void SubGraph_SaveLoadBool_Save_Out_694(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_694 = e.boolean;
		local_NewWeaponAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_694;
		Relay_Save_Out_694();
	}

	private void SubGraph_SaveLoadBool_Load_Out_694(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_694 = e.boolean;
		local_NewWeaponAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_694;
		Relay_Load_Out_694();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_694(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_694 = e.boolean;
		local_NewWeaponAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_694;
		Relay_Restart_Out_694();
	}

	private void SubGraph_SaveLoadBool_Save_Out_740(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_740 = e.boolean;
		local_BlockLimitCritical_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_740;
		Relay_Save_Out_740();
	}

	private void SubGraph_SaveLoadBool_Load_Out_740(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_740 = e.boolean;
		local_BlockLimitCritical_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_740;
		Relay_Load_Out_740();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_740(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_740 = e.boolean;
		local_BlockLimitCritical_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_740;
		Relay_Restart_Out_740();
	}

	private void SubGraph_LoadObjectiveStates_Out_746(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_746();
	}

	private void SubGraph_SaveLoadInt_Save_Out_752(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_752 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_752;
		Relay_Save_Out_752();
	}

	private void SubGraph_SaveLoadInt_Load_Out_752(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_752 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_752;
		Relay_Load_Out_752();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_752(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_752 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_752;
		Relay_Restart_Out_752();
	}

	private void SubGraph_SaveLoadBool_Save_Out_857(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_857 = e.boolean;
		local_msgRebuyShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_857;
		Relay_Save_Out_857();
	}

	private void SubGraph_SaveLoadBool_Load_Out_857(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_857 = e.boolean;
		local_msgRebuyShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_857;
		Relay_Load_Out_857();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_857(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_857 = e.boolean;
		local_msgRebuyShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_857;
		Relay_Restart_Out_857();
	}

	private void SubGraph_SaveLoadBool_Save_Out_906(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_906 = e.boolean;
		local_AdditionalVehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_906;
		Relay_Save_Out_906();
	}

	private void SubGraph_SaveLoadBool_Load_Out_906(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_906 = e.boolean;
		local_AdditionalVehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_906;
		Relay_Load_Out_906();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_906(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_906 = e.boolean;
		local_AdditionalVehiclePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_906;
		Relay_Restart_Out_906();
	}

	private void SubGraph_SaveLoadBool_Save_Out_1033(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_1033 = e.boolean;
		local_msgAllPlayersMustBeInShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_1033;
		Relay_Save_Out_1033();
	}

	private void SubGraph_SaveLoadBool_Load_Out_1033(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_1033 = e.boolean;
		local_msgAllPlayersMustBeInShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_1033;
		Relay_Load_Out_1033();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_1033(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_1033 = e.boolean;
		local_msgAllPlayersMustBeInShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_1033;
		Relay_Restart_Out_1033();
	}

	private void SubGraph_SaveLoadBool_Save_Out_1045(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_1045 = e.boolean;
		local_PlayerDetachedWeapon_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_1045;
		Relay_Save_Out_1045();
	}

	private void SubGraph_SaveLoadBool_Load_Out_1045(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_1045 = e.boolean;
		local_PlayerDetachedWeapon_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_1045;
		Relay_Load_Out_1045();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_1045(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_1045 = e.boolean;
		local_PlayerDetachedWeapon_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_1045;
		Relay_Restart_Out_1045();
	}

	private void SubGraph_SaveLoadBool_Save_Out_1059(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_1059 = e.boolean;
		local_BuyMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_1059;
		Relay_Save_Out_1059();
	}

	private void SubGraph_SaveLoadBool_Load_Out_1059(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_1059 = e.boolean;
		local_BuyMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_1059;
		Relay_Load_Out_1059();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_1059(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_1059 = e.boolean;
		local_BuyMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_1059;
		Relay_Restart_Out_1059();
	}

	private void Relay_True_1()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1.True(out logic_uScriptAct_SetBool_Target_1);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_1;
	}

	private void Relay_False_1()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1.False(out logic_uScriptAct_SetBool_Target_1);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_1;
	}

	private void Relay_Save_Out_2()
	{
		Relay_Save_12();
	}

	private void Relay_Load_Out_2()
	{
		Relay_Load_12();
	}

	private void Relay_Restart_Out_2()
	{
		Relay_Restart_12();
	}

	private void Relay_Save_2()
	{
		logic_SubGraph_SaveLoadBool_boolean_2 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_2 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Save(ref logic_SubGraph_SaveLoadBool_boolean_2, logic_SubGraph_SaveLoadBool_boolAsVariable_2, logic_SubGraph_SaveLoadBool_uniqueID_2);
	}

	private void Relay_Load_2()
	{
		logic_SubGraph_SaveLoadBool_boolean_2 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_2 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Load(ref logic_SubGraph_SaveLoadBool_boolean_2, logic_SubGraph_SaveLoadBool_boolAsVariable_2, logic_SubGraph_SaveLoadBool_uniqueID_2);
	}

	private void Relay_Set_True_2()
	{
		logic_SubGraph_SaveLoadBool_boolean_2 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_2 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_2, logic_SubGraph_SaveLoadBool_boolAsVariable_2, logic_SubGraph_SaveLoadBool_uniqueID_2);
	}

	private void Relay_Set_False_2()
	{
		logic_SubGraph_SaveLoadBool_boolean_2 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_2 = local_ObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_2.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_2, logic_SubGraph_SaveLoadBool_boolAsVariable_2, logic_SubGraph_SaveLoadBool_uniqueID_2);
	}

	private void Relay_SaveEvent_4()
	{
		Relay_Save_2();
	}

	private void Relay_LoadEvent_4()
	{
		Relay_Load_2();
	}

	private void Relay_RestartEvent_4()
	{
		Relay_Set_False_2();
	}

	private void Relay_OnUpdate_5()
	{
		Relay_In_272();
	}

	private void Relay_OnSuspend_5()
	{
	}

	private void Relay_OnResume_5()
	{
	}

	private void Relay_In_7()
	{
		logic_uScriptCon_CompareBool_Bool_7 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.In(logic_uScriptCon_CompareBool_Bool_7);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.False;
		if (num)
		{
			Relay_In_48();
		}
		if (flag)
		{
			Relay_In_1079();
		}
	}

	private void Relay_Succeed_11()
	{
		logic_uScript_FinishEncounter_owner_11 = owner_Connection_9;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_11.Succeed(logic_uScript_FinishEncounter_owner_11);
	}

	private void Relay_Fail_11()
	{
		logic_uScript_FinishEncounter_owner_11 = owner_Connection_9;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_11.Fail(logic_uScript_FinishEncounter_owner_11);
	}

	private void Relay_Save_Out_12()
	{
		Relay_Save_40();
	}

	private void Relay_Load_Out_12()
	{
		Relay_Load_40();
	}

	private void Relay_Restart_Out_12()
	{
		Relay_Set_False_40();
	}

	private void Relay_Save_12()
	{
		logic_SubGraph_SaveLoadInt_restartValue_12 = local_StartValue_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_12 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_12 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Save(logic_SubGraph_SaveLoadInt_restartValue_12, ref logic_SubGraph_SaveLoadInt_integer_12, logic_SubGraph_SaveLoadInt_intAsVariable_12, logic_SubGraph_SaveLoadInt_uniqueID_12);
	}

	private void Relay_Load_12()
	{
		logic_SubGraph_SaveLoadInt_restartValue_12 = local_StartValue_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_12 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_12 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Load(logic_SubGraph_SaveLoadInt_restartValue_12, ref logic_SubGraph_SaveLoadInt_integer_12, logic_SubGraph_SaveLoadInt_intAsVariable_12, logic_SubGraph_SaveLoadInt_uniqueID_12);
	}

	private void Relay_Restart_12()
	{
		logic_SubGraph_SaveLoadInt_restartValue_12 = local_StartValue_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_12 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_12 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_12.Restart(logic_SubGraph_SaveLoadInt_restartValue_12, ref logic_SubGraph_SaveLoadInt_integer_12, logic_SubGraph_SaveLoadInt_intAsVariable_12, logic_SubGraph_SaveLoadInt_uniqueID_12);
	}

	private void Relay_In_13()
	{
		int num = 0;
		Array array = msgMissionComplete;
		if (logic_uScript_AddOnScreenMessage_locString_13.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_13, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_13, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_13 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_13 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13.In(logic_uScript_AddOnScreenMessage_locString_13, logic_uScript_AddOnScreenMessage_msgPriority_13, logic_uScript_AddOnScreenMessage_holdMsg_13, logic_uScript_AddOnScreenMessage_tag_13, logic_uScript_AddOnScreenMessage_speaker_13, logic_uScript_AddOnScreenMessage_side_13);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_13.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_In_18()
	{
		logic_uScriptCon_CompareBool_Bool_18 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.In(logic_uScriptCon_CompareBool_Bool_18);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.False;
		if (num)
		{
			Relay_In_187();
		}
		if (flag)
		{
			Relay_In_7();
		}
	}

	private void Relay_InitialSpawn_20()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_20.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_20, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_20, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_20 = owner_Connection_22;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_20.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_20, logic_uScript_SpawnTechsFromData_ownerNode_20, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_20, logic_uScript_SpawnTechsFromData_allowResurrection_20);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_20.Out)
		{
			Relay_True_1();
		}
	}

	private void Relay_In_25()
	{
		int num = 0;
		Array array = msgNPCGreeting;
		if (logic_uScript_AddOnScreenMessage_locString_25.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_25, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_25, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_25 = local_54_System_String;
		logic_uScript_AddOnScreenMessage_speaker_25 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_25 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25.In(logic_uScript_AddOnScreenMessage_locString_25, logic_uScript_AddOnScreenMessage_msgPriority_25, logic_uScript_AddOnScreenMessage_holdMsg_25, logic_uScript_AddOnScreenMessage_tag_25, logic_uScript_AddOnScreenMessage_speaker_25, logic_uScript_AddOnScreenMessage_side_25);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25.Shown)
		{
			Relay_In_226();
		}
	}

	private void Relay_Out_27()
	{
	}

	private void Relay_In_27()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_27 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_27.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_27, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_27);
	}

	private void Relay_True_28()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.True(out logic_uScriptAct_SetBool_Target_28);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_28;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_28.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_False_28()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.False(out logic_uScriptAct_SetBool_Target_28);
		local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_28;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_28.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_31()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_In_32()
	{
		int num = 0;
		Array array = local_MobTechs1_TankArray;
		if (logic_uScript_DamageTechs_techs_32.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_32, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_32, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_32.In(logic_uScript_DamageTechs_techs_32, logic_uScript_DamageTechs_dmgPercent_32, logic_uScript_DamageTechs_givePlyrCredit_32, logic_uScript_DamageTechs_leaveBlksPercent_32, logic_uScript_DamageTechs_makeVulnerable_32);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_32.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_In_36()
	{
		logic_uScript_ClearEncounterTarget_owner_36 = owner_Connection_37;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_36.In(logic_uScript_ClearEncounterTarget_owner_36);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_36.Out)
		{
			Relay_In_152();
		}
	}

	private void Relay_Save_Out_40()
	{
		Relay_Save_53();
	}

	private void Relay_Load_Out_40()
	{
		Relay_Load_53();
	}

	private void Relay_Restart_Out_40()
	{
		Relay_Set_False_53();
	}

	private void Relay_Save_40()
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_40 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Save(ref logic_SubGraph_SaveLoadBool_boolean_40, logic_SubGraph_SaveLoadBool_boolAsVariable_40, logic_SubGraph_SaveLoadBool_uniqueID_40);
	}

	private void Relay_Load_40()
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_40 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Load(ref logic_SubGraph_SaveLoadBool_boolean_40, logic_SubGraph_SaveLoadBool_boolAsVariable_40, logic_SubGraph_SaveLoadBool_uniqueID_40);
	}

	private void Relay_Set_True_40()
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_40 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_40, logic_SubGraph_SaveLoadBool_boolAsVariable_40, logic_SubGraph_SaveLoadBool_uniqueID_40);
	}

	private void Relay_Set_False_40()
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_40 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_40, logic_SubGraph_SaveLoadBool_boolAsVariable_40, logic_SubGraph_SaveLoadBool_uniqueID_40);
	}

	private void Relay_In_41()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_41.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_41, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_41, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_41 = owner_Connection_45;
		int num2 = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_41.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_41, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_41, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_41 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.In(logic_uScript_GetAndCheckTechs_techData_41, logic_uScript_GetAndCheckTechs_ownerNode_41, ref logic_uScript_GetAndCheckTechs_techs_41);
		local_NPCTanks_TankArray = logic_uScript_GetAndCheckTechs_techs_41;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_44();
		}
		if (someAlive)
		{
			Relay_AtIndex_44();
		}
	}

	private void Relay_In_43()
	{
		logic_uScript_SetTankInvulnerable_tank_43 = local_NPCTank_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_43.In(logic_uScript_SetTankInvulnerable_invulnerable_43, logic_uScript_SetTankInvulnerable_tank_43);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_43.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_AtIndex_44()
	{
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_AccessListTech_techList_44.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_44, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_44, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_44.AtIndex(ref logic_uScript_AccessListTech_techList_44, logic_uScript_AccessListTech_index_44, out logic_uScript_AccessListTech_value_44);
		local_NPCTanks_TankArray = logic_uScript_AccessListTech_techList_44;
		local_NPCTank_Tank = logic_uScript_AccessListTech_value_44;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_44.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_48()
	{
		logic_uScriptCon_CompareBool_Bool_48 = local_TechsSetup_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.In(logic_uScriptCon_CompareBool_Bool_48);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.False;
		if (num)
		{
			Relay_In_59();
		}
		if (flag)
		{
			Relay_In_41();
		}
	}

	private void Relay_True_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.True(out logic_uScriptAct_SetBool_Target_50);
		local_TechsSetup_System_Boolean = logic_uScriptAct_SetBool_Target_50;
	}

	private void Relay_False_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.False(out logic_uScriptAct_SetBool_Target_50);
		local_TechsSetup_System_Boolean = logic_uScriptAct_SetBool_Target_50;
	}

	private void Relay_Save_Out_53()
	{
		Relay_Save_65();
	}

	private void Relay_Load_Out_53()
	{
		Relay_Load_65();
	}

	private void Relay_Restart_Out_53()
	{
		Relay_Set_False_65();
	}

	private void Relay_Save_53()
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_53 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Save(ref logic_SubGraph_SaveLoadBool_boolean_53, logic_SubGraph_SaveLoadBool_boolAsVariable_53, logic_SubGraph_SaveLoadBool_uniqueID_53);
	}

	private void Relay_Load_53()
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_53 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Load(ref logic_SubGraph_SaveLoadBool_boolean_53, logic_SubGraph_SaveLoadBool_boolAsVariable_53, logic_SubGraph_SaveLoadBool_uniqueID_53);
	}

	private void Relay_Set_True_53()
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_53 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_53, logic_SubGraph_SaveLoadBool_boolAsVariable_53, logic_SubGraph_SaveLoadBool_uniqueID_53);
	}

	private void Relay_Set_False_53()
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_53 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_53, logic_SubGraph_SaveLoadBool_boolAsVariable_53, logic_SubGraph_SaveLoadBool_uniqueID_53);
	}

	private void Relay_In_55()
	{
		logic_uScriptCon_CompareBool_Bool_55 = local_TestStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.In(logic_uScriptCon_CompareBool_Bool_55);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.False;
		if (num)
		{
			Relay_In_98();
		}
		if (flag)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_57()
	{
		logic_uScriptCon_CompareBool_Bool_57 = local_TestStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.In(logic_uScriptCon_CompareBool_Bool_57);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_57.False;
		if (num)
		{
			Relay_In_916();
		}
		if (flag)
		{
			Relay_In_1041();
		}
	}

	private void Relay_In_59()
	{
		logic_uScriptCon_CompareBool_Bool_59 = local_TestComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.In(logic_uScriptCon_CompareBool_Bool_59);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.False;
		if (num)
		{
			Relay_True_63();
		}
		if (flag)
		{
			Relay_In_158();
		}
	}

	private void Relay_Out_61()
	{
		Relay_In_116();
	}

	private void Relay_In_61()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_61 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_61.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_61, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_61);
	}

	private void Relay_True_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.True(out logic_uScriptAct_SetBool_Target_63);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_63;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_63.Out)
		{
			Relay_In_701();
		}
	}

	private void Relay_False_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.False(out logic_uScriptAct_SetBool_Target_63);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_63;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_63.Out)
		{
			Relay_In_701();
		}
	}

	private void Relay_Save_Out_65()
	{
		Relay_Save_68();
	}

	private void Relay_Load_Out_65()
	{
		Relay_Load_68();
	}

	private void Relay_Restart_Out_65()
	{
		Relay_Set_False_68();
	}

	private void Relay_Save_65()
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = local_TestComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_65 = local_TestComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Save(ref logic_SubGraph_SaveLoadBool_boolean_65, logic_SubGraph_SaveLoadBool_boolAsVariable_65, logic_SubGraph_SaveLoadBool_uniqueID_65);
	}

	private void Relay_Load_65()
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = local_TestComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_65 = local_TestComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Load(ref logic_SubGraph_SaveLoadBool_boolean_65, logic_SubGraph_SaveLoadBool_boolAsVariable_65, logic_SubGraph_SaveLoadBool_uniqueID_65);
	}

	private void Relay_Set_True_65()
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = local_TestComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_65 = local_TestComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_65, logic_SubGraph_SaveLoadBool_boolAsVariable_65, logic_SubGraph_SaveLoadBool_uniqueID_65);
	}

	private void Relay_Set_False_65()
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = local_TestComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_65 = local_TestComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_65, logic_SubGraph_SaveLoadBool_boolAsVariable_65, logic_SubGraph_SaveLoadBool_uniqueID_65);
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
		logic_SubGraph_SaveLoadBool_boolean_68 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_68 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Save(ref logic_SubGraph_SaveLoadBool_boolean_68, logic_SubGraph_SaveLoadBool_boolAsVariable_68, logic_SubGraph_SaveLoadBool_uniqueID_68);
	}

	private void Relay_Load_68()
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_68 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Load(ref logic_SubGraph_SaveLoadBool_boolean_68, logic_SubGraph_SaveLoadBool_boolAsVariable_68, logic_SubGraph_SaveLoadBool_uniqueID_68);
	}

	private void Relay_Set_True_68()
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_68 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_68, logic_SubGraph_SaveLoadBool_boolAsVariable_68, logic_SubGraph_SaveLoadBool_uniqueID_68);
	}

	private void Relay_Set_False_68()
	{
		logic_SubGraph_SaveLoadBool_boolean_68 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_68 = local_NPCMet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_68.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_68, logic_SubGraph_SaveLoadBool_boolAsVariable_68, logic_SubGraph_SaveLoadBool_uniqueID_68);
	}

	private void Relay_Save_Out_69()
	{
		Relay_Save_89();
	}

	private void Relay_Load_Out_69()
	{
		Relay_Load_89();
	}

	private void Relay_Restart_Out_69()
	{
		Relay_Set_False_89();
	}

	private void Relay_Save_69()
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = local_TestStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_69 = local_TestStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Save(ref logic_SubGraph_SaveLoadBool_boolean_69, logic_SubGraph_SaveLoadBool_boolAsVariable_69, logic_SubGraph_SaveLoadBool_uniqueID_69);
	}

	private void Relay_Load_69()
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = local_TestStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_69 = local_TestStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Load(ref logic_SubGraph_SaveLoadBool_boolean_69, logic_SubGraph_SaveLoadBool_boolAsVariable_69, logic_SubGraph_SaveLoadBool_uniqueID_69);
	}

	private void Relay_Set_True_69()
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = local_TestStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_69 = local_TestStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_69, logic_SubGraph_SaveLoadBool_boolAsVariable_69, logic_SubGraph_SaveLoadBool_uniqueID_69);
	}

	private void Relay_Set_False_69()
	{
		logic_SubGraph_SaveLoadBool_boolean_69 = local_TestStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_69 = local_TestStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_69.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_69, logic_SubGraph_SaveLoadBool_boolAsVariable_69, logic_SubGraph_SaveLoadBool_uniqueID_69);
	}

	private void Relay_In_71()
	{
		int num = 0;
		Array array = local_MobTechs2_TankArray;
		if (logic_uScript_DamageTechs_techs_71.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_71, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_71, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_71.In(logic_uScript_DamageTechs_techs_71, logic_uScript_DamageTechs_dmgPercent_71, logic_uScript_DamageTechs_givePlyrCredit_71, logic_uScript_DamageTechs_leaveBlksPercent_71, logic_uScript_DamageTechs_makeVulnerable_71);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_71.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_74()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_74.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_74.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_76()
	{
		int num = 0;
		Array array = local_MobTechs3_TankArray;
		if (logic_uScript_DamageTechs_techs_76.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_76, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_76, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_76.In(logic_uScript_DamageTechs_techs_76, logic_uScript_DamageTechs_dmgPercent_76, logic_uScript_DamageTechs_givePlyrCredit_76, logic_uScript_DamageTechs_leaveBlksPercent_76, logic_uScript_DamageTechs_makeVulnerable_76);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_76.Out)
		{
			Relay_In_184();
		}
	}

	private void Relay_In_79()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_79.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_79.Out)
		{
			Relay_In_184();
		}
	}

	private void Relay_In_81()
	{
		logic_uScriptCon_CompareBool_Bool_81 = local_EnemyAlive1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.In(logic_uScriptCon_CompareBool_Bool_81);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.False;
		if (num)
		{
			Relay_In_357();
		}
		if (flag)
		{
			Relay_InitialSpawn_92();
		}
	}

	private void Relay_In_84()
	{
		logic_uScriptCon_CompareBool_Bool_84 = local_EnemyAlive2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.In(logic_uScriptCon_CompareBool_Bool_84);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.False;
		if (num)
		{
			Relay_In_353();
		}
		if (flag)
		{
			Relay_InitialSpawn_94();
		}
	}

	private void Relay_In_87()
	{
		logic_uScriptCon_CompareBool_Bool_87 = local_EnemyAlive3_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.In(logic_uScriptCon_CompareBool_Bool_87);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.False;
		if (num)
		{
			Relay_In_347();
		}
		if (flag)
		{
			Relay_InitialSpawn_97();
		}
	}

	private void Relay_Save_Out_89()
	{
		Relay_Save_90();
	}

	private void Relay_Load_Out_89()
	{
		Relay_Load_90();
	}

	private void Relay_Restart_Out_89()
	{
		Relay_Set_False_90();
	}

	private void Relay_Save_89()
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = local_EnemyAlive1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_89 = local_EnemyAlive1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Save(ref logic_SubGraph_SaveLoadBool_boolean_89, logic_SubGraph_SaveLoadBool_boolAsVariable_89, logic_SubGraph_SaveLoadBool_uniqueID_89);
	}

	private void Relay_Load_89()
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = local_EnemyAlive1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_89 = local_EnemyAlive1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Load(ref logic_SubGraph_SaveLoadBool_boolean_89, logic_SubGraph_SaveLoadBool_boolAsVariable_89, logic_SubGraph_SaveLoadBool_uniqueID_89);
	}

	private void Relay_Set_True_89()
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = local_EnemyAlive1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_89 = local_EnemyAlive1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_89, logic_SubGraph_SaveLoadBool_boolAsVariable_89, logic_SubGraph_SaveLoadBool_uniqueID_89);
	}

	private void Relay_Set_False_89()
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = local_EnemyAlive1_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_89 = local_EnemyAlive1_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_89, logic_SubGraph_SaveLoadBool_boolAsVariable_89, logic_SubGraph_SaveLoadBool_uniqueID_89);
	}

	private void Relay_Save_Out_90()
	{
		Relay_Save_91();
	}

	private void Relay_Load_Out_90()
	{
		Relay_Load_91();
	}

	private void Relay_Restart_Out_90()
	{
		Relay_Set_False_91();
	}

	private void Relay_Save_90()
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = local_EnemyAlive2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_90 = local_EnemyAlive2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Save(ref logic_SubGraph_SaveLoadBool_boolean_90, logic_SubGraph_SaveLoadBool_boolAsVariable_90, logic_SubGraph_SaveLoadBool_uniqueID_90);
	}

	private void Relay_Load_90()
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = local_EnemyAlive2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_90 = local_EnemyAlive2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Load(ref logic_SubGraph_SaveLoadBool_boolean_90, logic_SubGraph_SaveLoadBool_boolAsVariable_90, logic_SubGraph_SaveLoadBool_uniqueID_90);
	}

	private void Relay_Set_True_90()
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = local_EnemyAlive2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_90 = local_EnemyAlive2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_90, logic_SubGraph_SaveLoadBool_boolAsVariable_90, logic_SubGraph_SaveLoadBool_uniqueID_90);
	}

	private void Relay_Set_False_90()
	{
		logic_SubGraph_SaveLoadBool_boolean_90 = local_EnemyAlive2_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_90 = local_EnemyAlive2_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_90.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_90, logic_SubGraph_SaveLoadBool_boolAsVariable_90, logic_SubGraph_SaveLoadBool_uniqueID_90);
	}

	private void Relay_Save_Out_91()
	{
		Relay_Save_124();
	}

	private void Relay_Load_Out_91()
	{
		Relay_Load_124();
	}

	private void Relay_Restart_Out_91()
	{
		Relay_Set_False_124();
	}

	private void Relay_Save_91()
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = local_EnemyAlive3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_91 = local_EnemyAlive3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Save(ref logic_SubGraph_SaveLoadBool_boolean_91, logic_SubGraph_SaveLoadBool_boolAsVariable_91, logic_SubGraph_SaveLoadBool_uniqueID_91);
	}

	private void Relay_Load_91()
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = local_EnemyAlive3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_91 = local_EnemyAlive3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Load(ref logic_SubGraph_SaveLoadBool_boolean_91, logic_SubGraph_SaveLoadBool_boolAsVariable_91, logic_SubGraph_SaveLoadBool_uniqueID_91);
	}

	private void Relay_Set_True_91()
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = local_EnemyAlive3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_91 = local_EnemyAlive3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_91, logic_SubGraph_SaveLoadBool_boolAsVariable_91, logic_SubGraph_SaveLoadBool_uniqueID_91);
	}

	private void Relay_Set_False_91()
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = local_EnemyAlive3_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_91 = local_EnemyAlive3_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_91, logic_SubGraph_SaveLoadBool_boolAsVariable_91, logic_SubGraph_SaveLoadBool_uniqueID_91);
	}

	private void Relay_InitialSpawn_92()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_92.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_92, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_92[num++] = enemyGroup1Data;
		logic_uScript_SpawnTechsFromData_ownerNode_92 = owner_Connection_93;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_92.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_92, logic_uScript_SpawnTechsFromData_ownerNode_92, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_92, logic_uScript_SpawnTechsFromData_allowResurrection_92);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_92.Out)
		{
			Relay_True_112();
		}
	}

	private void Relay_InitialSpawn_94()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_94.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_94, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_94[num++] = enemyGroup2Data;
		logic_uScript_SpawnTechsFromData_ownerNode_94 = owner_Connection_95;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_94.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_94, logic_uScript_SpawnTechsFromData_ownerNode_94, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_94, logic_uScript_SpawnTechsFromData_allowResurrection_94);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_94.Out)
		{
			Relay_True_111();
		}
	}

	private void Relay_InitialSpawn_97()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_97.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_97, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_97[num++] = enemyGroup3Data;
		logic_uScript_SpawnTechsFromData_ownerNode_97 = owner_Connection_96;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_97.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_97, logic_uScript_SpawnTechsFromData_ownerNode_97, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_97, logic_uScript_SpawnTechsFromData_allowResurrection_97);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_97.Out)
		{
			Relay_True_110();
		}
	}

	private void Relay_In_98()
	{
		logic_uScript_GetMissionTimerDisplayTime_owner_98 = owner_Connection_103;
		logic_uScript_GetMissionTimerDisplayTime_Return_98 = logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_98.In(logic_uScript_GetMissionTimerDisplayTime_owner_98);
		local_102_System_Single = logic_uScript_GetMissionTimerDisplayTime_Return_98;
		if (logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_98.Out)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_100()
	{
		logic_uScriptCon_CompareFloat_A_100 = local_102_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_100.In(logic_uScriptCon_CompareFloat_A_100, logic_uScriptCon_CompareFloat_B_100);
		bool greaterThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_100.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_100.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_924();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_101();
		}
	}

	private void Relay_In_101()
	{
		logic_uScript_StopMissionTimer_owner_101 = owner_Connection_99;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_101.In(logic_uScript_StopMissionTimer_owner_101);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_101.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_In_104()
	{
		logic_uScript_HideMissionTimerUI_owner_104 = owner_Connection_860;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_104.In(logic_uScript_HideMissionTimerUI_owner_104);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_104.Out)
		{
			Relay_In_136();
		}
	}

	private void Relay_True_106()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_106.True(out logic_uScriptAct_SetBool_Target_106);
		local_TestComplete_System_Boolean = logic_uScriptAct_SetBool_Target_106;
	}

	private void Relay_False_106()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_106.False(out logic_uScriptAct_SetBool_Target_106);
		local_TestComplete_System_Boolean = logic_uScriptAct_SetBool_Target_106;
	}

	private void Relay_In_107()
	{
		logic_uScript_StopMissionTimer_owner_107 = owner_Connection_109;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_107.In(logic_uScript_StopMissionTimer_owner_107);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_107.Out)
		{
			Relay_In_133();
		}
	}

	private void Relay_In_108()
	{
		logic_uScript_HideMissionTimerUI_owner_108 = owner_Connection_859;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_108.In(logic_uScript_HideMissionTimerUI_owner_108);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_108.Out)
		{
			Relay_In_280();
		}
	}

	private void Relay_True_110()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_110.True(out logic_uScriptAct_SetBool_Target_110);
		local_EnemyAlive3_System_Boolean = logic_uScriptAct_SetBool_Target_110;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_110.Out)
		{
			Relay_In_347();
		}
	}

	private void Relay_False_110()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_110.False(out logic_uScriptAct_SetBool_Target_110);
		local_EnemyAlive3_System_Boolean = logic_uScriptAct_SetBool_Target_110;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_110.Out)
		{
			Relay_In_347();
		}
	}

	private void Relay_True_111()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_111.True(out logic_uScriptAct_SetBool_Target_111);
		local_EnemyAlive2_System_Boolean = logic_uScriptAct_SetBool_Target_111;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_111.Out)
		{
			Relay_In_353();
		}
	}

	private void Relay_False_111()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_111.False(out logic_uScriptAct_SetBool_Target_111);
		local_EnemyAlive2_System_Boolean = logic_uScriptAct_SetBool_Target_111;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_111.Out)
		{
			Relay_In_353();
		}
	}

	private void Relay_True_112()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.True(out logic_uScriptAct_SetBool_Target_112);
		local_EnemyAlive1_System_Boolean = logic_uScriptAct_SetBool_Target_112;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_112.Out)
		{
			Relay_In_357();
		}
	}

	private void Relay_False_112()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.False(out logic_uScriptAct_SetBool_Target_112);
		local_EnemyAlive1_System_Boolean = logic_uScriptAct_SetBool_Target_112;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_112.Out)
		{
			Relay_In_357();
		}
	}

	private void Relay_In_114()
	{
		logic_uScript_PlayMiscSFX_miscSFXType_114 = SFXChallengeComplete;
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_114.In(logic_uScript_PlayMiscSFX_miscSFXType_114);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_114.Out)
		{
			Relay_In_211();
		}
	}

	private void Relay_In_115()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_115.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_115, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_115[num++] = enemyGroup2Data;
		logic_uScript_GetAndCheckTechs_ownerNode_115 = owner_Connection_72;
		int num2 = 0;
		Array array = local_MobTechs2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_115.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_115, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_115, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_115 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115.In(logic_uScript_GetAndCheckTechs_techData_115, logic_uScript_GetAndCheckTechs_ownerNode_115, ref logic_uScript_GetAndCheckTechs_techs_115);
		local_MobTechs2_TankArray = logic_uScript_GetAndCheckTechs_techs_115;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_71();
		}
		if (someAlive)
		{
			Relay_In_71();
		}
		if (allDead)
		{
			Relay_In_74();
		}
		if (waitingToSpawn)
		{
			Relay_In_74();
		}
	}

	private void Relay_In_116()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_116.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_116, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_116[num++] = enemyGroup1Data;
		logic_uScript_GetAndCheckTechs_ownerNode_116 = owner_Connection_34;
		int num2 = 0;
		Array array = local_MobTechs1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_116.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_116, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_116, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_116 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.In(logic_uScript_GetAndCheckTechs_techData_116, logic_uScript_GetAndCheckTechs_ownerNode_116, ref logic_uScript_GetAndCheckTechs_techs_116);
		local_MobTechs1_TankArray = logic_uScript_GetAndCheckTechs_techs_116;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_32();
		}
		if (someAlive)
		{
			Relay_In_32();
		}
		if (allDead)
		{
			Relay_In_31();
		}
		if (waitingToSpawn)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_117()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_117.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_117, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_117[num++] = enemyGroup3Data;
		logic_uScript_GetAndCheckTechs_ownerNode_117 = owner_Connection_77;
		int num2 = 0;
		Array array = local_MobTechs3_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_117.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_117, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_117, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_117 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_117.In(logic_uScript_GetAndCheckTechs_techData_117, logic_uScript_GetAndCheckTechs_ownerNode_117, ref logic_uScript_GetAndCheckTechs_techs_117);
		local_MobTechs3_TankArray = logic_uScript_GetAndCheckTechs_techs_117;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_117.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_117.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_117.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_117.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_76();
		}
		if (someAlive)
		{
			Relay_In_76();
		}
		if (allDead)
		{
			Relay_In_79();
		}
		if (waitingToSpawn)
		{
			Relay_In_79();
		}
	}

	private void Relay_Save_Out_124()
	{
		Relay_Save_125();
	}

	private void Relay_Load_Out_124()
	{
		Relay_Load_125();
	}

	private void Relay_Restart_Out_124()
	{
		Relay_Set_False_125();
	}

	private void Relay_Save_124()
	{
		logic_SubGraph_SaveLoadBool_boolean_124 = local_EnemyAlive4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_124 = local_EnemyAlive4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Save(ref logic_SubGraph_SaveLoadBool_boolean_124, logic_SubGraph_SaveLoadBool_boolAsVariable_124, logic_SubGraph_SaveLoadBool_uniqueID_124);
	}

	private void Relay_Load_124()
	{
		logic_SubGraph_SaveLoadBool_boolean_124 = local_EnemyAlive4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_124 = local_EnemyAlive4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Load(ref logic_SubGraph_SaveLoadBool_boolean_124, logic_SubGraph_SaveLoadBool_boolAsVariable_124, logic_SubGraph_SaveLoadBool_uniqueID_124);
	}

	private void Relay_Set_True_124()
	{
		logic_SubGraph_SaveLoadBool_boolean_124 = local_EnemyAlive4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_124 = local_EnemyAlive4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_124, logic_SubGraph_SaveLoadBool_boolAsVariable_124, logic_SubGraph_SaveLoadBool_uniqueID_124);
	}

	private void Relay_Set_False_124()
	{
		logic_SubGraph_SaveLoadBool_boolean_124 = local_EnemyAlive4_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_124 = local_EnemyAlive4_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_124.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_124, logic_SubGraph_SaveLoadBool_boolAsVariable_124, logic_SubGraph_SaveLoadBool_uniqueID_124);
	}

	private void Relay_Save_Out_125()
	{
		Relay_Save_147();
	}

	private void Relay_Load_Out_125()
	{
		Relay_Load_147();
	}

	private void Relay_Restart_Out_125()
	{
		Relay_Set_False_147();
	}

	private void Relay_Save_125()
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = local_EnemyAlive5_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_125 = local_EnemyAlive5_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Save(ref logic_SubGraph_SaveLoadBool_boolean_125, logic_SubGraph_SaveLoadBool_boolAsVariable_125, logic_SubGraph_SaveLoadBool_uniqueID_125);
	}

	private void Relay_Load_125()
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = local_EnemyAlive5_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_125 = local_EnemyAlive5_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Load(ref logic_SubGraph_SaveLoadBool_boolean_125, logic_SubGraph_SaveLoadBool_boolAsVariable_125, logic_SubGraph_SaveLoadBool_uniqueID_125);
	}

	private void Relay_Set_True_125()
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = local_EnemyAlive5_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_125 = local_EnemyAlive5_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_125, logic_SubGraph_SaveLoadBool_boolAsVariable_125, logic_SubGraph_SaveLoadBool_uniqueID_125);
	}

	private void Relay_Set_False_125()
	{
		logic_SubGraph_SaveLoadBool_boolean_125 = local_EnemyAlive5_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_125 = local_EnemyAlive5_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_125.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_125, logic_SubGraph_SaveLoadBool_boolAsVariable_125, logic_SubGraph_SaveLoadBool_uniqueID_125);
	}

	private void Relay_In_126()
	{
		logic_uScript_SetEncounterTarget_owner_126 = owner_Connection_127;
		logic_uScript_SetEncounterTarget_visibleObject_126 = local_NPCTank_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_126.In(logic_uScript_SetEncounterTarget_owner_126, logic_uScript_SetEncounterTarget_visibleObject_126);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_126.Out)
		{
			Relay_In_249();
		}
	}

	private void Relay_In_129()
	{
		int num = 0;
		Array array = msgNPCAttack;
		if (logic_uScript_AddOnScreenMessage_locString_129.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_129, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_129, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_129 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_129 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_129.In(logic_uScript_AddOnScreenMessage_locString_129, logic_uScript_AddOnScreenMessage_msgPriority_129, logic_uScript_AddOnScreenMessage_holdMsg_129, logic_uScript_AddOnScreenMessage_tag_129, logic_uScript_AddOnScreenMessage_speaker_129, logic_uScript_AddOnScreenMessage_side_129);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_129.Out)
		{
			Relay_In_267();
		}
	}

	private void Relay_True_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.True(out logic_uScriptAct_SetBool_Target_132);
		local_TestStarted_System_Boolean = logic_uScriptAct_SetBool_Target_132;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_132.Out)
		{
			Relay_True_325();
		}
	}

	private void Relay_False_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.False(out logic_uScriptAct_SetBool_Target_132);
		local_TestStarted_System_Boolean = logic_uScriptAct_SetBool_Target_132;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_132.Out)
		{
			Relay_True_325();
		}
	}

	private void Relay_In_133()
	{
		logic_uScript_ResetMissionTimerTimeElapsed_owner_133 = owner_Connection_134;
		logic_uScript_ResetMissionTimerTimeElapsed_startTime_133 = local_240_System_Single;
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_133.In(logic_uScript_ResetMissionTimerTimeElapsed_owner_133, logic_uScript_ResetMissionTimerTimeElapsed_startTime_133);
		if (logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_133.Out)
		{
			Relay_In_108();
		}
	}

	private void Relay_In_136()
	{
		logic_uScript_ResetMissionTimerTimeElapsed_owner_136 = owner_Connection_135;
		logic_uScript_ResetMissionTimerTimeElapsed_startTime_136 = local_239_System_Single;
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_136.In(logic_uScript_ResetMissionTimerTimeElapsed_owner_136, logic_uScript_ResetMissionTimerTimeElapsed_startTime_136);
		if (logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_136.Out)
		{
			Relay_True_106();
		}
	}

	private void Relay_True_137()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_137.True(out logic_uScriptAct_SetBool_Target_137);
		local_TestStarted_System_Boolean = logic_uScriptAct_SetBool_Target_137;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_137.Out)
		{
			Relay_False_308();
		}
	}

	private void Relay_False_137()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_137.False(out logic_uScriptAct_SetBool_Target_137);
		local_TestStarted_System_Boolean = logic_uScriptAct_SetBool_Target_137;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_137.Out)
		{
			Relay_False_308();
		}
	}

	private void Relay_In_139()
	{
		logic_uScriptCon_CompareBool_Bool_139 = local_RetryingTest_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_139.In(logic_uScriptCon_CompareBool_Bool_139);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_139.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_139.False;
		if (num)
		{
			Relay_In_144();
		}
		if (flag)
		{
			Relay_In_55();
		}
	}

	private void Relay_True_141()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_141.True(out logic_uScriptAct_SetBool_Target_141);
		local_RetryingTest_System_Boolean = logic_uScriptAct_SetBool_Target_141;
	}

	private void Relay_False_141()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_141.False(out logic_uScriptAct_SetBool_Target_141);
		local_RetryingTest_System_Boolean = logic_uScriptAct_SetBool_Target_141;
	}

	private void Relay_In_144()
	{
		int num = 0;
		Array array = msgNPCRetrying;
		if (logic_uScript_AddOnScreenMessage_locString_144.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_144, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_144, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_144 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_144 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_144.In(logic_uScript_AddOnScreenMessage_locString_144, logic_uScript_AddOnScreenMessage_msgPriority_144, logic_uScript_AddOnScreenMessage_holdMsg_144, logic_uScript_AddOnScreenMessage_tag_144, logic_uScript_AddOnScreenMessage_speaker_144, logic_uScript_AddOnScreenMessage_side_144);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_144.Shown)
		{
			Relay_False_141();
		}
	}

	private void Relay_Save_Out_147()
	{
		Relay_Save_209();
	}

	private void Relay_Load_Out_147()
	{
		Relay_Load_209();
	}

	private void Relay_Restart_Out_147()
	{
		Relay_Set_False_209();
	}

	private void Relay_Save_147()
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = local_RetryingTest_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_147 = local_RetryingTest_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Save(ref logic_SubGraph_SaveLoadBool_boolean_147, logic_SubGraph_SaveLoadBool_boolAsVariable_147, logic_SubGraph_SaveLoadBool_uniqueID_147);
	}

	private void Relay_Load_147()
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = local_RetryingTest_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_147 = local_RetryingTest_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Load(ref logic_SubGraph_SaveLoadBool_boolean_147, logic_SubGraph_SaveLoadBool_boolAsVariable_147, logic_SubGraph_SaveLoadBool_uniqueID_147);
	}

	private void Relay_Set_True_147()
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = local_RetryingTest_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_147 = local_RetryingTest_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_147, logic_SubGraph_SaveLoadBool_boolAsVariable_147, logic_SubGraph_SaveLoadBool_uniqueID_147);
	}

	private void Relay_Set_False_147()
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = local_RetryingTest_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_147 = local_RetryingTest_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_147, logic_SubGraph_SaveLoadBool_boolAsVariable_147, logic_SubGraph_SaveLoadBool_uniqueID_147);
	}

	private void Relay_In_148()
	{
		logic_uScript_FlyTechUpAndAway_tech_148 = local_NPCTank_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_148 = FlyAwayBehaviour;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_148.In(logic_uScript_FlyTechUpAndAway_tech_148, logic_uScript_FlyTechUpAndAway_maxLifetime_148, logic_uScript_FlyTechUpAndAway_targetHeight_148, logic_uScript_FlyTechUpAndAway_aiTree_148, logic_uScript_FlyTechUpAndAway_removalParticles_148);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_148.Out)
		{
			Relay_Succeed_11();
		}
	}

	private void Relay_AtIndex_149()
	{
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_AccessListTech_techList_149.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_149, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_149, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_149.AtIndex(ref logic_uScript_AccessListTech_techList_149, logic_uScript_AccessListTech_index_149, out logic_uScript_AccessListTech_value_149);
		local_NPCTanks_TankArray = logic_uScript_AccessListTech_techList_149;
		local_NPCTank_Tank = logic_uScript_AccessListTech_value_149;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_149.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_152()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_152.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_152, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_152, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_152 = owner_Connection_151;
		int num2 = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_152.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_152, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_152, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_152 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.In(logic_uScript_GetAndCheckTechs_techData_152, logic_uScript_GetAndCheckTechs_ownerNode_152, ref logic_uScript_GetAndCheckTechs_techs_152);
		local_NPCTanks_TankArray = logic_uScript_GetAndCheckTechs_techs_152;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_149();
		}
		if (someAlive)
		{
			Relay_AtIndex_149();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_156 = TriggerNPC;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_156.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_156);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_156.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_156.OutOfRange;
		if (inRange)
		{
			Relay_In_247();
		}
		if (outOfRange)
		{
			Relay_In_639();
		}
	}

	private void Relay_In_158()
	{
		logic_uScriptCon_CompareBool_Bool_158 = local_SwitchedTech_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158.In(logic_uScriptCon_CompareBool_Bool_158);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158.False;
		if (num)
		{
			Relay_In_686();
		}
		if (flag)
		{
			Relay_In_649();
		}
	}

	private void Relay_True_160()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_160.True(out logic_uScriptAct_SetBool_Target_160);
		local_EnemyAlive5_System_Boolean = logic_uScriptAct_SetBool_Target_160;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_160.Out)
		{
			Relay_In_334();
		}
	}

	private void Relay_False_160()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_160.False(out logic_uScriptAct_SetBool_Target_160);
		local_EnemyAlive5_System_Boolean = logic_uScriptAct_SetBool_Target_160;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_160.Out)
		{
			Relay_In_334();
		}
	}

	private void Relay_InitialSpawn_163()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_163.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_163, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_163[num++] = enemyGroup4Data;
		logic_uScript_SpawnTechsFromData_ownerNode_163 = owner_Connection_169;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_163.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_163, logic_uScript_SpawnTechsFromData_ownerNode_163, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_163, logic_uScript_SpawnTechsFromData_allowResurrection_163);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_163.Out)
		{
			Relay_True_164();
		}
	}

	private void Relay_True_164()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.True(out logic_uScriptAct_SetBool_Target_164);
		local_EnemyAlive4_System_Boolean = logic_uScriptAct_SetBool_Target_164;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_164.Out)
		{
			Relay_In_340();
		}
	}

	private void Relay_False_164()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.False(out logic_uScriptAct_SetBool_Target_164);
		local_EnemyAlive4_System_Boolean = logic_uScriptAct_SetBool_Target_164;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_164.Out)
		{
			Relay_In_340();
		}
	}

	private void Relay_In_165()
	{
		logic_uScriptCon_CompareBool_Bool_165 = local_EnemyAlive4_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165.In(logic_uScriptCon_CompareBool_Bool_165);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165.False;
		if (num)
		{
			Relay_In_340();
		}
		if (flag)
		{
			Relay_InitialSpawn_163();
		}
	}

	private void Relay_InitialSpawn_166()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_166.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_166, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_166[num++] = enemyGroup5Data;
		logic_uScript_SpawnTechsFromData_ownerNode_166 = owner_Connection_172;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_166.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_166, logic_uScript_SpawnTechsFromData_ownerNode_166, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_166, logic_uScript_SpawnTechsFromData_allowResurrection_166);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_166.Out)
		{
			Relay_True_160();
		}
	}

	private void Relay_In_168()
	{
		logic_uScriptCon_CompareBool_Bool_168 = local_EnemyAlive5_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.In(logic_uScriptCon_CompareBool_Bool_168);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.False;
		if (num)
		{
			Relay_In_334();
		}
		if (flag)
		{
			Relay_InitialSpawn_166();
		}
	}

	private void Relay_In_174()
	{
		int num = 0;
		Array array = local_MobTechs4_TankArray;
		if (logic_uScript_DamageTechs_techs_174.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_174, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_174, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_174.In(logic_uScript_DamageTechs_techs_174, logic_uScript_DamageTechs_dmgPercent_174, logic_uScript_DamageTechs_givePlyrCredit_174, logic_uScript_DamageTechs_leaveBlksPercent_174, logic_uScript_DamageTechs_makeVulnerable_174);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_174.Out)
		{
			Relay_In_180();
		}
	}

	private void Relay_In_175()
	{
		int num = 0;
		Array array = local_MobTechs5_TankArray;
		if (logic_uScript_DamageTechs_techs_175.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_175, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_175, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_175.In(logic_uScript_DamageTechs_techs_175, logic_uScript_DamageTechs_dmgPercent_175, logic_uScript_DamageTechs_givePlyrCredit_175, logic_uScript_DamageTechs_leaveBlksPercent_175, logic_uScript_DamageTechs_makeVulnerable_175);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_175.Out)
		{
			Relay_In_261();
		}
	}

	private void Relay_In_176()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_176.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_176.Out)
		{
			Relay_In_261();
		}
	}

	private void Relay_In_180()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_180.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_180, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_180[num++] = enemyGroup5Data;
		logic_uScript_GetAndCheckTechs_ownerNode_180 = owner_Connection_183;
		int num2 = 0;
		Array array = local_MobTechs5_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_180.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_180, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_180, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_180 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_180.In(logic_uScript_GetAndCheckTechs_techData_180, logic_uScript_GetAndCheckTechs_ownerNode_180, ref logic_uScript_GetAndCheckTechs_techs_180);
		local_MobTechs5_TankArray = logic_uScript_GetAndCheckTechs_techs_180;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_180.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_180.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_180.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_180.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_175();
		}
		if (someAlive)
		{
			Relay_In_175();
		}
		if (allDead)
		{
			Relay_In_176();
		}
		if (waitingToSpawn)
		{
			Relay_In_176();
		}
	}

	private void Relay_In_184()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_184.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_184, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_184[num++] = enemyGroup4Data;
		logic_uScript_GetAndCheckTechs_ownerNode_184 = owner_Connection_182;
		int num2 = 0;
		Array array = local_MobTechs4_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_184.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_184, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_184, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_184 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184.In(logic_uScript_GetAndCheckTechs_techData_184, logic_uScript_GetAndCheckTechs_ownerNode_184, ref logic_uScript_GetAndCheckTechs_techs_184);
		local_MobTechs4_TankArray = logic_uScript_GetAndCheckTechs_techs_184;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_174();
		}
		if (someAlive)
		{
			Relay_In_174();
		}
		if (allDead)
		{
			Relay_In_185();
		}
		if (waitingToSpawn)
		{
			Relay_In_185();
		}
	}

	private void Relay_In_185()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_185.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_185.Out)
		{
			Relay_In_180();
		}
	}

	private void Relay_In_187()
	{
		logic_uScriptCon_CompareBool_Bool_187 = local_ReturnedToNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_187.In(logic_uScriptCon_CompareBool_Bool_187);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_187.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_187.False;
		if (num)
		{
			Relay_In_13();
		}
		if (flag)
		{
			Relay_In_188();
		}
	}

	private void Relay_In_188()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_188 = TriggerNPC;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_188.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_188);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_188.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_188.OutOfRange;
		if (inRange)
		{
			Relay_In_709();
		}
		if (outOfRange)
		{
			Relay_In_204();
		}
	}

	private void Relay_True_190()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.True(out logic_uScriptAct_SetBool_Target_190);
		local_ReturnedToNPC_System_Boolean = logic_uScriptAct_SetBool_Target_190;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_190.Out)
		{
			Relay_In_196();
		}
	}

	private void Relay_False_190()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.False(out logic_uScriptAct_SetBool_Target_190);
		local_ReturnedToNPC_System_Boolean = logic_uScriptAct_SetBool_Target_190;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_190.Out)
		{
			Relay_In_196();
		}
	}

	private void Relay_In_192()
	{
		int num = 0;
		Array array = msgNPCWellDone;
		if (logic_uScript_AddOnScreenMessage_locString_192.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_192, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_192, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_192 = local_191_System_String;
		logic_uScript_AddOnScreenMessage_speaker_192 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_192 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_192.In(logic_uScript_AddOnScreenMessage_locString_192, logic_uScript_AddOnScreenMessage_msgPriority_192, logic_uScript_AddOnScreenMessage_holdMsg_192, logic_uScript_AddOnScreenMessage_tag_192, logic_uScript_AddOnScreenMessage_speaker_192, logic_uScript_AddOnScreenMessage_side_192);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_192.Shown)
		{
			Relay_True_190();
		}
	}

	private void Relay_Out_196()
	{
	}

	private void Relay_In_196()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_196 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_196.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_196, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_196);
	}

	private void Relay_In_199()
	{
		logic_uScript_SetEncounterTarget_owner_199 = owner_Connection_206;
		logic_uScript_SetEncounterTarget_visibleObject_199 = local_NPCTank_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_199.In(logic_uScript_SetEncounterTarget_owner_199, logic_uScript_SetEncounterTarget_visibleObject_199);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_199.Out)
		{
			Relay_In_697();
		}
	}

	private void Relay_AtIndex_200()
	{
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_AccessListTech_techList_200.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_200, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_200, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_200.AtIndex(ref logic_uScript_AccessListTech_techList_200, logic_uScript_AccessListTech_index_200, out logic_uScript_AccessListTech_value_200);
		local_NPCTanks_TankArray = logic_uScript_AccessListTech_techList_200;
		local_NPCTank_Tank = logic_uScript_AccessListTech_value_200;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_200.Out)
		{
			Relay_In_202();
		}
	}

	private void Relay_In_202()
	{
		logic_uScript_SetTankInvulnerable_tank_202 = local_NPCTank_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_202.In(logic_uScript_SetTankInvulnerable_invulnerable_202, logic_uScript_SetTankInvulnerable_tank_202);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_202.Out)
		{
			Relay_In_199();
		}
	}

	private void Relay_In_204()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_204.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_204, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_204, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_204 = owner_Connection_198;
		int num2 = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_204.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_204, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_204, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_204 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_204.In(logic_uScript_GetAndCheckTechs_techData_204, logic_uScript_GetAndCheckTechs_ownerNode_204, ref logic_uScript_GetAndCheckTechs_techs_204);
		local_NPCTanks_TankArray = logic_uScript_GetAndCheckTechs_techs_204;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_204.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_204.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_200();
		}
		if (someAlive)
		{
			Relay_AtIndex_200();
		}
	}

	private void Relay_Save_Out_209()
	{
		Relay_Save_238();
	}

	private void Relay_Load_Out_209()
	{
		Relay_Load_238();
	}

	private void Relay_Restart_Out_209()
	{
		Relay_Set_True_238();
	}

	private void Relay_Save_209()
	{
		logic_SubGraph_SaveLoadBool_boolean_209 = local_ReturnedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_209 = local_ReturnedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Save(ref logic_SubGraph_SaveLoadBool_boolean_209, logic_SubGraph_SaveLoadBool_boolAsVariable_209, logic_SubGraph_SaveLoadBool_uniqueID_209);
	}

	private void Relay_Load_209()
	{
		logic_SubGraph_SaveLoadBool_boolean_209 = local_ReturnedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_209 = local_ReturnedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Load(ref logic_SubGraph_SaveLoadBool_boolean_209, logic_SubGraph_SaveLoadBool_boolAsVariable_209, logic_SubGraph_SaveLoadBool_uniqueID_209);
	}

	private void Relay_Set_True_209()
	{
		logic_SubGraph_SaveLoadBool_boolean_209 = local_ReturnedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_209 = local_ReturnedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_209, logic_SubGraph_SaveLoadBool_boolAsVariable_209, logic_SubGraph_SaveLoadBool_uniqueID_209);
	}

	private void Relay_Set_False_209()
	{
		logic_SubGraph_SaveLoadBool_boolean_209 = local_ReturnedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_209 = local_ReturnedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_209.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_209, logic_SubGraph_SaveLoadBool_boolAsVariable_209, logic_SubGraph_SaveLoadBool_uniqueID_209);
	}

	private void Relay_In_211()
	{
		int num = 0;
		Array array = msgNPCTestComplete;
		if (logic_uScript_AddOnScreenMessage_locString_211.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_211, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_211, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_211 = local_msgTagTestComplete_System_String;
		logic_uScript_AddOnScreenMessage_speaker_211 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_211 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_211.In(logic_uScript_AddOnScreenMessage_locString_211, logic_uScript_AddOnScreenMessage_msgPriority_211, logic_uScript_AddOnScreenMessage_holdMsg_211, logic_uScript_AddOnScreenMessage_tag_211, logic_uScript_AddOnScreenMessage_speaker_211, logic_uScript_AddOnScreenMessage_side_211);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_211.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_216()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_216.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_216, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_216, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_216 = owner_Connection_220;
		int num2 = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_216.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_216, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_216, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_216 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_216.In(logic_uScript_GetAndCheckTechs_techData_216, logic_uScript_GetAndCheckTechs_ownerNode_216, ref logic_uScript_GetAndCheckTechs_techs_216);
		local_NPCTanks_TankArray = logic_uScript_GetAndCheckTechs_techs_216;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_216.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_216.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_219();
		}
		if (someAlive)
		{
			Relay_AtIndex_219();
		}
	}

	private void Relay_AtIndex_219()
	{
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_AccessListTech_techList_219.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_219, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_219, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_219.AtIndex(ref logic_uScript_AccessListTech_techList_219, logic_uScript_AccessListTech_index_219, out logic_uScript_AccessListTech_value_219);
		local_NPCTanks_TankArray = logic_uScript_AccessListTech_techList_219;
		local_NPCTank_Tank = logic_uScript_AccessListTech_value_219;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_219.Out)
		{
			Relay_In_222();
		}
	}

	private void Relay_In_221()
	{
		logic_uScript_SetEncounterTarget_owner_221 = owner_Connection_224;
		logic_uScript_SetEncounterTarget_visibleObject_221 = local_NPCTank_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_221.In(logic_uScript_SetEncounterTarget_owner_221, logic_uScript_SetEncounterTarget_visibleObject_221);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_221.Out)
		{
			Relay_In_892();
		}
	}

	private void Relay_In_222()
	{
		logic_uScript_SetTankInvulnerable_tank_222 = local_NPCTank_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_222.In(logic_uScript_SetTankInvulnerable_invulnerable_222, logic_uScript_SetTankInvulnerable_tank_222);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_222.Out)
		{
			Relay_In_1090();
		}
	}

	private void Relay_In_226()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_226 = TriggerCrater;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_226.In(logic_uScript_SetEncounterTargetPosition_positionName_226);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_226.Out)
		{
			Relay_True_28();
		}
	}

	private void Relay_In_228()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_228 = TriggerCrater;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_228.In(logic_uScript_SetEncounterTargetPosition_positionName_228);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_228.Out)
		{
			Relay_In_232();
		}
	}

	private void Relay_In_232()
	{
		int num = 0;
		Array array = msgNPCRebriefing;
		if (logic_uScript_AddOnScreenMessage_locString_232.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_232, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_232, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_232 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_232 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_232.In(logic_uScript_AddOnScreenMessage_locString_232, logic_uScript_AddOnScreenMessage_msgPriority_232, logic_uScript_AddOnScreenMessage_holdMsg_232, logic_uScript_AddOnScreenMessage_tag_232, logic_uScript_AddOnScreenMessage_speaker_232, logic_uScript_AddOnScreenMessage_side_232);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_232.Shown)
		{
			Relay_True_234();
		}
	}

	private void Relay_True_234()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_234.True(out logic_uScriptAct_SetBool_Target_234);
		local_Rebriefed_System_Boolean = logic_uScriptAct_SetBool_Target_234;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_234.Out)
		{
			Relay_False_1082();
		}
	}

	private void Relay_False_234()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_234.False(out logic_uScriptAct_SetBool_Target_234);
		local_Rebriefed_System_Boolean = logic_uScriptAct_SetBool_Target_234;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_234.Out)
		{
			Relay_False_1082();
		}
	}

	private void Relay_True_235()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_235.True(out logic_uScriptAct_SetBool_Target_235);
		local_Rebriefed_System_Boolean = logic_uScriptAct_SetBool_Target_235;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_235.Out)
		{
			Relay_True_309();
		}
	}

	private void Relay_False_235()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_235.False(out logic_uScriptAct_SetBool_Target_235);
		local_Rebriefed_System_Boolean = logic_uScriptAct_SetBool_Target_235;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_235.Out)
		{
			Relay_True_309();
		}
	}

	private void Relay_Save_Out_238()
	{
		Relay_Save_251();
	}

	private void Relay_Load_Out_238()
	{
		Relay_Load_251();
	}

	private void Relay_Restart_Out_238()
	{
		Relay_Set_False_251();
	}

	private void Relay_Save_238()
	{
		logic_SubGraph_SaveLoadBool_boolean_238 = local_Rebriefed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_238 = local_Rebriefed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Save(ref logic_SubGraph_SaveLoadBool_boolean_238, logic_SubGraph_SaveLoadBool_boolAsVariable_238, logic_SubGraph_SaveLoadBool_uniqueID_238);
	}

	private void Relay_Load_238()
	{
		logic_SubGraph_SaveLoadBool_boolean_238 = local_Rebriefed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_238 = local_Rebriefed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Load(ref logic_SubGraph_SaveLoadBool_boolean_238, logic_SubGraph_SaveLoadBool_boolAsVariable_238, logic_SubGraph_SaveLoadBool_uniqueID_238);
	}

	private void Relay_Set_True_238()
	{
		logic_SubGraph_SaveLoadBool_boolean_238 = local_Rebriefed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_238 = local_Rebriefed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_238, logic_SubGraph_SaveLoadBool_boolAsVariable_238, logic_SubGraph_SaveLoadBool_uniqueID_238);
	}

	private void Relay_Set_False_238()
	{
		logic_SubGraph_SaveLoadBool_boolean_238 = local_Rebriefed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_238 = local_Rebriefed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_238.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_238, logic_SubGraph_SaveLoadBool_boolAsVariable_238, logic_SubGraph_SaveLoadBool_uniqueID_238);
	}

	private void Relay_In_243()
	{
		int num = 0;
		Array array = msgNPCIntroJoke;
		if (logic_uScript_AddOnScreenMessage_locString_243.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_243, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_243, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_243 = local_244_System_String;
		logic_uScript_AddOnScreenMessage_speaker_243 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_243 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_243.In(logic_uScript_AddOnScreenMessage_locString_243, logic_uScript_AddOnScreenMessage_msgPriority_243, logic_uScript_AddOnScreenMessage_holdMsg_243, logic_uScript_AddOnScreenMessage_tag_243, logic_uScript_AddOnScreenMessage_speaker_243, logic_uScript_AddOnScreenMessage_side_243);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_243.Shown)
		{
			Relay_True_245();
		}
	}

	private void Relay_True_245()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_245.True(out logic_uScriptAct_SetBool_Target_245);
		local_FinishedJoke_System_Boolean = logic_uScriptAct_SetBool_Target_245;
	}

	private void Relay_False_245()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_245.False(out logic_uScriptAct_SetBool_Target_245);
		local_FinishedJoke_System_Boolean = logic_uScriptAct_SetBool_Target_245;
	}

	private void Relay_In_247()
	{
		logic_uScriptCon_CompareBool_Bool_247 = local_FinishedJoke_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_247.In(logic_uScriptCon_CompareBool_Bool_247);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_247.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_247.False;
		if (num)
		{
			Relay_In_721();
		}
		if (flag)
		{
			Relay_True_637();
		}
	}

	private void Relay_In_249()
	{
		logic_uScript_SetCustomRadarTeamID_tech_249 = local_NPCTank_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_249.In(logic_uScript_SetCustomRadarTeamID_tech_249, logic_uScript_SetCustomRadarTeamID_radarTeamID_249);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_249.Out)
		{
			Relay_In_259();
		}
	}

	private void Relay_Save_Out_251()
	{
		Relay_Save_288();
	}

	private void Relay_Load_Out_251()
	{
		Relay_Load_288();
	}

	private void Relay_Restart_Out_251()
	{
		Relay_Set_False_288();
	}

	private void Relay_Save_251()
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = local_FinishedJoke_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_251 = local_FinishedJoke_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Save(ref logic_SubGraph_SaveLoadBool_boolean_251, logic_SubGraph_SaveLoadBool_boolAsVariable_251, logic_SubGraph_SaveLoadBool_uniqueID_251);
	}

	private void Relay_Load_251()
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = local_FinishedJoke_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_251 = local_FinishedJoke_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Load(ref logic_SubGraph_SaveLoadBool_boolean_251, logic_SubGraph_SaveLoadBool_boolAsVariable_251, logic_SubGraph_SaveLoadBool_uniqueID_251);
	}

	private void Relay_Set_True_251()
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = local_FinishedJoke_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_251 = local_FinishedJoke_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_251, logic_SubGraph_SaveLoadBool_boolAsVariable_251, logic_SubGraph_SaveLoadBool_uniqueID_251);
	}

	private void Relay_Set_False_251()
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = local_FinishedJoke_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_251 = local_FinishedJoke_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_251, logic_SubGraph_SaveLoadBool_boolAsVariable_251, logic_SubGraph_SaveLoadBool_uniqueID_251);
	}

	private void Relay_In_253()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_253 = local_252_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_253.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_253, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_253);
	}

	private void Relay_In_254()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_254 = owner_Connection_255;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_254.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_254);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_254.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_254.False;
		if (num)
		{
			Relay_In_156();
		}
		if (flag)
		{
			Relay_In_253();
		}
	}

	private void Relay_InitialSpawn_257()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_SpawnTechsFromData_spawnData_257.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_257, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_SpawnTechsFromData_spawnData_257, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_257 = owner_Connection_256;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_257.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_257, logic_uScript_SpawnTechsFromData_ownerNode_257, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_257, logic_uScript_SpawnTechsFromData_allowResurrection_257);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_257.Out)
		{
			Relay_InitialSpawn_20();
		}
	}

	private void Relay_In_259()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_259.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_259, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_259, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_259 = owner_Connection_258;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_259.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_259, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_259, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_259 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_259.In(logic_uScript_GetAndCheckTechs_techData_259, logic_uScript_GetAndCheckTechs_ownerNode_259, ref logic_uScript_GetAndCheckTechs_techs_259);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_259;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_259.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_259.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_260();
		}
		if (someAlive)
		{
			Relay_AtIndex_260();
		}
	}

	private void Relay_AtIndex_260()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_260.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_260, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_260, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_260.AtIndex(ref logic_uScript_AccessListTech_techList_260, logic_uScript_AccessListTech_index_260, out logic_uScript_AccessListTech_value_260);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_260;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_260;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_260.Out)
		{
			Relay_True_50();
		}
	}

	private void Relay_In_261()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_261.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_261.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_263()
	{
		logic_uScript_PlayMiscSFX_miscSFXType_263 = SFXChallengeStarted;
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_263.In(logic_uScript_PlayMiscSFX_miscSFXType_263);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_263.Out)
		{
			Relay_True_132();
		}
	}

	private void Relay_In_266()
	{
		logic_uScript_StartMissionTimer_owner_266 = owner_Connection_262;
		logic_uScript_StartMissionTimer_startTime_266 = RunTimeLimit;
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_266.In(logic_uScript_StartMissionTimer_owner_266, logic_uScript_StartMissionTimer_startTime_266);
		if (logic_uScript_StartMissionTimer_uScript_StartMissionTimer_266.Out)
		{
			Relay_In_263();
		}
	}

	private void Relay_In_267()
	{
		logic_uScript_ShowMissionTimerUI_owner_267 = owner_Connection_265;
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_267.In(logic_uScript_ShowMissionTimerUI_owner_267, logic_uScript_ShowMissionTimerUI_showBestTime_267);
		if (logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_267.Out)
		{
			Relay_In_266();
		}
	}

	private void Relay_In_269()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_269 = owner_Connection_270;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_269.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_269);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_269.Out)
		{
			Relay_InitialSpawn_257();
		}
	}

	private void Relay_Pause_271()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_271.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_271.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_UnPause_271()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_271.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_271.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_272()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_272 = owner_Connection_273;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_272.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_272);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_272.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_272.False;
		if (num)
		{
			Relay_Pause_271();
		}
		if (flag)
		{
			Relay_UnPause_1030();
		}
	}

	private void Relay_In_274()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_274 = local_275_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_274.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_274, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_274);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_274.Out)
		{
			Relay_In_1067();
		}
	}

	private void Relay_In_276()
	{
		int num = 0;
		Array array = msgNPCTestFailedPlayerFled;
		if (logic_uScript_AddOnScreenMessage_locString_276.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_276, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_276, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_276 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_276 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_276.In(logic_uScript_AddOnScreenMessage_locString_276, logic_uScript_AddOnScreenMessage_msgPriority_276, logic_uScript_AddOnScreenMessage_holdMsg_276, logic_uScript_AddOnScreenMessage_tag_276, logic_uScript_AddOnScreenMessage_speaker_276, logic_uScript_AddOnScreenMessage_side_276);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_276.Out)
		{
			Relay_False_298();
		}
	}

	private void Relay_In_279()
	{
		logic_uScript_PlayMiscSFX_miscSFXType_279 = local_SFXChallengeFailed_ManSFX_MiscSfxType;
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_279.In(logic_uScript_PlayMiscSFX_miscSFXType_279);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_279.Out)
		{
			Relay_False_137();
		}
	}

	private void Relay_In_280()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_280 = local_282_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_280.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_280, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_280);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_280.Out)
		{
			Relay_In_283();
		}
	}

	private void Relay_In_283()
	{
		logic_uScriptCon_CompareBool_Bool_283 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_283.In(logic_uScriptCon_CompareBool_Bool_283);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_283.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_283.False;
		if (num)
		{
			Relay_In_297();
		}
		if (flag)
		{
			Relay_In_295();
		}
	}

	private void Relay_In_285()
	{
		logic_uScriptCon_CompareBool_Bool_285 = local_TestFailed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_285.In(logic_uScriptCon_CompareBool_Bool_285);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_285.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_285.False;
		if (num)
		{
			Relay_In_116();
		}
		if (flag)
		{
			Relay_In_707();
		}
	}

	private void Relay_Save_Out_288()
	{
		Relay_Save_314();
	}

	private void Relay_Load_Out_288()
	{
		Relay_Load_314();
	}

	private void Relay_Restart_Out_288()
	{
		Relay_Set_False_314();
	}

	private void Relay_Save_288()
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = local_TestFailed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_288 = local_TestFailed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Save(ref logic_SubGraph_SaveLoadBool_boolean_288, logic_SubGraph_SaveLoadBool_boolAsVariable_288, logic_SubGraph_SaveLoadBool_uniqueID_288);
	}

	private void Relay_Load_288()
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = local_TestFailed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_288 = local_TestFailed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Load(ref logic_SubGraph_SaveLoadBool_boolean_288, logic_SubGraph_SaveLoadBool_boolAsVariable_288, logic_SubGraph_SaveLoadBool_uniqueID_288);
	}

	private void Relay_Set_True_288()
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = local_TestFailed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_288 = local_TestFailed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_288, logic_SubGraph_SaveLoadBool_boolAsVariable_288, logic_SubGraph_SaveLoadBool_uniqueID_288);
	}

	private void Relay_Set_False_288()
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = local_TestFailed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_288 = local_TestFailed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_288, logic_SubGraph_SaveLoadBool_boolAsVariable_288, logic_SubGraph_SaveLoadBool_uniqueID_288);
	}

	private void Relay_True_289()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_289.True(out logic_uScriptAct_SetBool_Target_289);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_289;
	}

	private void Relay_False_289()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_289.False(out logic_uScriptAct_SetBool_Target_289);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_289;
	}

	private void Relay_True_290()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_290.True(out logic_uScriptAct_SetBool_Target_290);
		local_TestFailed_System_Boolean = logic_uScriptAct_SetBool_Target_290;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_290.Out)
		{
			Relay_In_285();
		}
	}

	private void Relay_False_290()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_290.False(out logic_uScriptAct_SetBool_Target_290);
		local_TestFailed_System_Boolean = logic_uScriptAct_SetBool_Target_290;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_290.Out)
		{
			Relay_In_285();
		}
	}

	private void Relay_In_295()
	{
		logic_uScriptCon_CompareBool_Bool_295 = local_TestFailed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_295.In(logic_uScriptCon_CompareBool_Bool_295);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_295.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_295.False;
		if (num)
		{
			Relay_In_912();
		}
		if (flag)
		{
			Relay_In_297();
		}
	}

	private void Relay_In_296()
	{
		logic_uScriptCon_CompareBool_Bool_296 = local_OutOfBounds_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_296.In(logic_uScriptCon_CompareBool_Bool_296);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_296.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_296.False;
		if (num)
		{
			Relay_In_276();
		}
		if (flag)
		{
			Relay_In_301();
		}
	}

	private void Relay_In_297()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_297.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_297.Out)
		{
			Relay_In_317();
		}
	}

	private void Relay_True_298()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_298.True(out logic_uScriptAct_SetBool_Target_298);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_298;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_298.Out)
		{
			Relay_In_279();
		}
	}

	private void Relay_False_298()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_298.False(out logic_uScriptAct_SetBool_Target_298);
		local_OutOfBounds_System_Boolean = logic_uScriptAct_SetBool_Target_298;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_298.Out)
		{
			Relay_In_279();
		}
	}

	private void Relay_In_301()
	{
		logic_uScriptCon_CompareBool_Bool_301 = local_PlayerSwitchedTech_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301.In(logic_uScriptCon_CompareBool_Bool_301);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301.False;
		if (num)
		{
			Relay_In_303();
		}
		if (flag)
		{
			Relay_In_1047();
		}
	}

	private void Relay_In_303()
	{
		int num = 0;
		Array array = msgNPCTestFailedPlayerSwitchedTech;
		if (logic_uScript_AddOnScreenMessage_locString_303.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_303, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_303, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_303 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_303 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_303.In(logic_uScript_AddOnScreenMessage_locString_303, logic_uScript_AddOnScreenMessage_msgPriority_303, logic_uScript_AddOnScreenMessage_holdMsg_303, logic_uScript_AddOnScreenMessage_tag_303, logic_uScript_AddOnScreenMessage_speaker_303, logic_uScript_AddOnScreenMessage_side_303);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_303.Out)
		{
			Relay_False_306();
		}
	}

	private void Relay_True_306()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_306.True(out logic_uScriptAct_SetBool_Target_306);
		local_PlayerSwitchedTech_System_Boolean = logic_uScriptAct_SetBool_Target_306;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_306.Out)
		{
			Relay_In_279();
		}
	}

	private void Relay_False_306()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_306.False(out logic_uScriptAct_SetBool_Target_306);
		local_PlayerSwitchedTech_System_Boolean = logic_uScriptAct_SetBool_Target_306;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_306.Out)
		{
			Relay_In_279();
		}
	}

	private void Relay_True_308()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_308.True(out logic_uScriptAct_SetBool_Target_308);
		local_TestFailed_System_Boolean = logic_uScriptAct_SetBool_Target_308;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_308.Out)
		{
			Relay_False_235();
		}
	}

	private void Relay_False_308()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_308.False(out logic_uScriptAct_SetBool_Target_308);
		local_TestFailed_System_Boolean = logic_uScriptAct_SetBool_Target_308;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_308.Out)
		{
			Relay_False_235();
		}
	}

	private void Relay_True_309()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_309.True(out logic_uScriptAct_SetBool_Target_309);
		local_RetryingTest_System_Boolean = logic_uScriptAct_SetBool_Target_309;
	}

	private void Relay_False_309()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_309.False(out logic_uScriptAct_SetBool_Target_309);
		local_RetryingTest_System_Boolean = logic_uScriptAct_SetBool_Target_309;
	}

	private void Relay_Save_Out_314()
	{
		Relay_Save_315();
	}

	private void Relay_Load_Out_314()
	{
		Relay_Load_315();
	}

	private void Relay_Restart_Out_314()
	{
		Relay_Set_False_315();
	}

	private void Relay_Save_314()
	{
		logic_SubGraph_SaveLoadBool_boolean_314 = local_OutOfBounds_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_314 = local_OutOfBounds_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Save(ref logic_SubGraph_SaveLoadBool_boolean_314, logic_SubGraph_SaveLoadBool_boolAsVariable_314, logic_SubGraph_SaveLoadBool_uniqueID_314);
	}

	private void Relay_Load_314()
	{
		logic_SubGraph_SaveLoadBool_boolean_314 = local_OutOfBounds_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_314 = local_OutOfBounds_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Load(ref logic_SubGraph_SaveLoadBool_boolean_314, logic_SubGraph_SaveLoadBool_boolAsVariable_314, logic_SubGraph_SaveLoadBool_uniqueID_314);
	}

	private void Relay_Set_True_314()
	{
		logic_SubGraph_SaveLoadBool_boolean_314 = local_OutOfBounds_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_314 = local_OutOfBounds_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_314, logic_SubGraph_SaveLoadBool_boolAsVariable_314, logic_SubGraph_SaveLoadBool_uniqueID_314);
	}

	private void Relay_Set_False_314()
	{
		logic_SubGraph_SaveLoadBool_boolean_314 = local_OutOfBounds_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_314 = local_OutOfBounds_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_314.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_314, logic_SubGraph_SaveLoadBool_boolAsVariable_314, logic_SubGraph_SaveLoadBool_uniqueID_314);
	}

	private void Relay_Save_Out_315()
	{
		Relay_Save_316();
	}

	private void Relay_Load_Out_315()
	{
		Relay_Load_316();
	}

	private void Relay_Restart_Out_315()
	{
		Relay_Set_False_316();
	}

	private void Relay_Save_315()
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = local_PlayerSwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_315 = local_PlayerSwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Save(ref logic_SubGraph_SaveLoadBool_boolean_315, logic_SubGraph_SaveLoadBool_boolAsVariable_315, logic_SubGraph_SaveLoadBool_uniqueID_315);
	}

	private void Relay_Load_315()
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = local_PlayerSwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_315 = local_PlayerSwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Load(ref logic_SubGraph_SaveLoadBool_boolean_315, logic_SubGraph_SaveLoadBool_boolAsVariable_315, logic_SubGraph_SaveLoadBool_uniqueID_315);
	}

	private void Relay_Set_True_315()
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = local_PlayerSwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_315 = local_PlayerSwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_315, logic_SubGraph_SaveLoadBool_boolAsVariable_315, logic_SubGraph_SaveLoadBool_uniqueID_315);
	}

	private void Relay_Set_False_315()
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = local_PlayerSwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_315 = local_PlayerSwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_315, logic_SubGraph_SaveLoadBool_boolAsVariable_315, logic_SubGraph_SaveLoadBool_uniqueID_315);
	}

	private void Relay_Save_Out_316()
	{
		Relay_Save_328();
	}

	private void Relay_Load_Out_316()
	{
		Relay_Load_328();
	}

	private void Relay_Restart_Out_316()
	{
		Relay_Set_False_328();
	}

	private void Relay_Save_316()
	{
		logic_SubGraph_SaveLoadBool_boolean_316 = local_PlayerSpawnedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_316 = local_PlayerSpawnedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Save(ref logic_SubGraph_SaveLoadBool_boolean_316, logic_SubGraph_SaveLoadBool_boolAsVariable_316, logic_SubGraph_SaveLoadBool_uniqueID_316);
	}

	private void Relay_Load_316()
	{
		logic_SubGraph_SaveLoadBool_boolean_316 = local_PlayerSpawnedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_316 = local_PlayerSpawnedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Load(ref logic_SubGraph_SaveLoadBool_boolean_316, logic_SubGraph_SaveLoadBool_boolAsVariable_316, logic_SubGraph_SaveLoadBool_uniqueID_316);
	}

	private void Relay_Set_True_316()
	{
		logic_SubGraph_SaveLoadBool_boolean_316 = local_PlayerSpawnedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_316 = local_PlayerSpawnedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_316, logic_SubGraph_SaveLoadBool_boolAsVariable_316, logic_SubGraph_SaveLoadBool_uniqueID_316);
	}

	private void Relay_Set_False_316()
	{
		logic_SubGraph_SaveLoadBool_boolean_316 = local_PlayerSpawnedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_316 = local_PlayerSpawnedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_316.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_316, logic_SubGraph_SaveLoadBool_boolAsVariable_316, logic_SubGraph_SaveLoadBool_uniqueID_316);
	}

	private void Relay_In_317()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_317.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_317.Out)
		{
			Relay_In_318();
		}
	}

	private void Relay_In_318()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_318.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_318.Out)
		{
			Relay_False_137();
		}
	}

	private void Relay_True_319()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_319.True(out logic_uScriptAct_SetBool_Target_319);
		local_TestFailed_System_Boolean = logic_uScriptAct_SetBool_Target_319;
	}

	private void Relay_False_319()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_319.False(out logic_uScriptAct_SetBool_Target_319);
		local_TestFailed_System_Boolean = logic_uScriptAct_SetBool_Target_319;
	}

	private void Relay_True_321()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_321.True(out logic_uScriptAct_SetBool_Target_321);
		local_PlayerSwitchedTech_System_Boolean = logic_uScriptAct_SetBool_Target_321;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_321.Out)
		{
			Relay_True_319();
		}
	}

	private void Relay_False_321()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_321.False(out logic_uScriptAct_SetBool_Target_321);
		local_PlayerSwitchedTech_System_Boolean = logic_uScriptAct_SetBool_Target_321;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_321.Out)
		{
			Relay_True_319();
		}
	}

	private void Relay_In_323()
	{
		logic_uScriptCon_CompareBool_Bool_323 = local_Rebriefed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_323.In(logic_uScriptCon_CompareBool_Bool_323);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_323.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_323.False;
		if (num)
		{
			Relay_In_1088();
		}
		if (flag)
		{
			Relay_In_333();
		}
	}

	private void Relay_True_325()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_325.True(out logic_uScriptAct_SetBool_Target_325);
		local_TestAttempted_System_Boolean = logic_uScriptAct_SetBool_Target_325;
	}

	private void Relay_False_325()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_325.False(out logic_uScriptAct_SetBool_Target_325);
		local_TestAttempted_System_Boolean = logic_uScriptAct_SetBool_Target_325;
	}

	private void Relay_Save_Out_328()
	{
		Relay_Save_661();
	}

	private void Relay_Load_Out_328()
	{
		Relay_Load_661();
	}

	private void Relay_Restart_Out_328()
	{
		Relay_Set_False_661();
	}

	private void Relay_Save_328()
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = local_TestAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_328 = local_TestAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Save(ref logic_SubGraph_SaveLoadBool_boolean_328, logic_SubGraph_SaveLoadBool_boolAsVariable_328, logic_SubGraph_SaveLoadBool_uniqueID_328);
	}

	private void Relay_Load_328()
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = local_TestAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_328 = local_TestAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Load(ref logic_SubGraph_SaveLoadBool_boolean_328, logic_SubGraph_SaveLoadBool_boolAsVariable_328, logic_SubGraph_SaveLoadBool_uniqueID_328);
	}

	private void Relay_Set_True_328()
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = local_TestAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_328 = local_TestAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_328, logic_SubGraph_SaveLoadBool_boolAsVariable_328, logic_SubGraph_SaveLoadBool_uniqueID_328);
	}

	private void Relay_Set_False_328()
	{
		logic_SubGraph_SaveLoadBool_boolean_328 = local_TestAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_328 = local_TestAttempted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_328.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_328, logic_SubGraph_SaveLoadBool_boolAsVariable_328, logic_SubGraph_SaveLoadBool_uniqueID_328);
	}

	private void Relay_In_329()
	{
		logic_uScriptCon_CompareBool_Bool_329 = local_TestAttempted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_329.In(logic_uScriptCon_CompareBool_Bool_329);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_329.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_329.False;
		if (num)
		{
			Relay_In_323();
		}
		if (flag)
		{
			Relay_In_1084();
		}
	}

	private void Relay_In_331()
	{
		logic_uScriptCon_CompareBool_Bool_331 = local_TestStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_331.In(logic_uScriptCon_CompareBool_Bool_331);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_331.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_331.False;
		if (num)
		{
			Relay_In_1084();
		}
		if (flag)
		{
			Relay_In_329();
		}
	}

	private void Relay_In_333()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_333.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_333.Out)
		{
			Relay_In_216();
		}
	}

	private void Relay_In_334()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_334.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_334, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_334[num++] = enemyGroup5Data;
		logic_uScript_GetAndCheckTechs_ownerNode_334 = owner_Connection_335;
		logic_uScript_GetAndCheckTechs_Return_334 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_334.In(logic_uScript_GetAndCheckTechs_techData_334, logic_uScript_GetAndCheckTechs_ownerNode_334, ref logic_uScript_GetAndCheckTechs_techs_334);
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_334.AllDead)
		{
			Relay_False_336();
		}
	}

	private void Relay_True_336()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_336.True(out logic_uScriptAct_SetBool_Target_336);
		local_EnemyAlive5_System_Boolean = logic_uScriptAct_SetBool_Target_336;
	}

	private void Relay_False_336()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_336.False(out logic_uScriptAct_SetBool_Target_336);
		local_EnemyAlive5_System_Boolean = logic_uScriptAct_SetBool_Target_336;
	}

	private void Relay_In_340()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_340.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_340, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_340[num++] = enemyGroup4Data;
		logic_uScript_GetAndCheckTechs_ownerNode_340 = owner_Connection_342;
		logic_uScript_GetAndCheckTechs_Return_340 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_340.In(logic_uScript_GetAndCheckTechs_techData_340, logic_uScript_GetAndCheckTechs_ownerNode_340, ref logic_uScript_GetAndCheckTechs_techs_340);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_340.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_340.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_340.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_340.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_359();
		}
		if (someAlive)
		{
			Relay_In_359();
		}
		if (allDead)
		{
			Relay_False_343();
		}
		if (waitingToSpawn)
		{
			Relay_In_359();
		}
	}

	private void Relay_True_343()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_343.True(out logic_uScriptAct_SetBool_Target_343);
		local_EnemyAlive4_System_Boolean = logic_uScriptAct_SetBool_Target_343;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_343.Out)
		{
			Relay_In_359();
		}
	}

	private void Relay_False_343()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_343.False(out logic_uScriptAct_SetBool_Target_343);
		local_EnemyAlive4_System_Boolean = logic_uScriptAct_SetBool_Target_343;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_343.Out)
		{
			Relay_In_359();
		}
	}

	private void Relay_True_346()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_346.True(out logic_uScriptAct_SetBool_Target_346);
		local_EnemyAlive3_System_Boolean = logic_uScriptAct_SetBool_Target_346;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_346.Out)
		{
			Relay_In_363();
		}
	}

	private void Relay_False_346()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_346.False(out logic_uScriptAct_SetBool_Target_346);
		local_EnemyAlive3_System_Boolean = logic_uScriptAct_SetBool_Target_346;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_346.Out)
		{
			Relay_In_363();
		}
	}

	private void Relay_In_347()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_347.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_347, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_347[num++] = enemyGroup3Data;
		logic_uScript_GetAndCheckTechs_ownerNode_347 = owner_Connection_344;
		logic_uScript_GetAndCheckTechs_Return_347 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_347.In(logic_uScript_GetAndCheckTechs_techData_347, logic_uScript_GetAndCheckTechs_ownerNode_347, ref logic_uScript_GetAndCheckTechs_techs_347);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_347.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_347.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_347.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_347.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_363();
		}
		if (someAlive)
		{
			Relay_In_363();
		}
		if (allDead)
		{
			Relay_False_346();
		}
		if (waitingToSpawn)
		{
			Relay_In_363();
		}
	}

	private void Relay_True_352()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_352.True(out logic_uScriptAct_SetBool_Target_352);
		local_EnemyAlive2_System_Boolean = logic_uScriptAct_SetBool_Target_352;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_352.Out)
		{
			Relay_In_370();
		}
	}

	private void Relay_False_352()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_352.False(out logic_uScriptAct_SetBool_Target_352);
		local_EnemyAlive2_System_Boolean = logic_uScriptAct_SetBool_Target_352;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_352.Out)
		{
			Relay_In_370();
		}
	}

	private void Relay_In_353()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_353.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_353, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_353[num++] = enemyGroup2Data;
		logic_uScript_GetAndCheckTechs_ownerNode_353 = owner_Connection_350;
		logic_uScript_GetAndCheckTechs_Return_353 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_353.In(logic_uScript_GetAndCheckTechs_techData_353, logic_uScript_GetAndCheckTechs_ownerNode_353, ref logic_uScript_GetAndCheckTechs_techs_353);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_353.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_353.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_353.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_353.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_370();
		}
		if (someAlive)
		{
			Relay_In_370();
		}
		if (allDead)
		{
			Relay_False_352();
		}
		if (waitingToSpawn)
		{
			Relay_In_370();
		}
	}

	private void Relay_True_356()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_356.True(out logic_uScriptAct_SetBool_Target_356);
		local_EnemyAlive1_System_Boolean = logic_uScriptAct_SetBool_Target_356;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_356.Out)
		{
			Relay_In_374();
		}
	}

	private void Relay_False_356()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_356.False(out logic_uScriptAct_SetBool_Target_356);
		local_EnemyAlive1_System_Boolean = logic_uScriptAct_SetBool_Target_356;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_356.Out)
		{
			Relay_In_374();
		}
	}

	private void Relay_In_357()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_357.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_357, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_357[num++] = enemyGroup1Data;
		logic_uScript_GetAndCheckTechs_ownerNode_357 = owner_Connection_358;
		logic_uScript_GetAndCheckTechs_Return_357 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.In(logic_uScript_GetAndCheckTechs_techData_357, logic_uScript_GetAndCheckTechs_ownerNode_357, ref logic_uScript_GetAndCheckTechs_techs_357);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_374();
		}
		if (someAlive)
		{
			Relay_In_374();
		}
		if (allDead)
		{
			Relay_False_356();
		}
		if (waitingToSpawn)
		{
			Relay_In_374();
		}
	}

	private void Relay_In_359()
	{
		logic_uScript_GetMissionTimerDisplayTime_owner_359 = owner_Connection_361;
		logic_uScript_GetMissionTimerDisplayTime_Return_359 = logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_359.In(logic_uScript_GetMissionTimerDisplayTime_owner_359);
		local_360_System_Single = logic_uScript_GetMissionTimerDisplayTime_Return_359;
		if (logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_359.Out)
		{
			Relay_In_362();
		}
	}

	private void Relay_In_362()
	{
		logic_uScriptCon_CompareFloat_A_362 = local_360_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_362.In(logic_uScriptCon_CompareFloat_A_362, logic_uScriptCon_CompareFloat_B_362);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_362.LessThan)
		{
			Relay_In_168();
		}
	}

	private void Relay_In_363()
	{
		logic_uScript_GetMissionTimerDisplayTime_owner_363 = owner_Connection_366;
		logic_uScript_GetMissionTimerDisplayTime_Return_363 = logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_363.In(logic_uScript_GetMissionTimerDisplayTime_owner_363);
		local_365_System_Single = logic_uScript_GetMissionTimerDisplayTime_Return_363;
		if (logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_363.Out)
		{
			Relay_In_364();
		}
	}

	private void Relay_In_364()
	{
		logic_uScriptCon_CompareFloat_A_364 = local_365_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_364.In(logic_uScriptCon_CompareFloat_A_364, logic_uScriptCon_CompareFloat_B_364);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_364.LessThan)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_368()
	{
		logic_uScriptCon_CompareFloat_A_368 = local_367_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_368.In(logic_uScriptCon_CompareFloat_A_368, logic_uScriptCon_CompareFloat_B_368);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_368.LessThan)
		{
			Relay_In_87();
		}
	}

	private void Relay_In_370()
	{
		logic_uScript_GetMissionTimerDisplayTime_owner_370 = owner_Connection_369;
		logic_uScript_GetMissionTimerDisplayTime_Return_370 = logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_370.In(logic_uScript_GetMissionTimerDisplayTime_owner_370);
		local_367_System_Single = logic_uScript_GetMissionTimerDisplayTime_Return_370;
		if (logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_370.Out)
		{
			Relay_In_368();
		}
	}

	private void Relay_In_372()
	{
		logic_uScriptCon_CompareFloat_A_372 = local_371_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_372.In(logic_uScriptCon_CompareFloat_A_372, logic_uScriptCon_CompareFloat_B_372);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_372.LessThan)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_374()
	{
		logic_uScript_GetMissionTimerDisplayTime_owner_374 = owner_Connection_373;
		logic_uScript_GetMissionTimerDisplayTime_Return_374 = logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_374.In(logic_uScript_GetMissionTimerDisplayTime_owner_374);
		local_371_System_Single = logic_uScript_GetMissionTimerDisplayTime_Return_374;
		if (logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_374.Out)
		{
			Relay_In_372();
		}
	}

	private void Relay_In_375()
	{
		int num = 0;
		Array array = vehicleSpawnData3;
		if (logic_uScript_GetAndCheckTechs_techData_375.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_375, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_375, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_375 = owner_Connection_634;
		int num2 = 0;
		Array array2 = local_vehicleTechs3_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_375.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_375, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_375, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_375 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_375.In(logic_uScript_GetAndCheckTechs_techData_375, logic_uScript_GetAndCheckTechs_ownerNode_375, ref logic_uScript_GetAndCheckTechs_techs_375);
		local_vehicleTechs3_TankArray = logic_uScript_GetAndCheckTechs_techs_375;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_375.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_375.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_508();
		}
		if (someAlive)
		{
			Relay_AtIndex_508();
		}
	}

	private void Relay_Output1_376()
	{
		Relay_In_840();
	}

	private void Relay_Output2_376()
	{
		Relay_In_844();
	}

	private void Relay_Output3_376()
	{
		Relay_In_846();
	}

	private void Relay_Output4_376()
	{
		Relay_In_850();
	}

	private void Relay_Output5_376()
	{
	}

	private void Relay_Output6_376()
	{
	}

	private void Relay_Output7_376()
	{
	}

	private void Relay_Output8_376()
	{
	}

	private void Relay_In_376()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_376 = local_MaxPlayers_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.In(logic_uScriptCon_ManualSwitch_CurrentOutput_376);
	}

	private void Relay_In_377()
	{
		logic_uScript_LockTechSendToSCU_tech_377 = local_vehicleTech3_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_377.In(logic_uScript_LockTechSendToSCU_tech_377, logic_uScript_LockTechSendToSCU_lockSendToSCU_377);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_377.Out)
		{
			Relay_In_958();
		}
	}

	private void Relay_In_380()
	{
		logic_uScript_LockTechSendToSCU_tech_380 = local_vehicleTech2_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_380.In(logic_uScript_LockTechSendToSCU_tech_380, logic_uScript_LockTechSendToSCU_lockSendToSCU_380);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_380.Out)
		{
			Relay_In_959();
		}
	}

	private void Relay_In_384()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_384.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_384.Out)
		{
			Relay_In_512();
		}
	}

	private void Relay_In_387()
	{
		logic_uScript_LockTechSendToSCU_tech_387 = local_vehicleTech4_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_387.In(logic_uScript_LockTechSendToSCU_tech_387, logic_uScript_LockTechSendToSCU_lockSendToSCU_387);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_387.Out)
		{
			Relay_In_443();
		}
	}

	private void Relay_In_388()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_388.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_388.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_388.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_407();
		}
		if (multiplayer)
		{
			Relay_In_572();
		}
	}

	private void Relay_In_389()
	{
		int num = 0;
		Array array = msgNPCVehiclePurchased;
		if (logic_uScript_AddOnScreenMessage_locString_389.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_389, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_389, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_389 = msgTagSwitchTech;
		logic_uScript_AddOnScreenMessage_speaker_389 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_389 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_389.In(logic_uScript_AddOnScreenMessage_locString_389, logic_uScript_AddOnScreenMessage_msgPriority_389, logic_uScript_AddOnScreenMessage_holdMsg_389, logic_uScript_AddOnScreenMessage_tag_389, logic_uScript_AddOnScreenMessage_speaker_389, logic_uScript_AddOnScreenMessage_side_389);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_389.Shown)
		{
			Relay_True_551();
		}
	}

	private void Relay_In_390()
	{
		logic_uScriptCon_CompareBool_Bool_390 = local_msg03aShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_390.In(logic_uScriptCon_CompareBool_Bool_390);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_390.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_390.False;
		if (num)
		{
			Relay_In_403();
		}
		if (flag)
		{
			Relay_In_481();
		}
	}

	private void Relay_In_394()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_394 = msgTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_394.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_394, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_394);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_394.Out)
		{
			Relay_In_733();
		}
	}

	private void Relay_In_397()
	{
		logic_uScript_GetCurrentMoneyEarned_Return_397 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_397.In();
		local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_397;
		if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_397.Out)
		{
			Relay_In_615();
		}
	}

	private void Relay_In_398()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_398 = msgTagSwitchTech;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_398.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_398, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_398);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_398.Out)
		{
			Relay_In_531();
		}
	}

	private void Relay_In_399()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_399.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_399.Out)
		{
			Relay_In_616();
		}
	}

	private void Relay_InitialSpawn_400()
	{
		int num = 0;
		Array array = vehicleSpawnData4;
		if (logic_uScript_SpawnTechsFromData_spawnData_400.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_400, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_400, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_400 = owner_Connection_599;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_400.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_400, logic_uScript_SpawnTechsFromData_ownerNode_400, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_400, logic_uScript_SpawnTechsFromData_allowResurrection_400);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_400.Out)
		{
			Relay_In_846();
		}
	}

	private void Relay_In_403()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_403.In();
	}

	private void Relay_In_404()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_404.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_404, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_404, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_404 = owner_Connection_472;
		int num2 = 0;
		Array array2 = local_vehicleTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_404.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_404, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_404, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_404 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_404.In(logic_uScript_GetAndCheckTechs_techData_404, logic_uScript_GetAndCheckTechs_ownerNode_404, ref logic_uScript_GetAndCheckTechs_techs_404);
		local_vehicleTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_404;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_404.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_404.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_429();
		}
		if (someAlive)
		{
			Relay_AtIndex_429();
		}
	}

	private void Relay_In_407()
	{
		logic_uScript_IsTechPlayer_tech_407 = local_vehicleTech_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_407.In(logic_uScript_IsTechPlayer_tech_407);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_407.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_407.False;
		if (num)
		{
			Relay_In_448();
		}
		if (flag)
		{
			Relay_In_575();
		}
	}

	private void Relay_Pause_411()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_411.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_411.Out)
		{
			Relay_In_614();
		}
	}

	private void Relay_UnPause_411()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_411.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_411.Out)
		{
			Relay_In_614();
		}
	}

	private void Relay_In_413()
	{
		logic_uScript_IsTechPlayer_tech_413 = local_vehicleTech4_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_413.In(logic_uScript_IsTechPlayer_tech_413);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_413.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_413.False;
		if (num)
		{
			Relay_In_513();
		}
		if (flag)
		{
			Relay_In_460();
		}
	}

	private void Relay_In_417()
	{
		logic_uScript_LockTechSendToSCU_tech_417 = local_vehicleTech4_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_417.In(logic_uScript_LockTechSendToSCU_tech_417, logic_uScript_LockTechSendToSCU_lockSendToSCU_417);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_417.Out)
		{
			Relay_In_957();
		}
	}

	private void Relay_In_418()
	{
		logic_uScript_LockTech_tech_418 = local_vehicleTech4_Tank;
		logic_uScript_LockTech_uScript_LockTech_418.In(logic_uScript_LockTech_tech_418, logic_uScript_LockTech_lockType_418);
		if (logic_uScript_LockTech_uScript_LockTech_418.Out)
		{
			Relay_In_387();
		}
	}

	private void Relay_In_420()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_420.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_420, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_420, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_420 = owner_Connection_401;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_420.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_420, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_420, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_420 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_420.In(logic_uScript_GetAndCheckTechs_techData_420, logic_uScript_GetAndCheckTechs_ownerNode_420, ref logic_uScript_GetAndCheckTechs_techs_420);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_420;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_420.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_420.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_466();
		}
		if (someAlive)
		{
			Relay_AtIndex_466();
		}
	}

	private void Relay_InitialSpawn_423()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_423.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_423, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_423, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_423 = owner_Connection_402;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_423.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_423, logic_uScript_SpawnTechsFromData_ownerNode_423, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_423, logic_uScript_SpawnTechsFromData_allowResurrection_423);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_423.Out)
		{
			Relay_In_492();
		}
	}

	private void Relay_In_427()
	{
		logic_uScript_LockTech_tech_427 = local_vehicleTech3_Tank;
		logic_uScript_LockTech_uScript_LockTech_427.In(logic_uScript_LockTech_tech_427, logic_uScript_LockTech_lockType_427);
		if (logic_uScript_LockTech_uScript_LockTech_427.Out)
		{
			Relay_In_620();
		}
	}

	private void Relay_In_428()
	{
		logic_uScriptCon_CompareBool_Bool_428 = local_619_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_428.In(logic_uScriptCon_CompareBool_Bool_428);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_428.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_428.False;
		if (num)
		{
			Relay_In_628();
		}
		if (flag)
		{
			Relay_In_729();
		}
	}

	private void Relay_AtIndex_429()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_429.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_429, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_429, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_429.AtIndex(ref logic_uScript_AccessListTech_techList_429, logic_uScript_AccessListTech_index_429, out logic_uScript_AccessListTech_value_429);
		local_vehicleTechs_TankArray = logic_uScript_AccessListTech_techList_429;
		local_vehicleTech_Tank = logic_uScript_AccessListTech_value_429;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_429.Out)
		{
			Relay_In_432();
		}
	}

	private void Relay_In_430()
	{
		logic_uScript_GetMaxPlayers_Return_430 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_430.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_430;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_430.Out)
		{
			Relay_In_485();
		}
	}

	private void Relay_In_432()
	{
		logic_uScript_LockTechSendToSCU_tech_432 = local_vehicleTech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_432.In(logic_uScript_LockTechSendToSCU_tech_432, logic_uScript_LockTechSendToSCU_lockSendToSCU_432);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_432.Out)
		{
			Relay_True_550();
		}
	}

	private void Relay_In_433()
	{
		logic_uScript_LockTechSendToSCU_tech_433 = local_vehicleTech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_433.In(logic_uScript_LockTechSendToSCU_tech_433, logic_uScript_LockTechSendToSCU_lockSendToSCU_433);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_433.Out)
		{
			Relay_In_956();
		}
	}

	private void Relay_In_436()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_436 = TriggerNPC;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_436.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_436);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_436.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_436.OutOfRange;
		if (inRange)
		{
			Relay_In_596();
		}
		if (outOfRange)
		{
			Relay_In_553();
		}
	}

	private void Relay_In_442()
	{
		logic_uScript_HideArrow_uScript_HideArrow_442.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_442.Out)
		{
			Relay_In_451();
		}
	}

	private void Relay_In_443()
	{
		logic_uScript_LockTech_tech_443 = local_vehicleTech3_Tank;
		logic_uScript_LockTech_uScript_LockTech_443.In(logic_uScript_LockTech_tech_443, logic_uScript_LockTech_lockType_443);
		if (logic_uScript_LockTech_uScript_LockTech_443.Out)
		{
			Relay_In_427();
		}
	}

	private void Relay_In_447()
	{
		logic_uScript_LockTechSendToSCU_tech_447 = local_vehicleTech2_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_447.In(logic_uScript_LockTechSendToSCU_tech_447, logic_uScript_LockTechSendToSCU_lockSendToSCU_447);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_447.Out)
		{
			Relay_In_494();
		}
	}

	private void Relay_In_448()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_448.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_448.Out)
		{
			Relay_In_513();
		}
	}

	private void Relay_In_450()
	{
		logic_uScript_LockTech_tech_450 = local_vehicleTech2_Tank;
		logic_uScript_LockTech_uScript_LockTech_450.In(logic_uScript_LockTech_tech_450, logic_uScript_LockTech_lockType_450);
		if (logic_uScript_LockTech_uScript_LockTech_450.Out)
		{
			Relay_In_495();
		}
	}

	private void Relay_In_451()
	{
		logic_uScript_EnableGlow_targetObject_451 = local_vehicleTech_Tank;
		logic_uScript_EnableGlow_uScript_EnableGlow_451.In(logic_uScript_EnableGlow_targetObject_451, logic_uScript_EnableGlow_enable_451);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_451.Out)
		{
			Relay_In_754();
		}
	}

	private void Relay_In_453()
	{
		logic_uScriptCon_CompareBool_Bool_453 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_453.In(logic_uScriptCon_CompareBool_Bool_453);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_453.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_453.False;
		if (num)
		{
			Relay_In_566();
		}
		if (flag)
		{
			Relay_In_566();
		}
	}

	private void Relay_In_454()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_454 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_454.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_454);
		if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_454.Out)
		{
			Relay_In_588();
		}
	}

	private void Relay_In_456()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_456 = TriggerNPC;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_456.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_456);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_456.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_456.OutOfRange;
		if (inRange)
		{
			Relay_In_520();
		}
		if (outOfRange)
		{
			Relay_In_565();
		}
	}

	private void Relay_In_458()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_458.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_458.Out)
		{
			Relay_In_494();
		}
	}

	private void Relay_In_459()
	{
		logic_uScript_LockTech_tech_459 = local_vehicleTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_459.In(logic_uScript_LockTech_tech_459, logic_uScript_LockTech_lockType_459);
		if (logic_uScript_LockTech_uScript_LockTech_459.Out)
		{
			Relay_In_584();
		}
	}

	private void Relay_In_460()
	{
		logic_uScript_IsTechPlayer_tech_460 = local_vehicleTech3_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_460.In(logic_uScript_IsTechPlayer_tech_460);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_460.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_460.False;
		if (num)
		{
			Relay_In_583();
		}
		if (flag)
		{
			Relay_In_617();
		}
	}

	private void Relay_In_465()
	{
		int num = 0;
		Array array = msgLeavingEarlyPostPurchase;
		if (logic_uScript_AddOnScreenMessage_locString_465.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_465, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_465, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_465 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_465 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_465.In(logic_uScript_AddOnScreenMessage_locString_465, logic_uScript_AddOnScreenMessage_msgPriority_465, logic_uScript_AddOnScreenMessage_holdMsg_465, logic_uScript_AddOnScreenMessage_tag_465, logic_uScript_AddOnScreenMessage_speaker_465, logic_uScript_AddOnScreenMessage_side_465);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_465.Out)
		{
			Relay_True_542();
		}
	}

	private void Relay_AtIndex_466()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_466.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_466, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_466, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_466.AtIndex(ref logic_uScript_AccessListTech_techList_466, logic_uScript_AccessListTech_index_466, out logic_uScript_AccessListTech_value_466);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_466;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_466;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_466.Out)
		{
			Relay_In_557();
		}
	}

	private void Relay_Output1_467()
	{
		Relay_In_533();
	}

	private void Relay_Output2_467()
	{
		Relay_In_483();
	}

	private void Relay_Output3_467()
	{
		Relay_In_552();
	}

	private void Relay_Output4_467()
	{
		Relay_In_475();
	}

	private void Relay_Output5_467()
	{
	}

	private void Relay_Output6_467()
	{
	}

	private void Relay_Output7_467()
	{
	}

	private void Relay_Output8_467()
	{
	}

	private void Relay_In_467()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_467 = local_MaxPlayers_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_467.In(logic_uScriptCon_ManualSwitch_CurrentOutput_467);
	}

	private void Relay_In_468()
	{
		int num = 0;
		Array array = vehicleSpawnData4;
		if (logic_uScript_GetAndCheckTechs_techData_468.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_468, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_468, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_468 = owner_Connection_606;
		int num2 = 0;
		Array array2 = local_vehicleTechs4_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_468.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_468, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_468, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_468 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_468.In(logic_uScript_GetAndCheckTechs_techData_468, logic_uScript_GetAndCheckTechs_ownerNode_468, ref logic_uScript_GetAndCheckTechs_techs_468);
		local_vehicleTechs4_TankArray = logic_uScript_GetAndCheckTechs_techs_468;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_468.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_468.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_569();
		}
		if (someAlive)
		{
			Relay_AtIndex_569();
		}
	}

	private void Relay_In_475()
	{
		logic_uScript_LockTech_tech_475 = local_vehicleTech4_Tank;
		logic_uScript_LockTech_uScript_LockTech_475.In(logic_uScript_LockTech_tech_475, logic_uScript_LockTech_lockType_475);
		if (logic_uScript_LockTech_uScript_LockTech_475.Out)
		{
			Relay_In_554();
		}
	}

	private void Relay_In_481()
	{
		logic_uScript_AddMessage_messageData_481 = msgNPCPurchaseDeclined;
		logic_uScript_AddMessage_speaker_481 = SpeakerNPC;
		logic_uScript_AddMessage_Return_481 = logic_uScript_AddMessage_uScript_AddMessage_481.In(logic_uScript_AddMessage_messageData_481, logic_uScript_AddMessage_speaker_481);
		if (logic_uScript_AddMessage_uScript_AddMessage_481.Shown)
		{
			Relay_True_549();
		}
	}

	private void Relay_In_483()
	{
		logic_uScript_LockTech_tech_483 = local_vehicleTech2_Tank;
		logic_uScript_LockTech_uScript_LockTech_483.In(logic_uScript_LockTech_tech_483, logic_uScript_LockTech_lockType_483);
		if (logic_uScript_LockTech_uScript_LockTech_483.Out)
		{
			Relay_In_489();
		}
	}

	private void Relay_Output1_485()
	{
	}

	private void Relay_Output2_485()
	{
		Relay_In_617();
	}

	private void Relay_Output3_485()
	{
		Relay_In_460();
	}

	private void Relay_Output4_485()
	{
		Relay_In_413();
	}

	private void Relay_Output5_485()
	{
	}

	private void Relay_Output6_485()
	{
	}

	private void Relay_Output7_485()
	{
	}

	private void Relay_Output8_485()
	{
	}

	private void Relay_In_485()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_485 = local_MaxPlayers_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.In(logic_uScriptCon_ManualSwitch_CurrentOutput_485);
	}

	private void Relay_In_489()
	{
		logic_uScript_LockTech_tech_489 = local_vehicleTech2_Tank;
		logic_uScript_LockTech_uScript_LockTech_489.In(logic_uScript_LockTech_tech_489, logic_uScript_LockTech_lockType_489);
		if (logic_uScript_LockTech_uScript_LockTech_489.Out)
		{
			Relay_In_380();
		}
	}

	private void Relay_In_492()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_492.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_492.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_492.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_840();
		}
		if (multiplayer)
		{
			Relay_In_560();
		}
	}

	private void Relay_In_493()
	{
		logic_uScript_LockTechSendToSCU_tech_493 = local_vehicleTech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_493.In(logic_uScript_LockTechSendToSCU_tech_493, logic_uScript_LockTechSendToSCU_lockSendToSCU_493);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_493.Out)
		{
			Relay_In_503();
		}
	}

	private void Relay_In_494()
	{
		logic_uScript_SetEncounterTarget_owner_494 = owner_Connection_392;
		logic_uScript_SetEncounterTarget_visibleObject_494 = local_vehicleTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_494.In(logic_uScript_SetEncounterTarget_owner_494, logic_uScript_SetEncounterTarget_visibleObject_494);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_494.Out)
		{
			Relay_In_592();
		}
	}

	private void Relay_In_495()
	{
		logic_uScript_LockTech_tech_495 = local_vehicleTech2_Tank;
		logic_uScript_LockTech_uScript_LockTech_495.In(logic_uScript_LockTech_tech_495, logic_uScript_LockTech_lockType_495);
		if (logic_uScript_LockTech_uScript_LockTech_495.Out)
		{
			Relay_In_447();
		}
	}

	private void Relay_In_496()
	{
		logic_uScript_EnableGlow_targetObject_496 = local_vehicleTech_Tank;
		logic_uScript_EnableGlow_uScript_EnableGlow_496.In(logic_uScript_EnableGlow_targetObject_496, logic_uScript_EnableGlow_enable_496);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_496.Out)
		{
			Relay_In_545();
		}
	}

	private void Relay_In_497()
	{
		logic_uScript_LockTech_tech_497 = local_vehicleTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_497.In(logic_uScript_LockTech_tech_497, logic_uScript_LockTech_lockType_497);
		if (logic_uScript_LockTech_uScript_LockTech_497.Out)
		{
			Relay_In_546();
		}
	}

	private void Relay_In_498()
	{
		logic_uScript_LockTech_tech_498 = local_vehicleTech3_Tank;
		logic_uScript_LockTech_uScript_LockTech_498.In(logic_uScript_LockTech_tech_498, logic_uScript_LockTech_lockType_498);
		if (logic_uScript_LockTech_uScript_LockTech_498.Out)
		{
			Relay_In_377();
		}
	}

	private void Relay_True_499()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_499.True(out logic_uScriptAct_SetBool_Target_499);
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_uScriptAct_SetBool_Target_499;
	}

	private void Relay_False_499()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_499.False(out logic_uScriptAct_SetBool_Target_499);
		local_ShownMsgLeavingEarlyPrePurchase_System_Boolean = logic_uScriptAct_SetBool_Target_499;
	}

	private void Relay_In_502()
	{
		int num = 0;
		Array array = msgLeavingEarlyPrePurchase;
		if (logic_uScript_AddOnScreenMessage_locString_502.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_502, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_502, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_502 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_502 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_502.In(logic_uScript_AddOnScreenMessage_locString_502, logic_uScript_AddOnScreenMessage_msgPriority_502, logic_uScript_AddOnScreenMessage_holdMsg_502, logic_uScript_AddOnScreenMessage_tag_502, logic_uScript_AddOnScreenMessage_speaker_502, logic_uScript_AddOnScreenMessage_side_502);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_502.Out)
		{
			Relay_True_499();
		}
	}

	private void Relay_In_503()
	{
		logic_uScript_PointArrowAtVisible_targetObject_503 = local_vehicleTech_Tank;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_503.In(logic_uScript_PointArrowAtVisible_targetObject_503, logic_uScript_PointArrowAtVisible_timeToShowFor_503, logic_uScript_PointArrowAtVisible_offset_503);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_503.Out)
		{
			Relay_In_496();
		}
	}

	private void Relay_In_504()
	{
		logic_uScript_LockTech_tech_504 = local_vehicleTech4_Tank;
		logic_uScript_LockTech_uScript_LockTech_504.In(logic_uScript_LockTech_tech_504, logic_uScript_LockTech_lockType_504);
		if (logic_uScript_LockTech_uScript_LockTech_504.Out)
		{
			Relay_In_418();
		}
	}

	private void Relay_ResponseEvent_506()
	{
		local_635_TankBlock = event_UnityEngine_GameObject_TankBlock_506;
		local_619_System_Boolean = event_UnityEngine_GameObject_Accepted_506;
		Relay_In_1093();
	}

	private void Relay_AtIndex_507()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_507.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_507, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_507, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_507.AtIndex(ref logic_uScript_AccessListTech_techList_507, logic_uScript_AccessListTech_index_507, out logic_uScript_AccessListTech_value_507);
		local_vehicleTechs_TankArray = logic_uScript_AccessListTech_techList_507;
		local_vehicleTech_Tank = logic_uScript_AccessListTech_value_507;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_507.Out)
		{
			Relay_In_388();
		}
	}

	private void Relay_AtIndex_508()
	{
		int num = 0;
		Array array = local_vehicleTechs3_TankArray;
		if (logic_uScript_AccessListTech_techList_508.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_508, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_508, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_508.AtIndex(ref logic_uScript_AccessListTech_techList_508, logic_uScript_AccessListTech_index_508, out logic_uScript_AccessListTech_value_508);
		local_vehicleTechs3_TankArray = logic_uScript_AccessListTech_techList_508;
		local_vehicleTech3_Tank = logic_uScript_AccessListTech_value_508;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_508.Out)
		{
			Relay_In_511();
		}
	}

	private void Relay_In_511()
	{
		int num = 0;
		Array array = vehicleSpawnData2;
		if (logic_uScript_GetAndCheckTechs_techData_511.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_511, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_511, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_511 = owner_Connection_562;
		int num2 = 0;
		Array array2 = local_vehicleTechs2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_511.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_511, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_511, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_511 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_511.In(logic_uScript_GetAndCheckTechs_techData_511, logic_uScript_GetAndCheckTechs_ownerNode_511, ref logic_uScript_GetAndCheckTechs_techs_511);
		local_vehicleTechs2_TankArray = logic_uScript_GetAndCheckTechs_techs_511;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_511.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_511.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_532();
		}
		if (someAlive)
		{
			Relay_AtIndex_532();
		}
	}

	private void Relay_In_512()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_512.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_512.Out)
		{
			Relay_In_456();
		}
	}

	private void Relay_In_513()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_513.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_513.Out)
		{
			Relay_In_583();
		}
	}

	private void Relay_In_514()
	{
		logic_uScriptCon_CompareBool_Bool_514 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514.In(logic_uScriptCon_CompareBool_Bool_514);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514.False;
		if (num)
		{
			Relay_In_399();
		}
		if (flag)
		{
			Relay_In_1053();
		}
	}

	private void Relay_In_515()
	{
		logic_uScript_HideArrow_uScript_HideArrow_515.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_515.Out)
		{
			Relay_In_454();
		}
	}

	private void Relay_In_518()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_518.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_518.Out)
		{
			Relay_In_453();
		}
	}

	private void Relay_In_520()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_520.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_520, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_520, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_520 = owner_Connection_470;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_520.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_520, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_520, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_520 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_520.In(logic_uScript_GetAndCheckTechs_techData_520, logic_uScript_GetAndCheckTechs_ownerNode_520, ref logic_uScript_GetAndCheckTechs_techs_520);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_520;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_520.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_520.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_578();
		}
		if (someAlive)
		{
			Relay_AtIndex_578();
		}
	}

	private void Relay_In_521()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_521.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_521.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_521.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_398();
		}
		if (multiplayer)
		{
			Relay_In_547();
		}
	}

	private void Relay_In_522()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_522.In();
	}

	private void Relay_In_523()
	{
		logic_uScript_AddMessage_messageData_523 = msgNPCNotEnoughMoney;
		logic_uScript_AddMessage_speaker_523 = SpeakerNPC;
		logic_uScript_AddMessage_Return_523 = logic_uScript_AddMessage_uScript_AddMessage_523.In(logic_uScript_AddMessage_messageData_523, logic_uScript_AddMessage_speaker_523);
		if (logic_uScript_AddMessage_uScript_AddMessage_523.Shown)
		{
			Relay_True_535();
		}
	}

	private void Relay_In_526()
	{
		logic_uScriptCon_CompareBool_Bool_526 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_526.In(logic_uScriptCon_CompareBool_Bool_526);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_526.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_526.False;
		if (num)
		{
			Relay_In_573();
		}
		if (flag)
		{
			Relay_In_573();
		}
	}

	private void Relay_In_527()
	{
		logic_uScript_CompareBlock_A_527 = local_635_TankBlock;
		logic_uScript_CompareBlock_B_527 = local_TerminalBlock_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_527.In(logic_uScript_CompareBlock_A_527, logic_uScript_CompareBlock_B_527);
		if (logic_uScript_CompareBlock_uScript_CompareBlock_527.EqualTo)
		{
			Relay_In_428();
		}
	}

	private void Relay_In_528()
	{
		logic_uScriptCon_CompareBool_Bool_528 = _DEBUGIgnoreMoneyCheck;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_528.In(logic_uScriptCon_CompareBool_Bool_528);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_528.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_528.False;
		if (num)
		{
			Relay_In_518();
		}
		if (flag)
		{
			Relay_In_397();
		}
	}

	private void Relay_In_529()
	{
		logic_uScriptCon_CompareBool_Bool_529 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_529.In(logic_uScriptCon_CompareBool_Bool_529);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_529.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_529.False;
		if (num)
		{
			Relay_In_539();
		}
		if (flag)
		{
			Relay_In_389();
		}
	}

	private void Relay_In_531()
	{
		logic_uScript_ClearEncounterTarget_owner_531 = owner_Connection_419;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_531.In(logic_uScript_ClearEncounterTarget_owner_531);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_531.Out)
		{
			Relay_In_442();
		}
	}

	private void Relay_AtIndex_532()
	{
		int num = 0;
		Array array = local_vehicleTechs2_TankArray;
		if (logic_uScript_AccessListTech_techList_532.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_532, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_532, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_532.AtIndex(ref logic_uScript_AccessListTech_techList_532, logic_uScript_AccessListTech_index_532, out logic_uScript_AccessListTech_value_532);
		local_vehicleTechs2_TankArray = logic_uScript_AccessListTech_techList_532;
		local_vehicleTech2_Tank = logic_uScript_AccessListTech_value_532;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_532.Out)
		{
			Relay_In_407();
		}
	}

	private void Relay_In_533()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_533.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_533.Out)
		{
			Relay_In_398();
		}
	}

	private void Relay_True_535()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_535.True(out logic_uScriptAct_SetBool_Target_535);
		local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_535;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_535.Out)
		{
			Relay_In_522();
		}
	}

	private void Relay_False_535()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_535.False(out logic_uScriptAct_SetBool_Target_535);
		local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_535;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_535.Out)
		{
			Relay_In_522();
		}
	}

	private void Relay_In_537()
	{
		logic_uScriptCon_CompareBool_Bool_537 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_537.In(logic_uScriptCon_CompareBool_Bool_537);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_537.False)
		{
			Relay_In_465();
		}
	}

	private void Relay_Out_539()
	{
	}

	private void Relay_Shown_539()
	{
	}

	private void Relay_In_539()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_539 = msgSwitchTech;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_539 = msgSwitchTech_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_539 = SpeakerNPC;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_539.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_539, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_539, logic_SubGraph_AddMessageWithPadSupport_speaker_539);
	}

	private void Relay_True_541()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_541.True(out logic_uScriptAct_SetBool_Target_541);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_541;
	}

	private void Relay_False_541()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_541.False(out logic_uScriptAct_SetBool_Target_541);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_541;
	}

	private void Relay_True_542()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_542.True(out logic_uScriptAct_SetBool_Target_542);
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_uScriptAct_SetBool_Target_542;
	}

	private void Relay_False_542()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_542.False(out logic_uScriptAct_SetBool_Target_542);
		local_ShownMsgLeavingEarlyPostPurchase_System_Boolean = logic_uScriptAct_SetBool_Target_542;
	}

	private void Relay_In_545()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_545.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_545.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_545.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_494();
		}
		if (multiplayer)
		{
			Relay_In_625();
		}
	}

	private void Relay_In_546()
	{
		logic_uScript_LockTech_tech_546 = local_vehicleTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_546.In(logic_uScript_LockTech_tech_546, logic_uScript_LockTech_lockType_546);
		if (logic_uScript_LockTech_uScript_LockTech_546.Out)
		{
			Relay_In_433();
		}
	}

	private void Relay_In_547()
	{
		logic_uScript_GetMaxPlayers_Return_547 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_547.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_547;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_547.Out)
		{
			Relay_In_467();
		}
	}

	private void Relay_In_548()
	{
		logic_uScriptCon_CompareBool_Bool_548 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_548.In(logic_uScriptCon_CompareBool_Bool_548);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_548.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_548.False;
		if (num)
		{
			Relay_In_646();
		}
		if (flag)
		{
			Relay_In_645();
		}
	}

	private void Relay_True_549()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_549.True(out logic_uScriptAct_SetBool_Target_549);
		local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_549;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_549.Out)
		{
			Relay_In_403();
		}
	}

	private void Relay_False_549()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_549.False(out logic_uScriptAct_SetBool_Target_549);
		local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_549;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_549.Out)
		{
			Relay_In_403();
		}
	}

	private void Relay_True_550()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_550.True(out logic_uScriptAct_SetBool_Target_550);
		local_VehicleSetup_System_Boolean = logic_uScriptAct_SetBool_Target_550;
	}

	private void Relay_False_550()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_550.False(out logic_uScriptAct_SetBool_Target_550);
		local_VehicleSetup_System_Boolean = logic_uScriptAct_SetBool_Target_550;
	}

	private void Relay_True_551()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_551.True(out logic_uScriptAct_SetBool_Target_551);
		local_SaidMsgNPCVehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_551;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_551.Out)
		{
			Relay_In_539();
		}
	}

	private void Relay_False_551()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_551.False(out logic_uScriptAct_SetBool_Target_551);
		local_SaidMsgNPCVehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_551;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_551.Out)
		{
			Relay_In_539();
		}
	}

	private void Relay_In_552()
	{
		logic_uScript_LockTech_tech_552 = local_vehicleTech3_Tank;
		logic_uScript_LockTech_uScript_LockTech_552.In(logic_uScript_LockTech_tech_552, logic_uScript_LockTech_lockType_552);
		if (logic_uScript_LockTech_uScript_LockTech_552.Out)
		{
			Relay_In_498();
		}
	}

	private void Relay_In_553()
	{
		logic_uScript_HideArrow_uScript_HideArrow_553.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_553.Out)
		{
			Relay_UnPause_411();
		}
	}

	private void Relay_In_554()
	{
		logic_uScript_LockTech_tech_554 = local_vehicleTech4_Tank;
		logic_uScript_LockTech_uScript_LockTech_554.In(logic_uScript_LockTech_tech_554, logic_uScript_LockTech_lockType_554);
		if (logic_uScript_LockTech_uScript_LockTech_554.Out)
		{
			Relay_In_417();
		}
	}

	private void Relay_In_555()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_555.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_555, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_555, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_555 = owner_Connection_424;
		int num2 = 0;
		Array array2 = local_vehicleTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_555.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_555, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_555, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_555 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_555.In(logic_uScript_GetAndCheckTechs_techData_555, logic_uScript_GetAndCheckTechs_ownerNode_555, ref logic_uScript_GetAndCheckTechs_techs_555);
		local_vehicleTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_555;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_555.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_555.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_507();
		}
		if (someAlive)
		{
			Relay_AtIndex_507();
		}
	}

	private void Relay_In_557()
	{
		logic_uScript_GetTankBlock_tank_557 = local_PaymentPointTech_Tank;
		logic_uScript_GetTankBlock_blockType_557 = interactableBlockType;
		logic_uScript_GetTankBlock_Return_557 = logic_uScript_GetTankBlock_uScript_GetTankBlock_557.In(logic_uScript_GetTankBlock_tank_557, logic_uScript_GetTankBlock_blockType_557);
		local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_557;
		bool num = logic_uScript_GetTankBlock_uScript_GetTankBlock_557.Out;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_557.Returned;
		if (num)
		{
			Relay_In_561();
		}
		if (returned)
		{
			Relay_In_561();
		}
	}

	private void Relay_In_559()
	{
		logic_uScript_SetEncounterTarget_owner_559 = owner_Connection_385;
		logic_uScript_SetEncounterTarget_visibleObject_559 = local_PaymentPointTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_559.In(logic_uScript_SetEncounterTarget_owner_559, logic_uScript_SetEncounterTarget_visibleObject_559);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_559.Out)
		{
			Relay_In_514();
		}
	}

	private void Relay_In_560()
	{
		logic_uScript_GetMaxPlayers_Return_560 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_560.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_560;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_560.Out)
		{
			Relay_In_376();
		}
	}

	private void Relay_In_561()
	{
		logic_uScriptCon_CompareBool_Bool_561 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_561.In(logic_uScriptCon_CompareBool_Bool_561);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_561.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_561.False;
		if (num)
		{
			Relay_In_555();
		}
		if (flag)
		{
			Relay_In_384();
		}
	}

	private void Relay_In_563()
	{
		logic_uScriptCon_CompareBool_Bool_563 = local_msg03bShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_563.In(logic_uScriptCon_CompareBool_Bool_563);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_563.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_563.False;
		if (num)
		{
			Relay_In_522();
		}
		if (flag)
		{
			Relay_In_523();
		}
	}

	private void Relay_In_565()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_565.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_565.Out)
		{
			Relay_In_632();
		}
	}

	private void Relay_In_566()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_566 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_566 = msgPromptAccept;
		logic_uScript_MissionPromptBlock_Show_rejectButtonText_566 = msgPromptDecline;
		logic_uScript_MissionPromptBlock_Show_targetBlock_566 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_566.In(logic_uScript_MissionPromptBlock_Show_bodyText_566, logic_uScript_MissionPromptBlock_Show_acceptButtonText_566, logic_uScript_MissionPromptBlock_Show_rejectButtonText_566, logic_uScript_MissionPromptBlock_Show_targetBlock_566, logic_uScript_MissionPromptBlock_Show_highlightBlock_566, logic_uScript_MissionPromptBlock_Show_singleUse_566);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_566.Out)
		{
			Relay_True_541();
		}
	}

	private void Relay_In_567()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_567.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_567.Out)
		{
			Relay_In_459();
		}
	}

	private void Relay_InitialSpawn_568()
	{
		int num = 0;
		Array array = vehicleSpawnData2;
		if (logic_uScript_SpawnTechsFromData_spawnData_568.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_568, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_568, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_568 = owner_Connection_556;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_568.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_568, logic_uScript_SpawnTechsFromData_ownerNode_568, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_568, logic_uScript_SpawnTechsFromData_allowResurrection_568);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_568.Out)
		{
			Relay_In_840();
		}
	}

	private void Relay_AtIndex_569()
	{
		int num = 0;
		Array array = local_vehicleTechs4_TankArray;
		if (logic_uScript_AccessListTech_techList_569.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_569, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_569, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_569.AtIndex(ref logic_uScript_AccessListTech_techList_569, logic_uScript_AccessListTech_index_569, out logic_uScript_AccessListTech_value_569);
		local_vehicleTechs4_TankArray = logic_uScript_AccessListTech_techList_569;
		local_vehicleTech4_Tank = logic_uScript_AccessListTech_value_569;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_569.Out)
		{
			Relay_In_375();
		}
	}

	private void Relay_Out_571()
	{
		Relay_True_741();
	}

	private void Relay_In_571()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_571 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_571.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_571, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_571);
	}

	private void Relay_In_572()
	{
		logic_uScript_GetMaxPlayers_Return_572 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_572.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_572;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_572.Out)
		{
			Relay_In_574();
		}
	}

	private void Relay_In_573()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_573 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_573 = msgPromptNoMoney;
		logic_uScript_MissionPromptBlock_Show_targetBlock_573 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_573.In(logic_uScript_MissionPromptBlock_Show_bodyText_573, logic_uScript_MissionPromptBlock_Show_acceptButtonText_573, logic_uScript_MissionPromptBlock_Show_rejectButtonText_573, logic_uScript_MissionPromptBlock_Show_targetBlock_573, logic_uScript_MissionPromptBlock_Show_highlightBlock_573, logic_uScript_MissionPromptBlock_Show_singleUse_573);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_573.Out)
		{
			Relay_False_541();
		}
	}

	private void Relay_Output1_574()
	{
		Relay_In_608();
	}

	private void Relay_Output2_574()
	{
		Relay_In_511();
	}

	private void Relay_Output3_574()
	{
		Relay_In_375();
	}

	private void Relay_Output4_574()
	{
		Relay_In_468();
	}

	private void Relay_Output5_574()
	{
	}

	private void Relay_Output6_574()
	{
	}

	private void Relay_Output7_574()
	{
	}

	private void Relay_Output8_574()
	{
	}

	private void Relay_In_574()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_574 = local_MaxPlayers_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_574.In(logic_uScriptCon_ManualSwitch_CurrentOutput_574);
	}

	private void Relay_In_575()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_575.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_575.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_575.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_567();
		}
		if (multiplayer)
		{
			Relay_In_430();
		}
	}

	private void Relay_AtIndex_578()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_578.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_578, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_578, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_578.AtIndex(ref logic_uScript_AccessListTech_techList_578, logic_uScript_AccessListTech_index_578, out logic_uScript_AccessListTech_value_578);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_578;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_578;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_578.Out)
		{
			Relay_In_559();
		}
	}

	private void Relay_InitialSpawn_579()
	{
		int num = 0;
		Array array = vehicleSpawnData3;
		if (logic_uScript_SpawnTechsFromData_spawnData_579.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_579, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_579, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_579 = owner_Connection_538;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_579.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_579, logic_uScript_SpawnTechsFromData_ownerNode_579, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_579, logic_uScript_SpawnTechsFromData_allowResurrection_579);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_579.Out)
		{
			Relay_In_844();
		}
	}

	private void Relay_In_580()
	{
		logic_uScriptCon_CompareBool_Bool_580 = local_NPCMet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_580.In(logic_uScriptCon_CompareBool_Bool_580);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_580.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_580.False;
		if (num)
		{
			Relay_In_420();
		}
		if (flag)
		{
			Relay_In_254();
		}
	}

	private void Relay_In_583()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_583.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_583.Out)
		{
			Relay_In_497();
		}
	}

	private void Relay_In_584()
	{
		logic_uScript_LockTech_tech_584 = local_vehicleTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_584.In(logic_uScript_LockTech_tech_584, logic_uScript_LockTech_lockType_584);
		if (logic_uScript_LockTech_uScript_LockTech_584.Out)
		{
			Relay_In_493();
		}
	}

	private void Relay_In_586()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_586 = msgTagPurchase;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_586.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_586, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_586);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_586.Out)
		{
			Relay_In_616();
		}
	}

	private void Relay_In_588()
	{
		int num = 0;
		Array array = discoverableBlockTypesOnVehicle;
		if (logic_uScript_DiscoverBlocks_blockTypes_588.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DiscoverBlocks_blockTypes_588, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DiscoverBlocks_blockTypes_588, num, array.Length);
		num += array.Length;
		logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_588.In(logic_uScript_DiscoverBlocks_blockTypes_588);
		if (logic_uScript_DiscoverBlocks_uScript_DiscoverBlocks_588.Out)
		{
			Relay_In_743();
		}
	}

	private void Relay_In_592()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_592 = TriggerNPC;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_592.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_592);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_592.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_592.OutOfRange;
		if (inRange)
		{
			Relay_In_529();
		}
		if (outOfRange)
		{
			Relay_In_537();
		}
	}

	private void Relay_In_596()
	{
		logic_uScript_PointArrowAtVisible_targetObject_596 = local_TerminalBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_596.In(logic_uScript_PointArrowAtVisible_targetObject_596, logic_uScript_PointArrowAtVisible_timeToShowFor_596, logic_uScript_PointArrowAtVisible_offset_596);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_596.Out)
		{
			Relay_In_394();
		}
	}

	private void Relay_Output1_604()
	{
		Relay_In_458();
	}

	private void Relay_Output2_604()
	{
		Relay_In_450();
	}

	private void Relay_Output3_604()
	{
		Relay_In_443();
	}

	private void Relay_Output4_604()
	{
		Relay_In_504();
	}

	private void Relay_Output5_604()
	{
	}

	private void Relay_Output6_604()
	{
	}

	private void Relay_Output7_604()
	{
	}

	private void Relay_Output8_604()
	{
	}

	private void Relay_In_604()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_604 = local_MaxPlayers_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_604.In(logic_uScriptCon_ManualSwitch_CurrentOutput_604);
	}

	private void Relay_In_608()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_608.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_608.Out)
		{
			Relay_In_407();
		}
	}

	private void Relay_In_614()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_614 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_614.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_614);
	}

	private void Relay_In_615()
	{
		logic_uScriptCon_CompareInt_A_615 = local_CurrentMoney_System_Int32;
		logic_uScriptCon_CompareInt_B_615 = vehicleCost;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_615.In(logic_uScriptCon_CompareInt_A_615, logic_uScriptCon_CompareInt_B_615);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_615.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_615.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_453();
		}
		if (lessThan)
		{
			Relay_In_526();
		}
	}

	private void Relay_In_616()
	{
		logic_uScriptCon_CompareBool_Bool_616 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_616.In(logic_uScriptCon_CompareBool_Bool_616);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_616.False)
		{
			Relay_In_436();
		}
	}

	private void Relay_In_617()
	{
		logic_uScript_IsTechPlayer_tech_617 = local_vehicleTech2_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_617.In(logic_uScript_IsTechPlayer_tech_617);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_617.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_617.False;
		if (num)
		{
			Relay_In_497();
		}
		if (flag)
		{
			Relay_In_459();
		}
	}

	private void Relay_In_620()
	{
		logic_uScript_LockTechSendToSCU_tech_620 = local_vehicleTech3_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_620.In(logic_uScript_LockTechSendToSCU_tech_620, logic_uScript_LockTechSendToSCU_lockSendToSCU_620);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_620.Out)
		{
			Relay_In_450();
		}
	}

	private void Relay_Out_622()
	{
	}

	private void Relay_Shown_622()
	{
		Relay_In_616();
	}

	private void Relay_In_622()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_622 = msgPurchaseVehicle;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_622 = msgPurchaseVehicle_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_622 = SpeakerNPC;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_622.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_622, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_622, logic_SubGraph_AddMessageWithPadSupport_speaker_622);
	}

	private void Relay_In_625()
	{
		logic_uScript_GetMaxPlayers_Return_625 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_625.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_625;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_625.Out)
		{
			Relay_In_604();
		}
	}

	private void Relay_In_628()
	{
		logic_uScriptCon_CompareBool_Bool_628 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_628.In(logic_uScriptCon_CompareBool_Bool_628);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_628.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_628.False;
		if (num)
		{
			Relay_In_830();
		}
		if (flag)
		{
			Relay_In_736();
		}
	}

	private void Relay_Out_629()
	{
		Relay_True_755();
	}

	private void Relay_In_629()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_629 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_629.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_629, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_629);
	}

	private void Relay_In_632()
	{
		logic_uScriptCon_CompareBool_Bool_632 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_632.In(logic_uScriptCon_CompareBool_Bool_632);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_632.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_632.False;
		if (num)
		{
			Relay_In_1055();
		}
		if (flag)
		{
			Relay_In_502();
		}
	}

	private void Relay_True_637()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_637.True(out logic_uScriptAct_SetBool_Target_637);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_637;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_637.Out)
		{
			Relay_In_243();
		}
	}

	private void Relay_False_637()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_637.False(out logic_uScriptAct_SetBool_Target_637);
		local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_637;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_637.Out)
		{
			Relay_In_243();
		}
	}

	private void Relay_In_639()
	{
		logic_uScriptCon_CompareBool_Bool_639 = local_NPCSeen_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_639.In(logic_uScriptCon_CompareBool_Bool_639);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_639.True)
		{
			Relay_In_721();
		}
	}

	private void Relay_In_645()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_645.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_645.Out)
		{
			Relay_In_580();
		}
	}

	private void Relay_In_646()
	{
		logic_uScriptCon_CompareBool_Bool_646 = local_VehicleSetup_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_646.In(logic_uScriptCon_CompareBool_Bool_646);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_646.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_646.False;
		if (num)
		{
			Relay_In_580();
		}
		if (flag)
		{
			Relay_In_404();
		}
	}

	private void Relay_In_649()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_649.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_649.Out)
		{
			Relay_In_650();
		}
	}

	private void Relay_In_650()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_650.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_650.Out)
		{
			Relay_In_548();
		}
	}

	private void Relay_Save_Out_661()
	{
		Relay_Save_662();
	}

	private void Relay_Load_Out_661()
	{
		Relay_Load_662();
	}

	private void Relay_Restart_Out_661()
	{
		Relay_Set_False_662();
	}

	private void Relay_Save_661()
	{
		logic_SubGraph_SaveLoadBool_boolean_661 = local_SwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_661 = local_SwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Save(ref logic_SubGraph_SaveLoadBool_boolean_661, logic_SubGraph_SaveLoadBool_boolAsVariable_661, logic_SubGraph_SaveLoadBool_uniqueID_661);
	}

	private void Relay_Load_661()
	{
		logic_SubGraph_SaveLoadBool_boolean_661 = local_SwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_661 = local_SwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Load(ref logic_SubGraph_SaveLoadBool_boolean_661, logic_SubGraph_SaveLoadBool_boolAsVariable_661, logic_SubGraph_SaveLoadBool_uniqueID_661);
	}

	private void Relay_Set_True_661()
	{
		logic_SubGraph_SaveLoadBool_boolean_661 = local_SwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_661 = local_SwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_661, logic_SubGraph_SaveLoadBool_boolAsVariable_661, logic_SubGraph_SaveLoadBool_uniqueID_661);
	}

	private void Relay_Set_False_661()
	{
		logic_SubGraph_SaveLoadBool_boolean_661 = local_SwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_661 = local_SwitchedTech_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_661.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_661, logic_SubGraph_SaveLoadBool_boolAsVariable_661, logic_SubGraph_SaveLoadBool_uniqueID_661);
	}

	private void Relay_Save_Out_662()
	{
		Relay_Save_663();
	}

	private void Relay_Load_Out_662()
	{
		Relay_Load_663();
	}

	private void Relay_Restart_Out_662()
	{
		Relay_Set_False_663();
	}

	private void Relay_Save_662()
	{
		logic_SubGraph_SaveLoadBool_boolean_662 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_662 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Save(ref logic_SubGraph_SaveLoadBool_boolean_662, logic_SubGraph_SaveLoadBool_boolAsVariable_662, logic_SubGraph_SaveLoadBool_uniqueID_662);
	}

	private void Relay_Load_662()
	{
		logic_SubGraph_SaveLoadBool_boolean_662 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_662 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Load(ref logic_SubGraph_SaveLoadBool_boolean_662, logic_SubGraph_SaveLoadBool_boolAsVariable_662, logic_SubGraph_SaveLoadBool_uniqueID_662);
	}

	private void Relay_Set_True_662()
	{
		logic_SubGraph_SaveLoadBool_boolean_662 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_662 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_662, logic_SubGraph_SaveLoadBool_boolAsVariable_662, logic_SubGraph_SaveLoadBool_uniqueID_662);
	}

	private void Relay_Set_False_662()
	{
		logic_SubGraph_SaveLoadBool_boolean_662 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_662 = local_VehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_662.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_662, logic_SubGraph_SaveLoadBool_boolAsVariable_662, logic_SubGraph_SaveLoadBool_uniqueID_662);
	}

	private void Relay_Save_Out_663()
	{
		Relay_Save_664();
	}

	private void Relay_Load_Out_663()
	{
		Relay_Load_664();
	}

	private void Relay_Restart_Out_663()
	{
		Relay_Set_False_664();
	}

	private void Relay_Save_663()
	{
		logic_SubGraph_SaveLoadBool_boolean_663 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_663 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Save(ref logic_SubGraph_SaveLoadBool_boolean_663, logic_SubGraph_SaveLoadBool_boolAsVariable_663, logic_SubGraph_SaveLoadBool_uniqueID_663);
	}

	private void Relay_Load_663()
	{
		logic_SubGraph_SaveLoadBool_boolean_663 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_663 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Load(ref logic_SubGraph_SaveLoadBool_boolean_663, logic_SubGraph_SaveLoadBool_boolAsVariable_663, logic_SubGraph_SaveLoadBool_uniqueID_663);
	}

	private void Relay_Set_True_663()
	{
		logic_SubGraph_SaveLoadBool_boolean_663 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_663 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_663, logic_SubGraph_SaveLoadBool_boolAsVariable_663, logic_SubGraph_SaveLoadBool_uniqueID_663);
	}

	private void Relay_Set_False_663()
	{
		logic_SubGraph_SaveLoadBool_boolean_663 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_663 = local_VehicleSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_663.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_663, logic_SubGraph_SaveLoadBool_boolAsVariable_663, logic_SubGraph_SaveLoadBool_uniqueID_663);
	}

	private void Relay_Save_Out_664()
	{
		Relay_Save_665();
	}

	private void Relay_Load_Out_664()
	{
		Relay_Load_665();
	}

	private void Relay_Restart_Out_664()
	{
		Relay_Set_False_665();
	}

	private void Relay_Save_664()
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_664 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Save(ref logic_SubGraph_SaveLoadBool_boolean_664, logic_SubGraph_SaveLoadBool_boolAsVariable_664, logic_SubGraph_SaveLoadBool_uniqueID_664);
	}

	private void Relay_Load_664()
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_664 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Load(ref logic_SubGraph_SaveLoadBool_boolean_664, logic_SubGraph_SaveLoadBool_boolAsVariable_664, logic_SubGraph_SaveLoadBool_uniqueID_664);
	}

	private void Relay_Set_True_664()
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_664 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_664, logic_SubGraph_SaveLoadBool_boolAsVariable_664, logic_SubGraph_SaveLoadBool_uniqueID_664);
	}

	private void Relay_Set_False_664()
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_664 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_664, logic_SubGraph_SaveLoadBool_boolAsVariable_664, logic_SubGraph_SaveLoadBool_uniqueID_664);
	}

	private void Relay_Save_Out_665()
	{
		Relay_Save_666();
	}

	private void Relay_Load_Out_665()
	{
		Relay_Load_666();
	}

	private void Relay_Restart_Out_665()
	{
		Relay_Set_False_666();
	}

	private void Relay_Save_665()
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_665 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Save(ref logic_SubGraph_SaveLoadBool_boolean_665, logic_SubGraph_SaveLoadBool_boolAsVariable_665, logic_SubGraph_SaveLoadBool_uniqueID_665);
	}

	private void Relay_Load_665()
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_665 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Load(ref logic_SubGraph_SaveLoadBool_boolean_665, logic_SubGraph_SaveLoadBool_boolAsVariable_665, logic_SubGraph_SaveLoadBool_uniqueID_665);
	}

	private void Relay_Set_True_665()
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_665 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_665, logic_SubGraph_SaveLoadBool_boolAsVariable_665, logic_SubGraph_SaveLoadBool_uniqueID_665);
	}

	private void Relay_Set_False_665()
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_665 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_665, logic_SubGraph_SaveLoadBool_boolAsVariable_665, logic_SubGraph_SaveLoadBool_uniqueID_665);
	}

	private void Relay_Save_Out_666()
	{
		Relay_Save_667();
	}

	private void Relay_Load_Out_666()
	{
		Relay_Load_667();
	}

	private void Relay_Restart_Out_666()
	{
		Relay_Set_False_667();
	}

	private void Relay_Save_666()
	{
		logic_SubGraph_SaveLoadBool_boolean_666 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_666 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Save(ref logic_SubGraph_SaveLoadBool_boolean_666, logic_SubGraph_SaveLoadBool_boolAsVariable_666, logic_SubGraph_SaveLoadBool_uniqueID_666);
	}

	private void Relay_Load_666()
	{
		logic_SubGraph_SaveLoadBool_boolean_666 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_666 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Load(ref logic_SubGraph_SaveLoadBool_boolean_666, logic_SubGraph_SaveLoadBool_boolAsVariable_666, logic_SubGraph_SaveLoadBool_uniqueID_666);
	}

	private void Relay_Set_True_666()
	{
		logic_SubGraph_SaveLoadBool_boolean_666 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_666 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_666, logic_SubGraph_SaveLoadBool_boolAsVariable_666, logic_SubGraph_SaveLoadBool_uniqueID_666);
	}

	private void Relay_Set_False_666()
	{
		logic_SubGraph_SaveLoadBool_boolean_666 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_666 = local_HasEnoughMoney_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_666.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_666, logic_SubGraph_SaveLoadBool_boolAsVariable_666, logic_SubGraph_SaveLoadBool_uniqueID_666);
	}

	private void Relay_Save_Out_667()
	{
		Relay_Save_668();
	}

	private void Relay_Load_Out_667()
	{
		Relay_Load_668();
	}

	private void Relay_Restart_Out_667()
	{
		Relay_Set_False_668();
	}

	private void Relay_Save_667()
	{
		logic_SubGraph_SaveLoadBool_boolean_667 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_667 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Save(ref logic_SubGraph_SaveLoadBool_boolean_667, logic_SubGraph_SaveLoadBool_boolAsVariable_667, logic_SubGraph_SaveLoadBool_uniqueID_667);
	}

	private void Relay_Load_667()
	{
		logic_SubGraph_SaveLoadBool_boolean_667 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_667 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Load(ref logic_SubGraph_SaveLoadBool_boolean_667, logic_SubGraph_SaveLoadBool_boolAsVariable_667, logic_SubGraph_SaveLoadBool_uniqueID_667);
	}

	private void Relay_Set_True_667()
	{
		logic_SubGraph_SaveLoadBool_boolean_667 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_667 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_667, logic_SubGraph_SaveLoadBool_boolAsVariable_667, logic_SubGraph_SaveLoadBool_uniqueID_667);
	}

	private void Relay_Set_False_667()
	{
		logic_SubGraph_SaveLoadBool_boolean_667 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_667 = local_WaitingOnPrompt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_667.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_667, logic_SubGraph_SaveLoadBool_boolAsVariable_667, logic_SubGraph_SaveLoadBool_uniqueID_667);
	}

	private void Relay_Save_Out_668()
	{
		Relay_Save_669();
	}

	private void Relay_Load_Out_668()
	{
		Relay_Load_669();
	}

	private void Relay_Restart_Out_668()
	{
		Relay_Set_False_669();
	}

	private void Relay_Save_668()
	{
		logic_SubGraph_SaveLoadBool_boolean_668 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_668 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Save(ref logic_SubGraph_SaveLoadBool_boolean_668, logic_SubGraph_SaveLoadBool_boolAsVariable_668, logic_SubGraph_SaveLoadBool_uniqueID_668);
	}

	private void Relay_Load_668()
	{
		logic_SubGraph_SaveLoadBool_boolean_668 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_668 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Load(ref logic_SubGraph_SaveLoadBool_boolean_668, logic_SubGraph_SaveLoadBool_boolAsVariable_668, logic_SubGraph_SaveLoadBool_uniqueID_668);
	}

	private void Relay_Set_True_668()
	{
		logic_SubGraph_SaveLoadBool_boolean_668 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_668 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_668, logic_SubGraph_SaveLoadBool_boolAsVariable_668, logic_SubGraph_SaveLoadBool_uniqueID_668);
	}

	private void Relay_Set_False_668()
	{
		logic_SubGraph_SaveLoadBool_boolean_668 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_668 = local_SaidMsgNPCVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_668.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_668, logic_SubGraph_SaveLoadBool_boolAsVariable_668, logic_SubGraph_SaveLoadBool_uniqueID_668);
	}

	private void Relay_Save_Out_669()
	{
		Relay_Save_670();
	}

	private void Relay_Load_Out_669()
	{
		Relay_Load_670();
	}

	private void Relay_Restart_Out_669()
	{
		Relay_Set_False_670();
	}

	private void Relay_Save_669()
	{
		logic_SubGraph_SaveLoadBool_boolean_669 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_669 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Save(ref logic_SubGraph_SaveLoadBool_boolean_669, logic_SubGraph_SaveLoadBool_boolAsVariable_669, logic_SubGraph_SaveLoadBool_uniqueID_669);
	}

	private void Relay_Load_669()
	{
		logic_SubGraph_SaveLoadBool_boolean_669 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_669 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Load(ref logic_SubGraph_SaveLoadBool_boolean_669, logic_SubGraph_SaveLoadBool_boolAsVariable_669, logic_SubGraph_SaveLoadBool_uniqueID_669);
	}

	private void Relay_Set_True_669()
	{
		logic_SubGraph_SaveLoadBool_boolean_669 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_669 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_669, logic_SubGraph_SaveLoadBool_boolAsVariable_669, logic_SubGraph_SaveLoadBool_uniqueID_669);
	}

	private void Relay_Set_False_669()
	{
		logic_SubGraph_SaveLoadBool_boolean_669 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_669 = local_ShownMsgLeavingEarlyPostPurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_669.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_669, logic_SubGraph_SaveLoadBool_boolAsVariable_669, logic_SubGraph_SaveLoadBool_uniqueID_669);
	}

	private void Relay_Save_Out_670()
	{
		Relay_Save_692();
	}

	private void Relay_Load_Out_670()
	{
		Relay_Load_692();
	}

	private void Relay_Restart_Out_670()
	{
		Relay_Set_False_692();
	}

	private void Relay_Save_670()
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_670 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Save(ref logic_SubGraph_SaveLoadBool_boolean_670, logic_SubGraph_SaveLoadBool_boolAsVariable_670, logic_SubGraph_SaveLoadBool_uniqueID_670);
	}

	private void Relay_Load_670()
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_670 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Load(ref logic_SubGraph_SaveLoadBool_boolean_670, logic_SubGraph_SaveLoadBool_boolAsVariable_670, logic_SubGraph_SaveLoadBool_uniqueID_670);
	}

	private void Relay_Set_True_670()
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_670 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_670, logic_SubGraph_SaveLoadBool_boolAsVariable_670, logic_SubGraph_SaveLoadBool_uniqueID_670);
	}

	private void Relay_Set_False_670()
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_670 = local_ShownMsgLeavingEarlyPrePurchase_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_670, logic_SubGraph_SaveLoadBool_boolAsVariable_670, logic_SubGraph_SaveLoadBool_uniqueID_670);
	}

	private void Relay_In_680()
	{
		logic_uScript_LockTechSendToSCU_tech_680 = local_vehicleTech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_680.In(logic_uScript_LockTechSendToSCU_tech_680, logic_uScript_LockTechSendToSCU_lockSendToSCU_680);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_680.Out)
		{
			Relay_In_331();
		}
	}

	private void Relay_In_683()
	{
		int num = 0;
		Array array = msgNPCVehicleSwitched;
		if (logic_uScript_AddOnScreenMessage_locString_683.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_683, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_683, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_683 = local_msgTagSwitchedTech_System_String;
		logic_uScript_AddOnScreenMessage_speaker_683 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_683 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_683.In(logic_uScript_AddOnScreenMessage_locString_683, logic_uScript_AddOnScreenMessage_msgPriority_683, logic_uScript_AddOnScreenMessage_holdMsg_683, logic_uScript_AddOnScreenMessage_tag_683, logic_uScript_AddOnScreenMessage_speaker_683, logic_uScript_AddOnScreenMessage_side_683);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_683.Shown)
		{
			Relay_True_688();
		}
	}

	private void Relay_In_686()
	{
		logic_uScriptCon_CompareBool_Bool_686 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_686.In(logic_uScriptCon_CompareBool_Bool_686);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_686.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_686.False;
		if (num)
		{
			Relay_In_680();
		}
		if (flag)
		{
			Relay_In_683();
		}
	}

	private void Relay_True_688()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_688.True(out logic_uScriptAct_SetBool_Target_688);
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_uScriptAct_SetBool_Target_688;
	}

	private void Relay_False_688()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_688.False(out logic_uScriptAct_SetBool_Target_688);
		local_SaidMsgNPCVehicleSwitched_System_Boolean = logic_uScriptAct_SetBool_Target_688;
	}

	private void Relay_In_689()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_689 = local_msgTagSwitchedTech_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_689.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_689, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_689);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_689.Out)
		{
			Relay_In_274();
		}
	}

	private void Relay_Save_Out_692()
	{
		Relay_Save_694();
	}

	private void Relay_Load_Out_692()
	{
		Relay_Load_694();
	}

	private void Relay_Restart_Out_692()
	{
		Relay_Set_False_694();
	}

	private void Relay_Save_692()
	{
		logic_SubGraph_SaveLoadBool_boolean_692 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_692 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Save(ref logic_SubGraph_SaveLoadBool_boolean_692, logic_SubGraph_SaveLoadBool_boolAsVariable_692, logic_SubGraph_SaveLoadBool_uniqueID_692);
	}

	private void Relay_Load_692()
	{
		logic_SubGraph_SaveLoadBool_boolean_692 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_692 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Load(ref logic_SubGraph_SaveLoadBool_boolean_692, logic_SubGraph_SaveLoadBool_boolAsVariable_692, logic_SubGraph_SaveLoadBool_uniqueID_692);
	}

	private void Relay_Set_True_692()
	{
		logic_SubGraph_SaveLoadBool_boolean_692 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_692 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_692, logic_SubGraph_SaveLoadBool_boolAsVariable_692, logic_SubGraph_SaveLoadBool_uniqueID_692);
	}

	private void Relay_Set_False_692()
	{
		logic_SubGraph_SaveLoadBool_boolean_692 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_692 = local_SaidMsgNPCVehicleSwitched_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_692.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_692, logic_SubGraph_SaveLoadBool_boolAsVariable_692, logic_SubGraph_SaveLoadBool_uniqueID_692);
	}

	private void Relay_Save_Out_694()
	{
		Relay_Save_740();
	}

	private void Relay_Load_Out_694()
	{
		Relay_Load_740();
	}

	private void Relay_Restart_Out_694()
	{
		Relay_Set_False_740();
	}

	private void Relay_Save_694()
	{
		logic_SubGraph_SaveLoadBool_boolean_694 = local_NewWeaponAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_694 = local_NewWeaponAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Save(ref logic_SubGraph_SaveLoadBool_boolean_694, logic_SubGraph_SaveLoadBool_boolAsVariable_694, logic_SubGraph_SaveLoadBool_uniqueID_694);
	}

	private void Relay_Load_694()
	{
		logic_SubGraph_SaveLoadBool_boolean_694 = local_NewWeaponAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_694 = local_NewWeaponAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Load(ref logic_SubGraph_SaveLoadBool_boolean_694, logic_SubGraph_SaveLoadBool_boolAsVariable_694, logic_SubGraph_SaveLoadBool_uniqueID_694);
	}

	private void Relay_Set_True_694()
	{
		logic_SubGraph_SaveLoadBool_boolean_694 = local_NewWeaponAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_694 = local_NewWeaponAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_694, logic_SubGraph_SaveLoadBool_boolAsVariable_694, logic_SubGraph_SaveLoadBool_uniqueID_694);
	}

	private void Relay_Set_False_694()
	{
		logic_SubGraph_SaveLoadBool_boolean_694 = local_NewWeaponAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_694 = local_NewWeaponAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_694.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_694, logic_SubGraph_SaveLoadBool_boolAsVariable_694, logic_SubGraph_SaveLoadBool_uniqueID_694);
	}

	private void Relay_In_695()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_695.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_695.Out)
		{
			Relay_In_279();
		}
	}

	private void Relay_In_696()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_696.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_696.Out)
		{
			Relay_In_695();
		}
	}

	private void Relay_In_697()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_697.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_697.Out)
		{
			Relay_In_698();
		}
	}

	private void Relay_In_698()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_698.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_698.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_In_701()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_701.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_701, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_701, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_701 = owner_Connection_703;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_701.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_701, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_701, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_701 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_701.In(logic_uScript_GetAndCheckTechs_techData_701, logic_uScript_GetAndCheckTechs_ownerNode_701, ref logic_uScript_GetAndCheckTechs_techs_701);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_701;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_701.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_701.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_702();
		}
		if (someAlive)
		{
			Relay_AtIndex_702();
		}
	}

	private void Relay_AtIndex_702()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_702.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_702, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_702, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_702.AtIndex(ref logic_uScript_AccessListTech_techList_702, logic_uScript_AccessListTech_index_702, out logic_uScript_AccessListTech_value_702);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_702;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_702;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_702.Out)
		{
			Relay_In_706();
		}
	}

	private void Relay_In_706()
	{
		logic_uScript_FlyTechUpAndAway_tech_706 = local_PaymentPointTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_706 = FlyAwayBehaviour;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_706.In(logic_uScript_FlyTechUpAndAway_tech_706, logic_uScript_FlyTechUpAndAway_maxLifetime_706, logic_uScript_FlyTechUpAndAway_targetHeight_706, logic_uScript_FlyTechUpAndAway_aiTree_706, logic_uScript_FlyTechUpAndAway_removalParticles_706);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_706.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_707()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_707.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_707.Out)
		{
			Relay_In_1091();
		}
	}

	private void Relay_In_709()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_709 = local_msgTagTestComplete_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_709.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_709, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_709);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_709.Out)
		{
			Relay_In_192();
		}
	}

	private void Relay_In_712()
	{
		logic_uScript_GetMissionTimerDisplayTime_owner_712 = owner_Connection_713;
		logic_uScript_GetMissionTimerDisplayTime_Return_712 = logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_712.In(logic_uScript_GetMissionTimerDisplayTime_owner_712);
		local_711_System_Single = logic_uScript_GetMissionTimerDisplayTime_Return_712;
		if (logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_712.Out)
		{
			Relay_In_714();
		}
	}

	private void Relay_In_714()
	{
		logic_uScriptCon_CompareFloat_A_714 = local_711_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_714.In(logic_uScriptCon_CompareFloat_A_714, logic_uScriptCon_CompareFloat_B_714);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_714.GreaterThan)
		{
			Relay_In_81();
		}
	}

	private void Relay_In_716()
	{
		logic_uScript_SetCustomRadarTeamID_tech_716 = local_NPCTank_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_716.In(logic_uScript_SetCustomRadarTeamID_tech_716, logic_uScript_SetCustomRadarTeamID_radarTeamID_716);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_716.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_AtIndex_719()
	{
		int num = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_AccessListTech_techList_719.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_719, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_719, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_719.AtIndex(ref logic_uScript_AccessListTech_techList_719, logic_uScript_AccessListTech_index_719, out logic_uScript_AccessListTech_value_719);
		local_NPCTanks_TankArray = logic_uScript_AccessListTech_techList_719;
		local_NPCTank_Tank = logic_uScript_AccessListTech_value_719;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_719.Out)
		{
			Relay_In_716();
		}
	}

	private void Relay_In_721()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_721.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_721, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_721, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_721 = owner_Connection_720;
		int num2 = 0;
		Array array = local_NPCTanks_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_721.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_721, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_721, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_721 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_721.In(logic_uScript_GetAndCheckTechs_techData_721, logic_uScript_GetAndCheckTechs_ownerNode_721, ref logic_uScript_GetAndCheckTechs_techs_721);
		local_NPCTanks_TankArray = logic_uScript_GetAndCheckTechs_techs_721;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_721.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_721.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_719();
		}
		if (someAlive)
		{
			Relay_AtIndex_719();
		}
	}

	private void Relay_In_722()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_722.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_722.Out)
		{
			Relay_True_724();
		}
	}

	private void Relay_True_724()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_724.True(out logic_uScriptAct_SetBool_Target_724);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_724;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_724.Out)
		{
			Relay_In_725();
		}
	}

	private void Relay_False_724()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_724.False(out logic_uScriptAct_SetBool_Target_724);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_724;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_724.Out)
		{
			Relay_In_725();
		}
	}

	private void Relay_In_725()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_725 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_725 = msgPromptAccessDenied;
		logic_uScript_MissionPromptBlock_Show_targetBlock_725 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_725.In(logic_uScript_MissionPromptBlock_Show_bodyText_725, logic_uScript_MissionPromptBlock_Show_acceptButtonText_725, logic_uScript_MissionPromptBlock_Show_rejectButtonText_725, logic_uScript_MissionPromptBlock_Show_targetBlock_725, logic_uScript_MissionPromptBlock_Show_highlightBlock_725, logic_uScript_MissionPromptBlock_Show_singleUse_725);
	}

	private void Relay_In_729()
	{
		logic_uScriptCon_CompareBool_Bool_729 = local_BlockLimitCritical_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_729.In(logic_uScriptCon_CompareBool_Bool_729);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_729.False)
		{
			Relay_In_390();
		}
	}

	private void Relay_In_730()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_730.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_730, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_730, num, array.Length);
		num += array.Length;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_730 = local_MaxPlayers_System_Int32;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_730.In(logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_730, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_730);
		bool num2 = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_730.True;
		bool flag = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_730.False;
		if (num2)
		{
			Relay_False_738();
		}
		if (flag)
		{
			Relay_In_722();
		}
	}

	private void Relay_In_733()
	{
		logic_uScript_GetMaxPlayers_Return_733 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_733.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_733;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_733.Out)
		{
			Relay_In_730();
		}
	}

	private void Relay_In_736()
	{
		logic_uScriptCon_CompareBool_Bool_736 = local_BlockLimitCritical_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_736.In(logic_uScriptCon_CompareBool_Bool_736);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_736.False)
		{
			Relay_In_563();
		}
	}

	private void Relay_True_738()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_738.True(out logic_uScriptAct_SetBool_Target_738);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_738;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_738.Out)
		{
			Relay_In_528();
		}
	}

	private void Relay_False_738()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_738.False(out logic_uScriptAct_SetBool_Target_738);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_738;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_738.Out)
		{
			Relay_In_528();
		}
	}

	private void Relay_Save_Out_740()
	{
		Relay_Save_857();
	}

	private void Relay_Load_Out_740()
	{
		Relay_Load_857();
	}

	private void Relay_Restart_Out_740()
	{
		Relay_Set_True_857();
	}

	private void Relay_Save_740()
	{
		logic_SubGraph_SaveLoadBool_boolean_740 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_740 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Save(ref logic_SubGraph_SaveLoadBool_boolean_740, logic_SubGraph_SaveLoadBool_boolAsVariable_740, logic_SubGraph_SaveLoadBool_uniqueID_740);
	}

	private void Relay_Load_740()
	{
		logic_SubGraph_SaveLoadBool_boolean_740 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_740 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Load(ref logic_SubGraph_SaveLoadBool_boolean_740, logic_SubGraph_SaveLoadBool_boolAsVariable_740, logic_SubGraph_SaveLoadBool_uniqueID_740);
	}

	private void Relay_Set_True_740()
	{
		logic_SubGraph_SaveLoadBool_boolean_740 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_740 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_740, logic_SubGraph_SaveLoadBool_boolAsVariable_740, logic_SubGraph_SaveLoadBool_uniqueID_740);
	}

	private void Relay_Set_False_740()
	{
		logic_SubGraph_SaveLoadBool_boolean_740 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_740 = local_BlockLimitCritical_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_740.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_740, logic_SubGraph_SaveLoadBool_boolAsVariable_740, logic_SubGraph_SaveLoadBool_uniqueID_740);
	}

	private void Relay_True_741()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_741.True(out logic_uScriptAct_SetBool_Target_741);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_741;
	}

	private void Relay_False_741()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_741.False(out logic_uScriptAct_SetBool_Target_741);
		local_VehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_741;
	}

	private void Relay_In_743()
	{
		logic_uScriptCon_CompareBool_Bool_743 = local_VehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_743.In(logic_uScriptCon_CompareBool_Bool_743);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_743.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_743.False;
		if (num)
		{
			Relay_True_902();
		}
		if (flag)
		{
			Relay_In_571();
		}
	}

	private void Relay_Out_746()
	{
		Relay_In_748();
	}

	private void Relay_In_746()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_746 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_746.In(logic_SubGraph_LoadObjectiveStates_currentObjective_746);
	}

	private void Relay_In_747()
	{
		logic_uScript_ResetMissionTimer_owner_747 = owner_Connection_751;
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_747.In(logic_uScript_ResetMissionTimer_owner_747);
		if (logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_747.Out)
		{
			Relay_In_749();
		}
	}

	private void Relay_In_748()
	{
		logic_uScript_StopMissionTimer_owner_748 = owner_Connection_753;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_748.In(logic_uScript_StopMissionTimer_owner_748);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_748.Out)
		{
			Relay_In_747();
		}
	}

	private void Relay_In_749()
	{
		logic_uScript_HideMissionTimerUI_owner_749 = owner_Connection_858;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_749.In(logic_uScript_HideMissionTimerUI_owner_749);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_749.Out)
		{
			Relay_In_1073();
		}
	}

	private void Relay_Save_Out_752()
	{
	}

	private void Relay_Load_Out_752()
	{
		Relay_In_746();
	}

	private void Relay_Restart_Out_752()
	{
	}

	private void Relay_Save_752()
	{
		logic_SubGraph_SaveLoadInt_integer_752 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_752 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.Save(logic_SubGraph_SaveLoadInt_restartValue_752, ref logic_SubGraph_SaveLoadInt_integer_752, logic_SubGraph_SaveLoadInt_intAsVariable_752, logic_SubGraph_SaveLoadInt_uniqueID_752);
	}

	private void Relay_Load_752()
	{
		logic_SubGraph_SaveLoadInt_integer_752 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_752 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.Load(logic_SubGraph_SaveLoadInt_restartValue_752, ref logic_SubGraph_SaveLoadInt_integer_752, logic_SubGraph_SaveLoadInt_intAsVariable_752, logic_SubGraph_SaveLoadInt_uniqueID_752);
	}

	private void Relay_Restart_752()
	{
		logic_SubGraph_SaveLoadInt_integer_752 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_752 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_752.Restart(logic_SubGraph_SaveLoadInt_restartValue_752, ref logic_SubGraph_SaveLoadInt_integer_752, logic_SubGraph_SaveLoadInt_intAsVariable_752, logic_SubGraph_SaveLoadInt_uniqueID_752);
	}

	private void Relay_In_754()
	{
		logic_uScriptCon_CompareBool_Bool_754 = local_SwitchedTech_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_754.In(logic_uScriptCon_CompareBool_Bool_754);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_754.False)
		{
			Relay_In_629();
		}
	}

	private void Relay_True_755()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_755.True(out logic_uScriptAct_SetBool_Target_755);
		local_SwitchedTech_System_Boolean = logic_uScriptAct_SetBool_Target_755;
	}

	private void Relay_False_755()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_755.False(out logic_uScriptAct_SetBool_Target_755);
		local_SwitchedTech_System_Boolean = logic_uScriptAct_SetBool_Target_755;
	}

	private void Relay_In_760()
	{
		logic_uScript_GetMaxPlayers_Return_760 = logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_760.In();
		local_MaxPlayers_System_Int32 = logic_uScript_GetMaxPlayers_Return_760;
		if (logic_uScript_GetMaxPlayers_uScript_GetMaxPlayers_760.Out)
		{
			Relay_In_768();
		}
	}

	private void Relay_In_764()
	{
		logic_uScriptCon_CompareBool_Bool_764 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_764.In(logic_uScriptCon_CompareBool_Bool_764);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_764.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_764.False;
		if (num)
		{
			Relay_In_789();
		}
		if (flag)
		{
			Relay_In_789();
		}
	}

	private void Relay_In_766()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_766 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_766 = msgPromptNoMoney;
		logic_uScript_MissionPromptBlock_Show_targetBlock_766 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_766.In(logic_uScript_MissionPromptBlock_Show_bodyText_766, logic_uScript_MissionPromptBlock_Show_acceptButtonText_766, logic_uScript_MissionPromptBlock_Show_rejectButtonText_766, logic_uScript_MissionPromptBlock_Show_targetBlock_766, logic_uScript_MissionPromptBlock_Show_highlightBlock_766, logic_uScript_MissionPromptBlock_Show_singleUse_766);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_766.Out)
		{
			Relay_False_777();
		}
	}

	private void Relay_In_767()
	{
		logic_uScriptCon_CompareInt_A_767 = local_CurrentMoney_System_Int32;
		logic_uScriptCon_CompareInt_B_767 = vehicleCost;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_767.In(logic_uScriptCon_CompareInt_A_767, logic_uScriptCon_CompareInt_B_767);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_767.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_767.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_764();
		}
		if (lessThan)
		{
			Relay_In_780();
		}
	}

	private void Relay_In_768()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_768.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_768, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_768, num, array.Length);
		num += array.Length;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_768 = local_MaxPlayers_System_Int32;
		logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_768.In(logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_spawnData_768, logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_count_768);
		bool num2 = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_768.True;
		bool flag = logic_uScript_CanSpawnPlayerTechsWithinBlockLimit_uScript_CanSpawnPlayerTechsWithinBlockLimit_768.False;
		if (num2)
		{
			Relay_False_773();
		}
		if (flag)
		{
			Relay_In_790();
		}
	}

	private void Relay_In_769()
	{
		logic_uScriptCon_CompareBool_Bool_769 = _DEBUGIgnoreMoneyCheck;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_769.In(logic_uScriptCon_CompareBool_Bool_769);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_769.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_769.False;
		if (num)
		{
			Relay_In_770();
		}
		if (flag)
		{
			Relay_In_786();
		}
	}

	private void Relay_In_770()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_770.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_770.Out)
		{
			Relay_In_764();
		}
	}

	private void Relay_True_773()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_773.True(out logic_uScriptAct_SetBool_Target_773);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_773;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_773.Out)
		{
			Relay_In_769();
		}
	}

	private void Relay_False_773()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_773.False(out logic_uScriptAct_SetBool_Target_773);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_773;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_773.Out)
		{
			Relay_In_769();
		}
	}

	private void Relay_True_777()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_777.True(out logic_uScriptAct_SetBool_Target_777);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_777;
	}

	private void Relay_False_777()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_777.False(out logic_uScriptAct_SetBool_Target_777);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_777;
	}

	private void Relay_In_780()
	{
		logic_uScriptCon_CompareBool_Bool_780 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_780.In(logic_uScriptCon_CompareBool_Bool_780);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_780.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_780.False;
		if (num)
		{
			Relay_In_766();
		}
		if (flag)
		{
			Relay_In_766();
		}
	}

	private void Relay_In_784()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_784 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_784 = msgPromptAccessDenied;
		logic_uScript_MissionPromptBlock_Show_targetBlock_784 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_784.In(logic_uScript_MissionPromptBlock_Show_bodyText_784, logic_uScript_MissionPromptBlock_Show_acceptButtonText_784, logic_uScript_MissionPromptBlock_Show_rejectButtonText_784, logic_uScript_MissionPromptBlock_Show_targetBlock_784, logic_uScript_MissionPromptBlock_Show_highlightBlock_784, logic_uScript_MissionPromptBlock_Show_singleUse_784);
	}

	private void Relay_In_786()
	{
		logic_uScript_GetCurrentMoneyEarned_Return_786 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_786.In();
		local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_786;
		if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_786.Out)
		{
			Relay_In_767();
		}
	}

	private void Relay_In_789()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_789 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_789 = msgPromptAccept;
		logic_uScript_MissionPromptBlock_Show_rejectButtonText_789 = msgPromptDecline;
		logic_uScript_MissionPromptBlock_Show_targetBlock_789 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_789.In(logic_uScript_MissionPromptBlock_Show_bodyText_789, logic_uScript_MissionPromptBlock_Show_acceptButtonText_789, logic_uScript_MissionPromptBlock_Show_rejectButtonText_789, logic_uScript_MissionPromptBlock_Show_targetBlock_789, logic_uScript_MissionPromptBlock_Show_highlightBlock_789, logic_uScript_MissionPromptBlock_Show_singleUse_789);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_789.Out)
		{
			Relay_True_777();
		}
	}

	private void Relay_In_790()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_790.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_790.Out)
		{
			Relay_True_791();
		}
	}

	private void Relay_True_791()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_791.True(out logic_uScriptAct_SetBool_Target_791);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_791;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_791.Out)
		{
			Relay_In_784();
		}
	}

	private void Relay_False_791()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_791.False(out logic_uScriptAct_SetBool_Target_791);
		local_BlockLimitCritical_System_Boolean = logic_uScriptAct_SetBool_Target_791;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_791.Out)
		{
			Relay_In_784();
		}
	}

	private void Relay_AtIndex_795()
	{
		int num = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_AccessListTech_techList_795.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_795, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_795, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_795.AtIndex(ref logic_uScript_AccessListTech_techList_795, logic_uScript_AccessListTech_index_795, out logic_uScript_AccessListTech_value_795);
		local_NPCPaymentPoints_TankArray = logic_uScript_AccessListTech_techList_795;
		local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_795;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_795.Out)
		{
			Relay_In_799();
		}
	}

	private void Relay_In_799()
	{
		logic_uScript_GetTankBlock_tank_799 = local_PaymentPointTech_Tank;
		logic_uScript_GetTankBlock_blockType_799 = interactableBlockType;
		logic_uScript_GetTankBlock_Return_799 = logic_uScript_GetTankBlock_uScript_GetTankBlock_799.In(logic_uScript_GetTankBlock_tank_799, logic_uScript_GetTankBlock_blockType_799);
		local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_799;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_799.Returned)
		{
			Relay_In_899();
		}
	}

	private void Relay_In_802()
	{
		int num = 0;
		Array array = vehicleSpawnDataRebuy1;
		if (logic_uScript_GetAndCheckTechs_techData_802.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_802, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_802, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_802 = owner_Connection_804;
		logic_uScript_GetAndCheckTechs_Return_802 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_802.In(logic_uScript_GetAndCheckTechs_techData_802, logic_uScript_GetAndCheckTechs_ownerNode_802, ref logic_uScript_GetAndCheckTechs_techs_802);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_802.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_802.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_802.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_802.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_811();
		}
		if (someAlive)
		{
			Relay_In_811();
		}
		if (allDead)
		{
			Relay_InitialSpawn_819();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_819();
		}
	}

	private void Relay_In_805()
	{
		logic_uScript_AddMoney_amount_805 = local_808_System_Int32;
		logic_uScript_AddMoney_uScript_AddMoney_805.In(logic_uScript_AddMoney_amount_805);
		if (logic_uScript_AddMoney_uScript_AddMoney_805.Out)
		{
			Relay_In_515();
		}
	}

	private void Relay_In_806()
	{
		logic_uScriptAct_MultiplyInt_v2_A_806 = vehicleCost;
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_806.In(logic_uScriptAct_MultiplyInt_v2_A_806, logic_uScriptAct_MultiplyInt_v2_B_806, out logic_uScriptAct_MultiplyInt_v2_IntResult_806, out logic_uScriptAct_MultiplyInt_v2_FloatResult_806);
		local_808_System_Int32 = logic_uScriptAct_MultiplyInt_v2_IntResult_806;
		if (logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_806.Out)
		{
			Relay_In_805();
		}
	}

	private void Relay_In_811()
	{
		int num = 0;
		Array array = vehicleSpawnDataRebuy2;
		if (logic_uScript_GetAndCheckTechs_techData_811.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_811, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_811, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_811 = owner_Connection_809;
		logic_uScript_GetAndCheckTechs_Return_811 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_811.In(logic_uScript_GetAndCheckTechs_techData_811, logic_uScript_GetAndCheckTechs_ownerNode_811, ref logic_uScript_GetAndCheckTechs_techs_811);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_811.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_811.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_811.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_811.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_813();
		}
		if (someAlive)
		{
			Relay_In_813();
		}
		if (allDead)
		{
			Relay_InitialSpawn_822();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_822();
		}
	}

	private void Relay_In_813()
	{
		int num = 0;
		Array array = vehicleSpawnDataRebuy3;
		if (logic_uScript_GetAndCheckTechs_techData_813.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_813, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_813, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_813 = owner_Connection_814;
		logic_uScript_GetAndCheckTechs_Return_813 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_813.In(logic_uScript_GetAndCheckTechs_techData_813, logic_uScript_GetAndCheckTechs_ownerNode_813, ref logic_uScript_GetAndCheckTechs_techs_813);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_813.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_813.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_813.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_813.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_815();
		}
		if (someAlive)
		{
			Relay_In_815();
		}
		if (allDead)
		{
			Relay_InitialSpawn_825();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_825();
		}
	}

	private void Relay_In_815()
	{
		int num = 0;
		Array array = vehicleSpawnDataRebuy4;
		if (logic_uScript_GetAndCheckTechs_techData_815.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_815, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_815, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_815 = owner_Connection_817;
		logic_uScript_GetAndCheckTechs_Return_815 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_815.In(logic_uScript_GetAndCheckTechs_techData_815, logic_uScript_GetAndCheckTechs_ownerNode_815, ref logic_uScript_GetAndCheckTechs_techs_815);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_815.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_815.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_815.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_815.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_839();
		}
		if (someAlive)
		{
			Relay_In_839();
		}
		if (allDead)
		{
			Relay_InitialSpawn_828();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_828();
		}
	}

	private void Relay_InitialSpawn_819()
	{
		int num = 0;
		Array array = vehicleSpawnDataRebuy1;
		if (logic_uScript_SpawnTechsFromData_spawnData_819.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_819, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_819, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_819 = owner_Connection_820;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_819.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_819, logic_uScript_SpawnTechsFromData_ownerNode_819, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_819, logic_uScript_SpawnTechsFromData_allowResurrection_819);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_819.Out)
		{
			Relay_In_833();
		}
	}

	private void Relay_InitialSpawn_822()
	{
		int num = 0;
		Array array = vehicleSpawnDataRebuy2;
		if (logic_uScript_SpawnTechsFromData_spawnData_822.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_822, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_822, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_822 = owner_Connection_823;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_822.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_822, logic_uScript_SpawnTechsFromData_ownerNode_822, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_822, logic_uScript_SpawnTechsFromData_allowResurrection_822);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_822.Out)
		{
			Relay_In_834();
		}
	}

	private void Relay_InitialSpawn_825()
	{
		int num = 0;
		Array array = vehicleSpawnDataRebuy3;
		if (logic_uScript_SpawnTechsFromData_spawnData_825.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_825, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_825, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_825 = owner_Connection_826;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_825.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_825, logic_uScript_SpawnTechsFromData_ownerNode_825, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_825, logic_uScript_SpawnTechsFromData_allowResurrection_825);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_825.Out)
		{
			Relay_In_835();
		}
	}

	private void Relay_InitialSpawn_828()
	{
		int num = 0;
		Array array = vehicleSpawnDataRebuy4;
		if (logic_uScript_SpawnTechsFromData_spawnData_828.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_828, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_828, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_828 = owner_Connection_829;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_828.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_828, logic_uScript_SpawnTechsFromData_ownerNode_828, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_828, logic_uScript_SpawnTechsFromData_allowResurrection_828);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_828.Out)
		{
			Relay_In_836();
		}
	}

	private void Relay_In_830()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_830.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_830, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_830, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_830 = owner_Connection_832;
		logic_uScript_GetAndCheckTechs_Return_830 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_830.In(logic_uScript_GetAndCheckTechs_techData_830, logic_uScript_GetAndCheckTechs_ownerNode_830, ref logic_uScript_GetAndCheckTechs_techs_830);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_830.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_830.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_830.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_830.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_842();
		}
		if (someAlive)
		{
			Relay_In_842();
		}
		if (allDead)
		{
			Relay_InitialSpawn_423();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_423();
		}
	}

	private void Relay_In_833()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_833.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_833.Out)
		{
			Relay_In_834();
		}
	}

	private void Relay_In_834()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_834.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_834.Out)
		{
			Relay_In_835();
		}
	}

	private void Relay_In_835()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_835.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_835.Out)
		{
			Relay_In_836();
		}
	}

	private void Relay_In_836()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_836.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_836.Out)
		{
			Relay_In_806();
		}
	}

	private void Relay_In_839()
	{
		logic_uScript_AddMessage_messageData_839 = msgNPCSoldOut;
		logic_uScript_AddMessage_speaker_839 = SpeakerNPC;
		logic_uScript_AddMessage_Return_839 = logic_uScript_AddMessage_uScript_AddMessage_839.In(logic_uScript_AddMessage_messageData_839, logic_uScript_AddMessage_speaker_839);
		if (logic_uScript_AddMessage_uScript_AddMessage_839.Out)
		{
			Relay_In_841();
		}
	}

	private void Relay_In_840()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_840.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_840.Out)
		{
			Relay_In_806();
		}
	}

	private void Relay_In_841()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_841.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_841.Out)
		{
			Relay_In_515();
		}
	}

	private void Relay_In_842()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_842.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_842.Out)
		{
			Relay_In_852();
		}
	}

	private void Relay_In_844()
	{
		int num = 0;
		Array array = vehicleSpawnData2;
		if (logic_uScript_GetAndCheckTechs_techData_844.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_844, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_844, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_844 = owner_Connection_843;
		logic_uScript_GetAndCheckTechs_Return_844 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_844.In(logic_uScript_GetAndCheckTechs_techData_844, logic_uScript_GetAndCheckTechs_ownerNode_844, ref logic_uScript_GetAndCheckTechs_techs_844);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_844.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_844.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_844.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_844.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_802();
		}
		if (someAlive)
		{
			Relay_In_802();
		}
		if (allDead)
		{
			Relay_InitialSpawn_568();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_568();
		}
	}

	private void Relay_In_846()
	{
		int num = 0;
		Array array = vehicleSpawnData3;
		if (logic_uScript_GetAndCheckTechs_techData_846.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_846, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_846, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_846 = owner_Connection_847;
		logic_uScript_GetAndCheckTechs_Return_846 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_846.In(logic_uScript_GetAndCheckTechs_techData_846, logic_uScript_GetAndCheckTechs_ownerNode_846, ref logic_uScript_GetAndCheckTechs_techs_846);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_846.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_846.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_846.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_846.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_853();
		}
		if (someAlive)
		{
			Relay_In_853();
		}
		if (allDead)
		{
			Relay_InitialSpawn_579();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_579();
		}
	}

	private void Relay_In_850()
	{
		int num = 0;
		Array array = vehicleSpawnData4;
		if (logic_uScript_GetAndCheckTechs_techData_850.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_850, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_850, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_850 = owner_Connection_851;
		logic_uScript_GetAndCheckTechs_Return_850 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_850.In(logic_uScript_GetAndCheckTechs_techData_850, logic_uScript_GetAndCheckTechs_ownerNode_850, ref logic_uScript_GetAndCheckTechs_techs_850);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_850.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_850.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_850.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_850.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_852();
		}
		if (someAlive)
		{
			Relay_In_852();
		}
		if (allDead)
		{
			Relay_InitialSpawn_400();
		}
		if (waitingToSpawn)
		{
			Relay_InitialSpawn_400();
		}
	}

	private void Relay_In_852()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_852.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_852.Out)
		{
			Relay_In_853();
		}
	}

	private void Relay_In_853()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_853.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_853.Out)
		{
			Relay_In_802();
		}
	}

	private void Relay_True_854()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_854.True(out logic_uScriptAct_SetBool_Target_854);
		local_msgRebuyShown_System_Boolean = logic_uScriptAct_SetBool_Target_854;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_854.Out)
		{
			Relay_In_1086();
		}
	}

	private void Relay_False_854()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_854.False(out logic_uScriptAct_SetBool_Target_854);
		local_msgRebuyShown_System_Boolean = logic_uScriptAct_SetBool_Target_854;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_854.Out)
		{
			Relay_In_1086();
		}
	}

	private void Relay_Save_Out_857()
	{
		Relay_Save_906();
	}

	private void Relay_Load_Out_857()
	{
		Relay_Load_906();
	}

	private void Relay_Restart_Out_857()
	{
		Relay_Set_False_906();
	}

	private void Relay_Save_857()
	{
		logic_SubGraph_SaveLoadBool_boolean_857 = local_msgRebuyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_857 = local_msgRebuyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Save(ref logic_SubGraph_SaveLoadBool_boolean_857, logic_SubGraph_SaveLoadBool_boolAsVariable_857, logic_SubGraph_SaveLoadBool_uniqueID_857);
	}

	private void Relay_Load_857()
	{
		logic_SubGraph_SaveLoadBool_boolean_857 = local_msgRebuyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_857 = local_msgRebuyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Load(ref logic_SubGraph_SaveLoadBool_boolean_857, logic_SubGraph_SaveLoadBool_boolAsVariable_857, logic_SubGraph_SaveLoadBool_uniqueID_857);
	}

	private void Relay_Set_True_857()
	{
		logic_SubGraph_SaveLoadBool_boolean_857 = local_msgRebuyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_857 = local_msgRebuyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_857, logic_SubGraph_SaveLoadBool_boolAsVariable_857, logic_SubGraph_SaveLoadBool_uniqueID_857);
	}

	private void Relay_Set_False_857()
	{
		logic_SubGraph_SaveLoadBool_boolean_857 = local_msgRebuyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_857 = local_msgRebuyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_857.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_857, logic_SubGraph_SaveLoadBool_boolAsVariable_857, logic_SubGraph_SaveLoadBool_uniqueID_857);
	}

	private void Relay_In_862()
	{
		int num = 0;
		Array array = local_MobTechs1_TankArray;
		if (logic_uScript_DamageTechs_techs_862.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_862, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_862, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_862.In(logic_uScript_DamageTechs_techs_862, logic_uScript_DamageTechs_dmgPercent_862, logic_uScript_DamageTechs_givePlyrCredit_862, logic_uScript_DamageTechs_leaveBlksPercent_862, logic_uScript_DamageTechs_makeVulnerable_862);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_862.Out)
		{
			Relay_In_881();
		}
	}

	private void Relay_In_865()
	{
		int num = 0;
		Array array = local_MobTechs5_TankArray;
		if (logic_uScript_DamageTechs_techs_865.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_865, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_865, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_865.In(logic_uScript_DamageTechs_techs_865, logic_uScript_DamageTechs_dmgPercent_865, logic_uScript_DamageTechs_givePlyrCredit_865, logic_uScript_DamageTechs_leaveBlksPercent_865, logic_uScript_DamageTechs_makeVulnerable_865);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_865.Out)
		{
			Relay_In_878();
		}
	}

	private void Relay_In_866()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_866.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_866.Out)
		{
			Relay_In_870();
		}
	}

	private void Relay_In_869()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_869.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_869, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_869[num++] = enemyGroup5Data;
		logic_uScript_GetAndCheckTechs_ownerNode_869 = owner_Connection_861;
		int num2 = 0;
		Array array = local_MobTechs5_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_869.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_869, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_869, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_869 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_869.In(logic_uScript_GetAndCheckTechs_techData_869, logic_uScript_GetAndCheckTechs_ownerNode_869, ref logic_uScript_GetAndCheckTechs_techs_869);
		local_MobTechs5_TankArray = logic_uScript_GetAndCheckTechs_techs_869;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_869.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_869.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_869.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_869.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_865();
		}
		if (someAlive)
		{
			Relay_In_865();
		}
		if (allDead)
		{
			Relay_In_882();
		}
		if (waitingToSpawn)
		{
			Relay_In_882();
		}
	}

	private void Relay_In_870()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_870.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_870, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_870[num++] = enemyGroup3Data;
		logic_uScript_GetAndCheckTechs_ownerNode_870 = owner_Connection_879;
		int num2 = 0;
		Array array = local_MobTechs3_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_870.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_870, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_870, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_870 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_870.In(logic_uScript_GetAndCheckTechs_techData_870, logic_uScript_GetAndCheckTechs_ownerNode_870, ref logic_uScript_GetAndCheckTechs_techs_870);
		local_MobTechs3_TankArray = logic_uScript_GetAndCheckTechs_techs_870;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_870.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_870.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_870.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_870.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_880();
		}
		if (someAlive)
		{
			Relay_In_880();
		}
		if (allDead)
		{
			Relay_In_883();
		}
		if (waitingToSpawn)
		{
			Relay_In_883();
		}
	}

	private void Relay_In_875()
	{
		int num = 0;
		Array array = local_MobTechs2_TankArray;
		if (logic_uScript_DamageTechs_techs_875.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_875, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_875, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_875.In(logic_uScript_DamageTechs_techs_875, logic_uScript_DamageTechs_dmgPercent_875, logic_uScript_DamageTechs_givePlyrCredit_875, logic_uScript_DamageTechs_leaveBlksPercent_875, logic_uScript_DamageTechs_makeVulnerable_875);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_875.Out)
		{
			Relay_In_870();
		}
	}

	private void Relay_In_878()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_878.In();
	}

	private void Relay_In_880()
	{
		int num = 0;
		Array array = local_MobTechs3_TankArray;
		if (logic_uScript_DamageTechs_techs_880.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_880, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_880, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_880.In(logic_uScript_DamageTechs_techs_880, logic_uScript_DamageTechs_dmgPercent_880, logic_uScript_DamageTechs_givePlyrCredit_880, logic_uScript_DamageTechs_leaveBlksPercent_880, logic_uScript_DamageTechs_makeVulnerable_880);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_880.Out)
		{
			Relay_In_891();
		}
	}

	private void Relay_In_881()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_881.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_881, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_881[num++] = enemyGroup2Data;
		logic_uScript_GetAndCheckTechs_ownerNode_881 = owner_Connection_871;
		int num2 = 0;
		Array array = local_MobTechs2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_881.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_881, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_881, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_881 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_881.In(logic_uScript_GetAndCheckTechs_techData_881, logic_uScript_GetAndCheckTechs_ownerNode_881, ref logic_uScript_GetAndCheckTechs_techs_881);
		local_MobTechs2_TankArray = logic_uScript_GetAndCheckTechs_techs_881;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_881.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_881.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_881.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_881.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_875();
		}
		if (someAlive)
		{
			Relay_In_875();
		}
		if (allDead)
		{
			Relay_In_866();
		}
		if (waitingToSpawn)
		{
			Relay_In_866();
		}
	}

	private void Relay_In_882()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_882.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_882.Out)
		{
			Relay_In_878();
		}
	}

	private void Relay_In_883()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_883.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_883.Out)
		{
			Relay_In_891();
		}
	}

	private void Relay_In_884()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_884.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_884, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_884[num++] = enemyGroup1Data;
		logic_uScript_GetAndCheckTechs_ownerNode_884 = owner_Connection_868;
		int num2 = 0;
		Array array = local_MobTechs1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_884.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_884, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_884, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_884 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_884.In(logic_uScript_GetAndCheckTechs_techData_884, logic_uScript_GetAndCheckTechs_ownerNode_884, ref logic_uScript_GetAndCheckTechs_techs_884);
		local_MobTechs1_TankArray = logic_uScript_GetAndCheckTechs_techs_884;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_884.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_884.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_884.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_884.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_862();
		}
		if (someAlive)
		{
			Relay_In_862();
		}
		if (allDead)
		{
			Relay_In_887();
		}
		if (waitingToSpawn)
		{
			Relay_In_887();
		}
	}

	private void Relay_In_885()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_885.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_885.Out)
		{
			Relay_In_869();
		}
	}

	private void Relay_In_887()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_887.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_887.Out)
		{
			Relay_In_881();
		}
	}

	private void Relay_In_888()
	{
		int num = 0;
		Array array = local_MobTechs4_TankArray;
		if (logic_uScript_DamageTechs_techs_888.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_888, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_888, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_888.In(logic_uScript_DamageTechs_techs_888, logic_uScript_DamageTechs_dmgPercent_888, logic_uScript_DamageTechs_givePlyrCredit_888, logic_uScript_DamageTechs_leaveBlksPercent_888, logic_uScript_DamageTechs_makeVulnerable_888);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_888.Out)
		{
			Relay_In_869();
		}
	}

	private void Relay_In_891()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_891.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_891, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_891[num++] = enemyGroup4Data;
		logic_uScript_GetAndCheckTechs_ownerNode_891 = owner_Connection_890;
		int num2 = 0;
		Array array = local_MobTechs4_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_891.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_891, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_891, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_891 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_891.In(logic_uScript_GetAndCheckTechs_techData_891, logic_uScript_GetAndCheckTechs_ownerNode_891, ref logic_uScript_GetAndCheckTechs_techs_891);
		local_MobTechs4_TankArray = logic_uScript_GetAndCheckTechs_techs_891;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_891.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_891.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_891.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_891.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_888();
		}
		if (someAlive)
		{
			Relay_In_888();
		}
		if (allDead)
		{
			Relay_In_885();
		}
		if (waitingToSpawn)
		{
			Relay_In_885();
		}
	}

	private void Relay_In_892()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_892.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_892.Out)
		{
			Relay_In_884();
		}
	}

	private void Relay_In_893()
	{
		logic_uScript_Wait_repeat_893 = local_RepeatWaitTime_System_Boolean;
		logic_uScript_Wait_uScript_Wait_893.In(logic_uScript_Wait_seconds_893, logic_uScript_Wait_repeat_893);
		if (logic_uScript_Wait_uScript_Wait_893.Waited)
		{
			Relay_False_900();
		}
	}

	private void Relay_In_894()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_894 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_894.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_894);
		if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_894.Out)
		{
			Relay_In_893();
		}
	}

	private void Relay_True_895()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_895.True(out logic_uScriptAct_SetBool_Target_895);
		local_RepeatWaitTime_System_Boolean = logic_uScriptAct_SetBool_Target_895;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_895.Out)
		{
			Relay_In_962();
		}
	}

	private void Relay_False_895()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_895.False(out logic_uScriptAct_SetBool_Target_895);
		local_RepeatWaitTime_System_Boolean = logic_uScriptAct_SetBool_Target_895;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_895.Out)
		{
			Relay_In_962();
		}
	}

	private void Relay_In_899()
	{
		logic_uScriptCon_CompareBool_Bool_899 = local_AdditionalVehiclePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_899.In(logic_uScriptCon_CompareBool_Bool_899);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_899.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_899.False;
		if (num)
		{
			Relay_In_894();
		}
		if (flag)
		{
			Relay_In_1039();
		}
	}

	private void Relay_True_900()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_900.True(out logic_uScriptAct_SetBool_Target_900);
		local_AdditionalVehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_900;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_900.Out)
		{
			Relay_True_895();
		}
	}

	private void Relay_False_900()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_900.False(out logic_uScriptAct_SetBool_Target_900);
		local_AdditionalVehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_900;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_900.Out)
		{
			Relay_True_895();
		}
	}

	private void Relay_True_902()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_902.True(out logic_uScriptAct_SetBool_Target_902);
		local_AdditionalVehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_902;
	}

	private void Relay_False_902()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_902.False(out logic_uScriptAct_SetBool_Target_902);
		local_AdditionalVehiclePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_902;
	}

	private void Relay_Save_Out_906()
	{
		Relay_Save_1033();
	}

	private void Relay_Load_Out_906()
	{
		Relay_Load_1033();
	}

	private void Relay_Restart_Out_906()
	{
		Relay_Set_False_1033();
	}

	private void Relay_Save_906()
	{
		logic_SubGraph_SaveLoadBool_boolean_906 = local_AdditionalVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_906 = local_AdditionalVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Save(ref logic_SubGraph_SaveLoadBool_boolean_906, logic_SubGraph_SaveLoadBool_boolAsVariable_906, logic_SubGraph_SaveLoadBool_uniqueID_906);
	}

	private void Relay_Load_906()
	{
		logic_SubGraph_SaveLoadBool_boolean_906 = local_AdditionalVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_906 = local_AdditionalVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Load(ref logic_SubGraph_SaveLoadBool_boolean_906, logic_SubGraph_SaveLoadBool_boolAsVariable_906, logic_SubGraph_SaveLoadBool_uniqueID_906);
	}

	private void Relay_Set_True_906()
	{
		logic_SubGraph_SaveLoadBool_boolean_906 = local_AdditionalVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_906 = local_AdditionalVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_906, logic_SubGraph_SaveLoadBool_boolAsVariable_906, logic_SubGraph_SaveLoadBool_uniqueID_906);
	}

	private void Relay_Set_False_906()
	{
		logic_SubGraph_SaveLoadBool_boolean_906 = local_AdditionalVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_906 = local_AdditionalVehiclePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_906.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_906, logic_SubGraph_SaveLoadBool_boolAsVariable_906, logic_SubGraph_SaveLoadBool_uniqueID_906);
	}

	private void Relay_True_908()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_908.True(out logic_uScriptAct_SetBool_Target_908);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_908;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_908.Out)
		{
			Relay_In_279();
		}
	}

	private void Relay_False_908()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_908.False(out logic_uScriptAct_SetBool_Target_908);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_908;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_908.Out)
		{
			Relay_In_279();
		}
	}

	private void Relay_In_909()
	{
		int num = 0;
		Array array = msgNPCTestFailedPlayerDead;
		if (logic_uScript_AddOnScreenMessage_locString_909.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_909, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_909, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_909 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_909 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_909.In(logic_uScript_AddOnScreenMessage_locString_909, logic_uScript_AddOnScreenMessage_msgPriority_909, logic_uScript_AddOnScreenMessage_holdMsg_909, logic_uScript_AddOnScreenMessage_tag_909, logic_uScript_AddOnScreenMessage_speaker_909, logic_uScript_AddOnScreenMessage_side_909);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_909.Out)
		{
			Relay_False_908();
		}
	}

	private void Relay_In_912()
	{
		logic_uScriptCon_CompareBool_Bool_912 = local_PlayerDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_912.In(logic_uScriptCon_CompareBool_Bool_912);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_912.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_912.False;
		if (num)
		{
			Relay_In_909();
		}
		if (flag)
		{
			Relay_In_296();
		}
	}

	private void Relay_In_914()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_914.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_914.Out)
		{
			Relay_In_960();
		}
	}

	private void Relay_In_916()
	{
		logic_uScriptCon_CompareBool_Bool_916 = local_OutOfBounds_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_916.In(logic_uScriptCon_CompareBool_Bool_916);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_916.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_916.False;
		if (num)
		{
			Relay_In_919();
		}
		if (flag)
		{
			Relay_In_917();
		}
	}

	private void Relay_In_917()
	{
		logic_uScriptCon_CompareBool_Bool_917 = local_PlayerDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_917.In(logic_uScriptCon_CompareBool_Bool_917);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_917.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_917.False;
		if (num)
		{
			Relay_In_919();
		}
		if (flag)
		{
			Relay_In_920();
		}
	}

	private void Relay_In_919()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_919.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_919.Out)
		{
			Relay_True_290();
		}
	}

	private void Relay_In_920()
	{
		logic_uScript_Wait_uScript_Wait_920.In(logic_uScript_Wait_seconds_920, logic_uScript_Wait_repeat_920);
		if (logic_uScript_Wait_uScript_Wait_920.Waited)
		{
			Relay_True_289();
		}
	}

	private void Relay_In_922()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_922.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_922.Out)
		{
			Relay_In_925();
		}
	}

	private void Relay_In_924()
	{
		logic_uScript_IsTechInTrigger_triggerAreaName_924 = TriggerCrater;
		int num = 0;
		Array array = local_921_TankArray;
		if (logic_uScript_IsTechInTrigger_techs_924.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_IsTechInTrigger_techs_924, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_IsTechInTrigger_techs_924, num, array.Length);
		num += array.Length;
		logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_924.In(logic_uScript_IsTechInTrigger_triggerAreaName_924, ref logic_uScript_IsTechInTrigger_techs_924);
		local_921_TankArray = logic_uScript_IsTechInTrigger_techs_924;
		bool inRange = logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_924.InRange;
		bool outOfRange = logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_924.OutOfRange;
		if (inRange)
		{
			Relay_In_929();
		}
		if (outOfRange)
		{
			Relay_In_922();
		}
	}

	private void Relay_In_925()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_925.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_925.Out)
		{
			Relay_In_712();
		}
	}

	private void Relay_In_928()
	{
		int num = 0;
		Array array = local_927_TankArray;
		if (logic_uScript_CheckTechWeapons_techs_928.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_CheckTechWeapons_techs_928, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_CheckTechWeapons_techs_928, num, array.Length);
		num += array.Length;
		logic_uScript_CheckTechWeapons_WeaponBlockType_928 = PrototypeWeaponBlock;
		logic_uScript_CheckTechWeapons_uScript_CheckTechWeapons_928.In(logic_uScript_CheckTechWeapons_techs_928, logic_uScript_CheckTechWeapons_WeaponBlockType_928);
		bool hasOnlyGivenWeapon = logic_uScript_CheckTechWeapons_uScript_CheckTechWeapons_928.HasOnlyGivenWeapon;
		bool hasNoWeapons = logic_uScript_CheckTechWeapons_uScript_CheckTechWeapons_928.HasNoWeapons;
		bool hasOtherWeapons = logic_uScript_CheckTechWeapons_uScript_CheckTechWeapons_928.HasOtherWeapons;
		if (hasOnlyGivenWeapon)
		{
			Relay_In_712();
		}
		if (hasNoWeapons)
		{
			Relay_True_1043();
		}
		if (hasOtherWeapons)
		{
			Relay_True_321();
		}
	}

	private void Relay_In_929()
	{
		int num = 0;
		Array array = local_921_TankArray;
		if (logic_uScript_IsTechFriendlyToPlayer_techsIn_929.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_IsTechFriendlyToPlayer_techsIn_929, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_IsTechFriendlyToPlayer_techsIn_929, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = local_927_TankArray;
		if (logic_uScript_IsTechFriendlyToPlayer_techsOut_929.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_IsTechFriendlyToPlayer_techsOut_929, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_IsTechFriendlyToPlayer_techsOut_929, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_IsTechFriendlyToPlayer_uScript_IsTechFriendlyToPlayer_929.In(logic_uScript_IsTechFriendlyToPlayer_techsIn_929, ref logic_uScript_IsTechFriendlyToPlayer_techsOut_929);
		local_927_TankArray = logic_uScript_IsTechFriendlyToPlayer_techsOut_929;
		bool num3 = logic_uScript_IsTechFriendlyToPlayer_uScript_IsTechFriendlyToPlayer_929.True;
		bool flag = logic_uScript_IsTechFriendlyToPlayer_uScript_IsTechFriendlyToPlayer_929.False;
		if (num3)
		{
			Relay_In_928();
		}
		if (flag)
		{
			Relay_In_925();
		}
	}

	private void Relay_In_956()
	{
		logic_uScript_SetBatteryChargeAmount_tech_956 = local_vehicleTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_956.In(logic_uScript_SetBatteryChargeAmount_tech_956, logic_uScript_SetBatteryChargeAmount_chargeAmount_956);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_956.Out)
		{
			Relay_In_521();
		}
	}

	private void Relay_In_957()
	{
		logic_uScript_SetBatteryChargeAmount_tech_957 = local_vehicleTech4_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_957.In(logic_uScript_SetBatteryChargeAmount_tech_957, logic_uScript_SetBatteryChargeAmount_chargeAmount_957);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_957.Out)
		{
			Relay_In_552();
		}
	}

	private void Relay_In_958()
	{
		logic_uScript_SetBatteryChargeAmount_tech_958 = local_vehicleTech3_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_958.In(logic_uScript_SetBatteryChargeAmount_tech_958, logic_uScript_SetBatteryChargeAmount_chargeAmount_958);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_958.Out)
		{
			Relay_In_483();
		}
	}

	private void Relay_In_959()
	{
		logic_uScript_SetBatteryChargeAmount_tech_959 = local_vehicleTech2_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_959.In(logic_uScript_SetBatteryChargeAmount_tech_959, logic_uScript_SetBatteryChargeAmount_chargeAmount_959);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_959.Out)
		{
			Relay_In_398();
		}
	}

	private void Relay_In_960()
	{
		int num = 0;
		Array nPCPaymentPoint = NPCPaymentPoint;
		if (logic_uScript_GetAndCheckTechs_techData_960.Length != num + nPCPaymentPoint.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_960, num + nPCPaymentPoint.Length);
		}
		Array.Copy(nPCPaymentPoint, 0, logic_uScript_GetAndCheckTechs_techData_960, num, nPCPaymentPoint.Length);
		num += nPCPaymentPoint.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_960 = owner_Connection_796;
		int num2 = 0;
		Array array = local_NPCPaymentPoints_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_960.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_960, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_960, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_960 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_960.In(logic_uScript_GetAndCheckTechs_techData_960, logic_uScript_GetAndCheckTechs_ownerNode_960, ref logic_uScript_GetAndCheckTechs_techs_960);
		local_NPCPaymentPoints_TankArray = logic_uScript_GetAndCheckTechs_techs_960;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_960.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_960.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_795();
		}
		if (someAlive)
		{
			Relay_AtIndex_795();
		}
	}

	private void Relay_In_962()
	{
		int num = 0;
		Array array = vehicleSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_962.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_962, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_962, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_962 = owner_Connection_966;
		int num2 = 0;
		Array array2 = local_vehicleTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_962.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_962, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_962, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_962 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_962.In(logic_uScript_GetAndCheckTechs_techData_962, logic_uScript_GetAndCheckTechs_ownerNode_962, ref logic_uScript_GetAndCheckTechs_techs_962);
		local_vehicleTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_962;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_962.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_962.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_962.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_962.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_964();
		}
		if (someAlive)
		{
			Relay_AtIndex_964();
		}
		if (allDead)
		{
			Relay_In_979();
		}
		if (waitingToSpawn)
		{
			Relay_In_979();
		}
	}

	private void Relay_AtIndex_964()
	{
		int num = 0;
		Array array = local_vehicleTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_964.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_964, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_964, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_964.AtIndex(ref logic_uScript_AccessListTech_techList_964, logic_uScript_AccessListTech_index_964, out logic_uScript_AccessListTech_value_964);
		local_vehicleTechs_TankArray = logic_uScript_AccessListTech_techList_964;
		local_vehicleTech_Tank = logic_uScript_AccessListTech_value_964;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_964.Out)
		{
			Relay_In_988();
		}
	}

	private void Relay_AtIndex_971()
	{
		int num = 0;
		Array array = local_vehicleTechs2_TankArray;
		if (logic_uScript_AccessListTech_techList_971.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_971, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_971, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_971.AtIndex(ref logic_uScript_AccessListTech_techList_971, logic_uScript_AccessListTech_index_971, out logic_uScript_AccessListTech_value_971);
		local_vehicleTechs2_TankArray = logic_uScript_AccessListTech_techList_971;
		local_vehicleTech2_Tank = logic_uScript_AccessListTech_value_971;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_971.Out)
		{
			Relay_In_989();
		}
	}

	private void Relay_In_972()
	{
		int num = 0;
		Array array = vehicleSpawnData4;
		if (logic_uScript_GetAndCheckTechs_techData_972.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_972, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_972, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_972 = owner_Connection_973;
		int num2 = 0;
		Array array2 = local_vehicleTechs4_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_972.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_972, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_972, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_972 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_972.In(logic_uScript_GetAndCheckTechs_techData_972, logic_uScript_GetAndCheckTechs_ownerNode_972, ref logic_uScript_GetAndCheckTechs_techs_972);
		local_vehicleTechs4_TankArray = logic_uScript_GetAndCheckTechs_techs_972;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_972.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_972.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_972.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_972.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_977();
		}
		if (someAlive)
		{
			Relay_AtIndex_977();
		}
		if (allDead)
		{
			Relay_In_1021();
		}
		if (waitingToSpawn)
		{
			Relay_In_1021();
		}
	}

	private void Relay_AtIndex_977()
	{
		int num = 0;
		Array array = local_vehicleTechs4_TankArray;
		if (logic_uScript_AccessListTech_techList_977.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_977, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_977, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_977.AtIndex(ref logic_uScript_AccessListTech_techList_977, logic_uScript_AccessListTech_index_977, out logic_uScript_AccessListTech_value_977);
		local_vehicleTechs4_TankArray = logic_uScript_AccessListTech_techList_977;
		local_vehicleTech4_Tank = logic_uScript_AccessListTech_value_977;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_977.Out)
		{
			Relay_In_1022();
		}
	}

	private void Relay_In_978()
	{
		int num = 0;
		Array array = vehicleSpawnData2;
		if (logic_uScript_GetAndCheckTechs_techData_978.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_978, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_978, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_978 = owner_Connection_969;
		int num2 = 0;
		Array array2 = local_vehicleTechs2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_978.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_978, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_978, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_978 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_978.In(logic_uScript_GetAndCheckTechs_techData_978, logic_uScript_GetAndCheckTechs_ownerNode_978, ref logic_uScript_GetAndCheckTechs_techs_978);
		local_vehicleTechs2_TankArray = logic_uScript_GetAndCheckTechs_techs_978;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_978.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_978.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_978.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_978.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_971();
		}
		if (someAlive)
		{
			Relay_AtIndex_971();
		}
		if (allDead)
		{
			Relay_In_980();
		}
		if (waitingToSpawn)
		{
			Relay_In_980();
		}
	}

	private void Relay_In_979()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_979.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_979.Out)
		{
			Relay_In_978();
		}
	}

	private void Relay_In_980()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_980.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_980.Out)
		{
			Relay_In_986();
		}
	}

	private void Relay_AtIndex_981()
	{
		int num = 0;
		Array array = local_vehicleTechs3_TankArray;
		if (logic_uScript_AccessListTech_techList_981.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_981, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_981, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_981.AtIndex(ref logic_uScript_AccessListTech_techList_981, logic_uScript_AccessListTech_index_981, out logic_uScript_AccessListTech_value_981);
		local_vehicleTechs3_TankArray = logic_uScript_AccessListTech_techList_981;
		local_vehicleTech3_Tank = logic_uScript_AccessListTech_value_981;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_981.Out)
		{
			Relay_In_990();
		}
	}

	private void Relay_In_986()
	{
		int num = 0;
		Array array = vehicleSpawnData3;
		if (logic_uScript_GetAndCheckTechs_techData_986.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_986, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_986, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_986 = owner_Connection_982;
		int num2 = 0;
		Array array2 = local_vehicleTechs3_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_986.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_986, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_986, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_986 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_986.In(logic_uScript_GetAndCheckTechs_techData_986, logic_uScript_GetAndCheckTechs_ownerNode_986, ref logic_uScript_GetAndCheckTechs_techs_986);
		local_vehicleTechs3_TankArray = logic_uScript_GetAndCheckTechs_techs_986;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_986.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_986.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_986.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_986.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_981();
		}
		if (someAlive)
		{
			Relay_AtIndex_981();
		}
		if (allDead)
		{
			Relay_In_987();
		}
		if (waitingToSpawn)
		{
			Relay_In_987();
		}
	}

	private void Relay_In_987()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_987.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_987.Out)
		{
			Relay_In_972();
		}
	}

	private void Relay_In_988()
	{
		logic_uScript_SetBatteryChargeAmount_tech_988 = local_vehicleTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_988.In(logic_uScript_SetBatteryChargeAmount_tech_988, logic_uScript_SetBatteryChargeAmount_chargeAmount_988);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_988.Out)
		{
			Relay_In_978();
		}
	}

	private void Relay_In_989()
	{
		logic_uScript_SetBatteryChargeAmount_tech_989 = local_vehicleTech2_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_989.In(logic_uScript_SetBatteryChargeAmount_tech_989, logic_uScript_SetBatteryChargeAmount_chargeAmount_989);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_989.Out)
		{
			Relay_In_986();
		}
	}

	private void Relay_In_990()
	{
		logic_uScript_SetBatteryChargeAmount_tech_990 = local_vehicleTech3_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_990.In(logic_uScript_SetBatteryChargeAmount_tech_990, logic_uScript_SetBatteryChargeAmount_chargeAmount_990);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_990.Out)
		{
			Relay_In_972();
		}
	}

	private void Relay_AtIndex_995()
	{
		int num = 0;
		Array array = local_vehicleTechsRebuy1_TankArray;
		if (logic_uScript_AccessListTech_techList_995.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_995, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_995, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_995.AtIndex(ref logic_uScript_AccessListTech_techList_995, logic_uScript_AccessListTech_index_995, out logic_uScript_AccessListTech_value_995);
		local_vehicleTechsRebuy1_TankArray = logic_uScript_AccessListTech_techList_995;
		local_vehicleTechRebuy1_Tank = logic_uScript_AccessListTech_value_995;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_995.Out)
		{
			Relay_In_1016();
		}
	}

	private void Relay_AtIndex_996()
	{
		int num = 0;
		Array array = local_vehicleTechsRebuy4_TankArray;
		if (logic_uScript_AccessListTech_techList_996.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_996, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_996, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_996.AtIndex(ref logic_uScript_AccessListTech_techList_996, logic_uScript_AccessListTech_index_996, out logic_uScript_AccessListTech_value_996);
		local_vehicleTechsRebuy4_TankArray = logic_uScript_AccessListTech_techList_996;
		local_vehicleTechRebuy4_Tank = logic_uScript_AccessListTech_value_996;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_996.Out)
		{
			Relay_In_1023();
		}
	}

	private void Relay_In_998()
	{
		logic_uScript_SetBatteryChargeAmount_tech_998 = local_vehicleTechRebuy2_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_998.In(logic_uScript_SetBatteryChargeAmount_tech_998, logic_uScript_SetBatteryChargeAmount_chargeAmount_998);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_998.Out)
		{
			Relay_In_1003();
		}
	}

	private void Relay_In_1001()
	{
		logic_uScript_SetBatteryChargeAmount_tech_1001 = local_vehicleTechRebuy3_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1001.In(logic_uScript_SetBatteryChargeAmount_tech_1001, logic_uScript_SetBatteryChargeAmount_chargeAmount_1001);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1001.Out)
		{
			Relay_In_1004();
		}
	}

	private void Relay_In_1003()
	{
		int num = 0;
		Array array = vehicleSpawnDataRebuy3;
		if (logic_uScript_GetAndCheckTechs_techData_1003.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_1003, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_1003, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_1003 = owner_Connection_997;
		int num2 = 0;
		Array array2 = local_vehicleTechsRebuy3_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_1003.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_1003, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_1003, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_1003 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1003.In(logic_uScript_GetAndCheckTechs_techData_1003, logic_uScript_GetAndCheckTechs_ownerNode_1003, ref logic_uScript_GetAndCheckTechs_techs_1003);
		local_vehicleTechsRebuy3_TankArray = logic_uScript_GetAndCheckTechs_techs_1003;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1003.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1003.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1003.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1003.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_1009();
		}
		if (someAlive)
		{
			Relay_AtIndex_1009();
		}
		if (allDead)
		{
			Relay_In_1019();
		}
		if (waitingToSpawn)
		{
			Relay_In_1019();
		}
	}

	private void Relay_In_1004()
	{
		int num = 0;
		Array array = vehicleSpawnDataRebuy4;
		if (logic_uScript_GetAndCheckTechs_techData_1004.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_1004, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_1004, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_1004 = owner_Connection_1012;
		int num2 = 0;
		Array array2 = local_vehicleTechsRebuy4_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_1004.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_1004, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_1004, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_1004 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1004.In(logic_uScript_GetAndCheckTechs_techData_1004, logic_uScript_GetAndCheckTechs_ownerNode_1004, ref logic_uScript_GetAndCheckTechs_techs_1004);
		local_vehicleTechsRebuy4_TankArray = logic_uScript_GetAndCheckTechs_techs_1004;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1004.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1004.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_996();
		}
		if (someAlive)
		{
			Relay_AtIndex_996();
		}
	}

	private void Relay_In_1007()
	{
		int num = 0;
		Array array = vehicleSpawnDataRebuy1;
		if (logic_uScript_GetAndCheckTechs_techData_1007.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_1007, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_1007, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_1007 = owner_Connection_1020;
		int num2 = 0;
		Array array2 = local_vehicleTechsRebuy1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_1007.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_1007, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_1007, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_1007 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1007.In(logic_uScript_GetAndCheckTechs_techData_1007, logic_uScript_GetAndCheckTechs_ownerNode_1007, ref logic_uScript_GetAndCheckTechs_techs_1007);
		local_vehicleTechsRebuy1_TankArray = logic_uScript_GetAndCheckTechs_techs_1007;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1007.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1007.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1007.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1007.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_995();
		}
		if (someAlive)
		{
			Relay_AtIndex_995();
		}
		if (allDead)
		{
			Relay_In_1013();
		}
		if (waitingToSpawn)
		{
			Relay_In_1013();
		}
	}

	private void Relay_In_1008()
	{
		int num = 0;
		Array array = vehicleSpawnDataRebuy2;
		if (logic_uScript_GetAndCheckTechs_techData_1008.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_1008, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_1008, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_1008 = owner_Connection_1018;
		int num2 = 0;
		Array array2 = local_vehicleTechsRebuy2_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_1008.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_1008, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_1008, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_1008 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1008.In(logic_uScript_GetAndCheckTechs_techData_1008, logic_uScript_GetAndCheckTechs_ownerNode_1008, ref logic_uScript_GetAndCheckTechs_techs_1008);
		local_vehicleTechsRebuy2_TankArray = logic_uScript_GetAndCheckTechs_techs_1008;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1008.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1008.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1008.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_1008.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_1015();
		}
		if (someAlive)
		{
			Relay_AtIndex_1015();
		}
		if (allDead)
		{
			Relay_In_1010();
		}
		if (waitingToSpawn)
		{
			Relay_In_1010();
		}
	}

	private void Relay_AtIndex_1009()
	{
		int num = 0;
		Array array = local_vehicleTechsRebuy3_TankArray;
		if (logic_uScript_AccessListTech_techList_1009.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_1009, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_1009, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_1009.AtIndex(ref logic_uScript_AccessListTech_techList_1009, logic_uScript_AccessListTech_index_1009, out logic_uScript_AccessListTech_value_1009);
		local_vehicleTechsRebuy3_TankArray = logic_uScript_AccessListTech_techList_1009;
		local_vehicleTechRebuy3_Tank = logic_uScript_AccessListTech_value_1009;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_1009.Out)
		{
			Relay_In_1001();
		}
	}

	private void Relay_In_1010()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1010.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1010.Out)
		{
			Relay_In_1003();
		}
	}

	private void Relay_In_1013()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1013.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1013.Out)
		{
			Relay_In_1008();
		}
	}

	private void Relay_AtIndex_1015()
	{
		int num = 0;
		Array array = local_vehicleTechsRebuy2_TankArray;
		if (logic_uScript_AccessListTech_techList_1015.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_1015, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_1015, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_1015.AtIndex(ref logic_uScript_AccessListTech_techList_1015, logic_uScript_AccessListTech_index_1015, out logic_uScript_AccessListTech_value_1015);
		local_vehicleTechsRebuy2_TankArray = logic_uScript_AccessListTech_techList_1015;
		local_vehicleTechRebuy2_Tank = logic_uScript_AccessListTech_value_1015;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_1015.Out)
		{
			Relay_In_998();
		}
	}

	private void Relay_In_1016()
	{
		logic_uScript_SetBatteryChargeAmount_tech_1016 = local_vehicleTechRebuy1_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1016.In(logic_uScript_SetBatteryChargeAmount_tech_1016, logic_uScript_SetBatteryChargeAmount_chargeAmount_1016);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1016.Out)
		{
			Relay_In_1008();
		}
	}

	private void Relay_In_1019()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1019.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1019.Out)
		{
			Relay_In_1004();
		}
	}

	private void Relay_In_1021()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1021.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1021.Out)
		{
			Relay_In_1007();
		}
	}

	private void Relay_In_1022()
	{
		logic_uScript_SetBatteryChargeAmount_tech_1022 = local_vehicleTech4_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1022.In(logic_uScript_SetBatteryChargeAmount_tech_1022, logic_uScript_SetBatteryChargeAmount_chargeAmount_1022);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1022.Out)
		{
			Relay_In_1007();
		}
	}

	private void Relay_In_1023()
	{
		logic_uScript_SetBatteryChargeAmount_tech_1023 = local_vehicleTechRebuy4_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_1023.In(logic_uScript_SetBatteryChargeAmount_tech_1023, logic_uScript_SetBatteryChargeAmount_chargeAmount_1023);
	}

	private void Relay_In_1026()
	{
		logic_uScript_AddMessage_messageData_1026 = msgNPCAllPlayersMustBeInAreaForTest;
		logic_uScript_AddMessage_speaker_1026 = SpeakerNPC;
		logic_uScript_AddMessage_Return_1026 = logic_uScript_AddMessage_uScript_AddMessage_1026.In(logic_uScript_AddMessage_messageData_1026, logic_uScript_AddMessage_speaker_1026);
		if (logic_uScript_AddMessage_uScript_AddMessage_1026.Out)
		{
			Relay_True_1028();
		}
	}

	private void Relay_True_1028()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1028.True(out logic_uScriptAct_SetBool_Target_1028);
		local_msgAllPlayersMustBeInShown_System_Boolean = logic_uScriptAct_SetBool_Target_1028;
	}

	private void Relay_False_1028()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1028.False(out logic_uScriptAct_SetBool_Target_1028);
		local_msgAllPlayersMustBeInShown_System_Boolean = logic_uScriptAct_SetBool_Target_1028;
	}

	private void Relay_Pause_1030()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_1030.Pause();
	}

	private void Relay_UnPause_1030()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_1030.UnPause();
	}

	private void Relay_In_1031()
	{
		logic_uScriptCon_CompareBool_Bool_1031 = local_msgAllPlayersMustBeInShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1031.In(logic_uScriptCon_CompareBool_Bool_1031);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1031.False)
		{
			Relay_In_1026();
		}
	}

	private void Relay_Save_Out_1033()
	{
		Relay_Save_1045();
	}

	private void Relay_Load_Out_1033()
	{
		Relay_Load_1045();
	}

	private void Relay_Restart_Out_1033()
	{
		Relay_Set_False_1045();
	}

	private void Relay_Save_1033()
	{
		logic_SubGraph_SaveLoadBool_boolean_1033 = local_msgAllPlayersMustBeInShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_1033 = local_msgAllPlayersMustBeInShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Save(ref logic_SubGraph_SaveLoadBool_boolean_1033, logic_SubGraph_SaveLoadBool_boolAsVariable_1033, logic_SubGraph_SaveLoadBool_uniqueID_1033);
	}

	private void Relay_Load_1033()
	{
		logic_SubGraph_SaveLoadBool_boolean_1033 = local_msgAllPlayersMustBeInShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_1033 = local_msgAllPlayersMustBeInShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Load(ref logic_SubGraph_SaveLoadBool_boolean_1033, logic_SubGraph_SaveLoadBool_boolAsVariable_1033, logic_SubGraph_SaveLoadBool_uniqueID_1033);
	}

	private void Relay_Set_True_1033()
	{
		logic_SubGraph_SaveLoadBool_boolean_1033 = local_msgAllPlayersMustBeInShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_1033 = local_msgAllPlayersMustBeInShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_1033, logic_SubGraph_SaveLoadBool_boolAsVariable_1033, logic_SubGraph_SaveLoadBool_uniqueID_1033);
	}

	private void Relay_Set_False_1033()
	{
		logic_SubGraph_SaveLoadBool_boolean_1033 = local_msgAllPlayersMustBeInShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_1033 = local_msgAllPlayersMustBeInShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1033.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_1033, logic_SubGraph_SaveLoadBool_boolAsVariable_1033, logic_SubGraph_SaveLoadBool_uniqueID_1033);
	}

	private void Relay_In_1037()
	{
		logic_uScript_AddMessage_messageData_1037 = msgNPCYouCanRebuy;
		logic_uScript_AddMessage_speaker_1037 = SpeakerNPC;
		logic_uScript_AddMessage_Return_1037 = logic_uScript_AddMessage_uScript_AddMessage_1037.In(logic_uScript_AddMessage_messageData_1037, logic_uScript_AddMessage_speaker_1037);
		if (logic_uScript_AddMessage_uScript_AddMessage_1037.Shown)
		{
			Relay_True_1040();
		}
	}

	private void Relay_In_1039()
	{
		logic_uScriptCon_CompareBool_Bool_1039 = local_msgRebuyShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1039.In(logic_uScriptCon_CompareBool_Bool_1039);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1039.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1039.False;
		if (num)
		{
			Relay_In_760();
		}
		if (flag)
		{
			Relay_In_1085();
		}
	}

	private void Relay_True_1040()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1040.True(out logic_uScriptAct_SetBool_Target_1040);
		local_msgRebuyShown_System_Boolean = logic_uScriptAct_SetBool_Target_1040;
	}

	private void Relay_False_1040()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1040.False(out logic_uScriptAct_SetBool_Target_1040);
		local_msgRebuyShown_System_Boolean = logic_uScriptAct_SetBool_Target_1040;
	}

	private void Relay_In_1041()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1041.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1041.Multiplayer)
		{
			Relay_In_1031();
		}
	}

	private void Relay_True_1043()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1043.True(out logic_uScriptAct_SetBool_Target_1043);
		local_PlayerDetachedWeapon_System_Boolean = logic_uScriptAct_SetBool_Target_1043;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1043.Out)
		{
			Relay_True_319();
		}
	}

	private void Relay_False_1043()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1043.False(out logic_uScriptAct_SetBool_Target_1043);
		local_PlayerDetachedWeapon_System_Boolean = logic_uScriptAct_SetBool_Target_1043;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1043.Out)
		{
			Relay_True_319();
		}
	}

	private void Relay_Save_Out_1045()
	{
		Relay_Save_1059();
	}

	private void Relay_Load_Out_1045()
	{
		Relay_Load_1059();
	}

	private void Relay_Restart_Out_1045()
	{
		Relay_Set_False_1059();
	}

	private void Relay_Save_1045()
	{
		logic_SubGraph_SaveLoadBool_boolean_1045 = local_PlayerDetachedWeapon_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_1045 = local_PlayerDetachedWeapon_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Save(ref logic_SubGraph_SaveLoadBool_boolean_1045, logic_SubGraph_SaveLoadBool_boolAsVariable_1045, logic_SubGraph_SaveLoadBool_uniqueID_1045);
	}

	private void Relay_Load_1045()
	{
		logic_SubGraph_SaveLoadBool_boolean_1045 = local_PlayerDetachedWeapon_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_1045 = local_PlayerDetachedWeapon_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Load(ref logic_SubGraph_SaveLoadBool_boolean_1045, logic_SubGraph_SaveLoadBool_boolAsVariable_1045, logic_SubGraph_SaveLoadBool_uniqueID_1045);
	}

	private void Relay_Set_True_1045()
	{
		logic_SubGraph_SaveLoadBool_boolean_1045 = local_PlayerDetachedWeapon_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_1045 = local_PlayerDetachedWeapon_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_1045, logic_SubGraph_SaveLoadBool_boolAsVariable_1045, logic_SubGraph_SaveLoadBool_uniqueID_1045);
	}

	private void Relay_Set_False_1045()
	{
		logic_SubGraph_SaveLoadBool_boolean_1045 = local_PlayerDetachedWeapon_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_1045 = local_PlayerDetachedWeapon_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1045.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_1045, logic_SubGraph_SaveLoadBool_boolAsVariable_1045, logic_SubGraph_SaveLoadBool_uniqueID_1045);
	}

	private void Relay_In_1047()
	{
		logic_uScriptCon_CompareBool_Bool_1047 = local_PlayerDetachedWeapon_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1047.In(logic_uScriptCon_CompareBool_Bool_1047);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1047.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1047.False;
		if (num)
		{
			Relay_In_1048();
		}
		if (flag)
		{
			Relay_In_696();
		}
	}

	private void Relay_In_1048()
	{
		int num = 0;
		Array array = msgNPCTestFailedPlayerDetachedWeapon;
		if (logic_uScript_AddOnScreenMessage_locString_1048.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_1048, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_1048, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_1048 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_1048 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1048.In(logic_uScript_AddOnScreenMessage_locString_1048, logic_uScript_AddOnScreenMessage_msgPriority_1048, logic_uScript_AddOnScreenMessage_holdMsg_1048, logic_uScript_AddOnScreenMessage_tag_1048, logic_uScript_AddOnScreenMessage_speaker_1048, logic_uScript_AddOnScreenMessage_side_1048);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1048.Out)
		{
			Relay_False_1050();
		}
	}

	private void Relay_True_1050()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1050.True(out logic_uScriptAct_SetBool_Target_1050);
		local_PlayerDetachedWeapon_System_Boolean = logic_uScriptAct_SetBool_Target_1050;
	}

	private void Relay_False_1050()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1050.False(out logic_uScriptAct_SetBool_Target_1050);
		local_PlayerDetachedWeapon_System_Boolean = logic_uScriptAct_SetBool_Target_1050;
	}

	private void Relay_In_1053()
	{
		logic_uScriptCon_CompareBool_Bool_1053 = local_BuyMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1053.In(logic_uScriptCon_CompareBool_Bool_1053);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1053.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1053.False;
		if (num)
		{
			Relay_In_1055();
		}
		if (flag)
		{
			Relay_In_622();
		}
	}

	private void Relay_In_1055()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1055.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1055.Out)
		{
			Relay_In_586();
		}
	}

	private void Relay_True_1057()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1057.True(out logic_uScriptAct_SetBool_Target_1057);
		local_BuyMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_1057;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1057.Out)
		{
			Relay_In_527();
		}
	}

	private void Relay_False_1057()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1057.False(out logic_uScriptAct_SetBool_Target_1057);
		local_BuyMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_1057;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1057.Out)
		{
			Relay_In_527();
		}
	}

	private void Relay_Save_Out_1059()
	{
		Relay_Save_752();
	}

	private void Relay_Load_Out_1059()
	{
		Relay_Load_752();
	}

	private void Relay_Restart_Out_1059()
	{
		Relay_Restart_752();
	}

	private void Relay_Save_1059()
	{
		logic_SubGraph_SaveLoadBool_boolean_1059 = local_BuyMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_1059 = local_BuyMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Save(ref logic_SubGraph_SaveLoadBool_boolean_1059, logic_SubGraph_SaveLoadBool_boolAsVariable_1059, logic_SubGraph_SaveLoadBool_uniqueID_1059);
	}

	private void Relay_Load_1059()
	{
		logic_SubGraph_SaveLoadBool_boolean_1059 = local_BuyMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_1059 = local_BuyMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Load(ref logic_SubGraph_SaveLoadBool_boolean_1059, logic_SubGraph_SaveLoadBool_boolAsVariable_1059, logic_SubGraph_SaveLoadBool_uniqueID_1059);
	}

	private void Relay_Set_True_1059()
	{
		logic_SubGraph_SaveLoadBool_boolean_1059 = local_BuyMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_1059 = local_BuyMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_1059, logic_SubGraph_SaveLoadBool_boolAsVariable_1059, logic_SubGraph_SaveLoadBool_uniqueID_1059);
	}

	private void Relay_Set_False_1059()
	{
		logic_SubGraph_SaveLoadBool_boolean_1059 = local_BuyMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_1059 = local_BuyMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_1059.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_1059, logic_SubGraph_SaveLoadBool_boolAsVariable_1059, logic_SubGraph_SaveLoadBool_uniqueID_1059);
	}

	private void Relay_True_1060()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1060.True(out logic_uScriptAct_SetBool_Target_1060);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_1060;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1060.Out)
		{
			Relay_True_290();
		}
	}

	private void Relay_False_1060()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1060.False(out logic_uScriptAct_SetBool_Target_1060);
		local_PlayerDead_System_Boolean = logic_uScriptAct_SetBool_Target_1060;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1060.Out)
		{
			Relay_True_290();
		}
	}

	private void Relay_TechDestroyedEvent_1062()
	{
		local_1068_Tank = event_UnityEngine_GameObject_Tech_1062;
		Relay_In_1063();
	}

	private void Relay_In_1063()
	{
		logic_uScriptCon_CompareBool_Bool_1063 = local_TestStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1063.In(logic_uScriptCon_CompareBool_Bool_1063);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1063.True)
		{
			Relay_True_1060();
		}
	}

	private void Relay_In_1067()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1067.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1067.Out)
		{
			Relay_In_285();
		}
	}

	private void Relay_True_1069()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1069.True(out logic_uScriptAct_SetBool_Target_1069);
		local_TestStarted_System_Boolean = logic_uScriptAct_SetBool_Target_1069;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1069.Out)
		{
			Relay_True_1075();
		}
	}

	private void Relay_False_1069()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1069.False(out logic_uScriptAct_SetBool_Target_1069);
		local_TestStarted_System_Boolean = logic_uScriptAct_SetBool_Target_1069;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1069.Out)
		{
			Relay_True_1075();
		}
	}

	private void Relay_True_1072()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1072.True(out logic_uScriptAct_SetBool_Target_1072);
		local_Rebriefed_System_Boolean = logic_uScriptAct_SetBool_Target_1072;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1072.Out)
		{
			Relay_In_1078();
		}
	}

	private void Relay_False_1072()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1072.False(out logic_uScriptAct_SetBool_Target_1072);
		local_Rebriefed_System_Boolean = logic_uScriptAct_SetBool_Target_1072;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1072.Out)
		{
			Relay_In_1078();
		}
	}

	private void Relay_In_1073()
	{
		logic_uScriptCon_CompareBool_Bool_1073 = local_TestStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1073.In(logic_uScriptCon_CompareBool_Bool_1073);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1073.True)
		{
			Relay_False_1069();
		}
	}

	private void Relay_True_1075()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1075.True(out logic_uScriptAct_SetBool_Target_1075);
		local_TestAttempted_System_Boolean = logic_uScriptAct_SetBool_Target_1075;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1075.Out)
		{
			Relay_False_1072();
		}
	}

	private void Relay_False_1075()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1075.False(out logic_uScriptAct_SetBool_Target_1075);
		local_TestAttempted_System_Boolean = logic_uScriptAct_SetBool_Target_1075;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1075.Out)
		{
			Relay_False_1072();
		}
	}

	private void Relay_In_1078()
	{
		int num = 0;
		Array array = msgNPCTestFailedSaveLoaded;
		if (logic_uScript_AddOnScreenMessage_locString_1078.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_1078, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_1078, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_1078 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_1078 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1078.In(logic_uScript_AddOnScreenMessage_locString_1078, logic_uScript_AddOnScreenMessage_msgPriority_1078, logic_uScript_AddOnScreenMessage_holdMsg_1078, logic_uScript_AddOnScreenMessage_tag_1078, logic_uScript_AddOnScreenMessage_speaker_1078, logic_uScript_AddOnScreenMessage_side_1078);
	}

	private void Relay_In_1079()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_1079 = owner_Connection_1080;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_1079.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_1079);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_1079.Out)
		{
			Relay_In_269();
		}
	}

	private void Relay_True_1082()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1082.True(out logic_uScriptAct_SetBool_Target_1082);
		local_msgAllPlayersMustBeInShown_System_Boolean = logic_uScriptAct_SetBool_Target_1082;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1082.Out)
		{
			Relay_In_884();
		}
	}

	private void Relay_False_1082()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1082.False(out logic_uScriptAct_SetBool_Target_1082);
		local_msgAllPlayersMustBeInShown_System_Boolean = logic_uScriptAct_SetBool_Target_1082;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1082.Out)
		{
			Relay_In_884();
		}
	}

	private void Relay_In_1084()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1084.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_1084.Out)
		{
			Relay_In_1086();
		}
	}

	private void Relay_In_1085()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1085.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1085.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1085.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_1037();
		}
		if (multiplayer)
		{
			Relay_In_760();
		}
	}

	private void Relay_In_1086()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_1086 = TriggerCrater;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1086.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_1086);
		bool allInRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1086.AllInRange;
		bool someOutOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1086.SomeOutOfRange;
		if (allInRange)
		{
			Relay_In_689();
		}
		if (someOutOfRange)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_1088()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_1088 = TriggerCrater;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1088.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_1088);
		bool allInRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1088.AllInRange;
		bool someOutOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1088.SomeOutOfRange;
		if (allInRange)
		{
			Relay_In_1084();
		}
		if (someOutOfRange)
		{
			Relay_In_1089();
		}
	}

	private void Relay_In_1089()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_1089 = TriggerNPC;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1089.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_1089);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1089.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1089.OutOfRange;
		if (inRange)
		{
			Relay_In_914();
		}
		if (outOfRange)
		{
			Relay_False_854();
		}
	}

	private void Relay_In_1090()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_1090 = TriggerNPC;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1090.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_1090);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1090.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_1090.OutOfRange;
		if (inRange)
		{
			Relay_In_228();
		}
		if (outOfRange)
		{
			Relay_In_221();
		}
	}

	private void Relay_In_1091()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1091.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1091.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_1091.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_139();
		}
		if (multiplayer)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_1093()
	{
		logic_uScriptCon_CompareBool_Bool_1093 = local_BlockLimitCritical_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1093.In(logic_uScriptCon_CompareBool_Bool_1093);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1093.False)
		{
			Relay_True_1057();
		}
	}
}
