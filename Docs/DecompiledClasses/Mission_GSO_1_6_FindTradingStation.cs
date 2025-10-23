using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_GSO_1_6_FindTradingStation : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public float bossBatteryCharge;

	public float clearSceneryRadius;

	private float local_118_System_Single;

	private GameHints.HintID local_89_GameHints_HintID = GameHints.HintID.DefeatedByTraderTroll2;

	private GameHints.HintID local_93_GameHints_HintID = GameHints.HintID.DefeatedByTraderTroll1;

	private Tank[] local_BossList_TankArray = new Tank[0];

	private Tank local_GSOVendor_Tank;

	private bool local_Init_System_Boolean;

	private bool local_InitialBatteryCharge_System_Boolean;

	private Vector3 local_InitialPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private ManOnScreenMessages.OnScreenMessage local_Msg04DefeatTroll_ManOnScreenMessages_OnScreenMessage;

	private bool local_msgTradingStationFoundShown_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private bool local_TechsSpawned_System_Boolean;

	private bool local_TrollDead_System_Boolean;

	private Tank local_VendorBoss_Tank;

	private Vector3 local_VendorPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01FindTradingStation;

	public uScript_AddMessage.MessageData msg02TradingStationFound;

	public uScript_AddMessage.MessageData msg03TrollIncoming;

	public uScript_AddMessage.MessageData msg04DefeatTroll;

	public SpawnTechData SpawnDataBoss;

	public float TrollAchievementTimeSeconds;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_24;

	private GameObject owner_Connection_26;

	private GameObject owner_Connection_72;

	private GameObject owner_Connection_76;

	private GameObject owner_Connection_131;

	private GameObject owner_Connection_139;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_0 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_0;

	private float logic_uScript_IsPlayerInRangeOfTech_range_0 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_0 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_0 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_0 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_0 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_4 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_4;

	private object logic_uScript_SetEncounterTarget_visibleObject_4 = "";

	private bool logic_uScript_SetEncounterTarget_Out_4 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_5 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_5;

	private float logic_uScript_IsPlayerInRangeOfTech_range_5 = 50f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_5 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_5 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_5 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_5 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_9 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_9;

	private bool logic_uScriptAct_SetBool_Out_9 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_9 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_9 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_10;

	private bool logic_uScriptCon_CompareBool_True_10 = true;

	private bool logic_uScriptCon_CompareBool_False_10 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_12 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_12 = new Tank[0];

	private int logic_uScript_AccessListTech_index_12;

	private Tank logic_uScript_AccessListTech_value_12;

	private bool logic_uScript_AccessListTech_Out_12 = true;

	private uScript_AI_SetPOI logic_uScript_AI_SetPOI_uScript_AI_SetPOI_16 = new uScript_AI_SetPOI();

	private Tank logic_uScript_AI_SetPOI_tank_16;

	private bool logic_uScript_AI_SetPOI_usePOI_16 = true;

	private Vector3 logic_uScript_AI_SetPOI_position_16;

	private float logic_uScript_AI_SetPOI_distance_16 = 250f;

	private bool logic_uScript_AI_SetPOI_Out_16 = true;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_20 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_20;

	private string logic_uScript_GetPositionInEncounter_posName_20 = "VendorBoss";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_20;

	private bool logic_uScript_GetPositionInEncounter_Out_20 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_25 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_25;

	private object logic_uScript_SetEncounterTarget_visibleObject_25 = "";

	private bool logic_uScript_SetEncounterTarget_Out_25 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_29;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_29 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_29 = "TechsSpawned";

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_30 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_30 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_30 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_30 = true;

	private uScript_GetNearestVendorPos logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_32 = new uScript_GetNearestVendorPos();

	private Vector3 logic_uScript_GetNearestVendorPos_Return_32;

	private bool logic_uScript_GetNearestVendorPos_Out_32 = true;

	private bool logic_uScript_GetNearestVendorPos_Found_32 = true;

	private bool logic_uScript_GetNearestVendorPos_Missing_32 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_35 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_37 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_37;

	private bool logic_uScript_FinishEncounter_Out_37 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_38;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_40 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_40;

	private bool logic_uScriptAct_SetBool_Out_40 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_40 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_40 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_42;

	private bool logic_uScriptCon_CompareBool_True_42 = true;

	private bool logic_uScriptCon_CompareBool_False_42 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_44 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_44;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_47 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_47;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_47 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_47 = "Stage";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_48 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_54 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_54;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_54;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_57 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_57;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_57;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_57;

	private bool logic_uScript_AddMessage_Out_57 = true;

	private bool logic_uScript_AddMessage_Shown_57 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_58 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_58;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_58;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_58;

	private bool logic_uScript_AddMessage_Out_58 = true;

	private bool logic_uScript_AddMessage_Shown_58 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_61 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_61;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_61;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_61;

	private bool logic_uScript_AddMessage_Out_61 = true;

	private bool logic_uScript_AddMessage_Shown_61 = true;

	private uScript_SetVendorsVisibleOnRadar logic_uScript_SetVendorsVisibleOnRadar_uScript_SetVendorsVisibleOnRadar_65 = new uScript_SetVendorsVisibleOnRadar();

	private bool logic_uScript_SetVendorsVisibleOnRadar_Out_65 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_67;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_67 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_67 = "Init";

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_68 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_68;

	private int logic_uScriptCon_CompareInt_B_68 = 1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_68 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_68 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_68 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_68 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_68 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_68 = true;

	private uScript_SetEncounterPosition logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_71 = new uScript_SetEncounterPosition();

	private GameObject logic_uScript_SetEncounterPosition_ownerNode_71;

	private Vector3 logic_uScript_SetEncounterPosition_position_71;

	private bool logic_uScript_SetEncounterPosition_Out_71 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_73 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_73 = true;

	private uScript_FindNearestVendor logic_uScript_FindNearestVendor_uScript_FindNearestVendor_74 = new uScript_FindNearestVendor();

	private Tank logic_uScript_FindNearestVendor_Return_74;

	private bool logic_uScript_FindNearestVendor_Out_74 = true;

	private bool logic_uScript_FindNearestVendor_Returned_74 = true;

	private bool logic_uScript_FindNearestVendor_NotReturned_74 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_75 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_75;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_75 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_75 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_79 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_79;

	private bool logic_uScriptCon_CompareBool_True_79 = true;

	private bool logic_uScriptCon_CompareBool_False_79 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_80 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_80;

	private bool logic_uScriptAct_SetBool_Out_80 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_80 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_80 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_83;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_83 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_83 = "msgTradingStationFoundShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_85;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_85 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_85 = "TrollDead";

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_87 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_87;

	private bool logic_uScript_ShowHint_Out_87 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_88 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_88;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_88 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_88 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_90 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_90;

	private bool logic_uScript_GetPlayerTank_Returned_90 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_90 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_92 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_92;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_92 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_92 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_94 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_94;

	private bool logic_uScript_ShowHint_Out_94 = true;

	private uScript_RemoveSceneryAtPosition logic_uScript_RemoveSceneryAtPosition_uScript_RemoveSceneryAtPosition_95 = new uScript_RemoveSceneryAtPosition();

	private Vector3 logic_uScript_RemoveSceneryAtPosition_position_95;

	private float logic_uScript_RemoveSceneryAtPosition_radius_95;

	private bool logic_uScript_RemoveSceneryAtPosition_preventChunksSpawning_95 = true;

	private bool logic_uScript_RemoveSceneryAtPosition_Out_95 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_97;

	private bool logic_uScriptCon_CompareBool_True_97 = true;

	private bool logic_uScriptCon_CompareBool_False_97 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_100 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_100;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_100;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_101;

	private bool logic_uScriptCon_CompareBool_True_101 = true;

	private bool logic_uScriptCon_CompareBool_False_101 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_103 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_103 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_104 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_107;

	private bool logic_uScriptCon_CompareBool_True_107 = true;

	private bool logic_uScriptCon_CompareBool_False_107 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_108 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_108;

	private bool logic_uScriptAct_SetBool_Out_108 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_108 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_108 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_110;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_110 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_110 = "InitialBatteryCharge";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_112 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_112;

	private bool logic_uScriptAct_SetBool_Out_112 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_112 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_112 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_113 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_113 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_114 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_114 = true;

	private uScript_CompleteAchievement logic_uScript_CompleteAchievement_uScript_CompleteAchievement_116 = new uScript_CompleteAchievement();

	private ManAchievements.AchievementTypes logic_uScript_CompleteAchievement_achievementID_116 = ManAchievements.AchievementTypes.DefeatTraderTrollInTime;

	private bool logic_uScript_CompleteAchievement_Out_116 = true;

	private uScript_GetModeRunningTime logic_uScript_GetModeRunningTime_uScript_GetModeRunningTime_117 = new uScript_GetModeRunningTime();

	private float logic_uScript_GetModeRunningTime_Return_117;

	private bool logic_uScript_GetModeRunningTime_Out_117 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_119 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_119;

	private float logic_uScriptCon_CompareFloat_B_119;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_119 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_119 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_119 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_119 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_119 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_119 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_122 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_122;

	private bool logic_uScript_RemoveOnScreenMessage_instant_122 = true;

	private bool logic_uScript_RemoveOnScreenMessage_Out_122 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_126 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_126 = "tutorial_stage";

	private string logic_uScript_SendAnaliticsEvent_parameterName_126 = "stage_complete";

	private object logic_uScript_SendAnaliticsEvent_parameter_126 = "trader_troll";

	private bool logic_uScript_SendAnaliticsEvent_Out_126 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_127 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_127;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_127;

	private bool logic_uScript_SetBatteryChargeAmount_Out_127 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_133 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_133 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_133;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_133 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_133;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_133 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_133 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_133 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_133 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_136 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_136;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_136;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_136;

	private bool logic_uScript_AddMessage_Out_136 = true;

	private bool logic_uScript_AddMessage_Shown_136 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_138 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_138 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_138;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_138;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_138;

	private bool logic_uScript_SpawnTechsFromData_Out_138 = true;

	private uScript_SetVendorsEnabled logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_140 = new uScript_SetVendorsEnabled();

	private bool logic_uScript_SetVendorsEnabled_enableShop_140;

	private bool logic_uScript_SetVendorsEnabled_enableMissionBoard_140;

	private bool logic_uScript_SetVendorsEnabled_enableSelling_140;

	private bool logic_uScript_SetVendorsEnabled_enableSCU_140;

	private bool logic_uScript_SetVendorsEnabled_enableCharging_140;

	private bool logic_uScript_SetVendorsEnabled_Out_140 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
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
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_8;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_8;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_8;
				}
			}
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
		}
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
		}
		if (null == owner_Connection_24 || !m_RegisteredForEvents)
		{
			owner_Connection_24 = parentGameObject;
		}
		if (null == owner_Connection_26 || !m_RegisteredForEvents)
		{
			owner_Connection_26 = parentGameObject;
			if (null != owner_Connection_26)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_26.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_26.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_27;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_27;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_27;
				}
			}
		}
		if (null == owner_Connection_72 || !m_RegisteredForEvents)
		{
			owner_Connection_72 = parentGameObject;
		}
		if (null == owner_Connection_76 || !m_RegisteredForEvents)
		{
			owner_Connection_76 = parentGameObject;
		}
		if (null == owner_Connection_131 || !m_RegisteredForEvents)
		{
			owner_Connection_131 = parentGameObject;
		}
		if (null == owner_Connection_139 || !m_RegisteredForEvents)
		{
			owner_Connection_139 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_7)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_7.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_7.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_8;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_8;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_8;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_26)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_26.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_26.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_27;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_27;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_27;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_7)
		{
			uScript_EncounterUpdate component = owner_Connection_7.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_8;
				component.OnSuspend -= Instance_OnSuspend_8;
				component.OnResume -= Instance_OnResume_8;
			}
		}
		if (null != owner_Connection_26)
		{
			uScript_SaveLoad component2 = owner_Connection_26.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_27;
				component2.LoadEvent -= Instance_LoadEvent_27;
				component2.RestartEvent -= Instance_RestartEvent_27;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_0.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_4.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_5.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_9.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_12.SetParent(g);
		logic_uScript_AI_SetPOI_uScript_AI_SetPOI_16.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_20.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_25.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_30.SetParent(g);
		logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_32.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_37.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_44.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_54.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_57.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_58.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_61.SetParent(g);
		logic_uScript_SetVendorsVisibleOnRadar_uScript_SetVendorsVisibleOnRadar_65.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_68.SetParent(g);
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_71.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_73.SetParent(g);
		logic_uScript_FindNearestVendor_uScript_FindNearestVendor_74.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_75.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_79.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_87.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_88.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_90.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_92.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_94.SetParent(g);
		logic_uScript_RemoveSceneryAtPosition_uScript_RemoveSceneryAtPosition_95.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_100.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_103.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_113.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_114.SetParent(g);
		logic_uScript_CompleteAchievement_uScript_CompleteAchievement_116.SetParent(g);
		logic_uScript_GetModeRunningTime_uScript_GetModeRunningTime_117.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_119.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_122.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_126.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_127.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_133.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_136.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_138.SetParent(g);
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_140.SetParent(g);
		owner_Connection_3 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_24 = parentGameObject;
		owner_Connection_26 = parentGameObject;
		owner_Connection_72 = parentGameObject;
		owner_Connection_76 = parentGameObject;
		owner_Connection_131 = parentGameObject;
		owner_Connection_139 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_44.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_54.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_100.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Save_Out += SubGraph_SaveLoadBool_Save_Out_29;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Load_Out += SubGraph_SaveLoadBool_Load_Out_29;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output1 += uScriptCon_ManualSwitch_Output1_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output2 += uScriptCon_ManualSwitch_Output2_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output3 += uScriptCon_ManualSwitch_Output3_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output4 += uScriptCon_ManualSwitch_Output4_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output5 += uScriptCon_ManualSwitch_Output5_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output6 += uScriptCon_ManualSwitch_Output6_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output7 += uScriptCon_ManualSwitch_Output7_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output8 += uScriptCon_ManualSwitch_Output8_38;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_44.Out += SubGraph_LoadObjectiveStates_Out_44;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.Save_Out += SubGraph_SaveLoadInt_Save_Out_47;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.Load_Out += SubGraph_SaveLoadInt_Load_Out_47;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_47;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_54.Out += SubGraph_CompleteObjectiveStage_Out_54;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Save_Out += SubGraph_SaveLoadBool_Save_Out_67;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Load_Out += SubGraph_SaveLoadBool_Load_Out_67;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_67;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Save_Out += SubGraph_SaveLoadBool_Save_Out_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Load_Out += SubGraph_SaveLoadBool_Load_Out_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Save_Out += SubGraph_SaveLoadBool_Save_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Load_Out += SubGraph_SaveLoadBool_Load_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_85;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_100.Out += SubGraph_CompleteObjectiveStage_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Save_Out += SubGraph_SaveLoadBool_Save_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Load_Out += SubGraph_SaveLoadBool_Load_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_110;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_44.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_54.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_100.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.OnEnable();
		logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_32.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_44.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_54.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_88.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_92.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_100.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_0.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_5.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_44.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_54.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_57.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_58.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_61.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_100.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_136.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_44.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_54.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_100.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_44.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_54.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_100.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Save_Out -= SubGraph_SaveLoadBool_Save_Out_29;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Load_Out -= SubGraph_SaveLoadBool_Load_Out_29;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output1 -= uScriptCon_ManualSwitch_Output1_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output2 -= uScriptCon_ManualSwitch_Output2_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output3 -= uScriptCon_ManualSwitch_Output3_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output4 -= uScriptCon_ManualSwitch_Output4_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output5 -= uScriptCon_ManualSwitch_Output5_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output6 -= uScriptCon_ManualSwitch_Output6_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output7 -= uScriptCon_ManualSwitch_Output7_38;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.Output8 -= uScriptCon_ManualSwitch_Output8_38;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_44.Out -= SubGraph_LoadObjectiveStates_Out_44;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.Save_Out -= SubGraph_SaveLoadInt_Save_Out_47;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.Load_Out -= SubGraph_SaveLoadInt_Load_Out_47;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_47;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_54.Out -= SubGraph_CompleteObjectiveStage_Out_54;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Save_Out -= SubGraph_SaveLoadBool_Save_Out_67;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Load_Out -= SubGraph_SaveLoadBool_Load_Out_67;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_67;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Save_Out -= SubGraph_SaveLoadBool_Save_Out_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Load_Out -= SubGraph_SaveLoadBool_Load_Out_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Save_Out -= SubGraph_SaveLoadBool_Save_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Load_Out -= SubGraph_SaveLoadBool_Load_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_85;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_100.Out -= SubGraph_CompleteObjectiveStage_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Save_Out -= SubGraph_SaveLoadBool_Save_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Load_Out -= SubGraph_SaveLoadBool_Load_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_110;
	}

	private void Instance_OnUpdate_8(object o, EventArgs e)
	{
		Relay_OnUpdate_8();
	}

	private void Instance_OnSuspend_8(object o, EventArgs e)
	{
		Relay_OnSuspend_8();
	}

	private void Instance_OnResume_8(object o, EventArgs e)
	{
		Relay_OnResume_8();
	}

	private void Instance_SaveEvent_27(object o, EventArgs e)
	{
		Relay_SaveEvent_27();
	}

	private void Instance_LoadEvent_27(object o, EventArgs e)
	{
		Relay_LoadEvent_27();
	}

	private void Instance_RestartEvent_27(object o, EventArgs e)
	{
		Relay_RestartEvent_27();
	}

	private void SubGraph_SaveLoadBool_Save_Out_29(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_29;
		Relay_Save_Out_29();
	}

	private void SubGraph_SaveLoadBool_Load_Out_29(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_29;
		Relay_Load_Out_29();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_29(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_29;
		Relay_Restart_Out_29();
	}

	private void uScriptCon_ManualSwitch_Output1_38(object o, EventArgs e)
	{
		Relay_Output1_38();
	}

	private void uScriptCon_ManualSwitch_Output2_38(object o, EventArgs e)
	{
		Relay_Output2_38();
	}

	private void uScriptCon_ManualSwitch_Output3_38(object o, EventArgs e)
	{
		Relay_Output3_38();
	}

	private void uScriptCon_ManualSwitch_Output4_38(object o, EventArgs e)
	{
		Relay_Output4_38();
	}

	private void uScriptCon_ManualSwitch_Output5_38(object o, EventArgs e)
	{
		Relay_Output5_38();
	}

	private void uScriptCon_ManualSwitch_Output6_38(object o, EventArgs e)
	{
		Relay_Output6_38();
	}

	private void uScriptCon_ManualSwitch_Output7_38(object o, EventArgs e)
	{
		Relay_Output7_38();
	}

	private void uScriptCon_ManualSwitch_Output8_38(object o, EventArgs e)
	{
		Relay_Output8_38();
	}

	private void SubGraph_LoadObjectiveStates_Out_44(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_44();
	}

	private void SubGraph_SaveLoadInt_Save_Out_47(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_47 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_47;
		Relay_Save_Out_47();
	}

	private void SubGraph_SaveLoadInt_Load_Out_47(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_47 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_47;
		Relay_Load_Out_47();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_47(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_47 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_47;
		Relay_Restart_Out_47();
	}

	private void SubGraph_CompleteObjectiveStage_Out_54(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_54 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_54;
		Relay_Out_54();
	}

	private void SubGraph_SaveLoadBool_Save_Out_67(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_67;
		Relay_Save_Out_67();
	}

	private void SubGraph_SaveLoadBool_Load_Out_67(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_67;
		Relay_Load_Out_67();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_67(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_67;
		Relay_Restart_Out_67();
	}

	private void SubGraph_SaveLoadBool_Save_Out_83(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = e.boolean;
		local_msgTradingStationFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_83;
		Relay_Save_Out_83();
	}

	private void SubGraph_SaveLoadBool_Load_Out_83(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = e.boolean;
		local_msgTradingStationFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_83;
		Relay_Load_Out_83();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_83(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = e.boolean;
		local_msgTradingStationFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_83;
		Relay_Restart_Out_83();
	}

	private void SubGraph_SaveLoadBool_Save_Out_85(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = e.boolean;
		local_TrollDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_85;
		Relay_Save_Out_85();
	}

	private void SubGraph_SaveLoadBool_Load_Out_85(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = e.boolean;
		local_TrollDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_85;
		Relay_Load_Out_85();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_85(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = e.boolean;
		local_TrollDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_85;
		Relay_Restart_Out_85();
	}

	private void SubGraph_CompleteObjectiveStage_Out_100(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_100 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_100;
		Relay_Out_100();
	}

	private void SubGraph_SaveLoadBool_Save_Out_110(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = e.boolean;
		local_InitialBatteryCharge_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_110;
		Relay_Save_Out_110();
	}

	private void SubGraph_SaveLoadBool_Load_Out_110(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = e.boolean;
		local_InitialBatteryCharge_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_110;
		Relay_Load_Out_110();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_110(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = e.boolean;
		local_InitialBatteryCharge_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_110;
		Relay_Restart_Out_110();
	}

	private void Relay_In_0()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_0 = local_GSOVendor_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_0.In(logic_uScript_IsPlayerInRangeOfTech_tech_0, logic_uScript_IsPlayerInRangeOfTech_range_0, logic_uScript_IsPlayerInRangeOfTech_techs_0);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_0.InRange)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_SetEncounterTarget_owner_4 = owner_Connection_3;
		logic_uScript_SetEncounterTarget_visibleObject_4 = local_VendorBoss_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_4.In(logic_uScript_SetEncounterTarget_owner_4, logic_uScript_SetEncounterTarget_visibleObject_4);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_4.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_In_5()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_5 = local_VendorBoss_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_5.In(logic_uScript_IsPlayerInRangeOfTech_tech_5, logic_uScript_IsPlayerInRangeOfTech_range_5, logic_uScript_IsPlayerInRangeOfTech_techs_5);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_5.InRange)
		{
			Relay_In_4();
		}
	}

	private void Relay_OnUpdate_8()
	{
		Relay_In_10();
	}

	private void Relay_OnSuspend_8()
	{
	}

	private void Relay_OnResume_8()
	{
	}

	private void Relay_True_9()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_9.True(out logic_uScriptAct_SetBool_Target_9);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_9;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_9.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_False_9()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_9.False(out logic_uScriptAct_SetBool_Target_9);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_9;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_9.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_In_10()
	{
		logic_uScriptCon_CompareBool_Bool_10 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.In(logic_uScriptCon_CompareBool_Bool_10);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.False;
		if (num)
		{
			Relay_In_20();
		}
		if (flag)
		{
			Relay_True_9();
		}
	}

	private void Relay_AtIndex_12()
	{
		int num = 0;
		Array array = local_BossList_TankArray;
		if (logic_uScript_AccessListTech_techList_12.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_12, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_12, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_12.AtIndex(ref logic_uScript_AccessListTech_techList_12, logic_uScript_AccessListTech_index_12, out logic_uScript_AccessListTech_value_12);
		local_BossList_TankArray = logic_uScript_AccessListTech_techList_12;
		local_VendorBoss_Tank = logic_uScript_AccessListTech_value_12;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_12.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_16()
	{
		logic_uScript_AI_SetPOI_tank_16 = local_VendorBoss_Tank;
		logic_uScript_AI_SetPOI_position_16 = local_InitialPos_UnityEngine_Vector3;
		logic_uScript_AI_SetPOI_uScript_AI_SetPOI_16.In(logic_uScript_AI_SetPOI_tank_16, logic_uScript_AI_SetPOI_usePOI_16, logic_uScript_AI_SetPOI_position_16, logic_uScript_AI_SetPOI_distance_16);
		if (logic_uScript_AI_SetPOI_uScript_AI_SetPOI_16.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_20 = owner_Connection_22;
		logic_uScript_GetPositionInEncounter_Return_20 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_20.In(logic_uScript_GetPositionInEncounter_ownerNode_20, logic_uScript_GetPositionInEncounter_posName_20);
		local_InitialPos_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_20;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_20.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_In_25()
	{
		logic_uScript_SetEncounterTarget_owner_25 = owner_Connection_24;
		logic_uScript_SetEncounterTarget_visibleObject_25 = local_GSOVendor_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_25.In(logic_uScript_SetEncounterTarget_owner_25, logic_uScript_SetEncounterTarget_visibleObject_25);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_25.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_SaveEvent_27()
	{
		Relay_Save_67();
	}

	private void Relay_LoadEvent_27()
	{
		Relay_Load_67();
	}

	private void Relay_RestartEvent_27()
	{
		Relay_Set_False_67();
	}

	private void Relay_Save_Out_29()
	{
		Relay_Save_110();
	}

	private void Relay_Load_Out_29()
	{
		Relay_Load_110();
	}

	private void Relay_Restart_Out_29()
	{
		Relay_Set_False_110();
	}

	private void Relay_Save_29()
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_29 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Save(ref logic_SubGraph_SaveLoadBool_boolean_29, logic_SubGraph_SaveLoadBool_boolAsVariable_29, logic_SubGraph_SaveLoadBool_uniqueID_29);
	}

	private void Relay_Load_29()
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_29 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Load(ref logic_SubGraph_SaveLoadBool_boolean_29, logic_SubGraph_SaveLoadBool_boolAsVariable_29, logic_SubGraph_SaveLoadBool_uniqueID_29);
	}

	private void Relay_Set_True_29()
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_29 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_29, logic_SubGraph_SaveLoadBool_boolAsVariable_29, logic_SubGraph_SaveLoadBool_uniqueID_29);
	}

	private void Relay_Set_False_29()
	{
		logic_SubGraph_SaveLoadBool_boolean_29 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_29 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_29.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_29, logic_SubGraph_SaveLoadBool_boolAsVariable_29, logic_SubGraph_SaveLoadBool_uniqueID_29);
	}

	private void Relay_In_30()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_30 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_30.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_30, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_30);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_30.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_32()
	{
		logic_uScript_GetNearestVendorPos_Return_32 = logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_32.In();
		local_VendorPos_UnityEngine_Vector3 = logic_uScript_GetNearestVendorPos_Return_32;
		if (logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_32.Found)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_35()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_35.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_Succeed_37()
	{
		logic_uScript_FinishEncounter_owner_37 = owner_Connection_14;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_37.Succeed(logic_uScript_FinishEncounter_owner_37);
		if (logic_uScript_FinishEncounter_uScript_FinishEncounter_37.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_Fail_37()
	{
		logic_uScript_FinishEncounter_owner_37 = owner_Connection_14;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_37.Fail(logic_uScript_FinishEncounter_owner_37);
		if (logic_uScript_FinishEncounter_uScript_FinishEncounter_37.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_Output1_38()
	{
		Relay_In_25();
	}

	private void Relay_Output2_38()
	{
		Relay_In_5();
	}

	private void Relay_Output3_38()
	{
		Relay_In_42();
	}

	private void Relay_Output4_38()
	{
	}

	private void Relay_Output5_38()
	{
	}

	private void Relay_Output6_38()
	{
	}

	private void Relay_Output7_38()
	{
	}

	private void Relay_Output8_38()
	{
	}

	private void Relay_In_38()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_38 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_38.In(logic_uScriptCon_ManualSwitch_CurrentOutput_38);
	}

	private void Relay_True_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.True(out logic_uScriptAct_SetBool_Target_40);
		local_TrollDead_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_False_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.False(out logic_uScriptAct_SetBool_Target_40);
		local_TrollDead_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_In_42()
	{
		logic_uScriptCon_CompareBool_Bool_42 = local_TrollDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.In(logic_uScriptCon_CompareBool_Bool_42);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.False;
		if (num)
		{
			Relay_In_122();
		}
		if (flag)
		{
			Relay_In_90();
		}
	}

	private void Relay_Out_44()
	{
	}

	private void Relay_In_44()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_44 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_44.In(logic_SubGraph_LoadObjectiveStates_currentObjective_44);
	}

	private void Relay_Save_Out_47()
	{
	}

	private void Relay_Load_Out_47()
	{
		Relay_In_44();
	}

	private void Relay_Restart_Out_47()
	{
		Relay_Set_False_85();
	}

	private void Relay_Save_47()
	{
		logic_SubGraph_SaveLoadInt_integer_47 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_47 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.Save(logic_SubGraph_SaveLoadInt_restartValue_47, ref logic_SubGraph_SaveLoadInt_integer_47, logic_SubGraph_SaveLoadInt_intAsVariable_47, logic_SubGraph_SaveLoadInt_uniqueID_47);
	}

	private void Relay_Load_47()
	{
		logic_SubGraph_SaveLoadInt_integer_47 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_47 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.Load(logic_SubGraph_SaveLoadInt_restartValue_47, ref logic_SubGraph_SaveLoadInt_integer_47, logic_SubGraph_SaveLoadInt_intAsVariable_47, logic_SubGraph_SaveLoadInt_uniqueID_47);
	}

	private void Relay_Restart_47()
	{
		logic_SubGraph_SaveLoadInt_integer_47 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_47 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_47.Restart(logic_SubGraph_SaveLoadInt_restartValue_47, ref logic_SubGraph_SaveLoadInt_integer_47, logic_SubGraph_SaveLoadInt_intAsVariable_47, logic_SubGraph_SaveLoadInt_uniqueID_47);
	}

	private void Relay_In_48()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_48.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_Out_54()
	{
	}

	private void Relay_In_54()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_54 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_54.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_54, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_54);
	}

	private void Relay_In_57()
	{
		logic_uScript_AddMessage_messageData_57 = msg01FindTradingStation;
		logic_uScript_AddMessage_speaker_57 = messageSpeaker;
		logic_uScript_AddMessage_Return_57 = logic_uScript_AddMessage_uScript_AddMessage_57.In(logic_uScript_AddMessage_messageData_57, logic_uScript_AddMessage_speaker_57);
		if (logic_uScript_AddMessage_uScript_AddMessage_57.Out)
		{
			Relay_SetVisible_65();
		}
	}

	private void Relay_In_58()
	{
		logic_uScript_AddMessage_messageData_58 = msg02TradingStationFound;
		logic_uScript_AddMessage_speaker_58 = messageSpeaker;
		logic_uScript_AddMessage_Return_58 = logic_uScript_AddMessage_uScript_AddMessage_58.In(logic_uScript_AddMessage_messageData_58, logic_uScript_AddMessage_speaker_58);
		if (logic_uScript_AddMessage_uScript_AddMessage_58.Shown)
		{
			Relay_True_80();
		}
	}

	private void Relay_In_61()
	{
		logic_uScript_AddMessage_messageData_61 = msg04DefeatTroll;
		logic_uScript_AddMessage_speaker_61 = messageSpeaker;
		logic_uScript_AddMessage_Return_61 = logic_uScript_AddMessage_uScript_AddMessage_61.In(logic_uScript_AddMessage_messageData_61, logic_uScript_AddMessage_speaker_61);
		local_Msg04DefeatTroll_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_61;
		if (logic_uScript_AddMessage_uScript_AddMessage_61.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_SetVisible_65()
	{
		logic_uScript_SetVendorsVisibleOnRadar_uScript_SetVendorsVisibleOnRadar_65.SetVisible();
		if (logic_uScript_SetVendorsVisibleOnRadar_uScript_SetVendorsVisibleOnRadar_65.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_SetInvisible_65()
	{
		logic_uScript_SetVendorsVisibleOnRadar_uScript_SetVendorsVisibleOnRadar_65.SetInvisible();
		if (logic_uScript_SetVendorsVisibleOnRadar_uScript_SetVendorsVisibleOnRadar_65.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_Save_Out_67()
	{
		Relay_Save_29();
	}

	private void Relay_Load_Out_67()
	{
		Relay_Load_29();
	}

	private void Relay_Restart_Out_67()
	{
		Relay_Set_False_29();
	}

	private void Relay_Save_67()
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_67 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Save(ref logic_SubGraph_SaveLoadBool_boolean_67, logic_SubGraph_SaveLoadBool_boolAsVariable_67, logic_SubGraph_SaveLoadBool_uniqueID_67);
	}

	private void Relay_Load_67()
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_67 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Load(ref logic_SubGraph_SaveLoadBool_boolean_67, logic_SubGraph_SaveLoadBool_boolAsVariable_67, logic_SubGraph_SaveLoadBool_uniqueID_67);
	}

	private void Relay_Set_True_67()
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_67 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_67, logic_SubGraph_SaveLoadBool_boolAsVariable_67, logic_SubGraph_SaveLoadBool_uniqueID_67);
	}

	private void Relay_Set_False_67()
	{
		logic_SubGraph_SaveLoadBool_boolean_67 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_67 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_67.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_67, logic_SubGraph_SaveLoadBool_boolAsVariable_67, logic_SubGraph_SaveLoadBool_uniqueID_67);
	}

	private void Relay_In_68()
	{
		logic_uScriptCon_CompareInt_A_68 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_68.In(logic_uScriptCon_CompareInt_A_68, logic_uScriptCon_CompareInt_B_68);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_68.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_68.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_101();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_71()
	{
		logic_uScript_SetEncounterPosition_ownerNode_71 = owner_Connection_72;
		logic_uScript_SetEncounterPosition_position_71 = local_VendorPos_UnityEngine_Vector3;
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_71.In(logic_uScript_SetEncounterPosition_ownerNode_71, logic_uScript_SetEncounterPosition_position_71);
		if (logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_71.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_In_73()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_73.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_73.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_In_74()
	{
		logic_uScript_FindNearestVendor_Return_74 = logic_uScript_FindNearestVendor_uScript_FindNearestVendor_74.In();
		local_GSOVendor_Tank = logic_uScript_FindNearestVendor_Return_74;
		if (logic_uScript_FindNearestVendor_uScript_FindNearestVendor_74.Returned)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_75()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_75 = owner_Connection_76;
		logic_uScript_MoveEncounterWithVisible_visibleObject_75 = local_VendorBoss_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_75.In(logic_uScript_MoveEncounterWithVisible_ownerNode_75, logic_uScript_MoveEncounterWithVisible_visibleObject_75);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_75.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_79()
	{
		logic_uScriptCon_CompareBool_Bool_79 = local_msgTradingStationFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_79.In(logic_uScriptCon_CompareBool_Bool_79);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_79.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_79.False;
		if (num)
		{
			Relay_In_97();
		}
		if (flag)
		{
			Relay_In_58();
		}
	}

	private void Relay_True_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.True(out logic_uScriptAct_SetBool_Target_80);
		local_msgTradingStationFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_80;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_80.Out)
		{
			Relay_In_97();
		}
	}

	private void Relay_False_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.False(out logic_uScriptAct_SetBool_Target_80);
		local_msgTradingStationFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_80;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_80.Out)
		{
			Relay_In_97();
		}
	}

	private void Relay_Save_Out_83()
	{
		Relay_Save_47();
	}

	private void Relay_Load_Out_83()
	{
		Relay_Load_47();
	}

	private void Relay_Restart_Out_83()
	{
		Relay_Restart_47();
	}

	private void Relay_Save_83()
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = local_msgTradingStationFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_83 = local_msgTradingStationFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Save(ref logic_SubGraph_SaveLoadBool_boolean_83, logic_SubGraph_SaveLoadBool_boolAsVariable_83, logic_SubGraph_SaveLoadBool_uniqueID_83);
	}

	private void Relay_Load_83()
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = local_msgTradingStationFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_83 = local_msgTradingStationFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Load(ref logic_SubGraph_SaveLoadBool_boolean_83, logic_SubGraph_SaveLoadBool_boolAsVariable_83, logic_SubGraph_SaveLoadBool_uniqueID_83);
	}

	private void Relay_Set_True_83()
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = local_msgTradingStationFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_83 = local_msgTradingStationFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_83, logic_SubGraph_SaveLoadBool_boolAsVariable_83, logic_SubGraph_SaveLoadBool_uniqueID_83);
	}

	private void Relay_Set_False_83()
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = local_msgTradingStationFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_83 = local_msgTradingStationFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_83, logic_SubGraph_SaveLoadBool_boolAsVariable_83, logic_SubGraph_SaveLoadBool_uniqueID_83);
	}

	private void Relay_Save_Out_85()
	{
	}

	private void Relay_Load_Out_85()
	{
	}

	private void Relay_Restart_Out_85()
	{
	}

	private void Relay_Save_85()
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = local_TrollDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_85 = local_TrollDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Save(ref logic_SubGraph_SaveLoadBool_boolean_85, logic_SubGraph_SaveLoadBool_boolAsVariable_85, logic_SubGraph_SaveLoadBool_uniqueID_85);
	}

	private void Relay_Load_85()
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = local_TrollDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_85 = local_TrollDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Load(ref logic_SubGraph_SaveLoadBool_boolean_85, logic_SubGraph_SaveLoadBool_boolAsVariable_85, logic_SubGraph_SaveLoadBool_uniqueID_85);
	}

	private void Relay_Set_True_85()
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = local_TrollDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_85 = local_TrollDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_85, logic_SubGraph_SaveLoadBool_boolAsVariable_85, logic_SubGraph_SaveLoadBool_uniqueID_85);
	}

	private void Relay_Set_False_85()
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = local_TrollDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_85 = local_TrollDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_85, logic_SubGraph_SaveLoadBool_boolAsVariable_85, logic_SubGraph_SaveLoadBool_uniqueID_85);
	}

	private void Relay_In_87()
	{
		logic_uScript_ShowHint_hintId_87 = local_89_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_87.In(logic_uScript_ShowHint_hintId_87);
		if (logic_uScript_ShowHint_uScript_ShowHint_87.Out)
		{
			Relay_In_92();
		}
	}

	private void Relay_In_88()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_88 = local_89_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_88.In(logic_uScript_HasHintBeenShownBefore_hintID_88);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_88.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_88.NotShown;
		if (shown)
		{
			Relay_In_92();
		}
		if (notShown)
		{
			Relay_In_87();
		}
	}

	private void Relay_In_90()
	{
		logic_uScript_GetPlayerTank_Return_90 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_90.In();
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_90.NotReturned)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_92()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_92 = local_93_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_92.In(logic_uScript_HasHintBeenShownBefore_hintID_92);
		if (logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_92.NotShown)
		{
			Relay_In_94();
		}
	}

	private void Relay_In_94()
	{
		logic_uScript_ShowHint_hintId_94 = local_93_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_94.In(logic_uScript_ShowHint_hintId_94);
	}

	private void Relay_In_95()
	{
		logic_uScript_RemoveSceneryAtPosition_position_95 = local_InitialPos_UnityEngine_Vector3;
		logic_uScript_RemoveSceneryAtPosition_radius_95 = clearSceneryRadius;
		logic_uScript_RemoveSceneryAtPosition_uScript_RemoveSceneryAtPosition_95.In(logic_uScript_RemoveSceneryAtPosition_position_95, logic_uScript_RemoveSceneryAtPosition_radius_95, logic_uScript_RemoveSceneryAtPosition_preventChunksSpawning_95);
		if (logic_uScript_RemoveSceneryAtPosition_uScript_RemoveSceneryAtPosition_95.Out)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_97()
	{
		logic_uScriptCon_CompareBool_Bool_97 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97.In(logic_uScriptCon_CompareBool_Bool_97);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97.False;
		if (num)
		{
			Relay_In_100();
		}
		if (flag)
		{
			Relay_InitialSpawn_138();
		}
	}

	private void Relay_Out_100()
	{
	}

	private void Relay_In_100()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_100 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_100.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_100, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_100);
	}

	private void Relay_In_101()
	{
		logic_uScriptCon_CompareBool_Bool_101 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.In(logic_uScriptCon_CompareBool_Bool_101);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.False;
		if (num)
		{
			Relay_In_133();
		}
		if (flag)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_103()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_103.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_103.Out)
		{
			Relay_True_40();
		}
	}

	private void Relay_In_104()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104.Out)
		{
			Relay_AtIndex_12();
		}
	}

	private void Relay_In_107()
	{
		logic_uScriptCon_CompareBool_Bool_107 = local_InitialBatteryCharge_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107.In(logic_uScriptCon_CompareBool_Bool_107);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107.False;
		if (num)
		{
			Relay_In_113();
		}
		if (flag)
		{
			Relay_True_108();
		}
	}

	private void Relay_True_108()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.True(out logic_uScriptAct_SetBool_Target_108);
		local_InitialBatteryCharge_System_Boolean = logic_uScriptAct_SetBool_Target_108;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_108.Out)
		{
			Relay_In_127();
		}
	}

	private void Relay_False_108()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.False(out logic_uScriptAct_SetBool_Target_108);
		local_InitialBatteryCharge_System_Boolean = logic_uScriptAct_SetBool_Target_108;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_108.Out)
		{
			Relay_In_127();
		}
	}

	private void Relay_Save_Out_110()
	{
		Relay_Save_83();
	}

	private void Relay_Load_Out_110()
	{
		Relay_Load_83();
	}

	private void Relay_Restart_Out_110()
	{
		Relay_Set_False_83();
	}

	private void Relay_Save_110()
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = local_InitialBatteryCharge_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_110 = local_InitialBatteryCharge_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Save(ref logic_SubGraph_SaveLoadBool_boolean_110, logic_SubGraph_SaveLoadBool_boolAsVariable_110, logic_SubGraph_SaveLoadBool_uniqueID_110);
	}

	private void Relay_Load_110()
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = local_InitialBatteryCharge_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_110 = local_InitialBatteryCharge_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Load(ref logic_SubGraph_SaveLoadBool_boolean_110, logic_SubGraph_SaveLoadBool_boolAsVariable_110, logic_SubGraph_SaveLoadBool_uniqueID_110);
	}

	private void Relay_Set_True_110()
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = local_InitialBatteryCharge_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_110 = local_InitialBatteryCharge_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_110, logic_SubGraph_SaveLoadBool_boolAsVariable_110, logic_SubGraph_SaveLoadBool_uniqueID_110);
	}

	private void Relay_Set_False_110()
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = local_InitialBatteryCharge_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_110 = local_InitialBatteryCharge_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_110, logic_SubGraph_SaveLoadBool_boolAsVariable_110, logic_SubGraph_SaveLoadBool_uniqueID_110);
	}

	private void Relay_True_112()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.True(out logic_uScriptAct_SetBool_Target_112);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_112;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_112.Out)
		{
			Relay_In_100();
		}
	}

	private void Relay_False_112()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.False(out logic_uScriptAct_SetBool_Target_112);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_112;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_112.Out)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_113()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_113.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_113.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_114()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_114.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_114.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_116()
	{
		logic_uScript_CompleteAchievement_uScript_CompleteAchievement_116.In(logic_uScript_CompleteAchievement_achievementID_116);
	}

	private void Relay_In_117()
	{
		logic_uScript_GetModeRunningTime_Return_117 = logic_uScript_GetModeRunningTime_uScript_GetModeRunningTime_117.In();
		local_118_System_Single = logic_uScript_GetModeRunningTime_Return_117;
		if (logic_uScript_GetModeRunningTime_uScript_GetModeRunningTime_117.Out)
		{
			Relay_In_119();
		}
	}

	private void Relay_In_119()
	{
		logic_uScriptCon_CompareFloat_A_119 = local_118_System_Single;
		logic_uScriptCon_CompareFloat_B_119 = TrollAchievementTimeSeconds;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_119.In(logic_uScriptCon_CompareFloat_A_119, logic_uScriptCon_CompareFloat_B_119);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_119.LessThanOrEqualTo)
		{
			Relay_In_116();
		}
	}

	private void Relay_In_122()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_122 = local_Msg04DefeatTroll_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_122.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_122, logic_uScript_RemoveOnScreenMessage_instant_122);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_122.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_In_126()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_126.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_126, logic_uScript_SendAnaliticsEvent_parameterName_126, logic_uScript_SendAnaliticsEvent_parameter_126);
		if (logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_126.Out)
		{
			Relay_Succeed_37();
		}
	}

	private void Relay_In_127()
	{
		logic_uScript_SetBatteryChargeAmount_tech_127 = local_VendorBoss_Tank;
		logic_uScript_SetBatteryChargeAmount_chargeAmount_127 = bossBatteryCharge;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_127.In(logic_uScript_SetBatteryChargeAmount_tech_127, logic_uScript_SetBatteryChargeAmount_chargeAmount_127);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_127.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_133()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_133.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_133, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_133[num++] = SpawnDataBoss;
		logic_uScript_GetAndCheckTechs_ownerNode_133 = owner_Connection_131;
		int num2 = 0;
		Array array = local_BossList_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_133.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_133, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_133, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_133 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_133.In(logic_uScript_GetAndCheckTechs_techData_133, logic_uScript_GetAndCheckTechs_ownerNode_133, ref logic_uScript_GetAndCheckTechs_techs_133);
		local_BossList_TankArray = logic_uScript_GetAndCheckTechs_techs_133;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_133.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_133.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_133.AllDead;
		if (allAlive)
		{
			Relay_In_104();
		}
		if (someAlive)
		{
			Relay_In_104();
		}
		if (allDead)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_136()
	{
		logic_uScript_AddMessage_messageData_136 = msg03TrollIncoming;
		logic_uScript_AddMessage_speaker_136 = messageSpeaker;
		logic_uScript_AddMessage_Return_136 = logic_uScript_AddMessage_uScript_AddMessage_136.In(logic_uScript_AddMessage_messageData_136, logic_uScript_AddMessage_speaker_136);
		if (logic_uScript_AddMessage_uScript_AddMessage_136.Out)
		{
			Relay_True_112();
		}
	}

	private void Relay_InitialSpawn_138()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_138.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_138, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_138[num++] = SpawnDataBoss;
		logic_uScript_SpawnTechsFromData_ownerNode_138 = owner_Connection_139;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_138.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_138, logic_uScript_SpawnTechsFromData_ownerNode_138, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_138, logic_uScript_SpawnTechsFromData_allowResurrection_138);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_138.Out)
		{
			Relay_In_136();
		}
	}

	private void Relay_In_140()
	{
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_140.In(logic_uScript_SetVendorsEnabled_enableShop_140, logic_uScript_SetVendorsEnabled_enableMissionBoard_140, logic_uScript_SetVendorsEnabled_enableSelling_140, logic_uScript_SetVendorsEnabled_enableSCU_140, logic_uScript_SetVendorsEnabled_enableCharging_140);
		if (logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_140.Out)
		{
			Relay_In_0();
		}
	}
}
