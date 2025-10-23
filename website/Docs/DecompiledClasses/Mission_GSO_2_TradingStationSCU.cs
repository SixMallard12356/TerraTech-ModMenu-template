using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_GSO_2_TradingStationSCU : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public SpawnTechData[] alliedTechData = new SpawnTechData[0];

	public float clearSceneryRadius;

	public SpawnTechData[] enemyTechData = new SpawnTechData[0];

	private Quaternion local_101_UnityEngine_Quaternion = new Quaternion(0f, 0f, 0f, 0f);

	private GameHints.HintID local_198_GameHints_HintID = GameHints.HintID.RecallStoredTech;

	private Tank local_AlliedTech_Tank;

	private Tank[] local_AlliedTechs_TankArray = new Tank[0];

	private Vector3 local_EncounterPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_EnemiesDead_System_Boolean;

	private Tank[] local_EnemyTechs_TankArray = new Tank[0];

	private Tank local_GSOVendor_Tank;

	private bool local_Init_System_Boolean;

	private bool local_InventoryOpened_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_Msg07OpenInventory_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg07OpenInventory_Pad2_ManOnScreenMessages_OnScreenMessage;

	private bool local_msgDefeatEnemies_System_Boolean;

	private bool local_MsgSCUEnabled_System_Boolean;

	private bool local_msgTurretIdle_System_Boolean;

	private Tank local_PlayerTech_Tank;

	private int local_Stage_System_Int32 = 1;

	private bool local_Stage3Init_System_Boolean;

	private bool local_Stage4Init_System_Boolean;

	private bool local_TechsSpawned_System_Boolean;

	private Vector3 local_VendorPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	public uScript_AddMessage.MessageSpeaker messageSpeakerDefault;

	public uScript_AddMessage.MessageSpeaker messageSpeakerTurret;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01aDefeatEnemies;

	public uScript_AddMessage.MessageData msg01bDefeatEnemies;

	public uScript_AddMessage.MessageData msg02EnemiesDefeated;

	public uScript_AddMessage.MessageData msg03TurretIdle;

	public uScript_AddMessage.MessageData msg04aOpenAIMenu;

	public uScript_AddMessage.MessageData msg04aOpenAIMenu_Pad1;

	public uScript_AddMessage.MessageData msg04aOpenAIMenu_Pad2;

	public uScript_AddMessage.MessageData msg04bSelectGuard;

	public uScript_AddMessage.MessageData msg04bSelectGuard_Pad;

	public uScript_AddMessage.MessageData msg05AllyOrdered;

	public uScript_AddMessage.MessageData msg06SCUEnabled;

	public uScript_AddMessage.MessageData msg07OpenInventory;

	public uScript_AddMessage.MessageData msg07OpenInventory_Pad1;

	public uScript_AddMessage.MessageData msg07OpenInventory_Pad2;

	public uScript_AddMessage.MessageData msg08MissionComplete;

	public float tradingStationFoundDist;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_35;

	private GameObject owner_Connection_39;

	private GameObject owner_Connection_103;

	private GameObject owner_Connection_124;

	private GameObject owner_Connection_129;

	private GameObject owner_Connection_132;

	private GameObject owner_Connection_136;

	private GameObject owner_Connection_154;

	private GameObject owner_Connection_161;

	private GameObject owner_Connection_171;

	private GameObject owner_Connection_175;

	private GameObject owner_Connection_180;

	private GameObject owner_Connection_194;

	private GameObject owner_Connection_213;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_5;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_5 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_5 = "Init";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_6 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_6;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_6 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_6 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_8 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_8;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_10;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_18 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_18;

	private bool logic_uScript_FinishEncounter_Out_18 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_19 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_19;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_19;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_22;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_22 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_22 = "Stage3Init";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_24 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_24;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_24;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_24;

	private bool logic_uScript_AddMessage_Out_24 = true;

	private bool logic_uScript_AddMessage_Shown_24 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_27;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_27 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_27 = "Stage4Init";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_29;

	private bool logic_uScriptCon_CompareBool_True_29 = true;

	private bool logic_uScriptCon_CompareBool_False_29 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_31;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_31;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_33 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_33;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_33 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_33;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_33 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_33 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_33 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_33 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_38 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_38 = new Tank[0];

	private int logic_uScript_AccessListTech_index_38;

	private Tank logic_uScript_AccessListTech_value_38;

	private bool logic_uScript_AccessListTech_Out_38 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_42 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_42 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_42;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_42 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_42;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_42 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_42 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_42 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_42 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_43 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_43 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_44 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_44 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_44;

	private bool logic_uScript_SetTankInvulnerable_Out_44 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_47 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_47;

	private bool logic_uScriptAct_SetBool_Out_47 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_47 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_47 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_48 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_49;

	private bool logic_uScriptCon_CompareBool_True_49 = true;

	private bool logic_uScriptCon_CompareBool_False_49 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_51 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_51;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_51 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_51 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_53 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_53 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_53 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_55 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_55 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_55 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_59 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_59;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_59;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_59;

	private bool logic_uScript_AddMessage_Out_59 = true;

	private bool logic_uScript_AddMessage_Shown_59 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_60 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_60;

	private bool logic_uScriptAct_SetBool_Out_60 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_60 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_60 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_63 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_63 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_63;

	private bool logic_uScript_SetTankInvulnerable_Out_63 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_64 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_64;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_64;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_68 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_68;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_68;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_68;

	private bool logic_uScript_AddMessage_Out_68 = true;

	private bool logic_uScript_AddMessage_Shown_68 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_69 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_69;

	private bool logic_uScriptCon_CompareBool_True_69 = true;

	private bool logic_uScriptCon_CompareBool_False_69 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_71 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_71;

	private bool logic_uScriptAct_SetBool_Out_71 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_71 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_71 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_73 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_73;

	private float logic_uScript_IsPlayerInRangeOfTech_range_73;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_73 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_73 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_73 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_73 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_75 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_75;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_75;

	private bool logic_uScript_SetTechAIType_Out_75 = true;

	private uScript_IsTechInAIState logic_uScript_IsTechInAIState_uScript_IsTechInAIState_76 = new uScript_IsTechInAIState();

	private Tank logic_uScript_IsTechInAIState_tech_76;

	private AITreeType.AITypes logic_uScript_IsTechInAIState_targetAIType_76 = AITreeType.AITypes.Guard;

	private bool logic_uScript_IsTechInAIState_True_76 = true;

	private bool logic_uScript_IsTechInAIState_False_76 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_78 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_78;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_78;

	private bool logic_uScript_SetTechAIType_Out_78 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_83 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_83;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_83;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_83;

	private bool logic_uScript_AddMessage_Out_83 = true;

	private bool logic_uScript_AddMessage_Shown_83 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_84;

	private bool logic_uScriptCon_CompareBool_True_84 = true;

	private bool logic_uScriptCon_CompareBool_False_84 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_87 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_87;

	private bool logic_uScriptAct_SetBool_Out_87 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_87 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_87 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_89;

	private bool logic_uScriptCon_CompareBool_True_89 = true;

	private bool logic_uScriptCon_CompareBool_False_89 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_93 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_93;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_93;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_93;

	private bool logic_uScript_AddMessage_Out_93 = true;

	private bool logic_uScript_AddMessage_Shown_93 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_94 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_94;

	private bool logic_uScriptAct_SetBool_Out_94 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_94 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_94 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_97 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_97 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_97 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_97 = true;

	private uScript_GetTechRotation logic_uScript_GetTechRotation_uScript_GetTechRotation_99 = new uScript_GetTechRotation();

	private Tank logic_uScript_GetTechRotation_tech_99;

	private Quaternion logic_uScript_GetTechRotation_Return_99;

	private bool logic_uScript_GetTechRotation_Out_99 = true;

	private uScript_SetEncounterRotation logic_uScript_SetEncounterRotation_uScript_SetEncounterRotation_102 = new uScript_SetEncounterRotation();

	private GameObject logic_uScript_SetEncounterRotation_ownerNode_102;

	private Quaternion logic_uScript_SetEncounterRotation_rotation_102;

	private bool logic_uScript_SetEncounterRotation_Out_102 = true;

	private uScript_IsHUDElementExpanded logic_uScript_IsHUDElementExpanded_uScript_IsHUDElementExpanded_104 = new uScript_IsHUDElementExpanded();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementExpanded_hudElement_104 = ManHUD.HUDElementType.BlockPalette;

	private bool logic_uScript_IsHUDElementExpanded_True_104 = true;

	private bool logic_uScript_IsHUDElementExpanded_False_104 = true;

	private uScript_SetVendorsEnabled logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_105 = new uScript_SetVendorsEnabled();

	private bool logic_uScript_SetVendorsEnabled_enableShop_105;

	private bool logic_uScript_SetVendorsEnabled_enableMissionBoard_105;

	private bool logic_uScript_SetVendorsEnabled_enableSelling_105 = true;

	private bool logic_uScript_SetVendorsEnabled_enableSCU_105 = true;

	private bool logic_uScript_SetVendorsEnabled_enableCharging_105;

	private bool logic_uScript_SetVendorsEnabled_Out_105 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_108 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_108;

	private bool logic_uScriptAct_SetBool_Out_108 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_108 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_108 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_109;

	private bool logic_uScriptCon_CompareBool_True_109 = true;

	private bool logic_uScriptCon_CompareBool_False_109 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_111 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_111;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_111;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_111;

	private bool logic_uScript_AddMessage_Out_111 = true;

	private bool logic_uScript_AddMessage_Shown_111 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_114 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_114;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_114;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_114;

	private bool logic_uScript_AddMessage_Out_114 = true;

	private bool logic_uScript_AddMessage_Shown_114 = true;

	private uScript_EnableBlockPalette logic_uScript_EnableBlockPalette_uScript_EnableBlockPalette_115 = new uScript_EnableBlockPalette();

	private bool logic_uScript_EnableBlockPalette_Out_115 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_116;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_116 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_116 = "TechsSpawned";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_118;

	private bool logic_uScriptCon_CompareBool_True_118 = true;

	private bool logic_uScriptCon_CompareBool_False_118 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_120 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_120;

	private bool logic_uScriptAct_SetBool_Out_120 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_120 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_120 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_122 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_122 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_122;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_122;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_122;

	private bool logic_uScript_SpawnTechsFromData_Out_122 = true;

	private uScript_RemoveSceneryAtPosition logic_uScript_RemoveSceneryAtPosition_uScript_RemoveSceneryAtPosition_126 = new uScript_RemoveSceneryAtPosition();

	private Vector3 logic_uScript_RemoveSceneryAtPosition_position_126;

	private float logic_uScript_RemoveSceneryAtPosition_radius_126;

	private bool logic_uScript_RemoveSceneryAtPosition_preventChunksSpawning_126 = true;

	private bool logic_uScript_RemoveSceneryAtPosition_Out_126 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_127 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_127 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_127;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_127;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_127;

	private bool logic_uScript_SpawnTechsFromData_Out_127 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_133 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_133;

	private object logic_uScript_SetEncounterTarget_visibleObject_133 = "";

	private bool logic_uScript_SetEncounterTarget_Out_133 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_134 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_134;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_134 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_134;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_134 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_137;

	private bool logic_uScriptCon_CompareBool_True_137 = true;

	private bool logic_uScriptCon_CompareBool_False_137 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_138 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_138;

	private int logic_uScriptCon_CompareInt_B_138 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_138 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_138 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_138 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_138 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_138 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_138 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_140 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_140;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_140 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_140 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_141 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_141;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_141;

	private bool logic_uScript_LockTech_Out_141 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_142 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_142;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_142 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_142 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_146;

	private bool logic_uScriptCon_CompareBool_True_146 = true;

	private bool logic_uScriptCon_CompareBool_False_146 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_148 = true;

	private uScript_SetEncounterPosition logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_151 = new uScript_SetEncounterPosition();

	private GameObject logic_uScript_SetEncounterPosition_ownerNode_151;

	private Vector3 logic_uScript_SetEncounterPosition_position_151;

	private bool logic_uScript_SetEncounterPosition_Out_151 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_156 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_156;

	private bool logic_uScriptAct_SetBool_Out_156 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_156 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_156 = true;

	private uScript_TechInRangeOfPosition logic_uScript_TechInRangeOfPosition_uScript_TechInRangeOfPosition_157 = new uScript_TechInRangeOfPosition();

	private Tank logic_uScript_TechInRangeOfPosition_tech_157;

	private Vector3 logic_uScript_TechInRangeOfPosition_position_157;

	private float logic_uScript_TechInRangeOfPosition_range_157 = 400f;

	private bool logic_uScript_TechInRangeOfPosition_Out_157 = true;

	private bool logic_uScript_TechInRangeOfPosition_True_157 = true;

	private bool logic_uScript_TechInRangeOfPosition_False_157 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_158 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_158;

	private bool logic_uScript_GetPlayerTank_Returned_158 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_158 = true;

	private uScript_GetCurrentEncounterPosition logic_uScript_GetCurrentEncounterPosition_uScript_GetCurrentEncounterPosition_160 = new uScript_GetCurrentEncounterPosition();

	private GameObject logic_uScript_GetCurrentEncounterPosition_ownerNode_160;

	private Vector3 logic_uScript_GetCurrentEncounterPosition_Return_160;

	private bool logic_uScript_GetCurrentEncounterPosition_Out_160 = true;

	private uScript_GetSecondNearestVendorPos logic_uScript_GetSecondNearestVendorPos_uScript_GetSecondNearestVendorPos_164 = new uScript_GetSecondNearestVendorPos();

	private Vector3 logic_uScript_GetSecondNearestVendorPos_Return_164;

	private bool logic_uScript_GetSecondNearestVendorPos_Out_164 = true;

	private bool logic_uScript_GetSecondNearestVendorPos_Found_164 = true;

	private bool logic_uScript_GetSecondNearestVendorPos_Missing_164 = true;

	private uScript_TechInRangeOfPosition logic_uScript_TechInRangeOfPosition_uScript_TechInRangeOfPosition_167 = new uScript_TechInRangeOfPosition();

	private Tank logic_uScript_TechInRangeOfPosition_tech_167;

	private Vector3 logic_uScript_TechInRangeOfPosition_position_167;

	private float logic_uScript_TechInRangeOfPosition_range_167;

	private bool logic_uScript_TechInRangeOfPosition_Out_167 = true;

	private bool logic_uScript_TechInRangeOfPosition_True_167 = true;

	private bool logic_uScript_TechInRangeOfPosition_False_167 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_168 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_168;

	private bool logic_uScript_GetPlayerTank_Returned_168 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_168 = true;

	private uScript_GetCurrentEncounterPosition logic_uScript_GetCurrentEncounterPosition_uScript_GetCurrentEncounterPosition_170 = new uScript_GetCurrentEncounterPosition();

	private GameObject logic_uScript_GetCurrentEncounterPosition_ownerNode_170;

	private Vector3 logic_uScript_GetCurrentEncounterPosition_Return_170;

	private bool logic_uScript_GetCurrentEncounterPosition_Out_170 = true;

	private uScript_FindNearestVendorToEncounter logic_uScript_FindNearestVendorToEncounter_uScript_FindNearestVendorToEncounter_174 = new uScript_FindNearestVendorToEncounter();

	private GameObject logic_uScript_FindNearestVendorToEncounter_ownerNode_174;

	private Tank logic_uScript_FindNearestVendorToEncounter_Return_174;

	private bool logic_uScript_FindNearestVendorToEncounter_Out_174 = true;

	private bool logic_uScript_FindNearestVendorToEncounter_Returned_174 = true;

	private bool logic_uScript_FindNearestVendorToEncounter_NotReturned_174 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_178 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_178;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_178 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_178 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_181 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_181;

	private object logic_uScript_SetEncounterTarget_visibleObject_181 = "";

	private bool logic_uScript_SetEncounterTarget_Out_181 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_183 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_183;

	private int logic_uScriptCon_CompareInt_B_183 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_183 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_183 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_183 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_183 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_183 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_183 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_185 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_185;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_185 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_185 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_188 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_188;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_188;

	private bool logic_uScript_SetCustomRadarTeamID_Out_188 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_189 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_189 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_190 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_190;

	private int logic_uScript_SetTankTeam_team_190;

	private bool logic_uScript_SetTankTeam_Out_190 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_192 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_192 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_193 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_193;

	private object logic_uScript_SetEncounterTarget_visibleObject_193 = "";

	private bool logic_uScript_SetEncounterTarget_Out_193 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_197 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_197;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_197 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_197 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_199 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_199;

	private bool logic_uScript_ShowHint_Out_199 = true;

	private uScript_HideHintFloating logic_uScript_HideHintFloating_uScript_HideHintFloating_200 = new uScript_HideHintFloating();

	private bool logic_uScript_HideHintFloating_Out_200 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_201 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_201;

	private bool logic_uScript_RemoveOnScreenMessage_instant_201;

	private bool logic_uScript_RemoveOnScreenMessage_Out_201 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_204;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_204 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_204 = "msgDefeatEnemies";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_206;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_206 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_206 = "msgTurretIdle";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_208;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_208 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_208 = "MsgSCUEnabled";

	private uScript_SetVendorsEnabled logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_211 = new uScript_SetVendorsEnabled();

	private bool logic_uScript_SetVendorsEnabled_enableShop_211 = true;

	private bool logic_uScript_SetVendorsEnabled_enableMissionBoard_211 = true;

	private bool logic_uScript_SetVendorsEnabled_enableSelling_211 = true;

	private bool logic_uScript_SetVendorsEnabled_enableSCU_211;

	private bool logic_uScript_SetVendorsEnabled_enableCharging_211;

	private bool logic_uScript_SetVendorsEnabled_Out_211 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_212 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_212;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_212 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_212 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_212 = true;

	private uScript_SetVendorsEnabled logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_214 = new uScript_SetVendorsEnabled();

	private bool logic_uScript_SetVendorsEnabled_enableShop_214;

	private bool logic_uScript_SetVendorsEnabled_enableMissionBoard_214;

	private bool logic_uScript_SetVendorsEnabled_enableSelling_214;

	private bool logic_uScript_SetVendorsEnabled_enableSCU_214;

	private bool logic_uScript_SetVendorsEnabled_enableCharging_214;

	private bool logic_uScript_SetVendorsEnabled_Out_214 = true;

	private uScript_SetVendorsEnabled logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_216 = new uScript_SetVendorsEnabled();

	private bool logic_uScript_SetVendorsEnabled_enableShop_216 = true;

	private bool logic_uScript_SetVendorsEnabled_enableMissionBoard_216 = true;

	private bool logic_uScript_SetVendorsEnabled_enableSelling_216 = true;

	private bool logic_uScript_SetVendorsEnabled_enableSCU_216 = true;

	private bool logic_uScript_SetVendorsEnabled_enableCharging_216 = true;

	private bool logic_uScript_SetVendorsEnabled_Out_216 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_217 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_217;

	private bool logic_uScriptCon_CompareBool_True_217 = true;

	private bool logic_uScriptCon_CompareBool_False_217 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_220 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_220;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_220;

	private bool logic_uScript_SetTechAIType_Out_220 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_221 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_222 = true;

	private uScript_SetVendorsEnabled logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_224 = new uScript_SetVendorsEnabled();

	private bool logic_uScript_SetVendorsEnabled_enableShop_224;

	private bool logic_uScript_SetVendorsEnabled_enableMissionBoard_224;

	private bool logic_uScript_SetVendorsEnabled_enableSelling_224;

	private bool logic_uScript_SetVendorsEnabled_enableSCU_224 = true;

	private bool logic_uScript_SetVendorsEnabled_enableCharging_224;

	private bool logic_uScript_SetVendorsEnabled_Out_224 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_225;

	private bool logic_uScriptCon_CompareBool_True_225 = true;

	private bool logic_uScriptCon_CompareBool_False_225 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_226 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_226;

	private bool logic_uScriptCon_CompareBool_True_226 = true;

	private bool logic_uScriptCon_CompareBool_False_226 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_229 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_229;

	private bool logic_uScriptAct_SetBool_Out_229 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_229 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_229 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_230;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_230 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_230 = "InventoryOpened";

	private uScript_LockSendToSCU logic_uScript_LockSendToSCU_uScript_LockSendToSCU_232 = new uScript_LockSendToSCU();

	private bool logic_uScript_LockSendToSCU_lockSendToSCU_232;

	private bool logic_uScript_LockSendToSCU_Out_232 = true;

	private uScript_LockSendToSCU logic_uScript_LockSendToSCU_uScript_LockSendToSCU_233 = new uScript_LockSendToSCU();

	private bool logic_uScript_LockSendToSCU_lockSendToSCU_233 = true;

	private bool logic_uScript_LockSendToSCU_Out_233 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_234 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_234;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_234 = AITreeType.AITypes.Guard;

	private bool logic_uScript_SetTechAIType_Out_234 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_236 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_236;

	private bool logic_uScript_RemoveOnScreenMessage_instant_236;

	private bool logic_uScript_RemoveOnScreenMessage_Out_236 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_238;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_238;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_238;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_238;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_238;

	private uScript_IsHUDElementVisible logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_239 = new uScript_IsHUDElementVisible();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementVisible_hudElement_239 = ManHUD.HUDElementType.TechControlChoice;

	private bool logic_uScript_IsHUDElementVisible_True_239 = true;

	private bool logic_uScript_IsHUDElementVisible_False_239 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_244;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_244;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_244;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_244;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_244;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_247 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_247 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_247 = true;

	private uScript_IsHUDElementVisible logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_250 = new uScript_IsHUDElementVisible();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementVisible_hudElement_250 = ManHUD.HUDElementType.InteractionMode;

	private bool logic_uScript_IsHUDElementVisible_True_250 = true;

	private bool logic_uScript_IsHUDElementVisible_False_250 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_251 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_251;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_251;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_251;

	private bool logic_uScript_AddMessage_Out_251 = true;

	private bool logic_uScript_AddMessage_Shown_251 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_252 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_252 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_252 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_252 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_252 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_255 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_255 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_256 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_256 = true;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_257 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_257 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_257 = true;

	private uScript_IsPlayerInBeam logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_258 = new uScript_IsPlayerInBeam();

	private bool logic_uScript_IsPlayerInBeam_True_258 = true;

	private bool logic_uScript_IsPlayerInBeam_False_258 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_259 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_259;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_259;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_259;

	private bool logic_uScript_AddMessage_Out_259 = true;

	private bool logic_uScript_AddMessage_Shown_259 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_265;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_265;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_265;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_265;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_265;

	private SubGraph_ShowHintWithPadSupport logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_266 = new SubGraph_ShowHintWithPadSupport();

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintControlPad_266 = UIHintFloating.HintFloatTypes.Buildbeam_Pad;

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_266 = UIHintFloating.HintFloatTypes.Buildbeam_Keyboard;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_267 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_267 = 0.2f;

	private bool logic_uScript_Wait_repeat_267;

	private bool logic_uScript_Wait_Waited_267 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_270 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_270;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_270;

	private bool logic_uScript_SetTankHideBlockLimit_Out_270 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_271 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_271 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_271;

	private bool logic_uScript_SetTankHideBlockLimit_Out_271 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_1.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_1.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_0;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_0;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_0;
				}
			}
		}
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
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
		if (null == owner_Connection_39 || !m_RegisteredForEvents)
		{
			owner_Connection_39 = parentGameObject;
		}
		if (null == owner_Connection_103 || !m_RegisteredForEvents)
		{
			owner_Connection_103 = parentGameObject;
		}
		if (null == owner_Connection_124 || !m_RegisteredForEvents)
		{
			owner_Connection_124 = parentGameObject;
		}
		if (null == owner_Connection_129 || !m_RegisteredForEvents)
		{
			owner_Connection_129 = parentGameObject;
		}
		if (null == owner_Connection_132 || !m_RegisteredForEvents)
		{
			owner_Connection_132 = parentGameObject;
		}
		if (null == owner_Connection_136 || !m_RegisteredForEvents)
		{
			owner_Connection_136 = parentGameObject;
		}
		if (null == owner_Connection_154 || !m_RegisteredForEvents)
		{
			owner_Connection_154 = parentGameObject;
		}
		if (null == owner_Connection_161 || !m_RegisteredForEvents)
		{
			owner_Connection_161 = parentGameObject;
		}
		if (null == owner_Connection_171 || !m_RegisteredForEvents)
		{
			owner_Connection_171 = parentGameObject;
		}
		if (null == owner_Connection_175 || !m_RegisteredForEvents)
		{
			owner_Connection_175 = parentGameObject;
		}
		if (null == owner_Connection_180 || !m_RegisteredForEvents)
		{
			owner_Connection_180 = parentGameObject;
		}
		if (null == owner_Connection_194 || !m_RegisteredForEvents)
		{
			owner_Connection_194 = parentGameObject;
		}
		if (null == owner_Connection_213 || !m_RegisteredForEvents)
		{
			owner_Connection_213 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_1.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_1.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_0;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_0;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_0;
			}
		}
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
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_1)
		{
			uScript_SaveLoad component = owner_Connection_1.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_0;
				component.LoadEvent -= Instance_LoadEvent_0;
				component.RestartEvent -= Instance_RestartEvent_0;
			}
		}
		if (null != owner_Connection_2)
		{
			uScript_EncounterUpdate component2 = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_3;
				component2.OnSuspend -= Instance_OnSuspend_3;
				component2.OnResume -= Instance_OnResume_3;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_8.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_18.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_19.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_24.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_38.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_42.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_43.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_44.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_51.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_53.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_55.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_59.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_63.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_64.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_68.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_69.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_73.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_75.SetParent(g);
		logic_uScript_IsTechInAIState_uScript_IsTechInAIState_76.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_78.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_83.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_87.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_93.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_97.SetParent(g);
		logic_uScript_GetTechRotation_uScript_GetTechRotation_99.SetParent(g);
		logic_uScript_SetEncounterRotation_uScript_SetEncounterRotation_102.SetParent(g);
		logic_uScript_IsHUDElementExpanded_uScript_IsHUDElementExpanded_104.SetParent(g);
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_105.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_111.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_114.SetParent(g);
		logic_uScript_EnableBlockPalette_uScript_EnableBlockPalette_115.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_120.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_122.SetParent(g);
		logic_uScript_RemoveSceneryAtPosition_uScript_RemoveSceneryAtPosition_126.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_127.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_133.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_134.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_138.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_140.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_141.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_142.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148.SetParent(g);
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_151.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_156.SetParent(g);
		logic_uScript_TechInRangeOfPosition_uScript_TechInRangeOfPosition_157.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_158.SetParent(g);
		logic_uScript_GetCurrentEncounterPosition_uScript_GetCurrentEncounterPosition_160.SetParent(g);
		logic_uScript_GetSecondNearestVendorPos_uScript_GetSecondNearestVendorPos_164.SetParent(g);
		logic_uScript_TechInRangeOfPosition_uScript_TechInRangeOfPosition_167.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_168.SetParent(g);
		logic_uScript_GetCurrentEncounterPosition_uScript_GetCurrentEncounterPosition_170.SetParent(g);
		logic_uScript_FindNearestVendorToEncounter_uScript_FindNearestVendorToEncounter_174.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_178.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_181.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_183.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_185.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_188.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_189.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_190.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_192.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_193.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_197.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_199.SetParent(g);
		logic_uScript_HideHintFloating_uScript_HideHintFloating_200.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_201.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.SetParent(g);
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_211.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_212.SetParent(g);
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_214.SetParent(g);
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_216.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_217.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_220.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222.SetParent(g);
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_224.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_226.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_229.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.SetParent(g);
		logic_uScript_LockSendToSCU_uScript_LockSendToSCU_232.SetParent(g);
		logic_uScript_LockSendToSCU_uScript_LockSendToSCU_233.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_234.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_236.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238.SetParent(g);
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_239.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_247.SetParent(g);
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_250.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_251.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_252.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_255.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_256.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_257.SetParent(g);
		logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_258.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_259.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265.SetParent(g);
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_266.SetParent(g);
		logic_uScript_Wait_uScript_Wait_267.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_270.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_271.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_2 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_35 = parentGameObject;
		owner_Connection_39 = parentGameObject;
		owner_Connection_103 = parentGameObject;
		owner_Connection_124 = parentGameObject;
		owner_Connection_129 = parentGameObject;
		owner_Connection_132 = parentGameObject;
		owner_Connection_136 = parentGameObject;
		owner_Connection_154 = parentGameObject;
		owner_Connection_161 = parentGameObject;
		owner_Connection_171 = parentGameObject;
		owner_Connection_175 = parentGameObject;
		owner_Connection_180 = parentGameObject;
		owner_Connection_194 = parentGameObject;
		owner_Connection_213 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_8.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_19.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_64.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265.Awake();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_266.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Save_Out += SubGraph_SaveLoadBool_Save_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Load_Out += SubGraph_SaveLoadBool_Load_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_5;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Save_Out += SubGraph_SaveLoadInt_Save_Out_6;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Load_Out += SubGraph_SaveLoadInt_Load_Out_6;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_6;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_8.Out += SubGraph_LoadObjectiveStates_Out_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output1 += uScriptCon_ManualSwitch_Output1_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output2 += uScriptCon_ManualSwitch_Output2_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output3 += uScriptCon_ManualSwitch_Output3_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output4 += uScriptCon_ManualSwitch_Output4_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output5 += uScriptCon_ManualSwitch_Output5_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output6 += uScriptCon_ManualSwitch_Output6_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output7 += uScriptCon_ManualSwitch_Output7_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output8 += uScriptCon_ManualSwitch_Output8_10;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_19.Out += SubGraph_CompleteObjectiveStage_Out_19;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Save_Out += SubGraph_SaveLoadBool_Save_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Load_Out += SubGraph_SaveLoadBool_Load_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Save_Out += SubGraph_SaveLoadBool_Save_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Load_Out += SubGraph_SaveLoadBool_Load_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_27;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.Out += SubGraph_CompleteObjectiveStage_Out_31;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_64.Out += SubGraph_CompleteObjectiveStage_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Save_Out += SubGraph_SaveLoadBool_Save_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Load_Out += SubGraph_SaveLoadBool_Load_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Save_Out += SubGraph_SaveLoadBool_Save_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Load_Out += SubGraph_SaveLoadBool_Load_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Save_Out += SubGraph_SaveLoadBool_Save_Out_206;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Load_Out += SubGraph_SaveLoadBool_Load_Out_206;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_206;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Save_Out += SubGraph_SaveLoadBool_Save_Out_208;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Load_Out += SubGraph_SaveLoadBool_Load_Out_208;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_208;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Save_Out += SubGraph_SaveLoadBool_Save_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Load_Out += SubGraph_SaveLoadBool_Load_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_230;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238.Out += SubGraph_AddMessageWithPadSupport_Out_238;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238.Shown += SubGraph_AddMessageWithPadSupport_Shown_238;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244.Out += SubGraph_AddMessageWithPadSupport_Out_244;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244.Shown += SubGraph_AddMessageWithPadSupport_Shown_244;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265.Out += SubGraph_AddMessageWithPadSupport_Out_265;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265.Shown += SubGraph_AddMessageWithPadSupport_Shown_265;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_266.Out += SubGraph_ShowHintWithPadSupport_Out_266;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_8.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_19.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_64.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265.Start();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_266.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_8.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_19.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_64.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.OnEnable();
		logic_uScript_GetSecondNearestVendorPos_uScript_GetSecondNearestVendorPos_164.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_197.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265.OnEnable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_266.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_8.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_19.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_24.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_44.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_59.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_63.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_64.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_68.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_73.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_83.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_93.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_111.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_114.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_134.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_212.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.OnDisable();
		logic_uScript_LockSendToSCU_uScript_LockSendToSCU_232.OnDisable();
		logic_uScript_LockSendToSCU_uScript_LockSendToSCU_233.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_247.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_251.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_257.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_259.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265.OnDisable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_266.OnDisable();
		logic_uScript_Wait_uScript_Wait_267.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_8.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_19.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_64.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265.Update();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_266.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_8.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_19.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_64.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265.OnDestroy();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_266.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Save_Out -= SubGraph_SaveLoadBool_Save_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Load_Out -= SubGraph_SaveLoadBool_Load_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_5;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Save_Out -= SubGraph_SaveLoadInt_Save_Out_6;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Load_Out -= SubGraph_SaveLoadInt_Load_Out_6;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_6;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_8.Out -= SubGraph_LoadObjectiveStates_Out_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output1 -= uScriptCon_ManualSwitch_Output1_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output2 -= uScriptCon_ManualSwitch_Output2_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output3 -= uScriptCon_ManualSwitch_Output3_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output4 -= uScriptCon_ManualSwitch_Output4_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output5 -= uScriptCon_ManualSwitch_Output5_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output6 -= uScriptCon_ManualSwitch_Output6_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output7 -= uScriptCon_ManualSwitch_Output7_10;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.Output8 -= uScriptCon_ManualSwitch_Output8_10;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_19.Out -= SubGraph_CompleteObjectiveStage_Out_19;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Save_Out -= SubGraph_SaveLoadBool_Save_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Load_Out -= SubGraph_SaveLoadBool_Load_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Save_Out -= SubGraph_SaveLoadBool_Save_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Load_Out -= SubGraph_SaveLoadBool_Load_Out_27;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_27;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.Out -= SubGraph_CompleteObjectiveStage_Out_31;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_64.Out -= SubGraph_CompleteObjectiveStage_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Save_Out -= SubGraph_SaveLoadBool_Save_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Load_Out -= SubGraph_SaveLoadBool_Load_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Save_Out -= SubGraph_SaveLoadBool_Save_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Load_Out -= SubGraph_SaveLoadBool_Load_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Save_Out -= SubGraph_SaveLoadBool_Save_Out_206;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Load_Out -= SubGraph_SaveLoadBool_Load_Out_206;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_206;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Save_Out -= SubGraph_SaveLoadBool_Save_Out_208;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Load_Out -= SubGraph_SaveLoadBool_Load_Out_208;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_208;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Save_Out -= SubGraph_SaveLoadBool_Save_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Load_Out -= SubGraph_SaveLoadBool_Load_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_230;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238.Out -= SubGraph_AddMessageWithPadSupport_Out_238;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238.Shown -= SubGraph_AddMessageWithPadSupport_Shown_238;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244.Out -= SubGraph_AddMessageWithPadSupport_Out_244;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244.Shown -= SubGraph_AddMessageWithPadSupport_Shown_244;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265.Out -= SubGraph_AddMessageWithPadSupport_Out_265;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265.Shown -= SubGraph_AddMessageWithPadSupport_Shown_265;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_266.Out -= SubGraph_ShowHintWithPadSupport_Out_266;
	}

	private void Instance_SaveEvent_0(object o, EventArgs e)
	{
		Relay_SaveEvent_0();
	}

	private void Instance_LoadEvent_0(object o, EventArgs e)
	{
		Relay_LoadEvent_0();
	}

	private void Instance_RestartEvent_0(object o, EventArgs e)
	{
		Relay_RestartEvent_0();
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

	private void SubGraph_SaveLoadBool_Save_Out_5(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_5;
		Relay_Save_Out_5();
	}

	private void SubGraph_SaveLoadBool_Load_Out_5(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_5;
		Relay_Load_Out_5();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_5(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_5;
		Relay_Restart_Out_5();
	}

	private void SubGraph_SaveLoadInt_Save_Out_6(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_6 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_6;
		Relay_Save_Out_6();
	}

	private void SubGraph_SaveLoadInt_Load_Out_6(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_6 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_6;
		Relay_Load_Out_6();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_6(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_6 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_6;
		Relay_Restart_Out_6();
	}

	private void SubGraph_LoadObjectiveStates_Out_8(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_8();
	}

	private void uScriptCon_ManualSwitch_Output1_10(object o, EventArgs e)
	{
		Relay_Output1_10();
	}

	private void uScriptCon_ManualSwitch_Output2_10(object o, EventArgs e)
	{
		Relay_Output2_10();
	}

	private void uScriptCon_ManualSwitch_Output3_10(object o, EventArgs e)
	{
		Relay_Output3_10();
	}

	private void uScriptCon_ManualSwitch_Output4_10(object o, EventArgs e)
	{
		Relay_Output4_10();
	}

	private void uScriptCon_ManualSwitch_Output5_10(object o, EventArgs e)
	{
		Relay_Output5_10();
	}

	private void uScriptCon_ManualSwitch_Output6_10(object o, EventArgs e)
	{
		Relay_Output6_10();
	}

	private void uScriptCon_ManualSwitch_Output7_10(object o, EventArgs e)
	{
		Relay_Output7_10();
	}

	private void uScriptCon_ManualSwitch_Output8_10(object o, EventArgs e)
	{
		Relay_Output8_10();
	}

	private void SubGraph_CompleteObjectiveStage_Out_19(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_19 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_19;
		Relay_Out_19();
	}

	private void SubGraph_SaveLoadBool_Save_Out_22(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = e.boolean;
		local_Stage3Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_22;
		Relay_Save_Out_22();
	}

	private void SubGraph_SaveLoadBool_Load_Out_22(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = e.boolean;
		local_Stage3Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_22;
		Relay_Load_Out_22();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_22(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = e.boolean;
		local_Stage3Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_22;
		Relay_Restart_Out_22();
	}

	private void SubGraph_SaveLoadBool_Save_Out_27(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = e.boolean;
		local_Stage4Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_27;
		Relay_Save_Out_27();
	}

	private void SubGraph_SaveLoadBool_Load_Out_27(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = e.boolean;
		local_Stage4Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_27;
		Relay_Load_Out_27();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_27(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = e.boolean;
		local_Stage4Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_27;
		Relay_Restart_Out_27();
	}

	private void SubGraph_CompleteObjectiveStage_Out_31(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_31 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_31;
		Relay_Out_31();
	}

	private void SubGraph_CompleteObjectiveStage_Out_64(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_64 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_64;
		Relay_Out_64();
	}

	private void SubGraph_SaveLoadBool_Save_Out_116(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_116;
		Relay_Save_Out_116();
	}

	private void SubGraph_SaveLoadBool_Load_Out_116(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_116;
		Relay_Load_Out_116();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_116(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_116;
		Relay_Restart_Out_116();
	}

	private void SubGraph_SaveLoadBool_Save_Out_204(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = e.boolean;
		local_msgDefeatEnemies_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_204;
		Relay_Save_Out_204();
	}

	private void SubGraph_SaveLoadBool_Load_Out_204(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = e.boolean;
		local_msgDefeatEnemies_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_204;
		Relay_Load_Out_204();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_204(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = e.boolean;
		local_msgDefeatEnemies_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_204;
		Relay_Restart_Out_204();
	}

	private void SubGraph_SaveLoadBool_Save_Out_206(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = e.boolean;
		local_msgTurretIdle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_206;
		Relay_Save_Out_206();
	}

	private void SubGraph_SaveLoadBool_Load_Out_206(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = e.boolean;
		local_msgTurretIdle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_206;
		Relay_Load_Out_206();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_206(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = e.boolean;
		local_msgTurretIdle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_206;
		Relay_Restart_Out_206();
	}

	private void SubGraph_SaveLoadBool_Save_Out_208(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = e.boolean;
		local_MsgSCUEnabled_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_208;
		Relay_Save_Out_208();
	}

	private void SubGraph_SaveLoadBool_Load_Out_208(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = e.boolean;
		local_MsgSCUEnabled_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_208;
		Relay_Load_Out_208();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_208(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = e.boolean;
		local_MsgSCUEnabled_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_208;
		Relay_Restart_Out_208();
	}

	private void SubGraph_SaveLoadBool_Save_Out_230(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = e.boolean;
		local_InventoryOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_230;
		Relay_Save_Out_230();
	}

	private void SubGraph_SaveLoadBool_Load_Out_230(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = e.boolean;
		local_InventoryOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_230;
		Relay_Load_Out_230();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_230(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = e.boolean;
		local_InventoryOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_230;
		Relay_Restart_Out_230();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_238(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_238 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_238 = e.messageControlPadReturn;
		Relay_Out_238();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_238(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_238 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_238 = e.messageControlPadReturn;
		Relay_Shown_238();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_244(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_244 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_244 = e.messageControlPadReturn;
		Relay_Out_244();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_244(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_244 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_244 = e.messageControlPadReturn;
		Relay_Shown_244();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_265(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_265 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_265 = e.messageControlPadReturn;
		local_Msg07OpenInventory_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_265;
		local_Msg07OpenInventory_Pad2_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_265;
		Relay_Out_265();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_265(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_265 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_265 = e.messageControlPadReturn;
		local_Msg07OpenInventory_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_265;
		local_Msg07OpenInventory_Pad2_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_265;
		Relay_Shown_265();
	}

	private void SubGraph_ShowHintWithPadSupport_Out_266(object o, SubGraph_ShowHintWithPadSupport.LogicEventArgs e)
	{
		Relay_Out_266();
	}

	private void Relay_SaveEvent_0()
	{
		Relay_Save_5();
	}

	private void Relay_LoadEvent_0()
	{
		Relay_Load_5();
	}

	private void Relay_RestartEvent_0()
	{
		Relay_Set_False_5();
	}

	private void Relay_OnUpdate_3()
	{
		Relay_In_29();
	}

	private void Relay_OnSuspend_3()
	{
	}

	private void Relay_OnResume_3()
	{
	}

	private void Relay_Save_Out_5()
	{
		Relay_Save_116();
	}

	private void Relay_Load_Out_5()
	{
		Relay_Load_116();
	}

	private void Relay_Restart_Out_5()
	{
		Relay_Set_False_116();
	}

	private void Relay_Save_5()
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Save(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
	}

	private void Relay_Load_5()
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Load(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
	}

	private void Relay_Set_True_5()
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
	}

	private void Relay_Set_False_5()
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
	}

	private void Relay_Save_Out_6()
	{
	}

	private void Relay_Load_Out_6()
	{
		Relay_In_8();
	}

	private void Relay_Restart_Out_6()
	{
	}

	private void Relay_Save_6()
	{
		logic_SubGraph_SaveLoadInt_integer_6 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_6 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Save(logic_SubGraph_SaveLoadInt_restartValue_6, ref logic_SubGraph_SaveLoadInt_integer_6, logic_SubGraph_SaveLoadInt_intAsVariable_6, logic_SubGraph_SaveLoadInt_uniqueID_6);
	}

	private void Relay_Load_6()
	{
		logic_SubGraph_SaveLoadInt_integer_6 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_6 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Load(logic_SubGraph_SaveLoadInt_restartValue_6, ref logic_SubGraph_SaveLoadInt_integer_6, logic_SubGraph_SaveLoadInt_intAsVariable_6, logic_SubGraph_SaveLoadInt_uniqueID_6);
	}

	private void Relay_Restart_6()
	{
		logic_SubGraph_SaveLoadInt_integer_6 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_6 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_6.Restart(logic_SubGraph_SaveLoadInt_restartValue_6, ref logic_SubGraph_SaveLoadInt_integer_6, logic_SubGraph_SaveLoadInt_intAsVariable_6, logic_SubGraph_SaveLoadInt_uniqueID_6);
	}

	private void Relay_Out_8()
	{
	}

	private void Relay_In_8()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_8 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_8.In(logic_SubGraph_LoadObjectiveStates_currentObjective_8);
	}

	private void Relay_Output1_10()
	{
		Relay_In_44();
	}

	private void Relay_Output2_10()
	{
		Relay_In_63();
	}

	private void Relay_Output3_10()
	{
		Relay_In_181();
	}

	private void Relay_Output4_10()
	{
		Relay_In_193();
	}

	private void Relay_Output5_10()
	{
	}

	private void Relay_Output6_10()
	{
	}

	private void Relay_Output7_10()
	{
	}

	private void Relay_Output8_10()
	{
	}

	private void Relay_In_10()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_10 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_10.In(logic_uScriptCon_ManualSwitch_CurrentOutput_10);
	}

	private void Relay_Succeed_18()
	{
		logic_uScript_FinishEncounter_owner_18 = owner_Connection_17;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_18.Succeed(logic_uScript_FinishEncounter_owner_18);
	}

	private void Relay_Fail_18()
	{
		logic_uScript_FinishEncounter_owner_18 = owner_Connection_17;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_18.Fail(logic_uScript_FinishEncounter_owner_18);
	}

	private void Relay_Out_19()
	{
	}

	private void Relay_In_19()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_19 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_19.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_19, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_19);
	}

	private void Relay_Save_Out_22()
	{
		Relay_Save_27();
	}

	private void Relay_Load_Out_22()
	{
		Relay_Load_27();
	}

	private void Relay_Restart_Out_22()
	{
		Relay_Set_False_27();
	}

	private void Relay_Save_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_Stage3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_Stage3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Save(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Load_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_Stage3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_Stage3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Load(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Set_True_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_Stage3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_Stage3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Set_False_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_Stage3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_Stage3Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_In_24()
	{
		logic_uScript_AddMessage_messageData_24 = msg01aDefeatEnemies;
		logic_uScript_AddMessage_speaker_24 = messageSpeakerTurret;
		logic_uScript_AddMessage_Return_24 = logic_uScript_AddMessage_uScript_AddMessage_24.In(logic_uScript_AddMessage_messageData_24, logic_uScript_AddMessage_speaker_24);
		if (logic_uScript_AddMessage_uScript_AddMessage_24.Shown)
		{
			Relay_True_94();
		}
	}

	private void Relay_Save_Out_27()
	{
		Relay_Save_204();
	}

	private void Relay_Load_Out_27()
	{
		Relay_Load_204();
	}

	private void Relay_Restart_Out_27()
	{
		Relay_Set_False_204();
	}

	private void Relay_Save_27()
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = local_Stage4Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_27 = local_Stage4Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Save(ref logic_SubGraph_SaveLoadBool_boolean_27, logic_SubGraph_SaveLoadBool_boolAsVariable_27, logic_SubGraph_SaveLoadBool_uniqueID_27);
	}

	private void Relay_Load_27()
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = local_Stage4Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_27 = local_Stage4Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Load(ref logic_SubGraph_SaveLoadBool_boolean_27, logic_SubGraph_SaveLoadBool_boolAsVariable_27, logic_SubGraph_SaveLoadBool_uniqueID_27);
	}

	private void Relay_Set_True_27()
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = local_Stage4Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_27 = local_Stage4Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_27, logic_SubGraph_SaveLoadBool_boolAsVariable_27, logic_SubGraph_SaveLoadBool_uniqueID_27);
	}

	private void Relay_Set_False_27()
	{
		logic_SubGraph_SaveLoadBool_boolean_27 = local_Stage4Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_27 = local_Stage4Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_27.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_27, logic_SubGraph_SaveLoadBool_boolAsVariable_27, logic_SubGraph_SaveLoadBool_uniqueID_27);
	}

	private void Relay_In_29()
	{
		logic_uScriptCon_CompareBool_Bool_29 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.In(logic_uScriptCon_CompareBool_Bool_29);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.False;
		if (num)
		{
			Relay_In_174();
		}
		if (flag)
		{
			Relay_In_164();
		}
	}

	private void Relay_Out_31()
	{
	}

	private void Relay_In_31()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_31 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_31.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_31, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_31);
	}

	private void Relay_In_33()
	{
		int num = 0;
		Array array = alliedTechData;
		if (logic_uScript_GetAndCheckTechs_techData_33.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_33, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_33, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_33 = owner_Connection_35;
		int num2 = 0;
		Array array2 = local_AlliedTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_33.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_33, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_33, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_33 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.In(logic_uScript_GetAndCheckTechs_techData_33, logic_uScript_GetAndCheckTechs_ownerNode_33, ref logic_uScript_GetAndCheckTechs_techs_33);
		local_AlliedTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_33;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_38();
		}
		if (someAlive)
		{
			Relay_AtIndex_38();
		}
		if (allDead)
		{
			Relay_In_43();
		}
	}

	private void Relay_AtIndex_38()
	{
		int num = 0;
		Array array = local_AlliedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_38.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_38, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_38, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_38.AtIndex(ref logic_uScript_AccessListTech_techList_38, logic_uScript_AccessListTech_index_38, out logic_uScript_AccessListTech_value_38);
		local_AlliedTechs_TankArray = logic_uScript_AccessListTech_techList_38;
		local_AlliedTech_Tank = logic_uScript_AccessListTech_value_38;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_38.Out)
		{
			Relay_In_233();
		}
	}

	private void Relay_In_42()
	{
		int num = 0;
		Array array = enemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_42.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_42, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_42, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_42 = owner_Connection_39;
		int num2 = 0;
		Array array2 = local_EnemyTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_42.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_42, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_42, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_42 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_42.In(logic_uScript_GetAndCheckTechs_techData_42, logic_uScript_GetAndCheckTechs_ownerNode_42, ref logic_uScript_GetAndCheckTechs_techs_42);
		local_EnemyTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_42;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_42.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_42.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_42.AllDead;
		if (allAlive)
		{
			Relay_In_48();
		}
		if (someAlive)
		{
			Relay_In_48();
		}
		if (allDead)
		{
			Relay_True_47();
		}
	}

	private void Relay_In_43()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_43.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_43.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_44()
	{
		logic_uScript_SetTankInvulnerable_tank_44 = local_AlliedTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_44.In(logic_uScript_SetTankInvulnerable_invulnerable_44, logic_uScript_SetTankInvulnerable_tank_44);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_44.Out)
		{
			Relay_SetInvulnerable_53();
		}
	}

	private void Relay_True_47()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.True(out logic_uScriptAct_SetBool_Target_47);
		local_EnemiesDead_System_Boolean = logic_uScriptAct_SetBool_Target_47;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_47.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_False_47()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.False(out logic_uScriptAct_SetBool_Target_47);
		local_EnemiesDead_System_Boolean = logic_uScriptAct_SetBool_Target_47;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_47.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_In_48()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_49()
	{
		logic_uScriptCon_CompareBool_Bool_49 = local_EnemiesDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49.In(logic_uScriptCon_CompareBool_Bool_49);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49.True)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_51()
	{
		logic_uScript_SetBatteryChargeAmount_tech_51 = local_AlliedTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_51.In(logic_uScript_SetBatteryChargeAmount_tech_51, logic_uScript_SetBatteryChargeAmount_chargeAmount_51);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_51.Out)
		{
			Relay_In_134();
		}
	}

	private void Relay_SetInvulnerable_53()
	{
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_53.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_53, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_53, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_53.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_53);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_53.Out)
		{
			Relay_In_170();
		}
	}

	private void Relay_SetVulnerable_53()
	{
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_53.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_53, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_53, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_53.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_53);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_53.Out)
		{
			Relay_In_170();
		}
	}

	private void Relay_SetInvulnerable_55()
	{
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_55.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_55, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_55, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_55.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_55);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_55.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_SetVulnerable_55()
	{
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_55.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_55, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_55, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_55.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_55);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_55.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_59()
	{
		logic_uScript_AddMessage_messageData_59 = msg02EnemiesDefeated;
		logic_uScript_AddMessage_speaker_59 = messageSpeakerTurret;
		logic_uScript_AddMessage_Return_59 = logic_uScript_AddMessage_uScript_AddMessage_59.In(logic_uScript_AddMessage_messageData_59, logic_uScript_AddMessage_speaker_59);
		if (logic_uScript_AddMessage_uScript_AddMessage_59.Shown)
		{
			Relay_In_75();
		}
	}

	private void Relay_True_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.True(out logic_uScriptAct_SetBool_Target_60);
		local_Stage3Init_System_Boolean = logic_uScriptAct_SetBool_Target_60;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_60.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_False_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.False(out logic_uScriptAct_SetBool_Target_60);
		local_Stage3Init_System_Boolean = logic_uScriptAct_SetBool_Target_60;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_60.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_In_63()
	{
		logic_uScript_SetTankInvulnerable_tank_63 = local_AlliedTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_63.In(logic_uScript_SetTankInvulnerable_invulnerable_63, logic_uScript_SetTankInvulnerable_tank_63);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_63.Out)
		{
			Relay_In_51();
		}
	}

	private void Relay_Out_64()
	{
	}

	private void Relay_In_64()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_64 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_64.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_64, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_64);
	}

	private void Relay_In_68()
	{
		logic_uScript_AddMessage_messageData_68 = msg05AllyOrdered;
		logic_uScript_AddMessage_speaker_68 = messageSpeakerTurret;
		logic_uScript_AddMessage_Return_68 = logic_uScript_AddMessage_uScript_AddMessage_68.In(logic_uScript_AddMessage_messageData_68, logic_uScript_AddMessage_speaker_68);
		if (logic_uScript_AddMessage_uScript_AddMessage_68.Shown)
		{
			Relay_True_71();
		}
	}

	private void Relay_In_69()
	{
		logic_uScriptCon_CompareBool_Bool_69 = local_Stage4Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_69.In(logic_uScriptCon_CompareBool_Bool_69);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_69.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_69.False;
		if (num)
		{
			Relay_In_105();
		}
		if (flag)
		{
			Relay_In_234();
		}
	}

	private void Relay_True_71()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.True(out logic_uScriptAct_SetBool_Target_71);
		local_Stage4Init_System_Boolean = logic_uScriptAct_SetBool_Target_71;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_71.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_False_71()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.False(out logic_uScriptAct_SetBool_Target_71);
		local_Stage4Init_System_Boolean = logic_uScriptAct_SetBool_Target_71;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_71.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_73 = local_AlliedTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_73 = tradingStationFoundDist;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_73.In(logic_uScript_IsPlayerInRangeOfTech_tech_73, logic_uScript_IsPlayerInRangeOfTech_range_73, logic_uScript_IsPlayerInRangeOfTech_techs_73);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_73.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_73.OutOfRange;
		if (inRange)
		{
			Relay_In_239();
		}
		if (outOfRange)
		{
			Relay_In_97();
		}
	}

	private void Relay_In_75()
	{
		logic_uScript_SetTechAIType_tech_75 = local_AlliedTech_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_75.In(logic_uScript_SetTechAIType_tech_75, logic_uScript_SetTechAIType_aiType_75);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_75.Out)
		{
			Relay_True_60();
		}
	}

	private void Relay_In_76()
	{
		logic_uScript_IsTechInAIState_tech_76 = local_AlliedTech_Tank;
		logic_uScript_IsTechInAIState_uScript_IsTechInAIState_76.In(logic_uScript_IsTechInAIState_tech_76, logic_uScript_IsTechInAIState_targetAIType_76);
		bool num = logic_uScript_IsTechInAIState_uScript_IsTechInAIState_76.True;
		bool flag = logic_uScript_IsTechInAIState_uScript_IsTechInAIState_76.False;
		if (num)
		{
			Relay_In_64();
		}
		if (flag)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_78()
	{
		logic_uScript_SetTechAIType_tech_78 = local_AlliedTech_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_78.In(logic_uScript_SetTechAIType_tech_78, logic_uScript_SetTechAIType_aiType_78);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_78.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_83()
	{
		logic_uScript_AddMessage_messageData_83 = msg03TurretIdle;
		logic_uScript_AddMessage_speaker_83 = messageSpeakerDefault;
		logic_uScript_AddMessage_Return_83 = logic_uScript_AddMessage_uScript_AddMessage_83.In(logic_uScript_AddMessage_messageData_83, logic_uScript_AddMessage_speaker_83);
		if (logic_uScript_AddMessage_uScript_AddMessage_83.Shown)
		{
			Relay_True_87();
		}
	}

	private void Relay_In_84()
	{
		logic_uScriptCon_CompareBool_Bool_84 = local_msgTurretIdle_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.In(logic_uScriptCon_CompareBool_Bool_84);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.False;
		if (num)
		{
			Relay_In_73();
		}
		if (flag)
		{
			Relay_In_83();
		}
	}

	private void Relay_True_87()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_87.True(out logic_uScriptAct_SetBool_Target_87);
		local_msgTurretIdle_System_Boolean = logic_uScriptAct_SetBool_Target_87;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_87.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_False_87()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_87.False(out logic_uScriptAct_SetBool_Target_87);
		local_msgTurretIdle_System_Boolean = logic_uScriptAct_SetBool_Target_87;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_87.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_89()
	{
		logic_uScriptCon_CompareBool_Bool_89 = local_msgDefeatEnemies_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89.In(logic_uScriptCon_CompareBool_Bool_89);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89.False;
		if (num)
		{
			Relay_In_93();
		}
		if (flag)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_93()
	{
		logic_uScript_AddMessage_messageData_93 = msg01bDefeatEnemies;
		logic_uScript_AddMessage_speaker_93 = messageSpeakerDefault;
		logic_uScript_AddMessage_Return_93 = logic_uScript_AddMessage_uScript_AddMessage_93.In(logic_uScript_AddMessage_messageData_93, logic_uScript_AddMessage_speaker_93);
		if (logic_uScript_AddMessage_uScript_AddMessage_93.Out)
		{
			Relay_SetVulnerable_55();
		}
	}

	private void Relay_True_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.True(out logic_uScriptAct_SetBool_Target_94);
		local_msgDefeatEnemies_System_Boolean = logic_uScriptAct_SetBool_Target_94;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_94.Out)
		{
			Relay_In_93();
		}
	}

	private void Relay_False_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.False(out logic_uScriptAct_SetBool_Target_94);
		local_msgDefeatEnemies_System_Boolean = logic_uScriptAct_SetBool_Target_94;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_94.Out)
		{
			Relay_In_93();
		}
	}

	private void Relay_In_97()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_97 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_97.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_97, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_97);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_97.Out)
		{
			Relay_In_178();
		}
	}

	private void Relay_In_99()
	{
		logic_uScript_GetTechRotation_tech_99 = local_GSOVendor_Tank;
		logic_uScript_GetTechRotation_Return_99 = logic_uScript_GetTechRotation_uScript_GetTechRotation_99.In(logic_uScript_GetTechRotation_tech_99);
		local_101_UnityEngine_Quaternion = logic_uScript_GetTechRotation_Return_99;
		if (logic_uScript_GetTechRotation_uScript_GetTechRotation_99.Out)
		{
			Relay_In_102();
		}
	}

	private void Relay_In_102()
	{
		logic_uScript_SetEncounterRotation_ownerNode_102 = owner_Connection_103;
		logic_uScript_SetEncounterRotation_rotation_102 = local_101_UnityEngine_Quaternion;
		logic_uScript_SetEncounterRotation_uScript_SetEncounterRotation_102.In(logic_uScript_SetEncounterRotation_ownerNode_102, logic_uScript_SetEncounterRotation_rotation_102);
		if (logic_uScript_SetEncounterRotation_uScript_SetEncounterRotation_102.Out)
		{
			Relay_In_133();
		}
	}

	private void Relay_In_104()
	{
		logic_uScript_IsHUDElementExpanded_uScript_IsHUDElementExpanded_104.In(logic_uScript_IsHUDElementExpanded_hudElement_104);
		bool num = logic_uScript_IsHUDElementExpanded_uScript_IsHUDElementExpanded_104.True;
		bool flag = logic_uScript_IsHUDElementExpanded_uScript_IsHUDElementExpanded_104.False;
		if (num)
		{
			Relay_In_201();
		}
		if (flag)
		{
			Relay_In_257();
		}
	}

	private void Relay_In_105()
	{
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_105.In(logic_uScript_SetVendorsEnabled_enableShop_105, logic_uScript_SetVendorsEnabled_enableMissionBoard_105, logic_uScript_SetVendorsEnabled_enableSelling_105, logic_uScript_SetVendorsEnabled_enableSCU_105, logic_uScript_SetVendorsEnabled_enableCharging_105);
		if (logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_105.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_True_108()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.True(out logic_uScriptAct_SetBool_Target_108);
		local_MsgSCUEnabled_System_Boolean = logic_uScriptAct_SetBool_Target_108;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_108.Out)
		{
			Relay_In_226();
		}
	}

	private void Relay_False_108()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.False(out logic_uScriptAct_SetBool_Target_108);
		local_MsgSCUEnabled_System_Boolean = logic_uScriptAct_SetBool_Target_108;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_108.Out)
		{
			Relay_In_226();
		}
	}

	private void Relay_In_109()
	{
		logic_uScriptCon_CompareBool_Bool_109 = local_MsgSCUEnabled_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.In(logic_uScriptCon_CompareBool_Bool_109);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.False;
		if (num)
		{
			Relay_In_226();
		}
		if (flag)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_111()
	{
		logic_uScript_AddMessage_messageData_111 = msg06SCUEnabled;
		logic_uScript_AddMessage_speaker_111 = messageSpeakerDefault;
		logic_uScript_AddMessage_Return_111 = logic_uScript_AddMessage_uScript_AddMessage_111.In(logic_uScript_AddMessage_messageData_111, logic_uScript_AddMessage_speaker_111);
		if (logic_uScript_AddMessage_uScript_AddMessage_111.Shown)
		{
			Relay_In_115();
		}
	}

	private void Relay_In_114()
	{
		logic_uScript_AddMessage_messageData_114 = msg08MissionComplete;
		logic_uScript_AddMessage_speaker_114 = messageSpeakerDefault;
		logic_uScript_AddMessage_Return_114 = logic_uScript_AddMessage_uScript_AddMessage_114.In(logic_uScript_AddMessage_messageData_114, logic_uScript_AddMessage_speaker_114);
		if (logic_uScript_AddMessage_uScript_AddMessage_114.Shown)
		{
			Relay_In_216();
		}
	}

	private void Relay_In_115()
	{
		logic_uScript_EnableBlockPalette_uScript_EnableBlockPalette_115.In();
		if (logic_uScript_EnableBlockPalette_uScript_EnableBlockPalette_115.Out)
		{
			Relay_True_108();
		}
	}

	private void Relay_Save_Out_116()
	{
		Relay_Save_22();
	}

	private void Relay_Load_Out_116()
	{
		Relay_Load_22();
	}

	private void Relay_Restart_Out_116()
	{
		Relay_Set_False_22();
	}

	private void Relay_Save_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Save(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Load_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Load(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Set_True_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Set_False_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_In_118()
	{
		logic_uScriptCon_CompareBool_Bool_118 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.In(logic_uScriptCon_CompareBool_Bool_118);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.False;
		if (num)
		{
			Relay_In_33();
		}
		if (flag)
		{
			Relay_In_160();
		}
	}

	private void Relay_True_120()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_120.True(out logic_uScriptAct_SetBool_Target_120);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_120;
	}

	private void Relay_False_120()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_120.False(out logic_uScriptAct_SetBool_Target_120);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_120;
	}

	private void Relay_InitialSpawn_122()
	{
		int num = 0;
		Array array = alliedTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_122.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_122, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_122, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_122 = owner_Connection_129;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_122.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_122, logic_uScript_SpawnTechsFromData_ownerNode_122, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_122, logic_uScript_SpawnTechsFromData_allowResurrection_122);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_122.Out)
		{
			Relay_InitialSpawn_127();
		}
	}

	private void Relay_In_126()
	{
		logic_uScript_RemoveSceneryAtPosition_position_126 = local_VendorPos_UnityEngine_Vector3;
		logic_uScript_RemoveSceneryAtPosition_radius_126 = clearSceneryRadius;
		logic_uScript_RemoveSceneryAtPosition_uScript_RemoveSceneryAtPosition_126.In(logic_uScript_RemoveSceneryAtPosition_position_126, logic_uScript_RemoveSceneryAtPosition_radius_126, logic_uScript_RemoveSceneryAtPosition_preventChunksSpawning_126);
		if (logic_uScript_RemoveSceneryAtPosition_uScript_RemoveSceneryAtPosition_126.Out)
		{
			Relay_In_99();
		}
	}

	private void Relay_InitialSpawn_127()
	{
		int num = 0;
		Array array = enemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_127.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_127, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_127, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_127 = owner_Connection_124;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_127.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_127, logic_uScript_SpawnTechsFromData_ownerNode_127, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_127, logic_uScript_SpawnTechsFromData_allowResurrection_127);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_127.Out)
		{
			Relay_True_120();
		}
	}

	private void Relay_In_133()
	{
		logic_uScript_SetEncounterTarget_owner_133 = owner_Connection_132;
		logic_uScript_SetEncounterTarget_visibleObject_133 = local_GSOVendor_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_133.In(logic_uScript_SetEncounterTarget_owner_133, logic_uScript_SetEncounterTarget_visibleObject_133);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_133.Out)
		{
			Relay_InitialSpawn_122();
		}
	}

	private void Relay_In_134()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_134 = owner_Connection_136;
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_134.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_134, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_134, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_134 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_134.In(logic_uScript_SetOneTechAsEncounterTarget_owner_134, logic_uScript_SetOneTechAsEncounterTarget_techs_134);
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_134.Out)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_137()
	{
		logic_uScriptCon_CompareBool_Bool_137 = local_Stage3Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137.In(logic_uScriptCon_CompareBool_Bool_137);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137.False;
		if (num)
		{
			Relay_In_76();
		}
		if (flag)
		{
			Relay_In_59();
		}
	}

	private void Relay_In_138()
	{
		logic_uScriptCon_CompareInt_A_138 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_138.In(logic_uScriptCon_CompareInt_A_138, logic_uScriptCon_CompareInt_B_138);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_138.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_138.NotEqualTo;
		if (equalTo)
		{
			Relay_In_146();
		}
		if (notEqualTo)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_140()
	{
		logic_uScript_LockTech_tech_140 = local_AlliedTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_140.In(logic_uScript_LockTech_tech_140, logic_uScript_LockTech_lockType_140);
		if (logic_uScript_LockTech_uScript_LockTech_140.Out)
		{
			Relay_In_189();
		}
	}

	private void Relay_In_141()
	{
		logic_uScript_LockTech_tech_141 = local_AlliedTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_141.In(logic_uScript_LockTech_tech_141, logic_uScript_LockTech_lockType_141);
		if (logic_uScript_LockTech_uScript_LockTech_141.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_In_142()
	{
		logic_uScript_LockTech_tech_142 = local_AlliedTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_142.In(logic_uScript_LockTech_tech_142, logic_uScript_LockTech_lockType_142);
		if (logic_uScript_LockTech_uScript_LockTech_142.Out)
		{
			Relay_In_189();
		}
	}

	private void Relay_In_146()
	{
		logic_uScriptCon_CompareBool_Bool_146 = local_msgTurretIdle_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.In(logic_uScriptCon_CompareBool_Bool_146);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.False;
		if (num)
		{
			Relay_In_141();
		}
		if (flag)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_148()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148.Out)
		{
			Relay_In_189();
		}
	}

	private void Relay_In_151()
	{
		logic_uScript_SetEncounterPosition_ownerNode_151 = owner_Connection_154;
		logic_uScript_SetEncounterPosition_position_151 = local_VendorPos_UnityEngine_Vector3;
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_151.In(logic_uScript_SetEncounterPosition_ownerNode_151, logic_uScript_SetEncounterPosition_position_151);
		if (logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_151.Out)
		{
			Relay_True_156();
		}
	}

	private void Relay_True_156()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_156.True(out logic_uScriptAct_SetBool_Target_156);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_156;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_156.Out)
		{
			Relay_In_174();
		}
	}

	private void Relay_False_156()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_156.False(out logic_uScriptAct_SetBool_Target_156);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_156;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_156.Out)
		{
			Relay_In_174();
		}
	}

	private void Relay_In_157()
	{
		logic_uScript_TechInRangeOfPosition_tech_157 = local_PlayerTech_Tank;
		logic_uScript_TechInRangeOfPosition_position_157 = local_EncounterPos_UnityEngine_Vector3;
		logic_uScript_TechInRangeOfPosition_uScript_TechInRangeOfPosition_157.In(logic_uScript_TechInRangeOfPosition_tech_157, logic_uScript_TechInRangeOfPosition_position_157, logic_uScript_TechInRangeOfPosition_range_157);
		if (logic_uScript_TechInRangeOfPosition_uScript_TechInRangeOfPosition_157.True)
		{
			Relay_In_126();
		}
	}

	private void Relay_In_158()
	{
		logic_uScript_GetPlayerTank_Return_158 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_158.In();
		local_PlayerTech_Tank = logic_uScript_GetPlayerTank_Return_158;
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_158.Returned)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_160()
	{
		logic_uScript_GetCurrentEncounterPosition_ownerNode_160 = owner_Connection_161;
		logic_uScript_GetCurrentEncounterPosition_Return_160 = logic_uScript_GetCurrentEncounterPosition_uScript_GetCurrentEncounterPosition_160.In(logic_uScript_GetCurrentEncounterPosition_ownerNode_160);
		local_EncounterPos_UnityEngine_Vector3 = logic_uScript_GetCurrentEncounterPosition_Return_160;
		if (logic_uScript_GetCurrentEncounterPosition_uScript_GetCurrentEncounterPosition_160.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_164()
	{
		logic_uScript_GetSecondNearestVendorPos_Return_164 = logic_uScript_GetSecondNearestVendorPos_uScript_GetSecondNearestVendorPos_164.In();
		local_VendorPos_UnityEngine_Vector3 = logic_uScript_GetSecondNearestVendorPos_Return_164;
		if (logic_uScript_GetSecondNearestVendorPos_uScript_GetSecondNearestVendorPos_164.Found)
		{
			Relay_In_151();
		}
	}

	private void Relay_In_167()
	{
		logic_uScript_TechInRangeOfPosition_tech_167 = local_PlayerTech_Tank;
		logic_uScript_TechInRangeOfPosition_position_167 = local_EncounterPos_UnityEngine_Vector3;
		logic_uScript_TechInRangeOfPosition_range_167 = tradingStationFoundDist;
		logic_uScript_TechInRangeOfPosition_uScript_TechInRangeOfPosition_167.In(logic_uScript_TechInRangeOfPosition_tech_167, logic_uScript_TechInRangeOfPosition_position_167, logic_uScript_TechInRangeOfPosition_range_167);
		if (logic_uScript_TechInRangeOfPosition_uScript_TechInRangeOfPosition_167.True)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_168()
	{
		logic_uScript_GetPlayerTank_Return_168 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_168.In();
		local_PlayerTech_Tank = logic_uScript_GetPlayerTank_Return_168;
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_168.Returned)
		{
			Relay_In_167();
		}
	}

	private void Relay_In_170()
	{
		logic_uScript_GetCurrentEncounterPosition_ownerNode_170 = owner_Connection_171;
		logic_uScript_GetCurrentEncounterPosition_Return_170 = logic_uScript_GetCurrentEncounterPosition_uScript_GetCurrentEncounterPosition_170.In(logic_uScript_GetCurrentEncounterPosition_ownerNode_170);
		local_EncounterPos_UnityEngine_Vector3 = logic_uScript_GetCurrentEncounterPosition_Return_170;
		if (logic_uScript_GetCurrentEncounterPosition_uScript_GetCurrentEncounterPosition_170.Out)
		{
			Relay_In_168();
		}
	}

	private void Relay_In_174()
	{
		logic_uScript_FindNearestVendorToEncounter_ownerNode_174 = owner_Connection_175;
		logic_uScript_FindNearestVendorToEncounter_Return_174 = logic_uScript_FindNearestVendorToEncounter_uScript_FindNearestVendorToEncounter_174.In(logic_uScript_FindNearestVendorToEncounter_ownerNode_174);
		local_GSOVendor_Tank = logic_uScript_FindNearestVendorToEncounter_Return_174;
		if (logic_uScript_FindNearestVendorToEncounter_uScript_FindNearestVendorToEncounter_174.Returned)
		{
			Relay_In_118();
		}
	}

	private void Relay_In_178()
	{
		logic_uScript_LockTech_tech_178 = local_AlliedTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_178.In(logic_uScript_LockTech_tech_178, logic_uScript_LockTech_lockType_178);
	}

	private void Relay_In_181()
	{
		logic_uScript_SetEncounterTarget_owner_181 = owner_Connection_180;
		logic_uScript_SetEncounterTarget_visibleObject_181 = local_AlliedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_181.In(logic_uScript_SetEncounterTarget_owner_181, logic_uScript_SetEncounterTarget_visibleObject_181);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_181.Out)
		{
			Relay_In_137();
		}
	}

	private void Relay_In_183()
	{
		logic_uScriptCon_CompareInt_A_183 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_183.In(logic_uScriptCon_CompareInt_A_183, logic_uScriptCon_CompareInt_B_183);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_183.GreaterThanOrEqualTo;
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_183.EqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_183.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_188();
		}
		if (equalTo)
		{
			Relay_In_190();
		}
		if (lessThan)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_185()
	{
		logic_uScript_SetCustomRadarTeamID_tech_185 = local_AlliedTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_185.In(logic_uScript_SetCustomRadarTeamID_tech_185, logic_uScript_SetCustomRadarTeamID_radarTeamID_185);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_185.Out)
		{
			Relay_In_192();
		}
	}

	private void Relay_In_188()
	{
		logic_uScript_SetCustomRadarTeamID_tech_188 = local_AlliedTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_188.In(logic_uScript_SetCustomRadarTeamID_tech_188, logic_uScript_SetCustomRadarTeamID_radarTeamID_188);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_188.Out)
		{
			Relay_In_270();
		}
	}

	private void Relay_In_189()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_189.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_189.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_In_190()
	{
		logic_uScript_SetTankTeam_tank_190 = local_AlliedTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_190.In(logic_uScript_SetTankTeam_tank_190, logic_uScript_SetTankTeam_team_190);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_190.Out)
		{
			Relay_In_217();
		}
	}

	private void Relay_In_192()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_192.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_192.Out)
		{
			Relay_In_212();
		}
	}

	private void Relay_In_193()
	{
		logic_uScript_SetEncounterTarget_owner_193 = owner_Connection_194;
		logic_uScript_SetEncounterTarget_visibleObject_193 = local_GSOVendor_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_193.In(logic_uScript_SetEncounterTarget_owner_193, logic_uScript_SetEncounterTarget_visibleObject_193);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_193.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_197()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_197 = local_198_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_197.In(logic_uScript_HasHintBeenShownBefore_hintID_197);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_197.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_197.NotShown;
		if (shown)
		{
			Relay_Succeed_18();
		}
		if (notShown)
		{
			Relay_In_199();
		}
	}

	private void Relay_In_199()
	{
		logic_uScript_ShowHint_hintId_199 = local_198_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_199.In(logic_uScript_ShowHint_hintId_199);
		if (logic_uScript_ShowHint_uScript_ShowHint_199.Out)
		{
			Relay_Succeed_18();
		}
	}

	private void Relay_In_200()
	{
		logic_uScript_HideHintFloating_uScript_HideHintFloating_200.In();
		if (logic_uScript_HideHintFloating_uScript_HideHintFloating_200.Out)
		{
			Relay_True_229();
		}
	}

	private void Relay_In_201()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_201 = local_Msg07OpenInventory_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_201.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_201, logic_uScript_RemoveOnScreenMessage_instant_201);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_201.Out)
		{
			Relay_In_236();
		}
	}

	private void Relay_Save_Out_204()
	{
		Relay_Save_206();
	}

	private void Relay_Load_Out_204()
	{
		Relay_Load_206();
	}

	private void Relay_Restart_Out_204()
	{
		Relay_Set_False_206();
	}

	private void Relay_Save_204()
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = local_msgDefeatEnemies_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_204 = local_msgDefeatEnemies_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Save(ref logic_SubGraph_SaveLoadBool_boolean_204, logic_SubGraph_SaveLoadBool_boolAsVariable_204, logic_SubGraph_SaveLoadBool_uniqueID_204);
	}

	private void Relay_Load_204()
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = local_msgDefeatEnemies_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_204 = local_msgDefeatEnemies_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Load(ref logic_SubGraph_SaveLoadBool_boolean_204, logic_SubGraph_SaveLoadBool_boolAsVariable_204, logic_SubGraph_SaveLoadBool_uniqueID_204);
	}

	private void Relay_Set_True_204()
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = local_msgDefeatEnemies_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_204 = local_msgDefeatEnemies_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_204, logic_SubGraph_SaveLoadBool_boolAsVariable_204, logic_SubGraph_SaveLoadBool_uniqueID_204);
	}

	private void Relay_Set_False_204()
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = local_msgDefeatEnemies_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_204 = local_msgDefeatEnemies_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_204, logic_SubGraph_SaveLoadBool_boolAsVariable_204, logic_SubGraph_SaveLoadBool_uniqueID_204);
	}

	private void Relay_Save_Out_206()
	{
		Relay_Save_208();
	}

	private void Relay_Load_Out_206()
	{
		Relay_Load_208();
	}

	private void Relay_Restart_Out_206()
	{
		Relay_Set_False_208();
	}

	private void Relay_Save_206()
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = local_msgTurretIdle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_206 = local_msgTurretIdle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Save(ref logic_SubGraph_SaveLoadBool_boolean_206, logic_SubGraph_SaveLoadBool_boolAsVariable_206, logic_SubGraph_SaveLoadBool_uniqueID_206);
	}

	private void Relay_Load_206()
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = local_msgTurretIdle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_206 = local_msgTurretIdle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Load(ref logic_SubGraph_SaveLoadBool_boolean_206, logic_SubGraph_SaveLoadBool_boolAsVariable_206, logic_SubGraph_SaveLoadBool_uniqueID_206);
	}

	private void Relay_Set_True_206()
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = local_msgTurretIdle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_206 = local_msgTurretIdle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_206, logic_SubGraph_SaveLoadBool_boolAsVariable_206, logic_SubGraph_SaveLoadBool_uniqueID_206);
	}

	private void Relay_Set_False_206()
	{
		logic_SubGraph_SaveLoadBool_boolean_206 = local_msgTurretIdle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_206 = local_msgTurretIdle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_206.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_206, logic_SubGraph_SaveLoadBool_boolAsVariable_206, logic_SubGraph_SaveLoadBool_uniqueID_206);
	}

	private void Relay_Save_Out_208()
	{
		Relay_Save_230();
	}

	private void Relay_Load_Out_208()
	{
		Relay_Load_230();
	}

	private void Relay_Restart_Out_208()
	{
		Relay_Set_False_230();
	}

	private void Relay_Save_208()
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = local_MsgSCUEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_208 = local_MsgSCUEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Save(ref logic_SubGraph_SaveLoadBool_boolean_208, logic_SubGraph_SaveLoadBool_boolAsVariable_208, logic_SubGraph_SaveLoadBool_uniqueID_208);
	}

	private void Relay_Load_208()
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = local_MsgSCUEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_208 = local_MsgSCUEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Load(ref logic_SubGraph_SaveLoadBool_boolean_208, logic_SubGraph_SaveLoadBool_boolAsVariable_208, logic_SubGraph_SaveLoadBool_uniqueID_208);
	}

	private void Relay_Set_True_208()
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = local_MsgSCUEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_208 = local_MsgSCUEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_208, logic_SubGraph_SaveLoadBool_boolAsVariable_208, logic_SubGraph_SaveLoadBool_uniqueID_208);
	}

	private void Relay_Set_False_208()
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = local_MsgSCUEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_208 = local_MsgSCUEnabled_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_208, logic_SubGraph_SaveLoadBool_boolAsVariable_208, logic_SubGraph_SaveLoadBool_uniqueID_208);
	}

	private void Relay_In_211()
	{
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_211.In(logic_uScript_SetVendorsEnabled_enableShop_211, logic_uScript_SetVendorsEnabled_enableMissionBoard_211, logic_uScript_SetVendorsEnabled_enableSelling_211, logic_uScript_SetVendorsEnabled_enableSCU_211, logic_uScript_SetVendorsEnabled_enableCharging_211);
		if (logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_211.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_212()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_212 = owner_Connection_213;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_212.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_212);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_212.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_212.False;
		if (num)
		{
			Relay_In_225();
		}
		if (flag)
		{
			Relay_In_211();
		}
	}

	private void Relay_In_214()
	{
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_214.In(logic_uScript_SetVendorsEnabled_enableShop_214, logic_uScript_SetVendorsEnabled_enableMissionBoard_214, logic_uScript_SetVendorsEnabled_enableSelling_214, logic_uScript_SetVendorsEnabled_enableSCU_214, logic_uScript_SetVendorsEnabled_enableCharging_214);
		if (logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_214.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_216()
	{
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_216.In(logic_uScript_SetVendorsEnabled_enableShop_216, logic_uScript_SetVendorsEnabled_enableMissionBoard_216, logic_uScript_SetVendorsEnabled_enableSelling_216, logic_uScript_SetVendorsEnabled_enableSCU_216, logic_uScript_SetVendorsEnabled_enableCharging_216);
		if (logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_216.Out)
		{
			Relay_In_232();
		}
	}

	private void Relay_In_217()
	{
		logic_uScriptCon_CompareBool_Bool_217 = local_Stage3Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_217.In(logic_uScriptCon_CompareBool_Bool_217);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_217.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_217.False;
		if (num)
		{
			Relay_In_220();
		}
		if (flag)
		{
			Relay_In_222();
		}
	}

	private void Relay_In_220()
	{
		logic_uScript_SetTechAIType_tech_220 = local_AlliedTech_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_220.In(logic_uScript_SetTechAIType_tech_220, logic_uScript_SetTechAIType_aiType_220);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_220.Out)
		{
			Relay_In_185();
		}
	}

	private void Relay_In_221()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221.Out)
		{
			Relay_In_192();
		}
	}

	private void Relay_In_222()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222.Out)
		{
			Relay_In_185();
		}
	}

	private void Relay_In_224()
	{
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_224.In(logic_uScript_SetVendorsEnabled_enableShop_224, logic_uScript_SetVendorsEnabled_enableMissionBoard_224, logic_uScript_SetVendorsEnabled_enableSelling_224, logic_uScript_SetVendorsEnabled_enableSCU_224, logic_uScript_SetVendorsEnabled_enableCharging_224);
		if (logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_224.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_225()
	{
		logic_uScriptCon_CompareBool_Bool_225 = local_Stage4Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.In(logic_uScriptCon_CompareBool_Bool_225);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.False;
		if (num)
		{
			Relay_In_224();
		}
		if (flag)
		{
			Relay_In_214();
		}
	}

	private void Relay_In_226()
	{
		logic_uScriptCon_CompareBool_Bool_226 = local_InventoryOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_226.In(logic_uScriptCon_CompareBool_Bool_226);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_226.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_226.False;
		if (num)
		{
			Relay_In_114();
		}
		if (flag)
		{
			Relay_In_104();
		}
	}

	private void Relay_True_229()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_229.True(out logic_uScriptAct_SetBool_Target_229);
		local_InventoryOpened_System_Boolean = logic_uScriptAct_SetBool_Target_229;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_229.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_False_229()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_229.False(out logic_uScriptAct_SetBool_Target_229);
		local_InventoryOpened_System_Boolean = logic_uScriptAct_SetBool_Target_229;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_229.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_Save_Out_230()
	{
		Relay_Save_6();
	}

	private void Relay_Load_Out_230()
	{
		Relay_Load_6();
	}

	private void Relay_Restart_Out_230()
	{
		Relay_Restart_6();
	}

	private void Relay_Save_230()
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = local_InventoryOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_230 = local_InventoryOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Save(ref logic_SubGraph_SaveLoadBool_boolean_230, logic_SubGraph_SaveLoadBool_boolAsVariable_230, logic_SubGraph_SaveLoadBool_uniqueID_230);
	}

	private void Relay_Load_230()
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = local_InventoryOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_230 = local_InventoryOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Load(ref logic_SubGraph_SaveLoadBool_boolean_230, logic_SubGraph_SaveLoadBool_boolAsVariable_230, logic_SubGraph_SaveLoadBool_uniqueID_230);
	}

	private void Relay_Set_True_230()
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = local_InventoryOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_230 = local_InventoryOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_230, logic_SubGraph_SaveLoadBool_boolAsVariable_230, logic_SubGraph_SaveLoadBool_uniqueID_230);
	}

	private void Relay_Set_False_230()
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = local_InventoryOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_230 = local_InventoryOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_230, logic_SubGraph_SaveLoadBool_boolAsVariable_230, logic_SubGraph_SaveLoadBool_uniqueID_230);
	}

	private void Relay_In_232()
	{
		logic_uScript_LockSendToSCU_uScript_LockSendToSCU_232.In(logic_uScript_LockSendToSCU_lockSendToSCU_232);
		if (logic_uScript_LockSendToSCU_uScript_LockSendToSCU_232.Out)
		{
			Relay_In_197();
		}
	}

	private void Relay_In_233()
	{
		logic_uScript_LockSendToSCU_uScript_LockSendToSCU_233.In(logic_uScript_LockSendToSCU_lockSendToSCU_233);
		if (logic_uScript_LockSendToSCU_uScript_LockSendToSCU_233.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_234()
	{
		logic_uScript_SetTechAIType_tech_234 = local_AlliedTech_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_234.In(logic_uScript_SetTechAIType_tech_234, logic_uScript_SetTechAIType_aiType_234);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_234.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_In_236()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_236 = local_Msg07OpenInventory_Pad2_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_236.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_236, logic_uScript_RemoveOnScreenMessage_instant_236);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_236.Out)
		{
			Relay_In_200();
		}
	}

	private void Relay_Out_238()
	{
		Relay_In_252();
	}

	private void Relay_Shown_238()
	{
	}

	private void Relay_In_238()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_238 = msg04aOpenAIMenu;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_238 = msg04aOpenAIMenu_Pad2;
		logic_SubGraph_AddMessageWithPadSupport_speaker_238 = messageSpeakerDefault;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_238.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_238, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_238, logic_SubGraph_AddMessageWithPadSupport_speaker_238);
	}

	private void Relay_In_239()
	{
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_239.In(logic_uScript_IsHUDElementVisible_hudElement_239);
		bool num = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_239.True;
		bool flag = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_239.False;
		if (num)
		{
			Relay_In_244();
		}
		if (flag)
		{
			Relay_In_247();
		}
	}

	private void Relay_Out_244()
	{
		Relay_In_255();
	}

	private void Relay_Shown_244()
	{
	}

	private void Relay_In_244()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_244 = msg04bSelectGuard;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_244 = msg04bSelectGuard_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_244 = messageSpeakerDefault;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_244.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_244, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_244, logic_SubGraph_AddMessageWithPadSupport_speaker_244);
	}

	private void Relay_In_247()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_247.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_247.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_247.MouseAndKeyboard;
		if (joypad)
		{
			Relay_In_250();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_256();
		}
	}

	private void Relay_In_250()
	{
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_250.In(logic_uScript_IsHUDElementVisible_hudElement_250);
		bool num = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_250.True;
		bool flag = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_250.False;
		if (num)
		{
			Relay_In_238();
		}
		if (flag)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_251()
	{
		logic_uScript_AddMessage_messageData_251 = msg04aOpenAIMenu_Pad1;
		logic_uScript_AddMessage_speaker_251 = messageSpeakerDefault;
		logic_uScript_AddMessage_Return_251 = logic_uScript_AddMessage_uScript_AddMessage_251.In(logic_uScript_AddMessage_messageData_251, logic_uScript_AddMessage_speaker_251);
	}

	private void Relay_In_252()
	{
		logic_uScript_PointArrowAtVisible_targetObject_252 = local_AlliedTech_Tank;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_252.In(logic_uScript_PointArrowAtVisible_targetObject_252, logic_uScript_PointArrowAtVisible_timeToShowFor_252, logic_uScript_PointArrowAtVisible_offset_252);
	}

	private void Relay_In_255()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_255.In();
	}

	private void Relay_In_256()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_256.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_256.Out)
		{
			Relay_In_238();
		}
	}

	private void Relay_In_257()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_257.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_257.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_257.MouseAndKeyboard;
		if (joypad)
		{
			Relay_In_265();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_266();
		}
	}

	private void Relay_In_258()
	{
		logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_258.In();
		bool num = logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_258.True;
		bool flag = logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_258.False;
		if (num)
		{
			Relay_In_267();
		}
		if (flag)
		{
			Relay_In_259();
		}
	}

	private void Relay_In_259()
	{
		logic_uScript_AddMessage_messageData_259 = msg07OpenInventory_Pad1;
		logic_uScript_AddMessage_speaker_259 = messageSpeakerDefault;
		logic_uScript_AddMessage_Return_259 = logic_uScript_AddMessage_uScript_AddMessage_259.In(logic_uScript_AddMessage_messageData_259, logic_uScript_AddMessage_speaker_259);
	}

	private void Relay_Out_265()
	{
	}

	private void Relay_Shown_265()
	{
	}

	private void Relay_In_265()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_265 = msg07OpenInventory;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_265 = msg07OpenInventory_Pad2;
		logic_SubGraph_AddMessageWithPadSupport_speaker_265 = messageSpeakerDefault;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_265.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_265, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_265, logic_SubGraph_AddMessageWithPadSupport_speaker_265);
	}

	private void Relay_Out_266()
	{
		Relay_In_265();
	}

	private void Relay_In_266()
	{
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_266.In(logic_SubGraph_ShowHintWithPadSupport_hintControlPad_266, logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_266);
	}

	private void Relay_In_267()
	{
		logic_uScript_Wait_uScript_Wait_267.In(logic_uScript_Wait_seconds_267, logic_uScript_Wait_repeat_267);
		if (logic_uScript_Wait_uScript_Wait_267.Waited)
		{
			Relay_In_265();
		}
	}

	private void Relay_In_270()
	{
		logic_uScript_SetTankHideBlockLimit_tech_270 = local_AlliedTech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_270.In(logic_uScript_SetTankHideBlockLimit_hidden_270, logic_uScript_SetTankHideBlockLimit_tech_270);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_270.Out)
		{
			Relay_In_192();
		}
	}

	private void Relay_In_271()
	{
		logic_uScript_SetTankHideBlockLimit_tech_271 = local_AlliedTech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_271.In(logic_uScript_SetTankHideBlockLimit_hidden_271, logic_uScript_SetTankHideBlockLimit_tech_271);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_271.Out)
		{
			Relay_In_221();
		}
	}
}
