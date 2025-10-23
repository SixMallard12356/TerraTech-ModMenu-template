using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("craterPrefab2", "")]
public class Mission_GSO_1_5_DeliveryCrate : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public float crateOpeningTime;

	[Multiline(3)]
	public string cratePositionName = "";

	[Multiline(3)]
	public string craterPositionName = "";

	public Transform craterPrefab;

	private GameHints.HintID local_117_GameHints_HintID = GameHints.HintID.Minimap;

	private GameHints.HintID local_119_GameHints_HintID = GameHints.HintID.Radar;

	private GameHints.HintID local_123_GameHints_HintID = GameHints.HintID.BatteryPower;

	private GameHints.HintID local_125_GameHints_HintID = GameHints.HintID.BatteryCharge;

	private GameHints.HintID local_126_GameHints_HintID = GameHints.HintID.PowerStorage;

	private GameHints.HintID local_135_GameHints_HintID = GameHints.HintID.BlockLimit2;

	private GameHints.HintID local_152_GameHints_HintID = GameHints.HintID.BlockLimit;

	private Vector3 local_39_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_BlockLimitTutorial_System_Boolean;

	private Crate local_Crate_Crate;

	private float local_CrateOpenRange_System_Single;

	private bool local_CrateSpawned_System_Boolean;

	private bool local_CrateUnlocked_System_Boolean;

	private Vector3 local_EncounterPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_Init_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_msg01GoToLocation_ManOnScreenMessages_OnScreenMessage;

	private bool local_MsgArrivedAtLocation_System_Boolean;

	private bool local_MsgCrateLanding_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private Vector3 local_VendorPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01GoToLocation;

	public uScript_AddMessage.MessageData msg02ArrivedAtLocation;

	public uScript_AddMessage.MessageData msg03CrateLanding;

	public uScript_AddMessage.MessageData msg04CrateLanded;

	public uScript_AddMessage.MessageData msg05CrateUnlocked;

	public uScript_AddMessage.MessageData msg06LeaveArea;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_21;

	private GameObject owner_Connection_29;

	private GameObject owner_Connection_31;

	private GameObject owner_Connection_34;

	private GameObject owner_Connection_45;

	private GameObject owner_Connection_87;

	private GameObject owner_Connection_91;

	private GameObject owner_Connection_96;

	private GameObject owner_Connection_98;

	private GameObject owner_Connection_108;

	private GameObject owner_Connection_112;

	private uScript_SpawnDeliveryCrate logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_2 = new uScript_SpawnDeliveryCrate();

	private string logic_uScript_SpawnDeliveryCrate_positionName_2 = "";

	private GameObject logic_uScript_SpawnDeliveryCrate_ownerNode_2;

	private bool logic_uScript_SpawnDeliveryCrate_visibleOnRadar_2 = true;

	private bool logic_uScript_SpawnDeliveryCrate_Out_2 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_4;

	private bool logic_uScriptCon_CompareBool_True_4 = true;

	private bool logic_uScriptCon_CompareBool_False_4 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_6 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_6;

	private bool logic_uScriptAct_SetBool_Out_6 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_6 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_6 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_9;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_9 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_9 = "CrateSpawned";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_14 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_14;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_14;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_14;

	private bool logic_uScript_AddMessage_Out_14 = true;

	private bool logic_uScript_AddMessage_Shown_14 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_15 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_15;

	private bool logic_uScriptAct_SetBool_Out_15 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_15 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_15 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_17;

	private bool logic_uScriptCon_CompareBool_True_17 = true;

	private bool logic_uScriptCon_CompareBool_False_17 = true;

	private uScript_SetEncounterPosition logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_18 = new uScript_SetEncounterPosition();

	private GameObject logic_uScript_SetEncounterPosition_ownerNode_18;

	private Vector3 logic_uScript_SetEncounterPosition_position_18;

	private bool logic_uScript_SetEncounterPosition_Out_18 = true;

	private uScript_GetCurrentEncounterPosition logic_uScript_GetCurrentEncounterPosition_uScript_GetCurrentEncounterPosition_20 = new uScript_GetCurrentEncounterPosition();

	private GameObject logic_uScript_GetCurrentEncounterPosition_ownerNode_20;

	private Vector3 logic_uScript_GetCurrentEncounterPosition_Return_20;

	private bool logic_uScript_GetCurrentEncounterPosition_Out_20 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_24;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_24 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_24 = "Init";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_26 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_26;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_26;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_26;

	private bool logic_uScript_AddMessage_Out_26 = true;

	private bool logic_uScript_AddMessage_Shown_26 = true;

	private uScript_CheckDeliveryCrateSpawned logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_28 = new uScript_CheckDeliveryCrateSpawned();

	private GameObject logic_uScript_CheckDeliveryCrateSpawned_ownerNode_28;

	private bool logic_uScript_CheckDeliveryCrateSpawned_Out_28 = true;

	private bool logic_uScript_CheckDeliveryCrateSpawned_Yes_28 = true;

	private bool logic_uScript_CheckDeliveryCrateSpawned_No_28 = true;

	private uScript_UnlockDeliveryCrate logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_30 = new uScript_UnlockDeliveryCrate();

	private GameObject logic_uScript_UnlockDeliveryCrate_ownerNode_30;

	private bool logic_uScript_UnlockDeliveryCrate_Out_30 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_32 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_32;

	private string logic_uScript_RemoveScenery_positionName_32 = "";

	private float logic_uScript_RemoveScenery_radius_32 = 25f;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_32 = true;

	private bool logic_uScript_RemoveScenery_Out_32 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_36 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_36;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_36;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_36;

	private bool logic_uScript_AddMessage_Out_36 = true;

	private bool logic_uScript_AddMessage_Shown_36 = true;

	private uScript_GetStartPositionAsVector3 logic_uScript_GetStartPositionAsVector3_uScript_GetStartPositionAsVector3_40 = new uScript_GetStartPositionAsVector3();

	private Vector3 logic_uScript_GetStartPositionAsVector3_Return_40;

	private bool logic_uScript_GetStartPositionAsVector3_Out_40 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_41;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_43;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_43;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_46 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_46;

	private bool logic_uScript_FinishEncounter_Out_46 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_48 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_48;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_48;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_53 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_53;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_53;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_53;

	private bool logic_uScript_AddMessage_Out_53 = true;

	private bool logic_uScript_AddMessage_Shown_53 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_56 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_56 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_56 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_56 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_58 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_58;

	private bool logic_uScript_Wait_repeat_58;

	private bool logic_uScript_Wait_Waited_58 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_60;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_60 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_60 = "MsgCrateLanding";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_62 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_62;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_62 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_62 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_64 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_64;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_66 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_66;

	private bool logic_uScriptAct_SetBool_Out_66 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_66 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_66 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_68;

	private bool logic_uScriptCon_CompareBool_True_68 = true;

	private bool logic_uScriptCon_CompareBool_False_68 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_70 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_70 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_70 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_70 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_72;

	private bool logic_uScriptCon_CompareBool_True_72 = true;

	private bool logic_uScriptCon_CompareBool_False_72 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_73 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_73;

	private bool logic_uScriptAct_SetBool_Out_73 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_73 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_73 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_76 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_76;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_76;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_76;

	private bool logic_uScript_AddMessage_Out_76 = true;

	private bool logic_uScript_AddMessage_Shown_76 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_81 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_81;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_81;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_81;

	private bool logic_uScript_AddMessage_Out_81 = true;

	private bool logic_uScript_AddMessage_Shown_81 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_82;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_82 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_82 = "CrateUnlocked";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_84;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_84 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_84 = "MsgArrivedAtLocation";

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_86 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_86;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_86 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_86 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_86 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_88 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_88 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_88 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_88 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_90 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_90;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_90 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_90 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_90 = true;

	private uScript_SetEncounterPosition logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_92 = new uScript_SetEncounterPosition();

	private GameObject logic_uScript_SetEncounterPosition_ownerNode_92;

	private Vector3 logic_uScript_SetEncounterPosition_position_92;

	private bool logic_uScript_SetEncounterPosition_Out_92 = true;

	private uScript_GetNearestVendorPos logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_93 = new uScript_GetNearestVendorPos();

	private Vector3 logic_uScript_GetNearestVendorPos_Return_93;

	private bool logic_uScript_GetNearestVendorPos_Out_93 = true;

	private bool logic_uScript_GetNearestVendorPos_Found_93 = true;

	private bool logic_uScript_GetNearestVendorPos_Missing_93 = true;

	private uScript_GetDeliveryCrate logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_99 = new uScript_GetDeliveryCrate();

	private GameObject logic_uScript_GetDeliveryCrate_ownerNode_99;

	private Crate logic_uScript_GetDeliveryCrate_Return_99;

	private bool logic_uScript_GetDeliveryCrate_Out_99 = true;

	private bool logic_uScript_GetDeliveryCrate_Success_99 = true;

	private bool logic_uScript_GetDeliveryCrate_Failure_99 = true;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_100 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_100 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_100;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_100 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_100 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_100 = true;

	private uScript_GetCrateOpenTriggerRange logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_101 = new uScript_GetCrateOpenTriggerRange();

	private Crate logic_uScript_GetCrateOpenTriggerRange_crate_101;

	private float logic_uScript_GetCrateOpenTriggerRange_Return_101;

	private bool logic_uScript_GetCrateOpenTriggerRange_Out_101 = true;

	private uScript_SpawnEncounterObject logic_uScript_SpawnEncounterObject_uScript_SpawnEncounterObject_107 = new uScript_SpawnEncounterObject();

	private GameObject logic_uScript_SpawnEncounterObject_ownerNode_107;

	private Transform logic_uScript_SpawnEncounterObject_encounterObjectToSpawn_107;

	private string logic_uScript_SpawnEncounterObject_nameWithinEncounter_107 = "crater";

	private string logic_uScript_SpawnEncounterObject_positionName_107 = "";

	private bool logic_uScript_SpawnEncounterObject_Out_107 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_111 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_111;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_111 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_111 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_111 = true;

	private uScript_IsOnScreenMessage logic_uScript_IsOnScreenMessage_uScript_IsOnScreenMessage_113 = new uScript_IsOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_IsOnScreenMessage_onScreenMessage_113;

	private bool logic_uScript_IsOnScreenMessage_Out_113 = true;

	private bool logic_uScript_IsOnScreenMessage_True_113 = true;

	private bool logic_uScript_IsOnScreenMessage_False_113 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_114 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_114;

	private bool logic_uScript_ShowHint_Out_114 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_116 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_116;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_116 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_116 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_118 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_118;

	private bool logic_uScript_ShowHint_Out_118 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_120 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_120;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_120 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_120 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_121 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_121;

	private bool logic_uScript_ShowHint_Out_121 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_122 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_122;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_122 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_122 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_124 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_124;

	private bool logic_uScript_ShowHint_Out_124 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_127 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_127;

	private bool logic_uScript_ShowHint_Out_127 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_128 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_128;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_128 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_128 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_130 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_130;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_130 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_130 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_134 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_134;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_134 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_134 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_136 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_136;

	private bool logic_uScript_ShowHint_Out_136 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_138 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_138;

	private bool logic_uScriptAct_SetBool_Out_138 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_138 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_138 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_139 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_139;

	private bool logic_uScriptCon_CompareBool_True_139 = true;

	private bool logic_uScriptCon_CompareBool_False_139 = true;

	private uScript_HideHint logic_uScript_HideHint_uScript_HideHint_141 = new uScript_HideHint();

	private GameHints.HintID logic_uScript_HideHint_hintId_141 = GameHints.HintID.BlockLimit;

	private bool logic_uScript_HideHint_Out_141 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_142;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_142 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_142 = "BlockLimitTutorial";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_145 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_145 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_146 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_146 = true;

	private uScript_IsBlockLimitEnabled logic_uScript_IsBlockLimitEnabled_uScript_IsBlockLimitEnabled_148 = new uScript_IsBlockLimitEnabled();

	private bool logic_uScript_IsBlockLimitEnabled_True_148 = true;

	private bool logic_uScript_IsBlockLimitEnabled_False_148 = true;

	private uScript_SetBlockLimitUIState logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_149 = new uScript_SetBlockLimitUIState();

	private UIBlockLimit.ShowReason logic_uScript_SetBlockLimitUIState_showReason_149 = UIBlockLimit.ShowReason.Tutorial;

	private bool logic_uScript_SetBlockLimitUIState_Out_149 = true;

	private uScript_SetBlockLimitUIState logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_150 = new uScript_SetBlockLimitUIState();

	private UIBlockLimit.ShowReason logic_uScript_SetBlockLimitUIState_showReason_150 = UIBlockLimit.ShowReason.Tutorial;

	private bool logic_uScript_SetBlockLimitUIState_Out_150 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_151 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_151 = "tutorial_stage";

	private string logic_uScript_SendAnaliticsEvent_parameterName_151 = "stage_complete";

	private object logic_uScript_SendAnaliticsEvent_parameter_151 = "delivery_crate";

	private bool logic_uScript_SendAnaliticsEvent_Out_151 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_153 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_153;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_153 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_153 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_154 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_154;

	private bool logic_uScript_ShowHint_Out_154 = true;

	private uScript_HideHint logic_uScript_HideHint_uScript_HideHint_155 = new uScript_HideHint();

	private GameHints.HintID logic_uScript_HideHint_hintId_155 = GameHints.HintID.BlockLimit2;

	private bool logic_uScript_HideHint_Out_155 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_1.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
				}
			}
		}
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
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
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_8;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_8;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_8;
				}
			}
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
		}
		if (null == owner_Connection_21 || !m_RegisteredForEvents)
		{
			owner_Connection_21 = parentGameObject;
		}
		if (null == owner_Connection_29 || !m_RegisteredForEvents)
		{
			owner_Connection_29 = parentGameObject;
		}
		if (null == owner_Connection_31 || !m_RegisteredForEvents)
		{
			owner_Connection_31 = parentGameObject;
		}
		if (null == owner_Connection_34 || !m_RegisteredForEvents)
		{
			owner_Connection_34 = parentGameObject;
		}
		if (null == owner_Connection_45 || !m_RegisteredForEvents)
		{
			owner_Connection_45 = parentGameObject;
		}
		if (null == owner_Connection_87 || !m_RegisteredForEvents)
		{
			owner_Connection_87 = parentGameObject;
		}
		if (null == owner_Connection_91 || !m_RegisteredForEvents)
		{
			owner_Connection_91 = parentGameObject;
		}
		if (null == owner_Connection_96 || !m_RegisteredForEvents)
		{
			owner_Connection_96 = parentGameObject;
		}
		if (null == owner_Connection_98 || !m_RegisteredForEvents)
		{
			owner_Connection_98 = parentGameObject;
		}
		if (null == owner_Connection_108 || !m_RegisteredForEvents)
		{
			owner_Connection_108 = parentGameObject;
		}
		if (null == owner_Connection_112 || !m_RegisteredForEvents)
		{
			owner_Connection_112 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_1.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
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
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_8;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_8;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_8;
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
			uScript_EncounterUpdate component = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_0;
				component.OnSuspend -= Instance_OnSuspend_0;
				component.OnResume -= Instance_OnResume_0;
			}
		}
		if (null != owner_Connection_11)
		{
			uScript_SaveLoad component2 = owner_Connection_11.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_8;
				component2.LoadEvent -= Instance_LoadEvent_8;
				component2.RestartEvent -= Instance_RestartEvent_8;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_2.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_6.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_14.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_15.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.SetParent(g);
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_18.SetParent(g);
		logic_uScript_GetCurrentEncounterPosition_uScript_GetCurrentEncounterPosition_20.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_26.SetParent(g);
		logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_28.SetParent(g);
		logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_30.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_32.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_36.SetParent(g);
		logic_uScript_GetStartPositionAsVector3_uScript_GetStartPositionAsVector3_40.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_46.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_48.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_53.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_56.SetParent(g);
		logic_uScript_Wait_uScript_Wait_58.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_64.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_70.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_76.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_81.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_86.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_88.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_90.SetParent(g);
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_92.SetParent(g);
		logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_93.SetParent(g);
		logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_99.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_100.SetParent(g);
		logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_101.SetParent(g);
		logic_uScript_SpawnEncounterObject_uScript_SpawnEncounterObject_107.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_111.SetParent(g);
		logic_uScript_IsOnScreenMessage_uScript_IsOnScreenMessage_113.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_114.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_116.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_118.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_120.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_121.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_122.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_124.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_127.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_128.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_130.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_134.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_136.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_138.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_139.SetParent(g);
		logic_uScript_HideHint_uScript_HideHint_141.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_145.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_146.SetParent(g);
		logic_uScript_IsBlockLimitEnabled_uScript_IsBlockLimitEnabled_148.SetParent(g);
		logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_149.SetParent(g);
		logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_150.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_151.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_153.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_154.SetParent(g);
		logic_uScript_HideHint_uScript_HideHint_155.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_21 = parentGameObject;
		owner_Connection_29 = parentGameObject;
		owner_Connection_31 = parentGameObject;
		owner_Connection_34 = parentGameObject;
		owner_Connection_45 = parentGameObject;
		owner_Connection_87 = parentGameObject;
		owner_Connection_91 = parentGameObject;
		owner_Connection_96 = parentGameObject;
		owner_Connection_98 = parentGameObject;
		owner_Connection_108 = parentGameObject;
		owner_Connection_112 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_48.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_64.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out += SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out += SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Save_Out += SubGraph_SaveLoadBool_Save_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Load_Out += SubGraph_SaveLoadBool_Load_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_24;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output1 += uScriptCon_ManualSwitch_Output1_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output2 += uScriptCon_ManualSwitch_Output2_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output3 += uScriptCon_ManualSwitch_Output3_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output4 += uScriptCon_ManualSwitch_Output4_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output5 += uScriptCon_ManualSwitch_Output5_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output6 += uScriptCon_ManualSwitch_Output6_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output7 += uScriptCon_ManualSwitch_Output7_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output8 += uScriptCon_ManualSwitch_Output8_41;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.Out += SubGraph_CompleteObjectiveStage_Out_43;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_48.Out += SubGraph_CompleteObjectiveStage_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Save_Out += SubGraph_SaveLoadBool_Save_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Load_Out += SubGraph_SaveLoadBool_Load_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_60;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Save_Out += SubGraph_SaveLoadInt_Save_Out_62;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Load_Out += SubGraph_SaveLoadInt_Load_Out_62;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_62;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_64.Out += SubGraph_LoadObjectiveStates_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Save_Out += SubGraph_SaveLoadBool_Save_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Load_Out += SubGraph_SaveLoadBool_Load_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Save_Out += SubGraph_SaveLoadBool_Save_Out_84;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Load_Out += SubGraph_SaveLoadBool_Load_Out_84;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_84;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Save_Out += SubGraph_SaveLoadBool_Save_Out_142;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Load_Out += SubGraph_SaveLoadBool_Load_Out_142;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_142;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_48.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_64.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.OnEnable();
		logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_28.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_48.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_64.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.OnEnable();
		logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_93.OnEnable();
		logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_99.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_116.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_120.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_122.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_128.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_130.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_134.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_153.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_14.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_26.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_36.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_48.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_53.OnDisable();
		logic_uScript_Wait_uScript_Wait_58.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_64.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_76.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_81.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_86.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_90.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_100.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_111.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_48.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_64.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_48.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_64.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out -= SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out -= SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Save_Out -= SubGraph_SaveLoadBool_Save_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Load_Out -= SubGraph_SaveLoadBool_Load_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_24;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output1 -= uScriptCon_ManualSwitch_Output1_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output2 -= uScriptCon_ManualSwitch_Output2_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output3 -= uScriptCon_ManualSwitch_Output3_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output4 -= uScriptCon_ManualSwitch_Output4_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output5 -= uScriptCon_ManualSwitch_Output5_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output6 -= uScriptCon_ManualSwitch_Output6_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output7 -= uScriptCon_ManualSwitch_Output7_41;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.Output8 -= uScriptCon_ManualSwitch_Output8_41;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.Out -= SubGraph_CompleteObjectiveStage_Out_43;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_48.Out -= SubGraph_CompleteObjectiveStage_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Save_Out -= SubGraph_SaveLoadBool_Save_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Load_Out -= SubGraph_SaveLoadBool_Load_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_60;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Save_Out -= SubGraph_SaveLoadInt_Save_Out_62;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Load_Out -= SubGraph_SaveLoadInt_Load_Out_62;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_62;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_64.Out -= SubGraph_LoadObjectiveStates_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Save_Out -= SubGraph_SaveLoadBool_Save_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Load_Out -= SubGraph_SaveLoadBool_Load_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Save_Out -= SubGraph_SaveLoadBool_Save_Out_84;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Load_Out -= SubGraph_SaveLoadBool_Load_Out_84;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_84;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Save_Out -= SubGraph_SaveLoadBool_Save_Out_142;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Load_Out -= SubGraph_SaveLoadBool_Load_Out_142;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_142;
	}

	private void Instance_OnUpdate_0(object o, EventArgs e)
	{
		Relay_OnUpdate_0();
	}

	private void Instance_OnSuspend_0(object o, EventArgs e)
	{
		Relay_OnSuspend_0();
	}

	private void Instance_OnResume_0(object o, EventArgs e)
	{
		Relay_OnResume_0();
	}

	private void Instance_SaveEvent_8(object o, EventArgs e)
	{
		Relay_SaveEvent_8();
	}

	private void Instance_LoadEvent_8(object o, EventArgs e)
	{
		Relay_LoadEvent_8();
	}

	private void Instance_RestartEvent_8(object o, EventArgs e)
	{
		Relay_RestartEvent_8();
	}

	private void SubGraph_SaveLoadBool_Save_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_CrateSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Save_Out_9();
	}

	private void SubGraph_SaveLoadBool_Load_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_CrateSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Load_Out_9();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_CrateSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Restart_Out_9();
	}

	private void SubGraph_SaveLoadBool_Save_Out_24(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_24;
		Relay_Save_Out_24();
	}

	private void SubGraph_SaveLoadBool_Load_Out_24(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_24;
		Relay_Load_Out_24();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_24(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_24;
		Relay_Restart_Out_24();
	}

	private void uScriptCon_ManualSwitch_Output1_41(object o, EventArgs e)
	{
		Relay_Output1_41();
	}

	private void uScriptCon_ManualSwitch_Output2_41(object o, EventArgs e)
	{
		Relay_Output2_41();
	}

	private void uScriptCon_ManualSwitch_Output3_41(object o, EventArgs e)
	{
		Relay_Output3_41();
	}

	private void uScriptCon_ManualSwitch_Output4_41(object o, EventArgs e)
	{
		Relay_Output4_41();
	}

	private void uScriptCon_ManualSwitch_Output5_41(object o, EventArgs e)
	{
		Relay_Output5_41();
	}

	private void uScriptCon_ManualSwitch_Output6_41(object o, EventArgs e)
	{
		Relay_Output6_41();
	}

	private void uScriptCon_ManualSwitch_Output7_41(object o, EventArgs e)
	{
		Relay_Output7_41();
	}

	private void uScriptCon_ManualSwitch_Output8_41(object o, EventArgs e)
	{
		Relay_Output8_41();
	}

	private void SubGraph_CompleteObjectiveStage_Out_43(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_43 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_43;
		Relay_Out_43();
	}

	private void SubGraph_CompleteObjectiveStage_Out_48(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_48 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_48;
		Relay_Out_48();
	}

	private void SubGraph_SaveLoadBool_Save_Out_60(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = e.boolean;
		local_MsgCrateLanding_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_60;
		Relay_Save_Out_60();
	}

	private void SubGraph_SaveLoadBool_Load_Out_60(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = e.boolean;
		local_MsgCrateLanding_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_60;
		Relay_Load_Out_60();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_60(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = e.boolean;
		local_MsgCrateLanding_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_60;
		Relay_Restart_Out_60();
	}

	private void SubGraph_SaveLoadInt_Save_Out_62(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_62 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_62;
		Relay_Save_Out_62();
	}

	private void SubGraph_SaveLoadInt_Load_Out_62(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_62 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_62;
		Relay_Load_Out_62();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_62(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_62 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_62;
		Relay_Restart_Out_62();
	}

	private void SubGraph_LoadObjectiveStates_Out_64(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_64();
	}

	private void SubGraph_SaveLoadBool_Save_Out_82(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = e.boolean;
		local_CrateUnlocked_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_82;
		Relay_Save_Out_82();
	}

	private void SubGraph_SaveLoadBool_Load_Out_82(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = e.boolean;
		local_CrateUnlocked_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_82;
		Relay_Load_Out_82();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_82(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = e.boolean;
		local_CrateUnlocked_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_82;
		Relay_Restart_Out_82();
	}

	private void SubGraph_SaveLoadBool_Save_Out_84(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = e.boolean;
		local_MsgArrivedAtLocation_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_84;
		Relay_Save_Out_84();
	}

	private void SubGraph_SaveLoadBool_Load_Out_84(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = e.boolean;
		local_MsgArrivedAtLocation_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_84;
		Relay_Load_Out_84();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_84(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = e.boolean;
		local_MsgArrivedAtLocation_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_84;
		Relay_Restart_Out_84();
	}

	private void SubGraph_SaveLoadBool_Save_Out_142(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = e.boolean;
		local_BlockLimitTutorial_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_142;
		Relay_Save_Out_142();
	}

	private void SubGraph_SaveLoadBool_Load_Out_142(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = e.boolean;
		local_BlockLimitTutorial_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_142;
		Relay_Load_Out_142();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_142(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = e.boolean;
		local_BlockLimitTutorial_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_142;
		Relay_Restart_Out_142();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_17();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_In_2()
	{
		logic_uScript_SpawnDeliveryCrate_positionName_2 = cratePositionName;
		logic_uScript_SpawnDeliveryCrate_ownerNode_2 = owner_Connection_3;
		logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_2.In(logic_uScript_SpawnDeliveryCrate_positionName_2, logic_uScript_SpawnDeliveryCrate_ownerNode_2, logic_uScript_SpawnDeliveryCrate_visibleOnRadar_2);
		if (logic_uScript_SpawnDeliveryCrate_uScript_SpawnDeliveryCrate_2.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_4()
	{
		logic_uScriptCon_CompareBool_Bool_4 = local_CrateSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.In(logic_uScriptCon_CompareBool_Bool_4);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.False;
		if (num)
		{
			Relay_In_68();
		}
		if (flag)
		{
			Relay_In_72();
		}
	}

	private void Relay_True_6()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_6.True(out logic_uScriptAct_SetBool_Target_6);
		local_CrateSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_6;
	}

	private void Relay_False_6()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_6.False(out logic_uScriptAct_SetBool_Target_6);
		local_CrateSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_6;
	}

	private void Relay_SaveEvent_8()
	{
		Relay_Save_24();
	}

	private void Relay_LoadEvent_8()
	{
		Relay_Load_24();
	}

	private void Relay_RestartEvent_8()
	{
		Relay_Set_False_24();
	}

	private void Relay_Save_Out_9()
	{
		Relay_Save_82();
	}

	private void Relay_Load_Out_9()
	{
		Relay_Load_82();
	}

	private void Relay_Restart_Out_9()
	{
		Relay_Set_False_82();
	}

	private void Relay_Save_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Load_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Set_True_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Set_False_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_CrateSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_In_14()
	{
		logic_uScript_AddMessage_messageData_14 = msg01GoToLocation;
		logic_uScript_AddMessage_speaker_14 = messageSpeaker;
		logic_uScript_AddMessage_Return_14 = logic_uScript_AddMessage_uScript_AddMessage_14.In(logic_uScript_AddMessage_messageData_14, logic_uScript_AddMessage_speaker_14);
		local_msg01GoToLocation_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_14;
		if (logic_uScript_AddMessage_uScript_AddMessage_14.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_True_15()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_15.True(out logic_uScriptAct_SetBool_Target_15);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_15;
	}

	private void Relay_False_15()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_15.False(out logic_uScriptAct_SetBool_Target_15);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_15;
	}

	private void Relay_In_17()
	{
		logic_uScriptCon_CompareBool_Bool_17 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.In(logic_uScriptCon_CompareBool_Bool_17);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.False;
		if (num)
		{
			Relay_In_20();
		}
		if (flag)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_18()
	{
		logic_uScript_SetEncounterPosition_ownerNode_18 = owner_Connection_19;
		logic_uScript_SetEncounterPosition_position_18 = local_39_UnityEngine_Vector3;
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_18.In(logic_uScript_SetEncounterPosition_ownerNode_18, logic_uScript_SetEncounterPosition_position_18);
		if (logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_18.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_GetCurrentEncounterPosition_ownerNode_20 = owner_Connection_21;
		logic_uScript_GetCurrentEncounterPosition_Return_20 = logic_uScript_GetCurrentEncounterPosition_uScript_GetCurrentEncounterPosition_20.In(logic_uScript_GetCurrentEncounterPosition_ownerNode_20);
		local_EncounterPos_UnityEngine_Vector3 = logic_uScript_GetCurrentEncounterPosition_Return_20;
		if (logic_uScript_GetCurrentEncounterPosition_uScript_GetCurrentEncounterPosition_20.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_Save_Out_24()
	{
		Relay_Save_9();
	}

	private void Relay_Load_Out_24()
	{
		Relay_Load_9();
	}

	private void Relay_Restart_Out_24()
	{
		Relay_Set_False_9();
	}

	private void Relay_Save_24()
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_24 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Save(ref logic_SubGraph_SaveLoadBool_boolean_24, logic_SubGraph_SaveLoadBool_boolAsVariable_24, logic_SubGraph_SaveLoadBool_uniqueID_24);
	}

	private void Relay_Load_24()
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_24 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Load(ref logic_SubGraph_SaveLoadBool_boolean_24, logic_SubGraph_SaveLoadBool_boolAsVariable_24, logic_SubGraph_SaveLoadBool_uniqueID_24);
	}

	private void Relay_Set_True_24()
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_24 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_24, logic_SubGraph_SaveLoadBool_boolAsVariable_24, logic_SubGraph_SaveLoadBool_uniqueID_24);
	}

	private void Relay_Set_False_24()
	{
		logic_SubGraph_SaveLoadBool_boolean_24 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_24 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_24.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_24, logic_SubGraph_SaveLoadBool_boolAsVariable_24, logic_SubGraph_SaveLoadBool_uniqueID_24);
	}

	private void Relay_In_26()
	{
		logic_uScript_AddMessage_messageData_26 = msg05CrateUnlocked;
		logic_uScript_AddMessage_speaker_26 = messageSpeaker;
		logic_uScript_AddMessage_Return_26 = logic_uScript_AddMessage_uScript_AddMessage_26.In(logic_uScript_AddMessage_messageData_26, logic_uScript_AddMessage_speaker_26);
		if (logic_uScript_AddMessage_uScript_AddMessage_26.Shown)
		{
			Relay_In_48();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_CheckDeliveryCrateSpawned_ownerNode_28 = owner_Connection_31;
		logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_28.In(logic_uScript_CheckDeliveryCrateSpawned_ownerNode_28);
		if (logic_uScript_CheckDeliveryCrateSpawned_uScript_CheckDeliveryCrateSpawned_28.Yes)
		{
			Relay_True_6();
		}
	}

	private void Relay_In_30()
	{
		logic_uScript_UnlockDeliveryCrate_ownerNode_30 = owner_Connection_29;
		logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_30.In(logic_uScript_UnlockDeliveryCrate_ownerNode_30);
		if (logic_uScript_UnlockDeliveryCrate_uScript_UnlockDeliveryCrate_30.Out)
		{
			Relay_True_66();
		}
	}

	private void Relay_In_32()
	{
		logic_uScript_RemoveScenery_ownerNode_32 = owner_Connection_34;
		logic_uScript_RemoveScenery_positionName_32 = cratePositionName;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_32.In(logic_uScript_RemoveScenery_ownerNode_32, logic_uScript_RemoveScenery_positionName_32, logic_uScript_RemoveScenery_radius_32, logic_uScript_RemoveScenery_preventChunksSpawning_32);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_32.Out)
		{
			Relay_In_113();
		}
	}

	private void Relay_In_36()
	{
		logic_uScript_AddMessage_messageData_36 = msg04CrateLanded;
		logic_uScript_AddMessage_speaker_36 = messageSpeaker;
		logic_uScript_AddMessage_Return_36 = logic_uScript_AddMessage_uScript_AddMessage_36.In(logic_uScript_AddMessage_messageData_36, logic_uScript_AddMessage_speaker_36);
	}

	private void Relay_In_40()
	{
		logic_uScript_GetStartPositionAsVector3_Return_40 = logic_uScript_GetStartPositionAsVector3_uScript_GetStartPositionAsVector3_40.In();
		local_39_UnityEngine_Vector3 = logic_uScript_GetStartPositionAsVector3_Return_40;
		if (logic_uScript_GetStartPositionAsVector3_uScript_GetStartPositionAsVector3_40.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_Output1_41()
	{
		Relay_In_90();
	}

	private void Relay_Output2_41()
	{
		Relay_In_4();
	}

	private void Relay_Output3_41()
	{
		Relay_In_53();
	}

	private void Relay_Output4_41()
	{
	}

	private void Relay_Output5_41()
	{
	}

	private void Relay_Output6_41()
	{
	}

	private void Relay_Output7_41()
	{
	}

	private void Relay_Output8_41()
	{
	}

	private void Relay_In_41()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_41 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_41.In(logic_uScriptCon_ManualSwitch_CurrentOutput_41);
	}

	private void Relay_Out_43()
	{
	}

	private void Relay_In_43()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_43 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_43, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_43);
	}

	private void Relay_Succeed_46()
	{
		logic_uScript_FinishEncounter_owner_46 = owner_Connection_45;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_46.Succeed(logic_uScript_FinishEncounter_owner_46);
	}

	private void Relay_Fail_46()
	{
		logic_uScript_FinishEncounter_owner_46 = owner_Connection_45;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_46.Fail(logic_uScript_FinishEncounter_owner_46);
	}

	private void Relay_Out_48()
	{
	}

	private void Relay_In_48()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_48 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_48.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_48, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_48);
	}

	private void Relay_In_53()
	{
		logic_uScript_AddMessage_messageData_53 = msg06LeaveArea;
		logic_uScript_AddMessage_speaker_53 = messageSpeaker;
		logic_uScript_AddMessage_Return_53 = logic_uScript_AddMessage_uScript_AddMessage_53.In(logic_uScript_AddMessage_messageData_53, logic_uScript_AddMessage_speaker_53);
		if (logic_uScript_AddMessage_uScript_AddMessage_53.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_56()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_56 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_56.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_56, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_56);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_56.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_In_58()
	{
		logic_uScript_Wait_seconds_58 = crateOpeningTime;
		logic_uScript_Wait_uScript_Wait_58.In(logic_uScript_Wait_seconds_58, logic_uScript_Wait_repeat_58);
		if (logic_uScript_Wait_uScript_Wait_58.Waited)
		{
			Relay_In_26();
		}
	}

	private void Relay_Save_Out_60()
	{
		Relay_Save_142();
	}

	private void Relay_Load_Out_60()
	{
		Relay_Load_142();
	}

	private void Relay_Restart_Out_60()
	{
		Relay_Set_False_142();
	}

	private void Relay_Save_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_MsgCrateLanding_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_MsgCrateLanding_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Save(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Load_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_MsgCrateLanding_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_MsgCrateLanding_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Load(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Set_True_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_MsgCrateLanding_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_MsgCrateLanding_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Set_False_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_MsgCrateLanding_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_MsgCrateLanding_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Save_Out_62()
	{
	}

	private void Relay_Load_Out_62()
	{
		Relay_In_64();
	}

	private void Relay_Restart_Out_62()
	{
	}

	private void Relay_Save_62()
	{
		logic_SubGraph_SaveLoadInt_integer_62 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_62 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Save(logic_SubGraph_SaveLoadInt_restartValue_62, ref logic_SubGraph_SaveLoadInt_integer_62, logic_SubGraph_SaveLoadInt_intAsVariable_62, logic_SubGraph_SaveLoadInt_uniqueID_62);
	}

	private void Relay_Load_62()
	{
		logic_SubGraph_SaveLoadInt_integer_62 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_62 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Load(logic_SubGraph_SaveLoadInt_restartValue_62, ref logic_SubGraph_SaveLoadInt_integer_62, logic_SubGraph_SaveLoadInt_intAsVariable_62, logic_SubGraph_SaveLoadInt_uniqueID_62);
	}

	private void Relay_Restart_62()
	{
		logic_SubGraph_SaveLoadInt_integer_62 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_62 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_62.Restart(logic_SubGraph_SaveLoadInt_restartValue_62, ref logic_SubGraph_SaveLoadInt_integer_62, logic_SubGraph_SaveLoadInt_intAsVariable_62, logic_SubGraph_SaveLoadInt_uniqueID_62);
	}

	private void Relay_Out_64()
	{
	}

	private void Relay_In_64()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_64 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_64.In(logic_SubGraph_LoadObjectiveStates_currentObjective_64);
	}

	private void Relay_True_66()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.True(out logic_uScriptAct_SetBool_Target_66);
		local_CrateUnlocked_System_Boolean = logic_uScriptAct_SetBool_Target_66;
	}

	private void Relay_False_66()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.False(out logic_uScriptAct_SetBool_Target_66);
		local_CrateUnlocked_System_Boolean = logic_uScriptAct_SetBool_Target_66;
	}

	private void Relay_In_68()
	{
		logic_uScriptCon_CompareBool_Bool_68 = local_CrateUnlocked_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.In(logic_uScriptCon_CompareBool_Bool_68);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.False;
		if (num)
		{
			Relay_In_58();
		}
		if (flag)
		{
			Relay_In_99();
		}
	}

	private void Relay_In_70()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_70 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_70.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_70, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_70);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_70.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_72()
	{
		logic_uScriptCon_CompareBool_Bool_72 = local_MsgArrivedAtLocation_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.In(logic_uScriptCon_CompareBool_Bool_72);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.False;
		if (num)
		{
			Relay_In_81();
		}
		if (flag)
		{
			Relay_In_76();
		}
	}

	private void Relay_True_73()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.True(out logic_uScriptAct_SetBool_Target_73);
		local_MsgArrivedAtLocation_System_Boolean = logic_uScriptAct_SetBool_Target_73;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_73.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_False_73()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.False(out logic_uScriptAct_SetBool_Target_73);
		local_MsgArrivedAtLocation_System_Boolean = logic_uScriptAct_SetBool_Target_73;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_73.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_In_76()
	{
		logic_uScript_AddMessage_messageData_76 = msg02ArrivedAtLocation;
		logic_uScript_AddMessage_speaker_76 = messageSpeaker;
		logic_uScript_AddMessage_Return_76 = logic_uScript_AddMessage_uScript_AddMessage_76.In(logic_uScript_AddMessage_messageData_76, logic_uScript_AddMessage_speaker_76);
		if (logic_uScript_AddMessage_uScript_AddMessage_76.Shown)
		{
			Relay_True_73();
		}
	}

	private void Relay_In_81()
	{
		logic_uScript_AddMessage_messageData_81 = msg03CrateLanding;
		logic_uScript_AddMessage_speaker_81 = messageSpeaker;
		logic_uScript_AddMessage_Return_81 = logic_uScript_AddMessage_uScript_AddMessage_81.In(logic_uScript_AddMessage_messageData_81, logic_uScript_AddMessage_speaker_81);
		if (logic_uScript_AddMessage_uScript_AddMessage_81.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_Save_Out_82()
	{
		Relay_Save_84();
	}

	private void Relay_Load_Out_82()
	{
		Relay_Load_84();
	}

	private void Relay_Restart_Out_82()
	{
		Relay_Set_False_84();
	}

	private void Relay_Save_82()
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = local_CrateUnlocked_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_82 = local_CrateUnlocked_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Save(ref logic_SubGraph_SaveLoadBool_boolean_82, logic_SubGraph_SaveLoadBool_boolAsVariable_82, logic_SubGraph_SaveLoadBool_uniqueID_82);
	}

	private void Relay_Load_82()
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = local_CrateUnlocked_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_82 = local_CrateUnlocked_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Load(ref logic_SubGraph_SaveLoadBool_boolean_82, logic_SubGraph_SaveLoadBool_boolAsVariable_82, logic_SubGraph_SaveLoadBool_uniqueID_82);
	}

	private void Relay_Set_True_82()
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = local_CrateUnlocked_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_82 = local_CrateUnlocked_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_82, logic_SubGraph_SaveLoadBool_boolAsVariable_82, logic_SubGraph_SaveLoadBool_uniqueID_82);
	}

	private void Relay_Set_False_82()
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = local_CrateUnlocked_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_82 = local_CrateUnlocked_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_82, logic_SubGraph_SaveLoadBool_boolAsVariable_82, logic_SubGraph_SaveLoadBool_uniqueID_82);
	}

	private void Relay_Save_Out_84()
	{
		Relay_Save_60();
	}

	private void Relay_Load_Out_84()
	{
		Relay_Load_60();
	}

	private void Relay_Restart_Out_84()
	{
		Relay_Set_False_60();
	}

	private void Relay_Save_84()
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = local_MsgArrivedAtLocation_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_84 = local_MsgArrivedAtLocation_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Save(ref logic_SubGraph_SaveLoadBool_boolean_84, logic_SubGraph_SaveLoadBool_boolAsVariable_84, logic_SubGraph_SaveLoadBool_uniqueID_84);
	}

	private void Relay_Load_84()
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = local_MsgArrivedAtLocation_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_84 = local_MsgArrivedAtLocation_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Load(ref logic_SubGraph_SaveLoadBool_boolean_84, logic_SubGraph_SaveLoadBool_boolAsVariable_84, logic_SubGraph_SaveLoadBool_uniqueID_84);
	}

	private void Relay_Set_True_84()
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = local_MsgArrivedAtLocation_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_84 = local_MsgArrivedAtLocation_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_84, logic_SubGraph_SaveLoadBool_boolAsVariable_84, logic_SubGraph_SaveLoadBool_uniqueID_84);
	}

	private void Relay_Set_False_84()
	{
		logic_SubGraph_SaveLoadBool_boolean_84 = local_MsgArrivedAtLocation_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_84 = local_MsgArrivedAtLocation_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_84.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_84, logic_SubGraph_SaveLoadBool_boolAsVariable_84, logic_SubGraph_SaveLoadBool_uniqueID_84);
	}

	private void Relay_In_86()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_86 = owner_Connection_87;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_86.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_86);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_86.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_86.False;
		if (num)
		{
			Relay_In_36();
		}
		if (flag)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_88()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_88 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_88.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_88, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_88);
	}

	private void Relay_In_90()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_90 = owner_Connection_91;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_90.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_90);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_90.True)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_92()
	{
		logic_uScript_SetEncounterPosition_ownerNode_92 = owner_Connection_96;
		logic_uScript_SetEncounterPosition_position_92 = local_VendorPos_UnityEngine_Vector3;
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_92.In(logic_uScript_SetEncounterPosition_ownerNode_92, logic_uScript_SetEncounterPosition_position_92);
	}

	private void Relay_In_93()
	{
		logic_uScript_GetNearestVendorPos_Return_93 = logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_93.In();
		local_VendorPos_UnityEngine_Vector3 = logic_uScript_GetNearestVendorPos_Return_93;
		if (logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_93.Found)
		{
			Relay_In_92();
		}
	}

	private void Relay_In_99()
	{
		logic_uScript_GetDeliveryCrate_ownerNode_99 = owner_Connection_98;
		logic_uScript_GetDeliveryCrate_Return_99 = logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_99.In(logic_uScript_GetDeliveryCrate_ownerNode_99);
		local_Crate_Crate = logic_uScript_GetDeliveryCrate_Return_99;
		if (logic_uScript_GetDeliveryCrate_uScript_GetDeliveryCrate_99.Success)
		{
			Relay_In_101();
		}
	}

	private void Relay_In_100()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_100 = local_Crate_Crate;
		logic_uScript_IsPlayerInRangeOfVisible_range_100 = local_CrateOpenRange_System_Single;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_100.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_100, logic_uScript_IsPlayerInRangeOfVisible_range_100);
		bool inRange = logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_100.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_100.OutOfRange;
		if (inRange)
		{
			Relay_In_70();
		}
		if (outOfRange)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_101()
	{
		logic_uScript_GetCrateOpenTriggerRange_crate_101 = local_Crate_Crate;
		logic_uScript_GetCrateOpenTriggerRange_Return_101 = logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_101.In(logic_uScript_GetCrateOpenTriggerRange_crate_101);
		local_CrateOpenRange_System_Single = logic_uScript_GetCrateOpenTriggerRange_Return_101;
		if (logic_uScript_GetCrateOpenTriggerRange_uScript_GetCrateOpenTriggerRange_101.Out)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_107()
	{
		logic_uScript_SpawnEncounterObject_ownerNode_107 = owner_Connection_108;
		logic_uScript_SpawnEncounterObject_encounterObjectToSpawn_107 = craterPrefab;
		logic_uScript_SpawnEncounterObject_positionName_107 = craterPositionName;
		logic_uScript_SpawnEncounterObject_uScript_SpawnEncounterObject_107.In(logic_uScript_SpawnEncounterObject_ownerNode_107, logic_uScript_SpawnEncounterObject_encounterObjectToSpawn_107, logic_uScript_SpawnEncounterObject_nameWithinEncounter_107, logic_uScript_SpawnEncounterObject_positionName_107);
		if (logic_uScript_SpawnEncounterObject_uScript_SpawnEncounterObject_107.Out)
		{
			Relay_True_15();
		}
	}

	private void Relay_In_111()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_111 = owner_Connection_112;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_111.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_111);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_111.False)
		{
			Relay_In_56();
		}
	}

	private void Relay_In_113()
	{
		logic_uScript_IsOnScreenMessage_onScreenMessage_113 = local_msg01GoToLocation_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_IsOnScreenMessage_uScript_IsOnScreenMessage_113.In(logic_uScript_IsOnScreenMessage_onScreenMessage_113);
		bool num = logic_uScript_IsOnScreenMessage_uScript_IsOnScreenMessage_113.Out;
		bool flag = logic_uScript_IsOnScreenMessage_uScript_IsOnScreenMessage_113.False;
		if (num)
		{
			Relay_In_41();
		}
		if (flag)
		{
			Relay_In_120();
			Relay_In_122();
		}
	}

	private void Relay_In_114()
	{
		logic_uScript_ShowHint_hintId_114 = local_117_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_114.In(logic_uScript_ShowHint_hintId_114);
		if (logic_uScript_ShowHint_uScript_ShowHint_114.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_In_116()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_116 = local_119_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_116.In(logic_uScript_HasHintBeenShownBefore_hintID_116);
		if (logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_116.NotShown)
		{
			Relay_In_118();
		}
	}

	private void Relay_In_118()
	{
		logic_uScript_ShowHint_hintId_118 = local_119_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_118.In(logic_uScript_ShowHint_hintId_118);
	}

	private void Relay_In_120()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_120 = local_117_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_120.In(logic_uScript_HasHintBeenShownBefore_hintID_120);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_120.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_120.NotShown;
		if (shown)
		{
			Relay_In_116();
		}
		if (notShown)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_121()
	{
		logic_uScript_ShowHint_hintId_121 = local_126_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_121.In(logic_uScript_ShowHint_hintId_121);
		if (logic_uScript_ShowHint_uScript_ShowHint_121.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_In_122()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_122 = local_126_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_122.In(logic_uScript_HasHintBeenShownBefore_hintID_122);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_122.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_122.NotShown;
		if (shown)
		{
			Relay_In_128();
		}
		if (notShown)
		{
			Relay_In_121();
		}
	}

	private void Relay_In_124()
	{
		logic_uScript_ShowHint_hintId_124 = local_125_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_124.In(logic_uScript_ShowHint_hintId_124);
		if (logic_uScript_ShowHint_uScript_ShowHint_124.Out)
		{
			Relay_In_130();
		}
	}

	private void Relay_In_127()
	{
		logic_uScript_ShowHint_hintId_127 = local_123_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_127.In(logic_uScript_ShowHint_hintId_127);
	}

	private void Relay_In_128()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_128 = local_125_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_128.In(logic_uScript_HasHintBeenShownBefore_hintID_128);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_128.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_128.NotShown;
		if (shown)
		{
			Relay_In_130();
		}
		if (notShown)
		{
			Relay_In_124();
		}
	}

	private void Relay_In_130()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_130 = local_123_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_130.In(logic_uScript_HasHintBeenShownBefore_hintID_130);
		if (logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_130.NotShown)
		{
			Relay_In_127();
		}
	}

	private void Relay_In_134()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_134 = local_135_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_134.In(logic_uScript_HasHintBeenShownBefore_hintID_134);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_134.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_134.NotShown;
		if (shown)
		{
			Relay_In_111();
		}
		if (notShown)
		{
			Relay_In_136();
		}
	}

	private void Relay_In_136()
	{
		logic_uScript_ShowHint_hintId_136 = local_135_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_136.In(logic_uScript_ShowHint_hintId_136);
		if (logic_uScript_ShowHint_uScript_ShowHint_136.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_True_138()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_138.True(out logic_uScriptAct_SetBool_Target_138);
		local_BlockLimitTutorial_System_Boolean = logic_uScriptAct_SetBool_Target_138;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_138.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_False_138()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_138.False(out logic_uScriptAct_SetBool_Target_138);
		local_BlockLimitTutorial_System_Boolean = logic_uScriptAct_SetBool_Target_138;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_138.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_139()
	{
		logic_uScriptCon_CompareBool_Bool_139 = local_BlockLimitTutorial_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_139.In(logic_uScriptCon_CompareBool_Bool_139);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_139.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_139.False;
		if (num)
		{
			Relay_In_141();
		}
		if (flag)
		{
			Relay_Succeed_46();
		}
	}

	private void Relay_In_141()
	{
		logic_uScript_HideHint_uScript_HideHint_141.In(logic_uScript_HideHint_hintId_141);
		if (logic_uScript_HideHint_uScript_HideHint_141.Out)
		{
			Relay_In_155();
		}
	}

	private void Relay_Save_Out_142()
	{
		Relay_Save_62();
	}

	private void Relay_Load_Out_142()
	{
		Relay_Load_62();
	}

	private void Relay_Restart_Out_142()
	{
		Relay_Restart_62();
	}

	private void Relay_Save_142()
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = local_BlockLimitTutorial_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_142 = local_BlockLimitTutorial_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Save(ref logic_SubGraph_SaveLoadBool_boolean_142, logic_SubGraph_SaveLoadBool_boolAsVariable_142, logic_SubGraph_SaveLoadBool_uniqueID_142);
	}

	private void Relay_Load_142()
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = local_BlockLimitTutorial_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_142 = local_BlockLimitTutorial_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Load(ref logic_SubGraph_SaveLoadBool_boolean_142, logic_SubGraph_SaveLoadBool_boolAsVariable_142, logic_SubGraph_SaveLoadBool_uniqueID_142);
	}

	private void Relay_Set_True_142()
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = local_BlockLimitTutorial_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_142 = local_BlockLimitTutorial_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_142, logic_SubGraph_SaveLoadBool_boolAsVariable_142, logic_SubGraph_SaveLoadBool_uniqueID_142);
	}

	private void Relay_Set_False_142()
	{
		logic_SubGraph_SaveLoadBool_boolean_142 = local_BlockLimitTutorial_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_142 = local_BlockLimitTutorial_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_142.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_142, logic_SubGraph_SaveLoadBool_boolAsVariable_142, logic_SubGraph_SaveLoadBool_uniqueID_142);
	}

	private void Relay_In_145()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_145.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_145.Out)
		{
			Relay_In_146();
		}
	}

	private void Relay_In_146()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_146.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_146.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_148()
	{
		logic_uScript_IsBlockLimitEnabled_uScript_IsBlockLimitEnabled_148.In();
		bool num = logic_uScript_IsBlockLimitEnabled_uScript_IsBlockLimitEnabled_148.True;
		bool flag = logic_uScript_IsBlockLimitEnabled_uScript_IsBlockLimitEnabled_148.False;
		if (num)
		{
			Relay_In_134();
		}
		if (flag)
		{
			Relay_In_145();
		}
	}

	private void Relay_Show_149()
	{
		logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_149.Show(logic_uScript_SetBlockLimitUIState_showReason_149);
		if (logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_149.Out)
		{
			Relay_True_138();
		}
	}

	private void Relay_Hide_149()
	{
		logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_149.Hide(logic_uScript_SetBlockLimitUIState_showReason_149);
		if (logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_149.Out)
		{
			Relay_True_138();
		}
	}

	private void Relay_Show_150()
	{
		logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_150.Show(logic_uScript_SetBlockLimitUIState_showReason_150);
		if (logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_150.Out)
		{
			Relay_Succeed_46();
		}
	}

	private void Relay_Hide_150()
	{
		logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_150.Hide(logic_uScript_SetBlockLimitUIState_showReason_150);
		if (logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_150.Out)
		{
			Relay_Succeed_46();
		}
	}

	private void Relay_In_151()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_151.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_151, logic_uScript_SendAnaliticsEvent_parameterName_151, logic_uScript_SendAnaliticsEvent_parameter_151);
		if (logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_151.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_In_153()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_153 = local_152_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_153.In(logic_uScript_HasHintBeenShownBefore_hintID_153);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_153.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_153.NotShown;
		if (shown)
		{
			Relay_Show_149();
		}
		if (notShown)
		{
			Relay_In_154();
		}
	}

	private void Relay_In_154()
	{
		logic_uScript_ShowHint_hintId_154 = local_152_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_154.In(logic_uScript_ShowHint_hintId_154);
		if (logic_uScript_ShowHint_uScript_ShowHint_154.Out)
		{
			Relay_Show_149();
		}
	}

	private void Relay_In_155()
	{
		logic_uScript_HideHint_uScript_HideHint_155.In(logic_uScript_HideHint_hintId_155);
		if (logic_uScript_HideHint_uScript_HideHint_155.Out)
		{
			Relay_Hide_150();
		}
	}
}
